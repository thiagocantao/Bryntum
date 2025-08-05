import Base from '../../../Core/Base.js';
import Rectangle from '../../../Core/helper/util/Rectangle.js';
import DateHelper from '../../../Core/helper/DateHelper.js';
import Scroller from '../../../Core/helper/util/Scroller.js';
import DomHelper from '../../../Core/helper/DomHelper.js';

/**
 * @module Scheduler/view/mixin/TimelineScroll
 */
const
    maintainVisibleStart = {
        maintainVisibleStart : true
    },
    defaultScrollOptions = {
        block : 'nearest'
    };

/**
 * Functions for scrolling to events, dates etc.
 *
 * @mixin
 */
export default Target => class TimelineScroll extends (Target || Base) {
    static get $name() {
        return 'TimelineScroll';
    }

    static get configurable() {
        return {
            /**
             * This config defines the size of the start and end invisible parts of the timespan when {@link #config-infiniteScroll} set to `true`.
             *
             * It should be provided as a coefficient, which will be multiplied by the size of the scheduling area.
             *
             * For example, if `bufferCoef` is `5` and the panel view width is 200px then the timespan will be calculated to
             * have approximately 1000px (`5 * 200`) to the left and 1000px to the right of the visible area, resulting
             * in 2200px of totally rendered content.
             *
             * @config {Number}
             * @category Infinite scroll
             * @default
             */
            bufferCoef : 5,

            /**
             * This config defines the scroll limit, which, when exceeded will cause a timespan shift.
             * The limit is calculated as the `panelWidth * {@link #config-bufferCoef} * bufferThreshold`. During scrolling, if the left or right side
             * has less than that of the rendered content - a shift is triggered.
             *
             * For example if `bufferCoef` is `5` and the panel view width is 200px and `bufferThreshold` is 0.2, then the timespan
             * will be shifted when the left or right side has less than 200px (5 * 200 * 0.2) of content.
             * @config {Number}
             * @category Infinite scroll
             * @default
             */
            bufferThreshold : 0.2,

            /**
             * Configure as `true` to automatically adjust the panel timespan during scrolling in the time dimension,
             * when the scroller comes close to the start/end edges.
             *
             * The actually rendered timespan in this mode (and thus the amount of HTML in the DOM) is calculated based
             * on the {@link #config-bufferCoef} option, and is thus not controlled by the {@link Scheduler/view/TimelineBase#config-startDate}
             * and {@link Scheduler/view/TimelineBase#config-endDate} configs. The moment when the timespan shift
             * happens is determined by the {@link #config-bufferThreshold} value.
             *
             * To specify initial point in time to view, supply the
             * {@link Scheduler/view/TimelineBase#config-visibleDate} config.
             *
             * @config {Boolean} infiniteScroll
             * @category Infinite scroll
             * @default
             */
            infiniteScroll : false
        };
    }

    initScroll() {
        const
            me = this,
            {
                isHorizontal,
                visibleDate
            }  = me;

        super.initScroll();

        const { scrollable } = isHorizontal ? me.timeAxisSubGrid : me;

        scrollable.ion({
            scroll  : 'onTimelineScroll',
            thisObj : me
        });

        // Ensure the TimeAxis starts life at the correct size with buffer zones
        // outside the visible window.
        if (me.infiniteScroll) {
            const
                setTimeSpanOptions     = visibleDate ? { ...visibleDate, visibleDate : visibleDate.date } : { visibleDate : me.viewportCenterDate, block : 'center' },
                { startDate, endDate } = me.timeAxisViewModel.calculateInfiniteScrollingDateRange(setTimeSpanOptions.visibleDate, setTimeSpanOptions.block === 'center');

            // Don't ask to maintain visible start - we're initializing - there's no visible start yet.
            // If there's a visibleDate set, it will execute its scroll on paint.
            me.setTimeSpan(
                startDate,
                endDate,
                setTimeSpanOptions
            );
        }
    }

    /**
     * A {@link Core.helper.util.Scroller} which scrolls the time axis in whatever {@link Scheduler.view.Scheduler#config-mode} the
     * Scheduler is configured, either `horiontal` or `vertical`.
     *
     * The width and height dimensions are replaced by `size`. So this will expose the following properties:
     *
     *    - `clientSize` The size of the time axis viewport.
     *    - `scrollSize` The full scroll size of the time axis viewport
     *    - `position` The position scrolled to along the time axis viewport
     *
     * @property {Core.helper.util.Scroller}
     * @readonly
     * @category Scrolling
     */
    get timelineScroller() {
        const me = this;

        if (!me.scrollInitialized) {
            me.initScroll();
        }
        return me._timelineScroller || (me._timelineScroller = new TimelineScroller({
            widget       : me,
            scrollable   : me.isHorizontal ? me.timeAxisSubGrid.scrollable : me.scrollable,
            isHorizontal : me.isHorizontal
        }));
    }

    doDestroy() {
        this._timelineScroller?.destroy();

        super.doDestroy();
    }

    onTimelineScroll({ source }) {
        // On scroll, check if we are nearing the end to see if the sliding window needs moving.
        // onSchedulerHorizontalScroll is delayed to animationFrame
        if (this.infiniteScroll) {
            this.checkTimeAxisScroll(source[this.isHorizontal ? 'x' : 'y']);
        }
    }

    checkTimeAxisScroll(scrollPos) {
        const
            me             = this,
            scrollable     = me.timelineScroller,
            { clientSize } = scrollable,
            requiredSize   = clientSize * me.bufferCoef,
            limit          = requiredSize * me.bufferThreshold,
            maxScroll      = scrollable.maxPosition,
            { style }      = me.timeAxisSubGrid.virtualScrollerElement;

        // if scroll violates limits let's shift timespan
        if ((maxScroll - scrollPos < limit) || scrollPos < limit) {
            // If they were dragging the thumb, this must be a one-time thing. They *must* lose contact
            // with the thumb when the window shift occurs and the thumb zooms back to the center.
            // Changing for a short time to overflow:hidden terminates the thumb drag.
            // They can start again from the center, the reset happens very quickly.
            style.overflow = 'hidden';
            style.pointerEvents = 'none';

            // Avoid content height changing when scrollbar disappears
            style.paddingBottom = `${DomHelper.scrollBarWidth}px`;

            me.setTimeout(() => {
                style.overflow = '';
                style.paddingBottom = '';
                style.pointerEvents = '';
            }, 100);

            me.shiftToDate(me.getDateFromCoordinate(scrollPos, null, true, false, true));
        }
    }

    shiftToDate(date, centered) {
        const newRange = this.timeAxisViewModel.calculateInfiniteScrollingDateRange(date, centered);

        // this will trigger a refresh (`refreshKeepingScroll`) which will perform `restoreScrollState` and sync the scrolling position
        this.setTimeSpan(newRange.startDate, newRange.endDate, maintainVisibleStart);
    }

    // If we change to infinite scrolling dynamically, it should create the buffer zones.
    updateInfiniteScroll(infiniteScroll) {
        // At construction time, this is handled in initScroll.
        // This is just here to handle dynamic updates.
        if (!this.isConfiguring && infiniteScroll) {
            this.checkTimeAxisScroll(this.timelineScroller.position);
        }
    }

    //region Scroll to date

    /**
     * Scrolls the timeline "tick" encapsulating the passed `Date` into view according to the passed options.
     * @param {Date} date The date to which to scroll the timeline
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @category Scrolling
     */
    async scrollToDate(date, options = {}) {
        const
            me               = this,
            {
                timeAxis,
                visibleDateRange,
                infiniteScroll
            }              = me,
            {
                unit,
                increment
            }              = timeAxis,
            edgeOffset     = options.edgeOffset || 0,
            visibleWidth   = DateHelper.ceil(visibleDateRange.endDate, increment + ' ' + unit) - DateHelper.floor(visibleDateRange.startDate, increment + ' ' + unit),
            direction      = date > me.viewportCenterDate ? 1 : -1,
            extraScroll    = (infiniteScroll ? visibleWidth * me.bufferCoef * me.bufferThreshold : (options.block === 'center' ? visibleWidth / 2 : (edgeOffset ? me.getMilliSecondsPerPixelForZoomLevel(me.viewPreset) * edgeOffset : 0))) * direction,
            visibleDate    = new Date(date.getTime() + extraScroll),
            shiftDirection = visibleDate > timeAxis.endDate ? 1 : visibleDate < timeAxis.startDate ? -1 : 0;

        // Required visible date outside TimeAxis and infinite scrolling, that has opinions about how
        // much scroll range has to be created after the target date.
        if (shiftDirection && me.infiniteScroll) {
            me.shiftToDate(new Date(date - extraScroll), null, true);
            // shift to date could trigger a native browser async scroll out of our control. If a scroll
            // happens during scrollToCoordinate, the scrolling is cancelled so we wait a bit here
            await me.nextAnimationFrame();
        }

        const
            scrollerViewport = me.timelineScroller.viewport,
            localCoordinate  = me.getCoordinateFromDate(date, true),
            // Available space can be less than tick size (Export.t.js in Gantt)
            width            = Math.min(me.timeAxisViewModel.tickSize, me.timeAxisViewModel.availableSpace),
            target           = me.isHorizontal
                // In RTL coordinate is for the right edge of the tick, so we need to subtract width
                ? new Rectangle(me.getCoordinateFromDate(date, false) - (me.rtl ? width : 0), scrollerViewport.y, width, scrollerViewport.height)
                : new Rectangle(scrollerViewport.x, me.getCoordinateFromDate(date, false), scrollerViewport.width, me.timeAxisViewModel.tickSize);

        await me.scrollToCoordinate(localCoordinate, target, date, options);
    }

    /**
     * Scrolls to current time.
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @category Scrolling
     */
    scrollToNow(options = {}) {
        return this.scrollToDate(new Date(), options);
    }

    /**
     * Used by {@link #function-scrollToDate} to scroll to correct coordinate.
     * @param {Number} localCoordinate Coordinate to scroll to
     * @param {Date} date Date to scroll to, used for reconfiguring the time axis
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @private
     * @category Scrolling
     */
    async scrollToCoordinate(localCoordinate, target, date, options = {}) {
        const me = this;

        // Not currently have this date in a timeaxis. Ignore negative scroll in weekview, it can be just 'filtered' with
        // startTime/endTime config
        if (localCoordinate < 0) {
            // adjust the timeaxis first
            const
                visibleSpan         = me.endDate - me.startDate,
                { unit, increment } = me.timeAxis,
                newStartDate        = DateHelper.floor(new Date(date.getTime() - (visibleSpan / 2)), increment + ' ' + unit),
                newEndDate          = DateHelper.add(newStartDate, visibleSpan);

            // We're trying to reconfigure time span to current dates, which means we are as close to center as it
            // could be. Do nothing then.
            // covered by 1102_panel_api
            if (newStartDate - me.startDate !== 0 && newEndDate - me.endDate !== 0) {
                me.setTimeSpan(newStartDate, newEndDate);

                return me.scrollToDate(date, options);
            }

            return;
        }

        await me.timelineScroller.scrollIntoView(target, options);

        // Horizontal scroll is triggered on next frame in SubGrid.js, view is not up to date yet. Resolve on next frame
        return !me.isDestroyed && me.nextAnimationFrame();
    }

    //endregion

    //region Relative scrolling
    // These methods are important to users because although they are mixed into the top level Grid/Scheduler,
    // for X scrolling the explicitly target the SubGrid that holds the scheduler.

    /**
     * Get/set the `scrollLeft` value of the SubGrid that holds the scheduler.
     *
     * This may be __negative__ when the writing direction is right-to-left.
     * @property {Number}
     * @category Scrolling
     */
    set scrollLeft(left) {
        this.timeAxisSubGrid.scrollable.element.scrollLeft = left;
    }

    get scrollLeft() {
        return this.timeAxisSubGrid.scrollable.element.scrollLeft;
    }

    /**
     * Get/set the writing direction agnostic horizontal scroll position.
     *
     * This is always the __positive__ offset from the scroll origin whatever the writing
     * direction in use.
     *
     * Applies to the SubGrid that holds the scheduler
     * @property {Number}
     * @category Scrolling
     */
    set scrollX(x) {
        this.timeAxisSubGrid.scrollable.x = x;
    }

    get scrollX() {
        return this.timeAxisSubGrid.scrollable.x;
    }

    /**
     * Get/set vertical scroll
     * @property {Number}
     * @category Scrolling
     */
    set scrollTop(top) {
        this.scrollable.y = top;
    }

    get scrollTop() {
        return this.scrollable.y;
    }

    /**
     * Horizontal scrolling. Applies to the SubGrid that holds the scheduler
     * @param {Number} x
     * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
     * @returns {Promise} A promise which is resolved when the scrolling has finished.
     * @category Scrolling
     */
    scrollHorizontallyTo(coordinate, options = true) {
        return this.timeAxisSubGrid.scrollable.scrollTo(coordinate, null, options);
    }

    /**
     * Vertical scrolling
     * @param {Number} y
     * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
     * @returns {Promise} A promise which is resolved when the scrolling has finished.
     * @category Scrolling
     */
    scrollVerticallyTo(y, options = true) {
        return this.scrollable.scrollTo(null, y, options);
    }

    /**
     * Scrolls the subgrid that contains the scheduler
     * @param {Number} x
     * @param {ScrollOptions|Boolean} [options] How to scroll. May be passed as `true` to animate.
     * @returns {Promise} A promise which is resolved when the scrolling has finished.
     * @category Scrolling
     */
    scrollTo(x, options = true) {
        return this.timeAxisSubGrid.scrollable.scrollTo(x, null, options);
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};

// Internal class used to interrogate and manipulate the timeline scroll position.
// This delegates all operations to the appropriate Scroller, horizontal or vertical.
class TimelineScroller extends Scroller {
    static get configurable() {
        return {
            position : null,
            x        : null,
            y        : null
        };
    }

    // This class is passive about configuring the element.
    // It has no opinions about *how* the overflow is handled.
    updateOverflowX() {}
    updateOverflowY() {}

    onScroll(e) {
        super.onScroll(e);
        this._position = null;
    }

    syncPartners(force) {
        this.scrollable.syncPartners(force);
    }

    updatePosition(position) {
        this.scrollable[this.isHorizontal ? 'x' : 'y'] = position;
    }

    get viewport() {
        return this.scrollable.viewport;
    }

    get position() {
        return this._position = this.scrollable[this.isHorizontal ? 'x' : 'y'];
    }

    get clientSize() {
        return this.scrollable[`client${this.isHorizontal ? 'Width' : 'Height'}`];
    }

    get scrollSize() {
        return this.scrollable[`scroll${this.isHorizontal ? 'Width' : 'Height'}`];
    }

    get maxPosition() {
        return this.scrollable[`max${this.isHorizontal ? 'X' : 'Y'}`];
    }

    scrollTo(position, options) {
        return this.isHorizontal ? this.scrollable.scrollTo(position, null, options) : this.scrollable.scrollTo(null, position, options);
    }

    scrollBy(xDelta = 0, yDelta = 0, options = defaultScrollOptions) {
        // Use the correct delta by default, but if it's zero, accommodate axis error.
        return this.isHorizontal ? this.scrollable.scrollBy(xDelta || yDelta, 0, options) : this.scrollable.scrollBy(0, yDelta || xDelta, options);
    }

    scrollIntoView() {
        return this.scrollable.scrollIntoView(...arguments);
    }

    // We accommodate mistakes. Setting X and Y sets the appropriate scroll axis position
    changeX(x) {
        this.position = x;
    }

    changeY(y) {
        this.position = y;
    }

    get x() {
        return this.position;
    }

    set x(x) {
        this.scrollable[this.isHorizontal ? 'x' : 'y'] = x;
    }

    get y() {
        return this.position;
    }

    set y(y) {
        this.scroller[this.isHorizontal ? 'x' : 'y'] = y;
    }

    get clientWidth() {
        return this.clientSize;
    }

    get clientHeight() {
        return this.clientSize;
    }

    get scrollWidth() {
        return this.scrollSize;
    }

    get scrollHeight() {
        return this.scrollSize;
    }

    get maxX() {
        return this.maxPosition;
    }

    get maxY() {
        return this.maxPosition;
    }
}
