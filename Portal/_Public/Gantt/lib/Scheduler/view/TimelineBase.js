import GlobalEvents from '../../Core/GlobalEvents.js';
import GridBase from '../../Grid/view/GridBase.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import FunctionHelper from '../../Core/helper/FunctionHelper.js';
import ResizeMonitor from '../../Core/helper/ResizeMonitor.js';
import Collection from '../../Core/util/Collection.js';
import IdHelper from '../../Core/helper/IdHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

import VersionHelper from '../../Core/helper/VersionHelper.js';
import TimeAxis from '../data/TimeAxis.js';

import TimeAxisViewModel from './model/TimeAxisViewModel.js';
import TimelineDateMapper from './mixin/TimelineDateMapper.js';
import TimelineDomEvents from './mixin/TimelineDomEvents.js';
import TimelineViewPresets from './mixin/TimelineViewPresets.js';
import TimelineZoomable from './mixin/TimelineZoomable.js';
import RecurringEvents from './mixin/RecurringEvents.js';
import TimelineEventRendering from './mixin/TimelineEventRendering.js';
import TimelineScroll from './mixin/TimelineScroll.js';
import TimelineState from './mixin/TimelineState.js';
import './TimeAxisSubGrid.js';

const
    exitTransition = {
        fn                : 'exitTransition',
        delay             : 0,
        cancelOutstanding : true
    },
    inRange = (v, r0, r1) => (r0 == null)
        ? (r1 == null || v < r1)
        : (r1 == null)
            ? v >= r0
            : (r0 < r1)
                ? (r0 <= v && v < r1)       // 5 in [1, 10]  (after 1 and before 10)
                : (v < r1 || r0 <= v),      // 5 in [10, 1]  (after 10 or before 1)
    isWorkingTime = (d, wt) => inRange(d.getDay(), wt.fromDay, wt.toDay) && inRange(d.getHours(), wt.fromHour, wt.toHour),
    emptyObject   = {};

/**
 * @module Scheduler/view/TimelineBase
 */

/**
 * Options accepted by the Scheduler's {@link Scheduler.view.Scheduler#config-visibleDate} property.
 *
 * @typedef {Object} VisibleDate
 * @property {Date} date The date to bring into view.
 * @property {'start'|'end'|'center'|'nearest'} [block] How far to scroll the date.
 * @property {Number} [edgeOffset] edgeOffset A margin around the date to bring into view.
 * @property {AnimateScrollOptions|Boolean|Number} [animate] Set to `true` to animate the scroll by 300ms,
 * or the number of milliseconds to animate over, or an animation config object.
 */

/**
 * Abstract base class used by timeline based components such as Scheduler and Gantt. Based on Grid, supplies a "locked"
 * region for columns and a "normal" for rendering of events etc.
 * @abstract
 *
 * @mixes Scheduler/view/mixin/TimelineDateMapper
 * @mixes Scheduler/view/mixin/TimelineDomEvents
 * @mixes Scheduler/view/mixin/TimelineEventRendering
 * @mixes Scheduler/view/mixin/TimelineScroll
 * @mixes Scheduler/view/mixin/TimelineState
 * @mixes Scheduler/view/mixin/TimelineViewPresets
 * @mixes Scheduler/view/mixin/TimelineZoomable
 * @mixes Scheduler/view/mixin/RecurringEvents
 *
 * @extends Grid/view/Grid
 */
