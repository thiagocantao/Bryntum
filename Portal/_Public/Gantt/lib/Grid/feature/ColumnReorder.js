import DomHelper from '../../Core/helper/DomHelper.js';
import DragHelper from '../../Core/helper/DragHelper.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import Delayable from '../../Core/mixin/Delayable.js';
import Widget from '../../Core/widget/Widget.js';
import ScrollManager from '../../Core/util/ScrollManager.js';

/**
 * @module Grid/feature/ColumnReorder
 */

/**
 * Allows user to reorder columns by dragging headers. To get notified about column reorder listen to `change` event
 * on {@link Grid.data.ColumnStore columns} store.
 *
 * {@inlineexample Grid/feature/ColumnReorder.js}
 *
 * This feature is <strong>enabled</strong> by default.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Grid/columns
 * @classtype columnReorder
 * @feature
 */
export default class ColumnReorder extends Delayable(InstancePlugin) {
    //region Init

    static $name = 'ColumnReorder';

    ignoreSelectors = [
        '.b-grid-header-resize-handle',
        '.b-field'
    ];

    // Plugin configuration. This plugin chains some functions in Grid
    static pluginConfig = {
        after : ['onPaint', 'renderContents']
    };

    /**
     * Initialize drag & drop (called from render)
     * @private
     */
    init() {
        const
            me         = this,
            { grid }   = me,
            gridEl     = grid.element,
            containers = DomHelper.children(gridEl, '.b-grid-headers');

        containers.push(...DomHelper.children(gridEl, '.b-grid-header-children'));

        if (me.dragHelper) {
            // update the dragHelper with the new set of containers it should operate upon
            me.dragHelper.containers = containers;
        }
        else {
            // When using state provider in Salesforce we get into init before component is rendered
            // which makes it impossible to locate correct root node. This is why we set up drag helper on paint
            // https://github.com/bryntum/support/issues/6998
            grid.whenVisible(() => me.createDragHelper());
        }
    }

    createDragHelper() {
        const
            me         = this,
            { grid }   = me,
            gridEl     = grid.element,
            containers = DomHelper.children(gridEl, '.b-grid-headers');

        containers.push(...DomHelper.children(gridEl, '.b-grid-header-children'));

        me.dragHelper = new DragHelper({
            name                       : 'columnReorder',
            mode                       : 'container',
            dragThreshold              : 10,
            targetSelector             : '.b-grid-header',
            floatRootOwner             : grid,
            rtlSource                  : grid,
            outerElement               : grid.headerContainer,
            // Require that we drag inside grid header while dragging if we don't have a drag toolbar or external drop
            // target defined
            dropTargetSelector         : '.b-grid-headers, .b-groupbar, .b-columndragtoolbar',
            externalDropTargetSelector : '.b-groupbar, .b-columndragtoolbar',

            monitoringConfig : {
                scrollables : [{
                    element : '.b-grid-headers'
                }]
            },
            scrollManager : ScrollManager.new({
                direction : 'horizontal',
                element   : grid.headerContainer
            }),
            containers,
            isElementDraggable(element) {
                const abort = Boolean(element.closest(me.ignoreSelectors.join(',')));

                if (abort || me.disabled) {
                    return false;
                }

                const
                    columnEl = element.closest(this.targetSelector),
                    column   = columnEl && grid.columns.getById(columnEl.dataset.columnId),
                    isLast   = column?.childLevel === 0 && grid.subGrids[column.region].columns.count === 1;



                return Boolean(column) && column.draggable !== false && !isLast;
            },
            ignoreSelector    : '.b-filter-icon,.b-grid-header-resize-handle',
            internalListeners : {
                beforeDragStart : me.onBeforeDragStart,
                dragstart       : me.onDragStart,
                drag            : me.onDrag,
                drop            : me.onDrop,
                abort           : me.onAbort,
                thisObj         : me
            }
        });

        me.relayEvents(me.dragHelper, ['dragStart', 'drag', 'drop', 'abort'], 'gridHeader');

        return me.dragHelper;
    }

