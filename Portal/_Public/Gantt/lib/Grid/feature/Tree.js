import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import Delayable from '../../Core/mixin/Delayable.js';
import '../column/TreeColumn.js';

/**
 * @module Grid/feature/Tree
 */

const immediatePromise = Promise.resolve();

/**
 * Feature that makes the grid work more like a tree. Included by default in {@link Grid.view.TreeGrid}. Requires
 * exactly one {@link Grid.column.TreeColumn} among grids columns. That column will have its renderer replaced with a
 * tree renderer that adds padding and icon to give the appearance of a tree. The original renderer is preserved and
 * also called.
 *
 * {@inlineexample Grid/feature/Tree.js}
 *
 * This feature is <strong>disabled</strong> by default. When enabled, the feature cannot be disabled during runtime.
 *
 * ## Keyboard shortcuts
 * This feature has the following default keyboard shortcuts:
 *
 * | Keys                 | Action                   | Action description                                                   |
 * |----------------------|--------------------------|----------------------------------------------------------------------|
 * | `Space`              | *toggleCollapseByKey*    | When focus on a parent node, this expands or collapses it's children |
 * | `ArrowRight`         | *expandIfSingleColumn*   | Expands a focused parent node if grid consist of one column only     |
 * | `Shift`+`ArrowRight` | *expandByKey*            | Expands a focused parent node                                        |
 * | `ArrowLeft`          | *collapseIfSingleColumn* | Collapses a focused parent node if grid consist of one column only   |
 * | `Shift`+`ArrowLeft`  | *collapseByKey*          | Collapses a focused parent node                                      |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md)
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/tree
 * @classtype tree
 * @feature
 */
export default class Tree extends InstancePlugin.mixin(Delayable) {
    //region Config

    static $name = 'Tree';

    static configurable = {
        /**
         * Expand parent nodes when clicking on their cell
         * @prp {Boolean}
         * @default
         */
        expandOnCellClick : false,

        /**
         * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
         * @config {Object<String,String>}
         */
        keyMap : {
            ' '                : 'toggleCollapseByKey',
            ArrowRight         : 'expandIfSingleColumn',
            'Shift+ArrowRight' : 'expandByKey',
            ArrowLeft          : 'collapseIfSingleColumn',
            'Shift+ArrowLeft'  : 'collapseByKey'
        }
    };

    // Plugin configuration. This plugin chains some functions in Grid.
    static get pluginConfig() {
        return {
            assign : ['collapseAll', 'expandAll', 'collapse', 'expand', 'expandTo', 'toggleCollapse'],
            chain  : ['onElementPointerUp', 'onElementClick', 'bindStore']
        };
    }

    //endregion

    //region Init

    construct(client, config) {
        super.construct(client, config);

        // find column
        if (!this.treeColumn) {
            console.info('To use the tree feature, one column should be configured with `type: \'tree\'`');
        }

        client.store && this.bindStore(client.store);
    }

    doDisable(disable) {
        if (disable) {
            throw new Error('Tree feature cannot be disabled');
        }
    }

    get store() {
        return this.client.store;
    }

    get treeColumn() {
        const
            me          = this,
            { columns } = me.client;

        if (!me._treeColumn || !columns.includes(me._treeColumn)) {
            me._treeColumn = columns.find(column => column.isTreeColumn);
        }

        return me._treeColumn;
    }

    bindStore(store) {
        this.detachListeners('store');

        store.ion({
            name                  : 'store',
            beforeLoadChildren    : 'onBeforeLoadChildren',
            loadChildren          : 'onLoadChildren',
            loadChildrenException : 'onLoadChildrenException',
            beforeToggleNode      : 'onBeforeToggleNode',
            thisObj               : this
        });
    }

    //endregion

    //region Expand & collapse

    /**
     * Collapse an expanded node or expand a collapsed. Optionally forcing a certain state.
     * This function is exposed on Grid and can thus be called as `grid.toggleCollapse()`
     * @param {String|Number|Core.data.Model} idOrRecord Record (the node itself) or id of a node to toggle
     * @param {Boolean} [collapse] Force collapse (true) or expand (false)
     * @on-owner
     * @category Tree
     */
    async toggleCollapse(idOrRecord, collapse) {
        if (idOrRecord == null) {
            throw new Error('Tree#toggleCollapse must be passed a record');
        }

        const
            me                = this,
            { store, client } = me,
            { rowManager }    = client,
            record            = store.getById(idOrRecord),
            meta              = record.instanceMeta(store);

        // Record generation is incremented to force React/Vue wrappers to recreate UI elements
        record.generation++;

        if (await store.toggleCollapse(record, collapse)) {
            const row = rowManager.getRowFor(record);

            if (row && record.ancestorsExpanded()) {
                const cellElement = me.treeColumn && !me.treeColumn.subGrid.collapsed && row.getCell(me.treeColumn.id);

                // Toggle cell's expanded/collapsed state
                cellElement && row.renderCell(cellElement);
            }

            // Add a temporary cls, used by Scheduler & Gantt to prevent transitions on events/tasks
            // Block multiple applications in the case of a recursive collapseAll operation
            if (!me.isTogglingNode) {
                client.element.classList.add('b-toggling-node');
                me.isTogglingNode = true;
                me.requestAnimationFrame(() => {
                    client.element.classList.remove('b-toggling-node');
                    me.isTogglingNode = false;
                });
            }

            /**
             * Fired before a parent node record is collapsed.
             * @event collapseNode
             * @param {Grid.view.Grid} source The firing Grid instance.
             * @param {Core.data.Model} record The record which has been collapsed.
             * @on-owner
             */

            /**
             * Fired after a parent node record is expanded.
             * @event expandNode
             * @param {Grid.view.Grid} source The firing Grid instance.
             * @param {Core.data.Model} record The record which has been expanded.
             * @on-owner
             */

            client.trigger(meta.collapsed ? 'collapseNode' : 'expandNode', { record });

            /**
             * Fired after a parent node record toggles its collapsed state.
             * @event toggleNode
             * @param {Core.data.Model} record The record being toggled.
             * @param {Boolean} collapse `true` if the node is being collapsed.
             * @on-owner
             */

            client.trigger('toggleNode', { record, collapse : meta.collapsed });
        }
    }

