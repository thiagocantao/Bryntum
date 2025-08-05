import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import Widget from '../../Core/widget/Widget.js';
import Objects from '../../Core/helper/util/Objects.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import GlobalEvents from '../../Core/GlobalEvents.js';
import '../column/ActionColumn.js';
import Delayable from '../../Core/mixin/Delayable.js';

/**
 * @module Grid/feature/RowExpander
 */

const storeRemoveActions = { remove : 1, filter : 1, dataset : 1, replace : 1 };

/**
 * Enables expanding of Grid rows by either row click or double click, or by adding a separate Grid column which renders
 * a button that expands or collapses the row.
 *
 * {@inlineexample Grid/feature/RowExpander.js}
 *
 * The content of the expanded row body is rendered by providing either a {@link #config-renderer} function to the
 * rowExpander feature config:
 *
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            renderer({record, region, expanderElement}){
 *                return htmlToBeExpanded;
 *            }
 *        }
 *    }
 * });
 * ```
 *
 * Or a {@link #config-widget} configuration object:
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            widget : {
 *                type : 'detailGrid',
 *            },
 *            dataField : 'orderDetails'
 *        }
 *    }
 * });
 * ```
 *
 * {@inlineexample Grid/feature/RowExpanderWidget.js}
 *
 * <div class="note">Note that if used in a Gantt, the Gant's `fixedRowHeight` must be set to `false`.</div>
 *
 * This feature is **disabled** by default
 *
 * ## Expand on click
 * Set {@link #config-triggerEvent} to a Grid cell event that should trigger row expanding and collapsing.
 *
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            triggerEvent: 'celldblclick',
 *            renderer...
 *        }
 *    }
 * });
 * ```
 *
 * ## Expander column position
 * The expander column can either be inserted before or after the existing Grid columns. If the Grid has multiple
 * regions the column will be added to the first region.
 *
 * Adjust expander column position to last in a specific Grid region by setting {@link #config-columnPosition}
 * to `last` and configuring the {@link #config-column} with a region name.
 *
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            column: {
 *                region: 'last'
 *            },
 *            columnPosition: 'last',
 *            renderer...
 *        }
 *    }
 * });
 * ```
 *
 * ## Record update
 * If the expander content depends on row record data, the expander can be re-rendered on record update by setting
 * {@link #config-refreshOnRecordChange} to `true`.
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            refreshOnRecordChange: true,
 *            renderer...
 *        }
 *    }
 * });
 * ```
 *
 * ## Async
 * When the content of the row expander should be rendered async just see to it that you return a promise.
 * ```javascript
 * new Grid({
 *    features : {
 *        rowExpander : {
 *            async renderer({record, region, expanderElement}){
 *                return fetchFromBackendAndRenderData(record);
 *            }
 *        }
 *    }
 * });
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype rowExpander
 * @feature
 */
export default class RowExpander extends InstancePlugin.mixin(Delayable) {

    //region Config
    static $name = 'RowExpander';

    // Cannot use `static properties = {}`, new Map/Set would pollute the prototype
    static get properties() {
        return {
            // CSS classes
            expanderBodyClass        : 'b-rowexpander-body',
            expandedRowClass         : 'b-rowexpander-row-expanded',
            shadowRootContainerClass : 'b-rowexpander-shadowroot-container',
            // Map where the keys are the expanded records and values are an object
            // {rowHeight, cellHeight, expandedBodyElements}
            recordStateMap           : new Map(),
            collapsingStateMap       : new Map()
        };
    }

