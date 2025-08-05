import { SchedulingMode } from "../../../scheduling/Types.js";
export const FixedEffort = (base) => {
    class FixedEffort extends base {
        // FixedEffort scheduling mode forces the task to be effort driven
        *calculateEffortDriven(proposedValue) {
            const schedulingMode = yield this.$.schedulingMode;
            return (schedulingMode === SchedulingMode.FixedEffort) || (yield* super.calculateEffortDriven(proposedValue));
        }
        *shouldRecalculateDuration() {
            const schedulingMode = yield this.$.schedulingMode;
            const assignments = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedEffort && assignments.size > 0) {
                return !(yield* this.shouldRecalculateEffort());
            }
            else
                return yield* super.shouldRecalculateDuration();
        }
        *shouldRecalculateEffort() {
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedEffort) {
                return this.$.effort.value == null;
            }
            else
                return yield* super.shouldRecalculateEffort();
        }
        async setAssignmentUnits(assignment, units) {
            if (this.schedulingMode === SchedulingMode.FixedEffort) {
                assignment.$.units.put(units);
                assignment.markAsNeedRecalculation(assignment.event.$.duration);
                return assignment.propagate();
            }
            else
                return super.setAssignmentUnits(assignment, units);
        }
        *shouldRecalculateAssignmentUnits(assignment) {
            const schedulingMode = this.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedEffort) {
                if (this.$.duration.hasProposedValue())
                    return true;
                else
                    return false;
            }
            else {
                return yield* super.shouldRecalculateAssignmentUnits(assignment);
            }
        }
        async setDuration(duration, unit, ...args) {
            const schedulingMode = this.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedEffort) {
                this.assigned.forEach((assignment) => {
                    this.markAsNeedRecalculation(assignment.$.units);
                    assignment.$.units.clearUserInput();
                });
            }
            return super.setDuration(duration, unit, ...args);
        }
        addAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedEffort) {
                this.markAsNeedRecalculation(this.$.duration);
            }
            return super.addAssignment(assignment);
        }
        removeAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedEffort) {
                if (this.assigned.size > 1)
                    this.markAsNeedRecalculation(this.$.duration);
            }
            return super.removeAssignment(assignment);
        }
    }
    return FixedEffort;
};
