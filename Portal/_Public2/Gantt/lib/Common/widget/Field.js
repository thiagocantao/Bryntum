import Widget from './Widget.js';
import Tooltip from './Tooltip.js';
import Badge from './mixin/Badge.js';
import BrowserHelper from '../helper/BrowserHelper.js';
import EventHelper from '../helper/EventHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import IdHelper from '../helper/IdHelper.js';
import DomHelper from '../helper/DomHelper.js';
import WidgetHelper from '../helper/WidgetHelper.js';
import ClickRepeater from '../util/ClickRepeater.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Common/widget/Field
 */

const byWeight = (l, r) => (l.weight || 0) - (r.weight || 0),
    byWeightReverse = (l, r) => (r.weight || 0) - (l.weight || 0),
    arrayOption = { array : true };

/**
 * Base class for TextField and NumberField. Not to be used directly.
 * @extends Common/widget/Widget
 * @mixes Common/widget/mixin/Badge
 * @abstract
 */
export default class Field extends Badge(Widget) {

    //region Config

    static get defaultConfig() {
        return {
            /**
             * Text to display in empty field.
             * @config {String}
             */
            placeholder : '',

            /**
             * Default value
             * @config {String}
             */
            value : '',

            /**
             * Label, prepended to field
             * @config {String}
             */
            label : null,

            /**
             * Configure as `true` to indicate that a `null` field value is to be marked as invalid.
             * @config {Boolean}
             * @default false
             */
            required : null,

            /**
             * Show a trigger to clear field, and allow `ESC` key to clear field if this field is
             * not {@link #config-readOnly}. The trigger is available in the {@link #property-triggers} object
             * under the name `clear`.
             * @config {Boolean}
             * @default false
             */
            clearable : null,

            /**
             * The width to apply to the `<label>` element. If a number is specified, `px` will be used.
             * @config {String|Number}
             */
            labelWidth : null,

            /**
             * The width to apply to the `<input>` element. If a number is specified, `px` will be used.
             * @config {String|Number}
             */
            inputWidth : null,

            /**
             * The delay in milliseconds to wait after the last keystroke before triggering a change event.
             * Set to 0 to not trigger change events from keystrokes (listen for input event instead to have
             * immediate feedback, change will still be triggered on blur)
             * @config {Number}
             * @default
             */
            keyStrokeChangeDelay : 0,

            /**
             * Makes the field unmodifiable by user action. The input area is not editable.
             * @config {Boolean}
             */
            readOnly : null,

            /**
             * Set to false to prevent user from editing the field. For TextFields it is basically the same as setting
             * {@link #config-readOnly}, but for PickerFields there is a distinction where it allows you to pick a value but not to type
             * one in the field.
             * @config {Boolean}
             * @default true
             */
            editable : true,

            defaultAction : 'change',

            /**
             * The triggers to add either before or after the input field. Each property name is the reference by which an
             * instantiated Trigger Widget may be retrieved from the live `{@link #property-triggers}` property.
             * Each trigger may have the following properties:
             * * `cls` The CSS class to apply.
             * * `handler` A method in the field to call upon click
             * * `align` `'start'` or `'end'` which end of the field the trigger should go.
             * * `weight` (Optional) Heigher weighted triggers gravitate towards the input field.
             * @config {Object}
             */
            triggers : null,

            /**
             * Specify `false` to prevent field from being highlighted when on external value changes
             * @config {Boolean}
             */
            highlightExternalChange : true,

            localizableProperties : ['label', 'title'],

            autoSelect : false,

            /**
             * Sets the native `autocomplete` property of the underlying input element. For more information, please refer to
             * [MDN](https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes/autocomplete)
             * @config {String}
             * @default
             */
            autoComplete : 'off'
        };
    }

    doDestroy() {
        const errorTip = Field._errorTip;

        super.doDestroy();

        // The errorTip references this field, hide it when we die.
        if (errorTip && errorTip.field === this) {
            errorTip.hide();
        }
    }

    /**
     * A singleton error tooltip which activates on hover of invalid fields.
     * before show, it gets a reference to the field and interrogates its
     * active error list to display as the tip content.
     * @member {Common.widget.Tooltip}
     * @readonly
     */
    get errorTip() {
        return this.constructor.errorTip;
    }

