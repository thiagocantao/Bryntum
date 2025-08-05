import Base from '../../../Core/Base.js';
import Rectangle from '../../../Core/helper/util/Rectangle.js';
import Location from '../../util/Location.js';
import DomHelper from '../../../Core/helper/DomHelper.js';
import GlobalEvents from '../../../Core/GlobalEvents.js';
import VersionHelper from '../../../Core/helper/VersionHelper.js';

/**
 * @module Grid/view/mixin/GridNavigation
 */

const
    defaultFocusOptions = Object.freeze({}),
    disableScrolling = Object.freeze({
        x : false,
        y : false
    }),
    containedFocusable = function(e) {
        // When we step outside of the target cell, throw.
        // The TreeWalker silences the exception and terminates the traverse.
        if (!this.focusableFinderCell.contains(e)) {
            return DomHelper.NodeFilter.FILTER_REJECT;
        }
        if (DomHelper.isFocusable(e) && !e.disabled) {
            return DomHelper.NodeFilter.FILTER_ACCEPT;
        }
        return DomHelper.NodeFilter.FILTER_SKIP;
    };

/**
 * Mixin for Grid that handles cell to cell navigation.
 *
 * See {@link Grid.view.Grid} for more information on grid cell keyboard navigation.
 *
 * @mixin
 */
