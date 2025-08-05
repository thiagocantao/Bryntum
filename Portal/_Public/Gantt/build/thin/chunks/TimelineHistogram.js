/*!
 *
 * Bryntum Gantt 5.5.0
 *
 * Copyright(c) 2023 Bryntum AB
 * https://bryntum.com/contact
 * https://bryntum.com/license
 *
 */
import { ObjectHelper, InstancePlugin, EventHelper, StringHelper, Popup, Rectangle, Duration, Delayable, DomHelper, DateHelper, DomSync, parseAlign, Base, ArrayHelper, Tooltip, Objects } from './Editor.js';
import { AttachToProjectMixin, TimelineBase } from './ScheduleMenu.js';
import { ColumnStore, Column, GridFeatureManager, CopyPasteBase } from './GridBase.js';
import './MessageDialog.js';
import { DependencyModel, TimeSpan } from './CrudManagerView.js';
import { DragBase, DragCreateBase, TooltipBase, AbstractTimeRanges } from './TimeAxisHeaderMenu.js';
import './Tree.js';
import { Scale, Histogram } from './Scale.js';

/**
 * @module Scheduler/column/ScaleColumn
 */
/**
 * An object representing a point on the scale displayed by {@link Scheduler.column.ScaleColumn}.
 *
 * @typedef {Object} ScalePoint
 * @property {Number} value Point value
 * @property {String} unit Point value unit
 * @property {String} text Point text label
 */
/**
 * A specialized column showing a graduated scale from a defined array of values
 * and labels. This column is used in the {@link Scheduler.view.TimelineHistogram} and is not editable. Normally
 * you should not need to interact with this class directly.
 *
 * @extends Grid/column/Column
 * @classType scale
 * @column
 */
class ScaleColumn extends Column {
  //region Config
  static $name = 'ScaleColumn';
  static type = 'scale';
  static isScaleColumn = true;
  static get fields() {
    return ['scalePoints'];
  }
  static get defaults() {
    return {
      text: '\xa0',
      width: 40,
      minWidth: 40,
      field: 'scalePoints',
      cellCls: 'b-scale-cell',
      editor: false,
      sortable: false,
      groupable: false,
      filterable: false,
      alwaysClearCell: false,
      scalePoints: null
    };
  }
  //endregion
  //region Constructor/Destructor
  onDestroy() {
    this.scaleWidget.destroy();
  }
  //endregion
  //region Internal
  set width(width) {
    super.width = width;
    this.scaleWidget.width = width;
  }
  get width() {
    return super.width;
  }
  applyValue(useProp, key, value) {
    // pass value to scaleWidget
    if (key === 'scalePoints') {
      this.scaleWidget[key] = value;
    }
    return super.applyValue(...arguments);
  }
  buildScaleWidget() {
    const me = this;
    const scaleWidget = new Scale({
      owner: me.grid,
      appendTo: me.grid.floatRoot,
      cls: 'b-hide-offscreen',
      align: 'right',
      scalePoints: me.scalePoints,
      monitorResize: false
    });
    Object.defineProperties(scaleWidget, {
      width: {
        get() {
          return me.width;
        },
        set(width) {
          this.element.style.width = `${width}px`;
          this._width = me.width;
        }
      },
      height: {
        get() {
          return this._height;
        },
        set(height) {
          this.element.style.height = `${height}px`;
          this._height = height;
        }
      }
    });
    scaleWidget.width = me.width;
    return scaleWidget;
  }
  get scaleWidget() {
    const me = this;
    if (!me._scaleWidget) {
      me._scaleWidget = me.buildScaleWidget();
    }
    return me._scaleWidget;
  }
  //endregion
  //region Render
  renderer({
    cellElement,
    value,
    scaleWidgetConfig,
    scaleWidget = this.scaleWidget
  }) {
    ObjectHelper.assign(scaleWidget, {
      scalePoints: value || this.scalePoints,
      height: this.grid.rowHeight
    }, scaleWidgetConfig);
    scaleWidget.refresh();
    // Clone the scale widget element since every row is supposed to have
    // the same scale settings
    const scaleCloneElement = scaleWidget.element.cloneNode(true);
    scaleCloneElement.removeAttribute('id');
    scaleCloneElement.classList.remove('b-hide-offscreen');
    cellElement.innerHTML = '';
    cellElement.appendChild(scaleCloneElement);
  }
  //endregion
}

ColumnStore.registerColumnType(ScaleColumn);
ScaleColumn._$name = 'ScaleColumn';

/**
 * @module Scheduler/feature/base/ResourceTimeRangesBase
 */
/**
 * Abstract base class for ResourceTimeRanges and ResourceNonWorkingTime features.
 * You should not use this class directly.
 *
 * @extends Core/mixin/InstancePlugin
 * @abstract
 */
class ResourceTimeRangesBase extends InstancePlugin.mixin(AttachToProjectMixin) {
  //region Config
  static configurable = {
    /**
     * Specify value to use for the tabIndex attribute of range elements
     * @config {Number}
     * @category Misc
     */
    tabIndex: null,
    entityName: 'resourceTimeRange'
  };
  static get pluginConfig() {
    return {
      chain: ['getEventsToRender', 'onEventDataGenerated', 'noFeatureElementsInAxis'],
      override: ['matchScheduleCell', 'resolveResourceRecord']
    };
  }
  // Let Scheduler know if we have ResourceTimeRanges in view or not
  noFeatureElementsInAxis() {
    const {
      timeAxis
    } = this.client;
    return !this.needsRefresh && this.store && !this.store.storage.values.some(t => timeAxis.isTimeSpanInAxis(t));
  }
  //endregion
  //region Init
  doDisable(disable) {
    if (this.client.isPainted) {
      this.client.refresh();
    }
    super.doDisable(disable);
  }
  updateTabIndex() {
    if (!this.isConfiguring) {
      this.client.refresh();
    }
  }
  //endregion
  getEventsToRender(resource, events) {
    throw new Error('Implement in subclass');
  }
  // Called for each event during render, allows manipulation of render data. Adjust any resource time ranges
  // (chained function from Scheduler)
  onEventDataGenerated(renderData) {
    const me = this,
      {
        eventRecord,
        iconCls
      } = renderData;
    if (me.shouldInclude(eventRecord)) {
      if (me.client.isVertical) {
        renderData.width = renderData.resourceRecord.columnWidth || me.client.resourceColumnWidth;
      } else {
        renderData.top = 0;
      }
      // Flag that we should fill entire row/col
      renderData.fillSize = true;
      // Add our own cls
      renderData.wrapperCls['b-sch-resourcetimerange'] = 1;
      if (me.rangeCls) {
        renderData.wrapperCls[me.rangeCls] = 1;
      }
      renderData.wrapperCls[`b-sch-color-${eventRecord.timeRangeColor}`] = eventRecord.timeRangeColor;
      // Add label
      renderData.eventContent.text = eventRecord.name;
      renderData.children.push(renderData.eventContent);
      // Allow configuring tabIndex
      renderData.tabIndex = me.tabIndex != null ? String(me.tabIndex) : null;
      // Add icon
      if ((iconCls === null || iconCls === void 0 ? void 0 : iconCls.length) > 0) {
        renderData.children.unshift({
          tag: 'i',
          className: iconCls.toString()
        });
      }
      // Event data for DOMSync comparison
      renderData.eventId = me.generateElementId(eventRecord);
    }
  }
  /**
   * Generates ID from the passed time range record
   * @param {Scheduler.model.TimeSpan} record
   * @returns {String} Generated ID for the DOM element
   * @internal
   */
  generateElementId(record) {
    return record.domId;
  }
  resolveResourceTimeRangeRecord(rangeElement) {
    var _rangeElement$closest;
    return rangeElement === null || rangeElement === void 0 ? void 0 : (_rangeElement$closest = rangeElement.closest(`.${this.rangeCls}`)) === null || _rangeElement$closest === void 0 ? void 0 : _rangeElement$closest.elementData.eventRecord;
  }
  getElementFromResourceTimeRangeRecord(record) {
    // return this.client.foregroundCanvas.querySelector(`[data-event-id="${record.domId}"]`);
    return this.client.foregroundCanvas.syncIdMap[record.domId];
  }
  resolveResourceRecord(event) {
    var _this$resolveResource;
    const record = this.overridden.resolveResourceRecord(...arguments);
    return record || ((_this$resolveResource = this.resolveResourceTimeRangeRecord(event.target || event)) === null || _this$resolveResource === void 0 ? void 0 : _this$resolveResource.resource);
  }
  shouldInclude(eventRecord) {
    throw new Error('Implement in subclass');
  }
  // Called when a ResourceTimeRangeModel is manipulated, relays to Scheduler#onInternalEventStoreChange which updates to UI
  onStoreChange(event) {
    // Edge case for scheduler not using any events, it has to refresh anyway to get rid of ResourceTimeRanges
    if (event.action === 'removeall' || event.action === 'dataset') {
      this.needsRefresh = true;
    }
    this.client.onInternalEventStoreChange(event);
    this.needsRefresh = false;
  }
  // Override to let scheduler find the time cell from a resource time range element
  matchScheduleCell(target) {
    let cell = this.overridden.matchScheduleCell(target);
    if (!cell && this.enableMouseEvents) {
      const {
          client
        } = this,
        rangeElement = target.closest(`.${this.rangeCls}`);
      cell = rangeElement && client.getCell({
        record: client.isHorizontal ? rangeElement.elementData.resource : client.store.first,
        column: client.timeAxisColumn
      });
    }
    return cell;
  }
  handleRangeMouseEvent(domEvent) {
    const me = this,
      rangeElement = domEvent.target.closest(`.${me.rangeCls}`);
    if (rangeElement) {
      const eventName = EventHelper.eventNameMap[domEvent.type] ?? StringHelper.capitalize(domEvent.type),
        resourceTimeRangeRecord = me.resolveResourceTimeRangeRecord(rangeElement);
      me.client.trigger(me.entityName + eventName, {
        feature: me,
        [`${me.entityName}Record`]: resourceTimeRangeRecord,
        resourceRecord: me.client.resourceStore.getById(resourceTimeRangeRecord.resourceId),
        domEvent
      });
    }
  }
  updateEnableMouseEvents(enable) {
    var _me$mouseEventsDetach;
    const me = this,
      {
        client
      } = me;
    (_me$mouseEventsDetach = me.mouseEventsDetacher) === null || _me$mouseEventsDetach === void 0 ? void 0 : _me$mouseEventsDetach.call(me);
    me.mouseEventsDetacher = null;
    if (enable) {
      function attachMouseEvents() {
        me.mouseEventsDetacher = EventHelper.on({
          element: client.foregroundCanvas,
          delegate: `.${me.rangeCls}`,
          mousedown: 'handleRangeMouseEvent',
          mouseup: 'handleRangeMouseEvent',
          click: 'handleRangeMouseEvent',
          dblclick: 'handleRangeMouseEvent',
          contextmenu: 'handleRangeMouseEvent',
          mouseover: 'handleRangeMouseEvent',
          mouseout: 'handleRangeMouseEvent',
          thisObj: me
        });
      }
      client.whenVisible(attachMouseEvents);
    }
    client.element.classList.toggle('b-interactive-resourcetimeranges', Boolean(enable));
  }
}
// No feature based styling needed, do not add a cls to Scheduler
ResourceTimeRangesBase.featureClass = '';
ResourceTimeRangesBase._$name = 'ResourceTimeRangesBase';

/**
 * @module Scheduler/view/DependencyEditor
 */
/**
 * A dependency editor popup.
 *
 * @extends Core/widget/Popup
 * @private
 */
class DependencyEditor extends Popup {
  static get $name() {
    return 'DependencyEditor';
  }
  static get defaultConfig() {
    return {
      items: [],
      draggable: {
        handleSelector: ':not(button,.b-field-inner)' // blacklist buttons and field inners
      },

      axisLock: 'flexible'
    };
  }
  processWidgetConfig(widget) {
    const {
      dependencyEditFeature
    } = this;
    if (widget.ref === 'lagField' && !dependencyEditFeature.showLagField) {
      return false;
    }
    if (widget.ref === 'deleteButton' && !dependencyEditFeature.showDeleteButton) {
      return false;
    }
    return super.processWidgetConfig(widget);
  }
  afterShow(...args) {
    const {
      deleteButton
    } = this.widgetMap;
    // Only show delete button if the dependency record belongs to a store
    if (deleteButton) {
      deleteButton.hidden = !this.record.isPartOfStore();
    }
    super.afterShow(...args);
  }
  onInternalKeyDown(event) {
    this.trigger('keyDown', {
      event
    });
    super.onInternalKeyDown(event);
  }
}
DependencyEditor._$name = 'DependencyEditor';

/**
 * @module Scheduler/feature/DependencyEdit
 */
/**
 * Feature that displays a popup containing fields for editing a dependency. Requires the
 * {@link Scheduler.feature.Dependencies} feature to be enabled. Double click a line in the demo below to show the
 * editor.
 *
 * {@inlineexample Scheduler/feature/Dependencies.js}
 *
 * ## Customizing the built-in widgets
 *
 * ```javascript
 *  const scheduler = new Scheduler({
 *      columns : [
 *          { field : 'name', text : 'Name', width : 100 }
 *      ],
 *      features : {
 *          dependencies   : true,
 *          dependencyEdit : {
 *              editorConfig : {
 *                  items : {
 *                      // Custom label for the type field
 *                      typeField : {
 *                          label : 'Kind'
 *                      }
 *                  },
 *
 *                  bbar : {
 *                      items : {
 *                          // Hiding save button
 *                          saveButton : {
 *                              hidden : true
 *                          }
 *                      }
 *                  }
 *              }
 *          }
 *      }
 *  });
 * ```
 *
 * ## Built in widgets
 *
 * | Widget ref             | Type                              | Weight | Description               |
 * |------------------------|-----------------------------------|--------|---------------------------|
 * | `fromNameField`        | {@link Core.widget.DisplayField}  | 100    | From task name (readonly) |
 * | `toNameField`          | {@link Core.widget.DisplayField}  | 200    | To task name (readonly)   |
 * | `typeField`            | {@link Core.widget.Combo}         | 300    | Edit type                 |
 * | `lagField`             | {@link Core.widget.DurationField} | 400    | Edit lag                  |
 *
 * The built in buttons are:
 *
 * | Widget ref             | Type                       | Weight | Description                       |
 * |------------------------|----------------------------|--------|-----------------------------------|
 * | `saveButton`           | {@link Core.widget.Button} | 100    | Save button on the bbar           |
 * | `deleteButton`         | {@link Core.widget.Button} | 200    | Delete button on the bbar         |
 * | `cancelButton`         | {@link Core.widget.Button} | 300    | Cancel editing button on the bbar |
 *
 * This feature is **off** by default.
 * For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/dependencies
 * @classtype dependencyEdit
 * @feature
 */
class DependencyEdit extends InstancePlugin {
  //region Config
  static get $name() {
    return 'DependencyEdit';
  }
  static get configurable() {
    return {
      /**
       * True to hide this editor if a click is detected outside it (defaults to true)
       * @config {Boolean}
       * @default
       * @category Editor
       */
      autoClose: true,
      /**
       * True to save and close this panel if ENTER is pressed in one of the input fields inside the panel.
       * @config {Boolean}
       * @default
       * @category Editor
       */
      saveAndCloseOnEnter: true,
      /**
       * True to show a delete button in the form.
       * @config {Boolean}
       * @default
       * @category Editor widgets
       */
      showDeleteButton: true,
      /**
       * The event that shall trigger showing the editor. Defaults to `dependencydblclick`, set to empty string or
       * `null` to disable editing of dependencies.
       * @config {String}
       * @default
       * @category Editor
       */
      triggerEvent: 'dependencydblclick',
      /**
       * True to show the lag field for the dependency
       * @config {Boolean}
       * @default
       * @category Editor widgets
       */
      showLagField: false,
      dependencyRecord: null,
      /**
       * Default editor configuration, used to configure the Popup.
       * @config {PopupConfig}
       * @category Editor
       */
      editorConfig: {
        title: 'L{Edit dependency}',
        localeClass: this,
        closable: true,
        defaults: {
          localeClass: this
        },
        items: {
          /**
           * Reference to the from name
           * @member {Core.widget.DisplayField} fromNameField
           * @readonly
           */
          fromNameField: {
            type: 'display',
            weight: 100,
            label: 'L{From}'
          },
          /**
           * Reference to the to name field
           * @member {Core.widget.DisplayField} toNameField
           * @readonly
           */
          toNameField: {
            type: 'display',
            weight: 200,
            label: 'L{To}'
          },
          /**
           * Reference to the type field
           * @member {Core.widget.Combo} typeField
           * @readonly
           */
          typeField: {
            type: 'combo',
            weight: 300,
            label: 'L{Type}',
            name: 'type',
            editable: false,
            valueField: 'id',
            displayField: 'name',
            localizeDisplayFields: true,
            buildItems: function () {
              const dialog = this.parent;
              return Object.keys(DependencyModel.Type).map(type => ({
                id: DependencyModel.Type[type],
                name: dialog.L(type),
                localeKey: type
              }));
            }
          },
          /**
           * Reference to the lag field
           * @member {Core.widget.DurationField} lagField
           * @readonly
           */
          lagField: {
            type: 'duration',
            weight: 400,
            label: 'L{Lag}',
            name: 'lag',
            allowNegative: true
          }
        },
        bbar: {
          defaults: {
            localeClass: this
          },
          items: {
            foo: {
              type: 'widget',
              cls: 'b-label-filler'
            },
            /**
             * Reference to the save button, if used
             * @member {Core.widget.Button} saveButton
             * @readonly
             */
            saveButton: {
              color: 'b-green',
              text: 'L{Save}'
            },
            /**
             * Reference to the delete button, if used
             * @member {Core.widget.Button} deleteButton
             * @readonly
             */
            deleteButton: {
              color: 'b-gray',
              text: 'L{Delete}'
            },
            /**
             * Reference to the cancel button, if used
             * @member {Core.widget.Button} cancelButton
             * @readonly
             */
            cancelButton: {
              color: 'b-gray',
              text: 'L{Object.Cancel}'
            }
          }
        }
      }
    };
  }
  //endregion
  //region Init & destroy
  construct(client, config) {
    const me = this;
    client.dependencyEdit = me;
    super.construct(client, config);
    if (!client.features.dependencies) {
      throw new Error('Dependencies feature required when using DependencyEdit');
    }
    me.clientListenersDetacher = client.ion({
      [me.triggerEvent]: me.onActivateEditor,
      thisObj: me
    });
  }
  doDestroy() {
    var _this$editor;
    this.clientListenersDetacher();
    (_this$editor = this.editor) === null || _this$editor === void 0 ? void 0 : _this$editor.destroy();
    super.doDestroy();
  }
  //endregion
  //region Editing
  changeEditorConfig(config) {
    const me = this,
      {
        autoClose,
        cls,
        client
      } = me;
    return ObjectHelper.assign({
      owner: client,
      align: 'b-t',
      id: `${client.id}-dependency-editor`,
      autoShow: false,
      anchor: true,
      scrollAction: 'realign',
      clippedBy: [client.timeAxisSubGridElement, client.bodyContainer],
      constrainTo: globalThis,
      autoClose,
      cls
    }, config);
  }
  //endregion
  //region Save
  get isValid() {
    return Object.values(this.editor.widgetMap).every(field => {
      if (!field.name || field.hidden) {
        return true;
      }
      return field.isValid !== false;
    });
  }
  get values() {
    const values = {};
    this.editor.eachWidget(widget => {
      if (!widget.name || widget.hidden) return;
      values[widget.name] = widget.value;
    }, true);
    return values;
  }
  /**
   * Template method, intended to be overridden. Called before the dependency record has been updated.
   * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record
   *
   **/
  onBeforeSave(dependencyRecord) {}
  /**
   * Template method, intended to be overridden. Called after the dependency record has been updated.
   * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record
   *
   **/
  onAfterSave(dependencyRecord) {}
  /**
   * Updates record being edited with values from the editor
   * @private
   */
  updateRecord(dependencyRecord) {
    const {
      values
    } = this;
    // Engine does not understand { magnitude, unit } syntax
    if (values.lag) {
      values.lagUnit = values.lag.unit;
      values.lag = values.lag.magnitude;
    }
    // Type replaces fromSide/toSide, if they are used
    if ('type' in values) {
      dependencyRecord.fromSide != null && (values.fromSide = null);
      dependencyRecord.toSide != null && (values.toSide = null);
    }
    // Chronograph doesn't filter out undefined fields, it nullifies them instead
    // https://github.com/bryntum/chronograph/issues/11
    ObjectHelper.cleanupProperties(values, true);
    dependencyRecord.set(values);
  }
  //endregion
  //region Events
  onPopupKeyDown({
    event
  }) {
    if (event.key === 'Enter' && this.saveAndCloseOnEnter && event.target.tagName.toLowerCase() === 'input') {
      // Need to prevent this key events from being fired on whatever receives focus after the editor is hidden
      event.preventDefault();
      this.onSaveClick();
    }
  }
  onSaveClick() {
    if (this.save()) {
      this.afterSave();
      this.editor.hide();
    }
  }
  async onDeleteClick() {
    if (await this.deleteDependency()) {
      this.afterDelete();
    }
    this.editor.hide();
  }
  onCancelClick() {
    this.afterCancel();
    this.editor.hide();
  }
  afterSave() {}
  afterDelete() {}
  afterCancel() {}
  //region Editing
  // Called from editDependency() to actually show the editor
  internalShowEditor(dependencyRecord) {
    const me = this,
      {
        client
      } = me,
      editor = me.getEditor(dependencyRecord);
    me.loadRecord(dependencyRecord);
    /**
     * Fires on the owning Scheduler when the editor for a dependency is available but before it is shown. Allows
     * manipulating fields before the widget is shown.
     * @event beforeDependencyEditShow
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The scheduler
     * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependencyEdit feature
     * @param {Scheduler.model.DependencyModel} dependencyRecord The record about to be shown in the editor.
     * @param {Core.widget.Popup} editor The editor popup
     */
    client.trigger('beforeDependencyEditShow', {
      dependencyEdit: me,
      dependencyRecord,
      editor
    });
    let showPoint = me.lastPointerDownCoordinate;
    if (!showPoint) {
      const center = Rectangle.from(client.element).center;
      showPoint = [center.x - editor.width / 2, center.y - editor.height / 2];
    }
    return editor.showBy(showPoint);
  }
  /**
   * Opens a popup to edit the passed dependency.
   * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency to edit
   * @return {Promise} A Promise that yields `true` after the editor is shown
   * or `false` if some application logic vetoed the editing (see `beforeDependencyEdit` in the docs).
   */
  async editDependency(dependencyRecord) {
    const me = this,
      {
        client
      } = me;
    if (client.readOnly || dependencyRecord.readOnly ||
    /**
     * Fires on the owning Scheduler before an dependency is displayed in the editor.
     * This may be listened for to allow an application to take over dependency editing duties. Return `false` to
     * stop the default editing UI from being shown or a `Promise` yielding `true` or `false` for async vetoing.
     * @event beforeDependencyEdit
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The scheduler
     * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependencyEdit feature
     * @param {Scheduler.model.DependencyModel} dependencyRecord The record about to be shown in the editor.
     * @preventable
     * @async
     */
    (await client.trigger('beforeDependencyEdit', {
      dependencyEdit: me,
      dependencyRecord
    })) === false) {
      return false;
    }
    // wait till the editor is shown
    await this.internalShowEditor(dependencyRecord);
    return true;
  }
  //endregion
  //region Save
  /**
   * Gets an editor instance. Creates on first call, reuses on consecutive
   * @internal
   * @returns {Scheduler.view.DependencyEditor} Editor popup
   */
  getEditor() {
    var _me$saveButton, _me$deleteButton, _me$cancelButton;
    const me = this;
    let {
      editor
    } = me;
    if (editor) {
      return editor;
    }
    editor = me.editor = DependencyEditor.new({
      dependencyEditFeature: me,
      autoShow: false,
      anchor: true,
      scrollAction: 'realign',
      constrainTo: globalThis,
      autoClose: me.autoClose,
      cls: me.cls,
      rootElement: me.client.rootElement,
      internalListeners: {
        keydown: me.onPopupKeyDown,
        thisObj: me
      }
    }, me.editorConfig);
    if (editor.items.length === 0) {
      console.warn('Editor configured without any `items`');
    }
    // assign widget refs
    editor.eachWidget(widget => {
      const ref = widget.ref || widget.id;
      // don't overwrite if already defined
      if (ref && !me[ref]) {
        me[ref] = widget;
      }
    });
    (_me$saveButton = me.saveButton) === null || _me$saveButton === void 0 ? void 0 : _me$saveButton.ion({
      click: 'onSaveClick',
      thisObj: me
    });
    (_me$deleteButton = me.deleteButton) === null || _me$deleteButton === void 0 ? void 0 : _me$deleteButton.ion({
      click: 'onDeleteClick',
      thisObj: me
    });
    (_me$cancelButton = me.cancelButton) === null || _me$cancelButton === void 0 ? void 0 : _me$cancelButton.ion({
      click: 'onCancelClick',
      thisObj: me
    });
    return me.editor;
  }
  //endregion
  //region Delete
  /**
   * Sets fields values from record being edited
   * @private
   */
  loadRecord(dependency) {
    const me = this;
    me.fromNameField.value = dependency.fromEvent.name;
    me.toNameField.value = dependency.toEvent.name;
    if (me.lagField) {
      me.lagField.value = new Duration(dependency.lag, dependency.lagUnit);
    }
    me.editor.record = me.dependencyRecord = dependency;
  }
  //endregion
  //region Stores
  /**
   * Saves the changes (applies them to record if valid, if invalid editor stays open)
   * @private
   * @fires beforeDependencySave
   * @fires beforeDependencyAdd
   * @fires afterDependencySave
   * @returns {*}
   */
  async save() {
    const me = this,
      {
        client,
        dependencyRecord
      } = me;
    if (!dependencyRecord || !me.isValid) {
      return;
    }
    const {
      dependencyStore,
      values
    } = me;
    /**
     * Fires on the owning Scheduler before a dependency is saved
     * @event beforeDependencySave
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The scheduler instance
     * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be saved
     * @param {Object} values The new values
     * @preventable
     */
    if (client.trigger('beforeDependencySave', {
      dependencyRecord,
      values
    }) !== false) {
      var _client$project;
      me.onBeforeSave(dependencyRecord);
      me.updateRecord(dependencyRecord);
      // Check if this is a new record
      if (dependencyStore && !dependencyRecord.stores.length) {
        /**
         * Fires on the owning Scheduler before a dependency is added
         * @event beforeDependencyAdd
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler
         * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependency edit feature
         * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be added
         * @preventable
         */
        if (client.trigger('beforeDependencyAdd', {
          dependencyRecord,
          dependencyEdit: me
        }) === false) {
          return;
        }
        dependencyStore.add(dependencyRecord);
      }
      await ((_client$project = client.project) === null || _client$project === void 0 ? void 0 : _client$project.commitAsync());
      /**
       * Fires on the owning Scheduler after a dependency is successfully saved
       * @event afterDependencySave
       * @on-owner
       * @param {Scheduler.view.Scheduler} source The scheduler instance
       * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be saved
       */
      client.trigger('afterDependencySave', {
        dependencyRecord
      });
      me.onAfterSave(dependencyRecord);
    }
    return dependencyRecord;
  }
  /**
   * Delete dependency being edited
   * @private
   * @fires beforeDependencyDelete
   */
  async deleteDependency() {
    const {
      client,
      editor,
      dependencyRecord
    } = this;
    /**
     * Fires on the owning Scheduler before a dependency is deleted
     * @event beforeDependencyDelete
     * @on-owner
     * @param {Scheduler.view.Scheduler} source The scheduler instance
     * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record about to be deleted
     * @preventable
     */
    if (client.trigger('beforeDependencyDelete', {
      dependencyRecord
    }) !== false) {
      var _client$project2;
      if (editor.containsFocus) {
        editor.revertFocus();
      }
      client.dependencyStore.remove(dependencyRecord);
      await ((_client$project2 = client.project) === null || _client$project2 === void 0 ? void 0 : _client$project2.commitAsync());
      return true;
    }
    return false;
  }
  get dependencyStore() {
    return this.client.dependencyStore;
  }
  //endregion
  //region Events
  onActivateEditor({
    dependency,
    event
  }) {
    if (!this.disabled) {
      this.lastPointerDownCoordinate = [event.clientX, event.clientY];
      this.editDependency(dependency);
    }
  }
  //endregion
}

