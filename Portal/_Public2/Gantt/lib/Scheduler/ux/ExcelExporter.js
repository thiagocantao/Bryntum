import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import InstancePlugin from '../../Common/mixin/InstancePlugin.js';
import DateHelper from '../../Common/helper/DateHelper.js';
import ObjectHelper from '../../Common/helper/ObjectHelper.js';
import DomHelper from '../../Common/helper/DomHelper.js';

/*global zipcelx*/
/*eslint no-undef: "error"*/

/**
 * @module Scheduler/ux/ExcelExporter
 */

/**
 * **Note:** This a full copy of Grid/ux/ExcelExporter but adapted for Scheduler. Should be extended from Grid version if it's moved to core.
 *
 * A plugin that allows exporting Scheduler data to Excel without involving the server.
 * It uses zipcelx library (https://www.npmjs.com/package/zipcelx)
 * forked and adjusted to support column width config https://github.com/TrancePaul/zipcelx/tree/column-width-build
 * and Microsoft XML specification (https://msdn.microsoft.com/en-us/library/office/documentformat.openxml.spreadsheet.aspx)
 *
 * Here is an example of how to add the feature:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         excelExporter : {
 *             // Choose the date format for date fields
 *             dateFormat : 'YYYY-MM-DD HH:mm',
 *             // Choose the Resource fields to include in the exported file
 *             resourceColumns : [{ text : 'Staff', field : 'name' }],
 *             // Choose the Event fields to include in the exported file
 *             eventColumns    : [
 *                 { text : 'Task', field : 'name' },
 *                 { text : 'Starts', field : 'startDate', width : 140 },
 *                 { text : 'Ends', field : 'endDate', width : 140 }
 *             ]
 *         }
 *     }
 * });
 * ```
 *
 * And how to call it:
 *
 * ```javascript
 * scheduler.features.excelExporter.export({
 *     filename : 'Export'
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
            filename : 'schedule',

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
             * Set to `false` to do not include events that have no assignment to the export. `true` by default.
             * @config {Boolean} includeUnassigned
             * @default
             */
            includeUnassigned : true,

            /**
             * An array of Resource columns configuration used to specify columns width, headers name, and column fields to get the data from.
             * 'field' config is required. If 'text' is missing, it will try to get it retrieved from the scheduler locked column or the 'field' config.
             * If 'width' is missing, it will try to get it retrieved from the scheduler locked column or {@link #config-defaultColumnWidth} config.
             * If no Resource columns provided and no Event columns provided, columns will be generated from the scheduler locked columns.
             *
             * For example:
             * ```javascript
             * resourceColumns : [
             *     { text : 'Staff', field : 'name' }
             * ]
             * ```
             *
             * @config {String[]|Object[]} resourceColumns
             * @default
             */
            resourceColumns : null,

            /**
             * An array of Event columns configuration used to specify columns width, headers name, and column fields to get the data from.
             * 'field' config is required. If 'text' is missing, the 'field' config will be used instead.
             * If 'width' is missing, {@link #config-defaultColumnWidth} config value will be used.
             * If no Resource columns provided and no Event columns provided, columns will be generated from the scheduler locked columns.
             *
             * For example:
             * ```javascript
             * eventColumns    : [
             *     { text : 'Task', field : 'name' },
             *     { text : 'Starts', field : 'startDate', width : 140 },
             *     { text : 'Ends', field : 'endDate', width : 140 }
             * ]
             * ```
             *
             * @config {String[]|Object[]} eventColumns
             * @default
             */
            eventColumns : [
                { text : 'Task', field : 'name' },
                { text : 'Starts', field : 'startDate', width : 140 },
                { text : 'Ends', field : 'endDate', width : 140 }
            ]
        };
    }

    /**
     * The main export function to generate and download *.xlsx file
     * @param {Object} config A configuration for a specific export.
     * @param {String} [config.filename] A file name for a specific export. If omitted the value will be taken from the {@link #config-filename} config.
     * @param {String[]|Object[]} [config.resourceColumns] An array of resource columns configuration for a specific export. If omitted the value will be taken from the {@link #config-resourceColumns} config.
     * @param {String[]|Object[]} [config.eventColumns] An array of event columns configuration for a specific export. If omitted the value will be taken from the {@link #config-eventColumns} config.
     */
    export(config = {}) {
        const
            me                 = this,
            filename           = config.filename || me.filename,
            eventColumnsCfg    = config.eventColumns || me.eventColumns,
            resourceColumnsCfg = config.resourceColumns || me.resourceColumns || me.scheduler.columns.bottomColumns.filter(rec => {
                return rec.data.locked;
            }).map((record) => record.data),
            resourceColumns    = me.generateColumns(resourceColumnsCfg, true),
            eventColumns       = me.generateColumns(eventColumnsCfg),
            columns            = resourceColumns.concat(eventColumns),
            rows               = me.generateRowData(columns, resourceColumns, eventColumns);

        zipcelx({
            filename : filename,
            sheet    : {
                data : [columns].concat(rows),
                cols : columns
            }
        });
    }

    construct(scheduler, config) {
        this.scheduler = scheduler;

        //<debug>
        if (!zipcelx) {
            throw new Error('ExcelExporter: "zipcelx" library is required');
        }
        //</debug>

        super.construct(scheduler, config);
    }

    generateColumns(columnsCfg = [], isResourceColumn = false) {
        const nbrColumns     = columnsCfg.length,
            schedulerColumns = this.scheduler.columns,
            columns          = [];

        for (let i = 0; i < nbrColumns; i++) {
            // Transform columnsCfg to an object
            const col = columnsCfg[i] ? (typeof columnsCfg[i] === 'string' ? { field : columnsCfg[i] } : ObjectHelper.clone(columnsCfg[i])) : {};

            //<debug>
            if (!col.field) {
                throw new Error('ExcelExporter: "field" config is required for all columns');
            }
            //</debug>

            let { field, text : value, width } = col;

            // If name or width is missing try to retrieve them from the scheduler column and the field, or use default values.
            if (!value || !width) {
                const schedulerColumn = isResourceColumn && schedulerColumns.find((schedulerCol) => schedulerCol.field === field);

                if (!value) {
                    value = schedulerColumn && schedulerColumn.text || field;
                }

                if (!width) {
                    width = schedulerColumn && schedulerColumn.width || this.defaultColumnWidth;
                }
            }

            // zipcelx expects 'value' as a cell text https://github.com/egeriis/zipcelx/wiki/The-config-object
            columns.push({ field, value, width, type : 'string', isResourceColumn });
        }

        return columns;
    }

    generateRowData(columns, resourceColumns, eventColumns) {
        const
            me   = this,
            rows = [];

        // forEach skips group records, summary records etc
        me.scheduler.resourceStore.map(resourceRecord => {
            // Get all events for resource (including assignment store)
            const events = resourceRecord.events || [];

            // Set dummy event to have resource info printed without events
            if (!events.length) {
                events.push('');
            }

            events.forEach(eventRecord => rows.push(me.getRowData(columns, resourceRecord, eventRecord)));
        });

        const notAssignedEvents = me.scheduler.eventStore.query(eventRecord => {
            return !eventRecord.resources.length &&
                // this extra check is needed until eventRecord.resources skips grouped and collapced resources
                // checked by ExcelExport.t.js when it can be removed
                !me.scheduler.resourceStore.idRegister[eventRecord.resourceId];
        });

        if (me.includeUnassigned && eventColumns.length && notAssignedEvents.length) {
            const cells = [];

            // Use offset to match first event column
            for (let i = 0; i < resourceColumns.length; i++) {
                cells.push({ value : '', type : 'string' });
            }

            cells.push({ value : me.L('No resource assigned'), type : 'string' });

            rows.push(cells);

            // Set dummy resource to have event info printed without resource
            notAssignedEvents.forEach(eventRecord => rows.push(me.getRowData(columns, '', eventRecord)));
        }

        // filter out empty rows
        return rows.filter(cells => cells.length);
    }

    getRowData(cols, resource, event) {
        const cells = [];

        for (let i = 0; i < cols.length; i++) {
            const columnFieldName  = cols[i].field,
                isResourceColumn = cols[i].isResourceColumn,
                record           = isResourceColumn ? resource : event;

            let value = '';

            if (record && record.meta.specialRow) {
                if (this.showGroupHeader && record.meta.groupRowFor) {
                    value = this.scheduler.features.group.buildGroupHeader({
                        // Create dummy element to get html from
                        cellElement : DomHelper.createElement(),
                        persist     : true,
                        record
                    });

                    cells.push({ value, type : 'string' });
                }

                // break column loop, no need to print out anything else in this row
                break;
            }
            else if (record) {
                value = record[columnFieldName];

                if (value instanceof Date) {
                    value = DateHelper.format(value, this.dateFormat);
                }
            }

            cells.push({ value, type : 'string' });
        }

        return cells;
    }
}

GridFeatureManager.registerFeature(ExcelExporter, false, 'Scheduler');

export default ExcelExporter;
