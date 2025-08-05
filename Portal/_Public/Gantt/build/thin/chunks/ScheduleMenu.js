/*!
 *
 * Bryntum Gantt 5.5.0
 *
 * Copyright(c) 2023 Bryntum AB
 * https://bryntum.com/contact
 * https://bryntum.com/license
 *
 */
import { Model, StringHelper, IdHelper, DateHelper, LocaleHelper, Localizable, Store, unitMagnitudes, ObjectHelper, Events, Base, EventHelper, BrowserHelper, DomHelper, GlobalEvents, DomDataStore, Popup, Rectangle, Scroller, ResizeMonitor, Collection, FunctionHelper, VersionHelper, DomClassList, TimeZoneHelper, ArrayHelper, Delayable, Navigator, DomSync, Widget, ColorPicker, ContextMenuBase, Combo, Panel, Objects } from './Editor.js';
import { TimeSpan, AbstractCrudManagerMixin, ProjectCrudManager, AjaxTransport, JsonEncoder, ProjectModel, ResourceStore, EventStore, AssignmentStore, DependencyStore, CrudManagerView, RecurrenceDayRuleEncoder } from './CrudManagerView.js';
import { AvatarRendering, ColorField } from './AvatarRendering.js';
import { ButtonGroup } from './MessageDialog.js';
import { Header as Header$1, SubGrid, GridBase, Location, ColumnStore, WidgetColumn, Column, GridFeatureManager } from './GridBase.js';

/**
 * @module Scheduler/preset/ViewPreset
 */
/**
 * An object containing a unit identifier and an increment variable, used to define the `timeResolution` of a
 * `ViewPreset`.
 * @typedef {Object} ViewPresetTimeResolution
 * @property {String} unit The unit of the resolution, e.g. 'minute'
 * @property {Number} increment The increment of the resolution, e.g. 15
 */
/**
 * Defines a header level for a ViewPreset.
 *
 * A sample header configuration can look like below
 * ```javascript
 * headers    : {
 *     {
 *         unit        : "month",
 *         renderer : function(start, end, headerConfig, index) {
 *             var month = start.getMonth();
 *             // Simple alternating month in bold
 *             if (start.getMonth() % 2) {
 *                 return '<strong>' + month + '</strong>';
 *             }
 *             return month;
 *         },
 *         align       : 'start' // `start` or `end`, omit to center content (default)
 *     },
 *     {
 *         unit        : "week",
 *         increment   : 1,
 *         renderer    : function(start, end, headerConfig, index) {
 *             return 'foo';
 *         }
 *     },
 * }
 * ```
 *
 * @typedef {Object} ViewPresetHeaderRow
 * @property {'start'|'center'|'end'} align The text alignment for the cell. Valid values are `start` or `end`, omit
 * this to center text content (default). Can also be added programmatically in `the renderer`.
 * @property {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} unit The unit of time
 * represented by each cell in this header row. See also increment property. Valid values are "millisecond", "second",
 * "minute", "hour", "day", "week", "month", "quarter", "year".
 * @property {String} headerCellCls A CSS class to add to the cells in the time axis header row. Can also be added
 * programmatically in the `renderer`.
 * @property {Number} increment The number of units each header cell will represent (e.g. 30 together with unit:
 * "minute" for 30 minute cells)
 * @property {String} dateFormat Defines how the cell date will be formatted
 * @property {Function} renderer A custom renderer function used to render the cell content. It should return text/HTML
 * to put in the header cell.
 *
 * ```javascript
 * function (startDate, endDate, headerConfig, i) {
 *   // applies special CSS class to align header left
 *   headerConfig.align = "start";
 *   // will be added as a CSS class of the header cell DOM element
 *   headerConfig.headerCellCls = "myClass";
 *
 *   return DateHelper.format(startDate, 'YYYY-MM-DD');
 * }
 * ```
 *
 * The render function is called with the following parameters:
 *
 * @property {Date} renderer.startDate The start date of the cell.
 * @property {Date} renderer.endDate The end date of the cell.
 * @property {Object} renderer.headerConfig An object containing the header config.
 * @property {'start'|'center'|'end'} [renderer.headerConfig.align] The text alignment for the cell. See `align` above.
 * @property {String} [renderer.headerConfig.headerCellCls] A CSS class to add to the cells in the time axis header row.
 * See `headerCellCls` above.
 * @property {Number} renderer.index The index of the cell in the row.
 * @property {Object} thisObj `this` reference for the renderer function
 * @property {Function} cellGenerator A function that should return an array of objects containing 'start', 'end' and
 * 'header' properties. Use this if you want full control over how the header rows are generated.
 *
 * **Note:** `cellGenerator` cannot be used for the bottom level of your headers.
 *
 * Example :
 * ```javascript
 * viewPreset : {
 *     displayDateFormat : 'H:mm',
 *     shiftIncrement    : 1,
 *     shiftUnit         : 'WEEK',
 *     timeResolution    : {
 *         unit      : 'MINUTE',
 *         increment : 10
 *     },
 *     headers           : [
 *         {
 *             unit          : 'year',
 *             // Simplified scenario, assuming view will always just show one US fiscal year
 *             cellGenerator : (viewStart, viewEnd) => [{
 *                 start  : viewStart,
 *                 end    : viewEnd,
 *                 header : `Fiscal Year ${viewStart.getFullYear() + 1}`
 *             }]
 *         },
 *         {
 *             unit : 'quarter',
 *             renderer(start, end, cfg) {
 *                 const
 *                     quarter       = Math.floor(start.getMonth() / 3) + 1,
 *                     fiscalQuarter = quarter === 4 ? 1 : (quarter + 1);
 *
 *                 return `FQ${fiscalQuarter} ${start.getFullYear() + (fiscalQuarter === 1 ? 1 : 0)}`;
 *             }
 *         },
 *         {
 *             unit       : 'month',
 *             dateFormat : 'MMM Y'
 *         }
 *     ]
 *  },
 * ```
 */
/**
 * A ViewPreset is a record of {@link Scheduler.preset.PresetStore PresetStore} which describes the granularity
 * of the timeline view of a {@link Scheduler.view.Scheduler Scheduler} and the layout and subdivisions of the timeline header.
 *
 * You can create a new instance by specifying all fields:
 *
 * ```javascript
 * const myViewPreset = new ViewPreset({
 *     id   : 'myPreset',              // Unique id value provided to recognize your view preset. Not required, but having it you can simply set new view preset by id: scheduler.viewPreset = 'myPreset'
 *
 *     name : 'My view preset',        // A human-readable name provided to be used in GUI, e.i. preset picker, etc.
 *
 *     tickWidth  : 24,                // Time column width in horizontal mode
 *     tickHeight : 50,                // Time column height in vertical mode
 *     displayDateFormat : 'HH:mm',    // Controls how dates will be displayed in tooltips etc
 *
 *     shiftIncrement : 1,             // Controls how much time to skip when calling shiftNext and shiftPrevious.
 *     shiftUnit      : 'day',         // Valid values are 'millisecond', 'second', 'minute', 'hour', 'day', 'week', 'month', 'quarter', 'year'.
 *     defaultSpan    : 12,            // By default, if no end date is supplied to a view it will show 12 hours
 *
 *     timeResolution : {              // Dates will be snapped to this resolution
 *         unit      : 'minute',       // Valid values are 'millisecond', 'second', 'minute', 'hour', 'day', 'week', 'month', 'quarter', 'year'.
 *         increment : 15
 *     },
 *
 *     headers : [                     // This defines your header rows from top to bottom
 *         {                           // For each row you can define 'unit', 'increment', 'dateFormat', 'renderer', 'align', and 'thisObj'
 *             unit       : 'day',
 *             dateFormat : 'ddd DD/MM'
 *         },
 *         {
 *             unit       : 'hour',
 *             dateFormat : 'HH:mm'
 *         }
 *     ],
 *
 *     columnLinesFor : 1              // Defines header level column lines will be drawn for. Defaults to the last level.
 * });
 * ```
 *
 * Or you can extend one of view presets registered in {@link Scheduler.preset.PresetManager PresetManager}:
 *
 * ```javascript
 * const myViewPreset2 = new ViewPreset({
 *     id   : 'myPreset',                  // Unique id value provided to recognize your view preset. Not required, but having it you can simply set new view preset by id: scheduler.viewPreset = 'myPreset'
 *     name : 'My view preset',            // A human-readable name provided to be used in GUI, e.i. preset picker, etc.
 *     base : 'hourAndDay',                // Extends 'hourAndDay' view preset provided by PresetManager. You can pick out any of PresetManager's view presets: PresetManager.records
 *
 *     timeResolution : {                  // Override time resolution
 *         unit      : 'minute',
 *         increment : 15                  // Make it increment every 15 mins
 *     },
 *
 *     headers : [                         // Override headers
 *         {
 *             unit       : 'day',
 *             dateFormat : 'DD.MM.YYYY'   // Use different date format for top header 01.10.2020
 *         },
 *         {
 *             unit       : 'hour',
 *             dateFormat : 'LT'
 *         }
 *     ]
 * });
 * ```
 *
 * See {@link Scheduler.preset.PresetManager PresetManager} for the list of base presets. You may add your own
 * presets to this global list:
 *
 * ```javascript
 * PresetManager.add(myViewPreset);     // Adds new preset to the global scope. All newly created scheduler instances will have it too.
 *
 * const scheduler = new Scheduler({
 *     viewPreset : 'myPreset'
 *     // other configs...
 * });
 * ```
 *
 * Or add them on an individual basis to Scheduler instances:
 *
 * ```javascript
 * const scheduler = new Scheduler({...});
 *
 * scheduler.presets.add(myViewPreset); // Adds new preset to the scheduler instance only. All newly created scheduler instances will **not** have it.
 *
 * scheduler.viewPreset = 'myPreset';
 * ```
 *
 * ## Defining custom header rows
 *
 * You can have any number of header rows by specifying {@link #field-headers}, see {@link #typedef-ViewPresetHeaderRow}
 * for the config object format and {@link Core.helper.DateHelper} for the supported date formats, or use to render
 * custom contents into the row cells.
 *
 * ```javascript
 *  headers : [
 *      {
 *          unit       : 'month',
 *          dateFormat : 'MM.YYYY'
 *      },
 *      {
 *          unit       : 'week',
 *          renderer   : ({ startDate }) => `Week ${DateHelper.format(startDate, 'WW')}`
 *      }
 *  ]
 * ```
 *
 * {@inlineexample Scheduler/preset/CustomHeader.js}
 *
 * This live demo shows a custom ViewPreset with AM/PM time format:
 * @inlineexample Scheduler/preset/amPmPreset.js
 * @extends Core/data/Model
 */
class ViewPreset extends Model {
  static $name = 'ViewPreset';
  static get fields() {
    return [
    /**
     * The name of an existing view preset to extend
     * @field {String} base
     */
    {
      name: 'base',
      type: 'string'
    },
    /**
     * The name of the view preset
     * @field {String} name
     */
    {
      name: 'name',
      type: 'string'
    },
    /**
     * The height of the row in horizontal orientation
     * @field {Number} rowHeight
     * @default
     */
    {
      name: 'rowHeight',
      defaultValue: 24
    },
    /**
     * The width of the time tick column in horizontal orientation
     * @field {Number} tickWidth
     * @default
     */
    {
      name: 'tickWidth',
      defaultValue: 50
    },
    /**
     * The height of the time tick column in vertical orientation
     * @field {Number} tickHeight
     * @default
     */
    {
      name: 'tickHeight',
      defaultValue: 50
    },
    /**
     * Defines how dates will be formatted in tooltips etc
     * @field {String} displayDateFormat
     * @default
     */
    {
      name: 'displayDateFormat',
      defaultValue: 'HH:mm'
    },
    /**
     * The unit to shift when calling shiftNext/shiftPrevious to navigate in the chart.
     * Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
     * @field {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} shiftUnit
     * @default
     */
    {
      name: 'shiftUnit',
      defaultValue: 'hour'
    },
    /**
     * The amount to shift (in shiftUnits)
     * @field {Number} shiftIncrement
     * @default
     */
    {
      name: 'shiftIncrement',
      defaultValue: 1
    },
    /**
     * The amount of time to show by default in a view (in the unit defined by {@link #field-mainUnit})
     * @field {Number} defaultSpan
     * @default
     */
    {
      name: 'defaultSpan',
      defaultValue: 12
    },
    /**
     * Initially set to a unit. Defaults to the unit defined by the middle header.
     * @field {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} mainUnit
     */
    {
      name: 'mainUnit'
    },
    /**
     * Note: Currently, this field only applies when changing viewPreset with the {@link Scheduler.widget.ViewPresetCombo}.
     *
     * Set to a number and that amount of {@link #field-mainUnit} will be added to the startDate. For example: A
     * start value of `5` together with the mainUnit `hours` will add 5 hours to the startDate. This can achieve
     * a "day view" that starts 5 AM.
     *
     * Set to a string unit (for example week, day, month) and the startDate will be the start of that unit
     * calculated from current startDate. A start value of `week` will result in a startDate in the first day of
     * the week.
     *
     * If set to a number or not set at all, the startDate will be calculated at the beginning of current
     * mainUnit.
     * @field {Number|String} start
     */
    {
      name: 'start'
    },
    /**
     * An object containing a unit identifier and an increment variable. This value means minimal task duration
     * you can create using UI. For example when you drag create a task or drag & drop a task, if increment is 5
     * and unit is 'minute' that means that you can create a 5-minute-long task, or move it 5 min
     * forward/backward. This config maps to scheduler's
     * {@link Scheduler.view.mixin.TimelineDateMapper#property-timeResolution} config.
     *
     * ```javascript
     * timeResolution : {
     *   unit      : 'minute',  //Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
     *   increment : 5
     * }
     * ```
     *
     * @field {ViewPresetTimeResolution} timeResolution
     */
    'timeResolution',
    /**
     * An array containing one or more {@link #typedef-ViewPresetHeaderRow} config objects, each of
     * which defines a level of headers for the scheduler.
     * The `main` unit will be the last header's unit, but this can be changed using the
     * {@link #field-mainHeaderLevel} field.
     * @field {ViewPresetHeaderRow[]} headers
     */
    'headers',
    /**
     * Index of the {@link #field-headers} array to define which header level is the `main` header.
     * Defaults to the bottom header.
     * @field {Number} mainHeaderLevel
     */
    'mainHeaderLevel',
    /**
     * Index of a header level in the {@link #field-headers} array for which column lines are drawn. See
     * {@link Scheduler.feature.ColumnLines}.
     * Defaults to the bottom header.
     * @field {Number} columnLinesFor
     */
    'columnLinesFor'];
  }
  construct() {
    super.construct(...arguments);
    this.normalizeUnits();
  }
  generateId(owner) {
    const me = this,
      {
        headers
      } = me,
      parts = [];
    // If we were subclassed from a base, use that id as the basis of our.
    let result = Object.getPrototypeOf(me.data).id;
    if (!result) {
      for (let {
          length
        } = headers, i = length - 1; i >= 0; i--) {
        const {
            unit,
            increment
          } = headers[i],
          multiple = increment > 1;
        parts.push(`${multiple ? increment : ''}${i ? unit : StringHelper.capitalize(unit)}${multiple ? 's' : ''}`);
      }
      // Use upwards header units so eg "monthAndYear"
      result = parts.join('And');
    }
    // If duplicate, decorate the generated by adding details.
    // For example make it "hourAndDay-50by80"
    // Only interrogate the store if it is loaded. If consulted during
    // a load, the idMap will be created empty
    if (owner.count && owner.includes(result)) {
      result += `-${me.tickWidth}by${me.tickHeight || me.tickWidth}`;
      // If still duplicate use increment
      if (owner.includes(result)) {
        result += `-${me.bottomHeader.increment}`;
        // And if STILL duplicate, make it unique with a suffix
        if (owner.includes(result)) {
          result = IdHelper.generateId(`${result}-`);
        }
      }
    }
    return result;
  }
  normalizeUnits() {
    const me = this,
      {
        timeResolution,
        headers,
        shiftUnit
      } = me;
    if (headers) {
      // Make sure date "unit" constant specified in the preset are resolved
      for (let i = 0, {
          length
        } = headers; i < length; i++) {
        const header = headers[i];
        header.unit = DateHelper.normalizeUnit(header.unit);
        if (header.splitUnit) {
          header.splitUnit = DateHelper.normalizeUnit(header.splitUnit);
        }
        if (!('increment' in header)) {
          headers[i] = Object.assign({
            increment: 1
          }, header);
        }
      }
    }
    if (timeResolution) {
      timeResolution.unit = DateHelper.normalizeUnit(timeResolution.unit);
    }
    if (shiftUnit) {
      me.shiftUnit = DateHelper.normalizeUnit(shiftUnit);
    }
  }
  // Process legacy columnLines config into a headers array.
  static normalizeHeaderConfig(data) {
    const {
        headerConfig,
        columnLinesFor,
        mainHeaderLevel
      } = data,
      headers = data.headers = [];
    if (headerConfig.top) {
      if (columnLinesFor === 'top') {
        data.columnLinesFor = 0;
      }
      if (mainHeaderLevel === 'top') {
        data.mainHeaderLevel = 0;
      }
      headers[0] = headerConfig.top;
    }
    if (headerConfig.middle) {
      if (columnLinesFor === 'middle') {
        data.columnLinesFor = headers.length;
      }
      if (mainHeaderLevel === 'middle') {
        data.mainHeaderLevel = headers.length;
      }
      headers.push(headerConfig.middle);
    } else {
      throw new Error('ViewPreset.headerConfig must be configured with a middle');
    }
    if (headerConfig.bottom) {
      // Main level is middle when using headerConfig object.
      data.mainHeaderLevel = headers.length - 1;
      // There *must* be a middle above this bottom header
      // so that is the columnLines one by default.
      if (columnLinesFor == null) {
        data.columnLinesFor = headers.length - 1;
      } else if (columnLinesFor === 'bottom') {
        data.columnLinesFor = headers.length;
      }
      // There *must* be a middle above this bottom header
      // so that is the main one by default.
      if (mainHeaderLevel == null) {
        data.mainHeaderLevel = headers.length - 1;
      }
      if (mainHeaderLevel === 'bottom') {
        data.mainHeaderLevel = headers.length;
      }
      headers.push(headerConfig.bottom);
    }
  }
  // These are read-only once configured.
  set() {}
  inSet() {}
  get columnLinesFor() {
    return 'columnLinesFor' in this.data ? this.data.columnLinesFor : this.headers.length - 1;
  }
  get tickSize() {
    return this._tickSize || this.tickWidth;
  }
  get tickWidth() {
    return 'tickWidth' in this.data ? this.data.tickWidth : 50;
  }
  get tickHeight() {
    return 'tickHeight' in this.data ? this.data.tickHeight : 50;
  }
  get headerConfig() {
    // Configured in the legacy manner, just return the configured value.
    if (this.data.headerConfig) {
      return this.data.headerConfig;
    }
    // Rebuild the object based upon the configured headers array.
    const result = {},
      {
        headers
      } = this,
      {
        length
      } = headers;
    switch (length) {
      case 1:
        result.middle = headers[0];
        break;
      case 2:
        if (this.mainHeaderLevel === 0) {
          result.middle = headers[0];
          result.bottom = headers[1];
        } else {
          result.top = headers[0];
          result.middle = headers[1];
        }
        break;
      case 3:
        result.top = headers[0];
        result.middle = headers[1];
        result.bottom = headers[2];
        break;
      default:
        throw new Error('headerConfig object not supported for >3 header levels');
    }
    return result;
  }
  set mainHeaderLevel(mainHeaderLevel) {
    this.data.mainHeaderLevel = mainHeaderLevel;
  }
  get mainHeaderLevel() {
    if ('mainHeaderLevel' in this.data) {
      return this.data.mainHeaderLevel;
    }
    // 3 headers, then it's the middle
    if (this.data.headers.length === 3) {
      return 1;
    }
    // Assume it goes top, middle.
    // If it's middle, top, use mainHeaderLevel : 0
    return this.headers.length - 1;
  }
  get mainHeader() {
    return this.headers[this.mainHeaderLevel];
  }
  get topHeader() {
    return this.headers[0];
  }
  get topUnit() {
    return this.topHeader.unit;
  }
  get topIncrement() {
    return this.topHeader.increment;
  }
  get bottomHeader() {
    return this.headers[this.headers.length - 1];
  }
  get leafUnit() {
    return this.bottomHeader.unit;
  }
  get leafIncrement() {
    return this.bottomHeader.increment;
  }
  get mainUnit() {
    if ('mainUnit' in this.data) {
      return this.data.mainUnit;
    }
    return this.mainHeader.unit;
  }
  get msPerPixel() {
    const {
      bottomHeader
    } = this;
    return Math.round(DateHelper.asMilliseconds(bottomHeader.increment || 1, bottomHeader.unit) / this.tickWidth);
  }
  get isValid() {
    const me = this;
    let valid = true;
    // Make sure all date "unit" constants are valid
    for (const header of me.headers) {
      valid = valid && Boolean(DateHelper.normalizeUnit(header.unit));
    }
    if (me.timeResolution) {
      valid = valid && DateHelper.normalizeUnit(me.timeResolution.unit);
    }
    if (me.shiftUnit) {
      valid = valid && DateHelper.normalizeUnit(me.shiftUnit);
    }
    return valid;
  }
}
ViewPreset._$name = 'ViewPreset';

const locale = {
  localeName: 'En',
  localeDesc: 'English (US)',
  localeCode: 'en-US',
  Object: {
    newEvent: 'New event'
  },
  ResourceInfoColumn: {
    eventCountText: data => data + ' event' + (data !== 1 ? 's' : '')
  },
  Dependencies: {
    from: 'From',
    to: 'To',
    valid: 'Valid',
    invalid: 'Invalid'
  },
  DependencyType: {
    SS: 'SS',
    SF: 'SF',
    FS: 'FS',
    FF: 'FF',
    StartToStart: 'Start-to-Start',
    StartToEnd: 'Start-to-Finish',
    EndToStart: 'Finish-to-Start',
    EndToEnd: 'Finish-to-Finish',
    short: ['SS', 'SF', 'FS', 'FF'],
    long: ['Start-to-Start', 'Start-to-Finish', 'Finish-to-Start', 'Finish-to-Finish']
  },
  DependencyEdit: {
    From: 'From',
    To: 'To',
    Type: 'Type',
    Lag: 'Lag',
    'Edit dependency': 'Edit dependency',
    Save: 'Save',
    Delete: 'Delete',
    Cancel: 'Cancel',
    StartToStart: 'Start to Start',
    StartToEnd: 'Start to End',
    EndToStart: 'End to Start',
    EndToEnd: 'End to End'
  },
  EventEdit: {
    Name: 'Name',
    Resource: 'Resource',
    Start: 'Start',
    End: 'End',
    Save: 'Save',
    Delete: 'Delete',
    Cancel: 'Cancel',
    'Edit event': 'Edit event',
    Repeat: 'Repeat'
  },
  EventDrag: {
    eventOverlapsExisting: 'Event overlaps existing event for this resource',
    noDropOutsideTimeline: 'Event may not be dropped completely outside the timeline'
  },
  SchedulerBase: {
    'Add event': 'Add event',
    'Delete event': 'Delete event',
    'Unassign event': 'Unassign event',
    color: 'Color'
  },
  TimeAxisHeaderMenu: {
    pickZoomLevel: 'Zoom',
    activeDateRange: 'Date range',
    startText: 'Start date',
    endText: 'End date',
    todayText: 'Today'
  },
  EventCopyPaste: {
    copyEvent: 'Copy event',
    cutEvent: 'Cut event',
    pasteEvent: 'Paste event'
  },
  EventFilter: {
    filterEvents: 'Filter tasks',
    byName: 'By name'
  },
  TimeRanges: {
    showCurrentTimeLine: 'Show current timeline'
  },
  PresetManager: {
    secondAndMinute: {
      displayDateFormat: 'll LTS',
      name: 'Seconds'
    },
    minuteAndHour: {
      topDateFormat: 'ddd MM/DD, hA',
      displayDateFormat: 'll LST'
    },
    hourAndDay: {
      topDateFormat: 'ddd MM/DD',
      middleDateFormat: 'LST',
      displayDateFormat: 'll LST',
      name: 'Day'
    },
    day: {
      name: 'Day/hours'
    },
    week: {
      name: 'Week/hours'
    },
    dayAndWeek: {
      displayDateFormat: 'll LST',
      name: 'Week/days'
    },
    dayAndMonth: {
      name: 'Month'
    },
    weekAndDay: {
      displayDateFormat: 'll LST',
      name: 'Week'
    },
    weekAndMonth: {
      name: 'Weeks'
    },
    weekAndDayLetter: {
      name: 'Weeks/weekdays'
    },
    weekDateAndMonth: {
      name: 'Months/weeks'
    },
    monthAndYear: {
      name: 'Months'
    },
    year: {
      name: 'Years'
    },
    manyYears: {
      name: 'Multiple years'
    }
  },
  RecurrenceConfirmationPopup: {
    'delete-title': 'You are deleting an event',
    'delete-all-message': 'Do you want to delete all occurrences of this event?',
    'delete-further-message': 'Do you want to delete this and all future occurrences of this event, or only the selected occurrence?',
    'delete-further-btn-text': 'Delete All Future Events',
    'delete-only-this-btn-text': 'Delete Only This Event',
    'update-title': 'You are changing a repeating event',
    'update-all-message': 'Do you want to change all occurrences of this event?',
    'update-further-message': 'Do you want to change only this occurrence of the event, or this and all future occurrences?',
    'update-further-btn-text': 'All Future Events',
    'update-only-this-btn-text': 'Only This Event',
    Yes: 'Yes',
    Cancel: 'Cancel',
    width: 600
  },
  RecurrenceLegend: {
    ' and ': ' and ',
    Daily: 'Daily',
    'Weekly on {1}': ({
      days
    }) => `Weekly on ${days}`,
    'Monthly on {1}': ({
      days
    }) => `Monthly on ${days}`,
    'Yearly on {1} of {2}': ({
      days,
      months
    }) => `Yearly on ${days} of ${months}`,
    'Every {0} days': ({
      interval
    }) => `Every ${interval} days`,
    'Every {0} weeks on {1}': ({
      interval,
      days
    }) => `Every ${interval} weeks on ${days}`,
    'Every {0} months on {1}': ({
      interval,
      days
    }) => `Every ${interval} months on ${days}`,
    'Every {0} years on {1} of {2}': ({
      interval,
      days,
      months
    }) => `Every ${interval} years on ${days} of ${months}`,
    position1: 'the first',
    position2: 'the second',
    position3: 'the third',
    position4: 'the fourth',
    position5: 'the fifth',
    'position-1': 'the last',
    day: 'day',
    weekday: 'weekday',
    'weekend day': 'weekend day',
    daysFormat: ({
      position,
      days
    }) => `${position} ${days}`
  },
  RecurrenceEditor: {
    'Repeat event': 'Repeat event',
    Cancel: 'Cancel',
    Save: 'Save',
    Frequency: 'Frequency',
    Every: 'Every',
    DAILYintervalUnit: 'day(s)',
    WEEKLYintervalUnit: 'week(s)',
    MONTHLYintervalUnit: 'month(s)',
    YEARLYintervalUnit: 'year(s)',
    Each: 'Each',
    'On the': 'On the',
    'End repeat': 'End repeat',
    'time(s)': 'time(s)'
  },
  RecurrenceDaysCombo: {
    day: 'day',
    weekday: 'weekday',
    'weekend day': 'weekend day'
  },
  RecurrencePositionsCombo: {
    position1: 'first',
    position2: 'second',
    position3: 'third',
    position4: 'fourth',
    position5: 'fifth',
    'position-1': 'last'
  },
  RecurrenceStopConditionCombo: {
    Never: 'Never',
    After: 'After',
    'On date': 'On date'
  },
  RecurrenceFrequencyCombo: {
    None: 'No repeat',
    Daily: 'Daily',
    Weekly: 'Weekly',
    Monthly: 'Monthly',
    Yearly: 'Yearly'
  },
  RecurrenceCombo: {
    None: 'None',
    Custom: 'Custom...'
  },
  Summary: {
    'Summary for': date => `Summary for ${date}`
  },
  ScheduleRangeCombo: {
    completeview: 'Complete schedule',
    currentview: 'Visible schedule',
    daterange: 'Date range',
    completedata: 'Complete schedule (for all events)'
  },
  SchedulerExportDialog: {
    'Schedule range': 'Schedule range',
    'Export from': 'From',
    'Export to': 'To'
  },
  ExcelExporter: {
    'No resource assigned': 'No resource assigned'
  },
  CrudManagerView: {
    serverResponseLabel: 'Server response:'
  },
  DurationColumn: {
    Duration: 'Duration'
  }
};
LocaleHelper.publishLocale(locale);

/**
 * @module Scheduler/preset/PresetStore
 */
/**
 * A special Store subclass which holds {@link Scheduler.preset.ViewPreset ViewPresets}.
 * Each ViewPreset in this store represents a zoom level. The store data is sorted in special
 * zoom order. That is zoomed out to zoomed in. The first Preset will produce the narrowest event bars
 * the last one will produce the widest event bars.
 *
 * To specify view presets (zoom levels) please provide set of view presets to the scheduler:
 *
 * ```javascript
 * const myScheduler = new Scheduler({
 *     presets : [
 *         {
 *             base : 'hourAndDay',
 *             id   : 'MyHourAndDay',
 *             // other preset configs....
 *         },
 *         {
 *             base : 'weekAndMonth',
 *             id   : 'MyWeekAndMonth',
 *             // other preset configs....
 *         }
 *     ],
 *     viewPreset : 'MyHourAndDay',
 *     // other scheduler configs....
 *     });
 * ```
 *
 * @extends Core/data/Store
 */
class PresetStore extends Localizable(Store) {
  static get $name() {
    return 'PresetStore';
  }
  static get defaultConfig() {
    return {
      useRawData: true,
      modelClass: ViewPreset,
      /**
       * Specifies the sort order of the presets in the store.
       * By default they are in zoomed out to zoomed in order. That is
       * presets which will create widest event bars to presets
       * which will produce narrowest event bars.
       *
       * Configure this as `-1` to reverse this order.
       * @config {Number}
       * @default
       */
      zoomOrder: 1
    };
  }
  set storage(storage) {
    super.storage = storage;
    // Maintained in order automatically while adding.
    this.storage.addSorter((lhs, rhs) => {
      const leftBottomHeader = lhs.bottomHeader,
        rightBottomHeader = rhs.bottomHeader;
      // Sort order:
      //  Milliseconds per pixel.
      //  Tick size.
      //  Unit magnitude.
      //  Increment size.
      const order = rhs.msPerPixel - lhs.msPerPixel || unitMagnitudes[leftBottomHeader.unit] - unitMagnitudes[rightBottomHeader.unit] || leftBottomHeader.increment - rightBottomHeader.increment;
      return order * this.zoomOrder;
    });
  }
  get storage() {
    return super.storage;
  }
  getById(id) {
    // If we do not know about the id, inherit it from the PresetManager singleton
    return super.getById(id) || !this.isPresetManager && pm.getById(id);
  }
  createRecord(data, ...args) {
    let result;
    if (data.isViewPreset) {
      return data;
    }
    if (typeof data === 'string') {
      result = this.getById(data);
    } else if (typeof data === 'number') {
      result = this.getAt(data);
    }
    // Its a ViewPreset data object
    else {
      // If it's extending an existing ViewPreset, inherit then override
      // the data from the base.
      if (data.base) {
        data = this.copyBaseValues(data);
      }
      // Model constructor will call generateId if no id is provided
      return super.createRecord(data, ...args);
    }
    if (!result) {
      throw new Error(`ViewPreset ${data} does not exist`);
    }
    return result;
  }
  updateLocalization() {
    super.updateLocalization();
    const me = this;
    // Collect presets from store...
    let presets = me.allRecords;
    // and basePresets if we are the PresetManager
    if (me.isPresetManager) {
      presets = new Set(presets.concat(Object.values(me.basePresets)));
    }
    presets.forEach(preset => {
      let localePreset = me.optionalL(`L{PresetManager.${preset.id}}`, null, true);
      // Default presets generated from base presets use localization of base if they have no own
      if (typeof localePreset === 'string' && preset.baseId) {
        localePreset = me.optionalL(`L{PresetManager.${preset.baseId}}`, null, true);
      }
      // Apply any custom format defined in locale, or the original format if none exists
      if (localePreset && typeof localePreset === 'object') {
        if (!preset.originalDisplayDateFormat) {
          preset.originalDisplayDateFormat = preset.displayDateFormat;
        }
        // it there is a topDateFormat but preset.mainHeaderLevel is 0, means the middle header is the top header actually,
        // so convert property to middle (if middle doesn't exists) to localization understand (topDateFormat for weekAndDay for example)
        // topDateFormat doesn't work when mainHeaderLevel is 0 because it doesn't have top config
        // but has top header visually (Check on get headerConfig method in ViewPreset class)
        if (preset.mainHeaderLevel === 0 && localePreset.topDateFormat) {
          localePreset.middleDateFormat = localePreset.middleDateFormat || localePreset.topDateFormat;
        }
        preset.setData('displayDateFormat', localePreset.displayDateFormat || preset.originalDisplayDateFormat);
        ['top', 'middle', 'bottom'].forEach(level => {
          const levelConfig = preset.headerConfig[level],
            localeLevelDateFormat = localePreset[level + 'DateFormat'];
          if (levelConfig) {
            if (!levelConfig.originalDateFormat) {
              levelConfig.originalDateFormat = levelConfig.dateFormat;
            }
            // if there was defined topDateFormat on locale file for example, use it instead of renderer from basePresets (https://github.com/bryntum/support/issues/1307)
            if (localeLevelDateFormat && levelConfig.renderer) {
              levelConfig.renderer = null;
            }
            levelConfig.dateFormat = localeLevelDateFormat || levelConfig.originalDateFormat;
          }
        });
        // The preset names are used in ViewPresetCombo and are localized by default
        if (localePreset.name) {
          if (!preset.unlocalizedName) {
            preset.unlocalizedName = preset.name;
          }
          preset.setData('name', localePreset.name);
        } else if (preset.unlocalizedName && preset.unlocalizedName !== preset.name) {
          preset.name = preset.unlocalizedName;
          preset.unlocalizedName = null;
        }
      }
    });
  }
  // This function is not meant to be called by any code other than Base#getCurrentConfig().
  // Preset config on Scheduler/Gantt expects array of presets and not store config
  getCurrentConfig(options) {
    return super.getCurrentConfig(options).data;
  }
  copyBaseValues(presetData) {
    let base = this.getById(presetData.base);
    if (!base) {
      throw new Error(`ViewPreset base '${presetData.base}' does not exist.`);
    }
    base = ObjectHelper.clone(base.data);
    delete base.id;
    if (presetData.name) {
      delete base.name;
    }
    // Merge supplied data into a clone of the base ViewPreset's data
    // so that the new one overrides the base.
    return ObjectHelper.merge(base, presetData);
  }
  add(preset) {
    preset = Array.isArray(preset) ? preset : [preset];
    preset.forEach(preset => {
      // If a ViewPreset instance that extends another preset was added
      // Only in add we can apply the base data
      if (preset.isViewPreset && preset.base) {
        preset.data = this.copyBaseValues(preset.originalData);
      }
    });
    return super.add(...arguments);
  }
}
PresetStore._$name = 'PresetStore';

// No module tag here. That stops the singleton from being included by the docs.
/**
 * ## Intro
 * This is a global Store of {@link Scheduler.preset.ViewPreset ViewPresets}, required to supply initial data to
 * Scheduler's {@link Scheduler.view.mixin.TimelineViewPresets#config-presets}.
 *
 * You can provide new view presets globally or for a specific scheduler.
 *
 * **NOTE:** You **cannot** modify existing records in the PresetManager store. You can either remove
 * preset records from the store or add new records to the store.
 * Also please keep in mind, all changes provided to the PresetManager store are not reflected to the
 * {@link Scheduler.view.mixin.TimelineViewPresets#config-presets} of schedulers that already exist!
 *
 * If you want to have just a few presets (also known as zoom levels) in your Scheduler, you can slice corresponding records
 * from the `PresetManager` and apply them to the Scheduler `presets` config.
 * ```javascript
 * const newPresets = PresetManager.records.slice(10, 12);
 *
 * const scheduler = new Scheduler({
 *     presets    : newPresets, // Only 2 zoom levels are available
 *     viewPreset : newPresets[0].id
 * });
 * ```
 *
 * If you want to adjust all default presets and assign to a specific scheduler you are going to create,
 * you can extend them and pass as an array to the Scheduler `presets` config.
 * Here is an example of how to set the same `timeResolution` to all `ViewPresets`.
 * ```javascript
 * const newPresets = PresetManager.map(preset => {
 *     return {
 *         id             : 'my_' + preset.id,
 *         base           : preset.id, // Based on an existing preset
 *         timeResolution : {
 *             unit      : 'day',
 *             increment : 1
 *         }
 *     };
 * });
 *
 * const scheduler = new Scheduler({
 *     presets     : newPresets,
 *     viewPreset : 'my_hourAndDay'
 * });
 * ```
 *
 * If you want to do the same for **all** schedulers which will be created next, you can register new presets in a loop.
 * ```javascript
 * PresetManager.records.forEach(preset => {
 *     // Pass the same ID, so when a new preset is added to the store,
 *     // it will replace the current one.
 *     PresetManager.registerPreset(preset.id, {
 *        id             : preset.id,
 *        base           : preset.id,
 *        timeResolution : {
 *            unit      : 'day',
 *            increment : 1
 *        }
 *    });
 * });
 * ```
 *
 * Here is an example of how to add a new `ViewPreset` to the global `PresetManager` store and to the already created
 * scheduler `presets`.
 * ```javascript
 * const scheduler = new Scheduler({...});
 *
 * const newGlobalPresets = PresetManager.add({
 *     id              : 'myNewPreset',
 *     base            : 'hourAndDay', // Based on an existing preset
 *     columnLinesFor  : 0,
 *     // Override headers
 *     headers : [
 *         {
 *             unit       : 'day',
 *             // Use different date format for top header 01.10.2020
 *             dateFormat : 'DD.MM.YYYY'
 *         },
 *         {
 *             unit       : 'hour',
 *             dateFormat : 'LT'
 *         }
 *     ]
 * });
 *
 * // Add new presets to the scheduler that has been created before changes
 * // to PresetManager are applied
 * scheduler.presets.add(newGlobalPresets);
 * ```
 *
 * ## Predefined presets
 *
 * Predefined presets are:
 *
 * - `secondAndMinute` - creates a 2 level header - minutes and seconds:
 * {@inlineexample Scheduler/preset/secondAndMinute.js}
 * - `minuteAndHour` - creates a 2 level header - hours and minutes:
 * {@inlineexample Scheduler/preset/minuteAndHour.js}
 * - `hourAndDay` - creates a 2 level header - days and hours:
 * {@inlineexample Scheduler/preset/hourAndDay.js}
 * - `dayAndWeek` - creates a 2 level header - weeks and days:
 * {@inlineexample Scheduler/preset/dayAndWeek.js}
 * - `dayAndMonth` - creates a 2 level header - months and days:
 * {@inlineexample Scheduler/preset/dayAndMonth.js}
 * - `weekAndDay` - just like `dayAndWeek` but with different formatting:
 * {@inlineexample Scheduler/preset/weekAndDay.js}
 * - `weekAndDayLetter` - creates a 2 level header - weeks and day letters:
 * {@inlineexample Scheduler/preset/weekAndDayLetter.js}
 * - `weekAndMonth` - creates a 2 level header - months and weeks:
 * {@inlineexample Scheduler/preset/weekAndMonth.js}
 * - `weekDateAndMonth` - creates a 2 level header - months and weeks (weeks shown by first day only):
 * {@inlineexample Scheduler/preset/weekDateAndMonth.js}
 * - `monthAndYear` - creates a 2 level header - years and months:
 * {@inlineexample Scheduler/preset/monthAndYear.js}
 * - `year` - creates a 2 level header - years and quarters:
 * {@inlineexample Scheduler/preset/year.js}
 * - `manyYears` - creates a 2 level header - 5-years and years:
 * {@inlineexample Scheduler/preset/manyYears.js}
 *
 * See the {@link Scheduler.preset.ViewPreset} and {@link Scheduler.preset.ViewPresetHeaderRow} classes for a
 * description of the view preset properties.
 *
 * ## Localizing View Presets
 * Bryntum Scheduler uses locales for translations including date formats for view presets.
 *
 * To translate date format for view presets just define the date format for the specified region
 * for your locale file, like this:
 * ```javascript
 * const locale = {
 *
 *     localeName : 'En',
 *
 *     // ... Other translations here ...
 *
 *     PresetManager : {
 *         // Translation for the "weekAndDay" ViewPreset
 *         weekAndDay : {
 *             // Change the date format for the top and middle levels
 *             topDateFormat    : 'MMM',
 *             middleDateFormat : 'D'
 *         },
 *         // Translation for the "dayAndWeek" ViewPreset
 *         dayAndWeek : {
 *             // Change the date format for the top level
 *             topDateFormat : 'MMMM YYYY'
 *         }
 *     }
 * }
 *
 * LocaleManager.applyLocale(locale);
 * ```
 *
 * Check the <a target="_blank" href="../examples/localization/">localization demo</a> and [this guide](#Scheduler/guides/customization/localization.md) for more details.
 *
 * @singleton
 * @extends Scheduler/preset/PresetStore
 */
class PresetManager extends PresetStore {
  static get $name() {
    return 'PresetManager';
  }
  static get defaultConfig() {
    return {
      // To not break CSP demo
      preventSubClassingModel: true,
      basePresets: {
        secondAndMinute: {
          name: 'Seconds',
          tickWidth: 30,
          // Time column width
          tickHeight: 40,
          displayDateFormat: 'll LTS',
          // Controls how dates will be displayed in tooltips etc
          shiftIncrement: 10,
          // Controls how much time to skip when calling shiftNext and shiftPrevious.
          shiftUnit: 'minute',
          // Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
          defaultSpan: 24,
          // By default, if no end date is supplied to a view it will show 24 hours
          timeResolution: {
            // Dates will be snapped to this resolution
            unit: 'second',
            // Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
            increment: 5
          },
          // This defines your header rows.
          // For each row you can define "unit", "increment", "dateFormat", "renderer", "align", and "thisObj"
          headers: [{
            unit: 'minute',
            dateFormat: 'lll'
          }, {
            unit: 'second',
            increment: 10,
            dateFormat: 'ss'
          }]
        },
        minuteAndHour: {
          name: 'Minutes',
          tickWidth: 60,
          // Time column width
          tickHeight: 60,
          displayDateFormat: 'll LT',
          // Controls how dates will be displayed in tooltips etc
          shiftIncrement: 1,
          // Controls how much time to skip when calling shiftNext and shiftPrevious.
          shiftUnit: 'hour',
          // Valid values are "MILLI", "SECOND", "minute", "HOUR", "DAY", "WEEK", "MONTH", "QUARTER", "YEAR".
          defaultSpan: 24,
          // By default, if no end date is supplied to a view it will show 24 hours
          timeResolution: {
            // Dates will be snapped to this resolution
            unit: 'minute',
            // Valid values are "MILLI", "SECOND", "minute", "HOUR", "DAY", "WEEK", "MONTH", "QUARTER", "YEAR".
            increment: 15
          },
          headers: [{
            unit: 'hour',
            dateFormat: 'ddd MM/DD, hA'
          }, {
            unit: 'minute',
            increment: 30,
            dateFormat: 'mm'
          }]
        },
        hourAndDay: {
          name: 'Day',
          tickWidth: 70,
          tickHeight: 40,
          displayDateFormat: 'll LT',
          shiftIncrement: 1,
          shiftUnit: 'day',
          defaultSpan: 24,
          timeResolution: {
            unit: 'minute',
            increment: 30
          },
          headers: [{
            unit: 'day',
            dateFormat: 'ddd DD/MM' //Mon 01/10
          }, {
            unit: 'hour',
            dateFormat: 'LT'
          }]
        },
        day: {
          name: 'Day/hours',
          displayDateFormat: 'LT',
          shiftIncrement: 1,
          shiftUnit: 'day',
          defaultSpan: 1,
          timeResolution: {
            unit: 'minute',
            increment: 30
          },
          mainHeaderLevel: 0,
          headers: [{
            unit: 'day',
            dateFormat: 'ddd DD/MM',
            // Mon 01/02
            splitUnit: 'day'
          }, {
            unit: 'hour',
            renderer(value) {
              return `
                                    <div class="b-sch-calendarcolumn-ct"><span class="b-sch-calendarcolumn-hours">${DateHelper.format(value, 'HH')}</span>
                                    <span class="b-sch-calendarcolumn-minutes">${DateHelper.format(value, 'mm')}</span></div>
                                `;
            }
          }]
        },
        week: {
          name: 'Week/hours',
          displayDateFormat: 'LT',
          shiftIncrement: 1,
          shiftUnit: 'week',
          defaultSpan: 24,
          timeResolution: {
            unit: 'minute',
            increment: 30
          },
          mainHeaderLevel: 0,
          headers: [{
            unit: 'week',
            dateFormat: 'D d',
            splitUnit: 'day'
          }, {
            unit: 'hour',
            dateFormat: 'LT',
            // will be overridden by renderer
            renderer(value) {
              return `
                                    <div class="sch-calendarcolumn-ct">
                                    <span class="sch-calendarcolumn-hours">${DateHelper.format(value, 'HH')}</span>
                                    <span class="sch-calendarcolumn-minutes">${DateHelper.format(value, 'mm')}</span>
                                    </div>
                                `;
            }
          }]
        },
        dayAndWeek: {
          name: 'Days & Weeks',
          tickWidth: 100,
          tickHeight: 80,
          displayDateFormat: 'll LT',
          shiftUnit: 'day',
          shiftIncrement: 1,
          defaultSpan: 5,
          timeResolution: {
            unit: 'hour',
            increment: 1
          },
          headers: [{
            unit: 'week',
            renderer(start) {
              return DateHelper.getShortNameOfUnit('week') + '.' + DateHelper.format(start, 'WW MMM YYYY');
            }
          }, {
            unit: 'day',
            dateFormat: 'dd DD'
          }]
        },
        // dayAndMonth : {
        //     name              : 'Days & Months',
        //     tickWidth         : 100,
        //     tickHeight        : 80,
        //     displayDateFormat : 'll LT',
        //     shiftUnit         : 'day',
        //     shiftIncrement    : 1,
        //     defaultSpan       : 5,
        //     timeResolution    : {
        //         unit      : 'hour',
        //         increment : 1
        //     },
        //     headers : [
        //         {
        //             unit       : 'month',
        //             dateFormat : 'MMMM YYYY',
        //             align      : 'start'
        //         },
        //         {
        //             unit       : 'day',
        //             dateFormat : 'dd DD'
        //         }
        //     ]
        // },
        dayAndMonth: {
          name: 'Month',
          tickWidth: 100,
          tickHeight: 80,
          displayDateFormat: 'll LT',
          shiftUnit: 'month',
          shiftIncrement: 1,
          defaultSpan: 1,
          mainUnit: 'month',
          timeResolution: {
            unit: 'hour',
            increment: 1
          },
          headers: [{
            unit: 'month',
            dateFormat: 'MMMM YYYY'
          }, {
            unit: 'day',
            dateFormat: 'DD'
          }]
        },
        weekAndDay: {
          name: 'Week',
          tickWidth: 100,
          tickHeight: 80,
          displayDateFormat: 'll hh:mm A',
          shiftUnit: 'week',
          shiftIncrement: 1,
          defaultSpan: 1,
          timeResolution: {
            unit: 'day',
            increment: 1
          },
          mainHeaderLevel: 0,
          headers: [{
            unit: 'week',
            dateFormat: 'YYYY MMMM DD' // 2017 January 01
          }, {
            unit: 'day',
            increment: 1,
            dateFormat: 'DD MMM'
          }]
        },
        weekAndMonth: {
          name: 'Weeks',
          tickWidth: 100,
          tickHeight: 105,
          displayDateFormat: 'll',
          shiftUnit: 'week',
          shiftIncrement: 5,
          defaultSpan: 6,
          timeResolution: {
            unit: 'day',
            increment: 1
          },
          headers: [{
            unit: 'month',
            dateFormat: 'MMM YYYY' //Jan 2017
          }, {
            unit: 'week',
            dateFormat: 'DD MMM'
          }]
        },
        weekAndDayLetter: {
          name: 'Weeks/weekdays',
          tickWidth: 20,
          tickHeight: 50,
          displayDateFormat: 'll',
          shiftUnit: 'week',
          shiftIncrement: 1,
          defaultSpan: 10,
          timeResolution: {
            unit: 'day',
            increment: 1
          },
          mainHeaderLevel: 0,
          headers: [{
            unit: 'week',
            dateFormat: 'ddd DD MMM YYYY',
            verticalColumnWidth: 115
          }, {
            unit: 'day',
            dateFormat: 'd1',
            verticalColumnWidth: 25
          }]
        },
        weekDateAndMonth: {
          name: 'Months/weeks',
          tickWidth: 30,
          tickHeight: 40,
          displayDateFormat: 'll',
          shiftUnit: 'week',
          shiftIncrement: 1,
          defaultSpan: 10,
          timeResolution: {
            unit: 'day',
            increment: 1
          },
          headers: [{
            unit: 'month',
            dateFormat: 'YYYY MMMM'
          }, {
            unit: 'week',
            dateFormat: 'DD'
          }]
        },
        monthAndYear: {
          name: 'Months',
          tickWidth: 110,
          tickHeight: 110,
          displayDateFormat: 'll',
          shiftIncrement: 3,
          shiftUnit: 'month',
          defaultSpan: 12,
          timeResolution: {
            unit: 'day',
            increment: 1
          },
          headers: [{
            unit: 'year',
            dateFormat: 'YYYY' //2017
          }, {
            unit: 'month',
            dateFormat: 'MMM YYYY' //Jan 2017
          }]
        },

        year: {
          name: 'Years',
          tickWidth: 100,
          tickHeight: 100,
          resourceColumnWidth: 100,
          displayDateFormat: 'll',
          shiftUnit: 'year',
          shiftIncrement: 1,
          defaultSpan: 1,
          timeResolution: {
            unit: 'month',
            increment: 1
          },
          headers: [{
            unit: 'year',
            dateFormat: 'YYYY'
          }, {
            unit: 'quarter',
            renderer(start, end, cfg) {
              return DateHelper.getShortNameOfUnit('quarter').toUpperCase() + (Math.floor(start.getMonth() / 3) + 1);
            }
          }]
        },
        manyYears: {
          name: 'Multiple years',
          tickWidth: 40,
          tickHeight: 50,
          displayDateFormat: 'll',
          shiftUnit: 'year',
          shiftIncrement: 1,
          defaultSpan: 10,
          timeResolution: {
            unit: 'year',
            increment: 1
          },
          mainHeaderLevel: 0,
          headers: [{
            unit: 'year',
            increment: 5,
            renderer: (start, end) => start.getFullYear() + ' - ' + end.getFullYear()
          }, {
            unit: 'year',
            dateFormat: 'YY',
            increment: 1
          }]
        }
      },
      // This is a list of bryntum-supplied preset adjustments used to create the Scheduler's
      // default initial set of ViewPresets.
      defaultPresets: [
      // Years over years
      'manyYears', {
        width: 80,
        increment: 1,
        resolution: 1,
        base: 'manyYears',
        resolutionUnit: 'YEAR'
      },
      // Years over quarters
      'year', {
        width: 30,
        increment: 1,
        resolution: 1,
        base: 'year',
        resolutionUnit: 'MONTH'
      }, {
        width: 50,
        increment: 1,
        resolution: 1,
        base: 'year',
        resolutionUnit: 'MONTH'
      }, {
        width: 200,
        increment: 1,
        resolution: 1,
        base: 'year',
        resolutionUnit: 'MONTH'
      },
      // Years over months
      'monthAndYear',
      // Months over weeks
      'weekDateAndMonth',
      // Months over weeks
      'weekAndMonth',
      // Months over weeks
      'weekAndDayLetter',
      // Months over days
      'dayAndMonth',
      // Weeks over days
      'weekAndDay', {
        width: 54,
        increment: 1,
        resolution: 1,
        base: 'weekAndDay',
        resolutionUnit: 'HOUR'
      },
      // Days over hours
      'hourAndDay', {
        width: 64,
        increment: 6,
        resolution: 30,
        base: 'hourAndDay',
        resolutionUnit: 'MINUTE'
      }, {
        width: 100,
        increment: 6,
        resolution: 30,
        base: 'hourAndDay',
        resolutionUnit: 'MINUTE'
      }, {
        width: 64,
        increment: 2,
        resolution: 30,
        base: 'hourAndDay',
        resolutionUnit: 'MINUTE'
      },
      // Hours over minutes
      'minuteAndHour', {
        width: 60,
        increment: 15,
        resolution: 5,
        base: 'minuteAndHour'
      }, {
        width: 130,
        increment: 15,
        resolution: 5,
        base: 'minuteAndHour'
      }, {
        width: 60,
        increment: 5,
        resolution: 5,
        base: 'minuteAndHour'
      }, {
        width: 100,
        increment: 5,
        resolution: 5,
        base: 'minuteAndHour'
      },
      // Minutes over seconds
      'secondAndMinute', {
        width: 60,
        increment: 10,
        resolution: 5,
        base: 'secondAndMinute'
      }, {
        width: 130,
        increment: 5,
        resolution: 5,
        base: 'secondAndMinute'
      }],
      internalListeners: {
        locale: 'updateLocalization'
      }
    };
  }
  set basePresets(basePresets) {
    const presetCache = this._basePresets = {};
    for (const id in basePresets) {
      basePresets[id].id = id;
      presetCache[id] = this.createRecord(basePresets[id]);
    }
  }
  get basePresets() {
    return this._basePresets;
  }
  set defaultPresets(defaultPresets) {
    for (let i = 0, {
        length
      } = defaultPresets; i < length; i++) {
      const presetAdjustment = defaultPresets[i],
        isBase = typeof presetAdjustment === 'string',
        baseType = isBase ? presetAdjustment : presetAdjustment.base;
      let preset;
      // The default was just a string, so it's an unmodified instance of a base type.
      if (isBase) {
        preset = this.basePresets[baseType];
      }
      // If it's an object, it's an adjustment to a base type
      else {
        const config = Object.setPrototypeOf(ObjectHelper.clone(this.basePresets[baseType].data), {
            id: baseType
          }),
          {
            timeResolution
          } = config,
          bottomHeader = config.headers[config.headers.length - 1];
        config.id = undefined;
        if ('width' in presetAdjustment) {
          config.tickWidth = presetAdjustment.width;
        }
        if ('height' in presetAdjustment) {
          config.tickHeight = presetAdjustment.height;
        }
        if ('increment' in presetAdjustment) {
          bottomHeader.increment = presetAdjustment.increment;
        }
        if ('resolution' in presetAdjustment) {
          timeResolution.increment = presetAdjustment.resolution;
        }
        if ('resolutionUnit' in presetAdjustment) {
          timeResolution.unit = DateHelper.getUnitByName(presetAdjustment.resolutionUnit);
        }
        preset = this.createRecord(config);
        // Keep id of original preset around, used with localization in PresetStore
        preset.baseId = baseType;
      }
      this.add(preset);
    }
  }
  getById(id) {
    // Look first in the default set, and if it's one of the base types that is not imported into the
    // default set, then look at the bases.
    return super.getById(id) || this.basePresets[id];
  }
  /**
   * Registers a new view preset base to be used by any scheduler grid or tree on the page.
   * @param {String} id The unique identifier for this preset
   * @param {ViewPresetConfig} config The configuration properties of the view preset (see
   * {@link Scheduler.preset.ViewPreset} for more information)
   * @returns {Scheduler.preset.ViewPreset} A new ViewPreset based upon the passed configuration.
   */
  registerPreset(id, config) {
    const preset = this.createRecord(Object.assign({
        id
      }, config)),
      existingDuplicate = this.find(p => p.equals(preset));
    if (existingDuplicate) {
      return existingDuplicate;
    }
    if (preset.isValid) {
      this.add(preset);
    } else {
      throw new Error('Invalid preset, please check your configuration');
    }
    return preset;
  }
  getPreset(preset) {
    if (typeof preset === 'number') {
      preset = this.getAt(preset);
    }
    if (typeof preset === 'string') {
      preset = this.getById(preset);
    } else if (!(preset instanceof ViewPreset)) {
      preset = this.createRecord(preset);
    }
    return preset;
  }
  /**
   * Applies preset customizations or fetches a preset view preset using its name.
   * @param {String|ViewPresetConfig} presetOrId Id of a predefined preset or a preset config object
   * @returns {Scheduler.preset.ViewPreset} Resulting ViewPreset instance
   */
  normalizePreset(preset) {
    const me = this;
    if (!(preset instanceof ViewPreset)) {
      if (typeof preset === 'string') {
        preset = me.getPreset(preset);
        if (!preset) {
          throw new Error('You must define a valid view preset. See PresetManager for reference');
        }
      } else if (typeof preset === 'object') {
        // Look up any existing ViewPreset that it is based upon.
        if (preset.base) {
          const base = this.getById(preset.base);
          if (!base) {
            throw new Error(`ViewPreset base '${preset.base}' does not exist`);
          }
          // The config is based upon the base's data with the new config object merged in.
          preset = ObjectHelper.merge(ObjectHelper.clone(base.data), preset);
        }
        // Ensure the new ViewPreset has a legible, logical id which does not already
        // exist in our store.
        if (preset.id) {
          preset = me.createRecord(preset);
        } else {
          preset = me.createRecord(ObjectHelper.assign({}, preset));
          preset.id = preset.generateId(preset);
        }
      }
    }
    return preset;
  }
  /**
   * Deletes a view preset
   * @param {String} id The id of the preset, or the preset instance.
   */
  deletePreset(presetOrId) {
    if (typeof presetOrId === 'string') {
      presetOrId = this.getById(presetOrId);
    } else if (typeof presetOrId === 'number') {
      presetOrId = this.getAt(presetOrId);
    }
    if (presetOrId) {
      this.remove(presetOrId);
      // ALso remove it from our base list
      delete this.basePresets[presetOrId.id];
    }
  }
}
const pm = new PresetManager();

/**
 * @module Scheduler/data/TimeAxis
 */
// Micro-optimized version of TimeSpan for faster reading. Hit a lot and since it is internal fields are guaranteed to
// not be remapped and changes won't be batches, so we can always return raw value from data avoiding all additional
// checks and logic
class Tick extends TimeSpan {
  // Only getters on purpose, we do not support manipulating ticks
  get startDate() {
    return this.data.startDate;
  }
  get endDate() {
    return this.data.endDate;
  }
}
/**
 * A class representing the time axis of the scheduler. The scheduler timescale is based on the ticks generated by this
 * class. This is a pure "data" (model) representation of the time axis and has no UI elements.
 *
 * The time axis can be {@link #config-continuous} or not. In continuous mode, each timespan starts where the previous
 * ended, and in non-continuous mode there can be gaps between the ticks.
 * A non-continuous time axis can be used when want to filter out certain periods of time (like weekends) from the time
 * axis.
 *
 * To create a non-continuous time axis you have 2 options. First, you can create a time axis containing only the time
 * spans of interest. To do that, subclass this class and override the {@link #property-generateTicks} method.
 *
 * The other alternative is to call the {@link #function-filterBy} method, passing a function to it which should return
 * `false` if the time tick should be filtered out. Calling {@link Core.data.mixin.StoreFilter#function-clearFilters}
 * will return you to a full time axis.
 *
 * @extends Core/data/Store
 */
class TimeAxis extends Store {
  //region Events
  /**
   * Fires before the timeaxis is about to be reconfigured (e.g. new start/end date or unit/increment). Return `false`
   * to abort the operation.
   * @event beforeReconfigure
   * @param {Scheduler.data.TimeAxis} source The time axis instance
   * @param {Date} startDate The new time axis start date
   * @param {Date} endDate The new time axis end date
   */
  /**
   * Event that is triggered when we end reconfiguring and everything UI-related should be done
   * @event endReconfigure
   * @private
   */
  /**
   * Fires when the timeaxis has been reconfigured (e.g. new start/end date or unit/increment)
   * @event reconfigure
   * @param {Scheduler.data.TimeAxis} source The time axis instance
   */
  /**
   * Fires if all the ticks in the timeaxis are filtered out. After firing the filter is temporarily disabled to
   * return the time axis to a valid state. A disabled filter will be re-enabled the next time ticks are regenerated
   * @event invalidFilter
   * @param {Scheduler.data.TimeAxis} source The time axis instance
   */
  //endregion
  //region Default config
  static get defaultConfig() {
    return {
      modelClass: Tick,
      /**
       * Set to false if the timeline is not continuous, e.g. the next timespan does not start where the previous ended (for example skipping weekends etc).
       * @config {Boolean}
       * @default
       */
      continuous: true,
      originalContinuous: null,
      /**
       * Include only certain hours or days in the time axis (makes it `continuous : false`). Accepts and object
       * with `day` and `hour` properties:
       * ```
       * const scheduler = new Scheduler({
       *     timeAxis : {
       *         include : {
       *              // Do not display hours after 17 or before 9 (only display 9 - 17). The `to´ value is not
       *              // included in the time axis
       *              hour : {
       *                  from : 9,
       *                  to   : 17
       *              },
       *              // Do not display sunday or saturday
       *              day : [0, 6]
       *         }
       *     }
       * }
       * ```
       * In most cases we recommend that you use Scheduler's workingTime config instead. It is easier to use and
       * makes sure all parts of the Scheduler gets updated.
       * @config {Object}
       */
      include: null,
      /**
       * Automatically adjust the timespan when generating ticks with {@link #property-generateTicks} according to
       * the `viewPreset` configuration. Setting this to false may lead to shifting time/date of ticks.
       * @config {Boolean}
       * @default
       */
      autoAdjust: true,
      //isConfigured : false,
      // in case of `autoAdjust : false`, the 1st and last ticks can be truncated, containing only part of the normal tick
      // these dates will contain adjusted start/end (like if the tick has not been truncated)
      adjustedStart: null,
      adjustedEnd: null,
      // the visible position in the first tick, can actually be > 1 because the adjustment is done by the `mainUnit`
      visibleTickStart: null,
      // the visible position in the first tick, is always ticks count - 1 < value <= ticks count, in case of autoAdjust, always = ticks count
      visibleTickEnd: null,
      tickCache: {},
      viewPreset: null,
      maxTraverseTries: 100,
      useRawData: {
        disableDuplicateIdCheck: true,
        disableDefaultValue: true,
        disableTypeConversion: true
      }
    };
  }
  static get configurable() {
    return {
      /**
       * Method generating the ticks for this time axis. Should return an array of ticks. Each tick is an object of the following structure:
       * ```
       * {
       *    startDate : ..., // start date
       *    endDate   : ...  // end date
       * }
       * ```
       * Take notice, that this function either has to be called with `start`/`end` parameters, or create those variables.
       *
       * To see it in action please check out our [TimeAxis](https://bryntum.com/products/scheduler/examples/timeaxis/) example and navigate to "Compressed non-working time" tab.
       *
       * @param {Date} axisStartDate The start date of the interval
       * @param {Date} axisEndDate The end date of the interval
       * @param {String} unit The unit of the time axis
       * @param {Number} increment The increment for the unit specified.
       * @returns {Array} ticks The ticks representing the time axis
       * @config {Function}
       */
      generateTicks: null,
      unit: null,
      increment: null,
      resolutionUnit: null,
      resolutionIncrement: null,
      mainUnit: null,
      shiftUnit: null,
      shiftIncrement: 1,
      defaultSpan: 1,
      weekStartDay: null,
      // Used to force resolution to match whole ticks, to snap accordingly when using fillTicks in the UI
      forceFullTicks: null
    };
  }
  //endregion
  //region Init
  // private
  construct(config) {
    const me = this;
    super.construct(config);
    me.originalContinuous = me.continuous;
    me.ion({
      change: ({
        action
      }) => {
        // If the change was due to filtering, there will be a refresh event
        // arriving next, so do not reconfigure
        if (action !== 'filter') {
          me.trigger('reconfigure', {
            supressRefresh: false
          });
        }
      },
      refresh: () => me.trigger('reconfigure', {
        supressRefresh: false
      }),
      endreconfigure: event => me.trigger('reconfigure', event)
    });
    if (me.startDate) {
      me.internalOnReconfigure();
      me.trigger('reconfigure');
    } else if (me.viewPreset) {
      const range = me.getAdjustedDates(new Date());
      me.startDate = range.startDate;
      me.endDate = range.endDate;
    }
  }
  get isTimeAxis() {
    return true;
  }
  //endregion
  //region Configuration (reconfigure & consumePreset)
  /**
   * Reconfigures the time axis based on the config object supplied and generates the new 'ticks'.
   * @param {Object} config
   * @param {Boolean} [suppressRefresh]
   * @private
   */
  reconfigure(config, suppressRefresh = false, preventThrow = false) {
    const me = this,
      normalized = me.getAdjustedDates(config.startDate, config.endDate),
      oldConfig = {};
    if (me.trigger('beforeReconfigure', {
      startDate: normalized.startDate,
      endDate: normalized.endDate,
      config
    }) !== false) {
      me.trigger('beginReconfigure');
      me._configuredStartDate = config.startDate;
      me._configuredEndDate = config.endDate;
      // Collect old values for end event
      for (const propName in config) {
        oldConfig[propName] = me[propName];
      }
      const viewPresetChanged = config.viewPreset && config.viewPreset !== me.viewPreset;
      // If changing viewPreset, try to gracefully recover if an applied filter results in an empty view
      if (viewPresetChanged) {
        preventThrow = me.isFiltered;
        me.filters.forEach(f => f.disabled = false);
      }
      Object.assign(me, config);
      if (me.internalOnReconfigure(preventThrow, viewPresetChanged) === false) {
        return false;
      }
      me.trigger('endReconfigure', {
        suppressRefresh,
        config,
        oldConfig
      });
    }
  }
  internalOnReconfigure(preventThrow = false, viewPresetChanged) {
    const me = this;
    me.isConfigured = true;
    const adjusted = me.getAdjustedDates(me.startDate, me.endDate, true),
      normalized = me.getAdjustedDates(me.startDate, me.endDate),
      start = normalized.startDate,
      end = normalized.endDate;
    if (start >= end) {
      throw new Error(`Invalid start/end dates. Start date must be less than end date. Start date: ${start}. End date: ${end}.`);
    }
    const {
        unit,
        increment = 1
      } = me,
      ticks = me.generateTicks(start, end, unit, increment);
    // Suspending to be able to detect an invalid filter
    me.suspendEvents();
    me.maintainFilter = preventThrow;
    me.data = ticks;
    me.maintainFilter = false;
    const {
      count
    } = me;
    if (count === 0) {
      if (preventThrow) {
        if (viewPresetChanged) {
          me.disableFilters();
        }
        me.resumeEvents();
        return false;
      }
      throw new Error('Invalid time axis configuration or filter, please check your input data.');
    }
    // start date is cached, update it to fill after generated ticks
    me.startDate = me.first.startDate;
    me.endDate = me.last.endDate;
    me.resumeEvents();
    if (me.isContinuous) {
      me.adjustedStart = adjusted.startDate;
      me.adjustedEnd = DateHelper.getNext(count > 1 ? ticks[count - 1].startDate : adjusted.startDate, unit, increment, me.weekStartDay);
    } else {
      me.adjustedStart = me.startDate;
      me.adjustedEnd = me.endDate;
    }
    me.updateVisibleTickBoundaries();
    me.updateTickCache(true);
  }
  updateVisibleTickBoundaries() {
    const me = this,
      {
        count,
        unit,
        startDate,
        endDate,
        weekStartDay,
        increment = 1
      } = me;
    // Denominator is amount of milliseconds in a full tick (unit * increment). Normally we use 30 days in a month
    // and 365 days in a year. But if month is 31 day long or year is a leap one standard formula might calculate
    // wrong value. e.g. if we're rendering 1 day from August, formula goes like (2021-08-31 - 2021-08-02) / 30 = 1
    // and renders full tick which is incorrect. For such cases we need to adjust denominator to a correct one.
    // Thankfully there are only a few of them - month, year and day with DST transition.
    const startDenominator = DateHelper.getNormalizedUnitDuration(startDate, unit) * increment,
      endDenominator = DateHelper.getNormalizedUnitDuration(endDate, unit) * increment;
    // if visibleTickStart > 1 this means some tick is fully outside of the view - we are not interested in it and want to
    // drop it and adjust "adjustedStart" accordingly
    do {
      me.visibleTickStart = (startDate - me.adjustedStart) / startDenominator;
      if (me.autoAdjust) me.visibleTickStart = Math.floor(me.visibleTickStart);
      if (me.visibleTickStart >= 1) me.adjustedStart = DateHelper.getNext(me.adjustedStart, unit, increment, weekStartDay);
    } while (me.visibleTickStart >= 1);
    do {
      me.visibleTickEnd = count - (me.adjustedEnd - endDate) / endDenominator;
      if (count - me.visibleTickEnd >= 1) me.adjustedEnd = DateHelper.getNext(me.adjustedEnd, unit, -1, weekStartDay);
    } while (count - me.visibleTickEnd >= 1);
    // This flag indicates that the time axis starts exactly on a tick boundary and finishes on a tick boundary
    // This is used as an optimization flag by TimeAxisViewModel.createHeaderRow
    me.fullTicks = !me.visibleTickStart && me.visibleTickEnd === count;
  }
  /**
   * Get the currently used time unit for the ticks
   * @readonly
   * @member {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} unit
   */
  /**
   * Get/set currently used preset
   * @property {Scheduler.preset.ViewPreset}
   */
  get viewPreset() {
    return this._viewPreset;
  }
  set viewPreset(preset) {
    const me = this;
    preset = pm.getPreset(preset);
    if (!(preset instanceof ViewPreset)) {
      throw new Error('TimeAxis must be configured with the ViewPreset instance that the Scheduler is using');
    }
    me._viewPreset = preset;
    Object.assign(me, {
      unit: preset.bottomHeader.unit,
      increment: preset.bottomHeader.increment || 1,
      resolutionUnit: preset.timeResolution.unit,
      resolutionIncrement: preset.timeResolution.increment,
      mainUnit: preset.mainHeader.unit,
      shiftUnit: preset.shiftUnit || preset.mainHeader.unit,
      shiftIncrement: preset.shiftIncrement || 1,
      defaultSpan: preset.defaultSpan || 1,
      presetName: preset.id,
      // Weekview columns are updated upon 'datachanged' event on this object.
      // We have to pass headers in order to render them correctly (timeAxisViewModel is incorrect in required time)
      headers: preset.headers
    });
  }
  //endregion
  //region Getters & setters
  get weekStartDay() {
    return this._weekStartDay ?? DateHelper.weekStartDay;
  }
  // private
  get resolution() {
    return {
      unit: this.resolutionUnit,
      increment: this.resolutionIncrement
    };
  }
  // private
  set resolution(resolution) {
    this.resolutionUnit = resolution.unit;
    this.resolutionIncrement = resolution.increment;
  }
  get resolutionUnit() {
    return this.forceFullTicks ? this.unit : this._resolutionUnit;
  }
  get resolutionIncrement() {
    return this.forceFullTicks ? this.increment : this._resolutionIncrement;
  }
  //endregion
  //region Timespan & resolution
  /**
   * Changes the time axis timespan to the supplied start and end dates.
   *
   * **Note** This does **not** preserve the temporal scroll position. You may use
   * {@link Scheduler.view.Scheduler#function-setTimeSpan} to set the time axis and
   * maintain temporal scroll position (if possible).
   * @param {Date} newStartDate The new start date
   * @param {Date} [newEndDate] The new end date
   */
  setTimeSpan(newStartDate, newEndDate, preventThrow = false) {
    // If providing a 0 span range, add default range
    if (newEndDate && newStartDate - newEndDate === 0) {
      newEndDate = null;
    }
    const me = this,
      {
        startDate,
        endDate
      } = me.getAdjustedDates(newStartDate, newEndDate);
    if (me.startDate - startDate !== 0 || me.endDate - endDate !== 0) {
      return me.reconfigure({
        startDate,
        endDate
      }, false, preventThrow);
    }
  }
  /**
   * Moves the time axis by the passed amount and unit.
   *
   * NOTE: When using a filtered TimeAxis the result of `shift()` cannot be guaranteed, it might shift into a
   * filtered out span. It tries to be smart about it by shifting from unfiltered start and end dates.
   * If that solution does not work for your filtering setup, please call {@link #function-setTimeSpan} directly
   * instead.
   *
   * @param {Number} amount The number of units to jump
   * @param {String} [unit] The unit (Day, Week etc)
   */
  shift(amount, unit = this.shiftUnit) {
    const me = this;
    let {
      startDate,
      endDate
    } = me;
    // Use unfiltered start and end dates when shifting a filtered time axis, to lessen risk of messing it up.
    // Still not guaranteed to work though
    if (me.isFiltered) {
      startDate = me.allRecords[0].startDate;
      endDate = me.allRecords[me.allCount - 1].endDate;
    }
    // Hack for filtered time axis, for example if weekend is filtered out and you shiftPrev() day from monday
    let tries = 0;
    do {
      startDate = DateHelper.add(startDate, amount, unit);
      endDate = DateHelper.add(endDate, amount, unit);
    } while (tries++ < me.maxTraverseTries && me.setTimeSpan(startDate, endDate, {
      preventThrow: true
    }) === false);
  }
  /**
   * Moves the time axis forward in time in units specified by the view preset `shiftUnit`, and by the amount specified by the `shiftIncrement`
   * config of the current view preset.
   *
   * NOTE: When using a filtered TimeAxis the result of `shiftNext()` cannot be guaranteed, it might shift into a
   * filtered out span. It tries to be smart about it by shifting from unfiltered start and end dates.
   * If that solution does not work for your filtering setup, please call {@link #function-setTimeSpan} directly
   * instead.
   *
   * @param {Number} [amount] The number of units to jump forward
   */
  shiftNext(amount = this.shiftIncrement) {
    this.shift(amount);
  }
  /**
   * Moves the time axis backward in time in units specified by the view preset `shiftUnit`, and by the amount specified by the `shiftIncrement` config of the current view preset.
   *
   * NOTE: When using a filtered TimeAxis the result of `shiftPrev()` cannot be guaranteed, it might shift into a
   * filtered out span. It tries to be smart about it by shifting from unfiltered start and end dates.
   * If that solution does not work for your filtering setup, please call {@link #function-setTimeSpan} directly
   * instead.
   *
   * @param {Number} [amount] The number of units to jump backward
   */
  shiftPrevious(amount = this.shiftIncrement) {
    this.shift(-amount);
  }
  //endregion
  //region Filter & continuous
  /**
   * Filter the time axis by a function (and clears any existing filters first). The passed function will be called with each tick in time axis.
   * If the function returns `true`, the 'tick' is included otherwise it is filtered. If all ticks are filtered out
   * the time axis is considered invalid, triggering `invalidFilter` and then removing the filter.
   * @param {Function} fn The function to be called, it will receive an object with `startDate`/`endDate` properties, and `index` of the tick.
   * @param {Object} [thisObj] `this` reference for the function
   * @typings {Promise<any|null>}
   */
  filterBy(fn, thisObj = this) {
    const me = this;
    me.filters.clear();
    super.filterBy((tick, index) => fn.call(thisObj, tick.data, index));
  }
  filter() {
    const me = this,
      retVal = super.filter(...arguments);
    if (!me.maintainFilter && me.count === 0) {
      me.resumeEvents();
      me.trigger('invalidFilter');
      me.disableFilters();
    }
    return retVal;
  }
  disableFilters() {
    this.filters.forEach(f => f.disabled = true);
    this.filter();
  }
  triggerFilterEvent(event) {
    const me = this;
    if (!event.filters.count) {
      me.continuous = me.originalContinuous;
    } else {
      me.continuous = false;
    }
    // Filters has been applied (or cleared) but listeners are not informed yet, update tick cache to have start and
    // end dates correct when later redrawing events & header
    me.updateTickCache();
    super.triggerFilterEvent(event);
  }
  /**
   * Returns `true` if the time axis is continuous (will return `false` when filtered)
   * @property {Boolean}
   */
  get isContinuous() {
    return this.continuous !== false && !this.isFiltered;
  }
  //endregion
  //region Dates
  getAdjustedDates(startDate, endDate, forceAdjust = false) {
    const me = this;
    // If providing a 0 span range, add default range
    if (endDate && startDate - endDate === 0) {
      endDate = null;
    }
    startDate = startDate || me.startDate;
    endDate = endDate || DateHelper.add(startDate, me.defaultSpan, me.mainUnit);
    return me.autoAdjust || forceAdjust ? {
      startDate: me.floorDate(startDate, false, me.autoAdjust ? me.mainUnit : me.unit, 1),
      endDate: me.ceilDate(endDate, false, me.autoAdjust ? me.mainUnit : me.unit, 1)
    } : {
      startDate,
      endDate
    };
  }
  /**
   * Method to get the current start date of the time axis.
   * @property {Date}
   */
  get startDate() {
    return this._start || (this.first ? new Date(this.first.startDate) : null);
  }
  set startDate(start) {
    this._start = DateHelper.parse(start);
  }
  /**
   * Method to get a the current end date of the time axis
   * @property {Date}
   */
  get endDate() {
    return this._end || (this.last ? new Date(this.last.endDate) : null);
  }
  set endDate(end) {
    if (end) this._end = DateHelper.parse(end);
  }
  // used in performance critical code for comparisons
  get startMS() {
    return this._startMS;
  }
  // used in performance critical code for comparisons
  get endMS() {
    return this._endMS;
  }
  // Floors a date and optionally snaps it to one of the following resolutions:
  // 1. 'resolutionUnit'. If param 'resolutionUnit' is passed, the date will simply be floored to this unit.
  // 2. If resolutionUnit is not passed: If date should be snapped relative to the timeaxis start date,
  // the resolutionUnit of the timeAxis will be used, or the timeAxis 'mainUnit' will be used to snap the date
  //
  // returns a copy of the original date
  // private
  floorDate(date, relativeToStart, resolutionUnit, incr) {
    relativeToStart = relativeToStart !== false;
    const me = this,
      relativeTo = relativeToStart ? DateHelper.clone(me.startDate) : null,
      increment = incr || me.resolutionIncrement,
      unit = resolutionUnit || (relativeToStart ? me.resolutionUnit : me.mainUnit),
      snap = (value, increment) => Math.floor(value / increment) * increment;
    if (relativeToStart) {
      const snappedDuration = snap(DateHelper.diff(relativeTo, date, unit), increment);
      return DateHelper.add(relativeTo, snappedDuration, unit, false);
    }
    const dt = DateHelper.clone(date);
    if (unit === 'week') {
      const day = dt.getDay() || 7,
        startDay = me.weekStartDay || 7;
      DateHelper.add(DateHelper.startOf(dt, 'day', false), day >= startDay ? startDay - day : -(7 - startDay + day), 'day', false);
      // Watch out for Brazil DST craziness (see test 028_timeaxis_dst.t.js)
      if (dt.getDay() !== startDay && dt.getHours() === 23) {
        DateHelper.add(dt, 1, 'hour', false);
      }
    } else {
      // removes "smaller" units from date (for example minutes; removes seconds and milliseconds)
      DateHelper.startOf(dt, unit, false);
      // day and year are 1-based so need to make additional adjustments
      const modifier = ['day', 'year'].includes(unit) ? 1 : 0,
        useUnit = unit === 'day' ? 'date' : unit,
        snappedValue = snap(DateHelper.get(dt, useUnit) - modifier, increment) + modifier;
      DateHelper.set(dt, useUnit, snappedValue);
    }
    return dt;
  }
  /**
   * Rounds the date to nearest unit increment
   * @private
   */
  roundDate(date, relativeTo, resolutionUnit = this.resolutionUnit, increment = this.resolutionIncrement || 1) {
    const me = this,
      dt = DateHelper.clone(date);
    relativeTo = DateHelper.clone(relativeTo || me.startDate);
    switch (resolutionUnit) {
      case 'week':
        {
          DateHelper.startOf(dt, 'day', false);
          let distanceToWeekStartDay = dt.getDay() - me.weekStartDay,
            toAdd;
          if (distanceToWeekStartDay < 0) {
            distanceToWeekStartDay = 7 + distanceToWeekStartDay;
          }
          if (Math.round(distanceToWeekStartDay / 7) === 1) {
            toAdd = 7 - distanceToWeekStartDay;
          } else {
            toAdd = -distanceToWeekStartDay;
          }
          return DateHelper.add(dt, toAdd, 'day', false);
        }
      case 'month':
        {
          const nbrMonths = DateHelper.diff(relativeTo, dt, 'month') + DateHelper.as('month', dt.getDay() / DateHelper.daysInMonth(dt)),
            //*/DH.as('month', DH.diff(relativeTo, dt)) + (dt.getDay() / DH.daysInMonth(dt)),
            snappedMonths = Math.round(nbrMonths / increment) * increment;
          return DateHelper.add(relativeTo, snappedMonths, 'month', false);
        }
      case 'quarter':
        DateHelper.startOf(dt, 'month', false);
        return DateHelper.add(dt, 3 - dt.getMonth() % 3, 'month', false);
      default:
        {
          const duration = DateHelper.as(resolutionUnit, DateHelper.diff(relativeTo, dt)),
            // Need to find the difference of timezone offsets between relativeTo and original dates. 0 if timezone offsets are the same.
            offset = DateHelper.as(resolutionUnit, relativeTo.getTimezoneOffset() - dt.getTimezoneOffset(), 'minute'),
            // Need to add the offset to the whole duration, so the divided value will take DST into account
            snappedDuration = Math.round((duration + offset) / increment) * increment;
          // Now when the round is done, we need to subtract the offset, so the result also will take DST into account
          return DateHelper.add(relativeTo, snappedDuration - offset, resolutionUnit, false);
        }
    }
  }
  // private
  ceilDate(date, relativeToStart, resolutionUnit, increment) {
    const me = this;
    relativeToStart = relativeToStart !== false;
    increment = increment || (relativeToStart ? me.resolutionIncrement : 1);
    const unit = resolutionUnit || (relativeToStart ? me.resolutionUnit : me.mainUnit),
      dt = DateHelper.clone(date);
    let doCall = false;
    switch (unit) {
      case 'minute':
        doCall = !DateHelper.isStartOf(dt, 'minute');
        break;
      case 'hour':
        doCall = !DateHelper.isStartOf(dt, 'hour');
        break;
      case 'day':
      case 'date':
        doCall = !DateHelper.isStartOf(dt, 'day');
        break;
      case 'week':
        DateHelper.startOf(dt, 'day', false);
        doCall = dt.getDay() !== me.weekStartDay || !DateHelper.isEqual(dt, date);
        break;
      case 'month':
        DateHelper.startOf(dt, 'day', false);
        doCall = dt.getDate() !== 1 || !DateHelper.isEqual(dt, date);
        break;
      case 'quarter':
        DateHelper.startOf(dt, 'day', false);
        doCall = dt.getMonth() % 3 !== 0 || dt.getDate() !== 1 || !DateHelper.isEqual(dt, date);
        break;
      case 'year':
        DateHelper.startOf(dt, 'day', false);
        doCall = dt.getMonth() !== 0 || dt.getDate() !== 1 || !DateHelper.isEqual(dt, date);
        break;
    }
    if (doCall) {
      return DateHelper.getNext(dt, unit, increment, me.weekStartDay);
    }
    return dt;
  }
  //endregion
  //region Ticks
  get include() {
    return this._include;
  }
  set include(include) {
    const me = this;
    me._include = include;
    me.continuous = !include;
    if (!me.isConfiguring) {
      me.startDate = me._configuredStartDate;
      me.endDate = me._configuredEndDate;
      me.internalOnReconfigure();
      me.trigger('includeChange');
    }
  }
  // Check if a certain date is included based on timeAxis.include rules
  processExclusion(startDate, endDate, unit) {
    const {
      include
    } = this;
    if (include) {
      return Object.entries(include).some(([includeUnit, rule]) => {
        if (!rule) {
          return false;
        }
        const {
          from,
          to
        } = rule;
        // Including the closest smaller unit with a { from, to} rule should affect start & end of the
        // generated tick. Currently only works for days or smaller.
        if (DateHelper.compareUnits('day', unit) >= 0 && DateHelper.getLargerUnit(includeUnit) === unit) {
          if (from) {
            DateHelper.set(startDate, includeUnit, from);
          }
          if (to) {
            let stepUnit = unit;
            // Stepping back base on date, not day
            if (unit === 'day') {
              stepUnit = 'date';
            }
            // Since endDate is not inclusive it points to the next day etc.
            // Turns for example 2019-01-10T00:00 -> 2019-01-09T18:00
            DateHelper.set(endDate, {
              [stepUnit]: DateHelper.get(endDate, stepUnit) - 1,
              [includeUnit]: to
            });
          }
        }
        // "Greater" unit being included? Then we need to care about it
        // (for example excluding day will also affect hour, minute etc)
        if (DateHelper.compareUnits(includeUnit, unit) >= 0) {
          const datePart = includeUnit === 'day' ? startDate.getDay() : DateHelper.get(startDate, includeUnit);
          if (from && datePart < from || to && datePart >= to) {
            return true;
          }
        }
      });
    }
    return false;
  }
  // Calculate constants used for exclusion when scaling within larger ticks
  initExclusion() {
    Object.entries(this.include).forEach(([unit, rule]) => {
      if (rule) {
        const {
          from,
          to
        } = rule;
        // For example for hour:
        // 1. Get the next bigger unit -> day, get ratio -> 24
        // 2. to 20 - from 8 = 12 hours visible each day. lengthFactor 24 / 12 = 2 means that each hour used
        // needs to represent 2 hours when drawn (to stretch)
        // |    ████    | -> |  ████████  |
        rule.lengthFactor = DateHelper.getUnitToBaseUnitRatio(unit, DateHelper.getLargerUnit(unit)) / (to - from);
        rule.lengthFactorExcl = DateHelper.getUnitToBaseUnitRatio(unit, DateHelper.getLargerUnit(unit)) / (to - from - 1);
        // Calculate weighted center to stretch around |   ██x█ |
        rule.center = from + from / (rule.lengthFactor - 1);
      }
    });
  }
  /**
   * Method generating the ticks for this time axis. Should return an array of ticks. Each tick is an object of the following structure:
   * ```
   * {
   *    startDate : ..., // start date
   *    endDate   : ...  // end date
   * }
   * ```
   * Take notice, that this function either has to be called with `start`/`end` parameters, or create those variables.
   *
   * To see it in action please check out our [TimeAxis](https://bryntum.com/products/scheduler/examples/timeaxis/) example and navigate to "Compressed non-working time" tab.
   *
   * @member {Function} generateTicks
   * @param {Date} axisStartDate The start date of the interval
   * @param {Date} axisEndDate The end date of the interval
   * @param {String} unit The unit of the time axis
   * @param {Number} increment The increment for the unit specified.
   * @returns {Array} ticks The ticks representing the time axis
   */
  updateGenerateTicks() {
    if (!this.isConfiguring) {
      this.reconfigure(this);
    }
  }
  _generateTicks(axisStartDate, axisEndDate, unit = this.unit, increment = this.increment) {
    const me = this,
      ticks = [],
      usesExclusion = Boolean(me.include);
    let intervalEnd,
      tickEnd,
      isExcluded,
      dstDiff = 0,
      {
        startDate,
        endDate
      } = me.getAdjustedDates(axisStartDate, axisEndDate);
    me.tickCache = {};
    if (usesExclusion) {
      me.initExclusion();
    }
    while (startDate < endDate) {
      intervalEnd = DateHelper.getNext(startDate, unit, increment, me.weekStartDay);
      if (!me.autoAdjust && intervalEnd > endDate) {
        intervalEnd = endDate;
      }
      // Handle hourly increments crossing DST boundaries to keep the timescale looking correct
      // Only do this for HOUR resolution currently, and only handle it once per tick generation.
      if (unit === 'hour' && increment > 1 && ticks.length > 0 && dstDiff === 0) {
        const prev = ticks[ticks.length - 1];
        dstDiff = (prev.startDate.getHours() + increment) % 24 - prev.endDate.getHours();
        if (dstDiff !== 0) {
          // A DST boundary was crossed in previous tick, adjust this tick to keep timeaxis "symmetric".
          intervalEnd = DateHelper.add(intervalEnd, dstDiff, 'hour');
        }
      }
      isExcluded = false;
      if (usesExclusion) {
        tickEnd = new Date(intervalEnd.getTime());
        isExcluded = me.processExclusion(startDate, intervalEnd, unit);
      } else {
        tickEnd = intervalEnd;
      }
      if (!isExcluded) {
        ticks.push({
          id: ticks.length + 1,
          startDate,
          endDate: intervalEnd
        });
        me.tickCache[startDate.getTime()] = ticks.length - 1;
      }
      startDate = tickEnd;
    }
    return ticks;
  }
  /**
   * How many ticks are visible across the TimeAxis.
   *
   * Usually, this is an integer because {@link #config-autoAdjust} means that the start and end
   * dates are adjusted to be on tick boundaries.
   * @property {Number}
   * @internal
   */
  get visibleTickTimeSpan() {
    const me = this;
    return me.isContinuous ? me.visibleTickEnd - me.visibleTickStart : me.count;
  }
  /**
   * Gets a tick "coordinate" representing the date position on the time scale. Returns -1 if the date is not part of the time axis.
   * @param {Date} date the date
   * @returns {Number} the tick position on the scale or -1 if the date is not part of the time axis
   */
  getTickFromDate(date) {
    var _date$getTime;
    const me = this,
      ticks = me.records,
      dateMS = ((_date$getTime = date.getTime) === null || _date$getTime === void 0 ? void 0 : _date$getTime.call(date)) ?? date;
    let begin = 0,
      end = ticks.length - 1,
      middle,
      tick,
      tickStart,
      tickEnd;
    // Quickly eliminate out of range dates or if we have not been set up with a time range yet
    if (!ticks.length || dateMS < ticks[0].startDateMS || dateMS > ticks[end].endDateMS) {
      return -1;
    }
    if (me.isContinuous) {
      // Chop tick cache in half until we find a match
      while (begin < end) {
        middle = begin + end + 1 >> 1;
        if (dateMS > ticks[middle].endDateMS) {
          begin = middle + 1;
        } else if (dateMS < ticks[middle].startDateMS) {
          end = middle - 1;
        } else {
          begin = middle;
        }
      }
      tick = ticks[begin];
      tickStart = tick.startDateMS;
      // Part way though, calculate the fraction
      if (dateMS > tickStart) {
        tickEnd = tick.endDateMS;
        begin += (dateMS - tickStart) / (tickEnd - tickStart);
      }
      return Math.min(Math.max(begin, me.visibleTickStart), me.visibleTickEnd);
    } else {
      for (let i = 0; i <= end; i++) {
        tickEnd = ticks[i].endDateMS;
        if (dateMS <= tickEnd) {
          tickStart = ticks[i].startDateMS;
          // date < tickStart can occur in filtered case
          tick = i + (dateMS > tickStart ? (dateMS - tickStart) / (tickEnd - tickStart) : 0);
          return tick;
        }
      }
    }
  }
  getSnappedTickFromDate(date) {
    const startTickIdx = Math.floor(this.getTickFromDate(date));
    return this.getAt(startTickIdx);
  }
  /**
   * Gets the time represented by a tick "coordinate".
   * @param {Number} tick the tick "coordinate"
   * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @returns {Date} The date to represented by the tick "coordinate", or null if invalid.
   */
  getDateFromTick(tick, roundingMethod) {
    const me = this;
    if (tick === me.visibleTickEnd) {
      return me.endDate;
    }
    const wholeTick = Math.floor(tick),
      fraction = tick - wholeTick,
      t = me.getAt(wholeTick);
    if (!t) {
      return null;
    }
    const
      // if we've filtered timeaxis using filterBy, then we cannot trust to adjustedStart property and should use tick start
      start = wholeTick === 0 && me.isContinuous ? me.adjustedStart : t.startDate,
      // if we've filtered timeaxis using filterBy, then we cannot trust to adjustedEnd property and should use tick end
      end = wholeTick === me.count - 1 && me.isContinuous ? me.adjustedEnd : t.endDate;
    let date = DateHelper.add(start, fraction * (end - start), 'millisecond');
    if (roundingMethod) {
      date = me[roundingMethod + 'Date'](date);
    }
    return date;
  }
  /**
   * Returns the ticks of the timeaxis in an array of objects with a "startDate" and "endDate".
   * @property {Scheduler.model.TimeSpan[]}
   */
  get ticks() {
    return this.records;
  }
  /**
   * Caches ticks and start/end dates for faster processing during rendering of events.
   * @private
   */
  updateTickCache(onlyStartEnd = false) {
    const me = this;
    if (me.count) {
      me._start = me.first.startDate;
      me._end = me.last.endDate;
      me._startMS = me.startDate.getTime();
      me._endMS = me.endDate.getTime();
    } else {
      me._start = me._end = me._startMs = me._endMS = null;
    }
    // onlyStartEnd is true prior to clearing filters, to get start and end dates correctly during that process.
    // No point in filling tickCache yet in that case, it will be done after the filters are cleared
    if (!onlyStartEnd) {
      me.tickCache = {};
      me.forEach((tick, i) => me.tickCache[tick.startDate.getTime()] = i);
    }
  }
  //endregion
  //region Axis
  /**
   * Returns true if the passed date is inside the span of the current time axis.
   * @param {Date} date The date to query for
   * @returns {Boolean} true if the date is part of the time axis
   */
  dateInAxis(date, inclusiveEnd = false) {
    const me = this,
      axisStart = me.startDate,
      axisEnd = me.endDate;
    // Date is between axis start/end and axis is not continuous - need to perform better lookup
    if (me.isContinuous) {
      return inclusiveEnd ? DateHelper.betweenLesserEqual(date, axisStart, axisEnd) : DateHelper.betweenLesser(date, axisStart, axisEnd);
    } else {
      const length = me.getCount();
      let tickStart, tickEnd, tick;
      for (let i = 0; i < length; i++) {
        tick = me.getAt(i);
        tickStart = tick.startDate;
        tickEnd = tick.endDate;
        if (inclusiveEnd && date <= tickEnd || !inclusiveEnd && date < tickEnd) {
          return date >= tickStart;
        }
      }
    }
    return false;
  }
  /**
   * Returns true if the passed timespan is part of the current time axis (in whole or partially).
   * @param {Date} start The start date
   * @param {Date} end The end date
   * @returns {Boolean} true if the timespan is part of the timeaxis
   */
  timeSpanInAxis(start, end) {
    const me = this;
    if (!end || end.getTime() === start.getTime()) {
      return this.dateInAxis(start, true);
    }
    if (me.isContinuous) {
      return DateHelper.intersectSpans(start, end, me.startDate, me.endDate);
    }
    return start < me.startDate && end > me.endDate || me.getTickFromDate(start) !== me.getTickFromDate(end);
  }
  // Accepts a TimeSpan model (uses its cached MS values to be a bit faster during rendering)
  isTimeSpanInAxis(timeSpan) {
    const me = this,
      {
        startMS,
        endMS
      } = me,
      {
        startDateMS
      } = timeSpan,
      endDateMS = timeSpan.endDateMS ?? timeSpan.meta.endDateCached;
    // only consider fully scheduled ranges
    if (!startDateMS || !endDateMS) return false;
    if (endDateMS === startDateMS) {
      return me.dateInAxis(timeSpan.startDate, true);
    }
    if (me.isContinuous) {
      return endDateMS > startMS && startDateMS < endMS;
    }
    const startTick = me.getTickFromDate(timeSpan.startDate),
      endTick = me.getTickFromDate(timeSpan.endDate);
    // endDate is not inclusive
    if (startTick === me.count && DateHelper.isEqual(timeSpan.startDate, me.last.endDate) || endTick === 0 && DateHelper.isEqual(timeSpan.endDate, me.first.startDate)) {
      return false;
    }
    return (
      // Spanning entire axis
      startDateMS < startMS && endDateMS > endMS ||
      // Unintentionally 0 wide (ticks excluded or outside)
      startTick !== endTick
    );
  }
  //endregion
  //region Iteration
  /**
   * Calls the supplied iterator function once per interval. The function will be called with four parameters, startDate endDate, index, isLastIteration.
   * @internal
   * @param {String} unit The unit to use when iterating over the timespan
   * @param {Number} increment The increment to use when iterating over the timespan
   * @param {Function} iteratorFn The function to call
   * @param {Object} [thisObj] `this` reference for the function
   */
  forEachAuxInterval(unit, increment = 1, iteratorFn, thisObj = this) {
    const end = this.endDate;
    let dt = this.startDate,
      i = 0,
      intervalEnd;
    if (dt > end) throw new Error('Invalid time axis configuration');
    while (dt < end) {
      intervalEnd = DateHelper.min(DateHelper.getNext(dt, unit, increment, this.weekStartDay), end);
      iteratorFn.call(thisObj, dt, intervalEnd, i, intervalEnd >= end);
      dt = intervalEnd;
      i++;
    }
  }
  //endregion
}

TimeAxis._$name = 'TimeAxis';

/**
 * @module Scheduler/view/model/TimeAxisViewModel
 */
/**
 * This class is an internal view model class, describing the visual representation of a {@link Scheduler.data.TimeAxis}.
 * The config for the header rows is described in the {@link Scheduler.preset.ViewPreset#field-headers headers}.
 * To calculate the size of each cell in the time axis, this class requires:
 *
 * - availableSpace  - The total width or height available for the rendering
 * - tickSize       - The fixed width or height of each cell in the lowest header row. This value is normally read from the
 * {@link Scheduler.preset.ViewPreset viewPreset} but this can also be updated programmatically using the {@link #property-tickSize} setter
 *
 * Normally you should not interact with this class directly.
 *
 * @extends Core/mixin/Events
 */
class TimeAxisViewModel extends Events() {
  //region Default config
  static get defaultConfig() {
    return {
      /**
       * The time axis providing the underlying data to be visualized
       * @config {Scheduler.data.TimeAxis}
       * @internal
       */
      timeAxis: null,
      /**
       * The available width/height, this is normally not known by the consuming UI component using this model
       * class until it has been fully rendered. The consumer of this model should set
       * {@link #property-availableSpace} when its width has changed.
       * @config {Number}
       * @internal
       */
      availableSpace: null,
      /**
       * The "tick width" for horizontal mode or "tick height" for vertical mode, to use for the cells in the
       * bottom most header row.
       * This value is normally read from the {@link Scheduler.preset.ViewPreset viewPreset}
       * @config {Number}
       * @default
       * @internal
       */
      tickSize: 100,
      /**
       * true if there is a requirement to be able to snap events to a certain view resolution.
       * This has implications of the {@link #config-tickSize} that can be used, since all widths must be in even pixels.
       * @config {Boolean}
       * @default
       * @internal
       */
      snap: false,
      /**
       * true if cells in the bottom-most row should be fitted to the {@link #property-availableSpace available space}.
       * @config {Boolean}
       * @default
       * @internal
       */
      forceFit: false,
      headers: null,
      mode: 'horizontal',
      // or 'vertical'
      //used for Exporting. Make sure the tick columns are not recalculated when resizing.
      suppressFit: false,
      // cache of the config currently used.
      columnConfig: [],
      // the view preset name to apply initially
      viewPreset: null,
      // The default header level to draw column lines for
      columnLinesFor: null,
      originalTickSize: null,
      headersDatesCache: []
    };
  }
  //endregion
  //region Init & destroy
  construct(config) {
    const me = this;
    // getSingleUnitInPixels results are memoized because of frequent calls during rendering.
    me.unitToPixelsCache = {};
    super.construct(config);
    const viewPreset = me.timeAxis.viewPreset || me.viewPreset;
    if (viewPreset) {
      if (viewPreset instanceof ViewPreset) {
        me.consumeViewPreset(viewPreset);
      } else {
        const preset = pm.getPreset(viewPreset);
        preset && me.consumeViewPreset(preset);
      }
    }
    // When time axis is changed, reconfigure the model
    me.timeAxis.ion({
      reconfigure: 'onTimeAxisReconfigure',
      thisObj: me
    });
    me.configured = true;
  }
  doDestroy() {
    this.timeAxis.un('reconfigure', this.onTimeAxisReconfigure, this);
    super.doDestroy();
  }
  /**
   * Used to calculate the range to extend the TimeAxis to during infinite scroll.
   * @param {Date} date
   * @param {Boolean} centered
   * @param {Scheduler.preset.ViewPreset} [preset] Optional, the preset for which to calculate the range.
   * defaults to the currently active ViewPreset
   * @returns {Object} `{ startDate, endDate }`
   * @internal
   */
  calculateInfiniteScrollingDateRange(date, centered, preset = this.viewPreset) {
    const {
        timeAxis,
        availableSpace
      } = this,
      {
        bufferCoef
      } = this.owner,
      {
        leafUnit,
        leafIncrement,
        topUnit,
        topIncrement,
        tickSize
      } = preset,
      // If the units are the same and the increments are integer, snap to the top header's unit & increment
      useTop = leafUnit === topUnit && Math.round(topIncrement) === topIncrement && Math.round(leafIncrement) === leafIncrement,
      snapSize = useTop ? topIncrement : leafIncrement,
      snapUnit = useTop ? topUnit : leafUnit;
    // if provided date is the central point on the timespan
    if (centered) {
      const halfSpan = Math.ceil((availableSpace * bufferCoef + availableSpace / 2) / tickSize);
      return {
        startDate: timeAxis.floorDate(DateHelper.add(date, -halfSpan * leafIncrement, leafUnit), false, snapUnit, snapSize),
        endDate: timeAxis.ceilDate(DateHelper.add(date, halfSpan * leafIncrement, leafUnit), false, snapUnit, snapSize)
      };
    }
    // if provided date is the left coordinate of the visible timespan area
    else {
      const bufferedTicks = Math.ceil(availableSpace * bufferCoef / tickSize);
      return {
        startDate: timeAxis.floorDate(DateHelper.add(date, -bufferedTicks * leafIncrement, leafUnit), false, snapUnit, snapSize),
        endDate: timeAxis.ceilDate(DateHelper.add(date, Math.ceil((availableSpace / tickSize + bufferedTicks) * leafIncrement), leafUnit), false, snapUnit, snapSize)
      };
    }
  }
  /**
   * Returns an array representing the headers of the current timeAxis. Each element is an array representing the cells for that level in the header.
   * @returns {Object[]} An array of headers, each element being an array representing each cell (with start date and end date) in the timeline representation.
   * @internal
   */
  get columnConfig() {
    return this._columnConfig;
  }
  set columnConfig(config) {
    this._columnConfig = config;
  }
  get headers() {
    return this._headers;
  }
  set headers(headers) {
    if (headers && headers.length && headers[headers.length - 1].cellGenerator) {
      throw new Error('`cellGenerator` cannot be used for the bottom level of your headers. Use TimeAxis#generateTicks() instead.');
    }
    this._headers = headers;
  }
  get isTimeAxisViewModel() {
    return true;
  }
  //endregion
  //region Events
  /**
   * Fires after the model has been updated.
   * @event update
   * @param {Scheduler.view.model.TimeAxisViewModel} source The model instance
   */
  /**
   * Fires after the model has been reconfigured.
   * @event reconfigure
   * @param {Scheduler.view.model.TimeAxisViewModel} source The model instance
   */
  //endregion
  //region Mode
  /**
   * Using horizontal mode?
   * @returns {Boolean}
   * @readonly
   * @internal
   */
  get isHorizontal() {
    return this.mode !== 'vertical';
  }
  /**
   * Using vertical mode?
   * @returns {Boolean}
   * @readonly
   * @internal
   */
  get isVertical() {
    return this.mode === 'vertical';
  }
  /**
   * Gets/sets the forceFit value for the model. Setting it will cause it to update its contents and fire the
   * {@link #event-update} event.
   * @property {Boolean}
   * @internal
   */
  set forceFit(value) {
    if (value !== this._forceFit) {
      this._forceFit = value;
      this.update();
    }
  }
  //endregion
  //region Reconfigure & update
  reconfigure(config) {
    // clear the cached headers
    this.headers = null;
    // Ensure correct ordering
    this.setConfig(config);
    this.trigger('reconfigure');
  }
  onTimeAxisReconfigure({
    source: timeAxis,
    suppressRefresh
  }) {
    if (this.viewPreset !== timeAxis.viewPreset) {
      this.consumeViewPreset(timeAxis.viewPreset);
    }
    if (!suppressRefresh && timeAxis.count > 0) {
      this.update();
    }
  }
  /**
   * Updates the view model current timeAxis configuration and available space.
   * @param {Number} [availableSpace] The available space for the rendering of the axis (used in forceFit mode)
   * @param {Boolean} [silent] Pass `true` to suppress the firing of the `update` event.
   * @param {Boolean} [forceUpdate] Pass `true` to fire the `update` event even if the size has not changed.
   * @internal
   */
  update(availableSpace, silent = false, forceUpdate = false) {
    const me = this,
      {
        timeAxis,
        headers
      } = me,
      spaceAvailable = availableSpace !== 0;
    // We're in configuration, or no change, quit
    if (me.isConfiguring || spaceAvailable && me._availableSpace === availableSpace) {
      if (forceUpdate) {
        me.trigger('update');
      }
      return;
    }
    me._availableSpace = Math.max(availableSpace || me.availableSpace || 0, 0);
    if (typeof me.availableSpace !== 'number') {
      throw new Error('Invalid available space provided to TimeAxisModel');
    }
    me.columnConfig = [];
    // The "column width" is considered to be the width of each tick in the lowest header row and this width
    // has to be same for all cells in the lowest row.
    const tickSize = me._tickSize = me.calculateTickSize(me.originalTickSize);
    if (typeof tickSize !== 'number' || tickSize <= 0) {
      throw new Error('Invalid timeAxis tick size');
    }
    // getSingleUnitInPixels results are memoized because of frequent calls during rendering.
    me.unitToPixelsCache = {};
    // totalSize is cached because of frequent calls which calculate it.
    me._totalSize = null;
    // Generate the underlying date ranges for each header row, which will provide input to the cell rendering
    for (let pos = 0, {
        length
      } = headers; pos < length; pos++) {
      const header = headers[pos];
      if (header.cellGenerator) {
        const headerCells = header.cellGenerator.call(me, timeAxis.startDate, timeAxis.endDate);
        me.columnConfig[pos] = me.createHeaderRow(pos, header, headerCells);
      } else {
        me.columnConfig[pos] = me.createHeaderRow(pos, header);
      }
    }
    if (!silent) {
      me.trigger('update');
    }
  }
  //endregion
  //region Date / position mapping
  /**
   * Returns the distance in pixels for a timespan with the given start and end date.
   * @param {Date} start start date
   * @param {Date} end end date
   * @returns {Number} The length of the time span
   * @category Date mapping
   */
  getDistanceBetweenDates(start, end) {
    return this.getPositionFromDate(end) - this.getPositionFromDate(start);
  }
  /**
   * Returns the distance in pixels for a time span
   * @param {Number} durationMS Time span duration in ms
   * @returns {Number} The length of the time span
   * @category Date mapping
   */
  getDistanceForDuration(durationMs) {
    return this.getSingleUnitInPixels('millisecond') * durationMs;
  }
  /**
   * Gets the position of a date on the projected time axis or -1 if the date is not in the timeAxis.
   * @param {Date} date the date to query for.
   * @returns {Number} the coordinate representing the date
   * @category Date mapping
   */
  getPositionFromDate(date, options = {}) {
    const tick = this.getScaledTick(date, options);
    if (tick === -1) {
      return -1;
    }
    return this.tickSize * (tick - this.timeAxis.visibleTickStart);
  }
  // Translates a tick along the time axis to facilitate scaling events when excluding certain days or hours
  getScaledTick(date, {
    respectExclusion,
    snapToNextIncluded,
    isEnd,
    min,
    max
  }) {
    const {
        timeAxis
      } = this,
      {
        include,
        unit
      } = timeAxis;
    let tick = timeAxis.getTickFromDate(date);
    if (tick !== -1 && respectExclusion && include) {
      let tickChanged = false;
      // Stretch if we are using a larger unit than 'hour', except if it is 'day'. If so, it is already handled
      // by a cheaper reconfiguration of the ticks in `generateTicks`
      if (include.hour && DateHelper.compareUnits(unit, 'hour') > 0 && unit !== 'day') {
        const {
            from,
            to,
            lengthFactor,
            center
          } = include.hour,
          // Original hours
          originalHours = date.getHours(),
          // Crop to included hours
          croppedHours = Math.min(Math.max(originalHours, from), to);
        // If we are not asked to snap (when other part of span is not included) any cropped away hour
        // should be considered excluded
        if (!snapToNextIncluded && croppedHours !== originalHours) {
          return -1;
        }
        const
          // Should scale hour and smaller units (seconds will hardly affect visible result...)
          fractionalHours = croppedHours + date.getMinutes() / 60,
          // Number of hours from the center    |xxxx|123c----|xxx|
          hoursFromCenter = center - fractionalHours,
          // Step from center to stretch event  |x|112233c----|xxx|
          newHours = center - hoursFromCenter * lengthFactor;
        // Adding instead of setting to get a clone of the date, to not affect the original
        date = DateHelper.add(date, newHours - originalHours, 'h');
        tickChanged = true;
      }
      if (include.day && DateHelper.compareUnits(unit, 'day') > 0) {
        const {
          from,
          to,
          lengthFactor,
          center
        } = include.day;
        //region Crop
        let checkDay = date.getDay();
        // End date is exclusive, check the day before if at 00:00
        if (isEnd && date.getHours() === 0 && date.getMinutes() === 0 && date.getSeconds() === 0 && date.getMilliseconds() === 0) {
          if (--checkDay < 0) {
            checkDay = 6;
          }
        }
        let addDays = 0;
        if (checkDay < from || checkDay >= to) {
          // If end date is in view but start date is excluded, snap to next included day
          if (snapToNextIncluded) {
            // Step back to "to-1" (not inclusive) for end date
            if (isEnd) {
              addDays = (to - checkDay - 8) % 7;
            }
            // Step forward to "from" for start date
            else {
              addDays = (from - checkDay + 7) % 7;
            }
            date = DateHelper.add(date, addDays, 'd');
            date = DateHelper.startOf(date, 'd', false);
            // Keep end after start and vice versa
            if (max && date.getTime() >= max || min && date.getTime() <= min) {
              return -1;
            }
          } else {
            // day excluded at not snapping to next
            return -1;
          }
        }
        //endregion
        const {
            weekStartDay
          } = timeAxis,
          // Center to stretch around, for some reason pre-calculated cannot be used for sundays :)
          fixedCenter = date.getDay() === 0 ? 0 : center,
          // Should scale day and smaller units (minutes will hardly affect visible result...)
          fractionalDay = date.getDay() + date.getHours() / 24,
          //+ dateClone.getMinutes() / (24 * 1440),
          // Number of days from the calculated center
          daysFromCenter = fixedCenter - fractionalDay,
          // Step from center to stretch event
          newDay = fixedCenter - daysFromCenter * lengthFactor;
        // Adding instead of setting to get a clone of the date, to not affect the original
        date = DateHelper.add(date, newDay - fractionalDay + weekStartDay, 'd');
        tickChanged = true;
      }
      // Now the date might start somewhere else (fraction of ticks)
      if (tickChanged) {
        // When stretching date might end up outside of time axis, making it invalid to use. Clip it to time axis
        // to circumvent this
        date = DateHelper.constrain(date, timeAxis.startDate, timeAxis.endDate);
        // Get a new tick based on the "scaled" date
        tick = timeAxis.getTickFromDate(date);
      }
    }
    return tick;
  }
  /**
   * Gets the date for a position on the time axis
   * @param {Number} position The page X or Y coordinate
   * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @param {Boolean} [allowOutOfRange=false] By default, this returns `null` if the position is outside
   * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
   * @returns {Date} the Date corresponding to the xy coordinate
   * @category Date mapping
   */
  getDateFromPosition(position, roundingMethod, allowOutOfRange = false) {
    const me = this,
      {
        timeAxis
      } = me,
      tick = me.getScaledPosition(position) / me.tickSize + timeAxis.visibleTickStart;
    if (tick < 0 || tick > timeAxis.count) {
      if (allowOutOfRange) {
        let result;
        // Subtract the correct number of tick units from the start date
        if (tick < 0) {
          result = DateHelper.add(timeAxis.startDate, tick, timeAxis.unit);
        } else {
          // Add the correct number of tick units to the end date
          result = DateHelper.add(timeAxis.endDate, tick - timeAxis.count, timeAxis.unit);
        }
        // Honour the rounding requested
        if (roundingMethod) {
          result = timeAxis[roundingMethod + 'Date'](result);
        }
        return result;
      }
      return null;
    }
    return timeAxis.getDateFromTick(tick, roundingMethod);
  }
  // Translates a position along the time axis to facilitate scaling events when excluding certain days or hours
  getScaledPosition(position) {
    const {
      include,
      unit,
      weekStartDay
    } = this.timeAxis;
    // Calculations are
    if (include) {
      const dayWidth = this.getSingleUnitInPixels('day');
      // Have to calculate day before hour to get end result correct
      if (include.day && DateHelper.compareUnits(unit, 'day') > 0) {
        const {
            from,
            lengthFactor
          } = include.day,
          // Scaling happens within a week, determine position within it
          positionInWeek = position % (dayWidth * 7),
          // Store were the week starts to be able to re-add it after scale
          weekStartPosition = position - positionInWeek;
        // Scale position using calculated length per day factor, adding the width of excluded days
        position = positionInWeek / lengthFactor + (from - weekStartDay) * dayWidth + weekStartPosition;
      }
      // Hours are not taken into account when viewing days, since the day ticks are reconfigured in
      // `generateTicks` instead
      if (include.hour && DateHelper.compareUnits(unit, 'hour') > 0 && unit !== 'day') {
        const {
            from,
            lengthFactorExcl
          } = include.hour,
          hourWidth = this.getSingleUnitInPixels('hour'),
          // Scaling happens within a day, determine position within it
          positionInDay = position % dayWidth,
          // Store were the day starts to be able to re-add it after scale
          dayStartPosition = position - positionInDay;
        // Scale position using calculated length per day factor, adding the width of excluded hours
        position = positionInDay / lengthFactorExcl + from * hourWidth + dayStartPosition;
      }
    }
    return position;
  }
  /**
   * Returns the amount of pixels for a single unit
   * @internal
   * @returns {Number} The unit in pixel
   */
  getSingleUnitInPixels(unit) {
    const me = this;
    return me.unitToPixelsCache[unit] || (me.unitToPixelsCache[unit] = DateHelper.getUnitToBaseUnitRatio(me.timeAxis.unit, unit, true) * me.tickSize / me.timeAxis.increment);
  }
  /**
   * Returns the pixel increment for the current view resolution.
   * @internal
   * @returns {Number} The increment
   */
  get snapPixelAmount() {
    if (this.snap) {
      const {
        resolution
      } = this.timeAxis;
      return (resolution.increment || 1) * this.getSingleUnitInPixels(resolution.unit);
    }
    return 1;
  }
  //endregion
  //region Sizes
  /**
   * Get/set the current time column size (the width or height of a cell in the bottom-most time axis header row,
   * depending on mode)
   * @internal
   * @property {Number}
   */
  get tickSize() {
    return this._tickSize;
  }
  set tickSize(size) {
    this.setTickSize(size, false);
  }
  setTickSize(size, suppressEvent) {
    this._tickSize = this.originalTickSize = size;
    this.update(undefined, suppressEvent);
  }
  get timeResolution() {
    return this.timeAxis.resolution;
  }
  // Calculates the time column width/height based on the value defined viewPreset "tickWidth/tickHeight". It also
  // checks for the forceFit view option and the snap, both of which impose constraints on the time column width
  // configuration.
  calculateTickSize(proposedSize) {
    const me = this,
      {
        forceFit,
        timeAxis,
        suppressFit
      } = me,
      timelineUnit = timeAxis.unit;
    let size = 0,
      ratio = 1; //Number.MAX_VALUE;
    if (me.snap) {
      const resolution = timeAxis.resolution;
      ratio = DateHelper.getUnitToBaseUnitRatio(timelineUnit, resolution.unit) * resolution.increment;
    }
    if (!suppressFit) {
      const fittingSize = me.availableSpace / timeAxis.visibleTickTimeSpan;
      size = forceFit || proposedSize < fittingSize ? fittingSize : proposedSize;
      if (ratio > 0 && (!forceFit || ratio < 1)) {
        size = Math.max(1, ratio * size) / ratio;
      }
    } else {
      size = proposedSize;
    }
    return size;
  }
  /**
   * Returns the total width/height of the time axis representation, depending on mode.
   * @returns {Number} The width or height
   * @internal
   * @readonly
   */
  get totalSize() {
    // Floor the space to prevent spurious overflow
    return this._totalSize || (this._totalSize = Math.floor(this.tickSize * this.timeAxis.visibleTickTimeSpan));
  }
  /**
   * Get/set the available space for the time axis representation. If size changes it will cause it to update its
   * contents and fire the {@link #event-update} event.
   * @internal
   * @property {Number}
   */
  get availableSpace() {
    return this._availableSpace;
  }
  set availableSpace(space) {
    const me = this;
    // We should only need to repaint fully if the tick width has changed (which will happen if forceFit is set, or if the full size of the time axis doesn't
    // occupy the available space - and gets stretched
    me._availableSpace = Math.max(0, space);
    if (me._availableSpace > 0) {
      const newTickSize = me.calculateTickSize(me.originalTickSize);
      if (newTickSize > 0 && newTickSize !== me.tickSize) {
        me.update();
      }
    }
  }
  //endregion
  //region Fitting & snapping
  /**
   * Returns start dates for ticks at the specified level in format { date, isMajor }.
   * @param {Number} level Level in headers array, `0` meaning the topmost...
   * @param {Boolean} useLowestHeader Use lowest level
   * @param getEnd
   * @returns {Array}
   * @internal
   */
  getDates(level = this.columnLinesFor, useLowestHeader = false, getEnd = false) {
    const me = this,
      ticks = [],
      linesForLevel = useLowestHeader ? me.lowestHeader : level,
      majorLevel = me.majorHeaderLevel,
      levelUnit = me.headers && me.headers[level].unit,
      majorUnit = majorLevel != null && me.headers && me.headers[majorLevel].unit,
      validMajor = majorLevel != null && DateHelper.doesUnitsAlign(majorUnit, levelUnit),
      hasGenerator = !!(me.headers && me.headers[linesForLevel].cellGenerator);
    if (hasGenerator) {
      const cells = me.columnConfig[linesForLevel];
      for (let i = 1, l = cells.length; i < l; i++) {
        ticks.push({
          date: cells[i].startDate
        });
      }
    } else {
      me.forEachInterval(linesForLevel, (start, end) => {
        ticks.push({
          date: getEnd ? end : start,
          // do not want to consider tick to be major tick, hence the check for majorHeaderLevel
          isMajor: majorLevel !== level && validMajor && me.isMajorTick(getEnd ? end : start)
        });
      });
    }
    return ticks;
  }
  get forceFit() {
    return this._forceFit;
  }
  /**
   * This function fits the time columns into the available space in the time axis column.
   * @param {Boolean} suppressEvent `true` to skip firing the 'update' event.
   * @internal
   */
  fitToAvailableSpace(suppressEvent) {
    const proposedSize = Math.floor(this.availableSpace / this.timeAxis.visibleTickTimeSpan);
    this.setTickSize(proposedSize, suppressEvent);
  }
  get snap() {
    return this._snap;
  }
  /**
   * Gets/sets the snap value for the model. Setting it will cause it to update its contents and fire the
   * {@link #event-update} event.
   * @property {Boolean}
   * @internal
   */
  set snap(value) {
    if (value !== this._snap) {
      this._snap = value;
      if (this.configured) {
        this.update();
      }
    }
  }
  //endregion
  //region Headers
  // private
  createHeaderRow(position, headerRowConfig, headerCells) {
    const me = this,
      cells = [],
      {
        align,
        headerCellCls = ''
      } = headerRowConfig,
      today = DateHelper.clearTime(new Date()),
      {
        timeAxis
      } = me,
      tickLevel = me.headers.length - 1,
      createCellContext = (start, end, i, isLast, data) => {
        let value = DateHelper.format(start, headerRowConfig.dateFormat);
        const
          // So that we can use shortcut tickSize as the tickLevel cell width.
          // We can do this if the TimeAxis is aligned to start and end on tick boundaries
          // or if it's not the first or last tick.
          // getDistanceBetweenDates is an expensive operation.
          isInteriorTick = i > 0 && !isLast,
          cellData = {
            align,
            start,
            end,
            value: data ? data.header : value,
            headerCellCls,
            width: tickLevel === position && me.owner && (timeAxis.fullTicks || isInteriorTick) ? me.owner.tickSize : me.getDistanceBetweenDates(start, end),
            index: i
          };
        if (cellData.width === 0) {
          return;
        }
        // Vertical mode uses absolute positioning for header cells
        cellData.coord = size - 1;
        size += cellData.width;
        me.headersDatesCache[position][start.getTime()] = 1;
        if (headerRowConfig.renderer) {
          value = headerRowConfig.renderer.call(headerRowConfig.thisObj || me, start, end, cellData, i);
          cellData.value = value == null ? '' : value;
        }
        // To be able to style individual day cells, weekends or other important days
        if (headerRowConfig.unit === 'day' && (!headerRowConfig.increment || headerRowConfig.increment === 1)) {
          cellData.headerCellCls += ' b-sch-dayheadercell-' + start.getDay();
          if (DateHelper.clearTime(start, true) - today === 0) {
            cellData.headerCellCls += ' b-sch-dayheadercell-today';
          }
        }
        cells.push(cellData);
      };
    let size = 0;
    me.headersDatesCache[position] = {};
    if (headerCells) {
      headerCells.forEach((cellData, i) => createCellContext(cellData.start, cellData.end, i, i === headerCells.length - 1, cellData));
    } else {
      me.forEachInterval(position, createCellContext);
    }
    return cells;
  }
  get mainHeader() {
    return 'mainHeaderLevel' in this ? this.headers[this.mainHeaderLevel] : this.bottomHeader;
  }
  get bottomHeader() {
    return this.headers[this.headers.length - 1];
  }
  get lowestHeader() {
    return this.headers.length - 1;
  }
  /**
   * This method is meant to return the level of the header which 2nd lowest.
   * It is used for {@link #function-isMajorTick} method
   * @returns {String}
   * @private
   */
  get majorHeaderLevel() {
    const {
      headers
    } = this;
    if (headers) {
      return Math.max(headers.length - 2, 0);
    }
    return null;
  }
  //endregion
  //region Ticks
  /**
   * For vertical view (and column lines plugin) we sometimes want to know if current tick starts along with the
   * upper header level.
   * @param {Date} date
   * @returns {Boolean}
   * @private
   */
  isMajorTick(date) {
    const nextLevel = this.majorHeaderLevel;
    // if forceFit is used headersDatesCache won´t have been generated yet on the first call here,
    // since no size is set yet
    return nextLevel != null && this.headersDatesCache[nextLevel] && this.headersDatesCache[nextLevel][date.getTime()] || false;
  }
  /**
   * Calls the supplied iterator function once per interval. The function will be called with three parameters, start date and end date and an index.
   * Return false to break the iteration.
   * @param {Number} position The index of the header in the headers array.
   * @param {Function} iteratorFn The function to call, will be called with start date, end date and "tick index"
   * @param {Object} [thisObj] `this` reference for the function
   * @internal
   */
  forEachInterval(position, iteratorFn, thisObj = this) {
    const {
      headers,
      timeAxis
    } = this;
    if (headers) {
      // This is the lowest header row, which should be fed the data in the tickStore (or a row above using same unit)
      if (position === headers.length - 1) {
        timeAxis.forEach((r, index) => iteratorFn.call(thisObj, r.startDate, r.endDate, index, index === timeAxis.count - 1));
      }
      // All other rows
      else {
        const header = headers[position];
        timeAxis.forEachAuxInterval(header.unit, header.increment, iteratorFn, thisObj);
      }
    }
  }
  /**
   * Calls the supplied iterator function once per interval. The function will be called with three parameters, start date and end date and an index.
   * Return false to break the iteration.
   * @internal
   * @param {Function} iteratorFn The function to call
   * @param {Object} [thisObj] `this` reference for the function
   */
  forEachMainInterval(iteratorFn, thisObj) {
    this.forEachInterval(this.mainHeaderLevel, iteratorFn, thisObj);
  }
  //endregion
  //region ViewPreset
  consumeViewPreset(preset) {
    const me = this;
    // clear the cached headers
    me.headers = null;
    me.getConfig('tickSize');
    // Since we are bypassing the tickSize setter below, ensure that
    // the config initial setter has been removed by referencing the property.
    // We only do this to avoid multiple updates from this.
    me.viewPreset = preset;
    Object.assign(me, {
      headers: preset.headers,
      columnLinesFor: preset.columnLinesFor,
      mainHeaderLevel: preset.mainHeaderLevel,
      _tickSize: me.isHorizontal ? preset.tickWidth : preset.tickHeight
    });
    me.originalTickSize = me.tickSize;
  }
  //endregion
}

TimeAxisViewModel._$name = 'TimeAxisViewModel';

// Used to avoid having to create huge amounts of Date objects
const tempDate = new Date();
/**
 * @module Scheduler/view/mixin/TimelineDateMapper
 */
/**
 * Mixin that contains functionality to convert between coordinates and dates etc.
 *
 * @mixin
 */
var TimelineDateMapper = (Target => class TimelineDateMapper extends (Target || Base) {
  static $name = 'TimelineDateMapper';
  static configurable = {
    /**
     * Set to `true` to snap to the current time resolution increment while interacting with scheduled events.
     *
     * The time resolution increment is either determined by the currently applied view preset, or it can be
     * overridden using {@link #property-timeResolution}.
     *
     * <div class="note">When the {@link Scheduler/view/mixin/TimelineEventRendering#config-fillTicks} option is
     * enabled, snapping will align to full ticks, regardless of the time resolution.</div>
     *
     * @prp {Boolean}
     * @default
     * @category Scheduled events
     */
    snap: false
  };
  //region Coordinate <-> Date
  getRtlX(x) {
    if (this.rtl && this.isHorizontal) {
      x = this.timeAxisViewModel.totalSize - x;
    }
    return x;
  }
  /**
   * Gets the date for an X or Y coordinate, either local to the view element or the page based on the 3rd argument.
   * If the coordinate is not in the currently rendered view, null will be returned unless the `allowOutOfRange`
   * parameter is passed a `true`.
   * @param {Number} coordinate The X or Y coordinate
   * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @param {Boolean} [local] true if the coordinate is local to the scheduler view element
   * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
   * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
   * @returns {Date} The Date corresponding to the X or Y coordinate
   * @category Dates
   */
  getDateFromCoordinate(coordinate, roundingMethod, local = true, allowOutOfRange = false, ignoreRTL = false) {
    if (!local) {
      coordinate = this.currentOrientation.translateToScheduleCoordinate(coordinate);
    }
    // Time axis is flipped for RTL
    if (!ignoreRTL) {
      coordinate = this.getRtlX(coordinate);
    }
    return this.timeAxisViewModel.getDateFromPosition(coordinate, roundingMethod, allowOutOfRange);
  }
  getDateFromCoord(options) {
    return this.getDateFromCoordinate(options.coord, options.roundingMethod, options.local, options.allowOutOfRange, options.ignoreRTL);
  }
  /**
   * Gets the date for an XY coordinate regardless of the orientation of the time axis.
   * @param {Array} xy The page X and Y coordinates
   * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @param {Boolean} [local] true if the coordinate is local to the scheduler element
   * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
   * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
   * @returns {Date} the Date corresponding to the xy coordinate
   * @category Dates
   */
  getDateFromXY(xy, roundingMethod, local = true, allowOutOfRange = false) {
    return this.currentOrientation.getDateFromXY(xy, roundingMethod, local, allowOutOfRange);
  }
  /**
   * Gets the time for a DOM event such as 'mousemove' or 'click' regardless of the orientation of the time axis.
   * @param {Event} e the Event instance
   * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
   * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
   * @returns {Date} The date corresponding to the EventObject's position along the orientation of the time axis.
   * @category Dates
   */
  getDateFromDomEvent(e, roundingMethod, allowOutOfRange = false) {
    return this.getDateFromXY([e.pageX, e.pageY], roundingMethod, false, allowOutOfRange);
  }
  /**
   * Gets the start and end dates for an element Region
   * @param {Core.helper.util.Rectangle} rect The rectangle to map to start and end dates
   * @param {'floor'|'round'|'ceil'} roundingMethod Rounding method to use. 'floor' to take the tick (lowest header
   * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
   * @param {Number} duration The duration in MS of the underlying event
   * @returns {Object} an object containing start/end properties
   */
  getStartEndDatesFromRectangle(rect, roundingMethod, duration, allowOutOfRange = false) {
    const me = this,
      {
        isHorizontal
      } = me,
      startPos = isHorizontal ? rect.x : rect.top,
      endPos = isHorizontal ? rect.right : rect.bottom;
    let start, end;
    // Element within bounds
    if (startPos >= 0 && endPos < me.timeAxisViewModel.totalSize) {
      start = me.getDateFromCoordinate(startPos, roundingMethod, true);
      end = me.getDateFromCoordinate(endPos, roundingMethod, true);
    }
    // Starts before, start is worked backwards from end
    else if (startPos < 0) {
      end = me.getDateFromCoordinate(endPos, roundingMethod, true, allowOutOfRange);
      start = end && DateHelper.add(end, -duration, 'ms');
    }
    // Ends after, end is calculated from the start
    else {
      start = me.getDateFromCoordinate(startPos, roundingMethod, true, allowOutOfRange);
      end = start && DateHelper.add(start, duration, 'ms');
    }
    return {
      start,
      end
    };
  }
  //endregion
  //region Date display
  /**
   * Method to get a displayed end date value, see {@link #function-getFormattedEndDate} for more info.
   * @private
   * @param {Date} endDate The date to format
   * @param {Date} startDate The start date
   * @returns {Date} The date value to display
   */
  getDisplayEndDate(endDate, startDate) {
    if (
    // If time is midnight,
    endDate.getHours() === 0 && endDate.getMinutes() === 0 && (
    // and end date is greater then start date
    !startDate || !(endDate.getYear() === startDate.getYear() && endDate.getMonth() === startDate.getMonth() && endDate.getDate() === startDate.getDate())) &&
    // and UI display format doesn't contain hour info (in this case we'll just display the exact date)
    !DateHelper.formatContainsHourInfo(this.displayDateFormat)) {
      // format the date inclusively as 'the whole previous day'.
      endDate = DateHelper.add(endDate, -1, 'day');
    }
    return endDate;
  }
  /**
   * Method to get a formatted end date for a scheduled event, the grid uses the "displayDateFormat" property defined in the current view preset.
   * End dates are formatted as 'inclusive', meaning when an end date falls on midnight and the date format doesn't involve any hour/minute information,
   * 1ms will be subtracted (e.g. 2010-01-08T00:00:00 will first be modified to 2010-01-07 before being formatted).
   * @private
   * @param {Date} endDate The date to format
   * @param {Date} startDate The start date
   * @returns {String} The formatted date
   */
  getFormattedEndDate(endDate, startDate) {
    return this.getFormattedDate(this.getDisplayEndDate(endDate, startDate));
  }
  //endregion
  //region Other date functions
  /**
   * Gets the x or y coordinate relative to the scheduler element, or page coordinate (based on the 'local' flag)
   * If the coordinate is not in the currently rendered view, -1 will be returned.
   * @param {Date|Number} date the date to query for (or a date as ms)
   * @param {Boolean|Object} options true to return a coordinate local to the scheduler view element (defaults to true),
   * also accepts a config object like { local : true }.
   * @returns {Number} the x or y position representing the date on the time axis
   * @category Dates
   */
  getCoordinateFromDate(date, options = true) {
    var _options;
    const me = this,
      {
        timeAxisViewModel
      } = me,
      {
        isContinuous,
        startMS,
        endMS,
        startDate,
        endDate,
        unit
      } = me.timeAxis,
      dateMS = date.valueOf();
    // Avoiding to break the API while allowing passing options through to getPositionFromDate()
    if (options === true) {
      options = {
        local: true
      };
    } else if (!options) {
      options = {
        local: false
      };
    } else if (!('local' in options)) {
      options.local = true;
    }
    let pos;
    if (!(date instanceof Date)) {
      tempDate.setTime(date);
      date = tempDate;
    }
    // Shortcut for continuous time axis that is using a unit that can be reliably translated to days (or smaller)
    if (isContinuous && date.getTimezoneOffset() === startDate.getTimezoneOffset() && startDate.getTimezoneOffset() === endDate.getTimezoneOffset() && DateHelper.getUnitToBaseUnitRatio(unit, 'day') !== -1) {
      if (dateMS < startMS || dateMS > endMS) {
        return -1;
      }
      pos = (dateMS - startMS) / (endMS - startMS) * timeAxisViewModel.totalSize;
    }
    // Non-continuous or using for example months (vary in length)
    else {
      pos = timeAxisViewModel.getPositionFromDate(date, options);
    }
    // RTL coords from the end of the time axis
    if (me.rtl && me.isHorizontal && !((_options = options) !== null && _options !== void 0 && _options.ignoreRTL)) {
      pos = timeAxisViewModel.totalSize - pos;
    }
    if (!options.local) {
      pos = me.currentOrientation.translateToPageCoordinate(pos);
    }
    return pos;
  }
  /**
   * Returns the distance in pixels for the time span in the view.
   * @param {Date} startDate The start date of the span
   * @param {Date} endDate The end date of the span
   * @returns {Number} The distance in pixels
   * @category Dates
   */
  getTimeSpanDistance(startDate, endDate) {
    return this.timeAxisViewModel.getDistanceBetweenDates(startDate, endDate);
  }
  /**
   * Returns the center date of the currently visible timespan of scheduler.
   *
   * @property {Date}
   * @readonly
   * @category Dates
   */
  get viewportCenterDate() {
    const {
      timeAxis,
      timelineScroller
    } = this;
    // Take the easy way if the axis is continuous.
    // We can just work out how far along the time axis the viewport center is.
    if (timeAxis.isContinuous) {
      // The offset from the start of the whole time axis
      const timeAxisOffset = (timelineScroller.position + timelineScroller.clientSize / 2) / timelineScroller.scrollSize;
      return new Date(timeAxis.startMS + (timeAxis.endMS - timeAxis.startMS) * timeAxisOffset);
    }
    return this.getDateFromCoordinate(timelineScroller.position + timelineScroller.clientSize / 2);
  }
  get viewportCenterDateCached() {
    return this.cachedCenterDate || (this.cachedCenterDate = this.viewportCenterDate);
  }
  //endregion
  //region TimeAxis getters/setters
  /**
   * Gets/sets the current time resolution object, which contains a unit identifier and an increment count
   * `{ unit, increment }`. This value means minimal task duration you can create using UI.
   *
   * For example when you drag create a task or drag & drop a task, if increment is 5 and unit is 'minute'
   * that means that you can create tasks in 5 minute increments, or move it in 5 minute steps.
   *
   * This value is taken from viewPreset {@link Scheduler.preset.ViewPreset#field-timeResolution timeResolution}
   * config by default. When supplying a `Number` to the setter only the `increment` is changed and the `unit` value
   * remains untouched.
   *
   * ```javascript
   * timeResolution : {
   *   unit      : 'minute',  //Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
   *   increment : 5
   * }
   * ```
   *
   * <div class="note">When the {@link Scheduler/view/mixin/TimelineEventRendering#config-fillTicks} option is
   * enabled, the resolution will be in full ticks regardless of configured value.</div>
   *
   * @property {Object|Number}
   * @category Dates
   */
  get timeResolution() {
    return this.timeAxis.resolution;
  }
  set timeResolution(resolution) {
    this.timeAxis.resolution = typeof resolution === 'number' ? {
      increment: resolution,
      unit: this.timeAxis.resolution.unit
    } : resolution;
  }
  //endregion
  //region Snap
  get snap() {
    var _this$_timeAxisViewMo;
    return ((_this$_timeAxisViewMo = this._timeAxisViewModel) === null || _this$_timeAxisViewMo === void 0 ? void 0 : _this$_timeAxisViewMo.snap) ?? this._snap;
  }
  updateSnap(snap) {
    if (!this.isConfiguring) {
      this.timeAxisViewModel.snap = snap;
      this.timeAxis.forceFullTicks = snap && this.fillTicks;
    }
  }
  //endregion
  onSchedulerHorizontalScroll({
    subGrid,
    scrollLeft,
    scrollX
  }) {
    // Invalidate cached center date unless we are scrolling to center on it.
    if (!this.scrollingToCenter) {
      this.cachedCenterDate = null;
    }
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineDomEvents
 */
const {
  eventNameMap
} = EventHelper;
/**
 * An object which encapsulates a schedule timeline tick context based on a DOM event. This will include
 * the row and resource information and the tick and time information for a DOM pointer event detected
 * in the timeline.
 * @typedef {Object} TimelineContext
 * @property {Event} domEvent The DOM event which triggered the context change.
 * @property {HTMLElement} eventElement If the `domEvent` was on an event bar, this will be the event bar element.
 * @property {HTMLElement} cellElement The cell element under the `domEvent`
 * @property {Date} date The date corresponding to the `domEvent` position in the timeline
 * @property {Scheduler.model.TimeSpan} tick A {@link Scheduler.model.TimeSpan} record which encapsulates the contextual tick
 * @property {Number} tickIndex The contextual tick index. This may be fractional.
 * @property {Number} tickParentIndex The integer contextual tick index.
 * @property {Date} tickStartDate The start date of the contextual tick.
 * @property {Date} tickEndDate The end date of the contextual tick.
 * @property {Grid.row.Row} row The contextual {@link Grid.row.Row}
 * @property {Number} index The contextual row index
 * @property {Scheduler.model.EventModel} [eventRecord] The contextual event record (if any) if the event source is a `Scheduler`
 * @property {Scheduler.model.AssignmentModel} [assignmentRecord] The contextual assignment record (if any) if the event source is a `Scheduler`
 * @property {Scheduler.model.ResourceModel} [resourceRecord] The contextual resource record(if any)  if the event source is a `Scheduler`
 */
/**
 * Mixin that handles dom events (click etc) for scheduler and rendered events.
 *
 * @mixin
 */
var TimelineDomEvents = (Target => class TimelineDomEvents extends (Target || Base) {
  /**
   * Fires after a click on a time axis cell
   * @event timeAxisHeaderClick
   * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
   * @param {Date} startDate The start date of the header cell
   * @param {Date} endDate The end date of the header cell
   * @param {Event} event The event object
   */
  /**
   * Fires after a double click on a time axis cell
   * @event timeAxisHeaderDblClick
   * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
   * @param {Date} startDate The start date of the header cell
   * @param {Date} endDate The end date of the header cell
   * @param {Event} event The event object
   */
  /**
   * Fires after a right click on a time axis cell
   * @event timeAxisHeaderContextMenu
   * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
   * @param {Date} startDate The start date of the header cell
   * @param {Date} endDate The end date of the header cell
   * @param {Event} event The event object
   */
  static $name = 'TimelineDomEvents';
  //region Default config
  static configurable = {
    /**
     * The currently hovered timeline context. This is updated as the mouse or pointer moves over the timeline.
     * @member {TimelineContext} timelineContext
     * @readonly
     * @category Dates
     */
    timelineContext: {
      $config: {
        // Reject non-changes so that when set from scheduleMouseMove and EventMouseMove,
        // we only update the context and fire events when it changes.
        equal(c1, c2) {
          // index is the resource index, tickParentIndex is the
          // tick's index in the TimeAxis.
          return (c1 === null || c1 === void 0 ? void 0 : c1.index) === (c2 === null || c2 === void 0 ? void 0 : c2.index) && (c1 === null || c1 === void 0 ? void 0 : c1.tickParentIndex) === (c2 === null || c2 === void 0 ? void 0 : c2.tickParentIndex) && !(((c1 === null || c1 === void 0 ? void 0 : c1.tickStartDate) || 0) - ((c2 === null || c2 === void 0 ? void 0 : c2.tickStartDate) || 0));
        }
      }
    },
    /**
     * By default, scrolling the schedule will update the {@link #property-timelineContext} to reflect the new
     * currently hovered context. When displaying a large number of events on screen at the same time, this will
     * have a slight impact on scrolling performance. In such scenarios, opt out of this behavior by setting
     * this config to `false`.
     * @default
     * @prp {Boolean}
     * @category Misc
     */
    updateTimelineContextOnScroll: true,
    /**
     * Set to `true` to ignore reacting to DOM events (mouseover/mouseout etc) while scrolling. Useful if you
     * want to maximize scroll performance.
     * @config {Boolean}
     * @default false
     */
    ignoreDomEventsWhileScrolling: null
  };
  static properties = {
    schedulerEvents: {
      pointermove: 'handleScheduleEvent',
      mouseover: 'handleScheduleEvent',
      mousedown: 'handleScheduleEvent',
      mouseup: 'handleScheduleEvent',
      click: 'handleScheduleEvent',
      dblclick: 'handleScheduleEvent',
      contextmenu: 'handleScheduleEvent',
      mousemove: 'handleScheduleEvent',
      mouseout: 'handleScheduleEvent'
    }
  };
  static delayable = {
    // Allow the scroll event to complete in its thread, and dispatch the mousemove event next AF
    onScheduleScroll: 'raf'
  };
  // Currently hovered events (can be parent + child)
  hoveredEvents = new Set();
  //endregion
  //region Init
  /**
   * Adds listeners for DOM events for the scheduler and its events.
   * Which events is specified in Scheduler#schedulerEvents.
   * @private
   */
  initDomEvents() {
    const me = this,
      {
        schedulerEvents
      } = me;
    // Set thisObj and element of the configured listener specs.
    schedulerEvents.element = me.timeAxisSubGridElement;
    schedulerEvents.thisObj = me;
    EventHelper.on(schedulerEvents);
    EventHelper.on({
      element: me.timeAxisSubGridElement,
      mouseleave: 'handleScheduleLeaveEvent',
      capture: true,
      thisObj: me
    });
    // This is to handle scroll events while the mouse is over the schedule.
    // For example magic mouse or touchpad scrolls, or scrolls caused by keyboard
    // navigation while the mouse happens to be over the schedule.
    // The context must update. We must consider any scroll because the document
    // or some other wrapping element could be scrolling the Scheduler under the mouse.
    if (me.updateTimelineContextOnScroll && BrowserHelper.supportsPointerEventConstructor) {
      EventHelper.on({
        element: document,
        scroll: 'onScheduleScroll',
        capture: true,
        thisObj: me
      });
    }
  }
  //endregion
  //region Event handling
  getTimeSpanMouseEventParams(eventElement, event) {
    throw new Error('Implement in subclass');
  }
  getScheduleMouseEventParams(cellData, event) {
    throw new Error('Implement in subclass');
  }
  /**
   * Wraps dom Events for the scheduler and event bars and fires as our events.
   * For example click -> scheduleClick or eventClick
   * @private
   * @param event
   */
  handleScheduleEvent(event) {
    const me = this;
    if (me.ignoreDomEventsWhileScrolling && (me.scrolling || me.timeAxisSubGrid.scrolling)) {
      return;
    }
    const timelineContext = me.getTimelineEventContext(event);
    // Cache the last pointer event so that  when scrolling below the mouse
    // we can inject mousemove events at that point.
    me.lastPointerEvent = event;
    // We are over the schedule region
    if (timelineContext) {
      // Only fire a scheduleXXXX event if we are *not* over an event.
      // If over an event fire (event|task)XXXX.
      me.trigger(`${timelineContext.eventElement ? me.scheduledEventName : 'schedule'}${eventNameMap[event.type] || StringHelper.capitalize(event.type)}`, timelineContext);
    }
    // If the context has changed, updateTimelineContext will fire events
    me.timelineContext = timelineContext;
  }
  handleScheduleLeaveEvent(event) {
    if (event.target === this.timeAxisSubGridElement) {
      this.handleScheduleEvent(event);
    }
  }
  /**
   * This handles the scheduler being scrolled below the mouse by trackpad or keyboard events.
   * The context, if present needs to be recalculated.
   * @private
   */
  onScheduleScroll({
    target
  }) {
    var _me$features$pan;
    const me = this;
    // If the latest mouse event resulted in setting a context, we need to reproduce that event at the same clientX,
    // clientY in order to keep the context up to date while scrolling.
    // If the scroll is because of a pan feature drag (on us or a partner), we must not do this.
    // Target might be removed in salesforce by Locker Service if scroll event occurs on body
    if (target && me.updateTimelineContextOnScroll && !((_me$features$pan = me.features.pan) !== null && _me$features$pan !== void 0 && _me$features$pan.isActive) && !me.partners.some(p => {
      var _p$features$pan;
      return (_p$features$pan = p.features.pan) === null || _p$features$pan === void 0 ? void 0 : _p$features$pan.isActive;
    }) && (target.contains(me.element) || me.bodyElement.contains(target))) {
      const {
        timelineContext,
        lastPointerEvent
      } = me;
      if (timelineContext) {
        var _GlobalEvents$current, _GlobalEvents$current2;
        const targetElement = DomHelper.elementFromPoint(timelineContext.domEvent.clientX, timelineContext.domEvent.clientY),
          pointerEvent = new BrowserHelper.PointerEventConstructor('pointermove', lastPointerEvent),
          mouseEvent = new MouseEvent('mousemove', lastPointerEvent);
        // See https://github.com/bryntum/support/issues/6274
        // The pointerId does not propagate correctly on the synthetic PointerEvent, but also is readonly, so
        // redefine the property. This is required by Ext JS gesture publisher which tracks pointer movements
        // while a pointer is down. Without the correct pointerId, Ext JS would see this move as a "missed"
        // pointerdown and forever await its pointerup (i.e., it would get stuck in the activeTouches). This
        // would cause all future events to be perceived as part of or the end of a drag and would never again
        // dispatch pointer events correctly. Finally, lastPointerEvent.pointerId is often incorrect (undefined
        // in fact), so check the most recent pointerdown/touchstart event and default to 1
        Object.defineProperty(pointerEvent, 'pointerId', {
          value: ((_GlobalEvents$current = GlobalEvents.currentPointerDown) === null || _GlobalEvents$current === void 0 ? void 0 : _GlobalEvents$current.pointerId) ?? ((_GlobalEvents$current2 = GlobalEvents.currentTouch) === null || _GlobalEvents$current2 === void 0 ? void 0 : _GlobalEvents$current2.identifier) ?? 1
        });
        // Drag code should ignore these synthetic events
        pointerEvent.scrollInitiated = mouseEvent.scrollInitiated = true;
        // Emulate the correct browser sequence for mouse move events
        targetElement === null || targetElement === void 0 ? void 0 : targetElement.dispatchEvent(pointerEvent);
        targetElement === null || targetElement === void 0 ? void 0 : targetElement.dispatchEvent(mouseEvent);
      }
    }
  }
  updateTimelineContext(context, oldContext) {
    /**
     * Fired when the pointer-activated {@link #property-timelineContext} has changed.
     * @event timelineContextChange
     * @param {TimelineContext} oldContext The tick/resource context being deactivated.
     * @param {TimelineContext} context The tick/resource context being activated.
     */
    this.trigger('timelineContextChange', {
      oldContext,
      context
    });
    if (context && !oldContext) {
      this.trigger('scheduleMouseEnter', context);
    } else if (!context) {
      this.trigger('scheduleMouseLeave', {
        event: oldContext.event
      });
    }
  }
  /**
   * Gathers contextual information about the schedule contextual position of the passed event.
   *
   * Used by schedule mouse event handlers, but also by the scheduleContext feature.
   * @param {Event} domEvent The DOM event to gather context for.
   * @returns {TimelineContext} the schedule DOM event context
   * @internal
   */
  getTimelineEventContext(domEvent) {
    const me = this,
      eventElement = domEvent.target.closest(me.eventInnerSelector),
      cellElement = me.getCellElementFromDomEvent(domEvent);
    if (cellElement) {
      const date = me.getDateFromDomEvent(domEvent, 'floor');
      if (!date) {
        return;
      }
      const cellData = DomDataStore.get(cellElement),
        mouseParams = eventElement ? me.getTimeSpanMouseEventParams(eventElement, domEvent) : me.getScheduleMouseEventParams(cellData, domEvent);
      if (!mouseParams) {
        return;
      }
      const index = me.isVertical ? me.resourceStore.indexOf(mouseParams.resourceRecord) : cellData.row.dataIndex,
        tickIndex = me.timeAxis.getTickFromDate(date),
        tick = me.timeAxis.getAt(Math.floor(tickIndex));
      if (tick) {
        return {
          isTimelineContext: true,
          domEvent,
          eventElement,
          cellElement,
          index,
          tick,
          tickIndex,
          date,
          tickStartDate: tick.startDate,
          tickEndDate: tick.endDate,
          tickParentIndex: tick.parentIndex,
          row: cellData.row,
          event: domEvent,
          ...mouseParams
        };
      }
    }
  }
  getCellElementFromDomEvent({
    target,
    clientY,
    type
  }) {
    const me = this,
      {
        isVertical,
        foregroundCanvas
      } = me,
      eventElement = target.closest(me.eventSelector);
    // If event was on an event bar, calculate the cell.
    if (eventElement) {
      return me.getCell({
        [isVertical ? 'row' : 'record']: isVertical ? 0 : me.resolveRowRecord(eventElement),
        column: me.timeAxisColumn
      });
    }
    // If event was triggered by an element in the foreground canvas, but not an event element
    // we need to ascertain the cell "behind" that element to be able to create the context.
    else if (foregroundCanvas.contains(target)) {
      // Only trigger a Scheduler event if the event was on the background itself.
      // Otherwise, we will trigger unexpected events on things like dependency lines which historically
      // have never triggered scheduleXXXX events. The exception to this is the mousemove event which
      // needs to always fire so that timelineContext and scheduleTooltip correctly track the mouse
      if (target === foregroundCanvas || type === 'mousemove') {
        var _me$rowManager$getRow;
        return (_me$rowManager$getRow = me.rowManager.getRowAt(clientY, false)) === null || _me$rowManager$getRow === void 0 ? void 0 : _me$rowManager$getRow.getCell(me.timeAxisColumn.id);
      }
    } else {
      // Event was inside a row, or on a row border.
      return target.matches('.b-grid-row') ? target.firstElementChild : target.closest(me.timeCellSelector);
    }
  }
  // Overridden by ResourceTimeRanges to "pass events through" to the schedule
  matchScheduleCell(element) {
    return element.closest(this.timeCellSelector);
  }
  onElementMouseButtonEvent(event) {
    const targetCell = event.target.closest('.b-sch-header-timeaxis-cell');
    if (targetCell) {
      const me = this,
        position = targetCell.parentElement.dataset.headerPosition,
        headerCells = me.timeAxisViewModel.columnConfig[position],
        index = me.timeAxis.isFiltered ? headerCells.findIndex(cell => cell.index == targetCell.dataset.tickIndex) : targetCell.dataset.tickIndex,
        cellConfig = headerCells[index],
        contextMenu = me.features.contextMenu;
      // Skip same events with Grid context menu triggerEvent
      if (!contextMenu || event.type !== contextMenu.triggerEvent) {
        this.trigger(`timeAxisHeader${StringHelper.capitalize(event.type)}`, {
          startDate: cellConfig.start,
          endDate: cellConfig.end,
          event
        });
      }
    }
  }
  onElementMouseDown(event) {
    this.onElementMouseButtonEvent(event);
    super.onElementMouseDown(event);
  }
  onElementClick(event) {
    this.onElementMouseButtonEvent(event);
    super.onElementClick(event);
  }
  onElementDblClick(event) {
    this.onElementMouseButtonEvent(event);
    super.onElementDblClick(event);
  }
  onElementContextMenu(event) {
    this.onElementMouseButtonEvent(event);
    super.onElementContextMenu(event);
  }
  /**
   * Relays mouseover events as eventmouseenter if over rendered event.
   * Also adds Scheduler#overScheduledEventClass to the hovered element.
   * @private
   */
  onElementMouseOver(event) {
    var _me$features$eventDra;
    const me = this;
    if (me.ignoreDomEventsWhileScrolling && (me.scrolling || me.timeAxisSubGrid.scrolling)) {
      return;
    }
    super.onElementMouseOver(event);
    const {
        target
      } = event,
      {
        hoveredEvents
      } = me;
    // We must be over the event bar
    if (target.closest(me.eventInnerSelector) && !((_me$features$eventDra = me.features.eventDrag) !== null && _me$features$eventDra !== void 0 && _me$features$eventDra.isDragging)) {
      const eventElement = target.closest(me.eventSelector);
      if (!hoveredEvents.has(eventElement) && !me.preventOverCls) {
        hoveredEvents.add(eventElement);
        eventElement.classList.add(me.overScheduledEventClass);
        const params = me.getTimeSpanMouseEventParams(eventElement, event);
        if (params) {
          // do not fire this event if model cannot be found
          // this can be the case for "b-sch-dragcreator-proxy" elements for example
          me.trigger(`${me.scheduledEventName}MouseEnter`, params);
        }
      }
    } else if (hoveredEvents.size) {
      me.unhoverAll(event);
    }
  }
  /**
   * Relays mouseout events as eventmouseleave if out from rendered event.
   * Also removes Scheduler#overScheduledEventClass from the hovered element.
   * @private
   */
  onElementMouseOut(event) {
    var _me$features$eventDra2;
    super.onElementMouseOut(event);
    const me = this,
      {
        target,
        relatedTarget
      } = event,
      eventInner = target.closest(me.eventInnerSelector),
      eventWrap = target.closest(me.eventSelector),
      timeSpanRecord = me.resolveTimeSpanRecord(target);
    // We must be over the event bar
    if (eventInner && timeSpanRecord && me.hoveredEvents.has(eventWrap) && !((_me$features$eventDra2 = me.features.eventDrag) !== null && _me$features$eventDra2 !== void 0 && _me$features$eventDra2.isDragging)) {
      // out to child shouldn't count...
      if (relatedTarget && DomHelper.isDescendant(eventInner, relatedTarget)) {
        return;
      }
      me.unhover(eventWrap, event);
    }
  }
  unhover(element, event) {
    const me = this;
    element.classList.remove(me.overScheduledEventClass);
    me.trigger(`${me.scheduledEventName}MouseLeave`, me.getTimeSpanMouseEventParams(element, event));
    me.hoveredEvents.delete(element);
  }
  unhoverAll(event) {
    for (const element of this.hoveredEvents) {
      !element.isReleased && !element.classList.contains('b-released') && this.unhover(element, event);
    }
    // Might not be empty because of conditional unhover above
    this.hoveredEvents.clear();
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineViewPresets
 */
const datesDiffer = (d1 = 0, d2 = 0) => d2 - d1;
/**
 * View preset handling.
 *
 * A Scheduler's {@link #config-presets} are loaded with a default set of {@link Scheduler.preset.ViewPreset ViewPresets}
 * which are defined by the system in the {@link Scheduler.preset.PresetManager PresetManager}.
 *
 * The zooming feature works by reconfiguring the Scheduler with a new {@link Scheduler.preset.ViewPreset ViewPreset} selected
 * from the {@link #config-presets} store.
 *
 * {@link Scheduler.preset.ViewPreset ViewPresets} can be added and removed from the store to change the amount of available steps.
 * Range of zooming in/out can be also modified with {@link Scheduler.view.mixin.TimelineZoomable#config-maxZoomLevel} / {@link Scheduler.view.mixin.TimelineZoomable#config-minZoomLevel} properties.
 *
 * This mixin adds additional methods to the column : {@link Scheduler.view.mixin.TimelineZoomable#property-maxZoomLevel}, {@link Scheduler.view.mixin.TimelineZoomable#property-minZoomLevel}, {@link Scheduler.view.mixin.TimelineZoomable#function-zoomToLevel}, {@link Scheduler.view.mixin.TimelineZoomable#function-zoomIn},
 * {@link Scheduler.view.mixin.TimelineZoomable#function-zoomOut}, {@link Scheduler.view.mixin.TimelineZoomable#function-zoomInFull}, {@link Scheduler.view.mixin.TimelineZoomable#function-zoomOutFull}.
 *
 * **Notice**: Zooming is not supported when `forceFit` option is set to true for the Scheduler or for filtered timeaxis.
 *
 * @mixin
 */
var TimelineViewPresets = (Target => class TimelineViewPresets extends (Target || Base) {
  static get $name() {
    return 'TimelineViewPresets';
  }
  //region Default config
  static get configurable() {
    return {
      /**
       * A string key used to lookup a predefined {@link Scheduler.preset.ViewPreset} (e.g. 'weekAndDay', 'hourAndDay'),
       * managed by {@link Scheduler.preset.PresetManager}. See {@link Scheduler.preset.PresetManager} for more information.
       * Or a config object for a viewPreset.
       *
       * Options:
       * - 'secondAndMinute'
       * - 'minuteAndHour'
       * - 'hourAndDay'
       * - 'dayAndWeek'
       * - 'dayAndMonth'
       * - 'weekAndDay'
       * - 'weekAndMonth',
       * - 'monthAndYear'
       * - 'year'
       * - 'manyYears'
       * - 'weekAndDayLetter'
       * - 'weekDateAndMonth'
       * - 'day'
       * - 'week'
       *
       * If passed as a config object, the settings from the viewPreset with the provided `base` property will be used along
       * with any overridden values in your object.
       *
       * To override:
       * ```javascript
       * viewPreset : {
       *   base    : 'hourAndDay',
       *   id      : 'myHourAndDayPreset',
       *   headers : [
       *       {
       *           unit      : "hour",
       *           increment : 12,
       *           renderer  : (startDate, endDate, headerConfig, cellIdx) => {
       *               return "";
       *           }
       *       }
       *   ]
       * }
       * ```
       * or set a new valid preset config if the preset is not registered in the {@link Scheduler.preset.PresetManager}.
       *
       * When you use scheduler in weekview mode, this config is used to pick view preset. If passed view preset is not
       * supported by weekview (only 2 supported by default - 'day' and 'week') default preset will be used - 'week'.
       * @config {String|ViewPresetConfig}
       * @default
       * @category Common
       */
      viewPreset: 'weekAndDayLetter',
      /**
       * Get the {@link Scheduler.preset.PresetStore} created for the Scheduler,
       * or set an array of {@link Scheduler.preset.ViewPreset} config objects.
       * @member {Scheduler.preset.PresetStore|ViewPresetConfig[]} presets
       * @category Common
       */
      /**
       * An array of {@link Scheduler.preset.ViewPreset} config objects
       * which describes the available timeline layouts for this scheduler.
       *
       * By default, a predefined set is loaded from the {@link Scheduler.preset.PresetManager}.
       *
       * A {@link Scheduler.preset.ViewPreset} describes the granularity of the
       * timeline view and the layout and subdivisions of the timeline header.
       * @config {ViewPresetConfig[]} presets
       *
       * @category Common
       */
      presets: true,
      /**
       * Defines how dates will be formatted in tooltips etc. This config has priority over similar config on the
       * view preset. For allowed values see {@link Core.helper.DateHelper#function-format-static}.
       *
       * By default, this is ingested from {@link Scheduler.preset.ViewPreset} upon change of
       * {@link Scheduler.preset.ViewPreset} (Such as when zooming in or out). But Setting this
       * to your own value, overrides that behaviour.
       * @prp {String}
       * @category Scheduled events
       */
      displayDateFormat: null
    };
  }
  //endregion
  /**
   * Get/set the current view preset
   * @member {Scheduler.preset.ViewPreset|ViewPresetConfig|String} viewPreset
   * @param [viewPreset.options]
   * @param {Date} [viewPreset.options.startDate] A new start date for the time axis
   * @param {Date} [viewPreset.options.endDate] A new end date for the time axis
   * @param {Date} [viewPreset.options.centerDate] Where to center the new time axis
   * @category Common
  */
  //region Get/set
  changePresets(presets) {
    const config = {
      owner: this
    };
    let data = [];
    // By default includes all presets
    if (presets === true) {
      data = pm.allRecords;
    }
    // Accepts an array of presets
    else if (Array.isArray(presets)) {
      for (const preset of presets) {
        // If we got a presetId
        if (typeof preset === 'string') {
          const presetRecord = pm.getById(preset);
          if (presetRecord) {
            data.push(presetRecord);
          }
        } else {
          data.push(preset);
        }
      }
    }
    // Or a store config object
    else {
      ObjectHelper.assign(config, presets);
    }
    // Creates store first and then adds data, because data config does not support a mix of raw objects and records.
    const presetStore = new PresetStore(config);
    presetStore.add(data);
    return presetStore;
  }
  changeViewPreset(viewPreset, oldViewPreset) {
    const me = this,
      {
        presets
      } = me;
    if (viewPreset) {
      viewPreset = presets.createRecord(viewPreset);
      // If an existing ViewPreset id is used, this will replace it.
      if (!presets.includes(viewPreset)) {
        presets.add(viewPreset);
      }
    } else {
      viewPreset = presets.first;
    }
    const lastOpts = me.lastViewPresetOptions || {},
      options = viewPreset.options || (viewPreset.options = {}),
      event = options.event = {
        startDate: options.startDate,
        endDate: options.endDate,
        from: oldViewPreset,
        to: viewPreset,
        preset: viewPreset
      },
      presetChanged = !me._viewPreset || !me._viewPreset.equals(viewPreset),
      optionsChanged = datesDiffer(options.startDate, lastOpts.startDate) || datesDiffer(options.endDate, lastOpts.endDate) || datesDiffer(options.centerDate, lastOpts.centerDate) || options.startDate && datesDiffer(options.startDate, me.startDate) || options.endDate && datesDiffer(options.endDate, me.endDate);
    // Only return the value for onward processing if there's a change
    if (presetChanged || optionsChanged) {
      // Bypass the no-change check if the viewPreset is the same and we only got in here
      // because different options were asked for.
      if (!presetChanged) {
        me._viewPreset = null;
      }
      /**
       * Fired before the {@link #config-viewPreset} is changed.
       * @event beforePresetChange
       * @param {Scheduler.view.Scheduler} source This Scheduler instance.
       * @param {Date} startDate The new start date of the timeline.
       * @param {Date} endDate The new end date of the timeline.
       * @param {Scheduler.preset.ViewPreset} from The outgoing ViewPreset.
       * @param {Scheduler.preset.ViewPreset} to The ViewPreset being switched to.
       * @preventable
       */
      // Do not trigger events for the initial preset
      if (me.isConfiguring || me.trigger('beforePresetChange', event) !== false) {
        return viewPreset;
      }
    }
  }
  get displayDateFormat() {
    return this._displayDateFormat || this.viewPreset.displayDateFormat;
  }
  updateDisplayDateFormat(format) {
    // Start/EndDateColumn listens for this to change their format to match
    this.trigger('displayDateFormatChange', {
      format
    });
  }
  /**
   * Method to get a formatted display date
   * @private
   * @param {Date} date The date
   * @returns {String} The formatted date
   */
  getFormattedDate(date) {
    return DateHelper.format(date, this.displayDateFormat);
  }
  updateViewPreset(preset) {
    var _me$syncSplits;
    const me = this,
      {
        options
      } = preset,
      {
        event,
        startDate,
        endDate
      } = options,
      {
        isHorizontal,
        _timeAxis: timeAxis,
        // Do not tickle the getter, we are just peeking to see if it's there yet.
        _timeAxisViewModel: timeAxisViewModel // Ditto
      } = me,
      rtl = isHorizontal && me.rtl;
    let {
        centerDate,
        zoomDate,
        zoomPosition
      } = options,
      forceUpdate = false;
    (_me$syncSplits = me.syncSplits) === null || _me$syncSplits === void 0 ? void 0 : _me$syncSplits.call(me, split => split.viewPreset = preset);
    // Options must not be reused when this preset is used again.
    delete preset.options;
    // Raise flag to prevent partner from changing view preset if one is in progress
    me._viewPresetChanging = true;
    if (timeAxis && !me.isConfiguring) {
      const {
        timelineScroller
      } = me;
      // Cache options only when they are applied so that non-change vetoing in changeViewPreset is accurate
      me.lastViewPresetOptions = options;
      // Timeaxis may already be configured (in case of sharing with the timeline partner), no need to reconfigure it
      if (timeAxis.isConfigured) {
        // None of this reconfiguring should cause a refresh
        me.suspendRefresh();
        // Set up these configs only if we actually have them.
        const timeAxisCfg = ObjectHelper.copyProperties({}, me, ['weekStartDay', 'startTime', 'endTime']);
        if (me.infiniteScroll) {
          Object.assign(timeAxisCfg, timeAxisViewModel.calculateInfiniteScrollingDateRange(centerDate || new Date((startDate.getTime() + endDate.getTime()) / 2), true, preset));
        }
        // if startDate is provided we use it and the provided endDate
        else if (startDate) {
          timeAxisCfg.startDate = startDate;
          timeAxisCfg.endDate = endDate;
          // if both dates are provided we can calculate centerDate for the viewport
          if (!centerDate && endDate) {
            centerDate = new Date((startDate.getTime() + endDate.getTime()) / 2);
          }
          // when no start/end dates are provided we use the current timespan
        } else {
          timeAxisCfg.startDate = timeAxis.startDate;
          timeAxisCfg.endDate = endDate || timeAxis.endDate;
          if (!centerDate) {
            centerDate = me.viewportCenterDate;
          }
        }
        timeAxis.isConfigured = false;
        timeAxisCfg.viewPreset = preset;
        timeAxis.reconfigure(timeAxisCfg, true);
        timeAxisViewModel.reconfigure({
          viewPreset: preset,
          headers: preset.headers,
          // This was hardcoded to 'middle' prior to the Preset refactor.
          // In the old code, the default headers were 'top' and 'middle', which
          // meant that 'middle' meant the lowest header.
          // So this is now length - 1.
          columnLinesFor: preset.columnLinesFor != null ? preset.columnLinesFor : preset.headers.length - 1,
          tickSize: isHorizontal ? preset.tickWidth : preset.tickHeight || preset.tickWidth || 60
        });
        // Allow refresh to run after the reconfiguring, without refreshing since we will do that below anyway
        me.resumeRefresh(false);
      }
      me.refresh();
      // if view is rendered and scroll is not disabled by "notScroll" option
      if (!options.notScroll && me.isPainted) {
        if (options.visibleDate) {
          me.visibleDate = options.visibleDate;
        }
        // If a zoom at a certain date position is being requested, scroll the zoomDate
        // to the required zoomPosition so that the zoom happens centered where the
        // pointer events that are driving it targeted.
        else if (zoomDate && zoomPosition) {
          const unitMagnitude = unitMagnitudes[timeAxis.resolutionUnit],
            unit = unitMagnitude > 3 ? 'hour' : 'minute',
            milliseconds = DateHelper.asMilliseconds(unit === 'minute' ? 15 : 1, unit),
            // Round the date to either 15 minutes for fine levels or 1 hour for coarse levels
            targetDate = new Date(Math.round(zoomDate / milliseconds) * milliseconds);
          // setViewPreset method on partner panels should be executed with same arguments.
          // if one partner was provided with zoom info, other one has to be too to generate exact
          // header and set same scroll
          event.zoomDate = zoomDate;
          event.zoomPosition = zoomPosition;
          event.zoomLevel = options.zoomLevel;
          // Move the targetDate back under the mouse position as indicated by zoomPosition.
          // That is the offset into the TimeAxisSubGridElement.
          if (rtl) {
            timelineScroller.position = timelineScroller.scrollWidth - (me.getCoordinateFromDate(targetDate) + zoomPosition);
          } else {
            timelineScroller.position = me.getCoordinateFromDate(targetDate) - zoomPosition;
          }
        }
        // and we have centerDate to scroll to
        else if (centerDate) {
          // remember the central date we scroll to (it gets reset after user scroll)
          me.cachedCenterDate = centerDate;
          // setViewPreset method on partner panels should be executed with same arguments.
          // if one partner was provided with a centerDate, other one has to be too to generate exact
          // header and set same scroll
          event.centerDate = centerDate;
          const viewportSize = me.timelineScroller.clientSize,
            centerCoord = rtl ? me.timeAxisViewModel.totalSize - me.getCoordinateFromDate(centerDate, true) : me.getCoordinateFromDate(centerDate, true),
            coord = Math.max(centerCoord - viewportSize / 2, 0);
          // The horizontal scroll handler must not invalidate the cached center
          // when this scroll event rolls round on the next frame.
          me.scrollingToCenter = true;
          // If preset change does not lead to a scroll we have to "refresh" manually at the end
          if (coord === (me.isHorizontal ? me.scrollLeft : me.scrollTop)) {
            forceUpdate = true;
          } else if (me.isHorizontal) {
            me.scrollHorizontallyTo(coord, false);
          } else {
            me.scrollVerticallyTo(coord, false);
          }
          // Release the lock on scrolling invalidating the cached center.
          me.setTimeout(() => {
            me.scrollingToCenter = false;
          }, 100);
        } else {
          // If preset change does not lead to a scroll we have to "refresh" manually at the end
          if ((me.isHorizontal ? me.scrollLeft : me.scrollTop) === 0) {
            forceUpdate = true;
          }
          // If we don't have a center date to scroll to, we reset scroll (this is bw compatible behavior)
          else {
            me.timelineScroller.scrollTo(0);
          }
        }
      }
    }
    // Update Scheduler element showing what preset is applied
    me.dataset.presetId = preset.id;
    /**
     * Fired after the {@link #config-viewPreset} has changed.
     * @event presetChange
     * @param {Scheduler.view.Scheduler} source This Scheduler instance.
     * @param {Date} startDate The new start date of the timeline.
     * @param {Date} centerDate The new center date of the timeline.
     * @param {Date} endDate The new end date of the timeline.
     * @param {Scheduler.preset.ViewPreset} from The outgoing ViewPreset.
     * @param {Scheduler.preset.ViewPreset} to The ViewPreset being switched to.
     * @preventable
     */
    me.trigger('presetChange', event);
    me._viewPresetChanging = false;
    if (forceUpdate) {
      if (me.isHorizontal) {
        me.currentOrientation.updateFromHorizontalScroll(me.scrollLeft, true);
      } else {
        me.currentOrientation.updateFromVerticalScroll(me.scrollTop);
      }
    }
  }
  //endregion
  doDestroy() {
    if (this._presets.owner === this) {
      this._presets.destroy();
    }
    super.doDestroy();
  }
  // This function is not meant to be called by any code other than Base#getCurrentConfig().
  getCurrentConfig(options) {
    const result = super.getCurrentConfig(options);
    // Cannot store name, will not be allowed when reapplying
    if (result.viewPreset && result.viewPreset.name && !result.viewPreset.base) {
      delete result.viewPreset.name;
    }
    return result;
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineZoomable
 */
/**
 * Options which may be used when changing the {@link Scheduler.view.Scheduler#property-viewPreset} property.
 *
 * @typedef {Object} ChangePresetOptions
 * @property {VisibleDate} [visibleDate] A `visibleDate` specification to bring into view after the new
 * `ViewPreset` is applied.
 * @property {Date} [startDate] New time frame start. If provided along with end, view will be centered in this
 * time interval, ignoring centerDate config. __Ignored if {@link Scheduler.view.Scheduler#config-infiniteScroll} is used.__
 * @property {Date} [endDate] New time frame end. __Ignored if {@link Scheduler.view.Scheduler#config-infiniteScroll} is used.__
 * @property {Date} [centerDate] Date to keep in center. Is ignored when start and end are provided.
 * @property {Date} [zoomDate] The date that should be positioned at the passed `datePosition` client offset.
 * @property {Number} [zoomPosition] The client offset at which the passed `zoomDate` should be positioned.
 * @property {Number} [width] Lowest tick width. Might be increased automatically
 */
/**
 * Mixin providing "zooming" functionality.
 *
 * The zoom levels are stored as instances of {@link Scheduler.preset.ViewPreset}s, and are
 * cached centrally in the {@link Scheduler.preset.PresetManager}.
 *
 * The default presets are loaded into the {@link Scheduler.view.mixin.TimelineViewPresets#config-presets}
 * store upon Scheduler instantiation. Preset selection is covered in the
 * {@link Scheduler.view.mixin.TimelineViewPresets} mixin.
 *
 * To specify custom zoom levels please provide a set of view presets to the global PresetManager store **before**
 * scheduler creation, or provide a set of view presets to a specific scheduler only:
 *
 * ```javascript
 * const myScheduler = new Scheduler({
 *     presets : [
 *         {
 *             base : 'hourAndDay',
 *             id   : 'MyHourAndDay',
 *             // other preset configs....
 *         },
 *         {
 *             base : 'weekAndMonth',
 *             id   : 'MyWeekAndMonth',
 *             // other preset configs....
 *         }
 *     ],
 *     viewPreset : 'MyHourAndDay',
 *     // other scheduler configs....
 *     });
 * ```
 *
 * @mixin
 */
var TimelineZoomable = (Target => class TimelineZoomable extends (Target || Base) {
  static $name = 'TimelineZoomable';
  static defaultConfig = {
    /**
     * If true, you can zoom in and out on the time axis using CTRL-key + mouse wheel.
     * @config {Boolean}
     * @default
     * @category Zoom
     */
    zoomOnMouseWheel: true,
    /**
     * True to zoom to time span when double-clicking a time axis cell.
     * @config {Boolean}
     * @default
     * @category Zoom
     */
    zoomOnTimeAxisDoubleClick: true,
    /**
     * The minimum zoom level to which {@link #function-zoomOut} will work. Defaults to 0 (year ticks)
     * @config {Number}
     * @category Zoom
     * @default
     */
    minZoomLevel: 0,
    /**
     * The maximum zoom level to which {@link #function-zoomIn} will work. Defaults to the number of
     * {@link Scheduler.preset.ViewPreset ViewPresets} available, see {@link Scheduler/view/mixin/TimelineViewPresets#property-presets}
     * for information. Unless you have modified the collection of available presets, the max zoom level is
     * milliseconds.
     * @config {Number}
     * @category Zoom
     * @default 23
     */
    maxZoomLevel: null,
    /**
     * Integer number indicating the size of timespan during zooming. When zooming, the timespan is adjusted to make
     * the scrolling area `visibleZoomFactor` times wider than the timeline area itself. Used in
     * {@link #function-zoomToSpan} and {@link #function-zoomToLevel} functions.
     * @config {Number}
     * @default
     * @category Zoom
     */
    visibleZoomFactor: 5,
    /**
     * Whether the originally rendered timespan should be preserved while zooming. By default, it is set to `false`,
     * meaning the timeline panel will adjust the currently rendered timespan to limit the amount of HTML content to
     * render. When setting this option to `true`, be careful not to allow to zoom a big timespan in seconds
     * resolution for example. That will cause **a lot** of HTML content to be rendered and affect performance. You
     * can use {@link #config-minZoomLevel} and {@link #config-maxZoomLevel} config options for that.
     * @config {Boolean}
     * @default
     * @category Zoom
     */
    zoomKeepsOriginalTimespan: null
  };
  // We cache the last mousewheel position, so that during zooming we can
  // maintain a stable zoom point even if the mouse moves a little.
  lastWheelTime = -1;
  lastZoomPosition = -1;
  construct(config) {
    const me = this;
    super.construct(config);
    if (me.zoomOnMouseWheel) {
      EventHelper.on({
        element: me.timeAxisSubGridElement,
        wheel: 'onWheel',
        // Throttle zooming with the wheel a bit to have greater control of it
        throttled: {
          buffer: 100,
          // Prevent events from slipping through the throttle, causing scroll
          alt: e => e.ctrlKey && e.preventDefault()
        },
        thisObj: me,
        capture: true,
        passive: false
      });
    }
    if (me.zoomOnTimeAxisDoubleClick) {
      me.ion({
        timeaxisheaderdblclick: ({
          startDate,
          endDate
        }) => {
          if (!me.forceFit) {
            me.zoomToSpan({
              startDate,
              endDate
            });
          }
        }
      });
    }
  }
  get maxZoomLevel() {
    return this._maxZoomLevel || this.presets.count - 1;
  }
  /**
   * Get/set the {@link #config-maxZoomLevel} value
   * @property {Number}
   * @category Zoom
   */
  set maxZoomLevel(level) {
    if (typeof level !== 'number') {
      level = this.presets.count - 1;
    }
    if (level < 0 || level >= this.presets.count) {
      throw new Error('Invalid range for `maxZoomLevel`');
    }
    this._maxZoomLevel = level;
  }
  get minZoomLevel() {
    return this._minZoomLevel;
  }
  /**
   * Sets the {@link #config-minZoomLevel} value
   * @property {Number}
   * @category Zoom
   */
  set minZoomLevel(level) {
    if (typeof level !== 'number') {
      level = 0;
    }
    if (level < 0 || level >= this.presets.count) {
      throw new Error('Invalid range for `minZoomLevel`');
    }
    this._minZoomLevel = level;
  }
  /**
   * Current zoom level, which is equal to the {@link Scheduler.preset.ViewPreset ViewPreset} index
   * in the provided array of {@link Scheduler.view.mixin.TimelineViewPresets#config-presets zoom levels}.
   * @property {Number}
   * @category Zoom
   */
  get zoomLevel() {
    return this.presets.indexOf(this.viewPreset);
  }
  // noinspection JSAnnotator
  set zoomLevel(level) {
    this.zoomToLevel(level);
  }
  /**
   * Returns number of milliseconds per pixel.
   * @param {Object} level Element from array of {@link Scheduler.view.mixin.TimelineViewPresets#config-presets}.
   * @param {Boolean} ignoreActualWidth If true, then density will be calculated using default zoom level settings.
   * Otherwise, density will be calculated for actual tick width.
   * @returns {Number} Return number of milliseconds per pixel.
   * @private
   */
  getMilliSecondsPerPixelForZoomLevel(preset, ignoreActualWidth) {
    const {
        bottomHeader
      } = preset,
      // Scheduler uses direction independent tickSize, but presets are allowed to define different sizes for
      // vertical and horizontal -> cant use preset.tickSize here
      width = this.isHorizontal ? preset.tickWidth : preset.tickHeight;
    // trying to convert the unit + increment to a number of milliseconds
    // this number is not fixed (month can be 28, 30 or 31 day), but at least this conversion
    // will be consistent (should be no DST changes at year 1)
    return Math.round((DateHelper.add(new Date(1, 0, 1), bottomHeader.increment || 1, bottomHeader.unit) - new Date(1, 0, 1)) / (
    // `actualWidth` is a column width after view adjustments applied to it (see `calculateTickWidth`)
    // we use it if available to return the precise index value from `getCurrentZoomLevelIndex`
    ignoreActualWidth ? width : preset.actualWidth || width));
  }
  /**
   * Zooms to passed view preset, saving center date. Method accepts config object as a first argument, which can be
   * reduced to primitive type (string,number) when no additional options required. e.g.:
   * ```javascript
   * // zooming to preset
   * scheduler.zoomTo({ preset : 'hourAndDay' })
   * // shorthand
   * scheduler.zoomTo('hourAndDay')
   *
   * // zooming to level
   * scheduler.zoomTo({ level : 0 })
   * // shorthand
   * scheduler.zoomTo(0)
   * ```
   *
   * It is also possible to zoom to a time span by omitting `preset` and `level` configs, in which case scheduler sets
   * the time frame to a specified range and applies zoom level which allows to fit all columns to this range. The
   * given time span will be centered in the scheduling view (unless `centerDate` config provided). In the same time,
   * the start/end date of the whole time axis will be extended to allow scrolling for user.
   * ```javascript
   * // zooming to time span
   * scheduler.zoomTo({
   *     startDate : new Date(..),
   *     endDate : new Date(...)
   * });
   * ```
   *
   * @param {ViewPresetConfig|Object|String|Number} config Config object, preset name or zoom level index.
   * @param {String} [config.preset] Preset name to zoom to. Ignores level config in this case
   * @param {Number} [config.level] Zoom level to zoom to. Is ignored, if preset config is provided
   * @param {VisibleDate} [config.visibleDate] A `visibleDate` specification to bring into view after the zoom.
   * @param {Date} [config.startDate] New time frame start. If provided along with end, view will be centered in this
   * time interval (unless `centerDate` is present)
   * @param {Date} [config.endDate] New time frame end
   * @param {Date} [config.centerDate] Date that should be kept in the center. Has priority over start and end params
   * @param {Date} [config.zoomDate] The date that should be positioned at the passed `datePosition` client offset.
   * @param {Number} [config.zoomPosition] The client offset at which the passed `date` should be positioned.
   * @param {Number} [config.width] Lowest tick width. Might be increased automatically
   * @param {Number} [config.leftMargin] Amount of pixels to extend span start on (used, when zooming to span)
   * @param {Number} [config.rightMargin] Amount of pixels to extend span end on (used, when zooming to span)
   * @param {Number} [config.adjustStart] Amount of units to extend span start on (used, when zooming to span)
   * @param {Number} [config.adjustEnd] Amount of units to extend span end on (used, when zooming to span)
   * @category Zoom
   */
  zoomTo(config) {
    const me = this;
    if (typeof config === 'object') {
      if (config.preset) {
        me.zoomToLevel(config.preset, config);
      } else if (config.level != null) {
        me.zoomToLevel(config.level, config);
      } else {
        me.zoomToSpan(config);
      }
    } else {
      me.zoomToLevel(config);
    }
  }
  /**
   * Allows zooming to certain level of {@link Scheduler.view.mixin.TimelineViewPresets#config-presets} array.
   * Automatically limits zooming between {@link #config-maxZoomLevel} and {@link #config-minZoomLevel}. Can also set
   * time axis timespan to the supplied start and end dates.
   *
   * @param {Number} preset Level to zoom to.
   * @param {ChangePresetOptions} [options] Object containing options which affect how the new preset is applied.
   * @returns {Number|null} level Current zoom level or null if it hasn't changed.
   * @category Zoom
   */
  zoomToLevel(preset, options = {}) {
    if (this.forceFit) {
      console.warn('Warning: The forceFit setting and zooming cannot be combined');
      return;
    }
    // Sanitize numeric zooming.
    if (typeof preset === 'number') {
      preset = Math.min(Math.max(preset, this.minZoomLevel), this.maxZoomLevel);
    }
    const me = this,
      {
        presets
      } = me,
      tickSizeProp = me.isVertical ? 'tickHeight' : 'tickWidth',
      newPreset = presets.createRecord(preset),
      configuredTickSize = newPreset[tickSizeProp],
      startDate = options.startDate ? new Date(options.startDate) : null,
      endDate = options.endDate ? new Date(options.endDate) : null;
    // If an existing ViewPreset id is used, this will replace it.
    presets.add(newPreset);
    let span = startDate && endDate ? {
      startDate,
      endDate
    } : null;
    const centerDate = options.centerDate ? new Date(options.centerDate) : span ? new Date((startDate.getTime() + endDate.getTime()) / 2) : me.viewportCenterDateCached;
    let scrollableViewportSize = me.isVertical ? me.scrollable.clientHeight : me.timeAxisSubGrid.width;
    if (scrollableViewportSize === 0) {
      const {
        _beforeCollapseState
      } = me.timeAxisSubGrid;
      if (me.isHorizontal && me.timeAxisSubGrid.collapsed && _beforeCollapseState !== null && _beforeCollapseState !== void 0 && _beforeCollapseState.width) {
        scrollableViewportSize = _beforeCollapseState.width;
      } else {
        return null;
      }
    }
    // Always calculate an optimal date range for the new zoom level
    if (!span) {
      span = me.calculateOptimalDateRange(centerDate, scrollableViewportSize, newPreset);
    }
    // Temporarily override tick size while reconfiguring the TimeAxisViewModel
    if ('width' in options) {
      newPreset.setData(tickSizeProp, options.width);
    }
    me.isZooming = true;
    // Passed through to the viewPreset changing method
    newPreset.options = {
      ...options,
      startDate: span.startDate || me.startDate,
      endDate: span.endDate || me.endDate,
      centerDate
    };
    me.viewPreset = newPreset;
    // after switching the view preset the `width` config of the zoom level may change, because of adjustments
    // we will save the real value in the `actualWidth` property, so that `getCurrentZoomLevelIndex` method
    // will return the exact level index after zooming
    newPreset.actualWidth = me.timeAxisViewModel.tickSize;
    me.isZooming = false;
    // Restore the tick size because the default presets are shared.
    newPreset.setData(tickSizeProp, configuredTickSize);
    return me.zoomLevel;
  }
  /**
   * Changes the range of the scheduling chart to fit all the events in its event store.
   * @param {Object} [options] Options object for the zooming operation.
   * @param {Number} [options.leftMargin] Defines margin in pixel between the first event start date and first visible
   * date
   * @param {Number} [options.rightMargin] Defines margin in pixel between the last event end date and last visible
   * date
   */
  zoomToFit(options) {
    const eventStore = this.eventStore,
      span = eventStore.getTotalTimeSpan();
    options = {
      leftMargin: 0,
      rightMargin: 0,
      ...options,
      ...span
    };
    // Make sure we received a time span, event store might be empty
    if (options.startDate && options.endDate) {
      if (options.endDate > options.startDate) {
        this.zoomToSpan(options);
      } else {
        // If we only had a zero time span, just scroll it into view
        this.scrollToDate(options.startDate);
      }
    }
  }
  /**
   * Sets time frame to specified range and applies zoom level which allows to fit all columns to this range.
   *
   * The given time span will be centered in the scheduling view, in the same time, the start/end date of the whole
   * time axis will be extended in the same way as {@link #function-zoomToLevel} method does, to allow scrolling for
   * user.
   *
   * @param {Object} config The time frame.
   * @param {Date} config.startDate The time frame start.
   * @param {Date} config.endDate The time frame end.
   * @param {Date} [config.centerDate] Date that should be kept in the center. Has priority over start and end params
   * @param {Number} [config.leftMargin] Amount of pixels to extend span start on
   * @param {Number} [config.rightMargin] Amount of pixels to extend span end on
   * @param {Number} [config.adjustStart] Amount of units to extend span start on
   * @param {Number} [config.adjustEnd] Amount of units to extend span end on
   *
   * @returns {Number|null} level Current zoom level or null if it hasn't changed.
   * @category Zoom
   */
  zoomToSpan(config = {}) {
    if (config.leftMargin || config.rightMargin) {
      config.adjustStart = 0;
      config.adjustEnd = 0;
    }
    if (!config.leftMargin) config.leftMargin = 0;
    if (!config.rightMargin) config.rightMargin = 0;
    if (!config.startDate || !config.endDate) throw new Error('zoomToSpan: must provide startDate + endDate dates');
    const me = this,
      {
        timeAxis
      } = me,
      // this config enables old zoomToSpan behavior which we want to use for zoomToFit in Gantt
      needToAdjust = config.adjustStart >= 0 || config.adjustEnd >= 0;
    let {
      startDate,
      endDate
    } = config;
    if (needToAdjust) {
      startDate = DateHelper.add(startDate, -config.adjustStart, timeAxis.mainUnit);
      endDate = DateHelper.add(endDate, config.adjustEnd, timeAxis.mainUnit);
    }
    if (startDate <= endDate) {
      // get scheduling view width
      const {
          availableSpace
        } = me.timeAxisViewModel,
        presets = me.presets.allRecords,
        diffMS = endDate - startDate || 1;
      // if potential width of col is less than col width provided by zoom level
      //   - we'll zoom out panel until col width fit into width from zoom level
      // and if width of column is more than width from zoom level
      //   - we'll zoom in until col width fit won't fit into width from zoom level
      let currLevel = me.zoomLevel,
        inc,
        range;
      // if we zoomed out even more than the highest zoom level - limit it to the highest zoom level
      if (currLevel === -1) currLevel = 0;
      let msPerPixel = me.getMilliSecondsPerPixelForZoomLevel(presets[currLevel], true),
        // increment to get next zoom level:
        // -1 means that given timespan won't fit the available width in the current zoom level, we need to zoom out,
        // so that more content will "fit" into 1 px
        //
        // +1 mean that given timespan will already fit into available width in the current zoom level, but,
        // perhaps if we'll zoom in a bit more, the fitting will be better
        candidateLevel = currLevel + (inc = diffMS / msPerPixel + config.leftMargin + config.rightMargin > availableSpace ? -1 : 1),
        zoomLevel,
        levelToZoom = null;
      // loop over zoom levels
      while (candidateLevel >= 0 && candidateLevel <= presets.length - 1) {
        // get zoom level
        zoomLevel = presets[candidateLevel];
        msPerPixel = me.getMilliSecondsPerPixelForZoomLevel(zoomLevel, true);
        const spanWidth = diffMS / msPerPixel + config.leftMargin + config.rightMargin;
        // if zooming out
        if (inc === -1) {
          // if columns fit into available space, then all is fine, we've found appropriate zoom level
          if (spanWidth <= availableSpace) {
            levelToZoom = candidateLevel;
            // stop searching
            break;
          }
          // if zooming in
        } else {
          // if columns still fits into available space, we need to remember the candidate zoom level as a potential
          // resulting zoom level, the indication that we've found correct zoom level will be that timespan won't fit
          // into available view
          if (spanWidth <= availableSpace) {
            // if it's not currently active level
            if (currLevel !== candidateLevel - inc) {
              // remember this level as applicable
              levelToZoom = candidateLevel;
            }
          } else {
            // Sanity check to find the following case:
            // If we're already zoomed in at the appropriate level, but the current zoomLevel is "too small" to fit and had to be expanded,
            // there is an edge case where we should actually just stop and use the currently selected zoomLevel
            break;
          }
        }
        candidateLevel += inc;
      }
      // If we didn't find a large/small enough zoom level, use the lowest/highest level
      levelToZoom = levelToZoom != null ? levelToZoom : candidateLevel - inc;
      // presets is the array of all ViewPresets this Scheduler is using
      zoomLevel = presets[levelToZoom];
      const unitToZoom = zoomLevel.bottomHeader.unit;
      // Extract the correct msPerPixel value for the new zoom level
      msPerPixel = me.getMilliSecondsPerPixelForZoomLevel(zoomLevel, true);
      if (config.leftMargin || config.rightMargin) {
        // time axis doesn't yet know about new view preset (zoom level) so it cannot round/ceil date correctly
        startDate = new Date(startDate.getTime() - msPerPixel * config.leftMargin);
        endDate = new Date(endDate.getTime() + msPerPixel * config.rightMargin);
      }
      const tickCount = DateHelper.getDurationInUnit(startDate, endDate, unitToZoom, true) / zoomLevel.bottomHeader.increment;
      if (tickCount === 0) {
        return null;
      }
      const customWidth = Math.floor(availableSpace / tickCount),
        centerDate = config.centerDate || new Date((startDate.getTime() + endDate.getTime()) / 2);
      if (needToAdjust) {
        range = {
          startDate,
          endDate
        };
      } else {
        range = me.calculateOptimalDateRange(centerDate, availableSpace, zoomLevel);
      }
      let result = me.zoomLevel;
      // No change of zoom level needed, just move to the date range
      if (me.zoomLevel === levelToZoom) {
        timeAxis.reconfigure(range);
      } else {
        result = me.zoomToLevel(levelToZoom, Object.assign(range, {
          width: customWidth,
          centerDate
        }));
      }
      if (me.infiniteScroll) {
        me.scrollToDate(startDate, {
          block: 'start'
        });
      }
      return result;
    }
    return null;
  }
  /**
   * Zooms in the timeline according to the array of zoom levels. If the amount of levels to zoom is given, the view
   * will zoom in by this value. Otherwise, a value of `1` will be used.
   *
   * @param {Number} [levels] (optional) amount of levels to zoom in
   * @param {ChangePresetOptions} [options] Object containing options which affect how the new preset is applied.
   * @returns {Number|null} currentLevel New zoom level of the panel or null if level hasn't changed.
   * @category Zoom
   */
  zoomIn(levels = 1, options) {
    // Allow zoomIn({ visibleDate : ... })
    if (typeof levels === 'object') {
      options = levels;
      levels = 1;
    }
    const currentZoomLevelIndex = this.zoomLevel;
    if (currentZoomLevelIndex >= this.maxZoomLevel) {
      return null;
    }
    return this.zoomToLevel(currentZoomLevelIndex + levels, options);
  }
  /**
   * Zooms out the timeline according to the array of zoom levels. If the amount of levels to zoom is given, the view
   * will zoom out by this value. Otherwise, a value of `1` will be used.
   *
   * @param {Number} levels (optional) amount of levels to zoom out
   * @param {ChangePresetOptions} [options] Object containing options which affect how the new preset is applied.
   * @returns {Number|null} currentLevel New zoom level of the panel or null if level hasn't changed.
   * @category Zoom
   */
  zoomOut(levels = 1, options) {
    // Allow zoomOut({ visibleDate : ... })
    if (typeof levels === 'object') {
      options = levels;
      levels = 1;
    }
    const currentZoomLevelIndex = this.zoomLevel;
    if (currentZoomLevelIndex <= this.minZoomLevel) {
      return null;
    }
    return this.zoomToLevel(currentZoomLevelIndex - levels, options);
  }
  /**
   * Zooms in the timeline to the {@link #config-maxZoomLevel} according to the array of zoom levels.
   *
   * @param {ChangePresetOptions} [options] Object containing options which affect how the new preset is applied.
   * @returns {Number|null} currentLevel New zoom level of the panel or null if level hasn't changed.
   * @category Zoom
   */
  zoomInFull(options) {
    return this.zoomToLevel(this.maxZoomLevel, options);
  }
  /**
   * Zooms out the timeline to the {@link #config-minZoomLevel} according to the array of zoom levels.
   *
   * @param {ChangePresetOptions} [options] Object containing options which affect how the new preset is applied.
   * @returns {Number|null} currentLevel New zoom level of the panel or null if level hasn't changed.
   * @category Zoom
   */
  zoomOutFull(options) {
    return this.zoomToLevel(this.minZoomLevel, options);
  }
  /*
   * Adjusts the timespan of the panel to the new zoom level. Used for performance reasons,
   * as rendering too many columns takes noticeable amount of time so their number is limited.
   * @category Zoom
   * @private
   */
  calculateOptimalDateRange(centerDate, viewportSize, viewPreset, userProvidedSpan) {
    // this line allows us to always use the `calculateOptimalDateRange` method when calculating date range for zooming
    // (even in case when user has provided own interval)
    // other methods may override/hook into `calculateOptimalDateRange` to insert own processing
    // (infinite scrolling feature does)
    if (userProvidedSpan) return userProvidedSpan;
    const me = this,
      {
        timeAxis
      } = me,
      {
        bottomHeader
      } = viewPreset,
      tickWidth = me.isHorizontal ? viewPreset.tickWidth : viewPreset.tickHeight;
    if (me.zoomKeepsOriginalTimespan) {
      return {
        startDate: timeAxis.startDate,
        endDate: timeAxis.endDate
      };
    }
    const unit = bottomHeader.unit,
      difference = Math.ceil(viewportSize / tickWidth * bottomHeader.increment * me.visibleZoomFactor / 2),
      startDate = DateHelper.add(centerDate, -difference, unit),
      endDate = DateHelper.add(centerDate, difference, unit);
    if (me.infiniteScroll) {
      return me.timeAxisViewModel.calculateInfiniteScrollingDateRange(centerDate, true);
    } else {
      return {
        startDate: timeAxis.floorDate(startDate, false, unit, bottomHeader.increment),
        endDate: timeAxis.ceilDate(endDate, false, unit, bottomHeader.increment)
      };
    }
  }
  onElementMouseMove(event) {
    const {
      isHorizontal,
      zoomContext
    } = this;
    super.onElementMouseMove(event);
    if (event.isTrusted && zoomContext) {
      // Invalidate the zoomContext if mouse has strayed away from it
      if (Math.abs(event[`client${isHorizontal ? 'X' : 'Y'}`] - zoomContext.coordinate) > 10) {
        this.zoomContext = null;
      }
    }
  }
  async onWheel(event) {
    if (event.ctrlKey && !this.forceFit) {
      event.preventDefault();
      const me = this,
        {
          zoomContext,
          isHorizontal,
          timelineScroller,
          zoomLevel
        } = me,
        now = performance.now(),
        coordinate = event[`client${isHorizontal ? 'X' : 'Y'}`];
      let zoomPosition = coordinate - timelineScroller.viewport[`${isHorizontal ? 'x' : 'y'}`];
      // zoomPosition is the offset into the TimeAxisSubGridElement.
      if (isHorizontal && me.rtl) {
        zoomPosition = timelineScroller.viewport.width + timelineScroller.viewport.x - coordinate;
      }
      // If we are in a fast-arriving stream of wheel events, we use the same zoomDate as last time.
      // If it's a new zoom gesture or the pointer has strayed away from the context then ascertain
      // the gesture's center date
      if (now - me.lastWheelTime > 200 || !zoomContext || Math.abs(coordinate - me.zoomContext.coordinate) > 20) {
        // We're creating a zoom gesture which lasts as long as the
        // wheel events keep arriving at the same timeline position
        me.zoomContext = {
          // So we can track if we're going in (to finer resolutions)
          zoomLevel,
          // Pointer client(X|Y)
          coordinate,
          // Full TimeAxis offset position at which to place the date
          zoomPosition,
          // The date to place at the position
          zoomDate: me.getDateFromDomEvent(event)
        };
      }
      // Use the current zoomContext's zoomDate, but at each level, the relative position of that date
      // in the TimeAxis has to be corrected as the TimeAxis grows and scrolls to keep the zoomPosition
      // stable.
      else {
        // If we zoom in to a finer resolution, get a more accurate centering date.
        // If gesture was started at a years/months level, the date will be inaccurate.
        if (zoomLevel > zoomContext.zoomLevel) {
          zoomContext.zoomDate = me.getDateFromDomEvent(event);
          zoomContext.zoomLevel = zoomLevel;
        }
        zoomContext.zoomPosition = zoomPosition;
      }
      me.lastWheelTime = now;
      me[`zoom${event.deltaY > 0 ? 'Out' : 'In'}`](undefined, me.zoomContext);
    }
  }
  /**
   * Changes the time axis timespan to the supplied start and end dates.
   * @param {Date} startDate The new start date
   * @param {Date} [endDate] The new end date. If omitted or equal to startDate, the
   * {@link Scheduler.preset.ViewPreset#field-defaultSpan} property of the current view preset will be used to
   * calculate the new end date.
   */
  setTimeSpan(startDate, endDate) {
    this.timeAxis.setTimeSpan(startDate, endDate);
  }
  /**
   * Moves the time axis by the passed amount and unit.
   *
   * NOTE: If using a filtered time axis, see {@link Scheduler.data.TimeAxis#function-shift} for more information.
   *
   * @param {Number} amount The number of units to jump
   * @param {'ms'|'s'|'m'|'h'|'d'|'w'|'M'|'y'} [unit] The unit (Day, Week etc)
   */
  shift(amount, unit) {
    this.timeAxis.shift(amount, unit);
  }
  /**
   * Moves the time axis forward in time in units specified by the view preset `shiftUnit`, and by the amount
   * specified by the `shiftIncrement` config of the current view preset.
   *
   * NOTE: If using a filtered time axis, see {@link Scheduler.data.TimeAxis#function-shiftNext} for more information.
   *
   * @param {Number} [amount] The number of units to jump forward
   */
  shiftNext(amount) {
    this.timeAxis.shiftNext(amount);
  }
  /**
   * Moves the time axis backward in time in units specified by the view preset `shiftUnit`, and by the amount
   * specified by the `shiftIncrement` config of the current view preset.
   *
   * NOTE: If using a filtered time axis, see {@link Scheduler.data.TimeAxis#function-shiftPrevious} for more
   * information.
   *
   * @param {Number} [amount] The number of units to jump backward
   */
  shiftPrevious(amount) {
    this.timeAxis.shiftPrevious(amount);
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/recurrence/RecurrenceConfirmationPopup
 */
/**
 * A confirmation dialog shown when modifying a recurring event or some of its occurrences.
 * For recurring events, the dialog informs the user that the change will be applied to all occurrences.
 *
 * For occurrences, the dialog lets the user choose if the change should affect all future occurrences,
 * or this occurrence only.
 *
 * Usage example:
 *
 * ```javascript
 * const confirmation = new RecurrenceConfirmationPopup();
 *
 * confirmation.confirm({
 *     eventRecord : recurringEvent,
 *     actionType  : "delete",
 *     changerFn   : () => recurringEvent.remove(event)
 * });
 * ```
 *
 * @classType recurrenceconfirmation
 * @extends Core/widget/Popup
 */
class RecurrenceConfirmationPopup extends Popup {
  static get $name() {
    return 'RecurrenceConfirmationPopup';
  }
  // Factoryable type name
  static get type() {
    return 'recurrenceconfirmation';
  }
  static get defaultConfig() {
    return {
      localizableProperties: [],
      align: 'b-t',
      autoShow: false,
      autoClose: false,
      closeAction: 'onRecurrenceClose',
      modal: true,
      centered: true,
      scrollAction: 'realign',
      constrainTo: globalThis,
      draggable: true,
      closable: true,
      floating: true,
      eventRecord: null,
      cls: 'b-sch-recurrenceconfirmation',
      bbar: {
        defaults: {
          localeClass: this
        },
        items: {
          changeSingleButton: {
            weight: 100,
            cls: 'b-raised',
            color: 'b-blue',
            text: 'L{update-only-this-btn-text}',
            onClick: 'up.onChangeSingleButtonClick'
          },
          changeMultipleButton: {
            weight: 200,
            color: 'b-green',
            text: 'L{Object.Yes}',
            onClick: 'up.onChangeMultipleButtonClick'
          },
          cancelButton: {
            weight: 300,
            color: 'b-gray',
            text: 'L{Object.Cancel}',
            onClick: 'up.onCancelButtonClick'
          }
        }
      }
    };
  }
  /**
   * Reference to the "Apply changes to multiple occurrences" button, if used
   * @property {Core.widget.Button}
   * @readonly
   */
  get changeMultipleButton() {
    return this.widgetMap.changeMultipleButton;
  }
  /**
   * Reference to the button that causes changing of the event itself only, if used
   * @property {Core.widget.Button}
   * @readonly
   */
  get changeSingleButton() {
    return this.widgetMap.changeSingleButton;
  }
  /**
   * Reference to the cancel button, if used
   * @property {Core.widget.Button}
   * @readonly
   */
  get cancelButton() {
    return this.widgetMap.cancelButton;
  }
  /**
   * Handler for "Apply changes to multiple occurrences" {@link #property-changeMultipleButton button}.
   * It calls {@link #function-processMultipleRecords} and then hides the dialog.
   */
  onChangeMultipleButtonClick() {
    this.processMultipleRecords();
    this.hide();
  }
  /**
   * Handler for the {@link #property-changeSingleButton button} that causes changing of the event itself only.
   * It calls {@link #function-processSingleRecord} and then hides the dialog.
   */
  onChangeSingleButtonClick() {
    this.processSingleRecord();
    this.hide();
  }
  /**
   * Handler for {@link #property-cancelButton cancel button}.
   * It calls `cancelFn` provided to {@link #function-confirm} call and then hides the dialog.
   */
  onCancelButtonClick() {
    this.cancelFn && this.cancelFn.call(this.thisObj);
    this.hide();
  }
  onRecurrenceClose() {
    if (this.cancelFn) {
      this.cancelFn.call(this.thisObj);
    }
    this.hide();
  }
  /**
   * Displays the confirmation.
   * Usage example:
   *
   * ```javascript
   * const popup = new RecurrenceConfirmationPopup();
   *
   * popup.confirm({
   *     eventRecord,
   *     actionType : "delete",
   *     changerFn  : () => eventStore.remove(record)
   * });
   * ```
   *
   * @param {Object} config The following config options are supported:
   * @param {Scheduler.model.EventModel} config.eventRecord   Event being modified.
   * @param {'update'|'delete'} config.actionType Type of modification to be applied to the event. Can be
   * either "update" or "delete".
   * @param {Function} config.changerFn A function that should be called to apply the change to the event upon user
   * choice.
   * @param {Function} [config.thisObj] `changerFn` and `cancelFn` functions scope.
   * @param {Function} [config.cancelFn] Function called on `Cancel` button click.
   */
  confirm(config = {}) {
    const me = this;
    ['actionType', 'eventRecord', 'title', 'html', 'changerFn', 'cancelFn', 'finalizerFn', 'thisObj'].forEach(prop => {
      if (prop in config) me[prop] = config[prop];
    });
    me.updatePopupContent();
    return super.show(config);
  }
  updatePopupContent() {
    const me = this,
      {
        changeMultipleButton,
        changeSingleButton,
        cancelButton
      } = me.widgetMap,
      {
        eventRecord,
        actionType = 'update'
      } = me,
      isMaster = eventRecord === null || eventRecord === void 0 ? void 0 : eventRecord.isRecurring;
    if (isMaster) {
      changeMultipleButton.text = me.L('L{Object.Yes}');
      me.html = me.L(`${actionType}-all-message`);
    } else {
      changeMultipleButton.text = me.L(`${actionType}-further-btn-text`);
      me.html = me.L(`${actionType}-further-message`);
    }
    changeSingleButton.text = me.L(`${actionType}-only-this-btn-text`);
    cancelButton.text = me.L('L{Object.Cancel}');
    me.width = me.L('L{width}');
    me.title = me.L(`${actionType}-title`);
  }
  /**
   * Applies changes to multiple occurrences as reaction on "Apply changes to multiple occurrences"
   * {@link #property-changeMultipleButton button} click.
   */
  processMultipleRecords() {
    const {
      eventRecord,
      changerFn,
      thisObj,
      finalizerFn
    } = this;
    eventRecord.beginBatch();
    // Apply changes to the occurrence.
    // It is not joined to any stores, so this has no consequence.
    changerFn && this.callback(changerFn, thisObj, [eventRecord]);
    // afterChange will promote it to being an new recurring base because there's still recurrence
    eventRecord.endBatch();
    finalizerFn && this.callback(finalizerFn, thisObj, [eventRecord]);
  }
  /**
   * Applies changes to a single record by making it a "real" event and adding an exception to the recurrence.
   * The method is called as reaction on clicking the {@link #property-changeSingleButton button} that causes changing of the event itself only.
   */
  processSingleRecord() {
    var _firstOccurrence;
    const {
      eventRecord,
      changerFn,
      thisObj,
      finalizerFn
    } = this;
    eventRecord.beginBatch();
    let firstOccurrence;
    // If that's a master event get its very first occurrence
    if (eventRecord !== null && eventRecord !== void 0 && eventRecord.isRecurring) {
      eventRecord.recurrence.forEachOccurrence(eventRecord.startDate, null, (occurrence, isFirst, index) => {
        // index 1 is used by to the event itself, > 1 since there might be exceptions
        if (index > 1) {
          firstOccurrence = occurrence;
          return false;
        }
      });
    }
    // turn the 1st occurrence into a new "master" event
    (_firstOccurrence = firstOccurrence) === null || _firstOccurrence === void 0 ? void 0 : _firstOccurrence.convertToRealEvent();
    // When the changes apply, because there's no recurrence, it will become an exception
    eventRecord.recurrence = null;
    // Apply changes to the occurrence.
    // It is not joined to any stores, so this has no consequence.
    changerFn && this.callback(changerFn, thisObj, [eventRecord]);
    // Must also change after the callback in case the callback sets the rule.
    // This will update the batch update data block to prevent it being set back to recurring.
    eventRecord.recurrenceRule = null;
    // afterChange will promote it to being an exception because there's no recurrence
    eventRecord.endBatch();
    finalizerFn && this.callback(finalizerFn, thisObj, [eventRecord]);
  }
  updateLocalization() {
    this.updatePopupContent();
    super.updateLocalization();
  }
}
// Register this widget type with its Factory
RecurrenceConfirmationPopup.initClass();
RecurrenceConfirmationPopup._$name = 'RecurrenceConfirmationPopup';

/**
 * @module Scheduler/view/mixin/RecurringEvents
 */
/**
 * A mixin that adds recurring events functionality to the Scheduler.
 *
 * The main purpose of the code in here is displaying a {@link Scheduler.view.recurrence.RecurrenceConfirmationPopup special confirmation}
 * on user mouse dragging/resizing/deleting recurring events and their occurrences.
 *
 * @mixin
 */
var RecurringEvents = (Target => class RecurringEvents extends (Target || Base) {
  static $name = 'RecurringEvents';
  static configurable = {
    /**
     * Enables showing occurrences of recurring events across the scheduler's time axis.
     *
     * Enables extra recurrence UI fields in the system-provided event editor (not in Scheduler Pro's task editor).
     * @config {Boolean}
     * @default
     * @category Scheduled events
     */
    enableRecurringEvents: false,
    recurrenceConfirmationPopup: {
      $config: ['lazy'],
      value: {
        type: 'recurrenceconfirmation'
      }
    }
  };
  construct(config) {
    super.construct(config);
    this.ion({
      beforeEventDropFinalize: 'onRecurrableBeforeEventDropFinalize',
      beforeEventResizeFinalize: 'onRecurrableBeforeEventResizeFinalize',
      beforeAssignmentDelete: 'onRecurrableAssignmentBeforeDelete'
    });
  }
  changeRecurrenceConfirmationPopup(recurrenceConfirmationPopup, oldRecurrenceConfirmationPopup) {
    // Widget.reconfigure reither reconfigures an existing instance, or creates a new one, or,
    // if the configuration is null, destroys the existing instance.
    const result = this.constructor.reconfigure(oldRecurrenceConfirmationPopup, recurrenceConfirmationPopup, 'recurrenceconfirmation');
    result.owner = this;
    return result;
  }
  findRecurringEventToConfirmDelete(eventRecords) {
    // show confirmation if we deal with at least one recurring event (or its occurrence)
    // and if the record is not being edited by event editor (since event editor has its own confirmation)
    return eventRecords.find(eventRecord => eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence));
  }
  onRecurrableAssignmentBeforeDelete({
    assignmentRecords,
    context
  }) {
    const eventRecords = assignmentRecords.map(as => as.event),
      eventRecord = this.findRecurringEventToConfirmDelete(eventRecords);
    if (this.enableRecurringEvents && eventRecord) {
      this.recurrenceConfirmationPopup.confirm({
        actionType: 'delete',
        eventRecord,
        changerFn() {
          context.finalize(true);
        },
        cancelFn() {
          context.finalize(false);
        }
      });
      return false;
    }
  }
  onRecurrableBeforeEventDropFinalize({
    context
  }) {
    if (this.enableRecurringEvents) {
      const {
          eventRecords
        } = context,
        recurringEvents = eventRecords.filter(eventRecord => eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence));
      if (recurringEvents.length) {
        context.async = true;
        this.recurrenceConfirmationPopup.confirm({
          actionType: 'update',
          eventRecord: recurringEvents[0],
          changerFn() {
            context.finalize(true);
          },
          cancelFn() {
            context.finalize(false);
          }
        });
      }
    }
  }
  onRecurrableBeforeEventResizeFinalize({
    context
  }) {
    if (this.enableRecurringEvents) {
      const {
          eventRecord
        } = context,
        isRecurring = eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence);
      if (isRecurring) {
        context.async = true;
        this.recurrenceConfirmationPopup.confirm({
          actionType: 'update',
          eventRecord,
          changerFn() {
            context.finalize(true);
          },
          cancelFn() {
            context.finalize(false);
          }
        });
      }
    }
  }
  // Make sure occurrence cache is up-to-date when reassigning events
  onAssignmentChange({
    action,
    records: assignments
  }) {
    if (action !== 'dataset' && Array.isArray(assignments)) {
      for (const assignment of assignments) {
        var _assignment$event;
        if ((_assignment$event = assignment.event) !== null && _assignment$event !== void 0 && _assignment$event.isRecurring && !assignment.event.isBatchUpdating) {
          assignment.event.removeOccurrences();
        }
      }
    }
  }
  /**
   * Returns occurrences of the provided recurring event across the date range of this Scheduler.
   * @param  {Scheduler.model.TimeSpan} recurringEvent Recurring event for which occurrences should be retrieved.
   * @returns {Scheduler.model.TimeSpan[]} Array of the provided timespans occurrences.
   *
   * __Empty if the passed event is not recurring, or has no occurrences in the date range.__
   *
   * __If the date range encompasses the start point, the recurring event itself will be the first entry.__
   * @category Data
   */
  getOccurrencesFor(recurringEvent) {
    return this.eventStore.getOccurrencesForTimeSpan(recurringEvent, this.timeAxis.startDate, this.timeAxis.endDate);
  }
  /**
   * Internal utility function to remove events. Used when pressing [DELETE] or [BACKSPACE] or when clicking the
   * delete button in the event editor. Triggers a preventable `beforeEventDelete` or `beforeAssignmentDelete` event.
   * @param {Scheduler.model.EventModel[]|Scheduler.model.AssignmentModel[]} eventRecords Records to remove
   * @param {Function} [callback] Optional callback executed after triggering the event but before deletion
   * @returns {Boolean} Returns `false` if the operation was prevented, otherwise `true`
   * @internal
   * @fires beforeEventDelete
   * @fires beforeAssignmentDelete
   */
  async removeEvents(eventRecords, callback = null, popupOwner = this) {
    const me = this;
    if (!me.readOnly && eventRecords.length) {
      const context = {
        finalize(removeRecord = true) {
          if (callback) {
            callback(removeRecord);
          }
          if (removeRecord !== false) {
            if (eventRecords.some(record => {
              var _record$event;
              return record.isOccurrence || ((_record$event = record.event) === null || _record$event === void 0 ? void 0 : _record$event.isOccurrence);
            })) {
              eventRecords.forEach(record => record.isOccurrenceAssignment ? record.event.remove() : record.remove());
            } else {
              const store = eventRecords[0].isAssignment ? me.assignmentStore : me.eventStore;
              store.remove(eventRecords);
            }
          }
        }
      };
      let shouldFinalize;
      if (eventRecords[0].isAssignment) {
        /**
         * Fires before an assignment is removed. Can be triggered by user pressing [DELETE] or [BACKSPACE] or
         * by the event editor. Can for example be used to display a custom dialog to confirm deletion, in which
         * case records should be "manually" removed after confirmation:
         *
         * ```javascript
         * scheduler.on({
         *    beforeAssignmentDelete({ assignmentRecords, context }) {
         *        // Show custom confirmation dialog (pseudo code)
         *        confirm.show({
         *            listeners : {
         *                onOk() {
         *                    // Remove the assignments on confirmation
         *                    context.finalize(true);
         *                },
         *                onCancel() {
         *                    // do not remove the assignments if "Cancel" clicked
         *                    context.finalize(false);
         *                }
         *            }
         *        });
         *
         *        // Prevent default behaviour
         *        return false;
         *    }
         * });
         * ```
         *
         * @event beforeAssignmentDelete
         * @param {Scheduler.view.Scheduler} source  The Scheduler instance
         * @param {Scheduler.model.EventModel[]} eventRecords  The records about to be deleted
         * @param {Object} context  Additional removal context:
         * @param {Function} context.finalize  Function to call to finalize the removal.
         *      Used to asynchronously decide to remove the records or not. Provide `false` to the function to
         *      prevent the removal.
         * @param {Boolean} [context.finalize.removeRecords = true]   Provide `false` to the function to prevent
         *      the removal.
         * @preventable
         */
        shouldFinalize = me.trigger('beforeAssignmentDelete', {
          assignmentRecords: eventRecords,
          context
        });
      } else {
        /**
         * Fires before an event is removed. Can be triggered by user pressing [DELETE] or [BACKSPACE] or by the
         * event editor. Return `false` to immediately veto the removal (or a `Promise` yielding `true` or `false`
         * for async vetoing).
         *
         * Can for example be used to display a custom dialog to confirm deletion, in which case
         * records should be "manually" removed after confirmation:
         *
         * ```javascript
         * scheduler.on({
         *    beforeEventDelete({ eventRecords, context }) {
         *        // Show custom confirmation dialog (pseudo code)
         *        confirm.show({
         *            listeners : {
         *                onOk() {
         *                    // Remove the events on confirmation
         *                    context.finalize(true);
         *                },
         *                onCancel() {
         *                    // do not remove the events if "Cancel" clicked
         *                    context.finalize(false);
         *                }
         *            }
         *        });
         *
         *        // Prevent default behaviour
         *        return false;
         *    }
         * });
         * ```
         *
         * @event beforeEventDelete
         * @param {Scheduler.view.Scheduler} source  The Scheduler instance
         * @param {Scheduler.model.EventModel[]} eventRecords  The records about to be deleted
         * @param {Object} context  Additional removal context:
         * @param {Function} context.finalize  Function to call to finalize the removal.
         *      Used to asynchronously decide to remove the records or not. Provide `false` to the function to
         *      prevent the removal.
         * @param {Boolean} [context.finalize.removeRecords = true]  Provide `false` to the function to prevent
         *      the removal.
         * @preventable
         * @async
         */
        shouldFinalize = await me.trigger('beforeEventDelete', {
          eventRecords,
          context
        });
      }
      if (shouldFinalize !== false) {
        const recurringEventRecord = eventRecords.find(eventRecord => eventRecord.isRecurring || eventRecord.isOccurrence);
        if (recurringEventRecord) {
          me.recurrenceConfirmationPopup.owner = popupOwner;
          me.recurrenceConfirmationPopup.confirm({
            actionType: 'delete',
            eventRecord: recurringEventRecord,
            changerFn() {
              context.finalize(true);
            },
            cancelFn() {
              context.finalize(false);
            }
          });
        } else {
          context.finalize(true);
        }
        return true;
      }
    }
    return false;
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineEventRendering
 */
/**
 * Functions to handle event rendering (EventModel -> dom elements).
 *
 * @mixin
 */
var TimelineEventRendering = (Target => class TimelineEventRendering extends (Target || Base) {
  static get $name() {
    return 'TimelineEventRendering';
  }
  //region Default config
  static get defaultConfig() {
    return {
      /**
       * When `true`, events are sized and positioned based on rowHeight, resourceMargin and barMargin settings.
       * Set this to `false` if you want to control height and vertical position using CSS instead.
       *
       * Note that events always get an absolute top position, but when this setting is enabled that position
       * will match row's top. To offset within the row using CSS, use `transform : translateY(y)`.
       *
       * @config {Boolean}
       * @default
       * @category Scheduled events
       */
      managedEventSizing: true,
      /**
       * The CSS class added to an event/assignment when it is newly created
       * in the UI and unsynced with the server.
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      generatedIdCls: 'b-sch-dirty-new',
      /**
       * The CSS class added to an event when it has unsaved modifications
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      dirtyCls: 'b-sch-dirty',
      /**
       * The CSS class added to an event when it is currently committing changes
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      committingCls: 'b-sch-committing',
      /**
       * The CSS class added to an event/assignment when it ends outside of the visible time range.
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      endsOutsideViewCls: 'b-sch-event-endsoutside',
      /**
       * The CSS class added to an event/assignment when it starts outside of the visible time range.
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      startsOutsideViewCls: 'b-sch-event-startsoutside',
      /**
       * The CSS class added to an event/assignment when it is not draggable.
       * @config {String}
       * @default
       * @private
       * @category CSS
       */
      fixedEventCls: 'b-sch-event-fixed'
    };
  }
  static configurable = {
    /**
     * Controls how much space to leave between stacked event bars in px.
     *
     * Value will be constrained by half the row height in horizontal mode.
     *
     * @prp {Number}
     * @default
     * @category Scheduled events
     */
    barMargin: 10,
    /**
     * Specify `true` to force rendered events/tasks to fill entire ticks. This only affects rendering, start
     * and end dates retain their value on the data level.
     *
     * When enabling `fillTicks` you should consider either disabling EventDrag/TaskDrag and EventResize/TaskResize,
     * or enabling {@link Scheduler/view/mixin/TimelineDateMapper#config-snap}. Otherwise their behaviour might not
     * be what a user expects.
     *
     * @prp {Boolean}
     * @default
     * @category Scheduled events
     */
    fillTicks: false,
    resourceMargin: null,
    /**
     * Event color used by default. Events and resources can specify their own color, with priority order being:
     * Event -> Resource -> Scheduler default.
     *
     * Specify `null` to not apply a default color and take control using custom CSS (an easily overridden color
     * will be used to make sure events are still visible).
     *
     * For available standard colors, see {@link Scheduler.model.mixin.EventModelMixin#typedef-EventColor}.
     *
     * @prp {EventColor} eventColor
     * @category Scheduled events
     */
    eventColor: 'green',
    /**
     * Event style used by default. Events and resources can specify their own style, with priority order being:
     * Event -> Resource -> Scheduler default. Determines the appearance of the event by assigning a CSS class
     * to it. Available styles are:
     *
     * * `'plain'` (default) - flat look
     * * `'border'` - has border in darker shade of events color
     * * `'colored'` - has colored text and wide left border in same color
     * * `'hollow'` - only border + text until hovered
     * * `'line'` - as a line with the text below it
     * * `'dashed'` - as a dashed line with the text below it
     * * `'minimal'` - as a thin line with small text above it
     * * `'rounded'` - minimalistic style with rounded corners
     * * `null` - do not apply a default style and take control using custom CSS (easily overridden basic styling will be used).
     *
     * In addition, there are two styles intended to be used when integrating with Bryntum Calendar. To match
     * the look of Calendar events, you can use:
     *
     * * `'calendar'` - a variation of the "colored" style matching the default style used by Calendar
     * * `'interday'` - a variation of the "plain" style, for interday events
     *
     * @prp {'plain'|'border'|'colored'|'hollow'|'line'|'dashed'|'minimal'|'rounded'|'calendar'|'interday'|null}
     * @default
     * @category Scheduled events
     */
    eventStyle: 'plain',
    /**
     * The width/height (depending on vertical / horizontal mode) of all the time columns.
     *
     * There is a limit for the tick size value. Its minimal allowed value is calculated so ticks would fit the
     * available space. Only applicable when {@link Scheduler.view.TimelineBase#config-forceFit} is set to
     * `false`. To set `tickSize` freely skipping that limitation please set
     * {@link Scheduler.view.TimelineBase#config-suppressFit} to `true`.
     *
     * @prp {Number}
     * @category Scheduled events
     */
    tickSize: null
  };
  //endregion
  //region Settings
  updateFillTicks(fillTicks) {
    if (!this.isConfiguring) {
      this.timeAxis.forceFullTicks = fillTicks && this.snap;
      this.refreshWithTransition();
      this.trigger('stateChange');
    }
  }
  changeBarMargin(margin) {
    ObjectHelper.assertNumber(margin, 'barMargin');
    // bar margin should not exceed half of the row height
    if (this.isHorizontal && this.rowHeight) {
      return Math.min(Math.ceil(this.rowHeight / 2), margin);
    }
    return margin;
  }
  updateBarMargin() {
    if (this.rendered) {
      this.currentOrientation.onBeforeRowHeightChange();
      this.refreshWithTransition();
      this.trigger('stateChange');
    }
  }
  // Documented in SchedulerEventRendering to not show up in Gantt docs
  get resourceMargin() {
    return this._resourceMargin == null ? this.barMargin : this._resourceMargin;
  }
  changeResourceMargin(margin) {
    ObjectHelper.assertNumber(margin, 'resourceMargin');
    // resource margin should not exceed half of the row height
    if (this.isHorizontal && this.rowHeight) {
      return Math.min(Math.ceil(this.rowHeight / 2), margin);
    }
    return margin;
  }
  updateResourceMargin() {
    if (this.rendered) {
      this.currentOrientation.onBeforeRowHeightChange();
      this.refreshWithTransition();
    }
  }
  changeTickSize(width) {
    ObjectHelper.assertNumber(width, 'tickSize');
    return width;
  }
  updateTickSize(width) {
    this.timeAxisViewModel.tickSize = width;
  }
  get tickSize() {
    return this.timeAxisViewModel.tickSize;
  }
  /**
   * Predefined event colors, useful in combos etc.
   * @type {String[]}
   * @category Scheduled events
   */
  static get eventColors() {
    // These are the colors available by default for Scheduler and Gantt
    // They classes are located in eventstyles.scss
    return ['red', 'pink', 'purple', 'magenta', 'violet', 'indigo', 'blue', 'cyan', 'teal', 'green', 'gantt-green', 'lime', 'yellow', 'orange', 'deep-orange', 'gray', 'light-gray'];
  }
  /**
   * Predefined event styles , useful in combos etc.
   * @type {String[]}
   * @category Scheduled events
   */
  static get eventStyles() {
    return ['plain', 'border', 'hollow', 'colored', 'line', 'dashed', 'minimal', 'rounded'];
  }
  updateEventStyle(style) {
    if (!this.isConfiguring) {
      this.refreshWithTransition();
      this.trigger('stateChange');
    }
  }
  updateEventColor(color) {
    if (!this.isConfiguring) {
      this.refreshWithTransition();
      this.trigger('stateChange');
    }
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineScroll
 */
const maintainVisibleStart = {
    maintainVisibleStart: true
  },
  defaultScrollOptions$1 = {
    block: 'nearest'
  };
/**
 * Functions for scrolling to events, dates etc.
 *
 * @mixin
 */
var TimelineScroll = (Target => class TimelineScroll extends (Target || Base) {
  static get $name() {
    return 'TimelineScroll';
  }
  static get configurable() {
    return {
      /**
       * This config defines the size of the start and end invisible parts of the timespan when {@link #config-infiniteScroll} set to `true`.
       *
       * It should be provided as a coefficient, which will be multiplied by the size of the scheduling area.
       *
       * For example, if `bufferCoef` is `5` and the panel view width is 200px then the timespan will be calculated to
       * have approximately 1000px (`5 * 200`) to the left and 1000px to the right of the visible area, resulting
       * in 2200px of totally rendered content.
       *
       * @config {Number}
       * @category Infinite scroll
       * @default
       */
      bufferCoef: 5,
      /**
       * This config defines the scroll limit, which, when exceeded will cause a timespan shift.
       * The limit is calculated as the `panelWidth * {@link #config-bufferCoef} * bufferThreshold`. During scrolling, if the left or right side
       * has less than that of the rendered content - a shift is triggered.
       *
       * For example if `bufferCoef` is `5` and the panel view width is 200px and `bufferThreshold` is 0.2, then the timespan
       * will be shifted when the left or right side has less than 200px (5 * 200 * 0.2) of content.
       * @config {Number}
       * @category Infinite scroll
       * @default
       */
      bufferThreshold: 0.2,
      /**
       * Configure as `true` to automatically adjust the panel timespan during scrolling in the time dimension,
       * when the scroller comes close to the start/end edges.
       *
       * The actually rendered timespan in this mode (and thus the amount of HTML in the DOM) is calculated based
       * on the {@link #config-bufferCoef} option, and is thus not controlled by the {@link Scheduler/view/TimelineBase#config-startDate}
       * and {@link Scheduler/view/TimelineBase#config-endDate} configs. The moment when the timespan shift
       * happens is determined by the {@link #config-bufferThreshold} value.
       *
       * To specify initial point in time to view, supply the
       * {@link Scheduler/view/TimelineBase#config-visibleDate} config.
       *
       * @config {Boolean} infiniteScroll
       * @category Infinite scroll
       * @default
       */
      infiniteScroll: false
    };
  }
  initScroll() {
    const me = this,
      {
        isHorizontal,
        visibleDate
      } = me;
    super.initScroll();
    const {
      scrollable
    } = isHorizontal ? me.timeAxisSubGrid : me;
    scrollable.ion({
      scroll: 'onTimelineScroll',
      thisObj: me
    });
    // Ensure the TimeAxis starts life at the correct size with buffer zones
    // outside the visible window.
    if (me.infiniteScroll) {
      const setTimeSpanOptions = visibleDate ? {
          ...visibleDate,
          visibleDate: visibleDate.date
        } : {
          visibleDate: me.viewportCenterDate,
          block: 'center'
        },
        {
          startDate,
          endDate
        } = me.timeAxisViewModel.calculateInfiniteScrollingDateRange(setTimeSpanOptions.visibleDate, setTimeSpanOptions.block === 'center');
      // Don't ask to maintain visible start - we're initializing - there's no visible start yet.
      // If there's a visibleDate set, it will execute its scroll on paint.
      me.setTimeSpan(startDate, endDate, setTimeSpanOptions);
    }
  }
  /**
   * A {@link Core.helper.util.Scroller} which scrolls the time axis in whatever {@link Scheduler.view.Scheduler#config-mode} the
   * Scheduler is configured, either `horiontal` or `vertical`.
   *
   * The width and height dimensions are replaced by `size`. So this will expose the following properties:
   *
   *    - `clientSize` The size of the time axis viewport.
   *    - `scrollSize` The full scroll size of the time axis viewport
   *    - `position` The position scrolled to along the time axis viewport
   *
   * @property {Core.helper.util.Scroller}
   * @readonly
   * @category Scrolling
   */
  get timelineScroller() {
    const me = this;
    if (!me.scrollInitialized) {
      me.initScroll();
    }
    return me._timelineScroller || (me._timelineScroller = new TimelineScroller({
      widget: me,
      scrollable: me.isHorizontal ? me.timeAxisSubGrid.scrollable : me.scrollable,
      isHorizontal: me.isHorizontal
    }));
  }
  doDestroy() {
    var _this$_timelineScroll;
    (_this$_timelineScroll = this._timelineScroller) === null || _this$_timelineScroll === void 0 ? void 0 : _this$_timelineScroll.destroy();
    super.doDestroy();
  }
  onTimelineScroll({
    source
  }) {
    // On scroll, check if we are nearing the end to see if the sliding window needs moving.
    // onSchedulerHorizontalScroll is delayed to animationFrame
    if (this.infiniteScroll) {
      this.checkTimeAxisScroll(source[this.isHorizontal ? 'x' : 'y']);
    }
  }
  checkTimeAxisScroll(scrollPos) {
    const me = this,
      scrollable = me.timelineScroller,
      {
        clientSize
      } = scrollable,
      requiredSize = clientSize * me.bufferCoef,
      limit = requiredSize * me.bufferThreshold,
      maxScroll = scrollable.maxPosition,
      {
        style
      } = me.timeAxisSubGrid.virtualScrollerElement;
    // if scroll violates limits let's shift timespan
    if (maxScroll - scrollPos < limit || scrollPos < limit) {
      // If they were dragging the thumb, this must be a one-time thing. They *must* lose contact
      // with the thumb when the window shift occurs and the thumb zooms back to the center.
      // Changing for a short time to overflow:hidden terminates the thumb drag.
      // They can start again from the center, the reset happens very quickly.
      style.overflow = 'hidden';
      style.pointerEvents = 'none';
      // Avoid content height changing when scrollbar disappears
      style.paddingBottom = `${DomHelper.scrollBarWidth}px`;
      me.setTimeout(() => {
        style.overflow = '';
        style.paddingBottom = '';
        style.pointerEvents = '';
      }, 100);
      me.shiftToDate(me.getDateFromCoordinate(scrollPos, null, true, false, true));
    }
  }
  shiftToDate(date, centered) {
    const newRange = this.timeAxisViewModel.calculateInfiniteScrollingDateRange(date, centered);
    // this will trigger a refresh (`refreshKeepingScroll`) which will perform `restoreScrollState` and sync the scrolling position
    this.setTimeSpan(newRange.startDate, newRange.endDate, maintainVisibleStart);
  }
  // If we change to infinite scrolling dynamically, it should create the buffer zones.
  updateInfiniteScroll(infiniteScroll) {
    // At construction time, this is handled in initScroll.
    // This is just here to handle dynamic updates.
    if (!this.isConfiguring && infiniteScroll) {
      this.checkTimeAxisScroll(this.timelineScroller.position);
    }
  }
  //region Scroll to date
  /**
   * Scrolls the timeline "tick" encapsulating the passed `Date` into view according to the passed options.
   * @param {Date} date The date to which to scroll the timeline
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @category Scrolling
   */
  async scrollToDate(date, options = {}) {
    const me = this,
      {
        timeAxis,
        visibleDateRange,
        infiniteScroll
      } = me,
      {
        unit,
        increment
      } = timeAxis,
      edgeOffset = options.edgeOffset || 0,
      visibleWidth = DateHelper.ceil(visibleDateRange.endDate, increment + ' ' + unit) - DateHelper.floor(visibleDateRange.startDate, increment + ' ' + unit),
      direction = date > me.viewportCenterDate ? 1 : -1,
      extraScroll = (infiniteScroll ? visibleWidth * me.bufferCoef * me.bufferThreshold : options.block === 'center' ? visibleWidth / 2 : edgeOffset ? me.getMilliSecondsPerPixelForZoomLevel(me.viewPreset) * edgeOffset : 0) * direction,
      visibleDate = new Date(date.getTime() + extraScroll),
      shiftDirection = visibleDate > timeAxis.endDate ? 1 : visibleDate < timeAxis.startDate ? -1 : 0;
    // Required visible date outside TimeAxis and infinite scrolling, that has opinions about how
    // much scroll range has to be created after the target date.
    if (shiftDirection && me.infiniteScroll) {
      me.shiftToDate(new Date(date - extraScroll), null, true);
      // shift to date could trigger a native browser async scroll out of our control. If a scroll
      // happens during scrollToCoordinate, the scrolling is cancelled so we wait a bit here
      await me.nextAnimationFrame();
    }
    const scrollerViewport = me.timelineScroller.viewport,
      localCoordinate = me.getCoordinateFromDate(date, true),
      // Available space can be less than tick size (Export.t.js in Gantt)
      width = Math.min(me.timeAxisViewModel.tickSize, me.timeAxisViewModel.availableSpace),
      target = me.isHorizontal
      // In RTL coordinate is for the right edge of the tick, so we need to subtract width
      ? new Rectangle(me.getCoordinateFromDate(date, false) - (me.rtl ? width : 0), scrollerViewport.y, width, scrollerViewport.height) : new Rectangle(scrollerViewport.x, me.getCoordinateFromDate(date, false), scrollerViewport.width, me.timeAxisViewModel.tickSize);
    await me.scrollToCoordinate(localCoordinate, target, date, options);
  }
  /**
   * Scrolls to current time.
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @category Scrolling
   */
  scrollToNow(options = {}) {
    return this.scrollToDate(new Date(), options);
  }
  /**
   * Used by {@link #function-scrollToDate} to scroll to correct coordinate.
   * @param {Number} localCoordinate Coordinate to scroll to
   * @param {Date} date Date to scroll to, used for reconfiguring the time axis
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @private
   * @category Scrolling
   */
  async scrollToCoordinate(localCoordinate, target, date, options = {}) {
    const me = this;
    // Not currently have this date in a timeaxis. Ignore negative scroll in weekview, it can be just 'filtered' with
    // startTime/endTime config
    if (localCoordinate < 0) {
      // adjust the timeaxis first
      const visibleSpan = me.endDate - me.startDate,
        {
          unit,
          increment
        } = me.timeAxis,
        newStartDate = DateHelper.floor(new Date(date.getTime() - visibleSpan / 2), increment + ' ' + unit),
        newEndDate = DateHelper.add(newStartDate, visibleSpan);
      // We're trying to reconfigure time span to current dates, which means we are as close to center as it
      // could be. Do nothing then.
      // covered by 1102_panel_api
      if (newStartDate - me.startDate !== 0 && newEndDate - me.endDate !== 0) {
        me.setTimeSpan(newStartDate, newEndDate);
        return me.scrollToDate(date, options);
      }
      return;
    }
    await me.timelineScroller.scrollIntoView(target, options);
    // Horizontal scroll is triggered on next frame in SubGrid.js, view is not up to date yet. Resolve on next frame
    return !me.isDestroyed && me.nextAnimationFrame();
  }
  //endregion
  //region Relative scrolling
  // These methods are important to users because although they are mixed into the top level Grid/Scheduler,
  // for X scrolling the explicitly target the SubGrid that holds the scheduler.
  /**
   * Get/set the `scrollLeft` value of the SubGrid that holds the scheduler.
   *
   * This may be __negative__ when the writing direction is right-to-left.
   * @property {Number}
   * @category Scrolling
   */
  set scrollLeft(left) {
    this.timeAxisSubGrid.scrollable.element.scrollLeft = left;
  }
  get scrollLeft() {
    return this.timeAxisSubGrid.scrollable.element.scrollLeft;
  }
  /**
   * Get/set the writing direction agnostic horizontal scroll position.
   *
   * This is always the __positive__ offset from the scroll origin whatever the writing
   * direction in use.
   *
   * Applies to the SubGrid that holds the scheduler
   * @property {Number}
   * @category Scrolling
   */
  set scrollX(x) {
    this.timeAxisSubGrid.scrollable.x = x;
  }
  get scrollX() {
    return this.timeAxisSubGrid.scrollable.x;
  }
  /**
   * Get/set vertical scroll
   * @property {Number}
   * @category Scrolling
   */
  set scrollTop(top) {
    this.scrollable.y = top;
  }
  get scrollTop() {
    return this.scrollable.y;
  }
  /**
   * Horizontal scrolling. Applies to the SubGrid that holds the scheduler
   * @param {Number} x
   * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
   * @returns {Promise} A promise which is resolved when the scrolling has finished.
   * @category Scrolling
   */
  scrollHorizontallyTo(coordinate, options = true) {
    return this.timeAxisSubGrid.scrollable.scrollTo(coordinate, null, options);
  }
  /**
   * Vertical scrolling
   * @param {Number} y
   * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
   * @returns {Promise} A promise which is resolved when the scrolling has finished.
   * @category Scrolling
   */
  scrollVerticallyTo(y, options = true) {
    return this.scrollable.scrollTo(null, y, options);
  }
  /**
   * Scrolls the subgrid that contains the scheduler
   * @param {Number} x
   * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
   * @returns {Promise} A promise which is resolved when the scrolling has finished.
   * @category Scrolling
   */
  scrollTo(x, options = true) {
    return this.timeAxisSubGrid.scrollable.scrollTo(x, null, options);
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});
// Internal class used to interrogate and manipulate the timeline scroll position.
// This delegates all operations to the appropriate Scroller, horizontal or vertical.
class TimelineScroller extends Scroller {
  static get configurable() {
    return {
      position: null,
      x: null,
      y: null
    };
  }
  // This class is passive about configuring the element.
  // It has no opinions about *how* the overflow is handled.
  updateOverflowX() {}
  updateOverflowY() {}
  onScroll(e) {
    super.onScroll(e);
    this._position = null;
  }
  syncPartners(force) {
    this.scrollable.syncPartners(force);
  }
  updatePosition(position) {
    this.scrollable[this.isHorizontal ? 'x' : 'y'] = position;
  }
  get viewport() {
    return this.scrollable.viewport;
  }
  get position() {
    return this._position = this.scrollable[this.isHorizontal ? 'x' : 'y'];
  }
  get clientSize() {
    return this.scrollable[`client${this.isHorizontal ? 'Width' : 'Height'}`];
  }
  get scrollSize() {
    return this.scrollable[`scroll${this.isHorizontal ? 'Width' : 'Height'}`];
  }
  get maxPosition() {
    return this.scrollable[`max${this.isHorizontal ? 'X' : 'Y'}`];
  }
  scrollTo(position, options) {
    return this.isHorizontal ? this.scrollable.scrollTo(position, null, options) : this.scrollable.scrollTo(null, position, options);
  }
  scrollBy(xDelta = 0, yDelta = 0, options = defaultScrollOptions$1) {
    // Use the correct delta by default, but if it's zero, accommodate axis error.
    return this.isHorizontal ? this.scrollable.scrollBy(xDelta || yDelta, 0, options) : this.scrollable.scrollBy(0, yDelta || xDelta, options);
  }
  scrollIntoView() {
    return this.scrollable.scrollIntoView(...arguments);
  }
  // We accommodate mistakes. Setting X and Y sets the appropriate scroll axis position
  changeX(x) {
    this.position = x;
  }
  changeY(y) {
    this.position = y;
  }
  get x() {
    return this.position;
  }
  set x(x) {
    this.scrollable[this.isHorizontal ? 'x' : 'y'] = x;
  }
  get y() {
    return this.position;
  }
  set y(y) {
    this.scroller[this.isHorizontal ? 'x' : 'y'] = y;
  }
  get clientWidth() {
    return this.clientSize;
  }
  get clientHeight() {
    return this.clientSize;
  }
  get scrollWidth() {
    return this.scrollSize;
  }
  get scrollHeight() {
    return this.scrollSize;
  }
  get maxX() {
    return this.maxPosition;
  }
  get maxY() {
    return this.maxPosition;
  }
}

/**
 * @module Scheduler/view/mixin/TimelineState
 */
const copyProperties$1 = ['barMargin'];
/**
 * Mixin for Timeline base that handles state. It serializes the following timeline properties:
 *
 * * barMargin
 * * zoomLevel
 *
 * See {@link Grid.view.mixin.GridState} and {@link Core.mixin.State} for more information on state.
 *
 * @mixin
 */
var TimelineState = (Target => class TimelineState extends (Target || Base) {
  static get $name() {
    return 'TimelineState';
  }
  /**
   * Gets or sets timeline's state. Check out {@link Scheduler.view.mixin.TimelineState} mixin for details.
   * @member {Object} state
   * @property {Object[]} state.columns
   * @property {Number} state.rowHeight
   * @property {Object} state.scroll
   * @property {Number} state.scroll.scrollLeft
   * @property {Number} state.scroll.scrollTop
   * @property {Array} state.selectedRecords
   * @property {String} state.style
   * @property {String} state.selectedCell
   * @property {Object} state.store
   * @property {Object} state.store.sorters
   * @property {Object} state.store.groupers
   * @property {Object} state.store.filters
   * @property {Object} state.subGrids
   * @property {Number} state.barMargin
   * @property {Number} state.zoomLevel
   * @category State
   */
  /**
   * Get timeline's current state for serialization. State includes rowHeight, headerHeight, readOnly, selectedCell,
   * selectedRecordId, column states and store state etc.
   * @returns {Object} State object to be serialized
   * @private
   */
  getState() {
    const me = this,
      state = ObjectHelper.copyProperties(super.getState(), me, copyProperties$1);
    state.zoomLevel = me.zoomLevel;
    state.zoomLevelOptions = {
      startDate: me.startDate,
      endDate: me.endDate,
      // With infinite scroll reading viewportCenterDate too early will lead to exception
      centerDate: !me.infiniteScroll || me.timeAxisViewModel.availableSpace ? me.viewportCenterDate : undefined,
      width: me.tickSize
    };
    return state;
  }
  /**
   * Apply previously stored state.
   * @param {Object} state
   * @private
   */
  applyState(state) {
    const me = this;
    me.suspendRefresh();
    ObjectHelper.copyProperties(me, state, copyProperties$1);
    super.applyState(state);
    if (state.zoomLevel != null) {
      // Do not restore left scroll, infinite scroll should do all the work
      if (me.infiniteScroll) {
        var _state$scroll;
        if (state !== null && state !== void 0 && (_state$scroll = state.scroll) !== null && _state$scroll !== void 0 && _state$scroll.scrollLeft) {
          state.scroll.scrollLeft = {};
        }
      }
      if (me.isPainted) {
        me.zoomToLevel(state.zoomLevel, state.zoomLevelOptions);
      } else {
        me._zoomAfterPaint = {
          zoomLevel: state.zoomLevel,
          zoomLevelOptions: state.zoomLevelOptions
        };
      }
    }
    me.resumeRefresh(true);
  }
  onPaint(...args) {
    super.onPaint(...args);
    if (this._zoomAfterPaint) {
      const {
        zoomLevel,
        zoomLevelOptions
      } = this._zoomAfterPaint;
      this.zoomToLevel(zoomLevel, zoomLevelOptions);
      delete this._zoomAfterPaint;
    }
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/Header
 */
/**
 * Custom header subclass which handles the existence of the special TimeAxisColumn
 *
 * @extends Grid/view/Header
 * @private
 */
class Header extends Header$1 {
  static get $name() {
    return 'SchedulerHeader';
  }
  refreshContent() {
    var _this$headersElement;
    // Only render contents into the header once as it contains the special rendering of the TimeAxisColumn
    // In case ResizeObserver polyfill is used headers element will have resize monitors inserted and we should
    // take that into account
    // https://github.com/bryntum/support/issues/3444
    if (!((_this$headersElement = this.headersElement) !== null && _this$headersElement !== void 0 && _this$headersElement.querySelector('.b-sch-timeaxiscolumn'))) {
      super.refreshContent();
    }
  }
}
Header._$name = 'Header';

/**
 * @module Scheduler/view/TimeAxisSubGrid
 */
/**
 * Widget that encapsulates the SubGrid part of the scheduler which houses the timeline view.
 * @extends Grid/view/SubGrid
 * @private
 */
class TimeAxisSubGrid extends SubGrid {
  static get $name() {
    return 'TimeAxisSubGrid';
  }
  // Factoryable type name
  static get type() {
    return 'timeaxissubgrid';
  }
  static get configurable() {
    return {
      // A Scheduler's SubGrid doesn't accept external columns moving in
      sealedColumns: true,
      // Use Scheduler's Header class
      headerClass: Header
    };
  }
  startConfigure(config) {
    const {
      grid: scheduler
    } = config;
    // Scheduler references its TimeAxisSubGrid instance through this property.
    scheduler.timeAxisSubGrid = this;
    super.startConfigure(config);
    if (scheduler.isHorizontal) {
      config.header = {
        cls: {
          'b-sticky-headers': scheduler.stickyHeaders
        }
      };
      // We don't use what the GridSubGrids mixin tells us to.
      // We use the Sheduler's Header class.
      delete config.headerClass;
    }
    // If user have not specified a width or flex for scheduler region, default to flex=1
    if (!('flex' in config || 'width' in config)) {
      config.flex = 1;
    }
  }
  changeScrollable() {
    const me = this,
      scrollable = super.changeScrollable(...arguments);
    // TimeAxisSubGrid's X axis is stretched by its canvas.
    // We don't need the Scroller's default stretching implementation.
    if (scrollable) {
      Object.defineProperty(scrollable, 'scrollWidth', {
        get() {
          var _this$element;
          return ((_this$element = this.element) === null || _this$element === void 0 ? void 0 : _this$element.scrollWidth) ?? 0;
        },
        set() {
          // Setting the scroll width to be wide just updates the canvas side in Scheduler.
          // We do not need the Scroller's default stretcher element to be added.
          // Note that "me" here is the TimeAxisSubGrid, so we are calling Scheduler.
          me.grid.updateCanvasSize();
        }
      });
    }
    return scrollable;
  }
  handleHorizontalScroll(addCls = true) {
    // Swallow scroll syncing calls that happen during view preset changes, that process triggers multiple when
    // it first changes tickWidth, then scrolls to center and then an additional sync on scroll end
    if (!this.grid._viewPresetChanging) {
      super.handleHorizontalScroll(addCls);
    }
  }
  /**
   * This is an event handler triggered when the TimeAxisSubGrid changes size.
   * Its height changes when content height changes, and that is not what we are
   * interested in here. If the *width* changes, that means the visible viewport
   * has changed size.
   * @param {HTMLElement} element
   * @param {Number} width
   * @param {Number} height
   * @param {Number} oldWidth
   * @param {Number} oldHeight
   * @private
   */
  onInternalResize(element, width, height, oldWidth, oldHeight) {
    const me = this;
    // We, as the TimeAxisSubGrid dictate the scheduler viewport width
    if (me.isPainted && width !== oldWidth) {
      const scheduler = me.grid,
        bodyHeight = scheduler._bodyRectangle.height;
      // Avoid ResizeObserver errors when this operation may create a scrollbar
      if (DomHelper.scrollBarWidth && width < oldWidth) {
        me.monitorResize = false;
      }
      scheduler.onSchedulerViewportResize(width, bodyHeight, oldWidth, bodyHeight);
      // Revert to monitoring on the next animation frame.
      // This is to avoid "ResizeObserver loop completed with undelivered notifications."
      if (!me.monitorResize) {
        me.requestAnimationFrame(() => me.monitorResize = true);
      }
    }
    super.onInternalResize(...arguments);
  }
  // When restoring state we need to update time axis size immediately, resize event is not triggered fast enough to
  // restore center date consistently
  clearWidthCache() {
    super.clearWidthCache();
    // Check if we are in horizontal mode
    if (this.owner.isHorizontal) {
      this.owner.updateViewModelAvailableSpace(this.width);
    }
  }
  async expand() {
    const {
      owner
    } = this;
    await super.expand();
    if (owner.isPainted) {
      owner.timeAxisViewModel.update(this.width, false, true);
    }
  }
}
// Register this widget type with its Factory
TimeAxisSubGrid.initClass();
TimeAxisSubGrid._$name = 'TimeAxisSubGrid';

const exitTransition = {
    fn: 'exitTransition',
    delay: 0,
    cancelOutstanding: true
  },
  inRange = (v, r0, r1) => r0 == null ? r1 == null || v < r1 : r1 == null ? v >= r0 : r0 < r1 ? r0 <= v && v < r1 // 5 in [1, 10]  (after 1 and before 10)
  : v < r1 || r0 <= v,
  // 5 in [10, 1]  (after 10 or before 1)
  isWorkingTime = (d, wt) => inRange(d.getDay(), wt.fromDay, wt.toDay) && inRange(d.getHours(), wt.fromHour, wt.toHour),
  emptyObject$2 = {};
/**
 * @module Scheduler/view/TimelineBase
 */
/**
 * Options accepted by the Scheduler's {@link Scheduler.view.Scheduler#config-visibleDate} property.
 *
 * @typedef {Object} VisibleDate
 * @property {Date} date The date to bring into view.
 * @property {'start'|'end'|'center'|'nearest'} [block] How far to scroll the date.
 * @property {Number} [edgeOffset] edgeOffset A margin around the date to bring into view.
 * @property {AnimateScrollOptions|Boolean|Number} [animate] Set to `true` to animate the scroll by 300ms,
 * or the number of milliseconds to animate over, or an animation config object.
 */
/**
 * Abstract base class used by timeline based components such as Scheduler and Gantt. Based on Grid, supplies a "locked"
 * region for columns and a "normal" for rendering of events etc.
 * @abstract
 *
 * @mixes Scheduler/view/mixin/TimelineDateMapper
 * @mixes Scheduler/view/mixin/TimelineDomEvents
 * @mixes Scheduler/view/mixin/TimelineEventRendering
 * @mixes Scheduler/view/mixin/TimelineScroll
 * @mixes Scheduler/view/mixin/TimelineState
 * @mixes Scheduler/view/mixin/TimelineViewPresets
 * @mixes Scheduler/view/mixin/TimelineZoomable
 * @mixes Scheduler/view/mixin/RecurringEvents
 *
 * @extends Grid/view/Grid
 */
class TimelineBase extends GridBase.mixin(TimelineDateMapper, TimelineDomEvents, TimelineEventRendering, TimelineScroll, TimelineState, TimelineViewPresets, TimelineZoomable, RecurringEvents) {
  //region Config
  static get $name() {
    return 'TimelineBase';
  }
  // Factoryable type name
  static get type() {
    return 'timelinebase';
  }
  static configurable = {
    partnerSharedConfigs: {
      value: ['timeAxisViewModel', 'timeAxis', 'viewPreset'],
      $config: {
        merge: 'distinct'
      }
    },
    /**
     * Get/set startDate. Defaults to current date if none specified.
     *
     * When using {@link #config-infiniteScroll}, use {@link #config-visibleDate} to control initially visible date
     * instead.
     *
     * **Note:** If you need to set start and end date at the same time, use {@link #function-setTimeSpan} method.
     * @member {Date} startDate
     * @category Common
     */
    /**
     * The start date of the timeline (if not configure with {@link #config-infiniteScroll}).
     *
     * If omitted, and a TimeAxis has been set, the start date of the provided {@link Scheduler.data.TimeAxis} will
     * be used. If no TimeAxis has been configured, it'll use the start/end dates of the loaded event dataset. If no
     * date information exists in the event data set, it defaults to the current date and time.
     *
     * If a string is supplied, it will be parsed using
     * {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat}.
     *
     * When using {@link #config-infiniteScroll}, use {@link #config-visibleDate} to control initially visible date
     * instead.
     *
     * **Note:** If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
     * @config {Date|String}
     * @category Common
     */
    startDate: {
      $config: {
        equal: 'date'
      },
      value: null
    },
    /**
     * Get/set endDate. Defaults to startDate + default span of the used ViewPreset.
     *
     * **Note:** If you need to set start and end date at the same time, use {@link #function-setTimeSpan} method.
     * @member {Date} endDate
     * @category Common
     */
    /**
     * The end date of the timeline (if not configure with {@link #config-infiniteScroll}).
     *
     * If omitted, it will be calculated based on the {@link #config-startDate} setting and the 'defaultSpan'
     * property of the current {@link #config-viewPreset}.
     *
     * If a string is supplied, it will be parsed using
     * {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat}.
     *
     * **Note:** If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
     * @config {Date|String}
     * @category Common
     */
    endDate: {
      $config: {
        equal: 'date'
      },
      value: null
    },
    /**
     * When set, the text in the major time axis header sticks in the scrolling viewport as long as possible.
     * @config {Boolean}
     * @default
     * @category Time axis
     */
    stickyHeaders: true,
    /**
     * A scrolling `options` object describing the scroll action, including a `date` option
     * which references a `Date`. See {@link #function-scrollToDate} for details about scrolling options.
     *
     * ```javascript
     *     // The date we want in the center of the Scheduler viewport
     *     myScheduler.visibleDate = {
     *         date    : new Date(2023, 5, 17, 12),
     *         block   : 'center',
     *         animate : true
     *     };
     * ```
     * @member {Object} visibleDate
     * @category Common
     */
    /**
     * A date to bring into view initially on the scrollable timeline.
     *
     * This may be configured as either a `Date` or a scrolling `options` object describing
     * the scroll action, including a `date` option which references a `Date`.
     *
     * See {@link #function-scrollToDate} for details about scrolling options.
     *
     * Note that if a naked `Date` is passed, it will be stored internally as a scrolling options object
     * using the following defaults:
     *
     * ```javascript
     * {
     *     date  : <The Date object>,
     *     block : 'nearest'
     * }
     * ```
     *
     * This moves the date into view by the shortest scroll, so that it just appears at an edge.
     *
     * To bring your date of interest to the center of the viewport, configure your
     * Scheduler thus:
     *
     * ```javascript
     *     visibleDate : {
     *         date  : new Date(2023, 5, 17, 12),
     *         block : 'center'
     *     }
     * ```
     * @config {Date|VisibleDate}
     * @category Common
     */
    visibleDate: null,
    /**
     * CSS class to add to rendered events
     * @config {String}
     * @category CSS
     * @private
     */
    eventCls: null,
    /**
     * Set to `true` to force the time columns to fit to the available space (horizontal or vertical depends on mode).
     * Note that setting {@link #config-suppressFit} to `true`, will disable `forceFit` functionality. Zooming
     * cannot be used when `forceFit` is set.
     * @prp {Boolean}
     * @default
     * @category Time axis
     */
    forceFit: false,
    /**
     * Set to a time zone or a UTC offset. This will set the projects
     * {@link Scheduler.model.ProjectModel#config-timeZone} config accordingly. As this config is only a referer,
     * please se project's config {@link Scheduler.model.ProjectModel#config-timeZone documentation} for more
     * information.
     *
     * ```javascript
     * new Calendar(){
     *   timeZone : 'America/Chicago'
     * }
     * ```
     * @prp {String|Number} timeZone
     * @category Misc
     */
    timeZone: null
  };
  static get defaultConfig() {
    return {
      /**
       * A valid JS day index between 0-6 (0: Sunday, 1: Monday etc.) to be considered the start day of the week.
       * When omitted, the week start day is retrieved from the active locale class.
       * @config {Number} weekStartDay
       * @category Time axis
       */
      /**
       * An object with format `{ fromDay, toDay, fromHour, toHour }` that describes the working days and hours.
       * This object will be used to populate TimeAxis {@link Scheduler.data.TimeAxis#config-include} property.
       *
       * Using it results in a non-continuous time axis. Any ticks not covered by the working days and hours will
       * be excluded. Events within larger ticks (for example if using week as the unit for ticks) will be
       * stretched to fill the gap otherwise left by the non working hours.
       *
       * As with end dates, `toDay` and `toHour` are exclusive. Thus `toDay : 6` means that day 6 (saturday) will
       * not be included.
       *
       *
       * **NOTE:** When this feature is enabled {@link Scheduler.view.mixin.TimelineZoomable Zooming feature} is
       * not supported. It's recommended to disable zooming controls:
       *
       * ```javascript
       * new Scheduler({
       *     zoomOnMouseWheel          : false,
       *     zoomOnTimeAxisDoubleClick : false,
       *     ...
       * });
       * ```
       *
       * @config {Object}
       * @category Time axis
       */
      workingTime: null,
      /**
       * A backing data store of 'ticks' providing the input date data for the time axis of timeline panel.
       * @member {Scheduler.data.TimeAxis} timeAxis
       * @readonly
       * @category Time axis
       */
      /**
       * A {@link Scheduler.data.TimeAxis} config object or instance, used to create a backing data store of
       * 'ticks' providing the input date data for the time axis of timeline panel. Created automatically if none
       * supplied.
       * @config {TimeAxisConfig|Scheduler.data.TimeAxis}
       * @category Time axis
       */
      timeAxis: null,
      /**
       * The backing view model for the visual representation of the time axis.
       * Either a real instance or a simple config object.
       * @private
       * @config {Scheduler.view.model.TimeAxisViewModel|TimeAxisViewModelConfig}
       * @category Time axis
       */
      timeAxisViewModel: null,
      /**
       * You can set this option to `false` to make the timeline panel start and end on the exact provided
       * {@link #config-startDate}/{@link #config-endDate} w/o adjusting them.
       * @config {Boolean}
       * @default
       * @category Time axis
       */
      autoAdjustTimeAxis: true,
      /**
       * Affects drag drop and resizing of events when {@link Scheduler/view/mixin/TimelineDateMapper#config-snap}
       * is enabled.
       *
       * If set to `true`, dates will be snapped relative to event start. e.g. for a zoom level with
       * `timeResolution = { unit: "s", increment: "20" }`, an event that starts at 10:00:03 and is dragged would
       * snap its start date to 10:00:23, 10:00:43 etc.
       *
       * When set to `false`, dates will be snapped relative to the timeAxis startDate (tick start)
       * - 10:00:03 -> 10:00:20, 10:00:40 etc.
       *
       * @config {Boolean}
       * @default
       * @category Scheduled events
       */
      snapRelativeToEventStartDate: false,
      /**
       * Set to `true` to prevent auto calculating of a minimal {@link Scheduler.view.mixin.TimelineEventRendering#property-tickSize}
       * to always fit the content to the screen size. Setting this property on `true` will disable {@link #config-forceFit} behaviour.
       * @config {Boolean}
       * @default false
       * @category Time axis
       */
      suppressFit: false,
      /**
       * CSS class to add to cells in the timeaxis column
       * @config {String}
       * @category CSS
       * @private
       */
      timeCellCls: null,
      scheduledEventName: null,
      //dblClickTime : 200,
      /**
       * A CSS class to apply to each event in the view on mouseover.
       * @config {String}
       * @category CSS
       * @private
       */
      overScheduledEventClass: null,
      // allow the panel to prevent adding the hover CSS class in some cases - during drag drop operations
      preventOverCls: false,
      // This setting is set to true by features that need it
      useBackgroundCanvas: false,
      /**
       * Set to `false` if you don't want event bar DOM updates to animate.
       * @prp {Boolean}
       * @default true
       * @category Scheduled events
       */
      enableEventAnimations: true,
      disableGridRowModelWarning: true,
      // does not look good with locked columns and also interferes with event animations
      animateRemovingRows: false,
      /**
       * Partners this Timeline panel with another Timeline in order to sync their region sizes (sub-grids like locked, normal will get the same width),
       * start and end dates, view preset, zoom level and scrolling position. All these values will be synced with the timeline defined as the `partner`.
       *
       * - To add a new partner dynamically see {@link #function-addPartner} method.
       * - To remove existing partner see {@link #function-removePartner} method.
       * - To check if timelines are partners see {@link #function-isPartneredWith} method.
       *
       * Column widths and hide/show state are synced between partnered schedulers when the column set is identical.
       * @config {Scheduler.view.TimelineBase}
       * @category Time axis
       */
      partner: null,
      schedulerRegion: 'normal',
      transitionDuration: 200,
      // internal timer id reference
      animationTimeout: null,
      /**
       * Region to which columns are added when they have none specified
       * @config {String}
       * @default
       * @category Misc
       */
      defaultRegion: 'locked',
      /**
       * Decimal precision used when displaying durations, used by tooltips and DurationColumn.
       * Specify `false` to use raw value
       * @config {Number|Boolean}
       * @default
       * @category Common
       */
      durationDisplayPrecision: 1,
      /**
       * An object with configuration for the {@link Scheduler.column.TimeAxisColumn} in horizontal
       * {@link Scheduler.view.SchedulerBase#config-mode}.
       *
       * Example:
       *
       * ```javascript
       * new Scheduler({
       *     timeAxisColumn : {
       *         renderer : ({ record, cellElement }) => {
       *             // output some markup as a layer below the events layer, you can draw a chart for example
       *         }
       *     },
       *     ...
       * });
       * ```
       *
       * @config {TimeAxisColumnConfig} timeAxisColumn
       * @category Time axis
       */
      asyncEventSuffix: 'PreCommit'
    };
  }
  timeCellSelector = null;
  updateTimeZone(timeZone) {
    if (this.project) {
      if (this.isConfiguring) {
        this.project._isConfiguringTimeZone = true;
      }
      this.project.timeZone = timeZone;
    }
  }
  get timeZone() {
    var _this$project;
    return (_this$project = this.project) === null || _this$project === void 0 ? void 0 : _this$project.timeZone;
  }
  //endregion
  //region Feature hooks
  /**
   * Populates the event context menu. Chained in features to add menu items.
   * @param {Object} options Contains menu items and extra data retrieved from the menu target.
   * @param {Grid.column.Column} options.column Column for which the menu will be shown.
   * @param {Scheduler.model.EventModel} options.eventRecord The context event.
   * @param {Scheduler.model.ResourceModel} options.resourceRecord The context resource.
   * @param {Scheduler.model.AssignmentModel} options.assignmentRecord The context assignment if any.
   * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items.
   * @internal
   */
  populateEventMenu() {}
  /**
   * Populates the time axis context menu. Chained in features to add menu items.
   * @param {Object} options Contains menu items and extra data retrieved from the menu target.
   * @param {Grid.column.Column} options.column Column for which the menu will be shown.
   * @param {Scheduler.model.ResourceModel} options.resourceRecord The context resource.
   * @param {Date} options.date The Date corresponding to the mouse position in the time axis.
   * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items.
   * @internal
   */
  populateScheduleMenu() {}
  // Called when visible date range potentially changes such as when scrolling in
  // the time axis.
  onVisibleDateRangeChange(range) {
    if (!this.handlingVisibleDateRangeChange) {
      const me = this,
        {
          _visibleDateRange
        } = me,
        dateRangeChange = !_visibleDateRange || _visibleDateRange.startDate - range.startDate || _visibleDateRange.endDate - range.endDate;
      if (dateRangeChange) {
        me.timeView.range = range;
        me.handlingVisibleDateRangeChange = true;
        /**
         * Fired when the range of dates visible within the viewport changes. This will be when
         * scrolling along a time axis.
         *
         * __Note__ that this event will fire frequently during scrolling, so any listener
         * should probably be added with the `buffer` option to slow down the calls to your
         * handler function :
         *
         * ```javascript
         * listeners : {
         *     visibleDateRangeChange({ old, new }) {
         *         this.updateRangeRequired(old, new);
         *     },
         *     // Only call once. 300 ms after the last event was detected
         *     buffer : 300
         * }
         * ```
         * @event visibleDateRangeChange
         * @param {Scheduler.view.Scheduler} source This Scheduler instance.
         * @param {Object} old The old date range
         * @param {Date} old.startDate the old start date.
         * @param {Date} old.endDate the old end date.
         * @param {Object} new The new date range
         * @param {Date} new.startDate the new start date.
         * @param {Date} new.endDate the new end date.
         */
        me.trigger('visibleDateRangeChange', {
          old: _visibleDateRange,
          new: range
        });
        me.handlingVisibleDateRangeChange = false;
        me._visibleDateRange = range;
      }
    }
  }
  // Called when visible resource range changes in vertical mode
  onVisibleResourceRangeChange() {}
  //endregion
  //region Init
  construct(config = {}) {
    const me = this;
    super.construct(config);
    me.$firstVerticalOverflow = true;
    me.initDomEvents();
    me.currentOrientation.init();
    me.rowManager.ion({
      refresh: () => {
        me.forceLayout = false;
      }
    });
  }
  // Override from Grid.view.GridSubGrids
  createSubGrid(region, config = {}) {
    const me = this,
      {
        stickyHeaders
      } = me;
    // We are creating the TimeAxisSubGrid
    if (region === (me.schedulerRegion || 'normal')) {
      config.type = 'timeaxissubgrid';
    }
    // The assumption is that if we are in vertical mode, the locked SubGrid
    // is used to house the verticalTimeAxis, and so it must all be overflow:visible
    else if (region === 'locked' && stickyHeaders && me.isVertical) {
      config.scrollable = {
        overflowX: 'visible',
        overflowY: 'visible'
      };
      // It's the child of the overflowElement
      me.bodyContainer.classList.add('b-sticky-headers');
    }
    return super.createSubGrid(region, config);
  }
  doDestroy() {
    const me = this,
      {
        partneredWith,
        currentOrientation
      } = me;
    currentOrientation === null || currentOrientation === void 0 ? void 0 : currentOrientation.destroy();
    // Break links between this TimeLine and any partners.
    if (partneredWith) {
      partneredWith.forEach(p => {
        me.removePartner(p);
      });
      partneredWith.destroy();
    } else {
      me.timeAxisViewModel.destroy();
      me.timeAxis.destroy();
    }
    super.doDestroy();
  }
  startConfigure(config) {
    super.startConfigure(config);
    // When the body height changes, we must update the SchedulerViewport's height
    ResizeMonitor.addResizeListener(this.bodyContainer, this.onBodyResize.bind(this));
    // partner needs to be initialized first so that the various shared
    // configs are assigned first before we default them in.
    this.getConfig('partner');
  }
  changeStartDate(startDate) {
    if (typeof startDate === 'string') {
      startDate = DateHelper.parse(startDate);
    }
    return startDate;
  }
  onPaint({
    firstPaint
  }) {
    // Upon first paint we need to pass the forceUpdate flag in case we are sharing the TimAxisViewModel
    // with another Timeline which will already have done this.
    if (firstPaint) {
      // Take height from container element
      const me = this,
        scrollable = me.isHorizontal ? me.timeAxisSubGrid.scrollable : me.scrollable,
        // Use exact subpixel available space so that tick size calculation is correct.
        availableSpace = scrollable.element.getBoundingClientRect()[me.isHorizontal ? 'width' : 'height'];
      // silent = true if infiniteScroll. If that is set, TimelineScroll.initScroll which is
      // called by the base class's onPaint reconfigures the TAVM when it initializes.
      me.timeAxisViewModel.update(availableSpace, me.infiniteScroll, true);
      // If infiniteScroll caused the TAVM update to be silent, force the rendering to
      // get hold of the scroll state and visible range
      if (me.infiniteScroll) {
        var _me$currentOrientatio, _me$currentOrientatio2;
        (_me$currentOrientatio = (_me$currentOrientatio2 = me.currentOrientation).doUpdateTimeView) === null || _me$currentOrientatio === void 0 ? void 0 : _me$currentOrientatio.call(_me$currentOrientatio2);
      }
    }
    super.onPaint(...arguments);
  }
  onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX) {
    // rerender cells in scheduler column on horizontal scroll to display events in view
    this.currentOrientation.updateFromHorizontalScroll(scrollX);
    super.onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX);
  }
  /**
   * Overrides initScroll from Grid, listens for horizontal scroll to do virtual event rendering
   * @private
   */
  initScroll() {
    const me = this;
    let frameCount = 0;
    super.initScroll();
    me.ion({
      horizontalScroll: ({
        subGrid,
        scrollLeft,
        scrollX
      }) => {
        if (me.isPainted && subGrid === me.timeAxisSubGrid && !me.isDestroying && !me.refreshSuspended) {
          me.onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX);
        }
        frameCount++;
      }
    });
    if (me.testPerformance === 'horizontal') {
      me.setTimeout(() => {
        const start = performance.now();
        let scrollSpeed = 5,
          direction = 1;
        const scrollInterval = me.setInterval(() => {
          scrollSpeed = scrollSpeed + 5;
          me.scrollX += (10 + Math.floor(scrollSpeed)) * direction;
          if (direction === 1 && me.scrollX > 5500) {
            direction = -1;
            scrollSpeed = 5;
          }
          if (direction === -1 && me.scrollX <= 0) {
            const done = performance.now(),
              // eslint-disable-line no-undef
              elapsed = done - start;
            const timePerFrame = elapsed / frameCount,
              fps = Math.round(1000 / timePerFrame * 10) / 10;
            clearInterval(scrollInterval);
            console.log(me.eventPositionMode, me.eventScrollMode, fps + 'fps');
          }
        }, 0);
      }, 500);
    }
  }
  //endregion
  /**
   * Calls the specified function (returning its return value) and preserves the timeline center
   * point. This is a useful way of retaining the user's visual context while making updates
   * and changes to the view which require major changes or a full refresh.
   * @param {Function} fn The function to call.
   * @param {Object} thisObj The `this` context for the function.
   * @param {...*} args Parameters to the function.
   */
  preserveViewCenter(fn, thisObj = this, ...args) {
    const me = this,
      centerDate = me.viewportCenterDate,
      result = fn.apply(thisObj, args),
      scroller = me.timelineScroller,
      {
        clientSize
      } = scroller,
      scrollStart = Math.max(Math.floor(me.getCoordinateFromDate(centerDate, true) - clientSize / 2), 0);
    me.scrollingToCenter = true;
    scroller.scrollTo(scrollStart, false).then(() => me.scrollingToCenter = false);
    return result;
  }
  /**
   * Changes this Scheduler's time axis timespan to the supplied start and end dates.
   *
   * @async
   * @param {Date} newStartDate The new start date
   * @param {Date} newEndDate The new end date
   * @param {Object} [options] An object containing modifiers for the time span change operation.
   * @param {Boolean} [options.maintainVisibleStart] Specify as `true` to keep the visible start date stable.
   * @param {Date} [options.visibleDate] The date inside the range to scroll into view
   */
  setTimeSpan(newStartDate, newEndDate, options = emptyObject$2) {
    const me = this,
      {
        timeAxis
      } = me,
      {
        preventThrow = false,
        // Private, only used by the shift method.
        maintainVisibleStart = false,
        visibleDate
      } = options,
      {
        startDate,
        endDate
      } = timeAxis.getAdjustedDates(newStartDate, newEndDate),
      startChanged = timeAxis.startDate - startDate !== 0,
      endChanged = timeAxis.endDate - endDate !== 0;
    if (startChanged || endChanged) {
      if (maintainVisibleStart) {
        const {
            timeAxisViewModel
          } = me,
          {
            totalSize
          } = timeAxisViewModel,
          oldTickSize = timeAxisViewModel.tickSize,
          scrollable = me.timelineScroller,
          currentScroll = scrollable.position,
          visibleStart = timeAxisViewModel.getDateFromPosition(currentScroll);
        // If the current visibleStart is in the new range, maintain it
        // So that there is no visual jump.
        if (visibleStart >= startDate && visibleStart < endDate) {
          // We need to correct the scroll position as soon as the TimeAxisViewModel
          // has updated itself and before any other UI updates which that may trigger.
          timeAxisViewModel.ion({
            update() {
              const tickSizeChanged = timeAxisViewModel.tickSize !== oldTickSize;
              // Ensure the canvas element matches the TimeAxisViewModel's new totalSize.
              // This creates the required scroll range to be able to have the scroll
              // position correct before any further UI updates.
              me.updateCanvasSize();
              // If *only* the start moved, we can keep scroll position the same
              // by adjusting it by the amount the start moved.
              if (startChanged && !endChanged && !tickSizeChanged) {
                scrollable.position += timeAxisViewModel.totalSize - totalSize;
              }
              // If only the end has changed, and tick size is same, we can maintain
              // the same scroll position.
              else if (!startChanged && !tickSizeChanged) {
                scrollable.position = currentScroll;
              }
              // Fall back to restoring the position by restoring the visible start time
              else {
                scrollable.position = timeAxisViewModel.getPositionFromDate(visibleStart);
              }
              // Force partners to sync with what we've just done to reset the scroll.
              // We are now in control.
              scrollable.syncPartners(true);
            },
            prio: 10000,
            once: true
          });
        }
      }
      const returnValue = timeAxis.reconfigure({
        startDate,
        endDate
      }, false, preventThrow);
      if (visibleDate) {
        return me.scrollToDate(visibleDate, options).then(() => returnValue);
      }
      return returnValue;
    }
  }
  //region Config getters/setters
  /**
   * Returns `true` if any of the events/tasks or feature injected elements (such as ResourceTimeRanges) are within
   * the {@link #config-timeAxis}
   * @property {Boolean}
   * @readonly
   * @category Scheduled events
   */
  get hasVisibleEvents() {
    return !this.noFeatureElementsInAxis() || this.eventStore.storage.values.some(t => this.timeAxis.isTimeSpanInAxis(t));
  }
  // Template function to be chained in features to determine if any elements are in time axis (needed since we cannot
  // currently chain getters). Negated to not break chain. First feature that has elements visible returns false,
  // which prevents other features from being queried.
  noFeatureElementsInAxis() {}
  // Private getter used to piece together event names such as beforeEventDrag / beforeTaskDrag. Could also be used
  // in templates.
  get capitalizedEventName() {
    if (!this._capitalizedEventName) {
      this._capitalizedEventName = StringHelper.capitalize(this.scheduledEventName);
    }
    return this._capitalizedEventName;
  }
  set partner(partner) {
    this._partner = partner;
    this.addPartner(partner);
  }
  /**
   * Partners this Timeline with the passed Timeline in order to sync the horizontal scrolling position and zoom level.
   *
   * - To remove existing partner see {@link #function-removePartner} method.
   * - To get the list of partners see {@link #property-partners} getter.
   *
   * @param {Scheduler.view.TimelineBase} otherTimeline The timeline to partner with
   */
  addPartner(partner) {
    const me = this;
    if (!me.isPartneredWith(partner)) {
      const partneredWith = me.partneredWith || (me.partneredWith = new Collection());
      // Each must know about the other so that they can sync others upon region resize
      partneredWith.add(partner);
      (partner.partneredWith || (partner.partneredWith = new Collection())).add(me);
      // Flush through viewPreset initGetter so that the setup in setConfig doesn't
      // take them to be the class's defined getters.
      me.getConfig('viewPreset');
      partner.ion({
        presetchange: 'onPartnerPresetChange',
        thisObj: me
      });
      partner.scrollable.ion({
        overflowChange: 'onPartnerOverflowChange',
        thisObj: me
      });
      // collect configs that are meant to be shared between partners
      const partnerSharedConfig = me.partnerSharedConfigs.reduce((config, configName) => {
        config[configName] = partner[configName];
        return config;
      }, {});
      me.setConfig(partnerSharedConfig);
      me.ion({
        presetchange: 'onPartnerPresetChange',
        thisObj: partner
      });
      me.scrollable.ion({
        overflowChange: 'onPartnerOverflowChange',
        thisObj: partner
      });
      if (me.isPainted) {
        me.scrollable.addPartner(partner.scrollable, me.isHorizontal ? 'x' : 'y');
        partner.syncPartnerSubGrids();
      } else {
        // When initScroll comes round, make sure it syncs with the partner
        me.initScroll = FunctionHelper.createSequence(me.initScroll, () => {
          me.scrollable.addPartner(partner.scrollable, me.isHorizontal ? 'x' : 'y');
          partner.syncPartnerSubGrids();
        }, me);
      }
    }
  }
  /**
   * Breaks the link between current Timeline and the passed Timeline
   *
   * - To add a new partner see {@link #function-addPartner} method.
   * - To get the list of partners see {@link #property-partners} getter.
   *
   * @param {Scheduler.view.TimelineBase} otherTimeline The timeline to unlink from
   */
  removePartner(partner) {
    const me = this,
      {
        partneredWith
      } = me;
    if (me.isPartneredWith(partner)) {
      partneredWith.remove(partner);
      me.scrollable.removePartner(partner.scrollable);
      me.un({
        presetchange: 'onPartnerPresetChange',
        thisObj: partner
      });
      me.scrollable.un({
        overflowChange: 'onPartnerOverflowChange',
        thisObj: partner
      });
      partner.removePartner(me);
    }
  }
  /**
   * Checks whether the passed timeline is partnered with the current timeline.
   * @param {Scheduler.view.TimelineBase} partner The timeline to check the partnering with
   * @returns {Boolean} Returns `true` if the timelines are partnered
   */
  isPartneredWith(partner) {
    var _this$partneredWith;
    return Boolean((_this$partneredWith = this.partneredWith) === null || _this$partneredWith === void 0 ? void 0 : _this$partneredWith.includes(partner));
  }
  /**
   * Called when a partner scheduler changes its overflowing state. The scrollable
   * of a Grid/Scheduler only handles overflowY, so this will mean the addition
   * or removal of a vertical scrollbar.
   *
   * All partners must stay in sync. If another parter has a vertical scrollbar
   * and we do not, we must set our overflowY to 'scroll' so that we show an empty
   * scrollbar to keep widths synchronized.
   * @param {Object} event A {@link Core.helper.util.Scroller#event-overflowChange} event
   * @internal
   */
  onPartnerOverflowChange({
    source: otherScrollable,
    y
  }) {
    const {
        scrollable
      } = this,
      ourY = scrollable.hasOverflow('y');
    // If we disagree with our partner, the partner which doesn't have
    // overflow, has to become overflowY : scroll
    if (ourY !== y) {
      if (ourY) {
        otherScrollable.overflowY = 'scroll';
      } else {
        otherScrollable.overflowY = true;
        scrollable.overflowY = 'scroll';
        this.refreshVirtualScrollbars();
      }
    }
    // If we agree with our partner, we can reset ourselves to overflowY : auto
    else {
      scrollable.overflowY = true;
    }
  }
  onPartnerPresetChange({
    preset,
    startDate,
    endDate,
    centerDate,
    zoomDate,
    zoomPosition,
    zoomLevel
  }) {
    if (!this._viewPresetChanging && this.viewPreset !== preset) {
      // Passed through to the viewPreset changing method
      preset.options = {
        startDate,
        endDate,
        centerDate,
        zoomDate,
        zoomPosition,
        zoomLevel
      };
      this.viewPreset = preset;
    }
  }
  get partner() {
    return this._partner;
  }
  /**
   * Returns the partnered timelines.
   *
   * - To add a new partner see {@link #function-addPartner} method.
   * - To remove existing partner see {@link #function-removePartner} method.
   *
   * @readonly
   * @member {Scheduler.view.TimelineBase[]} partners
   * @category Time axis
   */
  get partners() {
    const partners = this.partner ? [this.partner] : [];
    if (this.partneredWith) {
      partners.push.apply(partners, this.partneredWith.allValues);
    }
    return partners;
  }
  get timeAxisColumn() {
    return this.columns && this._timeAxisColumn;
  }
  changeColumns(columns, currentStore) {
    const me = this;
    let timeAxisColumnIndex, timeAxisColumnConfig;
    // No columns means destroy
    if (columns) {
      const isArray = Array.isArray(columns);
      let cols = columns;
      if (!isArray) {
        cols = columns.data;
      }
      timeAxisColumnIndex = cols && cols.length;
      cols.some((col, index) => {
        if (col.type === 'timeAxis') {
          timeAxisColumnIndex = index;
          timeAxisColumnConfig = ObjectHelper.assign(col, me.timeAxisColumn);
          return true;
        }
        return false;
      });
      if (me.isVertical) {
        cols = [ObjectHelper.assign({
          type: 'verticalTimeAxis'
        }, me.verticalTimeAxisColumn),
        // Make space for a regular TimeAxisColumn after the VerticalTimeAxisColumn
        cols[timeAxisColumnIndex]];
        timeAxisColumnIndex = 1;
      } else {
        // We're going to mutate this array which we do not own, so copy it first.
        cols = cols.slice();
      }
      // Fix up the timeAxisColumn config in place
      cols[timeAxisColumnIndex] = this._timeAxisColumn || {
        type: 'timeAxis',
        cellCls: me.timeCellCls,
        mode: me.mode,
        ...timeAxisColumnConfig
      };
      // If we are passed a raw array, or the Store we are passed is owned by another
      // Scheduler, pass the raw column data ro the Grid's changeColumns
      if (isArray || columns.isStore && columns.owner !== this) {
        columns = cols;
      } else {
        columns.data = cols;
      }
    }
    return super.changeColumns(columns, currentStore);
  }
  updateColumns(columns, was) {
    super.updateColumns(columns, was);
    // Extract the known columns by type. Sorting will have placed them into visual order.
    if (columns) {
      const me = this,
        timeAxisColumn = me._timeAxisColumn = me.columns.find(c => c.isTimeAxisColumn);
      if (me.isVertical) {
        me.verticalTimeAxisColumn = me.columns.find(c => c.isVerticalTimeAxisColumn);
        me.verticalTimeAxisColumn.relayAll(me);
      }
      // Set up event relaying early
      timeAxisColumn.relayAll(me);
    }
  }
  onColumnsChanged({
    action,
    changes,
    record: column,
    records
  }) {
    var _this$partneredWith2;
    const {
      timeAxisColumn,
      columns
    } = this;
    // If someone replaces the column set (syncing leads to batch), ensure time axis is always added
    if ((action === 'dataset' || action === 'batch') && !columns.includes(timeAxisColumn)) {
      columns.add(timeAxisColumn, true);
    } else if (column === timeAxisColumn && 'width' in changes) {
      this.updateCanvasSize();
    }
    column && ((_this$partneredWith2 = this.partneredWith) === null || _this$partneredWith2 === void 0 ? void 0 : _this$partneredWith2.forEach(partner => {
      const partnerColumn = partner.columns.getAt(column.allIndex);
      if (partnerColumn !== null && partnerColumn !== void 0 && partnerColumn.shouldSync(column)) {
        const partnerChanges = {};
        for (const k in changes) {
          partnerChanges[k] = changes[k].value;
        }
        partnerColumn.set(partnerChanges);
      }
    }));
    super.onColumnsChanged(...arguments);
  }
  get timeView() {
    var _me$verticalTimeAxisC, _me$timeAxisColumn;
    const me = this;
    // Maintainer, we need to ensure that the columns property is initialized
    // if this getter is called at configuration time before columns have been ingested.
    return me.columns && me.isVertical ? (_me$verticalTimeAxisC = me.verticalTimeAxisColumn) === null || _me$verticalTimeAxisC === void 0 ? void 0 : _me$verticalTimeAxisC.view : (_me$timeAxisColumn = me.timeAxisColumn) === null || _me$timeAxisColumn === void 0 ? void 0 : _me$timeAxisColumn.timeAxisView;
  }
  updateEventCls(eventCls) {
    const me = this;
    if (!me.eventSelector) {
      // No difference with new rendering, released have 'b-released' only
      me.unreleasedEventSelector = me.eventSelector = `.${eventCls}-wrap`;
    }
    if (!me.eventInnerSelector) {
      me.eventInnerSelector = `.${eventCls}`;
    }
  }
  set timeAxisViewModel(timeAxisViewModel) {
    var _timeAxisViewModel;
    const me = this,
      currentModel = me._timeAxisViewModel,
      tavmListeners = {
        name: 'timeAxisViewModel',
        update: 'onTimeAxisViewModelUpdate',
        prio: 100,
        thisObj: me
      };
    if (me.partner && !timeAxisViewModel || currentModel && currentModel === timeAxisViewModel) {
      return;
    }
    if ((currentModel === null || currentModel === void 0 ? void 0 : currentModel.owner) === me) {
      // We created this model, destroy it
      currentModel.destroy();
    }
    me.detachListeners('timeAxisViewModel');
    // Getting rid of instanceof check to allow using code from different bundles
    if ((_timeAxisViewModel = timeAxisViewModel) !== null && _timeAxisViewModel !== void 0 && _timeAxisViewModel.isTimeAxisViewModel) {
      timeAxisViewModel.ion(tavmListeners);
    } else {
      timeAxisViewModel = TimeAxisViewModel.new({
        mode: me._mode,
        snap: me.snap,
        forceFit: me.forceFit,
        timeAxis: me.timeAxis,
        suppressFit: me.suppressFit,
        internalListeners: tavmListeners,
        owner: me
      }, timeAxisViewModel);
    }
    // Replace in dependent classes relying on the model
    if (!me.isConfiguring) {
      if (me.isHorizontal) {
        me.timeAxisColumn.timeAxisViewModel = timeAxisViewModel;
      } else {
        me.verticalTimeAxisColumn.view.model = timeAxisViewModel;
      }
    }
    me._timeAxisViewModel = timeAxisViewModel;
    me.relayEvents(timeAxisViewModel, ['update'], 'timeAxisViewModel');
    if (currentModel && timeAxisViewModel) {
      me.trigger('timeAxisViewModelChange', {
        timeAxisViewModel
      });
    }
  }
  /**
   * The internal view model, describing the visual representation of the time axis.
   * @property {Scheduler.view.model.TimeAxisViewModel}
   * @readonly
   * @category Time axis
   */
  get timeAxisViewModel() {
    if (!this._timeAxisViewModel) {
      this.timeAxisViewModel = null;
    }
    return this._timeAxisViewModel;
  }
  get suppressFit() {
    var _this$_timeAxisViewMo;
    return ((_this$_timeAxisViewMo = this._timeAxisViewModel) === null || _this$_timeAxisViewMo === void 0 ? void 0 : _this$_timeAxisViewMo.suppressFit) ?? this._suppressFit;
  }
  set suppressFit(value) {
    if (this._timeAxisViewModel) {
      this.timeAxisViewModel.suppressFit = value;
    } else {
      this._suppressFit = value;
    }
  }
  set timeAxis(timeAxis) {
    var _timeAxis;
    const me = this,
      currentTimeAxis = me._timeAxis,
      timeAxisListeners = {
        name: 'timeAxis',
        reconfigure: 'onTimeAxisReconfigure',
        thisObj: me
      };
    if (me.partner && !timeAxis || currentTimeAxis && currentTimeAxis === timeAxis) {
      return;
    }
    if (currentTimeAxis) {
      if (currentTimeAxis.owner === me) {
        // We created this model, destroy it
        currentTimeAxis.destroy();
      }
    }
    me.detachListeners('timeAxis');
    // Getting rid of instanceof check to allow using code from different bundles
    if (!((_timeAxis = timeAxis) !== null && _timeAxis !== void 0 && _timeAxis.isTimeAxis)) {
      timeAxis = ObjectHelper.assign({
        owner: me,
        viewPreset: me.viewPreset,
        autoAdjust: me.autoAdjustTimeAxis,
        weekStartDay: me.weekStartDay,
        forceFullTicks: me.fillTicks && me.snap
      }, timeAxis);
      if (me.startDate) {
        timeAxis.startDate = me.startDate;
      }
      if (me.endDate) {
        timeAxis.endDate = me.endDate;
      }
      if (me.workingTime) {
        me.applyWorkingTime(timeAxis);
      }
      timeAxis = new TimeAxis(timeAxis);
    }
    // Inform about reconfiguring the timeaxis, to allow users to react to start & end date changes
    timeAxis.ion(timeAxisListeners);
    me._timeAxis = timeAxis;
  }
  onTimeAxisReconfigure({
    config,
    oldConfig
  }) {
    if (config) {
      const dateRangeChange = !oldConfig || oldConfig.startDate - config.startDate || oldConfig.endDate - config.endDate;
      if (dateRangeChange) {
        /**
         * Fired when the range of dates encapsulated by the UI changes. This will be when
         * moving a view in time by reconfiguring its {@link #config-timeAxis}. This will happen
         * when zooming, or changing {@link #config-viewPreset}.
         *
         * Contrast this with the {@link #event-visibleDateRangeChange} event which fires much
         * more frequently, during scrolling along the time axis and changing the __visible__
         * date range.
         * @event dateRangeChange
         * @param {Scheduler.view.TimelineBase} source This Scheduler/Gantt instance.
         * @param {Object} old The old date range
         * @param {Date} old.startDate the old start date.
         * @param {Date} old.endDate the old end date.
         * @param {Object} new The new date range
         * @param {Date} new.startDate the new start date.
         * @param {Date} new.endDate the new end date.
         */
        this.trigger('dateRangeChange', {
          old: {
            startDate: oldConfig.startDate,
            endDate: oldConfig.endDate
          },
          new: {
            startDate: config.startDate,
            endDate: config.endDate
          }
        });
      }
    }
    /**
     * Fired when the timeaxis has changed, for example by zooming or configuring a new time span.
     * @event timeAxisChange
     * @param {Scheduler.view.Scheduler} source - This Scheduler
     * @param {Object} config Config object used to reconfigure the time axis.
     * @param {Date} config.startDate New start date (if supplied)
     * @param {Date} config.endDate New end date (if supplied)
     */
    this.trigger('timeAxisChange', {
      config
    });
  }
  get timeAxis() {
    if (!this._timeAxis) {
      this.timeAxis = null;
    }
    return this._timeAxis;
  }
  updateForceFit(value) {
    if (this._timeAxisViewModel) {
      this._timeAxisViewModel.forceFit = value;
    }
  }
  /**
   * Get/set working time. Assign `null` to stop using working time. See {@link #config-workingTime} config for details.
   * @property {Object}
   * @category Scheduled events
   */
  set workingTime(config) {
    this._workingTime = config;
    if (!this.isConfiguring) {
      this.applyWorkingTime(this.timeAxis);
    }
  }
  get workingTime() {
    return this._workingTime;
  }
  // Translates the workingTime configs into TimeAxis#include rules, applies them and then refreshes the header and
  // redraws the events
  applyWorkingTime(timeAxis) {
    const me = this,
      config = me._workingTime;
    if (config) {
      let hour = null;
      // Only use valid values
      if (config.fromHour >= 0 && config.fromHour < 24 && config.toHour > config.fromHour && config.toHour <= 24 && config.toHour - config.fromHour < 24) {
        hour = {
          from: config.fromHour,
          to: config.toHour
        };
      }
      let day = null;
      // Only use valid values
      if (config.fromDay >= 0 && config.fromDay < 7 && config.toDay > config.fromDay && config.toDay <= 7 && config.toDay - config.fromDay < 7) {
        day = {
          from: config.fromDay,
          to: config.toDay
        };
      }
      if (hour || day) {
        timeAxis.include = {
          hour,
          day
        };
      } else {
        // No valid rules, restore timeAxis
        timeAxis.include = null;
      }
    } else {
      // No rules, restore timeAxis
      timeAxis.include = null;
    }
    if (me.isPainted) {
      var _me$features$columnLi;
      // Refreshing header, which also recalculate tickSize and header data
      me.timeAxisColumn.refreshHeader();
      // Update column lines
      (_me$features$columnLi = me.features.columnLines) === null || _me$features$columnLi === void 0 ? void 0 : _me$features$columnLi.refresh();
      // Animate event changes
      me.refreshWithTransition();
    }
  }
  updateStartDate(date) {
    this.setStartDate(date);
  }
  /**
   * Sets the timeline start date.
   *
   * **Note:**
   * - If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
   * - If keepDuration is false and new start date is greater than end date, it will throw an exception.
   *
   * @param {Date} date The new start date
   * @param {Boolean} keepDuration Pass `true` to keep the duration of the timeline ("move" the timeline),
   * `false` to change the duration ("resize" the timeline). Defaults to `true`.
   */
  setStartDate(date, keepDuration = true) {
    const me = this,
      ta = me._timeAxis,
      {
        startDate,
        endDate,
        mainUnit
      } = ta || emptyObject$2;
    if (typeof date === 'string') {
      date = DateHelper.parse(date);
    }
    if (ta && endDate) {
      if (date) {
        let calcEndDate = endDate;
        if (keepDuration && startDate) {
          const diff = DateHelper.diff(startDate, endDate, mainUnit, true);
          calcEndDate = DateHelper.add(date, diff, mainUnit);
        }
        me.setTimeSpan(date, calcEndDate);
      }
    } else {
      me._tempStartDate = date;
    }
  }
  get startDate() {
    var _this$_timeAxis;
    let ret = ((_this$_timeAxis = this._timeAxis) === null || _this$_timeAxis === void 0 ? void 0 : _this$_timeAxis.startDate) || this._tempStartDate;
    if (!ret) {
      ret = new Date();
      const {
        workingTime
      } = this;
      if (workingTime) {
        while (!isWorkingTime(ret, workingTime)) {
          ret.setHours(ret.getHours() + 1);
        }
      }
      this._tempStartDate = ret;
    }
    return ret;
  }
  changeEndDate(date) {
    if (typeof date === 'string') {
      date = DateHelper.parse(date);
    }
    this.setEndDate(date);
  }
  /**
   * Sets the timeline end date
   *
   * **Note:**
   * - If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
   * - If keepDuration is false and new end date is less than start date, it will throw an exception.
   *
   * @param {Date} date The new end date
   * @param {Boolean} keepDuration Pass `true` to keep the duration of the timeline ("move" the timeline),
   * `false` to change the duration ("resize" the timeline). Defaults to `false`.
   */
  setEndDate(date, keepDuration = false) {
    const me = this,
      ta = me._timeAxis,
      {
        startDate,
        endDate,
        mainUnit
      } = ta || emptyObject$2;
    if (typeof date === 'string') {
      date = DateHelper.parse(date);
    }
    if (ta && startDate) {
      if (date) {
        let calcStartDate = startDate;
        if (keepDuration && endDate) {
          const diff = DateHelper.diff(startDate, endDate, mainUnit, true);
          calcStartDate = DateHelper.add(date, -diff, mainUnit);
        }
        me.setTimeSpan(calcStartDate, date);
      }
    } else {
      me._tempEndDate = date;
    }
  }
  get endDate() {
    const me = this;
    if (me._timeAxis) {
      return me._timeAxis.endDate;
    }
    return me._tempEndDate || DateHelper.add(me.startDate, me.viewPreset.defaultSpan, me.viewPreset.mainHeader.unit);
  }
  changeVisibleDate(options) {
    if (options instanceof Date) {
      return {
        date: options,
        block: 'nearest'
      };
    }
    if (options instanceof Object) {
      return {
        date: options.date,
        ...options
      };
    }
  }
  updateVisibleDate(options) {
    const me = this;
    // Infinite scroll initialization takes care of its visibleDate after
    // calculating the optimum scroll range in TimelineScroll#initScroll
    if (!(me.infiniteScroll && me.isConfiguring)) {
      if (me.isPainted) {
        me.scrollToDate(options.date, options);
      } else {
        me.ion({
          paint: () => me.scrollToDate(options.date, options),
          once: true
        });
      }
    }
  }
  get features() {
    return super.features;
  }
  // add region resize by default
  set features(features) {
    features = features === true ? {} : features;
    if (!('regionResize' in features)) {
      features.regionResize = true;
    }
    super.features = features;
  }
  //endregion
  //region Event handlers
  onLocaleChange() {
    super.onLocaleChange();
    const oldAutoAdjust = this.timeAxis.autoAdjust;
    // Time axis should rebuild as weekStartDay may have changed
    this.timeAxis.reconfigure({
      autoAdjust: false
    });
    // Silently set it back to what the user had for next view refresh
    this.timeAxis.autoAdjust = oldAutoAdjust;
  }
  /**
   * Called when the element which encapsulates the Scheduler's visible height changes size.
   * We only respond to *height* changes here. The TimeAxisSubGrid monitors its own width.
   * @param {HTMLElement} element
   * @param {DOMRect} oldRect
   * @param {DOMRect} newRect
   * @private
   */
  onBodyResize(element, oldRect, {
    width,
    height
  }) {
    // Uncache old value upon element resize, not upon initial sizing
    if (this.isVertical && oldRect && width !== oldRect.width) {
      delete this.timeAxisSubGrid._width;
    }
    const newWidth = this.timeAxisSubGrid.element.offsetWidth;
    // The Scheduler (The Grid) dictates the viewport height.
    // Don't react on first invocation which will be initial size.
    if (this._bodyRectangle && oldRect && height !== oldRect.height) {
      this.onSchedulerViewportResize(newWidth, height, newWidth, oldRect.height);
    }
  }
  onSchedulerViewportResize(width, height, oldWidth, oldHeight) {
    if (this.isPainted) {
      const me = this,
        {
          isHorizontal,
          partneredWith
        } = me;
      me.currentOrientation.onViewportResize(width, height, oldWidth, oldHeight);
      // Raw width is always correct for horizontal layout because the TimeAxisSubGrid
      // never shows a scrollbar. It's always contained by an owning Grid which shows
      // the vertical scrollbar.
      me.updateViewModelAvailableSpace(isHorizontal ? width : Math.floor(height));
      if (partneredWith && !me.isSyncingFromPartner) {
        me.syncPartnerSubGrids();
      }
      /**
       * Fired when the *scheduler* viewport (not the overall Scheduler element) changes size.
       * This happens when the grid changes height, or when the subgrid which encapsulates the
       * scheduler column changes width.
       * @event timelineViewportResize
       * @param {Core.widget.Widget} source - This Scheduler
       * @param {Number} width The new width
       * @param {Number} height The new height
       * @param {Number} oldWidth The old width
       * @param {Number} oldHeight The old height
       */
      me.trigger('timelineViewportResize', {
        width,
        height,
        oldWidth,
        oldHeight
      });
    }
  }
  updateViewModelAvailableSpace(space) {
    this.timeAxisViewModel.availableSpace = space;
  }
  onTimeAxisViewModelUpdate() {
    if (!this._viewPresetChanging && !this.timeAxisSubGrid.collapsed) {
      this.updateCanvasSize();
      this.currentOrientation.onTimeAxisViewModelUpdate();
    }
  }
  syncPartnerSubGrids() {
    this.partneredWith.forEach(partner => {
      if (!partner.isSyncingFromPartner) {
        partner.isSyncingFromPartner = true;
        this.eachSubGrid(subGrid => {
          const partnerSubGrid = partner.subGrids[subGrid.region];
          // If there is a difference, sync the partner SubGrid state
          if (partnerSubGrid.width !== subGrid.width) {
            if (subGrid.collapsed) {
              partnerSubGrid.collapse();
            } else {
              if (partnerSubGrid.collapsed) {
                partnerSubGrid.expand();
              }
              // When using flexed subgrid, make sure flex values has prio over width
              if (subGrid.flex) {
                // If flex values match, resize should be fine without changing anything
                if (subGrid.flex !== partnerSubGrid.flex) {
                  partnerSubGrid.flex = subGrid.flex;
                }
              } else {
                partnerSubGrid.width = subGrid.width;
              }
            }
          }
        });
        partner.isSyncingFromPartner = false;
      }
    });
  }
  //endregion
  //region Mode
  get currentOrientation() {
    throw new Error('Implement in subclass');
  }
  // Horizontal is the default, overridden in scheduler
  get isHorizontal() {
    return true;
  }
  //endregion
  //region Canvases and elements
  get backgroundCanvas() {
    return this._backgroundCanvas;
  }
  get foregroundCanvas() {
    return this._foregroundCanvas;
  }
  get svgCanvas() {
    const me = this;
    if (!me._svgCanvas) {
      const svg = me._svgCanvas = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
      svg.setAttribute('id', IdHelper.generateId('svg'));
      // To not be recycled by DomSync
      svg.retainElement = true;
      me.foregroundCanvas.appendChild(svg);
      me.trigger('svgCanvasCreated', {
        svg
      });
    }
    return me._svgCanvas;
  }
  /**
   * Returns the subGrid containing the time axis
   * @member {Grid.view.SubGrid} timeAxisSubGrid
   * @readonly
   * @category Time axis
   */
  /**
   * Returns the html element for the subGrid containing the time axis
   * @property {HTMLElement}
   * @readonly
   * @category Time axis
   */
  get timeAxisSubGridElement() {
    // Hit a lot, caching the element (it will never change)
    if (!this._timeAxisSubGridElement) {
      var _this$timeAxisColumn;
      // We need the TimeAxisSubGrid to exist, so regions must be initialized
      this.getConfig('regions');
      this._timeAxisSubGridElement = (_this$timeAxisColumn = this.timeAxisColumn) === null || _this$timeAxisColumn === void 0 ? void 0 : _this$timeAxisColumn.subGridElement;
    }
    return this._timeAxisSubGridElement;
  }
  updateCanvasSize() {
    const me = this,
      {
        totalSize
      } = me.timeAxisViewModel,
      width = me.isHorizontal ? totalSize : me.timeAxisColumn.width;
    let result = false;
    if (me.isVertical) {
      // Ensure vertical scroll range accommodates the TimeAxis
      if (me.isPainted) {
        // We used to have a bug here from not including the row border in the total height. Border is now
        // removed, but leaving code here just in case some client is using border
        me.refreshTotalHeight(totalSize + me._rowBorderHeight, true);
      }
      // Canvas might need a height in vertical mode, if ticks does not fill height (suppressFit : true)
      if (me.suppressFit) {
        DomHelper.setLength(me.foregroundCanvas, 'height', totalSize);
      }
      result = true;
    }
    if (width !== me.$canvasWidth && me.foregroundCanvas) {
      if (me.backgroundCanvas) {
        DomHelper.setLength(me.backgroundCanvas, 'width', width);
      }
      DomHelper.setLength(me.foregroundCanvas, 'width', width);
      me.$canvasWidth = width;
      result = true;
    }
    return result;
  }
  /**
   * A chainable function which Features may hook to add their own content to the timeaxis header.
   * @param {Array} configs An array of domConfigs, append to it to have the config applied to the header
   */
  getHeaderDomConfigs(configs) {}
  /**
   * A chainable function which Features may hook to add their own content to the foreground canvas
   * @param {Array} configs An array of domConfigs, append to it to have the config applied to the foreground canvas
   */
  getForegroundDomConfigs(configs) {}
  //endregion
  //region Grid overrides
  async onStoreDataChange({
    action
  }) {
    const me = this;
    // Only update the UI immediately if we are visible
    if (me.isVisible) {
      var _me$project;
      // When repopulating stores (pro and up on data reload), the engine is not in a valid state until committed.
      // Don't want to commit here, since it might be repopulating multiple stores.
      // Instead delay grids refresh until project is ready
      if (action === 'dataset' && (_me$project = me.project) !== null && _me$project !== void 0 && _me$project.isRepopulatingStores) {
        await me.project.await('refresh', false);
      }
      super.onStoreDataChange(...arguments);
    }
    // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
    else {
      me.whenVisible('refresh', me, [true]);
    }
  }
  refresh(forceLayout = true) {
    const me = this;
    if (me.isPainted && !me.refreshSuspended) {
      // We need to refresh if there are Features laying claim to the visible time axis.
      // Or there are events which fall inside the time axis.
      // Or (if no events fall inside the time axis) there are event elements to remove.
      if (me.isVertical || me.hasVisibleEvents || me.timeAxisSubGridElement.querySelector(me.eventSelector)) {
        if (!me.project || me.isEngineReady) {
          me.refreshRows(false, forceLayout);
        } else {
          me.refreshAfterProjectRefresh = true;
          me.currentOrientation.refreshAllWhenReady = true;
        }
      }
      // Even if there are no events in our timeline, Features
      // assume there will be a refresh event from the RowManager
      // after a refresh request so fire it here.
      else {
        me.rowManager.trigger('refresh');
      }
    }
  }
  render() {
    const me = this,
      schedulerEl = me.timeAxisSubGridElement;
    if (me.useBackgroundCanvas) {
      me._backgroundCanvas = DomHelper.createElement({
        className: 'b-sch-background-canvas',
        parent: schedulerEl,
        nextSibling: schedulerEl.firstElementChild
      });
    }
    // The font-size trick is no longer used by scheduler, since it allows per resource margins
    const fgCanvas = me._foregroundCanvas = DomHelper.createElement({
      className: 'b-sch-foreground-canvas',
      style: `font-size:${me.rowHeight - me.resourceMargin * 2}px`,
      parent: schedulerEl
    });
    me.timeAxisSubGrid.insertRowsBefore = fgCanvas;
    // Size correctly in case ticks does not fill height
    if (me.isVertical && me.suppressFit) {
      me.updateCanvasSize();
    }
    super.render(...arguments);
  }
  refreshRows(returnToTop = false, reLayoutEvents = true) {
    const me = this;
    if (me.isConfiguring) {
      return;
    }
    me.currentOrientation.refreshRows(reLayoutEvents);
    super.refreshRows(returnToTop);
  }
  updateHideHeaders(hide) {
    const me = this,
      scrollLeft = me.isPainted ? me.scrollLeft : 0;
    super.updateHideHeaders(hide);
    if (me.isPainted) {
      if (!hide) {
        me.timeAxisColumn.refreshHeader(null, true);
      }
      me.nextAnimationFrame().then(() => me.scrollLeft = scrollLeft);
    }
  }
  getCellDataFromEvent(event, includeSingleAxisMatch) {
    if (includeSingleAxisMatch) {
      includeSingleAxisMatch = !Boolean(event.target.closest('.b-sch-foreground-canvas'));
    }
    return super.getCellDataFromEvent(event, includeSingleAxisMatch);
  }
  // This GridSelection override disables drag-selection in timeaxis column for scheduler and gantt
  onCellNavigate(me, from, to) {
    var _to$cell, _GlobalEvents$current;
    if ((_to$cell = to.cell) !== null && _to$cell !== void 0 && _to$cell.classList.contains('b-timeaxis-cell') && !((_GlobalEvents$current = GlobalEvents.currentMouseDown) !== null && _GlobalEvents$current !== void 0 && _GlobalEvents$current.target.classList.contains('b-grid-cell'))) {
      this.preventDragSelect = true;
    }
    super.onCellNavigate(...arguments);
  }
  //endregion
  //region Other
  // duration = false prevents transition
  runWithTransition(fn, duration) {
    const me = this;
    // Do not attempt to enter animating state if we are not visible
    if (me.isVisible) {
      // Allow calling with true/false to keep code simpler in other places
      if (duration == null || duration === true) {
        duration = me.transitionDuration;
      }
      // Ask Grid superclass to enter the animated state if requested and enabled.
      if (duration && me.enableEventAnimations) {
        if (!me.hasTimeout('exitTransition')) {
          me.isAnimating = true;
        }
        // Exit animating state in duration milliseconds.
        exitTransition.delay = duration;
        me.setTimeout(exitTransition);
      }
    }
    fn();
  }
  exitTransition() {
    this.isAnimating = false;
    this.trigger('transitionend');
  }
  // Awaited by CellEdit to make sure that the editor is not moved until row heights have transitioned, to avoid it
  // ending up misaligned
  async waitForAnimations() {
    // If project is calculating, we should await that too. It might lead to transitions
    if (!this.isEngineReady && this.project) {
      await this.project.await('dataReady', false);
    }
    await super.waitForAnimations();
  }
  /**
   * Refreshes the grid with transitions enabled.
   */
  refreshWithTransition(forceLayout, duration) {
    const me = this;
    // No point in starting a transition if we cant refresh anyway
    if (!me.refreshSuspended && me.isPainted) {
      // Since we suspend refresh when loading with CrudManager, rows might not have been initialized yet
      if (!me.rowManager.topRow) {
        me.rowManager.reinitialize();
      } else {
        me.runWithTransition(() => me.refresh(forceLayout), duration);
      }
    }
  }
  /**
   * Returns an object representing the visible date range
   * @property {Object}
   * @property {Date} visibleDateRange.startDate
   * @property {Date} visibleDateRange.endDate
   * @readonly
   * @category Dates
   */
  get visibleDateRange() {
    return this.currentOrientation.visibleDateRange;
  }
  // This override will force row selection on timeaxis column selection, effectively disabling cell selection there
  isRowNumberSelecting(...selectors) {
    return super.isRowNumberSelecting(...selectors) || selectors.some(cs => {
      var _cs$cell;
      return cs.column ? cs.column.isTimeAxisColumn : (_cs$cell = cs.cell) === null || _cs$cell === void 0 ? void 0 : _cs$cell.closest('.b-timeaxis-cell');
    });
  }
  //endregion
  /**
   * Returns a rounded duration value to be displayed in UI (tooltips, labels etc)
   * @param {Number} duration The raw duration value
   * @param {Number} [nbrDecimals] The number of decimals, defaults to {@link #config-durationDisplayPrecision}
   * @returns {Number} The rounded duration
   */
  formatDuration(duration, nbrDecimals = this.durationDisplayPrecision) {
    const multiplier = Math.pow(10, nbrDecimals);
    return Math.round(duration * multiplier) / multiplier;
  }
  beginListeningForBatchedUpdates() {
    var _this$syncSplits;
    this.listenToBatchedUpdates = (this.listenToBatchedUpdates || 0) + 1;
    // Allow live resizing (etc) in all splits
    (_this$syncSplits = this.syncSplits) === null || _this$syncSplits === void 0 ? void 0 : _this$syncSplits.call(this, other => other.beginListeningForBatchedUpdates());
  }
  endListeningForBatchedUpdates() {
    var _this$syncSplits2;
    if (this.listenToBatchedUpdates) {
      this.listenToBatchedUpdates -= 1;
    }
    (_this$syncSplits2 = this.syncSplits) === null || _this$syncSplits2 === void 0 ? void 0 : _this$syncSplits2.call(this, other => other.endListeningForBatchedUpdates());
  }
  onConnectedCallback(connected, initialConnect) {
    if (connected && !initialConnect) {
      this.timeAxisSubGrid.scrollable.x += 0.5;
    }
  }
  updateRtl(rtl) {
    const me = this,
      {
        isConfiguring
      } = me;
    let visibleDateRange;
    if (!isConfiguring) {
      visibleDateRange = me.visibleDateRange;
    }
    super.updateRtl(rtl);
    if (!isConfiguring) {
      me.currentOrientation.clearAll();
      if (me.infiniteScroll) {
        me.shiftToDate(visibleDateRange.startDate);
        me.scrollToDate(visibleDateRange.startDate, {
          block: 'start'
        });
      } else {
        me.timelineScroller.position += 0.5;
      }
    }
  }
  /**
   * Applies the start and end date to each event store request (formatted in the same way as the start date field,
   * defined in the EventStore Model class).
   * @category Data
   * @private
   */
  applyStartEndParameters(params) {
    const me = this,
      field = me.eventStore.modelClass.fieldMap.startDate;
    if (me.passStartEndParameters) {
      params[me.startParamName] = field.print(me.startDate);
      params[me.endParamName] = field.print(me.endDate);
    }
  }
}
// Register this widget type with its Factory
TimelineBase.initClass();
// Has to be here because Gantt extends TimelineBase
VersionHelper.setVersion('scheduler', '5.5.0');
TimelineBase._$name = 'TimelineBase';

/**
 * @module Scheduler/crud/AbstractCrudManager
 */
/**
 * @typedef {Object} CrudManagerStoreDescriptor
 * @property {String} storeId Unique store identifier. Store related requests/responses will be sent under this name.
 * @property {Core.data.Store} store The store itself.
 * @property {String} [phantomIdField] Set this if the store model has a predefined field to keep phantom record identifier.
 * @property {String} [idField] id field name, if it's not specified then class will try to get it from store model.
 * @property {Boolean} [writeAllFields] Set to true to write all fields from modified records
 */
/**
 * This is an abstract class serving as the base for the {@link Scheduler.data.CrudManager} class.
 * It implements basic mechanisms to organize batch communication with a server.
 * Yet it does not contain methods related to _data transfer_ nor _encoding_.
 * These methods are to be provided in sub-classes by consuming the appropriate mixins.
 *
 * For example, this is how the class can be used to implement an JSON encoding system:
 *
 * ```javascript
 * // let's make new CrudManager using AJAX as a transport system and JSON for encoding
 * class MyCrudManager extends JsonEncode(AjaxTransport(AbstractCrudManager)) {
 *
 * }
 * ```
 *
 * ## Data transfer and encoding methods
 *
 * These are methods that must be provided by subclasses of this class:
 *
 * - [#sendRequest](#Scheduler/crud/AbstractCrudManagerMixin#function-sendRequest)
 * - [#cancelRequest](#Scheduler/crud/AbstractCrudManagerMixin#function-cancelRequest)
 * - [#encode](#Scheduler/crud/AbstractCrudManagerMixin#function-encode)
 * - [#decode](#Scheduler/crud/AbstractCrudManagerMixin#function-decode)
 *
 * @extends Core/Base
 * @mixes Scheduler/crud/AbstractCrudManagerMixin
 * @abstract
 */
class AbstractCrudManager extends Base.mixin(AbstractCrudManagerMixin) {
  //region Default config
  /**
   * The server revision stamp.
   * The _revision stamp_ is a number which should be incremented after each server-side change.
   * This property reflects the current version of the data retrieved from the server and gets updated after each
   * {@link Scheduler/crud/AbstractCrudManagerMixin#function-load} and {@link Scheduler/crud/AbstractCrudManagerMixin#function-sync} call.
   * @property {Number}
   * @readonly
   */
  get revision() {
    return this.crudRevision;
  }
  set revision(value) {
    this.crudRevision = value;
  }
  /**
   * Get or set data of {@link #property-crudStores} as a JSON string.
   *
   * Get a JSON string:
   * ```javascript
   *
   * const jsonString = scheduler.crudManager.json;
   *
   * // returned jsonString:
   * '{"eventsData":[...],"resourcesData":[...],...}'
   *
   * // object representation of the returned jsonString:
   * {
   *     resourcesData    : [...],
   *     eventsData       : [...],
   *     assignmentsData  : [...],
   *     dependenciesData : [...],
   *     timeRangesData   : [...],
   *     // data from other stores
   * }
   * ```
   *
   * Set a JSON string (to populate the CrudManager stores):
   *
   * ```javascript
   * scheduler.crudManager.json = '{"eventsData":[...],"resourcesData":[...],...}'
   * ```
   *
   * @property {String}
   */
  get json() {
    return StringHelper.safeJsonStringify(this);
  }
  set json(json) {
    if (typeof json === 'string') {
      json = StringHelper.safeJsonParse(json);
    }
    this.forEachCrudStore(store => {
      const dataName = `${store.storeId}Data`;
      if (json[dataName]) {
        store.data = json[dataName];
      }
    });
  }
  static get defaultConfig() {
    return {
      /**
       * Sets the list of stores controlled by the CRUD manager.
       *
       * When adding a store to the CrudManager, make sure the server response format is correct for `load` and `sync` requests.
       * Learn more in the [Working with data](#Scheduler/guides/data/crud_manager.md#loading-data) guide.
       *
       * Store can be provided as in instance, using its `storeId` or as an {@link #typedef-CrudManagerStoreDescriptor}
       * object.
       * @config {Core.data.Store[]|String[]|CrudManagerStoreDescriptor[]}
       */
      stores: null
      /**
       * Encodes request to the server.
       * @function encode
       * @param {Object} request The request to encode.
       * @returns {String} The encoded request.
       * @abstract
       */
      /**
       * Decodes response from the server.
       * @function decode
       * @param {String} response The response to decode.
       * @returns {Object} The decoded response.
       * @abstract
       */
    };
  }
  //endregion
  //region Init
  construct(config = {}) {
    if (config.stores) {
      config.crudStores = config.stores;
      delete config.stores;
    }
    super.construct(config);
  }
  //endregion
  //region inline data
  /**
   * Returns the data from all CrudManager `crudStores` in a format that can be consumed by `inlineData`.
   *
   * Used by JSON.stringify to correctly convert this CrudManager to json.
   *
   * The returned data is identical to what {@link Scheduler/crud/AbstractCrudManager#property-inlineData} contains.
   *
   * ```javascript
   *
   * const json = scheduler.crudManager.toJSON();
   *
   * // json:
   * {
   *     eventsData : [...],
   *     resourcesData : [...],
   *     dependenciesData : [...],
   *     assignmentsData : [...],
   *     timeRangesData : [...],
   *     resourceTimeRangesData : [...],
   *     // ... other stores data
   * }
   * ```
   *
   * Output can be consumed by `inlineData`.
   *
   * ```javascript
   * const json = scheduler.crudManager.toJSON();
   *
   * // Plug it back in later
   * scheduler.crudManager.inlineData = json;
   * ```
   *
   * @function toJSON
   * @returns {Object}
   * @category JSON
   */
  toJSON() {
    // Collect data from crudStores
    const result = {};
    this.forEachCrudStore((store, storeId) => result[`${storeId}Data`] = store.toJSON());
    return result;
  }
  /**
   * Get or set data of CrudManager stores. The returned data is identical to what
   * {@link Scheduler/crud/AbstractCrudManager#function-toJSON} returns:
   *
   * ```javascript
   *
   * const data = scheduler.crudManager.inlineData;
   *
   * // data:
   * {
   *     eventsData : [...],
   *     resourcesData : [...],
   *     dependenciesData : [...],
   *     assignmentsData : [...],
   *     timeRangesData : [...],
   *     resourceTimeRangesData : [...],
   *     ... other stores data
   * }
   *
   *
   * // Plug it back in later
   * scheduler.crudManager.inlineData = data;
   * ```
   *
   * @property {Object}
   */
  get inlineData() {
    return this.toJSON();
  }
  set inlineData(data) {
    this.json = data;
  }
  //endregion
  //region Store collection (add, remove, get & iterate)
  set stores(stores) {
    if (stores !== this.crudStores) {
      this.crudStores = stores;
    }
  }
  /**
   * A list of registered stores whose server communication will be collected into a single batch.
   * Each store is represented by a _store descriptor_.
   * @member {CrudManagerStoreDescriptor[]} stores
   */
  get stores() {
    return this.crudStores;
  }
  //endregion
  /**
   * Returns true if the crud manager is currently loading data
   * @property {Boolean}
   * @readonly
   * @category CRUD
   */
  get isLoading() {
    return this.isCrudManagerLoading;
  }
  /**
   * Adds a store to the collection.
   *
   *```javascript
   * // append stores to the end of collection
   * crudManager.addStore([
   *     store1,
   *     // storeId
   *     'bar',
   *     // store descriptor
   *     {
   *         storeId : 'foo',
   *         store   : store3
   *     },
   *     {
   *         storeId         : 'bar',
   *         store           : store4,
   *         // to write all fields of modified records
   *         writeAllFields  : true
   *     }
   * ]);
   *```
   *
   * **Note:** Order in which stores are kept in the collection is very essential sometimes.
   * Exactly in this order the loaded data will be put into each store.
   *
   * When adding a store to the CrudManager, make sure the server response format is correct for `load` and `sync`
   * requests. Learn more in the [Working with data](#Scheduler/guides/data/crud_manager.md#loading-data) guide.
   *
   * @param {Core.data.Store|String|CrudManagerStoreDescriptor|Core.data.Store[]|String[]|CrudManagerStoreDescriptor[]} store
   * A store or list of stores. Each store might be specified by its instance, `storeId` or _descriptor_.
   * @param {Number} [position] The relative position of the store. If `fromStore` is specified the position will be
   * taken relative to it.
   * If not specified then store(s) will be appended to the end of collection.
   * Otherwise, it will be an index in stores collection.
   *
   * ```javascript
   * // insert stores store4, store5 to the start of collection
   * crudManager.addStore([ store4, store5 ], 0);
   * ```
   *
   * @param {String|Core.data.Store|CrudManagerStoreDescriptor} [fromStore] The store relative to which position
   * should be calculated. Can be defined as a store identifier, instance or descriptor (the result of
   * {@link Scheduler/crud/AbstractCrudManagerMixin#function-getStoreDescriptor} call).
   *
   * ```javascript
   * // insert store6 just before a store having storeId equal to 'foo'
   * crudManager.addStore(store6, 0, 'foo');
   *
   * // insert store7 just after store3 store
   * crudManager.addStore(store7, 1, store3);
   * ```
   */
  addStore(...args) {
    return this.addCrudStore(...args);
  }
  removeStore(...args) {
    return this.removeCrudStore(...args);
  }
  getStore(...args) {
    return this.getCrudStore(...args);
  }
  hasChanges(...args) {
    return this.crudStoreHasChanges(...args);
  }
  loadData(...args) {
    return this.loadCrudManagerData(...args);
  }
}
AbstractCrudManager._$name = 'AbstractCrudManager';

/**
 * @module Scheduler/data/CrudManager
 */
/**
 * The Crud Manager (or "CM") is a class implementing centralized loading and saving of data in multiple stores.
 * Loading the stores and saving all changes is done using a single request. The stores managed by CRUD manager should
 * not be configured with their own CRUD URLs or use {@link Core/data/AjaxStore#config-autoLoad}/{@link Core/data/AjaxStore#config-autoCommit}.
 *
 * This class uses JSON as its data encoding format.
 *
 * ## Scheduler stores
 *
 * The class supports Scheduler specific stores (namely: resource, event, assignment and dependency stores).
 * For these stores, the CM has separate configs ({@link #config-resourceStore}, {@link #config-eventStore},
 * {@link #config-assignmentStore}) to register them.
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     autoLoad        : true,
 *     resourceStore   : resourceStore,
 *     eventStore      : eventStore,
 *     assignmentStore : assignmentStore,
 *     transport       : {
 *         load : {
 *             url : 'php/read.php'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 * ```
 *
 * ## AJAX request configuration
 *
 * To configure AJAX request parameters please take a look at the
 * {@link Scheduler/crud/transport/AjaxTransport} docs.
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     autoLoad        : true,
 *     resourceStore,
 *     eventStore,
 *     assignmentStore,
 *     transport       : {
 *         load    : {
 *             url         : 'php/read.php',
 *             // use GET request
 *             method      : 'GET',
 *             // pass request JSON in "rq" parameter
 *             paramName   : 'rq',
 *             // extra HTTP request parameters
 *             params      : {
 *                 foo     : 'bar'
 *             },
 *             // pass some extra Fetch API option
 *             credentials : 'include'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 * ```
 *
 * ## Using inline data
 *
 * The CrudManager provides settable property {@link #property-inlineData} that can
 * be used to get data from all {@link #property-crudStores} at once and to set this
 * data as well. Populating the stores this way can be useful if you cannot or you do not want to use CrudManager for
 * server requests but you pull the data by other means and have it ready outside CrudManager. Also, the data from all
 * stores is available in a single assignment statement.
 *
 * ### Getting data
 * ```javascript
 * const data = scheduler.crudManager.inlineData;
 *
 * // use the data in your application
 * ```
 *
 * ### Setting data
 * ```javascript
 * const data = // your function to pull server data
 *
 * scheduler.crudManager.inlineData = data;
 * ```
 *
 * ## Load order
 *
 * The CM is aware of the proper load order for Scheduler specific stores so you don't need to worry about it.
 * If you provide any extra stores (using {@link #config-stores} config) they will be
 * added to the start of collection before the Scheduler specific stores.
 * If you need a different loading order, you should use {@link #function-addStore} method to
 * register your store:
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     resourceStore   : resourceStore,
 *     eventStore      : eventStore,
 *     assignmentStore : assignmentStore,
 *     // extra user defined stores will get to the start of collection
 *     // so they will be loaded first
 *     stores          : [ store1, store2 ],
 *     transport       : {
 *         load : {
 *             url : 'php/read.php'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 *
 * // append store3 to the end so it will be loaded last
 * crudManager.addStore(store3);
 *
 * // now when we registered all the stores let's load them
 * crudManager.load();
 * ```
 *
 * ## Assignment store
 *
 * The Crud Manager is designed to use {@link Scheduler/data/AssignmentStore} for assigning events to one or multiple resources.
 * However if server provides `resourceId` for any of the `events` then the Crud Manager enables backward compatible mode when
 * an event could have a single assignment only. This also disables multiple assignments in Scheduler UI.
 * In order to use multiple assignments server backend should be able to receive/send `assignments` for `load` and `sync` requests.
 *
 * ## Project
 *
 * The Crud Manager automatically consumes stores of the provided project (namely its {@link Scheduler/model/ProjectModel#property-eventStore},
 * {@link Scheduler/model/ProjectModel#property-resourceStore}, {@link Scheduler/model/ProjectModel#property-assignmentStore},
 * {@link Scheduler/model/ProjectModel#property-dependencyStore}, {@link Scheduler/model/ProjectModel#property-timeRangeStore} and
 * {@link Scheduler/model/ProjectModel#property-resourceTimeRangeStore}):
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     // crud manager will get stores from myAppProject project
 *     project   : myAppProject,
 *     transport : {
 *         load : {
 *             url : 'php/read.php'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 * ```
 *
 * @mixes Scheduler/data/mixin/ProjectCrudManager
 * @mixes Scheduler/crud/encoder/JsonEncoder
 * @mixes Scheduler/crud/transport/AjaxTransport
 * @extends Scheduler/crud/AbstractCrudManager
 */
class CrudManager extends AbstractCrudManager.mixin(ProjectCrudManager, AjaxTransport, JsonEncoder) {
  static $name = 'CrudManager';
  //region Config
  static get defaultConfig() {
    return {
      projectClass: ProjectModel,
      resourceStoreClass: ResourceStore,
      eventStoreClass: EventStore,
      assignmentStoreClass: AssignmentStore,
      dependencyStoreClass: DependencyStore,
      /**
       * A store with resources (or a config object).
       * @config {Scheduler.data.ResourceStore|ResourceStoreConfig}
       */
      resourceStore: {},
      /**
       * A store with events (or a config object).
       *
       * ```
       * crudManager : {
       *      eventStore {
       *          storeClass : MyEventStore
       *      }
       * }
       * ```
       * @config {Scheduler.data.EventStore|EventStoreConfig}
       */
      eventStore: {},
      /**
       * A store with assignments (or a config object).
       * @config {Scheduler.data.AssignmentStore|AssignmentStoreConfig}
       */
      assignmentStore: {},
      /**
       * A store with dependencies(or a config object).
       * @config {Scheduler.data.DependencyStore|DependencyStoreConfig}
       */
      dependencyStore: {},
      /**
       * A project that holds and links stores
       * @config {Scheduler.model.ProjectModel}
       */
      project: null
    };
  }
  //endregion
  buildProject() {
    return new this.projectClass(this.buildProjectConfig());
  }
  buildProjectConfig() {
    return ObjectHelper.cleanupProperties({
      eventStore: this.eventStore,
      resourceStore: this.resourceStore,
      assignmentStore: this.assignmentStore,
      dependencyStore: this.dependencyStore,
      resourceTimeRangeStore: this.resourceTimeRangeStore
    });
  }
  //region Stores
  set project(project) {
    const me = this;
    if (project !== me._project) {
      me.detachListeners('beforeDataReady');
      me.detachListeners('afterDataReady');
      me._project = project;
      if (project) {
        me.eventStore = project.eventStore;
        me.resourceStore = project.resourceStore;
        me.assignmentStore = project.assignmentStore;
        me.dependencyStore = project.dependencyStore;
        me.timeRangeStore = project.timeRangeStore;
        me.resourceTimeRangeStore = project.resourceTimeRangeStore;
        // When adding multiple events to the store it will trigger multiple change events each of which will
        // call crudManager.hasChanges, which will try to actually get the changeset package. It takes some time
        // and we better skip that part for the dataready event, suspending changes tracking.
        project.ion({
          name: 'beforeDataReady',
          dataReady: () => me.suspendChangesTracking(),
          prio: 100,
          thisObj: me
        });
        project.ion({
          name: 'afterDataReady',
          dataReady: () => me.resumeChangesTracking(),
          prio: -100,
          thisObj: me
        });
      }
      if (!me.eventStore) {
        me.eventStore = {};
      }
      if (!me.resourceStore) {
        me.resourceStore = {};
      }
      if (!me.assignmentStore) {
        me.assignmentStore = {};
      }
      if (!me.dependencyStore) {
        me.dependencyStore = {};
      }
    }
  }
  get project() {
    return this._project;
  }
  /**
   * Store for {@link Scheduler/feature/TimeRanges timeRanges} feature.
   * @property {Core.data.Store}
   */
  get timeRangeStore() {
    var _this$_timeRangeStore;
    return (_this$_timeRangeStore = this._timeRangeStore) === null || _this$_timeRangeStore === void 0 ? void 0 : _this$_timeRangeStore.store;
  }
  set timeRangeStore(store) {
    var _this$project;
    this.setFeaturedStore('_timeRangeStore', store, (_this$project = this.project) === null || _this$project === void 0 ? void 0 : _this$project.timeRangeStoreClass);
  }
  /**
   * Store for {@link Scheduler/feature/ResourceTimeRanges resourceTimeRanges} feature.
   * @property {Core.data.Store}
   */
  get resourceTimeRangeStore() {
    var _this$_resourceTimeRa;
    return (_this$_resourceTimeRa = this._resourceTimeRangeStore) === null || _this$_resourceTimeRa === void 0 ? void 0 : _this$_resourceTimeRa.store;
  }
  set resourceTimeRangeStore(store) {
    var _this$project2;
    this.setFeaturedStore('_resourceTimeRangeStore', store, (_this$project2 = this.project) === null || _this$project2 === void 0 ? void 0 : _this$project2.resourceTimeRangeStoreClass);
  }
  /**
   * Get/set the resource store bound to the CRUD manager.
   * @property {Scheduler.data.ResourceStore}
   */
  get resourceStore() {
    var _this$_resourceStore;
    return (_this$_resourceStore = this._resourceStore) === null || _this$_resourceStore === void 0 ? void 0 : _this$_resourceStore.store;
  }
  set resourceStore(store) {
    const me = this;
    me.setFeaturedStore('_resourceStore', store, me.resourceStoreClass);
  }
  /**
   * Get/set the event store bound to the CRUD manager.
   * @property {Scheduler.data.EventStore}
   */
  get eventStore() {
    var _this$_eventStore;
    return (_this$_eventStore = this._eventStore) === null || _this$_eventStore === void 0 ? void 0 : _this$_eventStore.store;
  }
  set eventStore(store) {
    const me = this;
    me.setFeaturedStore('_eventStore', store, me.eventStoreClass);
  }
  /**
   * Get/set the assignment store bound to the CRUD manager.
   * @property {Scheduler.data.AssignmentStore}
   */
  get assignmentStore() {
    var _this$_assignmentStor;
    return (_this$_assignmentStor = this._assignmentStore) === null || _this$_assignmentStor === void 0 ? void 0 : _this$_assignmentStor.store;
  }
  set assignmentStore(store) {
    this.setFeaturedStore('_assignmentStore', store, this.assignmentStoreClass);
  }
  /**
   * Get/set the dependency store bound to the CRUD manager.
   * @property {Scheduler.data.DependencyStore}
   */
  get dependencyStore() {
    var _this$_dependencyStor;
    return (_this$_dependencyStor = this._dependencyStore) === null || _this$_dependencyStor === void 0 ? void 0 : _this$_dependencyStor.store;
  }
  set dependencyStore(store) {
    this.setFeaturedStore('_dependencyStore', store, this.dependencyStoreClass);
  }
  setFeaturedStore(property, store, storeClass) {
    var _me$property;
    const me = this,
      oldStore = (_me$property = me[property]) === null || _me$property === void 0 ? void 0 : _me$property.store;
    // if not the same store
    if (oldStore !== store) {
      var _store;
      // normalize store value (turn it into a storeClass instance if needed)
      store = Store.getStore(store, ((_store = store) === null || _store === void 0 ? void 0 : _store.storeClass) || storeClass);
      if (oldStore) {
        me.removeStore(oldStore);
      }
      me[property] = store && {
        store
      } || null;
      // Adds configured scheduler stores to the store collection ensuring correct order
      // unless they're already registered.
      me.addPrioritizedStore(me[property]);
    }
    return me[property];
  }
  getChangesetPackage() {
    var _this$eventStore$mode, _this$eventStore$mode2;
    const pack = super.getChangesetPackage();
    // Remove assignments from changes if using single assignment mode (resourceId) or resourceIds
    if (pack && (this.eventStore.usesSingleAssignment || (_this$eventStore$mode = this.eventStore.modelClass.fieldMap) !== null && _this$eventStore$mode !== void 0 && (_this$eventStore$mode2 = _this$eventStore$mode.resourceIds) !== null && _this$eventStore$mode2 !== void 0 && _this$eventStore$mode2.persist)) {
      delete pack[this.assignmentStore.storeId];
      // No other changes?
      if (!this.crudStores.some(storeInfo => pack[storeInfo.storeId])) {
        return null;
      }
    }
    return pack;
  }
  //endregion
  get crudLoadValidationMandatoryStores() {
    return [this._eventStore.storeId, this._resourceStore.storeId];
  }
}
CrudManager._$name = 'CrudManager';

/**
 * @module Scheduler/view/mixin/CurrentConfig
 */
const stores = ['eventStore', 'taskStore', 'assignmentStore', 'resourceStore', 'dependencyStore', 'timeRangeStore', 'resourceTimeRangeStore'],
  inlineProperties = ['events', 'tasks', 'resources', 'assignments', 'dependencies', 'timeRanges', 'resourceTimeRanges'];
/**
 * Mixin that makes sure inline data & crud manager data are removed from current config for products using a project.
 * The data is instead inlined in the project (by ProjectModel.js)
 *
 * @mixin
 * @private
 */
var CurrentConfig = (Target => class CurrentConfig extends Target {
  static get $name() {
    return 'CurrentConfig';
  }
  preProcessCurrentConfigs(configs) {
    // Remove inline data on the component
    for (const prop of inlineProperties) {
      delete configs[prop];
    }
    super.preProcessCurrentConfigs(configs);
  }
  // This function is not meant to be called by any code other than Base#getCurrentConfig().
  getCurrentConfig(options) {
    const project = this.project.getCurrentConfig(options),
      result = super.getCurrentConfig(options);
    // Force project with inline data
    if (project) {
      result.project = project;
      const {
        crudManager
      } = result;
      // Transfer crud store configs to project (mainly fields)
      if (crudManager) {
        for (const store of stores) {
          if (crudManager[store]) {
            project[store] = crudManager[store];
          }
        }
      }
      if (Object.keys(project).length === 0) {
        delete result.project;
      }
    }
    // Store (resource store) data is included in project
    delete result.data;
    // Remove CrudManager, since data will be placed inline
    delete result.crudManager;
    return result;
  }
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/Describable
 */
const arrayify = format => !format || Array.isArray(format) ? format : [format],
  pickFormat = (formats, index, defaultFormat) => formats && formats[index] !== true ? formats[index] : defaultFormat;
/**
 * Mixin that provides a consistent method for describing the ranges of time presented by a view. This is currently
 * consumed only by the Calendar widget for describing its child views. This mixin is defined here to facilitate using
 * a Scheduler as a child view of a Calendar.
 *
 * @mixin
 */
var Describable = (Target => class Describable extends (Target || Base) {
  static $name = 'Describable';
  static configurable = {
    /**
     * A {@link Core.helper.DateHelper} format string to use to create date output for view descriptions.
     * @config {String}
     * @default
     */
    dateFormat: 'MMMM d, YYYY',
    /**
     * A string used to separate start and end dates in the {@link #config-descriptionFormat}.
     * @prp {String}
     * @default
     */
    dateSeparator: ' - ',
    /**
     * The date format used by the default {@link #config-descriptionRenderer} for rendering the view's description.
     * If this value is `null`, the {@link #config-dateFormat} (and potentially {@link #config-dateSeparator}) will
     * be used.
     *
     * For views that can span a range of dates, this can be a 2-item array with the following interpretation:
     *
     * - `descriptionFormat[0]` is either a date format string or `true` (to use {@link #config-dateFormat}). The
     *   result of formatting the `startDate` with this format specification is used when the formatting both the
     *   `startDate` and `endDate` with this specification produces the same result. For example, a week view
     *   displays only the month and year components of the date, so this will be used unless the end of the week
     *   crosses into the next month.
     *
     * - `descriptionFormat[1]` is used with {@link Core.helper.DateHelper#function-formatRange-static} when the
     *  `startDate` and `endDate` format differently using `descriptionFormat[0]` (as described above). This one
     *  format string produces a result for both dates. If this value is `true`, the {@link #config-dateFormat} and
     *  {@link #config-dateSeparator} are combined to produce the range format.
     *
     * @prp {String|String[]|Boolean[]}
     * @default
     */
    descriptionFormat: null,
    /**
     * A function that provides the textual description for this view. If provided, this function overrides the
     * {@link #config-descriptionFormat}.
     *
     * ```javascript
     *  descriptionRenderer() {
     *      const
     *          eventsInView = this.eventStore.records.filter(
     *              eventRec => DateHelper.intersectSpans(
     *                  this.startDate, this.endDate,
     *                  eventRec.startDate, eventRec.endDate)).length,
     *          sd = DateHelper.format(this.startDate, 'DD/MM/YYY'),
     *          ed = DateHelper.format(this.endDate, 'DD/MM/YYY');
     *
     *     return `${sd} - ${ed}, ${eventsInView} event${eventsInView === 1 ? '' : 's'}`;
     * }
     * ```
     * @config {Function} descriptionRenderer
     * @param {Core.widget.Widget} view The active view in case the function is in another scope.
     */
    descriptionRenderer: null
  };
  /**
   * Returns the date or ranges of included dates as an array. If there is only one significant date, the array will
   * have only one element. Otherwise, a range of dates is returned as a two-element array with `[0]` being the
   * `startDate` and `[1]` the `lastDate`.
   * @member {Date[]}
   * @internal
   */
  get dateBounds() {
    return [this.date];
  }
  /**
   * The textual description generated by the {@link #config-descriptionRenderer} if present, or by the
   * view's date (or date *range* if it has a range) and the {@link #config-descriptionFormat}.
   * @property {String}
   * @readonly
   */
  get description() {
    const me = this,
      {
        descriptionRenderer
      } = me;
    return descriptionRenderer ? me.callback(descriptionRenderer, me, [me]) : me.formattedDescription;
  }
  get formattedDescription() {
    const me = this,
      {
        dateBounds,
        dateFormat
      } = me,
      descriptionFormat = me.descriptionFormat ?? arrayify(me.defaultDescriptionFormat),
      format0 = pickFormat(descriptionFormat, 0, dateFormat),
      end = dateBounds.length > 1 && (descriptionFormat === null || descriptionFormat === void 0 ? void 0 : descriptionFormat.length) > 1 && DateHelper.format(dateBounds[0], format0) !== DateHelper.format(dateBounds[1], format0);
    // Format the startDate and endDate using the first format
    let ret = DateHelper.format(dateBounds[0], format0);
    if (end) {
      // The endDate renders a different description, and we have a range format.
      ret = DateHelper.formatRange(dateBounds, pickFormat(descriptionFormat, 1, `S${dateFormat}${me.dateSeparator}E${dateFormat}`));
    }
    return ret;
  }
  changeDescriptionFormat(format) {
    return arrayify(format);
  }
  get widgetClass() {} // no b-describable class
});

/**
 * @module Scheduler/view/mixin/SchedulerDom
 */
/**
 * Mixin with EventModel and ResourceModel <-> HTMLElement mapping functions
 *
 * @mixin
 */
var SchedulerDom = (Target => class SchedulerDom extends (Target || Base) {
  static get $name() {
    return 'SchedulerDom';
  }
  //region Get
  /**
   * Returns a single HTMLElement representing an event record assigned to a specific resource.
   * @param {Scheduler.model.AssignmentModel} assignmentRecord An assignment record
   * @returns {HTMLElement} The element representing the event record
   * @category DOM
   */
  getElementFromAssignmentRecord(assignmentRecord, returnWrapper = false) {
    if (this.isPainted && assignmentRecord) {
      var _this$foregroundCanva, _wrapper, _wrapper$syncIdMap;
      let wrapper = (_this$foregroundCanva = this.foregroundCanvas.syncIdMap) === null || _this$foregroundCanva === void 0 ? void 0 : _this$foregroundCanva[assignmentRecord.id];
      // When using links, the original might not be rendered but a link might
      if (!wrapper && assignmentRecord.resource.hasLinks) {
        for (const link of assignmentRecord.resource.$links) {
          var _this$foregroundCanva2;
          wrapper = (_this$foregroundCanva2 = this.foregroundCanvas.syncIdMap) === null || _this$foregroundCanva2 === void 0 ? void 0 : _this$foregroundCanva2[`${assignmentRecord.id}_${link.id}`];
          if (wrapper) {
            break;
          }
        }
      }
      // Wrapper won't have syncIdMap when saving dragcreated event from editor
      return returnWrapper ? wrapper : (_wrapper = wrapper) === null || _wrapper === void 0 ? void 0 : (_wrapper$syncIdMap = _wrapper.syncIdMap) === null || _wrapper$syncIdMap === void 0 ? void 0 : _wrapper$syncIdMap.event;
    }
    return null;
  }
  /**
   * Returns a single HTMLElement representing an event record assigned to a specific resource.
   * @param {Scheduler.model.EventModel} eventRecord An event record
   * @param {Scheduler.model.ResourceModel} resourceRecord A resource record
   * @returns {HTMLElement} The element representing the event record
   * @category DOM
   */
  getElementFromEventRecord(eventRecord, resourceRecord = (() => {
    var _eventRecord$resource;
    return (_eventRecord$resource = eventRecord.resources) === null || _eventRecord$resource === void 0 ? void 0 : _eventRecord$resource[0];
  })(), returnWrapper = false) {
    if (eventRecord.isResourceTimeRange) {
      var _this$foregroundCanva3;
      const wrapper = (_this$foregroundCanva3 = this.foregroundCanvas.syncIdMap) === null || _this$foregroundCanva3 === void 0 ? void 0 : _this$foregroundCanva3[eventRecord.domId];
      return returnWrapper ? wrapper : wrapper === null || wrapper === void 0 ? void 0 : wrapper.syncIdMap.event;
    }
    const assignmentRecord = this.assignmentStore.getAssignmentForEventAndResource(eventRecord, resourceRecord);
    return this.getElementFromAssignmentRecord(assignmentRecord, returnWrapper);
  }
  /**
   * Returns all the HTMLElements representing an event record.
   *
   * @param {Scheduler.model.EventModel} eventRecord An event record
   * @param {Scheduler.model.ResourceModel} [resourceRecord] A resource record
   *
   * @returns {HTMLElement[]} The element(s) representing the event record
   * @category DOM
   */
  getElementsFromEventRecord(eventRecord, resourceRecord, returnWrapper = false) {
    // Single event instance, as array
    if (resourceRecord) {
      return [this.getElementFromEventRecord(eventRecord, resourceRecord, returnWrapper)];
    }
    // All instances
    else {
      return eventRecord.resources.reduce((result, resourceRecord) => {
        const el = this.getElementFromEventRecord(eventRecord, resourceRecord, returnWrapper);
        el && result.push(el);
        return result;
      }, []);
    }
  }
  //endregion
  //region Resolve
  /**
   * Resolves the resource based on a dom element or event. In vertical mode, if resolving from an element higher up in
   * the hierarchy than event elements, then it is required to supply an coordinates since resources are virtual
   * columns.
   * @param {HTMLElement|Event} elementOrEvent The HTML element or DOM event to resolve a resource from
   * @param {Number[]} [xy] X and Y coordinates, required in some cases in vertical mode, disregarded in horizontal
   * @returns {Scheduler.model.ResourceModel} The resource corresponding to the element, or null if not found.
   * @category DOM
   */
  resolveResourceRecord(elementOrEvent, xy) {
    return this.currentOrientation.resolveRowRecord(elementOrEvent, xy);
  }
  /**
   * Product agnostic method which yields the {@link Scheduler.model.ResourceModel} record which underpins the row which
   * encapsulates the passed element. The element can be a grid cell, or an event element, and the result
   * will be a {@link Scheduler.model.ResourceModel}
   * @param {HTMLElement|Event} elementOrEvent The HTML element or DOM event to resolve a record from
   * @returns {Scheduler.model.ResourceModel} The resource corresponding to the element, or null if not found.
   * @category DOM
   */
  resolveRowRecord(elementOrEvent) {
    return this.resolveResourceRecord(elementOrEvent);
  }
  /**
   * Returns the event record for a DOM element
   * @param {HTMLElement|Event} elementOrEvent The DOM node to lookup
   * @returns {Scheduler.model.EventModel} The event record
   * @category DOM
   */
  resolveEventRecord(elementOrEvent) {
    var _elementOrEvent;
    if (elementOrEvent instanceof Event) {
      elementOrEvent = elementOrEvent.target;
    }
    const element = (_elementOrEvent = elementOrEvent) === null || _elementOrEvent === void 0 ? void 0 : _elementOrEvent.closest(this.eventSelector);
    if (element) {
      if (element.dataset.eventId) {
        return this.eventStore.getById(element.dataset.eventId);
      }
      if (element.dataset.assignmentId) {
        return this.assignmentStore.getById(element.dataset.assignmentId).event;
      }
    }
    return null;
  }
  // Used by shared features to resolve an event or task
  resolveTimeSpanRecord(element) {
    return this.resolveEventRecord(element);
  }
  /**
   * Returns an assignment record for a DOM element
   * @param {HTMLElement} element The DOM node to lookup
   * @returns {Scheduler.model.AssignmentModel} The assignment record
   * @category DOM
   */
  resolveAssignmentRecord(element) {
    const eventElement = element.closest(this.eventSelector),
      assignmentRecord = eventElement && this.assignmentStore.getById(eventElement.dataset.assignmentId),
      eventRecord = eventElement && this.eventStore.getById(eventElement.dataset.eventId);
    // When resolving a recurring event, we might be resolving an occurrence
    return this.assignmentStore.getOccurrence(assignmentRecord, eventRecord);
  }
  //endregion
  // Decide if a record is inside a collapsed tree node, or inside a collapsed group (using grouping feature)
  isRowVisible(resourceRecord) {
    // records in collapsed groups/branches etc. are removed from processedRecords
    return this.store.indexOf(resourceRecord) >= 0;
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/SchedulerDomEvents
 */
/**
 * Mixin that handles dom events (click etc) for scheduler and rendered events.
 *
 * @mixin
 */
var SchedulerDomEvents = (Target => class SchedulerDomEvents extends (Target || Base) {
  static get $name() {
    return 'SchedulerDomEvents';
  }
  //region Events
  /**
   * Triggered when user mousedowns over an empty area in the schedule.
   * @event scheduleMouseDown
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when mouse enters an empty area in the schedule.
   * @event scheduleMouseEnter
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when mouse leaves an empty area in the schedule.
   * @event scheduleMouseLeave
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when user mouseups over an empty area in the schedule.
   * @event scheduleMouseUp
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when user moves mouse over an empty area in the schedule.
   * @event scheduleMouseMove
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Scheduler.model.TimeSpan} tick A record which encapsulates the time axis tick clicked on.
   * @param {Number} tickIndex The index of the time axis tick clicked on.
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when user clicks an empty area in the schedule.
   * @event scheduleClick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Scheduler.model.TimeSpan} tick A record which encapsulates the time axis tick clicked on.
   * @param {Number} tickIndex The index of the time axis tick clicked on.
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when user double-clicks an empty area in the schedule.
   * @event scheduleDblClick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Scheduler.model.TimeSpan} tick A record which encapsulates the time axis tick clicked on.
   * @param {Number} tickIndex The index of the time axis tick clicked on.
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Index of double-clicked resource
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when user right-clicks an empty area in the schedule.
   * @event scheduleContextMenu
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Date} date Date at mouse position
   * @param {Scheduler.model.TimeSpan} tick A record which encapsulates the time axis tick clicked on.
   * @param {Number} tickIndex The index of the time axis tick clicked on.
   * @param {Date} tickStartDate The start date of the current time axis tick
   * @param {Date} tickEndDate The end date of the current time axis tick
   * @param {Grid.row.Row} row Row under the mouse (in horizontal mode only)
   * @param {Number} index Resource index
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for mouse down on an event.
   * @event eventMouseDown
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for mouse up on an event.
   * @event eventMouseUp
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for click on an event.
   * @event eventClick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for double-click on an event.
   * @event eventDblClick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for right-click on an event.
   * @event eventContextMenu
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when the mouse enters an event bar.
   * @event eventMouseEnter
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered when the mouse leaves an event bar.
   * @event eventMouseLeave
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for mouse over events when moving into and within an event bar.
   *
   * Note that `mouseover` events bubble, therefore this event will fire while moving from
   * element to element *within* an event bar.
   *
   * _If only an event when moving into the event bar is required, use the {@link #event-eventMouseEnter} event._
   * @event eventMouseOver
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  /**
   * Triggered for mouse out events within and when moving out of an event bar.
   *
   * Note that `mouseout` events bubble, therefore this event will fire while moving from
   * element to element *within* an event bar.
   *
   * _If only an event when moving out of the event bar is required, use the {@link #event-eventMouseLeave} event._
   * @event eventMouseOut
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord Event record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record
   * @param {MouseEvent} event Browser event
   */
  //endregion
  //region Event handling
  getTimeSpanMouseEventParams(eventElement, event) {
    // May have hovered a record being removed / faded out
    const eventRecord = this.resolveEventRecord(eventElement);
    return eventRecord && {
      eventRecord,
      resourceRecord: this.resolveResourceRecord(eventElement),
      assignmentRecord: this.resolveAssignmentRecord(eventElement),
      eventElement,
      event
    };
  }
  getScheduleMouseEventParams(cellData, event) {
    const resourceRecord = this.isVertical ? this.resolveResourceRecord(event) : this.store.getById(cellData.id);
    return {
      resourceRecord
    };
  }
  /**
   * Relays keydown events as eventkeydown if we have a selected task.
   * @private
   */
  onElementKeyDown(event) {
    const result = super.onElementKeyDown(event),
      me = this;
    if (me.selectedEvents.length) {
      me.trigger(me.scheduledEventName + 'KeyDown', {
        eventRecords: me.selectedEvents,
        assignmentRecords: me.selectedAssignments,
        event,
        eventRecord: me.selectedEvents,
        assignmentRecord: me.selectedAssignments
      });
    }
    return result;
  }
  /**
   * Relays keyup events as eventkeyup if we have a selected task.
   * @private
   */
  onElementKeyUp(event) {
    super.onElementKeyUp(event);
    const me = this;
    if (me.selectedEvents.length) {
      me.trigger(me.scheduledEventName + 'KeyUp', {
        eventRecords: me.selectedEvents,
        assignmentRecords: me.selectedAssignments,
        event,
        eventRecord: me.selectedEvents,
        assignmentRecord: me.selectedAssignments
      });
    }
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/eventlayout/HorizontalLayout
 */
/**
 * Base class for horizontal layouts (HorizontalLayoutPack and HorizontalLayoutStack). Should not be used directly,
 * instead specify {@link Scheduler.view.mixin.SchedulerEventRendering#config-eventLayout} in Scheduler config (stack,
 * pack or none):
 *
 * @example
 * let scheduler = new Scheduler({
 *   eventLayout: 'stack'
 * });
 *
 * @abstract
 * @private
 */
class HorizontalLayout extends Base {
  static get defaultConfig() {
    return {
      nbrOfBandsByResource: {},
      bandIndexToPxConvertFn: null,
      bandIndexToPxConvertThisObj: null
    };
  }
  clearCache(resource) {
    if (resource) {
      delete this.nbrOfBandsByResource[resource.id];
    } else {
      this.nbrOfBandsByResource = {};
    }
  }
  /**
   * This method performs layout on an array of event render data and returns amount of _bands_. Band is a multiplier of a
   * configured {@link Scheduler.view.Scheduler#config-rowHeight} to calculate total row height required to fit all
   * events.
   * This method should not be used directly, it is called by the Scheduler during the row rendering process.
   * @param {EventRenderData[]} events Unordered array of event render data, sorting may be required
   * @param {Scheduler.model.ResourceModel} resource The resource for which the events are being laid out.
   * @returns {Number}
   */
  applyLayout(events, resource) {
    // Return number of bands required
    return this.nbrOfBandsByResource[resource.id] = this.layoutEventsInBands(events, resource);
  }
  /**
   * This method iterates over events and calculates top position for each of them. Default layouts calculate
   * positions to avoid events overlapping horizontally (except for the 'none' layout). Pack layout will squeeze events to a single
   * row by reducing their height, Stack layout will increase the row height and keep event height intact.
   * This method should not be used directly, it is called by the Scheduler during the row rendering process.
   * @param {EventRenderData[]} events Unordered array of event render data, sorting may be required
   * @param {Scheduler.model.ResourceModel} resource The resource for which the events are being laid out.
   */
  layoutEventsInBands(events, resource) {
    throw new Error('Implement in subclass');
  }
}
HorizontalLayout._$name = 'HorizontalLayout';

/**
 * @module Scheduler/eventlayout/HorizontalLayoutStack
 */
/**
 * Handles layout of events within a row (resource) in horizontal mode. Stacks events, increasing row height when to fit
 * all overlapping events.
 *
 * This layout is used by default in horizontal mode.
 *
 * @extends Scheduler/eventlayout/HorizontalLayout
 * @private
 */
class HorizontalLayoutStack extends HorizontalLayout {
  static get $name() {
    return 'HorizontalLayoutStack';
  }
  static get configurable() {
    return {
      type: 'stack'
    };
  }
  // Input: Array of event layout data
  // heightRun is used when pre-calculating row heights, taking a cheaper path
  layoutEventsInBands(events, resource, heightRun = false) {
    let verticalPosition = 0;
    do {
      let eventIndex = 0,
        event = events[0];
      while (event) {
        if (!heightRun) {
          // Apply band height to the event cfg
          event.top = this.bandIndexToPxConvertFn.call(this.bandIndexToPxConvertThisObj || this, verticalPosition, event.eventRecord, event.resourceRecord);
        }
        // Remove it from the array and continue searching
        events.splice(eventIndex, 1);
        eventIndex = this.findClosestSuccessor(event, events);
        event = events[eventIndex];
      }
      verticalPosition++;
    } while (events.length > 0);
    // Done!
    return verticalPosition;
  }
  findClosestSuccessor(eventRenderData, events) {
    const {
        endMS,
        group
      } = eventRenderData,
      isMilestone = eventRenderData.eventRecord && eventRenderData.eventRecord.duration === 0;
    let minGap = Infinity,
      closest,
      gap,
      event;
    for (let i = 0, l = events.length; i < l; i++) {
      event = events[i];
      gap = event.startMS - endMS;
      if (gap >= 0 && gap < minGap && (
      // Two milestones should not overlap
      gap > 0 || event.endMS - event.startMS > 0 || !isMilestone)) {
        // Events are sorted by group, so when we find first event with a different group, we can stop iteration
        if (this.grouped && group !== event.group) {
          break;
        }
        closest = i;
        minGap = gap;
      }
    }
    return closest;
  }
}
HorizontalLayoutStack._$name = 'HorizontalLayoutStack';

/**
 * @module Scheduler/eventlayout/PackMixin
 */
/**
 * Mixin holding functionality shared between HorizontalLayoutPack and VerticalLayout.
 *
 * @mixin
 * @private
 */
var PackMixin = (Target => class PackMixin extends (Target || Base) {
  static get $name() {
    return 'PackMixin';
  }
  static get defaultConfig() {
    return {
      coordProp: 'top',
      sizeProp: 'height',
      inBandCoordProp: 'inBandTop',
      inBandSizeProp: 'inBandHeight'
    };
  }
  isSameGroup(a, b) {
    return this.grouped ? a.group === b.group : true;
  }
  // Packs the events to consume as little space as possible
  packEventsInBands(events, applyClusterFn) {
    const me = this,
      {
        coordProp,
        sizeProp
      } = me;
    let slot, firstInCluster, cluster, j;
    for (let i = 0, l = events.length; i < l; i++) {
      firstInCluster = events[i];
      slot = me.findStartSlot(events, firstInCluster);
      cluster = me.getCluster(events, i);
      if (cluster.length > 1) {
        firstInCluster[coordProp] = slot.start;
        firstInCluster[sizeProp] = slot.end - slot.start;
        // If there are multiple slots, and events in the cluster have multiple start dates, group all same-start events into first slot
        j = 1;
        while (j < cluster.length - 1 && cluster[j + 1].start - firstInCluster.start === 0) {
          j++;
        }
        // See if there's more than 1 slot available for this cluster, if so - first group in cluster consumes the entire first slot
        const nextSlot = me.findStartSlot(events, cluster[j]);
        if (nextSlot && nextSlot.start < 0.8) {
          cluster.length = j;
        }
      }
      const clusterSize = cluster.length,
        slotSize = (slot.end - slot.start) / clusterSize;
      // Apply fraction values
      for (j = 0; j < clusterSize; j++) {
        applyClusterFn(cluster[j], j, slot, slotSize);
      }
      i += clusterSize - 1;
    }
    return 1;
  }
  findStartSlot(events, event) {
    const {
        inBandSizeProp,
        inBandCoordProp,
        coordProp,
        sizeProp
      } = this,
      priorOverlappers = this.getPriorOverlappingEvents(events, event);
    let i;
    if (priorOverlappers.length === 0) {
      return {
        start: 0,
        end: 1
      };
    }
    for (i = 0; i < priorOverlappers.length; i++) {
      const item = priorOverlappers[i],
        COORD_PROP = inBandCoordProp in item ? inBandCoordProp : coordProp,
        SIZE_PROP = inBandSizeProp in item ? inBandSizeProp : sizeProp;
      if (i === 0 && item[COORD_PROP] > 0) {
        return {
          start: 0,
          end: item[COORD_PROP]
        };
      } else {
        if (item[COORD_PROP] + item[SIZE_PROP] < (i < priorOverlappers.length - 1 ? priorOverlappers[i + 1][COORD_PROP] : 1)) {
          return {
            start: item[COORD_PROP] + item[SIZE_PROP],
            end: i < priorOverlappers.length - 1 ? priorOverlappers[i + 1][COORD_PROP] : 1
          };
        }
      }
    }
    return false;
  }
  getPriorOverlappingEvents(events, event) {
    const start = event.start,
      end = event.end,
      overlappers = [];
    for (let i = 0, l = events.indexOf(event); i < l; i++) {
      const item = events[i];
      if (this.isSameGroup(item, event) && DateHelper.intersectSpans(start, end, item.start, item.end)) {
        overlappers.push(item);
      }
    }
    overlappers.sort(this.sortOverlappers.bind(this));
    return overlappers;
  }
  sortOverlappers(e1, e2) {
    const {
      coordProp
    } = this;
    return e1[coordProp] - e2[coordProp];
  }
  getCluster(events, startIndex) {
    const startEvent = events[startIndex],
      result = [startEvent];
    if (startIndex >= events.length - 1) {
      return result;
    }
    let {
      start,
      end
    } = startEvent;
    for (let i = startIndex + 1, l = events.length; i < l; i++) {
      const item = events[i];
      if (!this.isSameGroup(item, startEvent) || !DateHelper.intersectSpans(start, end, item.start, item.end)) {
        break;
      }
      result.push(item);
      start = DateHelper.max(start, item.start);
      end = DateHelper.min(item.end, end);
    }
    return result;
  }
});

/**
 * @module Scheduler/eventlayout/HorizontalLayoutPack
 */
/**
 * Handles layout of events within a row (resource) in horizontal mode. Packs events (adjusts their height) to fit
 * available row height
 *
 * @extends Scheduler/eventlayout/HorizontalLayout
 * @mixes Scheduler/eventlayout/PackMixin
 * @private
 */
class HorizontalLayoutPack extends HorizontalLayout.mixin(PackMixin) {
  static get $name() {
    return 'HorizontalLayoutPack';
  }
  static get configurable() {
    return {
      type: 'pack'
    };
  }
  // Packs the events to consume as little space as possible
  layoutEventsInBands(events) {
    const result = this.packEventsInBands(events, (event, j, slot, slotSize) => {
      event.height = slotSize;
      event.top = slot.start + j * slotSize;
    });
    events.forEach(event => {
      Object.assign(event, this.bandIndexToPxConvertFn.call(this.bandIndexToPxConvertThisObj || this, event.top, event.height, event.eventRecord, event.resourceRecord));
    });
    return result;
  }
}
HorizontalLayoutPack._$name = 'HorizontalLayoutPack';

/**
 * @module Scheduler/view/mixin/SchedulerResourceRendering
 */
/**
 * Configs and functions used for resource rendering
 * and by the {@link Scheduler/column/ResourceInfoColumn} class.
 *
 * @mixin
 */
var SchedulerResourceRendering = (Target => class SchedulerResourceRendering extends (Target || Base) {
  static $name = 'SchedulerResourceRendering';
  //region Default config
  static configurable = {
    /**
     * Control how much space to leave between the first event/last event and the resources edge (top/bottom
     * margin within the resource row in horizontal mode, left/right margin within the resource column in
     * vertical mode), in px. Defaults to the value of {@link Scheduler.view.Scheduler#config-barMargin}.
     *
     * Can be configured per resource by setting {@link Scheduler.model.ResourceModel#field-resourceMargin
     * resource.resourceMargin}.
     *
     * @prp {Number}
     * @category Scheduled events
     */
    resourceMargin: null,
    /**
     * A config object used to configure the resource columns in vertical mode.
     * See {@link Scheduler.view.ResourceHeader} for more details on available properties.
     *
     * ```javascript
     * new Scheduler({
     *     resourceColumns : {
     *         columnWidth    : 100,
     *         headerRenderer : ({ resourceRecord }) => `${resourceRecord.id} - ${resourceRecord.name}`
     *     }
     * })
     * ```
     * @config {ResourceHeaderConfig}
     * @category Resources
     */
    resourceColumns: null,
    /**
     * Path to load resource images from. Used by the resource header in vertical mode and the
     * {@link Scheduler.column.ResourceInfoColumn} in horizontal mode. Set this to display miniature
     * images for each resource using their `image` or `imageUrl` fields.
     *
     * * `image` represents image name inside the specified `resourceImagePath`,
     * * `imageUrl` represents fully qualified image URL.
     *
     *  If set and a resource has no `imageUrl` or `image` specified it will try show miniature using
     *  the resource's name with {@link #config-resourceImageExtension} appended.
     *
     * **NOTE**: The path should end with a `/`:
     *
     * ```
     * new Scheduler({
     *   resourceImagePath : 'images/resources/'
     * });
     * ```
     * @config {String}
     * @category Resources
     */
    resourceImagePath: null,
    /**
     * Generic resource image, used when provided `imageUrl` or `image` fields or path calculated from resource
     * name are all invalid. If left blank, resource name initials will be shown when no image can be loaded.
     * @default
     * @config {String}
     * @category Resources
     */
    defaultResourceImageName: null,
    /**
     * Resource image extension, used when creating image path from resource name.
     * @default
     * @config {String}
     * @category Resources
     */
    resourceImageExtension: '.jpg'
  };
  //endregion
  //region Resource header/columns
  // NOTE: The configs below are initially applied to the resource header in `TimeAxisColumn#set mode`
  /**
   * Use it to manipulate resource column properties at runtime.
   * @property {Scheduler.view.ResourceHeader}
   * @readonly
   */
  get resourceColumns() {
    var _this$timeAxisColumn;
    return ((_this$timeAxisColumn = this.timeAxisColumn) === null || _this$timeAxisColumn === void 0 ? void 0 : _this$timeAxisColumn.resourceColumns) || this._resourceColumns;
  }
  /**
   * Get resource column width. Only applies to vertical mode. To set it, assign to
   * `scheduler.resourceColumns.columnWidth`.
   * @property {Number}
   * @readonly
   */
  get resourceColumnWidth() {
    var _this$resourceColumns;
    return ((_this$resourceColumns = this.resourceColumns) === null || _this$resourceColumns === void 0 ? void 0 : _this$resourceColumns.columnWidth) || null;
  }
  //endregion
  //region Event rendering
  // Returns a resource specific resourceMargin, falling back to Schedulers setting
  // This fn could be made public to allow hooking it as an alternative to only setting this in data
  getResourceMargin(resourceRecord) {
    return (resourceRecord === null || resourceRecord === void 0 ? void 0 : resourceRecord.resourceMargin) ?? this.resourceMargin;
  }
  // Returns a resource specific barMargin, falling back to Schedulers setting
  // This fn could be made public to allow hooking it as an alternative to only setting this in data
  getBarMargin(resourceRecord) {
    return (resourceRecord === null || resourceRecord === void 0 ? void 0 : resourceRecord.barMargin) ?? this.barMargin;
  }
  // Returns a resource specific rowHeight, falling back to Schedulers setting
  // Prio order: Height from record, configured height
  // This fn could be made public to allow hooking it as an alternative to only setting this in data
  getResourceHeight(resourceRecord) {
    return resourceRecord.rowHeight ?? (this.isHorizontal ? this.rowHeight : this.getResourceWidth(resourceRecord));
  }
  getResourceWidth(resourceRecord) {
    return resourceRecord.columnWidth ?? this.resourceColumnWidth;
  }
  // Similar to getResourceHeight(), but for usage later in the process to take height set by renderers into account.
  // Cant be used earlier in the process because then the row will grow
  // Prio order: Height requested by renderer, height from record, configured height
  getAppliedResourceHeight(resourceRecord) {
    const row = this.getRowById(resourceRecord);
    return (row === null || row === void 0 ? void 0 : row.maxRequestedHeight) ?? this.getResourceHeight(resourceRecord);
  }
  // Combined convenience getter for destructuring on calling side
  // Second arg only passed for nested events, handled by NestedEvent feature
  getResourceLayoutSettings(resourceRecord, parentEventRecord = null) {
    const resourceMargin = this.getResourceMargin(resourceRecord, parentEventRecord),
      rowHeight = this.getAppliedResourceHeight(resourceRecord, parentEventRecord);
    return {
      barMargin: this.getBarMargin(resourceRecord, parentEventRecord),
      contentHeight: Math.max(rowHeight - resourceMargin * 2, 1),
      rowHeight,
      resourceMargin
    };
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/SchedulerEventRendering
 */
/**
 * Layout data object used to lay out an event record.
 * @typedef {Object} EventRenderData
 * @property {Scheduler.model.EventModel} eventRecord Event instance
 * @property {Scheduler.model.ResourceModel} resourceRecord Assigned resource
 * @property {Scheduler.model.AssignmentModel} assignmentRecord Assignment instance
 * @property {Number} startMS Event start date time in milliseconds
 * @property {Number} endMS Event end date in milliseconds
 * @property {Number} height Calculated event element height
 * @property {Number} width Calculated event element width
 * @property {Number} top Calculated event element top position in the row (or column)
 * @property {Number} left Calculated event element left position in the row (or column)
 */
/**
 * Functions to handle event rendering (EventModel -> dom elements).
 *
 * @mixes Scheduler/view/mixin/SchedulerResourceRendering
 * @mixin
 */
var SchedulerEventRendering = (Target => class SchedulerEventRendering extends SchedulerResourceRendering(Target || Base) {
  static get $name() {
    return 'SchedulerEventRendering';
  }
  //region Default config
  static get configurable() {
    return {
      /**
       * Position of the milestone text:
       * * 'inside' - for short 1-char text displayed inside the diamond, not applicable when using
       *   {@link #config-milestoneLayoutMode})
       * * 'outside' - for longer text displayed outside the diamond, but inside it when using
       *   {@link #config-milestoneLayoutMode}
       * * 'always-outside' - outside even when combined with {@link #config-milestoneLayoutMode}
       *
       * @prp {'inside'|'outside'|'always-outside'}
       * @default
       * @category Milestones
       */
      milestoneTextPosition: 'outside',
      /**
       * How to align milestones in relation to their startDate. Only applies when using a `milestoneLayoutMode`
       * other than `default`. Valid values are:
       * * start
       * * center (default)
       * * end
       * @prp {'start'|'center'|'end'}
       * @default
       * @category Milestones
       */
      milestoneAlign: 'center',
      /**
       * Factor representing the average char width in pixels used to determine milestone width when configured
       * with `milestoneLayoutMode: 'estimate'`.
       * @prp {Number}
       * @default
       * @category Milestones
       */
      milestoneCharWidth: 10,
      /**
       * How to handle milestones during event layout. How the milestones are displayed when part of the layout
       * are controlled using {@link #config-milestoneTextPosition}.
       *
       * Options are:
       * * default - Milestones do not affect event layout
       * * estimate - Milestone width is estimated by multiplying text length with Scheduler#milestoneCharWidth
       * * data - Milestone width is determined by checking EventModel#milestoneWidth
       * * measure - Milestone width is determined by measuring label width
       * Please note that currently text width is always determined using EventModel#name.
       * Also note that only 'default' is supported by eventStyles line, dashed and minimal.
       * @prp {'default'|'estimate'|'data'|'measure'}
       * @default
       * @category Milestones
       */
      milestoneLayoutMode: 'default',
      /**
       * Defines how to handle overlapping events. Valid values are:
       * - `stack`, adjusts row height (only horizontal)
       * - `pack`, adjusts event height
       * - `mixed`, allows two events to overlap, more packs (only vertical)
       * - `none`, allows events to overlap
       *
       * This config can also accept an object:
       *
       * ```javascript
       * new Scheduler({
       *     eventLayout : { type : 'stack' }
       * })
       * ```
       *
       * @prp {'stack'|'pack'|'mixed'|'none'|Object}
       * @default
       * @category Scheduled events
       */
      eventLayout: 'stack',
      /**
       * Override this method to provide a custom sort function to sort any overlapping events. See {@link
       * #config-overlappingEventSorter} for more details.
       *
       * @param  {Scheduler.model.EventModel} a First event
       * @param  {Scheduler.model.EventModel} b Second event
       * @returns {Number} Return -1 to display `a` above `b`, 1 for `b` above `a`
       * @member {Function} overlappingEventSorter
       * @category Misc
       */
      /**
       * Override this method to provide a custom sort function to sort any overlapping events. This only applies
       * to the horizontal mode, where the order the events are sorted in determines their vertical placement
       * within a resource.
       *
       * By default, overlapping events are laid out based on the start date. If the start date is equal, events
       * with earlier end date go first. And lastly the name of events is taken into account.
       *
       * Here's a sample sort function, sorting on start- and end date. If this function returns -1, then event
       * `a` is placed above event `b`:
       *
       * ```javascript
       * overlappingEventSorter(a, b) {
       *
       *   const startA = a.startDate, endA = a.endDate;
       *   const startB = b.startDate, endB = b.endDate;
       *
       *   const sameStart = (startA - startB === 0);
       *
       *   if (sameStart) {
       *     return endA > endB ? -1 : 1;
       *   } else {
       *     return (startA < startB) ? -1 : 1;
       *   }
       * }
       * ```
       *
       * NOTE: The algorithms (stack, pack) that lay the events out expects them to be served in chronological
       * order, be sure to first sort by `startDate` to get predictable results.
       *
       * @param  {Scheduler.model.EventModel} a First event
       * @param  {Scheduler.model.EventModel} b Second event
       * @returns {Number} Return -1 to display `a` above `b`, 1 for `b` above `a`
       * @config {Function}
       * @category Misc
       */
      overlappingEventSorter: null,
      /**
       * Deprecated, to be removed in version 6.0. Replaced by {@link #config-overlappingEventSorter}.
       * @deprecated Since 5.0. Use {@link #config-overlappingEventSorter} instead.
       * @config {Function}
       * @category Misc
       */
      horizontalEventSorterFn: null,
      /**
       * Control how much space to leave between the first event/last event and the resources edge (top/bottom
       * margin within the resource row in horizontal mode, left/right margin within the resource column in
       * vertical mode), in px. Defaults to the value of {@link Scheduler.view.Scheduler#config-barMargin}.
       *
       * Can be configured per resource by setting {@link Scheduler.model.ResourceModel#field-resourceMargin
       * resource.resourceMargin}.
       *
       * @prp {Number}
       * @category Scheduled events
       */
      resourceMargin: null,
      /**
       * By default, scheduler fade events in on load. Specify `false` to prevent this animation or specify one
       * of the available animation types to use it (`true` equals `'fade-in'`):
       * * fade-in (default)
       * * slide-from-left
       * * slide-from-top
       * ```
       * // Slide events in from the left on load
       * scheduler = new Scheduler({
       *     useInitialAnimation : 'slide-from-left'
       * });
       * ```
       * @prp {Boolean|String}
       * @default
       * @category Misc
       */
      useInitialAnimation: true,
      /**
       * An empty function by default, but provided so that you can override it. This function is called each time
       * an event is rendered into the schedule to render the contents of the event. It's called with the event,
       * its resource and a `renderData` object which allows you to populate data placeholders inside the event
       * template. **IMPORTANT** You should never modify any data on the EventModel inside this method.
       *
       * By default, the DOM markup of an event bar includes placeholders for 'cls' and 'style'. The cls property
       * is a {@link Core.helper.util.DomClassList} which will be added to the event element. The style property
       * is an inline style declaration for the event element.
       *
       * IMPORTANT: When returning content, be sure to consider how that content should be encoded to avoid XSS
       * (Cross-Site Scripting) attacks. This is especially important when including user-controlled data such as
       * the event's `name`. The function {@link Core.helper.StringHelper#function-encodeHtml-static} as well as
       * {@link Core.helper.StringHelper#function-xss-static} can be helpful in these cases.
       *
       * ```javascript
       *  eventRenderer({ eventRecord, resourceRecord, renderData }) {
       *      renderData.style = 'color:white';                 // You can use inline styles too.
       *
       *      // Property names with truthy values are added to the resulting elements CSS class.
       *      renderData.cls.isImportant = this.isImportant(eventRecord);
       *      renderData.cls.isModified = eventRecord.isModified;
       *
       *      // Remove a class name by setting the property to false
       *      renderData.cls[scheduler.generatedIdCls] = false;
       *
       *      // Or, you can treat it as a string, but this is less efficient, especially
       *      // if your renderer wants to *remove* classes that may be there.
       *      renderData.cls += ' extra-class';
       *
       *      return StringHelper.xss`${DateHelper.format(eventRecord.startDate, 'YYYY-MM-DD')}: ${eventRecord.name}`;
       *  }
       * ```
       *
       * @param {Object} detail An object containing the information needed to render an Event.
       * @param {Scheduler.model.EventModel} detail.eventRecord The event record.
       * @param {Scheduler.model.ResourceModel} detail.resourceRecord The resource record.
       * @param {Scheduler.model.AssignmentModel} detail.assignmentRecord The assignment record.
       * @param {Object} detail.renderData An object containing details about the event rendering.
       * @param {Scheduler.model.EventModel} detail.renderData.event The event record.
       * @param {Core.helper.util.DomClassList|String} detail.renderData.cls An object whose property names
       * represent the CSS class names to be added to the event bar element. Set a property's value to truthy or
       * falsy to add or remove the class name based on the property name. Using this technique, you do not have
       * to know whether the class is already there, or deal with concatenation.
       * @param {Core.helper.util.DomClassList|String} detail.renderData.wrapperCls An object whose property names
       * represent the CSS class names to be added to the event wrapper element. Set a property's value to truthy
       * or falsy to add or remove the class name based on the property name. Using this technique, you do not
       * have to know whether the class is already there, or deal with concatenation.
       * @param {Core.helper.util.DomClassList|String} detail.renderData.iconCls An object whose property names
       * represent the CSS class names to be added to an event icon element.
       *
       * Note that an element carrying this icon class is injected into the event element *after*
       * the renderer completes, *before* the renderer's created content.
       *
       * To disable this if the renderer takes full control and creates content using the iconCls,
       * you can set `renderData.iconCls = null`.
       * @param {Number} detail.renderData.left Vertical offset position (in pixels) on the time axis.
       * @param {Number} detail.renderData.width Width in pixels of the event element.
       * @param {Number} detail.renderData.height Height in pixels of the event element.
       * @param {String|Object<String,String>} detail.renderData.style Inline styles for the event bar DOM element.
       * Use either 'border: 1px solid black' or `{ border: '1px solid black' }`
       * @param {String|Object<String,String>} detail.renderData.wrapperStyle Inline styles for wrapper of the
       * event bar DOM element. Use either 'border: 1px solid green' or `{ border: '1px solid green' }`
       * @param {String} detail.renderData.eventStyle The `eventStyle` of the event. Use this to apply custom
       * styles to the event DOM element
       * @param {String} detail.renderData.eventColor The `eventColor` of the event. Use this to set a custom
       * color for the rendered event
       * @param {DomConfig[]} detail.renderData.children An array of DOM configs used as children to the
       * `b-sch-event` element. Can be populated with additional DOM configs to have more control over contents.
       * @returns {String|Object} A simple string, or a custom object which will be applied to the
       * {@link #config-eventBodyTemplate}, creating the actual HTML
       * @config {Function}
       * @category Scheduled events
       */
      eventRenderer: null,
      /**
       * `this` reference for the {@link #config-eventRenderer} function
       * @config {Object}
       * @category Scheduled events
       */
      eventRendererThisObj: null,
      /**
       * Field from EventModel displayed as text in the bar when rendering
       * @config {String}
       * @default
       * @category Scheduled events
       */
      eventBarTextField: 'name',
      /**
       * The template used to generate the markup of your events in the scheduler. To 'populate' the
       * eventBodyTemplate with data, use the {@link #config-eventRenderer} method.
       * @config {Function}
       * @category Scheduled events
       */
      eventBodyTemplate: null,
      /**
       * The class responsible for the packing horizontal event layout process.
       * Override this to take control over the layout process.
       * @config {Scheduler.eventlayout.HorizontalLayout}
       * @typings {typeof HorizontalLayout}
       * @default
       * @private
       * @category Misc
       */
      horizontalLayoutPackClass: HorizontalLayoutPack,
      /**
       * The class name responsible for the stacking horizontal event layout process.
       * Override this to take control over the layout process.
       * @config {Scheduler.eventlayout.HorizontalLayout}
       * @typings {typeof HorizontalLayout}
       * @default
       * @private
       * @category Misc
       */
      horizontalLayoutStackClass: HorizontalLayoutStack,
      /**
       * Controls how much space to leave between stacked event bars in px.
       *
       * Can be configured per resource by setting {@link Scheduler.model.ResourceModel#field-barMargin
       * resource.barMargin}.
       *
       * @config {Number} barMargin
       * @default
       * @category Scheduled events
       */
      // Used to animate events on first render
      isFirstRender: true,
      initialAnimationDuration: 2000,
      /**
       * When an event bar has a width less than this value, it gets the CSS class `b-sch-event-narrow`
       * added. You may apply custom CSS rules using this class.
       *
       * In vertical mode, this class causes the text to be rotated so that it runs vertically.
       * @default
       * @config {Number}
       * @category Scheduled events
       */
      narrowEventWidth: 10,
      internalEventLayout: null,
      eventPositionMode: 'translate',
      eventScrollMode: 'move'
    };
  }
  //endregion
  //region Settings
  changeEventLayout(eventLayout) {
    // Pass layout config to internal config to normalize its form
    this.internalEventLayout = eventLayout;
    // Return normalized string type
    return this.internalEventLayout.type;
  }
  changeInternalEventLayout(eventLayout) {
    return this.getEventLayout(eventLayout);
  }
  updateInternalEventLayout(eventLayout, oldEventLayout) {
    const me = this;
    if (oldEventLayout) {
      me.element.classList.remove(`b-eventlayout-${oldEventLayout.type}`);
    }
    me.element.classList.add(`b-eventlayout-${eventLayout.type}`);
    if (!me.isConfiguring) {
      me.refreshWithTransition();
      me.trigger('stateChange');
    }
  }
  changeHorizontalEventSorterFn(fn) {
    VersionHelper.deprecate('Scheduler', '6.0.0', 'Replaced by overlappingEventSorter()');
    this.overlappingEventSorter = fn;
  }
  updateOverlappingEventSorter(fn) {
    if (!this.isConfiguring) {
      this.refreshWithTransition();
    }
  }
  //endregion
  //region Layout helpers
  // Wraps string config to object with type
  getEventLayout(value) {
    var _value;
    if ((_value = value) !== null && _value !== void 0 && _value.isModel) {
      value = value.eventLayout || this.internalEventLayout;
    }
    if (typeof value === 'string') {
      value = {
        type: value
      };
    }
    return value;
  }
  /**
   * Get event layout handler. The handler decides the vertical placement of events within a resource.
   * Returns null if no eventLayout is used (if {@link #config-eventLayout} is set to "none")
   * @internal
   * @returns {Scheduler.eventlayout.HorizontalLayout}
   * @readonly
   * @category Scheduled events
   */
  getEventLayoutHandler(eventLayout) {
    const me = this;
    if (!me.isHorizontal) {
      return null;
    }
    const {
        timeAxisViewModel,
        horizontal
      } = me,
      {
        type
      } = eventLayout;
    if (!me.layouts) {
      me.layouts = {};
    }
    switch (type) {
      // stack, adjust row height to fit all events
      case 'stack':
        {
          if (!me.layouts.horizontalStack) {
            me.layouts.horizontalStack = new me.horizontalLayoutStackClass(ObjectHelper.assign({
              scheduler: me,
              timeAxisViewModel,
              bandIndexToPxConvertFn: horizontal.layoutEventVerticallyStack,
              bandIndexToPxConvertThisObj: horizontal
            }, eventLayout));
          }
          return me.layouts.horizontalStack;
        }
      // pack, fit all events in available height by adjusting their height
      case 'pack':
        {
          if (!me.layouts.horizontalPack) {
            me.layouts.horizontalPack = new me.horizontalLayoutPackClass(ObjectHelper.assign({
              scheduler: me,
              timeAxisViewModel,
              bandIndexToPxConvertFn: horizontal.layoutEventVerticallyPack,
              bandIndexToPxConvertThisObj: horizontal
            }, eventLayout));
          }
          return me.layouts.horizontalPack;
        }
      default:
        return null;
    }
  }
  //endregion
  //region Event rendering
  // Chainable function called with the events to render for a specific resource. Allows features to add/remove.
  // Chained by ResourceTimeRanges
  getEventsToRender(resource, events) {
    return events;
  }
  /**
   * Rerenders events for specified resource (by rerendering the entire row).
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @category Rendering
   */
  repaintEventsForResource(resourceRecord) {
    this.currentOrientation.repaintEventsForResource(resourceRecord);
  }
  /**
   * Rerenders the events for all resources connected to the specified event
   * @param {Scheduler.model.EventModel} eventRecord
   * @private
   */
  repaintEvent(eventRecord) {
    const resources = this.eventStore.getResourcesForEvent(eventRecord);
    resources.forEach(resourceRecord => this.repaintEventsForResource(resourceRecord));
  }
  getEventStyle(eventRecord, resourceRecord) {
    return eventRecord.eventStyle || resourceRecord.eventStyle || this.eventStyle;
  }
  getEventColor(eventRecord, resourceRecord) {
    var _eventRecord$event, _eventRecord$parent;
    return eventRecord.eventColor || ((_eventRecord$event = eventRecord.event) === null || _eventRecord$event === void 0 ? void 0 : _eventRecord$event.eventColor) || ((_eventRecord$parent = eventRecord.parent) === null || _eventRecord$parent === void 0 ? void 0 : _eventRecord$parent.eventColor) || resourceRecord.eventColor || this.eventColor;
  }
  //endregion
  //region Template
  /**
   * Generates data used in the template when rendering an event. For example which css classes to use. Also applies
   * #eventBodyTemplate and calls the {@link #config-eventRenderer}.
   * @private
   * @param {Scheduler.model.EventModel} eventRecord Event to generate data for
   * @param {Scheduler.model.ResourceModel} resourceRecord Events resource
   * @param {Boolean|Object} includeOutside Specify true to get boxes for timespans outside the rendered zone in both
   * dimensions. This option is used when calculating dependency lines, and we need to include routes from timespans
   * which may be outside the rendered zone.
   * @param {Boolean} includeOutside.timeAxis Pass as `true` to include timespans outside the TimeAxis's bounds
   * @param {Boolean} includeOutside.viewport Pass as `true` to include timespans outside the vertical timespan viewport's bounds.
   * @returns {Object} Data to use in event template, or `undefined` if the event is outside the rendered zone.
   */
  generateRenderData(eventRecord, resourceRecord, includeOutside = {
    viewport: true
  }) {
    const me = this,
      // generateRenderData calculates layout for events which are outside the vertical viewport
      // because the RowManager needs to know a row height.
      renderData = me.currentOrientation.getTimeSpanRenderData(eventRecord, resourceRecord, includeOutside),
      {
        isEvent
      } = eventRecord,
      {
        eventResize
      } = me.features,
      // Don't want events drag created to zero duration to render as milestones
      isMilestone = !eventRecord.meta.isDragCreating && eventRecord.isMilestone,
      // $originalId allows lookup to yield same result for original resources and linked resources
      assignmentRecord = isEvent && eventRecord.assignments.find(a => a.resourceId === resourceRecord.$originalId),
      // Events inner element, will be populated by renderer and/or eventBodyTemplate
      eventContent = {
        className: 'b-sch-event-content',
        role: 'presentation',
        dataset: {
          taskBarFeature: 'content'
        }
      };
    if (renderData) {
      var _renderData$iconCls2;
      renderData.tabIndex = '0';
      let resizable = eventRecord.isResizable;
      if (eventResize && resizable) {
        if (renderData.startsOutsideView) {
          if (resizable === true) {
            resizable = 'end';
          } else if (resizable === 'start') {
            resizable = false;
          }
        }
        if (renderData.endsOutsideView) {
          if (resizable === true) {
            resizable = 'start';
          } else if (resizable === 'end') {
            resizable = false;
          }
        }
        // Let the feature veto start/end handles
        if (resizable) {
          if (me.isHorizontal) {
            if (!me.rtl && !eventResize.leftHandle || me.rtl && !eventResize.rightHandle) {
              resizable = resizable === 'start' ? false : 'end';
            } else if (!me.rtl && !eventResize.rightHandle || me.rtl && !eventResize.leftHandle) {
              resizable = resizable === 'end' ? false : 'start';
            }
          } else {
            if (!eventResize.topHandle) {
              resizable = resizable === 'start' ? false : 'end';
            } else if (!eventResize.bottomHandle) {
              resizable = resizable === 'end' ? false : 'start';
            }
          }
        }
      }
      // Event record cls properties are now DomClassList instances, so clone them
      // so that they can be manipulated here and by renderers.
      // Truthy value means the key will be added as a class name.
      // ResourceTimeRanges applies custom cls to wrapper.
      const
        // Boolean needed here, otherwise DomSync will dig into comparing the modifications
        isDirty = Boolean(eventRecord.hasPersistableChanges || (assignmentRecord === null || assignmentRecord === void 0 ? void 0 : assignmentRecord.hasPersistableChanges)),
        clsListObj = {
          [resourceRecord.cls]: resourceRecord.cls,
          [me.generatedIdCls]: !eventRecord.isOccurrence && eventRecord.hasGeneratedId,
          [me.dirtyCls]: isDirty,
          [me.committingCls]: eventRecord.isCommitting,
          [me.endsOutsideViewCls]: renderData.endsOutsideView,
          [me.startsOutsideViewCls]: renderData.startsOutsideView,
          'b-clipped-start': renderData.clippedStart,
          'b-clipped-end': renderData.clippedEnd,
          'b-iscreating': eventRecord.isCreating,
          'b-rtl': me.rtl
        },
        wrapperClsListObj = {
          [`${me.eventCls}-parent`]: resourceRecord.isParent,
          'b-readonly': eventRecord.readOnly || (assignmentRecord === null || assignmentRecord === void 0 ? void 0 : assignmentRecord.readOnly),
          'b-linked-resource': resourceRecord.isLinked,
          'b-original-resource': resourceRecord.hasLinks
        },
        clsList = eventRecord.isResourceTimeRange ? new DomClassList() : eventRecord.internalCls.clone(),
        wrapperClsList = eventRecord.isResourceTimeRange ? eventRecord.internalCls.clone() : new DomClassList();
      renderData.wrapperStyle = '';
      // mark as wrapper to make sure fire render events for this level only
      renderData.isWrap = true;
      // Event specifics, things that do not apply to ResourceTimeRanges
      if (isEvent) {
        const selected = assignmentRecord && me.isAssignmentSelected(assignmentRecord);
        ObjectHelper.assign(clsListObj, {
          [me.eventCls]: 1,
          'b-milestone': isMilestone,
          'b-sch-event-narrow': !isMilestone && renderData.width < me.narrowEventWidth,
          [me.fixedEventCls]: eventRecord.isDraggable === false,
          [`b-sch-event-resizable-${resizable}`]: Boolean(eventResize && !eventRecord.readOnly),
          [me.eventSelectedCls]: selected,
          [me.eventAssignHighlightCls]: me.eventAssignHighlightCls && !selected && me.isEventSelected(eventRecord),
          'b-recurring': eventRecord.isRecurring,
          'b-occurrence': eventRecord.isOccurrence,
          'b-inactive': eventRecord.inactive
        });
        renderData.eventId = eventRecord.id;
        const eventStyle = me.getEventStyle(eventRecord, resourceRecord),
          eventColor = me.getEventColor(eventRecord, resourceRecord),
          hasAnimation = me.isFirstRender && me.useInitialAnimation && globalThis.bryntum.noAnimations !== true;
        ObjectHelper.assign(wrapperClsListObj, {
          [`${me.eventCls}-wrap`]: 1,
          'b-milestone-wrap': isMilestone
        });
        if (hasAnimation) {
          const index = renderData.row ? renderData.row.index : (renderData.top - me.scrollTop) / me.tickSize,
            delayMS = index / 20 * 1000;
          renderData.wrapperStyle = `animation-delay: ${delayMS}ms;`;
          me.maxDelay = Math.max(me.maxDelay || 0, delayMS);
          // Add an extra delay to wait for the most delayed animation to finish
          // before we call stopInitialAnimation. In this way, we allow them all to finish
          // before we remove the b-initial-${me._useInitialAnimation} class.
          if (!me.initialAnimationDetacher) {
            me.initialAnimationDetacher = EventHelper.on({
              element: me.foregroundCanvas,
              delegate: me.eventSelector,
              // Just listen for the first animation end fired by our event els
              once: true,
              animationend: () => me.setTimeout({
                fn: 'stopInitialAnimation',
                delay: me.maxDelay,
                cancelOutstanding: true
              }),
              // Fallback in case animation is interrupted
              expires: {
                alt: 'stopInitialAnimation',
                delay: me.initialAnimationDuration + me.maxDelay
              },
              thisObj: me
            });
          }
        }
        renderData.eventColor = eventColor;
        renderData.eventStyle = eventStyle;
        renderData.assignmentRecord = renderData.assignment = assignmentRecord;
      }
      // If not using a wrapping div, this cls will be added to event div for correct rendering
      renderData.wrapperCls = ObjectHelper.assign(wrapperClsList, wrapperClsListObj);
      renderData.cls = ObjectHelper.assign(clsList, clsListObj);
      renderData.iconCls = new DomClassList(eventRecord.getValue(me.eventBarIconClsField) || eventRecord.iconCls);
      // ResourceTimeRanges applies custom style to the wrapper
      if (eventRecord.isResourceTimeRange) {
        renderData.style = '';
        renderData.wrapperStyle += eventRecord.style || '';
      }
      // Others to inner
      else {
        renderData.style = eventRecord.style || '';
      }
      renderData.resource = renderData.resourceRecord = resourceRecord;
      renderData.resourceId = renderData.rowId;
      if (isEvent) {
        let childContent = null,
          milestoneLabelConfig = null,
          value;
        if (me.eventRenderer) {
          // User has specified a renderer fn, either to return a simple string, or an object intended for the eventBodyTemplate
          const rendererValue = me.eventRenderer.call(me.eventRendererThisObj || me, {
            eventRecord,
            resourceRecord,
            assignmentRecord: renderData.assignmentRecord,
            renderData
          });
          // If the user's renderer coerced it into a string, recreate a DomClassList.
          if (typeof renderData.cls === 'string') {
            renderData.cls = new DomClassList(renderData.cls);
          }
          if (typeof renderData.wrapperCls === 'string') {
            renderData.wrapperCls = new DomClassList(renderData.wrapperCls);
          }
          // Same goes for iconCls
          if (typeof renderData.iconCls === 'string') {
            renderData.iconCls = new DomClassList(renderData.iconCls);
          }
          if (me.eventBodyTemplate) {
            value = me.eventBodyTemplate(rendererValue);
          } else {
            value = rendererValue;
          }
        } else if (me.eventBodyTemplate) {
          // User has specified an eventBodyTemplate, but no renderer - just apply the entire event record data.
          value = me.eventBodyTemplate(eventRecord);
        } else if (me.eventBarTextField) {
          // User has specified a field in the data model to read from
          value = StringHelper.encodeHtml(eventRecord.getValue(me.eventBarTextField) || '');
        }
        if (!me.eventBodyTemplate || Array.isArray(value)) {
          var _renderData$iconCls;
          eventContent.children = [];
          // Give milestone a dedicated label element so we can use padding
          if (isMilestone && (me.milestoneLayoutMode === 'default' || me.milestoneTextPosition === 'always-outside') && value != null && value !== '') {
            eventContent.children.unshift(milestoneLabelConfig = {
              tag: 'label',
              children: []
            });
          }
          if ((_renderData$iconCls = renderData.iconCls) !== null && _renderData$iconCls !== void 0 && _renderData$iconCls.length) {
            eventContent.children.unshift({
              tag: 'i',
              className: renderData.iconCls
            });
          }
          // Array, assumed to contain DOM configs for eventContent children (or milestone label)
          if (Array.isArray(value)) {
            (milestoneLabelConfig || eventContent).children.push(...value);
          }
          // Likely HTML content
          else if (StringHelper.isHtml(value)) {
            if (eventContent.children.length) {
              childContent = {
                tag: 'span',
                class: 'b-event-text-wrap',
                html: value
              };
            } else {
              eventContent.children = null;
              eventContent.html = value;
            }
          }
          // DOM config or plain string can be used as is
          else if (typeof value === 'string' || typeof value === 'object') {
            childContent = value;
          }
          // Other, use string
          else if (value != null) {
            childContent = String(value);
          }
          // Must allow empty string as valid content
          if (childContent != null) {
            // Milestones have content in their label, other events in their "body"
            (milestoneLabelConfig || eventContent).children.push(childContent);
            renderData.cls.add('b-has-content');
          }
          if (eventContent.html != null || eventContent.children.length) {
            renderData.children.push(eventContent);
          }
        } else {
          eventContent.html = value;
          renderData.children.push(eventContent);
        }
      }
      const {
        eventStyle,
        eventColor,
        wrapperCls
      } = renderData;
      // Renderers have last say on style & color
      wrapperCls[`b-sch-style-${eventStyle || 'none'}`] = 1;
      // Named colors are applied as a class to the wrapper
      if (DomHelper.isNamedColor(eventColor)) {
        wrapperCls[`b-sch-color-${eventColor}`] = eventColor;
      } else if (eventColor) {
        const colorProp = eventStyle ? 'color' : 'background-color',
          style = `${colorProp}:${eventColor};`;
        renderData.style = style + renderData.style;
        wrapperCls['b-sch-custom-color'] = 1;
        renderData._customColorStyle = style; // Saves the styling string to be able to remove it if needed
      } else {
        wrapperCls[`b-sch-color-none`] = 1;
      }
      // Milestones has to apply styling to b-sch-event-content
      if (renderData.style && isMilestone && eventContent) {
        eventContent.style = renderData.style;
        delete renderData.style;
      }
      // If there are any iconCls entries...
      renderData.cls['b-sch-event-withicon'] = (_renderData$iconCls2 = renderData.iconCls) === null || _renderData$iconCls2 === void 0 ? void 0 : _renderData$iconCls2.length;
      // For comparison in sync, cheaper than comparing DocumentFragments
      renderData.eventContent = eventContent;
      renderData.wrapperChildren = [];
      // Method which features may chain in to
      me.onEventDataGenerated(renderData);
    }
    return renderData;
  }
  /**
   * A method which may be chained by features. It is called when an event's render
   * data is calculated so that features may update the style, class list or body.
   * @param {Object} eventData
   * @internal
   */
  onEventDataGenerated(eventData) {}
  //endregion
  //region Initial animation
  changeUseInitialAnimation(name) {
    return name === true ? 'fade-in' : name;
  }
  updateUseInitialAnimation(name, old) {
    const {
      classList
    } = this.element;
    if (old) {
      classList.remove(`b-initial-${old}`);
    }
    if (name) {
      classList.add(`b-initial-${name}`);
      // Transition block for FF, to not interfere with animations
      if (BrowserHelper.isFirefox) {
        classList.add('b-prevent-event-transitions');
      }
    }
  }
  /**
   * Restarts initial events animation with new value {@link #config-useInitialAnimation}.
   * @param {Boolean|String} initialAnimation new initial animation value
   * @category Misc
   */
  restartInitialAnimation(initialAnimation) {
    var _me$initialAnimationD;
    const me = this;
    (_me$initialAnimationD = me.initialAnimationDetacher) === null || _me$initialAnimationD === void 0 ? void 0 : _me$initialAnimationD.call(me);
    me.initialAnimationDetacher = null;
    me.useInitialAnimation = initialAnimation;
    me.isFirstRender = true;
    me.refresh();
  }
  stopInitialAnimation() {
    const me = this;
    me.initialAnimationDetacher();
    me.isFirstRender = false;
    // Prevent any further initial animations
    me.useInitialAnimation = false;
    // Remove transition block for FF a bit later, to not interfere with animations
    if (BrowserHelper.isFirefox) {
      me.setTimeout(() => me.element.classList.remove('b-prevent-event-transitions'), 100);
    }
  }
  //endregion
  //region Milestones
  /**
   * Determines width of a milestones label. How width is determined is decided by configuring
   * {@link #config-milestoneLayoutMode}. Please note that text width is always determined using the events
   * {@link Scheduler/model/EventModel#field-name}.
   * @param {Scheduler.model.EventModel} eventRecord
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @returns {Number}
   * @category Milestones
   */
  getMilestoneLabelWidth(eventRecord, resourceRecord) {
    const me = this,
      mode = me.milestoneLayoutMode,
      size = me.getResourceLayoutSettings(resourceRecord).contentHeight;
    if (mode === 'measure') {
      const html = StringHelper.encodeHtml(eventRecord.name),
        color = me.getEventColor(eventRecord, resourceRecord),
        style = me.getEventStyle(eventRecord, resourceRecord),
        element = me.milestoneMeasureElement || (me.milestoneMeasureElement = DomHelper.createElement({
          className: {
            'b-sch-event-wrap': 1,
            'b-milestone-wrap': 1,
            'b-measure': 1,
            [`b-sch-color-${color}`]: color,
            [`b-sch-style-${style}`]: style
          },
          children: [{
            className: 'b-sch-event b-milestone',
            children: [{
              className: 'b-sch-event-content',
              children: [{
                tag: 'label'
              }]
            }]
          }],
          parent: me.foregroundCanvas
        }));
      // DomSync should not touch
      element.retainElement = true;
      element.style.fontSize = `${size}px`;
      if (me.milestoneTextPosition === 'always-outside') {
        const label = element.firstElementChild.firstElementChild.firstElementChild;
        label.innerHTML = html;
        const bounds = Rectangle.from(label, label.parentElement);
        // +2 for a little margin
        return bounds.left + bounds.width + 2;
      } else {
        // b-sch-event-content
        element.firstElementChild.firstElementChild.innerHTML = `<label></label>${html}`;
        return element.firstElementChild.offsetWidth;
      }
    }
    if (mode === 'estimate') {
      return eventRecord.name.length * me.milestoneCharWidth + (me.milestoneTextPosition === 'always-outside' ? size : 0);
    }
    if (mode === 'data') {
      return eventRecord.milestoneWidth;
    }
    return 0;
  }
  updateMilestoneLayoutMode(mode) {
    const me = this,
      alwaysOutside = me.milestoneTextPosition === 'always-outside';
    me.element.classList.toggle('b-sch-layout-milestones', mode !== 'default' && !alwaysOutside);
    me.element.classList.toggle('b-sch-layout-milestone-labels', mode !== 'default' && alwaysOutside);
    if (!me.isConfiguring) {
      me.refreshWithTransition();
    }
  }
  updateMilestoneTextPosition(position) {
    this.element.classList.toggle('b-sch-layout-milestone-text-position-inside', position === 'inside');
    this.updateMilestoneLayoutMode(this.milestoneLayoutMode);
  }
  updateMilestoneAlign() {
    if (!this.isConfiguring) {
      this.refreshWithTransition();
    }
  }
  updateMilestoneCharWidth() {
    if (!this.isConfiguring) {
      this.refreshWithTransition();
    }
  }
  // endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/data/mixin/ProjectConsumer
 */
const engineStoreNames = ['assignmentStore', 'dependencyStore', 'eventStore', 'resourceStore'];
/**
 * Creates a Project using any configured stores, and sets the stores configured into the project into
 * the host object.
 *
 * @mixin
 */
var ProjectConsumer = (Target => class ProjectConsumer extends (Target || Base) {
  static get $name() {
    return 'ProjectConsumer';
  }
  //region Default config
  static get declarable() {
    return ['projectStores'];
  }
  static get configurable() {
    return {
      projectModelClass: ProjectModel,
      /**
       * The {@link Scheduler.model.ProjectModel} instance, containing the data visualized by the Scheduler.
       *
       * **Note:** In SchedulerPro the project is instance of SchedulerPro.model.ProjectModel class.
       * @member {Scheduler.model.ProjectModel} project
       * @typings {ProjectModel}
       * @category Data
       */
      /**
       * A {@link Scheduler.model.ProjectModel} instance or a config object. The project holds all Scheduler data.
       * Can be omitted in favor of individual store configs or {@link Scheduler.view.mixin.SchedulerStores#config-crudManager} config.
       *
       * **Note:** This config is **mandatory** in SchedulerPro. See SchedulerPro.model.ProjectModel class.
       * @config {Scheduler.model.ProjectModel|ProjectModelConfig} project
       * @category Data
       */
      project: {},
      /**
       * Configure as `true` to destroy the Project and stores when `this` is destroyed.
       * @config {Boolean}
       * @category Data
       */
      destroyStores: null,
      // Will be populated by AttachToProjectMixin which features mix in
      projectSubscribers: []
    };
  }
  #suspendedByRestore;
  //endregion
  startConfigure(config) {
    // process the project first which ingests any configured data sources,
    this.getConfig('project');
    super.startConfigure(config);
  }
  //region Project
  // This is where all the ingestion happens.
  // At config time, the changers inject incoming values into the project config object
  // that we are building. At the end we instantiate the project with all incoming
  // config values filled in.
  changeProject(project, oldProject) {
    const me = this,
      {
        projectStoreNames,
        projectDataNames
      } = me.constructor;
    me.projectCallbacks = new Set();
    if (project) {
      // Flag for changes to know what stage we are at
      me.buildingProjectConfig = true;
      if (!project.isModel) {
        // When configuring, prio order:
        // 1. If using an already existing CrudManager, it is assumed to already have the stores we should use,
        //    adopt them as ours.
        // 2. If a supplied store already has a project, it is assumed to be shared with another scheduler and
        //    that project is adopted as ours. Unless we are given some store not part of that project,
        //    in which case we create a new project.
        // 3. Use stores from a supplied project config.
        // 4. Use stores configured on scheduler.
        // + Pass on inline data (events, resources, dependencies, assignments -> xxData on the project config)
        //
        // What happens during project initialization is this:
        // this._project is the project *config* object.
        // changeXxxx methods put incoming values directly into it through this.project
        // to be used as its configuration.
        // So when it is instantiated, it has had all configs injected.
        if (me.isConfiguring) {
          // Set property for changers to put incoming values into
          me._project = project;
          // crudManager will be a clone of the raw config if it is a raw config.
          const {
            crudManager
          } = me;
          // Pull in stores from the crudManager config first
          if (crudManager) {
            const {
              isCrudManager
            } = crudManager;
            for (const storeName of projectStoreNames) {
              if (crudManager[storeName]) {
                // We configure the project with the stores, and *not* the CrudManager.
                // The CrudManager ends up having its project set and thereby adopting ours.
                me[storeName] = crudManager[storeName];
                // If it's just a config, take the stores out.
                // We will *configure* it with this project and it will ingest
                // its stores from there.
                if (!isCrudManager) {
                  delete crudManager[storeName];
                }
              }
            }
          }
          // Pull in all our configured stores into the project config object.
          // That also extracts any project into this._sharedProject
          me.getConfig('projectStores');
          // Referencing these data configs causes them to be pulled into
          // the _project.xxxData config property if they are present.
          for (const dataName of projectDataNames) {
            me.getConfig(dataName);
          }
        }
        const {
          eventStore
        } = project;
        let {
          _sharedProject: sharedProject
        } = me;
        // Delay autoLoading until listeners are set up, to be able to inject params
        if (eventStore && !eventStore.isEventStoreMixin && eventStore.autoLoad && !eventStore.count) {
          eventStore.autoLoad = false;
          me.delayAutoLoad = true;
        }
        // We should not adopt a project from a store if we are given any store not part of that project
        if (sharedProject && engineStoreNames.some(store => project[store] && project[store] !== sharedProject[store])) {
          // We have to chain any store used by the other project, they can only belong to one
          for (const store of engineStoreNames) {
            if (project[store] && project[store] === sharedProject[store]) {
              project[store] = project[store].chain();
            }
          }
          sharedProject = null;
        }
        // Use sharedProject if found, else instantiate our config.
        project = sharedProject || new me.projectModelClass(project);
        // Clear the property so that the updater is called.
        delete me._project;
      }
      // In the updater, configs are live
      me.buildingProjectConfig = false;
    }
    return project;
  }
  /**
   * Implement in subclass to take action when project is replaced.
   *
   * __`super.updateProject(...arguments)` must be called first.__
   *
   * @param {Scheduler.model.ProjectModel} project
   * @category Data
   */
  updateProject(project, oldProject) {
    const me = this,
      {
        projectListeners,
        crudManager
      } = me;
    me.detachListeners('projectConsumer');
    // When we set the crudManager now, it will go through to the CrudManagerVIew
    delete me._crudManager;
    if (project) {
      var _project$stm;
      projectListeners.thisObj = me;
      project.ion(projectListeners);
      // If the project is a CrudManager, use it as such.
      if (project.isCrudManager) {
        me.crudManager = project;
      }
      // Apply the project to CrudManager, making sure the same stores are used there and here
      else if (crudManager) {
        crudManager.project = project;
        // CrudManager goes through the changer as usual and is initialized
        // from the Project, not any stores it was originally configured with.
        me.crudManager = crudManager;
      }
      // Notifies classes that mix AttachToProjectMixin that we have a new project
      me.projectSubscribers.forEach(subscriber => {
        subscriber.detachFromProject(oldProject);
        subscriber.attachToProject(project);
      });
      // Sets the project's stores into the host object
      for (const storeName of me.constructor.projectStoreNames) {
        me[storeName] = project[storeName];
      }
      // Listeners are set up, if EventStore was configured with autoLoad now is the time to load
      if (me.delayAutoLoad) {
        // Restore the flag, not needed but to look good on inspection
        project.eventStore.autoLoad = true;
        project.eventStore.load();
      }
      (_project$stm = project.stm) === null || _project$stm === void 0 ? void 0 : _project$stm.ion({
        name: 'projectConsumer',
        restoringStart: 'onProjectRestoringStart',
        restoringStop: 'onProjectRestoringStop',
        thisObj: me
      });
    }
    me.trigger('projectChange', {
      project
    });
  }
  // Implementation here because we need to get first look at it to adopt its stores
  changeCrudManager(crudManager) {
    // Set the property to be scanned for incoming stores.
    // If it's a config, it will be stripped of those stores prior to construction.
    if (this.buildingProjectConfig) {
      this._crudManager = crudManager.isCrudManager ? crudManager : Object.assign({}, crudManager);
    } else {
      return super.changeCrudManager(crudManager);
    }
  }
  // Called when project changes are committed, after data is written back to records
  onProjectDataReady() {
    const me = this;
    // Only update the UI when we are visible
    me.whenVisible(() => {
      if (me.projectCallbacks.size) {
        me.projectCallbacks.forEach(callback => callback());
        me.projectCallbacks.clear();
      }
    }, null, null, 'onProjectDataReady');
  }
  onProjectRestoringStart({
    stm
  }) {
    const {
      rawQueue
    } = stm;
    // Suspend refresh if undo/redo potentially leads to multiple refreshes
    if (rawQueue.length && rawQueue[rawQueue.length - 1].length > 1) {
      this.#suspendedByRestore = true;
      this.suspendRefresh();
    }
  }
  onProjectRestoringStop() {
    if (this.#suspendedByRestore) {
      this.#suspendedByRestore = false;
      this.resumeRefresh(true);
    }
  }
  // Overridden in CalendarStores.js
  onBeforeTimeZoneChange() {}
  // When project changes time zone, change start and end dates
  onTimeZoneChange({
    timeZone,
    oldTimeZone
  }) {
    const me = this;
    // The timeAxis timeZone could be equal to timeZone if we are a partnered scheduler
    if (me.startDate && me.timeAxis.timeZone !== timeZone) {
      const startDate = oldTimeZone != null ? TimeZoneHelper.fromTimeZone(me.startDate, oldTimeZone) : me.startDate;
      me.startDate = timeZone != null ? TimeZoneHelper.toTimeZone(startDate, timeZone) : startDate;
      // Saves the timeZone on the timeAxis as it is shared between partnered schedulers
      me.timeAxis.timeZone = timeZone;
    }
  }
  onStartApplyChangeset() {
    this.suspendRefresh();
  }
  onEndApplyChangeset() {
    this.resumeRefresh(true);
  }
  /**
   * Accepts a callback that will be called when the underlying project is ready (no commit pending and current commit
   * finalized)
   * @param {Function} callback
   * @category Data
   */
  whenProjectReady(callback) {
    // Might already be ready, call directly
    if (this.isEngineReady) {
      callback();
    } else {
      this.projectCallbacks.add(callback);
    }
  }
  /**
   * Returns `true` if engine is in a stable calculated state, `false` otherwise.
   * @property {Boolean}
   * @category Misc
   */
  get isEngineReady() {
    var _this$project$isEngin, _this$project;
    // NonWorkingTime calls this during destruction, hence the ?.
    return Boolean((_this$project$isEngin = (_this$project = this.project).isEngineReady) === null || _this$project$isEngin === void 0 ? void 0 : _this$project$isEngin.call(_this$project));
  }
  //endregion
  //region Destroy
  // Cleanup, destroys stores if this.destroyStores is true.
  doDestroy() {
    super.doDestroy();
    if (this.destroyStores) {
      // Shared project might already be destroyed
      !this.project.isDestroyed && this.project.destroy();
    }
  }
  //endregion
  get projectStores() {
    const {
      projectStoreNames
    } = this.constructor;
    return projectStoreNames.map(storeName => this[storeName]);
  }
  static get projectStoreNames() {
    return Object.keys(this.projectStores);
  }
  static get projectDataNames() {
    return this.projectStoreNames.reduce((result, storeName) => {
      const {
        dataName
      } = this.projectStores[storeName];
      if (dataName) {
        result.push(dataName);
      }
      return result;
    }, []);
  }
  static setupProjectStores(cls, meta) {
    const {
      projectStores
    } = cls;
    if (projectStores) {
      const projectListeners = {
          name: 'projectConsumer',
          dataReady: 'onProjectDataReady',
          change: 'relayProjectDataChange',
          beforeTimeZoneChange: 'onBeforeTimeZoneChange',
          timeZoneChange: 'onTimeZoneChange',
          startApplyChangeset: 'onStartApplyChangeset',
          endApplyChangeset: 'onEndApplyChangeset'
        },
        storeConfigs = {
          projectListeners
        };
      let previousDataName;
      // Create a property and updater for each dataName and a changer for each store
      for (const storeName in projectStores) {
        const {
          dataName
        } = projectStores[storeName];
        // Define "eventStore" and "events" configs
        storeConfigs[storeName] = storeConfigs[dataName] = null;
        // Define up the "events" property
        if (dataName) {
          // Getter to return store data
          Object.defineProperty(meta.class.prototype, dataName, {
            configurable: true,
            // So that Config can add its setter.
            get() {
              var _this$project$storeNa;
              // get events() { return this.project.eventStore.records; }
              return (_this$project$storeNa = this.project[storeName]) === null || _this$project$storeNa === void 0 ? void 0 : _this$project$storeNa.records;
            }
          });
          // Create an updater for the data name;
          this.createDataUpdater(storeName, dataName, previousDataName, meta);
        }
        this.createStoreDescriptor(meta, storeName, projectStores[storeName], projectListeners);
        // The next data updater must reference this data name
        previousDataName = dataName;
      }
      // Create the projectListeners config.
      this.setupConfigs(meta, storeConfigs);
    }
  }
  static createDataUpdater(storeName, dataName, previousDataName, meta) {
    // Create eg "updateEvents(data)".
    // We need it to call this.getConfig('resources') so that ordering of
    // data ingestion is corrected.
    meta.class.prototype[`update${StringHelper.capitalize(dataName)}`] = function (data) {
      const {
        project
      } = this;
      // Ensure a dataName that we depend on is called in.
      // For example dependencies must load in order after the events.
      previousDataName && this.getConfig(previousDataName);
      if (this.buildingProjectConfig) {
        // Set the property in the project config object.
        // eg project.eventsData = [...]
        project[`${dataName}Data`] = data;
      } else {
        // Live update the project when in use.
        project[storeName].data = data;
      }
    };
  }
  // eslint-disable-next-line bryntum/no-listeners-in-lib
  static createStoreDescriptor(meta, storeName, {
    listeners
  }, projectListeners) {
    const {
        prototype: clsProto
      } = meta.class,
      storeNameCap = StringHelper.capitalize(storeName);
    // Set up onProjectEventStoreChange to set this.eventStore
    projectListeners[`${storeName}Change`] = function ({
      store
    }) {
      this[storeName] = store;
    };
    // create changeEventStore
    clsProto[`change${storeNameCap}`] = function (store, oldStore) {
      var _store;
      const me = this,
        {
          project
        } = me,
        storeProject = (_store = store) === null || _store === void 0 ? void 0 : _store.project;
      if (me.buildingProjectConfig) {
        // Capture any project found at project config time
        // to use as our shared project
        if (storeProject !== null && storeProject !== void 0 && storeProject.isProjectModel) {
          me._sharedProject = storeProject;
        }
        // Set the property in the project config object.
        // Must not go through the updater. It's too early to
        // inform host of store change.
        project[storeName] = store;
        return;
      }
      // Live update the project when in use.
      if (!me.initializingProject) {
        if (project[storeName] !== store) {
          project[`set${storeNameCap}`](store);
          store = project[storeName];
        }
      }
      // Implement processing here instead of creating a separate updater.
      // Subclasses can implement updaters.
      if (store !== oldStore) {
        if (listeners) {
          listeners.thisObj = me;
          listeners.name = `${storeName}Listeners`;
          me.detachListeners(listeners.name);
          store.ion(listeners);
        }
        // Set backing var temporarily, so it can be accessed from AttachToProjectMixin subscribers
        me[`_${storeName}`] = store;
        // Notifies classes that mix AttachToProjectMixin that we have a new XxxxxStore
        me.projectSubscribers.forEach(subscriber => {
          var _subscriber;
          (_subscriber = subscriber[`attachTo${storeNameCap}`]) === null || _subscriber === void 0 ? void 0 : _subscriber.call(subscriber, store);
        });
        me[`_${storeName}`] = null;
      }
      return store;
    };
  }
  relayProjectDataChange(event) {
    // Don't trigger change event for tree node collapse/expand
    if ((event.isExpand || event.isCollapse) && !event.records[0].fieldMap.expanded.persist) {
      return;
    }
    /**
     * Fired when data in any of the projects stores changes.
     *
     * Basically a relayed version of each store's own change event, decorated with which store it originates from.
     * See the {@link Core.data.Store#event-change store change event} documentation for more information.
     *
     * @event dataChange
     * @param {Scheduler.data.mixin.ProjectConsumer} source Owning component
     * @param {Scheduler.model.mixin.ProjectModelMixin} project Project model
     * @param {Core.data.Store} store Affected store
     * @param {'remove'|'removeAll'|'add'|'updatemultiple'|'clearchanges'|'filter'|'update'|'dataset'|'replace'} action
     * Name of action which triggered the change. May be one of:
     * * `'remove'`
     * * `'removeAll'`
     * * `'add'`
     * * `'updatemultiple'`
     * * `'clearchanges'`
     * * `'filter'`
     * * `'update'`
     * * `'dataset'`
     * * `'replace'`
     * @param {Core.data.Model} record Changed record, for actions that affects exactly one record (`'update'`)
     * @param {Core.data.Model[]} records Changed records, passed for all actions except `'removeAll'`
     * @param {Object} changes Passed for the `'update'` action, info on which record fields changed
     */
    return this.trigger('dataChange', {
      project: event.source,
      ...event,
      source: this
    });
  }
  //region WidgetClass
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
  //endregion
});

/**
 * @module Scheduler/view/mixin/SchedulerStores
 */
/**
 * Functions for store assignment and store event listeners.
 *
 * @mixin
 * @extends Scheduler/data/mixin/ProjectConsumer
 */
var SchedulerStores = (Target => class SchedulerStores extends ProjectConsumer(Target || Base) {
  static get $name() {
    return 'SchedulerStores';
  }
  //region Default config
  // This is the static definition of the Stores we consume from the project, and
  // which we must provide *TO* the project if we or our CrudManager is configured
  // with them.
  // The property name is the store name, and within that there is the dataName which
  // is the property which provides static data definition. And there is a listeners
  // definition which specifies the listeners *on this object* for each store.
  //
  // To process incoming stores, implement an updateXxxxxStore method such
  // as `updateEventStore(eventStore)`.
  //
  // To process an incoming Project implement `updateProject`. __Note that
  // `super.updateProject(...arguments)` must be called first.__
  static get projectStores() {
    return {
      resourceStore: {
        dataName: 'resources'
      },
      eventStore: {
        dataName: 'events',
        // eslint-disable-next-line bryntum/no-listeners-in-lib
        listeners: {
          batchedUpdate: 'onEventStoreBatchedUpdate',
          changePreCommit: 'onInternalEventStoreChange',
          commitStart: 'onEventCommitStart',
          commit: 'onEventCommit',
          exception: 'onEventException',
          idchange: 'onEventIdChange',
          beforeLoad: 'onBeforeLoad'
        }
      },
      assignmentStore: {
        dataName: 'assignments',
        // eslint-disable-next-line bryntum/no-listeners-in-lib
        listeners: {
          changePreCommit: 'onAssignmentChange',
          // In EventSelection.js
          commitStart: 'onAssignmentCommitStart',
          commit: 'onAssignmentCommit',
          exception: 'onAssignmentException',
          beforeRemove: {
            fn: 'onAssignmentBeforeRemove',
            // We must go last in case an app vetoes a remove
            // by returning false from a handler.
            prio: -1000
          }
        }
      },
      dependencyStore: {
        dataName: 'dependencies'
      },
      calendarManagerStore: {},
      timeRangeStore: {},
      resourceTimeRangeStore: {}
    };
  }
  static get configurable() {
    return {
      /**
       * Overridden to *not* auto create a store at the Scheduler level.
       * The store is the {@link Scheduler.data.ResourceStore} of the backing project
       * @config {Core.data.Store}
       * @private
       */
      store: null,
      /**
       * The name of the start date parameter that will be passed to in every `eventStore` load request.
       * @config {String}
       * @category Data
       */
      startParamName: 'startDate',
      /**
       * The name of the end date parameter that will be passed to in every `eventStore` load request.
       * @config {String}
       * @category Data
       */
      endParamName: 'endDate',
      /**
       * Set to true to include `startDate` and `endDate` params indicating the currently viewed date range.
       * Dates are formatted using the same format as the `startDate` field on the EventModel
       * (e.g. 2023-03-08T00:00:00+01:00).
       *
       * Enabled by default in version 6.0 and above.
       *
       * @config {Boolean}
       */
      passStartEndParameters: VersionHelper.checkVersion('core', '6.0', '>='),
      /**
       * Class that should be used to instantiate a CrudManager in case it's provided as a simple object to
       * {@link #config-crudManager} config.
       * @config {Scheduler.data.CrudManager}
       * @typings {typeof CrudManager}
       * @category Data
       */
      crudManagerClass: null,
      /**
       * Get/set the CrudManager instance
       * @member {Scheduler.data.CrudManager} crudManager
       * @category Data
       */
      /**
       * Supply a {@link Scheduler.data.CrudManager} instance or a config object if you want to use
       * CrudManager for handling data.
       * @config {CrudManagerConfig|Scheduler.data.CrudManager}
       * @category Data
       */
      crudManager: null
    };
  }
  //endregion
  //region Project
  updateProject(project, oldProject) {
    super.updateProject(project, oldProject);
    this.detachListeners('schedulerStores');
    project.ion({
      name: 'schedulerStores',
      refresh: 'onProjectRefresh',
      thisObj: this
    });
  }
  // Called when project changes are committed, before data is written back to records (but still ready to render
  // since data is fetched from engine)
  onProjectRefresh({
    isInitialCommit
  }) {
    const me = this;
    // Only update the UI immediately if we are visible
    if (me.isVisible) {
      if (isInitialCommit) {
        if (me.isVertical) {
          me.refreshAfterProjectRefresh = false;
          me.refreshWithTransition();
        }
      }
      if (me.navigateToAfterRefresh) {
        me.navigateTo(me.navigateToAfterRefresh);
        me.navigateToAfterRefresh = null;
      }
      if (me.refreshAfterProjectRefresh) {
        me.refreshWithTransition(false, !isInitialCommit);
        me.refreshAfterProjectRefresh = false;
      }
    }
    // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
    else {
      me.whenVisible('refresh', me, [true]);
    }
  }
  //endregion
  //region CrudManager
  changeCrudManager(crudManager) {
    const me = this;
    if (crudManager && !crudManager.isCrudManager) {
      // CrudManager injects itself into is Scheduler's _crudManager property
      // because code it triggers needs to access it through its getter.
      crudManager = me.crudManagerClass.new({
        scheduler: me
      }, crudManager);
    }
    // config setter will veto because of above described behaviour
    // of setting the property early on creation
    me._crudManager = crudManager;
    me.bindCrudManager(crudManager);
  }
  //endregion
  //region Row store
  get store() {
    // Vertical uses a dummy store
    if (!this._store && this.isVertical) {
      this._store = new Store({
        data: [{
          id: 'verticalTimeAxisRow',
          cls: 'b-verticaltimeaxis-row'
        }]
      });
    }
    return super.store;
  }
  set store(store) {
    super.store = store;
  }
  // Wrap w/ transition refreshFromRowOnStoreAdd() inherited from Grid
  refreshFromRowOnStoreAdd(row, {
    isExpand,
    records
  }) {
    const args = arguments;
    this.runWithTransition(() => {
      // Postpone drawing of events for a new resource until the following project refresh. Previously the draw
      // would not happen because engine was not ready, but now when we allow commits and can read values during
      // commit that block is no longer there
      this.currentOrientation.suspended = !isExpand && !records.some(r => r.isLinked);
      super.refreshFromRowOnStoreAdd(row, ...args);
      this.currentOrientation.suspended = false;
    }, !isExpand);
  }
  onStoreAdd(event) {
    super.onStoreAdd(event);
    if (this.isPainted) {
      this.calculateRowHeights(event.records);
    }
  }
  onStoreUpdateRecord({
    source: store,
    record,
    changes
  }) {
    // Ignore engine changes that do not affect row rendering
    let ignoreCount = 0;
    if ('assigned' in changes) {
      ignoreCount++;
    }
    if ('calendar' in changes) {
      ignoreCount++;
    }
    if (ignoreCount !== Object.keys(changes).length) {
      super.onStoreUpdateRecord(...arguments);
    }
  }
  //endregion
  //region ResourceStore
  updateResourceStore(resourceStore) {
    // Reconfigure grid if resourceStore is backing the rows
    if (resourceStore && this.isHorizontal) {
      resourceStore.metaMapId = this.id;
      this.store = resourceStore;
    }
  }
  get usesDisplayStore() {
    return this.store !== this.resourceStore;
  }
  //endregion
  //region Events
  onEventIdChange(params) {
    this.currentOrientation.onEventStoreIdChange && this.currentOrientation.onEventStoreIdChange(params);
  }
  /**
   * Listener to the batchedUpdate event which fires when a field is changed on a record which
   * is batch updating. Occasionally UIs must keep in sync with batched changes.
   * For example, the EventResize feature performs batched updating of the startDate/endDate
   * and it tells its client to listen to batchedUpdate.
   * @private
   */
  onEventStoreBatchedUpdate(event) {
    if (this.listenToBatchedUpdates) {
      return this.onInternalEventStoreChange(event);
    }
  }
  /**
   * Calls appropriate functions for current event layout when the event store is modified.
   * @private
   */
  // Named as Internal to avoid naming collision with wrappers that relay events
  onInternalEventStoreChange(params) {
    // Too early, bail out
    // Also bail out if this is a reassign using resourceId, any updates will be handled by AssignmentStore instead
    if (!this.isPainted || !this._mode || params.isAssign || this.assignmentStore.isRemovingAssignment) {
      return;
    }
    // Only respond if we are visible. If not, defer until we are shown
    if (this.isVisible) {
      this.currentOrientation.onEventStoreChange(params);
    } else {
      this.whenVisible(this.onInternalEventStoreChange, this, [params]);
    }
  }
  /**
   * Refreshes committed events, to remove dirty/committing flag.
   * CSS is added
   * @private
   */
  onEventCommit({
    changes
  }) {
    let resourcesToRepaint = [...changes.added, ...changes.modified].map(eventRecord => this.eventStore.getResourcesForEvent(eventRecord));
    // getResourcesForEvent returns an array, so need to flatten resourcesToRepaint
    resourcesToRepaint = Array.prototype.concat.apply([], resourcesToRepaint);
    // repaint relevant resource rows
    new Set(resourcesToRepaint).forEach(resourceRecord => this.repaintEventsForResource(resourceRecord));
  }
  /**
   * Adds the committing flag to changed events before commit.
   * @private
   */
  onEventCommitStart({
    changes
  }) {
    const {
      currentOrientation,
      committingCls
    } = this;
    // Committing sets a flag in meta that during event rendering applies a CSS class. But to not mess up drag and
    // drop between resources no redraw is performed before committing, so class is never applied to the element(s).
    // Applying here instead
    [...changes.added, ...changes.modified].forEach(eventRecord => eventRecord.assignments.forEach(assignmentRecord => currentOrientation.toggleCls(assignmentRecord, committingCls, true)));
  }
  // Clear committing flag
  onEventException({
    action
  }) {
    if (action === 'commit') {
      const {
        changes
      } = this.eventStore;
      [...changes.added, ...changes.modified, ...changes.removed].forEach(eventRecord => this.repaintEvent(eventRecord));
    }
  }
  onAssignmentCommit({
    changes
  }) {
    this.repaintEventsForAssignmentChanges(changes);
  }
  onAssignmentCommitStart({
    changes
  }) {
    const {
      currentOrientation,
      committingCls
    } = this;
    [...changes.added, ...changes.modified].forEach(assignmentRecord => {
      currentOrientation.toggleCls(assignmentRecord, committingCls, true);
    });
  }
  // Clear committing flag
  onAssignmentException({
    action
  }) {
    if (action === 'commit') {
      this.repaintEventsForAssignmentChanges(this.assignmentStore.changes);
    }
  }
  repaintEventsForAssignmentChanges(changes) {
    const resourcesToRepaint = [...changes.added, ...changes.modified, ...changes.removed].map(assignmentRecord => assignmentRecord.getResource());
    // repaint relevant resource rows
    new Set(resourcesToRepaint).forEach(resourceRecord => this.repaintEventsForResource(resourceRecord));
  }
  onAssignmentBeforeRemove({
    records,
    removingAll
  }) {
    if (removingAll) {
      return;
    }
    const me = this;
    let moveTo;
    // Deassigning the active assignment
    if (!me.isConfiguring && (
    // If we have current active assignment or we scheduled navigating to an assignment, we should check
    // if we're removing that assignment in order to avoid navigating to it
    me.navigateToAfterRefresh || me.activeAssignment && records.includes(me.activeAssignment))) {
      // If next navigation target is removed, clean up the flag
      if (records.includes(me.navigateToAfterRefresh)) {
        me.navigateToAfterRefresh = null;
      }
      // If being done by a keyboard gesture then look for a close target until we find an existing record, not
      // scheduled for removal. Otherwise, push focus outside of the Scheduler.
      // This condition will react not only on meaningful keyboard action - like pressing DELETE key on selected
      // event - but also in case user started dragging and pressed CTRL (or any other key) in process.
      // https://github.com/bryntum/support/issues/3479
      if (GlobalEvents.lastInteractionType === 'key') {
        // Look for a close target until we find an existing record, not scheduled for removal. Provided
        // assignment position in store is arbitrary as well as order of removed records, it does not make much
        // sense trying to apply any specific order to them. Existing assignment next to any removed one is as
        // good as any.
        for (let i = 0, l = records.length; i < l && !moveTo; i++) {
          const assignment = records[i];
          if (assignment.resource && assignment.resource.isModel) {
            // Find next record
            let next = me.getNext(assignment);
            // If next record is not found or also removed, look for previous. This should not become a
            // performance bottleneck because we only can get to this code if project is committing, if
            // records are removed on a dragdrop listener and user pressed any key after mousedown, or if
            // user is operating with a keyboard and pressed [DELETE] to remove multiple records.
            if (!next || records.includes(next)) {
              next = me.getPrevious(assignment);
            }
            if (next && !records.includes(next)) {
              moveTo = next;
            }
          }
        }
      }
      // Move focus away from the element which will soon have no backing data.
      if (moveTo) {
        // Although removing records from assignment store will trigger project commit and consequently
        // `refresh` event on the project which will use this record to navigate to, some tests expect
        // immediate navigation
        me.navigateTo(moveTo);
        me.navigateToAfterRefresh = moveTo;
      }
      // Focus must exit the Scheduler's subgrid, otherwise, if a navigation
      // key gesture is delivered before the outgoing event's element has faded
      // out and been removed, navigation will be attempted from a deleted
      // event. Animated hiding is problematic.
      //
      // We cannot just revertFocus() because that might move focus back to an
      // element in a floating EventEditor which is not yet faded out and
      // been removed. Animated hiding is problematic.
      //
      // We cannot focus scheduler.timeAxisColumn.element because the browser
      // would scroll it in some way if we have horizontal overflow.
      //
      // The only thing we can know about to focus here is the Scheduler itself.
      else {
        DomHelper.focusWithoutScrolling(me.focusElement);
      }
    }
  }
  //endregion
  //region TimeRangeStore & TimeRanges
  /**
   * Inline time ranges, will be loaded into an internally created store if {@link Scheduler.feature.TimeRanges}
   * is enabled.
   * @config {Scheduler.model.TimeSpan[]|TimeSpanConfig[]} timeRanges
   * @category Data
   */
  /**
   * Get/set time ranges, applies to the backing project's TimeRangeStore.
   * @member {Scheduler.model.TimeSpan[]} timeRanges
   * @accepts {Scheduler.model.TimeSpan[]|TimeSpanConfig[]}
   * @category Data
   */
  /**
   * Get/set the time ranges store instance or config object for {@link Scheduler.feature.TimeRanges} feature.
   * @member {Core.data.Store} timeRangeStore
   * @accepts {Core.data.Store|StoreConfig}
   * @category Data
   */
  /**
   * The time ranges store instance for {@link Scheduler.feature.TimeRanges} feature.
   * @config {Core.data.Store|StoreConfig} timeRangeStore
   * @category Data
   */
  set timeRanges(timeRanges) {
    this.project.timeRanges = timeRanges;
  }
  get timeRanges() {
    return this.project.timeRanges;
  }
  //endregion
  //region ResourceTimeRangeStore
  /**
   * Inline resource time ranges, will be loaded into an internally created store if
   * {@link Scheduler.feature.ResourceTimeRanges} is enabled.
   * @prp {Scheduler.model.ResourceTimeRangeModel[]} resourceTimeRanges
   * @accepts {Scheduler.model.ResourceTimeRangeModel[]|ResourceTimeRangeModelConfig[]}
   * @category Data
   */
  /**
   * Get/set the resource time ranges store instance for {@link Scheduler.feature.ResourceTimeRanges} feature.
   * @member {Scheduler.data.ResourceTimeRangeStore} resourceTimeRangeStore
   * @accepts {Scheduler.data.ResourceTimeRangeStore|ResourceTimeRangeStoreConfig}
   * @category Data
   */
  /**
   * Resource time ranges store instance or config object for {@link Scheduler.feature.ResourceTimeRanges} feature.
   * @config {Scheduler.data.ResourceTimeRangeStore|ResourceTimeRangeStoreConfig} resourceTimeRangeStore
   * @category Data
   */
  set resourceTimeRanges(resourceTimeRanges) {
    this.project.resourceTimeRanges = resourceTimeRanges;
  }
  get resourceTimeRanges() {
    return this.project.resourceTimeRanges;
  }
  //endregion
  //region Other functions
  onBeforeLoad({
    params
  }) {
    this.applyStartEndParameters(params);
  }
  /**
   * Get events grouped by timeAxis ticks from resources array
   * @category Data
   * @param {Scheduler.model.ResourceModel[]} resources An array of resources to process. If not passed, all resources
   * will be used.
   * @param {Function} filterFn filter function to filter events if required. Optional.
   * @private
   */
  getResourcesEventsPerTick(resources, filterFn) {
    const {
        timeAxis,
        resourceStore
      } = this,
      eventsByTick = [];
    resources = resources || resourceStore.records;
    resources.forEach(resource => {
      resource.events.forEach(event => {
        if (!timeAxis.isTimeSpanInAxis(event) || filterFn && !filterFn.call(this, {
          resource,
          event
        })) {
          return;
        }
        // getTickFromDate may return float if event starts/ends in a middle of a tick
        let startTick = Math.floor(timeAxis.getTickFromDate(event.startDate)),
          endTick = Math.ceil(timeAxis.getTickFromDate(event.endDate));
        // if startDate/endDate of the event is out of timeAxis' bounds, use first/last tick id instead
        if (startTick == -1) {
          startTick = 0;
        }
        if (endTick === -1) {
          endTick = timeAxis.ticks.length;
        }
        do {
          if (!eventsByTick[startTick]) {
            eventsByTick[startTick] = [event];
          } else {
            eventsByTick[startTick].push(event);
          }
        } while (++startTick < endTick);
      });
    });
    return eventsByTick;
  }
  //endregion
  //region WidgetClass
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
  //endregion
});

/**
 * @module Scheduler/view/mixin/SchedulerScroll
 */
const defaultScrollOptions = {
    block: 'nearest',
    edgeOffset: 20
  },
  unrenderedScrollOptions = {
    highlight: false,
    focus: false
  };
/**
 * Functions for scrolling to events, dates etc.
 *
 * @mixin
 */
var SchedulerScroll = (Target => class SchedulerScroll extends (Target || Base) {
  static get $name() {
    return 'SchedulerScroll';
  }
  //region Scroll to event
  /**
   * Scrolls an event record into the viewport.
   * If the resource store is a tree store, this method will also expand all relevant parent nodes to locate the event.
   *
   * This function is not applicable for events with multiple assignments, please use #scrollResourceEventIntoView instead.
   *
   * @param {Scheduler.model.EventModel} eventRecord the event record to scroll into view
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @async
   * @category Scrolling
   */
  async scrollEventIntoView(eventRecord, options = defaultScrollOptions) {
    const me = this,
      resources = eventRecord.resources || [eventRecord];
    if (resources.length > 1) {
      throw new Error('scrollEventIntoView() is not applicable for events with multiple assignments, please use scrollResourceEventIntoView() instead.');
    }
    if (!resources.length) {
      console.warn('You have asked to scroll to an event which is not assigned to a resource');
    }
    await me.scrollResourceEventIntoView(resources[0], eventRecord, options);
  }
  /**
   * Scrolls an assignment record into the viewport.
   *
   * If the resource store is a tree store, this method will also expand all relevant parent nodes
   * to locate the event.
   *
   * @param {Scheduler.model.AssignmentModel} assignmentRecord A resource record an event record is assigned to
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @category Scrolling
   */
  scrollAssignmentIntoView(assignmentRecord, ...args) {
    return this.scrollResourceEventIntoView(assignmentRecord.resource, assignmentRecord.event, ...args);
  }
  /**
   * Scrolls a resource event record into the viewport.
   *
   * If the resource store is a tree store, this method will also expand all relevant parent nodes
   * to locate the event.
   *
   * @param {Scheduler.model.ResourceModel} resourceRecord A resource record an event record is assigned to
   * @param {Scheduler.model.EventModel} eventRecord An event record to scroll into view
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A Promise which resolves when the scrolling is complete.
   * @category Scrolling
   * @async
   */
  async scrollResourceEventIntoView(resourceRecord, eventRecord, options = defaultScrollOptions) {
    const me = this,
      eventStart = eventRecord.startDate,
      eventEnd = eventRecord.endDate,
      eventIsOutside = eventRecord.isScheduled && eventStart < me.timeAxis.startDate | (eventEnd > me.timeAxis.endDate) << 1;
    if (arguments.length > 3) {
      options = arguments[3];
    }
    let el;
    if (options.edgeOffset == null) {
      options.edgeOffset = 20;
    }
    // Make sure event is within TimeAxis time span unless extendTimeAxis passed as false.
    // The EventEdit feature passes false because it must not mutate the TimeAxis.
    // Bitwise flag:
    //  1 === start is before TimeAxis start.
    //  2 === end is after TimeAxis end.
    if (eventIsOutside && options.extendTimeAxis !== false) {
      const currentTimeSpanRange = me.timeAxis.endDate - me.timeAxis.startDate;
      // Event is too wide, expand the range to encompass it.
      if (eventIsOutside === 3) {
        await me.setTimeSpan(new Date(eventStart.valueOf() - currentTimeSpanRange / 2), new Date(eventEnd.valueOf() + currentTimeSpanRange / 2));
      } else if (me.infiniteScroll) {
        const {
            visibleDateRange
          } = me,
          visibleMS = visibleDateRange.endMS - visibleDateRange.startMS,
          // If event starts before time axis, scroll to a date one full viewport after target date
          // (reverse for an event starting after time axis), to allow a scroll animation
          sign = eventIsOutside & 1 ? 1 : -1;
        await me.setTimeSpan(new Date(eventStart.valueOf() - currentTimeSpanRange / 2), new Date(eventStart.valueOf() + currentTimeSpanRange / 2), {
          visibleDate: new Date(eventEnd.valueOf() + sign * visibleMS)
        });
      }
      // Event is partially or wholly outside but will fit.
      // Move the TimeAxis to include it. That will maintain visual position.
      else {
        // Event starts before
        if (eventIsOutside & 1) {
          await me.setTimeSpan(new Date(eventStart), new Date(eventStart.valueOf() + currentTimeSpanRange));
        }
        // Event ends after
        else {
          await me.setTimeSpan(new Date(eventEnd.valueOf() - currentTimeSpanRange), new Date(eventEnd));
        }
      }
    }
    if (me.store.tree) {
      var _me$expandTo;
      // If we're a tree, ensure parents are expanded first
      await ((_me$expandTo = me.expandTo) === null || _me$expandTo === void 0 ? void 0 : _me$expandTo.call(me, resourceRecord));
    }
    // Handle nested events too
    if (eventRecord.parent && !eventRecord.parent.isRoot) {
      await this.scrollEventIntoView(eventRecord.parent);
    }
    // Establishing element to scroll to
    el = me.getElementFromEventRecord(eventRecord, resourceRecord);
    if (el) {
      // It's usually the event wrapper that holds focus
      if (!DomHelper.isFocusable(el)) {
        el = el.parentNode;
      }
      const scroller = me.timeAxisSubGrid.scrollable;
      // Scroll into view with animation and highlighting if needed.
      await scroller.scrollIntoView(el, options);
    }
    // If event is fully outside the range, and we are not allowed to extend
    // the range, then we cannot perform the operation.
    else if (eventIsOutside === 3 && options.extendTimeAxis === false) {
      console.warn('You have asked to scroll to an event which is outside the current view and extending timeaxis is disabled');
    } else if (!eventRecord.isOccurrence && !me.eventStore.isAvailable(eventRecord)) {
      console.warn('You have asked to scroll to an event which is not available');
    } else if (eventRecord.isScheduled) {
      // Event scheduled but not rendered, scroll to calculated location
      await me.scrollUnrenderedEventIntoView(resourceRecord, eventRecord, options);
    } else {
      // Event not scheduled, just scroll resource row into view
      await me.scrollResourceIntoView(resourceRecord, options);
    }
  }
  /**
   * Scrolls an unrendered event into view. Internal function used from #scrollResourceEventIntoView.
   * @private
   * @category Scrolling
   */
  scrollUnrenderedEventIntoView(resourceRec, eventRec, options = defaultScrollOptions) {
    // We must only resolve when the event's element has been painted
    // *and* the scroll has fully completed.
    return new Promise(resolve => {
      const me = this,
        // Knock out highlight and focus options. They must be applied after the scroll
        // has fully completed and we have an element. Use a default edgeOffset of 20.
        modifiedOptions = Object.assign({
          edgeOffset: 20
        }, options, unrenderedScrollOptions),
        scroller = me.timeAxisSubGrid.scrollable,
        box = me.getResourceEventBox(eventRec, resourceRec),
        scrollerViewport = scroller.viewport;
      // Event may fall on a time not included by workingTime settings
      if (!scrollerViewport || !box) {
        resolve();
        return;
      }
      // In case of subPixel position, scroll the whole pixel into view
      box.x = Math.ceil(box.x);
      box.y = Math.ceil(box.y);
      if (me.rtl) {
        // RTL scrolls in negative direction but coordinates are still LTR
        box.translate(-me.timeAxisViewModel.totalSize + scrollerViewport.width, 0);
      }
      // Note use of scroller.scrollLeft here. We need the natural DOM scrollLeft value
      // not the +ve X position along the scrolling axis.
      box.translate(scrollerViewport.x - scroller.scrollLeft, scrollerViewport.y - scroller.y);
      const
        // delta         = scroller.getDeltaTo(box, modifiedOptions)[me.isHorizontal ? 'xDelta' : 'yDelta'],
        onEventRender = async ({
          eventRecord,
          element,
          targetElement
        }) => {
          if (eventRecord === eventRec) {
            // Vertical's renderEvent is different to horizontal's
            const el = element || targetElement;
            detacher();
            // Don't resolve until the scroll has fully completed.
            await initialScrollPromise;
            options.highlight && DomHelper.highlight(el);
            options.focus && el.focus();
            resolve();
          }
        },
        // On either paint or repaint of the event, resolve the scroll promise and detach the listeners.
        detacher = me.ion({
          renderEvent: onEventRender
        }),
        initialScrollPromise = scroller.scrollIntoView(box, modifiedOptions);
      initialScrollPromise.then(() => {
        if (initialScrollPromise.cancelled) {
          resolve();
        }
      });
    });
  }
  /**
   * Scrolls the specified resource into view, works for both horizontal and vertical modes.
   * @param {Scheduler.model.ResourceModel} resourceRecord A resource record an event record is assigned to
   * @param {ScrollOptions} [options] How to scroll.
   * @returns {Promise} A promise which is resolved when the scrolling has finished.
   * @category Scrolling
   */
  scrollResourceIntoView(resourceRecord, options = defaultScrollOptions) {
    if (this.isVertical) {
      return this.currentOrientation.scrollResourceIntoView(resourceRecord, options);
    }
    return this.scrollRowIntoView(resourceRecord, options);
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/SchedulerRegions
 */
/**
 * Functions to get regions (bounding boxes) for scheduler, events etc.
 *
 * @mixin
 */
var SchedulerRegions = (Target => class SchedulerRegions extends (Target || Base) {
  static get $name() {
    return 'SchedulerRegions';
  }
  //region Orientation dependent regions
  /**
   * Gets the region represented by the schedule and optionally only for a single resource. The view will ask the
   * scheduler for the resource availability by calling getResourceAvailability. By overriding that method you can
   * constrain events differently for different resources.
   * @param {Scheduler.model.ResourceModel} resourceRecord (optional) The resource record
   * @param {Scheduler.model.EventModel} eventRecord (optional) The event record
   * @returns {Core.helper.util.Rectangle} The region of the schedule
   */
  getScheduleRegion(resourceRecord, eventRecord, local = true, dateConstraints) {
    return this.currentOrientation.getScheduleRegion(...arguments);
  }
  /**
   * Gets the region, relative to the timeline view element, representing the passed resource and optionally just for a certain date interval.
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Date} startDate A start date constraining the region
   * @param {Date} endDate An end date constraining the region
   * @returns {Core.helper.util.Rectangle} A Rectangle which encapsulates the resource time span
   */
  getResourceRegion(resourceRecord, startDate, endDate) {
    return this.currentOrientation.getRowRegion(...arguments);
  }
  //endregion
  //region ResourceEventBox
  getAssignmentEventBox(assignmentRecord, includesOutside) {
    return this.getResourceEventBox(assignmentRecord.event, assignmentRecord.resource, includesOutside);
  }
  /**
   * Get the region for a specified resources specified event.
   * @param {Scheduler.model.EventModel} eventRecord
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @param {Boolean} includeOutside Specify true to get boxes for events outside of the rendered zone in both
   *   dimensions. This option is used when calculating dependency lines, and we need to include routes from events
   *   which may be outside the rendered zone.
   * @returns {Core.helper.util.Rectangle}
   */
  getResourceEventBox(eventRecord, resourceRecord, includeOutside = false, roughly = false) {
    return this.currentOrientation.getResourceEventBox(...arguments);
  }
  //endregion
  //region Item box
  /**
   * Gets box for displayed item designated by the record. If several boxes are displayed for the given item
   * then the method returns all of them. Box coordinates are in view coordinate system.
   *
   * Boxes outside scheduling view timeaxis timespan and inside collapsed rows (if row defining store is a tree store)
   * will not be returned. Boxes outside scheduling view vertical visible area (i.e. boxes above currently visible
   * top row or below currently visible bottom row) will be calculated approximately.
   *
   * @param {Scheduler.model.EventModel} event
   * @returns {Object|Object[]}
   * @returns {Boolean} return.isPainted Whether the box was calculated for the rendered scheduled record or was
   *    approximately calculated for the scheduled record outside of the current vertical view area.
   * @returns {Number} return.top
   * @returns {Number} return.bottom
   * @returns {Number} return.start
   * @returns {Number} return.end
   * @returns {'before'|'after'} return.relPos if the item is not rendered then provides a view relative
   * position one of 'before', 'after'
   * @internal
   */
  getItemBox(event, includeOutside = false) {
    return event.resources.map(resource => this.getResourceEventBox(event, resource, includeOutside));
  }
  //endregion
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/SchedulerState
 */
const copyProperties = ['eventLayout', 'mode', 'eventColor', 'eventStyle', 'tickSize', 'fillTicks'];
/**
 * A Mixin for Scheduler that handles state. It serializes the following scheduler properties, in addition to what
 * is already stored by its superclass {@link Grid/view/mixin/GridState}:
 *
 * * eventLayout
 * * barMargin
 * * mode
 * * tickSize
 * * zoomLevel
 * * eventColor
 * * eventStyle
 *
 * See {@link Grid.view.mixin.GridState} and {@link Core.mixin.State} for more information on state.
 *
 * @mixin
 */
var SchedulerState = (Target => class SchedulerState extends (Target || Base) {
  static get $name() {
    return 'SchedulerState';
  }
  /**
   * Gets or sets scheduler's state. Check out {@link Scheduler.view.mixin.SchedulerState} mixin
   * and {@link Grid.view.mixin.GridState} for more details.
   * @member {Object} state
   * @property {String} state.eventLayout
   * @property {String} state.eventStyle
   * @property {String} state.eventColor
   * @property {Number} state.barMargin
   * @property {Number} state.tickSize
   * @property {Boolean} state.fillTicks
   * @property {Number} state.zoomLevel
   * @property {'horizontal'|'vertical'} state.mode
   * @property {Object[]} state.columns
   * @property {Boolean} state.readOnly
   * @property {Number} state.rowHeight
   * @property {Object} state.scroll
   * @property {Number} state.scroll.scrollLeft
   * @property {Number} state.scroll.scrollTop
   * @property {Array} state.selectedRecords
   * @property {String} state.selectedCell
   * @property {String} state.style
   * @property {Object} state.subGrids
   * @property {Object} state.store
   * @property {Object} state.store.sorters
   * @property {Object} state.store.groupers
   * @property {Object} state.store.filters
   * @category State
   */
  /**
   * Get scheduler's current state for serialization. State includes rowHeight, headerHeight, readOnly, selectedCell,
   * selectedRecordId, column states and store state etc.
   * @returns {Object} State object to be serialized
   * @private
   */
  getState() {
    return ObjectHelper.copyProperties(super.getState(), this, copyProperties);
  }
  /**
   * Apply previously stored state.
   * @param {Object} state
   * @private
   */
  applyState(state) {
    var _state$zoomLevelOptio;
    this.suspendRefresh();
    let propsToCopy = copyProperties.slice();
    if ((state === null || state === void 0 ? void 0 : state.eventLayout) === 'layoutFn') {
      delete state.eventLayout;
      propsToCopy.splice(propsToCopy.indexOf('eventLayout'), 1);
    }
    // Zoom level will set tick size, no need to update model additionally
    if (state !== null && state !== void 0 && (_state$zoomLevelOptio = state.zoomLevelOptions) !== null && _state$zoomLevelOptio !== void 0 && _state$zoomLevelOptio.width) {
      propsToCopy = propsToCopy.filter(p => p !== 'tickSize');
    }
    ObjectHelper.copyProperties(this, state, propsToCopy);
    super.applyState(state);
    this.resumeRefresh(true);
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/EventSelection
 */
/**
 * Mixin that tracks event or assignment selection by clicking on one or more events in the scheduler.
 * @mixin
 */
var SchedulerEventSelection = (Target => class EventSelection extends (Target || Base) {
  static get $name() {
    return 'EventSelection';
  }
  //region Default config
  static get configurable() {
    return {
      /**
       * Configure as `true`, or set property to `true` to highlight dependent events as well when selecting an event.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      highlightPredecessors: false,
      /**
       * Configure as `true`, or set property to `true` to highlight dependent events as well when selecting an event.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      highlightSuccessors: false,
      /**
       * Configure as `true` to deselect a selected event upon click.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      deselectOnClick: false,
      /**
       * Configure as `false` to preserve selection when clicking the empty schedule area.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      deselectAllOnScheduleClick: true,
      /**
       * Collection to store selection.
       * @config {Core.util.Collection}
       * @private
       */
      selectedCollection: {}
    };
  }
  static get defaultConfig() {
    return {
      /**
       * Configure as `true` to allow `CTRL+click` to select multiple events in the scheduler.
       * @config {Boolean}
       * @category Selection
       */
      multiEventSelect: false,
      /**
       * Configure as `true`, or set property to `true` to disable event selection.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      eventSelectionDisabled: false,
      /**
       * CSS class to add to selected events.
       * @config {String}
       * @default
       * @category CSS
       * @private
       */
      eventSelectedCls: 'b-sch-event-selected',
      /**
       * Configure as `true` to trigger `selectionChange` when removing a selected event/assignment.
       * @config {Boolean}
       * @default
       * @category Selection
       */
      triggerSelectionChangeOnRemove: false,
      /**
       * This flag controls whether Scheduler should preserve its selection of events when loading a new dataset
       * (if selected event ids are included in the newly loaded dataset).
       * @config {Boolean}
       * @default
       * @category Selection
       */
      maintainSelectionOnDatasetChange: true,
      /**
       * CSS class to add to other instances of a selected event, to highlight them.
       * @config {String}
       * @default
       * @category CSS
       * @private
       */
      eventAssignHighlightCls: 'b-sch-event-assign-selected'
    };
  }
  //endregion
  //region Events
  /**
   * Fired any time there is a change to the events selected in the Scheduler.
   * @event eventSelectionChange
   * @param {'select'|'deselect'|'update'|'clear'} action One of the actions 'select', 'deselect', 'update',
   * 'clear'
   * @param {Scheduler.model.EventModel[]} selected An array of the Events added to the selection.
   * @param {Scheduler.model.EventModel[]} deselected An array of the Event removed from the selection.
   * @param {Scheduler.model.EventModel[]} selection The new selection.
   */
  /**
   * Fired any time there is going to be a change to the events selected in the Scheduler.
   * Returning `false` prevents the change
   * @event beforeEventSelectionChange
   * @preventable
   * @param {String} action One of the actions 'select', 'deselect', 'update', 'clear'
   * @param {Scheduler.model.EventModel[]} selected An array of events that will be added to the selection.
   * @param {Scheduler.model.EventModel[]} deselected An array of events that will be removed from the selection.
   * @param {Scheduler.model.EventModel[]} selection The currently selected events, before applying `selected` and `deselected`.
   */
  /**
   * Fired any time there is a change to the assignments selected in the Scheduler.
   * @event assignmentSelectionChange
   * @param {'select'|'deselect'|'update'|'clear'} action One of the actions 'select', 'deselect', 'update',
   * 'clear'
   * @param {Scheduler.model.AssignmentModel[]} selected An array of the Assignments added to the selection.
   * @param {Scheduler.model.AssignmentModel[]} deselected An array of the Assignments removed from the selection.
   * @param {Scheduler.model.AssignmentModel[]} selection The new selection.
   */
  /**
   * Fired any time there is going to be a change to the assignments selected in the Scheduler.
   * Returning `false` prevents the change
   * @event beforeAssignmentSelectionChange
   * @preventable
   * @param {String} action One of the actions 'select', 'deselect', 'update', 'clear'
   * @param {Scheduler.model.EventModel[]} selected An array of assignments that will be added to the selection.
   * @param {Scheduler.model.EventModel[]} deselected An array of assignments that will be removed from the selection.
   * @param {Scheduler.model.EventModel[]} selection The currently selected assignments, before applying `selected` and `deselected`.
   */
  //endregion
  //region Init
  afterConstruct() {
    var _this$navigator;
    super.afterConstruct();
    (_this$navigator = this.navigator) === null || _this$navigator === void 0 ? void 0 : _this$navigator.ion({
      navigate: 'onEventNavigate',
      thisObj: this
    });
  }
  //endregion
  //region Selected Collection
  changeSelectedCollection(selectedCollection) {
    if (!selectedCollection.isCollection) {
      selectedCollection = new Collection(selectedCollection);
    }
    return selectedCollection;
  }
  updateSelectedCollection(selectedCollection) {
    const me = this;
    // When sharing collection, only the owner should destroy it
    if (!selectedCollection.owner) {
      selectedCollection.owner = me;
    }
    // Fire row change events from onSelectedCollectionChange
    selectedCollection.ion({
      change: (...args) => me.project.deferUntilRepopulationIfNeeded('onSelectedCollectionChange', (...args) => !me.isDestroying && me.onSelectedCollectionChange(...args), args),
      // deferring this handler breaks the UI
      beforeSplice: 'onBeforeSelectedCollectionSplice',
      thisObj: me
    });
  }
  get selectedCollection() {
    return this._selectedCollection;
  }
  getActionType(selection, selected, deselected) {
    return selection.length > 0 ? selected.length > 0 && deselected.length > 0 ? 'update' : selected.length > 0 ? 'select' : 'deselect' : 'clear';
  }
  //endregion
  //region Modify selection
  getEventsFromAssignments(assignments) {
    return ArrayHelper.unique(assignments.map(assignment => assignment.event));
  }
  /**
   * The {@link Scheduler.model.EventModel events} which are selected.
   * @property {Scheduler.model.EventModel[]}
   * @category Selection
   */
  get selectedEvents() {
    return this.getEventsFromAssignments(this.selectedCollection.values);
  }
  set selectedEvents(events) {
    var _events;
    // Select all assignments
    const assignments = [];
    events = ArrayHelper.asArray(events);
    (_events = events) === null || _events === void 0 ? void 0 : _events.forEach(event => {
      if (this.isEventSelectable(event) !== false) {
        if (event.isOccurrence) {
          event.assignments.forEach(as => {
            assignments.push(this.assignmentStore.getOccurrence(as, event));
          });
        } else {
          assignments.push(...event.assignments);
        }
      }
    });
    // Replace the entire selected collection with the new record set
    this.selectedCollection.splice(0, this.selectedCollection.count, assignments);
  }
  /**
   * The {@link Scheduler.model.AssignmentModel events} which are selected.
   * @property {Scheduler.model.AssignmentModel[]}
   * @category Selection
   */
  get selectedAssignments() {
    return this.selectedCollection.values;
  }
  set selectedAssignments(assignments) {
    // Replace the entire selected collection with the new record set
    this.selectedCollection.splice(0, this.selectedCollection.count, assignments || []);
  }
  /**
   * Returns `true` if the {@link Scheduler.model.EventModel event} is selected.
   * @param {Scheduler.model.EventModel} event The event
   * @returns {Boolean} Returns `true` if the event is selected
   * @category Selection
   */
  isEventSelected(event) {
    const {
      selectedCollection
    } = this;
    return Boolean(selectedCollection.count && selectedCollection.includes(event.assignments));
  }
  /**
   * A template method (empty by default) allowing you to control if an event can be selected or not.
   *
   * ```javascript
   * new Scheduler({
   *     isEventSelectable(event) {
   *         return event.startDate >= Date.now();
   *     }
   * })
   * ```
   *
   * This selection process is applicable to calendar too:
   *
   * ```javascript
   * new Calendar({
   *     isEventSelectable(event) {
   *         return event.startDate >= Date.now();
   *     }
   * })
   * ```
   *
   * @param {Scheduler.model.EventModel} event The event record
   * @returns {Boolean} true if event can be selected, otherwise false
   * @prp {Function}
   * @category Selection
   */
  isEventSelectable(event) {}
  /**
   * Returns `true` if the {@link Scheduler.model.AssignmentModel assignment} is selected.
   * @param {Scheduler.model.AssignmentModel} assignment The assignment
   * @returns {Boolean} Returns `true` if the assignment is selected
   * @category Selection
   */
  isAssignmentSelected(assignment) {
    return this.selectedCollection.includes(assignment);
  }
  /**
   * Selects the passed {@link Scheduler.model.EventModel event} or {@link Scheduler.model.AssignmentModel assignment}
   * *if it is not selected*. Selecting events results in all their assignments being selected.
   * @param {Scheduler.model.EventModel|Scheduler.model.AssignmentModel} eventOrAssignment The event or assignment to select
   * @param {Boolean} [preserveSelection] Pass `true` to preserve any other selected events or assignments
   * @category Selection
   */
  select(eventOrAssignment, preserveSelection = false) {
    if (eventOrAssignment.isAssignment) {
      this.selectAssignment(eventOrAssignment, preserveSelection);
    } else {
      this.selectEvent(eventOrAssignment, preserveSelection);
    }
  }
  /**
   * Selects the passed {@link Scheduler.model.EventModel event} *if it is not selected*. Selecting an event will
   * select all its assignments.
   * @param {Scheduler.model.EventModel} event The event to select
   * @param {Boolean} [preserveSelection] Pass `true` to preserve any other selected events
   * @category Selection
   */
  selectEvent(event, preserveSelection = false) {
    // If the event is already selected, this is a no-op.
    // In this case, selection must not be cleared even in the absence of preserveSelection
    if (!this.isEventSelected(event)) {
      this.selectEvents([event], preserveSelection);
    }
  }
  /**
   * Selects the passed {@link Scheduler.model.AssignmentModel assignment} *if it is not selected*.
   * @param {Scheduler.model.AssignmentModel} assignment The assignment to select
   * @param {Boolean} [preserveSelection] Pass `true` to preserve any other selected assignments
   * @param {Event} [event] If this method was invoked as a result of a user action, this is the DOM event that triggered it
   * @category Selection
   */
  selectAssignment(assignment, preserveSelection = false, event) {
    // If the event is already selected, this is a no-op.
    // In this case, selection must not be cleared even in the absence of preserveSelection
    if (!this.isAssignmentSelected(assignment)) {
      preserveSelection ? this.selectedCollection.add(assignment) : this.selectedAssignments = assignment;
    }
  }
  /**
   * Deselects the passed {@link Scheduler.model.EventModel event} or {@link Scheduler.model.AssignmentModel assignment}
   * *if it is selected*.
   * @param {Scheduler.model.EventModel|Scheduler.model.AssignmentModel} eventOrAssignment The event or assignment to deselect.
   * @category Selection
   */
  deselect(eventOrAssignment) {
    if (eventOrAssignment.isAssignment) {
      this.deselectAssignment(eventOrAssignment);
    } else {
      this.deselectEvent(eventOrAssignment);
    }
  }
  /**
   * Deselects the passed {@link Scheduler.model.EventModel event} *if it is selected*.
   * @param {Scheduler.model.EventModel} event The event to deselect.
   * @category Selection
   */
  deselectEvent(event) {
    if (this.isEventSelected(event)) {
      this.selectedCollection.remove(...event.assignments);
    }
  }
  /**
   * Deselects the passed {@link Scheduler.model.AssignmentModel assignment} *if it is selected*.
   * @param {Scheduler.model.AssignmentModel} assignment The assignment to deselect
   * @param {Event} [event] If this method was invoked as a result of a user action, this is the DOM event that triggered it
   * @category Selection
   */
  deselectAssignment(assignment) {
    if (this.isAssignmentSelected(assignment)) {
      this.selectedCollection.remove(assignment);
    }
  }
  /**
   * Adds {@link Scheduler.model.EventModel events} to the selection.
   * @param {Scheduler.model.EventModel[]} events Events to be selected
   * @param {Boolean} [preserveSelection] Pass `true` to preserve any other selected events
   * @category Selection
   */
  selectEvents(events, preserveSelection = false) {
    if (preserveSelection) {
      const assignments = events.reduce((assignments, event) => {
        if (this.isEventSelectable(event) !== false) {
          assignments.push(...event.assignments);
        }
        return assignments;
      }, []);
      this.selectedCollection.add(assignments);
    } else {
      this.selectedEvents = events;
    }
  }
  /**
   * Removes {@link Scheduler.model.EventModel events} from the selection.
   * @param {Scheduler.model.EventModel[]} events Events or assignments  to be deselected
   * @category Selection
   */
  deselectEvents(events) {
    this.selectedCollection.remove(events.reduce((assignments, event) => {
      assignments.push(...event.assignments);
      return assignments;
    }, []));
  }
  /**
   * Adds {@link Scheduler.model.AssignmentModel assignments} to the selection.
   * @param {Scheduler.model.AssignmentModel[]} assignments Assignments to be selected
   * @category Selection
   */
  selectAssignments(assignments) {
    this.selectedCollection.add(assignments);
  }
  /**
   * Removes {@link Scheduler.model.AssignmentModel assignments} from the selection.
   * @param {Scheduler.model.AssignmentModel[]} assignments Assignments  to be deselected
   * @category Selection
   */
  deselectAssignments(assignments) {
    this.selectedCollection.remove(assignments);
  }
  /**
   * Deselects all {@link Scheduler.model.EventModel events} and {@link Scheduler.model.AssignmentModel assignments}.
   * @category Selection
   */
  clearEventSelection() {
    this.selectedAssignments = [];
  }
  //endregion
  //region Events
  /**
   * Responds to mutations of the underlying selection Collection.
   * Keeps the UI synced, eventSelectionChange and assignmentSelectionChange event is fired when `me.silent` is falsy.
   * @private
   */
  onBeforeSelectedCollectionSplice({
    toAdd,
    toRemove,
    index
  }) {
    const me = this,
      selection = me._selectedCollection.values,
      selected = toAdd,
      deselected = toRemove > 0 ? selected.slice(index, toRemove + index) : [],
      action = me.getActionType(selection, selected, deselected);
    if (me.trigger('beforeEventSelectionChange', {
      action,
      selection: me.getEventsFromAssignments(selection) || [],
      selected: me.getEventsFromAssignments(selected) || [],
      deselected: me.getEventsFromAssignments(deselected) || []
    }) === false) {
      return false;
    }
    if (me.trigger('beforeAssignmentSelectionChange', {
      action,
      selection,
      selected,
      deselected
    }) === false) {
      return false;
    }
  }
  onSelectedCollectionChange({
    added,
    removed
  }) {
    const me = this,
      selection = me.selectedAssignments,
      selected = added || [],
      deselected = removed || [];
    function updateSelection(assignmentRecord, select) {
      const eventRecord = assignmentRecord.event;
      if (eventRecord) {
        const {
            eventAssignHighlightCls
          } = me,
          element = me.getElementFromAssignmentRecord(assignmentRecord);
        me.currentOrientation.toggleCls(assignmentRecord, me.eventSelectedCls, select);
        eventAssignHighlightCls && me.getElementsFromEventRecord(eventRecord).forEach(el => {
          if (el !== element) {
            const otherAssignmentRecord = me.resolveAssignmentRecord(el);
            me.currentOrientation.toggleCls(otherAssignmentRecord, eventAssignHighlightCls, select);
            if (select) {
              // Need to force a reflow to get the highlightning animation triggered
              el.style.animation = 'none';
              el.offsetHeight;
              el.style.animation = '';
            }
            el.classList.toggle(eventAssignHighlightCls, select);
          }
        });
      }
    }
    deselected.forEach(record => updateSelection(record, false));
    selected.forEach(record => updateSelection(record, true));
    if (me.highlightSuccessors || me.highlightPredecessors) {
      me.highlightLinkedEvents(me.selectedEvents);
    }
    // To be able to restore selection after reloading resources (which might lead to regenerated assignments in
    // the single assignment scenario, so cannot rely on records or ids)
    me.$selectedAssignments = selection.map(assignment => ({
      eventId: assignment.eventId,
      resourceId: assignment.resourceId
    }));
    if (!me.silent) {
      const action = this.getActionType(selection, selected, deselected);
      me.trigger('assignmentSelectionChange', {
        action,
        selection,
        selected,
        deselected
      });
      me.trigger('eventSelectionChange', {
        action,
        selection: me.selectedEvents,
        selected: me.getEventsFromAssignments(selected),
        deselected: me.getEventsFromAssignments(deselected)
      });
    }
  }
  /**
   * Assignment change listener to remove events from selection which are no longer in the assignments.
   * @private
   */
  onAssignmentChange(event) {
    super.onAssignmentChange(event);
    const me = this,
      {
        action,
        records: assignments
      } = event;
    me.silent = !me.triggerSelectionChangeOnRemove;
    if (action === 'remove') {
      me.deselectAssignments(assignments);
    } else if (action === 'removeall' && !me.eventStore.isSettingData) {
      me.clearEventSelection();
    } else if (action === 'dataset' && me.$selectedAssignments) {
      if (!me.maintainSelectionOnDatasetChange) {
        me.clearEventSelection();
      } else {
        const newAssignments = me.$selectedAssignments.map(selector => assignments.find(a => a.eventId === selector.eventId && a.resourceId === selector.resourceId));
        me.selectedAssignments = ArrayHelper.clean(newAssignments);
      }
    }
    me.silent = false;
  }
  onInternalEventStoreChange({
    source,
    action,
    records
  }) {
    // Setting empty event dataset cannot be handled in onAssignmentChange above, no assignments might be affected
    if (!source.isResourceTimeRangeStore && action === 'dataset' && !records.length) {
      this.clearEventSelection();
    }
    super.onInternalEventStoreChange(...arguments);
  }
  /**
   * Mouse listener to update selection.
   * @private
   */
  onAssignmentSelectionClick(event, clickedRecord) {
    const me = this;
    // Multi selection: CTRL means preserve selection, just add or remove the event.
    // Single selection: CTRL deselects already selected event
    if (me.isAssignmentSelected(clickedRecord)) {
      if (me.deselectOnClick || event.ctrlKey) {
        me.deselectAssignment(clickedRecord, me.multiEventSelect, event);
      }
    } else if (this.isEventSelectable(clickedRecord.event) !== false) {
      me.selectAssignment(clickedRecord, event.ctrlKey && me.multiEventSelect, event);
    }
  }
  /**
   * Navigation listener to update selection.
   * @private
   */
  onEventNavigate({
    event,
    item
  }) {
    if (!this.eventSelectionDisabled) {
      const assignment = item && (item.nodeType === Element.ELEMENT_NODE ? this.resolveAssignmentRecord(item) : item);
      if (assignment) {
        this.onAssignmentSelectionClick(event, assignment);
      }
      // The click was not an event or assignment
      else if (this.deselectAllOnScheduleClick) {
        this.clearEventSelection();
      }
    }
  }
  changeHighlightSuccessors(value) {
    return this.changeLinkedEvents(value);
  }
  changeHighlightPredecessors(value) {
    return this.changeLinkedEvents(value);
  }
  changeLinkedEvents(value) {
    const me = this;
    if (value) {
      me.highlighted = me.highlighted || new Set();
      me.highlightLinkedEvents(me.selectedEvents);
    } else if (me.highlighted) {
      me.highlightLinkedEvents();
    }
    return value;
  }
  // Function that highlights/unhighlights events in a dependency chain
  highlightLinkedEvents(eventRecords = []) {
    const me = this,
      {
        highlighted,
        eventStore
      } = me,
      dependenciesFeature = me.features.dependencies;
    // Unhighlight previously highlighted records
    highlighted.forEach(eventRecord => {
      if (!eventRecords.includes(eventRecord)) {
        eventRecord.meta.highlight = false;
        highlighted.delete(eventRecord);
        if (eventStore.includes(eventRecord)) {
          eventRecord.dependencies.forEach(dep => dependenciesFeature.unhighlight(dep, 'b-highlight'));
        }
      }
    });
    eventRecords.forEach(eventRecord => {
      const toWalk = [eventRecord];
      // Collect all events along the dependency chain
      while (toWalk.length) {
        const record = toWalk.pop();
        highlighted.add(record);
        if (me.highlightSuccessors) {
          record.outgoingDeps.forEach(outgoing => {
            dependenciesFeature.highlight(outgoing, 'b-highlight');
            !highlighted.has(outgoing.toEvent) && toWalk.push(outgoing.toEvent);
          });
        }
        if (me.highlightPredecessors) {
          record.incomingDeps.forEach(incoming => {
            dependenciesFeature.highlight(incoming, 'b-highlight');
            !highlighted.has(incoming.fromEvent) && toWalk.push(incoming.fromEvent);
          });
        }
      }
      // Highlight them
      highlighted.forEach(record => record.meta.highlight = true);
    });
    // Toggle flag on schedulers element, to fade others in or out
    me.element.classList.toggle('b-highlighting', eventRecords.length > 0);
    me.refreshWithTransition();
  }
  onEventDataGenerated(renderData) {
    if (this.highlightSuccessors || this.highlightPredecessors) {
      renderData.cls['b-highlight'] = renderData.eventRecord.meta.highlight;
    }
    super.onEventDataGenerated(renderData);
  }
  updateProject(project, old) {
    // Clear selection when the whole world shifts :)
    this.clearEventSelection();
    super.updateProject(project, old);
  }
  //endregion
  doDestroy() {
    var _this$_selectedCollec;
    ((_this$_selectedCollec = this._selectedCollection) === null || _this$_selectedCollec === void 0 ? void 0 : _this$_selectedCollec.owner) === this && this._selectedCollection.destroy();
    super.doDestroy();
  }
  //region Getters/Setters
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
  //endregion
});

/**
 * @module Scheduler/view/mixin/EventNavigation
 */
const preventDefault = e => e.preventDefault(),
  isArrowKey = {
    ArrowRight: 1,
    ArrowLeft: 1,
    ArrowUp: 1,
    ArrowDown: 1
  },
  animate100 = {
    animate: 100
  },
  emptyObject$1 = Object.freeze({});
/**
 * Mixin that tracks event or assignment selection by clicking on one or more events in the scheduler.
 * @mixin
 */
var SchedulerEventNavigation = (Target => class EventNavigation extends Delayable(Target || Base) {
  static get $name() {
    return 'EventNavigation';
  }
  //region Default config
  static get configurable() {
    return {
      /**
       * A config object to use when creating the {@link Core.helper.util.Navigator}
       * to use to perform keyboard navigation in the timeline.
       * @config {NavigatorConfig}
       * @default
       * @category Misc
       * @internal
       */
      navigator: {
        allowCtrlKey: true,
        scrollSilently: true,
        keys: {
          Space: 'onEventSpaceKey',
          Enter: 'onEventEnterKey',
          Delete: 'onDeleteKey',
          Backspace: 'onDeleteKey',
          ArrowUp: 'onArrowUpKey',
          ArrowDown: 'onArrowDownKey',
          Escape: 'onEscapeKey',
          // These are processed by GridNavigation's handlers
          Tab: 'onTab',
          'SHIFT+Tab': 'onShiftTab'
        }
      },
      isNavigationKey: {
        ArrowDown: 1,
        ArrowUp: 1,
        ArrowLeft: 1,
        ArrowRight: 1
      }
    };
  }
  static get defaultConfig() {
    return {
      /**
       * A CSS class name to add to focused events.
       * @config {String}
       * @default
       * @category CSS
       * @private
       */
      focusCls: 'b-active',
      /**
       * Allow using [Delete] and [Backspace] to remove events/assignments
       * @config {Boolean}
       * @default
       * @category Misc
       */
      enableDeleteKey: true,
      // Number in milliseconds to buffer handlers execution. See `Delayable.throttle` function docs.
      onDeleteKeyBuffer: 500,
      navigatePreviousBuffer: 200,
      navigateNextBuffer: 200,
      testConfig: {
        onDeleteKeyBuffer: 1
      }
    };
  }
  //endregion
  //region Events
  /**
   * Fired when a user gesture causes the active item to change.
   * @event navigate
   * @param {Event} event The browser event which instigated navigation. May be a click or key or focus event.
   * @param {HTMLElement|null} item The newly active item, or `null` if focus moved out.
   * @param {HTMLElement|null} oldItem The previously active item, or `null` if focus is moving in.
   */
  //endregion
  construct(config) {
    const me = this;
    me.isInTimeAxis = me.isInTimeAxis.bind(me);
    me.onDeleteKey = me.throttle(me.onDeleteKey, me.onDeleteKeyBuffer, me);
    super.construct(config);
  }
  changeNavigator(navigator) {
    const me = this;
    me.getConfig('subGridConfigs');
    return new Navigator(me.constructor.mergeConfigs({
      ownerCmp: me,
      target: me.timeAxisSubGridElement,
      processEvent: me.processEvent,
      itemSelector: `.${me.eventCls}-wrap`,
      focusCls: me.focusCls,
      navigatePrevious: me.throttle(me.navigatePrevious, {
        delay: me.navigatePreviousBuffer,
        throttled: preventDefault
      }),
      navigateNext: me.throttle(me.navigateNext, {
        delay: me.navigateNextBuffer,
        throttled: preventDefault
      })
    }, navigator));
  }
  doDestroy() {
    this.navigator.destroy();
    super.doDestroy();
  }
  isInTimeAxis(record) {
    // If event is hidden by workingTime configs, horizontal mapper would raise a flag on instance meta
    // We still need to check if time span is included in axis
    return !record.instanceMeta(this).excluded && this.timeAxis.isTimeSpanInAxis(record);
  }
  onElementKeyDown(keyEvent) {
    var _me$focusedCell, _me$focusedCell2;
    const me = this,
      {
        navigator
      } = me;
    // If we're focused in the time axis, and *not* on an event, then ENTER means
    // jump down into the first visible assignment in the cell.
    if (((_me$focusedCell = me.focusedCell) === null || _me$focusedCell === void 0 ? void 0 : _me$focusedCell.rowIndex) !== -1 && ((_me$focusedCell2 = me.focusedCell) === null || _me$focusedCell2 === void 0 ? void 0 : _me$focusedCell2.column) === me.timeAxisColumn && !keyEvent.target.closest(navigator.itemSelector) && keyEvent.key === 'Enter') {
      const firstAssignment = me.getFirstVisibleAssignment();
      if (firstAssignment) {
        me.navigateTo(firstAssignment, {
          uiEvent: keyEvent
        });
        return false;
      }
    } else {
      var _super$onElementKeyDo;
      (_super$onElementKeyDo = super.onElementKeyDown) === null || _super$onElementKeyDo === void 0 ? void 0 : _super$onElementKeyDo.call(this, keyEvent);
    }
  }
  getFirstVisibleAssignment(location = this.focusedCell) {
    const me = this,
      {
        currentOrientation,
        rowManager,
        eventStore
      } = me;
    if (me.isHorizontal) {
      var _renderedEvents;
      let renderedEvents = currentOrientation.rowMap.get(rowManager.getRow(location.rowIndex));
      if ((_renderedEvents = renderedEvents) !== null && _renderedEvents !== void 0 && _renderedEvents.length) {
        var _renderedEvents$;
        return (_renderedEvents$ = renderedEvents[0]) === null || _renderedEvents$ === void 0 ? void 0 : _renderedEvents$.elementData.assignmentRecord;
      } else {
        var _currentOrientation$r, _renderedEvents2;
        renderedEvents = (_currentOrientation$r = currentOrientation.resourceMap.get(location.id)) === null || _currentOrientation$r === void 0 ? void 0 : _currentOrientation$r.eventsData;
        if ((_renderedEvents2 = renderedEvents) !== null && _renderedEvents2 !== void 0 && _renderedEvents2.length) {
          var _renderedEvents$filte;
          // When events are gathered from resource, we need to check they're available
          return (_renderedEvents$filte = renderedEvents.filter(e => eventStore.isAvailable(e.eventRecord))[0]) === null || _renderedEvents$filte === void 0 ? void 0 : _renderedEvents$filte.assignmentRecord;
        }
      }
    } else {
      const firstResource = [...currentOrientation.resourceMap.values()][0],
        renderedEvents = firstResource && Object.values(firstResource);
      if (renderedEvents !== null && renderedEvents !== void 0 && renderedEvents.length) {
        return renderedEvents.filter(e => eventStore.isAvailable(e.renderData.eventRecord))[0].renderData.assignmentRecord;
      }
    }
  }
  onGridBodyFocusIn(focusEvent) {
    const isGridCellFocus = focusEvent.target.closest(this.focusableSelector);
    // Event navigation only has a say when navigation is inside the TimeAxisSubGrid
    if (this.timeAxisSubGridElement.contains(focusEvent.target)) {
      const me = this,
        {
          navigationEvent
        } = me,
        {
          target
        } = focusEvent,
        eventFocus = target.closest(me.navigator.itemSelector),
        destinationCell = eventFocus ? me.normalizeCellContext({
          rowIndex: me.isVertical ? 0 : me.resourceStore.indexOf(me.resolveResourceRecord(target)),
          column: me.timeAxisColumn,
          target
        }) : new Location(target);
      // Don't take over what the event navigator does if it's doing event navigation.
      // Just silently cache our actionable location.
      if (eventFocus) {
        var _me$onCellNavigate;
        const {
          _focusedCell
        } = me;
        me._focusedCell = destinationCell;
        (_me$onCellNavigate = me.onCellNavigate) === null || _me$onCellNavigate === void 0 ? void 0 : _me$onCellNavigate.call(me, me, _focusedCell, destinationCell, navigationEvent, true);
        return;
      }
      // Depending on how we got here, try to focus the first event in the cell *if we're in a cell*.
      if (isGridCellFocus && (!navigationEvent || isArrowKey[navigationEvent.key])) {
        const firstAssignment = me.getFirstVisibleAssignment(destinationCell);
        if (firstAssignment) {
          me.navigateTo(firstAssignment, {
            // Only change scroll if focus came from key press
            scrollIntoView: Boolean(navigationEvent && navigationEvent.type !== 'mousedown'),
            uiEvent: navigationEvent || focusEvent
          });
          return;
        }
      }
    }
    // Grid-level focus movement, let superclass handle it.
    if (isGridCellFocus) {
      super.onGridBodyFocusIn(focusEvent);
    }
  }
  /*
   * Override of GridNavigation#focusCell method to handle the TimeAxisColumn.
   * Not needed until we implement full keyboard accessibility.
   */
  accessibleFocusCell(cellSelector, options) {
    const me = this;
    cellSelector = me.normalizeCellContext(cellSelector);
    if (cellSelector.columnId === me.timeAxisColumn.id) ; else {
      return super.focusCell(cellSelector, options);
    }
  }
  // Interface method to extract the navigated to record from a populated 'navigate' event.
  // Gantt, Scheduler and Calendar handle event differently, adding different properties to it.
  // This method is meant to be overridden to return correct target from event
  normalizeTarget(event) {
    return event.assignmentRecord;
  }
  getPrevious(assignmentRecord, isDelete) {
    const me = this,
      {
        resourceStore
      } = me,
      {
        eventSorter
      } = me.currentOrientation,
      // start/end dates are required to limit time span to look at in case recurrence feature is enabled
      {
        startDate,
        endDate
      } = me.timeAxis,
      eventRecord = assignmentRecord.event,
      resourceEvents = me.eventStore.getEvents({
        resourceRecord: assignmentRecord.resource,
        startDate,
        endDate
      }).filter(this.isInTimeAxis).sort(eventSorter);
    let resourceRecord = assignmentRecord.resource,
      previousEvent = resourceEvents[resourceEvents.indexOf(eventRecord) - 1];
    // At first event for resource, traverse up the resource store.
    if (!previousEvent) {
      // If we are deleting an event, skip other instances of the event which we may encounter
      // due to multi-assignment.
      for (let rowIdx = resourceStore.indexOf(resourceRecord) - 1; (!previousEvent || isDelete && previousEvent === eventRecord) && rowIdx >= 0; rowIdx--) {
        resourceRecord = resourceStore.getAt(rowIdx);
        const events = me.eventStore.getEvents({
          resourceRecord,
          startDate,
          endDate
        }).filter(me.isInTimeAxis).sort(eventSorter);
        previousEvent = events.length && events[events.length - 1];
      }
    }
    return me.assignmentStore.getAssignmentForEventAndResource(previousEvent, resourceRecord);
  }
  navigatePrevious(keyEvent) {
    const me = this,
      previousAssignment = me.getPrevious(me.normalizeTarget(keyEvent));
    keyEvent.preventDefault();
    if (previousAssignment) {
      if (!keyEvent.ctrlKey) {
        me.clearEventSelection();
      }
      return me.navigateTo(previousAssignment, {
        uiEvent: keyEvent
      });
    }
    // No previous event/task, fall back to Grid's handling of this gesture
    return me.doGridNavigation(keyEvent);
  }
  getNext(assignmentRecord, isDelete) {
    const me = this,
      {
        resourceStore
      } = me,
      {
        eventSorter
      } = me.currentOrientation,
      // start/end dates are required to limit time span to look at in case recurrence feature is enabled
      {
        startDate,
        endDate
      } = me.timeAxis,
      eventRecord = assignmentRecord.event,
      resourceEvents = me.eventStore.getEvents({
        resourceRecord: assignmentRecord.resource,
        // start/end are required to limit time
        startDate,
        endDate
      }).filter(this.isInTimeAxis).sort(eventSorter);
    let resourceRecord = assignmentRecord.resource,
      nextEvent = resourceEvents[resourceEvents.indexOf(eventRecord) + 1];
    // At last event for resource, traverse down the resource store
    if (!nextEvent) {
      // If we are deleting an event, skip other instances of the event which we may encounter
      // due to multi-assignment.
      for (let rowIdx = resourceStore.indexOf(resourceRecord) + 1; (!nextEvent || isDelete && nextEvent === eventRecord) && rowIdx < resourceStore.count; rowIdx++) {
        resourceRecord = resourceStore.getAt(rowIdx);
        const events = me.eventStore.getEvents({
          resourceRecord,
          startDate,
          endDate
        }).filter(me.isInTimeAxis).sort(eventSorter);
        nextEvent = events[0];
      }
    }
    return me.assignmentStore.getAssignmentForEventAndResource(nextEvent, resourceRecord);
  }
  navigateNext(keyEvent) {
    const me = this,
      nextAssignment = me.getNext(me.normalizeTarget(keyEvent));
    keyEvent.preventDefault();
    if (nextAssignment) {
      if (!keyEvent.ctrlKey) {
        me.clearEventSelection();
      }
      return me.navigateTo(nextAssignment, {
        uiEvent: keyEvent
      });
    }
    // No next event/task, fall back to Grid's handling of this gesture
    return me.doGridNavigation(keyEvent);
  }
  doGridNavigation(keyEvent) {
    if (!keyEvent.handled && keyEvent.key.indexOf('Arrow') === 0) {
      this[`navigate${keyEvent.key.substring(5)}ByKey`](keyEvent);
    }
  }
  async navigateTo(targetAssignment, {
    scrollIntoView = true,
    uiEvent = {}
  } = emptyObject$1) {
    const me = this,
      {
        navigator
      } = me,
      {
        skipScrollIntoView
      } = navigator;
    if (targetAssignment) {
      if (scrollIntoView) {
        // No key processing during scroll
        navigator.disabled = true;
        await me.scrollAssignmentIntoView(targetAssignment, animate100);
        navigator.disabled = false;
      } else {
        navigator.skipScrollIntoView = true;
      }
      // Panel can be destroyed before promise is resolved
      // Perform a sanity check to make sure element is still in the DOM (syncIdMap actually).
      if (!me.isDestroyed && this.getElementFromAssignmentRecord(targetAssignment)) {
        me.activeAssignment = targetAssignment;
        navigator.skipScrollIntoView = skipScrollIntoView;
        navigator.trigger('navigate', {
          event: uiEvent,
          item: me.getElementFromAssignmentRecord(targetAssignment).closest(navigator.itemSelector)
        });
      }
    }
  }
  set activeAssignment(assignmentRecord) {
    const assignmentEl = this.getElementFromAssignmentRecord(assignmentRecord, true);
    if (assignmentEl) {
      this.navigator.activeItem = assignmentEl;
    }
  }
  get activeAssignment() {
    const {
      activeItem
    } = this.navigator;
    if (activeItem) {
      return this.resolveAssignmentRecord(activeItem);
    }
  }
  get previousActiveEvent() {
    const {
      previousActiveItem
    } = this.navigator;
    if (previousActiveItem) {
      return this.resolveEventRecord(previousActiveItem);
    }
  }
  processEvent(keyEvent) {
    const me = this,
      eventElement = keyEvent.target.closest(me.eventSelector);
    if (!me.navigator.disabled && eventElement) {
      keyEvent.assignmentRecord = me.resolveAssignmentRecord(eventElement);
      keyEvent.eventRecord = me.resolveEventRecord(eventElement);
      keyEvent.resourceRecord = me.resolveResourceRecord(eventElement);
    }
    return keyEvent;
  }
  onDeleteKey(keyEvent) {
    const me = this;
    if (!me.readOnly && me.enableDeleteKey) {
      const records = me.eventStore.usesSingleAssignment ? me.selectedEvents : me.selectedAssignments;
      me.removeEvents(records.filter(r => !r.readOnly));
    }
  }
  onArrowUpKey(keyEvent) {
    this.focusCell({
      rowIndex: this.focusedCell.rowIndex - 1,
      column: this.timeAxisColumn
    });
    keyEvent.handled = true;
  }
  onArrowDownKey(keyEvent) {
    if (this.focusedCell.rowIndex < this.resourceStore.count - 1) {
      this.focusCell({
        rowIndex: this.focusedCell.rowIndex + 1,
        column: this.timeAxisColumn
      });
      keyEvent.handled = true;
    }
  }
  onEscapeKey(keyEvent) {
    if (!keyEvent.target.closest('.b-dragging')) {
      this.focusCell({
        rowIndex: this.focusedCell.rowIndex,
        column: this.timeAxisColumn
      });
      keyEvent.handled = true;
    }
  }
  onEventSpaceKey(keyEvent) {
    // Empty, to be chained by features
  }
  onEventEnterKey(keyEvent) {
    // Empty, to be chained by features
  }
  get isActionableLocation() {
    // Override from grid if the Navigator's location is an event (or task if we're in Gantt)
    // Being focused on a task/event means that it's *not* actionable. It's not valid to report
    // that we're "inside" the cell in a TimeLine, so ESC must not attempt to focus the cell.
    if (!this.navigator.activeItem) {
      return super.isActionableLocation;
    }
  }
  // This does not need a className on Widgets.
  // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
  // to the Widget it's mixed in to should implement thus.
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TransactionalFeatureMixin
 */
/**
 * This mixin declares a common config to disable feature transactions in components which support scheduling engine:
 * SchedulerPro and Gantt.
 * @mixin
 */
var TransactionalFeatureMixin = (Target => class TransactionalFeatureMixin extends Target {
  static get $name() {
    return 'TransactionalFeatureMixin';
  }
  static configurable = {
    /**
     * When true, some features will start a project transaction, blocking the project queue, suspending
     * store events and preventing UI from updates. It behaves similar to
     * {@link Grid.column.Column#config-instantUpdate} set to `false`.
     * Set `false` to not use project queue.
     * @config {Boolean}
     * @internal
     * @default
     */
    enableTransactionalFeatures: false,
    testConfig: {
      enableTransactionalFeatures: false
    }
  };
  get widgetClass() {}
  /**
   * Returns `true` if queue is supported and enabled
   * @member {Boolean}
   * @internal
   * @readonly
   */
  get transactionalFeaturesEnabled() {
    var _this$project;
    return this.enableTransactionalFeatures && ((_this$project = this.project) === null || _this$project === void 0 ? void 0 : _this$project.queue);
  }
});

/**
 * @module Scheduler/data/mixin/AttachToProjectMixin
 */
/**
 * Mixin that calls the target class `attachToProject()` function when a new project is assigned to Scheduler/Gantt.
 *
 * @mixin
 */
var AttachToProjectMixin = (Target => class AttachToProjectMixin extends Target {
  static get $name() {
    return 'AttachToProjectMixin';
  }
  async afterConstruct() {
    var _projectHolder$projec;
    super.afterConstruct();
    const me = this,
      projectHolder = me.client || me.grid,
      {
        project
      } = projectHolder;
    (_projectHolder$projec = projectHolder.projectSubscribers) === null || _projectHolder$projec === void 0 ? void 0 : _projectHolder$projec.push(me);
    // Attach to already existing stores
    if (project) {
      me.attachToProject(project);
      me.attachToResourceStore(project.resourceStore);
      me.attachToEventStore(project.eventStore);
      me.attachToAssignmentStore(project.assignmentStore);
      me.attachToDependencyStore(project.dependencyStore);
      me.attachToCalendarManagerStore(project.calendarManagerStore);
    }
  }
  /**
   * Override to take action when the project instance is replaced.
   *
   * @param {Scheduler.model.ProjectModel} project
   */
  attachToProject(project) {
    var _super$attachToProjec;
    this.detachListeners('project');
    this._project = project;
    (_super$attachToProjec = super.attachToProject) === null || _super$attachToProjec === void 0 ? void 0 : _super$attachToProjec.call(this, project);
  }
  detachFromProject(project) {
    var _super$detachFromProj;
    (_super$detachFromProj = super.detachFromProject) === null || _super$detachFromProj === void 0 ? void 0 : _super$detachFromProj.call(this, project);
  }
  /**
   * Override to take action when the EventStore instance is replaced, either from being replaced on the project or
   * from assigning a new project.
   *
   * @param {Scheduler.data.EventStore} store
   */
  attachToEventStore(store) {
    var _super$attachToEventS;
    this.detachListeners('eventStore');
    (_super$attachToEventS = super.attachToEventStore) === null || _super$attachToEventS === void 0 ? void 0 : _super$attachToEventS.call(this, store);
  }
  /**
   * Override to take action when the ResourceStore instance is replaced, either from being replaced on the project
   * or from assigning a new project.
   *
   * @param {Scheduler.data.ResourceStore} store
   */
  attachToResourceStore(store) {
    var _super$attachToResour;
    this.detachListeners('resourceStore');
    (_super$attachToResour = super.attachToResourceStore) === null || _super$attachToResour === void 0 ? void 0 : _super$attachToResour.call(this, store);
  }
  /**
   * Override to take action when the AssignmentStore instance is replaced, either from being replaced on the project
   * or from assigning a new project.
   *
   * @param {Scheduler.data.AssignmentStore} store
   */
  attachToAssignmentStore(store) {
    var _super$attachToAssign;
    this.detachListeners('assignmentStore');
    (_super$attachToAssign = super.attachToAssignmentStore) === null || _super$attachToAssign === void 0 ? void 0 : _super$attachToAssign.call(this, store);
  }
  /**
   * Override to take action when the DependencyStore instance is replaced, either from being replaced on the project
   * or from assigning a new project.
   *
   * @param {Scheduler.data.DependencyStore} store
   */
  attachToDependencyStore(store) {
    var _super$attachToDepend;
    this.detachListeners('dependencyStore');
    (_super$attachToDepend = super.attachToDependencyStore) === null || _super$attachToDepend === void 0 ? void 0 : _super$attachToDepend.call(this, store);
  }
  /**
   * Override to take action when the CalendarManagerStore instance is replaced, either from being replaced on the
   * project or from assigning a new project.
   *
   * @param {Core.data.Store} store
   */
  attachToCalendarManagerStore(store) {
    var _super$attachToCalend;
    this.detachListeners('calendarManagerStore');
    (_super$attachToCalend = super.attachToCalendarManagerStore) === null || _super$attachToCalend === void 0 ? void 0 : _super$attachToCalend.call(this, store);
  }
  get project() {
    return this._project;
  }
  get calendarManagerStore() {
    return this.project.calendarManagerStore;
  }
  get assignmentStore() {
    return this.project.assignmentStore;
  }
  get resourceStore() {
    return this.project.resourceStore;
  }
  get eventStore() {
    return this.project.eventStore;
  }
  get dependencyStore() {
    return this.project.dependencyStore;
  }
});

/**
 * @module Scheduler/view/orientation/HorizontalRendering
 */
/**
 * @typedef HorizontalRenderData
 * @property {Scheduler.model.EventModel} eventRecord
 * @property {Date} start Span start
 * @property {Date} end Span end
 * @property {String} rowId Id of the resource row
 * @property {DomConfig[]} children Child elements
 * @property {Number} startMS Wrap element start in milliseconds
 * @property {Number} endMS Span Wrap element end in milliseconds
 * @property {Number} durationMS Wrap duration in milliseconds (not just a difference between start and end)
 * @property {Number} innerStartMS Actual event start in milliseconds
 * @property {Number} innerEndMS Actual event end in milliseconds
 * @property {Number} innerDurationMS Actual event duration in milliseconds
 * @property {Boolean} startsOutsideView True if span starts before time axis start
 * @property {Boolean} endsOutsideView True if span ends after time axis end
 * @property {Number} left Absolute left coordinate of the wrap element
 * @property {Number} width
 * @property {Number} top Absolute top coordinate of the wrap element (can be changed by layout)
 * @property {Number} height
 * @property {Boolean} clippedStart True if start is clipped
 * @property {Boolean} clippedEnd True if end is clipped
 * @private
 */
const releaseEventActions$1 = {
    releaseElement: 1,
    // Not used at all at the moment
    reuseElement: 1 // Used by some other element
  },
  renderEventActions$1 = {
    newElement: 1,
    reuseOwnElement: 1,
    reuseElement: 1
  },
  MAX_WIDTH = 9999999,
  heightEventSorter = ({
    startDateMS: lhs
  }, {
    startDateMS: rhs
  }) => lhs - rhs,
  chronoFields$1 = {
    startDate: 1,
    endDate: 1,
    duration: 1
  };
function getStartEnd(scheduler, eventRecord, useEnd, fieldName, useEventBuffer) {
  var _eventRecord$hasBatch, _eventRecord$meta;
  // Must use Model.get in order to get latest values in case we are inside a batch.
  // EventResize changes the endDate using batching to enable a tentative change
  // via the batchedUpdate event which is triggered when changing a field in a batch.
  // Fall back to accessor if propagation has not populated date fields.
  const {
      timeAxis
    } = scheduler,
    date = eventRecord.isBatchUpdating && !useEventBuffer ? eventRecord.get(fieldName) : eventRecord[fieldName],
    hasBatchedChange = (_eventRecord$hasBatch = eventRecord.hasBatchedChange) === null || _eventRecord$hasBatch === void 0 ? void 0 : _eventRecord$hasBatch.call(eventRecord, fieldName),
    // fillTicks shouldn't be used during resizing for changing date for smooth animation.
    // correct date will be applied after resize, when `isResizing` will be falsy
    useTickDates = scheduler.fillTicks && (!((_eventRecord$meta = eventRecord.meta) !== null && _eventRecord$meta !== void 0 && _eventRecord$meta.isResizing) || !hasBatchedChange);
  if (useTickDates) {
    let tick = timeAxis.getTickFromDate(date);
    if (tick >= 0) {
      // If date matches a tick start/end, use the earlier tick
      if (useEnd && tick === Math.round(tick) && tick > 0) {
        tick--;
      }
      const tickIndex = Math.floor(tick),
        tickRecord = timeAxis.getAt(tickIndex);
      return tickRecord[fieldName].getTime();
    }
  }
  return date === null || date === void 0 ? void 0 : date.getTime();
}
/**
 * Handles event rendering in Schedulers horizontal mode. Reacts to project/store changes to keep the UI up to date.
 *
 * @internal
 */
class HorizontalRendering extends Base.mixin(AttachToProjectMixin) {
  //region Config & Init
  static $name = 'HorizontalRendering';
  static get configurable() {
    return {
      /**
       * Amount of pixels to extend the current visible range at both ends with when deciding which events to
       * render. Only applies when using labels or for milestones
       * @config {Number}
       * @default
       */
      bufferSize: 150,
      verticalBufferSize: 150
    };
  }
  static get properties() {
    return {
      // Map with event DOM configs, keyed by resource id
      resourceMap: new Map(),
      // Map with visible events DOM configs, keyed by row instance
      rowMap: new Map(),
      eventConfigs: [],
      // Flag to avoid transitioning on first refresh
      isFirstRefresh: true,
      toDrawOnProjectRefresh: new Set(),
      toDrawOnDataReady: new Set()
    };
  }
  construct(scheduler) {
    const me = this;
    me.client = me.scheduler = scheduler;
    me.eventSorter = me.eventSorter.bind(scheduler);
    me.scrollBuffer = scheduler.scrollBuffer;
    // Catch scroll before renderers are called
    scheduler.scrollable.ion({
      scroll: 'onEarlyScroll',
      prio: 1,
      thisObj: me
    });
    scheduler.rowManager.ion({
      name: 'rowManager',
      renderDone: 'onRenderDone',
      removeRows: 'onRemoveRows',
      translateRow: 'onTranslateRow',
      offsetRows: 'onOffsetRows',
      beforeRowHeight: 'onBeforeRowHeightChange',
      thisObj: me
    });
    super.construct({});
  }
  init() {}
  updateVerticalBufferSize() {
    const {
      rowManager
    } = this.scheduler;
    if (this.scheduler.isPainted) {
      // Refresh rows when vertical buffer size changes to trigger event repaint. Required for the export feature.
      rowManager.renderRows(rowManager.rows);
    }
  }
  //endregion
  //region Region, dates & coordinates
  get visibleDateRange() {
    return this._visibleDateRange;
  }
  getDateFromXY(xy, roundingMethod, local, allowOutOfRange = false) {
    const {
      scheduler
    } = this;
    let coord = xy[0];
    if (!local) {
      coord = this.translateToScheduleCoordinate(coord);
    }
    coord = scheduler.getRtlX(coord);
    return scheduler.timeAxisViewModel.getDateFromPosition(coord, roundingMethod, allowOutOfRange);
  }
  translateToScheduleCoordinate(x) {
    const {
        scheduler
      } = this,
      {
        scrollable
      } = scheduler.timeAxisSubGrid;
    let result = x - scheduler.timeAxisSubGridElement.getBoundingClientRect().left - globalThis.scrollX;
    // Because we use getBoundingClientRect's left, we have to adjust for page scroll.
    // The vertical counterpart uses the _bodyRectangle which was created with that adjustment.
    if (scheduler.rtl) {
      result += scrollable.maxX - Math.abs(scheduler.scrollLeft);
    } else {
      result += scheduler.scrollLeft;
    }
    return result;
  }
  translateToPageCoordinate(x) {
    const {
        scheduler
      } = this,
      {
        scrollable
      } = scheduler.timeAxisSubGrid;
    let result = x + scheduler.timeAxisSubGridElement.getBoundingClientRect().left;
    if (scheduler.rtl) {
      result -= scrollable.maxX - Math.abs(scheduler.scrollLeft);
    } else {
      result -= scheduler.scrollLeft;
    }
    return result;
  }
  /**
   * Gets the region, relative to the page, represented by the schedule and optionally only for a single resource.
   * This method will call getDateConstraints to allow for additional resource/event based constraints. By overriding
   * that method you can constrain events differently for different resources.
   * @param {Scheduler.model.ResourceModel} [resourceRecord] (optional) The row record
   * @param {Scheduler.model.EventModel} [eventRecord] (optional) The event record
   * @returns {Core.helper.util.Rectangle} The region of the schedule
   */
  getScheduleRegion(resourceRecord, eventRecord, local = true, dateConstraints, stretch = false) {
    var _dateConstraints, _scheduler$getDateCon;
    const me = this,
      {
        scheduler
      } = me,
      {
        timeAxisSubGridElement,
        timeAxis
      } = scheduler,
      resourceMargin = (!stretch || resourceRecord) && scheduler.getResourceMargin(resourceRecord) || 0;
    let region;
    if (resourceRecord) {
      const eventElement = eventRecord && scheduler.getElementsFromEventRecord(eventRecord, resourceRecord)[0];
      region = Rectangle.from(scheduler.getRowById(resourceRecord.id).getElement('normal'), timeAxisSubGridElement);
      if (eventElement) {
        const eventRegion = Rectangle.from(eventElement, timeAxisSubGridElement);
        region.y = eventRegion.y;
        region.bottom = eventRegion.bottom;
      } else {
        region.y = region.y + resourceMargin;
        region.bottom = region.bottom - resourceMargin;
      }
    } else {
      // The coordinate space needs to be sorted out here!
      region = Rectangle.from(timeAxisSubGridElement).moveTo(null, 0);
      region.width = timeAxisSubGridElement.scrollWidth;
      region.y = region.y + resourceMargin;
      region.bottom = region.bottom - resourceMargin;
    }
    const taStart = timeAxis.startDate,
      taEnd = timeAxis.endDate;
    dateConstraints = ((_dateConstraints = dateConstraints) === null || _dateConstraints === void 0 ? void 0 : _dateConstraints.start) && dateConstraints || ((_scheduler$getDateCon = scheduler.getDateConstraints) === null || _scheduler$getDateCon === void 0 ? void 0 : _scheduler$getDateCon.call(scheduler, resourceRecord, eventRecord)) || {
      start: taStart,
      end: taEnd
    };
    let startX = scheduler.getCoordinateFromDate(dateConstraints.start ? DateHelper.max(taStart, dateConstraints.start) : taStart),
      endX = scheduler.getCoordinateFromDate(dateConstraints.end ? DateHelper.min(taEnd, dateConstraints.end) : taEnd);
    if (!local) {
      startX = me.translateToPageCoordinate(startX);
      endX = me.translateToPageCoordinate(endX);
    }
    region.left = Math.min(startX, endX);
    region.right = Math.max(startX, endX);
    return region;
  }
  /**
   * Gets the Region, relative to the timeline view element, representing the passed row and optionally just for a
   * certain date interval.
   * @param {Core.data.Model} rowRecord The row record
   * @param {Date} startDate A start date constraining the region
   * @param {Date} endDate An end date constraining the region
   * @returns {Core.helper.util.Rectangle} The Rectangle which encapsulates the row
   */
  getRowRegion(rowRecord, startDate, endDate) {
    const {
        scheduler
      } = this,
      {
        timeAxis
      } = scheduler,
      row = scheduler.getRowById(rowRecord.id);
    // might not be rendered
    if (!row) {
      return null;
    }
    const taStart = timeAxis.startDate,
      taEnd = timeAxis.endDate,
      start = startDate ? DateHelper.max(taStart, startDate) : taStart,
      end = endDate ? DateHelper.min(taEnd, endDate) : taEnd,
      startX = scheduler.getCoordinateFromDate(start),
      endX = scheduler.getCoordinateFromDate(end, true, true),
      y = row.top,
      x = Math.min(startX, endX),
      bottom = y + row.offsetHeight;
    return new Rectangle(x, y, Math.max(startX, endX) - x, bottom - y);
  }
  getResourceEventBox(eventRecord, resourceRecord, includeOutside, roughly = false) {
    const resourceData = this.resourceMap.get(resourceRecord.id);
    let eventLayout = null,
      approx = false;
    if (resourceData) {
      eventLayout = resourceData.eventsData.find(d => d.eventRecord === eventRecord);
    }
    // Outside of view, layout now if supposed to be included
    if (!eventLayout) {
      eventLayout = this.getTimeSpanRenderData(eventRecord, resourceRecord, {
        viewport: true,
        timeAxis: includeOutside
      });
      approx = true;
    }
    if (eventLayout) {
      // Event layout is relative to row, need to make to absolute before returning
      const rowBox = this.scheduler.rowManager.getRecordCoords(resourceRecord, true, roughly),
        absoluteTop = eventLayout.top + rowBox.top,
        box = new Rectangle(eventLayout.left, absoluteTop, eventLayout.width, eventLayout.height);
      // Flag informing other parts of the code that this box is approximated
      box.layout = !approx;
      box.rowTop = rowBox.top;
      box.rowBottom = rowBox.bottom;
      box.resourceId = resourceRecord.id;
      return box;
    }
    return null;
  }
  //endregion
  //region Element <-> Record mapping
  resolveRowRecord(elementOrEvent) {
    const me = this,
      {
        scheduler
      } = me,
      element = elementOrEvent.nodeType ? elementOrEvent : elementOrEvent.target,
      // Fix for FF on Linux having text nodes as event.target
      el = element.nodeType === Element.TEXT_NODE ? element.parentElement : element,
      eventNode = el.closest(scheduler.eventSelector);
    if (eventNode) {
      return me.resourceStore.getById(eventNode.dataset.resourceId);
    }
    // When resourceNonWorkingTime.enableMouseEvents is set to true, and the current element is a resource non working time range,
    // in order to get the corresponding resource row, we need to read from data-resource-id attribute.
    // That's because element has not a .b-grid-row as parent when enableMouseEvents is set to true.
    if (!el.closest('.b-grid-row') && el.dataset.resourceId) {
      return me.resourceStore.getById(el.dataset.resourceId);
    }
    return scheduler.getRecordFromElement(el);
  }
  //endregion
  //region Project
  attachToProject(project) {
    super.attachToProject(project);
    this.refreshAllWhenReady = true;
    // Perform a full clear when replacing the project, to not leave any references to old project in DOM
    if (!this.scheduler.isConfiguring) {
      this.clearAll({
        clearDom: true
      });
    }
    project === null || project === void 0 ? void 0 : project.ion({
      name: 'project',
      refresh: 'onProjectRefresh',
      commitFinalized: 'onProjectCommitFinalized',
      thisObj: this
    });
  }
  onProjectCommitFinalized() {
    const {
      scheduler,
      toDrawOnDataReady,
      project
    } = this;
    // Only update the UI immediately if we are visible
    if (scheduler.isVisible) {
      if (scheduler.isPainted && !scheduler.refreshSuspended) {
        // If this is a timezone commit, we got here from a store dataset
        // We need to do a full refresh
        if (!toDrawOnDataReady.size && project.timeZone != null && project.ignoreRecordChanges) {
          project.resourceStore.forEach(r => toDrawOnDataReady.add(r.id));
        }
        if (toDrawOnDataReady.size) {
          this.clearResources(toDrawOnDataReady);
          this.refreshResources(toDrawOnDataReady);
        }
        toDrawOnDataReady.clear();
      }
    }
    // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
    else {
      scheduler.whenVisible('refreshRows');
    }
  }
  onProjectRefresh({
    isCalculated,
    isInitialCommit
  }) {
    const me = this,
      {
        scheduler,
        toDrawOnProjectRefresh
      } = me;
    // Only update the UI immediately if we are visible
    if (scheduler.isVisible) {
      if (scheduler.isPainted && !scheduler.isConfiguring && !scheduler.refreshSuspended) {
        // Either refresh all rows (on for example dataset or when delayed calculations are finished)
        if (me.refreshAllWhenReady || isInitialCommit && isCalculated) {
          scheduler.calculateAllRowHeights(true);
          const {
            rowManager
          } = scheduler;
          // Rows rendered? Refresh
          if (rowManager.topRow) {
            me.clearAll();
            // Refresh only if it won't be refreshed elsewhere (SchedulerStore#onProjectRefresh())
            if (!scheduler.refreshAfterProjectRefresh) {
              // If refresh was suspended when replacing the dataset in a scrolled view we might end up with a
              // topRow outside of available range -> reset it. Call renderRows() to mimic what normally happens
              // when refresh is not suspended
              if (rowManager.topRow.dataIndex >= scheduler.store.count) {
                scheduler.renderRows(false);
              } else {
                // Don't transition first refresh / early render
                scheduler.refreshWithTransition(false, !me.isFirstRefresh && isCalculated && !isInitialCommit);
              }
            }
            me.isFirstRefresh = false;
          }
          // No rows yet, reinitialize (happens if initial project empty and then non empty project assigned)
          else {
            rowManager.reinitialize();
          }
          me.refreshAllWhenReady = false;
        }
        // Or only affected rows (if any)
        else if (toDrawOnProjectRefresh.size) {
          me.refreshResources(toDrawOnProjectRefresh);
        }
        toDrawOnProjectRefresh.clear();
      }
    }
    // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
    else {
      scheduler.whenVisible('refresh', scheduler, [true]);
    }
  }
  //endregion
  //region AssignmentStore
  attachToAssignmentStore(assignmentStore) {
    this.refreshAllWhenReady = true;
    super.attachToAssignmentStore(assignmentStore);
    if (assignmentStore) {
      assignmentStore.ion({
        name: 'assignmentStore',
        changePreCommit: 'onAssignmentStoreChange',
        refreshPreCommit: 'onAssignmentStoreRefresh',
        thisObj: this
      });
    }
  }
  onAssignmentStoreChange({
    source,
    action,
    records: assignmentRecords = [],
    replaced,
    changes
  }) {
    const me = this,
      {
        scheduler
      } = me,
      resourceIds = new Set(assignmentRecords.flatMap(assignmentRecord => {
        var _assignmentRecord$res, _assignmentRecord$res2;
        return [assignmentRecord.resourceId,
        // Also include any linked resources (?. twice since resource might not be resolved and point to id)
        ...(((_assignmentRecord$res = assignmentRecord.resource) === null || _assignmentRecord$res === void 0 ? void 0 : (_assignmentRecord$res2 = _assignmentRecord$res.$links) === null || _assignmentRecord$res2 === void 0 ? void 0 : _assignmentRecord$res2.map(link => link.id)) ?? [])];
      }));
    // Ignore assignment changes caused by removing resources, the remove will redraw things anyway
    // Also ignore case when resource id is changed. In this case row will be refreshed by the grid
    if (me.resourceStore.isRemoving || me.resourceStore.isChangingId) {
      return;
    }
    switch (action) {
      // These operations will invalidate the graph, need to draw later
      case 'dataset':
        {
          // Ignore dataset when using single assignment mode
          if (!me.eventStore.usesSingleAssignment) {
            if (resourceIds.size) {
              me.refreshResourcesWhenReady(resourceIds);
            } else {
              me.clearAll();
              scheduler.refreshWithTransition();
            }
          }
          return;
        }
      case 'add':
      case 'remove':
      case 'updateMultiple':
        me.refreshResourcesWhenReady(resourceIds);
        return;
      case 'removeall':
        me.refreshAllWhenReady = true;
        return;
      case 'replace':
        // Gather resources from both the old record and the new one
        replaced.forEach(([oldAssignment, newAssignment]) => {
          resourceIds.add(oldAssignment.resourceId);
          resourceIds.add(newAssignment.resourceId);
        });
        // And refresh them
        me.refreshResourcesWhenReady(resourceIds);
        return;
      // These operations won't invalidate the graph, redraw now
      case 'filter':
        me.clearAll();
        scheduler.calculateAllRowHeights(true);
        scheduler.refreshWithTransition();
        return;
      case 'update':
        {
          if ('eventId' in changes || 'resourceId' in changes || 'id' in changes) {
            // When reassigning, clear old resource also
            if ('resourceId' in changes) {
              resourceIds.add(changes.resourceId.oldValue);
            }
            // When chaining stores in single assignment mode, we might not be the project store
            if (source === scheduler.project.assignmentStore) {
              me.refreshResourcesOnDataReady(resourceIds);
            }
            // Refresh directly when we are not
            else {
              me.refreshResources(resourceIds);
            }
          }
          break;
        }
      case 'clearchanges':
        {
          const {
            added,
            modified,
            removed
          } = changes;
          // If modified records appear in the clearchanges action we need to refresh entire view
          // because we have not enough information about previously assigned resource
          if (modified.length) {
            scheduler.refreshWithTransition();
          } else {
            added.forEach(r => resourceIds.add(r.resourceId));
            removed.forEach(r => resourceIds.add(r.resourceId));
            me.refreshResourcesOnDataReady(resourceIds);
          }
        }
    }
  }
  onAssignmentStoreRefresh({
    action,
    records
  }) {
    if (action === 'batch') {
      this.clearAll();
      this.scheduler.refreshWithTransition();
    }
  }
  //endregion
  //region EventStore
  attachToEventStore(eventStore) {
    this.refreshAllWhenReady = true;
    super.attachToEventStore(eventStore);
    if (eventStore) {
      eventStore.ion({
        name: 'eventStore',
        addConfirmed: 'onEventStoreAddConfirmed',
        refreshPreCommit: 'onEventStoreRefresh',
        thisObj: this
      });
    }
  }
  onEventStoreAddConfirmed({
    record
  }) {
    for (const element of this.client.getElementsFromEventRecord(record)) {
      element.classList.remove('b-iscreating');
    }
  }
  onEventStoreRefresh({
    action
  }) {
    if (action === 'batch') {
      const {
        scheduler
      } = this;
      if (scheduler.isEngineReady && scheduler.isPainted) {
        this.clearAll();
        scheduler.refreshWithTransition();
      }
    }
  }
  onEventStoreChange({
    action,
    records: eventRecords = [],
    record,
    replaced,
    changes,
    source
  }) {
    const me = this,
      {
        scheduler
      } = me,
      isResourceTimeRange = source.isResourceTimeRangeStore,
      resourceIds = new Set();
    if (!scheduler.isPainted) {
      return;
    }
    eventRecords.forEach(eventRecord => {
      var _eventRecord$$linkedR;
      // Update all resource rows to which this event is assigned *if* the resourceStore
      // contains that resource (We could have filtered the resourceStore)
      const renderedEventResources = (_eventRecord$$linkedR = eventRecord.$linkedResources) === null || _eventRecord$$linkedR === void 0 ? void 0 : _eventRecord$$linkedR.filter(r => me.resourceStore.includes(r));
      // When rendering a Gantt project, the project model also passes through here -> no `resources`
      renderedEventResources === null || renderedEventResources === void 0 ? void 0 : renderedEventResources.forEach(resourceRecord => resourceIds.add(resourceRecord.id));
    });
    if (isResourceTimeRange) {
      switch (action) {
        // - dataset cant pass through same path as events, which relies on project being invalidated. and
        // resource time ranges does not pass through engine
        // - removeall also needs special path, since no resources to redraw will be collected
        case 'removeall':
        case 'dataset':
          me.clearAll();
          scheduler.refreshWithTransition();
          return;
      }
      me.refreshResources(resourceIds);
    } else {
      switch (action) {
        // No-ops
        case 'batch': // Handled elsewhere, don't want it to clear again
        case 'sort': // Order in EventStore does not matter, so these actions are no-ops
        case 'group':
        case 'move':
          return;
        case 'remove':
          // Remove is a no-op since assignment will also be removed
          return;
        case 'clearchanges':
          me.clearAll();
          scheduler.refreshWithTransition();
          return;
        case 'dataset':
          {
            me.clearAll();
            // This is mainly for chained stores, where data is set from main store without project being
            // invalidated. Nothing to wait for, refresh now
            if (scheduler.isEngineReady) {
              scheduler.refreshWithTransition();
            } else {
              me.refreshAllWhenReady = true;
            }
            return;
          }
        case 'add':
        case 'updateMultiple':
          // Just refresh below
          break;
        case 'replace':
          // Gather resources from both the old record and the new one
          replaced.forEach(([, newEvent]) => {
            // Old cleared by changed assignment
            newEvent.resources.map(resourceRecord => resourceIds.add(resourceRecord.id));
          });
          break;
        case 'removeall':
        case 'filter':
          // Filter might be caused by add retriggering filters, in which case we need to refresh later
          if (!scheduler.isEngineReady) {
            me.refreshAllWhenReady = true;
            return;
          }
          // Clear all when filtering for simplicity. If that turns out to give bad performance, one would need to
          // figure out which events was filtered out and only clear their resources.
          me.clearAll();
          scheduler.calculateAllRowHeights(true);
          scheduler.refreshWithTransition();
          return;
        case 'update':
          {
            // Check if changes are graph related or not
            const allChrono = record.$entity ? !Object.keys(changes).some(name => !record.$entity.getField(name)) : !Object.keys(changes).some(name => !chronoFields$1[name]);
            let dateChanges = 0;
            'startDate' in changes && dateChanges++;
            'endDate' in changes && dateChanges++;
            'duration' in changes && dateChanges++;
            if ('resourceId' in changes) {
              resourceIds.add(changes.resourceId.oldValue);
            }
            // If we have a set of resources to update, refresh them.
            // Always redraw non chrono changes (name etc) and chrono changes that can affect appearance
            if (resourceIds.size && (!allChrono ||
            // skip case when changed "duration" only (w/o start/end affected)
            dateChanges && !('duration' in changes && dateChanges === 1) || 'percentDone' in changes || 'inactive' in changes || 'constraintDate' in changes || 'constraintType' in changes || 'segments' in changes)) {
              var _me$project, _me$project2;
              // if we are finalizing data loading let's delay the resources refresh till all the
              // propagation results get into stores
              if ((_me$project = me.project) !== null && _me$project !== void 0 && _me$project.propagatingLoadChanges || (_me$project2 = me.project) !== null && _me$project2 !== void 0 && _me$project2.isWritingData) {
                me.refreshResourcesOnDataReady(resourceIds);
              } else {
                me.refreshResources(resourceIds);
              }
            }
            return;
          }
      }
      me.refreshResourcesWhenReady(resourceIds);
    }
  }
  //endregion
  //region ResourceStore
  attachToResourceStore(resourceStore) {
    this.refreshAllWhenReady = true;
    super.attachToResourceStore(resourceStore);
    if (resourceStore) {
      this.clearAll({
        clearLayoutCache: true
      });
      resourceStore.ion({
        name: 'resourceStore',
        changePreCommit: 'onResourceStoreChange',
        thisObj: this
      });
    }
  }
  get resourceStore() {
    return this.client.store;
  }
  onResourceStoreChange({
    action,
    isExpand,
    records,
    changes
  }) {
    const me = this,
      // Update link + original when asked for link
      resourceIds = records === null || records === void 0 ? void 0 : records.flatMap(r => r.isLinked ? [r.id, r.$originalId] : [r.id]);
    if (!me.scheduler.isPainted) {
      return;
    }
    switch (action) {
      case 'add':
        // #635 Events disappear when toggling other node
        // If we are expanding project won't fire refresh event
        if (!isExpand) {
          // Links won't cause calculations, refresh now
          if (records.every(r => r.isLinked)) {
            me.refreshResources(resourceIds);
          }
          // Otherwise refresh when project is ready
          else {
            me.refreshResourcesWhenReady(resourceIds);
          }
        }
        return;
      case 'update':
        {
          // Ignore changes from project commit, if they affect events they will be redrawn anyway
          // Also ignore explicit transformation of leaf <-> parent
          if (!me.project.isBatchingChanges && !changes.isLeaf) {
            // Resource changes might affect events, refresh
            me.refreshResources(resourceIds);
          }
          return;
        }
      case 'filter':
        // Bail out on filter action. Map was already updated on `refresh` event triggered before this `change`
        // one. And extra records are removed from rowMap by `onRemoveRows`
        return;
      case 'removeall':
        me.clearAll({
          clearLayoutCache: true
        });
        return;
      // We must not clear all resources when whole dataset changes
      // https://github.com/bryntum/support/issues/3292
      case 'dataset':
        return;
    }
    resourceIds && me.clearResources(resourceIds);
  }
  //endregion
  //region RowManager
  onTranslateRow({
    row
  }) {
    // Newly added rows are translated prior to having an id, rule those out since they will be rendered later
    if (row.id != null) {
      // Event layouts are stored relative to the resource, only need to rerender the row to have its absolute
      // position updated to match new translation
      this.refreshEventsForResource(row, false);
    }
  }
  // RowManager error correction, cached layouts will no longer match.
  // Redraw to have events correctly positioned for dependency feature to draw to their elements
  onOffsetRows() {
    this.clearAll();
    this.doUpdateTimeView();
  }
  // Used to pre-calculate row heights
  calculateRowHeight(resourceRecord) {
    var _resourceRecord$assig;
    const {
        scheduler
      } = this,
      rowHeight = scheduler.getResourceHeight(resourceRecord),
      eventLayout = scheduler.getEventLayout(resourceRecord),
      layoutType = eventLayout.type;
    if (layoutType === 'stack' && scheduler.isEngineReady && !resourceRecord.isSpecialRow &&
    // Generated parents when TreeGrouping do not have assigned bucket
    ((_resourceRecord$assig = resourceRecord.assigned) === null || _resourceRecord$assig === void 0 ? void 0 : _resourceRecord$assig.size) > 1) {
      const {
          assignmentStore,
          eventStore,
          timeAxis
        } = scheduler,
        {
          barMargin,
          resourceMargin,
          contentHeight
        } = scheduler.getResourceLayoutSettings(resourceRecord),
        // When using an AssignmentStore we will get all events for the resource even if the EventStore is
        // filtered
        eventFilter = (eventStore.isFiltered || assignmentStore.isFiltered) && (eventRecord => eventRecord.assignments.some(a => a.resource === resourceRecord.$original && assignmentStore.includes(a))),
        events = eventStore.getEvents({
          resourceRecord,
          includeOccurrences: scheduler.enableRecurringEvents,
          startDate: timeAxis.startDate,
          endDate: timeAxis.endDate,
          filter: eventFilter
        }).sort(heightEventSorter).map(eventRecord => {
          const
            // Must use Model.get in order to get latest values in case we are inside a batch.
            // EventResize changes the endDate using batching to enable a tentative change
            // via the batchedUpdate event which is triggered when changing a field in a batch.
            // Fall back to accessor if propagation has not populated date fields.
            startDate = eventRecord.isBatchUpdating ? eventRecord.get('startDate') : eventRecord.startDate,
            endDate = eventRecord.isBatchUpdating ? eventRecord.get('endDate') : eventRecord.endDate || startDate;
          return {
            eventRecord,
            resourceRecord,
            startMS: startDate.getTime(),
            endMS: endDate.getTime()
          };
        }),
        layoutHandler = scheduler.getEventLayoutHandler(eventLayout),
        nbrOfBandsRequired = layoutHandler.layoutEventsInBands(events, resourceRecord, true);
      if (layoutHandler.type === 'layoutFn') {
        return nbrOfBandsRequired;
      }
      return nbrOfBandsRequired * contentHeight + (nbrOfBandsRequired - 1) * barMargin + resourceMargin * 2;
    }
    return rowHeight;
  }
  //endregion
  //region TimeAxis
  doUpdateTimeView() {
    const {
      scrollable
    } = this.scheduler.timeAxisSubGrid;
    // scrollLeft is the DOM's concept which is -ve in RTL mode.
    // scrollX is always the +ve scroll offset from the origin.
    // Both may be needed for different calculations.
    this.updateFromHorizontalScroll(scrollable.x, true);
  }
  onTimeAxisViewModelUpdate() {
    const me = this,
      {
        scheduler
      } = me;
    me.clearAll();
    // If refresh is suspended, update timeView as soon as refresh gets unsuspended
    if (scheduler.refreshSuspended) {
      me.detachListeners('renderingSuspend');
      scheduler.ion({
        name: 'renderingSuspend',
        resumeRefresh({
          trigger
        }) {
          // This code will try to refresh rows, but resumeRefresh event doesn't guarantee rowManager rows are
          // in actual state. e.g. if resources were removed during a suspended refresh rowManager won't get a
          // chance to update them until `refresh` event from the project. We can safely update the view only
          // if engine in ready (not committing), otherwise we leave refresh a liability of normal project refresh
          // logic. Covered by SchedulerRendering.t.js
          // https://github.com/bryntum/support/issues/1462
          if (scheduler.isEngineReady && trigger) {
            me.doUpdateTimeView();
          }
        },
        thisObj: me,
        once: true
      });
    }
    // Call update anyway. If refresh is suspended this call will only update visible date range and will not redraw rows
    me.doUpdateTimeView();
  }
  //endregion
  //region Dependency connectors
  /**
   * Gets displaying item start side
   *
   * @param {Scheduler.model.EventModel} eventRecord
   * @returns {'start'|'end'|'top'|'bottom'} 'start' / 'end' / 'top' / 'bottom'
   */
  getConnectorStartSide(eventRecord) {
    return 'start';
  }
  /**
   * Gets displaying item end side
   *
   * @param {Scheduler.model.EventModel} eventRecord
   * @returns {'start'|'end'|'top'|'bottom'} 'start' / 'end' / 'top' / 'bottom'
   */
  getConnectorEndSide(eventRecord) {
    return 'end';
  }
  //endregion
  //region Scheduler hooks
  refreshRows(reLayoutEvents) {
    if (reLayoutEvents) {
      this.clearAll();
    }
  }
  // Clear events in case they use date as part of displayed info
  onLocaleChange() {
    this.clearAll();
  }
  // Called when viewport size changes
  onViewportResize(width, height, oldWidth, oldHeight) {
    // We don't draw events for all rendered rows, "refresh" when height changes to make sure events in previously
    // invisible rows gets displayed
    if (height > oldHeight) {
      this.onRenderDone();
    }
  }
  // Called from EventDrag
  onDragAbort({
    context,
    dragData
  }) {
    // Aborted a drag in a scrolled scheduler, with origin now out of view. Element is no longer needed
    if (this.resourceStore.indexOf(dragData.record.resource) < this.scheduler.topRow.dataIndex) {
      context.element.remove();
    }
  }
  // Called from EventSelection
  toggleCls(assignmentRecord, cls, add = true, useWrapper = false) {
    const element = this.client.getElementFromAssignmentRecord(assignmentRecord, useWrapper),
      resourceData = this.resourceMap.get(assignmentRecord.isModel ? assignmentRecord.get('resourceId') : assignmentRecord.resourceId),
      eventData = resourceData === null || resourceData === void 0 ? void 0 : resourceData.eventsData.find(d => d.eventId === assignmentRecord.eventId);
    // Update cached config
    if (eventData) {
      eventData[useWrapper ? 'wrapperCls' : 'cls'][cls] = add;
    }
    // Live update element
    if (element) {
      // Update element
      element.classList[add ? 'add' : 'remove'](cls);
      // And its DOM config
      element.lastDomConfig.className[cls] = add;
    }
  }
  // React to rows being removed, refreshes view without any relayouting needed since layout is cached relative to row
  onRemoveRows({
    rows
  }) {
    rows.forEach(row => this.rowMap.delete(row));
    this.onRenderDone();
  }
  // Reset renderer flag before any renderers are called
  onEarlyScroll() {
    this.rendererCalled = false;
  }
  // If vertical scroll did not cause a renderer to be called we still want to update since we only draw events in
  // view, "independent" from their rows
  updateFromVerticalScroll() {
    this.fromScroll = true;
    if (!this.rendererCalled) {
      this.onRenderDone();
    }
  }
  // Update header range on horizontal scroll. No need to draw any tasks, Gantt only cares about vertical scroll
  updateFromHorizontalScroll(scrollX, force) {
    var _me$_visibleDateRange, _me$_visibleDateRange2;
    const me = this,
      {
        scheduler,
        scrollBuffer
      } = me,
      renderAll = scrollBuffer === -1,
      {
        timeAxisSubGrid,
        timeAxis,
        rtl
      } = scheduler,
      {
        width
      } = timeAxisSubGrid,
      {
        totalSize
      } = scheduler.timeAxisViewModel,
      start = scrollX,
      // If there are few pixels left from the right most position then just render all remaining ticks,
      // there wouldn't be many. It makes end date reachable with more page zoom levels while not having any poor
      // implications.
      // 5px to make TimeViewRangePageZoom test stable in puppeteer.
      returnEnd = timeAxisSubGrid.scrollable.maxX !== 0 && Math.abs(timeAxisSubGrid.scrollable.maxX) <= Math.round(start) + 5,
      startDate = renderAll ? timeAxis.startDate : scheduler.getDateFromCoord({
        coord: Math.max(0, start - scrollBuffer),
        ignoreRTL: true
      }),
      endDate = returnEnd || renderAll ? timeAxis.endDate : scheduler.getDateFromCoord({
        coord: start + width + scrollBuffer,
        ignoreRTL: true
      }) || timeAxis.endDate;
    if (startDate && !scheduler._viewPresetChanging && (
    // If rendering all, no action needed if scrolling horizontally unless start/end/tick size etc changes
    !renderAll || force || startDate - (((_me$_visibleDateRange = me._visibleDateRange) === null || _me$_visibleDateRange === void 0 ? void 0 : _me$_visibleDateRange.startDate) || 0) || endDate - (((_me$_visibleDateRange2 = me._visibleDateRange) === null || _me$_visibleDateRange2 === void 0 ? void 0 : _me$_visibleDateRange2.endDate) || 0))) {
      me._visibleDateRange = {
        startDate,
        endDate,
        startMS: startDate.getTime(),
        endMS: endDate.getTime()
      };
      me.viewportCoords = renderAll ? {
        left: 0,
        right: totalSize
      } : rtl
      // RTL starts all the way to the right (and goes in opposite direction)
      ? {
        left: totalSize - scrollX - width + scrollBuffer,
        right: totalSize - scrollX - scrollBuffer
      }
      // LTR all the way to the left
      : {
        left: scrollX - scrollBuffer,
        right: scrollX + width + scrollBuffer
      };
      // Update timeaxis header making it display the new dates
      const range = scheduler.timeView.range = {
        startDate,
        endDate
      };
      scheduler.onVisibleDateRangeChange(range);
      // If refresh is suspended, someone else is responsible for updating the UI later
      if (!scheduler.refreshSuspended && scheduler.rowManager.rows.length) {
        // Gets here too early in Safari for ResourceHistogram. ResizeObserver triggers a scroll before rows are
        // rendered first time. Could not track down why, bailing out
        if (scheduler.rowManager.rows[0].id === null) {
          return;
        }
        me.fromScroll = true;
        scheduler.rowManager.rows.forEach(row => me.refreshEventsForResource(row, false, false));
        me.onRenderDone();
      }
    }
  }
  // Called from SchedulerEventRendering
  repaintEventsForResource(resourceRecord) {
    this.refreshResources([resourceRecord.id]);
  }
  onBeforeRowHeightChange() {
    // Row height is cached per resource, all have to be re-laid out
    this.clearAll();
  }
  //endregion
  //region Refresh resources
  refreshResourcesOnDataReady(resourceIds) {
    resourceIds.forEach(id => this.toDrawOnDataReady.add(id));
  }
  /**
   * Clears resources directly and redraws them on next project refresh
   * @param {Number[]|String[]} resourceIds
   * @private
   */
  refreshResourcesWhenReady(resourceIds) {
    this.clearResources(resourceIds);
    resourceIds.forEach(id => this.toDrawOnProjectRefresh.add(id));
  }
  /**
   * Clears and redraws resources directly. Respects schedulers refresh suspension
   * @param {Number[]|String[]} ids Resource ids
   * @param {Boolean} [transition] Use transition or not
   * @private
   */
  refreshResources(ids, transition = true) {
    const me = this,
      {
        scheduler
      } = me,
      rows = [],
      noRows = [];
    me.clearResources(ids);
    if (!scheduler.refreshSuspended) {
      ids.forEach(id => {
        const row = scheduler.getRowById(id);
        if (row) {
          rows.push(row);
        } else {
          noRows.push(row);
        }
      });
      scheduler.runWithTransition(() => {
        // Rendering rows populates row heights, but not all resources might have a row in view
        scheduler.calculateRowHeights(noRows.map(id => this.resourceStore.getById(id)), true);
        // Render those that do
        scheduler.rowManager.renderRows(rows);
      }, transition);
    }
  }
  //endregion
  //region Stack & pack
  layoutEventVerticallyStack(bandIndex, eventRecord, resourceRecord) {
    const {
      barMargin,
      resourceMargin,
      contentHeight
    } = this.scheduler.getResourceLayoutSettings(resourceRecord, eventRecord.parent);
    return bandIndex === 0 ? resourceMargin : resourceMargin + bandIndex * contentHeight + bandIndex * barMargin;
  }
  layoutEventVerticallyPack(topFraction, heightFraction, eventRecord, resourceRecord) {
    const {
        barMargin,
        resourceMargin,
        contentHeight
      } = this.scheduler.getResourceLayoutSettings(resourceRecord, eventRecord.parent),
      count = 1 / heightFraction,
      bandIndex = topFraction * count,
      // "y" within row
      height = (contentHeight - (count - 1) * barMargin) * heightFraction,
      top = resourceMargin + bandIndex * height + bandIndex * barMargin;
    return {
      top,
      height
    };
  }
  //endregion
  //region Render
  /**
   * Used by event drag features to bring into existence event elements that are outside of the rendered block.
   * @param {Scheduler.model.TimeSpan} eventRecord The event to render
   * @param {Scheduler.model.ResourceModel} [resourceRecord] The event to render
   * @private
   */
  addTemporaryDragElement(eventRecord, resourceRecord = eventRecord.resource) {
    const {
        scheduler
      } = this,
      renderData = scheduler.generateRenderData(eventRecord, resourceRecord, {
        timeAxis: true,
        viewport: true
      });
    renderData.absoluteTop = renderData.row ? renderData.top + renderData.row.top : scheduler.getResourceEventBox(eventRecord, resourceRecord, true).top;
    const domConfig = this.renderEvent(renderData),
      {
        dataset
      } = domConfig;
    delete domConfig.tabIndex;
    delete dataset.eventId;
    delete dataset.resourceId;
    delete dataset.assignmentId;
    delete dataset.syncId;
    dataset.transient = true;
    domConfig.parent = this.scheduler.foregroundCanvas;
    // So that the regular DomSyncing which may happen during scroll does not
    // sweep up and reuse the temporary element.
    domConfig.retainElement = true;
    const result = DomHelper.createElement(domConfig);
    result.innerElement = result.firstChild;
    eventRecord.instanceMeta(scheduler).hasTemporaryDragElement = true;
    return result;
  }
  // Earlier start dates are above later tasks
  // If same start date, longer tasks float to top
  // If same start + duration, sort by name
  // Fn can be called with layout date or event records (from EventNavigation)
  eventSorter(a, b) {
    if (this.overlappingEventSorter) {
      return this.overlappingEventSorter(a.eventRecord || a, b.eventRecord || b);
    }
    const startA = a.isModel ? a.startDateMS : a.dataStartMS || a.startMS,
      // dataXX are used if configured with fillTicks
      endA = a.isModel ? a.endDateMS : a.dataEndMS || a.endMS,
      startB = b.isModel ? b.startDateMS : b.dataStartMS || b.startMS,
      endB = b.isModel ? b.endDateMS : b.dataEndMS || b.endMS,
      nameA = a.isModel ? a.name : a.eventRecord.name,
      nameB = b.isModel ? b.name : b.eventRecord.name;
    return startA - startB || endB - endA || (nameA < nameB ? -1 : nameA == nameB ? 0 : 1);
  }
  /**
   * Converts a start/endDate into a MS value used when rendering the event. If scheduler is configured with
   * `fillTicks: true` the value returned will be snapped to tick start/end.
   * @private
   * @param {Scheduler.model.TimeSpan} eventRecord
   * @param {String} startDateField
   * @param {String} endDateField
   * @param {Boolean} useEventBuffer
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @returns {Object} Object of format { startMS, endMS, durationMS }
   */
  calculateMS(eventRecord, startDateField, endDateField, useEventBuffer, resourceRecord) {
    const me = this,
      {
        scheduler
      } = me,
      {
        timeAxisViewModel
      } = scheduler;
    let startMS = getStartEnd(scheduler, eventRecord, false, startDateField, useEventBuffer),
      endMS = getStartEnd(scheduler, eventRecord, true, endDateField, useEventBuffer),
      durationMS = endMS - startMS;
    if (scheduler.milestoneLayoutMode !== 'default' && durationMS === 0) {
      const pxPerMinute = timeAxisViewModel.getSingleUnitInPixels('minute'),
        lengthInPx = scheduler.getMilestoneLabelWidth(eventRecord, resourceRecord),
        duration = lengthInPx * (1 / pxPerMinute);
      durationMS = duration * 60 * 1000;
      if (scheduler.milestoneTextPosition === 'always-outside') {
        // Milestone is offset half a diamond to the left (compensated in CSS with padding) for the layout pass,
        // to take diamond corner into account
        const diamondSize = scheduler.getResourceLayoutSettings(resourceRecord, eventRecord.parent).contentHeight,
          diamondMS = diamondSize * (1 / pxPerMinute) * 60 * 1000;
        startMS -= diamondMS / 2;
        endMS = startMS + durationMS;
      } else {
        switch (scheduler.milestoneAlign) {
          case 'start':
          case 'left':
            endMS = startMS + durationMS;
            break;
          case 'end':
          case 'right':
            endMS = startMS;
            startMS = endMS - durationMS;
            break;
          default:
            // using center as default
            endMS = startMS + durationMS / 2;
            startMS = endMS - durationMS;
            break;
        }
      }
    }
    return {
      startMS,
      endMS,
      durationMS
    };
  }
  /**
   * Returns event render data except actual position information.
   * @param timeSpan
   * @param rowRecord
   * @returns {HorizontalRenderData}
   * @private
   */
  setupRenderData(timeSpan, rowRecord) {
    var _scheduler$features$e;
    const me = this,
      {
        scheduler
      } = me,
      {
        timeAxis,
        timeAxisViewModel
      } = scheduler,
      {
        preamble,
        postamble
      } = timeSpan,
      useEventBuffer = me.isProHorizontalRendering && ((_scheduler$features$e = scheduler.features.eventBuffer) === null || _scheduler$features$e === void 0 ? void 0 : _scheduler$features$e.enabled) && (preamble || postamble) && !timeSpan.isMilestone,
      pxPerMinute = timeAxisViewModel.getSingleUnitInPixels('minute'),
      {
        isBatchUpdating
      } = timeSpan,
      startDateField = useEventBuffer ? 'wrapStartDate' : 'startDate',
      endDateField = useEventBuffer ? 'wrapEndDate' : 'endDate',
      // Must use Model.get in order to get latest values in case we are inside a batch.
      // EventResize changes the endDate using batching to enable a tentative change
      // via the batchedUpdate event which is triggered when changing a field in a batch.
      // Fall back to accessor if propagation has not populated date fields.
      // Use endDate accessor if duration has not been propagated to create endDate
      timespanStart = isBatchUpdating && !useEventBuffer ? timeSpan.get(startDateField) : timeSpan[startDateField],
      // Allow timespans to be rendered even when they are missing an end date
      timespanEnd = isBatchUpdating && !useEventBuffer ? timeSpan.get(endDateField) : timeSpan[endDateField] || timespanStart,
      viewStartMS = timeAxis.startMS,
      viewEndMS = timeAxis.endMS,
      {
        startMS,
        endMS,
        durationMS
      } = me.calculateMS(timeSpan, startDateField, endDateField, useEventBuffer, rowRecord),
      // These flags have two components because includeOutsideViewport
      // means that we can be calculating data for events either side of
      // the TimeAxis.
      // The start is outside of the view if it's before *or after* the TimeAxis range.
      // 1 set means the start is before the TimeAxis
      // 2 set means the start is after the TimeAxis
      // Either way, a truthy value means that the start is outside of the TimeAxis.
      startsOutsideView = startMS < viewStartMS | (startMS > viewEndMS) << 1,
      // The end is outside of the view if it's before *or after* the TimeAxis range.
      // 1 set means the end is after the TimeAxis
      // 2 set means the end is before the TimeAxis
      // Either way, a truthy value means that the end is outside of the TimeAxis.
      endsOutsideView = endMS > viewEndMS | (endMS <= viewStartMS) << 1,
      durationMinutes = durationMS / (1000 * 60),
      width = endsOutsideView ? pxPerMinute * durationMinutes : null,
      row = scheduler.getRowById(rowRecord);
    return {
      eventRecord: timeSpan,
      taskRecord: timeSpan,
      // Helps with using Gantt projects in Scheduler Pro
      start: timespanStart,
      end: timespanEnd,
      rowId: rowRecord.id,
      children: [],
      startMS,
      endMS,
      durationMS,
      startsOutsideView,
      endsOutsideView,
      width,
      row,
      useEventBuffer
    };
  }
  /**
   * Populates render data with information about width and horizontal position of the wrap.
   * @param {HorizontalRenderData} renderData
   * @returns {Boolean}
   * @private
   */
  fillTimeSpanHorizontalPosition(renderData) {
    const {
        startMS,
        endMS,
        durationMS
      } = renderData,
      // With delayed calculation there is no guarantee data is normalized, might be missing a crucial component
      result = startMS != null && endMS != null && this.calculateHorizontalPosition(renderData, startMS, endMS, durationMS);
    if (result) {
      Object.assign(renderData, result);
      return true;
    }
    return false;
  }
  /**
   * Fills render data with `left` and `width` properties
   * @param {HorizontalRenderData} renderData
   * @param {Number} startMS
   * @param {Number} endMS
   * @param {Number} durationMS
   * @returns {{left: number, width: number, clippedStart: boolean, clippedEnd: boolean}|null}
   * @private
   */
  calculateHorizontalPosition(renderData, startMS, endMS, durationMS) {
    const {
        scheduler
      } = this,
      {
        timeAxis,
        timeAxisViewModel
      } = scheduler,
      {
        startsOutsideView,
        endsOutsideView,
        eventRecord
      } = renderData,
      viewStartMS = timeAxis.startMS,
      pxPerMinute = timeAxisViewModel.getSingleUnitInPixels('minute'),
      durationMinutes = durationMS / (1000 * 60),
      width = endsOutsideView ? pxPerMinute * durationMinutes : null;
    let endX = scheduler.getCoordinateFromDate(endMS, {
        local: true,
        respectExclusion: true,
        isEnd: true
      }),
      startX,
      clippedStart = false,
      clippedEnd = false;
    // If event starts outside of view, estimate where.
    if (startsOutsideView) {
      startX = (startMS - viewStartMS) / (1000 * 60) * pxPerMinute;
      // Flip -ve startX to being to the right of the viewport end
      if (scheduler.rtl) {
        startX = scheduler.timeAxisSubGrid.scrollable.scrollWidth - startX;
      }
    }
    // Starts in view, calculate exactly
    else {
      // If end date is included in time axis but start date is not (when using time axis exclusions), snap start date to next included data
      startX = scheduler.getCoordinateFromDate(startMS, {
        local: true,
        respectExclusion: true,
        isEnd: false,
        snapToNextIncluded: endX !== -1
      });
      clippedStart = startX === -1;
    }
    if (endsOutsideView) {
      // Have to clip the events in Safari when using stickyEvents, it does not support `overflow: clip`
      if (BrowserHelper.isSafari && scheduler.features.stickyEvents && timeAxis.endMS || endX === -1 && !timeAxis.continuous) {
        endX = scheduler.getCoordinateFromDate(timeAxis.endMS);
      } else {
        // Parentheses needed
        endX = startX + width * (scheduler.rtl ? -1 : 1);
      }
    } else {
      clippedEnd = endX === -1;
    }
    if (clippedEnd && !clippedStart) {
      // We know where to start but not where to end, snap it (the opposite is already handled by the
      // snapToNextIncluded flag when calculating startX above)
      endX = scheduler.getCoordinateFromDate(endMS, {
        local: true,
        respectExclusion: true,
        isEnd: true,
        snapToNextIncluded: true
      });
    }
    // If the element is very wide there's no point in displaying it all.
    // Indeed the element may not be displayable at extremely large widths.
    if (width > MAX_WIDTH) {
      // The start is before the TimeAxis start
      if (startsOutsideView === 1) {
        // Both ends outside - spans TimeAxis
        if (endsOutsideView === 1) {
          startX = -100;
          endX = scheduler.timeAxisColumn.width + 100;
        }
        // End is in view
        else {
          startX = endX - MAX_WIDTH;
        }
      }
      // The end is after, but the start is in view
      else if (endsOutsideView === 1) {
        endX = startX + MAX_WIDTH;
      }
    }
    if (clippedStart && clippedEnd) {
      // Both ends excluded, but there might be some part in between that should be displayed...
      startX = scheduler.getCoordinateFromDate(startMS, {
        local: true,
        respectExclusion: true,
        isEnd: false,
        snapToNextIncluded: true,
        max: endMS
      });
      endX = scheduler.getCoordinateFromDate(endMS, {
        local: true,
        respectExclusion: true,
        isEnd: true,
        snapToNextIncluded: true,
        min: startMS
      });
      if (startX === endX) {
        // Raise flag on instance meta to avoid duplicating this logic
        eventRecord.instanceMeta(scheduler).excluded = true;
        // Excluded by time axis exclusion rules, render nothing
        return null;
      }
    }
    return {
      left: Math.min(startX, endX),
      // Use min width 5 for normal events, 0 for milestones (won't have width specified at all in the
      // end). During drag create a normal event can get 0 duration, in this case we still want it to
      // get a min width of 5 (6px for wrapper, -1 px for event element
      width: Math.abs(endX - startX) || (eventRecord.isMilestone && !eventRecord.meta.isDragCreating ? 0 : 6),
      clippedStart,
      clippedEnd
    };
  }
  fillTimeSpanVerticalPosition(renderData, rowRecord) {
    const {
        scheduler
      } = this,
      {
        start,
        end
      } = renderData,
      {
        resourceMargin,
        contentHeight
      } = scheduler.getResourceLayoutSettings(rowRecord);
    // If filling ticks we need to also keep data's MS values, since they are used for sorting timespans
    if (scheduler.fillTicks) {
      renderData.dataStartMS = start.getTime();
      renderData.dataEndMS = end.getTime();
    }
    renderData.top = Math.max(0, resourceMargin);
    if (scheduler.managedEventSizing) {
      // Timespan height should be at least 1px
      renderData.height = contentHeight;
    }
  }
  /**
   * Gets timespan coordinates etc. Relative to containing row. If the timespan is outside of the zone in
   * which timespans are rendered, that is outside of the TimeAxis, or outside of the vertical zone in which timespans
   * are rendered, then `undefined` is returned.
   * @private
   * @param {Scheduler.model.TimeSpan} timeSpan TimeSpan record
   * @param {Core.data.Model} rowRecord Row record
   * @param {Boolean|Object} includeOutside Specify true to get boxes for timespans outside of the rendered zone in both
   * dimensions. This option is used when calculating dependency lines, and we need to include routes from timespans
   * which may be outside the rendered zone.
   * @param {Boolean} includeOutside.timeAxis Pass as `true` to include timespans outside of the TimeAxis's bounds
   * @param {Boolean} includeOutside.viewport Pass as `true` to include timespans outside of the vertical timespan viewport's bounds.
   * @returns {{event/task: *, left: number, width: number, start: (Date), end: (Date), startMS: number, endMS: number, startsOutsideView: boolean, endsOutsideView: boolean}}
   */
  getTimeSpanRenderData(timeSpan, rowRecord, includeOutside = false) {
    const me = this,
      {
        scheduler
      } = me,
      {
        timeAxis
      } = scheduler,
      includeOutsideTimeAxis = includeOutside === true || includeOutside.timeAxis,
      includeOutsideViewport = includeOutside === true || includeOutside.viewport;
    // If timespan is outside the TimeAxis, give up trying to calculate a layout (Unless we're including timespans
    // outside our zone)
    if (includeOutsideTimeAxis || timeAxis.isTimeSpanInAxis(timeSpan)) {
      const row = scheduler.getRowById(rowRecord);
      if (row || includeOutsideViewport) {
        const data = me.setupRenderData(timeSpan, rowRecord);
        if (!me.fillTimeSpanHorizontalPosition(data)) {
          return null;
        }
        me.fillTimeSpanVerticalPosition(data, rowRecord);
        return data;
      }
    }
  }
  // Layout a set of events, code shared by normal event render path and nested events
  layoutEvents(resourceRecord, allEvents, includeOutside = false, parentEventRecord, eventSorter) {
    const me = this,
      {
        scheduler
      } = me,
      {
        timeAxis
      } = scheduler,
      // Generate layout data
      eventsData = allEvents.reduce((result, eventRecord) => {
        // Only those in time axis (by default)
        if (includeOutside || timeAxis.isTimeSpanInAxis(eventRecord)) {
          const eventBox = scheduler.generateRenderData(eventRecord, resourceRecord, false);
          // Collect layouts of visible events
          if (eventBox) {
            result.push(eventBox);
          }
        }
        return result;
      }, []);
    // Ensure the events are rendered in natural order so that navigation works.
    eventsData.sort(eventSorter ?? me.eventSorter);
    let rowHeight = scheduler.getAppliedResourceHeight(resourceRecord, parentEventRecord);
    const
      // Only events and tasks should be considered during layout (not resource time ranges if any, or events
      // being drag created when configured with lockLayout)
      layoutEventData = eventsData.filter(({
        eventRecord
      }) => eventRecord.isEvent && !eventRecord.meta.excludeFromLayout),
      eventLayout = scheduler.getEventLayout(resourceRecord, parentEventRecord),
      layoutHandler = scheduler.getEventLayoutHandler(eventLayout);
    if (layoutHandler) {
      const {
          barMargin,
          resourceMargin,
          contentHeight
        } = scheduler.getResourceLayoutSettings(resourceRecord, parentEventRecord),
        bandsRequired = layoutHandler.applyLayout(layoutEventData, resourceRecord) || 1;
      if (layoutHandler.type === 'layoutFn') {
        rowHeight = bandsRequired;
      } else {
        rowHeight = bandsRequired * contentHeight + (bandsRequired - 1) * barMargin + resourceMargin * 2;
      }
    }
    // Apply z-index when event elements might overlap, to keep "overlap order" consistent
    else if (layoutEventData.length > 0) {
      for (let i = 0; i < layoutEventData.length; i++) {
        const data = layoutEventData[i];
        // $event-zindex scss var is 5
        data.wrapperStyle += `;z-index:${i + 5}`;
      }
    }
    return {
      rowHeight,
      eventsData
    };
  }
  // Lay out events within a resource, relative to the resource
  layoutResourceEvents(resourceRecord, includeOutside = false) {
    const me = this,
      {
        scheduler
      } = me,
      {
        eventStore,
        assignmentStore,
        timeAxis
      } = scheduler,
      // Events for this resource
      resourceEvents = eventStore.getEvents({
        includeOccurrences: scheduler.enableRecurringEvents,
        resourceRecord,
        startDate: timeAxis.startDate,
        endDate: timeAxis.endDate,
        filter: (assignmentStore.isFiltered || eventStore.isFiltered) && (eventRecord => eventRecord.assignments.some(a => a.resource === resourceRecord.$original && assignmentStore.includes(a)))
      }),
      // Call a chainable template function on scheduler to allow features to add additional "events" to render
      // Currently used by ResourceTimeRanges, CalendarHighlight & NestedEvents
      allEvents = scheduler.getEventsToRender(resourceRecord, resourceEvents) || [];
    return me.layoutEvents(resourceRecord, allEvents, includeOutside);
  }
  // Generates a DOMConfig for an EventRecord
  renderEvent(data, rowHeight) {
    const {
        scheduler
      } = this,
      {
        resourceRecord,
        assignmentRecord,
        eventRecord
      } = data,
      {
        milestoneLayoutMode: layoutMode,
        milestoneTextPosition: textPosition
      } = scheduler,
      // Sync using assignment id for events and event id for ResourceTimeRanges. Add eventId for occurrences to make them unique
      syncId = assignmentRecord
      // Assignment, might be an occurrence
      ? this.assignmentStore.getOccurrence(assignmentRecord, eventRecord).id
      // Something else, probably a ResourceTimeRange
      : data.eventId,
      eventElementConfig = {
        className: data.cls,
        style: data.style || '',
        children: data.children,
        role: 'presentation',
        dataset: {
          // Each feature putting contents in the event wrap should have this to simplify syncing and
          // element retrieval after sync
          taskFeature: 'event'
        },
        syncOptions: {
          syncIdField: 'taskBarFeature'
        }
      },
      // Event element config, applied to existing element or used to create a new one below
      elementConfig = {
        className: data.wrapperCls,
        tabIndex: 'tabIndex' in data ? data.tabIndex : -1,
        children: [eventElementConfig, ...data.wrapperChildren],
        style: {
          top: data.absoluteTop,
          left: data.left,
          // ResourceTimeRanges fill row height, cannot be done earlier than this since row height is not
          // known initially
          height: data.fillSize ? rowHeight : data.height,
          // DomHelper appends px to dimensions when using numbers.
          // Do not ignore width for normal milestones, use height value. It is required to properly center
          // pseudo element with top/bottom labels.
          // Milestone part of layout that contain the label gets a width
          width: eventRecord.isMilestone && !eventRecord.meta.isDragCreating && (layoutMode === 'default' && (textPosition === 'outside' || textPosition === 'inside' && !data.width) || textPosition === 'always-outside') ? data.height : data.width,
          style: data.wrapperStyle,
          fontSize: data.height + 'px'
        },
        dataset: {
          // assignmentId is set in this function conditionally
          resourceId: resourceRecord.id,
          eventId: data.eventId,
          // Not using eventRecord.id to distinguish between Event and ResourceTimeRange
          syncId: resourceRecord.isLinked ? `${syncId}_${resourceRecord.id}` : syncId
        },
        // Will not be part of DOM, but attached to the element
        elementData: data,
        // Dragging etc. flags element as retained, to not reuse/release it during that operation. Events
        // always use assignments, but ResourceTimeRanges does not
        retainElement: (assignmentRecord === null || assignmentRecord === void 0 ? void 0 : assignmentRecord.instanceMeta(scheduler).retainElement) || eventRecord.instanceMeta(scheduler).retainElement,
        // Options for this level of sync, lower levels can have their own
        syncOptions: {
          syncIdField: 'taskFeature',
          // Remove instead of release when a feature is disabled
          releaseThreshold: 0
        }
      };
    // Write back the correct height for elements filling the row, to not derender them later based on wrong height
    if (data.fillSize) {
      data.height = rowHeight;
    }
    // Some browsers throw warnings on zIndex = ''
    if (data.zIndex) {
      elementConfig.zIndex = data.zIndex;
    }
    // Do not want to spam dataset with empty prop when not using assignments (ResourceTimeRanges)
    if (assignmentRecord) {
      elementConfig.dataset.assignmentId = assignmentRecord.id;
    }
    data.elementConfig = elementConfig;
    scheduler.afterRenderEvent({
      renderData: data,
      rowHeight,
      domConfig: elementConfig
    });
    return elementConfig;
  }
  /**
   * Refresh events for resource record (or Row), clearing its cache and forcing DOM refresh.
   * @param {Scheduler.model.ResourceModel} recordOrRow Record or row to refresh
   * @param {Boolean} [force] Specify `false` to prevent clearing cache and forcing DOM refresh
   * @internal
   */
  refreshEventsForResource(recordOrRow, force = true, draw = true) {
    const me = this,
      record = me.scheduler.store.getById(recordOrRow.isRow ? recordOrRow.id : recordOrRow),
      row = me.scheduler.rowManager.getRowFor(record);
    if (force) {
      me.clearResources([record]);
    }
    if (row && record) {
      me.renderer({
        row,
        record
      });
      if (force && draw) {
        me.onRenderDone();
      }
    }
  }
  // Returns layout for the current resource. Used by the renderer and exporter
  getResourceLayout(resourceRecord) {
    const me = this;
    // Use cached layout if available
    let resourceLayout = me.resourceMap.get(resourceRecord.id);
    if (!resourceLayout || resourceLayout.invalid) {
      // Previously we would bail out here if engine wasn't ready. Now we instead allow drawing in most cases,
      // since data can be read and written during commit (previously it could not)
      if (me.suspended) {
        return;
      }
      resourceLayout = me.layoutResourceEvents(resourceRecord, false);
      me.resourceMap.set(resourceRecord.id, resourceLayout);
    }
    return resourceLayout;
  }
  getEventDOMConfigForCurrentView(resourceLayout, row, left, right) {
    const me = this,
      {
        bufferSize,
        scheduler
      } = me,
      {
        labels,
        eventBuffer
      } = scheduler.features,
      // Left/right labels and event buffer elements require using a buffer to not derender too early
      usesLabels = (eventBuffer === null || eventBuffer === void 0 ? void 0 : eventBuffer.enabled) || (labels === null || labels === void 0 ? void 0 : labels.enabled) && (labels.left || labels.right || labels.before || labels.after),
      {
        eventsData
      } = resourceLayout,
      // When scrolling, layout will be reused and any events that are still in view can reuse their DOM configs
      reusableDOMConfigs = me.fromScroll ? me.rowMap.get(row) : null,
      eventDOMConfigs = [];
    let useLeft, useRight;
    // Only collect configs for those actually in view
    for (let i = 0; i < eventsData.length; i++) {
      const layout = eventsData[i];
      useLeft = left;
      useRight = right;
      // Labels/milestones requires keeping events rendered longer
      if (usesLabels || layout.width === 0) {
        useLeft -= bufferSize;
        useRight += bufferSize;
      }
      if (layout.left + layout.width >= useLeft && layout.left <= useRight) {
        layout.absoluteTop = layout.top + row.top;
        const prevDomConfig = reusableDOMConfigs === null || reusableDOMConfigs === void 0 ? void 0 : reusableDOMConfigs.find(config => config.elementData.eventId === layout.eventId && config.elementData.resourceId === layout.resourceId);
        eventDOMConfigs.push(prevDomConfig ?? me.renderEvent(layout, resourceLayout.rowHeight));
      }
    }
    return eventDOMConfigs;
  }
  // Called per row in "view", collect configs
  renderer({
    row,
    record: resourceRecord,
    size = {}
  }) {
    const me = this;
    // Bail out for group headers/footers
    if (resourceRecord.isSpecialRow) {
      // Clear any cached layout for row retooled to special row, and bail out
      me.rowMap.delete(row);
      return;
    }
    const {
        left,
        right
      } = me.viewportCoords,
      resourceLayout = me.getResourceLayout(resourceRecord);
    // Layout is suspended
    if (!resourceLayout) {
      return;
    }
    // Size row to fit events
    size.height = resourceLayout.rowHeight;
    // Avoid storing our calculated height as the rows max height, to not affect next round of calculations
    size.transient = true;
    const eventDOMConfigs = me.getEventDOMConfigForCurrentView(resourceLayout, row, left, right);
    me.rowMap.set(row, eventDOMConfigs);
    // Keep track if we need to draw on vertical scroll or not, to not get multiple onRenderDone() calls
    me.rendererCalled = true;
  }
  // Called when the current row rendering "pass" is complete, sync collected configs to DOM
  onRenderDone() {
    const {
        scheduler,
        rowMap,
        verticalBufferSize
      } = this,
      visibleEventDOMConfigs = [],
      bodyTop = scheduler._scrollTop ?? 0,
      viewTop = bodyTop - verticalBufferSize,
      viewBottom = bodyTop + scheduler._bodyRectangle.height + verticalBufferSize,
      unbuffered = verticalBufferSize < 0,
      unmanagedSize = !scheduler.managedEventSizing;
    // Event configs are collected when rows are rendered, but we do not want to waste resources on rendering
    // events far out of view. Especially with many events per row giving large row heights, rows in the RowManagers
    // buffer might far away -> collect events for rows within viewport + small vertical buffer
    rowMap.forEach((eventDOMConfigs, row) => {
      // Render events "in view". Export specifies a negative verticalBufferSize to disable it
      if (unbuffered || row.bottom > viewTop && row.top < viewBottom) {
        for (let i = 0; i < eventDOMConfigs.length; i++) {
          const config = eventDOMConfigs[i],
            data = config.elementData,
            {
              absoluteTop,
              eventRecord
            } = data;
          // Conditions under which event bars are included in the DOM:
          //   If bufferSize is -ve, meaning render all events.
          //   scheduler.managedEventSizing is false.
          //   The event is beig drag-created or drag-resized
          //   The event is within the bounds of the rendered region.
          if (unbuffered || unmanagedSize || eventRecord.meta.isDragCreating || eventRecord.meta.isResizing || absoluteTop + data.height > viewTop && absoluteTop < viewBottom) {
            visibleEventDOMConfigs.push(config);
          }
        }
      }
      // We are using cached DomConfigs. When DomSync releases an element, it also flags the config as released.
      // Next time we pass it that very same config, it says it is released and nothing shows up.
      //
      // We are breaching the DomSync contract a bit with the cached approach. DomSync expects new configs on each
      // call, so to facilitate that we clone the configs shallowly (nothing deep is affected by sync releasing).
      // That way we can always pass it fresh unreleased configs.
      for (let i = 0; i < eventDOMConfigs.length; i++) {
        eventDOMConfigs[i] = {
          ...eventDOMConfigs[i]
        };
      }
    });
    this.fromScroll = false;
    this.visibleEventDOMConfigs = visibleEventDOMConfigs;
    DomSync.sync({
      domConfig: {
        onlyChildren: true,
        children: visibleEventDOMConfigs
      },
      targetElement: scheduler.foregroundCanvas,
      syncIdField: 'syncId',
      // Called by DomSync when it creates, releases or reuses elements
      callback({
        action,
        domConfig,
        lastDomConfig,
        targetElement,
        jsx
      }) {
        var _scheduler$processEve, _domConfig$elementDat;
        const {
            reactComponent
          } = scheduler,
          // Some actions are considered first a release and then a render (reusing another element).
          // This gives clients code a chance to clean up before reusing an element
          isRelease = releaseEventActions$1[action],
          isRender = renderEventActions$1[action];
        !isRelease && ((_scheduler$processEve = scheduler.processEventContent) === null || _scheduler$processEve === void 0 ? void 0 : _scheduler$processEve.call(scheduler, {
          jsx,
          action,
          domConfig,
          targetElement,
          isRelease,
          reactComponent
        }));
        if (action === 'none' || !(domConfig !== null && domConfig !== void 0 && (_domConfig$elementDat = domConfig.elementData) !== null && _domConfig$elementDat !== void 0 && _domConfig$elementDat.isWrap)) {
          return;
        }
        // Trigger release for events (it might be a proxy element, skip those)
        if (isRelease && lastDomConfig !== null && lastDomConfig !== void 0 && lastDomConfig.elementData) {
          var _scheduler$processEve2;
          const {
              eventRecord,
              resourceRecord,
              assignmentRecord
            } = lastDomConfig.elementData,
            event = {
              renderData: lastDomConfig.elementData,
              element: targetElement,
              eventRecord,
              resourceRecord,
              assignmentRecord
            };
          // Process event necessary in the case of release
          (_scheduler$processEve2 = scheduler.processEventContent) === null || _scheduler$processEve2 === void 0 ? void 0 : _scheduler$processEve2.call(scheduler, {
            isRelease,
            targetElement,
            reactComponent,
            assignmentRecord
          });
          // Some browsers do not blur on set to display:none, so releasing the active element
          // must *explicitly* move focus outwards to the view.
          if (targetElement === DomHelper.getActiveElement(targetElement)) {
            scheduler.focusElement.focus();
          }
          // This event is documented on Scheduler
          scheduler.trigger('releaseEvent', event);
        }
        if (isRender) {
          const {
              eventRecord,
              resourceRecord,
              assignmentRecord
            } = domConfig.elementData,
            event = {
              renderData: domConfig.elementData,
              element: targetElement,
              isReusingElement: action === 'reuseElement',
              isRepaint: action === 'reuseOwnElement',
              eventRecord,
              resourceRecord,
              assignmentRecord
            };
          // Prevent transitions when reusing some other events element
          if (action === 'reuseElement' && scheduler.isAnimating) {
            DomHelper.addTemporaryClass(targetElement, 'b-reusing-own', 50, scheduler);
          }
          // This event is documented on Scheduler
          scheduler.trigger('renderEvent', event);
        }
      }
    });
  }
  //endregion
  //region Cache
  // Clears cached resource layout
  clearResources(recordsOrIds) {
    recordsOrIds = ArrayHelper.asArray(recordsOrIds);
    const resourceIds = recordsOrIds.map(Model.asId);
    resourceIds.forEach(resourceId => {
      // Invalidate resourceLayout, keeping it around in case we need it before next refresh
      const cached = this.resourceMap.get(resourceId);
      if (cached) {
        cached.invalid = true;
      }
      const row = this.scheduler.getRowById(resourceId);
      row && this.rowMap.delete(row);
    });
  }
  clearAll({
    clearDom = false,
    clearLayoutCache = false
  } = {}) {
    const me = this,
      {
        layouts,
        foregroundCanvas
      } = me.scheduler;
    if (clearLayoutCache && layouts) {
      for (const layout in layouts) {
        layouts[layout].clearCache();
      }
    }
    // it seems `foregroundCanvas` can be missing at this point
    // for example if scheduler instance is created w/o of `appendTo` config
    if (foregroundCanvas && clearDom) {
      // Start from scratch when replacing the project, to not retain anything in maps or released elements
      foregroundCanvas.syncIdMap = foregroundCanvas.lastDomConfig = null;
      for (const child of foregroundCanvas.children) {
        child.lastDomConfig = child.elementData = null;
      }
    }
    me.resourceMap.clear();
    me.rowMap.clear();
  }
  //endregion
}

HorizontalRendering._$name = 'HorizontalRendering';

/**
 * @module Scheduler/eventlayout/VerticalLayout
 */
/**
 * Assists with event layout in vertical mode, handles `eventLayout: none|pack|mixed`
 * @private
 * @mixes Scheduler/eventlayout/PackMixin
 */
class VerticalLayout extends PackMixin() {
  static get defaultConfig() {
    return {
      coordProp: 'leftFactor',
      sizeProp: 'widthFactor'
    };
  }
  // Try to pack the events to consume as little space as possible
  applyLayout(events, columnWidth, resourceMargin, barMargin, columnIndex, eventLayout) {
    const me = this,
      layoutType = eventLayout.type;
    return me.packEventsInBands(events, (tplData, clusterIndex, slot, slotSize) => {
      // Stretch events to fill available width
      if (layoutType === 'none') {
        tplData.width = columnWidth - resourceMargin * 2;
        tplData.left += resourceMargin;
      } else {
        // Fractions of resource column
        tplData.widthFactor = slotSize;
        const leftFactor = tplData.leftFactor = slot.start + clusterIndex * slotSize,
          // Number of "columns" in the current slot
          packColumnCount = Math.round(1 / slotSize),
          // Index among those columns for current event
          packColumnIndex = leftFactor / slotSize,
          // Width with all bar margins subtracted
          availableWidth = columnWidth - resourceMargin * 2 - barMargin * (packColumnCount - 1);
        // Allowing two events to overlap? Slightly offset the second
        if (layoutType === 'mixed' && packColumnCount === 2) {
          tplData.left += leftFactor * columnWidth / 5 + barMargin;
          tplData.width = columnWidth - leftFactor * columnWidth / 5 - barMargin * 2;
          tplData.zIndex = 5 + packColumnIndex;
        }
        // Pack by default
        else {
          // Fractional width
          tplData.width = slotSize * availableWidth;
          // Translate to absolute position
          tplData.left += leftFactor * availableWidth + resourceMargin + barMargin * packColumnIndex;
        }
      }
      tplData.cls['b-sch-event-narrow'] = tplData.width < me.scheduler.narrowEventWidth;
    });
  }
}
VerticalLayout._$name = 'VerticalLayout';

/**
 * @module Scheduler/view/orientation/VerticalRendering
 */
const releaseEventActions = {
    releaseElement: 1,
    // Not used at all at the moment
    reuseElement: 1 // Used by some other element
  },
  renderEventActions = {
    newElement: 1,
    reuseOwnElement: 1,
    reuseElement: 1
  },
  chronoFields = {
    startDate: 1,
    endDate: 1,
    duration: 1
  },
  emptyObject = Object.freeze({});
/**
 * Handles event rendering in Schedulers vertical mode. Reacts to project/store changes to keep the UI up to date.
 *
 * @internal
 */
class VerticalRendering extends Base.mixin(Delayable, AttachToProjectMixin) {
  //region Config & Init
  static get properties() {
    return {
      eventMap: new Map(),
      resourceMap: new Map(),
      releasedElements: {},
      toDrawOnProjectRefresh: new Set(),
      resourceBufferSize: 1
    };
  }
  construct(scheduler) {
    this.client = this.scheduler = scheduler;
    this.verticalLayout = new VerticalLayout({
      scheduler
    });
    super.construct({});
  }
  init() {
    const me = this,
      {
        scheduler,
        resourceColumns
      } = me;
    // Resource header/columns
    resourceColumns.resourceStore = me.resourceStore;
    resourceColumns.ion({
      name: 'resourceColumns',
      columnWidthChange: 'onResourceColumnWidthChange',
      thisObj: me
    });
    me.initialized = true;
    if (scheduler.isPainted) {
      me.renderer();
    }
    resourceColumns.availableWidth = scheduler.timeAxisSubGridElement.offsetWidth;
  }
  //endregion
  //region Elements <-> Records
  resolveRowRecord(elementOrEvent, xy) {
    const me = this,
      {
        scheduler
      } = me,
      event = elementOrEvent.nodeType ? null : elementOrEvent,
      element = event ? event.target : elementOrEvent,
      coords = event ? [event.borderOffsetX, event.borderOffsetY] : xy,
      // Fix for FF on Linux having text nodes as event.target
      el = element.nodeType === Element.TEXT_NODE ? element.parentElement : element,
      eventElement = el.closest(scheduler.eventSelector);
    if (eventElement) {
      return scheduler.resourceStore.getById(eventElement.dataset.resourceId);
    }
    // Need to be inside schedule at least
    if (!element.closest('.b-sch-timeaxis-cell')) {
      return null;
    }
    if (!coords) {
      throw new Error(`Vertical mode needs coordinates to resolve this element. Can also be called with a browser
                event instead of element to extract element and coordinates from`);
    }
    if (scheduler.variableColumnWidths || scheduler.resourceStore.isGrouped) {
      let totalWidth = 0;
      for (const col of me.resourceStore) {
        if (!col.isSpecialRow) {
          totalWidth += col.columnWidth || me.resourceColumns.columnWidth;
        }
        if (totalWidth >= coords[0]) {
          return col;
        }
      }
      return null;
    }
    const index = Math.floor(coords[0] / me.resourceColumns.columnWidth);
    return me.allResourceRecords[index];
  }
  toggleCls(assignmentRecord, cls, add = true, useWrapper = false) {
    var _this$eventMap$get;
    const eventData = (_this$eventMap$get = this.eventMap.get(assignmentRecord.eventId)) === null || _this$eventMap$get === void 0 ? void 0 : _this$eventMap$get[assignmentRecord.resourceId];
    if (eventData) {
      eventData.renderData[useWrapper ? 'wrapperCls' : 'cls'][cls] = add;
      // Element from the map cannot be trusted, might be reused in which case map is not updated to reflect that.
      // To be safe, retrieve using `getElementFromAssignmentRecord`
      const element = this.client.getElementFromAssignmentRecord(assignmentRecord, useWrapper);
      if (element) {
        element.classList[add ? 'add' : 'remove'](cls);
      }
    }
  }
  //endregion
  //region Coordinate <-> Date
  getDateFromXY(xy, roundingMethod, local, allowOutOfRange = false) {
    let coord = xy[1];
    if (!local) {
      coord = this.translateToScheduleCoordinate(coord);
    }
    return this.scheduler.timeAxisViewModel.getDateFromPosition(coord, roundingMethod, allowOutOfRange);
  }
  translateToScheduleCoordinate(y) {
    return y - this.scheduler.timeAxisSubGridElement.getBoundingClientRect().top - globalThis.scrollY;
  }
  translateToPageCoordinate(y) {
    return y + this.scheduler.timeAxisSubGridElement.getBoundingClientRect().top + globalThis.scrollY;
  }
  //endregion
  //region Regions
  getResourceEventBox(event, resource) {
    var _this$eventMap$get2;
    const eventId = event.id,
      resourceId = resource.id;
    let {
      renderData
    } = ((_this$eventMap$get2 = this.eventMap.get(eventId)) === null || _this$eventMap$get2 === void 0 ? void 0 : _this$eventMap$get2[resourceId]) || emptyObject;
    if (!renderData) {
      var _this$eventMap$get3, _this$eventMap$get3$r;
      // Never been in view, lay it out
      this.layoutResourceEvents(this.scheduler.resourceStore.getById(resourceId));
      // Have another go at getting the layout data
      renderData = (_this$eventMap$get3 = this.eventMap.get(eventId)) === null || _this$eventMap$get3 === void 0 ? void 0 : (_this$eventMap$get3$r = _this$eventMap$get3[resourceId]) === null || _this$eventMap$get3$r === void 0 ? void 0 : _this$eventMap$get3$r.renderData;
    }
    return renderData ? new Rectangle(renderData.left, renderData.top, renderData.width, renderData.bottom - renderData.top) : null;
  }
  getScheduleRegion(resourceRecord, eventRecord, local) {
    var _scheduler$getDateCon;
    const me = this,
      {
        scheduler
      } = me,
      // Only interested in width / height (in "local" coordinates)
      region = Rectangle.from(scheduler.timeAxisSubGridElement, scheduler.timeAxisSubGridElement);
    if (resourceRecord) {
      region.left = me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth;
      region.right = region.left + scheduler.resourceColumnWidth;
    }
    const start = scheduler.timeAxis.startDate,
      end = scheduler.timeAxis.endDate,
      dateConstraints = ((_scheduler$getDateCon = scheduler.getDateConstraints) === null || _scheduler$getDateCon === void 0 ? void 0 : _scheduler$getDateCon.call(scheduler, resourceRecord, eventRecord)) || {
        start,
        end
      },
      startY = scheduler.getCoordinateFromDate(DateHelper.max(start, dateConstraints.start)),
      endY = scheduler.getCoordinateFromDate(DateHelper.min(end, dateConstraints.end));
    if (!local) {
      region.top = me.translateToPageCoordinate(startY);
      region.bottom = me.translateToPageCoordinate(endY);
    } else {
      region.top = startY;
      region.bottom = endY;
    }
    return region;
  }
  getRowRegion(resourceRecord, startDate, endDate) {
    const me = this,
      {
        scheduler
      } = me,
      x = me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth,
      taStart = scheduler.timeAxis.startDate,
      taEnd = scheduler.timeAxis.endDate,
      start = startDate ? DateHelper.max(taStart, startDate) : taStart,
      end = endDate ? DateHelper.min(taEnd, endDate) : taEnd,
      startY = scheduler.getCoordinateFromDate(start),
      endY = scheduler.getCoordinateFromDate(end, true, true),
      y = Math.min(startY, endY),
      height = Math.abs(startY - endY);
    return new Rectangle(x, y, scheduler.resourceColumnWidth, height);
  }
  get visibleDateRange() {
    const scheduler = this.scheduler,
      scrollPos = scheduler.scrollable.y,
      height = scheduler.scrollable.clientHeight,
      startDate = scheduler.getDateFromCoordinate(scrollPos) || scheduler.timeAxis.startDate,
      endDate = scheduler.getDateFromCoordinate(scrollPos + height) || scheduler.timeAxis.endDate;
    return {
      startDate,
      endDate,
      startMS: startDate.getTime(),
      endMS: endDate.getTime()
    };
  }
  //endregion
  //region Events
  // Column width changed, rerender fully
  onResourceColumnWidthChange({
    width,
    oldWidth
  }) {
    const me = this,
      {
        scheduler
      } = me;
    // Fix width of column & header
    me.resourceColumns.width = scheduler.timeAxisColumn.width = me.allResourceRecords.length * width;
    me.clearAll();
    // Only transition large changes, otherwise it is janky when dragging slider in demo
    me.refresh(Math.abs(width - oldWidth) > 30);
    // Not detected by resizeobserver? Need to call this for virtual scrolling to react to update
    //        scheduler.callEachSubGrid('refreshFakeScroll');
    //        scheduler.refreshVirtualScrollbars();
  }
  //endregion
  //region Project
  attachToProject(project) {
    super.attachToProject(project);
    if (project) {
      project.ion({
        name: 'project',
        refresh: 'onProjectRefresh',
        thisObj: this
      });
    }
  }
  onProjectRefresh() {
    const me = this,
      {
        scheduler,
        toDrawOnProjectRefresh
      } = me;
    // Only update the UI immediately if we are visible
    if (scheduler.isVisible) {
      if (scheduler.rendered && !scheduler.refreshSuspended) {
        // Either refresh all rows (on for example dataset)
        if (me.refreshAllWhenReady) {
          me.clearAll();
          //scheduler.refreshWithTransition();
          me.refresh();
          me.refreshAllWhenReady = false;
        }
        // Or only affected rows (if any)
        else if (toDrawOnProjectRefresh.size) {
          me.refresh();
        }
        toDrawOnProjectRefresh.clear();
      }
    }
    // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
    else {
      scheduler.whenVisible('refresh', scheduler, [true]);
    }
  }
  //endregion
  //region EventStore
  attachToEventStore(eventStore) {
    super.attachToEventStore(eventStore);
    this.refreshAllWhenReady = true;
    if (eventStore) {
      eventStore.ion({
        name: 'eventStore',
        addConfirmed: 'onEventStoreAddConfirmed',
        refreshPreCommit: 'onEventStoreRefresh',
        thisObj: this
      });
    }
  }
  onEventStoreAddConfirmed({
    record
  }) {
    for (const element of this.client.getElementsFromEventRecord(record)) {
      element.classList.remove('b-iscreating');
    }
  }
  onEventStoreRefresh({
    action
  }) {
    if (action === 'batch') {
      this.refreshAllWhenReady = true;
    }
  }
  onEventStoreChange({
    action,
    records: eventRecords = [],
    record,
    replaced,
    changes,
    isAssign
  }) {
    const me = this,
      resourceIds = new Set();
    eventRecords.forEach(eventRecord => {
      var _eventRecord$$linkedR;
      // Update all resource rows to which this event is assigned *if* the resourceStore
      // contains that resource (We could have filtered the resourceStore)
      const renderedEventResources = (_eventRecord$$linkedR = eventRecord.$linkedResources) === null || _eventRecord$$linkedR === void 0 ? void 0 : _eventRecord$$linkedR.filter(r => me.resourceStore.includes(r));
      renderedEventResources === null || renderedEventResources === void 0 ? void 0 : renderedEventResources.forEach(resourceRecord => resourceIds.add(resourceRecord.id));
    });
    switch (action) {
      // No-ops
      case 'sort': // Order in EventStore does not matter, so these actions are no-ops
      case 'group':
      case 'move':
      case 'remove':
        // Remove is a no-op since assignment will also be removed
        return;
      case 'dataset':
        me.refreshAllResourcesWhenReady();
        return;
      case 'add':
      case 'updateMultiple':
        // Just refresh below
        break;
      case 'replace':
        // Gather resources from both the old record and the new one
        replaced.forEach(([, newEvent]) => {
          // Old cleared by changed assignment
          newEvent.resources.map(resourceRecord => resourceIds.add(resourceRecord.id));
        });
        // And clear them
        me.clearResources(resourceIds);
        break;
      case 'removeall':
      case 'filter':
        // Clear all when filtering for simplicity. If that turns out to give bad performance, one would need to
        // figure out which events was filtered out and only clear their resources.
        me.clearAll();
        me.refresh();
        return;
      case 'update':
        {
          // Check if changes are graph related or not
          const allChrono = record.$entity ? !Object.keys(changes).some(name => !record.$entity.getField(name)) : !Object.keys(changes).some(name => !chronoFields[name]);
          // If any one of these in changes, it will affect visuals
          let changeCount = 0;
          if ('startDate' in changes) changeCount++;
          if ('endDate' in changes) changeCount++;
          if ('duration' in changes) changeCount++;
          // Always redraw non chrono changes (name etc)
          if (!allChrono || changeCount || 'percentDone' in changes || 'inactive' in changes || 'segments' in changes) {
            if (me.shouldWaitForInitializeAndEngineReady) {
              me.refreshResourcesWhenReady(resourceIds);
            } else {
              me.clearResources(resourceIds);
              me.refresh();
            }
          }
          return;
        }
    }
    me.refreshResourcesWhenReady(resourceIds);
  }
  //endregion
  //region ResourceStore
  attachToResourceStore(resourceStore) {
    const me = this;
    super.attachToResourceStore(resourceStore);
    me.refreshAllWhenReady = true;
    if (me.resourceColumns) {
      me.resourceColumns.resourceStore = resourceStore;
    }
    resourceStore.ion({
      name: 'resourceStore',
      changePreCommit: 'onResourceStoreChange',
      refreshPreCommit: 'onResourceStoreRefresh',
      // In vertical, resource store is not the row store but should toggle the load mask
      load: () => me.scheduler.unmaskBody(),
      thisObj: me,
      prio: 1 // Call before others to clear cache before redraw
    });

    if (me.initialized && me.scheduler.isPainted) {
      // Invalidate resource range and events
      me.firstResource = me.lastResource = null;
      me.clearAll();
      me.renderer();
    }
  }
  onResourceStoreChange({
    source: resourceStore,
    action,
    records = [],
    record,
    replaced,
    changes
  }) {
    const me = this,
      // records for add, record for update, replaced [[old, new]] for replace
      resourceRecords = replaced ? replaced.map(r => r[1]) : records,
      resourceIds = new Set(resourceRecords.map(resourceRecord => resourceRecord.id));
    // Invalidate resource range
    me.firstResource = me.lastResource = null;
    resourceStore._allResourceRecords = null;
    const {
      allResourceRecords
    } = resourceStore;
    // Operation that did not invalidate engine, refresh directly
    if (me.scheduler.isEngineReady) {
      switch (action) {
        case 'update':
          if (changes !== null && changes !== void 0 && changes.id) {
            me.clearResources([changes.id.oldValue, changes.id.value]);
          } else {
            me.clearResources([record.id]);
          }
          // Only the invalidation above needed
          break;
        case 'filter':
          // All filtered out resources needs clearing and so does those not filtered out since they might have
          // moved horizontally when others hide
          me.clearAll();
          break;
      }
      // Changing a column width means columns after that will have to be recalculated
      // so clear all cached layouts.
      if (changes && 'columnWidth' in changes) {
        me.clearAll();
      }
      me.refresh(true);
    }
    // Operation that did invalidate project, update on project refresh
    else {
      switch (action) {
        case 'dataset':
        case 'remove': // Cannot tell from which index it was removed
        case 'removeall':
          me.refreshAllResourcesWhenReady();
          return;
        case 'replace':
        case 'add':
          {
            if (!resourceStore.isGrouped) {
              // Make sure all existing events following added resources are offset correctly
              const firstIndex = resourceRecords.reduce((index, record) => Math.min(index, allResourceRecords.indexOf(record)), allResourceRecords.length);
              for (let i = firstIndex; i < allResourceRecords.length; i++) {
                resourceIds.add(allResourceRecords[i].id);
              }
            }
          }
      }
      me.refreshResourcesWhenReady(resourceIds);
    }
  }
  onResourceStoreRefresh({
    action
  }) {
    const me = this;
    if (action === 'sort' || action === 'group') {
      // Invalidate resource range & cache
      me.firstResource = me.lastResource = me.resourceStore._allResourceRecords = null;
      me.clearAll();
      me.refresh();
    }
  }
  //endregion
  //region AssignmentStore
  attachToAssignmentStore(assignmentStore) {
    super.attachToAssignmentStore(assignmentStore);
    this.refreshAllWhenReady = true;
    if (assignmentStore) {
      assignmentStore.ion({
        name: 'assignmentStore',
        changePreCommit: 'onAssignmentStoreChange',
        refreshPreCommit: 'onAssignmentStoreRefresh',
        thisObj: this
      });
    }
  }
  onAssignmentStoreChange({
    action,
    records: assignmentRecords = [],
    replaced,
    changes
  }) {
    const me = this,
      resourceIds = new Set(assignmentRecords.map(assignmentRecord => assignmentRecord.resourceId));
    // Operation that did not invalidate engine, refresh directly
    if (me.scheduler.isEngineReady) {
      switch (action) {
        case 'remove':
          me.clearResources(resourceIds);
          break;
        case 'filter':
          me.clearAll();
          break;
        case 'update':
          {
            // When reassigning, clear old resource also
            if ('resourceId' in changes) {
              resourceIds.add(changes.resourceId.oldValue);
            }
            // Ignore engine resolving resourceId -> resource, eventId -> event
            if (!Object.keys(changes).filter(field => field !== 'resource' && field !== 'event').length) {
              return;
            }
            me.clearResources(resourceIds);
          }
      }
      me.refresh(true);
    }
    // Operation that did invalidate project, update on project refresh
    else {
      if (changes && 'resourceId' in changes) {
        resourceIds.add(changes.resourceId.oldValue);
      }
      switch (action) {
        case 'removeall':
          me.refreshAllResourcesWhenReady();
          return;
        case 'replace':
          // Gather resources from both the old record and the new one
          replaced.forEach(([oldAssignment, newAssignment]) => {
            resourceIds.add(oldAssignment.resourceId);
            resourceIds.add(newAssignment.resourceId);
          });
      }
      me.refreshResourcesWhenReady(resourceIds);
    }
  }
  onAssignmentStoreRefresh({
    action,
    records
  }) {
    if (action === 'batch') {
      this.clearAll();
      this.refreshAllResourcesWhenReady();
    }
  }
  //endregion
  //region View hooks
  refreshRows(reLayoutEvents) {
    if (reLayoutEvents) {
      this.clearAll();
      this.scheduler.refreshFromRerender = false;
    }
  }
  // Called from SchedulerEventRendering
  repaintEventsForResource(resourceRecord) {
    this.renderResource(resourceRecord);
  }
  updateFromHorizontalScroll(scrollX) {
    if (scrollX !== this.prevScrollX) {
      this.renderer();
      this.prevScrollX = scrollX;
    }
  }
  updateFromVerticalScroll() {
    this.renderer();
  }
  scrollResourceIntoView(resourceRecord, options) {
    const {
        scheduler
      } = this,
      x = this.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth;
    return scheduler.scrollHorizontallyTo(x, options);
  }
  get allResourceRecords() {
    return this.scheduler.resourceStore.allResourceRecords;
  }
  // Called when viewport size changes
  onViewportResize(width) {
    this.resourceColumns.availableWidth = width;
    this.renderer();
  }
  get resourceColumns() {
    var _this$scheduler$timeA;
    return (_this$scheduler$timeA = this.scheduler.timeAxisColumn) === null || _this$scheduler$timeA === void 0 ? void 0 : _this$scheduler$timeA.resourceColumns;
  }
  // Clear events in case they use date as part of displayed info
  onLocaleChange() {
    this.clearAll();
  }
  // No need to do anything special
  onDragAbort() {}
  onBeforeRowHeightChange() {}
  onTimeAxisViewModelUpdate() {}
  updateElementId() {}
  releaseTimeSpanDiv() {}
  //endregion
  //region Dependency connectors
  /**
   * Gets displaying item start side
   *
   * @param {Scheduler.model.EventModel} eventRecord
   * @returns {'top'|'left'|'bottom'|'right'} 'left' / 'right' / 'top' / 'bottom'
   */
  getConnectorStartSide(eventRecord) {
    return 'top';
  }
  /**
   * Gets displaying item end side
   *
   * @param {Scheduler.model.EventModel} eventRecord
   * @returns {'top'|'left'|'bottom'|'right'} 'left' / 'right' / 'top' / 'bottom'
   */
  getConnectorEndSide(eventRecord) {
    return 'bottom';
  }
  //endregion
  //region Refresh resources
  /**
   * Clears resources directly and redraws them on next project refresh
   * @param {Number[]|String[]} resourceIds
   * @private
   */
  refreshResourcesWhenReady(resourceIds) {
    this.clearResources(resourceIds);
    resourceIds.forEach(id => this.toDrawOnProjectRefresh.add(id));
  }
  /**
   * Clears all resources directly and redraws them on next project refresh
   * @private
   */
  refreshAllResourcesWhenReady() {
    this.clearAll();
    this.refreshAllWhenReady = true;
  }
  //region Rendering
  // Resources in view + buffer
  get resourceRange() {
    return this.getResourceRange(true);
  }
  // Resources strictly in view
  get visibleResources() {
    const {
      first,
      last
    } = this.getResourceRange();
    return {
      first: this.allResourceRecords[first],
      last: this.allResourceRecords[last]
    };
  }
  getResourceRange(withBuffer) {
    const {
        scheduler,
        resourceStore
      } = this,
      {
        resourceColumnWidth,
        scrollX
      } = scheduler,
      {
        scrollWidth
      } = scheduler.timeAxisSubGrid.scrollable,
      resourceBufferSize = withBuffer ? this.resourceBufferSize : 0,
      viewportStart = scrollX - resourceBufferSize,
      viewportEnd = scrollX + scrollWidth + resourceBufferSize;
    if (!(resourceStore !== null && resourceStore !== void 0 && resourceStore.count)) {
      return {
        first: -1,
        last: -1
      };
    }
    // Some resources define their own width
    if (scheduler.variableColumnWidths) {
      let first,
        last = 0,
        start,
        end = 0;
      this.allResourceRecords.forEach((resource, i) => {
        resource.instanceMeta(scheduler).insetStart = start = end;
        end = start + resource.columnWidth;
        if (start > viewportEnd) {
          return false;
        }
        if (end > viewportStart && first == null) {
          first = i;
        } else if (start < viewportEnd) {
          last = i;
        }
      });
      return {
        first,
        last
      };
    }
    // We are using fixed column widths
    else {
      return {
        first: Math.max(Math.floor(scrollX / resourceColumnWidth) - resourceBufferSize, 0),
        last: Math.min(Math.floor((scrollX + scheduler.timeAxisSubGrid.width) / resourceColumnWidth) + resourceBufferSize, this.allResourceRecords.length - 1)
      };
    }
  }
  // Dates in view + buffer
  get dateRange() {
    const {
      scheduler
    } = this;
    let bottomDate = scheduler.getDateFromCoordinate(Math.min(scheduler.scrollTop + scheduler.bodyHeight + scheduler.tickSize - 1, (scheduler.virtualScrollHeight || scheduler.scrollable.scrollHeight) - 1));
    // Might end up below time axis (out of ticks)
    if (!bottomDate) {
      bottomDate = scheduler.timeAxis.last.endDate;
    }
    let topDate = scheduler.getDateFromCoordinate(Math.max(scheduler.scrollTop - scheduler.tickSize, 0));
    // Might end up above time axis when reconfiguring (since this happens as part of rendering)
    if (!topDate) {
      topDate = scheduler.timeAxis.first.startDate;
      bottomDate = scheduler.getDateFromCoordinate(scheduler.bodyHeight + scheduler.tickSize - 1);
    }
    return {
      topDate,
      bottomDate
    };
  }
  getTimeSpanRenderData(eventRecord, resourceRecord, includeOutside = false) {
    var _scheduler$features$e;
    const me = this,
      {
        scheduler
      } = me,
      {
        preamble,
        postamble
      } = eventRecord,
      {
        variableColumnWidths
      } = scheduler,
      useEventBuffer = ((_scheduler$features$e = scheduler.features.eventBuffer) === null || _scheduler$features$e === void 0 ? void 0 : _scheduler$features$e.enabled) && me.isProVerticalRendering && (preamble || postamble) && !eventRecord.isMilestone,
      startDateField = useEventBuffer ? 'wrapStartDate' : 'startDate',
      endDateField = useEventBuffer ? 'wrapEndDate' : 'endDate',
      // Must use Model.get in order to get latest values in case we are inside a batch.
      // EventResize changes the endDate using batching to enable a tentative change
      // via the batchedUpdate event which is triggered when changing a field in a batch.
      // Fall back to accessor if propagation has not populated date fields.
      startDate = eventRecord.isBatchUpdating && eventRecord.hasBatchedChange(startDateField) && !useEventBuffer ? eventRecord.get(startDateField) : eventRecord[startDateField],
      endDate = eventRecord.isBatchUpdating && eventRecord.hasBatchedChange(endDateField) && !useEventBuffer ? eventRecord.get(endDateField) : eventRecord[endDateField],
      resourceMargin = scheduler.getResourceMargin(resourceRecord),
      top = scheduler.getCoordinateFromDate(startDate),
      instanceMeta = resourceRecord.instanceMeta(scheduler),
      // Preliminary values for left & width, used for proxy. Will be changed on layout.
      // The property "left" is utilized based on Scheduler's rtl setting.
      // If RTL, then it's used as the "right" style position.
      left = variableColumnWidths ? instanceMeta.insetStart : me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth,
      resourceWidth = scheduler.getResourceWidth(resourceRecord),
      width = resourceWidth - resourceMargin * 2,
      startDateMS = startDate.getTime(),
      endDateMS = endDate.getTime();
    let bottom = scheduler.getCoordinateFromDate(endDate),
      height = bottom - top;
    // Below, estimate height
    if (bottom === -1) {
      height = Math.round((endDateMS - startDateMS) * scheduler.timeAxisViewModel.getSingleUnitInPixels('millisecond'));
      bottom = top + height;
    }
    return {
      eventRecord,
      resourceRecord,
      left,
      top,
      bottom,
      resourceWidth,
      width,
      height,
      startDate,
      endDate,
      startDateMS,
      endDateMS,
      useEventBuffer,
      children: [],
      start: startDate,
      end: endDate,
      startMS: startDateMS,
      endMS: endDateMS
    };
  }
  // Earlier start dates are above later tasks
  // If same start date, longer tasks float to top
  // If same start + duration, sort by name
  eventSorter(a, b) {
    const startA = a.dataStartMs || a.startDateMS,
      // dataXX are used if configured with fillTicks
      endA = a.dataEndMs || a.endDateMS,
      startB = b.dataStartMs || b.startDateMS,
      endB = b.dataEndMs || b.endDateMS,
      nameA = a.isModel ? a.name : a.eventRecord.name,
      nameB = b.isModel ? b.name : b.eventRecord.name;
    return startA - startB || endB - endA || (nameA < nameB ? -1 : nameA == nameB ? 0 : 1);
  }
  layoutEvents(resourceRecord, allEvents, includeOutside = false, parentEventRecord, eventSorter) {
    const me = this,
      {
        scheduler
      } = me,
      {
        variableColumnWidths
      } = scheduler,
      {
        id: resourceId
      } = resourceRecord,
      instanceMeta = resourceRecord.instanceMeta(scheduler),
      cacheKey = parentEventRecord ? `${resourceId}-${parentEventRecord.id}` : resourceId,
      // Cache per resource
      cache = me.resourceMap.set(cacheKey, {}).get(cacheKey),
      // Resource "column"
      resourceIndex = me.allResourceRecords.indexOf(resourceRecord),
      {
        barMargin,
        resourceMargin
      } = scheduler.getResourceLayoutSettings(resourceRecord, parentEventRecord);
    const layoutData = allEvents.reduce((toLayout, eventRecord) => {
      if (eventRecord.isScheduled) {
        const renderData = scheduler.generateRenderData(eventRecord, resourceRecord, false),
          // Elements will be appended to eventData during syncing
          eventData = {
            renderData
          },
          eventResources = ObjectHelper.getMapPath(me.eventMap, renderData.eventId, {});
        // Cache per event, { e1 : { r1 : { xxx }, r2 : ... }, e2 : ... }
        // Uses renderData.eventId in favor of eventRecord.id to work with ResourceTimeRanges
        eventResources[resourceId] = eventData;
        // Cache per resource
        cache[renderData.eventId] = eventData;
        // Position ResourceTimeRanges directly, they do not affect the layout of others
        if (renderData.fillSize) {
          // The property "left" is utilized based on Scheduler's rtl setting.
          // If RTL, then it's used as the "right" style position.
          renderData.left = variableColumnWidths ? instanceMeta.insetStart : resourceIndex * scheduler.resourceColumnWidth;
          renderData.width = scheduler.getResourceWidth(resourceRecord);
        }
        // Anything not flagged with `fillSize` should take part in layout
        else {
          toLayout.push(renderData);
        }
      }
      return toLayout;
    }, []);
    // Ensure the events are rendered in natural order so that navigation works.
    layoutData.sort(eventSorter ?? me.eventSorter);
    // Apply per resource event layout (pack, overlap or mixed)
    me.verticalLayout.applyLayout(layoutData, scheduler.getResourceWidth(resourceRecord, parentEventRecord), resourceMargin, barMargin, resourceIndex, scheduler.getEventLayout(resourceRecord, parentEventRecord));
    return cache;
  }
  // Calculate the layout for all events assigned to a resource. Since we are never stacking, the layout of one
  // resource will never affect the others
  layoutResourceEvents(resourceRecord) {
    const me = this,
      {
        scheduler
      } = me,
      // Used in loop, reduce access time a wee bit
      {
        assignmentStore,
        eventStore,
        timeAxis
      } = scheduler;
    // Events for the resource, minus those that are filtered out by filtering assignments and events
    let events = eventStore.getEvents({
      includeOccurrences: scheduler.enableRecurringEvents,
      resourceRecord,
      startDate: timeAxis.startDate,
      endDate: timeAxis.endDate,
      filter: (assignmentStore.isFiltered || eventStore.isFiltered) && (eventRecord => eventRecord.assignments.some(a => a.resource === resourceRecord && assignmentStore.includes(a)))
    });
    // Hook for features to inject additional timespans to render
    events = scheduler.getEventsToRender(resourceRecord, events);
    return me.layoutEvents(resourceRecord, events);
  }
  /**
   * Used by event drag features to bring into existence event elements that are outside of the rendered block.
   * @param {Scheduler.model.TimeSpan} eventRecord The event to render
   * @private
   */
  addTemporaryDragElement(eventRecord) {
    const {
        scheduler
      } = this,
      renderData = scheduler.generateRenderData(eventRecord, eventRecord.resource, {
        timeAxis: true,
        viewport: true
      });
    renderData.top = renderData.row ? renderData.top + renderData.row.top : scheduler.getResourceEventBox(eventRecord, eventRecord.resource, true).top;
    const domConfig = this.renderEvent({
        renderData
      }),
      {
        dataset
      } = domConfig;
    delete domConfig.tabIndex;
    delete dataset.eventId;
    delete dataset.resourceId;
    delete dataset.assignmentId;
    delete dataset.syncId;
    dataset.transient = true;
    domConfig.parent = this.scheduler.foregroundCanvas;
    // So that the regular DomSyncing which may happen during scroll does not
    // sweep up and reuse the temporary element.
    domConfig.retainElement = true;
    const result = DomHelper.createElement(domConfig);
    result.innerElement = result.firstChild;
    eventRecord.instanceMeta(scheduler).hasTemporaryDragElement = true;
    return result;
  }
  // To update an event, first release its element and then render it again.
  // The element will be reused and updated. Keeps code simpler
  renderEvent(eventData) {
    // No point in rendering event that already has an element
    const {
        scheduler
      } = this,
      data = eventData.renderData,
      {
        resourceRecord,
        assignmentRecord,
        eventRecord
      } = data,
      // Event element config, applied to existing element or used to create a new one below
      elementConfig = {
        className: data.wrapperCls,
        tabIndex: -1,
        children: [{
          role: 'presentation',
          className: data.cls,
          style: (data.internalStyle || '') + (data.style || ''),
          children: data.children,
          dataset: {
            // Each feature putting contents in the event wrap should have this to simplify syncing and
            // element retrieval after sync
            taskFeature: 'event'
          },
          syncOptions: {
            syncIdField: 'taskBarFeature'
          }
        }, ...data.wrapperChildren],
        style: {
          top: data.top,
          [scheduler.rtl ? 'right' : 'left']: data.left,
          // DomHelper appends px to dimensions when using numbers
          height: eventRecord.isMilestone ? '1em' : data.height,
          width: data.width,
          style: data.wrapperStyle || '',
          fontSize: eventRecord.isMilestone ? Math.min(data.width, 40) : null
        },
        dataset: {
          // assignmentId is set in this function conditionally
          resourceId: resourceRecord.id,
          eventId: data.eventId,
          // Not using eventRecord.id to distinguish between Event and ResourceTimeRange
          // Sync using assignment id for events and event id for ResourceTimeRanges
          syncId: assignmentRecord ? this.assignmentStore.getOccurrence(assignmentRecord, eventRecord).id : data.eventId
        },
        // Will not be part of DOM, but attached to the element
        elementData: eventData,
        // Dragging etc. flags element as retained, to not reuse/release it during that operation. Events
        // always use assignments, but ResourceTimeRanges does not
        retainElement: (assignmentRecord || eventRecord).instanceMeta(this.scheduler).retainElement,
        // Options for this level of sync, lower levels can have their own
        syncOptions: {
          syncIdField: 'taskFeature',
          // Remove instead of release when a feature is disabled
          releaseThreshold: 0
        }
      };
    elementConfig.className['b-sch-vertical'] = 1;
    // Some browsers throw warnings on zIndex = ''
    if (data.zIndex) {
      elementConfig.zIndex = data.zIndex;
    }
    // Do not want to spam dataset with empty prop when not using assignments (ResourceTimeRanges)
    if (assignmentRecord) {
      elementConfig.dataset.assignmentId = assignmentRecord.id;
    }
    // Allows access to the used config later, for example to retrieve element
    data.elementConfig = eventData.elementConfig = elementConfig;
    scheduler.afterRenderEvent({
      renderData: data,
      domConfig: elementConfig
    });
    return elementConfig;
  }
  renderResource(resourceRecord) {
    const me = this,
      // Date at top and bottom for determining which events to include
      {
        topDateMS,
        bottomDateMS
      } = me,
      // Will hold element configs
      eventDOMConfigs = [];
    let resourceEntry = me.resourceMap.get(resourceRecord.id);
    // Layout all events for the resource unless already done
    if (!resourceEntry) {
      resourceEntry = me.layoutResourceEvents(resourceRecord);
    }
    // Iterate over all events for the resource
    for (const eventId in resourceEntry) {
      const eventData = resourceEntry[eventId],
        {
          endDateMS,
          startDateMS,
          eventRecord
        } = eventData.renderData;
      if (
      // Only collect configs for those actually in view
      endDateMS >= topDateMS && startDateMS <= bottomDateMS &&
      // And not being dragged, those have a temporary element already
      !eventRecord.instanceMeta(me.scheduler).hasTemporaryDragElement) {
        var _eventData$elementCon;
        // Reuse DomConfig if available, otherwise render event to create one
        const domConfig = ((_eventData$elementCon = eventData.elementConfig) === null || _eventData$elementCon === void 0 ? void 0 : _eventData$elementCon.className) !== 'b-released' && eventData.elementConfig || me.renderEvent(eventData);
        eventDOMConfigs.push(domConfig);
      }
    }
    return eventDOMConfigs;
  }
  isEventElement(domConfig) {
    const className = domConfig && domConfig.className;
    return className && className[this.scheduler.eventCls + '-wrap'];
  }
  get shouldWaitForInitializeAndEngineReady() {
    return !this.initialized || !this.scheduler.isEngineReady && !this.scheduler.isCreating;
  }
  // Single cell so only one call to this renderer, determine which events are in view and draw them.
  // Drawing on scroll is triggered by `updateFromVerticalScroll()` and `updateFromHorizontalScroll()`
  renderer() {
    const me = this,
      {
        scheduler
      } = me,
      // Determine resource range to draw events for
      {
        first: firstResource,
        last: lastResource
      } = me.resourceRange,
      // Date at top and bottom for determining which events to include
      {
        topDate,
        bottomDate
      } = me.dateRange,
      syncConfigs = [],
      featureDomConfigs = [];
    // If scheduler is creating a new event, the render needs to be synchronous, so
    // we cannot wait for the engine to normalize - the new event will have correct data set.
    if (me.shouldWaitForInitializeAndEngineReady) {
      return;
    }
    // Update current time range, reflecting the change on the vertical time axis header
    if (!DateHelper.isEqual(topDate, me.topDate) || !DateHelper.isEqual(bottomDate, me.bottomDate)) {
      // Calculated values used by `renderResource()`
      me.topDate = topDate;
      me.bottomDate = bottomDate;
      me.topDateMS = topDate.getTime();
      me.bottomDateMS = bottomDate.getTime();
      const range = me.timeView.range = {
        startDate: topDate,
        endDate: bottomDate
      };
      scheduler.onVisibleDateRangeChange(range);
    }
    if (firstResource !== -1 && lastResource !== -1) {
      // Collect all events for resources in view
      for (let i = firstResource; i <= lastResource; i++) {
        syncConfigs.push.apply(syncConfigs, me.renderResource(me.allResourceRecords[i]));
      }
    }
    scheduler.getForegroundDomConfigs(featureDomConfigs);
    syncConfigs.push.apply(syncConfigs, featureDomConfigs);
    DomSync.sync({
      domConfig: {
        onlyChildren: true,
        children: syncConfigs
      },
      targetElement: scheduler.foregroundCanvas,
      syncIdField: 'syncId',
      // Called by DomHelper when it creates, releases or reuses elements
      callback({
        action,
        domConfig,
        lastDomConfig,
        targetElement,
        jsx
      }) {
        var _domConfig$elementDat;
        const {
          reactComponent
        } = scheduler;
        // If element is an event wrap, trigger appropriate events
        if (me.isEventElement(domConfig) || jsx || domConfig !== null && domConfig !== void 0 && (_domConfig$elementDat = domConfig.elementData) !== null && _domConfig$elementDat !== void 0 && _domConfig$elementDat.jsx) {
          var _scheduler$processEve;
          const
            // Some actions are considered first a release and then a render (reusing another element).
            // This gives clients code a chance to clean up before reusing an element
            isRelease = releaseEventActions[action],
            isRender = renderEventActions[action];
          if ((_scheduler$processEve = scheduler.processEventContent) !== null && _scheduler$processEve !== void 0 && _scheduler$processEve.call(scheduler, {
            action,
            domConfig,
            isRelease: false,
            targetElement,
            reactComponent,
            jsx
          })) return;
          // If we are reusing an element that was previously released we should not trigger again
          if (isRelease && me.isEventElement(lastDomConfig) && !lastDomConfig.isReleased) {
            var _scheduler$processEve2;
            const data = lastDomConfig.elementData.renderData,
              event = {
                renderData: data,
                assignmentRecord: data.assignmentRecord,
                eventRecord: data.eventRecord,
                resourceRecord: data.resourceRecord,
                element: targetElement
              };
            // Release any portal in React event content
            (_scheduler$processEve2 = scheduler.processEventContent) === null || _scheduler$processEve2 === void 0 ? void 0 : _scheduler$processEve2.call(scheduler, {
              isRelease,
              targetElement,
              reactComponent,
              assignmentRecord: data.assignmentRecord
            });
            // Some browsers do not blur on set to display:none, so releasing the active element
            // must *explicitly* move focus outwards to the view.
            if (targetElement === DomHelper.getActiveElement(targetElement)) {
              scheduler.focusElement.focus();
            }
            // This event is documented on Scheduler
            scheduler.trigger('releaseEvent', event);
          }
          if (isRender) {
            const data = domConfig.elementData.renderData,
              event = {
                renderData: data,
                assignmentRecord: data.assignmentRecord,
                eventRecord: data.eventRecord,
                resourceRecord: data.resourceRecord,
                element: targetElement,
                isReusingElement: action === 'reuseElement',
                isRepaint: action === 'reuseOwnElement'
              };
            event.reusingElement = action === 'reuseElement';
            // This event is documented on Scheduler
            scheduler.trigger('renderEvent', event);
          }
        }
      }
    });
    // Change in displayed resources?
    if (me.firstResource !== firstResource || me.lastResource !== lastResource) {
      // Update header to match
      const range = me.resourceColumns.visibleResources = {
        firstResource,
        lastResource
      };
      // Store which resources are currently in view
      me.firstResource = firstResource;
      me.lastResource = lastResource;
      scheduler.onVisibleResourceRangeChange(range);
      scheduler.trigger('resourceRangeChange', range);
    }
  }
  refresh(transition) {
    this.scheduler.runWithTransition(() => this.renderer(), transition);
  }
  // To match horizontals API, used from EventDrag
  refreshResources(resourceIds) {
    this.clearResources(resourceIds);
    this.refresh();
  }
  // To match horizontals API, used from EventDrag
  refreshEventsForResource(recordOrRow, force = true, draw = true) {
    this.refreshResources([recordOrRow.id]);
  }
  onRenderDone() {}
  //endregion
  //region Other
  get timeView() {
    return this.scheduler.timeView;
  }
  //endregion
  //region Cache
  // Clears cached resource layout
  clearResources(resourceIds) {
    const {
      resourceMap,
      eventMap
    } = this;
    resourceIds.forEach(resourceId => {
      if (resourceMap.has(resourceId)) {
        // The *keys* of an Object are strings, so we must iterate the values
        // and use the original eventId to look up in the Map which preserves key type.
        Object.values(resourceMap.get(resourceId)).forEach(({
          renderData: {
            eventId
          }
        }) => {
          delete eventMap.get(eventId)[resourceId];
        });
        resourceMap.delete(resourceId);
      }
    });
  }
  clearAll() {
    this.resourceMap.clear();
    this.eventMap.clear();
  }
  //endregion
}

VerticalRendering._$name = 'VerticalRendering';

/**
 * @module Scheduler/view/TimeAxisBase
 */
function isLastLevel(level, levels) {
  return level === levels.length - 1;
}
function isLastCell(level, cell) {
  return cell === level.cells[level.cells.length - 1];
}
/**
 * Base class for HorizontalTimeAxis and VerticalTimeAxis. Contains shared functionality to only render ticks in view,
 * should not be used directly.
 *
 * @extends Core/widget/Widget
 * @private
 * @abstract
 */
class TimeAxisBase extends Widget {
  static $name = 'TimeAxisBase';
  //region Config
  static configurable = {
    /**
     * The minimum width for a bottom row header cell to be considered 'compact', which adds a special CSS class
     * to the row (for special styling). Copied from Scheduler/Gantt.
     * @config {Number}
     * @default
     */
    compactCellWidthThreshold: 15,
    // TimeAxisViewModel
    model: null,
    cls: null,
    /**
     * Style property to use as cell size. Either width or height depending on orientation
     * @config {'width'|'height'}
     * @private
     */
    sizeProperty: null,
    /**
     * Style property to use as cells position. Either left or top depending on orientation
     * @config {'left'|'top'}
     * @private
     */
    positionProperty: null
  };
  startDate = null;
  endDate = null;
  levels = [];
  size = null;
  // Set visible date range
  set range({
    startDate,
    endDate
  }) {
    const me = this;
    // Only process a change
    if (me.startDate - startDate || me.endDate - endDate) {
      var _client$verticalTimeA;
      const {
        client
      } = me;
      me.startDate = startDate;
      me.endDate = endDate;
      // Avoid refreshing if time axis view is not visible
      if (me.sizeProperty === 'width' && client !== null && client !== void 0 && client.hideHeaders || me.sizeProperty === 'height' && client !== null && client !== void 0 && (_client$verticalTimeA = client.verticalTimeAxisColumn) !== null && _client$verticalTimeA !== void 0 && _client$verticalTimeA.hidden) {
        return;
      }
      me.refresh();
    }
  }
  //endregion
  //region Html & rendering
  // Generates element configs for all levels defined by the current ViewPreset
  buildCells(start = this.startDate, end = this.endDate) {
    var _me$client;
    const me = this,
      {
        sizeProperty
      } = me,
      {
        stickyHeaders,
        isVertical
      } = me.client || {},
      featureHeaderConfigs = [],
      {
        length
      } = me.levels;
    const cellConfigs = me.levels.map((level, i) => {
      var _level$cells;
      const stickyHeader = stickyHeaders && (isVertical || i < length - 1);
      return {
        className: {
          'b-sch-header-row': 1,
          [`b-sch-header-row-${level.position}`]: 1,
          'b-sch-header-row-main': i === me.model.viewPreset.mainHeaderLevel,
          'b-lowest': isLastLevel(i, me.levels),
          'b-sticky-header': stickyHeader
        },
        syncOptions: {
          // Keep a maximum of 5 released cells. Might be fine with fewer since ticks are fixed width.
          // Prevents an unnecessary amount of cells from sticking around when switching from narrow to
          // wide tickSizes
          releaseThreshold: 5,
          syncIdField: 'tickIndex'
        },
        dataset: {
          headerFeature: `headerRow${i}`,
          headerPosition: level.position
        },
        // Only include cells in view
        children: (_level$cells = level.cells) === null || _level$cells === void 0 ? void 0 : _level$cells.filter(cell => cell.start < end && cell.end > start).map((cell, j) => ({
          role: 'presentation',
          className: {
            'b-sch-header-timeaxis-cell': 1,
            [cell.headerCellCls]: cell.headerCellCls,
            [`b-align-${cell.align}`]: cell.align,
            'b-last': isLastCell(level, cell)
          },
          dataset: {
            tickIndex: cell.index,
            // Used in export tests to resolve dates from tick elements
            ...(globalThis.DEBUG && {
              date: cell.start.getTime()
            })
          },
          style: {
            // DomHelper appends px to numeric dimensions
            [me.positionProperty]: cell.coord,
            [sizeProperty]: cell.width,
            [`min-${sizeProperty}`]: cell.width
          },
          children: [{
            tag: 'span',
            role: 'presentation',
            className: {
              'b-sch-header-text': 1,
              'b-sticky-header': stickyHeader
            },
            html: cell.value
          }]
        }))
      };
    });
    // When tested in isolation there is no client
    (_me$client = me.client) === null || _me$client === void 0 ? void 0 : _me$client.getHeaderDomConfigs(featureHeaderConfigs);
    cellConfigs.push(...featureHeaderConfigs);
    // noinspection JSSuspiciousNameCombination
    return {
      className: me.widgetClassList,
      syncOptions: {
        // Do not keep entire levels no longer used, for example after switching view preset
        releaseThreshold: 0
      },
      children: cellConfigs
    };
  }
  render(targetElement) {
    super.render(targetElement);
    this.refresh(true);
  }
  /**
   * Refresh the UI
   * @param {Boolean} [rebuild] Specify `true` to force a rebuild of the underlying header level definitions
   */
  refresh(rebuild = !this.levels.length) {
    const me = this,
      {
        columnConfig
      } = me.model,
      {
        levels
      } = me,
      oldLevelsCount = levels.length;
    if (rebuild) {
      levels.length = 0;
      columnConfig.forEach((cells, position) => levels[position] = {
        position,
        cells
      });
      me.size = levels[0].cells.reduce((sum, cell) => sum + cell.width, 0);
      const {
        parentElement
      } = me.element;
      // Don't mutate a classList unless necessary. Browsers invalidate the style.
      if (parentElement && (levels.length !== oldLevelsCount || rebuild)) {
        parentElement.classList.remove(`b-sch-timeaxiscolumn-levels-${oldLevelsCount}`);
        parentElement.classList.add(`b-sch-timeaxiscolumn-levels-${levels.length}`);
      }
    }
    if (!me.startDate || !me.endDate) {
      return;
    }
    // Boil down levels to only show what is in view
    DomSync.sync({
      domConfig: me.buildCells(),
      targetElement: me.element,
      syncIdField: 'headerFeature'
    });
    me.trigger('refresh');
  }
  //endregion
  // Our widget class doesn't include "base".
  get widgetClass() {
    return 'b-timeaxis';
  }
}
TimeAxisBase._$name = 'TimeAxisBase';

/**
 * @module Scheduler/view/HorizontalTimeAxis
 */
/**
 * A visual horizontal representation of the time axis described in the
 * {@link Scheduler.preset.ViewPreset#field-headers} field.
 * Normally you should not interact with this class directly.
 *
 * @extends Scheduler/view/TimeAxisBase
 * @private
 */
class HorizontalTimeAxis extends TimeAxisBase {
  //region Config
  static $name = 'HorizontalTimeAxis';
  static type = 'horizontaltimeaxis';
  static configurable = {
    model: null,
    sizeProperty: 'width'
  };
  //endregion
  get positionProperty() {
    var _this$owner;
    return (_this$owner = this.owner) !== null && _this$owner !== void 0 && _this$owner.rtl ? 'right' : 'left';
  }
  get width() {
    return this.size;
  }
  onModelUpdate() {
    var _this$owner2;
    // Force rebuild when availableSpace has changed, to recalculate width and maybe apply compact styling
    if (!((_this$owner2 = this.owner) !== null && _this$owner2 !== void 0 && _this$owner2.hideHeaders) && this.model.availableSpace > 0 && this.model.availableSpace !== this.width) {
      this.refresh(true);
    }
  }
  updateModel(timeAxisViewModel) {
    this.detachListeners('tavm');
    timeAxisViewModel === null || timeAxisViewModel === void 0 ? void 0 : timeAxisViewModel.ion({
      name: 'tavm',
      update: 'onModelUpdate',
      thisObj: this
    });
  }
}
HorizontalTimeAxis._$name = 'HorizontalTimeAxis';

/**
 * @module Scheduler/view/ResourceHeader
 */
/**
 * Header widget that renders resource column headers and acts as the interaction point for resource columns in vertical
 * mode. Note that it uses virtual rendering and element reusage to gain performance, only headers in view are available
 * in DOM. Because of this you should avoid direct element manipulation, any such changes can be discarded at any time.
 *
 * By default, it displays resources `name` and also applies its `iconCls` if any, like this:
 *
 * ```html
 * <i class="iconCls">name</i>
 * ```
 *
 * If Scheduler is configured with a {@link Scheduler.view.Scheduler#config-resourceImagePath} the
 * header will render miniatures for the resources, using {@link Scheduler.model.ResourceModel#field-imageUrl}
 * or {@link Scheduler.model.ResourceModel#field-image} with fallback to
 * {@link Scheduler.model.ResourceModel#field-name} + {@link Scheduler.view.Scheduler#config-resourceImageExtension}
 * for unset values.
 *
 * The contents and styling of the resource cells in the header can be customized using {@link #config-headerRenderer}:
 *
 * ```javascript
 * new Scheduler({
 *     mode            : 'vertical',
 *     resourceColumns : {
 *         headerRenderer : ({ resourceRecord }) => `Hello ${resourceRecord.name}`
 *     }
 * }
 *```
 *
 * The width of the resource columns is determined by the {@link #config-columnWidth} config.
 *
 * @extends Core/widget/Widget
 */
class ResourceHeader extends Widget {
  //region Config
  static $name = 'ResourceHeader';
  static type = 'resourceheader';
  static configurable = {
    /**
     * Resource store used to render resource headers. Assigned from Scheduler.
     * @config {Scheduler.data.ResourceStore}
     * @private
     */
    resourceStore: null,
    /**
     * Custom header renderer function. Can be used to manipulate the element config used to create the element
     * for the header:
     *
     * ```javascript
     * new Scheduler({
     *   resourceColumns : {
     *     headerRenderer({ elementConfig, resourceRecord }) {
     *       elementConfig.dataset.myExtraData = 'extra';
     *       elementConfig.style.fontWeight = 'bold';
     *     }
     *   }
     * });
     * ```
     *
     * See {@link DomConfig} for more information.
     * Please take care to not break the default configs :)
     *
     * Or as a template by returning HTML from the function:
     *
     * ```javascript
     * new Scheduler({
     *   resourceColumns : {
     *     headerRenderer : ({ resourceRecord }) => `
     *       <div class="my-custom-template">
     *       ${resourceRecord.firstName} {resourceRecord.surname}
     *       </div>
     *     `
     *   }
     * });
     * ```
     *
     * NOTE: When using `headerRenderer` no default internal markup is applied to the resource header cell,
     * `iconCls` and {@link Scheduler.model.ResourceModel#field-imageUrl} or {@link Scheduler.model.ResourceModel#field-image}
     * will have no effect unless you supply custom markup for them.
     *
     * @config {Function}
     * @param {Object} params Object containing the params below
     * @param {Scheduler.model.ResourceModel} params.resourceRecord Resource whose header is being rendered
     * @param {DomConfig} params.elementConfig A config object used to create the element for the resource
     */
    headerRenderer: null,
    /**
     * Set to `false` to render just the resource name, `true` to render an avatar (or initials if no image exists)
     * @config {Boolean}
     * @default true
     */
    showAvatars: {
      value: true,
      $config: 'nullify'
    },
    /**
     * Assign to toggle resource columns **fill* mode. `true` means they will stretch (grow) to fill viewport, `false`
     * that they will respect their configured `columnWidth`.
     *
     * This is ignored if *any* resources are loaded with {@link Scheduler.model.ResourceModel#field-columnWidth}.
     * @member {Boolean} fillWidth
     */
    /**
     * Automatically resize resource columns to **fill** available width. Set to `false` to always respect the
     * configured `columnWidth`.
     *
     * This is ignored if *any* resources are loaded with {@link Scheduler.model.ResourceModel#field-columnWidth}.
     * @config {Boolean}
     * @default
     */
    fillWidth: true,
    /**
     * Assign to toggle resource columns **fit* mode. `true` means they will grow or shrink to always fit viewport,
     * `false` that they will respect their configured `columnWidth`.
     *
     * This is ignored if *any* resources are loaded with {@link Scheduler.model.ResourceModel#field-columnWidth}.
     * @member {Boolean} fitWidth
     */
    /**
     * Automatically resize resource columns to always **fit** available width.
     *
     * This is ignored if *any* resources are loaded with {@link Scheduler.model.ResourceModel#field-columnWidth}.
     * @config {Boolean}
     * @default
     */
    fitWidth: false,
    /**
     * Width for each resource column.
     *
     * This is used for resources which are not are loaded with a {@link Scheduler.model.ResourceModel#field-columnWidth}.
     * @config {Number}
     */
    columnWidth: 150,
    // Copied from Scheduler#resourceImagePath on creation in TimeAxisColumn.js
    imagePath: null,
    // Copied from Scheduler#resourceImageExtension on creation in TimeAxisColumn.js
    imageExtension: null,
    // Copied from Scheduler#defaultResourceImageName on creation in TimeAxisColumn.js
    defaultImageName: null,
    availableWidth: null
  };
  /**
   * An index of the first visible resource in vertical mode
   * @property {Number}
   * @readonly
   * @private
   */
  firstResource = -1;
  /**
   * An index of the last visible resource in vertical mode
   * @property {Number}
   * @readonly
   * @private
   */
  lastResource = -1;
  //endregion
  //region Init
  construct(config) {
    const me = this;
    // Inject this into owning Scheduler early because code further down
    // can call code which uses scheduler.resourceColumns.
    config.scheduler._resourceColumns = me;
    super.construct(config);
    if (me.imagePath != null) {
      // Need to increase height a bit when displaying images
      me.element.classList.add('b-has-images');
    }
    EventHelper.on({
      element: me.element,
      delegate: '.b-resourceheader-cell',
      capture: true,
      click: 'onResourceMouseEvent',
      dblclick: 'onResourceMouseEvent',
      contextmenu: 'onResourceMouseEvent',
      thisObj: me
    });
  }
  changeShowAvatars(show) {
    var _this$avatarRendering;
    (_this$avatarRendering = this.avatarRendering) === null || _this$avatarRendering === void 0 ? void 0 : _this$avatarRendering.destroy();
    if (show) {
      this.avatarRendering = new AvatarRendering({
        element: this.element
      });
    }
    return show;
  }
  updateShowAvatars() {
    if (!this.isConfiguring) {
      this.refresh();
    }
  }
  //endregion
  //region ResourceStore
  updateResourceStore(store) {
    const me = this;
    me.detachListeners('resourceStore');
    if (store) {
      store.ion({
        name: 'resourceStore',
        changePreCommit: 'onResourceStoreDataChange',
        thisObj: me
      });
      // Already have data? Update width etc
      if (store.count) {
        me.onResourceStoreDataChange({});
      }
    }
  }
  // Redraw resource headers on any data change
  onResourceStoreDataChange({
    action
  }) {
    const me = this;
    // These must be ingested before we assess the source of column widths
    // so that they can be cleared *after* their values have been cached.
    me.getConfig('fillWidth');
    me.getConfig('fitWidth');
    me.updateWidthCache();
    const {
        element
      } = me,
      width = me.totalWidth;
    // If we have some defined columnWidths in the resourceStore
    // we must then bypass configured fitWidth and fillWidth behaviour.
    if (me.scheduler.variableColumnWidths) {
      me._fillWidth = me._fitWidth = false;
    } else {
      me._fillWidth = me.configuredFillWidth;
      me._fitWidth = me.configuredFitWidth;
    }
    if (width !== me.width) {
      DomHelper.setLength(element, 'width', width);
      // During setup, silently set the width. It will then render correctly. After setup, let the world know...
      me.column.set('width', width, me.column.grid.isConfiguring);
    }
    if (action === 'removeall') {
      // Keep nothing
      element.innerHTML = '';
    }
    if (action === 'remove' || action === 'add' || action === 'filter' || me.fitWidth || me.fillWidth) {
      me.refreshWidths();
    }
    me.column.grid.toggleEmptyText();
  }
  get totalWidth() {
    return this.updateWidthCache();
  }
  updateWidthCache() {
    let result = 0;
    const {
      scheduler
    } = this;
    // Flag so that VerticalRendering#getResourceRange knows
    // whether to use fast or slow mode to ascertain visible columns.
    scheduler.variableColumnWidths = false;
    scheduler.resourceStore.forEach(resource => {
      // Set the start position for each resource with respect to the widths
      resource.instanceMeta(scheduler).insetStart = result;
      resource.instanceMeta(scheduler).insetEnd = result + (resource.columnWidth || scheduler.resourceColumnWidth);
      if (resource.columnWidth == null) {
        result += scheduler.resourceColumnWidth;
      } else {
        result += resource.columnWidth;
        scheduler.variableColumnWidths = true;
      }
    });
    return result;
  }
  //endregion
  //region Properties
  changeColumnWidth(columnWidth) {
    // Cache configured value, because if *all* resources have their own columnWidths
    // the property will be nulled, but if we ever recieve a new resource with no
    // columnWidth, or a columnWidth is nulled, we then have to fall back to using this.
    if (!this.refreshingWidths) {
      this.configuredColumnWidth = columnWidth;
    }
    return columnWidth;
  }
  updateColumnWidth(width, oldWidth) {
    const me = this;
    // Flag set in refreshWidths, do not want to create a loop
    if (!me.refreshingWidths) {
      me.refreshWidths();
    }
    if (!me.isConfiguring) {
      // If resources are grouped, I need to refresh manually the cached width of resource header columns
      if (me.resourceStore.isGrouped) {
        me.updateWidthCache();
      }
      me.refresh();
      // Cannot trigger with requested width, might have changed because of fit/fill
      me.trigger('columnWidthChange', {
        width,
        oldWidth
      });
    }
  }
  changeFillWidth(fillWidth) {
    return this.configuredFillWidth = fillWidth;
  }
  updateFillWidth() {
    if (!this.isConfiguring) {
      this.refreshWidths();
    }
  }
  changeFitWidth(fitWidth) {
    return this.configuredFitWidth = fitWidth;
  }
  updateFitWidth() {
    if (!this.isConfiguring) {
      this.refreshWidths();
    }
  }
  getImageURL(imageName) {
    return StringHelper.joinPaths([this.imagePath || '', imageName || '']);
  }
  updateImagePath() {
    if (!this.isConfiguring) {
      this.refresh();
    }
  }
  //endregion
  //region Fit to width
  updateAvailableWidth(width) {
    this.refreshWidths();
  }
  // Updates the column widths according to fill and fit settings
  refreshWidths() {
    var _me$resourceStore;
    const me = this,
      {
        availableWidth,
        configuredColumnWidth
      } = me,
      count = (_me$resourceStore = me.resourceStore) === null || _me$resourceStore === void 0 ? void 0 : _me$resourceStore.count;
    // Bail out if availableWidth not yet set or resource store not assigned/loaded
    // or column widths are defined in the resources.
    if (!availableWidth || !count || me.scheduler.variableColumnWidths) {
      return;
    }
    me.refreshingWidths = true;
    const
      // Fit width if configured to do so or if configured to fill and used width is less than available width
      fit = me.fitWidth || me.fillWidth && configuredColumnWidth * count < availableWidth,
      useWidth = fit ? Math.floor(availableWidth / count) : configuredColumnWidth,
      shouldAnimate = me.column.grid.enableEventAnimations && Math.abs(me._columnWidth - useWidth) > 30;
    DomHelper.addTemporaryClass(me.element, 'b-animating', shouldAnimate ? 300 : 0, me);
    me.columnWidth = useWidth;
    me.refreshingWidths = false;
  }
  //endregion
  //region Rendering
  // Visual resource range, set by VerticalRendering + its buffer
  set visibleResources({
    firstResource,
    lastResource
  }) {
    this.firstResource = firstResource;
    this.lastResource = lastResource;
    this.updateWidthCache();
    this.refresh();
  }
  /**
   * Refreshes the visible headers
   */
  refresh() {
    const me = this,
      {
        firstResource,
        scheduler,
        resourceStore,
        lastResource
      } = me,
      {
        variableColumnWidths
      } = scheduler,
      groupField = resourceStore.isGrouped && resourceStore.groupers[0].field,
      configs = [];
    me.element.classList.toggle('b-grouped', Boolean(groupField));
    if (!me.column.grid.isConfiguring && firstResource > -1 && lastResource > -1 && lastResource < resourceStore.count) {
      let currentGroup;
      // Gather element configs for resource headers in view
      for (let i = firstResource; i <= lastResource; i++) {
        var _currentGroup;
        const resourceRecord = resourceStore.allResourceRecords[i],
          groupRecord = resourceRecord.instanceMeta(resourceStore).groupParent,
          groupChildren = groupRecord === null || groupRecord === void 0 ? void 0 : groupRecord.groupChildren;
        if (groupField && groupRecord.id !== ((_currentGroup = currentGroup) === null || _currentGroup === void 0 ? void 0 : _currentGroup.dataset.resourceId)) {
          const groupLeft = groupChildren[0].instanceMeta(scheduler).insetStart,
            groupWidth = groupChildren[groupChildren.length - 1].instanceMeta(scheduler).insetEnd - groupLeft;
          currentGroup = {
            className: 'b-resourceheader-group-cell',
            dataset: {
              resourceId: groupRecord.id
            },
            style: {
              left: groupLeft,
              width: groupWidth
            },
            children: [{
              tag: 'span',
              html: StringHelper.encodeHtml(groupChildren[0][groupField])
            }, {
              className: 'b-resourceheader-group-children',
              children: []
            }]
          };
          configs.push(currentGroup);
        }
        const instanceMeta = resourceRecord.instanceMeta(scheduler),
          // Possible variable column width taken from the resources, fallback to scheduler's default
          width = resourceRecord.columnWidth || me.columnWidth,
          position = groupField ? instanceMeta.insetStart - currentGroup.style.left //groupChildren[0].instanceMeta(scheduler).insetStart
          : variableColumnWidths ? instanceMeta.insetStart : i * me.columnWidth,
          elementConfig = {
            // Might look like overkill to use DomClassList here, but can be used in headerRenderer
            className: new DomClassList({
              'b-resourceheader-cell': 1
            }),
            dataset: {
              resourceId: resourceRecord.id
            },
            style: {
              [scheduler.rtl ? 'right' : 'left']: position,
              width
            },
            children: []
          };
        // Let a configured headerRenderer have a go at it before applying
        if (me.headerRenderer) {
          const value = me.headerRenderer({
            elementConfig,
            resourceRecord
          });
          if (value != null) {
            elementConfig.html = value;
          }
        }
        // No headerRenderer, apply default markup
        else {
          let imageUrl;
          if (resourceRecord.imageUrl) {
            imageUrl = resourceRecord.imageUrl;
          } else {
            if (me.imagePath != null) {
              if (resourceRecord.image !== false) {
                var _resourceRecord$name;
                const imageName = resourceRecord.image || ((_resourceRecord$name = resourceRecord.name) === null || _resourceRecord$name === void 0 ? void 0 : _resourceRecord$name.toLowerCase()) + me.imageExtension;
                imageUrl = me.getImageURL(imageName);
              }
            }
          }
          // By default showing resource name and optionally avatar
          elementConfig.children.push(me.showAvatars && me.avatarRendering.getResourceAvatar({
            resourceRecord,
            initials: resourceRecord.initials,
            color: resourceRecord.eventColor,
            iconCls: resourceRecord.iconCls,
            defaultImageUrl: me.defaultImageName && me.getImageURL(me.defaultImageName),
            imageUrl
          }), {
            tag: 'span',
            className: 'b-resource-name',
            html: StringHelper.encodeHtml(resourceRecord.name)
          });
        }
        if (groupField) {
          currentGroup.children[1].children.push(elementConfig);
        } else {
          configs.push(elementConfig);
        }
      }
    }
    // Sync changes to the header
    DomSync.sync({
      domConfig: {
        onlyChildren: true,
        children: configs
      },
      targetElement: me.element,
      syncIdField: 'resourceId'
    });
  }
  //endregion
  onResourceMouseEvent(event) {
    const resourceCell = event.target.closest('.b-resourceheader-cell'),
      resourceRecord = this.resourceStore.getById(resourceCell.dataset.resourceId);
    this.trigger('resourceHeader' + StringHelper.capitalize(event.type), {
      resourceRecord,
      event
    });
  }
  // This function is not meant to be called by any code other than Base#getCurrentConfig().
  // It extracts the current configs for the header, removing irrelevant ones
  getCurrentConfig(options) {
    const result = super.getCurrentConfig(options);
    // Assigned from Scheduler
    delete result.resourceStore;
    delete result.column;
    delete result.type;
    return result;
  }
}
ResourceHeader._$name = 'ResourceHeader';

/**
 * @module Scheduler/column/TimeAxisColumn
 */
/**
 * A column containing the timeline "viewport", in which events, dependencies etc. are drawn.
 * Normally you do not need to interact with or create this column, it is handled by Scheduler.
 *
 * If you wish to output custom contents inside the time axis row cells, you can provide your custom column configuration
 * using the {@link #config-renderer} like so:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *    appendTo         : document.body
 *    columns          : [
 *       { text : 'Name', field : 'name', width : 130 },
 *       {
 *           type : 'timeAxis',
 *           renderer({ record, cellElement }) {
 *               return '<div class="cool-chart"></div>';
 *           }
 *       }
 *    ]
 * });
 * ```
 *
 * @extends Grid/column/WidgetColumn
 * @column
 */
class TimeAxisColumn extends Events(WidgetColumn) {
  //region Config
  static $name = 'TimeAxisColumn';
  static get fields() {
    return [
    // Exclude some irrelevant fields from getCurrentConfig()
    {
      name: 'locked',
      persist: false
    }, {
      name: 'flex',
      persist: false
    }, {
      name: 'width',
      persist: false
    }, {
      name: 'cellCls',
      persist: false
    }, {
      name: 'field',
      persist: false
    }, 'mode'];
  }
  static get defaults() {
    return {
      /**
       * Set to false to prevent this column header from being dragged.
       * @config {Boolean} draggable
       * @category Interaction
       * @default false
       */
      draggable: false,
      /**
       * Set to false to prevent grouping by this column.
       * @config {Boolean} groupable
       * @category Interaction
       * @default false
       */
      groupable: false,
      /**
       * Allow column visibility to be toggled through UI.
       * @config {Boolean} hideable
       * @default false
       * @category Interaction
       */
      hideable: false,
      /**
       * Show column picker for the column.
       * @config {Boolean} showColumnPicker
       * @default false
       * @category Menu
       */
      showColumnPicker: false,
      /**
       * Allow filtering data in the column (if Filter feature is enabled)
       * @config {Boolean} filterable
       * @default false
       * @category Interaction
       */
      filterable: false,
      /**
       * Allow sorting of data in the column
       * @config {Boolean} sortable
       * @category Interaction
       * @default false
       */
      sortable: false,
      /**
       * Set to `false` to prevent the column from being drag-resized when the ColumnResize plugin is enabled.
       * @config {Boolean} resizable
       * @default false
       * @category Interaction
       */
      resizable: false,
      /**
       * Allow searching in the column (respected by QuickFind and Search features)
       * @config {Boolean} searchable
       * @default false
       * @category Interaction
       */
      searchable: false,
      /**
       * @config {String} editor
       * @hide
       */
      editor: false,
      /**
       * Set to `true` to show a context menu on the cell elements in this column
       * @config {Boolean} enableCellContextMenu
       * @default false
       * @category Menu
       */
      enableCellContextMenu: false,
      /**
       * @config {Function|Boolean} tooltipRenderer
       * @hide
       */
      tooltipRenderer: false,
      /**
       * CSS class added to the header of this column
       * @config {String} cls
       * @category Rendering
       * @default 'b-sch-timeaxiscolumn'
       */
      cls: 'b-sch-timeaxiscolumn',
      // needs to have width specified, flex-basis messes measurements up
      needWidth: true,
      mode: null,
      region: 'normal',
      exportable: false,
      htmlEncode: false
    };
  }
  static get type() {
    return 'timeAxis';
  }
  //region Init
  construct(config) {
    const me = this;
    super.construct(...arguments);
    me.thisObj = me;
    me.timeAxisViewModel = me.grid.timeAxisViewModel;
    // A bit hacky, because mode is a field and not a config
    // eslint-disable-next-line no-self-assign
    me.mode = me.mode;
    me.grid.ion({
      paint: 'onTimelinePaint',
      thisObj: me,
      once: true
    });
  }
  static get autoExposeFields() {
    return true;
  }
  // endregion
  doDestroy() {
    var _this$resourceColumns, _this$timeAxisView;
    (_this$resourceColumns = this.resourceColumns) === null || _this$resourceColumns === void 0 ? void 0 : _this$resourceColumns.destroy();
    (_this$timeAxisView = this.timeAxisView) === null || _this$timeAxisView === void 0 ? void 0 : _this$timeAxisView.destroy();
    super.doDestroy();
  }
  set mode(mode) {
    const me = this,
      {
        grid
      } = me;
    me.set('mode', mode);
    // In horizontal mode this column has a time axis header on top, with timeline ticks
    if (mode === 'horizontal') {
      me.timeAxisView = new HorizontalTimeAxis({
        model: me.timeAxisViewModel,
        compactCellWidthThreshold: me.compactCellWidthThreshold,
        owner: grid,
        client: grid
      });
    }
    // In vertical mode, it instead displays resources at top
    else if (mode === 'vertical') {
      me.resourceColumns = ResourceHeader.new({
        column: me,
        scheduler: grid,
        resourceStore: grid.resourceStore,
        imagePath: grid.resourceImagePath,
        imageExtension: grid.resourceImageExtension,
        defaultImageName: grid.defaultResourceImageName
      }, grid.resourceColumns || {});
      me.relayEvents(me.resourceColumns, ['resourceheaderclick', 'resourceheaderdblclick', 'resourceheadercontextmenu']);
    }
  }
  get mode() {
    return this.get('mode');
  }
  //region Events
  onViewModelUpdate({
    source: viewModel
  }) {
    const me = this;
    if (me.grid.timeAxisSubGrid.collapsed) {
      return;
    }
    if (me.mode === 'horizontal') {
      // render the time axis view into the column header element
      me.refreshHeader(true);
      me.width = viewModel.totalSize;
      me.grid.refresh();
      // When width is set above, that ends up on a columnsResized listener, but the refreshing of the fake
      // scrollers to accommodate the new width is not done in this timeframe, so the upcoming centering related
      // to preset change cannot work. So we have to refresh the fake scrollers now
      me.subGrid.refreshFakeScroll();
    } else if (me.mode === 'vertical') {
      // Refresh to rerender cells, in the process updating the vertical timeaxis to reflect view model changes
      me.grid.refreshRows();
    }
  }
  // Called on paint. SubGrid has its width so this is the earliest time to configure the TimeAxisViewModel with
  // correct width
  onTimelinePaint({
    firstPaint
  }) {
    const me = this;
    if (!me.subGrid.insertRowsBefore) {
      return;
    }
    if (firstPaint) {
      me.subGridElement.classList.add('b-timeline-subgrid');
      if (me.mode === 'vertical') {
        var _me$grid;
        me.refreshHeader();
        // The above operation can cause height change.
        (_me$grid = me.grid) === null || _me$grid === void 0 ? void 0 : _me$grid.onHeightChange();
      }
    }
  }
  //endregion
  //region Rendering
  /**
   * Refreshes the columns header contents (which is either a HorizontalTimeAxis or a ResourceHeader). Useful if you
   * have rendered some extra meta data that depends on external data such as the EventStore or ResourceStore.
   */
  refreshHeader(internal) {
    const me = this,
      {
        element
      } = me;
    if (element) {
      if (me.mode === 'horizontal') {
        // Force timeAxisViewModel to regenerate its column config, which calls header renderers etc.
        !internal && me.timeAxisViewModel.update(undefined, undefined, true);
        if (!me.timeAxisView.rendered) {
          // Do not need the normal header markup
          element.innerHTML = '';
          me.timeAxisView.render(element);
        } else {
          // Force rebuild of cells in case external data has changed (cheap since it still syncs to DOM)
          me.timeAxisView.refresh(true);
        }
      } else if (me.mode === 'vertical') {
        if (!me.resourceColumns.currentElement) {
          // Do not need the normal header markup
          element.innerHTML = '';
          me.resourceColumns.render(element);
        } else {
          me.resourceColumns.refresh();
        }
        // Vertical's resourceColumns is redrawn with the events, no need here
      }
    }
  }

  internalRenderer(renderData) {
    const {
      grid
    } = this;
    // No drawing of events before engines initial commit
    if (grid.project.isInitialCommitPerformed || grid.project.isDelayingCalculation) {
      grid.currentOrientation.renderer(renderData);
      return super.internalRenderer(renderData);
    }
    return '';
  }
  //endregion
  get timeAxisViewModel() {
    return this._timeAxisViewModel;
  }
  set timeAxisViewModel(timeAxisViewModel) {
    const me = this;
    me.detachListeners('tavm');
    timeAxisViewModel === null || timeAxisViewModel === void 0 ? void 0 : timeAxisViewModel.ion({
      name: 'tavm',
      update: 'onViewModelUpdate',
      prio: -10000,
      thisObj: me
    });
    me._timeAxisViewModel = timeAxisViewModel;
    if (me.timeAxisView) {
      me.timeAxisView.model = timeAxisViewModel;
    }
  }
  // Width of the time axis column is solely determined by the zoom level. We should not keep it part of the state
  // otherwise restoring the state might break the normal zooming process.
  // Covered by SchedulerState.t
  // https://github.com/bryntum/support/issues/5545
  getState() {
    const state = super.getState();
    delete state.width;
    delete state.flex;
    return state;
  }
}
ColumnStore.registerColumnType(TimeAxisColumn);
TimeAxisColumn._$name = 'TimeAxisColumn';

/**
 * @module Scheduler/view/VerticalTimeAxis
 */
/**
 * Widget that renders a vertical time axis. Only renders ticks in view. Used in vertical mode.
 * @extends Core/widget/Widget
 * @private
 */
class VerticalTimeAxis extends TimeAxisBase {
  static get $name() {
    return 'VerticalTimeAxis';
  }
  static get configurable() {
    return {
      cls: 'b-verticaltimeaxis',
      sizeProperty: 'height',
      positionProperty: 'top',
      wrapText: true
    };
  }
  // All cells overlayed in the same space.
  // For future use.
  buildHorizontalCells() {
    const me = this,
      {
        client
      } = me,
      stickyHeaders = client === null || client === void 0 ? void 0 : client.stickyHeaders,
      featureHeaderConfigs = [],
      cellConfigs = me.levels.reduce((result, level, i) => {
        if (level.cells) {
          var _level$cells;
          result.push(...((_level$cells = level.cells) === null || _level$cells === void 0 ? void 0 : _level$cells.filter(cell => cell.start < me.endDate && cell.end > me.startDate).map((cell, j, cells) => ({
            role: 'presentation',
            className: {
              'b-sch-header-timeaxis-cell': 1,
              [cell.headerCellCls]: cell.headerCellCls,
              [`b-align-${cell.align}`]: cell.align,
              'b-last': j === cells.length - 1,
              'b-lowest': i === me.levels.length - 1
            },
            dataset: {
              tickIndex: cell.index,
              cellId: `${i}-${cell.index}`,
              headerPosition: i,
              // Used in export tests to resolve dates from tick elements
              ...(globalThis.DEBUG && {
                date: cell.start.getTime()
              })
            },
            style: {
              // DomHelper appends px to numeric dimensions
              top: cell.coord,
              height: cell.width,
              minHeight: cell.width
            },
            children: [{
              role: 'presentation',
              className: {
                'b-sch-header-text': 1,
                'b-sticky-header': stickyHeaders
              },
              html: cell.value
            }]
          }))));
        }
        return result;
      }, []);
    // When tested in isolation there is no client
    client === null || client === void 0 ? void 0 : client.getHeaderDomConfigs(featureHeaderConfigs);
    cellConfigs.push(...featureHeaderConfigs);
    // noinspection JSSuspiciousNameCombination
    return {
      className: me.widgetClassList,
      dataset: {
        headerFeature: `headerRow0`,
        headerPosition: 0
      },
      syncOptions: {
        // Keep a maximum of 5 released cells. Might be fine with fewer since ticks are fixed width.
        // Prevents an unnecessary amount of cells from sticking around when switching from narrow to
        // wide tickSizes
        releaseThreshold: 5,
        syncIdField: 'cellId'
      },
      children: cellConfigs
    };
  }
  get height() {
    return this.size;
  }
}
VerticalTimeAxis._$name = 'VerticalTimeAxis';

/**
 * @module Scheduler/column/VerticalTimeAxisColumn
 */
/**
 * A special column containing the time axis labels when the Scheduler is used in vertical mode. You can configure,
 * it using the {@link Scheduler.view.Scheduler#config-verticalTimeAxisColumn} config object.
 *
 * **Note**: this column is sized by flexing to consume full width of its containing {@link Grid.view.SubGrid}. To
 * change width of this column, instead size the subgrid like so:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     mode           : 'vertical',
 *     subGridConfigs : {
 *         locked : {
 *             width : 300
 *         }
 *     }
 * });
 * ```
 *
 * @extends Grid/column/Column
 */
class VerticalTimeAxisColumn extends Column {
  static $name = 'VerticalTimeAxisColumn';
  static get type() {
    return 'verticalTimeAxis';
  }
  static get defaults() {
    return {
      /**
       * @hideconfigs autoWidth, autoHeight
       */
      /**
       * Set to false to prevent this column header from being dragged.
       * @config {Boolean} draggable
       * @category Interaction
       * @default false
       * @hide
       */
      draggable: false,
      /**
       * Set to false to prevent grouping by this column.
       * @config {Boolean} groupable
       * @category Interaction
       * @default false
       * @hide
       */
      groupable: false,
      /**
       * Allow column visibility to be toggled through UI.
       * @config {Boolean} hideable
       * @default false
       * @category Interaction
       * @hide
       */
      hideable: false,
      /**
       * Show column picker for the column.
       * @config {Boolean} showColumnPicker
       * @default false
       * @category Menu
       * @hide
       */
      showColumnPicker: false,
      /**
       * Allow filtering data in the column (if Filter feature is enabled)
       * @config {Boolean} filterable
       * @default false
       * @category Interaction
       * @hide
       */
      filterable: false,
      /**
       * Allow sorting of data in the column
       * @config {Boolean} sortable
       * @category Interaction
       * @default false
       * @hide
       */
      sortable: false,
      // /**
      //  * Set to `false` to prevent the column from being drag-resized when the ColumnResize plugin is enabled.
      //  * @config {Boolean} resizable
      //  * @default false
      //  * @category Interaction
      //  * @hide
      //  */
      // resizable : false,
      /**
       * Allow searching in the column (respected by QuickFind and Search features)
       * @config {Boolean} searchable
       * @default false
       * @category Interaction
       * @hide
       */
      searchable: false,
      /**
       * Specifies if this column should be editable, and define which editor to use for editing cells in the column (if CellEdit feature is enabled)
       * @config {String} editor
       * @default false
       * @category Interaction
       * @hide
       */
      editor: false,
      /**
       * Set to `true` to show a context menu on the cell elements in this column
       * @config {Boolean} enableCellContextMenu
       * @default false
       * @category Menu
       * @hide
       */
      enableCellContextMenu: false,
      /**
       * @config {Function|Boolean} tooltipRenderer
       * @hide
       */
      tooltipRenderer: false,
      /**
       * Column minimal width. If value is Number then minimal width is in pixels
       * @config {Number|String} minWidth
       * @default 0
       * @category Layout
       */
      minWidth: 0,
      resizable: false,
      cellCls: 'b-verticaltimeaxiscolumn',
      locked: true,
      flex: 1,
      alwaysClearCell: false
    };
  }
  get isFocusable() {
    return false;
  }
  construct(data) {
    super.construct(...arguments);
    this.view = new VerticalTimeAxis({
      model: this.grid.timeAxisViewModel,
      client: this.grid
    });
  }
  renderer({
    cellElement,
    size
  }) {
    this.view.render(cellElement);
    size.height = this.view.height;
  }
  // This function is not meant to be called by any code other than Base#getCurrentConfig().
  // It extracts the current configs (fields) for the column, removing irrelevant ones
  getCurrentConfig(options) {
    const result = super.getCurrentConfig(options);
    // Remove irrelevant configs
    delete result.id;
    delete result.region;
    delete result.type;
    delete result.field;
    delete result.ariaLabel;
    delete result.cellAriaLabel;
    return result;
  }
}
ColumnStore.registerColumnType(VerticalTimeAxisColumn);
VerticalTimeAxisColumn._$name = 'VerticalTimeAxisColumn';

/**
 * @module Scheduler/view/SchedulerBase
 */
const descriptionFormats = {
  month: 'MMMM, YYYY',
  week: ['MMMM YYYY (Wp)', 'S{MMM} - E{MMM YYYY} (S{Wp})'],
  day: 'MMMM D, YYYY'
};
/**
 * A thin base class for {@link Scheduler.view.Scheduler}. Does not include any features by default, allowing smaller
 * custom-built bundles if used in place of {@link Scheduler.view.Scheduler}.
 *
 * **NOTE:** In most scenarios you do probably want to use Scheduler instead of SchedulerBase.
 *
 * @mixes Scheduler/view/mixin/Describable
 * @mixes Scheduler/view/mixin/EventNavigation
 * @mixes Scheduler/view/mixin/EventSelection
 * @mixes Scheduler/view/mixin/SchedulerDom
 * @mixes Scheduler/view/mixin/SchedulerDomEvents
 * @mixes Scheduler/view/mixin/SchedulerEventRendering
 * @mixes Scheduler/view/mixin/SchedulerRegions
 * @mixes Scheduler/view/mixin/SchedulerScroll
 * @mixes Scheduler/view/mixin/SchedulerState
 * @mixes Scheduler/view/mixin/SchedulerStores
 * @mixes Scheduler/view/mixin/TimelineDateMapper
 * @mixes Scheduler/view/mixin/TimelineDomEvents
 * @mixes Scheduler/view/mixin/TimelineEventRendering
 * @mixes Scheduler/view/mixin/TimelineScroll
 * @mixes Scheduler/view/mixin/TimelineViewPresets
 * @mixes Scheduler/view/mixin/TimelineZoomable
 * @mixes Scheduler/view/mixin/TransactionalFeatureMixin
 * @mixes Scheduler/crud/mixin/CrudManagerView
 * @mixes Scheduler/data/mixin/ProjectConsumer
 *
 * @features Scheduler/feature/ColumnLines
 * @features Scheduler/feature/Dependencies
 * @features Scheduler/feature/DependencyEdit
 * @features Scheduler/feature/EventCopyPaste
 * @features Scheduler/feature/EventDrag
 * @features Scheduler/feature/EventDragCreate
 * @features Scheduler/feature/EventDragSelect
 * @features Scheduler/feature/EventEdit
 * @features Scheduler/feature/EventFilter
 * @features Scheduler/feature/EventMenu
 * @features Scheduler/feature/EventNonWorkingTime
 * @features Scheduler/feature/EventResize
 * @features Scheduler/feature/EventTooltip
 * @features Scheduler/feature/GroupSummary
 * @features Scheduler/feature/HeaderZoom
 * @features Scheduler/feature/Labels
 * @features Scheduler/feature/NonWorkingTime
 * @features Scheduler/feature/Pan
 * @features Scheduler/feature/ResourceMenu
 * @features Scheduler/feature/ResourceTimeRanges
 * @features Scheduler/feature/RowReorder
 * @features Scheduler/feature/ScheduleContext
 * @features Scheduler/feature/ScheduleMenu
 * @features Scheduler/feature/ScheduleTooltip
 * @features Scheduler/feature/SimpleEventEdit
 * @features Scheduler/feature/Split
 * @features Scheduler/feature/StickyEvents
 * @features Scheduler/feature/Summary
 * @features Scheduler/feature/TimeAxisHeaderMenu
 * @features Scheduler/feature/TimeRanges
 * @features Scheduler/feature/TimeSelection
 *
 * @features Scheduler/feature/experimental/ExcelExporter
 *
 * @features Scheduler/feature/export/PdfExport
 * @features Scheduler/feature/export/exporter/MultiPageExporter
 * @features Scheduler/feature/export/exporter/MultiPageVerticalExporter
 * @features Scheduler/feature/export/exporter/SinglePageExporter
 *
 * @extends Scheduler/view/TimelineBase
 * @widget
 */
class SchedulerBase extends TimelineBase.mixin(CrudManagerView, Describable, SchedulerDom, SchedulerDomEvents, SchedulerStores, SchedulerScroll, SchedulerState, SchedulerEventRendering, SchedulerRegions, SchedulerEventSelection, SchedulerEventNavigation, CurrentConfig, TransactionalFeatureMixin) {
  //region Config
  static $name = 'SchedulerBase';
  // Factoryable type name
  static type = 'schedulerbase';
  static configurable = {
    /**
     * Get/set the scheduler's read-only state. When set to `true`, any UIs for modifying data are disabled.
     * @member {Boolean} readOnly
     * @category Misc
     */
    /**
     * Configure as `true` to make the scheduler read-only, by disabling any UIs for modifying data.
     *
     * __Note that checks MUST always also be applied at the server side.__
     * @config {Boolean} readOnly
     * @default false
     * @category Misc
     */
    /**
     * The date to display when used as a component of a Calendar.
     *
     * This is required by the Calendar Mode Interface.
     *
     * @config {Date}
     * @category Calendar integration
     */
    date: {
      value: null,
      $config: {
        equal: 'date'
      }
    },
    /**
     * Unit used to control how large steps to take when clicking the previous and next buttons in the Calendar
     * UI. Only applies when used as a component of a Calendar.
     *
     * Suitable units depend on configured {@link #config-range}, a smaller or equal unit is recommended.
     *
     * @config {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'}
     * @default
     * @category Calendar integration
     */
    stepUnit: 'week',
    /**
     * Unit used to set the length of the time axis when used as a component of a Calendar. Suitable units are
     * `'month'`, `'week'` and `'day'`.
     *
     * @config {'day'|'week'|'month'}
     * @category Calendar integration
     * @default
     */
    range: 'week',
    /**
     * When the scheduler is used in a Calendar, this function provides the textual description for the
     * Calendar's toolbar.
     *
     * ```javascript
     *  descriptionRenderer : scheduler => {
     *      const
     *          count = scheduler.eventStore.records.filter(
     *              eventRec => DateHelper.intersectSpans(
     *                  scheduler.startDate, scheduler.endDate,
     *                  eventRec.startDate, eventRec.endDate)).length,
     *          startDate = DateHelper.format(scheduler.startDate, 'DD/MM/YYY'),
     *          endData = DateHelper.format(scheduler.endDate, 'DD/MM/YYY');
     *
     *      return `${startDate} - ${endData}, ${count} event${count === 1 ? '' : 's'}`;
     *  }
     * ```
     * @config {Function}
     * @param {Scheduler.view.SchedulerBase} view The active view.
     * @category Calendar integration
     */
    /**
     * A method allowing you to define date boundaries that will constrain resize, create and drag drop
     * operations. The method will be called with the Resource record, and the Event record.
     *
     * ```javascript
     *  new Scheduler({
     *      getDateConstraints(resourceRecord, eventRecord) {
     *          // Assuming you have added these extra fields to your own EventModel subclass
     *          const { minStartDate, maxEndDate } = eventRecord;
     *
     *          return {
     *              start : minStartDate,
     *              end   : maxEndDate
     *          };
     *      }
     *  });
     * ```
     * @param {Scheduler.model.ResourceModel} [resourceRecord] The resource record
     * @param {Scheduler.model.EventModel} [eventRecord] The event record
     * @returns {Object} Constraining object containing `start` and `end` constraints. Omitting either
     * will mean that end is not constrained. So you can prevent a resize or move from moving *before*
     * a certain time while not constraining the end date.
     * @returns {Date} [return.start] Start date
     * @returns {Date} [return.end] End date
     * @config {Function}
     * @category Scheduled events
     */
    getDateConstraints: null,
    /**
     * The time axis column config for vertical {@link Scheduler.view.SchedulerBase#config-mode}.
     *
     * Object with {@link Scheduler.column.VerticalTimeAxisColumn} configuration.
     *
     * This object will be used to configure the vertical time axis column instance.
     *
     * The config allows configuring the `VerticalTimeAxisColumn` instance used in vertical mode with any Column options that apply to it.
     *
     * Example:
     *
     * ```javascript
     * new Scheduler({
     *     mode     : 'vertical',
     *     features : {
     *         filterBar : true
     *     },
     *     verticalTimeAxisColumn : {
     *         text  : 'Filter by event name',
     *         width : 180,
     *         filterable : {
     *             // add a filter field to the vertical column access header
     *             filterField : {
     *                 type        : 'text',
     *                 placeholder : 'Type to search',
     *                 onChange    : ({ value }) => {
     *                     // filter event by name converting to lowerCase to be equal comparison
     *                     scheduler.eventStore.filter({
     *                         filters : event => event.name.toLowerCase().includes(value.toLowerCase()),
     *                         replace : true
     *                     });
     *                 }
     *             }
     *         }
     *     },
     *     ...
     * });
     * ```
     *
     * @config {VerticalTimeAxisColumnConfig}
     * @category Time axis
     */
    verticalTimeAxisColumn: {},
    /**
     * See {@link Scheduler.view.Scheduler#keyboard-shortcuts Keyboard shortcuts} for details
     * @config {Object<String,String>} keyMap
     * @category Common
     */
    /**
     * If true, a new event will be created when user double-clicks on a time axis cell (if scheduler is not in
     * read only mode).
     *
     * The duration / durationUnit of the new event will be 1 time axis tick (default), or it can be read from
     * the {@link Scheduler.model.EventModel#field-duration} and
     * {@link Scheduler.model.EventModel#field-durationUnit} fields.
     *
     * Set to `false` to not create events on double click.
     * @config {Boolean|Object} createEventOnDblClick
     * @param {Boolean} [createEventOnDblClick.useEventModelDefaults] set to `true` to set default duration
     * based on the defaults specified by the {@link Scheduler.model.EventModel#field-duration} and
     * {@link Scheduler.model.EventModel#field-durationUnit} fields.
     * @default
     * @category Scheduled events
     */
    createEventOnDblClick: true,
    /**
         * Number of pixels to horizontally extend the visible render zone by, controlling the events that will be
         * rendered. You can use this to increase or reduce the amount of event rendering happening when scrolling
         * along a horizontal time axis. This can be useful if you render huge amount of events.
         *
         * To force the scheduler to render all events within the TimeAxis start & end dates, set this to -1.
         * The initial render will take slightly longer but no extra event rendering will take place when scrolling.
         *
         * NOTE: This is an experimental API which might change in future releases.
         * @config {Number}
         * @default
         * @internal
         * @category Experimental
         */
    scrollBuffer: 0,
    // A CSS class identifying areas where events can be scheduled using drag-create, double click etc.
    schedulableAreaSelector: '.b-sch-timeaxis-cell',
    scheduledEventName: 'event',
    sortFeatureStore: 'resourceStore',
    /**
     * If set to `true` this will show a color field in the {@link Scheduler.feature.EventEdit} editor and also a
     * picker in the {@link Scheduler.feature.EventMenu}. Both enables the user to choose a color which will be
     * applied to the event bar's background. See EventModel's
     * {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} config.
     * config.
     * @config {Boolean}
     * @default false
     * @category Misc
     */
    showEventColorPickers: null
  };
  static get defaultConfig() {
    return {
      /**
       * Scheduler mode. Supported values: horizontal, vertical
       * @config {'horizontal'|'vertical'} mode
       * @default
       * @category Common
       */
      mode: 'horizontal',
      /**
       * CSS class to add to rendered events
       * @config {String}
       * @category CSS
       * @private
       * @default
       */
      eventCls: 'b-sch-event',
      /**
       * CSS class to add to cells in the timeaxis column
       * @config {String}
       * @category CSS
       * @private
       * @default
       */
      timeCellCls: 'b-sch-timeaxis-cell',
      /**
       * A CSS class to apply to each event in the view on mouseover (defaults to 'b-sch-event-hover').
       * @config {String}
       * @default
       * @category CSS
       * @private
       */
      overScheduledEventClass: 'b-sch-event-hover',
      /**
       * Set to `false` if you don't want to allow events overlapping times for any one resource (defaults to `true`).
       * <div class="note">Note that toggling this at runtime won't affect already overlapping events.</div>
       *
       * @prp {Boolean}
       * @default
       * @category Scheduled events
       */
      allowOverlap: true,
      /**
       * The height in pixels of Scheduler rows.
       * @config {Number}
       * @default
       * @category Common
       */
      rowHeight: 60,
      /**
       * Scheduler overrides Grids default implementation of {@link Grid.view.GridBase#config-getRowHeight} to
       * pre-calculate row heights based on events in the rows.
       *
       * The amount of rows that are pre-calculated is limited for performance reasons. The limit is configurable
       * by specifying the {@link Scheduler.view.SchedulerBase#config-preCalculateHeightLimit} config.
       *
       * The results of the calculation are cached internally.
       *
       * @config {Function} getRowHeight
       * @param {Scheduler.model.ResourceModel} getRowHeight.record Resource record to determine row height for
       * @returns {Number} Desired row height
       * @category Layout
       */
      /**
       * Maximum number of resources for which height is pre-calculated. If you have many events per
       * resource you might want to lower this number to gain some initial rendering performance.
       *
       * Specify a falsy value to opt out of row height pre-calculation.
       *
       * @config {Number}
       * @default
       * @category Layout
       */
      preCalculateHeightLimit: 10000,
      crudManagerClass: CrudManager,
      testConfig: {
        loadMaskError: {
          autoClose: 10,
          showDelay: 0
        }
      }
    };
  }
  timeCellSelector = '.b-sch-timeaxis-cell';
  resourceTimeRangeSelector = '.b-sch-resourcetimerange';
  //endregion
  //region Store & model docs
  // Documented here instead of in SchedulerStores since SchedulerPro uses different types
  // Configs
  /**
   * Inline events, will be loaded into an internally created EventStore.
   * @config {Scheduler.model.EventModel[]|EventModelConfig[]} events
   * @category Data
   */
  /**
   * The {@link Scheduler.data.EventStore} holding the events to be rendered into the scheduler (required).
   * @config {Scheduler.data.EventStore|EventStoreConfig} eventStore
   * @category Data
   */
  /**
   * Inline resources, will be loaded into an internally created ResourceStore.
   * @config {Scheduler.model.ResourceModel[]|ResourceModelConfig[]} resources
   * @category Data
   */
  /**
   * The {@link Scheduler.data.ResourceStore} holding the resources to be rendered into the scheduler (required).
   * @config {Scheduler.data.ResourceStore|ResourceStoreConfig} resourceStore
   * @category Data
   */
  /**
   * Inline assignments, will be loaded into an internally created AssignmentStore.
   * @config {Scheduler.model.AssignmentModel[]|Object[]} assignments
   * @category Data
   */
  /**
   * The optional {@link Scheduler.data.AssignmentStore}, holding assignments between resources and events.
   * Required for multi assignments.
   * @config {Scheduler.data.AssignmentStore|AssignmentStoreConfig} assignmentStore
   * @category Data
   */
  /**
   * Inline dependencies, will be loaded into an internally created DependencyStore.
   * @config {Scheduler.model.DependencyModel[]|DependencyModelConfig[]} dependencies
   * @category Data
   */
  /**
   * The optional {@link Scheduler.data.DependencyStore}.
   * @config {Scheduler.data.DependencyStore|DependencyStoreConfig} dependencyStore
   * @category Data
   */
  // Properties
  /**
   * Get/set events, applies to the backing project's EventStore.
   * @member {Scheduler.model.EventModel[]} events
   * @accepts {Scheduler.model.EventModel[]|EventModelConfig[]}
   * @category Data
   */
  /**
   * Get/set the event store instance of the backing project.
   * @member {Scheduler.data.EventStore} eventStore
   * @category Data
   */
  /**
   * Get/set resources, applies to the backing project's ResourceStore.
   * @member {Scheduler.model.ResourceModel[]} resources
   * @accepts {Scheduler.model.ResourceModel[]|ResourceModelConfig[]}
   * @category Data
   */
  /**
   * Get/set the resource store instance of the backing project
   * @member {Scheduler.data.ResourceStore} resourceStore
   * @category Data
   */
  /**
   * Get/set assignments, applies to the backing project's AssignmentStore.
   * @member {Scheduler.model.AssignmentModel[]} assignments
   * @accepts {Scheduler.model.AssignmentModel[]|Object[]}
   * @category Data
   */
  /**
   * Get/set the event store instance of the backing project.
   * @member {Scheduler.data.AssignmentStore} assignmentStore
   * @category Data
   */
  /**
   * Get/set dependencies, applies to the backing projects DependencyStore.
   * @member {Scheduler.model.DependencyModel[]} dependencies
   * @accepts {Scheduler.model.DependencyModel[]|DependencyModelConfig[]}
   * @category Data
   */
  /**
   * Get/set the dependencies store instance of the backing project.
   * @member {Scheduler.data.DependencyStore} dependencyStore
   * @category Data
   */
  //endregion
  //region Events
  /**
   * Fired after rendering an event, when its element is available in DOM.
   * @event renderEvent
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord The event record
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord The assignment record
   * @param {Object} renderData An object containing details about the event rendering, see
   *   {@link Scheduler.view.mixin.SchedulerEventRendering#config-eventRenderer} for details
   * @param {Boolean} isRepaint `true` if this render is a repaint of the event, updating its existing element
   * @param {Boolean} isReusingElement `true` if this render lead to the event reusing a released events element
   * @param {HTMLElement} element The event bar element
   */
  /**
   * Fired after releasing an event, useful to cleanup of custom content added on `renderEvent` or in `eventRenderer`.
   * @event releaseEvent
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel} eventRecord The event record
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord The assignment record
   * @param {Object} renderData An object containing details about the event rendering
   * @param {HTMLElement} element The event bar element
   */
  /**
   * Fired when clicking a resource header cell
   * @event resourceHeaderClick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Event} event The event
   */
  /**
   * Fired when double clicking a resource header cell
   * @event resourceHeaderDblclick
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Event} event The event
   */
  /**
   * Fired when activating context menu on a resource header cell
   * @event resourceHeaderContextmenu
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {Event} event The event
   */
  /**
   * Triggered when a keydown event is observed if there are selected events.
   * @event eventKeyDown
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel[]} eventRecords The selected event records
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The selected assignment records
   * @param {KeyboardEvent} event Browser event
   */
  /**
   * Triggered when a keyup event is observed if there are selected events.
   * @event eventKeyUp
   * @param {Scheduler.view.Scheduler} source This Scheduler
   * @param {Scheduler.model.EventModel[]} eventRecords The selected event records
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The selected assignment records
   * @param {KeyboardEvent} event Browser event
   */
  //endregion
  //region Functions injected by features
  // For documentation & typings purposes
  /**
   * Opens an editor UI to edit the passed event.
   *
   * *NOTE: Only available when the {@link Scheduler/feature/EventEdit EventEdit} feature is enabled.*
   *
   * @function editEvent
   * @param {Scheduler.model.EventModel} eventRecord Event to edit
   * @param {Scheduler.model.ResourceModel} [resourceRecord] The Resource record for the event.
   * This parameter is needed if the event is newly created for a resource and has not been assigned, or when using
   * multi assignment.
   * @param {HTMLElement} [element] Element to anchor editor to (defaults to events element)
   * @category Feature shortcuts
   */
  /**
   * Returns the dependency record for a DOM element
   *
   * *NOTE: Only available when the {@link Scheduler/feature/Dependencies Dependencies} feature is enabled.*
   *
   * @function resolveDependencyRecord
   * @param {HTMLElement} element The dependency line element
   * @returns {Scheduler.model.DependencyModel} The dependency record
   * @category Feature shortcuts
   */
  //endregion
  //region Init
  afterConstruct() {
    const me = this;
    super.afterConstruct();
    me.ion({
      scroll: 'onVerticalScroll',
      thisObj: me
    });
    if (me.createEventOnDblClick) {
      me.ion({
        scheduledblclick: me.onTimeAxisCellDblClick
      });
    }
  }
  //endregion
  //region Overrides
  onPaintOverride() {
    // Internal procedure used for paint method overrides
    // Not used in onPaint() because it may be chained on instance and Override won't be applied
  }
  //endregion
  //region Config getters/setters
  // Placeholder getter/setter for mixins, please make any changes needed to SchedulerStores#store instead
  get store() {
    return super.store;
  }
  set store(store) {
    super.store = store;
  }
  /**
   * Returns an object defining the range of visible resources
   * @property {Object}
   * @property {Scheduler.model.ResourceModel} visibleResources.first First visible resource
   * @property {Scheduler.model.ResourceModel} visibleResources.last Last visible resource
   * @readonly
   * @category Resources
   */
  get visibleResources() {
    var _me$firstVisibleRow, _me$lastVisibleRow;
    const me = this;
    if (me.isVertical) {
      return me.currentOrientation.visibleResources;
    }
    return {
      first: me.store.getById((_me$firstVisibleRow = me.firstVisibleRow) === null || _me$firstVisibleRow === void 0 ? void 0 : _me$firstVisibleRow.id),
      last: me.store.getById((_me$lastVisibleRow = me.lastVisibleRow) === null || _me$lastVisibleRow === void 0 ? void 0 : _me$lastVisibleRow.id)
    };
  }
  //endregion
  //region Event handlers
  onLocaleChange() {
    this.currentOrientation.onLocaleChange();
    super.onLocaleChange();
  }
  onTimeAxisCellDblClick({
    date: startDate,
    resourceRecord,
    row
  }) {
    this.createEvent(startDate, resourceRecord, row);
  }
  onVerticalScroll({
    scrollTop
  }) {
    this.currentOrientation.updateFromVerticalScroll(scrollTop);
  }
  /**
   * Called when new event is created.
   * Сan be overridden to supply default record values etc.
   * @param {Scheduler.model.EventModel} eventRecord Newly created event
   * @category Scheduled events
   */
  onEventCreated(eventRecord) {}
  //endregion
  //region Mode
  /**
   * Checks if scheduler is in horizontal mode
   * @returns {Boolean}
   * @readonly
   * @category Common
   * @private
   */
  get isHorizontal() {
    return this.mode === 'horizontal';
  }
  /**
   * Checks if scheduler is in vertical mode
   * @returns {Boolean}
   * @readonly
   * @category Common
   * @private
   */
  get isVertical() {
    return this.mode === 'vertical';
  }
  /**
   * Get mode (horizontal/vertical)
   * @property {'horizontal'|'vertical'}
   * @readonly
   * @category Common
   */
  get mode() {
    return this._mode;
  }
  set mode(mode) {
    const me = this;
    me._mode = mode;
    if (!me[mode]) {
      me.element.classList.add(`b-sch-${mode}`);
      if (mode === 'horizontal') {
        me.horizontal = new HorizontalRendering(me);
        if (me.isPainted) {
          me.horizontal.init();
        }
      } else if (mode === 'vertical') {
        me.vertical = new VerticalRendering(me);
        if (me.rendered) {
          me.vertical.init();
        }
      }
    }
  }
  get currentOrientation() {
    return this[this.mode];
  }
  //endregion
  //region Dom event dummies
  // this is ugly, but needed since super cannot be called from SchedulerDomEvents mixin...
  onElementKeyDown(event) {
    return super.onElementKeyDown(event);
  }
  onElementKeyUp(event) {
    return super.onElementKeyUp(event);
  }
  onElementMouseOver(event) {
    return super.onElementMouseOver(event);
  }
  onElementMouseOut(event) {
    return super.onElementMouseOut(event);
  }
  //endregion
  //region Feature hooks
  // Called for each event during drop
  processEventDrop() {}
  processCrossSchedulerEventDrop() {}
  // Called before event drag starts
  beforeEventDragStart() {}
  // Called after event drag starts
  afterEventDragStart() {}
  // Called after aborting a drag
  afterEventDragAbortFinalized() {}
  // Called during event drag validation
  checkEventDragValidity() {}
  // Called after event resizing starts
  afterEventResizeStart() {}
  // Called after generating a DomConfig for an event
  afterRenderEvent() {}
  //endregion
  //region Scheduler specific date mapping functions
  get hasEventEditor() {
    return Boolean(this.eventEditingFeature);
  }
  get eventEditingFeature() {
    const {
      eventEdit,
      taskEdit,
      simpleEventEdit
    } = this.features;
    return eventEdit !== null && eventEdit !== void 0 && eventEdit.enabled ? eventEdit : taskEdit !== null && taskEdit !== void 0 && taskEdit.enabled ? taskEdit : simpleEventEdit !== null && simpleEventEdit !== void 0 && simpleEventEdit.enabled ? simpleEventEdit : null;
  }
  // Method is chained by event editing features. Ensure that the event is in the store.
  editEvent(eventRecord, resourceRecord, element) {
    const me = this,
      {
        eventStore,
        assignmentStore
      } = me;
    // Abort the chain if no event editing features available
    if (!me.hasEventEditor) {
      return false;
    }
    if (eventRecord.eventStore !== eventStore) {
      const {
          enableEventAnimations
        } = me,
        resourceRecords = [];
      // It's only a provisional event because we are going to edit it which will
      // allow an opportunity to cancel the add (by removing it).
      eventRecord.isCreating = true;
      let assignmentRecords = [];
      if (resourceRecord) {
        resourceRecords.push(resourceRecord);
        assignmentRecords = assignmentStore.assignEventToResource(eventRecord, resourceRecord);
      }
      // Vetoable beforeEventAdd allows cancel of this operation
      if (me.trigger('beforeEventAdd', {
        eventRecord,
        resourceRecords,
        assignmentRecords
      }) === false) {
        // Remove any assignment created above, to leave store as it was
        assignmentStore === null || assignmentStore === void 0 ? void 0 : assignmentStore.remove(assignmentRecords);
        return false;
      }
      me.enableEventAnimations = false;
      eventStore.add(eventRecord);
      me.project.commitAsync().then(() => me.enableEventAnimations = enableEventAnimations);
      // Element must be created synchronously, not after the project's normalizing delays.
      me.refreshRows();
    }
  }
  /**
   * Creates an event on the specified date (and scrolls it into view), for the specified resource which conforms to
   * this scheduler's {@link #config-createEventOnDblClick} setting.
   *
   * NOTE: If the scheduler is readonly, or resource type is invalid (group header), or if `allowOverlap` is `false`
   * and slot is already occupied - no event is created.
   *
   * This method may be called programmatically by application code if the `createEventOnDblClick` setting
   * is `false`, in which case the default values for `createEventOnDblClick` will be used.
   *
   * If the {@link Scheduler.feature.EventEdit} feature is active, the new event
   * will be displayed in the event editor.
   * @param {Date} date The date to add the event at.
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource to create the event for.
   * @category Scheduled events
   */
  async createEvent(startDate, resourceRecord) {
    var _me$eventEditingFeatu;
    const me = this,
      {
        enableEventAnimations,
        eventStore,
        assignmentStore,
        hasEventEditor
      } = me,
      resourceRecords = [resourceRecord],
      useEventModelDefaults = me.createEventOnDblClick.useEventModelDefaults,
      defaultDuration = useEventModelDefaults ? eventStore.modelClass.defaultValues.duration : 1,
      defaultDurationUnit = useEventModelDefaults ? eventStore.modelClass.defaultValues.durationUnit : me.timeAxis.unit,
      eventRecord = eventStore.createRecord({
        startDate,
        endDate: DateHelper.add(startDate, defaultDuration, defaultDurationUnit),
        duration: defaultDuration,
        durationUnit: defaultDurationUnit,
        name: me.L('L{Object.newEvent}')
      });
    if (me.readOnly || resourceRecord.isSpecialRow || resourceRecord.readOnly || !me.allowOverlap && !me.isDateRangeAvailable(eventRecord.startDate, eventRecord.endDate, null, resourceRecord)) {
      return;
    }
    (_me$eventEditingFeatu = me.eventEditingFeature) === null || _me$eventEditingFeatu === void 0 ? void 0 : _me$eventEditingFeatu.captureStm(true);
    // It's only a provisional event if there is an event edit feature available to
    // cancel the add (by removing it). Otherwise it's a definite event creation.
    eventRecord.isCreating = hasEventEditor;
    me.onEventCreated(eventRecord);
    const assignmentRecords = assignmentStore === null || assignmentStore === void 0 ? void 0 : assignmentStore.assignEventToResource(eventRecord, resourceRecord);
    /**
     * Fires before an event is added. Can be triggered by schedule double click or drag create action.
     * @event beforeEventAdd
     * @param {Scheduler.view.Scheduler} source The Scheduler instance
     * @param {Scheduler.model.EventModel} eventRecord The record about to be added
     * @param {Scheduler.model.ResourceModel[]} resourceRecords Resources that the record is assigned to
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignment records
     * @preventable
     */
    if (me.trigger('beforeEventAdd', {
      eventRecord,
      resourceRecords,
      assignmentRecords
    }) === false) {
      var _me$eventEditingFeatu2;
      // Remove any assignment created above, to leave store as it was
      assignmentStore === null || assignmentStore === void 0 ? void 0 : assignmentStore.remove(assignmentRecords);
      (_me$eventEditingFeatu2 = me.eventEditingFeature) === null || _me$eventEditingFeatu2 === void 0 ? void 0 : _me$eventEditingFeatu2.freeStm(false);
      return;
    }
    me.enableEventAnimations = false;
    eventStore.add(eventRecord);
    me.project.commitAsync().then(() => me.enableEventAnimations = enableEventAnimations);
    // Element must be created synchronously, not after the project's normalizing delays.
    // Overrides the check for isEngineReady in VerticalRendering so that the newly added record
    // will be rendered when we call refreshRows.
    me.isCreating = true;
    me.refreshRows();
    me.isCreating = false;
    await me.scrollEventIntoView(eventRecord);
    /**
     * Fired when a double click or drag gesture has created a new event and added it to the event store.
     * @event eventAutoCreated
     * @param {Scheduler.view.Scheduler} source This Scheduler.
     * @param {Scheduler.model.EventModel} eventRecord The new event record.
     * @param {Scheduler.model.ResourceModel} resourceRecord The resource assigned to the new event.
     */
    me.trigger('eventAutoCreated', {
      eventRecord,
      resourceRecord
    });
    if (hasEventEditor) {
      me.editEvent(eventRecord, resourceRecord, me.getEventElement(eventRecord));
    }
  }
  /**
   * Checks if a date range is allocated or not for a given resource.
   * @param {Date} start The start date
   * @param {Date} end The end date
   * @param {Scheduler.model.EventModel|null} excludeEvent An event to exclude from the check (or null)
   * @param {Scheduler.model.ResourceModel} resource The resource
   * @returns {Boolean} True if the timespan is available for the resource
   * @category Dates
   */
  isDateRangeAvailable(start, end, excludeEvent, resource) {
    return this.eventStore.isDateRangeAvailable(start, end, excludeEvent, resource);
  }
  //endregion
  /**
   * Suspends UI refresh on store operations.
   *
   * Multiple calls to `suspendRefresh` stack up, and will require an equal number of `resumeRefresh` calls to
   * actually resume UI refresh.
   *
   * @function suspendRefresh
   * @category Rendering
   */
  /**
   * Resumes UI refresh on store operations.
   *
   * Multiple calls to `suspendRefresh` stack up, and will require an equal number of `resumeRefresh` calls to
   * actually resume UI refresh.
   *
   * Specify `true` as the first param to trigger a refresh if this call unblocked the refresh suspension.
   * If the underlying project is calculating changes, the refresh will be postponed until it is done.
   *
   * @param {Boolean} trigger `true` to trigger a refresh, if this resume unblocks suspension
   * @category Rendering
   */
  async resumeRefresh(trigger) {
    super.resumeRefresh(false);
    const me = this;
    if (!me.refreshSuspended && trigger) {
      // Do not refresh until project is in a valid state
      if (!me.isEngineReady) {
        // Refresh will happen because of the commit, bail out of this one after forcing rendering to consider
        // next one a full refresh
        me.currentOrientation.refreshAllWhenReady = true;
        return me.project.commitAsync();
      }
      // View could've been destroyed while we waited for engine
      if (!me.isDestroyed) {
        // If it already is, refresh now
        me.refreshWithTransition();
      }
    }
  }
  //region Appearance
  // Overrides grid to take crudManager loading into account
  toggleEmptyText() {
    const me = this;
    if (me.bodyContainer) {
      var _me$crudManager;
      DomHelper.toggleClasses(me.bodyContainer, 'b-grid-empty', !(me.resourceStore.count > 0 || (_me$crudManager = me.crudManager) !== null && _me$crudManager !== void 0 && _me$crudManager.isLoading));
    }
  }
  // Overrides Grids base implementation to return a correctly calculated height for the row. Also stores it in
  // RowManagers height map, which is used to calculate total height etc.
  getRowHeight(resourceRecord) {
    if (this.isHorizontal) {
      const height = this.currentOrientation.calculateRowHeight(resourceRecord);
      this.rowManager.storeKnownHeight(resourceRecord.id, height);
      return height;
    }
  }
  // Calculates the height for specified rows. Call when changes potentially makes its height invalid
  calculateRowHeights(resourceRecords, silent = false) {
    // Array allowed to have nulls in it for easier code when calling this fn
    resourceRecords.forEach(resourceRecord => resourceRecord && this.getRowHeight(resourceRecord));
    if (!silent) {
      this.rowManager.estimateTotalHeight(true);
    }
  }
  // Calculate heights for all rows (up to the preCalculateHeightLimit)
  calculateAllRowHeights(silent = false) {
    const {
        store,
        rowManager
      } = this,
      count = Math.min(store.count, this.preCalculateHeightLimit);
    // Allow opt out by specifying falsy value.
    if (count) {
      rowManager.clearKnownHeights();
      for (let i = 0; i < count; i++) {
        // This will both calculate and store the height
        this.getRowHeight(store.getAt(i));
      }
      // Make sure height is reflected on scroller etc.
      if (!silent) {
        rowManager.estimateTotalHeight(true);
      }
    }
  }
  //endregion
  //region Calendar Mode Interface
  // These are all internal and match up w/CalendarMixin
  /**
   * Returns the date or ranges of included dates as an array. If only the {@link #config-startDate} is significant,
   * the array will have that date as its only element. Otherwise, a range of dates is returned as a two-element
   * array with `[0]` is the {@link #config-startDate} and `[1]` is the {@link #property-lastDate}.
   * @member {Date[]}
   * @internal
   */
  get dateBounds() {
    const me = this,
      ret = [me.startDate];
    if (me.range === 'week') {
      ret.push(me.lastDate);
    }
    return ret;
  }
  get defaultDescriptionFormat() {
    return descriptionFormats[this.range];
  }
  /**
   * The last day that is included in the date range. This is different than {@link #config-endDate} since that date
   * is not inclusive. For example, an `endDate` of 2022-07-21 00:00:00 indicates that the time range ends at that
   * time, and so 2022-07-21 is _not_ in the range. In this example, `lastDate` would be 2022-07-20 since that is the
   * last day included in the range.
   * @member {Date}
   * @internal
   */
  get lastDate() {
    const lastDate = this.endDate;
    // endDate is "exclusive" because it means 00:00:00 of that day, so subtract 1
    // to keep description consistent with human expectations.
    return lastDate && DateHelper.add(lastDate, -1, 'day');
  }
  getEventRecord(target) {
    target = DomHelper.getEventElement(target);
    return this.resolveEventRecord(target);
  }
  getEventElement(eventRecord) {
    return this.getElementFromEventRecord(eventRecord);
  }
  changeRange(unit) {
    return DateHelper.normalizeUnit(unit);
  }
  updateRange(unit) {
    if (!this.isConfiguring) {
      const currentDate = this.date,
        newDate = this.date = DateHelper.startOf(currentDate, unit);
      // Force a span update if changing the range did not change the date
      if (currentDate.getTime() === newDate.getTime()) {
        this.updateDate(newDate);
      }
    }
  }
  changeStepUnit(unit) {
    return DateHelper.normalizeUnit(unit);
  }
  updateDate(newDate) {
    const me = this,
      start = DateHelper.startOf(newDate, me.range);
    me.setTimeSpan(start, DateHelper.add(start, 1, me.range));
    // Cant always use newDate here in case timeAxis is filtered
    me.visibleDate = {
      date: DateHelper.max(newDate, me.timeAxis.startDate),
      block: 'start',
      animate: true
    };
    me.trigger('descriptionChange');
  }
  updateScrollBuffer(value) {
    if (!this.isConfiguring) {
      this.currentOrientation.scrollBuffer = value;
    }
  }
  previous() {
    this.date = DateHelper.add(this.date, -1, this.stepUnit);
  }
  next() {
    this.date = DateHelper.add(this.date, 1, this.stepUnit);
  }
  //endregion
  /**
   * Assigns and schedules an unassigned event record (+ adds it to this Scheduler's event store unless already in it).
   * @param {Object} config The config containing data about the event record to schedule
   * @param {Date} config.startDate The start date
   * @param {Scheduler.model.EventModel|EventModelConfig} config.eventRecord Event (or data for it) to assign and schedule
   * @param {Scheduler.model.EventModel} [config.parentEventRecord] Parent event to add the event to (to nest it),
   * only applies when using the NestedEvents feature
   * @param {Scheduler.model.ResourceModel} config.resourceRecord Resource to assign the event to
   * @param {HTMLElement} [config.element] The element if you are dragging an element from outside the scheduler
   * @category Scheduled events
   */
  async scheduleEvent({
    startDate,
    eventRecord,
    resourceRecord,
    element
  }) {
    const me = this;
    // NestedEvents has an override for this function to handle parentEventRecord
    if (!me.eventStore.includes(eventRecord)) {
      [eventRecord] = me.eventStore.add(eventRecord);
    }
    eventRecord.startDate = startDate;
    eventRecord.assign(resourceRecord);
    if (element) {
      const eventRect = Rectangle.from(element, me.foregroundCanvas);
      // Clear translate styles used by DragHelper
      DomHelper.setTranslateXY(element, 0, 0);
      DomHelper.setTopLeft(element, eventRect.y, eventRect.x);
      DomSync.addChild(me.foregroundCanvas, element, eventRecord.assignments[0].id);
    }
    await me.project.commitAsync();
  }
}
// Register this widget type with its Factory
SchedulerBase.initClass();
// Scheduler version is specified in TimelineBase because Gantt extends it
SchedulerBase._$name = 'SchedulerBase';

/**
 * @module Scheduler/widget/EventColorPicker
 */
/**
 * A color picker that displays a list of available event colors which the user can select by using mouse or keyboard.
 * See Schedulers {@link Scheduler.view.mixin.TimelineEventRendering#config-eventColor eventColor config} for default
 * available colors.
 *
 * {@inlineexample Scheduler/widget/EventColorPicker.js}
 *
 * ```javascript
 * new EventColorPicker({
 *    appendTo : 'container',
 *    width    : '10em',
 *    onColorSelected() {
 *        console.log(...arguments);
 *    }
 * });
 * ```
 *
 * @classType colorpicker
 * @extends Core/widget/ColorPicker
 */
class EventColorPicker extends ColorPicker {
  static $name = 'EventColorPicker';
  static type = 'eventcolorpicker';
  static configurable = {
    colorClasses: SchedulerBase.eventColors,
    colorClassPrefix: 'b-sch-',
    /**
     * @hideconfigs colors
     */
    colors: null,
    /**
     * Provide a {@link Scheduler.model.EventModel} instance to update it's
     * {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} field
     * @config {Scheduler.model.EventModel}
     */
    record: null
  };
  colorSelected({
    color
  }) {
    if (this.record) {
      this.record.eventColor = color;
    }
  }
}
EventColorPicker.initClass();
EventColorPicker._$name = 'EventColorPicker';

/**
 * @module Scheduler/tooltip/ClockTemplate
 */
/**
 * A template showing a clock, it consumes an object containing a date and a text
 * @private
 */
class ClockTemplate extends Base {
  static get defaultConfig() {
    return {
      minuteHeight: 8,
      minuteTop: 2,
      hourHeight: 8,
      hourTop: 2,
      handLeft: 10,
      div: document.createElement('div'),
      scheduler: null,
      // may be passed to the constructor if needed
      // `b-sch-clock-day` for calendar icon
      // `b-sch-clock-hour` for clock icon
      template(data) {
        return `<div class="b-sch-clockwrap b-sch-clock-${data.mode || this.mode} ${data.cls || ''}">
                    <div class="b-sch-clock">
                        <div class="b-sch-hour-indicator">${DateHelper.format(data.date, 'MMM')}</div>
                        <div class="b-sch-minute-indicator">${DateHelper.format(data.date, 'D')}</div>
                        <div class="b-sch-clock-dot"></div>
                    </div>
                    <span class="b-sch-clock-text">${StringHelper.encodeHtml(data.text)}</span>
                </div>`;
      }
    };
  }
  generateContent(data) {
    return this.div.innerHTML = this.template(data);
  }
  updateDateIndicator(el, date) {
    const hourIndicatorEl = el === null || el === void 0 ? void 0 : el.querySelector('.b-sch-hour-indicator'),
      minuteIndicatorEl = el === null || el === void 0 ? void 0 : el.querySelector('.b-sch-minute-indicator');
    if (date && hourIndicatorEl && minuteIndicatorEl && BrowserHelper.isBrowserEnv) {
      if (this.mode === 'hour') {
        hourIndicatorEl.style.transform = `rotate(${date.getHours() % 12 * 30}deg)`;
        minuteIndicatorEl.style.transform = `rotate(${date.getMinutes() * 6}deg)`;
      } else {
        hourIndicatorEl.style.transform = 'none';
        minuteIndicatorEl.style.transform = 'none';
      }
    }
  }
  set mode(mode) {
    this._mode = mode;
  }
  // `day` mode for calendar icon
  // `hour` mode for clock icon
  get mode() {
    if (this._mode) {
      return this._mode;
    }
    const unitLessThanDay = DateHelper.compareUnits(this.scheduler.timeAxisViewModel.timeResolution.unit, 'day') < 0,
      formatContainsHourInfo = DateHelper.formatContainsHourInfo(this.scheduler.displayDateFormat);
    return unitLessThanDay && formatContainsHourInfo ? 'hour' : 'day';
  }
  set template(template) {
    this._template = template;
  }
  /**
   * Get the clock template, which accepts an object of format { date, text }
   * @property {function(*): string}
   */
  get template() {
    return this._template;
  }
}
ClockTemplate._$name = 'ClockTemplate';

/**
 * @module Scheduler/feature/mixin/TaskEditStm
 */
/**
 * Mixin adding STM transactable behavior to TaskEdit feature.
 *
 * @mixin
 */
var TaskEditStm = (Target => class TaskEditStm extends (Target || Base) {
  static get $name() {
    return 'TaskEditStm';
  }
  getStmCapture() {
    return {
      stmInitiallyAutoRecord: this.stmInitiallyAutoRecord,
      stmInitiallyDisabled: this.stmInitiallyDisabled,
      // this flag indicates whether the STM capture has been transferred to
      // another feature, which will be responsible for finalizing the STM transaction
      // (otherwise we'll do it ourselves)
      transferred: false
    };
  }
  applyStmCapture(stmCapture) {
    this.stmInitiallyAutoRecord = stmCapture.stmInitiallyAutoRecord;
    this.stmInitiallyDisabled = stmCapture.stmInitiallyDisabled;
  }
  captureStm(startTransaction = false) {
    const me = this,
      project = me.project,
      stm = project.getStm();
    if (me.hasStmCapture) {
      return;
    }
    me.hasStmCapture = true;
    me.stmInitiallyDisabled = stm.disabled;
    me.stmInitiallyAutoRecord = stm.autoRecord;
    if (me.stmInitiallyDisabled) {
      stm.enable();
      // it seems this branch has never been exercised by tests
      // but the intention is to stop the auto-recording while
      // task editor is active (all editing is one manual transaction)
      stm.autoRecord = false;
    } else {
      if (me.stmInitiallyAutoRecord) {
        stm.autoRecord = false;
      }
      if (stm.isRecording) {
        stm.stopTransaction();
      }
    }
    if (startTransaction) {
      this.startStmTransaction();
    }
  }
  startStmTransaction() {
    this.project.getStm().startTransaction();
  }
  commitStmTransaction() {
    const me = this,
      stm = me.project.getStm();
    if (!me.hasStmCapture) {
      throw new Error('Does not have STM capture, no transaction to commit');
    }
    if (stm.enabled) {
      stm.stopTransaction();
      if (me.stmInitiallyDisabled) {
        stm.resetQueue();
      }
    }
  }
  async rejectStmTransaction() {
    const stm = this.project.getStm(),
      {
        client
      } = this;
    if (!this.hasStmCapture) {
      throw new Error('Does not have STM capture, no transaction to reject');
    }
    if (stm.enabled) {
      var _stm$transaction;
      if ((_stm$transaction = stm.transaction) !== null && _stm$transaction !== void 0 && _stm$transaction.length) {
        client.suspendRefresh();
        stm.rejectTransaction();
        await client.resumeRefresh(true);
      } else {
        stm.stopTransaction();
      }
    }
  }
  enableStm() {
    this.project.getStm().enable();
  }
  disableStm() {
    this.project.getStm().disable();
  }
  async freeStm(commitOrReject = null) {
    const me = this,
      stm = me.project.getStm(),
      {
        stmInitiallyDisabled,
        stmInitiallyAutoRecord
      } = me;
    if (!me.hasStmCapture) {
      return;
    }
    let promise;
    me.rejectingStmTransaction = true;
    if (commitOrReject === true) {
      promise = me.commitStmTransaction();
    } else if (commitOrReject === false) {
      // Note - we don't wait for async to complete here
      promise = me.rejectStmTransaction();
    }
    await promise;
    if (!stm.isDestroying) {
      stm.disabled = stmInitiallyDisabled;
      stm.autoRecord = stmInitiallyAutoRecord;
    }
    if (!me.isDestroying) {
      me.rejectingStmTransaction = true;
      me.hasStmCapture = false;
    }
  }
});

/**
 * @module Scheduler/feature/base/TimeSpanMenuBase
 */
/**
 * Abstract base class used by other context menu features which show the context menu for TimeAxis.
 * Using this class you can make sure the menu expects the target to disappear,
 * since it can be scroll out of the scheduling zone.
 *
 * Features that extend this class are:
 *  * {@link Scheduler/feature/EventMenu};
 *  * {@link Scheduler/feature/ScheduleMenu};
 *  * {@link Scheduler/feature/TimeAxisHeaderMenu};
 *
 * @extends Core/feature/base/ContextMenuBase
 * @abstract
 */
class TimeSpanMenuBase extends ContextMenuBase {}
TimeSpanMenuBase._$name = 'TimeSpanMenuBase';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceFrequencyCombo
 */
/**
 * A combobox field allowing to pick frequency in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencefrequencycombo
 */
class RecurrenceFrequencyCombo extends Combo {
  static $name = 'RecurrenceFrequencyCombo';
  // Factoryable type name
  static type = 'recurrencefrequencycombo';
  static configurable = {
    editable: false,
    displayField: 'text',
    valueField: 'value',
    localizeDisplayFields: true,
    addNone: false
  };
  buildItems() {
    return [...(this.addNone ? [{
      text: 'L{None}',
      value: 'NONE'
    }] : []), {
      value: 'DAILY',
      text: 'L{Daily}'
    }, {
      value: 'WEEKLY',
      text: 'L{Weekly}'
    }, {
      value: 'MONTHLY',
      text: 'L{Monthly}'
    }, {
      value: 'YEARLY',
      text: 'L{Yearly}'
    }];
  }
}
// Register this widget type with its Factory
RecurrenceFrequencyCombo.initClass();
RecurrenceFrequencyCombo._$name = 'RecurrenceFrequencyCombo';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceDaysCombo
 */
/**
 * A combobox field allowing to pick days for the `Monthly` and `Yearly` mode in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencedayscombo
 */
class RecurrenceDaysCombo extends Combo {
  static get $name() {
    return 'RecurrenceDaysCombo';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencedayscombo';
  }
  static get defaultConfig() {
    const allDaysValueAsArray = ['SU', 'MO', 'TU', 'WE', 'TH', 'FR', 'SA'],
      allDaysValue = allDaysValueAsArray.join(',');
    return {
      allDaysValue,
      editable: false,
      defaultValue: allDaysValue,
      workingDaysValue: allDaysValueAsArray.filter((day, index) => !DateHelper.nonWorkingDays[index]).join(','),
      nonWorkingDaysValue: allDaysValueAsArray.filter((day, index) => DateHelper.nonWorkingDays[index]).join(','),
      splitCls: 'b-recurrencedays-split',
      displayField: 'text',
      valueField: 'value'
    };
  }
  buildItems() {
    const me = this;
    me._weekDays = null;
    return me.weekDays.concat([{
      value: me.allDaysValue,
      text: me.L('L{day}'),
      cls: me.splitCls
    }, {
      value: me.workingDaysValue,
      text: me.L('L{weekday}')
    }, {
      value: me.nonWorkingDaysValue,
      text: me.L('L{weekend day}')
    }]);
  }
  get weekDays() {
    const me = this;
    if (!me._weekDays) {
      const weekStartDay = DateHelper.weekStartDay;
      const dayNames = DateHelper.getDayNames().map((text, index) => ({
        text,
        value: RecurrenceDayRuleEncoder.encodeDay(index)
      }));
      // we should start week w/ weekStartDay
      me._weekDays = dayNames.slice(weekStartDay).concat(dayNames.slice(0, weekStartDay));
    }
    return me._weekDays;
  }
  set value(value) {
    const me = this;
    if (value && Array.isArray(value)) {
      value = value.join(',');
    }
    // if the value has no matching option in the store we need to use default value
    if (!value || !me.store.findRecord('value', value)) {
      value = me.defaultValue;
    }
    super.value = value;
  }
  get value() {
    let value = super.value;
    if (value && Array.isArray(value)) {
      value = value.join(',');
    }
    return value;
  }
}
// Register this widget type with its Factory
RecurrenceDaysCombo.initClass();
RecurrenceDaysCombo._$name = 'RecurrenceDaysCombo';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceDaysButtonGroup
 */
/**
 * A segmented button field allowing to pick days for the "Weekly" mode in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * {@inlineexample Scheduler/view/RecurrenceDaysButtonGroup.js}
 *
 * @extends Core/widget/ButtonGroup
 */
class RecurrenceDaysButtonGroup extends ButtonGroup {
  static get $name() {
    return 'RecurrenceDaysButtonGroup';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencedaysbuttongroup';
  }
  static get defaultConfig() {
    return {
      defaults: {
        cls: 'b-raised',
        toggleable: true
      }
    };
  }
  construct(config = {}) {
    const me = this;
    config.columns = 7;
    config.items = me.buildItems();
    super.construct(config);
  }
  updateItemText(item) {
    const day = RecurrenceDayRuleEncoder.decodeDay(item.value)[0];
    item.text = DateHelper.getDayName(day).substring(0, 3);
  }
  buildItems() {
    const me = this;
    if (!me.__items) {
      const weekStartDay = DateHelper.weekStartDay;
      const dayNames = DateHelper.getDayNames().map((text, index) => ({
        text: text.substring(0, 3),
        value: RecurrenceDayRuleEncoder.encodeDay(index)
      }));
      // we should start week w/ weekStartDay
      me.__items = dayNames.slice(weekStartDay).concat(dayNames.slice(0, weekStartDay));
    }
    return me.__items;
  }
  set value(value) {
    if (value && Array.isArray(value)) {
      value = value.join(',');
    }
    super.value = value;
  }
  get value() {
    let value = super.value;
    if (value && Array.isArray(value)) {
      value = value.join(',');
    }
    return value;
  }
  onLocaleChange() {
    // update button texts on locale switch
    this.items.forEach(this.updateItemText, this);
  }
  updateLocalization() {
    this.onLocaleChange();
    super.updateLocalization();
  }
  get widgetClassList() {
    const classList = super.widgetClassList;
    // to look more like a real field
    classList.push('b-field');
    return classList;
  }
}
// Register this widget type with its Factory
RecurrenceDaysButtonGroup.initClass();
RecurrenceDaysButtonGroup._$name = 'RecurrenceDaysButtonGroup';

/**
 * A segmented button field allowing to pick month days for the `Monthly` mode in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/ButtonGroup
 */
class RecurrenceMonthDaysButtonGroup extends ButtonGroup {
  static get $name() {
    return 'RecurrenceMonthDaysButtonGroup';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencemonthdaysbuttongroup';
  }
  static get defaultConfig() {
    return {
      defaults: {
        toggleable: true,
        cls: 'b-raised'
      }
    };
  }
  get minValue() {
    return 1;
  }
  get maxValue() {
    return 31;
  }
  construct(config = {}) {
    const me = this;
    config.columns = 7;
    config.items = me.buildItems();
    super.construct(config);
  }
  buildItems() {
    const me = this,
      items = [];
    for (let value = me.minValue; value <= me.maxValue; value++) {
      // button config
      items.push({
        text: value + '',
        value
      });
    }
    return items;
  }
  get widgetClassList() {
    const classList = super.widgetClassList;
    // to look more like a real field
    classList.push('b-field');
    return classList;
  }
}
// Register this widget type with its Factory
RecurrenceMonthDaysButtonGroup.initClass();
RecurrenceMonthDaysButtonGroup._$name = 'RecurrenceMonthDaysButtonGroup';

/**
 * A segmented button field allowing to pick months for the `Yearly` mode in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/ButtonGroup
 */
class RecurrenceMonthsButtonGroup extends ButtonGroup {
  static get $name() {
    return 'RecurrenceMonthsButtonGroup';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencemonthsbuttongroup';
  }
  static get defaultConfig() {
    return {
      defaults: {
        toggleable: true,
        cls: 'b-raised'
      }
    };
  }
  construct(config = {}) {
    const me = this;
    config.columns = 4;
    config.items = me.buildItems();
    super.construct(config);
  }
  buildItems() {
    return DateHelper.getMonthNames().map((item, index) => ({
      text: item.substring(0, 3),
      value: index + 1 // 1-based
    }));
  }

  updateItemText(item) {
    item.text = DateHelper.getMonthName(item.value - 1).substring(0, 3);
  }
  onLocaleChange() {
    // update button texts on locale switch
    this.items.forEach(this.updateItemText, this);
  }
  updateLocalization() {
    this.onLocaleChange();
    super.updateLocalization();
  }
  get widgetClassList() {
    const classList = super.widgetClassList;
    // to look more like a real field
    classList.push('b-field');
    return classList;
  }
}
// Register this widget type with its Factory
RecurrenceMonthsButtonGroup.initClass();
RecurrenceMonthsButtonGroup._$name = 'RecurrenceMonthsButtonGroup';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceStopConditionCombo
 */
/**
 * A combobox field allowing to choose stop condition for the recurrence in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencestopconditioncombo
 */
class RecurrenceStopConditionCombo extends Combo {
  static get $name() {
    return 'RecurrenceStopConditionCombo';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencestopconditioncombo';
  }
  static get defaultConfig() {
    return {
      editable: false,
      placeholder: 'Never',
      displayField: 'text',
      valueField: 'value'
    };
  }
  buildItems() {
    return [{
      value: 'never',
      text: this.L('L{Never}')
    }, {
      value: 'count',
      text: this.L('L{After}')
    }, {
      value: 'date',
      text: this.L('L{On date}')
    }];
  }
  set value(value) {
    // Use 'never' instead of falsy value
    value = value || 'never';
    super.value = value;
  }
  get value() {
    return super.value;
  }
  get recurrence() {
    return this._recurrence;
  }
  set recurrence(recurrence) {
    let value = null;
    if (recurrence.endDate) {
      value = 'date';
    } else if (recurrence.count) {
      value = 'count';
    }
    this._recurrence = recurrence;
    this.value = value;
  }
}
// Register this widget type with its Factory
RecurrenceStopConditionCombo.initClass();
RecurrenceStopConditionCombo._$name = 'RecurrenceStopConditionCombo';

/**
 * @module Scheduler/view/recurrence/field/RecurrencePositionsCombo
 */
/**
 * A combobox field allowing to specify day positions in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence editor}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencepositionscombo
 */
class RecurrencePositionsCombo extends Combo {
  static get $name() {
    return 'RecurrencePositionsCombo';
  }
  // Factoryable type name
  static get type() {
    return 'recurrencepositionscombo';
  }
  static get defaultConfig() {
    return {
      editable: false,
      splitCls: 'b-sch-recurrencepositions-split',
      displayField: 'text',
      valueField: 'value',
      defaultValue: 1,
      maxPosition: 5
    };
  }
  buildItems() {
    return this.buildDayNumbers().concat([{
      value: '-1',
      text: this.L('L{position-1}'),
      cls: this.splitCls
    }]);
  }
  buildDayNumbers() {
    return ArrayHelper.populate(this.maxPosition, i => ({
      value: i + 1,
      text: this.L(`position${i + 1}`)
    }));
  }
  set value(value) {
    const me = this;
    if (value && Array.isArray(value)) {
      value = value.join(',');
    }
    // if the value has no matching option in the store we need to use default value
    if (!value || !me.store.findRecord('value', value)) {
      value = me.defaultValue;
    }
    super.value = value;
  }
  get value() {
    const value = super.value;
    return value ? `${value}`.split(',').map(item => parseInt(item, 10)) : [];
  }
}
// Register this widget type with its Factory
RecurrencePositionsCombo.initClass();
RecurrencePositionsCombo._$name = 'RecurrencePositionsCombo';

/**
 * @module Scheduler/view/recurrence/RecurrenceEditorPanel
 */
/**
 * Panel containing fields used to edit a {@link Scheduler.model.RecurrenceModel recurrence model}. Used by
 * {@link Scheduler/view/recurrence/RecurrenceEditor}, and by the recurrence tab in Scheduler Pro's event editor.
 *
 * Not intended to be used separately.
 *
 * @extends Core/widget/Panel
 * @classType recurrenceeditorpanel
 * @private
 */
class RecurrenceEditorPanel extends Panel {
  static $name = 'RecurrenceEditorPanel';
  static type = 'recurrenceeditorpanel';
  static configurable = {
    cls: 'b-recurrenceeditor',
    record: false,
    addNone: false,
    items: {
      frequencyField: {
        type: 'recurrencefrequencycombo',
        name: 'frequency',
        label: 'L{RecurrenceEditor.Frequency}',
        weight: 10,
        onChange: 'up.onFrequencyFieldChange',
        addNone: 'up.addNone'
      },
      intervalField: {
        type: 'numberfield',
        weight: 15,
        name: 'interval',
        label: 'L{RecurrenceEditor.Every}',
        min: 1,
        required: true
      },
      daysButtonField: {
        type: 'recurrencedaysbuttongroup',
        weight: 20,
        name: 'days',
        forFrequency: 'WEEKLY'
      },
      // the radio button enabling "monthDaysButtonField" in MONTHLY mode
      monthDaysRadioField: {
        type: 'checkbox',
        weight: 30,
        toggleGroup: 'radio',
        forFrequency: 'MONTHLY',
        label: 'L{RecurrenceEditor.Each}',
        checked: true,
        onChange: 'up.onMonthDaysRadioFieldChange'
      },
      monthDaysButtonField: {
        type: 'recurrencemonthdaysbuttongroup',
        weight: 40,
        name: 'monthDays',
        forFrequency: 'MONTHLY'
      },
      monthsButtonField: {
        type: 'recurrencemonthsbuttongroup',
        weight: 50,
        name: 'months',
        forFrequency: 'YEARLY'
      },
      // the radio button enabling positions & days combos in MONTHLY & YEARLY modes
      positionAndDayRadioField: {
        type: 'checkbox',
        weight: 60,
        toggleGroup: 'radio',
        forFrequency: 'MONTHLY|YEARLY',
        label: 'L{RecurrenceEditor.On the}',
        onChange: 'up.onPositionAndDayRadioFieldChange'
      },
      positionsCombo: {
        type: 'recurrencepositionscombo',
        weight: 80,
        name: 'positions',
        forFrequency: 'MONTHLY|YEARLY'
      },
      daysCombo: {
        type: 'recurrencedayscombo',
        weight: 90,
        name: 'days',
        forFrequency: 'MONTHLY|YEARLY',
        flex: 1
      },
      stopRecurrenceField: {
        type: 'recurrencestopconditioncombo',
        weight: 100,
        label: 'L{RecurrenceEditor.End repeat}',
        onChange: 'up.onStopRecurrenceFieldChange'
      },
      countField: {
        type: 'numberfield',
        weight: 110,
        name: 'count',
        min: 2,
        required: true,
        disabled: true,
        label: ' '
      },
      endDateField: {
        type: 'datefield',
        weight: 120,
        name: 'endDate',
        hidden: true,
        disabled: true,
        label: ' ',
        required: true
      }
    }
  };
  setupWidgetConfig(widgetConfig) {
    // All our inputs must be mutated using triggers and touch gestures on mobile
    if (BrowserHelper.isMobile && !('editable' in widgetConfig)) {
      widgetConfig.editable = false;
    }
    return super.setupWidgetConfig(...arguments);
  }
  updateRecord(record) {
    super.updateRecord(record);
    const me = this,
      {
        frequencyField,
        daysButtonField,
        monthDaysButtonField,
        monthsButtonField,
        monthDaysRadioField,
        positionAndDayRadioField,
        stopRecurrenceField
      } = me.widgetMap;
    if (record) {
      const event = record.timeSpan,
        startDate = event === null || event === void 0 ? void 0 : event.startDate;
      // some fields default values are calculated based on event "startDate" value
      if (startDate) {
        // if no "days" value provided
        if (!record.days || !record.days.length) {
          daysButtonField.value = [RecurrenceDayRuleEncoder.encodeDay(startDate.getDay())];
        }
        // if no "monthDays" value provided
        if (!record.monthDays || !record.monthDays.length) {
          monthDaysButtonField.value = startDate.getDate();
        }
        // if no "months" value provided
        if (!record.months || !record.months.length) {
          monthsButtonField.value = startDate.getMonth() + 1;
        }
      }
      // if the record has both "days" & "positions" fields set check "On the" checkbox
      if (record.days && record.positions) {
        positionAndDayRadioField.check();
        if (!me.isPainted) {
          monthDaysRadioField.uncheck();
        }
      } else {
        monthDaysRadioField.check();
        if (!me.isPainted) {
          positionAndDayRadioField.uncheck();
        }
      }
      stopRecurrenceField.recurrence = record;
    } else {
      frequencyField.value = 'NONE';
    }
  }
  /**
   * Updates the provided recurrence model with the contained form data.
   * If recurrence model is not provided updates the last loaded recurrence model.
   * @internal
   */
  syncEventRecord(recurrence) {
    // get values relevant to the RecurrenceModel (from enabled fields only)
    const values = this.getValues(w => w.name in recurrence && !w.disabled);
    // Disabled field does not contribute to values, clear manually
    if (!('endDate' in values)) {
      values.endDate = null;
    }
    if (!('count' in values)) {
      values.count = null;
    }
    recurrence.set(values);
  }
  toggleStopFields() {
    const me = this,
      {
        countField,
        endDateField
      } = me.widgetMap;
    switch (me.widgetMap.stopRecurrenceField.value) {
      case 'count':
        countField.show();
        countField.enable();
        endDateField.hide();
        endDateField.disable();
        break;
      case 'date':
        countField.hide();
        countField.disable();
        endDateField.show();
        endDateField.enable();
        break;
      default:
        countField.hide();
        endDateField.hide();
        countField.disable();
        endDateField.disable();
    }
  }
  onMonthDaysRadioFieldChange({
    checked
  }) {
    const {
      monthDaysButtonField
    } = this.widgetMap;
    monthDaysButtonField.disabled = !checked || !this.isWidgetAvailableForFrequency(monthDaysButtonField);
  }
  onPositionAndDayRadioFieldChange({
    checked
  }) {
    const {
      daysCombo,
      positionsCombo
    } = this.widgetMap;
    // toggle day & positions combos
    daysCombo.disabled = positionsCombo.disabled = !checked || !this.isWidgetAvailableForFrequency(daysCombo);
  }
  onStopRecurrenceFieldChange() {
    this.toggleStopFields();
  }
  isWidgetAvailableForFrequency(widget, frequency = this.widgetMap.frequencyField.value) {
    return !widget.forFrequency || widget.forFrequency.includes(frequency);
  }
  onFrequencyFieldChange({
    value,
    oldValue,
    valid
  }) {
    const me = this,
      items = me.queryAll(w => 'forFrequency' in w),
      {
        intervalField,
        stopRecurrenceField
      } = me.widgetMap;
    if (valid && value) {
      for (let i = 0; i < items.length; i++) {
        const item = items[i];
        if (me.isWidgetAvailableForFrequency(item, value)) {
          item.show();
          item.enable();
        } else {
          item.hide();
          item.disable();
        }
      }
      // Special handling of NONE
      intervalField.hidden = stopRecurrenceField.hidden = value === 'NONE';
      if (value !== 'NONE') {
        intervalField.hint = me.L(`L{RecurrenceEditor.${value}intervalUnit}`);
      }
      // When a non-recurring record is loaded, intervalField is set to empty. We want it to default to 1 here
      // to not look weird (defaults to 1 on the data layer)
      if (oldValue === 'NONE' && intervalField.value == null) {
        intervalField.value = 1;
      }
      me.toggleFieldsState();
    }
  }
  toggleFieldsState() {
    const me = this,
      {
        widgetMap
      } = me;
    me.onMonthDaysRadioFieldChange({
      checked: widgetMap.monthDaysRadioField.checked
    });
    me.onPositionAndDayRadioFieldChange({
      checked: widgetMap.positionAndDayRadioField.checked
    });
    me.onStopRecurrenceFieldChange();
  }
  updateLocalization() {
    // do extra labels translation (not auto-translated yet)
    const {
      countField,
      intervalField,
      frequencyField
    } = this.widgetMap;
    countField.hint = this.L('L{RecurrenceEditor.time(s)}');
    if (frequencyField.value && frequencyField.value !== 'NONE') {
      intervalField.hint = this.L(`L{RecurrenceEditor.${frequencyField.value}intervalUnit}`);
    }
    super.updateLocalization();
  }
}
// Register this widget type with its Factory
RecurrenceEditorPanel.initClass();
RecurrenceEditorPanel._$name = 'RecurrenceEditorPanel';

/**
 * @module Scheduler/widget/EventColorField
 */
/**
 * Color field widget for editing the EventModel's {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} field.
 * See Schedulers {@link Scheduler.view.mixin.TimelineEventRendering#config-eventColor eventColor config} for default
 * available colors.
 *
 * What differs this widget from {@link Core.widget.ColorField} is that this uses the
 * {@link Scheduler.widget.EventColorPicker} as its picker. And also that the {@link #config-name} config is set to
 * `eventColor` per default.
 *
 * {@inlineexample Scheduler/widget/EventColorField.js}
 *
 * This widget may be operated using the keyboard. `ArrowDown` opens the color picker, which itself is keyboard
 * navigable.
 *
 * ```javascript
 * let eventColorField = new EventColorField();
 * ```
 *
 * @extends Core/widget/ColorField
 * @classType eventcolorfield
 * @inputfield
 */
class EventColorField extends ColorField {
  static $name = 'EventColorField';
  static type = 'eventcolorfield';
  static configurable = {
    picker: {
      type: 'eventcolorpicker'
    },
    name: 'eventColor'
  };
}
EventColorField.initClass();
EventColorField._$name = 'EventColorField';

/**
 * @module Scheduler/feature/EventMenu
 */
/**
 * Displays a context menu for events. Items are populated by other features and/or application code.
 *
 * {@inlineexample Scheduler/feature/EventMenu.js}
 *
 * ### Default event menu items
 *
 * Here is the list of menu items provided by the feature and populated by the other features:
 *
 * | Reference       | Text           | Weight | Feature                                  | Description                                                       |
 * |-----------------|----------------|--------|------------------------------------------|-------------------------------------------------------------------|
 * | `editEvent`     | Edit event     | 100    | {@link Scheduler/feature/EventEdit}      | Edit in the event editor. Hidden when read-only                   |
 * | `copyEvent`     | Copy event     | 110    | {@link Scheduler/feature/EventCopyPaste} | Copy event or assignment. Hidden when read-only                   |
 * | `cutEvent `     | Cut event      | 120    | {@link Scheduler/feature/EventCopyPaste} | Cut event or assignment. Hidden when read-only                    |
 * | `deleteEvent`   | Delete event   | 200    | *This feature*                           | Remove event. Hidden when read-only                               |
 * | `unassignEvent` | Unassign event | 300    | *This feature*                           | Unassign event. Hidden when read-only, shown for multi-assignment |
 * | `splitEvent`    | Split event    | 650    | *Scheduler Pro only*                     | Split an event into two segments at the mouse position            |
 * | `renameSegment` | Rename segment | 660    | *Scheduler Pro only*                     | Show an inline editor to rename the segment                       |
 * | `eventColor` ¹  | Color          | 400    | *This feature*                           | Choose background color for the event bar                         |
 *
 * **¹** Set {@link Scheduler.view.SchedulerBase#config-showEventColorPickers} to `true` to enable this item
 *
 * ### Customizing the menu items
 *
 * The menu items in the Event menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra items for all events:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 extraItem : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem({eventRecord}) {
 *                         eventRecord.flagged = true;
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Remove existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 deleteEvent   : false,
 *                 unassignEvent : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Customize existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 deleteEvent : {
 *                     text : 'Delete booking'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Manipulate existing items for all events or specific events:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             // Process items before menu is shown
 *             processItems({eventRecord, items}) {
 *                  // Push an extra item for conferences
 *                  if (eventRecord.type === 'conference') {
 *                      items.showSessionItem = {
 *                          text : 'Show sessions',
 *                          onItem({eventRecord}) {
 *                              // ...
 *                          }
 *                      };
 *                  }
 *
 *                  // Do not show menu for secret events
 *                  if (eventRecord.type === 'secret') {
 *                      return false;
 *                  }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Note that the {@link #property-menuContext} is applied to the Menu's `item` event, so your `onItem`
 * handler's single event parameter also contains the following properties:
 *
 * - **source** The {@link Scheduler.view.Scheduler} who's UI was right clicked.
 * - **targetElement** The element right clicked on.
 * - **eventRecord** The {@link Scheduler.model.EventModel event record} clicked on.
 * - **resourceRecord** The {@link Scheduler.model.ResourceModel resource record} clicked on.
 * - **assignmentRecord** The {@link Scheduler.model.AssignmentModel assignment record} clicked on.
 *
 * Full information of the menu customization can be found in the "Customizing the Event menu, the Schedule menu, and the TimeAxisHeader menu" guide.
 *
 * This feature is **enabled** by default
 *
 * @extends Scheduler/feature/base/TimeSpanMenuBase
 * @demo Scheduler/eventmenu
 * @classtype eventMenu
 * @feature
 */
class EventMenu extends TimeSpanMenuBase {
  //region Config
  static get $name() {
    return 'EventMenu';
  }
  /**
   * @member {Object} menuContext
   * An informational object containing contextual information about the last activation
   * of the context menu. The base properties are listed below.
   * @property {Event} menuContext.domEvent The initiating event.
   * @property {Event} menuContext.event DEPRECATED: The initiating event.
   * @property {Number[]} menuContext.point The client `X` and `Y` position of the initiating event.
   * @property {HTMLElement} menuContext.targetElement The target to which the menu is being applied.
   * @property {Object<String,MenuItemConfig>} menuContext.items The context menu **configuration** items.
   * @property {Core.data.Model[]} menuContext.selection The record selection in the client (Grid, Scheduler, Gantt or Calendar).
   * @property {Scheduler.model.EventModel} menuContext.eventRecord The event record clicked on.
   * @property {Scheduler.model.ResourceModel} menuContext.resourceRecord The resource record clicked on.
   * @property {Scheduler.model.AssignmentModel} menuContext.assignmentRecord The assignment record clicked on.
   * @readonly
   */
  static get configurable() {
    return {
      /**
       * A function called before displaying the menu that allows manipulations of its items.
       * Returning `false` from this function prevents the menu being shown.
       *
       * ```javascript
       * features         : {
       *    eventMenu : {
       *         processItems({ items, eventRecord, assignmentRecord, resourceRecord }) {
       *             // Add or hide existing items here as needed
       *             items.myAction = {
       *                 text   : 'Cool action',
       *                 icon   : 'b-fa b-fa-fw b-fa-ban',
       *                 onItem : () => console.log(`Clicked ${eventRecord.name}`),
       *                 weight : 1000 // Move to end
       *             };
       *
       *            if (!eventRecord.allowDelete) {
       *                 items.deleteEvent.hidden = true;
       *             }
       *         }
       *     }
       * },
       * ```
       * @param {Object} context An object with information about the menu being shown
       * @param {Scheduler.model.EventModel} context.eventRecord The record representing the current event
       * @param {Scheduler.model.ResourceModel} context.resourceRecord The record representing the current resource
       * @param {Scheduler.model.AssignmentModel} context.assignmentRecord The assignment record
       * @param {Object<String,MenuItemConfig>} context.items An object containing the {@link Core.widget.MenuItem menu item} configs keyed by their id
       * @param {Event} context.event The DOM event object that triggered the show
       * @config {Function}
       * @preventable
       */
      processItems: null,
      type: 'event'
      /**
       * This is a preconfigured set of items used to create the default context menu.
       *
       * The `items` provided by this feature are listed below. These are the property names which you may
       * configure:
       *
       * - `deleteEvent` Deletes the context event.
       * - `unassignEvent` Unassigns the context event from the current resource (only added when multi assignment is used).
       *
       * To remove existing items, set corresponding keys `null`:
       *
       * ```javascript
       * const scheduler = new Scheduler({
       *     features : {
       *         eventMenu : {
       *             items : {
       *                 deleteEvent   : null,
       *                 unassignEvent : null
       *             }
       *         }
       *     }
       * });
       * ```
       *
       * See the feature config in the above example for details.
       *
       * @config {Object<String,MenuItemConfig|Boolean|null>} items
       */
    };
  }

  static get pluginConfig() {
    const config = super.pluginConfig;
    config.chain.push('populateEventMenu');
    return config;
  }
  //endregion
  //region Events
  /**
   * This event fires on the owning Scheduler before the context menu is shown for an event. Allows manipulation of the items
   * to show in the same way as in `processItems`. Returning `false` from a listener prevents the menu from
   * being shown.
   * @event eventMenuBeforeShow
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source
   * @param {Object<String,MenuItemConfig>} items Menu item configs
   * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   * @param {MouseEvent} [event] Pointer event which triggered the context menu (if any)
   */
  /**
   * This event fires on the owning Scheduler when an item is selected in the context menu.
   * @event eventMenuItem
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Core.widget.MenuItem} item
   * @param {Scheduler.model.EventModel} eventRecord
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   */
  /**
   * This event fires on the owning Scheduler after showing the context menu for an event
   * @event eventMenuShow
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Core.widget.Menu} menu The menu
   * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   */
  //endregion
  get resourceStore() {
    // In horizontal mode, we use store (might be a display store), in vertical & calendar we use resourceStore
    return this.client.isHorizontal ? this.client.store : this.client.resourceStore;
  }
  getDataFromEvent(event) {
    var _ref;
    const data = super.getDataFromEvent(event),
      eventElement = data.targetElement,
      {
        client
      } = this,
      eventRecord = client.resolveEventRecord(eventElement),
      // For vertical mode the resource must be resolved from the event
      resourceRecord = eventRecord && ((_ref = client.resolveResourceRecord(eventElement) || this.resourceStore.last) === null || _ref === void 0 ? void 0 : _ref.$original),
      assignmentRecord = eventRecord && client.resolveAssignmentRecord(eventElement);
    return Object.assign(data, {
      eventElement,
      eventRecord,
      resourceRecord,
      assignmentRecord
    });
  }
  getTargetElementFromEvent({
    target
  }) {
    return target.closest(this.client.eventSelector) || target;
  }
  shouldShowMenu(eventParams) {
    return eventParams.eventRecord;
  }
  /**
   * Shows context menu for the provided event. If record is not rendered (outside of time span/filtered)
   * menu won't appear.
   * @param {Scheduler.model.EventModel} eventRecord Event record to show menu for.
   * @param {Object} [options]
   * @param {HTMLElement} options.targetElement Element to align context menu to.
   * @param {MouseEvent} options.event Browser event.
   * If provided menu will be aligned according to clientX/clientY coordinates.
   * If omitted, context menu will be centered to event element.
   */
  showContextMenuFor(eventRecord, {
    targetElement,
    event
  } = {}) {
    if (this.disabled) {
      return;
    }
    if (!targetElement) {
      targetElement = this.getElementFromRecord(eventRecord);
      // If record is not rendered, do nothing
      if (!targetElement) {
        return;
      }
    }
    DomHelper.triggerMouseEvent(targetElement, this.tiggerEvent);
  }
  getElementFromRecord(record) {
    return this.client.getElementsFromEventRecord(record)[0];
  }
  populateEventMenu({
    items,
    eventRecord,
    assignmentRecord
  }) {
    const {
      client
    } = this;
    items.deleteEvent = {
      disabled: eventRecord.readOnly || (assignmentRecord === null || assignmentRecord === void 0 ? void 0 : assignmentRecord.readOnly),
      hidden: client.readOnly
    };
    items.unassignEvent = {
      disabled: eventRecord.readOnly || (assignmentRecord === null || assignmentRecord === void 0 ? void 0 : assignmentRecord.readOnly),
      hidden: client.readOnly || client.eventStore.usesSingleAssignment
    };
    if (client.showEventColorPickers || client.showTaskColorPickers) {
      items.eventColor = {
        disabled: eventRecord.readOnly,
        hidden: client.readOnly
      };
    } else {
      items.eventColor = {
        hidden: true
      };
    }
  }
  populateItemsWithData({
    items,
    eventRecord
  }) {
    var _items$eventColor;
    super.populateItemsWithData(...arguments);
    const {
      client
    } = this;
    if ((client.showEventColorPickers || client.isSchedulerPro && client.showTaskColorPickers) && (_items$eventColor = items.eventColor) !== null && _items$eventColor !== void 0 && _items$eventColor.menu) {
      Objects.merge(items.eventColor.menu.colorMenu, {
        value: eventRecord.eventColor,
        record: eventRecord
      });
    }
  }
  // This generates the fixed, unchanging part of the items and is only called once
  // to generate the baseItems of the feature.
  // The dynamic parts which are set by populateEventMenu have this merged into them.
  changeItems(items) {
    const {
      client
    } = this;
    return Objects.merge({
      deleteEvent: {
        text: 'L{SchedulerBase.Delete event}',
        icon: 'b-icon b-icon-trash',
        weight: 200,
        onItem({
          menu,
          eventRecord
        }) {
          var _menu$focusInEvent;
          // We must synchronously push focus back into the menu's triggering
          // event so that our beforeRemove handlers can move focus onwards
          // to the closest remaining event.
          // Otherwise, the menu's default hide processing on hide will attempt
          // to move focus back to the menu's triggering event which will
          // by then have been deleted.
          const revertTarget = (_menu$focusInEvent = menu.focusInEvent) === null || _menu$focusInEvent === void 0 ? void 0 : _menu$focusInEvent.relatedTarget;
          if (revertTarget) {
            revertTarget.focus();
            client.navigator.activeItem = revertTarget;
          }
          client.removeEvents(client.isEventSelected(eventRecord) ? client.selectedEvents : [eventRecord]);
        }
      },
      unassignEvent: {
        text: 'L{SchedulerBase.Unassign event}',
        icon: 'b-icon b-icon-unassign',
        weight: 300,
        onItem({
          menu,
          eventRecord,
          resourceRecord
        }) {
          var _menu$focusInEvent2;
          // We must synchronously push focus back into the menu's triggering
          // event so that our beforeRemove handlers can move focus onwards
          // to the closest remaining event.
          // Otherwise, the menu's default hide processing on hide will attempt
          // to move focus back to the menu's triggering event which will
          // by then have been deleted.
          const revertTarget = (_menu$focusInEvent2 = menu.focusInEvent) === null || _menu$focusInEvent2 === void 0 ? void 0 : _menu$focusInEvent2.relatedTarget;
          if (revertTarget) {
            revertTarget.focus();
            client.navigator.activeItem = revertTarget;
          }
          if (client.isEventSelected(eventRecord)) {
            client.assignmentStore.remove(client.selectedAssignments);
          } else {
            eventRecord.unassign(resourceRecord);
          }
        }
      },
      eventColor: {
        text: 'L{SchedulerBase.color}',
        icon: 'b-icon b-icon-palette',
        separator: true,
        menu: {
          colorMenu: {
            type: 'eventcolorpicker'
          }
        }
      }
    }, items);
  }
}
EventMenu.featureClass = '';
EventMenu._$name = 'EventMenu';
GridFeatureManager.registerFeature(EventMenu, true, 'Scheduler');
GridFeatureManager.registerFeature(EventMenu, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/ScheduleMenu
 */
/**
 * Displays a context menu for empty parts of the schedule. Items are populated in the first place
 * by configurations of this Feature, then by other features and/or application code.
 *
 * ### Default scheduler zone menu items
 *
 * The Scheduler menu feature provides only one item:
 *
 * | Reference              | Text         | Weight | Feature                                  | Description                                                           |
 * |------------------------|--------------|--------|------------------------------------------|-----------------------------------------------------------------------|
 * | `addEvent`             | Add event    | 100    | *This feature*                           | Add new event at the target time and resource. Hidden when read-only  |
 * | `pasteEvent`           | Paste event  | 110    | {@link Scheduler/feature/EventCopyPaste} | Paste event at the target time and resource. Hidden when is read-only |
 * | `splitSchedule`        | Split        | 200    | {@link Scheduler/feature/Split}          | Shows the "Split schedule" sub-menu                                   |
 * | \> `splitHorizontally` | Horizontally | 100    | {@link Scheduler/feature/Split}          | Split horizontally                                                    |
 * | \> `splitVertically `  | Vertically   | 200    | {@link Scheduler/feature/Split}          | Split vertically                                                      |
 * | \> `splitBoth`         | Both         | 300    | {@link Scheduler/feature/Split}          | Split both ways                                                       |
 * | `unsplitSchedule`      | Split        | 210    | {@link Scheduler/feature/Split}          | Unsplit a previously split schedule                                   |
 *
 * ### Customizing the menu items
 *
 * The menu items in the Scheduler menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * Add extra item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 extraItem : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem({date, resourceRecord, items}) {
 *                         // Custom date based action
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Remove existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 addEvent : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Customize existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             items : {
 *                 addEvent : {
 *                     text : 'Create new booking'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Manipulate existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleMenu : {
 *             // Process items before menu is shown
 *             processItems({date, resourceRecord, items}) {
 *                  // Add an extra item for ancient times
 *                  if (date < new Date(2018, 11, 17)) {
 *                      items.modernize = {
 *                          text : 'Modernize',
 *                          ontItem({date}) {
 *                              // Custom date based action
 *                          }
 *                      };
 *                  }
 *
 *                  // Do not show menu for Sundays
 *                  if (date.getDay() === 0) {
 *                      return false;
 *                  }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Full information of the menu customization can be found in the "Customizing the Event menu, the Schedule menu, and the TimeAxisHeader menu" guide.
 *
 * This feature is **enabled** by default
 *
 * @demo Scheduler/basic
 * @extends Scheduler/feature/base/TimeSpanMenuBase
 * @classtype scheduleMenu
 * @feature
 */
class ScheduleMenu extends TimeSpanMenuBase {
  //region Config
  static get $name() {
    return 'ScheduleMenu';
  }
  static get defaultConfig() {
    return {
      type: 'schedule',
      /**
       * This is a preconfigured set of items used to create the default context menu.
       *
       * The `items` provided by this feature are listed below. These are the predefined property names which you may
       * configure:
       *
       * - `addEvent` Add an event for at the resource and time indicated by the `contextmenu` event.
       *
       * To remove existing items, set corresponding keys `null`:
       *
       * ```javascript
       * const scheduler = new Scheduler({
       *     features : {
       *         scheduleMenu : {
       *             items : {
       *                 addEvent : null
       *             }
       *         }
       *     }
       * });
       * ```
       *
       * @config {Object<String,MenuItemConfig|Boolean|null>} items
       */
      items: null,
      /**
       * A function called before displaying the menu that allows manipulations of its items.
       * Returning `false` from this function prevents the menu being shown.
       *
       * ```javascript
       * features         : {
       *    scheduleMenu : {
       *         processItems({ items, date, resourceRecord }) {
       *            // Add or hide existing items here as needed
       *            items.myAction = {
       *                text   : 'Cool action',
       *                icon   : 'b-fa b-fa-cat',
       *                onItem : () => console.log(`Clicked on ${resourceRecord.name} at ${date}`),
       *                weight : 1000 // Move to end
       *            };
       *
       *            if (!resourceRecord.allowAdd) {
       *                items.addEvent.hidden = true;
       *            }
       *        }
       *    }
       * },
       * ```
       * @param {Object} context An object with information about the menu being shown
       * @param {Scheduler.model.ResourceModel} context.resourceRecord The record representing the current resource
       * @param {Date} context.date The clicked date
       * @param {Object<String,MenuItemConfig>} context.items An object containing the
       * {@link Core.widget.MenuItem menu item} configs keyed by their id
       * @param {Event} context.event The DOM event object that triggered the show
       * @config {Function}
       * @preventable
       */
      processItems: null
    };
  }
  static get pluginConfig() {
    const config = super.pluginConfig;
    config.chain.push('populateScheduleMenu');
    return config;
  }
  //endregion
  //region Events
  /**
   * This event fires on the owning Scheduler before the context menu is shown for an event. Allows manipulation of the items
   * to show in the same way as in `processItems`. Returning `false` from a listener prevents the menu from
   * being shown.
   * @event scheduleMenuBeforeShow
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source
   * @param {Object<String,MenuItemConfig>} items Menu item configs
   * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   */
  /**
   * This event fires on the owning Scheduler when an item is selected in the context menu.
   * @event scheduleMenuItem
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Core.widget.MenuItem} item
   * @param {Scheduler.model.EventModel} eventRecord
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   */
  /**
   * This event fires on the owning Scheduler after showing the context menu for an event
   * @event scheduleMenuShow
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Core.widget.Menu} menu The menu
   * @param {Scheduler.model.EventModel} eventRecord Event record for which the menu was triggered
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
   * @param {Scheduler.model.AssignmentModel} assignmentRecord Assignment record, if assignments are used
   * @param {HTMLElement} eventElement
   */
  //endregion
  shouldShowMenu(eventParams) {
    const {
        client
      } = this,
      {
        targetElement,
        resourceRecord
      } = eventParams,
      isTimeAxisColumn = client.timeAxisSubGridElement.contains(targetElement);
    return !targetElement.closest(client.eventSelector) && isTimeAxisColumn && !(resourceRecord && resourceRecord.isSpecialRow);
  }
  getDataFromEvent(event) {
    // Process event if it wasn't yet processed
    if (DomHelper.isDOMEvent(event)) {
      var _client$getCellDataFr, _client$getDateFromDo;
      const {
          client
        } = this,
        cellData = (_client$getCellDataFr = client.getCellDataFromEvent) === null || _client$getCellDataFr === void 0 ? void 0 : _client$getCellDataFr.call(client, event),
        date = (_client$getDateFromDo = client.getDateFromDomEvent) === null || _client$getDateFromDo === void 0 ? void 0 : _client$getDateFromDo.call(client, event, 'floor'),
        // For vertical mode the resource must be resolved from the event
        resourceRecord = client.resolveResourceRecord(event) || client.isVertical && client.resourceStore.last;
      return ObjectHelper.assign(super.getDataFromEvent(event), cellData, {
        date,
        resourceRecord
      });
    }
    return event;
  }
  populateScheduleMenu({
    items,
    resourceRecord,
    date
  }) {
    const {
      client
    } = this;
    // Menu can work for ResourceHistogram which doesn't have event store
    if (!client.readOnly && client.eventStore) {
      items.addEvent = {
        text: 'L{SchedulerBase.Add event}',
        icon: 'b-icon b-icon-add',
        disabled: !resourceRecord || resourceRecord.readOnly || !resourceRecord.isWorkingTime(date),
        weight: 100,
        onItem() {
          client.createEvent(date, resourceRecord, client.getRowFor(resourceRecord));
        }
      };
    }
  }
}
ScheduleMenu.featureClass = '';
ScheduleMenu._$name = 'ScheduleMenu';
GridFeatureManager.registerFeature(ScheduleMenu, true, 'Scheduler');

export { AbstractCrudManager, AttachToProjectMixin, ClockTemplate, CrudManager, CurrentConfig, Describable, EventColorField, EventColorPicker, EventMenu, HorizontalLayoutPack, HorizontalLayoutStack, HorizontalRendering, HorizontalTimeAxis, PackMixin, PresetStore, ProjectConsumer, RecurrenceConfirmationPopup, RecurrenceDaysButtonGroup, RecurrenceDaysCombo, RecurrenceEditorPanel, RecurrenceFrequencyCombo, RecurrenceMonthDaysButtonGroup, RecurrenceMonthsButtonGroup, RecurrencePositionsCombo, RecurrenceStopConditionCombo, RecurringEvents, ResourceHeader, ScheduleMenu, SchedulerBase, SchedulerDom, SchedulerDomEvents, SchedulerEventNavigation, SchedulerEventRendering, SchedulerEventSelection, SchedulerRegions, SchedulerResourceRendering, SchedulerScroll, SchedulerState, SchedulerStores, TaskEditStm, TimeAxis, TimeAxisColumn, TimeAxisSubGrid, TimeAxisViewModel, TimeSpanMenuBase, TimelineBase, TimelineDateMapper, TimelineDomEvents, TimelineEventRendering, TimelineScroll, TimelineState, TimelineViewPresets, TimelineZoomable, TransactionalFeatureMixin, VerticalRendering, VerticalTimeAxisColumn, ViewPreset, pm };
//# sourceMappingURL=ScheduleMenu.js.map
