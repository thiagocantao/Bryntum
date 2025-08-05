import GridRowReorder from '../../Grid/feature/RowReorder.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import TransactionalFeature from './mixin/TransactionalFeature.js';

/**
 * @module Scheduler/feature/RowReorder
 */

/**
 * This feature implement support for project transactions and used by default in Gantt. For general RowReorder feature
 * documentation see {@link Grid.feature.RowReorder}.
 * @extends Grid/feature/RowReorder
 * @classtype rowReorder
 * @feature
 *
 * @typings Grid.feature.RowReorder -> Grid.feature.GridRowReorder
 */
export default class RowReorder extends TransactionalFeature(GridRowReorder) {
    static $name = 'RowReorder';

    onDragStart(...args) {
        super.onDragStart(...args);

        if (this.client.transactionalFeaturesEnabled) {
            return this.startFeatureTransaction();
        }
    }

    onDrop(...args) {
        // Actual reorder will happen in a wrapper function to `tryPropagateWithChanges`, meaning reorder will be a
        // transaction. This transaction will not even have any changes in it. So we can reject it.
        this.rejectFeatureTransaction();

        return super.onDrop(...args);
    }

    onAbort(...args) {
        this.rejectFeatureTransaction();

        return super.onAbort(...args);
    }
}

GridFeatureManager.registerFeature(RowReorder, false, 'Scheduler');
GridFeatureManager.registerFeature(RowReorder, true, 'Gantt');
