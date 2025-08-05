import BaseHorizontalMapper from '../../../Scheduler/view/orientation/BaseHorizontalMapper.js';
import TaskCache from './TaskCache.js';
import DomHelper from '../../../Common/helper/DomHelper.js';
import DomClassList from '../../../Common/helper/util/DomClassList.js';

/**
 * @module Gantt/view/orientation/TaskRendering
 */

/**
 * Handles rendering of tasks. The need to interact with this class should be
 * minimal, most functions are called from Gantt or its mixins.
 * @private
 */
export default class TaskRendering extends BaseHorizontalMapper {
    //region Init

    construct(gantt) {
        const me = this;

        me.gantt = gantt;

        super.construct(gantt);

        me.cache = new TaskCache(this);
    }

    bindTaskStore(taskStore) {
        const me = this;

        me.taskStoreDetacher && me.taskStoreDetacher();

        // using direct events that clear tasks cache before normal rendering process starts
        me.taskStoreDetacher = taskStore.on({
            thisObj   : me,
            add       : me.onTaskStoreAdd,
            remove    : me.onTaskStoreRemove,
            update    : me.onTaskStoreUpdate,
            idChange  : me.onTaskStoreIdChange,
            refresh   : me.onTaskStoreRefresh,
            removeall : me.onTaskStoreRefresh,
            prio      : 1
        });
    }

    //endregion

    //region Store events

    clearCacheBelowTask(record) {
        const
            { gantt } = this,
            { taskStore } = gantt;

        let startRow = gantt.getRowFor(record);

        // Record may be:
        // - rendered
        // - appended but not rendered
        // - removed
        // Here iterate over tasks, starting from passed one, looking for first rendered task
        while (!startRow && (record = taskStore.getNext(record))) {
            startRow = gantt.getRowFor(record);
        }

        // There still might not be startRow if we append record to the end
        if (startRow) {
            while (record && gantt.getRowFor(record)) {
                this.cache.clearRow(record.id);
                record = taskStore.getNext(record);
            }
        }
    }

    onTaskStoreAdd({ records, isExpand, isChild }) {
        if (!isExpand && !(isChild && !records[0].parent.isExpanded(this.gantt.taskStore))) {
            this.clearCacheBelowTask(records[0]);
        }
    }

    onTaskStoreIdChange({ oldValue, value }) {
        this.cache.changeRowId(oldValue, value);
    }

    onTaskStoreUpdate({ record }) {
        const me = this,
            { id } = record;

        if (me.cache.getRow(id)) {
            const
                eventLayoutData = me.getTimeSpanRenderData(record, record),
                isVisible = Boolean(eventLayoutData && me.isEventInView(eventLayoutData));

            me.gantt.runWithTransition(() => me.cache.clearRow(id, false, isVisible), me.gantt.transitionDuration);
        }
    }

    onTaskStoreRemove({ allRecords : records, isCollapse, index }) {
        if (!isCollapse && index !== -1) {
            records.forEach(taskRecord => {
                this.cache.clearRow(taskRecord.id, true);
            });
            const task = this.gantt.taskStore.getAt(Math.max(index, 0));
            task && this.clearCacheBelowTask(task);
        }
    }

    onTaskStoreRefresh() {
        this.cache.clear();
    }

    // private
    onTranslateRow({ source : row }) {
        const taskId = row.id;
        if (taskId) {
            const me      = this,
                taskCache = me.cache.getRow(taskId);

            if (taskCache && taskCache.rowTop !== row.top) {
                const deltaY   = row.top - taskCache.rowTop,
                    taskLayout = taskCache.layoutCache;

                taskLayout.top += deltaY;
                taskLayout.bottom += deltaY;
                if (taskLayout.div) me.positionEvent(taskLayout.div, taskLayout.start, taskLayout.top);

                taskCache.top += deltaY;
                taskCache.rowTop = row.top;
            }
        }
    }

    //endregion