    /**
     * A singleton error tooltip which activates on hover of invalid fields.
     * before show, it gets a reference to the field and interrogates its
     * active error list to display as the tip content.
     * @member {Common.widget.Tooltip}
     * @readonly
     * @static
     */
    static get errorTip() {
        return Field._errorTip || (Field._errorTip = new Tooltip({
            cls          : 'b-field-error-tip',
            forSelector  : '.b-field.b-invalid .b-field-inner',
            align        : 'l-r',
            scrollAction : 'realign',
            onBeforeShow() {
                const tip = this,
                    field = IdHelper.fromElement(tip.activeTarget);

                if (field) {
                    const errors = field.getErrors();

                    if (errors) {
                        tip.html = errors.join('<br>');
                        tip.field = field;
                        return true;
                    }
                }

                // Veto show
                return false;
            }
        }));
    }

    //endregion

    //region Event
    /**
     * User typed into the field
     * @event input
     * @param {Common.widget.Field} source - The field
     * @param {String} value - Value
     */

    /**
     * Fired when the field value changes
     * @event change
     * @param {String} value - Value
     * @param {String} oldValue - Previous value
     * @param {Boolean} userAction - Triggered by user taking an action (`true`) or by setting a value (`false`)
     * @param {Common.widget.Field} source - Field
     */

    /**
     * User performed default action (typed into the field)
     * @event click
     * @param {Common.widget.Field} field - Field
     * @param {String} value - Value
     */

    /**
     * User clicked fields clear icon
     * @event clear
     * @param {Common.widget.Field} field - Field
     */

    /*
     * User clicked on one of the field's {@link #property-triggers}
     * @event trigger
     * @param {Common.widget.Field.Trigger} trigger The trigger activated by click or touch tap.
     * @param {Common.widget.Field} trigger.field The trigger's owning Field.
     */

    //endregion

    //region Init

    construct(config) {
        const me = this;

        me.highlightChanged = me.createOnFrame('addUpdatedCls');
        super.construct(config);

        // Instantiate error tip
        // eslint-disable-next-line
        Field.errorTip;

        if (me.keyStrokeChangeDelay) {
            me.changeOnKeyStroke = me.buffer(me.internalOnChange, me.keyStrokeChangeDelay);
        }
    }

    onFocusOut(e) {
        super.onFocusOut(e);

        // Check field consistency on blur
        this.onEditComplete();
    }

    /**
     * Template function which may be implemented by subclasses to synchronize
     * input state and validity state upon completion of the edit.
     * @internal
     */
    onEditComplete() {

    }

    set element(element) {
        const me = this,
            value = me.initialConfig.value,
            innerElements = DomHelper.createElementFromTemplate(me.inputTemplate(me), arrayOption),
            children = [{
                className : 'b-field-inner',
                reference : 'inputWrap',
                children  : innerElements
            }],
            startTriggers = [],
            endTriggers = [];

        if (me.label) {
            children.unshift({
                tag       : 'label',
                reference : 'labelElement',
                htmlFor   : `${me.id}_input`,
                html      : me.label
            });
        }

        // Read the clearable config which will force evaluation of triggers
        // to include clearable trigger.
        me._thisIsAUsedExpression(me.clearable);

        for (const triggerRef in me.triggers) {
            const trigger = me.triggers[triggerRef];

            if (trigger.align === 'start') {
                startTriggers.unshift(trigger);
            }
            else {
                endTriggers.push(trigger);
            }
        }

        // The triggers at each end are sorted "gavitationally".
        // Higher weight sorts towards the center which is the input element.
        startTriggers.sort(byWeight);
        endTriggers.sort(byWeightReverse);
        innerElements.unshift(...startTriggers.map(t => t.element));
        innerElements.push(...endTriggers.map(t => t.element));

        super.element = {
            className : me.label ? 'b-has-label' : '',
            children
        };

        // Value must be injected into the input element after it has been constructed, not in the
        // initial template, otherwise the caret position will not be as expected.
        if (value != null) {
            me.value = value;
        }

        me.updateEmpty();
        me.updateInvalid();
    }

    get element() {
        return super.element;
    }

    //endregion

    //region Focus & select

    get focusElement() {
        return this.input;
    }

