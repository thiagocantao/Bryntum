import DomDataStore from '../../Core/data/DomDataStore.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from './GridFeatureManager.js';
import DomSync from '../../Core/helper/DomSync.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import StringHelper from '../../Core/helper/StringHelper.js';



/**
 * @module Grid/feature/Group
 */

/**
 * Enables rendering and handling of row groups. The actual grouping is done in the store, but triggered by [shift] +
 * clicking headers or by using two finger tap (one on header, one anywhere on grid). Use [shift] + [alt] + click to
 * remove a column grouper.
 *
 * Groups can be expanded/collapsed by clicking on the group row or pressing [space] when group row is selected.
 * The actual grouping is done by the store, see {@link Core.data.mixin.StoreGroup#function-group}.
 *
 * Grouping by a field performs sorting by the field automatically. It's not possible to prevent sorting.
 * If you group, the records have to be sorted so that records in a group stick together. You can either control sorting
 * direction, or provide a custom sorting function called {@link #config-groupSortFn} to your feature config object.
 *
 * For info on programmatically handling grouping, see {@link Core.data.mixin.StoreGroup}.
 *
 * Currently, grouping is not supported when using pagination, the underlying store cannot group data that is split into pages.
 *
 * **Note:** Custom height for group header rows cannot be set with CSS, should instead be defined in a renderer function using the `size` param. See the {@link #config-renderer} config for details.
 *
 * This feature is **enabled** by default.
 *
 * ## Keyboard shortcuts
 * This feature has the following default keyboard shortcuts:
 *
 * | Keys     | Action        | Action description                                                         |
 * |----------|---------------|----------------------------------------------------------------------------|
 * | `Space`  | *toggleGroup* | When a group header is focused, this expands or collapses the grouped rows |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md)
 *
 * @example
 * // grouping feature is enabled, no default value though
 * let grid = new Grid({
 *     features : {
 *         group : true
 *     }
 * });
 *
 * // use initial grouping
 * let grid = new Grid({
 *     features : {
 *         group : 'city'
 *     }
 * });
 *
 * // default grouper and custom renderer, which will be applied to each cell except the "group" cell
 * let grid = new Grid({
 *     features : {
 *       group : {
 *           field : 'city',
 *           ascending : false,
 *           renderer : ({ isFirstColumn, count, groupRowFor, record }) => isFirstColumn ? `${groupRowFor} (${count})` : ''
 *       }
 *     }
 * });
 *
 * // group using custom sort function
 * let grid = new Grid({
 *     features : {
 *         group       : {
 *             field       : 'city',
 *             groupSortFn : (a, b) => a.city.length < b.city.length ? -1 : 1
 *         }
 *     }
 * });
 *
 * // can also be specified on the store
 * let grid = new Grid({
 *     store : {
 *         groupers : [
 *             { field : 'city', ascending : false }
 *         ]
 *     }
 * });
 *
 * // custom sorting function can also be specified on the store
 * let grid = new Grid({
 *     store : {
 *         groupers : [{
 *             field : 'city',
 *             fn : (recordA, recordB) => {
 *                 // apply custom logic, for example:
 *                 return recordA.city.length < recordB.city.length ? -1 : 1;
 *             }
 *         }]
 *     }
 * });
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/grouping
 * @classtype group
 * @feature
 *
 * @inlineexample Grid/feature/Group.js
 */
export default class Group extends InstancePlugin {

    static get $name() {
        return 'Group';
    }

