import AssignmentModel from '../AssignmentModel.js';
import ChangeLogEntity from './ChangeLogEntity.js';

/**
 * @module SchedulerPro/model/changelog/ChangeLogAssignmentEntity
 */

/**
 * An immutable, serializable object that describes a resource assignment entity instance.
 *
 * @extends SchedulerPro/model/changelog/ChangeLogEntity
 */
export default class ChangeLogAssignmentEntity extends ChangeLogEntity {

    static $name = 'ChangeLogAssignmentEntity';

    constructor({ model, event, resource }) {
        super({ model, type : AssignmentModel });
        Object.assign(this, {
            /**
             * @member {SchedulerPro.model.changelog.ChangeLogEntity} event The event to which the assignment was made.
             * @readonly
             * @immutable
             * @category Common
             */
            event,

            /**
             * @member {SchedulerPro.model.changelog.ChangeLogEntity} resource The resource that was assigned.
             * @readonly
             * @immutable
             * @category Common
             */
            resource
        });
        Object.freeze(this);
    }
}
