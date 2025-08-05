import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DragHelper from '../../Core/helper/DragHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module SchedulerPro/feature/PercentBar
 */

//region Static

function cls(classes) {
    return `b-task-percent-bar${classes[0] ? `-${classes[0]}` : ''}`;
}

//endregion

/**
 * This feature visualizes the {@link SchedulerPro.model.mixin.PercentDoneMixin#field-percentDone percentDone} field as a
 * progress bar on the event elements. Each progress bar also optionally has a drag handle which users can drag can
 * change the value.
 *
 * You can customize data source for the feature with {@link #config-valueField} and {@link #config-displayField} configs.
 *
 * {@inlineexample SchedulerPro/feature/PercentBar.js}
 *
 * ## Restricting resizing for certain tasks
 *
 * You can prevent certain tasks from having their percent done value changed by overriding the
 * {@link Scheduler.model.TimeSpan#function-isEditable} method on your EventModel or TaskModel.
 *
 * ```javascript
 * class MyTaskModel extends TaskModel {
 *     isEditable(field) {
 *         // Add any condition here, `this` refers to the a task instance
 *         return this.field !== 'percentDone' && super.isEditable(field);
 *     }
 * };
 *
 * gantt = new Gantt({
 *     project : {
 *         taskModelClass : MyTaskModel
 *     }
 * });
 * ```
 *
 * This feature is **enabled** by default in Gantt, but **off** by default in Scheduler Pro.
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype percentBar
 * @feature
 * @demo SchedulerPro/percent-done
 */
export default class PercentBar extends TransactionalFeature(InstancePlugin) {

    /**
     * Fired on the owning SchedulerPro when percent bar dragging starts
     * @event percentBarDragStart
     * @on-owner
     * @param {Scheduler.view.TimelineBase} source SchedulerPro or Gantt instance
     * @param {Core.data.Model} taskRecord The task record
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning SchedulerPro when dragging the percent bar
     * @event percentBarDrag
     * @on-owner
     * @param {Scheduler.view.TimelineBase} source SchedulerPro or Gantt instance
     * @param {Core.data.Model} taskRecord The task record
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning SchedulerPro when dropping the percent bar
     * @event percentBarDrop
     * @on-owner
     * @param {Scheduler.view.TimelineBase} source SchedulerPro or Gantt instance
     * @param {Core.data.Model} taskRecord The task record
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning SchedulerPro if a percent bar drag-drop operation is aborted
     * @event percentBarDragAbort
     * @on-owner
     * @param {Scheduler.view.TimelineBase} source SchedulerPro instance
     * @param {Core.data.Model} taskRecord The task record
     * @param {MouseEvent} domEvent Browser event
     */

    //region Config

    static get $name() {
        return 'PercentBar';
    }

    static get configurable() {
        return {
            /**
             * `true` to allow drag drop resizing to set the % done
             * @config {Boolean}
             * @default
             */
            allowResize : true,

            /**
             * `true` to show a small % done label within the event while drag changing its value
             * @config {Boolean}
             * @default
             */
            showPercentage : true,

            /**
             * By default, the underlying task record is updated live as the user drags the handle. Set to false
             * to only update the record upon drop.
             * @config {Boolean}
             * @default
             */
            instantUpdate : true,

            /**
             * Field name to use as the data source
             * @config {String}
             * @default
             */
            valueField : 'percentDone',

            /**
             * Field name to use to display the value
             * @config {String}
             * @default
             */
            displayField : 'renderedPercentDone'
        };
    }

    static get pluginConfig() {
        return {
            chain : [
                'onPaint',
                {
                    fn   : 'onTaskDataGenerated',
                    // make sure the function runs after other onTaskDataGenerated chains
                    prio : -10000
                },
                {
                    fn   : 'onEventDataGenerated',
                    // make sure the function runs after other onEventDataGenerated chains
                    prio : -10000
                }
            ]
        };
    }

    //endregion

    //region Init

    /**
     * Called when scheduler is painted. Sets up drag and drop and hover tooltip.
     * @private
     */
    onPaint({ firstPaint }) {
        if (firstPaint) {
            const
                me         = this,
                { client } = me;

            me.drag = new DragHelper({
                name              : 'percentBarHandle',
                lockX             : client.isVertical,
                lockY             : client.isHorizontal,
                // Handle is not draggable for parents
                targetSelector    : `${client.eventSelector} .b-task-percent-bar-handle`,
                dragThreshold     : 1,
                outerElement      : client.timeAxisSubGridElement,
                internalListeners : {
                    beforeDragStart : 'onBeforeDragStart',
                    dragStart       : 'onDragStart',
                    drag            : 'onDrag',
                    drop            : 'onDrop',
                    abort           : 'onDragAbort',
                    thisObj         : me
                }
            });

            me.detachListeners('view');

            me.client.ion({
                name                                       : 'view',
                [`${client.scheduledEventName}mouseenter`] : 'onTimeSpanMouseEnter',
                [`${client.scheduledEventName}mouseleave`] : 'onTimeSpanMouseLeave',
                thisObj                                    : me
            });
        }
    }

