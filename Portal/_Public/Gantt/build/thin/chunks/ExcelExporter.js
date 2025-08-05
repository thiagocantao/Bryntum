/*!
 *
 * Bryntum Gantt 5.5.0
 *
 * Copyright(c) 2023 Bryntum AB
 * https://bryntum.com/contact
 * https://bryntum.com/license
 *
 */
import { Menu, InstancePlugin, ObjectHelper, DomHelper, ArrayHelper, Rectangle, AsyncHelper, Base, DateHelper } from './Editor.js';
import { ColumnStore, Column, GridFeatureManager } from './GridBase.js';
import { SummaryFormatter } from './PdfExport.js';
import { Splitter } from './Splitter.js';

/**
 * @module Grid/column/ColorColumn
 */
/**
 * A column that displays color values (built-in color classes or CSS colors) as a colored element similar to
 * the {@link Core.widget.ColorField}. When the user clicks the element, a {@link Core.widget.ColorPicker} lets the user
 * select from a range of colors.
 *
 * {@inlineexample Grid/column/ColorColumn.js}
 *
 * ```javascript
 * new Grid({
 *    columns : [
 *       {
 *          type   : 'color',
 *          field  : 'color',
 *          text   : 'Color'
 *       }
 *    ]
 * });
 * ```
 *
 * @extends Grid/column/Column
 * @classType color
 */
class ColorColumn extends Column {
  static $name = 'ColorColumn';
  static type = 'color';
  static fields = [{
    name: 'colorEditorType',
    defaultValue: 'colorpicker'
  },
  /**
   * Array of CSS color strings to be able to chose from. This will override the
   * {@link Core.widget.ColorPicker#config-colors pickers default colors}.
   *
   * Provide an array of string CSS colors:
   * ```javascript
   * new Grid({
   *    columns : [
   *       {
   *          type   : 'color',
   *          field  : 'color',
   *          text   : 'Color',
   *          colors : ['#00FFFF', '#F0FFFF', '#89CFF0', '#0000FF', '#7393B3']
   *       }
   *    ]
   * });
   * ```
   * @prp {String[]}
   */
  'colors',
  /**
   * Adds an option in the picker to set no background color
   * @prp {Boolean}
   * @default true
   */
  {
    name: 'addNoColorItem',
    defaultValue: true
  }];
  static defaults = {
    align: 'center',
    editor: null
  };
  construct() {
    var _me$colors;
    super.construct(...arguments);
    const me = this,
      {
        grid
      } = me;
    me.menu = new Menu({
      owner: grid,
      rootElement: grid.rootElement,
      autoShow: false,
      align: 't50-b50',
      anchor: true,
      internalListeners: {
        hide() {
          me.picker.navigator.activeItem = null;
          delete me._editingRecord;
        }
      },
      items: [Object.assign({
        type: me.colorEditorType,
        ref: 'list',
        addNoColorItem: me.addNoColorItem,
        colorSelected({
          color
        }) {
          var _me$_editingRecord;
          (_me$_editingRecord = me._editingRecord) === null || _me$_editingRecord === void 0 ? void 0 : _me$_editingRecord.set(me.field, color);
          me.menu.hide();
        }
      }, (_me$colors = me.colors) !== null && _me$colors !== void 0 && _me$colors.length ? {
        colors: me.colors
      } : {})]
    });
  }
  applyValue(useProp, field, value) {
    if (!this.isConstructing) {
      const {
        picker
      } = this;
      if (field === 'colors') {
        picker.colors = value;
      } else if (field === 'addNoColorItem') {
        picker.addNoColorItem = value;
      }
    }
    super.applyValue(...arguments);
  }
  get picker() {
    return this.menu.widgetMap.list;
  }
  renderer({
    value
  }) {
    let colorClass = 'b-empty',
      backgroundColor = value;
    if (value) {
      const colorClassName = this.picker.getColorClassName(value);
      if (colorClassName) {
        colorClass = colorClassName;
        backgroundColor = null;
      } else {
        colorClass = '';
      }
    }
    return {
      className: 'b-color-cell-inner ' + colorClass,
      style: {
        backgroundColor
      },
      'data-btip': value
    };
  }
  onCellClick({
    grid,
    record,
    target
  }) {
    if (target.classList.contains('b-color-cell-inner') && !this.readOnly && !grid.readOnly && !record.isSpecialRow && !record.readOnly) {
      const {
          picker,
          menu
        } = this,
        value = record.get(this.field);
      this._editingRecord = record;
      picker.deselectAll();
      picker.select(value);
      picker.refresh();
      menu.showBy(target);
    }
  }
}
ColumnStore.registerColumnType(ColorColumn);
ColorColumn._$name = 'ColorColumn';

/**
 * @module Grid/feature/GroupSummary
 */
/**
 * Displays a summary row as a group footer in a grouped grid. Uses same configuration options on columns as
 * {@link Grid.feature.Summary}.
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * ```javascript
 * features : {
 *     group        : 'city',
 *     groupSummary : true
 * }
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/groupsummary
 * @classtype groupSummary
 * @feature
 *
 * @inlineexample Grid/feature/GroupSummary.js
 */
