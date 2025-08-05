import GridRowModel from '../../Grid/data/GridRowModel.js';
import ResourceModelMixin from '../../Scheduler/model/mixin/ResourceModelMixin.js';
import { SchedulerProResourceMixin } from '../../Engine/quark/model/scheduler_pro/SchedulerProResourceMixin.js';
import PartOfProject from '../data/mixin/PartOfProject.js';

/**
 * @module SchedulerPro/model/ResourceModel
 */

/**
 * An object representing a certain time interval.
 *
 * @typedef {Object} TickInfo
 * @property {Date} startDate The interval start date
 * @property {Date} endDate The interval end date
 */

/**
 * An object containing info on the resource allocation in a certain time interval.
 *
 * The object is used when rendering interval bars and tooltips so it additionally provides a `rectConfig` property
 * which contains a configuration object for the `rect` SVG-element representing the interval bar.
 *
 * @typedef {Object} ResourceAllocationInterval
 * @property {SchedulerPro.model.ResourceModel} resource Resource model
 * @property {Set} assignments Set of ongoing assignments for the interval
 * @property {Map} assignmentIntervals Individual ongoing assignments allocation indexed by assignments
 * @property {Number} effort Resource effort in the interval (in milliseconds)
 * @property {Boolean} isOverallocated `true` if the interval contains a fact of the resource overallocation
 * @property {Boolean} isUnderallocated `true` if the resource is underallocated in the interval
 * @property {Number} maxEffort Maximum possible resource effort in the interval (in milliseconds)
 * @property {TickInfo} tick The time interval
 * @property {Number} units Resource allocation in percents
 * @property {Boolean} inEventTimeSpan Indicates if the interval is in the middle of the event timespan.
 */

/**
 * An object containing info on the assignment effort in a certain time interval.
 *
 * The object is used when rendering interval bars and tooltips so it additionally provides a `rectConfig` property
 * which contains a configuration object for the`rect` SVG-element representing the interval bar.
 *
 * @typedef {Object} AssignmentAllocationInterval
 * @property {SchedulerPro.model.AssignmentModel} assignment The assignment which allocation is displayed.
 * @property {Number} effort Amount of work performed by the assigned resource in the interval
 * @property {TickInfo} tick The interval of time the allocation is collected for
 * @property {Number} units Assignment {@link SchedulerPro.model.AssignmentModel#field-units} value
 * @property {Object} rectConfig The rectangle DOM configuration object
 * @property {Boolean} inEventTimeSpan Indicates if the interval is in the middle of the event timespan.
 */

/**
 * Resource allocation information.
 * @typedef ResourceAllocation
 * @property {SchedulerPro.model.ResourceModel} resource Resource model.
 * @property {ResourceAllocationInfo} owner The allocation report this instance is part of.
 * @property {ResourceAllocationInterval[]} total The resource allocation data collected.
 * @property {Map} byAssignments A `Map` keyed by {@link SchedulerPro/model/AssignmentModel} containing the
 * resource allocation collected for individual assignments.
 */

/**
 * Class implementing _resource allocation report_ - data representing the provided `resource`
 * utilization in the provided period of time.
 * The data is grouped by the provided time intervals (`ticks`).
 * @typedef {Object} ResourceAllocationInfo
 * @property {SchedulerPro.model.ResourceModel} resource Resource model.
 * @property {ResourceAllocation} allocation The collected allocation info.
 * @property {SchedulerPro.model.CalendarModel} ticks A calendar specifying intervals to group the collected
 * allocation by. __Working__ time intervals of the calendars will be used for grouping.
 * This also specifies the time period to collect allocation for.
 * So the first interval `startDate` is treated as the period start and the last interval `endDate` is the period end.
 * @property {Boolean} includeInactiveEvents `true` indicates inactive events allocation is included
 * and `false` - it's skipped.
 */

/**
 * This class represent a single Resource in Scheduler Pro, usually added to a {@link SchedulerPro.data.ResourceStore}.
 *
 * It is a subclass of  {@link Core.data.Model}. Please refer to the documentation for that class to become familiar
 * with the base interface of the resource.
 *
 * ## Fields and references
 *
 * A resource has a few predefined fields, see Fields below. If you want to add more fields with meta data describing
 * your resources then you should subclass this class:
 *
 * ```javascript
 * class MyResource extends ResourceModel {
 *   static get fields() {
 *     return [
 *       // "id" and "name" fields are already provided by the superclass
 *       { name: 'company', type : 'string' }
 *     ];
 *   }
 * });
 * ```
 *
 * If you want to use other names in your data for the id and name fields you can configure them as seen below:
 *
 * ```javascript
 * class MyResource extends ResourceModel {
 *   static get fields() {
 *     return [
 *        { name: 'name', dataSource: 'userName' }
 *     ];
 *   },
 * });
 * ```
 *
 * After load and project normalization, these references are accessible (assuming their respective stores are loaded):
 * - `{@link #property-assignments}` - The linked assignment records
 * - `{@link #property-events}` - The linked (through assignments) event records
 *
 * ## Async resolving of references
 *
 * As described above, a resource has links to assignments and events. These references are populated async, using the
 * calculation engine of the project that the resource via its store is a part of. Because of this asyncness, references
 * cannot be used immediately after assignment modifications:
 *
 * ```javascript
 * assignment.resourceId = 2;
 * // resource.assignments is not yet up to date
 * ```
 *
 * To make sure references are updated, wait for calculations to finish:
 *
 * ```javascript
 * assignment.resourceId = 2;
 * await assignment.project.commitAsync();
 * // resource.assignments is up to date
 * ```
 *
 * As an alternative, you can also use `setAsync()` to trigger calculations directly after the change:
 *
 * ```javascript
 * await assignment.setAsync({ resourceId : 2});
 * // resource.assignments is up to date
 * ```
 *
 * @extends Grid/data/GridRowModel
 * @mixes Scheduler/model/mixin/ResourceModelMixin
 *
 * @typings Scheduler.model.ResourceModel -> Scheduler.model.SchedulerResourceModel
 */
export default class ResourceModel extends PartOfProject(ResourceModelMixin(SchedulerProResourceMixin.derive(GridRowModel))) {

    //region Calendar

    /**
     * Sets the calendar of the task. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method setCalendar
     * @param {SchedulerPro.model.CalendarModel} calendar The new calendar. Provide `null` to use the project calendar.
     * @async
     * @propagating
     */

    /**
     * Returns the resource calendar.
     *
     * @method getCalendar
     * @returns {SchedulerPro.model.CalendarModel} The resource calendar.
     */

    /**
     * The calendar, assigned to the entity. Allows you to set the time when entity can perform the work.
     *
     * @field {SchedulerPro.model.CalendarModel} calendar
     * @accepts {SchedulerPro.model.CalendarModel|String}
     * @category Scheduling
     */

    //endregion

    //region Config

    static get $name() {
        return 'ResourceModel';
    }

    //endregion

    /**
     * Get associated events
     *
     * @member {SchedulerPro.model.EventModel[]} events
     * @readonly
     * @category Common
     */

    /**
     * Returns all assignments for the resource
     *
     * @member {SchedulerPro.model.AssignmentModel[]} assignments
     * @category Common
     */

    //region Early render

    get assigned() {
        const { project } = this;

        // Figure assigned events out before buckets are created (if part of project)
        if (project?.assignmentStore.storage._indices?.resource) {
            return project.assignmentStore.storage.findItem('resource', this) ?? new Set();
        }

        return super.assigned;
    }

    set assigned(assigned) {
        super.assigned = assigned;
    }

    //endregion
}
