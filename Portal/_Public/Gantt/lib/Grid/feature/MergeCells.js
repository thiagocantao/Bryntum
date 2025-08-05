import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from './GridFeatureManager.js';
import DomClassList from '../../Core/helper/util/DomClassList.js';

/**
 * @module Grid/feature/MergeCells
 */

// Maps DOM events to relayed events that need correct casing
const camelCase = {
    mousedown   : 'mouseDown',
    mousemove   : 'mouseMove',
    mouseup     : 'mouseUp',
    touchdown   : 'touchDown',
    touchmove   : 'touchMove',
    touchup     : 'touchUp',
    pointerover : 'mouseOver',
    mouseout    : 'mouseOut',
    dblclick    : 'dblClick',
    keydown     : 'keyDown',
    keypress    : 'keyPress',
    keyup       : 'keyUp',
    contextmenu : 'contextMenu'
};

/**
 * This feature merges cells that have the same value in sorted (or {@link #config-sortedOnly optionally} any) columns
 * configured to {@link Grid/column/Column#config-mergeCells}.
 *
 * The content of merged cells is sticky for Grids with a single subgrid section when all columns fit in view (content
 * stays in view until the cell is scrolled fully out of view).
 *
 * {@note}
 * Support for sticky content is limited because of how <code>position: sticky</code> works. Grid
 * scrolls vertically in one element, and horizontally in another (to support multiple regions in the grid), and this
 * setup is not supported by current browsers implementation of sticky positioning
 * {/@note}
 *
 * Try scrolling in the demo below. As mentioned above, cells are by default merged only in sorted columns -  try
 * sorting by the other columns ("City" and "Favorite food" are configured to merge cells):
 *
 * {@inlineexample Grid/feature/MergeCells.js}
 *
 * By configuring the feature with `sortedOnly : false`, cells can be merged in any column:
 *
 * {@inlineexample Grid/feature/MergeCellsAll.js}
 *
 * This feature is **disabled** by default.
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype mergeCells
 * @feature
 */
export default class MergeCells extends InstancePlugin {
    //region Config

    static $name = 'MergeCells';

    static configurable = {
        /**
         * By default, merged cells allow pointer events to pass through to the underlying row/cell, to allow selecting
         * a row and editing an individual cell even when they are merged. Configure as `false` to allow merged cells to
         * catch and react to the pointer events instead.
         *
         * ```javascript
         * const grid = new Grid({
         *     features : {
         *         mergeCells : {
         *             // Let merged cells react to pointer events
         *             passthrough : false
         *         }
         *     }
         * });
         * ```
         *
         * @config {Boolean}
         */
        passthrough : true,

        /**
         * Configure as `false` to allow merging cells in columns that are not sorted.
         *
         * {@note}
         * Note that this will have a slight negative impact on performance, since cells in all columns configured to
         * merge cells have to be iterated.
         * {/@note}
         *
         * @config {Boolean}
         * @default true
         */
        sortedOnly : true
    };

    // All current ranges (once rendered)
    mergedRanges = [];
    // Ranges indexed by row index & column id -> Each range included multiple times, for easy lookup
    mergedMap    = {};

    static get pluginConfig() {
        return {
            chain : [
                'beforeRenderCell',
                'afterRenderRow',
                'bindStore',
                'afterColumnsChange',
                'afterRemove',
                'afterToggleGroup',
                'afterToggleSubGrid',
                'handleEvent',
                'populateHeaderMenu',
                // 'setHoveredRow'
                'afterSelectionChange'
            ],

            // Grid must perform its update *after* we do.
            before : [
                'onInternalResize'
            ],
            override : [
                'getColumnFromElement',
                'getRecordFromElement'
            ]
        };
    };

    //endregion

    //region Init

    afterConstruct() {
        const
            me         = this,
            { client } = me;

        // Each subgrid gets a merged cells container
        client.eachSubGrid(subGrid => me.setupSubGrid(subGrid));

        // Merged cells are synced after rows are updated
        client.rowManager.ion({
            renderDone : 'onRenderDone',
            thisObj    : me
        });

        me.bindStore(client.store);
    }

    setupSubGrid(subGrid) {
        // Element that will contain the merged cells
        subGrid.$mergedCellsElement = DomHelper.createElement({
            parent    : subGrid.element,
            className : {
                'b-grid-merged-cells-container' : 1
            }
        });

        // Catch resizing region
        subGrid.ion({
            beforeInternalResize : 'refreshBounds',
            thisObj              : this
        });
    }