    static configurable = {

        /**
         * The implementation of this function is called each time the body of an expanded row is rendered. Either
         * return an HTML string, a {@link Core.helper.DomHelper#typedef-DomConfig} object describing the markup or any
         * Widget configuration object, like a Grid configuration object for example.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            renderer({record, region, expanderElement}){
         *                return htmlToBeExpanded;
         *            }
         *        }
         *    }
         * });
         * ```
         *
         * Or return a {@link Core.helper.DomHelper#typedef-DomConfig} object.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            renderer({record, region, expanderElement}){
         *                return {
         *                   tag       : 'form',
         *                   className : 'expanded-row-form',
         *                   children  : [
         *                       {
         *                           tag        : 'textarea',
         *                           name       : 'description',
         *                           className  : 'expanded-textarea'
         *                       },
         *                       {
         *                           tag        : 'button',
         *                           text       : 'Save',
         *                           className  : 'expanded-save-button',
         *                       }
         *                   ]
         *                };
         *            }
         *        }
         *    }
         * });
         * ```
         *
         * Or return a Widget configuration object. What differs a Widget configuration object from a DomConfig object
         * is the presence of the `type` property and the absence of a `tag` property.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            async renderer({record, region, expanderElement}){
         *                const myData = await fetch('myURL');
         *                return {
         *                   type : 'grid',
         *                   autoHeight : true,
         *                   columns : [
         *                       ...
         *                   ],
         *                   data : myData
         *                };
         *            }
         *        }
         *    }
         * });
         * ```
         *
         * It is also possible to add markup directly to the expanderElement.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            renderer({record, region, expanderElement}){
         *                new UIComponent({
         *                    appendTo: expanderElement,
         *                    ...
         *                });
         *            }
         *        }
         *    }
         * });
         * ```
         * The renderer function can also be asynchronous.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            async renderer({record, region, expanderElement}){
         *                return await awaitAsynchronousOperation();
         *            }
         *        }
         *    }
         * });
         * ```
         * @param {Object} renderData Object containing renderer parameters
         * @param {Core.data.Model} renderData.record Record for the row
         * @param {HTMLElement} renderData.expanderElement Expander body element
         * @param {HTMLElement} renderData.rowElement Row element
         * @param {String} renderData.region Grid region name
         * @returns {String|DomConfig} Row expander body content
         * @config {Function}
         * @async
         */
        renderer : null,

        /**
         * The name of the Grid event that will toggle expander. Defaults to `null` but can be set to any event such
         * as {@link Grid.view.mixin.GridElementEvents#event-cellDblClick} or
         * {@link Grid.view.mixin.GridElementEvents#event-cellClick}.
         *
         * ```javascript
         * features : {
         *     rowExpander : {
         *         triggerEvent : 'cellclick'
         *     }
         * }
         * ```
         *
         * @config {String}
         */
        triggerEvent : null,

        /**
         * Provide a column config object to display a button with expand/collapse functionality.
         * Shown by default, set to `null` to not include.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            column: {
         *                // Use column config options here
         *                region: 'last'
         *            }
         *        }
         *    }
         * });
         * ```
         *
         * @config {ActionColumnConfig|Grid.column.ActionColumn}
         */
        column : {},

        /**
         * Makes the expand/collapse button column appear either as the first column (default or `first`) or as the
         * last (set to `last`). Note that the column by default will be added to the first region, if the Grid
         * has multiple regions. Use the {@link #config-column} config to change region.
         * @config {String}
         * @default
         */
        columnPosition : 'first',

        /**
         * If set to `true`, the RowExpander will, on record update, re-render an expanded row by calling the
         * {@link #config-renderer} function or recreate the configured {@link #config-widget}.
         * @config {Boolean}
         * @default
         */
        refreshOnRecordChange : false,

        /**
         * Use this for customizing async {@link #config-renderer} loading indicator height.
         * @config {Number}
         * @defalt
         */
        loadingIndicatorHeight : 100,

        /**
         * Use this for customizing async {@link #config-renderer} loading indicator text.
         * @config {String}
         * @default Loading
         */
        loadingIndicatorText : 'L{loading}',

        /**
         * Use this to disable expand and collapse animations.
         * @config {Boolean}
         * @default
         */
        enableAnimations : true,

        /**
         * A widget configuration object that will be used to create a widget to render into the row expander body. Can
         * be used instead of providing a {@link #config-renderer}.
         *
         * If the widget needs a store, it can be populated by use of the {@link #config-dataField} config. This will
         * create a store from the expanded record's corresponding `dataField` value, which needs to be an array of
         * objects or a store itself.
         *
         * ```javascript
         * new Grid({
         *    features : {
         *        rowExpander : {
         *            widget : {
         *                type : 'detailGrid',
         *            },
         *            dataField : 'orderDetails'
         *        }
         *    }
         * });
         *
         * @config {ContainerItemConfig}
         */
        widget : null,

        /**
         * Used together with {@link #config-widget} to populate the widget's Store from the expanded record's
         * corresponding `dataField` value, which needs to be an array of objects or a store itself.
         * @config {String}
         */
        dataField : null,

        keyMap : {
            // Private
            Tab         : { handler : 'onTab', weight : 50 },
            'Shift+Tab' : { handler : 'onShiftTab', weight : 50 }
        },

        /**
         * When expanding a row and the expanded body element is not completely in view, setting this to `true` will
         * automatically scroll the expanded row into view.
         * @config {Boolean}
         * @default
         */
        autoScroll : false
    };

    // Plugin configuration. This plugin chains/overrides some of the functions in Grid.
    static get pluginConfig() {
        return {
            chain    : ['afterColumnsChange', 'beforeRenderRow', 'processRowHeight', 'bindStore', 'navigateUp'],
            override : ['onGridBodyFocusIn', 'navigateDown', 'catchFocus', 'keyMapOnKeyDown']
        };
    }

    //endregion

    //region Init

    afterConstruct() {
        const
            me         = this,
            { client } = me;

        if (!me.renderer && !me.widget) {
            console.warn('RowExpander requires either a widget config or implementing the renderer function.');
            return;
        }
        if (client.isGanttBase && client.fixedRowHeight !== false) {
            console.warn('When using RowExpander on a Gantt, the Gantt`s fixedRowHeight config must be set to false.');
        }

        if (me.widget) {
            GlobalEvents.ion({
                theme   : me.onThemeChange,
                thisObj : me
            });
        }

        // Bind initial store
        me.bindStore(client.store);

        if (me.triggerEvent) {
            client.ion({ [me.triggerEvent] : 'onTriggerEvent', thisObj : me });
        }

        me.addColumn();

        me.resizeObserver = new ResizeObserver((entries) => me.onExpanderBodyResize(entries));
    }

