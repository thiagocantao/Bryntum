/**
 * @module SchedulerPro/data/stm/action/EventUpdateAction
 */
import UpdateAction from '../../../../Core/data/stm/action/UpdateAction.js';

/**
 * Action to record the fact that an event model has been updated.
 * @extends Core/data/stm/action/UpdateAction
 */
export default class EventUpdateAction extends UpdateAction {

    get type() {
        return 'EventUpdateAction';
    }

    construct(config) {
        let {
            model,
            newData,
            oldData
        } = config;

        // If we have "segments" represented in both old & new data states
        if (newData.segments && oldData.segments) {
            oldData = { ...oldData };
            newData = { ...newData };

            const
                oldDataSegments = oldData.segments.slice(),
                newDataSegments = newData.segments.slice();

            let hasChanges = false;

            // If a segment instance exists in both states
            // we need to find segments existing in both versions.
            // They should not be changed when undo/redo the main event
            // since their model changes are recorded by STM.
            oldData.segments.forEach((segment, index) => {
                const newDataIndex = newData.segments.indexOf(segment);

                // If a segment instance exists in both states
                // we uses it as-is ..since STM is supposed to handle the instance changes
                if (newDataIndex > -1) {
                    oldDataSegments[index] = newDataSegments[newDataIndex] = segment;
                    hasChanges = true;
                }
            });

            if (hasChanges) {
                oldData.segments = oldDataSegments;
                newData.segments = newDataSegments;
            }
        }

        return super.construct({
            model,
            newData,
            oldData
        });
    }
}
