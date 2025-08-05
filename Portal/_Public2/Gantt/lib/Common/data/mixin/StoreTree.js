import Base from '../../Base.js';

/**
 * @module Common/data/mixin/StoreTree
 */

/**
 * Mixin for store with tree related functionality. To learn more about working tree nodes please see the {@link Common/data/mixin/TreeNode} class and [this guide](#guides/data/treedata.md).
 * @mixin
 */
export default Target => class StoreTree extends (Target || Base) {
    //region Getters

    /**
     * True if this Store is configured to handle tree data (with `tree : true`).
     * @property {Boolean}
     * @readonly
     * @category Tree
     */
    get isTree() {
        return this.tree;
    }

    /**
     * Get all leaves in store with tree data
     * @returns {Common.data.Model[]}
     * @category Tree
     */
    get leaves() {
        const me = this,
            result = [];

        if (me.tree) {
            me.traverse(record => {
                if (record.isLeaf) {
                    result.push(record);
                }
            });

            return result;
        }
        else {
            me.allRecords.forEach(r => {
                if (r.isLeaf) {
                    result.push(r);
                }
                r.traverse(record => {
                    if (record.isLeaf) {
                        result.push(record);
                    }
                }, true);
            });
        }
        return result;
    }

    //endregion

    //region Children

    /**
     * Loads children for a parent node that uses load on demand (when expanding it). Base implementation does nothing,
     * either use AjaxStore which implements it, create your own subclass with an implementation or listen for
     * `toggleNode` and insert records when you have them available.
     * @param {Common.data.Model} parentRecord
     */
    async loadChildren(parentRecord) {

    }

    /**
     * Called from Model when adding children. Not to be called directly, use Model#appendChild() instead.
     * @internal
     * @param {Common.data.mixin.TreeNode} parent
     * @param {Common.data.mixin.TreeNode[]} children
     * @param {Number} index
     * @param {Object} isMove
     * @param {Boolean} [silent]
     * @fires add
     * @fires change
     */
    onNodeAddChild(parent, children, index, isMove, silent) {
        const me = this,
            isRootLoad = parent === me.rootNode && parent.isLoading,
            { storage } = me,
            toAddToUI = [],
            toAdd = [],
            previousSibling = children[0].previousSibling;

        let storeInsertionPoint;

        me.collectDescendants(children, toAddToUI, toAdd, !(parent.isExpanded(me) && parent.ancestorsExpanded(me)));

        // Keep CRUD caches up to date unless it's a root load
        if (!isRootLoad && toAdd.length) {
            for (const record of toAdd) {
                // Only considered an add if not modified or moved
                if (!me.modified.includes(record) && !isMove[record.id]) {
                    me.added.add(record);
                    me.removed.remove(record);
                }
            }
        }

        // Root node inserted first
        if (isRootLoad && me.rootVisible) {
            toAddToUI.unshift(parent);
            toAdd.unshift(parent);
        }

        if (toAddToUI.length) {
            // Calculate the insertion point into the flat store.
            // If the new node is the first, then it goes after the parent node.
            if (index === 0 || !previousSibling) {
                storeInsertionPoint = storage.indexOf(parent);
            }
            // Otherwise it has to go after the previous visible node which has
            // to be calculated. See indexOfPreviousVisibleNode for explanation.
            else {
                storeInsertionPoint = storage.indexOf(previousSibling) + previousSibling.getDescendantCount(true, me);
            }

            // Insert added child nodes at correct location in storage.
            // We must not react to change - we fire the events here.
            storage.suspendEvents();
            me.storage.splice(++storeInsertionPoint, 0, toAddToUI);
            storage.resumeEvents();
        }

        // Since we do not pass through Store#onDataChange we have to handle relations manually here. And since they are
        // not tied to flat part of store, use all children
        me.updateDependentStores('add', children);

        // If it's a root level set data op, then signal 'dataset'
        if (isRootLoad && toAddToUI.length) {
            // If we have initial sorters, perform a silent sort before triggering `dataset`
            // NOTE: Records in toAddToUI will be in the original order, not affected by the sort
            if (me.sorters.length) {
                me.sort(null, null, false, true);
            }

            if (!silent) {
                const event = { action : 'dataset', data : me._data, records : toAddToUI };
                me.trigger('refresh', event);
                me.trigger('change', event);
            }
        }
        // Else, continue as before to signal a bizarre "isChild" add.
        else if (!silent) {
            const event = { action : 'add', parent, isChild : true, isMove, records : children, allRecords : toAdd, index : storeInsertionPoint };
            me.trigger('add', event);
            me.trigger('change', event);
        }
    }

    onNodeRemoveChild(parent, children, index, isMove) {
        const me = this,
            { storage } = me,
            toRemoveFromUI = [],
            toRemove = [];

        me.collectDescendants(children, toRemoveFromUI, toRemove, !(parent.isExpanded(me) && parent.ancestorsExpanded(me)));

        if (!isMove) {
            // Unjoin is recursive, use flat children array
            for (let record of children) {
                record.unJoinStore(me);
            }

            // Keep CRUD caches up to date
            if (toRemove.length) {
                for (let record of toRemove) {
                    if (record.stores.includes(me)) {
                        record.unJoinStore(me);
                    }

                    // If was newly added, remove from added list
                    if (me.added.includes(record)) {
                        me.added.remove(record);
                    }
                    // Else add to removed list
                    else {
                        me.removed.add(record);
                    }
                }
                me.modified.remove(toRemove);
            }
        }

        // Remove removed child nodes at correct location in storage
        if (toRemoveFromUI.length) {
            index = storage.indexOf(toRemoveFromUI[0]);
            // We must not react to change - we fire the events here.
            if (index > -1) {
                storage.suspendEvents();
                storage.splice(index, toRemoveFromUI.length);
                storage.resumeEvents();
            }
        }
        else {
            // If nothing is removed from UI (storage) return -1, showing that removed node was in the collapsed branch
            index = -1;
        }

        const event = { action : 'remove', parent, isChild : true, isMove, records : children, allRecords : toRemove, index };
        me.trigger('remove', event);
        me.trigger('change', event);
    }

    collectDescendants(node, visible = [], all = [], inCollapsedBranch = false) {
        const me = this,
            children = Array.isArray(node) ? node : node.children;

        if (children) {
            for (let i = 0, len = children.length, child; i < len; i++) {
                child = children[i];
                if (!inCollapsedBranch) {
                    visible.push(child);
                }
                all.push(child);
                me.collectDescendants(child, visible, all, inCollapsedBranch || !child.isExpanded(me));
            }
        }
        return { visible, all };
    }

    /**
     * Includes or excludes all records beneath parentRecord in storage. Used when expanding or collapsing
     * nodes.
     * @private
     * @param parentRecord Parent record
     * @param include Include (true) or exclude (false)
     * @category Tree
     */
    internalToggleTreeSubRecords(parentRecord, include) {
        const me              = this,
            { storage }     = me,
            index           = storage.indexOf(parentRecord),
            children = [];

        let excludeCount    = 0,
            parentCollapsed = -1;

        parentRecord.traverse(child => {
            const isExpanded = child.isExpanded(me),
                mapMeta = child.instanceMeta(me.id);

            // at new subparent at the same (or lower) level as previous, reset collapsed check
            if (parentCollapsed && child.childLevel <= parentCollapsed) parentCollapsed = -1;
            // records under already collapsed parent wont be processed
            if (parentCollapsed === -1) {
                // at a parent and it is collapsed, store its level for collapsed check
                if (!child.isLeaf && !isExpanded) parentCollapsed = child.childLevel;

                if (include) {
                    // if including subrecords, add those who are not hidden by a collapsed sub parent
                    children.push(child);
                }
                else if (!mapMeta.hidden) {
                    children.push(child);
                    // excluding, only need to count how many visible we have
                    excludeCount++;
                }
            }
            mapMeta.hidden = !include;
        }, true);

        // If we expanded a node which is yet to load children, the collected children
        // array will be empty, so do not broadcast any change event.
        // If we are collapsing a record which isn't visible (because parent is collapsed) we won't get an index,
        // which is fine since it is already removed from processedRecords
        if (children.length && index !== false) {
            // We must not react to change - we fire the events here with a flag
            // to tell responders that it's due to an expoand or collapse.
            storage.suspendEvents();

            if (include) {
                storage.splice(index + 1, 0, ...children);

                const event = { action : 'add', isExpand : true, records : children, index : index + 1 };
                me.trigger('add', event);
                me.trigger('change', event);
            }
            else {
                storage.splice(index + 1, excludeCount);

                const event = { action : 'remove', isCollapse : true, records : children, index : index + 1 };
                me.trigger('remove', event);
                me.trigger('change', event);
            }
            storage.resumeEvents();
        }
    }

    /**
     * Remove all records beneath parentRecord from storage.
     * @private
     * @param parentRecord Parent record
     * @category Tree
     */
    onNodeCollapse(parentRecord) {
        return this.internalToggleTreeSubRecords(parentRecord, false);
    }

    /**
     * Add all records beneath parentRecord from storage.
     * @private
     * @param parentRecord Parent record
     * @category Tree
     */
    onNodeExpand(parentRecord) {
        return this.internalToggleTreeSubRecords(parentRecord, true);
    }

    //endregion
};