    /**
     * Collapse a single node.
     * This function is exposed on Grid and can thus be called as `grid.collapse()`
     * @param {String|Number|Core.data.Model} idOrRecord Record (the node itself) or id of a node to collapse
     * @on-owner
     * @category Tree
     */
    async collapse(idOrRecord) {
        return this.toggleCollapse(idOrRecord, true);
    }

    /**
     * Expand a single node.
     * This function is exposed on Grid and can thus be called as `grid.expand()`
     * @param {String|Number|Core.data.Model} idOrRecord Record (the node itself) or id of a node to expand
     * @on-owner
     * @category Tree
     */
    async expand(idOrRecord) {
        return this.toggleCollapse(idOrRecord, false);
    }

    onBeforeToggleNode({ record, collapse }) {
        /**
         * Fired before a parent node record toggles its collapsed state.
         * @event beforeToggleNode
         * @param {Grid.view.Grid} source The firing Grid instance.
         * @param {Core.data.Model} record The record being toggled.
         * @param {Boolean} collapse `true` if the node is being collapsed.
         * @on-owner
         */
        this.client.trigger('beforeToggleNode', { record, collapse });
    }

    onBeforeLoadChildren({ source : store, params }) {
        const
            parent = store.getById(params[store.modelClass.idField]),
            row    = this.client.rowManager.getRowFor(parent);

        row?.addCls('b-loading-children');
    }

    onLoadChildren({ source : store, params }) {
        const
            parent = store.getById(params[store.modelClass.idField]),
            row    = this.client.rowManager.getRowFor(parent);

        row?.removeCls('b-loading-children');
    }

    onLoadChildrenException({ record }) {
        const row = this.client.rowManager.getRowFor(record);

        row?.removeCls('b-loading-children');
    }

    /**
     * Expand or collapse all nodes, as specified by param, starting at the passed node (which defaults to the root node)
     * @param {Boolean} [collapse] Set to true to collapse, false to expand (defaults to true)
     * @param {Core.data.Model} [topNode] The topmost node from which to cascade a collapse.
     * Defaults to the {@link Core.data.Store#property-rootNode}. Not included in the cascade if
     * the root node is being used.
     * @category Tree
     */
    async expandOrCollapseAll(collapse = true, topNode = this.store.rootNode) {

        const
            { client, store } = this,
            promises          = [],
            childRecords      = [];

        client.trigger('beforeToggleAllNodes', { collapse });

        // Each collapse/expand will trigger events on store, avoid that by suspending
        store.suspendEvents();
        store.traverse(record => {
            const gridMeta = record.instanceMeta(store);
            if (!record.isLeaf) {
                if (collapse && !gridMeta.collapsed) {
                    this.toggleCollapse(record, true);
                    childRecords.push(...record.children);
                }
                else if (!collapse && gridMeta.collapsed) {
                    if (Array.isArray(record.children)) {
                        childRecords.push(...record.children);
                    }
                    promises.push(this.toggleCollapse(record, false));
                }
            }
        }, topNode, topNode === store.rootNode);
        store.resumeEvents();

        return (collapse ? immediatePromise : Promise.all(promises)).then(() => {
            // Return to top when collapsing all
            client.refreshRows(collapse);

            if (childRecords.length) {
                if (collapse) {
                    store.trigger('remove', { records : childRecords, isCollapse : true, isCollapseAll : true });
                }
                else {
                    store.trigger('add', { records : childRecords, isExpand : true, isExpandAll : true });
                }
            }

            client.trigger('toggleAllNodes', { collapse });
        });
    }

    /**
     * Collapse all nodes.
     * This function is exposed on Grid and can thus be called as `grid.collapseAll()`
     * @on-owner
     * @category Tree
     */
    async collapseAll() {
        return this.expandOrCollapseAll(true);
    }

    /**
     * Expand all nodes.
     * This function is exposed on Grid and can thus be called as `grid.expandAll()`
     * @on-owner
     * @category Tree
     */
    async expandAll() {
        return this.expandOrCollapseAll(false);
    }

