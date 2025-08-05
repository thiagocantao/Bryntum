import TextField from './TextField.js';
import GlobalEvents from '../GlobalEvents.js';
import EventHelper from '../helper/EventHelper.js';
import DomHelper from '../helper/DomHelper.js';

/**
 * @module Common/widget/PickerField
 */

/**
 * Base class used for {@link Common.widget.Combo Combo}, {@link Common.widget.DateField DateField}, and {@link Common.widget.TimeField TimeField}.
 * Displays a picker ({@link Common.widget.List List}, {@link Common.widget.DatePicker DatePicker}) anchored to the field.
 * Not intended to be used directly
 *
 * When focused by means of *touch* tapping on the trigger element (eg, the down arrow on a Combo)
 * on a tablet, the keyboard will not be shown by default to allow for interaction with the dropdown.
 *
 * A second tap on the input area will then show the keyboard is required.
 *
 * @extends Common/widget/TextField
 * @abstract
 */
export default class PickerField extends TextField {
    //region Config

    static get defaultConfig() {
        return {

            /**
             * User can edit text in text field (otherwise only pick from attached picker)
             * @config {Boolean}
             * @default
             */
            editable : true,

            /**
             * The name of the element property to which the picker should size and align itself.
             * @config {String}
             * @default element
             */
            pickerAlignElement : 'inputWrap',

            // Does not get set, but prevents PickerFields inheriting value:'' from Field.
            value : null,

            triggers : {
                expand : {
                    cls : 'bars'
                }
            },

            /**
             * By default PickerFiled's picker is transient, and will {@link #function-hidePicker} when the user clicks or
             * taps outside or when focus moves outside picker.
             *
             * Configure as `false` to make picker non-transient.
             * @config {Boolean}
             * @default
             */
            autoClose : true,

            /**
             * Configure as `true` to have the picker expand upon focus enter.
             * @config {Boolean}
             */
            autoExpand : null,

            /**
             * A config object which is merged into the generated picker configuration to allow specific use cases
             * to override behaviour. For example:
             *
             *     picker: {
             *         align: {
             *             anchor: true
             *         }
             *     }
             *
             * @config {Object}
             * @default
             */
            picker : null,

            inputType : 'text',

            // We need to realign the picker if we resize (eg a multiSelect Combo's ChipView wrapping)
            monitorResize : true
        };
    }

    //endregion

    //region Init & destroy

    doDestroy() {
        const me = this;

        // Remove touch keyboard showing listener if we added it
        me.globalTapListener && me.globalTapListener();

        if (me._picker) {
            me.hidePicker();
            me._picker.destroy();
            me.pickerVisible = false;
        }

        super.doDestroy();
    }

    finalizeInit() {
        super.finalizeInit();

        const me          = this,
            element     = me.element;

        if (me.editable === false) {
            element.classList.add('b-not-editable');
            EventHelper.on({
                element : me.input,
                click   : 'onTriggerClick',
                thisObj : me
            });
        }
        else {
            // In case the field was temporarily set to readOnly="true" to prevent
            // the intrusive keyboard (This happens when tapping the trigger
            // and when focused by the container in response to a touch tap),
            // allow a subsequent touch tap to show the keyboard.
            me.globalTapListener = GlobalEvents.on({
                globaltap : 'showKeyboard',
                thisObj   : me
            });
        }
    }

    //endregion

    //region Events

    /**
     * Check if field value is valid
     * @internal
     */
    onEditComplete() {
        super.onEditComplete();
        this.autoClosePicker();
    }

    onElementResize(resizedElement) {
        const me = this;

        // If the field changes size while the picker is visible, the picker
        // must be kept in alignment. For example a multiSelect: true
        // ComboBox with a wrapped ChipView.
        if (me.pickerVisible) {
            // Push realignment out to the next AF, because this picker itself may move in
            // response to the element resize, and the picker must realign *after* that happens.
            // For example a multiSelect: true ComboBox with a wrapped ChipView inside
            // a Popup that is aligned *above* an element. When the ChipView gains or
            // loses height, the Popup must realign first, and then the List must align to the
            // new position of the ComboBox.
            me.picker.requestAnimationFrame(me.picker.realign, null, me.picker);
        }

        super.onElementResize(resizedElement);
    }

