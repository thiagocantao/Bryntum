import AbstractTimeRanges from './AbstractTimeRanges.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import AttachToProjectMixin from '../data/mixin/AttachToProjectMixin.js';

/**
 * @module Scheduler/feature/NonWorkingTime
 */

/**
 * Feature that allows styling of weekends (and other non working time) by adding timeRanges for those days.
 *
 * By default, Schedulers calendar is empty. When enabling this feature for the basic Scheduler, it injects weekend
 * intervals if no intervals are encountered (saturday and sunday).
 *
 * Please note that the feature does not render ranges smaller than the base unit used by the time axis
 * and for some levels it bails out rendering ranges completely (see {@link #config-maxTimeAxisUnit} for details).
 *
 * This feature is **disabled** by default for Scheduler, but **enabled** by default for Scheduler Pro.
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @demo Scheduler/configuration
 * @externalexample scheduler/NonWorkingTime.js
 * @classtype nonWorkingTime
 */
export default class NonWorkingTime extends AbstractTimeRanges.mixin(AttachToProjectMixin) {
    //region Default config

    static get $name() {
        return 'NonWorkingTime';
    }

    static get defaultConfig() {
        return {
            /**
             * Set to `true` to highlight non working periods of time
             * @config {Boolean}
             * @default
             */
            highlightWeekends : true,

            showHeaderElements : true,
            showLabelInBody    : true,

            cls : 'b-sch-nonworkingtime',

            /**
             * The maximum time axis unit to display non working ranges for.
             * Allows to filter out zoom levels on which the feature should not render.
             * @config {Boolean}
             * @default
             */
            maxTimeAxisUnit : 'week'
        };
    }

    static get pluginConfig() {
        return {
            chain : [
                'onPaint',
                'attachToProject'
            ]
        };
    }

    //endregion

    //region Init & destroy

    doDestroy() {
        this.attachToCalendar(null);
        super.doDestroy();
    }

    //endregion

    //region Project

    attachToProject(project) {
        super.attachToProject(project);

        this.attachToCalendar(project.effectiveCalendar);

        project.on({
            name           : 'project',
            calendarChange : () => this.attachToCalendar(this.project.effectiveCalendar),
            thisObj        : this
        });
    }

    //endregion

    //region TimeAxisViewModel

    onTimeAxisViewModelUpdate() {
        this._timeAxisUnitDurationMs = null;
        return super.onTimeAxisViewModelUpdate();
    }

    //endregion

    //region Calendar

    attachToCalendar(calendar) {
        const
            me                  = this,
            { project, client } = me;

        me.detachListeners('calendar');

        if (calendar) {
            if (
                // For basic scheduler...
                !client.isSchedulerPro &&
                !client.isGantt &&
                // ...that uses the default calendar...
                calendar === project.defaultCalendar &&
                // ...and has no defined intervals
                !project.defaultCalendar.intervalStore.count
            ) {
                // Add weekends as non-working time
                calendar.addIntervals([
                    {
                        recurrentStartDate : 'on Sat at 0:00',
                        recurrentEndDate   : 'on Mon at 0:00',
                        isWorking          : false
                    }
                ]);
            }

            calendar.intervalStore.on({
                name    : 'calendar',
                change  : me.renderRanges,
                delay   : 1,
                thisObj : me
            });
        }

        // On changing calendar we react to a data level event which is triggered after project refresh.
        // Redraw right away
        if (client.isEngineReady) {
            me.renderRanges();
        }
        // Initially there is no guarantee we are ready to draw, wait for refresh
        else if (!project.isDestroyed) {
            project.on({
                refresh : me.renderRanges,
                thisObj : me,
                once    : true
            });
        }
    }

    get calendar() {
        return this.project?.effectiveCalendar;
    }

    //endregion

    //region Draw

    get timeAxisUnitDurationMs() {
        // calculate and cache duration of the timeAxis unit in milliseconds
        if (!this._timeAxisUnitDurationMs) {
            this._timeAxisUnitDurationMs = DateHelper.as('ms', 1, this.client.timeAxis.unit);
        }

        return this._timeAxisUnitDurationMs;
    }

    shouldRenderRange(range) {
        // if the range is longer or equal than one timeAxis unit then render it
        return super.shouldRenderRange(range) && (range.durationMS >= this.timeAxisUnitDurationMs);
    }

    renderRanges() {
        const
            me                             = this,
            { store, calendar }            = me,
            { timeAxis, foregroundCanvas } = me.client;

        // if not too early (project correctly set up yet etc)
        if (calendar && foregroundCanvas && store && !store.isDestroyed) {
            if (!me.disabled) {
                // checks if we should render ranges for the current zoom level
                const shouldPaint = !me.maxTimeAxisUnit || DateHelper.compareUnits(timeAxis.unit, me.maxTimeAxisUnit) <= 0;

                store.removeAll(true);

                if (calendar && me.highlightWeekends && shouldPaint && timeAxis.count) {
                    const timeRanges = calendar.getNonWorkingTimeRanges(timeAxis.startDate, timeAxis.endDate).map((r, i) => ({
                        name      : r.name,
                        cls       : 'b-nonworkingtime',
                        startDate : r.startDate,
                        endDate   : r.endDate
                    }));

                    const timeRangesMerged = [];

                    let prevRange;

                    // intervals returned by the calendar are not merged ..let's combine them
                    timeRanges.forEach(range => {
                        if (prevRange && range.startDate <= prevRange.endDate && range.name === prevRange.name) {
                            prevRange.endDate = range.endDate;
                        }
                        else {
                            timeRangesMerged.push(range);
                            range.id  = `nonworking-${timeRangesMerged.length}`;
                            prevRange = range;
                        }
                    });

                    store.add(timeRangesMerged, true);
                }
            }

            super.renderRanges();
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(NonWorkingTime, false, 'Scheduler');
GridFeatureManager.registerFeature(NonWorkingTime, true, 'SchedulerPro');
GridFeatureManager.registerFeature(NonWorkingTime, true, 'Gantt');
