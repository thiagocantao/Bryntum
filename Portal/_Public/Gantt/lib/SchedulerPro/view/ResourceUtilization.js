import StringHelper from '../../Core/helper/StringHelper.js';
import ResourceHistogram from './ResourceHistogram.js';
import ResourceUtilizationStore from '../data/ResourceUtilizationStore.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import { TimeUnit } from '../../Engine/scheduling/Types.js';
import '../../Grid/column/TreeColumn.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import Tree from '../../Grid/feature/Tree.js';

/**
 * @module SchedulerPro/view/ResourceUtilization
 */

/**
 * View showing the utilization levels of the project resources.
 * The resources are displayed in a summary list where each row can
 * be expanded to show the events assigned for the resource.
 *
 * This demo shows the Resource utilization widget:
 * {@inlineexample SchedulerPro/view/ResourceUtilization.js}
 *
 * The view requires a {@link #config-project Project instance} to be provided:
 *
 * ```javascript
 * const project = new ProjectModel({
 *     autoLoad  : true,
 *     transport : {
 *         load : {
 *             url : 'examples/schedulerpro/view/data.json'
 *         }
 *     }
 * });
 *
 * const resourceUtilization = new ResourceUtilization({
 *     project,
 *     appendTo    : 'targetDiv',
 *     rowHeight   : 60,
 *     minHeight   : '20em',
 *     flex        : '1 1 50%',
 *     showBarTip  : true
 * });
 * ```
 *
 * ## Pairing the component
 *
 * You can also pair the view with other timeline views such as the Gantt or Scheduler,
 * using the {@link #config-partner} config.
 *
  * ## Changing displayed values
 *
 * To change the displayed bar texts, supply a {@link #config-getBarText} function.
 * Here for example the provided function displays resources time **left** instead of
 * allocated time
 *
 * ```javascript
 * new ResourceUtilization({
 *     getBarText(datum) {
 *         const view = this.owner;
 *
 *         // get default bar text
 *         let result = view.getBarTextDefault(...arguments);
 *
 *         // For resource records we will display the time left for allocation
 *         if (result && datum.resource) {
 *
 *             const unit = view.getBarTextEffortUnit();
 *
 *             // display the resource available time
 *             result = view.getEffortText(datum.maxEffort - datum.effort, unit);
 *         }
 *
 *         return result;
 *     },
 * })
 * ```
 *
 * @extends SchedulerPro/view/ResourceHistogram
 * @classtype resourceutilization
 * @widget
 */

export default class ResourceUtilization extends ResourceHistogram {

    //region Config

    static $name = 'ResourceUtilization';

    static type = 'resourceutilization';

