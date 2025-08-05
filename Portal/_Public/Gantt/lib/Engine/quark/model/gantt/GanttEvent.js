import { MixinAny } from "../../../../ChronoGraph/class/BetterMixin.js";
import { SchedulerProEvent } from "../scheduler_pro/SchedulerProEvent.js";
import { ConstrainedByParentMixin } from "./ConstrainedByParentMixin.js";
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js";
import { ScheduledByDependenciesLateEventMixin } from "./ScheduledByDependenciesLateEventMixin.js";
import { FixedEffortMixin } from "./scheduling_modes/FixedEffortMixin.js";
import { FixedUnitsMixin } from "./scheduling_modes/FixedUnitsMixin.js";
import { InactiveEventMixin } from "./InactiveEventMixin.js";
/**
 * This is an event class, [[GanttProjectMixin]] is working with.
 * It is constructed as [[SchedulerProEvent]], enhanced with extra functionality.
 */
export class GanttEvent extends MixinAny([
    SchedulerProEvent,
    ConstrainedByParentMixin,
    ConstrainedLateEventMixin,
    ScheduledByDependenciesLateEventMixin,
    FixedEffortMixin,
    FixedUnitsMixin,
    InactiveEventMixin
], (base) => {
    class GanttEvent extends base {
    }
    return GanttEvent;
}) {
}
