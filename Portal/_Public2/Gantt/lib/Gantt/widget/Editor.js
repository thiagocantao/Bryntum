import GridEditor from '../../Common/widget/Editor.js';
import StringHelper from '../../Common/helper/StringHelper.js';
import DependencyField from './DependencyField.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

export default class Editor extends GridEditor {
    async onEditComplete() {
        const
            me          = this,
            { record, dataField, inputField, oldValue, lastAlignSpec, owner : grid } = me,
            { target }  = lastAlignSpec;

        let { value } = inputField;

        if (inputField instanceof DependencyField) {
            if (!me.isFinishing) {
                // Hiding must not trigger our blurAction
                me.isFinishing = true;
                me.hide();

                if (record && value) {
                    value = value.slice();

                    const
                        dependencyStore = grid.dependencyStore,
                        toValidate      = value.filter(v => !v.project),
                        results         = [];

                    // Only allowed to check one dependency at the time, since validation triggers a propagate.
                    // Need to wait for one to finish before starting the next.
                    async function checkNext() {
                        const dependency = toValidate.shift();

                        if (dependency) {
                            // TODO: Could bail out on first fail, but probably a more detailed error msg should be shown
                            //   instead with info on which deps where invalid
                            results.push(
                                await dependencyStore.isValidDependency({
                                    fromEvent : dependency.fromEvent,
                                    toEvent   : dependency.toEvent,
                                    lag       : dependency.lag,
                                    lagUnit   : dependency.lagUnit,
                                    type      : dependency.type
                                })
                            );

                            await checkNext();
                        }
                    }

                    // Will wait for all to finish, since it is recursive
                    await checkNext();

                    const valid = results.every(result => result);

                    if (valid) {
                        const setterName = `set${StringHelper.capitalizeFirstLetter(dataField)}`;
                        if (record[setterName]) {
                            record[setterName](value);
                        }
                        else {
                            record[dataField] = value;
                        }

                        me.trigger('completeAsync', { value, oldValue });
                    }
                    else {
                        me.trigger('cancelAsync', { value, oldValue });
                    }
                }

                me.trigger('complete', { value, oldValue });
                if (target.nodeType === 1) {
                    target.classList.remove('b-editing');
                }

                me.isFinishing = false;
            }

            return;
        }

        super.onEditComplete();
    }
}

BryntumWidgetAdapterRegister.register('gantteditor', Editor);
