import { SchedulerProProjectMixin } from '../../Engine/quark/model/scheduler_pro/SchedulerProProjectMixin.js';
import ProjectModelMixin from '../../Scheduler/model/mixin/ProjectModelMixin.js';

import ProjectCrudManager from '../data/mixin/ProjectCrudManager.js';

import AssignmentModel from './AssignmentModel.js';
import CalendarModel from './CalendarModel.js';
import DependencyModel from './DependencyModel.js';
import EventModel from './EventModel.js';
import ResourceModel from './ResourceModel.js';

import CalendarManagerStore from '../data/CalendarManagerStore.js';
import DependencyStore from '../data/DependencyStore.js';
import EventStore from '../data/EventStore.js';
import ResourceStore from '../data/ResourceStore.js';
import AssignmentStore from '../data/AssignmentStore.js';

/**
 * @module SchedulerPro/model/ProjectModel
 */

/**
 * Scheduler Pro Project model class - a central place for all data.
 *
 * It holds and links the stores usually used by Scheduler Pro:
 *
 * - {@link SchedulerPro.data.AssignmentStore}
 * - {@link SchedulerPro.data.CalendarManagerStore}
 * - {@link SchedulerPro.data.DependencyStore}
 * - {@link SchedulerPro.data.ResourceStore}
 * - {@link SchedulerPro.data.EventStore}
 *
 * The project uses a scheduling engine to calculate dates, durations and such. It is also responsible for
 * handling references between models, for example to link an event via an assignment to a resource. These operations
 * are asynchronous, a fact that is hidden when working in the Scheduler Pro UI but which you must know about when
 * performing operations on the data level.
 *
 * When there is a change to data that requires something else to be recalculated, the project schedules a calculation
 * (a commit) which happens moments later. It is also possible to trigger these calculations directly. This flow
 * illustrates the process:
 *
 * 1. Something changes which requires the project to recalculate, for example adding a new task:
 *
 * ```javascript
 * const [event] = project.eventStore.add({ startDate, endDate });
 * ```
 *
 * 2. A recalculation is scheduled, thus:
 *
 * ```javascript
 * event.duration; // <- Not yet calculated
 * ```
 *
 * 3. Calculate now instead of waiting for the scheduled calculation
 *
 * ```javascript
 * await project.commitAsync();
 *
 * event.duration; // <- Now available
 * ```
 *
 * Please refer to [this guide](#guides/schedulerpro/project_data.md) for more information.
 *
 * ## Built in CrudManger
 *
 * Scheduler Pro's project has a {@link Scheduler.data.CrudManager CrudManager} built in. Using it is the recommended
 * way of syncing data between Scheduler Pro and a backend. Example usage:
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
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
 * scheduler.project.load()
 * ```
 *
 * For more information on CrudManager, see Schedulers docs on {@link Scheduler.data.CrudManager}.
 * For a detailed description of the protocol used by CrudManager, see the [Crud manager guide](#guides/data/crud_manager.md)
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
 * @mixes SchedulerPro/data/mixin/PartOfProject
 * @mixes SchedulerPro/data/mixin/ProjectCrudManager
 * @mixes Core/mixin/Events
 *
 * @extends Scheduler/model/mixin/ProjectModelMixin
 *
 * @typings Scheduler/model/ProjectModel -> Scheduler/model/SchedulerProjectModel
 */
export default class ProjectModel extends ProjectCrudManager(SchedulerProProjectMixin.derive(ProjectModelMixin())) {

    //region Config

    static get $name() {
        return 'ProjectModel';
    }

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
     * new SchedulerPro{
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

    /**
     * The number of hours per day (is used when converting the duration from one unit to another).
     * @field {Number} hoursPerDay
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
     * The scheduling direction of the project events.
     * Possible values are `Forward` and `Backward`. The `Forward` direction corresponds to the As-Soon-As-Possible scheduling (ASAP),
     * `Backward` - to As-Late-As-Possible (ALAP).
     * @field {String} direction
     * @default 'Forward'
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
     * @field {SchedulerPro.model.CalendarModel} calendar
     */

    /**
     * The project calendar.
     * @member {SchedulerPro.model.CalendarModel} calendar
     */

    /**
     * Causes the scheduling engine to re-evaluate the task data and all associated data and constraints
     * and apply necessary changes.
     * @async
     * @returns {Promise}
     * @function commitAsync
     */

    /**
     * DEPRECATED. Use {@link #function-commitAsync} instead.
     * @deprecated
     * @returns {Promise}
     * @function propagate
     */

    /**
     * Collection of the project calendars.
     * @property {SchedulerPro.data.CalendarManagerStore} calendarManagerStore
     */

    static get defaultConfig() {
        return {
            calendarModelClass : CalendarModel,

            /**
             * The constructor of the dependency model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-dependencyStore}
             *
             * @config {SchedulerPro.model.DependencyModel}
             * @category Models & Stores
             */
            dependencyModelClass : DependencyModel,

            /**
             * The constructor of the event model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-eventStore}
             *
             * @config {SchedulerPro.model.EventModel}
             * @category Models & Stores
             */
            eventModelClass : EventModel,

            /**
             * The constructor of the assignment model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-assignmentStore}
             *
             * @config {SchedulerPro.model.AssignmentModel}
             * @category Models & Stores
             */
            assignmentModelClass : AssignmentModel,

            /**
             * The constructor of the resource model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-resourceStore}
             *
             * @config {SchedulerPro.model.ResourceModel}
             * @category Models & Stores
             */
            resourceModelClass : ResourceModel,

            calendarManagerStoreClass : CalendarManagerStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.DependencyStore}
             * @config {SchedulerPro.data.DependencyStore|Object}
             * @category Models & Stores
             */
            dependencyStoreClass : DependencyStore,

            /**
             * The constructor to create an event store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.EventStore}
             * @config {SchedulerPro.data.EventStore|Object}
             * @category Models & Stores
             */
            eventStoreClass : EventStore,

            /**
             * The constructor to create an assignment store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.AssignmentStore}
             * @config {SchedulerPro.data.AssignmentStore|Object}
             * @category Models & Stores
             */
            assignmentStoreClass : AssignmentStore,

            /**
             * The constructor to create a resource store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.ResourceStore}
             * @config {SchedulerPro.data.ResourceStore|Object}
             * @category Models & Stores
             */
            resourceStoreClass : ResourceStore
        };
    }

    //endregion

}