    static configurable = {
        /**
         * @hideconfigs crudManager, crudManagerClass, assignments, resources, events, dependencies, assignmentStore,
         * resourceStore, eventStore, dependencyStore, data, timeZone
         */

        scaleColumn : null,

        /**
         * A Function which returns the text to render inside a bar.
         *
         * Here for example the provided function displays resources time **left** instead of
         * allocated time
         *
         * ```javascript
         * new ResourceUtilization({
         *     getBarText(datum) {
         *         const resourceUtilization = this.owner;
         *
         *         // get default bar text
         *         let result = view.getBarTextDefault();
         *
         *         // For resource records we will display the time left for allocation
         *         if (result && datum.resource) {
         *
         *             const unit = resourceUtilization.getBarTextEffortUnit();
         *
         *             // display the resource available time
         *             result = resourceUtilization.getEffortText(datum.maxEffort - datum.effort, unit);
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
         * So `this` in the function will refer to the {@link Core/widget/graph/Histogram} instance.
         * To access the `ResourceUtilization` instance please use `this.owner` in the function body:
         *
         * ```javascript
         * new ResourceUtilization({
         *     getBarText(datum) {
         *         // "this" in the method refers core Histogram instance
         *         // get the view instance
         *         const view = this.owner;
         *
         *         .....
         *     },
         * })
         * ```
         * The following parameters are passed:
         * @param {ResourceAllocationInterval|AssignmentAllocationInterval} datum The datum being rendered.
         * Either {@link SchedulerPro.model.ResourceModel#typedef-ResourceAllocationInterval} object for resource records (root level records)
         * or {@link SchedulerPro.model.ResourceModel#typedef-AssignmentAllocationInterval} object for assignment records
         * @param {Number} index The index of the datum being rendered
         * @returns {String} Text to render inside the bar
         * @config {Function} getBarText
         */

        /* */

        timeAxisColumnCellCls : 'b-sch-timeaxis-cell b-resourceutilization-cell',

        /**
         * A ProjectModel instance (or a config object) to display resource allocation of.
         *
         * Note: This config is mandatory.
         * @config {ProjectModelConfig|SchedulerPro.model.ProjectModel} project
         */

        rowHeight : 30,

        showEffortUnit : false,

        /**
         * @config {Boolean} showMaxEffort
         * @hide
         */

        showMaxEffort : false,

        /**
         * Set to `true` if you want to display resources effort values in bars
         * (for example: `24h`, `7d`, `60min` etc.).
         * The text contents can be changed by providing {@link #config-getBarText} function.
         * @config {Boolean}
         * @default
         */
        showBarText : true,

        /**
         * A Function which returns the tooltip text to display when hovering a bar.
         * The following parameters are passed:
         * @param {Object} data The backing data of the histogram rectangle
         * @param {Object} data.rectConfig The rectangle configuration object
         * @param {Object} data.datum The hovered bar data
         * @param {Number} data.index The index of the hovered bar data
         * @param {SchedulerPro.model.ResourceUtilizationModel} data.record The record which effort
         * the hovered bar displays.
         * @returns {String} Tooltip HTML content
         * @config {Function}
         */
        barTooltipTemplate({ datum }) {
            let result = '';

            const { inEventTimeSpan, isGroup, resource, assignment } = datum;

            // const barTip = this.callback('getBarTextTip', me, [renderData, data[index], index]);
            if (inEventTimeSpan) {
                if (isGroup) {
                    result = this.getGroupBarTip(...arguments);
                }
                else if (assignment) {
                    result = this.getAssignmentBarTip(...arguments);
                }
                else if (resource) {
                    result = this.getResourceBarTip(...arguments);
                }
            }

            return result;
        },

        series : {
            effort : {
                // We don't want the histogram bar heights based on effort
                // so set "stretch" here to make bars to take the whole row height
                stretch : true
            }
        },

        readOnly : true,

        columns : [
            {
                type        : 'tree',
                field       : 'name',
                text        : 'L{nameColumnText}',
                localeClass : this
            }
        ],

        histogramWidget : {
            cls : 'b-hide-offscreen b-resourceutilization-histogram'
        }
    };

    //endregion

    /**
     * @event generateScalePoints
     * @hide
     */

    /**
     * @function generateScalePoints
     * @hide
     */

    /**
     * @member {Scheduler.column.ScaleColumn} scaleColumn
     * @hide
     */

    construct() {
        super.construct(...arguments);

        this.rowManager.ion({
            renderRow : 'onRowManagerRenderRow',
            thisObj   : this
        });
    }

    updateProject(project) {
        const store = this.store;

        super.updateProject(project);

        // Super call sets this.store to resource store
        // 1) revert if a store was provided explicitly
        // 2) otherwise generating ResourceUtilizationStore
        this.store = store || this.buildStore(project);
    }

    updateResourceStore(resourceStore) {
        this._resourceStore = resourceStore;
    }

    buildStore(project) {
        project = this.project;
        return ResourceUtilizationStore.new({ project });
    }

    //region Render

