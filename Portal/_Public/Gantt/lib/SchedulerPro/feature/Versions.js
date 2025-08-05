import ChangeLogTransactionModel from '../model/changelog/ChangeLogTransactionModel.js';
import VersionModel from '../model/VersionModel.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DependencyModel from '../model/DependencyModel.js';
import AssignmentModel from '../model/AssignmentModel.js';
import ResourceModel from '../model/ResourceModel.js';
import ChangeLogEntity from '../model/changelog/ChangeLogEntity.js';
import ChangeLogDependencyEntity from '../model/changelog/ChangeLogDependencyEntity.js';
import ChangeLogAssignmentEntity from '../model/changelog/ChangeLogAssignmentEntity.js';
import ChangeLogAction from '../model/changelog/ChangeLogAction.js';
import ChangeLogMoveAction from '../model/changelog/ChangeLogMoveAction.js';
import ChangeLogUpdateAction from '../model/changelog/ChangeLogUpdateAction.js';
import VersionStore from '../data/VersionStore.js';
import ChangeLogStore from '../data/ChangeLogStore.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import WalkHelper from '../../Core/helper/WalkHelper.js';
import AttachToProjectMixin from '../../Scheduler/data/mixin/AttachToProjectMixin.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';

/**
 * @module SchedulerPro/feature/Versions
 */

/**
 * Captures versions (snapshots) of the active project, including a detailed log of the changes new in each version.
 *
 * When active, the feature monitors the project for changes and appends them to the changelog. When a version is captured,
 * the version will consist of a complete snapshot of the project data at the time of the capture, in addition to the list
 * of changes in the changelog that have occurred since the last version was captured.
 *
 * For information about the data structure representing a version and how to persist it, see {@link SchedulerPro.model.VersionModel}.
 *
 * For information about the data structures representing the changelog and how to persist them, see
 * {@link SchedulerPro.model.changelog.ChangeLogTransactionModel}.
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         versions : true
 *     }
 * });
 * ```
 *
 * To display versions and their changes, use a {@link SchedulerPro.widget.VersionGrid} configured with a {@link SchedulerPro.model.ProjectModel}.
 *
 * {@inlineexample SchedulerPro/guides/whats-new/5.3.0/versions.js}
 *
 * See also:
 * - {@link SchedulerPro.model.VersionModel} A stored version of a ProjectModel, captured at a point in time, with change log
 * - {@link SchedulerPro.model.changelog.ChangeLogTransactionModel} The set of add/remove/update actions that occurred in response to a user action
 * - {@link SchedulerPro.widget.VersionGrid} Widget for displaying a project's versions and changes
 *
 * @extends Core/mixin/InstancePlugin
 * @classType versions
 * @feature
 */
export default class Versions extends InstancePlugin.mixin(AttachToProjectMixin) {



    static $name = 'Versions';

    static configurable = {

        /**
         * Optional subclass of {@link SchedulerPro.model.VersionModel} to use instead of {@link SchedulerPro.model.VersionModel}.
         * Use this to extend VersionModel to add any additional fields your application needs.
         * @config {SchedulerPro.model.VersionModel}
         * @typings {typeof VersionModel}
         */
        versionModelClass : VersionModel,

        /**
         * Optional subclass of {@link SchedulerPro.model.changelog.ChangeLogTransactionModel} to use instead of {@link SchedulerPro.model.changelog.ChangeLogTransactionModel}.
         * Use this to extend ChangeLogTransactionModel to add any additional fields your application needs.
         * @config {SchedulerPro.model.changelog.ChangeLogTransactionModel}
         * @typings {typeof ChangeLogTransactionModel}
         */
        transactionModelClass : ChangeLogTransactionModel,

        /**
         * The interval between autosaves, in minutes. To disable autosave, set the interval to zero.
         * To save on the hour, use 'hourly'.
         *
         * @config {'hourly'|Number}
         * @default
         */
        autoSaveInterval : 'hourly',

        /**
         * The set of Model types whose subtypes should be recorded as the base type in the change log. For example,
         * by default if a subclassed TaskModelEx exists and an instance of one is updated, it will be recorded in the
         * changelog as a TaskModel.
         * @config {Array}
         * @default [AssignmentModel, DependencyModel, ResourceModel]
         */
        knownBaseTypes : [AssignmentModel, DependencyModel, ResourceModel],

        versionContentLoadTimeout : 60000
    };

    _remoteActionRanges = [];
    _comparingVersionId;