    static get configurable() {
        return {
            /**
             * The name of the record field to group by.
             * @config {String}
             * @default
             */
            field : null,

            /**
             * A function used to sort the groups.
             * When grouping, the records have to be sorted so that records in a group stick together.
             * Technically that means that records having the same {@link #config-field} value
             * should go next to each other.
             * And this function (if provided) is responsible for applying such grouping order.
             * ```javascript
             * const grid = new Grid({
             *     features : {
             *         group : {
             *             // group by category
             *             field       : 'category',
             *             groupSortFn : (a, b) => {
             *                 const
             *                     aCategory = a.category || '',
             *                     bCategory = b.category || '';
             *
             *                 // 1st sort by "calegory" field
             *                 return aCategory > bCategory ? -1 :
             *                     aCategory < bCategory ? 1 :
             *                     // inside calegory groups we sort by "name" field
             *                     (a.name > b.name ? -1 : 1);
             *             }
             *         }
             *     }
             * });
             * ```
             * @config {Function}
             */
            groupSortFn : null,

            /**
             * A function which produces the HTML for a group header.
             * The function is called in the context of this Group feature object.
             * Default group renderer displays the `groupRowFor` and `count`.
             *
             * @config {Function}
             * @property {String} groupRowFor The value of the `field` for the group.
             * @property {Core.data.Model} record The group record representing the group.
             * @property {Object} record.meta Meta data with additional info about the grouping.
             * @property {Array} record.groupChildren The group child records.
             * @property {Number} count Number of records in the group.
             * @property {Grid.column.Column} column The column the renderer runs for.
             * @property {Boolean} isFirstColumn True, if `column` is the first column.
             * If `RowNumberColumn` is the real first column, it's not taken into account.
             * @property {Grid.column.Column} [groupColumn] The column under which the `field` is shown.
             * @property {Object} size Sizing information for the group header row, only `height` is relevant.
             * @property {Number} size.height The height of the row, set this if you want a custom height for the group header row
             * That is UI part, so do not rely on its existence.
             * @default
             */
            renderer : null,

            /**
             * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
             * @config {Object<String,String>}
             */
            keyMap : {
                ' ' : 'toggleGroup'
            },

            /**
             * By default, clicking anywhere in a group row toggles its expanded/collapsed state.
             *
             * Configure this as `false` to limit this to only toggling on click of the expanded/collapsed
             * state icon.
             * @prp {Boolean}
             * @default
             */
            toggleOnRowClick : true
        };
    }

    //region Init

    construct(grid, config) {
        const me = this;

        if (grid.features.tree) {
            return;
        }

        // groupSummary feature needs to be initialized first, if it is used
        me._thisIsAUsedExpression(grid.features.groupSummary);

        // process initial config into an actual config object
        config = me.processConfig(config);

        me.grid = grid;

        super.construct(grid, config);

        me.bindStore(grid.store);

        grid.rowManager.ion({
            beforeRenderRow : 'onBeforeRenderRow',
            renderCell      : 'renderCell',

            // The feature gets to see cells being rendered before the GroupSummary feature
            // because this injects header content into group header rows and adds rendering
            // info to the cells renderData which GroupSummary must comply with.
            prio    : 1100,
            thisObj : me
        });
    }

    // Group feature handles special config cases, where user can supply a string or a group config object
    // instead of a normal config object
    processConfig(config) {
        if (typeof config === 'string') {
            return {
                field     : config,
                ascending : null
            };
        }

        return config;
    }

    // override setConfig to process config before applying it (used mainly from ReactGrid)
    setConfig(config) {
        if (config === null) {
            this.store.clearGroupers();
        }
        else {
            super.setConfig(this.processConfig(config));
        }
    }

    bindStore(store) {
        this.detachListeners('store');

        store.ion({
            name        : 'store',
            group       : 'onStoreGroup',
            change      : 'onStoreChange',
            toggleGroup : 'onStoreToggleGroup',
            thisObj     : this
        });

        this.onStoreGroup({ groupers : store.groupers });
    }

    updateRenderer(renderer) {
        this.groupRenderer = renderer;
    }

    updateField(field) {
        // Do not reapply grouping if already grouped by the field. This will prevent group direction from flipping
        // when splitting grids using group feature configured with field (store is shared)
        if (!this.isConfiguring || !this.store.groupers?.some(g => g.field === field)) {
            this.store.group({
                field,
                ascending : this.ascending,
                fn        : this.groupSortFn
            });
        }
    }

    updateGroupSortFn(fn) {
        if (!this.isConfiguring) {
            this.store.group({
                field     : this.field,
                ascending : this.ascending,
                fn
            });
        }
    }

    doDestroy() {
        super.doDestroy();
    }

