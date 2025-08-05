import Model from '../../Core/data/Model.js';
import PartOfProject from '../data/mixin/PartOfProject.js';
import { BaseCalendarMixin } from '../../Engine/quark/model/scheduler_basic/BaseCalendarMixin.js';
import CalendarIntervalModel from './CalendarIntervalModel.js';

/**
 * @module SchedulerPro/model/CalendarModel
 */

/**
 * This class represents a calendar in the Scheduler Pro project. It contains a collection of the {@link SchedulerPro.model.CalendarIntervalModel}.
 * Every interval can be either recurrent (regularly repeating in time) or static. These intervals can be visualized
 * by the {@link SchedulerPro.feature.ResourceNonWorkingTime} or {@link Scheduler.feature.NonWorkingTime} features.
 *
 * Please refer to the [calendars guide](#SchedulerPro/guides/basics/calendars.md) for details
 *
 * @mixes SchedulerPro/data/mixin/PartOfProject
 *
 * @extends Core/data/Model
 */
export default class CalendarModel extends PartOfProject(BaseCalendarMixin.derive(Model)) {

    //region Config

    static get $name() {
        return 'CalendarModel';
    }

    /**
     * Returns the earliest point at which a working period of time starts, following the given date.
     * Can be the date itself, if it occurs during working time.
     * @method skipNonWorkingTime
     * @param {Date} date The date after which to skip the non-working time
     * @param {Boolean} [isForward=true] Whether the "following" means forward in time or backward
     * @returns {Date} The earliest available date
     */

    /**
     * Returns working time ranges between the provided dates.
     * @method getWorkingTimeRanges
     * @param {Date} startDate Start of the period to get ranges from
     * @param {Date} endDate End of the period to get ranges from
     * @returns {Array<{name: string, startDate: Date, endDate: Date}>} An array of objects representing working time ranges
     */

    /**
     * Returns non-working time ranges between the provided dates.
     * @method getNonWorkingTimeRanges
     * @param {Date} startDate Start of the period to get ranges from
     * @param {Date} endDate End of the period to get ranges from
     * @returns {Array<{name: string, startDate: Date, endDate: Date}>} An array of objects representing non-working time ranges
     */

    /**
     * This method adds a single {@link SchedulerPro.model.CalendarIntervalModel} to the internal collection of the
     * calendar
     * @method addInterval
     * @param {SchedulerPro.model.CalendarIntervalModel|CalendarIntervalModelConfig} interval record or an object with
     * data used to create a new record
     * @returns {SchedulerPro.model.CalendarIntervalModel[]} Added intervals
     */

    /**
     * This method adds an array of {@link SchedulerPro.model.CalendarIntervalModel} to the internal collection of the
     * calendar
     * @method addIntervals
     * @param {SchedulerPro.model.CalendarIntervalModel[]|CalendarIntervalModelConfig[]} intervals An array of records
     * or an array of objects with data used to create new records
     * @returns {SchedulerPro.model.CalendarIntervalModel[]} Added intervals
     */

    /**
     * This method removes all intervals from the internal collection of the calendar
     * @method clearIntervals
     * @param {Boolean} [silent] Do not trigger events
     */

    /**
     * Calculate the working time duration for specific interval, in milliseconds.
     * @method calculateDurationMs
     * @param {Date} startDate Start of the interval
     * @param {Date} endDate End of the interval
     * @returns {Number} Returns working time in milliseconds
     */

    /**
     * Checks if there is a working time interval in the provided time range
     * @method isWorkingTime
     * @param {Date} startDate Start of the interval
     * @param {Date} endDate End of the interval
     * @returns {Boolean} Returns `true` if the interval contains working time
     */

    /**
     * Calculates the end date of the time interval which starts at `startDate` and has `durationMs` working time
     * duration (in milliseconds).
     * @method calculateEndDate
     * @param {Date} startDate Start date
     * @param {Number} durationMs Duration in milliseconds
     * @returns {Date} The end date
     */

    /**
     * Calculates the start date of the time interval which ends at `endDate` and has `durationMs` working time
     * duration (in milliseconds).
     * @method calculateStartDate
     * @param {Date} endDate End date
     * @param {Number} durationMs Duration in milliseconds
     * @returns {Date} The start date
     */

    static get fields() {
        return [
            /**
             * The calendar name.
             * @field {String} name
             */

            /**
             * A CSS class to add to calendar interval elements rendered in the UI.
             * @field {String} cls
             */

            /**
             * A CSS class defining an icon to show in non-working time elements rendered in the UI.
             * @field {String} iconCls
             */

            /**
             * The flag, indicating, whether the "unspecified" time (time that does not belong to any interval
             * is working (`true`) or not (`false`).
             *
             * @field {Boolean} unspecifiedTimeIsWorking
             * @default true
             */

            /**
             * {@link SchedulerPro.model.CalendarIntervalModel Intervals} collection of the calendar. Accepts an array
             * as its input, which behind the scenes will be converted to a store containing the intervals. When
             * serializing it will be converted back to an array.
             *
             * @field {Core.data.Store} intervals
             * @accepts {SchedulerPro.model.CalendarIntervalModel[]|CalendarIntervalModelConfig[]}
             */

            /**
             * Start expanded or not
             * @readonly
             * @field {Boolean} expanded
             * @default true
             * @category Tree
             */
            {
                name         : 'expanded',
                internal     : true,
                defaultValue : true
            },

            /**
             * If using the {@link Scheduler.model.ProjectModel#config-timeZone} config, you can set this to `true` to
             * *not* convert this calendar to the specified time zone.
             * @field {Boolean} ignoreTimeZone
             */
            'ignoreTimeZone'
        ];
    }

    //endregion

    toString() {
        return this.name || '';
    }

    static get defaultConfig() {
        return {
            calendarIntervalModelClass : CalendarIntervalModel
        };
    }
}
