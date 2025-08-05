import Model from '../../Core/data/Model.js';
import Store from '../../Core/data/Store.js';
import { GanttProjectMixin } from '../../Engine/quark/model/gantt/GanttProjectMixin.js';
import TimeSpan from '../../Scheduler/model/TimeSpan.js';
import AssignmentStore from '../data/AssignmentStore.js';
import CalendarManagerStore from '../data/CalendarManagerStore.js';
import DependencyStore from '../data/DependencyStore.js';
import ResourceStore from '../data/ResourceStore.js';
import TaskStore from '../data/TaskStore.js';
import AssignmentModel from './AssignmentModel.js';
import CalendarModel from './CalendarModel.js';
import DependencyModel from './DependencyModel.js';
import ResourceModel from './ResourceModel.js';
import TaskModel from './TaskModel.js';
import ProjectCrudManager from '../../SchedulerPro/data/mixin/ProjectCrudManager.js';
import StringHelper from '../../Core/helper/StringHelper.js';

/**
 * @module Gantt/model/ProjectModel
 */

/**
 * This class represents a global project of your Project plan or Gantt - a central place for all data.
 *
 * It holds and links the stores usually used by Gantt:
 *
 * - {@link Gantt.data.AssignmentStore}
 * - {@link Gantt.data.CalendarManagerStore}
 * - {@link Gantt.data.DependencyStore}
 * - {@link Gantt.data.ResourceStore}
 * - {@link Gantt.data.TaskStore}
 *
 * The project uses a scheduling engine to calculate dates, durations and such. It is also responsible for
 * handling references between models, for example to link an task via an assignment to a resource. These operations
 * are asynchronous, a fact that is hidden when working in the Gantt UI but which you must know about when performing
 * operations on the data level.
 *
 * When there is a change to data that requires something else to be recalculated, the project schedules a calculation
 * (a commit) which happens moments later. It is also possible to trigger these calculations directly. This flow
 * illustrates the process:
 *
 * 1. Something changes which requires the project to recalculate, for example adding a new task:
 *
 * ```javascript
 * const [task] = project.taskStore.add({ startDate, endDate });
 * ```
 *
 * 2. A recalculation is scheduled, thus:
 *
 * ```javascript
 * task.duration; // <- Not yet calculated
 * ```
 *
 * 3. Calculate now instead of waiting for the scheduled calculation
 *
 * ```javascript
 * await project.commitAsync();
 *
 * task.duration; // <- Now available
 * ```
 *
 * Please refer to [this guide](#guides/project_data.md) for more information.
 *
 * ## Built in CrudManger
 *
 * Gantt's project has a {@link Scheduler.data.CrudManager CrudManager} built in. Using it is the recommended way of
 * syncing data between Gantt and a backend. Example usage:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     project : {
 *         // Configure urls used by the built in CrudManager
 *         transport : {
 *             load : {
 *                 url : 'php/load.php'
 *             },
 *             sync : {
 *                 url : 'php/sync.php'
 *             }
 *         }
 *     }
 * });
 *
 * // Load data from the backend
 * gantt.project.load()
 * ```
 *
 * For more information on CrudManager, see Schedulers docs on {@link Scheduler.data.CrudManager}.
 * For a detailed description of the protocol used by CrudManager, please see the
 * [Crud manager guide](#guides/data/crud_manager.md)
 *
 * ## Built in StateTrackingManager
 *
 * The project also has a built in {@link Core.data.stm.StateTrackingManager StateTrackingManager} (STM for short), that
 * handles undo/redo for the project stores (additional stores can also be added). By default, it is only used while
 * editing tasks using the task editor, the editor updates tasks live and uses STM to rollback changes if canceled. But
 * you can enable it to track all project store changes:
 *
 * ```javascript
 * // Enable automatic transaction creation and start recording
 * project.stm.autoRecord = true;
 * project.stm.enable();
 *
 * // Undo a transaction
 * project.stm.undo();
 *
 * // Redo
 * project.stm.redo();
 * ```
 *
 * Check out the `undoredo` demo to see it in action.
 *
 * @extends Core/data/Model
 *
 * @mixes SchedulerPro/data/mixin/ProjectCrudManager
 * @mixes Core/mixin/Events
 *
 * @typings SchedulerPro/model/ProjectModel -> SchedulerPro/model/SchedulerProProjectModel
 */