class GroupSummary extends SummaryFormatter(InstancePlugin) {
  //region Init
  static get $name() {
    return 'GroupSummary';
  }
  static get configurable() {
    return {
      /**
       * Set to `true` to have group summaries rendered in the group header when a group is collapsed.
       *
       * Only applies when {@link #config-target} is `'footer'` (the default).
       *
       * @member {Boolean} collapseToHeader
       */
      /**
       * Configure as `true` to have group summaries rendered in the group header when a group is collapsed.
       *
       * ```javascript
       * const grid = new Grid({
       *    features : {
       *        groupSummary : {
       *            collapseToHeader : true
       *        }
       *    }
       * });
       * ```
       *
       * Only applies when {@link #config-target} is `'footer'` (the default).
       *
       * @config {Boolean}
       */
      collapseToHeader: null,
      /**
       * Where to render the group summaries to, either `header` to display them in the group header or `footer`
       * to display them in the group footer (the default).
       *
       * @member {'header'|'footer'} target
       */
      /**
       * Where to render the group summaries to, either `header` to display them in the group header or `footer`
       * to display them in the group footer (the default).
       *
       * ```javascript
       * const grid = new Grid({
       *    features : {
       *        groupSummary : {
       *            target : 'header'
       *        }
       *    }
       * });
       * ```
       *
       * @config {'header'|'footer'}
       * @default
       */
      target: 'footer'
    };
  }
  construct(grid, config) {
    this.grid = grid;
    super.construct(grid, config);
    if (!grid.features.group) {
      throw new Error('Requires Group feature to work, please enable');
    }
    this.bindStore(grid.store);
    grid.rowManager.ion({
      beforeRenderRow: 'onBeforeRenderRow',
      renderCell: 'renderCell',
      // The feature gets to see cells being rendered after the Group feature
      // because the Group feature injects header content into group header rows
      // and adds rendering info to the cells renderData which we must comply with.
      // In particular, it calculates the isFirstColumn flag which it adds to
      // the cell renderData which we interrogate.
      prio: 1000,
      thisObj: this
    });
  }
  bindStore(store) {
    this.detachListeners('store');
    store.ion({
      name: 'store',
      update: 'onStoreUpdate',
      // need to run before grids listener, to flag for full refresh
      prio: 1,
      thisObj: this
    });
  }
  get store() {
    return this.grid.store;
  }
  doDisable(disable) {
    // Toggle footers if needed
    this.updateTarget(this.target);
    super.doDisable(disable);
  }
  changeTarget(target) {
    ObjectHelper.assertString(target, 'target');
    return target;
  }
  updateTarget(target) {
    // Flag that will make the Store insert rows for group footers
    this.store.useGroupFooters = !this.disabled && target === 'footer';
    // Refresh groups to show/hide footers
    if (!this.isConfiguring) {
      this.store.group();
    }
  }
  changeCollapseToHeader(collapseToHeader) {
    ObjectHelper.assertBoolean(collapseToHeader, 'collapseToHeader');
    return collapseToHeader;
  }
  updateCollapseToHeader() {
    if (!this.isConfiguring) {
      this.store.group();
    }
  }
  //endregion
  //region Plugin config
  // Plugin configuration. This plugin chains some of the functions in Grid.
  static get pluginConfig() {
    return {
      chain: ['bindStore']
    };
  }
  //endregion
  //region Render
  /**
   * Called before rendering row contents, used to reset rows no longer used as group summary rows
   * @private
   */
  onBeforeRenderRow({
    row,
    record
  }) {
    if (row.isGroupFooter && !('groupFooterFor' in record.meta)) {
      // not a group row.
      row.isGroupFooter = false;
      // force full "redraw" when rendering cells
      row.forceInnerHTML = true;
    } else if (row.isGroupHeader && !record.meta.collapsed) {
      // remove any summary elements
      row.eachElement(this.removeSummaryElements);
    }
  }
  removeSummaryElements(rowEl) {}
  /**
   * Called when a cell is rendered, styles the group rows first cell.
   * @private
   */
  renderCell({
    column,
    cellElement,
    row,
    record,
    size,
    isFirstColumn
  }) {
    const me = this,
      {
        meta
      } = record,
      {
        rowHeight
      } = me.grid,
      isGroupHeader = ('groupRowFor' in meta),
      isGroupFooter = ('groupFooterFor' in meta),
      targetsHeader = me.target === 'header',
      rowClasses = {
        'b-group-footer': 0,
        'b-header-summary': 0
      },
      isSummaryTarget =
      // Header cell should have summary content if we are targeting the header or if the group is collapsed
      // and we are configured with collapseToHeader, excluding the first column which holds the group title
      isGroupHeader && (targetsHeader || me.collapseToHeader && meta.collapsed) && !isFirstColumn ||
      // Footer cell should have summary content if we are targeting the footer (won't render if collapsed)
      isGroupFooter && !targetsHeader;
    // Needed to restore height when summary is no longer displayed
    if (isGroupHeader || isGroupFooter) {
      size.height = isGroupHeader ? size.height || rowHeight : rowHeight;
    }
    if (me.store.isGrouped && isSummaryTarget && !me.disabled) {
      // clear cell before add any HTML in it. if the cell contained widgets, they will be properly destroyed.
      column.clearCell(cellElement);
      const groupRecord = isGroupHeader ? record : meta.groupRecord;
      row.isGroupFooter = isGroupFooter;
      row.isGroupHeader = isGroupHeader;
      // This is a group footer row, add css
      if (isGroupFooter) {
        rowClasses['b-group-footer'] = 1;
      }
      // This is a group header row, add css
      else {
        rowClasses['b-header-summary'] = 1;
      }
      // returns height config or count. config format is { height, count }. where `height is in px and should be
      // added to value calculated from `count
      const heightSetting = me.updateSummaryHtml(cellElement, column, groupRecord),
        count = typeof heightSetting === 'number' ? heightSetting : heightSetting.count;
      // number of summaries returned, use to calculate cell height
      if (count > 1) {
        size.height += meta.collapsed && !targetsHeader ? 0 : count * rowHeight * 0.1;
      }
      // height config with height specified, added to cell height
      if (heightSetting.height) {
        size.height += heightSetting.height;
      }
    }
    // Sync row's classes with its status as a group header or footer.
    row.assignCls(rowClasses);
  }
  updateSummaryHtml(cellElement, column, groupRecord) {
    const records = groupRecord.groupChildren.slice();
    // Group footers should not be included in summary calculations
    if (records[records.length - 1].isGroupFooter) {
      records.pop();
    }
    const html = this.generateHtml(column, records, 'b-grid-group-summary', groupRecord, groupRecord.meta.groupField, groupRecord.meta.groupRowFor);
    // First time, set table
    if (!cellElement.children.length) {
      cellElement.innerHTML = html;
    }
    // Following times, sync changes
    else {
      DomHelper.sync(html, cellElement.firstElementChild);
    }
    // return summary "count", used to set row height
    return column.summaries ? column.summaries.length : column.sum ? 1 : 0;
  }
  //endregion
  //region Events
  /**
   * Updates summaries on store changes (except record update, handled below)
   * @private
   */
  onStoreUpdate({
    source: store,
    changes
  }) {
    if (!this.disabled && store.isGrouped) {
      // If a grouping field is among the changes, StoreGroup#onDataChanged will
      // take care of the update by re-sorting.
      if (changes && store.groupers.find(grouper => grouper.field in changes)) {
        return;
      }
      // only update summary when a field that affects summary is changed
      const shouldUpdate = Object.keys(changes).some(field => {
        const colField = this.grid.columns.get(field);
        // check existence, since a field not used in a column might have changed
        return Boolean(colField) && (Boolean(colField.sum) || Boolean(colField.summaries));
      });
      if (shouldUpdate) {
        this.grid.forceFullRefresh = true;
      }
    }
  }
  //endregion
  /**
   * Refreshes the summaries
   */
  refresh() {
    this.grid.columns.visibleColumns.forEach(column => {
      if (this.hasSummary(column)) {
        this.grid.refreshColumn(column);
      }
    });
  }
  hasSummary(column) {
    return column.sum || column.summaries;
  }
}
GroupSummary.featureClass = 'b-group-summary';
GroupSummary._$name = 'GroupSummary';
GridFeatureManager.registerFeature(GroupSummary);

/**
 * @module Grid/feature/Split
 */
const startScrollOptions = Object.freeze({
    animate: false,
    block: 'start'
  }),
  endScrollOptions = Object.freeze({
    animate: false,
    block: 'end'
  }),
  splitterWidth = 7,
  // Listeners for these events should not be added to splits
  ignoreListeners = {
    split: 1,
    unsplit: 1
  };
