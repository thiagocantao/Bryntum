import DateHelper from '../../Core/helper/DateHelper.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DomSync from '../../Core/helper/DomSync.js';
import EventResize from '../../SchedulerPro/feature/EventResize.js';

/**
 * @module SchedulerPro/feature/EventSegmentResize
 */

/**
 * Feature that allows resizing an event segment by dragging its end.
 *
 * {@inlineexample SchedulerPro/feature/EventSegments.js}
 *
 * This feature is **enabled** by default
 *
 * @extends SchedulerPro/feature/EventResize
 * @classtype eventSegmentResize
 * @feature
 */
export default class EventSegmentResize extends EventResize {

    //region Events

    /**
     * Fired on the owning Scheduler Pro before resizing starts. Return `false` to prevent the action.
     * @event beforeEventSegmentResize
     * @on-owner
     * @preventable
     * @param {SchedulerPro.view.SchedulerPro} source Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} eventRecord Segment being resized
     * @param {SchedulerPro.model.ResourceModel} resourceRecord Resource record the resize starts within
     * @param {MouseEvent} event Browser event
     */

    /**
     * Fires on the owning Scheduler Pro when segment resizing starts
     * @event eventSegmentResizeStart
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} eventRecord Segment being resized
     * @param {SchedulerPro.model.ResourceModel} resourceRecord Resource record the resize starts within
     * @param {MouseEvent} event Browser event
     */

    /**
     * Fires on the owning Scheduler Pro on each segment resize move event
     * @event eventSegmentPartialResize
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} eventRecord Segment being resized
     * @param {Date} startDate
     * @param {Date} endDate
     * @param {HTMLElement} element
     */

    /**
     * Fired on the owning Scheduler Pro to allow implementer to prevent immediate finalization by setting
     * `data.context.async = true` in the listener, to show a confirmation popup etc
     * ```javascript
     *  scheduler.on('beforeEventSegmentResizeFinalize', ({context}) => {
     *      context.async = true;
     *      setTimeout(() => {
     *          // async code don't forget to call finalize
     *          context.finalize();
     *      }, 1000);
     *  })
     * ```
     * @event beforeEventSegmentResizeFinalize
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source Scheduler Pro instance
     * @param {Object} context
     * @param {Boolean} context.async Set true to handle resize asynchronously (e.g. to wait for user confirmation)
     * @param {Function} context.finalize Call this method to finalize resize. This method accepts one argument:
     *                   pass `true` to update records, or `false`, to ignore changes
     */

    /**
     * Fires on the owning Scheduler Pro after the resizing gesture has finished.
     * @event eventSegmentResizeEnd
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source Scheduler Pro instance
     * @param {Boolean} changed Shows if the record has been changed by the resize action
     * @param {SchedulerPro.model.EventModel} eventRecord Segment being resized
     */

    //endregion

    //region Config

    static $name = 'EventSegmentResize';

    static get pluginConfig() {
        return {
            chain : [...super.pluginConfig.chain, 'isEventSegmentElementDraggable']
        };
    }

    //endregion

    //region Init & destroy

    render() {
        super.render(...arguments);

        // Only active when in these items
        this.dragSelector = this.dragItemSelector = '.b-sch-event-segment';
    }

    // Prevent segment dragging when it starts over resize handles
    isEventSegmentElementDraggable(eventElement, eventRecord, el, event) {
        return this.isEventElementDraggable(...arguments);
    }

    // Prevent event dragging when it starts over resize handles
    isEventElementDraggable(eventElement, eventRecord, el, event) {
        const
            me = this,
            eventResizable = eventRecord?.resizable;

        // ALLOW event drag:
        // - if resizing is disabled or event is not resizable
        // - if it's a milestone Milestones cannot be resized
        if (me.disabled || !eventResizable || eventRecord.isMilestone) {
            return true;
        }

        // not over a segment resize handle
        if (eventRecord.segments) {
            const eventName = me.client.scheduledEventName;

            return eventRecord.segments.every((segmentRecord, index) => {
                const
                    segmentResizable = segmentRecord.resizable,
                    segmentElement = DomSync.getChild(eventElement, `${eventName}.segments.${index}`);

                return !segmentResizable ||
                    (((segmentResizable !== true && segmentResizable !== 'start') ||
                        !me.isOverStartHandle(event, segmentElement)) &&
                    ((segmentResizable !== true && segmentResizable !== 'end') ||
                        !me.isOverEndHandle(event, segmentElement)));
            });
        }

        return super.isEventElementDraggable(...arguments);
    }

