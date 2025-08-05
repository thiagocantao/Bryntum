import Base from '../Base.js';
import Pluggable from '../mixin/Pluggable.js';
import Events from '../mixin/Events.js';
import State from '../mixin/State.js';
import Identifiable from '../mixin/Identifiable.js';
import Model from './Model.js';
import BrowserHelper from '../helper/BrowserHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import StringHelper from '../helper/StringHelper.js';
import StoreBag from './StoreBag.js';
import Collection from '../util/Collection.js';

import StoreCRUD from './mixin/StoreCRUD.js';
import StoreChanges from './mixin/StoreChanges.js';
import StoreFilter from './mixin/StoreFilter.js';
import StoreGroup from './mixin/StoreGroup.js';
import StoreProxy from './mixin/StoreProxy.js';
import StoreRelation from './mixin/StoreRelation.js';
import StoreSum from './mixin/StoreSum.js';
import StoreSearch from './mixin/StoreSearch.js';
import StoreSort from './mixin/StoreSort.js';
import StoreChained from './mixin/StoreChained.js';
import StoreState from './mixin/StoreState.js';
import StoreTree from './mixin/StoreTree.js';
import StoreSync from './mixin/StoreSync.js';
import StoreStm from './stm/mixin/StoreStm.js';
import Delayable from '../mixin/Delayable.js';

/**
 * @module Core/data/Store
 */
const
    dataAddRemoveActions   = {
        splice : 1,
        clear  : 1
    },
    defaultTraverseOptions = {
        includeFilteredOutRecords    : false,
        includeCollapsedGroupRecords : false
    },
    fixTraverseOptions     = (store, options) => {
        // backward compatibility to support includeFilteredOutRecords parameter instead of options
        options = options || false;


        if (typeof options === 'boolean') {
            options = {
                includeFilteredOutRecords    : options,
                includeCollapsedGroupRecords : false
            };
        }

        return options || defaultTraverseOptions;
    };

/**
 * The Store represents a data container which holds flat data or tree structures. An item in the Store is often called
 * a ´record´ and it is simply an instance of the {@link Core.data.Model} (or any subclass thereof).
 *
 * Typically you load data into a store to display it in a Grid or a ComboBox. The Store is the backing data component
 * for any component that is showing data in a list style UI.
 *
 * ## Data format
 * Data is store in a JSON array the Store offers an API to edit, filter, group and sort the records.
 *
 * ## Store with flat data
 * To create a flat store simply provide an array of objects that describe your records
 *
 * ```javascript
 * const store = new Store({
 *     data : [
 *         { id : 1, name : 'ABBA', country : 'Sweden' },
 *         { id : 2, name : 'Beatles', country : 'UK' }
 *     ]
 * });
 *
 * // retrieve record by id
 * const beatles = store.getById(2);
 * ```
 *
 * ## Store with tree data
 * To create a tree store use `children` property for descendant records
 *
 * ```javascript
 * const store = new Store({
 *     tree: true,
 *     data : [
 *         { id : 1, name : 'ABBA', country : 'Sweden', children: [
 *             { id: 2, name: 'Agnetha' },
 *             { id: 3, name: 'Bjorn' },
 *             { id: 4, name: 'Benny' },
 *             { id: 5, name: 'Anni-Frid' }
 *         ]},
 *     ]
 * });
 *
 * // retrieve record by id
 * let benny = store.getById(4);
 * ```
 *
 * Optionally a tree store can consume a flat dataset with nodes that have a `parentId` property. By configuring the
 * store with `tree : true` and `transformFlatData : true`, the flat data is transformed into tree data:
 *
 * ```javascript
 * const store = new Store({
 *     tree              : true,
 *     transformFlatData : true,
 *     data              : [
 *         { id : 1, name : 'ABBA', country : 'Sweden' },
 *         { id : 2, name : 'Agnetha', parentId : 1 },
 *         { id : 3, name : 'Bjorn', parentId : 1 },
 *         { id : 4, name : 'Benny', parentId : 1 },
 *         { id : 5, name : 'Anni-Frid', parentId : 1 }
 *     ]
 * });
 * ```
 *
 * ### Retrieving and consuming JSON
 * For both flat stores or tree stores it is possible to retrieve the data of all records in JSON format:
 *
 * ```javascript
 * const jsonString = store.json;
 *
 * // or
 *
 * const jsonArray = store.toJSON();
 * ```
 *
 * To plug the JSON data back in later:
 *
 * ```javascript
 * store.data = JSON.parse(jsonString);
 *
 * // or
 *
 * store.data = jsonArray;
 * ```
 *
 * ## Sharing stores
 * You cannot directly share a Store between widgets, but the data in a Store can be shared. There are two different
 * approaches depending on your needs, sharing data and chaining stores:
 *
 * ### Shared data
 * To create 2 widgets that share data, you can create 2 separate stores and pass records of the first store as the
 * dataset of the second store.
 *
 * ```javascript
 * let combo1 = new Combo({
 *     appendTo : document.body,
 *     store    : new Store({
 *         data : [
 *             { id : 1, name : 'ABBA', country : 'Sweden' },
 *             { id : 2, name : 'Beatles', country : 'UK' }
 *         ]
 *     }),
 *     valueField   : 'id',
 *     displayField : 'name'
 * });
 *
 * let combo2 = new Combo({
 *     appendTo : document.body,
 *     store    : new Store({
 *         data : combo1.store.records
 *     }),
 *     valueField   : 'id',
 *     displayField : 'name'
 * });
 *
 * combo1.store.first.name = 'foo';
 * combo2.store.first.name; // "foo"
 * ```
 *
 * ### Chained stores
 * Another more powerful option to share data between widgets is to create {@link Core.data.mixin.StoreChained chained}
 * stores. The easiest way to create a chained store is to call {@link #function-chain} function.
 *
 * ```javascript
 * let combo1 = new Combo({
 *     appendTo : document.body,
 *     store    : new Store({
 *         data : [
 *             { id : 1, name : 'ABBA', country : 'Sweden' },
 *             { id : 2, name : 'Beatles', country : 'UK' }
 *         ]
 *     }),
 *     valueField   : 'id',
 *     displayField : 'name'
 * });
 *
 * let combo2 = new Combo({
 *     appendTo     : document.body,
 *     store        : combo1.store.chain(),
 *     valueField   : 'id',
 *     displayField : 'name'
 * });
 *
 * combo1.store.first.name = 'foo';
 * combo2.store.first.name; // "foo"
 * ```
 *
 * A chained store can optionally be created with a filtering function, to only contain a subset of the records from
 * the main store. In addition, the chained store will reflect record removals/additions to the master store, something
 * the shared data approach will not.
 *
 * ## Non-homogeneous data structures
 *
 * You can use different Model classes to represent the records in the store by overriding the {@link #function-createRecord}
 * method:
 *
 * ```javascript
 * const store = new Store ({
 *     modelClass : Gate,
 *     readUrl    : 'data/the-airport.json',
 *     autoLoad   : true,
 *     // The default model is a Gate (see above) and in this createRecord method, we can decide at runtime based
 *     // on the data which model class to use. This is useful when your record types aren't homogenous.
 *     createRecord(data) {
 *         let modelClass = this.modelClass;
 *         if (data.type === 'terminal') {
 *             modelClass = Terminal;
 *         }
 *         return new modelClass(data, this);
 *     }
 * },
 * ```
 *
 * @mixes Core/mixin/Events
 * @mixes Core/data/mixin/StoreFilter
 * @mixes Core/data/mixin/StoreChanges
 * @mixes Core/data/mixin/StoreCRUD
 * @mixes Core/data/mixin/StoreSum
 * @mixes Core/data/mixin/StoreSearch
 * @mixes Core/data/mixin/StoreSort
 * @mixes Core/data/mixin/StoreGroup
 * @mixes Core/data/mixin/StoreChained
 * @mixes Core/data/mixin/StoreState
 * @mixes Core/data/mixin/StoreRelation
 * @mixes Core/data/mixin/StoreTree
 * @mixes Core/data/stm/mixin/StoreStm
 * @mixes Core/data/mixin/StoreSync
 *
 * @extends Core/Base
 */