    /**
     * Allows using arrow keys to open/close list. Relays other keypresses to list if open.
     * @private
     */
    onInternalKeyDown(event) {
        const me = this;

        if (me.disabled) return;

        if (me.pickerVisible) {
            const { picker } = me;

            if (event.key === 'Escape') {
                event.stopPropagation();
                me.hidePicker();
            }
            else if (picker.onInternalKeyDown) {
                // if picker is visible, give it a shot at the event
                picker.onInternalKeyDown(event);
            }
            else if (event.key === 'ArrowDown') {
                if (picker.focusable) {
                    picker.focus();
                }
            }
        }
        else if (event.key === 'ArrowDown') {
            me.onTriggerClick(event);
        }
    }

    onFocusIn(e) {
        super.onFocusIn(e);
        if (this.autoExpand) {
            this.onTriggerClick(e);
        }
    }

    onFocusOut(e) {
        this.autoClosePicker();
        super.onFocusOut(e);
    }

    /**
     * User clicked trigger icon, toggle list.
     * @private
     */
    onTriggerClick(event) {
        if (!this.disabled) {
            // Pass focus flag as true if invoked by a key event
            this.togglePicker('key' in event);
        }
    }

    /**
     * User clicked on an editable input field. If it's a touch event
     * ensure that the keyboard is shown.
     * @private
     */
    showKeyboard({ event }) {
        const input = this.input;

        if (DomHelper.isTouchEvent && document.activeElement === input && event.target === input) {
            GlobalEvents.suspendFocusEvents();
            input.blur();
            input.focus();
            GlobalEvents.resumeFocusEvents();
        }
    }

    //endregion

    //region Toggle picker

    /**
     * Toggle picker display
     */
    togglePicker(focusPicker) {
        if (this.pickerVisible) {
            this.hidePicker();
        }
        else {
            this.showPicker(focusPicker);
        }
    }

    /**
     * Show picker
     */
    showPicker(focusPicker) {
        const me = this,
            picker = me.picker;

        if (!me.pickerHideShowListenersAdded) {
            picker.on({
                show    : 'onPickerShow',
                hide    : 'onPickerHide',
                thisObj : me
            });
            me.pickerHideShowListenersAdded = true;
        }

        picker.autoClose = me.autoClose;
        picker.show();

        // Not been vetoed
        if (picker.isVisible) {
            if (focusPicker) {
                me.focusPicker();
            }
        }
    }

    onPickerShow() {
        const me = this;

        me.pickerVisible = true;
        me.element.classList.add('b-open');
        me.trigger('togglePicker', { show : true });
        me.pickerTapOutRemover = GlobalEvents.on({
            globaltap : 'onPickerTapOut',
            thisObj   : me
        });
        me.pickerKeyDownRemover = EventHelper.on({
            element : me.picker.element,
            keydown : 'onPickerKeyDown',
            thisObj : me
        });
    }

    onPickerHide() {
        const me = this;

        me.pickerVisible = false;
        me.element.classList.remove('b-open');
        me.trigger('togglePicker', { show : false });
        me.pickerTapOutRemover && me.pickerTapOutRemover();
        me.pickerKeyDownRemover && me.pickerKeyDownRemover();
    }

    onPickerTapOut({ event }) {
        if (!this.owns(event.target)) {
            this.autoClosePicker();
        }
    }

    onPickerKeyDown(event) {
        if (event.key === 'Tab') {
            const activeEl = document.activeElement;

            // Offer our own element a shot at the TAB event.
            // Some widgets or plugins may actively navigate.
            this.input.dispatchEvent(new KeyboardEvent('keydown', event));

            // No listener intervened, point the TAB event at the input,
            // and user agent default navigation will proceed.
            if (document.activeElement === activeEl) {
                this.input.focus();
            }
            // Some listener *did* navigate, prevent user agent default.
            else {
                event.preventDefault();
            }

            // If listeners have not destroyed us, close our picker.
            if (!this.isDestroyed) {
                this.hidePicker();
            }
        }
    }

    //endregion

    //region Visibility

    autoClosePicker() {
        if (this.autoClose) {
            this.hidePicker();
        }
    }

    /**
     * Hide picker
     */
    hidePicker() {
        if (this.pickerVisible) {
            this.picker.hide();
        }
    }

    focusPicker() {

    }

    focus() {
        const input = this.input;

        // If we are focusing an editable PickerField from a touch event, temporarily
        // set it to readOnly to prevent the showing of the intrusive keyboard.
        // It's more likely that a user on a touch device will interact with the picker
        // rather than the input field.
        // A second touch tap on an already focused input will show the keyboard;
        // see the showKeyboard method.
        if (DomHelper.isTouchEvent && this.editable) {
            input.readOnly = true;
            setTimeout(() => input.readOnly = false, 500);
        }
        super.focus();
    }

    //endregion

}
