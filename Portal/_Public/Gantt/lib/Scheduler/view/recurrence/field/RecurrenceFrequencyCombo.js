import Combo from '../../../../Core/widget/Combo.js';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceFrequencyCombo
 */

/**
 * A combobox field allowing to pick frequency in the {@link Scheduler.view.recurrence.RecurrenceEditor recurrence dialog}.
 *
 * @extends Core/widget/Combo
 * @classType recurrencefrequencycombo
 */
export default class RecurrenceFrequencyCombo extends Combo {

    static $name = 'RecurrenceFrequencyCombo';

    // Factoryable type name
    static type = 'recurrencefrequencycombo';

    static configurable = {
        editable              : false,
        displayField          : 'text',
        valueField            : 'value',
        localizeDisplayFields : true,
        addNone               : false
    };

    buildItems() {
        return [
            ...(this.addNone ? [{ text : 'L{None}', value : 'NONE' }] : []),
            { value : 'DAILY',   text : 'L{Daily}' },
            { value : 'WEEKLY',  text : 'L{Weekly}' },
            { value : 'MONTHLY', text : 'L{Monthly}' },
            { value : 'YEARLY',  text : 'L{Yearly}' }
        ];
    }
};

// Register this widget type with its Factory
RecurrenceFrequencyCombo.initClass();
