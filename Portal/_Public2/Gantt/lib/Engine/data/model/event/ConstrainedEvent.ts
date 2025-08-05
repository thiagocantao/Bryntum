import { ChronoIterator } from "../../../../ChronoGraph/chrono/Atom.js"
import { PropagationResult } from "../../../../ChronoGraph/chrono/Graph.js"
import { AnyConstructor, AnyFunction, Mixin } from "../../../../ChronoGraph/class/Mixin.js"
import { calculate, field } from "../../../../ChronoGraph/replica/Entity.js"
import { Conflict, ConflictResolution } from "../../../chrono/Conflict.js"
import { dateConverter, model_field } from "../../../chrono/ModelFieldAtom.js"
import { DateInterval, intersectIntervals } from "../../../scheduling/DateInterval.js"
import { EventMixin } from "./EventMixin.js"
import { ProjectMixin, IProjectMixin } from "../ProjectMixin.js";
import { Duration, TimeUnit } from "../../../scheduling/Types.js";
import DateHelper from "../../../../Common/helper/DateHelper.js";


export class ConstraintInterval extends DateInterval {
    originDescription   : string

    // what to do, if user decides to remove this constraining interval - for example, remove the dependency, or remove the constraint
    onRemoveAction      : AnyFunction

    toString () : string {
        return `from ${this.startDate} till ${this.endDate}`
    }
}

//---------------------------------------------------------------------------------------------------------------------
export class RemoveConstrainingInterval extends ConflictResolution {

    interval        : ConstraintInterval


    get originDescription () : string {
        return this.interval.originDescription
    }


    resolve () {
        if (!this.interval) throw new Error("Can't use this resolution option - no constraint interval available")
        if (!this.interval.onRemoveAction) throw new Error("Can't use this resolution option - no `onRemoveAction` available")

        this.interval.onRemoveAction()
    }
}

export class IntervalConflict extends Conflict {
    proposedDate        : Date

    conflictingInterval : ConstraintInterval

    conflictingEvent    : ConstrainedEvent

    get description () : string {
        return `The change causes scheduling conflict with the constraining interval ${this.conflictingInterval},
which is created by the ${ this.conflictingInterval.originDescription }`
    }

    get resolutions () : ConflictResolution[] {
        const resolutions   = []

        if (this.conflictingInterval.onRemoveAction)
            resolutions.push(
                RemoveConstrainingInterval.new({ interval : this.conflictingInterval })
            )

        // lazy property pattern
        Object.defineProperty(this, 'resolutions', { value : resolutions })

        return resolutions
    }
}

//---------------------------------------------------------------------------------------------------------------------
export class ProposedDateOutsideOfConstraint extends IntervalConflict {
    proposedDate        : Date

    get description () : string {
        return `The date ${this.proposedDate} is outside of the constraining interval ${this.conflictingInterval},
which is created by the ${this.conflictingInterval.originDescription}`
    }
}

