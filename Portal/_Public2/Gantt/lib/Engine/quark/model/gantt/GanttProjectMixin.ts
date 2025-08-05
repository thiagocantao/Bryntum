import { ProposedOrPrevious, UnsafeProposedOrPreviousValueOf } from "../../../../ChronoGraph/chrono/Effect.js"
import { AnyConstructor, Mixin } from '../../../../ChronoGraph/class/BetterMixin.js'
import { CalculationIterator } from "../../../../ChronoGraph/primitives/Calculation.js"
import { DateInterval } from '../../../scheduling/DateInterval.js'
import { Direction, ProjectType } from '../../../scheduling/Types.js'
import { MAX_DATE, MIN_DATE } from '../../../util/Constants.js'
import { ChronoAssignmentStoreMixin } from "../../store/ChronoAssignmentStoreMixin.js"
import { ChronoEventTreeStoreMixin } from "../../store/ChronoEventStoreMixin.js"
import { ChronoResourceStoreMixin } from "../../store/ChronoResourceStoreMixin.js"
import { HasChildrenMixin } from "../scheduler_basic/HasChildrenMixin.js"
import { GanttAssignmentMixin } from "./GanttAssignmentMixin.js"
import { SchedulerProProjectMixin } from "../scheduler_pro/SchedulerProProjectMixin.js"
import { SchedulerProResourceMixin } from "../scheduler_pro/SchedulerProResourceMixin.js"
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js"
import { GanttEvent } from './GanttEvent.js'
import { HasCriticalPathsMixin } from "./HasCriticalPathsMixin.js"
import { HasEffortMixin } from "./HasEffortMixin.js"


export type PartOfGanttProject = { project : GanttProjectMixin }

//---------------------------------------------------------------------------------------------------------------------
/**
 * Gantt project mixin type. At this level, events are called "tasks". All scheduling features from the [[SchedulerProProjectMixin]]
 * are preserved. Additionally, tasks inherit constraints from parent tasks. Tasks also receives the [[HasEffortMixin.effort|effort]] field
 * and [[HasSchedulingModeMixin.schedulingMode|schedulingMode]] field.
 *
 * The base event class for this level is [[GanttEvent]]. The base assignment class is [[GanttAssignmentMixin]].
 *
 * At this level, project can be scheduled in backward direction. This is controlled with the [[direction]] field.
 *
 * * Forward ASAP scheduling
 *
 * This is a default, most-used mode. In this mode, the "base" date is project start date. If it is not provided,
 * it is calculated as the earliest date of all project tasks. Events are scheduled ASAP, based on the "early" constraints
 * (plus "generic" constraints).
 *
 * * Forward ALAP scheduling
 *
 * In this mode, the "base" date is still project start date. If it is not provided,
 * it is calculated as the earliest date of all project tasks.
 *
 * Events are first scheduled ASAP, based on the "early" constraints. This gives the project end date.
 * Now events are scheduled ALAP, using the project end date as the base.
 *
 * The difference between the task position in Forward ASAP and Forward ALAP scheduling is called [[ConstrainedLateEventMixin.totalSlack|"slack"]]
 *
 * * Backward ALAP scheduling
 *
 * This is a "default" backward scheduling. In this mode, the "base" date is project end date. If it is not provided,
 * it is calculated as the latest date of all project tasks. Events are scheduled ALAP, based on the "late" constraints
 * (plus "generic" constraints).
 *
 * * Backward ASAP scheduling
 *
 * In this mode, the "base" date is still project end date. If it is not provided,
 * it is calculated as the latest date of all project tasks.
 *
 * Events are first scheduled ALAP, based on the "late" constraints. This gives the project start date.
 * Now events are scheduled ASAP, using the project start date as the base.
 *
 */