/**
 * This feature allows splitting the Grid into multiple views, either by using the cell context menu, or
 * programmatically by calling {@link #function-split split()}.
 *
 * {@inlineexample Grid/feature/Split.js}
 *
 * It handles splitting in 3 "directions":
 *
 * - `'horizontal'` - Splitting the grid into 2 sub-views, one above the other.
 * - `'vertical'` - Splitting the grid into 2 sub-views, one to the left of the other.
 * - `'both'` - Splitting the grid into 4 sub-views, one in each corner.
 *
 * Or, by supplying a record and/or a column to split by.
 *
 * The first sub-view (top, left or top-left depending on split direction) is the original grid, and the others are
 * clones of the original. The clones share the same store, columns and selection.
 *
 * Sub-views in the same column sync their scrolling horizontally, and sub-views in the same row sync their scrolling
 * vertically.
 *
 * Sub-views are separated by splitters, that can be dragged to resize the views.
 *
 * Splitting a multi-region grid (two regions supported) only includes the region in which the split was performed in
 * the split view.
 *
 * Splitting works best on grids that use fixed column widths, since flexed columns will resize when the grid is split.
 *
 * ## Splitting programmatically
 *
 * The split feature assigns two methods to the owning grid:
 *
 * - {@link #function-split split()} - Splits the grid into sub-views.
 * - {@link #function-unsplit unsplit()} - Re-joins the sub-views into a single grid.
 *
 * Use them to split programmatically in your app.
 *
 * ```javascript
 * // Split horizontally (eg. at the row in the center of the grid)
 * await grid.split({ direction : 'horizontal' });
 *
 * // Split both ways by a specific column and record
 * await grid.split({
 *    atRecord : grid.store.getById(10),
 *    atColumn : grid.columns.get('city')
 * });
 *
 * // Remove splits, returning to a single grid
 * grid.unsplit();
 * ```
 *
 * ## Splitting using the cell context menu
 *
 * The feature also adds a new sub-menu to the cell context menu, allowing the user to split (or un-split) the grid. See
 * the API documentation for the {@link Grid/feature/CellMenu} feature for more information on how to customize the
 * sub-menu.
 *
 * ## Accessing a sub-view
 * The sub-views are accessed by index. The original grid is at index 0, and the others are as shown below.
 * For 'horizontal' splits:
 *
 * <div style="font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-bottom: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-top: none; padding: 1em; width: 13em">1 - Sub-view</div>
 * </div>
 *
 * For 'vertical' splits:
 *
 * <div style="display: flex; flex-direction: row;font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-left: none; padding: 1em; width: 13em">1 - Sub-view</div>
 * </div>
 *
 * For 'both' splits:
 *
 * <div style="display: flex; flex-flow: row wrap; width : 27em;font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999; border-bottom: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-left: none; border-bottom: 2px solid #999; padding: 1em; width: 13em">1 - Sub-view</div>
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999;border-top: none; padding: 1em; width: 13em">2 - Sub-view</div>
 *     <div style="border: 1px solid #ccc; border-top: none; border-left: none; padding: 1em; width: 13em">3 - Sub-view</div>
 * </div>
 *
 * The {@link #property-subViews} property returns an array containing all sub-views, including the original. Note that
 * the property is also exposed on the owning Grid. Access a specific sub-view by index (see illustrations above). For
 * example to access the bottom right sub-view in a 'both' split:
 *
 * ```javascript
 * await grid.split({ direction : 'both' });
 * const bottomRight = grid.subViews[3];
 * await bottomRight.scrollRowIntoView(100);
 * ```
 *
 * ## Troubleshooting
 *
 * The splits are inserted into a container element (which has the `.b-split-container` CSS class), replacing the
 * original grid. If it does not render correctly out of the box, you should make sure that any CSS rules you have that
 * apply size to the grid also applies to the container element.
 *
 * For example if you use a CSS flex rule to size the grid:
 *
 * ```css
 * .b-grid {
 *     // Size grid using flex
 *     flex : 3;
 * }
 * ```
 *
 * Then you should also apply the same rule to the container element:
 *
 * ```css
 * .b-grid,
 * .b-split-container {
 *     flex : 3;
 * }
 * ```
 *
 * {@note}
 * Note that configuration changes at runtime, when already split, are not automatically propagated to the sub-views. If
 * you need to change a config at runtime, either first unsplit the grid, or change it on each sub-view individually. A
 * notable exception from this is that enabling / disabling features at runtime is reflected in the sub-views.
 * {/@note}
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype split
 * @feature
 */