    bindStore(store) {
        const me = this;

        me.recordStateMap.clear();
        me.collapsingStateMap.clear();
        me.detachListeners('clientStoreChange');

        store.ion({
            name    : 'clientStoreChange',
            change  : me.onStoreChange,
            thisObj : me
        });
    }

    doDisable(disable) {
        const { client } = this;

        if (disable) {
            this.recordStateMap.clear();
            this.collapsingStateMap.clear();
        }

        if (!client.isConfiguring) {
            client.rowManager.renderFromRow();
        }
        super.doDisable(disable);
    }

    changeLoadingIndicatorText(text) {
        return text ? this.L(text) : text;
    }

    // Overrides onGridBodyFocusIn to ignore events on row expander body.
    onGridBodyFocusIn(event) {
        const me = this;

        if (me.widget ? !event.target.matches(`.${me.expanderBodyClass}, .${me.shadowRootContainerClass}`)
            : !me.client.lastMousedownEvent?.target?.closest('.' + me.expanderBodyClass)
        ) {
            me.overridden.onGridBodyFocusIn(event);
        }
    }

    // Override keyMap key down so to not acting on keydown inside nested grid
    keyMapOnKeyDown({ target }) {
        if (!this.widget || !target.classList?.contains(this.shadowRootContainerClass)) {
            this.overridden.keyMapOnKeyDown(...arguments);
        }
    }

    get isAnimating() {
        return this.client.isAnimating;
    }

    set isAnimating(value) {
        const
            { client }     = this,
            wasAnimating   = client.isAnimating;
        client.isAnimating = value;

        if (client.isAnimating !== wasAnimating) {
            client.element.classList.toggle('b-rowexpander-animating');
        }
    }

    //endregion

    //region Events
    /**
     * This event fires before row expand is started.
     *
     * Returning `false` from a listener prevents the RowExpander to expand the row.
     *
     * Note that this event fires when the RowExpander toggles the row, not when the actual row expander body is
     * rendered. Most of the time this is synchronous, but in the case of a row that is not yet rendered into view by
     * scrolling, it can happen much later.
     *
     * @event beforeExpand
     * @preventable
     * @async
     * @param {Core.data.Model} record Record
     */

    /**
     * This event fires before row collapse is started.
     *
     * Returning `false` from a listener prevents the RowExpander to collapse the row.
     *
     * Note that this event fires when the RowExpander toggles the row, not when the actual row expander body is
     * rendered. Most of the time this is synchronous, but in the case of a row that is not yet rendered into view by
     * scrolling, it can happen much later.
     *
     * @event beforeCollapse
     * @preventable
     * @async
     * @param {Core.data.Model} record Record
     */

    /**
     * This event fires when a row expand has finished expanding.
     *
     * Note that this event fires when actual row expander body is rendered, and not necessarily in immediate succession
     * of an expand action. In the case of expanding a row that is not yet rendered into view by scrolling, it can happen
     * much later.
     *
     * @event expand
     * @param {Core.data.Model} record Record
     * @param {Object} expandedElements An object with the Grid region name as property and the expanded body
     * element as value
     * @param {Core.widget.Widget} widget In case of expanding a Widget, this will be a reference to the instance
     * created by the actual expansion
     */

    /**
     * This event fires when a row has finished collapsing.
     *
     * @event collapse
     * @param {Core.data.Model} record Record
     */
    //endregion

    //region ExpanderColumn
    afterColumnsChange() {
        this.addColumn();
    }

    changeColumn(config) {
        if (config == null) {
            return config;
        }
        return {
            type    : 'action',
            actions : [{
                cls     : 'b-icon b-icon-collapse-down',
                tooltip : ({ record }) => this.L(this.recordStateMap.has(record) ? 'L{RowExpander.collapse}' : 'L{RowExpander.expand}'),
                onClick : ({ record }) => this.toggleExpand(record)
            }],
            width    : 40,
            hideable : false,
            align    : 'center',
            region   : this.client.regions[0],
            ...config,
            field    : 'expanderActionColumn'
        };
    }

    // Called in construct and if grid columns change
    addColumn() {
        const
            me          = this,
            { column }  = me,
            { columns } = me.client;

        if (!me._isAddingExpanderColumn && column && (!me._expander || !columns.includes(me._expander))) {
            me._isAddingExpanderColumn = true;
            if (me.columnPosition === 'last') {
                [me._expander] = columns.add(column);
            }
            else {
                [me._expander] = columns.insert(0, column);
            }
            me._isAddingExpanderColumn = false;
        }
    }

    //endregion

    //region UI events

    onTriggerEvent({ target }) {
        // Only grid cell event is handled. Action-cell event has its own handler.
        if (this.disabled || target?.closest('.b-action-cell') || !target.closest('.b-grid-cell')) {
            return;
        }
        this.toggleExpand(this.client.getRecordFromElement(target));
    }

