import { ProposedOrPrevious, UnsafeProposedOrPreviousValueOf } from "../../../../ChronoGraph/chrono/Effect.js";
import { Mixin } from '../../../../ChronoGraph/class/BetterMixin.js';
import { DateInterval } from '../../../scheduling/DateInterval.js';
import { Direction, ProjectType } from '../../../scheduling/Types.js';
import { MAX_DATE, MIN_DATE } from '../../../util/Constants.js';
import { ChronoEventTreeStoreMixin } from "../../store/ChronoEventStoreMixin.js";
import { GanttAssignmentMixin } from "./GanttAssignmentMixin.js";
import { SchedulerProProjectMixin } from "../scheduler_pro/SchedulerProProjectMixin.js";
import { SchedulerProResourceMixin } from "../scheduler_pro/SchedulerProResourceMixin.js";
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js";
import { GanttEvent } from './GanttEvent.js';
import { HasCriticalPathsMixin } from "./HasCriticalPathsMixin.js";
import { HasEffortMixin } from "./HasEffortMixin.js";
export class GanttProjectMixin extends Mixin([
    SchedulerProProjectMixin,
    HasEffortMixin,
    ConstrainedLateEventMixin,
    HasCriticalPathsMixin
], (base) => {
    const superProto = base.prototype;
    class GanttProjectMixin extends base {
        *calculateDirection() {
            return yield ProposedOrPrevious;
        }
        *calculateStartDate() {
            const direction = yield this.$.direction;
            if (direction === Direction.Forward) {
                let result = yield ProposedOrPrevious;
                if (!result) {
                    result = yield* this.unsafeCalculateInitialMinChildrenStartDateDeep();
                }
                return result;
            }
            else if (direction === Direction.Backward) {
                return yield* this.calculateMinChildrenStartDate();
            }
        }
        *calculateEndDate() {
            const direction = yield this.$.direction;
            if (direction === Direction.Forward) {
                return yield* this.calculateMaxChildrenEndDate();
            }
            else if (direction === Direction.Backward) {
                let result = yield ProposedOrPrevious;
                if (!result) {
                    result = yield* this.unsafeCalculateInitialMaxChildrenEndDateDeep();
                }
                return result;
            }
        }
        *calculateEarlyStartDateConstraintIntervals() {
            const intervals = yield* superProto.calculateEarlyStartDateConstraintIntervals.call(this);
            const direction = yield this.$.direction;
            if (direction === Direction.Forward) {
                const startDate = yield this.$.startDate;
                startDate && intervals.push(DateInterval.new({ startDate }));
            }
            else if (direction === Direction.Backward) {
                const startDate = yield this.$.lateStartDate;
                startDate && intervals.push(DateInterval.new({ startDate }));
            }
            return intervals;
        }
        *calculateLateEndDateConstraintIntervals() {
            const intervals = yield* superProto.calculateLateEndDateConstraintIntervals.call(this);
            const direction = yield this.$.direction;
            if (direction === Direction.Forward) {
                const endDate = yield this.$.earlyEndDate;
                endDate && intervals.push(DateInterval.new({ endDate }));
            }
            else if (direction === Direction.Backward) {
                const endDate = yield this.$.endDate;
                endDate && intervals.push(DateInterval.new({ endDate }));
            }
            return intervals;
        }
        getDefaultEventModelClass() {
            return GanttEvent;
        }
        getDefaultAssignmentModelClass() {
            return GanttAssignmentMixin;
        }
        getDefaultResourceModelClass() {
            return SchedulerProResourceMixin;
        }
        getDefaultEventStoreClass() {
            return ChronoEventTreeStoreMixin;
        }
        getType() {
            return ProjectType.Gantt;
        }
        *unsafeCalculateInitialMinChildrenStartDateDeep() {
            const childEvents = yield this.$.childEvents;
            if (!childEvents.size)
                return yield UnsafeProposedOrPreviousValueOf(this.$.startDate);
            let result = MAX_DATE, child;
            const toProcess = [...childEvents];
            while ((child = toProcess.shift())) {
                const childDate = yield UnsafeProposedOrPreviousValueOf(child.$.startDate);
                if (childDate && childDate < result)
                    result = childDate;
                toProcess.push(...yield child.$.childEvents);
            }
            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null;
        }
        *unsafeCalculateInitialMaxChildrenEndDateDeep() {
            const childEvents = yield this.$.childEvents;
            if (!childEvents.size)
                return yield UnsafeProposedOrPreviousValueOf(this.$.endDate);
            let result = MIN_DATE, child;
            const toProcess = [...childEvents];
            while ((child = toProcess.shift())) {
                const childDate = yield UnsafeProposedOrPreviousValueOf(child.$.endDate);
                if (childDate && childDate > result)
                    result = childDate;
                toProcess.push(...yield child.$.childEvents);
            }
            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null;
        }
    }
    return GanttProjectMixin;
}) {
}