    construct(scheduler, config) {
        super.construct(scheduler, config);
        const me = this;

        me.versionStore = new VersionStore({
            modelClass        : me.versionModelClass,
            internalListeners : {
                change  : me.onStoreChangeExternal,
                thisObj : me
            },
            id : VersionStore.configurable.storeId
        });

        me.changeStore = new ChangeLogStore({
            modelClass        : me.transactionModelClass,
            internalListeners : {
                change  : me.onStoreChangeExternal,
                thisObj : me
            },
            id : ChangeLogStore.configurable.storeId
        });

        me._currentChanges = [];

        // Hook UI events from other features to apply default transaction descriptions
        me.client.ion({
            beforeTaskDelete          : me.onBeforeTaskDelete,
            beforeTaskSave            : me.onBeforeTaskSave,
            gridRowBeforeDropFinalize : me.onGridRowBeforeDropFinalize,
            thisObj                   : me
        });
    }

    //region Project

    attachToProject(project) {
        const me = this;
        super.attachToProject(project);

        project.addCrudStore([
            me.versionStore,
            me.changeStore
        ]);

        project.ion({
            name    : 'project',
            load    : me.onProjectLoad,
            once    : true,
            thisObj : me
        });

        me.detachProjectListeners();
        me.attachProjectListeners(project);
    }

    //endregion

    // From TaskEdit feature - 'delete' via task edit popup
    onBeforeTaskDelete() {
        this.transactionDescription = this.L(`L{Versions.deletedTask}`);
    }

    onBeforeTaskSave() {
        this.transactionDescription = this.L(`L{Versions.editedTask}`);
    }

    onGridRowBeforeDropFinalize({ context: { valid, records } }) {
        if (valid) {
            this.transactionDescription = records.length === 1 ? this.L(`L{Versions.movedTask}`)
                : this.L(`L{Versions.movedTasks}`);
        }
    }

    attachProjectListeners(project) {
        const me = this;
        project?.stm?.ion({
            recordingStart : me.onStmRecordingStart,
            recordingStop  : me.handleTransactionStop,
            restoringStop  : me.onStmRestoringStop,
            thisObj        : me,
            name           : 'stmListeners'
        });
        project?.ion({
            wsBeforeReceiveChanges : me.onBeforeApplyRemoteChanges,
            wsReceiveChanges       : me.onAfterApplyRemoteChanges,
            thisObj                : me,
            name                   : 'stmListeners'
        });
    }

    detachProjectListeners() {
        this.detachListeners('stmListeners');
    }

    updateVersionModelClass(newVersionModelClass) {
        if (!newVersionModelClass.isVersionModel) {
            throw new Error(`versionModelClass config must be a subclass of SchedulerPro.model.VersionModel.`);
        }
    }

    updateTransactionModelClass(newTransactionModelClass) {
        if (!newTransactionModelClass.isChangeLogTransactionModel) {
            throw new Error(`transactionModelClass config must be a subclass of SchedulerPro.model.changelog.ChangeLogTransactionModel.`);
        }
    }

    onStoreChangeExternal() {
        this.triggerVersionChange();
    }

    tryAutoSave() {
        const
            me = this,
            { project } = me.client;
        if (me.changeStore.find(({ versionId }) => !versionId)) {
            if (project?.wsSend) {
                // When using websockets, ask the server for permission before saving
                project.wsSend('requestVersionAutoSave', { project : project.wsProjectId });
                me.detachListeners('autoSave');
                project.ion({
                    name                : 'autoSave',
                    wsVersionAutoSaveOK : me.autoSave,
                    thisObj             : me,
                    once                : true
                });
            }
            else {
                me.saveVersionWithConfig({ isAutosave : true });
            }
        }
    }

    autoSave() {
        this.saveVersionWithConfig({ isAutosave : true });
    }

    /**
     * When autosave is 'hourly', we check the time every 30 seconds and autosave on the hour.
     */
    autoSaveHourly() {
        if (new Date().getMinutes() === 0) {
            this.tryAutoSave();
        }
    }

    updateAutoSaveInterval(newInterval) {
        const
            me = this,
            { client } = me;
        if (me._autoSaveInterval) {
            client.clearInterval(me._autosaveInterval);
        }
        if (newInterval === 'hourly') {
            me._autoSaveInterval = client.setInterval(me.autoSaveHourly.bind(me), 10 * 1000);
        }
        else if (newInterval) {
            me._autoSaveInterval = client.setInterval(me.tryAutoSave.bind(me), newInterval * 60 * 1000);
        }
    }

    /**
     * Save a new version containing any unsaved audit log entries, with the given name (optional).
     * @param {String} [versionName] The name for the version
     */
    saveVersion(versionName = null) {
        return this.saveVersionWithConfig({ name : versionName });
    }

