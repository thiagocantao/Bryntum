import ChangeLogAction from './ChangeLogAction.js';

/**
 * @module SchedulerPro/model/changelog/ChangeLogMoveAction
 */

/**
 * The location of a {@link SchedulerPro.model.changelog.ChangeLogEntity} in a tree.
 * @typedef {Object} ChangeLogEntityLocation
 * @property {SchedulerPro.model.changelog.ChangeLogEntity} parent The parent node of which the target entity is a child, if any
 * @property {Number} index The index of the child within the list of parent's children
 */

/**
 * An immutable, serializable object that describes an action that updated properties on a single entity.
 *
 * @extends SchedulerPro/model/changelog/ChangeLogAction
 */
export default class ChangeLogMoveAction extends ChangeLogAction {

    static get $name() {
        return 'ChangeLogMoveAction';
    }

    constructor({ entity, from, to }) {
        super({
            entity,
            actionType : 'move'
        });
        Object.assign(this, {

            /**
             * @member {ChangeLogEntityLocation} from The original location from which the entity was moved
             * @readonly
             * @immutable
             * @category Common
             */
            from,

            /**
             * @member {ChangeLogEntityLocation} to The new location to which the entity was moved
             * @readonly
             * @immutable
             * @category Common
             */
            to
        });
        Object.freeze(this);
    }
}
