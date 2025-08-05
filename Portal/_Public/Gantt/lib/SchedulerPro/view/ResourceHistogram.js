import StringHelper from '../../Core/helper/StringHelper.js';
import '../../Scheduler/feature/NonWorkingTime.js';

import '../localization/En.js';

// Always required features
import { TimeUnit } from '../../Engine/scheduling/Types.js';
import { CalculatedValueGen } from '../../ChronoGraph/chrono/Identifier.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import { BaseCalendarMixin } from '../../Engine/quark/model/scheduler_basic/BaseCalendarMixin.js';
import NumberFormat from '../../Core/helper/util/NumberFormat.js';
import TimelineHistogram from '../../Scheduler/view/TimelineHistogram.js';
import SchedulerStores from '../../Scheduler/view/mixin/SchedulerStores.js';
import SchedulerResourceRendering from '../../Scheduler/view/mixin/SchedulerResourceRendering.js';
import CrudManagerView from '../../Scheduler/crud/mixin/CrudManagerView.js';
import Objects from '../../Core/helper/util/Objects.js';
import ProjectModel from '../model/ProjectModel.js';

// Imitates ResourceAllocationInfo structure
function buildReturnedValue(total) {
    return {
        allocation : {
            total
        }
    };
}

/**
 * @module SchedulerPro/view/ResourceHistogram
 */

/**
 * This view displays a read-only timeline report of the workload for the resources in a
 * {@link SchedulerPro/model/ProjectModel project}. The resource allocation is visualized as bars along the time axis
 * with an optional line indicating the maximum available time for each resource. A {@link Scheduler/column/ScaleColumn}
 * is also added automatically.
 *
 * To create a standalone histogram, simply configure it with a Project instance:
 *
 * ```javascript
 * const project = new ProjectModel({
 *     autoLoad : true,
 *     loadUrl  : 'examples/schedulerpro/view/data.json'
 * });
 *
 * const histogram = new ResourceHistogram({
 *     project,
 *     appendTo    : 'targetDiv',
 *     rowHeight   : 60,
 *     minHeight   : '20em',
 *     flex        : '1 1 50%',
 *     showBarTip  : true,
 *     columns     : [
 *         {
 *             width : 200,
 *             field : 'name',
 *             text  : 'Resource'
 *         }
 *     ]
 * });
 * ```
 *
 * {@inlineexample SchedulerPro/view/ResourceHistogram.js}
 *
 * ## Pairing the component
 *
 * You can also pair the histogram with other timeline views such as the Gantt or Scheduler,
 * using the {@link Scheduler/view/TimelineBase#config-partner} config.
 *
 * You can configure (or hide completely) the built-in scale column easily:
 *
 * ```javascript
 * const histogram = new ResourceHistogram({
 *    project,
 *    appendTo    : 'targetDiv',
 *    columns     : [
 *        {
 *            width : 200,
 *            field : 'name',
 *            text  : 'Resource'
 *        },
 *        // Hide the scale column (or add any other column configs)
 *        {
 *            type   : 'scale',
 *            hidden : true
 *        }
 *    ]
 * });
 * ```
 *
 * ## Changing displayed values
 *
 * To change the histogram bar texts, supply a {@link #config-getBarText} function.
 * Here for example the provided function displays resources time **left** instead of
 * allocated time
 *
 * ```javascript
 * new ResourceHistogram({
 *     getBarText(datum) {
 *         const resourceHistogram = this.owner;
 *
 *         // get default bar text
 *         let result = resourceHistogram.getBarTextDefault(...arguments);
 *
 *         // and if some work is done in the tick
 *         if (result) {
 *
 *             const unit = resourceHistogram.getBarTextEffortUnit();
 *
 *             // display the resource available time
 *             result = resourceHistogram.getEffortText(datum.maxEffort - datum.effort, unit);
 *         }
 *
 *         return result;
 *     },
 *     ...
 * })
 * ```
 * @extends Scheduler/view/TimelineHistogram
 * @mixes Scheduler/view/mixin/SchedulerStores
 * @mixes Scheduler/view/mixin/SchedulerResourceRendering
 * @mixes Scheduler/crud/mixin/CrudManagerView
 * @classtype resourcehistogram
 * @widget
 */
export default class ResourceHistogram extends TimelineHistogram.mixin(SchedulerStores, SchedulerResourceRendering, CrudManagerView) {

    //region Config

    static $name = 'ResourceHistogram';

    static type = 'resourcehistogram';

    /**
     * @hideconfigs durationDisplayPrecision, resourceColumns, enableRecurringEvents, eventBarTextField,
     * eventBodyTemplate, eventColor, eventLayout, eventRenderer, eventRendererThisObj, eventStyle,
     * horizontalEventSorterFn, horizontalLayoutPackClass, horizontalLayoutStackClass, milestoneAlign,
     * milestoneTextPosition, highlightPredecessors, highlightSuccessors, removeUnassignedEvent,
     * eventAssignHighlightCls, eventCls, eventSelectedCls, fixedEventCls, overScheduledEventClass,
     * timeZone
     */

