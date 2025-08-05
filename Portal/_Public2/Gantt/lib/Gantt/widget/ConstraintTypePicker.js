import Combo from '../../Common/widget/Combo.js';
import Store from '../../Common/data/Store.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/ConstraintTypePicker
 */

/**
 * Combo box preconfigured with possible scheduling mode values. This picker doesn't support {@link Common/widget/Combo#config-multiSelect multiSelect}
 *
 * @extends Common/widget/Combo
 */
export default class ConstraintTypePicker extends Combo {
    static get type() {
        return 'constrainttypepicker';
    }

    updateLocalization() {
        super.updateLocalization();
        // rebuild newly translated options
        this.store.data = this.buildStoreData();
    }

    buildStoreData() {
        return [
            {
                id   : 'muststarton',
                text : this.L('Must start on')
            },
            {
                id   : 'mustfinishon',
                text : this.L('Must finish on')
            },
            {
                id   : 'startnoearlierthan',
                text : this.L('Start no earlier than')
            },
            {
                id   : 'startnolaterthan',
                text : this.L('Start no later than')
            },
            {
                id   : 'finishnoearlierthan',
                text : this.L('Finish no earlier than')
            },
            {
                id   : 'finishnolaterthan',
                text : this.L('Finish no later than')
            }
        ];
    }

    get store() {
        if (!this._store) {
            this.store = new Store({
                data : this.buildStoreData()
            });
        }

        return this._store;
    }

    set store(store) {
        super.store = store;
    }
}

BryntumWidgetAdapterRegister.register(ConstraintTypePicker.type, ConstraintTypePicker);
