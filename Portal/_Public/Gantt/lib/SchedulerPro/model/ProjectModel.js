import { SchedulerProProjectMixin } from '../../Engine/quark/model/scheduler_pro/SchedulerProProjectMixin.js';
import ProjectModelMixin from '../../Scheduler/model/mixin/ProjectModelMixin.js';
import ProjectChangeHandlerMixin from './mixin/ProjectChangeHandlerMixin.js';

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
import StateTrackingManager from '../data/stm/StateTrackingManager.js';

/**
 * @module SchedulerPro/model/ProjectModel
 */

/**
 * Scheduler Pro Project model class - a central place for all data.
 *
 * It holds and links the stores usually used by Scheduler Pro:
 *
 * - {@link SchedulerPro/data/EventStore}
 * - {@link SchedulerPro/data/ResourceStore}
 * - {@link SchedulerPro/data/AssignmentStore}
 * - {@link SchedulerPro/data/DependencyStore}
 * - {@link SchedulerPro/data/CalendarManagerStore}
 * - {@link Scheduler/data/ResourceTimeRangeStore}
 * - {@link #config-timeRangeStore TimeRangeStore}
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
 * Please refer to [this guide](#SchedulerPro/guides/basics/project_data.md) for more information.
 *
 * ## Built in CrudManager
 *
 * Scheduler Pro's project has a {@link Scheduler/crud/AbstractCrudManagerMixin CrudManager} built in. Using it is the recommended
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
 * For more information on CrudManager, see Schedulers docs on {@link Scheduler/data/CrudManager}.
 * For a detailed description of the protocol used by CrudManager, see the [Crud manager guide](#Scheduler/guides/data/crud_manager.md)
 *
 * You can access the current Project data changes anytime using the {@link #property-changes} property.
 *
 * ## Working with inline data
 *
 * The project provides an {@link #property-inlineData} getter/setter that can
 * be used to manage data from all Project stores at once. Populating the stores this way can
 * be useful if you do not want to use the CrudManager for server communication but instead load data using Axios
 * or similar.
 *
 * ### Getting data
 * ```javascript
 * const data = scheduler.project.inlineData;
 *
 * // use the data in your application
 * ```
 *
 * ### Setting data
 * ```javascript
 * // Get data from server manually
 * const data = await axios.get('/project?id=12345');
 *
 * // Feed it to the project
 * scheduler.project.inlineData = data;
 * ```
 *
 * See also {@link #function-loadInlineData}
 *
 * ### Getting changed records
 *
 * You can access the changes in the current Project dataset anytime using the {@link #property-changes} property. It
 * returns an object with all changes:
 *
 * ```javascript
 * const changes = project.changes;
 *
 * console.log(changes);
 *
 * > {
 *   tasks : {
 *       updated : [{
 *           name : 'My task',
 *           id   : 12
 *       }]
 *   },
 *   assignments : {
 *       added : [{
 *           event      : 12,
 *           resource   : 7,
 *           units      : 100,
 *           $PhantomId : 'abc123'
 *       }]
 *     }
 * };
 * ```
 *
 * ## Monitoring data changes
 *
 * While it is possible to listen for data changes on the projects individual stores, it is sometimes more convenient
 * to have a centralized place to handle all data changes. By listening for the {@link #event-change change event} your
 * code gets notified when data in any of the stores changes. Useful for example to keep an external data model up to
 * date:
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     project: {
 *         listeners : {
 *             change({ store, action, records }) {
 *                 const { $name } = store.constructor;
 *
 *                 if (action === 'add') {
 *                     externalDataModel.add($name, records);
 *                 }
 *
 *                 if (action === 'remove') {
 *                     externalDataModel.remove($name, records);
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ## Processing the data loaded from the server
 *
 * If you want to process the data received from the server after loading, you can use
 * the {@link #event-beforeLoadApply} or {@link #event-beforeSyncApply} events:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     project: {
 *         listeners : {
 *             beforeLoadApply({ response }) {
 *                 // do something with load-response object before data is fed to the stores
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ## Built in StateTrackingManager
 *
 * The project also has a built in {@link Core/data/stm/StateTrackingManager} (STM for short), that
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
 * @mixes Core/mixin/Events
 * @mixes SchedulerPro/data/mixin/PartOfProject
 * @mixes SchedulerPro/data/mixin/ProjectCrudManager
 * @mixes SchedulerPro/model/mixin/ProjectChangeHandlerMixin
 *
 * @extends Scheduler/model/mixin/ProjectModelMixin
 *
 * @typings Scheduler.model.ProjectModel -> Scheduler.model.SchedulerProjectModel
 */
