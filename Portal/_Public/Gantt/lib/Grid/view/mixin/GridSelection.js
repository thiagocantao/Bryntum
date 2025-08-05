import Base from '../../../Core/Base.js';
import GlobalEvents from '../../../Core/GlobalEvents.js';
import ArrayHelper from '../../../Core/helper/ArrayHelper.js';
import EventHelper from '../../../Core/helper/EventHelper.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import Collection from '../../../Core/util/Collection.js';
import ColumnStore from '../../data/ColumnStore.js';
import Location from '../../util/Location.js';

import '../../column/RowNumberColumn.js';

const
    validIdTypes   = {
        string : 1,
        number : 1
    },
    isDataLoadAction = {
        dataset : 1,
        batch   : 1
    };

/**
 * @module Grid/view/mixin/GridSelection
 */

/**
 * A mixin for Grid that handles row and cell selection. See {@link #config-selectionMode} for details on how to control
 * what should be selected (rows or cells)
 *
 * @example
 * // select a row
 * grid.selectedRow = 7;
 *
 * // select a cell
 * grid.selectedCell = { id: 5, columnId: 'column1' }
 *
 * // select a record
 * grid.selectedRecord = grid.store.last;
 *
 * // select multiple records by ids
 * grid.selectedRecords = [1, 2, 4, 6]
 *
 * @mixin
 */
