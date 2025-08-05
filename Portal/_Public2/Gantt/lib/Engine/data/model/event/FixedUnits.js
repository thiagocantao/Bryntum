import { SchedulingMode } from "../../../scheduling/Types.js";
export const FixedUnits = (base) => {
    class FixedUnits extends base {
        *shouldRecalculateDuration() {
            const schedulingMode = yield this.$.schedulingMode;
            const assigned = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedUnits && assigned.size > 0) {
                if (this.$.duration.value == null)
                    return true;
                if (this.$.effort.hasProposedValue())
                    return true;
                return !(yield* this.shouldRecalculateEffort());
            }
            return yield* super.shouldRecalculateDuration();
        }
        *shouldRecalculateEffort() {
            const schedulingMode = yield this.$.schedulingMode;
            const assigned = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedUnits && assigned.size > 0) {
                if (this.$.effort.value == null && (yield* this.canRecalculateEffort()))
                    return true;
                if (this.$.duration.hasProposedValue())
                    return true;
                return !(yield this.$.effortDriven);
            }
            return yield* super.shouldRecalculateEffort();
        }
        addAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedUnits) {
                this.markAsNeedRecalculation(this.effortDriven ? this.$.duration : this.$.effort);
            }
            return super.addAssignment(assignment);
        }
        removeAssignment(assignment) {
            if (this.schedulingMode === SchedulingMode.FixedUnits) {
                this.markAsNeedRecalculation(this.effortDriven ? this.$.duration : this.$.effort);
            }
            return super.removeAssignment(assignment);
        }
        async setEffort(duration, unit) {
            if (this.schedulingMode === SchedulingMode.FixedUnits) {
                this.markAsNeedRecalculation(this.$.duration);
            }
            return super.setEffort(duration, unit);
        }
        async setDuration(duration, unit, ...args) {
            if (this.schedulingMode === SchedulingMode.FixedUnits) {
                this.markAsNeedRecalculation(this.$.effort);
            }
            return super.setDuration(duration, unit, ...args);
        }
        *useDurationForProjectedXDateCalculation() {
            const schedulingMode = yield this.$.schedulingMode;
            const assigned = yield this.$.assigned;
            if (schedulingMode === SchedulingMode.FixedUnits && assigned.size > 0) {
                const effortDriven = yield this.$.effortDriven;
                // if user there's a user input for duration - we should calculate based on that
                return (!effortDriven || !this.$.effort.hasValue() || this.$.duration.hasProposedValue())
                    // this means:
                    // 1) this is not a initial data calculation (hasConsistentValue)
                    // 2) there's a user input for duration (hasProposedValue)
                    && (!this.$.effort.hasProposedValue() || !this.$.effort.hasConsistentValue());
            }
            return yield* super.useDurationForProjectedXDateCalculation();
        }
    }
    return FixedUnits;
};