export default class ProjectModel extends ProjectChangeHandlerMixin(ProjectCrudManager(ProjectModelMixin(SchedulerProProjectMixin))) {
    //region Events

    /**
     * Fired when the engine has finished its calculations and the results has been written back to the records.
     *
     * ```javascript
     * scheduler.project.on({
     *     dataReady() {
     *        console.log('Calculations finished');
     *     }
     * });
     *
     * scheduler.eventStore.first.duration = 10;
     *
     * // At some point a bit later it will log 'Calculations finished'
     * ```
     *
     * @event dataReady
     * @param {SchedulerPro.model.ProjectModel} source The project
     * @param {Boolean} isInitialCommit Flag that shows if this commit is initial
     * @param {Set} records Set of all {@link Core.data.Model}s that were modified in the completed transaction.
     * Use the {@link Core.data.Model#property-modifications} property of each Model to identify
     * modified fields.
     */

    /**
     * Fired during the Engine calculation if {@link #config-enableProgressNotifications enableProgressNotifications} config is `true`
     * @event progress
     * @param {Number} total The total number of operations
     * @param {Number} remaining The number of remaining operations
     * @param {'storePopulation'|'propagating'} phase The phase of the calculation, either 'storePopulation'
     * when data is getting loaded, or 'propagating' when data is getting calculated
     */

    /**
     * Fired when the Engine detects a computation cycle.
     * @event cycle
     * @param {Object} schedulingIssue Scheduling error describing the case:
     * @param {Function} schedulingIssue.getDescription Returns the cycle description
     * @param {Object} schedulingIssue.cycle Object providing the cycle info
     * @param {Function} schedulingIssue.getResolutions Returns possible resolutions
     * @param {Function} continueWithResolutionResult Function to call after a resolution is chosen to
     * proceed with the Engine calculations:
     * ```js
     * project.on('cycle', ({ continueWithResolutionResult }) => {
     *     // cancel changes in case of a cycle
     *     continueWithResolutionResult(EffectResolutionResult.Cancel);
     * })
     * ```
     */

    /**
     * Fired when the Engine detects a scheduling conflict.
     * @event schedulingConflict
     * @param {Object} schedulingIssue The conflict details:
     * @param {Function} schedulingIssue.getDescription Returns the conflict description
     * @param {Object[]} schedulingIssue.intervals Array of conflicting intervals
     * @param {Function} schedulingIssue.getResolutions Function to get possible resolutions
     * @param {Function} continueWithResolutionResult Function to call after a resolution is chosen to
     * proceed with the Engine calculations:
     * ```js
     * project.on('schedulingConflict', ({ schedulingIssue, continueWithResolutionResult }) => {
     *     // apply the first resolution and continue
     *     schedulingIssue.getResolutions()[0].resolve();
     *     continueWithResolutionResult(EffectResolutionResult.Resume);
     * })
     * ```
     */

    /**
     * Fired when the Engine detects a calendar misconfiguration when the calendar does
     * not provide any working periods of time which makes the calendar usage impossible.
     * @event emptyCalendar
     * @param {Object} schedulingIssue Scheduling error describing the case:
     * @param {Function} schedulingIssue.getDescription Returns the error description
     * @param {Function} schedulingIssue.getCalendar Returns the calendar that must be fixed
     * @param {Function} schedulingIssue.getResolutions Returns possible resolutions
     * @param {Function} continueWithResolutionResult Function to call after a resolution is chosen to
     * proceed with the Engine calculations:
     * ```js
     * project.on('emptyCalendar', ({ schedulingIssue, continueWithResolutionResult }) => {
     *     // apply the first resolution and continue
     *     schedulingIssue.getResolutions()[0].resolve();
     *     continueWithResolutionResult(EffectResolutionResult.Resume);
     * })
     * ```
     */

    //endregion

    //region Config

