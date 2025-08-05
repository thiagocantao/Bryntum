import DragBase from '../../Scheduler/feature/base/DragBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module Gantt/feature/TaskDrag
 */

/**
 * @typedef ValidationMessage
 * @property {Boolean} valid `true` for valid, `false` for invalid
 * @property {String} message Validation message
 */

/**
 * Allows user to drag and drop tasks within Gantt, to change their start date.
 *
 * ## Constraining the drag drop area
 *
 * You can constrain how the dragged task is allowed to move by using {@link Gantt.view.Gantt#config-getDateConstraints}.
 * This method is configured on the Gantt instance and lets you define the date range for the dragged task programmatically.
 *
 * ## Drag drop tasks from outside
 *
 * Dragging unplanned tasks from an external grid is a very popular use case. Please refer to the [Drag from grid demo](../examples/drag-from-grid)
 * and study the [Drag from grid guide](#Gantt/guides/dragdrop/drag_tasks_from_grid.md) to learn more.
 *
 * ## Validating a drag drop operation
 *
 * It is easy to programmatically decide what is a valid drag drop operation. Use the {@link #config-validatorFn}
 * and return either `true` / `false` (optionally a message to show to the user).
 *
 * ```javascript
 * features : {
 *     taskDrag : {
 *        validatorFn(draggedTaskRecords, newStartDate) {
 *            const valid = Date.now() >= newStartDate;
 *
 *            return {
 *                valid,
 *                message : valid ? '' : 'Not allow to drag a task into the past'
 *            };
 *        }
 *     }
 * }
 * ```
 *
 * If you instead want to do a single validation upon drop, you can listen to {@link #event-beforeTaskDropFinalize}
 * and set the `valid` flag on the context object provided.
 *
 * ```javascript
 * const gantt = new Gantt({
 *     listeners : {
 *         beforeTaskDropFinalize({ context }) {
 *             const { taskRecords } = context;
 *             // Don't allow dropping a task in the past
 *             context.valid = Date.now() <= eventRecords[0].startDate;
 *         }
 *     }
 * });
 * ```
 *
 * ## Preventing drag of certain tasks
 *
 * To prevent certain tasks from being dragged, you have two options. You can set {@link Gantt.model.TaskModel#field-draggable}
 * to `false` in your data, or you can listen for the {@link Gantt.view.Gantt#event-beforeTaskDrag} event and
 * return `false` to block the drag.
 *
 * ```javascript
 * new Gantt({
 *     listeners : {
 *         beforeTaskDrag({ taskRecord }) {
 *             // Only allow dragging tasks that has not started
 *             return taskRecord.percentDone === 0;
 *         }
 *     }
 * })
 * ```
 *
 * ## Customizing the drag drop tooltip
 *
 * To show custom HTML in the tooltip, please see the {@link #config-tooltipTemplate} config. Example:
 *
 * ```javascript
 * features: {
 *     taskDrag: {
 *         // A minimal start date tooltip
 *         tooltipTemplate : ({ taskRecord, startDate }) => {
 *             return DateHelper.format(startDate, 'HH:mm');
 *         }
 *     }
 * }
 * ```
 *
 * This feature is **enabled** by default
 *
 * @extends Scheduler/feature/base/DragBase
 * @demo Gantt/basic
 * @classtype taskDrag
 * @feature
 */
export default class TaskDrag extends TransactionalFeature(DragBase) {
    //region Config

    static get $name() {
        return 'TaskDrag';
    }

    static get configurable() {
        return {
            /**
             * An empty function by default, but provided so that you can perform custom validation on
             * the item being dragged. This function is called during the drag and drop process and also after the drop is made.
             * Return true if the new position is valid, false to prevent the drag.
             * @param {Gantt.model.TaskModel[]} taskRecords An array of tasks being dragged
             * @param {Date} startDate The new start date
             * @param {Number} duration The duration of the item being dragged
             * @param {Event} event The event object
             * @returns {Boolean|ValidationMessage} `true` if this validation passes, `false` if it does not.
             *
             * Or an object with 2 properties: `valid` -  Boolean `true`/`false` depending on validity,
             * and `message` - String with a custom error message to display when invalid.
             * @config {Function}
             */
            validatorFn : (taskRecords, startDate, duration, event) => true,

            /**
             * `this` reference for the validatorFn
             * @config {Object}
             */
            validatorFnThisObj : null,

            /**
             * Gets or sets special key to activate successor pinning behavior. Supported values are:
             * * 'ctrl'
             * * 'shift'
             * * 'alt'
             * * 'meta'
             *
             * Assign false to disable it.
             * @member {Boolean|String} pinSuccessors
             */
            /**
             * Set to true to enable dragging task while pinning dependent tasks. By default, this behavior is activated
             * if you hold CTRL key during drag. Alternatively, you may provide key name to use. Supported values are:
             * * 'ctrl'
             * * 'shift'
             * * 'alt'
             * * 'meta'
             *
             * **Note**: Only supported in forward-scheduled project
             *
             * @config {Boolean|String}
             * @default
             */
            pinSuccessors : false,

            tooltipCls : 'b-gantt-taskdrag-tooltip',

            capitalizedEventName : null
        };
    }