    /**
     * Toggles expanded state.
     * @private
     * @param {Core.data.Model} record The record that should be toggled
     * @category Internal
     */
    toggleExpand(record) {
        if (record) {
            if (this.recordStateMap.has(record)) {
                this.collapse(record);
            }
            else {
                this.expand(record);
            }
        }
    }

    onExpanderBodyResize(entries) {
        const
            { client } = this,
            { store }  = client;

        for (const entry of entries) {
            const
                record      = store.getById(entry.target.parentElement?._domData?.id),
                recordState = record && this.recordStateMap.get(record);

            if (recordState && !recordState.ignoreResize) {
                const oldHeight = recordState.expanderBodyHeight;

                recordState.expanderBodyHeight = null; // Clears saved height to recalc in processRowHeight
                if (this.processRowHeight(record, 0) !== oldHeight) {
                    // bufferedRenderer takes care of multiple rendering calls
                    this.bufferedRenderer(record);
                }
            }
        }
    }

    //endregion

    //region Rendering

    /**
     * Listens to changes in the Grid Store. Will remove expand State data on Store removal.
     * If the refreshOnRecordChange config is `true`, it will trigger a re-render of the expander.
     * @private
     * @param {String} action
     * @param {Core.data.Store} source
     * @param {Core.data.Model[]} records
     * @category Internal
     */
    onStoreChange({ action, source, records, changes }) {
        const
            me                                     = this,
            { recordStateMap, collapsingStateMap } = me,
            changedKeys                            = changes && Object.keys(changes);

        if (changedKeys?.length === 1 && source.modelClass.fieldMap[changedKeys[0]].type === 'store') {
            return;
        }
        if (me.disabled) {
            return;
        }
        if (action === 'removeAll') {
            recordStateMap.clear();
            collapsingStateMap.clear();
        }
        else if (storeRemoveActions[action]) {
            for (const [record, state] of recordStateMap) {
                if (!source.includes(record)) {
                    state.widget?.destroy();
                    recordStateMap.delete(record);
                    collapsingStateMap.delete(record);
                }
            }
        }
        else if (me.refreshOnRecordChange && records?.length) {
            if (action === 'update') {
                const recordState = recordStateMap.get(records[0]);
                if (recordState?.isCreated) {
                    recordState.isCreated = false;
                    me.client.rowManager.renderFromRecord(records[0]);
                }
            }
            else if (action === 'updatemultiple') {
                let topRecordIndex,
                    topRecord;

                for (const rec of records) {
                    const recordState = recordStateMap.get(rec);

                    if (recordState?.isCreated) {
                        const index           = source.records.indexOf(rec);
                        recordState.isCreated = false;

                        if (!topRecord || topRecordIndex > index) {
                            topRecordIndex = index;
                            topRecord      = rec;
                        }
                    }
                }
                if (topRecord) {
                    me.client.rowManager.renderFromRecord(topRecord);
                }
            }
        }
    }

    // Implements grid.processRowHeight hook
    processRowHeight(record, height) {
        const recordState = this.recordStateMap.get(record);
        if (recordState) {
            // If we are waiting for async rendering, height is calculated from a fixed loadingIndicatorHeight.
            if (!recordState.isCreated && recordState.isRenderingAsync) {
                return this.loadingIndicatorHeight + height;
            }

            // If we have a recordState but no expanderBodyHeight, we should recalculate height.
            if (!recordState.expanderBodyHeight) {
                for (const region of this.client.regions) {
                    const height = recordState.expandedBodyElements[region].offsetHeight;
                    if (height > recordState.expanderBodyHeight) {
                        recordState.expanderBodyHeight = height;
                    }
                }
            }
        }
        return (recordState?.expanderBodyHeight ?? 0) + height;
    }

