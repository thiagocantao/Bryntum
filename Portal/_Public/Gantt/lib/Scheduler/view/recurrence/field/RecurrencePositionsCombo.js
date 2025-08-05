import ArrayHelper from '../../../../Core/helper/ArrayHelper.js';
import Combo from '../../../../Core/widget/Combo.js';

/**
 * @module Scheduler/view/recurrence/field/RecurrencePositionsCombo
 */

/**
 * A combobox field allowing to specify day positions in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence editor}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencepositionscombo
 */
export default class RecurrencePositionsCombo extends Combo {



    static get $name() {
        return 'RecurrencePositionsCombo';
    }

    // Factoryable type name
    static get type() {
        return 'recurrencepositionscombo';
    }

    static get defaultConfig() {
        return {
            editable     : false,
            splitCls     : 'b-sch-recurrencepositions-split',
            displayField : 'text',
            valueField   : 'value',
            defaultValue : 1,
            maxPosition  : 5
        };
    }

    buildItems() {
        return this.buildDayNumbers().concat([
            { value : '-1', text : this.L('L{position-1}'), cls : this.splitCls }
        ]);
    }

    buildDayNumbers() {
        return ArrayHelper.populate(this.maxPosition, i => (
            { value : i + 1, text : this.L(`position${i + 1}`) }
        ));
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
        const value = super.value;

        return value ? `${value}`.split(',').map(item => parseInt(item, 10)) : [];
    }

};

// Register this widget type with its Factory
RecurrencePositionsCombo.initClass();
