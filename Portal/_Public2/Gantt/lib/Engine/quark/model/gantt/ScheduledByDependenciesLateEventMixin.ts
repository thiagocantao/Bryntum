import { AnyConstructor, Mixin } from '../../../../ChronoGraph/class/BetterMixin.js'
import { CalculationIterator } from "../../../../ChronoGraph/primitives/Calculation.js"
import { calculate } from '../../../../ChronoGraph/replica/Entity.js'
import { isAtomicValue } from '../../../../ChronoGraph/util/Helpers.js'
import { DateInterval } from '../../../scheduling/DateInterval.js'
import { DependencyType, TimeUnit } from '../../../scheduling/Types.js'
import { BaseCalendarMixin } from '../scheduler_basic/BaseCalendarMixin.js'
import { ScheduledByDependenciesEarlyEventMixin } from "../scheduler_pro/ScheduledByDependenciesEarlyEventMixin.js"
import { SchedulerProDependencyMixin } from "../scheduler_pro/SchedulerProDependencyMixin.js"
import { SchedulerProProjectMixin } from "../scheduler_pro/SchedulerProProjectMixin.js"
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js"


//---------------------------------------------------------------------------------------------------------------------
/**
 * This mixin adds support for scheduling event ALAP, by dependencies. All it does is
 * create the "late" constraint interval for every outgoing dependency.
 *
 * See [[ConstrainedEarlyEventMixin]] for more details about constraint-based scheduling.
 * See also [[ScheduledByDependenciesEarlyEventMixin]].
 */
export class ScheduledByDependenciesLateEventMixin extends Mixin(
    [ ScheduledByDependenciesEarlyEventMixin, ConstrainedLateEventMixin ],
    (base : AnyConstructor<ScheduledByDependenciesEarlyEventMixin & ConstrainedLateEventMixin, typeof ScheduledByDependenciesEarlyEventMixin & typeof ConstrainedLateEventMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class ScheduledByDependenciesLateEventMixin extends base {

        project             : SchedulerProProjectMixin


        @calculate('lateStartDateIntervals')
        * calculateLateStartDateConstraintIntervals () : CalculationIterator<DateInterval[]> {
            const intervals : DateInterval[] = yield* superProto.calculateLateStartDateConstraintIntervals.call(this)

            const project : SchedulerProProjectMixin   = this.getProject()

            let dependency : SchedulerProDependencyMixin

            for (dependency of (yield this.$.outgoingDeps)) {
                const successor : ScheduledByDependenciesLateEventMixin = yield dependency.$.toEvent

                // ignore missing from events
                if (successor == null || isAtomicValue(successor)) continue

                let interval : DateInterval

                switch (yield dependency.$.type) {
                    case DependencyType.StartToStart:
                        const successorStartDate : Date = yield successor.$.lateStartDateRaw

                        if (successorStartDate) {
                            const lag : number             = yield dependency.$.lag
                            const lagUnit : TimeUnit       = yield dependency.$.lagUnit
                            const calendar : BaseCalendarMixin = yield dependency.$.calendar

                            interval = DateInterval.new({
                                startDate   : null,
                                endDate     : calendar.calculateStartDate(
                                    successorStartDate,
                                    yield* project.$convertDuration(lag, lagUnit, TimeUnit.Millisecond)
                                ),
                            })
                        }
                        break

                    case DependencyType.StartToEnd :
                        const successorEndDate : Date = yield successor.$.lateEndDateRaw

                        if (successorEndDate) {
                            const lag : number             = yield dependency.$.lag
                            const lagUnit : TimeUnit       = yield dependency.$.lagUnit
                            const calendar : BaseCalendarMixin = yield dependency.$.calendar

                            interval = DateInterval.new({
                                startDate   : null,
                                endDate     : calendar.calculateStartDate(
                                    successorEndDate,
                                    yield* project.$convertDuration(lag, lagUnit, TimeUnit.Millisecond)
                                ),
                            })
                        }
                        break
                }

                interval && intervals.unshift(interval)
            }

            return intervals
        }


        @calculate('lateEndDateIntervals')
        * calculateLateEndDateConstraintIntervals () : CalculationIterator<DateInterval[]> {
            const intervals : DateInterval[] = yield* superProto.calculateLateEndDateConstraintIntervals.call(this)

            const project : SchedulerProProjectMixin   = this.getProject()

            let dependency : SchedulerProDependencyMixin

            for (dependency of (yield this.$.outgoingDeps)) {
                const successor : ScheduledByDependenciesLateEventMixin = yield dependency.$.toEvent

                // ignore missing from events
                if (successor == null || isAtomicValue(successor)) continue

                let interval : DateInterval

                switch (yield dependency.$.type) {
                    case DependencyType.EndToEnd:
                        const successorEndDate : Date = yield successor.$.lateEndDateRaw

                        if (successorEndDate) {
                            const lag : number             = yield dependency.$.lag
                            const lagUnit : TimeUnit       = yield dependency.$.lagUnit
                            const calendar : BaseCalendarMixin = yield dependency.$.calendar

                            interval = DateInterval.new({
                                startDate           : null,
                                endDate             : calendar.calculateStartDate(
                                    successorEndDate,
                                    yield* project.$convertDuration(lag, lagUnit, TimeUnit.Millisecond)
                                ),
                            })
                        }
                        break

                    case DependencyType.EndToStart:
                        const successorStartDate : Date = yield successor.$.lateStartDateRaw

                        if (successorStartDate) {
                            const lag : number             = yield dependency.$.lag
                            const lagUnit : TimeUnit       = yield dependency.$.lagUnit
                            const calendar : BaseCalendarMixin = yield dependency.$.calendar

                            interval = DateInterval.new({
                                startDate           : null,
                                endDate             : calendar.calculateStartDate(
                                    successorStartDate,
                                    yield* project.$convertDuration(lag, lagUnit, TimeUnit.Millisecond)
                                ),
                            })
                        }
                        break
                }

                interval && intervals.unshift(interval)
            }

            return intervals
        }
    }

    return ScheduledByDependenciesLateEventMixin
}){}