    /**
     * Hooks on before row render to render or remove row expander content depending on record state.
     * @private
     * @category Internal
     */
    beforeRenderRow({ row, record }) {
        const
            me           = this,
            { regions }  = me.client,
            {
                expandedRowClass,
                collapsingStateMap
            }            = me,
            // The map only contains record that are expanded
            recordState  = me.recordStateMap.get(record);

        row.cls.toggle('b-rowexpander-disabled', me.disabled);

        // If current row is expanded
        if (row.cls[expandedRowClass]) {
            // If animating a collapse, content should not be removed until animation is complete
            if (me.enableAnimations && me.isAnimating && collapsingStateMap.has(record)) {
                me.waitForTransition(row, () => {
                    // Make sure record still should be collapsed after animation is complete
                    const collapsingState = collapsingStateMap.get(record);

                    if (collapsingState) {
                        collapsingStateMap.delete(record);
                        me.removeExpander(row);
                        collapsingState.widget?.destroy();
                    }
                });
            }
            // Row is expanded but record should not be, remove expander
            else if (!recordState) {
                me.removeExpander(row);
            }
        }
        else {
            // Makes sure record should collapse no longer
            collapsingStateMap.delete(record);
        }

        if (!me.disabled && recordState) {
            // Expander content is created once, then reused.
            if (!recordState.isCreated) {
                recordState.ignoreResize = true; // Tells the resizeObserver to ignore this element right now
                me.renderExpander(record, row, recordState);
            }

            row.cls.add(expandedRowClass);

            for (const region of regions) {
                const rowElement = row.getElement(region);

                // isCreated means that the content has finished its creation process, which can be async
                if (recordState.isCreated) {
                    const bodyElement = recordState.expandedBodyElements[region];

                    // If the bodyElement is connected to our rowElement, we do not need to do anything
                    if (bodyElement.parentElement !== rowElement) {
                        // If not, remove current content and add the created element
                        DomHelper.removeEachSelector(rowElement, '.' + me.expanderBodyClass);
                        rowElement.appendChild(bodyElement);

                        // Observe body element to refresh grid when the body element resizes
                        me.resizeObserver.observe(bodyElement);
                    }

                    recordState.ignoreResize = false;

                    // Resolve the expand promise on next animation frame
                    if (recordState.renderPromiseResolver) {
                        me.delay(recordState.renderPromiseResolver);
                        recordState.renderPromiseResolver = null;
                    }
                }
                else {
                    // If the renderer is async, we show a loading indicator.
                    me.renderLoadingIndicator(rowElement, recordState);
                }
                me.lockCellHeight(rowElement, recordState.cellHeight, false);
            }

            // If expander body is rendered not fully in view, it will be scrolled into view
            if (me._shouldScrollIntoView && me.autoScroll) {
                me._shouldScrollIntoView = false;
                if (!DomHelper.isInView(recordState.expandedBodyElements[regions[0]], true)) {
                    // Wait for rendering to complete, then scroll
                    me.client.rowManager.ion({
                        once       : true,
                        thisObj    : me,
                        renderDone : () => me.scrollRowIntoView(row, record)
                    });
                }
            }
        }
    }

    /**
     * Scrolls expanded row into view. This function is called after rowManager has finished rendering.
     * @private
     * @category Internal
     */
    scrollRowIntoView(row, record) {
        // If animating expand, need to wait for the animation to end before scrolling.
        if (this.isAnimating) {
            this.waitForTransition(row, () => this.client.scrollRowIntoView(record));
        }
        else {
            this.client.scrollRowIntoView(record);
        }
    }

    /**
     * Waits for height transition on the provided rows element. Then calls provided function.
     * @private
     * @category Internal
     */
    waitForTransition(row, fn) {
        EventHelper.onTransitionEnd({
            element  : row.element,
            property : 'height',
            handler  : fn,
            thisObj  : this,
            duration : DomHelper.getPropertyTransitionDuration(row.element, 'height') ?? 1
        });
    }

    removeExpander(row, destroyWidget) {
        row.cls.remove(this.expandedRowClass);

        for (const region of this.client.regions) {
            const rowElement = row.getElement(region);

            for (const child of rowElement.querySelectorAll('.' + this.expanderBodyClass)) {
                destroyWidget && child.widget?.destroy();
                this.resizeObserver.unobserve(child);
                child.remove();
            }

            // If this function is called after animation finished, we need to remove class `manually`
            rowElement.classList.remove(this.expandedRowClass);

            this.lockCellHeight(rowElement, null, false);
        }
    }

    renderLoadingIndicator(rowElement, recordState) {
        recordState.loadingIndicators.push(DomHelper.createElement({
            parent    : rowElement,
            className : this.expanderBodyClass + ' b-rowexpander-loading',
            style     : {
                top    : recordState.cellHeight,
                height : this.loadingIndicatorHeight
            },
            children : [
                {
                    tag       : 'i',
                    className : 'b-icon b-icon-spinner'
                },
                this.loadingIndicatorText
            ]
        }));
    }