export default class ProjectModel extends ProjectCrudManager(GanttProjectMixin.derive(Model)) {
    //region Config

    /**
     * Silences propagations caused by the project loading.
     *
     * Applying the loaded data to the project occurs in two basic stages:
     *
     * 1. Data gets into the engine graph which triggers changes propagation
     * 2. The changes caused by the propagation get written to related stores
     *
     * Setting this flag to `true` makes the component perform step 2 silently without triggering events causing reactions on those changes
     * (like sending changes back to the server if `autoSync` is enabled) and keeping stores in unmodified state.
     *
     * This is safe if the loaded data is consistent so propagation doesn't really do any adjustments.
     * By default the system treats the data as consistent so this option is `true`.
     *
     * ```js
     * new Gantt({
     *     project : {
     *         // We want scheduling engine to recalculate the data properly
     *         // so then we could save it back to the server
     *         silenceInitialCommit : false,
     *         ...
     *     }
     *     ...
     * })
     * ```
     *
     * @config {Boolean} silenceInitialCommit
     * @default true
     */

    static get defaults() {
        return {
            /**
             * The number of hours per day (is used when converting the duration from one unit to another).
             * @field {number} hoursPerDay
             * @default 24
             */

            /**
             * The number of days per week (is used when converting the duration from one unit to another).
             * @field {number} daysPerWeek
             * @default 7
             */

            /**
             * The number of days per month (is used when converting the duration from one unit to another).
             * @field {number} daysPerMonth
             * @default 30
             */

            /**
             * The source of the calendar for dependencies (the calendar used for taking dependencies lag into account).
             * Possible values are:
             *
             * - `ToEvent` - successor calendar will be used (default);
             * - `FromEvent` - predecessor calendar will be used;
             * - `Project` - the project calendar will be used.
             *
             * @field {string} dependenciesCalendar
             * @default 'ToEvent'
             */

            /**
             * The project calendar.
             * @field {Gantt.model.CalendarModel} calendar
             */

            /**
             * State tracking manager instance the project relies on
             * @property {Core.data.stm.StateTrackingManager}
             * @name stm
             */

            /**
             * The {@link Gantt.data.TaskStore store} holding the tasks information.
             *
             * See also {@link Gantt.model.TaskModel}
             *
             * @property {Gantt.data.TaskStore}
             * @name eventStore
             */

            /**
             * An alias for the {@link #property-eventStore eventStore}
             *
             * See also {@link Gantt.model.TaskModel}
             *
             * @property {Gantt.data.TaskStore}
             * @name taskStore
             */

            /**
             * The {@link Gantt.data.DependencyStore store} holding the dependencies information.
             *
             * See also {@link Gantt.model.DependencyModel}
             *
             * @property {Gantt.data.DependencyStore}
             * @name dependencyStore
             */

            /**
             * The {@link Gantt.data.ResourceStore store} holding the resources that can be assigned to the tasks in the task store.
             *
             * See also {@link Gantt.model.ResourceModel}
             *
             * @property {Gantt.data.ResourceStore}
             * @name resourceStore
             */

            /**
             * The {@link Gantt.data.AssignmentStore store} holding the assignments information.
             *
             * See also {@link Gantt.model.AssignmentModel}
             *
             * @property {Gantt.data.AssignmentStore}
             * @name assignmentStore
             */

            /**
             * The store, holding the calendars information.
             *
             * @property {Gantt.data.CalendarManagerStore}
             * @name calendarManagerStore
             */

            /**
             * Returns an array of critical paths.
             * Each _critical path_ is an array of critical path nodes.
             * Each _critical path node_ is an object which contains {@link Gantt.model.TaskModel#field-critical critical task}
             * and {@link Gantt.model.DependencyModel dependency} leading to the next critical path node.
             * Dependency is missing if it is the last critical path node in the critical path.
             * To highlight critical paths, enable {@link Gantt.feature.CriticalPaths CriticalPaths} feature.
             *
             * ```javascript
             * // This is an example of critical paths structure
             * [
             *      // First path
             *      [
             *          {
             *              event : Gantt.model.TaskModel
             *              dependency : Gantt.model.DependencyModel
             *          },
             *          {
             *              event : Gantt.model.TaskModel
             *          }
             *      ],
             *      // Second path
             *      [
             *          {
             *              event : Gantt.model.TaskModel
             *          }
             *      ]
             *      // and so on....
             * ]
             * ```
             *
             * @property {Array[]}
             * @name criticalPaths
             */

            // root should be always expanded
            expanded : true
        };
    }

