import EditorTab from './EditorTab.js';
import '../../../Scheduler/view/recurrence/RecurrenceEditorPanel.js';

/**
 * @module SchedulerPro/widget/taskeditor/RecurrenceTab
 */

/**
 * Tab for editing an events recurrence rule, only shown when Scheduler Pro is configured to use recurring events
 * (see {@link Scheduler/view/mixin/RecurringEvents#config-enableRecurringEvents}).
 *
 * Does not currently offer any customization options.
 *
 * @extends SchedulerPro/widget/taskeditor/EditorTab
 * @classtype recurrencetab
 */
export default class RecurrenceTab extends EditorTab {

    static $name = 'RecurrenceTab';

    static type = 'recurrencetab';

    static configurable = {
        title      : 'L{title}',
        cls        : 'b-recurrence-tab',
        scrollable : true,
        items      : {
            recurrenceEditor : {
                type    : 'recurrenceeditorpanel',
                addNone : true
            }
        }

    };

    get recurrenceEditor() {
        return this.widgetMap.recurrenceEditor;
    }

    loadEvent(eventRecord) {
        if (this.recurrenceEditor) {
            this.loadingRecord = true;
            this.recurrenceEditor.record = eventRecord.recurrence ?? null;
            this.loadingRecord = false;
        }

        super.loadEvent(eventRecord);
    }

    onFieldChange({ source, value }) {
        const me = this;

        if (!me.isConfiguring && !me.recurrenceEditor.assigningValues && !me.loadingRecord) {
            const { record } = me;

            // Create a new recurrence if event wasn't recurring already
            if (!record.recurrence) {
                record.recurrence = new record.recurrenceModel({});
            }
            else {
                // Clear recurrence if picking 'NONE'
                if (source === me.recurrenceEditor.widgetMap.frequencyField && value === 'NONE') {
                    record.recurrence = null;
                }
                else {
                    me.recurrenceEditor.syncEventRecord(record.recurrence);
                }
            }

            me.up(w => w.isTaskEditorBase).editingRecurring = true;
        }
    }
}

// Register this widget type with its Factory
RecurrenceTab.initClass();
