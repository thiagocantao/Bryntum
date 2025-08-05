import AbstractCrudManagerMixin from '../../../Scheduler/crud/AbstractCrudManagerMixin.js';
import JsonEncoder from '../../../Scheduler/crud/encoder/JsonEncoder.js';
import AjaxTransport from '../../../Scheduler/crud/transport/AjaxTransport.js';
import Events from '../../../Common/mixin/Events.js';
import { EffectResolutionResult } from '../../../ChronoGraph/chrono/Effect.js';

/**
 * @module Gantt/data/mixin/GanttCrudManager
 */

// the order of the @mixes tags is important below, as the "AbstractCrudManagerMixin"
// contains the abstract methods, which are then overwritten by the concrete
// implementation in the AjaxTransport and JsonEncoder

/**
 * This is a mixin, provding Crud manager functionality, specialized for the Gantt project.
 *
 * @mixin
 * @mixes Common/mixin/Events
 * @mixes Scheduler/crud/AbstractCrudManagerMixin
 * @mixes Scheduler/crud/transport/AjaxTransport
 * @mixes Scheduler/crud/encoder/JsonEncoder
 */
export default Base => class GanttCrudManager extends JsonEncoder(AjaxTransport(AbstractCrudManagerMixin(Events(Base)))) {

    construct(...args) {
        const me = this;

        super.construct(...args);

        // add the gantt specific stores to the crud manager
        me.addPrioritizedStore(me.calendarManagerStore);
        me.addPrioritizedStore(me.assignmentStore);
        me.addPrioritizedStore(me.dependencyStore);
        me.addPrioritizedStore(me.resourceStore);
        me.addPrioritizedStore(me.eventStore);
        me.addPrioritizedStore(me.timeRangeStore);
    }

    async applyResponse(requestType, response, options) {

        await super.applyResponse(requestType, response, options);

        // if there is the project data provided
        response && response.project && Object.assign(this, response.project);

        // the initial propagation should always use "Resume" for conflicts
        await this.propagate(() => EffectResolutionResult.Resume);

        // TODO:
        this.clearCrudStoresChanges();
    }

    clearCrudStoresChanges() {
        // this.crudStores.forEach(store => store.store.clearChanges());
        this.crudStores.forEach(store => {
            const me = store.store;

            me.remove(me.added.values, true);
            me.modified.forEach(r => r.clearChanges(false));

            me.added.clear();
            me.modified.clear();
            me.removed.clear();
        });
    }

    applyProjectResponse(projectResponse) {
        this.loadProjectFields(projectResponse);
    }
};
