import { AnyConstructor, MixinAny } from "../../../../ChronoGraph/class/BetterMixin.js"
import { SchedulerProEvent } from "../scheduler_pro/SchedulerProEvent.js"
import { ConstrainedByParentMixin } from "./ConstrainedByParentMixin.js"
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js"
import { GanttProjectMixin } from "./GanttProjectMixin.js"
import { ScheduledByDependenciesLateEventMixin } from "./ScheduledByDependenciesLateEventMixin.js"
import { FixedEffortMixin } from "./scheduling_modes/FixedEffortMixin.js"
import { FixedUnitsMixin } from "./scheduling_modes/FixedUnitsMixin.js"
import { InactiveEventMixin } from "./InactiveEventMixin.js"


/**
 * This is an event class, [[GanttProjectMixin]] is working with.
 * It is constructed as [[SchedulerProEvent]], enhanced with extra functionality.
 */
export class GanttEvent extends MixinAny(
    [
        SchedulerProEvent,
        ConstrainedByParentMixin,
        ConstrainedLateEventMixin,
        ScheduledByDependenciesLateEventMixin,
        FixedEffortMixin,
        FixedUnitsMixin,
        InactiveEventMixin
    ],
    (base : AnyConstructor<
        SchedulerProEvent
        & ConstrainedByParentMixin
        & ConstrainedLateEventMixin
        & ScheduledByDependenciesLateEventMixin
        & FixedEffortMixin
        & FixedUnitsMixin
        & InactiveEventMixin,
        typeof SchedulerProEvent
        & typeof ConstrainedByParentMixin
        & typeof ConstrainedLateEventMixin
        & typeof ScheduledByDependenciesLateEventMixin
        & typeof FixedEffortMixin
        & typeof FixedUnitsMixin
        & typeof InactiveEventMixin
    >) => {

    class GanttEvent extends base {
        // surprisingly this seems to be fine (see the comment in the SchedulerProEvent)
        project         : GanttProjectMixin
    }

    return GanttEvent
}){}

