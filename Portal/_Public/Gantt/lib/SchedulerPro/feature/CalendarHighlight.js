import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ResourceTimeRangesBase from '../../Scheduler/feature/base/ResourceTimeRangesBase.js';
import ResourceTimeRangeModel from '../../Scheduler/model/ResourceTimeRangeModel.js';

/**
 * @module SchedulerPro/feature/CalendarHighlight
 */

let counter = 0;

class CalendarHighlightModel extends ResourceTimeRangeModel {
    static get $name() {
        return 'CalendarHighlightModel';
    }

    static domIdPrefix = 'calendarhighlight';

    // For nicer DOM, since the records are transient we do not need a fancy UUID
    static generateId() {
        return ++counter;
    }
}

/**
 * This feature temporarily visualizes {@link SchedulerPro/model/CalendarModel calendars} for the event or resource
 * calendar (controlled by the {@link #config-calendar} config). The calendars are highlighted while a user is creating,
 * dragging or resizing a task. Enabling this feature makes it easier for the end user to understand the underlying
 * rules of the schedule.
 *
 * {@inlineexample SchedulerPro/feature/CalendarHighlight.js}
 *
 * ## Example usage
 *
 * ```javascript
 * new SchedulerPro({
 *     features : {
 *         calendarHighlight : {
 *             // visualize resource calendars while interacting with events
 *             calendar : 'resource'
 *         }
 *     }
 * })
 * ```
 *
 * This feature is **disabled** by default.
 *
 * @extends Scheduler/feature/base/ResourceTimeRangesBase
 * @classtype calendarHighlight
 * @feature
 * @demo SchedulerPro/highlight-event-calendars
 */
export default class CalendarHighlight extends ResourceTimeRangesBase {

    //region Config

    static get $name() {
        return 'CalendarHighlight';
    }

    static get configurable() {
        return {
            /**
             * A string defining which calendar(s) to highlight during drag drop, resize or create flows.
             * Valid values are `event` or `resource`.
             *
             * @config {'event'|'resource'}
             * @default
             */
            calendar : 'event',

            /**
             * A string defining which calendar(s) to highlight during drag drop, resize or create flows.
             * Valid values are `event` or `resource`.
             *
             * @config {'event'|'resource'}
             */
            unhighlightOnDrop : null,

            /**
             * A callback function which is called when you interact with one or more events (e.g. drag drop) to
             * highlight only available resources.
             *
             * ```javascript
             * new SchedulerPro({
             *     features : {
             *         calendarHighlight : {
             *             collectAvailableResources({ scheduler, eventRecords }) {
             *                  const mainEvent = eventRecords[0];
             *                  return scheduler.resourceStore.query(resource => resource.role === mainEvent.requiredRole || !mainEvent.requiredRole);
             *              }
             *         }
             *     }
             * });
             * ```
             *
             * @param {Object} context A context object
             * @param {SchedulerPro.view.SchedulerPro} context.scheduler The scheduler instance
             * @param {Scheduler.model.EventModel[]} context.eventRecords The event records
             * @returns {Scheduler.model.ResourceModel[]} An array with the available resource records
             * @config {Function}
             */
            collectAvailableResources : null,

            rangeCls                    : 'b-sch-highlighted-calendar-range',
            resourceTimeRangeModelClass : CalendarHighlightModel,
            inflate                     : 3
        };
    }

    static get pluginConfig() {
        const config  = super.pluginConfig;

        config.assign = [
            'highlightEventCalendars',
            'highlightResourceCalendars',
            'unhighlightCalendars'
        ];

        return config;
    }

    afterConstruct() {
        super.afterConstruct();

        this.client.ion({
            eventDragStart   : 'onEventDragStart',
            eventDragReset   : 'unhighlightCalendars',
            eventResizeStart : 'onEventResizeStart',
            eventResizeEnd   : 'unhighlightCalendars',
            dragCreateStart  : 'onDragCreateStart',
            afterDragCreate  : 'unhighlightCalendars',
            thisObj          : this
        });
    }

    //endregion

    highlightCalendar(eventRecords, resourceRecords) {
        eventRecords = ArrayHelper.asArray(eventRecords);
        resourceRecords = ArrayHelper.asArray(resourceRecords);

        if (this.calendar === 'event') {
            this.highlightEventCalendars(eventRecords, resourceRecords);
        }
        else {
            this.highlightResourceCalendars(resourceRecords);
        }
    }

