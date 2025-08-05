import BrowserHelper from '../../../Core/helper/BrowserHelper.js';
import DH from '../../../Core/helper/DateHelper.js';
import GridFeatureManager from '../../../Grid/feature/GridFeatureManager.js';
import InstancePlugin from '../../../Core/mixin/InstancePlugin.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import XMLHelper from '../../../Core/helper/XMLHelper.js';

const
    MIN_DATE = DH.clearTime(new Date(1900, 5, 15)), // TODO some early date to safely not intersect w/ some calendar exceptions
    taskUnitMap = {
        minute : 3,
        hour   : 5,
        day    : 7,
        week   : 9,
        month  : 11
    },
    projectUnitMap = {
        minute : 1,
        hour   : 2,
        day    : 3,
        week   : 4,
        month  : 5
    },
    constraintMap = {
        finishnoearlierthan : 6,
        finishnolaterthan   : 7,
        mustfinishon        : 3,
        muststarton         : 2,
        startnoearlierthan  : 4,
        startnolaterthan    : 5
    },
    typeMap = {
        FixedDuration : 1,
        FixedUnits    : 0,
        FixedEffort   : 2,
        Normal        : 0
    },
    dependencyTypeMap = {
        0 : 3,
        1 : 2,
        2 : 1,
        3 : 0
    };

/**
 * @module Gantt/feature/export/MspExport
 */

/**
 * A feature that allows exporting Gantt to Microsoft Project without involving a server.
 *
 * [Microsoft Project XML specification](https://docs.microsoft.com/en-us/office-project/xml-data-interchange/introduction-to-project-xml-data)
 *
 * This feature supports exporting an XML format that can be imported by MS Project Professional 2013. Assignments, percent work complete and relationship of the calendar with resource and work weeks for calendars are not supported.
 *
 * We plan to expand support to include the features mentioned above + exporting to MS Project 2019 in the future.
 *
 * Here is an example of how to add the feature:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         mspExport : {
 *             // Choose the filename for the exported file
 *             filename : 'Gantt Export'
 *         }
 *     }
 * });
 * ```
 *
 * And how to call it:
 *
 * ```javascript
 * gantt.features.mspExport.export({
 *     filename : 'Gantt Export'
 * })
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Gantt/msprojectexport
 */
export default class MspExport extends InstancePlugin {
    static get $name() {
        return 'MspExport';
    }

    static get configurable() {
        return {
            /**
             * Name of the exported file (including extension)
             * @config {String}
             * @default
             */
            filename : null,

            /**
             * Defines how date is formatted for MSProject. Information about formats are in {@link Core.helper.DateHelper}
             * @config {String}
             * @default
             */
            dateFormat : 'YYYY-MM-DDTHH:mm:ss',

            /**
             * Defines how time is formatted for MSProject. Information about formats are in {@link Core.helper.DateHelper}
             * @config {String}
             * @default
             */
            timeFormat : 'HH:mm:ss',

            /**
             * Defines version used for MSProject (2013 or 2019)
             * @config {Number}
             * @default
             */
            msProjectVersion : 2019
        };
    }

    /**
     * Generate the export data to generate the XML.
     * @returns {Object} Gantt data on MS Project structure to generate the XML
     * @private
     */
    generateExportData() {
        const me = this;

        return {
            ...me.getMsProjectConfig(),
            Calendars : {
                Calendar : me.getCalendarsData()
            },
            Tasks : {
                Task : me.getTasksData()
            },
            Resources : {
                Resource : me.getResourcesData()
            },
            Assignments : {
                Assignment : me.getAssignmentsData()
            }
        };
    }

    /**
     * Generate and download the .xml file.
     * @param {Object} config Optional configuration object, which overrides initial settings of the feature/exporter.
     */
    export(config = {}) {
        const me = this;

        if (me.disabled) {
            return;
        }

        config = ObjectHelper.assign({}, me.config, config);

        if (!config.filename) {
            config.filename = `${me.client.$$name}.xml`;
        }

        const
            data = me.generateExportData(config),
            xml  = me.convertToXml(data);

        BrowserHelper.download(config.filename, `data:text/xml;charset=utf-8,${escape(xml)}`);
    }

    /**
     * Convert Object data to XML.
     * @param {Object} data The Object with data.
     * @returns {String} The XML data.
     * @private
     */
    convertToXml(data) {
        return XMLHelper.convertFromObject(data, {
            rootName            : 'Project',
            elementName         : '',
            xmlns               : 'http://schemas.microsoft.com/project',
            rootElementForArray : false
        });
    }

