import RecurrenceDayRuleEncoder from '../../data/util/recurrence/RecurrenceDayRuleEncoder.js';
import Panel from '../../../Core/widget/Panel.js';
import '../../../Core/widget/Widget.js';
import '../../../Core/widget/Button.js';
import '../../../Core/widget/Checkbox.js';
import '../../../Core/widget/SlideToggle.js';
import '../../../Core/widget/DateField.js';
import '../../../Core/widget/NumberField.js';
import './field/RecurrenceFrequencyCombo.js';
import './field/RecurrenceDaysCombo.js';
import './field/RecurrenceDaysButtonGroup.js';
import './field/RecurrenceMonthDaysButtonGroup.js';
import './field/RecurrenceMonthsButtonGroup.js';
import './field/RecurrenceStopConditionCombo.js';
import './field/RecurrencePositionsCombo.js';
import BrowserHelper from '../../../Core/helper/BrowserHelper.js';

/**
 * @module Scheduler/view/recurrence/RecurrenceEditorPanel
 */

/**
 * Panel containing fields used to edit a {@link Scheduler.model.RecurrenceModel recurrence model}. Used by
 * {@link Scheduler/view/recurrence/RecurrenceEditor}, and by the recurrence tab in Scheduler Pro's event editor.
 *
 * Not intended to be used separately.
 *
 * @extends Core/widget/Panel
 * @classType recurrenceeditorpanel
 * @private
 */
export default class RecurrenceEditorPanel extends Panel {

    static $name = 'RecurrenceEditorPanel';

    static type = 'recurrenceeditorpanel';

    static configurable = {
        cls     : 'b-recurrenceeditor',
        record  : false,
        addNone : false,
        items   : {
            frequencyField : {
                type     : 'recurrencefrequencycombo',
                name     : 'frequency',
                label    : 'L{RecurrenceEditor.Frequency}',
                weight   : 10,
                onChange : 'up.onFrequencyFieldChange',
                addNone  : 'up.addNone'
            },
            intervalField : {
                type     : 'numberfield',
                weight   : 15,
                name     : 'interval',
                label    : 'L{RecurrenceEditor.Every}',
                min      : 1,
                required : true
            },
            daysButtonField : {
                type         : 'recurrencedaysbuttongroup',
                weight       : 20,
                name         : 'days',
                forFrequency : 'WEEKLY'
            },
            // the radio button enabling "monthDaysButtonField" in MONTHLY mode
            monthDaysRadioField : {
                type         : 'checkbox',
                weight       : 30,
                toggleGroup  : 'radio',
                forFrequency : 'MONTHLY',
                label        : 'L{RecurrenceEditor.Each}',
                checked      : true,
                onChange     : 'up.onMonthDaysRadioFieldChange'
            },
            monthDaysButtonField : {
                type         : 'recurrencemonthdaysbuttongroup',
                weight       : 40,
                name         : 'monthDays',
                forFrequency : 'MONTHLY'
            },
            monthsButtonField : {
                type         : 'recurrencemonthsbuttongroup',
                weight       : 50,
                name         : 'months',
                forFrequency : 'YEARLY'
            },
            // the radio button enabling positions & days combos in MONTHLY & YEARLY modes
            positionAndDayRadioField : {
                type         : 'checkbox',
                weight       : 60,
                toggleGroup  : 'radio',
                forFrequency : 'MONTHLY|YEARLY',
                label        : 'L{RecurrenceEditor.On the}',
                onChange     : 'up.onPositionAndDayRadioFieldChange'
            },
            positionsCombo : {
                type         : 'recurrencepositionscombo',
                weight       : 80,
                name         : 'positions',
                forFrequency : 'MONTHLY|YEARLY'
            },
            daysCombo : {
                type         : 'recurrencedayscombo',
                weight       : 90,
                name         : 'days',
                forFrequency : 'MONTHLY|YEARLY',
                flex         : 1
            },
            stopRecurrenceField : {
                type     : 'recurrencestopconditioncombo',
                weight   : 100,
                label    : 'L{RecurrenceEditor.End repeat}',
                onChange : 'up.onStopRecurrenceFieldChange'
            },
            countField : {
                type     : 'numberfield',
                weight   : 110,
                name     : 'count',
                min      : 2,
                required : true,
                disabled : true,
                label    : ' '
            },
            endDateField : {
                type     : 'datefield',
                weight   : 120,
                name     : 'endDate',
                hidden   : true,
                disabled : true,
                label    : ' ',
                required : true
            }
        }
    };

    setupWidgetConfig(widgetConfig) {
        // All our inputs must be mutated using triggers and touch gestures on mobile
        if (BrowserHelper.isMobile && !('editable' in widgetConfig)) {
            widgetConfig.editable = false;
        }
        return super.setupWidgetConfig(...arguments);
    }