    get sizeProp() {
        return this.client.isVertical ? 'height' : 'width';
    }

    get offsetSizeProp() {
        return this.client.isVertical ? 'offsetHeight' : 'offsetWidth';
    }

    get positionProp() {
        return this.client.isVertical ? 'top' : (this.client.rtl ? 'right' : 'left');
    }

    get offsetPositionProp() {
        return this.client.isVertical ? 'offsetTop' : (this.client.rtl ? 'offsetRight' : 'offsetLeft');
    }

    updateAllowResize(value) {
        this.client.element.classList.toggle(cls`drag-disabled`, !value);
    }

    updateShowPercentage(value) {
        this.client.element.classList.toggle(cls`show-percentage`, Boolean(value));
    }

    doDestroy() {
        this.drag?.destroy();
        super.doDestroy();
    }

    doDisable(disable) {
        // Redraw to toggle percent bars
        if (this.client.isPainted) {
            this.client.refresh();
        }

        super.doDisable(disable);
    }

    //endregion

    //region Contents

    reset(context) {
        const
            me          = this,
            { client }  = me,
            { project } = client;

        client.element.classList.remove(cls`resizing-task`);

        // Remove handle if operation ended outside the event
        if (!me.isMouseInsideEvent) {
            me.handle.remove();
            me.handle = null;
        }

        if (client.transactionalFeaturesEnabled) {
            if (context.valid) {
                me.finishFeatureTransaction();
            }
            else {
                me.rejectFeatureTransaction();
            }
        }
        else {
            // If we were applying percentDone values while dragging
            if (me.instantUpdate) {
                const { stm } = project;

                // If we have active STM - restore its state we changed before the dragging
                if (!stm.disabled) {
                    // If we started a special STM transaction
                    if (me._stmTransactionStarted) {
                        // finish it or reject depending if the dragging succeeded or was aborted respectively
                        if (context.valid) {
                            stm.stopTransaction();
                        }
                        else {
                            stm.rejectTransaction();
                        }
                    }

                    stm.autoRecord = me.oldStmAutoRecord;
                    me._stmTransactionStarted = false;
                }
            }
        }

        // If we were applying percentDone values while dragging
        if (me.instantUpdate) {
            project.resumeAutoSync();
            client.eventStore.resumeAutoCommit();
        }
    }

    getPercentBarDOMConfig(taskRecord) {
        return {
            className : cls`outer`,
            dataset   : {
                taskBarFeature : 'percentBar'
            },
            children : [
                {
                    className : cls``,
                    dataset   : {
                        percent : taskRecord.getValue(this.displayField)
                    },
                    style : {
                        [this.sizeProp] : taskRecord.getValue(this.valueField) + '%'
                    }
                }
            ]
        };
    }

    appendDOMConfig(taskRecord, children, renderData) {
        if ((taskRecord.isEvent || taskRecord.isTask) && !taskRecord.isMilestone && !this.disabled) {
            const { eventSegments } = this.client.features;
            // If the event is segmented and we have segments rendering feature onboard
            // - draw a percent bar for each segment
            if (taskRecord.isSegmented && eventSegments?.enabled) {
                taskRecord.segments.forEach((segmentRecord, index) => {
                    renderData.segmentsDOMConfig[index].children.unshift(this.getPercentBarDOMConfig(segmentRecord));
                });
            }
            else {
                children.unshift(this.getPercentBarDOMConfig(taskRecord));
            }
        }
    }

    // For Scheduler Pro
    onEventDataGenerated(eventData) {
        this.appendDOMConfig(eventData.eventRecord, eventData.children, eventData);
    }

    // For Gantt
    onTaskDataGenerated(taskData) {
        this.appendDOMConfig(taskData.taskRecord, taskData.children, taskData);
    }

    //endregion

    //region Events

    getHoverSegment(event) {
        const segmentElement = (event.toElement || event.target).closest('.b-sch-event-segment');

        if (segmentElement) {
            const segmentBox = Rectangle.from(segmentElement);

            if (segmentBox?.contains(EventHelper.getPagePoint(event))) {
                return segmentElement;
            }
        }
    }

    isOverSegment(event) {
        return Boolean(this.getHoverSegment(event));
    }