    static get defaultConfig() {
        return {
            /**
             * Deprecated, use {@link #config-taskModelClass}
             * @deprecated Use {@link #config-taskModelClass}
             * @config {Gantt.model.TaskModel} [eventModelClass]
             */
            eventModelClass : TaskModel,

            /**
             * The constructor of the event model class, to be used in the project. Will be set as the {@link Core.data.Store#config-modelClass modelClass}
             * property of the {@link #property-eventStore}
             *
             * @config {Gantt.model.TaskModel} [taskModelClass]
             */
            taskModelClass : TaskModel,

            /**
             * The constructor of the dependency model class, to be used in the project. Will be set as the {@link Core.data.Store#config-modelClass modelClass}
             * property of the {@link #property-dependencyStore}
             *
             * @config {Gantt.model.DependencyModel} [dependencyModelClass]
             */
            dependencyModelClass : DependencyModel,

            /**
             * The constructor of the resource model class, to be used in the project. Will be set as the {@link Core.data.Store#config-modelClass modelClass}
             * property of the {@link #property-resourceStore}
             *
             * @config {Gantt.model.ResourceModel} [resourceModelClass]
             */
            resourceModelClass : ResourceModel,

            /**
             * The constructor of the assignment model class, to be used in the project. Will be set as the {@link Core.data.Store#config-modelClass modelClass}
             * property of the {@link #property-assignmentStore}
             *
             * @config {Gantt.model.AssignmentModel} [assignmentModelClass]
             */
            assignmentModelClass : AssignmentModel,

            /**
             * The constructor of the calendar model class, to be used in the project. Will be set as the {@link Core.data.Store#config-modelClass modelClass}
             * property of the {@link #property-calendarManagerStore}
             *
             * @config {Gantt.model.CalendarModel} [calendarModelClass]
             */
            calendarModelClass : CalendarModel,

            /**
             * Deprecated, use {@link #config-taskStoreClass}
             * @deprecated
             * @config {Gantt.data.TaskStore}
             */
            eventStoreClass : TaskStore,

            /**
             * The constructor to create an task store instance with. Should be a class, subclassing the {@link Gantt.data.TaskStore}
             * @config {Gantt.data.TaskStore}
             */
            taskStoreClass : TaskStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the {@link Gantt.data.DependencyStore}
             * @config {Gantt.data.DependencyStore}
             */
            dependencyStoreClass : DependencyStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the {@link Gantt.data.ResourceStore}
             * @config {Gantt.data.ResourceStore}
             */
            resourceStoreClass : ResourceStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the {@link Gantt.data.AssignmentStore}
             * @config {Gantt.data.AssignmentStore}
             */
            assignmentStoreClass : AssignmentStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the {@link Gantt.data.CalendarManagerStore}
             * @config {Gantt.data.CalendarManagerStore}
             */
            calendarManagerStoreClass : CalendarManagerStore,

            /**
             * Start date of the project in the ISO 8601 format. Setting this date will constrain all other tasks in the project,
             * to start no later than it. If this date is not provided, it will be calculated as the earliest date among all events.
             *
             * @field {string|Date} startDate
             */

            /**
             * End date of the project in the ISO 8601 format. If this date is not provided, it will be calculated
             * as the earliest date among all tasks.
             *
             * @field {string|Date} endDate
             */

            /**
             * The scheduling direction of the project events.
             * The `Forward` direction corresponds to the As-Soon-As-Possible (ASAP) scheduling,
             * `Backward` - to As-Late-As-Possible (ALAP).
             *
             * @field {string} direction
             * @default 'Forward'
             */

            /**
             * The initial data, to fill the {@link #property-taskStore taskStore} with.
             * Should be an array of {@link Gantt.model.TaskModel TaskModels} or it's configuration objects.
             *
             * @config {Gantt.model.TaskModel[]}
             */
            tasksData : null,

            // What is actually used to hold initial tasks, tasksData is transformed in construct()
            eventsData : null,

            /**
             * The initial data, to fill the {@link #property-dependencyStore dependencyStore} with.
             * Should be an array of {@link Gantt.model.DependencyModel DependencyModels} or it's configuration objects.
             *
             * @config {Gantt.model.DependencyModel[]}
             */
            dependenciesData : null,

            /**
             * The initial data, to fill the {@link #property-resourceStore resourceStore} with.
             * Should be an array of {@link Gantt.model.ResourceModel ResourceModels} or it's configuration objects.
             *
             * @config {Gantt.model.ResourceModel[]}
             */
            resourcesData : null,

            /**
             * The initial data, to fill the {@link #property-assignmentStore assignmentStore} with.
             * Should be an array of {@link Gantt.model.AssignmentModel AssignmentModels} or it's configuration objects.
             *
             * @config {Gantt.model.AssignmentModel[]}
             */
            assignmentsData : null,

            /**
             * The initial data, to fill the {@link #property-calendarManagerStore calendarManagerStore} with.
             * Should be an array of {@link Gantt.model.CalendarModel CalendarModels} or it's configuration objects.
             *
             * @config {Gantt.model.CalendarModel[]}
             */
            calendarsData : null,

            /**
             * Store that holds time ranges (using the {@link Scheduler.model.TimeSpan} model or subclass thereof) for
             * {@link Scheduler.feature.TimeRanges} feature. A store will be automatically created if none is specified.
             * @config {Object|Core.data.Store}
             */
            timeRangeStore : null,

            convertEmptyParentToLeaf : false
        };
    }

