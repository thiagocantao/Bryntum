import PartOfProject from '../data/mixin/PartOfProject.js';
import PercentDoneMixin from './mixin/PercentDoneMixin.js';
import { SchedulerProEvent } from '../../Engine/quark/model/scheduler_pro/SchedulerProEvent.js';
import Duration from '../../Core/data/Duration.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import EventModelMixin from '../../Scheduler/model/mixin/EventModelMixin.js';
import TimeSpan from '../../Scheduler/model/TimeSpan.js';
import RecurringTimeSpan from '../../Scheduler/model/mixin/RecurringTimeSpan.js';
import EventSegmentModel from './EventSegmentModel.js';

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
 * ```javascript
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
 *
 * If you in your data want to use other names for the startDate, endDate, resourceId and name fields you can configure
 * them as seen below:
 * ```javascript
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
 * @typings Scheduler.model.EventModel -> Scheduler.model.SchedulerEventModel
 */
export default class EventModel extends SchedulerProEvent.derive(TimeSpan).mixin(
    RecurringTimeSpan,
    PartOfProject,
    EventModelMixin,
    PercentDoneMixin
) {

    /**
     * Returns the event store this event is part of.
     *
     * @member {SchedulerPro.data.EventStore} eventStore
     * @readonly
     * @typings Scheduler.model.TimeSpan:eventStore -> {Scheduler.data.EventStore||SchedulerPro.data.EventStore}
     */

    /**
     * If given resource is assigned to this event, returns a {@link SchedulerPro.model.AssignmentModel} record.
     * Otherwise returns `null`
     *
     * @method getAssignmentFor
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     *
     * @returns {SchedulerPro.model.AssignmentModel|null}
     */

    /**
     * This method assigns a resource to this event.
     *
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method assign
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     * @param {Number} [units=100] The `units` field of the new assignment
     *
     * @async
     * @propagating
     */

    /**
     * This method unassigns a resource from this event.
     *
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method unassign
     * @param {SchedulerPro.model.ResourceModel} resource The instance of {@link SchedulerPro.model.ResourceModel}
     *
     * @async
     * @propagating
     */

    /**
     * Sets the calendar of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method setCalendar
     * @param {SchedulerPro.model.CalendarModel} calendar The new calendar. Provide `null` to fall back to the project calendar.
     * @async
     * @propagating
     */

    /**
     * Returns the event calendar.
     *
     * @method getCalendar
     * @returns {SchedulerPro.model.CalendarModel} The event calendar.
     */

    /**
     * Either activates or deactivates the task depending on the passed value.
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method setInactive
     * @param {Boolean} inactive `true` to deactivate the task, `false` to activate it.
     * @async
     * @propagating
     */

    /**
     * Sets the start date of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * Note, that the actually set start date may be adjusted, according to the calendar, by skipping the non-working time forward.
     *
     * @method setStartDate
     * @param {Date} date The new start date.
     * @param {Boolean} [keepDuration=true] Whether to keep the duration (and update the end date), while changing the start date, or vice-versa.
     * @async
     * @propagating
     */

    /**
     * Sets the end date of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * Note, that the actually set end date may be adjusted, according to the calendar, by skipping the non-working time backward.
     *
     * @method setEndDate
     * @param {Date} date The new end date.
     * @param {Boolean} [keepDuration=false] Whether to keep the duration (and update the start date), while changing the end date, or vice-versa.
     * @async
     * @propagating
     */

    /**
     * Updates the duration (and optionally unit) of the event. Will cause the schedule to be updated - returns a `Promise`
     *
     * @method setDuration
     * @param {Number} duration New duration value
     * @param {String} [unit] New duration unit
     * @async
     * @propagating
     */

    /**
     * Sets the constraint type and (optionally) constraining date to the event.
     *
     * @method setConstraint
     * @param {'finishnoearlierthan'|'finishnolaterthan'|'mustfinishon'|'muststarton'|'startnoearlierthan'|'startnolaterthan'|null} constraintType
     * Constraint type, please refer to the {@link #field-constraintType} for the valid
     * values.
     * @param {Date} [constraintDate] Constraint date.
     * @async
     * @propagating
     */

    /**
     * Updates the {@link #field-effort} (and optionally {@link #field-effortUnit unit}) of the event.
     * Will cause the schedule to be updated - returns a `Promise`
     *
     * @method setEffort
     * @param {Number} effort New effort value
     * @param {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} [unit] New effort
     * unit
     * @async
     * @propagating
     */

    /**
     * Sets {@link #field-segments} field value.
     *
     * @method
     * @name setSegments
     * @param {SchedulerPro.model.EventSegmentModel[]} segments Array of segments or null to make the event not segmented.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Splits the event into segments.
     * @method splitToSegments
     * @param {Date} from The date to split this event at.
     * @param {Number} [lag=1] Split duration.
     * @param {String} [lagUnit] Split duration unit.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Merges the event segments.
     * The method merges two provided event segments (and all the segment between them if any).
     * @method mergeSegments
     * @param {SchedulerPro.model.EventSegmentModel} [segment1] First segment to merge.
     * @param {SchedulerPro.model.EventSegmentModel} [segment2] Second segment to merge.
     * @returns {Promise}
     * @propagating
     */

    /**
     * Sets the event {@link #field-ignoreResourceCalendar} field value and triggers rescheduling.
     *
     * @method setIgnoreResourceCalendar
     * @param {Boolean} ignore Provide `true` to ignore the calendars of the assigned resources
     * when scheduling the event. If `false` the event performs only when
     * its own {@link #field-calendar} and some of the assigned
     * resource calendars allow that.
     * @async
     * @propagating
     */

    /**
     * Returns the event {@link #field-ignoreResourceCalendar} field value.
     *
     * @method getIgnoreResourceCalendar
     * @returns {Boolean} The event {@link #field-ignoreResourceCalendar} field value.
     */

    /**
     * The event first segment or null if the event is not segmented.
     * @member {SchedulerPro.model.EventSegmentModel} firstSegment
     */

    /**
     * The event last segment or null if the event is not segmented.
     * @member {SchedulerPro.model.EventSegmentModel} lastSegment
     */

    //region Config

    static get $name() {
        return 'EventModel';
    }

    static isProEventModel = true;

    static get fields() {
        return [
            /**
             * This field is automatically set to `true` when the event is "unscheduled" - user has provided an empty
             * string in one of the UI editors for start date, end date or duration. Such event is not rendered,
             * and does not affect the schedule of its successors.
             *
             * To schedule the event back, enter one of the missing values, so that there's enough information
             * to calculate start date, end date and duration.
             *
             * Note, that setting this field manually does nothing. This field should be persisted, but not updated
             * manually.
             *
             * @field {Boolean} unscheduled
              * @readonly
              * @category Scheduling
             */

            /**
             * Segments of the event that appear when the event gets {@link #function-splitToSegments}.
             * @field {SchedulerPro.model.EventSegmentModel[]} segments
             * @category Scheduling
             */

            /**
             * The current status of a task, expressed as the percentage completed (integer from 0 to 100)
             *
             * UI fields representing this data field are disabled for summary events.
             * See {@link #function-isEditable} for details.
             *
             * @field {Number} percentDone
             * @category Scheduling
             */

            /**
             * The start date of a time span (or Event / Task).
             *
             * Uses {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat} to convert a
             * supplied string to a Date. To specify another format, either change that setting or subclass TimeSpan and
             * change the dateFormat for this field.
             *
             * UI fields representing this data field are disabled for summary events
             * except the {@link #field-manuallyScheduled manually scheduled} events.
             * See {@link #function-isEditable} for details.
             *
             * Note that the field always returns a `Date`.
             *
             * @field {Date} startDate
             * @accepts {String|Date}
             * @category Scheduling
             */

            /**
             * The end date of a time span (or Event / Task).
             *
             * Uses {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat} to convert a
             * supplied string to a Date. To specify another format, either change that setting or subclass TimeSpan and
             * change the dateFormat for this field.
             *
             * UI fields representing this data field are disabled for summary events
             * except the {@link #field-manuallyScheduled manually scheduled} events.
             * See {@link #function-isEditable} for details.
             *
             * Note that the field always returns a `Date`.
             *
             * @field {Date} endDate
             * @accepts {String|Date}
             * @category Scheduling
             */

            /**
             * The numeric part of the timespan's duration (the number of units).
             *
             * UI fields representing this data field are disabled for summary events
             * except the {@link #field-manuallyScheduled manually scheduled} events.
             * See {@link #function-isEditable} for details.
             *
             * @field {Number} duration
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
             * @field {'finishnoearlierthan'|'finishnolaterthan'|'mustfinishon'|'muststarton'|'startnoearlierthan'|'startnolaterthan'|null} constraintType
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
             * When set to `true` the event becomes inactive and stops taking part in the project scheduling (doesn't
             * affect linked events and affect its assigned resources allocation).
             *
             * @field {Boolean} inactive
             * @category Scheduling
             */

            /**
             * When set to `true` the calendars of the assigned resources
             * are not taken into account when scheduling the event.
             *
             * By default the field value is `false` resulting in that the event performs only when
             * its own {@link #field-calendar} and some of the assigned
             * resource calendars allow that.

             * @field {Boolean} ignoreResourceCalendar
             * @category Scheduling
             */

            /**
             * A calculated field storing the _early start date_ of the event.
             * The _early start date_ is the earliest possible date the event can start.
             * This value is calculated based on the earliest dates of the event predecessors and the event own constraints.
             * If the event has no predecessors nor other constraints, its early start date matches the project start date.
             *
             * UI fields representing this data field are naturally disabled since the field is readonly.
             * See {@link #function-isEditable} for details.
             *
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
             *
             * UI fields representing this data field are naturally disabled since the field is readonly.
             * See {@link #function-isEditable} for details.
             *
             * @field {Date} earlyEndDate
             * @calculated
             * @readonly
             * @category Scheduling
             */

            /**
             * The calendar, assigned to the entity. Allows you to set the time when entity can perform the work.
             *
             * All entities are by default assigned to the project calendar, provided as the {@link SchedulerPro.model.ProjectModel#field-calendar} option.
             *
             * @field {SchedulerPro.model.CalendarModel} calendar
             * @category Scheduling
             */

            /**
             * The numeric part of the event effort (the number of units).
             *
             * @field {Number} effort
             * @category Scheduling
             */

            /**
             * The unit part of the event effort, defaults to "h" (hours). Valid values are:
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
             * @field {'millisecond'|'second'|'minute'|'hour'|'day'|'week'|'month'|'quarter'|'year'} effortUnit
             * @default "hour"
             * @category Scheduling
             * @readonly
             */

            /**
             * This field defines the event scheduling mode. Based on this field some fields of the event
             * will be "fixed" (should be provided by the user) and some - computed.
             *
             * Possible values are:
             *
             * - `Normal` is the default (and backward compatible) mode. It means the event will be scheduled based on
             * information about its start/end dates, event own calendar (project calendar if there's no one) and
             * calendars of the assigned resources.
             *
             * - `FixedDuration` mode means, that event has fixed start and end dates, but its effort will be computed
             * dynamically, based on the assigned resources information. When duration of such event increases,
             * its effort is increased too. The mode tends to preserve user provided duration so changing effort
             * results adjusting assignment units and vise-versa assignment changes adjusts effort.
             *
             * @field {'Normal'|'FixedDuration'} schedulingMode
             * @category Scheduling
             */

            /**
             * This boolean flag defines what part the data should be updated in the `FixedDuration` scheduling
             * mode.
             * If it is `true`, then {@link #field-effort} is kept intact when new duration is provided and
             * assignment {@link SchedulerPro.model.AssignmentModel#field-units} is updated.
             * If it is `false`, then assignment {@link SchedulerPro.model.AssignmentModel#field-units} is kept
             * intact when new duration is provided and {@link #field-effort} is updated.
             *
             * @field {Boolean} effortDriven
             * @default false
             * @category Scheduling
             */

            /**
             * The event effective calendar. Returns the
             * {@link SchedulerPro.model.ProjectModel#field-calendar project calendar} if the event has no own
             * {@link #field-calendar} provided.
             * @member {SchedulerPro.model.CalendarModel} effectiveCalendar
             */

            /**
             * Set this to true if this task should be shown in the Timeline widget
             * @field {Boolean} showInTimeline
             * @category Common
             */
            { name : 'showInTimeline', type : 'boolean', defaultValue : false },

            /**
             * Note about the event
             * @field {String} note
             * @category Common
             */
            'note',

            /**
             * Buffer time before event start. Specified in a human-friendly form as accepted by
             * {@link Core.helper.DateHelper#function-parseDuration-static}:
             * ```javascript
             * // Create event model with a 30 minutes buffer time before the event start
             * new EventModel({ startDate : '2020-01-01', endDate : '2020-01-02', preamble : '30 minutes' })
             * ```
             *
             * Used by the {@link SchedulerPro.feature.EventBuffer} feature.
             *
             * @field {Core.data.Duration} preamble
             * @accepts {String}
             * @category Scheduling
             */
            {
                name    : 'preamble',
                convert : value => value ? new Duration(value) : null
            },
            /**
             * Buffer time after event end. Specified in a human-friendly form as accepted by
             * {@link Core.helper.DateHelper#function-parseDuration-static}:
             * ```javascript
             * // Create event model with a 1 hour buffer time after the event end
             * new EventModel({ startDate : '2020-01-01', endDate : '2020-01-02', postamble : '1 hour' })
             * ```
             *
             * Used by the {@link SchedulerPro.feature.EventBuffer} feature.
             *
             * @field {String} postamble
             * @accepts {String}
             * @category Scheduling
             */
            {
                name    : 'postamble',
                convert : value => value ? new Duration(value) : null
            }
        ];
    }

    getDefaultSegmentModelClass() {
        return EventSegmentModel;
    }

    //endregion

    //region EventBuffer

    updateWrapDate(date, duration, forward = true) {
        duration = new Duration(duration);

        return new Date(date.getTime() + (forward ? 1 : -1) * duration.milliseconds);
    }

    get startDate() {
        let dt;

        if (this.isOccurrence) {
            dt = this.get('startDate');
        }
        else {
            // Micro optimization to avoid expensive super call. super will be hit in Scheduler Pro
            dt = this._startDate ?? super.startDate;
        }

        if (this.allDay) {
            dt = this.constructor.getAllDayStartDate(dt);
        }

        return dt;
    }

    set startDate(startDate) {
        const me = this;

        // Update children when parents startDate changes (ignoring initial data set)

        if (me.generation && me.isParent && !me.$ignoreChange) {
            const timeDiff = DateHelper.diff(me.startDate, startDate);

            if (timeDiff) {
                // Move all children same amount
                for (const child of this.children) {
                    child.startDate = DateHelper.add(child.startDate, timeDiff);
                }
            }
        }

        if (me.batching) {
            me._startDate = startDate;
            me.set({ startDate });
        }
        else {
            super.startDate = startDate;

            if (me.preamble) {
                me.wrapStartDate = null;
                me.wrapEndDate = null;
            }
        }
    }

    get endDate() {
        let dt;

        if (this.isOccurrence) {
            dt = this.get('endDate');
        }
        else {
            // Micro optimization to avoid expensive super call. super will be hit in Scheduler Pro
            dt = this._endDate ?? super.endDate;
        }

        if (this.allDay) {
            dt = this.constructor.getAllDayEndDate(dt);
        }

        return dt;
    }

    set endDate(endDate) {
        const me = this;

        if (me.batching) {
            me._endDate = endDate;
            me.set({ endDate });
        }
        else {
            super.endDate = endDate;

            if (me.postamble) {
                me.wrapStartDate = null;
                me.wrapEndDate = null;
            }
        }
    }

    /**
     * Property which encapsulates the effort's magnitude and units.
     *
     * UI fields representing this property are disabled for summary events.
     * See {@link #function-isEditable} for details.
     *
     * @property {Core.data.Duration}
     */
    get fullEffort() {
        return new Duration({
            unit      : this.effortUnit,
            magnitude : this.effort
        });
    }

    set fullEffort(effort) {
        this.setEffort(effort.magnitude, effort.unit);
    }

    // Cannot use `convert` method because it might be disabled by `useRawData : true` and we always need to calculate
    // that value
    get wrapStartDate() {
        const
            me                      = this,
            { preamble, startDate } = me,
            wrapStartDate           = me._wrapStartDate;

        let result;

        if (wrapStartDate) {
            result = wrapStartDate;
        }
        else {
            if (preamble) {
                result = me.updateWrapDate(startDate, preamble, false);
                me._wrapStartDate = result;
            }
            else {
                result = startDate;
            }
        }

        return result;
    }

    set wrapStartDate(value) {
        this._wrapStartDate = value;
    }

    get wrapEndDate() {
        const
            me                     = this,
            { postamble, endDate } = me,
            wrapEndDate            = me._wrapEndDate;

        let result;

        if (wrapEndDate) {
            result = wrapEndDate;
        }
        else {
            if (postamble) {
                result = me.updateWrapDate(endDate, postamble, true);
                me._wrapEndDate = result;
            }
            else {
                result = endDate;
            }
        }

        return result;
    }

    set wrapEndDate(value) {
        this._wrapEndDate = value;
    }

    set(data) {
        const isObject = typeof data === 'object';
        if (data === 'preamble' || (isObject && 'preamble' in data)) {
            this.wrapStartDate = null;
        }
        if (data === 'postamble' || (isObject && 'postamble' in data)) {
            this.wrapEndDate = null;
        }
        return super.set(...arguments);
    }

    /**
     * Returns event start date adjusted by {@link #field-preamble} (start date - duration).
     * @property {Date}
     * @readonly
     */
    get outerStartDate() {
        return this.wrapStartDate;
    }

    /**
     * Returns event end date adjusted by {@link #field-postamble} (end date + duration).
     * @property {Date}
     * @readonly
     */
    get outerEndDate() {
        return this.wrapEndDate;
    }

    //endregion

    /**
     * Defines if the given event field should be manually editable in UI.
     * You can override this method to provide your own logic.
     *
     * By default, the method defines:
     * - {@link #field-earlyStartDate}, {@link #field-earlyEndDate} as not editable;
     * - {@link #field-endDate}, {@link #field-duration} and {@link #field-fullDuration} fields
     *   as not editable for summary events except the {@link #field-manuallyScheduled manually scheduled} ones;
     * - {@link #field-percentDone} as not editable for summary events.
     *
     * @param {String} fieldName Name of the field
     * @returns {Boolean} Returns `true` if the field is editable, `false` if it is not and `undefined` if the event has
     * no such field.
     */
    isEditable(fieldName) {
        switch (fieldName) {
            // r/o fields
            case 'earlyStartDate':
            case 'earlyEndDate':
                return false;

            // disable percentDone editing for summary tasks
            case 'percentDone' :
            case 'renderedPercentDone' :
                return this.isLeaf;

            // end/duration is allowed to edit for leafs and manually scheduled summaries
            case 'endDate' :
            case 'duration' :
            case 'fullDuration' :
                return this.isLeaf || this.manuallyScheduled;
        }

        return super.isEditable(fieldName);
    }

    // Occurrences are not part of the project, when requesting their stm we retrieve it from the master event instead
    get stm() {
        return this.recurringEvent?.stm ?? super.stm;
    }

    set stm(stm) {
        super.stm = stm;
    }

    //region Early render

    get assigned() {
        const
            { project }  = this,
            assigned     = super.assigned;

        // Figure assigned events out before buckets are created  (if part of project)
        if (project?.isDelayingCalculation && !assigned) {
            return project.assignmentStore.storage.findItem('event', this);
        }

        return assigned;
    }

    set assigned(assigned) {
        super.assigned = assigned;
    }

    //endregion

    getCurrentConfig(options) {
        const
            { segments } = this,
            result       = super.getCurrentConfig(options);

        // include segments
        if (result && segments) {
            result.segments = segments.map(segment => segment.getCurrentConfig(options));
        }

        return result;
    }

}