    doDisable(disable) {
        if (!this.isConfiguring) {
            // Flag to allow reset to redraw even though we are already disabled at this stage
            this.isDisabling = true;
            this.reset();
            this.isDisabling = false;
        }

        super.doDisable(disable);
    }

    updatePassthrough(use) {
        // Toggle CSS class that has `pointer-events : none`
        this.client.element.classList.toggle('b-mergecells-passthrough', use);
    }

    updateSortedOnly(sortedOnly) {
        if (!this.isConfiguring) {
            this.reset(true, sortedOnly);
        }
    }

    //endregion

    //region Grid hooks

    bindStore(store) {
        this.detachListeners('store');

        store.ion({
            name    : 'store',
            change  : 'onStoreChange',
            // Call our refresh logic before grids
            refresh : {
                prio : 1,
                fn   : 'onStoreRefresh'
            },
            thisObj : this
        });
    }

    // Refresh all ranges when grid is resized
    onInternalResize() {
        this.refreshBounds();
    }

    // Get / create ranges before cell contents are rendered, redirecting the contents to the range
    beforeRenderCell(renderData) {
        const
            me                              = this,
            { column, record, cellElement } = renderData,
            subGrid                         = me.client.subGrids[column.region];

        // Only affect sorted columns configured to merge cells
        if (!me.disabled && column.mergeCells && (column.isSorted || !me.sortedOnly) && !record.isSpecialRow && !subGrid.collapsed) {
            const mergedRange = me.getMergeRange(record, column);
            if (mergedRange?.use) {
                // Flag the unmerged cell to allow styling it
                cellElement.classList.add('b-merged-cell');
                // Make sure it is empty
                cellElement.innerHTML = '';
                // Redirect output to the merged cell
                renderData.cellElement = mergedRange.cellElement;


            }
        }
    }

    afterRenderRow({ row, oldId, oldHeight }) {
        // Row reused for same record changed height
        if (oldId === row.id && oldHeight !== row.height) {
            this.heightChanged = true;
        }
    }

    // Refresh all ranges when a column is resized (since it might affect their position and size)
    afterColumnsChange({ action, changes, column }) {
        if (this.disabled) {
            return;
        }

        if (action === 'update' && changes.mergeCells && !column.mergeCells) {
            // Remove all ranges for the column when mergeCells is set to false
            for (const range of this.mergedRanges.slice()) {
                if (range.column === column) {
                    this.removeRange(range);
                }
            }

            this.syncDom();
            return;
        }

        this.refreshBounds();
    }

    // Remove might be transitioned, wait until it finishes before resetting (in the afterRemove hook)
    afterRemove() {
        !this.disabled && this.reset();
    }

    // Group collapsed or expanded
    afterToggleGroup() {
        !this.disabled && this.reset();
    }

    // SubGrid collapsed or expanded
    afterToggleSubGrid() {
        !this.disabled && this.reset();
    }

    // setHoveredRow(row) {
    //     this.$hovered = row && this.mergedRanges.find(r => r.fromIndex <= row.dataIndex && r.toIndex >= row.dataIndex);
    //
    //     this.syncDom();
    // }

    // Selection changed, range is considered selected when all of its rows are selected (only in passthrough mode)
    afterSelectionChange() {
        if (!this.passthrough) {
            const
                { client } = this,
                indices    = client.selectedRecords.map(r => client.store.indexOf(r));
            let changed    = false;

            for (const range of this.mergedRanges) {
                let allSelected = true;
                // Check if all records in the range are selected
                for (let i = range.fromIndex; i <= range.toIndex && allSelected; i++) {
                    allSelected = indices.includes(i);
                }

                if (range.isSelected !== allSelected) {
                    range.isSelected = allSelected;
                    changed = true;
                }
            }

            // Only redraw if a range selection status changed
            changed && this.syncDom();
        }
    }

    //endregion

    //region Grid overrides

    // Extract record from merged cells
    getRecordFromElement(element) {
        if (element.elementData?.range) {
            return this.client.store.getAt(element.elementData.range.fromIndex);
        }

        return this.overridden.getRecordFromElement(element);
    }

    // Extract column from merged cells
    getColumnFromElement(element) {
        if (element.elementData?.range) {
            return element.elementData.range.column;
        }

        return this.overridden.getColumnFromElement(element);
    }

    //endregion

    //region Header menu