    afterConstruct() {
        this.capitalizedEventName = this.capitalizedEventName || this.client.capitalizedEventName;
        super.afterConstruct(...arguments);
    }

    changePinSuccessors(value) {
        return EventHelper.toSpecialKey(value);
    }

    /**
     * Template used to generate drag tooltip contents.
     * ```javascript
     * const gantt = new Gantt({
     *     features : {
     *         taskDrag : {
     *             tooltipTemplate({taskRecord, startText}) {
     *                 return `${taskRecord.name}: ${startText}`
     *             }
     *         }
     *     }
     * });
     * ```
     * @config {Function} tooltipTemplate
     * @param {Object} data Tooltip data
     * @param {Gantt.model.TaskModel} data.taskRecord
     * @param {Boolean} data.valid Currently over a valid drop target or not
     * @param {Date} data.startDate New start date
     * @param {Date} data.endDate New end date
     * @returns {String}
     */

    //endregion

    //region Events

    /**
     * Fires on the owning Gantt before task dragging starts. Return false to prevent the action.
     * @event beforeTaskDrag
     * @preventable
     * @on-owner
     * @param {Gantt.view.Gantt} source
     * @param {Gantt.model.TaskModel} taskRecord
     * @param {Event} event The native browser event
     */

    /**
     * Fires on the owning Gantt when task dragging starts
     * @event taskDragStart
     * @on-owner
     * @param {Gantt.view.Gantt} source
     * @param {Gantt.model.TaskModel[]} taskRecords
     */

    /**
     * Fires on the owning Gantt while a task is being dragged
     * @event taskDrag
     * @on-owner
     * @param {Gantt.view.Gantt} source
     * @param {Gantt.model.TaskModel[]} taskRecords
     * @param {Date} startDate
     * @param {Date} endDate
     * @param {Object} dragData
     * @param {Boolean} changed `true` if startDate has changed.
     */

    /**
     * Fires on the owning Gantt to allow implementer to prevent immediate finalization by setting `data.context.async = true`
     * in the listener, to show a confirmation popup etc
     * ```javascript
     * scheduler.on('beforetaskdropfinalize', ({ context }) => {
     *     context.async = true;
     *     setTimeout(() => {
     *         // async code don't forget to call finalize
     *         context.finalize();
     *     }, 1000);
     * })
     * ```
     * @event beforeTaskDropFinalize
     * @on-owner
     * @param {Gantt.view.Gantt} source Gantt instance
     * @param {Object} context
     * @param {Gantt.model.TaskModel[]} context.taskRecords The dragged task records
     * @param {Boolean} context.valid Set this to `false` to mark the drop as invalid
     * @param {Boolean} context.async Set true to handle dragdrop asynchronously (e.g. to wait for user
     * confirmation)
     * @param {Function} context.finalize Call this method to finalize dragdrop. This method accepts one
     * argument: pass true to update records, or false, to ignore changes
     */

    /**
     * Fires on the owning Gantt after a valid task drop
     * @event taskDrop
     * @on-owner
     * @param {Gantt.view.Gantt} source
     * @param {Gantt.model.TaskModel[]} taskRecords
     * @param {Boolean} isCopy
     */

    /**
     * Fires on the owning Gantt after a task drop, regardless if the drop validity
     * @event afterTaskDrop
     * @on-owner
     * @param {Gantt.view.Gantt} source
     * @param {Gantt.model.TaskModel[]} taskRecords
     * @param {Boolean} valid
     */

    //endregion

    //region Init

    construct(gantt, config) {
        this.gantt = gantt;

        super.construct(gantt, config);
    }

    get store() {
        return this.gantt.store;
    }

    //endregion

    //region Drag events

    getDraggableElement(el) {
        return el?.closest(this.drag.targetSelector);
    }

    resolveEventRecord(eventElement, client = this.client) {
        return client.resolveTaskRecord(eventElement);
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
        // if (el.closest(gantt.eventSelector).matches('[class$="-handle"]')) {
        if (el.matches('[class$="-handle"]')) {
            return false;
        }

        const eventRecord = me.resolveEventRecord(eventElement, client);

        // Tasks not part of project are transient tasks in a display store, which are not meant to be manipulated
        if (!eventRecord || !eventRecord.isDraggable || eventRecord.readOnly || !eventRecord.project) {
            return false;
        }

        // Hook for features that need to prevent drag
        const prevented = client[`is${me.capitalizedEventName}ElementDraggable`]?.(
            eventElement, eventRecord, el, event
        ) === false;

        return !prevented;
    }