class Split extends InstancePlugin {
  static $name = 'Split';
  static featureClass = '';
  static configurable = {
    /**
     * An array of sub-views. The first sub-view is the original grid, and the others are clones of the original.
     * See the "Accessing a sub-view" section above for more information.
     *
     * ```javascript
     * await grid.split('vertical');
     * const bottom = grid.subViews[1];
     * await bottom.scrollRowIntoView(100);
     * ```
     *
     * Note that this property is accessible directly on the grid instance.
     *
     * @member {Grid.view.Grid[]} subViews
     * @on-owner
     * @readonly
     * @category Common
     */
    subViews: [],
    // Not a config, but still defined in configurable to allow assigning it in pluginConfig,
    /**
     * Properties whose changes should be relayed to sub-views at runtime.
     *
     * Supply an object with property names as keys, and a truthy value to relay the change, or a falsy value to not
     * relay it. The object will be merged with the default values.
     *
     * By default, these properties are relayed:
     * * {@link Grid/view/Grid#property-readOnly}
     * * {@link Grid/view/Grid#property-rowHeight}
     *
     * Example of supplying a custom set of properties to relay:
     * ```javascript
     * const grid = new Grid({
     *     features : {
     *         split : {
     *             relayProperties : {
     *                 readOnly : false, // Do not relay readOnly changes
     *                 myConfig : true   // Relay changes to the myConfig property
     *             }
     *         }
     *     }
     * }
     * ```
     * @config {Object<String,Boolean>}
     */
    relayProperties: {
      value: {
        readOnly: 1,
        rowHeight: 1
      },
      $config: {
        merge: 'merge'
      }
    }
  };
  static pluginConfig = {
    chain: ['populateCellMenu', 'afterConfigChange', 'afterAddListener', 'afterRemoveListener'],
    assign: ['split', 'unsplit', 'subViews', 'syncSplits']
  };
  // Flag used to ignore column changes that arise from syncing columns
  #ignoreColumnChanges = false;
  restorers = [];
  doDestroy() {
    this.unsplit(true);
    super.doDestroy();
  }
  doDisable(disable) {
    const me = this;
    if (!me.isConfiguring) {
      if (disable) {
        me._disabledSplitOptions = me._splitOptions;
        me.unsplit();
      } else if (me._disabledSplitOptions) {
        me.split(me._disabledSplitOptions);
        me._disabledSplitOptions = null;
      }
    }
  }
  //region Split / unsplit
  get isSplit() {
    var _this$widgets;
    return Boolean((_this$widgets = this.widgets) === null || _this$widgets === void 0 ? void 0 : _this$widgets.length);
  }
  getClientConfig(appendTo, order, options, config = {}) {
    const {
        client
      } = this,
      {
        subGrids,
        regions
      } = client,
      columns = client.columns.records.slice(),
      subGridConfigs = ObjectHelper.assign({}, client.subGridConfigs);
    // Match current sub-grid sizes
    client.eachSubGrid(subGrid => {
      const config = subGridConfigs[subGrid.region];
      if (subGrid.flex) {
        config.flex = subGrid.flex;
      } else {
        config.width = subGrid.element.style.width;
      }
    });
    if (options.atColumn && regions.length > 1 && order > 0) {
      // Exclude regions to the left of the split
      const subGridIndex = regions.indexOf(options.atColumn.region);
      for (let i = 0; i < subGridIndex; i++) {
        const subGrid = subGrids[regions[i]];
        ArrayHelper.remove(columns, ...subGrid.columns.records);
        delete subGridConfigs[regions[i]];
      }
    }
    const clientConfig = ObjectHelper.assign({}, client.initialConfig, {
      appendTo,
      insertFirst: null,
      insertBefore: null,
      splitFrom: client,
      owner: client.owner,
      // Use no toolbar or fake empty toolbar for things to line up nicely
      tbar: client.initialConfig.tbar && order === 1 ? {
        height: client.tbar.height,
        items: [' ']
      } : null,
      // Share store & selection
      store: client.store,
      selectedRecordCollection: client.selectedRecordCollection,
      subGridConfigs,
      // Cannot directly share columns, since there is a 1-1 mapping between column and it's header
      columns: this.cloneColumns(columns),
      minHeight: 0,
      minWidth: 0
    }, config);
    // Listeners are removed from initialConfig during initialization, use non-internal current listeners
    const appListeners = {};
    for (const name in client.listeners) {
      if (!ignoreListeners[name]) {
        const [listener] = client.listeners[name];
        if (!listener.$internal) {
          appListeners[name] = listener;
        }
      }
    }
    // Not internalListeners on purpose, these are app listeners
    clientConfig.listeners = appListeners;
    // Hide headers for bottom clone in horizontal split
    if (options.direction === 'horizontal') {
      clientConfig.hideHeaders = true;
    }
    // Hide headers for bottom clones in both split
    else if (options.direction === 'both' && order !== 1) {
      clientConfig.hideHeaders = true;
    }
    delete clientConfig.data;
    return clientConfig;
  }
  cloneColumns(source) {
    return source.flatMap(col => {
      // Do not clone selection column, it will be injected by GridSelection.
      // Ditto for the row expander column
      if (col.meta.isSelectionColumn || col.field === 'expanderActionColumn') {
        return [];
      }
      const data = {
        ...col.data
      };
      if (col.children) {
        data.children = col.children.map(child => ({
          ...child.data
        }));
      }
      // RowNumberColumn "pollutes" headerRenderer, will create infinite loop if not cleaned up
      delete data.headerRenderer;
      delete data.parentId;
      return data;
    });
  }
  cloneClient(appendTo, order, options, config) {
    const clientConfig = this.getClientConfig(appendTo, order, options, config),
      clone = new this.client.constructor(clientConfig);
    clone.element.classList.add('b-split-clone');
    return clone;
  }
  // Process options, deducing direction, atRecord, etc.
  processOptions(options) {
    const {
        client
      } = this,
      {
        atRecord,
        atColumn,
        direction
      } = options;
    if (!direction) {
      // Infer direction from record & column
      if (atRecord && atColumn) {
        options.direction = 'both';
      } else if (atColumn) {
        options.direction = 'vertical';
      } else {
        options.direction = 'horizontal';
      }
    } else {
      // Only given a direction, cut roughly in half
      if (direction !== 'vertical' && !atRecord && client.store.count) {
        const centerY = client._bodyRectangle.height / 2 + client.scrollable.y,
          centerRow = client.rowManager.getRowAt(centerY, true) ?? client.rowManager.rows[Math.ceil(client.rowManager.rows.length / 2)];
        options.atRecord = client.store.getById(centerRow.id);
      }
      if (direction !== 'horizontal' && !atColumn) {
        const bounds = Rectangle.from(client.element);
        // Figure out subgrid intersecting center of grid
        let centerX = bounds.center.x - bounds.x,
          subGrid = client.subGrids[client.regions[0]],
          i = 0,
          column = null;
        while (centerX > subGrid.width) {
          centerX -= subGrid.width;
          subGrid = client.subGrids[client.regions[++i]];
        }
        // We want the center column in view, but iteration below is over all columns
        centerX += subGrid.scrollable.x;
        // Figure out column in the subgrid
        const {
          visibleColumns
        } = subGrid.columns;
        let x = 0,
          j = 0;
        while (x < centerX && j < visibleColumns.length) {
          column = visibleColumns[j++];
          x += column.element.offsetWidth;
        }
        options.atColumn = column;
      }
    }
    return options;
  }
  // Create element to contain the splits, it "both" mode it will hold a top container and a bottom container.
  // In single mode, it will hold the splits + splitters directly.
  createSplitContainer({
    direction
  }) {
    const {
        client
      } = this,
      {
        element
      } = client;
    return this.splitContainer = DomHelper.createElement({
      parent: element.parentElement,
      className: {
        'b-split-container': 1,
        [`b-split-${direction}`]: 1,
        'b-rtl': client.rtl
      },
      style: {
        width: element.style.width,
        height: element.style.height
      },
      children: [
      // Split in one dir, use original as first child
      direction !== 'both' && element,
      // Split in both directions, make two sub-containers and put original in first
      direction === 'both' && {
        className: 'b-split-top',
        children: [element]
      }, direction === 'both' && {
        className: 'b-split-bottom'
      }]
    });
  }
  // Make the headers of all splits same height. Since headers shrinkwrap, they might differ depending on which
  // subgrids was cloned to each split
  syncHeaderHeights() {
    let maxHeaderHeight = 0;
    // Find tallest header
    for (const split of this.subViews) {
      split.eachSubGrid(subGrid => {
        if (subGrid.header.height > maxHeaderHeight) {
          maxHeaderHeight = subGrid.header.height;
        }
      });
    }
    // Apply its height to all headers
    for (const split of this.subViews) {
      split.eachSubGrid(subGrid => {
        subGrid.header.height = maxHeaderHeight;
      });
    }
  }
  // Clones can be created with correct subgrids, in the original we might instead need to hide some when splitting
  // in a region that is not the last one (locked for example)
  toggleOriginalSubGrids(options) {
    const me = this,
      {
        client
      } = me,
      {
        regions
      } = client;
    // Split at a column with multiple regions
    if (options.atColumn && regions.length > 1) {
      const subGridIndex = regions.indexOf(options.atColumn.region),
        // Always process the original
        splits = [client];
      // And the bottom left one in a four way split
      if (options.direction === 'both') {
        splits.push(me.subViews[2]);
      }
      for (const split of splits) {
        // Hide regions to the right of the split in the original
        if (subGridIndex + 1 < regions.length) {
          const isOriginal = split === client;
          // Leftmost subgrid to keep visible
          const subGrid = split.subGrids[regions[subGridIndex]];
          // It won't need a splitter when succeeding subgrids are hidden
          subGrid.hideSplitter();
          isOriginal && me.restorers.push(() => subGrid.showSplitter());
          // Force flex to fill space left by hiding succeeding subgrids
          if (!subGrid.flex) {
            // Don't affect other splits
            client.inForEachOther = true;
            subGrid.flex = 1;
            client.inForEachOther = false;
            isOriginal && me.restorers.push(() => {
              subGrid.flex = null;
              subGrid.width = subGrid._initialWidth;
            });
          }
          // Hide succeeding subgrids
          for (let i = subGridIndex + 1; i < regions.length; i++) {
            const subGrid = split.subGrids[regions[i]];
            subGrid.hide();
            isOriginal && me.restorers.push(() => {
              subGrid.show();
            });
          }
          // Only one subgrid remains visible, use its width as splits width
          if (regions.length === 2) {
            split._initialWidth = split.element.style.width;
            split._initialFlex = split.flex;
            split.width = subGrid._initialWidth;
            isOriginal && me.restorers.push(() => {
              if (split._initialFlex !== null) {
                split.flex = split._initialFlex;
              } else if (split._initialWidth !== null) {
                split.width = split._initialWidth;
              }
            });
          }
        }
      }
    }
  }
  /**
   * Split the grid into two or four parts.
   *
   * - Splits into two when passed `direction : 'vertical'`, `direction : 'horizontal'` or `atColumn` or `atRecord`.
   * - Splits into four when passed `direction : 'both'` or `atColumn` and `atRecord`.
   *
   * ```javascript
   * // Split horizontally (at the row in the center of the grid)
   * await grid.split({ direction : 'horizontal' });
   *
   * // Split both ways by a specific column and record
   * await grid.split({
   *    atRecord : grid.store.getById(10),
   *    atColumn : grid.columns.get('city')
   * });
   * ```
   *
   * To return to a single grid, call {@link #function-unsplit}.
   *
   * Note that this function is callable directly on the grid instance.
   *
   * @param {Object} [options] Split options
   * @param {'vertical'|'horizontal'|'both'} [options.direction] Split direction, 'vertical', 'horizontal' or 'both'.
   * Not needed when passing `atColumn` or `atRecord`.
   * @param {Grid.column.Column} [options.atColumn] Column to split at
   * @param {Core.data.Model} [options.atRecord] Record to split at
   * @returns {Promise} Resolves when split is complete, and subviews are scrolled to the correct position.
   * @async
   * @on-owner
   * @category Common
   */
  async split(options = {}) {
    const me = this,
      {
        client
      } = me;
    // Can't split a split
    if (client.splitFrom) {
      return;
    }
    if (me.isSplit) {
      await me.unsplit(true);
    }
    const {
        rtl
      } = client,
      {
        atRecord,
        atColumn,
        direction
      } = me.processOptions(options);
    let {
        splitX,
        remainingWidth
      } = options,
      splitY = null,
      remainingHeight = null;
    if (atRecord) {
      await client.scrollRowIntoView(atRecord);
      const row = client.getRowFor(atRecord);
      if (!row) {
        throw new Error(`Could not find row for record ${atRecord.id}`);
      }
      splitY = Rectangle.from(row.cells[0], client.element).bottom;
      remainingHeight = Rectangle.from(client.element).height - splitY;
    }
    if (atColumn && !splitX) {
      splitX = Rectangle.from(atColumn.element, client.element).getEnd(rtl);
      remainingWidth = Rectangle.from(client.element).width - splitX - DomHelper.scrollBarWidth;
      if (rtl) {
        const x = splitX;
        splitX = remainingWidth + DomHelper.scrollBarWidth;
        remainingWidth = x - DomHelper.scrollBarWidth;
      }
    }
    const scrollPromises = [],
      splitContainer = me.createSplitContainer(options),
      {
        visibleColumns
      } = client.columns,
      nextColumn = atColumn ? visibleColumns[visibleColumns.indexOf(atColumn) + 1] : null,
      nextRecord = atRecord ? client.store.getNext(atRecord) : null;
    client.eachSubGrid(subGrid => subGrid._initialWidth = subGrid.width);
    if (direction !== 'both') {
      const cloneConfig = {
        flex: `0 0 ${(splitY != null ? remainingHeight : remainingWidth) - splitterWidth}px`,
        height: null
      };
      // Horizontal or vertical, only needs one splitter and one clone
      const [, clone] = me.widgets = [new Splitter({
        appendTo: splitContainer
      }), me.cloneClient(splitContainer, direction === 'vertical' ? 1 : 0, options, cloneConfig)];
      if (splitX != null) {
        // It does not like being thrown around in DOM and resized when scrolled, fix up
        client.renderRows();
        // Don't bother scrolling here if given a date, Schedulers feature handles that
        if (!options.atDate) {
          scrollPromises.push(client.scrollColumnIntoView(atColumn, endScrollOptions));
          nextColumn && scrollPromises.push(clone.scrollColumnIntoView(nextColumn, startScrollOptions));
        }
      }
      if (splitY != null) {
        // Always have an atRecord to split at
        scrollPromises.push(clone.scrollRowIntoView(nextRecord, startScrollOptions));
      }
      client.element.classList.add('b-split-start');
      clone.element.classList.add('b-split-end');
      // Sync scrolling
      client.scrollable.addPartner(clone.scrollable, {
        x: direction === 'horizontal',
        y: direction !== 'horizontal'
      });
    } else {
      const rightConfig = {
        flex: `0 0 ${remainingWidth - splitterWidth}px`
      };
      splitContainer.lastElementChild.style.flex = `0 0 ${remainingHeight - splitterWidth}px`;
      // Both directions, 3 splitters (one horizontal with full span, two vertical halves) and 3 clones
      me.widgets = [new Splitter({
        insertBefore: splitContainer.lastElementChild
      }),
      // Full horizontal
      me.topSplitter = new Splitter({
        appendTo: splitContainer.firstElementChild
      }),
      // Top vertical
      me.cloneClient(splitContainer.firstElementChild, 1, options, rightConfig),
      // Top right
      me.cloneClient(splitContainer.lastElementChild, 0, options),
      // Bottom left
      me.bottomSplitter = new Splitter({
        appendTo: splitContainer.lastElementChild
      }),
      // Bottom vertical
      me.cloneClient(splitContainer.lastElementChild, 2, options, rightConfig) // Bottom right
      ];

      const topLeft = client,
        topRight = me.widgets[2],
        bottomLeft = me.widgets[3],
        bottomRight = me.widgets[5];
      topLeft.element.classList.add('b-split-top-start');
      topRight.element.classList.add('b-split-top-end');
      bottomLeft.element.classList.add('b-split-bottom-start');
      bottomRight.element.classList.add('b-split-bottom-end');
      if (splitX != null) {
        // It does not like being thrown around in DOM and resized when scrolled, fix up
        topLeft.renderRows();
        bottomLeft.renderRows();
        // Don't bother scrolling here if given a date, Schedulers feature handles that
        if (atColumn && !options.atDate) {
          scrollPromises.push(client.scrollColumnIntoView(atColumn, endScrollOptions));
          nextColumn && scrollPromises.push(topRight.scrollColumnIntoView(nextColumn, startScrollOptions));
        }
      }
      if (splitY != null) {
        scrollPromises.push(bottomLeft.scrollRowIntoView(nextRecord, startScrollOptions), bottomRight.scrollRowIntoView(nextRecord, startScrollOptions));
      }
      // Sync scrolling
      topLeft.scrollable.addPartner(topRight.scrollable, 'y');
      topLeft.scrollable.addPartner(bottomLeft.scrollable, 'x');
      topRight.scrollable.addPartner(bottomRight.scrollable, 'x');
      bottomLeft.scrollable.addPartner(bottomRight.scrollable, 'y');
      // Set up vertical splitter sync
      me.topSplitter.ion({
        splitterMouseDown: 'onSplitterMouseDown',
        drag: 'onSplitterDrag',
        drop: 'onSplitterDrop',
        thisObj: me
      });
      me.bottomSplitter.ion({
        splitterMouseDown: 'onSplitterMouseDown',
        drag: 'onSplitterDrag',
        drop: 'onSplitterDrop',
        thisObj: me
      });
    }
    me.subViews = [client, ...me.widgets.filter(w => w.isGridBase)];
    me.toggleOriginalSubGrids(options);
    me.syncHeaderHeights();
    me._splitOptions = options;
    await Promise.all(scrollPromises);
    // Moving in DOM does not seem to trigger resize, do it manually
    const bounds = Rectangle.from(client.element);
    client.onInternalResize(client.element, bounds.width, bounds.height);
    client.eachSubGrid(subGrid => {
      const subGridBounds = Rectangle.from(subGrid.element);
      subGrid.onInternalResize(subGrid.element, subGridBounds.width, subGridBounds.height);
    });
    // If scrolled, the original element gets out of sync when moved around in DOM
    client.scrollable.x += 0.5;
    client.scrollable.y += 0.5;
    me.startSyncingColumns();
    /**
     * Fires when splitting the Grid.
     * @event split
     * @param {Grid.view.GridBase[]} subViews The sub views created by the split
     * @param {Object} options The options passed to the split call
     * @param {'horizontal'|'vertical'|'both'} options.direction The direction of the split
     * @param {Grid.column.Column} options.atColumn The column to split at
     * @param {Core.data.Model} options.atRecord The record to split at
     * @on-owner
     */
    client.trigger('split', {
      subViews: me.subViews,
      options
    });
    return me.subViews;
  }
  /**
   * Remove splits, returning to a single grid.
   *
   * Note that this function is callable directly on the grid instance.
   *
   * @on-owner
   * @async
   * @category Common
   */
  async unsplit(silent = false) {
    const me = this,
      {
        client
      } = me,
      {
        element
      } = client;
    if (me.isSplit) {
      var _me$widgets;
      me.stopSyncingColumns();
      (_me$widgets = me.widgets) === null || _me$widgets === void 0 ? void 0 : _me$widgets.forEach(split => split.destroy());
      me.widgets = null;
      // Safari & FF looses scroll position when moving elements around in DOM,
      // but reading it here fixes it
      client.eachSubGrid(subGrid => subGrid.scrollable.x);
      client.scrollable.y;
      me.splitContainer.parentElement.appendChild(element);
      me.splitContainer.remove();
      me.splitContainer = null;
      // Reset any size applied by splitter
      element.style.flexBasis = element.style.flexGrow = '';
      element.classList.remove('b-split-top-start', 'b-split-start');
      me.subViews.length = 0;
      if (!me.isDestroying) {
        // We have been pretty violent with the DOM, so force a repaint of rows
        client.renderRows();
        me.unsplitCleanup();
        for (const restorer of me.restorers) {
          restorer();
        }
        me.restorers.length = 0;
        // Ugly, but FF needs a couple of frames to not lose scroll position if we are splitting again
        await AsyncHelper.animationFrame();
        await AsyncHelper.animationFrame();
        if (me.isDestroyed) {
          return;
        }
        /**
         * Fires when un-splitting the Grid.
         * @event unsplit
         * @on-owner
         */
        !silent && client.trigger('unsplit');
        me._splitOptions = null;
      }
    }
  }
  unsplitCleanup() {}
  //endregion
  //region Context menu
  populateCellMenu({
    record,
    column,
    items
  }) {
    const me = this,
      {
        isSplit
      } = me,
      {
        splitFrom
      } = me.client;
    if (!me.disabled) {
      items.splitGrid = {
        text: 'L{split}',
        localeClass: me,
        icon: 'b-icon b-icon-split-vertical',
        weight: 500,
        separator: true,
        hidden: isSplit || splitFrom,
        menu: {
          splitHorizontally: {
            text: 'L{horizontally}',
            icon: 'b-icon b-icon-split-horizontal',
            localeClass: me,
            weight: 100,
            onItem() {
              me.split({
                atRecord: record
              });
            }
          },
          splitVertically: {
            text: 'L{vertically}',
            icon: 'b-icon b-icon-split-vertical',
            localeClass: me,
            weight: 200,
            onItem() {
              me.split({
                atColumn: column
              });
            }
          },
          splitBoth: {
            text: 'L{both}',
            icon: 'b-icon b-icon-split-both',
            localeClass: me,
            weight: 300,
            onItem() {
              me.split({
                atColumn: column,
                atRecord: record
              });
            }
          }
        }
      };
      items.unsplitGrid = {
        text: 'L{unsplit}',
        localeClass: me,
        icon: 'b-icon b-icon-clear',
        hidden: !(isSplit || splitFrom),
        weight: 400,
        separator: true,
        onItem() {
          (splitFrom || me).unsplit();
        }
      };
    }
  }
  //endregion
  //region Syncing columns
  startSyncingColumns() {
    for (const subView of this.subViews) {
      subView.columns.ion({
        name: 'columns',
        change: 'onColumnsChange',
        thisObj: this
      });
    }
  }
  stopSyncingColumns() {
    this.detachListeners('columns');
  }
  onColumnsChange({
    source,
    isMove,
    action,
    /*index, */parent,
    records,
    changes
  }) {
    const me = this;
    if (!me.#ignoreColumnChanges) {
      me.#ignoreColumnChanges = true;
      for (const clone of me.subViews) {
        const {
          columns
        } = clone;
        if (source !== columns) {
          // Special handling for column moved from subgrid not in split to subgrid in split
          if (action === 'update' && changes.region && Object.keys(changes).length === 1) {
            // Move from non-existing to existing, add
            if (!columns.getById(records[0].id)) {
              const [column] = records,
                targetParent = columns.getById(me.$before.parent.id) ?? columns.rootNode,
                targetBefore = me.$before.id !== null && columns.getById(me.$before.id);
              targetParent.insertChild(column.data, targetBefore);
            }
            // Vice versa, remove
            else {
              columns.remove(records[0].id);
            }
            me.$before = null;
          } else if (!(isMove !== null && isMove !== void 0 && isMove[records[0].id])) {
            if (action === 'add') {
              // Only add columns that are in a subgrid that is visible in the clone
              const relevantColumns = records.filter(column => clone.getSubGridFromColumn(column));
              columns.add(me.cloneColumns(relevantColumns));
            } else {
              columns.applyChangesFromStore(source);
            }
          }
          // We have to handle move separately, since it does not leave the column store modified (in any
          // meaningful way)
          else if (action === 'add') {
            const sourceColumn = records[0],
              sourceBefore = sourceColumn.nextSibling,
              targetColumn = columns.getById(sourceColumn.id); //columns.allRecords.find(r => r.id === sourceColumn.id);
            // When splitting a multi-region grid, not all columns are present in all splits. But, it might
            // be moved from locked to normal (etc.) in original, but split is not showing source region. In
            // that case, we handle it on the region update - and must store details here
            if (!targetColumn) {
              me.$before = {
                id: sourceBefore === null || sourceBefore === void 0 ? void 0 : sourceBefore.id,
                parent
              };
              me.#ignoreColumnChanges = false;
              return;
            }
            if (sourceColumn.meta.isSelectionColumn) {
              me.#ignoreColumnChanges = false;
              return;
            }
            const targetParent = columns.getById(parent.id) ?? columns.rootNode,
              targetBefore = sourceBefore && columns.getById(sourceBefore.id);
            targetParent.insertChild(targetColumn, targetBefore);
          }
          columns.commit();
        }
      }
      source.commit();
      me.#ignoreColumnChanges = false;
    }
  }
  //endregion
  //region Syncing splitters
  getOtherSplitter(splitter) {
    return splitter === this.topSplitter ? this.bottomSplitter : this.topSplitter;
  }
  onSplitterMouseDown({
    source,
    event
  }) {
    if (!event.handled) {
      event.handled = true;
      this.getOtherSplitter(source).onMouseDown(event);
    }
  }
  onSplitterDrag({
    source,
    event
  }) {
    if (!event.handled) {
      event.handled = true;
      this.getOtherSplitter(source).onMouseMove(event);
    }
  }
  onSplitterDrop({
    source,
    event
  }) {
    if (!event.handled) {
      event.handled = true;
      this.getOtherSplitter(source).onMouseUp(event);
    }
  }
  //endregion
  //region Relaying property changes & events
  // Relay relevant config changes to other splits
  afterConfigChange({
    name,
    value
  }) {
    if (this.isSplit && this.relayProperties[name]) {
      this.syncSplits(split => {
        split[name] = value;
      });
    }
  }
  // Sync listeners added at runtime to other splits
  afterAddListener(eventName, listener) {
    if (this.isSplit && !listener.$internal && !ignoreListeners[eventName]) {
      // Not using `ion()` on purpose, these are app listeners
      // eslint-disable-next-line bryntum/no-on-in-lib
      this.syncSplits(split => split.on(eventName, listener));
    }
  }
  afterRemoveListener(eventName, listener) {
    if (!listener.$internal) {
      this.syncSplits(split => split.un(eventName, listener));
    }
  }
  //endregion
  //region Util
  // Call a fn for all splits except the on this fn is called on
  forEachOther(fn) {
    const original = this.client.splitFrom || this.client;
    if (original.features.split.enabled && !original.inForEachOther) {
      // Protect against infinite recursion by being called from the fn
      original.inForEachOther = true;
      for (const view of original.subViews) {
        if (view !== this.client) {
          fn(view);
        }
      }
      original.inForEachOther = false;
    }
  }
  syncSplits(fn) {
    this.forEachOther(fn);
  }
  //endregion
}

