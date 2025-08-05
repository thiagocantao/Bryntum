import Store from "../../../Common/data/Store.js";
import { BryntumEvent } from "../model/event/BryntumEvent.js";
import { PartOfProjectGenericMixin } from "../PartOfProjectGenericMixin.js";
import { ChronoStoreMixin } from "./mixin/ChronoStoreMixin.js";
import { PartOfProjectStoreMixin } from "./mixin/PartOfProjectMixin.js";
export const EventStoreMixin = (base) => {
    class EventStoreMixin extends base {
        static get defaultConfig() {
            return {
                tree: true,
                modelClass: BryntumEvent
            };
        }
        buildRootNode() {
            return this.getProject() || {};
        }
    }
    return EventStoreMixin;
};
//export type EventStoreMixin = Mixin<typeof EventStoreMixin>
export const BuildMinimalEventStore = (base = Store) => EventStoreMixin(PartOfProjectStoreMixin(PartOfProjectGenericMixin(ChronoStoreMixin(base))));
export class MinimalEventStore extends BuildMinimalEventStore() {
}