    // Inject handle on mouse over
    onTimeSpanMouseEnter(event) {
        const
            me                   = this,
            { client, sizeProp } = me,
            record               = event[`${client.scheduledEventName}Record`];

        if (record.isMilestone || record.readOnly || me.disabled || record.isParent || !record.isEditable(me.valueField)) {
            return;
        }

        // No ongoing drag
        if (!me.drag.context) {
            const
                element = event[`${client.scheduledEventName}Element`],
                parent  = DomSync.getChild(element, client.scheduledEventName);

            // Add handle if not already there
            if (!me.handle) {
                const dataset = {
                    percent : record.getValue(me.valueField)
                };

                let pos = record.getValue(me.valueField) + '%';

                const { eventSegments } = client.features;

                if (record.isSegmented && eventSegments?.enabled) {

                    dataset.segment = 0;

                    const parentBox = Rectangle.from(parent);

                    let lastPercentDone, lastPercentBarBox;

                    // iterate segments to find the handle position
                    record.segments.some((segment, index) => {
                        // exit - if this segment is not started
                        // unless previous segment is complete - then we place the handle
                        // at the start of the next not-started segment
                        if (!segment[me.valueField] && lastPercentDone !== 100) {
                            return true;
                        }

                        lastPercentDone = segment[me.valueField];

                        dataset.segment = index;

                        const segmentPercentBarElement = DomSync.getChild(parent, `segments.${index}.percentBar`).firstChild;

                        lastPercentBarBox = Rectangle.from(segmentPercentBarElement);
                    });

                    if (client.isVertical) {
                        pos = lastPercentBarBox ? lastPercentBarBox.top + lastPercentBarBox[sizeProp] - parentBox.top : 0;
                    }
                    else if (client.rtl) {

                        pos = lastPercentBarBox ? lastPercentBarBox.right + lastPercentBarBox[sizeProp] - parentBox.right : 0;
                    }
                    else {
                        pos = lastPercentBarBox ? lastPercentBarBox.left + lastPercentBarBox[sizeProp] - parentBox.left : 0;
                    }
                }

                me.handle = DomHelper.createElement({
                    parent,
                    className : cls`handle`,
                    style     : {
                        [me.positionProp] : pos
                    },
                    dataset
                });
            }

            // Mouse is inside event, used later to not remove handle
            me.isMouseInsideEvent = true;
        }
        // Ongoing drag, mouse coming back into active event
        else if (record === me.drag.context.taskRecord) {
            // Mouse is inside event, used later to not remove handle
            me.isMouseInsideEvent = true;
        }
    }

    // Remove handle on mouse leave, if not dragging
    onTimeSpanMouseLeave(event) {
        const me = this;

        if (!me.drag.context && me.handle && event.event.toElement !== me.handle) {
            me.handle.remove();
            me.handle = null;
        }

        me.isMouseInsideEvent = false;
    }

    onBeforeDragStart({ source, context, event }) {
        const
            { client, offsetPositionProp, offsetSizeProp } = this,
            { eventSegments }                              = client.features,
            { element }                                    = context,
            taskRecord                                     = client.resolveEventRecord(element);

        let percentBarOuter, percentBar, initialPos, size;

        // if the event is segmented
        if (taskRecord.isSegmented && eventSegments?.enabled) {
            const segmentIndex = element.dataset.segment;

            percentBarOuter = DomSync.getChild(element.parentElement, `segments.${segmentIndex}.percentBar`);
            percentBar      = percentBarOuter.firstElementChild;

            // total size allowed to drag in is defined by the event borders
            size       = element.parentElement[offsetSizeProp];
            initialPos = DomSync.getChild(element.parentElement, `segments.${segmentIndex}`)[offsetPositionProp] + percentBar[offsetSizeProp];
        }
        else {
            percentBarOuter = DomSync.getChild(element.parentElement, 'percentBar');
            percentBar      = percentBarOuter.firstElementChild;

            size       = percentBarOuter[offsetSizeProp];
            initialPos = percentBar[offsetSizeProp];
        }

        if (client.isVertical) {
            source.minY = -initialPos;
            source.maxY = size - initialPos;
        }
        else if (client.rtl) {
            source.maxX = initialPos;
            source.minX = -(size - initialPos);
        }
        else {
            source.minX = -initialPos;
            source.maxX = size - initialPos;
        }

        Object.assign(context, {
            percentBar,
            initialPos,
            size,
            taskRecord,
            initialValue : taskRecord.getValue(this.valueField),
            domEvent     : event
        });
    }