    /**
     * @internal
     */
    saveVersionWithConfig(versionConfig) {
        const
            me = this,
            content = me.captureVersionContent(),
            version = new me.versionModelClass({
                savedAt : new Date(),
                content,
                ...versionConfig
            });
        version.onBeforeSave();
        me.versionStore.add(version);
        me.changeStore.query(changeRecord => changeRecord.versionId == null)
            .forEach(txn => txn.versionId = version.id);
        me.triggerVersionChange();
        me.triggerTransactionChange();
        return version;
    }

    /**
     * Retrieve a single version's content from the backend.
     * @param {SchedulerPro.model.VersionModel} version Load content into the `content` field of a VersionModel
     */
    async loadVersionContent(version) {
        const
            me = this,
            { client } = me,
            { project } = client,
            context = {
                version,
                content : null
            };
        if (await me.trigger('beforeLoadVersionContent', { context })) {
            if (project?.wsSend) {
                project.wsSend('loadVersionContent', {
                    project   : project.wsProjectId,
                    versionId : version.id
                });
                const content = await new Promise((resolve, reject) => {
                    const start = Date.now();
                    project.ion({
                        name : 'loadVersionContent',
                        loadVersionContent({ project: projectId, versionId, content }) {
                            if (projectId === project.wsProjectId && versionId === version.id) {
                                project.detachListeners('loadVersionContent');
                                return resolve(content);
                            }
                        },
                        thisObj : me,
                        expires : {
                            delay : me.versionContentLoadTimeout - (Date.now() - start),
                            alt   : () => reject(`Failed to receive version content within ${this.versionContentLoadTimeout} ms`)
                        }
                    });
                });
                if (content) {
                    me._versionContentCache.set(version.id, content);
                }
                return content;
            }
        }
        else if (context.content) {
            return context.content;
        }
    }

    async getVersionContent(version) {
        return version.content ?? await this.loadVersionContent(version);
    }

    triggerTransactionChange() {
        /**
         * __Note that this event fires on the owning {@link SchedulerPro.view.SchedulerPro}.__
         *
         * Fires when the list of observed transactions changes.
         *
         * @event transactionChange
         * @param {Boolean} hasChanges Whether any changes are recorded that are not yet attached to a version.
         *
         * @on-owner
         */
        this.client.trigger('transactionChange', {
            hasUnattachedTransactions : this.hasUnattachedTransactions
        });
    }

    triggerVersionChange() {
        this.client.trigger('versionChange', { versions : this.versionStore.records });
    }

    captureVersionContent() {
        // See https://github.com/bryntum/bryntum-suite/issues/5645
        return this.client.project.toJSON();
    }

    /**
     * Restores the given version, replacing any {@link SchedulerPro.model.ProjectModel} currently
     * present in the scheduler.
     *
     * @param {SchedulerPro.model.VersionModel} version The version to compare against the current working copy
     */
    async restoreVersion(version) {
        const
            content = await this.getVersionContent(version),
            { project } = this.client,
            stmWasEnabled = project.stm?.enabled;
        project.stm?.disable();
        project.json = content;
        await project.commitAsync();
        if (stmWasEnabled) {
            project.stm?.enable();
        }
        project.stm?.resetQueue();
    }

    /**
     * Loads the given version as a set of baselines into the current project.
     *
     * @param {SchedulerPro.model.VersionModel} version The version to compare against the current working copy
     */
    async compareVersion(version) {
        const
            versionData = await this.getVersionContent(version),
            baselinesByTaskId = {},
            { project } = this.client,
            stmWasEnabled = project.stm?.enabled;

        project.stm?.disable();

        for (const rootTask of versionData.eventsData) {
            WalkHelper.preWalk(rootTask, task => task.children, ({ id, startDate, endDate }) => {
                baselinesByTaskId[id] = { startDate, endDate };
            });
        }

        project.taskStore.traverse(task => {
            if (baselinesByTaskId[task.id]) {
                task.baselines.removeAll();
                task.baselines.loadData([{ task, ...baselinesByTaskId[task.id] }]);
            }
        });

        if (stmWasEnabled) {
            project.stm?.enable();
        }

        this._comparingVersionId = version.id;
    }

    /**
     * Stops comparing a currently compared version.
     */
    stopComparing() {
        if (!this._comparingVersionId) {
            return;
        }

        const
            { project } = this.client,
            stmWasEnabled = project.stm?.enabled;

        project.stm?.disable();

        project.taskStore.traverse(task => {
            task.baselines.removeAll();
        });

        if (stmWasEnabled) {
            project.stm?.enable();
        }

        this._comparingVersionId = null;
    }