    static configurable = {
        projectModelClass : ProjectModel,

        sortFeatureStore      : 'store',
        timeAxisColumnCellCls : 'b-sch-timeaxis-cell b-resourcehistogram-cell',

        /**
         * Effort value format string.
         * Must be a template supported by {@link Core/helper/util/NumberFormat} class.
         * @config {String}
         * @default
         */
        effortFormat : '0.#',

        getRecordData : 'getRecordAllocationData',

        aggregateDataEntry : 'aggregateAllocationEntry',

        initAggregatedDataEntry : 'initAggregatedAllocationEntry',

        hardRefreshOnTimeAxisReconfigure : false,

        /**
         * Specifies whether effort values should display units or not.
         * @config {Boolean}
         * @default
         */
        showEffortUnit : true,

        useProjectTimeUnitsForScale : false,

        /**
         * Default time unit to display resources effort values.
         * The value is used as default when displaying effort in tooltips and bars text.
         * Yet the effective time unit used might change dynamically when zooming in the histogram
         * so its ticks unit gets smaller than the default unit.
         * Please use {@link #config-barTipEffortUnit} to customize default units for tooltips only
         * and {@link #config-barTextEffortUnit} to customize default units in bar texts.
         * @config {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'}
         * @default hour
         */
        effortUnit : TimeUnit.Hour,

        /**
         * Default time unit used for displaying resources effort in bars.
         * Yet the effective time unit used might change dynamically when zooming in the histogram
         * so its ticks unit gets smaller than the default unit.
         * Please use {@link #config-barTipEffortUnit} to customize default units for tooltips
         * (or {@link #config-effortUnit} to customize both texts and tooltips default units).
         * @config {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'}
         * @default hour
         */
        barTextEffortUnit : null,

        /**
         * Default time unit used when displaying resources effort in tooltips.
         * Yet the effective time unit used might change dynamically when zooming in the histogram
         * so its ticks unit gets smaller than the default unit.
         * Please use {@link #config-barTextEffortUnit} to customize default units for bar texts
         * (or {@link #config-effortUnit} to customize both texts and tooltips default units).
         * @config {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'}
         * @default hour
         */
        barTipEffortUnit : null,

        /**
         * Set to `true` if you want to display the maximum resource allocation line.
         * @config {Boolean}
         * @default
         */
        showMaxEffort : true,

        series : {
            maxEffort : {
                type  : 'outline',
                field : 'maxEffort'
            },
            effort : {
                type  : 'bar',
                field : 'effort'
            }
        },

        /**
         * A Function which returns the tooltip text to display when hovering a bar.
         * The following parameters are passed:
         * @param {Object} context The tooltip context info
         * @param {ResourceAllocationInterval} context.datum The histogram bar being hovered info
         * @param {Core.widget.Tooltip} context.tip The tooltip instance
         * @param {HTMLElement} context.element The Element for which the Tooltip is monitoring mouse movement
         * @param {HTMLElement} context.activeTarget The target element that triggered the show
         * @param {Event} context.event The raw DOM event
         * @param {SchedulerPro.model.ResourceModel} data.record The record which effort
         * the hovered bar displays.
         * @returns {String} Tooltip HTML content
         * @config {Function}
         */
        barTooltipTemplate({ datum }) {
            let result = '';

            const { inEventTimeSpan, isGroup } = datum;

            if (inEventTimeSpan) {
                if (isGroup) {
                    result = this.getGroupBarTip(...arguments);
                }
                else {
                    result = this.getResourceBarTip(...arguments);
                }
            }

            return result;
        },

        /**
         * Set to `true` if you want to display resources effort values in bars
         * (for example: `24h`, `7d`, `60min` etc.).
         * The text contents can be changed by providing {@link #config-getBarText} function.
         * @config {Boolean}
         */
        showBarText : false,

        /**
         * A Function which returns the text to render inside a bar.
         *
         * Here for example the provided function displays resources time **left** instead of
         * allocated time
         *
         * ```javascript
         * new ResourceHistogram({
         *     getBarText(datum) {
         *         const resourceHistogram = this.owner;
         *
         *         const { showBarText } = resourceHistogram;
         *
         *         let result = '';
         *
         *         // respect existing API - show bar texts only when "showBarText" is true
         *         // and if some work is done in the tick
         *         if (showBarText && datum.effort) {
         *
         *             const unit = resourceHistogram.getBarTextEffortUnit();
         *
         *             // display the resource available time
         *             result = resourceHistogram.getEffortText(datum.maxEffort - datum.effort, unit);
         *         }
         *
         *         return result;
         *     },
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
         * new ResourceHistogram({
         *     getBarText(datum) {
         *         // "this" in the method refers core Histogram instance
         *         // get the view instance
         *         const resourceHistogram = this.owner;
         *
         *         .....
         *     },
         * })
         * ```
         * The following parameters are passed:
         * @param {ResourceAllocationInterval} datum The datum being rendered
         * @param {Number} index The index of the datum being rendered
         * @returns {String} Text to render inside the bar
         * @config {Function} getBarText
         */

        groupBarTipAssignmentLimit : 5,

        /**
         * Set to `true` to include inactive tasks allocation and `false` to not take such tasks into account.
         * @config {Boolean}
         * @default
         */
        includeInactiveEvents : false,

        histogramWidget : {
            cls : 'b-hide-offscreen b-resourcehistogram-histogram'
        },

        fixedRowHeight : true
    };

