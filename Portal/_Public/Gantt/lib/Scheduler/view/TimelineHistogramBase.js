import Objects from '../../Core/helper/util/Objects.js';
import Histogram from '../../Core/widget/graph/Histogram.js';
import Tooltip from '../../Core/widget/Tooltip.js';
import '../../Grid/column/TreeColumn.js';
import '../../Grid/feature/Tree.js';
import '../column/TimeAxisColumn.js';
import '../feature/ColumnLines.js';
import DelayedRecordsRendering from './mixin/DelayedRecordsRendering.js';
import TimelineBase from './TimelineBase.js';
import TimelineHistogramRendering from './TimelineHistogramRendering.js';

/**
 * @module Scheduler/view/TimelineHistogramBase
 */

const
    histogramWidgetCleanState = {
        series   : null,
        topValue : null
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
export default class TimelineHistogramBase extends TimelineBase.mixin(DelayedRecordsRendering) {

    //region Config

    static $name = 'TimelineHistogramBase';

    static type = 'timelinehistogrambase';

    static configurable = {

        timeAxisColumnCellCls : 'b-sch-timeaxis-cell b-timelinehistogram-cell',

        mode : 'horizontal',

        rowHeight : 50,

        /**
         * Set to `true` if you want to display a tooltip when hovering an allocation bar. You can also pass a
         * {@link Core/widget/Tooltip#configs} config object.
         * Please use {@link #config-barTooltipTemplate} function to customize the tooltip contents.
         * @config {Boolean|TooltipConfig}
         */
        showBarTip : false,

        barTooltip : null,

        barTooltipClass : Tooltip,

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
        series : null,

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
        dataModelField : 'histogramData',

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
        getRecordData : null,

        /**
         * When set to `true` (default) the component reacts on time axis changes
         * (zooming or changing the displayed time span), clears the histogram data cache of the records
         * and then refreshes the view.
         * @config {Boolean}
         * @default
         */
        hardRefreshOnTimeAxisReconfigure : true,

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
        getRectClass : null,

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

        readOnly : true,


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
        getBarTip : null,

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
        barTooltipTemplate : null,

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
        getBarText : null,

        getRectConfig : null,

        getBarTextRenderData : undefined,

        /**
         * The class used for building the {@link #property-histogramWidget histogram widget}
         * @config {Core.widget.graph.Histogram}
         * @default
         */
        histogramWidgetClass : Histogram,

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
        histogramWidget : {
            cls                : 'b-hide-offscreen b-timelinehistogram-histogram',
            omitZeroHeightBars : true,
            data               : []
        },

        fixedRowHeight : true
    };

    static get properties() {
        return {
            histogramDataByRecord : new Map(),
            collectingDataFor     : new Map()
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
                endReconfigure : 'onTimeAxisEndReconfigure',
                thisObj        : this
            });
        }
        else {
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
            beforeRowHeight : 'onBeforeRowHeight',
            thisObj         : me
        });
    }

    onDestroy() {
        this.clearHistogramDataCache();
        this._histogramWidget?.destroy();
        this.barTooltip = null;
    }

    //endregion

    //region Internal

    // Used by shared features to resolve an event or task
    resolveTimeSpanRecord(element) {}

    getScheduleMouseEventParams(cellData, event) {
        const record = this.store.getById(cellData.id);

        return { record };
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

    onPaint({ firstPaint }) {
        super.onPaint({ firstPaint });

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
        oldTooltip?.destroy();

        if (tooltip) {
            return tooltip.isTooltip ? tooltip : this.barTooltipClass.new({
                forElement  : this.timeAxisSubGridElement,
                forSelector : '.b-histogram rect',
                hoverDelay  : 0,
                trackMouse  : false,
                cls         : 'b-celltooltip-tip',
                getHtml     : this.getTipHtml.bind(this)
            }, this.showBarTip, tooltip);
        }

        return null;
    }

    async getTipHtml(args) {
        if (this.showBarTip && this.barTooltipTemplate) {
            const
                { activeTarget } = args,
                index            = parseInt(activeTarget.dataset.index, 10),
                record           = this.getRecordFromElement(activeTarget),
                histogramData    = await this.getRecordHistogramData(record);

            return this.barTooltipTemplate({
                ...args,
                datum : this.extractHistogramDataArray(histogramData, record)[index],
                record,
                index
            });
        }
    }

    collectTicksWidth() {
        const
            { ticks }     = this.timeAxis,
            prevDuration  = ticks[0].endDate - ticks[0].startDate,
            tickDurations = { 0 : prevDuration };

        let
            totalDuration = prevDuration,
            isMonotonous  = true;

        for (let i = 1, { length } = ticks; i < length; i++) {
            const
                tick   = ticks[i],
                duration = tick.endDate - tick.startDate;

            // the ticks width is different -> reset isMonotonous flag
            if (prevDuration !== duration) {
                isMonotonous = false;
            }

            totalDuration    += duration;
            tickDurations[i] = duration;
        }

        // if the ticks widths are not monotonous we need to calculate
        // each bar width to provide it to the histogram widget later
        if (!isMonotonous) {
            const ticksWidth = {};
            for (let i = 0, { length } = ticks; i < length; i++) {
                ticksWidth[i] = tickDurations[i] / totalDuration;
            }
            this.ticksWidth = ticksWidth;
        }
        else {
            this.ticksWidth = null;
        }
    }

    changeHistogramWidget(widget) {
        const me = this;

        if (widget && !widget.isHistogram) {
            if (me.getBarTextRenderData && !widget.getBarTextRenderData) {
                widget.getBarTextRenderData = me.getBarTextRenderData;
            }

            widget = me.histogramWidgetClass.new({
                owner           : me,
                appendTo        : me.element,
                height          : me.rowHeight,
                width           : me.timeAxisColumn?.width || 0,
                getBarTip       : !me.barTooltipTemplate && me.getBarTip || emptyFn,
                getRectClass    : me.getRectClass || me.getRectClassDefault,
                getBarText      : me.getBarText || me.getBarTextDefault,
                getOutlineClass : me.getOutlineClass,
                getRectConfig   : me.getRectConfig
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

    onHistogramDataCacheSet({ record, data }) {
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

    onStoreUpdateRecord({ record, changes }) {
        const me = this;

        // If we read histogram data from a field and that field got changed
        // - clear the corresponding record cache
        if (!me.getRecordData && me.dataModelField && changes[me.dataModelField]) {
            me.clearHistogramDataCache(record);
        }

        return super.onStoreUpdateRecord(...arguments);
    }

    onStoreRemove({ records }) {
        super.onStoreRemove(...arguments);

        for (const record of records) {
            this.clearHistogramDataCache(record);
        }
    }

    onBeforeRowHeight({ height }) {

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
        }
        else {
            this.histogramDataByRecord.clear();
        }
    }

    /**
     * Caches the provided histogram data for the given record.
     * @param {Core.data.Model} record Record to cache data for.
     * @param {Object} data Histogram data to cache.
     */
    setHistogramDataCache(record, data) {
        const eventData = { record, data };

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
        const
            me     = this,
            { getRecordData } = me;

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
        const
            me = this,
            histogramData = me.getRecordHistogramData(eventData.record);

        if (!Objects.isPromise(histogramData)) {
            const data = histogramData ? me.extractHistogramDataArray(histogramData, eventData.record) : [];

            // if ticks widths are not monotonous
            // we provide widths for each bar since in that case the histogram widget
            // won't be able to calculate them properly
            if (me.ticksWidth) {
                for (let i = 0, { length } = data; i < length; i++) {
                    data[i].width = me.ticksWidth[i];
                }
            }

            const histogramConfig = Objects.merge(
                // reset topValue by default to enable its auto-detection
                { topValue : null },
                me.initialConfig.histogramWidget,
                {
                    data,
                    series : { ...me.series }
                });

            eventData = {
                ...eventData,
                histogramConfig,
                histogramData,
                histogramWidget : me.histogramWidget
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
        const
            me = this,
            { histogramData, cellElement } = renderData;

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
            args : [renderData]
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
        const
            me            = this,
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