    async onDragStart({ context, event }) {
        const
            me         = this,
            { client } = me,
            { project } = client;

        client.element.classList.add(cls`resizing-task`);

        context.element.retainElement = true;
        context.domEvent              = event;
        client.trigger('percentBarDragStart', context);

        if (client.transactionalFeaturesEnabled) {
            await me.startFeatureTransaction();
        }
        else {
            // If we have STM enabled - turn its auto recording off
            if (!project.stm.disabled) {
                this.oldStmAutoRecord = project.stm.autoRecord;
                project.stm.autoRecord = false;
            }
        }

        if (me.instantUpdate) {
            project.suspendAutoSync();
            client.eventStore.suspendAutoCommit();
        }
    }

    onDrag({ context, event }) {
        const
            me                                   = this,
            { sizeProp, offsetSizeProp, client } = me,
            percent                              = client.isHorizontal ? Math.round(((context.initialPos + (context.newX * (client.rtl ? -1 : 1))) / context.size) * 100)
                : Math.round(((context.initialPos + context.newY) / context.size) * 100),
            { eventSegments }                    = client.features;

        if (me.instantUpdate) {
            if (!client.transactionalFeaturesEnabled) {
                const { stm } = client.project;

                // If STM is active but we have not transaction started
                if (!stm.disabled && !stm.transaction) {
                    // start a new transaction
                    stm.startTransaction();
                    me._stmTransactionStarted = true;
                }
            }

            context.taskRecord.set(me.valueField, percent);
        }
        context.domEvent = event;

        context.percent = context.element.dataset.percent = percent;

        if (context.taskRecord.isSegmented && eventSegments?.enabled) {
            context.taskRecord.segments.forEach((segment, index) => {
                const
                    percentBar     = DomSync.getChild(context.element.parentElement, `segments.${index}.percentBar`).firstChild,
                    segmentElement = DomSync.getChild(context.element.parentElement, `segments.${index}`);

                // complete segment
                if (context.percent >= segment.endPercentDone) {
                    percentBar.style[sizeProp] = segmentElement[offsetSizeProp] + 'px';
                }
                // not started segment
                else if (context.percent <= segment.startPercentDone) {
                    percentBar.style[sizeProp] = '0';
                }
                // the segment in progress
                else if (context.percent >= segment.startPercentDone && context.percent <= segment.endPercentDone) {
                    const percentsInSegment = segment.endPercentDone - segment.startPercentDone;

                    // new percent bar size is: number_of_percents * pixels_per_percent
                    percentBar.style[sizeProp] = (percent - segment.startPercentDone) * segmentElement[offsetSizeProp] / percentsInSegment + 'px';
                }
            });
        }

        client.trigger('percentBarDrag', context);
    }

    onDragAbort({ context, event }) {
        // Reset percentBar size on abort
        if (context.taskRecord.isSegmented) {
            context.percentBar.style[this.sizeProp] = context.taskRecord.segments[context.element.dataset.segment][this.valueField] + '%';
        }

        context.taskRecord.set(this.valueField, context.initialValue);
        context.domEvent = event;

        this.reset(context);

        this.client.trigger('percentBarDragAbort', context);
    }

    onDrop({ context }) {
        const
            me                = this,
            { taskRecord }    = context,
            { eventSegments } = me.client.features;

        taskRecord.set(me.valueField, context.percent);

        // for segmented task we need to calculate the new position
        // of the handle
        if (taskRecord.isSegmented && eventSegments?.enabled) {
            taskRecord.segments.some((segment, index) => {
                // find the segment in progress
                if (context.percent >= segment.startPercentDone && context.percent <= segment.endPercentDone) {
                    const
                        percentsInSegment = segment.endPercentDone - segment.startPercentDone,
                        segmentElement    = DomSync.getChild(context.element.parentElement, `segments.${index}`),
                        // new coordinate is calculated roughly as:
                        // segment xy-coordinate + number of percents * pixels per percent
                        size              = parseInt(segmentElement.style[me.sizeProp]) +
                            (context.percent - segment.startPercentDone) * segmentElement[me.offsetSizeProp] / percentsInSegment;

                    // Fully overwrite handle style to get rid of translate also
                    context.element.style.cssText = `${me.positionProp}: ${size}px`;

                    // patch the handle segment index too
                    context.element.dataset.segment = index;

                    return true;
                }
            });
        }
        else {
            // Fully overwrite handle style to get rid of translate also
            context.element.style.cssText = `${me.positionProp}: ${me.client.rtl ? 100 - context.percent : context.percent}%`;
        }

        me.reset(context);

        me.client.trigger('percentBarDrop', context);
    }

    //endregion

    // No classname on Scheduler's/Gantt's element
    get featureClass() {}
}

GridFeatureManager.registerFeature(PercentBar, false, 'SchedulerPro');
GridFeatureManager.registerFeature(PercentBar, true, 'Gantt');
