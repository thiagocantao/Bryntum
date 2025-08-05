import Widget from '../../../Core/widget/Widget.js';
import Base from '../../../Core/Base.js';
import '../../view/recurrence/RecurrenceConfirmationPopup.js';
import '../../view/recurrence/field/RecurrenceCombo.js';
import '../../view/recurrence/RecurrenceLegendButton.js';
import '../../view/recurrence/RecurrenceEditor.js';

/**
 * @module Scheduler/feature/mixin/RecurringEventEdit
 */

/**
 * This mixin class provides recurring events functionality to the {@link Scheduler.feature.EventEdit event editor}.
 * @mixin
 */
export default Target => class RecurringEventEdit extends (Target || Base) {
    static get $name() {
        return 'RecurringEventEdit';
    }

    static get configurable() {
        return {
            recurringEventsItems : {
                /**
                 * Reference to the `Repeat` event field, if used
                 * @member {Scheduler.view.recurrence.field.RecurrenceCombo} recurrenceCombo
                 * @readonly
                 */
                recurrenceCombo : {
                    type   : 'recurrencecombo',
                    label  : 'L{EventEdit.Repeat}',
                    ref    : 'recurrenceCombo',
                    weight : 700
                },
                /**
                 * Reference to the button that opens the event repeat settings dialog, if used
                 * @member {Scheduler.view.recurrence.RecurrenceLegendButton} editRecurrenceButton
                 * @readonly
                 */
                editRecurrenceButton : {
                    type                 : 'recurrencelegendbutton',
                    ref                  : 'editRecurrenceButton',
                    name                 : 'recurrenceRule',
                    color                : 'b-gray',
                    menuIcon             : null,
                    flex                 : 1,
                    weight               : 800,
                    ignoreParentReadOnly : true
                }
            },

            /**
             * Set to `false` to hide recurring fields in event editor, even if the
             * {@link Scheduler.view.mixin.RecurringEvents#config-enableRecurringEvents Recurring Events} is `true`
             * and a recurring event is being edited.
             * @config {Boolean}
             * @category Recurring
             */
            showRecurringUI : null
        };
    }

    changeEditorConfig(editorConfig) {
        editorConfig.items = { ...editorConfig.items, ...this.recurringEventsItems };

        // EditBase inserts extraItems *after* all default items are in
        editorConfig = super.changeEditorConfig(editorConfig);

        return editorConfig;
    }

    doDestroy() {
        this._recurrenceConfirmation?.destroy();
        this._recurrenceEditor?.destroy();

        super.doDestroy();
    }

    onEditorConstructed(editor) {
        const me = this;

        editor.ion({
            hide    : me.onRecurringEventEditorHide,
            thisObj : me
        });

        if (me.editRecurrenceButton) {
            me.editRecurrenceButton.menu = me.recurrenceEditor;
        }

        me.recurrenceCombo?.ion({
            change  : me.onRecurrenceComboChange,
            thisObj : me
        });
    }

    updateReadOnly(readOnly) {
        if (this._recurrenceEditor) {
            this._recurrenceEditor.readOnly = readOnly;
        }
    }

    internalShowEditor() {
        this.toggleRecurringFieldsVisibility(this.client.enableRecurringEvents && this.showRecurringUI !== false);
    }

    toggleRecurringFieldsVisibility(show = true) {
        const methodName = show ? 'show' : 'hide';

        this.editRecurrenceButton?.[methodName]?.();
        this.recurrenceCombo?.[methodName]?.();
    }

    onRecurringEventEditorHide() {
        if (this.recurrenceEditor?.isVisible) {
            this.recurrenceEditor.hide();
        }

        if (this.recurrenceConfirmation?.isVisible) {
            this.recurrenceConfirmation.hide();
        }
    }

    // Builds RecurrenceModel to load into the recurrenceEditor
    // It builds the model based on either:
    // - recurrence rule string (if provided)
    // - or the event being edited recurrence (if the event is repeating)
    // - or simply make a recurrence model w/ default state (by default means: Frequency=Daily, Interval=1)
    makeRecurrence(rule) {
        const
            event     = this.eventRecord,
            eventCopy = event.copy();

        let recurrence = event.recurrence;

        if (!rule && recurrence) {
            recurrence = recurrence.copy();
        }
        else {
            recurrence = new event.recurrenceModel(rule ? { rule } : {});
        }

        // bind cloned recurrence to the cloned event
        recurrence.timeSpan = eventCopy;
        // update cloned event w/ start date from the UI field
        eventCopy.setStartDate(this.values.startDate);

        recurrence.suspendTimeSpanNotifying();

        return recurrence;
    }

    onRecurrableEventBeforeSave({ eventRecord, context }) {
        const me = this;

        // Other views features may trigger beforeEventSave, so only react when *we* are editing.
        if (me.isEditing && !eventRecord.isCreating && eventRecord.supportsRecurring &&
                (eventRecord.isRecurring || eventRecord.isOccurrence)) {
            me.recurrenceConfirmation.confirm({
                actionType : 'update',
                eventRecord,
                changerFn() {
                    context.finalize(true);
                },
                cancelFn() {
                    context.finalize(false);
                }
            });

            // signalizes that we plan to decide save or not asynchronously
            context.async = true;
        }
    }

    set recurrenceConfirmation(recurrenceConfirmation) {
        this._recurrenceConfirmation = recurrenceConfirmation;
    }

    get recurrenceConfirmation() {
        const me = this;

        let recurrenceConfirmation = me._recurrenceConfirmation;

        if (!recurrenceConfirmation || !recurrenceConfirmation.$$name) {
            recurrenceConfirmation = Widget.create({
                type  : 'recurrenceconfirmation',
                owner : me.editor,
                ...recurrenceConfirmation
            });

            me._recurrenceConfirmation = recurrenceConfirmation;
        }

        return recurrenceConfirmation;
    }

    set recurrenceEditor(recurrenceEditor) {
        this._recurrenceEditor = recurrenceEditor;
    }

    get recurrenceEditor() {
        const me = this;

        let recurrenceEditor = me._recurrenceEditor;

        // Recurrence editor is centered and modal.
        if (!recurrenceEditor || !recurrenceEditor.$$name) {
            me._recurrenceEditor = recurrenceEditor = Widget.create({
                type         : 'recurrenceeditor',
                autoShow     : false,
                centered     : true,
                modal        : true,
                // It's used as the Menu of a Button which syncs the width unless it's already set
                minWidth     : 'auto',
                constrainTo  : globalThis,
                anchor       : false,
                rootElement  : me.rootElement,
                saveHandler  : me.recurrenceEditorSaveHandler,
                onBeforeShow : me.onBeforeShowRecurrenceEditor.bind(me),
                thisObj      : me,
                ...recurrenceEditor
            });

            // Must set *after* construction, otherwise it becomes the default state
            // to reset readOnly back to.  Must use direct property access because
            // getter consults state of editor.
            recurrenceEditor.readOnly = me._readOnly;
        }

        return recurrenceEditor;
    }

    onBeforeShowRecurrenceEditor() {
        const
            me                                = this,
            { recurrenceEditor, eventRecord } = me;

        if (recurrenceEditor && eventRecord?.supportsRecurring) {
            // if the event has no recurrence yet ..initialize it before showing recurrence editor
            if (!me.recurrence) {
                me.recurrence = me.makeRecurrence();
            }

            // update the cloned recurrence w/ up to date start date value
            me.recurrence.timeSpan.setStartDate(me.values.startDate);

            // load RecurrenceModel record into the recurrence editor
            recurrenceEditor.record = me.recurrence;

            // In case they drag it. Centered falls off if the widget has position set.
            recurrenceEditor.centered = true;
        }
    }

    loadRecurrenceData(recurrence) {
        this.recurrence = recurrence;

        this.updateRecurrenceFields(recurrence);
    }

    updateRecurrenceFields(recurrence) {
        const
            me                       = this,
            { editRecurrenceButton } = me;

        if (me.recurrenceCombo) {
            me.recurrenceCombo.recurrence = recurrence;
        }

        // update the recurrence legend
        if (editRecurrenceButton) {
            editRecurrenceButton.recurrence = recurrence;
            editRecurrenceButton.value = recurrence ? recurrence.rule : null;

            if (recurrence && me.client.enableRecurringEvents && me.showRecurringUI !== false) {
                editRecurrenceButton.show();
            }
            else {
                editRecurrenceButton.hide();
            }
        }
    }

    onRecurrenceComboChange({ source, value, userAction }) {
        if (userAction) {
            const
                me                   = this,
                { recurrenceEditor } = me;

            if (value === source.customValue) {
                // if user picked "Custom" - show recurrence editor
                // This will recurse through the change event into the opposite side
                // of the value test which will call updateRecurrenceFields, where the
                // assignment to the value of the recurrenceCombo will be a non-change.
                // That will sync the state of the recurrenceButton.
                me.recurrenceCombo.recurrence = me.makeRecurrence();

                if (recurrenceEditor.centered) {
                    recurrenceEditor.show();
                }
                else {
                    recurrenceEditor.show((me.editRecurrenceButton || source).element);
                }
            }
            // user has picked some frequency -> make a new recurrence based on it
            else {
                me.loadRecurrenceData(value && value !== 'none' ? me.makeRecurrence(`FREQ=${value}`) : null);
            }
        }
    }

    recurrenceEditorSaveHandler(editor, recurrence) {
        // apply changes to the kept recurrence
        editor.recurrenceEditorPanel.syncEventRecord(recurrence);

        // update the recurrence related UI
        this.updateRecurrenceFields(recurrence);

        editor.close();
    }

    onDatesChange(...args) {
        super.onDatesChange(...args);

        if (!this.loadingRecord && this.editRecurrenceButton) {
            const { startDate } = this.values;

            if (startDate) {
                this.editRecurrenceButton.eventStartDate = startDate;
            }
        }
    }

    internalLoadRecord(eventRecord) {
        if (eventRecord?.supportsRecurring) {
            this.loadRecurrenceData(eventRecord.recurrence ? this.makeRecurrence() : null);
        }
    }

    updateRecord(record) {
        // Special handling for when setting recurrence to "None". Since button gets hidden its value is not picked up
        // by the normal flow.
        if (record.recurrenceRule && !this.recurrence) {
            record.recurrenceRule = null;
        }

        return super.updateRecord(record);
    }
};