    // Allow toggling merging cells from the column header menu (unless column is explicitly disallowing it)
    populateHeaderMenu({ column, items }) {
        if (column.mergeable !== false) {
            items.mergeCells = {
                text     : 'L{MergeCells.mergeCells}',
                icon     : `b-fw-icon ${column.mergeCells ? 'b-icon-checked' : 'b-icon-unchecked'}`,
                tooltip  : 'L{MergeCells.menuTooltip}',
                cls      : 'b-separator',
                weight   : 500,
                disabled : this.disabled,
                onItem   : () => column.mergeCells = !column.mergeCells
            };
        }
    }

    //endregion

    //region Relaying events

    // Relay pointer events from the merged cell on grid
    async handleEvent(event) {
        if (!this.passthrough) {
            const mergedCellsElement = event.target.closest('.b-grid-merged-cells');

            if (mergedCellsElement) {
                const
                    { client }   = this,
                    { range }    = mergedCellsElement.elementData,
                    { cellEdit } = client.features,
                    { column }   = range,
                    type         = StringHelper.capitalize(camelCase[event.type] ?? event.type),
                    eventData    = {
                        grid        : client,
                        records     : [],
                        column,
                        cellElement : mergedCellsElement.firstElementChild,
                        target      : event.target,
                        event
                    };

                for (let i = range.fromIndex; i <= range.toIndex; i++) {
                    eventData.records.push(client.store.getAt(i));
                }

                client.trigger(`mergedCell${type}`, eventData);

                // Cell editing, pass through to cell underneath even when not using passthrough mode
                if (cellEdit?.triggerEvent.toLowerCase() === `cell${type}`.toLowerCase()) {
                    const row = client.rowManager.getRowAt(event.clientY);
                    await cellEdit.startEditing({ id : row.id, columnId : column.id });
                }
                // Select ranges rows when clicking the merged cell
                else if (event.type === 'click') {
                    this.onRangeClick({ range });
                }
            }
        }
    }

    //endregion

    //region Event listeners

    // Select ranges rows when clicking a merged cell
    onRangeClick({ range }) {
        const records = [];

        for (let i = range.fromIndex; i <= range.toIndex; i++) {
            records.push(this.client.store.getAt(i));
        }

        this.client.selectedRecords = records;
    }

    // Reset when data changes
    onStoreChange() {
        !this.disabled && this.reset();
    }

    // Reset before grids refresh listener come into play, grid will redraw
    onStoreRefresh({ action }) {
        // filter & dataset triggers change too, handled above
        if (action !== 'filter' && action !== 'dataset') {
            !this.disabled && this.reset(false);
        }
    }

    //endregion

    //region Render

    // Element is needed early since it is passed to column renderers, and syncing for each rendered cell would give a
    // lot of overhead. Thus creating element directly instead
    createRangeElement(range) {
        const
            parent = this.client.getSubGridFromColumn(range.column).$mergedCellsElement,
            syncId = `${range.fromIndex}-@-${range.column.id}`;

        // If there is already an element for the range (also if previously released), we grab that one.
        // We are bending the rules of DomSync here by spawning the element early
        let element = parent.syncIdMap?.[syncId] ?? parent.releasedIdMap?.[syncId];

        // No longer considered released
        if (parent.releasedIdMap) {
            delete parent.releasedIdMap[syncId];
        }

        if (!element) {
            element = DomHelper.createElement(this.createRangeDomConfig(range));
        }

        // New element or reusing released one, DomSync need to be made aware
        DomSync.addChild(parent, element, syncId);

        range.element = element;
        range.cellElement = element.firstElementChild;

        return element;
    }