    /**
     * Creates expander element for each grid region and calls the renderer, also for each grid region.
     * @private
     * @param {Core.data.Model} record
     * @param {Grid.row.Row} row
     * @param {Object} recordState
     * @category Internal
     */
    renderExpander(record, row, recordState) {
        const
            me                                   = this,
            { client : grid, widget, dataField } = me,
            cellHeight                           = row.cells[0]?.offsetHeight,
            { expandedBodyElements = {} }        = recordState,
            renderPromises                       = [],
            // Will be called sync or async depending on the implementation of the renderer function.
            continueRendering                    = (content, expanderElement, region) => {
                if (content != null) {
                    if (typeof content === 'string') {
                        expanderElement.innerHTML = content;
                    }
                    else if (content.type && !content.tag) {
                        createWidget(content, expanderElement);
                    }
                    // Everything else will be treated as a dom config for now
                    else {
                        content = DomHelper.createElement(content);
                        expanderElement.appendChild(content);
                    }
                }
                expandedBodyElements[region] = expanderElement;
            },
            createWidget = (widgetConfig, expanderElement) => {
                const
                    themeName           = DomHelper.getThemeInfo()?.name,
                    shadowRootContainer = DomHelper.createElement({
                        parent    : expanderElement,
                        className : me.shadowRootContainerClass,
                        style     : 'flex : 1'
                    }),
                    shadowRoot = shadowRootContainer._shadowRoot = shadowRootContainer.attachShadow({ mode : 'closed' });

                renderPromises.push(DomHelper.cloneStylesIntoShadowRoot(shadowRoot).then(() => {
                    if (grid.isDestroyed) {
                        return;
                    }

                    if (dataField) {
                        const fieldData = record.getValue(dataField);
                        // This path is used if field is a StoreDataField
                        if (fieldData?.isStore) {
                            widgetConfig.store = fieldData;
                        }
                        else if (grid.store[`${dataField}Store`]) {
                            const relatedStore = grid.store[`${dataField}Store`];
                            widgetConfig.store = relatedStore.chain(r => record.getValue(dataField).includes(r));
                        }
                        else {
                            widgetConfig.data = fieldData;
                        }
                    }

                    if (themeName) {
                        const
                            { cls }  = widgetConfig,
                            themeCls = `b-theme-${themeName.toLowerCase()}`;

                        widgetConfig.cls = cls ? cls + ' ' + themeCls : themeCls;
                    }

                    recordState.widget = expanderElement.widget = Widget.create(ObjectHelper.assign({
                        appendTo  : shadowRoot,
                        owner     : grid,
                        flex      : 1,
                        minHeight : '5em',
                        isNested  : true
                    }, widgetConfig));

                    if (dataField) {
                        // If we have created a store, refresh expanded row on store changes
                        recordState.widget.store.ion({
                            change  : () => !row.isDestroyed && row.render(),
                            thisObj : me
                        });
                    }

                }).catch((href) => {
                    throw new Error('Could not load stylesheet ' + href);
                }));
            };

        // If another rendering of the same record is made while waiting for async, we should ignore it.
        if (recordState.isRenderingAsync) {
            return;
        }

        Object.assign(recordState, { cellHeight, expandedBodyElements, expanderBodyHeight : 0, loadingIndicators : [] });

        for (const region of grid.regions) {
            const
                rowElement = row.getElement(region);
            let expanderBodyElement = expandedBodyElements[region];

            // class needed at this point to give the expander container correct height
            row.addCls(me.expandedRowClass);

            if (!expanderBodyElement) {
                // Create expand container
                // Expander element needs to be in the DOM for appendTo to work correctly
                expanderBodyElement = DomHelper.createElement({
                    parent    : rowElement,
                    tabIndex  : -1,
                    className : me.expanderBodyClass,
                    style     : {
                        top : cellHeight + 'px'
                    }
                });

                me.resizeObserver.observe(expanderBodyElement);
            }

            let renderResponse;

            if (widget) {
                createWidget(widget, expanderBodyElement);
            }
            else {
                // The renderer can be async or sync
                renderResponse = me.renderer({
                    record, expanderElement : expanderBodyElement, rowElement, region, grid
                });
            }

            if (Objects.isPromise(renderResponse)) {
                renderPromises.push(renderResponse.then(content => continueRendering(content, expanderBodyElement, region)));
            }
            else {
                continueRendering(renderResponse, expanderBodyElement, region);
            }
        }

        // If we have async renderer, wait for all to complete
        if (renderPromises.length) {
            recordState.isRenderingAsync = true;
            Promise.all(renderPromises).then(() => {
                // One of the promises (createWidget) can, while resolving, add another promise to the array
                // That's why we need to do this twice
                Promise.all(renderPromises).then(() => {
                    if (grid.isDestroyed) {
                        return;
                    }
                    // Flag that indicates the completion of expand rendering
                    recordState.isCreated = true;
                    // Remove loading indicator
                    recordState.loadingIndicators?.forEach(li => li.remove());
                    recordState.loadingIndicators.length = 0;

                    recordState.ignoreResize = false;
                    recordState.isRenderingAsync = false;

                    // Initiate a render if all current states is created, this code should be executed once for each
                    // state
                    for (const [, state] of me.recordStateMap) {
                        if (!state.isCreated) {
                            return;
                        }
                    }

                    // (?. since we might have been destroyed while waiting for promises)
                    me.renderRowsWithAnimation?.(record, true);

                });
            });
        }
        else {
            // Sync rendering
            recordState.isCreated = true;
        }
    }

    /**
     * Called when grid rows needs to re-render, for example on expand or collapse.
     * Activates animations on grid, and deactivates them when they are completed.
     * @private
     * @param {Core.data.Model} record Record whose row was toggled
     * @category Internal
     */
    renderRowsWithAnimation(record) {
        const me = this;

        if (me.enableAnimations) {
            const row = me.client.rowManager.getRowById(record);

            if (row) {
                me.isAnimating = true;

                if (me.collapsingStateMap.has(record)) {
                    row.addCls('b-row-is-collapsing');
                }

                me.waitForTransition(row, () => {
                    me.isAnimating = false;
                    if (!row.isDestroyed) {
                        row.removeCls?.('b-row-is-collapsing');
                    }
                });
            }
        }
        return me.bufferedRenderer(record);
    }

    /**
     * Collects a rendering call for each record, saves them in array and calls the delayed (RAF) rafRenderer function
     * @private
     * @param {Core.data.Model} record Record whose row was toggled
     * @category Internal
     */
    bufferedRenderer(record) {
        (this._bufferedRecords ?? (this._bufferedRecords = [])).push(record);

        if (!this._rafPromise) {
            this._rafPromise = new Promise(resolve => {
                requestAnimationFrame(() => {
                    this.internalRender?.(resolve);
                    this._rafPromise = null;
                });
            });
        }

        return this._rafPromise;
    }

