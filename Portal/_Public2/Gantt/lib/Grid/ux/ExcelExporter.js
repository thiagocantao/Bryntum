import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import InstancePlugin from '../../Common/mixin/InstancePlugin.js';
import DateHelper from '../../Common/helper/DateHelper.js';
import ObjectHelper from '../../Common/helper/ObjectHelper.js';
import DomHelper from '../../Common/helper/DomHelper.js';

/*global zipcelx*/
/*eslint no-undef: "error"*/

/**
 * @module Grid/ux/ExcelExporter
 */

/**
 * A plugin that allows exporting Grid data to Excel without involving the server.
 * It uses zipcelx library (https://www.npmjs.com/package/zipcelx)
 * forked and adjusted to support column width config https://github.com/TrancePaul/zipcelx/tree/column-width-build
 * and Microsoft XML specification (https://msdn.microsoft.com/en-us/library/office/documentformat.openxml.spreadsheet.aspx)
 *
 * Here is an example of how to add the feature:
 *
 * ```javascript
 * const grid = new Grid({
 *     features : {
 *         excelExporter : {
 *             // Choose the date format for date fields
 *             dateFormat : 'YYYY-MM-DD HH:mm',
 *             // Choose the columns to include in the exported file
 *             columns : ['name', 'role']
 *         }
 *     }
 * });
 * ```
 *
 * And how to call it:
 *
 * ```javascript
 * grid.features.excelExporter.export({
 *     filename : 'Export',
 *     columns : [
 *         { text : 'First Name', field : 'firstName', width : 90 },
 *         { text : 'Age', field : 'age', width : 40 },
 *         { text : 'Starts', field : 'start', width : 140 },
 *         { text : 'Ends', field : 'finish', width : 140 }
 *     ]
 * })
 * ```
 *
 * @extends Common/mixin/InstancePlugin
 * @demo exporttoexcel
 */
class ExcelExporter extends InstancePlugin {
    static get defaultConfig() {
        return {
            /**
             * An export file name
             * @config {String} filename
             * @default
             */
            filename : 'grid',

            /**
             * Defines how date in a cell will be formatted
             * @config {String} dateFormat
             * @default
             */
            dateFormat : 'YYYY-MM-DD',

            /**
             * Specifies a default column width if no width specified
             * @config {Number} defaultColumnWidth
             * @default
             */
            defaultColumnWidth : 100,

            /**
             * If true and the grid is grouped, shows the grouped value in the first column. True by default.
             * @config {Boolean} showGroupHeader
             * @default
             */
            showGroupHeader : true,

            /**
             * An array of columns configuration used to specify columns width, headers name, and column fields to get the data from.
             * 'field' config is required. If 'text' is missing, it will try to get it retrieved from the grid column or the 'field' config.
             * If 'width' is missing, it will try to get it retrieved from the grid column or {@link #config-defaultColumnWidth} config.
             * If no columns provided the config will be generated from the grid columns.
             *
             * For example:
             * ```javascript
             * columns : [
             *     'firstName', // field
             *     'age', // field
             *     { text : 'Starts', field : 'start', width : 140 },
             *     { text : 'Ends', field : 'finish', width : 140 }
             * ]
             * ```
             *
             * @config {String[]|Object[]} columns
             * @default
             */
            columns : []
        };
    }

    /**
     * The main export function to generate and download *.xlsx file
     * @param {Object} config A configuration for a specific export.
     * @param {String} [config.filename] A file name for a specific export. If omitted the value will be taken from the {@link #config-filename} config.
     * @param {String[]|Object[]} [config.columns] An array of columns configuration for a specific export. If omitted the value will be taken from the {@link #config-columns} config.
     */
    export(config = {}) {
        const filename = config.filename || this.filename,
            columnsCfg = config.columns || this.columns,
            columns    = this.generateColumns(columnsCfg),
            rows       = this.generateRowData(columns);

        zipcelx({
            filename : filename,
            sheet    : {
                data : [columns].concat(rows),
                cols : columns
            }
        });
    }

    construct(grid, config) {
        this.grid = grid;

        //<debug>
        if (!zipcelx) {
            throw new Error('ExcelExporter: "zipcelx" library is required');
        }
        //</debug>

        super.construct(grid, config);
    }

    generateColumns(columnsCfg = []) {
        const gridColumns = this.grid.columns,
            columns       = [];

        let nbrColumns = columnsCfg.length;

        // if no columns provided take grid columns as a config
        if (!nbrColumns) {
            columnsCfg = gridColumns.map(rec => rec.data);
            nbrColumns = columnsCfg.length;
        }

        for (let i = 0; i < nbrColumns; i++) {
            // Transform columnsCfg to an object
            const col = columnsCfg[i] ? (typeof columnsCfg[i] === 'string' ? { field : columnsCfg[i] } : ObjectHelper.clone(columnsCfg[i])) : {};

            //<debug>
            if (!col.field) {
                throw new Error('ExcelExporter: "field" config is required for all columns');
            }
            //</debug>

            let { field, text : value, width } = col;

            // If name or width is missing try to retrieve them from the grid column and the field, or use default values.
            if (!value || !width) {
                const gridColumn = gridColumns.find((gridCol) => gridCol.field === field);

                if (!value) {
                    value = gridColumn && gridColumn.text || field;
                }

                if (!width) {
                    width = gridColumn && gridColumn.width || this.defaultColumnWidth;
                }
            }

            // zipcelx expects 'value' as a cell text https://github.com/egeriis/zipcelx/wiki/The-config-object
            columns.push({ field, value, width, type : 'string' });
        }

        return columns;
    }

    generateRowData(columns) {
        const nbrColumns = columns.length;

        if (!nbrColumns) {
            return [];
        }

        const rows = this.grid.store.map((record) => {
            const cells = [];

            if (record.meta.specialRow) {
                if (this.showGroupHeader && record.meta.groupRowFor) {
                    const value = this.grid.features.group.buildGroupHeader({
                        // Create dummy element to get html from
                        cellElement : DomHelper.createElement(),
                        persist     : true,
                        record
                    });

                    cells.push({ value, type : 'string' });
                }
            }
            else {
                for (let i = 0; i < nbrColumns; i++) {
                    let value = record[columns[i].field];

                    if (value instanceof Date) {
                        value = DateHelper.format(value, this.dateFormat);
                    }

                    cells.push({ value, type : 'string' });
                }
            }

            return cells;
        });

        // filter out empty rows
        return rows.filter(cells => cells.length);
    }
}

GridFeatureManager.registerFeature(ExcelExporter, false, 'Grid');

export default ExcelExporter;
