import SubGrid from '../../Grid/view/SubGrid.js';
import Header from './Header.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module Scheduler/view/TimeAxisSubGrid
 */

/**
 * Widget that encapsulates the SubGrid part of the scheduler which houses the timeline view.
 * @extends Grid/view/SubGrid
 * @private
 */
export default class TimeAxisSubGrid extends SubGrid {

    static get $name() {
        return 'TimeAxisSubGrid';
    }

    // Factoryable type name
    static get type() {
        return 'timeaxissubgrid';
    }

    static get configurable() {
        return {
            // A Scheduler's SubGrid doesn't accept external columns moving in
            sealedColumns : true,

            // Use Scheduler's Header class
            headerClass : Header
        };
    }

    startConfigure(config) {
        const { grid : scheduler } = config;

        // Scheduler references its TimeAxisSubGrid instance through this property.
        scheduler.timeAxisSubGrid = this;

        super.startConfigure(config);

        if (scheduler.isHorizontal) {
            config.header = {
                cls : {
                    'b-sticky-headers' : scheduler.stickyHeaders
                }
            };
            // We don't use what the GridSubGrids mixin tells us to.
            // We use the Sheduler's Header class.
            delete config.headerClass;
        }

        // If user have not specified a width or flex for scheduler region, default to flex=1
        if (!('flex' in config || 'width' in config)) {
            config.flex = 1;
        }
    }

    changeScrollable() {
        const
            me         = this,
            scrollable = super.changeScrollable(...arguments);

        // TimeAxisSubGrid's X axis is stretched by its canvas.
        // We don't need the Scroller's default stretching implementation.
        if (scrollable) {
            Object.defineProperty(scrollable, 'scrollWidth', {
                get() {
                    return this.element?.scrollWidth ?? 0;
                },
                set() {
                    // Setting the scroll width to be wide just updates the canvas side in Scheduler.
                    // We do not need the Scroller's default stretcher element to be added.
                    // Note that "me" here is the TimeAxisSubGrid, so we are calling Scheduler.
                    me.grid.updateCanvasSize();
                }
            });
        }

        return scrollable;
    }

    handleHorizontalScroll(addCls = true) {
        // Swallow scroll syncing calls that happen during view preset changes, that process triggers multiple when
        // it first changes tickWidth, then scrolls to center and then an additional sync on scroll end
        if (!this.grid._viewPresetChanging) {
            super.handleHorizontalScroll(addCls);
        }
    }

    /**
     * This is an event handler triggered when the TimeAxisSubGrid changes size.
     * Its height changes when content height changes, and that is not what we are
     * interested in here. If the *width* changes, that means the visible viewport
     * has changed size.
     * @param {HTMLElement} element
     * @param {Number} width
     * @param {Number} height
     * @param {Number} oldWidth
     * @param {Number} oldHeight
     * @private
     */
    onInternalResize(element, width, height, oldWidth, oldHeight) {
        const me = this;

        // We, as the TimeAxisSubGrid dictate the scheduler viewport width
        if (me.isPainted && width !== oldWidth) {
            const
                scheduler  = me.grid,
                bodyHeight = scheduler._bodyRectangle.height;

            // Avoid ResizeObserver errors when this operation may create a scrollbar
            if (DomHelper.scrollBarWidth && width < oldWidth) {
                me.monitorResize = false;
            }
            scheduler.onSchedulerViewportResize(width, bodyHeight, oldWidth, bodyHeight);

            // Revert to monitoring on the next animation frame.
            // This is to avoid "ResizeObserver loop completed with undelivered notifications."
            if (!me.monitorResize) {
                me.requestAnimationFrame(() => me.monitorResize = true);
            }
        }

        super.onInternalResize(...arguments);
    }

    // When restoring state we need to update time axis size immediately, resize event is not triggered fast enough to
    // restore center date consistently
    clearWidthCache() {
        super.clearWidthCache();

        // Check if we are in horizontal mode
        if (this.owner.isHorizontal) {
            this.owner.updateViewModelAvailableSpace(this.width);
        }
    }

    async expand() {
        const { owner } = this;

        await super.expand();

        if (owner.isPainted) {
            owner.timeAxisViewModel.update(this.width, false, true);
        }
    }
}

// Register this widget type with its Factory
TimeAxisSubGrid.initClass();