    //endregion

    //region Plugin config

    doDestroy() {
        this.dragHelper?.scrollManager.destroy();
        this.dragHelper?.destroy();

        super.doDestroy();
    }

    get grid() {
        return this.client;
    }

    //endregion

    //region Events (drop)

    onBeforeDragStart({ context, event }) {
        const
            me          = this,
            { element } = context,
            column      = context.column = me.client.columns.getById(element.dataset.columnId);

        me.dragHelper.autoSizeClonedTarget = !me.usingGroupBarWidget;

        /**
         * This event is fired prior to starting a column drag gesture. The drag is canceled if a listener returns `false`.
         * @on-owner
         * @event beforeColumnDragStart
         * @param {Grid.view.Grid} source The grid instance.
         * @param {Grid.column.Column} column The dragged column.
         * @param {Event} event The browser event.
         * @preventable
         */
        return column.allowDrag && me.client.trigger('beforeColumnDragStart', { column, event });
    }

    onDragStart({ context, event }) {
        const
            me                            = this,
            { grid, usingGroupBarWidget } = me,
            { column, dragProxy }                    = context;

        if (!grid.features.columnDragToolbar && !usingGroupBarWidget) {
            const headerContainerBox = grid.element.querySelector('.b-grid-header-container').getBoundingClientRect();

            me.dragHelper.minY = headerContainerBox.top;
            me.dragHelper.maxY = headerContainerBox.bottom;
        }

        grid.element.classList.add('b-dragging-header');

        if (usingGroupBarWidget) {
            dragProxy.classList.add('b-grid-reordering-columns-with-groupbar');
        }
        // Hide compact filter field while dragging
        if (grid.features.filterBar && grid.features.filterBar.compactMode) {
            dragProxy.classList.add('b-filter-bar-compact');
        }
        dragProxy.style.fontSize = DomHelper.getStyleValue(context.element, 'fontSize');

        /**
         * This event is fired when a column drag gesture has started.
         * @on-owner
         * @event columnDragStart
         * @param {Grid.view.Grid} source The grid instance.
         * @param {Grid.column.Column} column The dragged column.
         * @param {Event} event The browser event.
         */
        grid.trigger('columnDragStart', { column, event });
    }

    onDrag({ context, event }) {
        const
            me        = this,
            grid                     = me.client,
            { column, insertBefore : insertBeforeElement } = context,
            insertBefore                                   = grid.columns.getById(insertBeforeElement?.dataset.columnId),
            targetHeader = Widget.fromElement(event.target, 'gridheader');

        // If SubGrid is configured with a sealed column set, do not allow moving into it
        if (targetHeader?.subGrid.sealedColumns) {
            context.valid = false;
        }

        /**
         * This event is fired when a column is being dragged, and you can set the `valid` flag on t
         * @event columnDrag
         * @on-owner
         * @param {Grid.view.Grid} source The grid instance.
         * @param {Grid.column.Column} column The dragged column.
         * @param {Grid.column.Column} insertBefore The column before which the dragged column will be inserted.
         * @param {Event} event The browser event.
         * @param {Object} context
         * @param {Boolean} context.valid Set this to true or false to indicate whether the drop position is valid.
         */
        grid.trigger('columnDrag', { column, insertBefore, event, context });
    }