    async getTipHtml(tooltipContext) {
        const
            index          = tooltipContext.activeTarget.dataset.index,
            record         = this.getRecordFromElement(tooltipContext.activeTarget),
            allocationData = await this.getRecordHistogramData(record),
            datum          = this.extractHistogramDataArray(allocationData, record)[parseInt(index, 10)];

        return this.barTooltipTemplate({ ...tooltipContext, record, index, datum });
    }

    getRecordAllocationData(record, ...args) {
        record = this.resolveRecordToOrigin(record);

        if (record.isResourceModel) {
            return super.getRecordAllocationData(record, ...args);
        }
        else if (record.isAssignmentModel) {
            // if that's an assignment re-invoke this.getRecordHistogramData() for resource
            return this.getRecordHistogramData(record.resource, ...args);
        }
    }

    onDestroy() {
        if (this.destroyStores) {
            this.store?.destroy();
        }

        super.onDestroy();
    }

    onRecordAllocationCalculated(allocation) {
        const me = this;

        if (!me.isDestroying) {
            const
                allocationReport  = allocation.owner,
                assignmentRecords = [...allocation.byAssignments.keys()]
                    .reduce((acc, assignment) => {
                        const record = me.resolveOriginToRecord(assignment);
                        if (record) {
                            acc.push(record);
                        }
                        return acc;
                    }, []),
                resourceRecord = me.resolveOriginToRecord(allocation.resource);

            if (resourceRecord) {
                me.setHistogramDataCache(resourceRecord, allocationReport);
            }

            for (const assignmentRecord of assignmentRecords) {
                me.setHistogramDataCache(assignmentRecord, allocationReport);
            }

            // trigger rendering right after the Engine finishes its current commitAsync() call
            if (!me._renderOnCommitPromise) {
                me._renderOnCommitPromise = me.project.graph.ongoing.then(me.onCommitAsyncCompletion.bind(me));
            }
        }
    }

    onRowManagerRenderRow({ row, record }) {
        const isGroup = this.isGroupRecord(record);

        record = this.resolveRecordToOrigin(record);

        // indicate row kinds
        row.assignCls({
            'b-resource-row'   : record.isResourceModel || isGroup,
            'b-assignment-row' : !isGroup && record.isAssignmentModel
        });
    }

    /**
     * The view store records wrap "real" resources and assignments.
     * This method resolves a record to its original record resource or assignment.
     * If the record does not wrap any record (happens for example for parent records when
     * using {@link Grid/feature/TreeGroup} feature) then the method returns the record itself.
     * @param {SchedulerPro.model.ResourceUtilizationModel} record The view store record
     * @returns {SchedulerPro.model.ResourceModel|SchedulerPro.model.AssignmentModel|SchedulerPro.model.ResourceUtilizationModel} Original model
     */
    resolveRecordToOrigin(record) {
        return record.origin || record.$original || record;
    }

    resolveOriginToRecord(origin) {
        let record = origin;

        if (this.store.isResourceUtilizationStore) {
            record = this.store.getModelByOrigin(origin) || record;
        }
        else if (origin.hasLinks) {
            for (const link of origin.$links) {
                if (this.store.includes(link)) {
                    return link;
                }
            }
        }

        return record;
    }

    getHistogramDataCache(record) {
        if (record) {
            record = this.resolveRecordToOrigin(record);
        }

        return super.getHistogramDataCache(record);
    }

    setHistogramDataCache(record, data) {
        record = this.resolveRecordToOrigin(record);

        return super.setHistogramDataCache(record, data);
    }

    scheduleRecordRefresh(record) {
        record = this.resolveOriginToRecord(record);

        return super.scheduleRecordRefresh(record);
    }

    scheduleRecordParentsRefresh(record) {
        record = this.resolveOriginToRecord(record);

        return super.scheduleRecordParentsRefresh(record);
    }


    getCell(data) {
        // if real resource or assignment is provided
        if (data.record?.isResourceModel || data.record?.isAssignmentModel) {
            // use its wrapper record to find proper cell
            data.record = this.resolveOriginToRecord(data.record);
        }

        return super.getCell(data);
    }

