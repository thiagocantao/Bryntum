import ChangeLogEntity from './ChangeLogEntity.js';
import DependencyModel from '../DependencyModel.js';

/**
 * @module SchedulerPro/model/changelog/ChangeLogDependencyEntity
 */

/**
 * An immutable, serializable object that describes a dependency entity instance.
 *
 * @extends SchedulerPro/model/changelog/ChangeLogEntity
 */
export default class ChangeLogDependencyEntity extends ChangeLogEntity {

    static $name = 'ChangeLogDependencyEntity';

    constructor({ model, fromTask, toTask }) {
        super({ model, type : DependencyModel });
        Object.assign(this, {
            /**
             * @member {SchedulerPro.model.changelog.ChangeLogEntity} fromTask The 'from' task of the dependency.
             * @readonly
             * @immutable
             * @category Common
             */
            fromTask,

            /**
             * @member {SchedulerPro.model.changelog.ChangeLogEntity} toTask The 'to' task of the dependency.
             * @readonly
             * @immutable
             * @category Common
             */
            toTask
        });
        Object.freeze(this);
    }
}