    /**
     * Selects the field contents. Optionally may be passed a start and end.
     * NOTE: This method is async for IE11
     * @param {Number} [start] The start index from which to select the input.
     * @param {Number} [end] The index at which to end the selection of the input.
     */
    select(start, end) {
        // Use focusElement which is the input field in this class
        // but allows subclasses to use other elements.
        // See, for example, TextAreaField
        const input = this.focusElement;

        if (input.value.length) {

            if (arguments.length === 0) {
                this.selectAll();
                return;
            }

            // Only allowed to select range in certain element / input types
            if (!this.supportsTextSelection) {
                return;
                // throw new Error('Trying to select text on an invalid element type');
            }

            if (BrowserHelper.isIE11) {
                // this.clearTimeout(this.selectTimeout);

                // HACK: IE focus processing is async and we can't select text until field is focused
                // Getting flaky exception in IE, something timing related. Found no way to detect it so using this
                // workaround for now
                input.focus();
                input.setSelectionRange(start, end);
                //
                // this.selectTimeout = this.setTimeout(() => {
                //     input && input.setSelectionRange(start, end);
                // }, 10);
            }
            else {
                input.setSelectionRange(start, end);
            }
        }
    }

    moveCaretToEnd() {
        const input = this.input;

        if (input.createTextRange) {
            const range = input.createTextRange();
            range.collapse(false);
            range.select();
        }
        else if (this.supportsTextSelection) {
            // Move caret to the end if possible
            this.select(input.value.length, input.value.length);
        }

    }

    selectAll() {
        this.focusElement.select();
    }

    // called on value changes to update styling of empty vs non-empty field
    updateEmpty() {
        const { isEmptyInput, isEmpty, element, clearIcon } = this,
            empty = isEmptyInput && isEmpty;

        if (element) {
            if (clearIcon) {
                // IE11...
                clearIcon.classList[empty ? 'add' : 'remove']('b-icon-hidden');
            }

            // IE11...
            element.classList[empty ? 'add' : 'remove']('b-empty');
        }
    }

    updateInvalid() {
        const { isValid, element, errorTip, inputWrap } = this;

        if (element) {
            element.classList[isValid ? 'remove' : 'add']('b-invalid');

            // We achieved validity, so ensure the error tip is hidden
            if (isValid) {
                if (errorTip.isVisible && errorTip.field === this) {
                    errorTip.hide();
                }
            }

            // If the mouse is over, the tip should spring into view
            else if (inputWrap.contains(Tooltip.currentOverElement)) {
                // Already shown by this field's inputWrap, just update content.
                if (errorTip.activeTarget === inputWrap && errorTip.isVisible) {
                    errorTip.onBeforeShow();
                }
                else {
                    errorTip.activeTarget = inputWrap;
                    errorTip.showBy(inputWrap);
                }
            }
        }
    }

    //endregion

    //region Getters/setters

    get editable() {
        return this._editable && !this.readOnly;
    }

    set editable(editable) {
        const
            me    = this,
            input = me.input;

        me._editable = editable;

        if (input) {
            if (!me.readOnly) {
                input.readOnly = editable === false ? 'readOnly' : null;
            }

            if (editable !== false) {
                me.inputListenerRemover = EventHelper.on({
                    element  : input,
                    thisObj  : me,
                    focus    : 'internalOnInputFocus',
                    change   : 'internalOnChange',
                    input    : 'internalOnInput',
                    keydown  : 'internalOnKeyPress',
                    keypress : 'internalOnKeyPress',
                    keyup    : 'internalOnKeyPress'
                });
            }
            else {
                me.inputListenerRemover && me.inputListenerRemover();
            }
        }
    }

    set clearable(clearable) {
        this._clearable = clearable;
        if (clearable && !this.triggers) {
            this.triggers = {};
        }
    }

    get clearable() {
        return this._clearable;
    }

    /**
     * The trigger Widgets as specified by the {@link #config-triggers} configuration and the {@link #config-clearable} configuration.
     * Each is a {@link Common.widget.Widget Widget} instance which may be hidden, shown and observed and styled just like any other widget.
     * @property {Object}
     */
    set triggers(triggers) {
        const me = this,
            myTriggers = me._triggers = {};

        if (me.clearable) {
            (triggers || (triggers = {})).clear = {
                cls : 'b-icon-remove',
                handler() {
                    me._isUserAction = true;
                    me.clear();
                    me._isUserAction = false;
                },
                weight : 1000
            };
        }

        for (const triggerRef in triggers) {
            myTriggers[triggerRef] = WidgetHelper.createWidget(ObjectHelper.assign({
                type      : 'trigger',
                reference : triggerRef,
                parent    : me
            }, triggers[triggerRef]), me.defaultTriggerType || 'trigger');
        }
    }

