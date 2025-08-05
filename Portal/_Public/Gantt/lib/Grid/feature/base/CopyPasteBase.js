import InstancePlugin from '../../../Core/mixin/InstancePlugin.js';
import Clipboardable from '../../../Core/mixin/Clipboardable.js';
import DateHelper from '../../../Core/helper/DateHelper.js';

/**
 * @module Grid/feature/base/CopyPasteBase
 */

/**
 * Base copy-paste functionality for row-based widgets. Not to be used directly.
 * @abstract
 * @mixes Core/mixin/Clipboardable
 */
export default class CopyPasteBase extends InstancePlugin.mixin(Clipboardable) {
    static configurable = {
        /**
         * If `true` this prevents cutting and pasting. Will default to `true` if {@link Grid/feature/CellEdit} feature
         * is disabled. Set to `false` to prevent this behaviour.
         * @config {Boolean}
         */
        copyOnly : null,

        /**
         * Default keyMap configuration: Ctrl/Cmd+c to copy, Ctrl/Cmd+x to cut and Ctrl/Cmd+v to paste. These keyboard
         * shortcuts require a selection to be made.
         * @config {Object<String,String>}
         */
        keyMap : {
            'Ctrl+C' : 'copy',
            'Ctrl+X' : 'cut',
            'Ctrl+V' : 'paste'
        },

        /**
         * Set this to `false` to not use native Clipboard API even if it is available
         * @config {Boolean}
         * @default
         */
        useNativeClipboard : false,

        /**
         * Provide a function to be able to customize the string value which is copied
         *
         * ````javascript
         * new Grid({
         *     features : {
         *         cellCopyPaste : {
         *             toCopyString({currentValue, column, record}) {
         *                 if(record.isAvatar){
         *                     return record.fullName;
         *                 }
         *                 return currentValue;
         *             }
         *         }
         *     }
         * });
         * ````
         *
         * Note that this function is only called when copying cell values or copying values from other Bryntum
         * component instances or from native clipboard.
         *
         * @param {Object} data
         * @param {String} data.currentValue
         * @param {Grid.column.Column} data.column
         * @param {Core.data.Model} data.record
         * @config {Function}
         */
        toCopyString : null,

        /**
         * Provide a function to be able to customize the value which will be set onto the record
         *
         * ````javascript
         * new Grid({
         *     features : {
         *         cellCopyPaste : {
         *             toPasteValue({currentValue, column, record, field}) {
         *                 if(typeof currentValue === 'string'){
         *                     return currentValue.replace('$', '');
         *                 }
         *                 return currentValue;
         *             }
         *         }
         *     }
         * });
         * ````
         *
         * Note that this function is only called when pasting string values, either from CellCopyPaste or copying
         * values from other Bryntum component instances or from native clipboard.
         *
         * @param {Object} data
         * @param {String} data.currentValue
         * @param {Grid.column.Column} data.column
         * @param {Core.data.Model} data.record
         * @config {Function}
         */
        toPasteValue : null,

        /**
         * If an empty value (null or empty string) is copied or cut, this config will replace that value.
         * This allows for clipboard data to skip columns.
         *
         * For example, look at these two selections
         * |  ROW  |   0  |      1       |       2      |   3  |
         * |-------|------|--------------|--------------|------|
         * | ROW 1 | SEL1 | not selected | not selected | SEL2 |
         * | ROW 2 | SEL3 | SEL4 (empty) | SEL5 (empty) | SEL6 |
         *
         * The clipboardData for `ROW 1` will look like this:
         `* SEL1\t\t\SEl2\nSEL3\t\t\SEL4`
         *
         * And `ROW 2` will look like this:
         * `SEL3\t\u{0020}\t\u{0020}\tSEL6`
         *
         * `ROW 1` will set value `SEL1` at column index 0 and `SEL2` at column index 3. This leaves column index 1 and
         * 2 untouched.
         *
         * `ROW 2` will set value `SEL3` at column index 0, `u{0020}` at column index 1 and 2, and `SEL`6 at column
         * index 3.
         *
         * The default `u{0020}` is a blank space.
         *
         * Note that this only applies when copy-pasting cell values or copying rows from other Bryntum component
         * instances or from native clipboard.
         *
         * @config {String}
         * @default
         */
        emptyValueChar : '\u{0020}',

        /**
         * The format a copied date value should have when converted to a string. To learn more about available formats,
         * check out {@link Core.helper.DateHelper} docs.
         * @config {String}
         */
        dateFormat : 'lll'
    };

