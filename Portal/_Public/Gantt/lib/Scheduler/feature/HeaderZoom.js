import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';

/**
 * @module Scheduler/feature/HeaderZoom
 */

/**
 * Enables users to click and drag to zoom to a date range in Scheduler's header time axis. Only supported in horizontal
 * mode.
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *   features : {
 *     headerZoom : true
 *   }
 * });
 * ```
 *
 * {@inlineexample Scheduler/feature/HeaderZoom.js}
 *
 * <div class="note">Not compatible with the {@link Scheduler/feature/TimeSelection} feature.</div>
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype headerZoom
 * @feature
 */
export default class HeaderZoom extends InstancePlugin {

    static $name = 'HeaderZoom';

    static get pluginConfig() {
        return {
            chain : ['onElementMouseDown', 'onElementMouseMove']
        };
    }

    onElementMouseDown(event) {
        const me = this;

        // only react to mouse input, and left button
        if (event.touches || event.button !== 0 || me.disabled) {
            return;
        }

        // only react to mousedown directly on timeaxis cell
        if (event.target.closest('.b-sch-header-timeaxis-cell')) {
            const headerEl = me.client.subGrids.normal.header.headersElement;

            me.startX = event.clientX;

            me.element = DomHelper.createElement({
                parent    : headerEl,
                tag       : 'div',
                className : 'b-headerzoom-rect'
            });

            me.headerElementRect = Rectangle.from(headerEl);

            EventHelper.on({
                element : document,
                mouseup : 'onMouseUp',
                thisObj : me,
                once    : true
            });
        }
    }

    onElementMouseMove(event) {
        const me = this;

        if (typeof me.startX === 'number') {
            const
                x     = Math.max(event.clientX, me.headerElementRect.left),
                left  = Math.min(me.startX, x),
                width = Math.abs(me.startX - x),
                rect  = new Rectangle(left - me.headerElementRect.x + me.client.scrollLeft, 0, width, me.headerElementRect.height);

            DomHelper.setTranslateX(me.element, rect.left);
            me.element.style.width = rect.width + 'px';
        }
    }

    onMouseUp() {
        const me = this;

        if (typeof me.startX === 'number') {
            const
                { client } = me,
                rect       = Rectangle.from(me.element),
                startDate  = client.getDateFromCoordinate(rect.left, 'round', false),
                endDate    = client.getDateFromCoordinate(rect.right, 'round', false);

            me.element.remove();
            me.startX = null;

            client.zoomToSpan({
                startDate,
                endDate
            });
        }
    }
}

GridFeatureManager.registerFeature(HeaderZoom, false, 'Scheduler');