    /**
     * Re-renders the grid from the topmost record of those saved in bufferedRenderer
     * @private
     * @category Internal
     */
    internalRender(resolvePromise) {
        const
            me                   = this,
            { _bufferedRecords } = me,
            { store }            = me.client;

        me.recordStateMap.forEach((state, record) => {
            if (state.renderPromiseResolver && state.isCreated && !_bufferedRecords.includes(record)) {
                _bufferedRecords.push(record);
            }
        });

        const [top] = _bufferedRecords.sort((a, b) => store.indexOf(a) - store.indexOf(b));

        me.client.rowManager.renderFromRecord(top);
        _bufferedRecords.length = 0;

        // So that rendering is completed when promises are resolved
        me.delay(resolvePromise);
    }

    /**
     * Called when row is expanded. This function locks all cell's height to current height (before expanding).
     * @private
     * @param {HTMLElement} rowElement
     * @param {Number} cellHeight The height to lock
     * @param {Boolean} unlock To remove locked cell height when the row is collapsed
     * @category Internal
     */
    lockCellHeight(rowElement, cellHeight, unlock) {
        for (let a = 0; a < rowElement.children.length; a++) {
            const child = rowElement.children[a];
            // Should not lock expander element
            if (!child.classList.contains(this.expanderBodyClass)) {
                child.style.height = unlock ? '' : cellHeight + 'px';
            }
        }
    }

    //endregion

    //region Public

    /**
     * Tells the RowExpander that the provided record should be expanded. If or when the record is rendered into view,
     * the record will be expanded.
     *
     * Promise will resolve when the row gets expanded. Note that this can be much later than the actual expand call,
     * depending on response times and if current record is in view or not.
     *
     * @param {Core.data.Model} record Record whose row should be expanded
     * @category Common
     */
    async expand(record, fromSplit = false) {
        const me = this;

        if (me.disabled || me.recordStateMap.has(record) || await me.trigger('beforeExpand', { record }) === false) {
            return;
        }

        let recordState;

        return new Promise((resolve) => {
            recordState = {
                isCreated             : false,
                renderPromiseResolver : resolve
            };
            // Tells renderer that this record should be expanded
            me.recordStateMap.set(record, recordState);

            // In the event that we have expanded a record which is in collapsing animation state
            me.collapsingStateMap.delete(record);
            me._shouldScrollIntoView = true;

            me.renderRowsWithAnimation(record);

            // Propagate to splits
            if (!fromSplit) {
                me.client.syncSplits?.(other => other.features.rowExpander.expand(record, true));
            }

        }).then(() => {
            me.trigger?.('expand', {
                record,
                expandedElements : recordState.expandedBodyElements,
                widget           : recordState.widget
            });
        });
    }

    /**
     * Tells the RowExpander that the provided record should be collapsed. If the record is in view, it will be
     * collapsed. If the record is not in view, it will simply not be expanded when rendered into view.
     *
     * @param {Core.data.Model} record Record whose row should be collapsed
     * @category Common
     */
    async collapse(record, fromSplit = false) {
        const
            me          = this,
            recordState = me.recordStateMap.get(record);

        if (me.disabled || await me.trigger('beforeCollapse', { record }) === false) {
            return;
        }

        // Unobserve resize
        if (recordState?.expandedBodyElements) {
            for (const region in recordState.expandedBodyElements) {
                me.resizeObserver.unobserve(recordState.expandedBodyElements[region]);
            }
        }

        me.recordStateMap.delete(record);
        me.collapsingStateMap.set(record, recordState);

        await me.renderRowsWithAnimation(record);

        me.trigger('collapse', { record });

        // Propagate to splits
        if (!fromSplit) {
            me.client.syncSplits?.(other => other.features.rowExpander.collapse(record, true));
        }
    }

    //endregion

    // region Nested navigation

    // Overrides the original, hence the if statement
    navigateDown() {
        if (!this.onKeyboardIn()) {
            return this.overridden.navigateDown(...arguments);
        }
    }

    // Chains the original
    navigateUp() {
        this.onKeyboardIn(true);
    }

    // Detects if focus is being reverted here by a nested grid, and focuses either the expanded row or the row below
    catchFocus({ navigationDirection, source, editing }) {
        if (this.widget) {
            const
                { client }      = this,
                { focusedCell } = client;

            for (let [record, state] of this.recordStateMap.entries()) {
                for (const body in state.expandedBodyElements) {
                    if (state.expandedBodyElements[body].widget === source) {
                        if (navigationDirection === 'down') {
                            record = client.store.getNext(record, undefined, true);
                        }

                        let column;
                        // If a column has been navigated to earlier, focus that
                        if (!editing && focusedCell && !focusedCell._isDefaultFocus) {
                            column = focusedCell.column;
                        }
                        // Else, get best candidate
                        else {
                            column = this.getNavigateableColumn(client, true, editing && navigationDirection === 'up');
                        }

                        // In case the cell we want to revert to is the one that was focused previously
                        client._focusedCell = null;

                        const cellContext = client.normalizeCellContext({ record, column });

                        client.focusCell(cellContext);

                        if (editing) {
                            client.startEditing(cellContext);
                        }

                        return true;
                    }
                }
            }
        }
    }