    //endregion

    //region Constructor/Destructor

    get timeAxis() {
        return super.timeAxis;
    }

    set timeAxis(timeAxis) {
        const currentTimeAxis = this._timeAxis;

        super.timeAxis = timeAxis;

        if (this.partner && !timeAxis || (currentTimeAxis && currentTimeAxis === timeAxis)) {
            return;
        }

        this._timeAxis.ion({
            name           : 'timeAxis',
            endReconfigure : 'onTimeAxisEndReconfigure',
            thisObj        : this
        });
    }

    afterConfigure() {
        super.afterConfigure();

        const me = this;

        me.onRecordAllocationCalculated = me.onRecordAllocationCalculated.bind(me);
        me.onCommitAsyncCompletion = me.onCommitAsyncCompletion.bind(me);
    }

    onDestroy() {
        super.onDestroy(...arguments);

        // get graph ..if the project is not destroyed yet
        const graph = this.project.getGraph?.();

        if (graph && this.ticksIdentifier) {
            graph.removeIdentifier(this.ticksIdentifier);
        }
    }

    clearHistogramDataCache(record) {
        if (!record) {
            // remove cached histogram data (which is a set of graph entities) from the graph
            for (const [record, entity] of this.getHistogramDataCache()?.entries()) {
                record.removeEntity?.(entity);
            }
        }

        super.clearHistogramDataCache(...arguments);
    }

    //endregion

    //region Project

    bindProject(project) {
        this.detachListeners('resourceHistogramProject');

        project.ion({
            name                  : 'resourceHistogramProject',
            refresh               : 'internalOnProjectRefresh',
            delayCalculationStart : 'onProjectDelayCalculationStart',
            delayCalculationEnd   : 'onProjectDelayCalculationEnd',
            repopulateReplica     : 'onRepopulateReplica',

            thisObj : this
        });
    }

    updateProject(project) {
        this.bindProject(project);

        // project implements CrudManager API
        this.crudManager = project;

        this.store = project.resourceStore;
    }

    //endregion

    //region Internal

    getRowHeight() {
        return this.rowHeight;
    }

    convertEffortUnit(value, unit, toUnit) {
        return this.project.run('$convertDuration', value, unit, toUnit);
    }

    updateUseProjectTimeUnitsForScale() {
        const me = this;
        // Below this.scalePoints assignment of doesn't work until ResourceHistogram is painted
        // since ScaleWidget being constructed tries to read its rootElement which results:
        // "Floating Widgets must have "rootElement" to be ..."
        if (me.isPainted) {
            // we need to regenerate ScaleColumn points according to new unit values
            const eventParams = { scalePoints : me.generateScalePoints() };

            /**
             * Fires when the component generates points for the {@link #property-scaleColumn scale column}.
             *
             * Use a listeners to override the generated scale points:
             *
             * ```javascript
             * new ResourceHistogram({
             *     ...
             *     listeners : {
             *         generateScalePoints(params) {
             *             // provide text for each scale point (if not provided already)
             *             params.scalePoints.forEach(point => {
             *                 point.text = point.text || point.value;
             *             });
             *         }
             *     }
             * })
             * ```
             *
             * @param {SchedulerPro.view.ResourceHistogram} source The component instance
             * @param {ScalePoint[]} scalePoints Array of objects representing scale points. Each entry can have properties:
             * - `value` - point value
             * - `unit` - point value unit
             * - `text` - label text (if not provided the point will not have a label displayed)
             * @event generateScalePoints
             * @category Scale column
             */
            me.trigger('generateScalePoints', eventParams);

            // allow to override the points in a listener
            me._generatedScalePoints = me.scalePoints = eventParams.scalePoints;

            me.scheduleRefreshRows();
        }
    }

    updateShowBarText(value) {
        this.scheduleRefreshRows();
    }

    get eventStore() {
        return this.project?.eventStore;
    }

    set eventStore(eventStore) {
        super.eventStore = eventStore;
    }

    convertUnitsToHistogramValue(value, unit) {
        return this.useProjectTimeUnitsForScale
            ? this.convertEffortUnit(value, unit, TimeUnit.Millisecond)
            : DateHelper.asMilliseconds(value, unit);
    }

    convertHistogramValueToUnits(value, unit) {
        return this.useProjectTimeUnitsForScale
            ? this.convertEffortUnit(value, TimeUnit.Millisecond, unit)
            : DateHelper.as(unit, value);
    }

