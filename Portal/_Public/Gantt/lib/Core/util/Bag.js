import { keyTypes } from './Collection.js';
import ArrayHelper from '../helper/ArrayHelper.js';

const
    nonPrimitives          = new WeakMap(),
    safeIndexKey           = (value) => {
        if (value && typeof value === 'object') {
            let substitute = nonPrimitives.get(value);
            if (substitute === undefined) {
                substitute = Symbol('bscik'); // Bryntum safe collection index key
                nonPrimitives.set(value, substitute);
            }
            value = substitute;
        }

        return value;
    };

/**
 * @module Core/util/Bag
 */

/**
 * A simple collection class to contain unique, keyed items.
 * @internal
 */
export default class Bag {
    constructor(config) {
        const me = this;

        me.generation = 0;
        me.items = new Set();
        me.idMap = {};
        me.idProperty = 'id';

        if (config) {
            if (config.idProperty) {
                me.idProperty = config.idProperty;
            }
            if (config.values) {
                me.values = config.values;
            }
        }
    }

    /**
     * Returns the item with the passed `id`.
     *
     * @param {*} id The `id` to find.
     * @returns {Object} The found item, or `undefined`.
     */
    get(key) {
        return this.idMap[safeIndexKey(key)];
    }

    /**
     * The number of items in this Bag.
     * @property {Number}
     * @readonly
     */
    get count() {
        return this.items.size;
    }

    /**
     * Adds the passed item(s) to this Bag. Existing items with the same ID
     * will be replaced.
     * @param {Object|Object[]} toAdd Item(s) to add.
     */
    add(...toAdd) {
        if (toAdd.length === 1 && Array.isArray(toAdd[0])) {
            toAdd = toAdd[0];
        }

        const
            me  = this,
            {
                items,
                idMap,
                idProperty
            }   = me,
            len = toAdd.length;

        for (let i = 0; i < len; i++) {
            const
                item         = toAdd[i],
                key          = keyTypes[typeof item] ? item : safeIndexKey(item[idProperty]),
                existingItem = idMap[key];

            // Not already present
            if (existingItem == null) {
                idMap[key] = item;
                items.add(item);
                me.generation++;
            }
            // Already present, replace it if different. No generational change.
            // IDs rule.
            else if (existingItem !== item) {
                idMap[key] = item;
                items.delete(existingItem);
                items.add(item);
            }
        }
    }

    /**
     * Returns `nth` item in this Bag.
     * @param {Number} nth The index of the matching item. Negative numbers index for the last item. For example, -1
     * returns the last item, -2 the 2nd to last item etc.
     * @returns {Object} The matched item, or `undefined`.
     */
    at(nth) {
        let item, ret;

        if (nth < 0) {
            nth += this.count;  // ex: count=10 ... -1 -> 9, -2 -> 8, -3 -> 7, ... -9 -> 1, -10 -> 0
        }

        for (item of this.items) {
            if (!nth--) {
                ret = item;
                break;
            }
        }

        return ret;
    }

    /**
     * Removes the passed item(s) from this Bag.
     * @param {Object|Object[]} toRemove Item(s) to remove.
     */
    remove(toRemove) {
        toRemove = ArrayHelper.asArray(toRemove);

        const
            { items, idMap, idProperty } = this,
            len = toRemove.length;

        for (let i = 0; i < len; i++) {
            const
                item         = toRemove[i],
                key          = keyTypes[typeof item] ? item : safeIndexKey(item[idProperty]),
                existingItem = idMap[key];

            // Found match, so delete it
            if (existingItem != null) {
                items.delete(existingItem);
                delete idMap[key];
                this.generation++;
            }
        }
    }

    clear() {
        this.items.clear();
        this.idMap = {};
        this.generation++;
    }

    /**
     * Returns the number of items in this Bag which elicits a truthy return value from the passed function.
     * @param {Function} fn A function, which, when passed an item, returns `true` to select it as the item to return.
     * @returns {Number} The number of matched items.
     */
    countOf(fn) {
        let ret = 0,
            item;

        for (item of this.items) {
            if (fn(item)) {
                ++ret;
            }
        }

        return ret;
    }

