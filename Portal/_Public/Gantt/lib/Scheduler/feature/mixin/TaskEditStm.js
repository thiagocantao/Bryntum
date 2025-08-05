import Base from '../../../Core/Base.js';

/**
 * @module Scheduler/feature/mixin/TaskEditStm
 */

/**
 * Mixin adding STM transactable behavior to TaskEdit feature.
 *
 * @mixin
 */
export default Target => class TaskEditStm extends (Target || Base) {
    static get $name() {
        return 'TaskEditStm';
    }

    getStmCapture() {
        return {
            stmInitiallyAutoRecord : this.stmInitiallyAutoRecord,
            stmInitiallyDisabled   : this.stmInitiallyDisabled,
            // this flag indicates whether the STM capture has been transferred to
            // another feature, which will be responsible for finalizing the STM transaction
            // (otherwise we'll do it ourselves)
            transferred            : false
        };
    }

    applyStmCapture(stmCapture) {
        this.stmInitiallyAutoRecord = stmCapture.stmInitiallyAutoRecord;
        this.stmInitiallyDisabled = stmCapture.stmInitiallyDisabled;
    }

    captureStm(startTransaction = false) {
        const
            me      = this,
            project = me.project,
            stm     = project.getStm();

        if (me.hasStmCapture) {
            return;
        }

        me.hasStmCapture = true;
        me.stmInitiallyDisabled = stm.disabled;
        me.stmInitiallyAutoRecord = stm.autoRecord;

        if (me.stmInitiallyDisabled) {
            stm.enable();
            // it seems this branch has never been exercised by tests
            // but the intention is to stop the auto-recording while
            // task editor is active (all editing is one manual transaction)
            stm.autoRecord = false;
        }
        else {
            if (me.stmInitiallyAutoRecord) {
                stm.autoRecord = false;
            }
            if (stm.isRecording) {
                stm.stopTransaction();
            }
        }

        if (startTransaction) {
            this.startStmTransaction();
        }
    }

    startStmTransaction() {
        this.project.getStm().startTransaction();
    }

    commitStmTransaction() {
        const
            me  = this,
            stm = me.project.getStm();

        if (!me.hasStmCapture) {
            throw new Error('Does not have STM capture, no transaction to commit');
        }

        if (stm.enabled) {
            stm.stopTransaction();

            if (me.stmInitiallyDisabled) {
                stm.resetQueue();
            }
        }
    }

    async rejectStmTransaction() {
        const
            stm        = this.project.getStm(),
            { client } = this;

        if (!this.hasStmCapture) {
            throw new Error('Does not have STM capture, no transaction to reject');
        }

        if (stm.enabled) {
            if (stm.transaction?.length) {
                client.suspendRefresh();

                stm.rejectTransaction();

                await client.resumeRefresh(true);
            }
            else {
                stm.stopTransaction();
            }
        }
    }

    enableStm() {
        this.project.getStm().enable();
    }

    disableStm() {
        this.project.getStm().disable();
    }

    async freeStm(commitOrReject = null) {
        const
            me  = this,
            stm = me.project.getStm(),
            {
                stmInitiallyDisabled,
                stmInitiallyAutoRecord
            } = me;

        if (!me.hasStmCapture) {
            return;
        }

        let promise;

        me.rejectingStmTransaction = true;

        if (commitOrReject === true) {
            promise = me.commitStmTransaction();
        }
        else if (commitOrReject === false) {
            // Note - we don't wait for async to complete here
            promise = me.rejectStmTransaction();
        }

        await promise;

        if (!stm.isDestroying) {
            stm.disabled = stmInitiallyDisabled;
            stm.autoRecord = stmInitiallyAutoRecord;
        }

        if (!me.isDestroying) {
            me.rejectingStmTransaction = true;
            me.hasStmCapture = false;
        }
    };
};
