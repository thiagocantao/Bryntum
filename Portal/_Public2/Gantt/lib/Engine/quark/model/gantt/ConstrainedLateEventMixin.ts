import { Reject } from "../../../../ChronoGraph/chrono/Effect.js"
import { AnyConstructor, Mixin } from '../../../../ChronoGraph/class/BetterMixin.js'
import { CalculationIterator } from '../../../../ChronoGraph/primitives/Calculation.js'
import { calculate, field } from '../../../../ChronoGraph/replica/Entity.js'
import DateHelper from '../../../../Core/helper/DateHelper.js'
import { dateConverter, model_field } from '../../../chrono/ModelFieldAtom.js'
import { DateInterval } from '../../../scheduling/DateInterval.js'
import { Direction, Duration, TimeUnit } from '../../../scheduling/Types.js'
import { MAX_DATE, MIN_DATE, isDateFinite } from "../../../util/Constants.js"
import { HasChildrenMixin } from '../scheduler_basic/HasChildrenMixin.js'
import { ConstrainedEarlyEventMixin, EarlyLateLazyness } from "../scheduler_pro/ConstrainedEarlyEventMixin.js"
import { SchedulerProProjectMixin } from "../scheduler_pro/SchedulerProProjectMixin.js"
import { ConflictEffect, ConflictResolutionResult } from '../../../chrono/Conflict.js'


//---------------------------------------------------------------------------------------------------------------------
/**
 * This mixin provides the constraint-based as-late-as-possible scheduling. See the [[ConstrainedEarlyEventMixin]]
 * for the description of the ASAP constraints-based scheduling. See [[GanttProjectMixin]] for more details about
 * forward/backward, ASAP/ALAP scheduling.
 *
 * It also provides the facilities for calculating the event's [[totalSlack]] and the [[critical]] flag.
 *
 * The ALAP-specific constraints are accumulated in [[lateStartDateConstraintIntervals]], [[lateEndDateConstraintIntervals]] fields.
 */
