import EventDrag from '../../Scheduler/feature/EventDrag.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';

/**
 * @module SchedulerPro/feature/EventSegmentDrag
 */

/**
 * Allows user to drag and drop event segments within the row.
 *
 * {@inlineexample SchedulerPro/feature/EventSegments.js}
 *
 * This feature is **enabled** by default
 *
 * @extends Scheduler/feature/EventDrag
 * @classtype eventSegmentDrag
 * @feature
 */
export default class EventSegmentDrag extends EventDrag {
    //region Config

    static $name = 'EventSegmentDrag';

    static get defaultConfig() {
        return {
            constrainDragToResource : true
        };
    }

    static get configurable() {
        return {
            capitalizedEventName : 'EventSegment'
        };
    }

    static get pluginConfig() {
        return {
            chain : ['onPaint', 'isEventElementDraggable']
        };
    }

    //endregion

    //region Events

    /**
     * Fired on the owning Scheduler to allow implementer to use asynchronous finalization by setting
     * `context.async = true` in the listener, to show a confirmation popup etc.
     * ```javascript
     *  scheduler.on('beforeEventSegmentDropFinalize', ({ context }) => {
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
     *  scheduler.on('beforeEventSegmentDropFinalize', ({ context }) => {
     *      context.valid = false;
     *  })
     * ```
     * @event beforeEventSegmentDropFinalize
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Object} context
     * @param {Boolean} context.async Set true to not finalize the drag-drop operation immediately (e.g. to wait for user confirmation)
     * @param {Scheduler.model.EventModel[]} context.eventRecords Dragged segments
     * @param {Boolean} context.valid Set this to `false` to abort the drop immediately.
     * @param {Function} context.finalize Call this method after an **async** finalization flow, to finalize the drag-drop operation. This method accepts one
     * argument: pass `true` to update records, or `false` to ignore changes
     */

    /**
     * Fired on the owning Scheduler after an event segment is dropped
     * @event afterEventSegmentDrop
     * @on-owner
     * @param {Scheduler.view.Scheduler} source
     * @param {Scheduler.model.EventModel[]} eventRecords Dropped segments
     * @param {Boolean} valid
     * @param {Object} context
     */

    /**
     * Fired on the owning Scheduler when an event segment is dropped
     * @event eventSegmentDrop
     * @on-owner
     * @param {Scheduler.view.Scheduler} source
     * @param {Scheduler.model.EventModel[]} eventRecords Dropped segments
     */

    /**
     * Fired on the owning Scheduler before event segment dragging starts. Return `false` to prevent the action.
     * @event beforeEventSegmentDrag
     * @on-owner
     * @preventable
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Segments to drag
     * @param {MouseEvent} event Browser event
     */

    /**
     * Fired on the owning Scheduler when event segment dragging starts
     * @event eventSegmentDragStart
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Dragged segments
     * @param {MouseEvent} event Browser event
     */

    /**
     * Fired on the owning Scheduler when event segments are dragged
     * @event eventSegmentDrag
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Dragged segments
     * @param {Date} startDate Start date for the current location
     * @param {Date} endDate End date for the current location
     * @param {Object} context
     * @param {Boolean} context.valid Set this to `false` to signal that the current drop position is invalid.
     */

    /**
     * Fired on the owning Scheduler after an event segment drag operation has been aborted
     * @event eventSegmentDragAbort
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Dragged segments
     */
    /**
     * Fired on the owning Scheduler after an event segment drag operation regardless of the operation being cancelled
     * or not
     * @event eventSegmentDragReset
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     */
    //endregion

    //region Drag events

    getTriggerParams(dragData) {
        const { assignmentRecords, eventRecords, resourceRecord, browserEvent: event } = dragData;

        return {
            // `context` is now private, but used in WebSocketHelper
            context : dragData,
            eventRecords,
            resourceRecord,
            assignmentRecords,
            event
        };
    }

    triggerEventDrag(dragData, start) {
        this.scheduler.trigger('eventSegmentDrag', Object.assign(this.getTriggerParams(dragData), {
            startDate : dragData.startDate,
            endDate   : dragData.endDate
        }));
    }

    triggerDragStart(dragData) {
        this.scheduler.navigator.skipNextClick = true;

        this.scheduler.trigger('eventSegmentDragStart', this.getTriggerParams(dragData));
    }

    triggerDragAbort(dragData) {
        this.scheduler.trigger('eventSegmentDragAbort', this.getTriggerParams(dragData));
    }

    triggerDragAbortFinalized(dragData) {
        this.scheduler.trigger('eventSegmentDragAbortFinalized', this.getTriggerParams(dragData));
    }

