import Combo from '../../Common/widget/Combo.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/ModelCombo
 */

/**
 * Special combo class returning a model from the store as it's value
 */
export default class ModelCombo extends Combo {
    static get type() {
        return 'modelcombo';
    }

    get value() {
        const superValue = super.value,
            model = this.store.getById(superValue);

        return model || superValue;
    }

    set value(v) {
        super.value = v;
    }
}

BryntumWidgetAdapterRegister.register(ModelCombo.type, ModelCombo);
