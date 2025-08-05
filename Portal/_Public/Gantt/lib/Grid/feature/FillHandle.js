import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import Delayable from '../../Core/mixin/Delayable.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import Location from '../../Grid/util/Location.js';
import GlobalEvents from '../../Core/GlobalEvents.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';

/**
 * @module Grid/feature/FillHandle
 */

/**
 * This features adds a fill handle to a Grid range selection, which when dragged, fills the cells being dragged over
 * with values based on the values in the original selected range. This is similar to functionality normally seen in
 * various spreadsheet applications.
 *
 * {@inlineexample Grid/feature/FillHandle.js}
 *
 * Requires {@link Grid/view/Grid#config-selectionMode selectionMode.cell} to be activated.
 *
 * This feature is **disabled** by default
 *
 * ```javascript
 * const grid = new Grid({
 *     features : {
 *         fillHandle : true
 *     }
 * });
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype fillHandle
 * @feature
 */
export default class FillHandle extends InstancePlugin.mixin(Delayable) {
    static $name = 'FillHandle';

    static configurable = {
        /**
         * Implement this function to be able to customize the value that cells will be filled with.
         * Return `undefined` to use default calculations.
         *
         * ````javascript
         * new Grid({
         *    features : {
         *        fillHandle : {
         *           calculateFillValue({cell, column, range, record}) {
         *              if(column.field === 'number') {
         *                 return range.reduce(
         *                    (sum, location) => sum + location.record[location.column.field]
         *                 );
         *              }
         *           }
         *        }
         *    }
         * });
         * ````
         *
         * @param {Object} data Object containing information about current cell and fill value
         * @param {Grid.util.Location} data.cell Current cell data
         * @param {Grid.column.Column} data.column Current cell column
         * @param {Grid.util.Location[]} data.range Range from where to calculate values
         * @param {Core.data.Model} data.record Current cell record
         * @returns {String|Number|Date} Value to fill current cell
         * @config {Function}
         */
        calculateFillValue : null,

        /**
         * Set to `true` to enable the fill range to crop the original selected range. This clears the cells which were
         * a part of the original selected range, but are no longer a part of the smaller range.
         * @config {Boolean}
         */
        allowCropping : false

    };

    // Plugin configuration. This plugin chains/overrides some functions in Grid.
    static pluginConfig = {
        chain    : ['afterSelectionChange', 'onContentChange', 'afterColumnsChange', 'fixElementHeights'],
        override : ['getCellDataFromEvent']
    };

    afterConstruct() {
        super.afterConstruct();

        if (!this.client.selectionMode.cell) {
            this.disabled = true;
        }

        this._fillListeners = {};
    }

    delayable = {
        handleSelection : 'raf'
    };

    onContentChange() {
        this.handleSelection();
    }

    afterColumnsChange() {
        this.handleSelection();
    }

    fixElementHeights() {
        this.handleSelection();
    }

    getCellDataFromEvent(event, includeSingleAxisMatch) {
        if (includeSingleAxisMatch) {
            includeSingleAxisMatch = !event.target.classList.contains('b-fill-handle');
        }
        return this.overridden.getCellDataFromEvent(event, includeSingleAxisMatch);
    }

    // region Pattern recognition
    findPatternsIn2dRange(range, horizontal, negative) {
        const values = {};

        // Converts a cellselector range to values per column or row
        for (const cell of range) {
            const id = horizontal ? cell.id : cell.columnId;
            let value = cell.record.getValue(cell.column.field);

            // If a number string, convert to number
            if (value && typeof value === 'string' && !isNaN(value)) {
                value = parseFloat(value);
            }

            if (!values[id]) {
                values[id] = [];
            }
            values[id].push(value);
        }

        // Find patterns for each column or row in range
        for (const rowOrCol in values) {
            values[rowOrCol].pattern = this.findPatternsIn1dRange(values[rowOrCol], negative);
        }

        return values;
    }

    findPatternsIn1dRange(range, negative) {
        const
            lastValue = range[negative ? 0 : (range.length - 1)],
            pattern   = {
                next : () => lastValue,
                lastValue
            };

        // If all values in same column/row is either number or date
        if (range.every(val => typeof val === 'number') || range.every(val => val instanceof Date)) {
            const diffs = range.map((val, ix) => val - range[ix - 1]);
            diffs.shift(); // Removes initial NaN

            // Found a repeating pattern
            if (new Set(diffs).size === 1) {
                pattern.increaseBy = diffs[0] * (negative ? -1 : 1);

                pattern.next = () => {
                    if (pattern.lastValue instanceof Date) {
                        pattern.lastValue = new Date(pattern.lastValue.getTime() + pattern.increaseBy);
                    }
                    else {
                        pattern.lastValue += pattern.increaseBy;
                    }
                    return pattern.lastValue;
                };
            }
        }
        // Else it's treated as a string value
        else if (range.length > 1) {
            pattern.stringPattern = [...range];
            pattern.next = () => {
                if (pattern.currentIndex === undefined) {
                    pattern.currentIndex = 0;
                }
                else {
                    pattern.currentIndex += 1;
                    if (pattern.currentIndex >= pattern.stringPattern.length) {
                        pattern.currentIndex = 0;
                    }
                }
                return pattern.stringPattern[pattern.currentIndex];
            };
        }
        return pattern;
    }

