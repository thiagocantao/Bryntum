import Combo from '../../Common/widget/Combo.js';
import Store from '../../Common/data/Store.js';
import { SchedulingMode } from '../../Engine/scheduling/Types.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/SchedulingModePicker
 */

/**
 * Combo box preconfigured with possible scheduling mode values.
 *
 * @extends Common/widget/Combo
 */
export default class SchedulingModePicker extends Combo {
    static get type() {
        return 'schedulingmodecombo';
    }

    buildStoreData() {
        return [{
            id   : SchedulingMode.Normal,
            text : this.L('Normal')
        }, {
            id   : SchedulingMode.FixedDuration,
            text : this.L('Fixed Duration')
        }, {
            id   : SchedulingMode.FixedUnits,
            text : this.L('Fixed Units')
        }, {
            id   : SchedulingMode.FixedEffort,
            text : this.L('Fixed Effort')
        }];
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

    updateLocalization() {
        super.updateLocalization();
        // rebuild newly translated options
        this.store.data = this.buildStoreData();
    }
}

BryntumWidgetAdapterRegister.register(SchedulingModePicker.type, SchedulingModePicker);