    doDisable(disable) {
        const { store } = this;

        // Grouping mostly happens in store, need to clear groupers there to remove headers.
        // Use configured groupers as first sorters to somewhat maintain the order
        if (disable && store.isGrouped) {
            const { sorters } = store;
            sorters.unshift(...store.groupers);
            this.currentGroupers = store.groupers;
            store.clearGroupers();
            store.sort(sorters);
        }
        else if (!disable && this.currentGroupers) {
            store.group(this.currentGroupers[0]);
            this.currentGroupers = null;
        }

        super.doDisable(disable);
    }

    get store() {
        return this.grid.store;
    }

    //endregion

    //region Plugin config

    // Plugin configuration. This plugin chains some of the functions in Grid.
    static get pluginConfig() {
        return {
            assign : ['collapseAll', 'expandAll'],
            chain  : ['renderHeader', 'populateHeaderMenu', 'getColumnDragToolbarItems', 'onElementTouchStart',
                'onElementClick', 'bindStore']
        };
    }

    //endregion

    //region Expand/collapse

    refreshGrid(groupRecord) {
        const { store, rowManager } = this.grid;

        // If collapsing the group reduces amount of records below amount of rendered rows, we need to refresh
        // entire view
        // https://github.com/bryntum/support/issues/5893
        if (rowManager.rowCount > store.count || !rowManager.getRowFor(groupRecord)) {
            rowManager.renderFromRow();
        }
        else {
            // render from group record and down, no need to touch those above
            rowManager.renderFromRecord(groupRecord);
        }
    }

    /**
     * Collapses or expands a group depending on its current state
     * @param {Core.data.Model|String} recordOrId Record or records id for a group row to collapse or expand
     * @param {Boolean} collapse Force collapse (`true`) or expand (`false`)
     * @fires togglegroup
     */
    toggleCollapse(recordOrId, collapse) {
        this.internalToggleCollapse(recordOrId, collapse);
    }

    /**
     * Collapses or expands a group depending on its current state
     * @param {Core.data.Model|String} recordOrId Record or records id for a group row to collapse or expand
     * @param {Boolean} collapse Force collapse (true) or expand (true)
     * @param {Boolean} skipRender True to not render rows
     * @param {Event} domEvent The user interaction event (eg a `click` event) if the toggle request was
     * instigated by user interaction.
     * @internal
     * @fires togglegroup
     */
    internalToggleCollapse(recordOrId, collapse, skipRender = false, domEvent) {


        const
            me              = this,
            { store, grid } = me,
            groupRecord     = store.getById(recordOrId);

        if (!groupRecord.isGroupHeader) {
            return;
        }

        collapse = collapse === undefined ? !groupRecord.meta.collapsed : collapse;

        /**
         * Fired when a group is going to be expanded or collapsed using the UI.
         * Returning `false` from a listener prevents the operation
         * @event beforeToggleGroup
         * @on-owner
         * @preventable
         * @param {Core.data.Model} groupRecord Group record
         * @param {Boolean} collapse Collapsed (true) or expanded (false)
         * @param {Event} domEvent The user interaction event (eg a `click` event) if the toggle request was
         * instigated by user interaction.
         */
        if (grid.trigger('beforeToggleGroup', { groupRecord, collapse, domEvent }) === false) {
            return;
        }

        me.isToggling = true;

        if (collapse) {
            store.collapse(groupRecord);
        }
        else {
            store.expand(groupRecord);
        }

        me.isToggling = false;

        if (!skipRender) {
            me.refreshGrid(groupRecord);
        }

        /**
         * Group expanded or collapsed
         * @event toggleGroup
         * @on-owner
         * @param {Core.data.Model} groupRecord Group record
         * @param {Boolean} collapse Collapsed (true) or expanded (false)
         */
        grid.trigger('toggleGroup', { groupRecord, collapse });
        grid.afterToggleGroup();
    }

    /**
     * Collapse all groups. This function is exposed on Grid and can thus be called as `grid.collapseAll()`
     * @on-owner
     */
    collapseAll() {
        const me = this;
        if (me.store.isGrouped && !me.disabled) {
            me.store.groupRecords.forEach(r => me.internalToggleCollapse(r, true, true));
            me.grid.refreshRows(true);
        }
    }

    /**
     * Expand all groups. This function is exposed on Grid and can thus be called as `grid.expandAll()`
     * @on-owner
     */
    expandAll() {
        const me = this;
        if (me.store.isGrouped && !me.disabled) {
            me.store.groupRecords.forEach(r => me.internalToggleCollapse(r, false, true));
            me.grid.refreshRows();
        }
    }