    get tipId() {
        return `${this.client.id}-event-segment-resize-tip`;
    }

    dragStart(drag) {
        this._segmentsSlices      = null;
        this._segmentsSlicesIndex = 0;

        return super.dragStart(...arguments);
    }

    beginEventRecordBatch(eventRecord) {
        super.beginEventRecordBatch(eventRecord);

        // when resizing segment we change its master event too
        if (eventRecord.isEventSegment) {
            eventRecord.event.beginBatch();
        }
    }

    // Subclasses may override this
    triggerBeforeResize(drag) {
        const
            { client }  = this,
            name        = client.scheduledEventName,
            eventRecord = client.resolveTimeSpanRecord(drag.itemElement);

        return client.trigger(
            `before${client.capitalizedEventName}SegmentResize`,
            {
                [name + 'Record'] : eventRecord,
                event             : drag.event,
                ...this.getBeforeResizeParams({ event : drag.startEvent, element : drag.itemElement })
            }
        );
    }

    // Subclasses may override this
    triggerEventResizeStart(eventType, event) {
        const { client } = this;

        client.trigger(`${client.scheduledEventName}SegmentResizeStart`, event);
    }

    triggerEventResizeEnd(eventType, event) {
        const { client } = this;

        client.trigger(`${client.scheduledEventName}SegmentResizeEnd`, event);
    }

    triggerEventPartialResize(eventType, event) {
        // Trigger eventPartialResize or taskPartialResize depending on product
        const { client } = this;

        client.trigger(`${client.scheduledEventName}SegmentPartialResize`, event);
    }

    triggerBeforeEventResizeFinalize(eventType, event) {
        const { client } = this;

        client.trigger(`before${client.capitalizedEventName}SegmentResizeFinalize`, event);
    }

    applyDateConstraints(date, eventRecord, context) {
        let
            minDate = context.dateConstraints?.start,
            maxDate = context.dateConstraints?.end;

        // constrain segment resize w/ previous & next segments
        if (eventRecord.isEventSegment) {
            const { previousSegment, nextSegment } = eventRecord;

            if (previousSegment) {
                minDate = minDate ? DateHelper.max(previousSegment.endDate, minDate) : previousSegment.endDate;
            }

            if (nextSegment) {
                maxDate = maxDate ? DateHelper.min(nextSegment.startDate, maxDate) : nextSegment.startDate;
            }
        }

        // Keep desired date within constraints
        if (minDate || maxDate) {
            date = DateHelper.constrain(date, minDate, maxDate);
            context.snappedDate = DateHelper.constrain(context.snappedDate, minDate, maxDate);
        }

        return date;
    }

    resizeEventPartiallyInternal(eventRecord, context) {
        const
            { toSet } = context;

        super.resizeEventPartiallyInternal(...arguments);

        // in case that's a segment
        if (eventRecord.isEventSegment) {
            const { event } = eventRecord;

            // if that's the last segment and user is dragging its endDate
            // -> update the event endDate too
            if (!eventRecord.nextSegment && toSet === 'endDate') {
                event.set('endDate', context[toSet]);
            }
            // if that's the first segment and user is dragging its startDate
            // -> update the event startDate too
            else if (!eventRecord.previousSegment && toSet === 'startDate') {
                event.set('startDate', context[toSet]);
            }
            else {
                const segmentsField = event.fieldMap['segments'];

                // Set a special flag on "segments" field forcing changes by making isEqual() result false
                segmentsField._skipSegmentsIsEqual++;
                event.set('segments', event.get('segments'));
                segmentsField._skipSegmentsIsEqual--;
            }
        }

    }

    cancelEventRecordBatch(eventRecord) {
        super.cancelEventRecordBatch(eventRecord);

        // if resizing a segment revert master event changes too
        if (eventRecord.isEventSegment) {
            eventRecord.event.cancelBatch();
        }
    }

    /**
     * Highlights handles (applies css that changes cursor).
     * @private
     */
    highlightHandle() {
        const { overItem } = this;

        // over a handle, add cls to change cursor
        overItem.classList.add('b-resize-handle', 'b-over-resize-handle');
    }

    /**
     * Unhighlight handles (removes css).
     * @private
     */
    unHighlightHandle(item = this.overItem) {
        item?.classList.remove('b-resize-handle', this.resizingItemInnerCls, 'b-over-resize-handle', this.draggingItemCls);
    }

    //endregion

}

GridFeatureManager.registerFeature(EventSegmentResize, true, 'SchedulerPro');
GridFeatureManager.registerFeature(EventSegmentResize, false, 'ResourceHistogram');