    buildScalePointText(scalePoint) {
        return `${scalePoint.value}${DateHelper.getShortNameOfUnit(scalePoint.unit)}`;
    }

    /**
     * Generates points for the {@link #property-scaleColumn scale column}.
     *
     * **Override the method to customize the scale column points.**
     *
     * @param {Number} [scaleMax] Maximum value for the scale. Uses current timeaxis increment if not provided.
     * @param {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} [unit] Time
     * unit `scaleMax` argument is expressed in.
     * Uses current timeaxis unit if not provided.
     * @returns {ScalePoint[]} Array of objects representing scale points. Each entry can have properties:
     * - `value` - point value
     * - `unit` - point value unit
     * - `text` - label text (if not provided the point will not have a label displayed)
     * @category Scale column
     */
    generateScalePoints(scaleMax, unit) {
        // bail out if there is no project or it's not in the graph
        if (!this.project?.graph) {
            return;
        }

        const
            { timeAxis } = this,
            scalePoints           = [];

        scaleMax = scaleMax || timeAxis.increment;
        unit = unit || timeAxis.unit;

        let scaleStep;

        // If the ticks are defined as 1 unit let's break it down to smaller units
        if (scaleMax === 1) {
            // getting timeaxis tick sub-unit and number of them in a tick
            unit     = DateHelper.getSmallerUnit(unit);
            scaleMax = Math.round(
                this.useProjectTimeUnitsForScale ? this.convertEffortUnit(scaleMax, timeAxis.unit, unit)
                    : DateHelper.as(unit, scaleMax, timeAxis.unit)
            );
        }

        // Let's try to guess how many points in the scale will work nicely
        for (const factor of [7, 5, 4, 3, 2]) {
            // unitsNumber is multiple of "factor" -> we generate "factor"-number of points
            if (!(scaleMax % factor)) {
                scaleStep = scaleMax / factor;
                break;
            }
        }

        // fallback to a single point equal to maximum value
        if (!scaleStep) {
            scaleStep = scaleMax;
        }

        for (let value = scaleStep; value <= scaleMax; value += scaleStep) {
            scalePoints.push({
                value
            });
        }

        const lastPoint = scalePoints[scalePoints.length - 1];
        // put unit and label to the last point
        lastPoint.unit = unit;
        lastPoint.text = this.buildScalePointText(lastPoint);

        return scalePoints;
    }

    updateViewPreset(viewPreset) {
        const me = this;

        // Set a flag indicating that we're inside of `updateViewPreset` so our `onTimeAxisEndReconfigure` will skip its call.
        // We call it here later.
        me._updatingViewPreset = true;
        super.updateViewPreset(...arguments);
        me._updatingViewPreset = false;

        // In `super.updateViewPreset` function `this.render` is called which checks if the engine is not dirty
        // ..and we modify `ticksIdentifier` atom in `onTimeAxisEndReconfigure`
        // so the engine state gets dirty and rendering gets delayed which ends up an exception.
        // So we call `onTimeAxisEndReconfigure` after super `updateViewPreset` code
        // to keep the engine non-dirty while zooming/setting a preset.
        // This scenario is covered w/ SchedulerPro/tests/pro/view/ResourceHistogramZoom.t.js
        if (me.project.isInitialCommitPerformed && me.isPainted) {
            me.onTimeAxisEndReconfigure();
        }
    }

    onRepopulateReplica() {
        this.ticksIdentifier = null;
        this.clearHistogramDataCache();
    }

    buildTicksIdentifier() {
        const
            me    = this,
            graph = me.project.getGraph();

        if (!me.ticksIdentifier) {
            me.ticksIdentifier = graph.addIdentifier(CalculatedValueGen.new());
        }
        else {
            const prevTicksCalendar = graph.read(me.ticksIdentifier);

            me.project.clearCombinationsWith(prevTicksCalendar);
        }

        me.ticksIdentifier.writeToGraph(graph, new BaseCalendarMixin({
            unspecifiedTimeIsWorking : false,
            intervals                : me.timeAxis.ticks.map(tick => {
                return {
                    startDate : tick.startDate,
                    endDate   : tick.endDate,
                    isWorking : true
                };
            })
        }));

        // process ticks to detect if their widths are monotonous
        // or some tick has a different width value
        me.collectTicksWidth();

        return me.ticksIdentifier;
    }

    onProjectDelayCalculationStart() {
        this.suspendRefresh();
    }

    onProjectDelayCalculationEnd() {
        this.resumeRefresh();
    }

    projectUnitsHasChanged() {
        const { project } = this;

        return project.daysPerMonth !== this._projectDaysPerMonth ||
            project.daysPerWeek !== this._projectDaysPerWeek ||
            project.hoursPerDay !== this._projectHoursPerDay;
    }