    //endregion

    //region Rendering

    /**
     * Called before rendering row contents, used to reset rows no longer used as group rows
     * @private
     */
    onBeforeRenderRow({ row }) {
        // row.id contains previous record id on before render
        const oldRecord    = row.grid.store.getById(row.id);
        // force update of inner html if this row used for group data
        row.forceInnerHTML = row.forceInnerHTML || oldRecord?.isGroupHeader;
    }

    /**
     * Called when a cell is rendered, styles the group rows first cell.
     * @private
     */
    renderCell(renderData) {
        const
            me         = this,
            {
                cellElement,
                row,
                column,
                grid
            }          = renderData,
            { meta }   = renderData.record,
            rowClasses = {
                'b-group-row'            : 0,
                'b-grid-group-collapsed' : 0
            };

        if (!me.disabled && me.store.isGrouped && 'groupRowFor' in meta) {
            // do nothing with action column to make possible using actions for groups
            if (column.type === 'action') {
                return;
            }
            // let column clear the cell, in case it needs to do some cleanup
            column.clearCell(cellElement);

            // this is a group row, add css classes
            rowClasses['b-grid-group-collapsed'] = meta.collapsed;
            rowClasses['b-group-row']            = 1;

            if (grid.buildGroupHeader) {
                grid.buildGroupHeader(renderData);
            }
            else {
                me.buildGroupHeader(renderData);
            }

            if (column === me.groupHeaderColumn) {
                DomHelper.createElement({
                    parent      : cellElement,
                    tag         : 'i',
                    className   : 'b-group-state-icon',
                    nextSibling : cellElement.firstChild
                });
                cellElement.classList.add('b-group-title');
                cellElement.$groupHeader = cellElement._hasHtml = true;
            }
        }
        else if (cellElement.$groupHeader) {
            cellElement.querySelector('.b-group-state-icon')?.remove();
            cellElement.classList.remove('b-group-title');
            cellElement.$groupHeader = false;
        }

        // Still need to sync row classes is disabled or not grouped.
        // Previous b-group-row and b-grid-group-collapsed classes must be removed.
        row.assignCls(rowClasses);
    }

    // renderData.cellElement is required
    buildGroupHeader(renderData) {
        const
            me               = this,
            {
                record,
                cellElement,
                column,
                persist
            }                = renderData,
            { grid }         = me,
            meta             = record.meta,
            { groupRowFor }  = meta,
            { groupSummary } = grid.features,
            // Need to adjust count if group summary is used

            count            = meta.childCount - (groupSummary && groupSummary.target !== 'header' ? 1 : 0);

        let html         = null,
            applyDefault = true;

        if (persist || column) {
            const
                groupColumn         = grid.columns.get(meta.groupField),
                isGroupHeaderColumn = renderData.isFirstColumn = column === me.groupHeaderColumn;
            // First try using columns groupRenderer (might not even have a column if grouping programmatically)
            if (groupColumn?.groupRenderer) {
                if (isGroupHeaderColumn) {
                    // groupRenderer could return nothing and just apply changes directly to DOM element
                    html = groupColumn.groupRenderer({
                        ...renderData,
                        groupRowFor,
                        groupRecords : record.groupChildren,
                        groupColumn,
                        count
                    });

                    applyDefault = false;
                }
            }
            // Secondly use features groupRenderer, if configured with one
            else if (me.groupRenderer) {
                // groupRenderer could return nothing and just apply changes directly to DOM element
                html = me.groupRenderer({
                    ...renderData,
                    groupRowFor,
                    groupRecords  : record.groupChildren,
                    groupColumn,
                    count,
                    isFirstColumn : isGroupHeaderColumn
                });
            }

            // Third, just display unformatted value and child count (also applied for features groupRenderer that do
            // not output any html of their own)
            if (isGroupHeaderColumn && html == null && applyDefault && DomHelper.getChildElementCount(cellElement) === 0) {
                html = StringHelper.encodeHtml(`${groupRowFor === '__novalue__' ? '' : groupRowFor} (${count})`);
            }
        }
        else if (me.groupRenderer) {
            // groupRenderer could return nothing and just apply changes directly to DOM element
            html = me.groupRenderer(renderData);
        }

        // Renderers could return nothing and just apply changes directly to DOM element
        if (typeof html === 'string') {
            cellElement.innerHTML = html;
        }
        else if (typeof html === 'object') {
            DomSync.sync({
                targetElement : cellElement,
                domConfig     : {
                    onlyChildren : true,
                    children     : ArrayHelper.asArray(html)
                }
            });
        }

        // If groupRenderer added elements to the cell, we need to remember that to clear it on re-usage as a normal cell
        if (DomHelper.getChildElementCount(cellElement) > 0) {
            cellElement._hasHtml = true;
        }

        return cellElement.innerHTML;
    }

