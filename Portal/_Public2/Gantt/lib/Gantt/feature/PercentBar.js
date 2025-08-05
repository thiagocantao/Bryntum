import InstancePlugin from '../../Common/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ResizeHelper from '../../Common/helper/ResizeHelper.js';
import DomHelper from '../../Common/helper/DomHelper.js';

/**
 * @module Gantt/feature/PercentBar
 */

/**
 * This feature renders a special drag handler in every event, by dragging which, user can change the
 * {@link Gantt.model.TaskModel#field-percentDone percentDone} field.
 *
 * This feature is **enabled** by default
 *
 * {@inlineexample gantt/feature/PercentBar.js}
 *
 * @extends Common/mixin/InstancePlugin
 */
export default class PercentBar extends InstancePlugin {
    //region Config

    static get defaultConfig() {
        return {
            allowDrag : true
        };
    }

    static get pluginConfig() {
        return {
            chain : ['render', 'onTaskDataGenerated']
        };
    }

    //endregion

    //region Init

    /**
     * Called when scheduler is rendered. Sets up drag and drop and hover tooltip.
     * @private
     */
    render() {
        const me    = this,
            gantt = me.client;

        if (me.resize) {
            me.resize.destroy();
        }

        me.resize = new ResizeHelper({
            name           : 'percentBarResize',
            outerElement   : gantt.timeAxisSubGridElement,
            targetSelector : '.b-gantt-task-percent',
            handleSelector : '.b-gantt-task-percent-handle',
            allowResize    : me.isResizable.bind(me),
            dragThreshold  : 0,

            listeners : {
                resizeStart : me.onResizeStart,
                resizing    : me.onResizing,
                resize      : me.onFinishResize,
                cancel      : me.onCancelResize,
                thisObj     : me
            }
        });
    }

    doDestroy() {
        this.resize && this.resize.destroy();
        super.doDestroy();
    }

    //endregion

    //region Other

    isResizable(el) {
        // cannot change percent of parents, calculated from children
        return !el.closest('.b-gantt-task-parent');
    }

    //endregion

    //region Contents

    cleanup(context) {
        const gantt  = this.client,
            taskEl = context.element.closest(gantt.eventSelector);

        taskEl.classList.remove('b-gantt-task-percent-resizing');
        gantt.element.classList.remove('b-gantt-resizing-task-percent');
    }

    onTaskDataGenerated(taskData) {
        const { task, row } = taskData;

        if (!task.milestone) {
            // TODO: When rendering pipeline is refactored to have element available before rendering this can change to
            //  attach to that element instead.
            if (!row.percentBarElement) {
                row.percentBarElement = DomHelper.createElement({
                    className : 'b-gantt-task-percent',
                    children  : [
                        {
                            className : 'b-gantt-task-percent-handle'
                        }
                    ]
                });
            }

            taskData.body.insertBefore(row.percentBarElement, taskData.body.firstChild);
            row.percentBarElement.dataset.percent = Math.round(task.percentDone);
            row.percentBarElement.style.width = task.percentDone + '%';

            // TODO: Make animateable. Should not replace it on every update
            //taskData.body.insertBefore(DomHelper.createElementFromTemplate(`<div class="b-gantt-task-percent" style="width:${task.percentDone}%" data-percent="${task.percentDone}"><div class="b-gantt-task-percent-handle"></div></div>`), taskData.body.firstChild);
        }
    }

    //endregion

    //region Events

    onResizeStart({ context }) {
        const taskEl = context.element.closest(this.client.eventSelector);

        taskEl.classList.add('b-gantt-task-percent-resizing');
        this.client.element.classList.add('b-gantt-resizing-task-percent');
    }

    onResizing({ context }) {
        const el     = context.element,
            taskEl = el.closest(this.client.eventSelector),
            width = el.offsetWidth === 1 ? 0 : el.offsetWidth;

        el.dataset.percent = Math.min(100, Math.round(100 * width / taskEl.offsetWidth));
    }

    onFinishResize({ context }) {
        const me         = this,
            gantt      = me.client,
            el         = context.element,
            taskRecord = gantt.resolveTaskRecord(el);

        me.cleanup(context);

        // dont want to redraw, ui is already in correct state
        // gantt.suspendStoreRedraw = true;
        taskRecord.setPercentDone(parseInt(el.dataset.percent));
        // gantt.suspendStoreRedraw = false;

        //el.classList.add('b-percent-resize-done');
    }

    onCancelResize({ context }) {
        this.cleanup(context);
    }

    //endregion
}

GridFeatureManager.registerFeature(PercentBar, true, 'Gantt');
