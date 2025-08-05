import DomClassList from '../../Core/helper/util/DomClassList.js';
import DomSync from '../../Core/helper/DomSync.js';
import Editor from '../../Core/widget/Editor.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module SchedulerPro/feature/EventSegments
 */

/**
 * This feature provides segmented events support. It implements rendering of such events and also adds a entries to the
 * event context menu allowing to split the selected event and rename segments.
 *
 * {@inlineexample SchedulerPro/feature/EventSegments.js}
 *
 * The feature is **enabled** by default
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype eventSegments
 * @feature
 */
export default class EventSegments extends InstancePlugin {



    //region Config

    static $name = 'EventSegments';

    static configurable = {
        /**
         * The split duration to be used when "Split event" menu item is called.
         * When set to zero (default) the duration is calculated automatically as the clicked tick duration
         * restricted by {@link #config-minSplitDuration} and {@link #config-maxSplitDuration} values.
         *
         * The duration can be provided as {@link Core.data.Duration} instance (or its configuration object) including
         * both numeric and unit parts.
         * ```js
         * ...
         * features : {
         *     eventSegments : {
         *         // split events by 1 day
         *         splitDuration : {
         *             magnitude : 1,
         *             unit      : "day"
         *         }
         *     }
         * ...
         * ```
         * Or it can be provided as a positive number which means it's expressed in the clicked event
         * {@link SchedulerPro.model.EventModel#field-durationUnit duration units}.
         * @config {Core.data.Duration|DurationConfig|Number}
         * @default
         */
        splitDuration : 0,

        /**
         * Maximum allowed {@link #config-splitDuration split duration}.
         * The value is used when calculating split duration automatically.
         *
         * Setting the config to zero means not limiting {@link #config-splitDuration split duration} max value.
         *
         * The duration can be provided as {@link Core.data.Duration} instance (or its configuration object) including
         * both numeric and unit parts.
         * ```javascript
         * ...
         * features : {
         *     eventSegments : {
         *         // split duration is automatic and changes depending on zoom level
         *         // but we limit its maximum as 1 week
         *         maxSplitDuration : {
         *             magnitude : 1,
         *             unit      : "week"
         *         }
         *     }
         * ...
         * ```
         * Or it can be provided as a positive number which means it's expressed in the clicked event
         * {@link SchedulerPro.model.EventModel#field-durationUnit duration units}.
         *
         * Defaults to 1 day.
         *
         * @config {Core.data.Duration|DurationConfig|Number}
         */
        maxSplitDuration : {
            magnitude : 1,
            unit      : 'day'
        },

        /**
         * Minimum allowed {@link #config-splitDuration split duration}.
         * The value is used when calculating split duration automatically.
         *
         * Setting the config to zero (default) means not limiting {@link #config-splitDuration split duration}
         * min value.
         *
         * The duration can be provided as {@link Core.data.Duration} instance (or its configuration object) including
         * both numeric and unit parts.
         * ```js
         * ...
         * features : {
         *     eventSegments : {
         *         // split duration is automatic and changes depending on zoom level
         *         // limit its minimum as 1 hour
         *         minSplitDuration : {
         *             magnitude : 1,
         *             unit      : "hour"
         *         }
         *         // we limit its maximum as 1 day
         *         maxSplitDuration : {
         *             magnitude : 1,
         *             unit      : "day"
         *         }
         *     }
         * ...
         * ```
         * Or it can be provided as a positive number which means it's expressed in the clicked event
         * {@link SchedulerPro.model.EventModel#field-durationUnit duration units}.
         * @config {Core.data.Duration|DurationConfig|Number}
         * @default
         */
        minSplitDuration : 0
    };

    static pluginConfig = {
        override : [
            'getElementsFromEventRecord',
            'resolveEventRecord',
            'resolveTaskRecord',
            'onElementMouseOut'
        ],
        chain : [
            'populateTaskMenu',
            'populateEventMenu',
            'onTaskDataGenerated',
            'onEventDataGenerated'
        ]
    };

    //endregion

    onElementMouseOut(event) {
        const
            me = this,
            { client } = me,
            { target, relatedTarget } = event,
            eventWrap                 = target.closest(client.eventSelector),
            timeSpanRecord            = client.resolveTimeSpanRecord(target);

        // We must be over the event bar
        if (timeSpanRecord?.isEventSegment && eventWrap && client.hoveredEvents.has(eventWrap)) {
            // out to child shouldn't count...
            if (relatedTarget && DomHelper.isDescendant(eventWrap, relatedTarget)) {
                return;
            }
        }

        this.overridden.onElementMouseOut(...arguments);
    }

    //region Segment record <-> DOM resolution