    triggerAfterDrop(dragData, valid) {
        this.scheduler.trigger('afterEventSegmentDrop', Object.assign(this.getTriggerParams(dragData), {
            valid
        }));

        if (!valid) {
            // Edge cases:
            // 1. If this drag was a no-op, and underlying data was changed while drag was ongoing (e.g. web socket
            // push), we need to manually force a view refresh to ensure a correct render state
            //
            // or
            // 2. Events were removed before we dropped at an invalid point
            const
                { assignmentStore, eventStore } = this.client,
                needRefresh                     = this.dragData.initialAssignmentsState.find(({
                    resource, assignment
                }, i) => {
                    return !assignmentStore.includes(assignment) ||
                        !eventStore.includes(assignment.event) ||
                        resource.id !== this.dragData.assignmentRecords[i]?.resourceId;
                });

            if (needRefresh) {
                this.client.refresh();
            }
        }
    }

    //endregion

    //region Update records

    /**
     * Update events being dragged.
     * @private
     * @param context Drag data.
     * @async
     */
    async updateRecords(context) {
        const
            me             = this,
            { client }     = me,
            copyKeyPressed = false;

        let result;

        if (!context.externalDropTarget) {
            client.eventStore.suspendAutoCommit();

            result = await me.updateSegment(client, context, copyKeyPressed);

            client.eventStore.resumeAutoCommit();
        }

        // Tell the world there was a successful drop
        client.trigger('eventSegmentDrop', Object.assign(me.getTriggerParams(context), {
            isCopy               : copyKeyPressed,
            event                : context.browserEvent,
            targetEventRecord    : context.targetEventRecord,
            targetResourceRecord : context.newResource,
            externalDropTarget   : context.externalDropTarget
        }));

        return result;
    }

    /**
     * Update assignments being dragged
     * @private
     * @async
     */
    async updateSegment(client, context) {
        // The code is written to emit as few store events as possible
        const
            me                  = this,
            isVertical          = client.mode === 'vertical',
            {
                eventRecords,
                assignmentRecords,
                timeDiff
            }                   = context;

        client.suspendRefresh();

        let updated = false;

        if (isVertical) {

            eventRecords.forEach((draggedEvent, i) => {
                const eventBar = context.eventBarEls[i];

                delete draggedEvent.instanceMeta(client).hasTemporaryDragElement;

                // If it was created by a call to scheduler.currentOrientation.addTemporaryDragElement
                // then release it back to be available to DomSync next time the rendered event block
                // is synced.
                if (eventBar.dataset.transient) {
                    eventBar.remove();
                }
            });
        }

        const
            eventBarEls  = context.eventBarEls.slice(),
            draggedEvent = context.eventRecord,
            newStartDate = me.adjustStartDate(context.origStart, timeDiff);

        if (!DateHelper.isEqual(draggedEvent.startDate, newStartDate)) {

            client.endListeningForBatchedUpdates();

            me.cancelBatchUpdate(draggedEvent);

            draggedEvent.startDate = newStartDate;

            updated = true;

            await client.project.commitAsync();

            me.endBatchUpdate?.(draggedEvent);
        }

        client.resumeRefresh();

        if (assignmentRecords.length > 0) {
            if (!updated) {
                context.valid = false;
            }
            else {
                // https://github.com/bryntum/support/issues/630
                // Force re-render when using fillTicks. If date changed within same tick the element won't actually
                // change and since we hijacked it for drag it won't be returned to its original position
                if (client.fillTicks) {
                    eventBarEls.forEach(el => delete el.lastDomConfig);
                }

                // Not doing full refresh above, to allow for animations
                client.refreshWithTransition();
            }
        }
    }

    //endregion

    //region Drag data

    // Prevent event draggind when it starts over a resize handle
    isEventElementDraggable(eventElement, eventRecord, el, event) {
        const me = this;

        // ALLOW event drag:
        // - if segments dragging is disabled or event is not segmented
        if (me.disabled || !(eventRecord.isEventSegment || eventRecord.segments)) {
            return true;
        }

        // otherwise make sure EventDrag is not trying to handle a segment element drag
        return !el.closest(me.drag.targetSelector);
    }

    buildDragHelperConfig() {
        const config = super.buildDragHelperConfig();

        config.targetSelector = '.b-sch-event-segment:not(.b-first)';

        return config;
    }

