import DateHelper from '../../../../Core/helper/DateHelper.js';
import Combo from '../../../../Core/widget/Combo.js';
import RecurrenceDayRuleEncoder from '../../../data/util/recurrence/RecurrenceDayRuleEncoder.js';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceDaysCombo
 */

/**
 * A combobox field allowing to pick days for the `Monthly` and `Yearly` mode in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencedayscombo
 */
export default class RecurrenceDaysCombo extends Combo {

    static get $name() {
        return 'RecurrenceDaysCombo';
    }

    // Factoryable type name
    static get type() {
        return 'recurrencedayscombo';
    }

    static get defaultConfig() {
        const
            allDaysValueAsArray = ['SU', 'MO', 'TU', 'WE', 'TH', 'FR', 'SA'],
            allDaysValue        = allDaysValueAsArray.join(',');

        return {
            allDaysValue,
            editable            : false,
            defaultValue        : allDaysValue,
            workingDaysValue    : allDaysValueAsArray.filter((day, index) => !DateHelper.nonWorkingDays[index]).join(','),
            nonWorkingDaysValue : allDaysValueAsArray.filter((day, index) => DateHelper.nonWorkingDays[index]).join(','),
            splitCls            : 'b-recurrencedays-split',
            displayField        : 'text',
            valueField          : 'value'
        };
    }

    buildItems() {
        const me = this;

        me._weekDays = null;

        return me.weekDays.concat([
            { value : me.allDaysValue,        text : me.L('L{day}'), cls : me.splitCls },
            { value : me.workingDaysValue,    text : me.L('L{weekday}') },
            { value : me.nonWorkingDaysValue, text : me.L('L{weekend day}') }
        ]);
    }

    get weekDays() {
        const me = this;

        if (!me._weekDays) {
            const weekStartDay = DateHelper.weekStartDay;

            const dayNames = DateHelper.getDayNames().map((text, index) => ({ text, value : RecurrenceDayRuleEncoder.encodeDay(index) }));

            // we should start week w/ weekStartDay
            me._weekDays = dayNames.slice(weekStartDay).concat(dayNames.slice(0, weekStartDay));
        }

        return me._weekDays;
    }

    set value(value) {
        const me = this;

        if (value && Array.isArray(value)) {
            value = value.join(',');
        }

        // if the value has no matching option in the store we need to use default value
        if (!value || !me.store.findRecord('value', value)) {
            value = me.defaultValue;
        }

        super.value = value;
    }

    get value() {
        let value = super.value;

        if (value && Array.isArray(value)) {
            value = value.join(',');
        }

        return value;
    }
}

// Register this widget type with its Factory
RecurrenceDaysCombo.initClass();