    // Internal backwards compability
    get clipboardRecords() {
        return this.clipboardData || [];
    }

    /**
     * Used by CellCopyPaste and RowCopyPaste to generate string representations of grid records
     * @param cells
     * @returns {String}
     * @private
     */
    cellsToString(cells) {
        const
            me           = this;
        let lastRowIndex = 0,
            lastColIndex = 0,
            stringData   = '';

        // Sorted by rowIndex then by columnIndex
        cells.sort((c1, c2) => c1.rowIndex === c2.rowIndex ? c1.columnIndex - c2.columnIndex : c1.rowIndex - c2.rowIndex);

        for (const cell of cells) {
            const { record, column, rowIndex, columnIndex } = cell;

            // Separate with \n if new row
            if (rowIndex > lastRowIndex) {
                if (stringData.length > 0) {
                    stringData += '\n'.repeat(rowIndex - lastRowIndex);
                }
                lastRowIndex = rowIndex;
                lastColIndex = columnIndex;
            }
            // Separate with \t if new column
            else if (columnIndex > lastColIndex) {
                if (stringData.length > 0) {
                    stringData += '\t'.repeat(columnIndex - lastColIndex);
                }
                lastColIndex = columnIndex;
            }

            // The column can provide its own toClipboardString
            let cellValue = column.toClipboardString?.(cell);

            // Or we use the raw value from the record
            if (cellValue === undefined) {
                cellValue = record.getValue(column.field);
                // If a date, format it with the configured dateFormat
                if (cellValue instanceof Date) {
                    cellValue = DateHelper.format(cellValue, me.dateFormat);
                }
                else {
                    cellValue = cellValue?.toString();
                }
            }

            // The client can provide its own as well.
            if (me.toCopyString) {
                cellValue = me.toCopyString({ currentValue : cellValue, column, record });
            }

            cellValue = cellValue?.replace(/[\n\t]/, ' ');

            stringData += cellValue || me.emptyValueChar;
        }

        return stringData;
    }

