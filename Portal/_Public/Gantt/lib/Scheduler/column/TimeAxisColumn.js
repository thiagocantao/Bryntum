import WidgetColumn from '../../Grid/column/WidgetColumn.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import Events from '../../Core/mixin/Events.js';
import HorizontalTimeAxis from '../view/HorizontalTimeAxis.js';
import ResourceHeader from '../view/ResourceHeader.js';

/**
 * @module Scheduler/column/TimeAxisColumn
 */

/**
 * A column containing the timeline "viewport", in which events, dependencies etc. are drawn.
 * Normally you do not need to interact with or create this column, it is handled by Scheduler.
 *
 * If you wish to output custom contents inside the time axis row cells, you can provide your custom column configuration
 * using the {@link #config-renderer} like so:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *    appendTo         : document.body
 *    columns          : [
 *       { text : 'Name', field : 'name', width : 130 },
 *       {
 *           type : 'timeAxis',
 *           renderer({ record, cellElement }) {
 *               return '<div class="cool-chart"></div>';
 *           }
 *       }
 *    ]
 * });
 * ```
 *
 * @extends Grid/column/WidgetColumn
 * @column
 */
export default class TimeAxisColumn extends Events(WidgetColumn) {
    //region Config

    static $name = 'TimeAxisColumn';

    static get fields() {
        return [
            // Exclude some irrelevant fields from getCurrentConfig()
            { name : 'locked', persist : false },
            { name : 'flex', persist : false },
            { name : 'width', persist : false },
            { name : 'cellCls', persist : false },
            { name : 'field', persist : false },
            'mode'
        ];
    }

    static get defaults() {
        return {
            /**
             * Set to false to prevent this column header from being dragged.
             * @config {Boolean} draggable
             * @category Interaction
             * @default false
             */
            draggable : false,

            /**
             * Set to false to prevent grouping by this column.
             * @config {Boolean} groupable
             * @category Interaction
             * @default false
             */
            groupable : false,

            /**
             * Allow column visibility to be toggled through UI.
             * @config {Boolean} hideable
             * @default false
             * @category Interaction
             */
            hideable : false,

            /**
             * Show column picker for the column.
             * @config {Boolean} showColumnPicker
             * @default false
             * @category Menu
             */
            showColumnPicker : false,

            /**
             * Allow filtering data in the column (if Filter feature is enabled)
             * @config {Boolean} filterable
             * @default false
             * @category Interaction
             */
            filterable : false,

            /**
             * Allow sorting of data in the column
             * @config {Boolean} sortable
             * @category Interaction
             * @default false
             */
            sortable : false,

            /**
             * Set to `false` to prevent the column from being drag-resized when the ColumnResize plugin is enabled.
             * @config {Boolean} resizable
             * @default false
             * @category Interaction
             */
            resizable : false,

            /**
             * Allow searching in the column (respected by QuickFind and Search features)
             * @config {Boolean} searchable
             * @default false
             * @category Interaction
             */
            searchable : false,

            /**
             * @config {String} editor
             * @hide
             */
            editor : false,

            /**
             * Set to `true` to show a context menu on the cell elements in this column
             * @config {Boolean} enableCellContextMenu
             * @default false
             * @category Menu
             */
            enableCellContextMenu : false,

            /**
             * @config {Function|Boolean} tooltipRenderer
             * @hide
             */
            tooltipRenderer : false,

            /**
             * CSS class added to the header of this column
             * @config {String} cls
             * @category Rendering
             * @default 'b-sch-timeaxiscolumn'
             */
            cls : 'b-sch-timeaxiscolumn',

            // needs to have width specified, flex-basis messes measurements up
            needWidth : true,

            mode       : null,
            region     : 'normal',
            exportable : false,
            htmlEncode : false
        };
    }

    static get type() {
        return 'timeAxis';
    }

    //region Init

    construct(config) {
        const me = this;

        super.construct(...arguments);

        me.thisObj = me;
        me.timeAxisViewModel = me.grid.timeAxisViewModel;
        // A bit hacky, because mode is a field and not a config
        // eslint-disable-next-line no-self-assign
        me.mode = me.mode;

        me.grid.ion({
            paint   : 'onTimelinePaint',
            thisObj : me,
            once    : true
        });
    }

    static get autoExposeFields() {
        return true;
    }

    // endregion

    doDestroy() {
        this.resourceColumns?.destroy();
        this.timeAxisView?.destroy();
        super.doDestroy();
    }