    //region Layout & render events

    /**
     * Converts a start/endDate into a MS value used when rendering the event.
     * @private
     * @param {Gantt.model.TaskModel} taskRecord
     * @returns {Object} Object of format { startMS, endMS, durationMS }
     */
    calculateMS(taskRecord) {
        const startMS    = taskRecord.startDate.getTime(),
            endMS      = taskRecord.endDate.getTime(),
            durationMS = endMS - startMS;

        return {
            startMS,
            endMS,
            durationMS
        };
    }

    /**
     * Generates data used in the template when rendering a task. For example which css classes to use. Also applies
     * #taskBodyTemplate and calls the {@link Gantt/view/Gantt#config-taskRenderer}.
     * @private
     * @param {Gantt.model.TaskModel} taskRecord Task to generate data for
     * @returns {Object} Data to use in task template, or `undefined` if the event is outside of the rendered zone.
     */
    generateTplData(taskRecord) {
        const me        = this,
            { gantt } = me,
            // generateTplData calculates layout for events which are outside of the vertical viewport
            // because the RowManager needs to know a row height.
            renderData  = me.getTimeSpanRenderData(taskRecord, taskRecord, { viewport : true });

        let taskContent = '';

        if (renderData) {
            let resizable = (taskRecord.isResizable === undefined ? true : taskRecord.isResizable);
            if (renderData.startsOutsideView) {
                if (resizable === true) resizable = 'end';
                else if (resizable === 'start') resizable = false;
            }
            if (renderData.endsOutsideView) {
                if (resizable === true) resizable = 'start';
                else if (resizable === 'end') resizable = false;
            }

            // Task record cls property is now a DomClassList, so clone it
            // so that it can be manipulated here and by renderers.
            renderData.cls = taskRecord.isResourceTimeRange ? new DomClassList() : taskRecord.cls.clone();

            // Gather event element classes as keys to add to the renderData.cls DomClassList.
            // Truthy value means the key will be added as a class name.
            Object.assign(renderData.cls, {
                [gantt.eventCls]                       : 1,
                [gantt.generatedIdCls]                 : taskRecord.hasGeneratedId,
                [gantt.dirtyCls]                       : taskRecord.modifications,
                [gantt.committingCls]                  : taskRecord.isCommitting,
                [gantt.endsOutsideViewCls]             : renderData.endsOutsideView,
                [gantt.startsOutsideViewCls]           : renderData.startsOutsideView,
                [gantt.fixedEventCls]                  : taskRecord.isDraggable === false,
                [`b-sch-event-resizable-${resizable}`] : 1,
                'b-milestone'                          : taskRecord.milestone,
                'b-critical'                           : taskRecord.critical,
                'b-task-started'                       : taskRecord.percentDone > 0,
                'b-task-finished'                      : taskRecord.isCompleted,
                'b-task-selected'                      : gantt.selectedRecords.includes(taskRecord)
            });

            renderData.iconCls = new DomClassList(taskRecord.get(gantt.eventBarIconClsField) || taskRecord.iconCls);
            renderData.id = gantt.getEventRenderId(taskRecord);
            renderData.style = taskRecord.style || '';
            renderData.taskId  = taskRecord.id;

            // Classes for the wrapping div
            renderData.wrapperCls = new DomClassList({
                [gantt.eventCls + '-wrap']   : 1,
                [`${gantt.eventCls}-parent`] : taskRecord.isParent,
                'b-milestone-wrap'           : taskRecord.milestone,
                'b-has-baselines'            : taskRecord.hasBaselines
            });

            const eventStyle = taskRecord.eventStyle || gantt.eventStyle,
                eventColor = taskRecord.eventColor || gantt.eventColor;

            renderData.eventColor = eventColor;
            renderData.eventStyle = eventStyle;

            if (gantt.taskRenderer) {
                // User has specified a renderer fn, either to return a simple string, or an object intended for the taskBodyTemplate
                const value = gantt.taskRenderer.call(gantt.taskRendererThisObj || gantt, { taskRecord, tplData : renderData });

                // If the user's renderer coerced it into a string, recreate a DomClassList.
                if (typeof renderData.cls === 'string') {
                    renderData.cls = new DomClassList(renderData.cls);
                }

                // Same goes for iconCls
                if (typeof renderData.iconCls === 'string') {
                    renderData.iconCls = new DomClassList(renderData.iconCls);
                }

                taskContent = (gantt.taskBodyTemplate && gantt.taskBodyTemplate(value)) || (value == null ? '' : String(value));
            }
            else if (gantt.taskBodyTemplate) {
                // User has specified an eventBodyTemplate, but no renderer - just apply the entire event record data.
                taskContent = gantt.taskBodyTemplate(taskRecord);
            }
            // else if (gantt.taskTextField) {
            //     // User has specified a field in the data model to read from
            //     eventContent = taskRecord.data[gantt.taskTextField] || '';
            // }

            // If there are any iconCls entries...
            renderData.cls['b-sch-event-withicon'] = renderData.iconCls.length;

            // renderers have last say on style & color
            renderData.wrapperCls[`b-sch-style-${renderData.eventStyle}`] = renderData.eventStyle;

            if (renderData.eventColor && renderData.eventColor.startsWith('#')) {
                renderData.style = `background-color:${renderData.eventColor};` + renderData.style;
            }
            else {
                renderData.wrapperCls[`b-sch-color-${renderData.eventColor}`] = renderData.eventColor;
            }

            if (!gantt.taskBodyTemplate) {
                // Give milestone a dedicated label element so we can use padding
                if (taskRecord.milestone && taskContent) {
                    taskContent = `<label>${taskContent}</label>`;
                }

                if (renderData.iconCls && renderData.iconCls.length) {
                    taskContent = `<i class="${renderData.iconCls}"></i>${taskContent}`;
                }
            }

            // html, use templates fragment
            if (taskContent.includes('<')) {
                // Create content as a DocumentFragment which may now be exposed to Features.
                renderData.body = DomHelper.createElementFromTemplate(taskContent, {
                    fragment : true
                });
            }
            // plain text, create fragment with the text in it
            else {
                renderData.body = document.createDocumentFragment();
                renderData.body.textContent = taskContent;
            }
        }

        // Method which features may chain in to
        gantt.onTaskDataGenerated(renderData);
        return renderData;
    }

