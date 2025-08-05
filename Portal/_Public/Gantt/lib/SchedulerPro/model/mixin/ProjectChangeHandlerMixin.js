import Base from '../../../Core/Base.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module SchedulerPro/model/mixin/ProjectChangeHandlerMixin
 */

// Check if assigning a raw value will make the field change
const willChange = (fieldName, rawData, record) => {
    const
        field          = record.getFieldDefinition(fieldName),
        { dataSource } = field,
        newValue       = record.constructor.processField(fieldName, rawData[dataSource], record);

    return dataSource in rawData && !field.isEqual(newValue, record.getValue(fieldName));
};

/**
 * This mixin allows syncing a changes object between projects. See {@link #function-applyProjectChanges} for usage
 * @mixin
 */
export default Target => class ProjectChangeHandlerMixin extends (Target || Base) {
    startConfigure(config) {
        // process the project first which ingests any configured data sources,
        this.getConfig('project');

        super.startConfigure(config);
    }

    beforeApplyProjectChanges() {
        const { stm } = this;

        let shouldResume  = false,
            transactionId = null;

        this.suspendChangesTracking();

        if (stm.enabled) {
            shouldResume = true;

            if (stm.isRecording) {
                transactionId = stm.stash();
            }

            if (this.ignoreRemoteChangesInSTM) {
                stm.disable();
            }
            else {
                stm.startTransaction();
            }
        }

        return { shouldResume, transactionId };
    }

    /**
     * Allows to apply changes from one project to another. For method to produce correct results, projects should be
     * isomorphic - they should use same models and store configuration, also data in source and target projects
     * should be identical before changes to the source project are made and applied to the target project.
     * This method is meant to apply changes in real time - as source project is changed, changes should be applied to
     * the target project before it is changed.
     * When changes are applied all changes are committed and project is recalculated, which means target project
     * won't have any local changes after.
     *
     * Usage:
     * ```javascript
     * // Collect changes from first project
     * const { changes } = projectA;
     *
     * // Apply changes to second project
     * await projectB.applyProjectChanges(changes);
     * ```
     *
     * <div class="note">
     * This method will apply changes from the incoming object and accept all current project changes. Before
     * applying changes make sure you've processed current project changes in order not to lose them.
     * </div>
     *
     * @param {Object} changes Project {@link Scheduler/crud/AbstractCrudManagerMixin#property-changes} object
     * @returns {Promise}
     */
    async applyProjectChanges(changes) {
        const
            me = this,
            {
                shouldResume,
                transactionId
            }  = me.beforeApplyProjectChanges();

        me.trigger('startApplyChangeset');

        // Raise a flag to let store know not to stash stm changes
        me.applyingChangeset = true;

        if (changes.project) {
            me.applyProjectResponse(changes.project);
        }

        // Apply changes from other project, except for dates that will lead to changed constraints (engine is not aware
        // that we want to keep the constraint)
        // Has to clone to be able to catch the change and clean it up after commit
        me.applyChangeset(ObjectHelper.clone(changes), (storeChanges, store) => {
            if ((store.id === 'tasks' || store.id === 'events')) {
                const
                    { modelClass } = store,
                    startDate      = modelClass.getFieldDataSource('startDate'),
                    endDate        = modelClass.getFieldDataSource('endDate');

                if (storeChanges.updated) {
                    for (const data of storeChanges.updated) {
                        const record = store.getById(data[modelClass.idField]);
                        if (!(
                            willChange('constraintType', data, record) ||
                            willChange('constraintDate', data, record)
                        )) {
                            delete data[startDate];
                            delete data[endDate];
                        }
                    }
                }
            }
        });

        await me.commitAsync();

        // This will clean up changes in the project model if they match incoming values
        me.commitRespondedChanges();

        // The call to applyChangeset() clears changes (it might, but not always), but commitAsync() leads to new
        // changes. If those changes match what we requested, we flag them as not modified
        for (const storeId in changes) {
            const storeDescriptor = me.getStoreDescriptor(storeId);

            // if that a Store section
            if (storeDescriptor) {
                const
                    // Due to this issue better to use lookup on the project instance rather than in global registry
                    // https://github.com/bryntum/support/issues/5238
                    { store }    = storeDescriptor,
                    storeChanges = changes[storeId],
                    changedRows  = [...storeChanges.updated ?? [], ...storeChanges.added ?? []];

                // Store might be destroyed, asyncness...
                if (store) {
                    // Iterate updated and added rows
                    for (const data of changedRows) {
                        const record = store.getById(data[store.modelClass.idField]);

                        // Record might not have been added e.g. if change was conflicting and got resolved by rejecting
                        if (record) {
                            // Compare each change on the matching record with the raw data value, unflag change if they match
                            for (const fieldName in record.modifications) {
                                if (!willChange(fieldName, data, record)) {
                                    delete record.meta.modified[fieldName];
                                }
                            }
                        }
                    }
                }
            }
        }

        me.afterApplyProjectChanges(shouldResume, transactionId);

        me.applyingChangeset = false;

        // Trigger commit async in case non-project related field (e.g. name) was changed to update possibly
        // opened task editor
        await me.commitAsync();

        me.trigger('endApplyChangeset');
    }

    afterApplyProjectChanges(shouldResume, transactionId) {
        if (shouldResume) {
            const { stm } = this;

            if (this.ignoreRemoteChangesInSTM) {
                stm.enable();
            }
            else {
                stm.stopTransaction();
            }

            stm.applyStash(transactionId);
        }

        this.resumeChangesTracking();
    }
};