DependencyEdit._$name = 'DependencyEdit';
GridFeatureManager.registerFeature(DependencyEdit, false);

/**
 * @module Scheduler/feature/ScheduleContext
 */
/**
 * Allow visually selecting a schedule "cell" by clicking, or {@link #config-triggerEvent any other pointer gesture}.
 *
 * This feature is **disabled** by default
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         // Configure as a truthy value to enable the feature
 *         scheduleContext : {
 *             triggerEvent : 'hover',
 *             renderer     : (context, element) => {
 *                 element.innerText = '😎';
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * The contextual details are available in the {@link #property-context} property.
 *
 * **Note that the context is cleared upon change of {@link Scheduler.view.Scheduler#property-viewPreset}
 * such as when zooming in or out.**
 *
 * @extends Core/mixin/InstancePlugin
 * @inlineexample Scheduler/feature/ScheduleContext.js
 * @classtype scheduleContext
 * @feature
 */
class ScheduleContext extends InstancePlugin.mixin(Delayable) {
  static get $name() {
    return 'ScheduleContext';
  }
  static delayable = {
    syncContextElement: 'raf'
  };
  static configurable = {
    /**
     * The pointer event type to use to update the context. May be `'hover'` to highlight the
     * tick context when moving the mouse across the timeline.
     * @config {'click'|'hover'|'contextmenu'|'mousedown'}
     * @default
     */
    triggerEvent: 'click',
    /**
     * A function (or the name of a function) which may mutate the contents of the context overlay
     * element which tracks the active resource/tick context.
     * @config {String|Function}
     * @param {TimelineContext} context The context being highlighted.
     * @param {HTMLElement} element The context highlight element. This will be empty each time.
     */
    renderer: null,
    /**
     * The active context.
     * @member {TimelineContext} timelineContext
     * @readonly
     */
    context: {
      $config: {
        // Reject non-changes so that when using mousemove, we only update the context
        // when it changes.
        equal(c1, c2) {
          return (c1 === null || c1 === void 0 ? void 0 : c1.index) === (c2 === null || c2 === void 0 ? void 0 : c2.index) && (c1 === null || c1 === void 0 ? void 0 : c1.tickParentIndex) === (c2 === null || c2 === void 0 ? void 0 : c2.tickParentIndex) && !(((c1 === null || c1 === void 0 ? void 0 : c1.tickStartDate) || 0) - ((c2 === null || c2 === void 0 ? void 0 : c2.tickStartDate) || 0));
        }
      }
    }
  };
  /**
   * The contextual information about which cell was clicked on and highlighted.
   *
   * When the {@link Scheduler.view.Scheduler#property-viewPreset} is changed (such as when zooming)
   * the context is cleared and the highlight is removed.
   *
   * @member {Object} context
   * @property {Scheduler.view.TimelineBase} context.source The owning Scheduler
   * @property {Date} context.date Date at mouse position
   * @property {Scheduler.model.TimeSpan} context.tick A record which encapsulates the time axis tick clicked on.
   * @property {Number} context.tickIndex The index of the time axis tick clicked on.
   * @property {Date} context.tickStartDate The start date of the current time axis tick
   * @property {Date} context.tickEndDate The end date of the current time axis tick
   * @property {Grid.row.Row} context.row Clicked row (in horizontal mode only)
   * @property {Number} context.index Index of clicked resource
   * @property {Scheduler.model.ResourceModel} context.resourceRecord Resource record
   * @property {MouseEvent} context.event Browser event
   */
  construct(client, config) {
    super.construct(client, config);
    const {
        triggerEvent
      } = this,
      listeners = {
        datachange: 'syncContextElement',
        timeaxisviewmodelupdate: 'onTimeAxisViewModelUpdate',
        presetchange: 'clearContext',
        thisObj: this
      };
    // If mousemove is our trigger, we cab use the client's timelineContextChange event
    if (triggerEvent === 'mouseover') {
      listeners.timelineContextChange = 'onTimelineContextChange';
    }
    // Otherwise, we have to listen for the required events on Schedule and events
    else {
      // Context menu will be expected to update the context if click or mousedown
      // is the triggerEvent. Context menu is a mousedown gesture.
      if (triggerEvent === 'click' || triggerEvent === 'mousedown') {
        listeners.schedulecontextmenu = 'onScheduleContextGesture';
      }
      Object.assign(listeners, {
        [`schedule${triggerEvent}`]: 'onScheduleContextGesture',
        [`event${triggerEvent}`]: 'onScheduleContextGesture',
        ...listeners
      });
    }
    // required to work
    client.useBackgroundCanvas = true;
    client.ion(listeners);
    client.rowManager.ion({
      rowheight: 'syncContextElement',
      thisObj: this
    });
  }
  changeTriggerEvent(triggerEvent) {
    // Both these things should route through to using the client's timelineContextChange event
    if (triggerEvent === 'hover' || triggerEvent === 'mousemove') {
      triggerEvent = 'mouseover';
    }
    return triggerEvent;
  }
  get element() {
    return this._element || (this._element = DomHelper.createElement({
      parent: this.client.backgroundCanvas,
      className: 'b-schedule-selected-tick'
    }));
  }
  // Handle the Client's own timelineContextChange event which it maintains on mousemove
  onTimelineContextChange({
    context
  }) {
    this.context = context;
  }
  // Handle the scheduleclick or eventclick Scheduler events if we re not using mouseover
  onScheduleContextGesture(context) {
    this.context = context;
  }
  onTimeAxisViewModelUpdate({
    source: timeAxisViewModel
  }) {
    var _this$context;
    // Just a mutation of existing tick details, sync the element
    if (timeAxisViewModel.timeAxis.includes((_this$context = this.context) === null || _this$context === void 0 ? void 0 : _this$context.tick)) {
      this.syncContextElement();
    }
    // The tick has gone, we have moved to a new ViewPreset, so clear the context.
    else {
      this.clearContext();
    }
  }
  clearContext() {
    this.context = null;
  }
  updateContext(context, oldContext) {
    this.syncContextElement();
  }
  syncContextElement() {
    if (this.context && this.enabled) {
      const me = this,
        {
          client,
          element,
          context,
          renderer
        } = me,
        {
          isVertical
        } = client,
        {
          style
        } = element,
        row = isVertical ? client.rowManager.rows[0] : client.getRowFor(context.resourceRecord);
      if (row) {
        const {
            tickStartDate,
            tickEndDate,
            resourceRecord
          } = context,
          // get the position clicked based on dates
          renderData = client.currentOrientation.getTimeSpanRenderData({
            startDate: tickStartDate,
            endDate: tickEndDate,
            startDateMS: tickStartDate.getTime(),
            endDateMS: tickEndDate.getTime()
          }, resourceRecord);
        let top, width, height;
        if (isVertical) {
          top = renderData.top;
          width = renderData.resourceWidth;
          height = renderData.height;
        } else {
          top = row.top;
          width = renderData.width;
          height = row.height;
        }
        // Move to current cell
        style.display = '';
        style.width = `${width}px`;
        style.height = `${height}px`;
        DomHelper.setTranslateXY(element, renderData.left, top);
        // In case we updated on a datachange action : 'remove' or 'add' event.
        context.index = row.index;
        // Undo any contents added by the renderer last time round.
        element.innerHTML = '';
        // Show the context and the element to the renderer
        renderer && me.callback(renderer, me, [context, element]);
      }
      // No row for resource might mean it's scrolled out of view or filtered out
      // so just hide so that the next valid sync can restore it to visibility
      else {
        style.display = 'none';
      }
    } else {
      this.element.style.display = 'none';
    }
  }
}
ScheduleContext.featureClass = 'b-scheduler-context';
ScheduleContext._$name = 'ScheduleContext';
GridFeatureManager.registerFeature(ScheduleContext, false, ['Scheduler']);

/**
 * @module Scheduler/feature/EventCopyPaste
 */
/**
 * Allow using [Ctrl/CMD + C/X] and [Ctrl/CMD + V] to copy/cut and paste events.
 *
 * This feature also adds entries to the {@link Scheduler/feature/EventMenu} for copying & cutting (see example below
 * for how to configure) and to the {@link Scheduler/feature/ScheduleMenu} for pasting.
 *
 * You can configure how a newly pasted record is named using {@link #function-generateNewName}.
 *
 * {@inlineexample Scheduler/feature/EventCopyPaste.js}
 *
 * If you want to highlight the paste location when clicking in the schedule, consider enabling the
 * {@link Scheduler/feature/ScheduleContext} feature.
 *
 * <div class="note">When used with Scheduler Pro, pasting will bypass any constraint set on the event to allow the
 * copy to be assigned the targeted date.</div>
 *
 * This feature is **enabled** by default.
 *
 * ## Customize menu items
 *
 * See {@link Scheduler/feature/EventMenu} and {@link Scheduler/feature/ScheduleMenu} for more info on customizing the
 * menu items supplied by the feature. This snippet illustrates the concept:
 *
 * ```javascript
 * // Custom copy text + remove cut option from event menu:
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 copyEvent : {
 *                     text : 'Copy booking'
 *                 },
 *                 cutEvent  : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ## Keyboard shortcuts
 *
 * The feature has the following default keyboard shortcuts:
 *
 * | Keys       | Action   | Action description                                |
 * |------------|----------|---------------------------------------------------|
 * | `Ctrl`+`C` | *copy*   | Copies selected event(s) into the clipboard.      |
 * | `Ctrl`+`X` | *cut*    | Cuts out selected event(s) into the clipboard.    |
 * | `Ctrl`+`V` | *paste*  | Insert copied or cut event(s) from the clipboard. |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Scheduler/guides/customization/keymap.md).
 *
 * ## Multi assigned events
 *
 * In a Scheduler that uses single assignment, copying and then pasting creates a clone of the event and assigns it
 * to the target resource. Cutting and pasting moves the original event to the target resource.
 *
 * In a Scheduler using multi assignment, the behaviour is slightly more complex. Cutting and pasting reassigns the
 * event to the target, keeping other assignments of the same event intact. The behaviour for copying and pasting is
 * configurable using the {@link #config-copyPasteAction} config. It accepts two values:
 *
 * * `'clone'` - The default, the event is cloned and the clone is assigned to the target resource. Very similar to the
 *   behaviour with single assignment (event count goes up by 1).
 * * `'assign'` - The original event is assigned to the target resource (event count is unaffected).
 *
 * This snippet shows how to reconfigure it:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventCopyPaste : {
 *             copyPasteAction : 'assign'
 *         }
 *     }
 * });
 * ```
 *
 * <div class="note">Copying multiple assignments of the same event will always result in all but the first assignment
 * being removed on paste, since paste targets a single resource and an event can only be assigned to a resource once.
 * </div>
 *
 * ## Native/shared clipboard
 *
 * If you have multiple Schedulers (or other Bryntum products) on the same page, they will share clipboard. This makes
 * it possible to copy and paste between different Scheduler instances. It is also possible to use the native Clipboard
 * API if it is available and if you set {@link #config-useNativeClipboard} to `true`.
 *
 * Regardless of native clipboard availability, copy-pasting "outside" of the current Scheduler instance will convert
 * the copied events to a string. When pasting, the string will then be parsed back into events. In case of usage of the
 * native Clipboard API, this means it is possible to copy and paste events between completely different applications.
 *
 * To configure the fields that is converted and parsed from the copied string value, please see the
 * {@link #config-eventToStringFields} config.
 *
 * @extends Grid/feature/base/CopyPasteBase
 * @classtype eventCopyPaste
 * @feature
 */
class EventCopyPaste extends CopyPasteBase.mixin(AttachToProjectMixin) {
  static $name = 'EventCopyPaste';
  static pluginConfig = {
    assign: ['copyEvents', 'pasteEvents'],
    chain: ['populateEventMenu', 'populateScheduleMenu', 'onEventDataGenerated']
  };
  static configurable = {
    /**
     * The field to use as the name field when updating the name of copied records
     * @config {String}
     * @default
     */
    nameField: 'name',
    /**
     * How to handle a copy paste operation when the host uses multi assignment. Either:
     *
     * - `'clone'`  - The default, clone the copied event, assigning the clone to the target resource.
     * - `'assign'` - Add an assignment for the existing event to the target resource.
     *
     * For single assignment mode, it always uses the `'clone'` behaviour.
     *
     * @config {'clone'|'assign'}
     * @default
     */
    copyPasteAction: 'clone',
    /**
     * When copying events (or assignments), data will be sent to the clipboard as a tab (`\t`) and new-line (`\n`)
     * separated string with field values for fields present in this config (in specified order). The default
     * included fields are (in this order):
     * * name
     * * startDate
     * * endDate
     * * duration
     * * durationUnit
     * * allDay
     * To override, provide your own array of fields:
     * ```javascript
     * new Scheduler({
     *     features : {
     *         eventCopyPaste : {
     *             eventToStringFields : [
     *                'name',
     *                'startDate',
     *                'endDate',
     *                'percentDone'
     *             ]
     *         }
     *     }
     * });
     * ```
     * <div class="note">Please note that this config is both used for **converting** events to a string value and
     * is also used to **parse** a string value to events.</div>
     * @config {Array<String>}
     */
    eventToStringFields: ['name', 'startDate', 'endDate', 'duration', 'durationUnit', 'allDay']
  };
  construct(scheduler, config) {
    super.construct(scheduler, config);
    scheduler.ion({
      eventClick: 'onEventClick',
      scheduleClick: 'onScheduleClick',
      projectChange: () => {
        this.clearClipboard();
        this._cellClickedContext = null;
      },
      thisObj: this
    });
  }
  // Used in events to separate events from different features from each other
  entityName = 'event';
  get scheduler() {
    return this.client;
  }
  attachToEventStore(eventStore) {
    super.attachToEventStore(eventStore);
    delete this._eventClickedContext;
  }
  onEventDataGenerated(eventData) {
    const {
      assignmentRecord
    } = eventData;
    // No assignmentRecord for resource time ranges, which we want to ignore anyway
    if (assignmentRecord) {
      eventData.cls['b-cut-item'] = assignmentRecord.meta.isCut;
    }
  }
  onEventClick(context) {
    this._cellClickedContext = null;
    this._eventClickedContext = context;
  }
  onScheduleClick(context) {
    this._cellClickedContext = context;
    this._eventClickedContext = null;
  }
  isActionAvailable({
    event
  }) {
    var _this$client$features, _this$client$focusedC;
    // No action if
    // 1. there is selected text on the page
    // 2. cell editing is active
    // 3. cursor is not in the grid (filter bar etc)
    // 4. focus is on specialrow
    return !this.disabled && globalThis.getSelection().toString().length === 0 && !((_this$client$features = this.client.features.cellEdit) !== null && _this$client$features !== void 0 && _this$client$features.isEditing) && Boolean(event.target.closest('.b-timeaxissubgrid')) && !((_this$client$focusedC = this.client.focusedCell) !== null && _this$client$focusedC !== void 0 && _this$client$focusedC.isSpecialRow);
  }
  async copy() {
    await this.copyEvents();
  }
  async cut() {
    await this.copyEvents(undefined, true);
  }
  async paste() {
    await this.pasteEvents();
  }
  /**
   * Copy events (when using single assignment mode) or assignments (when using multi assignment mode) to clipboard to
   * paste later
   * @fires beforeCopy
   * @fires copy
   * @param {Scheduler.model.EventModel[]|Scheduler.model.AssignmentModel[]} [records] Pass records to copy them,
   * leave out to copying current selection
   * @param {Boolean} [isCut] Copies by default, pass `true` to cut instead
   * @category Edit
   * @on-owner
   */
  async copyEvents(records = this.scheduler.selectedAssignments, isCut = false) {
    const me = this,
      {
        scheduler
      } = me;
    // Relay to original if split
    if (scheduler.splitFrom) {
      return scheduler.splitFrom.features.eventCopyPaste.copyEvents(records, isCut);
    }
    if (!(records !== null && records !== void 0 && records.length)) {
      return;
    }
    let assignmentRecords = records.slice(); // Slice to not lose records if selection changes
    if (records[0].isEventModel) {
      assignmentRecords = records.map(r => r.assignments).flat();
    }
    // Prevent cutting readOnly events
    if (isCut) {
      assignmentRecords = assignmentRecords.filter(a => !a.event.readOnly);
    }
    const eventRecords = assignmentRecords.map(a => a.event);
    if (!assignmentRecords.length || scheduler.readOnly) {
      return;
    }
    await me.writeToClipboard({
      assignmentRecords,
      eventRecords
    }, isCut);
    /**
     * Fires on the owning Scheduler after a copy action is performed.
     * @event copy
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Owner scheduler
     * @param {Scheduler.model.EventModel[]} eventRecords The event records that were copied
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignment records that were copied
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'event' to distinguish this event from other copy events
     */
    scheduler.trigger('copy', {
      assignmentRecords,
      eventRecords,
      isCut,
      entityName: me.entityName
    });
    // refresh to call onEventDataGenerated and reapply the cls for records where the cut was canceled
    scheduler.refreshWithTransition();
    me._focusedEventOnCopy = me._eventClickedContext;
  }
  async beforeCopy({
    data: {
      assignmentRecords,
      eventRecords
    },
    isCut
  }) {
    /**
     * Fires on the owning Scheduler before a copy action is performed, return `false` to prevent the action
     * @event beforeCopy
     * @preventable
     * @on-owner
     * @async
     * @param {Scheduler.view.Scheduler} source Owner scheduler
     * @param {Scheduler.model.EventModel[]} eventRecords The event records about to be copied
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignment records about to be copied
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'event' to distinguish this event from other beforeCopy events
     */
    return await this.scheduler.trigger('beforeCopy', {
      assignmentRecords,
      eventRecords,
      isCut,
      entityName: this.entityName
    });
  }
  // Called from Clipboardable when cutData changes
  handleCutData({
    source
  }) {
    var _me$cutData;
    const me = this;
    if (source !== me && (_me$cutData = me.cutData) !== null && _me$cutData !== void 0 && _me$cutData.length) {
      const {
        assignmentRecords,
        eventRecords
      } = me.cutData[0];
      if (assignmentRecords !== null && assignmentRecords !== void 0 && assignmentRecords.length) {
        me.scheduler.assignmentStore.remove(assignmentRecords);
      }
      if (eventRecords !== null && eventRecords !== void 0 && eventRecords.length) {
        me.scheduler.eventStore.remove(eventRecords);
      }
    }
  }
  /**
   * Called from Clipboardable after writing a non-string value to the clipboard
   * @param eventRecords
   * @returns {string}
   * @private
   */
  stringConverter({
    eventRecords
  }) {
    const rows = [];
    for (const event of eventRecords) {
      rows.push(this.eventToStringFields.map(field => {
        const value = event[field];
        if (value instanceof Date) {
          return DateHelper.format(value, this.dateFormat);
        }
        return value;
      }).join('\t'));
    }
    return rows.join('\n');
  }
  // Called from Clipboardable for each cut out record
  setIsCut({
    assignmentRecords
  }, isCut) {
    assignmentRecords.forEach(assignment => {
      assignment.meta.isCut = isCut;
    });
    // refresh to call onEventDataGenerated and reapply the cls for records where the cut was canceled
    this.scheduler.refreshWithTransition();
  }
  /**
   * Paste events or assignments to specified date and resource
   * @fires beforePaste
   * @fires paste
   * @param {Date} [date] Date where the events or assignments will be pasted
   * @param {Scheduler.model.ResourceModel} [resourceRecord] Resource to assign the pasted events or assignments to
   * @category Edit
   * @on-owner
   */
  async pasteEvents(date, resourceRecord) {
    var _clipboardData$assign;
    const me = this,
      {
        scheduler
      } = me;
    // Relay to original if split
    if (scheduler.splitFrom) {
      return scheduler.splitFrom.features.eventCopyPaste.pasteEvents(date, resourceRecord);
    }
    const {
        entityName,
        isCut,
        _cellClickedContext,
        _eventClickedContext
      } = me,
      {
        eventStore,
        assignmentStore
      } = scheduler;
    if (arguments.length === 0) {
      if (_cellClickedContext) {
        date = _cellClickedContext.date;
        resourceRecord = _cellClickedContext.resourceRecord;
      } else if (me._focusedEventOnCopy !== _eventClickedContext) {
        date = _eventClickedContext.eventRecord.startDate;
        resourceRecord = _eventClickedContext.resourceRecord;
      }
    }
    if (resourceRecord) {
      resourceRecord = resourceRecord.$original;
    }
    const clipboardData = await me.readFromClipboard({
      resourceRecord,
      date
    });
    if (!(clipboardData !== null && clipboardData !== void 0 && (_clipboardData$assign = clipboardData.assignmentRecords) !== null && _clipboardData$assign !== void 0 && _clipboardData$assign.length)) {
      return;
    }
    const {
      assignmentRecords,
      eventRecords
    } = clipboardData;
    let toFocus = null;
    const pastedEvents = new Set(),
      pastedEventRecords = [];
    for (const assignmentRecord of assignmentRecords) {
      let {
        event
      } = assignmentRecord;
      const targetResourceRecord = resourceRecord || assignmentRecord.resource,
        targetDate = date || assignmentRecord.event.startDate;
      // Pasting targets a specific resource, we cannot have multiple assignments to the same so remove all but
      // the first (happens when pasting multiple assignments of the same event)
      if (pastedEvents.has(event)) {
        if (isCut) {
          assignmentRecord.remove();
        }
        continue;
      }
      pastedEvents.add(event);
      // Cut always means reassign
      if (isCut) {
        assignmentRecord.meta.isCut = false;
        assignmentRecord.resource = targetResourceRecord;
        toFocus = assignmentRecord;
      }
      // Copy creates a new event in single assignment, or when configured to copy
      else if (eventStore.usesSingleAssignment || me.copyPasteAction === 'clone') {
        event = event.copy();
        event.name = me.generateNewName(event);
        eventStore.add(event);
        event.assign(targetResourceRecord);
        toFocus = assignmentStore.last;
      }
      // Safeguard against pasting on a resource where the event is already assigned,
      // a new assignment in multiassign mode will only change the date in such case
      else if (!event.resources.includes(targetResourceRecord)) {
        const newAssignmentRecord = assignmentRecord.copy();
        newAssignmentRecord.resource = targetResourceRecord;
        [toFocus] = assignmentStore.add(newAssignmentRecord);
      }
      event.startDate = targetDate;
      // Pro specific, to allow event to appear where pasted
      if (event.constraintDate) {
        event.constraintDate = null;
      }
      pastedEventRecords.push(event);
    }
    /**
     * Fires on the owning Scheduler after a paste action is performed.
     * @event paste
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Owner scheduler
     * @param {Scheduler.model.EventModel[]} eventRecords Original events
     * @param {Scheduler.model.EventModel[]} pastedEventRecords Pasted events
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Pasted assignments
     * @param {Date} date date Pasted to this date
     * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'event' to distinguish this event from other paste events
     */
    scheduler.trigger('paste', {
      assignmentRecords,
      pastedEventRecords,
      eventRecords,
      resourceRecord,
      date,
      isCut,
      entityName
    });
    // Focus the last pasted assignment
    const detacher = scheduler.ion({
      renderEvent({
        assignmentRecord
      }) {
        if (assignmentRecord === toFocus) {
          scheduler.navigateTo(assignmentRecord, {
            scrollIntoView: false
          });
          detacher();
        }
      }
    });
    if (isCut) {
      await me.clearClipboard();
    }
  }
  // Called from Clipboardable before finishing the internal clipboard read
  async beforePaste({
    data: {
      assignmentRecords,
      eventRecords
    },
    resourceRecord,
    isCut,
    date
  }) {
    const {
        scheduler
      } = this,
      eventData = {
        assignmentRecords,
        eventRecords,
        resourceRecord: resourceRecord || assignmentRecords[0].resource,
        date,
        isCut,
        entityName: this.entityName
      };
    let reason;
    // No pasting to readOnly resources
    if (resourceRecord !== null && resourceRecord !== void 0 && resourceRecord.readOnly) {
      reason = 'resourceReadOnly';
    }
    if (!scheduler.allowOverlap) {
      const pasteWouldResultInOverlap = assignmentRecords.some(assignmentRecord => !scheduler.isDateRangeAvailable(assignmentRecord.event.startDate, assignmentRecord.event.endDate, isCut ? assignmentRecord.event : null, assignmentRecord.resource));
      if (pasteWouldResultInOverlap) {
        reason = 'overlappingEvents';
      }
    }
    /**
     * Fires on the owning Scheduler if a paste action is not allowed
     * @event pasteNotAllowed
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Owner scheduler
     * @param {Scheduler.model.EventModel[]} eventRecords
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
     * @param {Date} date The paste date
     * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'event' to distinguish this event from other `pasteNotAllowed` events
     * @param {'overlappingEvents'|'resourceReadOnly'} reason A string id to use for displaying an error message to the user.
     */
    if (reason) {
      scheduler.trigger('pasteNotAllowed', {
        ...eventData,
        reason
      });
      return false;
    }
    /**
     * Fires on the owning Scheduler before a paste action is performed, return `false` to prevent the action
     * @event beforePaste
     * @preventable
     * @on-owner
     * @async
     * @param {Scheduler.view.Scheduler} source Owner scheduler
     * @param {Scheduler.model.EventModel[]} eventRecords The events about to be pasted
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignments about to be pasted
     * @param {Date} date The date when the pasted events will be scheduled
     * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record, the clipboard
     * event records will be assigned to this resource.
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'event' to distinguish this event from other beforePaste events
     */
    return await this.scheduler.trigger('beforePaste', eventData);
  }
  /**
   * Called from Clipboardable after reading from clipboard, and it is determined that the clipboard data is
   * "external"
   * @param json
   * @returns {Object}
   * @private
   */
  stringParser(clipboardData) {
    const {
        eventStore,
        assignmentStore
      } = this.scheduler,
      {
        modifiedRecords: eventRecords
      } = this.setFromStringData(clipboardData, true, eventStore, this.eventToStringFields),
      assignmentRecords = [];
    for (const event of eventRecords) {
      const assignment = new assignmentStore.modelClass({
        eventId: event.id
      });
      assignment.event = event;
      assignmentRecords.push(assignment);
    }
    return {
      eventRecords,
      assignmentRecords
    };
  }
  populateEventMenu({
    assignmentRecord,
    items
  }) {
    const me = this,
      {
        scheduler
      } = me;
    if (!scheduler.readOnly) {
      items.copyEvent = {
        text: 'L{copyEvent}',
        localeClass: me,
        icon: 'b-icon b-icon-copy',
        weight: 110,
        onItem: () => {
          const assignments = scheduler.isAssignmentSelected(assignmentRecord) ? scheduler.selectedAssignments : [assignmentRecord];
          me.copyEvents(assignments);
        }
      };
      items.cutEvent = {
        text: 'L{cutEvent}',
        localeClass: me,
        icon: 'b-icon b-icon-cut',
        weight: 120,
        disabled: assignmentRecord.event.readOnly,
        onItem: () => {
          const assignments = scheduler.isAssignmentSelected(assignmentRecord) ? scheduler.selectedAssignments : [assignmentRecord];
          me.copyEvents(assignments, true);
        }
      };
    }
  }
  populateScheduleMenu({
    items,
    resourceRecord
  }) {
    const me = this,
      {
        scheduler
      } = me;
    if (!scheduler.readOnly && me.hasClipboardData() !== false) {
      items.pasteEvent = {
        text: 'L{pasteEvent}',
        localeClass: me,
        icon: 'b-icon b-icon-paste',
        disabled: scheduler.resourceStore.count === 0 || resourceRecord.readOnly,
        weight: 110,
        onItem: ({
          date,
          resourceRecord
        }) => me.pasteEvents(date, resourceRecord, scheduler.getRowFor(resourceRecord))
      };
    }
  }
  /**
   * A method used to generate the name for a copy pasted record. By defaults appends "- 2", "- 3" as a suffix.
   *
   * @param {Scheduler.model.EventModel} eventRecord The new eventRecord being pasted
   * @returns {String}
   */
  generateNewName(eventRecord) {
    const originalName = eventRecord.getValue(this.nameField);
    let counter = 2;
    while (this.client.eventStore.findRecord(this.nameField, `${originalName} - ${counter}`)) {
      counter++;
    }
    return `${originalName} - ${counter}`;
  }
}
EventCopyPaste.featureClass = 'b-event-copypaste';
EventCopyPaste._$name = 'EventCopyPaste';
GridFeatureManager.registerFeature(EventCopyPaste, true, 'Scheduler');

