import Base from '../Base.js';
import Events from '../mixin/Events.js';
import CollectionFilter from './CollectionFilter.js';
import CollectionSorter from './CollectionSorter.js';
import FunctionHelper from '../helper/FunctionHelper.js';
import ArrayHelper from '../helper/ArrayHelper.js';

/**
 * @module Core/util/Collection
 */

const
    return0                 = () => 0,
    reverseNumericSortFn    = (a, b) => b - a,
    filteredIndicesProperty = Symbol('filteredIndicesProperty'),
    emptyArray              = Object.freeze([]),
    sortEvent               = Object.freeze({
        action   : 'sort',
        added    : emptyArray,
        removed  : emptyArray,
        replaced : emptyArray
    }),
    filterEvent             = Object.freeze({
        action   : 'filter',
        added    : emptyArray,
        removed  : emptyArray,
        replaced : emptyArray
    }),
    keyTypes               = {
        string : 1,
        number : 1
    };

// Adds a single item to a single index using the specified key
function addItemToIndex(item, index, key) {
    // Unique holds a single entry
    if (index.unique !== false) {
        index.set(key, item);
    }
    // Non-unique index holds a Set
    else {
        let set = index.get(key);

        // Add a set if this is the first entry
        if (!set) {
            set = new Set();
            index.set(key, set);
        }

        // Add entry to the set
        set.add(item);
    }
}

// Removes a single item from a single index using the specified key
function removeItemFromIndex(item, index, key) {
    // Unique holds single entry, remove it
    if (index.unique !== false) {
        index.delete(key);
    }
    else if (index.has(key)) {
        // Remove from set
        index.get(key).delete(item);

        // Remove turned empty set
        if (!index.get(key).size) {
            index.delete(key);
        }
    }
}

// Used to fully build the indices, normal and filtered (if used). Better to do full builds for performance reasons
// when assigning new datasets. For other CRUD operations, indices are kept up to date elsewhere
function doRebuildIndices(values, indices, keyProps, indexCount) {
    for (let i = 0; i < values.length; i++) {
        const item = values[i];

        for (let j = 0; j < indexCount; j++) {
            const
                keyProp = keyProps[j],
                key     = item[keyProp],
                index   = indices[keyProp];

            addItemToIndex(item, index, key);
        }
    }
}

/**
 * A class which encapsulates a {@link #function-get keyed},
 * {@link #function-addFilter filterable}, {@link #function-addSorter sortable}
 * collection of objects. Entries may not be atomic data types such as `string` or `number`.
 *
 * The entries are keyed by their `id` which is determined by interrogating the {@link #config-idProperty}.
 *
 * To filter a Collection, add a {@link Core.util.CollectionFilter CollectionFilter}
 * using the {@link #function-addFilter} method. A Filter config object may be specified here
 * which will be promoted to a CollectionFilter instance.
 *
 * To sort a Collection, add a {@link Core.util.CollectionSorter CollectionSorter}
 * using the {@link #function-addSorter} method. A Sorter config object may be specified here
 * which will be promoted to a CollectionSorter instance.
 */
export default class Collection extends Base.mixin(Events) {

    _sortFunction = null;
    _addedValues = null;

    //region Config

    static get configurable() {
        return {
            /**
             * Specify the name of the property of added objects which provides the lookup key
             * @config {String}
             * @default
             */
            idProperty : 'id',

            /**
             * Specify the names or index configs of properties which are to be indexed for fast lookup.
             *
             * Index configs use the format `{ property : string, unique : boolean }`. Unique indices stores one index
             * per entry, non-unique stores a `Set`. If left out, `unique` defaults to `true`
             *
             * @config {String[]|Object[]}
             * @property {String} property Property to index by
             * @property {Boolean} [unique=true] `true` for unique keys (~primary keys), `false` for non-unique keys
             * (~foreign keys)
             */
            extraKeys : null,

            /**
             * Automatically apply filters on item add.
             * @config {Boolean}
             * @default
             */
            autoFilter : true,

            /**
             * Automatically apply sorters on item add.
             * @config {Boolean}
             * @default
             */
            autoSort : true,

            /**
             * A {@link Core.util.CollectionSorter Sorter}, or Sorter config object, or
             * an array of these, to use to sort this Collection.
             * @config {CollectionSorterConfig[]}
             * @default
             */
            sorters : {
                $config : ['lazy'],
                value   : []
            }
        };
    }

    get isCollection() {
        return true;
    }

    //endregion

    //region Init & destroy

    construct(config) {
        /**
         * A counter which is incremented whenever the Collection is mutated in a meaningful way.
         *
         * If a {@link #function-splice} call results in no net replacement, removal or addition,
         * then the `generation` will not be incremented.
         * @property {Number}
         * @readonly
         */
        this.generation = 0;

        this._values = [];

        super.construct(config);
    }

    doDestroy() {
        super.doDestroy();

        const me = this;

        me._values.length = 0;

        if (me.isFiltered) {
            me._filteredValues.length = 0;
            me.filters.destroy();
        }

        me._sorters?.destroy();
    }

    //endregion

    //region "CRUD"

