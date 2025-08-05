import Popup from '../../../Core/widget/Popup.js';

/**
 * @module Scheduler/view/recurrence/RecurrenceConfirmationPopup
 */

/**
 * A confirmation dialog shown when modifying a recurring event or some of its occurrences.
 * For recurring events, the dialog informs the user that the change will be applied to all occurrences.
 *
 * For occurrences, the dialog lets the user choose if the change should affect all future occurrences,
 * or this occurrence only.
 *
 * Usage example:
 *
 * ```javascript
 * const confirmation = new RecurrenceConfirmationPopup();
 *
 * confirmation.confirm({
 *     eventRecord : recurringEvent,
 *     actionType  : "delete",
 *     changerFn   : () => recurringEvent.remove(event)
 * });
 * ```
 *
 * @classType recurrenceconfirmation
 * @extends Core/widget/Popup
 */
export default class RecurrenceConfirmationPopup extends Popup {



    static get $name() {
        return 'RecurrenceConfirmationPopup';
    }

    // Factoryable type name
    static get type() {
        return 'recurrenceconfirmation';
    }

    static get defaultConfig() {
        return {
            localizableProperties : [],
            align                 : 'b-t',
            autoShow              : false,
            autoClose             : false,
            closeAction           : 'onRecurrenceClose',
            modal                 : true,
            centered              : true,
            scrollAction          : 'realign',
            constrainTo           : globalThis,
            draggable             : true,
            closable              : true,
            floating              : true,
            eventRecord           : null,
            cls                   : 'b-sch-recurrenceconfirmation',
            bbar                  : {
                defaults : {
                    localeClass : this
                },
                items : {
                    changeSingleButton : {
                        weight  : 100,
                        cls     : 'b-raised',
                        color   : 'b-blue',
                        text    : 'L{update-only-this-btn-text}',
                        onClick : 'up.onChangeSingleButtonClick'
                    },
                    changeMultipleButton : {
                        weight  : 200,
                        color   : 'b-green',
                        text    : 'L{Object.Yes}',
                        onClick : 'up.onChangeMultipleButtonClick'
                    },
                    cancelButton : {
                        weight  : 300,
                        color   : 'b-gray',
                        text    : 'L{Object.Cancel}',
                        onClick : 'up.onCancelButtonClick'
                    }
                }
            }
        };
    }

    /**
     * Reference to the "Apply changes to multiple occurrences" button, if used
     * @property {Core.widget.Button}
     * @readonly
     */
    get changeMultipleButton() {
        return this.widgetMap.changeMultipleButton;
    }

    /**
     * Reference to the button that causes changing of the event itself only, if used
     * @property {Core.widget.Button}
     * @readonly
     */
    get changeSingleButton() {
        return this.widgetMap.changeSingleButton;
    }

    /**
     * Reference to the cancel button, if used
     * @property {Core.widget.Button}
     * @readonly
     */
    get cancelButton() {
        return this.widgetMap.cancelButton;
    }

    /**
     * Handler for "Apply changes to multiple occurrences" {@link #property-changeMultipleButton button}.
     * It calls {@link #function-processMultipleRecords} and then hides the dialog.
     */
    onChangeMultipleButtonClick() {
        this.processMultipleRecords();
        this.hide();
    }

    /**
     * Handler for the {@link #property-changeSingleButton button} that causes changing of the event itself only.
     * It calls {@link #function-processSingleRecord} and then hides the dialog.
     */
    onChangeSingleButtonClick() {
        this.processSingleRecord();
        this.hide();
    }

    /**
     * Handler for {@link #property-cancelButton cancel button}.
     * It calls `cancelFn` provided to {@link #function-confirm} call and then hides the dialog.
     */
    onCancelButtonClick() {
        this.cancelFn && this.cancelFn.call(this.thisObj);
        this.hide();
    }

    onRecurrenceClose() {
        if (this.cancelFn) {
            this.cancelFn.call(this.thisObj);
        }
        this.hide();
    }