/**
 * @module Scheduler/feature/EventDrag
 */
/**
 * Allows user to drag and drop events within the scheduler, to change startDate or resource assignment.
 *
 * This feature is **enabled** by default
 *
 * ## Customizing the drag drop tooltip
 *
 * To show custom HTML in the tooltip, please see the {@link #config-tooltipTemplate} config. Example:
 *
 * ```javascript
 * features: {
 *     eventDrag : {
 *         // A minimal start date tooltip
 *         tooltipTemplate : ({ eventRecord, startDate }) => {
 *             return DateHelper.format(startDate, 'HH:mm');
 *         }
 *     }
 * }
 * ```
 *
 * ## Constraining the drag drop area
 *
 * You can constrain how the dragged event is allowed to move by using the following configs
 * * {@link #config-constrainDragToResource} Resource fixed, only allowed to change start date
 * * {@link #config-constrainDragToTimeSlot} Start date is fixed, only move between resources
 * * {@link Scheduler.view.Scheduler#config-getDateConstraints} A method on the Scheduler instance
 *    which lets you define the date range for the dragged event programmatically
 *
 * ```js
 * // Enable dragging + constrain drag to current resource
 * const scheduler = new Scheduler({
 *     features : {
 *         eventDrag : {
 *             constrainDragToResource : true
 *         }
 *     }
 * });
 * ```
 *
 * ## Drag drop events from outside
 *
 * Dragging unplanned events from an external grid is a very popular use case. There are
 * several demos showing you how to do this. Please see the [Drag from grid demo](../examples/dragfromgrid)
 * and study the **Drag from grid guide** to learn more.
 *
 * ## Drag drop events to outside target
 *
 * You can also drag events outside the schedule area by setting {@link #config-constrainDragToTimeline} to `false`. You
 * should also either:
 * * provide a {@link #config-validatorFn} to programmatically define if a drop location is valid or not
 * * configure a {@link #config-externalDropTargetSelector} CSS selector to define where drops are allowed
 *
 * See [this demo](../examples/drag-outside) to see this in action.
 *
 * ## Validating drag drop
 *
 * It is easy to programmatically decide what is a valid drag drop operation. Use the {@link #config-validatorFn}
 * and return either `true` / `false` (optionally a message to show to the user).
 *
 * ```javascript
 * features : {
 *     eventDrag : {
 *        validatorFn({ eventRecords, newResource }) {
 *            const task  = eventRecords[0],
 *                  valid = newResource.role === task.resource.role;
 *
 *            return {
 *                valid   : newResource.role === task.resource.role,
 *                message : valid ? '' : 'Resource role does not match required role for this task'
 *            };
 *        }
 *     }
 * }
 * ```
 *
 * See [this demo](../examples/validation) to see validation in action.
 *
 * If you instead want to do a single validation upon drop, you can listen to {@link #event-beforeEventDropFinalize}
 * and set the `valid` flag on the context object provided.
 *
 * ```javascript
 *   const scheduler = new Scheduler({
 *      listeners : {
 *          beforeEventDropFinalize({ context }) {
 *              const { eventRecords } = context;
 *              // Don't allow dropping events in the past
 *              context.valid = Date.now() <= eventRecords[0].startDate;
 *          }
 *      }
 *  });
 * ```
 *
 * ## Preventing drag of certain events
 *
 * To prevent certain events from being dragged, you have two options. You can set {@link Scheduler.model.EventModel#field-draggable}
 * to `false` in your data, or you can listen for the {@link Scheduler.view.Scheduler#event-beforeEventDrag} event and
 * return `false` to block the drag.
 *
 * ```javascript
 * new Scheduler({
 *    listeners : {
 *        beforeEventDrag({ eventRecord }) {
 *            // Don't allow dragging events that have already started
 *            return Date.now() <= eventRecord.startDate;
 *        }
 *    }
 * })
 * ```
 *
 * @extends Scheduler/feature/base/DragBase
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/EventDrag.js
 * @classtype eventDrag
 * @feature
 */
