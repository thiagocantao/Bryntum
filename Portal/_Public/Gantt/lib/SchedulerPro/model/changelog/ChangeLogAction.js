/**
 * @module SchedulerPro/model/changelog/ChangeLogAction
 */

/**
 * An immutable, serializable object that describes an action that affected a single entity.
 */
export default class ChangeLogAction {

    static $name = 'ChangeLogAction';

    constructor({ entity, actionType }) {
        Object.assign(this, {
            /**
             * @member {SchedulerPro.model.changelog.ChangeLogEntity} entity A descriptor for the entity (object) affected by this action.
             * @readonly
             * @immutable
             * @category Common
             */
            entity,

            /**
             * @member {'add'|'remove'|'update'} actionType The type of change.
             * @readonly
             * @category Common
             */
            actionType

        });
        if (this.constructor === ChangeLogAction) {
            Object.freeze(this);
        }
    }
}