export class GanttProjectMixin extends Mixin(
    [
        SchedulerProProjectMixin,
        HasEffortMixin,
        ConstrainedLateEventMixin,
        HasCriticalPathsMixin
    ],
    (base : AnyConstructor<
        SchedulerProProjectMixin &
        HasEffortMixin &
        ConstrainedLateEventMixin &
        HasCriticalPathsMixin
        ,
        typeof SchedulerProProjectMixin &
        typeof HasEffortMixin &
        typeof ConstrainedLateEventMixin &
        typeof HasCriticalPathsMixin
    >) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class GanttProjectMixin extends base {

        eventModelClass             : typeof GanttEvent
        assignmentModelClass        : typeof GanttAssignmentMixin
        resourceModelClass          : typeof SchedulerProResourceMixin

        eventStoreClass             : typeof ChronoEventTreeStoreMixin

        eventStore                  : ChronoEventTreeStoreMixin & PartOfGanttProject
        assignmentStore             : ChronoAssignmentStoreMixin & PartOfGanttProject
        resourceStore               : ChronoResourceStoreMixin & PartOfGanttProject


        * calculateDirection () : CalculationIterator<Direction> {
            return yield ProposedOrPrevious
        }


        * calculateStartDate () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Forward) {
                let result : Date   =  yield ProposedOrPrevious

                if (!result) {
                    result  = yield* this.unsafeCalculateInitialMinChildrenStartDateDeep()
                }

                return result
            }
            else if (direction === Direction.Backward) {
                return yield* this.calculateMinChildrenStartDate()
            }
        }


        * calculateEndDate () : CalculationIterator<Date> {
            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Forward) {
                return yield* this.calculateMaxChildrenEndDate()
            }
            else if (direction === Direction.Backward) {
                let result : Date   =  yield ProposedOrPrevious

                if (!result) {
                    result  = yield* this.unsafeCalculateInitialMaxChildrenEndDateDeep()
                }

                return result
            }
        }


        * calculateEarlyStartDateConstraintIntervals () : CalculationIterator<DateInterval[]> {
            const intervals : DateInterval[]    = yield* superProto.calculateEarlyStartDateConstraintIntervals.call(this)

            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Forward) {
                const startDate : Date              = yield this.$.startDate

                startDate && intervals.push(DateInterval.new({ startDate }))
            }
            else if (direction === Direction.Backward) {
                const startDate : Date              = yield this.$.lateStartDate

                startDate && intervals.push(DateInterval.new({ startDate }))
            }

            return intervals
        }


        * calculateLateEndDateConstraintIntervals () : CalculationIterator<DateInterval[]> {
            const intervals : DateInterval[]    = yield* superProto.calculateLateEndDateConstraintIntervals.call(this)

            const direction : Direction     = yield this.$.direction

            if (direction === Direction.Forward) {
                const endDate : Date                = yield this.$.earlyEndDate

                endDate && intervals.push(DateInterval.new({ endDate }))
            }
            else if (direction === Direction.Backward) {
                const endDate : Date              = yield this.$.endDate

                endDate && intervals.push(DateInterval.new({ endDate }))
            }

            return intervals
        }


        getDefaultEventModelClass () : this[ 'eventModelClass' ] {
            return GanttEvent
        }


        getDefaultAssignmentModelClass () : this[ 'assignmentModelClass' ] {
            return GanttAssignmentMixin
        }


        getDefaultResourceModelClass () : this[ 'resourceModelClass' ] {
            return SchedulerProResourceMixin
        }


        getDefaultEventStoreClass () : this[ 'eventStoreClass' ] {
            return ChronoEventTreeStoreMixin
        }


        getType () : ProjectType {
            return ProjectType.Gantt
        }


        // this method is only used to calculated "initial" project start date only
        * unsafeCalculateInitialMinChildrenStartDateDeep () : CalculationIterator<Date> {
            const childEvents : Set<HasChildrenMixin>    = yield this.$.childEvents

            // note, that we does not yield here, as we want to calculate "initial" project start date
            // which will be used only if there's no user input or explicit setting for it
            // such project date should be calculated as earliest date of all tasks, based on the
            // "initial" data (which includes proposed)
            if (!childEvents.size) return yield UnsafeProposedOrPreviousValueOf(this.$.startDate)

            let result : Date       = MAX_DATE,
                child : HasChildrenMixin

            const toProcess : HasChildrenMixin[]        = [...childEvents]

            while ((child = toProcess.shift())) {
                const childDate     = yield UnsafeProposedOrPreviousValueOf(child.$.startDate)

                if (childDate && childDate < result) result = childDate

                toProcess.push(...yield child.$.childEvents)
            }

            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null
        }


        * unsafeCalculateInitialMaxChildrenEndDateDeep () : CalculationIterator<Date> {
            const childEvents : Set<HasChildrenMixin>    = yield this.$.childEvents

            // note, that we use "unsafe" ProposedOrPrevios effect here, because we only get into this method
            // if there's no user input for the project end date
            if (!childEvents.size) return yield UnsafeProposedOrPreviousValueOf(this.$.endDate)

            let result : Date       = MIN_DATE,
                child : HasChildrenMixin

            const toProcess : HasChildrenMixin[]        = [...childEvents]

            while ((child = toProcess.shift())) {
                const childDate     = yield UnsafeProposedOrPreviousValueOf(child.$.endDate)

                if (childDate && childDate > result) result = childDate

                toProcess.push(...yield child.$.childEvents)
            }

            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null
        }
    }

    return GanttProjectMixin
}) {}
