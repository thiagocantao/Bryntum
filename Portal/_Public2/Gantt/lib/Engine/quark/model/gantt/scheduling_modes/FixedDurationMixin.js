import { HasProposedValue } from "../../../../../ChronoGraph/chrono/Effect.js";
import { Mixin } from "../../../../../ChronoGraph/class/BetterMixin.js";
import { Direction, SchedulingMode } from "../../../../scheduling/Types.js";
import { EffortVar, UnitsVar } from "../HasEffortDispatcher.js";
import { HasSchedulingModeMixin } from "../HasSchedulingModeMixin.js";
import { fixedDurationSEDWUBackwardEffortDriven, fixedDurationSEDWUBackwardNonEffortDriven, fixedDurationSEDWUForwardEffortDriven, fixedDurationSEDWUForwardNonEffortDriven } from "./FixedDurationDispatcher.js";
export class FixedDurationMixin extends Mixin([HasSchedulingModeMixin], (base) => {
    const superProto = base.prototype;
    class FixedDurationMixin extends base {
        *prepareDispatcher(YIELD) {
            const schedulingMode = yield* this.effectiveSchedulingMode();
            if (schedulingMode === SchedulingMode.FixedDuration) {
                const cycleDispatcher = yield* superProto.prepareDispatcher.call(this, YIELD);
                const effortDriven = yield this.$.effortDriven;
                if (effortDriven)
                    cycleDispatcher.addKeepIfPossibleFlag(EffortVar);
                if (yield HasProposedValue(this.$.assigned)) {
                    if (effortDriven) {
                        cycleDispatcher.addProposedValueFlag(EffortVar);
                    }
                    else {
                        cycleDispatcher.addProposedValueFlag(UnitsVar);
                    }
                }
                return cycleDispatcher;
            }
            else {
                return yield* superProto.prepareDispatcher.call(this, YIELD);
            }
        }
        cycleResolutionContext(Y) {
            const schedulingMode = this.effectiveSchedulingModeSync(Y);
            if (schedulingMode === SchedulingMode.FixedDuration) {
                const direction = Y(this.$.direction);
                const effortDriven = Y(this.$.effortDriven);
                if (direction === Direction.Forward || direction === Direction.None) {
                    return effortDriven ? fixedDurationSEDWUForwardEffortDriven : fixedDurationSEDWUForwardNonEffortDriven;
                }
                else {
                    return effortDriven ? fixedDurationSEDWUBackwardEffortDriven : fixedDurationSEDWUBackwardNonEffortDriven;
                }
            }
            else {
                return superProto.cycleResolutionContext.call(this, Y);
            }
        }
        *getBaseOptionsForDurationCalculations() {
            const schedulingMode = yield* this.effectiveSchedulingMode();
            if (schedulingMode === SchedulingMode.FixedDuration) {
                return { ignoreResourceCalendars: true };
            }
            else {
                return yield* superProto.getBaseOptionsForDurationCalculations.call(this);
            }
        }
    }
    return FixedDurationMixin;
}) {
}
