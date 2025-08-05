import ResizeHelper from '../../Core/helper/ResizeHelper.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import BrowserHelper from '../../Core/helper/BrowserHelper.js';

/**
 * @module Grid/feature/ColumnResize
 */

/**
 * Enables user to resize columns by dragging a handle on the right hand side of the header. To get notified about column
 * resize listen to `change` event on {@link Grid.data.ColumnStore columns} store.
 *
 * This feature is <strong>enabled</strong> by default.
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/columns
 * @classtype columnResize
 * @inlineexample Grid/feature/ColumnResize.js
 * @feature
 */
export default class ColumnResize extends InstancePlugin {

    static get $name() {
        return 'ColumnResize';
    }

    static get configurable() {
        return {
            /**
             * Resize all cells below a resizing header during dragging.
             * `'auto'` means `true` on non-mobile platforms.
             * @config {String|Boolean}
             * @default
             */
            liveResize : 'auto'
        };
    }

    //region Init

    construct(grid, config) {
        const me = this;

        me.grid = grid;

        super.construct(grid, config);

        me.resizer = new ResizeHelper({
            name              : 'columnResize',
            targetSelector    : '.b-grid-header',
            handleSelector    : '.b-grid-header-resize-handle',
            outerElement      : grid.element,
            rtlSource         : grid,
            internalListeners : {
                beforeresizestart : me.onBeforeResizeStart,
                resizestart       : me.onResizeStart,
                resizing          : me.onResizing,
                resize            : me.onResize,
                cancel            : me.onCancel,
                thisObj           : me
            }
        });
    }

    doDestroy() {
        this.resizer?.destroy();
        super.doDestroy();
    }

    //endregion

    changeLiveResize(liveResize) {
        if (liveResize === 'auto') {
            return !BrowserHelper.isMobileSafari;
        }
        return liveResize;
    }

    //region Events

    onBeforeResizeStart() {
        return !this.disabled;
    }

    onResizeStart({ context }) {
        const
            { grid, resizer } = this,
            column            = context.column = grid.columns.getById(context.element.dataset.columnId);

        resizer.minWidth = column.minWidth;

        grid.element.classList.add('b-column-resizing');
    }

    /**
     * Handle drag event - resize the column live unless it's a touch gesture
     * @private
     */
    onResizing({ context }) {
        if (context.valid && this.liveResize) {
            this.grid.resizingColumns = true;
            context.column.width = context.newWidth;
        }
    }

    /**
     * Handle drop event (only used for touch)
     * @private
     */
    onResize({ context }) {
        const
            { grid } = this,
            { column } = context;

        grid.element.classList.remove('b-column-resizing');

        if (context.valid) {
            if (this.liveResize) {
                grid.resizingColumns = false;
                grid.afterColumnsResized(column);
            }
            else {
                column.width = context.newWidth;
            }
        }
    }

    /**
     * Restore column width on cancel (ESC)
     * @private
     */
    onCancel({ context }) {
        const { grid } = this;

        grid.element.classList.remove('b-column-resizing');

        context.column.width = context.elementWidth;
        grid.resizingColumns = false;
    }

    //endregion
}

GridFeatureManager.registerFeature(ColumnResize, true);