    /**
     * Clears this collection.
     */
    clear() {
        const
            me      = this,
            removed = me._values.slice();

        if (me.totalCount) {
            me._values.length = 0;
            if (me._filteredValues) {
                me._filteredValues.length = 0;
            }
            me._indicesInvalid = true;

            // Indicate to observers that data has changed.
            me.generation++;
            me.trigger('change', {
                action : 'clear',
                removed
            });
        }
    }

    /**
     * Compares the content of this Collection with the content of the passed Collection or
     * with the passed array. Order insensitive. This returns `true` if the two objects passed
     * contain the same set of items.
     * @param {Core.util.Collection|Array} other The Collection or array to compare with.
     * @param {Function} [map] Optionally a function to convert the items into a comparable object
     * to compare. For example `item => item.id` could be used to compare the ids of the
     * constituent items.
     * @returns {Boolean} `true` if the two objects passed have the same content.
     */
    equals(other, map) {
        if (other.isCollection) {
            other = other.values;
        }

        if (other.length === this.count) {
            let { values } = this;

            if (map) {
                other = other.map(map);
                values = values.map(map);
            }
            return ArrayHelper.delta(other, values).inBoth.length === this.count;
        }
        return false;
    }

    /**
     * Replaces the internal `values` array with the passed `values`, or `filteredValues` array with the passed `filteredValues`.
     * If `filteredValues` are not passed explicitly, but storage is filtered, decides internally `values` or `filteredValues` should
     * be replaced by passed `values`.
     *
     * Note that this takes ownership of the array, and the array must not be mutated by outside code.
     *
     * This is an internal utility method, not designed for use by application code.
     *
     * @param {Object} params Values and parameters to replace
     * @param {Object[]} params.values The new `values` array
     * @param {Object[]} [params.filteredValues] The new `filteredValues` array. Applicable only when storage is filtered.
     * @param {Boolean} [params.silent=false] If true, `change` event will not be fired
     * @param {Boolean} [params.isNewDataset=false] If true, `values` is a new dataset
     * @fires change
     * @internal
     */
    replaceValues({ values, filteredValues, silent = false, isNewDataset = false }) {
        const me = this;

        let replacedValues, replacedFilteredValues;

        // The isNewDataset flag is passed by store#loadData to indicate that it's
        // a new data load, and that local filters can be applied.
        // Other use cases are for purely local updates of an existing dataset such as
        // refreshing the visible data with a values array containing group headers.
        if (me.isFiltered && !isNewDataset) {
            const filteredPassed = Boolean(filteredValues);

            // If `filteredValues` are missing, take `values` as a source of filtered values
            if (!filteredPassed) {
                filteredValues = values.slice();
                values = null;
            }
            // otherwise check if non-filtered values are passed together with filtered, and replace them too
            else if (values) {
                replacedValues = me._values;
                me._values = values.slice();
            }

            replacedFilteredValues = me._filteredValues;
            me._filteredValues = filteredValues.slice();
        }
        else {
            replacedValues = me._values;
            me._values = values.slice();
            filteredValues = null;

            if (me.isFiltered && isNewDataset && me.autoFilter) {
                me._filterFunction = null;
                me._filteredValues = me._values.filter(me.filterFunction);
            }
            else if (me._filteredValues) {
                me._filteredValues.length = 0;
            }
        }

        me._indicesInvalid = true;
        me._addedValues = undefined;

        // Indicate to observers that data has changed.
        me.generation++;

        if (!silent) {
            me.trigger('change', {
                action : 'replaceValues',
                replacedValues,
                replacedFilteredValues,
                values,
                filteredValues
            });
        }
    }

    set values(values) {
        // Want a full rebuild for new dataset, less costly than doing it per item
        this.invalidateIndices();

        this.splice(0, this._values.length, values);
    }

    /**
     * The set of values of this Collection. If this Collection {@link #property-isFiltered},
     * this yields the filtered data set.
     *
     * Setting this property replaces the data set.
     * @property {Object[]}
     */
    get values() {
        return this.isFiltered ? this._filteredValues : this._values;
    }

    /**
     * The set of filtered values of this Collection (those matching the current filters).
     * @property {Object[]}
     * @private
     */
    get filteredValues() {
        return this._filteredValues;
    }

    /**
     * Iterator that allows you to do `for (const item of collection)`
     */
    [Symbol.iterator]() {
        return this.values[Symbol.iterator]();
    }

    /**
     * Executes the passed function for each item in this Collection, passing in the item,
     * ths index, and the full item array.
     * @param {Function} fn The function to execute.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to include all items, bypassing filters.
     */
    forEach(fn, ignoreFilters = false) {
        (this.isFiltered && !ignoreFilters ? this._filteredValues : this._values).forEach(fn);
    }

    /**
     * Extracts ths content of this Collection into an array based upon the passed
     * value extraction function.
     * @param {Function} fn A function, which, when passed an item, returns a value to place into the resulting array.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to process an item even if it is filtered out.
     * @returns {Object[]} An array of values extracted from this Collection.
     */
    map(fn, ignoreFilters = false) {
        return (this.isFiltered && !ignoreFilters ? this._filteredValues : this._values).map(fn);
    }