export default Target => class GridSelection extends (Target || Base) {
    static get $name() {
        return 'GridSelection';
    }

    static configurable =  {
        /**
         * The selection settings, where you can set these boolean flags to control what is selected. Options below:
         * @config {Object} selectionMode
         * @param {Boolean} selectionMode.cell Set to `true` to enable cell selection. This takes precedence over
         * row selection, but rows can still be selected programmatically or with checkbox or RowNumber selection.
         * Required for `column` selection
         * @param {Boolean} selectionMode.multiSelect Allow multiple selection with ctrl and shift+click or with
         * `checkbox` selection. Required for `dragSelect` and `column` selection
         * @param {Boolean|CheckColumnConfig} selectionMode.checkbox Set to `true` to add a checkbox selection column to
         * the grid, or pass a config object for the {@link Grid.column.CheckColumn}
         * @param {Number|String} selectionMode.checkboxIndex Positions the checkbox column at the provided index or to
         * the right of a provided column id. Defaults to 0 or to the right of an included `RowNumberColumn`
         * @param {Boolean} selectionMode.checkboxOnly Select rows only when clicking in the checkbox column. Requires
         * cell selection config to be `false` and checkbox to be set to `true`. This setting was previously named
         * `rowCheckboxSelection`
         * @param {Boolean} selectionMode.showCheckAll Set to `true` to add a checkbox to the selection column header to
         * select/deselect all rows. Requires checkbox to also be set to `true`
         * @param {Boolean} selectionMode.deselectFilteredOutRecords Set to `true` to deselect records when they are
         * filtered out
         * @param {Boolean|String} selectionMode.includeChildren Set to `true` to also select/deselect child nodes
         * when a parent node is selected by toggling the checkbox. Set to `always` to always select/deselect child
         * nodes.
         * @param {Boolean|'all'|'some'} selectionMode.includeParents Set to `all` or `true` to auto select
         * parent if all its children gets selected. If one gets deselected, the parent will also be deselected. Set to
         * 'some' to select parent if one of its children gets selected. The parent will be deselected if all children
         * gets deselected.
         * @param {Boolean} selectionMode.preserveSelectionOnPageChange In `row` selection mode, this flag controls
         * whether the Grid should preserve its selection when loading a new page of a paged data store. Defaults to
         * `false`
         * @param {Boolean} selectionMode.preserveSelectionOnDatasetChange In `row` selection mode, this flag
         * controls whether the Grid should preserve its selection of cells / rows when loading a new dataset
         * (assuming the selected records are included in the newly loaded dataset)
         * @param {Boolean} selectionMode.deselectOnClick Toggles whether the Grid should deselect a selected row or
         * cell when clicking it
         * @param {Boolean} selectionMode.dragSelect Set to `true` to enable multiple selection by dragging.
         * Requires `multiSelect` to also be set to `true`. Also requires the {@link Grid.feature.RowReorder} feature
         * to be set to {@link Grid.feature.RowReorder#config-gripOnly}.
         * @param {Boolean} selectionMode.selectOnKeyboardNavigation Set to `false` to disable auto-selection by keyboard
         * navigation. This will activate the `select` keyboard shortcut.
         * @param {Boolean} selectionMode.column Set to `true` to be able to select whole columns of cells by clicking the header.
         * Requires cell to be set to `true`
         * @param {Boolean|RowNumberColumnConfig} selectionMode.rowNumber Set to `true` or a config object to add a RowNumberColumn
         * which, when clicked, selects the row.
         * @param {Boolean} selectionMode.selectRecordOnCell Set to `false` not to include the record in the
         * `selectedRecords` array when one of the record row's cells is selected.
         * @default
         * @category Selection
         */
        selectionMode : {
            cell                             : false,
            multiSelect                      : true,
            checkboxOnly                     : false,
            checkbox                         : false,
            checkboxPosition                 : null,
            showCheckAll                     : false,
            deselectFilteredOutRecords       : false,
            includeChildren                  : false,
            includeParents                   : false,
            preserveSelectionOnPageChange    : false,
            preserveSelectionOnDatasetChange : true,
            deselectOnClick                  : false,
            dragSelect                       : false,
            selectOnKeyboardNavigation       : true,
            column                           : false,
            rowNumber                        : false,
            selectRecordOnCell               : true
        },

        keyMap : {
            'Shift+ArrowUp'    : 'extendSelectionUp',
            'Shift+ArrowDown'  : 'extendSelectionDown',
            'Shift+ArrowLeft'  : 'extendSelectionLeft',
            'Shift+ArrowRight' : 'extendSelectionRight',
            ' '                : { handler : 'toggleSelection', weight : 10 }
        },

        selectedRecordCollection : {}
    };

    construct(config) {
        this._selectedCells   = [];
        this._selectedRows    = [];

        super.construct(config);

        if (config?.selectedRecords) {
            this.selectedRecords = config.selectedRecords;
        }
    }

    //region Init

    getDefaultGridSelection(clas) {
        if (clas.$name === 'GridSelection') {
            return clas.configurable.selectionMode;
        }
        else if (clas.superclass) {
            return this.getDefaultGridSelection(clas.superclass);
        }
    }

    changeSelectionMode(mode) {
        const me = this;

        // If changing the selectionMode config object after creation
        if (me.selectionMode) {
            ObjectHelper.assign(me.selectionMode, mode);
            return me.selectionMode;
        }

        me.$defaultGridSelection = me.getDefaultGridSelection(me.constructor);

        // Wraps changeSelectionMode object in a proxy to monitor property changes.
        return new Proxy(mode, {
            set(obj, prop, value) {
                const old = ObjectHelper.assign({}, obj);
                obj[prop] = value;
                // Calls selectionMode's update method on property change
                me.updateSelectionMode(obj, old);
                return true;
            }
        });
    }

    /**
     * The selectionMode configuration has been changed.
     * @event selectionModeChange
     * @param {Object} selectionMode The new {@link #config-selectionMode}
     */

    // Will be called if selectionMode config object changes or if one of its properties changes
    updateSelectionMode(mode, oldMode = this.$defaultGridSelection) {
        const
            me             = this,
            {
                columns,
                checkboxSelectionColumn
            }              = me,
            changed        = {},
            { rowReorder } = me.features;

        for (const property in mode) {
            if (mode[property] != oldMode[property]) {
                changed[property] = mode[property];
            }
        }

        // Backwards compatibility. Remove on 7.X?
        if (mode.rowCheckboxSelection && !mode.checkboxOnly) {
            mode.checkboxOnly = true;
            delete mode.rowCheckboxSelection;
        }

        // If column config has been activated, activate cell and multiSelect
        if (changed.column) {
            mode.cell        = true;
            mode.multiSelect = true;
        }

        // If cell config has been activated, deactivate checkboxOnly
        if (changed.cell) {
            mode.checkboxOnly = false;
        }

        // If cell config has been deactivated, deactivate column
        if (changed.cell === false) {
            mode.column = false;
        }

        // If checkboxOnly config has been activated, activate checkbox and deactivate cell
        if (changed.checkboxOnly) {
            if (!mode.checkbox) {
                // checkbox can be a CheckboxColumnConfig
                mode.checkbox = true;
            }
            mode.cell = false;
        }

        // If checkbox config has been deactivated, deactivate checkboxOnly and showCheckAll
        if (changed.checkbox === false) {
            changed.checkboxOnly = false;
            changed.showCheckAll = false;
        }

        // If showCheckAll has been activated, activate checkbox and multiselect
        if (changed.showCheckAll) {
            mode.checkbox    = mode.checkbox || true;
            mode.multiSelect = true;
        }

        // If includeChildren config has been activated, activate multiselect
        if (changed.includeChildren || changed.includeParents) {
            mode.multiSelect = true;
        }

        // If multiSelect has been deactivated, deactivate column, showCheckAll, dragSelect and includeChildren
        if (changed.multiSelect === false) {
            mode.column = mode.showCheckAll = mode.dragSelect = mode.includeChildren = mode.includeParents = false;
        }

        if (changed.dragSelect) {
            if (rowReorder?.enabled && rowReorder.gripOnly !== true) {
                rowReorder.showGrip = rowReorder.gripOnly = true;
            }
            mode.multiSelect = true;
            me._selectionListenersDetachers = {};
        }
        if (changed.dragSelect === false && me._selectionListenersDetachers) {
            me._selectionListenersDetachers.selectiondrag?.();
            delete me._selectionListenersDetachers.selectiondrag;
        }

        // Deselect all when switching between row or cell selection mode
        // Deselect all when switching from multiselect to singleselect
        // Deselect all when changing deselectFilteredOutRecords
        if (oldMode && (
            changed.cell !== undefined ||
                changed.deselectFilteredOutRecords !== undefined ||
                changed.multiSelect !== undefined
        )) {
            me.deselectAll();
        }

        // Row number selection
        if (changed.rowNumber) {
            if (!columns.findRecord('type', 'rownumber')) {
                columns.insert(0, {
                    ...(typeof mode.rowNumber == 'object' ? mode.rowNumber : {}),
                    type : 'rownumber'
                });
                me._selectionAddedRowNumberColumn = true;
            }
        }
        else if (changed.rowNumber === false && me._selectionAddedRowNumberColumn) {
            columns.remove(columns.findRecord('type', 'rownumber'));
            delete me._selectionAddedRowNumberColumn;
        }

        // Add or remove checkbox column
        if (mode.checkbox !== oldMode?.checkbox ||
            (mode.checkbox && (mode.showCheckAll !== oldMode?.showCheckAll))
        ) {
            // See to it that were done configuring when initCheckboxSelection is called.
            if (me.isConfiguring) {
                me.shouldInitCheckboxSelection = true;
            }
            else {
                if (oldMode) {
                    me.deselectAll();
                }
                me.initCheckboxSelection();
            }
        }

        // If only checkboxIndex has changed
        if (oldMode && mode.checkbox && oldMode.checkbox &&
            mode.checkboxIndex !== oldMode.checkboxIndex && checkboxSelectionColumn) {
            checkboxSelectionColumn.parent.insertChild(checkboxSelectionColumn, columns.getAt(me.checkboxSelectionColumnInsertIndex));
        }

        me.trigger('selectionModeChange', ObjectHelper.clone(mode));
        me.afterSelectionModeChange(mode);
    }

    afterConfigure() {
        // See to it that were done configuring when initCheckboxSelection is called.
        if (this.shouldInitCheckboxSelection) {
            this.shouldInitCheckboxSelection = false;
            this.initCheckboxSelection();
        }
        super.afterConfigure();
    }

    initCheckboxSelection() {
        const
            me           = this,
            {
                selectionMode,
                columns,
                checkboxSelectionColumn
            }            = me,
            { checkbox } = selectionMode;

        // Always remove checkbox column when config changes
        if (checkboxSelectionColumn) {
            // Need to remove this handle because GridBase restores it if it exists.
            me.checkboxSelectionColumn = null;
            columns.remove(checkboxSelectionColumn);
        }

        // Inject our CheckColumn into the ColumnStore
        if (checkbox) {
            const
                checkColumnClass = ColumnStore.getColumnClass('check'),
                config           = checkbox === true ? null : checkbox;

            if (!checkColumnClass) {
                throw new Error('CheckColumn must be imported for checkbox selection mode to work');
            }

            const col = me.checkboxSelectionColumn = new checkColumnClass(ObjectHelper.assign({
                id           : `${me.id}-selection-column`,
                width        : '4em',
                minWidth     : '4em', // Needed because 4em is below Column's default minWidth
                field        : null,
                sortable     : false,
                filterable   : false,
                hideable     : false,
                cellCls      : 'b-checkbox-selection',
                // Always put the checkcolumn in the first region
                region       : me.items?.[0]?.region,
                showCheckAll : selectionMode.showCheckAll,
                draggable    : false,
                resizable    : false,
                widgets      : [{
                    type          : 'checkbox',
                    valueProperty : 'checked',
                    ariaLabel     : 'L{Checkbox.toggleRowSelect}'
                }]
            }, config), columns, { isSelectionColumn : true });

            col.meta.depth = 0;
            // This is assigned in Column.js for normal columns
            col._grid      = me;

            // Override renderer to inject the rendered record's selected status into the value
            const checkboxRenderer = col.renderer;

            col.renderer = renderData => {
                renderData.value = me.isSelected(renderData.record);
                checkboxRenderer.call(col, renderData);
            };

            col.ion({
                toggle    : 'onCheckChange',
                toggleAll : 'onCheckAllChange',
                thisObj   : me
            });

            columns.insert(me.checkboxSelectionColumnInsertIndex, col);
        }
    }

    // Used internally to get the index where to insert checkboxselectioncolumn
    // Default : Insert the checkbox after any rownumber column. If not there, -1 means in at 0.
    // If provided, insert at provided index
    get checkboxSelectionColumnInsertIndex() {
        const
            { columns }       = this;
        let { checkboxIndex } = this.selectionMode;

        if (!checkboxIndex) {
            checkboxIndex = columns.indexOf(columns.findRecord('type', 'rownumber')) + 1;
        }
        else if (typeof checkboxIndex === 'string') {
            checkboxIndex = columns.indexOf(columns.getById(checkboxIndex));
        }

        return checkboxIndex;
    }

    //endregion

    // region Events docs & Hooks

    /**
     * The selection has been changed.
     * @event selectionChange
     * @param {'select'|'deselect'} action `'select'`/`'deselect'`
     * @param {'row'|'cell'} mode `'row'`/`'cell'`
     * @param {Grid.view.Grid} source
     * @param {Core.data.Model[]} deselected The records deselected in this operation.
     * @param {Core.data.Model[]} selected The records selected in this operation.
     * @param {Core.data.Model[]} selection The records in the new selection.
     * @param {Grid.util.Location[]} deselectedCells The cells deselected in this operation.
     * @param {Grid.util.Location[]} selectedCells The cells selected in this operation.
     * @param {Grid.util.Location[]} cellSelection The cells in the new selection.
     */

    /**
     * Fires before the selection changes. Returning `false` from a listener prevents the change
     * @event beforeSelectionChange
     * @preventable
     * @param {String} action `'select'`/`'deselect'`
     * @param {'row'|'cell'} mode `'row'`/`'cell'`
     * @param {Grid.view.Grid} source
     * @param {Core.data.Model[]} deselected The records to be deselected in this operation.
     * @param {Core.data.Model[]} selected The records to be selected in this operation.
     * @param {Core.data.Model[]} selection The records in the current selection, before applying `selected` and
     * `deselected`
     * @param {Grid.util.Location[]} deselectedCells The cells to be deselected in this operation.
     * @param {Grid.util.Location[]} selectedCells The cells to be selected in this operation.
     * @param {Grid.util.Location[]} cellSelection  The cells in the current selection, before applying `selectedCells`
     * and `deselectedCells`
     */

    afterSelectionChange() {}

    afterSelectionModeChange() {}

    // endregion

    // region selectedRecordCollection

    changeSelectedRecordCollection(collection) {
        if (collection?.isCollection)  {
            if (!collection.owner) {
                collection.owner = this;
            }

            return collection;
        }

        return Collection.new(collection, { owner : this });
    }

    updateSelectedRecordCollection(collection) {

        collection.ion({
            change  : 'onSelectedRecordCollectionChange',
            thisObj : this
        });
    }

    onSelectedRecordCollectionChange({ added = [], removed }) {
        if (this.selectedRecordCollection._fromSelection !== this) {
            // Filter out unselectable rows
            added = added.filter(row => this.isSelectable(row));
            this.performSelection({
                selectedCells     : [],
                deselectedCells   : [],
                selectedRecords   : added,
                deselectedRecords : removed
            });
        }
    }

    changeSelectedRecordCollectionSilent(fn) {
        this.selectedRecordCollection._fromSelection = this;
        const result = fn(this.selectedRecordCollection);
        delete this.selectedRecordCollection._fromSelection;
        return result;
    }
    // endregion

    // region Store

    bindStore(store) {
        this.detachListeners('selectionStoreFilter');

        store.ion({
            name    : 'selectionStoreFilter',
            filter  : 'onStoreFilter',
            thisObj : this
        });
        super.bindStore?.(store);
    }

    unbindStore(oldStore) {
        this.detachListeners('selectionStoreFilter');

        super.unbindStore(oldStore);
    }

    onStoreFilter({ source }) {
        const
            me       = this,
            deselect = [];

        // Look for selected records which is not in the store
        for (const selectedRecord of me.selectedRows) {
            if (!source.includes(selectedRecord)) {
                // Should be deselected
                deselect.push(selectedRecord);
            }
        }

        // Deselects
        const selectionChange = me.prepareSelection(me.selectionMode.deselectFilteredOutRecords ? deselect : []);

        // If cell mode, always deselect cells
        if (me.isCellSelectionMode) {
            const { deselectedCells } = me.prepareSelection(me.getSelectedCellsForRecords(deselect));
            if (deselectedCells?.length) {
                selectionChange.deselectedCells = (selectionChange.deselectedCells || []).concat(deselectedCells);
            }
        }

        if (selectionChange.deselectedCells.length || selectionChange.deselectedRecords.length) {
            // Trigger deselect event
            me.performSelection(selectionChange, false);
            me.updateCheckboxHeader();
        }
    }

    /**
     * Triggered from Grid view when the id of a record has changed.
     * Update the collection indices.
     * @private
     * @category Selection
     */
    onStoreRecordIdChange({ record, oldValue }) {
        // If the next mixin up the inheritance chain has an implementation, call it
        super.onStoreRecordIdChange?.(...arguments);

        const item = this.selectedRecordCollection.get(oldValue);

        // having the record registered by the oldValue means we need to rebuild indices
        if (item === record) {
            this.selectedRecordCollection.rebuildIndices();
        }
    }

    /**
     * Triggered from Grid view when records get removed from the store.
     * Deselects all records which have been removed.
     * @private
     * @category Selection
     */
    onStoreRemove(event) {
        // If the next mixin up the inheritance chain has an implementation, call it
        super.onStoreRemove?.(event);

        if (!event.isCollapse) {
            const
                me                = this,
                deselectedRecords = event.records.filter(rec => this.isSelected(rec));

            if (deselectedRecords.length) {
                const selectionChange = me.prepareSelection(deselectedRecords);

                // If cell selection mode, also deselect cells for removed records
                // No need to update ui as grid will refresh
                if (me.isCellSelectionMode) {
                    const { deselectedCells } = me.prepareSelection(me.getSelectedCellsForRecords(deselectedRecords));
                    if (deselectedCells?.length) {
                        selectionChange.deselectedCells = (selectionChange.deselectedCells || []).concat(deselectedCells);
                    }
                }
                me.performSelection(selectionChange);
            }
        }
    }

    /**
     * Triggered from Grid view when the store changes. This might happen
     * if store events are batched and then resumed.
     * Deselects all records which have been removed.
     * @private
     * @category Selection
     */

    onStoreDataChange({ action, source : store }) {
        const
            me                 = this,
            { selectionMode }  = me;
        let selectionChange;

        // If the next mixin up the inheritance chain has an implementation, call it
        super.onStoreDataChange?.(...arguments);

        if (action === 'pageLoad') {
            // on page load, clear selection if not `preserverSelectionOnPageChange` is true
            if (!selectionMode.preserveSelectionOnPageChange) {
                selectionChange = me.prepareSelection(null, null, true);
            }

            // For paged grid scenario, we need to update the check-all checkbox in the checkbox column header
            // as we move between store pages
            me.updateCheckboxHeader();
        }
        else if (isDataLoadAction[action]) {
            const deselect = [];

            if (selectionMode.preserveSelectionOnDatasetChange === false) {
                selectionChange = me.prepareSelection(null, null, true);
            }
            else {
                // Update selected records
                deselect.push(...me.changeSelectedRecordCollectionSilent(c => c.match(store.storage)));

                for (const selectedCell of me._selectedCells) {
                    if (!store.getById(selectedCell.id)) {
                        deselect.push(selectedCell);
                    }
                }

                selectionChange = me.prepareSelection(deselect);
            }
        }

        if (selectionChange && (selectionChange.deselectAll || selectionChange.deselectedCells.length || selectionChange.deselectedRecords.length || selectionChange.selectedCells.length || selectionChange.selectedRecords.length)) {
            me.performSelection(selectionChange, false);
            me.updateCheckboxHeader();
        }
    }

    /**
     * Triggered from Grid view when all records get removed from the store.
     * Deselects all records.
     * @private
     * @category Selection
     */
    onStoreRemoveAll() {
        // If the next mixin up the inheritance chain has an implementation, call it
        super.onStoreRemoveAll?.();

        this.performSelection(this.prepareSelection(null, null, true), false);
    }

    //endregion

    // region Checkbox selection

    onCheckChange({ checked, record }) {
        const
            me          = this,
            deselectAll = !me.selectionMode.multiSelect && checked,
            deselect    = !deselectAll && !checked ? [record] : null,
            select      = checked ? [record] : null;

        // Saves previously non-shift checked checkbox
        if (checked && !GlobalEvents.shiftKeyDown) {
            me._lastSelectionChecked = record;
        }
        // Shift range select
        if (checked && me._lastSelectionChecked && GlobalEvents.shiftKeyDown) {
            me.performSelection(me.internalSelectRange(me._lastSelectionChecked, record, true));
        }
        // Regular selection
        else {
            // Updates UI and triggers events
            me.performSelection(me.prepareSelection(deselect, select, deselectAll, true));
        }
    }

    // Update header checkbox
    updateCheckboxHeader() {
        const { selectionMode, checkboxSelectionColumn, store } = this;

        if (selectionMode.checkbox && selectionMode.showCheckAll && checkboxSelectionColumn?.headerCheckbox) {
            const allSelected = store.count && !store.some(record => this.isSelectable(record) && !this.isSelected(record));

            if (checkboxSelectionColumn.headerCheckbox.checked !== allSelected) {
                checkboxSelectionColumn.suspendEvents();
                checkboxSelectionColumn.headerCheckbox.checked = allSelected;
                checkboxSelectionColumn.resumeEvents();
            }
        }
    }

    onCheckAllChange({ checked }) {
        this[checked ? 'selectAll' : 'deselectAll'](this.store.isPaged && this.selectionMode.preserveSelectionOnPageChange);
    }

    //endregion

    // region Selection drag

    // Hook for SalesForce-code to overwrite
    get selectionDragMouseEventListenerElement() {
        return globalThis;
    }

    // Creates new selection range on mouseover. Listener is initiated on mousedown
    onSelectionDrag(event) {
        const
            me                      = this,
            { _selectionStartCell } = me;

        // If we're here but there's no mouse button down for some reason, cancel
        if (!GlobalEvents.isMouseDown()) {
            me.onSelectionEnd();
        }

        // No start cell, ignore
        if (!_selectionStartCell) {
            return;
        }

        const
            { items, _lastSelectionDragRegion } = me,
            cellData                            = me.getCellDataFromEvent(event, true),
            region                              = cellData?.column.region,
            cellSelector                        = cellData?.cellSelector && me.normalizeCellContext(cellData.cellSelector);

        // If mouse enters new cell
        if (cellSelector && !cellSelector.equals(me._lastSelectionDragCell, true)) {
            if (!me._isSelectionDragging) {
                // When starting selection, start monitoring for near edge scrolling
                me.enableScrollingCloseToEdges(items);
            }

            // If we start a new selection drag on already selected cell, the default (de)selection is delayed until
            // mouseup. If we detect that a drag range is indeed what the user intends, deselect immediately
            if (me._clearSelectionOnSelectionDrag && !_selectionStartCell.equals(cellSelector, true)) {
                me.deselectAll();
                delete me._clearSelectionOnSelectionDrag;
            }

            // A grid with multiple regions need to handle selection and scrolling moving between regions
            if (_lastSelectionDragRegion && region !== _lastSelectionDragRegion) {
                const
                    leavingSubGrid     = me.subGrids[_lastSelectionDragRegion],
                    enteringSubGrid    = me.subGrids[region],
                    leavingScrollable  = leavingSubGrid.scrollable,
                    enteringScrollable = enteringSubGrid.scrollable,
                    goingForward       = items.indexOf(leavingSubGrid) - items.indexOf(enteringSubGrid) < 0;

                // Immediately scrolls an entering subgrid to either start or end depending on direction
                enteringScrollable.x = goingForward ? 0 : enteringScrollable.maxX;

                // Waiting for grid to scroll to start/end (handled by scrollmanager)
                if (goingForward ? leavingScrollable.x < leavingScrollable.maxX - 1 : leavingScrollable.x > 1) {
                    return;
                }

                // Forces the previous subgrid to stop reserving horizontal scroll
                const activeHorizontalScroll = me.scrollManager._activeScroll?.horizontal;

                if (activeHorizontalScroll && activeHorizontalScroll.element !== enteringScrollable.element) {
                    activeHorizontalScroll.stopScroll(true);
                }
            }

            me._lastSelectionDragRegion = region;
            me._lastSelectionDragCell   = cellSelector;
            me._isSelectionDragging     = true;

            const selectionChange = me._lastSelectionDragChange = me.internalSelectRange(_selectionStartCell,
                cellSelector, me.isRowNumberSelecting(cellSelector) || me.isRowNumberSelecting(_selectionStartCell));

            // As selection at this point is UI only, we don't want to affect already selected records
            selectionChange.deselectedCells   = selectionChange.deselectedCells.filter(cell => !me.isCellSelected(cell));
            selectionChange.deselectedRecords = selectionChange.deselectedRecords.filter(record => !me.isSelected(record));

            // selectionChange event fires onSelectionEnd
            me.refreshGridSelectionUI(selectionChange);

            /**
             * Fires while drag selecting. UI will update with current range, but the cells will not be selected until
             * mouse up. This event can be listened for to perform actions while drag selecting.
             * @event dragSelecting
             * @param {Grid.view.Grid} source
             * @param {Core.data.Model[]|Object} selectedCells The cells that is currently being dragged over
             */
            me.trigger('dragSelecting', selectionChange);
        }
    }

    // Tells onSelectionDrag that it's not dragging any longer
    onSelectionEnd() {
        const
            me        = this,
            lastChange = me._lastSelectionDragChange;

        if (me._isSelectionDragging && !me._selectionStartCell.equals(me._lastSelectionDragCell, true) && lastChange) {
            me.performSelection(lastChange, false);
        }

        me.disableScrollingCloseToEdges(me.items);

        me._isSelectionDragging     = false;
        me._lastSelectionDragChange = me._lastSelectionDragCell = me._lastSelectionDragRegion = null;

        // Remove listeners
        me._selectionListenersDetachers.selectiondrag?.();
        delete me._selectionListenersDetachers.selectiondrag;
    }

    // endregion

    // region Column selection

    onHandleElementClick(event) {
        const me = this;

        // If rownumber column is clicked, toggle selectAll
        if (me.selectionMode.rowNumber && event.target.closest('.b-rownumber-header')) {
            event.handled = true;
            if (me.store.count && me.store.some(record => !me.isSelected(record))) {
                me.selectAll();
            }
            else {
                me.deselectAll();
            }

        }
        // In column selection mode, and we clicked a header, the column should be selected
        else if (me.selectionMode.column && event.target.closest('.b-grid-header')) {
            event.handled = true;
            me.selectColumn(event, event.ctrlKey);
        }

        super.onHandleElementClick(event);
    }

    selectColumn(event, addToSelection = false) {
        const
            me           = this,
            { store }    = me,
            { columnId } = me.getHeaderDataFromEvent(event);

        // internalSelectRange uses this to remember last range, we have no need for that here
        me._shiftSelectRange = null;

        if (!event.shiftKey) {
            me._shiftSelectColumn = columnId;
        }
        const
            fromColumnId    = (event.shiftKey && me._shiftSelectColumn) || columnId,
            selectionChange = me.internalSelectRange(
                me.normalizeCellContext({ id : store.first.id, columnId : fromColumnId }),
                me.normalizeCellContext({ id : store.last.id, columnId })
            );

        // If we are selecting a column that is already selected, deselect it
        if (addToSelection && !selectionChange.selectedCells.some(sc => !me.isCellSelected(sc))) {
            selectionChange.deselectedCells = selectionChange.selectedCells;
            selectionChange.selectedCells   = [];
        }

        if (!addToSelection) {
            selectionChange.deselectedCells = me._selectedCells;
        }
        me.cleanSelectionChange(selectionChange);
        me.performSelection(selectionChange);
    }

    // endregion

    // region Public row/record selection

    /**
     * Checks whether a row is selected. Will not check if any of a row's cells are selected.
     * @param {LocationConfig|String|Number|Core.data.Model} cellSelectorOrId Cell selector { id: x, column: xx } or row
     * id, or record
     * @returns {Boolean} true if row is selected, otherwise false
     * @category Selection
     */
    isSelected(cellSelectorOrId) {
        // Not a selected cell, check recoWds
        if (cellSelectorOrId?.id) {
            cellSelectorOrId = cellSelectorOrId.id;
        }

        if (validIdTypes[typeof cellSelectorOrId]) {
            return this.selectedRows.some(rec => rec.id === cellSelectorOrId);
        }

        return false;
    }

    /**
     * Checks whether a cell is selected.
     * @param {LocationConfig|Location} cellSelector Cell selector { id: x, column: xx }
     * @param {Boolean} includeRow to also check if row is selected
     * @returns {Boolean} true if cell is selected, otherwise false
     * @category Selection
     */
    isCellSelected(cellSelector, includeRow) {
        cellSelector = this.normalizeCellContext(cellSelector);
        return (this.isCellSelectionMode && this._selectedCells.some(cell => cellSelector.equals(cell, true))) ||
            (includeRow && this.isSelected(cellSelector));
    }

    /**
     * Checks whether a cell or row can be selected.
     * @param {Core.data.Model|LocationConfig|String|Number} recordCellOrId Record or cell or record id
     * @returns {Boolean} true if cell or row can be selected, otherwise false
     * @category Selection
     */
    isSelectable(recordCellOrId) {
        return this.normalizeCellContext({ id : recordCellOrId.id || recordCellOrId }).isSelectable;
    }

    /**
     * The last selected record. Set to select a row or use Grid#selectRow. Set to null to
     * deselect all
     * @property {Core.data.Model}
     * @category Selection
     */
    get selectedRecord() {
        return this.selectedRecords[this.selectedRecords.length - 1] || null;
    }

    set selectedRecord(record) {
        this.selectRow({ record });
    }

    /**
     * Selected records.
     *
     * If {@link #config-selectionMode deselectFilteredOutRecords} is `false` (default) this will include selected
     * records which has been filtered out.
     *
     * If {@link #config-selectionMode preserveSelectionOnPageChange} is `true` (defaults to `false`) this will include
     * selected records on all pages.
     *
     * If {@link #config-selectionMode selectRecordOnCell} is `true` (default) this will include any record which has at
     * least one cell selected.
     *
     * Can be set as array of ids:
     *
     * ```javascript
     * grid.selectedRecords = [1, 2, 4, 6]
     * ```
     *
     * @property {Core.data.Model[]}
     * @accepts {Core.data.Model[]|Number[]}
     * @category Selection
     */
    get selectedRecords() {
        return this.selectedRecordCollection.values;
    }

    set selectedRecords(selectedRecords) {
        this.selectRows(selectedRecords);
    }

    /**
     * Selected records. Records selected via cell selection is excluded.
     *
     * If {@link #config-selectionMode deselectFilteredOutRecords} is `false` (default) this will include selected
     * records which has been filtered out.
     *
     * If {@link #config-selectionMode preserveSelectionOnPageChange} is `true` (defaults to `false`) this will include
     * selected records on all pages.
     *
     * if {@link #config-selectionMode selectRecordOnCell} is `false` this will return same records as
     * {@link #property-selectedRecords}.
     *
     * Can be set as array of ids:
     *
     * ```javascript
     * grid.selectedRecords = [1, 2, 4, 6]
     * ```
     *
     * @property {Core.data.Model[]}
     * @accepts {Core.data.Model[]|Number[]}
     * @category Selection
     */
    get selectedRows() {
        return [...this._selectedRows];
    }

    set selectedRows(selectedRows) {
        this.selectRows(selectedRows);
    }

    /**
     * Removes and adds records to/from the selection at the same time. Analogous
     * to the `Array` `splice` method.
     *
     * Note that if items that are specified for removal are also in the `toAdd` array,
     * then those items are *not* removed then appended. They remain in the same position
     * relative to all remaining items.
     *
     * @param {Number} index Index at which to remove a block of items. Only valid if the
     * second, `toRemove` argument is a number.
     * @param {Object[]|Number} toRemove Either the number of items to remove starting
     * at the passed `index`, or an array of items to remove (If an array is passed, the `index` is ignored).
     * @param  {Object[]|Object} toAdd An item, or an array of items to add.
     * @category Selection
     */
    spliceSelectedRecords(index, toRemove, toAdd) {
        const me = this;

        if (typeof toRemove == 'number') {
            const select = [...me.selectedRecords];
            select.splice(index, toRemove, ...ArrayHelper.asArray(toAdd));
            me.performSelection(me.prepareSelection(null, select, true, true));
        }
        else {
            // Just add and remove
            me.performSelection(me.prepareSelection(toRemove, toAdd, false, true));
        }
    }

    /**
     * Select one row
     * @param {Object|Core.data.Model|String|Number} options A record or id to select or a config object describing the
     * selection
     * @param {Core.data.Model|String|Number} options.record Record or record id, specifying null will deselect all
     * @param {Grid.column.Column} [options.column] The column to scroll into view if `scrollIntoView` is not specified as
     * `false`. Defaults to the grid's first column.
     * @param {Boolean} [options.scrollIntoView] Specify `false` to prevent row from being scrolled into view
     * @param {Boolean} [options.addToSelection] Specify `true` to add to selection, defaults to `false` which replaces
     * @fires selectionChange
     * @category Selection
     */
    selectRow(options) {
        // Make sure we have an object
        if (typeof options === 'number' || options.isModel || !('record' in options)) {
            options = {
                records : [options]
            };
        }

        // scrollIntoView is default here
        ObjectHelper.assignIf(options, {
            scrollIntoView : true
        });

        this.selectRows(options);
    }

    /**
     * Select one or more rows
     * @param {Object|Core.data.Model[]|String[]|Number[]} options An array of records or ids for a record or a
     * config object describing the selection
     * @param {Core.data.Model[]|String[]|Number[]} options.records An array of records or ids for a record
     * @param {Grid.column.Column} options.column The column to scroll into view if `scrollIntoView` is not specified as
     * `false`. Defaults to the grid's first column.
     * @param {Boolean} [options.scrollIntoView] Specify `false` to prevent row from being scrolled into view
     * @param {Boolean} [options.addToSelection] Specify `true` to add to selection, defaults to `false` which replaces
     * @category Selection
     */
    selectRows(options) {
        // Got a single or an array of records/ids, convert it to an object
        if (!options || Array.isArray(options) || options.isModel || typeof options === 'number' ||
            (!('records' in options) && !('record' in options))
        ) {
            options = {
                records : ArrayHelper.asArray(options) || []
            };
        }

        const
            me                 = this,
            { store }          = me,
            toSelect           = [],
            {
                records        = options.record ? [options.record] : [], // Got a record instead of records
                column         = me.columns.visibleColumns[0], // Default
                scrollIntoView,
                addToSelection = arguments[1] // Backwards compatibility
            }                  = options;

        for (let record of records) {
            record = store.getById(record);
            if (record) {
                toSelect.push(record);
            }
        }

        if (!addToSelection) {
            me._shiftSelectRange = null;
        }

        me.performSelection(me.prepareSelection(null, toSelect, !addToSelection, true));

        if (toSelect.length && scrollIntoView) {
            me.scrollRowIntoView(toSelect[0].id, {
                column
            });
        }
    }

    /**
     * This selects all rows. If store is filtered, this will merge the selection of all visible rows with any selection
     * made prior to filtering.
     * @privateparam {Boolean} [silent] Pass `true` not to fire any event upon selection change
     * @category Selection
     */
    selectAll(silent = false) {
        const
            { store } = this,
            records   = (store.isGrouped ? store.allRecords : store.records).filter(r => !r.isSpecialRow);

        // If store is grouped, store.records excludes collapsed records and allRecords excludes filtered out records
        // Else, store records holds what we're after
        this.performSelection(this.prepareSelection(null, records, false, true), true, silent);
    }

    /**
     * Deselects all selected rows and cells. If store is filtered, this will unselect all visible rows only. Any
     * selections made prior to filtering remains.
     * @param {Boolean} [removeCurrentRecordsOnly] Pass `false` to clear all selected records, and `true` to only
     * clear selected records in the current set of records
     * @param {Boolean} [silent] Pass `true` not to fire any event upon selection change
     * @category Selection
     */
    deselectAll(removeCurrentRecordsOnly = false, silent = false) {
        const
            { store } = this,
            records   = removeCurrentRecordsOnly
                ? (store.isGrouped ? store.allRecords : store.records).filter(r => !r.isSpecialRow) : null;

        this.performSelection(this.prepareSelection(records, null, !removeCurrentRecordsOnly), true, silent);
    }

    /**
     * Deselect one row
     * @param {Core.data.Model|String|Number} recordOrId Record or an id for a record
     * @category Selection
     */
    deselectRow(record) {
        this.deselectRows(record);
    }

    /**
     * Deselect one or more rows
     * @param {Core.data.Model|String|Number|Core.data.Model[]|String[]|Number[]} recordOrIds An array of records or ids
     * for a record
     * @category Selection
     */
    deselectRows(recordsOrIds) {
        // Ignore any non-existing row records passed
        const
            { store } = this,
            records   = ArrayHelper.asArray(recordsOrIds).map(recordOrId => store.getById(recordOrId)).filter(rec => rec);

        this.performSelection(this.prepareSelection(records));
    }

    /**
     * Selects rows corresponding to a range of records (from fromId to toId)
     * @param {String|Number} fromId
     * @param {String|Number} toId
     * @category Selection
     */
    selectRange(fromId, toId, addToSelection = false) {
        const
            me        = this,
            { store } = me,
            selection = me.internalSelectRange(store.getById(fromId), store.getById(toId), true);

        me._shiftSelectRange = null; // For below function to not replace last range with new one
        me.performSelection(selection);
    }

    // endregion

    // region Public cell selection

    /**
     * In cell selection mode, this will get the cell selector for the (last) selected cell. Set to an available cell
     * selector to select only that cell. Or use {@link #function-selectCell()} instead.
     * @property {Grid.util.Location}
     * @category Selection
     */
    get selectedCell() {
        return this._selectedCells[this._selectedCells.length - 1];
    }

    set selectedCell(cellSelector) {
        this.selectCells([cellSelector]);
    }

    /**
     * In cell selection mode, this will get the cell selectors for all selected cells. Set to an array of available
     * cell selectors. Or use {@link #function-selectCells()} instead.
     * @property {Grid.util.Location[]}
     * @category Selection
     */
    get selectedCells() {
        return [...this._selectedCells];
    }

    set selectedCells(cellSelectors) {
        this.selectCells(cellSelectors);
    }

    /**
     * CSS selector for the currently selected cell. Format is "[data-index=index] [data-column-id=column]".
     * @type {String}
     * @category Selection
     * @readonly
     */
    get selectedCellCSSSelector() {
        const
            cell = this.selectedCell,
            row  = cell && this.getRowById(cell.id);

        if (!cell || !row) return '';

        return `[data-index=${row.dataIndex}] [data-column-id=${cell.columnId}]`;
    }

    /**
     * If in cell selection mode, this selects one cell. If not, this selects the cell's record.
     * @param {LocationConfig|Object} options A cell selector ({ id: rowId, columnId: 'columnId' }) or a config object
     * @param {LocationConfig} options.cell  A cell selector ({ id: rowId, columnId: 'columnId' })
     * @param {Boolean} [options.scrollIntoView] Specify `false` to prevent row from being scrolled into view
     * @param {Boolean} [options.addToSelection] Specify `true` to add to selection, defaults to `false` which replaces
     * @param {Boolean} [options.silent] Specify `true` to not trigger any events when selecting the cell
     * @returns {Grid.util.Location} Cell selector
     * @fires selectionChange
     * @category Selection
     */
    selectCell(options) {
        // Got a cell selector as first argument
        if ('id' in options) {
            options = {
                cell : options
            };

            // Arguments backward's compability
            options = Object.assign({
                scrollIntoView : arguments[1],
                addToSelection : arguments[2],
                silent         : arguments[3]
            }, options);
        }

        return this.selectCells(options)?.[0];
    }

    /**
     * If in cell selection mode, this selects a number of cells. If not, this selects corresponding records.
     * @param {Object|LocationConfig[]} options An array of cell selectors ({ id: rowId, columnId: 'columnId' }) or a config
     * object
     * @param {LocationConfig[]} options.cells An array of cell selectors { id: rowId, columnId: 'columnId' }
     * @param {Boolean} [options.scrollIntoView] Specify `false` to prevent row from being scrolled into view
     * @param {Boolean} [options.addToSelection] Specify `true` to add to selection, defaults to `false` which replaces
     * @param {Boolean} [options.silent] Specify `true` to not trigger any events when selecting the cell
     * @returns {Grid.util.Location[]} Cell selectors
     * @returns {Grid.util.Location[]} Cell selectors
     * @fires selectionChange
     * @category Selection
     */
    selectCells(options) {
        // Got a cell selector array as first argument
        if (Array.isArray(options)) {
            options = {
                cells : options
            };
        }

        const
            me                 = this,
            {
                cells          = options.cell ? [options.cell] : [], // Got a cell instead of cells
                scrollIntoView = true,
                addToSelection = false,
                silent         = false
            }                  = options,
            selectionChange    = me.prepareSelection(null, cells, !addToSelection);

        if (!addToSelection) {
            me._shiftSelectRange = null;
        }

        me.performSelection(selectionChange, true, silent);

        if (scrollIntoView) {
            me.scrollRowIntoView(cells[0].id, {
                column : cells[0].columnId
            });
        }

        return me.isCellSelectionMode ? selectionChange.selectedCells : selectionChange.selectedRecords;
    }

    /**
     * If in cell selection mode, this deselects one cell. If not, this deselects the cell's record.
     * @param {LocationConfig} cellSelector
     * @returns {Grid.util.Location} Normalized cell selector
     * @category Selection
     */
    deselectCell(cellSelector) {
        return this.deselectCells([cellSelector])?.[0];
    }

    /**
     * If in cell selection mode, this deselects a number of cells. If not, this deselects corresponding records.
     * @param {LocationConfig[]} cellSelectors
     * @returns {Grid.util.Location[]} Normalized cell selectors
     * @category Selection
     */
    deselectCells(cellSelectors) {
        const selectionChange = this.prepareSelection(cellSelectors);
        this.performSelection(selectionChange);
        return this.isCellSelectionMode ? selectionChange.deselectedCells : selectionChange.deselectedRecords;
    }

    // Used by keymap to toggle selection of currently focused cell.
    toggleSelection(keyEvent) {
        const
            me          = this,
            {
                _focusedCell,
                selectionMode
            }           = me,
            isRowNumber = me.isRowNumberSelecting(_focusedCell),
            isSelected  = me.isCellSelected(_focusedCell, true);

        // Only if keyboardNavigation selection is deactivated and were not focusing an actionable cell
        if (selectionMode.selectOnKeyboardNavigation === true || _focusedCell.isActionable) {
            // Return false to ley keyMap know we didn't handle this event
            return false;
        }

        me.performSelection(
            me.prepareSelection(
                isSelected ? _focusedCell : null,
                isSelected ? null : _focusedCell,
                !selectionMode.multiSelect,
                isRowNumber
            ));

        // Space key has preventDefault = false somewhere
        keyEvent.preventDefault();
    }

    /**
     * Selects a range of cells, from a cell selector (Location) to another
     * @param {Grid.util.Location|LocationConfig} from
     * @param {Grid.util.Location|LocationConfig} to
     * @category Selection
     */
    selectCellRange(from, to) {
        this.performSelection(this.internalSelectRange(from, to));
    }

    // endregion

    // region Private convenience functions & properties
    getSelection() {
        if (this.isRowSelectionMode) {
            return this.selectedRecords;
        }
        else {
            return this.selectedCells;
        }
    }

    // Makes sure the same record or cell isn't deselected and selected at the same time. Selection will take precedence
    cleanSelectionChange(selectionChange) {
        const
            {
                deselectedRecords,
                selectedRecords,
                deselectedCells,
                selectedCells,
                deselectedCellRecords,
                selectedCellRecords
            } = selectionChange;

        // Filter out records which is both selected and deselected
        if (deselectedRecords?.length && selectedRecords?.length) {
            selectionChange.deselectedRecords = deselectedRecords.filter(dr => !selectedRecords.some(sr => dr === sr));
        }

        // Filter out cells which is both selected and deselected
        if (deselectedCells?.length && selectedCells?.length) {
            selectionChange.deselectedCells = deselectedCells.filter(dc => !selectedCells.some(sc => dc.equals(sc, true)));
        }

        // Filter out cell-selected records that is being selected
        if (deselectedCellRecords.length && (selectedCellRecords?.length || selectedRecords?.length)) {
            selectionChange.deselectedCellRecords = deselectedCellRecords.filter(dcr => {
                return !selectedCellRecords.some(scr => dcr.id === scr.id) &&
                    !selectedRecords.some(sr => dcr.id === sr.id);
            });
        }

        return selectionChange;
    }

    getSelectedCellsForRecords(records) {
        return this._selectedCells.filter(cell => cell.id && records.some(record => record.id === cell.id));
    }

    delayUntilMouseUp(fn) {

        const detacher = EventHelper.on({
            element : globalThis,
            blur    : ev => fn(ev, detacher),
            mouseup : ev => fn(ev, detacher),
            thisObj : this,
            once    : true
        });
    }

    get isRowSelectionMode() {
        return !this.isCellSelectionMode;
    }

    get isCellSelectionMode() {
        return this.selectionMode.cell === true;
    }

    // Checks if rowNumber is activated and that all arguments (cellselectors) is of type rownumber
    isRowNumberSelecting(...selectors) {
        return this.selectionMode.rowNumber && !selectors.some(cs => cs.column.type !== 'rownumber');
    }

    // endregion

    //region Navigation

    // Used by keyMap to extend selection range
    extendSelectionLeft() {
        this.extendSelection('Left');
    }

    // Used by keyMap to extend selection range
    extendSelectionRight() {
        this.extendSelection('Right');
    }

    // Used by keyMap to extend selection range
    extendSelectionUp() {
        this.extendSelection('Up');
    }

    // Used by keyMap to extend selection range
    extendSelectionDown() {
        this.extendSelection('Down');
    }

    // Used by keyMap to extend selection range
    extendSelection(dir) {
        this._isKeyboardRangeSelecting = true;
        this['navigate' + dir]();
        this._isKeyboardRangeSelecting = false;
    }

    // Called from GridNavigation on mouse or keyboard events
    // Single entry point for all default user selection actions
    onCellNavigate(me, fromCellSelector, toCellSelector, doSelect) {
        const
            {
                selectionMode,
                _selectionListenersDetachers
            }                                            = me,
            { multiSelect, deselectOnClick, dragSelect } = selectionMode,
            { ctrlKeyDown, shiftKeyDown }                = GlobalEvents,
            isMouseLeft                                  = GlobalEvents.isMouseDown(),
            isMouseRight                                 = GlobalEvents.isMouseDown(2),
            currentEvent                                 = GlobalEvents.currentMouseDown || GlobalEvents.currentKeyDown;

        // To be sure we got Locations
        toCellSelector = me.normalizeCellContext(toCellSelector);

        if (
            !doSelect ||
            // Do not affect selection if navigating into header row.
            toCellSelector.rowIndex === -1 ||
            toCellSelector.record?.isGroupHeader ||
            // Don't allow keyboard selection if keyboardNavigation is deactivated
            (currentEvent?.fromKeyMap && !selectionMode.selectOnKeyboardNavigation) ||
            // CheckColumn events are handled by the CheckColumn itself.
            me.columns.getById(toCellSelector.columnId) === me.checkboxSelectionColumn ||
            selectionMode.checkboxOnly ||
            // Don't select if event was handled elsewhere
            currentEvent?.handled === true
        ) {
            return;
        }

        // Save adding state unless shift key
        if (!shiftKeyDown) {
            me._isAddingToSelection = ctrlKeyDown && multiSelect;
            me._selectionStartCell  = toCellSelector; // To be able to begin a new range
        }

        // Flags that it's possible for onSelectDrag to apply its logic if the right conditions are met
        // (preventDragSelect is set by RowReorder if mousedown on the grip with gripOnly configured. also set in
        // DragCreateBase and EventDragSelect)
        if (multiSelect && dragSelect && isMouseLeft && !_selectionListenersDetachers.selectiondrag && !me.preventDragSelect) {
            _selectionListenersDetachers.selectiondrag = EventHelper.on({
                name    : 'selectiondrag',
                element : me.selectionDragMouseEventListenerElement,
                blur    : 'onSelectionEnd',
                mouseup : {
                    handler : 'onSelectionEnd',
                    element : globalThis
                },
                mousemove : 'onSelectionDrag',
                thisObj   : me
            });
        }

        me.preventDragSelect = false;

        const
            startCell = me._selectionStartCell,
            adding    = me._isAddingToSelection;

        // Select range on shiftKey
        if (((shiftKeyDown && isMouseLeft) || me._isKeyboardRangeSelecting) && startCell && multiSelect) {
            me.performSelection(
                me.internalSelectRange(
                    startCell,
                    toCellSelector,
                    me.isRowNumberSelecting(startCell, toCellSelector)
                )
            );
        }
        else {
            let delay             = false,
                continueSelecting = true,
                deselect;

            // If current is already selected
            if (me.isCellSelected(toCellSelector, true)) {
                // Do nothing if we right-clicked already selected row/cell
                if (isMouseRight) {
                    return;
                }
                // Deselect current if selected and multiselecting or deselect all if deselectOnClick is true
                if ((adding || deselectOnClick)) {
                    deselect = deselectOnClick ? null : [toCellSelector];
                    continueSelecting = false; // Only deselect at this code path
                }
                // If this is only row or cell that's selected
                else if (me.selectedRecords.length + (me.isCellSelectionMode ? me._selectedCells.length : 0) <= 1) {
                    // Should stay selected, do no more
                    return;
                }
                // Delay if click a selected cell which will be deselected (for dragging)
                delay = deselectOnClick || multiSelect;
            }
            // deselect all if not multiselecting
            if (!deselect && !adding) {
                deselect = null;
                // Set flag so that dragselection functionality know to clear selection if needed
                if (dragSelect && delay && _selectionListenersDetachers.selectiondrag) {
                    me._clearSelectionOnSelectionDrag = true;
                }
            }

            // Wrapping selection in a function to be called either directly or on mouse up
            const finishSelection = (mouseUpEvent, detacher) => {
                detacher?.();
                if (mouseUpEvent?.target?.nodeType === Node.ELEMENT_NODE) {
                    // If we are waiting for mouseUp and have moved to a different cell, abort selection change
                    const mouseUpSelector = new Location(mouseUpEvent.target);
                    if (mouseUpSelector?.grid && !mouseUpSelector.equals(toCellSelector, true)) {
                        return;
                    };
                }

                if (!shiftKeyDown) {
                    me._shiftSelectRange = null; // Clear any previous range selected
                }

                me.performSelection(
                    me.prepareSelection(
                        deselect,
                        continueSelecting && [toCellSelector],
                        deselect === null,
                        continueSelecting && me.isRowNumberSelecting(toCellSelector)
                    )
                );
            };

            if (me.features.rowReorder?.isDragging) {
                return;
            }
            // Delay doing the selection until mouse up for allowing drag of row in certain cases
            if (delay) {
                me.delayUntilMouseUp(finishSelection);
            }
            else {
                finishSelection();
            }
        }
    }

    // endregion

    // region Internal selection & deselection functions

    /**
     * Used internally to prepare a number of cells or records for selection/deselection depending on if cell
     * selectionMode is activated. This function will not select/deselect anything by itself
     * (that's done in performSelection).
     * @param {LocationConfig[]|Core.data.Model[]} cellSelectorsToDeselect Array of cell selectors or records.
     * @param {LocationConfig[]|Core.data.Model[]} cellSelectorsToSelect Array of cell selectors or records.
     * @param {Boolean} deselectAll Set to `true` to clear all selected records and cells.
     * @param {Boolean} forceRecordSelection Set to `true` to force record selection even if cell selection is active.
     * @returns {Object} selectionChange object to use for UI update
     * @private
     * @category Selection
     */
    prepareSelection(cellSelectorsToDeselect, cellSelectorsToSelect, deselectAll = false, forceRecordSelection = false) {
        const
            me                    = this,
            isDragging            = me._isSelectionDragging,
            {
                includeParents,
                selectRecordOnCell
            }                     = me.selectionMode,
            selectedRecords       = [],
            selectedCells         = [];
        let deselectedCells       = [],
            deselectedRecords     = [],
            selectedCellRecords   = [],
            deselectedCellRecords = [];

        if (deselectAll) {
            deselectedCells       = me._selectedCells;
            deselectedRecords     = me._selectedRows;
            deselectedCellRecords = me.selectedRecords.filter(r => !deselectedRecords.some(dr => dr.id === r.id));
        }
        else if (cellSelectorsToDeselect) {
            for (const selector of ArrayHelper.asArray(cellSelectorsToDeselect)) {
                const
                    cellSelector = me.normalizeCellContext(selector),
                    record       = cellSelector?.record ||
                        (selector.isModel ? selector : me.store.getById(cellSelector.id));

                if (cellSelector.isSpecialRow) {
                    continue;
                }

                deselectedCells.push(cellSelector);

                if (record && !deselectedRecords.some(r => r.id === record.id)) {
                    // When dragging, this path is taken but nothing is actually selected until mouseup
                    // So should check if selected for dragselection (until mouseup)
                    if (isDragging || me.isSelected(record)) {
                        deselectedRecords.push(record);
                    }
                    // If not directly selected, but selected by cell, deselect by cell
                    else if (selectRecordOnCell && me.selectedRecordCollection.get(record.id) &&
                        !deselectedCellRecords.some(dr => dr.id === record.id)
                    ) {
                        deselectedCellRecords.push(record);
                    }
                    // If configured, also deselect children
                    if (me.selectionMode.includeChildren && me.selectionMode.multiSelect && !record.isLeaf &&
                        record.allChildren?.length
                    ) {
                        for (const child of record.allChildren) {
                            if (!deselectedRecords.some(r => r.id === child.id) &&
                                (isDragging || me.isSelected(child))
                            ) {
                                deselectedRecords.push(child);
                            }
                        }
                    }
                }
            }
        }

        if (cellSelectorsToSelect) {
            for (const selector of ArrayHelper.asArray(cellSelectorsToSelect)) {
                const
                    cellSelector = me.normalizeCellContext(selector),
                    record       = cellSelector?.record ||
                        (selector.isModel ? selector : me.store.getById(cellSelector.id));

                if (!record || cellSelector.isSpecialRow) {
                    continue;
                }

                // Only select cells if in cell selection mode and not forcing record selection
                if (me.isCellSelectionMode && !forceRecordSelection) {
                    selectedCells.push(cellSelector);
                }
                if ((me.isRowSelectionMode || forceRecordSelection) &&
                    !selectedRecords.some(r => r.id === record.id)
                ) {
                    selectedRecords.push(record);
                    // If configured, also select children
                    if (me.selectionMode.includeChildren && me.selectionMode.multiSelect && !record.isLeaf &&
                        record.allChildren?.length
                    ) {
                        for (const child of record.allChildren) {
                            if (!selectedRecords.some(r => r.id === child.id)) {
                                selectedRecords.push(child);
                            }
                        }
                    }
                }
            }

            if (selectRecordOnCell && selectedCells.length) {
                selectedCellRecords = ArrayHelper.unique(selectedCells.map(c => c.record))
                    .filter(r => !selectedRecords.some(sr => sr.id === r.id));
            }
        }

        // This setting could be either off, or true/'all' or 'some'
        if (includeParents && (deselectedRecords.length || selectedRecords.length)) {
            const
                allChanges         = [...deselectedRecords, ...selectedRecords],
                lowestLevelParents = ArrayHelper.unique(
                    allChanges.filter(rec =>
                        rec.parent && !rec.allChildren.some(child =>
                            allChanges.includes(child))).map(rec => rec.parent));

            lowestLevelParents.forEach(parent => me.toggleParentSelection(parent, selectedRecords, deselectedRecords));

        }

        return me.cleanSelectionChange({
            selectedCells,
            selectedRecords,
            deselectedCells,
            deselectedRecords,
            deselectAll,
            action : selectedRecords?.length || selectedCells?.length ? 'select' : 'deselect',
            selectedCellRecords,
            deselectedCellRecords
        });
    }

    toggleParentSelection(parent, toSelect, toDeselect) {
        if (!parent || parent.isRoot) {
            return;
        }

        const
            isSelected      = this.isSelected(parent),
            inToSelect      = toSelect.includes(parent),
            inToDeselect    = toDeselect.includes(parent),
            childIsSelected = child => (this.isSelected(child) && !toDeselect.includes(child)) || toSelect.includes(child);

        if (this.selectionMode.includeParents === 'some') {
            // If any children are selected
            if (parent.allChildren.some(childIsSelected)) {
                // And parent is not being deselected => select
                if ((!isSelected || inToDeselect) && !inToSelect) {
                    toSelect.push(parent);
                }
            }
            // No children are selected and parent is selected => deselect
            else if (isSelected && !inToDeselect) {
                toDeselect.push(parent);
            }
        }
        else { // includeParents = true/'all'
            if (isSelected) {
                // If previously selected, and some child is to be deselected => deselect
                if (!inToDeselect && !inToSelect && parent.allChildren.some(child => toDeselect.includes(child))) {
                    toDeselect.push(parent);
                }
            }
            else if (!inToSelect) {
                // If not previously selected, select if all children are selected
                if (parent.allChildren.every(childIsSelected)) {
                    toSelect.push(parent);
                }
            }
        }

        // Go up one level if it exists
        if (parent.parent) {
            this.toggleParentSelection(parent.parent, toSelect, toDeselect);
        }
    }

    /**
     * Used internally to select a range of cells or records depending on selectionMode. Used in both shift-selection
     * and for drag selection. Will remember current selection range and replace it with new one when it changes. But a
     * range which is completed (drag select mouse up or a new shift range starting point has been set) will remain.
     * This function will not update UI (that's done in refreshGridSelectionUI).
     * @param {LocationConfig} fromSelector
     * @param {LocationConfig} toSelector
     * @returns {Object} selectionChange object to use for UI update
     * @private
     * @category Selection
     */
    internalSelectRange(fromSelector, toSelector, forceRecordSelection = false) {
        const
            me              = this,
            selectRecords   = me.isRowSelectionMode || forceRecordSelection,
            selectionChange = me.prepareSelection(me._shiftSelectRange,
                me.getRange(fromSelector, toSelector, selectRecords), false, forceRecordSelection);

        me._shiftSelectRange = selectionChange[`selected${selectRecords ? 'Records' : 'Cells'}`];

        return selectionChange;
    }

    /**
     * Used internally to get a range of cell selectors from a start selector to an end selector.
     * @private
     */
    getRange(fromSelector, toSelector, selectRecords = false) {
        const
            me            = this,
            { store }     = me,
            fromCell      = me.normalizeCellContext(fromSelector),
            toCell        = me.normalizeCellContext(toSelector),
            startRowIndex = Math.min(fromCell.rowIndex, toCell.rowIndex),
            endRowIndex   = Math.max(fromCell.rowIndex, toCell.rowIndex),
            toSelect      = [],
            startColIndex = Math.min(fromCell.columnIndex, toCell.columnIndex),
            endColIndex   = Math.max(fromCell.columnIndex, toCell.columnIndex);

        if (startRowIndex === -1 || endRowIndex === -1) {
            throw new Error('Record not found in selectRange');
        }

        // Row selection
        if (selectRecords) {
            const range = store.getRange(startRowIndex, endRowIndex + 1, false);
            // To make selectedRecords in correct order when range selecting upwards
            if (toCell.rowIndex < fromCell.rowIndex) {
                range.reverse();
            }
            toSelect.push(...range);
        }
        // Cell selection
        else {
            // Loops from start cell to end cell and creates selectors for all containing cells
            for (let rIx = startRowIndex; rIx <= endRowIndex; rIx++) {
                for (let cIx = startColIndex; cIx <= endColIndex; cIx++) {
                    toSelect.push({ rowIndex : rIx, columnIndex : cIx });
                }
            }
        }

        return toSelect.map(s => me.normalizeCellContext(s));
    }

    // endregion

    // region Update UI & trigger events

    performSelection(selectionChange, updateUI = true, silent = false) {
        const
            me = this,
            {
                selectedRecords       = [],
                selectedCells         = [],
                selectedCellRecords   = [],
                deselectedRecords     = [],
                deselectedCells       = [],
                deselectedCellRecords = [],
                action
            }                    = selectionChange,
            allSelectedRecords   = [...selectedRecords, ...selectedCellRecords],
            allDeselectedRecords = [...deselectedRecords, ...deselectedCellRecords],
            rowMode              = me.isRowSelectionMode;

        // Fire event to be able to prevent selection
        if (me.trigger('beforeSelectionChange', {
            mode          : rowMode ? 'row' : 'cell',
            action,
            selected      : allSelectedRecords,
            deselected    : allDeselectedRecords,
            selection     : me.selectedRecords,
            selectedCells,
            deselectedCells,
            cellSelection : me.selectedCells
        }) === false) {
            return;
        }

        // If deselecting all cells
        if (me._selectedCells === deselectedCells) {
            me._selectedCells   = [];
        }
        // Not deselecting all cells
        else {
            const keepCells = [];

            for (const selectedCell of me._selectedCells) {
                if (!deselectedCells.some(cellSelector => selectedCell.equals(cellSelector, true))) {
                    keepCells.push(selectedCell);
                }
            }

            me._selectedCells = keepCells;
        }

        selectionChange.deselectedRecords = [...selectionChange.deselectedRecords];

        // If deselecting all rows
        if (deselectedRecords === me._selectedRows) {
            me.changeSelectedRecordCollectionSilent(c => c.clear());
            me._selectedRows.length = 0;
        }
        // Not deselecting all rows
        else {
            const
                keepRecords      = [],
                keepInCollection = [];

            for (const selectedRecord of me.selectedRecords) {
                if (!allDeselectedRecords.some(record => selectedRecord.id === record.id)) {
                    if (me.isSelected(selectedRecord)) {
                        keepRecords.push(selectedRecord);
                    }
                    else {
                        keepInCollection.push(selectedRecord);
                    }
                }
            }

            me.changeSelectedRecordCollectionSilent(c => c.values = [...keepRecords, ...keepInCollection]);
            me._selectedRows = keepRecords;
        }

        // New selection
        if (selectedCells.length) {
            for (const selectedCell of selectedCells) {
                if (!me._selectedCells.some(cellSelector => cellSelector.equals(selectedCell, true))) {
                    me._selectedCells.push(selectedCell);
                }
            }
        }
        if (selectedRecords.length) {
            me.changeSelectedRecordCollectionSilent(c => c.add(...selectedRecords));
            me._selectedRows.push(...selectedRecords.filter(r => !me._selectedRows.some(sr => sr.id === r.id)));
        }

        if (selectedCellRecords.length) {
            me.changeSelectedRecordCollectionSilent(c => c.add(...selectedCellRecords));
        }

        if (updateUI) {
            me.refreshGridSelectionUI(selectionChange);
        }

        me.afterSelectionChange(selectionChange);

        if (!silent) {
            me.triggerSelectionChangeEvent(selectionChange);
        }
    }

    // Makes sure the DOM is up-to-date with current selection.
    refreshGridSelectionUI({ selectedRecords, selectedCells, deselectedRecords, deselectedCells }) {
        const
            me                          = this,
            { checkboxSelectionColumn } = me;

        // Row selection
        checkboxSelectionColumn?.suspendEvents();
        me.updateGridSelectionRecords(selectedRecords, true);
        me.updateGridSelectionRecords(deselectedRecords, false);
        me.updateCheckboxHeader();
        checkboxSelectionColumn?.resumeEvents();

        // Cell selection
        if (me.isCellSelectionMode) {
            me.updateGridSelectionCells(selectedCells, true);
            if (me.selectionMode.column) {
                me.updateGridSelectionColumns(selectedCells);
            }
        }
        me.updateGridSelectionCells(deselectedCells, false);
    }

    // Loops through records and updates Grid rows
    updateGridSelectionRecords(records, selected) {
        const { checkboxSelectionColumn } = this;
        if (records?.length) {
            for (let i = 0; i < records.length; i++) {
                const row = this.getRowFor(records[i]);
                if (row) {
                    row.toggleCls('b-selected', selected);
                    row.setAttribute('aria-selected', selected);
                    if (checkboxSelectionColumn && !checkboxSelectionColumn.hidden && !records[i].isSpecialRow) {
                        row.getCell(checkboxSelectionColumn.id).widget.checked = selected;
                    }
                }
            }
        }
    }

    // Loops through cell selectors and updates Grid cell's
    updateGridSelectionCells(cells, selected) {
        if (cells?.length) {
            for (let i = 0; i < cells.length; i++) {
                const cell = this.getCell(cells[i]);
                if (cell) {
                    cell.setAttribute('aria-selected', selected);
                    cell.classList.toggle('b-selected', selected);
                }
            }
        }
    }

    // Loops through columns to toggle their selected state
    updateGridSelectionColumns(selectedCells) {
        const { count } = this.store;
        for (const column of this.columns.visibleColumns) {
            column.element?.classList.toggle(
                'b-selected',
                selectedCells?.filter(s => s.columnId === column.id).length === count
            );
        }
    }

    triggerSelectionChangeEvent(selectionChange) {
        const
            {
                selectedRecords       = [],
                selectedCells         = [],
                selectedCellRecords   = [],
                deselectedRecords     = [],
                deselectedCells       = [],
                deselectedCellRecords = []
            }                    = selectionChange,
            allSelectedRecords   = [...selectedRecords, ...selectedCellRecords],
            allDeselectedRecords = [...deselectedRecords, ...deselectedCellRecords],
            rowMode              = this.isRowSelectionMode;

        this.trigger('selectionChange', {
            mode          : rowMode ? 'row' : 'cell',
            action        : selectionChange.action,
            selected      : allSelectedRecords,
            deselected    : allDeselectedRecords,
            selection     : this.selectedRecords,
            selectedCells,
            deselectedCells,
            cellSelection : this.selectedCells
        });
    }

    //endregion

    doDestroy() {
        this.selectedRecordCollection?.owner === this && this.selectedRecordCollection.destroy();
        this._selectedCells.length = 0;
        this._selectedRows.length  = 0;
        for (const detacher in this._selectionListenersDetachers) {
            this._selectionListenersDetachers[detacher]();
        }
        super.doDestroy();
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}

};