    // Override Scheduler getElementsFromEventRecord so it could handle segment records
    getElementsFromEventRecord(eventRecord, resourceRecord) {
        // if that's a segment
        if (eventRecord?.isEventSegment) {
            // get the main event element
            const mainEventElement = this.overridden.getElementsFromEventRecord(eventRecord.event, resourceRecord)[0];

            return [DomSync.getChild(mainEventElement, 'segments.' + eventRecord.segmentIndex)];
        }

        return this.overridden.getElementsFromEventRecord(...arguments);
    }

    // Override Scheduler resolveEventRecord so it could get segment record by element
    resolveEventRecord(elementOrEvent) {
        const
            element        = elementOrEvent instanceof Event ? elementOrEvent.target : elementOrEvent,
            segmentElement = element?.closest('.b-sch-event-segment');

        let result = this.overridden.resolveEventRecord(elementOrEvent);

        if (result?.segments && segmentElement) {
            result = result.segments[segmentElement.dataset.segment];
        }

        return result;
    }

    // Override Gantt resolveTaskRecord so it could get segment record by element
    resolveTaskRecord(element) {
        const segmentElement = element?.closest('.b-sch-event-segment');

        let result = this.overridden.resolveTaskRecord(element);

        if (result?.segments && segmentElement) {
            result = result.segments[segmentElement.dataset.segment];
        }

        return result;
    }

    //endregion

    //region Context menu

    populateTaskMenu(data) {
        data.eventRecord = data.taskRecord;

        // add entry if right clicked a task element
        if (data.targetElement.closest(data.feature.client.eventSelector)) {
            this.populateEventMenu(data);
        }
    }

    populateEventMenu({ eventRecord, taskRecord, items, domEvent }) {
        const me = this;

        // add "Split task" entry if the component is NOT in readonly mode
        // and it's not a summary task nor a milestone
        if (!me.client.readOnly && !eventRecord.isParent && !eventRecord.milestone) {
            items[`split${taskRecord ? 'Task' : 'Event'}`] = {
                localeClass : me,
                text        : `L{split${taskRecord ? 'Task' : 'Event'}}`,
                icon        : 'b-icon b-icon-cut',
                disabled    : eventRecord.readOnly,
                weight      : 650,
                separator   : true,
                onItem(context) {
                    me.splitEvent(context);
                }
            };

            const segmentElement = domEvent.target.closest('.b-sch-event-segment');
            if (segmentElement) {
                const segmentRecord = me.client.resolveEventRecord(segmentElement);
                items.renameSegment = {
                    localeClass : me,
                    text        : 'L{renameSegment}',
                    icon        : 'b-icon b-icon-rename',
                    disabled    : eventRecord.readOnly || segmentRecord.readOnly,
                    weight      : 660,
                    onItem() {
                        me.rename(segmentRecord, segmentElement);
                    }
                };
            }
        }
    }

    /**
     * Returns a date at which to split an event.
     *
     * Returns start date of the tick being clicked if the tick duration is less than {@link #config-maxSplitDuration}
     * or {@link #config-maxSplitDuration} is zero.
     * When the tick duration is greater than {@link #config-maxSplitDuration} returns `context.date` rounded based on
     * active time axis resolution unit.
     *
     * Override this method if you want to implement another way of calculating the split date.
     *
     * See also: {@link #function-getSplitDuration}, {@link #function-getSplitDurationUnit}.
     *
     * @param  {Object}                        context             Split function-call context
     * @param  {SchedulerPro.model.EventModel} context.eventRecord Event being split
     * @param  {Array}                         context.point       Click position. Array containing [x, y] coordinates
     * of mouse click.
     * @param  {Date}                          context.date        Date corresponding to the click position.
     * @param  {Object}                        context.tick        Time axis tick corresponding to the click position.
     * @param  {Scheduler.data.TimeAxis}       context.timeAxis    Time axis instance.
     * @return {Date} Returns a date to be used to split.
     */
    getSplitDate(context) {
        const
            {
                eventRecord,
                date,
                timeAxis
            } = context;

        // round clicked datetime relative to event start using active time axis resolution unit
        return timeAxis.roundDate(date, eventRecord.startDate);
    }