Split._$name = 'Split';
GridFeatureManager.registerFeature(Split, false);

/**
 * @module Grid/util/TableExporter
 */
/**
 * This class transforms grid component into two arrays: rows and columns. Columns array contains objects with
 * meta information about column: field name, column name, width and type of the rendered value, rows array contains
 * arrays of cell values.
 *
 * ```javascript
 * const exporter = new TableExporter({ target : grid });
 * exporter.export()
 *
 * // Output
 * {
 *     columns : [
 *         { field : 'name',     value : 'First name', type : 'string',  width : 100 },
 *         { field : 'surname',  value : 'Last name',  type : 'string',  width : 100 },
 *         { field : 'age',      value : 'Age',        type : 'number',  width : 50  },
 *         { field : 'married',  value : 'Married',    type : 'boolean', width : 50  },
 *         { field : 'children', value : 'Children',   type : 'object',  width : 100 }
 *     ],
 *     rows : [
 *         ['Michael', 'Scott',   40, false, []],
 *         ['Jim',     'Halpert', 30, true,  [...]]
 *     ]
 * }
 * ```
 *
 * ## How data is exported
 *
 * Exporter iterates over store records and processes each record for each column being exported. Exporter uses same
 * approach to retrieve data as column: reading record field, configured on the column, or calling renderer function
 * if one is provided. This means data can be of any type: primitives or objects. So children array in the above code
 * snippet may contain instances of child record class.
 *
 * ## Column renderers
 *
 * Column renderers are commonly used to style the cell, or even render more HTML into it, like {@link Grid.column.WidgetColumn}
 * does. This is not applicable in case of export. Also, given grid uses virtual rendering (only renders visible rows) and
 * exporter iterates over all records, not just visible ones, we cannot provide all data necessary to the renderer. Some
 * arguments, like cellElement and row, wouldn't exist. Thus renderer is called with as much data we have: value,
 * record, column, grid, other {@link Grid.column.Column#config-renderer documented arguments} would be undefined.
 *
 * Exporter adds one more flag for renderer function: isExport. When renderer receives this flag it knows
 * data is being exported and can skip DOM work to return simpler value. Below snippet shows simplified code of the
 * widget column handling export:
 *
 * ```javascript
 * renderer({ isExport }) {
 *     if (isExport) {
 *         return null;
 *     }
 *     else {
 *         // widget rendering routine
 *         ...
 *     }
 * }
 * ```
 *
 * ## Column types
 *
 * Column types are not actually a complete list of JavaScript types (you can get actual type of the cell using typeof) it
 * is a simple and helpful meta information.
 *
 * Available column types are:
 *  * string
 *  * number
 *  * boolean
 *  * date
 *  * object
 *
 * Everything which is not primitive like string/number/bool (or a date) is considered an object. This includes null, undefined,
 * arrays, classes, functions etc.
 *
 * ## Getting column type
 *
 * If existing grid column is used, column type first would be checked with {@link Grid.column.Column#config-exportedType exportedType}
 * config. If exportedType is undefined or column does not exist in grid, type is read from a record field definition.
 * If the field is not defined, object type is used.
 *
 * Configuring exported type:
 *
 * ```javascript
 * new Grid({
 *     columns : [
 *         {
 *             name         : 'Name',
 *             field        : 'name',
 *             exportedType : 'object',
 *             renderer     : ({ value, isExport }) => {
 *                 if (isExport) {
 *                     return { value }; // return value wrapped into object
 *                 }
 *             }
 *     ]
 * })
 * ```
 *
 * @extends Core/Base
 */