    // region public APIs
    /**
     * Highlights the time spans representing the calendars of the passed event records, and resource records.
     * @on-owner
     * @param {Scheduler.model.EventModel[]} eventRecords The event records
     * @param {Scheduler.model.ResourceModel[]} [resourceRecords] The resource records
     * @param {Boolean} [clearExisting] Provide `false` to leave previous highlight elements
     */
    highlightEventCalendars(eventRecords, resourceRecords, clearExisting = true) {
        const
            me                     = this,
            { client }             = me,
            { startDate, endDate } = client;

        if (me.disabled) {
            return;
        }

        if (clearExisting) {
            me.unhighlightCalendars();
        }

        eventRecords = ArrayHelper.asArray(eventRecords);

        if (!resourceRecords) {
            resourceRecords = eventRecords.flatMap(event => event.$linkedResources);
        }

        me.highlight = new Map();

        resourceRecords = ArrayHelper.asArray(resourceRecords);

        eventRecords.forEach(eventRecord => {
            if (!eventRecord.calendar) {
                return;
            }

            const timespans = eventRecord.calendar
                ?.getWorkingTimeRanges(startDate, endDate)
                .map(timespan => new CalendarHighlightModel(timespan));

            if (timespans) {
                for (const resourceRecord of resourceRecords) {
                    me.highlight.set(resourceRecord, timespans);
                    client.currentOrientation.refreshEventsForResource(resourceRecord, true, false);
                }
                if (resourceRecords.length > 0) {
                    client.currentOrientation.onRenderDone();
                }
            }
        });

        client.syncSplits?.(split => split.highlightEventCalendars(eventRecords, resourceRecords, clearExisting));
    }

    /**
     * Highlights the time spans representing the working time calendars of the passed resource records.
     * @on-owner
     * @param {Scheduler.model.ResourceModel[]} resourceRecords The resource records
     * @param {Boolean} [clearExisting] Provide `false` to leave previous highlight elements
     */
    highlightResourceCalendars(resourceRecords, clearExisting = true) {
        const
            me                                         = this,
            { startDate, endDate, currentOrientation } = me.client;

        if (me.disabled) {
            return;
        }

        if (clearExisting) {
            me.unhighlightCalendars();
        }

        // Highlight resource calendars
        me.highlight = new Map();

        for (const resourceRecord of resourceRecords) {
            const timespans = resourceRecord.calendar
                ?.getWorkingTimeRanges(startDate, endDate)
                .map(timespan => new CalendarHighlightModel(timespan));

            if (timespans) {
                me.highlight.set(resourceRecord, timespans);
                currentOrientation.refreshEventsForResource(resourceRecord, true, false);
            }
        }
        if (resourceRecords.length > 0) {
            currentOrientation.onRenderDone();
        }

        me.client.syncSplits?.(split => split.highlightResourceCalendars(resourceRecords, clearExisting));
    }

    /**
     * Removes all highlight elements.
     * @on-owner
     */
    unhighlightCalendars() {
        const me = this;

        if (!me.highlight) {
            // We're not highlighting anything, bail out
            return;
        }

        const
            { currentOrientation } = me.client,
            resources              = me.highlight.keys();

        me.highlight = null;

        for (const resource of resources) {
            currentOrientation.refreshEventsForResource(resource, true, false);
        }

        currentOrientation.onRenderDone();

        me.client.syncSplits?.(split => split.unhighlightCalendars());
    }

    // endregion

    // region event listeners
    onEventDragStart({ context }) {
        if (this.disabled) {
            return;
        }

        const
            me               = this,
            { client }       = me,
            { eventRecords } = context,
            resourceRecords  = context.availableResources =
                client.features.eventDrag.constrainDragToResource
                    ? [context.resourceRecord]
                    : me.collectAvailableResources?.({
                        scheduler : client,
                        eventRecords
                    }) ?? client.resourceStore.records;

        me.highlightCalendar(eventRecords, resourceRecords);
    }

    onEventResizeStart({ eventRecord, resourceRecord }) {
        if (!this.disabled) {
            this.highlightCalendar(eventRecord, [resourceRecord]);
        }
    }

    onDragCreateStart({ eventRecord, resourceRecord }) {
        if (!this.disabled) {
            this.highlightCalendar(eventRecord, [resourceRecord]);
        }
    }

    // endregion

    // Called on render of resources events to get events to render. Add any ranges
    // (chained function from Scheduler)
    getEventsToRender(resource, events) {
        const timespans = this.highlight?.get(resource);

        timespans && events.push(...timespans);

        return events;
    }

    onEventDataGenerated(renderData) {
        const { eventRecord } = renderData;

        if (eventRecord.isCalendarHighlightModel) {
            const { inflate } = this;

            // Flag that we should fill entire row/col
            renderData.fillSize = this.client.isVertical;
            // Add our own cls
            renderData.wrapperCls['b-sch-highlighted-calendar-range'] = 1;
            // Add label
            renderData.children.push({
                className : 'b-sch-event-content',
                html      : eventRecord.name,
                dataset   : {
                    taskBarFeature : 'content'
                }
            });

            // Inflate
            renderData.width += inflate * 2;
            renderData.height += inflate * 2;
            renderData.left -= inflate;
            renderData.top -= inflate;

            // Event data for DOMSync comparison, unique per calendar & resource combination
            renderData.eventId = `${this.generateElementId(eventRecord)}-resource-${renderData.resourceRecord.id}`;
        }
    }

    updateDisabled(disabled, was) {
        super.updateDisabled(disabled, was);

        if (disabled) {
            this.unhighlightCalendars();
        }
    }

    shouldInclude(eventRecord) {
        return eventRecord.isCalendarHighlightModel;
    }

    // No classname on Scheduler's/Gantt's element
    get featureClass() {}
}

GridFeatureManager.registerFeature(CalendarHighlight, false, 'SchedulerPro');
