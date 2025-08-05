import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import SchedulerDependencies from '../../Scheduler/feature/Dependencies.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module SchedulerPro/feature/Dependencies
 */

/**
 * This feature implements support for project transactions and is used by default in Scheduler Pro. For general
 * dependencies documentation see {@link Scheduler.feature.Dependencies}.
 * @extends Scheduler/feature/Dependencies
 * @classtype dependencies
 * @feature
 *
 * @typings Scheduler.feature.Dependencies -> Scheduler.feature.SchedulerDependencies
 */
export default class Dependencies extends TransactionalFeature(SchedulerDependencies) {
    static $name = 'Dependencies';

    onRequestDragCreate(event) {
        const result = super.onRequestDragCreate(event);

        if (result !== false) {
            this.startFeatureTransaction().then();
        }

        return result;
    }

    doAfterDependencyDrop(data) {
        // dependency property is present only if dependency creation went fine
        const
            { dependency } = data,
            {
                taskStore,
                dependencyStore
            }              = this.client;

        if (dependency) {
            this.finishFeatureTransaction(() => {
                if (!taskStore.includes(dependency.fromEvent) || !taskStore.includes(dependency.toEvent)) {
                    dependencyStore.remove(dependency);
                }
            });
        }
        else {
            this.rejectFeatureTransaction();
        }

        super.doAfterDependencyDrop(data);
    }
}

GridFeatureManager.registerFeature(Dependencies, true, 'SchedulerPro');
