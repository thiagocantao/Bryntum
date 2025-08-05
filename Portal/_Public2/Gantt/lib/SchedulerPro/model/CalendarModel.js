import Model from '../../Core/data/Model.js';
import PartOfProject from '../data/mixin/PartOfProject.js';
import { BaseCalendarMixin } from '../../Engine/quark/model/scheduler_basic/BaseCalendarMixin.js';
import CalendarIntervalModel from './CalendarIntervalModel.js';

/**
 * @module SchedulerPro/model/CalendarModel
 */

/**
 * This class represents a calendar in the Scheduler Pro project. It contains a collection of the {@link SchedulerPro.model.CalendarIntervalModel}.
 * Every interval can be either recurrent (regularly repeating in time) or static.
 *
 * Please refer to the [calendars guide](#guides/schedulerpro/calendars.md) for details
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
     * This method adds a single {@link SchedulerPro.model.CalendarIntervalModel} to the internal collection of the calendar
     *
     * @param {SchedulerPro.model.CalendarIntervalModel} interval
     *
     * @method addInterval
     */

    /**
     * This method adds an array of {@link SchedulerPro.model.CalendarIntervalModel} to the internal collection of the calendar
     *
     * @param {SchedulerPro.model.CalendarIntervalModel[]} intervals
     *
     * @method addIntervals
     */

    static get fields() {
        return [
            /**
             * The calendar name.
             * @field {String} name
             */

            /**
             * The flag, indicating, whether the "unspecified" time (time that does not belong to any interval
             * is working (`true`) or not (`false`).
             *
             * @field {Boolean} unspecifiedTimeIsWorking
             * @default true
             */

            /**
             * {@link SchedulerPro.model.CalendarIntervalModel Intervals} collection of the calendar
             * @field {SchedulerPro.model.CalendarIntervalModel[]} intervals
             */
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
