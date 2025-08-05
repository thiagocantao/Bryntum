import StringHelper from '../../Core/helper/StringHelper.js';
import SchedulingIssueResolutionPopup from './SchedulingIssueResolutionPopup.js';
import '../../Core/widget/Combo.js';

/**
 * @module SchedulerPro/widget/CycleResolutionPopup
 */

/**
 * Class implementing a dialog informing user of an infinite cycle in the data.
 * The dialog displays tasks and dependencies causing the cycle and allows
 * to pick one of the dependencies and either deactivate or remove it.
 *
 * @demo SchedulerPro/conflicts
 * @extends SchedulerPro/widget/SchedulingIssueResolutionPopup
 * @classType cycleresolutionpopup
 */
export default class CycleResolutionPopup extends SchedulingIssueResolutionPopup {

    static get $name() {
        return 'CycleResolutionPopup';
    }

    // Factoryable type name
    static get type() {
        return 'cycleresolutionpopup';
    }

    getDependencyTitle(dependency) {
        return `"${StringHelper.encodeHtml(dependency.fromEvent.name)}" -> "${StringHelper.encodeHtml(dependency.toEvent.name)}"`;
    }

    getResolutionWidgetConfig(resolution) {
        const
            { dependency }      = resolution,
            invalidDependencies = this.schedulingIssue.getInvalidDependencies(),
            result              = super.getResolutionWidgetConfig(resolution);

        if (invalidDependencies.includes(dependency)) {
            Object.assign(result, {
                name    : `dependency-resolution`,
                // if it's the first resolution for that dependency - check it
                checked : !this._dependencyResolutionsChecked++
            });
        }

        return result;
    }

    getResolutions() {
        const
            { schedulingIssue } = this,
            invalidDependencies = schedulingIssue?.getInvalidDependencies();

        let resolutions = schedulingIssue?.getResolutions();

        // If there are invalid dependencies involved (like parent-child or self-to-self)
        // let's not suggests other resolutions to simplify the UI

        if (resolutions && invalidDependencies.length) {
            resolutions = resolutions.filter(r => r.dependency && invalidDependencies.includes(r.dependency));
        }

        return resolutions;
    }

    updatePopupContent(schedulingIssue, continueWithResolutionResult) {
        const
            me = this,
            { invalidDependenciesDescription, dependencyField } = me.widgetMap;

        me._dependencyResolutionsChecked = 0;

        super.updatePopupContent(...arguments);

        // hide entries initially
        invalidDependenciesDescription?.hide();
        dependencyField?.hide();

        schedulingIssue = me.schedulingIssue;

        if (schedulingIssue) {
            const
                dependencies        = schedulingIssue.getDependencies(),
                invalidDependencies = schedulingIssue.getInvalidDependencies(),
                validDependencies   = dependencies.filter(dependency => !invalidDependencies.includes(dependency));

            // If we've got invalid dependencies tha MUST be addressed
            if (invalidDependencies.length) {
                if (invalidDependenciesDescription) {
                    invalidDependenciesDescription.show();
                }
                else {
                    me.add({
                        type   : 'widget',
                        ref    : 'invalidDependenciesDescription',
                        weight : -50,
                        width  : '100%',
                        cls    : 'b-invalid-dependencies-description',
                        html   : me.L('L{invalidDependencyLabel}')
                    });
                }
            }
            // got dependency combo - show there all dependencies building the cycle
            else {
                const dependencyItems = validDependencies?.map(dep => ({
                    value : dep.id,
                    text  : me.getDependencyTitle(dep)
                }));

                if (dependencyField) {
                    dependencyField.value = null;
                    dependencyField.items = dependencyItems;
                    dependencyField.show();
                }
                // no dependency combo - build it w/ all the dependencies building the cycle
                else {
                    me.add({
                        type              : 'combo',
                        ref               : 'dependencyField',
                        weight            : 50,
                        width             : '100%',
                        name              : 'dependency',
                        label             : me.L('L{dependencyLabel}'),
                        labelPosition     : 'above',
                        cls               : 'b-dependency-field',
                        items             : dependencyItems,
                        internalListeners : {
                            change : 'up.onDependencyChange'
                        }
                    });
                }
            }
        }
    }

    get canApply() {
        const { widgetMap } = this;

        // can apply if any resolution and dependency is chosen or if cancel is selected
        return super.canApply &&
            (widgetMap.cancelResolution.checked || (!widgetMap.dependencyField || widgetMap.dependencyField.value));
    }

    onDependencyChange() {
        this.toggleControlsState();
    }

    getResolutionParameters(resolution) {
        // These resolution types need a dependency to be passed to resolve() method as an argument
        if (resolution.isRemoveDependencyCycleEffectResolution || resolution.isDeactivateDependencyCycleEffectResolution) {
            const
                dependencyId = this.widgetMap.dependencyField.value,
                dependency = this.project.dependencyStore.getById(dependencyId);



            return [dependency];
        }

        return super.getResolutionParameters(resolution);
    }
};

// Register this widget type with its Factory
CycleResolutionPopup.initClass();
