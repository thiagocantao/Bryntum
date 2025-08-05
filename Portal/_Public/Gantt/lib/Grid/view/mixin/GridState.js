import Base from '../../../Core/Base.js';
import WidgetHelper from '../../../Core/helper/WidgetHelper.js';



/**
 * @module Grid/view/mixin/GridState
 */
const
    suspendStoreEvents = subGrid => subGrid.columns.suspendEvents(),
    resumeStoreEvents = subGrid => subGrid.columns.resumeEvents(),
    fillSubGridColumns = subGrid => {
        subGrid.columns.clearCaches();
        subGrid.columns.fillFromMaster();
    },
    compareStateSortIndex = (a, b) => a.stateSortIndex - b.stateSortIndex;

/**
 * Mixin for Grid that handles state. It serializes the following grid properties:
 *
 * * rowHeight
 * * selectedCell
 * * selectedRecords
 * * columns (order, widths, visibility)
 * * store (sorters, groupers, filters)
 * * scroll position
 *
 * See {@link Core.mixin.State} for more information on state.
 *
 * @demo Grid/state
 * @inlineexample Grid/view/mixin/GridState.js
 * @mixin
 */
export default Target => class GridState extends (Target || Base) {
    static get $name() {
        return 'GridState';
    }

    static get configurable() {
        return {
            statefulEvents : ['subGridCollapse', 'subGridExpand', 'horizontalScrollEnd', 'stateChange']
        };
    }

    /**
     * Gets or sets grid's state. Check out {@link Grid.view.mixin.GridState} mixin for details.
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
     * @category State
     */

    updateStore(store, was) {
        super.updateStore?.(store, was);

        this.detachListeners('stateStoreListeners');

        store?.ion({
            name    : 'stateStoreListeners',
            filter  : 'triggerUpdate',
            group   : 'triggerUpdate',
            sort    : 'triggerUpdate',
            thisObj : this
        });
    }

    updateColumns(columns, was) {
        super.updateColumns?.(columns, was);

        this.detachListeners('stateColumnListeners');

        columns.ion({
            name    : 'stateColumnListeners',
            change  : 'triggerUpdate',
            thisObj : this
        });
    }

    updateRowManager(manager, was) {
        super.updateRowManager?.(manager, was);

        this.detachListeners('stateRowManagerListeners');

        manager.ion({
            name      : 'stateRowManagerListeners',
            rowHeight : 'triggerUpdate',
            thisObj   : this
        });
    }

    triggerUpdate() {
        this.trigger('stateChange');
    }

    finalizeInit() {
        super.finalizeInit();

        this.ion({
            selectionChange : 'triggerUpdate',
            thisObj         : this
        });
    }

    /**
     * Get grid's current state for serialization. State includes rowHeight, headerHeight, selectedCell,
     * selectedRecordId, column states and store state etc.
     * @returns {Object} State object to be serialized
     * @private
     */
    getState() {
        const
            me    = this,
            style = me.element.style.cssText,
            state = {
                rowHeight : me.rowHeight
            };

        if (style) {
            state.style = style;
        }

        if (me.selectedCell) {

            const { id, columnId } = me.selectedCell;
            state.selectedCell = { id, columnId };
        }

        state.selectedRecords = me.selectedRecords.map(entry => entry.id);
        state.columns = me.columns.allRecords.map(column => column.getState());
        state.store = me.store.state;
        state.scroll = me.storeScroll();

        state.subGrids = {};

        me.eachSubGrid(subGrid => {
            const config = state.subGrids[subGrid.region] = state.subGrids[subGrid.region] || {};

            if (subGrid.isPainted) {
                if (subGrid.flex == null) {
                    config.width = subGrid.width;
                }
            }
            else {
                if (subGrid.config.width != null) {
                    config.width = subGrid.config.width;
                }
                else {
                    config.flex = subGrid.config.flex;
                }
            }

            config.collapsed = subGrid.collapsed ?? false;

            // Part of a collapsed SubGrid's state is the state to restore to when expanding again.
            if (config.collapsed) {
                config._beforeCollapseState = subGrid._beforeCollapseState;
            }
        });

        return state;
    }

    /**
     * Apply previously stored state.
     * @param {Object} state
     * @private
     */
    applyState(state) {
        const me = this;

        // Applying state will call row renderer at least 7 times. Suspending refresh helps to save some time.
        // Roughly on default testing grid apply state takes 26ms without suspend and 16ms with it.
        me.suspendRefresh();

        // Do this first since it might perform full rendering of contents, recreating filterbar header fields
        if ('columns' in state) {
            let columnsChanged = false,
                needSort = false;

            // We're going to renderContents anyway, so stop the ColumnStores from updating the UI
            me.columns.suspendEvents();
            me.eachSubGrid(suspendStoreEvents);

            // each column triggers rerender at least once...
            state.columns.forEach((columnState, index) => {
                const column = me.columns.getById(columnState.id);

                if (column) {
                    const columnGeneration = column.generation;

                    // If column region is missing in the current config, clear it from the column state and
                    // stick to the default configuration
                    if ('region' in columnState && !(columnState.region in me.subGrids)) {
                        delete columnState.region;
                        delete columnState.locked;
                    }

                    column.applyState(columnState);
                    columnsChanged = columnsChanged || (column.generation !== columnGeneration);

                    // In case a sort is needed, stamp in the ordinal position.
                    column.stateSortIndex = index;

                    // If we find one out of order, only then do we need to sort
                    if (column.allIndex !== index) {
                        needSort = columnsChanged = true;
                    }
                }
            });

            if (columnsChanged) {
                me.eachSubGrid(fillSubGridColumns);
            }
            if (needSort) {
                me.eachSubGrid(subGrid => {
                    subGrid.columns.records.sort(compareStateSortIndex);
                    subGrid.columns.allRecords.sort(compareStateSortIndex);
                });
                me.columns.sort({
                    fn        : compareStateSortIndex,
                    // always sort ascending
                    ascending : true
                });
            }

            // If we have been painted, and column restoration changed the column layout, refresh contents
            if (me.isPainted && columnsChanged) {
                me.renderContents();
            }

            // Allow ColumnStores to update the UI again
            me.columns.resumeEvents();
            me.eachSubGrid(resumeStoreEvents);
        }

        if ('subGrids' in state) {
            me.eachSubGrid(subGrid => {
                if (subGrid.region in state.subGrids) {
                    const subGridState = state.subGrids[subGrid.region];

                    if ('width' in subGridState) {
                        subGrid.width = subGridState.width;
                    }
                    else if ('flex' in subGridState) {
                        subGrid.flex = subGridState.flex;
                    }

                    if ('collapsed' in subGridState) {
                        subGrid.collapsed = subGridState.collapsed;
                        subGrid._beforeCollapseState = subGridState._beforeCollapseState;
                    }
                }

                subGrid.clearWidthCache();
            });
        }

        if ('rowHeight' in state) {
            me.rowHeight = state.rowHeight;
        }

        if ('style' in state) {
            me.style = state.style;
        }

        if ('selectedCell' in state) {
            me.selectedCell = state.selectedCell;
        }

        if ('store' in state) {
            me.store.state = state.store;
        }

        if ('selectedRecords' in state) {
            me.selectedRecords = state.selectedRecords;
        }

        me.resumeRefresh(true);

        me.whenVisible(() => me.applyScrollState(state));
    }

    applyScrollState(state) {
        const me = this;

        // Update scroll state
        me.eachSubGrid(s => s.refreshFakeScroll());

        if ('scroll' in state) {
            me.restoreScroll(state.scroll);

            // We need to force resize handler on all observable elements, because vertical scroll triggered by the
            // previous method will suspend the listener. So by the time ResizeObserver triggers mutation handler
            // listener won't actually update widget size.
            // Handler works here because we haven't _yet_ suspended it, it will happen one animation frame after
            // scroll event is triggered
            if (state.scroll.scrollTop) {
                me.element.querySelectorAll('.b-resize-monitored').forEach(element => {
                    const widget = WidgetHelper.fromElement(element);

                    if (widget) {
                        widget.onElementResize(element);
                    }
                });
            }
        }
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {
    }
};
