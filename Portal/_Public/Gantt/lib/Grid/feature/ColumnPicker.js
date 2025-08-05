import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import GridFeatureManager from './GridFeatureManager.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Grid/feature/ColumnPicker
 */

/**
 * Displays a column picker (to show/hide columns) in the header context menu. Columns can be displayed in sub menus
 * by region or tag. Grouped headers are displayed as menu hierarchies.
 *
 * {@inlineexample Grid/feature/ColumnPicker.js}
 *
 * This feature is <strong>enabled</strong> by default.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Grid/columns
 * @classtype columnPicker
 * @feature
 */
export default class ColumnPicker extends InstancePlugin {
    //region Config

    static $name = 'ColumnPicker';

    static configurable = {
        /**
         * Groups columns in the picker by region (each region gets its own sub menu)
         * @config {Boolean}
         * @default
         */
        groupByRegion : false,

        /**
         * Groups columns in the picker by tag, each column may be shown under multiple tags. See
         * {@link Grid.column.Column#config-tags}
         * @config {Boolean}
         * @default
         */
        groupByTag : false,

        /**
         * Configure this as `true` to have the fields from the Grid's {@link Core.data.Store}'s
         * {@link Core.data.Store#config-modelClass} added to the menu to create __new__ columns
         * to display the fields.
         *
         * This may be combined with the {@link Grid.view.mixin.GridState stateful} ability of the grid
         * to create a self-configuring grid.
         * @config {Boolean}
         * @default
         */
        createColumnsFromModel : false,

        menuCls : 'b-column-picker-menu b-sub-menu'
    };

    static get pluginConfig() {
        return {
            chain : ['populateHeaderMenu', 'getColumnDragToolbarItems']
        };
    }

    get grid() {
        return this.client;
    }

    //endregion

    //region Context menu

    /**
     * Get menu items, either a straight list of columns or sub menus per subgrid
     * @private
     * @param columnStore Column store to traverse
     * @returns {MenuItemConfig[]} Menu item configs
     */
    getColumnPickerItems(columnStore) {
        const
            me                        = this,
            { createColumnsFromModel } = me;

        let result;

        if (me.groupByRegion) {
            // submenus for grids regions
            result = me.grid.regions.map(region => {
                const columns = me.grid.getSubGrid(region).columns.topColumns;

                return {
                    text     : StringHelper.capitalize(region),
                    menu     : me.buildColumnMenu(columns),
                    disabled : columns.length === 0,
                    region
                };
            });
            if (createColumnsFromModel) {
                result.push({
                    text : me.L('L{newColumns}'),
                    menu : me.createAutoColumnItems()
                });
            }
        }
        else if (me.groupByTag) {
            // submenus for column tags
            const tags = {};
            columnStore.topColumns.forEach(column => {
                column.tags && Array.isArray(column.tags) && column.hideable !== false && column.tags.forEach(tag => {
                    if (!tags[tag]) {
                        tags[tag] = 1;
                    }
                });
            });


            result = Object.keys(tags).sort().map(tag => ({
                text            : StringHelper.capitalize(tag),
                menu            : me.buildColumnMenu(me.getColumnsForTag(tag)),
                tag,
                onBeforeSubMenu : ({ item, itemEl }) => {
                    me.refreshTagMenu(item, itemEl);
                }
            }));
            if (createColumnsFromModel) {
                result.push({
                    text : me.L('L{newColumns}'),
                    menu : me.createAutoColumnItems()
                });
            }
        }
        else {
            // all columns in same menu
            result = me.buildColumnMenu(columnStore.topColumns);

            if (createColumnsFromModel) {
                result.items.push(...ObjectHelper.transformNamedObjectToArray(me.createAutoColumnItems()));
            }
        }

        return result;
    }

    createAutoColumnItems() {
        const
            me             = this,
            { grid }       = me,
            {
                columns,
                store
            }              = grid,
            { modelClass } = store,
            { allFields }  = modelClass,
            result         = {};

        for (let i = 0, { length } = allFields; i < length; i++) {
            const
                field     = allFields[i],
                fieldName = field.name;

            if (!columns.get(fieldName)) {
                // Don't include system-level "internal" fields from the base Model classes like rowHeight or cls.
                if (!field.internal) {
                    result[fieldName] = {
                        text     : field.text || StringHelper.separate(field.name),
                        checked  : false,
                        onToggle : (event) => {
                            const column = columns.get(fieldName);

                            if (column) {
                                column[event.checked ? 'show' : 'hide']();
                            }
                            else {
                                columns.add(columns.generateColumnForField(field, {
                                    region : me.forColumn.region
                                }));
                            }
                            event.bubbles = false;
                        }
                    };
                }
            }
        }

        return result;
    }

    /**
     * Get all columns that has the specified tag.
     * @private
     * @param tag
     * @returns {Grid.column.Column[]}
     */
    getColumnsForTag(tag) {

        return this.grid.columns.records.filter(column =>
            column.tags && Array.isArray(column.tags) && column.tags.includes(tag) && column.hideable !== false
        );
    }