    // Detects if the user keyboard navigates either from the expanded row and down, or the row below the expanded row
    // and up. If so, and there is a Grid in the expanded body, it starts to keyboard navigate there
    onKeyboardIn(up) {
        if (this.widget) {
            const
                { focusedCell } = this.client,
                state           = focusedCell && this.recordStateMap.get(focusedCell.record),
                widget          = state?.expandedBodyElements?.[focusedCell?.column?.region]?.widget;

            if (widget?.isGrid) {
                let column;
                // If a column has been navigated to earlier, focus that
                if (widget.focusedCell && !widget.focusedCell._isDefaultFocus) {
                    column = widget.focusedCell.column;
                }
                // Else, get the best possible candidate
                else {
                    column = this.getNavigateableColumn(widget);
                }

                // In case the cell we want to revert to is the one that was focused previously
                widget._focusedCell = null;

                widget.focusCell(widget.normalizeCellContext({ record : widget[`${up ? 'last' : 'first'}VisibleRow`], column }));

                return true;
            }
        }
        return false;
    }

    /**
     * Get the first column that is not the `checkboxSelectionColumn` and not the expander column.
     * @param grid
     * @param editable Also checks that the column has an `editor`
     * @param reverse If `true`, this functions returns the last column which meets the requirements
     * @private
     */
    getNavigateableColumn(grid, editable = true, reverse = false) {
        const columns = reverse ? [...grid.columns.visibleColumns].reverse() : grid.columns.visibleColumns;

        return columns.find(c =>
            c !== grid.checkboxSelectionColumn &&
            c !== grid.features.rowExpander?._expander &&
            (!editable || c.editor)
        ) ?? columns[0];
    }

    isActionAvailable({ action }) {
        if (this.isDisabled) {
            return false;
        }

        if (['rowExpander.onTab', 'rowExpander.onShiftTab'].includes(action)) {
            return this.client.features.cellEdit?.isEditing;
        }
        // Return true to let customized actions through
        return true;
    }

    onTab(previous) {
        const
            { client }   = this,
            { cellEdit } = client.features;

        if (cellEdit?.enabled) {
            const
                next             = previous !== true,
                { activeRecord } = cellEdit,
                nextCell         = cellEdit.getAdjacentEditableCell(client.focusedCell, next),
                expandedRecord   = next ? activeRecord : (nextCell ? client.store.getById(nextCell.id) : null),
                widget           = expandedRecord && this.recordStateMap.get(expandedRecord)?.widget;

            if (activeRecord?.id !== nextCell?.id && widget?.features.cellEdit?.enabled) {
                cellEdit.finishEditing().then(() => {
                    const
                        record = widget[`${next ? 'first' : 'last'}VisibleRow`],
                        column = this.getNavigateableColumn(widget, true, !next);

                    widget.startEditing(widget.normalizeCellContext({ record, column }));
                });
                return true;
            }

        }

        // KeyMap continues to call action handlers for this shortcut
        return false;
    }

    onShiftTab() {
        return this.onTab(true);
    }

    // endregion

    doDestroy() {
        this.resizeObserver?.disconnect();
        delete this.resizeObserver;

        // destroy any nested widgets
        for (const [, state] of this.recordStateMap) {
            state.widget?.destroy();
        }

        super.doDestroy();
    }

    onThemeChange({ prev, theme }) {
        for (const [, entry] of this.recordStateMap) {
            Object.values(entry.expandedBodyElements).forEach(bodyElement => {
                const shadowRootContainer = bodyElement.querySelector('.' + this.shadowRootContainerClass);
                if (shadowRootContainer?._shadowRoot) {
                    DomHelper.cloneStylesIntoShadowRoot(shadowRootContainer?._shadowRoot, true);
                    bodyElement.widget?.element?.classList.remove(`b-theme-${prev}`);
                    bodyElement.widget?.element?.classList.add(`b-theme-${theme}`);
                }
            });
        }
    }

    /**
     * Gets the corresponding expanded record from either a nested widget or an element in the expanded body.
     * @param {HTMLElement|Core.widget.Widget} elementOrWidget
     * @returns {Core.data.Model}
     */
    getExpandedRecord(elementOrWidget) {
        for (const [rec, obj] of this.recordStateMap.entries()) {
            if (elementOrWidget.isWidget && obj.widget) {
                if (obj.widget === elementOrWidget) {
                    return rec;
                }
            }
            else {
                const { expandedBodyElements } = obj;

                for (const region in expandedBodyElements) {
                    const curEl = expandedBodyElements[region];

                    if (curEl === elementOrWidget || curEl.contains(elementOrWidget)) {
                        return rec;
                    }
                }
            }
        }
        return null;
    }
}

GridFeatureManager.registerFeature(RowExpander);