class TableExporter extends Base {
  static get defaultConfig() {
    return {
      /**
       * Target grid instance to export data from
       * @config {Grid.view.Grid} target
       */
      target: null,
      /**
       * Specifies a default column width if no width specified
       * @config {Number} defaultColumnWidth
       * @default
       */
      defaultColumnWidth: 100,
      /**
       * Set to false to export date as it is displayed by Date column formatter
       * @config {Boolean}
       * @default
       */
      exportDateAsInstance: true,
      /**
       * If true and the grid is grouped, shows the grouped value in the first column. True by default.
       * @config {Boolean} showGroupHeader
       * @default
       */
      showGroupHeader: true,
      /**
       * An array of column configuration objects used to specify column widths, header text, and data fields to get the data from.
       * 'field' config is required. If 'text' is missing, it will read it from the grid column or the 'field' config.
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
      columns: null,
      /**
       * When true and tree is being exported, node names are indented with {@link #config-indentationSymbol}
       * @config {Boolean}
       * @default
       */
      indent: true,
      /**
       * This symbol (four spaces by default) is used to indent node names when {@link #config-indent} is true
       * @config {String}
       * @default
       */
      indentationSymbol: '\u00A0\u00A0\u00A0\u00A0'
    };
  }
  /**
   * Exports grid data according to provided config
   * @param {Object} config
   * @returns {{ rows : Object[][], columns : Object[] }}
   */
  export(config = {}) {
    const me = this;
    config = ObjectHelper.assign({}, me.config, config);
    me.normalizeColumns(config);
    return me.generateExportData(config);
  }
  generateExportData(config) {
    const me = this,
      columns = me.generateColumns(config),
      rows = me.generateRows(config);
    return {
      rows,
      columns
    };
  }
  normalizeColumns(config) {
    // In case columns are provided we need to use normalized config. If those are not provided, we are going
    // to use real columns, possible invoking renderers (we need to pass column instance to the renderer to
    // avoid breaking API too much)
    const columns = config.columns || this.target.columns.visibleColumns.filter(rec => rec.exportable !== false);
    config.columns = columns.map(col => {
      if (typeof col === 'string') {
        return this.target.columns.find(column => column.field === col) || {
          field: col
        };
      } else {
        return col;
      }
    });
  }
  generateColumns(config) {
    return config.columns.map(col => this.processColumn(col, config));
  }
  generateRows(config) {
    const {
      columns,
      rows
    } = config;
    if (columns.length === 0 || (rows === null || rows === void 0 ? void 0 : rows.length) === 0) {
      return [];
    }
    const me = this,
      {
        target
      } = me;
    return (rows || target.store
    // although columns are taken from config, it is convenient to provide them as a separate argument
    // because that allows to override set of columns to process
    ).map(record => me.processRecord(record, columns, config))
    // filter out empty rows
    .filter(cells => cells === null || cells === void 0 ? void 0 : cells.length);
  }
  getColumnType(column, store = this.target.store) {
    let result = column.exportedType || 'object';
    if (column.exportedType === undefined) {
      if (column.field) {
        const fieldDefinition = store.modelClass.getFieldDefinition(column.field);
        if (fieldDefinition && fieldDefinition.type !== 'auto') {
          result = fieldDefinition.type;
        }
      }
    }
    return result;
  }
  /**
   * Extracts export data from the column instance
   * @param {Grid.column.Column} column
   * @param {Object} config
   * @private
   * @returns {Object}
   */
  processColumn(column, config) {
    const me = this,
      {
        target
      } = me,
      {
        defaultColumnWidth
      } = config;
    let {
      field,
      text: value,
      width,
      minWidth
    } = column;
    // If column is not configured with field, field is generated (see Column.js around line 514).
    // In export we want empty string there
    if (!(field in target.store.modelClass.fieldMap)) {
      field = '';
    }
    // If name or width is missing try to retrieve them from the grid column and the field, or use default values.
    if (!value || !width) {
      const gridColumn = target.columns.find(col => col.field === field);
      if (!value) {
        value = gridColumn && gridColumn.text || field;
      }
      // null or undefined
      if (width == null) {
        width = gridColumn && gridColumn.width || defaultColumnWidth;
      }
    }
    width = Math.max(width || defaultColumnWidth, minWidth || defaultColumnWidth);
    return {
      field,
      value,
      width,
      type: me.getColumnType(column)
    };
  }
  /**
   * Extracts export data from the record instance reading supplied column configs
   * @param {Core.data.Model|null} record If null is passed, all columns will be filled with empty strings
   * @param {Grid.column.Column[]} columns
   * @param {Object} config
   * @private
   * @returns {Object[]}
   */
  processRecord(record, columns, config) {
    const {
        target
      } = this,
      {
        showGroupHeader,
        indent,
        indentationSymbol
      } = config;
    let cells;
    if (!record) {
      cells = columns.map(() => '');
    } else if (record.isSpecialRow) {
      if (showGroupHeader && record.meta.groupRowFor) {
        cells = columns.map(column => {
          return target.features.group.buildGroupHeader({
            // Create dummy element to get html from
            cellElement: DomHelper.createElement(),
            grid: target,
            record,
            column
          });
        });
      }
    } else {
      cells = columns.map(column => {
        let value = record.getValue(column.field);
        const useRenderer = column.renderer || column.defaultRenderer;
        if (useRenderer && !(value && column.isDateColumn && config.exportDateAsInstance)) {
          value = useRenderer.call(column, {
            value,
            record,
            column,
            grid: target,
            isExport: true
          });
        }
        if (indent && column.tree) {
          value = `${indentationSymbol.repeat(record.childLevel)}${value}`;
        }
        return value;
      });
    }
    return cells;
  }
}
TableExporter._$name = 'TableExporter';

