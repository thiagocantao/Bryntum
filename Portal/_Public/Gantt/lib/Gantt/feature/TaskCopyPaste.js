import RowCopyPaste from '../../Grid/feature/RowCopyPaste.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module Gantt/feature/TaskCopyPaste
 */

/**
 * Allow using [Ctrl/CMD + C/X] and [Ctrl/CMD + V] to copy/cut and paste tasks. You can configure how a newly pasted record
 * is named using {@link #function-generateNewName}
 *
 * This feature is **enabled** by default
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         taskCopyPaste : true
 *     }
 * });
 * ```
 *
 * ## Keyboard shortcuts
 *
 * By default, this feature will react to Ctrl+C, Ctrl+X and Ctrl+V for standard clipboard actions.
 * You can reconfigure the keys used to trigger these actions, see {@link #config-keyMap} for more details.
 *
 * @extends Grid/feature/RowCopyPaste
 * @inlineexample Gantt/feature/TaskCopyPaste.js
 * @classtype taskCopyPaste
 * @feature
 */
export default class TaskCopyPaste extends TransactionalFeature(RowCopyPaste) {

    static $name = 'TaskCopyPaste';

    static type = 'taskCopyPaste';

    static configurable = {
        copyRecordText  : 'L{copyTask}',
        cutRecordText   : 'L{cutTask}',
        pasteRecordText : 'L{pasteTask}'
    };

    //region Events

    /**
     * Fires on the owning Gantt after a paste action is performed.
     * @event paste
     * @on-owner
     * @param {Gantt.view.Gantt} source Owner gantt
     * @param {Gantt.model.TaskModel} referenceRecord The reference task record, the clipboard task records will
     * be pasted above this row.
     * @param {Gantt.model.TaskModel[]} records The pasted task records
     * @param {Gantt.model.TaskModel[]} originalRecords For a copy action, these are the records that were copied.
     * For cut action, this is same as the `records` param.
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'task' to distinguish this event from other beforePaste events
     */

    /**
     * Fires on the owning Gantt before a paste action is performed, return `false` to prevent the action
     * @event beforePaste
     * @preventable
     * @on-owner
     * @param {Gantt.view.Gantt} source Owner Gantt
     * @param {Gantt.model.TaskModel} referenceRecord The reference task record, the clipboard task records will
     * be pasted above this row.
     * @param {Gantt.model.TaskModel[]} records The records about to be pasted
     * @param {Boolean} isCut `true` if this is a cut action
     * @param {String} entityName 'task' to distinguish this event from other beforePaste events
     */

    //endregion

    construct(gantt, config) {
        super.construct(gantt, config);

        gantt.ion({
            beforeRenderTask : 'onBeforeRenderTask',
            thisObj          : this
        });
    }

    // Used in events to separate events from different features from each other
    entityName = 'task';

    //region Display store adjustments

    populateCellMenu({ record, items }) {
        super.populateCellMenu(...arguments);

        // No copy pasting when using a "display store"
        if (this.client.usesDisplayStore) {
            items.cut && (items.cut.disabled = true);
            items.copy && (items.copy.disabled = true);
            items.paste && (items.paste.disabled = true);
        }
    }

    isActionAvailable({ key, action, event }) {
        return !this.client.usesDisplayStore && super.isActionAvailable({ key, action, event });
    }

    //endregion

    setIsCut(taskRecord) {
        super.setIsCut(...arguments);

        // After a row is cut or copied - also refresh the associated task bar
        this.client.taskRendering.redraw(taskRecord);
    }

    onBeforeRenderTask({ renderData }) {
        renderData.cls['b-cut-row'] = renderData.row.cls['b-cut-row'];
    }

    extractParents(taskRecords, idMap, generateNames = true) {
        const result = super.extractParents(taskRecords, idMap, generateNames);

        if (!this.isCut) {
            this.depsToCopy = this.extractDependencies(taskRecords, idMap);
        }

        return result;
    }

    async insertCopiedRecords(toInsert, recordReference) {
        const me = this;

        await me.startFeatureTransaction();

        const result = await super.insertCopiedRecords(toInsert, recordReference);

        toInsert.forEach(parent => parent.refreshWbs({ deep : true, useOrderedTree : true }));

        me.client.dependencyStore.add(me.depsToCopy);

        delete me.depsToCopy;

        await me.finishFeatureTransaction();

        return result;
    }

    /**
     * Extract dependencies from passed records. The result will include only deps via records and not include deps
     * with foreign records.
     * @param {Core.data.Model[]} taskRecords array of records to extract dependencies from
     * @param {Object} idMap Map linking original node id with its copy
     * @returns {Object[]} array of dependencies settings via passed records to apply using applyDependencies method
     * @private
     */
    extractDependencies(taskRecords, idMap) {
        // This map is required to see which tasks are already connected
        const depsMap = {};

        return taskRecords.reduce((deps, task) => {
            task.predecessors.forEach(predecessor => {
                const key = predecessor.id;

                if (!(key in depsMap) && taskRecords.includes(predecessor.fromEvent)) {
                    depsMap[key] = true;

                    deps.push(Object.assign({}, predecessor.data, {
                        id        : undefined,
                        to        : undefined,
                        toEvent   : idMap[task.id].id,
                        toTask    : undefined,
                        from      : undefined,
                        fromEvent : idMap[predecessor.fromEvent.id].id,
                        fromTask  : undefined
                    }));
                }
            });

            task.successors.forEach(successor => {
                const key = successor.id;

                if (!(key in depsMap) && taskRecords.includes(successor.toEvent)) {
                    depsMap[key] = true;

                    deps.push(Object.assign({}, successor.data, {
                        id        : undefined,
                        to        : undefined,
                        toEvent   : idMap[successor.toEvent.id].id,
                        toTask    : undefined,
                        from      : undefined,
                        fromEvent : idMap[task.id].id,
                        fromTask  : undefined
                    }));
                }
            });

            return deps;
        }, []);
    }
}

GridFeatureManager.registerFeature(TaskCopyPaste, true, 'Gantt');
