import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import TimeSpanMenuBase from './base/TimeSpanMenuBase.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/feature/EventMenu
 */

/**
 * Displays a context menu for events. Items are populated by other features and/or application code.
 *
 * ### Default event menu items
 *
 * Here is the list of menu items provided by the feature and populated by the other features:
 *
 * | Item reference  | Text           | Weight | Feature     | Enabled by default | Description                                                                                                                                |
 * |-----------------|----------------|--------|-------------|--------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
 * | `editEvent`     | Edit event     | 100    | `EventEdit` | true               | Opens event editor for the selected event. Shown if the Scheduler is not read-only.                                                        |
 * | `deleteEvent`   | Delete event   | 200    | `EventMenu` | true               | Removes the selected event from the store. Shown if the Scheduler is not read-only.                                                        |
 * | `unassignEvent` | Unassign event | 300    | `EventMenu` | true               | Removes the selected resource assignment from the selected event. Shown in case of multi-assignment and if the Scheduler is not read-only. |
 *
 * ### Customizing the menu items
 *
 * The menu items in the Event menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra items for all events:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 extraItem : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem({eventRecord}) {
 *                         eventRecord.flagged = true;
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Remove existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 deleteEvent   : false,
 *                 unassignEvent : false
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
 *         eventMenu : {
 *             items : {
 *                 deleteEvent : {
 *                     text : 'Delete booking'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Manipulate existing items for all events or specific events:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             // Process items before menu is shown
 *             processItems({eventRecord, items}) {
 *                  // Push an extra item for conferences
 *                  if (eventRecord.type === 'conference') {
 *                      items.showSessionItem = {
 *                          text : 'Show sessions',
 *                          onItem({eventRecord}) {
 *                              // ...
 *                          }
 *                      };
 *                  }
 *
 *                  // Do not show menu for secret events
 *                  if (eventRecord.type === 'secret') {
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
 * @extends Scheduler/feature/base/TimeSpanMenuBase
 * @demo Scheduler/eventmenu
 * @classtype eventMenu
 */
export default class EventMenu extends TimeSpanMenuBase {
    //region Config

    static get $name() {
        return 'EventMenu';
    }

    static get defaultConfig() {
        return {
            type : 'event'

            /**
             * This is a preconfigured set of items used to create the default context menu.
             *
             * The `items` provided by this feature are listed below. These are the property names which you may
             * configure in the feature's {@link Grid.feature.base.ContextMenuBase#config-items} config:
             *
             * - `deleteEvent` Deletes the context event.
             * - `unassignEvent` Unassigns the context event from the context resource.
             *
             * To remove existing items, set corresponding keys to `false`
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         eventMenu : {
             *             items : {
             *                 deleteEvent   : false,
             *                 unassignEvent : false
             *             }
             *         }
             *     }
             * });
             * ```
             *
             * See the feature config in the above example for details.
             *
             * @config {Object} items
             */
        };
    }

    static get pluginConfig() {
        const config = super.pluginConfig;

        config.chain.push('populateEventMenu');

        return config;
    }

    //endregion

    //region Events

    /**
     * Fired from scheduler before the context menu is shown for an event. Allows manipulation of the items
     * to show in the same way as in `processItems`. Returning `false` from a listener prevents the menu from
     * being shown.
     * @event eventMenuBeforeShow
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
     * @event eventMenuItem
     * @param {Scheduler.view.Scheduler} source
     * @param {Core.widget.MenuItem} item
     * @param {Scheduler.model.EventModel} eventRecord
     * @param {Scheduler.model.ResourceModel} resourceRecord
     * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
     * @param {HTMLElement} eventElement
     */

    /**
     * Fired from scheduler after showing the context menu for an event
     * @event eventMenuShow
     * @param {Scheduler.view.Scheduler} source
     * @param {Core.widget.Menu} menu The menu
     * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
     * @param {HTMLElement} eventElement
     */

    //endregion

