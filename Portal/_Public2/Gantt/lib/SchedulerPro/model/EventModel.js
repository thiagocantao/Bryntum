import PartOfProject from '../data/mixin/PartOfProject.js';
import PercentDoneMixin from './mixin/PercentDoneMixin.js';
import { SchedulerProEvent } from '../../Engine/quark/model/scheduler_pro/SchedulerProEvent.js';
import EventModelMixin from '../../Scheduler/model/mixin/EventModelMixin.js';
import TimeSpan from '../../Scheduler/model/TimeSpan.js';
import RecurringTimeSpan from '../../Scheduler/model/mixin/RecurringTimeSpan.js';

/**
 * @module SchedulerPro/model/EventModel
 */

/**
 * This class represent a single event in your schedule, usually added to a {@link SchedulerPro.data.EventStore}.
 *
 * It is a subclass of the {@link Scheduler.model.TimeSpan}, which is in turn subclass of {@link Core.data.Model}.
 * Please refer to documentation of that class to become familiar with the base interface of the event.
 *
 * ## Async date calculations
 *
 * A record created from an `EventModel` is normally part of an `EventStore`, which in turn is part of a project. When
 * dates or the duration of an event is changed, the project performs async calculations to normalize the other fields.
 * For example if `duration` is change, it will calculate `endDate`.
 *
 * As a result of this being an async operation, the values of other fields are not guaranteed to be up to date
 * immediately after a change. To ensure data is up to date, await the calculations to finish.
 *
 * For example, `endDate` is not up to date after this operation:
 *
 * ```javascript
 * eventRecord.duration = 5;
 * // endDate not yet calculated
 * ```
 *
 * But if calculations are awaited it is up to date:
 *
 * ```javascript
 * eventRecord.duration = 5;
 * await eventRecord.project.commitAsync();
 * // endDate is calculated
 * ```
 *
 * As an alternative, you can also use `setAsync()` to trigger calculations directly after the change:
 *
 * ```javascript
 * await eventRecord.setAsync({ duration : 5});
 * // endDate is calculated
 * ```
 *
 * ## Subclassing the Event model class
 * The Event model has a few predefined fields as seen below. If you want to add new fields or change the options for
 * the existing fields, you can do that by subclassing this class (see example below).
 *
 * ```
 * class MyEvent extends EventModel {
 *
 *     static get fields() {
 *         return [
 *            // Add new field
 *            { name: 'myField', type : 'number', defaultValue : 0 }
 *         ];
 *     },
 *
 *     myCheckMethod() {
 *         return this.myField > 0
 *     },
 *
 *     ...
 * });
 * ```
 * If you in your data want to use other names for the startDate, endDate, resourceId and name fields you can configure
 * them as seen below:
 * ```
 * class MyEvent extends EventModel {
 *
 *     static get fields() {
 *         return [
 *            { name: 'startDate', dataSource 'taskStart' },
 *            { name: 'endDate', dataSource 'taskEnd', format: 'YYYY-MM-DD' },
 *            { name: 'resourceId', dataSource 'userId' },
 *            { name: 'name', dataSource 'taskTitle' },
 *         ];
 *     },
 *     ...
 * });
 * ```
 *
 * Please refer to {@link Core.data.Model} for additional details.
 *
 * @extends Scheduler/model/TimeSpan
 * @mixes Scheduler/model/mixin/RecurringTimeSpan
 * @mixes Scheduler/model/mixin/EventModelMixin
 * @mixes SchedulerPro/model/mixin/PercentDoneMixin
 * @mixes SchedulerPro/data/mixin/PartOfProject
 *
 * @typings Scheduler/model/EventModel -> Scheduler/model/SchedulerEventModel
 */
export default class EventModel extends SchedulerProEvent.derive(TimeSpan).mixin(RecurringTimeSpan, PartOfProject, EventModelMixin, PercentDoneMixin) {

    /**
     * Returns the event store this event is part of.
     *
     * @property {SchedulerPro.data.EventStore}
     * @name eventStore
     * @readonly
     * @typings Scheduler/model/TimeSpan:eventStore -> {Scheduler.data.EventStore|SchedulerPro.data.EventStore}
     */

    /**
     * If given resource is assigned to this event, returns a {@link SchedulerPro.model.AssignmentModel} record.
     * Otherwise returns `null`
     *
     * @method
     * @name getAssignmentFor
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     *
     * @return {SchedulerPro.model.AssignmentModel|null}
     */

    /**
     * This method assigns a resource to this event.
     *
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name assign
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     * @param {Number} [units=100] The `units` field of the new assignment
     *
     * @returns {Promise}
     * @propagating
     */

    /**
     * This method unassigns a resource from this event.
     *
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name unassign
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     *
     * @returns {Promise}
     * @propagating
     */