class BooleanUnicodeSymbol {
  constructor(value) {
    this._value = value;
  }
  get value() {
    return this._value;
  }
  toString() {
    return Boolean(this.value) ? '✓' : '';
  }
}
BooleanUnicodeSymbol._$name = 'BooleanUnicodeSymbol';

/**
 * @module Grid/feature/experimental/ExcelExporter
 */
/**
 * **NOTE**: This class requires a 3rd party library to operate.
 *
 * A feature that allows exporting Grid data to Excel without involving the server. It uses {@link Grid.util.TableExporter}
 * class as data provider, [zipcelx library](https://www.npmjs.com/package/zipcelx)
 * forked and adjusted to support [column width config](https://github.com/bryntum/zipcelx/tree/column-width-build)
 * and [Microsoft XML specification](https://msdn.microsoft.com/en-us/library/office/documentformat.openxml.spreadsheet.aspx).
 * Zipcelx should be either in global scope (window) or can be provided with {@link #config-zipcelx} config.
 *
 * ```html
 * // Global scope
 * <script src="zipcelx.js"></script>
 * ```
 *
 * ```javascript
 * // importing from package
 * import zipcelx from 'zipcelx';
 *
 * const grid = new Grid({
 *     features : {
 *         excelExporter : {
 *             zipcelx
 *         }
 *     }
 * })
 * ```
 *
 * Here is an example of how to add the feature:
 *
 * ```javascript
 * const grid = new Grid({
 *     features : {
 *         excelExporter : {
 *             // Choose the date format for date fields
 *             dateFormat : 'YYYY-MM-DD HH:mm',
 *
 *             exporterConfig : {
 *                 // Choose the columns to include in the exported file
 *                 columns : ['name', 'role'],
 *                 // Optional, export only selected rows
 *                 rows    : grid.selectedRecords
 *             }
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
 *     exporterConfig : {
 *         columns : [
 *             { text : 'First Name', field : 'firstName', width : 90 },
 *             { text : 'Age', field : 'age', width : 40 },
 *             { text : 'Starts', field : 'start', width : 140 },
 *             { text : 'Ends', field : 'finish', width : 140 }
 *         ]
 *     }
 * })
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Grid/exporttoexcel
 * @classtype excelExporter
 * @feature
 */
