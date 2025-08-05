import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import AttachToProjectMixin from '../data/mixin/AttachToProjectMixin.js';
import NonWorkingTimeMixin from './mixin/NonWorkingTimeMixin.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module Scheduler/feature/EventNonWorkingTime
 */

/**
 * Feature that allows rendering non-working time ranges into event bars (weekends for Scheduler, effective calendar for
 * SchedulerPro):
 *
 * {@inlineexample Scheduler/feature/EventNonWorkingTime.js}
 *
 * Please note that to not clutter the view (and have a large negative effect on performance) the feature bails out of
 * rendering ranges for very zoomed out views (see {@link #config-maxTimeAxisUnit} for details).
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid/view/mixin/GridFeatures}.
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @classtype eventNonWorkingTime
 * @mixes Scheduler/feature/mixin/NonWorkingTimeMixin
 * @feature
 */
export default class EventNonWorkingTime extends InstancePlugin.mixin(AttachToProjectMixin, NonWorkingTimeMixin) {
    static $name = 'EventNonWorkingTime';

    static pluginConfig = {
        chain : [
            'onTimeAxisViewModelUpdate',
            'updateLocalization',
            'onEventDataGenerated'
        ]
    };

    // Cannot use `static properties = {}`, new Map would pollute the prototype
    static get properties() {
        return {
            calendarRangeCache : new Map()
        };
    }

    doDisable(disable) {
        this.client.refresh();

        super.doDisable(disable);
    }

    attachToCalendarManagerStore(store) {
        super.attachToCalendarManagerStore(store);

        const me = this;

        store.ion({
            name    : 'calendarManagerStore',
            change  : 'onCalendarChange',
            thisObj : me
        });

        // Schedulers calendar is not part of the calendar manager store
        if (me.client.isScheduler) {
            me.project.effectiveCalendar.intervals.ion({
                name    : 'calendarManagerStore',
                change  : 'onCalendarChange',
                thisObj : me
            });
        }

        me.setupDefaultCalendar();
    }

    onCalendarChange() {
        this.calendarRangeCache.clear();
    }

    onTimeAxisViewModelUpdate() {
        this.calendarRangeCache.clear();
    }

    //region Draw

    getRangeDomConfig(timeRange, minDate, maxDate, relativeTo) {
        const
            me         = this,
            { client } = me,
            { rtl }    = client,
            startPos   = client.getCoordinateFromDate(DateHelper.max(timeRange.startDate, minDate)) - relativeTo,
            endPos     = timeRange.endDate ? client.getCoordinateFromDate(DateHelper.min(timeRange.endDate, maxDate)) - relativeTo : startPos,
            size       = Math.abs(endPos - startPos),
            isRange    = size > 0,
            translateX = rtl ? `calc(${startPos}px - 100%)` : `${startPos}px`;

        return {
            className : {
                // Borrow styling from time ranges
                'b-sch-timerange'      : 1,
                'b-sch-range'          : 1,
                'b-sch-nonworkingtime' : 1,
                [timeRange.cls]        : timeRange.cls,
                'b-rtl'                : rtl
            },
            dataset : {
                id : timeRange.id
            },
            elementData : {
                timeRange
            },
            style : client.isVertical
                ? `transform: translateY(${translateX}); ${isRange ? `height:${size}px` : ''};`
                : `transform: translateX(${translateX}); ${isRange ? `width:${size}px` : ''};`
        };
    }

    getCalendarTimeRanges(calendar) {
        let cached = this.calendarRangeCache.get(calendar);

        if (!cached) {
            cached = super.getCalendarTimeRanges(calendar, true);
            this.calendarRangeCache.set(calendar, cached);
        }

        return cached;
    }

    // Render ranges into event bars
    onEventDataGenerated({ eventRecord, endMS, startMS, start, end, left, top, children }) {
        if (this.enabled) {
            const
                { isVertical } = this.client,
                // Use combined calendar for Pro & Gantt, project calendar for Scheduler
                calendar       = eventRecord.effectiveCalendarsCombination ?? this.client.project.calendar;

            for (const timeRange of this.getCalendarTimeRanges(calendar)) {
                // Only care about ranges inside the event bar
                if (timeRange.startDateMS < endMS && timeRange.endDateMS > startMS) {
                    const domConfig = this.getRangeDomConfig(timeRange, start, end, isVertical ? top : left);
                    children.push(domConfig);
                }
            }
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(EventNonWorkingTime, false, 'Scheduler');
