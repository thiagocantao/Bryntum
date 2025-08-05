import AjaxStore from '../../Core/data/AjaxStore.js';
import TaskModel from '../model/TaskModel.js';
import { ChronoEventTreeStoreMixin } from '../../Engine/quark/store/ChronoEventStoreMixin.js';
import PartOfProject from './mixin/PartOfProject.js';
import DateHelper from '../../Core/helper/DateHelper.js';

const padWbs = wbs => wbs.replace(/(\d+)/g, index => +index + 100000);

/**
 * @module Gantt/data/TaskStore
 */

/**
 * A class representing the tree of tasks in the Gantt project. An individual task is represented as an instance of the
 * {@link Gantt.model.TaskModel} class. The store expects the data loaded to be hierarchical. Each parent node should
 * contain its children in a property called 'children'.
 *
 * ```javascript
 * const taskStore = new TaskStore({
 *      data : [
 *          {
 *               "id"           : 1000,
 *               "name"         : "Cool project",
 *               "percentDone"  : 50,
 *               "startDate"    : "2019-01-02",
 *               "expanded"     : true,
 *               "children"     : [
 *                   {
 *                       "id"           : 1,
 *                       "name"         : "A leaf node",
 *                       "startDate"    : "2019-01-02",
 *                       "percentDone"  : 50,
 *                       "duration"     : 10,
 *                   }
 *              ]
 *           }
 *      ]
 * });
 * ```
 *
 * @extends Core/data/AjaxStore
 */
export default class TaskStore extends ChronoEventTreeStoreMixin.derive(AjaxStore).mixin(PartOfProject) {

    static get defaultConfig() {
        return {
            modelClass : TaskModel,

            /**
             * CrudManager must load stores in the correct order. Lowest first.
             * @private
             */
            loadPriority : 200,

            /**
             * CrudManager must sync stores in the correct order. Lowest first.
             * @private
             */
            syncPriority : 300,

            storeId : 'tasks',

            tree : true
        };
    }

    /**
     * For each task in this TaskStore, sets the data in the passed baseline index to the current state of the task.
     * @param {Number} index The index in the baselines list of the baseline to update.
     */
    setBaseline(index) {
        const data = this.storage.values;

        this.forEach(task => task.setBaseline(index));
        this.trigger('refresh', {
            action  : 'batch',
            records : data,
            data
        });
    }

    /**
     * Increase the indentation level of one or more tasks in the tree
     * @param {Gantt.model.TaskModel|Gantt.model.TaskModel[]} nodes The nodes to indent.
     * @return {Promise} A promise which yields the result of the operation
     * @fires indent
     * @fires change
     */
    async indent(nodes) {
        const
            me                     = this,
            { taskStore, project } = me;

        let result = false;

        nodes = Array.isArray(nodes) ? nodes.slice() : [nodes];

        // 1. Filter out project nodes and nodes whose beforeIndent event was vetoed.
        nodes = nodes.filter(task => !task.isProjectModel && project.trigger('beforeIndent', { task }) !== false);

        // 2. Filtering out all nodes which parents are also to be indented as well as the ones having no previous
        //    sibling since such nodes canâ€™t be indented
        nodes = nodes.filter(node => {
            let result;

            result = Boolean(node.previousSibling);

            while (result && !node.isRoot) {
                result = !nodes.includes(node.parent);
                node   = node.parent;
            }

            return result;
        });

        if (nodes.length) {
            // 3. Sorting nodes into tree walk order
            nodes.sort((lhs, rhs) => {
                // First pad WBS to ensure 1.10 is greater than 1.2
                const
                    lhsWbs = padWbs(lhs.wbsCode),
                    rhsWbs = padWbs(rhs.wbsCode);

                return lhsWbs < rhsWbs ? -1 : lhsWbs > rhsWbs ? 1 : 0;
            });

            // No events should go to the UI until we have finished the operation successfully
            taskStore.beginBatch();

            // Ask the project to try the indent operation
            result = await project.tryPropagateWithChanges(() => {
                for (const node of nodes) {
                    const newParent = node.previousSibling;
                    newParent.appendChild(node);
                    me.toggleCollapse(newParent, false);
                }
            });

            // Now show the successful result
            taskStore.endBatch();

            if (result) {
                /**
                 * Fired after tasks in the tree are indented
                 * @event indent
                 * @param {Gantt.data.TaskStore} source The task store
                 * @param {Gantt.model.TaskModel[]} records Tasks that got indent
                 */
                me.trigger('indent', { records : nodes });
                me.trigger('change', {
                    action  : 'indent',
                    records : nodes
                });
            }
        }

        return result;
    }

