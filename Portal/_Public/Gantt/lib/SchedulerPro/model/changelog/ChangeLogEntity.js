/**
 * @module SchedulerPro/model/changelog/ChangeLogEntity
 */

/**
 * An immutable, serializable object that describes an entity instance (for example, a single task).
 * ChangeLogEntity instances appear in change log entries to indicate which entity was affected by
 * a {@link SchedulerPro/model/changelog/ChangeLogAction}.
 */
export default class ChangeLogEntity {

    static $name = 'ChangeLogEntity';

    constructor({ model, type }) {
        Object.assign(this, {
            /**
             * @member {String} id The unique id of the entity instance, e.g. 'TaskModel-1'.
             * @readonly
             * @category Common
             */
            id : model.id ?? model.$entityName,

            /**
             * @member {String} type The name of the Model type of the entity, e.g. 'TaskModel'.
             * @readonly
             * @category Common
             */
            type : type?.$$name ?? model.constructor.name,

            /**
             * @member {String} name A user-friendly name for the entity instance, e.g. 'My important task'.
             * @readonly
             * @category Common
             */
            name : model.name ?? model.$entityName
        });
        if (new.target === ChangeLogEntity) {
            Object.freeze(this);
        }
    }
}
