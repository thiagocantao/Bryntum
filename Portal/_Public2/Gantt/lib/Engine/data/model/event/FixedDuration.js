import { SchedulingMode } from "../../../scheduling/Types.js";
export const FixedDuration = (base) => {
    class FixedDuration extends base {
        *shouldRecalculateDuration() {
            const schedulingMode = yield this.$.schedulingMode;
            const assignments = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedDuration && assignments.size) {
                if (this.$.duration.value == null)
                    return true;
                return false;
            }
            return yield* super.shouldRecalculateDuration();
        }
        *shouldRecalculateEffort() {
            const schedulingMode = yield this.$.schedulingMode;
            const assignments = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedDuration && assignments.size) {
                if (this.$.effort.value == null)
                    return true;
                return !(yield* this.shouldRecalculateDuration()) && !(yield* this.shouldRecalculateAssignmentUnits(null));
            }
            return yield* super.shouldRecalculateEffort();
        }
        *shouldRecalculateAssignmentUnits(assignment) {
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedDuration) {
                if (yield this.$.effortDriven) {
                    if (this.$.effort.hasProposedValue() || this.$.duration.hasProposedValue()) {
                        return true;
                    }
                    if ((this.$.assigned.newRefs.size > 0 || this.$.assigned.oldRefs.size > 0)
                        &&
                            this.$.effort.value != null) {
                        return true;
                    }
                    return false;
                }
                else {
                    return this.$.effort.hasProposedValue();
                }
            }
            return yield* super.shouldRecalculateAssignmentUnits(assignment);
        }
        async setEffort(effort, unit) {
            const schedulingMode = this.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedDuration) {
                this.assigned.forEach((assignment) => {
                    this.markAsNeedRecalculation(assignment.$.units);
                });
            }
            return super.setEffort(effort, unit);
        }
        async setDuration(duration, unit) {
            const schedulingMode = this.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedDuration && this.effortDriven) {
                this.assigned.forEach((assignment) => {
                    this.markAsNeedRecalculation(assignment.$.units);
                });
            }
            return super.setDuration(duration, unit);
        }
        async setAssignmentUnits(assignment, units) {
            if (this.schedulingMode === SchedulingMode.FixedDuration) {
                assignment.$.units.put(units);
                assignment.markAsNeedRecalculation(assignment.event.$.effort);
                return assignment.propagate();
            }
            else
                return super.setAssignmentUnits(assignment, units);
        }
        addAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedDuration && this.effortDriven) {
                // `clearUserInput` basically means we need to ignore the user-provided value for the assignment unit
                // and calculate it, based on other information
                // (currently `calculateAssignmentUnits` in the HasAssignments always uses user-provided value if it exists)
                // ideally, we need to detect this case in the `calculateAssignmentUnits` and ignore the `proposedValue`
                assignment.$.units.clearUserInput();
                this.assigned.forEach((assignment) => {
                    this.markAsNeedRecalculation(assignment.$.units);
                    assignment.$.units.clearUserInput();
                });
            }
            return super.addAssignment(assignment);
        }
        removeAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedDuration && this.effortDriven) {
                // `clearUserInput` basically means we need to ignore the user-provided value for the assignment unit
                // and calculate it, based on other information
                // (currently `calculateAssignmentUnits` in the HasAssignments always uses user-provided value if it exists)
                // ideally, we need to detect this case in the `calculateAssignmentUnits` and ignore the `proposedValue`
                assignment.$.units.clearUserInput();
                this.assigned.forEach((assignment) => {
                    this.markAsNeedRecalculation(assignment.$.units);
                    assignment.$.units.clearUserInput();
                });
            }
            return super.removeAssignment(assignment);
        }
        *useDurationForProjectedXDateCalculation() {
            const schedulingMode = yield this.$.schedulingMode;
            return (schedulingMode === SchedulingMode.FixedDuration) || (yield* super.useDurationForProjectedXDateCalculation());
        }
        *getBaseOptionsForDurationCalculations() {
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.FixedDuration) {
                return { ignoreResourceCalendars: true };
            }
            else {
                return yield* super.getBaseOptionsForDurationCalculations();
            }
        }
    }
    return FixedDuration;
};