//---------------------------------------------------------------------------------------------------------------------
export const ConstrainedEvent = <T extends AnyConstructor<EventMixin>>(base : T) => {

    class ConstrainedEvent extends base {

        @field()
        earlyStartDateRaw : Date

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        earlyStartDate : Date

        @field()
        earlyEndDateRaw : Date

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        earlyEndDate : Date

        @field()
        lateStartDateRaw : Date

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        lateStartDate : Date

        @field()
        lateEndDateRaw : Date

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        lateEndDate : Date

        /**
         * A field storing the _total slack_ (or _total float_) of the event.
         * The _total slack_ is the amount of time that this event can be delayed without causing a delay
         * to the project end.
         * The value is calculated in [[slackUnit]] units.
         */
        @model_field(
            { type : 'number', persist : false },
            { persistent : false }
        )
        totalSlack : Duration

        /**
         * A field storing unit for the [[totalSlack]] value.
         */
        @model_field(
            { type : 'string', defaultValue : TimeUnit.Day, persist : false },
            { converter : DateHelper.normalizeUnit, persistent : false }
        )
        slackUnit : TimeUnit

        /**
         * A boolean field, indicating whether the event is critical or not.
         * The event is __critical__ if its {@link #getTotalSlack total slack} is zero (or less than zero).
         * This means that if the event is delayed, its successor tasks and the project finish date are delayed as well.
         */
        @model_field(
            { type : 'boolean', defaultValue : false, persist : false },
            { persistent : false }
        )
        critical                        : boolean

        /**
         * A boolean field, indicating whether this event actually respects the constrain intervals provided by the
         * [[calculateStartDateConstraintIntervals]]/[[calculateEndDateConstraintIntervals]]. Setting it to `true`
         * will "downgrade" the event to raw [[EventMixin]]
         */
        @model_field({ type : 'boolean', defaultValue : false })
        manuallyScheduled               : boolean

        /**
         * An array of intervals, constraining the start date (as point in time) of this event.
         * Effectively when scheduling the event the full list of its restrictions is calculated as the combination of this array and
         * either [[earlyStartDateConstraintIntervals]] or [[lateStartDateConstraintIntervals]] array
         * (depending on the event scheduling ASAP or ALAP respectively).
         */
        @field()
        startDateConstraintIntervals      : ConstraintInterval[]

        /**
         * An array of intervals, constraining the start date (as point in time) of this event
         * in case the event is scheduled ASAP (as soon as possible).
         */
        @field()
        earlyStartDateConstraintIntervals : ConstraintInterval[]

        /**
         * An array of intervals, constraining the start date (as point in time) of this event
         * in case the event is scheduled ALAP (as late as possible).
         */
        @field()
        lateStartDateConstraintIntervals  : ConstraintInterval[]

        // Combination of startDateConstraintIntervals and earlyStartDateConstraintIntervals (or lateStartDateConstraintIntervals depending on the scheduling ASAP or ALAP)
        // which represents the effective list of the task restrictions
        @field()
        combinedStartDateConstraintIntervals : ConstraintInterval[]

        /**
         * An array of intervals, constraining the end date (as point in time) of this event.
         * Effectively when scheduling the event the full list of its restrictions is calculated as the combination of this array and
         * either [[earlyEndDateConstraintIntervals]] or [[lateEndDateConstraintIntervals]] array
         * (depending on the event scheduling ASAP or ALAP respectively).
         */
        @field()
        endDateConstraintIntervals      : ConstraintInterval[]

        /**
         * An array of intervals, constraining the end date (as point in time) of this event
         * in case the event is scheduled ASAP (as soon as possible).
         */
        @field()
        earlyEndDateConstraintIntervals : ConstraintInterval[]

        /**
         * An array of intervals, constraining the end date (as point in time) of this event
         * in case the event is scheduled ALAP (as late as possible).
         */
        @field()
        lateEndDateConstraintIntervals  : ConstraintInterval[]

        // Combination of startDateConstraintIntervals and earlyEndDateConstraintIntervals or lateEndDateConstraintIntervals (depending on the scheduling ASAP or ALAP)
        // which represents the effective list of the task restrictions
        @field()
        combinedEndDateConstraintIntervals : ConstraintInterval[]

        @field()
        validInitialIntervals           : { startDateInterval : DateInterval, endDateInterval : DateInterval }

        async setManuallyScheduled (mode : boolean) : Promise<PropagationResult> {
            this.$.manuallyScheduled.put(mode)
            return this.propagate()
        }


        * validateProposedStartDate (startDate : Date) {
            // if we have a proposed date let's validate it against registered constraining intervals
            if (startDate) {
                // need to adjust the proposed date according to the calendar, to avoid unnecessary conflicts
                const adjustedProposedDate : Date = yield* this.skipNonWorkingTime(startDate, true)

                const startDateIntervals : ConstraintInterval[] = yield this.$.combinedStartDateConstraintIntervals

                let acc : DateInterval      = DateInterval.new()

                for (let interval of startDateIntervals) {
                    acc                     = acc.intersect(interval)

                    if (!acc.containsDate(adjustedProposedDate)) {
                        yield ProposedDateOutsideOfConstraint.new({
                            proposedDate        : adjustedProposedDate,
                            conflictingInterval : interval
                        })
                    }
                }
            }
        }


        * validateProposedEndDate (endDate : Date) : ChronoIterator<Date> {
            if (endDate) {
                const adjustedProposedDate : Date = yield* this.skipNonWorkingTime(endDate, false)

                const endDateIntervals : ConstraintInterval[] = yield this.$.combinedEndDateConstraintIntervals

                let acc : DateInterval      = DateInterval.new()

                for (let interval of endDateIntervals) {
                    acc                     = acc.intersect(interval)

                    if (!acc.containsDate(adjustedProposedDate)) {
                        yield ProposedDateOutsideOfConstraint.new({
                            proposedDate        : adjustedProposedDate,
                            conflictingInterval : interval
                        })
                    }
                }
            }
        }


        @calculate('earlyStartDateRaw')
        * calculateEarlyStartDateRaw () : ChronoIterator<Date | boolean> {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateStartDateInitial()

            const project : IProjectMixin        = this.getProject(),
                projectStartDate : Date          = yield project.$.startDate,
                validInitialIntervals            = yield this.$.validInitialIntervals,
                startDateInterval : DateInterval = validInitialIntervals.startDateInterval

            return startDateInterval.startDateIsFinite() ? startDateInterval.startDate : projectStartDate
        }


        @calculate('earlyStartDate')
        * calculateEarlyStartDate () : ChronoIterator<Date | boolean> {
            const date : Date = yield this.$.earlyStartDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, true)
        }


        @calculate('earlyEndDateRaw')
        * calculateEarlyEndDateRaw () : ChronoIterator<Date | boolean> {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateEndDateInitial()

            const project : IProjectMixin            = this.getProject(),
                projectStartDate : Date              = yield project.$.startDate,
                validInitialIntervals                = yield this.$.validInitialIntervals,
                endDateInterval : DateInterval       = validInitialIntervals.endDateInterval,
                canCalculateProjectedXDate : boolean = yield* this.canCalculateProjectedXDate()

            let result : Date = null

            if (endDateInterval.startDateIsFinite()) {
                result = endDateInterval.startDate

            // if no end date restrictions are found we use formula: task early end == project start - task duration (if duration is available)
            } else if (projectStartDate && canCalculateProjectedXDate) {
                result = yield* this.calculateProjectedEndDate(projectStartDate)
            }

            return result
        }


        @calculate('earlyEndDate')
        * calculateEarlyEndDate () : ChronoIterator<Date | boolean> {
            const date : Date = yield this.$.earlyEndDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, false)
        }


        @calculate('lateStartDateRaw')
        * calculateLateStartDateRaw () : ChronoIterator<Date | boolean> {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateStartDateInitial()

            const project : IProjectMixin            = this.getProject(),
                projectEndDate : Date                = yield project.$.endDate,
                validInitialIntervals                = yield this.$.validInitialIntervals,
                startDateInterval : DateInterval     = validInitialIntervals.startDateInterval,
                canCalculateProjectedXDate : boolean = yield* this.canCalculateProjectedXDate()

            let result : Date = null

            if (startDateInterval.endDateIsFinite()) {
                result = startDateInterval.endDate

            } else if (projectEndDate && canCalculateProjectedXDate) {
                result = yield* this.calculateProjectedStartDate(projectEndDate)
            }

            return result
        }


        @calculate('lateStartDate')
        * calculateLateStartDate () : ChronoIterator<Date | boolean> {
            const date : Date = yield this.$.lateStartDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, true)
        }


        @calculate('lateEndDateRaw')
        * calculateLateEndDateRaw () : ChronoIterator<Date | boolean> {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateEndDateInitial()

            const project : IProjectMixin      = this.getProject(),
                projectEndDate : Date          = yield project.$.endDate,
                validInitialIntervals          = yield this.$.validInitialIntervals,
                endDateInterval : DateInterval = validInitialIntervals.endDateInterval

            return endDateInterval.endDateIsFinite() ? endDateInterval.endDate : projectEndDate
        }


        @calculate('lateEndDate')
        * calculateLateEndDate () : ChronoIterator<Date | boolean> {
            const date : Date = yield this.$.lateEndDateRaw

            return yield* this.maybeSkipNonWorkingTime(date, false)
        }


        @calculate('totalSlack')
        * calculateTotalSlack () : ChronoIterator<Duration> {
            const earlyStartDate = yield this.$.earlyStartDateRaw
            const lateStartDate  = yield this.$.lateStartDateRaw
            const earlyEndDate   = yield this.$.earlyEndDateRaw
            const lateEndDate    = yield this.$.lateEndDateRaw
            const slackUnit      = yield this.$.slackUnit

            let endSlack : Duration, result : Duration

            if ((earlyStartDate && lateStartDate) || (earlyEndDate && lateEndDate)) {
                if (earlyStartDate && lateStartDate) {
                    result = yield* this.calculateDurationBetweenDates(earlyStartDate, lateStartDate, slackUnit)

                    if (earlyEndDate && lateEndDate) {
                        endSlack = yield* this.calculateDurationBetweenDates(earlyEndDate, lateEndDate, slackUnit)
                        if (endSlack < result) result = endSlack
                    }
                } else if (earlyEndDate && lateEndDate) {
                    result = yield* this.calculateDurationBetweenDates(earlyEndDate, lateEndDate, slackUnit)
                }
            }

            return result
        }


        @calculate('critical')
        * calculateCritical () : ChronoIterator<number> {
            const totalSlack = yield this.$.totalSlack
            return totalSlack <= 0
        }


        * calculateStartDateInitial () : ChronoIterator<Date | any> {
            const proposedValue = this.$.startDate.proposedValue

            // early exit in manually scheduled case
            const manuallyScheduled = yield this.$.manuallyScheduled
            if (manuallyScheduled) return yield* super.calculateStartDateInitial()

            // Trigger effective constraint intervals calculation
            // since when we exit in the following line (happens during data normalization)
            // the graph doesn't build an edge - start date dependency on the intervals
            yield this.$.combinedStartDateConstraintIntervals
            yield this.$.combinedEndDateConstraintIntervals

            // If we normalize the task that does not have duration provided
            // and we have start date provided -> return start date as-is to calculate duration based on it
            if (!this.$.duration.hasConsistentValue() && !this.$.duration.hasProposedValue() && proposedValue) {
                return proposedValue
            }

            // user want to "unschedule the task"
            if (proposedValue === null) return null

            // if we have a proposed date let's validate it against registered constraining intervals
            yield* this.validateProposedStartDate(proposedValue)

            // TODO should also:
            // 1) check if proposed start date is inside the `validInitialIntervals.startDateInterval`
            // (which has an additional constraint interval, calculated by the end date)
            // 2) check if proposed start date is inside `validInitialIntervals.startDateInterval`, but
            // not its start point (which is required by the currently implicit ASAP constraint)
            // then it should yield a conflict like: ProposedDateViolateASAPConstraint
            // the resolutions would be:
            // - remove constraint
            // - add additional "Must start on" constraint
            // - add additional "Must start no sooner than" constraint
            // - switch the task to "manual" scheduling mode (whatever it means)

            return (yield this.$.earlyStartDateRaw) || (yield* super.calculateStartDateInitial())
        }


        * calculateEndDateInitial () : ChronoIterator<Date | any> {
            const proposedValue = this.$.endDate.proposedValue

            // early exit in manually scheduled case
            const manuallyScheduled = yield this.$.manuallyScheduled
            if (manuallyScheduled) return yield* super.calculateEndDateInitial()

            // Trigger effective constraint intervals calculation
            // since when we exit in the following line (happens during data normalization)
            // the graph doesn't build an edge - end date dependency on the intervals
            yield this.$.combinedStartDateConstraintIntervals
            yield this.$.combinedEndDateConstraintIntervals

            // If we normalize the task that does not have duration provided
            // and we have end date provided -> return end date as-is to calculate duration based on it
            if (!this.$.duration.hasConsistentValue() && !this.$.duration.hasProposedValue() && proposedValue) {
                return proposedValue
            }

            // user want to "unschedule the task"
            if (proposedValue === null) return null

            yield* this.validateProposedEndDate(proposedValue)

            return (yield this.$.earlyEndDateRaw) || (yield* super.calculateEndDateInitial())
        }


        // TODO make this method smart in regard of providing conflict resolution information
        /**
         * Finds the intersection of provided intervals.
         * If some of the intervals doesn't intersect the methods yields IntervalConflict
         * with the interval reference.
         * @param intervals Intervals to find intersection of
         * @private
         */
        * validateIntervalsIntersection (intervals : ConstraintInterval[]) : ChronoIterator<DateInterval> {
            let intersection : DateInterval = DateInterval.new()

            for (let interval of intervals) {
                intersection = intersection.intersect(interval)

                if (intersection.isIntervalEmpty()) {
                    // if some interval has no intersection w/ other(s) we throw a conflict
                    yield IntervalConflict.new({ conflictingInterval : interval })
                }
            }

            return intersection
        }


        * calculateStartDateIntervalsByEndDateIntervals (intervals : ConstraintInterval[]) : ChronoIterator<Date | ConstraintInterval[]> {
            let result : ConstraintInterval[] = []

            for (let interval of intervals) {
                result.push(ConstraintInterval.new({
                    onRemoveAction    : interval.onRemoveAction,
                    originDescription : interval.originDescription,
                    startDate         : interval.startDateIsFinite() ? yield* this.calculateProjectedStartDate(interval.startDate) : null,
                    endDate           : interval.endDateIsFinite() ? yield* this.calculateProjectedStartDate(interval.endDate) : null,
                }))
            }

            return result
        }


        * calculateEndDateIntervalsByStartDateIntervals (intervals : ConstraintInterval[]) : ChronoIterator<Date | ConstraintInterval[]> {
            let result : ConstraintInterval[] = []

            for (let interval of intervals) {
                result.push(ConstraintInterval.new({
                    onRemoveAction    : interval.onRemoveAction,
                    originDescription : interval.originDescription,
                    startDate         : interval.startDateIsFinite() ? yield* this.calculateProjectedEndDate(interval.startDate) : null,
                    endDate           : interval.endDateIsFinite() ? yield* this.calculateProjectedEndDate(interval.endDate) : null,
                }))
            }

            return result
        }


        // Indicates if calculateProjectedStartDate and calculateProjectedStartDate method can calculate values.
        * canCalculateProjectedXDate () : ChronoIterator<boolean> {
            // need to have duration value
            return !(yield* this.shouldRecalculateDuration())
        }


        @calculate('validInitialIntervals')
        * calculateValidInitialIntervals () : ChronoIterator<boolean | Date | this[ 'validInitialIntervals' ] | ConstraintInterval[]> {

            const startDateIntervals : ConstraintInterval[] = yield this.$.combinedStartDateConstraintIntervals
            const endDateIntervals : ConstraintInterval[]   = yield this.$.combinedEndDateConstraintIntervals

            // calculate effective start date constraining interval
            let startDateInterval : DateInterval = intersectIntervals(startDateIntervals)
            // calculate effective end date constraining interval
            let endDateInterval : DateInterval   = intersectIntervals(endDateIntervals)

            const canCalculateProjectedXDate = yield* this.canCalculateProjectedXDate()

            // if we can't use calculateProjectedStartDate/calculateProjectedEndDate methods then we cannot do anything else -> return
            if (!canCalculateProjectedXDate && !startDateInterval.isIntervalEmpty() && !endDateInterval.isIntervalEmpty()) {
                return {
                    startDateInterval,
                    endDateInterval
                }
            }

            const additionalConstraintForStartDate  = DateInterval.new({
                startDate   : endDateInterval.startDateIsFinite() ? yield* this.calculateProjectedStartDate(endDateInterval.startDate) : null,
                endDate     : endDateInterval.endDateIsFinite() ? yield* this.calculateProjectedStartDate(endDateInterval.endDate) : null,
            })

            startDateInterval = startDateInterval.intersect(additionalConstraintForStartDate)

            // If no intersection w/ additional interval let's intersects intervals one by one
            // and yield Conflict
            if (startDateInterval.isIntervalEmpty()) {
                const reflectedIntervals = yield* this.calculateStartDateIntervalsByEndDateIntervals(endDateIntervals)

                let combinedIntervals

                if (startDateIntervals.length > 1) {
                    combinedIntervals = startDateIntervals
                        .slice(0, startDateIntervals.length - 1)
                        .concat(reflectedIntervals)
                        .concat(startDateIntervals[startDateIntervals.length - 1])
                } else {
                    combinedIntervals = startDateIntervals.concat(reflectedIntervals)
                }

                yield* this.validateIntervalsIntersection(combinedIntervals)

            } else {

                const additionalConstraintForEndDate    = DateInterval.new({
                    startDate   : startDateInterval.startDateIsFinite() ? yield* this.calculateProjectedEndDate(startDateInterval.startDate) : null,
                    endDate     : startDateInterval.endDateIsFinite() ? yield* this.calculateProjectedEndDate(startDateInterval.endDate) : null,
                })

                endDateInterval = endDateInterval.intersect(additionalConstraintForEndDate)
            }

            return {
                startDateInterval,
                endDateInterval
            }
        }


        /**
         * A template method which should provide an array on intervals, constraining the start date of this event.
         * Supposed to be overridden in other mixins.
         */
        @calculate('startDateConstraintIntervals')
        protected * calculateStartDateConstraintIntervals () : ChronoIterator<this[ 'startDateConstraintIntervals' ]> {
            return []
        }

        @calculate('combinedStartDateConstraintIntervals')
        protected * calculateCombinedStartDateConstraintIntervals () : ChronoIterator<this[ 'combinedStartDateConstraintIntervals' ]> {
            let result : this[ 'combinedStartDateConstraintIntervals' ] = yield this.$.startDateConstraintIntervals

            // TODO: add ALAP support
            if (true) {
                result = result.concat(yield this.$.earlyStartDateConstraintIntervals)
            } else {
                result = result.concat(yield this.$.lateStartDateConstraintIntervals)
            }

            return result
        }

        @calculate('earlyStartDateConstraintIntervals')
        protected * calculateEarlyStartDateConstraintIntervals () : ChronoIterator<this[ 'earlyStartDateConstraintIntervals' ]> {
            return []
        }


        @calculate('lateStartDateConstraintIntervals')
        protected * calculateLateStartDateConstraintIntervals () : ChronoIterator<this[ 'lateStartDateConstraintIntervals' ]> {
            return []
        }


        /**
         * A template method which should provide an array on intervals, constraining the end date of this event.
         * Supposed to be overridden in other mixins.
         */
        @calculate('endDateConstraintIntervals')
        protected * calculateEndDateConstraintIntervals () : ChronoIterator<this[ 'endDateConstraintIntervals' ]> {
            return []
        }

        @calculate('combinedEndDateConstraintIntervals')
        protected * calculateCombinedEndDateConstraintIntervals () : ChronoIterator<this[ 'combinedEndDateConstraintIntervals' ]> {
            let result : this[ 'combinedEndDateConstraintIntervals' ] = yield this.$.endDateConstraintIntervals

            // TODO: add ALAP support
            if (true) {
                result = result.concat(yield this.$.earlyEndDateConstraintIntervals)
            } else {
                result = result.concat(yield this.$.lateEndDateConstraintIntervals)
            }

            return result
        }


        @calculate('earlyEndDateConstraintIntervals')
        protected * calculateEarlyEndDateConstraintIntervals () : ChronoIterator<this[ 'earlyEndDateConstraintIntervals' ]> {
            return []
        }


        @calculate('lateEndDateConstraintIntervals')
        protected * calculateLateEndDateConstraintIntervals () : ChronoIterator<this[ 'lateEndDateConstraintIntervals' ]> {
            return []
        }


        putEndDate (date : Date, keepDuration : boolean = false) {
            // Force intervals recalculation
            // Covered by 10_basic and 50_milestone
            this.markAsNeedRecalculation(this.$.validInitialIntervals)

            super.putEndDate(date, keepDuration)
        }
    }

    return ConstrainedEvent
}

/**
 * This is a "constrained event" mixin.
 *
 * It provides both a generic functionality of constraining the start/end dates of the event using constraint intervals
 * and _critical path method_ related API ([[earlyStartDate]], [[earlyEndDate]], [[lateStartDate]], [[lateEndDate]], [[totalSlack]], [[critical]]).
 *
 * It does not implement any constraining logic itself though, leaving this task to other mixins.
 */
export interface ConstrainedEvent extends Mixin<typeof ConstrainedEvent> {}