    changeHistogramWidget(widget) {
        if (widget && !widget.isHistogram) {
            if (!this.getBarTextRenderData && !widget?.getBarTextRenderData) {
                widget.getBarTextRenderData = this.getBarTextRenderDataDefault;
            }

            widget.height = this.rowHeight;
        }

        return super.changeHistogramWidget(widget);
    }

    getBarTextRenderDataDefault(renderData, datum, index) {
        // place effort text centered vertically
        renderData.y = '50%';

        return renderData;
    }

    extractHistogramDataArray(allocationReport, record) {
        let data;

        const origin = this.resolveRecordToOrigin(record);

        if (this.isGroupRecord(record)) {
            data = allocationReport.allocation.total;
        }
        else if (origin.isResourceModel) {
            data = allocationReport.allocation.total;
        }
        else if (origin.isAssignmentModel) {
            // Not having an assignment in the report could mean
            // we've just added the assignment and it's not yet processed by the Engine.
            // So in this case we just do and empty row rendering
            data = allocationReport.allocation.byAssignments.get(origin) || [];
        }

        return data;
    }

    initAggregatedAllocationEntry(_entryIndex, aggregationContext) {
        // keep list of resources met when aggregating children
        if (!aggregationContext.targetArray.$resources) {
            aggregationContext.targetArray.$resources = new Set();
        }

        return {
            tick            : null,
            effort          : 0,
            maxEffort       : 0,
            units           : 0,
            isGroup         : true,
            inEventTimeSpan : false,
            members         : new Map(),
            resources       : new Set()
        };
    }

    aggregateAllocationEntry(acc, entry, recordIndex, entryIndex, aggregationContext) {
        const
            { targetArray } = aggregationContext,
            recordArray = aggregationContext.arrays[recordIndex];

        acc.tick             = entry.tick;
        acc.isOverallocated  = acc.isOverallocated  || entry.isOverallocated;
        acc.isUnderallocated = acc.isUnderallocated || entry.isUnderallocated;
        acc.inEventTimeSpan  = acc.inEventTimeSpan || entry.inEventTimeSpan;

        // For a group entry we add members property that includes child records regrdless of their types
        if (entry.members) {
            acc.members = new Map([...acc.members, ...entry.members]);
        }
        else {
            acc.members.set(entry.resource || entry.assignment, entry);
        }

        // If that's a group entry that already met resources
        if (recordArray.$resources) {
            // inherit the resources
            targetArray.$resources = new Set([...targetArray.$resources, ...recordArray.$resources]);
        }
        // If that's a resource or assignment row entry
        else if (entry.assignment || entry.resource) {
            const resource = entry.resource || entry.assignment.resource;

            // remember we met the resource (we need this to correctly calculate maxEffort on upper levels)
            targetArray.$resources.add(resource);

            const resourceEntry = this.getHistogramDataCache(resource).allocation.total[entryIndex];

            acc.isOverallocated  = acc.isOverallocated || resourceEntry.isOverallocated;
            acc.isUnderallocated  = acc.isUnderallocated || resourceEntry.isUnderallocated;
        }

        return acc;
    }

    aggregateHistogramData() {
        const result = super.aggregateHistogramData(...arguments);

        // post process aggregated row data to find proper maxEffort
        for (let i = 0, l = result.length; i < l; i++) {
            const entry = result[i];

            entry.maxEffort = 0;

            for (const resource of result.$resources) {
                const resourceEntry = this.getHistogramDataCache(resource).allocation.total[i];

                // get nested resources maxEffort sum
                entry.maxEffort += resourceEntry.maxEffort;
            }
        }

        return result;
    }

    //endregion

