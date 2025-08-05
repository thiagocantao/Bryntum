import SchedulerProjectCrudManager from '../../../Scheduler/data/mixin/ProjectCrudManager.js';
import Base from '../../../Core/Base.js';

/**
 * @module SchedulerPro/data/mixin/ProjectCrudManager
 */

// the order of the @mixes tags is important below, as the "AbstractCrudManagerMixin"
// contains the abstract methods, which are then overwritten by the concrete
// implementation in the AjaxTransport and JsonEncoder

/**
 * This mixin provides Crud manager functionality to a Scheduler Pro project.
 * The mixin turns the provided project model into a Crud manager instance.
 *
 * @mixin
 * @mixes Scheduler/data/mixin/ProjectCrudManager
 *
 * @typings Scheduler.data.mixin.ProjectCrudManager -> Scheduler.data.mixin.SchedulerProjectCrudManager
 */
export default Target => class ProjectCrudManager extends (Target || Base).mixin(SchedulerProjectCrudManager) {
    static get configurable() {
        return {
            crudLoadValidationWarningPrefix : 'Project load response error(s):',

            crudSyncValidationWarningPrefix : 'Project sync response error(s):',

            /**
             * If `true`, project {@link #property-changes} API will also report project model changes: start/end date,
             * calendar, effort, duration, etc.
             * @prp {Boolean}
             * @default
             */
            trackProjectModelChanges : false
        };
    }

    construct(...args) {
        const me = this;

        super.construct(...args);

        // add the Engine specific stores to the crud manager
        me.addPrioritizedStore(me.calendarManagerStore);
        me.addPrioritizedStore(me.assignmentStore);
        me.addPrioritizedStore(me.dependencyStore);
        me.addPrioritizedStore(me.resourceStore);
        me.addPrioritizedStore(me.eventStore);
        if (me.timeRangeStore) {
            me.addPrioritizedStore(me.timeRangeStore);
        }
        if (me.resourceTimeRangeStore) {
            me.addPrioritizedStore(me.resourceTimeRangeStore);
        }
    }

    get project() {
        return this;
    }

    set project(value) {
        super.project = value;
    }

    get crudLoadValidationMandatoryStores() {
        return [this.getStoreDescriptor(this.eventStore).storeId];
    }

    loadCrudManagerData(...args) {
        if (this.delayCalculation && !this.isDelayingCalculation && !this.usingSyncDataOnLoad()) {
            this.scheduleDelayedCalculation();
        }

        super.loadCrudManagerData(...args);
    }

    acceptChanges() {
        super.acceptChanges();

        // clear project model own field changes
        this.clearChanges(true, false);
    }

    revertChanges() {
        // revertChanges method exists both on the Model and AbstractCrudManagerMixin class
        // so here we have to couple both of them

        // first invoke Crud Manager logic
        this.revertCrudStoreChanges();

        // then invoke Model logic - revert project model own field changes
        this.set(this.meta.modified, undefined, true);
    }

    // Override to take into account project model own field changes
    crudStoreHasChanges(storeId) {
        const store = this.getCrudStore(storeId);

        let result;

        if (store) {
            result = super.crudStoreHasChanges(store);
        }
        else {
            result = this.hasPersistableChanges || super.crudStoreHasChanges();
        }

        return result;
    }

    /**
     * Returns current changes as an object consisting of added/modified/removed arrays of records for every
     * managed store, keyed by each store's `id`. Returns `null` if no changes exist. Format:
     *
     * ```javascript
     * {
     *     resources : {
     *         added    : [{ name : 'New guy' }],
     *         modified : [{ id : 2, name : 'Mike' }],
     *         removed  : [{ id : 3 }]
     *     },
     *     events : {
     *         modified : [{  id : 12, name : 'Cool task' }]
     *     },
     *     ...
     * }
     * ```
     *
     * To also include changes of the project model itself set {@link #property-trackProjectModelChanges} to `true`:
     *
     * ```javascript
     * {
     *     project : {
     *         calendar  : 'custom',
     *         startDate : '2020-02-02',
     *         endDate   : '2020-02-10
     *     },
     *     resources : {...},
     *     events    : {...}
     * }
     * ```
     *
     * @property {Object}
     * @readonly
     * @category CRUD
     */
    get changes() {
        let changes = super.changes;

        if (this.trackProjectModelChanges) {
            const projectChanges = this.modificationDataToWrite;

            // include project changes
            if (projectChanges) {
                changes = changes || {};
                changes.project = projectChanges;
            }
        }

        return changes;
    }

    shouldClearRecordFieldChange(record, field, value) {
        // If that's a calendar model "intervals" field
        // we just check if the underlying store is actually dirty.
        if (record.isCalendarModel && field === 'intervals') {
            return !record.get('intervals').changes;
        }

        return super.shouldClearRecordFieldChange(...arguments);
    }
};