    internalOnProjectRefresh({ source, isInitialCommit, isCalculated }) {
        if (isCalculated) {
            const me = this;

            if (!me.ticksIdentifier) {
                me.onTimeAxisEndReconfigure();
            }

            // if project units has changed and we use them for scale points
            if (me.useProjectTimeUnitsForScale && me.projectUnitsHasChanged()) {
                me._projectDaysPerMonth = source.daysPerMonth;
                me._projectDaysPerWeek = source.daysPerWeek;
                me._projectHoursPerDay = source.hoursPerDay;

                // regenerate scale points
                const eventParams = { scalePoints : me.generateScalePoints() };

                me.trigger('generateScalePoints', eventParams);

                // allow to override the points in a listener
                me._generatedScalePoints = me.scalePoints = eventParams.scalePoints;
            }

            // If rowManager got no topRow yet - reinitialize it otherwise refresh does n0thing
            if (!me.rowManager.topRow) {
                me.rowManager.reinitialize();
            }
            // enable view refreshing back (trigger refresh if that's an initial commit)
            else {
                me.resumeRefresh(isInitialCommit);
            }
        }
    }

    relayStoreDataChange(event) {
        super.relayStoreDataChange(event);

        if (this.store.count === 0) {
            // To clear histogram when no rows to refresh
            this.histogramWidget.data = [];
            this.histogramWidget.refresh();
        }
    }

    changeHistogramWidget(widget, oldWidget) {
        const me = this;

        if (!oldWidget) {
            const { series } = me;

            if (!me.showMaxEffort && series.maxEffort) {
                series.maxEffort = false;
            }

            widget = super.changeHistogramWidget(...arguments);
        }

        return widget;
    }

    getRectConfig(rectConfig, datum, index, series) {
        if (datum.inEventTimeSpan) {
            const
                { topValue } = this,
                value = datum[series.field],
                forceHeight = series.stretch ? 1 : datum.height;

            rectConfig.height = forceHeight || (value > topValue ? topValue : value) / topValue;
            rectConfig.y = 1 - rectConfig.height;

            return rectConfig;
        }
    }

    // Injectable method.
    getRectClassDefault(series, rectConfig, datum) {
        if (series.id === 'effort') {
            switch (true) {
                case datum.isOverallocated :
                    return 'b-overallocated';

                case datum.isUnderallocated :
                    return 'b-underallocated';
            }
        }

        return '';
    }

    get effortFormatter() {
        const
            me     = this,
            format = me.effortFormat;

        let formatter = me._effortFormatter;

        if (!formatter || me._effortFormat !== format) {
            formatter = NumberFormat.get(me._lastFormat = format);

            me._effortFormatter = formatter;
        }

        return formatter;
    }

    /**
     * Formats effort value to display in the component bars and tooltips.
     * @param {Number} effort Effort value
     * @param {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} unit Effort value unit
     * @param {Boolean} [showEffortUnit=this.showEffortUnit] Provide `true` to include effort unit. If not provided
     * uses {@link #config-showEffortUnit} value.
     * @returns {String} Formatted effort value.
     */
    getEffortText(effort, unit, showEffortUnit = this.showEffortUnit) {
        // bail out if there is no project or it's not in the graph
        if (!this.project?.graph) {
            return;
        }

        const { scaleUnit, effortFormatter } = this;

        unit = unit || scaleUnit;

        const
            localizedUnit = DateHelper.getShortNameOfUnit(unit),
            effortInUnits = this.convertHistogramValueToUnits(effort, unit);

        return effortFormatter.format(effortInUnits) + (showEffortUnit ? localizedUnit : '');
    }

    getBarTipEffortUnit() {
        const
            { effortUnit, barTipEffortUnit, timeAxis } = this,
            defaultUnit                                = barTipEffortUnit || effortUnit;

        return DateHelper.compareUnits(timeAxis.unit, defaultUnit) < 0 ? timeAxis.unit : defaultUnit;
    }