class ExcelExporter extends InstancePlugin {
  static get $name() {
    return 'ExcelExporter';
  }
  static get defaultConfig() {
    return {
      /**
       * Name of the exported file
       * @config {String} filename
       * @default
       */
      filename: null,
      /**
       * Defines how dates in a cell will be formatted
       * @config {String} dateFormat
       * @default
       */
      dateFormat: 'YYYY-MM-DD',
      /**
       * Exporter class to use as a data provider. {@link Grid.util.TableExporter} by default.
       * @config {Grid.util.TableExporter}
       * @typings {typeof TableExporter}
       * @default
       */
      exporterClass: TableExporter,
      /**
       * Configuration object for {@link #config-exporterClass exporter class}.
       * @config {Object}
       */
      exporterConfig: null,
      /**
       * Reference to zipcelx library. If not provided, exporter will look in the global scope.
       * @config {Object}
       */
      zipcelx: null,
      /**
       * If this config is true, exporter will convert all empty values to ''. Empty values are:
       * * undefined, null, NaN
       * * Objects/class instances that do not have toString method defined and are stringified to [object Object]
       * * functions
       * @config {Boolean}
       */
      convertEmptyValueToEmptyString: true
    };
  }
  processValue(value) {
    if (value === undefined || value === null || Number.isNaN(value) || typeof value === 'function' || typeof value === 'object' && String(value) === '[object Object]') {
      return '';
    } else {
      return value;
    }
  }
  generateExportData(config) {
    const me = this,
      {
        rows,
        columns
      } = me.exporter.export(config.exporterConfig);
    return {
      rows: rows.map(row => {
        return row.map((value, index) => {
          var _columns$index;
          if (value instanceof Date) {
            value = DateHelper.format(value, config.dateFormat);
          } else if (typeof value === 'boolean') {
            value = new BooleanUnicodeSymbol(value);
          }
          if (me.convertEmptyValueToEmptyString) {
            value = me.processValue(value);
          }
          const type = ((_columns$index = columns[index]) === null || _columns$index === void 0 ? void 0 : _columns$index.type) === 'number' ? 'number' : 'string';
          return {
            value,
            type
          };
        });
      }),
      columns: columns.map(col => {
        let {
          field,
          value,
          width,
          type
        } = col;
        // when number column is exported with zipcelx, excel warns that sheet is broken and asks for repair
        // repair works, but having error on open doesn't look acceptable
        // type = type === 'number' ? 'number' : 'string';
        type = 'string';
        return {
          field,
          value,
          width,
          type
        };
      })
    };
  }
  /**
   * Generate and download an Excel file (.xslx).
   * @param {Object} config Optional configuration object, which overrides initial settings of the feature/exporter.
   * @param {String} [config.filename] Name of the exported file
   * @param {String} [config.dateFormat] Defines how dates in a cell will be formatted
   * @param {String[]|Object[]} [config.columns] An array of column configuration objects
   * @param {Core.data.Model[]} [config.rows] An array of records to export
   * @returns {Promise} Promise that resolves when the export is completed
   */
  export(config = {}) {
    const me = this,
      zipcelx = me.zipcelx || globalThis.zipcelx;
    if (!zipcelx) {
      throw new Error('ExcelExporter: "zipcelx" library is required');
    }
    if (me.disabled) {
      return;
    }
    config = ObjectHelper.assign({}, me.config, config);
    if (!config.filename) {
      config.filename = me.client.$$name;
    }
    const {
        filename
      } = config,
      {
        rows,
        columns
      } = me.generateExportData(config);
    return zipcelx({
      filename,
      sheet: {
        data: [columns].concat(rows),
        cols: columns
      }
    });
  }
  construct(grid, config) {
    super.construct(grid, config);
    if (!this.zipcelx) {
      if (typeof zipcelx !== 'undefined') {
        this.zipcelx = globalThis.zipcelx;
      }
    }
  }
  get exporter() {
    const me = this;
    return me._exporter || (me._exporter = me.exporterClass.new({
      target: me.client
    }, me.exporterConfig));
  }
}
ExcelExporter._$name = 'ExcelExporter';
GridFeatureManager.registerFeature(ExcelExporter, false, 'Grid');

export { ColorColumn, ExcelExporter, GroupSummary, Split, TableExporter };
//# sourceMappingURL=ExcelExporter.js.map