    doDisable(disable) {
        super.doDisable(disable);
        if (disable) {
            this.detachProjectListeners();
        }
        else {
            this.attachProjectListeners(this.client.project);
        }
        this.client.refresh();
    }

    getEntityDescriptor(model) {
        const recordedType = this.knownBaseTypes.find(knownType => model instanceof knownType) ?? model.constructor;
        if (model.isDependencyModel) {
            return new ChangeLogDependencyEntity({
                model,
                fromTask : this.getEntityDescriptor(model.fromTask),
                toTask   : this.getEntityDescriptor(model.toTask)
            });
        }
        else if (model.isAssignmentModel) {
            return new ChangeLogAssignmentEntity({
                model,
                event    : this.getEntityDescriptor(model.event),
                resource : this.getEntityDescriptor(model.resource)
            });
        }
        return new ChangeLogEntity({ model, type : recordedType });
    }

    /**
     * Create a changelog action from a set of STM UpdateActions affecting a single entity (model).
     * @param {Core.data.stm.action.UpdateAction[]} stmUpdateActions
     * @returns {SchedulerPro.model.changelog.ChangeLogUpdateAction} A ChangeLogUpdateAction representing the updates to this entity
     * @private
     */
    getUpdateAction(stmUpdateActions) {
        const
            entityModel = stmUpdateActions[0].model,
            // Flatten all property updates for this entity from all actions, as { property, value, oldValue }
            allPropertyUpdates =
                stmUpdateActions.flatMap(({ newData, oldData }) =>
                    Object.entries(newData)
                        .map(([property, value]) => ({
                            property,
                            value,
                            oldValue : oldData[property]
                        }))
                        .filter(({ property, value, oldValue }) =>
                            !ObjectHelper.isEqual(value, oldValue) &&
                                entityModel.getFieldDefinition(property)?.persist
                        )
                ),
            // When we have multiple changes for the same property in the same transaction,
            // drop intermediate values and keep only first 'before' and last 'after'
            allUpdatesByProperty = ArrayHelper.groupBy(allPropertyUpdates, 'property'),
            propertyUpdates = Object.entries(allUpdatesByProperty)
                .map(([property, changes]) => ({
                    property,
                    before : changes[0].oldValue,
                    after  : changes[changes.length - 1].value
                })),
            isInitialUserAction = stmUpdateActions.some(({ isInitialUserAction }) => isInitialUserAction);

        return new ChangeLogUpdateAction({
            actionType : 'update',
            entity     : this.getEntityDescriptor(entityModel),
            propertyUpdates,
            isInitialUserAction
        });
    }

    /**
     * This listener is used to exclude changes that occur during a project's initial load and scheduling.
     * @private
     */
    onProjectLoad() {
        this._currentStmTransaction = null;
        // Must early-close the project-load transaction so immediate follow-on changes get put into a new transaction by STM
        this.stopTransaction();
    }

    onStmRecordingStart({ transaction }) {
        this._transactionStart = new Date();
        this._currentStmTransaction = transaction;
        this.triggerTransactionChange();
    }

    /**
     * Force-stop a transaction immediately.
     * @private
     */
    stopTransaction() {
        if (this.client.project?.stm.isRecording) {
            this.client.project?.stm.stopTransaction();
        }
        this.handleTransactionStop();
    }

    async onStmRestoringStop({ cause, transactions }) {
        const me = this;
        if (cause === 'undo') {
            me.transactionDescription = me.L(`L{undid}`);
        }
        else if (cause === 'redo') {
            me.transactionDescription = me.L(`L{redid}`);
        }
        await me.client.project.commitAsync();
        me._transactionStart = new Date();
        // Synthetic transaction with joined queue
        me.finalizeTransaction({ queue : transactions.flatMap(txn => txn.queue) });
    }

    /**
     * Track ranges of remote actions inside a single transaction, to exclude them from changelog
     * recording later in finalizeTransaction.
     * @private
     */
    onBeforeApplyRemoteChanges() {
        if (this._remoteChangesStartPos == null) {
            this._remoteChangesStartPos = this._currentStmTransaction?.queue.length ?? 0;
        }
    }

    async onAfterApplyRemoteChanges() {
        this.endCurrentRemoteActionRange();
        await this.client.project.commitAsync();
    }

    /**
     * Process the end of a remote-changes subset of a current transaction, adding to the tracked set of
     * ranges.
     * @private
     */
    endCurrentRemoteActionRange() {
        const me = this;
        if (me._remoteChangesStartPos != null) {
            const currentPos = me._currentStmTransaction?.queue.length ?? 0;
            if (currentPos !== me._remoteChangesStartPos) {
                me._remoteActionRanges.push({ start : me._remoteChangesStartPos, end : currentPos });
            }
            me._remoteChangesStartPos = null;
        }
    }