    get triggers() {
        return this._triggers;
    }

    set labelWidth(labelWidth) {
        if (this.labelElement) {
            this.labelElement.style.flex = `0 0 ${DomHelper.setLength(labelWidth)}`;

            // If there's a label width, the input must conform with it, and not try to expand to 100%
            this.inputWrap.style.flexBasis = (labelWidth == null) ? '' : 0;
        }
    }

    /**
     * Get/set fields label. Please note that the Field needs to have a label specified from start for this to work,
     * otherwise no element is created.
     * @property {String}
     */
    get label() {
        return this._label;
    }

    set label(label) {
        if (label === null || label === undefined) label = '';
        this._label = label;
        // since value is used in template it is not certain that element is available
        // TODO: move the code from template here instead
        if (this.labelElement) {
            // using innerHTML since we sometimes use icons as label
            this.labelElement.innerHTML = label;
        }
    }

    /**
     * Get/set read only
     * @property {Boolean}
     */
    get readOnly() {
        // Fields which are disabled cannot be updated.
        // This ensures ESC and clear click won't be able to clear disabled fields.
        return this._readOnly || this.disabled;
    }

    set readOnly(readOnly) {
        this._readOnly = readOnly;

        this.element.classList[readOnly ? 'add' : 'remove']('b-readonly');
        if (readOnly) {
            this.input.setAttribute('readonly', '');
        }
        else {
            this.input.removeAttribute('readonly');
        }
    }

    /**
     * Returns true if the field value is valid
     * @type {Boolean}
     * @readonly
     */
    get isValid() {
        if (this.errors && ObjectHelper.getTruthyValues(this.errors).length) {
            return false;
        }
        if (this.input && this.input.validity) {
            return this.input.validity.valid;
        }

        return true;
    }

    /**
     * Returns true if the field value is empty
     * @type {boolean}
     * @readonly
     */
    get isEmpty() {
        return this.value == null || this.value === '';
    }

    /**
     * Returns true if the field's input is empty
     * @type {boolean}
     * @readonly
     */
    get isEmptyInput() {
        return !this.input || this.input.value == null || this.input.value === '';
    }

    /**
     * Gets or sets the value. The returned type will depend upon the Field subclass.
     *
     * `TextField` returns a `String`.
     *
     * `NumberField` returns a `Number`.
     *
     * `DateField` and `TimeField` return a `Date` object, and `null` if the field is empty.
     *
     * `Combo` will return a `String` if configured with `items` as a simple string array.
     * Otherwise it will return the {@link Common.widget.Combo#config-valueField} value from the
     * selected record, or `null` if no selection has been made.
     * @property {*}
     */
    get value() {
        return this._value;
    }

    set value(value) {
        const me = this,
            oldValue = me._value;

        if (me.hasChanged(oldValue, value)) {
            // lastValue is used for IE to check if a change event should be triggered when pressing ENTER
            if (!me.inputting) {
                me._lastValue = value;
            }

            me._value = value;

            if (me.required && (value == '' || value == null)) {
                me.setError('fieldRequired', true);
            }
            else {
                me.clearError('fieldRequired', true);
            }

            // Do not trigger change event during configuration phase
            // or during keyboard input
            if (!me.isConfiguring && !me.inputting) {
                // trigger change event, signaling that origin is from set operation,
                // makes it easier to ignore such events in applications that set value on load etc
                me.trigger('change', { value : me.value, oldValue, userAction : Boolean(me._isUserAction), valid : me.isValid });
            }

            me.syncInputFieldValue();
        }
    }

    /**
     * Compare's this field's value with its previous value. May be overridden in subclasses
     * which have more complex value types. See, for example, {@link Common.widget.DurationField}.
     * @param {*} oldValue
     * @param {*} newValue
     * @private
     */
    hasChanged(oldValue, newValue) {
        return newValue !== oldValue;
    }

