import DragBase from './base/DragBase.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import EventHelper from '../../Core/helper/EventHelper.js';

/**
 * @module Scheduler/feature/EventDrag
 */

/**
 * Allows user to drag and drop events within the scheduler, to change startDate or resource assignment.
 *
 * This feature is **enabled** by default
 *
 * ## Customizing the drag drop tooltip
 *
 * To show custom HTML in the tooltip, please see the {@link #config-tooltipTemplate} config. Example:
 *
 * ```javascript
 * features: {
 *     eventDrag : {
 *         // A minimal start date tooltip
 *         tooltipTemplate : ({ eventRecord, startDate }) => {
 *             return DateHelper.format(startDate, 'HH:mm');
 *         }
 *     }
 * }
 * ```
 *
 * ## Constraining the drag drop area
 *
 * You can constrain how the dragged event is allowed to move by using the following configs
 * * {@link #config-constrainDragToResource} Resource fixed, only allowed to change start date
 * * {@link #config-constrainDragToTimeSlot} Start date is fixed, only move between resources
 * * {@link Scheduler.view.Scheduler#config-getDateConstraints} A method on the Scheduler instance
 *    which lets you define the date range for the dragged event programmatically
 *
 * ```js
 * // Enable dragging + constrain drag to current resource
 * const scheduler = new Scheduler({
 *     features : {
 *         eventDrag : {
 *             constrainDragToResource : true
 *         }
 *     }
 * });
 * ```
 *
 * ## Drag drop events from outside
 *
 * Dragging unplanned events from an external grid is a very popular use case. There are
 * several demos showing you how to do this. Please see the [Drag from grid demo](../examples/dragfromgrid)
 * and study the **Drag from grid guide** to learn more.
 *
 * ## Drag drop events to outside target
 *
 * You can also drag events outside the schedule area by setting {@link #config-constrainDragToTimeline} to `false`. You
 * should also either:
 * * provide a {@link #config-validatorFn} to programmatically define if a drop location is valid or not
 * * configure a {@link #config-externalDropTargetSelector} CSS selector to define where drops are allowed
 *
 * See [this demo](../examples/drag-outside) to see this in action.
 *
 * ## Validating drag drop
 *
 * It is easy to programmatically decide what is a valid drag drop operation. Use the {@link #config-validatorFn}
 * and return either `true` / `false` (optionally a message to show to the user).
 *
 * ```javascript
 * features : {
 *     eventDrag : {
 *        validatorFn({ eventRecords, newResource }) {
 *            const task  = eventRecords[0],
 *                  valid = newResource.role === task.resource.role;
 *
 *            return {
 *                valid   : newResource.role === task.resource.role,
 *                message : valid ? '' : 'Resource role does not match required role for this task'
 *            };
 *        }
 *     }
 * }
 * ```
 *
 * See [this demo](../examples/validation) to see validation in action.
 *
 * If you instead want to do a single validation upon drop, you can listen to {@link #event-beforeEventDropFinalize}
 * and set the `valid` flag on the context object provided.
 *
 * ```javascript
 *   const scheduler = new Scheduler({
 *      listeners : {
 *          beforeEventDropFinalize({ context }) {
 *              const { eventRecords } = context;
 *              // Don't allow dropping events in the past
 *              context.valid = Date.now() <= eventRecords[0].startDate;
 *          }
 *      }
 *  });
 * ```
 *
 * ## Preventing drag of certain events
 *
 * To prevent certain events from being dragged, you have two options. You can set {@link Scheduler.model.EventModel#field-draggable}
 * to `false` in your data, or you can listen for the {@link Scheduler.view.Scheduler#event-beforeEventDrag} event and
 * return `false` to block the drag.
 *
 * ```javascript
 * new Scheduler({
 *    listeners : {
 *        beforeEventDrag({ eventRecord }) {
 *            // Don't allow dragging events that have already started
 *            return Date.now() <= eventRecord.startDate;
 *        }
 *    }
 * })
 * ```
 *
 * @extends Scheduler/feature/base/DragBase
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/EventDrag.js
 * @classtype eventDrag
 * @feature
 */
export default class EventDrag extends DragBase {
    //region Config

    static get $name() {
        return 'EventDrag';
    }