    updateRecord(record) {
        super.updateRecord(record);

        const
            me = this,
            {
                frequencyField,
                daysButtonField,
                monthDaysButtonField,
                monthsButtonField,
                monthDaysRadioField,
                positionAndDayRadioField,
                stopRecurrenceField
            }  = me.widgetMap;

        if (record) {
            const
                event     = record.timeSpan,
                startDate = event?.startDate;

            // some fields default values are calculated based on event "startDate" value
            if (startDate) {
                // if no "days" value provided
                if (!record.days || !record.days.length) {
                    daysButtonField.value = [RecurrenceDayRuleEncoder.encodeDay(startDate.getDay())];
                }

                // if no "monthDays" value provided
                if (!record.monthDays || !record.monthDays.length) {
                    monthDaysButtonField.value = startDate.getDate();
                }

                // if no "months" value provided
                if (!record.months || !record.months.length) {
                    monthsButtonField.value = startDate.getMonth() + 1;
                }
            }

            // if the record has both "days" & "positions" fields set check "On the" checkbox
            if (record.days && record.positions) {
                positionAndDayRadioField.check();

                if (!me.isPainted) {
                    monthDaysRadioField.uncheck();
                }
            }
            else {
                monthDaysRadioField.check();

                if (!me.isPainted) {
                    positionAndDayRadioField.uncheck();
                }
            }

            stopRecurrenceField.recurrence = record;
        }
        else {
            frequencyField.value = 'NONE';
        }
    }

    /**
     * Updates the provided recurrence model with the contained form data.
     * If recurrence model is not provided updates the last loaded recurrence model.
     * @internal
     */
    syncEventRecord(recurrence) {
        // get values relevant to the RecurrenceModel (from enabled fields only)
        const values = this.getValues((w) => w.name in recurrence && !w.disabled);

        // Disabled field does not contribute to values, clear manually
        if (!('endDate' in values)) {
            values.endDate = null;
        }
        if (!('count' in values)) {
            values.count = null;
        }

        recurrence.set(values);
    }

    toggleStopFields() {
        const
            me                           = this,
            { countField, endDateField } = me.widgetMap;

        switch (me.widgetMap.stopRecurrenceField.value) {

            case 'count' :
                countField.show();
                countField.enable();
                endDateField.hide();
                endDateField.disable();
                break;

            case 'date' :
                countField.hide();
                countField.disable();
                endDateField.show();
                endDateField.enable();
                break;

            default :
                countField.hide();
                endDateField.hide();
                countField.disable();
                endDateField.disable();
        }
    }

    onMonthDaysRadioFieldChange({ checked }) {
        const { monthDaysButtonField } = this.widgetMap;

        monthDaysButtonField.disabled = !checked || !this.isWidgetAvailableForFrequency(monthDaysButtonField);
    }

    onPositionAndDayRadioFieldChange({ checked }) {
        const { daysCombo, positionsCombo } = this.widgetMap;

        // toggle day & positions combos
        daysCombo.disabled = positionsCombo.disabled = !checked || !this.isWidgetAvailableForFrequency(daysCombo);
    }

    onStopRecurrenceFieldChange() {
        this.toggleStopFields();
    }

    isWidgetAvailableForFrequency(widget, frequency = this.widgetMap.frequencyField.value) {
        return !widget.forFrequency || widget.forFrequency.includes(frequency);
    }

    onFrequencyFieldChange({ value, oldValue, valid }) {
        const
            me    = this,
            items = me.queryAll(w => 'forFrequency' in w),
            {
                intervalField,
                stopRecurrenceField
            }     = me.widgetMap;

        if (valid && value) {
            for (let i = 0; i < items.length; i++) {
                const item = items[i];

                if (me.isWidgetAvailableForFrequency(item, value)) {
                    item.show();
                    item.enable();
                }
                else {
                    item.hide();
                    item.disable();
                }
            }

            // Special handling of NONE
            intervalField.hidden = stopRecurrenceField.hidden = value === 'NONE';

            if (value !== 'NONE') {
                intervalField.hint = me.L(`L{RecurrenceEditor.${value}intervalUnit}`);
            }

            // When a non-recurring record is loaded, intervalField is set to empty. We want it to default to 1 here
            // to not look weird (defaults to 1 on the data layer)
            if (oldValue === 'NONE' && intervalField.value == null) {
                intervalField.value = 1;
            }

            me.toggleFieldsState();
        }
    }

    toggleFieldsState() {
        const
            me            = this,
            { widgetMap } = me;

        me.onMonthDaysRadioFieldChange({ checked : widgetMap.monthDaysRadioField.checked });
        me.onPositionAndDayRadioFieldChange({ checked : widgetMap.positionAndDayRadioField.checked });
        me.onStopRecurrenceFieldChange();
    }

    updateLocalization() {
        // do extra labels translation (not auto-translated yet)
        const { countField, intervalField, frequencyField } = this.widgetMap;

        countField.hint = this.L('L{RecurrenceEditor.time(s)}');

        if (frequencyField.value && frequencyField.value !== 'NONE') {
            intervalField.hint = this.L(`L{RecurrenceEditor.${frequencyField.value}intervalUnit}`);
        }

        super.updateLocalization();
    }

}

// Register this widget type with its Factory
RecurrenceEditorPanel.initClass();
