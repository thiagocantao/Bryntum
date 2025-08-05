import Base from '../../../Core/Base.js';
import AttachToProjectMixin from '../../data/mixin/AttachToProjectMixin.js';
import IdHelper from '../../../Core/helper/IdHelper.js';

/**
 * @module Scheduler/feature/mixin/TransactionalFeature
 */

/**
 * Feature defining methods to lock the view for a time of a user action
 * @internal
 * @mixin
 */
export default Target => class TransactionalFeature extends AttachToProjectMixin(Target || Base) {
    static $name = 'TransactionalFeature';

    //#region AttachToProjectMixin implementation

    detachFromProject(project) {
        this.rejectFeatureTransaction();
        super.detachFromProject(project);
    }

    //#endregion

    getStmCapture() {
        const result = super.getStmCapture();
        result._editorPromiseResolve = this._editorPromiseResolve;
        return result;
    }

    applyStmCapture(stmCapture) {
        super.applyStmCapture(stmCapture);

        this._editorPromiseResolve = stmCapture._editorPromiseResolve;
    }

    async startFeatureTransaction() {
        if (!this.client.transactionalFeaturesEnabled) {
            return;
        }

        const
            me          = this,
            { project } = me.client,
            { stm }     = project;

        // Await previous promise chain to resolve
        let chainResolved;

        if (me.hasStmCapture) {
            stm.startTransaction();
        }
        else {
            chainResolved = project.queue(() => project.commitAsync());
        }

        project.queue(() => {
            if (!me.hasStmCapture) {
                me._stmInitiallyDisabled = stm.disabled;
                me._stmInitiallyAutoRecord = stm.autoRecord;

                if (stm.isRecording) {
                    stm.stopTransaction();
                }
                else if (me._stmInitiallyDisabled) {
                    stm.enable();
                }

                // Disable autoRecord to avoid finishing transaction after a timeout
                stm.autoRecord = false;
            }

            if (!stm.isRecording) {
                // We need to wrap cell editing into own transaction to be able to apply user changes last
                stm.startTransaction();
            }

            me.trigger?.('featureTransactionStart');

            // Put an empty promise to the queue to pause it
            return new Promise(resolve => me._editorPromiseResolve = resolve);
        });

        await chainResolved;
    }

    rejectFeatureTransaction() {
        if (!this.client.transactionalFeaturesEnabled) {
            return;
        }

        const
            me = this,
            { stm } = me.client.project;

        me._editorPromiseResolve?.();
        me._editorPromiseResolve = null;

        stm.isRecording && stm.rejectTransaction();

        if (!me.hasStmCapture && me._stmInitiallyDisabled != null) {
            stm.disabled = me._stmInitiallyDisabled;
            stm.autoRecord = me._stmInitiallyAutoRecord;
        }

        me.trigger('featureTransactionReject');
    }

    async finishFeatureTransaction(afterApplyStashCallback) {
        if (!this.client.transactionalFeaturesEnabled) {
            return;
        }

        const
            me            = this,
            { project }   = me.client,
            { stm }       = project;

        // In case there is a commit pending, we need to wait to not suspend more events than we should
        if (!project.isEngineReady()) {
            await project.commitAsync();
        }

        const
            transactionId = stm.stash(),
            {
                _stmInitiallyDisabled,
                _stmInitiallyAutoRecord
            }             = me,
            // This id is used to help debugging concurrent promises
            id            = IdHelper.generateId('featureTransaction');

        me._editorPromiseResolve?.();
        me._editorPromiseResolve = null;

        if (!me.isDestroying) {
            me.trigger('featureTransactionFinalizeStart', { id });
        }

        return project.queue(async() => {
            stm?.applyStash(transactionId);

            await afterApplyStashCallback?.();

            await project.commitAsync?.();

            if (stm.isRecording) {
                stm.stopTransaction();
            }

            if (!me.hasStmCapture && stm && !stm.isDestroying && _stmInitiallyDisabled != null) {
                stm.disabled = _stmInitiallyDisabled;
                stm.autoRecord = _stmInitiallyAutoRecord;
            }

            me.trigger?.('featureTransactionFinalized', { id });
        });
    }
};
