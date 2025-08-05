import '../../Grid/column/TreeColumn.js';
import '../../Grid/feature/Tree.js';
import '../column/ScaleColumn.js';
import '../column/TimeAxisColumn.js';
import TimelineHistogramGrouping from './mixin/TimelineHistogramGrouping.js';
import '../feature/ColumnLines.js';
import '../feature/ScheduleTooltip.js';
import TimelineHistogramBase from './TimelineHistogramBase.js';
import TimelineHistogramScaleColumn from './mixin/TimelineHistogramScaleColumn.js';

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
export default class TimelineHistogram extends TimelineHistogramBase.mixin(
    TimelineHistogramGrouping,
    TimelineHistogramScaleColumn
) {

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