    /**
     * @internal
     */
    handleTransactionStop() {
        const me = this;
        if (me.hasChanges) {
            me.finalizeTransaction(me._currentStmTransaction);
        }
        me._currentTransactionDescription = null;
        me._currentStmTransaction = null;
        me._remoteActionRanges = [];
        me.triggerTransactionChange();
    }

    /**
     * @private
     */
    excludeRanges(array, ranges) {
        let keep = [],
            position = 0;
        for (const { start, end } of ranges) {
            keep = keep.concat(array.slice(position, start));
            position = end;
        }
        return keep.concat(array.slice(position));
    }

    /**
     * Package the tracked set of project changes into an ChangeLogTransaction.
     * @private
     */
    finalizeTransaction(stmTransaction) {
        const me = this;

        // In case transaction ends in the middle of tracking remote changes, finalize those ranges here
        me.endCurrentRemoteActionRange();

        const localActions = me.excludeRanges(stmTransaction.queue, me._remoteActionRanges);
        if (localActions.length === 0) {
            return;
        }
        const
            // Group updates by their affected model's ID
            uniqueRecordId = ({ model }) => model.$entityName,
            actionsByType = ArrayHelper.groupBy(localActions, 'type'),
            allChanges = [];

        for (const updateActionType of ['UpdateAction', 'EventUpdateAction']) {
            const updateActionsByRecordId = ArrayHelper.groupBy(actionsByType[updateActionType] || [], uniqueRecordId);
            for (const updateActions of Object.values(updateActionsByRecordId)) {
                allChanges.push(me.getUpdateAction(updateActions));
            }
        }

        for (const { modelList, isInitialUserAction } of actionsByType['RemoveAction'] || []) {
            for (const model of modelList) {
                allChanges.push(new ChangeLogAction({
                    actionType : 'remove',
                    entity     : me.getEntityDescriptor(model),
                    isInitialUserAction
                }));
            }
        }

        for (const { childModels } of actionsByType['RemoveChildAction'] || []) {
            for (const childModel of childModels) {
                allChanges.push(new ChangeLogAction({
                    actionType : 'remove',
                    entity     : me.getEntityDescriptor(childModel)
                }));
            }
        }

        // e.g. adding links (dependencies)
        for (const { modelList } of actionsByType['AddAction'] || []) {
            for (const model of modelList) {
                allChanges.push(new ChangeLogAction({
                    actionType : 'add',
                    entity     : me.getEntityDescriptor(model)
                }));
            }
        }

        // e.g. adding, moving tasks
        for (const { childModels, parentModel, context, insertIndex } of actionsByType['InsertChildAction'] || []) {
            // When moving a task in the tree, we get one InsertChildAction w/ context. Convert this to 'move'
            for (const childModel of childModels) {
                const childContext = context.get(childModel);
                if (childContext != undefined) {
                    allChanges.push(new ChangeLogMoveAction({
                        actionType : 'move',
                        entity     : me.getEntityDescriptor(childModel),
                        from       : {
                            parent : me.getEntityDescriptor(childContext.parent),
                            index  : childContext.index
                        },
                        to : {
                            parent : me.getEntityDescriptor(parentModel),
                            index  : insertIndex
                        }
                    }));
                }
                else {
                    allChanges.push(new ChangeLogAction({
                        actionType : 'add',
                        entity     : me.getEntityDescriptor(childModel)
                    }));
                }
            }
        }

        const transaction = new me.transactionModelClass({
            description : me._currentTransactionDescription,
            actions     : allChanges,
            occurredAt  : me._transactionStart
        });
        me.changeStore.add(transaction);
    }

    /**
     * Sets the description of the current transaction. This will override the default
     * transaction description.
     *
     * @property {String}
     * @category Common
     */
    set transactionDescription(description) {
        this._currentTransactionDescription = description;
    }

    /**
     * Whether a pending transaction is open with changes not yet added to the changelog.
     *
     * @property {Boolean}
     * @category Common
     */
    get hasChanges() {
        return this._currentStmTransaction?.queue?.length > 0;
    }

    get hasUnattachedTransactions() {
        return this.changeStore.find(transaction => transaction.versionId == null);
    }

    /**
     * Whether a saved version is currently being compared.
     *
     * @property {Boolean}
     * @category Common
     */
    get isComparing() {
        return this._comparingVersionId != null;
    }
}

GridFeatureManager.registerFeature(Versions, false, 'SchedulerPro');
