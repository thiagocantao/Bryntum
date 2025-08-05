import SchedulerProDependencyModel from '../../SchedulerPro/model/DependencyModel.js';

/**
 * @module Gantt/model/DependencyModel
 */

/**
 * This class represents a single dependency between the tasks in your Gantt project.
 *
 * ## Subclassing the Dependency class
 *
 * The name of any field in data can be customized in the subclass, see the example below.
 *
 * ```javascript
 * class MyDependencyModel extends DependencyModel {
 *   static get fields() {
 *     return [
 *       { name: 'to', dataSource : 'targetId' },
 *       { name: 'from', dataSource : 'sourceId' }
 *     ];
 *   }
 * }
 * ```
 * @extends SchedulerPro/model/DependencyModel
 *
 * @typings Scheduler/model/DependencyModel -> Scheduler/model/SchedulerDependencyModel
 * @typings SchedulerPro/model/DependencyModel -> SchedulerPro/model/SchedulerProDependencyModel
 */
export default class DependencyModel extends SchedulerProDependencyModel {

    constructor(...args) {
        const [config] = args;

        if (config && config.fromTask) {
            config.fromEvent = config.fromTask;
        }

        if (config && config.toTask) {
            config.toEvent = config.toTask;
        }

        super(...args);
    }

    get from() {
        return this.fromEvent?.id;
    }

    /**
     * The origin task of this dependency
     * @field {Gantt.model.TaskModel} fromTask
     */

    /**
     * The destination task of this dependency
     * @field {Gantt.model.TaskModel} toTask
     */

    get fromTask() {
        return this.fromEvent;
    }

    set fromTask(task) {
        this.fromEvent = task;
    }

    get to() {
        return this.toEvent?.id;
    }

    get toTask() {
        return this.toEvent;
    }

    set toTask(task) {
        this.toEvent = task;
    }
}
