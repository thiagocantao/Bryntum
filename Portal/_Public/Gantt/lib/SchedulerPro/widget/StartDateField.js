import DateField from '../../Core/widget/DateField.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module SchedulerPro/widget/StartDateField
 */

const year2300 = new Date(2300, 0, 1);

/**
 * Date field widget (text field + date picker) to be used together with Scheduling Engine.
 * This field adjusts time to the earliest possible time of the day based on either:
 *
 * - the event calendars (which is a combination of its own calendar and assigned resources ones) - if
 *   {@link #config-eventRecord} is provided.
 * - the project {@link SchedulerPro.model.ProjectModel#field-calendar calendar} - if {@link #config-project} is
 *   provided. The project start date is used as a default value for the {@link #property-min} property.
 *   Also, the default value of the {@link #property-max} property is set to be 200 years
 *   after the project's end date (or to the year 2300 if no project is provided).
 *
 * **Please note, that either {@link #config-eventRecord} or {@link #config-project} value must be provided.**
 *
 * This field can be used as an editor for the {@link Grid.column.Column Column}.
 * It is used as the default editor for the `StartDateColumn`.
 *
 * {@inlineexample SchedulerPro/widget/StartDateField.js}
 * @extends Core/widget/DateField
 * @classType startdatefield
 * @inputfield
 */
export default class StartDateField extends DateField {

    //region Config

    static get $name() {
        return 'StartDateField';
    }

    // Factoryable type name
    static get type() {
        return 'startdatefield';
    }

    // Factoryable type alias
    static get alias() {
        return 'startdate';
    }

    static get defaultConfig() {
        return {
            /**
             * Project model calendar of which should be used by the field.
             * @config {SchedulerPro.model.ProjectModel}
             */
            project : null,

            /**
             * Event model calendars of which should be used by the field.
             * @config {SchedulerPro.model.EventModel}
             */
            eventRecord : null,

            strictParsing : true,

            /**
             * Number of milliseconds to add to the project's start date (should be negative). Then, during editing,
             * the resulting date is assigned to the {@link #config-min} property of the field,
             * preventing the user from entering too low values.
             *
             * This also prevents freezing, when user enters the incomplete date with one-digit year.
             *
             * The value of this config will be passed to {@link Core.helper.DateHelper#function-add-static},
             * so in addition to number of milliseconds, strings like "-1 year" are recognized.
             *
             * Default value is '-10 years'
             *
             * @config {Number|String}
             * @default
             */
            minDateDelta : '-10 years',

            /**
             * Number of milliseconds to add to the project's start date. Then, during editing,
             * the resulting date is assigned to the {@link #config-max} property of the field,
             * preventing the user from entering too high values.
             *
             * This also prevents freezing, when user enters the date with five-digits year.
             *
             * The value of this config will be passed to {@link Core.helper.DateHelper#function-add-static},
             * so in addition to number of milliseconds, strings like "1 year" are recognized.
             *
             * Default value is '200 years'
             *
             * @config {Number|String}
             * @default
             */
            maxDateDelta : '200 years'
        };
    }

    //endregion

    //region Internal

    get calendarProvider() {
        // Occurrences does not have their own calendar, use master events calendar
        return (this.eventRecord?.recurringEvent ?? this.eventRecord) || this.project;
    }

    get backShiftDate() {
        const me = this;

        return me.calendarProvider.run('skipWorkingTime', me.value, false, me._step.magnitude, me._step.unit);
    }

    get forwardShiftDate() {
        const me = this;

        let result = me.calendarProvider.run('skipWorkingTime', me.value, true, me._step.magnitude, me._step.unit);

        // Need to skip non-working time
        // since after the above step "result" can be set at 17:00 for example (for business calendar)
        result = result && me.calendarProvider.run('skipNonWorkingTime', result, true);

        return result;
    }

    transformTimeValue(value) {
        const { calendarProvider, keepTime } = this;

        if (calendarProvider && keepTime !== 'entered') {
            const
                startOfTheDay = DateHelper.clearTime(value),
                // search for the earliest available time for this day
                earliestTime  = calendarProvider.run('skipNonWorkingTime', startOfTheDay);

            // if it's the same day, the earliest time is found, use it
            if (DateHelper.isValidDate(earliestTime) && DateHelper.isEqual(earliestTime, startOfTheDay, 'day')) {
                return DateHelper.copyTimeValues(startOfTheDay, earliestTime);
            }
        }

        return super.transformTimeValue(value);
    }

    get min() {
        return super.min || this.project?.startDate ? DateHelper.add(this.project.startDate, this.minDateDelta) : null;
    }

    set min(value) {
        super.min = value;
    }

    get max() {
        return super.max || this.project?.startDate ? DateHelper.add(this.project.startDate, this.maxDateDelta) : year2300;
    }

    set max(value) {
        super.max = value;
    }
    //endregion

}

// Register this widget type with its Factory
StartDateField.initClass();
