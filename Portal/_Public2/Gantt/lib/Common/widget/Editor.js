import Widget from './Widget.js';
import Container from './Container.js';
import IdHelper from '../helper/IdHelper.js';
import WidgetHelper from '../helper/WidgetHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import Rectangle from '../helper/util/Rectangle.js';
import EventHelper from '../helper/EventHelper.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import LocaleManager from '../../Common/localization/LocaleManager.js';
import './layout/Fit.js';
import StringHelper from '../helper/StringHelper.js';

/**
 * @module Common/widget/Editor
 */

/**
 * Displays an input field, optionally editing a field of a record at a particular position.
 *
 * Offers events to signal edit completion upon `ENTER` or focus loss (if configured to do so),
 * or edit cancellation on `ESC`, or focus loss if configured that way.
 * @extends Common/widget/Container
 *
 * @classType Editor
 */
export default class Editor extends Container {
    //region Config

    static get defaultConfig() {
        return {
            positioned : true,

            hidden : true,

            layout : 'fit',

            /**
             * A config object, or the `type` string of the input field which this editor will encapsulate.
             * @config {Object|String}
             * @default
             */
            inputField : 'textfield',

            /**
             * What action should be taken when focus moves out of the editor, either by `TAB` or clicking outside.
             * May be `'complete'` or `'cancel`'. Any other value results in no action being taken upon focus leaving the editor
             * leaving the application to listen for the {@link Common.widget.Widget#event-focusout focusout} event.
             * @config {String}
             * @default
             */
            blurAction : 'complete',

            /**
             * The name of the `key` which completes the edit.
             *
             * See https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key/Key_Values for key names.
             * @config {String}
             * @default
             */
            completeKey : 'Enter',

            /**
             * The name of the `key` which cancels the edit.
             *
             * See https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key/Key_Values for key names.
             * @config {String}
             * @default
             */
            cancelKey : 'Escape',

            /**
             * Configure as `true` to allow editing to complete when the field is invalid. Editing may always be _canceled_.
             * @config {String}
             * @default
             */
            allowInvalid : false
        };
    }

    //endregion

    //region Events

    /**
     * Fired before the editor is shown to start an edit operation. Returning `false` from a handler vetoes the edit operation.
     * @event beforestart
     * @property {Object} value - The value to be edited.
     * @preventable
     */
    /**
     * Fired when an edit operation has begun.
     * @event start
     * @property {Object} value - The starting value of the field.
     * @property {Common.widget.Editor} source - The Editor that triggered the event.
     */
    /**
     * Fired when an edit completion has been requested, either by `ENTER`, or focus loss (if configured to complete on blur).
     * The completion may be vetoed, in which case, focus is moved back into the editor.
     * @event beforecomplete
     * @property {Object} oldValue - The original value.
     * @property {Object} value - The new value.
     * @property {Common.widget.Editor} source - The Editor that triggered the event.
     * @preventable
     */
    /**
     * Edit has been completed, and any associated record or element has been updated.
     * @event complete
     * @property {Object} oldValue - The original value.
     * @property {Object} value - The new value.
     * @property {Common.widget.Editor} source - The Editor that triggered the event.
     */
    /**
     * Fired when cancellation has been requested, either by `ESC`, or focus loss (if configured to cancel on blur).
     * The cancellation may be vetoed, in which case, focus is moved back into the editor.
     * @event beforecancel
     * @property {Object} oldValue - The original value.
     * @property {Object} value - The new value.
     * @property {Common.widget.Editor} source - The Editor that triggered the event.
     * @preventable
     */
    /**
     * Edit has been canceled without updating the associated record or element.
     * @event cancel
     * @property {Object} oldValue - The original value.
     * @property {Object} value - The value of the field.
     * @property {Common.widget.Editor} source - The Editor that triggered the event.
     */
    /**
     * Fire to relay a `keypress` event from the field.
     * @event keypress
     * @property {Event} event - The key event.
     */

    //endregion

    afterConfigure() {
        const me = this;
        super.afterConfigure();

        if (me.completeKey || me.cancelKey) {
            EventHelper.on({
                element : me.element,
                keydown : 'onKeyDown',
                thisObj : me
            });
        }

        LocaleManager.on({
            locale  : 'onLocaleChange',
            thisObj : me
        });
    }

    onLocaleChange() {
        const me = this;
        if (me.inputField) {
            me.inputField.syncInputFieldValue();
        }
    }

    /**
     * Start editing
     * @param {Object} editObject An object containing details about what to edit.
     * @param {HTMLElement/Common.helper.util.Rectangle} editObject.target the element or Rectangle to align to.
     * @param {String} [editObject.align=t0-t0] How to align to the target.
     * @param {Boolean} [editObject.matchSize=true] Match editor size to target size.
     * @param {Common.data.Model} [editObject.record] The record to edit.
     * @param {String} [editObject.field] The field name in the record to edit. This defaults to the `name` of the {@link #config-inputField}.
     * Also if record has method set + capitalized field, method will be called, e.g. if record has method named
     * `setFoobar` and this config is `foobar`, then instead of `record.foobar = value`, `record.setFoobar(value)` will be called.
     * @param {Object} [editObject.value] The value to edit.
     * @param {Boolean} [editObject.focus=true] Focus the field.
     */
    startEdit({ target, align = 't0-t0', matchSize = true, value, record, field = this.inputField.name, focus = true }) {
        const me = this,
            { inputField } = me,
            targetRect = (target instanceof Rectangle) ? target : Rectangle.inner(target);

        if (me.trigger('beforestart', { value }) !== false) {
            if (record && field && value === undefined) {
                me.record = record;
                me.dataField = field;
                value = record[field];
            }
            if (matchSize) {
                me.width = targetRect.width;
                me.height = targetRect.height;
            }
            inputField.value = value;
            me.showBy({
                target,
                align
            });
            if (focus && me.inputField.focus) {
                me.inputField.focus();
            }
            if (target.nodeType === 1) {
                target.classList.add('b-editing');
            }
            // Passed value may have been '10/06/2019', send the live field value to startedit
            me.trigger('start', { value : inputField.value });
            me.oldValue = inputField.value;

            // If the value from th value getter is an array, we must clone it because
            // if it's the same *instance*, the ObjectHelper.isEqual test in completeEdit
            // will find that there are no changes.
            if (Array.isArray(me.oldValue)) {
                me.oldValue = me.oldValue.slice();
            }
            return true;
        }
        return false;
    }

