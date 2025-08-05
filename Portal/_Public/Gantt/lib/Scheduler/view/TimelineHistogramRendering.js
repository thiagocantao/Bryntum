import Base from '../../Core/Base.js';

export default class TimelineHistogramRendering extends Base {

    static configurable = {
        scrollBuffer : 0
    };

    construct(client) {
        super.construct();
        this.client = client;
    }

    init() {}

    onTimeAxisViewModelUpdate() {
        const { scrollable } = this.client.timeAxisSubGrid;

        // scrollLeft is the DOM's concept which is -ve in RTL mode.
        // scrollX i always the +ve scroll offset from the origin.
        // Both may be needed for different calculations.
        this.updateFromHorizontalScroll(scrollable.x);
    }

    // Update header range on horizontal scroll
    updateFromHorizontalScroll(scrollX) {
        const
            me            = this,
            {
                client,
                // scrollBuffer is an export only thing
                scrollBuffer
            } = me,
            {
                timeAxisSubGrid,
                timeAxis,
                rtl
            }             = client,
            { width }     = timeAxisSubGrid,
            { totalSize } = client.timeAxisViewModel,
            start         = scrollX,
            // If there are few pixels left from the right most position then just render all remaining ticks,
            // there wouldn't be many. It makes end date reachable with more page zoom levels while not having any poor
            // implications.
            // 5px to make TimeViewRangePageZoom test stable in puppeteer.
            returnEnd     = timeAxisSubGrid.scrollable.maxX !== 0 && Math.abs(timeAxisSubGrid.scrollable.maxX) <= Math.round(start) + 5,
            startDate     = client.getDateFromCoord({ coord : Math.max(0, start - scrollBuffer), ignoreRTL : true }),
            endDate       = returnEnd ? timeAxis.endDate : (client.getDateFromCoord({ coord : start + width + scrollBuffer, ignoreRTL : true }) || timeAxis.endDate);

        if (startDate && !client._viewPresetChanging) {
            me._visibleDateRange = { startDate, endDate, startMS : startDate.getTime(), endMS : endDate.getTime() };
            me.viewportCoords  = rtl
                // RTL starts all the way to the right (and goes in opposite direction)
                ? { left : totalSize - scrollX - width + scrollBuffer, right : totalSize - scrollX - scrollBuffer }
                // LTR all the way to the left
                : { left : scrollX - scrollBuffer, right : scrollX + width + scrollBuffer };

            // Update timeaxis header making it display the new dates
            const range = client.timeView.range = { startDate, endDate };

            client.onVisibleDateRangeChange(range);

            // If refresh is suspended, someone else is responsible for updating the UI later
            if (!client.refreshSuspended && client.rowManager.rows.length) {
                // Gets here too early in Safari for ResourceHistogram. ResizeObserver triggers a scroll before rows are
                // rendered first time. Could not track down why, bailing out
                if (client.rowManager.rows[0].id === null) {
                    return;
                }

                // re-render all rows is timeAxis range has been updated
                if (me._timeAxisStartDate - timeAxis.startDate || me._timeAxisEndDate - timeAxis.endDate) {
                    me._timeAxisStartDate = timeAxis.startDate;
                    me._timeAxisEndDate = timeAxis.endDate;

                    client.rowManager.renderRows(client.rowManager.rows);
                }
            }
        }
    }

    onViewportResize() {}

    refreshRows() {}

    get visibleDateRange() {
        return this._visibleDateRange;
    }

    translateToPageCoordinate(x) {
        const
            { client } = this,
            { scrollable } = client.timeAxisSubGrid;

        let result = x + client.timeAxisSubGridElement.getBoundingClientRect().left;

        if (client.rtl) {
            result -= scrollable.maxX - Math.abs(client.scrollLeft);
        }
        else {
            result -= client.scrollLeft;
        }

        return result;
    }

    translateToScheduleCoordinate(x) {
        const
            { client } = this,
            { scrollable } = client.timeAxisSubGrid;

        let result = x - client.timeAxisSubGridElement.getBoundingClientRect().left - globalThis.scrollX;

        // Because we use getBoundingClientRect's left, we have to adjust for page scroll.

        if (client.rtl) {
            result += scrollable.maxX - Math.abs(client.scrollLeft);
        }
        else {
            result += client.scrollLeft;
        }

        return result;
    }

    getDateFromXY(xy, roundingMethod, local, allowOutOfRange = false) {
        const { client } = this;

        let coord = xy[0];

        if (!local) {
            coord = this.translateToScheduleCoordinate(coord);
        }

        coord = client.getRtlX(coord);

        return client.timeAxisViewModel.getDateFromPosition(coord, roundingMethod, allowOutOfRange);
    }

}
