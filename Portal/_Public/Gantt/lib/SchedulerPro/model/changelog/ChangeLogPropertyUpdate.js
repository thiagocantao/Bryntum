/**
 * @module SchedulerPro/model/changelog/ChangeLogPropertyUpdate
 */

/**
 * An immutable, serializable object that describes an update to a single object property from one value to another.
 */
export default class ChangeLogPropertyUpdate {

    static $name = 'ChangeLogPropertyUpdate';

    constructor({ property, before, after }) {
        Object.assign(this, {
            /**
             * @member {String} property A descriptor for the entity (object) affected by this action.
             * @readonly
             * @category Common
             */
            property,

            /**
             * @member {String|Number|Object} before The property's value before the action.
             * @readonly
             * @immutable
             * @category Common
             */
            before,

            /**
             * @member {String|Number|Object} after The property's value after the action.
             * @readonly
             * @immutable
             * @category Common
             */
            after
        });
        Object.freeze(this);
    }
}
