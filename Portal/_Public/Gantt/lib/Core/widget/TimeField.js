import DH from '../helper/DateHelper.js';
import PickerField from './PickerField.js';
import TimePicker from './TimePicker.js';

/**
 * @module Core/widget/TimeField
 */

/**
 * The time field widget is a text input field with a time picker drop down. It shows left/right arrows to increase or
 * decrease time by the {@link #config-step step value}.
 *
 * This field can be used as an {@link Grid.column.Column#config-editor editor} for the {@link Grid.column.Column Column}.
 * It is used as the default editor for the {@link Grid.column.TimeColumn TimeColumn}.
 *
 * ## Configuring the picker hour / minute fields
 *
 * You can easily configure the fields in the drop-down picker, to control the increment of the up/down step arrows:
 *
 * ```javascript
 * new TimeField({
 *     label     : 'Time field',
 *     appendTo  : document.body,
 *     picker    : {
 *         items : {
 *             minute : {
 *                 step : 5
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * This widget may be operated using the keyboard. `ArrowDown` opens the time picker, which itself
 * is keyboard navigable. `Shift+ArrowDown` activates the {@link #config-step} back trigger.
 * `Shift+ArrowUp` activates the {@link #config-step} forwards trigger.
 *
 * @extends Core/widget/PickerField
 *
 * @example
 * let field = new TimeField({
 *   format: 'HH'
 * });
 *
 * @classType timefield
 * @inlineexample Core/widget/TimeField.js
 * @inputfield
 */
export default class TimeField extends PickerField {
    //region Config
    static get $name() {
        return 'TimeField';
    }

    // Factoryable type name
    static get type() {
        return 'timefield';
    }

    // Factoryable type alias
    static get alias() {
        return 'time';
    }

    static get configurable() {
        return {
            picker : {
                type  : 'timepicker',
                align : {
                    align    : 't0-b0',
                    axisLock : true
                }
            },

            /**
             * Get/Set format for time displayed in field (see {@link Core.helper.DateHelper#function-format-static}
             * for formatting options).
             * @member {String} format
             */
            /**
             * Format for date displayed in field (see Core.helper.DateHelper#function-format-static for formatting
             * options).
             * @config {String}
             * @default
             */
            format : 'LT',

            triggers : {
                expand : {
                    align   : 'end',
                    handler : 'onTriggerClick',
                    compose : () => ({
                        children : [{
                            class : {
                                'b-icon-clock-live' : 1
                            }
                        }]
                    })
                },

                back : {
                    align   : 'start',
                    cls     : 'b-icon b-icon-angle-left b-step-trigger',
                    key     : 'Shift+ArrowDown',
                    handler : 'onBackClick'
                },

                forward : {
                    align   : 'end',
                    cls     : 'b-icon b-icon-angle-right b-step-trigger',
                    key     : 'Shift+ArrowUp',
                    handler : 'onForwardClick'
                }
            },

            /**
             * Get/set min value, which can be a Date or a string. If a string is specified, it will be converted using
             * the specified {@link #config-format}.
             * @member {Date} min
             * @accepts {String|Date}
             */
            /**
             * Min time value
             * @config {String|Date}
             */
            min : null,

            /**
             * Get/set max value, which can be a Date or a string. If a string is specified, it will be converted using
             * the specified {@link #config-format}.
             * @member {Date} max
             * @accepts {String|Date}
             */
            /**
             * Max time value
             * @config {String|Date}
             */
            max : null,

            /**
             * The `step` property may be set in Object form specifying two properties, `magnitude`, a Number, and
             * `unit`, a String.
             *
             * If a Number is passed, the steps's current unit is used and just the magnitude is changed.
             *
             * If a String is passed, it is parsed by {@link Core.helper.DateHelper#function-parseDuration-static}, for
             * example `'5m'`, `'5 m'`, `'5 min'`, `'5 minutes'`.
             *
             * Upon read, the value is always returned in object form containing `magnitude` and `unit`.
             * @member {DurationConfig} step
             * @accepts {String|Number|DurationConfig}
             */
            /**
             * Time increment duration value. Defaults to 5 minutes.
             * The value is taken to be a string consisting of the numeric magnitude and the units.
             * The units may be a recognised unit abbreviation of this locale or the full local unit name.
             * For example `"10m"` or `"5min"` or `"2 hours"`
             * @config {String}
             */
            step : '5m',

            stepTriggers : null,

            /**
             * Get/set value, which can be a Date or a string. If a string is specified, it will be converted using the
             * specified {@link #config-format}.
             * @member {Date} value
             * @accepts {String|Date}
             */
            /**
             * Value, which can be a Date or a string. If a string is specified, it will be converted using the
             * specified {@link #config-format}
             * @config {String|Date}
             */
            value : null,

            /**
             * Set to true to not clean up the date part of the passed value. Set to false to reset the date part to
             * January 1st
             * @prp {Boolean}
             * @default
             */
            keepDate : false
        };
    }

    //endregion

    //region Init & destroy

    changePicker(picker, oldPicker) {
        const me = this;

        return TimePicker.reconfigure(oldPicker, picker, {
            owner : me,

            defaults : {
                value      : me.value,
                forElement : me[me.pickerAlignElement],
                owner      : me,
                align      : {
                    anchor : me.overlayAnchor,
                    target : me[me.pickerAlignElement]
                },

                onTimeChange({ time }) {
                    me._isUserAction = true;
                    me.value = time;
                    me._isUserAction = false;
                }
            }
        });
    }

    //endregion

    //region Click listeners

