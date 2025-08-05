/*!
 *
 * Bryntum Gantt 5.5.0
 *
 * Copyright(c) 2023 Bryntum AB
 * https://bryntum.com/contact
 * https://bryntum.com/license
 *
 */
import { stripDuplicates, CalendarCache, IntervalCache, TimeSpan, DependencyBaseModel, DependencyModel } from './CrudManagerView.js';
import { ColumnStore, GridFeatureManager, HeaderMenu } from './GridBase.js';
import { NumberColumn } from './Tree.js';
import { ObjectHelper, Duration, DateHelper, InstancePlugin, Objects, Tooltip, Rectangle, DomHelper, Widget, StringHelper, EventHelper, BrowserHelper, VersionHelper, Base as Base$1, IdHelper, Delayable, DomSync, ArrayHelper, WalkHelper } from './Editor.js';
import { DragHelper, ResizeHelper } from './MessageDialog.js';
import { ClockTemplate, AttachToProjectMixin, TaskEditStm } from './ScheduleMenu.js';
import { Draggable, Droppable } from './AvatarRendering.js';
import './Slider.js';

//---------------------------------------------------------------------------------------------------------------------
/**
 * This is a base class, providing the type-safe static constructor [[new]]. This is very convenient when using
 * [[Mixin|mixins]], as mixins can not have types in the constructors.
 */
class Base {
  /**
   * This method applies its 1st argument (if any) to the current instance using `Object.assign()`.
   *
   * Supposed to be overridden in the subclasses to customize the instance creation process.
   *
   * @param props
   */
  initialize(props) {
    props && Object.assign(this, props);
  }
  /**
   * This is a type-safe static constructor method, accepting a single argument, with the object, corresponding to the
   * class properties. It will generate a compilation error, if unknown property is provided.
   *
   * For example:
   *
   * ```ts
   * class MyClass extends Base {
   *     prop     : string
   * }
   *
   * const instance : MyClass = MyClass.new({ prop : 'prop', wrong : 11 })
   * ```
   *
   * will produce:
   *
   * ```plaintext
   * TS2345: Argument of type '{ prop: string; wrong: number; }' is not assignable to parameter of type 'Partial<MyClass>'.
   * Object literal may only specify known properties, and 'wrong' does not exist in type 'Partial<MyClass>'
   * ```
   *
   * The only thing this constructor does is create an instance and call the [[initialize]] method on it, forwarding
   * the first argument. The customization of instance is supposed to be performed in that method.
   *
   * @param props
   */
  static new(props) {
    const instance = new this();
    instance.initialize(props);
    return instance;
  }
}

class CalendarCacheIntervalMultiple {
  constructor(config) {
    this.intervalGroups = [];
    config && Object.assign(this, config);
  }
  combineWith(interval) {
    const copy = this.intervalGroups.slice();
    copy.push([interval.calendar, interval]);
    return new CalendarCacheIntervalMultiple({
      intervalGroups: copy
    });
  }
  getIsWorkingForEvery() {
    if (this.isWorkingForEvery != null) return this.isWorkingForEvery;
    for (let [_calendar, intervals] of this.getGroups()) {
      if (!intervals[0].isWorking) return this.isWorkingForEvery = false;
    }
    return this.isWorkingForEvery = true;
  }
  getIsWorkingForSome() {
    if (this.isWorkingForSome != null) return this.isWorkingForSome;
    for (let [_calendar, intervals] of this.getGroups()) {
      if (intervals[0].isWorking) return this.isWorkingForSome = true;
    }
    return this.isWorkingForSome = false;
  }
  getCalendars() {
    this.getGroups();
    return this.calendars;
  }
  isCalendarWorking(calendar) {
    return this.getCalendarsWorkStatus().get(calendar);
  }
  getCalendarsWorkStatus() {
    if (this.calendarsWorkStatus) return this.calendarsWorkStatus;
    const res = new Map();
    for (let [calendar, intervals] of this.getGroups()) {
      res.set(calendar, intervals[0].isWorking);
    }
    return this.calendarsWorkStatus = res;
  }
  getCalendarsWorking() {
    if (this.calendarsWorking) return this.calendarsWorking;
    const calendars = [];
    for (let [calendar, intervals] of this.getGroups()) {
      if (intervals[0].isWorking) calendars.push(calendar);
    }
    return this.calendarsWorking = calendars;
  }
  getCalendarsNonWorking() {
    if (this.calendarsNonWorking) return this.calendarsNonWorking;
    const calendars = [];
    for (let [calendar, intervals] of this.getGroups()) {
      if (!intervals[0].isWorking) calendars.push(calendar);
    }
    return this.calendarsNonWorking = calendars;
  }
  getGroups() {
    if (this.intervalsByCalendar) return this.intervalsByCalendar;
    const calendars = this.calendars = [];
    const intervalsByCalendar = new Map();
    this.intervalGroups.forEach(([calendar, interval]) => {
      let data = intervalsByCalendar.get(calendar);
      if (!data) {
        calendars.push(calendar);
        data = [];
        intervalsByCalendar.set(calendar, data);
      }
      data.push.apply(data, interval.intervals);
    });
    intervalsByCalendar.forEach((intervals, calendar) => {
      const unique = stripDuplicates(intervals);
      unique.sort(
      // sort in decreasing order
      (interval1, interval2) => interval2.getPriorityField() - interval1.getPriorityField());
      intervalsByCalendar.set(calendar, unique);
    });
    return this.intervalsByCalendar = intervalsByCalendar;
  }
}

/**
 * The calendar cache for combination of multiple calendars
 */
class CalendarCacheMultiple extends CalendarCache {
  constructor(config) {
    super(config);
    this.calendarCaches = stripDuplicates(this.calendarCaches);
    this.intervalCache = new IntervalCache({
      emptyInterval: new CalendarCacheIntervalMultiple(),
      combineIntervalsFn: (interval1, interval2) => {
        return interval1.combineWith(interval2);
      }
    });
  }
  fillCache(startDate, endDate) {
    this.calendarCaches.forEach(calendarCache => {
      calendarCache.fillCache(startDate, endDate);
      this.includeWrappingRangeFrom(calendarCache, startDate, endDate);
    });
  }
}
const COMBINED_CALENDARS_CACHE = new Map();
const combineCalendars = calendars => {
  const uniqueOnly = stripDuplicates(calendars);
  if (uniqueOnly.length === 0) throw new Error("No calendars to combine");
  uniqueOnly.sort((calendar1, calendar2) => {
    if (calendar1.internalId < calendar2.internalId) return -1;else return 1;
  });
  const hash = uniqueOnly.map(calendar => calendar.internalId + '/').join('');
  const versionsHash = uniqueOnly.map(calendar => calendar.version + '/').join('');
  let cached = COMBINED_CALENDARS_CACHE.get(hash);
  let res;
  if (cached && cached.versionsHash === versionsHash) res = cached.cache;else {
    res = new CalendarCacheMultiple({
      calendarCaches: uniqueOnly.map(calendar => calendar.calendarCache)
    });
    // COMBINED_CALENDARS_CACHE.set(hash, {
    //     versionsHash    : versionsHash,
    //     cache           : res
    // })
  }

  return res;
};

/**
 * @module Scheduler/column/DurationColumn
 */
/**
 * A column showing the task {@link Scheduler/model/TimeSpan#field-fullDuration duration}. Please note, this column
 * is preconfigured and expects its field to be of the {@link Core.data.Duration} type.
 *
 * The default editor is a {@link Core.widget.DurationField}. It parses time units, so you can enter "4d"
 * indicating 4 days duration, or "4h" indicating 4 hours, etc. The numeric magnitude can be either an integer or a
 * float value. Both "," and "." are valid decimal separators. For example, you can enter "4.5d" indicating 4.5 days
 * duration, or "4,5h" indicating 4.5 hours.
 *
 * {@inlineexample Scheduler/column/DurationColumn.js}
 * @extends Grid/column/NumberColumn
 * @classType duration
 * @column
 */
class DurationColumn extends NumberColumn {
  compositeField = true;
  //region Config
  static get $name() {
    return 'DurationColumn';
  }
  static get type() {
    return 'duration';
  }
  static get isGanttColumn() {
    return true;
  }
  static get fields() {
    return [
    /**
     * Precision of displayed duration, defaults to use {@link Scheduler.view.Scheduler#config-durationDisplayPrecision}.
     * Specify an integer value to override that setting, or `false` to use raw value
     * @config {Number|Boolean} decimalPrecision
     */
    {
      name: 'decimalPrecision',
      defaultValue: 1
    }];
  }
  static get defaults() {
    return {
      /**
       * Min value
       * @config {Number}
       */
      min: null,
      /**
       * Max value
       * @config {Number}
       */
      max: null,
      /**
       * Step size for spin button clicks.
       * @member {Number} step
       */
      /**
       * Step size for spin button clicks. Also used when pressing up/down keys in the field.
       * @config {Number}
       * @default
       */
      step: 1,
      /**
       * Large step size, defaults to 10 * `step`. Applied when pressing SHIFT and stepping either by click or
       * using keyboard.
       * @config {Number}
       * @default 10
       */
      largeStep: 0,
      field: 'fullDuration',
      text: 'L{Duration}',
      instantUpdate: true,
      // Undocumented, used by Filter feature to get type of the filter field
      filterType: 'duration',
      sortable(durationEntity1, durationEntity2) {
        const ms1 = durationEntity1.getValue(this.field),
          ms2 = durationEntity2.getValue(this.field);
        return ms1 - ms2;
      }
    };
  }
  construct() {
    super.construct(...arguments);
    const sortFn = this.sortable;
    this.sortable = (...args) => sortFn.call(this, ...args);
  }
  get defaultEditor() {
    const {
      max,
      min,
      step,
      largeStep
    } = this;
    // Remove any undefined configs, to allow config system to use default values instead
    return ObjectHelper.cleanupProperties({
      type: 'duration',
      name: this.field,
      max,
      min,
      step,
      largeStep
    });
  }
  //endregion
  //region Internal
  get durationUnitField() {
    return `${this.field}Unit`;
  }
  roundValue(duration) {
    const nbrDecimals = typeof this.grid.durationDisplayPrecision === 'number' ? this.grid.durationDisplayPrecision : this.decimalPrecision,
      multiplier = Math.pow(10, nbrDecimals),
      rounded = Math.round(duration * multiplier) / multiplier;
    return rounded;
  }
  formatValue(duration, durationUnit) {
    if (duration instanceof Duration) {
      durationUnit = duration.unit;
      duration = duration.magnitude;
    }
    duration = this.roundValue(duration);
    return duration + ' ' + DateHelper.getLocalizedNameOfUnit(durationUnit, duration !== 1);
  }
  //endregion
  //region Render
  defaultRenderer({
    value,
    record,
    isExport
  }) {
    const type = typeof value,
      durationValue = type === 'number' ? value : value === null || value === void 0 ? void 0 : value.magnitude,
      durationUnit = type === 'number' ? record.getValue(this.durationUnitField) : value === null || value === void 0 ? void 0 : value.unit;
    // in case of bad input (for instance NaN, undefined or NULL value)
    if (typeof durationValue !== 'number') {
      return isExport ? '' : null;
    }
    return this.formatValue(durationValue, durationUnit);
  }
  //endregion
  // Used with CellCopyPaste as fullDuration doesn't work via record.get
  toClipboardString({
    record
  }) {
    return record.getValue(this.field).toString();
  }
  fromClipboardString({
    string,
    record
  }) {
    const duration = DateHelper.parseDuration(string, true, this.durationUnit);
    if (duration && 'magnitude' in duration) {
      return duration;
    }
    return record.fullDuration;
  }
  calculateFillValue({
    value,
    record
  }) {
    return this.fromClipboardString({
      string: value,
      record
    });
  }
}
ColumnStore.registerColumnType(DurationColumn);
DurationColumn._$name = 'DurationColumn';

/**
 * @module Scheduler/feature/base/DragBase
 */
/**
 * Base class for EventDrag (Scheduler) and TaskDrag (Gantt) features. Contains shared code. Not to be used directly.
 *
 * @extends Core/mixin/InstancePlugin
 * @abstract
 */
class DragBase extends InstancePlugin {
  //region Config
  static get defaultConfig() {
    return {
      // documented on Schedulers EventDrag feature and Gantt's TaskDrag
      tooltipTemplate: data => `
                <div class="b-sch-tip-${data.valid ? 'valid' : 'invalid'}">
                    ${data.startClockHtml}
                    ${data.endClockHtml}
                    <div class="b-sch-tip-message">${data.message}</div>
                </div>
            `,
      /**
       * Specifies whether or not to show tooltip while dragging event
       * @config {Boolean}
       * @default
       */
      showTooltip: true,
      /**
       * When enabled, the event being dragged always "snaps" to the exact start date that it will have after drop.
       * @config {Boolean}
       * @default
       */
      showExactDropPosition: false,
      /*
       * The store from which the dragged items are mapped to the UI.
       * In Scheduler's implementation of this base class, this will be
       * an EventStore, in Gantt's implementations, this will be a TaskStore.
       * Because both derive from this base, we must refer to it as this.store.
       * @private
       */
      store: null,
      /**
       * An object used to configure the internal {@link Core.helper.DragHelper} class
       * @config {DragHelperConfig}
       */
      dragHelperConfig: null,
      tooltipCls: 'b-eventdrag-tooltip'
    };
  }
  static get configurable() {
    return {
      /**
       * Set to `false` to allow dragging tasks outside the client Scheduler.
       * Useful when you want to drag tasks between multiple Scheduler instances
       * @config {Boolean}
       * @default
       */
      constrainDragToTimeline: true,
      // documented on Schedulers EventDrag feature, not used for Gantt
      constrainDragToResource: true,
      constrainDragToTimeSlot: false,
      /**
       * Yields the {@link Core.widget.Tooltip} which tracks the event during a drag operation.
       * @member {Core.widget.Tooltip} tip
       */
      /**
       * A config object to allow customization of the {@link Core.widget.Tooltip} which tracks
       * the event during a drag operation.
       * @config {TooltipConfig}
       */
      tip: {
        $config: ['lazy', 'nullify'],
        value: {
          align: {
            align: 'b-t',
            allowTargetOut: true
          },
          autoShow: true,
          updateContentOnMouseMove: true
        }
      },
      /**
       * The `eventDrag`and `taskDrag` events are normally only triggered when the drag operation will lead to a
       * change in date or assignment. By setting this config to `false`, that logic is bypassed to trigger events
       * for each native mouse move event.
       * @prp {Boolean}
       */
      throttleDragEvent: true
    };
  }
  // Plugin configuration. This plugin chains some of the functions in Grid.
  static get pluginConfig() {
    return {
      chain: ['onPaint']
    };
  }
  //endregion
  //region Init
  internalSnapToPosition(snapTo) {
    var _this$snapToPosition;
    const {
      dragData
    } = this;
    (_this$snapToPosition = this.snapToPosition) === null || _this$snapToPosition === void 0 ? void 0 : _this$snapToPosition.call(this, {
      assignmentRecord: dragData.assignmentRecord,
      eventRecord: dragData.eventRecord,
      resourceRecord: dragData.newResource || dragData.resourceRecord,
      startDate: dragData.startDate,
      endDate: dragData.endDate,
      snapTo
    });
  }
  buildDragHelperConfig() {
    const me = this,
      {
        client,
        constrainDragToTimeline,
        constrainDragToResource,
        constrainDragToTimeSlot,
        dragHelperConfig = {}
      } = me,
      {
        timeAxisViewModel,
        isHorizontal
      } = client,
      lockY = isHorizontal ? constrainDragToResource : constrainDragToTimeSlot,
      lockX = isHorizontal ? constrainDragToTimeSlot : constrainDragToResource;
    // If implementer wants to allow users dragging outside the timeline element, setup the internal dropTargetSelector
    if (me.externalDropTargetSelector) {
      dragHelperConfig.dropTargetSelector = `.b-timeaxissubgrid,${me.externalDropTargetSelector}`;
    }
    return Objects.merge({
      name: me.constructor.name,
      // useful when debugging with multiple draggers
      positioning: 'absolute',
      lockX,
      lockY,
      minX: true,
      // Allows dropping with start before time axis
      maxX: true,
      // Allows dropping with end after time axis
      constrain: false,
      cloneTarget: !constrainDragToTimeline,
      // If we clone event dragged bars, we assume ownership upon drop so we can reuse the element and have animations
      removeProxyAfterDrop: false,
      dragWithin: constrainDragToTimeline ? null : document.body,
      hideOriginalElement: true,
      dropTargetSelector: '.b-timelinebase',
      // A CSS class added to drop target while dragging events
      dropTargetCls: me.externalDropTargetSelector ? 'b-drop-target' : '',
      outerElement: client.timeAxisSubGridElement,
      targetSelector: client.eventSelector,
      scrollManager: constrainDragToTimeline ? client.scrollManager : null,
      createProxy: el => me.createProxy(el),
      snapCoordinates: ({
        element,
        newX,
        newY
      }) => {
        const {
          dragData
        } = me;
        // Snapping not supported when dragging outside a scheduler
        if (me.constrainDragToTimeline && !me.constrainDragToTimeSlot && (me.showExactDropPosition || timeAxisViewModel.snap)) {
          const draggedEventRecord = dragData.draggedEntities[0],
            coordinate = me.getCoordinate(draggedEventRecord, element, [newX, newY]),
            snappedDate = timeAxisViewModel.getDateFromPosition(coordinate, 'round'),
            {
              calendar
            } = draggedEventRecord;
          if (!calendar || snappedDate && calendar.isWorkingTime(snappedDate, DateHelper.add(snappedDate, draggedEventRecord.fullDuration))) {
            const snappedPosition = snappedDate && timeAxisViewModel.getPositionFromDate(snappedDate);
            if (snappedDate && snappedDate >= client.startDate && snappedPosition != null) {
              if (isHorizontal) {
                newX = snappedPosition;
              } else {
                newY = snappedPosition;
              }
            }
          }
        }
        const snapTo = {
          x: newX,
          y: newY
        };
        me.internalSnapToPosition(snapTo);
        return snapTo;
      },
      internalListeners: {
        beforedragstart: 'onBeforeDragStart',
        dragstart: 'onDragStart',
        afterdragstart: 'onAfterDragStart',
        drag: 'onDrag',
        drop: 'onDrop',
        abort: 'onDragAbort',
        abortFinalized: 'onDragAbortFinalized',
        reset: 'onDragReset',
        thisObj: me
      }
    }, dragHelperConfig, {
      isElementDraggable: (el, event) => {
        return (!dragHelperConfig || !dragHelperConfig.isElementDraggable || dragHelperConfig.isElementDraggable(el, event)) && me.isElementDraggable(el, event);
      }
    });
  }
  /**
   * Called when scheduler is rendered. Sets up drag and drop and hover tooltip.
   * @private
   */
  onPaint({
    firstPaint
  }) {
    var _me$drag;
    const me = this,
      {
        client
      } = me;
    (_me$drag = me.drag) === null || _me$drag === void 0 ? void 0 : _me$drag.destroy();
    me.drag = DragHelper.new(me.buildDragHelperConfig());
    if (firstPaint) {
      client.rowManager.ion({
        changeTotalHeight: () => {
          var _me$dragData;
          return me.updateYConstraint((_me$dragData = me.dragData) === null || _me$dragData === void 0 ? void 0 : _me$dragData[`${client.scheduledEventName}Record`]);
        },
        thisObj: me
      });
    }
    if (me.showTooltip) {
      me.clockTemplate = new ClockTemplate({
        scheduler: client
      });
    }
  }
  doDestroy() {
    var _this$drag, _this$clockTemplate, _this$tip;
    (_this$drag = this.drag) === null || _this$drag === void 0 ? void 0 : _this$drag.destroy();
    (_this$clockTemplate = this.clockTemplate) === null || _this$clockTemplate === void 0 ? void 0 : _this$clockTemplate.destroy();
    (_this$tip = this.tip) === null || _this$tip === void 0 ? void 0 : _this$tip.destroy();
    super.doDestroy();
  }
  get tipId() {
    return `${this.client.id}-event-drag-tip`;
  }
  changeTip(tip, oldTip) {
    const me = this;
    if (tip) {
      const result = Tooltip.reconfigure(oldTip, Tooltip.mergeConfigs({
        forElement: me.element,
        id: me.tipId,
        getHtml: me.getTipHtml.bind(me),
        cls: me.tooltipCls,
        owner: me.client
      }, tip), {
        owner: me.client,
        defaults: {
          type: 'tooltip'
        }
      });
      result.ion({
        innerHtmlUpdate: 'updateDateIndicator',
        thisObj: me
      });
      return result;
    } else {
      oldTip === null || oldTip === void 0 ? void 0 : oldTip.destroy();
    }
  }
  //endregion
  //region Drag events
  createProxy(element) {
    const proxy = element.cloneNode(true);
    delete proxy.id;
    proxy.classList.add(`b-sch-${this.client.mode}`);
    return proxy;
  }
  onBeforeDragStart({
    context,
    event
  }) {
    const me = this,
      {
        client
      } = me,
      dragData = me.getMinimalDragData(context, event),
      eventRecord = dragData === null || dragData === void 0 ? void 0 : dragData[`${client.scheduledEventName}Record`],
      resourceRecord = dragData.resourceRecord;
    if (client.readOnly || me.disabled || !eventRecord || eventRecord.isDraggable === false || eventRecord.readOnly || resourceRecord !== null && resourceRecord !== void 0 && resourceRecord.readOnly) {
      return false;
    }
    // Cache the date corresponding to the drag start point so that on drag, we can always
    // perform the same calculation to then find the time delta without having to calculate
    // the new start and end times from the position that the element is.
    context.pointerStartDate = client.getDateFromXY([context.startClientX, context.startPageY], null, false);
    const result = me.triggerBeforeEventDrag(`before${client.capitalizedEventName}Drag`, {
      ...dragData,
      event,
      // to be deprecated
      context: {
        ...context,
        ...dragData
      }
    }) !== false;
    if (result) {
      var _client;
      me.updateYConstraint(eventRecord, resourceRecord);
      // Hook for features that need to react to drag starting, used by NestedEvents
      (_client = client[`before${client.capitalizedEventName}DragStart`]) === null || _client === void 0 ? void 0 : _client.call(client, context, dragData);
    }
    return result;
  }
  onAfterDragStart({
    context,
    event
  }) {}
  /**
   * Returns true if a drag operation is active
   * @property {Boolean}
   * @readonly
   */
  get isDragging() {
    var _this$drag2;
    return (_this$drag2 = this.drag) === null || _this$drag2 === void 0 ? void 0 : _this$drag2.isDragging;
  }
  // Checked by dependencies to determine if live redrawing is needed
  get isActivelyDragging() {
    return this.isDragging && !this.finalizing;
  }
  /**
   * Triggered when dragging of an event starts. Initializes drag data associated with the event being dragged.
   * @private
   */
  onDragStart({
    context,
    event
  }) {
    var _client2, _menuFeature$hideCont;
    const me = this,
      // When testing with Selenium, it simulates drag and drop with a single mousemove event, we might be over
      // another client already
      client = me.findClientFromTarget(event, context) ?? me.client;
    me.currentOverClient = client;
    me.drag.unifiedProxy = me.unifiedDrag;
    me.onMouseOverNewTimeline(client, true);
    const dragData = me.dragData = me.getDragData(context);
    // Do not let DomSync reuse the element
    me.suspendElementRedrawing(context.element);
    if (me.showTooltip && me.tip) {
      const tipTarget = dragData.context.dragProxy ? dragData.context.dragProxy.firstChild : context.element;
      me.tip.showBy(tipTarget);
    }
    me.triggerDragStart(dragData);
    // Hook for features that need to take action after drag starts
    (_client2 = client[`after${client.capitalizedEventName}DragStart`]) === null || _client2 === void 0 ? void 0 : _client2.call(client, context, dragData);
    const {
        eventMenu,
        taskMenu
      } = client.features,
      menuFeature = eventMenu || taskMenu;
    // If this is a touch action, hide the context menu which may have shown
    menuFeature === null || menuFeature === void 0 ? void 0 : (_menuFeature$hideCont = menuFeature.hideContextMenu) === null || _menuFeature$hideCont === void 0 ? void 0 : _menuFeature$hideCont.call(menuFeature, false);
  }
  updateDateIndicator() {
    const {
        startDate,
        endDate
      } = this.dragData,
      {
        tip,
        clockTemplate
      } = this,
      endDateElement = tip.element.querySelector('.b-sch-tooltip-enddate');
    clockTemplate.updateDateIndicator(tip.element, startDate);
    endDateElement && clockTemplate.updateDateIndicator(endDateElement, endDate);
  }
  findClientFromTarget(event, context) {
    let {
      target
    } = event;
    // Can't detect target under a touch event
    if (/^touch/.test(event.type)) {
      const center = Rectangle.from(context.element, null, true).center;
      target = DomHelper.elementFromPoint(center.x, center.y);
    }
    const client = Widget.fromElement(target, 'timelinebase');
    // Do not allow drops on histogram widgets
    return client !== null && client !== void 0 && client.isResourceHistogram ? null : client;
  }
  /**
   * Triggered while dragging an event. Updates drag data, validation etc.
   * @private
   */
  onDrag({
    context,
    event
  }) {
    const me = this,
      dd = me.dragData,
      start = dd.startDate;
    let client;
    if (me.constrainDragToTimeline) {
      client = me.client;
    } else {
      client = me.findClientFromTarget(event, dd.context);
    }
    me.updateDragContext(context, event);
    if (!client) {
      return;
    }
    if (client !== me.currentOverClient) {
      me.onMouseOverNewTimeline(client);
    }
    //this.checkShiftChange();
    // Let product specific implementations trigger drag event (eventDrag, taskDrag)
    if (dd.dirty || !me.throttleDragEvent) {
      const valid = dd.valid;
      me.triggerEventDrag(dd, start);
      if (valid !== dd.valid) {
        dd.context.valid = dd.externalDragValidity = dd.valid;
      }
    }
    if (me.showTooltip && me.tip) {
      // If we've an error message to show, force the tip to be visible
      // even if the target is not in view.
      me.tip.lastAlignSpec.allowTargetOut = !dd.valid;
      me.tip.realign();
    }
  }
  onMouseOverNewTimeline(newTimeline, initial) {
    const me = this,
      {
        drag: {
          lockX,
          lockY
        }
      } = me,
      scrollables = [];
    me.currentOverClient.element.classList.remove('b-dragging-' + me.currentOverClient.scheduledEventName);
    newTimeline.element.classList.add('b-dragging-' + newTimeline.scheduledEventName);
    if (!initial) {
      me.currentOverClient.scrollManager.stopMonitoring();
    }
    if (!lockX) {
      scrollables.push({
        element: newTimeline.timeAxisSubGrid.scrollable.element,
        direction: 'horizontal'
      });
    }
    if (!lockY) {
      scrollables.push({
        element: newTimeline.scrollable.element,
        direction: 'vertical'
      });
    }
    newTimeline.scrollManager.startMonitoring({
      scrollables,
      callback: me.drag.onScrollManagerScrollCallback
    });
    me.currentOverClient = newTimeline;
  }
  triggerBeforeEventDropFinalize(eventType, eventData, client) {
    client.trigger(eventType, eventData);
  }
  /**
   * Triggered when dropping an event. Finalizes the operation.
   * @private
   */
  onDrop({
    context,
    event
  }) {
    var _me$tip;
    const me = this,
      {
        currentOverClient,
        dragData
      } = me;
    let modified = false;
    // Stop monitoring early, to avoid scrolling during finalization
    currentOverClient === null || currentOverClient === void 0 ? void 0 : currentOverClient.scrollManager.stopMonitoring();
    (_me$tip = me.tip) === null || _me$tip === void 0 ? void 0 : _me$tip.hide();
    context.valid = context.valid && me.isValidDrop(dragData);
    // If dropping outside scheduler, we opt in on DragHelper removing the proxy element
    me.drag.removeProxyAfterDrop = Boolean(dragData.externalDropTarget);
    if (context.valid && dragData.startDate && dragData.endDate) {
      let beforeDropTriggered = false;
      dragData.finalize = async valid => {
        if (beforeDropTriggered || dragData.async) {
          await me.finalize(valid);
        } else {
          // If user finalized operation synchronously in the beforeDropFinalize listener, just use
          // the valid param and carry on
          // but ignore it, if the context is already marked as invalid
          context.valid = context.valid && valid;
        }
      };
      me.triggerBeforeEventDropFinalize(`before${currentOverClient.capitalizedEventName}DropFinalize`, {
        context: dragData,
        domEvent: event
      }, currentOverClient);
      beforeDropTriggered = true;
      // Allow implementer to take control of the flow, by returning false from this listener,
      // to show a confirmation popup etc. This event is documented in EventDrag and TaskDrag
      context.async = dragData.async;
      // Internal validation, making sure all dragged records fit inside the view
      if (!context.async && !dragData.externalDropTarget) {
        modified = dragData.startDate - dragData.origStart !== 0 || dragData.newResource !== dragData.resourceRecord;
      }
    }
    if (!context.async) {
      me.finalize(dragData.valid && context.valid && modified);
    }
  }
  onDragAbort({
    context
  }) {
    var _me$currentOverClient, _me$tip2;
    const me = this;
    // Stop monitoring early, to avoid scrolling during finalization
    (_me$currentOverClient = me.currentOverClient) === null || _me$currentOverClient === void 0 ? void 0 : _me$currentOverClient.scrollManager.stopMonitoring();
    me.client.currentOrientation.onDragAbort({
      context,
      dragData: me.dragData
    });
    // otherwise the event disappears on next refresh (#62)
    me.resetDraggedElements();
    (_me$tip2 = me.tip) === null || _me$tip2 === void 0 ? void 0 : _me$tip2.hide();
    // Trigger eventDragAbort / taskDragAbort depending on product
    me.triggerDragAbort(me.dragData);
  }
  // Fired after any abort animation has completed (the point where we want to trigger redraw of progress lines etc)
  onDragAbortFinalized({
    context
  }) {
    var _me$client, _me$client2;
    const me = this;
    me.triggerDragAbortFinalized(me.dragData);
    // Hook for features that need to react on drag abort, used by NestedEvents
    (_me$client = (_me$client2 = me.client)[`after${me.client.capitalizedEventName}DragAbortFinalized`]) === null || _me$client === void 0 ? void 0 : _me$client.call(_me$client2, context, me.dragData);
  }
  // For the drag across multiple schedulers, tell all involved scroll managers to stop monitoring
  onDragReset({
    source: dragHelper
  }) {
    var _dragHelper$context;
    const me = this,
      currentTimeline = me.currentOverClient;
    if ((_dragHelper$context = dragHelper.context) !== null && _dragHelper$context !== void 0 && _dragHelper$context.started) {
      me.resetDraggedElements();
      currentTimeline.trigger(`${currentTimeline.scheduledEventName}DragReset`);
    }
    currentTimeline === null || currentTimeline === void 0 ? void 0 : currentTimeline.element.classList.remove(`b-dragging-${currentTimeline.scheduledEventName}`);
    me.dragData = null;
  }
  resetDraggedElements() {
    const {
        dragData
      } = this,
      {
        eventBarEls,
        draggedEntities
      } = dragData;
    this.resumeRecordElementRedrawing(dragData.record);
    draggedEntities.forEach((record, i) => {
      this.resumeRecordElementRedrawing(record);
      eventBarEls[i].classList.remove(this.drag.draggingCls);
      eventBarEls[i].retainElement = false;
    });
    // Code expects 1:1 ratio between eventBarEls & dragged assignments, but when dragging an event of a linked
    // resource that is not the case, and we need to clean up some more
    dragData.context.element.retainElement = false;
  }
  /**
   * Triggered internally on invalid drop.
   * @private
   */
  onInternalInvalidDrop(abort) {
    var _me$tip3;
    const me = this,
      {
        context
      } = me.drag;
    (_me$tip3 = me.tip) === null || _me$tip3 === void 0 ? void 0 : _me$tip3.hide();
    me.triggerAfterDrop(me.dragData, false);
    context.valid = false;
    if (abort) {
      me.drag.abort();
    }
  }
  //endregion
  //region Finalization & validation
  /**
   * Called on drop to update the record of the event being dropped.
   * @private
   * @param {Boolean} updateRecords Specify true to update the record, false to treat as invalid
   */
  async finalize(updateRecords) {
    const me = this,
      {
        dragData,
        currentOverClient
      } = me,
      clientEventTipFeature = currentOverClient.features.taskTooltip || currentOverClient.features.eventTooltip;
    // Drag could've been aborted by window blur event. If it is aborted - we have nothing to finalize.
    if (!dragData || me.finalizing) {
      return;
    }
    const {
      context,
      draggedEntities,
      externalDropTarget
    } = dragData;
    let result;
    me.finalizing = true;
    draggedEntities.forEach((record, i) => {
      me.resumeRecordElementRedrawing(record);
      dragData.eventBarEls[i].classList.remove(me.drag.draggingCls);
      dragData.eventBarEls[i].retainElement = false;
    });
    // Code expects 1:1 ratio between eventBarEls & dragged assignments, but when dragging an event of a linked
    // resource that is not the case, and we need to clean up some more
    context.element.retainElement = false;
    if (externalDropTarget && dragData.valid || updateRecords) {
      // updateRecords may or may not be async.
      // We see if it returns a Promise.
      result = me.updateRecords(dragData);
      // If updateRecords is async, the calling DragHelper must know this and
      // go into a awaitingFinalization state.
      if (!externalDropTarget && Objects.isPromise(result)) {
        context.async = true;
        await result;
      }
      // If the finalize handler decided to change the dragData's validity...
      if (!dragData.valid) {
        me.onInternalInvalidDrop(true);
      } else {
        if (context.async) {
          context.finalize();
        }
        if (externalDropTarget) {
          // Force a refresh early so that removed events will not temporary be visible while engine is
          // recalculating (the row below clears the 'b-hidden' CSS class of the original drag element)
          me.client.refreshRows(false);
        }
        me.triggerAfterDrop(dragData, true);
      }
    } else {
      me.onInternalInvalidDrop(context.async || dragData.async);
    }
    me.finalizing = false;
    // Prevent event tooltip showing after a drag drop
    if (clientEventTipFeature !== null && clientEventTipFeature !== void 0 && clientEventTipFeature.enabled) {
      clientEventTipFeature.disabled = true;
      currentOverClient.setTimeout(() => {
        clientEventTipFeature.disabled = false;
      }, 200);
    }
    return result;
  }
  //endregion
  //region Drag data
  /**
   * Updates drag data's dates and validity (calls #validatorFn if specified)
   * @private
   */
  updateDragContext(info, event) {
    const me = this,
      {
        drag
      } = me,
      dd = me.dragData,
      client = me.currentOverClient,
      {
        isHorizontal
      } = client,
      [record] = dd.draggedEntities,
      eventRecord = record.isAssignment ? record.event : record,
      lastDragStartDate = dd.startDate,
      constrainToTimeSlot = me.constrainDragToTimeSlot || (isHorizontal ? drag.lockX : drag.lockY);
    dd.browserEvent = event;
    // getProductDragContext may switch valid flag, need to keep it here
    Object.assign(dd, me.getProductDragContext(dd));
    if (constrainToTimeSlot) {
      dd.timeDiff = 0;
    } else {
      let timeDiff;
      // Time diff is calculated differently for continuous and non-continuous time axis
      if (client.timeAxis.isContinuous) {
        const timeAxisPosition = client.isHorizontal ? info.pageX ?? info.startPageX : info.pageY ?? info.startPageY,
          // Use the localized coordinates to ask the TimeAxisViewModel what date the mouse is at.
          // Pass allowOutOfRange as true in case we have dragged out of either side of the timeline viewport.
          pointerDate = client.getDateFromCoordinate(timeAxisPosition, null, false, true);
        timeDiff = dd.timeDiff = pointerDate - info.pointerStartDate;
      } else {
        const range = me.resolveStartEndDates(info.element);
        // if dragging is out of timeAxis rect bounds, we will not be able to get dates
        dd.valid = Boolean(range.startDate && range.endDate);
        if (dd.valid) {
          timeDiff = range.startDate - dd.origStart;
        }
      }
      // If we got a time diff, we calculate new dates the same way no matter if it's continuous or not.
      // This prevents no-change drops in non-continuous time axis from being processed by updateAssignments()
      if (timeDiff !== null) {
        // calculate and round new startDate based on actual timeDiff
        dd.startDate = me.adjustStartDate(dd.origStart, timeDiff);
        dd.endDate = DateHelper.add(dd.startDate, eventRecord.fullDuration);
        if (dd.valid) {
          dd.timeDiff = dd.startDate - dd.origStart;
        }
      }
    }
    const positionDirty = dd.dirty = dd.dirty || lastDragStartDate - dd.startDate !== 0;
    if (dd.valid) {
      // If it's fully outside, we don't allow them to drop it - the event would disappear from their control.
      if (me.constrainDragToTimeline && (dd.endDate <= client.timeAxis.startDate || dd.startDate >= client.timeAxis.endDate)) {
        dd.valid = false;
        dd.context.message = me.L('L{EventDrag.noDropOutsideTimeline}');
      } else if (positionDirty || dd.externalDropTarget) {
        // Used to rely on faulty code above that would not be valid initially. With that changed we ignore
        // checking validity here on drag start, which is detected by not having a pageX
        const result = dd.externalDragValidity = !event || info.pageX && me.checkDragValidity(dd, event);
        if (!result || typeof result === 'boolean') {
          dd.valid = result !== false;
          dd.context.message = '';
        } else {
          dd.valid = result.valid !== false;
          dd.context.message = result.message;
        }
      } else {
        var _dd$externalDragValid;
        // Apply cached value from external drag validation
        dd.valid = dd.externalDragValidity !== false && ((_dd$externalDragValid = dd.externalDragValidity) === null || _dd$externalDragValid === void 0 ? void 0 : _dd$externalDragValid.valid) !== false;
      }
    } else {
      dd.valid = false;
    }
    dd.context.valid = dd.valid;
  }
  suspendRecordElementRedrawing(record, suspend = true) {
    this.suspendElementRedrawing(this.getRecordElement(record), suspend);
    record.instanceMeta(this.client).retainElement = suspend;
  }
  resumeRecordElementRedrawing(record) {
    this.suspendRecordElementRedrawing(record, false);
  }
  suspendElementRedrawing(element, suspend = true) {
    if (element) {
      element.retainElement = suspend;
    }
  }
  resumeElementRedrawing(element) {
    this.suspendElementRedrawing(element, false);
  }
  /**
   * Initializes drag data (dates, constraints, dragged events etc). Called when drag starts.
   * @private
   * @param info
   * @returns {*}
   */
  getDragData(info) {
    const me = this,
      {
        client,
        drag
      } = me,
      productDragData = me.setupProductDragData(info),
      {
        record,
        eventBarEls,
        draggedEntities
      } = productDragData,
      {
        startEvent
      } = drag,
      timespan = record.isAssignment ? record.event : record,
      origStart = timespan.startDate,
      origEnd = timespan.endDate,
      timeAxis = client.timeAxis,
      startsOutsideView = origStart < timeAxis.startDate,
      endsOutsideView = origEnd > timeAxis.endDate,
      multiSelect = client.isSchedulerBase ? client.multiEventSelect : client.selectionMode.multiSelect,
      coordinate = me.getCoordinate(timespan, info.element, [info.elementStartX, info.elementStartY]),
      clientCoordinate = me.getCoordinate(timespan, info.element, [info.startClientX, info.startClientY]);
    me.suspendRecordElementRedrawing(record);
    // prevent elements from being released when out of view
    draggedEntities.forEach(record => me.suspendRecordElementRedrawing(record));
    // Make sure the dragged event is selected (no-op for already selected)
    // Preserve other selected events if ctrl/meta is pressed
    if (record.isAssignment) {
      client.selectAssignment(record, startEvent.ctrlKey && multiSelect);
    } else {
      client.selectEvent(record, startEvent.ctrlKey && multiSelect);
    }
    const dragData = {
      context: info,
      ...productDragData,
      sourceDate: startsOutsideView ? origStart : client.getDateFromCoordinate(coordinate),
      screenSourceDate: client.getDateFromCoordinate(clientCoordinate, null, false),
      startDate: origStart,
      endDate: origEnd,
      timeDiff: 0,
      origStart,
      origEnd,
      startsOutsideView,
      endsOutsideView,
      duration: origEnd - origStart,
      browserEvent: startEvent // So we can know if SHIFT/CTRL was pressed
    };

    eventBarEls.forEach(el => el.classList.remove('b-sch-event-hover', 'b-active'));
    if (eventBarEls.length > 1) {
      // RelatedElements are secondary elements moved by the same delta as the grabbed element
      info.relatedElements = eventBarEls.slice(1);
    }
    return dragData;
  }
  //endregion
  //region Constraints
  // private
  setupConstraints(constrainRegion, elRegion, tickSize, constrained) {
    const me = this,
      xTickSize = !me.showExactDropPosition && tickSize > 1 ? tickSize : 0,
      yTickSize = 0;
    // If `constrained` is false then we have no date constraints and should constrain mouse position to scheduling area
    // else we have specified date constraints and so we should limit mouse position to smaller region inside of constrained region using offsets and width.
    if (constrained) {
      me.setXConstraint(constrainRegion.left, constrainRegion.right - elRegion.width, xTickSize);
    }
    // And if not constrained, release any constraints from the previous drag.
    else {
      // minX being true means allow the start to be before the time axis.
      // maxX being true means allow the end to be after the time axis.
      me.setXConstraint(true, true, xTickSize);
    }
    me.setYConstraint(constrainRegion.top, constrainRegion.bottom - elRegion.height, yTickSize);
  }
  updateYConstraint(eventRecord, resourceRecord) {
    const me = this,
      {
        client
      } = me,
      {
        context
      } = me.drag,
      tickSize = client.timeAxisViewModel.snapPixelAmount;
    // If we're dragging when the vertical size is recalculated by the host grid,
    // we must update our Y constraint unless we are locked in the Y axis.
    if (context && !me.drag.lockY) {
      let constrainRegion;
      // This calculates a relative region which the DragHelper uses within its outerElement
      if (me.constrainDragToTimeline) {
        constrainRegion = client.getScheduleRegion(resourceRecord, eventRecord);
      }
      // Not constraining to timeline.
      // Unusual configuration, but this must mean no Y constraining.
      else {
        me.setYConstraint(null, null, tickSize);
        return;
      }
      me.setYConstraint(constrainRegion.top, constrainRegion.bottom - context.element.offsetHeight, tickSize);
    } else {
      me.setYConstraint(null, null, tickSize);
    }
  }
  setXConstraint(iLeft, iRight, iTickSize) {
    const {
      drag
    } = this;
    drag.minX = iLeft;
    drag.maxX = iRight;
  }
  setYConstraint(iUp, iDown, iTickSize) {
    const {
      drag
    } = this;
    drag.minY = iUp;
    drag.maxY = iDown;
  }
  //endregion
  //region Other stuff
  adjustStartDate(startDate, timeDiff) {
    const rounded = this.client.timeAxis.roundDate(new Date(startDate - 0 + timeDiff), this.client.snapRelativeToEventStartDate ? startDate : false);
    return this.constrainStartDate(rounded);
  }
  resolveStartEndDates(draggedElement) {
    const timeline = this.currentOverClient,
      {
        timeAxis
      } = timeline,
      proxyRect = Rectangle.from(draggedElement.querySelector(timeline.eventInnerSelector), timeline.timeAxisSubGridElement),
      dd = this.dragData,
      [record] = dd.draggedEntities,
      eventRecord = record.isAssignment ? record.event : record,
      {
        fullDuration
      } = eventRecord,
      fillSnap = timeline.fillTicks && timeline.snapRelativeToEventStartDate;
    // Non-continuous time axis will return null instead of date for a rectangle outside of the view unless
    // told to estimate date.
    // When using fillTicks, we need exact dates for calculations below
    let {
      start: startDate,
      end: endDate
    } = timeline.getStartEndDatesFromRectangle(proxyRect, fillSnap ? null : 'round', fullDuration, true);
    // if dragging is out of timeAxis rect bounds, we will not be able to get dates
    if (startDate && endDate) {
      // When filling ticks, proxy start does not represent actual start date.
      // Need to compensate to get expected result
      if (fillSnap) {
        const
          // Events offset into the tick, in MS
          offsetMS = eventRecord.startDate - DateHelper.startOf(eventRecord.startDate, timeAxis.unit),
          // Proxy length in MS
          proxyMS = endDate - startDate,
          // Part of proxy that is "filled" and needs to be removed
          offsetPx = offsetMS / proxyMS * proxyRect.width;
        // Deflate top for vertical mode, left for horizontal mode
        proxyRect.deflate(offsetPx, 0, 0, offsetPx);
        const proxyStart = proxyRect.getStart(timeline.rtl, !timeline.isVertical);
        // Get date from offset proxy start
        startDate = timeline.getDateFromCoordinate(proxyStart, null, true);
        // Snap relative to event start date
        startDate = timeAxis.roundDate(startDate, eventRecord.startDate);
      }
      startDate = this.adjustStartDate(startDate, 0);
      if (!dd.startsOutsideView) {
        // Make sure we didn't target a start date that is filtered out, if we target last hour cell (e.g. 21:00) of
        // the time axis, and the next tick is 08:00 following day. Trying to drop at end of 21:00 cell should target start of next cell
        if (!timeAxis.dateInAxis(startDate, false)) {
          const tick = timeAxis.getTickFromDate(startDate);
          if (tick >= 0) {
            startDate = timeAxis.getDateFromTick(tick);
          }
        }
        endDate = startDate && DateHelper.add(startDate, fullDuration);
      } else if (!dd.endsOutsideView) {
        startDate = endDate && DateHelper.add(endDate, -fullDuration);
      }
    }
    return {
      startDate,
      endDate
    };
  }
  //endregion
  //region Dragtip
  /**
   * Gets html to display in tooltip while dragging event. Uses clockTemplate to display start & end dates.
   */
  getTipHtml() {
    const me = this,
      {
        dragData,
        client,
        tooltipTemplate
      } = me,
      {
        startDate,
        endDate,
        draggedEntities
      } = dragData,
      startText = client.getFormattedDate(startDate),
      endText = client.getFormattedEndDate(endDate, startDate),
      {
        valid,
        message,
        element,
        dragProxy
      } = dragData.context,
      tipTarget = dragProxy ? dragProxy.firstChild : element,
      dragged = draggedEntities[0],
      // Scheduler always drags assignments
      timeSpanRecord = dragged.isTask ? dragged : dragged.event;
    // Keep align target up to date in case of derendering the target when
    // dragged outside render window, and re-entry into the render window.
    me.tip.lastAlignSpec.target = tipTarget;
    return tooltipTemplate({
      valid,
      startDate,
      endDate,
      startText,
      endText,
      dragData,
      message: message || '',
      [client.scheduledEventName + 'Record']: timeSpanRecord,
      startClockHtml: me.clockTemplate.template({
        date: startDate,
        text: startText,
        cls: 'b-sch-tooltip-startdate'
      }),
      endClockHtml: timeSpanRecord.isMilestone ? '' : me.clockTemplate.template({
        date: endDate,
        text: endText,
        cls: 'b-sch-tooltip-enddate'
      })
    });
  }
  //endregion
  //region Configurable
  // Constrain to time slot means lockX if we're horizontal, otherwise lockY
  updateConstrainDragToTimeSlot(value) {
    const axis = this.client.isHorizontal ? 'lockX' : 'lockY';
    if (this.drag) {
      this.drag[axis] = value;
    }
  }
  // Constrain to resource means lockY if we're horizontal, otherwise lockX
  updateConstrainDragToResource(constrainDragToResource) {
    const me = this;
    if (me.drag) {
      const {
          constrainDragToTimeSlot
        } = me,
        {
          isHorizontal
        } = me.client;
      if (constrainDragToResource) {
        me.constrainDragToTimeline = true;
      }
      me.drag.lockY = isHorizontal ? constrainDragToResource : constrainDragToTimeSlot;
      me.drag.lockX = isHorizontal ? constrainDragToTimeSlot : constrainDragToResource;
    }
  }
  updateConstrainDragToTimeline(constrainDragToTimeline) {
    if (!this.isConfiguring) {
      Object.assign(this.drag, {
        cloneTarget: !constrainDragToTimeline,
        dragWithin: constrainDragToTimeline ? null : document.body,
        scrollManager: constrainDragToTimeline ? this.client.scrollManager : null
      });
    }
  }
  constrainStartDate(startDate) {
    const {
        dragData
      } = this,
      {
        dateConstraints
      } = dragData,
      scheduleableRecord = dragData.eventRecord || dragData.taskRecord || dragData.draggedEntities[0];
    if (dateConstraints !== null && dateConstraints !== void 0 && dateConstraints.start) {
      startDate = DateHelper.max(dateConstraints.start, startDate);
    }
    if (dateConstraints !== null && dateConstraints !== void 0 && dateConstraints.end) {
      startDate = DateHelper.min(new Date(dateConstraints.end - scheduleableRecord.durationMS), startDate);
    }
    return startDate;
  }
  //endregion
  //region Product specific, implemented in subclasses
  getElementFromContext(context) {
    return context.grabbed || context.dragProxy || context.element;
  }
  // Provide your custom implementation of this to allow additional selected records to be dragged together with the original one.
  getRelatedRecords(record) {
    return [];
  }
  getMinimalDragData(info, event) {
    // Can be overridden in subclass
    return {};
  }
  // Check if element can be dropped at desired location
  isValidDrop(dragData) {
    throw new Error('Implement in subclass');
  }
  // Similar to the fn above but also calls validatorFn
  checkDragValidity(dragData) {
    throw new Error('Implement in subclass');
  }
  // Update records being dragged
  updateRecords(context) {
    throw new Error('Implement in subclass');
  }
  // Determine if an element can be dragged
  isElementDraggable(el, event) {
    throw new Error('Implement in subclass');
  }
  // Get coordinate for correct axis
  getCoordinate(record, element, coord) {
    throw new Error('Implement in subclass');
  }
  // Product specific drag data
  setupProductDragData(info) {
    throw new Error('Implement in subclass');
  }
  // Product specific data in drag context
  getProductDragContext(dd) {
    throw new Error('Implement in subclass');
  }
  getRecordElement(record) {
    throw new Error('Implement in subclass');
  }
  //endregion
}

