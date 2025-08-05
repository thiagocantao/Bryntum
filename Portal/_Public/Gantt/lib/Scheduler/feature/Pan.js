import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import EventHelper from '../../Core/helper/EventHelper.js';

/**
 * @module Scheduler/feature/Pan
 */

/**
 * Makes the scheduler's timeline pannable by dragging with the mouse. Try it out in the demo below.
 *
 * {@inlineexample Scheduler/feature/Pan.js}
 *
 * ```javascript
 * // Enable Pan
 * const scheduler = new Scheduler({
 *   features : {
 *     pan : true,
 *     eventDragCreate : false
 *   }
 * });
 * ```
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * <div class="note">Incompatible with the {@link Scheduler.feature.EventDragCreate} and the
 * {@link Scheduler.feature.EventDragSelect} features.</div>
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype pan
 * @feature
 */
export default class Pan extends InstancePlugin {
    // region Init

    static $name = 'Pan';

    static configurable = {
        /**
         * Set to `false` to not pan horizontally
         * @prp {Boolean}
         * @default
         */
        horizontal : true,

        /**
         * Set to `false` to not pan vertically
         * @prp {Boolean}
         * @default
         */
        vertical : true,

        /**
         * Set to `false` to not pan horizontally when dragging in the time axis header
         * @prp {Boolean}
         * @default
         */
        enableInHeader : true
    };

    get targetSelector() {
        return `.b-sch-timeaxis-cell,.b-timeline-subgrid${this.enableInHeader ? ',.b-sch-header-timeaxis-cell,.b-sch-header-text' : ''}`;
    }

    //endregion

    //region Plugin config

    static get pluginConfig() {
        return {
            chain : ['onElementMouseDown']
        };
    }

    //endregion

    // region Events

    /**
     * Fires on the owning Scheduler before pan starts. Return `false` to prevent the operation.
     * @event beforePan
     * @preventable
     * @on-owner
     * @param {Event} event The native browser DOM event
     */

    //endregion

    onElementMouseDown(event) {
        const
            me                                            = this,
            { client }                                    = me,
            { button, touches, target, clientX, clientY } = event,
            dragFeature                                   = client.features.taskDrag || client.features.eventDrag,
            enablePanOnEvents                             = client.readOnly || !dragFeature?.enabled;

        // only react to mouse input, and left button
        if (touches || button !== 0 || me.disabled || client.trigger('beforePan', { event }) === false) {
            return;
        }

        // only react to mousedown directly on grid cell, subgrid element or if drag is disabled - the events too
        if (target.matches(me.targetSelector) || (enablePanOnEvents && target.closest(client.eventSelector))) {
            me.mouseX   = clientX;
            me.mouseY   = clientY;
            me.onHeader = me.enableInHeader && target.closest('.b-sch-header-timeaxis-cell');

            me.mouseDetacher = EventHelper.on({
                element   : document,
                mousemove : 'onMouseMove',
                mouseup   : 'onMouseUp',
                thisObj   : me
            });
        }
    }

    onMouseMove(event) {
        const
            me         = this,
            { client } = me,
            xScroller  = client.timeAxisSubGrid.scrollable,
            yScroller  = client.scrollable,
            x          = event.clientX,
            y          = event.clientY;

        event.preventDefault();

        if (me.vertical && (client.isVertical || !me.onHeader)) {
            yScroller.scrollBy(0, me.mouseY - y);
        }

        if (me.horizontal && (client.isHorizontal || !me.onHeader)) {
            xScroller.scrollBy(me.mouseX - x);
        }

        me.mouseX = x;
        me.mouseY = y;
    }

    onMouseUp() {
        this.mouseDetacher();
        this.mouseDetacher = null;
    }

    /**
     * Yields `true` if a pan gesture is in process.
     * @property {Boolean}
     * @readonly
     */
    get isActive() {
        return Boolean(this.mouseDetacher);
    }

    //endregion
}

GridFeatureManager.registerFeature(Pan, false, ['Scheduler', 'Gantt']);
