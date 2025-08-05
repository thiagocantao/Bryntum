import Scroller from '../../Core/helper/util/Scroller.js';

/**
 * @module Grid/util/GridScroller
 */

const
    xAxis = {
        x : 1
    },
    subGridFilter = w => w.isSubGrid;

/**
 * A Scroller subclass which handles scrolling in a grid.
 *
 * If the grid has no parallel scrolling grids (No locked columns), then this functions
 * transparently as a Scroller.
 *
 * If there are locked columns, then scrolling to an _element_ will invoke the scroller
 * of the subgrid which contains that element.
 * @internal
 */
export default class GridScroller extends Scroller {
    addScroller(scroller) {
        (this.xScrollers || (this.xScrollers = [])).push(scroller);
    }

    addPartner(otherScroller, axes = xAxis) {
        if (typeof axes === 'string') {
            axes = {
                [axes] : 1
            };
        }

        // Link up all our X scrollers
        if (axes.x) {
            // Ensure the other grid has set up its scrollers. This is done on first paint
            // so may not have been executed yet.
            otherScroller.owner.initScroll();


            const
                subGrids = this.widget.items.filter(subGridFilter),
                otherSubGrids = otherScroller.widget.items.filter(subGridFilter);

            // Loop through SubGrids to ensure that we partner their scrollers up correctly
            for (let i = 0, { length } = subGrids; i < length; i++) {
                subGrids[i].scrollable.addPartner(otherSubGrids[i].scrollable, 'x');
            }
        }
        // We are the only Y scroller
        if (axes.y) {
            super.addPartner(otherScroller, 'y');
        }
    }

    removePartner(otherScroller) {
        this.xScrollers.forEach((scroller, i) => {
            if (!scroller.isDestroyed) {
                scroller.removePartner(otherScroller.xScrollers[i]);
            }
        });

        super.removePartner(otherScroller);
    }

    updateOverflowX(overflowX) {
        const hideScroll = overflowX === false;
        this.xScrollers?.forEach(s => s.overflowX = hideScroll ? 'hidden' : 'hidden-scroll');
        this.widget.virtualScrollers.classList.toggle('b-hide-display', hideScroll);
    }

    scrollIntoView(element, options) {
        // If we are after an element, we have to ask the scroller of the SubGrid
        // that the element is in. It will do the X scrolling and delegate the Y
        // scrolling up to this GridScroller.
        if (element.nodeType === Element.ELEMENT_NODE && this.element.contains(element)) {
            for (const subGridScroller of this.xScrollers) {
                if (subGridScroller.element.contains(element)) {
                    return subGridScroller.scrollIntoView(element, options);
                }
            }
        }
        else {
            return super.scrollIntoView(element, options);
        }
    }

    hasOverflow(axis = 'y') {
        return axis === 'y' ? this.scrollHeight > this.clientHeight : false;
    }

    set x(x) {
        if (this.xScrollers) {
            this.xScrollers[0].x = x;
        }
    }

    get x() {
        // when trying to scroll grid with no columns xScrollers do not exist
        return this.xScrollers ? this.xScrollers[0].x : 0;
    }
}