export default class TimelineBase extends GridBase.mixin(
    TimelineDateMapper,
    TimelineDomEvents,
    TimelineEventRendering,
    TimelineScroll,
    TimelineState,
    TimelineViewPresets,
    TimelineZoomable,
    RecurringEvents
) {
    //region Config

    static get $name() {
        return 'TimelineBase';
    }

    // Factoryable type name
    static get type() {
        return 'timelinebase';
    }

    static configurable = {
        partnerSharedConfigs : {
            value : ['timeAxisViewModel', 'timeAxis', 'viewPreset'],

            $config : {
                merge : 'distinct'
            }
        },

        /**
         * Get/set startDate. Defaults to current date if none specified.
         *
         * When using {@link #config-infiniteScroll}, use {@link #config-visibleDate} to control initially visible date
         * instead.
         *
         * **Note:** If you need to set start and end date at the same time, use {@link #function-setTimeSpan} method.
         * @member {Date} startDate
         * @category Common
         */
        /**
         * The start date of the timeline (if not configure with {@link #config-infiniteScroll}).
         *
         * If omitted, and a TimeAxis has been set, the start date of the provided {@link Scheduler.data.TimeAxis} will
         * be used. If no TimeAxis has been configured, it'll use the start/end dates of the loaded event dataset. If no
         * date information exists in the event data set, it defaults to the current date and time.
         *
         * If a string is supplied, it will be parsed using
         * {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat}.
         *
         * When using {@link #config-infiniteScroll}, use {@link #config-visibleDate} to control initially visible date
         * instead.
         *
         * **Note:** If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
         * @config {Date|String}
         * @category Common
         */
        startDate : {
            $config : {
                equal : 'date'
            },
            value : null
        },

        /**
         * Get/set endDate. Defaults to startDate + default span of the used ViewPreset.
         *
         * **Note:** If you need to set start and end date at the same time, use {@link #function-setTimeSpan} method.
         * @member {Date} endDate
         * @category Common
         */
        /**
         * The end date of the timeline (if not configure with {@link #config-infiniteScroll}).
         *
         * If omitted, it will be calculated based on the {@link #config-startDate} setting and the 'defaultSpan'
         * property of the current {@link #config-viewPreset}.
         *
         * If a string is supplied, it will be parsed using
         * {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat}.
         *
         * **Note:** If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
         * @config {Date|String}
         * @category Common
         */
        endDate : {
            $config : {
                equal : 'date'
            },
            value : null
        },

        /**
         * When set, the text in the major time axis header sticks in the scrolling viewport as long as possible.
         * @config {Boolean}
         * @default
         * @category Time axis
         */
        stickyHeaders : true,

        /**
         * A scrolling `options` object describing the scroll action, including a `date` option
         * which references a `Date`. See {@link #function-scrollToDate} for details about scrolling options.
         *
         * ```javascript
         *     // The date we want in the center of the Scheduler viewport
         *     myScheduler.visibleDate = {
         *         date    : new Date(2023, 5, 17, 12),
         *         block   : 'center',
         *         animate : true
         *     };
         * ```
         * @member {Object} visibleDate
         * @category Common
         */
        /**
         * A date to bring into view initially on the scrollable timeline.
         *
         * This may be configured as either a `Date` or a scrolling `options` object describing
         * the scroll action, including a `date` option which references a `Date`.
         *
         * See {@link #function-scrollToDate} for details about scrolling options.
         *
         * Note that if a naked `Date` is passed, it will be stored internally as a scrolling options object
         * using the following defaults:
         *
         * ```javascript
         * {
         *     date  : <The Date object>,
         *     block : 'nearest'
         * }
         * ```
         *
         * This moves the date into view by the shortest scroll, so that it just appears at an edge.
         *
         * To bring your date of interest to the center of the viewport, configure your
         * Scheduler thus:
         *
         * ```javascript
         *     visibleDate : {
         *         date  : new Date(2023, 5, 17, 12),
         *         block : 'center'
         *     }
         * ```
         * @config {Date|VisibleDate}
         * @category Common
         */
        visibleDate : null,

        /**
         * CSS class to add to rendered events
         * @config {String}
         * @category CSS
         * @private
         */
        eventCls : null,

        /**
         * Set to `true` to force the time columns to fit to the available space (horizontal or vertical depends on mode).
         * Note that setting {@link #config-suppressFit} to `true`, will disable `forceFit` functionality. Zooming
         * cannot be used when `forceFit` is set.
         * @prp {Boolean}
         * @default
         * @category Time axis
         */
        forceFit : false,

        /**
         * Set to a time zone or a UTC offset. This will set the projects
         * {@link Scheduler.model.ProjectModel#config-timeZone} config accordingly. As this config is only a referer,
         * please se project's config {@link Scheduler.model.ProjectModel#config-timeZone documentation} for more
         * information.
         *
         * ```javascript
         * new Calendar(){
         *   timeZone : 'America/Chicago'
         * }
         * ```
         * @prp {String|Number} timeZone
         * @category Misc
         */
        timeZone : null

    };

    static get defaultConfig() {
        return {
            /**
             * A valid JS day index between 0-6 (0: Sunday, 1: Monday etc.) to be considered the start day of the week.
             * When omitted, the week start day is retrieved from the active locale class.
             * @config {Number} weekStartDay
             * @category Time axis
             */

            /**
             * An object with format `{ fromDay, toDay, fromHour, toHour }` that describes the working days and hours.
             * This object will be used to populate TimeAxis {@link Scheduler.data.TimeAxis#config-include} property.
             *
             * Using it results in a non-continuous time axis. Any ticks not covered by the working days and hours will
             * be excluded. Events within larger ticks (for example if using week as the unit for ticks) will be
             * stretched to fill the gap otherwise left by the non working hours.
             *
             * As with end dates, `toDay` and `toHour` are exclusive. Thus `toDay : 6` means that day 6 (saturday) will
             * not be included.
             *
             *
             * **NOTE:** When this feature is enabled {@link Scheduler.view.mixin.TimelineZoomable Zooming feature} is
             * not supported. It's recommended to disable zooming controls:
             *
             * ```javascript
             * new Scheduler({
             *     zoomOnMouseWheel          : false,
             *     zoomOnTimeAxisDoubleClick : false,
             *     ...
             * });
             * ```
             *
             * @config {Object}
             * @category Time axis
             */
            workingTime : null,

            /**
             * A backing data store of 'ticks' providing the input date data for the time axis of timeline panel.
             * @member {Scheduler.data.TimeAxis} timeAxis
             * @readonly
             * @category Time axis
             */

            /**
             * A {@link Scheduler.data.TimeAxis} config object or instance, used to create a backing data store of
             * 'ticks' providing the input date data for the time axis of timeline panel. Created automatically if none
             * supplied.
             * @config {TimeAxisConfig|Scheduler.data.TimeAxis}
             * @category Time axis
             */
            timeAxis : null,

            /**
             * The backing view model for the visual representation of the time axis.
             * Either a real instance or a simple config object.
             * @private
             * @config {Scheduler.view.model.TimeAxisViewModel|TimeAxisViewModelConfig}
             * @category Time axis
             */
            timeAxisViewModel : null,

            /**
             * You can set this option to `false` to make the timeline panel start and end on the exact provided
             * {@link #config-startDate}/{@link #config-endDate} w/o adjusting them.
             * @config {Boolean}
             * @default
             * @category Time axis
             */
            autoAdjustTimeAxis : true,

            /**
             * Affects drag drop and resizing of events when {@link Scheduler/view/mixin/TimelineDateMapper#config-snap}
             * is enabled.
             *
             * If set to `true`, dates will be snapped relative to event start. e.g. for a zoom level with
             * `timeResolution = { unit: "s", increment: "20" }`, an event that starts at 10:00:03 and is dragged would
             * snap its start date to 10:00:23, 10:00:43 etc.
             *
             * When set to `false`, dates will be snapped relative to the timeAxis startDate (tick start)
             * - 10:00:03 -> 10:00:20, 10:00:40 etc.
             *
             * @config {Boolean}
             * @default
             * @category Scheduled events
             */
            snapRelativeToEventStartDate : false,

            /**
             * Set to `true` to prevent auto calculating of a minimal {@link Scheduler.view.mixin.TimelineEventRendering#property-tickSize}
             * to always fit the content to the screen size. Setting this property on `true` will disable {@link #config-forceFit} behaviour.
             * @config {Boolean}
             * @default false
             * @category Time axis
             */
            suppressFit : false,

            /**
             * CSS class to add to cells in the timeaxis column
             * @config {String}
             * @category CSS
             * @private
             */
            timeCellCls : null,

            scheduledEventName : null,

            //dblClickTime : 200,

            /**
             * A CSS class to apply to each event in the view on mouseover.
             * @config {String}
             * @category CSS
             * @private
             */
            overScheduledEventClass : null,

            // allow the panel to prevent adding the hover CSS class in some cases - during drag drop operations
            preventOverCls : false,

            // This setting is set to true by features that need it
            useBackgroundCanvas : false,

            /**
             * Set to `false` if you don't want event bar DOM updates to animate.
             * @prp {Boolean}
             * @default true
             * @category Scheduled events
             */
            enableEventAnimations : true,

            disableGridRowModelWarning : true,

            // does not look good with locked columns and also interferes with event animations
            animateRemovingRows : false,

            /**
             * Partners this Timeline panel with another Timeline in order to sync their region sizes (sub-grids like locked, normal will get the same width),
             * start and end dates, view preset, zoom level and scrolling position. All these values will be synced with the timeline defined as the `partner`.
             *
             * - To add a new partner dynamically see {@link #function-addPartner} method.
             * - To remove existing partner see {@link #function-removePartner} method.
             * - To check if timelines are partners see {@link #function-isPartneredWith} method.
             *
             * Column widths and hide/show state are synced between partnered schedulers when the column set is identical.
             * @config {Scheduler.view.TimelineBase}
             * @category Time axis
             */
            partner : null,

            schedulerRegion : 'normal',

            transitionDuration : 200,
            // internal timer id reference
            animationTimeout   : null,

            /**
             * Region to which columns are added when they have none specified
             * @config {String}
             * @default
             * @category Misc
             */
            defaultRegion : 'locked',

            /**
             * Decimal precision used when displaying durations, used by tooltips and DurationColumn.
             * Specify `false` to use raw value
             * @config {Number|Boolean}
             * @default
             * @category Common
             */
            durationDisplayPrecision : 1,

            /**
             * An object with configuration for the {@link Scheduler.column.TimeAxisColumn} in horizontal
             * {@link Scheduler.view.SchedulerBase#config-mode}.
             *
             * Example:
             *
             * ```javascript
             * new Scheduler({
             *     timeAxisColumn : {
             *         renderer : ({ record, cellElement }) => {
             *             // output some markup as a layer below the events layer, you can draw a chart for example
             *         }
             *     },
             *     ...
             * });
             * ```
             *
             * @config {TimeAxisColumnConfig} timeAxisColumn
             * @category Time axis
             */

            asyncEventSuffix : 'PreCommit'
        };
    }

    timeCellSelector = null;

    updateTimeZone(timeZone) {
        if (this.project) {
            if (this.isConfiguring) {
                this.project._isConfiguringTimeZone = true;
            }
            this.project.timeZone = timeZone;
        }
    }

    get timeZone() {
        return this.project?.timeZone;
    }

    //endregion

    //region Feature hooks

    /**
     * Populates the event context menu. Chained in features to add menu items.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown.
     * @param {Scheduler.model.EventModel} options.eventRecord The context event.
     * @param {Scheduler.model.ResourceModel} options.resourceRecord The context resource.
     * @param {Scheduler.model.AssignmentModel} options.assignmentRecord The context assignment if any.
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items.
     * @internal
     */
    populateEventMenu() {}

    /**
     * Populates the time axis context menu. Chained in features to add menu items.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown.
     * @param {Scheduler.model.ResourceModel} options.resourceRecord The context resource.
     * @param {Date} options.date The Date corresponding to the mouse position in the time axis.
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items.
     * @internal
     */
    populateScheduleMenu() {}

    // Called when visible date range potentially changes such as when scrolling in
    // the time axis.
    onVisibleDateRangeChange(range) {
        if (!this.handlingVisibleDateRangeChange) {
            const
                me                    = this,
                { _visibleDateRange } = me,
                dateRangeChange       = !_visibleDateRange || (_visibleDateRange.startDate - range.startDate || _visibleDateRange.endDate - range.endDate);

            if (dateRangeChange) {
                me.timeView.range                 = range;
                me.handlingVisibleDateRangeChange = true;

                /**
                 * Fired when the range of dates visible within the viewport changes. This will be when
                 * scrolling along a time axis.
                 *
                 * __Note__ that this event will fire frequently during scrolling, so any listener
                 * should probably be added with the `buffer` option to slow down the calls to your
                 * handler function :
                 *
                 * ```javascript
                 * listeners : {
                 *     visibleDateRangeChange({ old, new }) {
                 *         this.updateRangeRequired(old, new);
                 *     },
                 *     // Only call once. 300 ms after the last event was detected
                 *     buffer : 300
                 * }
                 * ```
                 * @event visibleDateRangeChange
                 * @param {Scheduler.view.Scheduler} source This Scheduler instance.
                 * @param {Object} old The old date range
                 * @param {Date} old.startDate the old start date.
                 * @param {Date} old.endDate the old end date.
                 * @param {Object} new The new date range
                 * @param {Date} new.startDate the new start date.
                 * @param {Date} new.endDate the new end date.
                 */
                me.trigger('visibleDateRangeChange', {
                    old : _visibleDateRange,
                    new : range
                });
                me.handlingVisibleDateRangeChange = false;
                me._visibleDateRange              = range;
            }
        }
    }

    // Called when visible resource range changes in vertical mode
    onVisibleResourceRangeChange() {}

    //endregion

    //region Init

    construct(config = {}) {
        const me = this;

        super.construct(config);

        me.$firstVerticalOverflow = true;

        me.initDomEvents();

        me.currentOrientation.init();

        me.rowManager.ion({
            refresh : () => {
                me.forceLayout = false;
            }
        });
    }

    // Override from Grid.view.GridSubGrids
    createSubGrid(region, config = {}) {
        const
            me                = this,
            { stickyHeaders } = me;

        // We are creating the TimeAxisSubGrid
        if (region === (me.schedulerRegion || 'normal')) {
            config.type = 'timeaxissubgrid';
        }

        // The assumption is that if we are in vertical mode, the locked SubGrid
        // is used to house the verticalTimeAxis, and so it must all be overflow:visible
        else if (region === 'locked' && stickyHeaders && me.isVertical) {
            config.scrollable = {
                overflowX : 'visible',
                overflowY : 'visible'
            };

            // It's the child of the overflowElement
            me.bodyContainer.classList.add('b-sticky-headers');
        }

        return super.createSubGrid(region, config);
    }

    doDestroy() {
        const
            me                                    = this,
            { partneredWith, currentOrientation } = me;

        currentOrientation?.destroy();

        // Break links between this TimeLine and any partners.
        if (partneredWith) {
            partneredWith.forEach(p => {
                me.removePartner(p);
            });
            partneredWith.destroy();
        }
        else {
            me.timeAxisViewModel.destroy();
            me.timeAxis.destroy();
        }

        super.doDestroy();
    }

    startConfigure(config) {
        super.startConfigure(config);

        // When the body height changes, we must update the SchedulerViewport's height
        ResizeMonitor.addResizeListener(this.bodyContainer, this.onBodyResize.bind(this));

        // partner needs to be initialized first so that the various shared
        // configs are assigned first before we default them in.
        this.getConfig('partner');
    }

    changeStartDate(startDate) {
        if (typeof startDate === 'string') {
            startDate = DateHelper.parse(startDate);
        }
        return startDate;
    }

    onPaint({ firstPaint }) {
        // Upon first paint we need to pass the forceUpdate flag in case we are sharing the TimAxisViewModel
        // with another Timeline which will already have done this.
        if (firstPaint) {
            // Take height from container element

            const
                me             = this,
                scrollable     = me.isHorizontal ? me.timeAxisSubGrid.scrollable : me.scrollable,
                // Use exact subpixel available space so that tick size calculation is correct.
                availableSpace = scrollable.element.getBoundingClientRect()[me.isHorizontal ? 'width' : 'height'];

            // silent = true if infiniteScroll. If that is set, TimelineScroll.initScroll which is
            // called by the base class's onPaint reconfigures the TAVM when it initializes.
            me.timeAxisViewModel.update(availableSpace, me.infiniteScroll, true);

            // If infiniteScroll caused the TAVM update to be silent, force the rendering to
            // get hold of the scroll state and visible range
            if (me.infiniteScroll) {
                me.currentOrientation.doUpdateTimeView?.();
            }
        }

        super.onPaint(...arguments);
    }

    onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX) {
        // rerender cells in scheduler column on horizontal scroll to display events in view
        this.currentOrientation.updateFromHorizontalScroll(scrollX);

        super.onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX);
    }

    /**
     * Overrides initScroll from Grid, listens for horizontal scroll to do virtual event rendering
     * @private
     */
    initScroll() {
        const me = this;

        let frameCount = 0;

        super.initScroll();

        me.ion({
            horizontalScroll : ({ subGrid, scrollLeft, scrollX }) => {
                if (me.isPainted && subGrid === me.timeAxisSubGrid && !me.isDestroying && !me.refreshSuspended) {
                    me.onSchedulerHorizontalScroll(subGrid, scrollLeft, scrollX);
                }
                frameCount++;
            }
        });

        if (me.testPerformance === 'horizontal') {
            me.setTimeout(() => {
                const start     = performance.now();
                let scrollSpeed = 5,
                    direction   = 1;

                const scrollInterval = me.setInterval(() => {
                    scrollSpeed = scrollSpeed + 5;

                    me.scrollX += (10 + Math.floor(scrollSpeed)) * direction;

                    if (direction === 1 && me.scrollX > 5500) {
                        direction   = -1;
                        scrollSpeed = 5;
                    }

                    if (direction === -1 && me.scrollX <= 0) {
                        const
                            done    = performance.now(), // eslint-disable-line no-undef
                            elapsed = done - start;

                        const
                            timePerFrame = elapsed / frameCount,
                            fps          = Math.round((1000 / timePerFrame) * 10) / 10;

                        clearInterval(scrollInterval);

                        console.log(me.eventPositionMode, me.eventScrollMode, fps + 'fps');
                    }
                }, 0);
            }, 500);
        }
    }

    //endregion

    /**
     * Calls the specified function (returning its return value) and preserves the timeline center
     * point. This is a useful way of retaining the user's visual context while making updates
     * and changes to the view which require major changes or a full refresh.
     * @param {Function} fn The function to call.
     * @param {Object} thisObj The `this` context for the function.
     * @param {...*} args Parameters to the function.
     */
    preserveViewCenter(fn, thisObj = this, ...args) {
        const
            me             = this,
            centerDate     = me.viewportCenterDate,
            result         = fn.apply(thisObj, args),
            scroller       = me.timelineScroller,
            { clientSize } = scroller,
            scrollStart    = Math.max(Math.floor(me.getCoordinateFromDate(centerDate, true) - clientSize / 2), 0);

        me.scrollingToCenter = true;
        scroller.scrollTo(scrollStart, false).then(() => me.scrollingToCenter = false);

        return result;
    }

    /**
     * Changes this Scheduler's time axis timespan to the supplied start and end dates.
     *
     * @async
     * @param {Date} newStartDate The new start date
     * @param {Date} newEndDate The new end date
     * @param {Object} [options] An object containing modifiers for the time span change operation.
     * @param {Boolean} [options.maintainVisibleStart] Specify as `true` to keep the visible start date stable.
     * @param {Date} [options.visibleDate] The date inside the range to scroll into view
     */
    setTimeSpan(newStartDate, newEndDate, options = emptyObject) {
        const
            me           = this,
            { timeAxis } = me,
            {
                preventThrow = false, // Private, only used by the shift method.
                maintainVisibleStart = false,
                visibleDate
            }            = options,
            {
                startDate,
                endDate
            }            = timeAxis.getAdjustedDates(newStartDate, newEndDate),
            startChanged = timeAxis.startDate - startDate !== 0,
            endChanged   = timeAxis.endDate - endDate !== 0;

        if (startChanged || endChanged) {
            if (maintainVisibleStart) {
                const
                    {
                        timeAxisViewModel
                    }             = me,
                    { totalSize } = timeAxisViewModel,
                    oldTickSize   = timeAxisViewModel.tickSize,
                    scrollable    = me.timelineScroller,
                    currentScroll = scrollable.position,
                    visibleStart  = timeAxisViewModel.getDateFromPosition(currentScroll);

                // If the current visibleStart is in the new range, maintain it
                // So that there is no visual jump.
                if (visibleStart >= startDate && visibleStart < endDate) {
                    // We need to correct the scroll position as soon as the TimeAxisViewModel
                    // has updated itself and before any other UI updates which that may trigger.
                    timeAxisViewModel.ion({
                        update() {
                            const tickSizeChanged = timeAxisViewModel.tickSize !== oldTickSize;

                            // Ensure the canvas element matches the TimeAxisViewModel's new totalSize.
                            // This creates the required scroll range to be able to have the scroll
                            // position correct before any further UI updates.
                            me.updateCanvasSize();

                            // If *only* the start moved, we can keep scroll position the same
                            // by adjusting it by the amount the start moved.
                            if (startChanged && !endChanged && !tickSizeChanged) {
                                scrollable.position += (timeAxisViewModel.totalSize - totalSize);
                            }
                            // If only the end has changed, and tick size is same, we can maintain
                            // the same scroll position.
                            else if (!startChanged && !tickSizeChanged) {
                                scrollable.position = currentScroll;
                            }
                            // Fall back to restoring the position by restoring the visible start time
                            else {
                                scrollable.position = timeAxisViewModel.getPositionFromDate(visibleStart);
                            }

                            // Force partners to sync with what we've just done to reset the scroll.
                            // We are now in control.
                            scrollable.syncPartners(true);
                        },
                        prio : 10000,
                        once : true
                    });
                }
            }

            const returnValue = timeAxis.reconfigure({
                startDate,
                endDate
            }, false, preventThrow);

            if (visibleDate) {
                return me.scrollToDate(visibleDate, options).then(() => returnValue);
            }

            return returnValue;
        }
    }

    //region Config getters/setters

    /**
     * Returns `true` if any of the events/tasks or feature injected elements (such as ResourceTimeRanges) are within
     * the {@link #config-timeAxis}
     * @property {Boolean}
     * @readonly
     * @category Scheduled events
     */
    get hasVisibleEvents() {
        return !this.noFeatureElementsInAxis() || this.eventStore.storage.values.some(t => this.timeAxis.isTimeSpanInAxis(t));
    }

    // Template function to be chained in features to determine if any elements are in time axis (needed since we cannot
    // currently chain getters). Negated to not break chain. First feature that has elements visible returns false,
    // which prevents other features from being queried.
    noFeatureElementsInAxis() { }

    // Private getter used to piece together event names such as beforeEventDrag / beforeTaskDrag. Could also be used
    // in templates.
    get capitalizedEventName() {
        if (!this._capitalizedEventName) {
            this._capitalizedEventName = StringHelper.capitalize(this.scheduledEventName);
        }

        return this._capitalizedEventName;
    }

    set partner(partner) {
        this._partner = partner;

        this.addPartner(partner);
    }

    /**
     * Partners this Timeline with the passed Timeline in order to sync the horizontal scrolling position and zoom level.
     *
     * - To remove existing partner see {@link #function-removePartner} method.
     * - To get the list of partners see {@link #property-partners} getter.
     *
     * @param {Scheduler.view.TimelineBase} otherTimeline The timeline to partner with
     */
    addPartner(partner) {
        const me = this;

        if (!me.isPartneredWith(partner)) {
            const partneredWith = me.partneredWith || (me.partneredWith = new Collection());

            // Each must know about the other so that they can sync others upon region resize
            partneredWith.add(partner);

            (partner.partneredWith || (partner.partneredWith = new Collection())).add(me);

            // Flush through viewPreset initGetter so that the setup in setConfig doesn't
            // take them to be the class's defined getters.
            me.getConfig('viewPreset');

            partner.ion({
                presetchange : 'onPartnerPresetChange',
                thisObj      : me
            });
            partner.scrollable.ion({
                overflowChange : 'onPartnerOverflowChange',
                thisObj        : me
            });

            // collect configs that are meant to be shared between partners
            const partnerSharedConfig = me.partnerSharedConfigs.reduce((config, configName) => {
                config[configName] = partner[configName];
                return config;
            }, {});

            me.setConfig(partnerSharedConfig);

            me.ion({
                presetchange : 'onPartnerPresetChange',
                thisObj      : partner
            });
            me.scrollable.ion({
                overflowChange : 'onPartnerOverflowChange',
                thisObj        : partner
            });

            if (me.isPainted) {
                me.scrollable.addPartner(partner.scrollable, me.isHorizontal ? 'x' : 'y');

                partner.syncPartnerSubGrids();
            }
            else {
                // When initScroll comes round, make sure it syncs with the partner
                me.initScroll = FunctionHelper.createSequence(me.initScroll, () => {
                    me.scrollable.addPartner(partner.scrollable, me.isHorizontal ? 'x' : 'y');
                    partner.syncPartnerSubGrids();
                }, me);
            }
        }
    }

    /**
     * Breaks the link between current Timeline and the passed Timeline
     *
     * - To add a new partner see {@link #function-addPartner} method.
     * - To get the list of partners see {@link #property-partners} getter.
     *
     * @param {Scheduler.view.TimelineBase} otherTimeline The timeline to unlink from
     */
    removePartner(partner) {
        const
            me                = this,
            { partneredWith } = me;

        if (me.isPartneredWith(partner)) {
            partneredWith.remove(partner);
            me.scrollable.removePartner(partner.scrollable);
            me.un({
                presetchange : 'onPartnerPresetChange',
                thisObj      : partner
            });
            me.scrollable.un({
                overflowChange : 'onPartnerOverflowChange',
                thisObj        : partner
            });

            partner.removePartner(me);
        }
    }

    /**
     * Checks whether the passed timeline is partnered with the current timeline.
     * @param {Scheduler.view.TimelineBase} partner The timeline to check the partnering with
     * @returns {Boolean} Returns `true` if the timelines are partnered
     */
    isPartneredWith(partner) {
        return Boolean(this.partneredWith?.includes(partner));
    }

    /**
     * Called when a partner scheduler changes its overflowing state. The scrollable
     * of a Grid/Scheduler only handles overflowY, so this will mean the addition
     * or removal of a vertical scrollbar.
     *
     * All partners must stay in sync. If another parter has a vertical scrollbar
     * and we do not, we must set our overflowY to 'scroll' so that we show an empty
     * scrollbar to keep widths synchronized.
     * @param {Object} event A {@link Core.helper.util.Scroller#event-overflowChange} event
     * @internal
     */
    onPartnerOverflowChange({ source : otherScrollable, y }) {
        const
            { scrollable } = this,
            ourY           = scrollable.hasOverflow('y');

        // If we disagree with our partner, the partner which doesn't have
        // overflow, has to become overflowY : scroll
        if (ourY !== y) {
            if (ourY) {
                otherScrollable.overflowY = 'scroll';
            }
            else {
                otherScrollable.overflowY = true;
                scrollable.overflowY      = 'scroll';
                this.refreshVirtualScrollbars();
            }
        }
        // If we agree with our partner, we can reset ourselves to overflowY : auto
        else {
            scrollable.overflowY = true;
        }
    }

    onPartnerPresetChange({ preset, startDate, endDate, centerDate, zoomDate, zoomPosition, zoomLevel }) {
        if (!this._viewPresetChanging && this.viewPreset !== preset) {

            // Passed through to the viewPreset changing method
            preset.options  = {
                startDate,
                endDate,
                centerDate,
                zoomDate,
                zoomPosition,
                zoomLevel
            };
            this.viewPreset = preset;
        }
    }

    get partner() {
        return this._partner;
    }

    /**
     * Returns the partnered timelines.
     *
     * - To add a new partner see {@link #function-addPartner} method.
     * - To remove existing partner see {@link #function-removePartner} method.
     *
     * @readonly
     * @member {Scheduler.view.TimelineBase[]} partners
     * @category Time axis
     */
    get partners() {
        const partners = this.partner ? [this.partner] : [];

        if (this.partneredWith) {
            partners.push.apply(partners, this.partneredWith.allValues);
        }

        return partners;
    }

    get timeAxisColumn() {
        return this.columns && this._timeAxisColumn;
    }

    changeColumns(columns, currentStore) {
        const me = this;
        let timeAxisColumnIndex, timeAxisColumnConfig;

        // No columns means destroy
        if (columns) {
            const isArray = Array.isArray(columns);

            let cols = columns;

            if (!isArray) {
                cols = columns.data;
            }

            timeAxisColumnIndex = cols && cols.length;

            cols.some((col, index) => {
                if (col.type === 'timeAxis') {
                    timeAxisColumnIndex  = index;
                    timeAxisColumnConfig = ObjectHelper.assign(col, me.timeAxisColumn);
                    return true;
                }
                return false;
            });


            if (me.isVertical) {
                cols = [
                    ObjectHelper.assign({
                        type : 'verticalTimeAxis'
                    }, me.verticalTimeAxisColumn),
                    // Make space for a regular TimeAxisColumn after the VerticalTimeAxisColumn
                    cols[timeAxisColumnIndex]
                ];

                timeAxisColumnIndex = 1;
            }
            else {
                // We're going to mutate this array which we do not own, so copy it first.
                cols = cols.slice();
            }

            // Fix up the timeAxisColumn config in place
            cols[timeAxisColumnIndex] = this._timeAxisColumn || {
                type    : 'timeAxis',
                cellCls : me.timeCellCls,
                mode    : me.mode,
                ...timeAxisColumnConfig
            };

            // If we are passed a raw array, or the Store we are passed is owned by another
            // Scheduler, pass the raw column data ro the Grid's changeColumns
            if (isArray || (columns.isStore && columns.owner !== this)) {
                columns = cols;
            }
            else {
                columns.data = cols;
            }
        }

        return super.changeColumns(columns, currentStore);
    }

    updateColumns(columns, was) {
        super.updateColumns(columns, was);

        // Extract the known columns by type. Sorting will have placed them into visual order.
        if (columns) {
            const
                me             = this,
                timeAxisColumn = me._timeAxisColumn = me.columns.find(c => c.isTimeAxisColumn);

            if (me.isVertical) {
                me.verticalTimeAxisColumn = me.columns.find(c => c.isVerticalTimeAxisColumn);
                me.verticalTimeAxisColumn.relayAll(me);
            }

            // Set up event relaying early
            timeAxisColumn.relayAll(me);
        }
    }

    onColumnsChanged({ action, changes, record : column, records }) {
        const { timeAxisColumn, columns } = this;
        // If someone replaces the column set (syncing leads to batch), ensure time axis is always added
        if ((action === 'dataset' || action === 'batch') && !columns.includes(timeAxisColumn)) {
            columns.add(timeAxisColumn, true);
        }

        else if (column === timeAxisColumn && 'width' in changes) {
            this.updateCanvasSize();
        }

        column && this.partneredWith?.forEach(partner => {
            const partnerColumn = partner.columns.getAt(column.allIndex);

            if (partnerColumn?.shouldSync(column)) {
                const partnerChanges = {};
                for (const k in changes) {
                    partnerChanges[k] = changes[k].value;
                }
                partnerColumn.set(partnerChanges);
            }
        });

        super.onColumnsChanged(...arguments);
    }

    get timeView() {
        const me = this;
        // Maintainer, we need to ensure that the columns property is initialized
        // if this getter is called at configuration time before columns have been ingested.
        return me.columns && me.isVertical
            ? me.verticalTimeAxisColumn?.view
            : me.timeAxisColumn?.timeAxisView;
    }

    updateEventCls(eventCls) {
        const me = this;

        if (!me.eventSelector) {
            // No difference with new rendering, released have 'b-released' only
            me.unreleasedEventSelector = me.eventSelector = `.${eventCls}-wrap`;
        }
        if (!me.eventInnerSelector) {
            me.eventInnerSelector = `.${eventCls}`;
        }
    }

    set timeAxisViewModel(timeAxisViewModel) {
        const
            me            = this,
            currentModel  = me._timeAxisViewModel,
            tavmListeners = {
                name    : 'timeAxisViewModel',
                update  : 'onTimeAxisViewModelUpdate',
                prio    : 100,
                thisObj : me
            };

        if ((me.partner && !timeAxisViewModel) || (currentModel && currentModel === timeAxisViewModel)) {
            return;
        }

        if (currentModel?.owner === me) {
            // We created this model, destroy it
            currentModel.destroy();
        }

        me.detachListeners('timeAxisViewModel');

        // Getting rid of instanceof check to allow using code from different bundles
        if (timeAxisViewModel?.isTimeAxisViewModel) {
            timeAxisViewModel.ion(tavmListeners);
        }
        else {
            timeAxisViewModel = TimeAxisViewModel.new({
                mode              : me._mode,
                snap              : me.snap,
                forceFit          : me.forceFit,
                timeAxis          : me.timeAxis,
                suppressFit       : me.suppressFit,
                internalListeners : tavmListeners,
                owner             : me
            }, timeAxisViewModel);
        }

        // Replace in dependent classes relying on the model
        if (!me.isConfiguring) {
            if (me.isHorizontal) {
                me.timeAxisColumn.timeAxisViewModel = timeAxisViewModel;
            }
            else {
                me.verticalTimeAxisColumn.view.model = timeAxisViewModel;
            }
        }

        me._timeAxisViewModel = timeAxisViewModel;

        me.relayEvents(timeAxisViewModel, ['update'], 'timeAxisViewModel');

        if (currentModel && timeAxisViewModel) {
            me.trigger('timeAxisViewModelChange', { timeAxisViewModel });
        }
    }

    /**
     * The internal view model, describing the visual representation of the time axis.
     * @property {Scheduler.view.model.TimeAxisViewModel}
     * @readonly
     * @category Time axis
     */
    get timeAxisViewModel() {
        if (!this._timeAxisViewModel) {
            this.timeAxisViewModel = null;
        }
        return this._timeAxisViewModel;
    }

    get suppressFit() {
        return this._timeAxisViewModel?.suppressFit ?? this._suppressFit;
    }

    set suppressFit(value) {
        if (this._timeAxisViewModel) {
            this.timeAxisViewModel.suppressFit = value;
        }
        else {
            this._suppressFit = value;
        }
    }

    set timeAxis(timeAxis) {
        const
            me                = this,
            currentTimeAxis   = me._timeAxis,
            timeAxisListeners = {
                name        : 'timeAxis',
                reconfigure : 'onTimeAxisReconfigure',
                thisObj     : me
            };

        if (me.partner && !timeAxis || (currentTimeAxis && currentTimeAxis === timeAxis)) {
            return;
        }

        if (currentTimeAxis) {
            if (currentTimeAxis.owner === me) {
                // We created this model, destroy it
                currentTimeAxis.destroy();
            }
        }

        me.detachListeners('timeAxis');

        // Getting rid of instanceof check to allow using code from different bundles
        if (!timeAxis?.isTimeAxis) {
            timeAxis = ObjectHelper.assign({
                owner          : me,
                viewPreset     : me.viewPreset,
                autoAdjust     : me.autoAdjustTimeAxis,
                weekStartDay   : me.weekStartDay,
                forceFullTicks : me.fillTicks && me.snap
            }, timeAxis);

            if (me.startDate) {
                timeAxis.startDate = me.startDate;
            }
            if (me.endDate) {
                timeAxis.endDate = me.endDate;
            }

            if (me.workingTime) {
                me.applyWorkingTime(timeAxis);
            }

            timeAxis = new TimeAxis(timeAxis);
        }

        // Inform about reconfiguring the timeaxis, to allow users to react to start & end date changes
        timeAxis.ion(timeAxisListeners);

        me._timeAxis = timeAxis;
    }

    onTimeAxisReconfigure({ config, oldConfig }) {
        if (config) {
            const dateRangeChange = !oldConfig || (oldConfig.startDate - config.startDate || oldConfig.endDate - config.endDate);

            if (dateRangeChange) {
                /**
                 * Fired when the range of dates encapsulated by the UI changes. This will be when
                 * moving a view in time by reconfiguring its {@link #config-timeAxis}. This will happen
                 * when zooming, or changing {@link #config-viewPreset}.
                 *
                 * Contrast this with the {@link #event-visibleDateRangeChange} event which fires much
                 * more frequently, during scrolling along the time axis and changing the __visible__
                 * date range.
                 * @event dateRangeChange
                 * @param {Scheduler.view.TimelineBase} source This Scheduler/Gantt instance.
                 * @param {Object} old The old date range
                 * @param {Date} old.startDate the old start date.
                 * @param {Date} old.endDate the old end date.
                 * @param {Object} new The new date range
                 * @param {Date} new.startDate the new start date.
                 * @param {Date} new.endDate the new end date.
                 */
                this.trigger('dateRangeChange', {
                    old : {
                        startDate : oldConfig.startDate,
                        endDate   : oldConfig.endDate
                    },
                    new : {
                        startDate : config.startDate,
                        endDate   : config.endDate
                    }
                });
            }
        }

        /**
         * Fired when the timeaxis has changed, for example by zooming or configuring a new time span.
         * @event timeAxisChange
         * @param {Scheduler.view.Scheduler} source - This Scheduler
         * @param {Object} config Config object used to reconfigure the time axis.
         * @param {Date} config.startDate New start date (if supplied)
         * @param {Date} config.endDate New end date (if supplied)
         */
        this.trigger('timeAxisChange', { config });
    }

    get timeAxis() {
        if (!this._timeAxis) {
            this.timeAxis = null;
        }
        return this._timeAxis;
    }

    updateForceFit(value) {
        if (this._timeAxisViewModel) {
            this._timeAxisViewModel.forceFit = value;
        }
    }

    /**
     * Get/set working time. Assign `null` to stop using working time. See {@link #config-workingTime} config for details.
     * @property {Object}
     * @category Scheduled events
     */
    set workingTime(config) {
        this._workingTime = config;

        if (!this.isConfiguring) {
            this.applyWorkingTime(this.timeAxis);
        }
    }

    get workingTime() {
        return this._workingTime;
    }

    // Translates the workingTime configs into TimeAxis#include rules, applies them and then refreshes the header and
    // redraws the events
    applyWorkingTime(timeAxis) {
        const me     = this,
            config = me._workingTime;

        if (config) {
            let hour = null;
            // Only use valid values
            if (config.fromHour >= 0 && config.fromHour < 24 && config.toHour > config.fromHour && config.toHour <= 24 && config.toHour - config.fromHour < 24) {
                hour = { from : config.fromHour, to : config.toHour };
            }

            let day = null;
            // Only use valid values
            if (config.fromDay >= 0 && config.fromDay < 7 && config.toDay > config.fromDay && config.toDay <= 7 && config.toDay - config.fromDay < 7) {
                day = { from : config.fromDay, to : config.toDay };
            }

            if (hour || day) {
                timeAxis.include = {
                    hour,
                    day
                };
            }
            else {
                // No valid rules, restore timeAxis
                timeAxis.include = null;
            }
        }
        else {
            // No rules, restore timeAxis
            timeAxis.include = null;
        }

        if (me.isPainted) {
            // Refreshing header, which also recalculate tickSize and header data
            me.timeAxisColumn.refreshHeader();
            // Update column lines
            me.features.columnLines?.refresh();

            // Animate event changes
            me.refreshWithTransition();
        }
    }

    updateStartDate(date) {
        this.setStartDate(date);
    }

    /**
     * Sets the timeline start date.
     *
     * **Note:**
     * - If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
     * - If keepDuration is false and new start date is greater than end date, it will throw an exception.
     *
     * @param {Date} date The new start date
     * @param {Boolean} keepDuration Pass `true` to keep the duration of the timeline ("move" the timeline),
     * `false` to change the duration ("resize" the timeline). Defaults to `true`.
     */
    setStartDate(date, keepDuration = true) {
        const
            me = this,
            ta = me._timeAxis,
            {
                startDate,
                endDate,
                mainUnit
            }  = ta || emptyObject;

        if (typeof date === 'string') {
            date = DateHelper.parse(date);
        }

        if (ta && endDate) {
            if (date) {
                let calcEndDate = endDate;

                if (keepDuration && startDate) {
                    const diff  = DateHelper.diff(startDate, endDate, mainUnit, true);
                    calcEndDate = DateHelper.add(date, diff, mainUnit);
                }

                me.setTimeSpan(date, calcEndDate);
            }
        }
        else {
            me._tempStartDate = date;
        }
    }

    get startDate() {
        let ret = this._timeAxis?.startDate || this._tempStartDate;

        if (!ret) {
            ret = new Date();

            const { workingTime } = this;
            if (workingTime) {
                while (!isWorkingTime(ret, workingTime)) {
                    ret.setHours(ret.getHours() + 1);
                }
            }

            this._tempStartDate = ret;
        }

        return ret;
    }

    changeEndDate(date) {
        if (typeof date === 'string') {
            date = DateHelper.parse(date);
        }
        this.setEndDate(date);
    }

    /**
     * Sets the timeline end date
     *
     * **Note:**
     * - If you need to set start and end date at the same time, use the {@link #function-setTimeSpan} method.
     * - If keepDuration is false and new end date is less than start date, it will throw an exception.
     *
     * @param {Date} date The new end date
     * @param {Boolean} keepDuration Pass `true` to keep the duration of the timeline ("move" the timeline),
     * `false` to change the duration ("resize" the timeline). Defaults to `false`.
     */
    setEndDate(date, keepDuration = false) {
        const
            me = this,
            ta = me._timeAxis,
            {
                startDate,
                endDate,
                mainUnit
            }  = ta || emptyObject;

        if (typeof date === 'string') {
            date = DateHelper.parse(date);
        }

        if (ta && startDate) {
            if (date) {
                let calcStartDate = startDate;

                if (keepDuration && endDate) {
                    const diff    = DateHelper.diff(startDate, endDate, mainUnit, true);
                    calcStartDate = DateHelper.add(date, -diff, mainUnit);
                }

                me.setTimeSpan(calcStartDate, date);
            }
        }
        else {
            me._tempEndDate = date;
        }
    }

    get endDate() {
        const me = this;

        if (me._timeAxis) {
            return me._timeAxis.endDate;
        }

        return me._tempEndDate || DateHelper.add(me.startDate, me.viewPreset.defaultSpan, me.viewPreset.mainHeader.unit);
    }

    changeVisibleDate(options) {
        if (options instanceof Date) {
            return { date : options, block : 'nearest' };
        }
        if (options instanceof Object) {
            return {
                date : options.date,
                ...options
            };
        }
    }

    updateVisibleDate(options) {
        const me = this;

        // Infinite scroll initialization takes care of its visibleDate after
        // calculating the optimum scroll range in TimelineScroll#initScroll
        if (!(me.infiniteScroll && me.isConfiguring)) {
            if (me.isPainted) {
                me.scrollToDate(options.date, options);
            }
            else {
                me.ion({
                    paint : () => me.scrollToDate(options.date, options),
                    once  : true
                });
            }
        }
    }

    get features() {
        return super.features;
    }

    // add region resize by default
    set features(features) {
        features = features === true ? {} : features;

        if (!('regionResize' in features)) {
            features.regionResize = true;
        }

        super.features = features;
    }

    //endregion

    //region Event handlers

    onLocaleChange() {
        super.onLocaleChange();

        const oldAutoAdjust = this.timeAxis.autoAdjust;
        // Time axis should rebuild as weekStartDay may have changed
        this.timeAxis.reconfigure({
            autoAdjust : false
        });

        // Silently set it back to what the user had for next view refresh
        this.timeAxis.autoAdjust = oldAutoAdjust;
    }

    /**
     * Called when the element which encapsulates the Scheduler's visible height changes size.
     * We only respond to *height* changes here. The TimeAxisSubGrid monitors its own width.
     * @param {HTMLElement} element
     * @param {DOMRect} oldRect
     * @param {DOMRect} newRect
     * @private
     */
    onBodyResize(element, oldRect, { width, height }) {
        // Uncache old value upon element resize, not upon initial sizing
        if (this.isVertical && oldRect && width !== oldRect.width) {
            delete this.timeAxisSubGrid._width;
        }

        const newWidth = this.timeAxisSubGrid.element.offsetWidth;

        // The Scheduler (The Grid) dictates the viewport height.
        // Don't react on first invocation which will be initial size.
        if (this._bodyRectangle && oldRect && (height !== oldRect.height)) {
            this.onSchedulerViewportResize(newWidth, height, newWidth, oldRect.height);
        }
    }

    onSchedulerViewportResize(width, height, oldWidth, oldHeight) {
        if (this.isPainted) {
            const
                me = this,
                {
                    isHorizontal,
                    partneredWith
                }  = me;

            me.currentOrientation.onViewportResize(width, height, oldWidth, oldHeight);

            // Raw width is always correct for horizontal layout because the TimeAxisSubGrid
            // never shows a scrollbar. It's always contained by an owning Grid which shows
            // the vertical scrollbar.
            me.updateViewModelAvailableSpace(isHorizontal ? width : Math.floor(height));

            if (partneredWith && !me.isSyncingFromPartner) {
                me.syncPartnerSubGrids();
            }

            /**
             * Fired when the *scheduler* viewport (not the overall Scheduler element) changes size.
             * This happens when the grid changes height, or when the subgrid which encapsulates the
             * scheduler column changes width.
             * @event timelineViewportResize
             * @param {Core.widget.Widget} source - This Scheduler
             * @param {Number} width The new width
             * @param {Number} height The new height
             * @param {Number} oldWidth The old width
             * @param {Number} oldHeight The old height
             */
            me.trigger('timelineViewportResize', { width, height, oldWidth, oldHeight });
        }
    }

    updateViewModelAvailableSpace(space) {
        this.timeAxisViewModel.availableSpace = space;
    }

    onTimeAxisViewModelUpdate() {
        if (!this._viewPresetChanging && !this.timeAxisSubGrid.collapsed) {
            this.updateCanvasSize();
            this.currentOrientation.onTimeAxisViewModelUpdate();
        }
    }

    syncPartnerSubGrids() {
        this.partneredWith.forEach(partner => {
            if (!partner.isSyncingFromPartner) {
                partner.isSyncingFromPartner = true;
                this.eachSubGrid(subGrid => {
                    const partnerSubGrid = partner.subGrids[subGrid.region];

                    // If there is a difference, sync the partner SubGrid state
                    if (partnerSubGrid.width !== subGrid.width) {
                        if (subGrid.collapsed) {
                            partnerSubGrid.collapse();
                        }
                        else {
                            if (partnerSubGrid.collapsed) {
                                partnerSubGrid.expand();
                            }
                            // When using flexed subgrid, make sure flex values has prio over width
                            if (subGrid.flex) {
                                // If flex values match, resize should be fine without changing anything
                                if (subGrid.flex !== partnerSubGrid.flex) {
                                    partnerSubGrid.flex = subGrid.flex;
                                }
                            }
                            else {
                                partnerSubGrid.width = subGrid.width;
                            }
                        }
                    }
                });
                partner.isSyncingFromPartner = false;
            }
        });
    }

    //endregion

    //region Mode

    get currentOrientation() {
        throw new Error('Implement in subclass');
    }

    // Horizontal is the default, overridden in scheduler
    get isHorizontal() {
        return true;
    }

    //endregion

    //region Canvases and elements

    get backgroundCanvas() {
        return this._backgroundCanvas;
    }

    get foregroundCanvas() {
        return this._foregroundCanvas;
    }

    get svgCanvas() {
        const me = this;
        if (!me._svgCanvas) {
            const svg = me._svgCanvas = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
            svg.setAttribute('id', IdHelper.generateId('svg'));
            // To not be recycled by DomSync
            svg.retainElement = true;
            me.foregroundCanvas.appendChild(svg);
            me.trigger('svgCanvasCreated', { svg });
        }
        return me._svgCanvas;
    }

    /**
     * Returns the subGrid containing the time axis
     * @member {Grid.view.SubGrid} timeAxisSubGrid
     * @readonly
     * @category Time axis
     */

    /**
     * Returns the html element for the subGrid containing the time axis
     * @property {HTMLElement}
     * @readonly
     * @category Time axis
     */
    get timeAxisSubGridElement() {
        // Hit a lot, caching the element (it will never change)

        if (!this._timeAxisSubGridElement) {
            // We need the TimeAxisSubGrid to exist, so regions must be initialized
            this.getConfig('regions');

            this._timeAxisSubGridElement = this.timeAxisColumn?.subGridElement;
        }

        return this._timeAxisSubGridElement;
    }

    updateCanvasSize() {
        const
            me            = this,
            { totalSize } = me.timeAxisViewModel,
            width         = me.isHorizontal ? totalSize : me.timeAxisColumn.width;

        let result = false;

        if (me.isVertical) {
            // Ensure vertical scroll range accommodates the TimeAxis
            if (me.isPainted) {
                // We used to have a bug here from not including the row border in the total height. Border is now
                // removed, but leaving code here just in case some client is using border
                me.refreshTotalHeight(totalSize + me._rowBorderHeight, true);
            }

            // Canvas might need a height in vertical mode, if ticks does not fill height (suppressFit : true)
            if (me.suppressFit) {
                DomHelper.setLength(me.foregroundCanvas, 'height', totalSize);
            }

            result = true;
        }

        if (width !== me.$canvasWidth && me.foregroundCanvas) {
            if (me.backgroundCanvas) {
                DomHelper.setLength(me.backgroundCanvas, 'width', width);
            }

            DomHelper.setLength(me.foregroundCanvas, 'width', width);

            me.$canvasWidth = width;

            result = true;
        }

        return result;
    }

    /**
     * A chainable function which Features may hook to add their own content to the timeaxis header.
     * @param {Array} configs An array of domConfigs, append to it to have the config applied to the header
     */
    getHeaderDomConfigs(configs) {}

    /**
     * A chainable function which Features may hook to add their own content to the foreground canvas
     * @param {Array} configs An array of domConfigs, append to it to have the config applied to the foreground canvas
     */
    getForegroundDomConfigs(configs) {}

    //endregion

    //region Grid overrides

    async onStoreDataChange({ action }) {
        const me = this;

        // Only update the UI immediately if we are visible
        if (me.isVisible) {
            // When repopulating stores (pro and up on data reload), the engine is not in a valid state until committed.
            // Don't want to commit here, since it might be repopulating multiple stores.
            // Instead delay grids refresh until project is ready
            if (action === 'dataset' && me.project?.isRepopulatingStores) {
                await me.project.await('refresh', false);
            }

            super.onStoreDataChange(...arguments);
        }
        // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
        else {
            me.whenVisible('refresh', me, [true]);
        }
    }

    refresh(forceLayout = true) {
        const me = this;

        if (me.isPainted && !me.refreshSuspended) {
            // We need to refresh if there are Features laying claim to the visible time axis.
            // Or there are events which fall inside the time axis.
            // Or (if no events fall inside the time axis) there are event elements to remove.
            if (me.isVertical || me.hasVisibleEvents || me.timeAxisSubGridElement.querySelector(me.eventSelector)) {
                if (!me.project || me.isEngineReady) {
                    me.refreshRows(false, forceLayout);
                }
                else {
                    me.refreshAfterProjectRefresh             = true;
                    me.currentOrientation.refreshAllWhenReady = true;
                }
            }
            // Even if there are no events in our timeline, Features
            // assume there will be a refresh event from the RowManager
            // after a refresh request so fire it here.
            else {
                me.rowManager.trigger('refresh');
            }
        }
    }

    render() {
        const
            me          = this,
            schedulerEl = me.timeAxisSubGridElement;

        if (me.useBackgroundCanvas) {
            me._backgroundCanvas = DomHelper.createElement({
                className   : 'b-sch-background-canvas',
                parent      : schedulerEl,
                nextSibling : schedulerEl.firstElementChild
            });
        }

        // The font-size trick is no longer used by scheduler, since it allows per resource margins
        const fgCanvas = me._foregroundCanvas = DomHelper.createElement({
            className : 'b-sch-foreground-canvas',
            style     : `font-size:${(me.rowHeight - me.resourceMargin * 2)}px`,
            parent    : schedulerEl
        });

        me.timeAxisSubGrid.insertRowsBefore = fgCanvas;

        // Size correctly in case ticks does not fill height
        if (me.isVertical && me.suppressFit) {
            me.updateCanvasSize();
        }

        super.render(...arguments);
    }


    refreshRows(returnToTop = false, reLayoutEvents = true) {
        const me = this;

        if (me.isConfiguring) {
            return;
        }

        me.currentOrientation.refreshRows(reLayoutEvents);

        super.refreshRows(returnToTop);
    }

    updateHideHeaders(hide) {
        const
            me         = this,
            scrollLeft = me.isPainted ? me.scrollLeft : 0;

        super.updateHideHeaders(hide);

        if (me.isPainted) {
            if (!hide) {
                me.timeAxisColumn.refreshHeader(null, true);
            }

            me.nextAnimationFrame().then(() => me.scrollLeft = scrollLeft);
        }
    }

    getCellDataFromEvent(event, includeSingleAxisMatch) {
        if (includeSingleAxisMatch) {
            includeSingleAxisMatch = !Boolean(event.target.closest('.b-sch-foreground-canvas'));
        }
        return super.getCellDataFromEvent(event, includeSingleAxisMatch);
    }

    // This GridSelection override disables drag-selection in timeaxis column for scheduler and gantt
    onCellNavigate(me, from, to) {
        if (to.cell?.classList.contains('b-timeaxis-cell') && !GlobalEvents.currentMouseDown?.target.classList.contains('b-grid-cell')) {
            this.preventDragSelect = true;
        }
        super.onCellNavigate(...arguments);
    }

    //endregion

    //region Other

    // duration = false prevents transition
    runWithTransition(fn, duration) {
        const me = this;

        // Do not attempt to enter animating state if we are not visible
        if (me.isVisible) {
            // Allow calling with true/false to keep code simpler in other places
            if (duration == null || duration === true) {
                duration = me.transitionDuration;
            }

            // Ask Grid superclass to enter the animated state if requested and enabled.
            if (duration && me.enableEventAnimations) {
                if (!me.hasTimeout('exitTransition')) {
                    me.isAnimating = true;
                }

                // Exit animating state in duration milliseconds.
                exitTransition.delay = duration;
                me.setTimeout(exitTransition);
            }
        }

        fn();
    }

    exitTransition() {
        this.isAnimating = false;
        this.trigger('transitionend');
    }

    // Awaited by CellEdit to make sure that the editor is not moved until row heights have transitioned, to avoid it
    // ending up misaligned
    async waitForAnimations() {
        // If project is calculating, we should await that too. It might lead to transitions
        if (!this.isEngineReady && this.project) {
            await this.project.await('dataReady', false);
        }

        await super.waitForAnimations();
    }

    /**
     * Refreshes the grid with transitions enabled.
     */
    refreshWithTransition(forceLayout, duration) {
        const me = this;

        // No point in starting a transition if we cant refresh anyway
        if (!me.refreshSuspended && me.isPainted) {
            // Since we suspend refresh when loading with CrudManager, rows might not have been initialized yet
            if (!me.rowManager.topRow) {
                me.rowManager.reinitialize();
            }
            else {
                me.runWithTransition(() => me.refresh(forceLayout), duration);
            }
        }
    }

    /**
     * Returns an object representing the visible date range
     * @property {Object}
     * @property {Date} visibleDateRange.startDate
     * @property {Date} visibleDateRange.endDate
     * @readonly
     * @category Dates
     */
    get visibleDateRange() {
        return this.currentOrientation.visibleDateRange;
    }

    // This override will force row selection on timeaxis column selection, effectively disabling cell selection there
    isRowNumberSelecting(...selectors) {
        return super.isRowNumberSelecting(...selectors) ||
            selectors.some(cs => cs.column ? cs.column.isTimeAxisColumn : cs.cell?.closest('.b-timeaxis-cell'));
    }

    //endregion

    /**
     * Returns a rounded duration value to be displayed in UI (tooltips, labels etc)
     * @param {Number} duration The raw duration value
     * @param {Number} [nbrDecimals] The number of decimals, defaults to {@link #config-durationDisplayPrecision}
     * @returns {Number} The rounded duration
     */
    formatDuration(duration, nbrDecimals = this.durationDisplayPrecision) {
        const multiplier = Math.pow(10, nbrDecimals);

        return Math.round(duration * multiplier) / multiplier;
    }

    beginListeningForBatchedUpdates() {
        this.listenToBatchedUpdates = (this.listenToBatchedUpdates || 0) + 1;

        // Allow live resizing (etc) in all splits
        this.syncSplits?.(other => other.beginListeningForBatchedUpdates());
    }

    endListeningForBatchedUpdates() {
        if (this.listenToBatchedUpdates) {
            this.listenToBatchedUpdates -= 1;
        }

        this.syncSplits?.(other => other.endListeningForBatchedUpdates());
    }

    onConnectedCallback(connected, initialConnect) {
        if (connected && !initialConnect) {
            this.timeAxisSubGrid.scrollable.x += 0.5;
        }
    }

    updateRtl(rtl) {
        const
            me                = this,
            { isConfiguring } = me;

        let visibleDateRange;

        if (!isConfiguring) {
            visibleDateRange = me.visibleDateRange;
        }

        super.updateRtl(rtl);

        if (!isConfiguring) {
            me.currentOrientation.clearAll();
            if (me.infiniteScroll) {
                me.shiftToDate(visibleDateRange.startDate);
                me.scrollToDate(visibleDateRange.startDate, { block : 'start' });
            }
            else {
                me.timelineScroller.position += 0.5;
            }
        }
    }

    /**
     * Applies the start and end date to each event store request (formatted in the same way as the start date field,
     * defined in the EventStore Model class).
     * @category Data
     * @private
     */
    applyStartEndParameters(params) {
        const
            me    = this,
            field = me.eventStore.modelClass.fieldMap.startDate;

        if (me.passStartEndParameters) {
            params[me.startParamName] = field.print(me.startDate);
            params[me.endParamName] = field.print(me.endDate);
        }
    }
}

// Register this widget type with its Factory
TimelineBase.initClass();

// Has to be here because Gantt extends TimelineBase
VersionHelper.setVersion('scheduler', '5.5.0');