    triggerBeforeEventDrag(eventType, event) {
        return this.client.trigger(eventType, event);
    }

    triggerEventDrag(dragData, start) {
        // Trigger the event on every mousemove so that features which need to adjust
        // Such as dependencies and baselines can keep adjusted.
        this.client.trigger('taskDrag', {
            taskRecords : dragData.draggedEntities,
            startDate   : dragData.startDate,
            endDate     : dragData.endDate,
            dragData,
            changed     : dragData.startDate - start !== 0
        });
    }

    triggerDragStart(dragData) {
        this.client.trigger('taskDragStart', {
            taskRecords : dragData.draggedEntities,
            dragData
        });
        return this.startFeatureTransaction();
    }

    triggerDragAbort(dragData) {
        this.client.trigger('taskDragAbort', {
            taskRecords : dragData.draggedEntities,
            context     : dragData
        });
    }

    triggerDragAbortFinalized(dragData) {
        this.rejectFeatureTransaction();
        this.client.trigger('taskDragAbortFinalized', {
            taskRecords : dragData.draggedEntities,
            context     : dragData
        });
    }

    triggerAfterDrop(dragData, valid) {
        this.finishFeatureTransaction();
        this.currentOverClient.trigger('afterTaskDrop', {
            taskRecords : dragData.draggedEntities,
            context     : dragData,
            valid
        });
    }

    //endregion

    //region Drag data

    getProductDragContext(dd) {
        return {
            valid : true
        };
    }

    getMinimalDragData(info) {
        const
            element    = this.getElementFromContext(info),
            taskRecord = this.client.resolveTaskRecord(element);

        return { taskRecord };
    }

    getTaskScheduleRegion(taskRecord, dateConstraints) {
        return this.client.getScheduleRegion(taskRecord, true, dateConstraints);
    }

    getDateConstraints(taskRecord) {
        return this.client.getDateConstraints?.(taskRecord);
    }

    setupProductDragData(context) {
        // debugger
        const
            me              = this,
            { client }      = me,
            element         = context.element,
            taskRecord      = client.resolveTaskRecord(element),
            taskRegion      = Rectangle.from(element),
            relatedRecords  = me.getRelatedRecords(taskRecord) || [],
            dateConstraints = me.getDateConstraints(taskRecord),
            eventBarEls     = [element],
            scheduleRegion  = me.getTaskScheduleRegion(taskRecord, dateConstraints);

        me.setupConstraints(
            scheduleRegion,
            taskRegion,
            client.timeAxisViewModel.snapPixelAmount,
            Boolean(dateConstraints)
        );

        // Collecting additional elements to drag
        relatedRecords.forEach(r => {
            ArrayHelper.include(eventBarEls, client.getElementFromTaskRecord(r, false));
        });

        const draggedEntities = [taskRecord, ...relatedRecords];

        return { record : taskRecord, dateConstraints, eventBarEls, draggedEntities, taskRecords : draggedEntities };
    }

    /**
     * Get correct axis coordinate.
     * @private
     * @param {Gantt.model.TaskModel} taskRecord Record being dragged
     * @param {HTMLElement} element Element being dragged
     * @param {Number[]} coord XY coordinates
     * @returns {Number|Number[]} X,Y or XY
     */
    getCoordinate(taskRecord, element, coord) {
        return coord[0];
    }

    //endregion

    //region Finalize & validation

    // Called from EventDragBase to assert if a drag is valid or not
    checkDragValidity(dragData, event) {
        return this.validatorFn.call(this.validatorFnThisObj || this,
            dragData.draggedEntities,
            dragData.startDate,
            dragData.duration,
            event
        );
    }

    /**
     * Checks if a task can be dropped on the specified location
     * @private
     * @returns {Boolean} Valid (true) or invalid (false)
     */
    isValidDrop(dragData) {
        return true;
    }

    /**
     * Update tasks being dragged.
     * @private
     * @param {Object} context Drag data.
     */
    async updateRecords(context) {
        const
            {
                startDate,
                browserEvent,
                draggedEntities : [taskRecord]
            }                = context,
            oldStartDate     = taskRecord.startDate;

        if (this.pinSuccessors && browserEvent[this.pinSuccessors]) {
            await taskRecord.moveTaskPinningSuccessors(startDate);
        }
        else {
            await taskRecord.setStartDate(startDate, true);
        }

        // If not rejected (the startDate has changed), tell the world there was a successful drop.
        if (taskRecord.startDate - oldStartDate) {
            this.client.trigger('taskDrop', {
                taskRecords : context.draggedEntities
            });
        }
        else {
            this.dragData.valid = false;
        }
    }

    getRecordElement(task) {
        return this.client.getElementFromTaskRecord(task, true);
    }

    get tipId() {
        return `${this.client.id}-task-drag-tip`;
    }

    //endregion
}

GridFeatureManager.registerFeature(TaskDrag, true, 'Gantt');