    /**
     * Returns the first item in this Collection which elicits a *truthy* return value from the passed function.
     * @param {Function} fn A function, which, when passed an item, returns `true` to select it as the item to return.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to include filtered out items.
     * @returns {Object} The matched item, or `undefined`.
     */
    find(fn, ignoreFilters = false) {
        return (this.isFiltered && !ignoreFilters ? this._filteredValues : this._values).find(fn);
    }

    get first() {
        return this.values[0];
    }

    get last() {
        return this.values[this.count - 1];
    }

    /**
     * The set of all values of this Collection regardless of filters applied.
     * @readonly
     * @property {Object[]}
     */
    get allValues() {
        return this._values;
    }

    /**
     * The set of values added to this Collection since the last sort or replaceValues operation.
     * @private
     * @readonly
     * @property {Object[]}
     */
    get addedValues() {
        return this._addedValues;
    }

    /**
     * This method ensures that every item in this Collection is replaced by the matched by
     * `id` item in the other Collection.
     *
     * By default, any items in this Collection which are __not__ in the other Collection are removed.
     *
     * If the second parameter is passed as `false`, then items which are not in the other
     * Collection are not removed.
     *
     * This can be used for example when updating a selected record Collection when a new
     * Store or new store dataset arrives. The selected Collection must reference the latest
     * versions of the selected record `id`s
     * @param {Core.util.Collection} other The Collection whose items to match.
     */
    match(other, allowRemove = true) {
        const
            me          = this,
            { _values } = me,
            toRemove    = [];

        // Update selected records collection
        me.forEach(item => {
            const newInstance = other.get(item.id, true);
            // If item exists in other Collection, update this with a reference to the other version.
            // This must happen silently, so splice the _values array
            if (newInstance) {
                const
                    index       = me.indexOf(item, true),
                    oldInstance = _values[index];

                // Replace the instance directly into our values
                _values[index] = newInstance;

                // Ensure the indexes match
                me.removeFromIndices(oldInstance);
                me.addToIndices(newInstance);

            }
            else if (allowRemove) {
                toRemove.push(item);
            }
        });

        if (toRemove.length) {
            me.remove(toRemove);
        }

        // The filtered set must match the new reality.
        if (me.isFiltered) {
            me._filteredValues = me._values.filter(me.filterFunction);
        }

        return toRemove;
    }

    /**
     * Adds items to this Collection. Multiple new items may be passed.
     *
     * By default, new items are appended to the existing values.
     *
     * Any {@link #property-sorters} {@link #property-sorters} present are re-run.
     *
     * Any {@link #property-filters} {@link #property-filters} present are re-run.
     *
     * *Note that if application functionality requires add and remove, the
     * {@link #function-splice} operation is preferred as it performs both
     * operations in an atomic manner*
     * @param  {...Object} items The item(s) to add.
     */
    add(...items) {
        if (items.length === 1) {
            this.splice(this._values.length, null, ...items);
        }
        else {
            this.splice(this._values.length, null, items);
        }
    }

    /**
     * Removes items from this Collection. Multiple items may be passed.
     *
     * Any {@link #property-sorters} {@link #property-sorters} present are re-run.
     *
     * Any {@link #property-filters} {@link #property-filters} present are re-run.
     *
     * *Note that if application functionality requires add and remove, the
     * {@link #function-splice} operation is preferred as it performs both
     * operations in an atomic manner*
     * @param  {...Object} items The item(s) to remove.
     */
    remove(...items) {
        if (items.length === 1) {
            this.splice(0, ...items);
        }
        else {
            this.splice(0, items);
        }
    }

    /**
     * Moves an individual item, or a block of items to another location.
     * @param {Object|Object[]} items The item/items to move.
     * @param {Object} [beforeItem] the item to insert the first item before. If omitted, the `item`
     * is moved to the end of the Collection.
     * @returns {Number} The new index of the `item`.
     */
    move(items, beforeItem) {
        items = ArrayHelper.asArray(items);

        // Handle the case of move(myItem, myItem). It's a no-op
        while (items.length && items[0] === beforeItem) {
            items.shift();
        }
        if (!items.length) {
            return;
        }

        const
            me          = this,
            { _values } = me,
            itemIndex   = me.indexOf(items[0], true);

        // move(record, followingrecord) is a no-op
        if (items.length === 1 && _values[itemIndex + 1] === beforeItem) {
            return;
        }

        // Silently remove the items that are to be inserted before the "beforeItem".
        me.suspendEvents();
        me.remove(items);
        me.resumeEvents();

        const beforeIndex = beforeItem ? me.indexOf(beforeItem, true) : _values.length;

        if (beforeIndex === -1) {
            throw new Error('Collection move beforeItem parameter must be present in Collection');
        }

        _values.splice(beforeIndex, 0, ...items);
        me._indicesInvalid = 1;

        me.trigger('change', {
            action : 'move',
            items,
            from   : itemIndex,
            to     : beforeIndex
        });

        return beforeIndex;
    }

