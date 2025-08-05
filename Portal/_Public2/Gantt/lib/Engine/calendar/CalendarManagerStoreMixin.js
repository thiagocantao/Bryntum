import Store from "../../Common/data/Store.js";
import { PartOfProjectGenericMixin } from "../data/PartOfProjectGenericMixin.js";
import { ChronoStoreMixin } from "../data/store/mixin/ChronoStoreMixin.js";
import { PartOfProjectStoreMixin } from "../data/store/mixin/PartOfProjectMixin.js";
import { MinimalCalendar } from "./CalendarMixin.js";
export const CalendarManagerStoreMixin = (base) => {
    return class CalendarManagerStoreMixin extends base {
        static get defaultConfig() {
            return {
                tree: true,
                modelClass: MinimalCalendar
            };
        }
    };
};
/**
 * Function to a build a minimal possible [[CalendarManagerStoreMixin]] class.
 */
export const BuildMinimalCalendarManagerStore = (base = Store) => CalendarManagerStoreMixin(PartOfProjectStoreMixin(PartOfProjectGenericMixin(ChronoStoreMixin(base))));
/**
 * Minimal possible [[CalendarManagerStoreMixin]] class
 */
export class MinimalCalendarManagerStore extends BuildMinimalCalendarManagerStore() {
}
