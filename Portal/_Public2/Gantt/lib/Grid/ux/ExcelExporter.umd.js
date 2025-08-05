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
 * @extends Common/mixin/bryntum.grid.InstancePlugin
 * @demo exporttoexcel
 */
var ExcelExporter =
/*#__PURE__*/
function (_bryntum$grid$Instanc) {
  _inherits(ExcelExporter, _bryntum$grid$Instanc);

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
     * @param {String[]|Object[]} [config.columns] An array of columns configuration for a specific export. If omitted the value will be taken from the {@link #config-columns} config.
     */
    value: function _export() {
      var config = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      var filename = config.filename || this.filename,
          columnsCfg = config.columns || this.columns,
          columns = this.generateColumns(columnsCfg),
          rows = this.generateRowData(columns);
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
    value: function construct(grid, config) {
      this.grid = grid; //<debug>

      if (!zipcelx) {
        throw new Error('ExcelExporter: "zipcelx" library is required');
      } //</debug>


      _get(_getPrototypeOf(ExcelExporter.prototype), "construct", this).call(this, grid, config);
    }
  }, {
    key: "generateColumns",
    value: function generateColumns() {
      var _this = this;

      var columnsCfg = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : [];
      var gridColumns = this.grid.columns,
          columns = [];
      var nbrColumns = columnsCfg.length; // if no columns provided take grid columns as a config

      if (!nbrColumns) {
        columnsCfg = gridColumns.map(function (rec) {
          return rec.data;
        });
        nbrColumns = columnsCfg.length;
      }

      var _loop = function _loop(i) {
        // Transform columnsCfg to an object
        var col = columnsCfg[i] ? typeof columnsCfg[i] === 'string' ? {
          field: columnsCfg[i]
        } : bryntum.grid.ObjectHelper.clone(columnsCfg[i]) : {}; //<debug>

        if (!col.field) {
          throw new Error('ExcelExporter: "field" config is required for all columns');
        } //</debug>


        var field = col.field,
            value = col.text,
            width = col.width; // If name or width is missing try to retrieve them from the grid column and the field, or use default values.

        if (!value || !width) {
          var gridColumn = gridColumns.find(function (gridCol) {
            return gridCol.field === field;
          });

          if (!value) {
            value = gridColumn && gridColumn.text || field;
          }

          if (!width) {
            width = gridColumn && gridColumn.width || _this.defaultColumnWidth;
          }
        } // zipcelx expects 'value' as a cell text https://github.com/egeriis/zipcelx/wiki/The-config-object


        columns.push({
          field: field,
          value: value,
          width: width,
          type: 'string'
        });
      };

      for (var i = 0; i < nbrColumns; i++) {
        _loop(i);
      }

      return columns;
    }
  }, {
    key: "generateRowData",
    value: function generateRowData(columns) {
      var _this2 = this;

      var nbrColumns = columns.length;

      if (!nbrColumns) {
        return [];
      }

      var rows = this.grid.store.map(function (record) {
        var cells = [];

        if (record.meta.specialRow) {
          if (_this2.showGroupHeader && record.meta.groupRowFor) {
            var value = _this2.grid.features.group.buildGroupHeader({
              // Create dummy element to get html from
              cellElement: bryntum.grid.DomHelper.createElement(),
              persist: true,
              record: record
            });

            cells.push({
              value: value,
              type: 'string'
            });
          }
        } else {
          for (var i = 0; i < nbrColumns; i++) {
            var _value = record[columns[i].field];

            if (_value instanceof Date) {
              _value = bryntum.grid.DateHelper.format(_value, _this2.dateFormat);
            }

            cells.push({
              value: _value,
              type: 'string'
            });
          }
        }

        return cells;
      }); // filter out empty rows

      return rows.filter(function (cells) {
        return cells.length;
      });
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
        filename: 'grid',

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
        columns: []
      };
    }
  }]);

  return ExcelExporter;
}(bryntum.grid.InstancePlugin);

bryntum.grid.GridFeatureManager.registerFeature(ExcelExporter, false, 'Grid'); //export default ExcelExporter;