    getGroupBarTip({ datum }) {
        const
            me           = this,
            { timeAxis } = me;

        let result = '';

        if (datum.inEventTimeSpan) {
            const
                unit          = me.getBarTipEffortUnit(...arguments),
                allocated     = me.getEffortText(datum.effort, unit),
                available     = me.getEffortText(datum.maxEffort, unit),
                assignmentTpl = me.L('L{groupBarTipAssignment}');

            let
                dateFormat        = 'L',
                resultFormat      = me.L('L{groupBarTipInRange}'),
                assignmentsSuffix = '';

            if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Day) === 0) {
                resultFormat = me.L('L{groupBarTipOnDate}');
            }
            else if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Second) <= 0) {
                dateFormat = 'HH:mm:ss A';
            }
            else if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Hour) <= 0) {
                dateFormat = 'LT';
            }

            let assignmentsArray = [...datum.resourceAllocation.entries()]
                .filter(([resource, data]) => data.effort)
                .sort(([key1, value1], [key2, value2]) => value1.effort > value2.effort ? -1 : 1);

            if (assignmentsArray.length > me.groupBarTipAssignmentLimit) {
                assignmentsSuffix = '<br>' + me.L('L{plusMore}').replace('{value}', assignmentsArray.length - me.groupBarTipAssignmentLimit);
                assignmentsArray = assignmentsArray.slice(0, this.groupBarTipAssignmentLimit);
            }

            const assignments = assignmentsArray.map(([resource, info]) => {

                return assignmentTpl.replace('{resource}', StringHelper.encodeHtml(resource.name))
                    .replace('{allocated}', me.getEffortText(info.effort, unit))
                    .replace('{available}', me.getEffortText(info.maxEffort, unit))
                    .replace('{cls}', info.isOverallocated ? 'b-overallocated' : info.isUnderallocated ? 'b-underallocated' : '');

            }).join('<br>') + assignmentsSuffix;


            result = resultFormat
                .replace('{assignments}', assignments)
                .replace('{startDate}', DateHelper.format(datum.tick.startDate, dateFormat))
                .replace('{endDate}', DateHelper.format(datum.tick.endDate, dateFormat))
                .replace('{allocated}', allocated)
                .replace('{available}', available)
                .replace('{cls}', datum.isOverallocated ? 'b-overallocated' : datum.isUnderallocated ? 'b-underallocated' : '');

            result = `<div class="b-histogram-bar-tooltip">${result}</div>`;
        }

        return result;
    }

    getResourceBarTip({ datum }) {
        const
            me           = this,
            { timeAxis } = me;

        let result = '';

        if (datum.inEventTimeSpan) {
            const
                unit       = me.getBarTipEffortUnit(),
                allocated  = me.getEffortText(datum.effort, unit),
                available  = me.getEffortText(datum.maxEffort, unit);

            let
                dateFormat   = 'L',
                resultFormat = me.L('L{barTipInRange}');

            if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Day) === 0) {
                resultFormat = me.L('L{barTipOnDate}');
            }
            else if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Second) <= 0) {
                dateFormat = 'HH:mm:ss A';
            }
            else if (DateHelper.compareUnits(timeAxis.unit, TimeUnit.Hour) <= 0) {
                dateFormat = 'LT';
            }


            result = resultFormat
                .replace('{startDate}', DateHelper.format(datum.tick.startDate, dateFormat))
                .replace('{endDate}', DateHelper.format(datum.tick.endDate, dateFormat))
                .replace('{allocated}', allocated)
                .replace('{available}', available)
                .replace('{cls}', datum.isOverallocated ? 'b-overallocated' : datum.isUnderallocated ? 'b-underallocated' : '');

            if (datum.resource) {
                result = result
                    .replace('{resource}', StringHelper.encodeHtml(datum.resource.name));
            }

            result = `<div class="b-histogram-bar-tooltip">${result}</div>`;
        }

        return result;
    }

    /**
     * Returns unit to display effort values in when rendering the histogram bars.
     * The method by default returns {@link #config-barTextEffortUnit} value if provided
     * and if not falls back to {@link #config-effortUnit} value.
     * But it also takes zooming into account and when
     * the timeaxis ticks unit gets smaller than the default value the ticks unit is returned.
     *
     * @returns {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} Time unit to display
     * effort values in.
     */
    getBarTextEffortUnit() {
        const
            { effortUnit, barTextEffortUnit, timeAxis } = this,
            defaultUnit                                 = barTextEffortUnit || effortUnit;

        return DateHelper.compareUnits(timeAxis.unit, defaultUnit) < 0 ? timeAxis.unit : defaultUnit;
    }

    /**
     * The default method that returns the text to render inside a bar if no
     * {@link #config-getBarText} function was provided.
     *
     * The method can be used in a {@link #config-getBarText} function
     * to invoke the default implementation:
     *
     * ```javascript
     * new ResourceHistogram({
     *     getBarText(datum) {
     *         const resourceHistogram = this.owner;
     *
     *         // get default bar text
     *         let result = resourceHistogram.getBarTextDefault();
     *
     *         // if the resource is overallocated in that tick display "Overallocated! " string
     *         // before the allocation value
     *         if (result && datum.maxEffort < datum.effort) {
     *             result = 'Overallocated! ' + result;
     *         }
     *
     *         return result;
     *     },
     * })
     * ```
     * The following parameters are passed:
     * @param {ResourceAllocationInterval} datum The data of the bar being rendered
     * @param {Number} index The index of the datum being rendered
     * @param {String} series Identifier of the series (provided only if the histogram widget
     * {@link Core/widget/graph/Histogram#config-singleTextForAllBars} is `false`)
     * @param {HistogramRenderData} renderData Current render data giving access to the record, row and cell
     * being rendered.
     * @returns {String} Text to render inside the bar
     */
    getBarTextDefault(datum, index) {
        const
            view            = this.owner,
            { showBarText } = view;

        let result = '';

        if (showBarText && datum.inEventTimeSpan) {
            const unit = view.getBarTextEffortUnit();
            result     = view.getEffortText(datum.effort, unit);
        }

        return result;
    }

    updateShowMaxEffort(value) {
        const me = this;

        me._showMaxEffort = value;

        const widget = me._histogramWidget;

        // bail out in case there is no widget constructed yet
        if (!widget) {
            return;
        }

        const { series } = me;

        if (!value) {
            if (series.maxEffort) {
                me._seriesMaxEffort = series.maxEffort;
                series.maxEffort = false;
            }
        }
        else if (typeof value === 'object') {
            series.maxEffort = value;
        }
        else if (typeof me._seriesMaxEffort === 'object') {
            series.maxEffort = me._seriesMaxEffort;
        }
        else {
            series.maxEffort = {
                id    : 'maxEffort', 
                type  : 'outline',
                field : 'maxEffort'
            };
        }

        me.scheduleRefreshRows();
    }

    updateIncludeInactiveEvents(value) {
        // update collected reports wih new includeInactiveEvents flag state
        this.getHistogramDataCache()?.forEach(allocationReport => allocationReport.includeInactiveEvents = value);
    }

    //endregion

    //region Events

    onTimeAxisEndReconfigureInternal() {
        const me = this;

        // Skip call triggered by viewPreset setting we have `updateViewPreset` method overridden where we call `onTimeAxisEndReconfigure` later
        if (!me._updatingViewPreset) {
            const { unit, increment } = me.timeAxis;

            // re-generate scale point on zooming in/out
            if (unit !== me._lastTimeAxisUnit || increment !== me._lastTimeAxisIncrement) {
                // remember last used unit & increment to distinguish zooming from timespan changes
                me._lastTimeAxisUnit = unit;
                me._lastTimeAxisIncrement = increment;

                // regenerate scale points
                const
                    scalePoints = me.generateScalePoints(),
                    eventParams = { scalePoints };

                // allow to override the points in a listener
                me.trigger('generateScalePoints', eventParams);

                me._generatedScalePoints = me.scalePoints = eventParams.scalePoints;
            }


            me.buildTicksIdentifier();
        }
    }

    calculateRowHeights() {}

    onTimeAxisEndReconfigure() {
        const me = this;

        // Skip call triggered by viewPreset setting we have `updateViewPreset` method overridden where we call `onTimeAxisEndReconfigure` later
        if (!me._updatingViewPreset) {
            if (me.project.graph) {
                me.onTimeAxisEndReconfigureInternal();
            }
            // In delayed calculation mode (the default) we might not be in graph yet, postpone buildTicksIdentifier until we are
            else {
                me.project.ion({
                    graphReady() {
                        me.onTimeAxisEndReconfigureInternal();
                    },
                    thisObj : me,
                    once    : true
                });
            }
        }
    }

    //endregion

    //region Render

    extractHistogramDataArray(allocationReport, record) {
        return allocationReport.allocation.total;
    }

    renderRows() {
        const me = this;

        if (!me.ticksIdentifier && me.project.isInitialCommitPerformed) {
            // If we render rows but have no ticksIdentifier means data loading and 1st commit
            // happened before the histogram was created.
            // Handle timeaxis settings to build ticksIdentifier and scale column points.
            me.onTimeAxisEndReconfigure();

            // If timeView range is not defined then the timeaxis header looks empty so fill it in here (it triggers the column refresh)
            if (!me.timeView.startDate || !me.timeView.endDate) {
                me.timeView.range = {
                    startDate : me.startDate,
                    endDate   : me.endDate
                };
            }
        }

        return super.renderRows(...arguments);
    }

    async shiftPrevious() {
        super.shiftPrevious(...arguments);

        await this.project.commitAsync();
    }

    async shiftNext() {
        super.shiftNext(...arguments);

        await this.project.commitAsync();
    }

    onCommitAsyncCompletion() {
        // trigger rendering after the Engine finishes a transaction
        this.renderScheduledRecords();
        this._renderOnCommitPromise = null;
    }

    onRecordAllocationCalculated(allocation) {
        if (!this.isDestroying) {
            // update cache to trigger histogramDataCacheSet event
            this.setHistogramDataCache(allocation.resource, allocation.owner);
        }
    }

    buildResourceAllocationReport(resource) {
        return this.project.resourceAllocationInfoClass.new({
            includeInactiveEvents : this.includeInactiveEvents,
            ticks                 : this.ticksIdentifier,
            resource
        });
    }

    /**
     * Returns the provided record's allocation data.
     * The process of allocation collecting is asynchronous so the method returns a `Promise`
     * that provides the data once resolved.
     *
     * The method used as the default value of {@link #config-getRecordData} config.
     * @param {SchedulerPro.model.ResourceModel} record Resource record to collect allocation for.
     * @returns {Promise} A `Promise` that provides the provided resource
     * {@link SchedulerPro/model/ResourceModel#typedef-ResourceAllocationInfo allocation info} when resolved.
     */
    async getRecordAllocationData(record) {
        const
            me = this,
            { project } = me;

        // No drawing before engine's initial commit
        while (!me.ticksIdentifier || !project.isInitialCommitPerformed) {
            await project.await('commitFinalized');
        }

        // Ignore resources not in the graph
        if (record.graph) {
            const
                graph            = project.getGraph(),
                allocationReport = me.buildResourceAllocationReport(record);

            record.addEntity(allocationReport);

            await graph.readAsync(allocationReport.$.allocation);

            // after this transaction finishes we will
            // track further allocation report changes with onRecordAllocationCalculated method
            graph.ongoing.then(() => {
                graph.addListener(allocationReport.$.allocation, me.onRecordAllocationCalculated);
            });

            return allocationReport;
        }
    }

    onHistogramDataCacheSet() {
        super.onHistogramDataCacheSet(...arguments);

        const me = this;

        // trigger rendering right after the Engine finishes its current commitAsync() call
        if (!me._renderOnCommitPromise) {
            me._renderOnCommitPromise = me.project.graph.ongoing.then(me.onCommitAsyncCompletion);
        }
    }

    aggregateRecordsHistogramData(records, aggregationContext) {
        const result = super.aggregateRecordsHistogramData(records, aggregationContext);

        if (Objects.isPromise(result)) {
            return result.then(buildReturnedValue);
        }

        return buildReturnedValue(result);
    }

    /**
     * The default function that initializes a target group record entry.
     *
     * The method is used as {@link #config-initAggregatedDataEntry} default value.
     * @returns {ResourceAllocationInterval} Returns an empty allocation entry.
     * @category Parent histogram data collecting
     */
    initAggregatedAllocationEntry() {
        return {
            tick               : null,
            effort             : 0,
            maxEffort          : 0,
            units              : 0,
            isGroup            : true,
            inEventTimeSpan    : false,
            resourceAllocation : new Map()
        };
    }

    /**
     * The default function used for aggregating a child record histogram data values to its parent entry.
     * The function sums up `effort` and `maxEffort` series values. It also propagates
     * {@link SchedulerPro/model/ResourceModel#typedef-ResourceAllocationInterval isOverallocated} and
     * {@link SchedulerPro/model/ResourceModel#typedef-ResourceAllocationInterval isUnderallocated} values so if there
     * is a child having the corresponding value as `true` it will be `true` on the parent level as well.
     *
     * All children {@link SchedulerPro/model/ResourceModel#typedef-ResourceAllocationInterval assignments} are united
     * on the parent level {@link SchedulerPro/model/ResourceModel#typedef-ResourceAllocationInterval assignments}
     * property.
     *
     * The method is used as {@link #config-aggregateDataEntry} default value.
     *
     * @param {ResourceAllocationInterval} aggregated Target parent data entry to aggregate the entry into.
     * @param {ResourceAllocationInterval} entry Current entry to aggregate into `aggregated`.
     * @param {Number} arrayIndex Index of the current record (among other
     * records being aggregated).
     * @param {Number} colIndex `entry` index in the current array
     * @returns {ResourceAllocationInterval} Resulting parent data entry.
     * @category Parent histogram data collecting
     */
    aggregateAllocationEntry(acc, entry, _recordIndex, _entryIndex, aggregationContext) {
        acc.resourceAllocation.set(entry.resource, entry);

        acc.tick             = entry.tick;
        acc.isOverallocated  = acc.isOverallocated || entry.isOverallocated;
        acc.isUnderallocated = acc.isUnderallocated || entry.isUnderallocated;
        acc.inEventTimeSpan  = acc.inEventTimeSpan || entry.inEventTimeSpan;

        if (entry.assignments) {
            if (acc.assignments) {
                entry.assignments.forEach(assignment => acc.assignments.add(assignment));
            }
            else {
                acc.assignments = new Set(entry.assignments);
            }
        }

        return acc;
    }

    generateGroupScalePoints(record) {
        const
            me          = this,
            children    = me.getGroupChildren(record),
            scalePoints = me.generateScalePoints(me.timeAxis.increment * children.length),
            eventParams = { scalePoints, groupParent : record };

        me.trigger('generateScalePoints', eventParams);

        return eventParams.scalePoints;
    }

    processRecordRenderData(renderData) {
        // Override histogram topValue and scalePoints for group records
        if (this.isGroupRecord(renderData.record)) {
            const
                scalePoints = this.generateGroupScalePoints(renderData.record),
                topValue    = this.getTopValueByScalePoints(scalePoints);

            renderData.scaleWidgetConfig = { scalePoints };
            renderData.histogramConfig = { ...renderData.histogramConfig, topValue };
        }
        else {
            renderData = super.processRecordRenderData(renderData);
        }

        return renderData;
    }

    //endregion

    //region Localization

    updateLocalization() {
        const me = this;

        // Translate scale points if we have them (update localization on construction step is called too early)
        // and the scale points is generated by the histogram which means their labels use localized unit abbreviations
        if (me._generatedScalePoints === me.scalePoints && me.scalePoints) {
            me.scalePoints.forEach(scalePoint => {
                // if the point is labeled let's rebuild its text using new locale
                if (scalePoint.text && scalePoint.unit) {
                    scalePoint.text = me.buildScalePointText(scalePoint);
                }
            });
        }

        super.updateLocalization(...arguments);
    }

    //endregion

}

ResourceHistogram.initClass();
