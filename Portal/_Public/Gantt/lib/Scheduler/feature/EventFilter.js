import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';

/**
 * @module Scheduler/feature/EventFilter
 */

/**
 * Adds event filter menu items to the timeline header context menu.
 *
 * {@inlineexample Scheduler/feature/EventFilter.js}
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *   features : {
 *     eventFilter : true // `true` by default, set to `false` to disable the feature and remove the menu item from the timeline header
 *   }
 * });
 * ```
 *
 * This feature is **enabled** by default
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype eventFilter
 * @feature
 */
export default class EventFilter extends InstancePlugin {

    static get $name() {
        return 'EventFilter';
    }

    static get pluginConfig() {
        return {
            chain : ['populateTimeAxisHeaderMenu']
        };
    }

    /**
     * Populates the header context menu items.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateTimeAxisHeaderMenu({ items }) {
        const me = this;

        items.eventsFilter = {
            text        : 'L{filterEvents}',
            icon        : 'b-fw-icon b-icon-filter',
            disabled    : me.disabled,
            localeClass : me,
            weight      : 100,
            menu        : {
                type        : 'popup',
                localeClass : me,
                items       : {
                    nameFilter : {
                        weight               : 110,
                        type                 : 'textfield',
                        cls                  : 'b-eventfilter b-last-row',
                        clearable            : true,
                        keyStrokeChangeDelay : 300,
                        label                : 'L{byName}',
                        localeClass          : me,
                        width                : 200,
                        internalListeners    : {
                            change  : me.onEventFilterChange,
                            thisObj : me
                        }
                    }
                },
                onBeforeShow({ source : menu }) {
                    const
                        [filterByName] = menu.items,
                        filter         = me.store.filters.getBy('property', 'name');

                    filterByName.value = filter?.value || '';
                }
            }
        };
    }

    onEventFilterChange({ value }) {
        if (value !== '') {
            this.store.filter('name', value);
        }
        else {
            this.store.removeFilter('name');
        }
    }

    get store() {
        const { client } = this;

        return client.isGanttBase ? client.store : client.eventStore;
    }
}

EventFilter.featureClass = 'b-event-filter';

GridFeatureManager.registerFeature(EventFilter, true, ['Scheduler', 'Gantt']);
GridFeatureManager.registerFeature(EventFilter, false, 'ResourceHistogram');
