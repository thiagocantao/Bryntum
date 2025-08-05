import Store from "../../../Common/data/Store.js";
import { MinimalResource } from "../model/ResourceMixin.js";
import { PartOfProjectGenericMixin } from "../PartOfProjectGenericMixin.js";
import { ChronoStoreMixin } from "./mixin/ChronoStoreMixin.js";
import { PartOfProjectStoreMixin } from "./mixin/PartOfProjectMixin.js";
export const ResourceStoreMixin = (base) => {
    class ResourceStoreMixin extends base {
        static get defaultConfig() {
            return {
                modelClass: MinimalResource
            };
        }
    }
    return ResourceStoreMixin;
};
export const BuildMinimalResourceStore = (base = Store) => ResourceStoreMixin(PartOfProjectStoreMixin(PartOfProjectGenericMixin(ChronoStoreMixin(base))));
export class MinimalResourceStore extends BuildMinimalResourceStore() {
}
