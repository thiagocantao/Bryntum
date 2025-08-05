import TimeSpanMenuBase from '../../Scheduler/feature/base/TimeSpanMenuBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/feature/ScheduleMenu
 */

/**
 * Displays a context menu for empty parts of the schedule. Items are populated in the first place
 * by configurations of this Feature, then by other features and/or application code.
 *
 * ### Default scheduler zone menu items
 *
 * The Scheduler menu feature provides only one item:
 *
 * | Item reference | Text      | Weight | Feature        | Enabled by default | Description                                                                                                                                                                       |
 * |----------------|-----------|--------|----------------|--------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
 * | `addEvent`     | Add event | 100    | `ScheduleMenu` | true               | Adds a new event to the store. The event starts at the selected point of time and lasts 1 hour. It is assigned to the selected resource. Shown if the Scheduler is not read-only. |
 *
 * ### Customizing the menu items
 *
 * The menu items in the Scheduler menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 extraItem : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem({date, resourceRecord, items}) {
 *                         // Custom date based action
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Remove existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 addEvent : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Customize existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 addEvent : {
 *                     text : 'Create new booking'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Manipulate existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             // Process items before menu is shown
 *             processItems({date, resourceRecord, items}) {
 *                  // Add an extra item for ancient times
 *                  if (date < new Date(2018, 11, 17)) {
 *                      items.modernize = {
 *                          text : 'Modernize',
 *                          ontItem({date}) {
 *                              // Custom date based action
 *                          }
 *                      };
 *                  }
 *
 *                  // Do not show menu for Sundays
 *                  if (date.getDay() === 0) {
 *                      return false;
 *                  }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Full information of the menu customization can be found in the "Customizing the Event menu, the Schedule menu, and the TimeAxisHeader menu" guide.
 *
 * This feature is **enabled** by default
 *
 * @demo Scheduler/basic
 * @extends Scheduler/feature/base/TimeSpanMenuBase
 * @classtype scheduleMenu
 */
export default class ScheduleMenu extends TimeSpanMenuBase {
    //region Config

    static get $name() {
        return 'ScheduleMenu';
    }

    static get defaultConfig() {
        return {
            type : 'schedule'

            /**
             * This is a preconfigured set of items used to create the default context menu.
             *
             * The `items` provided by this feature are listed below. These are the property names which you may
             * configure in the feature's {@link Grid.feature.base.ContextMenuBase#config-items} config:
             *
             * - `addEvent` Add an event for at the resource and time indicated by the `contextmenu` event.
             *
             * To remove existing items, set corresponding keys to `false`
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         scheduleMenu : {
             *             items : {
             *                 addEvent : false
             *             }
             *         }
             *     }
             * });
             * ```
             *
             * @config {Object} items
             */
        };
    }

    static get pluginConfig() {
        const config = super.pluginConfig;

        config.chain.push('populateScheduleMenu');

        return config;
    }

    //endregion

    //region Events

    /**
     * Fired from scheduler before the context menu is shown for an event. Allows manipulation of the items
     * to show in the same way as in `processItems`. Returning `false` from a listener prevents the menu from
     * being shown.
     * @event scheduleMenuBeforeShow
     * @preventable
     * @param {Scheduler.view.Scheduler} source
     * @param {Object} items Menu item configs
     * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
     * @param {HTMLElement} eventElement
     */

    /**
     * Fired from scheduler when an item is selected in the context menu.
     * @event scheduleMenuItem
     * @param {Scheduler.view.Scheduler} source
     * @param {Core.widget.MenuItem} item
     * @param {Scheduler.model.EventModel} eventRecord
     * @param {Scheduler.model.ResourceModel} resourceRecord
     * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
     * @param {HTMLElement} eventElement
     */

    /**
     * Fired from scheduler after showing the context menu for an event
     * @event scheduleMenuShow
     * @param {Scheduler.view.Scheduler} source
     * @param {Core.widget.Menu} menu The menu
     * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
     * @param {HTMLElement} eventElement
     */

    //endregion

    shouldShowMenu(eventParams) {
        const
            { client } = this,
            {
                column,
                targetElement,
                resourceRecord
            } = eventParams,
            isTimeAxisColumn = column
                ? column === client.timeAxisColumn
                : client.timeAxisSubGrid.element === targetElement;

        return isTimeAxisColumn && !(resourceRecord && resourceRecord.isSpecialRow);
    }

    getDataFromEvent(event) {
        const
            { client }     = this,
            cellData       = client.getCellDataFromEvent(event),
            date           = client.getDateFromDomEvent(event, 'floor'),
            // For vertical mode the resource must be resolved from the event
            resourceRecord = client.resolveResourceRecord(event) || client.resourceStore.last;

        return ObjectHelper.assign(super.getDataFromEvent(event), cellData, { date, resourceRecord });
    }

    populateScheduleMenu({ items }) {
        const { client } = this;

        if (!client.readOnly) {
            items.addEvent = {
                text     : 'L{SchedulerBase.Add event}',
                icon     : 'b-icon b-icon-add',
                disabled : client.resourceStore.count === 0,
                weight   : 100,
                onItem({ date, resourceRecord }) {
                    client.internalAddEvent(date, resourceRecord, client.getRowFor(resourceRecord));
                }
            };
        }
    }
}

ScheduleMenu.featureClass = '';

GridFeatureManager.registerFeature(ScheduleMenu, true, 'Scheduler');
