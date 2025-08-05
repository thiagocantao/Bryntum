import ArrayHelper from '../../../Core/helper/ArrayHelper.js';
import Objects from '../../../Core/helper/util/Objects.js';
import TimelineHistogramBase from '../TimelineHistogramBase.js';

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
export default Target => class TimelineHistogramGrouping extends (Target || TimelineHistogramBase) {

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
        aggregateHistogramDataForGroups : true,

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
        aggregateDataEntry : null,

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
        getDataEntryForAggregating : null,

        /**
         * A function that initializes a target group record entry.
         *
         * Should be provided as a function, or name of a function in the ownership hierarchy which may be called.
         * @config {Function|String} initAggregatedDataEntry
         * @returns {Object} Target aggregated entry
         * @category Parent histogram data collecting
         * @default
         */
        initAggregatedDataEntry : null,

        aggregateFunctions : {
            sum : {
                aliases : ['add'],
                entry(seriesId, acc, entry) {
                    acc[seriesId] = (acc[seriesId] || 0) + entry[seriesId];

                    return acc;
                }
            },
            min : {
                entry(seriesId, acc, entry) {
                    const entryValue = entry[seriesId];

                    if (entryValue < (acc[seriesId] || Number.MAX_VALUE)) acc[seriesId] = entryValue;

                    return acc;
                }
            },
            max : {
                entry(seriesId, acc, entry) {
                    const entryValue = entry[seriesId];

                    if (entryValue > (acc[seriesId] || Number.MIN_VALUE)) acc[seriesId] = entryValue;

                    return acc;
                }
            },
            count : {
                init(seriesId, entry, entryIndex, aggregationContext) {
                    entry[seriesId] = aggregationContext.arrays.length;
                }
            },
            avg : {
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
                beforeDataLoad : me.onTreeGroupBeforeDataLoad,
                thisObj        : me
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
                name    : 'store',
                // reset groups cache on store grouping change
                group   : this.onStoreGroup,
                thisObj : this
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

    onHistogramDataCacheSet({ record, data }) {
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
        }
        else {
            result = super.getRecordHistogramData(...arguments);
        }

        return result;
    }

    //region ArrayHelper.aggregate default callbacks

    internalAggregateDataEntry(acc, ...args) {
        const { aggregateFunctions } = this;

        // call series aggregate functions
        for (const { id, aggregate = 'sum' } of Object.values(this.series)) {
            let fn;
            if (aggregate !== false && ((fn = aggregateFunctions[aggregate].entry))) {
                acc = fn(id, acc, ...args);
            }
        }

        return this.aggregateDataEntry ? this.aggregateDataEntry(acc, ...args) : acc;
    }

    internalInitAggregatedDataEntry() {
        const
            entry = this.initAggregatedDataEntry ? this.initAggregatedDataEntry(...arguments) : {},
            { aggregateFunctions } = this;

        // call series aggregate functions
        for (const { id, aggregate = 'sum' } of Object.values(this.series)) {
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
        const { store } = this;

        for (const record of this.getHistogramDataCache().keys()) {
            // clear cache for generated parents and links no longer in the store
            if (record.isGroupHeader || record.generatedParent || (record.isLinked && !store.includes(record))) {
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
            const { store } = this;

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
        const
            me = this,
            recordsData = [],
            { parentRecord } = aggregationContext;

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
        return record.isGroupHeader || (this.isTreeGrouped && record.generatedParent);
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

        return instanceMeta?.groupParent || (this.isTreeGrouped && record.parent);
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

        while ((groupParent = me.getRecordParent(record))) {
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
        const
            me = this,
            { aggregateFunctions } = me;

        aggregationContext.recordsData = recordsData;
        aggregationContext.records     = records;

        const arrays = recordsData.map((histogramData, index) => {
            return me.extractHistogramDataArray(
                histogramData,
                records[index]
            );
        });

        // summarize children histogram data
        const result = ArrayHelper.aggregate(
            arrays,
            me.getDataEntryForAggregating || (entry => entry),
            me.internalAggregateDataEntry,
            me.internalInitAggregatedDataEntry,
            aggregationContext
        );

        // call series aggregate functions
        for (const { id, aggregate = 'sum' } of Object.values(me.series)) {
            const fn = aggregateFunctions[aggregate].finalize;
            if (fn && aggregate !== false) {
                fn(id, result, ...arguments);
            }
        }

        return result;
    }

    get widgetClass() {}

};