    /**
     * The core data set mutation method. Removes and adds at the same time. Analogous
     * to the `Array` `splice` method.
     *
     * Note that if items that are specified for removal are also in the `toAdd` array,
     * then those items are *not* removed then appended. They remain in the same position
     * relative to all remaining items.
     *
     * @param {Number} index Index at which to remove a block of items. Only valid if the
     * second, `toRemove` argument is a number.
     * @param {Object[]|Number} [toRemove] Either the number of items to remove starting
     * at the passed `index`, or an array of items to remove (If an array is passed, the `index` is ignored).
     * @param  {Object[]|Object} [toAdd] An item, or an array of items to add.
     */
    splice(index = 0, toRemove, ...toAdd) {
        const
            me         = this,
            idProperty = me.idProperty,
            values     = me._values,
            newIds     = {},
            removed    = [],
            replaced   = [],
            oldCount   = me.totalCount;

        let added,
            mutated;

        // Create an "newIds" map of the new items so remove ops know if it's really a replace
        // {
        //     1234 : true
        // }
        // And an "added" array of the items that need adding (there was not already an entry for the id)
        //

        if (me.trigger('beforeSplice', { index, toRemove, toAdd }) === false) {
            return;
        }

        if (toAdd) {
            if (toAdd.length === 1 && Array.isArray(toAdd[0])) {
                toAdd = toAdd[0];
            }

            // Check for replacements if we contain any data
            if (oldCount && toAdd.length) {
                // Only risk rebuilding the indices if we are adding
                const idIndex = me.indices[idProperty];

                added = [];

                for (let i = 0; i < toAdd.length; i++) {
                    const
                        newItem       = toAdd[i],
                        id            = newItem[idProperty],
                        existingItem  = idIndex.get(id),
                        existingIndex = existingItem ? values.indexOf(existingItem) : -1;

                    // Register incoming id so that removal leaves it be
                    newIds[id] = true;

                    // Incoming id is already present.
                    // Replace it in place.
                    if (existingIndex !== -1) {
                        // If incoming is the same object, it's a no-op
                        if (values[existingIndex] !== newItem) {
                            replaced.push([values[existingIndex], newItem]);
                            values[existingIndex] = newItem;
                        }
                    }
                    else {
                        added.push(newItem);
                    }
                }
            }
            // Empty Collection, we simply add what we're passed
            else {
                added = toAdd;
            }
        }

        if (toRemove) {
            // We're removing a chunk starting at index
            if (typeof toRemove === 'number') {
                // Ensure we don't walk off the end if the toRemove count exceeds what we contain
                toRemove = Math.min(toRemove, values.length - index);

                for (let removeIndex = index; toRemove; --toRemove) {
                    const id = values[removeIndex][idProperty];

                    // If the entry here is being replaced, skip the insertion index past it
                    if (newIds[id]) {
                        index++;
                        removeIndex++;
                    }
                    // If the id is not among incoming items, remove it
                    else {
                        removed.push(values[removeIndex]);
                        values.splice(removeIndex, 1);
                        mutated = true;
                    }
                }
            }
            // We are removing an item/items
            else {
                let contiguous = added.length === 0,
                    lastIdx;

                toRemove = ArrayHelper.asArray(toRemove);

                // Create array of index points to remove.
                // They must be in reverse order so that removal leaves following remove indices stable
                const removeIndices = toRemove.reduce((result, item) => {
                    const
                        isNumeric = typeof item === 'number',
                        idx       = isNumeric ? item : me.indexOf(item, true);

                    // Drop out of contiguous mode if we find a non-contiguous record, or a remove *index*
                    if (contiguous && (lastIdx != null && idx !== lastIdx + 1 || isNumeric)) {
                        contiguous = false;
                    }

                    // Do not include indices out of range in our removeIndices
                    if (idx >= 0 && idx < oldCount) {
                        result.push(idx);
                    }
                    lastIdx = idx;
                    return result;
                }, []).sort(reverseNumericSortFn);

                // If it's a pure remove of contiguous items with no adds, fast track it.
                if (contiguous) {
                    // If reduced to zero by being asked to remove items we do not contain
                    // then this is a no-op
                    if (removeIndices.length) {
                        removed.push.apply(removed, toRemove);
                        values.splice(removeIndices[removeIndices.length - 1], removeIndices.length);
                        mutated = true;
                    }
                }
                else {
                    // Loop through removeIndices splicing each index out of the values
                    // unless there's an incoming identical id.
                    for (let i = 0; i < removeIndices.length; i++) {
                        const removeIndex = removeIndices[i];

                        if (removeIndex !== -1) {
                            const id = values[removeIndex][idProperty];

                            // If the id is not among incoming items, remove it
                            if (!newIds[id]) {
                                removed.unshift(values[removeIndex]);
                                values.splice(removeIndex, 1);
                                mutated = true;
                            }
                        }
                    }
                }
            }

            // Update indices only if they have been used
            if (removed.length && !me._indicesInvalid) {
                removed.forEach(me.removeFromIndices, me);
            }
        }

        // If we collected genuinely new entries, insert them at the splice index
        if (added.length) {
            values.splice(Math.min(index, values.length), 0, ...added);
            mutated = true;

            // Update indices only if they have been used
            if (!me._indicesInvalid) {
                added.forEach(me.addToIndices, me);
            }
            if (!me._addedValues) {
                me._addedValues = new Set();
            }
            for (const value of added) {
                me._addedValues.add(value);
            }
        }

        if (removed.length && me._addedValues) {
            for (const value of removed) {
                me._addedValues.delete(value);
            }
        }

        // Update indices only if they have been used
        if (replaced.length && !me._indicesInvalid) {
            replaced.forEach(rep => {
                me.removeFromIndices(rep[0]);
                me.addToIndices(rep[1]);
            });
        }

        // If we either added or removed items, or we did an in-place replace operation
        // then inform all interested parties.
        if (mutated || replaced.length) {
            // Ensure order of values matches the sorters
            if (me.isSorted) {
                me.onSortersChanged();
            }
            // The sort will also recreate the filteredValues so that it can be in correct sort order
            else if (me.isFiltered) {
                if (me.autoFilter) {
                    me.onFiltersChanged({ action : 'splice', oldCount : 1 });
                }
                else {
                    me._filteredValues.splice(Math.min(index, me._filteredValues.length), 0, ...added);
                }
            }

            // Indicate to observers that data has changed.
            me.generation++;

            /**
             * Fired when items are added, replace or removed
             * @event change
             * @param {'splice'|'clear'|'replaceValues'|'move'|'sort'|'filter'} action The underlying operation
             * which caused data change. May be `'splice'` (meaning an atomic add/remove operation, `'sort'` or
             * `'filter'`), `'clear'`, `'replaceValues'`, `'move'`, `'sort'` or `'filter'`.
             * @param {Core.util.Collection} source This Collection.
             * @param {Object[]} removed An array of removed items. If the `action` is `'filter'`, the
             * removed property represents the records which were filtered out by the action.
             * @param {Object[]} added An array of added items. If the `action` is `'filter'`, the
             * added property represents the records which were filtered in by the action.
             * @param {Object[]} replaced An array of replacements, each entry of which contains `[oldValue, newValue]`.
             * @param {Number} oldCount The number of items in the full, unfiltered collection prior to the splice operation.
             */
            me.trigger('change', {
                action : 'splice',
                removed,
                added,
                replaced,
                oldCount
            });
        }
        else {
            /**
             * Fired when a {@link #function-splice} operation is requested but the operation
             * is a no-op and has caused no change to this Collection's dataset. The splice
             * method's parameters are passed for reference.
             * @event noChange
             * @param {Number} index Index at which to remove a block of items.
             * @param {Object[]|Number} [toRemove] Either the number of items to remove starting
             * at the passed `index`, or an array of items to remove (If an array is passed, the `index` is ignored).
             * @param  {Object[]|Object} [toAdd] An item, or an array of items to add.
             */
            me.trigger('noChange', {
                index,
                toRemove,
                toAdd
            });
        }
    }