class EventDrag extends DragBase {
  //region Config
  static get $name() {
    return 'EventDrag';
  }
  static get configurable() {
    return {
      /**
       * Template used to generate drag tooltip contents.
       * ```javascript
       * const scheduler = new Scheduler({
       *     features : {
       *         eventDrag : {
       *             dragTipTemplate({eventRecord, startText}) {
       *                 return `${eventRecord.name}: ${startText}`
       *             }
       *         }
       *     }
       * });
       * ```
       * @config {Function} tooltipTemplate
       * @param {Object} data Tooltip data
       * @param {Scheduler.model.EventModel} data.eventRecord
       * @param {Boolean} data.valid Currently over a valid drop target or not
       * @param {Date} data.startDate New start date
       * @param {Date} data.endDate New end date
       * @returns {String}
       */
      /**
       * Set to true to only allow dragging events within the same resource.
       * @member {Boolean} constrainDragToResource
       */
      /**
       * Set to true to only allow dragging events within the same resource.
       * @config {Boolean}
       * @default
       */
      constrainDragToResource: false,
      /**
       * Set to true to only allow dragging events to different resources, and disallow rescheduling by dragging.
       * @member {Boolean} constrainDragToTimeSlot
       */
      /**
       * Set to true to only allow dragging events to different resources, and disallow rescheduling by dragging.
       * @config {Boolean}
       * @default
       */
      constrainDragToTimeSlot: false,
      /**
       * A CSS selector specifying elements outside the scheduler element which are valid drop targets.
       * @config {String}
       */
      externalDropTargetSelector: null,
      /**
       * An empty function by default, but provided so that you can perform custom validation on the item being
       * dragged. This function is called during the drag and drop process and also after the drop is made.
       * Return `true` if the new position is valid, `false` to prevent the drag.
       *
       * ```javascript
       * features : {
       *     eventDrag : {
       *         validatorFn({ eventRecords, newResource }) {
       *             const
       *                 task  = eventRecords[0],
       *                 valid = newResource.role === task.resource.role;
       *
       *             return {
       *                 valid   : newResource.role === task.resource.role,
       *                 message : valid ? '' : 'Resource role does not match required role for this task'
       *             };
       *         }
       *     }
       * }
       * ```
       * @param {Object} context A drag drop context object
       * @param {Date} context.startDate New start date
       * @param {Date} context.endDate New end date
       * @param {Scheduler.model.AssignmentModel[]} context.assignmentRecords Assignment records which were dragged
       * @param {Scheduler.model.EventModel[]} context.eventRecords Event records which were dragged
       * @param {Scheduler.model.ResourceModel} context.newResource New resource record
       * @param {Scheduler.model.EventModel} context.targetEventRecord Currently hovering this event record
       * @param {Event} event The event object
       * @returns {Boolean|Object} `true` if this validation passes, `false` if it does not.
       *
       * Or an object with 2 properties: `valid` -  Boolean `true`/`false` depending on validity,
       * and `message` - String with a custom error message to display when invalid.
       * @config {Function}
       */
      validatorFn: (context, event) => {},
      /**
       * The `this` reference for the validatorFn
       * @config {Object}
       */
      validatorFnThisObj: null,
      /**
       * When the host Scheduler is `{@link Scheduler.view.mixin.EventSelection#config-multiEventSelect}: true`
       * then, there are two modes of dragging *within the same Scheduler*.
       *
       * Non unified means that all selected events are dragged by the same number of resource rows.
       *
       * Unified means that all selected events are collected together and dragged as one, and are all dropped
       * on the same targeted resource row at the same targeted time.
       * @member {Boolean} unifiedDrag
       */
      /**
       * When the host Scheduler is `{@link Scheduler.view.mixin.EventSelection#config-multiEventSelect}: true`
       * then, there are two modes of dragging *within the same Scheduler*.
       *
       * Non unified means that all selected events are dragged by the same number of resource rows.
       *
       * Unified means that all selected events are collected together and dragged as one, and are all dropped
       * on the same targeted resource row at the same targeted time.
       * @config {Boolean}
       * @default false
       */
      unifiedDrag: null,
      /**
       * A hook that allows manipulating the position the drag proxy snaps to. Manipulate the `snapTo` property
       * to alter snap position.
       *
       * ```javascript
       * const scheduler = new Scheduler({
       *     features : {
       *         eventDrag : {
       *             snapToPosition({ eventRecord, snapTo }) {
       *                 if (eventRecord.late) {
       *                     snapTo.x = 400;
       *                 }
       *             }
       *         }
       *     }
       * });
       * ```
       *
       * @config {Function}
       * @param {Object} context
       * @param {Scheduler.model.AssignmentModel} context.assignmentRecord Dragged assignment
       * @param {Scheduler.model.EventModel} context.eventRecord Dragged event
       * @param {Scheduler.model.ResourceModel} context.resourceRecord Currently over this resource
       * @param {Date} context.startDate Start date for current position
       * @param {Date} context.endDate End date for current position
       * @param {Object} context.snapTo
       * @param {Number} context.snapTo.x X to snap to
       * @param {Number} context.snapTo.y Y to snap to
       */
      snapToPosition: null,
      /**
       * A modifier key (CTRL, SHIFT, ALT, META) that when pressed will copy an event instead of moving it. Set to
       * empty string to disable copying
       * @prp {'CTRL'|'ALT'|'SHIFT'|'META'|''}
       * @default
       */
      copyKey: 'SHIFT',
      /**
       * Event can be copied two ways: either by adding new assignment to an existing event ('assignment'), or
       * by copying the event itself ('event'). 'auto' mode will pick 'event' for a single-assignment mode (when
       * event has `resourceId` field) and 'assignment' mode otherwise.
       * @prp {'auto'|'assignment'|'event'}
       * @default
       */
      copyMode: 'auto',
      /**
       * Mode of the current drag drop operation.
       * @member {'move'|'copy'}
       * @readonly
       */
      mode: 'move',
      capitalizedEventName: null
    };
  }
  afterConstruct() {
    this.capitalizedEventName = this.capitalizedEventName || this.client.capitalizedEventName;
    super.afterConstruct(...arguments);
  }
  //endregion
  changeMode(value) {
    const {
      dragData,
      copyMode
    } = this;
    // Do not create assignments in case scheduler doesn't use multiple assignments
    // Do not allow to copy recurring events
    if ((copyMode === 'event' || copyMode === 'auto' || copyMode === 'assignment' && !this.scheduler.eventStore.usesSingleAssignment) && (!dragData || dragData.eventRecords.every(r => !r.isRecurring))) {
      return value;
    }
  }
  updateMode(mode) {
    if (this.dragData) {
      if (mode === 'copy') {
        this.setCopying();
      } else {
        this.setMoving();
      }
      /**
       * Triggered when drag mode is changed, for example when copy key is
       * pressed or released while dragging.
       * @event eventDragModeChange
       * @param {String} mode Drag mode, could be either 'move', 'copy', or 'auto'
       * @on-owner
       */
      this.client.trigger('eventDragModeChange', {
        mode
      });
    }
  }
  setCopying() {
    const {
      dragData
    } = this;
    if (!dragData) {
      return;
    }
    // Check if proxies are added to the DOM by checking if any of them is
    if (!dragData.eventBarCopies.some(el => el.isConnected)) {
      dragData.eventBarCopies.forEach(el => {
        el.classList.add('b-drag-proxy-copy');
        // hidden class can be added by the drag feature if we're dragging event outside
        el.classList.remove('b-hidden');
        dragData.context.grabbedParent.appendChild(el);
        // Mark this node as ignored for the DomSync
        el.retainElement = true;
      });
    } else {
      dragData.eventBarCopies.forEach(el => {
        el.classList.remove('b-hidden');
      });
    }
  }
  setMoving() {
    const {
      dragData
    } = this;
    if (!dragData) {
      return;
    }
    dragData.eventBarCopies.forEach(el => {
      el.classList.add('b-hidden');
    });
  }
  //region Events
  /**
   * Fired on the owning Scheduler to allow implementer to use asynchronous finalization by setting `context.async = true`
   * in the listener, to show a confirmation popup etc.
   * ```javascript
   *  scheduler.on('beforeeventdropfinalize', ({ context }) => {
   *      context.async = true;
   *      setTimeout(() => {
   *          // async code don't forget to call finalize
   *          context.finalize();
   *      }, 1000);
   *  })
   * ```
   *
   * For synchronous one-time validation, simply set `context.valid` to true or false.
   * ```javascript
   *  scheduler.on('beforeeventdropfinalize', ({ context }) => {
   *      context.valid = false;
   *  })
   * ```
   * @event beforeEventDropFinalize
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Object} context
   * @param {Boolean} context.async Set true to not finalize the drag-drop operation immediately (e.g. to wait for user confirmation)
   * @param {Scheduler.model.EventModel[]} context.eventRecords Event records being dragged
   * @param {Scheduler.model.AssignmentModel[]} context.assignmentRecords Assignment records being dragged
   * @param {Scheduler.model.EventModel} context.targetEventRecord Event record for drop target
   * @param {Scheduler.model.ResourceModel} context.newResource Resource record for drop target
   * @param {Boolean} context.valid Set this to `false` to abort the drop immediately.
   * @param {Function} context.finalize Call this method after an **async** finalization flow, to finalize the drag-drop operation. This method accepts one
   * argument: pass `true` to update records, or `false` to ignore changes
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler after event drop
   * @event afterEventDrop
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
   * @param {Scheduler.model.EventModel[]} eventRecords
   * @param {Boolean} valid
   * @param {Object} context
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler when an event is dropped
   * @event eventDrop
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.EventModel[]} eventRecords
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
   * @param {HTMLElement} externalDropTarget The HTML element dropped upon, if drop happened on a valid external drop target
   * @param {Boolean} isCopy
   * @param {Object} context
   * @param {Scheduler.model.EventModel} context.targetEventRecord Event record for drop target
   * @param {Scheduler.model.ResourceModel} context.newResource Resource record for drop target
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler before event dragging starts. Return `false` to prevent the action.
   * @event beforeEventDrag
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel} eventRecord Event record the drag starts from
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag starts from
   * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
   * @param {MouseEvent} event Browser event DEPRECATED (replaced by domEvent)
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler when event dragging starts
   * @event eventDragStart
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag starts from
   * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
   * @param {MouseEvent} event Browser event DEPRECATED (replaced by domEvent)
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler when event is dragged
   * @event eventDrag
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
   * @param {Date} startDate Start date for the current location
   * @param {Date} endDate End date for the current location
   * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag started from
   * @param {Scheduler.model.ResourceModel} newResource Resource at the current location
   * @param {Object} context
   * @param {Boolean} context.valid Set this to `false` to signal that the current drop position is invalid.
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler after an event drag operation has been aborted
   * @event eventDragAbort
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
   * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
   * @param {MouseEvent} domEvent Browser event
   */
  /**
   * Fired on the owning Scheduler after an event drag operation regardless of the operation being cancelled or not
   * @event eventDragReset
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   */
  //endregion
  //region Data layer
  // Deprecated. Use this.client instead
  get scheduler() {
    return this.client;
  }
  //endregion
  //#region Drag lifecycle
  onAfterDragStart(event) {
    const me = this,
      {
        context: {
          element
        }
      } = event;
    super.onAfterDragStart(event);
    me.handleKeyDownOrMove(event.event);
    me.keyEventDetacher = EventHelper.on({
      // In case we drag event between scheduler focused event gets moved and focus
      // moves to the body. We only need to read the key from this event
      element: DomHelper.getRootElement(element),
      keydown: me.handleKeyDownOrMove,
      keyup: me.handleKeyUp,
      thisObj: me
    });
  }
  onDragReset(event) {
    var _this$keyEventDetache;
    super.onDragReset(event);
    (_this$keyEventDetache = this.keyEventDetacher) === null || _this$keyEventDetache === void 0 ? void 0 : _this$keyEventDetache.call(this);
    this.mode = 'move';
  }
  onDrop(event) {
    var _this$dragData$eventB;
    // Always remove proxy on drop
    (_this$dragData$eventB = this.dragData.eventBarCopies) === null || _this$dragData$eventB === void 0 ? void 0 : _this$dragData$eventB.forEach(el => el.remove());
    return super.onDrop(event);
  }
  //#endregion
  //region Drag events
  getDraggableElement(el) {
    return el === null || el === void 0 ? void 0 : el.closest(this.drag.targetSelector);
  }
  resolveEventRecord(eventElement, client = this.client) {
    return client.resolveEventRecord(eventElement);
  }
  isElementDraggable(el, event) {
    var _client;
    const me = this,
      {
        client
      } = me,
      eventElement = me.getDraggableElement(el);
    if (!eventElement || me.disabled || client.readOnly) {
      return false;
    }
    // displaying something resizable within the event?
    if (el.matches('[class$="-handle"]')) {
      return false;
    }
    const eventRecord = me.resolveEventRecord(eventElement, client);
    if (!eventRecord || !eventRecord.isDraggable || eventRecord.readOnly) {
      return false;
    }
    // Hook for features that need to prevent drag
    const prevented = ((_client = client[`is${me.capitalizedEventName}ElementDraggable`]) === null || _client === void 0 ? void 0 : _client.call(client, eventElement, eventRecord, el, event)) === false;
    return !prevented;
  }
  getTriggerParams(dragData) {
    const {
      assignmentRecords,
      eventRecords,
      resourceRecord,
      browserEvent: domEvent
    } = dragData;
    return {
      // `context` is now private, but used in WebSocketHelper
      context: dragData,
      eventRecords,
      resourceRecord,
      assignmentRecords,
      event: domEvent,
      // Deprecated, remove on  6.0?
      domEvent
    };
  }
  triggerBeforeEventDrag(eventType, event) {
    return this.client.trigger(eventType, event);
  }
  triggerEventDrag(dragData, start) {
    this.client.trigger('eventDrag', Object.assign(this.getTriggerParams(dragData), {
      startDate: dragData.startDate,
      endDate: dragData.endDate,
      newResource: dragData.newResource
    }));
  }
  triggerDragStart(dragData) {
    this.client.navigator.skipNextClick = true;
    this.client.trigger('eventDragStart', this.getTriggerParams(dragData));
  }
  triggerDragAbort(dragData) {
    this.client.trigger('eventDragAbort', this.getTriggerParams(dragData));
  }
  triggerDragAbortFinalized(dragData) {
    this.client.trigger('eventDragAbortFinalized', this.getTriggerParams(dragData));
  }
  triggerAfterDrop(dragData, valid) {
    const me = this;
    me.currentOverClient.trigger('afterEventDrop', Object.assign(me.getTriggerParams(dragData), {
      valid
    }));
    if (!valid) {
      // Edge cases:
      // 1. If this drag was a no-op, and underlying data was changed while drag was ongoing (e.g. web socket
      // push), we need to manually force a view refresh to ensure a correct render state
      //
      // or
      // 2. Events were removed before we dropped at an invalid point
      const {
          assignmentStore,
          eventStore
        } = me.client,
        needRefresh = me.dragData.initialAssignmentsState.find(({
          resource,
          assignment
        }, i) => {
          var _me$dragData$assignme;
          return !assignmentStore.includes(assignment) || !eventStore.includes(assignment.event) || resource.id !== ((_me$dragData$assignme = me.dragData.assignmentRecords[i]) === null || _me$dragData$assignme === void 0 ? void 0 : _me$dragData$assignme.resourceId);
        });
      if (needRefresh) {
        me.client.refresh();
      }
    }
    // Reset the skipNextClick after a potential click event fires. https://github.com/bryntum/support/issues/5135
    me.client.setTimeout(() => me.client.navigator.skipNextClick = false, 10);
  }
  handleKeyDownOrMove(event) {
    if (this.mode !== 'copy') {
      var _this$copyKey, _this$copyKey2;
      if (event.key && EventHelper.specialKeyFromEventKey(event.key) === ((_this$copyKey = this.copyKey) === null || _this$copyKey === void 0 ? void 0 : _this$copyKey.toLowerCase()) || event[`${(_this$copyKey2 = this.copyKey) === null || _this$copyKey2 === void 0 ? void 0 : _this$copyKey2.toLowerCase()}Key`]) {
        this.mode = 'copy';
      }
    }
  }
  handleKeyUp(event) {
    if (EventHelper.specialKeyFromEventKey(event.key) === this.copyKey.toLowerCase()) {
      this.mode = 'move';
    }
  }
  //endregion
  //region Finalization & validation
  /**
   * Checks if an event can be dropped on the specified position.
   * @private
   * @returns {Boolean} Valid (true) or invalid (false)
   */
  isValidDrop(dragData) {
    const {
        newResource,
        resourceRecord,
        browserEvent
      } = dragData,
      sourceRecord = dragData.draggedEntities[0],
      {
        target
      } = browserEvent;
    // Only allowed to drop outside scheduler element if we hit an element matching the externalDropTargetSelector
    if (!newResource) {
      return !this.constrainDragToTimeline && this.externalDropTargetSelector ? Boolean(target.closest(this.externalDropTargetSelector)) : false;
    }
    // Not allowed to drop an event on a group header or a readOnly resource
    if (newResource.isSpecialRow || newResource.readOnly) {
      return false;
    }
    // Not allowed to assign an event twice to the same resource
    if (resourceRecord !== newResource) {
      return !sourceRecord.event.resources.includes(newResource);
    }
    return true;
  }
  checkDragValidity(dragData, event) {
    var _dragData$newResource;
    const me = this,
      scheduler = me.currentOverClient;
    let result;
    // Cannot assign anything to readOnly resources
    if ((_dragData$newResource = dragData.newResource) !== null && _dragData$newResource !== void 0 && _dragData$newResource.readOnly) {
      return false;
    }
    // First make sure there's no overlap, if not run the external validatorFn
    if (!scheduler.allowOverlap && !scheduler.isDateRangeAvailable(dragData.startDate, dragData.endDate, dragData.draggedEntities[0], dragData.newResource)) {
      result = {
        valid: false,
        message: me.L('L{eventOverlapsExisting}')
      };
    } else {
      result = me.validatorFn.call(me.validatorFnThisObj || me, dragData, event);
    }
    if (!result || result.valid) {
      var _scheduler$checkEvent;
      // Hook for features to have a say on validity
      result = ((_scheduler$checkEvent = scheduler['checkEventDragValidity']) === null || _scheduler$checkEvent === void 0 ? void 0 : _scheduler$checkEvent.call(scheduler, dragData, event)) ?? result;
    }
    return result;
  }
  //endregion
  //region Update records
  /**
   * Update events being dragged.
   * @private
   * @param context Drag data.
   */
  async updateRecords(context) {
    const me = this,
      fromScheduler = me.client,
      toScheduler = me.currentOverClient,
      copyKeyPressed = me.mode === 'copy',
      {
        draggedEntities,
        timeDiff,
        initialAssignmentsState
      } = context,
      originalStartDate = initialAssignmentsState[0].startDate,
      droppedStartDate = me.adjustStartDate(originalStartDate, timeDiff);
    let result;
    if (!context.externalDropTarget) {
      // Dropping dragged event completely outside the time axis is not allowed
      if (!toScheduler.timeAxis.timeSpanInAxis(droppedStartDate, DateHelper.add(droppedStartDate, draggedEntities[0].event.durationMS, 'ms'))) {
        context.valid = false;
      }
      if (context.valid) {
        fromScheduler.eventStore.suspendAutoCommit();
        toScheduler.eventStore.suspendAutoCommit();
        result = await me.updateAssignments(fromScheduler, toScheduler, context, copyKeyPressed);
        fromScheduler.eventStore.resumeAutoCommit();
        toScheduler.eventStore.resumeAutoCommit();
      }
    }
    // Might be flagged invalid in updateAssignments() above, if drop did not lead to any change
    // (for example if dropped on non-working-time in Pro)
    if (context.valid) {
      // Tell the world there was a successful drop
      toScheduler.trigger('eventDrop', Object.assign(me.getTriggerParams(context), {
        isCopy: copyKeyPressed,
        copyMode: me.copyMode,
        domEvent: context.browserEvent,
        targetEventRecord: context.targetEventRecord,
        targetResourceRecord: context.newResource,
        externalDropTarget: context.externalDropTarget
      }));
    }
    return result;
  }
  /**
   * Update assignments being dragged
   * @private
   */
  async updateAssignments(fromScheduler, toScheduler, context, copy) {
    // The code is written to emit as few store events as possible
    const me = this,
      {
        copyMode
      } = me,
      isCrossScheduler = fromScheduler !== toScheduler,
      {
        isVertical
      } = toScheduler,
      {
        assignmentStore: fromAssignmentStore,
        eventStore: fromEventStore
      } = fromScheduler,
      {
        assignmentStore: toAssignmentStore,
        eventStore: toEventStore
      } = toScheduler,
      // When using TreeGroup in horizontal mode, store != resourceStore. Does not apply for vertical mode.
      fromResourceStore = fromScheduler.isVertical ? fromScheduler.resourceStore : fromScheduler.store,
      toResourceStore = isVertical ? toScheduler.resourceStore : toScheduler.store,
      {
        eventRecords,
        assignmentRecords,
        timeDiff,
        initialAssignmentsState,
        resourceRecord: fromResource,
        newResource: toResource
      } = context,
      {
        unifiedDrag
      } = me,
      // For an empty target event store, check if it has usesSingleAssignment explicitly set, otherwise use
      // the value from the source event store
      useSingleAssignment = toEventStore.usesSingleAssignment || toEventStore.usesSingleAssignment !== false && fromEventStore.usesSingleAssignment,
      // this value has clear semantic only for same scheduler case
      effectiveCopyMode = copyMode === 'event' ? 'event' : copyMode === 'assignment' ? 'assignment' : useSingleAssignment ? 'event' : 'assignment',
      event1Date = me.adjustStartDate(assignmentRecords[0].event.startDate, timeDiff),
      eventsToAdd = [],
      eventsToRemove = [],
      assignmentsToAdd = [],
      assignmentsToRemove = [],
      eventsToCheck = [],
      eventsToBatch = new Set(),
      resourcesInStore = fromResourceStore.getAllDataRecords();
    fromScheduler.suspendRefresh();
    toScheduler.suspendRefresh();
    let updated = false,
      updatedEvent = false,
      indexDiff; // By how many resource rows has the drag moved.
    if (isCrossScheduler) {
      // The difference in indices via first dragged event will help us find resources for all the rest of the
      // events accordingly
      indexDiff = toResourceStore.indexOf(toResource) - fromResourceStore.indexOf(fromResource);
    } else if (me.constainDragToResource) {
      indexDiff = 0;
    } else if (isVertical && toResourceStore.isGrouped) {
      indexDiff = resourcesInStore.indexOf(fromResource) - resourcesInStore.indexOf(toResource);
    } else {
      indexDiff = fromResourceStore.indexOf(fromResource) - fromResourceStore.indexOf(toResource);
    }
    if (isVertical) {
      eventRecords.forEach((draggedEvent, i) => {
        const eventBar = context.eventBarEls[i];
        delete draggedEvent.instanceMeta(fromScheduler).hasTemporaryDragElement;
        // If it was created by a call to scheduler.currentOrientation.addTemporaryDragElement
        // then release it back to be available to DomSync next time the rendered event block
        // is synced.
        if (eventBar.dataset.transient) {
          eventBar.remove();
        }
      });
    }
    const eventBarEls = context.eventBarEls.slice(),
      addedEvents = [],
      // this map holds references between original assignment and its copy
      copiedAssignmentsMap = {};
    // Using for to support await inside
    for (let i = 0; i < assignmentRecords.length; i++) {
      const originalAssignment = assignmentRecords[i];
      // Reassigned when dropped on other scheduler, thus not const
      let draggedEvent = originalAssignment.event,
        draggedAssignment;
      if (copy) {
        draggedAssignment = originalAssignment.copy();
        copiedAssignmentsMap[originalAssignment.id] = draggedAssignment;
      } else {
        draggedAssignment = originalAssignment;
      }
      if (!draggedAssignment.isOccurrenceAssignment && (!fromAssignmentStore.includes(originalAssignment) || !fromEventStore.includes(draggedEvent))) {
        // Event was removed externally during the drag, just remove element from DOM (DomSync already has
        // tried to clean it up at this point, but could not due to retainElement being set)
        eventBarEls[i].remove();
        eventBarEls.splice(i, 1);
        assignmentRecords.splice(i, 1);
        i--;
        continue;
      }
      const initialState = initialAssignmentsState[i],
        originalEventRecord = draggedEvent,
        originalStartDate = initialState.startDate,
        // grabbing resource early, since after ".copy()" the record won't belong to any store
        // and ".getResources()" won't work. If it's a move to another scheduler, ensure the
        // array still has a length. The process function will do an assign as opposed
        // to a reassignment
        originalResourceRecord = initialState.resource,
        // Calculate new startDate (and round it) based on timeDiff up here, might be added to another
        // event store below in which case it is invalidated. But this is anyway the target date
        newStartDate = this.constrainDragToTimeSlot ? originalStartDate : unifiedDrag ? event1Date : me.adjustStartDate(originalStartDate, timeDiff);
      if (fromAssignmentStore !== toAssignmentStore) {
        // Single assignment from a multi assigned event dragged over, event needs to be copied over
        // Same if we hold the copy key
        const keepEvent = originalEventRecord.assignments.length > 1 || copy;
        let newAssignment;
        if (copy) {
          // In a copy mode dragged assignment is already a copy
          newAssignment = draggedAssignment;
        } else {
          newAssignment = draggedAssignment.copy();
          copiedAssignmentsMap[draggedAssignment.id] = newAssignment;
        }
        // Pro Engine does not seem to handle having the event already in place on the copied assignment,
        // replacing it with id to have events bucket properly set up on commit
        if (newAssignment.event && !useSingleAssignment) {
          newAssignment.event = newAssignment.event.id;
          newAssignment.resource = newAssignment.resource.id;
        }
        if (!copy) {
          // If we're not copying, remove assignment from source scheduler
          assignmentsToRemove.push(draggedAssignment);
        }
        // If it was the last assignment, the event should also be removed
        if (!keepEvent) {
          eventsToRemove.push(originalEventRecord);
        }
        // If event does not already exist in target scheduler a copy is added
        // if we're copying the event, we always need to create new record
        if (copy && (copyMode === 'event' || copyMode === 'auto' && toEventStore.usesSingleAssignment) || !toEventStore.getById(originalEventRecord.id)) {
          draggedEvent = toEventStore.createRecord({
            ...originalEventRecord.data,
            // If we're copying the event (not making new assignment to existing), we need to generate
            // phantom id to link event to the assignment record
            id: copy && (copyMode === 'event' || copyMode === 'auto') ? undefined : originalEventRecord.id,
            // Engine gets mad if not nulled
            calendar: null
          });
          newAssignment.set({
            eventId: draggedEvent.id,
            event: draggedEvent
          });
          eventsToAdd.push(draggedEvent);
        }
        // And add it to the target scheduler
        if (!useSingleAssignment) {
          assignmentsToAdd.push(newAssignment);
        }
        draggedAssignment = newAssignment;
      }
      let newResource = toResource,
        reassignedFrom = null;
      if (!unifiedDrag) {
        if (!isCrossScheduler) {
          // If not dragging events as a unified block, distribute each to a new resource
          // using the same offset as the dragged event.
          if (indexDiff !== 0) {
            var _newResource;
            let newIndex;
            if (isVertical && toResourceStore.isGrouped) {
              newIndex = Math.max(Math.min(resourcesInStore.indexOf(originalResourceRecord) - indexDiff, resourcesInStore.length - 1), 0);
              newResource = resourcesInStore[newIndex];
            } else {
              newIndex = Math.max(Math.min(fromResourceStore.indexOf(originalResourceRecord) - indexDiff, fromResourceStore.count - 1), 0);
              newResource = fromResourceStore.getAt(newIndex);
              // Exclude group headers, footers, summary row etc
              if (newResource.isSpecialRow) {
                newResource = fromResourceStore.getNext(newResource, false, true) || fromResourceStore.getPrevious(newResource, false, true);
              }
            }
            newResource = (_newResource = newResource) === null || _newResource === void 0 ? void 0 : _newResource.$original;
          } else {
            newResource = originalResourceRecord;
          }
        }
        // we have a resource for first dragged event in toResource
        else if (i > 0) {
          const draggedEventResourceIndex = fromResourceStore.indexOf(originalResourceRecord);
          newResource = toResourceStore.getAt(draggedEventResourceIndex + indexDiff) || newResource;
        }
      }
      const isCrossResource = draggedAssignment.resourceId !== newResource.id;
      // Cannot rely on assignment generation to detect update, since it might be a new assignment
      if (isCrossResource) {
        reassignedFrom = fromResourceStore.getById(draggedAssignment.resourceId);
        if (copy && fromAssignmentStore === toAssignmentStore) {
          // Scheduler Core patch
          // need to completely clear the resource/resourceId on the copied assignment, before setting the new
          // otherwise, what happens is that in the `$beforeChange.resource/Id` are still
          // stored the resource/Id of the original assignment
          // then, when finalizing commit, Core engine performs this:
          //     // First silently revert any data change (used by buckets), otherwise it won't be detected by `set()`
          //     me.setData(me.$beforeChange)
          // and then updates the data to new, which is recorded as UpdateAction in the STM with old/new data
          // then, when that update action in STM is undo-ed, the old data is written back to the record
          // and newly added assignment is pointing to the old resource
          // then, when STM action is redo-ed, a "duplicate assignment" exception is thrown
          // this is covered with the test:
          // Scheduler/tests/features/EventDragCopy.t.js -> Should not remove the original when undo-ing the copy-drag action ("multi-assignment")
          draggedAssignment.setData({
            resource: null,
            resourceId: null
          });
          // eof Scheduler Core patch
          draggedAssignment.resource = newResource;
          draggedAssignment.event = toEventStore.getById(draggedAssignment.eventId);
          const shouldCopyEvent = copyMode === 'event' || fromEventStore.usesSingleAssignment && copyMode === 'auto';
          if (shouldCopyEvent) {
            draggedEvent = draggedEvent.copy();
            // need to clear the `endDate` of the copy
            // this is because when we drag the copy to a different position on the timeline
            // it will set the new start date and re-calculate end date
            // as a result, in STM transaction for this drag-copy there will be "add" action
            // and "update" action and NO COMMIT in the middle
            // so when re-doing this transaction the duration change is lost
            // this is covered with the test:
            // "Scheduler/tests/features/EventDragCopy.t.js -> Should not remove the original when undo-ing the copy-drag action (usesSingleAssignment)",
            // Before doing it, save a copy of endDate in meta object, considering timeDiff: that's because below it will check if event is in timeAxis.
            draggedEvent.meta.endDateCached = me.adjustStartDate(draggedEvent.endDate, timeDiff);
            draggedEvent.endDate = null;
            draggedAssignment.event = draggedEvent;
            if (toEventStore.usesSingleAssignment) {
              draggedEvent.resource = newResource;
              draggedEvent.resourceId = newResource.id;
            }
          }
          if (!toAssignmentStore.find(a => a.eventId === draggedAssignment.eventId && a.resourceId === draggedAssignment.resourceId) && !assignmentsToAdd.find(r => r.eventId === draggedAssignment.eventId && r.resourceId === draggedAssignment.resourceId)) {
            shouldCopyEvent && eventsToAdd.push(draggedEvent);
            assignmentsToAdd.push(draggedAssignment);
          }
        } else {
          draggedAssignment.resource = newResource;
        }
        // Actual events should be batched, not data for new events when dragging between
        draggedEvent.isEvent && eventsToBatch.add(draggedEvent);
        updated = true;
        // When dragging an occurrence, the assignment is only temporary. We have to tag the newResource along
        // to be picked up by the occurrence -> event conversion
        if (draggedEvent.isOccurrence) {
          draggedEvent.set('newResource', newResource);
        }
        if (isCrossScheduler && useSingleAssignment) {
          // In single assignment mode, when dragged to another scheduler it will not copy the assignment
          // over but instead set the resourceId of the event. To better match expected behaviour
          draggedEvent.resourceId = newResource.id;
        }
      } else {
        if (copy && (copyMode === 'event' || copyMode === 'auto' && fromEventStore.usesSingleAssignment) && !eventsToAdd.includes(draggedEvent)) {
          draggedEvent = draggedEvent.copy();
          // see the comment above
          draggedEvent.meta.endDateCached = me.adjustStartDate(draggedEvent.endDate, timeDiff);
          draggedEvent.endDate = null;
          eventsToAdd.push(draggedEvent);
          draggedAssignment.event = draggedEvent;
          if (toEventStore.usesSingleAssignment) {
            draggedEvent.set({
              resource: newResource,
              resourceId: newResource.id
            });
          }
          // Always add assignment to the store to allow proper element reuse
          assignmentsToAdd.push(draggedAssignment);
        }
      }
      // Same for event
      if (!eventsToCheck.find(ev => ev.draggedEvent === draggedEvent) && !DateHelper.isEqual(draggedEvent.startDate, newStartDate)) {
        // only do for non occurence records
        while (!draggedEvent.isOccurrence && draggedEvent.isBatchUpdating) {
          draggedEvent.endBatch(true);
        }
        // for same scheduler with multi-assignments, and copyMode === assignment, need to keep the start date
        // because user intention is to create a new assignment, not re-schedule the event
        // but only for cross-resource dragging, same resource dragging has semantic of regular drag
        const shouldKeepStartDate = copy && !isCrossScheduler && !useSingleAssignment && effectiveCopyMode === 'assignment' && isCrossResource;
        if (!shouldKeepStartDate) {
          draggedEvent.startDate = newStartDate;
          eventsToCheck.push({
            draggedEvent,
            originalStartDate
          });
        }
        draggedEvent.isEvent && eventsToBatch.add(draggedEvent);
        updatedEvent = true;
      }
      // Hook for features that need to do additional processing on drop (used by NestedEvents)
      toScheduler.processEventDrop({
        eventRecord: draggedEvent,
        resourceRecord: newResource,
        element: i === 0 ? context.context.element : context.context.relatedElements[i - 1],
        context,
        toScheduler,
        reassignedFrom,
        eventsToAdd,
        addedEvents,
        draggedAssignment
      });
      // There are two cases to consider when triggering this event - `copy` and `move` mode. In case we are
      // copying the assignment (we can also copy the event) draggedAssignment will point to the copy of the
      // original assignment record. Same for draggedEvent. These records are new records which are not yet added
      // to the store and they contain correct state of the drop - which event is going to be assigned to which
      // resource on what time.
      // These records possess no knowledge about original records which they were cloned from. And that might be
      // useful. Let's say you want to copy assignment (or event) to every row in the way. You need to know start
      // row and the end row. That information is kept in the `originalAssignment` record. Which might be identical
      // to the `draggedAssignment` record in `move` mode.
      toScheduler.trigger('processEventDrop', {
        originalAssignment,
        draggedAssignment,
        context,
        copyMode,
        isCopy: copy
      });
    }
    fromAssignmentStore.remove(assignmentsToRemove);
    fromEventStore.remove(eventsToRemove);
    toAssignmentStore.add(assignmentsToAdd);
    // Modify syncIdMap on the FGCanvas to make sure elements get animated nicely to new position
    if (copy && fromAssignmentStore === toAssignmentStore) {
      const {
        syncIdMap
      } = fromScheduler.foregroundCanvas;
      Object.entries(copiedAssignmentsMap).forEach(([originalId, cloneRecord]) => {
        const element = syncIdMap[originalId];
        delete syncIdMap[originalId];
        syncIdMap[cloneRecord.id] = element;
      });
    }
    eventsToAdd.length && addedEvents.push(...toEventStore.add(eventsToAdd));
    // When not constrained to timeline we are dragging a clone and need to manually do some cleanup if
    // dropped in view
    if (!me.constrainDragToTimeline) {
      // go through assignmentRecords again after events has been added to toEventStore (if any)
      // now we have updated assignment ids and can properly reuse event HTML elements
      for (let i = 0; i < assignmentRecords.length; i++) {
        const assignmentRecord = copiedAssignmentsMap[assignmentRecords[i].id] || assignmentRecords[i],
          originalDraggedEvent = assignmentRecord.event,
          // try to get dragged event from addedEvents array, it will be there with updated ids
          // if toScheduler is different
          draggedEvent = (addedEvents === null || addedEvents === void 0 ? void 0 : addedEvents.find(r => r.id === originalDraggedEvent.id)) || originalDraggedEvent,
          eventBar = context.eventBarEls[i],
          element = i === 0 ? context.context.element : context.context.relatedElements[i - 1],
          // Determine if in time axis here also, since the records date might be invalidated further below
          inTimeAxis = toScheduler.isInTimeAxis(draggedEvent);
        // after checking if is in time axis, imeta.endDateCached can be deleted
        delete draggedEvent.meta.endDateCached;
        if (!copy) {
          // Remove original element properly
          DomSync.removeChild(eventBar.parentElement, eventBar);
        }
        if (draggedEvent.resource && (isVertical || toScheduler.rowManager.getRowFor(draggedEvent.resource)) && inTimeAxis) {
          // Nested events are added to correct parent by the feature
          if (!draggedEvent.parent || draggedEvent.parent.isRoot) {
            const elRect = Rectangle.from(element, toScheduler.foregroundCanvas, true);
            // Ensure that after inserting the dragged element clone into the toScheduler's foregroundCanvas
            // it's at the same visual position that it was dragged to.
            DomHelper.setTopLeft(element, elRect.y, elRect.x);
            // Add element properly, so that DomSync will reuse it on next update
            DomSync.addChild(toScheduler.foregroundCanvas, element, draggedEvent.assignments[0].id);
            isCrossScheduler && toScheduler.processCrossSchedulerEventDrop({
              eventRecord: draggedEvent,
              toScheduler
            });
          }
          element.classList.remove('b-sch-event-hover', 'b-active', 'b-drag-proxy', 'b-dragging');
          element.retainElement = false;
        }
      }
    }
    addedEvents === null || addedEvents === void 0 ? void 0 : addedEvents.forEach(added => eventsToBatch.add(added));
    // addedEvents order is the same with [context.element, ..context.relatedElements]
    // Any added or removed events or assignments => something changed
    if (assignmentsToRemove.length || eventsToRemove.length || assignmentsToAdd.length || eventsToAdd.length) {
      updated = true;
    }
    // Commit changes to affected projects
    if (updated || updatedEvent) {
      // By batching event changes when using single assignment we avoid two updates, without it there will be one
      // for date change and one when changed assignment updates resourceId on the event
      useSingleAssignment && eventsToBatch.forEach(eventRecord => eventRecord.beginBatch());
      await Promise.all([toScheduler.project !== fromScheduler.project ? toScheduler.project.commitAsync() : null, fromScheduler.project.commitAsync()]);
      // End batch in engine friendly way, avoiding to have `set()` trigger another round of calculations
      useSingleAssignment && eventsToBatch.forEach(eventRecord => eventRecord.endBatch(false, true));
    }
    if (!updated) {
      // Engine might have reverted the date change, in which case this should be considered an invalid op
      updated = eventsToCheck.some(({
        draggedEvent,
        originalStartDate
      }) => !DateHelper.isEqual(draggedEvent.startDate, originalStartDate));
    }
    // Resumes self twice if not cross scheduler, but was suspended twice above also so all good
    toScheduler.resumeRefresh();
    fromScheduler.resumeRefresh();
    if (assignmentRecords.length > 0) {
      if (!updated) {
        context.valid = false;
      } else {
        // Always force re-render of the bars, to return them to their original position when:
        // * Fill ticks leading to small date adjustment not actually changing the DOM
        //   (https://github.com/bryntum/support/issues/630)
        // * Dragging straight down with multiselection, events in the last resource will still be assigned to
        //   that resource = no change in the DOM (https://github.com/bryntum/support/issues/6293)
        eventBarEls.forEach(el => delete el.lastDomConfig);
        // Not doing full refresh above, to allow for animations
        toScheduler.refreshWithTransition();
        if (isCrossScheduler) {
          fromScheduler.refreshWithTransition();
          toScheduler.selectedEvents = addedEvents;
        }
      }
    }
  }
  //endregion
  //region Drag data
  getProductDragContext(dragData) {
    const me = this,
      {
        currentOverClient: scheduler
      } = me,
      target = dragData.browserEvent.target,
      previousResolvedResource = dragData.newResource || dragData.resourceRecord,
      previousTargetEventRecord = dragData.targetEventRecord;
    let targetEventRecord = scheduler ? me.resolveEventRecord(target, scheduler) : null,
      newResource,
      externalDropTarget;
    // Ignore if over dragged event
    if (dragData.eventRecords.includes(targetEventRecord)) {
      targetEventRecord = null;
    }
    if (me.constrainDragToResource) {
      newResource = dragData.resourceRecord;
    } else if (!me.constrainDragToTimeline) {
      newResource = me.resolveResource();
    } else if (scheduler) {
      newResource = me.resolveResource() || dragData.newResource || dragData.resourceRecord;
    }
    const {
        assignmentRecords,
        eventRecords
      } = dragData,
      isOverNewResource = previousResolvedResource !== newResource;
    let valid = Boolean(newResource && !newResource.isSpecialRow);
    if (!newResource && me.externalDropTargetSelector) {
      externalDropTarget = target.closest(me.externalDropTargetSelector);
      valid = Boolean(externalDropTarget);
    }
    return {
      valid,
      externalDropTarget,
      eventRecords,
      assignmentRecords,
      newResource,
      targetEventRecord,
      dirty: isOverNewResource || targetEventRecord !== previousTargetEventRecord,
      proxyElements: [dragData.context.element, ...(dragData.context.relatedElements || [])]
    };
  }
  getMinimalDragData(info) {
    const me = this,
      {
        scheduler
      } = me,
      element = me.getElementFromContext(info),
      eventRecord = me.resolveEventRecord(element, scheduler),
      resourceRecord = scheduler.resolveResourceRecord(element),
      assignmentRecord = scheduler.resolveAssignmentRecord(element),
      assignmentRecords = assignmentRecord ? [assignmentRecord] : [];
    // We multi drag other selected events if the dragged event is already selected, or the ctrl key is pressed
    if (assignmentRecord && (scheduler.isAssignmentSelected(assignmentRecords[0]) || me.drag.startEvent.ctrlKey && scheduler.multiEventSelect)) {
      assignmentRecords.push.apply(assignmentRecords, me.getRelatedRecords(assignmentRecord));
    }
    const eventRecords = [...new Set(assignmentRecords.map(assignment => assignment.event))];
    return {
      eventRecord,
      resourceRecord,
      assignmentRecord,
      eventRecords,
      assignmentRecords
    };
  }
  setupProductDragData(info) {
    var _dateConstraints;
    const me = this,
      {
        scheduler
      } = me,
      element = me.getElementFromContext(info),
      {
        eventRecord,
        resourceRecord,
        assignmentRecord,
        assignmentRecords
      } = me.getMinimalDragData(info),
      eventBarEls = [];
    if (me.constrainDragToResource && !resourceRecord) {
      throw new Error('Resource could not be resolved for event: ' + eventRecord.id);
    }
    let dateConstraints;
    if (me.constrainDragToTimeline) {
      var _me$getDateConstraint;
      dateConstraints = (_me$getDateConstraint = me.getDateConstraints) === null || _me$getDateConstraint === void 0 ? void 0 : _me$getDateConstraint.call(me, resourceRecord, eventRecord);
      const constrainRectangle = me.constrainRectangle = me.getConstrainingRectangle(dateConstraints, resourceRecord, eventRecord),
        eventRegion = Rectangle.from(element, scheduler.timeAxisSubGridElement);
      super.setupConstraints(constrainRectangle, eventRegion, scheduler.timeAxisViewModel.snapPixelAmount, Boolean(dateConstraints.start));
    }
    // Collecting all elements to drag
    assignmentRecords.forEach(assignment => {
      let eventBarEl = scheduler.getElementFromAssignmentRecord(assignment, true);
      if (!eventBarEl) {
        eventBarEl = scheduler.currentOrientation.addTemporaryDragElement(assignment.event, assignment.resource);
      }
      eventBarEls.push(eventBarEl);
    });
    return {
      record: assignmentRecord,
      draggedEntities: assignmentRecords,
      dateConstraints: (_dateConstraints = dateConstraints) !== null && _dateConstraints !== void 0 && _dateConstraints.start ? dateConstraints : null,
      // Create copies of the elements
      eventBarCopies: eventBarEls.map(el => me.createProxy(el)),
      eventBarEls
    };
  }
  getDateConstraints(resourceRecord, eventRecord) {
    var _scheduler$getDateCon;
    const {
        scheduler
      } = this,
      externalDateConstraints = (_scheduler$getDateCon = scheduler.getDateConstraints) === null || _scheduler$getDateCon === void 0 ? void 0 : _scheduler$getDateCon.call(scheduler, resourceRecord, eventRecord);
    let minDate, maxDate;
    if (this.constrainDragToTimeSlot) {
      minDate = eventRecord.startDate;
      maxDate = eventRecord.endDate;
    } else if (externalDateConstraints) {
      minDate = externalDateConstraints.start;
      maxDate = externalDateConstraints.end;
    }
    return {
      start: minDate,
      end: maxDate
    };
  }
  getConstrainingRectangle(dateRange, resourceRecord, eventRecord) {
    return this.scheduler.getScheduleRegion(this.constrainDragToResource && resourceRecord, eventRecord, true, dateRange && {
      start: dateRange.start,
      end: dateRange.end
    });
  }
  /**
   * Initializes drag data (dates, constraints, dragged events etc). Called when drag starts.
   * @private
   * @param info
   * @returns {*}
   */
  getDragData(info) {
    const dragData = this.getMinimalDragData(info) || {};
    return {
      ...super.getDragData(info),
      ...dragData,
      initialAssignmentsState: dragData.assignmentRecords.map(assignment => ({
        startDate: assignment.event.startDate,
        resource: assignment.resource,
        assignment
      }))
    };
  }
  /**
   * Provide your custom implementation of this to allow additional selected records to be dragged together with the original one.
   * @param {Scheduler.model.AssignmentModel} assignmentRecord The assignment about to be dragged
   * @returns {Scheduler.model.AssignmentModel[]} An array of assignment records to drag together with the original
   */
  getRelatedRecords(assignmentRecord) {
    return this.scheduler.selectedAssignments.filter(selectedRecord => selectedRecord !== assignmentRecord && !selectedRecord.resource.readOnly && selectedRecord.event.isDraggable);
  }
  /**
   * Get correct axis coordinate depending on schedulers mode (horizontal -> x, vertical -> y). Also takes milestone
   * layout into account.
   * @private
   * @param {Scheduler.model.EventModel} eventRecord Record being dragged
   * @param {HTMLElement} element Element being dragged
   * @param {Number[]} coord XY coordinates
   * @returns {Number|Number[]} X,Y or XY
   */
  getCoordinate(eventRecord, element, coord) {
    const scheduler = this.currentOverClient;
    if (scheduler.isHorizontal) {
      let x = coord[0];
      // Adjust coordinate for milestones if using a layout mode, since they are aligned differently than events
      if (scheduler.milestoneLayoutMode !== 'default' && eventRecord.isMilestone) {
        switch (scheduler.milestoneAlign) {
          case 'center':
            x += element.offsetWidth / 2;
            break;
          case 'end':
            x += element.offsetWidth;
            break;
        }
      }
      return x;
    } else {
      let y = coord[1];
      // Adjust coordinate for milestones if using a layout mode, since they are aligned differently than events
      if (scheduler.milestoneLayoutMode !== 'default' && eventRecord.isMilestone) {
        switch (scheduler.milestoneAlign) {
          case 'center':
            y += element.offsetHeight / 2;
            break;
          case 'end':
            y += element.offsetHeight;
            break;
        }
      }
      return y;
    }
  }
  /**
   * Get resource record occluded by the drag proxy.
   * @private
   * @returns {Scheduler.model.ResourceModel}
   */
  resolveResource() {
    const me = this,
      client = me.currentOverClient,
      {
        isHorizontal
      } = client,
      {
        context,
        browserEvent,
        dragProxy
      } = me.dragData,
      element = dragProxy || context.element,
      // Page coords for elementFromPoint
      pageRect = Rectangle.from(element, null, true),
      y = client.isVertical || me.unifiedDrag ? context.clientY : pageRect.center.y,
      // Local coords to resolve resource in vertical
      localRect = Rectangle.from(element, client.timeAxisSubGridElement, true),
      {
        x: lx,
        y: ly
      } = localRect.center,
      eventTarget = me.getMouseMoveEventTarget(browserEvent);
    let resource = null;
    if (client.element.contains(eventTarget)) {
      // This is benchmarked as the fastest way to find a Grid Row from a viewport Y coordinate
      // so use it in preference to elementFromPoint (which causes a forced synchronous layout) in horizontal mode.
      if (isHorizontal) {
        const row = client.rowManager.getRowAt(y);
        resource = row && client.store.getAt(row.dataIndex);
      } else {
        // In vertical mode, just use the X coordinate to find out which resource we are under.
        // The method requires that a .b-sch-timeaxis-cell element be passed.
        // There is only one in vertical mode, so use that.
        resource = client.resolveResourceRecord(client.timeAxisSubGridElement.querySelector('.b-sch-timeaxis-cell'), [lx, ly]);
      }
    }
    return resource;
  }
  //endregion
  //region Other stuff
  adjustStartDate(startDate, timeDiff) {
    const scheduler = this.currentOverClient;
    startDate = scheduler.timeAxis.roundDate(new Date(startDate - 0 + timeDiff), scheduler.snapRelativeToEventStartDate ? startDate : false);
    return this.constrainStartDate(startDate);
  }
  getRecordElement(assignmentRecord) {
    return this.client.getElementFromAssignmentRecord(assignmentRecord, true);
  }
  // Used by the Dependencies feature to draw lines to the drag proxy instead of the original event element
  getProxyElement(assignmentRecord) {
    if (this.isDragging) {
      const index = this.dragData.assignmentRecords.indexOf(assignmentRecord);
      if (index >= 0) {
        return this.dragData.proxyElements[index];
      }
    }
    return null;
  }
  //endregion
  //#region Salesforce hooks
  getMouseMoveEventTarget(event) {
    return event.target;
  }
  //#endregion
}