    get groupHeaderColumn() {
        return this.grid.columns.visibleColumns.find(column => !column.groupHeaderReserved);
    }

    /**
     * Called when a header is rendered, adds grouping icon if grouped by that column.
     * @private
     * @param headerContainerElement
     */
    renderHeader(headerContainerElement) {
        const { store, grid } = this;

        if (store.isGrouped) {
            // Sorted from start, reflect in rendering
            for (const groupInfo of store.groupers) {
                // Might be grouping by field without column, which is valid
                const
                    column = grid.columns.get(groupInfo.field),
                    header = column && grid.getHeaderElement(column.id);

                header?.classList.add('b-group', groupInfo.ascending ? 'b-asc' : 'b-desc');

                // If sort feature is active, it provides the icon - if not we add it here
                if (!grid.features.sort || grid.features.sort.disabled) {
                    const textEl = column.textWrapper;

                    if (!textEl?.querySelector('.b-sort-icon')) {
                        DomHelper.createElement({
                            parent    : textEl,
                            className : 'b-sort-icon'
                        });
                    }
                }
            }
        }
    }

    //endregion

    //region Context menu

    /**
     * Supply items for headers context menu.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateHeaderMenu({ column, items }) {
        const me = this;

        if (column.groupable !== false) {

            items.groupAsc = {
                text        : 'L{groupAscending}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-group-asc',
                cls         : 'b-separator',
                weight      : 400,
                disabled    : me.disabled,
                onItem      : () => me.store.group(column.field, true)
            };

            items.groupDesc = {
                text        : 'L{groupDescending}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-group-desc',
                weight      : 410,
                disabled    : me.disabled,
                onItem      : () => me.store.group(column.field, false)
            };
        }

        if (me.store.isGrouped) {
            items.groupRemove = {
                text        : 'L{stopGrouping}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-clear',
                cls         : column.groupable ? '' : 'b-separator',
                weight      : 420,
                disabled    : me.disabled,
                onItem      : () => me.store.clearGroupers()
            };
        }
    }

    /**
     * Supply items to ColumnDragToolbar
     * @private
     */
    getColumnDragToolbarItems(column, items) {
        const
            me                  = this,
            { store, disabled } = me;

        items.push({
            text        : 'L{groupAscendingShort}',
            group       : 'L{group}',
            localeClass : me,
            icon        : 'b-icon b-icon-group-asc',
            ref         : 'groupAsc',
            cls         : 'b-separator',
            weight      : 110,
            disabled,
            onDrop      : ({ column }) => store.group(column.field, true)
        });

        items.push({
            text        : 'L{groupDescendingShort}',
            group       : 'L{group}',
            localeClass : me,
            icon        : 'b-icon b-icon-group-desc',
            ref         : 'groupDesc',
            weight      : 110,
            disabled,
            onDrop      : ({ column }) => store.group(column.field, false)
        });

        const grouped = store.groupers?.some(col => col.field === column.field) && !disabled;
        items.push({
            text        : 'L{stopGroupingShort}',
            group       : 'L{group}',
            localeClass : me,
            icon        : 'b-icon b-icon-clear',
            ref         : 'groupRemove',
            disabled    : !grouped,
            weight      : 110,
            onDrop      : ({ column }) => store.removeGrouper(column.field)
        });

        return items;
    }

    //endregion

    //region Events - Store