    /**
     * Layouts a task on a row, caching on each task
     * @private
     * @param {Gantt.view.Gantt} gantt
     * @param {Gantt.model.TaskModel} task
     * @param {Grid.row.Row} row
     * @returns {boolean} Returns false if no events on row, otherwise true
     */
    layoutTask(gantt, task, row) {
        const me     = this,
            taskId = task.id;

        if (!me.timeAxis.isTimeSpanInAxis(task)) {
            me.cache.clearRow(taskId);
            return false;
        }

        // Iterate events belonging to current row
        const tplData = me.generateTplData(task);

        let rowHeight   = me.gantt.rowHeight || 0,
            absoluteTop = row.top;

        // adjust row top, when it is rendered on top (since in that case top is not known until height is set)
        if (rowHeight !== row.height && row.estimatedTop) {
            absoluteTop = row.top + row.height - rowHeight;
        }

        // cache boxes
        const layout      = me.cache.getTimeSpan(taskId),
            relativeTop = tplData.top;

        tplData.top += absoluteTop;
        tplData.rowTop = absoluteTop;

        // cache layout to not have to recalculate every time
        tplData.layoutCache = {
            layout         : true,
            // reuse div if already assigned (for example when resizing an event)
            div            : layout && layout.div,
            eventEl        : layout && layout.eventEl,
            width          : tplData.width,
            height         : tplData.height,
            start          : tplData.left,
            end            : tplData.left + tplData.width,
            relativeTop    : relativeTop,
            top            : tplData.top,
            relativeBottom : relativeTop + tplData.height,
            bottom         : tplData.top + tplData.height
        };

        me.cache.addRow(taskId, tplData);

        return true;
    }