    /**
     * Change the id of an existing member by mutating its {@link #config-idProperty}.
     * @param {String|Number|Object} item The item or id of the item to change.
     * @param {String|Number} newId The id to set in the existing member.
     */
    changeId(item, newId) {
        const
            me             = this,
            { idProperty } = me,
            oldId          = keyTypes[typeof item] ? item : item[idProperty],
            member         = me.get(oldId);

        if (member) {
            const existingMember = me.get(newId);

            if (existingMember && member !== existingMember) {
                throw new Error(`Attempt to set item ${oldId} to already existing member's id ${newId}`);
            }

            me.removeIndexEntry(item, idProperty, oldId);
            me.addIndexEntry(item, idProperty, newId);

            // Last on purpose, onItemMutation would fail to find the item if its id was changed prior to the call
            member[idProperty] = newId;
        }
    }

    /**
     * Returns the item with the passed `id`. By default, filtered are honoured, and
     * if the item with the requested `id` is filtered out, nothing will be returned.
     *
     * To return the item even if it has been filtered out, pass the second parameter as `true`.
     * @param {*} id The `id` to find.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to return an item even if it is filtered out.
     * @returns {Object} The found item, or `undefined`.
     */
    get(id, ignoreFilters = false) {
        return this.getBy(this.idProperty, id, ignoreFilters);
    }

    getAt(index, ignoreFilters = false) {
        if (this.isFiltered && !ignoreFilters) {
            return this._filteredValues[index];
        }
        else {
            return this._values[index];
        }
    }

    /**
     * Returns the item with passed property name equal to the passed value. By default,
     * filtered are honoured, and if the item with the requested `id` is filtered out,
     * nothing will be returned.
     *
     * To return the item even if it has been filtered out, pass the third parameter as `true`.
     * @param {String} propertyName The property to test.
     * @param {*} value The value to find.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to return an item even if it is filtered out.
     * @returns {Object} The found item, or `undefined`.
     */
    getBy(propertyName, value, ignoreFilters = false) {
        return this.findItem(propertyName, value, this.isFiltered && ignoreFilters);
    }

    /**
     * The number of items in this collection. Note that this honours filtering.
     * See {@link #property-totalCount};
     * @property {Number}
     * @readonly
     */
    get count() {
        return this.values.length;
    }

    /**
     * The number of items in this collection regardless of filtering.
     * @property {Number}
     * @readonly
     */
    get totalCount() {
        return this._values.length;
    }

    /**
     * The property name used to extract item `id`s from added objects.
     * @member {String} idProperty
     */
    updateIdProperty(idProperty) {
        this.addIndex({ property : idProperty, unique : true });
    }

    //endregion

    //region Sorting