    // endregion

    afterSelectionChange() {
        const me = this;

        if (me.client.readOnly) {
            me.removeElements();
            return;
        }

        // If selection isn't finished, wait for mouse up and then add fill elements
        if (GlobalEvents.isMouseDown()) {
            me.client.delayUntilMouseUp(() => me.handleSelection(true));
            // Remove prev elements immediately in this case
            me.removeElements();
        }
        // Otherwise, add fill elements immediately
        else {
            me.handleSelection(true);
        }
    }

    /**
     * Checks selection and sees to it that fill handle and border is drawn.
     * Runs on next animation frame
     * @internal
     */
    handleSelection() {
        if (!this._isExtending) {
            const range = this.rangeSelection;

            if (range) {
                this.drawFillHandleAndBorder(range[0], range[range.length - 1]);
            }
            else {
                this.removeElements();
            }
        }
    }

    // region Mouse events

    // On fillHandle mouse down only
    onMouseDown(event) {
        const { client } = this;

        if (!client.readOnly) {
            this._fillListeners.mouseMoveOrUp = EventHelper.on({
                element   : globalThis,
                mouseover : {
                    handler : 'onMouseOver',
                    element : client.selectionDragMouseEventListenerElement
                },
                mouseup : 'onMouseUp',
                thisObj : this
            });
            event.preventDefault();
            event.stopImmediatePropagation();
            event.handled = true;
        }
    }

    // Responsible for doing the filling
    onMouseUp() {
        const
            me              = this,
            {
                client,
                currentRange,
                _isCropping
            }               = me,
            range           = me.rangeSelection,
            selectionChange = range && currentRange && client.internalSelectRange(currentRange.from, currentRange.to),
            selectedCells   = selectionChange?.selectedCells || [],
            // For extending : Only modify cells that are not a part of original range
            // For cropping  : Only clear cells that are not a part of new selection
            extensionCells  = _isCropping ? me.croppingCells
                : selectedCells.filter(cell => !range.some(sel => sel.equals(cell, true)));

        delete me._isCropping; // Removing flag in case we bail out early

        if (me._isExtending) {
            client.disableScrollingCloseToEdges(client.items);
            delete me._isExtending;
        }

        // If no extension, do nothing
        if (!extensionCells?.length) {
            me.handleSelection();
            return;
        }

        client.suspendRefresh();

        // If trimming (inverted extension), clear cells that where previously selected and not a part of new selection
        if (_isCropping) {
            extensionCells.forEach(cell => cell.record.set(cell.column.field, null, false, false, false, true));
        }
        // Extending cell values depending on pattern
        else {
            const
                [firstCell] = extensionCells,
                // If extensioncells has a record that is included in original selection, then we are dragging horizontally
                horizontal  = range.some(sel => sel.record === firstCell.record),
                // negative in this aspect, means dragging either upwards or to the left depending on horizontal or vertical
                negative    = horizontal
                    ? firstCell.columnIndex < range[0].columnIndex
                    : firstCell.rowIndex < range[0].rowIndex,
                patterns    = me.findPatternsIn2dRange(range, horizontal, negative),
                changeMap   = new Map();

            if (negative) {
                extensionCells.reverse();
            }

            for (const cell of extensionCells) {
                const { column, record } = cell;

                if (!column.readOnly && column.canFillValue({ range, record, cell })) {
                    let value   = me.calculateFillValue?.({ range, column, record, cell }),
                        changed = changeMap.get(record);

                    if (!changed) {
                        changed = {};
                        changeMap.set(record, changed);
                    }

                    if (value === undefined) {
                        const pattern = patterns[horizontal ? cell.id : cell.columnId].pattern;
                        value = pattern.next();
                    }

                    changed[column.field] = column.calculateFillValue?.({ value, record, range }) || value;
                }
            }

            for (const [record, changes] of changeMap) {
                record.set(changes, null, null, null, null, true);
            }
        }

        client.resumeRefresh(true);

        // Selects the extended area
        client.performSelection(selectionChange);

        delete me.currentRange;
        me.handleSelection();
    }