    static get $name() {
        return 'ProjectModel';
    }

    /**
     * Class implementing resource allocation report used by
     * {@link SchedulerPro.view.ResourceHistogram resource histogram} and
     * {@link SchedulerPro.view.ResourceUtilization resource utilization} views
     * for collecting resource allocation.
     * @config {ResourceAllocationInfo} resourceAllocationInfoClass
     */

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
     * @category Advanced
     */

    /**
     * Maximum range the project calendars can iterate.
     * The value is defined in milliseconds and by default equals `5 years` roughly.
     * ```javascript
     * new SchedulerPro({
     *     project : {
     *         // adjust calendar iteration limit to 10 years roughly:
     *         // 10 years expressed in ms
     *         maxCalendarRange : 10 * 365 * 24 * 3600000,
     *         ...
     *     }
     * });
     * ```
     * @config {Number} maxCalendarRange
     * @default 157680000000
     * @category Advanced
     */

    /**
     * When `true` the project manually scheduled tasks will adjust their proposed start/end dates
     * to skip non working time.
     *
     * @field {Boolean} skipNonWorkingTimeWhenSchedulingManually
     * @default false
     */

    /**
     * When `true` the project's manually scheduled tasks adjust their duration by excluding the non-working time from it,
     * according to the calendar. However, this may lead to inconsistencies, when moving an event which both starts
     * and ends on the non-working time. For such cases you can disable this option.
     *
     * Default value is `true`
     *
     * IMPORTANT: Setting this option to `false` also forcefully sets the {@link #field-skipNonWorkingTimeWhenSchedulingManually} option
     * to `false`.
     * IMPORTANT: This option is going to be disabled by default from version 6.0.0.
     *
     * @field {Boolean} skipNonWorkingTimeInDurationWhenSchedulingManually
     * @default true
     */

    /**
     * This config manages DST correction in the scheduling engine. It only has effect when DST transition hour is
     * working time. Usually DST transition occurs on Sunday, so with non working weekends the DST correction logic
     * is not involved.
     *
     * If **true**, it will add/remove one hour when calculating end date. For example:
     * Assume weekends are working and on Sunday, 2020-10-25 at 03:00 clocks are set back 1 hour. Assume there is an event:
     *
     * ```javascript
     * {
     *     startDate    : '2020-10-20',
     *     duration     : 10 * 24 + 1,
     *     durationUnit : 'hour'
     * }
     * ```
     * It will end on 2020-10-30 01:00 (which is wrong) but duration will be reported correctly. Because of the DST
     * transition the SchedulerPro project will add one more hour when calculating the end date.
     *
     * Also this may occur when day with DST transition is working but there are non-working intervals between that day
     * and event end date.
     *
     * ```javascript
     * {
     *     calendar         : 1,
     *     calendarsData    : [
     *         {
     *             id           : 1,
     *             startDate    : '2020-10-26',
     *             endDate      : '2020-10-27',
     *             isWorking    : false
     *         }
     *     ],
     *     eventsData       : [
     *         {
     *             id           : 1,
     *             startDate    : '2020-10-20',
     *             endDate      : '2020-10-30'
     *         },
     *         {
     *             id           : 2,
     *             startDate    : '2020-10-20',
     *             duration     : 10 * 24 + 1,
     *             durationUnit : 'hour'
     *         }
     *     ]
     * }
     * ```
     *
     * Event 1 duration will be incorrectly reported as 9 days * 24 hours, missing 1 extra hour added by DST transition.
     * Event 2 end date will be calculated to 2020-10-30 01:00, adding one extra hour.
     *
     * If **false**, the SchedulerPro project will not add DST correction which fixes the quirk mentioned above.
     * Event 1 duration will be correctly reported as 9 days * 24 hours + 1 hour. Event 2 end date will be calculated
     * to 2020-10-30.
     *
     * Also, for those events days duration will be a floating point number due to extra (or missing) hour:
     *
     * ```javascript
     * eventStore.getById(1).getDuration('day')  // 10.041666666666666
     * eventStore.getById(1).getDuration('hour') // 241
     * ```
     *
     * @config {Boolean} adjustDurationToDST
     * @default false
     */