    getResourceBarTip({ datum }) {
        const
            me                       = this,
            { showBarTip, timeAxis } = me;

        let result = '';

        if (showBarTip && datum.inEventTimeSpan) {
            const
                unit          = me.getBarTipEffortUnit(...arguments),
                allocated     = me.getEffortText(datum.effort, unit, true),
                available     = me.getEffortText(datum.maxEffort, unit, true),
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

            let assignmentsArray = [...datum.assignmentIntervals.entries()]
                .filter(([assignment, data]) => data.effort)
                .sort(([key1, value1], [key2, value2]) => value1.effort > value2.effort ? -1 : 1);

            if (assignmentsArray.length > me.groupBarTipAssignmentLimit) {
                assignmentsSuffix = '<br>' + me.L('L{plusMore}').replace('{value}', assignmentsArray.length - me.groupBarTipAssignmentLimit);
                assignmentsArray = assignmentsArray.slice(0, this.groupBarTipAssignmentLimit);
            }

            const assignments = assignmentsArray.map(([assignment, info]) => {

                return assignmentTpl.replace('{event}', StringHelper.encodeHtml(assignment.event.name))
                    .replace('{allocated}', me.getEffortText(info.effort, unit, true))
                    .replace('{available}', me.getEffortText(info.maxEffort, unit, true))
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

    getGroupBarTip({ datum }) {
        const
            me                       = this,
            { showBarTip, timeAxis } = me;

        let result = '';

        if (showBarTip && datum.inEventTimeSpan) {
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

            let memberArray = [...datum.members.entries()]
                .filter(([member, data]) => data.effort)
                .sort(([key1, value1], [key2, value2]) => value1.effort > value2.effort ? -1 : 1);

            if (memberArray.length > me.groupBarTipAssignmentLimit) {
                assignmentsSuffix = '<br>' + me.L('L{plusMore}').replace('{value}', memberArray.length - me.groupBarTipAssignmentLimit);
                memberArray = memberArray.slice(0, this.groupBarTipAssignmentLimit);
            }

            const members = memberArray.map(([member, info]) => {

                return assignmentTpl
                    .replace('{resource}', StringHelper.encodeHtml(member.resource?.name || member.name))
                    .replace('{event}', StringHelper.encodeHtml(member.event?.name || member.name))
                    .replace('{allocated}', me.getEffortText(info.effort, unit))
                    .replace('{available}', me.getEffortText(info.maxEffort, unit))
                    .replace('{cls}', info.isOverallocated ? 'b-overallocated' : info.isUnderallocated ? 'b-underallocated' : '');

            }).join('<br>') + assignmentsSuffix;


            result = resultFormat
                .replace('{assignments}', members)
                .replace('{startDate}', DateHelper.format(datum.tick.startDate, dateFormat))
                .replace('{endDate}', DateHelper.format(datum.tick.endDate, dateFormat))
                .replace('{allocated}', allocated)
                .replace('{available}', available)
                .replace('{cls}', datum.isOverallocated ? 'b-overallocated' : datum.isUnderallocated ? 'b-underallocated' : '');

            result = `<div class="b-histogram-bar-tooltip">${result}</div>`;
        }

        return result;
    }

    getAssignmentBarTip({ datum }) {
        const
            me                       = this,
            { showBarTip, timeAxis } = me;

        let result = '';

        if (showBarTip && datum.inEventTimeSpan) {
            const
                unit      = me.getBarTipEffortUnit(...arguments),
                allocated = me.getEffortText(datum.effort, unit, true),
                available = me.getEffortText(datum.maxEffort, unit, true);

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
                .replace('{cls}', datum.cls || '');

            if (datum.assignment) {
                result = result.replace('{event}', StringHelper.encodeHtml(datum.assignment.event.name));
            }

            result = `<div class="b-histogram-bar-tooltip">${result}</div>`;
        }

        return result;
    }

}

ResourceUtilization.initClass();

// enable tree feature for the utilization panel by default
GridFeatureManager.registerFeature(Tree, true, 'ResourceUtilization');