export class ConstrainedLateEventMixin extends Mixin(
    [ ConstrainedEarlyEventMixin, HasChildrenMixin ],
    (base : AnyConstructor<ConstrainedEarlyEventMixin & HasChildrenMixin, typeof ConstrainedEarlyEventMixin & typeof HasChildrenMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class ConstrainedLateEventMixin extends base {

        project         : SchedulerProProjectMixin

        //--------------------------
        @field({ lazy : EarlyLateLazyness })
        lateStartDateRaw : Date

        @model_field(
            { type : 'date', persist : false },
            { lazy : EarlyLateLazyness, converter : dateConverter, persistent : false }
        )
        lateStartDate : Date


        //--------------------------
        @field({ lazy : EarlyLateLazyness })
        lateEndDateRaw : Date

        @model_field(
            { type : 'date', persist : false },
            { lazy : EarlyLateLazyness, converter : dateConverter, persistent : false }
        )
        lateEndDate : Date


        //--------------------------
        /**
         * An array of intervals, constraining the start date (as point in time) of this event
         * in case the event is scheduled ALAP (as late as possible). It is calculated with [[calculateLateStartDateConstraintIntervals]]
         */
        @field({ lazy : EarlyLateLazyness })
        lateStartDateConstraintIntervals  : DateInterval[]

        /**
         * An array of intervals, constraining the end date (as point in time) of this event
         * in case the event is scheduled ALAP (as late as possible). It is calculated with [[calculateLateEndDateConstraintIntervals]]
         */
        @field({ lazy : EarlyLateLazyness })
        lateEndDateConstraintIntervals  : DateInterval[]

        /**
         * A field storing the _total slack_ (or _total float_) of the event.
         * The _total slack_ is the amount of time that this event can be delayed without causing a delay
         * to the project end.
         * The value is calculated in [[slackUnit]] units.
         */
        @model_field(
            { type : 'number', persist : false },
            { lazy : EarlyLateLazyness, persistent : false }
        )
        totalSlack : Duration

        /**
         * A field storing unit for the [[totalSlack]] value.
         */
        @model_field(
            { type : 'string', defaultValue : TimeUnit.Day, persist : false },
            { lazy : EarlyLateLazyness, converter : DateHelper.normalizeUnit, persistent : false }
        )
        slackUnit : TimeUnit

        /**
         * A boolean field, indicating whether the event is critical or not.
         * The event is __critical__ if its [[totalSlack|total slack]] is zero (or less than zero).
         * This means that if the event is delayed, its successor tasks and the project finish date are delayed as well.
         */
        @model_field(
            { type : 'boolean', defaultValue : false, persist : false },
            { persistent : false, lazy : EarlyLateLazyness }
        )
        critical                        : boolean


        /**
         * Calculation method for the [[lateStartDateConstraintIntervals]]. Returns empty array by default.
         * Override this method to return some extra constraints for the start date during the ALAP scheduling.
         */
        @calculate('lateStartDateConstraintIntervals')
        * calculateLateStartDateConstraintIntervals () : CalculationIterator<this[ 'lateStartDateConstraintIntervals' ]> {
            const intervals : DateInterval[] = []

            const parentEvent : HasChildrenMixin & ConstrainedLateEventMixin = yield this.$.parentEvent

            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.lateStartDateConstraintIntervals

                intervals.push.apply(intervals, parentIntervals)
            }

            return intervals
        }


        /**
         * Calculation method for the [[lateEndDateConstraintIntervals]]. Returns empty array by default.
         * Override this method to return some extra constraints for the end date during the ALAP scheduling.
         */
        @calculate('lateEndDateConstraintIntervals')
        * calculateLateEndDateConstraintIntervals () : CalculationIterator<this[ 'lateEndDateConstraintIntervals' ]> {
            const intervals : DateInterval[] = []

            const parentEvent : HasChildrenMixin & ConstrainedLateEventMixin = yield this.$.parentEvent

            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.lateEndDateConstraintIntervals

                intervals.push.apply(intervals, parentIntervals)

                // If the parent is scheduled manually it should still restrict its children (even though it has no a constraint set)
                // so we append an artificial constraining interval
                if ((yield parentEvent.$.manuallyScheduled) && (yield this.$.direction) === Direction.Backward) {
                    intervals.push(DateInterval.new({
                        endDate   : yield parentEvent.$.endDate
                    }))
                }
            }

            return intervals
        }


        @calculate('lateStartDateRaw')
        * calculateLateStartDateRaw () : CalculationIterator<Date | boolean> {
            const childEvents : Set<HasChildrenMixin & ConstrainedLateEventMixin> = yield this.$.childEvents

            let result : Date

            if (childEvents.size) {
                result = MAX_DATE

                for (let childEvent of childEvents) {
                    const childDate : Date = yield childEvent.$.lateStartDate

                    if (childDate && childDate < result) result = childDate
                }

                result = result.getTime() - MAX_DATE.getTime() ? result : null

            } else {
                if (yield this.$.manuallyScheduled) {
                    return yield this.$.startDate
                }

                if (!(yield* this.isConstrainedLate())) {
                    return yield this.$.startDate
                }

                const startDateConstraintIntervals : DateInterval[] = yield this.$.lateStartDateConstraintIntervals
                const endDateConstraintIntervals : DateInterval[]   = yield this.$.lateEndDateConstraintIntervals

                const effectiveInterval = (yield* this.calculateEffectiveConstraintInterval(
                    true,
                    // need to use concat instead of directly mutating the `startDateConstraintIntervals` since that is
                    // used as storage for `this.$.lateStartDateConstraintIntervals`
                    startDateConstraintIntervals.concat(yield this.$.startDateConstraintIntervals),
                    endDateConstraintIntervals.concat(yield this.$.endDateConstraintIntervals),
                ))

                if (effectiveInterval === null) {
                    return null
                }
                else if (effectiveInterval.isIntervalEmpty()) {
                    const conflict  = ConflictEffect.new()

                    if ((yield conflict) === ConflictResolutionResult.Cancel) {
                        yield Reject(conflict)
                    } else {
                        return null
                    }
                }

                return isDateFinite(effectiveInterval.endDate) ? effectiveInterval.endDate : null
            }

            return result
        }


        @calculate('lateStartDate')
        * calculateLateStartDate () : CalculationIterator<Date | boolean> {
            const date : Date = yield this.$.lateStartDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, true)
        }


        @calculate('lateEndDateRaw')
        * calculateLateEndDateRaw () : CalculationIterator<Date> {
            const childEvents : Set<HasChildrenMixin & ConstrainedLateEventMixin> = yield this.$.childEvents

            let result : Date

            if (childEvents.size) {
                result = MIN_DATE

                for (let childEvent of childEvents) {
                    const childDate : Date = yield childEvent.$.lateEndDate

                    if (childDate && childDate > result) result = childDate
                }

                result = result.getTime() - MIN_DATE.getTime() ? result : null

            } else {
                if (yield this.$.manuallyScheduled) {
                    return yield this.$.endDate
                }

                if (!(yield* this.isConstrainedLate())) {
                    return yield this.$.endDate
                }

                const startDateConstraintIntervals : DateInterval[] = yield this.$.lateStartDateConstraintIntervals
                const endDateConstraintIntervals : DateInterval[]   = yield this.$.lateEndDateConstraintIntervals

                const effectiveInterval = (yield* this.calculateEffectiveConstraintInterval(
                    false,
                    // need to use concat instead of directly mutating the `startDateConstraintIntervals` since that is
                    // used as storage for `this.$.lateStartDateConstraintIntervals`
                    startDateConstraintIntervals.concat(yield this.$.startDateConstraintIntervals),
                    endDateConstraintIntervals.concat(yield this.$.endDateConstraintIntervals),
                ))

                if (effectiveInterval === null) {
                    return null
                }
                else if (effectiveInterval.isIntervalEmpty()) {
                    const conflict  = ConflictEffect.new()

                    if ((yield conflict) === ConflictResolutionResult.Cancel) {
                        yield Reject(conflict)
                    } else {
                        return null
                    }
                }

                return isDateFinite(effectiveInterval.endDate) ? effectiveInterval.endDate : null
            }

            return result
        }


        @calculate('lateEndDate')
        * calculateLateEndDate () : CalculationIterator<Date | boolean> {
            const date : Date = yield this.$.lateEndDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, false)
        }


        @calculate('totalSlack')
        * calculateTotalSlack () : CalculationIterator<Duration> {
            const earlyStartDate = yield this.$.earlyStartDateRaw
            const lateStartDate  = yield this.$.lateStartDateRaw
            const earlyEndDate   = yield this.$.earlyEndDateRaw
            const lateEndDate    = yield this.$.lateEndDateRaw
            const slackUnit      = yield this.$.slackUnit

            let endSlack : Duration, result : Duration

            if ((earlyStartDate && lateStartDate) || (earlyEndDate && lateEndDate)) {
                if (earlyStartDate && lateStartDate) {
                    result = yield* this.calculateProjectedDuration(earlyStartDate, lateStartDate, slackUnit)

                    if (earlyEndDate && lateEndDate) {
                        endSlack = yield* this.calculateProjectedDuration(earlyEndDate, lateEndDate, slackUnit)
                        if (endSlack < result) result = endSlack
                    }
                }
                else if (earlyEndDate && lateEndDate) {
                    result = yield* this.calculateProjectedDuration(earlyEndDate, lateEndDate, slackUnit)
                }
            }

            return result
        }


        @calculate('critical')
        * calculateCritical () : CalculationIterator<boolean> {
            const totalSlack = yield this.$.totalSlack
            return totalSlack <= 0
        }


        * isConstrainedLate () : CalculationIterator<boolean> {
            const startDateIntervals : DateInterval[]                   = yield this.$.startDateConstraintIntervals
            const endDateIntervals : DateInterval[]                     = yield this.$.endDateConstraintIntervals
            const lateStartDateConstraintIntervals : DateInterval[]     = yield this.$.lateStartDateConstraintIntervals
            const lateEndDateConstraintIntervals : DateInterval[]       = yield this.$.lateEndDateConstraintIntervals

            return Boolean(startDateIntervals?.length || endDateIntervals?.length || lateStartDateConstraintIntervals?.length || lateEndDateConstraintIntervals?.length)
        }


        * calculateStartDatePure () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Backward) {
                // early exit if this mixin is not applicable, but only after(!) the direction check
                // this is because the `isConstrainedLate` yield early constraint intervals, which are generally lazy,
                // depending from the direction
                if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                    return yield* superProto.calculateStartDatePure.call(this)
                }

                return yield this.$.lateStartDate
            } else {
                return yield* superProto.calculateStartDatePure.call(this)
            }
        }


        * calculateStartDateProposed () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            switch (direction) {
                case Direction.Backward:
                    // early exit if this mixin is not applicable, but only after(!) the direction check
                    // this is because the `isConstrainedLate` yield early constraint intervals, which are generally lazy,
                    // depending from the direction
                    if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                        return yield* superProto.calculateStartDateProposed.call(this)
                    }

                    return (yield this.$.lateStartDate) || (yield* superProto.calculateStartDateProposed.call(this))
                default:
                    return yield* superProto.calculateStartDateProposed.call(this)
            }
        }


        * calculateEndDatePure () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Backward) {
                // early exit if this mixin is not applicable, but only after(!) the direction check
                // this is because the `isConstrainedLate` yield early constraint intervals, which are generally lazy,
                // depending from the direction
                if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                    return yield* superProto.calculateEndDatePure.call(this)
                }

                return yield this.$.lateEndDate
            } else {
                return yield* superProto.calculateEndDatePure.call(this)
            }
        }


        * calculateEndDateProposed () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            switch (direction) {
                case Direction.Backward:
                    // early exit if this mixin is not applicable, but only after(!) the direction check
                    // this is because the `isConstrainedLate` yield early constraint intervals, which are generally lazy,
                    // depending from the direction
                    if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                        return yield* superProto.calculateEndDateProposed.call(this)
                    }

                    return (yield this.$.lateEndDate) || (yield* superProto.calculateEndDateProposed.call(this))
                default:
                    return yield* superProto.calculateEndDateProposed.call(this)
            }
        }
    }

    return ConstrainedLateEventMixin
}){}