    // Overrides fn from baseclass to trigger a paint task with correct params
    triggerPaint(taskData, element, isRepaint = false) {
        const { gantt } = this;
        gantt.trigger(isRepaint ? 'taskRepaint' : 'taskPaint', {
            taskData,
            taskRecord : taskData.task,
            element
        });
    }

    /**
     * Renders a single task, creating a div for it if needed or updates an existing div.
     * @private
     * @param data
     */
    renderTask(data) {
        const me         = this,
            { cache }    = me,
            layoutCache  = cache.getTimeSpan(data.taskId),
            renderedTask = cache.getRenderedTimeSpan(data.taskId);

        me.renderTimeSpan(data, layoutCache, renderedTask);

        cache.addRenderedTask(data.id, data);
    }

    updateRowTimeSpans(row, task, forceLayout = false) {
        const me                 = this,
            gantt              = me.gantt,
            cache              = me.cache,
            taskId             = task.id;

        let taskLayout   = cache.getRow(taskId);

        // no need to relayout events if only scrolling horizontally
        if ((gantt.forceLayout || forceLayout || !taskLayout) && !me.layoutTask(gantt, task, row)) {
            return;
        }

        // might have been updated above
        taskLayout = cache.getRow(taskId);

        if (!taskLayout) return;

        cache.addRenderedTask(taskId, taskLayout);

        me.renderTask(taskLayout);
    }

    renderer(renderData) {
        this.updateRowTimeSpans(renderData.row, renderData.record);
        renderData.size.height = this.gantt.rowHeight;
    }

    //endregion

    //region Div reusage

    // called from cache when removing events
    clearDiv(div, remove, remainVisible) {
        const me = this;
        if (!remove || remainVisible) {
            me.releaseTimeSpanDiv(div, remainVisible);
        }
        else {
            div.style.opacity = 0;
            me.setTimeout(() => {
                div.remove();
                div.style.opacity = 1;
            }, 200);
        }
    }

    // called from cache when removing events
    clearAllDivs() {
        const me = this;
        me.availableDivs.forEach(div => div.remove());
        me.availableDivs.length = 0;
        me.availableDivsById = {};
    }

    //endregion

    //region Dependency connectors

    // Cannot be moved from this file, called from currentOrientation.xx

    /**
     * Gets displaying item start side
     *
     * @param {Gantt.model.TaskModel} taskRecord
     * @return {String} 'left' / 'right' / 'top' / 'bottom'
     */
    getConnectorStartSide(taskRecord) {
        return 'left';
    }

    /**
     * Gets displaying item end side
     *
     * @param {Gantt.model.TaskModel} taskRecord
     * @return {String} 'left' / 'right' / 'top' / 'bottom'
     */
    getConnectorEndSide(taskRecord) {
        return 'right';
    }

    // We only have to ask this question in the horizontal axis.
    // Vertical rendering is driven fully by the Grid's RowManager
    // rendering and derendering rows.
    isEventInView(eventLayout) {
        //<debug>
        // This was causing bugs when TaskModel instance was passed in error.
        if (!('startMs' in eventLayout) && !('endMs' in eventLayout)) {
            throw new Error('Event render data block must be passed to TaskRendering#isEventInView');
        }
        //</debug>
        const viewportStart = this.view.timeAxis.startDate,
            viewportEnd = this.view.timeAxis.endDate;

        // Milestones need to be visible at start & end
        if (eventLayout.startMs === eventLayout.endMs) {
            return eventLayout.startMs <= viewportEnd && eventLayout.endMs > viewportStart;
        }

        // But normal events do not
        return eventLayout.startMs < viewportEnd && eventLayout.endMs > viewportStart;
    }

    //endregion
}