    construct(...args) {
        const config = args[0] || {};

        // put config to arguments (passed to the parent class "construct")
        args[0] = config;

        if ('tasksData' in config) {
            config.eventsData   = config.tasksData;
            delete config.tasksData;
        }

        if ('taskStore' in config) {
            config.eventStore = config.taskStore;
            delete config.taskStore;
        }

        // Maintain backwards compatibility
        // TODO remove for 3.0
        config.eventModelClass = config.taskModelClass || config.eventModelClass || this.defaultEventModelClass;
        config.eventStoreClass = config.taskStoreClass || config.eventStoreClass || this.defaultEventStoreClass;

        if (!config.timeRangeStore) {
            config.timeRangeStore = {
                modelClass : TimeSpan,
                storeId    : 'timeRanges'
            };
        }

        super.construct(...args);
    }

    get defaultEventModelClass() {
        return TaskModel;
    }

    get defaultEventStoreClass() {
        return TaskStore;
    }

    get taskStore() {
        return this.getEventStore();
    }

    get timeRangeStore() {
        return this._timeRangeStore;
    }

    set timeRangeStore(store) {
        const me = this;

        me._timeRangeStore = Store.getStore(store, Store);

        if (!me._timeRangeStore.storeId) {
            me._timeRangeStore.storeId = 'timeRanges';
        }
    }

    async tryInsertChild() {
        return this.tryPropagateWithChanges(() => {
            this.insertChild(...arguments);
        });
    }

    /**
     * Returns a calendar of the project. If task has never been assigned a calendar a project's calendar will be returned.
     *
     * @method
     * @name getCalendar
     * @returns {Gantt.model.CalendarModel}
     */

    /**
     * Sets the calendar of the project. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name setCalendar
     * @param {Gantt.model.CalendarModel} calendar The new calendar.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Causes the scheduling engine to re-evaluate the task data and all associated data and constraints
     * and apply necessary changes.
     * @returns {Promise}
     * @function propagate
     */

    /**
     * Suspend {@link #function-propagate propagation} processing. When propagation is suspended,
     * calls to {@link #function-propagate} do not proceed, instead a propagate call is deferred
     * until a matching {@link #function-resumePropagate} is called.
     * @function suspendPropagate
     */

    /**
     * Resume {@link #function-propagate propagation}. If propagation is resumed (calls may be nested
     * which increments a suspension counter), then if a call to propagate was made during suspension,
     * {@link #function-propagate} is executed.
     * @param {Boolean} [trigger] Pass `false` to inhibit automatic propagation if propagate was requested during suspension.
     * @returns {Promise}
     * @function resumePropagate
     */