    /**
     * Called when store grouping changes. Reflects on header and rerenders rows.
     * @private
     */
    onStoreGroup({ groupers }) {
        const
            { grid }        = this,
            { element }     = grid,
            curGroupHeaders = element && DomHelper.children(element, '.b-grid-header.b-group');

        if (element) {
            for (const header of curGroupHeaders) {
                header.classList.remove('b-group', 'b-asc', 'b-desc');
            }

            if (groupers) {
                for (const groupInfo of groupers) {
                    const header = grid.getHeaderElementByField(groupInfo.field);
                    if (header) {
                        header.classList.add('b-group', groupInfo.ascending ? 'b-asc' : 'b-desc');
                    }
                }
            }
        }
    }

    onStoreChange({ action, records }) {
        const
            { client }            = this,
            { rowManager, store } = client;

        if (store.isGrouped && action === 'move') {
            const
                { field } = store.groupers[0],
                fromRow   = Math.min(...records.reduce((result, record) => {
                    // Get index of the new group
                    result.push(store.indexOf(record.instanceMeta(store).groupParent));

                    // Get index of the old group
                    if (field in record.meta.modified) {
                        const oldGroup = store.groupRecords.find(r => r.meta.groupRowFor === record.meta.modified[field]);

                        if (oldGroup) {
                            result.push(store.indexOf(oldGroup));
                        }
                    }

                    return result;
                }, []));

            rowManager.renderFromRow(rowManager.getRow(fromRow));
        }
    }

    // React to programmatic expand/collapse
    onStoreToggleGroup({ groupRecord }) {
        if (!this.isToggling) {
            this.refreshGrid(groupRecord);
        }
    }

    //endregion

    //region Events - Grid

    /**
     * Store touches when user touches header, used in onElementTouchEnd.
     * @private
     */
    onElementTouchStart(event) {
        const
            me         = this,
            { target } = event,
            header     = target.closest('.b-grid-header'),
            column     = header && me.grid.getColumnFromElement(header);

        // If it's a multi touch, group.
        if (event.touches.length > 1 && column && column.groupable !== false && !me.disabled) {
            me.store.group(column.field);
        }
    }

    /**
     * React to click on headers (to group by that column if [alt] is pressed) and on group rows (expand/collapse).
     * @private
     * @param event
     * @returns {Boolean}
     */
    onElementClick(event) {
        const
            me         = this,
            { store }  = me,
            { target } = event,
            row        = target.closest('.b-group-row'),
            header     = target.closest('.b-grid-header'),
            field      = header?.dataset.column;

        // prevent expand/collapse if disabled or clicked on item with own handler
        if (
            target.classList.contains('b-resizer') ||
            me.disabled ||
            target.classList.contains('b-action-item') ||
            event.handled
        ) {
            return;
        }

        // Header
        if (header && field) {
            const columnGrouper = store.groupers?.find(g => g.field === field);

            // Store has a grouper for this column's field; flip grouper order
            if (columnGrouper && !event.shiftKey) {
                columnGrouper.ascending = !columnGrouper.ascending;
                store.group();
                return false;
            }
            // Group or ungroup
            else if (event.shiftKey) {
                const column = me.grid.columns.get(field);

                if (column.groupable !== false) {
                    if (event.altKey) {
                        store.removeGrouper(field);
                    }
                    else {
                        store.group(field);
                    }
                }
            }
        }

        // Anywhere on group-row if toggleOnRowClick set, otherwise only on icon
        if (row && (me.toggleOnRowClick || event.target.classList.contains('b-group-state-icon'))) {
            me.internalToggleCollapse(DomDataStore.get(row).id, undefined, undefined, event);
            return false;
        }
    }

    /**
     * Toggle groups with [space].
     * @private
     * @param event
     */
    toggleGroup(event) {
        const
            { grid }        = this,
            { focusedCell } = grid;

        // only catch space when focus is on a group header cell
        if (!this.disabled && !focusedCell.isActionable && focusedCell.record?.isGroupHeader) {
            this.internalToggleCollapse(focusedCell.id);

            // Other features (like context menu) must not process this.
            return true;
        }
        return false;
    }

    //endregion
}

GridFeatureManager.registerFeature(Group, true, ['Grid', 'Scheduler']);
GridFeatureManager.registerFeature(Group, false, ['TreeGrid']);