    /**
     * Returns the event split duration.
     *
     * If {@link #config-splitDuration} value is provided:
     * - as a `Number`the method returns the value as is
     * - as an `Object` or {@link Core/data/Duration} instance - the method returns the value `unit` part
     *
     * If {@link #config-splitDuration} is **NOT** provided the method returns
     * the clicked tick duration constrained by {@link #config-minSplitDuration} and
     * {@link #config-maxSplitDuration} values.
     *
     * Override this method if you want to implement another way of the split duration calculating.
     *
     * See also: {@link #function-getSplitDate}, {@link #function-getSplitDurationUnit}.
     *
     * @param  {Object}                        context             Split call context
     * @param  {SchedulerPro.model.EventModel} context.eventRecord Event being split
     * @param  {Array}                         context.point       Click position. Array containing [x, y]
     * coordinates of mouse click.
     * @param  {Date}                          context.date        Date corresponding to the click position.
     * @param  {Object}                        context.tick        Time axis tick corresponding to the click position.
     * @param  {Scheduler.data.TimeAxis}       context.timeAxis    Time axis instance.
     * @return {Number} Returns split duration.
     */
    getSplitDuration(context) {
        const {
            splitDuration,
            minSplitDuration,
            maxSplitDuration
        } = this;

        // if splitDuration is provided
        if (splitDuration?.magnitude) {
            return splitDuration.magnitude;
        }

        const { eventRecord, tick } = context;

        if (tick) {
            const
                splitUnit   = this.getSplitDurationUnit(context),
                { project } = eventRecord;

            // use 1 tick in MS as initial duration
            let splitDurationMS = tick.endDate - tick.startDate;

            // constrain duration w/ max
            if (maxSplitDuration) {
                const maxDurationMs = project.run('$convertDuration',
                    maxSplitDuration.magnitude,
                    maxSplitDuration.unit || splitUnit,
                    'millisecond'
                );

                splitDurationMS = Math.min(splitDurationMS, maxDurationMs);
            }

            // constrain duration w/ min
            if (minSplitDuration) {
                const minDurationMs = project.run('$convertDuration',
                    minSplitDuration.magnitude,
                    minSplitDuration.unit || splitUnit,
                    'millisecond'
                );

                splitDurationMS = Math.max(splitDurationMS, minDurationMs);
            }

            // convert value to proper unit
            return project.run('$convertDuration', splitDurationMS, 'millisecond', splitUnit);
        }
    }

    /**
     * Returns the duration unit to be used for the event splitting.
     *
     * When {@link #config-splitDuration} is provided as {@link Core/data/Duration} instance
     * or its configuration Object:
     * ```js
     * ...
     * features : {
     *     eventSegments : {
     *         // split events by 1 day
     *         splitDuration : {
     *             magnitude : 1,
     *             unit      : "day"
     *         }
     *     }
     *     ...
     * }
     *
     * ```
     * the method returns the value `unit` part otherwise it returns the event
     * {@link SchedulerPro.model.EventModel#field-durationUnit}.
     *
     * Override this method config-if you want to implement another way of the split duration unit defining.
     *
     * See also: {@link #function-getSplitDate}, {@link #function-getSplitDuration}.
     *
     * @param  {Object}                        context             Split function-call context
     * @param  {SchedulerPro.model.EventModel} context.eventRecord Event being split
     * @param  {Array}                         context.point       Click position. Array containing [x, y] coordinates
     * of mouse click.
     * @param  {Date}                          context.date        Date corresponding to the click position.
     * @param  {Object}                        context.tick        Time axis tick corresponding to the click position.
     * @param  {Scheduler.data.TimeAxis}       context.timeAxis    Time axis instance.
     * @return {String} Returns split duration unit.
     */
    getSplitDurationUnit(context) {
        const { splitDuration } = this;

        // use provided "splitDuration" unit or fallback to the event "durationUnit"
        return splitDuration?.unit || context.eventRecord.durationUnit;
    }

    /**
     * Handler for the "Split event" menu item
     * @internal
     */
    splitEvent(context) {
        const
            { client }   = context.feature,
            { timeAxis } = client;

        context.date     = client.getDateFromXY(context.point, undefined, false);
        context.tick     = timeAxis.getSnappedTickFromDate(context.date);
        context.timeAxis = timeAxis;

        context.eventRecord.splitToSegments(
            this.getSplitDate(context),
            this.getSplitDuration(context),
            this.getSplitDurationUnit(context)
        );
    }

    rename(segmentRecord, element) {
        const { client } = this;

        const editor = new Editor({
            owner        : client,
            appendTo     : client.timeAxisSubGridElement,
            scrollAction : 'realign',
            align        : {
                align : 'c-c'
            },
            cls               : 'b-event-segment-renamer',
            internalListeners : {
                complete() {

                    client.refresh();
                },
                thisObj : this
            }
        });

        editor.startEdit({
            target : element,
            record : segmentRecord,
            field  : 'name'
        });
    }

    //endregion

    //region Contents

    doDisable(disable) {
        if (this.client.isPainted) {
            this.client.refresh();
        }

        super.doDisable(disable);
    }

