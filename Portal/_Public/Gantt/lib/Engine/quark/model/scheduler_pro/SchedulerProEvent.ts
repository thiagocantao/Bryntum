import { AnyConstructor, Mixin } from "../../../../ChronoGraph/class/BetterMixin.js"
import { SchedulerBasicEvent } from "../scheduler_basic/SchedulerBasicEvent.js"
import { ConstrainedEarlyEventMixin } from "./ConstrainedEarlyEventMixin.js"
import { HasDateConstraintMixin } from "./HasDateConstraintMixin.js"
import { HasPercentDoneMixin } from "./HasPercentDoneMixin.js"
import { ScheduledByDependenciesEarlyEventMixin } from "./ScheduledByDependenciesEarlyEventMixin.js"
import { SchedulerProHasAssignmentsMixin } from "./SchedulerProHasAssignmentsMixin.js"
// https://github.com/bryntum/support/issues/6397
import { SplitEventMixin } from "./SplitEventMixin.js"
import { HasEffortMixin } from "./HasEffortMixin.js"
import { HasSchedulingModeMixin } from "./HasSchedulingModeMixin.js"
import { FixedDurationMixin } from "./scheduling_modes/FixedDurationMixin.js"

// import { ConstrainedByParentMixin } from "../gantt/ConstrainedByParentMixin.js"


/**
 * This is an event class, [[SchedulerProProjectMixin]] is working with.
 * It is constructed as [[SchedulerBasicEvent]], enhanced with extra functionality.
 */
export class SchedulerProEvent extends Mixin(
    [
        SchedulerBasicEvent,
        HasDateConstraintMixin,
        HasPercentDoneMixin,
        SchedulerProHasAssignmentsMixin,
        HasEffortMixin,
        HasSchedulingModeMixin,
        FixedDurationMixin,
        ConstrainedEarlyEventMixin,
        ScheduledByDependenciesEarlyEventMixin,
        SplitEventMixin,
        // ConstrainedByParentMixin
    ],
    (base : AnyConstructor<
        SchedulerBasicEvent
        & HasDateConstraintMixin
        & HasPercentDoneMixin
        & SchedulerProHasAssignmentsMixin
        & HasEffortMixin
        & HasSchedulingModeMixin
        & FixedDurationMixin
        & ConstrainedEarlyEventMixin
        & ScheduledByDependenciesEarlyEventMixin
        & SplitEventMixin
        // & ConstrainedByParentMixin
        ,
        typeof SchedulerBasicEvent
        & typeof HasDateConstraintMixin
        & typeof HasPercentDoneMixin
        & typeof SchedulerProHasAssignmentsMixin
        & typeof HasEffortMixin
        & typeof HasSchedulingModeMixin
        & typeof FixedDurationMixin
        & typeof ConstrainedEarlyEventMixin
        & typeof ScheduledByDependenciesEarlyEventMixin
        & typeof SplitEventMixin
        // & typeof ConstrainedByParentMixin
    >) => {

    class SchedulerProEvent extends base {
        // this seems to cause compilation error in incremental mode (IDE)
        // regular compilation does not produce errors
        // project         : SchedulerProProjectMixin

        static get fields () {
            return [
                { name : 'name', type : 'string' }
            ]
        }
    }

    return SchedulerProEvent
}){}
