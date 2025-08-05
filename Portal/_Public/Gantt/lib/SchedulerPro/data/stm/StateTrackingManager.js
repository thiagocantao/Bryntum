/**
 * @module SchedulerPro/data/stm/StateTrackingManager
 */
import EventUpdateAction from './action/EventUpdateAction.js';
import CoreStateTrackingManager from '../../../Core/data/stm/StateTrackingManager.js';
import UpdateAction from '../../../Core/data/stm/action/UpdateAction.js';

export const makeModelUpdateAction = (model, newData, oldData) => {
    // if that's a SplitEventMixin instance
    if (model.isSplitEventMixin) {
        return new EventUpdateAction({
            model,
            newData,
            oldData
        });
    }

    return new UpdateAction({
        model,
        newData,
        oldData
    });
};

/**
 * {@link Core/data/stm/StateTrackingManager} subclass that's aware of the Scheduler Pro data structure specifics,
 * namely supports tracking of event segment changes.
 *
 * There is normally no need to deal with this class manually since it's instantiated automatically by the project
 * and can be reached like this:
 * ```javascript
 * project.stm
 * ```
 *
 * ## Tracking store changes
 *
 * Tracks the state of every store registered via {@link #function-addStore}. It is {@link #config-disabled} by default
 * so remember to call {@link #function-enable} when your stores are registered and initial dataset is loaded.
 * Use {@link #function-undo} / {@link #function-redo} method calls to restore state to a particular
 * point in time
 *
 * ```javascript
 * stm = new StateTrackingManager({
 *     autoRecord : true,
 *     listeners  : {
 *        'recordingstop' : () => {
 *            // your custom code to update undo/redo GUI controls
 *            updateUndoRedoControls();
 *        },
 *        'restoringstop' : ({ stm }) => {
 *            // your custom code to update undo/redo GUI controls
 *            updateUndoRedoControls();
 *        }
 *    },
 *    getTransactionTitle : (transaction) => {
 *        // your custom code to analyze the transaction and return custom transaction title
 *        const lastAction = transaction.queue[transaction.queue.length - 1];
 *
 *        if (lastAction instanceof AddAction) {
 *            let title = 'Add new record';
 *        }
 *
 *        return title;
 *    }
 * });
 *
 * stm.addStore(userStore);
 * stm.addStore(companyStore);
 * stm.addStore(otherStore);
 *
 * stm.enable();
 * ```
 *
 * ## Resetting the queue on data loading
 *
 * When loading data from the server it makes perfect sense to {@link #function-resetQueue reset the queue}.
 *
 * If project (CrudManager protocol) is used for data loading it can be done like this:
 *
 * ```javascript
 * project.on({
 *     load() {
 *         project.stm.resetQueue();
 *     }
 * });
 * ```
 *
 * and in case individual stores are used:
 *
 * ```javascript
 * ajaxStore.on({
 *     load() {
 *         ajaxStore.stm.resetQueue();
 *     }
 * });
 * ```
 *
 * @extends Core/data/stm/StateTrackingManager
 *
 * @typings Core.data.stm.StateTrackingManager -> Core.data.stm.CoreStateTrackingManager
 */
export default class StateTrackingManager extends CoreStateTrackingManager {

    static get defaultConfig() {
        return {
            makeModelUpdateAction
        };
    }

}
