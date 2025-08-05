import { ChronoIterator } from "../../../../ChronoGraph/chrono/Atom.js"
import { PropagationResult } from "../../../../ChronoGraph/chrono/Graph.js"
import { AnyConstructor, Mixin } from "../../../../ChronoGraph/class/Mixin.js"
import { calculate } from "../../../../ChronoGraph/replica/Entity.js"
import DateHelper from "../../../../Common/helper/DateHelper.js"
import { CalendarMixin } from "../../../calendar/CalendarMixin.js"
import { dateConverter, model_field } from "../../../chrono/ModelFieldAtom.js"
import { Duration, SchedulingMode, TimeUnit } from "../../../scheduling/Types.js"
import { HasCalendarMixin } from "../HasCalendarMixin.js"

const hasMixin = Symbol('EventMixin')

//---------------------------------------------------------------------------------------------------------------------
export const EventMixin = <T extends AnyConstructor<HasCalendarMixin>>(base : T) => {

    class EventMixin extends base {

        [hasMixin] () {}

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        startDateInitial : Date

        @model_field({ type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ' }, { converter : dateConverter })
        startDate       : Date

        @model_field(
            { type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ', persist : false },
            { converter : dateConverter, persistent : false }
        )
        endDateInitial  : Date

        @model_field({ type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ' }, { converter : dateConverter })
        endDate         : Date

        @model_field({ type : 'number', allowNull : true })
        duration        : Duration

        @model_field({ type : 'string', defaultValue : TimeUnit.Day }, { converter : DateHelper.normalizeUnit })
        durationUnit    : TimeUnit

        // TODO: do we really need "Normal" scheduling mode?
        @model_field({ type : 'string', defaultValue : SchedulingMode.Normal })
        schedulingMode  : SchedulingMode


        @calculate('schedulingMode')
        * calculateSchedulingMode (proposedValue : SchedulingMode) : ChronoIterator<SchedulingMode> {
            return (proposedValue !== undefined ? proposedValue : this.$.schedulingMode.getConsistentValue())
                // empty scheduling mode always falls back to 'Normal'
                || SchedulingMode.Normal
        }


        // Skips non-working time if it's needed to the event
        * maybeSkipNonWorkingTime (date : Date, isForward : boolean = true) : ChronoIterator<Date | boolean> {
            let skipNonWorkingTime = true

            if (!(yield* this.shouldRecalculateDuration())) {
                const duration : Duration   = yield this.$.duration

                skipNonWorkingTime          = duration > 0
            }

            return date && skipNonWorkingTime ? yield* this.skipNonWorkingTime(date, isForward) : date
        }


        * skipNonWorkingTime (date : Date, isForward : boolean = true) : ChronoIterator<Date> {
            const calendar : CalendarMixin = yield this.$.calendar

            return calendar.skipNonWorkingTime(date, isForward)
        }


        * calculateDurationBetweenDates (startDate : Date, endDate : Date, unit : TimeUnit) : ChronoIterator<Duration> {
            const calendar : CalendarMixin = yield this.$.calendar

            return calendar.calculateDuration(startDate, endDate, unit)
        }


        convertDuration (duration : Duration, fromUnit : TimeUnit, toUnit : TimeUnit) {
            const projectCalendar = this.getProject().calendar

            return projectCalendar.convertDuration(duration, fromUnit, toUnit)
        }


        * $convertDuration (duration : Duration, fromUnit : TimeUnit, toUnit : TimeUnit) {
            const project : this['project']     = this.getProject(),
                projectCalendar : CalendarMixin = yield project.$.calendar

            return projectCalendar.convertDuration(duration, fromUnit, toUnit)
        }


        //#region StartDate
        @calculate('startDateInitial')
        * calculateStartDateInitial () : ChronoIterator<any | Date> {
            const proposedValue = this.$.startDate.proposedValue

            if (proposedValue !== undefined) return proposedValue

            const shouldRecalculateStartDate    = yield* this.shouldRecalculateStartDate()

            if (shouldRecalculateStartDate) {
                return yield* this.calculateProjectedStartDate(yield this.$.endDate)
            } else {
                // otherwise keep stable value
                return this.$.startDate.getConsistentValue()
            }
        }


        * canRecalculateStartDate () : ChronoIterator<boolean> {
            // we can recalculate start date if end date and duration is given
            return (this.$.endDate.hasProposedValue() || this.$.endDate.hasConsistentValue())
                && (this.$.duration.hasProposedValue() || this.$.duration.hasConsistentValue())
        }


        * shouldRecalculateStartDate () : ChronoIterator<boolean> {
            // we should recalculate start date if:
            // - no start date is filled yet
            // - end date is being changed and keepDuration is true
            // - duration is being changed and keepDuration is true
            return (!this.$.startDate.value
                || (this.$.endDate.proposedArgs && this.$.endDate.proposedArgs[ 1 ] === true)
                || (this.$.duration.proposedArgs && this.$.duration.proposedArgs[ 1 ] === false)
            ) && (yield* this.canRecalculateStartDate())
        }


        @calculate('startDate')
        * calculateStartDate (proposedValue : Date) : ChronoIterator<Date | boolean> {
            const startDateInitial : Date   = yield this.$.startDateInitial

            return yield* this.maybeSkipNonWorkingTime(startDateInitial)
        }


        * calculateProjectedStartDate (endDate : Date) : ChronoIterator<Date | any> {
            const calendar : CalendarMixin      = yield this.$.calendar,
                duration : Duration             = yield this.$.duration,
                durationUnit : TimeUnit         = yield this.$.durationUnit

            // Project calendar is used for units conversion
            const durationMS : Duration = yield* this.$convertDuration(duration, durationUnit, TimeUnit.Millisecond)

            return calendar.calculateStartDate(endDate, durationMS, TimeUnit.Millisecond)
        }
        //#endregion


        //#region EndDate
        @calculate('endDateInitial')
        * calculateEndDateInitial () : ChronoIterator<any | Date> {
            const proposedValue = this.$.endDate.proposedValue

            if (proposedValue !== undefined) return proposedValue

            const shouldRecalculateEndDate  = yield* this.shouldRecalculateEndDate()

            if (shouldRecalculateEndDate) {
                return yield* this.calculateProjectedEndDate(yield this.$.startDate)
            } else {
                // otherwise keep stable value
                return this.$.endDate.getConsistentValue()
            }
        }


        // End date can be calculated if there are start and duration values
        * canRecalculateEndDate () : ChronoIterator<boolean> {
            // not just `this.$.startDate.hasValue()` because that includes `nextStableValue` which appears during propagation
            return (this.$.startDate.hasProposedValue() || this.$.startDate.hasConsistentValue())
                // if duration is provided or we can calculate it
                // not just `this.$.duration.hasValue()` because that includes `nextStableValue` which appears during propagation
                && (this.$.duration.hasProposedValue() || this.$.duration.hasConsistentValue() || (yield* this.canRecalculateDuration()))
        }


        // Signalizes to recalculate end date if:
        // - end date has not value
        // - or start date is being changed by setStartDate() call and keepDuration is true
        // - or duration is being changed by setDuration() call and keepStartDate is true
        * shouldRecalculateEndDate () : ChronoIterator<boolean> {
            return (!this.$.endDate.hasConsistentValue()
                || (this.$.startDate.hasProposedValue() && this.$.startDate.proposedArgs[ 1 ] === true)
                || (this.$.duration.hasProposedValue() && this.$.duration.proposedArgs[ 1 ] === true)
                // "default" case, when data is stable and no user input is given - in this case we recalculate the end date
                // note, that in the backward scheduling this will be a start date
                || (
                    !this.$.startDate.hasProposedValue() && !this.$.endDate.hasProposedValue() && !this.$.duration.hasProposedValue()
                    && this.$.startDate.hasConsistentValue() && this.$.endDate.hasConsistentValue() && this.$.duration.hasConsistentValue()
                )
            ) && (yield* this.canRecalculateEndDate())
        }


        @calculate('endDate')
        * calculateEndDate (proposedValue : Date) : ChronoIterator<Date | boolean> {
            const endDateInitial        = yield this.$.endDateInitial

            let result = yield* this.maybeSkipNonWorkingTime(endDateInitial, false)

            // This part is required to fix end date during data loading
            // TODO: This validation should be really moved to constructor
            if (this.$.startDateInitial.hasNextStableValue()) {
                const startDateInitial : Date = yield this.$.startDateInitial

                if (result instanceof Date && result < startDateInitial) {
                    result = startDateInitial
                }
            }

            return result
        }


        // Calculates the end date as start date plus duration
        // using availability provided by the event calendar
        * calculateProjectedEndDate (startDate : Date) : ChronoIterator<Date | any> {
            const calendar : CalendarMixin      = yield this.$.calendar,
                duration : Duration             = yield this.$.duration,
                durationUnit : TimeUnit         = yield this.$.durationUnit

            // Project calendar is used for units conversion
            const durationMS : Duration = yield* this.$convertDuration(duration, durationUnit, TimeUnit.Millisecond)

            return calendar.calculateEndDate(startDate, durationMS, TimeUnit.Millisecond)
        }
        //#endregion


        //#region Duration
        @calculate('duration')
        * calculateDuration (proposedValue? : Duration) : ChronoIterator<any | Duration> {
            if (proposedValue !== undefined) return proposedValue

            const shouldRecalculateDuration = yield* this.shouldRecalculateDuration()

            if (shouldRecalculateDuration) {
                return yield* this.doCalculateDuration()
            } else {
                // otherwise keep stable value
                return this.$.duration.getConsistentValue()
            }
        }

        // Calculates the event duration based on the event start/end dates.
        // The duration is calculated as amount of working time between start and end dates.
        * doCalculateDuration () : ChronoIterator<Duration> {
            const startDate     = yield this.$.startDate
            const endDate       = yield this.$.endDate

            return yield* this.calculateProjectedDuration(startDate, endDate)
        }


        // Calculates the duration in `durationUnit`-s between start and end dates
        // using availability provided by the event calendar
        * calculateProjectedDuration (startDate : Date, endDate : Date) : ChronoIterator<Duration | any> {
            if (!startDate || !endDate || startDate > endDate) return null

            const calendar : CalendarMixin      = yield this.$.calendar,
                durationUnit : TimeUnit         = yield this.$.durationUnit

            return yield* this.$convertDuration(calendar.calculateDuration(startDate, endDate, TimeUnit.Millisecond), TimeUnit.Millisecond, durationUnit)
        }


        // Duration can be calculated if there are both start and end date values
        * canRecalculateDuration () : ChronoIterator<boolean> {
            // not using `atom.hasValue()` method intentionally, because that includes `nextStableValue` check, which
            // appears during propagation and may affect this method
            return (this.$.startDate.hasProposedValue() || this.$.startDate.hasConsistentValue())
                && (this.$.endDate.hasProposedValue() || this.$.endDate.hasConsistentValue())
        }


        // Signalizes to recalculate duration if:
        // - the event has no duration value
        // - or start date is being changed by setStartDate() call and keepDuration is false
        // - or end date is being changed by setEndDate() call and keepDuration is false
        * shouldRecalculateDuration () : ChronoIterator<boolean> {
            return (
                (
                    !this.$.duration.hasConsistentValue()
                    // We don't recalculate duration if all the fields are just provided during initial data loading
                    && (
                        !this.$.startDate.getProposedValue() || !this.$.endDate.getProposedValue() || this.$.duration.getProposedValue() == null
                        || this.$.startDate.hasConsistentValue() || this.$.endDate.hasConsistentValue()
                    )
                )
                || (this.$.startDate.proposedArgs && this.$.startDate.proposedArgs[ 1 ] === false)
                || (this.$.endDate.proposedArgs && this.$.endDate.proposedArgs[ 1 ] === false)
            ) && (yield* this.canRecalculateDuration())
        }
        //#endregion


        getStartDate () : Date {
            return this.startDate
        }


        async setStartDate (date : Date, keepDuration : boolean = true) : Promise<PropagationResult> {
            // when user "unschedules" the event, we always want to drop the duration and keep the opposite date
            keepDuration    = date === null ? false : keepDuration

            this.$.startDate.put(date, keepDuration)
            // this is an "artefact" requirement from the past, where the proposed value was going to the "initial" atom
            this.markAsNeedRecalculation(this.$.startDateInitial)

            // need to explicitly mark end date (initial) as "recalculation needed"
            // because edges may be in "wrong" state, because previously, for example,
            // start date was updated based on end date + duration (event.setDuration(1, null, false)
            this.markAsNeedRecalculation(keepDuration ? this.$.endDateInitial : this.$.duration)

            return this.propagate()
        }


        getEndDate () : Date {
            return this.endDate
        }

        putEndDate (date : Date, keepDuration : boolean = false) {
            // when user "unschedules" the event, we always want to drop the duration and keep the opposite date
            keepDuration    = date === null ? false : keepDuration

            const startDate = this.getStartDate()

            if (!keepDuration && !!date && !!startDate && startDate.getTime() === date.getTime()) {
                this.putDuration(0)
            }
            // Actually change end date if we are:
            // 1. moving task by end date
            // 2. trying to null it
            // 3. there is no start date to validate against
            // 4. or end date is greater
            else if (keepDuration || !date || !startDate || startDate <= date) {
                this.$.endDate.put(date, keepDuration)

                // this is an "artifact" requirement from the past, where the proposed value was going to the "initial" atom
                this.markAsNeedRecalculation(this.$.endDateInitial)

                this.markAsNeedRecalculation(keepDuration ? this.$.startDateInitial : this.$.duration)
            }
        }

        async setEndDate (date : Date, keepDuration : boolean = false) : Promise<PropagationResult> {
            this.putEndDate(date, keepDuration)

            return this.propagate()
        }


        getDuration (unit? : TimeUnit) : Duration {
            let duration      = this.duration

            if (unit) {
                duration = this.convertDuration(duration, this.durationUnit, unit)
            }

            return duration
        }


        putDuration (duration : Duration, unit? : TimeUnit, keepStartDate : boolean = true) {
            const hasNoStartDate    = !this.$.startDate.hasProposedValue() && !this.$.startDate.hasConsistentValue()

            // never try to keep the start date if its absent
            // (we might be able to calculate it, instead of keeping)
            keepStartDate           = hasNoStartDate ? false : keepStartDate

            if (unit != null && unit !== this.durationUnit) {
                this.$.durationUnit.put(unit)
            }

            this.$.duration.put(duration, keepStartDate)

            this.markAsNeedRecalculation(keepStartDate ? this.$.endDateInitial : this.$.startDateInitial)
        }

        async setDuration (duration : Duration, unit? : TimeUnit, keepStartDate : boolean = true) : Promise<PropagationResult> {
            this.putDuration(duration, unit, keepStartDate)

            return this.propagate()
        }

        getDurationUnit () : TimeUnit {
            return this.durationUnit
        }


        setDurationUnit (_value : TimeUnit) {
            throw new Error("Use `setDuration` instead")
        }


        // should be overridden in the visualizing code
        toString () : string {
            return `Event ${this.id}`
        }


        setSchedulingMode : (mode : SchedulingMode) => Promise<PropagationResult>
    }

    return EventMixin
}

/**
 * Event entity mixin type
 *
 * At this level event is only aware about its calendar (which is inherited from project, if not provided).
 * The functionality, related to the dependencies, constraints etc is provided in other mixins.
 */
export interface EventMixin extends Mixin<typeof EventMixin> {}


/**
 * Event mixin type guard
 */
export const hasEventMixin = (model : any) : model is EventMixin => Boolean(model && model[hasMixin])