DragBase._$name = 'DragBase';

/**
 * @module Scheduler/feature/EventResize
 */
const tipAlign = {
  top: 'b-t',
  right: 'b100-t100',
  bottom: 't-b',
  left: 'b0-t0'
};
/**
 * Feature that allows resizing an event by dragging its end.
 *
 * By default it displays a tooltip with the new start and end dates, formatted using
 * {@link Scheduler/view/mixin/TimelineViewPresets#config-displayDateFormat}.
 *
 * ## Customizing the resize tooltip
 *
 * To show custom HTML in the tooltip, please see the {@link #config-tooltipTemplate} config. Example:
 *
 * ```javascript
 * eventResize : {
 *     // A minimal end date tooltip
 *     tooltipTemplate : ({ record, endDate }) => {
 *         return DateHelper.format(endDate, 'MMM D');
 *     }
 * }
 * ```
 *
 * This feature is **enabled** by default
 *
 * This feature is extended with a few overrides by the Gantt's `TaskResize` feature.
 *
 * This feature updates the event's `startDate` or `endDate` live in order to leverage the
 * rendering pathway to always yield a correct appearance. The changes are done in
 * {@link Core.data.Model#function-beginBatch batched} mode so that changes do not become
 * eligible for data synchronization or propagation until the operation is completed.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/EventResize.js
 * @classtype eventResize
 * @feature
 */
class EventResize extends InstancePlugin.mixin(Draggable, Droppable) {
  //region Events
  /**
   * Fired on the owning Scheduler before resizing starts. Return `false` to prevent the action.
   * @event beforeEventResize
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel} eventRecord Event record being resized
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the resize starts within
   * @param {MouseEvent} event Browser event
   */
  /**
   * Fires on the owning Scheduler when event resizing starts
   * @event eventResizeStart
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel} eventRecord Event record being resized
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the resize starts within
   * @param {MouseEvent} event Browser event
   */
  /**
   * Fires on the owning Scheduler on each resize move event
   * @event eventPartialResize
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel} eventRecord Event record being resized
   * @param {Date} startDate
   * @param {Date} endDate
   * @param {HTMLElement} element
   */
  /**
   * Fired on the owning Scheduler to allow implementer to prevent immediate finalization by setting
   * `data.context.async = true` in the listener, to show a confirmation popup etc
   *
   * ```javascript
   *  scheduler.on('beforeeventresizefinalize', ({context}) => {
   *      context.async = true;
   *      setTimeout(() => {
   *          // async code don't forget to call finalize
   *          context.finalize();
   *      }, 1000);
   *  })
   * ```
   *
   * @event beforeEventResizeFinalize
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Object} context
   * @param {Scheduler.model.EventModel} context.eventRecord Event record being resized
   * @param {Date} context.startDate New startDate (changed if resizing start side)
   * @param {Date} context.endDate New endDate (changed if resizing end side)
   * @param {Date} context.originalStartDate Start date before resize
   * @param {Date} context.originalEndDate End date before resize
   * @param {Boolean} context.async Set true to handle resize asynchronously (e.g. to wait for user confirmation)
   * @param {Function} context.finalize Call this method to finalize resize. This method accepts one argument:
   *                   pass `true` to update records, or `false`, to ignore changes
   * @param {Event} event Browser event
   */
  /**
   * Fires on the owning Scheduler after the resizing gesture has finished.
   * @event eventResizeEnd
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Boolean} changed Shows if the record has been changed by the resize action
   * @param {Scheduler.model.EventModel} eventRecord Event record being resized
   */
  //endregion
  //region Config
  static get $name() {
    return 'EventResize';
  }
  static get configurable() {
    return {
      draggingItemCls: 'b-sch-event-wrap-resizing',
      resizingItemInnerCls: 'b-sch-event-resizing',
      /**
       * Use left handle when resizing. Only applies when owning client's `direction` is 'horizontal'
       * @config {Boolean}
       * @default
       */
      leftHandle: true,
      /**
       * Use right handle when resizing. Only applies when owning client's `direction` is 'horizontal'
       * @config {Boolean}
       * @default
       */
      rightHandle: true,
      /**
       * Use top handle when resizing. Only applies when owning client's direction` is 'vertical'
       * @config {Boolean}
       * @default
       */
      topHandle: true,
      /**
       * Use bottom handle when resizing. Only applies when owning client's `direction` is 'vertical'
       * @config {Boolean}
       * @default
       */
      bottomHandle: true,
      /**
       * Resizing handle size to use instead of that determined by CSS
       * @config {Number}
       * @deprecated Since 5.2.7. The handle size is determined from responsive CSS. Will be removed in 6.0
       */
      handleSize: null,
      /**
       * Automatically shrink virtual handles when available space < handleSize. The virtual handles will
       * decrease towards width/height 1, reserving space between opposite handles to for example leave room for
       * dragging. To configure reserved space, see {@link #config-reservedSpace}.
       * @config {Boolean}
       * @default false
       */
      dynamicHandleSize: true,
      /**
       * Set to true to allow resizing to a zero-duration span
       * @config {Boolean}
       * @default false
       */
      allowResizeToZero: null,
      /**
       * Room in px to leave unoccupied by handles when shrinking them dynamically (see
       * {@link #config-dynamicHandleSize}).
       * @config {Number}
       * @default
       */
      reservedSpace: 5,
      /**
       * Resizing handle size to use instead of that determined by CSS on touch devices
       * @config {Number}
       * @deprecated Since 5.2.7. The handle size is determined from responsive CSS. Will be removed in 6.0
       */
      touchHandleSize: null,
      /**
       * The amount of pixels to move pointer/mouse before it counts as a drag operation.
       * @config {Number}
       * @default
       */
      dragThreshold: 0,
      dragTouchStartDelay: 0,
      draggingClsSelector: '.b-timeline-base',
      /**
       * `false` to not show a tooltip while resizing
       * @config {Boolean}
       * @default
       */
      showTooltip: true,
      /**
       * true to see exact event length during resizing
       * @config {Boolean}
       * @default
       */
      showExactResizePosition: false,
      /**
       * An empty function by default, but provided so that you can perform custom validation on
       * the item being resized. Return true if the new duration is valid, false to signal that it is not.
       * @param {Object} context The resize context, contains the record & dates.
       * @param {Scheduler.model.TimeSpan} context.record The record being resized.
       * @param {Date} context.startDate The new start date.
       * @param {Date} context.endDate The new start date.
       * @param {Date} context.originalStartDate Start date before resize
       * @param {Date} context.originalEndDate End date before resize
       * @param {Event} event The browser Event object
       * @returns {Boolean}
       * @config {Function}
       */
      validatorFn: () => true,
      /**
       * `this` reference for the validatorFn
       * @config {Object}
       */
      validatorFnThisObj: null,
      /**
       * Setting this property may change the configuration of the {@link #config-tip}, or
       * cause it to be destroyed if `null` is passed.
       *
       * Reading this property returns the Tooltip instance.
       * @member {Core.widget.Tooltip|TooltipConfig} tip
       */
      /**
       * If a tooltip is required to illustrate the resize, specify this as `true`, or a config
       * object for the {@link Core.widget.Tooltip}.
       * @config {Core.widget.Tooltip|TooltipConfig}
       */
      tip: {
        $config: ['lazy', 'nullify'],
        value: {
          autoShow: false,
          axisLock: true,
          trackMouse: false,
          updateContentOnMouseMove: true,
          hideDelay: 0
        }
      },
      /**
       * A template function returning the content to show during a resize operation.
       * @param {Object} context A context object
       * @param {Date} context.startDate New start date
       * @param {Date} context.endDate New end date
       * @param {Scheduler.model.TimeSpan} context.record The record being resized
       * @config {Function} tooltipTemplate
       */
      tooltipTemplate: context => `
                <div class="b-sch-tip-${context.valid ? 'valid' : 'invalid'}">
                    ${context.startClockHtml}
                    ${context.endClockHtml}
                    <div class="b-sch-tip-message">${context.message}</div>
                </div>
            `,
      ignoreSelector: '.b-sch-terminal',
      dragActiveCls: 'b-resizing-event'
    };
  }
  static get pluginConfig() {
    return {
      chain: ['render', 'onEventDataGenerated', 'isEventElementDraggable']
    };
  }
  //endregion
  //region Init & destroy
  doDestroy() {
    var _this$dragging;
    super.doDestroy();
    (_this$dragging = this.dragging) === null || _this$dragging === void 0 ? void 0 : _this$dragging.destroy();
  }
  render() {
    const me = this,
      {
        client
      } = me;
    // Only active when in these items
    me.dragSelector = me.dragItemSelector = client.eventSelector;
    // Set up elements and listeners
    me.dragRootElement = me.dropRootElement = client.timeAxisSubGridElement;
    // Drag only in time dimension
    me.dragLock = client.isVertical ? 'y' : 'x';
  }
  // Prevent event dragging when it happens over a resize handle
  isEventElementDraggable(eventElement, eventRecord, el, event) {
    const me = this,
      eventResizable = eventRecord === null || eventRecord === void 0 ? void 0 : eventRecord.resizable;
    // ALLOW event drag:
    // - if resizing is disabled or event is not resizable
    // - if it's a milestone Milestones cannot be resized
    if (me.disabled || !eventResizable || eventRecord.isMilestone) {
      return true;
    }
    // not over the event handles
    return (eventResizable !== true && eventResizable !== 'start' || !me.isOverStartHandle(event, eventElement)) && (eventResizable !== true && eventResizable !== 'end' || !me.isOverEndHandle(event, eventElement));
  }
  // Called for each event during render, allows manipulation of render data.
  onEventDataGenerated({
    eventRecord,
    wrapperCls,
    cls
  }) {
    var _this$dragging2, _this$dragging2$conte;
    if (eventRecord === ((_this$dragging2 = this.dragging) === null || _this$dragging2 === void 0 ? void 0 : (_this$dragging2$conte = _this$dragging2.context) === null || _this$dragging2$conte === void 0 ? void 0 : _this$dragging2$conte.eventRecord)) {
      wrapperCls['b-active'] = wrapperCls[this.draggingItemCls] = wrapperCls['b-over-resize-handle'] = cls['b-resize-handle'] = cls[this.resizingItemInnerCls] = 1;
    }
  }
  // Sneak a first peek at the drag event to put necessary date values into the context
  onDragPointerMove(event) {
    var _dragging$context;
    const {
        client,
        dragging
      } = this,
      {
        visibleDateRange,
        isHorizontal
      } = client,
      rtl = isHorizontal && client.rtl,
      dimension = isHorizontal ? 'X' : 'Y',
      pageScroll = globalThis[`page${dimension}Offset`],
      coord = event[`page${dimension}`] + (((_dragging$context = dragging.context) === null || _dragging$context === void 0 ? void 0 : _dragging$context.offset) || 0),
      clientRect = Rectangle.from(client.timeAxisSubGridElement, null, true),
      startCoord = clientRect.getStart(rtl, isHorizontal),
      endCoord = clientRect.getEnd(rtl, isHorizontal);
    let date = client.getDateFromCoord({
      coord,
      local: false
    });
    if (rtl) {
      // If we're dragging off the start side, fix at the visible startDate
      if (coord - pageScroll > startCoord) {
        date = visibleDateRange.startDate;
      }
      // If we're dragging off the end side, fix at the visible endDate
      else if (coord < endCoord) {
        date = visibleDateRange.endDate;
      }
    }
    // If we're dragging off the start side, fix at the visible startDate
    else if (coord - pageScroll < startCoord) {
      date = visibleDateRange.startDate;
    }
    // If we're dragging off the end side, fix at the visible endDate
    else if (coord - pageScroll > endCoord) {
      date = visibleDateRange.endDate;
    }
    dragging.clientStartCoord = startCoord;
    dragging.clientEndCoord = endCoord;
    dragging.date = date;
    super.onDragPointerMove(event);
  }
  /**
   * Returns true if a resize operation is active
   * @property {Boolean}
   * @readonly
   */
  get isResizing() {
    return Boolean(this.dragging);
  }
  beforeDrag(drag) {
    const {
        client
      } = this,
      eventRecord = client.resolveTimeSpanRecord(drag.itemElement),
      resourceRecord = !client.isGanttBase && client.resolveResourceRecord(client.isVertical ? drag.startEvent : drag.itemElement);
    // Events not part of project are transient records in a Gantt display store and not meant to be modified
    if (this.disabled || client.readOnly || resourceRecord !== null && resourceRecord !== void 0 && resourceRecord.readOnly || eventRecord && (eventRecord.readOnly || !(eventRecord.project || eventRecord.isOccurrence)) || super.beforeDrag(drag) === false) {
      return false;
    }
    drag.mousedownDate = drag.date = client.getDateFromCoordinate(drag.event[`page${client.isHorizontal ? 'X' : 'Y'}`], null, false);
    // trigger beforeEventResize or beforeTaskResize depending on product
    return this.triggerBeforeResize(drag);
  }
  dragStart(drag) {
    var _client$features$even, _client$resolveAssign;
    const me = this,
      {
        client,
        tip
      } = me,
      {
        startEvent,
        itemElement
      } = drag,
      name = client.scheduledEventName,
      eventRecord = client.resolveEventRecord(itemElement),
      {
        isBatchUpdating,
        wrapStartDate,
        wrapEndDate
      } = eventRecord,
      useEventBuffer = (_client$features$even = client.features.eventBuffer) === null || _client$features$even === void 0 ? void 0 : _client$features$even.enabled,
      eventStartDate = isBatchUpdating ? eventRecord.get('startDate') : eventRecord.startDate,
      eventEndDate = isBatchUpdating ? eventRecord.get('endDate') : eventRecord.endDate,
      horizontal = me.dragLock === 'x',
      rtl = horizontal && client.rtl,
      draggingEnd = me.isOverEndHandle(startEvent, itemElement),
      toSet = draggingEnd ? 'endDate' : 'startDate',
      wrapToSet = !useEventBuffer ? null : draggingEnd ? 'wrapEndDate' : 'wrapStartDate',
      otherEnd = draggingEnd ? 'startDate' : 'endDate',
      setMethod = draggingEnd ? 'setEndDate' : 'setStartDate',
      setOtherMethod = draggingEnd ? 'setStartDate' : 'setEndDate',
      elRect = Rectangle.from(itemElement),
      startCoord = horizontal ? startEvent.clientX : startEvent.clientY,
      endCoord = draggingEnd ? elRect.getEnd(rtl, horizontal) : elRect.getStart(rtl, horizontal),
      context = drag.context = {
        eventRecord,
        element: itemElement,
        timespanRecord: eventRecord,
        taskRecord: eventRecord,
        owner: me,
        valid: true,
        oldValue: draggingEnd ? eventEndDate : eventStartDate,
        startDate: eventStartDate,
        endDate: eventEndDate,
        offset: useEventBuffer ? 0 : endCoord - startCoord,
        edge: horizontal ? draggingEnd ? 'right' : 'left' : draggingEnd ? 'bottom' : 'top',
        finalize: me.finalize,
        event: drag.event,
        // these two are public
        originalStartDate: eventStartDate,
        originalEndDate: eventEndDate,
        wrapStartDate,
        wrapEndDate,
        draggingEnd,
        toSet,
        wrapToSet,
        otherEnd,
        setMethod,
        setOtherMethod
      };
    // The record must know that it is being resized.
    eventRecord.meta.isResizing = true;
    client.element.classList.add(...me.dragActiveCls.split(' '));
    // During this batch we want the client's UI to update itself using the proposed changes
    // Only if startDrag has not already done it
    if (!client.listenToBatchedUpdates) {
      client.beginListeningForBatchedUpdates();
    }
    // No changes must get through to data.
    // Only if startDrag has not already started the batch
    if (!isBatchUpdating) {
      me.beginEventRecordBatch(eventRecord);
    }
    // Let products do their specific stuff
    me.setupProductResizeContext(context, startEvent);
    // Trigger eventResizeStart or taskResizeStart depending on product
    // Subclasses (like EventDragCreate) won't actually fire this event.
    me.triggerEventResizeStart(`${name}ResizeStart`, {
      [`${name}Record`]: eventRecord,
      event: startEvent,
      ...me.getResizeStartParams(context)
    }, context);
    // Scheduler renders assignments, Gantt renders Tasks
    context.resizedRecord = ((_client$resolveAssign = client.resolveAssignmentRecord) === null || _client$resolveAssign === void 0 ? void 0 : _client$resolveAssign.call(client, context.element)) || eventRecord;
    if (tip) {
      // Tip needs to be shown first for getTooltipTarget to be able to measure anchor size
      tip.show();
      tip.align = tipAlign[context.edge];
      tip.showBy(me.getTooltipTarget(drag));
    }
  }
  // Subclasses may override this
  triggerBeforeResize(drag) {
    const {
        client
      } = this,
      eventRecord = client.resolveTimeSpanRecord(drag.itemElement);
    return client.trigger(`before${client.capitalizedEventName}Resize`, {
      [`${client.scheduledEventName}Record`]: eventRecord,
      event: drag.event,
      ...this.getBeforeResizeParams({
        event: drag.startEvent,
        element: drag.itemElement
      })
    });
  }
  // Subclasses may override this
  triggerEventResizeStart(eventType, event, context) {
    var _this$client, _this$client2;
    this.client.trigger(eventType, event);
    // Hook for features that needs to react on resize start
    (_this$client = (_this$client2 = this.client)[`after${StringHelper.capitalize(eventType)}`]) === null || _this$client === void 0 ? void 0 : _this$client.call(_this$client2, context, event);
  }
  triggerEventResizeEnd(eventType, event) {
    this.client.trigger(eventType, event);
  }
  triggerEventPartialResize(eventType, event) {
    // Trigger eventPartialResize or taskPartialResize depending on product
    this.client.trigger(eventType, event);
  }
  triggerBeforeEventResizeFinalize(eventType, event) {
    this.client.trigger(eventType, event);
  }
  dragEnter(drag) {
    var _drag$context;
    // We only respond to our own DragContexts
    return ((_drag$context = drag.context) === null || _drag$context === void 0 ? void 0 : _drag$context.owner) === this;
  }
  resizeEventPartiallyInternal(eventRecord, context) {
    var _client$features$even2;
    const {
        client
      } = this,
      {
        toSet
      } = context;
    if ((_client$features$even2 = client.features.eventBuffer) !== null && _client$features$even2 !== void 0 && _client$features$even2.enabled) {
      if (toSet === 'startDate') {
        const diff = context.startDate.getTime() - context.originalStartDate.getTime();
        eventRecord.wrapStartDate = new Date(context.wrapStartDate.getTime() + diff);
      } else if (toSet === 'endDate') {
        const diff = context.endDate.getTime() - context.originalEndDate.getTime();
        eventRecord.wrapEndDate = new Date(context.wrapEndDate.getTime() + diff);
      }
    }
    eventRecord.set(toSet, context[toSet]);
  }
  applyDateConstraints(date, eventRecord, context) {
    var _context$dateConstrai, _context$dateConstrai2;
    const minDate = (_context$dateConstrai = context.dateConstraints) === null || _context$dateConstrai === void 0 ? void 0 : _context$dateConstrai.start,
      maxDate = (_context$dateConstrai2 = context.dateConstraints) === null || _context$dateConstrai2 === void 0 ? void 0 : _context$dateConstrai2.end;
    // Keep desired date within constraints
    if (minDate || maxDate) {
      date = DateHelper.constrain(date, minDate, maxDate);
      context.snappedDate = DateHelper.constrain(context.snappedDate, minDate, maxDate);
    }
    return date;
  }
  // Override the draggable interface so that we can update the bar while dragging outside
  // the Draggable's rootElement (by default it stops notifications when outside rootElement)
  moveDrag(drag) {
    const me = this,
      {
        client,
        tip
      } = me,
      horizontal = me.dragLock === 'x',
      dimension = horizontal ? 'X' : 'Y',
      name = client.scheduledEventName,
      {
        visibleDateRange,
        enableEventAnimations,
        timeAxis,
        weekStartDay
      } = client,
      rtl = horizontal && client.rtl,
      {
        resolutionUnit,
        resolutionIncrement
      } = timeAxis,
      {
        event,
        context
      } = drag,
      {
        eventRecord
      } = context,
      offset = context.offset * (rtl ? -1 : 1),
      {
        isOccurrence
      } = eventRecord,
      eventStart = eventRecord.get('startDate'),
      eventEnd = eventRecord.get('endDate'),
      coord = event[`client${dimension}`] + offset,
      clientRect = Rectangle.from(client.timeAxisSubGridElement, null, true),
      startCoord = clientRect.getStart(rtl, horizontal),
      endCoord = clientRect.getEnd(rtl, horizontal);
    context.event = event;
    // If this is the last move event recycled because of a scroll, refresh the date
    if (event.isScroll) {
      drag.date = client.getDateFromCoordinate(event[`page${dimension}`] + offset, null, false);
    }
    let crossedOver,
      avoidedZeroSize,
      // Use the value set up in onDragPointerMove by default
      {
        date
      } = drag,
      {
        toSet,
        otherEnd,
        draggingEnd
      } = context;
    if (rtl) {
      // If we're dragging off the start side, fix at the visible startDate
      if (coord > startCoord) {
        date = drag.date = visibleDateRange.startDate;
      }
      // If we're dragging off the end side, fix at the visible endDate
      else if (coord < endCoord) {
        date = drag.date = visibleDateRange.endDate;
      }
    }
    // If we're dragging off the start side, fix at the visible startDate
    else if (coord < startCoord) {
      date = drag.date = visibleDateRange.startDate;
    }
    // If we're dragging off the end side, fix at the visible endDate
    else if (coord > endCoord) {
      date = drag.date = visibleDateRange.endDate;
    }
    // Detect crossover which some subclasses might need to process
    if (toSet === 'endDate') {
      if (date < eventStart) {
        crossedOver = -1;
      }
    } else {
      if (date > eventEnd) {
        crossedOver = 1;
      }
    }
    // If we dragged the dragged end over to the opposite side of the start end.
    // Some subclasses allow this and need to respond. EventDragCreate does this.
    if (crossedOver && me.onDragEndSwitch) {
      me.onDragEndSwitch(context, date, crossedOver);
      otherEnd = context.otherEnd;
      toSet = context.toSet;
    }
    if (client.snapRelativeToEventStartDate) {
      date = timeAxis.roundDate(date, context.oldValue);
    }
    // The displayed and eventual data value
    context.snappedDate = DateHelper.round(date, timeAxis.resolution, null, weekStartDay);
    const duration = DateHelper.diff(date, context[otherEnd], resolutionUnit) * (draggingEnd ? -1 : 1);
    // Narrower than half resolutionIncrement will abort drag creation, set flag to have UI reflect this
    if (me.isEventDragCreate) {
      context.tooNarrow = duration < resolutionIncrement / 2;
    }
    // The mousepoint date means that the duration is less than resolutionIncrement resolutionUnits.
    // Ensure that the dragged end is at least resolutionIncrement resolutionUnits from the other end.
    else if (duration < resolutionIncrement) {
      // Snap to zero if allowed
      if (me.allowResizeToZero) {
        context.snappedDate = date = context[otherEnd];
      } else {
        const sign = otherEnd === 'startDate' ? 1 : -1;
        context.snappedDate = date = timeAxis.roundDate(DateHelper.add(eventRecord.get(otherEnd), resolutionIncrement * sign, resolutionUnit));
        avoidedZeroSize = true;
      }
    }
    // take dateConstraints into account
    date = me.applyDateConstraints(date, eventRecord, context);
    // If the mouse move has changed the detected date
    if (!context.date || date - context.date || avoidedZeroSize) {
      context.date = date;
      // The validityFn needs to see the proposed value.
      // Consult our snap config to see if we should be dragging in snapped mode
      context[toSet] = me.showExactResizePosition || client.timeAxisViewModel.snap ? context.snappedDate : date;
      // Snapping would take it to zero size - this is not allowed in drag resizing.
      context.valid = me.allowResizeToZero || context[toSet] - context[toSet === 'startDate' ? 'endDate' : 'startDate'] !== 0;
      // If the date to push into the record is new...
      if (eventRecord.get(toSet) - context[toSet]) {
        context.valid = me.checkValidity(context, event);
        context.message = '';
        if (context.valid && typeof context.valid !== 'boolean') {
          context.message = context.valid.message;
          context.valid = context.valid.valid;
        }
        // If users returns nothing, that's interpreted as valid
        context.valid = context.valid !== false;
        if (context.valid) {
          const partialResizeEvent = {
            [`${name}Record`]: eventRecord,
            startDate: eventStart,
            endDate: eventEnd,
            element: drag.itemElement,
            context
          };
          // Update the event we are about to fire and the context *before* we update the record
          partialResizeEvent[toSet] = context[toSet];
          // Trigger eventPartialResize or taskPartialResize depending on product
          me.triggerEventPartialResize(`${name}PartialResize`, partialResizeEvent);
          // An occurrence must have a store to announce its batched changes through.
          // They must usually never have a store - they are transient, but we
          // need to update the UI.
          if (isOccurrence) {
            eventRecord.stores.push(client.eventStore);
          }
          // Update the eventRecord.
          // Use setter rather than accessor so that in a Project, the entity's
          // accessor doesn't propagate the change to the whole project.
          // Scheduler must not animate this.
          client.enableEventAnimations = false;
          this.resizeEventPartiallyInternal(eventRecord, context);
          client.enableEventAnimations = enableEventAnimations;
          if (isOccurrence) {
            eventRecord.stores.length = 0;
          }
        }
        // Flag drag created too narrow events as invalid late, want all code above to execute for them
        // to get the proper size rendered
        if (context.tooNarrow) {
          context.valid = false;
        }
      }
    }
    if (tip) {
      // In case of edge flip (EventDragCreate), the align point may change
      tip.align = tipAlign[context.edge];
      tip.alignTo(me.getTooltipTarget(drag));
    }
    super.moveDrag(drag);
  }
  dragEnd(drag) {
    const {
      context
    } = drag;
    if (context) {
      context.event = drag.event;
    }
    if (drag.aborted) {
      context === null || context === void 0 ? void 0 : context.finalize(false);
    }
    // 062_resize.t.js specifies that if drag was not started but the mouse has moved,
    // the eventresizestart and eventresizeend must fire
    else if (!this.isEventDragCreate && !drag.started && !EventHelper.getPagePoint(drag.event).equals(EventHelper.getPagePoint(drag.startEvent))) {
      this.dragStart(drag);
      this.cleanup(drag.context, false);
    }
  }
  async dragDrop({
    context,
    event
  }) {
    var _this$tip;
    // Set the start/end date, whichever we were dragging
    // to the correctly rounded value before updating.
    context[context.toSet] = context.snappedDate;
    const {
        client
      } = this,
      {
        startDate,
        endDate
      } = context;
    let modified;
    (_this$tip = this.tip) === null || _this$tip === void 0 ? void 0 : _this$tip.hide();
    context.valid = startDate && endDate && (this.allowResizeToZero || endDate - startDate > 0) &&
    // Input sanity check
    context[context.toSet] - context.oldValue &&
    // Make sure dragged end changed
    context.valid !== false;
    if (context.valid) {
      // Seems to be a valid resize operation, ask outside world if anyone wants to take control over the finalizing,
      // to show a confirm dialog prior to applying the new values. Triggers beforeEventResizeFinalize or
      // beforeTaskResizeFinalize depending on product
      this.triggerBeforeEventResizeFinalize(`before${client.capitalizedEventName}ResizeFinalize`, {
        context,
        event,
        [`${client.scheduledEventName}Record`]: context.eventRecord
      });
      modified = true;
    }
    // If a handler has set the async flag, it means that they are going to finalize
    // the operation at some time in the future, so we should not call it.
    if (!context.async) {
      await context.finalize(modified);
    }
  }
  // This is called with a thisObj of the context object
  // We set "me" to the owner, and "context" to the thisObj so that it
  // reads as if it were a method of this class.
  async finalize(updateRecord) {
    const me = this.owner,
      context = this,
      {
        eventRecord,
        oldValue,
        toSet
      } = context,
      {
        snapRelativeToEventStartDate,
        timeAxis
      } = me.client;
    let wasChanged = false;
    if (updateRecord) {
      if (snapRelativeToEventStartDate) {
        context[toSet] = context.snappedDate = timeAxis.roundDate(context.date, oldValue);
      }
      // Each product updates the record differently
      wasChanged = await me.internalUpdateRecord(context, eventRecord);
    } else {
      // Reverts the changes, a batchedUpdate event will fire which will reset the UI
      me.cancelEventRecordBatch(eventRecord);
      // Manually trigger redraw of occurrences since they are not part of any stores
      if (eventRecord.isOccurrence) {
        eventRecord.resources.forEach(resource => me.client.repaintEventsForResource(resource));
      }
    }
    if (!me.isDestroyed) {
      me.cleanup(context, wasChanged);
    }
  }
  // This is always called on drop or abort.
  cleanup(context, changed) {
    var _me$tip;
    const me = this,
      {
        client
      } = me,
      {
        element,
        eventRecord
      } = context,
      name = client.scheduledEventName;
    // The record must know that it is being resized.
    eventRecord.meta.isResizing = false;
    client.endListeningForBatchedUpdates();
    (_me$tip = me.tip) === null || _me$tip === void 0 ? void 0 : _me$tip.hide();
    me.unHighlightHandle(element);
    client.element.classList.remove(...me.dragActiveCls.split(' '));
    // if (dependencies) {
    //     // When resizing is done and mouse is over element, we show terminals
    //     if (element.matches(':hover')) {
    //         dependencies.showTerminals(eventRecord, element);
    //     }
    // }
    // Triggers eventResizeEnd or taskResizeEnd depending on product
    me.triggerEventResizeEnd(`${name}ResizeEnd`, {
      changed,
      [`${name}Record`]: eventRecord,
      ...me.getResizeEndParams(context)
    });
  }
  async internalUpdateRecord(context, timespanRecord) {
    const {
        client
      } = this,
      {
        generation
      } = timespanRecord;
    // Special handling of occurrences, they need normalization since that is not handled by engine at the moment
    if (timespanRecord.isOccurrence) {
      client.endListeningForBatchedUpdates();
      // If >1 level deep, just unwind one level.
      timespanRecord[timespanRecord.batching > 1 ? 'endBatch' : 'cancelBatch']();
      timespanRecord.set(TimeSpan.prototype.inSetNormalize.call(timespanRecord, {
        startDate: context.startDate,
        endDate: context.endDate
      }));
    } else {
      const toSet = {
        [context.toSet]: context[context.toSet]
      };
      // If we have the Engine available, consult it to calculate a corrected duration.
      // Adjust the dragged date point to conform with the calculated duration.
      if (timespanRecord.isEntity) {
        var _client$features$even3;
        const {
          startDate,
          endDate,
          draggingEnd
        } = context;
        // Fix the duration according to the Entity's rules.
        context.duration = toSet.duration = timespanRecord.run('calculateProjectedDuration', startDate, endDate);
        // Fix the dragged date point according to the Entity's rules.
        toSet[context.toSet] = timespanRecord.run('calculateProjectedXDateWithDuration', draggingEnd ? startDate : endDate, draggingEnd, context.duration);
        const setOtherEnd = !timespanRecord[context.otherEnd];
        // Set all values, start and end in case they had never been set
        // ie, we're now scheduling a previously unscheduled event.
        if (setOtherEnd) {
          toSet[context.otherEnd] = context[context.otherEnd];
        }
        // Update the record to its final correct state using *batched changes*
        // These will *not* be propagated, it's just to force the dragged event bar
        // into its corrected shape before the real changes which will propagate are applied below.
        // We MUST do it like this because the final state may not be a net change if the changes
        // got rejected, and in that case, the engine will not end up firing any change events.
        timespanRecord.set(toSet);
        // Quit listening for batchedUpdate *before* we cancel the batch so that the
        // change events from the revert do not update the UI.
        client.endListeningForBatchedUpdates();
        this.cancelEventRecordBatch(timespanRecord);
        // Clear estimated wrap date, exact wrap date will be calculated when referred to from renderer
        if ((_client$features$even3 = client.features.eventBuffer) !== null && _client$features$even3 !== void 0 && _client$features$even3.enabled) {
          timespanRecord[context.wrapToSet] = null;
        }
        const promisesToWait = [];
        // Really update the data after cancelling the batch
        if (setOtherEnd) {
          promisesToWait.push(timespanRecord[context.setOtherMethod](toSet[context.otherEnd], false));
        }
        promisesToWait.push(timespanRecord[context.setMethod](toSet[context.toSet], false));
        await Promise.all(promisesToWait);
        timespanRecord.endBatch();
      } else {
        // Collect any changes (except the start/end date) that happened during the resize operation
        const batchChanges = Object.assign({}, timespanRecord.meta.batchChanges);
        delete batchChanges[context.toSet];
        client.endListeningForBatchedUpdates();
        this.cancelEventRecordBatch(timespanRecord);
        timespanRecord.set(batchChanges);
        timespanRecord[context.setMethod](toSet[context.toSet], false);
      }
    }
    // wait for project data update
    await client.project.commitAsync();
    // If the record has been changed
    return timespanRecord.generation !== generation;
  }
  onDragItemMouseMove(event) {
    if (event.pointerType !== 'touch' && !this.handleSelector) {
      this.checkResizeHandles(event);
    }
  }
  /**
   * Check if mouse is over a resize handle (virtual). If so, highlight.
   * @private
   * @param {MouseEvent} event
   */
  checkResizeHandles(event) {
    const me = this,
      {
        overItem
      } = me;
    // mouse over a target element and allowed to resize?
    if (overItem && !me.client.readOnly && (!me.allowResize || me.allowResize(overItem, event))) {
      const eventRecord = me.client.resolveTimeSpanRecord(overItem);
      if (eventRecord !== null && eventRecord !== void 0 && eventRecord.readOnly) {
        return;
      }
      if (me.isOverAnyHandle(event, overItem)) {
        me.highlightHandle(); // over handle
      } else {
        me.unHighlightHandle(); // not over handle
      }
    }
  }

