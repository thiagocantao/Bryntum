import Base from '../../../Core/Base.js';

/**
 * @module Scheduler/feature/mixin/TaskEditTransactional
 */

/**
 * Mixin adding live updates support
 *
 * @mixin
 */
export default Target => class TaskEditTransactional extends (Target || Base) {
    static get $name() {
        return 'TaskEditTransactional';
    }

    captureStm(force) {
        if (this.client.transactionalFeaturesEnabled) {
            super.captureStm();

            return this.startStmTransaction(force);
        }
        else {
            super.captureStm(force);
        }
    }

    freeStm(commitOrReject) {
        if (this.hasStmCapture || !this.client.transactionalFeaturesEnabled) {
            return super.freeStm(commitOrReject);
        }
    }

    async startStmTransaction(startRecordingEarly) {
        if (this.client.transactionalFeaturesEnabled) {
            await this.startFeatureTransaction(startRecordingEarly);
        }
        else {
            super.startStmTransaction();
        }
    }

    commitStmTransaction() {
        if (this.client.transactionalFeaturesEnabled) {
            return this.finishFeatureTransaction();
        }
        else {
            super.commitStmTransaction();
        }
    }

    async rejectStmTransaction() {
        if (this.client.transactionalFeaturesEnabled) {
            this.rejectFeatureTransaction();
        }
        else {
            await super.rejectStmTransaction();
        }
    }
};
