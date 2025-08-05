import ContextMenuBase from '../../Grid/feature/base/ContextMenuBase.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Grid/feature/CellMenu
 */

/**
 * Right click to display context menu for cells.
 *
 * ### Default cell menu items
 *
 * The Cell menu feature provides only one item by default and all the other items are populated by the other features:
 *
 * | Item reference         | Text             | Weight | Feature    | Enabled by default | Description                                                                    |
 * |------------------------|------------------|--------|------------|--------------------|--------------------------------------------------------------------------------|
 * | `removeRow`            | Delete record    | 100    | `CellMenu` | true               | Removes the selected record from the store                                     |
 * | `search`               | Search for value | 200    | `Search`   | false              | Searches the grid for the selected cell text                                   |
 * | `filterDateEquals`     | On               | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value  |
 * | `filterDateBefore`     | Before           | 310    | `Filter`   | false              | Filters records in the store by the column field less than selected cell value |
 * | `filterDateAfter`      | After            | 320    | `Filter`   | false              | Filters records in the store by the column field more than selected cell value |
 * | `filterNumberEquals`   | Equals           | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value  |
 * | `filterNumberLess`     | Less than        | 310    | `Filter`   | false              | Filters records in the store by the column field less than selected cell value |
 * | `filterNumberMore`     | More than        | 320    | `Filter`   | false              | Filters records in the store by the column field more than selected cell value |
 * | `filterDurationEquals` | Equals           | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value  |
 * | `filterDurationLess`   | Less than        | 310    | `Filter`   | false              | Filters records in the store by the column field less than selected cell value |
 * | `filterDurationMore`   | More than        | 320    | `Filter`   | false              | Filters records in the store by the column field more than selected cell value |
 * | `filterStringEquals`   | Equals           | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value  |
 * | `filterRemove`         | Remove filter    | 400    | `Filter`   | false              | Stops filtering by selected column field                                       |
 *
 * ### Customizing the menu items
 *
 * The menu items in the Cell menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra items for all columns:
 *
 * ```javascript
 * const grid = new Grid({
 *   features : {
 *     cellMenu : {
 *       items : {
 *          extraItem : { text: 'My cell item', icon: 'fa fa-bus', weight: 200, onItem : () => ... }
 *       }
 *     }
 *   }
 * });
 * ```
 *
 * It is also possible to add items using columns config. See examples below.
 *
 * Add extra items for a single column:
 *
 * ```javascript
 * const grid = new Grid({
 *   columns: [
 *     {
 *       field: 'city',
 *       text: 'City',
 *       cellMenuItems: {
 *         columnItem : { text: 'My unique cell item', icon: 'fa fa-beer', onItem : () => ... }
 *       }
 *     }
 *   ]
 * });
 * ```
 *
 * Remove existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         cellMenu : {
 *             items : {
 *                 removeRow : false
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
 *         cellMenu : {
 *             items : {
 *                 removeRow : {
 *                     text : 'Throw away',
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
 * const grid = new Grid({
 *   features : {
 *     cellMenu : {
 *       processItems({items, record}) {
 *           if (record.cost > 5000) {
 *              items.myItem = { text : 'Split cost' };
 *           }
 *       }
 *     }
 *   }
 * });
 * ```
 *
 * Full information of the menu customization can be found in the ["Customizing the Cell menu and the Header menu"](#guides/customization/contextmenu.md)
 * guide.
 *
 * This feature is **enabled** by default.
 *
 * @extends Grid/feature/base/ContextMenuBase
 * @demo Grid/filtering
 * @classtype cellMenu
 * @externalexample feature/CellMenu.js
 */
export default class CellMenu extends ContextMenuBase {
    //region Config

    static get $name() {
        return 'CellMenu';
    }

    static get defaultConfig() {
        return {
            type : 'cell'
        };
    }

    static get pluginConfig() {
        const config = super.pluginConfig;

        config.chain.push('populateCellMenu');

        return config;
    }

    //endregion

    //region Events

    /**
     * Fired from grid before the context menu is shown for a cell.
     * Allows manipulation of the items to show in the same way as in the {@link Grid.feature.base.ContextMenuBase#config-processItems}.
     *
     * Returning `false` from a listener prevents the menu from being shown.
     *
     * @event cellMenuBeforeShow
     * @preventable
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} items Menu item configs
     * @param {Grid.column.Column} column Column
     * @param {Core.data.Model} record Record
     */

    /**
     * Fired from grid after the context menu is shown for a cell.
     * @event cellMenuShow
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} items Menu item configs
     * @param {Grid.column.Column} column Column
     * @param {Core.data.Model} record Record
     */

    /**
     * Fired from grid when an item is selected in the cell context menu.
     * @event cellMenuItem
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} item Selected menu item
     * @param {Grid.column.Column} column Column
     * @param {Core.data.Model} record Record
     */

    /**
     * Fired from grid when a check item is toggled in the cell context menu.
     * @event cellMenuToggleItem
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} item Selected menu item
     * @param {Grid.column.Column} column Column
     * @param {Core.data.Model} record Record
     * @param {Boolean} checked Checked or not
     */

    //endregion

    //region Menu handlers

    showContextMenu(eventParams) {
        const
            me = this,
            {
                cellSelector,
                id,
                event
            }  = eventParams;

        // Process the gesture as navigation so that the use may select/multiselect
        // the items to include in their context menu operation.
        // Also select if not already selected.
        me.client.focusCell(cellSelector, {
            doSelect : !me.client.isSelected(id),
            event
        });

        super.showContextMenu(eventParams);
    }

    shouldShowMenu({ column }) {
        return column && column.enableCellContextMenu !== false;
    }

    getDataFromEvent(event) {
        return ObjectHelper.assign(super.getDataFromEvent(event), this.client.getCellDataFromEvent(event));
    }

    beforeContextMenuShow(eventParams) {
        if (!eventParams.record || eventParams.record.isSpecialRow) {
            eventParams.items.removeRow = false;
        }
    }

    //endregion

    //region Getters/Setters

    populateCellMenu({ items, column }) {
        const { client } = this;

        if (column?.cellMenuItems) {
            if (Array.isArray(column.cellMenuItems)) {
                VersionHelper.deprecate('Grid', '5.0.0', '`cellMenuItems` column config specified as an array is deprecated, need to specify the config as a named object. Please see https://bryntum.com/docs/grid/#guides/upgrades/4.0.0.md for more information.');
            }

            // Array works smoothly since number index turns into a key for named object
            ObjectHelper.merge(items, column.cellMenuItems);
        }

        if (!client.readOnly) {
            items.removeRow = {
                text        : client.selectedRecords.length > 1 ? 'L{removeRows}' : 'L{removeRow}',
                localeClass : this,
                icon        : 'b-fw-icon b-icon-trash',
                name        : 'removeRow',
                cls         : 'b-separator',
                weight      : 100,
                onItem      : () => client.store.remove(client.selectedRecords)
            };

            if (!client.showRemoveRowInContextMenu) {
                VersionHelper.deprecate('Grid', '5.0.0', '`showRemoveRowInContextMenu` config is deprecated, in favor of `CellMenu` feature configuration. Please see https://bryntum.com/docs/grid/#guides/upgrades/4.0.0.md for more information.');
                items.removeRow = false;
            }
        }
    }

    get showMenu() {
        return true;
    }

    //endregion
}

CellMenu.featureClass = '';

GridFeatureManager.registerFeature(CellMenu, true, ['Grid', 'Scheduler']);
GridFeatureManager.registerFeature(CellMenu, false, ['Gantt']);
