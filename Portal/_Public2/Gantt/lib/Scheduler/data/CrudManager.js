import ResourceStore from './ResourceStore.js';
import EventStore from './EventStore.js';
import AssignmentStore from './AssignmentStore.js';
import DependencyStore from './DependencyStore.js';
import Store from '../../Core/data/Store.js';
import ProjectCrudManager from './mixin/ProjectCrudManager.js';
import AbstractCrudManager from '../crud/AbstractCrudManager.js';
import AjaxTransport from '../crud/transport/AjaxTransport.js';
import JsonEncoder from '../crud/encoder/JsonEncoder.js';

/**
 * @module Scheduler/data/CrudManager
 */

/**
 * The Crud Manager (or "CM") is a class implementing centralized loading and saving of data in multiple stores.
 * Loading the stores and saving all changes is done using a single AJAX request.
 * This class uses JSON as the data encoding format.
 *
 * ## Scheduler stores
 *
 * The class supports Scheduler specific stores (namely: resource, event, assignment and dependency stores).
 * For these stores, the CM has separate configs ({@link #config-resourceStore}, {@link #config-eventStore},
 * {@link #config-assignmentStore}) to register them.
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     autoLoad        : true,
 *     resourceStore   : resourceStore,
 *     eventStore      : eventStore,
 *     assignmentStore : assignmentStore,
 *     transport       : {
 *         load : {
 *             url : 'php/read.php'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 * ```
 *
 * ## AJAX request configuration
 *
 * To configure AJAX request parameters please take a look at the
 * {@link Scheduler.crud.transport.AjaxTransport AjaxTransport} docs.
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     autoLoad        : true,
 *     resourceStore,
 *     eventStore,
 *     assignmentStore,
 *     transport       : {
 *         load    : {
 *             url         : 'php/read.php',
 *             // use GET request
 *             method      : 'GET',
 *             // pass request JSON in "rq" parameter
 *             paramName   : 'rq',
 *             // extra HTTP request parameters
 *             params      : {
 *                 foo     : 'bar'
 *             },
 *             // pass some extra Fetch API option
 *             credentials : 'include'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 * ```
 *
 * ## Load order
 *
 * The CM is aware of the proper load order for Scheduler specific stores so you don't need to worry about it.
 * If you provide any extra stores (using {@link Scheduler.crud.AbstractCrudManager#config-stores} config) they will be
 * added to the start of collection before the Scheduler specific stores.
 * If you need a different loading order, you should use {@link Scheduler.crud.AbstractCrudManager#function-addStore} method to
 * register your store:
 *
 * ```javascript
 * const crudManager = new CrudManager({
 *     resourceStore   : resourceStore,
 *     eventStore      : eventStore,
 *     assignmentStore : assignmentStore,
 *     // extra user defined stores will get to the start of collection
 *     // so they will be loaded first
 *     stores          : [ store1, store2 ],
 *     transport       : {
 *         load : {
 *             url : 'php/read.php'
 *         },
 *         sync : {
 *             url : 'php/save.php'
 *         }
 *     }
 * });
 *
 * // append store3 to the end so it will be loaded last
 * crudManager.addStore(store3);
 *
 * // now when we registered all the stores let's load them
 * crudManager.load();
 * ```
 *
 * ## Assignment store
 *
 * CrudManager is designed to use AssignmentStore for assigning events to one or multiple resources.
 * However if server provides `resourceId` for any of the `events` then CrudManager enables backward compatible mode when an event could have a single assignment only. This also disables multiple assignments in Scheduler UI.
 * In order to use multiple assignments server backend should be able to receive/send `assignments` for `load` and `sync` requests.
 *
 * ## Features
 *
 * The Crud Manager can automatically add Scheduler feature stores to the tracked collection.
 * For example, it tracks TimeRanges {@link Scheduler.feature.TimeRanges#config-store}.
 * You can receive a tracked store by its id:
 * ```javascript
 * const timeRangesStore = crudManager.getStore('timeRanges');
 * ```
 *
 * @mixes Scheduler/data/mixin/ProjectCrudManager
 * @mixes Scheduler/crud/encoder/JsonEncoder
 * @mixes Scheduler/crud/transport/AjaxTransport
 * @extends Scheduler/crud/AbstractCrudManager
 */

export default class CrudManager extends AbstractCrudManager.mixin(ProjectCrudManager, AjaxTransport, JsonEncoder) {

    //region Config

    static get defaultConfig() {
        return {
            resourceStoreClass   : ResourceStore,
            eventStoreClass      : EventStore,
            assignmentStoreClass : AssignmentStore,
            dependencyStoreClass : DependencyStore,

            /**
             * A store with resources (or its descriptor).
             * @config {Scheduler.data.ResourceStore|Object}
             */
            resourceStore : {},

            /**
             * A store with events (or its descriptor).
             *
             * ```
             * crudManager : {
             *      eventStore {
             *          storeClass : MyEventStore
             *      }
             * }
             * ```
             * @config {Scheduler.data.EventStore|Object}
             */
            eventStore : {},

            /**
             * A store with assignments (or its descriptor).
             * @config {Scheduler.data.AssignmentStore|Object}
             */
            assignmentStore : {},

            /**
             * A store with dependencies (or its descriptor).
             * @config {Scheduler.data.DependencyStore|Object}
             */
            dependencyStore : {},

            /**
             * A project that holds and links stores
             * @config {Scheduler.model.ProjectModel}
             */
            project : null,

            scheduler : null
        };
    }