    /**
     * Change the id of an existing member by mutating its idProperty.
     * @param {String|Number|Object} item The item or id of the item to change.
     * @param {String|Number} newId The id to set in the existing member.
     */
    changeId(item, newId) {
        const
            me                    = this,
            { idMap, idProperty } = me,
            oldId                 = keyTypes[typeof item] ? item : safeIndexKey(item[idProperty]),
            member                = me.get(oldId);

        if (member) {
            const existingMember = me.get(newId);

            if (existingMember && member !== existingMember) {
                throw new Error(`Attempt to set item ${oldId} to already existing member's id ${newId}`);
            }

            member[idProperty] = newId;
            delete idMap[oldId];
            idMap[newId] = member;
        }
    }

    /**
     * Extracts the matching items from this Bag into an array based upon the passed value filter function.
     * @param {Function} fn A function, which, when passed an item, returns a `true` to place into the resulting array.
     * @param {Object} [thisObj] The `this` reference when the function is called.
     * @returns {Object[]} An array of values extracted from this Bag.
     */
    filter(fn, thisObj) {
        const
            { items } = this,
            result    = [];

        let i = 0;

        items.forEach(item => {
            if (fn.call(thisObj, item, i++, items)) {
                result.push(item);
            }
        });

        return result;
    }

    /**
     * Returns `true` if this Collection includes an item with the same `id` as the passed item.
     *
     * @param {Object|String|Number} item The item to find, or an `id` to find.
     * @returns {Boolean} True if the passed item is found.
     */
    includes(item) {
        const key = keyTypes[typeof item] ? item : safeIndexKey(item[this.idProperty]);

        return Boolean(this.idMap[key]);
    }

    /**
     * Extracts the content of this Bag into an array based upon the passed
     * value extraction function.
     * @param {Function} fn A function, which, when passed an item, returns a value to place into the resulting array.
     * @param {Object} [thisObj] The `this` reference when the function is called.
     * @returns {Object[]} An array of values extracted from this Bag.
     */
    map(fn, thisObj) {
        const
            { items } = this,
            result    = new Array(items.size);

        // Set has no map
        let i = 0;

        items.forEach(item => {
            result[i] = fn.call(thisObj, item, i++, items);
        });

        return result;
    }

    /**
     * Executes the passed function for each item in this Bag, passing in the item.
     * @param {Function} fn The function to execute.
     * @param {Object} [thisObj] The `this` reference when the function is called.
     */
    forEach(fn, thisObj) {
        return this.items.forEach(fn, thisObj);
    }

    /**
     * Returns `nth` item in this Bag which elicits a truthy return value from the provided matcher function `fn`.
     * @param {Function} fn A function which, when passed an item, returns `true` to select the item as a match.
     * @param {Number} [nth=0] The index of the matching item. By default, 0 returns the first item that matches
     * according to `fn`. Negative numbers index for the last item. For example, -1 returns the last matching item,
     * -2 the 2nd to last matching item etc..
     * @returns {Object} The matched item, or `undefined`.
     */
    find(fn, nth = 0) {
        let item, ret;

        if (nth < 0) {
            nth += this.countOf(fn);  // ex: count=10 ... -1 -> 9, -2 -> 8, -3 -> 7, ... -9 -> 1, -10 -> 0
        }

        for (item of this.items) {
            if (fn(item) && !nth--) {
                ret = item;
                break;
            }
        }

        return ret;
    }

    /**
     * Iterator that allows you to do for (let item of bag)
     */
    [Symbol.iterator]() {
        return this.items[Symbol.iterator]();
    }

    indexOf(item, matchFn) {
        let index = -1,
            it;

        for (it of this.items) {
            if (!matchFn || matchFn(it)) {
                ++index;

                if (it === item) {
                    return index;
                }
            }
        }

        return -1;
    }

    /**
     * The set of values of this Bag.
     *
     * Setting this property replaces the data set.
     * @property {Object[]}
     */
    get values() {
        return [...this.items];
    }

    set values(values) {
        values = ArrayHelper.asArray(values);

        this.clear();
        this.add.apply(this, values);
        this.generation++;
    }

    /**
     * Sort the values of this Bag using the passed comparison function.
     *
     * Setting this property replaces the data set.
     * @param {Function} fn Comparison function which returns -ve, 0, or +ve
     */
    sort(fn) {
        this.values = this.values.sort(fn);
    }

    some(fn, thisObj) {
        return this.values.some(fn, thisObj);
    }
}