    /**
     * The Collection of {@link Core.util.CollectionSorter Sorters} for this Collection.
     * @member {Core.util.Collection} sorters
     */
    changeSorters(sorters) {
        return new Collection({
            values            : ArrayHelper.asArray(sorters),
            internalListeners : {
                change  : 'onSortersChanged',
                thisObj : this
            }
        });
    }

    /**
     * Adds a Sorter to the Collection of Sorters which are operating on this Collection.
     *
     * A Sorter may be specified as an instantiated {@link Core.util.CollectionSorter}, or a config object for a
     * CollectionSorter of the form
     *
     *     {
     *         property  : 'age',
     *         direction : 'desc'
     *     }
     *
     * Note that by default, a Sorter *replaces* a Sorter with the same `property` to make
     * it easy to change existing Sorters. A Sorter's `id` is its `property` by default. You
     * can avoid this and add multiple Sorters for one property by configuring Sorters with `id`s.
     *
     * A Sorter may also be specified as a function which compares two objects eg:
     *
     *     (lhs, rhs) => lhs.customerDetails.age - rhs.customerDetails.age
     *
     * @param {CollectionSorterConfig} sorter A Sorter configuration object to add to the Collection
     * of Sorters operating on this Collection.
     * @returns {Core.util.CollectionSorter} The resulting Sorter to make it easy to remove Sorters.
     */
    addSorter(sorter) {
        const result = (sorter instanceof CollectionSorter) ? sorter : new CollectionSorter(sorter);

        this.sorters.add(result);

        return result;
    }

    /**
     * A flag which is `true` if this Collection has active {@link #property-sorters}.
     * @property {Boolean}
     * @readonly
     */
    get isSorted() {
        return Boolean(this._sorters?.count);
    }

    onSortersChanged() {
        const me = this;

        me._sortFunction = null;
        me._addedValues = null;

        me._values.sort(me.sortFunction);

        me.trigger('change', sortEvent);
    }

    /**
     * A sorter function which encapsulates the {@link Core.util.CollectionSorter Sorters}
     * for this Collection.
     * @property {Function}
     * @readonly
     */
    get sortFunction() {
        if (!this._sortFunction) {
            if (this.isSorted) {
                this._sortFunction = CollectionSorter.generateSortFunction(this.sorters.values);
            }
            else {
                this._sortFunction = return0;
            }
        }

        return this._sortFunction;
    }

    //endregion

    //region Filtering

    /**
     * The Collection of {@link Core.util.CollectionFilter Filters} for this Collection.
     * @property {Core.util.Collection}
     * @readonly
     */
    get filters() {
        if (!this._filters) {
            this._filters = new Collection({
                internalListeners : {
                    change  : 'onFiltersChanged',
                    thisObj : this
                }
            });
        }
        return this._filters;
    }

    /**
     * Adds a Filter to the Collection of Filters which are operating on this Collection.
     *
     * A Filter may be an specified as an instantiated {@link Core.util.CollectionFilter
     * CollectionFilter}, or a config object for a CollectionFilter of the form
     *
     *     {
     *         property : 'age',
     *         operator : '>=',
     *         value    : 21
     *     }
     *
     * Note that by default, a Filter *replaces* a Filter with the same `property` to make
     * it easy to change existing Filters. A Filter's `id` is its `property` by default. You
     * can avoid this and add multiple Filters for one property by configuring Filters with `id`s.
     *
     * A Filter may also be specified as a function which filters candidate objects eg:
     *
     *     candidate => candidate.customerDetails.age >= 21
     *
     * @param {CollectionFilterConfig|Core.util.CollectionFilter} filter A Filter or Filter configuration object to add
     * to the Collection of Filters operating on this Collection.
     * @returns {Core.util.CollectionFilter} The resulting Filter to make it easy to remove Filters.
     */
    addFilter(filter) {
        const result = (filter instanceof CollectionFilter) ? filter : new CollectionFilter(filter);

        this.filters.add(result);

        return result;
    }

    removeFilter(filter) {
        const { filters } = this;

        if (!filter.isCollectionFilter) {
            filter = filters.get(filter);
        }
        filters.remove(filter);
    }

    clearFilters() {
        this.filters.clear();
    }

    /**
     * A flag which is `true` if this Collection has active {@link #property-filters}.
     * @property {Boolean}
     * @readonly
     */
    get isFiltered() {
        return Boolean(this._filters && this._filters.count);
    }

    onFiltersChanged({ action, removed : gone, oldCount }) {
        const
            me          = this,
            oldDataset  = oldCount || (action === 'clear' && gone.length) ? me._filteredValues : me._values;

        me._filterFunction = null;
        me._filteredValues = me._values.filter(me.filterFunction);
        me._indicesInvalid = true;

        const {
            toAdd    : added,
            toRemove : removed
        } = ArrayHelper.delta(me._filteredValues, oldDataset, true);

        me.trigger('change', { ...filterEvent, added, removed });
    }

    /**
     * A filter function which encapsulates the {@link Core.util.CollectionFilter Filters}
     * for this Collection.
     * @property {Function}
     * @readonly
     */
    get filterFunction() {
        if (!this._filterFunction) {
            if (this.isFiltered) {
                this._filterFunction = CollectionFilter.generateFiltersFunction(this.filters.values);
            }
            else {
                this._filterFunction = FunctionHelper.returnTrue;
            }
        }

        return this._filterFunction;
    }