    onBackClick() {
        const
            me      = this,
            { min } = me;

        if (!me.readOnly && me.value) {
            const newValue = DH.add(me.value, -1 * me.step.magnitude, me.step.unit);

            if (!min || min.getTime() <= newValue) {
                me._isUserAction = true;
                me.value = newValue;
                me._isUserAction = false;
            }
        }
    }

    onForwardClick() {
        const
            me      = this,
            { max } = me;

        if (!me.readOnly && me.value) {
            const newValue = DH.add(me.value, me.step.magnitude, me.step.unit);

            if (!max || max.getTime() >= newValue) {
                me._isUserAction = true;
                me.value = newValue;
                me._isUserAction = false;
            }
        }
    }

    //endregion

    // region Validation

    get isValid() {
        const me  = this;

        me.clearError('L{Field.minimumValueViolation}', true);
        me.clearError('L{Field.maximumValueViolation}', true);

        let value = me.value;

        if (value) {
            value = value.getTime();
            if (me._min && me._min.getTime() > value) {
                me.setError('L{Field.minimumValueViolation}', true);
                return false;
            }

            if (me._max && me._max.getTime() < value) {
                me.setError('L{Field.maximumValueViolation}', true);
                return false;
            }
        }

        return super.isValid;
    }

    hasChanged(oldValue, newValue) {
        if (oldValue?.getTime && newValue?.getTime) {
            // Only care about the time part
            return oldValue.getHours() !== newValue.getHours() ||
                oldValue.getMinutes() !== newValue.getMinutes() ||
                oldValue.getSeconds() !== newValue.getSeconds() ||
                oldValue.getMilliseconds() !== newValue.getMilliseconds();
        }

        return super.hasChanged(oldValue, newValue);
    }

    //endregion

    //region Toggle picker

    /**
     * Show picker
     */
    showPicker() {
        const
            me = this,
            {
                picker,
                value
            }  = me;

        if (me.readOnly) {
            return;
        }

        picker.value = value;
        picker.format = me.format;
        picker.maxTime = me.max;
        picker.minTime = me.min;

        // If we had no value initially.
        if (!value) {
            me.value = picker.value;
        }

        super.showPicker(true);
    }

    onPickerShow() {
        super.onPickerShow();

        // Remove PickerField key listener
        this.pickerKeyDownRemover = this.pickerKeyDownRemover?.();
    }

    /**
     * Focus time picker
     */
    focusPicker() {
        this.picker.focus();
    }

    //endregion

    //region Getters/setters

    transformTimeValue(value) {
        if (value != null) {
            if (typeof value === 'string') {
                value = DH.parse(value, this.format);

                if (this.keepDate) {
                    value = DH.copyTimeValues(new Date(this.value), value);
                }
            }
            else {
                value = new Date(value);
            }

            // We insist on a *valid* Time as the value
            if (DH.isValidDate(value)) {
                if (!this.keepDate) {
                    // Clear date part back to zero so that all we have is the time part of the epoch.
                    return DH.getTime(value);
                }
                else {
                    return value;
                }
            }
        }
        return null;
    }

    changeMin(value) {
        return this.transformTimeValue(value);
    }

    updateMin(value) {
        const { input } = this;

        if (input) {
            if (value == null) {
                input.removeAttribute('min');
            }
            else {
                input.min = value;
            }
        }

        this.syncInvalid();
    }

    changeMax(value) {
        return this.transformTimeValue(value);
    }

    updateMax(value) {
        const { input } = this;

        if (input) {
            if (value == null) {
                input.removeAttribute('max');
            }
            else {
                input.max = value;
            }
        }

        this.syncInvalid();
    }

    changeValue(value, was) {
        const
            me = this,
            newValue = me.transformTimeValue(value);

        // A value we could not parse
        if (value && !newValue || (me.isRequired && value === '')) {
            // setError uses localization
            me.setError('L{invalidTime}');
            return;
        }

        me.clearError('L{invalidTime}');

        // Reject non-change
        if (me.hasChanged(was, newValue)) {
            return super.changeValue(newValue, was);
        }

        // But we must fix up the display in case it was an unparseable string
        // and the value therefore did not change.
        if (!me.inputting) {
            me.syncInputFieldValue(true);
        }
    }

    updateValue(value, was) {
        const { expand } = this.triggers;

        // This makes to clock icon show correct time
        if (expand && value) {
            expand.element.firstElementChild.style.animationDelay =
                -((value.getHours() * 60 + value.getMinutes()) / 10) + 's';
        }

        super.updateValue(value, was);
    }

    changeStep(value, was) {
        const type = typeof value;

        if (!value) {
            return null;
        }

        if (type === 'number') {
            value = {
                magnitude : Math.abs(value),
                unit      : was ? was.unit : 'hour'
            };
        }
        else if (type === 'string') {
            value = DH.parseDuration(value);
        }

        if (value?.unit && value?.magnitude) {
            if (value.magnitude < 0) {
                value = {
                    magnitude : -value.magnitude,  // Math.abs
                    unit      : value.unit
                };
            }

            return value;
        }
    }

    updateStep(value) {
        // If a step is configured, show the steppers
        this.element.classList[value ? 'remove' : 'add']('b-no-steppers');

        this.syncInvalid();
    }

    updateFormat() {
        this.syncInputFieldValue(true);
    }

    get inputValue() {
        return DH.format(this.value, this.format);
    }

    //endregion

    //region Localization

    updateLocalization() {
        super.updateLocalization();
        this.syncInputFieldValue(true);
    }

    //endregion
}

// Register this widget type with its Factory
TimeField.initClass();
