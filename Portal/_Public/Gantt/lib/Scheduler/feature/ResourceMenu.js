import ContextMenuBase from '../../Core/feature/base/ContextMenuBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';

/**
 * @module Scheduler/feature/ResourceMenu
 */

/**
 * Applicable only for Scheduler in `vertical` mode. Right click resource header cells to display a context menu.
 *
 * To invoke the menu in a keyboard-accessible manner, use the `SPACE` key when a resource cell is focused.
 *
 * {@inlineexample Scheduler/feature/ResourceMenu.js}
 *
 * ### Default menu items
 *
 * The ResourceMenu feature provides only one item by default:
 *
 * | Reference              | Text   | Weight | Description         |
 * |------------------------|--------|--------|---------------------|
 * | `remove`               | Delete | 100    | Delete the resource |
 *
 * ### Customizing the menu items
 *
 * The menu items in the resource menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra items for all columns:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     mode     : 'vertical',
 *     features : {
 *         resourceMenu : {
 *             items : {
 *                 extraItem : {
 *                     text   : 'My cell item',
 *                     icon   : 'fa fa-bus',
 *                     weight : 200,
 *                     onItem : () => ...
 *                 }
 *             }
 *         }
 *     }
 * });
 *
 * Remove an existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     mode     : 'vertical',
 *     features : {
 *         resourceMenu : {
 *             items : {
 *                 remove : null
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
 *     mode     : 'vertical',
 *     features : {
 *         resourceMenu : {
 *             items : {
 *                 remove : {
 *                     text : 'Remove',
 *                     icon : 'b-fa b-fa-dumpster'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * It is also possible to manipulate the default items and add new items in the processing function:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     mode     : 'vertical',
 *     features : {
 *         resourceMenu : {
 *             processItems({items, record}) {
 *                 if (record.cost > 5000) {
 *                     items.myItem = { text : 'Split cost' };
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * This feature is **disabled** by default.
 *
 * @extends Core/feature/base/ContextMenuBase
 * @demo Scheduler/vertical
 * @classtype resourceMenu
 * @feature
 */
export default class ResourceMenu extends ContextMenuBase {
    //region Config

    static $name = 'ResourceMenu';

    static configurable = {
        /**
         * A function called before displaying the menu that allows manipulations of its items.
         * Returning `false` from this function prevents the menu being shown.
         *
         * ```javascript
         * features : {
         *     resourceMenu : {
         *         processItems({ items, record, column }) {
         *             // Add or hide existing items here as needed
         *             items.myAction = {
         *                 text   : 'Cool action',
         *                 icon   : 'b-fa b-fa-fw b-fa-ban',
         *                 onItem : () => console.log(`Clicked ${record.name}`),
         *                 weight : 1000 // Move to end
         *             };
         *
         *             if (!record.allowDelete) {
         *                 items.remove.hidden = true;
         *             }
         *         }
         *     }
         * },
         * ```
         * @param {Object} context An object with information about the menu being shown
         * @param {Scheduler.model.ResourceModel} context.resourceRecord The record representing the current resource
         * @param {Object<String,MenuItemConfig|Boolean|null>} context.items An object containing the
         * {@link Core.widget.MenuItem menu item} configs keyed by their id
         * @param {Event} context.event The DOM event object that triggered the show
         * @config {Function}
         * @preventable
         */
        processItems : null,

        /**
         * {@link Core.widget.Menu} items object containing named child menu items to apply to the feature's
         * provided context menu.
         *
         * This may add extra items as below, but you can also configure, or remove any of the default items by
         * configuring the name of the item as `null`
         *
         * ```javascript
         * features : {
         *     resourceMenu : {
         *         // This object is applied to the Feature's predefined default items
         *         items : {
         *             switchToDog : {
         *                 text : 'Dog',
         *                 icon : 'b-fa b-fa-fw b-fa-dog',
         *                 onItem({record}) {
         *                     record.dog = true;
         *                     record.cat = false;
         *                 },
         *                 weight : 500     // Make this second from end
         *             },
         *             switchToCat : {
         *                 text : 'Cat',
         *                 icon : 'b-fa b-fa-fw b-fa-cat',
         *                 onItem({record}) {
         *                     record.dog = false;
         *                     record.cat = true;
         *                 },
         *                 weight : 510     // Make this sink to end
         *             },
         *             remove : {
         *                 // Change icon for the delete item
         *                 icon : 'b-fa b-fa-times'
         *             }
         *         }
         *     }
         * },
         * ```
         *
         * @config {Object<String,MenuItemConfig|Boolean|null>}
         */
        items : null,

        type : 'resource'
    };

    static get pluginConfig() {
        const config = super.pluginConfig;

        config.chain.push('populateResourceMenu');

        return config;
    }

    //endregion

    //region Events

    /**
     * This event fires on the owning scheduler before the context menu is shown for a resource.
     * Allows manipulation of the items to show in the same way as in the {@link #config-processItems}.
     *
     * Returning `false` from a listener prevents the menu from being shown.
     *
     * @event resourceMenuBeforeShow
     * @preventable
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object<String,MenuItemConfig>} items Menu item configs
     * @param {Grid.column.Column} column Column
     * @param {Scheduler.model.ResourceModel} resourceRecord Record
     */

    /**
     * This event fires on the owning scheduler after the context menu is shown for a resource.
     * @event resourceMenuShow
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object<String,MenuItemConfig>} items Menu item configs
     * @param {Scheduler.model.ResourceModel} record Record
     */

    /**
     * This event fires on the owning scheduler when an item is selected in the context menu.
     * @event resourceMenuItem
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Core.widget.MenuItem} item Selected menu item
     * @param {Scheduler.model.ResourceModel} record Record
     */

    /**
     * This event fires on the owning grid when a check item is toggled in the context menu.
     * @event resourceMenuToggleItem
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Core.widget.MenuItem} item Selected menu item
     * @param {Scheduler.model.ResourceModel} resourceRecord Record
     * @param {Boolean} checked Checked or not
     */

    //endregion

    //region Menu handlers

    getDataFromEvent(event) {
        const
            cellElement = event.target.closest('.b-resourceheader-cell'),
            resourceId  = cellElement?.dataset.resourceId;

        // Only yield data to show a menu if we are on a cell
        if (resourceId) {
            const resourceRecord = this.client.resourceStore.getById(resourceId);

            return {
                cellElement,
                resourceRecord
            };
        }
    }

    //endregion

    //region Getters/Setters

    populateResourceMenu({ items, resourceRecord }) {
        const { client } = this;

        if (!client.readOnly) {
            items.remove = {
                text        : 'L{CellMenu.removeRow}',
                localeClass : this,
                icon        : 'b-fw-icon b-icon-trash',
                cls         : 'b-separator',
                weight      : 100,
                disabled    : resourceRecord.readOnly,
                onItem      : () => client.resourceStore.remove(resourceRecord)
            };
        }
    }

    get showMenu() {
        return true;
    }

    //endregion
}

ResourceMenu.featureClass = '';

GridFeatureManager.registerFeature(ResourceMenu, false, ['Scheduler']);