    /**
     * Called by the base Field class's `set value` to sync the state of the UI with the field's value.
     *
     * Relies upon the class implementation of `get inputValue` to return a string representation of
     * the value for user consumption and editing.
     * @private
     */
    syncInputFieldValue() {
        const me = this,
            input = me.input;

        // If we are updating from internalOnInput, we must not update the input field
        if (input && !me.inputting) {
            // Subclasses may implement their own read only inputValue property.
            me.input.value = me.inputValue;

            // If it's being manipulated from the outside, highlight it
            if (!me.isConfiguring && !me.containsFocus && me.highlightExternalChange) {
                input.classList.remove('b-field-updated');
                me.clearTimeout('removeUpdatedCls');
                me.highlightChanged();
            }
        }
        me.updateEmpty();
        me.updateInvalid();
    }

    addUpdatedCls() {
        this.input.classList.add('b-field-updated');
        this.setTimeout('removeUpdatedCls', 1500);
    }

    removeUpdatedCls() {
        this.input.classList.remove('b-field-updated');
    }

    /**
     * A String representation of the value of this field for {@link #function-syncInputFieldValue} to use
     * as the input element's value.
     *
     * Subclasses may override this to create string representations.
     *
     * For example, {@link Common.widget.DateField}'s implementation will format the field date
     * value according to its configured {@link Common.widget.DateField#config-format}. And {@link Common.widget.Combo}'s
     * implementation will return the {@link Common.widget.Combo#config-displayField} of the selected record.
     * @internal
     * @readOnly
     */
    get inputValue() {
        // Do not use the _value property. If called during configuration, this
        // will import the configured value from the config object.
        return this.value == null ? '' : this.value;
    }

    get supportsTextSelection() {
        const input = this.focusElement;

        // Text selection using setSelectionRange is allowed in Chrome for certain elements. Edge supports it even for input[type=number]
        return input && (input.tagName.toLowerCase() === 'textarea' || (input.type && (/text|search|password|tel|url/.test(input.type) || BrowserHelper.isEdge)));
    }

    //endregion

    //region Events

    internalOnInputFocus() {
        const length = this.input.value.length;

        // Help IE to set caret at the end like the other browsers
        if (BrowserHelper.isIE11 && length && !this.autoSelect) {
            this.select(length, length);
        }
    }

    /**
     * Trigger event when fields input changes
     * @fires change
     * @private
     */
    internalOnChange(event) {
        const me = this,
            value = me.value,
            oldValue = me._lastValue;

        // Don't trigger change if we enter invalid value or if value has not changed (for IE when pressing ENTER)
        if (me.isValid && value !== oldValue) {
            me._lastValue = value;

            // trigger change event, signaling that origin is from user
            me.trigger('change', { value, oldValue, event, userAction : true });

            // per default Field triggers action event on change, but might be reconfigured in subclasses (such as Combo)
            if (me.defaultAction === 'change') {
                me.trigger('action', { value, oldValue, event });
            }
        }

        // since Widget has Events mixed in configured with 'callOnFunctions' this will also call onClick and onAction
    }

    /**
     * Trigger event when user inputs into field
     * @fires input
     * @param event
     * @private
     */
    internalOnInput(event) {
        const me = this;

        // Keep the value synced with the inputValue at all times.
        me.inputting = true;
        me.value = me.input.value;
        me.inputting = false;

        me.trigger('input', { value : me.value, event });

        me.changeOnKeyStroke && me.changeOnKeyStroke(event);

        // since Widget has Events mixed in configured with 'callOnFunctions' this will also call onInput
    }

    internalOnKeyPress(event) {
        const me = this;

        if (event.type === 'keydown') {
            if (event.key === 'Escape' && !me.readOnly && me.clearable) {
                me.clear();

                // don't allow further processing of this event if it had destructive consequences
                if (me.isDestroyed) {
                    event.stopImmediatePropagation();
                    return;
                }
            }
            // #5730 - IE11 doesn't trigger "change" event by Enter click
            else if (event.key === 'Enter' && BrowserHelper.isIE11) {
                this.internalOnChange(event);
            }
        }

        me.trigger(event.type, { event });
    }

    clear() {
        this.value = null;

        this.trigger('clear');
    }

    //endregion

    //region Error

