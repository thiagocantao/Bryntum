import { Mixin } from "../../../ChronoGraph/class/BetterMixin.js";
import { SchedulerBasicEvent } from "../model/scheduler_basic/SchedulerBasicEvent.js";
import { ChronoPartOfProjectStoreMixin } from "./mixin/ChronoPartOfProjectStoreMixin.js";
import { AbstractEventStoreMixin } from "./AbstractEventStoreMixin.js";
/**
 * A store mixin class, that represent collection of all events in the [[SchedulerBasicProjectMixin|project]].
 */
export class ChronoEventStoreMixin extends Mixin([AbstractEventStoreMixin, ChronoPartOfProjectStoreMixin], (base) => {
    class ChronoEventStoreMixin extends base {
        constructor() {
            super(...arguments);
            this.removalOrder = 400;
        }
        static get defaultConfig() {
            return {
                modelClass: SchedulerBasicEvent
            };
        }
        // this method has to be in the code for the "plain" store, because it might be
        // suddenly upgraded to the "tree", based on the data
        buildRootNode() {
            return this.getProject() || {};
        }
        set data(value) {
            super.data = value;
            this.afterEventRemoval();
        }
    }
    return ChronoEventStoreMixin;
}) {
}
/**
 * The tree store version of [[ChronoEventStoreMixin]].
 */
export class ChronoEventTreeStoreMixin extends Mixin([ChronoEventStoreMixin], (base) => {
    class ChronoEventTreeStoreMixin extends base {
        static get defaultConfig() {
            return {
                tree: true
            };
        }
    }
    return ChronoEventTreeStoreMixin;
}) {
}
