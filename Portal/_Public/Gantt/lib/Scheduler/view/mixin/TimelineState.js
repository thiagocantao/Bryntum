import Base from '../../../Core/Base.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/view/mixin/TimelineState
 */

const copyProperties = [
    'barMargin'
];

/**
 * Mixin for Timeline base that handles state. It serializes the following timeline properties:
 *
 * * barMargin
 * * zoomLevel
 *
 * See {@link Grid.view.mixin.GridState} and {@link Core.mixin.State} for more information on state.
 *
 * @mixin
 */
export default Target => class TimelineState extends (Target || Base) {
    static get $name() {
        return 'TimelineState';
    }

    /**
     * Gets or sets timeline's state. Check out {@link Scheduler.view.mixin.TimelineState} mixin for details.
     * @member {Object} state
     * @property {Object[]} state.columns
     * @property {Number} state.rowHeight
     * @property {Object} state.scroll
     * @property {Number} state.scroll.scrollLeft
     * @property {Number} state.scroll.scrollTop
     * @property {Array} state.selectedRecords
     * @property {String} state.style
     * @property {String} state.selectedCell
     * @property {Object} state.store
     * @property {Object} state.store.sorters
     * @property {Object} state.store.groupers
     * @property {Object} state.store.filters
     * @property {Object} state.subGrids
     * @property {Number} state.barMargin
     * @property {Number} state.zoomLevel
     * @category State
     */

    /**
     * Get timeline's current state for serialization. State includes rowHeight, headerHeight, readOnly, selectedCell,
     * selectedRecordId, column states and store state etc.
     * @returns {Object} State object to be serialized
     * @private
     */
    getState() {
        const
            me    = this,
            state = ObjectHelper.copyProperties(super.getState(), me, copyProperties);

        state.zoomLevel = me.zoomLevel;

        state.zoomLevelOptions = {
            startDate  : me.startDate,
            endDate    : me.endDate,
            // With infinite scroll reading viewportCenterDate too early will lead to exception
            centerDate : !me.infiniteScroll || me.timeAxisViewModel.availableSpace ? me.viewportCenterDate : undefined,
            width      : me.tickSize
        };

        return state;
    }

    /**
     * Apply previously stored state.
     * @param {Object} state
     * @private
     */
    applyState(state) {
        const me = this;

        me.suspendRefresh();

        ObjectHelper.copyProperties(me, state, copyProperties);

        super.applyState(state);

        if (state.zoomLevel != null) {
            // Do not restore left scroll, infinite scroll should do all the work
            if (me.infiniteScroll) {
                if (state?.scroll?.scrollLeft) {
                    state.scroll.scrollLeft = {};
                }
            }

            if (me.isPainted) {
                me.zoomToLevel(state.zoomLevel, state.zoomLevelOptions);
            }
            else {
                me._zoomAfterPaint = { zoomLevel : state.zoomLevel, zoomLevelOptions : state.zoomLevelOptions };
            }
        }

        me.resumeRefresh(true);
    }

    onPaint(...args) {
        super.onPaint(...args);

        if (this._zoomAfterPaint) {
            const { zoomLevel, zoomLevelOptions } = this._zoomAfterPaint;

            this.zoomToLevel(zoomLevel, zoomLevelOptions);

            delete this._zoomAfterPaint;
        }
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