    generateSegmentRenderData(segmentRecord, renderData) {
        const { client } = this;
        let result, segmentRenderData;

        if (client.isGantt) {
            const
                taskRendering = client.currentOrientation,
                box           = taskRendering.getTaskBox(segmentRecord),
                data          = {
                    taskRecord : segmentRecord,

                    task       : segmentRecord,
                    row        : renderData.row,
                    children   : []
                };

            if (box) {
                Object.assign(data, {
                    isTask : true,
                    top    : box.top,
                    left   : box.left,
                    width  : box.width,
                    height : box.height
                });
            }

            taskRendering.internalPopulateTaskRenderData(data, segmentRecord);

            segmentRenderData = data;
        }
        else {
            segmentRenderData = client.generateRenderData(segmentRecord, renderData.resourceRecord, true);
        }

        if (segmentRenderData) {
            const
                eventColor      = segmentRenderData.eventColor || renderData.eventColor,
                isDefaultColor  = DomHelper.isNamedColor(eventColor);

            result              = {
                segmentRecord,
                eventContent : (segmentRenderData.eventContent || segmentRenderData.taskContent),
                cls          : segmentRenderData.cls || segmentRecord.cls?.clone() || new DomClassList(),
                top          : renderData.top,
                left         : segmentRenderData.left - renderData.left,
                width        : segmentRenderData.width,
                height       : renderData.height,
                style        : ''
            };

            // We got an eventColor from the main event or the segment, and it is not a default color class
            if (eventColor && !isDefaultColor) {
                result.style = { 'background-color' : eventColor };

                // If a eventColor is specified on the main event remove styling from outer element (added in
                // SchedulerEventRendering.js)
                if (renderData.eventColor && renderData._customColorStyle) {
                    renderData.style = renderData.style.replace(renderData._customColorStyle, '');
                }
            }

            Object.assign(result.cls,
                {
                    [`b-sch-color-${eventColor}`] : isDefaultColor,
                    'b-sch-color-none'            : !eventColor,
                    'b-sch-event-segment'         : true,
                    'b-first'                     : !segmentRecord.previousSegment,
                    'b-last'                      : !segmentRecord.nextSegment
                },
                segmentRenderData.cls
            );

            // Named colors are applied as a class to the wrapper
            if (DomHelper.isNamedColor(eventColor)) {
                result.cls[`b-sch-color-${eventColor}`] = eventColor;
            }
            else if (eventColor) {
                result.style = `background-color:${eventColor};` + result.style;
                result.cls['b-sch-custom-color'] = 1;
            }
            else {
                renderData.wrapperCls['b-sch-color-none'] = 1;
            }
        }

        return result;
    }

    appendDOMConfig(renderData) {
        const eventRecord = renderData.eventRecord || renderData.taskRecord;

        if (eventRecord.segments && !this.disabled) {

            const
                eventContent = renderData.eventContent || renderData.taskContent,
                index = renderData.children.indexOf(eventContent);

            // remove generated event content
            if (index > -1) {
                renderData.children.splice(index, 1);
            }

            delete renderData.eventContent;
            delete renderData.taskContent;

            renderData.cls['b-segmented'] = true;

            renderData.segments = eventRecord.segments.map(segment => this.generateSegmentRenderData(segment, renderData));

            renderData.segmentsDOMConfig = renderData.segments.map(this.getSegmentDOMConfig.bind(this));

            renderData.children.unshift(
                {
                    syncOptions : {
                        syncIdField      : 'segment',
                        releaseThreshold : 0
                    },
                    className : 'b-sch-event-segments',
                    dataset   : {
                        taskBarFeature : 'segments'
                    },
                    children : renderData.segmentsDOMConfig
                }
            );
        }
    }

    getSegmentDOMConfig(segmentData, index) {
        return {
            className : segmentData.cls,
            style     : {
                style  : segmentData.style,
                left   : segmentData.left,
                height : segmentData.height,
                width  : segmentData.width,
                ...segmentData.style
            },
            dataset : {
                segment : index
            },
            syncOptions : {
                syncIdField : 'taskBarFeature'
            },
            children : [
                segmentData.eventContent
            ]
        };
    }

    // For Scheduler Pro
    onEventDataGenerated(eventData) {
        this.appendDOMConfig(eventData);
    }

    // For Gantt
    onTaskDataGenerated(taskData) {
        this.appendDOMConfig(taskData);
    }

    //endregion

    // No classname on Scheduler's/Gantt's element
    get featureClass() {}
}

GridFeatureManager.registerFeature(EventSegments, true, 'SchedulerPro');
GridFeatureManager.registerFeature(EventSegments, true, 'Gantt');