    static get configurable() {
        return {
            /**
             * Template used to generate drag tooltip contents.
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         eventDrag : {
             *             dragTipTemplate({eventRecord, startText}) {
             *                 return `${eventRecord.name}: ${startText}`
             *             }
             *         }
             *     }
             * });
             * ```
             * @config {Function} tooltipTemplate
             * @param {Object} data Tooltip data
             * @param {Scheduler.model.EventModel} data.eventRecord
             * @param {Boolean} data.valid Currently over a valid drop target or not
             * @param {Date} data.startDate New start date
             * @param {Date} data.endDate New end date
             * @returns {String}
             */

            /**
             * Set to true to only allow dragging events within the same resource.
             * @member {Boolean} constrainDragToResource
             */
            /**
             * Set to true to only allow dragging events within the same resource.
             * @config {Boolean}
             * @default
             */
            constrainDragToResource : false,

            /**
             * Set to true to only allow dragging events to different resources, and disallow rescheduling by dragging.
             * @member {Boolean} constrainDragToTimeSlot
             */
            /**
             * Set to true to only allow dragging events to different resources, and disallow rescheduling by dragging.
             * @config {Boolean}
             * @default
             */
            constrainDragToTimeSlot : false,

            /**
             * A CSS selector specifying elements outside the scheduler element which are valid drop targets.
             * @config {String}
             */
            externalDropTargetSelector : null,

            /**
             * An empty function by default, but provided so that you can perform custom validation on the item being
             * dragged. This function is called during the drag and drop process and also after the drop is made.
             * Return `true` if the new position is valid, `false` to prevent the drag.
             *
             * ```javascript
             * features : {
             *     eventDrag : {
             *         validatorFn({ eventRecords, newResource }) {
             *             const
             *                 task  = eventRecords[0],
             *                 valid = newResource.role === task.resource.role;
             *
             *             return {
             *                 valid   : newResource.role === task.resource.role,
             *                 message : valid ? '' : 'Resource role does not match required role for this task'
             *             };
             *         }
             *     }
             * }
             * ```
             * @param {Object} context A drag drop context object
             * @param {Date} context.startDate New start date
             * @param {Date} context.endDate New end date
             * @param {Scheduler.model.AssignmentModel[]} context.assignmentRecords Assignment records which were dragged
             * @param {Scheduler.model.EventModel[]} context.eventRecords Event records which were dragged
             * @param {Scheduler.model.ResourceModel} context.newResource New resource record
             * @param {Scheduler.model.EventModel} context.targetEventRecord Currently hovering this event record
             * @param {Event} event The event object
             * @returns {Boolean|Object} `true` if this validation passes, `false` if it does not.
             *
             * Or an object with 2 properties: `valid` -  Boolean `true`/`false` depending on validity,
             * and `message` - String with a custom error message to display when invalid.
             * @config {Function}
             */
            validatorFn : (context, event) => {},

            /**
             * The `this` reference for the validatorFn
             * @config {Object}
             */
            validatorFnThisObj : null,

            /**
             * When the host Scheduler is `{@link Scheduler.view.mixin.EventSelection#config-multiEventSelect}: true`
             * then, there are two modes of dragging *within the same Scheduler*.
             *
             * Non unified means that all selected events are dragged by the same number of resource rows.
             *
             * Unified means that all selected events are collected together and dragged as one, and are all dropped
             * on the same targeted resource row at the same targeted time.
             * @member {Boolean} unifiedDrag
             */
            /**
             * When the host Scheduler is `{@link Scheduler.view.mixin.EventSelection#config-multiEventSelect}: true`
             * then, there are two modes of dragging *within the same Scheduler*.
             *
             * Non unified means that all selected events are dragged by the same number of resource rows.
             *
             * Unified means that all selected events are collected together and dragged as one, and are all dropped
             * on the same targeted resource row at the same targeted time.
             * @config {Boolean}
             * @default false
             */
            unifiedDrag : null,

            /**
             * A hook that allows manipulating the position the drag proxy snaps to. Manipulate the `snapTo` property
             * to alter snap position.
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         eventDrag : {
             *             snapToPosition({ eventRecord, snapTo }) {
             *                 if (eventRecord.late) {
             *                     snapTo.x = 400;
             *                 }
             *             }
             *         }
             *     }
             * });
             * ```
             *
             * @config {Function}
             * @param {Object} context
             * @param {Scheduler.model.AssignmentModel} context.assignmentRecord Dragged assignment
             * @param {Scheduler.model.EventModel} context.eventRecord Dragged event
             * @param {Scheduler.model.ResourceModel} context.resourceRecord Currently over this resource
             * @param {Date} context.startDate Start date for current position
             * @param {Date} context.endDate End date for current position
             * @param {Object} context.snapTo
             * @param {Number} context.snapTo.x X to snap to
             * @param {Number} context.snapTo.y Y to snap to
             */
            snapToPosition : null,

            /**
             * A modifier key (CTRL, SHIFT, ALT, META) that when pressed will copy an event instead of moving it. Set to
             * empty string to disable copying
             * @prp {'CTRL'|'ALT'|'SHIFT'|'META'|''}
             * @default
             */
            copyKey : 'SHIFT',

            /**
             * Event can be copied two ways: either by adding new assignment to an existing event ('assignment'), or
             * by copying the event itself ('event'). 'auto' mode will pick 'event' for a single-assignment mode (when
             * event has `resourceId` field) and 'assignment' mode otherwise.
             * @prp {'auto'|'assignment'|'event'}
             * @default
             */
            copyMode : 'auto',

            /**
             * Mode of the current drag drop operation.
             * @member {'move'|'copy'}
             * @readonly
             */
            mode : 'move',

            capitalizedEventName : null
        };
    }

    afterConstruct() {
        this.capitalizedEventName = this.capitalizedEventName || this.client.capitalizedEventName;
        super.afterConstruct(...arguments);
    }

    //endregion

    changeMode(value) {
        const { dragData, copyMode } = this;

        // Do not create assignments in case scheduler doesn't use multiple assignments
        // Do not allow to copy recurring events
        if (
            (copyMode === 'event' || copyMode === 'auto' ||
                copyMode === 'assignment' && !this.scheduler.eventStore.usesSingleAssignment) &&
            (!dragData || dragData.eventRecords.every(r => !r.isRecurring))
        ) {
            return value;
        }
    }

    updateMode(mode) {
        if (this.dragData) {
            if (mode === 'copy') {
                this.setCopying();
            }
            else {
                this.setMoving();
            }

            /**
             * Triggered when drag mode is changed, for example when copy key is
             * pressed or released while dragging.
             * @event eventDragModeChange
             * @param {String} mode Drag mode, could be either 'move', 'copy', or 'auto'
             * @on-owner
             */
            this.client.trigger('eventDragModeChange', { mode });
        }
    }

    setCopying() {
        const { dragData } = this;

        if (!dragData) {
            return;
        }

        // Check if proxies are added to the DOM by checking if any of them is
        if (!dragData.eventBarCopies.some(el => el.isConnected)) {
            dragData.eventBarCopies.forEach(el => {
                el.classList.add('b-drag-proxy-copy');
                // hidden class can be added by the drag feature if we're dragging event outside
                el.classList.remove('b-hidden');

                dragData.context.grabbedParent.appendChild(el);
                // Mark this node as ignored for the DomSync
                el.retainElement = true;
            });
        }
        else {
            dragData.eventBarCopies.forEach(el => {
                el.classList.remove('b-hidden');
            });
        }
    }

    setMoving() {
        const { dragData } = this;

        if (!dragData) {
            return;
        }

        dragData.eventBarCopies.forEach(el => {
            el.classList.add('b-hidden');
        });
    }

    //region Events

    /**
     * Fired on the owning Scheduler to allow implementer to use asynchronous finalization by setting `context.async = true`
     * in the listener, to show a confirmation popup etc.
     * ```javascript
     *  scheduler.on('beforeeventdropfinalize', ({ context }) => {
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
     *  scheduler.on('beforeeventdropfinalize', ({ context }) => {
     *      context.valid = false;
     *  })
     * ```
     * @event beforeEventDropFinalize
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Object} context
     * @param {Boolean} context.async Set true to not finalize the drag-drop operation immediately (e.g. to wait for user confirmation)
     * @param {Scheduler.model.EventModel[]} context.eventRecords Event records being dragged
     * @param {Scheduler.model.AssignmentModel[]} context.assignmentRecords Assignment records being dragged
     * @param {Scheduler.model.EventModel} context.targetEventRecord Event record for drop target
     * @param {Scheduler.model.ResourceModel} context.newResource Resource record for drop target
     * @param {Boolean} context.valid Set this to `false` to abort the drop immediately.
     * @param {Function} context.finalize Call this method after an **async** finalization flow, to finalize the drag-drop operation. This method accepts one
     * argument: pass `true` to update records, or `false` to ignore changes
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler after event drop
     * @event afterEventDrop
     * @on-owner
     * @param {Scheduler.view.Scheduler} source
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
     * @param {Scheduler.model.EventModel[]} eventRecords
     * @param {Boolean} valid
     * @param {Object} context
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler when an event is dropped
     * @event eventDrop
     * @on-owner
     * @param {Scheduler.view.Scheduler} source
     * @param {Scheduler.model.EventModel[]} eventRecords
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
     * @param {HTMLElement} externalDropTarget The HTML element dropped upon, if drop happened on a valid external drop target
     * @param {Boolean} isCopy
     * @param {Object} context
     * @param {Scheduler.model.EventModel} context.targetEventRecord Event record for drop target
     * @param {Scheduler.model.ResourceModel} context.newResource Resource record for drop target
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler before event dragging starts. Return `false` to prevent the action.
     * @event beforeEventDrag
     * @on-owner
     * @preventable
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel} eventRecord Event record the drag starts from
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag starts from
     * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
     * @param {MouseEvent} event Browser event DEPRECATED (replaced by domEvent)
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler when event dragging starts
     * @event eventDragStart
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag starts from
     * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
     * @param {MouseEvent} event Browser event DEPRECATED (replaced by domEvent)
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler when event is dragged
     * @event eventDrag
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
     * @param {Date} startDate Start date for the current location
     * @param {Date} endDate End date for the current location
     * @param {Scheduler.model.ResourceModel} resourceRecord Resource record the drag started from
     * @param {Scheduler.model.ResourceModel} newResource Resource at the current location
     * @param {Object} context
     * @param {Boolean} context.valid Set this to `false` to signal that the current drop position is invalid.
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler after an event drag operation has been aborted
     * @event eventDragAbort
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.EventModel[]} eventRecords Event records being dragged
     * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Assignment records being dragged
     * @param {MouseEvent} domEvent Browser event
     */
    /**
     * Fired on the owning Scheduler after an event drag operation regardless of the operation being cancelled or not
     * @event eventDragReset
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     */
    //endregion