    /**
     * Accepts a "data package" consisting of data for the projects stores, which is then loaded into the stores.
     *
     * The package can hold data for EventStore, AssignmentStore, ResourceStore and DependencyStore. It uses the same
     * format as when creating a project with inline data:
     *
     * ```javascript
     * await project.loadInlineData({
     *     eventsData       : [...],
     *     resourcesData    : [...],
     *     assignmentsData  : [...],
     *     dependenciesData : [...]
     * });
     * ```
     *
     * After populating the stores it commits the project, starting its calculations. By awaiting `loadInlineData()` you
     * can be sure that project calculations are finished.
     *
     * @function loadInlineData
     * @param {Object} dataPackage A data package as described above
     * @fires load
     * @async
     */

    /**
     * Project changes (CRUD operations to records in its stores) are automatically committed on a buffer to the
     * underlying graph based calculation engine. The engine performs it calculations async.
     *
     * By calling this function, the commit happens right away. And by awaiting it you are sure that project
     * calculations are finished and that references between records are up to date.
     *
     * ```javascript
     * // Move a task in time
     * taskStore.first.shift(1);
     * // Trigger calculations directly and wait for them to finish
     * await project.commitAsync();
     * ```
     *
     * @function commitAsync
     * @async
     */

    //region JSON

    /**
     * Returns the data from the records of the projects stores, in a format that can be consumed by `loadInlineData()`.
     *
     * Used by JSON.stringify to correctly convert this record to json.
     *
     *
     * ```javascript
     * const project = new ProjectModel({
     *     eventsData       : [...],
     *     resourcesData    : [...],
     *     assignmentsData  : [...],
     *     dependenciesData : [...]
     * });
     *
     * const json = project.toJSON();
     *
     * // json:
     * {
     *     eventsData : [...],
     *     resourcesData : [...],
     *     dependenciesData : [...],
     *     assignmentsData : [...]
     * }
     * ```
     *
     * Output can be consumed by `loadInlineData()`:
     *
     * ```javascript
     * const json = project.toJSON();
     *
     * // Plug it back in later
     * project.loadInlineData(json);
     * ```
     *
     * @returns {Object}
     * @category JSON
     */
    toJSON() {
        return {
            eventsData       : this.eventStore.toJSON(),
            resourcesData    : this.resourceStore.toJSON(),
            dependenciesData : this.dependencyStore.toJSON(),
            assignmentsData  : this.assignmentStore.toJSON()
        };
    }

    /**
     * Get or set project data (records from its stores) as a JSON string.
     *
     * Get a JSON string:
     *
     * ```javascript
     * const project = new ProjectModel({
     *     eventsData       : [...],
     *     resourcesData    : [...],
     *     assignmentsData  : [...],
     *     dependenciesData : [...]
     * });
     *
     * const jsonString = project.json;
     *
     * // jsonString:
     * '{"eventsData":[...],"resourcesData":[...],...}'
     * ```
     *
     * Set a JSON string (to populate the project stores):
     *
     * ```javascript
     * project.json = '{"eventsData":[...],"resourcesData":[...],...}'
     * ```
     *
     * @property {String}
     */
    get json() {
        return super.json;
    }

    set json(json) {
        if (typeof json === 'string') {
            json = StringHelper.safeJsonParse(json);
        }

        this.loadInlineData(json);
    }

    //endregion

    afterChange(toSet, wasSet) {
        super.afterChange(...arguments);

        if (wasSet.calendar) {
            this.trigger('calendarChange');
        }
    }

    refreshWbs(options) {
        const
            me = this,
            children = me.unfilteredChildren ?? me.children;

        if (children && children.length) {
            // We leverage the refreshWbs() method of TaskModel (our children) to do the work. This node does not
            // have a wbsIndex, so we pass -1 for the index to skip on to just our children.
            children[0].refreshWbs?.call(me, options, -1);
        }
    }
}

ProjectModel.applyConfigs = true;

// Ignored to keep autogenerated typescript typings w/o errors, otherwise there'll be 2 EffectResolutionResult definitions
/*
 * @typedef EffectResolutionResult
 * @property {Number} Cancel    Stop propagation
 * @property {Number} Restart   Restart propagation
 * @property {Number} Resume    Resume propagation from current state
 */