export default Target => class GridNavigation extends (Target || Base) {
    static get $name() {
        return 'GridNavigation';
    }

    static configurable =  {
        focusable : false,

        focusableSelector : '.b-grid-cell,.b-grid-header.b-depth-0',

        // Set to true to revert focus on Esc or on ArrowUp/ArrowDown above/below first/last row
        isNested : false,

        // Documented on Grid
        keyMap : {
            ArrowUp    : { handler : 'navigateUp', weight : 10 },
            ArrowRight : { handler : 'navigateRight', weight : 10 },
            ArrowDown  : { handler : 'navigateDown', weight : 10 },
            ArrowLeft  : { handler : 'navigateLeft', weight : 10 },

            'Ctrl+Home' : 'navigateFirstCell',
            Home        : 'navigateFirstColumn',
            'Ctrl+End'  : 'navigateLastCell',
            End         : 'navigateLastColumn',
            PageUp      : 'navigatePrevPage',
            PageDown    : 'navigateNextPage',
            Enter       : 'activateHeader',

            // Private
            Escape      : { handler : 'onEscape', weight : 10 },
            'Shift+Tab' : { handler : 'onShiftTab', preventDefault : false, weight : 200 },
            Tab         : { handler : 'onTab', preventDefault : false, weight : 200 },
            ' '         : { handler : 'onSpace', preventDefault : false }
        }
    };

    onStoreRecordIdChange(event) {
        super.onStoreRecordIdChange?.(event);

        const
            { focusedCell }     = this,
            { oldValue, value } = event;

        // https://github.com/bryntum/support/issues/4935
        if (focusedCell && focusedCell.id === oldValue) {
            focusedCell._id = value;
        }
    }

    /**
     * Called by the RowManager when the row which contains the focus location is derendered.
     *
     * This keeps focus in a consistent place.
     * @protected
     */
    onFocusedRowDerender() {
        const
            me              = this,
            { focusedCell } = me;

        if (focusedCell?.id != null && focusedCell.cell) {
            const isActive = focusedCell.cell.contains(DomHelper.getActiveElement(me));

            if (me.hideHeaders) {
                if (isActive) {
                    me.revertFocus();
                }
            }
            else {
                const headerContext = me.normalizeCellContext({
                    rowIndex    : -1,
                    columnIndex : isActive ? focusedCell.columnIndex : 0
                });

                // The row contained focus, focus the corresponding header
                if (isActive) {
                    me.focusCell(headerContext);
                }
                else {
                    headerContext.cell.tabIndex = 0;
                }
            }
            focusedCell.cell.tabIndex = -1;
        }
    }

    navigateFirstCell() {
        this.focusCell(Location.FIRST_CELL);
    }

    navigateFirstColumn() {
        this.focusCell(Location.FIRST_COLUMN);
    }

    navigateLastCell() {
        this.focusCell(Location.LAST_CELL);
    }

    navigateLastColumn() {
        this.focusCell(Location.LAST_COLUMN);
    }

    navigatePrevPage() {
        this.focusCell(Location.PREV_PAGE);
    }

    navigateNextPage() {
        this.focusCell(Location.NEXT_PAGE);
    }

    activateHeader(keyEvent) {
        if (keyEvent.target.classList.contains('b-grid-header') && this.focusedCell.isColumnHeader) {
            const { column } = this.focusedCell;

            column.onKeyDown?.(keyEvent);

            this.getHeaderElement(column.id).click();
        }
        return false;
    }

    onEscape(keyEvent) {
        const
            me              = this,
            { focusedCell } = me;

        if (!keyEvent.target.closest('.b-dragging') && focusedCell?.isActionable) {
            // The escape must not be processed by handlers for the cell we are about to focus.
            // We need to just push focus upwards to the cell, and stop there.
            keyEvent.stopImmediatePropagation();

            // To prevent the focusCell from being rejected as a no-op
            me._focusedCell = null;

            // Focus the cell with an explicit request to not jump in
            me.focusCell({
                rowIndex : focusedCell.rowIndex,
                column   : focusedCell.column
            }, {
                disableActionable : true
            });
        }
        // If configured as nested, revert focus to outer widget
        // The owner can supply a function to catch the focus. Used in rowExpander.
        else if (me.isNested && me.owner && !me.owner.catchFocus?.({ source : me })) {
            me.revertFocus(true);
        }
    }

    onTab(keyEvent) {
        const
            { target } = keyEvent,
            {
                focusedCell,
                bodyElement
            }          = this,
            {
                isActionable,
                actionTargets
            }          = focusedCell,
            isEditable = isActionable && DomHelper.isEditable(target) && !target.readOnly;

        // If we're on the last editable in a cell, TAB navigates right
        if (isEditable && target === actionTargets[actionTargets.length - 1]) {
            keyEvent.preventDefault();
            this.navigateRight(keyEvent);
        }
        // If we're *on* a cell, or on last subtarget, TAB moves off the grid.
        // Temporarily hide the grid body, and let TAB take effect from there
        else if (!isActionable || target === actionTargets[actionTargets.length - 1]) {
            bodyElement.style.display = 'none';
            this.requestAnimationFrame(() => bodyElement.style.display = '');

            // So that Navigator#onKeyDown does not continue to preventDefault;
            return false;
        }
    }

    onShiftTab(keyEvent) {
        const
            me = this,
            { target } = keyEvent,
            {
                focusedCell,
                bodyElement
            }   = me,
            {
                cell,
                isActionable,
                actionTargets
            } = focusedCell,
            isEditable  = isActionable && DomHelper.isEditable(target) && !target.readOnly,
            onFirstCell = focusedCell.columnIndex === 0 && focusedCell.rowIndex === (me.hideHeaders ? 0 : -1);

        // If we're on the first editable in a cell that is not the first cell, SHIFT+TAB navigates left
        if (!onFirstCell && isEditable && target === actionTargets[0]) {
            keyEvent.preventDefault();
            me.navigateLeft(keyEvent);
        }

        // If we're *on* a cell, or on first subtarget, SHIFT+TAB moves off the grid.
        else if (!isActionable || target === actionTargets[0]) {
            // Focus the first header cell and then let the key's default action take its course
            const f = !onFirstCell && !me.hideHeaders && me.focusCell({
                rowIndex : -1,
                column   : 0
            }, {
                disableActionable : true
            });

            // If that was successful then reset the tabIndex
            if (f) {
                f.cell.tabIndex = -1;
                cell.tabIndex = 0;
                me._focusedCell = focusedCell;
            }
            // Otherwise, temporarily hide the grid body, and let TAB take effect from there
            else {
                bodyElement.style.display = 'none';
                me.requestAnimationFrame(() => bodyElement.style.display = '');
            }

            // So that Navigator#onKeyDown does not continue to preventDefault;
            return false;
        }
    }

    onSpace(keyEvent) {
        // SPACE scrolls, so disable that
        if (!this.focusedCell.isActionable) {
            keyEvent.preventDefault();
        }
        // Return false to tell keyMap that any other actions should be called
        return false;
    }

    //region Cell

    /**
     * Triggered when a user navigates to a grid cell
     * @event navigate
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Grid.util.Location} last The previously focused location
     * @param {Grid.util.Location} location The new focused location
     * @param {Event} [event] The UI event which caused navigation.
     */

    /**
     * Grid Location which encapsulates the currently focused cell.
     * Set to focus a cell or use {@link #function-focusCell}.
     * @property {Grid.util.Location}
     */
    get focusedCell() {
        return this._focusedCell;
    }

    /**
     * This property is `true` if an element _within_ a cell is focused.
     * @property {Boolean}
     * @readonly
     */
    get isActionableLocation() {
        return this._focusedCell?.isActionable;
    }

    set focusedCell(cellSelector) {
        this.focusCell(cellSelector);
    }

    get focusedRecord() {
        return this._focusedCell?.record;
    }

    /**
     * CSS selector for currently focused cell. Format is "[data-index=index] [data-column-id=columnId]".
     * @property {String}
     * @readonly
     */
    get cellCSSSelector() {
        const cell = this._focusedCell;

        return cell ? `[data-index=${cell.rowIndex}] [data-column-id=${cell.columnId}]` : '';
    }

    afterHide() {
        super.afterHide(...arguments);

        // Do not scroll back to the last focused cell/last moused over cell upon reshow
        this.lastFocusedCell = null;
    }

    /**
     * Checks whether a cell is focused.
     * @param {LocationConfig|String|Number} cellSelector Cell selector { id: x, columnId: xx } or row id
     * @returns {Boolean} true if cell or row is focused, otherwise false
     */
    isFocused(cellSelector) {
        return Boolean(this._focusedCell?.equals(this.normalizeCellContext(cellSelector)));
    }

    get focusElement() {
        if (!this.isDestroying) {
            let focusCell;

            // If the store is not empty, focusedCell can return the closest cell
            if (this.store.count && this._focusedCell) {
                focusCell = this._focusedCell.target;
            }
            // If the store is empty, or we have had no focusedCell set, focus a column header.
            else {
                focusCell = this.normalizeCellContext({
                    rowIndex    : -1,
                    columnIndex : this._focusedCell?.columnIndex || 0
                }).target;
            }

            const superFocusEl = super.focusElement;

            // If there's no cell, or the Container's focus element is before the cell
            // use the Container's focus element.
            // For example, we may have a top toolbar.
            if (superFocusEl && (!focusCell || focusCell.compareDocumentPosition(superFocusEl) === Node.DOCUMENT_POSITION_PRECEDING)) {
                return superFocusEl;
            }

            return focusCell;
        }
    }

    onPaint({ firstPaint }) {
        const me = this;

        super.onPaint?.(...arguments);

        // Make the grid initally tabbable into.
        // The first cell has to have the initial roving tabIndex set into it.
        const defaultFocus = this.normalizeCellContext({
            rowIndex : me.hideHeaders ? 0 : -1,
            column   : me.hideHeaders ? 0 : me.columns.find(col => !col.hidden && col.isFocusable)
        });

        if (defaultFocus.cell) {
            defaultFocus._isDefaultFocus = true;

            me._focusedCell = defaultFocus;

            const { target } = defaultFocus;

            // If cell doesn't contain a focusable target, it needs tabIndex 0.
            if (target === defaultFocus.cell) {
                defaultFocus.cell.tabIndex = 0;
            }
        }
    }

    /**
     * This function handles focus moving into, or within the grid.
     * @param {Event} focusEvent
     * @private
     */
    onGridBodyFocusIn(focusEvent) {
        const
            me              = this,
            { bodyElement } = me,
            lastFocusedCell = me.focusedCell,
            lastTarget      = lastFocusedCell?.initialTarget || lastFocusedCell?.target,
            {
                target,
                relatedTarget
            }               = focusEvent,
            targetCell      = target.closest(me.focusableSelector);

        // If focus moved into a valid cell...
        // Only allows mouse left och right clicks (no other mouse buttons)
        if (targetCell &&
            (!GlobalEvents.currentMouseDown || GlobalEvents.isMouseDown(0) || GlobalEvents.isMouseDown(2))
        ) {
            const
                cellSelector  = new Location(target),
                { cell }      = cellSelector,
                lastCell      = lastFocusedCell?.cell,
                actionTargets = cellSelector.actionTargets = me.findFocusables(targetCell),
                // Don't select on focus on a contained actionable location
                doSelect      = (!me._fromFocusCell || me.selectOnFocus) && (target === cell || me._selectActionCell) && !target?._isRevertingFocus;

            // https://github.com/bryntum/support/issues/4039
            // Only try focusing cell is current target cell is getting removed
            if (!me.store.getById(targetCell.parentNode.dataset.id) && cell !== targetCell) {
                cell.focus({ preventScroll : true });
                return;
            }

            if (target.matches(me.focusableSelector)) {
                if (me.disableActionable) {
                    cellSelector._target = cell;
                }
                // Focus first focusable target if we are configured to.
                else if (actionTargets.length) {
                    me._selectActionCell = GlobalEvents.currentMouseDown?.target === target;
                    actionTargets[0].focus();
                    delete me._selectActionCell;
                    return;
                }
            }
            else {
                // If we have tabbed in and *NOT* mousedowned in, and hit a tabbable element which was not our
                // last focused cell, go back to last focused cell.
                if (lastFocusedCell?.target &&
                    relatedTarget &&
                    (!GlobalEvents.isMouseDown() || !bodyElement.contains(GlobalEvents.currentMouseDown?.target)) &&
                    !bodyElement.contains(relatedTarget) &&
                    !cellSelector.equals(lastFocusedCell)
                ) {
                    lastTarget.focus();
                    return;
                }
                cellSelector._target = target;
            }

            if (lastCell) {
                lastCell.classList.remove('b-focused');
                lastCell.tabIndex = -1;
            }
            if (cell) {
                cell.classList.add('b-focused');

                // Column may update DOM on cell focus for A11Y purposes.
                cellSelector.column.onCellFocus(cellSelector);

                // Only switch the cell to be tabbable if focus was not directed to an inner focusable.
                if (cell === target) {
                    cell.tabIndex = 0;
                }

                // Moving back to a cell from a cell-contained Editor
                if (cell.contains(focusEvent.relatedTarget)) {
                    if (lastTarget === target) {
                        return;
                    }
                }
            }

            //Remember
            me._focusedCell = cellSelector;

            me.onCellNavigate?.(me, lastFocusedCell, cellSelector, doSelect);

            me.trigger('navigate', { lastFocusedCell, focusedCell : cellSelector, event : focusEvent });

        }
        // Focus not moved into a valid cell, refocus last cell's target
        // if there was a previously focused cell.
        // Allow text selection inside a row expander body
        else if (!target.closest('.b-rowexpander-body')) {
            lastTarget?.focus();
        }
    }

    findFocusables(cell) {
        const
            { focusableFinder } = this,
            result              = [];

        focusableFinder.currentNode = this.focusableFinderCell = cell;

        for (let focusable = focusableFinder.nextNode(); focusable; focusable = focusableFinder.nextNode()) {
            result.push(focusable);
        }
        return result;
    }

    get focusableFinder() {
        const me = this;

        if (!me._focusableFinder) {
            me._focusableFinder = me.setupTreeWalker(me.bodyElement, DomHelper.NodeFilter.SHOW_ELEMENT, {
                acceptNode : containedFocusable.bind(me)
            });
        }

        return me._focusableFinder;
    }

    /**
     * Sets the passed record as the current focused record for keyboard navigation and selection purposes.
     * This API is used by Combo to activate items in its picker.
     * @param {Core.data.Model|Number|String} activeItem The record, or record index, or record id to highlight as the active ("focused") item.
     * @internal
     */
    restoreActiveItem(item = this._focusedCell) {
        if (this.rowManager.count) {
            // They sent a row number.
            if (!isNaN(item)) {
                item = this.store.getAt(item);
            }
            // Still not a record, treat it as a record ID.
            else if (!item.isModel) {
                item = this.store.getById(item);
            }
            return this.focusCell(item);
        }
    }

    /**
     * Navigates to a cell and/or its row (depending on selectionMode)
     * @param {LocationConfig} cellSelector Cell location descriptor
     * @param {Object} options Modifier options for how to deal with focusing the cell. These
     * are used as the {@link Core.helper.util.Scroller#function-scrollTo} options.
     * @param {ScrollOptions|Boolean} [options.scroll=true] Pass `false` to not scroll the cell into view, or a
     * scroll options object to affect the scroll.
     * @returns {Grid.util.Location} A Location object representing the focused location.
     * @fires navigate
     */
    focusCell(cellSelector, options = defaultFocusOptions) {
        const
            me               = this,
            { _focusedCell } = me,
            {
                scroll,
                disableActionable
            }                = options,
            isDown           = cellSelector === Location.DOWN,
            isUp             = cellSelector === Location.UP;

        // If we're being asked to go to a nonexistent header row, revert focus outwards
        if (cellSelector?.rowIndex === -1 && me.hideHeaders) {
            me.revertFocus();
            return;
        }

        // Get a Grid Location.
        // If the cellSelector is a number, it is taken to be a "relative" location as defined
        // in the Location class eg Location.UP, and we move the current focus accordingly.
        cellSelector = typeof cellSelector === 'number' && _focusedCell?.isLocation ? _focusedCell.move(cellSelector) : me.normalizeCellContext(cellSelector);

        const doSelect = ('doSelect' in options) ? options.doSelect
            : (!cellSelector.isActionable || cellSelector.initialTarget === cellSelector.cell);

        if (cellSelector.equals(_focusedCell)) {

            // If configured as nested, revert focus outside if navigating by keyboard below last row or above headers
            if (me.isNested && (isDown || isUp)) {
                // The owner can supply a function to catch the focus. Used in rowExpander.
                if (!me.owner?.catchFocus?.({ source : me, navigationDirection : isDown ? 'down' : 'up' })) {
                    me.revertFocus(true);
                }
            }
            else {
                // Request is a no-op, but it's still a navigate request which selection processing needs to know about
                me.onCellNavigate?.(me, _focusedCell, cellSelector, doSelect);
            }
            return _focusedCell;
        }

        const
            subGrid     = me.getSubGridFromColumn(cellSelector.columnId),
            { cell }    = cellSelector,
            testCell    = cell || me.getCell({
                rowIndex : me.rowManager.topIndex,
                columnId : cellSelector.columnId
            }),
            subGridRect = Rectangle.from(subGrid.element),
            bodyRect    = Rectangle.from(me.bodyElement),
            cellRect    = Rectangle.from(testCell).moveTo(null, subGridRect.y);

        // No scrolling possible if we're moving to a column header
        if (scroll === false || cellSelector.rowIndex === -1) {
            options = Object.assign({}, options, disableScrolling);
        }
        else {
            options = Object.assign({}, options, scroll);

            // If the test cell is larger than the subGrid, in any dimension, disable scrolling
            if (cellRect.width > subGridRect.width || cellRect.height > bodyRect.height) {
                options.x = options.y = false;
            }
            // Else ask for the column to be scrolled into view
            else {
                options.column = cellSelector.columnId;
            }

            me.scrollRowIntoView(cellSelector.id, options);
        }

        // Clear hovering upon navigating so to not have hover style stick around when keyboard navigating away
        if (me._hoveredRow || me.hoveredCell) {
            me.hoveredCell = null;
        }

        // Disable auto stepping into the focused cell.
        me.disableActionable = disableActionable;

        // Go through select pathway upon focus
        me.selectOnFocus = doSelect;

        // To let onGridBodyFocusIn know where the focus originates
        me._fromFocusCell = true;

        // Focus the location's target, be it a cell, or an interior element.
        // The onFocusIn element in this module responds to this.
        cellSelector[disableActionable ? 'cell' : 'target']?.focus({ preventScroll : true });

        me.disableActionable = me.selectOnFocus = false;
        delete me._fromFocusCell;

        return cellSelector;
    }

    blurCell(cellSelector) {
        const me   = this,
            cell = me.getCell(cellSelector);

        if (cell) {
            cell.classList.remove('b-focused');
        }
    }

    clearFocus(fullClear) {
        const me = this;

        if (me._focusedCell) {
            // set last to have focus return to previous cell when alt tabbing
            me.lastFocusedCell = fullClear ? null : me._focusedCell;

            me.blurCell(me._focusedCell);
            me._focusedCell = null;
        }
    }

    // For override-ability
    catchFocus() {}

    /**
     * Selects the cell before or after currently focused cell.
     * @private
     * @param next Specify true to select the next cell, false to select the previous
     * @returns {Object} Used cell selector
     */
    internalNextPrevCell(next = true) {
        const
            me           = this,
            cellSelector = me._focusedCell;

        if (cellSelector) {
            return me.focusCell({
                id       : cellSelector.id,
                columnId : me.columns.getAdjacentVisibleLeafColumn(cellSelector.column, next, true).id
            });
        }
        return null;
    }

    /**
     * Select the cell after the currently focused one.
     * @param {Event} [event] [DEPRECATED] unused param
     * @returns {Grid.util.Location} Cell selector
     */
    navigateRight() {
        if (arguments[0]?.fromKeyMap) {
            return this.focusCell(this.rtl ? Location.PREV_CELL : Location.NEXT_CELL);
        }
        if (arguments[0]) {
            VersionHelper.deprecate('Grid', '6.0.0', 'Event argument removed, unused param');
        }
        return this.internalNextPrevCell(!this.rtl);
    }

    /**
     * Select the cell before the currently focused one.
     * @param {Event} [event] [DEPRECATED] unused param
     * @returns {Grid.util.Location} Cell selector
     */
    navigateLeft() {
        if (arguments[0]?.fromKeyMap) {
            return this.focusCell(this.rtl ? Location.NEXT_CELL : Location.PREV_CELL);
        }
        if (arguments[0]) {
            VersionHelper.deprecate('Grid', '6.0.0', 'Event argument removed, unused param');
        }
        return this.internalNextPrevCell(Boolean(this.rtl));
    }

    //endregion

    //region Row

    /**
     * Selects the next or previous record in relation to the current selection. Scrolls into view if outside.
     * @private
     * @param next Next record (true) or previous (false)
     * @param {Boolean} [skipSpecialRows=true] True to not return specialRows like headers
     * @param {Boolean} [moveToHeader=true] True to allow focus to move to a header
     * @returns {Grid.util.Location|Boolean} Selection context for the focused row (& cell) or false if no selection was made
     */
    internalNextPrevRow(next, skipSpecialRows = true, moveToHeader = true) {
        const
            me   = this,
            cell = me._focusedCell;

        if (!cell) return false;

        const record = me.store[`get${next ? 'Next' : 'Prev'}`](cell.id, false, skipSpecialRows);

        if (record) {
            return me.focusCell({
                id       : record.id,
                columnId : cell.columnId,
                scroll   : {
                    x : false
                }
            });
        }
        else if (!next && moveToHeader && !cell.isColumnHeader) {
            this.clearFocus();
            this.getHeaderElement(cell.columnId).focus();
        }

        return false;
    }

    /**
     * Navigates to the cell below the currently focused cell
     * @param {Event} [event] [DEPRECATED] unused param
     * @returns {Grid.util.Location} Selector for focused row (& cell)
     */
    navigateDown() {
        if (arguments[0]?.fromKeyMap) {
            return this.focusCell(Location.DOWN);
        }
        if (arguments[0]) {
            VersionHelper.deprecate('Grid', '6.0.0', 'Event argument removed, unused param');
        }
        return this.internalNextPrevRow(true, false);
    }

    /**
     * Navigates to the cell above the currently focused cell
     * @param {Event} [event] [DEPRECATED] unused param
     * @returns {Grid.util.Location} Selector for focused row (& cell)
     */
    navigateUp() {
        if (arguments[0]?.fromKeyMap) {
            return this.focusCell(Location.UP);
        }
        if (arguments[0]) {
            VersionHelper.deprecate('Grid', '6.0.0', 'Event argument removed, unused param');
        }
        return this.internalNextPrevRow(false, false);
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