    //region Data layer

    // Deprecated. Use this.client instead
    get scheduler() {
        return this.client;
    }

    //endregion

    //#region Drag lifecycle

    onAfterDragStart(event) {
        const
            me                        = this,
            { context : { element } } = event;

        super.onAfterDragStart(event);

        me.handleKeyDownOrMove(event.event);

        me.keyEventDetacher = EventHelper.on({
            // In case we drag event between scheduler focused event gets moved and focus
            // moves to the body. We only need to read the key from this event
            element : DomHelper.getRootElement(element),
            keydown : me.handleKeyDownOrMove,
            keyup   : me.handleKeyUp,
            thisObj : me
        });
    }

    onDragReset(event) {
        super.onDragReset(event);

        this.keyEventDetacher?.();

        this.mode = 'move';
    }

    onDrop(event) {
        // Always remove proxy on drop
        this.dragData.eventBarCopies?.forEach(el => el.remove());

        return super.onDrop(event);
    }

    //#endregion

    //region Drag events

    getDraggableElement(el) {
        return el?.closest(this.drag.targetSelector);
    }

    resolveEventRecord(eventElement, client = this.client) {
        return client.resolveEventRecord(eventElement);
    }

    isElementDraggable(el, event) {
        const
            me           = this,
            { client }   = me,
            eventElement = me.getDraggableElement(el);

        if (!eventElement || me.disabled || client.readOnly) {
            return false;
        }

        // displaying something resizable within the event?
        if (el.matches('[class$="-handle"]')) {
            return false;
        }

        const eventRecord = me.resolveEventRecord(eventElement, client);

        if (!eventRecord || !eventRecord.isDraggable || eventRecord.readOnly) {
            return false;
        }

        // Hook for features that need to prevent drag
        const prevented = client[`is${me.capitalizedEventName}ElementDraggable`]?.(
            eventElement, eventRecord, el, event
        ) === false;

        return !prevented;
    }

    getTriggerParams(dragData) {
        const { assignmentRecords, eventRecords, resourceRecord, browserEvent : domEvent } = dragData;

        return {
            // `context` is now private, but used in WebSocketHelper
            context : dragData,
            eventRecords,
            resourceRecord,
            assignmentRecords,
            event   : domEvent, // Deprecated, remove on  6.0?
            domEvent
        };
    }

    triggerBeforeEventDrag(eventType, event) {
        return this.client.trigger(eventType, event);
    }

    triggerEventDrag(dragData, start) {
        this.client.trigger('eventDrag', Object.assign(this.getTriggerParams(dragData), {
            startDate   : dragData.startDate,
            endDate     : dragData.endDate,
            newResource : dragData.newResource
        }));
    }

    triggerDragStart(dragData) {
        this.client.navigator.skipNextClick = true;

        this.client.trigger('eventDragStart', this.getTriggerParams(dragData));
    }

    triggerDragAbort(dragData) {
        this.client.trigger('eventDragAbort', this.getTriggerParams(dragData));
    }

    triggerDragAbortFinalized(dragData) {
        this.client.trigger('eventDragAbortFinalized', this.getTriggerParams(dragData));
    }

