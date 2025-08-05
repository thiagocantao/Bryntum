import GridCellEdit from '../../Grid/feature/CellEdit.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module SchedulerPro/feature/CellEdit
 */

/**
 * Extends the {@link Grid.feature.CellEdit} to encapsulate SchedulerPro functionality. This feature is enabled by **default**
 *
 * @extends Grid/feature/CellEdit
 *
 * @classtype cellEdit
 * @feature
 *
 * @typings Grid.feature.CellEdit -> Grid.feature.GridCellEdit
 */
export default class CellEdit extends TransactionalFeature(GridCellEdit) {
    static get $name() {
        return 'CellEdit';
    }

    async startEditing(cellContext = {}) {
        const result = await super.startEditing(cellContext);

        if (result) {
            await this.startFeatureTransaction();
        }

        return result;
    }

    async finishEditing() {
        const result = await super.finishEditing();

        if (result) {
            await this.finishFeatureTransaction?.();
        }

        return result;
    }

    cancelEditing(silent = false, triggeredByEvent) {
        this.rejectFeatureTransaction();

        super.cancelEditing(silent, triggeredByEvent);
    }
}

GridFeatureManager.registerFeature(CellEdit, true, 'SchedulerPro');
