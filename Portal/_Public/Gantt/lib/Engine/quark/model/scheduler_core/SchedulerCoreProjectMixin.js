import { Mixin } from "../../../../ChronoGraph/class/BetterMixin.js";
import { SchedulerCoreEvent } from "./SchedulerCoreEvent.js";
import Store from "../../../../Core/data/Store.js";
import Model from "../../../../Core/data/Model.js";
import Delayable from "../../../../Core/mixin/Delayable.js";
import { CoreEventStoreMixin } from "../../store/CoreEventStoreMixin.js";
import { CoreAssignmentMixin } from "./CoreAssignmentMixin.js";
import { CoreAssignmentStoreMixin } from "../../store/CoreAssignmentStoreMixin.js";
import { CoreResourceMixin } from "./CoreResourceMixin.js";
import { CoreResourceStoreMixin } from "../../store/CoreResourceStoreMixin.js";
import { CorePartOfProjectGenericMixin } from "../../CorePartOfProjectGenericMixin.js";
import { CoreDependencyStoreMixin } from "../../store/CoreDependencyStoreMixin.js";
import { CoreDependencyMixin } from "./CoreDependencyMixin.js";
import { CoreCalendarMixin } from './CoreCalendarMixin.js';
import { CoreCalendarManagerStoreMixin } from '../../store/CoreCalendarManagerStoreMixin.js';
import { delay } from "../../../util/Functions.js";
import StateTrackingManager from "../../../../Core/data/stm/StateTrackingManager.js";
import { AbstractProjectMixin } from "../AbstractProjectMixin.js";
import ObjectHelper from "../../../../Core/helper/ObjectHelper.js";
export class DelayableWrapper extends Mixin([], Delayable) {
}
/**
 * This is a project, implementing _basic scheduling_ as [[SchedulerBasicProjectMixin]] does.
 * Yet this class does not use _chronograph_ based engine.
 */