    /**
     * Sets the calendar of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name setCalendar
     * @param {SchedulerPro.model.CalendarModel} calendar The new calendar. Provide `null` to fall back to the project calendar.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Returns a calendar of the event. If event has never been assigned a calendar the project's calendar will be returned.
     *
     * @method
     * @name getCalendar
     * @returns {SchedulerPro.model.CalendarModel}
     */

    /**
     * Sets the start date of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * Note, that the actually set start date may be adjusted, according to the calendar, by skipping the non-working time forward.
     *
     * @method
     * @name setStartDate
     * @param {Date} date The new start date.
     * @param {Boolean} [keepDuration=true] Whether to keep the duration (and update the end date), while changing the start date, or vice-versa.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Sets the end date of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * Note, that the actually set end date may be adjusted, according to the calendar, by skipping the non-working time backward.
     *
     * @method
     * @name setEndDate
     * @param {Date} date The new end date.
     * @param {Boolean} [keepDuration=false] Whether to keep the duration (and update the start date), while changing the end date, or vice-versa.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Updates the duration (and optionally unit) of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name setDuration
     * @param {Number} duration New duration value
     * @param {String} [unit] New duration unit
     * @returns {Promise}
     * @propagating
     */

    /**
     * Updates the effort (and optionally unit) of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method
     * @name setEffort
     * @param {Number} effort New effort value
     * @param {String} [unit] New effort unit
     * @returns {Promise}
     * @propagating
     */

    /**
     * Sets the constraint type and (optionally) constraining date to the event.
     *
     * @method
     * @name setConstraint
     * @param {String} constraintType Constraint type, please refer to the {@link SchedulerPro.model.EventModel#field-constraintType} for the valid values.
     * @param {Date}   [constraintDate] Constraint date.
     * @returns {Promise}
     * @propagating
     */

    //region Config

    static get $name() {
        return 'EventModel';
    }

    static get isProEventModel() {
        return true;
    }

    static get fields() {
        return [
            /**
             * The numeric part of the event effort (the number of units). The effort of the "parent" events will be automatically set to the sum
             * of efforts of their "child" events
             * @field {number} effort
             * @category Scheduling
             */

            /**
             * The unit part of the event's effort, defaults to "h" (hours). Valid values are:
             *
             * - "millisecond" - Milliseconds
             * - "second" - Seconds
             * - "minute" - Minutes
             * - "hour" - Hours
             * - "day" - Days
             * - "week" - Weeks
             * - "month" - Months
             * - "quarter" - Quarters
             * - "year"- Years
             *
             * This field is readonly after creation, to change it use the {@link #function-setEffort} call.
             * @field {String} effortUnit
             * @default "hour"
             * @category Scheduling
             */

            /**
             * Field storing the event constraint alias or NULL if not constraint set.
             * Valid values are:
             * - "finishnoearlierthan"
             * - "finishnolaterthan"
             * - "mustfinishon"
             * - "muststarton"
             * - "startnoearlierthan"
             * - "startnolaterthan"
             *
             * @field {String} constraintType
             * @category Scheduling
             */

            /**
             * Field defining the constraint boundary date, if applicable.
             * @field {Date} constraintDate
             * @category Scheduling
             */

            /**
             * When set to `true`, the `startDate` of the event will not be changed by any of its incoming dependencies
             * or constraints.
             *
             * @field {Boolean} manuallyScheduled
             * @category Scheduling
             */

            /**
             * A calculated field storing the _early start date_ of the event.
             * The _early start date_ is the earliest possible date the event can start.
             * This value is calculated based on the earliest dates of the event predecessors and the event own constraints.
             * If the event has no predecessors nor other constraints, its early start date matches the project start date.
             * @field {Date} earlyStartDate
             * @calculated
             * @readonly
             * @category Scheduling
             */

            /**
             * A calculated field storing the _early end date_ of the event.
             * The _early end date_ is the earliest possible date the event can finish.
             * This value is calculated based on the earliest dates of the event predecessors and the event own constraints.
             * If the event has no predecessors nor other constraints, its early end date matches the project start date plus the event duration.
             * @field {Date} earlyEndDate
             * @calculated
             * @readonly
             * @category Scheduling
             */

            /**
             * The calendar, assigned to the entity. Allows you to set the time when entity can perform the work.
             *
             * All entities are by default assigned to the project calendar, provided as the {@link SchedulerPro.model.ProjectModel#property-calendar} option.
             *
             * @field {SchedulerPro.model.CalendarModel} calendar
             * @category Scheduling
             */

            /**
             * Set this to true if this task should be shown in the Timeline widget
             * @field {Boolean} showInTimeline
             * @category Common
             */
            { name : 'showInTimeline', type : 'boolean' },

            /**
             * Note about the event
             * @field {String} note
             * @category Common
             */
            'note'
        ];
    }

    //endregion
}