export default class Store extends Base.mixin(
    Delayable,
    Identifiable,
    Events,
    Pluggable,
    State,
    StoreFilter,
    StoreChanges,
    StoreCRUD,
    StoreRelation, // Private
    StoreSum,
    StoreSearch,
    StoreSort,
    StoreGroup,
    StoreChained,
    StoreState,
    StoreTree,
    StoreStm,
    StoreSync,
    StoreProxy // Private for now, thus not mentioned in @mixes block above
) {
    //region Config & properties

    static get $name() {
        return 'Store';
    }

    static get properties() {
        return {
            relationCache         : {},
            dependentStoreConfigs : new Map()
        };
    }

    static get configurable() {
        return {
            /**
             * Store's unique identifier.
             *
             * @member {String|Number} id
             * @readonly
             * @category Common
             */
            /**
             * Store's unique identifier. When set the store is added to a store map accessible through
             * `Store.getStore(id)`.
             *
             * @config {String|Number}
             * @category Common
             */
            id : true,

            /**
             * Class used to represent records in the store, should be a subclass of {@link Core.data.Model}. Only
             * applies when supplying data to the store (load, add), any supplied record instances are kept as is.
             *
             * ```javascript
             * class MyModel extends Model {
             *     static get fields() {
             *         return [
             *             'name',
             *             'city',
             *             'company'
             *         ]
             *     }
             * }
             *
             * const store = new Store({
             *     modelClass : MyModel,
             *     data : [
             *         { id : 1, name : 'Mark', city : 'London', company : 'Cool inc' },
             *         ...
             *     ]
             * });
             * ```
             *
             * @config {Core.data.Model}
             * @default
             * @typings {typeof Model}
             * @category Common
             */
            modelClass : Model,

            /**
             * Verify that loaded data does not contain any generated ids. If it does, a warning is logged on console.
             *
             * Set this to `false` to disable the check and give a very minor performance boost.
             *
             * @prp {Boolean}
             * @default
             */
            verifyNoGeneratedIds : true
        };
    }

    static get defaultConfig() {
        return {
            /**
             * An array of field definitions used to create a {@link Core.data.Model} (modelClass) subclass. Optional.
             * If the Model already has fields defined, these fields will extend those.
             *
             * ```javascript
             * const store = new Store({
             *     fields : ['name', 'city', 'company'],
             *     data   : [
             *         { id : 1, name : 'Mark', city : 'London', company : 'Cool inc' },
             *         ...
             *     ]
             * });
             * ```
             *
             * See {@link Core.data.Model} for more info on defining fields, changing data source and mapping fields to
             * nested objects.
             *
             * Note that pre-created record instances supplied to the store are kept as is and thus these fields will
             * not apply to them.
             *
             * @config {Array<String|ModelFieldConfig|Core.data.field.DataField>}
             * @category Common
             */
            fields : null,

            /**
             * Automatically detect from set data if used as tree store or flat store
             * @config {Boolean}
             * @default
             * @category Tree
             */
            autoTree : true,

            /**
             * Raw data to load initially.
             *
             * Expects an array of JavaScript objects, with properties matching store's fields (defined on its
             * {@link #config-modelClass model} or in the {@link #config-fields} config).
             *
             * ```javascript
             * const store = new Store({
             *     data : [
             *         { id : 1, name : 'Linda', city : 'NY' },
             *         { id : 2, name : 'Olivia', city : 'Paris' },
             *         ...
             *     ]
             * });
             * ```
             *
             * @config {Object[]|Core.data.Model[]}
             * @category Common
             */
            data : null,

            /**
             * `true` to act as a tree store.
             * @config {Boolean}
             * @category Tree
             */
            tree : false,

            callOnFunctions : true,

            /**
             * A {@link Core/util/Collection}, or Collection config object
             * to use to contain this Store's constituent records.
             * @config {Core.util.Collection|CollectionConfig}
             * @category Advanced
             */
            storage : null,

            /**
             * Retools the loaded data objects instead of making shallow copies of them. This increases performance but
             * pollutes the incoming data and does not allow remapping of fields (dataSource).
             *
             * Also allows disabling certain steps in data loading, to further improve performance. Either accepts an
             * object with the params described below or `true` which equals `disableDuplicateIdCheck` and
             * `disableTypeConversion`.
             *
             * ```javascript
             * // No duplicate id checking, no type conversions
             * new Store({ useRawData : true });
             *
             * new Store({
             *   // No type conversions only
             *   useRawData : {
             *     disableTypeConversion : true
             *   }
             * });
             * ```
             *
             * @config {Boolean|Object}
             * @param {Boolean} [disableDuplicateIdCheck] Data must not contain duplicate ids, check is bypassed.
             * @param {Boolean} [disableDefaultValue] Default values will not be applied to record fields.
             * @param {Boolean} [disableTypeConversion] No type conversions will be performed on record data.
             * @category Advanced
             */
            useRawData : false,

            /**
             * Specify `false` to prevent loading records without ids, a good practise to enforce when syncing with a
             * backend.
             *
             * By default Store allows loading records without ids, in which case a generated id will be assigned.
             *
             * @config {Boolean}
             * @default true
             * @category Advanced
             */
            allowNoId : true,

            /**
             * Prevent dynamically subclassing the modelClass. It does so by default to not pollute it when exposing
             * properties. Should rarely need to be used.
             * @config {Boolean}
             * @default false
             * @private
             * @category Advanced
             */
            preventSubClassingModel : null
        };
    }

    static get identifiable() {
        return {
            registerGeneratedId : false
        };
    }

    /**
     * Class used to represent records. Defaults to class Model.
     * @member {Core.data.Model} modelClass
     * @typings {typeof Model}
     * @category Records
     */

    //endregion

    //region Events

    /**
     * Fired when the id of a record has changed
     * @event idChange
     * @param {Core.data.Store} source This Store
     * @param {Core.data.Model} record Modified record
     * @param {String|Number} oldValue Old id
     * @param {String|Number} value New id
     */

    /**
     * Fired before record is modified in this store.
     * Modification may be vetoed by returning `false` from a handler.
     * @event beforeUpdate
     * @param {Core.data.Store} source This Store
     * @param {Core.data.Model} record Modified record
     * @param {Object} changes Modification data
     */

    /**
     * Fired when a record is modified
     * @event update
     * @param {Core.data.Store} source This Store
     * @param {Core.data.Model} record Modified record
     * @param {Object} changes Modification data
     */

    /**
     * Fired when one of this Store's constituent records is modified while in
     * {@link Core.data.Model#function-beginBatch batched} state. This may be used to keep
     * UIs up to date while "tentative" changes are made to a record which must not be synced with a server.
     * @event batchedUpdate
     * @private
     */

    /**
     * Fired when the root node is set
     * @event rootChange
     * @param {Core.data.Store} source This Store
     * @param {Core.data.Model} oldRoot The old root node.
     * @param {Core.data.Model} rootNode The new root node.
     */

    /**
     * Data in the store was changed. This is a catch-all event which is fired for all changes
     * which take place to the store's data.
     *
     * This includes mutation of individual records, adding and removal of records, as well as
     * setting a new data payload using the {@link #property-data} property, sorting, filtering,
     * and calling {@link Core.data.mixin.StoreCRUD#function-removeAll}.
     *
     * Simple databound widgets may use to the `change` event to refresh their UI without having to add multiple
     * listeners to the {@link #event-update}, {@link Core.data.mixin.StoreCRUD#event-add},
     * {@link Core.data.mixin.StoreCRUD#event-remove}, {@link #event-refresh} and
     * {@link Core.data.mixin.StoreCRUD#event-removeAll} events.
     *
     * A more complex databound widget such as a grid may use the more granular events to perform less
     * destructive updates more appropriate to each type of change. The properties will depend upon the value of the
     * `action` property.
     *
     * @event change
     * @param {Core.data.Store} source This Store.
     * @param {'remove'|'removeAll'|'add'|'updatemultiple'|'clearchanges'|'filter'|'update'|'dataset'|'replace'} action
     * Name of action which triggered the change. May be one of the options listed above
     * @param {Core.data.Model} record Changed record, for actions that affects exactly one record (`'update'`)
     * @param {Core.data.Model[]} records Changed records, passed for all actions except `'removeAll'`
     * @param {Object} changes Passed for the `'update'` action, info on which record fields changed
     */
    // NOTE: When updating params above, also update change event in ProjectModelMixin and dataChange in ProjectConsumer

    /**
     * Data in the store has completely changed, such as by a filter, or sort or load operation.
     * @event refresh
     * @param {Core.data.Store} source This Store.
     * @param {Boolean} batch Flag set to `true` when the refresh is triggered by ending a batch
     * @param {'dataset'|'sort'|'clearchanges'|'filter'|'create'|'update'|'delete'|'group'} action Name of
     * action which triggered the change. May be one of the options listed above.
     */

    //endregion

    /* break doc comment from next method */

    //region Init

    constructor(...args) {
        super(...args);

        // When using a Proxy, the Proxy is returned instead of the actual Store
        if (this.objectify) {
            return this.initProxy();
        }
    }

    construct(config = {}) {
        const me = this;

        Object.assign(me, {
            added              : new StoreBag(),
            removed            : new StoreBag(),
            modified           : new StoreBag(),
            idRegister         : {},
            internalIdRegister : {},
            oldIdMap           : {}
        });

        if (config.storeId) {
            // avoid changing id when setting storeId:
            config = ObjectHelper.assign({ id : config.storeId }, config);

            // We cannot remove storeId since it can also be inherited and if so, it will override the id above:
            // delete config.storeId;
        }

        super.construct(config);

        me.initRelations();
    }

    /**
     * Retrieves/creates a store based on the passed config.
     *
     * | Type              | Result                                                                 |
     * |-------------------|------------------------------------------------------------------------|
     * | Core.data.Store   | Returns supplied store as is                                           |
     * | String            | Retrieves an existing store by id                                      |
     * | Object            | Creates a new store using supplied config object                       |
     * | Object[]          | Creates a new store, populated with records created from supplied data |
     * | Core.data.Model[] | Creates a new store, populated with supplied records                   |
     *
     *
     * @param {Core.data.Store|StoreConfig|String|StoreConfig[]|Core.data.Model[]} config
     * @param {Object} [defaults] Config object to apply when creating a new store for passed data
     * @param {Function} [converterFn] Function called for each data object prior to creating a record from it. The
     * return value is used to create a record.
     * @private
     */
    static from(config, defaults = {}, converterFn = null) {
        // null and store instances pass through
        if (config && !config.isStore) {
            // Passed a string, get store by id
            if (typeof config === 'string') {
                config = Store.getStore(config);
            }
            // Passed something else, create a store using the input as its data
            else {
                // Array of records or data, pass to converterFn if one is supplied
                if (Array.isArray(config)) {
                    if (converterFn) {
                        config = config.map(data => data.isModel ? data : converterFn(data));
                    }

                    config = ObjectHelper.assign({}, defaults, { data : config });
                }

                config = new Store(config);
            }
        }

        return config;
    }

    doDestroy() {
        const
            me         = this,
            allRecords = me.registeredRecords;

        // Remove from STM if added there (STM might also have gotten destroyed before us)
        me.stm?.removeStore?.(me);

        for (let i = allRecords.length - 1, rec; i >= 0; i--) {
            rec = allRecords[i];
            if (!rec?.isDestroyed) {
                rec.unjoinStore(me);
            }
        }

        me._storage?.destroy();

        if (!me.isChained) {
            me.rootNode?.destroy();
        }

        // Events superclass fires destroy event.
        super.doDestroy();

    }

    /**
     * Stops this store from firing events until {@link #function-endBatch} is called. Multiple calls to `beginBatch`
     * stack up, and will require an equal number of `endBatch` calls to resume events.
     *
     * Upon call of {@link #function-endBatch}, a {@link #event-refresh} event is triggered to allow UIs to
     * update themselves based upon the new state of the store.
     *
     * This is extremely useful when making a large number of changes to a store. It is important not to trigger
     * too many UI updates for performance reasons. Batching the changes ensures that UIs attached to this
     * store are only updated once at the end of the updates.
     */
    beginBatch() {
        this.suspendEvents();
    }

    /**
     * Ends event suspension started by {@link #function-beginBatch}. Multiple calls to {@link #function-beginBatch}
     * stack up, and will require an equal number of `endBatch` calls to resume events.
     *
     * Upon call of `endBatch`, a {@link #event-refresh} event with `action: batch` is triggered to allow UIs to update
     * themselves based upon the new state of the store.
     *
     * This is extremely useful when making a large number of changes to a store. It is important not to trigger
     * too many UI updates for performance reasons. Batching the changes ensures that UIs attached to this
     * store are only updated once at the end of the updates.
     */
    endBatch() {
        if (this.resumeEvents()) {
            this.trigger('refresh', {
                action  : 'batch',
                data    : this.storage.values,
                records : this.storage.values
            });
        }
    }

    set storage(storage) {
        const me = this;

        if (storage?.isCollection) {
            me._storage = storage;
        }
        else {
            me._storage = new Collection(storage);
        }
        me._storage.autoFilter = me.reapplyFilterOnAdd;

        me._storage.autoSort = me.reapplySortersOnAdd;

        // Join all the constituent records to this Store
        for (const r of me._storage) {
            r.joinStore(me);
        }
        me._storage.ion({
            change  : 'onDataChange',
            thisObj : me
        });
    }

    get storage() {
        if (!this._storage) {
            this.storage = {};
        }
        return this._storage;
    }

    /**
     * Returns all records (ignoring any filters) from the store.
     * @property {Core.data.Model[]}
     * @readonly
     * @category Records
     */
    get allRecords() {
        const me = this;

        if (me._allRecords?.generation !== me.storage.generation) {
            if (me.isTree) {
                const result = me.collectDescendants(me.rootNode, undefined, undefined, { unfiltered : true }).all;

                if (me.rootVisible) {
                    result.unshift(me.rootNode);
                }

                me._allRecords = result;
            }
            else {
                me._allRecords = me.isGrouped
                    ? me.collectGroupRecords()
                    : me.storage.allValues;
            }

            me._allRecords.generation = me.storage.generation;
        }

        return me._allRecords;
    }

    // All records except special rows such group headers etc
    getAllDataRecords(searchAllRecords) {
        const me = this;

        if (me.tree) {
            return searchAllRecords ? me.allRecords : me.rootNode.allChildren;
        }

        return me.isGrouped ? me.collectGroupRecords(searchAllRecords, false) : (searchAllRecords ? me.storage.allValues : me.storage.values);
    }

    /**
     * Called by owned record when the record has its {@link Core.data.Model#property-isCreating}
     * property toggled.
     * @param {Core.data.Model} record The record that is being changed.
     * @param {Boolean} isCreating The new value of the {@link Core.data.Model#property-isCreating} property.
     * @internal
     */
    onIsCreatingToggle(record, isCreating) {
        const
            me               = this,
            newlyPersistable = record.isPersistable && !isCreating;

        // If it's a transient "isCreating" record, waiting to be confirmed as a new entry
        // into the store, then it should *not* be in the added Bag as a syncable record.
        // If we are upgrading it to a permanent record, it *should* be in the added Bag.
        me.added[newlyPersistable ? 'add' : 'remove'](record);

        // If the record is newly persistable...
        if (newlyPersistable) {
            /**
             * Fired when a temporary record with the {@link Core.data.Model#property-isCreating} property set
             * has been confirmed as a part of this store by having its {@link Core.data.Model#property-isCreating}
             * property cleared.
             * @event addConfirmed
             * @param {Core.data.Store} source This Store.
             * @param {Core.data.Model} record The record confirmed as added.
             */
            me.trigger('addConfirmed', { record });

            // AjaxStore to commit confirmed new record
            if (me.autoCommit) {
                me.doAutoCommit();
            }
        }
    }

    // Join added records to store, not called when loading
    joinRecordsToStore(records) {
        const { allCount } = this;

        for (let i = 0; i < records.length; i++) {
            const record = records[i];
            // Set a parentIndex on newly added records, based on count prior to the add
            record.setData('parentIndex', allCount + i - records.length);
            record.joinStore(this);
        }
    }

    /**
     * Responds to mutations of the underlying storage Collection
     * @param {Object} event
     * @protected
     */
    onDataChange({ source : storage, action, added, removed, replaced, oldCount, items, from, to }) {
        const
            me           = this,
            isAddRemove  = dataAddRemoveActions[action],
            // The "filter" action's removed and added are not processed as adds and removes.
            // In a filter operation the records are still members of the store.
            addedCount   = isAddRemove && added?.length,
            removedCount = isAddRemove && removed?.length;

        let filtersWereReapplied,
            sortersWereReapplied;

        me._idMap = null;

        if (addedCount) {
            me.joinRecordsToStore(added);
        }

        replaced?.forEach(([oldRecord, newRecord]) => {
            oldRecord.unjoinStore(me, true);
            newRecord.joinStore(me);
        });

        // Allow mixins to mutate the storage before firing events.
        // StoreGroup does this to introduce group records into the mix.
        super.onDataChange(...arguments);

        // Join/unjoin incoming/outgoing records unless it's as a result of TreeNode operations.
        // If we are a tree, joining is done when nodes are added/removed
        // as child nodes of a joined parent.
        if (!me.isTree) {
            if (addedCount) {
                for (const record of added) {
                    // If was removed, remove from `removed` list
                    if (me.removed.includes(record)) {
                        me.removed.remove(record);
                    }
                    // Else add to `added` list
                    else if (!record.isLinked) {
                        me.added.add(record);
                    }
                }

                // Re-evaluate the current *local* filter set silently so that the
                // information we are broadcasting below is up-to-date.
                filtersWereReapplied = !me.remoteFilter && me.isFiltered && me.reapplyFilterOnAdd;
                if (filtersWereReapplied) {
                    me.filter({
                        silent : true
                    });
                }

                // if sortParamName not defined, is not remote sort
                sortersWereReapplied = !me.remoteSort && me.isSorted && me.reapplySortersOnAdd;
                if (sortersWereReapplied) {
                    me.sort(null, null, false, true);
                }
            }
            if (removedCount) {
                for (const record of removed) {
                    // If app was in the middle of a batched update, cancel the update.
                    record.cancelBatch();

                    record.unjoinStore(me);

                    // If was newly added, remove from `added` list
                    if (me.added.includes(record)) {
                        me.added.remove(record);
                    }
                    // Else add to `removed` list
                    // Unless it's StateTrackingManager reverting the record insertion.
                    // Also unless it's a record which was a transient record created by the UI
                    // and then the create was canceled at the edit stage.
                    else if (!record._undoingInsertion && !record.isCreating && !record.isLinked) {
                        me.removed.add(record);
                    }
                }
                me.modified.remove(removed);

                // Re-evaluate the current *local* filter set silently so that the
                // information we are broadcasting below is up-to-date.
                filtersWereReapplied = !me.remoteFilter && me.isFiltered;
                if (filtersWereReapplied) {
                    me.filter({
                        silent : true
                    });
                }
            }
        }

        switch (action) {
            case 'clear':
                // Clear our own relationCache, since we will be empty
                me.relationCache = {};

                // Signal to stores that depend on us
                me.updateDependentStores('removeall');

                me.trigger('removeAll');
                me.trigger('change', {
                    action : 'removeall'
                });
                break;

            case 'splice':
                if (addedCount) {
                    me.updateDependentStores('add', added);

                    const
                        // Collection does not handle moves, figure out if and where a record was moved from by checking
                        // previous index value stored in meta
                        oldIndex = added.reduce((lowest, record) => {
                            const { previousIndex } = record.meta;
                            if (previousIndex > -1 && previousIndex < lowest) lowest = previousIndex;
                            return lowest;
                        }, added[0].meta.previousIndex),

                        index    = storage.indexOf(added[0], !storage.autoFilter),

                        params   = {
                            records : added,
                            index
                        };

                    // Only include param oldIndex when used
                    if (oldIndex > -1) {
                        params.oldIndex = oldIndex;
                    }

                    me.trigger('add', params);

                    me.trigger('change', Object.assign({ action : 'add' }, params));

                    if (filtersWereReapplied) {
                        me.triggerFilterEvent({
                            action : 'filter', filters : me.filters, oldCount, records : me.storage.allValues
                        });
                    }

                    if (sortersWereReapplied) {
                        me.trigger('sort', { action : 'sort', sorters : me.sorters, records : me.storage.allValues });
                    }
                }

                if (removed.length) {
                    me.updateDependentStores('remove', removed);

                    me.trigger('remove', {
                        records : removed
                    });
                    me.trigger('change', {
                        action  : 'remove',
                        records : removed
                    });
                }

                if (replaced.length) {
                    me.trigger('replace', {
                        records : replaced,
                        all     : me.records.length === replaced.length
                    });
                    me.trigger('change', {
                        action : 'replace',
                        replaced,
                        all    : me.records.length === replaced.length
                    });
                }
                break;

            case 'filter':
                // Reapply grouping/sorting to make sure unfiltered records get sorted correctly
                if (me.isGrouped || me.isSorted) {
                    me.performSort(true);
                }
                break;

            case 'move': {
                // silently update parentIndex of records affected
                const
                    start = Math.min(from, to),
                    // We need to constrain maximum index in case record gets removed due to moving to the
                    // collapsed group
                    end   = Math.min(me.storage.allValues.length - 1, Math.max(from, to));

                for (let allRecords = me.storage.allValues, i = start; i <= end; i++) {
                    allRecords[i].setData('parentIndex', i);
                }

                /**
                 * Fired when a block of records has been moved within this Store
                 * @event move
                 * @param {Core.data.Store} source This Store
                 * @param {Core.data.Model} record (DEPRECATED) The first record moved (The
                 * {@link Core.data.mixin.StoreCRUD#function-move} API now accepts an array of records to move).
                 * @param {Core.data.Model[]} records The records moved.
                 * @param {Number} from The index from which the record was removed (applicable only for flat store).
                 * @param {Number} to The index at which the record was inserted (applicable only for flat store).
                 * @param {Core.data.Model} [newParent] The new parent record for the dragged records (applicable only for tree stores)
                 * @param {Core.data.Model[]} [oldParents] The old parent records for the dragged records (applicable only for move operations in tree stores)
                 */
                me.trigger('move', {
                    record  : items[0],
                    records : items,
                    from,
                    to
                });

                // The move was in real data. If we are filtered, the
                // filtered set has to be refreshed.
                if (me.isFiltered) {
                    me.performFilter();
                }
                me.trigger('change', {
                    action,
                    record  : items[0],
                    records : items,
                    from,
                    to
                });
                break;
            }
        }
    }

    onDataReplaced(action, data) {
        const
            me          = this,
            { storage } = me,
            all         = storage.allValues,
            sorted      = Boolean(me.sorters.length > 0);

        for (let i = 0; i < all.length; i++) {
            all[i].joinStore(me);
        }

        // The three operations below, filter, store and sort, all are passed
        // the "silent" parameter meaning they do not fire their own events.
        // The 'refresh' and 'change' events after are used to update UIs.
        if (!me.remoteFilter && me.isFiltered) {
            me.filter({
                silent : true
            });
        }

        if (me.remoteSort) {
            if (me.isGrouped) {
                storage.replaceValues({
                    // Need to update group records info (headers and footers)
                    ...me.prepareGroupRecords(),
                    silent : true
                });
            }
        }
        else {

            if (me.isGrouped) {
                me.group(null, null, false, !sorted, true);
            }

            // Only request sorting of arriving data if sorting is not remote.
            if (sorted) {
                me.sort(null, null, false, true);
            }
        }

        // Check for duplicate ids, unless user guarantees data validity
        if (!me.useRawData.disableDuplicateIdCheck) {
            const { idMap } = me;

            if (Object.keys(idMap).length < storage.values.length) {
                // idMap has fewer entries than expected, a duplicate id was used. pick idMap apart to find out which
                const collisions = [];

                storage.values.forEach(r => idMap[r.id] ? delete idMap[r.id] : collisions.push(r));

                throw new Error(`Id collision on ${collisions.map(r => r.id)}`);
            }
        }

        const event = { action, data, records : storage.values };

        me.updateDependentStores(action, event.records);

        // Allow subclasses to postprocess a new dataset
        me.afterLoadData?.();

        if (!me.isRemoteDataLoading) {
            me.trigger('refresh', event);
        }

        me.trigger('change', event);
    }

    /**
     * This is called from Model after mutating any fields so that Stores can take any actions necessary at that point,
     * and distribute mutation event information through events.
     * @param {Core.data.Model} record The record which has just changed
     * @param {Object} toSet A map of the field names and values that were passed to be set
     * @param {Object} wasSet A map of the fields that were set. Each property is a field name, and
     * the property value is an object containing two properties: `oldValue` and `value` eg:
     * ```javascript
     *     {
     *         name {
     *             oldValue : 'Rigel',
     *             value : 'Nigel'
     *         }
     *     }
     *
     * @param {Boolean} silent Do not trigger events
     * @param {Boolean} fromRelationUpdate Update caused by a change in related model
     * @private
     */
    onModelChange(record, toSet, wasSet, silent, fromRelationUpdate) {
        const
            me          = this,
            event       = {
                record,
                records : [record],
                changes : wasSet,
                // Cannot use isBatching, since change is triggered when batching has reached 0
                // (but before it is set to null)
                batch   : record.batching != null,
                fromRelationUpdate
            },
            committable = record.ignoreBag || record.isLinked ? false : me.updateModifiedBagForRecord(record);

        // Inform underlying collection of the changes, allowing it to keep any indices up to date
        me.storage.onItemMutation(record, wasSet);

        // Always update indices, otherwise they will be left out of date (was previously skipped when silent)
        if ('id' in wasSet) {
            const { oldValue, value } = toSet.id;

            me.updateDependentRecordIds(oldValue, value);

            me.onRecordIdChange({ record, oldValue, value });
        }

        if (!silent) {
            if ('id' in wasSet) {
                const { oldValue, value } = toSet.id;

                me.trigger('idChange', {
                    store : me,
                    record,
                    oldValue,
                    value
                });
            }

            me.onUpdateRecord(record, wasSet);

            me.trigger('update', event);
            me.trigger('change', Object.assign({ action : 'update' }, event));
        }

        if (me.autoCommit && committable) {
            me.doAutoCommit();
        }
    }

    updateModifiedBagForRecord(record) {
        const me       = this;
        let addedToBag = false;

        // Add or remove from our modified Bag
        if (record.isModified) {
            if (!me.modified.includes(record) && !me.added.includes(record) && record.isPartOfStore(me) && !record.isAutoRoot) {
                // When we add a new model first time and the model is not persistable (for example when the model is not valid),
                // it is not added to the "added" collection (StoreBag), but only joined to the store.
                // So if the record is not added neither to "modified" nor "added",
                // need to check if this record is phantom. If so, add it to the "added", otherwise to the "modified".
                if (record.isPhantom) {
                    me.added.add(record);
                }
                else {
                    me.modified.add(record);
                }

                addedToBag = true;
            }
        }
        else {
            me.modified.remove(record);
        }

        return addedToBag;
    }

    get idMap() {
        const
            me           = this,
            needsRebuild = !me._idMap,
            idMap        = me._idMap || (me._idMap = {});

        if (needsRebuild) {
            const processedRecords = me.storage.values;
            for (let record, index = 0, visibleIndex = 0; index < processedRecords.length; index++) {
                record           = processedRecords[index];
                idMap[record.id] = { index, visibleIndex, record };
                if (!record.isSpecialRow) {
                    visibleIndex++;
                }
            }

            // If store is filtered and grouped, we often need to lookup record index in filtered and unfiltered
            // collections
            if (me.isFiltered) {
                for (let index = 0, l = me.storage._values.length; index < l; index++) {
                    const record = me.storage._values[index];

                    if (record.id in idMap) {
                        idMap[record.id].unfilteredIndex = index;
                    }
                    else {
                        // If record is not in the idMap, set its index as -1 which allows
                        // `store.includes` API work correctly
                        idMap[record.id] = { index : -1, unfilteredIndex : index, record };
                    }
                }
            }
        }

        return idMap;
    }

    changeModelClass(ClassDef) {
        const { fields } = this;

        this.originalModelClass = ClassDef;

        let ClassDefEx = ClassDef;

        // Ensure our modelClass is exchanged for an extended of modelClass decorated with any configured fields.
        if (fields?.length) {
            // angular prod build messes up "Foo = class extends Base" (https://github.com/bryntum/support/issues/6395)
            class ModelClass extends ClassDef {
                static get fields() {
                    return fields;
                }
            }
            ClassDefEx = ModelClass;
        }
        // If we expose properties on Model we will pollute all other models, use internal subclass instead
        else if (!this.preventSubClassingModel) {
            // angular prod build messes up "Foo = class extends Base" (https://github.com/bryntum/support/issues/6395)
            class ModelClass extends ClassDef {}
            ClassDefEx = ModelClass;
        }

        // Need to properly expose relations on this new subclass
        ClassDefEx.initClass();

        return ClassDefEx;
    }

    //endregion

    //region Store id & map


    set storeId(storeId) {
        this.id = storeId;
    }

    get storeId() {
        return this.id;
    }

    changeId(id, oldId) {
        return super.changeId((id !== true) && id, oldId);
    }

    updateId(id, oldId) {

        const duplicate = Store.getById(id);

        duplicate && Store.unregisterInstance(duplicate);

        super.updateId(id, oldId);
    }

    generateAutoId() {
        return Store.generateId(`store-`);
    }

    get tree() {
        return this._tree;
    }

    set tree(tree) {
        this._tree = tree;

        if (tree && !this.rootNode) {
            this.rootNode            = this.buildRootNode();
            this.rootNode.isAutoRoot = true;
        }
    }

    // a hook to build a customized root node
    buildRootNode() {
        return {};
    }

    /**
     * Get a store from the store map by id.
     * @param {String|Number|Object[]} id The id of the store to retrieve, or an array of objects
     * from which to create the contents of a new Store.
     * @returns {Core.data.Store} The store with the specified id
     */
    static getStore(id, storeClass) {
        if (id instanceof Store) {
            return id;
        }
        if (this.getById(id)) {
            return this.getById(id);
        }
        if (Array.isArray(id)) {
            let storeModel;

            const storeData = id.map(item => {
                if (item instanceof Model) {
                    storeModel = item.constructor;
                }
                else if (typeof item === 'string') {
                    item = {
                        text : item
                    };
                }
                else {

                }
                return item;
            });

            if (!storeModel) {
                // angular prod build messes up "Foo = class extends Base" (https://github.com/bryntum/support/issues/6395)
                class ModelClass extends Model {}
                storeModel = ModelClass;
            }

            id = {
                autoCreated : true,
                data        : storeData,
                modelClass  : storeModel,
                allowNoId   : true // String items have no id and are not guaranteed to be unique
            };
            if (!storeClass) {
                storeClass = Store;
            }
        }
        if (storeClass) {
            return new storeClass(id);
        }
    }

    /**
     * Get all registered stores
     * @property {Core.data.Store[]}
     */
    static get stores() {
        return Store.registeredInstances;
    }

    //endregion

    //region Data

    /**
     * The invisible root node of this tree.
     * @property {Core.data.Model}
     * @readonly
     * @category Tree
     */
    get rootNode() {
        return this.masterStore ? this.masterStore.rootNode : this._rootNode;
    }

    set rootNode(rootNode) {
        const me      = this,
            oldRoot = me._rootNode;

        // No change
        if (rootNode === oldRoot) {
            return;
        }

        if (oldRoot) {
            me.clear(true);
        }
        if (rootNode instanceof Model) {
            // We insist that the rootNode is expanded otherwise no children will be added
            rootNode.instanceMeta(me).collapsed = false;

            me._rootNode = rootNode;
        }
        else {
            me._rootNode = rootNode = new me.modelClass(Object.assign({
                expanded                : true,
                [me.modelClass.idField] : `${me.id}-rootNode`
            }, rootNode), me, null, true);
        }
        me._tree        = true;
        rootNode.isRoot = true;
        rootNode.joinStore(me);

        // If there are nodes to be inserted into the flat storage
        // then onNodeAddChild knows how to do that and what events
        // to fire based upon rootNode.isLoading.
        if (rootNode.children?.length || me.rootVisible) {
            rootNode.isLoading = true;
            me.onNodeAddChild(rootNode, rootNode.children || [], 0);
            rootNode.isLoading = false;
        }

        me.trigger('rootChange', { oldRoot, rootNode });
    }

    /**
     * Sets data in the store.
     *
     * Expects an array of JavaScript objects, with properties matching store's fields (defined on its
     * {@link #config-modelClass model} or in the {@link #config-fields} config).
     *
     * Called on initialization if data is in config otherwise call it yourself after ajax call etc. Can also be used to
     * get the raw original data.
     *
     * ```javascript
     * store.data = [
     *     { id : 1, name : 'Linda', city : 'NY' },
     *     { id : 2, name : 'Olivia', city : 'Paris' },
     *     ...
     * ];
     * ```
     *
     * @property {Object[]}
     * @fires refresh
     * @fires change
     * @category Records
     */
    set data(data) {
        this.setStoreData(data);
    }

    // For overridability in engine
    setStoreData(data) {
        const
            me                         = this,
            { idField, childrenField } = me.modelClass;

        // Take a peek at first data row, and clone data if we are provided immutable objects
        if (me.transformFlatData && data?.length > 0 && !Object.isExtensible(data[0])) {
            // Avoid cloning object again in Model
            me.useRawData = me.useRawData || {
                disableDuplicateIdCheck : false,
                disableDefaultValue     : false,
                disableTypeConversion   : false
            };
            data          = ObjectHelper.clone(data);
        }

        // Make sure that if the plugins have not been processed yet, we call
        // the temporary property getter which configuration injects to
        // process plugins at this point. Some plugins are required to
        // operate on incoming data.
        me.getConfig('plugins');

        // In case data is loaded during configuration before configuredListeners have been processed
        me.processConfiguredListeners();

        // Allow data as a "named object", using keys as ids
        if (data && !Array.isArray(data)) {
            data = ObjectHelper.transformNamedObjectToArray(data, idField);
        }

        // Convert to being a tree store if any of the new rows have a children property
        me.tree = !me.isChained && (me.tree || Boolean(me.autoTree && data?.some(r => r[childrenField])));

        // Store received data order to preserve on sort if remote data loading enabled
        if (data && (me.remoteSort || me.remoteFilter)) {
            for (let i = 0; i < data.length; i++) {
                data[i]._remoteSortIndex = i;
            }
        }

        // Always load a new dataset initially
        if (!me.syncDataOnLoad || !me._data) {
            me._data = data;
            // This means load the root node
            if (me.tree) {
                if (me.transformFlatData) {
                    data = me.treeifyFlatData(data);
                }

                const root = me.rootNode;

                root.isLoading = true;

                // clear silently without marking as removed
                me.clear(true);
                // Append child will detect that this is a dataset operation and trigger sort + events needed
                root.appendChild(data);

                me.updateDependentStores('dataset', [root]);

                root.isLoading = false;

                if (data.length === 0) {
                    const event = { action : 'dataset', data : [], records : [] };
                    me.trigger('refresh', event);
                    me.trigger('change', event);
                }
                // we must re-apply filters for the filtered tree store
                else if (me.isFiltered) {
                    me.filter();
                }
            }
            else {
                me.loadData(data);
            }

            // loading the store discards all tracked changes
            me.added.clear();
            me.removed.clear();
            me.modified.clear();
        }
        // Sync dataset if configured to do so
        else {
            me.syncDataset(data);
        }
    }

    loadData(data, action = 'dataset') {
        const
            me                     = this,
            { storage, allowNoId } = me,
            idField                = me.modelClass.fieldMap.id.dataSource,
            creatingRecord         = me.find(rec => rec.isCreating);

        if (creatingRecord) {
            storage.values.splice(me.records.indexOf(creatingRecord), 1);
        }

        let warnGenerated = me.verifyNoGeneratedIds;

        // Need to unregister all groups
        me.removeHeadersAndFooters(me.storage.values);

        me._idMap   = null;
        me.oldIdMap = {};

        if (data) {
            const isRaw = !(data[0] instanceof Model);

            if (isRaw) {
                me.modelClass.exposeProperties(data[0]);

                const
                    count   = data.length,
                    records = new Array(count);

                for (let i = 0; i < count; i++) {
                    const recordData = data[i];

                    if (!allowNoId && recordData[idField] == null) {
                        throw new Error(`Id required but not found on row ${i}`);
                    }

                    if (warnGenerated && recordData[idField]?.startsWith?.('_generated')) {
                        console.warn(`Generated id found in data: ${recordData[idField]}. Generated ids are temporary and should be replaced with real ids by the backend`);
                        warnGenerated = false;
                    }

                    records[i] = me.processRecord(me.createRecord(recordData, true), true);
                    records[i].setData('parentIndex', i);
                }

                // clear without marking as removed
                me.clear(true);

                // Allow Collection's own filters to work on the Collection by
                // passing the isNewDataset param as true.
                // The storage Collection may have been set up with its own filters
                // while we are doing remote filtering. An example is ComboBox
                // with filterSelected: true. Records which are in the selection are
                // filtered out of visibility using a filter directly in the Combobox's
                // Store's Collection.
                storage.replaceValues({
                    values       : records,
                    isNewDataset : true,
                    silent       : true
                });
            }
            else {
                // clear without marking as removed
                me.clear(true);

                storage.replaceValues({
                    values       : data.slice(),
                    isNewDataset : true,
                    silent       : true
                });
            }

            if (creatingRecord && !storage.values.includes(creatingRecord)) {
                storage.values.push(creatingRecord);
            }
            me._data = data;
            me.onDataReplaced(action, data);
        }
        else {
            // clear without marking as removed
            me.clear(true);

            me._data = null;
        }

        me.isSyncingDataOnLoad = false;
    }

    get data() {
        return this._data;
    }

    /**
     * Creates an array of records from this store from the `start` to the `end' - 1
     * @param {Number} [start] The index of the first record to return
     * @param {Number} [end] The index *after* the last record to return `(start + length)`
     * @returns {Core.data.Model[]} The requested records.
     * @category Records
     */
    getRange(start, end, all = true) {
        return (all ? this.storage.allValues : this.storage.values).slice(start, end);
    }

    /**
     * Creates a model instance, used internally when data is set/added. Override this in a subclass to do your own custom
     * conversion from data to record.
     * @param {Object} data Json data
     * @param {Boolean} [skipExpose=false] Supply true when batch setting to not expose properties multiple times
     * @category Records
     */
    createRecord(data, skipExpose = false, rawData = false) {
        return new this.modelClass(data, this, null, skipExpose, false, rawData);
    }

    processRecord(record, isDataset = false) {
        return record;
    }

    refreshData() {
        this.filter();
        this.sort();
    }

    onRecordIdChange({ record, oldValue, value }) {
        const
            me                       = this,
            idMap                    = me._idMap,
            { idRegister, oldIdMap } = me;

        me.storage._indicesInvalid = true;

        // Remember the record used to have this identifier
        // this is used by STM to understand when a foreign key
        // value update really means targeting other record or
        // it's just a reaction to the target record id change
        oldIdMap[oldValue] = record;

        // Update idMap to reflect the changed id. Some code paths (auto syncing changes with CrudManager) will lead to
        // idMap already being up-to-date when we get here
        if (idMap && !idMap[value]) {
            const entry = idMap[oldValue];
            delete idMap[oldValue];
            idMap[value] = entry;
        }

        me.added.changeId(oldValue, value);
        me.removed.changeId(oldValue, value);
        me.modified.changeId(oldValue, value);

        delete idRegister[oldValue];
        idRegister[value] = record;

        record.index = me.storage.indexOf(record);
    }

    onUpdateRecord(record, changes) {
        const
            me                     = this,
            { internalId }         = changes,
            { internalIdRegister } = me;

        if (internalId) {
            this.storage._indicesInvalid = true;
            delete internalIdRegister[internalId.oldValue];
            internalIdRegister[internalId.value] = record;
        }

        // Reapply filters when records change?
        if (me.reapplyFilterOnUpdate && me.isFiltered) {
            me.filter();
        }
    }

    get useRawData() {
        return this._useRawData;
    }

    set useRawData(options) {
        if (options === true) {
            this._useRawData = {
                enabled                 : true,
                disableDuplicateIdCheck : true,
                disableTypeConversion   : true,
                disableDefaultValue     : false
            };
        }
        else {
            this._useRawData = options ? Object.assign(options, { enabled : true }) : { enabled : false };
        }
    }

    //endregion

    //region Count

    /**
     * Number of records in the store
     * @param {Boolean} [countProcessed] Count processed (true) or real records (false)
     * @returns {Number} Record count
     * @category Records
     */
    getCount(countProcessed = true) {
        return countProcessed ? this.count : this.originalCount;
    }

    /**
     * Record count, for data records. Not including records added for group headers etc.
     * @property {Number}
     * @readonly
     * @category Records
     */
    get originalCount() {
        return this.storage.totalCount - (this.groupRecords?.length || 0);
    }

    /**
     * Record count, including records added for group headers etc.
     * @property {Number}
     * @readonly
     * @category Records
     */
    get count() {
        return this.storage.count;
    }

    /**
     * Returns the complete dataset size regardless of tree node collapsing or filtering
     * @property {Number}
     * @readonly
     * @category Records
     */
    get allCount() {
        return this.isTree ? this.rootNode.descendantCount : this.storage.totalCount;
    }

    //endregion

    //region Get record(s)

    /**
     * Returns all "visible" records.
     * **Note:** The returned value **may not** be mutated!
     * @property {Core.data.Model[]}
     * @readonly
     * @immutable
     * @category Records
     */
    get records() {
        return this.storage.values;
    }

    /**
     * Get the first record in the store.
     * @property {Core.data.Model}
     * @readonly
     * @category Records
     */
    get first() {
        return this.storage.values[0];
    }

    /**
     * Get the last record in the store.
     * @property {Core.data.Model}
     * @readonly
     * @category Records
     */
    get last() {
        return this.storage.values[this.storage.values.length - 1];
    }

    /**
     * Get the record at the specified index
     * @param {Number} index Index for the record
     * @returns {Core.data.Model} Record at the specified index
     * @category Records
     */
    getAt(index, all = false) {
        // all means include filtered out records
        return this.storage.getAt(index, all);
    }

    // These are called by Model#join and Model#unjoin
    // register a record as a findable member keyed by id and internalId
    register(record) {
        const me = this;

        if (!me.useRawData.disableDuplicateIdCheck) {
            // Test for duplicate IDs on register only when a tree store.
            // loadData does it in the case of a non-tree
            const existingRec = me.isTree && me.idRegister[record.id];

            if (existingRec && existingRec !== record) {
                throw new Error(`Id collision on ${record.id}`);
            }
        }

        me.idRegister[record.id]                 = record;
        me.internalIdRegister[record.internalId] = record;
    }

    unregister(record) {
        delete this.idRegister[record.id];
        delete this.internalIdRegister[record.internalId];
    }

    get registeredRecords() {
        return Object.values(this.idRegister);
    }

    /**
     * Get a record by id. Find the record even if filtered out, part of collapsed group or collapsed node
     * @param {Core.data.Model|String|Number} id Id of record to return.
     * @returns {Core.data.Model} A record with the specified id
     * @category Records
     */
    getById(id) {
        // In case `id` is a record, we use its ID to try to find the record in the store,
        // because if the record is removed from the store it shouldn't be found.
        // if (id instanceof Model) {
        //     id = id.id;
        // }

        if (id?.isModel) {
            const record = id;

            // When asking for a record that has links, we resolve first link if original is not found.
            // This allows `linkedStore.isAvailable(original)` to return true and `linkedStore.getById(original)` to
            // return the linked record.
            if (record.hasLinks && !this.storage.allValues.includes(record)) {
                return record.$links.find(r => this.storage.allValues.includes(r));
            }

            return record;
        }

        //return this.tree ? this.idRegister[id] : this.storage.get(id);
        return this.idRegister[id];
    }

    /**
     * Checks if a record is available, in the sense that it is not filtered out,
     * hidden in a collapsed group or in a collapsed node.
     * @param {Core.data.Model|String|Number} recordOrId Record to check
     * @returns {Boolean}
     * @category Records
     */
    isAvailable(recordOrId) {
        const record = this.getById(recordOrId);

        return record && this.storage.includes(record) || false;
    }

    /**
     * Get a record by internalId.
     * @param {Number} internalId The internalId of the record to return
     * @returns {Core.data.Model} A record with the specified internalId
     * @category Records
     */
    getByInternalId(internalId) {
        return this.internalIdRegister[internalId];
    }

    /**
     * Checks if the specified record is contained in the store
     * @param {Core.data.Model|String|Number} recordOrId Record, or `id` of record
     * @returns {Boolean}
     * @category Records
     */
    includes(recordOrId) {
        if (this.isTree) {
            return this.idRegister[Model.asId(recordOrId)] != null;
        }

        return this.indexOf(recordOrId) > -1;
    }

    //endregion

    //region Get index

    /**
     * Returns the index of the specified record/id, or `-1` if not found.
     * @param {Core.data.Model|String|Number} recordOrId Record, or `id` of record to return the index of.
     * @param {Boolean} [visibleRecords] Pass `true` to find the visible index.
     * as opposed to the dataset index. This omits group header records.
     * @param {Boolean} [allExceptFilteredOutRecords] For trees, when true this searches all except filtered out records
     * in the flattened tree, similar to a flat store.
     * @returns {Number} Index for the record/id, or `-1` if not found.
     * @category Records
     */
    indexOf(recordOrId, visibleRecords = false, allExceptFilteredOutRecords = false) {
        // Only check records actually in the store ($store is for objectify scenario)
        if (recordOrId?.isModel && !recordOrId.stores.includes(this.$store || this)) {
            // When asking for a record that has links, we resolve first link if original is not found.
            // This allows finding index for links using relations to original records (dep -> event for example)
            const linkedRecord = recordOrId.$links.find(r => this.storage.allValues.includes(r));
            if (linkedRecord) {
                return this.indexOf(linkedRecord, visibleRecords);
            }

            return -1;
        }

        // When a tree, indexOf is always in the visible records - filtering is different in trees.
        if (this.isTree) {
            // Cheaper than this.storage.indexOf() which takes a detour to result in the same call
            return (allExceptFilteredOutRecords ? this.rootNode.allChildren : this.storage.values).indexOf(this.getById(recordOrId));
        }

        const id = Model.asId(recordOrId);

        if (id == null) {
            return -1;
        }

        const found = this.idMap[id];

        return found ? found[visibleRecords ? 'visibleIndex' : 'index'] : -1;
    }

    allIndexOf(recordOrId) {
        if (this.isTree) {
            return this.allRecords.indexOf(this.getById(recordOrId));
        }
        else {
            return this.storage.indexOf(recordOrId, true);
        }
    }

    //endregion

    //region Get values

    /**
     * Returns an array of distinct values for the specified field.
     *
     * ```javascript
     * store.getDistinctValues('age'); // Returns an array of the unique age values
     * ```
     *
     * @param {String} field Field to extract values for
     * @param {Boolean} [searchAllRecords] True to ignore any applied filters
     * @returns {Array} Array of values
     * @category Values
     */
    getDistinctValues(field, searchAllRecords = false) {
        const
            me     = this,
            values = [],
            keys   = {};
        let value;

        me.forEach(r => {
            if (!r.isSpecialRow && !r.isRoot) {
                value                = r.getValue(field);
                const primitiveValue = value instanceof Date ? value.getTime() : value;

                if (!keys[primitiveValue]) {
                    values.push(value);
                    keys[primitiveValue] = 1;
                }
            }
        }, me, searchAllRecords);

        return values;
    }

    /**
     * Counts how many times the specified value appears in the store
     * @param {String} field Field to look in
     * @param {*} value Value to look for
     * @returns {Number} Found count
     * @category Values
     */
    getValueCount(field, value) {
        let count = 0;

        this.forEach(r => {
            if (ObjectHelper.isEqual(r.getValue(field), value)) count++;
        });

        return count;
    }

    //endregion

    //region JSON & console

    /**
     * Retrieve or set the data of all records as a JSON string
     *
     * ```javascript
     * const store = new Store({
     *     data : [
     *         { id : 1, name : 'Superman' },
     *         { id : 2, name : 'Batman' }
     *     ]
     * });
     *
     * const jsonString = store.json;
     *
     * //jsonString:
     * '[{"id":1,"name":"Superman"},{"id":2,"name":"Batman"}]
     * ```
     *
     * @property {String}
     */
    set json(json) {
        if (typeof json === 'string') {
            json = StringHelper.safeJsonParse(json);
        }

        this.data = json;
    }

    get json() {
        return StringHelper.safeJsonStringify(this);
    }

    /**
     * Pretty printed version of {@link #property-json}
     * @readonly
     * @property {String}
     */
    get formattedJSON() {
        return StringHelper.safeJsonStringify(this, null, 4);
    }

    /**
     * Retrieve the data of all (unfiltered) records as an array of JSON objects.
     *
     * ```javascript
     * const store = new Store({
     *     data : [
     *         { id : 1, name : 'Superman' },
     *         { id : 2, name : 'Batman' }
     *     ]
     * });
     *
     * const jsonArray = store.toJSON();
     *
     * //jsonArray:
     * [{id:1,name:"Superman"},{id:2,name:"Batman"}]
     * ```
     *
     * @returns {Object[]}
     */
    toJSON() {
        // extract entire structure.
        // If we're a tree, then that consists of the payload of the rootNode.
        return (this.isTree ? this.rootNode.unfilteredChildren || this.rootNode.children || [] : this.allRecords).map(record => record.toJSON());
    }



    //endregion

    //region Extract config

    // These functions are not meant to be called by any code other than Base#getCurrentConfig()

    preProcessCurrentConfigs(configs) {
        super.preProcessCurrentConfigs(configs);

        delete configs.project;
    }

    // Extract current data for all accessible records
    getInlineData(options) {
        const data = [];

        if (this.tree) {
            this.rootNode.children?.forEach(r => data.push(r.getCurrentConfig(options)));
        }
        else {
            this.forEach(r => data.push(r.getCurrentConfig(options)));
        }

        return data;
    }

    // Extract current configs and data
    getCurrentConfig(options) {
        const
            result    = super.getCurrentConfig(options),
            { state } = this;

        if (result) {

            // Replace initial data with values from current records
            if (result.data) {
                result.data = this.getInlineData(options);
            }

            // Never include project or stm
            delete result.project;
            delete result.stm;
            delete result.asyncEvents;

            // Exclude default modelClass, gets added to config by engine, spam
            if (result.modelClass?.$meta.hierarchy[result.modelClass.$meta.hierarchy.length - 2] === this.constructor.defaultConfig.modelClass) {
                delete result.modelClass;
            }

            // Pollution from grid
            if (!this.tree) {
                delete result.tree;
            }

            // Include current state
            if (state) {
                Object.assign(result, state);
            }
        }

        return result;
    }

    //endregion

    //region Iteration & traversing

    /**
     * Iterates over all normal records in store. Omits group header and footer records if this store is grouped.
     * @param {Function} fn A function that is called for each record. Returning `false` from that function cancels
     * iteration. It is called with the following arguments:
     * @param {Core.data.Model} fn.record Current record
     * @param {Number} fn.index Current index
     * @param {Object} [thisObj] `this` reference for the function
     * @param {Object|Boolean} [options] A boolean for `includeFilteredOutRecords`, or detailed options for
     * exclude/include records
     * @param {Boolean} [options.includeFilteredOutRecords] `true` to also include filtered out records
     * @param {Boolean} [options.includeCollapsedGroupRecords] `true` to also include records from collapsed groups of
     * grouped store
     * @category Iteration
     */
    forEach(fn, thisObj = this, options) {
        const
            me       = this,
            callback = (r, i) => {
                if (!r.isRoot && !r.isSpecialRow) {
                    return fn.call(thisObj, r, i);
                }
            };

        options = fixTraverseOptions(me, options);

        if (me.isTree) {
            // forEach uses traverse() but is not perceived as a tree walk, so we want to apply our sorter
            if (me.isChained) {
                options = {
                    ...options,
                    sorterFn : me.sorterFn
                };
            }

            me.rootNode.traverseWhile(callback, false, options);
        }
        else {
            // native forEach cannot be aborted by returning false, have to loop "manually"
            const records = options.includeFilteredOutRecords ? me.storage.allValues : me.storage.values;

            // grouped store has own tree-like structure, but cannot be handled like a regular tree
            if (me.isGrouped && options.includeCollapsedGroupRecords) {
                for (let i = 0; i < records.length; i++) {
                    const
                        record        = records[i],
                        groupChildren = options.includeFilteredOutRecords ? record.unfilteredGroupChildren : record.groupChildren;

                    if (groupChildren && record.meta.collapsed === true) {
                        for (let j = 0; j < groupChildren.length; j++) {
                            const rec = groupChildren[j];

                            if (callback(rec, j) === false) {
                                return;
                            }
                        }
                    }
                    else if (callback(record, i) === false) {
                        return;
                    }
                }
            }
            else {
                for (let i = 0; i < records.length; i++) {
                    if (callback(records[i], i) === false) {
                        return;
                    }
                }
            }
        }
    }

    /**
     * Equivalent to Array.map(). Creates a new array with the results of calling a provided function on every record
     * @param {Function} fn
     * @param {Object} [thisObj] The `this` reference to call the function with. Defaults to this Store
     * @returns {Array}
     * @category Iteration
     */
    map(fn, thisObj = this) {
        return this.storage.values.map(fn, thisObj);
    }

    /**
     * Equivalent to {@link https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/flatMap Array.flatMap()}.
     * Creates a new array by spreading the results of calling a provided function on every record
     * @param {Function} fn A function returning an array of items to be spread into the new array, or a single item to include in it
     * @param {Object} [thisObj] The `this` reference to call the function with. Defaults to this Store
     * @returns {Array} The new array
     * @category Iteration
     */
    flatMap(fn, thisObj = this) {
        return this.storage.values.flatMap(fn, thisObj);
    }

    /**
     * Equivalent to Array.every(). Returns `true` if every call of the provided function
     * on each record yields a truthy value.
     * @param {Function} fn
     * @param {Function} fn.record The record to test.
     * @param {Object} [thisObj] The `this` reference to call the function with. Defaults to this Store.
     * @param {Boolean} [ignoreFilters] Pass `true` to iterate all records including filtered out ones.
     * @returns {Array}
     * @category Iteration
     */
    every(fn, thisObj = this, ignoreFilters) {
        return this.storage[ignoreFilters ? 'allValues' : 'values'].every(fn, thisObj);
    }

    /**
     * Equivalent to Array.reduce(). Applies a function against an accumulator and each record (from left to right) to
     * reduce it to a single value.
     * @param {Function} fn
     * @param {*} initialValue
     * @param {Object} [thisObj] The `this` reference to call the function with. Defaults to this Store
     * @returns {*}
     * @category Iteration
     */
    reduce(fn, initialValue = [], thisObj = this) {
        if (thisObj !== this) {
            fn = fn.bind(thisObj);
        }

        return this.storage.values.reduce(fn, initialValue, thisObj);
    }

    /**
     * Iterator that allows you to do for (let record of store)
     * @category Iteration
     */
    [Symbol.iterator]() {
        return this.storage.values[Symbol.iterator]();
    }

    /**
     * Traverse all tree nodes (only applicable for Tree Store)
     * @param {Function} fn The function to call on visiting each node.
     * @param {Core.data.Model} [topNode=this.rootNode] The top node to start the traverse at.
     * @param {Boolean} [skipTopNode] Pass true to not call `fn` on the top node, but proceed directly to its children.
     * @param {Object|Boolean} [options] A boolean for includeFilteredOutRecords, or detailed options for exclude/include records
     * @param {Boolean} [options.includeFilteredOutRecords] True to also include filtered out records
     * @param {Boolean} [options.includeCollapsedGroupRecords] True to also include records from collapsed groups of grouped store
     * @param {Boolean} [options.useOrderedTree] True to traverse unsorted/unfiltered tree
     * @category Traverse
     */
    traverse(fn, topNode = this.rootNode, skipTopNode = topNode === this.rootNode, options) {
        const me = this;

        options = fixTraverseOptions(me, options);

        if (me.isTree) {
            // Allow store.traverse(fn, true) to start from rootNode
            if (typeof topNode === 'boolean') {
                skipTopNode = topNode;
                topNode     = me.rootNode;
            }

            if (me.isChained) {
                const passedFn = fn;

                fn = node => {
                    if (me.chainedFilterFn(node)) {
                        passedFn(node);
                    }
                };
            }

            topNode.traverse(fn, skipTopNode, options);
        }
        else {
            me.forEach(rec => rec.traverse(fn, false, options), me, options);
        }
    }

    /**
     * Traverse all tree nodes while the passed `fn` returns true
     * @param {Function} fn The function to call on visiting each node. Returning `false` from it stops the traverse.
     * @param {Core.data.Model} [topNode=this.rootNode] The top node to start the traverse at.
     * @param {Boolean} [skipTopNode] Pass true to not call `fn` on the top node, but proceed directly to its children.
     * @param {Object} [options] An options object to exclude/include records
     * @param {Boolean} [options.includeFilteredOutRecords] True to also include filtered out records
     * @param {Boolean} [options.includeCollapsedGroupRecords] True to also include records from collapsed groups of grouped store
     * @category Traverse
     */
    traverseWhile(fn, topNode = this.rootNode, skipTopNode = topNode === this.rootNode, options) {
        const me = this;

        options = fixTraverseOptions(me, options);

        if (me.isTree) {
            // Allow store.traverse(fn, true) to start from rootNode
            if (typeof topNode === 'boolean') {
                skipTopNode = topNode;
                topNode     = me.rootNode;
            }

            if (me.isChained) {
                const passedFn = fn;

                fn = node => {
                    if (me.chainedFilterFn(node)) {
                        passedFn(node);
                    }
                };
            }

            topNode.traverseWhile(fn, skipTopNode, options);
        }
        else {
            for (const record of me.storage) {
                if (record.traverse(fn, false, options) === false) {
                    break;
                }
            }
        }
    }

    /**
     * Finds the next record.
     * @param {Core.data.Model|String|Number} recordOrId Current record or its id
     * @param {Boolean} [wrap=false] Wrap at start/end or stop there
     * @param {Boolean} [skipSpecialRows=false] True to not return specialRows like group headers
     * @returns {Core.data.Model} Next record or null if current is the last one
     * @category Traverse
     */
    getNext(recordOrId, wrap = false, skipSpecialRows = false) {
        const
            me      = this,
            records = me.storage.values;
        let idx     = me.indexOf(recordOrId);

        if (idx >= records.length - 1) {
            if (wrap) {
                idx = -1;
            }
            else {
                return null;
            }
        }

        const record = records[idx + 1];

        // Skip the result if it's a specialRow and we are told to skip them
        if (skipSpecialRows && record && record.isSpecialRow) {
            return me.getNext(records[idx + 1], wrap, true);
        }

        return record;
    }

    /**
     * Finds the previous record.
     * @param {Core.data.Model|String|Number} recordOrId Current record or its id
     * @param {Boolean} [wrap=false] Wrap at start/end or stop there
     * @param {Boolean} [skipSpecialRows=false] True to not return specialRows like group headers
     * @returns {Core.data.Model} Previous record or null if current is the last one
     * @category Traverse
     */
    getPrev(recordOrId, wrap = false, skipSpecialRows = false) {
        const
            me      = this,
            records = me.storage.values;
        let idx     = me.indexOf(recordOrId);

        if (idx === 0) {
            if (wrap) {
                idx = records.length;
            }
            else {
                return null;
            }
        }

        const record = records[idx - 1];

        // Skip the result if it's a specialRow and we are told to skip them
        if (skipSpecialRows && record && record.isSpecialRow && idx > 0) {
            return me.getPrev(records[idx - 1], wrap, true);
        }

        return record;
    }

    /**
     * Gets the next or the previous record. Optionally wraps from first -> last and vice versa
     * @param {String|Core.data.Model} recordOrId Record or records id
     * @param {Boolean} next Next (true) or previous (false)
     * @param {Boolean} wrap Wrap at start/end or stop there
     * @param {Boolean} [skipSpecialRows=false] True to not return specialRows like group headers
     * @returns {Core.data.Model}
     * @category Traverse
     * @internal
     */
    getAdjacent(recordOrId, next = true, wrap = false, skipSpecialRows = false) {
        return next ? this.getNext(recordOrId, wrap, skipSpecialRows) : this.getPrev(recordOrId, wrap, skipSpecialRows);
    }

    /**
     * Finds the next record among leaves (in a tree structure)
     * @param {Core.data.Model|String|Number} recordOrId Current record or its id
     * @param {Boolean} [wrap] Wrap at start/end or stop there
     * @returns {Core.data.Model} Next record or null if current is the last one
     * @category Traverse
     * @internal
     */
    getNextLeaf(recordOrId, wrap = false) {
        const
            me      = this,
            records = me.leaves,
            record  = me.getById(recordOrId);
        let idx     = records.indexOf(record);

        if (idx >= records.length - 1) {
            if (wrap) {
                idx = -1;
            }
            else {
                return null;
            }
        }

        return records[idx + 1];
    }

    /**
     * Finds the previous record among leaves (in a tree structure)
     * @param {Core.data.Model|String|Number} recordOrId Current record or its id
     * @param {Boolean} [wrap] Wrap at start/end or stop there
     * @returns {Core.data.Model} Previous record or null if current is the last one
     * @category Traverse
     * @internal
     */
    getPrevLeaf(recordOrId, wrap = false) {
        const
            me      = this,
            records = me.leaves,
            record  = me.getById(recordOrId);
        let idx     = records.indexOf(record);

        if (idx === 0) {
            if (wrap) {
                idx = records.length;
            }
            else {
                return null;
            }
        }

        return records[idx - 1];
    }

    /**
     * Gets the next or the previous record among leaves (in a tree structure). Optionally wraps from first -> last and
     * vice versa
     * @param {String|Core.data.Model} recordOrId Record or record id
     * @param {Boolean} [next] Next (true) or previous (false)
     * @param {Boolean} [wrap] Wrap at start/end or stop there
     * @returns {Core.data.Model}
     * @category Traverse
     * @internal
     */
    getAdjacentLeaf(recordOrId, next = true, wrap = false) {
        return next ? this.getNextLeaf(recordOrId, wrap) : this.getPrevLeaf(recordOrId, wrap);
    }

    //endregion
}

Store.initClass();