    //endregion

    //region Indexing

    changeExtraKeys(extraKeys) {
        const keys = ArrayHelper.asArray(extraKeys);
        // Normalize to always be an array of index configs
        return keys.map(config => {
            if (typeof config === 'string') {
                return { property : config, unique : true };
            }
            return config;
        });
    }

    updateExtraKeys(extraKeys) {
        for (let i = 0; i < extraKeys.length; i++) {
            this.addIndex(extraKeys[i]);
        }
    }

    /**
     * Adds a lookup index for the passed property name or index config. The index is built lazily when an index is
     * searched
     * @internal
     * @param {Object} indexConfig An index config
     * @param {String} indexConfig.property The property name to add an index for
     * @param {Boolean} [indexConfig.unique] Specify `false` to allow multiple entries of the same index, turning
     *   entries into sets
     * @param {Object} [indexConfig.dependentOn] The properties that make the key
     */
    addIndex(indexConfig) {
        const me = this;

        // Combo without valueField used in some tests -> addIndex(undefined). Safeguarding here
        if (indexConfig) {
            (me._indices || (me._indices = {}))[indexConfig.property] = new Map();

            // Piggyback the index config
            Object.assign(me._indices[indexConfig.property], indexConfig);

            // Indices need a rebuild now.
            me.invalidateIndices();

            if (indexConfig.dependentOn) {
                me.hasCompositeIndex = true;
            }
            /**
             * this.indices is keyed by the property name, and contains the keys linked to an item in the _values array.
             * So collection.add({id : foo, name : 'Nige'}, {id : 'bar', name : 'Faye'}) where collection has had an index
             * added for the "name" property would result in:
             *
             * {
             *     id : Map({
             *         foo : nige,
             *         bar : faye
             *     }),
             *     name : Map({
             *         Nige : nige,
             *         Faye : faye
             *     })
             * }
             */
        }
    }

    /**
     * Return the index of the item with the specified key having the specified value.
     *
     * By default, filtering is taken into account and this returns the index in the filtered dataset if present. To
     * bypass this, pass the third parameter as `true`.
     *
     * Only useful for indices configured with `unique: true`.
     *
     * @param {String} propertyName The name of the property to test.
     * @param {*} value The value to test for.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to return the index in
     * the original data set if the item is filtered out.
     * @returns {Number} The index of the item or `-1` if not found for unique indices
     */
    findIndex(propertyName, value, ignoreFilters = false) {
        const item = this.findItem(propertyName, value, ignoreFilters);

        if (!item) {
            return -1;
        }

        const values = this.isFiltered && !ignoreFilters ? this._filteredValues : this._values;

        return values.indexOf(item);
    }

    /**
     * Return the item with the specified key having the specified value.
     *
     * By default, filtering is taken into account. To bypass this, pass the third parameter as `true`.
     *
     * For indices configured with `unique: false`, a Set of items will be returned.
     *
     * @param {String} propertyName The name of the property to test.
     * @param {*} value The value to test for.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to return the index in
     * the original data set if the item is filtered out.
     * @returns {Object|Set} The found item or Set of items or null
     */
    findItem(propertyName, value, ignoreFilters = false) {
        const
            me             = this,
            { isFiltered } = me,
            index          = isFiltered && !ignoreFilters
                ? me.indices[filteredIndicesProperty][propertyName]
                : me.indices[propertyName];

        if (index) {
            // If the key is a numeric string, cast it to a number and find.
            // Store's idRegister is an object which treats numeric keys as strings
            // but Maps are more picky, so we have to work round that.
            const item = index.get(value) ?? ((typeof value === 'string' && value.length && !isNaN(value) && index.get(Number(value))) || null);

            if (item != null) {
                return item;
            }
        }
        else {
            // Search the filtered values if we are filtered and not ignoring filters
            const
                values = isFiltered && !ignoreFilters ? me._filteredValues : me._values,
                count  = values.length;

            for (let i = 0; i < count; i++) {
                const item = values[i];
                if (item[propertyName] == value) {
                    return item;
                }
            }
        }

        return null;
    }

    removeIndex(propertyName) {
        delete this._indices[propertyName];

        this.hasCompositeIndex = Object.values(this.indices).some(index => index.dependentOn);
    }

    /**
     * Returns the index of the item with the same `id` as the passed item.
     *
     * By default, filtering is honoured, so if the item in question has been added, but is currently filtered out of
     * visibility, `-1` will be returned.
     *
     * To find the index in the master, unfiltered dataset, pass the second parameter as `true`;
     * @param {Object|String|Number} item The item to find, or an `id` to find.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to find the index in the master, unfiltered data set.
     * @returns {Number} The index of the item, or `-1` if not found.
     */
    indexOf(item, ignoreFilters = false) {
        return this.findIndex(this.idProperty, keyTypes[typeof item] ? item : item[this.idProperty], ignoreFilters);
    }