    onElementKeyDown(event) {
        if (!event.handled && event.target.matches(this.client.eventSelector)) {
            if (event.key === ' ') {
                this.internalShowContextMenu(this.createContextMenuEventForElement(event.target));
            }
        }
    }

    getDataFromEvent(event) {
        const
            data             = super.getDataFromEvent(event),
            eventElement     = data.targetElement,
            { client }       = this,
            eventRecord      = client.resolveEventRecord(eventElement),
            // For vertical mode the resource must be resolved from the event
            resourceRecord   = eventRecord && (client.resolveResourceRecord(eventElement) || client.resourceStore.last),
            assignmentRecord = eventRecord && client.resolveAssignmentRecord(eventElement);

        return ObjectHelper.assign(data, { eventElement, eventRecord, resourceRecord, assignmentRecord });
    }

    getTargetElementFromEvent({ target }) {
        return DomHelper.up(target, this.client.eventSelector) || target;
    }

    shouldShowMenu(eventParams) {
        return eventParams.eventRecord;
    }

    /**
     * Shows context menu for the provided event. If record is not rendered (outside of time span/filtered)
     * menu won't appear.
     * @param {Scheduler.model.EventModel} eventRecord Event record to show menu for.
     * @param {Object} [options]
     * @param {HTMLElement} options.targetElement Element to align context menu to.
     * @param {MouseEvent} options.event Browser event.
     * If provided menu will be aligned according to clientX/clientY coordinates.
     * If omitted, context menu will be centered to event element.
     */
    showContextMenuFor(eventRecord, { targetElement, event } = {}) {
        if (this.disabled) {
            return;
        }

        if (!targetElement) {
            targetElement = this.getElementFromRecord(eventRecord);

            // If record is not rendered, do nothing
            if (!targetElement) {
                return;
            }
        }

        event = event || this.createContextMenuEventForElement(targetElement);

        this.internalShowContextMenu(event);
    }

    getElementFromRecord(record) {
        return this.client.getElementsFromEventRecord(record)[0];
    }

    populateEventMenu({ items }) {
        const { client } = this;

        if (!client.readOnly) {
            items.deleteEvent = {
                text   : 'L{SchedulerBase.Delete event}',
                icon   : 'b-icon b-icon-trash',
                weight : 200,
                onItem({ menu, eventRecord }) {
                    // We must synchronously push focus back into the menu's triggering
                    // event so that the our beforeRemove handlers can move focus onwards
                    // to the closest remaining event.
                    // Otherwise, the menu's default hide processing on hide will attempt
                    // to move focus back to the menu's triggering event which will
                    // by then have been deleted.
                    const revertTarget = menu.focusInEvent?.relatedTarget;
                    if (revertTarget) {
                        revertTarget.focus();
                        client.navigator.activeItem = revertTarget;
                    }
                    client.removeRecords([eventRecord]);
                }
            };

            if (!client.eventStore.usesSingleAssignment) {
                items.unassignEvent = {
                    text   : 'L{SchedulerBase.Unassign event}',
                    icon   : 'b-icon b-icon-unassign',
                    weight : 300,
                    name   : 'unassignEvent',
                    onItem({ menu, eventRecord, resourceRecord }) {
                        // We must synchronously push focus back into the menu's triggering
                        // event so that the our beforeRemove handlers can move focus onwards
                        // to the closest remaining event.
                        // Otherwise, the menu's default hide processing on hide will attempt
                        // to move focus back to the menu's triggering event which will
                        // by then have been deleted.
                        const revertTarget = menu.focusInEvent?.relatedTarget;
                        if (revertTarget) {
                            revertTarget.focus();
                            client.navigator.activeItem = revertTarget;
                        }
                        eventRecord.unassign(resourceRecord);
                    }
                };
            }
        }
    }
}

EventMenu.featureClass = '';

GridFeatureManager.registerFeature(EventMenu, true, 'Scheduler');