    /**
     * Sets tab and new-line separated string data into records.
     * Used by CellCopyPaste to set values into existing records.
     * Used by RowCopyPaste to create new records from values
     * @param clipboardData
     * @param createNewRecords If `false`, a selected cell is required and data will be set to existing records
     * @param store The store which to set/create new data to. Defaults to the clients default store.
     * @param fields Provide an array of string fields to create records instead of using columns
     * @returns {Object} modificationData
     * @private
     */
    setFromStringData(clipboardData, createNewRecords = false, store = this.client.store, fields) {
        const
            me              = this,
            { client }      = me,
            {
                columns,
                _shiftSelectRange
            }               = client,
            modifiedRecords = new Set(),
            // Converts the clipboard data into a 2-dimensional array of string values.
            rows            = me.stringAs2dArray(clipboardData),
            selectedCell    = client.selectedCells[0],
            targetCells     = [],
            affectedCells   = [];

        // If there is a selected range, pasting should be repeated into that range
        if (!createNewRecords && _shiftSelectRange?.some(cell => cell.equals(selectedCell))) {
            const cellRows = me.cellSelectorsAs2dArray(_shiftSelectRange);

            // The selection must fit the whole paste content. If pasting 2 rows for example, a number of rows that is
            // divisible by 2 is required. Same for columns.
            if (cellRows?.length % rows.length === 0 && cellRows.columnCount % rows.columnCount === 0) {
                // This code will calculate each cell target to repeat the pasting on
                for (let curI = 0; curI < cellRows.length; curI += rows.length) {
                    for (let curX = 0; curX < cellRows.columnCount; curX += rows.columnCount) {
                        targetCells.push(cellRows[curI][curX]);
                    }
                }
            }
        }
        // No valid range, just use one target
        if (!targetCells.length) {
            targetCells.push(selectedCell);
        }

        for (const targetCell of targetCells) {
            for (let rI = 0; rI < rows.length; rI++) {
                const
                    row          = rows[rI],
                    targetRecord = createNewRecords
                        ? new store.modelClass()
                        : store.getAt(targetCell.rowIndex + rI);

                // Starts with targetCell rowIndex and columnIndex and applies values from the clipboard string.
                // \n is interpreted as rowIndex++
                // \t is interpreted as columnIndex++
                if (targetRecord && !targetRecord.readOnly) {
                    for (let cI = 0; cI < row.length; cI++) {
                        const
                            targetColumn = fields ? null
                                : columns.visibleColumns[createNewRecords ? cI : targetCell.columnIndex + cI],
                            targetField  = targetColumn?.field || fields?.[cI];
                        let value        = row[cI];

                        // If no value, this column/field is skipped
                        if (targetField && value && !targetColumn?.readOnly) {
                            if (value === me.emptyValueChar) {
                                value = null;
                            }

                            // Column provided paste conversion function
                            if (targetColumn?.fromClipboardString) {
                                value = targetColumn.fromClipboardString({
                                    string : value,
                                    record : targetRecord
                                });
                            }

                            // Client provided paste customization function
                            if (me.toPasteValue) {
                                value = me.toPasteValue({
                                    currentValue : value,
                                    record       : targetRecord,
                                    column       : targetColumn,
                                    field        : targetField
                                });
                            }

                            // If field is a dateField, parse the value with the configured dateFormat
                            if (typeof value === 'string' && targetRecord.getFieldDefinition(targetField)?.isDateDataField) {
                                const parsedDate = DateHelper.parse(value, me.dateFormat);

                                if (!isNaN(parsedDate.getTime())) {
                                    value = parsedDate;
                                }
                            }

                            targetRecord.set(targetField, value, false, false, false, true);
                            affectedCells.push(client.normalizeCellContext({ column : targetColumn, record : targetRecord }));
                        }
                    }
                    modifiedRecords.add(targetRecord);
                }
            }
        }
        return {
            modifiedRecords : [...modifiedRecords],
            targetCells     : affectedCells
        };
    }

    /**
     * Converts an array of Location objects to a two-dimensional array where first level is rows and second level is
     * columns. If the array is inconsistent in the number of columns present for each row, the function will return
     * false.
     * @param {Grid.util.Location[]} locations
     * @private
     */
    cellSelectorsAs2dArray(locations) {
        const
            rows = [];
        let rId  = null,
            columns;

        for (const location of locations) {
            // If new id (new record) create new "row"
            if (location.id !== rId) {
                rId = location.id;
                columns = [];
                rows.push(columns);
            }
            columns.push(location);
        }

        // Save number of "columns" for easier access
        rows.columnCount = rows[0].length;

        // All "rows" must have the same number of columns
        if (rows.some(row => row.length !== rows.columnCount)) {
            return false;
        }

        return rows;
    }

    /**
     * Converts a new-line- and tab-separated string to a two-dimensional array where first level is rows and second
     * level is columns. If the string is inconsistent in the number of columns present for each row, the function will
     * return false.
     * @param {String} string String values separated with new-line(\n,\r or similar) and tabs (\t)
     * @private
     */
    stringAs2dArray(string) {
        const
            rows       = [],
            stringRows = string.split(/\r\n|(?!\r\n)[\n-\r\x85\u2028\u2029]/).filter(s => s.length);

        for (const row of stringRows) {
            const columns = row.split('\t');

            // All "rows" must have the same number of columns
            if (rows.columnCount && columns.length !== rows.columnCount) {
                return false;
            }
            // Save number of "columns" for easier access
            rows.columnCount = columns.length;
            rows.push(columns);
        }
        return rows;
    }
}