    /**
     * Get the XML configurations in MS Project format.
     * @returns {Object} MS Project configurations for the XML
     * @private
     */
    getMsProjectConfig() {
        const
            me = this,
            dateFormat = me.dateFormat,
            { project } = me.client,
            fileName = me.filename || me.client.$$name;

        return {
            CalendarUID       : me.getCalendarUID(project.effectiveCalendar),
            CreationDate      : DH.format(new Date(), dateFormat),
            CurrentDate       : DH.format(new Date(), dateFormat),
            DaysPerMonth      : project.daysPerMonth,
            FinishDate        : DH.format(project.endDate, dateFormat),
            MinutesPerDay     : project.hoursPerDay * 60,
            MinutesPerWeek    : project.daysPerWeek * project.hoursPerDay * 60,
            Name              : fileName,
            ScheduleFromStart : project.direction === 'Forward' ? 1 : 0,
            StartDate         : DH.format(project.startDate, dateFormat),
            Title             : fileName,
            WorkFormat        : projectUnitMap[project.effortUnit]
        };
    }

    /**
     * Format Calendars from Gantt to MS Project format.
     * @returns {Array} Calendars array formatted
     * @private
     */
    getCalendarsData() {
        const
            me = this,
            {
                calendarManagerStore : calStore,
                project
            } = me.client,
            { calendar : projCal } = project,
            calendars              = calStore.allRecords || [];

        // if project's calendar is not included on calendars array, include it
        if (!calStore.getByInternalId(projCal.internalId)) {
            calendars.push(projCal);
        }

        return calendars.map(calendar => ({
            BaseCalendarUID : me.getCalendarUID(calendar.parent),
            IsBaseCalendar  : calendar.parentId ? 0 : 1,
            Name            : `${calendar.name || calendar.internalId} - imported`,
            UID             : me.getCalendarUID(calendar),
            WeekDays        : {
                WeekDay : me.formatWeekDays(calendar)
            }//,
            // TODO
            // WorkWeeks : {
            //     WorkWeek : this.formatWorkWeeks(calendar)
            // }
        }));
    }

    /**
     * Format intervals to MS project format for the WeekDays property.
     * @param {Array} Array of intervals data.
     * @returns {Array} Array with data formatted
     * @private
     */
    formatWeekDays(calendar) {
        const
            { timeFormat } = this,
            ticks          = [],
            daysData       = {};

        let startDate = MIN_DATE,
            endDate;

        for (let i = 0; i < 7; i++) {
            // week day index
            const day = startDate.getDay();

            daysData[day] = {
                DayType    : day + 1,
                DayWorking : 0
            };

            endDate = DH.clearTime(DH.add(startDate, 1, 'day'));

            ticks.push({ startDate, endDate });

            // proceed to next day
            startDate = endDate;
        }

        const
            // dummy calendar with 7 day borders ..to force forEachAvailabilityInterval to stop on each day start
            dummyCalendar        = new calendar.constructor({ intervals : ticks }),
            calendarsCombination = this.client.project.combineCalendars([calendar, dummyCalendar]);

        calendarsCombination.forEachAvailabilityInterval(
            { startDate : MIN_DATE, endDate },
            (startDate, endDate, calendarCacheInterval) => {
                const
                    calendarsStatus   = calendarCacheInterval.getCalendarsWorkStatus(),
                    dayData           = daysData[startDate.getDay()];

                // if the calendar has working interval for that period
                if (calendarsStatus.get(calendar)) {
                    // consider the day as working
                    dayData.DayWorking = 1;

                    dayData.WorkingTimes = dayData.WorkingTimes || { WorkingTime : [] };

                    // put that time range
                    dayData.WorkingTimes.WorkingTime.push({
                        FromTime : DH.format(startDate, timeFormat),
                        ToTime   : DH.format(endDate, timeFormat)
                    });
                }
            }
        );

        return Object.values(daysData);
    }

    /**
     * Format intervals to MS project format for the WorkWeeks property.
     * @param {Array} Array of intervals data.
     * @returns {Array} Array with data formatted
     * @private
     */
    // formatWorkWeeks(calendar) {
    //     // TODO: implement
    // }