    /**
     * Decrease the indentation level of one or more tasks in the tree
     * @param {Gantt.model.TaskModel|Gantt.model.TaskModel[]} nodes The nodes to outdent.
     * @return {Promise} A promise which yields the result of the operation
     * @fires outdent
     * @fires change
     */
    async outdent(nodes) {
        const
            me = this,
            { taskStore, project } = me;

        let result = false;

        nodes = Array.isArray(nodes) ? nodes.slice() : [nodes];

        // 1. Filter out project nodes and nodes whose beforeOutdent event was vetoed.
        nodes = nodes.filter(task => !task.isProjectModel && project.trigger('beforeOutdent', { task }) !== false);

        // 2. Filtering out all nodes which parents are also to be outdented as well as the ones having no previous sibling
        //    since such nodes canâ€™t be indented
        nodes = nodes.filter(node => {
            let result;

            result = node.parent && !node.parent.isRoot;

            while (result && !node.isRoot) {
                result = !nodes.includes(node.parent);
                node = node.parent;
            }

            return result;
        });

        if (nodes.length) {
            // 3. Sorting nodes into reverse tree walk order
            nodes.sort((lhs, rhs) => {
                // First pad WBS to ensure 1.10 is greater than 1.2
                const
                    lhsWbs = padWbs(lhs.wbsCode),
                    rhsWbs = padWbs(rhs.wbsCode);

                return lhsWbs < rhsWbs ? 1 : lhsWbs > rhsWbs ? -1 : 0;
            });

            // No events should go to the UI until we have finished the operation successfully
            taskStore.beginBatch();

            result = await project.tryPropagateWithChanges(() => {
                for (const node of nodes) {
                    const newChildren = node.parent.children.slice(node.parent.children.indexOf(node) + 1);

                    node.parent.parent.insertChild(node, node.parent.nextSibling);

                    node.appendChild(newChildren);
                    me.toggleCollapse(node, false);
                }
            });

            taskStore.endBatch();

            if (result) {
                /**
                 * Fired after tasks in the tree are outdented
                 * @event outdent
                 * @param {Gantt.data.TaskStore} source The task store
                 * @param {Gantt.model.TaskModel[]} records Tasks that got outdent
                 */
                me.trigger('outdent', { records : nodes });
                me.trigger('change', {
                    action  : 'outdent',
                    records : nodes
                });
            }
        }

        return result;
    }

    getTotalTimeSpan() {
        return {
            startDate : this.getProject().startDate,
            endDate   : this.getProject().endDate
        };
    }

    // Trimmed down version of EventStore.getEvent(), needed when using a Gantt project in Scheduler Pro
    /**
     * Returns an array of tasks a Resource for the date range specified by the `startDate` and `endDate` options.
     *
     * By default, for any date, this includes any task which *intersects* that date.
     *
     * To only include tasks that are fully contained *within* the date range, pass the `allowPartial`
     * option as `false`
     *
     * Example:
     *
     * ```javascript
     *  visibleTasks = taskStore.getEvents({
     *      resourceRecord,
     *      startDate : gantt.timeAxis.startDate,
     *      endDate   : gantt.timeAxis.endDate
     *  });
     * ```
     *
     * @param {Date} [options.startDate] The start date for the range of tasks to include.
     * @param {Date} [options.endDate] The end date for the range of tasks to include.
     * @param {Gantt.model.ResourceModel} options.resourceRecord Resource to return tasks for.
     * @param {Function} [options.filter] A function to filter out tasks which are not required.
     * @returns {Scheduler.model.EventModel[]} Tasks which match the passed configuration.
     * @internal
     */
    getEvents({
        filter,
        resourceRecord,
        startDate,                          // Events which intersect the startDate/endDate
        endDate                             // will be returned
    }) {
        const me = this;

        if (!me.count) {
            return [];
        }

        const
            storeFilterFn = me.isFiltered ? eventRecord => me.indexOf(eventRecord) > -1 : null,
            dateFilter    = e => e.startDate && DateHelper.intersectSpans(e.startDate, e.endDate || e.startDate, startDate, endDate);

        let events = me.getEventsForResource(resourceRecord);

        // Events found from the date indices won't be filtered.
        // On the other side, when using getEventForResource we will get all events for
        // the resource even if the EventStore is filtered, handle this by excluding "invisible" events here
        if (storeFilterFn) {
            events = events.filter(storeFilterFn);
        }

        events = events.filter(dateFilter);

        // filter = filter && onlyAssigned ? e => filter(e) && e.resources.length : onlyAssigned ? e => e.resources.length : filter;

        return filter ? events.filter(filter) : events;
    }

    getEventsForResource(resourceId) {
        const
            resource    = this.resourceStore.getById(resourceId),
            assignments = resource?.assignments.filter(assignment => assignment.isPartOfStore(this.assignmentStore)) || [];

        return assignments.map(r => r.event);
    }

}