    getMinimalDragData(info) {
        const
            me                = this,
            { client }        = me,
            element           = me.getElementFromContext(info),
            eventRecord       = client.resolveEventRecord(element),
            resourceRecord    = client.resolveResourceRecord(element),
            assignmentRecord  = client.resolveAssignmentRecord(element),
            assignmentRecords = assignmentRecord ? [assignmentRecord] : [],
            eventRecords      = [eventRecord];

        return {
            eventRecord,
            resourceRecord,
            assignmentRecord,
            eventRecords,
            assignmentRecords
        };
    }

    beginBatchUpdate(eventRecord) {
        eventRecord.event.beginBatch();
        eventRecord.beginBatch();
    }

    endBatchUpdate(eventRecord) {
        // could be no "event" if segments got merged after dragging
        eventRecord.event?.endBatch();
        eventRecord.endBatch();
    }

    cancelBatchUpdate(eventRecord) {
        eventRecord.event?.cancelBatch();
        eventRecord.cancelBatch();
    }

    setupProductDragData(info) {
        const
            me            = this,
            { client }    = me,
            element       = me.getElementFromContext(info),
            {
                eventRecord,
                resourceRecord
            }             = me.getMinimalDragData(info),
            eventBarEls   = [],
            mainEventElement = client.getElementsFromEventRecord(eventRecord.event, resourceRecord, true)[0];

        if (me.constrainDragToResource && !resourceRecord) {
            throw new Error('Resource could not be resolved for event: ' + eventRecord.id);
        }

        // We tweak last segment drag in RTL mode so its X-ccordinate is always zero
        // so we have to tell DragHelper to still process corresponding drop event though
        // the coordinate hasn't changed
        me.drag.ignoreSamePositionDrop = !client.rtl || eventRecord.nextSegment;

        // During this batch we want the client's UI to update itself using the proposed changes
        // Only if startDrag has not already done it
        if (!client.listenToBatchedUpdates) {
            client.beginListeningForBatchedUpdates();
        }

        // Do changes in batch mode while dragging
        me.beginBatchUpdate(eventRecord);

        const
            dateConstraints    = me.getDateConstraints?.(resourceRecord, eventRecord),
            constrainRectangle = me.constrainRectangle = me.getConstrainingRectangle(dateConstraints, resourceRecord, eventRecord),
            eventRegion        = Rectangle.from(element, client.foregroundCanvas, true),
            mainEventRegion    = Rectangle.from(mainEventElement, client.foregroundCanvas, true);

        // For segment we shift constrainRectangle by the main event offset
        constrainRectangle.translate(-mainEventRegion.x);

        super.setupConstraints(
            constrainRectangle,
            eventRegion,
            client.timeAxisViewModel.snapPixelAmount,
            Boolean(dateConstraints.start)
        );

        eventBarEls.push(element);

        return {
            record          : eventRecord,
            draggedEntities : [eventRecord],
            dateConstraints : dateConstraints?.start ? dateConstraints : null,
            eventBarEls,
            mainEventElement
        };
    }

    suspendRecordElementRedrawing() {}

    suspendElementRedrawing() {}

    getDateConstraints(resourceRecord, eventRecord) {
        let { minDate, maxDate } = super.getDateConstraints(resourceRecord, eventRecord);

        // A segment movement is constrained by its neighbour segments if any
        if (eventRecord.previousSegment && (!minDate || minDate < eventRecord.previousSegment.endDate)) {
            minDate = eventRecord.previousSegment.endDate;
        }

        if (eventRecord.nextSegment && (!maxDate || maxDate < eventRecord.nextSegment.startDate)) {
            maxDate = eventRecord.nextSegment.startDate;
        }

        return {
            start : minDate,
            end   : maxDate
        };
    }

    get tipId() {
        return `${this.client.id}-segment-drag-tip`;
    }

    internalSnapToPosition(snapTo) {
        super.internalSnapToPosition();

        // for RTL we pin last segment to 0px offset ..the main event element will get updated
        if (this.client.rtl && !this.dragData.eventRecord.nextSegment) {
            snapTo.x = 0;
        }
    }

    updateDragContext(context, event) {
        super.updateDragContext(...arguments);

        const
            { client } = this,
            {
                dirty,
                eventRecord,
                endDate
            } = this.dragData;

        // If dragging the last segment update the main event width accordingly
        // need this to update dependency properly while dragging
        if (dirty && !eventRecord.nextSegment) {

            const { enableEventAnimations } = client;

            client.enableEventAnimations = false;

            eventRecord.event.set('endDate', endDate);

            if (client.features.eventBuffer?.enabled) {
                eventRecord.event.wrapEndDate = endDate;
            }

            client.enableEventAnimations = enableEventAnimations;
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(EventSegmentDrag, true, 'SchedulerPro');
GridFeatureManager.registerFeature(EventSegmentDrag, false, 'ResourceHistogram');
