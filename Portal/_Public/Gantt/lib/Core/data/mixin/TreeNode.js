import Base from '../../Base.js';
import ArrayHelper from '../../helper/ArrayHelper.js';

/**
 * @module Core/data/mixin/TreeNode
 */

const
    defaultTraverseOptions = {
        includeFilteredOutRecords : false
    },
    fixTraverseOptions     = options => {
        options = options || false;

        if (typeof options === 'boolean') {
            options = {
                includeFilteredOutRecords : options
            };
        }

        return options || defaultTraverseOptions;
    };

/**
 * Mixin for Model with tree node related functionality. This class is mixed into the {@link Core/data/Model} class.
 *
 * ## Adding and removing child nodes
 *
 * ```javascript
 * const parent = store.getById(1),
 *
 * firstBorn = parent.insertChild({
 *     name : 'Child node'
 * }, parent.children[0]); // Insert a child at a specific place in the children array
 *
 * parent.removeChild(parent.children[0]); // Removes a child node
 * parent.appendChild({ name : 'New child node' }); // Appends a child node
 * ```
 *
 * @mixin
 */
export default Target => class TreeNode extends (Target || Base) {
    static get $name() {
        return 'TreeNode';
    }

    /**
     * This static configuration option allows you to control whether an empty parent task should be converted into a
     * leaf. Enable/disable it for a whole class:
     *
     * ```javascript
     * Model.convertEmptyParentToLeaf = false;
     * ```
     *
     * By specifying `true`, all empty parents will be considered leafs. Can also be assigned a configuration object
     * with the following Boolean properties to customize the behaviour:
     *
     * ```javascript
     * Model.convertEmptyParentToLeaf = {
     *     onLoad   : false,
     *     onRemove : true
     * }
     * ```
     *
     * @member {Boolean|{ onLoad : Boolean, onRemove : Boolean }} convertEmptyParentToLeaf
     * @property {Boolean} onLoad Apply the transformation on load to any parents without children (`children : []`)
     * @property {Boolean} onRemove Apply the transformation when all children have been removed from a parent
     * @default false
     * @static
     * @category Parent & children
     * */
    static set convertEmptyParentToLeaf(value) {
        if (value === true) {
            value = {
                onLoad   : true,
                onRemove : true
            };
        }
        else if (value === false) {
            value = {
                onLoad   : false,
                onRemove : false
            };
        }
        this._convertEmptyParentToLeaf = value;
    }

    constructor(...args) {
        super(...args);

        if (this.children) {
            this.orderedChildren = this.orderedChildren || [];
        }
    }

    static get convertEmptyParentToLeaf() {
        return this._convertEmptyParentToLeaf || { onLoad : false, onRemove : false };
    }

    /**
     * This is a read-only property providing access to the parent node.
     * @member {Core.data.Model} parent
     * @readonly
     * @category Parent & children
     */

    /**
     * This is a read-only field provided in server synchronization packets to specify
     * which record id is the parent of the record.
     * @readonly
     * @field {String|Number|null} parentId
     * @category Tree
     */

    /**
     * This is a read-only field provided in server synchronization packets to specify
     * which position the node takes in the parent's children array.
     * This index is set on load and gets updated automatically after row reordering, sorting, etc.
     * To save the order, need to persist the field on the server and when data is fetched to be loaded,
     * need to sort by this field.
     * @readonly
     * @field {Number} parentIndex
     * @category Tree
     */

    /**
     * This is a read-only field provided in server synchronization packets to specify
     * which position the node takes in the parent's ordered children array.
     * This index is set on load and gets updated on reordering nodes in tree. Sorting and filtering
     * have no effect on it.
     * @readonly
     * @field {Number} orderedParentIndex
     * @category Tree
     */

    ingestChildren(childRecord, stores = this.stores) {
        const
            { inProcessChildren, constructor : MyClass } = this,
            store                                        = stores[0];

        if (childRecord === true) {
            if (inProcessChildren) {
                return true;
            }
            return [];
        }
        if (childRecord) {
            childRecord = ArrayHelper.asArray(childRecord);

            const
                len    = childRecord.length,
                result = [];

            for (let i = 0, child; i < len; i++) {
                child = childRecord[i];
                child = child.isModel ? child : (store ? store.createRecord(child, false, true) : new MyClass(child, null, null, true));
                child = store ? store.processRecord(child) : child;
                result.push(child);
            }

            if (this.children === true && store) {
                const sorter = store.createSorterFn(store.sorters);
                result.sort(sorter);
            }
            return result;
        }
    }

    /**
     * Child nodes. To allow loading children on demand, specify `children : true` in your data. Omit the field for leaf
     * tasks.
     *
     * Note, if the tree store loads data from a remote origin, make sure {@link Core/data/AjaxStore#config-readUrl}
     * is specified, and optionally {@link Core/data/AjaxStore#config-parentIdParamName} is set, otherwise
     * {@link Core/data/Store#function-loadChildren} has to be implemented.
     *
     * @field {Boolean|Object[]|Core.data.Model[]} children
     * @category Parent & children
     */

    /**
     * Array of sorted tree nodes but without a filter applied
     * @member {Core.data.Model[]|null} unfilteredChildren
     * @category Parent & children
     * @private
     */

    /**
     * Array of children unaffected by sorting and filtering, keeps original tree structure
     * @member {Core.data.Model[]|null} orderedChildren
     * @category Parent & children
     * @private
     */

    /**
     * Called during creation to also turn any children into Models joined to the same stores as this model
     * @internal
     * @category Parent & children
     */
    processChildren(stores = this.stores) {
        const
            me       = this,
            { meta } = me;

        me.inProcessChildren = true;

        const children = me.ingestChildren(me.data[me.constructor.childrenField], stores);

        if (children) {
            const
                { convertEmptyParentToLeaf } = me.constructor,
                shouldConvert                = convertEmptyParentToLeaf === true || convertEmptyParentToLeaf.onLoad;

            if (shouldConvert ? children.length : Array.isArray(children)) {
                meta.isLeaf = false;
                // We are processing a remote load
                if (me.children === true) {
                    me.children = [];
                }
                else if (children.length === 0) {
                    me.children = children;
                    return;
                }
                me.insertChild(children);
            }
            // Flagged for load on demand
            else if (children === true) {
                meta.isLeaf = false;
                me.children = true;
            }
            // Empty child array, flag is leaf if configured to do so
            else if (!me.isRoot) {
                meta.isLeaf = me.constructor.convertEmptyParentToLeaf.onLoad;
            }
        }

        me.inProcessChildren = false;
    }

    /**
     * This method returns `true` if this record has all expanded ancestors and is therefore
     * eligible for inclusion in a UI.
     * @param {Core.data.Store} [store] Optional store, defaults to nodes first store
     * @returns {Boolean}
     * @readonly
     * @category Parent & children
     * @returns {Boolean}
     */
    ancestorsExpanded(store = this.firstStore) {
        const { parent } = this;

        return !parent || (parent.isExpanded(store) && parent.ancestorsExpanded(store));
    }

    /**
     * Used by stores to assess the record's collapsed/expanded state in that store.
     * @param {Core.data.Store} store
     * @category Parent & children
     * @returns {Boolean}
     */
    isExpanded(store = this.firstStore) {
        const mapMeta = this.instanceMeta(store.id);

        // Default initial expanded/collapsed state when in the store
        // to the record's original expanded property.
        if (!Object.prototype.hasOwnProperty.call(mapMeta, 'collapsed')) {
            mapMeta.collapsed = !this.expanded;
        }

        return !mapMeta.collapsed;
    }

    // A read-only property. It provides the initial state upon load
    // The UI's expanded/collapsed state is in the store's meta map.
    get expanded() {
        return this.data.expanded;
    }

    /**
     * Depth in the tree at which this node exists. First visual level of nodes are at level 0, their direct children at
     * level 1 and so on.
     * @property {Number}
     * @readonly
     * @category Parent & children
     */
    get childLevel() {
        let node = this,
            ret  = -1;

        while (node && !node.isRoot) {
            ++ret;
            node = node.parent;
        }

        return ret;
    }

    /**
     * Is a leaf node in a tree structure?
     * @property {Boolean}
     * @readonly
     * @category Parent & children
     */
    get isLeaf() {
        return this.meta.isLeaf !== false && !this.isRoot;
    }

    /**
     * Returns `true` if this node is the root of the tree
     * @member {Boolean} isRoot
     * @readonly
     * @category Parent & children
     */

    /**
     * Is a parent node in a tree structure?
     * @property {Boolean}
     * @readonly
     * @category Parent & children
     */
    get isParent() {
        return !this.isLeaf;
    }

    /**
     * Returns true for parent nodes with children loaded (there might still be no children)
     * @property {Boolean}
     * @readonly
     * @category Parent & children
     */
    get isLoaded() {
        return this.isParent && Array.isArray(this.children);
    }

    /**
     * Count all children (including sub-children) for a node (in its `firstStore´)
     * @member {Number}
     * @category Parent & children
     */
    get descendantCount() {
        return this.getDescendantCount();
    }

    /**
     * Count visible (expanded) children (including sub-children) for a node (in its `firstStore`)
     * @member {Number}
     * @category Parent & children
     */
    get visibleDescendantCount() {
        return this.getDescendantCount(true);
    }

    /**
     * Count visible (expanded)/all children for this node, optionally specifying for which store.
     * @param {Boolean} [onlyVisible] Specify `true` to only count visible (expanded) children.
     * @param {Core.data.Store} [store] A Store to which this node belongs
     * @returns {Number}
     * @category Parent & children
     */
    getDescendantCount(onlyVisible = false, store = this.firstStore) {
        const { children } = this;

        if (!children || !Array.isArray(children) || (onlyVisible && !this.isExpanded(store))) {
            return 0;
        }

        return children.reduce((count, child) => count + child.getDescendantCount(onlyVisible), children.length);
    }

    /**
     * Retrieve all children, not including filtered out nodes (by traversing sub nodes)
     * @property {Core.data.Model[]}
     * @category Parent & children
     */
    get allChildren() {
        return this.getAllChildren(false);
    }

    /**
     * Retrieve all children, including filtered out nodes (by traversing sub nodes)
     * @property {Core.data.Model[]}
     * @private
     * @category Parent & children
     */
    get allUnfilteredChildren() {
        return this.getAllChildren(true);
    }

    getAllChildren(unfiltered = false) {
        const { [unfiltered ? 'unfilteredChildren' : 'children']: children } = this;

        if (!children || children === true) {
            return [];
        }

        return children.reduce((all, child) => {
            all.push(child);

            // push.apply is faster than push with array spread:
            // https://jsperf.com/push-apply-vs-push-with-array-spread/1
            all.push.apply(all, unfiltered ? child.allUnfilteredChildren : child.allChildren);
            return all;
        }, []);
    }

    /**
     * Get the first child of this node
     * @property {Core.data.Model}
     * @readonly
     * @category Parent & children
     */
    get firstChild() {
        const { children } = this;

        return (children?.length && children[0]) || null;
    }

    /**
     * Get the last child of this node
     * @property {Core.data.Model}
     * @readonly
     * @category Parent & children
     */
    get lastChild() {
        const { children } = this;

        return (children?.length && children[children.length - 1]) || null;
    }

    /**
     * Get the previous sibling of this node
     * @member {Core.data.Model} previousSibling
     * @readonly
     * @category Parent & children
     */

    /**
     * Get the next sibling of this node
     * @member {Core.data.Model} nextSibling
     * @readonly
     * @category Parent & children
     */

    /**
     * Returns count of all preceding sibling nodes (including their children).
     * @property {Number}
     * @category Parent & children
     */
    get previousSiblingsTotalCount() {
        let task  = this.previousSibling,
            count = this.parentIndex;

        while (task) {
            count += task.descendantCount;
            task = task.previousSibling;
        }

        return count;
    }

    get previousOrderedSibling() {
        return this.parent?.orderedChildren[this.orderedParentIndex - 1];
    }

    get nextOrderedSibling() {
        return this.parent?.orderedChildren[this.orderedParentIndex + 1];
    }

    get root() {
        return this.parent?.root || this;
    }

    /**
     * Reading this property returns the id of the parent node, if this record is a child of a node.
     *
     * Setting this property appends this record to the record with the passed id **in the same store that this record
     * is already in**.
     *
     * Note that setting this property is **only valid if this record is already part of a tree store**.
     *
     * This is not intended for general use. This is for when a server responds to a record mutation and the server
     * decides to move a record to a new parent. If a `parentId` property is passed in the response data for a record,
     * that record will be moved.
     *
     * @property {Number|String|null}
     * @category Parent & children
     */
    get parentId() {
        return this.parent && !this.parent.isAutoRoot ? this.parent.id : null;
    }

    set parentId(parentId) {
        const
            me         = this,
            { parent } = me,
            newParent  = parentId === null ? me.firstStore.rootNode : me.firstStore.getById(parentId);

        // Handle exact equality of parent.
        // Also handle one being null and the other being undefined meaning no change.
        if (!(newParent === parent || (!parent && !newParent))) {
            // If we are batching, we do not trigger a change immediately.
            // endBatch will set the field which will set the property again.
            if (me.isBatchUpdating) {
                me.meta.batchChanges.parentId = parentId;
            }
            else {
                if (newParent) {
                    newParent.appendChild(me);
                }
                else {
                    me.parent.removeChild(me);
                }
            }
        }
    }

    static set parentIdField(parentIdField) {
        // Maintainer: the "this" references in here reference two different contexts.
        // Outside of the property definition, it's the Model Class.
        // In the getter and setter, it's the record instance.
        this._parentIdField = parentIdField;

        Object.defineProperty(this.prototype, parentIdField, {
            set : function(parentId) {
                // no arrow functions here, need `this` to change to instance
                // noinspection JSPotentiallyInvalidUsageOfClassThis
                this.parentId = parentId;
            },
            get : function() {
                // no arrow functions here, need `this` to change to instance
                // noinspection JSPotentiallyInvalidUsageOfClassThis
                return this.parentId;
            }
        });
    }

    static get parentIdField() {
        return this._parentIdField || 'parentId';
    }

    getChildren(options) {
        let result;

        if (options.includeFilteredOutRecords) {
            result = this.unfilteredChildren || this.children;
        }
        else if (options.useOrderedTree) {
            result = this.orderedChildren;
        }
        else {
            result = this.children;
        }

        return result;
    }

    /**
     * Traverses all child nodes recursively calling the passed function
     * on a target node **before** iterating the child nodes.
     * @param {Function} fn The function to call
     * @param {Boolean} [skipSelf=false] True to ignore self
     * @param {Object|Boolean} [options] A boolean for includeFilteredOutRecords, or an options object
     * @param {Boolean} [options.includeFilteredOutRecords] True to also include filtered out records
     * @param {Boolean} [options.useOrderedTree] True to traverse unsorted/unfiltered tree
     * @category Parent & children
     */
    traverse(fn, skipSelf, options) {
        options = fixTraverseOptions(options);

        const
            me       = this,
            children = me.getChildren(options);

        if (!skipSelf) {
            fn.call(me, me);
        }

        // Simply testing whether there is non-zero children length
        // is 10x faster than using this.isLoaded
        for (let i = 0, l = children?.length; i < l; i++) {
            children[i].traverse(fn, false, options);
        }
    }

    /**
     * Traverses all child nodes recursively calling the passed function
     * on child nodes of a target **before** calling it on the node.
     * @param {Function} fn The function to call
     * @param {Boolean} [skipSelf=false] True to skip this node in the traversal
     * @param {Object|Boolean} [options] A boolean for includeFilteredOutRecords, or an options object
     * @param {Boolean} [options.includeFilteredOutRecords] True to also include filtered out records
     * @category Parent & children
     */
    traverseBefore(fn, skipSelf, options) {
        options = fixTraverseOptions(options);

        const
            me       = this,
            children = me.getChildren(options);

        // Simply testing whether there is non-zero children length
        // is 10x faster than using me.isLoaded
        for (let i = 0, l = children?.length; i < l; i++) {
            children[i].traverse(fn, false, options);
        }

        if (!skipSelf) {
            fn.call(me, me);
        }
    }

    /**
     * Traverses child nodes recursively while fn returns true
     * @param {Function} fn
     * @param {Boolean} [skipSelf=false] True to skip this node in the traversal
     * @param {Object|Boolean} [options] A boolean for includeFilteredOutRecords, or an options object
     * @param {Boolean} [options.includeFilteredOutRecords] True to also include filtered out records
     * @category Parent & children
     * @returns {Boolean}
     */
    traverseWhile(fn, skipSelf, options) {
        options = fixTraverseOptions(options);

        const me = this;

        let goOn = skipSelf || fn.call(me, me) !== false;

        if (goOn) {
            const children = me.getChildren(options);

            // Simply testing whether there is non-zero children length
            // is 10x faster than using me.isLoaded
            if (children?.length) {
                goOn = children.every(child => child.traverseWhile(fn, false, options));
            }
        }

        return goOn;
    }

    /**
     * Bubbles up from this node, calling the specified function with each node.
     *
     * @param {Function} fn The function to call for each node
     * @param {Boolean} [skipSelf] True to skip this node in the traversal
     * @category Parent & children
     */
    bubble(fn, skipSelf = false) {
        let me = this;

        if (!skipSelf) {
            fn.call(me, me);
        }

        while (me.parent) {
            me = me.parent;
            fn.call(me, me);
        }
    }

    /**
     * Bubbles up from this node, calling the specified function with each node,
     * while the function returns true.
     *
     * @param {Function} fn The function to call for each node
     * @param {Boolean} [skipSelf] True to skip this node in the traversal
     * @category Parent & children
     * @returns {Boolean}
     */
    bubbleWhile(fn, skipSelf = false) {
        let me   = this,
            goOn = true;

        if (!skipSelf) {
            goOn = fn.call(me, me);
        }

        while (goOn && me.parent) {
            me   = me.parent;
            goOn = fn.call(me, me);
        }

        return goOn;
    }

    /**
     * Checks if this model contains another model as one of it's descendants
     *
     * @param {Core.data.Model|String|Number} childOrId child node or id
     * @param {Boolean} [skipSelf=false] True to ignore self in the traversal
     * @category Parent & children
     * @returns {Boolean}
     */
    contains(childOrId, skipSelf = false) {
        if (childOrId && typeof childOrId === 'object') {
            childOrId = childOrId.id;
        }
        return !this.traverseWhile(node => node.id != childOrId, skipSelf);
    }

    getTopParent(all) {
        let result;

        if (all) {
            result = [];
            this.bubbleWhile((t) => {
                result.push(t);
                return t.parent && !t.parent.isRoot;
            });
        }
        else {
            result = null;
            this.bubbleWhile((t) => {
                result = t;

                return t.parent && !t.parent.isRoot;
            });
        }

        return result;
    }

    /**
     * Append a child record(s) to any current children.
     * @param {Core.data.Model|Core.data.Model[]|Object|Object[]} childRecord Array of records/data or a single
     * record/data to append
     * @param {Boolean} [silent] Pass `true` to not trigger events during append
     * @returns {Core.data.Model|Core.data.Model[]|null}
     * @category Parent & children
     */
    appendChild(childRecord, silent = false) {
        return this.insertChild(childRecord, null, silent);
    }

    /**
     * Insert a child record(s) before an existing child record.
     * @param {Core.data.Model|Core.data.Model[]|Object|Object[]} childRecord Array of records/data or a single
     * record/data to insert
     * @param {Core.data.Model} [before] Optional record to insert before, leave out to append to the end
     * @param {Boolean} [silent] Pass `true` to not trigger events during append
     * @returns {Core.data.Model|Core.data.Model[]|null}
     * @category Parent & children
     */
    insertChild(childRecord, before = null, silent = false, options = {}) {
        const
            me          = this,
            returnArray = Array.isArray(childRecord);

        childRecord = ArrayHelper.asArray(childRecord);

        if (typeof before === 'number') {
            before = me.children?.[before] ?? null;
        }

        if (!silent && !me.stores.every(s => s.trigger('beforeAdd', {
            records : childRecord, parent : me
        }) !== false)) {
            return null;
        }

        // This call makes child record an array containing Models
        childRecord = me.ingestChildren(childRecord);

        // NOTE: see comment in Model::set() about before/in/after calls approach.
        const
            index     = before?.parentIndex ?? me.children?.length ?? 0,
            preResult = me.beforeInsertChild?.(childRecord),
            inserted  = me.internalAppendInsert(childRecord, before, silent, options);

        // Turn into a parent if not already one
        if (inserted.length) {
            me.convertToParent(silent);
        }

        me.afterInsertChild?.(index, childRecord, preResult, inserted);

        return (returnArray || !inserted) ? inserted : inserted[0];
    }

    /**
     * Converts a leaf node to a parent node, assigning an empty array as its children
     * @param {Boolean} [silent] Pass `true` to not trigger any event
     * @category Parent & children
     */
    convertToParent(silent = false) {
        const
            me      = this,
            wasLeaf = me.isLeaf;

        me.meta.isLeaf = false;

        if (!me.children) {
            me.children = [];
        }

        // Signal a change event so that the UI updates, unless it is during load in which case StoreTree#onNodeAddChild
        // will handle it
        if (wasLeaf && !me.root.isLoading && !silent) {
            me.signalNodeChanged({
                isLeaf : {
                    value    : false,
                    oldValue : true
                }
            });
        }
    }

    signalNodeChanged(changes, stores = this.stores) {
        stores.forEach(s => {
            s.trigger('update', { record : this, records : [this], changes });
            s.trigger('change', { action : 'update', record : this, records : [this], changes });
        });
    }

    tryInsertChild() {
        return this.insertChild(...arguments);
    }

    internalAppendInsert(recordsToInsert, beforeRecord, silent, options) {
        const
            me                         = this,
            { stores, root, children } = me,
            { firstStore : rootStore } = root,
            { parentIdField }          = me.constructor,
            parentId                   = me.id;

        let isNoop, start, i, newRecordsCloned, oldParentIndices, isMove;

        if (!root.isLoading && rootStore) {
            // Only collect this info if not loading, to not produce garbage
            isMove = {};
            oldParentIndices = [];

            for (i = 0; i < recordsToInsert.length; i++) {
                const newRecord = recordsToInsert[i];

                // Store added should not be modified for adds
                // caused by moving.
                isMove[newRecord.id] = newRecord.root === root;
                oldParentIndices[i]  = newRecord.parentIndex;
            }
        }

        // The reference node must be one of our children. If not, fall back to an append.
        if (beforeRecord && beforeRecord.parent !== me) {
            beforeRecord = null;
        }

        // If the records starting at insertAt or (insertAt - 1), are the same sequence
        // that we are being asked to add, this is a no-op.
        if (children) {
            const insertAt = beforeRecord ? beforeRecord.parentIndex : children.length;

            if (children[start = insertAt] === recordsToInsert[0] || children[start = insertAt - 1] === recordsToInsert[0]) {
                for (isNoop = true, i = 0; isNoop && i < recordsToInsert.length; i++) {
                    if (recordsToInsert[i] !== children[start + i]) {
                        isNoop = false;
                    }
                }
            }
        }

        // Fulfill the contract of appendChild/insertChild even if we did not have to do anything.
        // Callers must be able to correctly postprocess the returned value as an array.
        if (isNoop) {
            return recordsToInsert;
        }

        // Remove incoming child nodes from any current parent.
        for (i = 0; i < recordsToInsert.length; i++) {
            const
                newRecord = recordsToInsert[i],
                oldParent = newRecord.parent;

            // Check if any descendants of the added node are moves.
            if (rootStore && !root.isLoading) {
                newRecord.traverse(r => {
                    if (r.root === root) {
                        isMove[r.id] = true;
                    }
                });
            }

            // If the new record has a parent, remove from that parent.
            // This operation may be vetoed by listeners.
            // If it is vetoed, then remove from the newRecords and do not
            // set the parent property
            if (oldParent?.removeChild(newRecord, isMove?.[newRecord.id], silent, { isInserting : true, ...options }) === false) {
                if (!newRecordsCloned) {
                    recordsToInsert  = recordsToInsert.slice();
                    newRecordsCloned = true;
                }
                recordsToInsert.splice(i--, 1);
            }
            else {
                newRecord.parent = me;

                // Set parentId directly to data, record.parentId uses a getter to return record.parent.id
                newRecord.data[parentIdField] = parentId;

                const { meta } = newRecord;

                // it seems we rely on setting the `oldParentId` flag on the "meta" object for managing `parentId` field
                // presence in the `meta.modified` in the code below
                // however, `oldParent` might be missing in case of STM removal restoring for example, but
                // `oldParentId` should still be set
                // covered with `Core/tests/data/stm/TreeRemoval.t.js//Should not include `parentId` to modifications after undo-ing the child removal`
                if (meta.modified[parentIdField] === parentId && !oldParent) {
                    meta.oldParentId = parentId;
                }

                if (oldParent) {
                    meta.oldParentId = oldParent.id;
                }
            }
        }

        // Still records to insert after beforeRemove listeners may have vetoed some
        if (recordsToInsert.length) {
            if (!Array.isArray(children)) {
                me.children = [];
            }

            if (!Array.isArray(me.orderedChildren)) {
                me.orderedChildren = [];
            }

            // Add to the children
            const insertAt = me.addToChildren(beforeRecord, recordsToInsert, options);

            stores.forEach(store => {
                if (!store.isChained) {

                    recordsToInsert.forEach(record => {
                        // Initialize context for newly added records
                        record.joinStore(store);
                    });

                    // Add to store (will also add any child records and trigger events)
                    store.onNodeAddChild(me, recordsToInsert, insertAt, isMove, silent);

                    recordsToInsert.forEach((record, i) => {
                        // If we are in the recursive inclusion of children at construction
                        // time, or in a store load, that must not be a data modification.
                        // Otherwise, we have to signal a change
                        if (record.meta.oldParentId != null && !(me.inProcessChildren || me.isLoading)) {
                            const
                                toSet                     = {
                                    [parentIdField]                   : parentId,
                                    [me.getDataSource('parentIndex')] : record.parentIndex
                                },
                                wasSet                    = {},
                                { modified, oldParentId } = record.meta,
                                oldParentIndex            = oldParentIndices[i];

                            delete record.meta.oldParentId;

                            if (me.id !== oldParentId) {
                                wasSet[parentIdField] = {
                                    value    : parentId,
                                    oldValue : oldParentId
                                };
                            }

                            if (record.parentIndex !== oldParentIndex) {
                                wasSet.parentIndex = {
                                    value    : record.parentIndex,
                                    oldValue : oldParentIndex
                                };
                            }

                            // Changing back to its original value
                            if (modified[parentIdField] === me.id) {
                                Reflect.deleteProperty(modified, parentIdField);
                            }
                            // Cache its original value
                            else if (!(parentIdField in modified)) {
                                modified[parentIdField] = oldParentId;
                            }

                            if (isMove[record.id]) {
                                const oldParent = store.getById(oldParentId);
                                // If old parent transitioned to being a leaf node, signal a change event so that the UI
                                // updates. Handled here and not on remove to get the correct order of events on move
                                if (oldParent.isLeaf && !silent) {
                                    oldParent.signalNodeChanged({
                                        isLeaf : {
                                            value    : true,
                                            oldValue : false
                                        }
                                    }, [store]);
                                }
                            }

                            record.afterChange(toSet, wasSet);
                        }

                        // should only perform this after all changes to `node.modified` are completed
                        record.traverse(node => {
                            if (!node.ignoreBag && !node.isLinked) {
                                store.updateModifiedBagForRecord(node);
                            }
                        });
                    });
                }
            });
        }

        return recordsToInsert;
    }

    /**
     * Remove a child record. Only direct children of this node can be removed, others are ignored.
     * @param {Core.data.Model|Core.data.Model[]} childRecords The record(s) to remove.
     * @param {Boolean} [isMove] Pass `true` if the record is being moved within the same store.
     * @param {Boolean} [silent] Pass `true` to not trigger events during remove.
     * @privateparam {Object} [options]
     * @privateparam {Object} [options.isInserting] `true` is passed when removal is part of record inserting (acted on by
     * ModelLink)
     * @returns {Core.data.Model[]} All records (including nested children) removed
     * @category Parent & children
     */
    removeChild(childRecords, isMove = false, silent = false, options = {}) {
        const
            me                = this,
            allRemovedRecords = [],
            wasLeaf           = me.isLeaf,
            {
                children,
                stores
            }                 = me;

        childRecords = ArrayHelper.asArray(childRecords);

        childRecords = childRecords.filter(r => r.parent === me);

        if (!silent) {
            // Allow store listeners to veto the beforeRemove event
            for (const store of stores) {
                if (!store.isChained && store.trigger('beforeRemove', {
                    parent : me, records : childRecords, isMove
                }) === false) {
                    return false;
                }
            }
        }

        const preResult = me.beforeRemoveChild?.(childRecords, isMove);

        for (const childRecord of childRecords) {
            const
                { parentIdField } = childRecord.constructor,
                { modified }      = childRecord.meta,
                oldParentId       = childRecord.parent ? childRecord.parent.id : null;

            // Cache its original value (not if it is a link, that would pollute original)
            if (!(parentIdField in modified) && !childRecord.isLinked) {
                modified[parentIdField] = oldParentId;
            }

            const index = me.removeFromChildren(childRecord, options);

            stores.forEach(store => {
                if (!store.isChained) {
                    const { isRemoving } = store;
                    // Raise the store isRemoving flag (it's set in Store#remove() but not when we call record#removeChild() directly)
                    store.isRemoving = true;
                    allRemovedRecords.push(...store.onNodeRemoveChild(me, [childRecord], index, { isMove, silent }));
                    // restore the flag initial state
                    store.isRemoving = isRemoving;
                }
            });

            // No need to clean up the node parent info and other meta data in case it is "move" operation. The info will be updated after "insert" operation.
            if (!isMove) {
                childRecord.parent = childRecord.parentIndex = childRecord.unfilteredIndex = childRecord.nextSibling = childRecord.previousSibling = null;

                // Reset parentId in data, record.parentId uses a getter to return record.parent.id
                childRecord.data[parentIdField] = null;
            }
        }

        // Convert emptied parent into leaf if configured to do so
        if ((me.unfilteredChildren || children).length === 0 && me.constructor.convertEmptyParentToLeaf.onRemove && !me.isRoot) {
            me.meta.isLeaf = true;
        }

        // If we've transitioned to being a leaf node, signal a change event so that the UI updates
        // (but not if part of move, will be signaled by insert)
        if (me.isLeaf !== wasLeaf && !silent && !isMove) {
            me.signalNodeChanged({
                isLeaf : {
                    value    : true,
                    oldValue : false
                }
            });
        }

        me.afterRemoveChild?.(childRecords, preResult, isMove);

        return allRemovedRecords;
    }

    clearParentId() {
        const me = this;

        Reflect.deleteProperty(me.data, me.parentIdField);
        Reflect.deleteProperty(me.originalData, me.parentIdField);

        if (me.meta.modified) {
            Reflect.deleteProperty(me.meta.modified, me.parentIdField);
        }
    }

    /**
     * Replaces all child nodes with the new node set.
     * @param {Core.data.Model|Core.data.Model[]} childRecords The new child record set.
     * @returns {Core.data.Model[]}
     * @category Parent & children
     */
    replaceChildren(newChildren) {
        this.clearChildren();
        this.data[this.constructor.childrenField] = newChildren;
        this.processChildren();
        return this.children;
    }

    /**
     * Removes all child nodes from this node.
     * @param {Boolean} [silent=false] Pass `true` to not fire Store events during the remove.
     * @returns {Core.data.Model[]}
     * @category Parent & children
     */
    clearChildren(silent = false) {
        const
            me         = this,
            { stores } = me,
            children   = me.unfilteredChildren || me.children;

        me.children        = [];
        me.orderedChildren = [];
        if (children && children !== true) {
            stores.forEach(store => {
                if (!store.isChained) {
                    // unfiltered:true to unregister children on filtered stores
                    store.onNodeRemoveChild(me, children, 0, { unfiltered : true, silent });
                }
            });

            // clear unfilteredChildren (must be after the above loop)
            if (me.unfilteredChildren) {
                me.unfilteredChildren = [];
            }
        }
    }

    /**
     * Removes all records from the rootNode
     * @private
     */
    clear() {
        const
            me         = this,
            { stores } = me,
            children   = me.children?.slice();

        // Only allow for root node and if data is present
        if (!me.isRoot || !children) {
            return;
        }

        for (const store of stores) {
            if (!store.isChained) {
                if (store.trigger('beforeRemove', {
                    parent : me, records : children, isMove : false, removingAll : true
                }) === false) {
                    return false;
                }
            }
        }

        me.children.length = 0;

        if (me.unfilteredChildren) {
            me.unfilteredChildren.length = 0;
        }

        stores.forEach(store => {
            children.forEach(child => {
                if (child.stores.includes(store)) {
                    // this will drill down the child, unregistering whole branch
                    child.unjoinStore(store);
                }

                child.parent = child.parentIndex = child.nextSibling = child.previousSibling = null;
            });

            store.storage.suspendEvents();
            store.storage.clear();
            store.storage.resumeEvents();

            store.added.clear();
            store.modified.clear();

            store.trigger('removeAll');
            store.trigger('change', { action : 'removeall' });
        });
    }

    updateChildrenIndices(children, indexName, silent = false) {
        let previousSibling = null;

        for (let i = 0; i < children.length; i++) {
            const
                child    = children[i],
                oldValue = child[indexName];

            if (indexName === 'parentIndex' || indexName === 'orderedParentIndex') {
                // Record should not be considered modified by initial assignment of parentIndex
                if (oldValue === undefined || silent) {
                    child.setData(indexName, i);
                }
                // Check to avoid pointless beforeUpdates from inSet
                else if (oldValue !== i) {
                    // Silent set, do not want to trigger events from updated indices
                    child.set(indexName, i, true);
                }
            }
            else {
                child[indexName] = i;
            }

            if (indexName === 'parentIndex') {
                child.previousSibling = previousSibling;
                if (previousSibling) {
                    previousSibling.nextSibling = child;
                }
                // Last child never has a nextSibling
                if (i === children.length - 1) {
                    child.nextSibling = null;
                }
                previousSibling = child;
            }
        }
    }

    addToChildren(beforeRecord, newRecords, options = {}) {
        // children can be sorted and filtered
        // unfilteredChildren can not be filtered
        // orderedChildren can not be nor filtered nor sorted. it holds true tree hierarchy
        const
            me      = this,
            configs = [
                [me.children, 'parentIndex', beforeRecord],
                [me.unfilteredChildren, 'unfilteredIndex', beforeRecord],
                [
                    me.orderedChildren,
                    'orderedParentIndex',
                    options?.orderedBeforeNode ?? (
                        options?.orderedParentIndex !== undefined
                            ? me.orderedChildren[options?.orderedParentIndex]
                            : beforeRecord
                    )
                ]
            ];

        for (const config of configs) {
            const [children, indexName, beforeRecord] = config;

            if (children) {
                // On undo/redo it might happen that we redo orderedParentIndex change first breaking indexing. Safe
                // way is to always check array for record index. We also cannot filter out orderedParentIndex change
                // because by itself it is valid.
                const index = beforeRecord ? (indexName === 'orderedParentIndex' ? children.indexOf(beforeRecord) : beforeRecord[indexName]) : children.length;

                config.push(index);

                children.splice(index, 0, ...newRecords);

                if (!options?.[indexName]?.skip) {
                    me.updateChildrenIndices(children, indexName);
                }
            }
        }

        // always return index of the record in the children array
        return configs[0][3];
    }

    removeFromChildren(childRecord, options) {
        const configs = [
            [this.children, 'parentIndex'],
            [this.unfilteredChildren, 'unfilteredIndex'],
            [this.orderedChildren, 'orderedParentIndex']
        ];

        for (const config of configs) {
            const [children, indexName] = config;

            if (children) {
                // parentIndex/orderedParentIndex might be changed when applying a remote changeset leading to
                // record getting removed from the wrong position in the children array. Therefore, we should
                // not rely on the index value, instead we query array itself
                const index = children.indexOf(childRecord);

                config.push(index);

                if (index > -1) {
                    children.splice(index, 1);
                    if (!options?.[indexName]?.skip) {
                        this.updateChildrenIndices(children, indexName);
                    }
                }
            }
        }

        // always return index of the record in the children array
        return configs[0][2];
    }

    /**
     * Iterates orderedChildren array to apply sorting order according to `orderedParentIndex`.
     * Normally sorting is not required because order is maintained on append/insert. But is useful
     * when pasting number of records to restore their original order.
     * @param {Boolean} [deep=true] True to dive into children. False to sort own children.
     * @param {Boolean} [usePreviousOrder=false] Enable to use previous value of `orderedParentIndex`.
     * @returns {Set} Returns Set of moved nodes which require WBS update
     * @private
     */
    sortOrderedChildren(deep = true, usePreviousOrder = false) {
        // Collect moved nodes, we need to recalculate WBS on them.
        const movedNodes = [];

        if (!this.isLeaf) {
            this.orderedChildren.sort((a, b) => {
                if (usePreviousOrder) {
                    const
                        aPrevIndex = a.meta.modified.orderedParentIndex ?? a.orderedParentIndex,
                        bPrevIndex = b.meta.modified.orderedParentIndex ?? b.orderedParentIndex,
                        result     = aPrevIndex - bPrevIndex;

                    if (result !== 0) {
                        movedNodes.push(a);
                        movedNodes.push(b);
                    }

                    return result;
                }
                else {
                    return a.orderedParentIndex - b.orderedParentIndex;
                }
            });

            if (deep) {
                this.orderedChildren.forEach(child => {
                    movedNodes.push(...child.sortOrderedChildren(deep, usePreviousOrder));
                });
            }

            this.updateChildrenIndices(this.orderedChildren, 'orderedParentIndex', true);
        }

        return new Set(movedNodes);
    }

    unjoinStore(store, isReplacing = false) {
        const me = this;

        // clear the filtering when unjoining the record from the store
        if (me.unfilteredChildren) {
            me.children = me.unfilteredChildren.slice();
            me.unfilteredChildren = null;
        }

        super.unjoinStore?.(store, isReplacing);
    }
};