export class SchedulerCoreProjectMixin extends Mixin([
    AbstractProjectMixin,
    CorePartOfProjectGenericMixin,
    DelayableWrapper,
    Model
], (base) => {
    const superProto = base.prototype;
    class SchedulerCoreProjectMixin extends base {
        static get configurable() {
            return {
                stm: {},
                eventStore: {},
                assignmentStore: {},
                resourceStore: {},
                dependencyStore: {},
                calendarManagerStore: {},
                eventModelClass: SchedulerCoreEvent,
                assignmentModelClass: CoreAssignmentMixin,
                resourceModelClass: CoreResourceMixin,
                dependencyModelClass: CoreDependencyMixin,
                calendarModelClass: CoreCalendarMixin,
                eventStoreClass: CoreEventStoreMixin,
                assignmentStoreClass: CoreAssignmentStoreMixin,
                resourceStoreClass: CoreResourceStoreMixin,
                dependencyStoreClass: CoreDependencyStoreMixin,
                calendarManagerStoreClass: CoreCalendarManagerStoreMixin,
                assignmentsData: null,
                calendarsData: null,
                dependenciesData: null,
                eventsData: null,
                resourcesData: null
            };
        }
        //endregion
        //region Init
        construct(config = {}) {
            const me = this;
            // Cannot be created with declaration, because of how TS is compiled to JS. Ends up after `construct()`
            me.$invalidated = new Set();
            // Define default values for these flags here
            // if defined where declared then TS compiles them this way:
            // constructor() {
            //     super(...arguments)
            //     this.isPerformingCommit   = false
            //     this.silenceInitialCommit = true
            //     this.ongoing              = Promise.resolve()
            // }
            // which messes the flags values for inline data loading (since it's async)
            me.isPerformingCommit = false;
            me.silenceInitialCommit = true;
            me.ongoing = Promise.resolve();
            if (config.eventStore && !config.assignmentStore) {
                const eventStore = config.eventStore.masterStore || config.eventStore;
                // If chained from a CrudManager, the assignment store might not be part of a project, and we might
                // need to ingest it from the CrudManager
                // @ts-ignore
                const assignmentStore = eventStore.assignmentStore || eventStore.crudManager?.assignmentStore;
                // In this case we must ingest the assignment store from the eventStore
                if (assignmentStore?.isAssignmentStore) {
                    config.assignmentStore = assignmentStore;
                    me.isSharingAssignmentStore = true;
                }
            }
            superProto.construct.call(me, config);
            // not part of the CalendarManagerStore intentionally, not persisted
            me.defaultCalendar = new me.calendarManagerStore.modelClass({
                unspecifiedTimeIsWorking: me.unspecifiedTimeIsWorking
            });
            me.defaultCalendar.project = me;
            const { calendarsData, eventsData, dependenciesData, resourcesData, assignmentsData } = me;
            const hasInlineData = Boolean(calendarsData || eventsData || dependenciesData || resourcesData || assignmentsData);
            if (hasInlineData) {
                me.loadInlineData({
                    calendarsData,
                    eventsData,
                    dependenciesData,
                    resourcesData,
                    assignmentsData
                });
                delete me.calendarsData;
                delete me.eventsData;
                delete me.dependenciesData;
                delete me.resourcesData;
                delete me.assignmentsData;
            }
            else {
                // Trigger initial commit
                me.bufferedCommitAsync();
            }
        }
        doDestroy() {
            const me = this;
            me.eventStore?.destroy();
            me.dependencyStore?.destroy();
            me.assignmentStore?.destroy();
            me.resourceStore?.destroy();
            me.calendarManagerStore?.destroy();
            me.defaultCalendar.destroy();
            me.stm?.destroy();
            superProto.doDestroy.call(this);
        }
        /**
         * This method loads the "raw" data into the project. The loading is basically happening by
         * assigning the individual data entries to the `data` property of the corresponding store.
         *
         * @param data
         */
        async loadInlineData(data) {
            const me = this;
            me.isLoadingInlineData = true;
            if (data.calendarsData) {
                me.calendarManagerStore.data = data.calendarsData;
            }
            if (data.resourcesData) {
                me.resourceStore.data = data.resourcesData;
            }
            if (data.assignmentsData) {
                me.assignmentStore.data = data.assignmentsData;
            }
            if (data.eventsData) {
                me.eventStore.data = data.eventsData;
            }
            if (data.tasksData) {
                me.eventStore.data = data.tasksData;
            }
            if (data.dependenciesData) {
                me.dependencyStore.data = data.dependenciesData;
            }
            await me.commitLoad();
            me.isLoadingInlineData = false;
            return;
        }
        //endregion
        //region Join
        async commitLoad() {
            await this.commitAsync();
            // Might have been destroyed during the async operation above
            if (!this.isDestroyed)
                this.trigger('load');
        }
        joinStoreRecords(store) {
            const fn = (record) => {
                record.setProject(this);
                record.joinProject();
            };
            if (store.rootNode) {
                store.rootNode.traverse(fn);
            }
            else {
                store.forEach(fn);
            }
        }
        unJoinStoreRecords(store) {
            const fn = (record) => {
                record.leaveProject();
                record.setProject(this);
            };
            if (store.rootNode) {
                store.rootNode.traverse(node => {
                    // do not unjoin/leave project for the root node, which is the project itself
                    if (node !== store.rootNode)
                        fn(node);
                });
            }
            else {
                store.forEach(fn);
            }
        }
        //endregion
        //region EventStore
        resolveStoreAndModelClass(name, config) {
            // storeClass from supplied config or our properties
            const storeClass = config?.storeClass || this[`${name}StoreClass`];
            // modelClass from supplied config
            let modelClass = config?.modelClass;
            if (!modelClass) {
                // or from storeClass.modelClass if customized
                // @ts-ignore
                if (this.getDefaultConfiguration()[`${name}ModelClass`] !== storeClass.getDefaultConfiguration().modelClass) {
                    modelClass = storeClass.getDefaultConfiguration().modelClass;
                }
                // and if none of the above, use from our properties
                else {
                    modelClass = this[`${name}ModelClass`];
                }
            }
            return { storeClass, modelClass };
        }
        setEventStore(eventStore) {
            this.eventStore = eventStore;
        }
        changeEventStore(eventStore, oldStore) {
            const me = this, { stm } = me;
            me.detachStore(oldStore);
            if (!(eventStore instanceof Store)) {
                const { storeClass, modelClass } = me.resolveStoreAndModelClass('event', eventStore);
                eventStore = new storeClass(ObjectHelper.assign({
                    modelClass,
                    project: me,
                    stm
                }, eventStore));
            }
            else {
                eventStore.project = me;
                stm.addStore(eventStore);
                me.joinStoreRecords(eventStore);
            }
            if (oldStore && stm.hasStore(oldStore)) {
                stm.removeStore(oldStore);
                me.unJoinStoreRecords(oldStore);
                const { assignmentsForRemoval } = oldStore;
                // remap the assignment
                assignmentsForRemoval.forEach(assignment => {
                    const oldEvent = assignment.event;
                    if (oldEvent) {
                        const newEvent = eventStore.getById(oldEvent.id);
                        if (newEvent) {
                            assignment.event = newEvent;
                            // keep the assignment
                            assignmentsForRemoval.delete(assignment);
                        }
                    }
                });
                oldStore.afterEventRemoval();
            }
            eventStore.setProject(me);
            return eventStore;
        }
        updateEventStore(eventStore, oldStore) {
            this.attachStore(eventStore);
            this.trigger('eventStoreChange', { store: eventStore });
        }
        //endregion
        //region AssignmentStore
        setAssignmentStore(assignmentStore) {
            this.assignmentStore = assignmentStore;
        }
        changeAssignmentStore(assignmentStore, oldStore) {
            const me = this, { stm } = me;
            me.detachStore(oldStore);
            if (oldStore && stm.hasStore(oldStore)) {
                stm.removeStore(oldStore);
                me.unJoinStoreRecords(oldStore);
            }
            if (!(assignmentStore instanceof Store)) {
                const { storeClass, modelClass } = me.resolveStoreAndModelClass('assignment', assignmentStore);
                assignmentStore = new storeClass(ObjectHelper.assign({
                    modelClass,
                    project: me,
                    stm
                }, assignmentStore));
            }
            else {
                assignmentStore.project = me;
                stm.addStore(assignmentStore);
                me.joinStoreRecords(assignmentStore);
            }
            assignmentStore.setProject(me);
            return assignmentStore;
        }
        updateAssignmentStore(assignmentStore, oldStore) {
            this.attachStore(assignmentStore);
            this.trigger('assignmentStoreChange', { store: assignmentStore });
        }
        //endregion
        //region ResourceStore
        setResourceStore(resourceStore) {
            this.resourceStore = resourceStore;
        }
        changeResourceStore(resourceStore, oldStore) {
            const me = this, { stm } = me;
            me.detachStore(oldStore);
            if (!(resourceStore instanceof Store)) {
                const { storeClass, modelClass } = me.resolveStoreAndModelClass('resource', resourceStore);
                resourceStore = new storeClass(ObjectHelper.assign({
                    modelClass,
                    project: me,
                    stm
                }, resourceStore));
            }
            else {
                resourceStore.project = me;
                stm.addStore(resourceStore);
                me.joinStoreRecords(resourceStore);
            }
            if (oldStore && stm.hasStore(oldStore)) {
                stm.removeStore(oldStore);
                me.unJoinStoreRecords(oldStore);
                const { assignmentsForRemoval } = oldStore;
                // remap the assignment
                assignmentsForRemoval.forEach(assignment => {
                    const oldResource = assignment.resource;
                    if (oldResource) {
                        const newResource = resourceStore.getById(oldResource.id);
                        if (newResource) {
                            assignment.resource = newResource;
                            // keep the assignment
                            assignmentsForRemoval.delete(assignment);
                        }
                    }
                });
                oldStore.afterResourceRemoval();
            }
            resourceStore.setProject(me);
            return resourceStore;
        }
        updateResourceStore(resourceStore, oldStore) {
            this.attachStore(resourceStore);
            this.trigger('resourceStoreChange', { store: resourceStore });
        }
        //endregion
        //region DependencyStore
        setDependencyStore(dependencyStore) {
            this.dependencyStore = dependencyStore;
        }
        changeDependencyStore(dependencyStore, oldStore) {
            const me = this;
            me.detachStore(oldStore);
            if (!(dependencyStore instanceof Store)) {
                const { storeClass, modelClass } = me.resolveStoreAndModelClass('dependency', dependencyStore);
                dependencyStore = new storeClass(ObjectHelper.assign({
                    modelClass,
                    project: me,
                    stm: me.stm
                }, dependencyStore));
            }
            else {
                dependencyStore.project = me;
                me.stm.addStore(dependencyStore);
                me.joinStoreRecords(dependencyStore);
            }
            return dependencyStore;
        }
        updateDependencyStore(dependencyStore, oldStore) {
            this.attachStore(dependencyStore);
            this.trigger('dependencyStoreChange', { store: dependencyStore });
        }
        //endregion
        //region CalendarManagerStore
        setCalendarManagerStore(calendarManagerStore) {
            this.calendarManagerStore = calendarManagerStore;
        }
        changeCalendarManagerStore(calendarManagerStore, oldStore) {
            const me = this;
            me.detachStore(oldStore);
            if (!(calendarManagerStore instanceof Store)) {
                // @ts-ignore
                const storeClass = calendarManagerStore?.storeClass || me.calendarManagerStoreClass;
                // @ts-ignore
                const modelClass = calendarManagerStore?.modelClass || storeClass.getDefaultConfiguration().modelClass || me.calendarModelClass;
                calendarManagerStore = new storeClass(ObjectHelper.assign({
                    modelClass,
                    project: me,
                    stm: me.stm
                }, calendarManagerStore));
            }
            else {
                me.stm.addStore(calendarManagerStore);
            }
            calendarManagerStore.setProject(me);
            return calendarManagerStore;
        }
        updateCalendarManagerStore(calendarManagerStore, oldStore) {
            this.attachStore(calendarManagerStore);
            this.trigger('calendarManagerStoreChange', { store: calendarManagerStore });
        }
        //endregion
        //region Calendar
        get calendar() {
            return this.$calendar || this.defaultCalendar;
        }
        set calendar(calendar) {
            this.$calendar = calendar;
        }
        get effectiveCalendar() {
            return this.calendar;
        }
        //endregion
        //region Add records
        async addEvent(event) {
            this.eventStore.add(event);
            return this.commitAsync();
        }
        async addAssignment(assignment) {
            this.assignmentStore.add(assignment);
            return this.commitAsync();
        }
        async addResource(resource) {
            this.resourceStore.add(resource);
            return this.commitAsync();
        }
        async addDependency(dependency) {
            this.dependencyStore.add(dependency);
            return this.commitAsync();
        }
        //endregion
        //region Auto commit
        // Buffer commitAsync using setTimeout. Not using `buffer` on purpose, for performance reasons and to better
        // mimic how graph does it
        bufferedCommitAsync() {
            if (!this.hasPendingAutoCommit) {
                this.setTimeout({
                    fn: 'commitAsync',
                    delay: 10
                });
            }
        }
        get hasPendingAutoCommit() {
            return this.hasTimeout('commitAsync');
        }
        unScheduleAutoCommit() {
            this.clearTimeout('commitAsync');
        }
        //endregion
        //region Commit
        async commitAsync() {
            if (this.isPerformingCommit)
                return this.ongoing;
            return this.ongoing = this.doCommitAsync();
        }
        async doCommitAsync() {
            const me = this;
            me.isPerformingCommit = true;
            // Cancel any outstanding commit
            me.unScheduleAutoCommit();
            await delay(0);
            if (!me.isDestroyed) {
                // Calculate all invalidated records, updates their data silently
                for (const record of me.$invalidated) {
                    record.calculateInvalidated();
                }
                const { isInitialCommit, silenceInitialCommit } = me;
                // apply changes silently if this is initial commit and "silenceInitialCommit" option is enabled
                const silenceCommit = isInitialCommit && silenceInitialCommit;
                // Notify stores that care about commit (internal)
                me.assignmentStore.onCommitAsync();
                me.dependencyStore.onCommitAsync();
                me.isInitialCommitPerformed = true;
                me.hasLoadedDataToCommit = false;
                me.isPerformingCommit = false;
                const stores = [me.assignmentStore, me.dependencyStore, me.eventStore, me.resourceStore, me.calendarManagerStore];
                stores.forEach(store => store.suspendAutoCommit?.());
                me.isWritingData = true;
                // "Real" project triggers refresh before data is written back to records
                me.trigger('refresh', { isInitialCommit, isCalculated: true });
                // If we are not announcing changes, take a cheaper path
                if (silenceCommit) {
                    for (const record of me.$invalidated) {
                        record.finalizeInvalidated(true);
                    }
                }
                else {
                    // Two loops looks a bit weird, but needed since editing assignment might affect event etc.
                    // And we do only want a single update in the end
                    // 1. Start batches and perform all calculations
                    for (const record of me.$invalidated) {
                        record.beginBatch(true);
                        record.finalizeInvalidated();
                    }
                    // 2. End batches, announcing changes (unless initial commit)
                    for (const record of me.$invalidated) {
                        record.endBatch(false, true);
                    }
                }
                me.isWritingData = false;
                me.$invalidated.clear();
                // Mimic real projects events
                me.trigger('dataReady');
                stores.forEach(store => store.resumeAutoCommit?.());
                // Chrono version triggers "dataReady" only if there were no commit rejection
                // (in case of a rejection it triggers "commitRejected" event)
                // but in both cases it triggers "commitFinalized" afterwards
                me.trigger('commitFinalized');
                return true;
            }
        }
        async propagateAsync() {
            return this.commitAsync();
        }
        // Called when a record invalidates itself, queues it for calculation
        invalidate(record) {
            this.$invalidated.add(record);
            this.bufferedCommitAsync();
        }
        // this does not account for possible scheduling conflicts
        async isValidDependency() {
            return true;
        }
        //endregion
        //region STM
        getStm() {
            return this.stm;
        }
        /**
         * State tracking manager instance the project relies on
         */
        set stm(stm) {
            stm = this.$stm = new StateTrackingManager(ObjectHelper.assign({
                disabled: true
            }, stm));
            stm.ion({
                // Propagate on undo/redo
                restoringStop: async () => {
                    // Disable STM meanwhile to not pick it up as a new STM transaction
                    stm.disable();
                    await this.commitAsync();
                    if (!this.isDestroyed) {
                        stm.enable();
                        this.trigger('stateRestoringDone');
                    }
                }
            });
        }
        get stm() {
            return this.$stm;
        }
        //endregion
        isEngineReady() {
            return !this.hasPendingAutoCommit && !this.isPerformingCommit && this.isInitialCommitPerformed;
        }
    }
    //region Config
    SchedulerCoreProjectMixin.applyConfigs = true;
    return SchedulerCoreProjectMixin;
}) {
}
