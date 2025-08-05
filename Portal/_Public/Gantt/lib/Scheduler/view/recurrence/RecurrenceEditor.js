import Popup from '../../../Core/widget/Popup.js';
import './RecurrenceEditorPanel.js';

/**
 * @module Scheduler/view/recurrence/RecurrenceEditor
 */

/**
 * Class implementing a dialog to edit a {@link Scheduler.model.RecurrenceModel recurrence model}. The class is used by
 * the {@link Scheduler.view.mixin.RecurringEvents recurring events} feature, and you normally don't need to instantiate
 * it.
 *
 * Before showing the dialog need to use {@link Core.widget.Container#property-record} to load a
 * {@link Scheduler.model.RecurrenceModel recurrence model} data into the editor fields. For example:
 *
 * ```javascript
 * // make the editor instance
 * const editor = new RecurrenceEditor();
 * // load recurrence model into it
 * editor.record = new RecurrenceModel({ frequency : "WEEKLY" });
 * // display the editor
 * editor.show();
 * ```
 *
 * @extends Core/widget/Popup
 * @classType recurrenceeditor
 */
export default class RecurrenceEditor extends Popup {



    static get $name() {
        return 'RecurrenceEditor';
    }

    // Factoryable type name
    static get type() {
        return 'recurrenceeditor';
    }

    static get configurable() {
        return {
            draggable : true,
            closable  : true,
            floating  : true,
            cls       : 'b-recurrenceeditor',
            title     : 'L{Repeat event}',
            autoClose : true,
            width     : 470,
            items     : {
                recurrenceEditorPanel : {
                    type  : 'recurrenceeditorpanel',
                    title : null
                }
            },
            bbar : {
                defaults : {
                    localeClass : this
                },
                items : {
                    foo : {
                        type   : 'widget',
                        cls    : 'b-label-filler',
                        weight : 100
                    },
                    saveButton : {
                        color   : 'b-green',
                        text    : 'L{Save}',
                        onClick : 'up.onSaveClick',
                        weight  : 200
                    },
                    cancelButton : {
                        color   : 'b-gray',
                        text    : 'L{Object.Cancel}',
                        onClick : 'up.onCancelClick',
                        weight  : 300
                    }
                }
            },
            scrollable : {
                overflowY : true
            }
        };
    }

    updateReadOnly(readOnly) {
        super.updateReadOnly(readOnly);

        // No save or cancel buttons. It's purely for information display when in readOnly mode
        this.bbar.hidden = readOnly;
    }

    get recurrenceEditorPanel() {
        return this.widgetMap.recurrenceEditorPanel;
    }

    updateRecord(record) {
        this.recurrenceEditorPanel.record = record;
    }

    onSaveClick() {
        const me = this;

        if (me.saveHandler) {
            me.saveHandler.call(me.thisObj || me, me, me.record);
        }
        else {
            me.recurrenceEditorPanel.syncEventRecord();
            me.close();
        }
    }

    onCancelClick() {
        const me = this;

        if (me.cancelHandler) {
            me.cancelHandler.call(me.thisObj || me, me, me.record);
        }
        else {
            me.close();
        }
    }

}

// Register this widget type with its Factory
RecurrenceEditor.initClass();
