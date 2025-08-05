import AbstractCrudManagerMixin from '../../../Scheduler/crud/AbstractCrudManagerMixin.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import Base from '../../../Core/Base.js';
import AjaxTransport from '../../crud/transport/AjaxTransport.js';
import JsonEncoder from '../../crud/encoder/JsonEncoder.js';

/**
 * @module Scheduler/data/mixin/ProjectCrudManager
 */

/**
 * This mixin provides Crud Manager functionality supporting loading of scheduling engine projects.
 *
 * @mixin
 * @mixes Scheduler/crud/AbstractCrudManagerMixin
 * @mixes Scheduler/crud/transport/AjaxTransport
 * @mixes Scheduler/crud/encoder/JsonEncoder
 */
export default Target => class ProjectCrudManager extends (Target || Base).mixin(AbstractCrudManagerMixin, AjaxTransport, JsonEncoder) {

    //region Config

    static get defaultConfig() {
        return {
            /**
             * A project that holds and links stores
             * @config {Scheduler.model.ProjectModel}
             */
            project : null
        };
    }

    startConfigure(config) {
        // process the project first which ingests any configured data sources,
        this.getConfig('project');

        super.startConfigure(config);
    }

    async doAutoLoad() {
        const { project } = this;

        // Delay autoLoad to after projects initial commit if configured with a project
        if (project) {
            await project.commitAsync();
        }

        return super.doAutoLoad();
    }

    loadCrudManagerData(response, options = {}) {
        const
            me = this,
            { project } = me;

        // we don't want reacting on store changes during loading of them
        me.suspendChangesTracking();

        super.loadCrudManagerData(...arguments);

        // if there is the project data provided
        if (response?.project) {
            me.applyingProjectResponse = true;
            Object.assign(project, response.project);
            me.applyingProjectResponse = false;
        }

        me.resumeChangesTracking();
    }

    async sync() {
        const { project } = this;

        // Suspend Crud Manager autoSync to not react on changes during commitAsync() call
        this.suspendAutoSync();

        // Make sure data is in a calculated state before syncing
        if (project) {
            await project.commitAsync();
        }

        this.resumeAutoSync(false);

        return super.sync();
    }

    async applyResponse(requestType, response, options) {
        const me = this;

        if (me.isDestroyed) {
            return;
        }

        me.trigger('startApplyResponse');

        // clear "added"/"modified" collections on the stores
        // TODO: need to snapshot their state to be able to revert in case of an exception
        me.clearCrudStoresChanges({ clearRemovedCollection : false });

        await super.applyResponse(requestType, response, options);

        // clear "removed" collection on the stores
        me.clearCrudStoresChanges({
            removeAddedRecords      : false,
            clearAddedCollection    : false,
            clearModifiedCollection : false,
            clearRemovedCollection  : true
        });

        me.applyingProjectResponse = true;
        // if there is the project data provided
        response && response.project && Object.assign(me, response.project);
        me.applyingProjectResponse = false;

        // if we have a project
        if (me.project) {
            // response can force its type
            if (me.trackResponseType) {
                requestType = response.type || requestType;
            }

            // Make a boolean flag indicating what has triggered the propagation ("propagatingLoadChanges" or  "propagatingSyncChanges")
            const propagationFlag = `propagating${StringHelper.capitalize(requestType)}Changes`;

            me[propagationFlag] = true;
            // Wait till calculation gets done
            await me.project.commitAsync();
            me[propagationFlag] = false;
        }
    }

    clearCrudStoresChanges(flags = { removeAddedRecords : true, clearAddedCollection : true, clearModifiedCollection : true, clearRemovedCollection : true }) {
        const {
            removeAddedRecords,
            clearAddedCollection,
            clearModifiedCollection,
            clearRemovedCollection
        } = flags;

        // TODO: Change when https://app.assembla.com/spaces/bryntum/tickets/8975 is fixed
        // this.crudStores.forEach(store => store.store.clearChanges());
        this.forEachCrudStore(store => {
            if (removeAddedRecords) {
                // remove phantom records
                store.remove(this.added, true);
            }

            if (clearModifiedCollection) {
                store.modified.forEach(r => r.clearChanges(false));
                store.modified.clear();
            }

            if (clearAddedCollection) {
                store.added.clear();
            }

            if (clearRemovedCollection) {
                store.removed.clear();
            }
        });
    }
};