    // Create a DomConfig object for the supplied range, used initially with DomHelper and thereafter with DomSync
    createRangeDomConfig(range) {
        const
            { column, fromIndex, toIndex, top, left, width, height } = range,
            { type } = column,
            record = this.client.store.getAt(fromIndex),
            domConfig = {
                className : {
                    'b-grid-merged-cells' : 1,
                    // 'b-hover'             : this.$hovered === range,
                    'b-selected'          : range.isSelected
                },
                elementData : {
                    range
                },
                style : {
                    top,
                    left,
                    height,
                    width
                },
                dataset : {
                    syncId   : `${fromIndex}-@-${column.id}`,
                    fromIndex,
                    toIndex,
                    column   : column.field,
                    columnId : column.id
                },
                children : [
                    // Div for an actual cell
                    {
                        className : new DomClassList(this.client.cellCls).assign({
                            [`b-${type?.toLowerCase()}-cell`] : type,
                            [column.cellCls]                  : column.cellCls,
                            [column.internalCellCls]          : column.internalCellCls
                        }).assign(column.autoCls),
                        // Don't touch cell children that might have been added by renderer
                        keepChildren : true,
                        // Tag along the range for easy resolving later
                        elementData  : {
                            range
                        },
                        // Dataset expected on cells
                        dataset : {
                            column   : column.field,
                            columnId : column.id
                        },
                        // Size using configured row height, to at least have a chance of lining up with surrounding cells
                        style : {
                            height : this.client.rowManager.rowHeight
                        }
                    }
                ]
            };

        // Allow application code a chance to affect the merged cell (intended for styling)
        column.mergedRenderer?.({ domConfig, value : column.getRawValue(record), record, column, fromIndex, toIndex });

        return domConfig;
    }

    buildMergedCellsConfig(fromIndex, toIndex, rows) {
        const
            me             = this,
            { client }     = me,
            { rowManager } = client;

        // When exporting content we render rows to a document fragment one by one without RowManager. In
        // order to draw ranges correctly in this case we need provide some meta information - list of row-like
        // objects from which we can size for merged cell element, i.e. top, bottom, height, dataIndex
        // Match grids row buffer, drawing ranges from the topmost row to the bottommost
        fromIndex = fromIndex ?? rowManager.topRow?.dataIndex;
        toIndex = toIndex ?? rowManager.bottomRow?.dataIndex;

        const
            // { locked = [], normal = [] }
            domConfigMap = client.regions.reduce((map, region) => {
                map[region] = {
                    className : {
                        'b-grid-merged-cells-container' : 1
                    },
                    children : []
                };
                return map;
            }, {});

        // Determine and update visible ranges
        for (const range of me.mergedRanges) {
            if (range.use && range.fromIndex <= toIndex && range.toIndex >= fromIndex) {
                // Update the range, in case more of it has been scrolled into view
                me.updateRange(range, me.heightChanged, rows);
                // Create a DomConfig for it
                domConfigMap[range.column.region].children.push(me.createRangeDomConfig(range));
            }
        }

        return domConfigMap;
    }

    syncDom(force = false) {
        const
            me             = this,
            { client }     = me,
            { rowManager } = client;

        // Bail out if we get here too early (happens in Scheduler) or are disabled
        if (!force && !rowManager.topRow || me.disabled && !me.isDisabling) {
            return;
        }

        const domConfigMap = this.buildMergedCellsConfig();

        // Sync per subgrid
        client.eachSubGrid(subGrid => {
            DomSync.sync({
                targetElement : subGrid.$mergedCellsElement,
                syncIdField   : 'syncId',
                domConfig     : domConfigMap[subGrid.region]
            });
        });

        me.heightChanged = false;
    }

    // Called after rows are updated, sync visible ranges per subgrid
    onRenderDone() {
        this.syncDom();
    }

    //endregion

    //region Ranges

    // Remove range from list and index/column map, leaving no trace it since we don't know if it will ever
    // come back (element will be released by DomSync)
    removeRange(range) {
        ArrayHelper.remove(this.mergedRanges, range);
        for (let index = range.fromIndex; index <= range.toIndex; index++) {
            delete this.mergedMap[`${index}-@-${range.column.id}`];
        }
    }

    // Full reset, regenerating all ranges
    reset(redraw = true, force = false) {
        const
            me        = this,
            columns   = me.client.columns.visibleColumns.filter(c => c.mergeCells && (c.isSorted || !me.sortedOnly || force), true),
            hadRanges = me.mergedRanges.length;

        me.mergedRanges = [];
        me.mergedMap = {};

        if (redraw && !me.client.refreshSuspended) {
            for (const row of me.client.rowManager.rows) {
                for (const column of columns) {
                    row.renderCell(row.getCell(column.id));
                }
            }

            // Redraw, forcing it if all rows are gone
            me.syncDom(hadRanges && !me.client.rowManager.rowCount);
        }
    }

    // Refreshes existing ranges coords
    refreshBounds() {
        const
            { mergedRanges } = this,
            row              = this.client.rowManager.topRow,
            boundsMap        = new Map();

        for (const range of mergedRanges.slice()) {
            const
                { column }  = range,
                cellElement = row.getCell(column.id);

            // Column shown
            if (cellElement) {
                let bounds = boundsMap.get(column);

                if (!bounds) {
                    // Temporarily cache bounds to avoid calculating them multiple times
                    boundsMap.set(column, bounds = Rectangle.from(cellElement, cellElement.parentElement));
                }

                range.left = bounds.left;
                range.width = bounds.width;
            }
            // Column hidden or removed
            else {
                this.removeRange(range);
            }
        }

        this.syncDom();
    }