  onDragItemMouseLeave(event, oldOverItem) {
    this.unHighlightHandle(oldOverItem);
  }
  /**
   * Highlights handles (applies css that changes cursor).
   * @private
   */
  highlightHandle() {
    var _item$syncIdMap;
    const {
        overItem: item,
        client
      } = this,
      handleTargetElement = ((_item$syncIdMap = item.syncIdMap) === null || _item$syncIdMap === void 0 ? void 0 : _item$syncIdMap[client.scheduledEventName]) ?? item.querySelector(client.eventInnerSelector);
    // over a handle, add cls to change cursor
    handleTargetElement.classList.add('b-resize-handle');
    item.classList.add('b-over-resize-handle');
  }
  /**
   * Unhighlight handles (removes css).
   * @private
   */
  unHighlightHandle(item = this.overItem) {
    if (item) {
      var _item$syncIdMap2;
      const me = this,
        inner = ((_item$syncIdMap2 = item.syncIdMap) === null || _item$syncIdMap2 === void 0 ? void 0 : _item$syncIdMap2[me.client.scheduledEventName]) ?? item.querySelector(me.client.eventInnerSelector);
      if (inner) {
        inner.classList.remove('b-resize-handle', me.resizingItemInnerCls);
      }
      item.classList.remove('b-over-resize-handle', me.draggingItemCls);
    }
  }
  isOverAnyHandle(event, target) {
    return this.isOverStartHandle(event, target) || this.isOverEndHandle(event, target);
  }
  isOverStartHandle(event, target) {
    var _this$getHandleRect;
    return (_this$getHandleRect = this.getHandleRect('start', event, target)) === null || _this$getHandleRect === void 0 ? void 0 : _this$getHandleRect.contains(EventHelper.getPagePoint(event));
  }
  isOverEndHandle(event, target) {
    var _this$getHandleRect2;
    return (_this$getHandleRect2 = this.getHandleRect('end', event, target)) === null || _this$getHandleRect2 === void 0 ? void 0 : _this$getHandleRect2.contains(EventHelper.getPagePoint(event));
  }
  getHandleRect(side, event, eventEl) {
    if (this.overItem) {
      eventEl = event.target.closest(`.${this.client.eventCls}`) || eventEl.querySelector(`.${this.client.eventCls}`);
      if (!eventEl) {
        return;
      }
      const me = this,
        start = side === 'start',
        {
          client
        } = me,
        rtl = Boolean(client.rtl),
        axis = me.dragLock,
        horizontal = axis === 'x',
        dim = horizontal ? 'width' : 'height',
        handleSpec = `${horizontal ? start && !rtl ? 'left' : 'right' : start ? 'top' : 'bottom'}Handle`,
        {
          offsetWidth
        } = eventEl,
        timespanRecord = client.resolveTimeSpanRecord(eventEl),
        resizable = timespanRecord === null || timespanRecord === void 0 ? void 0 : timespanRecord.isResizable,
        eventRect = Rectangle.from(eventEl),
        result = eventRect.clone(),
        handleStyle = globalThis.getComputedStyle(eventEl, ':before'),
        // Larger draggable zones on pure touch devices with no mouse
        touchHandleSize = !me.handleSelector && !BrowserHelper.isHoverableDevice ? me.touchHandleSize : undefined,
        handleSize = touchHandleSize || me.handleSize || parseFloat(handleStyle[dim]),
        handleVisThresh = me.handleVisibilityThreshold || 2 * me.handleSize,
        centerGap = me.dynamicHandleSize ? me.reservedSpace / 2 : 0,
        deflateArgs = [0, 0, 0, 0];
      // To decide if we are over a valid handle, we first check disabled state
      // Then this.leftHandle/this.rightHandle/this.topHandle/this.bottomHandle
      // Then whether there's enough event bar width to accommodate separate handles
      // Then whether the event itself allows resizing at the specified side.
      if (!me.disabled && me[handleSpec] && (offsetWidth >= handleVisThresh || me.dynamicHandleSize) && (resizable === true || resizable === side)) {
        const oppositeEnd = !horizontal && !start || horizontal && rtl === start;
        if (oppositeEnd) {
          // Push handle start point to other end and clip result to other end
          result[axis] += eventRect[dim] - handleSize;
          deflateArgs[horizontal ? 3 : 0] = eventRect[dim] / 2 + centerGap;
        } else {
          deflateArgs[horizontal ? 1 : 2] = eventRect[dim] / 2 + centerGap;
        }
        // Deflate the event bar rectangle to encapsulate 2px less than the side's own half
        // so that we can constrain the handle zone to be inside its own half when bar is small.
        eventRect.deflate(...deflateArgs);
        result[dim] = handleSize;
        // Constrain handle rectangles to each side so that they can never collide.
        // Each handle is constrained into its own half.
        result.constrainTo(eventRect);
        // Zero sized handles cannot be hovered
        if (result[dim]) {
          return result;
        }
      }
    }
  }
  setupDragContext(event) {
    const me = this;
    // Only start a drag if we are over a handle zone.
    if (me.overItem && me.isOverAnyHandle(event, me.overItem) && me.isElementResizable(me.overItem, event)) {
      const result = super.setupDragContext(event);
      result.scrollManager = me.client.scrollManager;
      return result;
    }
  }
  changeHandleSize() {
    VersionHelper.deprecate('Scheduler', '6.0.0', 'Handle size is from CSS');
  }
  changeTouchHandleSize() {
    VersionHelper.deprecate('Scheduler', '6.0.0', 'Handle size is from CSS');
  }
  changeTip(tip, oldTip) {
    const me = this;
    if (!me.showTooltip) {
      return null;
    }
    if (tip) {
      if (tip.isTooltip) {
        tip.owner = me;
      } else {
        tip = Tooltip.reconfigure(oldTip, Tooltip.mergeConfigs({
          id: me.tipId
        }, tip, {
          getHtml: me.getTipHtml.bind(me),
          owner: me.client
        }, me.tip), {
          owner: me,
          defaults: {
            type: 'tooltip'
          }
        });
      }
      tip.ion({
        innerhtmlupdate: 'updateDateIndicator',
        thisObj: me
      });
      me.clockTemplate = new ClockTemplate({
        scheduler: me.client
      });
    } else if (oldTip) {
      var _me$clockTemplate;
      oldTip.destroy();
      (_me$clockTemplate = me.clockTemplate) === null || _me$clockTemplate === void 0 ? void 0 : _me$clockTemplate.destroy();
    }
    return tip;
  }
  //endregion
  //region Events
  isElementResizable(element, event) {
    var _element;
    const me = this,
      {
        client
      } = me,
      timespanRecord = client.resolveTimeSpanRecord(element);
    if (client.readOnly) {
      return false;
    }
    let resizable = timespanRecord === null || timespanRecord === void 0 ? void 0 : timespanRecord.isResizable;
    // Not resizable if the mousedown is on a resizing handle of
    // a percent bar.
    const handleHoldingElement = ((_element = element) === null || _element === void 0 ? void 0 : _element.syncIdMap[client.scheduledEventName]) ?? element,
      handleEl = event.target.closest('[class$="-handle"]');
    if (!resizable || handleEl && handleEl !== handleHoldingElement) {
      return false;
    }
    element = event.target.closest(me.dragSelector);
    if (!element) {
      return false;
    }
    const startsOutside = element.classList.contains('b-sch-event-startsoutside'),
      endsOutside = element.classList.contains('b-sch-event-endsoutside');
    if (resizable === true) {
      if (startsOutside && endsOutside) {
        return false;
      } else if (startsOutside) {
        resizable = 'end';
      } else if (endsOutside) {
        resizable = 'start';
      } else {
        return me.isOverStartHandle(event, element) || me.isOverEndHandle(event, element);
      }
    }
    if (startsOutside && resizable === 'start' || endsOutside && resizable === 'end') {
      return false;
    }
    if (me.isOverStartHandle(event, element) && resizable === 'start' || me.isOverEndHandle(event, element) && resizable === 'end') {
      return true;
    }
    return false;
  }
  updateDateIndicator() {
    const {
        clockTemplate
      } = this,
      {
        eventRecord,
        draggingEnd,
        snappedDate
      } = this.dragging.context,
      startDate = draggingEnd ? eventRecord.get('startDate') : snappedDate,
      endDate = draggingEnd ? snappedDate : eventRecord.get('endDate'),
      {
        element
      } = this.tip;
    clockTemplate.updateDateIndicator(element.querySelector('.b-sch-tooltip-startdate'), startDate);
    clockTemplate.updateDateIndicator(element.querySelector('.b-sch-tooltip-enddate'), endDate);
  }
  getTooltipTarget({
    itemElement,
    context
  }) {
    const me = this,
      {
        rtl
      } = me.client,
      target = Rectangle.from(itemElement, null, true);
    if (me.dragLock === 'x') {
      // Align to the dragged edge of the proxy, and then bump right so that the anchor aligns perfectly.
      if (!rtl && context.edge === 'right' || rtl && context.edge === 'left') {
        target.x = target.right - 1;
      } else {
        target.x -= me.tip.anchorSize[0] / 2;
      }
      target.width = me.tip.anchorSize[0] / 2;
    } else {
      // Align to the dragged edge of the proxy, and then bump bottom so that the anchor aligns perfectly.
      if (context.edge === 'bottom') {
        target.y = target.bottom - 1;
      }
      target.height = me.tip.anchorSize[1] / 2;
    }
    return {
      target
    };
  }
  basicValidityCheck(context, event) {
    return context.startDate && (context.endDate > context.startDate || this.allowResizeToZero) && this.validatorFn.call(this.validatorFnThisObj || this, context, event);
  }
  //endregion
  //region Tooltip
  getTipHtml({
    tip
  }) {
    const me = this,
      {
        startDate,
        endDate,
        toSet,
        snappedDate,
        valid,
        message = '',
        timespanRecord
      } = me.dragging.context;
    // Empty string hides the tip - we get called before the Resizer, so first call will be empty
    if (!startDate || !endDate) {
      return tip.html;
    }
    // Set whichever one we are moving
    const tipData = {
      record: timespanRecord,
      valid,
      message,
      startDate,
      endDate,
      [toSet]: snappedDate
    };
    // Format the two ends. This has to be done outside of the object initializer
    // because they use properties that are only in the tipData object.
    tipData.startText = me.client.getFormattedDate(tipData.startDate);
    tipData.endText = me.client.getFormattedDate(tipData.endDate);
    tipData.startClockHtml = me.clockTemplate.template({
      date: tipData.startDate,
      text: tipData.startText,
      cls: 'b-sch-tooltip-startdate'
    });
    tipData.endClockHtml = me.clockTemplate.template({
      date: tipData.endDate,
      text: tipData.endText,
      cls: 'b-sch-tooltip-enddate'
    });
    return me.tooltipTemplate(tipData);
  }
  //endregion
  //region Product specific, may be overridden in subclasses
  beginEventRecordBatch(eventRecord) {
    eventRecord.beginBatch();
  }
  cancelEventRecordBatch(eventRecord) {
    // Reverts the changes, a batchedUpdate event will fire which will reset the UI
    eventRecord.cancelBatch();
  }
  getBeforeResizeParams(context) {
    const {
      client
    } = this;
    return {
      resourceRecord: client.resolveResourceRecord(client.isVertical ? context.event : context.element)
    };
  }
  getResizeStartParams(context) {
    return {
      resourceRecord: context.resourceRecord
    };
  }
  getResizeEndParams(context) {
    return {
      resourceRecord: context.resourceRecord,
      event: context.event
    };
  }
  setupProductResizeContext(context, event) {
    var _client$resolveResour, _client$resolveAssign2, _client$getDateConstr;
    const {
        client
      } = this,
      {
        element
      } = context,
      eventRecord = client.resolveEventRecord(element),
      resourceRecord = (_client$resolveResour = client.resolveResourceRecord) === null || _client$resolveResour === void 0 ? void 0 : _client$resolveResour.call(client, element),
      assignmentRecord = (_client$resolveAssign2 = client.resolveAssignmentRecord) === null || _client$resolveAssign2 === void 0 ? void 0 : _client$resolveAssign2.call(client, element);
    Object.assign(context, {
      eventRecord,
      taskRecord: eventRecord,
      resourceRecord,
      assignmentRecord,
      dateConstraints: (_client$getDateConstr = client.getDateConstraints) === null || _client$getDateConstr === void 0 ? void 0 : _client$getDateConstr.call(client, resourceRecord, eventRecord)
    });
  }
  checkValidity({
    startDate,
    endDate,
    eventRecord,
    resourceRecord
  }) {
    const {
      client
    } = this;
    if (!client.allowOverlap) {
      if (eventRecord.resources.some(resource => !client.isDateRangeAvailable(startDate, endDate, eventRecord, resource))) {
        return {
          valid: false,
          message: this.L('L{EventDrag.eventOverlapsExisting}')
        };
      }
    }
    return this.basicValidityCheck(...arguments);
  }
  get tipId() {
    return `${this.client.id}-event-resize-tip`;
  }
  //endregion
}