EventDrag._$name = 'EventDrag';
GridFeatureManager.registerFeature(EventDrag, true, 'Scheduler');
GridFeatureManager.registerFeature(EventDrag, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/EventDragCreate
 */
/**
 * Feature that allows the user to create new events by dragging in empty parts of the scheduler rows.
 *
 * {@inlineexample Scheduler/feature/EventDragCreate.js}
 *
 * This feature is **enabled** by default.
 *
 * <div class="note">Incompatible with the {@link Scheduler.feature.EventDragSelect EventDragSelect} and
 * {@link Scheduler.feature.Pan Pan} features. If either of those features are enabled, this feature has no effect.
 * </div>
 *
 * ## Conditionally preventing drag creation
 *
 * To conditionally prevent drag creation for a certain resource or a certain timespan, you listen for the
 * {@link #event-beforeDragCreate} event, add your custom logic to it and return `false` to prevent the operation
 * from starting. For example to not allow drag creation on the topmost resource:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     listeners : {
 *         beforeDragCreate({ resource }) {
 *             // Prevent drag creating on the topmost resource
 *             if (resource === scheduler.resourceStore.first) {
 *                 return false;
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * @extends Scheduler/feature/base/DragCreateBase
 * @demo Scheduler/basic
 * @classtype eventDragCreate
 * @feature
 */
class EventDragCreate extends DragCreateBase {
  //region Config
  static $name = 'EventDragCreate';
  static configurable = {
    /**
     * An empty function by default, but provided so that you can perform custom validation on the event being
     * created. Return `true` if the new event is valid, `false` to prevent an event being created.
     * @param {Object} context A drag create context
     * @param {Date} context.startDate Event start date
     * @param {Date} context.endDate Event end date
     * @param {Scheduler.model.EventModel} context.record Event record
     * @param {Scheduler.model.ResourceModel} context.resourceRecord Resource record
     * @param {Event} event The event object
     * @returns {Boolean} `true` if this validation passes
     * @config {Function}
     */
    validatorFn: () => true,
    /**
     * Locks the layout during drag create, overriding the default behaviour that uses the same rendering
     * pathway for drag creation as for already existing events.
     *
     * This more closely resembles the behaviour of versions prior to 4.2.0.
     *
     * @config {Boolean}
     * @default
     */
    lockLayout: false
  };
  //endregion
  //region Events
  /**
   * Fires on the owning Scheduler after the new event has been created.
   * @event dragCreateEnd
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.EventModel} eventRecord The new `EventModel` record.
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource for the row in which the event is being
   * created.
   * @param {MouseEvent} event The ending mouseup event.
   * @param {HTMLElement} eventElement The DOM element representing the newly created event un the UI.
   */
  /**
   * Fires on the owning Scheduler at the beginning of the drag gesture. Returning `false` from a listener prevents
   * the drag create operation from starting.
   *
   * ```javascript
   * const scheduler = new Scheduler({
   *     listeners : {
   *         beforeDragCreate({ date }) {
   *             // Prevent drag creating events in the past
   *             return date >= Date.now();
   *         }
   *     }
   * });
   * ```
   *
   * @event beforeDragCreate
   * @on-owner
   * @preventable
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.ResourceModel} resourceRecord
   * @param {Date} date The datetime associated with the drag start point.
   */
  /**
   * Fires on the owning Scheduler after the drag start has created a new Event record.
   * @event dragCreateStart
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.EventModel} eventRecord The event record being created
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {HTMLElement} eventElement The element representing the new event.
   */
  /**
   * Fires on the owning Scheduler to allow implementer to prevent immediate finalization by setting
   * `data.context.async = true` in the listener, to show a confirmation popup etc
   * ```javascript
   *  scheduler.on('beforedragcreatefinalize', ({context}) => {
   *      context.async = true;
   *      setTimeout(() => {
   *          // async code don't forget to call finalize
   *          context.finalize();
   *      }, 1000);
   *  })
   * ```
   * @event beforeDragCreateFinalize
   * @on-owner
   * @param {Scheduler.view.Scheduler} source Scheduler instance
   * @param {Scheduler.model.EventModel} eventRecord The event record being created
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {HTMLElement} eventElement The element representing the new Event record
   * @param {Object} context
   * @param {Boolean} context.async Set true to handle drag create asynchronously (e.g. to wait for user
   * confirmation)
   * @param {Function} context.finalize Call this method to finalize drag create. This method accepts one
   * argument: pass true to update records, or false, to ignore changes
   */
  /**
   * Fires on the owning Scheduler at the end of the drag create gesture whether or not
   * a new event was created by the gesture.
   * @event afterDragCreate
   * @on-owner
   * @param {Scheduler.view.Scheduler} source
   * @param {Scheduler.model.EventModel} eventRecord The event record being created
   * @param {Scheduler.model.ResourceModel} resourceRecord The resource record
   * @param {HTMLElement} eventElement The element representing the created event record
   */
  //endregion
  //region Init
  get scheduler() {
    return this.client;
  }
  get store() {
    return this.client.eventStore;
  }
  get project() {
    return this.client.project;
  }
  updateLockLayout(lock) {
    this.dragActiveCls = `b-dragcreating${lock ? ' b-dragcreate-lock' : ''}`;
  }
  //endregion
  //region Scheduler specific implementation
  handleBeforeDragCreate(drag, eventRecord, event) {
    var _scheduler$getDateCon;
    const {
      resourceRecord
    } = drag;
    if (resourceRecord.readOnly || !this.scheduler.resourceStore.isAvailable(resourceRecord)) {
      return false;
    }
    const {
        scheduler
      } = this,
      // For resources with a calendar, ensure the date is inside a working time range
      isWorkingTime = !scheduler.isSchedulerPro || eventRecord.ignoreResourceCalendar || resourceRecord.isWorkingTime(drag.mousedownDate),
      result = isWorkingTime && scheduler.trigger('beforeDragCreate', {
        resourceRecord,
        date: drag.mousedownDate,
        event
      });
    // Save date constraints
    this.dateConstraints = (_scheduler$getDateCon = scheduler.getDateConstraints) === null || _scheduler$getDateCon === void 0 ? void 0 : _scheduler$getDateCon.call(scheduler, resourceRecord, eventRecord);
    return result;
  }
  dragStart(drag) {
    var _client$onEventCreate;
    const me = this,
      {
        client
      } = me,
      {
        eventStore,
        assignmentStore,
        enableEventAnimations,
        enableTransactionalFeatures
      } = client,
      {
        resourceRecord
      } = drag,
      eventRecord = me.createEventRecord(drag),
      resourceRecords = [resourceRecord];
    eventRecord.set('duration', DateHelper.diff(eventRecord.startDate, eventRecord.endDate, eventRecord.durationUnit, true));
    // It's only a provisional event until gesture is completed (possibly longer if an editor dialog is shown after)
    eventRecord.isCreating = true;
    // Flag used by rendering to not draw a zero length event being drag created as a milestone
    eventRecord.meta.isDragCreating = true;
    // force the transaction canceling in the taskeditor early
    // this is because we are going to add a new event record to the store, and it has to be out of the
    // task editor's stm transaction
    // now there's a re-entrant protection in that method, so hopefully when it will be called by the
    // editor itself that's ok
    // `taskEdit === false` in some cases, so can't just use `?.` here
    client.features.taskEdit && client.features.taskEdit.doCancel();
    // This presents the event to be scheduled for validation at the proposed mouse/date point
    // If rejected, we cancel operation
    if (me.handleBeforeDragCreate(drag, eventRecord, drag.event) === false) {
      return false;
    }
    // This is an async function which will start transaction asynchronously. This workflow expect transaction to
    // be started ASAP
    me.captureStm(true);
    let assignmentRecords = [];
    if (resourceRecord) {
      if (eventStore.usesSingleAssignment || !enableTransactionalFeatures) {
        assignmentRecords = assignmentStore.assignEventToResource(eventRecord, resourceRecord);
      } else {
        // Do not add record to the store just yet, otherwise records would get to the STM queue assignment first,
        // then event, which will break `store.added` bag after undo/redo.
        assignmentRecords = [assignmentStore.createRecord({
          event: eventRecord,
          resource: resourceRecord
        })];
      }
    }
    // Vetoable beforeEventAdd allows cancel of this operation
    if (client.trigger('beforeEventAdd', {
      eventRecord,
      resourceRecords,
      assignmentRecords
    }) === false) {
      if (eventStore.usesSingleAssignment || !enableTransactionalFeatures) {
        assignmentStore.remove(assignmentRecords);
      }
      return false;
    }
    // When configured to lock layout during drag create, set a flag that HorizontalRendering will pick up to
    // exclude the new event from the layout calculations. It will then be at the topmost position in the "cell"
    if (me.lockLayout) {
      eventRecord.meta.excludeFromLayout = true;
    }
    (_client$onEventCreate = client.onEventCreated) === null || _client$onEventCreate === void 0 ? void 0 : _client$onEventCreate.call(client, eventRecord);
    client.enableEventAnimations = false;
    eventStore.addAsync(eventRecord).then(() => client.enableEventAnimations = enableEventAnimations);
    if (!eventStore.usesSingleAssignment && enableTransactionalFeatures) {
      // Add assignment after event only to keep STM transaction sane
      assignmentStore.add(assignmentRecords[0]);
    }
    // Element must be created synchronously, not after the project's normalizing delays.
    // Overrides the check for isEngineReady in VerticalRendering so that the newly added record
    // will be rendered when we call refreshRows.
    client.isCreating = true;
    client.refreshRows();
    client.isCreating = false;
    // Set the element we are dragging
    drag.itemElement = drag.element = client.getElementFromEventRecord(eventRecord);
    // If the resource row is very tall, the event may have been rendered outside of the
    // visible viewport. If so, scroll it into view.
    if (!DomHelper.isInView(drag.itemElement)) {
      client.scrollable.scrollIntoView(drag.itemElement, {
        animate: true,
        edgeOffset: client.barMargin
      });
    }
    return super.dragStart(drag);
  }
  checkValidity(context, event) {
    const me = this,
      {
        client
      } = me;
    // Nicer for users of validatorFn
    context.resourceRecord = me.dragging.resourceRecord;
    return (client.allowOverlap || client.isDateRangeAvailable(context.startDate, context.endDate, context.eventRecord, context.resourceRecord)) && me.createValidatorFn.call(me.validatorFnThisObj || me, context, event);
  }
  // Determine if resource already has events or not
  isRowEmpty(resourceRecord) {
    const events = this.store.getEventsForResource(resourceRecord);
    return !events || !events.length;
  }
  //endregion
  triggerBeforeFinalize(event) {
    this.client.trigger(`beforeDragCreateFinalize`, event);
  }
  /**
   * Creates an event by the event object coordinates
   * @param {Object} drag The Bryntum event object
   * @private
   */
  createEventRecord(drag) {
    const me = this,
      {
        client
      } = me,
      dimension = client.isHorizontal ? 'X' : 'Y',
      {
        timeAxis,
        eventStore,
        weekStartDay
      } = client,
      {
        event,
        mousedownDate
      } = drag,
      draggingEnd = me.draggingEnd = event[`page${dimension}`] > drag.startEvent[`page${dimension}`],
      eventConfig = {
        name: eventStore.modelClass.fieldMap.name.defaultValue || me.L('L{Object.newEvent}'),
        startDate: draggingEnd ? DateHelper.floor(mousedownDate, timeAxis.resolution, null, weekStartDay) : mousedownDate,
        endDate: draggingEnd ? mousedownDate : DateHelper.ceil(mousedownDate, timeAxis.resolution, null, weekStartDay)
      };
    // if project model has been imported from Gantt, we have to define constraint data directly to correct
    // auto-scheduling while dragCreate
    if (client.project.isGanttProjectMixin) {
      ObjectHelper.assign(eventConfig, {
        constraintDate: eventConfig.startDate,
        constraintType: 'startnoearlierthan'
      });
    }
    return eventStore.createRecord(eventConfig);
  }
  async internalUpdateRecord(context, eventRecord) {
    await super.internalUpdateRecord(context, eventRecord);
    // Toggle isCreating after ending batch, to make sure assignments can become persistable
    if (!this.client.hasEventEditor) {
      context.eventRecord.isCreating = false;
    }
  }
  async finalizeDragCreate(context) {
    const {
      meta
    } = context.eventRecord;
    // Remove the layout lock flag, event will jump into place as part of the finalization
    meta.excludeFromLayout = false;
    // Also allow new event to become a milestone now
    meta.isDragCreating = false;
    const transferred = await super.finalizeDragCreate(context);
    // if STM capture has NOT been transferred to the
    // event editor, we need to finalize the STM transaction / release the capture
    if (!transferred) {
      await this.freeStm(true);
    } else {
      // otherwise just freeing our capture
      this.hasStmCapture = false;
    }
    return transferred;
  }
  async cancelDragCreate(context) {
    await super.cancelDragCreate(context);
    await this.freeStm(false);
  }
  getTipHtml(...args) {
    const html = super.getTipHtml(...args),
      {
        element
      } = this.tip;
    element.classList.add('b-sch-dragcreate-tooltip');
    element.classList.toggle('b-too-narrow', this.dragging.context.tooNarrow);
    return html;
  }
  onAborted(context) {
    var _this$store$unassignE, _this$store;
    const {
      eventRecord,
      resourceRecord
    } = context;
    // The product this is being used in may not have resources.
    (_this$store$unassignE = (_this$store = this.store).unassignEventFromResource) === null || _this$store$unassignE === void 0 ? void 0 : _this$store$unassignE.call(_this$store, eventRecord, resourceRecord);
    this.store.remove(eventRecord);
  }
}
EventDragCreate._$name = 'EventDragCreate';
GridFeatureManager.registerFeature(EventDragCreate, true, 'Scheduler');
GridFeatureManager.registerFeature(EventDragCreate, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/EventTooltip
 */
// Alignment offsets to clear any dependency terminals depending on whether
// the tooltip is aligned top/bottom (1) or left/right (2) as parsed from the
// align string by Rectangle's parseAlign
const zeroOffset = [0, 0],
  depOffset = [null, [0, 10], [10, 0]];
/**
 * Displays a tooltip when hovering events. The template used to render the tooltip can be customized, see {@link #config-template}.
 * Config options are also applied to the tooltip shown, see {@link Core.widget.Tooltip} for available options.
 *
 * ## Showing local data
 * To show a basic "local" tooltip (with data available in the Event record) upon hover:
 * ```javascript
 * new Scheduler({
 *   features : {
 *     eventTooltip : {
 *         // Tooltip configs can be used here
 *         align : 'l-r' // Align left to right,
 *         // A custom HTML template
 *         template : data => `<dl>
 *           <dt>Assigned to:</dt>
 *              <dt>Time:</dt>
 *              <dd>
 *                  ${DateHelper.format(data.eventRecord.startDate, 'LT')} - ${DateHelper.format(data.eventRecord.endDate, 'LT')}
 *              </dd>
 *              ${data.eventRecord.get('note') ? `<dt>Note:</dt><dd>${data.eventRecord.note}</dd>` : ''}
 *
 *              ${data.eventRecord.get('image') ? `<dt>Image:</dt><dd><img class="image" src="${data.eventRecord.get('image')}"/></dd>` : ''}
 *          </dl>`
 *     }
 *   }
 * });
 * ```
 *
 * ## Showing remotely loaded data
 * Loading remote data into the event tooltip is easy. Simply use the {@link #config-template} and return a Promise which yields the content to show.
 * ```javascript
 * new Scheduler({
 *   features : {
 *     eventTooltip : {
 *        template : ({ eventRecord }) => AjaxHelper.get(`./fakeServer?name=${eventRecord.name}`).then(response => response.text())
 *     }
 *   }
 * });
 * ```
 *
 * This feature is **enabled** by default
 *
 * By default, the tooltip {@link Core.widget.Widget#config-scrollAction realigns on scroll}
 * meaning that it will stay aligned with its target should a scroll interaction make the target move.
 *
 * If this is causing performance issues in a Scheduler, such as if there are many dozens of events
 * visible, you can configure this feature with `scrollAction: 'hide'`. This feature's configuration is
 * applied to the tooltip, so that will mean that the tooltip will hide if its target is moved by a
 * scroll interaction.
 *
 * @extends Scheduler/feature/base/TooltipBase
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/EventTooltip.js
 * @classtype eventTooltip
 * @feature
 */
class EventTooltip extends TooltipBase {
  //region Config
  static get $name() {
    return 'EventTooltip';
  }
  static get defaultConfig() {
    return {
      /**
       * A function which receives data about the event and returns a string,
       * or a Promise yielding a string (for async tooltips), to be displayed in the tooltip.
       * This method will be called with an object containing the fields below
       * @param {Object} data
       * @param {Scheduler.model.EventModel} data.eventRecord
       * @param {Date} data.startDate
       * @param {Date} data.endDate
       * @param {String} data.startText
       * @param {String} data.endText
       * @config {Function} template
       */
      template: data => `
                ${data.eventRecord.name ? StringHelper.xss`<div class="b-sch-event-title">${data.eventRecord.name}</div>` : ''}
                ${data.startClockHtml}
                ${data.endClockHtml}`,
      cls: 'b-sch-event-tooltip',
      monitorRecordUpdate: true,
      /**
       * Defines what to do if document is scrolled while the tooltip is visible.
       *
       * Valid values: ´null´: do nothing, ´hide´: hide the tooltip or ´realign´: realign to the target if possible.
       *
       * @config {'hide'|'realign'|null}
       * @default
       */
      scrollAction: 'hide'
    };
  }
  /**
   * The event which the tooltip feature has been activated for.
   * @member {Scheduler.model.EventModel} eventRecord
   * @readonly
   */
  //endregion
  construct(client, config) {
    super.construct(client, config);
    if (typeof this.align === 'string') {
      this.align = {
        align: this.align
      };
    }
  }
  onPaint({
    firstPaint
  }) {
    super.onPaint(...arguments);
    if (firstPaint) {
      const {
        dependencies
      } = this.client.features;
      if (dependencies) {
        this.tooltip.ion({
          beforeAlign({
            source: tooltip,
            offset = zeroOffset
          }) {
            const {
                edgeAligned
              } = parseAlign(tooltip.align.align),
              depTerminalOffset = dependencies.disabled ? zeroOffset : depOffset[edgeAligned];
            // Add the spec's offset to the offset necessitated by dependency terminals
            arguments[0].offset = [offset[0] + depTerminalOffset[0], offset[1] + depTerminalOffset[1]];
          }
        });
      }
    }
  }
}
EventTooltip._$name = 'EventTooltip';
GridFeatureManager.registerFeature(EventTooltip, true, 'Scheduler');
GridFeatureManager.registerFeature(EventTooltip, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/StickyEvents
 */
const zeroMargins = {
  width: 0,
  height: 0
};
/**
 * This feature applies native `position: sticky` to event contents in horizontal mode, keeping the contents in view as
 * long as possible on scroll. For vertical mode it uses a programmatic solution to achieve the same result.
 *
 * Assign `eventRecord.stickyContents = false` to disable stickiness on a per event level (docs for
 * {@link Scheduler/model/EventModel#field-stickyContents}).
 *
 * This feature is **enabled** by default.
 *
 * ### Note
 * If a complex {@link Scheduler.view.Scheduler#config-eventRenderer} is used to create a DOM structure within the
 * `.b-sch-event-content` element, then application CSS will need to be written to cancel the stickiness on the
 * `.b-sch-event-content` element, and make some inner content element(s) sticky.
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype stickyEvents
 * @feature
 */
class StickyEvents extends InstancePlugin {
  static $name = 'StickyEvents';
  static type = 'stickyEvents';
  static pluginConfig = {
    chain: ['onEventDataGenerated']
  };
  construct(scheduler, config) {
    super.construct(scheduler, config);
    if (scheduler.isVertical) {
      this.toUpdate = new Set();
      scheduler.ion({
        scroll: 'onSchedulerScroll',
        horizontalScroll: 'onHorizontalScroll',
        thisObj: this,
        prio: 10000
      });
    }
  }
  onEventDataGenerated(renderData) {
    if (this.client.isHorizontal) {
      renderData.wrapperCls['b-disable-sticky'] = renderData.eventRecord.stickyContents === false;
    } else {
      this.syncEventContentPosition(renderData, undefined, true);
      this.updateStyles();
    }
  }
  //region Vertical mode
  onSchedulerScroll() {
    if (!this.disabled) {
      this.verticalSyncAllEventsContentPosition(this.client);
    }
  }
  // Have to sync also on horizontal scroll, since we reuse elements and dom configs
  onHorizontalScroll({
    subGrid
  }) {
    if (subGrid === this.client.timeAxisSubGrid) {
      this.verticalSyncAllEventsContentPosition(this.client);
    }
  }
  updateStyles() {
    for (const {
      contentEl,
      style
    } of this.toUpdate) {
      DomHelper.applyStyle(contentEl, style);
    }
    this.toUpdate.clear();
  }
  verticalSyncAllEventsContentPosition(scheduler) {
    const {
      resourceMap
    } = scheduler.currentOrientation;
    for (const eventsData of resourceMap.values()) {
      for (const {
        renderData,
        elementConfig
      } of Object.values(eventsData)) {
        const args = [renderData];
        if (elementConfig && renderData.eventRecord.isResourceTimeRange) {
          args.push(elementConfig.children[0]);
        }
        this.syncEventContentPosition.apply(this, args);
      }
    }
    this.toUpdate.size && this.updateStyles();
  }
  syncEventContentPosition(renderData, eventContent = renderData.eventContent, duringGeneration = false) {
    if (this.disabled ||
    // Allow client disable stickiness for certain events
    renderData.eventRecord.stickyContents === false) {
      return;
    }
    const {
        client
      } = this,
      {
        eventRecord,
        resourceRecord,
        useEventBuffer,
        bufferAfterWidth,
        bufferBeforeWidth,
        top,
        height
      } = renderData,
      scrollPosition = client.scrollable.y,
      wrapperEl = duringGeneration ? null : client.getElementFromEventRecord(eventRecord, resourceRecord, true),
      contentEl = wrapperEl && DomSync.getChild(wrapperEl, 'event.content'),
      meta = eventRecord.instanceMeta(client),
      style = typeof eventContent.style === 'string' ? eventContent.style = DomHelper.parseStyle(eventContent.style) : eventContent.style || (eventContent.style = {});
    // Do not process events being dragged
    if (wrapperEl !== null && wrapperEl !== void 0 && wrapperEl.classList.contains('b-dragging')) {
      return;
    }
    let start = top,
      contentSize = height,
      end = start + contentSize;
    if (useEventBuffer) {
      start += bufferBeforeWidth;
      contentSize = contentSize - bufferBeforeWidth - bufferAfterWidth;
      end = start + contentSize;
    }
    // Only process non-milestones that are partially out of view
    if (start < scrollPosition && end >= scrollPosition && !eventRecord.isMilestone) {
      const contentWidth = contentEl === null || contentEl === void 0 ? void 0 : contentEl.offsetWidth,
        justify = (contentEl === null || contentEl === void 0 ? void 0 : contentEl.parentNode) && DomHelper.getStyleValue(contentEl.parentNode, 'justifyContent'),
        c = justify === 'center' ? (renderData.width - contentWidth) / 2 : 0,
        eventStart = start,
        eventEnd = eventStart + contentSize - 1;
      // Only process non-milestone events. Milestones have no width.
      // If there's no offsetWidth, it's still b-released, so we cannot measure it.
      // If the event starts off the left edge, but its right edge is still visible,
      // translate the contentEl to compensate. If not, undo any translation.
      if ((!contentEl || contentWidth) && eventStart < scrollPosition && eventEnd >= scrollPosition) {
        const edgeSizes = this.getEventContentMargins(contentEl),
          maxOffset = contentEl ? contentSize - contentEl.offsetHeight - edgeSizes.height - c : Number.MAX_SAFE_INTEGER,
          offset = Math.min(scrollPosition - eventStart, maxOffset - 2);
        style.transform = offset > 0 ? `translateY(${offset}px)` : '';
        meta.stuck = true;
      } else {
        style.transform = '';
        meta.stuck = false;
      }
      if (contentEl) {
        this.toUpdate.add({
          contentEl,
          style
        });
      }
    } else if (contentEl && meta.stuck) {
      style.transform = '';
      meta.stuck = false;
      this.toUpdate.add({
        contentEl,
        style
      });
    }
  }
  // Only measure the margins of an event's contentEl once
  getEventContentMargins(contentEl) {
    if (contentEl !== null && contentEl !== void 0 && contentEl.classList.contains('b-sch-event-content')) {
      return DomHelper.getEdgeSize(contentEl, 'margin');
    }
    return zeroMargins;
  }
  //endregion
  doDisable() {
    super.doDisable(...arguments);
    if (!this.isConfiguring) {
      this.client.refreshWithTransition();
    }
  }
}
StickyEvents._$name = 'StickyEvents';
GridFeatureManager.registerFeature(StickyEvents, true, 'Scheduler');
GridFeatureManager.registerFeature(StickyEvents, false, 'ResourceHistogram');

/**
 * @module Scheduler/feature/TimeRanges
 */
/**
 * Feature that renders global ranges of time in the timeline. Use this feature to visualize a `range` like a 1 hr lunch
 * or some important point in time (a `line`, i.e. a range with 0 duration). This feature can also show a current time
 * indicator if you set {@link #config-showCurrentTimeLine} to true. To style the rendered elements, use the
 * {@link Scheduler.model.TimeSpan#field-cls cls} field of the `TimeSpan` class.
 *
 * {@inlineexample Scheduler/feature/TimeRanges.js}
 *
 * Each time range is represented by an instances of {@link Scheduler.model.TimeSpan}, held in a simple
 * {@link Core.data.Store}. The feature uses {@link Scheduler/model/ProjectModel#property-timeRangeStore} defined on the
 * project by default. The store's persisting/loading is handled by Crud Manager (if it's used by the component).
 *
 * Note that the feature uses virtualized rendering, only the currently visible ranges are available in the DOM.
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * ## Showing an icon in the time range header
 *
 * You can use Font Awesome icons easily (or set any other icon using CSS) by using the {@link Scheduler.model.TimeSpan#field-iconCls}
 * field. The JSON data below will show a flag icon:
 *
 * ```json
 * {
 *     "id"        : 5,
 *     "iconCls"   : "b-fa b-fa-flag",
 *     "name"      : "v5.0",
 *     "startDate" : "2019-02-07 15:45"
 * },
 * ```
 *
 * ## Recurring time ranges
 *
 * The feature supports recurring ranges in case the provided store and models
 * have {@link Scheduler/data/mixin/RecurringTimeSpansMixin} and {@link Scheduler/model/mixin/RecurringTimeSpan}
 * mixins applied:
 *
 * ```javascript
 * // We want to use recurring time ranges so we make a special model extending standard TimeSpan model with
 * // RecurringTimeSpan which adds recurrence support
 * class MyTimeRange extends RecurringTimeSpan(TimeSpan) {}
 *
 * // Define a new store extending standard Store with RecurringTimeSpansMixin mixin to add recurrence support to the
 * // store. This store will contain time ranges.
 * class MyTimeRangeStore extends RecurringTimeSpansMixin(Store) {
 *     static get defaultConfig() {
 *         return {
 *             // use our new MyResourceTimeRange model
 *             modelClass : MyTimeRange
 *         };
 *     }
 * };
 *
 * // Instantiate store for timeRanges using our new classes
 * const timeRangeStore = new MyTimeRangeStore({
 *     data : [{
 *         id             : 1,
 *         resourceId     : 'r1',
 *         startDate      : '2019-01-01T11:00',
 *         endDate        : '2019-01-01T13:00',
 *         name           : 'Lunch',
 *         // this time range should repeat every day
 *         recurrenceRule : 'FREQ=DAILY'
 *     }]
 * });
 *
 * const scheduler = new Scheduler({
 *     ...
 *     features : {
 *         timeRanges : true
 *     },
 *
 *     crudManager : {
 *         // store for "timeRanges" feature
 *         timeRangeStore
 *     }
 * });
 * ```
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @classtype timeRanges
 * @feature
 * @demo Scheduler/timeranges
 */
class TimeRanges extends AbstractTimeRanges.mixin(AttachToProjectMixin) {
  //region Config
  static get $name() {
    return 'TimeRanges';
  }
  static get defaultConfig() {
    return {
      store: true
    };
  }
  static configurable = {
    /**
     * Store that holds the time ranges (using the {@link Scheduler.model.TimeSpan} model or subclass thereof).
     * A store will be automatically created if none is specified.
     * @config {Core.data.Store|StoreConfig}
     * @category Misc
     */
    store: {
      modelClass: TimeSpan
    },
    /**
     * The interval (as amount of ms) defining how frequently the current timeline will be updated
     * @config {Number}
     * @default
     * @category Misc
     */
    currentTimeLineUpdateInterval: 10000,
    /**
     * The date format to show in the header for the current time line (when {@link #config-showCurrentTimeLine} is configured).
     * See {@link Core.helper.DateHelper} for the possible formats to use.
     * @config {String}
     * @default
     * @category Common
     */
    currentDateFormat: 'HH:mm',
    /**
     * Show a line indicating current time. Either `true` or `false` or a {@link Scheduler.model.TimeSpan}
     * configuration object to apply to this special time range (allowing you to provide a custom text):
     *
     * ```javascript
     * showCurrentTimeLine : {
     *     name : 'Now'
     * }
     * ```
     *
     * The line carries the CSS class name `b-sch-current-time`, and this may be used to add custom styling to it.
     *
     * @prp {Boolean|TimeSpanConfig}
     * @default
     * @category Common
     */
    showCurrentTimeLine: false
  };
  //endregion
  //region Init & destroy
  doDestroy() {
    var _this$storeDetacher;
    (_this$storeDetacher = this.storeDetacher) === null || _this$storeDetacher === void 0 ? void 0 : _this$storeDetacher.call(this);
    super.doDestroy();
  }
  /**
   * Returns the TimeRanges which occur within the client Scheduler's time axis.
   * @property {Scheduler.model.TimeSpan[]}
   */
  get timeRanges() {
    const me = this;
    if (!me._timeRanges) {
      const {
        store
      } = me;
      let {
        records
      } = store;
      if (store.recurringEvents) {
        const {
          startDate,
          endDate
        } = me.client.timeAxis;
        records = records.flatMap(timeSpan => {
          // Collect occurrences for the recurring events in the record set
          if (timeSpan.isRecurring) {
            return timeSpan.getOccurrencesForDateRange(startDate, endDate);
          }
          return timeSpan;
        });
      }
      if (me.currentTimeLine) {
        // Avoid polluting store records
        if (!store.recurringEvents) {
          records = records.slice();
        }
        records.push(me.currentTimeLine);
      }
      me._timeRanges = records;
    }
    return me._timeRanges;
  }
  //endregion
  //region Current time line
  attachToProject(project) {
    var _me$projectTimeZoneCh;
    super.attachToProject(project);
    const me = this;
    (_me$projectTimeZoneCh = me.projectTimeZoneChangeDetacher) === null || _me$projectTimeZoneCh === void 0 ? void 0 : _me$projectTimeZoneCh.call(me);
    if (me.showCurrentTimeLine) {
      var _me$client$project;
      // Update currentTimeLine immediately after a time zone change
      me.projectTimeZoneChangeDetacher = (_me$client$project = me.client.project) === null || _me$client$project === void 0 ? void 0 : _me$client$project.ion({
        timeZoneChange: () => me.updateCurrentTimeLine()
      });
      // Update currentTimeLine if its already created
      if (me.currentTimeLine) {
        me.updateCurrentTimeLine();
      }
    }
  }
  initCurrentTimeLine() {
    const me = this;
    if (me.currentTimeLine || !me.showCurrentTimeLine) {
      return;
    }
    const data = typeof me.showCurrentTimeLine === 'object' ? me.showCurrentTimeLine : {};
    me.currentTimeLine = me.store.modelClass.new({
      id: 'currentTime',
      cls: 'b-sch-current-time'
    }, data);
    me.currentTimeInterval = me.setInterval(() => me.updateCurrentTimeLine(), me.currentTimeLineUpdateInterval);
    me._timeRanges = null;
    me.updateCurrentTimeLine();
  }
  updateCurrentTimeLine() {
    var _me$project;
    const me = this,
      {
        currentTimeLine
      } = me;
    currentTimeLine.timeZone = (_me$project = me.project) === null || _me$project === void 0 ? void 0 : _me$project.timeZone;
    currentTimeLine.setLocalDate('startDate', new Date());
    currentTimeLine.endDate = currentTimeLine.startDate;
    if (!currentTimeLine.originalData.name) {
      currentTimeLine.name = DateHelper.format(currentTimeLine.startDate, me.currentDateFormat);
    }
    me.renderRanges();
  }
  hideCurrentTimeLine() {
    const me = this;
    if (!me.currentTimeLine) {
      return;
    }
    me.clearInterval(me.currentTimeInterval);
    me.currentTimeLine = null;
    me.refresh();
  }
  updateShowCurrentTimeLine(show) {
    if (show) {
      this.initCurrentTimeLine();
    } else {
      this.hideCurrentTimeLine();
    }
  }
  //endregion
  //region Menu items
  /**
   * Adds a menu item to show/hide current time line.
   * @param {Object} options Contains menu items and extra data retrieved from the menu target.
   * @param {Grid.column.Column} options.column Column for which the menu will be shown
   * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
   * @internal
   */
  populateTimeAxisHeaderMenu({
    items
  }) {
    items.currentTimeLine = {
      weight: 400,
      text: this.L('L{showCurrentTimeLine}'),
      checked: this.currentTimeLine,
      onToggle: ({
        checked
      }) => this.updateShowCurrentTimeLine(checked && this.showCurrentTimeLine)
    };
  }
  //endregion
  //region Store
  attachToStore(store) {
    const me = this;
    let renderRanges = false;
    // if we had some store assigned before we need to detach it
    if (me.storeDetacher) {
      me.storeDetacher();
      // then we'll need to render ranges provided by the new store
      renderRanges = true;
    }
    me.storeDetacher = store.ion({
      change: 'onStoreChange',
      refresh: 'onStoreChange',
      thisObj: me
    });
    me._timeRanges = null;
    // render ranges if needed
    renderRanges && me.renderRanges();
  }
  /**
   * Returns the {@link Core.data.Store store} used by this feature
   * @property {Core.data.Store}
   * @category Misc
   */
  get store() {
    return this.client.project.timeRangeStore;
  }
  updateStore(store) {
    const me = this,
      {
        client
      } = me,
      {
        project
      } = client;
    store = project.timeRangeStore;
    me.attachToStore(store);
    // timeRanges can be set on scheduler/gantt, for convenience. Should only be processed by the TimeRanges and not
    // any subclasses
    if (client.timeRanges && !client._timeRangesExposed) {
      store.add(client.timeRanges);
      delete client.timeRanges;
    }
  }
  // Called by ProjectConsumer after a new store is assigned at runtime
  attachToTimeRangeStore(store) {
    this.store = store;
  }
  resolveTimeRangeRecord(el) {
    return this.store.getById(el.closest(this.baseSelector).dataset.id);
  }
  onStoreChange({
    type,
    action
  }) {
    const me = this;
    // Force re-evaluating of which ranges to consider for render
    me._timeRanges = null;
    // https://github.com/bryntum/support/issues/1398 - checking also if scheduler is visible to change elements
    if (me.disabled || !me.client.isVisible || me.isConfiguring || type === 'refresh' && action !== 'batch') {
      return;
    }
    me.client.runWithTransition(() => me.renderRanges(), !me.client.refreshSuspended);
  }
  //endregion
  //region Drag
  onDragStart(event) {
    const me = this,
      {
        context
      } = event,
      record = me.resolveTimeRangeRecord(context.element.closest(me.baseSelector)),
      rangeBodyEl = me.getBodyElementByRecord(record);
    context.relatedElements = [rangeBodyEl];
    Object.assign(context, {
      record,
      rangeBodyEl,
      originRangeX: DomHelper.getTranslateX(rangeBodyEl),
      originRangeY: DomHelper.getTranslateY(rangeBodyEl)
    });
    super.onDragStart(event);
    me.showTip(context);
  }
  onDrop(event) {
    const {
      context
    } = event;
    if (!context.valid) {
      return this.onInvalidDrop({
        context
      });
    }
    const me = this,
      {
        client
      } = me,
      {
        record
      } = context,
      box = Rectangle.from(context.rangeBodyEl),
      newStart = client.getDateFromCoordinate(box.getStart(client.rtl, client.isHorizontal), 'round', false),
      wasModified = record.startDate - newStart !== 0;
    if (wasModified) {
      record.setStartDate(newStart);
    } else {
      me.onInvalidDrop();
    }
    me.destroyTip();
    super.onDrop(event);
  }
  //endregion
  //region Resize
  onResizeStart({
    context
  }) {
    const me = this,
      record = me.resolveTimeRangeRecord(context.element.closest(me.baseSelector)),
      rangeBodyEl = me.getBodyElementByRecord(record);
    Object.assign(context, {
      record,
      rangeBodyEl
    });
    me.showTip(context);
  }
  onResizeDrag({
    context
  }) {
    const me = this,
      {
        rangeBodyEl
      } = context,
      {
        client
      } = me,
      box = Rectangle.from(context.element),
      startPos = box.getStart(client.rtl, client.isHorizontal),
      endPos = box.getEnd(client.rtl, client.isHorizontal),
      startDate = client.getDateFromCoordinate(startPos, 'round', false),
      endDate = client.getDateFromCoordinate(endPos, 'round', false);
    if (me.client.isVertical) {
      if (context.edge === 'top') {
        DomHelper.setTranslateY(rangeBodyEl, context.newY);
      }
      rangeBodyEl.style.height = context.newHeight + 'px';
    } else {
      if (context.edge === 'left') {
        DomHelper.setTranslateX(rangeBodyEl, context.newX);
      }
      rangeBodyEl.style.width = context.newWidth + 'px';
    }
    me.updateDateIndicator({
      startDate,
      endDate
    });
  }
  onResize({
    context
  }) {
    if (!context.valid) {
      return this.onInvalidDrop({
        context
      });
    }
    const me = this,
      {
        client
      } = me,
      {
        rtl
      } = client,
      record = context.record,
      box = Rectangle.from(context.element),
      startPos = box.getStart(rtl, client.isHorizontal),
      endPos = box.getEnd(rtl, client.isHorizontal),
      newStart = client.getDateFromCoordinate(startPos, 'round', false),
      isStart = rtl && context.edge === 'right' || !rtl && context.edge === 'left' || context.edge === 'top',
      newEnd = client.getDateFromCoordinate(endPos, 'round', false),
      wasModified = isStart && record.startDate - newStart !== 0 || newEnd && record.endDate - newEnd !== 0;
    if (wasModified && newEnd > newStart) {
      if (isStart) {
        // could be that the drag operation placed the range with start/end outside the axis
        record.setStartDate(newStart, false);
      } else {
        record.setEndDate(newEnd, false);
      }
    } else {
      me.onInvalidResize({
        context
      });
    }
    me.destroyTip();
  }
  onInvalidResize({
    context
  }) {
    const me = this;
    me.resize.reset();
    // Allow DomSync to reapply original state
    context.rangeBodyEl.parentElement.lastDomConfig = context.rangeBodyEl.lastDomConfig = null;
    me.renderRanges();
    me.destroyTip();
  }
  //endregion
}

TimeRanges._$name = 'TimeRanges';
GridFeatureManager.registerFeature(TimeRanges, false, ['Scheduler', 'Gantt']);

/**
 * @module Scheduler/view/mixin/DelayedRecordsRendering
 */
/**
 * Mixin that implements scheduling/unscheduling a delayed row refresh.
 * @mixin
 * @internal
 */
var DelayedRecordsRendering = (Target => class DelayedRecordsRendering extends (Target || Base) {
  static $name = 'DelayedRecordsRendering';
  static configurable = {
    scheduledRecordsRefreshTimeout: 10
  };
  static get properties() {
    return {
      recordsToRefresh: new Set()
    };
  }
  beforeRenderRow({
    record
  }) {
    var _this$recordIsReadyFo;
    // unscheduler records refresh when corresponding rows are rendered
    if ((_this$recordIsReadyFo = this.recordIsReadyForRendering) !== null && _this$recordIsReadyFo !== void 0 && _this$recordIsReadyFo.call(this, record)) {
      this.unscheduleRecordRefresh(record);
    }
    return super.beforeRenderRow(...arguments);
  }
  cleanupScheduledRecord() {
    const {
      rowManager,
      store
    } = this;
    for (const record of [...this.recordsToRefresh]) {
      // Remove the record from to-refresh list if:
      // - it's not in the view store
      // - or it's not visible
      if (!record.stores.includes(store) || !rowManager.getRowById(record)) {
        this.recordsToRefresh.delete(record);
      }
    }
  }
  renderScheduledRecords() {
    const me = this;
    if (!me.refreshSuspended) {
      // remove invisible records from the set of scheduled
      me.cleanupScheduledRecord();
      const {
          rowManager
        } = me,
        records = [...me.recordsToRefresh],
        rows = records.map(record => rowManager.getRowById(record));
      if (rows.length) {
        rowManager.renderRows(rows);
        /**
         * This event fires when records which rendering
         * was previously scheduled is finally done.
         * @event scheduledRecordsRender
         * @param {Grid.view.Grid} source The component.
         * @param {Core.data.Model[]} records Rendered records.
         * @param {Grid.row.Row[]} rows Rendered rows.
         */
        me.trigger('scheduledRecordsRender', {
          records,
          rows
        });
      }
      if (me.recordsToRefresh.size) {
        me.scheduleRecordRefresh();
      }
    }
    // reschedule this call if view refresh is suspended
    else {
      me.scheduleRecordRefresh();
    }
  }
  /**
   * Cancels scheduled rows refresh.
   * @param {Core.data.Model|Core.data.Model[]|Boolean} [clearRecords=true] `true` to also clear the list of records
   * scheduled for refreshing. `false` will result only canceling the scheduled call and keeping intact
   * the list of records planned for refreshing.
   */
  unscheduleRecordRefresh(clearRecords = true) {
    const me = this;
    if (clearRecords === true) {
      me.recordsToRefresh.clear();
    } else if (clearRecords) {
      ArrayHelper.asArray(clearRecords).forEach(record => me.recordsToRefresh.delete(record));
    }
    if (me.scheduledRecordsRefreshTimer && !me.recordsToRefresh.size) {
      me.clearTimeout(me.scheduledRecordsRefreshTimer);
    }
  }
  /**
   * Schedules the provided record row refresh.
   * @param {Core.data.Model} records Record to refresh the row of.
   */
  scheduleRecordRefresh(records) {
    const me = this;
    if (records) {
      ArrayHelper.asArray(records).forEach(record => me.recordsToRefresh.add(record));
    }
    me.scheduledRecordsRefreshTimer = me.setTimeout({
      fn: 'renderScheduledRecords',
      delay: me.scheduledRecordsRefreshTimeout,
      cancelOutstanding: true
    });
  }
  get widgetClass() {}
});

class TimelineHistogramRendering extends Base {
  static configurable = {
    scrollBuffer: 0
  };
  construct(client) {
    super.construct();
    this.client = client;
  }
  init() {}
  onTimeAxisViewModelUpdate() {
    const {
      scrollable
    } = this.client.timeAxisSubGrid;
    // scrollLeft is the DOM's concept which is -ve in RTL mode.
    // scrollX i always the +ve scroll offset from the origin.
    // Both may be needed for different calculations.
    this.updateFromHorizontalScroll(scrollable.x);
  }
  // Update header range on horizontal scroll
  updateFromHorizontalScroll(scrollX) {
    const me = this,
      {
        client,
        // scrollBuffer is an export only thing
        scrollBuffer
      } = me,
      {
        timeAxisSubGrid,
        timeAxis,
        rtl
      } = client,
      {
        width
      } = timeAxisSubGrid,
      {
        totalSize
      } = client.timeAxisViewModel,
      start = scrollX,
      // If there are few pixels left from the right most position then just render all remaining ticks,
      // there wouldn't be many. It makes end date reachable with more page zoom levels while not having any poor
      // implications.
      // 5px to make TimeViewRangePageZoom test stable in puppeteer.
      returnEnd = timeAxisSubGrid.scrollable.maxX !== 0 && Math.abs(timeAxisSubGrid.scrollable.maxX) <= Math.round(start) + 5,
      startDate = client.getDateFromCoord({
        coord: Math.max(0, start - scrollBuffer),
        ignoreRTL: true
      }),
      endDate = returnEnd ? timeAxis.endDate : client.getDateFromCoord({
        coord: start + width + scrollBuffer,
        ignoreRTL: true
      }) || timeAxis.endDate;
    if (startDate && !client._viewPresetChanging) {
      me._visibleDateRange = {
        startDate,
        endDate,
        startMS: startDate.getTime(),
        endMS: endDate.getTime()
      };
      me.viewportCoords = rtl
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
      const range = client.timeView.range = {
        startDate,
        endDate
      };
      client.onVisibleDateRangeChange(range);
      // If refresh is suspended, someone else is responsible for updating the UI later
      if (!client.refreshSuspended && client.rowManager.rows.length) {
        // Gets here too early in Safari for ResourceHistogram. ResizeObserver triggers a scroll before rows are
        // rendered first time. Could not track down why, bailing out
        if (client.rowManager.rows[0].id === null) {
          return;
        }
        // re-render all rows is timeAxis range has been updated
        if (me._timeAxisStartDate - timeAxis.startDate || me._timeAxisEndDate - timeAxis.endDate) {
          me._timeAxisStartDate = timeAxis.startDate;
          me._timeAxisEndDate = timeAxis.endDate;
          client.rowManager.renderRows(client.rowManager.rows);
        }
      }
    }
  }
  onViewportResize() {}
  refreshRows() {}
  get visibleDateRange() {
    return this._visibleDateRange;
  }
  translateToPageCoordinate(x) {
    const {
        client
      } = this,
      {
        scrollable
      } = client.timeAxisSubGrid;
    let result = x + client.timeAxisSubGridElement.getBoundingClientRect().left;
    if (client.rtl) {
      result -= scrollable.maxX - Math.abs(client.scrollLeft);
    } else {
      result -= client.scrollLeft;
    }
    return result;
  }
  translateToScheduleCoordinate(x) {
    const {
        client
      } = this,
      {
        scrollable
      } = client.timeAxisSubGrid;
    let result = x - client.timeAxisSubGridElement.getBoundingClientRect().left - globalThis.scrollX;
    // Because we use getBoundingClientRect's left, we have to adjust for page scroll.
    if (client.rtl) {
      result += scrollable.maxX - Math.abs(client.scrollLeft);
    } else {
      result += client.scrollLeft;
    }
    return result;
  }
  getDateFromXY(xy, roundingMethod, local, allowOutOfRange = false) {
    const {
      client
    } = this;
    let coord = xy[0];
    if (!local) {
      coord = this.translateToScheduleCoordinate(coord);
    }
    coord = client.getRtlX(coord);
    return client.timeAxisViewModel.getDateFromPosition(coord, roundingMethod, allowOutOfRange);
  }
}
TimelineHistogramRendering._$name = 'TimelineHistogramRendering';

/**
 * @module Scheduler/view/TimelineHistogramBase
 */
const histogramWidgetCleanState = {
    series: null,
    topValue: null
  },
  emptyFn = () => {};
/**
 * Histogram renderer parameters.
 *
 * @typedef {Object} HistogramRenderData
 * @property {Object} histogramData Histogram data
 * @property {HistogramConfig} histogramConfig Configuration object for the histogram widget
 * @property {HTMLElement|null} cellElement Cell element, for adding CSS classes, styling etc.
 *        Can be `null` in case of export
 * @property {Core.data.Model} record Record for the row
 * @property {Grid.column.Column} column This column
 * @property {Grid.view.Grid} grid This grid
 * @property {Grid.row.Row} row Row object. Can be null in case of export. Use the
 * {@link Grid.row.Row#function-assignCls row's API} to manipulate CSS class names.
 */
/**
 * Base class for {@link Scheduler/view/TimelineHistogram} class.
 *
 * @extends Scheduler/view/TimelineBase
 * @abstract
 */
class TimelineHistogramBase extends TimelineBase.mixin(DelayedRecordsRendering) {
  //region Config
  static $name = 'TimelineHistogramBase';
  static type = 'timelinehistogrambase';
  static configurable = {
    timeAxisColumnCellCls: 'b-sch-timeaxis-cell b-timelinehistogram-cell',
    mode: 'horizontal',
    rowHeight: 50,
    /**
     * Set to `true` if you want to display a tooltip when hovering an allocation bar. You can also pass a
     * {@link Core/widget/Tooltip#configs} config object.
     * Please use {@link #config-barTooltipTemplate} function to customize the tooltip contents.
     * @config {Boolean|TooltipConfig}
     */
    showBarTip: false,
    barTooltip: null,
    barTooltipClass: Tooltip,
    /**
     * Object enumerating data series for the histogram.
     * The object keys are treated as the series identifiers and values are objects that
     * must contain two properties:
     *  - `type` A String, either `'bar'` or `'outline'`
     *  - `field` A String, the name of the property to use from the data objects in the {@link #config-data} option.
     *
     * ```javascript
     * histogram = new TimelineHistogram({
     *     ...
     *     series : {
     *         s1 : {
     *             type  : 'bar',
     *             field : 's1'
     *         },
     *         s2 : {
     *             type  : 'outline',
     *             field : 's2'
     *         }
     *     },
     *     store : new Store({
     *         data : [
     *             {
     *                 id            : 'r1',
     *                 name          : 'Record 1',
     *                 histogramData : [
     *                     { s1 : 200, s2 : 100 },
     *                     { s1 : 150, s2 : 50 },
     *                     { s1 : 175, s2 : 50 },
     *                     { s1 : 175, s2 : 75 }
     *                 ]
     *             },
     *             {
     *                 id            : 'r2',
     *                 name          : 'Record 2',
     *                 histogramData : [
     *                     { s1 : 100, s2 : 100 },
     *                     { s1 : 150, s2 : 125 },
     *                     { s1 : 175, s2 : 150 },
     *                     { s1 : 175, s2 : 75 }
     *                 ]
     *             }
     *         ]
     *     })
     * });
     * ```
     *
     * @config {Object<String, HistogramSeries>}
     */
    series: null,
    /**
     * Record field from which the histogram data will be collected.
     *
     * ```javascript
     * histogram = new TimelineHistogram({
     *     ...
     *     series : {
     *         s1 : {
     *             type : 'bar'
     *         }
     *     },
     *     dataModelField : 'foo',
     *     store : new Store({
     *         data : [
     *             {
     *                 id   : 'r1',
     *                 name : 'Record 1',
     *                 foo  : [
     *                     { s1 : 200 },
     *                     { s1 : 150 },
     *                     { s1 : 175 },
     *                     { s1 : 175 }
     *                 ]
     *             },
     *             {
     *                 id   : 'r2',
     *                 name : 'Record 2',
     *                 foo  : [
     *                     { s1 : 100 },
     *                     { s1 : 150 },
     *                     { s1 : 175 },
     *                     { s1 : 175 }
     *                 ]
     *             }
     *         ]
     *     })
     * });
     * ```
     *
     * Alternatively {@link #config-getRecordData} function can be used to build a
     * record's histogram data dynamically.
     * @config {String}
     * @default
     */
    dataModelField: 'histogramData',
    /**
     * A function, or name of a function which builds histogram data for the provided record.
     *
     * See also {@link #config-dataModelField} allowing to load histogram data from a record field.
     *
     * @config {Function|String} getRecordData
     * @param {Core.data.Model} getRecordData.record Record to get histogram data for.
     * @param {Object} [aggregationContext] Context object passed in case the data is being retrieved
     * as a part of some parent record data collecting.
     * @returns {Object} Histogram data.
     */
    getRecordData: null,
    /**
     * When set to `true` (default) the component reacts on time axis changes
     * (zooming or changing the displayed time span), clears the histogram data cache of the records
     * and then refreshes the view.
     * @config {Boolean}
     * @default
     */
    hardRefreshOnTimeAxisReconfigure: true,
    /**
     * A Function which returns a CSS class name to add to a rectangle element.
     * The following parameters are passed:
     * @param {HistogramSeries} series The series being rendered
     * @param {DomConfig} rectConfig The rectangle configuration object
     * @param {Object} datum The datum being rendered
     * @param {Number} index The index of the datum being rendered
     * @param {HistogramRenderData} renderData Current render data giving access to the record, row and cell
     * being rendered.
     * @returns {String} CSS classes of the rectangle element
     * @config {Function}
     */
    getRectClass: null,
    /**
     * A Function which returns a CSS class name to add to a path element
     * built for an `outline` type series.
     * The following parameters are passed:
     * @param {HistogramSeries} series The series being rendered
     * @param {Object[]} data The series data
     * @param {HistogramRenderData} renderData Current render data giving access to the record, row and cell
     * being rendered.
     * @returns {String} CSS class name of the path element
     * @config {Function}
     */
    getOutlineClass(series) {
      return '';
    },
    readOnly: true,
    /**
     * A Function which returns the tooltip text to display when hovering a bar.
     * The following parameters are passed:
     * @param {HistogramSeries} series The series being rendered
     * @param {DomConfig} rectConfig The rectangle configuration object
     * @param {Object} datum The datum being rendered
     * @param {Number} index The index of the datum being rendered
     * @deprecated Since 5.0.0. Please use {@link #config-barTooltipTemplate}
     * @config {Function}
     */
    getBarTip: null,
    /**
     * A Function which returns the tooltip text to display when hovering a bar.
     * The following parameters are passed:
     * @param {Object} context The tooltip context info
     * @param {Object} context.datum The histogram bar being hovered info
     * @param {Core.widget.Tooltip} context.tip The tooltip instance
     * @param {HTMLElement} context.element The Element for which the Tooltip is monitoring mouse movement
     * @param {HTMLElement} context.activeTarget The target element that triggered the show
     * @param {Event} context.event The raw DOM event
     * @param {Core.data.Model} data.record The record which value
     * the hovered bar displays.
     * @returns {String} Tooltip HTML content
     * @config {Function}
     */
    barTooltipTemplate: null,
    /**
     * A Function which returns the text to render inside a bar.
     *
     * ```javascript
     * new TimelineHistogram({
     *     series : {
     *         foo : {
     *             type  : 'bar',
     *             field : 'foo'
     *         }
     *     },
     *     getBarText(datum) {
     *         // display the value in the bar
     *         return datum.foo;
     *     },
     *     ...
     * })
     * ```
     *
     * **Please note** that the function will be injected into the underlying
     * {@link Core/widget/graph/Histogram} component that is used under the hood
     * to render actual charts.
     * So `this` will refer to the {@link Core/widget/graph/Histogram} instance, not
     * this class instance.
     * To access the view please use `this.owner` in the function:
     *
     * ```javascript
     * new TimelineHistogram({
     *     getBarText(datum) {
     *         // "this" in the method refers core Histogram instance
     *         // get the view instance
     *         const timelineHistogram = this.owner;
     *
     *         .....
     *     },
     *     ...
     * })
     * ```
     * The following parameters are passed:
     * @param {Object} datum The datum being rendered
     * @param {Number} index The index of the datum being rendered
     * @param {HistogramSeries} series The series (provided if histogram widget
     * {@link Core/widget/graph/Histogram#config-singleTextForAllBars} is `false`)
     * @param {HistogramRenderData} renderData Current render data giving access to the record, row and cell
     * being rendered.
     * @returns {String} Text to render inside the bar
     * @config {Function}
     */
    getBarText: null,
    getRectConfig: null,
    getBarTextRenderData: undefined,
    /**
     * The class used for building the {@link #property-histogramWidget histogram widget}
     * @config {Core.widget.graph.Histogram}
     * @default
     */
    histogramWidgetClass: Histogram,
    /**
     * The underlying {@link Core/widget/graph/Histogram} component that is used under the hood
     * to render actual charts.
     * @member {Core.widget.graph.Histogram} histogramWidget
     */
    /**
     * An instance or a configuration object of the underlying {@link Core/widget/graph/Histogram}
     * component that is used under the hood to render actual charts.
     * In case a configuration object is provided the built class is defined with
     * {@link #config-histogramWidgetClass} config.
     * @config {Core.widget.graph.Histogram|HistogramConfig}
     */
    histogramWidget: {
      cls: 'b-hide-offscreen b-timelinehistogram-histogram',
      omitZeroHeightBars: true,
      data: []
    },
    fixedRowHeight: true
  };
  static get properties() {
    return {
      histogramDataByRecord: new Map(),
      collectingDataFor: new Map()
    };
  }
  updateGetRecordData(fn) {
    this._getRecordData = fn ? this.resolveCallback(fn) : null;
  }
  updateHardRefreshOnTimeAxisReconfigure(value) {
    const name = 'hardRefreshOnTimeAxisReconfigure';
    if (value) {
      this.timeAxis.ion({
        name,
        endReconfigure: 'onTimeAxisEndReconfigure',
        thisObj: this
      });
    } else {
      this.detachListeners(name);
    }
  }
  //endregion
  //region Constructor/Destructor
  construct(config) {
    super.construct(config);
    const me = this;
    // debounce refreshRows calls
    me.scheduleRefreshRows = me.createOnFrame(me.refreshRows, [], me, true);
    me.rowManager.ion({
      beforeRowHeight: 'onBeforeRowHeight',
      thisObj: me
    });
  }
  onDestroy() {
    var _this$_histogramWidge;
    this.clearHistogramDataCache();
    (_this$_histogramWidge = this._histogramWidget) === null || _this$_histogramWidge === void 0 ? void 0 : _this$_histogramWidge.destroy();
    this.barTooltip = null;
  }
  //endregion
  //region Internal
  // Used by shared features to resolve an event or task
  resolveTimeSpanRecord(element) {}
  getScheduleMouseEventParams(cellData, event) {
    const record = this.store.getById(cellData.id);
    return {
      record
    };
  }
  get currentOrientation() {
    if (!this._currentOrientation) {
      this._currentOrientation = new TimelineHistogramRendering(this);
    }
    return this._currentOrientation;
  }
  updateSeries(value) {
    const me = this;
    me.histogramWidget.series = value;
    me._series = me.histogramWidget.series;
    if (me.isPainted && !me.isConfiguring) {
      me.scheduleRefreshRows();
    }
  }
  getAsyncEventSuffixForStore(store) {
    // Use xxPreCommit version of events if the store is a part of a project
    return store.isAbstractPartOfProjectStoreMixin ? 'PreCommit' : '';
  }
  /**
   * Schedules the component rows refresh on the next animation frame. However many time it is
   * called in one event run, it will only be scheduled to run once.
   */
  scheduleRefreshRows() {}
  getRowHeight() {
    return this.rowHeight;
  }
  onPaint({
    firstPaint
  }) {
    super.onPaint({
      firstPaint
    });
    if (firstPaint && this.showBarTip) {
      this.barTooltip = {};
    }
  }
  updateGetBarTip(value) {
    // reset barTooltipTemplate if custom getBarTip function is provided
    if (value) {
      this.barTooltipTemplate = null;
    }
    return value;
  }
  changeBarTooltip(tooltip, oldTooltip) {
    oldTooltip === null || oldTooltip === void 0 ? void 0 : oldTooltip.destroy();
    if (tooltip) {
      return tooltip.isTooltip ? tooltip : this.barTooltipClass.new({
        forElement: this.timeAxisSubGridElement,
        forSelector: '.b-histogram rect',
        hoverDelay: 0,
        trackMouse: false,
        cls: 'b-celltooltip-tip',
        getHtml: this.getTipHtml.bind(this)
      }, this.showBarTip, tooltip);
    }
    return null;
  }
  async getTipHtml(args) {
    if (this.showBarTip && this.barTooltipTemplate) {
      const {
          activeTarget
        } = args,
        index = parseInt(activeTarget.dataset.index, 10),
        record = this.getRecordFromElement(activeTarget),
        histogramData = await this.getRecordHistogramData(record);
      return this.barTooltipTemplate({
        ...args,
        datum: this.extractHistogramDataArray(histogramData, record)[index],
        record,
        index
      });
    }
  }
  collectTicksWidth() {
    const {
        ticks
      } = this.timeAxis,
      prevDuration = ticks[0].endDate - ticks[0].startDate,
      tickDurations = {
        0: prevDuration
      };
    let totalDuration = prevDuration,
      isMonotonous = true;
    for (let i = 1, {
        length
      } = ticks; i < length; i++) {
      const tick = ticks[i],
        duration = tick.endDate - tick.startDate;
      // the ticks width is different -> reset isMonotonous flag
      if (prevDuration !== duration) {
        isMonotonous = false;
      }
      totalDuration += duration;
      tickDurations[i] = duration;
    }
    // if the ticks widths are not monotonous we need to calculate
    // each bar width to provide it to the histogram widget later
    if (!isMonotonous) {
      const ticksWidth = {};
      for (let i = 0, {
          length
        } = ticks; i < length; i++) {
        ticksWidth[i] = tickDurations[i] / totalDuration;
      }
      this.ticksWidth = ticksWidth;
    } else {
      this.ticksWidth = null;
    }
  }
  changeHistogramWidget(widget) {
    const me = this;
    if (widget && !widget.isHistogram) {
      var _me$timeAxisColumn;
      if (me.getBarTextRenderData && !widget.getBarTextRenderData) {
        widget.getBarTextRenderData = me.getBarTextRenderData;
      }
      widget = me.histogramWidgetClass.new({
        owner: me,
        appendTo: me.element,
        height: me.rowHeight,
        width: ((_me$timeAxisColumn = me.timeAxisColumn) === null || _me$timeAxisColumn === void 0 ? void 0 : _me$timeAxisColumn.width) || 0,
        getBarTip: !me.barTooltipTemplate && me.getBarTip || emptyFn,
        getRectClass: me.getRectClass || me.getRectClassDefault,
        getBarText: me.getBarText || me.getBarTextDefault,
        getOutlineClass: me.getOutlineClass,
        getRectConfig: me.getRectConfig
      }, widget);
      widget.suspendRefresh();
      // bind default getBarText in case it will be called from a custom getBarText()
      me.getBarTextDefault = me.getBarTextDefault.bind(widget);
    }
    return widget;
  }
  // Injectable method.
  getRectClassDefault(series, rectConfig, datum) {}
  getBarTextDefault(datum, index) {}
  updateShowBarTip(value) {
    this.barTooltip = value;
  }
  //endregion
  //region Columns
  get columns() {
    return super.columns;
  }
  set columns(columns) {
    const me = this;
    super.columns = columns;
    if (!me.isDestroying) {
      me.timeAxisColumn.renderer = me.histogramRenderer.bind(me);
      me.timeAxisColumn.cellCls = me.timeAxisColumnCellCls;
    }
  }
  //endregion
  //region Events
  onHistogramDataCacheSet({
    record,
    data
  }) {
    // schedule record refresh for later
    this.scheduleRecordRefresh(record);
  }
  onTimeAxisEndReconfigure() {
    if (this.hardRefreshOnTimeAxisReconfigure) {
      // reset histogram cache
      this.clearHistogramDataCache();
      // schedule records refresh (that will re-fetch the histogram data from the server since the cache is empty)
      this.scheduleRefreshRows();
    }
  }
  onStoreUpdateRecord({
    record,
    changes
  }) {
    const me = this;
    // If we read histogram data from a field and that field got changed
    // - clear the corresponding record cache
    if (!me.getRecordData && me.dataModelField && changes[me.dataModelField]) {
      me.clearHistogramDataCache(record);
    }
    return super.onStoreUpdateRecord(...arguments);
  }
  onStoreRemove({
    records
  }) {
    super.onStoreRemove(...arguments);
    for (const record of records) {
      this.clearHistogramDataCache(record);
    }
  }
  onBeforeRowHeight({
    height
  }) {
    if (this._timeAxisColumn) {
      const widget = this._histogramWidget;
      if (widget) {
        widget.height = height;
        widget.onElementResize(widget.element);
      }
    }
  }
  onTimeAxisViewModelUpdate() {
    super.onTimeAxisViewModelUpdate(...arguments);
    const widget = this._histogramWidget;
    if (widget) {
      widget.width = this.timeAxisViewModel.totalSize;
      widget.onElementResize(widget.element);
    }
    this.collectTicksWidth();
  }
  //endregion
  //region Data processing
  extractHistogramDataArray(histogramData, record) {
    return histogramData;
  }
  processRecordRenderData(renderData) {
    return renderData;
  }
  /**
   * Clears the histogram data cache for the provided record (if provided).
   * If the record is not provided clears the cache for all records.
   * @param {Core.data.Model} [record] Record to clear the cache for.
   */
  clearHistogramDataCache(record) {
    if (record) {
      this.histogramDataByRecord.delete(record);
    } else {
      this.histogramDataByRecord.clear();
    }
  }
  /**
   * Caches the provided histogram data for the given record.
   * @param {Core.data.Model} record Record to cache data for.
   * @param {Object} data Histogram data to cache.
   */
  setHistogramDataCache(record, data) {
    const eventData = {
      record,
      data
    };
    /**
     * Fires before the component stores a record's histogram data into the cache.
     *
     * A listener can be used to transform the collected data dynamically before
     * it's cached:
     *
     * ```javascript
     * new TimelineHistogram({
     *     series : {
     *         foo : {
     *             type  : 'bar',
     *             field : 'f1'
     *         }
     *     },
     *     ...
     *     listeners : {
     *         beforeHistogramDataCacheSet(eventData) {
     *             // completely replace the data for a specific record
     *             if (eventData.record.id === 123) {
     *                 eventData.data = [
     *                     { f1 : 10 },
     *                     { f1 : 20 },
     *                     { f1 : 30 },
     *                     { f1 : 40 },
     *                     { f1 : 50 },
     *                     { f1 : 60 }
     *                 ];
     *             }
     *         }
     *     }
     * })
     * ```
     *
     * @param {Scheduler.view.TimelineHistogram} source The component instance
     * @param {Core.data.Model} record Record the histogram data of which is ready.
     * @param {Object} data The record histogram data.
     * @event beforeHistogramDataCacheSet
     */
    this.trigger('beforeHistogramDataCacheSet', eventData);
    this.histogramDataByRecord.set(eventData.record, eventData.data);
    /**
     * Fires after the component retrieves a record's histogram data and stores
     * it into the cache.
     *
     * Unlike similar {@link #event-beforeHistogramDataCacheSet} event this event is triggered
     * after the data is put into the cache.
     *
     * A listener can be used to transform the collected data dynamically:
     *
     * ```javascript
     * new TimelineHistogram({
     *     series : {
     *         bar : {
     *             type : 'bar',
     *             field : 'bar'
     *         },
     *         halfOfBar : {
     *             type  : 'outline',
     *             field : 'half'
     *         }
     *     },
     *     ...
     *     listeners : {
     *         histogramDataCacheSet({ data }) {
     *             // add extra entries to collected data
     *             data.forEach(entry => {
     *                 entry.half = entry.bar / 2;
     *             });
     *         }
     *     }
     * })
     * ```
     *
     * @param {Scheduler.view.TimelineHistogram} source The component instance
     * @param {Core.data.Model} record Record the histogram data of which is ready.
     * @param {Object} data The record histogram data.
     * @event histogramDataCacheSet
     */
    this.trigger('histogramDataCacheSet', eventData);
  }
  /**
   * Returns entire histogram data cache if no record provided,
   * or cached data for the provided record.
   * @param {Core.data.Model} [record] Record to get the cached data for.
   * @returns {Object} The provided record cached data or all the records data cache
   * as a `Map` keyed by records.
   */
  getHistogramDataCache(record) {
    return record ? this.histogramDataByRecord.get(record) : this.histogramDataByRecord;
  }
  /**
   * Returns `true` if there is cached histogram data for the provided record.
   * @param {Core.data.Model} record Record to check the cache existence for.
   * @returns {Boolean} `True` if there is a cache for provided record.
   */
  hasHistogramDataCache(record) {
    return this.histogramDataByRecord.has(record);
  }
  finalizeDataRetrievingInternal(record, data) {
    // cleanup collectingDataFor map on data collecting completion
    this.collectingDataFor.delete(record);
    // cache record data
    this.setHistogramDataCache(record, data);
    // pass data through
    return data;
  }
  finalizeDataRetrieving(record, data) {
    if (Objects.isPromise(data)) {
      this.collectingDataFor.set(record, data);
      return data.then(data => this.finalizeDataRetrievingInternal(record, data));
    }
    return this.finalizeDataRetrievingInternal(record, data);
  }
  /**
   * Retrieves the histogram data for the provided record.
   *
   * The method first checks if there is cached data for the record and returns it if found.
   * Otherwise it starts collecting data by calling {@link #config-getRecordData} (if provided)
   * or by reading it from {@link #config-dataModelField} record field.
   *
   * The method can be asynchronous depending on the provided {@link #config-getRecordData} function.
   * If the function returns a `Promise` then the method will return a wrapping `Promise` in turn that will
   * resolve with the collected histogram data.
   *
   * The method triggers {@link #event-histogramDataCacheSet} event when a record data is ready.
   *
   * @param {Core.data.Model} record Record to retrieve the histogram data for.
   * @returns {Object|Promise} The histogram data for the provided record or a `Promise` that will provide the data
   * when resolved.
   */
  getRecordHistogramData(record) {
    const me = this,
      {
        getRecordData
      } = me;
    let result = me.collectingDataFor.get(record) || me.getHistogramDataCache(record);
    if (!result && !me.hasHistogramDataCache(record)) {
      // use "getRecordData" function if provided
      if (getRecordData) {
        result = getRecordData.handler.call(getRecordData.thisObj, ...arguments);
      }
      // or read data from the configured model field
      else {
        result = record.get(me.dataModelField);
      }
      result = me.finalizeDataRetrieving(record, result);
    }
    return result;
  }
  recordIsReadyForRendering(record) {
    return !this.collectingDataFor.has(record);
  }
  //endregion
  //region Render
  beforeRenderRow(eventData) {
    const me = this,
      histogramData = me.getRecordHistogramData(eventData.record);
    if (!Objects.isPromise(histogramData)) {
      const data = histogramData ? me.extractHistogramDataArray(histogramData, eventData.record) : [];
      // if ticks widths are not monotonous
      // we provide widths for each bar since in that case the histogram widget
      // won't be able to calculate them properly
      if (me.ticksWidth) {
        for (let i = 0, {
            length
          } = data; i < length; i++) {
          data[i].width = me.ticksWidth[i];
        }
      }
      const histogramConfig = Objects.merge(
      // reset topValue by default to enable its auto-detection
      {
        topValue: null
      }, me.initialConfig.histogramWidget, {
        data,
        series: {
          ...me.series
        }
      });
      eventData = {
        ...eventData,
        histogramConfig,
        histogramData,
        histogramWidget: me.histogramWidget
      };
      /**
       * Fires before the component renders a row.
       *
       * This event is recommended to use instead of generic {@link #event-beforeRenderRow} event since
       * the component bails out of rendering rows for which histogram data is not ready yet
       * (happens in case of async data collecting). The generic {@link #event-beforeRenderRow}
       * is triggered in such cases too while this event is triggered only when the data is ready and the
       * row is actually about to be rendered.
       *
       * Use a listener to adjust histograms rendering dynamically for individual rows:
       *
       * ```javascript
       * new TimelineHistogram({
       *     ...
       *     listeners : {
       *         beforeRenderHistogramRow({ record, histogramConfig }) {
       *             // display an extra line for some specific record
       *             if (record.id == 111) {
       *                 histogramConfig.series.extraLine = {
       *                     type  : 'outline',
       *                     field : 'foo'
       *                 };
       *             }
       *         }
       *     }
       * })
       * ```
       *
       * @param {Scheduler.view.TimelineHistogram} source The component instance
       * @param {Core.data.Model} record Record the histogram data of which is ready.
       * @param {HistogramConfig} histogramConfig Configuration object that will be applied to `histogramWidget`.
       * @param {Core.widget.graph.Histogram} histogramWidget The underlying widget that is used to render a chart.
       * @event beforeRenderHistogramRow
       */
      me.trigger('beforeRenderHistogramRow', eventData);
      // We are going to use eventData as stored renderData
      // so sanitize it from unwanted properties
      delete eventData.eventName;
      delete eventData.source;
      delete eventData.type;
      delete eventData.oldId;
      delete eventData.row;
      delete eventData.recordIndex;
      me._recordRenderData = me.processRecordRenderData(eventData);
    }
    super.beforeRenderRow(...arguments);
  }
  applyHistogramWidgetConfig(histogramWidget = this.histogramWidget, histogramConfig) {
    // reset some parameters (topValue and series) to force recalculations
    // and apply new configuration after
    Object.assign(histogramWidget, histogramWidgetCleanState, histogramConfig);
  }
  /**
   * Renders a histogram for a row.
   * The method applies passed data to the underlying {@link #property-histogramWidget} component.
   * Then the component renders charts and the method injects them into the corresponding column cell.
   * @param {HistogramRenderData} renderData Render data
   * @internal
   */
  renderRecordHistogram(renderData) {
    const me = this,
      {
        histogramData,
        cellElement
      } = renderData;
    // reset the cell for rows not having histogram data
    if (!histogramData) {
      cellElement.innerHTML = '';
      return;
    }
    /**
     * Fires before the component renders a histogram in a cell.
     *
     * @param {Scheduler.view.TimelineHistogram} source The component instance
     * @param {Core.data.Model} record Record the histogram data of which is ready.
     * @param {HistogramConfig} histogramConfig Configuration object that will be applied to `histogramWidget`.
     * @param {Core.widget.graph.Histogram} histogramWidget The underlying widget that is used to render a chart.
     * @event beforeRenderRecordHistogram
     */
    me.trigger('beforeRenderRecordHistogram', renderData);
    // sanitize renderData from unwanted properties
    delete renderData.eventName;
    delete renderData.type;
    delete renderData.source;
    const histogramWidget = renderData.histogramWidget || me.histogramWidget;
    me.applyHistogramWidgetConfig(histogramWidget, renderData.histogramConfig);
    histogramWidget.refresh({
      // tell histogram we want it to pass renderData as an extra argument in nested calls of getBarText and
      // other configured hooks
      args: [renderData]
    });
    const histogramCloneElement = histogramWidget.element.cloneNode(true);
    histogramCloneElement.removeAttribute('id');
    histogramCloneElement.classList.remove('b-hide-offscreen');
    cellElement.innerHTML = '';
    cellElement.appendChild(histogramCloneElement);
  }
  /**
   * TimeAxis column renderer used by this view to render row histograms.
   * It first calls {@link #function-getRecordHistogramData} method to retrieve
   * the histogram data for the renderer record.
   * If the record data is ready the method renders the record histogram.
   * And in case the method returns a `Promise` the renderer just
   * schedules the record refresh for later and exits.
   *
   * @param {HistogramRenderData} renderData Object containing renderer parameters.
   * @internal
   */
  histogramRenderer(renderData) {
    const me = this,
      histogramData = renderData.histogramData || me.getRecordHistogramData(renderData.record);
    // If the data is ready we just render a histogram
    // Otherwise we render nothing and the rendering will happen once the data is ready
    // (which is signalized by histogramDataCacheSet event)
    if (!Objects.isPromise(histogramData)) {
      Object.assign(renderData, me._recordRenderData);
      return me.renderRecordHistogram(...arguments);
    }
    return '';
  }
  /**
   * Group feature hook triggered by the feature to render group headers
   * @param {*} renderData
   * @internal
   */
  buildGroupHeader(renderData) {
    if (renderData.column === this.timeAxisColumn) {
      return this.histogramRenderer(renderData);
    }
    return this.features.group.buildGroupHeader(renderData);
  }
  //endregion
  get widgetClass() {}
}
TimelineHistogramBase.initClass();
TimelineHistogramBase._$name = 'TimelineHistogramBase';

/**
 * @module Scheduler/view/mixin/TimelineHistogramGrouping
 */
/**
 * Mixin for {@link Scheduler/view/TimelineHistogram} that provides record grouping support.
 * The class implements API to work with groups and their members and allows to rollup group members data
 * to their parents.
 *
 * The _groups_ here are either group headers built with the {@link Grid/feature/Group} feature or
 * parent nodes built with the {@link Grid/feature/TreeGroup} feature.
 *
 * ## Parent histogram data aggregating
 *
 * The mixin provides a {@link #config-aggregateHistogramDataForGroups} config which enables automatically rolling up
 * child records histogram data to their parents. By default all registered {@link #config-series}' values are
 * just summed up on parents level, but that can be changed by providing `aggregate`
 * config to {@link #config-series}:
 *
 * ```javascript
 * new TimelineHistogram({
 *     series : {
 *         salary : {
 *            type : 'bar',
 *            // show maximum value on the parent level
 *            aggregate : 'max'
 *         }
 *     },
 *     ...
 * })
 * ```
 *
 * Here is the list of supported `aggregate` values:
 *
 * - `sum` or `add` - sum of values in the group (default)
 * - `min` - minimum value in the group
 * - `max` - maximum value in the group
 * - `count` - number of child records in the group
 * - `avg` - average of the child values in the group
 *
 * There are a few hooks allowing customization of the rolling up process:
 * {@link #config-aggregateDataEntry}, {@link #config-getDataEntryForAggregating} and
 * {@link #config-initAggregatedDataEntry}.
 *
 * @extends Scheduler/view/TimelineHistogramBase
 * @mixin
 */
var TimelineHistogramGrouping = (Target => class TimelineHistogramGrouping extends (Target || TimelineHistogramBase) {
  static $name = 'TimelineHistogramGrouping';
  //region Configs
  static configurable = {
    /**
     * When `true` the component will automatically calculate data for group records
     * based on the groups members data by calling {@link #function-getGroupRecordHistogramData} method.
     * @config {Boolean}
     * @category Parent histogram data collecting
     * @default
     */
    aggregateHistogramDataForGroups: true,
    /**
     * A function used for aggregating child records histogram data entries to their parent entry.
     *
     * It's called for each child entry and is meant to apply the child entry values to the
     * target parent entry (provided in `aggregated` parameter).
     * The function must return the resulting aggregated entry that will be passed as `aggregated`
     * parameter to the next __aggregating__ step.
     *
     * Should be provided as a function, or name of a function in the ownership hierarchy which may be called.
     * @config {Function|String} aggregateDataEntry
     * @param {Object} aggregateDataEntry.aggregated Target parent data entry to aggregate the entry into.
     * @param {Object} aggregateDataEntry.entry Current entry to aggregate into `aggregated`.
     * @param {Number} aggregateDataEntry.arrayIndex Index of current array (index of the record among other
     * records being aggregated).
     * @param {Object[]} aggregateDataEntry.entryIndex Index of `entry` in the current array.
     * @returns {Object} Return value becomes the value of the `aggregated` parameter on the next
     * invocation of this function.
     * @category Parent histogram data collecting
     * @default
     */
    aggregateDataEntry: null,
    /**
     * Function that extracts a record histogram data entry for aggregating.
     * By default it returns the entry as is. Override the function if you need a more complex way
     * to retrieve the value for aggregating.
     *
     * Should be provided as a function, or name of a function in the ownership hierarchy which may be called.
     * @config {Function|String} getDataEntryForAggregating
     * @param {Object} getDataEntryForAggregating.entry Current data entry.
     * @returns {Object} Entry to aggregate
     * @category Parent histogram data collecting
     * @default
     */
    getDataEntryForAggregating: null,
    /**
     * A function that initializes a target group record entry.
     *
     * Should be provided as a function, or name of a function in the ownership hierarchy which may be called.
     * @config {Function|String} initAggregatedDataEntry
     * @returns {Object} Target aggregated entry
     * @category Parent histogram data collecting
     * @default
     */
    initAggregatedDataEntry: null,
    aggregateFunctions: {
      sum: {
        aliases: ['add'],
        entry(seriesId, acc, entry) {
          acc[seriesId] = (acc[seriesId] || 0) + entry[seriesId];
          return acc;
        }
      },
      min: {
        entry(seriesId, acc, entry) {
          const entryValue = entry[seriesId];
          if (entryValue < (acc[seriesId] || Number.MAX_VALUE)) acc[seriesId] = entryValue;
          return acc;
        }
      },
      max: {
        entry(seriesId, acc, entry) {
          const entryValue = entry[seriesId];
          if (entryValue > (acc[seriesId] || Number.MIN_VALUE)) acc[seriesId] = entryValue;
          return acc;
        }
      },
      count: {
        init(seriesId, entry, entryIndex, aggregationContext) {
          entry[seriesId] = aggregationContext.arrays.length;
        }
      },
      avg: {
        entry(seriesId, acc, entry) {
          acc[seriesId] = (acc[seriesId] || 0) + entry[seriesId];
          return acc;
        },
        finalize(seriesId, data, recordsData, records, aggregationContext) {
          const cnt = aggregationContext.arrays.length;
          data.forEach(entry => entry[seriesId] /= cnt);
        }
      }
    }
  };
  afterConfigure() {
    const me = this;
    me.internalAggregateDataEntry = me.internalAggregateDataEntry.bind(this);
    me.internalInitAggregatedDataEntry = me.internalInitAggregatedDataEntry.bind(this);
    super.afterConfigure();
    if (me.features.treeGroup) {
      me.features.treeGroup.ion({
        // reset groups cache on store grouping change
        beforeDataLoad: me.onTreeGroupBeforeDataLoad,
        thisObj: me
      });
    }
  }
  updateAggregateFunctions(value) {
    for (const [id, fn] of Object.entries(value)) {
      fn.id = id;
      if (fn.aliases) {
        for (const alias of fn.aliases) {
          value[alias] = fn;
        }
      }
    }
  }
  updateStore(store) {
    super.updateStore(...arguments);
    this.detachListeners('store');
    if (store) {
      store.ion({
        name: 'store',
        // reset groups cache on store grouping change
        group: this.onStoreGroup,
        thisObj: this
      });
    }
  }
  changeAggregateDataEntry(fn) {
    return this.bindCallback(fn);
  }
  changeGetDataEntryForAggregating(fn) {
    return this.bindCallback(fn);
  }
  changeInitAggregatedDataEntry(fn) {
    return this.bindCallback(fn);
  }
  //endregion
  //region Event listeners
  onHistogramDataCacheSet({
    record,
    data
  }) {
    // schedule record refresh for later
    super.onHistogramDataCacheSet(...arguments);
    if (this.aggregateHistogramDataForGroups) {
      this.scheduleRecordParentsRefresh(record);
    }
  }
  onTreeGroupBeforeDataLoad() {
    if (this.aggregateHistogramDataForGroups) {
      // reset groups cache on store grouping change
      this.resetGeneratedRecordsHistogramDataCache();
    }
  }
  onStoreGroup() {
    if (this.aggregateHistogramDataForGroups) {
      // reset groups cache on store grouping change
      this.resetGeneratedRecordsHistogramDataCache();
    }
  }
  //endregion
  // Override getRecordHistogramData to support data aggregating for parents
  getRecordHistogramData(record, aggregationContext) {
    const me = this;
    let result;
    // If that's a group record and records aggregating is enabled
    // collect the aggregated data based on children
    if (me.aggregateHistogramDataForGroups && me.isGroupRecord(record)) {
      result = me.collectingDataFor.get(record) || me.getHistogramDataCache(record);
      if (!result && !me.hasHistogramDataCache(record)) {
        result = me.getGroupRecordHistogramData(record, aggregationContext);
        result = me.finalizeDataRetrieving(record, result);
      }
    } else {
      result = super.getRecordHistogramData(...arguments);
    }
    return result;
  }
  //region ArrayHelper.aggregate default callbacks
  internalAggregateDataEntry(acc, ...args) {
    const {
      aggregateFunctions
    } = this;
    // call series aggregate functions
    for (const {
      id,
      aggregate = 'sum'
    } of Object.values(this.series)) {
      let fn;
      if (aggregate !== false && (fn = aggregateFunctions[aggregate].entry)) {
        acc = fn(id, acc, ...args);
      }
    }
    return this.aggregateDataEntry ? this.aggregateDataEntry(acc, ...args) : acc;
  }
  internalInitAggregatedDataEntry() {
    const entry = this.initAggregatedDataEntry ? this.initAggregatedDataEntry(...arguments) : {},
      {
        aggregateFunctions
      } = this;
    // call series aggregate functions
    for (const {
      id,
      aggregate = 'sum'
    } of Object.values(this.series)) {
      const fn = aggregateFunctions[aggregate].init;
      if (fn && aggregate !== false) {
        fn(id, entry, ...arguments);
      }
    }
    return entry;
  }
  //endregion
  //region Public methods
  /**
   * Resets generated records (parents and links) data cache
   */
  resetGeneratedRecordsHistogramDataCache() {
    const {
      store
    } = this;
    for (const record of this.getHistogramDataCache().keys()) {
      // clear cache for generated parents and links no longer in the store
      if (record.isGroupHeader || record.generatedParent || record.isLinked && !store.includes(record)) {
        this.clearHistogramDataCache(record);
      }
    }
  }
  setHistogramDataCache(record, data) {
    super.setHistogramDataCache(record, data);
    // If that's a link let's update the original record cache too
    if (record.isLinked) {
      super.setHistogramDataCache(record.$original, data);
    }
    // if that's a record having links - update their caches too
    else if (record.$links) {
      const {
        store
      } = this;
      for (const link of record.$links) {
        // make sure the link belongs to this view store
        if (store.includes(link)) {
          super.setHistogramDataCache(link, data);
        }
      }
    }
  }
  // Override method to support links built by TreeGroup feature
  // so for the links the method will retrieve original records cache
  getHistogramDataCache(record) {
    let result = super.getHistogramDataCache(record);
    // if that's a link - try getting the original record cache
    if (!result && record.isLinked) {
      result = super.getHistogramDataCache(record.$original);
    }
    return result;
  }
  /**
   * Aggregates the provided group record children histogram data.
   * If some of the provided records data is not ready yet the method returns a `Promise`
   * that's resolved once the data is ready and aggregated.
   *
   * ```javascript
   * // get parent record aggregated histogram data
   * const aggregatedData = await histogram.getGroupRecordHistogramData(record);
   * ```
   *
   * @param {Core.data.Model} record Group record.
   * @param {Object} [aggregationContext] Optional aggregation context object.
   * When provided will be used as a shared object passed through while collecting the data.
   * So can be used for some custom application purposes.
   * @returns {Object[]|Promise} Either the provided group record histogram data or a `Promise` that
   * returns the data when resolved.
   * @category Parent histogram data collecting
   */
  getGroupRecordHistogramData(record, aggregationContext = {}) {
    aggregationContext.parentRecord = record;
    const result = this.aggregateRecordsHistogramData(this.getGroupChildren(record), aggregationContext);
    return Objects.isPromise(result) ? result.then(res => res) : result;
  }
  /**
   * Aggregates multiple records histogram data.
   * If some of the provided records data is not ready yet the method returns a `Promise`
   * that's resolved once the data is ready and aggregated.
   *
   * @param {Core.data.Model[]} records Records to aggregate data of.
   * @param {Object} [aggregationContext] Optional aggregation context object.
   * Can be used by to share some data between the aggregation steps.
   * @returns {Object[]|Promise} Either the provided group record histogram data or a `Promise` that
   * returns the data when resolved.
   * @category Parent histogram data collecting
   */
  aggregateRecordsHistogramData(records, aggregationContext = {}) {
    const me = this,
      recordsData = [],
      {
        parentRecord
      } = aggregationContext;
    let hasPromise = false;
    // collect children data
    for (const child of records) {
      const childData = me.getRecordHistogramData(child, aggregationContext);
      hasPromise = hasPromise || Objects.isPromise(childData);
      childData && recordsData.push(childData);
    }
    // If some of children daa is not ready yet
    if (hasPromise) {
      // wait till all children data is ready
      return Promise.all(recordsData).then(values => {
        // re-apply parentRecord since it could get overridden in above getRecordHistogramData() calls
        aggregationContext.parentRecord = parentRecord;
        // filter out empty values
        values = values.filter(x => x);
        return me.aggregateHistogramData(values, records, aggregationContext);
      });
    }
    // aggregate collected data
    return me.aggregateHistogramData(recordsData, records, aggregationContext);
  }
  /**
   * Indicates if the passed record represents a group header built by {@link Grid/feature/Group} feature
   * or a group built by {@link Grid/feature/TreeGroup} feature.
   *
   * @param {Core.data.Model} record The view record
   * @returns {Boolean} `true` if the record represents a group.
   * @internal
   */
  isGroupRecord(record) {
    return record.isGroupHeader || this.isTreeGrouped && record.generatedParent;
  }
  /**
   * For a record representing a group built by {@link Grid/feature/Group} or {@link Grid/feature/TreeGroup}
   * feature returns the group members.
   *
   * @param {Core.data.Model} record A group record
   * @returns {Core.data.Model[]} Records belonging to the group
   * @internal
   */
  getGroupChildren(record) {
    return record.groupChildren || record.children;
  }
  /**
   * For a record belonging to a group built by {@link Grid/feature/Group} or {@link Grid/feature/TreeGroup}
   * feature returns the group header or parent respectively.
   *
   * @param {Core.data.Model} record A member record
   * @returns {Core.data.Model} The record group header or parent record
   * @internal
   */
  getRecordParent(record) {
    const instanceMeta = record.instanceMeta(this.store.id);
    return (instanceMeta === null || instanceMeta === void 0 ? void 0 : instanceMeta.groupParent) || this.isTreeGrouped && record.parent;
  }
  /**
   * Schedules refresh of the provided record's parents.
   * The method iterates up from the provided record parent to the root node
   * and schedules the iterated node rows refresh.
   * @param {Core.data.Model} record Record to refresh parent rows of.
   * @param {Boolean} [clearCache=true] `true` to reset the scheduled records histogram data cache.
   * @internal
   */
  scheduleRecordParentsRefresh(record, clearCache = true) {
    const me = this;
    let groupParent;
    while (groupParent = me.getRecordParent(record)) {
      // reset group cache
      clearCache && me.clearHistogramDataCache(groupParent);
      // and scheduler its later refresh
      me.scheduleRecordRefresh(groupParent);
      // bubble up
      record = groupParent;
    }
  }
  //endregion
  /**
   * Aggregates collected child records data to its parent.
   * The method is synchronous and is called when all the child records data is ready.
   * Override the method if you need to preprocess or postprocess parent records aggregated data:
   *
   * ````javascript
   * class MyHistogramView extends TimelineHistogram({
   *
   *     aggregateHistogramData(recordsData, records, aggregationContext) {
   *         const result = super.aggregateHistogramData(recordsData, records, aggregationContext);
   *
   *         // postprocess averageSalary series values collected for a parent record
   *         result.forEach(entry => {
   *             entry.averageSalary = entry.averageSalary / records.length;
   *         });
   *
   *         return result;
   *     }
   *
   * });
   * ```
   *
   * @param {Object[]} recordsData Child records histogram data.
   * @param {Core.data.Model[]} records Child records.
   * @param {Object} aggregationContext An object containing current shared info on the current aggregation process
   */
  aggregateHistogramData(recordsData, records, aggregationContext = {}) {
    const me = this,
      {
        aggregateFunctions
      } = me;
    aggregationContext.recordsData = recordsData;
    aggregationContext.records = records;
    const arrays = recordsData.map((histogramData, index) => {
      return me.extractHistogramDataArray(histogramData, records[index]);
    });
    // summarize children histogram data
    const result = ArrayHelper.aggregate(arrays, me.getDataEntryForAggregating || (entry => entry), me.internalAggregateDataEntry, me.internalInitAggregatedDataEntry, aggregationContext);
    // call series aggregate functions
    for (const {
      id,
      aggregate = 'sum'
    } of Object.values(me.series)) {
      const fn = aggregateFunctions[aggregate].finalize;
      if (fn && aggregate !== false) {
        fn(id, result, ...arguments);
      }
    }
    return result;
  }
  get widgetClass() {}
});

/**
 * @module Scheduler/view/mixin/TimelineHistogramScaleColumn
 */
/**
 * Mixin of {@link Scheduler/view/TimelineHistogram} class that implements
 * {@link Scheduler/column/ScaleColumn} automatic injection and functioning.
 *
 * @mixin
 */
var TimelineHistogramScaleColumn = (Target => class TimelineHistogramScaleColumn extends Target {
  static $name = 'TimelineHistogramScaleColumn';
  //region Config
  static configurable = {
    /**
     * The locked grid scale column reference.
     * @member {Scheduler.column.ScaleColumn} scaleColumn
     * @readonly
     * @category Scale column
     */
    /**
     * An object with configuration for the {@link Scheduler/column/ScaleColumn}.
     *
     * Example:
     *
     * ```javascript
     * new TimelineHistogram({
     *     scaleColumn : {
     *         width : 50
     *     },
     *     ...
     * });
     * ```
     *
     * Provide `null` to the config to get rid of the column completely:
     *
     * ```javascript
     * new TimelineHistogram({
     *     // do not add scale column
     *     scaleColumn : null,
     *     ...
     * });
     * ```
     *
     * @config {Object} scaleColumn
     * @category Scale column
     */
    scaleColumn: {},
    scalePoints: null,
    scalePointsModelField: 'scalePoints',
    calculateTopValueByScalePoints: true
  };
  updateScalePoints(scalePoints) {
    const me = this,
      topScalePoint = scalePoints[scalePoints.length - 1];
    if (topScalePoint) {
      me.scaleUnit = topScalePoint.unit;
      // Applying new maximum value to the histogram.
      me.histogramWidget.topValue = me.getTopValueByScalePoints(scalePoints);
    }
    // Applying new points to the scale column
    if (me.scaleColumn) {
      me.scaleColumn.scalePoints = scalePoints;
    }
  }
  //endregion
  //region Columns
  changeColumns(columns, currentStore) {
    const me = this,
      scaleColumn = me.getConfig('scaleColumn');
    // No columns means destroy
    if (columns && scaleColumn) {
      var _cols;
      const isArray = Array.isArray(columns);
      let cols = columns;
      if (!isArray) {
        cols = columns.data;
      }
      let scaleColumnIndex = (_cols = cols) === null || _cols === void 0 ? void 0 : _cols.length,
        scaleColumnConfig = scaleColumn;
      cols.some((col, index) => {
        if (col.type === 'scale') {
          scaleColumnIndex = index;
          scaleColumnConfig = ObjectHelper.assign(col, scaleColumnConfig);
          return true;
        }
      });
      // We're going to mutate this array which we do not own, so copy it first.
      cols = cols.slice();
      // Fix up the scaleColumn config in place
      cols[scaleColumnIndex] = {
        type: 'scale',
        ...scaleColumnConfig
      };
      if (isArray) {
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
      this._scaleColumn = this.columns.find(c => c.isScaleColumn);
    }
  }
  onColumnsChanged({
    action,
    changes,
    record: column,
    records
  }) {
    const {
      scaleColumn,
      columns
    } = this;
    // If someone replaces the column set (syncing leads to batch), ensure scale is always added
    if (scaleColumn && (action === 'dataset' || action === 'batch') && !columns.includes(scaleColumn)) {
      columns.add(scaleColumn, true);
    }
    super.onColumnsChanged(...arguments);
  }
  //endregion
  //region Data processing
  /**
   * A hook to convert scale point values to histogram ones.
   * In case they use different units.
   *
   * Override this method in a sub-class to implement your custom
   * application specific conversion.
   * @param {Number} value Scale point value
   * @param {String} unit Scale point unit
   * @internal
   */
  convertUnitsToHistogramValue(value, unit) {
    return value;
  }
  /**
   * A hook to convert histogram values to scale point ones.
   * In case they use different units.
   *
   * Override this method in a sub-class to implement your custom
   * application specific conversion.
   * @param {Number} value Scale point value
   * @param {String} unit Scale point unit
   * @internal
   */
  convertHistogramValueToUnits(value, unit) {
    return value;
  }
  extractHistogramDataArray(histogramData, record) {
    return histogramData;
  }
  getTopValueByScalePoints(scalePoints) {
    const me = this,
      {
        scaleColumn
      } = me,
      lastPoint = scalePoints[scalePoints.length - 1],
      {
        value,
        unit
      } = lastPoint;
    let rawValue = value;
    if (scaleColumn) {
      // add padding to top value
      rawValue *= 1 + (scaleColumn.scaleWidget.scaleMaxPadding || 0);
    }
    return me.convertUnitsToHistogramValue(rawValue, unit || me.scaleUnit);
  }
  processRecordRenderData(renderData) {
    renderData = super.processRecordRenderData(...arguments);
    if (this.scaleColumn) {
      var _me$initialConfig$his;
      const me = this,
        {
          record,
          histogramData,
          histogramConfig = {}
        } = renderData;
      let topValue = (_me$initialConfig$his = me.initialConfig.histogramWidget) === null || _me$initialConfig$his === void 0 ? void 0 : _me$initialConfig$his.topValue,
        scalePoints = me.scalePoints || record.get(me.scalePointsModelField);
      if (!topValue) {
        // if no topValue provided but we have scalePoints
        if (scalePoints && me.calculateTopValueByScalePoints) {
          // calculate topValue based on the max scale point
          topValue = me.getTopValueByScalePoints(scalePoints);
        }
        // if still no topValue
        if (!topValue && histogramData) {
          const histogramWidget = renderData.histogramWidget || me.histogramWidget;
          ObjectHelper.assign(histogramWidget, histogramConfig);
          // get top value based on histogramData
          topValue = histogramWidget.getDataTopValue(histogramData);
          scalePoints = [{
            value: me.convertHistogramValueToUnits(topValue, me.scaleUnit),
            text: me.convertHistogramValueToUnits(topValue, me.scaleUnit)
          }];
          topValue += me.scaleColumn.scaleWidget.scaleMaxPadding * topValue;
        }
        renderData.scaleWidgetConfig = {
          scalePoints
        };
        renderData.histogramConfig = {
          ...histogramConfig,
          topValue
        };
      }
    }
    return renderData;
  }
  //endregion
  //region Render
  /**
   * Group feature hook triggered by the feature to render group headers
   * @param {Object} renderData
   * @internal
   */
  buildGroupHeader(renderData) {
    if (renderData.column === this.scaleColumn) {
      return this.scaleColumn.renderer(renderData);
    }
    return super.buildGroupHeader(...arguments);
  }
  beforeRenderCell(renderData) {
    if (this.scaleColumn && renderData.column === this.scaleColumn) {
      renderData.histogramData = this.getRecordHistogramData(renderData.record);
      // If data is read apply prepared render data
      if (!ObjectHelper.isPromise(renderData.histogramData)) {
        Object.assign(renderData, this._recordRenderData);
      }
    }
    return super.beforeRenderCell(...arguments);
  }
  /**
   * Renders record scale column content.
   * @param {Core.data.Model} record Record to render scale for
   * @param {Object} [renderData]
   * @category Scale column
   */
  renderRecordScale(record, renderData) {
    if (this.scaleColumn) {
      const row = this.getRowFor(record),
        cellElement = row === null || row === void 0 ? void 0 : row.getCell(this.scaleColumn.id);
      if (cellElement) {
        row.renderCell(cellElement);
      }
    }
  }
  get widgetClass() {}
  //endregion
});

/**
 * @module Scheduler/view/TimelineHistogram
 */
/**
 * This view displays histograms for the provided store records.
 *
 * A {@link Scheduler/column/ScaleColumn} is also added automatically.
 *
 * {@inlineexample Scheduler/view/TimelineHistogram.js}
 *
 * To create a standalone histogram, simply configure it with a {@link Core/data/Store} instance:
 *
 * ```javascript
 * const store = new Store({
 *     data : [
 *         {
 *             id            : 'r1',
 *             name          : 'Record 1',
 *             // data used to render a histogram for this record
 *             histogramData : [
 *                 { value1 : 200, value2 : 100 },
 *                 { value1 : 150, value2 : 50 },
 *                 { value1 : 175, value2 : 50 },
 *                 { value1 : 175, value2 : 75 }
 *             ]
 *         },
 *         {
 *             id            : 'r2',
 *             name          : 'Record 2',
 *             // data used to render a histogram for this record
 *             histogramData : [
 *                 { value1 : 100, value2 : 100 },
 *                 { value1 : 150, value2 : 125 },
 *                 { value1 : 175, value2 : 150 },
 *                 { value1 : 175, value2 : 75 }
 *             ]
 *         }
 *     ]
 * });
 *
 * const histogram = new TimelineHistogram({
 *     appendTo  : 'targetDiv',
 *     startDate : new Date(2022, 11, 26),
 *     endDate   : new Date(2022, 11, 30),
 *     store,
 *     // specify series displayed in the histogram
 *     series : {
 *         value1 : {
 *             type  : 'bar',
 *             field : 'value1'
 *         },
 *         value2 : {
 *             type  : 'bar',
 *             field : 'value2'
 *         }
 *     },
 *     columns : [
 *         {
 *             field : 'name',
 *             text  : 'Name'
 *         }
 *     ]
 * });
 * ```
 *
 * ## Providing histogram data
 *
 * There are two basic ways to provide histogram data:
 *
 * - the data can be provided statically in a record field configured as {@link #config-dataModelField}:
 *
 * ```javascript
 * const store = new Store({
 *     data : [
 *         {
 *             id   : 11,
 *             name : 'John Smith',
 *             // data used to render a histogram for this record
 *             hd   : [
 *                 { weight : 200, price : 100 },
 *                 { weight : 150, price : 105 },
 *                 { weight : 175, price : 90 },
 *                 { weight : 175, price : 95 }
 *             ]
 *         }
 *     ]
 * });
 *
 * const histogram = new TimelineHistogram({
 *     dataModelField : 'hd',
 *     series : {
 *         weight : {
 *             type : 'bar'
 *         },
 *         price : {
 *             type : 'outline'
 *         }
 *     },
 *     ...
 * });
 * ```
 * - the data can be collected dynamically with the provided {@link #config-getRecordData} function:
 *
 * ```javascript
 * const histogram = new TimelineHistogram({
 *     dataModelField : 'hd',
 *     series : {
 *         weight : {
 *             type : 'bar'
 *         },
 *         price : {
 *             type : 'outline'
 *         }
 *     },
 *     ...
 *     async getRecordData(record) {
 *         // we get record histogram data from the server
 *         const response = await fetch('https://some.url/to/get/data?' + new URLSearchParams({
 *             // pass the record identifier and the time span we need data for
 *             record    : record.id,
 *             startDate : DateHelper.format(this.startDate),
 *             endDate   : DateHelper.format(this.endDate),
 *         }));
 *         return response.json();
 *     }
 * });
 * ```
 *
 * Please check ["Timeline histogram" guide](#Scheduler/guides/timelinehistogram.md) for more details.
 *
 * @extends Scheduler/view/TimelineHistogramBase
 * @mixes Scheduler/view/mixin/TimelineHistogramGrouping
 * @mixes Scheduler/view/mixin/TimelineHistogramScaleColumn
 * @features Scheduler/feature/ColumnLines
 * @features Scheduler/feature/ScheduleTooltip
 * @classtype timelinehistogram
 * @widget
 */
class TimelineHistogram extends TimelineHistogramBase.mixin(TimelineHistogramGrouping, TimelineHistogramScaleColumn) {
  //region Config
  static $name = 'TimelineHistogram';
  static type = 'timelinehistogram';
  /**
   * Retrieves the histogram data for the provided record.
   *
   * The method first checks if there is cached data for the record and returns it if found.
   * Otherwise it starts collecting data by calling {@link #config-getRecordData} (if provided)
   * or by reading it from the {@link #config-dataModelField} record field.
   *
   * If the provided record represents a group and {@link #config-aggregateHistogramDataForGroups} is enabled
   * then the group members data is calculated with a {@link #function-getGroupRecordHistogramData} method call.
   *
   * The method can be asynchronous depending on the provided {@link #config-getRecordData} function.
   * If the function returns a `Promise` then the method will return a wrapping `Promise` in turn that will
   * resolve with the collected histogram data.
   *
   * The method triggers the {@link #event-histogramDataCacheSet} event when a record data is ready.
   *
   * @param {Core.data.Model} record Record to retrieve the histogram data for.
   * @param {Object} [aggregationContext] An optional object passed when the method is called when aggregating
   * a group members histogram data.
   *
   * See {@link #function-getGroupRecordHistogramData} and {@link Core/helper/ArrayHelper#function-aggregate-static}
   * for more details.
   * @returns {Object|Promise} The histogram data for the provided record or a `Promise` that will provide the data
   * when resolved.
   * @function getRecordHistogramData
   */
}

TimelineHistogram.initClass();
TimelineHistogram._$name = 'TimelineHistogram';

export { DelayedRecordsRendering, DependencyEdit, EventCopyPaste, EventDrag, EventDragCreate, EventTooltip, ResourceTimeRangesBase, ScaleColumn, ScheduleContext, StickyEvents, TimeRanges, TimelineHistogram, TimelineHistogramBase, TimelineHistogramGrouping, TimelineHistogramScaleColumn };
//# sourceMappingURL=TimelineHistogram.js.map