    /**
     * Adds an error message to the list of errors on this field.
     * By default the field's valid/invalid state is updated; pass
     * `false` as the second parameter to disable that if multiple
     * changes are being made to the error state.
     * @param {String} error A locale string, or message to use as an error message.
     * @param {Boolean} [silent=false] Pass as `true` to skip updating the field's valid/invalid state.
     */
    setError(error, silent) {
        (this.errors || (this.errors = {}))[error] = this.L(error);

        if (!silent) {
            this.updateInvalid();
        }
    }

    /**
     * Removes an error message from the list of errors on this field.
     * By default the field's valid/invalid state is updated; pass
     * `false` as the second parameter to disable that if multiple
     * changes are being made to the error state.
     * @param {String} error A locale string, or message to remove.
     * @param {Boolean} [silent=false] Pass as `true` to skip updating the field's valid/invalid state.
     */
    clearError(error, silent) {
        if (this.errors) {
            delete this.errors[error];
        }
        if (!silent) {
            this.updateInvalid();
        }
    }

    /**
     * Returns an array of error messages as set by {@link #function-setError}, or
     * `undefined` if there are currently no errors.
     * @return {String[]} The errors for this field, or `undefined` if there are no errors.
     */
    getErrors() {
        if (!this.isValid) {
            const errors = this.errors ? ObjectHelper.getTruthyValues(this.errors) : [this.input.validationMessage || this.L('invalidValue')];

            if (errors && errors.length) {
                return errors;
            }
        }
    }

    //endregion

}

/**
 * Base class for field triggers May be configured with a `cls` and a `handler` which is a function (or name of a function)
 * in the owning Field.
 * @extends Common/widget/Widget
 */
Field.Trigger = class FieldTrigger extends Widget {
    static get defaultConfig() {
        return {
            align : null
        };
    }

    template() {
        return `<div class="b-icon b-align-${this.align || 'end'}"></div>`;
    }

    construct(config) {
        super.construct(config);

        EventHelper.on({
            element : this.element,
            click   : {
                handler : 'onClick',
                thisObj : this
            },
            mousedown : {
                handler : 'onMousedown',
                thisObj : this
            }
        });
    }

    onClick(e) {
        const me = this,
            field = me.field,
            handler = (typeof me.handler === 'function') ? me.handler : field[me.handler];

        if (field.disabled || field.readOnly) {
            return;
        }

        if (handler && field.trigger('trigger', {
            trigger : me
        }) !== false) {
            handler.call(field, e);
        }
    }

    onMousedown(e) {
        const field = this.field,
            isKeyEvent = ('key' in e);

        // If it's a touch tap on the trigger of an editable, then
        // avoid the keyboard by setting the field to not be editable
        // before focusing the field. Reset to be editable after focusing
        // has happened. Keyboard will not appear.
        if (!isKeyEvent && DomHelper.isTouchEvent) {
            if (field.editable) {
                field.editable = false;
                field.setTimeout(() => field.editable = true, 500);
            }
        }

        e.preventDefault();
        if (document.activeElement !== field.input) {
            field.focus();
        }
    }

    get field() {
        return this.parent;
    }
};

Field.SpinTrigger = class SpinTrigger extends Field.Trigger {
    static get defaultConfig() {
        return {
            repeat : true
        };
    }

    template() {
        return  `<div class="b-icon b-align-${this.align || 'end'}">
                    <div reference="upButton" class="b-icon b-spin-up"></div>
                    <div reference="downButton" class="b-icon b-spin-down"></div>
                </div>`;
    }

    set repeat(repeat) {
        const me = this;

        if (repeat) {
            me.clickRepeater = new ClickRepeater(Object.assign({
                element : me.element
            }, repeat));
        }
        else if (me.clickrepeater) {
            me.clickRepeater.destroy();
            me.clickrepeater = null;
        }
    }

    onClick(e) {
        const me = this,
            field = me.field;

        if (field.disabled) {
            return;
        }

        if (e.target === me.upButton) {
            field.doSpinUp();
        }
        else if (e.target === me.downButton) {
            field.doSpinDown();
        }
    }
};

// Those two classes are defined in a very unusual manner, have to specify names directly
Field.Trigger._$name = 'FieldTrigger';
Field.SpinTrigger._$name = 'SpinTrigger';

BryntumWidgetAdapterRegister.register('trigger', Field.Trigger);
BryntumWidgetAdapterRegister.register('spintrigger', Field.SpinTrigger);
