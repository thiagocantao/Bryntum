import Base from '../../../Core/Base.js';
import DomDataStore from '../../../Core/data/DomDataStore.js';
import DomHelper from '../../../Core/helper/DomHelper.js';
import Rectangle from '../../../Core/helper/util/Rectangle.js';
import EventHelper from '../../../Core/helper/EventHelper.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import BrowserHelper from '../../../Core/helper/BrowserHelper.js';
import Location from '../../util/Location.js';



const gridBodyElementEventHandlers = {
        touchstart  : 'onElementTouchStart',
        touchmove   : 'onElementTouchMove',
        touchend    : 'onElementTouchEnd',
        pointerover : 'onElementMouseOver',
        mouseout    : 'onElementMouseOut',
        mousedown   : 'onElementMouseDown',
        mousemove   : 'onElementMouseMove',
        mouseup     : 'onElementMouseUp',
        click       : 'onHandleElementClick',
        dblclick    : 'onElementDblClick',
        keyup       : 'onElementKeyUp',
        keypress    : 'onElementKeyPress',
        contextmenu : 'onElementContextMenu',
        pointerdown : 'onElementPointerDown',
        pointerup   : 'onElementPointerUp'
    },
    eventProps = [
        'pageX',
        'pageY',
        'clientX',
        'clientY',
        'screenX',
        'screenY'
    ];

function toggleHover(element, add = true) {
    element?.classList.toggle('b-hover', add);
}

function setCellHover(columnId, row, add = true) {
    row && columnId && toggleHover(row.getCell(columnId), add);
}

/**
 * @module Grid/view/mixin/GridElementEvents
 */

/**
 * Mixin for Grid that handles dom events. Some listeners fire own events but all can be chained by features. None of
 * the functions in this class are indented to be called directly.
 *
 * See {@link Grid.view.Grid} for more information on grid keyboard interaction.
 *
 * @mixin
 */