    /**
     * Format Tasks from Gantt to MS Project format.
     * @returns {Array} Tasks array formatted
     * @private
     */
    getTasksData() {
        const
            me = this,
            { dateFormat } = me,
            { project } = me.client;

        return me.client.tasks.map(task => {
            const { wbsCode } = task;

            return {
                CalendarUID             : me.getCalendarUID(task.effectiveCalendar),
                ConstraintDate          : DH.format(task.constraintDate, dateFormat),
                ConstraintType          : constraintMap[task.constraintType],
                Deadline                : DH.format(task.deadlineDate, dateFormat),
                Duration                : me.toTimeSpanValue(task.duration, task.durationUnit),
                DurationFormat          : taskUnitMap[task.durationUnit],
                EarlyFinish             : DH.format(task.earlyEnd, dateFormat),
                EarlyStart              : DH.format(task.earlyStart, dateFormat),
                EffortDriven            : task.effortDriven ? 1 : 0,
                Estimated               : 0,
                Finish                  : DH.format(task.endDate, dateFormat),
                LateFinish              : DH.format(task.lateEnd, dateFormat),
                LateStart               : DH.format(task.lateStart, dateFormat),
                Manual                  : task.manuallyScheduled ? 1 : 0,
                ManualDuration          : me.toTimeSpanValue(task.duration, task.durationUnit),
                ManualFinish            : DH.format(task.endDate, dateFormat),
                ManualStart             : DH.format(task.startDate, dateFormat),
                Milestone               : task.isMilestone ? 1 : 0,
                Name                    : task.name,
                Notes                   : task.note,
                ProjectExternallyEdited : 0,
                OutlineLevel            : wbsCode.split('.').length,
                OutlineNumber           : wbsCode,
                // TODO: fix PercentComplete field because the value is missing for some specific tasks after export
                // PercentComplete         : Math.round(task.percentDone),
                PredecessorLink         : task.predecessors.map(predecessor => ({
                    LagFormat      : taskUnitMap[predecessor.lagUnit],
                    LinkLag        : project.convertDuration(predecessor.lag, predecessor.lagUnit, 'minute') * 10,
                    PredecessorUID : predecessor.fromEvent.id,
                    Type           : dependencyTypeMap[predecessor.type]
                })),
                RemainingDuration       : me.toTimeSpanValue(task.duration, task.durationUnit),
                Rollup                  : task.rollup ? 1 : 0,
                Start                   : DH.format(task.startDate, dateFormat),
                Summary                 : task.isLeaf ? 0 : 1,
                TotalSlack              : task.totalSlack,
                Type                    : task.isLeaf ? typeMap[task.schedulingMode] : 1,
                UID                     : task.id,
                WBS                     : wbsCode,
                Work                    : me.toTimeSpanValue(task.effort, task.effortUnit)
            };
        });
    }

    getCalendarUID(calendar, fallbackValue = -1) {
        return calendar && !calendar.isRoot ? calendar.internalId : fallbackValue;
    }

    /**
     * Format Resources from Gantt to MS Project format.
     * @returns {Array} Resources array formatted
     * @private
     */
    getResourcesData() {
        return this.client.resources.map(resource => ({
            // seems for version 2013 setting the calendar id it breaks so only Project level calendar is importable
            CalendarUID : this.msProjectVersion === 2013 ? null : this.getCalendarUID(resource.effectiveCalendar),
            Name        : resource.name,
            UID         : resource.id,
            Type        : 1
        }));
    }

    /**
     * Format Assignments from Gantt to MS Project format.
     * @returns {Array} Assignments array formatted
     * @private
     */
    getAssignmentsData() {
        // for version 2013 the assignments doesn't work
        if (this.msProjectVersion === 2013) {
            return [];
        }

        return this.client.assignments.map(({ data, event }) => ({
            Finish              : DH.format(event.endDate, this.dateFormat),
            // TODO it seems we need to provide effort per assignment value ..there is no ready to use field for that yet
            // PercentWorkComplete : Math.round(event.percentDone),
            ResourceUID         : data.resource,
            Start               : DH.format(event.startDate, this.dateFormat),
            TaskUID             : data.event,
            UID                 : data.id,
            Units               : data.units / 100
        }));
    }

    /**
     * Convert to MS Project Span Date Time format.
     * @param {Number} value The value to be converted.
     * @param {String} unit The unit of the value to be converted
     * @returns {String} The value formatted to "PTnHnMnS". E.g: PT10H30M, PT6H20M13S
     * @private
     */
    toTimeSpanValue(value, unit) {
        const
            delta = DH.getDelta(DH.as('ms', value, unit), { abbrev : true }),
            { w : weeks, min : mins, s : secs } = delta;

        let { yr : years, mon : months, d : days, h : hours } = delta;

        hours = hours || 0;

        // convert years, months, weeks and days to hours because MS Project work only with hours, minutes and seconds
        if (years) {
            hours += DH.as('h', years, 'y');
        }
        
        if (months) {
            hours += DH.as('h', months, 'month');
        }

        if (weeks) {
            hours += DH.as('h', weeks, 'w');
        }
        
        if (days) {
            hours += DH.as('h', days, 'd');
        }

        return `PT${hours}H${mins || 0}M${secs || 0}S`;
    }
}

GridFeatureManager.registerFeature(MspExport, false, 'Gantt');
