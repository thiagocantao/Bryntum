import Base from '../../../Core/Base.js';
import DateHelper from '../../../Core/helper/DateHelper.js';

// Used to avoid having to create huge amounts of Date objects
const tempDate = new Date();

/**
 * @module Scheduler/view/mixin/TimelineDateMapper
 */

/**
 * Mixin that contains functionality to convert between coordinates and dates etc.
 *
 * @mixin
 */
export default Target => class TimelineDateMapper extends (Target || Base) {
    static $name = 'TimelineDateMapper';

    static configurable = {
        /**
         * Set to `true` to snap to the current time resolution increment while interacting with scheduled events.
         *
         * The time resolution increment is either determined by the currently applied view preset, or it can be
         * overridden using {@link #property-timeResolution}.
         *
         * <div class="note">When the {@link Scheduler/view/mixin/TimelineEventRendering#config-fillTicks} option is
         * enabled, snapping will align to full ticks, regardless of the time resolution.</div>
         *
         * @prp {Boolean}
         * @default
         * @category Scheduled events
         */
        snap : false
    };

    //region Coordinate <-> Date

    getRtlX(x) {
        if (this.rtl && this.isHorizontal) {
            x = this.timeAxisViewModel.totalSize - x;
        }
        return x;
    }

    /**
     * Gets the date for an X or Y coordinate, either local to the view element or the page based on the 3rd argument.
     * If the coordinate is not in the currently rendered view, null will be returned unless the `allowOutOfRange`
     * parameter is passed a `true`.
     * @param {Number} coordinate The X or Y coordinate
     * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
     * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
     * @param {Boolean} [local] true if the coordinate is local to the scheduler view element
     * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
     * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
     * @returns {Date} The Date corresponding to the X or Y coordinate
     * @category Dates
     */
    getDateFromCoordinate(coordinate, roundingMethod, local = true, allowOutOfRange = false, ignoreRTL = false) {
        if (!local) {
            coordinate = this.currentOrientation.translateToScheduleCoordinate(coordinate);
        }

        // Time axis is flipped for RTL
        if (!ignoreRTL) {
            coordinate = this.getRtlX(coordinate);
        }

        return this.timeAxisViewModel.getDateFromPosition(coordinate, roundingMethod, allowOutOfRange);
    }

    getDateFromCoord(options) {
        return this.getDateFromCoordinate(options.coord, options.roundingMethod, options.local, options.allowOutOfRange, options.ignoreRTL);
    }

    /**
     * Gets the date for an XY coordinate regardless of the orientation of the time axis.
     * @param {Array} xy The page X and Y coordinates
     * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
     * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
     * @param {Boolean} [local] true if the coordinate is local to the scheduler element
     * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
     * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
     * @returns {Date} the Date corresponding to the xy coordinate
     * @category Dates
     */
    getDateFromXY(xy, roundingMethod, local = true, allowOutOfRange = false) {
        return this.currentOrientation.getDateFromXY(xy, roundingMethod, local, allowOutOfRange);
    }

    /**
     * Gets the time for a DOM event such as 'mousemove' or 'click' regardless of the orientation of the time axis.
     * @param {Event} e the Event instance
     * @param {'floor'|'round'|'ceil'} [roundingMethod] Rounding method to use. 'floor' to take the tick (lowest header
     * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
     * @param {Boolean} [allowOutOfRange] By default, this returns `null` if the position is outside
     * of the time axis. Pass `true` to attempt to calculate a date outside of the time axis.
     * @returns {Date} The date corresponding to the EventObject's position along the orientation of the time axis.
     * @category Dates
     */
    getDateFromDomEvent(e, roundingMethod, allowOutOfRange = false) {
        return this.getDateFromXY([e.pageX, e.pageY], roundingMethod, false, allowOutOfRange);
    }

    /**
     * Gets the start and end dates for an element Region
     * @param {Core.helper.util.Rectangle} rect The rectangle to map to start and end dates
     * @param {'floor'|'round'|'ceil'} roundingMethod Rounding method to use. 'floor' to take the tick (lowest header
     * in a time axis) start date, 'round' to round the value to nearest increment or 'ceil' to take the tick end date
     * @param {Number} duration The duration in MS of the underlying event
     * @returns {Object} an object containing start/end properties
     */
    getStartEndDatesFromRectangle(rect, roundingMethod, duration, allowOutOfRange = false) {
        const
            me               = this,
            { isHorizontal } = me,
            startPos         = isHorizontal ? rect.x : rect.top,
            endPos           = isHorizontal ? rect.right : rect.bottom;

        let start, end;

        // Element within bounds
        if (startPos >= 0 && endPos < me.timeAxisViewModel.totalSize) {
            start = me.getDateFromCoordinate(startPos, roundingMethod, true);
            end = me.getDateFromCoordinate(endPos, roundingMethod, true);
        }
        // Starts before, start is worked backwards from end
        else if (startPos < 0) {
            end = me.getDateFromCoordinate(endPos, roundingMethod, true, allowOutOfRange);
            start = end && DateHelper.add(end, -duration, 'ms');
        }
        // Ends after, end is calculated from the start
        else {
            start = me.getDateFromCoordinate(startPos, roundingMethod, true, allowOutOfRange);
            end = start && DateHelper.add(start, duration, 'ms');
        }

        return {
            start, end
        };
    }

    //endregion

    //region Date display

    /**
     * Method to get a displayed end date value, see {@link #function-getFormattedEndDate} for more info.
     * @private
     * @param {Date} endDate The date to format
     * @param {Date} startDate The start date
     * @returns {Date} The date value to display
     */
    getDisplayEndDate(endDate, startDate) {
        if (
            // If time is midnight,
            endDate.getHours() === 0 && endDate.getMinutes() === 0 &&

            // and end date is greater then start date
            (!startDate || !(endDate.getYear() === startDate.getYear() && endDate.getMonth() === startDate.getMonth() && endDate.getDate() === startDate.getDate())) &&

            // and UI display format doesn't contain hour info (in this case we'll just display the exact date)
            !DateHelper.formatContainsHourInfo(this.displayDateFormat)
        ) {
            // format the date inclusively as 'the whole previous day'.
            endDate = DateHelper.add(endDate, -1, 'day');
        }

        return endDate;
    }

    /**
     * Method to get a formatted end date for a scheduled event, the grid uses the "displayDateFormat" property defined in the current view preset.
     * End dates are formatted as 'inclusive', meaning when an end date falls on midnight and the date format doesn't involve any hour/minute information,
     * 1ms will be subtracted (e.g. 2010-01-08T00:00:00 will first be modified to 2010-01-07 before being formatted).
     * @private
     * @param {Date} endDate The date to format
     * @param {Date} startDate The start date
     * @returns {String} The formatted date
     */
    getFormattedEndDate(endDate, startDate) {
        return this.getFormattedDate(this.getDisplayEndDate(endDate, startDate));
    }

    //endregion

    //region Other date functions

    /**
     * Gets the x or y coordinate relative to the scheduler element, or page coordinate (based on the 'local' flag)
     * If the coordinate is not in the currently rendered view, -1 will be returned.
     * @param {Date|Number} date the date to query for (or a date as ms)
     * @param {Boolean|Object} options true to return a coordinate local to the scheduler view element (defaults to true),
     * also accepts a config object like { local : true }.
     * @returns {Number} the x or y position representing the date on the time axis
     * @category Dates
     */
    getCoordinateFromDate(date, options = true) {
        const
            me                    = this,
            { timeAxisViewModel } = me,
            {
                isContinuous,
                startMS,
                endMS,
                startDate,
                endDate,
                unit
            }                     = me.timeAxis,
            dateMS                = date.valueOf();

        // Avoiding to break the API while allowing passing options through to getPositionFromDate()
        if (options === true) {
            options = {
                local : true
            };
        }
        else if (!options) {
            options = {
                local : false
            };
        }
        else if (!('local' in options)) {
            options.local = true;
        }

        let pos;


        if (!(date instanceof Date)) {
            tempDate.setTime(date);
            date = tempDate;
        }

        // Shortcut for continuous time axis that is using a unit that can be reliably translated to days (or smaller)
        if (isContinuous &&
            date.getTimezoneOffset() === startDate.getTimezoneOffset() &&
            startDate.getTimezoneOffset() === endDate.getTimezoneOffset() &&
            DateHelper.getUnitToBaseUnitRatio(unit, 'day') !== -1
        ) {
            if (dateMS < startMS || dateMS > endMS) {
                return -1;
            }
            pos = (dateMS - startMS) / (endMS - startMS) * timeAxisViewModel.totalSize;
        }
        // Non-continuous or using for example months (vary in length)
        else {
            pos = timeAxisViewModel.getPositionFromDate(date, options);
        }

        // RTL coords from the end of the time axis
        if (me.rtl && me.isHorizontal && !options?.ignoreRTL) {
            pos = timeAxisViewModel.totalSize - pos;
        }

        if (!options.local) {
            pos = me.currentOrientation.translateToPageCoordinate(pos);
        }

        return pos;
    }

    /**
     * Returns the distance in pixels for the time span in the view.
     * @param {Date} startDate The start date of the span
     * @param {Date} endDate The end date of the span
     * @returns {Number} The distance in pixels
     * @category Dates
     */
    getTimeSpanDistance(startDate, endDate) {
        return this.timeAxisViewModel.getDistanceBetweenDates(startDate, endDate);
    }

    /**
     * Returns the center date of the currently visible timespan of scheduler.
     *
     * @property {Date}
     * @readonly
     * @category Dates
     */
    get viewportCenterDate() {
        const { timeAxis, timelineScroller } = this;

        // Take the easy way if the axis is continuous.
        // We can just work out how far along the time axis the viewport center is.
        if (timeAxis.isContinuous) {
            // The offset from the start of the whole time axis
            const timeAxisOffset = (timelineScroller.position + timelineScroller.clientSize / 2) / timelineScroller.scrollSize;

            return new Date(timeAxis.startMS + (timeAxis.endMS - timeAxis.startMS) * timeAxisOffset);
        }
        return this.getDateFromCoordinate(timelineScroller.position + timelineScroller.clientSize / 2);
    }

    get viewportCenterDateCached() {
        return this.cachedCenterDate || (this.cachedCenterDate = this.viewportCenterDate);
    }

    //endregion

    //region TimeAxis getters/setters

    /**
     * Gets/sets the current time resolution object, which contains a unit identifier and an increment count
     * `{ unit, increment }`. This value means minimal task duration you can create using UI.
     *
     * For example when you drag create a task or drag & drop a task, if increment is 5 and unit is 'minute'
     * that means that you can create tasks in 5 minute increments, or move it in 5 minute steps.
     *
     * This value is taken from viewPreset {@link Scheduler.preset.ViewPreset#field-timeResolution timeResolution}
     * config by default. When supplying a `Number` to the setter only the `increment` is changed and the `unit` value
     * remains untouched.
     *
     * ```javascript
     * timeResolution : {
     *   unit      : 'minute',  //Valid values are "millisecond", "second", "minute", "hour", "day", "week", "month", "quarter", "year".
     *   increment : 5
     * }
     * ```
     *
     * <div class="note">When the {@link Scheduler/view/mixin/TimelineEventRendering#config-fillTicks} option is
     * enabled, the resolution will be in full ticks regardless of configured value.</div>
     *
     * @property {Object|Number}
     * @category Dates
     */
    get timeResolution() {
        return this.timeAxis.resolution;
    }

    set timeResolution(resolution) {
        this.timeAxis.resolution = (typeof resolution === 'number') ? {
            increment : resolution,
            unit      : this.timeAxis.resolution.unit
        } : resolution;
    }

    //endregion

    //region Snap

    get snap() {
        return this._timeAxisViewModel?.snap ?? this._snap;
    }

    updateSnap(snap) {
        if (!this.isConfiguring) {
            this.timeAxisViewModel.snap = snap;
            this.timeAxis.forceFullTicks = snap && this.fillTicks;
        }
    }

    //endregion

    onSchedulerHorizontalScroll({ subGrid, scrollLeft, scrollX }) {
        // Invalidate cached center date unless we are scrolling to center on it.
        if (!this.scrollingToCenter) {
            this.cachedCenterDate = null;
        }
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