    //endregion

    startConfigure(config) {
        if (config.scheduler) {
            // The effects of this class's initialization are so wide ranging that they may need to
            // access the Scheduler's CrudManager, so ensure it's available immediately.
            config.scheduler._crudManager = this;
        }

        super.startConfigure(config);
    }

    afterConstruct() {
        const
            me            = this,
            { scheduler } = me;

        // Created by being configured on a scheduler
        if (scheduler) {
            // Features self initialize if not already initialized.
            // This must be done after the _crudManager is assigned because it may access this.crudManager
            // to get its eventStore.
            const { timeRanges } = scheduler.features; // Must pull resourceTimeRanges in here. TODO: Refactor this flow, it is weird

            if (timeRanges) {
                me.addCrudStore(timeRanges.store);
                me._timeRangesStore = timeRanges.store;
            }
        }
        // Created as a standalone instance, create stores to load if none supplied (and plug them into scheduler later)
        // (Not using {} as default value on the configs to not create unneeded stores when running the code above)
        else if (!me.project) {
            me.project = null;
        }

        super.afterConstruct();
    }

    //region Stores

    set project(project) {
        const me = this;

        me._project = project;

        if (project) {
            me.eventStore = project.eventStore;
            me.resourceStore = project.resourceStore;
            me.assignmentStore = project.assignmentStore;
            me.dependencyStore = project.dependencyStore;
            me.resourceTimeRangeStore = project.resourceTimeRangeStore;
        }

        if (!me.eventStore) {
            me.eventStore = {};
        }
        if (!me.resourceStore) {
            me.resourceStore = {};
        }
        if (!me.assignmentStore) {
            me.assignmentStore = {};
        }
        if (!me.dependencyStore) {
            me.dependencyStore = {};
        }
    }

    get project() {
        return this._project;
    }

    /**
     * Returns store associated with timeRanges feature, if feature is enabled.
     * @property {Core.data.Store}
     * @readonly
     */
    get timeRangesStore() {
        return this._timeRangesStore;
    }

    /**
     * Get/set the resource store bound to the CRUD manager.
     * @property {Scheduler.data.ResourceStore}
     */
    get resourceStore() {
        return this._resourceStore?.store;
    }

    set resourceStore(store) {
        const me = this;

        me.setFeaturedStore('_resourceStore', store, me.resourceStoreClass);
    }

    /**
     * Get/set the event store bound to the CRUD manager.
     * @property {Scheduler.data.EventStore}
     */
    get eventStore() {
        return this._eventStore?.store;
    }

    set eventStore(store) {
        const me = this;

        me.setFeaturedStore('_eventStore', store, me.eventStoreClass);
    }

    /**
     * Get/set the assignment store bound to the CRUD manager.
     * @property {Scheduler.data.AssignmentStore}
     */
    get assignmentStore() {
        return this._assignmentStore?.store;
    }

    set assignmentStore(store) {
        this.setFeaturedStore('_assignmentStore', store, this.assignmentStoreClass);
    }

    /**
     * Get/set the dependency store bound to the CRUD manager.
     * @property {Scheduler.data.DependencyStore}
     */
    get dependencyStore() {
        return this._dependencyStore?.store;
    }

    set dependencyStore(store) {
        this.setFeaturedStore('_dependencyStore', store, this.dependencyStoreClass);
    }

    set resourceTimeRangeStore(store) {
        this._resourceTimeRangesStore = store;
        this.addCrudStore(store);
    }

    get resourceTimeRangeStore() {
        return this._resourceTimeRangesStore;
    }

    setFeaturedStore(property, store, storeClass) {
        const
            me       = this,
            oldStore = me[property]?.store;

        // if not the same store
        if (oldStore !== store) {
            // normalize store value (turn it into a storeClass instance if needed)
            store = Store.getStore(store, store?.storeClass || storeClass);

            if (oldStore) {
                me.removeStore(oldStore);
            }

            me[property] = store && { store } || null;

            // Adds configured scheduler stores to the store collection ensuring correct order
            // unless they're already registered.
            me.addPrioritizedStore(me[property]);
        }

        return me[property];
    }

    getChangeSetPackage() {
        const pack = super.getChangeSetPackage();

        // Remove assignments from changes if using single assignment mode (resourceId)
        if (pack && this.eventStore.usesSingleAssignment) {
            delete pack[this.assignmentStore.storeId];
            // No other changes?
            if (!this.crudStores.some(storeInfo => pack[storeInfo.storeId])) {
                return null;
            }
        }

        return pack;
    }

    //endregion
};
