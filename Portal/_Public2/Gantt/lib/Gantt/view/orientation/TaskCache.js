export default class TaskCache {
    constructor(taskRendering) {
        const me = this;

        me.taskRendering = taskRendering;

        // caching layout calculations
        me.renderedTasksMap = {};
        me.rowLayoutCache = {};
    }

    /**
     * Clear layout cache
     * @protected
     */
    clear(removeDivs = false) {
        const me = this;

        me.renderedTasksMap = {};
        Object.keys(me.rowLayoutCache).forEach(taskId => me.clearRow(taskId, removeDivs));

        if (removeDivs) {
            me.taskRendering.clearAllDivs();
        }
    }

    //region Render data

    addRenderedTask(taskId, data) {
        this.renderedTasksMap[taskId] = data;
    }

    getRenderedTimeSpan(taskId) {
        return this.renderedTasksMap[taskId];
    }

    // clearRenderedTimeSpan(taskId) {
    //     delete this.renderedTasksMap[taskId];
    // }
    //
    // getRenderedEvents(taskId) {
    //     return this.renderedTasksMap[taskId];
    // }
    //
    // clearRenderedEvents(taskId) {
    //     delete this.renderedTasksMap[taskId];
    // }

    //endregion

    //region Task

    getTimeSpan(taskId) {
        const resourceCache = this.rowLayoutCache[taskId];

        if (!resourceCache) return null;

        return resourceCache.layoutCache;
    }

    /**
     * Clears the task layout for the passed task. Will usually preserve the tasks DIVs for recycling unless `removeDiv`
     * is passed. If preserving them, it will hide the div unless `remainVisible` is passed.
     * @param {*} taskId ID of task
     * @param {*} taskIdAgain ID of task again, to match scheduler caching
     * @param {*} removeDiv Defaults to false
     * @param {*} remainVisible Defaults to false
     * @private
     */
    clearTask(taskId, removeDiv = false, remainVisible = false) {
        const me         = this,
            eventCache = me.getTimeSpan(taskId, taskId);

        if (!eventCache) return null;

        if (eventCache.div) {
            me.taskRendering.clearDiv(eventCache.div, removeDiv, remainVisible);
        }

        me.rowLayoutCache[taskId].layoutCache = null;
    }

    //endregion

    //region Row

    changeRowId(fromId, toId) {
        const task = this.getRow(fromId);
        if (task) {
            delete this.rowLayoutCache[fromId];
            this.addRow(toId, task);
        }
    }

    getRow(taskId) {
        return this.rowLayoutCache[taskId];
    }

    addRow(taskId, data) {
        this.rowLayoutCache[taskId] = data;
    }

    clearRow(taskId, removeDivs = false, remainVisible = false) {
        const me            = this,
            resourceCache = me.rowLayoutCache[taskId];

        if (!resourceCache) return;

        if (me.renderedTasksMap[taskId]) delete me.renderedTasksMap[taskId];

        me.clearTask(taskId, removeDivs, remainVisible);

        delete me.rowLayoutCache[taskId];
    }

    //endregion
}
