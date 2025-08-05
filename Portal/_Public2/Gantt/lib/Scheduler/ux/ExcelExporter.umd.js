"use strict";

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _get(target, property, receiver) { if (typeof Reflect !== "undefined" && Reflect.get) { _get = Reflect.get; } else { _get = function _get(target, property, receiver) { var base = _superPropBase(target, property); if (!base) return; var desc = Object.getOwnPropertyDescriptor(base, property); if (desc.get) { return desc.get.call(receiver); } return desc.value; }; } return _get(target, property, receiver || target); }

function _superPropBase(object, property) { while (!Object.prototype.hasOwnProperty.call(object, property)) { object = _getPrototypeOf(object); if (object === null) break; } return object; }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

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
 * @extends Common/mixin/bryntum.scheduler.InstancePlugin
 * @demo exporttoexcel
 */
var ExcelExporter =
/*#__PURE__*/
function (_bryntum$scheduler$In) {
  _inherits(ExcelExporter, _bryntum$scheduler$In);

  function ExcelExporter() {
    _classCallCheck(this, ExcelExporter);

    return _possibleConstructorReturn(this, _getPrototypeOf(ExcelExporter).apply(this, arguments));
  }

  _createClass(ExcelExporter, [{
    key: "export",

    /**
     * The main //export function to generate and download *.xlsx file
     * @param {Object} config A configuration for a specific export.
     * @param {String} [config.filename] A file name for a specific export. If omitted the value will be taken from the {@link #config-filename} config.
     * @param {String[]|Object[]} [config.resourceColumns] An array of resource columns configuration for a specific export. If omitted the value will be taken from the {@link #config-resourceColumns} config.
     * @param {String[]|Object[]} [config.eventColumns] An array of event columns configuration for a specific export. If omitted the value will be taken from the {@link #config-eventColumns} config.
     */
    value: function _export() {
      var config = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      var me = this,
          filename = config.filename || me.filename,
          eventColumnsCfg = config.eventColumns || me.eventColumns,
          resourceColumnsCfg = config.resourceColumns || me.resourceColumns || me.scheduler.columns.bottomColumns.filter(function (rec) {
        return rec.data.locked;
      }).map(function (record) {
        return record.data;
      }),
          resourceColumns = me.generateColumns(resourceColumnsCfg, true),
          eventColumns = me.generateColumns(eventColumnsCfg),
          columns = resourceColumns.concat(eventColumns),
          rows = me.generateRowData(columns, resourceColumns, eventColumns);
      zipcelx({
        filename: filename,
        sheet: {
          data: [columns].concat(rows),
          cols: columns
        }
      });
    }
  }, {
    key: "construct",
    value: function construct(scheduler, config) {
      this.scheduler = scheduler; //<debug>

      if (!zipcelx) {
        throw new Error('ExcelExporter: "zipcelx" library is required');
      } //</debug>


      _get(_getPrototypeOf(ExcelExporter.prototype), "construct", this).call(this, scheduler, config);
    }
  }, {
    key: "generateColumns",
    value: function generateColumns() {
      var _this = this;

      var columnsCfg = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : [];
      var isResourceColumn = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
      var nbrColumns = columnsCfg.length,
          schedulerColumns = this.scheduler.columns,
          columns = [];

      var _loop = function _loop(i) {
        // Transform columnsCfg to an object
        var col = columnsCfg[i] ? typeof columnsCfg[i] === 'string' ? {
          field: columnsCfg[i]
        } : bryntum.scheduler.ObjectHelper.clone(columnsCfg[i]) : {}; //<debug>

        if (!col.field) {
          throw new Error('ExcelExporter: "field" config is required for all columns');
        } //</debug>


        var field = col.field,
            value = col.text,
            width = col.width; // If name or width is missing try to retrieve them from the scheduler column and the field, or use default values.

        if (!value || !width) {
          var schedulerColumn = isResourceColumn && schedulerColumns.find(function (schedulerCol) {
            return schedulerCol.field === field;
          });

          if (!value) {
            value = schedulerColumn && schedulerColumn.text || field;
          }

          if (!width) {
            width = schedulerColumn && schedulerColumn.width || _this.defaultColumnWidth;
          }
        } // zipcelx expects 'value' as a cell text https://github.com/egeriis/zipcelx/wiki/The-config-object


        columns.push({
          field: field,
          value: value,
          width: width,
          type: 'string',
          isResourceColumn: isResourceColumn
        });
      };

      for (var i = 0; i < nbrColumns; i++) {
        _loop(i);
      }

      return columns;
    }
  }, {
    key: "generateRowData",
    value: function generateRowData(columns, resourceColumns, eventColumns) {
      var me = this,
          rows = []; // forEach skips group records, summary records etc

      me.scheduler.resourceStore.map(function (resourceRecord) {
        // Get all events for resource (including assignment store)
        var events = resourceRecord.events || []; // Set dummy event to have resource info printed without events

        if (!events.length) {
          events.push('');
        }

        events.forEach(function (eventRecord) {
          return rows.push(me.getRowData(columns, resourceRecord, eventRecord));
        });
      });
      var notAssignedEvents = me.scheduler.eventStore.query(function (eventRecord) {
        return !eventRecord.resources.length && // this extra check is needed until eventRecord.resources skips grouped and collapced resources
        // checked by ExcelExport.t.js when it can be removed
        !me.scheduler.resourceStore.idRegister[eventRecord.resourceId];
      });

      if (me.includeUnassigned && eventColumns.length && notAssignedEvents.length) {
        var cells = []; // Use offset to match first event column

        for (var i = 0; i < resourceColumns.length; i++) {
          cells.push({
            value: '',
            type: 'string'
          });
        }

        cells.push({
          value: me.L('No resource assigned'),
          type: 'string'
        });
        rows.push(cells); // Set dummy resource to have event info printed without resource

        notAssignedEvents.forEach(function (eventRecord) {
          return rows.push(me.getRowData(columns, '', eventRecord));
        });
      } // filter out empty rows


      return rows.filter(function (cells) {
        return cells.length;
      });
    }
  }, {
    key: "getRowData",
    value: function getRowData(cols, resource, event) {
      var cells = [];

      for (var i = 0; i < cols.length; i++) {
        var columnFieldName = cols[i].field,
            isResourceColumn = cols[i].isResourceColumn,
            record = isResourceColumn ? resource : event;
        var value = '';

        if (record && record.meta.specialRow) {
          if (this.showGroupHeader && record.meta.groupRowFor) {
            value = this.scheduler.features.group.buildGroupHeader({
              // Create dummy element to get html from
              cellElement: bryntum.scheduler.DomHelper.createElement(),
              persist: true,
              record: record
            });
            cells.push({
              value: value,
              type: 'string'
            });
          } // break column loop, no need to print out anything else in this row


          break;
        } else if (record) {
          value = record[columnFieldName];

          if (value instanceof Date) {
            value = bryntum.scheduler.DateHelper.format(value, this.dateFormat);
          }
        }

        cells.push({
          value: value,
          type: 'string'
        });
      }

      return cells;
    }
  }], [{
    key: "defaultConfig",
    get: function get() {
      return {
        /**
         * An //export file name
         * @config {String} filename
         * @default
         */
        filename: 'schedule',

        /**
         * Defines how date in a cell will be formatted
         * @config {String} dateFormat
         * @default
         */
        dateFormat: 'YYYY-MM-DD',

        /**
         * Specifies a default column width if no width specified
         * @config {Number} defaultColumnWidth
         * @default
         */
        defaultColumnWidth: 100,

        /**
         * If true and the grid is grouped, shows the grouped value in the first column. True by default.
         * @config {Boolean} showGroupHeader
         * @default
         */
        showGroupHeader: true,

        /**
         * Set to `false` to do not include events that have no assignment to the export. `true` by default.
         * @config {Boolean} includeUnassigned
         * @default
         */
        includeUnassigned: true,

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
        resourceColumns: null,

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
        eventColumns: [{
          text: 'Task',
          field: 'name'
        }, {
          text: 'Starts',
          field: 'startDate',
          width: 140
        }, {
          text: 'Ends',
          field: 'endDate',
          width: 140
        }]
      };
    }
  }]);

  return ExcelExporter;
}(bryntum.scheduler.InstancePlugin);

bryntum.scheduler.GridFeatureManager.registerFeature(ExcelExporter, false, 'Scheduler'); //export default ExcelExporter;