    /**
     * Refreshes checked status for a tag menu. Needed since columns can appear under multiple tags.
     * @private
     */
    refreshTagMenu(item, itemEl) {
        const columns = this.getColumnsForTag(item.tag);
        columns.forEach(column => {
            const subItem = item.items.find(subItem => subItem.column === column);
            if (subItem) subItem.checked = column.hidden !== true;
        });
    }

    /**
     * Traverses columns to build menu items for the column picker.
     * @private
     */
    buildColumnMenu(columns) {
        let currentRegion = columns.length > 0 && columns[0].region;

        const
            { grid } = this,
            items    = columns.reduce((items, column) => {
                const visibleInRegion = grid.columns.visibleColumns.filter(col => col.region === column.region);

                if (column.hideable !== false) {
                    const itemConfig = {
                        grid,
                        column,
                        text     : column.headerText,
                        checked  : column.hidden !== true,
                        disabled : column.hidden !== true && visibleInRegion.length === 1,
                        cls      : column.region !== currentRegion ? 'b-separator' : ''
                    };

                    currentRegion = column.region;

                    if (column.children && !column.isCollapsible) {
                        itemConfig.menu = this.buildColumnMenu(column.children);
                    }

                    items.push(itemConfig);
                }
                return items;
            }, []);

        return {
            cls : this.menuCls,
            items
        };
    }

    /**
     * Populates the header context menu items.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateHeaderMenu({ column, items }) {
        const
            me          = this,
            { columns } = me.grid;

        /**
         * The column on which the context menu was invoked.
         * @property {Grid.column.Column} forColumn
         * @readonly
         * @private
         */
        me.forColumn = column;

        if (column.showColumnPicker !== false && columns.some(col => col.hideable)) {
            // column picker
            items.columnPicker = {
                text        : 'L{columnsMenu}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-columns',
                cls         : 'b-separator',
                weight      : 200,
                menu        : me.getColumnPickerItems(columns),
                onToggle    : me.onColumnToggle,
                disabled    : me.disabled
            };
        }

        // menu item for hiding this column
        if (column.hideable !== false && !column.parent.isCollapsible) {
            items.hideColumn = {
                text        : 'L{hideColumn}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-hide-column',
                weight      : 210,
                disabled    : !column.allowDrag || me.disabled,
                onItem      : () => column.hide()
            };
        }
    }

    /**
     * Handler for column hide/show menu checkitems.
     * @private
     * @param {Object} event The {@link Core.widget.MenuItem#event-toggle} event.
     */
    onColumnToggle({ menu, item, checked }) {
        if (Boolean(item.column.hidden) !== !checked) {
            item.column[checked ? 'show' : 'hide']();

            const
                { grid, column }      = item,
                { columns, features } = grid,
                // Sibling items, needed to disable other item if it is the last one in region
                siblingItems          = menu.items,
                // Columns left visible in same region as this items column
                visibleInRegion       = columns.visibleColumns.filter(col => col.region === item.column.region),
                // Needed to access "hide-column" item outside of column picker
                hideItem              = features.headerMenu?.enabled && features.headerMenu.menu.widgetMap.hideColumn;

            // Do not allow user to hide the last column in any region
            if (visibleInRegion.length === 1) {
                const lastVisibleItem = siblingItems.find(menuItem => menuItem.column === visibleInRegion[0]);
                if (lastVisibleItem) {
                    lastVisibleItem.disabled = true;
                }

                // Also disable "Hide column" item if only one column left in this region
                if (hideItem && column.region === item.column.region) {
                    hideItem.disabled = true;
                }
            }
            // Multiple columns visible, enable "hide-column" and all items for that region
            else {
                visibleInRegion.forEach(col => {
                    const siblingItem = siblingItems.find(sibling => sibling.column === col);
                    if (siblingItem) {
                        siblingItem.disabled = false;
                    }
                });

                if (hideItem && column.region === item.column.region) {
                    hideItem.disabled = false;
                }
            }

            // Reflect status in submenu.
            item.menu?.eachWidget(subItem => {
                subItem.checked = checked;
            });

            const parentItem = menu.owner;
            if (parentItem && parentItem.column === column.parent) {
                parentItem.checked = siblingItems.some(subItem => subItem.checked === true);
            }
        }
    }

    /**
     * Supply items to ColumnDragToolbar
     * @private
     */
    getColumnDragToolbarItems(column, items) {
        const visibleInRegion = this.grid.columns.visibleColumns.filter(col => col.region === column.region);

        if (column.hideable !== false && visibleInRegion.length > 1) {
            items.push({
                text        : 'L{hideColumnShort}',
                ref         : 'hideColumn',
                group       : 'L{column}',
                localeClass : this,
                icon        : 'b-fw-icon b-icon-hide-column',
                weight      : 101,
                onDrop      : ({ column }) => column.hide()
            });
        }
        return items;
    }

    //endregion
}

GridFeatureManager.registerFeature(ColumnPicker, true);
