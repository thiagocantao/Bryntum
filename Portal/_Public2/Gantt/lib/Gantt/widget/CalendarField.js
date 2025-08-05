import ModelCombo from './ModelCombo.js';
import BryntumWidgetAdapterRegister from '../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/CalendarField
 */

/**
 * Event calendar selector combo.
 */
export default class CalendarField extends ModelCombo {
    static get type() {
        return 'calendarfield';
    }

    static get defaultConfig() {
        return {
            valueField   : 'id',
            displayField : 'name',
            editable     : false,

            listItemTpl : c => {
                return c && c.name ? c.name : this.L('Default calendar');
            },

            displayValueRenderer : c => {
                return c ? c.name : this.L('Default calendar');
            }
        };
    }

    get value() {
        return super.value;
    }

    set value(v) {
        if (v && v.isDefault && v.isDefault()) {
            v = null;
        }
        super.value = v;
    }
}

BryntumWidgetAdapterRegister.register(CalendarField.type, CalendarField);