    /**
     * The number of hours per day.
     *
     * **Please note:** the value **does not define** the amount of **working** time per day
     * for that purpose one should use calendars.
     *
     * The value is used when converting the duration from one unit to another.
     * So when user enters a duration of, for example, `5 days` the system understands that it
     * actually means `120 hours` and schedules accordingly.
     * @field {Number} hoursPerDay
     * @default 24
     */

    /**
     * The number of days per week.
     *
     * **Please note:** the value **does not define** the amount of **working** time per week
     * for that purpose one should use calendars.
     *
     * The value is used when converting the duration from one unit to another.
     * So when user enters a duration of, for example, `2 weeks` the system understands that it
     * actually means `14 days` (which is then converted to {@link #field-hoursPerDay hours}) and
     * schedules accordingly.
     * @field {Number} daysPerWeek
     * @default 7
     */

    /**
     * The number of days per month.
     *
     * **Please note:** the value **does not define** the amount of **working** time per month
     * for that purpose one should use calendars.
     *
     * The value is used when converting the duration from one unit to another.
     * So when user enters a duration of, for example, `1 month` the system understands that it
     * actually means `30 days` (which is then converted to {@link #field-hoursPerDay hours}) and
     * schedules accordingly.
     * @field {Number} daysPerMonth
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
     * @field {'ToEvent'|'FromEvent'|'Project'} dependenciesCalendar
     * @default 'ToEvent'
     */

    /**
     * The project calendar.
     * @field {SchedulerPro.model.CalendarModel} calendar
     * @accepts {String|CalendarModelConfig|SchedulerPro.model.CalendarModel}
     */

    /**
     * Returns current Project changes as an object consisting of added/modified/removed arrays of records for every
     * managed store. Returns `null` if no changes exist. Format:
     *
     * ```javascript
     * {
     *     resources : {
     *         added    : [{ name : 'New guy' }],
     *         modified : [{ id : 2, name : 'Mike' }],
     *         removed  : [{ id : 3 }]
     *     },
     *     events : {
     *         modified : [{  id : 12, name : 'Cool task' }]
     *     },
     *     ...
     * }
     * ```
     *
     * @member {Object} changes
     * @readonly
     * @category Models & Stores
     */

    /**
     * Project changes (CRUD operations to records in its stores) are automatically committed on a buffer to the
     * underlying graph based calculation engine. The engine performs it calculations async.
     *
     * By calling this function, the commit happens right away. And by awaiting it you are sure that project
     * calculations are finished and that references between records are up to date.
     *
     * The returned promise is resolved with an object. If that object has `rejectedWith` set, there has been a conflict and the calculation failed.
     *
     * ```javascript
     * // Move an event in time
     * eventStore.first.shift(1);
     *
     * // Trigger calculations directly and wait for them to finish
     * const result = await project.commitAsync();
     *
     * if (result.rejectedWith) {
     *     // there was a conflict during the scheduling
     * }
     * ```
     *
     * @async
     * @function commitAsync
     * @category Common
     */

    /**
     * Set to `true` to enable calculation progress notifications.
     * When enabled the project fires {@link #event-progress progress} event.
     *
     * **Note**: Enabling progress notifications will impact calculation performance, since it needs to pause calculations to allow redrawing the UI.
     * @config {Boolean} enableProgressNotifications
     * @category Advanced
     */
    /**
     * Enables/disables the calculation progress notifications.
     * @member {Boolean} enableProgressNotifications
     * @category Advanced
     */

    /**
     * If this flag is set to `true` (default) when a start/end date is set on the event, a corresponding
     * `start-no-earlier/later-than` constraint is added, automatically. This is done in order to
     * keep the event "attached" to this date, according to the user intention.
     *
     * Depending on your use case, you might want to disable this behaviour.
     *
     * @field {Boolean} addConstraintOnDateSet
     * @default true
     */

