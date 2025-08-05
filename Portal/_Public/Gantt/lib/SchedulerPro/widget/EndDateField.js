import DateField from '../../Core/widget/DateField.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module SchedulerPro/widget/EndDateField
 */

const year2300 = new Date(2300, 0, 1);

/**
 * Date field widget (text field + date picker) to be used together with Scheduling Engine.
 * This field adjusts time to the latest possible time of the day based on either:
 *
 * - the event calendars (which is a combination of its own calendar and assigned resources ones) - if
 *   {@link #config-eventRecord} is provided.
 * - the project {@link SchedulerPro.model.ProjectModel#field-calendar calendar} - if {@link #config-project} is
 *   provided. The default value of the {@link #property-max} property is set to be 200 years
 *   after the project's end date (or to the year 2300 if no project is provided).
 *
 * **Please note, that either {@link #config-eventRecord} or {@link #config-project} value must be provided.**
 *
 * This field can be used as an editor for a {@link Grid.column.Column Column}.
 * It is used as the default editor for the `EndDateColumn`.
 *
 * {@inlineexample SchedulerPro/widget/EndDateField.js}
 * @extends Core/widget/DateField
 * @classType enddatefield
 * @inputfield
 */
export default class EndDateField extends DateField {

    //region Config

    static get $name() {
        return 'EndDateField';
    }

    // Factoryable type name
    static get type() {
        return 'enddatefield';
    }

    // Factoryable alias name
    static get alias() {
        return 'enddate';
    }

    static get defaultConfig() {
        return {
            /**
             * Project model calendar of which should be used by the field.
             * @config {SchedulerPro.model.ProjectModel}
             */
            project     : null,
            /**
             * The Event model defining the calendar to be used by the field.
             * @config {SchedulerPro.model.EventModel}
             */
            eventRecord : null,

            strictParsing : true
        };
    }

    //endregion

    //region Internal

    get min() {
        let min               = this._min;
        const eventStartDate  = this.eventRecord?.startDate;

        if (eventStartDate) {
            min = DateHelper.max(min || eventStartDate, eventStartDate);
        }

        return min;
    }

    set min(value) {
        super.min = value;
    }

    get max() {
        return super.max || this.project?.startDate ? DateHelper.add(this.project.startDate, 200, 'year') : year2300;
    }

    set max(value) {
        super.max = value;
    }

    get calendarProvider() {
        // Occurrences does not have their own calendar, use master events calendar
        return (this.eventRecord?.recurringEvent ?? this.eventRecord) || this.project;
    }

    get backShiftDate() {
        const me = this;

        let result = me.calendarProvider.run('skipWorkingTime', me.value, false, me._step.magnitude, me._step.unit);

        // Need to skip non-working time
        // since after the above step "result" can be set at 08:00 for example (for business calendar)
        result = result && me.calendarProvider.run('skipNonWorkingTime', result, false);

        return result;
    }

    get forwardShiftDate() {
        const me = this;

        return me.calendarProvider.run('skipWorkingTime', me.value, true, me._step.magnitude, me._step.unit);
    }

    transformTimeValue(value) {
        const { calendarProvider, keepTime } = this;

        if (calendarProvider && keepTime !== 'entered') {
            const
                startOfTheDay  = DateHelper.clearTime(value),
                startOfNextDay = DateHelper.add(startOfTheDay, 1, 'day'),
                // search for the latest available time for this day
                latestTime     = calendarProvider.run('skipNonWorkingTime', startOfNextDay, false);

            // if it's the same day, the latest time is found, use it
            if (DateHelper.isValidDate(latestTime) && DateHelper.isEqual(latestTime, startOfTheDay, 'day')) {
                return DateHelper.copyTimeValues(startOfTheDay, latestTime);
            }
        }

        // if keepTime==false means we reset time info ...make sure we do not violate "min" value in that case
        if (!keepTime && value && this.min && DateHelper.clearTime(value, false) < this.min) {
            return this.min;
        }

        return super.transformTimeValue(value);
    }

    //endregion

}

// Register this widget type with its Factory
EndDateField.initClass();
