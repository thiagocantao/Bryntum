import ChangeLogAction from './ChangeLogAction.js';

/**
 * @module SchedulerPro/model/changelog/ChangeLogUpdateAction
 */

/**
 * An immutable, serializable object that describes an action that updated properties on a single entity.
 *
 * @extends SchedulerPro/model/changelog/ChangeLogAction
 */
export default class ChangeLogUpdateAction extends ChangeLogAction {

    static get $name() {
        return 'ChangeLogUpdateAction';
    }

    constructor({ entity, propertyUpdates, isInitialUserAction }) {
        super({
            entity,
            actionType : 'update'
        });
        Object.assign(this, {
            /**
             * @member {SchedulerPro.model.changelog.ChangeLogPropertyUpdate[]} propertyUpdates The individual property updates, for 'update' type actions.
             * @readonly
             * @immutable
             * @category Common
             */
            propertyUpdates,

            /**
             * @member {Boolean} isUser Whether the action is part of the initial action taken by the user via the UI, or a follow-on
             *                          (computed) action.
             * @readonly
             * @immutable
             * @category Common
             */
            isUser : isInitialUserAction
        });
        Object.freeze(this);
    }
}
