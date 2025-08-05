import { Mixin } from "../../../ChronoGraph/class/BetterMixin.js";
import { BaseAssignmentMixin } from "../model/scheduler_basic/BaseAssignmentMixin.js";
import { ChronoPartOfProjectStoreMixin } from "./mixin/ChronoPartOfProjectStoreMixin.js";
import { AbstractAssignmentStoreMixin } from "./AbstractAssignmentStoreMixin.js";
/**
 * A store mixin class, that represent collection of all assignments in the [[SchedulerBasicProjectMixin|project]].
 */
export class ChronoAssignmentStoreMixin extends Mixin([AbstractAssignmentStoreMixin, ChronoPartOfProjectStoreMixin], (base) => {
    const superProto = base.prototype;
    class ChronoAssignmentStoreMixin extends base {
        constructor() {
            super(...arguments);
            this.removalOrder = 100;
        }
        static get defaultConfig() {
            return {
                modelClass: BaseAssignmentMixin
            };
        }
        set data(value) {
            this.allAssignmentsForRemoval = true;
            super.data = value;
            this.allAssignmentsForRemoval = false;
        }
    }
    return ChronoAssignmentStoreMixin;
}) {
}