    set mode(mode) {
        const
            me       = this,
            { grid } = me;

        me.set('mode', mode);

        // In horizontal mode this column has a time axis header on top, with timeline ticks
        if (mode === 'horizontal') {
            me.timeAxisView = new HorizontalTimeAxis({
                model                     : me.timeAxisViewModel,
                compactCellWidthThreshold : me.compactCellWidthThreshold,
                owner                     : grid,
                client                    : grid
            });
        }
        // In vertical mode, it instead displays resources at top
        else if (mode === 'vertical') {

            me.resourceColumns = ResourceHeader.new({
                column           : me,
                scheduler        : grid,
                resourceStore    : grid.resourceStore,
                imagePath        : grid.resourceImagePath,
                imageExtension   : grid.resourceImageExtension,
                defaultImageName : grid.defaultResourceImageName
            }, grid.resourceColumns || {});

            me.relayEvents(me.resourceColumns, [
                'resourceheaderclick',
                'resourceheaderdblclick',
                'resourceheadercontextmenu'
            ]);
        }
    }

    get mode() {
        return this.get('mode');
    }



    //region Events

    onViewModelUpdate({ source : viewModel }) {
        const me = this;

        if (me.grid.timeAxisSubGrid.collapsed) {
            return;
        }

        if (me.mode === 'horizontal') {
            // render the time axis view into the column header element
            me.refreshHeader(true);

            me.width = viewModel.totalSize;

            me.grid.refresh();

            // When width is set above, that ends up on a columnsResized listener, but the refreshing of the fake
            // scrollers to accommodate the new width is not done in this timeframe, so the upcoming centering related
            // to preset change cannot work. So we have to refresh the fake scrollers now
            me.subGrid.refreshFakeScroll();
        }
        else if (me.mode === 'vertical') {
            // Refresh to rerender cells, in the process updating the vertical timeaxis to reflect view model changes
            me.grid.refreshRows();
        }
    }

    // Called on paint. SubGrid has its width so this is the earliest time to configure the TimeAxisViewModel with
    // correct width
    onTimelinePaint({ firstPaint }) {
        const me = this;

        if (!me.subGrid.insertRowsBefore) {
            return;
        }

        if (firstPaint) {
            me.subGridElement.classList.add('b-timeline-subgrid');

            if (me.mode === 'vertical') {
                me.refreshHeader();

                // The above operation can cause height change.
                me.grid?.onHeightChange();
            }
        }
    }

    //endregion

    //region Rendering

    /**
     * Refreshes the columns header contents (which is either a HorizontalTimeAxis or a ResourceHeader). Useful if you
     * have rendered some extra meta data that depends on external data such as the EventStore or ResourceStore.
     */
    refreshHeader(internal) {
        const
            me          = this,
            { element } = me;

        if (element) {

            if (me.mode === 'horizontal') {
                // Force timeAxisViewModel to regenerate its column config, which calls header renderers etc.
                !internal && me.timeAxisViewModel.update(undefined, undefined, true);

                if (!me.timeAxisView.rendered) {
                    // Do not need the normal header markup
                    element.innerHTML = '';

                    me.timeAxisView.render(element);
                }
                else {
                    // Force rebuild of cells in case external data has changed (cheap since it still syncs to DOM)
                    me.timeAxisView.refresh(true);
                }
            }
            else if (me.mode === 'vertical') {
                if (!me.resourceColumns.currentElement) {
                    // Do not need the normal header markup
                    element.innerHTML = '';
                    me.resourceColumns.render(element);
                }
                else {
                    me.resourceColumns.refresh();
                }

                // Vertical's resourceColumns is redrawn with the events, no need here
            }
        }
    }

    internalRenderer(renderData) {
        const { grid } = this;
        // No drawing of events before engines initial commit
        if (grid.project.isInitialCommitPerformed || grid.project.isDelayingCalculation) {
            grid.currentOrientation.renderer(renderData);

            return super.internalRenderer(renderData);
        }

        return '';
    }

    //endregion

    get timeAxisViewModel() {
        return this._timeAxisViewModel;
    }

    set timeAxisViewModel(timeAxisViewModel) {
        const me = this;

        me.detachListeners('tavm');

        timeAxisViewModel?.ion({
            name    : 'tavm',
            update  : 'onViewModelUpdate',
            prio    : -10000,
            thisObj : me
        });

        me._timeAxisViewModel = timeAxisViewModel;

        if (me.timeAxisView) {
            me.timeAxisView.model = timeAxisViewModel;
        }
    }

    // Width of the time axis column is solely determined by the zoom level. We should not keep it part of the state
    // otherwise restoring the state might break the normal zooming process.
    // Covered by SchedulerState.t
    // https://github.com/bryntum/support/issues/5545
    getState() {
        const state = super.getState();

        delete state.width;
        delete state.flex;

        return state;
    }
}

ColumnStore.registerColumnType(TimeAxisColumn);