    static get defaultConfig() {
        return {
            /**
             * @hideproperties project, taskStore
             */

            //region Inline data configs & properties

            /**
             * Get/set {@link #property-eventStore} data.
             *
             * Always returns an array of {@link SchedulerPro.model.EventModel EventModels} but also accepts an array of
             * its configuration objects as input.
             *
             * @member {SchedulerPro.model.EventModel[]} events
             * @accepts {SchedulerPro.model.EventModel[]|EventModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-eventStore}. Should be an array of
             * {@link SchedulerPro.model.EventModel EventModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.EventModel[]|EventModelConfig[]} events
             * @category Inline data
             */

            /**
             * Get/set {@link #property-resourceStore} data.
             *
             * Always returns an array of {@link SchedulerPro.model.ResourceModel ResourceModels} but also accepts an
             * array of its configuration objects as input.
             *
             * @member {SchedulerPro.model.ResourceModel[]} resources
             * @accepts {SchedulerPro.model.ResourceModel[]|ResourceModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-resourceStore}. Should be an array of
             * {@link SchedulerPro.model.ResourceModel ResourceModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.ResourceModel[]|ResourceModelConfig[]} resources
             * @category Inline data
             */

            /**
             * Get/set {@link #property-assignmentStore} data.
             *
             * Always returns an array of {@link SchedulerPro.model.AssignmentModel AssignmentModels} but also accepts
             * an array of its configuration objects as input.
             *
             * @member {SchedulerPro.model.AssignmentModel[]} assignments
             * @accepts {SchedulerPro.model.AssignmentModel[]|AssignmentModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-assignmentStore}. Should be an array of
             * {@link SchedulerPro.model.AssignmentModel AssignmentModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.AssignmentModel[]|AssignmentModelConfig[]} assignments
             * @category Inline data
             */

            /**
             * Get/set {@link #property-dependencyStore} data.
             *
             * Always returns an array of {@link SchedulerPro.model.DependencyModel DependencyModels} but also accepts an
             * array of its configuration objects as input.
             *
             * @member {SchedulerPro.model.DependencyModel[]} dependencies
             * @accepts {SchedulerPro.model.DependencyModel[]|DependencyModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-dependencyStore}. Should be an array of
             * {@link SchedulerPro.model.DependencyModel DependencyModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.DependencyModel[]|DependencyModelConfig[]} dependencies
             * @category Inline data
             */

            /**
             * Get/set {@link #property-timeRangeStore} data.
             *
             * Always returns an array of {@link Scheduler.model.TimeSpan TimeSpans} but also accepts an
             * array of its configuration objects as input.
             *
             * @member {Scheduler.model.TimeSpan[]} timeRanges
             * @accepts {Scheduler.model.TimeSpan[]|TimeSpanConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-timeRangeStore}. Should be an array of
             * {@link Scheduler.model.TimeSpan TimeSpans} or its configuration objects.
             *
             * @config {Scheduler.model.TimeSpan[]|TimeSpanConfig[]} timeRanges
             * @category Inline data
             */

            /**
             * Get/set {@link #property-resourceTimeRangeStore} data.
             *
             * Always returns an array of {@link Scheduler.model.ResourceTimeRangeModel ResourceTimeRangeModels} but
             * also accepts an array of its configuration objects as input.
             *
             * @member {Scheduler.model.ResourceTimeRangeModel[]} resourceTimeRanges
             * @accepts {Scheduler.model.ResourceTimeRangeModel[]|ResourceTimeRangeModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-resourceTimeRangeStore}. Should be an array
             * of {@link Scheduler.model.ResourceTimeRangeModel ResourceTimeRangeModels} or its configuration objects.
             *
             * @config {Scheduler.model.ResourceTimeRangeModel[]|ResourceTimeRangeModelConfig[]} resourceTimeRanges
             * @category Inline data
             */

            //endregion

            //region Legacy inline data configs & properties

            /**
             * The initial data, to fill the {@link #property-eventStore eventStore} with.
             * Should be an array of {@link SchedulerPro.model.EventModel EventModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.EventModel[]} eventsData
             * @category Legacy inline data
             */

            /**
             * The initial data, to fill the {@link #property-dependencyStore dependencyStore} with.
             * Should be an array of {@link SchedulerPro.model.DependencyModel DependencyModels} or its configuration
             * objects.
             *
             * @config {SchedulerPro.model.DependencyModel[]} [dependenciesData]
             * @category Legacy inline data
             */

            /**
             * The initial data, to fill the {@link #property-resourceStore resourceStore} with.
             * Should be an array of {@link SchedulerPro.model.ResourceModel ResourceModels} or its configuration objects.
             *
             * @config {SchedulerPro.model.ResourceModel[]} [resourcesData]
             * @category Legacy inline data
             */

            /**
             * The initial data, to fill the {@link #property-assignmentStore assignmentStore} with.
             * Should be an array of {@link SchedulerPro.model.AssignmentModel AssignmentModels} or its configuration
             * objects.
             *
             * @config {SchedulerPro.model.AssignmentModel[]} [assignmentsData]
             * @category Legacy inline data
             */

            //endregion

            //region Store configs and properties

            /**
             * The {@link SchedulerPro.data.EventStore store} holding the event information.
             *
             * See also {@link SchedulerPro.model.EventModel}
             *
             * @member {SchedulerPro.data.EventStore} eventStore
             * @category Models & Stores
             */
            /**
             * An {@link SchedulerPro.data.EventStore} instance or a config object.
             * @config {SchedulerPro.data.EventStore|EventStoreConfig} eventStore
             * @category Models & Stores
             */

            /**
             * The {@link SchedulerPro.data.DependencyStore store} holding the dependency information.
             *
             * See also {@link SchedulerPro.model.DependencyModel}
             *
             * @member {SchedulerPro.data.DependencyStore} dependencyStore
             * @category Models & Stores
             */
            /**
             * A {@link SchedulerPro.data.DependencyStore} instance or a config object.
             * @config {SchedulerPro.data.DependencyStore|DependencyStoreConfig} dependencyStore
             * @category Models & Stores
             */

            /**
             * The {@link SchedulerPro.data.ResourceStore store} holding the resources that can be assigned to the
             * events in the event store.
             *
             * See also {@link SchedulerPro.model.ResourceModel}
             *
             * @member {SchedulerPro.data.ResourceStore} resourceStore
             * @category Models & Stores
             */
            /**
             * A {@link SchedulerPro.data.ResourceStore} instance or a config object.
             * @config {SchedulerPro.data.ResourceStore|ResourceStoreConfig} resourceStore
             * @category Models & Stores
             */

            /**
             * The {@link SchedulerPro.data.AssignmentStore store} holding the assignment information.
             *
             * See also {@link SchedulerPro.model.AssignmentModel}
             *
             * @member {SchedulerPro.data.AssignmentStore} assignmentStore
             * @category Models & Stores
             */
            /**
             * An {@link SchedulerPro.data.AssignmentStore} instance or a config object.
             * @config {SchedulerPro.data.AssignmentStore|AssignmentStoreConfig} assignmentStore
             * @category Models & Stores
             */

            /**
             * The {@link SchedulerPro.data.CalendarManagerStore store} holding the calendar information.
             *
             * See also {@link SchedulerPro.model.CalendarModel}
             * @member {SchedulerPro.data.CalendarManagerStore} calendarManagerStore
             * @category Models & Stores
             */
            /**
             * A {@link SchedulerPro.data.CalendarManagerStore} instance or a config object.
             * @config {SchedulerPro.data.CalendarManagerStore|CalendarManagerStoreConfig} calendarManagerStore
             * @category Models & Stores
             */

            //endregion

            //region Model & store class configs

            /**
             * The constructor of the calendar model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the
             * {@link #property-calendarManagerStore}
             *
             * @config {SchedulerPro.model.CalendarModel} [calendarModelClass]
             * @typings {typeof CalendarModel}
             * @category Models & Stores
             */
            calendarModelClass : CalendarModel,

            /**
             * The constructor of the dependency model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-dependencyStore}
             *
             * @config {SchedulerPro.model.DependencyModel}
             * @typings {typeof DependencyModel}
             * @category Models & Stores
             */
            dependencyModelClass : DependencyModel,

            /**
             * The constructor of the event model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-eventStore}
             *
             * @config {SchedulerPro.model.EventModel}
             * @typings {typeof EventModel}
             * @category Models & Stores
             */
            eventModelClass : EventModel,

            /**
             * The constructor of the assignment model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-assignmentStore}
             *
             * @config {SchedulerPro.model.AssignmentModel}
             * @typings {typeof AssignmentModel}
             * @category Models & Stores
             */
            assignmentModelClass : AssignmentModel,

            /**
             * The constructor of the resource model class, to be used in the project. Will be set as the
             * {@link Core.data.Store#config-modelClass modelClass} property of the {@link #property-resourceStore}
             *
             * @config {SchedulerPro.model.ResourceModel}
             * @typings {typeof ResourceModel}
             * @category Models & Stores
             */
            resourceModelClass : ResourceModel,

            /**
             * The constructor to create a calendar store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.CalendarManagerStore}
             * @config {SchedulerPro.data.CalendarManagerStore|Object}
             * @typings {typeof CalendarManagerStore|object}
             * @category Models & Stores
             */
            calendarManagerStoreClass : CalendarManagerStore,

            /**
             * The constructor to create a dependency store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.DependencyStore}
             * @config {SchedulerPro.data.DependencyStore|Object}
             * @typings {typeof DependencyStore|object}
             * @category Models & Stores
             */
            dependencyStoreClass : DependencyStore,

            /**
             * The constructor to create an event store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.EventStore}
             * @config {SchedulerPro.data.EventStore|Object}
             * @typings {typeof EventStore|object}
             * @category Models & Stores
             */
            eventStoreClass : EventStore,

            /**
             * The constructor to create an assignment store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.AssignmentStore}
             * @config {SchedulerPro.data.AssignmentStore|Object}
             * @typings {typeof AssignmentStore|object}
             * @category Models & Stores
             */
            assignmentStoreClass : AssignmentStore,

            /**
             * The constructor to create a resource store instance with. Should be a class, subclassing the
             * {@link SchedulerPro.data.ResourceStore}
             * @config {SchedulerPro.data.ResourceStore|Object}
             * @typings {typeof ResourceStore|object}
             * @category Models & Stores
             */
            resourceStoreClass : ResourceStore,

            //endregion

            /**
             * The initial data, to fill the {@link #property-calendarManagerStore} with.
             * Should be an array of {@link SchedulerPro.model.CalendarModel} or it's configuration objects.
             *
             * @config {SchedulerPro.model.CalendarModel[]}
             * @category Legacy inline data
             */
            calendarsData : null,

            /**
             * Set to `true` to reset the undo/redo queues of the internal {@link Core.data.stm.StateTrackingManager}
             * after the Project has loaded. Defaults to `false`
             * @config {Boolean} resetUndoRedoQueuesAfterLoad
             * @category Advanced
             */

            supportShortSyncResponseNote : 'Note: Please consider enabling "supportShortSyncResponse" option to allow less detailed sync responses (https://bryntum.com/products/schedulerpro/docs/api/SchedulerPro/model/ProjectModel#config-supportShortSyncResponse)',

            /**
             * Enables early rendering in SchedulerPro, by postponing calculations to after the first refresh.
             *
             * Requires event data loaded to be pre-normalized to function as intended, since it will be used to render
             * before engine has normalized the data. Given un-normalized data events will snap into place when
             * calculations are finished.
             *
             * The Gantt chart will be read-only until the initial calculations are finished.
             *
             * @config {Boolean}
             * @default
             * @category Advanced
             */
            delayCalculation : true,

            calendarManagerStore : {},

            stmClass : StateTrackingManager
        };
    }

    static get configurable() {
        return {
            /**
             * Get/set {@link #property-calendarManagerStore} data.
             *
             * Always returns a {@link SchedulerPro.model.CalendarModel} array but also accepts an array of
             * its configuration objects as input.
             *
             * @member {SchedulerPro.model.CalendarModel[]} calendars
             * @accepts {SchedulerPro.model.CalendarModel[]|CalendarModelConfig[]}
             * @category Inline data
             */
            /**
             * Data use to fill the {@link #property-eventStore}. Should be a {@link SchedulerPro.model.CalendarModel}
             * array or its configuration objects.
             *
             * @config {SchedulerPro.model.CalendarModel[]|CalendarModelConfig[]} calendars
             * @category Inline data
             */
            calendars : null
        };
    }

    // For TaskBoard compatibility
    get taskStore() {
        return this.eventStore;
    }

    //endregion

    //region Inline data

    get calendars() {
        return this.calendarManagerStore.allRecords;
    }

    updateCalendars(calendars) {
        this.calendarManagerStore.data = calendars;
    }

    //endregion
}