    triggerAfterDrop(dragData, valid) {
        const me = this;

        me.currentOverClient.trigger('afterEventDrop', Object.assign(me.getTriggerParams(dragData), {
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
                { assignmentStore, eventStore } = me.client,
                needRefresh                     = me.dragData.initialAssignmentsState.find(({
                    resource, assignment
                }, i) => {
                    return !assignmentStore.includes(assignment) ||
                        !eventStore.includes(assignment.event) ||
                        resource.id !== me.dragData.assignmentRecords[i]?.resourceId;
                });

            if (needRefresh) {
                me.client.refresh();
            }
        }
        // Reset the skipNextClick after a potential click event fires. https://github.com/bryntum/support/issues/5135
        me.client.setTimeout(() => me.client.navigator.skipNextClick = false, 10);
    }

    handleKeyDownOrMove(event) {
        if (this.mode !== 'copy') {
            if (event.key && EventHelper.specialKeyFromEventKey(event.key) === this.copyKey?.toLowerCase() || event[`${this.copyKey?.toLowerCase()}Key`]) {
                this.mode = 'copy';
            }
        }
    }

    handleKeyUp(event) {
        if (EventHelper.specialKeyFromEventKey(event.key) === this.copyKey.toLowerCase()) {
            this.mode = 'move';
        }
    }

    //endregion

    //region Finalization & validation

    /**
     * Checks if an event can be dropped on the specified position.
     * @private
     * @returns {Boolean} Valid (true) or invalid (false)
     */
    isValidDrop(dragData) {
        const
            {
                newResource,
                resourceRecord,
                browserEvent
            }            = dragData,
            sourceRecord = dragData.draggedEntities[0],
            { target }   = browserEvent;

        // Only allowed to drop outside scheduler element if we hit an element matching the externalDropTargetSelector
        if (!newResource) {
            return (!this.constrainDragToTimeline && this.externalDropTargetSelector) ? Boolean(target.closest(this.externalDropTargetSelector)) : false;
        }

        // Not allowed to drop an event on a group header or a readOnly resource
        if (newResource.isSpecialRow || newResource.readOnly) {
            return false;
        }

        // Not allowed to assign an event twice to the same resource
        if (resourceRecord !== newResource) {
            return !sourceRecord.event.resources.includes(newResource);
        }

        return true;
    }

    checkDragValidity(dragData, event) {
        const
            me        = this,
            scheduler = me.currentOverClient;

        let result;

        // Cannot assign anything to readOnly resources
        if (dragData.newResource?.readOnly) {
            return false;
        }

        // First make sure there's no overlap, if not run the external validatorFn
        if (!scheduler.allowOverlap && !scheduler.isDateRangeAvailable(
            dragData.startDate,
            dragData.endDate,
            dragData.draggedEntities[0],
            dragData.newResource
        )) {
            result = {
                valid   : false,
                message : me.L('L{eventOverlapsExisting}')
            };
        }
        else {
            result = me.validatorFn.call(
                me.validatorFnThisObj || me,
                dragData,
                event
            );
        }

        if (!result || result.valid) {
            // Hook for features to have a say on validity
            result = scheduler['checkEventDragValidity']?.(dragData, event) ?? result;
        }

        return result;
    }

    //endregion

    //region Update records

    /**
     * Update events being dragged.
     * @private
     * @param context Drag data.
     */
    async updateRecords(context) {
        const
            me                            = this,
            fromScheduler                                          = me.client,
            toScheduler                                            = me.currentOverClient,
            copyKeyPressed                                         = me.mode === 'copy',
            { draggedEntities, timeDiff, initialAssignmentsState } = context,
            originalStartDate                                      = initialAssignmentsState[0].startDate,
            droppedStartDate                                       = me.adjustStartDate(originalStartDate, timeDiff);

        let result;

        if (!context.externalDropTarget) {
            // Dropping dragged event completely outside the time axis is not allowed
            if (!toScheduler.timeAxis.timeSpanInAxis(droppedStartDate, DateHelper.add(droppedStartDate, draggedEntities[0].event.durationMS, 'ms'))) {
                context.valid = false;
            }

            if (context.valid) {
                fromScheduler.eventStore.suspendAutoCommit();
                toScheduler.eventStore.suspendAutoCommit();

                result = await me.updateAssignments(fromScheduler, toScheduler, context, copyKeyPressed);

                fromScheduler.eventStore.resumeAutoCommit();
                toScheduler.eventStore.resumeAutoCommit();
            }
        }

        // Might be flagged invalid in updateAssignments() above, if drop did not lead to any change
        // (for example if dropped on non-working-time in Pro)
        if (context.valid) {
            // Tell the world there was a successful drop
            toScheduler.trigger('eventDrop', Object.assign(me.getTriggerParams(context), {
                isCopy               : copyKeyPressed,
                copyMode             : me.copyMode,
                domEvent             : context.browserEvent,
                targetEventRecord    : context.targetEventRecord,
                targetResourceRecord : context.newResource,
                externalDropTarget   : context.externalDropTarget
            }));
        }

        return result;
    }

    /**
     * Update assignments being dragged
     * @private
     */
    async updateAssignments(fromScheduler, toScheduler, context, copy) {
        // The code is written to emit as few store events as possible
        const
            me                  = this,
            { copyMode }        = me,
            isCrossScheduler    = (fromScheduler !== toScheduler),
            { isVertical }      = toScheduler,
            {
                assignmentStore : fromAssignmentStore,
                eventStore      : fromEventStore
            }                   = fromScheduler,
            {
                assignmentStore : toAssignmentStore,
                eventStore      : toEventStore
            }                   = toScheduler,
            // When using TreeGroup in horizontal mode, store != resourceStore. Does not apply for vertical mode.
            fromResourceStore   = fromScheduler.isVertical ? fromScheduler.resourceStore : fromScheduler.store,
            toResourceStore     = isVertical ? toScheduler.resourceStore : toScheduler.store,
            {
                eventRecords,
                assignmentRecords,
                timeDiff,
                initialAssignmentsState,
                resourceRecord : fromResource,
                newResource    : toResource
            }                   = context,
            { unifiedDrag }     = me,
            // For an empty target event store, check if it has usesSingleAssignment explicitly set, otherwise use
            // the value from the source event store
            useSingleAssignment = (toEventStore.usesSingleAssignment ||
                (toEventStore.usesSingleAssignment !== false && fromEventStore.usesSingleAssignment)),
            // this value has clear semantic only for same scheduler case
            effectiveCopyMode   = copyMode === 'event'
                ? 'event'
                : copyMode === 'assignment'
                    ? 'assignment'
                    : useSingleAssignment ? 'event' : 'assignment',
            event1Date          = me.adjustStartDate(assignmentRecords[0].event.startDate, timeDiff),
            eventsToAdd         = [],
            eventsToRemove      = [],
            assignmentsToAdd    = [],
            assignmentsToRemove = [],
            eventsToCheck       = [],
            eventsToBatch       = new Set(),
            resourcesInStore    = fromResourceStore.getAllDataRecords();

        fromScheduler.suspendRefresh();
        toScheduler.suspendRefresh();

        let updated      = false,
            updatedEvent = false,
            indexDiff; // By how many resource rows has the drag moved.

        if (isCrossScheduler) {
            // The difference in indices via first dragged event will help us find resources for all the rest of the
            // events accordingly
            indexDiff = toResourceStore.indexOf(toResource) - fromResourceStore.indexOf(fromResource);
        }
        else if (me.constainDragToResource) {
            indexDiff = 0;
        }
        else if (isVertical && toResourceStore.isGrouped) {
            indexDiff = resourcesInStore.indexOf(fromResource) - resourcesInStore.indexOf(toResource);
        }
        else {
            indexDiff = fromResourceStore.indexOf(fromResource) - fromResourceStore.indexOf(toResource);
        }

        if (isVertical) {

            eventRecords.forEach((draggedEvent, i) => {
                const eventBar = context.eventBarEls[i];

                delete draggedEvent.instanceMeta(fromScheduler).hasTemporaryDragElement;

                // If it was created by a call to scheduler.currentOrientation.addTemporaryDragElement
                // then release it back to be available to DomSync next time the rendered event block
                // is synced.
                if (eventBar.dataset.transient) {
                    eventBar.remove();
                }
            });
        }

        const
            eventBarEls          = context.eventBarEls.slice(),
            addedEvents          = [],
            // this map holds references between original assignment and its copy
            copiedAssignmentsMap = {};

        // Using for to support await inside
        for (let i = 0; i < assignmentRecords.length; i++) {
            const originalAssignment = assignmentRecords[i];

            // Reassigned when dropped on other scheduler, thus not const
            let draggedEvent = originalAssignment.event,
                draggedAssignment;

            if (copy) {
                draggedAssignment                           = originalAssignment.copy();
                copiedAssignmentsMap[originalAssignment.id] = draggedAssignment;
            }
            else {
                draggedAssignment = originalAssignment;
            }

            if (!draggedAssignment.isOccurrenceAssignment && (!fromAssignmentStore.includes(originalAssignment) || !fromEventStore.includes(draggedEvent))) {
                // Event was removed externally during the drag, just remove element from DOM (DomSync already has
                // tried to clean it up at this point, but could not due to retainElement being set)
                eventBarEls[i].remove();
                eventBarEls.splice(i, 1);
                assignmentRecords.splice(i, 1);
                i--;
                continue;
            }

            const
                initialState           = initialAssignmentsState[i],
                originalEventRecord    = draggedEvent,
                originalStartDate      = initialState.startDate,
                // grabbing resource early, since after ".copy()" the record won't belong to any store
                // and ".getResources()" won't work. If it's a move to another scheduler, ensure the
                // array still has a length. The process function will do an assign as opposed
                // to a reassignment
                originalResourceRecord = initialState.resource,
                // Calculate new startDate (and round it) based on timeDiff up here, might be added to another
                // event store below in which case it is invalidated. But this is anyway the target date
                newStartDate           = this.constrainDragToTimeSlot
                    ? originalStartDate
                    : (unifiedDrag
                        ? event1Date
                        : me.adjustStartDate(originalStartDate, timeDiff));

            if (fromAssignmentStore !== toAssignmentStore) {
                // Single assignment from a multi assigned event dragged over, event needs to be copied over
                // Same if we hold the copy key
                const keepEvent = originalEventRecord.assignments.length > 1 || copy;

                let newAssignment;

                if (copy) {
                    // In a copy mode dragged assignment is already a copy
                    newAssignment = draggedAssignment;
                }
                else {
                    newAssignment                              = draggedAssignment.copy();
                    copiedAssignmentsMap[draggedAssignment.id] = newAssignment;
                }

                // Pro Engine does not seem to handle having the event already in place on the copied assignment,
                // replacing it with id to have events bucket properly set up on commit
                if (newAssignment.event && !useSingleAssignment) {
                    newAssignment.event    = newAssignment.event.id;
                    newAssignment.resource = newAssignment.resource.id;
                }

                if (!copy) {
                    // If we're not copying, remove assignment from source scheduler
                    assignmentsToRemove.push(draggedAssignment);
                }

                // If it was the last assignment, the event should also be removed
                if (!keepEvent) {
                    eventsToRemove.push(originalEventRecord);
                }

                // If event does not already exist in target scheduler a copy is added
                // if we're copying the event, we always need to create new record
                if (
                    copy && (copyMode === 'event' || (copyMode === 'auto' && toEventStore.usesSingleAssignment)) ||
                    !toEventStore.getById(originalEventRecord.id)
                ) {
                    draggedEvent = toEventStore.createRecord({
                        ...originalEventRecord.data,
                        // If we're copying the event (not making new assignment to existing), we need to generate
                        // phantom id to link event to the assignment record
                        id       : copy && (copyMode === 'event' || copyMode === 'auto') ? undefined : originalEventRecord.id,
                        // Engine gets mad if not nulled
                        calendar : null
                    });

                    newAssignment.set({
                        eventId : draggedEvent.id,
                        event   : draggedEvent
                    });

                    eventsToAdd.push(draggedEvent);
                }

                // And add it to the target scheduler

                if (!useSingleAssignment) {
                    assignmentsToAdd.push(newAssignment);
                }
                draggedAssignment = newAssignment;
            }

            let newResource    = toResource,
                reassignedFrom = null;

            if (!unifiedDrag) {
                if (!isCrossScheduler) {
                    // If not dragging events as a unified block, distribute each to a new resource
                    // using the same offset as the dragged event.
                    if (indexDiff !== 0) {
                        let newIndex;
                        if (isVertical && toResourceStore.isGrouped) {
                            newIndex    = Math.max(
                                Math.min(
                                    resourcesInStore.indexOf(originalResourceRecord) - indexDiff,
                                    resourcesInStore.length - 1
                                ),
                                0
                            );
                            newResource = resourcesInStore[newIndex];
                        }
                        else {
                            newIndex = Math.max(
                                Math.min(
                                    fromResourceStore.indexOf(originalResourceRecord) - indexDiff,
                                    fromResourceStore.count - 1
                                ),
                                0
                            );

                            newResource = fromResourceStore.getAt(newIndex);

                            // Exclude group headers, footers, summary row etc
                            if (newResource.isSpecialRow) {
                                newResource = fromResourceStore.getNext(newResource, false, true) || fromResourceStore.getPrevious(newResource, false, true);
                            }
                        }

                        newResource = newResource?.$original;
                    }
                    else {
                        newResource = originalResourceRecord;
                    }
                }
                // we have a resource for first dragged event in toResource
                else if (i > 0) {
                    const draggedEventResourceIndex = fromResourceStore.indexOf(originalResourceRecord);
                    newResource                     = toResourceStore.getAt(draggedEventResourceIndex + indexDiff) || newResource;
                }
            }

            const isCrossResource = draggedAssignment.resourceId !== newResource.id;

            // Cannot rely on assignment generation to detect update, since it might be a new assignment

            if (isCrossResource) {
                reassignedFrom = fromResourceStore.getById(draggedAssignment.resourceId);

                if (copy && fromAssignmentStore === toAssignmentStore) {
                    // Scheduler Core patch
                    // need to completely clear the resource/resourceId on the copied assignment, before setting the new
                    // otherwise, what happens is that in the `$beforeChange.resource/Id` are still
                    // stored the resource/Id of the original assignment
                    // then, when finalizing commit, Core engine performs this:
                    //     // First silently revert any data change (used by buckets), otherwise it won't be detected by `set()`
                    //     me.setData(me.$beforeChange)
                    // and then updates the data to new, which is recorded as UpdateAction in the STM with old/new data
                    // then, when that update action in STM is undo-ed, the old data is written back to the record
                    // and newly added assignment is pointing to the old resource
                    // then, when STM action is redo-ed, a "duplicate assignment" exception is thrown
                    // this is covered with the test:
                    // Scheduler/tests/features/EventDragCopy.t.js -> Should not remove the original when undo-ing the copy-drag action ("multi-assignment")
                    draggedAssignment.setData({
                        resource   : null,
                        resourceId : null
                    });
                    // eof Scheduler Core patch

                    draggedAssignment.resource = newResource;
                    draggedAssignment.event    = toEventStore.getById(draggedAssignment.eventId);

                    const shouldCopyEvent = copyMode === 'event' || (fromEventStore.usesSingleAssignment && copyMode === 'auto');

                    if (shouldCopyEvent) {
                        draggedEvent = draggedEvent.copy();
                        // need to clear the `endDate` of the copy
                        // this is because when we drag the copy to a different position on the timeline
                        // it will set the new start date and re-calculate end date
                        // as a result, in STM transaction for this drag-copy there will be "add" action
                        // and "update" action and NO COMMIT in the middle
                        // so when re-doing this transaction the duration change is lost
                        // this is covered with the test:
                        // "Scheduler/tests/features/EventDragCopy.t.js -> Should not remove the original when undo-ing the copy-drag action (usesSingleAssignment)",
                        // Before doing it, save a copy of endDate in meta object, considering timeDiff: that's because below it will check if event is in timeAxis.
                        draggedEvent.meta.endDateCached = me.adjustStartDate(draggedEvent.endDate, timeDiff);
                        draggedEvent.endDate = null;

                        draggedAssignment.event = draggedEvent;

                        if (toEventStore.usesSingleAssignment) {
                            draggedEvent.resource   = newResource;
                            draggedEvent.resourceId = newResource.id;
                        }
                    }

                    if (
                        !toAssignmentStore.find(a => a.eventId === draggedAssignment.eventId && a.resourceId === draggedAssignment.resourceId) &&
                        !assignmentsToAdd.find(r => r.eventId === draggedAssignment.eventId && r.resourceId === draggedAssignment.resourceId)
                    ) {
                        shouldCopyEvent && eventsToAdd.push(draggedEvent);
                        assignmentsToAdd.push(draggedAssignment);
                    }
                }
                else {
                    draggedAssignment.resource = newResource;
                }

                // Actual events should be batched, not data for new events when dragging between
                draggedEvent.isEvent && eventsToBatch.add(draggedEvent);
                updated = true;

                // When dragging an occurrence, the assignment is only temporary. We have to tag the newResource along
                // to be picked up by the occurrence -> event conversion

                if (draggedEvent.isOccurrence) {
                    draggedEvent.set('newResource', newResource);
                }


                if (isCrossScheduler && useSingleAssignment) {
                    // In single assignment mode, when dragged to another scheduler it will not copy the assignment
                    // over but instead set the resourceId of the event. To better match expected behaviour
                    draggedEvent.resourceId = newResource.id;
                }
            }
            else {
                if (
                    copy &&
                    (copyMode === 'event' || (copyMode === 'auto' && fromEventStore.usesSingleAssignment)) &&
                    !eventsToAdd.includes(draggedEvent)
                ) {
                    draggedEvent = draggedEvent.copy();
                    // see the comment above
                    draggedEvent.meta.endDateCached = me.adjustStartDate(draggedEvent.endDate, timeDiff);
                    draggedEvent.endDate = null;

                    eventsToAdd.push(draggedEvent);

                    draggedAssignment.event = draggedEvent;

                    if (toEventStore.usesSingleAssignment) {
                        draggedEvent.set({
                            resource   : newResource,
                            resourceId : newResource.id
                        });
                    }

                    // Always add assignment to the store to allow proper element reuse
                    assignmentsToAdd.push(draggedAssignment);
                }
            }

            // Same for event
            if (!eventsToCheck.find(ev => ev.draggedEvent === draggedEvent) && !DateHelper.isEqual(draggedEvent.startDate, newStartDate)) {

                // only do for non occurence records
                while (!draggedEvent.isOccurrence && draggedEvent.isBatchUpdating) {
                    draggedEvent.endBatch(true);
                }

                // for same scheduler with multi-assignments, and copyMode === assignment, need to keep the start date
                // because user intention is to create a new assignment, not re-schedule the event
                // but only for cross-resource dragging, same resource dragging has semantic of regular drag

                const shouldKeepStartDate = copy && !isCrossScheduler && !useSingleAssignment && effectiveCopyMode === 'assignment' && isCrossResource;

                if (!shouldKeepStartDate) {
                    draggedEvent.startDate = newStartDate;

                    eventsToCheck.push({ draggedEvent, originalStartDate });
                }

                draggedEvent.isEvent && eventsToBatch.add(draggedEvent);
                updatedEvent = true;
            }

            // Hook for features that need to do additional processing on drop (used by NestedEvents)
            toScheduler.processEventDrop({
                eventRecord    : draggedEvent,
                resourceRecord : newResource,
                element        : i === 0 ? context.context.element : context.context.relatedElements[i - 1],
                context,
                toScheduler,
                reassignedFrom,
                eventsToAdd,
                addedEvents,
                draggedAssignment
            });

            // There are two cases to consider when triggering this event - `copy` and `move` mode. In case we are
            // copying the assignment (we can also copy the event) draggedAssignment will point to the copy of the
            // original assignment record. Same for draggedEvent. These records are new records which are not yet added
            // to the store and they contain correct state of the drop - which event is going to be assigned to which
            // resource on what time.
            // These records possess no knowledge about original records which they were cloned from. And that might be
            // useful. Let's say you want to copy assignment (or event) to every row in the way. You need to know start
            // row and the end row. That information is kept in the `originalAssignment` record. Which might be identical
            // to the `draggedAssignment` record in `move` mode.
            toScheduler.trigger('processEventDrop', {
                originalAssignment,
                draggedAssignment,
                context,
                copyMode,
                isCopy : copy
            });
        }

        fromAssignmentStore.remove(assignmentsToRemove);
        fromEventStore.remove(eventsToRemove);
        toAssignmentStore.add(assignmentsToAdd);

        // Modify syncIdMap on the FGCanvas to make sure elements get animated nicely to new position
        if (copy && fromAssignmentStore === toAssignmentStore) {
            const { syncIdMap } = fromScheduler.foregroundCanvas;

            Object.entries(copiedAssignmentsMap).forEach(([originalId, cloneRecord]) => {
                const element = syncIdMap[originalId];
                delete syncIdMap[originalId];
                syncIdMap[cloneRecord.id] = element;
            });
        }

        eventsToAdd.length && addedEvents.push(...toEventStore.add(eventsToAdd));

        // When not constrained to timeline we are dragging a clone and need to manually do some cleanup if
        // dropped in view
        if (!me.constrainDragToTimeline) {
            // go through assignmentRecords again after events has been added to toEventStore (if any)
            // now we have updated assignment ids and can properly reuse event HTML elements
            for (let i = 0; i < assignmentRecords.length; i++) {
                const
                    assignmentRecord     = copiedAssignmentsMap[assignmentRecords[i].id] || assignmentRecords[i],
                    originalDraggedEvent = assignmentRecord.event,
                    // try to get dragged event from addedEvents array, it will be there with updated ids
                    // if toScheduler is different
                    draggedEvent         = addedEvents?.find(r => r.id === originalDraggedEvent.id) || originalDraggedEvent,
                    eventBar             = context.eventBarEls[i],
                    element              = i === 0 ? context.context.element : context.context.relatedElements[i - 1],
                    // Determine if in time axis here also, since the records date might be invalidated further below
                    inTimeAxis           = toScheduler.isInTimeAxis(draggedEvent);

                // after checking if is in time axis, imeta.endDateCached can be deleted
                delete draggedEvent.meta.endDateCached;

                if (!copy) {
                    // Remove original element properly
                    DomSync.removeChild(eventBar.parentElement, eventBar);
                }

                if (draggedEvent.resource && (isVertical || toScheduler.rowManager.getRowFor(draggedEvent.resource)) && inTimeAxis) {
                    // Nested events are added to correct parent by the feature
                    if (!draggedEvent.parent || draggedEvent.parent.isRoot) {
                        const elRect = Rectangle.from(element, toScheduler.foregroundCanvas, true);

                        // Ensure that after inserting the dragged element clone into the toScheduler's foregroundCanvas
                        // it's at the same visual position that it was dragged to.
                        DomHelper.setTopLeft(element, elRect.y, elRect.x);

                        // Add element properly, so that DomSync will reuse it on next update
                        DomSync.addChild(toScheduler.foregroundCanvas, element, draggedEvent.assignments[0].id);

                        isCrossScheduler && toScheduler.processCrossSchedulerEventDrop({
                            eventRecord : draggedEvent,
                            toScheduler
                        });
                    }

                    element.classList.remove('b-sch-event-hover', 'b-active', 'b-drag-proxy', 'b-dragging');
                    element.retainElement = false;
                }
            }
        }

        addedEvents?.forEach(added => eventsToBatch.add(added));

        // addedEvents order is the same with [context.element, ..context.relatedElements]
        // Any added or removed events or assignments => something changed
        if (assignmentsToRemove.length || eventsToRemove.length || assignmentsToAdd.length || eventsToAdd.length) {
            updated = true;
        }

        // Commit changes to affected projects
        if (updated || updatedEvent) {
            // By batching event changes when using single assignment we avoid two updates, without it there will be one
            // for date change and one when changed assignment updates resourceId on the event
            useSingleAssignment && eventsToBatch.forEach(eventRecord => eventRecord.beginBatch());

            await Promise.all([
                toScheduler.project !== fromScheduler.project ? toScheduler.project.commitAsync() : null,
                fromScheduler.project.commitAsync()
            ]);

            // End batch in engine friendly way, avoiding to have `set()` trigger another round of calculations
            useSingleAssignment && eventsToBatch.forEach(eventRecord => eventRecord.endBatch(false, true));
        }

        if (!updated) {
            // Engine might have reverted the date change, in which case this should be considered an invalid op
            updated = eventsToCheck.some(({ draggedEvent, originalStartDate }) =>
                !DateHelper.isEqual(draggedEvent.startDate, originalStartDate)
            );
        }

        // Resumes self twice if not cross scheduler, but was suspended twice above also so all good
        toScheduler.resumeRefresh();
        fromScheduler.resumeRefresh();

        if (assignmentRecords.length > 0) {
            if (!updated) {
                context.valid = false;
            }
            else {
                // Always force re-render of the bars, to return them to their original position when:
                // * Fill ticks leading to small date adjustment not actually changing the DOM
                //   (https://github.com/bryntum/support/issues/630)
                // * Dragging straight down with multiselection, events in the last resource will still be assigned to
                //   that resource = no change in the DOM (https://github.com/bryntum/support/issues/6293)
                eventBarEls.forEach(el => delete el.lastDomConfig);

                // Not doing full refresh above, to allow for animations
                toScheduler.refreshWithTransition();

                if (isCrossScheduler) {
                    fromScheduler.refreshWithTransition();

                    toScheduler.selectedEvents = addedEvents;
                }
            }
        }
    }

    //endregion

    //region Drag data

    getProductDragContext(dragData) {
        const
            me                                = this,
            { currentOverClient : scheduler } = me,
            target                            = dragData.browserEvent.target,
            previousResolvedResource          = dragData.newResource || dragData.resourceRecord,
            previousTargetEventRecord         = dragData.targetEventRecord;

        let
            targetEventRecord = scheduler ? me.resolveEventRecord(target, scheduler) : null,
            newResource, externalDropTarget;

        // Ignore if over dragged event
        if (dragData.eventRecords.includes(targetEventRecord)) {
            targetEventRecord = null;
        }

        if (me.constrainDragToResource) {
            newResource = dragData.resourceRecord;
        }
        else if (!me.constrainDragToTimeline) {
            newResource = me.resolveResource();
        }
        else if (scheduler) {
            newResource = me.resolveResource() || dragData.newResource || dragData.resourceRecord;
        }

        const
            { assignmentRecords, eventRecords } = dragData,
            isOverNewResource                   = previousResolvedResource !== newResource;

        let valid = Boolean(newResource && !newResource.isSpecialRow);

        if (!newResource && me.externalDropTargetSelector) {
            externalDropTarget = target.closest(me.externalDropTargetSelector);
            valid              = Boolean(externalDropTarget);
        }

        return {
            valid,
            externalDropTarget,
            eventRecords,
            assignmentRecords,
            newResource,
            targetEventRecord,
            dirty         : isOverNewResource || targetEventRecord !== previousTargetEventRecord,
            proxyElements : [dragData.context.element, ...dragData.context.relatedElements || []]
        };
    }

    getMinimalDragData(info) {
        const
            me                = this,
            { scheduler }     = me,
            element           = me.getElementFromContext(info),
            eventRecord       = me.resolveEventRecord(element, scheduler),
            resourceRecord    = scheduler.resolveResourceRecord(element),
            assignmentRecord  = scheduler.resolveAssignmentRecord(element),
            assignmentRecords = assignmentRecord ? [assignmentRecord] : [];

        // We multi drag other selected events if the dragged event is already selected, or the ctrl key is pressed
        if (assignmentRecord && (scheduler.isAssignmentSelected(assignmentRecords[0]) || (me.drag.startEvent.ctrlKey && scheduler.multiEventSelect))) {
            assignmentRecords.push.apply(assignmentRecords, me.getRelatedRecords(assignmentRecord));
        }

        const eventRecords = [...new Set(assignmentRecords.map(assignment => assignment.event))];

        return {
            eventRecord,
            resourceRecord,
            assignmentRecord,
            eventRecords,
            assignmentRecords
        };
    }

    setupProductDragData(info) {
        const
            me            = this,
            { scheduler } = me,
            element       = me.getElementFromContext(info),
            {
                eventRecord,
                resourceRecord,
                assignmentRecord,
                assignmentRecords
            }             = me.getMinimalDragData(info),
            eventBarEls   = [];

        if (me.constrainDragToResource && !resourceRecord) {
            throw new Error('Resource could not be resolved for event: ' + eventRecord.id);
        }

        let dateConstraints;

        if (me.constrainDragToTimeline) {
            dateConstraints = me.getDateConstraints?.(resourceRecord, eventRecord);

            const
                constrainRectangle = me.constrainRectangle = me.getConstrainingRectangle(dateConstraints, resourceRecord, eventRecord),
                eventRegion        = Rectangle.from(element, scheduler.timeAxisSubGridElement);

            super.setupConstraints(
                constrainRectangle,
                eventRegion,
                scheduler.timeAxisViewModel.snapPixelAmount,
                Boolean(dateConstraints.start)
            );
        }

        // Collecting all elements to drag
        assignmentRecords.forEach(assignment => {
            let eventBarEl = scheduler.getElementFromAssignmentRecord(assignment, true);

            if (!eventBarEl) {
                eventBarEl = scheduler.currentOrientation.addTemporaryDragElement(assignment.event, assignment.resource);
            }

            eventBarEls.push(eventBarEl);
        });

        return {
            record          : assignmentRecord,
            draggedEntities : assignmentRecords,
            dateConstraints : dateConstraints?.start ? dateConstraints : null,
            // Create copies of the elements
            eventBarCopies  : eventBarEls.map(el => me.createProxy(el)),
            eventBarEls
        };
    }

    getDateConstraints(resourceRecord, eventRecord) {
        const
            { scheduler }           = this,
            externalDateConstraints = scheduler.getDateConstraints?.(resourceRecord, eventRecord);

        let minDate, maxDate;

        if (this.constrainDragToTimeSlot) {
            minDate = eventRecord.startDate;
            maxDate = eventRecord.endDate;
        }
        else if (externalDateConstraints) {
            minDate = externalDateConstraints.start;
            maxDate = externalDateConstraints.end;
        }

        return {
            start : minDate,
            end   : maxDate
        };
    }

    getConstrainingRectangle(dateRange, resourceRecord, eventRecord) {
        return this.scheduler.getScheduleRegion(this.constrainDragToResource && resourceRecord, eventRecord, true, dateRange && {
            start : dateRange.start, end : dateRange.end
        });
    }

    /**
     * Initializes drag data (dates, constraints, dragged events etc). Called when drag starts.
     * @private
     * @param info
     * @returns {*}
     */
    getDragData(info) {
        const dragData = this.getMinimalDragData(info) || {};

        return {
            ...super.getDragData(info),
            ...dragData,
            initialAssignmentsState : dragData.assignmentRecords.map(assignment => ({
                startDate : assignment.event.startDate,
                resource  : assignment.resource,
                assignment
            }))
        };
    }

    /**
     * Provide your custom implementation of this to allow additional selected records to be dragged together with the original one.
     * @param {Scheduler.model.AssignmentModel} assignmentRecord The assignment about to be dragged
     * @returns {Scheduler.model.AssignmentModel[]} An array of assignment records to drag together with the original
     */
    getRelatedRecords(assignmentRecord) {
        return this.scheduler.selectedAssignments.filter(selectedRecord => selectedRecord !== assignmentRecord && !selectedRecord.resource.readOnly && selectedRecord.event.isDraggable);
    }

    /**
     * Get correct axis coordinate depending on schedulers mode (horizontal -> x, vertical -> y). Also takes milestone
     * layout into account.
     * @private
     * @param {Scheduler.model.EventModel} eventRecord Record being dragged
     * @param {HTMLElement} element Element being dragged
     * @param {Number[]} coord XY coordinates
     * @returns {Number|Number[]} X,Y or XY
     */
    getCoordinate(eventRecord, element, coord) {
        const scheduler = this.currentOverClient;

        if (scheduler.isHorizontal) {
            let x = coord[0];

            // Adjust coordinate for milestones if using a layout mode, since they are aligned differently than events
            if (scheduler.milestoneLayoutMode !== 'default' && eventRecord.isMilestone) {
                switch (scheduler.milestoneAlign) {
                    case 'center':
                        x += element.offsetWidth / 2;
                        break;
                    case 'end':
                        x += element.offsetWidth;
                        break;
                }
            }

            return x;
        }
        else {
            let y = coord[1];
            // Adjust coordinate for milestones if using a layout mode, since they are aligned differently than events
            if (scheduler.milestoneLayoutMode !== 'default' && eventRecord.isMilestone) {
                switch (scheduler.milestoneAlign) {
                    case 'center':
                        y += element.offsetHeight / 2;
                        break;
                    case 'end':
                        y += element.offsetHeight;
                        break;
                }
            }

            return y;
        }
    }

    /**
     * Get resource record occluded by the drag proxy.
     * @private
     * @returns {Scheduler.model.ResourceModel}
     */
    resolveResource() {
        const
            me                 = this,
            client             = me.currentOverClient,
            { isHorizontal }   = client,
            {
                context,
                browserEvent,
                dragProxy
            }                  = me.dragData,
            element            = dragProxy || context.element,
            // Page coords for elementFromPoint
            pageRect           = Rectangle.from(element, null, true),
            y                  = (client.isVertical || me.unifiedDrag) ? context.clientY : pageRect.center.y,
            // Local coords to resolve resource in vertical
            localRect          = Rectangle.from(element, client.timeAxisSubGridElement, true),
            { x : lx, y : ly } = localRect.center,
            eventTarget        = me.getMouseMoveEventTarget(browserEvent);

        let resource = null;

        if (client.element.contains(eventTarget)) {
            // This is benchmarked as the fastest way to find a Grid Row from a viewport Y coordinate
            // so use it in preference to elementFromPoint (which causes a forced synchronous layout) in horizontal mode.
            if (isHorizontal) {
                const row = client.rowManager.getRowAt(y);

                resource = row && client.store.getAt(row.dataIndex);
            }
            else {
                // In vertical mode, just use the X coordinate to find out which resource we are under.
                // The method requires that a .b-sch-timeaxis-cell element be passed.
                // There is only one in vertical mode, so use that.
                resource = client.resolveResourceRecord(client.timeAxisSubGridElement.querySelector('.b-sch-timeaxis-cell'), [lx, ly]);
            }
        }

        return resource;
    }

    //endregion

    //region Other stuff

    adjustStartDate(startDate, timeDiff) {
        const
            scheduler = this.currentOverClient;

        startDate = scheduler.timeAxis.roundDate(new Date(startDate - 0 + timeDiff), scheduler.snapRelativeToEventStartDate ? startDate : false);

        return this.constrainStartDate(startDate);
    }

    getRecordElement(assignmentRecord) {
        return this.client.getElementFromAssignmentRecord(assignmentRecord, true);
    }

    // Used by the Dependencies feature to draw lines to the drag proxy instead of the original event element
    getProxyElement(assignmentRecord) {
        if (this.isDragging) {
            const index = this.dragData.assignmentRecords.indexOf(assignmentRecord);

            if (index >= 0) {
                return this.dragData.proxyElements[index];
            }
        }

        return null;
    }

    //endregion

    //#region Salesforce hooks

    getMouseMoveEventTarget(event) {
        return event.target;
    }

    //#endregion
}

GridFeatureManager.registerFeature(EventDrag, true, 'Scheduler');
GridFeatureManager.registerFeature(EventDrag, false, 'ResourceHistogram');
