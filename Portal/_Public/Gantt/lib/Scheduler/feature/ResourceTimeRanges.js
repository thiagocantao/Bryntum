import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ResourceTimeRangesBase from './base/ResourceTimeRangesBase.js';
import ResourceTimeRangeStore from '../data/ResourceTimeRangeStore.js';

/**
 * @module Scheduler/feature/ResourceTimeRanges
 */

/**
 * Feature that draws resource time ranges, shaded areas displayed behind events. These zones are similar to events in
 * that they have a start and end date but different in that they do not take part in the event layout, and they always
 * occupy full row height.
 *
 * {@inlineexample Scheduler/feature/ResourceTimeRanges.js}
 *
 * Each time range is represented by an instances of {@link Scheduler.model.ResourceTimeRangeModel}, held in a
 * {@link Scheduler.data.ResourceTimeRangeStore}. Currently they are readonly UI-wise, but can be manipulated on
 * the data level. To style the rendered elements, use the {@link Scheduler.model.TimeSpan#field-cls} field or use the
 * {@link Scheduler.model.ResourceTimeRangeModel#field-timeRangeColor} field.
 *
 * Data can be provided either using the {@link Scheduler.view.Scheduler#config-resourceTimeRanges} config on the
 * Scheduler config object:
 *
 * ```javascript
 * new Scheduler({
 *     ...
 *    features :  {
 *        resourceTimeRanges : true
 *    },
 *
 *    // Data specified directly on the Scheduler instance
 *    resourceTimeRanges : [
 *        // Either specify startDate & endDate or startDate & duration when defining a range
 *        { startDate : new Date(2019,0,1), endDate : new Date(2019,0,3), name : 'Occupied', timeRangeColor : 'red' },
 *        { startDate : new Date(2019,0,3), duration : 2, durationUnit : 'd', name : 'Available' },
 *    ]
 * })
 * ```
 *
 * Or the {@link Scheduler.view.Scheduler#config-resourceTimeRangeStore} config on the Scheduler config object:
 *
 * ```javascript
 * new Scheduler({
 *     ...
 *     features :  {
 *         resourceTimeRanges : true
 *     },
 *     resourceTimeRangeStore : new ResourceTimeRangeStore({
 *         readUrl : './resourceTimeRanges/'
 *     })
 * })
 * ```
 *
 * Or on the project, using the {#Scheduler/model/mixin/ProjectModelMixin#config-resourceTimeRangesData} config.
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * ## Recurring ranges support
 *
 * Resource time ranges can also be recurring, as seen in the example below:
 *
 * ```js
 * const resourceTimeRangeStore = new ResourceTimeRangeStore({
 *     data : [{
 *         id             : 1,
 *         resourceId     : 'r1',
 *         startDate      : '2019-01-01T11:00',
 *         endDate        : '2019-01-01T13:00',
 *         name           : 'Lunch',
 *         // this time range will repeat every day
 *         recurrenceRule : 'FREQ=DAILY'
 *     }]
 * });
 *
 * ```
 *
 * @extends Scheduler/feature/base/ResourceTimeRangesBase
 * @demo Scheduler/resourcetimeranges
 * @classtype resourceTimeRanges
 * @feature
 */
export default class ResourceTimeRanges extends ResourceTimeRangesBase {
    //region Config

    static $name = 'ResourceTimeRanges';

    static configurable = {
        rangeCls : 'b-sch-resourcetimerange',

        /**
         * Set to `true` to allow mouse interactions with the rendered range elements. By default, the range elements
         * are not reachable with the mouse, and only serve as a static background.
         * @prp {Boolean}
         * @default
         */
        enableMouseEvents : false,

        /**
         * Specify value to use for the tabIndex attribute of resource time range elements
         * @config {Number}
         * @default
         */
        tabIndex : 0,

        entityName : 'resourceTimeRange'
    };

    static get pluginConfig() {
        const superConfig = super.pluginConfig;
        return {
            ...superConfig,
            assign : ['resolveResourceTimeRangeRecord', 'getElementFromResourceTimeRangeRecord']
        };
    }

    //endregion

    //region Init


