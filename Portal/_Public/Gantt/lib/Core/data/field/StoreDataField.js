import DataField from './DataField.js';
import ObjectHelper from '../../helper/ObjectHelper.js';
import StringHelper from '../../helper/StringHelper.js';

/**
 * @module Core/data/field/StoreDataField
 */

/**
 * This field class handles fields that accepts an array that is then converted to a store.
 *
 * ```javascript
 * class Task extends Model {
 *     static fields = [
 *         'name',
 *         // Store field
 *         { name : 'subTasks', type : 'store', storeClass : Store }
 *     ];
 * }
 * ```
 *
 * A record can be constructed like this:
 *
 * ```javascript
 * const task = new Task({
 *     name : 'Task 1',
 *     subTasks : [
 *         { text : 'Something', done : false },
 *         { text : 'Some other thing', done : true }
 *     ]
 * };
 * ```
 *
 * Or by populating a store:
 *
 * ```javascript
 * const store = new Store({
 *     modelClass : Task,
 *     data : [
 *         {
 *             name : 'Task 1',
 *             subTasks : [
 *                 { text : 'Something', done : false },
 *                 { text : 'Some other thing', done : true }
 *             ]
 *         },
 *         ...
 *     ]
 * });
 * ```
 *
 * Whenever the store or its records are manipulated, the field will be marked as modified:
 *
 * ```javascript
 * // These will all be detected as modifications
 * task.subTasks.first.done = true;
 * task.subTasks.last.remove();
 * task.subTasks.add({ text : 'New task', done : false });
 * ```
 *
 * <div class="note">Note that the underlying store by default will be configured with <code>syncDataOnLoad</code> set
 * to <code>true</code></div>
 *
 * @extends Core/data/field/DataField
 * @classtype store
 * @datafield
 */
export default class StoreDataField extends DataField {
    static $name = 'StoreDataField';

    static type = 'store';

    /**
     * Store class to use when creating the store.
     *
     * ```javascript
     * class TodoStore extends Store {
     *     ...
     * }
     *
     * const task = new Store({
     *     static fields = [
     *         { type : 'store', name: 'todoItems', storeClass : TodoStore }
     *     ]
     * });
     * ```
     *
     * @config {Class} storeClass
     * @typings {typeof Store}
     */

    /**
     * Model class to use for the store (can also be configured as usual on the store class, this config is for
     * convenience).
     *
     * ```javascript
     * class TodoItem extends Model {
     *   ...
     * }
     *
     * const task = new Store({
     *     static fields = [
     *         { type : 'store', name: 'todoItems', storeClass : Store, modelClass : TodoItem }
     *     ]
     * });
     * ```
     *
     * @config {Class} modelClass
     * @typings {typeof Model}
     */

    /**
     * Optional store configuration object to apply when creating the store.
     *
     * ```javascript
     * const task = new Store({
     *     static fields = [
     *         {
     *             type       : 'store',
     *             name       : 'todoItems',
     *             storeClass : Store
     *             store      : {
     *                  syncDataOnLoad : false
     *             }
     *         }
     *     ]
     * });
     * ```
     *
     * @config {StoreConfig} store
     */

    // Initializer, called when creating a record. Sets up the store and populates it with any initial data
    init(data, record) {
        const
            me        = this,
            storeName = `${me.name}Store`,
            config    = { skipStack : true, syncDataOnLoad : true }; // Optimization when used from sources, don't create a stack in Base

        if (me.store) {
            ObjectHelper.assign(config, me.store);
        }

        // Optionally apply modelClass, for convenient configuration
        if (me.modelClass) {
            config.modelClass = me.modelClass;
        }

        // Call optional initializer (initSubTasksStore for subTasks field) on the record, letting it manipulate the
        // config before creating a store
        record[`init${StringHelper.capitalize(storeName)}`]?.(config);

        if (!config.storeClass && !me.storeClass) {
            throw new Error(`Field '${me.name}' with type 'store' must have a storeClass configured`);
        }

        // Store has to be assigned on the record, field is shared
        const store = record.meta[storeName] = new (config.storeClass || me.storeClass)(config);

        if (me.complexMapping) {
            ObjectHelper.setPath(data, me.dataSource, store);
        }
        else {
            data[me.dataSource] = store;
        }

        // Don't warn about generated ids, responsibility lies elsewhere
        store.verifyNoGeneratedIds = false;
        // Keep track of if id should be included when serializing or not
        store.usesId = !store.count || !store.every(record => record.hasGeneratedId);
        // Cache value
        store.$currentValue = me.getValue(store);

        // Track changes to the store, applying them to the record and caching current value to be used when
        // serializing and in comparisons (required, otherwise we would be comparing to already updated store
        store.ion({
            change : ({ action }) => {
                const value = me.getValue(store);

                if (!store.$isSettingStoreFieldData) {
                    const oldPreserveCurrentDataset = store.$preserveCurrentDataset;

                    store.$preserveCurrentDataset = me.subStore && (
                        action === 'update' || action === 'remove' || action === 'add'
                    );

                    me.$isUpdatingRecord = true;
                    record.set(me.name, value);
                    me.$isUpdatingRecord = false;

                    store.$preserveCurrentDataset = oldPreserveCurrentDataset;
                }
                // cache the field current value
                store.$currentValue = value;
            }
        });
    }

    // Called when setting a new value to the field on a record
    set(value, data, record) {
        const
            me        = this,
            storeName = `${me.name}Store`,
            { [storeName] : store } = record.meta;

        // Lazy store might not be created yet, gets created on first access. Returning false keeps the value for later
        // if called during init
        if (!store) {
            // Missing store suggests value was not yet initialized and future value resides
            // in a special meta property. In which case we need to update it there
            record.meta.initableValues.set(me, value);

            return false;
        }

        // Prevent changes from leading to recursive calls
        if (store.$isSettingStoreFieldData) {
            return;
        }

        store.$isSettingStoreFieldData = true;

        // Call optional processing fn (processSubTasksStoreData for subTasks field) on the record, letting it
        // manipulate the data before creating records
        value = record[`process${StringHelper.capitalize(storeName)}Data`]?.(value, record) ?? value;

        // Apply incoming array to store
        if (!store.$preserveCurrentDataset) {
            store.data = value;
        }

        store.$isSettingStoreFieldData = false;

        // Keep track of if id should be included when serializing or not
        store.usesId = !store.count || !store.every(record => record.hasGeneratedId);
    }

    serialize(value, record) {
        const store = record.meta[`${this.name}Store`];
        return this.$isUpdatingRecord ? this.getValue(store) : store.$currentValue;
    }

    // Extract persistable values, optionally including id depending on if ids are used
    getValue(store) {
        return store.allRecords.map(r => {
            const data = r.persistableData;

            if (!store.usesId) {
                delete data.id;
            }

            return data;
        });
    }

    isEqual(a, b) {
        if (a?.isStore) {
            a = a.$currentValue;
        }

        if (b?.isStore) {
            b = b.$currentValue;
        }

        return ObjectHelper.isDeeplyEqual(a, b);
    }

    // Cloned value to be able to restore it later using STM
    getOldValue(record) {
        const store = record.meta[`${this.name}Store`];
        return store ? ObjectHelper.clone(store.$currentValue) : null;
    }

    getAt(record, index) {
        const store = record.meta[`${this.name}Store`];
        return store?.getAt(index);
    }
}

StoreDataField.initClass();
