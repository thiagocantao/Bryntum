import Combo from '../../Common/widget/Combo.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';
import LocaleManager from '../../Common/localization/LocaleManager.js';
import GanttCommon from '../localization/Common.js';

/**
 * @module Gantt/widget/DependencyTypePicker
 */

const buildItems = (items) => items.map((item, index) => [index, item]);

/**
 * Selects a Dependency linkage type between two tasks.
 *
 * @extends Common/widget/Combo
 *
 * @classType dependencytypepicker
 */
export default class DependencyTypePicker extends Combo {
    static get type() {
        return 'dependencytypepicker';
    }
    
    construct(config) {
        super.construct(config);
    
        // Update when changing locale
        LocaleManager.on({
            locale : ({ locale }) => {
                this.items = buildItems(locale.GanttCommon.dependencyTypesLong);
            },
            thisObj : this
        });
    }
    
    get store() {
        if (!this._items) {
            this.items = this._items = buildItems(GanttCommon.L('dependencyTypesLong'));
        }

        return super.store;
    }

    set store(store) {
        super.store = store;
    }
}

BryntumWidgetAdapterRegister.register(DependencyTypePicker.type, DependencyTypePicker);