    refreshEdit() {
        if (this.isVisible) {
            const { record, dataField, inputField } = this;

            if (record && dataField) {
                const value = record[dataField];

                // Only update the field if the value has changed
                if (!ObjectHelper.isEqual(inputField.value, value)) {
                    inputField.value = value;
                }
            }
        }
    }

    onKeyDown(event) {
        const me = this;

        switch (event.key) {
            case me.completeKey:
                me.completeEdit();
                event.stopImmediatePropagation();
                break;
            case me.cancelKey:
                me.cancelEdit();
                event.stopImmediatePropagation();
                break;
        }
        me.trigger('keydown', { event });
    }

    onFocusOut(event) {
        super.onFocusOut(event);

        if (!this.isFinishing) {
            const method = this[`${this.blurAction}Edit`];

            if (method) {
                method.call(this);
            }
        }
    }

    /**
     * Complete the edit, and, if associated with a record, update the record if possible.
     * If editing is completed, the editor is hidden.
     *
     * If the field is invalid, and this Editor is configured `{@link #config-allowInvalid}: false`
     * then editing is not completed.
     *
     * If a {@link #event-beforecomplete} handler returns `false` then editing is not completed.
     *
     * If the field's valus has not been changed, then editing is terminated through {@link #function-cancelEdit}.
     *
     * @returns `true` if editing ceased, `false` if the editor is still active.
     */
    completeEdit() {
        const me = this,
            { inputField, oldValue } = me,
            { value } = inputField;

        // If we're configured not to allow invalid values, refocus the field in case complete was triggered by focusout.
        if (!inputField.isValid && !(me.allowInvalid || inputField.allowInvalid)) {
            inputField.focus && inputField.focus();
            return false;
        }
        // No change means a cancel.
        else if (ObjectHelper.isEqual(value, oldValue)) {
            me.cancelEdit();
            return true;
        }
        // Allow veto of the completion
        else {
            const context = { value : value, oldValue };

            if (me.trigger('beforecomplete', context) === false) {
                inputField.focus && inputField.focus();
            }
            // EXPERIMENTAL: Allow async finalization of the editing, implementer may want to show a confirmation popup etc
            else if (context.async) {
                context.async.then((result) => {
                    if (result === true) {
                        me.onEditComplete();
                    } else {
                        inputField.setError(result || inputField.L('invalidValue'));
                        if (!(me.allowInvalid || inputField.allowInvalid)) {
                            inputField.focus && inputField.focus();
                        }
                    }
                });
                return false;
            }
            // Successful completion
            else {
                me.onEditComplete();
                return true;
            }
        }
        return false;
    }

    /**
     * Cancel the edit and hide the editor.
     */
    cancelEdit() {
        const me = this,
            { inputField, oldValue } = me,
            { value } = inputField;

        if (!me.isFinishing && me.trigger('beforecancel', { value : value, oldValue }) !== false) {
            // Hiding must not trigger our blurAction
            me.isFinishing = true;
            me.hide();
            me.trigger('cancel', { value, oldValue });
            me.isFinishing = false;
        }
    }

    // Handle updating what needs to be updated.
    onEditComplete() {
        const me = this,
            { record, dataField, inputField, oldValue, lastAlignSpec } = me,
            { target } = lastAlignSpec,
            { value } = inputField;

        if (!me.isFinishing) {
            // Hiding must not trigger our blurAction
            me.isFinishing = true;
            me.hide();

            if (record) {
                const setterName = `set${StringHelper.capitalizeFirstLetter(dataField)}`;
                if (record[setterName]) {
                    record[setterName](value);
                }
                else {
                    record[dataField] = value;
                }
            }
            me.trigger('complete', { value, oldValue });
            if (target.nodeType === 1) {
                target.classList.remove('b-editing');
            }

            me.isFinishing = false;
        }
    }

    doDestroy() {
        if (this.createdInputField) {
            this.inputField.destroy();
        }
        super.doDestroy();
    }

    set owner(owner) {
        this._owner = owner;
    }

    // This is a positioned widget appended to a Widget's contentElement. It has no owner link.
    // Grab the owner by finding what widget it is inside.
    get owner() {
        return this._owner || IdHelper.fromElement(this.element.parentNode);
    }

    get items() {
        return this._items = [this.inputField];
    }

    set inputField(inputField) {
        const me = this;

        if (me._inputField) {
            me._inputField.destroy();
        }
        if (typeof inputField === 'string') {
            inputField = {
                type : inputField
            };
        }
        if (inputField instanceof Widget) {
            me._inputField = inputField;
        }
        else {
            me._inputField = WidgetHelper.createWidget(inputField);
            me.createdInputField = true; // So we know we can destroy it
        }
        me._inputField.parent = me;
    }

    get inputField() {
        return this._inputField;
    }

}

BryntumWidgetAdapterRegister.register('editor', Editor);