    // Updates the range as user scrolls, until its start and end coords are fully known
    // Rows argument is required to render merged cells on export. We provide row-like objects there
    updateRange(range, force, rows = null) {
        const
            { store, rowManager }           = this.client,
            { topRendered, bottomRendered } = range,
            { rowOffsetHeight }             = rowManager;

        let { fromIndex, toIndex } = range,
            topRow, bottomRow;

        force = rows ? rows.length > 0 : force;

        // No need to update anything if bounds are correct already
        if (!force && topRendered && bottomRendered) {
            return;
        }

        if (!topRendered || force) {
            if (rows) {
                const index = rows.findIndex(row => row.dataIndex >= fromIndex);
                topRow = rows[index];
                fromIndex = topRow.dataIndex;
            }
            else {

                do {
                    topRow = rowManager.getRowById(store.getAt(fromIndex));
                }
                while (!topRow && fromIndex++ < toIndex);
            }

            // topRow is the actual topmost row in the range
            range.topRendered = (fromIndex === range.fromIndex);

            const rowsAbove = fromIndex - range.fromIndex;

            range.top = topRow.top - rowsAbove * (rows ? topRow.offsetHeight : rowOffsetHeight);
        }

        if (!bottomRendered || force) {
            if (rows) {
                // We need to find last index, so we can just pick max between -1 and whatever we find
                const index = rows.findIndex(row => row.dataIndex === toIndex);
                bottomRow = rows[index === -1 ? rows.length - 1 : index];
                fromIndex = bottomRow.dataIndex;
            }
            else {

                do {
                    bottomRow = rowManager.getRowById(store.getAt(toIndex));
                }
                while (!bottomRow && toIndex-- > fromIndex);
            }

            // bottomRow is the actual bottommost row in the range
            range.bottomRendered = (toIndex === range.toIndex);

            const rowsBelow = range.toIndex - toIndex;

            range.bottom = bottomRow.bottom + rowsBelow * (rows ? bottomRow.offsetHeight : rowOffsetHeight);
        }

        range.height = range.bottom - range.top;
    }

    // Retrieve a range for the specified record / column, creating a new one if none found by walking upwards and
    // downwards until a deviating value is found
    getMergeRange(record, column) {
        if (record.isSpecialRow) {
            return;
        }

        const
            me            = this,
            { mergedMap } = me,
            { store }     = me.client,
            row           = me.client.rowManager.topRow,
            columnId      = column.id,
            index         = store.indexOf(record),
            key           = `${index}-@-${columnId}`;

        let range = mergedMap[key];

        if (!range) {
            const value = column.getRawValue(record);

            range = mergedMap[key] = {
                column
            };

            me.mergedRanges.push(range);

            // Search up until encountering different value
            let earlierRecord, earlierValue, earlierIndex = index;

            do {
                earlierRecord = store.getAt(--earlierIndex);
                earlierValue  = earlierRecord && column.getRawValue(earlierRecord);

                if (earlierValue === value) {
                    mergedMap[`${earlierIndex}-@-${columnId}`] = range;
                }

            } while (earlierRecord && earlierValue === value);

            // And down
            let laterRecord, laterValue, laterIndex = index;

            do {
                laterRecord = store.getAt(++laterIndex);
                laterValue  = laterRecord && column.getRawValue(laterRecord);

                if (laterValue === value) {
                    mergedMap[`${laterIndex}-@-${columnId}`] = range;
                }
            } while (laterRecord && laterValue === value);

            range.fromIndex = earlierIndex + 1;
            range.toIndex   = laterIndex - 1;

            // We only care about ranges longer than 1 record
            if (range.toIndex - range.fromIndex > 0) {
                const
                    cellElement = row.getCell(columnId),
                    cellBounds  = Rectangle.from(cellElement, cellElement.parentElement);

                range.left  = cellBounds.left;
                range.width = cellBounds.width;
                range.use = true;
            }
        }

        if (!range.element && range.use) {
            me.createRangeElement(range);
        }

        return range;
    }

    //endregion
}

GridFeatureManager.registerFeature(MergeCells);