    /**
     * Displays the confirmation.
     * Usage example:
     *
     * ```javascript
     * const popup = new RecurrenceConfirmationPopup();
     *
     * popup.confirm({
     *     eventRecord,
     *     actionType : "delete",
     *     changerFn  : () => eventStore.remove(record)
     * });
     * ```
     *
     * @param {Object} config The following config options are supported:
     * @param {Scheduler.model.EventModel} config.eventRecord   Event being modified.
     * @param {'update'|'delete'} config.actionType Type of modification to be applied to the event. Can be
     * either "update" or "delete".
     * @param {Function} config.changerFn A function that should be called to apply the change to the event upon user
     * choice.
     * @param {Function} [config.thisObj] `changerFn` and `cancelFn` functions scope.
     * @param {Function} [config.cancelFn] Function called on `Cancel` button click.
     */
    confirm(config = {}) {
        const me = this;



        [
            'actionType',
            'eventRecord',
            'title',
            'html',
            'changerFn',
            'cancelFn',
            'finalizerFn',
            'thisObj'
        ].forEach(prop => {
            if (prop in config) me[prop] = config[prop];
        });

        me.updatePopupContent();

        return super.show(config);
    }

    updatePopupContent() {
        const
            me                                                         = this,
            { changeMultipleButton, changeSingleButton, cancelButton } = me.widgetMap,
            { eventRecord, actionType = 'update' }                     = me,
            isMaster                                                   = eventRecord?.isRecurring;

        if (isMaster) {
            changeMultipleButton.text = me.L('L{Object.Yes}');
            me.html = me.L(`${actionType}-all-message`);
        }
        else {
            changeMultipleButton.text = me.L(`${actionType}-further-btn-text`);
            me.html = me.L(`${actionType}-further-message`);
        }

        changeSingleButton.text = me.L(`${actionType}-only-this-btn-text`);
        cancelButton.text = me.L('L{Object.Cancel}');

        me.width = me.L('L{width}');

        me.title = me.L(`${actionType}-title`);
    }

    /**
     * Applies changes to multiple occurrences as reaction on "Apply changes to multiple occurrences"
     * {@link #property-changeMultipleButton button} click.
     */
    processMultipleRecords() {
        const { eventRecord, changerFn, thisObj, finalizerFn } = this;

        eventRecord.beginBatch();
        // Apply changes to the occurrence.
        // It is not joined to any stores, so this has no consequence.
        changerFn && this.callback(changerFn, thisObj, [eventRecord]);

        // afterChange will promote it to being an new recurring base because there's still recurrence
        eventRecord.endBatch();

        finalizerFn && this.callback(finalizerFn, thisObj, [eventRecord]);
    }

    /**
     * Applies changes to a single record by making it a "real" event and adding an exception to the recurrence.
     * The method is called as reaction on clicking the {@link #property-changeSingleButton button} that causes changing of the event itself only.
     */
    processSingleRecord() {
        const { eventRecord, changerFn, thisObj, finalizerFn } = this;

        eventRecord.beginBatch();

        let firstOccurrence;

        // If that's a master event get its very first occurrence
        if (eventRecord?.isRecurring) {
            eventRecord.recurrence.forEachOccurrence(eventRecord.startDate, null, (occurrence, isFirst, index) => {
                // index 1 is used by to the event itself, > 1 since there might be exceptions
                if (index > 1) {
                    firstOccurrence = occurrence;
                    return false;
                }
            });
        }

        // turn the 1st occurrence into a new "master" event
        firstOccurrence?.convertToRealEvent();

        // When the changes apply, because there's no recurrence, it will become an exception
        eventRecord.recurrence = null;

        // Apply changes to the occurrence.
        // It is not joined to any stores, so this has no consequence.
        changerFn && this.callback(changerFn, thisObj, [eventRecord]);

        // Must also change after the callback in case the callback sets the rule.
        // This will update the batch update data block to prevent it being set back to recurring.
        eventRecord.recurrenceRule = null;

        // afterChange will promote it to being an exception because there's no recurrence
        eventRecord.endBatch();

        finalizerFn && this.callback(finalizerFn, thisObj, [eventRecord]);
    }

    updateLocalization() {
        this.updatePopupContent();
        super.updateLocalization();
    }

};

// Register this widget type with its Factory
RecurrenceConfirmationPopup.initClass();