    /**
     * Handle drop
     * @private
     */
    onDrop({ context, event }) {
        if (!context.valid) {
            return this.onInvalidDrop({ context, event });
        }

        const
            me              = this,
            { grid }        = me,
            { column }      = context,
            element         = context.dragging,
            onHeader        = context.target.closest('.b-grid-header'),
            droppedInRegion = context.draggedTo?.dataset.region,
            isReorder       = droppedInRegion || onHeader;

        let vetoed, newParent, insertBefore, toRegion, oldParent;

        grid.element.classList.remove('b-dragging-header');

        // Regular grid column reorder
        if (isReorder) {
            // If dropping on right edge of grid-headers element, append to that subgrid
            const
                onColumn = onHeader ? grid.columns.get(onHeader.dataset.column) : grid.subGrids[droppedInRegion].columns.last,
                sibling  = context.insertBefore;

            toRegion     = droppedInRegion || onColumn.region;
            oldParent    = column.parent;
            insertBefore = sibling ? grid.columns.getById(sibling.dataset.columnId) : grid.subGrids[toRegion].columns.last.nextSibling;

            if (insertBefore) {
                newParent = insertBefore.parent;
            }
            else {
                const groupNode = onHeader?.parentElement.closest('.b-grid-header');

                if (groupNode) {
                    newParent = grid.columns.getById(groupNode.dataset.columnId);
                }
                else {
                    newParent = grid.columns.rootNode;
                }
            }

            // If dropped into its current position in the same SubGrid - abort
            vetoed = (toRegion === column.region && oldParent === newParent && (onColumn === column.previousSibling || insertBefore === column.nextSibling));
            element.remove();
        }

        // Clean up element used during drag drop as it will not be removed by Grid when it refreshes its header elements

        /**
         * This event is fired when a column is dropped, and you can return false from a listener to abort the operation.
         * @event beforeColumnDropFinalize
         * @on-owner
         * @param {Grid.view.Grid} source The grid instance.
         * @param {Grid.column.Column} column The dragged column.
         * @param {Grid.column.Column} insertBefore The column before which the dragged column will be inserted.
         * @param {Grid.column.Column} newParent The new parent column.
         * @param {Event} event The browser event.
         * @param {String} region The region where the column was dropped.
         * @preventable
         */
        vetoed = vetoed || grid.trigger('beforeColumnDropFinalize', {
            column, newParent, insertBefore, event, region : toRegion
        }) === false;

        if (!vetoed && isReorder) {
            // Insert the column into its new place, which might be vetoed if column is sealed
            vetoed = !newParent.insertChild(column, insertBefore);
        }

        context.valid = !vetoed;

        if (!vetoed && isReorder) {
            column.region = toRegion;

            // Check if we should remove last child
            if (oldParent.children.length === 0) {
                oldParent.parent.removeChild(oldParent);
            }
        }

        /**
         * This event is always fired after a column is dropped. The `valid` param is true if the operation was not
         * vetoed and the column was moved in the column store.
         * @event columnDrop
         * @on-owner
         * @param {Grid.view.Grid} source The grid instance.
         * @param {Grid.column.Column} column The dragged column.
         * @param {Grid.column.Column} insertBefore The column before which the dragged column will be inserted.
         * @param {Grid.column.Column} newParent The new parent column.
         * @param {Boolean} valid true if the operation was not vetoed.
         * @param {Event} event The browser event.
         * @param {String} region The region where the column was dropped.
         * @preventable
         */
        grid.trigger('columnDrop', { column, newParent, insertBefore, valid : context.valid, event, region : toRegion });
    }

    onAbort({ context, event }) {
        this.onInvalidDrop({ context, event });
    }

    /**
     * Handle invalid drop
     * @private
     */
    onInvalidDrop({ context, event }) {
        const
            { grid }   = this,
            { column } = context;

        grid.trigger('columnDrop', { column, valid : false, event });
        grid.element.classList.remove('b-dragging-header');
    }

    //endregion

    //region Render

    /**
     * Updates DragHelper with updated headers when grid contents is rerendered
     * @private
     */
    renderContents() {
        // columns shown, hidden or reordered
        this.init();
    }

    /**
     * Initializes this feature on grid paint.
     * @private
     */
    onPaint() {
        // always reinit on paint
        this.init();
    }

    /**
     * Returns true if a reorder operation is active
     * @property {Boolean}
     * @readonly
     */
    get isReordering() {
        return Boolean(this.dragHelper?.isDragging);
    }

    //endregion
}

ColumnReorder.featureClass = 'b-column-reorder';

GridFeatureManager.registerFeature(ColumnReorder, true);