    /**
     * Returns `true` if this Collection includes an item with the same `id` as the passed item.
     *
     * By default, filtering is honoured, so if the item in question has been added,
     * but is currently filtered out of visibility, `false` will be returned.
     *
     * To query inclusion in the master, unfiltered dataset, pass the second parameter as `true`;
     * @param {Object|String|Number} item The item to find, or an `id` to find.
     * @param {Boolean} [ignoreFilters=false] Pass `true` to find the index in the master, unfiltered data set.
     * @returns {Boolean} True if the passed item is found.
     */
    includes(item, ignoreFilters = false) {
        if (Array.isArray(item)) {
            return item.some(item => this.includes(item));
        }

        return Boolean(this.findItem(this.idProperty, keyTypes[typeof item] ? item : item[this.idProperty], ignoreFilters));
    }

    get indices() {
        if (this._indicesInvalid) {
            this.rebuildIndices();
        }
        return this._indices;
    }

    invalidateIndices() {
        this._indicesInvalid = true;
    }

    /**
     * Called when the Collection is mutated and the indices have been flagged as invalid.
     *
     * Rebuilds the indices object to allow lookup by keys.
     * @internal
     */
    rebuildIndices() {
        const
            me         = this,
            isFiltered = me.isFiltered,
            indices    = (me._indices || (me._indices = {})),
            keyProps   = Object.keys(indices),
            indexCount = keyProps.length,
            values     = me._values;

        let filteredIndices;

        if (isFiltered) {
            filteredIndices = indices[filteredIndicesProperty] = {};
        }

        // First, clear indices.
        for (let i = 0; i < indexCount; i++) {
            const index = indices[keyProps[i]];
            index.clear();

            if (isFiltered) {
                let filteredIndex = filteredIndices[keyProps[i]];

                if (filteredIndex) {
                    filteredIndex.clear();
                }
                else {
                    filteredIndex = filteredIndices[keyProps[i]] = new Map();
                    // Piggyback config
                    filteredIndex.unique = index.unique;
                }
            }
        }

        doRebuildIndices(values, indices, keyProps, indexCount);

        // Create a parallel lookup structure into the _filteredValues
        if (isFiltered) {
            doRebuildIndices(me._filteredValues, filteredIndices, keyProps, indexCount);
        }

        me._indicesInvalid = false;
    }

    // Returns an array with [indices] or [indices, filteredIndices] if filtering is used
    getIndices(propertyName) {
        const indices = [this.indices[propertyName]];

        if (this.isFiltered) {
            indices.push(this.indices[filteredIndicesProperty][propertyName]);
        }

        return indices;
    }

    /**
     * Add an item to all indices
     * @param {*} item Item already available in the Collection
     * @private
     */
    addToIndices(item) {
        Object.keys(this.indices).forEach(propertyName => {
            this.addIndexEntry(item, propertyName, item[propertyName]);
        });
    }

    /**
     * Remove an item from all indices
     * @param {*} item Item already available in the Collection
     * @private
     */
    removeFromIndices(item) {
        Object.keys(this.indices).forEach(propertyName => {
            this.removeIndexEntry(item, propertyName, item[propertyName]);
        });
    }

    /**
     * Remove an entry from an index, and if filtering is used also from the filtered index.
     * @param {*} item Item already available in the Collection
     * @param {String} propertyName Property of the item, will be matched with configured indices
     * @param {*} oldValue Value to remove
     * @private
     */
    removeIndexEntry(item, propertyName, oldValue) {
        this.getIndices(propertyName).forEach(index => removeItemFromIndex(item, index, oldValue));
    }

    /**
     * Add a new entry to an index, and if filtering is used also to the filtered index.
     * @param {*} item Item already available in the Collection
     * @param {String} propertyName Property of the item, will be matched with configured indices
     * @param {*} value Value to store
     * @private
     */
    addIndexEntry(item, propertyName, value) {
        this.getIndices(propertyName).forEach(index => addItemToIndex(item, index, value));
    }

    /**
     * Call externally to update indices on item mutation (from Store)
     * @param {*} item Item already available in the Collection
     * @param {Object} wasSet Uses the `wasSet` format from Store, `{ field : { oldValue, newValue } }`
     * @internal
     */
    onItemMutation(item, wasSet) {
        const me = this;

        // Iterate over changes if we have extra indices defined, keeping those indices up to date
        if (!me._indicesInvalid && Object.keys(me.indices).length > 1) {
            Object.keys(wasSet).forEach(propertyName => {
                const indexConfig = me.indices[propertyName];

                if (indexConfig) {
                    const { value, oldValue } = wasSet[propertyName];

                    me.removeIndexEntry(item, propertyName, oldValue);
                    me.addIndexEntry(item, propertyName, value);
                }
                // Might have both index and composite index
                if (me.hasCompositeIndex) {
                    // Now check if any composite index depends on the property that was changed
                    const dependentIndex = Object.values(me.indices).find(index => index.dependentOn?.[propertyName]);

                    if (dependentIndex) {
                        const keysAndOldValues = {};

                        for (const o in dependentIndex.dependentOn) {
                            keysAndOldValues[o] = wasSet[o]?.oldValue || item[o];
                        }

                        const oldIndex = item.buildIndexKey(keysAndOldValues);
                        me.removeIndexEntry(item, dependentIndex.property, oldIndex);
                        me.addIndexEntry(item, dependentIndex.property, item[dependentIndex.property]);
                    }
                }
            });
        }
    }

    //endregion
}

// These are used by Bag for the same purpose
export  { keyTypes };
