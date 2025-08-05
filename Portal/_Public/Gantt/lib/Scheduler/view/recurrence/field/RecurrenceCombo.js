import RecurrenceFrequencyCombo from './RecurrenceFrequencyCombo.js';

/**
 * @module Scheduler/view/recurrence/field/RecurrenceCombo
 */

/**
 * A combobox field for selecting a recurrence pattern: `Daily`, `Weekly`, `Monthly` or `Yearly` if the recurrence
 * has no other non-default settings, or `Custom...` if the recurrence has custom setting applied.
 *
 * {@inlineexample Scheduler/view/RecurrenceCombo.js}
 *
 * @extends Scheduler/view/recurrence/field/RecurrenceFrequencyCombo
 * @classType recurrencecombo
 */
export default class RecurrenceCombo extends RecurrenceFrequencyCombo {

    static get $name() {
        return 'RecurrenceCombo';
    }

    // Factoryable type name
    static get type() {
        return 'recurrencecombo';
    }

    static get defaultConfig() {
        return {
            customValue             : 'custom',
            placeholder             : 'None',

            splitCls                : 'b-recurrencecombo-split',
            items                   : true,
            highlightExternalChange : false
        };
    }

    buildItems() {
        const me = this;

        return [
            { value : 'none', text : 'L{None}' },
            ...super.buildItems(),
            { value : me.customValue, text : 'L{Custom}', cls : me.splitCls }
        ];
    }

    set value(value) {
        // Use 'none' instead of falsy value
        value = value || 'none';

        super.value = value;
    }

    get value() {
        return super.value;
    }

    set recurrence(recurrence) {
        const me = this;

        if (recurrence) {
            me.value = me.isCustomRecurrence(recurrence) ? me.customValue : recurrence.frequency;
        }
        else {
            me.value = null;
        }
    }

    isCustomRecurrence(recurrence) {
        const { interval, days, monthDays, months } = recurrence;

        return Boolean(interval > 1 || (days && days.length) || (monthDays && monthDays.length) || (months && months.length));
    }
};

// Register this widget type with its Factory
RecurrenceCombo.initClass();