export default Target => class GridElementEvents extends (Target || Base) {
    static get $name() {
        return 'GridElementEvents';
    }

    //region Config

    static get configurable() {
        return {
            /**
             * The currently hovered grid cell
             * @member {HTMLElement}
             * @readonly
             * @category Misc
             */
            hoveredCell : null,

            /**
             * Time in ms until a longpress is triggered
             * @prp {Number}
             * @default
             * @category Misc
             */
            longPressTime : 400,

            /**
             * Set to true to listen for CTRL-Z (CMD-Z on Mac OS) keyboard event and trigger undo (redo when SHIFT is
             * pressed). Only applicable when using a {@link Core.data.stm.StateTrackingManager}.
             * @prp {Boolean}
             * @default
             * @category Misc
             */
            enableUndoRedoKeys : true,

            keyMap : {
                'Ctrl+z'       : 'undoRedoKeyPress',
                'Ctrl+Shift+z' : 'undoRedoKeyPress',
                ' '            : { handler : 'clickCellByKey', weight : 1000 }
            }
        };
    }

    //endregion

    //region Events

    /**
     * Fired when user clicks in a grid cell
     * @event cellClick
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Core.data.Model} record The record representing the row
     * @param {Grid.column.Column} column The column to which the cell belongs
     * @param {HTMLElement} cellElement The cell HTML element
     * @param {HTMLElement} target The target element
     * @param {MouseEvent} event The native DOM event
     */

    /**
     * Fired when user double clicks a grid cell
     * @event cellDblClick
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Core.data.Model} record The record representing the row
     * @param {Grid.column.Column} column The column to which the cell belongs
     * @param {HTMLElement} cellElement The cell HTML element
     * @param {HTMLElement} target The target element
     * @param {MouseEvent} event The native DOM event
     */

    /**
     * Fired when user activates contextmenu in a grid cell
     * @event cellContextMenu
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Core.data.Model} record The record representing the row
     * @param {Grid.column.Column} column The column to which the cell belongs
     * @param {HTMLElement} cellElement The cell HTML element
     * @param {HTMLElement} target The target element
     * @param {MouseEvent} event The native DOM event
     */

    /**
     * Fired when user moves the mouse over a grid cell
     * @event cellMouseOver
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Core.data.Model} record The record representing the row
     * @param {Grid.column.Column} column The column to which the cell belongs
     * @param {HTMLElement} cellElement The cell HTML element
     * @param {HTMLElement} target The target element
     * @param {MouseEvent} event The native DOM event
     */

    /**
     * Fired when a user moves the mouse out of a grid cell
     * @event cellMouseOut
     * @param {Grid.view.Grid} grid The grid instance
     * @param {Core.data.Model} record The record representing the row
     * @param {Grid.column.Column} column The column to which the cell belongs
     * @param {HTMLElement} cellElement The cell HTML element
     * @param {HTMLElement} target The target element
     * @param {MouseEvent} event The native DOM event
     */

    //endregion

    //region Event handling

    /**
     * Init listeners for a bunch of dom events. All events are handled by handleEvent().
     * @private
     * @category Events
     */
    initInternalEvents() {
        const
            handledEvents = Object.keys(gridBodyElementEventHandlers),
            len           = handledEvents.length,
            listeners     = {
                element : this.bodyElement,
                thisObj : this
            };

        // Route all events through handleEvent, so that we can capture this.event
        // before we route to the handlers
        for (let i = 0; i < len; i++) {
            const eventName = handledEvents[i];

            listeners[eventName] = {
                handler : 'handleEvent'
            };
            // Override default for touch events.
            // Other event types already have correct default.
            if (eventName.startsWith('touch')) {
                listeners[eventName].passive = false;
            }
        }

        EventHelper.on(listeners);

        EventHelper.on({
            focusin : 'onGridBodyFocusIn',
            element : this.bodyElement,
            thisObj : this,
            capture : true
        });
    }

    /**
     * This method finds the cell location of the passed event. It returns an object describing the cell.
     * @param {Event} event A Mouse, Pointer or Touch event targeted at part of the grid.
     * @param {Boolean} [includeSingleAxisMatch] Set to `true` to return a cell from xy either above or below the Grid's
     * body or to the left or right.
     * @returns {Object} An object containing the following properties:
     * - `cellElement` - The cell element clicked on.
     * - `column` - The {@link Grid.column.Column column} clicked under.
     * - `columnId` - The `id` of the {@link Grid.column.Column column} clicked under.
     * - `record` - The {@link Core.data.Model record} clicked on.
     * - `id` - The `id` of the {@link Core.data.Model record} clicked on.
     * @internal
     * @category Events
     */
    getCellDataFromEvent(event, includeSingleAxisMatch = false) {
        const
            me          = this,
            { columns } = me,
            { target }  = event;
        let cellElement = target.closest('.b-grid-cell');
        // If event coords outside of cell, this will match a cell so long as either x or y is inside a cell.
        if (!cellElement && includeSingleAxisMatch && !target.classList.contains('b-grid-row') &&
            !target.classList.contains('b-grid-subgrid')
        ) {
            const {
                top,
                left,
                right,
                bottom
            }            = me.bodyContainer.getBoundingClientRect();
            let match,
                { x, y } = event;

            // X axis correct
            if (x >= left && x <= right) {
                // y will match row either at the top or at the bottom, dependent on what the provided x is
                y = match = Math.ceil(me[`${y < top ? 'first' : 'last'}FullyVisibleRow`].element.getBoundingClientRect().y);
            }
            // Y axis correct
            else if (y >= top && y <= bottom) {
                // x will match row either at to the left or to the right, dependent on what the provided y is
                x = match = Math.ceil(columns.visibleColumns[x < left ? 0 : columns.visibleColumns.length - 1].element.getBoundingClientRect().x);
            }
            if (match !== undefined) {
                cellElement = DomHelper.childFromPoint(event.target, event.offsetX, event.offsetY)?.closest('.b-grid-cell');
            }
        }

        // There is a cell
        if (cellElement) {
            const
                cellData         = DomDataStore.get(cellElement),
                { id, columnId } = cellData,
                record           = me.store.getById(id),
                column           = columns.getById(columnId);

            // Row might not have a record, since we transition record removal
            // https://app.assembla.com/spaces/bryntum/tickets/6805
            return record ? {
                cellElement,
                cellData,
                columnId,
                id,
                record,
                column,
                cellSelector : { id, columnId }
            } : null;
        }
    }

    /**
     * This method finds the header location of the passed event. It returns an object describing the header.
     * @param {Event} event A Mouse, Pointer or Touch event targeted at part of the grid.
     * @returns {Object} An object containing the following properties:
     * - `headerElement` - The header element clicked on.
     * - `column` - The {@link Grid.column.Column column} clicked under.
     * - `columnId` - The `id` of the {@link Grid.column.Column column} clicked under.
     * @internal
     * @category Events
     */
    getHeaderDataFromEvent(event) {
        const headerElement = event.target.closest('.b-grid-header');

        // There is a header
        if (headerElement) {
            const
                headerData   = ObjectHelper.assign({}, headerElement.dataset),
                { columnId } = headerData,
                column       = this.columns.getById(columnId);

            return column ? {
                headerElement,
                headerData,
                columnId,
                column
            } : null;
        }
    }

    /**
     * Handles all dom events, routing them to correct functions (touchstart -> onElementTouchStart)
     * @param event
     * @private
     * @category Events
     */
    handleEvent(event) {
        if (!this.disabled && gridBodyElementEventHandlers[event.type]) {
            this[gridBodyElementEventHandlers[event.type]](event);
        }
    }

    //endregion

    //region Touch events

    /**
     * Touch start, chain this function in features to handle the event.
     * @param event
     * @category Touch events
     * @internal
     */
    onElementTouchStart(event) {
        const
            me       = this,
            cellData = me.getCellDataFromEvent(event);

        DomHelper.isTouchEvent = true;

        if (event.touches.length === 1) {
            me.longPressTimeout = me.setTimeout(() => {
                me.onElementLongPress(event);
                event.preventDefault();
                me.longPressPerformed = true;
            }, me.longPressTime);
        }

        if (cellData && !event.defaultPrevented) {
            me.onFocusGesture(event);
        }
    }

    /**
     * Touch move, chain this function in features to handle the event.
     * @param event
     * @category Touch events
     * @internal
     */
    onElementTouchMove(event) {
        const
            me          = this,
            {
                lastTouchTarget
            }           = me,
            touch       = event.changedTouches[0],
            {
                pageX,
                pageY
            }           = touch,
            touchTarget = document.elementFromPoint(pageX, pageY);

        if (me.longPressTimeout) {
            me.clearTimeout(me.longPressTimeout);
            me.longPressTimeout = null;
        }

        // Keep grid informed about mouseover/outs during touch-based dragging
        if (touchTarget !== lastTouchTarget) {
            if (lastTouchTarget) {
                const mouseoutEvent = new MouseEvent('mouseout', ObjectHelper.copyProperties({
                    relatedTarget : touchTarget,
                    pointerType   : 'touch',
                    bubbles       : true
                }, touch, eventProps));

                mouseoutEvent.preventDefault = () => event.preventDefault();
                lastTouchTarget?.dispatchEvent(mouseoutEvent);
            }
            if (touchTarget) {
                const mouseoverEvent = new MouseEvent('mouseover', ObjectHelper.copyProperties({
                    relatedTarget : lastTouchTarget,
                    pointerType   : 'touch',
                    bubbles       : true
                }, touch, eventProps));

                mouseoverEvent.preventDefault = () => event.preventDefault();
                touchTarget?.dispatchEvent(mouseoverEvent);
            }
        }

        me.lastTouchTarget = touchTarget;
    }

    /**
     * Touch end, chain this function in features to handle the event.
     * @param event
     * @category Touch events
     * @internal
     */
    onElementTouchEnd(event) {
        const me = this;

        if (me.longPressPerformed) {
            if (event.cancelable) {
                event.preventDefault();
            }
            me.longPressPerformed = false;
        }

        if (me.longPressTimeout) {
            me.clearTimeout(me.longPressTimeout);
            me.longPressTimeout = null;
        }
    }

    onElementLongPress(event) {}

    //endregion

    //region Mouse events

    // Trigger events in same style when clicking, dblclicking and for contextmenu
    triggerCellMouseEvent(name, event, cellData = this.getCellDataFromEvent(event)) {
        const me = this;

        // There is a cell
        if (cellData) {
            const
                column    = me.columns.getById(cellData.columnId),
                eventData = {
                    grid         : me,
                    record       : cellData.record,
                    column,
                    cellSelector : cellData.cellSelector,
                    cellElement  : cellData.cellElement,
                    target       : event.target,
                    event
                };

            me.trigger('cell' + StringHelper.capitalize(name), eventData);

            if (name === 'click') {
                column.onCellClick?.(eventData);
            }
        }
    }

    /**
     * Mouse down, chain this function in features to handle the event.
     * @param event
     * @category Mouse events
     * @internal
     */
    onElementMouseDown(event) {
        const
            me       = this,
            cellData = me.getCellDataFromEvent(event);

        me.skipFocusSelection = true;

        // If click was on a scrollbar or splitter, preventDefault to not steal focus
        if (me.isScrollbarOrRowBorderOrSplitterClick(event)) {
            event.preventDefault();
        }
        else {
            me.triggerCellMouseEvent('mousedown', event, cellData);

            // Browser event unification fires a mousedown on touch tap prior to focus.
            if (cellData && !event.defaultPrevented) {
                me.onFocusGesture(event);
            }
        }
    }

    isScrollbarOrRowBorderOrSplitterClick({ target, x, y }) {
        // Normally cells catch the click, directly on row = user clicked border, which we ignore.
        // Also ignore clicks on the virtual width element used to stretch fake scrollbar
        if (target.closest('.b-grid-splitter') || target.matches('.b-grid-row, .b-virtual-width')) {
            return true;
        }
        if (target.matches('.b-vertical-overflow')) {
            const rect = target.getBoundingClientRect();
            return x > rect.right - DomHelper.scrollBarWidth;
        }
        else if (target.matches('.b-horizontal-overflow')) {
            const rect = target.getBoundingClientRect();
            return y > rect.bottom - DomHelper.scrollBarWidth - 1; // -1 for height of virtualScrollerWidth element
        }
    }

    /**
     * Mouse move, chain this function in features to handle the event.
     * @param event
     * @category Mouse events
     * @internal
     */
    onElementMouseMove(event) {
        // Keep track of the last mouse position in case, due to OSX sloppy focusing,
        // focus is moved into the browser before a mousedown is delivered.
        // The cached mousemove event will provide the correct target in
        // GridNavigation#onGridElementFocus.
        this.mouseMoveEvent = event;
    }

    /**
     * Mouse up, chain this function in features to handle the event.
     * @param event
     * @category Mouse events
     * @internal
     */
    onElementMouseUp(event) {}

    onElementPointerDown(event) {}

    /**
     * Pointer up, chain this function in features to handle the event.
     * @param event
     * @category Mouse events
     * @internal
     */
    onElementPointerUp(event) {}

    /**
     * Called before {@link #function-onElementClick}.
     * Fires 'beforeElementClick' event which can return false to cancel further onElementClick actions.
     * @param event
     * @fires beforeElementClick
     * @category Mouse events
     * @internal
     */

    onHandleElementClick(event) {
        if (this.trigger('beforeElementClick', { event }) !== false) {
            this.onElementClick(event);
        }
    }

    /**
     * Click, select cell on click and also fire 'cellClick' event.
     * Chain this function in features to handle the dom event.
     * @param event
     * @fires cellClick
     * @category Mouse events
     * @internal
     */
    onElementClick(event) {
        const
            me       = this,
            cellData = me.getCellDataFromEvent(event);

        // There is a cell
        if (cellData) {
            me.triggerCellMouseEvent('click', event, cellData);
        }
    }

    onFocusGesture(event) {
        const
            me                    = this,
            { navigationEvent }   = me,
            { target }            = event,
            isContextMenu         = event.button === 2,
            // Interaction with tree expand/collapse icons doesn't focus
            isTreeExpander        = !isContextMenu && target.matches('.b-icon-tree-expand, .b-icon-tree-collapse'),
            // Mac OS specific behaviour: when you right click a non-active window, the window does not receive focus, but the context menu is shown.
            // So for Mac OS we treat the right click as a non-focusable action, if window is not active
            isUnfocusedRightClick = !document.hasFocus() && BrowserHelper.isMac && isContextMenu;

        // Tree expander clicks and contextmenus on unfocused windows don't focus
        if (isTreeExpander || isUnfocusedRightClick) {
            event.preventDefault();
        }
        else {
            // Used by the GridNavigation mixin to detect what interaction event if any caused
            // the focus to be moved. If it's a programmatic focus, there won't be one.
            // Grid doesn't use a Navigator which maintains this property, so we need to set it.
            me.navigationEvent = event;

            const location = new Location(target);

            // Context menu doesn't focus by default, so that needs to explicitly focus.
            // If they're re-clicking the current focus, GridNavigation#focusCell
            // still needs to know. It's a no-op, but it informs the GridSelection of the event.
            if (isContextMenu || me.focusedCell?.equals(location)) {
                let focusOptions;

                // If current event is a MouseEvent and previous event is a TouchEvent on same target, this event is
                // most likely a MouseEvent triggered by a TouchEvent, which should not trigger selection
                if (globalThis.TouchEvent && event instanceof MouseEvent && navigationEvent instanceof TouchEvent &&
                    target === navigationEvent.target
                ) {
                    focusOptions = { doSelect : false };
                }

                me.focusCell(location, focusOptions);
            }
        }
    }

    /**
     * Double click, fires 'cellDblClick' event.
     * Chain this function in features to handle the dom event.
     * @param {Event} event
     * @fires cellDblClick
     * @category Mouse events
     * @internal
     */
    onElementDblClick(event) {
        const { target } = event;

        this.triggerCellMouseEvent('dblClick', event);

        if (target.classList.contains('b-grid-header-resize-handle')) {
            const
                header = target.closest('.b-grid-header'),
                column = this.columns.getById(header.dataset.columnId);

            column.resizeToFitContent();
        }
    }

    /**
     * Mouse over, adds 'hover' class to elements.
     * @param event
     * @fires mouseOver
     * @category Mouse events
     * @internal
     */
    onElementMouseOver(event) {
        // bail out early if scrolling
        if (!this.scrolling) {
            const
                me              = this,
                { hoveredCell } = me,
                // No hover effect needed if a mouse button is pressed (like when resizing window, region, or resizing something etc).
                // NOTE: 'buttons' not supported in Safari
                shouldHover     = (typeof event.buttons !== 'number' || event.buttons === 0) && event.pointerType !== 'touch';

            let cellElement = event.target.closest('.b-grid-cell');

            // If we are entering a grid row (which probably is a row border), we should hover
            // cell/row above so not to get a blinking hovering, especially on column header
            if (!cellElement && event.target.classList.contains('b-grid-row')) {
                cellElement = DomHelper.childFromPoint(event.target, event.offsetX, event.offsetY - 2)?.closest('.b-grid-cell');
            }

            if (cellElement) {
                if (shouldHover) {
                    me.hoveredCell = cellElement;
                }

                if (!hoveredCell && me.hoveredCell) {
                    me.triggerCellMouseEvent('mouseOver', event);
                }
            }

            /**
             * Mouse moved in over element in grid
             * @event mouseOver
             * @param {MouseEvent} event The native browser event
             */
            me.trigger('mouseOver', { event });
        }
    }

    /**
     * Mouse out, removes 'hover' class from elements.
     * @param event
     * @fires mouseOut
     * @category Mouse events
     * @internal
     */
    onElementMouseOut(event) {
        const
            me                        = this,
            { hoveredCell }           =  me,
            { target, relatedTarget } = event;

        if (!(relatedTarget?.closest('.b-grid-cell') && target.matches('.b-grid-row'))) {
            // If we have not moved onto a grid row
            // (meaning over its border which is handled by getCellDataFromEvent)
            // hover any new cell we are over.
            if (relatedTarget && target.matches('.b-grid-cell') && !target.contains(relatedTarget)) {
                if (!relatedTarget?.matches('.b-grid-row')) {
                    me.hoveredCell = relatedTarget.closest('.b-grid-cell');
                }
            }
            else if (!relatedTarget?.matches('.b-grid-row,.b-grid-cell') && !me.hoveredCell?.contains(relatedTarget)) {
                me.hoveredCell = null;
            }
        }

        // bail out early if scrolling
        if (!me.scrolling) {
            if (hoveredCell && !me.hoveredCell) {
                me.triggerCellMouseEvent('mouseOut', event);
            }

            /**
             * Mouse moved out from element in grid
             * @event mouseOut
             * @param {MouseEvent} event The native browser event
             */
            me.trigger('mouseOut', { event });
        }
    }

    // The may be chained in features
    updateHoveredCell(cellElement, was) {
        const
            me                        = this,
            { selectionMode }         = me,
            rowNumberColumnId         = selectionMode.rowNumber && me.columns.find(c => c.type == 'rownumber')?.id,
            checkboxSelectionColumnId = selectionMode.checkbox && me.checkboxSelectionColumn?.id;

        // Clear last hovered cell
        if (was) {
            toggleHover(was, false);

            // Also remove hovered class on checkcol, rownumbercol and column header
            const
                prevSelector      = DomDataStore.get(was),
                { row : prevRow } = prevSelector;

            if (prevRow && !prevRow.isDestroyed) {
                setCellHover(rowNumberColumnId, prevRow, false);
                setCellHover(checkboxSelectionColumnId, prevRow, false);
            }

            if (prevSelector?.columnId) {
                toggleHover(me.columns.getById(prevSelector.columnId)?.element, false);
            }
        }

        // Clears hovered row
        // Only remove cls if row isn't destroyed
        if (me._hoveredRow && !me._hoveredRow.isDestroyed) {
            me._hoveredRow.removeCls('b-hover');
        }
        me._hoveredRow = null;

        // Set hovered
        if (cellElement && !me.scrolling) {
            const
                selector = DomDataStore.get(cellElement),
                { row }  = selector;

            if (row) {
                // Set cell if cell selection mode is on
                if (selectionMode.cell && selector.columnId !== rowNumberColumnId && selector.columnId !== checkboxSelectionColumnId) {
                    toggleHover(cellElement);

                    // In cell selection mode:
                    // Also "hover" checkcolumn cell if such exists
                    setCellHover(checkboxSelectionColumnId, row);
                    // And also rownumbercolumn cell
                    setCellHover(rowNumberColumnId, row);
                    // And also column header
                    toggleHover(me.columns.getById(selector.columnId)?.element);
                }
                // Else row
                else {
                    me._hoveredRow = row;
                    row.addCls('b-hover');
                }
            }
            else {
                me.hoveredCell = null;
            }
        }
    }

    //endregion

    //region Keyboard events

    // Hooks on to keyMaps keydown-listener to be able to run before
    keyMapOnKeyDown(event) {
        if (this.element.contains(event.target)) {
            this.onElementKeyDown(event);
            super.keyMapOnKeyDown(event);
        }
    }

    /**
     * To catch all keydowns. For more specific keydown actions, use keyMap.
     * @param event
     * @category Keyboard events
     * @internal
     */
    onElementKeyDown(event) {
        // If some other function flagged the event as handled, we ignore it.
        if (event.handled || !this.element.contains(event.target)) {
            return;
        }

        const
            me          = this,
            // Read this to refresh cached reference in case this keystroke lead to the removal of current row
            focusedCell = me.focusedCell;

        if (focusedCell?.isCell && !focusedCell.isActionable) {
            const
                cellElement = focusedCell.cell;

            // If a cell is focused and column is interested - call special callback
            me.columns.getById(cellElement.dataset.columnId).onCellKeyDown?.({ event, cellElement });
        }
    }

    undoRedoKeyPress(event) {
        const { stm } = this.store;
        if (stm && this.enableUndoRedoKeys && !this.features.cellEdit?.isEditing) {
            stm.onUndoKeyPress(event);
            return true;
        }
        return false;
    }

    // Trigger column.onCellClick when space bar is pressed
    clickCellByKey(event) {
        const
            me          = this,
            // Read this to refresh cached reference in case this keystroke lead to the removal of current row
            focusedCell = me.focusedCell,
            cellElement = focusedCell?.cell,
            column      = me.columns.getById(cellElement.dataset.columnId);

        if (focusedCell?.isCell && !focusedCell.isActionable) {
            if (column.onCellClick) {
                column.onCellClick({
                    grid   : me,
                    column,
                    record : me.store.getById(focusedCell.id),
                    cellElement,
                    target : event.target,
                    event
                });
                return true;
            }
        }
        return false;
    }

    /**
     * Key press, chain this function in features to handle the dom event.
     * @param event
     * @category Keyboard events
     * @internal
     */
    onElementKeyPress(event) {}

    /**
     * Key up, chain this function in features to handle the dom event.
     * @param event
     * @category Keyboard events
     * @internal
     */
    onElementKeyUp(event) {}

    //endregion

    //region Other events

    /**
     * Context menu, chain this function in features to handle the dom event.
     * In most cases, include ContextMenu feature instead.
     * @param event
     * @category Other events
     * @internal
     */
    onElementContextMenu(event) {
        const
            me       = this,
            cellData = me.getCellDataFromEvent(event);

        // There is a cell
        if (cellData) {
            me.triggerCellMouseEvent('contextMenu', event, cellData);

            // Focus on tap for touch events.
            // Selection follows from focus.
            if (DomHelper.isTouchEvent) {
                me.onFocusGesture(event);
            }
        }
    }

    /**
     * Overrides empty base function in View, called when view is resized.
     * @fires resize
     * @param element
     * @param width
     * @param height
     * @param oldWidth
     * @param oldHeight
     * @category Other events
     * @internal
     */
    onInternalResize(element, width, height, oldWidth, oldHeight) {
        const me = this;

        if (me._devicePixelRatio && me._devicePixelRatio !== globalThis.devicePixelRatio) {
            // Pixel ratio changed, likely because of browser zoom. This affects the relative scrollbar width also
            DomHelper.resetScrollBarWidth();
        }

        me._devicePixelRatio = globalThis.devicePixelRatio;
        // cache to avoid recalculations in the middle of rendering code (RowManger#getRecordCoords())
        me._bodyRectangle    = Rectangle.client(me.bodyContainer);

        super.onInternalResize(...arguments);

        if (height !== oldHeight) {
            me._bodyHeight = me.bodyContainer.offsetHeight;
            if (me.isPainted) {
                // initial height will be set from render(),
                // it reaches onInternalResize too early when rendering, headers/footers are not sized yet
                me.rowManager.initWithHeight(me._bodyHeight);
            }
        }
        me.refreshVirtualScrollbars();

        if (width !== oldWidth) {
            // Slightly delay to avoid resize loops.
            me.setTimeout(() => {
                if (!me.isDestroyed) {
                    me.updateResponsive(width, oldWidth);
                }
            }, 0);
        }
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