EventResize._$name = 'EventResize';
GridFeatureManager.registerFeature(EventResize, true, 'Scheduler');
GridFeatureManager.registerFeature(EventResize, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/mixin/TaskEditTransactional
 */
/**
 * Mixin adding live updates support
 *
 * @mixin
 */
var TaskEditTransactional = (Target => class TaskEditTransactional extends (Target || Base$1) {
  static get $name() {
    return 'TaskEditTransactional';
  }
  captureStm(force) {
    if (this.client.transactionalFeaturesEnabled) {
      super.captureStm();
      return this.startStmTransaction(force);
    } else {
      super.captureStm(force);
    }
  }
  freeStm(commitOrReject) {
    if (this.hasStmCapture || !this.client.transactionalFeaturesEnabled) {
      return super.freeStm(commitOrReject);
    }
  }
  async startStmTransaction(startRecordingEarly) {
    if (this.client.transactionalFeaturesEnabled) {
      await this.startFeatureTransaction(startRecordingEarly);
    } else {
      super.startStmTransaction();
    }
  }
  commitStmTransaction() {
    if (this.client.transactionalFeaturesEnabled) {
      return this.finishFeatureTransaction();
    } else {
      super.commitStmTransaction();
    }
  }
  async rejectStmTransaction() {
    if (this.client.transactionalFeaturesEnabled) {
      this.rejectFeatureTransaction();
    } else {
      await super.rejectStmTransaction();
    }
  }
});

/**
 * @module Scheduler/feature/mixin/TransactionalFeature
 */
/**
 * Feature defining methods to lock the view for a time of a user action
 * @internal
 * @mixin
 */
var TransactionalFeature = (Target => class TransactionalFeature extends AttachToProjectMixin(Target || Base$1) {
  static $name = 'TransactionalFeature';
  //#region AttachToProjectMixin implementation
  detachFromProject(project) {
    this.rejectFeatureTransaction();
    super.detachFromProject(project);
  }
  //#endregion
  getStmCapture() {
    const result = super.getStmCapture();
    result._editorPromiseResolve = this._editorPromiseResolve;
    return result;
  }
  applyStmCapture(stmCapture) {
    super.applyStmCapture(stmCapture);
    this._editorPromiseResolve = stmCapture._editorPromiseResolve;
  }
  async startFeatureTransaction() {
    if (!this.client.transactionalFeaturesEnabled) {
      return;
    }
    const me = this,
      {
        project
      } = me.client,
      {
        stm
      } = project;
    // Await previous promise chain to resolve
    let chainResolved;
    if (me.hasStmCapture) {
      stm.startTransaction();
    } else {
      chainResolved = project.queue(() => project.commitAsync());
    }
    project.queue(() => {
      var _me$trigger;
      if (!me.hasStmCapture) {
        me._stmInitiallyDisabled = stm.disabled;
        me._stmInitiallyAutoRecord = stm.autoRecord;
        if (stm.isRecording) {
          stm.stopTransaction();
        } else if (me._stmInitiallyDisabled) {
          stm.enable();
        }
        // Disable autoRecord to avoid finishing transaction after a timeout
        stm.autoRecord = false;
      }
      if (!stm.isRecording) {
        // We need to wrap cell editing into own transaction to be able to apply user changes last
        stm.startTransaction();
      }
      (_me$trigger = me.trigger) === null || _me$trigger === void 0 ? void 0 : _me$trigger.call(me, 'featureTransactionStart');
      // Put an empty promise to the queue to pause it
      return new Promise(resolve => me._editorPromiseResolve = resolve);
    });
    await chainResolved;
  }
  rejectFeatureTransaction() {
    var _me$_editorPromiseRes;
    if (!this.client.transactionalFeaturesEnabled) {
      return;
    }
    const me = this,
      {
        stm
      } = me.client.project;
    (_me$_editorPromiseRes = me._editorPromiseResolve) === null || _me$_editorPromiseRes === void 0 ? void 0 : _me$_editorPromiseRes.call(me);
    me._editorPromiseResolve = null;
    stm.isRecording && stm.rejectTransaction();
    if (!me.hasStmCapture && me._stmInitiallyDisabled != null) {
      stm.disabled = me._stmInitiallyDisabled;
      stm.autoRecord = me._stmInitiallyAutoRecord;
    }
    me.trigger('featureTransactionReject');
  }
  async finishFeatureTransaction(afterApplyStashCallback) {
    var _me$_editorPromiseRes2;
    if (!this.client.transactionalFeaturesEnabled) {
      return;
    }
    const me = this,
      {
        project
      } = me.client,
      {
        stm
      } = project;
    // In case there is a commit pending, we need to wait to not suspend more events than we should
    if (!project.isEngineReady()) {
      await project.commitAsync();
    }
    const transactionId = stm.stash(),
      {
        _stmInitiallyDisabled,
        _stmInitiallyAutoRecord
      } = me,
      // This id is used to help debugging concurrent promises
      id = IdHelper.generateId('featureTransaction');
    (_me$_editorPromiseRes2 = me._editorPromiseResolve) === null || _me$_editorPromiseRes2 === void 0 ? void 0 : _me$_editorPromiseRes2.call(me);
    me._editorPromiseResolve = null;
    if (!me.isDestroying) {
      me.trigger('featureTransactionFinalizeStart', {
        id
      });
    }
    return project.queue(async () => {
      var _project$commitAsync, _me$trigger2;
      stm === null || stm === void 0 ? void 0 : stm.applyStash(transactionId);
      await (afterApplyStashCallback === null || afterApplyStashCallback === void 0 ? void 0 : afterApplyStashCallback());
      await ((_project$commitAsync = project.commitAsync) === null || _project$commitAsync === void 0 ? void 0 : _project$commitAsync.call(project));
      if (stm.isRecording) {
        stm.stopTransaction();
      }
      if (!me.hasStmCapture && stm && !stm.isDestroying && _stmInitiallyDisabled != null) {
        stm.disabled = _stmInitiallyDisabled;
        stm.autoRecord = _stmInitiallyAutoRecord;
      }
      (_me$trigger2 = me.trigger) === null || _me$trigger2 === void 0 ? void 0 : _me$trigger2.call(me, 'featureTransactionFinalized', {
        id
      });
    });
  }
});

/**
 * @module Scheduler/feature/base/DragCreateBase
 */
const getDragCreateDragDistance = function (event) {
  var _this$source, _this$source$client$f;
  // Do not allow the drag to begin if the taskEdit feature (if present) is in the process
  // of canceling. We must wait for it to have cleaned up its data manipulations before
  // we can add the new, drag-created record
  if ((_this$source = this.source) !== null && _this$source !== void 0 && (_this$source$client$f = _this$source.client.features.taskEdit) !== null && _this$source$client$f !== void 0 && _this$source$client$f._canceling) {
    return false;
  }
  return EventHelper.getDistanceBetween(this.startEvent, event);
};
/**
 * Base class for EventDragCreate (Scheduler) and TaskDragCreate (Gantt) features. Contains shared code. Not to be used directly.
 *
 * @extends Scheduler/feature/EventResize
 */
class DragCreateBase extends EventResize.mixin(TaskEditStm, TransactionalFeature, TaskEditTransactional) {
  //region Config
  static configurable = {
    /**
     * true to show a time tooltip when dragging to create a new event
     * @config {Boolean}
     * @default
     */
    showTooltip: true,
    /**
     * Number of pixels the drag target must be moved before dragging is considered to have started. Defaults to 2.
     * @config {Number}
     * @default
     */
    dragTolerance: 2,
    // used by gantt to only allow one task per row
    preventMultiple: false,
    dragTouchStartDelay: 300,
    /**
     * `this` reference for the validatorFn
     * @config {Object}
     */
    validatorFnThisObj: null,
    tipTemplate: data => `
            <div class="b-sch-tip-${data.valid ? 'valid' : 'invalid'}">
                ${data.startClockHtml}
                ${data.endClockHtml}
                <div class="b-sch-tip-message">${data.message}</div>
            </div>
        `,
    dragActiveCls: 'b-dragcreating'
  };
  static pluginConfig = {
    chain: ['render', 'onEventDataGenerated'],
    before: ['onElementContextMenu']
  };
  construct(scheduler, config) {
    if ((config === null || config === void 0 ? void 0 : config.showTooltip) === false) {
      config.tip = null;
    }
    super.construct(...arguments);
  }
  //endregion
  changeValidatorFn(validatorFn) {
    // validatorFn property is used by the EventResize base to validate each mousemove
    // We change the property name to createValidatorFn
    this.createValidatorFn = validatorFn;
  }
  render() {
    const me = this,
      {
        client
      } = me;
    // Set up elements and listeners
    me.dragRootElement = me.dropRootElement = client.timeAxisSubGridElement;
    // Drag only in time dimension
    me.dragLock = client.isVertical ? 'y' : 'x';
  }
  onDragEndSwitch(context) {
    const {
        client
      } = this,
      {
        enableEventAnimations
      } = client,
      {
        eventRecord,
        draggingEnd
      } = context,
      horizontal = this.dragLock === 'x',
      {
        initialDate
      } = this.dragging;
    // Setting the new opposite end should not animate
    client.enableEventAnimations = false;
    // Zero duration at the moment of the flip
    eventRecord.set({
      startDate: initialDate,
      endDate: initialDate
    });
    // We're switching to dragging the start
    if (draggingEnd) {
      Object.assign(context, {
        endDate: initialDate,
        toSet: 'startDate',
        otherEnd: 'endDate',
        setMethod: 'setStartDate',
        setOtherMethod: 'setEndDate',
        edge: horizontal ? 'left' : 'top'
      });
    } else {
      Object.assign(context, {
        startDate: initialDate,
        toSet: 'endDate',
        otherEnd: 'startDate',
        setMethod: 'setEndDate',
        setOtherMethod: 'setStartDate',
        edge: horizontal ? 'right' : 'bottom'
      });
    }
    context.draggingEnd = this.draggingEnd = !draggingEnd;
    client.enableEventAnimations = enableEventAnimations;
  }
  beforeDrag(drag) {
    const me = this,
      result = super.beforeDrag(drag),
      {
        pan,
        eventDragSelect
      } = me.client.features;
    // Superclass's handler may also veto
    if (result !== false && (
    // used by gantt to only allow one task per row
    me.preventMultiple && !me.isRowEmpty(drag.rowRecord) || me.disabled ||
    // If Pan is enabled, it has right of way
    pan && !pan.disabled ||
    // If EventDragSelect is enabled, it has right of way
    eventDragSelect && !eventDragSelect.disabled)) {
      return false;
    }
    // Prevent drag select if drag-creating, could collide otherwise
    // (reset by GridSelection)
    me.client.preventDragSelect = true;
    return result;
  }
  startDrag(drag) {
    const result = super.startDrag(drag);
    // Returning false means operation is aborted.
    if (result !== false) {
      const {
        context
      } = drag;
      // Date to flip around when changing direction
      drag.initialDate = context.eventRecord.get(this.draggingEnd ? 'startDate' : 'endDate');
      this.client.trigger('dragCreateStart', {
        proxyElement: drag.element,
        eventElement: drag.element,
        eventRecord: context.eventRecord,
        resourceRecord: context.resourceRecord
      });
      // We are always dragging the exact edge of the event element.
      drag.context.offset = 0;
      drag.context.oldValue = drag.mousedownDate;
    }
    return result;
  }
  // Used by our EventResize superclass to know whether the drag point is the end or the beginning.
  isOverEndHandle() {
    return this.draggingEnd;
  }
  setupDragContext(event) {
    const {
      client
    } = this;
    // Only mousedown on an empty cell can initiate drag-create
    if (client.matchScheduleCell(event.target)) {
      var _client$resolveResour;
      const resourceRecord = (_client$resolveResour = client.resolveResourceRecord(event)) === null || _client$resolveResour === void 0 ? void 0 : _client$resolveResour.$original;
      // And there must be a resource backing the cell.
      if (resourceRecord && !resourceRecord.isSpecialRow) {
        // Skip the EventResize's setupDragContext. We want the base one.
        const result = Draggable().prototype.setupDragContext.call(this, event),
          scrollables = [];
        if (client.isVertical) {
          scrollables.push({
            element: client.scrollable.element,
            direction: 'vertical'
          });
        } else {
          scrollables.push({
            element: client.timeAxisSubGrid.scrollable.element,
            direction: 'horizontal'
          });
        }
        result.scrollManager = client.scrollManager;
        result.monitoringConfig = {
          scrollables
        };
        result.resourceRecord = result.rowRecord = resourceRecord;
        // We use a special method to get the distance moved.
        // If the TaskEdit feature is still in its canceling phase, then
        // it returns false which inhibits the start of the drag-create
        // until the cancelation is complete.
        result.getDistance = getDragCreateDragDistance;
        return result;
      }
    }
  }
  async dragDrop({
    context,
    event
  }) {
    var _this$tip;
    // Set the start/end date, whichever we were dragging
    // to the correctly rounded value before updating.
    context[context.toSet] = context.snappedDate;
    const {
        client
      } = this,
      {
        startDate,
        endDate,
        eventRecord
      } = context,
      {
        generation
      } = eventRecord;
    let modified;
    (_this$tip = this.tip) === null || _this$tip === void 0 ? void 0 : _this$tip.hide();
    // Handle https://github.com/bryntum/support/issues/3210.
    // The issue arises when the mouseup arrives very quickly and the commit kicked off
    // at event add has not yet completed. If it now completes *after* we finalize
    // the drag, it will reset the event to its initial state.
    // If that commit has in fact finished, this will be a no-op
    await client.project.commitAsync();
    // If the above commit in fact reset the event back to the initial state, we have to
    // force the event rendering to bring it back to the currently known context state.
    if (eventRecord.generation !== generation) {
      context.eventRecord[context.toSet] = context.oldValue;
      context.eventRecord[context.toSet] = context[context.toSet];
    }
    context.valid = startDate && endDate && endDate - startDate > 0 &&
    // Input sanity check
    context[context.toSet] - context.oldValue &&
    // Make sure dragged end changed
    context.valid !== false;
    if (context.valid) {
      // Seems to be a valid drag-create operation, ask outside world if anyone wants to take control over the finalizing,
      // to show a confirm dialog prior to finalizing the create.
      client.trigger('beforeDragCreateFinalize', {
        context,
        event,
        proxyElement: context.element,
        eventElement: context.element,
        eventRecord: context.eventRecord,
        resourceRecord: context.resourceRecord
      });
      modified = true;
    }
    // If a handler has set the async flag, it means that they are going to finalize
    // the operation at some time in the future, so we should not call it.
    if (!context.async) {
      await context.finalize(modified);
    }
  }
  updateDragTolerance(dragTolerance) {
    this.dragThreshold = dragTolerance;
  }
  //region Tooltip
  changeTip(tip, oldTip) {
    return super.changeTip(!tip || tip.isTooltip ? tip : ObjectHelper.assign({
      id: `${this.client.id}-drag-create-tip`
    }, tip), oldTip);
  }
  //endregion
  //region Finalize (create EventModel)
  // this method is actually called on the `context` object,
  // so `this` object inside might not be what you think (see `me = this.owner` below)
  // not clear what was the motivation for such design
  async finalize(doCreate) {
    // only call this method once, do not re-enter
    if (this.finalized) {
      return;
    }
    this.finalized = true;
    const me = this.owner,
      context = this,
      completeFinalization = () => {
        if (!me.isDestroyed) {
          me.client.trigger('afterDragCreate', {
            proxyElement: context.element,
            eventElement: context.element,
            eventRecord: context.eventRecord,
            resourceRecord: context.resourceRecord
          });
          me.cleanup(context);
        }
      };
    if (doCreate) {
      // Call product specific implementation
      await me.finalizeDragCreate(context);
      completeFinalization();
    }
    // Aborting without going ahead with create - we must deassign and remove the event
    else {
      var _me$onAborted;
      await me.cancelDragCreate(context);
      (_me$onAborted = me.onAborted) === null || _me$onAborted === void 0 ? void 0 : _me$onAborted.call(me, context);
      completeFinalization();
    }
  }
  async cancelDragCreate(context) {}
  async finalizeDragCreate(context) {
    var _this$client, _this$client2;
    // EventResize base class applies final changes to the event record
    await this.internalUpdateRecord(context, context.eventRecord);
    const stmCapture = this.getStmCapture();
    (_this$client = this.client) === null || _this$client === void 0 ? void 0 : _this$client.trigger('dragCreateEnd', {
      eventRecord: context.eventRecord,
      resourceRecord: context.resourceRecord,
      event: context.event,
      eventElement: context.element,
      stmCapture
    });
    // Part of the Scheduler API. Triggered by its createEvent method.
    // Auto-editing features can use this to edit new events.
    // Note that this may be destroyed by a listener of the previous event.
    (_this$client2 = this.client) === null || _this$client2 === void 0 ? void 0 : _this$client2.trigger('eventAutoCreated', {
      eventRecord: context.eventRecord,
      resourceRecord: context.resourceRecord
    });
    return stmCapture.transferred;
  }
  cleanup(context) {
    var _this$tip2;
    const {
        client
      } = this,
      {
        eventRecord
      } = context;
    // Base class's cleanup is not called, we have to clear this flag.
    // The isCreating flag is only set if the event is to be handed off to the
    // eventEdit feature and that feature then has responsibility for clearing it.
    eventRecord.meta.isResizing = false;
    client.endListeningForBatchedUpdates();
    (_this$tip2 = this.tip) === null || _this$tip2 === void 0 ? void 0 : _this$tip2.hide();
    client.element.classList.remove(...this.dragActiveCls.split(' '));
    context.element.parentElement.classList.remove('b-sch-dragcreating');
  }
  //endregion
  //region Events
  /**
   * Prevent right click when drag creating
   * @returns {Boolean}
   * @private
   */
  onElementContextMenu() {
    if (this.proxy) {
      return false;
    }
  }
  prepareCreateContextForFinalization(createContext, event, finalize, async = false) {
    return {
      ...createContext,
      async,
      event,
      finalize
    };
  }
  // Apply drag create "proxy" styling
  onEventDataGenerated(renderData) {
    var _this$dragging, _this$dragging$contex;
    if (((_this$dragging = this.dragging) === null || _this$dragging === void 0 ? void 0 : (_this$dragging$contex = _this$dragging.context) === null || _this$dragging$contex === void 0 ? void 0 : _this$dragging$contex.eventRecord) === renderData.eventRecord) {
      // Allow custom styling for drag creation element
      renderData.wrapperCls['b-sch-dragcreating'] = true;
      // Styling when drag create will be aborted on drop (because it would yield zero duration)
      renderData.wrapperCls['b-too-narrow'] = this.dragging.context.tooNarrow;
    }
  }
  //endregion
  //region Product specific, implemented in subclasses
  // Empty implementation here. Only base EventResize class triggers this
  triggerBeforeResize() {}
  // Empty implementation here. Only base EventResize class triggers this
  triggerEventResizeStart() {}
  checkValidity(context, event) {
    throw new Error('Implement in subclass');
  }
  handleBeforeDragCreate(dateTime, event) {
    throw new Error('Implement in subclass');
  }
  isRowEmpty(rowRecord) {
    throw new Error('Implement in subclass');
  }
  //endregion
}

DragCreateBase._$name = 'DragCreateBase';

/**
 * @module Scheduler/feature/base/TooltipBase
 */
/**
 * Base class for `EventTooltip` (Scheduler) and `TaskTooltip` (Gantt) features. Contains shared code. Not to be used directly.
 *
 * @extends Core/mixin/InstancePlugin
 * @extendsconfigs Core/widget/Tooltip
 */
class TooltipBase extends InstancePlugin {
  //region Config
  static get defaultConfig() {
    return {
      /**
       * Specify true to have tooltip updated when mouse moves, if you for example want to display date at mouse
       * position.
       * @config {Boolean}
       * @default
       * @category Misc
       */
      autoUpdate: false,
      /**
       * The amount of time to hover before showing
       * @config {Number}
       * @default
       */
      hoverDelay: 250,
      /**
       * The time (in milliseconds) for which the Tooltip remains visible when the mouse leaves the target.
       *
       * May be configured as `false` to persist visible after the mouse exits the target element. Configure it
       * as 0 to always retrigger `hoverDelay` even when moving mouse inside `fromElement`
       * @config {Number}
       * @default
       */
      hideDelay: 100,
      template: null,
      cls: null,
      align: {
        align: 'b-t'
      },
      clockTemplate: null,
      // Set to true to update tooltip contents if record changes while tip is open
      monitorRecordUpdate: null,
      testConfig: {
        hoverDelay: 0
      }
    };
  }
  // Plugin configuration. This plugin chains some of the functions in Grid.
  static get pluginConfig() {
    return {
      chain: ['onPaint']
    };
  }
  //endregion
  //region Events
  /**
   * Triggered before a tooltip is shown. Return `false` to prevent the action.
   * @preventable
   * @event beforeShow
   * @param {Core.widget.Tooltip} source The tooltip being shown.
   * @param {Scheduler.model.EventModel} source.eventRecord The event record.
   */
  /**
   * Triggered after a tooltip is shown.
   * @event show
   * @param {Core.widget.Tooltip} source The tooltip.
   * @param {Scheduler.model.EventModel} source.eventRecord The event record.
   */
  //endregion
  //region Init
  construct(client, config) {
    const me = this;
    // process initial config into an actual config object
    config = me.processConfig(config);
    super.construct(client, config);
    // Default triggering selector is the client's inner element selector
    if (!me.forSelector) {
      me.forSelector = `${client.eventInnerSelector}:not(.b-dragproxy,.b-iscreating)`;
    }
    me.clockTemplate = new ClockTemplate({
      scheduler: client
    });
    client.ion({
      [`before${client.scheduledEventName}drag`]: () => {
        var _me$tooltip;
        // Using {} on purpose to not return the promise
        (_me$tooltip = me.tooltip) === null || _me$tooltip === void 0 ? void 0 : _me$tooltip.hide();
      }
    });
  }
  // TooltipBase feature handles special config cases, where user can supply a function to use as template
  // instead of a normal config object
  processConfig(config) {
    if (typeof config === 'function') {
      return {
        template: config
      };
    }
    return config;
  }
  // override setConfig to process config before applying it (used mainly from ReactScheduler)
  setConfig(config) {
    super.setConfig(this.processConfig(config));
  }
  doDestroy() {
    this.destroyProperties('clockTemplate', 'tooltip');
    super.doDestroy();
  }
  doDisable(disable) {
    if (this.tooltip) {
      this.tooltip.disabled = disable;
    }
    super.doDisable(disable);
  }
  //endregion
  onPaint({
    firstPaint
  }) {
    if (firstPaint) {
      var _me$tooltip2;
      const me = this,
        {
          client
        } = me,
        ignoreSelector = `:not(${['.b-dragselecting', '.b-eventeditor-editing', '.b-taskeditor-editing', '.b-resizing-event', '.b-task-percent-bar-resizing-task', '.b-dragcreating', `.b-dragging-${client.scheduledEventName}`, '.b-creating-dependency', '.b-dragproxy'].join()})`;
      (_me$tooltip2 = me.tooltip) === null || _me$tooltip2 === void 0 ? void 0 : _me$tooltip2.destroy();
      /**
       * A reference to the tooltip instance, which will have a special `eventRecord` property that
       * you can use to get data from the contextual event record to which this tooltip is related.
       * @member {Core.widget.Tooltip} tooltip
       * @readonly
       * @category Misc
       */
      const tip = me.tooltip = new Tooltip({
        axisLock: 'flexible',
        id: me.tipId || `${me.client.id}-event-tip`,
        cls: me.tipCls,
        forSelector: `.b-timelinebase${ignoreSelector} .b-grid-body-container:not(.b-scrolling) ${me.forSelector}`,
        scrollAction: 'realign',
        forElement: client.timeAxisSubGridElement,
        showOnHover: true,
        anchorToTarget: true,
        getHtml: me.getTipHtml.bind(me),
        disabled: me.disabled,
        // on Core/mixin/Events constructor, me.config.listeners is deleted and attributed its value to me.configuredListeners
        // to then on processConfiguredListeners it set me.listeners to our TooltipBase
        // but since we need our initial config.listeners to set to our internal tooltip, we leave processConfiguredListeners empty
        // to avoid lost our listeners to apply for our internal tooltip here and force our feature has all Tooltip events firing
        ...me.config,
        internalListeners: me.configuredListeners
      });
      tip.ion({
        innerhtmlupdate: 'updateDateIndicator',
        overtarget: 'onOverNewTarget',
        show: 'onTipShow',
        hide: 'onTipHide',
        thisObj: me
      });
      // Once instantiated, any Tooltip configs are relayed through the feature directly to the tip
      Object.keys(tip.$meta.configs).forEach(name => {
        Object.defineProperty(this, name, {
          set: v => tip[name] = v,
          get: () => tip[name]
        });
      });
    }
  }
  //region Listeners
  // leave configuredListeners alone until render time at which they are used on the tooltip
  processConfiguredListeners() {}
  addListener(...args) {
    var _this$tooltip;
    const
      // Call super method to handle enable/disable feature events
      defaultDetacher = super.addListener(...args),
      // Add listener to the `tooltip` instance
      tooltipDetacher = (_this$tooltip = this.tooltip) === null || _this$tooltip === void 0 ? void 0 : _this$tooltip.addListener(...args);
    if (defaultDetacher || tooltipDetacher) {
      return () => {
        defaultDetacher === null || defaultDetacher === void 0 ? void 0 : defaultDetacher();
        tooltipDetacher === null || tooltipDetacher === void 0 ? void 0 : tooltipDetacher();
      };
    }
  }
  removeListener(...args) {
    var _this$tooltip2;
    super.removeListener(...args);
    // Remove listener from the `tooltip` instance
    (_this$tooltip2 = this.tooltip) === null || _this$tooltip2 === void 0 ? void 0 : _this$tooltip2.removeListener(...args);
  }
  //endregion
  updateDateIndicator() {
    const me = this,
      tip = me.tooltip,
      endDateElement = tip.element.querySelector('.b-sch-tooltip-enddate');
    if (!me.record) {
      return;
    }
    me.clockTemplate.updateDateIndicator(tip.element, me.record.startDate);
    endDateElement && me.clockTemplate.updateDateIndicator(endDateElement, me.record.endDate);
  }
  resolveTimeSpanRecord(forElement) {
    return this.client.resolveTimeSpanRecord(forElement);
  }
  getTipHtml({
    tip,
    activeTarget
  }) {
    const me = this,
      {
        client
      } = me,
      recordProp = me.recordType || `${client.scheduledEventName}Record`,
      timeSpanRecord = me.resolveTimeSpanRecord(activeTarget);
    // If user has mouseovered a fading away element of a deleted event,
    // an event record will not be found. In this case the tip must hide.
    // Instance of check is to not display while propagating
    if ((timeSpanRecord === null || timeSpanRecord === void 0 ? void 0 : timeSpanRecord.startDate) instanceof Date) {
      const {
          startDate,
          endDate
        } = timeSpanRecord,
        startText = client.getFormattedDate(startDate),
        endDateValue = client.getDisplayEndDate(endDate, startDate),
        endText = client.getFormattedDate(endDateValue);
      tip.eventRecord = timeSpanRecord;
      return me.template({
        tip,
        // eventRecord for Scheduler, taskRecord for Gantt
        [`${recordProp}`]: timeSpanRecord,
        startDate,
        endDate,
        startText,
        endText,
        startClockHtml: me.clockTemplate.template({
          date: startDate,
          text: startText,
          cls: 'b-sch-tooltip-startdate'
        }),
        endClockHtml: timeSpanRecord.isMilestone ? '' : me.clockTemplate.template({
          date: endDateValue,
          text: endText,
          cls: 'b-sch-tooltip-enddate'
        })
      });
    } else {
      tip.hide();
      return '';
    }
  }
  get record() {
    return this.tooltip.eventRecord;
  }
  onTipShow() {
    const me = this;
    if (me.monitorRecordUpdate && !me.updateListener) {
      me.updateListener = me.client.eventStore.ion({
        change: me.onRecordUpdate,
        buffer: 300,
        thisObj: me
      });
    }
  }
  onTipHide() {
    var _this$updateListener;
    // To not retain full project when changing project
    this.tooltip.eventRecord = null;
    (_this$updateListener = this.updateListener) === null || _this$updateListener === void 0 ? void 0 : _this$updateListener.call(this);
    this.updateListener = null;
  }
  onOverNewTarget({
    newTarget
  }) {
    const {
      tooltip
    } = this;
    if (tooltip.isVisible) {
      if (this.client.timeAxisSubGrid.scrolling || this.client.scrolling) {
        tooltip.hide(false);
      } else {
        tooltip.eventRecord = this.resolveTimeSpanRecord(newTarget);
      }
    }
  }
  onRecordUpdate({
    record
  }) {
    const {
      tooltip
    } = this;
    // Make sure the record we are showing the tip for is still relevant
    // If the change moved the element out from under the mouse, we will be hidden.
    if (tooltip !== null && tooltip !== void 0 && tooltip.isVisible && record === this.record) {
      tooltip.updateContent();
      // If we were aligning to the event bar, realign to it.
      if (tooltip.lastAlignSpec.aligningToElement) {
        tooltip.realign();
      }
      // The pointer is still over the target (otherwise tooltip would be hidden)
      // So invoke the tooltip's positioning
      else {
        tooltip.internalOnPointerOver(this.client.lastPointerEvent);
      }
    }
  }
}
TooltipBase._$name = 'TooltipBase';

/**
 * @module Scheduler/feature/AbstractTimeRanges
 */
/**
 * Abstract base class, you should not use this class directly.
 * @abstract
 * @mixes Core/mixin/Delayable
 * @extends Core/mixin/InstancePlugin
 */
class AbstractTimeRanges extends InstancePlugin.mixin(Delayable) {
  //region Config
  /**
   * Fired on the owning Scheduler when a click happens on a time range header element
   * @event timeRangeHeaderClick
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
   * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler when a double click happens on a time range header element
   * @event timeRangeHeaderDblClick
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
   * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler when a right click happens on a time range header element
   * @event timeRangeHeaderContextMenu
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
   * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
   * @param {MouseEvent} domEvent Browser event
   */
  static get defaultConfig() {
    return {
      // CSS class to apply to range elements
      rangeCls: 'b-sch-range',
      // CSS class to apply to line elements (0-duration time range)
      lineCls: 'b-sch-line',
      /**
       * Set to `true` to enable dragging and resizing of range elements in the header. Only relevant when
       * {@link #config-showHeaderElements} is `true`.
       * @config {Boolean}
       * @default
       * @category Common
       */
      enableResizing: false,
      /**
       * A Boolean specifying whether to show tooltip while resizing range elements, or a
       * {@link Core.widget.Tooltip} config object which is applied to the tooltip
       * @config {Boolean|TooltipConfig}
       * @default
       * @category Common
       */
      showTooltip: true,
      /**
       * Template used to generate the tooltip contents when hovering a time range header element.
       * ```
       * const scheduler = new Scheduler({
       *   features : {
       *     timeRanges : {
       *       tooltipTemplate({ timeRange }) {
       *         return `${timeRange.name}`
       *       }
       *     }
       *   }
       * });
       * ```
       * @config {Function} tooltipTemplate
       * @param {Object} data Tooltip data
       * @param {Scheduler.model.TimeSpan} data.timeRange
       * @category Common
       */
      tooltipTemplate: null,
      dragTipTemplate: data => `
                <div class="b-sch-tip-${data.valid ? 'valid' : 'invalid'}">
                    <div class="b-sch-tip-name">${StringHelper.encodeHtml(data.name) || ''}</div>
                    ${data.startClockHtml}
                    ${data.endClockHtml || ''}
                </div>
            `,
      baseCls: 'b-sch-timerange',
      /**
       * Function used to generate the HTML content for a time range header element.
       * ```
       * const scheduler = new Scheduler({
       *   features : {
       *     timeRanges : {
       *       headerRenderer({ timeRange }) {
       *         return `${timeRange.name}`
       *       }
       *     }
       *   }
       * });
       * ```
       * @config {Function} headerRenderer
       * @param {Object} data Render data
       * @param {Scheduler.model.TimeSpan} data.timeRange
       * @category Common
       */
      headerRenderer: null,
      /**
       * Function used to generate the HTML content for a time range body element.
       * ```
       * const scheduler = new Scheduler({
       *   features : {
       *     timeRanges : {
       *       bodyRenderer({ timeRange }) {
       *         return `${timeRange.name}`
       *       }
       *     }
       *   }
       * });
       * ```
       * @config {Function} bodyRenderer
       * @param {Object} data Render data
       * @param {Scheduler.model.TimeSpan} data.timeRange
       * @category Common
       */
      bodyRenderer: null,
      // a unique cls used by subclasses to get custom styling of the elements rendered
      cls: null,
      narrowThreshold: 80
    };
  }
  static configurable = {
    /**
     * Set to `false` to not render range elements into the time axis header
     * @prp {Boolean}
     * @default
     * @category Common
     */
    showHeaderElements: true
  };
  // Plugin configuration. This plugin chains some functions in Grid.
  static pluginConfig = {
    chain: ['onPaint', 'populateTimeAxisHeaderMenu', 'onSchedulerHorizontalScroll', 'afterScroll', 'onInternalResize']
  };
  //endregion
  //region Init & destroy
  construct(client, config) {
    const me = this;
    super.construct(client, config);
    if (client.isVertical) {
      client.ion({
        renderRows: me.onUIReady,
        thisObj: me,
        once: true
      });
    }
    // Add a unique cls used by subclasses to get custom styling of the elements rendered
    // This makes sure that each class only removed its own elements from the DOM
    me.cls = me.cls || `b-sch-${me.constructor.$$name.toLowerCase()}`;
    me.baseSelector = `.${me.baseCls}.${me.cls}`;
    // header elements are required for interaction
    if (me.enableResizing) {
      me.showHeaderElements = true;
    }
  }
  doDestroy() {
    var _me$clockTemplate, _me$tip, _me$drag, _me$resize;
    const me = this;
    me.detachListeners('timeAxisViewModel');
    me.detachListeners('timeAxis');
    (_me$clockTemplate = me.clockTemplate) === null || _me$clockTemplate === void 0 ? void 0 : _me$clockTemplate.destroy();
    (_me$tip = me.tip) === null || _me$tip === void 0 ? void 0 : _me$tip.destroy();
    (_me$drag = me.drag) === null || _me$drag === void 0 ? void 0 : _me$drag.destroy();
    (_me$resize = me.resize) === null || _me$resize === void 0 ? void 0 : _me$resize.destroy();
    super.doDestroy();
  }
  doDisable(disable) {
    this.renderRanges();
    super.doDisable(disable);
  }
  setupTimeAxisViewModelListeners() {
    const me = this;
    me.detachListeners('timeAxisViewModel');
    me.detachListeners('timeAxis');
    me.client.timeAxisViewModel.ion({
      name: 'timeAxisViewModel',
      update: 'onTimeAxisViewModelUpdate',
      thisObj: me
    });
    me.client.timeAxis.ion({
      name: 'timeAxis',
      includeChange: 'renderRanges',
      thisObj: me
    });
    me.updateLineBuffer();
  }
  onUIReady() {
    const me = this,
      {
        client
      } = me;
    // If timeAxisViewModel is swapped, re-setup listeners to new instance
    client.ion({
      timeAxisViewModelChange: me.setupTimeAxisViewModelListeners,
      thisObj: me
    });
    me.setupTimeAxisViewModelListeners();
    if (!client.hideHeaders) {
      if (me.headerContainerElement) {
        EventHelper.on({
          click: me.onTimeRangeClick,
          dblclick: me.onTimeRangeClick,
          contextmenu: me.onTimeRangeClick,
          delegate: me.baseSelector,
          element: me.headerContainerElement,
          thisObj: me
        });
      }
      if (me.enableResizing) {
        me.drag = DragHelper.new({
          name: 'rangeDrag',
          lockX: client.isVertical,
          lockY: client.isHorizontal,
          constrain: true,
          outerElement: me.headerContainerElement,
          targetSelector: `${me.baseSelector}`,
          isElementDraggable: (el, event) => !client.readOnly && me.isElementDraggable(el, event),
          rtlSource: client,
          internalListeners: {
            dragstart: 'onDragStart',
            drag: 'onDrag',
            drop: 'onDrop',
            reset: 'onDragReset',
            abort: 'onInvalidDrop',
            thisObj: me
          }
        }, me.dragHelperConfig);
        me.resize = ResizeHelper.new({
          direction: client.mode,
          targetSelector: `${me.baseSelector}.b-sch-range`,
          outerElement: me.headerContainerElement,
          isElementResizable: (el, event) => !el.matches('.b-dragging,.b-readonly') && !event.target.matches('.b-fa'),
          internalListeners: {
            resizestart: 'onResizeStart',
            resizing: 'onResizeDrag',
            resize: 'onResize',
            cancel: 'onInvalidResize',
            reset: 'onResizeReset',
            thisObj: me
          }
        }, me.resizeHelperConfig);
      }
    }
    me.renderRanges();
    if (me.tooltipTemplate) {
      me.hoverTooltip = new Tooltip({
        forElement: me.headerContainerElement,
        getHtml({
          activeTarget
        }) {
          const timeRange = me.resolveTimeRangeRecord(activeTarget);
          return me.tooltipTemplate({
            timeRange
          });
        },
        forSelector: '.' + me.baseCls + (me.cls ? '.' + me.cls : '')
      });
    }
  }
  //endregion
  //region Draw
  refresh() {
    this._timeRanges = null;
    this.renderRanges();
  }
  getDOMConfig(startDate, endDate) {
    const me = this,
      bodyConfigs = [],
      headerConfigs = [];
    if (!me.disabled) {
      // clear label rotation map cache here, used to prevent height calculations for every timeRange entry to
      // speed up using recurrences
      me._labelRotationMap = {};
      for (const range of me.timeRanges) {
        const result = me.renderRange(range, startDate, endDate);
        if (result) {
          bodyConfigs.push(result.bodyConfig);
          headerConfigs.push(result.headerConfig);
        }
      }
    }
    return [bodyConfigs, headerConfigs];
  }
  renderRanges() {
    const me = this,
      {
        client
      } = me,
      {
        foregroundCanvas
      } = client;
    // Scheduler/Gantt might not yet be rendered
    if (foregroundCanvas && client.isPainted && !client.timeAxisSubGrid.collapsed) {
      const {
          headerContainerElement
        } = me,
        updatedBodyElements = [],
        [bodyConfigs, headerConfigs] = me.getDOMConfig();
      if (!me.bodyCanvas) {
        me.bodyCanvas = DomHelper.createElement({
          className: `b-timeranges-canvas ${me.cls}-canvas`,
          parent: foregroundCanvas,
          retainElement: true
        });
      }
      DomSync.sync({
        targetElement: me.bodyCanvas,
        childrenOnly: true,
        domConfig: {
          children: bodyConfigs,
          syncOptions: {
            releaseThreshold: 0,
            syncIdField: 'id'
          }
        },
        callback: me.showHeaderElements ? null : ({
          targetElement,
          action
        }) => {
          // Might need to rotate label when not showing header elements
          if (action === 'reuseElement' || action === 'newElement' || action === 'reuseOwnElement') {
            // Collect all here, to not force reflows in the middle of syncing
            updatedBodyElements.push(targetElement);
          }
        }
      });
      if (me.showHeaderElements && !me.headerCanvas) {
        me.headerCanvas = DomHelper.createElement({
          className: `${me.cls}-canvas`,
          parent: headerContainerElement,
          retainElement: true
        });
      }
      if (me.headerCanvas) {
        DomSync.sync({
          targetElement: me.headerCanvas,
          childrenOnly: true,
          domConfig: {
            children: headerConfigs,
            syncOptions: {
              releaseThreshold: 0,
              syncIdField: 'id'
            }
          }
        });
      }
      // Rotate labels last, to not force reflows. First check if rotation is needed
      for (const bodyElement of updatedBodyElements) {
        me.cacheRotation(bodyElement.elementData.timeRange, bodyElement);
      }
      // Then apply rotation
      for (const bodyElement of updatedBodyElements) {
        me.applyRotation(bodyElement.elementData.timeRange, bodyElement);
      }
    }
  }
  // Implement in subclasses
  get timeRanges() {
    return [];
  }
  /**
   * Based on this method result the feature decides whether the provided range should
   * be rendered or not.
   * The method checks that the range intersects the current viewport.
   *
   * Override the method to implement your custom range rendering vetoing logic.
   * @param {Scheduler.model.TimeSpan} range Range to render.
   * @param {Date} [startDate] Specifies view start date. Defaults to view visible range start
   * @param {Date} [endDate] Specifies view end date. Defaults to view visible range end
   * @returns {Boolean} `true` if the range should be rendered and `false` otherwise.
   */
  shouldRenderRange(range, startDate = this.client.visibleDateRange.startDate, endDate = this.client.visibleDateRange.endDate) {
    const {
        timeAxis
      } = this.client,
      {
        startDate: rangeStart,
        endDate: rangeEnd,
        duration
      } = range;
    return Boolean(rangeStart && (timeAxis.isContinuous || timeAxis.isTimeSpanInAxis(range)) && DateHelper.intersectSpans(startDate, endDate, rangeStart,
    // Lines are included longer, to make sure label does not disappear
    duration ? rangeEnd : DateHelper.add(rangeStart, this._lineBufferDurationMS)));
  }
  getRangeDomConfig(timeRange, minDate, maxDate, relativeTo = 0) {
    const me = this,
      {
        client
      } = me,
      {
        rtl
      } = client,
      startPos = client.getCoordinateFromDate(DateHelper.max(timeRange.startDate, minDate), {
        respectExclusion: true
      }) - relativeTo,
      endPos = timeRange.endDate ? client.getCoordinateFromDate(DateHelper.min(timeRange.endDate, maxDate), {
        respectExclusion: true,
        isEnd: true
      }) - relativeTo : startPos,
      size = Math.abs(endPos - startPos),
      isRange = size > 0,
      translateX = rtl ? `calc(${startPos}px - 100%)` : `${startPos}px`;
    return {
      className: {
        [me.baseCls]: 1,
        [me.cls]: me.cls,
        [me.rangeCls]: isRange,
        [me.lineCls]: !isRange,
        [timeRange.cls]: timeRange.cls,
        'b-narrow-range': isRange && size < me.narrowThreshold,
        'b-readonly': timeRange.readOnly,
        'b-rtl': rtl
      },
      dataset: {
        id: timeRange.id
      },
      elementData: {
        timeRange
      },
      style: client.isVertical ? `transform: translateY(${translateX}); ${isRange ? `height:${size}px` : ''};` : `transform: translateX(${translateX}); ${isRange ? `width:${size}px` : ''};`
    };
  }
  renderRange(timeRange, startDate, endDate) {
    const me = this,
      {
        client
      } = me,
      {
        timeAxis
      } = client;
    if (me.shouldRenderRange(timeRange, startDate, endDate) && timeAxis.startDate) {
      const config = me.getRangeDomConfig(timeRange, timeAxis.startDate, timeAxis.endDate),
        icon = timeRange.iconCls && StringHelper.xss`<i class="${timeRange.iconCls}"></i>`,
        name = timeRange.name && StringHelper.encodeHtml(timeRange.name),
        labelTpl = name || icon ? `<label>${icon || ''}${name || '&nbsp;'}</label>` : '',
        bodyConfig = {
          ...config,
          style: config.style + (timeRange.style || ''),
          html: me.bodyRenderer ? me.bodyRenderer({
            timeRange
          }) : me.showHeaderElements && !me.showLabelInBody ? '' : labelTpl
        };
      let headerConfig;
      if (me.showHeaderElements) {
        headerConfig = {
          ...config,
          html: me.headerRenderer ? me.headerRenderer({
            timeRange
          }) : me.showLabelInBody ? '' : labelTpl
        };
      }
      return {
        bodyConfig,
        headerConfig
      };
    }
  }
  // Cache label rotation to not have to calculate for each occurrence when using recurring timeranges
  cacheRotation(range, bodyElement) {
    // Lines have no label. Do not check label content to do not force DOM layout!
    if (!range.iconCls && !range.name || !range.duration) {
      return;
    }
    const label = bodyElement.firstElementChild;
    if (label && !range.recurringTimeSpan) {
      this._labelRotationMap[range.id] = this.client.isVertical ? label.offsetHeight < bodyElement.offsetHeight : label.offsetWidth > bodyElement.offsetWidth;
    }
  }
  applyRotation(range, bodyElement) {
    var _range$recurringTimeS, _bodyElement$firstEle;
    const rotate = this._labelRotationMap[((_range$recurringTimeS = range.recurringTimeSpan) === null || _range$recurringTimeS === void 0 ? void 0 : _range$recurringTimeS.id) ?? range.id];
    (_bodyElement$firstEle = bodyElement.firstElementChild) === null || _bodyElement$firstEle === void 0 ? void 0 : _bodyElement$firstEle.classList.toggle('b-vertical', Boolean(rotate));
  }
  getBodyElementByRecord(idOrRecord) {
    const id = typeof idOrRecord === 'string' ? idOrRecord : idOrRecord === null || idOrRecord === void 0 ? void 0 : idOrRecord.id;
    return id != null && DomSync.getChild(this.bodyCanvas, id);
  }
  // Implement in subclasses
  resolveTimeRangeRecord(el) {}
  get headerContainerElement() {
    const me = this,
      {
        isVertical,
        timeView,
        timeAxisColumn
      } = me.client;
    if (!me._headerContainerElement) {
      // Render into the subGrid´s header element or the vertical timeaxis depending on mode
      if (isVertical && timeView.element) {
        me._headerContainerElement = timeView.element.parentElement;
      } else if (!isVertical) {
        me._headerContainerElement = timeAxisColumn.element;
      }
    }
    return me._headerContainerElement;
  }
  //endregion
  //region Settings
  get showHeaderElements() {
    return !this.client.hideHeaders && this._showHeaderElements;
  }
  updateShowHeaderElements(show) {
    const {
      client
    } = this;
    if (!this.isConfiguring) {
      client.element.classList.toggle('b-sch-timeranges-with-headerelements', Boolean(show));
      this.renderRanges();
    }
  }
  //endregion
  //region Menu items
  /**
   * Adds menu items for the context menu, and may mutate the menu configuration.
   * @param {Object} options Contains menu items and extra data retrieved from the menu target.
   * @param {Grid.column.Column} options.column Column for which the menu will be shown
   * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
   * @internal
   */
  populateTimeAxisHeaderMenu({
    column,
    items
  }) {}
  //endregion
  //region Events & hooks
  onPaint({
    firstPaint
  }) {
    if (firstPaint && this.client.isHorizontal) {
      this.onUIReady();
    }
  }
  onSchedulerHorizontalScroll() {
    // Don't need a refresh, ranges are already available. Just need to draw those now in view
    this.client.isHorizontal && this.renderRanges();
  }
  afterScroll() {
    this.client.isVertical && this.renderRanges();
  }
  updateLineBuffer() {
    const {
      timeAxisViewModel
    } = this.client;
    // Lines have no duration, but we want them to be visible longer for the label to not suddenly disappear.
    // We use a 300px buffer for that, recalculated as an amount of ms
    this._lineBufferDurationMS = timeAxisViewModel.getDateFromPosition(300) - timeAxisViewModel.getDateFromPosition(0);
  }
  onInternalResize(element, newWidth, newHeight, oldWidth, oldHeight) {
    if (this.client.isVertical && oldHeight !== newHeight) {
      this.renderRanges();
    }
  }
  onTimeAxisViewModelUpdate() {
    this.updateLineBuffer();
    this.refresh();
  }
  onTimeRangeClick(event) {
    const timeRangeRecord = this.resolveTimeRangeRecord(event.target);
    this.client.trigger(`timeRangeHeader${StringHelper.capitalize(event.type)}`, {
      event,
      domEvent: event,
      timeRangeRecord
    });
  }
  //endregion
  //region Drag drop
  showTip(context) {
    const me = this;
    if (me.showTooltip) {
      me.clockTemplate = new ClockTemplate({
        scheduler: me.client
      });
      me.tip = new Tooltip(ObjectHelper.assign({
        id: `${me.client.id}-time-range-tip`,
        cls: 'b-interaction-tooltip',
        align: 'b-t',
        autoShow: true,
        updateContentOnMouseMove: true,
        forElement: context.element,
        getHtml: () => me.getTipHtml(context.record, context.element)
      }, me.showTooltip));
    }
  }
  destroyTip() {
    if (this.tip) {
      this.tip.destroy();
      this.tip = null;
    }
  }
  isElementDraggable(el) {
    el = el.closest(this.baseSelector + ':not(.b-resizing):not(.b-readonly)');
    return el && !el.classList.contains('b-over-resize-handle');
  }
  onDragStart({
    context
  }) {
    const {
      client,
      drag
    } = this;
    if (client.isVertical) {
      drag.minY = 0;
      // Moving the range, you can drag the start marker down until the end of the range hits the time axis end
      drag.maxY = client.timeAxisViewModel.totalSize - context.element.offsetHeight;
      // Setting min/max for X makes drag right of the header valid, but visually still constrained vertically
      drag.minX = 0;
      drag.maxX = Number.MAX_SAFE_INTEGER;
    } else {
      drag.minX = 0;
      // Moving the range, you can drag the start marker right until the end of the range hits the time axis end
      drag.maxX = client.timeAxisViewModel.totalSize - context.element.offsetWidth;
      // Setting min/max for Y makes drag below header valid, but visually still constrained horizontally
      drag.minY = 0;
      drag.maxY = Number.MAX_SAFE_INTEGER;
    }
    client.element.classList.add('b-dragging-timerange');
  }
  onDrop({
    context
  }) {
    this.client.element.classList.remove('b-dragging-timerange');
  }
  onInvalidDrop() {
    this.drag.reset();
    this.client.element.classList.remove('b-dragging-timerange');
    this.destroyTip();
  }
  updateDateIndicator({
    startDate,
    endDate
  }) {
    const me = this,
      {
        tip
      } = me,
      endDateElement = tip.element.querySelector('.b-sch-tooltip-enddate');
    me.clockTemplate.updateDateIndicator(tip.element, startDate);
    endDateElement && me.clockTemplate.updateDateIndicator(endDateElement, endDate);
  }
  onDrag({
    context
  }) {
    const me = this,
      {
        client
      } = me,
      box = Rectangle.from(context.element),
      startPos = box.getStart(client.rtl, client.isHorizontal),
      endPos = box.getEnd(client.rtl, client.isHorizontal),
      startDate = client.getDateFromCoordinate(startPos, 'round', false),
      endDate = client.getDateFromCoordinate(endPos, 'round', false);
    me.updateDateIndicator({
      startDate,
      endDate
    });
  }
  onDragReset() {}
  // endregion
  // region Resize
  onResizeStart() {}
  onResizeDrag() {}
  onResize() {}
  onInvalidResize() {}
  onResizeReset() {}
  //endregion
  //region Tooltip
  /**
   * Generates the html to display in the tooltip during drag drop.
   *
   */
  getTipHtml(record, element) {
    const me = this,
      {
        client
      } = me,
      box = Rectangle.from(element),
      startPos = box.getStart(client.rtl, client.isHorizontal),
      endPos = box.getEnd(client.rtl, client.isHorizontal),
      startDate = client.getDateFromCoordinate(startPos, 'round', false),
      endDate = record.endDate && client.getDateFromCoordinate(endPos, 'round', false),
      startText = client.getFormattedDate(startDate),
      endText = endDate && client.getFormattedEndDate(endDate, startDate);
    return me.dragTipTemplate({
      name: record.name || '',
      startDate,
      endDate,
      startText,
      endText,
      startClockHtml: me.clockTemplate.template({
        date: startDate,
        text: startText,
        cls: 'b-sch-tooltip-startdate'
      }),
      endClockHtml: endText && me.clockTemplate.template({
        date: endDate,
        text: endText,
        cls: 'b-sch-tooltip-enddate'
      })
    });
  }
  //endregion
}

AbstractTimeRanges._$name = 'AbstractTimeRanges';

/**
 * @module Scheduler/feature/ColumnLines
 */
const emptyObject$1 = Object.freeze({});
/**
 * Displays column lines for ticks, with a different styling for major ticks (by default they are darker). If this
 * feature is disabled, no lines are shown. If it's enabled, line are shown for the tick level which is set in current
 * ViewPreset. Please see {@link Scheduler.preset.ViewPreset#field-columnLinesFor} config for details.
 *
 * The lines are drawn as divs, with only visible lines available in DOM. The color and style of the lines are
 * determined the css rules for `.b-column-line` and `.b-column-line-major`.
 *
 * For vertical mode, this features also draws vertical resource column lines if scheduler is configured with
 * `columnLines : true` (which is the default, see {@link Grid.view.GridBase#config-columnLines}).
 *
 * This feature is **enabled** by default
 *
 * @extends Core/mixin/InstancePlugin
 * @mixes Core/mixin/Delayable
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/ColumnLines.js
 * @classtype columnLines
 * @feature
 */
class ColumnLines extends InstancePlugin.mixin(AttachToProjectMixin, Delayable) {
  //region Config
  static get $name() {
    return 'ColumnLines';
  }
  static get delayable() {
    return {
      refresh: {
        type: 'raf',
        cancelOutstanding: true
      }
    };
  }
  // Plugin configuration. This plugin chains some of the functions in Grid.
  static get pluginConfig() {
    return {
      after: ['render', 'updateCanvasSize', 'onVisibleDateRangeChange', 'onVisibleResourceRangeChange']
    };
  }
  //endregion
  //region Init & destroy
  construct(client, config) {
    client.useBackgroundCanvas = true;
    super.construct(client, config);
  }
  attachToResourceStore(resourceStore) {
    const {
      client
    } = this;
    super.attachToResourceStore(resourceStore);
    if (client.isVertical) {
      client.resourceStore.ion({
        name: 'resourceStore',
        group({
          groupers
        }) {
          if (groupers.length === 0) {
            this.refresh();
          }
        },
        thisObj: this
      });
    }
  }
  doDisable(disable) {
    super.doDisable(disable);
    if (!this.isConfiguring) {
      this.refresh();
    }
  }
  //endregion
  //region Draw
  /**
   * Draw lines when scheduler/gantt is rendered.
   * @private
   */
  render() {
    this.refresh();
  }
  getColumnLinesDOMConfig(startDate, endDate) {
    const me = this,
      {
        client
      } = me,
      {
        rtl
      } = client,
      m = rtl ? -1 : 1,
      {
        timeAxisViewModel,
        isHorizontal,
        resourceStore,
        variableColumnWidths
      } = client,
      {
        columnConfig
      } = timeAxisViewModel;
    const linesForLevel = timeAxisViewModel.columnLinesFor,
      majorLinesForLevel = Math.max(linesForLevel - 1, 0),
      start = startDate.getTime(),
      end = endDate.getTime(),
      domConfigs = [],
      dates = new Set(),
      dimension = isHorizontal ? 'X' : 'Y';
    if (!me.disabled) {
      const addLineConfig = (tick, isMajor) => {
        const tickStart = tick.start.getTime();
        // Only start of tick matters.
        // Each tick has an exact calculated start position along the time axis
        // and carries a border on its left, so column lines follow from
        // tick 1 (zero-based) onwards.
        if (tickStart > start && tickStart < end && !dates.has(tickStart)) {
          dates.add(tickStart);
          domConfigs.push({
            role: 'presentation',
            className: isMajor ? 'b-column-line-major' : 'b-column-line',
            style: {
              transform: `translate${dimension}(${tick.coord * m}px)`
            },
            dataset: {
              line: isMajor ? `major-${tick.index}` : `line-${tick.index}`
            }
          });
        }
      };
      // Collect configs for major lines
      if (linesForLevel !== majorLinesForLevel) {
        for (let i = 1; i <= columnConfig[majorLinesForLevel].length - 1; i++) {
          addLineConfig(columnConfig[majorLinesForLevel][i], true);
        }
      }
      // And normal lines, skipping dates already occupied by major lines
      for (let i = 1; i <= columnConfig[linesForLevel].length - 1; i++) {
        addLineConfig(columnConfig[linesForLevel][i], false);
      }
      // Add vertical resource column lines, if grid is configured to show column lines
      if (!isHorizontal && client.columnLines) {
        const {
          columnWidth
        } = client.resourceColumns;
        let {
          first: firstResource,
          last: lastResource
        } = client.currentOrientation.getResourceRange(true);
        let nbrGroupHeaders = 0;
        if (firstResource > -1) {
          for (let i = firstResource; i < lastResource + 1; i++) {
            var _instanceMeta$groupPa, _instanceMeta$groupPa2;
            const resourceRecord = resourceStore.getAt(i);
            // Only add lines for group children
            if (resourceRecord.isGroupHeader) {
              lastResource++;
              nbrGroupHeaders++;
              continue;
            }
            const instanceMeta = resourceRecord.instanceMeta(resourceStore),
              left = variableColumnWidths ? instanceMeta.insetStart + resourceRecord.columnWidth - 1 : (i - nbrGroupHeaders + 1) * columnWidth - 1;
            domConfigs.push({
              className: {
                'b-column-line': 1,
                'b-resource-column-line': 1,
                'b-resource-group-divider': resourceStore.isGrouped && ((_instanceMeta$groupPa = instanceMeta.groupParent) === null || _instanceMeta$groupPa === void 0 ? void 0 : _instanceMeta$groupPa.groupChildren[((_instanceMeta$groupPa2 = instanceMeta.groupParent) === null || _instanceMeta$groupPa2 === void 0 ? void 0 : _instanceMeta$groupPa2.groupChildren.length) - 1]) === resourceRecord
              },
              style: {
                transform: `translateX(${left * m}px)`
              },
              dataset: {
                line: `resource-${i}`
              }
            });
          }
        }
      }
    }
    return domConfigs;
  }
  /**
   * Draw column lines that are in view
   * @private
   */
  refresh() {
    const me = this,
      {
        client
      } = me,
      {
        timeAxis
      } = client,
      {
        startDate,
        endDate
      } = client.visibleDateRange || emptyObject$1,
      axisStart = timeAxis.startDate;
    // Early bailout for timeaxis without start date or when starting with schedule collapsed
    if (!axisStart || !startDate || me.client.timeAxisSubGrid.collapsed) {
      return;
    }
    if (!me.element) {
      me.element = DomHelper.createElement({
        parent: client.backgroundCanvas,
        className: 'b-column-lines-canvas'
      });
    }
    const domConfigs = me.getColumnLinesDOMConfig(startDate, endDate);
    DomSync.sync({
      targetElement: me.element,
      onlyChildren: true,
      domConfig: {
        children: domConfigs,
        syncOptions: {
          // When zooming in and out we risk getting a lot of released lines if we do not limit it
          releaseThreshold: 4
        }
      },
      syncIdField: 'line'
    });
  }
  //endregion
  //region Events
  // Called when visible date range changes, for example from zooming, scrolling, resizing
  onVisibleDateRangeChange() {
    this.refresh();
  }
  // Called when visible resource range changes, for example on scroll and resize
  onVisibleResourceRangeChange({
    firstResource,
    lastResource
  }) {
    this.refresh();
  }
  updateCanvasSize() {
    this.refresh();
  }
  //endregion
}

ColumnLines._$name = 'ColumnLines';
GridFeatureManager.registerFeature(ColumnLines, true, ['Scheduler', 'Gantt', 'TimelineHistogram']);

/**
 * @module Scheduler/feature/mixin/DependencyCreation
 */
/**
 * Mixin for Dependencies feature that handles dependency creation (drag & drop from terminals which are shown on hover).
 * Requires {@link Core.mixin.Delayable} to be mixed in alongside.
 *
 * @mixin
 */
var DependencyCreation = (Target => class DependencyCreation extends (Target || Base$1) {
  static get $name() {
    return 'DependencyCreation';
  }
  //region Config
  static get defaultConfig() {
    return {
      /**
       * `false` to require a drop on a target event bar side circle to define the dependency type.
       * If dropped on the event bar, the `defaultValue` of the DependencyModel `type` field will be used to
       * determine the target task side.
       *
       * @member {Boolean} allowDropOnEventBar
       */
      /**
       * `false` to require a drop on a target event bar side circle to define the dependency type.
       * If dropped on the event bar, the `defaultValue` of the DependencyModel `type` field will be used to
       * determine the target task side.
       *
       * @config {Boolean}
       * @default
       */
      allowDropOnEventBar: true,
      /**
       * `false` to not show a tooltip while creating a dependency
       * @config {Boolean}
       * @default
       */
      showCreationTooltip: true,
      /**
       * A tooltip config object that will be applied to the dependency creation {@link Core.widget.Tooltip}
       * @config {TooltipConfig}
       */
      creationTooltip: null,
      /**
       * A template function that will be called to generate the HTML contents of the dependency creation tooltip.
       * You can return either an HTML string or a {@link DomConfig} object.
       * @prp {Function} creationTooltipTemplate
       * @param {Object} data Data about the dependency being created
       * @param {Scheduler.model.TimeSpan} data.source The from event
       * @param {Scheduler.model.TimeSpan} data.target The target event
       * @param {String} data.fromSide The from side (start, end, top, bottom)
       * @param {String} data.toSide The target side (start, end, top, bottom)
       * @param {Boolean} data.valid The validity of the dependency
       * @returns {String|DomConfig}
       */
      /**
       * CSS class used for terminals
       * @config {String}
       * @default
       */
      terminalCls: 'b-sch-terminal',
      /**
       * Where (on event bar edges) to display terminals. The sides are `'start'`, `'top'`,
       * `'end'` and `'bottom'`
       * @config {String[]}
       */
      terminalSides: ['start', 'top', 'end', 'bottom'],
      /**
       * Set to `false` to not allow creating dependencies
       * @config {Boolean}
       * @default
       */
      allowCreate: true
    };
  }
  //endregion
  //region Init & destroy
  construct(view, config) {
    super.construct(view, config);
    const me = this;
    me.view = view;
    me.eventName = view.scheduledEventName;
    view.ion({
      readOnly: () => me.updateCreateListeners()
    });
    me.updateCreateListeners();
    me.chain(view, 'onElementTouchMove', 'onElementTouchMove');
  }
  doDestroy() {
    var _me$pointerUpMoveDeta, _me$creationTooltip;
    const me = this;
    me.detachListeners('view');
    me.creationData = null;
    (_me$pointerUpMoveDeta = me.pointerUpMoveDetacher) === null || _me$pointerUpMoveDeta === void 0 ? void 0 : _me$pointerUpMoveDeta.call(me);
    (_me$creationTooltip = me.creationTooltip) === null || _me$creationTooltip === void 0 ? void 0 : _me$creationTooltip.destroy();
    super.doDestroy();
  }
  updateCreateListeners() {
    const me = this;
    if (!me.view) {
      return;
    }
    me.detachListeners('view');
    if (me.isCreateAllowed) {
      me.view.ion({
        name: 'view',
        [`${me.eventName}mouseenter`]: 'onTimeSpanMouseEnter',
        [`${me.eventName}mouseleave`]: 'onTimeSpanMouseLeave',
        thisObj: me
      });
    }
  }
  set allowCreate(value) {
    this._allowCreate = value;
    this.updateCreateListeners();
  }
  get allowCreate() {
    return this._allowCreate;
  }
  get isCreateAllowed() {
    return this.allowCreate && !this.view.readOnly && !this.disabled;
  }
  //endregion
  //region Events
  /**
   * Show terminals when mouse enters event/task element
   * @private
   */
  onTimeSpanMouseEnter({
    event,
    source,
    [`${this.eventName}Record`]: record,
    [`${this.eventName}Element`]: element
  }) {
    if (!record.isCreating && !record.readOnly && (!this.client.features.nestedEvents || record.parent.isRoot)) {
      const me = this,
        {
          creationData
        } = me,
        eventBarElement = DomHelper.down(element, source.eventInnerSelector);
      // When we enter a different event than the one we started on
      if (record !== (creationData === null || creationData === void 0 ? void 0 : creationData.source)) {
        me.showTerminals(record, eventBarElement);
        if (creationData && event.target.closest(me.client.eventSelector)) {
          creationData.timeSpanElement = eventBarElement;
          me.onOverTargetEventBar(event);
        }
      }
    }
  }
  /**
   * Hide terminals when mouse leaves event/task element
   * @private
   */
  onTimeSpanMouseLeave(event) {
    var _event$event;
    const me = this,
      {
        creationData
      } = me,
      element = event[`${me.eventName}Element`],
      timeSpanLeft = DomHelper.down(element, me.view.eventInnerSelector),
      target = (_event$event = event.event) === null || _event$event === void 0 ? void 0 : _event$event.relatedTarget,
      timeSpanElement = creationData === null || creationData === void 0 ? void 0 : creationData.timeSpanElement;
    // Can happen when unhovering an occurrence during update
    if (!target) {
      return;
    }
    if (!creationData || !timeSpanElement || !target || !DomHelper.isDescendant(timeSpanElement, target)) {
      // We cannot hide the terminals for non-trusted events because non-trusted means it's
      // synthesized from a touchmove event and if the source element of a touchmove
      // leaves the DOM, the touch gesture is ended.
      if (event.event.isTrusted || timeSpanLeft !== (creationData === null || creationData === void 0 ? void 0 : creationData.sourceElement)) {
        me.hideTerminals(element);
      }
    }
    if (creationData && !creationData.finalizing && !target.closest(me.client.eventSelector)) {
      creationData.timeSpanElement = null;
      me.onOverNewTargetWhileCreating(undefined, undefined, event);
    }
  }
  onTerminalMouseOver(event) {
    if (this.creationData) {
      this.onOverTargetEventBar(event);
    }
  }
  /**
   * Remove hover styling when mouse leaves terminal. Also hides terminals when mouse leaves one it and not creating a
   * dependency.
   * @private
   */
  onTerminalMouseOut(event) {
    const me = this,
      {
        creationData
      } = me,
      eventElement = event.target.closest(me.view.eventSelector);
    if (eventElement && (!me.showingTerminalsFor || !DomHelper.isDescendant(eventElement, me.showingTerminalsFor)) && (!creationData || eventElement !== creationData.timeSpanElement)) {
      me.hideTerminals(eventElement);
      me.view.unhover(eventElement, event);
    }
    if (creationData) {
      me.onOverNewTargetWhileCreating(event.relatedTarget, creationData.target, event);
    }
  }
  /**
   * Start creating a dependency when mouse is pressed over terminal
   * @private
   */
  onTerminalPointerDown(event) {
    const me = this;
    // ignore non-left button clicks
    if (event.button === 0 && !me.creationData) {
      var _scheduler$resolveRes;
      const scheduler = me.view,
        timeAxisSubGridElement = scheduler.timeAxisSubGridElement,
        terminalNode = event.target,
        timeSpanElement = terminalNode.closest(scheduler.eventInnerSelector),
        viewBounds = Rectangle.from(scheduler.element, document.body);
      event.stopPropagation();
      me.creationData = {
        sourceElement: timeSpanElement,
        source: scheduler.resolveTimeSpanRecord(timeSpanElement).$original,
        fromSide: terminalNode.dataset.side,
        startPoint: Rectangle.from(terminalNode, timeAxisSubGridElement).center,
        startX: event.pageX - viewBounds.x + scheduler.scrollLeft,
        startY: event.pageY - viewBounds.y + scheduler.scrollTop,
        valid: false,
        sourceResource: (_scheduler$resolveRes = scheduler.resolveResourceRecord) === null || _scheduler$resolveRes === void 0 ? void 0 : _scheduler$resolveRes.call(scheduler, event),
        tooltip: me.creationTooltip
      };
      me.pointerUpMoveDetacher = EventHelper.on({
        pointerup: {
          element: scheduler.element.getRootNode(),
          handler: 'onMouseUp',
          passive: false
        },
        pointermove: {
          element: timeAxisSubGridElement,
          handler: 'onMouseMove',
          passive: false
        },
        thisObj: me
      });
      // If root element is anything but Document (it could be Document Fragment or regular Node in case of LWC)
      // then we should also add listener to document to cancel dependency creation
      me.documentPointerUpDetacher = EventHelper.on({
        pointerup: {
          element: document,
          handler: 'onDocumentMouseUp'
        },
        keydown: {
          element: document,
          handler: ({
            key
          }) => {
            if (key === 'Escape') {
              me.abort();
            }
          }
        },
        thisObj: me
      });
    }
  }
  onElementTouchMove(event) {
    var _super$onElementTouch;
    (_super$onElementTouch = super.onElementTouchMove) === null || _super$onElementTouch === void 0 ? void 0 : _super$onElementTouch.call(this, event);
    if (this.connector) {
      // Prevent touch scrolling while dragging a connector
      event.preventDefault();
    }
  }
  /**
   * Update connector line showing dependency between source and target when mouse moves. Also check if mouse is over
   * a valid target terminal
   * @private
   */
  onMouseMove(event) {
    const me = this,
      {
        view,
        creationData: data
      } = me,
      viewBounds = Rectangle.from(view.element, document.body),
      deltaX = event.pageX - viewBounds.x + view.scrollLeft - data.startX,
      deltaY = event.pageY - viewBounds.y + view.scrollTop - data.startY,
      length = Math.round(Math.sqrt(deltaX * deltaX + deltaY * deltaY)) - 3,
      angle = Math.atan2(deltaY, deltaX);
    let {
      connector
    } = me;
    if (!connector) {
      if (me.onRequestDragCreate(event) === false) {
        return;
      }
      connector = me.connector;
    }
    connector.style.width = `${length}px`;
    connector.style.transform = `rotate(${angle}rad)`;
    me.lastMouseMoveEvent = event;
  }
  onRequestDragCreate(event) {
    const me = this,
      {
        view,
        creationData: data
      } = me;
    /**
     * Fired on the owning Scheduler/Gantt before a dependency creation drag operation starts. Return `false` to
     * prevent it
     * @event beforeDependencyCreateDrag
     * @on-owner
     * @param {Scheduler.model.TimeSpan} source The source task
     */
    if (view.trigger('beforeDependencyCreateDrag', {
      data,
      source: data.source
    }) === false) {
      me.abort();
      return false;
    }
    view.element.classList.add('b-creating-dependency');
    me.createConnector(data.startPoint.x, data.startPoint.y);
    /**
     * Fired on the owning Scheduler/Gantt when a dependency creation drag operation starts
     * @event dependencyCreateDragStart
     * @on-owner
     * @param {Scheduler.model.TimeSpan} source The source task
     */
    view.trigger('dependencyCreateDragStart', {
      data,
      source: data.source
    });
    if (me.showCreationTooltip) {
      const tip = me.creationTooltip || (me.creationTooltip = me.createDragTooltip());
      me.creationData.tooltip = tip;
      tip.disabled = false;
      tip.show();
      tip.onMouseMove(event);
    }
    view.scrollManager.startMonitoring({
      scrollables: [{
        element: view.timeAxisSubGrid.scrollable.element,
        direction: 'horizontal'
      }, {
        element: view.scrollable.element,
        direction: 'vertical'
      }],
      callback: () => me.lastMouseMoveEvent && me.onMouseMove(me.lastMouseMoveEvent)
    });
  }
  onOverTargetEventBar(event) {
    var _overEventRecord;
    const me = this,
      {
        view,
        creationData: data,
        allowDropOnEventBar
      } = me,
      {
        target
      } = event;
    let overEventRecord = view.resolveTimeSpanRecord(target).$original;
    // use main event if a segment resolved
    if ((_overEventRecord = overEventRecord) !== null && _overEventRecord !== void 0 && _overEventRecord.isEventSegment) {
      overEventRecord = overEventRecord.event;
    }
    if (Objects.isPromise(data.valid) || !allowDropOnEventBar && !target.classList.contains(me.terminalCls)) {
      return;
    }
    if (overEventRecord !== data.source) {
      me.onOverNewTargetWhileCreating(target, overEventRecord, event);
    }
  }
  async onOverNewTargetWhileCreating(targetElement, overEventRecord, event) {
    const me = this,
      {
        view,
        creationData: data,
        allowDropOnEventBar,
        connector
      } = me;
    if (Objects.isPromise(data.valid)) {
      return;
    }
    // stop target updating if dependency finalizing in progress
    if (data.finalizing) {
      return;
    }
    // Connector might not exist at this point because `pointerout` on the terminal might fire before `pointermove`
    // on the time axis subgrid. This is difficult to reproduce, so shouldn't be triggered often.
    // https://github.com/bryntum/support/issues/3116#issuecomment-894256799
    if (!connector) {
      return;
    }
    connector.classList.remove('b-valid', 'b-invalid');
    data.timeSpanElement && DomHelper.removeClsGlobally(data.timeSpanElement, 'b-sch-terminal-active');
    if (!overEventRecord || overEventRecord === data.source || !allowDropOnEventBar && !targetElement.classList.contains(me.terminalCls)) {
      data.target = data.toSide = null;
      data.valid = false;
      connector.classList.add('b-invalid');
    } else {
      var _data$timeSpanElement, _data$timeSpanElement2;
      const target = data.target = overEventRecord,
        {
          source
        } = data;
      let toSide = targetElement.dataset.side;
      // If we allow dropping anywhere on a task, resolve target side based on the default type of the
      // dependency model used
      if (allowDropOnEventBar && !targetElement.classList.contains(me.terminalCls)) {
        toSide = me.getTargetSideFromType(me.dependencyStore.modelClass.fieldMap.type.defaultValue || DependencyBaseModel.Type.EndToStart);
      }
      if (view.resolveResourceRecord) {
        data.targetResource = view.resolveResourceRecord(event);
      }
      let dependencyType;
      data.toSide = toSide;
      const fromSide = data.fromSide,
        updateValidity = valid => {
          if (!me.isDestroyed) {
            data.valid = valid;
            targetElement.classList.add(valid ? 'b-valid' : 'b-invalid');
            connector.classList.add(valid ? 'b-valid' : 'b-invalid');
            /**
             * Fired on the owning Scheduler/Gantt when asynchronous dependency validation completes
             * @event dependencyValidationComplete
             * @on-owner
             * @param {Scheduler.model.TimeSpan} source The source task
             * @param {Scheduler.model.TimeSpan} target The target task
             * @param {Number} dependencyType The dependency type, see {@link Scheduler.model.DependencyBaseModel#property-Type-static}
             */
            view.trigger('dependencyValidationComplete', {
              data,
              source,
              target,
              dependencyType
            });
          }
        };
      // NOTE: Top/Bottom sides are not taken into account due to
      //       scheduler doesn't check for type value anyway, whereas
      //       gantt will reject any other dependency types undefined in
      //       DependencyBaseModel.Type enumeration.
      switch (true) {
        case fromSide === 'start' && toSide === 'start':
          dependencyType = DependencyBaseModel.Type.StartToStart;
          break;
        case fromSide === 'start' && toSide === 'end':
          dependencyType = DependencyBaseModel.Type.StartToEnd;
          break;
        case fromSide === 'end' && toSide === 'start':
          dependencyType = DependencyBaseModel.Type.EndToStart;
          break;
        case fromSide === 'end' && toSide === 'end':
          dependencyType = DependencyBaseModel.Type.EndToEnd;
          break;
      }
      /**
       * Fired on the owning Scheduler/Gantt when asynchronous dependency validation starts
       * @event dependencyValidationStart
       * @on-owner
       * @param {Scheduler.model.TimeSpan} source The source task
       * @param {Scheduler.model.TimeSpan} target The target task
       * @param {Number} dependencyType The dependency type, see {@link Scheduler.model.DependencyBaseModel#property-Type-static}
       */
      view.trigger('dependencyValidationStart', {
        data,
        source,
        target,
        dependencyType
      });
      let valid = data.valid = me.dependencyStore.isValidDependency(source, target, dependencyType);
      // Promise is returned when using the engine
      if (Objects.isPromise(valid)) {
        valid = await valid;
        updateValidity(valid);
      } else {
        updateValidity(valid);
      }
      const validityCls = valid ? 'b-valid' : 'b-invalid';
      connector.classList.add(validityCls);
      (_data$timeSpanElement = data.timeSpanElement) === null || _data$timeSpanElement === void 0 ? void 0 : (_data$timeSpanElement2 = _data$timeSpanElement.querySelector(`.b-sch-terminal[data-side=${toSide}]`)) === null || _data$timeSpanElement2 === void 0 ? void 0 : _data$timeSpanElement2.classList.add('b-sch-terminal-active', validityCls);
    }
    me.updateCreationTooltip();
  }
  /**
   * Create a new dependency if mouse release over valid terminal. Hides connector
   * @private
   */
  async onMouseUp() {
    var _me$pointerUpMoveDeta2;
    const me = this,
      data = me.creationData;
    data.finalizing = true;
    (_me$pointerUpMoveDeta2 = me.pointerUpMoveDetacher) === null || _me$pointerUpMoveDeta2 === void 0 ? void 0 : _me$pointerUpMoveDeta2.call(me);
    if (data.valid) {
      /**
       * Fired on the owning Scheduler/Gantt when a dependency drag creation operation is about to finalize
       *
       * @event beforeDependencyCreateFinalize
       * @on-owner
       * @preventable
       * @async
       * @param {Scheduler.model.TimeSpan} source The source task
       * @param {Scheduler.model.TimeSpan} target The target task
       * @param {'start'|'end'|'top'|'bottom'} fromSide The from side (start / end / top / bottom)
       * @param {'start'|'end'|'top'|'bottom'} toSide The to side (start / end / top / bottom)
       */
      const result = await me.view.trigger('beforeDependencyCreateFinalize', data);
      if (result === false) {
        data.valid = false;
      }
      // Await any async validation logic before continuing
      else if (Objects.isPromise(data.valid)) {
        data.valid = await data.valid;
      }
      if (data.valid) {
        let dependency = me.createDependency(data);
        if (dependency !== null) {
          if (Objects.isPromise(dependency)) {
            dependency = await dependency;
          }
          data.dependency = dependency;
          /**
           * Fired on the owning Scheduler/Gantt when a dependency drag creation operation succeeds
           * @event dependencyCreateDrop
           * @on-owner
           * @param {Scheduler.model.TimeSpan} source The source task
           * @param {Scheduler.model.TimeSpan} target The target task
           * @param {Scheduler.model.DependencyBaseModel} dependency The created dependency
           */
          me.view.trigger('dependencyCreateDrop', {
            data,
            source: data.source,
            target: data.target,
            dependency
          });
          me.doAfterDependencyDrop(data);
        }
      } else {
        me.doAfterDependencyDrop(data);
      }
    } else {
      data.valid = false;
      me.doAfterDependencyDrop(data);
    }
    me.abort();
  }
  doAfterDependencyDrop(data) {
    /**
     * Fired on the owning Scheduler/Gantt after a dependency drag creation operation finished, no matter to outcome
     * @event afterDependencyCreateDrop
     * @on-owner
     * @param {Scheduler.model.TimeSpan} source The source task
     * @param {Scheduler.model.TimeSpan} target The target task
     * @param {Scheduler.model.DependencyBaseModel} dependency The created dependency
     */
    this.view.trigger('afterDependencyCreateDrop', {
      data,
      ...data
    });
  }
  onDocumentMouseUp({
    target
  }) {
    if (!this.view.timeAxisSubGridElement.contains(target)) {
      this.abort();
    }
  }
  /**
   * Aborts dependency creation, removes proxy and cleans up listeners
   */
  abort() {
    var _me$pointerUpMoveDeta3, _me$documentPointerUp;
    const me = this,
      {
        view,
        creationData
      } = me;
    // Remove terminals from source and target events.
    if (creationData) {
      const {
        source,
        sourceResource,
        target,
        targetResource
      } = creationData;
      if (source) {
        const el = view.getElementFromEventRecord(source, sourceResource);
        if (el) {
          me.hideTerminals(el);
        }
      }
      if (target) {
        const el = view.getElementFromEventRecord(target, targetResource);
        if (el) {
          me.hideTerminals(el);
        }
      }
    }
    if (me.creationTooltip) {
      me.creationTooltip.disabled = true;
    }
    me.creationData = me.lastMouseMoveEvent = null;
    (_me$pointerUpMoveDeta3 = me.pointerUpMoveDetacher) === null || _me$pointerUpMoveDeta3 === void 0 ? void 0 : _me$pointerUpMoveDeta3.call(me);
    (_me$documentPointerUp = me.documentPointerUpDetacher) === null || _me$documentPointerUp === void 0 ? void 0 : _me$documentPointerUp.call(me);
    me.removeConnector();
  }
  //endregion
  //region Connector
  /**
   * Creates a connector line that visualizes dependency source & target
   * @private
   */
  createConnector(x, y) {
    const me = this,
      {
        view
      } = me;
    me.clearTimeout(me.removeConnectorTimeout);
    me.connector = DomHelper.createElement({
      parent: view.timeAxisSubGridElement,
      className: `${me.baseCls}-connector`,
      style: `left:${x}px;top:${y}px`
    });
    view.element.classList.add('b-creating-dependency');
  }
  createDragTooltip() {
    const me = this,
      {
        view
      } = me;
    return me.creationTooltip = Tooltip.new({
      id: `${view.id}-dependency-drag-tip`,
      cls: 'b-sch-dependency-creation-tooltip',
      loadingMsg: '',
      anchorToTarget: false,
      // Keep tip visible until drag drop operation is finalized
      forElement: view.timeAxisSubGridElement,
      trackMouse: true,
      // Do not constrain at all, want it to be able to go outside of the viewport to not get in the way
      constrainTo: null,
      header: {
        dock: 'right'
      },
      internalListeners: {
        // Show initial content immediately
        beforeShow: 'updateCreationTooltip',
        thisObj: me
      }
    }, me.creationTooltip);
  }
  /**
   * Remove connector
   * @private
   */
  removeConnector() {
    const me = this,
      {
        connector,
        view
      } = me;
    if (connector) {
      connector.classList.add('b-removing');
      connector.style.width = '0';
      me.removeConnectorTimeout = me.setTimeout(() => {
        connector.remove();
        me.connector = null;
      }, 200);
    }
    view.element.classList.remove('b-creating-dependency');
    me.creationTooltip && me.creationTooltip.hide();
    view.scrollManager.stopMonitoring();
  }
  //endregion
  //region Terminals
  /**
   * Show terminals for specified event at sides defined in #terminalSides.
   * @param {Scheduler.model.TimeSpan} timeSpanRecord Event/task to show terminals for
   * @param {HTMLElement} element Event/task element
   */
  showTerminals(timeSpanRecord, element) {
    const me = this;
    // Record not part of project is a transient record in a display store, not meant to be manipulated
    if (!me.isCreateAllowed || !timeSpanRecord.project) {
      return;
    }
    const cls = me.terminalCls,
      terminalsVisibleCls = `${cls}s-visible`;
    // We operate on the event bar, not the wrap
    element = DomHelper.down(element, me.view.eventInnerSelector);
    // bail out if terminals already shown or if view is readonly
    // do not draw new terminals if we are resizing event
    if (!element.classList.contains(terminalsVisibleCls) && !me.view.element.classList.contains('b-resizing-event') && !me.view.readOnly) {
      /**
       * Fired on the owning Scheduler/Gantt before showing dependency terminals on a task or event. Return `false` to
       * prevent it
       * @event beforeShowTerminals
       * @on-owner
       * @param {Scheduler.model.TimeSpan} source The hovered task
       */
      if (me.client.trigger('beforeShowTerminals', {
        source: timeSpanRecord
      }) === false) {
        return;
      }
      // create terminals for desired sides
      me.terminalSides.forEach(side => {
        // Allow code to use left for the start side and right for the end side
        side = me.fixSide(side);
        const terminal = DomHelper.createElement({
          parent: element,
          className: `${cls} ${cls}-${side}`,
          dataset: {
            side,
            feature: true
          }
        });
        terminal.detacher = EventHelper.on({
          element: terminal,
          mouseover: 'onTerminalMouseOver',
          mouseout: 'onTerminalMouseOut',
          // Needs to be pointerdown to match DragHelper, otherwise will be preventing wrong event
          pointerdown: {
            handler: 'onTerminalPointerDown',
            capture: true
          },
          thisObj: me
        });
      });
      element.classList.add(terminalsVisibleCls);
      timeSpanRecord.internalCls.add(terminalsVisibleCls);
      me.showingTerminalsFor = element;
    }
  }
  fixSide(side) {
    if (side === 'left') {
      return 'start';
    }
    if (side === 'right') {
      return 'end';
    }
    return side;
  }
  /**
   * Hide terminals for specified event
   * @param {HTMLElement} eventElement Event element
   */
  hideTerminals(eventElement) {
    // remove all terminals
    const me = this,
      eventParams = me.client.getTimeSpanMouseEventParams(eventElement),
      timeSpanRecord = eventParams === null || eventParams === void 0 ? void 0 : eventParams[`${me.eventName}Record`],
      terminalsVisibleCls = `${me.terminalCls}s-visible`;
    DomHelper.forEachSelector(eventElement, `.${me.terminalCls}`, terminal => {
      terminal.detacher && terminal.detacher();
      terminal.remove();
    });
    DomHelper.down(eventElement, me.view.eventInnerSelector).classList.remove(terminalsVisibleCls);
    timeSpanRecord.internalCls.remove(terminalsVisibleCls);
    me.showingTerminalsFor = null;
  }
  //endregion
  //region Dependency creation
  /**
   * Create a new dependency from source terminal to target terminal
   * @internal
   */
  createDependency(data) {
    const {
        source,
        target,
        fromSide,
        toSide
      } = data,
      type = (fromSide === 'start' ? 0 : 2) + (toSide === 'end' ? 1 : 0);
    const newDependency = this.dependencyStore.add({
      from: source.id,
      to: target.id,
      type,
      fromSide,
      toSide
    });
    return newDependency !== null ? newDependency[0] : null;
  }
  getTargetSideFromType(type) {
    if (type === DependencyBaseModel.Type.StartToStart || type === DependencyBaseModel.Type.EndToStart) {
      return 'start';
    }
    return 'end';
  }
  //endregion
  //region Tooltip
  /**
   * Update dependency creation tooltip
   * @private
   */
  updateCreationTooltip() {
    const me = this,
      data = me.creationData,
      {
        valid
      } = data,
      tip = me.creationTooltip,
      {
        classList
      } = tip.element;
    // Promise, when using engine
    if (Objects.isPromise(valid)) {
      classList.remove('b-invalid');
      classList.add('b-checking');
      return new Promise(resolve => valid.then(valid => {
        data.valid = valid;
        if (!tip.isDestroyed) {
          resolve(me.updateCreationTooltip());
        }
      }));
    }
    tip.html = me.creationTooltipTemplate(data);
  }
  creationTooltipTemplate(data) {
    var _data$target;
    const me = this,
      {
        tooltip,
        valid
      } = data,
      {
        classList
      } = tooltip.element;
    Object.assign(data, {
      fromText: StringHelper.encodeHtml(data.source.name),
      toText: StringHelper.encodeHtml(((_data$target = data.target) === null || _data$target === void 0 ? void 0 : _data$target.name) ?? ''),
      fromSide: data.fromSide,
      toSide: data.toSide || ''
    });
    let tipTitleIconClsSuffix, tipTitleText;
    classList.toggle('b-invalid', !valid);
    classList.remove('b-checking');
    // Valid
    if (valid === true) {
      tipTitleIconClsSuffix = 'valid';
      tipTitleText = me.L('L{Dependencies.valid}');
    }
    // Invalid
    else {
      tipTitleIconClsSuffix = 'invalid';
      tipTitleText = me.L('L{Dependencies.invalid}');
    }
    tooltip.title = `<i class="b-icon b-icon-${tipTitleIconClsSuffix}"></i>${tipTitleText}`;
    return {
      children: [{
        className: 'b-sch-dependency-tooltip',
        children: [{
          dataset: {
            ref: 'fromLabel'
          },
          tag: 'label',
          text: me.L('L{Dependencies.from}')
        }, {
          dataset: {
            ref: 'fromText'
          },
          text: data.fromText
        }, {
          dataset: {
            ref: 'fromBox'
          },
          className: `b-sch-box b-${data.fromSide}`
        }, {
          dataset: {
            ref: 'toLabel'
          },
          tag: 'label',
          text: me.L('L{Dependencies.to}')
        }, {
          dataset: {
            ref: 'toText'
          },
          text: data.toText
        }, {
          dataset: {
            ref: 'toBox'
          },
          className: `b-sch-box b-${data.toSide}`
        }]
      }]
    };
  }
  //endregion
  doDisable(disable) {
    if (!this.isConfiguring) {
      this.updateCreateListeners();
    }
    super.doDisable(disable);
  }
});

const ROWS_PER_CELL = 25;
// Mixin that handles the dependency grid cache
//
// Grid cache explainer
// ────────────────────
// The purpose of the grid cache is to reduce the amount of dependencies we have to iterate over when drawing by
// partitioning them into a virtual grid. With for example 10k deps we would have to iterate over all 10k on
// each draw since any of them might be intersecting the view.
//
// The cells are horizontally based on ticks (50 per cell) and vertically on rows (also 50 per cell. Each cell
// lists which dependencies intersect it. When drawing we only have to iterate over the dependencies for the
// cells that intersect the viewport.
//
// The grid cache is populated when dependencies are drawn. Any change to deps, resources, events or assignments
// clears the cache.
//
// The dependency drawn below will be included in the set that is considered for drawing if tickCell 0 or
// tickCell 1 and rowCell 0 intersects the current view (it is thus represented twice in the grid cache)
//
//       tickCell 0           tickCell 1
//       tick 0-49            tick 50-99
//    ┌────────────────────┬─────────────────────┐
// r r│0,0                 │1,0                  │
// o o│     │              │                     │
// w w│     │     !!!!!!!!!│!!!!!!!!!!!          │
// C  │     │     ! View   │          !          │
// e 0│     │     ! port   │          !          │
// l -│     │     !        │          !          │
// l 4│     └─────!────────┼──────────!────►     │
// 0 9│           !        │          !          │
//    ├────────────────────┼─────────────────────┤
// r r│0,1        !        │1,1       !          │
// o o│           !        │          !          │
// w w│           !!!!!!!!!│!!!!!!!!!!!          │
// C  │                    │                     │
// e 5│                    │                     │
// l 0│                    │                     │
// l -│                    │                     │
// 1 9│                    │                     │
//   9└────────────────────┴─────────────────────┘
//               uoᴉʇɐɹʇsnꞁꞁᴉ əɥɔɐɔ pᴉɹ⅁
var DependencyGridCache = (Target => class DependencyGridCache extends Target {
  static $name = 'DependencyGridCache';
  gridCache = null;
  // Dependencies that might intersect the current viewport and thus should be considered for drawing
  getDependenciesToConsider(startMS, endMS, startIndex, endIndex) {
    const me = this,
      {
        gridCache
      } = me,
      {
        timeAxis
      } = me.client;
    if (gridCache) {
      const dependencies = new Set(),
        fromMSCell = Math.floor((startMS - timeAxis.startMS) / me.MS_PER_CELL),
        toMSCell = Math.floor((endMS - timeAxis.startMS) / me.MS_PER_CELL),
        fromRowCell = Math.floor(startIndex / ROWS_PER_CELL),
        toRowCell = Math.floor(endIndex / ROWS_PER_CELL);
      for (let i = fromMSCell; i <= toMSCell; i++) {
        const msCell = gridCache[i];
        if (msCell) {
          for (let j = fromRowCell; j <= toRowCell; j++) {
            const intersectingDependencies = msCell[j];
            if (intersectingDependencies) {
              for (let i = 0; i < intersectingDependencies.length; i++) {
                dependencies.add(intersectingDependencies[i]);
              }
            }
          }
        }
      }
      return dependencies;
    }
  }
  // A (single) dependency was drawn, we might want to store info about it in the grid cache
  afterDrawDependency(dependency, fromIndex, toIndex, fromDateMS, toDateMS) {
    const me = this;
    if (me.constructGridCache) {
      const {
          MS_PER_CELL
        } = me,
        {
          startMS: timeAxisStartMS,
          endMS: timeAxisEndMS
        } = me.client.timeAxis,
        timeAxisCells = Math.ceil((timeAxisEndMS - timeAxisStartMS) / MS_PER_CELL),
        fromMSCell = Math.floor((fromDateMS - timeAxisStartMS) / MS_PER_CELL),
        toMSCell = Math.floor((toDateMS - timeAxisStartMS) / MS_PER_CELL),
        fromRowCell = Math.floor(fromIndex / ROWS_PER_CELL),
        toRowCell = Math.floor(toIndex / ROWS_PER_CELL),
        firstMSCell = Math.min(fromMSCell, toMSCell),
        lastMSCell = Math.max(fromMSCell, toMSCell),
        firstRowCell = Math.min(fromRowCell, toRowCell),
        lastRowCell = Math.max(fromRowCell, toRowCell);
      // Ignore dependencies fully outside of the time axis
      if (firstMSCell < 0 && lastMSCell < 0 || firstMSCell > timeAxisCells && lastMSCell > timeAxisCells) {
        return;
      }
      // Cache from time axis start, to time axis end ("cropping" deps starting or ending outside)
      const startMSCell = Math.max(firstMSCell, 0),
        endMSCell = Math.min(lastMSCell, timeAxisCells);
      for (let i = startMSCell; i <= endMSCell; i++) {
        const msCell = me.gridCache[i] ?? (me.gridCache[i] = {});
        for (let j = firstRowCell; j <= lastRowCell; j++) {
          const rowCell = msCell[j] ?? (msCell[j] = []);
          rowCell.push(dependency);
        }
      }
    }
  }
  // All dependencies are about to be drawn, check if we need to build the grid cache
  beforeDraw() {
    const me = this;
    if (!me.gridCache) {
      const {
        visibleDateRange
      } = me.client;
      me.constructGridCache = true;
      // Adjust number of ms used in grid cache to match viewport
      me.MS_PER_CELL = Math.max(visibleDateRange.endMS - visibleDateRange.startMS, 1000);
      // Start with empty cache, will be populated as deps are drawn
      me.gridCache = {};
    }
  }
  // All dependencies are drawn, we no longer need to rebuild the cache
  afterDraw() {
    this.constructGridCache = false;
  }
  reset() {
    this.gridCache = null;
  }
});

// Start adjusting if there is system scaling > 130%
const THRESHOLD = Math.min(1 / globalThis.devicePixelRatio, 0.75),
  BOX_PROPERTIES = ['start', 'end', 'top', 'bottom'],
  equalEnough = (a, b) => Math.abs(a - b) < 0.1;
/**
 * @module Scheduler/util/RectangularPathFinder
 */
/**
 * Class which finds rectangular path, i.e. path with 90 degrees turns, between two boxes.
 * @private
 */
class RectangularPathFinder extends Base$1 {
  static get configurable() {
    return {
      /**
       * Default start connection side: 'left', 'right', 'top', 'bottom'
       * @config {'top'|'bottom'|'left'|'right'}
       * @default
       */
      startSide: 'right',
      // /**
      //  * Default start arrow size in pixels
      //  * @config {Number}
      //  * @default
      //  */
      // startArrowSize : 0,
      /**
       * Default start arrow staff size in pixels
       * @config {Number}
       * @default
       */
      startArrowMargin: 12,
      /**
       * Default starting connection point shift from box's arrow pointing side middle point
       * @config {Number}
       * @default
       */
      startShift: 0,
      /**
       * Default end arrow pointing direction, possible values are: 'left', 'right', 'top', 'bottom'
       * @config {'top'|'bottom'|'left'|'right'}
       * @default
       */
      endSide: 'left',
      // /**
      //  * Default end arrow size in pixels
      //  * @config {Number}
      //  * @default
      //  */
      // endArrowSize : 0,
      /**
       * Default end arrow staff size in pixels
       * @config {Number}
       * @default
       */
      endArrowMargin: 12,
      /**
       * Default ending connection point shift from box's arrow pointing side middle point
       * @config {Number}
       * @default
       */
      endShift: 0,
      /**
       * Start / End box vertical margin, the amount of pixels from top and bottom line of a box where drawing
       * is prohibited
       * @config {Number}
       * @default
       */
      verticalMargin: 2,
      /**
       * Start / End box horizontal margin, the amount of pixels from left and right line of a box where drawing
       * @config {Number}
       * @default
       */
      horizontalMargin: 5,
      /**
       * Other rectangular areas (obstacles) to search path through
       * @config {Object[]}
       * @default
       */
      otherBoxes: null,
      /**
       * The owning Scheduler. Mandatory so that it can determin RTL state.
       * @config {Scheduler.view.Scheduler}
       * @private
       */
      client: {}
    };
  }
  /**
   * Returns list of horizontal and vertical segments connecting two boxes
   * <pre>
   *    |    | |  |    |       |
   *  --+----+----+----*-------*---
   *  --+=>Start  +----*-------*--
   *  --+----+----+----*-------*--
   *    |    | |  |    |       |
   *    |    | |  |    |       |
   *  --*----*-+-------+-------+--
   *  --*----*-+         End <=+--
   *  --*----*-+-------+-------+--
   *    |    | |  |    |       |
   * </pre>
   * Path goes by lines (-=) and turns at intersections (+), boxes depicted are adjusted by horizontal/vertical
   * margin and arrow margin, original boxes are smaller (path can't go at original box borders). Algorithm finds
   * the shortest path with minimum amount of turns. In short it's mix of "Lee" and "Dijkstra pathfinding"
   * with turns amount taken into account for distance calculation.
   *
   * The algorithm is not very performant though, it's O(N^2), where N is amount of
   * points in the grid, but since the maximum amount of points in the grid might be up to 34 (not 36 since
   * two box middle points are not permitted) that might be ok for now.
   *
   * @param {Object} lineDef An object containing any of the class configuration option overrides as well
   *                         as `startBox`, `endBox`, `startHorizontalMargin`, `startVerticalMargin`,
   *                         `endHorizontalMargin`, `endVerticalMargin` properties
   * @param {Object} lineDef.startBox An object containing `start`, `end`, `top`, `bottom` properties
   * @param {Object} lineDef.endBox   An object containing `start`, `end`, `top`, `bottom` properties
   * @param {Number} lineDef.startHorizontalMargin Horizontal margin override for start box
   * @param {Number} lineDef.startVerticalMargin   Vertical margin override for start box
   * @param {Number} lineDef.endHorizontalMargin   Horizontal margin override for end box
   * @param {Number} lineDef.endVerticalMargin     Vertical margin override for end box
   *
   *
   * @returns {Object[]|Boolean} Array of line segments or false if path cannot be found
   * @returns {Number} return.x1
   * @returns {Number} return.y1
   * @returns {Number} return.x2
   * @returns {Number} return.y2
   */
  //
  //@ignore
  //@privateparam {Function[]|Function} noPathFallbackFn
  //     A function or array of functions which will be tried in case a path can't be found
  //     Each function will be given a line definition it might try to adjust somehow and return.
  //     The new line definition returned will be tried to find a path.
  //     If a function returns false, then next function will be called if any.
  //
  findPath(lineDef, noPathFallbackFn) {
    const me = this,
      originalLineDef = lineDef;
    let lineDefFull, startBox, endBox, startShift, endShift, startSide, endSide,
      // startArrowSize,
      // endArrowSize,
      startArrowMargin, endArrowMargin, horizontalMargin, verticalMargin, startHorizontalMargin, startVerticalMargin, endHorizontalMargin, endVerticalMargin, otherHorizontalMargin, otherVerticalMargin, otherBoxes, connStartPoint, connEndPoint, pathStartPoint, pathEndPoint, gridStartPoint, gridEndPoint, startGridBox, endGridBox, grid, path, tryNum;
    noPathFallbackFn = ArrayHelper.asArray(noPathFallbackFn);
    for (tryNum = 0; lineDef && !path;) {
      lineDefFull = Object.assign(me.config, lineDef);
      startBox = lineDefFull.startBox;
      endBox = lineDefFull.endBox;
      startShift = lineDefFull.startShift;
      endShift = lineDefFull.endShift;
      startSide = lineDefFull.startSide;
      endSide = lineDefFull.endSide;
      // startArrowSize        = lineDefFull.startArrowSize;
      // endArrowSize          = lineDefFull.endArrowSize;
      startArrowMargin = lineDefFull.startArrowMargin;
      endArrowMargin = lineDefFull.endArrowMargin;
      horizontalMargin = lineDefFull.horizontalMargin;
      verticalMargin = lineDefFull.verticalMargin;
      startHorizontalMargin = lineDefFull.hasOwnProperty('startHorizontalMargin') ? lineDefFull.startHorizontalMargin : horizontalMargin;
      startVerticalMargin = lineDefFull.hasOwnProperty('startVerticalMargin') ? lineDefFull.startVerticalMargin : verticalMargin;
      endHorizontalMargin = lineDefFull.hasOwnProperty('endHorizontalMargin') ? lineDefFull.endHorizontalMargin : horizontalMargin;
      endVerticalMargin = lineDefFull.hasOwnProperty('endVerticalMargin') ? lineDefFull.endVerticalMargin : verticalMargin;
      otherHorizontalMargin = lineDefFull.hasOwnProperty('otherHorizontalMargin') ? lineDefFull.otherHorizontalMargin : horizontalMargin;
      otherVerticalMargin = lineDefFull.hasOwnProperty('otherVerticalMargin') ? lineDefFull.otherVerticalMargin : verticalMargin;
      otherBoxes = lineDefFull.otherBoxes;
      startSide = me.normalizeSide(startSide);
      endSide = me.normalizeSide(endSide);
      connStartPoint = me.getConnectionCoordinatesFromBoxSideShift(startBox, startSide, startShift);
      connEndPoint = me.getConnectionCoordinatesFromBoxSideShift(endBox, endSide, endShift);
      startGridBox = me.calcGridBaseBoxFromBoxAndDrawParams(startBox, startSide /*, startArrowSize*/, startArrowMargin, startHorizontalMargin, startVerticalMargin);
      endGridBox = me.calcGridBaseBoxFromBoxAndDrawParams(endBox, endSide /*, endArrowSize*/, endArrowMargin, endHorizontalMargin, endVerticalMargin);
      // Iterate over points and merge those which are too close to each other (e.g. if difference is less than one
      // over devicePixelRatio we won't even see this effect in GUI)
      // https://github.com/bryntum/support/issues/3923
      BOX_PROPERTIES.forEach(property => {
        // We're talking subpixel precision here, so it doesn't really matter which value we choose
        if (Math.abs(startGridBox[property] - endGridBox[property]) <= THRESHOLD) {
          endGridBox[property] = startGridBox[property];
        }
      });
      if (me.shouldLookForPath(startBox, endBox, startGridBox, endGridBox)) {
        var _otherBoxes;
        otherBoxes = (_otherBoxes = otherBoxes) === null || _otherBoxes === void 0 ? void 0 : _otherBoxes.map(box => me.calcGridBaseBoxFromBoxAndDrawParams(box, false /*, 0*/, 0, otherHorizontalMargin, otherVerticalMargin));
        pathStartPoint = me.getConnectionCoordinatesFromBoxSideShift(startGridBox, startSide, startShift);
        pathEndPoint = me.getConnectionCoordinatesFromBoxSideShift(endGridBox, endSide, endShift);
        grid = me.buildPathGrid(startGridBox, endGridBox, pathStartPoint, pathEndPoint, startSide, endSide, otherBoxes);
        gridStartPoint = me.convertDecartPointToGridPoint(grid, pathStartPoint);
        gridEndPoint = me.convertDecartPointToGridPoint(grid, pathEndPoint);
        path = me.findPathOnGrid(grid, gridStartPoint, gridEndPoint, startSide, endSide);
      }
      // Loop if
      // - path is still not found
      // - have no next line definition (which should be obtained from call to one of the functions from noPathFallbackFn array
      // - have noPathFallBackFn array
      // - current try number is less then noPathFallBackFn array length
      for (lineDef = false; !path && !lineDef && noPathFallbackFn && tryNum < noPathFallbackFn.length; tryNum++) {
        lineDef = noPathFallbackFn[tryNum](lineDefFull, originalLineDef);
      }
    }
    if (path) {
      path = me.prependPathWithArrowStaffSegment(path, connStartPoint /*, startArrowSize*/, startSide);
      path = me.appendPathWithArrowStaffSegment(path, connEndPoint /*, endArrowSize*/, endSide);
      path = me.optimizePath(path);
    }
    return path;
  }
  // Compares boxes relative position in the given direction.
  //  0 - 1 is to the left/top of 2
  //  1 - 1 overlaps with left/top edge of 2
  //  2 - 1 is inside 2
  // -2 - 2 is inside 1
  //  3 - 1 overlaps with right/bottom edge of 2
  //  4 - 1 is to the right/bottom of 2
  static calculateRelativePosition(box1, box2, vertical = false) {
    const startProp = vertical ? 'top' : 'start',
      endProp = vertical ? 'bottom' : 'end';
    let result;
    if (box1[endProp] < box2[startProp]) {
      result = 0;
    } else if (box1[endProp] <= box2[endProp] && box1[endProp] >= box2[startProp] && box1[startProp] < box2[startProp]) {
      result = 1;
    } else if (box1[startProp] >= box2[startProp] && box1[endProp] <= box2[endProp]) {
      result = 2;
    } else if (box1[startProp] < box2[startProp] && box1[endProp] > box2[endProp]) {
      result = -2;
    } else if (box1[startProp] <= box2[endProp] && box1[endProp] > box2[endProp]) {
      result = 3;
    } else {
      result = 4;
    }
    return result;
  }
  // Checks if relative position of the original and marginized boxes is the same
  static boxOverlapChanged(startBox, endBox, gridStartBox, gridEndBox, vertical = false) {
    const calculateOverlap = RectangularPathFinder.calculateRelativePosition,
      originalOverlap = calculateOverlap(startBox, endBox, vertical),
      finalOverlap = calculateOverlap(gridStartBox, gridEndBox, vertical);
    return originalOverlap !== finalOverlap;
  }
  shouldLookForPath(startBox, endBox, gridStartBox, gridEndBox) {
    let result = true;
    // Only calculate overlap if boxes are narrow in horizontal direction
    if (
    // We refer to the original arrow margins because during lookup those might be nullified and we need some
    // criteria to tell if events are too narrow
    (startBox.end - startBox.start <= this.startArrowMargin || endBox.end - endBox.start <= this.endArrowMargin) && Math.abs(RectangularPathFinder.calculateRelativePosition(startBox, endBox, true)) === 2) {
      result = !RectangularPathFinder.boxOverlapChanged(startBox, endBox, gridStartBox, gridEndBox);
    }
    return result;
  }
  getConnectionCoordinatesFromBoxSideShift(box, side, shift) {
    let coords;
    // Note that we deal with screen geometry here, not logical dependency sides
    // Possible 'start' and 'end' have been resolved to box sides.
    switch (side) {
      case 'left':
        coords = {
          x: box.start,
          y: (box.top + box.bottom) / 2 + shift
        };
        break;
      case 'right':
        coords = {
          x: box.end,
          y: (box.top + box.bottom) / 2 + shift
        };
        break;
      case 'top':
        coords = {
          x: (box.start + box.end) / 2 + shift,
          y: box.top
        };
        break;
      case 'bottom':
        coords = {
          x: (box.start + box.end) / 2 + shift,
          y: box.bottom
        };
        break;
    }
    return coords;
  }
  calcGridBaseBoxFromBoxAndDrawParams(box, side /*, arrowSize*/, arrowMargin, horizontalMargin, verticalMargin) {
    let gridBox;
    switch (this.normalizeSide(side)) {
      case 'left':
        gridBox = {
          start: box.start - Math.max( /*arrowSize + */arrowMargin, horizontalMargin),
          end: box.end + horizontalMargin,
          top: box.top - verticalMargin,
          bottom: box.bottom + verticalMargin
        };
        break;
      case 'right':
        gridBox = {
          start: box.start - horizontalMargin,
          end: box.end + Math.max( /*arrowSize + */arrowMargin, horizontalMargin),
          top: box.top - verticalMargin,
          bottom: box.bottom + verticalMargin
        };
        break;
      case 'top':
        gridBox = {
          start: box.start - horizontalMargin,
          end: box.end + horizontalMargin,
          top: box.top - Math.max( /*arrowSize + */arrowMargin, verticalMargin),
          bottom: box.bottom + verticalMargin
        };
        break;
      case 'bottom':
        gridBox = {
          start: box.start - horizontalMargin,
          end: box.end + horizontalMargin,
          top: box.top - verticalMargin,
          bottom: box.bottom + Math.max( /*arrowSize + */arrowMargin, verticalMargin)
        };
        break;
      default:
        gridBox = {
          start: box.start - horizontalMargin,
          end: box.end + horizontalMargin,
          top: box.top - verticalMargin,
          bottom: box.bottom + verticalMargin
        };
    }
    return gridBox;
  }
  normalizeSide(side) {
    const {
      rtl
    } = this.client;
    if (side === 'start') {
      return rtl ? 'right' : 'left';
    }
    if (side === 'end') {
      return rtl ? 'left' : 'right';
    }
    return side;
  }
  buildPathGrid(startGridBox, endGridBox, pathStartPoint, pathEndPoint, startSide, endSide, otherGridBoxes) {
    let xs, ys, y, x, ix, iy, xslen, yslen, ib, blen, box, permitted, point;
    const points = {},
      linearPoints = [];
    xs = [startGridBox.start, startSide === 'left' || startSide === 'right' ? (startGridBox.start + startGridBox.end) / 2 : pathStartPoint.x, startGridBox.end, endGridBox.start, endSide === 'left' || endSide === 'right' ? (endGridBox.start + endGridBox.end) / 2 : pathEndPoint.x, endGridBox.end];
    ys = [startGridBox.top, startSide === 'top' || startSide === 'bottom' ? (startGridBox.top + startGridBox.bottom) / 2 : pathStartPoint.y, startGridBox.bottom, endGridBox.top, endSide === 'top' || endSide === 'bottom' ? (endGridBox.top + endGridBox.bottom) / 2 : pathEndPoint.y, endGridBox.bottom];
    if (otherGridBoxes) {
      otherGridBoxes.forEach(box => {
        xs.push(box.start, (box.start + box.end) / 2, box.end);
        ys.push(box.top, (box.top + box.bottom) / 2, box.bottom);
      });
    }
    xs = [...new Set(xs.sort((a, b) => a - b))];
    ys = [...new Set(ys.sort((a, b) => a - b))];
    for (iy = 0, yslen = ys.length; iy < yslen; ++iy) {
      points[iy] = points[iy] || {};
      y = ys[iy];
      for (ix = 0, xslen = xs.length; ix < xslen; ++ix) {
        x = xs[ix];
        permitted = (x <= startGridBox.start || x >= startGridBox.end || y <= startGridBox.top || y >= startGridBox.bottom) && (x <= endGridBox.start || x >= endGridBox.end || y <= endGridBox.top || y >= endGridBox.bottom);
        if (otherGridBoxes) {
          for (ib = 0, blen = otherGridBoxes.length; permitted && ib < blen; ++ib) {
            box = otherGridBoxes[ib];
            permitted = x <= box.start || x >= box.end || y <= box.top || y >= box.bottom ||
            // Allow point if it is a path start/end even if point is inside any box
            x === pathStartPoint.x && y === pathStartPoint.y || x === pathEndPoint.x && y === pathEndPoint.y;
          }
        }
        point = {
          distance: Number.MAX_SAFE_INTEGER,
          permitted,
          x,
          y,
          ix,
          iy
        };
        points[iy][ix] = point;
        linearPoints.push(point);
      }
    }
    return {
      width: xs.length,
      height: ys.length,
      xs,
      ys,
      points,
      linearPoints
    };
  }
  convertDecartPointToGridPoint(grid, point) {
    const x = grid.xs.indexOf(point.x),
      y = grid.ys.indexOf(point.y);
    return grid.points[y][x];
  }
  findPathOnGrid(grid, gridStartPoint, gridEndPoint, startSide, endSide) {
    const me = this;
    let path = false;
    if (gridStartPoint.permitted && gridEndPoint.permitted) {
      grid = me.waveForward(grid, gridStartPoint, 0);
      path = me.collectPath(grid, gridEndPoint, endSide);
    }
    return path;
  }
  // Returns neighbors from Von Neiman ambit (see Lee pathfinding algorithm description)
  getGridPointNeighbors(grid, gridPoint, predicateFn) {
    const ix = gridPoint.ix,
      iy = gridPoint.iy,
      result = [];
    let neighbor;
    // NOTE:
    // It's important to push bottom neighbors first since this method is used
    // in collectPath(), which recursively collects path from end to start node
    // and if bottom neighbors are pushed first in result array then collectPath()
    // will produce a line which is more suitable (pleasant looking) for our purposes.
    if (iy < grid.height - 1) {
      neighbor = grid.points[iy + 1][ix];
      (!predicateFn || predicateFn(neighbor)) && result.push(neighbor);
    }
    if (iy > 0) {
      neighbor = grid.points[iy - 1][ix];
      (!predicateFn || predicateFn(neighbor)) && result.push(neighbor);
    }
    if (ix < grid.width - 1) {
      neighbor = grid.points[iy][ix + 1];
      (!predicateFn || predicateFn(neighbor)) && result.push(neighbor);
    }
    if (ix > 0) {
      neighbor = grid.points[iy][ix - 1];
      (!predicateFn || predicateFn(neighbor)) && result.push(neighbor);
    }
    return result;
  }
  waveForward(grid, gridStartPoint, distance) {
    const me = this;
    // I use the WalkHelper here because a point on a grid and it's neighbors might be considered as a hierarchy.
    // The point is the parent node, and it's neighbors are the children nodes. Thus the grid here is hierarchical
    // data structure which can be walked. WalkHelper walks non-recursively which is exactly what I need as well.
    WalkHelper.preWalkUnordered(
    // Walk starting point - a node is a grid point and it's distance from the starting point
    [gridStartPoint, distance],
    // Children query function
    // NOTE: It's important to fix neighbor distance first, before waving to a neighbor, otherwise waving might
    //       get through a neighbor point setting it's distance to a value more than (distance + 1) whereas we,
    //       at the children querying moment in time, already know that the possibly optimal distance is (distance + 1)
    ([point, distance]) => me.getGridPointNeighbors(grid, point, neighborPoint => neighborPoint.permitted && neighborPoint.distance > distance + 1).map(neighborPoint => [neighborPoint, distance + 1] // Neighbor distance fixation
    ),
    // Walk step iterator function
    ([point, distance]) => point.distance = distance // Neighbor distance applying
    );

    return grid;
  }
  collectPath(grid, gridEndPoint, endSide) {
    const me = this,
      path = [];
    let pathFound = true,
      neighbors,
      lowestDistanceNeighbor,
      xDiff,
      yDiff;
    while (pathFound && gridEndPoint.distance) {
      neighbors = me.getGridPointNeighbors(grid, gridEndPoint, point => point.permitted && point.distance === gridEndPoint.distance - 1);
      pathFound = neighbors.length > 0;
      if (pathFound) {
        // Prefer turnless neighbors first
        neighbors = neighbors.sort((a, b) => {
          let xDiff, yDiff;
          xDiff = a.ix - gridEndPoint.ix;
          yDiff = a.iy - gridEndPoint.iy;
          const resultA = (endSide === 'left' || endSide === 'right') && yDiff === 0 || (endSide === 'top' || endSide === 'bottom') && xDiff === 0 ? -1 : 1;
          xDiff = b.ix - gridEndPoint.ix;
          yDiff = b.iy - gridEndPoint.iy;
          const resultB = (endSide === 'left' || endSide === 'right') && yDiff === 0 || (endSide === 'top' || endSide === 'bottom') && xDiff === 0 ? -1 : 1;
          if (resultA > resultB) return 1;
          if (resultA < resultB) return -1;
          // apply additional sorting to be sure to pick bottom path in IE
          if (resultA === resultB) return a.y > b.y ? -1 : 1;
        });
        lowestDistanceNeighbor = neighbors[0];
        path.push({
          x1: lowestDistanceNeighbor.x,
          y1: lowestDistanceNeighbor.y,
          x2: gridEndPoint.x,
          y2: gridEndPoint.y
        });
        // Detecting new side, either xDiff or yDiff must be 0 (but not both)
        xDiff = lowestDistanceNeighbor.ix - gridEndPoint.ix;
        yDiff = lowestDistanceNeighbor.iy - gridEndPoint.iy;
        switch (true) {
          case !yDiff && xDiff > 0:
            endSide = 'left';
            break;
          case !yDiff && xDiff < 0:
            endSide = 'right';
            break;
          case !xDiff && yDiff > 0:
            endSide = 'top';
            break;
          case !xDiff && yDiff < 0:
            endSide = 'bottom';
            break;
        }
        gridEndPoint = lowestDistanceNeighbor;
      }
    }
    return pathFound && path.reverse() || false;
  }
  prependPathWithArrowStaffSegment(path, connStartPoint /*, startArrowSize*/, startSide) {
    if (path.length > 0) {
      const firstSegment = path[0],
        prependSegment = {
          x2: firstSegment.x1,
          y2: firstSegment.y1
        };
      switch (startSide) {
        case 'left':
          prependSegment.x1 = connStartPoint.x /* - startArrowSize*/;
          prependSegment.y1 = firstSegment.y1;
          break;
        case 'right':
          prependSegment.x1 = connStartPoint.x /* + startArrowSize*/;
          prependSegment.y1 = firstSegment.y1;
          break;
        case 'top':
          prependSegment.x1 = firstSegment.x1;
          prependSegment.y1 = connStartPoint.y /* - startArrowSize*/;
          break;
        case 'bottom':
          prependSegment.x1 = firstSegment.x1;
          prependSegment.y1 = connStartPoint.y /* + startArrowSize*/;
          break;
      }
      path.unshift(prependSegment);
    }
    return path;
  }
  appendPathWithArrowStaffSegment(path, connEndPoint /*, endArrowSize*/, endSide) {
    if (path.length > 0) {
      const lastSegment = path[path.length - 1],
        appendSegment = {
          x1: lastSegment.x2,
          y1: lastSegment.y2
        };
      switch (endSide) {
        case 'left':
          appendSegment.x2 = connEndPoint.x /* - endArrowSize*/;
          appendSegment.y2 = lastSegment.y2;
          break;
        case 'right':
          appendSegment.x2 = connEndPoint.x /* + endArrowSize*/;
          appendSegment.y2 = lastSegment.y2;
          break;
        case 'top':
          appendSegment.x2 = lastSegment.x2;
          appendSegment.y2 = connEndPoint.y /* - endArrowSize*/;
          break;
        case 'bottom':
          appendSegment.x2 = lastSegment.x2;
          appendSegment.y2 = connEndPoint.y /* + endArrowSize*/;
          break;
      }
      path.push(appendSegment);
    }
    return path;
  }
  optimizePath(path) {
    const optPath = [];
    let prevSegment, curSegment;
    if (path.length > 0) {
      prevSegment = path.shift();
      optPath.push(prevSegment);
      while (path.length > 0) {
        curSegment = path.shift();
        // both segments are as good as equal
        if (equalEnough(prevSegment.x1, curSegment.x1) && equalEnough(prevSegment.y1, curSegment.y1) && equalEnough(prevSegment.x2, curSegment.x2) && equalEnough(prevSegment.y2, curSegment.y2)) {
          prevSegment = curSegment;
        }
        // both segments are horizontal or very nearly so
        else if (equalEnough(prevSegment.y1, prevSegment.y2) && equalEnough(curSegment.y1, curSegment.y2)) {
          prevSegment.x2 = curSegment.x2;
        }
        // both segments are vertical or very nearly so
        else if (equalEnough(prevSegment.x1, prevSegment.x2) && equalEnough(curSegment.x1, curSegment.x2)) {
          prevSegment.y2 = curSegment.y2;
        }
        // segments have different orientation (path turn)
        else {
          optPath.push(curSegment);
          prevSegment = curSegment;
        }
      }
    }
    return optPath;
  }
}
RectangularPathFinder._$name = 'RectangularPathFinder';

// Determine a line segments drawing direction
function drawingDirection(pointSet) {
  if (pointSet.x1 === pointSet.x2) {
    return pointSet.y2 > pointSet.y1 ? 'd' : 'u';
  }
  return pointSet.x2 > pointSet.x1 ? 'r' : 'l';
}
// Determine a line segments length
function segmentLength(pointSet) {
  return pointSet.x1 === pointSet.x2 ? pointSet.y2 - pointSet.y1 : pointSet.x2 - pointSet.x1;
}
// Define an arc to tie two line segments together
function arc(pointSet, nextPointSet, radius) {
  const corner = drawingDirection(pointSet) + drawingDirection(nextPointSet),
    // Flip x if this or next segment is drawn right to left
    rx = radius * (corner.includes('l') ? -1 : 1),
    // Flip y if this or next segment is drawn bottom to top
    ry = radius * (corner.includes('u') ? -1 : 1),
    // Positive (0) or negative (1) angle
    sweep = corner === 'ur' || corner === 'lu' || corner === 'dl' || corner === 'rd' ? 1 : 0;
  return `a${rx},${ry} 0 0 ${sweep} ${rx},${ry}`;
}
// Define a line for a set of points, tying it together with the next set with an arc when applicable
function line(pointSet, nextPointSet, location, radius, prevRadius) {
  // Horizontal or vertical line
  let line = pointSet.x1 === pointSet.x2 ? 'v' : 'h',
    useRadius = radius;
  // Add an arc?
  if (radius) {
    const
      // Length of this line segment
      length = segmentLength(pointSet),
      // Length of the next one. Both are needed to determine max radius (half of the shortest delta)
      nextLength = nextPointSet ? Math.abs(segmentLength(nextPointSet)) : Number.MAX_SAFE_INTEGER,
      // Line direction
      sign = Math.sign(length);
    // If we are not passed a radius from the previous line drawn, we use the configured radius. It is used to shorten
    // this lines length to fit the arc that connects it to the previous line
    if (prevRadius == null) {
      prevRadius = radius;
    }
    // We cannot use a radius larger than half our or our successor's length, doing so would make the segment too long
    // when the arc is created
    if (Math.abs(length) < radius * 2 || nextLength < radius * 2) {
      useRadius = Math.min(Math.abs(length), nextLength) / 2;
    }
    const
      // Radius of neighbouring arcs, subtracted from length below...
      subtract = location === 'single' ? 0 : location === 'first' ? useRadius : location === 'between' ? prevRadius + useRadius : /*last*/prevRadius,
      // ...to produce the length of the line segment to draw
      useLength = length - subtract * sign;
    // Apply line segment length, unless it passed over 0 in which case we stick to 0
    line += Math.sign(useLength) !== sign ? 0 : useLength;
    // Add an arc if applicable
    if (location !== 'last' && location !== 'single' && useRadius > 0) {
      line += ` ${arc(pointSet, nextPointSet, useRadius)}`;
    }
  }
  // Otherwise take a shorter code path
  else {
    line += segmentLength(pointSet);
  }
  return {
    line,
    currentRadius: radius !== useRadius ? useRadius : null
  };
}
// Define an SVG path base on points from the path finder.
// Each segment in the path can be joined by an arc
function pathMapper(radius, points) {
  const {
    length
  } = points;
  if (!length) {
    return '';
  }
  let currentRadius = null;
  return `M${points[0].x1},${points[0].y1} ${points.map((pointSet, i) => {
    // Segment placement among all segments, used to determine if an arc should be added
    const location = length === 1 ? 'single' : i === length - 1 ? 'last' : i === 0 ? 'first' : 'between',
      lineSpec = line(pointSet, points[i + 1], location, radius, currentRadius);
    ({
      currentRadius
    } = lineSpec);
    return lineSpec.line;
  }).join(' ')}`;
}
// Mixin that holds the code needed to generate DomConfigs for dependency lines
var DependencyLineGenerator = (Target => class DependencyLineGenerator extends Target {
  static $name = 'DependencyLineGenerator';
  lineCache = {};
  onSVGReady() {
    const me = this;
    me.pathFinder = new RectangularPathFinder({
      ...me.pathFinderConfig,
      client: me.client
    });
    me.lineDefAdjusters = me.createLineDefAdjusters();
    me.createMarker();
  }
  changeRadius(radius) {
    if (radius !== null) {
      ObjectHelper.assertNumber(radius, 'radius');
    }
    return radius;
  }
  updateRadius() {
    if (!this.isConfiguring) {
      this.reset();
    }
  }
  updateRenderer() {
    if (!this.isConfiguring) {
      this.reset();
    }
  }
  changeClickWidth(width) {
    if (width !== null) {
      ObjectHelper.assertNumber(width, 'clickWidth');
    }
    return width;
  }
  updateClickWidth() {
    if (!this.isConfiguring) {
      this.reset();
    }
  }
  //region Marker
  createMarker() {
    var _me$marker;
    const me = this,
      {
        markerDef
      } = me,
      svg = this.client.svgCanvas,
      // SVG markers has to use an id, we want the id to be per scheduler when using multiple
      markerId = markerDef ? `${me.client.id}-arrowEnd` : 'arrowEnd';
    (_me$marker = me.marker) === null || _me$marker === void 0 ? void 0 : _me$marker.remove();
    svg.style.setProperty('--scheduler-dependency-marker', `url(#${markerId})`);
    me.marker = DomHelper.createElement({
      parent: svg,
      id: markerId,
      tag: 'marker',
      className: 'b-sch-dependency-arrow',
      ns: 'http://www.w3.org/2000/svg',
      markerHeight: 11,
      markerWidth: 11,
      refX: 8.5,
      refY: 3,
      viewBox: '0 0 9 6',
      orient: 'auto-start-reverse',
      markerUnits: 'userSpaceOnUse',
      retainElement: true,
      children: [{
        tag: 'path',
        ns: 'http://www.w3.org/2000/svg',
        d: me.markerDef ?? 'M3,0 L3,6 L9,3 z'
      }]
    });
  }
  updateMarkerDef() {
    if (!this.isConfiguring) {
      this.createMarker();
    }
  }
  //endregion
  //region DomConfig
  getAssignmentElement(assignment) {
    var _this$client$features, _this$client$features2;
    // If we are dragging an event, we need to use the proxy element
    // (which is not the original element if we are not constrained to timeline)
    const proxyElement = (_this$client$features = this.client.features.eventDrag) === null || _this$client$features === void 0 ? void 0 : (_this$client$features2 = _this$client$features.getProxyElement) === null || _this$client$features2 === void 0 ? void 0 : _this$client$features2.call(_this$client$features, assignment);
    return proxyElement || this.client.getElementFromAssignmentRecord(assignment);
  }
  // Generate a DomConfig for a dependency line between two assignments (tasks in Gantt)
  getDomConfigs(dependency, fromAssignment, toAssignment, forceBoxes) {
    const me = this,
      key = me.getDependencyKey(dependency, fromAssignment, toAssignment),
      // Under certain circumstances (scrolling) we might be able to reuse the previous DomConfig.
      cached = me.lineCache[key];
    // Create line def if not cached, or we are live drawing and have event elements (dragging, transitioning etc)
    if (me.constructLineCache || !cached || forceBoxes || me.drawingLive && (me.getAssignmentElement(fromAssignment) || me.getAssignmentElement(toAssignment))) {
      const lineDef = me.prepareLineDef(dependency, fromAssignment, toAssignment, forceBoxes),
        points = lineDef && me.pathFinder.findPath(lineDef, me.lineDefAdjusters),
        {
          client,
          clickWidth
        } = me,
        {
          toEvent
        } = dependency;
      if (points) {
        var _me$renderer;
        const highlighted = me.highlighted.get(dependency),
          domConfig = {
            tag: 'path',
            ns: 'http://www.w3.org/2000/svg',
            d: pathMapper(me.radius ?? 0, points),
            role: 'presentation',
            dataset: {
              syncId: key,
              depId: dependency.id,
              fromId: fromAssignment.id,
              toId: toAssignment.id
            },
            elementData: {
              dependency,
              points
            },
            class: {
              [me.baseCls]: 1,
              [dependency.cls]: dependency.cls,
              // Data highlight
              [dependency.highlighted]: dependency.highlighted,
              // Feature highlight
              [highlighted && [...highlighted].join(' ')]: highlighted,
              [me.noMarkerCls]: lineDef.hideMarker,
              'b-inactive': dependency.active === false,
              'b-sch-bidirectional-line': dependency.bidirectional,
              'b-readonly': dependency.readOnly,
              // If target event is outside the view add special CSS class to hide marker (arrow)
              'b-sch-dependency-ends-outside': !toEvent.milestone && (toEvent.endDate <= client.startDate || client.endDate <= toEvent.startDate) || toEvent.milestone && (toEvent.endDate < client.startDate || client.endDate < toEvent.startDate)
            }
          };
        (_me$renderer = me.renderer) === null || _me$renderer === void 0 ? void 0 : _me$renderer.call(me, {
          domConfig,
          points,
          dependencyRecord: dependency,
          fromAssignmentRecord: fromAssignment,
          toAssignmentRecord: toAssignment,
          fromBox: lineDef.startBox,
          toBox: lineDef.endBox,
          fromSide: lineDef.startSide,
          toSide: lineDef.endSide
        });
        const configs = [domConfig];
        if (clickWidth > 1) {
          configs.push({
            ...domConfig,
            // Shallow on purpose, to not waste perf cloning deeply
            class: {
              ...domConfig.class,
              'b-click-area': 1
            },
            dataset: {
              ...domConfig.dataset,
              syncId: `${domConfig.dataset.syncId}-click-area`
            },
            style: {
              strokeWidth: clickWidth
            }
          });
        }
        return me.lineCache[key] = configs;
      }
      // Nothing to draw or cache
      return me.lineCache[key] = null;
    }
    return cached;
  }
  //endregion
  //region Bounds
  // Generates `otherBoxes` config for rectangular path finder, which push dependency line to the row boundary.
  // It should be enough to return single box with top/bottom taken from row top/bottom and left/right taken from source
  // box, extended by start arrow margin to both sides.
  generateBoundaryBoxes(box, side) {
    // We need two boxes for the bottom edge, because otherwise path cannot be found. Ideally that shouldn't be
    // necessary. Other solution would be to adjust bottom by -1px, but that would make some dependency lines to take
    // 1px different path on a row boundary, which doesn't look nice (but slightly more performant)
    if (side === 'bottom') {
      return [{
        start: box.left,
        end: box.left + box.width / 2,
        top: box.rowTop,
        bottom: box.rowBottom
      }, {
        start: box.left + box.width / 2,
        end: box.right,
        top: box.rowTop,
        bottom: box.rowBottom
      }];
    } else {
      return [{
        start: box.left - this.pathFinder.startArrowMargin,
        end: box.right + this.pathFinder.startArrowMargin,
        top: box.rowTop,
        bottom: box.rowBottom
      }];
    }
  }
  // Bounding box for an assignment, uses elements bounds if rendered
  getAssignmentBounds(assignment) {
    const {
        client
      } = this,
      element = this.getAssignmentElement(assignment);
    if (element && !client.isExporting) {
      const rectangle = Rectangle.from(element, this.relativeTo);
      if (client.isHorizontal) {
        let row = client.getRowById(assignment.resource.id);
        // Outside of its row? It is being dragged, resolve new row
        if (rectangle.y < row.top || rectangle.bottom > row.bottom) {
          const overRow = client.rowManager.getRowAt(rectangle.center.y, true);
          if (overRow) {
            row = overRow;
          }
        }
        rectangle.rowTop = row.top;
        rectangle.rowBottom = row.bottom;
      }
      return rectangle;
    }
    return client.isEngineReady && client.getAssignmentEventBox(assignment, true);
  }
  //endregion
  //region Sides
  getConnectorStartSide(timeSpanRecord) {
    return this.client.currentOrientation.getConnectorStartSide(timeSpanRecord);
  }
  getConnectorEndSide(timeSpanRecord) {
    return this.client.currentOrientation.getConnectorEndSide(timeSpanRecord);
  }
  getDependencyStartSide(dependency) {
    const {
      fromEvent,
      type,
      fromSide
    } = dependency;
    if (fromSide) {
      return fromSide;
    }
    switch (true) {
      case type === DependencyModel.Type.StartToEnd:
      case type === DependencyModel.Type.StartToStart:
        return this.getConnectorStartSide(fromEvent);
      case type === DependencyModel.Type.EndToStart:
      case type === DependencyModel.Type.EndToEnd:
        return this.getConnectorEndSide(fromEvent);
      default:
        // Default value might not be applied yet when rendering early in Pro / Gantt
        return this.getConnectorEndSide(fromEvent);
    }
  }
  getDependencyEndSide(dependency) {
    const {
      toEvent,
      type,
      toSide
    } = dependency;
    if (toSide) {
      return toSide;
    }
    // Fallback to view trait if dependency end side is not given /*or can be obtained from type*/
    switch (true) {
      case type === DependencyModel.Type.EndToEnd:
      case type === DependencyModel.Type.StartToEnd:
        return this.getConnectorEndSide(toEvent);
      case type === DependencyModel.Type.EndToStart:
      case type === DependencyModel.Type.StartToStart:
        return this.getConnectorStartSide(toEvent);
      default:
        // Default value might not be applied yet when rendering early in Pro / Gantt
        return this.getConnectorStartSide(toEvent);
    }
  }
  //endregion
  //region Line def
  // An array of functions used to alter path config when no path found.
  // It first tries to shrink arrow margins and secondly hides arrows entirely
  createLineDefAdjusters() {
    const {
      client
    } = this;
    function shrinkArrowMargins(lineDef) {
      const {
        barMargin
      } = client;
      let adjusted = false;
      if (lineDef.startArrowMargin > barMargin || lineDef.endArrowMargin > barMargin) {
        lineDef.startArrowMargin = lineDef.endArrowMargin = barMargin;
        adjusted = true;
      }
      return adjusted ? lineDef : adjusted;
    }
    function resetArrowMargins(lineDef) {
      let adjusted = false;
      if (lineDef.startArrowMargin > 0 || lineDef.endArrowMargin > 0) {
        lineDef.startArrowMargin = lineDef.endArrowMargin = 0;
        adjusted = true;
      }
      return adjusted ? lineDef : adjusted;
    }
    function shrinkHorizontalMargin(lineDef, originalLineDef) {
      let adjusted = false;
      if (lineDef.horizontalMargin > 2) {
        lineDef.horizontalMargin = 1;
        adjusted = true;
        originalLineDef.hideMarker = true;
      }
      return adjusted ? lineDef : adjusted;
    }
    return [shrinkArrowMargins, resetArrowMargins, shrinkHorizontalMargin];
  }
  // Overridden in Gantt
  adjustLineDef(dependency, lineDef) {
    return lineDef;
  }
  // Prepare data to feed to the path finder
  prepareLineDef(dependency, fromAssignment, toAssignment, forceBoxes) {
    const me = this,
      startSide = me.getDependencyStartSide(dependency),
      endSide = me.getDependencyEndSide(dependency),
      startRectangle = (forceBoxes === null || forceBoxes === void 0 ? void 0 : forceBoxes.from) ?? me.getAssignmentBounds(fromAssignment),
      endRectangle = (forceBoxes === null || forceBoxes === void 0 ? void 0 : forceBoxes.to) ?? me.getAssignmentBounds(toAssignment),
      otherBoxes = [];
    if (!startRectangle || !endRectangle) {
      return null;
    }
    let {
      startArrowMargin,
      verticalMargin
    } = me.pathFinder;
    if (me.client.isHorizontal) {
      // Only add otherBoxes if assignments are in different resources
      if (startRectangle.rowTop != null && startRectangle.rowTop !== endRectangle.rowTop) {
        otherBoxes.push(...me.generateBoundaryBoxes(startRectangle, startSide));
      }
      // Do not change start arrow margin in case dependency is bidirectional
      if (!dependency.bidirectional) {
        if (/(top|bottom)/.test(startSide)) {
          startArrowMargin = me.client.barMargin / 2;
        }
        verticalMargin = me.client.barMargin / 2;
      }
    }
    return me.adjustLineDef(dependency, {
      startBox: startRectangle,
      endBox: endRectangle,
      otherBoxes,
      startArrowMargin,
      verticalMargin,
      otherVerticalMargin: 0,
      otherHorizontalMargin: 0,
      startSide,
      endSide
    });
  }
  //endregion
  //region Cache
  // All dependencies are about to be drawn, check if we need to build the line cache
  beforeDraw() {
    super.beforeDraw();
    if (!Object.keys(this.lineCache).length) {
      this.constructLineCache = true;
    }
  }
  // All dependencies are drawn, we no longer need to rebuild the cache
  afterDraw() {
    super.afterDraw();
    this.constructLineCache = false;
  }
  reset() {
    super.reset();
    this.lineCache = {};
  }
  //endregion
});

/**
 * @module Scheduler/feature/mixin/DependencyTooltip
 */
const
  // Map dependency type to side of a box, for displaying an icon in the tooltip
  fromBoxSide = ['start', 'start', 'end', 'end'],
  toBoxSide = ['start', 'end', 'start', 'end'];
/**
 * Mixin that adds tooltip support to the {@link Scheduler/feature/Dependencies} feature.
 * @mixin
 */
var DependencyTooltip = (Target => class DependencyTooltip extends Target {
  static $name = 'DependencyTooltip';
  static configurable = {
    /**
     * Set to true to show a tooltip when hovering a dependency line
     * @config {Boolean}
     */
    showTooltip: true,
    /**
     * A template function allowing you to configure the contents of the tooltip shown when hovering a
     * dependency line. You can return either an HTML string or a {@link DomConfig} object.
     * @prp {Function} tooltipTemplate
     * @param {Scheduler.model.DependencyBaseModel} dependency The dependency record
     * @returns {String|DomConfig}
     */
    tooltipTemplate(dependency) {
      return {
        children: [{
          className: 'b-sch-dependency-tooltip',
          children: [{
            tag: 'label',
            text: this.L('L{Dependencies.from}')
          }, {
            text: dependency.fromEvent.name
          }, {
            className: `b-sch-box b-${dependency.fromSide || fromBoxSide[dependency.type]}`
          }, {
            tag: 'label',
            text: this.L('L{Dependencies.to}')
          }, {
            text: dependency.toEvent.name
          }, {
            className: `b-sch-box b-${dependency.toSide || toBoxSide[dependency.type]}`
          }]
        }]
      };
    },
    /**
     * A tooltip config object that will be applied to the dependency hover tooltip. Can be used to for example
     * customize delay
     * @config {TooltipConfig}
     */
    tooltip: {
      $config: 'nullify',
      value: {}
    }
  };
  changeTooltip(tooltip, old) {
    const me = this;
    old === null || old === void 0 ? void 0 : old.destroy();
    if (!me.showTooltip || !tooltip) {
      return null;
    }
    return Tooltip.new({
      align: 'b-t',
      id: `${me.client.id}-dependency-tip`,
      forSelector: `.b-timelinebase:not(.b-eventeditor-editing,.b-taskeditor-editing,.b-resizing-event,.b-dragcreating,.b-dragging-event,.b-creating-dependency) .${me.baseCls}`,
      forElement: me.client.timeAxisSubGridElement,
      showOnHover: true,
      hoverDelay: 0,
      hideDelay: 0,
      anchorToTarget: false,
      textContent: false,
      // Skip max-width setting
      trackMouse: false,
      getHtml: me.getHoverTipHtml.bind(me)
    }, tooltip);
  }
  /**
   * Generates DomConfig content for the tooltip shown when hovering a dependency
   * @param {Object} tooltipConfig
   * @returns {DomConfig} DomConfig used as tooltips content
   * @private
   */
  getHoverTipHtml({
    activeTarget
  }) {
    return this.tooltipTemplate(this.resolveDependencyRecord(activeTarget));
  }
});

const eventNameMap = {
    click: 'Click',
    dblclick: 'DblClick',
    contextmenu: 'ContextMenu'
  },
  emptyObject = Object.freeze({});
/**
 * @module Scheduler/feature/Dependencies
 */
const collectLinkedAssignments = assignment => {
  var _assignment$resource;
  const result = [assignment];
  if ((_assignment$resource = assignment.resource) !== null && _assignment$resource !== void 0 && _assignment$resource.hasLinks) {
    // Fake linked assignments
    result.push(...assignment.resource.$links.map(l => ({
      id: `${l.id}_${assignment.id}`,
      resource: l,
      event: assignment.event,
      drawDependencies: assignment.drawDependencies
    })));
  }
  return result;
};
/**
 * Feature that draws dependencies between events. Uses a {@link Scheduler.data.DependencyStore} to determine which
 * dependencies to draw, if none is defined one will be created automatically. Dependencies can also be specified as
 * `scheduler.dependencies`, see example below:
 *
 * {@inlineexample Scheduler/feature/Dependencies.js}
 *
 * Dependencies also work in vertical mode:
 *
 * {@inlineexample Scheduler/feature/DependenciesVertical.js}
 *
 * To customize the dependency tooltip, you can provide the {@link #config-tooltip} config and specify a
 * {@link Core.widget.Tooltip#config-getHtml} function. For example:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         dependencies : {
 *             tooltip : {
 *                 getHtml({ activeTarget }) {
 *                     const dependencyModel = scheduler.resolveDependencyRecord(activeTarget);
 *
 *                     if (!dependencyModel) return null;
 *
 *                     const { fromEvent, toEvent } = dependencyModel;
 *
 *                     return `${fromEvent.name} (${fromEvent.id}) -> ${toEvent.name} (${toEvent.id})`;
 *                 }
 *             }
 *         }
 *     }
 * }
 * ```
 *
 * ## Styling dependency lines
 *
 * You can easily customize the arrows drawn between events. To change all arrows, apply the following basic SVG CSS:
 *
 * ```css
 * .b-sch-dependency {
 *    stroke-width: 2;
 *    stroke : red;
 * }
 *
 * .b-sch-dependency-arrow {
 *     fill: red;
 * }
 * ```
 *
 * To style an individual dependency line, you can provide a [cls](#Scheduler/model/DependencyModel#field-cls) in your
 * data:
 *
 * ```json
 * {
 *     "id"   : 9,
 *     "from" : 7,
 *     "to"   : 8,
 *     "cls"  : "special-dependency"
 * }
 * ```
 *
 * ```scss
 * // Make line dashed
 * .b-sch-dependency {
 *    stroke-dasharray: 5, 5;
 * }
 * ```
 *
 * To customize the marker used for the lines (the arrow header), you can supply a SVG path definition to the
 * {@link #config-markerDef} config:
 *
 * {@inlineexample Scheduler/feature/DependenciesMarker.js}
 *
 * You can also specify a {@link #config-radius} to get lines with rounded "corners", for a less boxy look:
 *
 * {@inlineexample Scheduler/feature/DependenciesRadius.js}
 *
 * For advanced use cases, you can also manipulate the {@link DomConfig} used to create a dependency line in a
 * {@link #config-renderer} function.
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @mixes Core/mixin/Delayable
 * @mixes Scheduler/feature/mixin/DependencyCreation
 * @mixes Scheduler/feature/mixin/DependencyTooltip
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/dependencies
 * @classtype dependencies
 * @feature
 */
class Dependencies extends InstancePlugin.mixin(AttachToProjectMixin, Delayable, DependencyCreation, DependencyGridCache, DependencyLineGenerator, DependencyTooltip) {
  static $name = 'Dependencies';
  /**
   * Fired when dependencies are rendered
   * @on-owner
   * @event dependenciesDrawn
   */
  //region Config
  static configurable = {
    /**
     * The CSS class to add to a dependency line when hovering over it
     * @config {String}
     * @default
     * @private
     */
    overCls: 'b-sch-dependency-over',
    /**
     * The CSS class applied to dependency lines
     * @config {String}
     * @default
     * @private
     */
    baseCls: 'b-sch-dependency',
    /**
     * The CSS class applied to a too narrow dependency line (to hide markers)
     * @config {String}
     * @default
     * @private
     */
    noMarkerCls: 'b-sch-dependency-markerless',
    /**
     * SVG path definition used as marker (arrow head) for the dependency lines.
     * Should fit in a viewBox that is 9 x 6.
     *
     * ```javascript
     * const scheduler = new Scheduler({
     *     features : {
     *         dependencies : {
     *             // Circular marker
     *             markerDef : 'M 2,3 a 3,3 0 1,0 6,0 a 3,3 0 1,0 -6,0'
     *         }
     *     }
     * });
     * ```
     *
     * @config {String}
     * @default 'M3,0 L3,6 L9,3 z'
     */
    markerDef: null,
    /**
     * Radius (in px) used to draw arcs where dependency line segments connect. Specify it to get a rounded look.
     * The radius will during drawing be reduced as needed on a per segment basis to fit lines.
     *
     * ```javascript
     * const scheduler = new Scheduler({
     *     features : {
     *         dependencies : {
     *             // Round the corner where line segments connect, similar to 'border-radius: 5px'
     *             radius : 5
     *         }
     *     }
     * });
     * ```
     *
     * <div class="note">Using a radius slightly degrades dependency rendering performance. If your app displays
     * a lot of dependencies, it might be worth taking this into account when deciding if you want to use radius
     * or not</div>
     *
     * @config {Number}
     */
    radius: null,
    /**
     * Renderer function, supply one if you want to manipulate the {@link DomConfig} object used to draw a
     * dependency line between two assignments.
     *
     * ```javascript
     * const scheduler = new Scheduler({
     *     features : {
     *         dependencies : {
     *             renderer({ domConfig, fromAssignmentRecord : from, toAssignmentRecord : to }) {
     *                 // Add a custom CSS class to dependencies between important assignments
     *                 domConfig.class.important = from.important || to.important;
     *                 domConfig.class.veryImportant = from.important && to.important;
     *             }
     *         }
     *     }
     * }
     * ```
     *
     * @param {Object} renderData
     * @param {DomConfig} renderData.domConfig that will be used to create the dependency line, can be manipulated by the
     * renderer
     * @param {Scheduler.model.DependencyModel} renderData.dependencyRecord The dependency being rendered
     * @param {Scheduler.model.AssignmentModel} renderData.fromAssignmentRecord Drawing line from this assignment
     * @param {Scheduler.model.AssignmentModel} renderData.toAssignmentRecord Drawing line to this assignment
     * @param {Object[]} renderData.points A collection of points making up the line segments for the dependency
     * line. Read-only in the renderer, any manipulation should be done to `domConfig`
     * @param {Core.helper.util.Rectangle} renderData.fromBox Bounds for the fromAssignment's element
     * @param {Core.helper.util.Rectangle} renderData.toBox Bounds for the toAssignment's element
     * @param {'top'|'right'|'bottom'|'left'} renderData.fromSide Drawn from this side of the fromAssignment
     * @param {'top'|'right'|'bottom'|'left'} renderData.toSide Drawn to this side of the fromAssignment
     * @prp {Function}
     */
    renderer: null,
    /**
     * Specify `true` to highlight incoming and outgoing dependencies when hovering an event.
     * @prp {Boolean}
     */
    highlightDependenciesOnEventHover: null,
    /**
     * Specify `false` to prevent dependencies from being drawn during scroll, for smoother scrolling in schedules
     * with lots of dependencies. Dependencies will be drawn when scrolling stops instead.
     * @prp {Boolean}
     * @default
     */
    drawOnScroll: true,
    /**
     * The clickable/touchable width of the dependency line in pixels. Setting this to a number greater than 1 will
     * draw an invisible but clickable line along the same path as the dependency line, making it easier to click.
     * The tradeoff is that twice as many lines will be drawn, which can affect performance.
     * @prp {Number}
     */
    clickWidth: null,
    /**
     * By default, the refresh of dependencies is buffered by 10 milliseconds so that multiple changes
     * which may cause the dependency lines to become invalid are coalesced into one refresh. This is more
     * efficient, but may mean the dependency lines may lag behind expectations when moving a pointer.
     *
     * Set this to `true` to update dependency lines immediately upon any change which causes them
     * to require an update.
     * @prp {Boolean}
     * @default false
     * @private
     */
    immediateRefresh: null
  };
  static delayable = {
    doRefresh: 10
  };
  static get pluginConfig() {
    return {
      chain: ['render', 'onPaint', 'onElementClick', 'onElementDblClick', 'onElementContextMenu', 'onElementMouseOver', 'onElementMouseOut', 'bindStore'],
      assign: ['getElementForDependency', 'getElementsForDependency', 'resolveDependencyRecord']
    };
  }
  domConfigs = new Map();
  drawingLive = false;
  lastScrollX = null;
  highlighted = new Map();
  // Cached lookups
  visibleResources = null;
  usingLinks = null;
  visibleDateRange = null;
  relativeTo = null;
  //endregion
  //region Init & destroy
  construct(client, config) {
    super.construct(client, config);
    const {
      scheduledEventName
    } = client;
    client.ion({
      svgCanvasCreated: 'onSVGReady',
      // These events trigger live refresh behaviour
      animationStart: 'refresh',
      // eventDrag in Scheduler, taskDrag in Gantt
      [scheduledEventName + 'DragStart']: 'refresh',
      [scheduledEventName + 'DragAbort']: 'refresh',
      [scheduledEventName + 'ResizeStart']: 'refresh',
      [scheduledEventName + 'SegmentDragStart']: 'refresh',
      [scheduledEventName + 'SegmentResizeStart']: 'refresh',
      // These events shift the surroundings to such extent that grid cache needs rebuilding to be sure that
      // all dependencies are considered
      timelineViewportResize: 'reset',
      timeAxisViewModelUpdate: 'reset',
      toggleNode: 'reset',
      thisObj: this
    });
    client.rowManager.ion({
      refresh: 'reset',
      // For example when changing barMargin or rowHeight
      changeTotalHeight: 'reset',
      // For example when collapsing groups
      thisObj: this
    });
    this.bindStore(client.store);
  }
  doDisable(disable) {
    if (!this.isConfiguring) {
      // Need a flag to clear dependencies when disabled, since drawing is otherwise disabled too
      this._isDisabling = disable;
      this.draw();
      this._isDisabling = false;
    }
    super.doDisable(disable);
  }
  //endregion
  //region RefreshTriggers
  get rowStore() {
    return this.client.isVertical ? this.client.resourceStore : this.client.store;
  }
  // React to replacing or refreshing a display store
  bindStore(store) {
    const me = this;
    if (!me.client.isVertical) {
      me.detachListeners('store');
      if (me.client.usesDisplayStore) {
        store === null || store === void 0 ? void 0 : store.ion({
          name: 'store',
          refresh: 'onStoreRefresh',
          thisObj: me
        });
        me.reset();
      }
    }
  }
  onStoreRefresh() {
    this.reset();
  }
  attachToProject(project) {
    super.attachToProject(project);
    project === null || project === void 0 ? void 0 : project.ion({
      name: 'project',
      commitFinalized: 'reset',
      thisObj: this
    });
  }
  attachToResourceStore(resourceStore) {
    super.attachToResourceStore(resourceStore);
    resourceStore === null || resourceStore === void 0 ? void 0 : resourceStore.ion({
      name: 'resourceStore',
      change: 'onResourceStoreChange',
      refresh: 'onResourceStoreChange',
      thisObj: this
    });
  }
  onResourceStoreChange() {
    // Might have added or removed links, need to re-cache the flag
    this.usingLinks = null;
    this.reset();
  }
  attachToEventStore(eventStore) {
    super.attachToEventStore(eventStore);
    eventStore === null || eventStore === void 0 ? void 0 : eventStore.ion({
      name: 'eventStore',
      refresh: 'reset',
      thisObj: this
    });
  }
  attachToAssignmentStore(assignmentStore) {
    super.attachToAssignmentStore(assignmentStore);
    assignmentStore === null || assignmentStore === void 0 ? void 0 : assignmentStore.ion({
      name: 'assignmentStore',
      refresh: 'reset',
      thisObj: this
    });
  }
  attachToDependencyStore(dependencyStore) {
    super.attachToDependencyStore(dependencyStore);
    dependencyStore === null || dependencyStore === void 0 ? void 0 : dependencyStore.ion({
      name: 'dependencyStore',
      change: 'reset',
      refresh: 'reset',
      thisObj: this
    });
  }
  updateDrawOnScroll(drawOnScroll) {
    const me = this;
    me.detachListeners('scroll');
    if (drawOnScroll) {
      me.client.ion({
        name: 'scroll',
        scroll: 'doRefresh',
        horizontalScroll: 'onHorizontalScroll',
        prio: -100,
        // After Scheduler draws on scroll, since we target elements
        thisObj: me
      });
    } else {
      me.client.scrollable.ion({
        name: 'scroll',
        scrollEnd: 'draw',
        thisObj: me
      });
      me.client.timeAxisSubGrid.scrollable.ion({
        name: 'scroll',
        scrollEnd: 'draw',
        thisObj: me
      });
    }
  }
  onHorizontalScroll({
    subGrid,
    scrollX
  }) {
    if (scrollX !== this.lastScrollX && subGrid === this.client.timeAxisSubGrid) {
      this.lastScrollX = scrollX;
      this.draw();
    }
  }
  onPaint() {
    this.refresh();
  }
  //endregion
  //region Dependency types
  // Used by DependencyField
  static getLocalizedDependencyType(type) {
    return type ? this.L(`L{DependencyType.${type}}`) : '';
  }
  //endregion
  //region Elements
  getElementForDependency(dependency, fromAssignment, toAssignment) {
    return this.getElementsForDependency(dependency, fromAssignment, toAssignment)[0];
  }
  // NOTE: If we ever make this public we should change it to use the syncIdMap. Currently not needed since only
  // used in tests
  getElementsForDependency(dependency, fromAssignment, toAssignment) {
    // Selector targeting all instances of a dependency
    let selector = `[data-dep-id="${dependency.id}"]`;
    // Optionally narrow it down to a single instance (assignment)
    if (fromAssignment) {
      selector += `[data-from-id="${fromAssignment.id}"]`;
    }
    if (toAssignment) {
      selector += `[data-to-id="${toAssignment.id}"]`;
    }
    return Array.from(this.client.svgCanvas.querySelectorAll(selector));
  }
  /**
   * Returns the dependency record for a DOM element
   * @param {HTMLElement} element The dependency line element
   * @returns {Scheduler.model.DependencyModel} The dependency record
   */
  resolveDependencyRecord(element) {
    var _element$elementData;
    return (_element$elementData = element.elementData) === null || _element$elementData === void 0 ? void 0 : _element$elementData.dependency;
  }
  isDependencyElement(element) {
    return element.matches(`.${this.baseCls}`);
  }
  //endregion
  //region DOM Events
  onElementClick(event) {
    const dependency = this.resolveDependencyRecord(event.target);
    if (dependency) {
      const eventName = eventNameMap[event.type];
      /**
       * Fires on the owning Scheduler/Gantt when a context menu event is registered on a dependency line.
       * @event dependencyContextMenu
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler
       * @param {Scheduler.model.DependencyModel} dependency
       * @param {MouseEvent} event
       */
      /**
       * Fires on the owning Scheduler/Gantt when a click is registered on a dependency line.
       * @event dependencyClick
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler
       * @param {Scheduler.model.DependencyModel} dependency
       * @param {MouseEvent} event
       */
      /**
       * Fires on the owning Scheduler/Gantt when a double click is registered on a dependency line.
       * @event dependencyDblClick
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler
       * @param {Scheduler.model.DependencyModel} dependency
       * @param {MouseEvent} event
       */
      this.client.trigger(`dependency${eventName}`, {
        dependency,
        event
      });
    }
  }
  onElementDblClick(event) {
    return this.onElementClick(event);
  }
  onElementContextMenu(event) {
    return this.onElementClick(event);
  }
  onElementMouseOver(event) {
    const me = this,
      dependency = me.resolveDependencyRecord(event.target);
    if (dependency) {
      /**
       * Fires on the owning Scheduler/Gantt when the mouse moves over a dependency line.
       * @event dependencyMouseOver
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler
       * @param {Scheduler.model.DependencyModel} dependency
       * @param {MouseEvent} event
       */
      me.client.trigger('dependencyMouseOver', {
        dependency,
        event
      });
      if (me.overCls) {
        me.highlight(dependency);
      }
    }
  }
  onElementMouseOut(event) {
    const me = this,
      dependency = me.resolveDependencyRecord(event.target);
    if (dependency) {
      /**
       * Fires on the owning Scheduler/Gantt when the mouse moves out of a dependency line.
       * @event dependencyMouseOut
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler
       * @param {Scheduler.model.DependencyModel} dependency
       * @param {MouseEvent} event
       */
      me.client.trigger('dependencyMouseOut', {
        dependency,
        event
      });
      if (me.overCls) {
        me.unhighlight(dependency);
      }
    }
  }
  //endregion
  //region Export
  // Export calls this fn to determine if a dependency should be included or not
  isDependencyVisible(dependency) {
    const me = this,
      {
        rowStore
      } = me,
      {
        fromEvent,
        toEvent
      } = dependency;
    // Bail out early in case source or target doesn't exist
    if (!fromEvent || !toEvent) {
      return false;
    }
    const fromResource = fromEvent.resource,
      toResource = toEvent.resource;
    // Verify these are real existing Resources and not collapsed away (resource not existing in resource store)
    if (!rowStore.isAvailable(fromResource) || !rowStore.isAvailable(toResource)) {
      return false;
    }
    return fromEvent.isModel && !fromResource.instanceMeta(rowStore).hidden && !toResource.instanceMeta(rowStore).hidden;
  }
  //endregion
  //region Highlight
  updateHighlightDependenciesOnEventHover(enable) {
    const me = this;
    if (enable) {
      const {
        client
      } = me;
      client.ion({
        name: 'highlightOnHover',
        [`${client.scheduledEventName}MouseEnter`]: params => me.highlightEventDependencies(params.eventRecord || params.taskRecord),
        [`${client.scheduledEventName}MouseLeave`]: params => me.unhighlightEventDependencies(params.eventRecord || params.taskRecord),
        thisObj: me
      });
    } else {
      me.detachListeners('highlightOnHover');
    }
  }
  highlight(dependency, cls = this.overCls) {
    let classes = this.highlighted.get(dependency);
    if (!classes) {
      this.highlighted.set(dependency, classes = new Set());
    }
    classes.add(cls);
    // Update element directly instead of refreshing and letting DomSync handle it,
    // to optimize highlight performance with many dependencies on screen
    for (const element of this.getElementsForDependency(dependency)) {
      element.classList.add(cls);
    }
  }
  unhighlight(dependency, cls = this.overCls) {
    const classes = this.highlighted.get(dependency);
    if (classes) {
      classes.delete(cls);
      if (!classes.size) {
        this.highlighted.delete(dependency);
      }
    }
    // Update element directly instead of refreshing and letting DomSync handle it,
    // to optimize highlight performance with many dependencies on screen
    for (const element of this.getElementsForDependency(dependency)) {
      element.classList.remove(cls);
    }
  }
  highlightEventDependencies(timespan, cls) {
    timespan.dependencies.forEach(dep => this.highlight(dep, cls));
  }
  unhighlightEventDependencies(timespan, cls) {
    timespan.dependencies.forEach(dep => this.unhighlight(dep, cls));
  }
  //endregion
  //region Drawing
  // Implemented in DependencyGridCache to return dependencies that might intersect the current viewport and thus
  // should be considered for drawing. Fallback value here is used when there is no grid cache (which happens when it
  // is reset. Also useful in case we want to have it configurable or opt out automatically for small datasets)
  getDependenciesToConsider(startMS, endMS, startIndex, endIndex) {
    var _super$getDependencie;
    // Get records from grid cache
    return ((_super$getDependencie = super.getDependenciesToConsider) === null || _super$getDependencie === void 0 ? void 0 : _super$getDependencie.call(this, startMS, endMS, startIndex, endIndex)) ??
    // Falling back to using all valid deps (fix for not trying to draw conflicted deps)
    this.project.dependencyStore.records.filter(d => d.isValid);
  }
  // String key used as syncId
  getDependencyKey(dependency, fromAssignment, toAssignment) {
    return `dep:${dependency.id};from:${fromAssignment.id};to:${toAssignment.id}`;
  }
  drawDependency(dependency, batch = false, forceBoxes = null) {
    var _fromAssigned, _toAssigned;
    const me = this,
      {
        domConfigs,
        client,
        rowStore,
        topIndex,
        bottomIndex
      } = me,
      {
        eventStore,
        useInitialAnimation
      } = client,
      {
        idMap
      } = rowStore,
      {
        startMS,
        endMS
      } = me.visibleDateRange,
      {
        fromEvent,
        toEvent
      } = dependency;
    let fromAssigned = fromEvent.assigned,
      toAssigned = toEvent.assigned;
    if (
    // No point in trying to draw dep between unscheduled/non-existing events
    fromEvent.isScheduled && toEvent.isScheduled &&
    // Or between filtered out events
    eventStore.includes(fromEvent) && eventStore.includes(toEvent) && // Or unassigned ones
    (_fromAssigned = fromAssigned) !== null && _fromAssigned !== void 0 && _fromAssigned.size && (_toAssigned = toAssigned) !== null && _toAssigned !== void 0 && _toAssigned.size) {
      // Add links, if used
      if (me.usingLinks) {
        fromAssigned = [...fromAssigned].flatMap(collectLinkedAssignments);
        toAssigned = [...toAssigned].flatMap(collectLinkedAssignments);
      }
      for (const from of fromAssigned) {
        for (const to of toAssigned) {
          var _idMap$from$resource$, _from$resource, _idMap$to$resource$id, _to$resource;
          const
            // Using direct lookup in idMap instead of indexOf() for performance.
            // Resource might be filtered out or not exist at all
            fromIndex = (_idMap$from$resource$ = idMap[(_from$resource = from.resource) === null || _from$resource === void 0 ? void 0 : _from$resource.id]) === null || _idMap$from$resource$ === void 0 ? void 0 : _idMap$from$resource$.index,
            toIndex = (_idMap$to$resource$id = idMap[(_to$resource = to.resource) === null || _to$resource === void 0 ? void 0 : _to$resource.id]) === null || _idMap$to$resource$id === void 0 ? void 0 : _idMap$to$resource$id.index,
            fromDateMS = Math.min(fromEvent.startDateMS, toEvent.startDateMS),
            toDateMS = Math.max(fromEvent.endDateMS, toEvent.endDateMS);
          // Draw only if dependency intersects view, unless it is part of an export
          if (client.isExporting || fromIndex != null && toIndex != null && from.drawDependencies !== false && to.drawDependencies !== false && rowStore.isAvailable(from.resource) && rowStore.isAvailable(to.resource) && !(
          // Both ends above view
          fromIndex < topIndex && toIndex < topIndex ||
          // Both ends below view
          fromIndex > bottomIndex && toIndex > bottomIndex ||
          // Both ends before view
          fromDateMS < startMS && toDateMS < startMS ||
          // Both ends after view
          fromDateMS > endMS && toDateMS > endMS)) {
            const key = me.getDependencyKey(dependency, from, to),
              lineDomConfigs = me.getDomConfigs(dependency, from, to, forceBoxes);
            if (lineDomConfigs) {
              // Allow deps to match animation delay of their events (the bottommost one) when fading in
              if (useInitialAnimation) {
                lineDomConfigs[0].style = {
                  animationDelay: `${Math.max(fromIndex, toIndex) / 20 * 1000}ms`
                };
              }
              domConfigs.set(key, lineDomConfigs);
            }
            // No room to draw a line
            else {
              domConfigs.delete(key);
            }
          }
          // Give mixins a shot at running code after a dependency is drawn. Used by grid cache to cache the
          // dependency (when needed)
          me.afterDrawDependency(dependency, fromIndex, toIndex, fromDateMS, toDateMS);
        }
      }
    }
    if (!batch) {
      me.domSync();
    }
  }
  // Hooks used by grid cache, to keep code in this file readable
  afterDrawDependency(dependency, fromIndex, toIndex, fromDateMS, toDateMS) {
    var _super$afterDrawDepen;
    (_super$afterDrawDepen = super.afterDrawDependency) === null || _super$afterDrawDepen === void 0 ? void 0 : _super$afterDrawDepen.call(this, dependency, fromIndex, toIndex, fromDateMS, toDateMS);
  }
  beforeDraw() {
    var _super$beforeDraw;
    (_super$beforeDraw = super.beforeDraw) === null || _super$beforeDraw === void 0 ? void 0 : _super$beforeDraw.call(this);
  }
  afterDraw() {
    var _super$afterDraw;
    (_super$afterDraw = super.afterDraw) === null || _super$afterDraw === void 0 ? void 0 : _super$afterDraw.call(this);
  }
  // Update DOM
  domSync(targetElement = this.client.svgCanvas) {
    DomSync.sync({
      targetElement,
      domConfig: {
        onlyChildren: true,
        children: Array.from(this.domConfigs.values()).flat()
      },
      syncIdField: 'syncId',
      releaseThreshold: 0,
      strict: true,
      callback() {}
    });
  }
  fillDrawingCache() {
    const me = this,
      {
        client
      } = me;
    // Cache subgrid bounds for the duration of this draw call to not have to figure it out per dep
    me.relativeTo = Rectangle.from(client.foregroundCanvas);
    // Cache other lookups too
    me.visibleResources = client.visibleResources;
    me.visibleDateRange = client.visibleDateRange;
    me.topIndex = me.rowStore.indexOf(me.visibleResources.first);
    me.bottomIndex = me.rowStore.indexOf(me.visibleResources.last);
    // Cache link lookup, to avoid semi-expensive flatMap calls in drawDependency
    if (me.usingLinks == null) {
      me.usingLinks = client.resourceStore.some(r => r.hasLinks);
    }
  }
  // Draw all dependencies intersecting the current viewport immediately
  draw() {
    const me = this,
      {
        client
      } = me,
      {
        visibleDateRange
      } = client;
    if (client.refreshSuspended || !client.foregroundCanvas || !visibleDateRange || !client.isEngineReady || me.disabled && !me._isDisabling || client.isExporting) {
      return;
    }
    me.fillDrawingCache();
    me.domConfigs.clear();
    // Nothing to draw if there are no rows or no ticks or we are disabled
    if (client.firstVisibleRow && client.lastVisibleRow && client.timeAxis.count && !me.disabled && visibleDateRange.endMS - visibleDateRange.startMS > 0) {
      const {
          topIndex,
          bottomIndex
        } = me,
        dependencies = me.getDependenciesToConsider(visibleDateRange.startMS, visibleDateRange.endMS, topIndex, bottomIndex);
      // Give mixins a shot at doing something before deps are drawn. Used by grid cache to determine if
      // the cache should be rebuilt
      me.beforeDraw();
      for (const dependency of dependencies) {
        me.drawDependency(dependency, true);
      }
      // Give mixins a shot at doing something after all deps are drawn
      me.afterDraw();
    }
    me.domSync();
    client.trigger('dependenciesDrawn');
  }
  //endregion
  //region Refreshing
  // Performs a draw on next frame, not intended to be called directly, call refresh() instead
  doRefresh() {
    var _client$features, _client$features2, _client$features3, _client$features4;
    const me = this,
      {
        client
      } = me,
      {
        scheduledEventName
      } = client;
    me.draw();
    // Refresh each frame during animations, during dragging & resizing  (if we have dependencies)
    me.drawingLive = client.dependencyStore.count && (client.isAnimating || client.useInitialAnimation && client.eventStore.count || ((_client$features = client.features[`${scheduledEventName}Drag`]) === null || _client$features === void 0 ? void 0 : _client$features.isActivelyDragging) || ((_client$features2 = client.features[`${scheduledEventName}Resize`]) === null || _client$features2 === void 0 ? void 0 : _client$features2.isResizing) || ((_client$features3 = client.features[`${scheduledEventName}SegmentDrag`]) === null || _client$features3 === void 0 ? void 0 : _client$features3.isActivelyDragging) || ((_client$features4 = client.features[`${scheduledEventName}SegmentResize`]) === null || _client$features4 === void 0 ? void 0 : _client$features4.isResizing));
    me.drawingLive && me.refresh(false);
  }
  /**
   * Redraws dependencies on the next animation frame
   */
  refresh(immediateRefresh = this.immediateRefresh) {
    const {
      client
    } = this;
    // Queue up a draw unless refresh is suspended.
    // immediateRefresh must be true to function because this method may be used as an event listener
    // so therefore may receive an event object as a sole parameter.
    if (!client.refreshSuspended && !this.disabled && client.isPainted && !client.timeAxisSubGrid.collapsed) {
      (immediateRefresh === true ? this.doRefresh.now : this.doRefresh).call(this);
    }
  }
  // Resets grid cache and performs a draw on next frame. Conditions when it should be called:
  // * Zooming
  // * Shifting time axis
  // * Resizing window
  // * CRUD
  // ...
  reset({
    source,
    type
  } = emptyObject) {
    var _super$reset;
    (_super$reset = super.reset) === null || _super$reset === void 0 ? void 0 : _super$reset.call(this);
    // Refresh immediately if the timeline viewport is changing size
    this.refresh(source === this.client && type === 'timelineviewportresize');
  }
  /**
   * Draws all dependencies for the specified task.
   * @deprecated 5.1 The Dependencies feature was refactored and this fn is no longer needed
   */
  drawForEvent() {
    VersionHelper.deprecate('Scheduler', '6.0.0', 'Dependencies.drawForEvent() is no longer needed');
    this.refresh();
  }
  //endregion
  //region Scheduler hooks
  render() {
    // Pull in the svg canvas early to have it available during drawing
    this.client.getConfig('svgCanvas');
  }
  //endregion
}

Dependencies._$name = 'Dependencies';
GridFeatureManager.registerFeature(Dependencies, false, ['Scheduler', 'ResourceHistogram']);

/**
 * @module Scheduler/feature/EventFilter
 */
/**
 * Adds event filter menu items to the timeline header context menu.
 *
 * {@inlineexample Scheduler/feature/EventFilter.js}
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *   features : {
 *     eventFilter : true // `true` by default, set to `false` to disable the feature and remove the menu item from the timeline header
 *   }
 * });
 * ```
 *
 * This feature is **enabled** by default
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype eventFilter
 * @feature
 */
class EventFilter extends InstancePlugin {
  static get $name() {
    return 'EventFilter';
  }
  static get pluginConfig() {
    return {
      chain: ['populateTimeAxisHeaderMenu']
    };
  }
  /**
   * Populates the header context menu items.
   * @param {Object} options Contains menu items and extra data retrieved from the menu target.
   * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
   * @internal
   */
  populateTimeAxisHeaderMenu({
    items
  }) {
    const me = this;
    items.eventsFilter = {
      text: 'L{filterEvents}',
      icon: 'b-fw-icon b-icon-filter',
      disabled: me.disabled,
      localeClass: me,
      weight: 100,
      menu: {
        type: 'popup',
        localeClass: me,
        items: {
          nameFilter: {
            weight: 110,
            type: 'textfield',
            cls: 'b-eventfilter b-last-row',
            clearable: true,
            keyStrokeChangeDelay: 300,
            label: 'L{byName}',
            localeClass: me,
            width: 200,
            internalListeners: {
              change: me.onEventFilterChange,
              thisObj: me
            }
          }
        },
        onBeforeShow({
          source: menu
        }) {
          const [filterByName] = menu.items,
            filter = me.store.filters.getBy('property', 'name');
          filterByName.value = (filter === null || filter === void 0 ? void 0 : filter.value) || '';
        }
      }
    };
  }
  onEventFilterChange({
    value
  }) {
    if (value !== '') {
      this.store.filter('name', value);
    } else {
      this.store.removeFilter('name');
    }
  }
  get store() {
    const {
      client
    } = this;
    return client.isGanttBase ? client.store : client.eventStore;
  }
}
EventFilter.featureClass = 'b-event-filter';
EventFilter._$name = 'EventFilter';
GridFeatureManager.registerFeature(EventFilter, true, ['Scheduler', 'Gantt']);
GridFeatureManager.registerFeature(EventFilter, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/mixin/NonWorkingTimeMixin
 */
/**
 * Mixin with functionality shared between {@link Scheduler/feature/NonWorkingTime} and
 * {@link Scheduler/feature/EventNonWorkingTime}.
 * @mixin
 */
var NonWorkingTimeMixin = (Target => class NonWorkingTimeMixin extends Target {
  static $name = 'NonWorkingTimeMixin';
  static configurable = {
    /**
     * The maximum time axis unit to display non-working ranges for ('hour' or 'day' etc).
     * When zooming to a view with a larger unit, no non-working time elements will be rendered.
     *
     * **Note:** Be careful with setting this config to big units like 'year'. When doing this,
     * make sure the timeline {@link Scheduler/view/TimelineBase#config-startDate start} and
     * {@link Scheduler/view/TimelineBase#config-endDate end} dates are set tightly.
     * When using a long range (for example many years) with non-working time elements rendered per hour,
     * you will end up with millions of elements, impacting performance.
     * When zooming, use the {@link Scheduler/view/mixin/TimelineZoomable#config-zoomKeepsOriginalTimespan} config.
     * @config {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'}
     * @default
     */
    maxTimeAxisUnit: 'week'
  };
  getNonWorkingTimeRanges(calendar, startDate, endDate) {
    if (!calendar.getNonWorkingTimeRanges) {
      const result = [];
      calendar.forEachAvailabilityInterval({
        startDate,
        endDate,
        isForward: true
      }, (intervalStartDate, intervalEndDate, calendarCacheInterval) => {
        for (const [entry, cache] of calendarCacheInterval.intervalGroups) {
          if (!cache.getIsWorking()) {
            result.push({
              name: entry.name,
              iconCls: entry.iconCls,
              cls: entry.cls,
              startDate: intervalStartDate,
              endDate: intervalEndDate
            });
          }
        }
      });
      return result;
    }
    return calendar.getNonWorkingTimeRanges(startDate, endDate);
  }
  getCalendarTimeRanges(calendar, ignoreName = false) {
    const me = this,
      {
        timeAxis,
        fillTicks
      } = me.client,
      {
        unit,
        increment
      } = timeAxis,
      shouldPaint = !me.maxTimeAxisUnit || DateHelper.compareUnits(unit, me.maxTimeAxisUnit) <= 0;
    if (calendar && shouldPaint && timeAxis.count) {
      const allRanges = me.getNonWorkingTimeRanges(calendar, timeAxis.startDate, timeAxis.endDate),
        timeSpans = allRanges.map(interval => new TimeSpan({
          name: interval.name,
          cls: `b-nonworkingtime ${interval.cls || ''}`,
          startDate: interval.startDate,
          endDate: interval.endDate
        })),
        mergedSpans = [];
      let prevRange = null;
      // intervals returned by the calendar are not merged, let's combine them to yield fewer elements
      for (const range of timeSpans) {
        if (prevRange && range.startDate <= prevRange.endDate && (ignoreName || range.name === prevRange.name) && range.duration > 0) {
          prevRange.endDate = range.endDate;
        } else {
          mergedSpans.push(range);
          range.setData('id', `nonworking-${mergedSpans.length}`);
          prevRange = range;
        }
      }
      // When filling ticks, non-working-time ranges are cropped to full ticks too
      if (fillTicks) {
        mergedSpans.forEach(span => {
          span.setStartEndDate(DateHelper.ceil(span.startDate, {
            magnitude: increment,
            unit
          }), DateHelper.floor(span.endDate, {
            magnitude: increment,
            unit
          }));
        });
      }
      return mergedSpans;
    } else {
      return [];
    }
  }
  //region Basic scheduler calendar
  setupDefaultCalendar() {
    const {
      client,
      project
    } = this;
    if (
    // Might have been set up by NonWorkingTime / EventNonWorkingTime already
    !this.autoGeneratedWeekends &&
    // For basic scheduler...
    !client.isSchedulerPro && !client.isGantt &&
    // ...that uses the default calendar...
    project.effectiveCalendar === project.defaultCalendar &&
    // ...and has no defined intervals
    !project.defaultCalendar.intervalStore.count) {
      this.autoGeneratedWeekends = true;
      this.updateDefaultCalendar();
    }
  }
  updateDefaultCalendar() {
    if (this.autoGeneratedWeekends) {
      const calendar = this.client.project.effectiveCalendar,
        intervals = this.defaultNonWorkingIntervals,
        hasIntervals = Boolean(intervals.length);
      // The default "weekends" calendar should not be time zone converted
      calendar.ignoreTimeZone = true;
      calendar.clearIntervals(hasIntervals);
      // Update weekends as non-working time
      if (hasIntervals) {
        calendar.addIntervals(intervals);
      }
    }
  }
  updateLocalization() {
    var _super$updateLocaliza;
    (_super$updateLocaliza = super.updateLocalization) === null || _super$updateLocaliza === void 0 ? void 0 : _super$updateLocaliza.call(this);
    this.autoGeneratedWeekends && this.updateDefaultCalendar();
  }
  get defaultNonWorkingIntervals() {
    const dayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return DateHelper.nonWorkingDaysAsArray.map(dayIndex => ({
      recurrentStartDate: `on ${dayNames[dayIndex]} at 0:00`,
      recurrentEndDate: `on ${dayNames[(dayIndex + 1) % 7]} at 0:00`,
      isWorking: false
    }));
  }
  //endregion
});

/**
 * @module Scheduler/feature/NonWorkingTime
 */
/**
 * Feature that allows styling of weekends (and other non-working time) by adding timeRanges for those days.
 *
 * {@inlineexample Scheduler/feature/NonWorkingTime.js}
 *
 * By default, the basic Scheduler's calendar is empty. When enabling this feature in the basic Scheduler, it injects
 * Saturday and Sunday weekend intervals if no intervals are encountered. For Scheduler Pro, it visualizes the project
 * calendar and does not automatically inject anything. You have to define a Calendar in the application and assign it
 * to the project, for more information on how to do that, please see Scheduler Pro's Scheduling/Calendars guide.
 *
 * Please note that to not clutter the view (and have a large negative effect on performance) the feature does not
 * render ranges shorter than the base unit used by the time axis. The behavior can be disabled with
 * {@link #config-hideRangesOnZooming} config.
 *
 * The feature also bails out of rendering ranges for very zoomed out views completely for the same reasons (see
 * {@link #config-maxTimeAxisUnit} for details).
 *
 * Also note that the feature uses virtualized rendering, only the currently visible non-working-time ranges are
 * available in the DOM.
 *
 * This feature is **off** by default for Scheduler, but **enabled** by default for Scheduler Pro.
 * For info on enabling it, see {@link Grid/view/mixin/GridFeatures}.
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @demo Scheduler/nonworkingdays
 * @classtype nonWorkingTime
 * @mixes Scheduler/feature/mixin/NonWorkingTimeMixin
 * @feature
 */
class NonWorkingTime extends AbstractTimeRanges.mixin(AttachToProjectMixin, NonWorkingTimeMixin) {
  //region Default config
  static $name = 'NonWorkingTime';
  /** @hideconfigs enableResizing, store*/
  static get defaultConfig() {
    return {
      /**
       * Set to `true` to highlight non-working periods of time
       * @config {Boolean}
       * @deprecated Since 5.2.0, will be removed since the feature is pointless if set to false
       */
      highlightWeekends: null,
      /**
       * The feature by default does not render ranges smaller than the base unit used by the time axis.
       * Set this config to `false` to disable this behavior.
       *
       * <div class="note">The {@link #config-maxTimeAxisUnit} config defines a zoom level at which to bail out of
       * rendering ranges completely.</div>
       * @config {Boolean}
       * @default
       */
      hideRangesOnZooming: true,
      showHeaderElements: true,
      showLabelInBody: true,
      autoGeneratedWeekends: false
    };
  }
  static pluginConfig = {
    chain: ['onPaint', 'attachToProject', 'updateLocalization', 'onConfigChange', 'onSchedulerHorizontalScroll']
  };
  //endregion
  //region Init & destroy
  doDestroy() {
    this.attachToCalendar(null);
    super.doDestroy();
  }
  set highlightWeekends(highlight) {
    VersionHelper.deprecate('Scheduler', '6.0.0', 'Deprecated in favour of disabling the feature');
    this.disabled = !highlight;
  }
  get highlightWeekends() {
    return !this.disabled;
  }
  onConfigChange({
    name
  }) {
    if (!this.isConfiguring && name === 'fillTicks') {
      this.refresh();
    }
  }
  //endregion
  //region Project
  attachToProject(project) {
    super.attachToProject(project);
    this.attachToCalendar(project.effectiveCalendar);
    // if there's no graph yet - need to delay this call until it appears, but not for scheduler
    if (!project.graph && !this.client.isScheduler) {
      project.ion({
        name: 'project',
        dataReady: {
          fn: () => this.attachToCalendar(project.effectiveCalendar),
          once: true
        },
        thisObj: this
      });
    }
    project.ion({
      name: 'project',
      calendarChange: () => this.attachToCalendar(project.effectiveCalendar),
      thisObj: this
    });
  }
  //endregion
  //region TimeAxisViewModel
  onTimeAxisViewModelUpdate(...args) {
    this._timeAxisUnitDurationMs = null;
    return super.onTimeAxisViewModelUpdate(...args);
  }
  //endregion
  //region Calendar
  attachToCalendar(calendar) {
    const me = this,
      {
        project,
        client
      } = me;
    me.detachListeners('calendar');
    me.autoGeneratedWeekends = false;
    if (calendar) {
      // Sets up a default weekend calendar for basic Scheduler, when no calendar is set
      me.setupDefaultCalendar();
      calendar.intervalStore.ion({
        name: 'calendar',
        change: () => me.setTimeout(() => me.refresh(), 1)
      });
    }
    // On changing calendar we react to a data level event which is triggered after project refresh.
    // Redraw right away
    if (client.isEngineReady && !client.project.isDelayingCalculation && !client.isDestroying) {
      me.refresh();
    }
    // Initially there is no guarantee we are ready to draw, wait for refresh
    else if (!project.isDestroyed) {
      me.detachListeners('initialProjectListener');
      project.ion({
        name: 'initialProjectListener',
        refresh({
          isCalculated
        }) {
          // Cant render early, have to wait for calculations
          if (isCalculated !== false) {
            me.refresh();
            me.detachListeners('initialProjectListener');
          }
        },
        thisObj: me
      });
    }
  }
  get calendar() {
    var _this$project;
    return (_this$project = this.project) === null || _this$project === void 0 ? void 0 : _this$project.effectiveCalendar;
  }
  //endregion
  //region Draw
  get timeAxisUnitDurationMs() {
    // calculate and cache duration of the timeAxis unit in milliseconds
    if (!this._timeAxisUnitDurationMs) {
      this._timeAxisUnitDurationMs = DateHelper.as('ms', 1, this.client.timeAxis.unit);
    }
    return this._timeAxisUnitDurationMs;
  }
  /**
   * Based on this method result the feature decides whether the provided non-working period should
   * be rendered or not.
   * The method checks that the range has non-zero {@link Scheduler.model.TimeSpan#field-duration},
   * lays in the visible timespan and its duration is longer or equal the base timeaxis unit
   * (if {@link #config-hideRangesOnZooming} is `true`).
   *
   * Override the method to implement your custom range rendering vetoing logic.
   * @param {Scheduler.model.TimeSpan} range Range to render.
   * @returns {Boolean} `true` if the range should be rendered and `false` otherwise.
   */
  shouldRenderRange(range) {
    // if the range is longer or equal than one timeAxis unit then render it
    return super.shouldRenderRange(range) && (!this.hideRangesOnZooming || range.durationMS >= this.timeAxisUnitDurationMs);
  }
  // Calendar intervals as TimeSpans, with adjacent intervals merged to create fewer
  get timeRanges() {
    const me = this;
    if (!me._timeRanges) {
      me._timeRanges = me.getCalendarTimeRanges(me.calendar);
    }
    return me._timeRanges;
  }
  //endregion
}

NonWorkingTime._$name = 'NonWorkingTime';
GridFeatureManager.registerFeature(NonWorkingTime, false, 'Scheduler');
GridFeatureManager.registerFeature(NonWorkingTime, true, ['SchedulerPro', 'Gantt', 'ResourceHistogram']);

/**
 * @module Scheduler/feature/ScheduleTooltip
 */
/**
 * Feature that displays a tooltip containing the time at the mouse position when hovering empty parts of the schedule.
 * To hide the schedule tooltip, just disable this feature:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleTooltip : false
 *     }
 * });
 * ```
 *
 * You can also output a message along with the default time indicator (to indicate resource availability etc)
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *    features : {
 *       scheduleTooltip : {
 *           getText(date, event, resource) {
 *               return 'Hovering ' + resource.name;
 *           }
 *       }
 *   }
 * });
 * ```
 *
 * To take full control over the markup shown in the tooltip you can override the {@link #function-generateTipContent} method:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleTooltip : {
 *             generateTipContent({ date, event, resourceRecord }) {
 *                 return `
 *                     <dl>
 *                         <dt>Date</dt><dd>${date}</dd>
 *                         <dt>Resource</dt><dd>${resourceRecord.name}</dd>
 *                     </dl>
 *                 `;
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Configuration properties from the feature are passed down into the resulting {@link Core.widget.Tooltip} instance.
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         scheduleTooltip : {
 *             // Don't show the tip until the mouse has been over the schedule for three seconds
 *             hoverDelay : 3000
 *         }
 *     }
 * });
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/ScheduleTooltip.js
 * @classtype scheduleTooltip
 * @feature
 */
class ScheduleTooltip extends InstancePlugin {
  //region Config
  static get $name() {
    return 'ScheduleTooltip';
  }
  static get configurable() {
    return {
      messageTemplate: data => `<div class="b-sch-hovertip-msg">${data.message}</div>`,
      /**
       * Set to `true` to hide this tooltip when hovering non-working time. Defaults to `false` for Scheduler,
       * `true` for SchedulerPro
       * @config {Boolean}
       */
      hideForNonWorkingTime: null
    };
  }
  // Plugin configuration. This plugin chains some of the functions in Grid.
  static get pluginConfig() {
    return {
      chain: ['onPaint']
    };
  }
  //endregion
  //region Init
  /**
   * Set up drag and drop and hover tooltip.
   * @private
   */
  onPaint({
    firstPaint
  }) {
    if (!firstPaint) {
      return;
    }
    const me = this,
      {
        client
      } = me;
    if (client.isSchedulerPro && me.hideForNonWorkingTime === undefined) {
      me.hideForNonWorkingTime = true;
    }
    let reshowListener;
    const tip = me.hoverTip = new Tooltip({
      id: `${client.id}-schedule-tip`,
      cls: 'b-sch-scheduletip',
      allowOver: true,
      hoverDelay: 0,
      hideDelay: 100,
      showOnHover: true,
      forElement: client.timeAxisSubGridElement,
      anchorToTarget: false,
      trackMouse: true,
      updateContentOnMouseMove: true,
      // disable text content and monitor resize for tooltip, otherwise it doesn't
      // get sized properly on first appearance
      monitorResize: false,
      textContent: false,
      forSelector: '.b-schedulerbase:not(.b-dragging-event):not(.b-dragcreating) .b-grid-body-container:not(.b-scrolling) .b-timeline-subgrid:not(.b-scrolling) > :not(.b-sch-foreground-canvas):not(.b-group-footer):not(.b-group-row) *',
      // Do not constrain at all, want it to be able to go outside of the viewport to not get in the way
      getHtml: me.getHoverTipHtml.bind(me),
      onDocumentMouseDown(event) {
        // Click on the scheduler hides until the very next
        // non-button-pressed mouse move!
        if (tip.forElement.contains(event.event.target)) {
          reshowListener = EventHelper.on({
            thisObj: me,
            element: client.timeAxisSubGridElement,
            mousemove: e => tip.internalOnPointerOver(e),
            capture: true
          });
        }
        const hideAnimation = tip.hideAnimation;
        tip.hideAnimation = false;
        tip.constructor.prototype.onDocumentMouseDown.call(tip, event);
        tip.hideAnimation = hideAnimation;
      },
      // on Core/mixin/Events constructor, me.config.listeners is deleted and attributed its value to me.configuredListeners
      // to then on processConfiguredListeners it set me.listeners to our TooltipBase
      // but since we need our initial config.listeners to set to our internal tooltip, we leave processConfiguredListeners empty
      // to avoid lost our listeners to apply for our internal tooltip here and force our feature has all Tooltip events firing
      ...me.config,
      internalListeners: me.configuredListeners
    });
    // We have to add our own listener after instantiation because it may conflict with a configured listener
    tip.ion({
      pointerover({
        event
      }) {
        const buttonsPressed = 'buttons' in event ? event.buttons > 0 : event.which > 0; // fallback for Safari which doesn't support 'buttons'
        // This is the non-button-pressed mousemove
        // after the document mousedown
        if (!buttonsPressed && reshowListener) {
          reshowListener();
        }
        // Never any tooltip while interaction is ongoing and a mouse button is pressed
        return !me.disabled && !buttonsPressed;
      },
      innerhtmlupdate({
        source
      }) {
        me.clockTemplate.updateDateIndicator(source.element, me.lastTime);
      }
    });
    // Update tooltip after zooming
    client.ion({
      timeAxisViewModelUpdate: 'updateTip',
      thisObj: me
    });
    me.clockTemplate = new ClockTemplate({
      scheduler: client
    });
  }
  // leave configuredListeners alone until render time at which they are used on the tooltip
  processConfiguredListeners() {}
  updateTip() {
    if (this.hoverTip.isVisible) {
      this.hoverTip.updateContent();
    }
  }
  doDestroy() {
    this.destroyProperties('clockTemplate', 'hoverTip');
    super.doDestroy();
  }
  //endregion
  //region Contents
  /**
   * @deprecated Use {@link #function-generateTipContent} instead.
   * Gets html to display in hover tooltip (tooltip displayed on empty parts of scheduler)
   * @private
   */
  getHoverTipHtml({
    tip,
    event
  }) {
    const me = this,
      scheduler = me.client,
      date = event && scheduler.getDateFromDomEvent(event, 'floor', true);
    let html = me.lastHtml;
    // event.target might be null in the case of being hosted in a web component https://github.com/bryntum/bryntum-suite/pull/4488
    if (date && event.target) {
      const resourceRecord = scheduler.resolveResourceRecord(event);
      // resourceRecord might be null if user hover over the tooltip, but we shouldn't hide the tooltip in this case
      if (resourceRecord && (date - me.lastTime !== 0 || resourceRecord.id !== me.lastResourceId)) {
        if (me.hideForNonWorkingTime) {
          const isWorkingTime = resourceRecord.isWorkingTime(date);
          tip.element.classList.toggle('b-nonworking-time', !isWorkingTime);
        }
        me.lastResourceId = resourceRecord.id;
        html = me.lastHtml = me.generateTipContent({
          date,
          event,
          resourceRecord
        });
      }
    } else {
      tip.hide();
      me.lastTime = null;
      me.lastResourceId = null;
    }
    return html;
  }
  /**
   * Called as mouse pointer is moved over a new resource or time block. You can override this to show
   * custom HTML in the tooltip.
   * @param {Object} context
   * @param {Date} context.date The date of the hovered point
   * @param {Event} context.event The DOM event that triggered this tooltip to show
   * @param {Scheduler.model.ResourceModel} context.resourceRecord The resource record
   * @returns {String} The HTML contents to show in the tooltip (an empty return value will hide the tooltip)
   */
  generateTipContent({
    date,
    event,
    resourceRecord
  }) {
    const me = this,
      clockHtml = me.clockTemplate.generateContent({
        date,
        text: me.client.getFormattedDate(date)
      }),
      messageHtml = me.messageTemplate({
        message: me.getText(date, event, resourceRecord) || ''
      });
    me.lastTime = date;
    return clockHtml + messageHtml;
  }
  /**
   * Override this to render custom text to default hover tip
   * @param {Date} date
   * @param {Event} event Browser event
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @returns {String}
   */
  getText(date, event, resourceRecord) {}
  //endregion
}

ScheduleTooltip.featureClass = 'b-scheduletip';
ScheduleTooltip._$name = 'ScheduleTooltip';
GridFeatureManager.registerFeature(ScheduleTooltip, true, 'Scheduler');
GridFeatureManager.registerFeature(ScheduleTooltip, false, 'ResourceUtilization');

/**
 * @module Scheduler/feature/TimeAxisHeaderMenu
 */
const setTimeSpanOptions = {
  maintainVisibleStart: true
};
/**
 * Adds scheduler specific menu items to the timeline header context menu.
 *
 * ## Default timeaxis header menu items
 *
 * Here is the list of menu items provided by this and other features:
 *
 * | Reference          | Text                  | Weight | Feature                                           | Description                  |
 * |--------------------|-----------------------|--------|---------------------------------------------------|------------------------------|
 * | `eventsFilter`     | Filter tasks          | 100    | {@link Scheduler.feature.EventFilter EventFilter} | Submenu for event filtering  |
 * | \>`nameFilter`     | By name               | 110    | {@link Scheduler.feature.EventFilter EventFilter} | Filter by `name`             |
 * | `zoomLevel`        | Zoom                  | 200    | *This feature*                                    | Submenu for timeline zooming |
 * | \>`zoomSlider`     | -                     | 210    | *This feature*                                    | Changes current zoom level   |
 * | `dateRange`        | Date range            | 300    | *This feature*                                    | Submenu for timeline range   |
 * | \>`startDateField` | Start date            | 310    | *This feature*                                    | Start date for the timeline  |
 * | \>`endDateField`   | End date              | 320    | *This feature*                                    | End date for the timeline    |
 * | \>`leftShiftBtn`   | <                     | 330    | *This feature*                                    | Shift backward               |
 * | \>`todayBtn`       | Today                 | 340    | *This feature*                                    | Go to today                  |
 * | \>`rightShiftBtn`  | \>                    | 350    | *This feature*                                    | Shift forward                |
 * | `currentTimeLine`  | Show current timeline | 400    | {@link Scheduler.feature.TimeRanges TimeRanges}   | Show current time line       |
 *
 * \> - first level of submenu
 *
 * ## Customizing the menu items
 *
 * The menu items in the TimeAxis Header menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * ### Add extra items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         timeAxisHeaderMenu : {
 *             items : {
 *                 extraItem : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem() {
 *                         ...
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ### Remove existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         timeAxisHeaderMenu : {
 *             items : {
 *                 zoomLevel : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ### Customize existing item:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         timeAxisHeaderMenu : {
 *             items : {
 *                 zoomLevel : {
 *                     text : 'Scale'
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ### Customizing submenu items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *      features : {
 *          timeAxisHeaderMenu : {
 *              items : {
 *                  dateRange : {
 *                      menu : {
 *                          items : {
 *                              todayBtn : {
 *                                  text : 'Now'
 *                              }
 *                          }
 *                      }
 *                  }
 *              }
 *          }
 *      }
 * });
 * ```
 *
 * ### Manipulate existing items:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         timeAxisHeaderMenu : {
 *             // Process items before menu is shown
 *             processItems({ items }) {
 *                  // Add an extra item dynamically
 *                 items.coolItem = {
 *                     text : 'Cool action',
 *                     onItem() {
 *                           // ...
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Full information of the menu customization can be found in the ["Customizing the Event menu, the Schedule menu, and the TimeAxisHeader menu"](#Scheduler/guides/customization/contextmenu.md)
 * guide.
 *
 * This feature is **enabled** by default
 *
 * @extends Grid/feature/HeaderMenu
 * @demo Scheduler/basic
 * @classtype timeAxisHeaderMenu
 * @feature
 * @inlineexample Scheduler/feature/TimeAxisHeaderMenu.js
 */
class TimeAxisHeaderMenu extends HeaderMenu {
  //region Config
  static get $name() {
    return 'TimeAxisHeaderMenu';
  }
  static get defaultConfig() {
    return {
      /**
       * A function called before displaying the menu that allows manipulations of its items.
       * Returning `false` from this function prevents the menu being shown.
       *
       * ```javascript
       *   features         : {
       *       timeAxisHeaderMenu : {
       *           processItems({ items }) {
       *               // Add or hide existing items here as needed
       *               items.myAction = {
       *                   text   : 'Cool action',
       *                   icon   : 'b-fa b-fa-fw b-fa-ban',
       *                   onItem : () => console.log('Some coolness'),
       *                   weight : 300 // Move to end
       *               };
       *
       *               // Hide zoom slider
       *               items.zoomLevel.hidden = true;
       *           }
       *       }
       *   },
       * ```
       *
       * @param {Object} context An object with information about the menu being shown
       * @param {Object<String,MenuItemConfig>} context.items An object containing the {@link Core.widget.MenuItem menu item} configs keyed by their id
       * @param {Event} context.event The DOM event object that triggered the show
       * @config {Function}
       * @preventable
       */
      processItems: null,
      /**
       * This is a preconfigured set of items used to create the default context menu.
       *
       * The `items` provided by this feature are listed in the intro section of this class. You can
       * configure existing items by passing a configuration object to the keyed items.
       *
       * To remove existing items, set corresponding keys `null`:
       *
       * ```javascript
       * const scheduler = new Scheduler({
       *     features : {
       *         timeAxisHeaderMenu : {
       *             items : {
       *                 eventsFilter : null
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
      items: null,
      type: 'timeAxisHeader'
    };
  }
  static get pluginConfig() {
    const config = super.pluginConfig;
    config.chain.push('populateTimeAxisHeaderMenu');
    return config;
  }
  //endregion
  //region Events
  /**
   * This event fires on the owning Scheduler before the context menu is shown for the time axis header.
   * Allows manipulation of the items to show in the same way as in the {@link #config-processItems}.
   *
   * Returning `false` from a listener prevents the menu from being shown.
   *
   * @event timeAxisHeaderMenuBeforeShow
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source The scheduler
   * @param {Core.widget.Menu} menu The menu
   * @param {Object<String,MenuItemConfig>} items Menu item configs
   * @param {Grid.column.Column} column Time axis column
   */
  /**
   * This event fires on the owning Scheduler after the context menu is shown for a header
   * @event timeAxisHeaderMenuShow
   * @on-owner
   * @param {Scheduler.view.Scheduler} source The scheduler
   * @param {Core.widget.Menu} menu The menu
   * @param {Object<String,MenuItemConfig>} items Menu item configs
   * @param {Grid.column.Column} column Time axis column
   */
  /**
   * This event fires on the owning Scheduler when an item is selected in the header context menu.
   * @event timeAxisHeaderMenuItem
   * @on-owner
   * @param {Scheduler.view.Scheduler} source The scheduler
   * @param {Core.widget.Menu} menu The menu
   * @param {Core.widget.MenuItem} item Selected menu item
   * @param {Grid.column.Column} column Time axis column
   */
  //endregion
  construct() {
    super.construct(...arguments);
    if (this.triggerEvent.includes('click') && this.client.zoomOnTimeAxisDoubleClick) {
      this.client.zoomOnTimeAxisDoubleClick = false;
    }
  }
  shouldShowMenu(eventParams) {
    const {
        column,
        targetElement
      } = eventParams,
      {
        client
      } = this;
    if (client.isHorizontal) {
      return (column === null || column === void 0 ? void 0 : column.enableHeaderContextMenu) !== false && (column === null || column === void 0 ? void 0 : column.isTimeAxisColumn);
    }
    return targetElement.matches('.b-sch-header-timeaxis-cell');
  }
  showContextMenu(eventParams) {
    super.showContextMenu(...arguments);
    if (this.menu) {
      // the TimeAxis's context menu probably will cause scrolls because it manipulates the dates.
      // The menu should not hide on scroll when for a TimeAxisColumn
      this.menu.scrollAction = 'realign';
    }
  }
  populateTimeAxisHeaderMenu({
    items
  }) {
    const me = this,
      {
        client
      } = me,
      dateStep = {
        magnitude: client.timeAxis.shiftIncrement,
        unit: client.timeAxis.shiftUnit
      };
    Object.assign(items, {
      zoomLevel: {
        text: 'L{pickZoomLevel}',
        localeClass: me,
        icon: 'b-fw-icon b-icon-search-plus',
        disabled: !client.presets.count || me.disabled,
        weight: 200,
        menu: {
          type: 'popup',
          items: {
            zoomSlider: {
              weight: 210,
              type: 'slider',
              minWidth: 130,
              showValue: false,
              // so that we can use the change event which is easier to inject in tests
              triggerChangeOnInput: true
            }
          },
          onBeforeShow({
            source: menu
          }) {
            const [zoom] = menu.items;
            zoom.min = client.minZoomLevel;
            zoom.max = client.maxZoomLevel;
            zoom.value = client.zoomLevel;
            // Default slider value is 50 which causes the above to trigger onZoomSliderChange (when
            // maxZoomLevel < 50) if we add our listener prior to this point.
            me.zoomDetatcher = zoom.ion({
              change: 'onZoomSliderChange',
              thisObj: me
            });
          },
          onHide() {
            if (me.zoomDetatcher) {
              me.zoomDetatcher();
              me.zoomDetatcher = null;
            }
          }
        }
      },
      dateRange: {
        text: 'L{activeDateRange}',
        localeClass: me,
        icon: 'b-fw-icon b-icon-calendar',
        weight: 300,
        menu: {
          type: 'popup',
          cls: 'b-sch-timeaxis-menu-daterange-popup',
          defaults: {
            localeClass: me
          },
          items: {
            startDateField: {
              type: 'datefield',
              label: 'L{startText}',
              weight: 310,
              labelWidth: '6em',
              required: true,
              step: dateStep,
              internalListeners: {
                change: me.onRangeDateFieldChange,
                thisObj: me
              }
            },
            endDateField: {
              type: 'datefield',
              label: 'L{endText}',
              weight: 320,
              labelWidth: '6em',
              required: true,
              step: dateStep,
              internalListeners: {
                change: me.onRangeDateFieldChange,
                thisObj: me
              }
            },
            leftShiftBtn: {
              type: 'button',
              weight: 330,
              cls: 'b-left-nav-btn',
              icon: 'b-icon b-icon-previous',
              color: 'b-blue b-raised',
              flex: 1,
              margin: 0,
              internalListeners: {
                click: me.onLeftShiftBtnClick,
                thisObj: me
              }
            },
            todayBtn: {
              type: 'button',
              weight: 340,
              cls: 'b-today-nav-btn',
              color: 'b-blue b-raised',
              text: 'L{todayText}',
              flex: 4,
              margin: '0 8',
              internalListeners: {
                click: me.onTodayBtnClick,
                thisObj: me
              }
            },
            rightShiftBtn: {
              type: 'button',
              weight: 350,
              cls: 'b-right-nav-btn',
              icon: 'b-icon b-icon-next',
              color: 'b-blue b-raised',
              flex: 1,
              internalListeners: {
                click: me.onRightShiftBtnClick,
                thisObj: me
              }
            }
          },
          internalListeners: {
            paint: me.initDateRangeFields,
            thisObj: me
          }
        }
      }
    });
  }
  onZoomSliderChange({
    value
  }) {
    const me = this;
    // Zooming maintains timeline center point by scrolling the newly rerendered timeline to the
    // correct point to maintain the visual center. Temporarily inhibit context menu hide on scroll
    // of its context element.
    me.menu.scrollAction = 'realign';
    me.client.zoomLevel = value;
    me.menu.setTimeout({
      fn: () => me.menu.scrollAction = 'hide',
      delay: 100,
      cancelOutstanding: true
    });
  }
  initDateRangeFields({
    source: dateRange,
    firstPaint
  }) {
    if (firstPaint) {
      const {
        widgetMap
      } = dateRange;
      this.startDateField = widgetMap.startDateField;
      this.endDateField = widgetMap.endDateField;
    }
    this.initDates();
  }
  initDates() {
    const me = this;
    me.startDateField.suspendEvents();
    me.endDateField.suspendEvents();
    // The actual scheduler start dates may include time, but our Date field cannot currently handle
    // a time portion and throws it away, so when we need the value from an unchanged field, we need
    // to use the initialValue set from the timeAxis values.
    // Until our DateField can optionally include a time value, this is the solution.
    me.startDateField.value = me.startDateFieldInitialValue = me.client.startDate;
    me.endDateField.value = me.endDateFieldInitialValue = me.client.endDate;
    me.startDateField.resumeEvents();
    me.endDateField.resumeEvents();
  }
  onRangeDateFieldChange({
    source
  }) {
    const me = this,
      startDateChanged = source === me.startDateField,
      {
        client
      } = me,
      {
        timeAxis
      } = client,
      startDate = me.startDateFieldInitialValue && !startDateChanged ? me.startDateFieldInitialValue : me.startDateField.value;
    let endDate = me.endDateFieldInitialValue && startDateChanged ? me.endDateFieldInitialValue : me.endDateField.value;
    // When either of the fields is changed, we no longer use its initialValue from the timeAxis start or end
    // so that gets nulled to indicate that it's unavailable and the real field value is to be used.
    if (startDateChanged) {
      me.startDateFieldInitialValue = null;
    } else {
      me.endDateFieldInitialValue = null;
    }
    // Because the start and end dates are exclusive, avoid a zero
    // length time axis by incrementing the end by one tick unit
    // if they are the same.
    if (!(endDate - startDate)) {
      endDate = DateHelper.add(endDate, timeAxis.shiftIncrement, timeAxis.shiftUnit);
    }
    // if start date got bigger than end date set end date to start date plus one tick
    else if (endDate < startDate) {
      endDate = DateHelper.add(startDate, timeAxis.shiftIncrement, timeAxis.shiftUnit);
    }
    // setTimeSpan will try to keep the scroll position the same.
    client.setTimeSpan(startDate, endDate, setTimeSpanOptions);
    me.initDates();
  }
  onLeftShiftBtnClick() {
    this.client.timeAxis.shiftPrevious();
    this.initDates();
  }
  onTodayBtnClick() {
    const today = DateHelper.clearTime(new Date());
    this.client.setTimeSpan(today, DateHelper.add(today, 1, 'day'));
    this.initDates();
  }
  onRightShiftBtnClick() {
    this.client.timeAxis.shiftNext();
    this.initDates();
  }
}
TimeAxisHeaderMenu._$name = 'TimeAxisHeaderMenu';
GridFeatureManager.registerFeature(TimeAxisHeaderMenu, true, ['Scheduler', 'TimelineHistogram', 'Gantt']);

export { AbstractTimeRanges, Base, CalendarCacheIntervalMultiple, CalendarCacheMultiple, ColumnLines, Dependencies, DependencyCreation, DependencyTooltip, DragBase, DragCreateBase, DurationColumn, EventFilter, EventResize, NonWorkingTime, NonWorkingTimeMixin, RectangularPathFinder, ScheduleTooltip, TaskEditTransactional, TimeAxisHeaderMenu, TooltipBase, TransactionalFeature, combineCalendars };
//# sourceMappingURL=TimeAxisHeaderMenu.js.map
