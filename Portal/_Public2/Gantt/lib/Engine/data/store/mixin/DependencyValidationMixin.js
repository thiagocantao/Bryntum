import { EffectResolutionResult, GraphCycleDetectedEffect } from "../../../../ChronoGraph/chrono/Effect.js";
import { PropagationResult } from "../../../../ChronoGraph/chrono/Graph.js";
import { hasDependencyMixin } from "../../model/DependencyMixin.js";
const hasMixin = Symbol('DependencyValidationMixin');
export const DependencyValidationMixin = (base) => {
    class DependencyValidationMixin extends base {
        [hasMixin]() { }
        async isValidDependency(dependency, toId, depType) {
            const me = this, project = me.getProject();
            //<debug>
            console.assert(!hasDependencyMixin(dependency) || dependency.getProject() != project, 'Can\'t validate dependency, the dependency given is already a part of this project, validation can be done only on depepdencies which are not joined yet!');
            //</debug>
            // In case we are currently propagating
            await project.waitForPropagateCompleted();
            // Effect handler, here we are interested only in cycles
            const effectHandler = async (effect) => {
                let result;
                if (effect instanceof GraphCycleDetectedEffect) {
                    result = EffectResolutionResult.Cancel;
                }
                else {
                    result = EffectResolutionResult.Resume;
                }
                return result;
            };
            if (!hasDependencyMixin(dependency)) {
                const eventStore = me.getEventStore(), dependencyClass = me.getDependencyStore().modelClass;
                if (typeof dependency == 'object') {
                    dependency = new dependencyClass(dependency);
                }
                else {
                    dependency = new dependencyClass({
                        fromEvent: eventStore.getById(dependency),
                        toEvent: eventStore.getById(toId),
                        type: depType
                    });
                }
            }
            const oldProject = dependency.getProject();
            dependency.setProject(project);
            let result = await project.tryPropagateWithEntities(effectHandler, [dependency]);
            dependency.setProject(oldProject);
            return result == PropagationResult.Passed;
        }
    }
    return DependencyValidationMixin;
};
/**
 * Dependency validation mixin type guard
 */
export const hasDependencyValidationMixin = (store) => Boolean(store && store[hasMixin]);