    // The fill border and handle should refresh on mouse move
    onMouseOver(event) {
        const
            me           = this,
            {
                client,
                rangeSelection
            }            = me,
            first        = rangeSelection[0],
            last         = rangeSelection[rangeSelection.length - 1],
            cellData     = client.getCellDataFromEvent(event, true);
        let cellSelector = cellData && client.normalizeCellContext(cellData.cellSelector);

        if (cellSelector?._column?.region === first._column.region) {
            const
                equalOrSmaller = rangeSelection.some(cs => cs.equals(cellSelector, true));
            let negative;

            if (!me._isExtending) {
                client.enableScrollingCloseToEdges(client.items);
            }

            if (equalOrSmaller) {
                // If were smaller, were cropping (if it's allowed)
                me._isCropping = me.allowCropping &&
                    (cellSelector.rowIndex < last.rowIndex || cellSelector.columnIndex < last.columnIndex);
            }
            else {
                // If cellSelector is on a row in range, endSelector should be current column but end/first row
                if (cellSelector.rowIndex >= first.rowIndex && cellSelector.rowIndex <= last.rowIndex) {
                    negative     = first.columnIndex > cellSelector.columnIndex;
                    cellSelector = new Location({
                        grid   : client,
                        record : negative ? first.record : last.record,
                        column : cellSelector.column
                    });
                }
                // Else endSelector should be current row but end/first column
                else {
                    negative     = first.rowIndex > cellSelector.rowIndex;
                    cellSelector = new Location({
                        grid   : client,
                        record : cellSelector.record,
                        column : negative ? first.column : last.column
                    });
                }
            }

            // negative means that current mouse over cell is above or to the left
            const
                // If negative, draw from calculated mouse over cell
                // otherwise, draw from top-left selection cell
                from = negative ? cellSelector : first,
                // If negative or were inside selection (but not cropping), draw to bottom-right selection cell
                // otherwise, draw to calculated mouse over cell
                to   = negative || (equalOrSmaller && !me._isCropping) ? last : cellSelector;

            me.currentRange = { from, to };

            // This flag is true even if were trimming
            me._isExtending = true;

            me.drawFillHandleAndBorder(from, to, true);
        }
    }

    // endregion

    // region Creating, updating and removing fillhandle and fillborder
    drawFillHandleAndBorder(from, to, keepListeners = false) {
        const
            me        = this,
            {
                client,
                currentRange,
                _fillListeners
            }         = me,
            regionEl  = client.subGrids[from.column.region].element,
            { x }     = Rectangle.from(from.cell || from.column.element, regionEl),
            { right } = Rectangle.from(to.cell || to.column.element, regionEl),
            { y }     = client.getRecordCoords(from.record, true),
            bottom    = client.getRecordCoords(to.record, true).bottom - 1;
        let {
            borderElement,
            handleElement
        }             = me;

        me.removeElements(keepListeners);

        if (!borderElement) {
            me.borderElement = borderElement = DomHelper.createElement({
                className : 'b-fill-selection-border'
            });

            me.handleElement = handleElement = DomHelper.createElement({
                className : 'b-fill-handle'
            });
        }

        DomHelper.setRect(borderElement, { y, x, width : (right - x), height : (bottom - y) });
        regionEl.appendChild(borderElement);

        // If fill handle is drawn at right edge, put it to the left instead
        DomHelper.setTopLeft(handleElement, bottom, right >= regionEl.scrollWidth ? x : right);
        regionEl.appendChild(handleElement);

        // Remove all previously cropping cls
        me.toggleCroppingCls(false);
        delete me.croppingCells;

        // If were cropping, we should add cls class to the cells that will be "shrunk"
        if (me._isCropping && me.rangeSelection?.length) {
            const newCells = client.getRange(currentRange.from, currentRange.to);

            me.croppingCells = me.rangeSelection.filter(sel => !newCells.some(cell => cell.equals(sel, true)));
            me.toggleCroppingCls();
        }

        if (!_fillListeners.handleClick) {
            _fillListeners.handleClick = EventHelper.on({
                element   : client.rootElement,
                delegate  : '.b-fill-handle',
                mousedown : 'onMouseDown',
                thisObj   : me
            });
        }

        me.hasFillElements = true;
    }

    toggleCroppingCls(add = true) {
        this.croppingCells?.forEach(sel => this.client.getCell(sel)?.classList.toggle('b-indicate-crop', add));
    }

    removeElements(keepListeners = false) {
        const me = this;

        me.handleElement?.remove();
        me.borderElement?.remove();

        if (!keepListeners) {
            me.removeListeners();
        }

        me.hasFillElements = false;
    }

    // Detach listeners
    removeListeners() {
        const me = this;

        for (const listener in me._fillListeners) {
            me._fillListeners[listener]();
        }
        me._fillListeners = {};
    }

    // endregion

    // Gets current selection range. Only allows for single range or single cell.
    get rangeSelection() {
        const
            { client }        = this,
            { selectedCells } = client,
            range             = client._shiftSelectRange ?? (selectedCells.length === 1 && selectedCells);

        // We only got one selected range, nothing else selected
        // Only allow fill handle on single region selection
        if (!client._selectedRows.length && range?.length && range.length === selectedCells.length &&
            range.every(c1 => selectedCells.some(c2 => c1.equals(c2, true)) &&
                c1._column.parent && c1._column.region === range[0]._column.region && client.store.isAvailable(c1.id)
            )
        ) {
            return range;
        }

        return null;
    }
}

GridFeatureManager.registerFeature(FillHandle);