    /**
     * Expands parent nodes to make this node "visible".
     * This function is exposed on Grid and can thus be called as `grid.expandTo()`
     * @param {String|Number|Core.data.Model|String[]|Number[]|Core.data.Model[]} idOrRecord Record (the node itself),
     * or id of a node. Also accepts arrays of the same types.
     * @param {Boolean} [scrollIntoView=true] A flag letting you control whether to scroll the record into view
     * @on-owner
     * @async
     * @category Tree
     */
    async expandTo(idOrRecord, scrollIntoView = true) {
        const
            me                = this,
            { store, client } = me;

        if (Array.isArray(idOrRecord)) {
            if (idOrRecord.length > 0) {
                client.suspendRefresh();
                for (let i = idOrRecord.length - 1; i >= 0; i--) {
                    const record = store.getById(idOrRecord[i]);

                    if (i === 0) {
                        client.resumeRefresh?.();
                        // Ensure all parents are rendered
                        client.rowManager.refresh();
                    }

                    await me.expandTo?.(record, i === 0);
                }
            }

            return;
        }

        const record = store.getById(idOrRecord);

        // Hidden because it's in a collapsed Group: abort
        if (record.instanceMeta(me.store).hiddenByCollapse === false) {
            return;
        }

        // Expand any parents that need to be expanded to allow the record to be rendered.
        if (!record.ancestorsExpanded()) {
            const parents = [];

            // Collect parents which need expanding
            for (let parent = record.parent; parent && !parent.isRoot; parent = parent.parent) {
                if (!parent.isExpanded(store)) {
                    parents.unshift(parent);
                }
            }

            client.suspendRefresh();

            // Expand them from the top-down
            for (const parent of parents) {
                if (!me.isDestroyed) {
                    await me.toggleCollapse(parent, false);
                }
            }

            client.resumeRefresh?.();

            // Refreshing on expand was inhibited in toggleCollapse calls
            client.refreshRows?.();
        }

        if (!me.isDestroyed && scrollIntoView) {
            await client.scrollRowIntoView(record);
        }
    }

    //endregion

    //region Events

    /**
     * Called when user clicks somewhere in the grid. Expand/collapse node on icon click.
     * @private
     */
    onElementPointerUp(event) {
        const
            me              = this,
            target          = event.target,
            cellData        = me.client.getCellDataFromEvent(event),
            clickedExpander = target.closest('.b-tree-expander');

        // Checks if click is on node expander icon, then toggles expand/collapse. Also toggles on entire cell if expandOnCellClick is true
        if (clickedExpander || (me.expandOnCellClick && cellData?.record.isParent)) {
            me.toggleCollapse(cellData.record);
        }
    }

    onElementClick(event) {
        // Prevent default to avoid triggering navigation if the tree node is a link
        if (event.target.closest('.b-tree-expander')) {
            event.preventDefault();
        }
    }

    /**
     * Called on key down in grid. Expand/collapse node on [space]
     * @private
     */
    toggleCollapseByKey() {
        const { focusedCell } = this.client;

        // Only catch space on grid cell element, not in header, editors etc...
        if (focusedCell?.rowIndex > -1 && !focusedCell.isActionable) {
            this.toggleCollapse(focusedCell.id);
            return true;
        }

        return false;
    }

    //endregion

    // Expands tree if single column.
    // Called by default on ArrowRight
    expandIfSingleColumn() {
        if (this.client.columns.count === 1) {
            return this.expandByKey();
        }

        // Tells keymap to continue with other actions
        return false;
    }

    // Expands tree on Shift+ArrowRight by default.
    expandByKey() {
        const
            me              = this,
            { client }      = me,
            { focusedCell } = client,
            record          = focusedCell?.record;

        // shift triggers tree navigation behaviour, also used by default for single column which is tree
        if (record && focusedCell?.column.tree && record.isParent && record.instanceMeta(client.store).collapsed) {
            me.expand(record);
            return true;
        }

        // Tells keymap to continue with other actions
        return false;
    }

    collapseIfSingleColumn() {
        if (this.client.columns.count === 1) {
            return this.collapseByKey();
        }

        // Tells keymap to continue with other actions
        return false;
    }

    collapseByKey() {
        const
            me              = this,
            { client }      = me,
            { focusedCell } = client,
            record          = focusedCell?.record;

        // shift triggers tree navigation behaviour, also used by default for single column which is tree
        if (focusedCell?.column.tree && record) {
            // on expanded parent, collapse
            if (record.isParent && !record.instanceMeta(client.store).collapsed) {
                me.collapse(record);
                return true;
            }
            // otherwise go to parent
            if (record.parent && !record.parent.isRoot) {
                // Deselect everything before doing this.
                // Causes strange selection ranges otherwise
                client.deselectAll();
                client.focusCell({
                    record : record.parent,
                    column : focusedCell.column
                });
                return true;
            }
        }

        // Tells keymap to continue with other actions
        return false;
    }
}

Tree.featureClass = 'b-tree';

GridFeatureManager.registerFeature(Tree, false, 'Grid');
GridFeatureManager.registerFeature(Tree, true, 'TreeGrid');