    attachToProject(project) {
        const
            me                     = this,
            { client : scheduler } = me;

        super.attachToProject(project);

        if (!project.resourceTimeRangeStore) {
            project.resourceTimeRangeStore = scheduler.resourceTimeRangeStore || new ResourceTimeRangeStore({
                owner : me
            });

            const { crudManager } = scheduler;

            if (crudManager && !crudManager.resourceTimeRangeStore) {
                crudManager.resourceTimeRangeStore = project.resourceTimeRangeStore;
            }
        }

        const store = project.resourceTimeRangeStore;

        if (!me.exposedOnScheduler) {
            // ResourceZones can be set on scheduler or feature, for convenience
            if (scheduler.resourceTimeRanges) {
                store.add(scheduler.resourceTimeRanges);
                delete scheduler.resourceTimeRanges;
            }

            // expose getter/setter for resourceTimeRanges on scheduler
            Object.defineProperty(scheduler, 'resourceTimeRanges', {
                get : () => store.records,
                set : resourceTimeRanges => store.data = resourceTimeRanges
            });

            me.exposedOnScheduler = true;
        }

        // Link to projects resourceStore if not already linked to one
        if (!store.resourceStore) {
            store.resourceStore = project.resourceStore;
        }

        me.detachListeners('store');

        store.ion({
            name    : 'store',
            change  : me.onStoreChange,
            thisObj : me
        });
    }

    // Called by ProjectConsumer after a new store is assigned at runtime

    attachToResourceTimeRangeStore(store) {
        this.attachToProject(this.project);
        this.client.refresh();
    }

    get store() {
        return this.project.resourceTimeRangeStore;
    }

    //endregion

    //region Events

    /**
     * Triggered for mouse down ona resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeMouseDown
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for mouse up ona resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeMouseUp
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for click on a resource time range. Only triggered if the ResourceTimeRange feature is configured with
     * `enableMouseEvents: true`.
     * @event resourceTimeRangeClick
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for double-click on a resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeDblClick
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for right-click on a resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeContextMenu
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for mouse over on a resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeMouseOver
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    /**
     * Triggered for mouse out of a resource time range. Only triggered if the ResourceTimeRange feature is configured
     * with `enableMouseEvents: true`.
     * @event resourceTimeRangeMouseOut
     * @param {Scheduler.view.Scheduler} source This Scheduler
     * @param {Scheduler.feature.ResourceTimeRanges} feature The ResourceTimeRange feature
     * @param {Scheduler.model.ResourceTimeRangeModel} resourceTimeRangeRecord Resource time range record
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record
     * @param {MouseEvent} domEvent Browser event
     * @on-owner
     */

    //endregion

    /**
     * Returns a resource time range record from the passed element
     * @param {HTMLElement} rangeElement
     * @returns {Scheduler.model.ResourceTimeRangeModel}
     * @on-owner
     * @function resolveResourceTimeRangeRecord
     * @category DOM
     */

    /**
     * Returns the element for the passed resource time range record, if rendered into DOM.
     * @param {Scheduler.model.ResourceTimeRangeModel} record
     * @returns {HTMLElement}
     * @on-owner
     * @function getElementFromResourceTimeRangeRecord
     * @category DOM
     */

    // Called on render of resources events to get events to render. Add any ranges
    // (chained function from Scheduler)
    getEventsToRender(resource, events) {
        if (!this.disabled) {
            const { timeRanges } = resource.$original;

            // if we have ranges and the feature is enabled
            if (timeRanges?.length) {
                const { startDate, endDate } = this.client;

                timeRanges.forEach(timeRange => {
                    // if this a recurring event let's include its visible occurrences
                    if (timeRange.isRecurring) {
                        events.push(...timeRange.getOccurrencesForDateRange(startDate, endDate));
                    }
                    else {
                        events.push(timeRange);
                    }
                });
            }
        }

        return events;
    }

    shouldInclude(eventRecord) {
        return eventRecord.isResourceTimeRange && !eventRecord.isNonWorking && !eventRecord.isCalendarHighlightModel;
    }

    doDestroy() {
        if (this.store?.owner === this) {
            this.store.destroy();
        }
        super.doDestroy();
    }
}

// No feature based styling needed, do not add a cls to Scheduler
ResourceTimeRanges.featureClass = '';

GridFeatureManager.registerFeature(ResourceTimeRanges, false, 'Scheduler');
