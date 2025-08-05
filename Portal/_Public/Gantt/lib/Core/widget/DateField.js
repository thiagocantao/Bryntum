import PickerField from './PickerField.js';
import DatePicker from './DatePicker.js';
import DH from '../helper/DateHelper.js';



/**
 * @module Core/widget/DateField
 */

/**
 * Date field widget (text field + date picker).
 *
 * This field can be used as an {@link Grid.column.Column#config-editor editor} for the {@link Grid.column.Column Column}.
 * It is used as the default editor for the {@link Grid.column.DateColumn DateColumn}.
 *
 * This widget may be operated using the keyboard. `ArrowDown` opens the date picker, which itself
 * is keyboard navigable. `Shift+ArrowDown` activates the {@link #config-step} back trigger.
 * `Shift+ArrowUp` activates the {@link #config-step} forwards trigger.
 *
 * @extends Core/widget/PickerField
 *
 * @example
 * // minimal DateField config with date format specified
 * let dateField = new DateField({
 *   format: 'YYMMDD'
 * });
 *
 * @classType datefield
 * @inlineexample Core/widget/DateField.js
 * @inputfield
 */
export default class DateField extends PickerField {
    //region Config
    static get $name() {
        return 'DateField';
    }

    // Factoryable type name
    static get type() {
        return 'datefield';
    }

    // Factoryable type alias
    static get alias() {
        return 'date';
    }

    static get configurable() {
        return {
            /**
             * Get / set format for date displayed in field (see {@link Core.helper.DateHelper#function-format-static}
             * for formatting options).
             * @member {String} format
             */
            /**
             * Format for date displayed in field. Defaults to using long date format, as defined by current locale (`L`)
             * @config {String}
             * @default
             */
            format : 'L',

            /**
             * A flag which indicates whether the date parsing should be strict - meaning if the date
             * is missing a year/month/day part - parsing fails.
             *
             * Turned off by default, meaning default values are substituted for missing parts.
             *
             * @config {Boolean}
             * @default
             */
            strictParsing : false,

            // same for all languages
            fallbackFormat : 'YYYY-MM-DD',
            timeFormat     : 'HH:mm:ss:SSS',

            /**
             * A flag which indicates what time should be used for selected date.
             * `false` by default which means time is reset to midnight.
             *
             * Possible options are:
             * - `false` to reset time to midnight
             * - `true` to keep original time value
             * - `'17:00'` a string which is parsed automatically
             * - `new Date(2020, 0, 1, 17)` a date object to copy time from
             * - `'entered'` to keep time value entered by user (in case {@link #config-format} includes time info)
             *
             * @config {Boolean|Date|String}
             * @default
             */
            keepTime : false,

            /**
             * Format for date in the {@link #config-picker}. Uses localized format per default
             * @config {String}
             */
            pickerFormat : null,

            /**
             * Set to true to first clear time of the field's value before comparing it to the max value
             * @internal
             * @config {Boolean}
             */
            validateDateOnly : null,

            triggers : {
                expand : {
                    cls     : 'b-icon-calendar',
                    handler : 'onTriggerClick',
                    weight  : 200
                },

                back : {
                    cls     : 'b-icon b-icon-angle-left b-step-trigger',
                    key     : 'Shift+ArrowDown',
                    handler : 'onBackClick',
                    align   : 'start',
                    weight  : 100
                },

                forward : {
                    cls     : 'b-icon b-icon-angle-right b-step-trigger',
                    key     : 'Shift+ArrowUp',
                    handler : 'onForwardClick',
                    align   : 'end',
                    weight  : 100
                }
            },

            // An optional extra CSS class to add to the picker container element
            calendarContainerCls : '',

            /**
             * Get/set min value, which can be a Date or a string. If a string is specified, it will be converted using
             * the specified {@link #config-format}.
             * @member {Date} min
             * @accepts {String|Date}
             */
            /**
             * Min value
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
             * Max value
             * @config {String|Date}
             */
            max : null,

            /**
             * The `step` property may be set in object form specifying two properties, `magnitude`, a Number, and
             * `unit`, a String.
             *
             * If a Number is passed, the step's current unit is used (or `day` if no current step set) and just the
             * magnitude is changed.
             *
             * If a String is passed, it is parsed by {@link Core.helper.DateHelper#function-parseDuration-static}, for
             * example `'1d'`, `'1 d'`, `'1 day'`, or `'1 day'`.
             *
             * Upon read, the value is always returned in object form containing `magnitude` and `unit`.
             * @member {DurationConfig} step
             * @accepts {String|Number|DurationConfig}
             */
            /**
             * Time increment duration value. If specified, `forward` and `back` triggers are displayed.
             * The value is taken to be a string consisting of the numeric magnitude and the units.
             * The units may be a recognised unit abbreviation of this locale or the full local unit name.
             * For example `'1d'` or `'1w'` or `'1 week'`. This may be specified as an object containing
             * two properties: `magnitude`, a Number, and `unit`, a String
             * @config {String|Number|DurationConfig}
             */
            step : false,

            stepTriggers : null,

            /**
             * The week start day in the {@link #config-picker}, 0 meaning Sunday, 6 meaning Saturday.
             * Uses localized value per default.
             * @config {Number}
             */
            weekStartDay : null,

            /**
             * A config object used to configure the {@link Core.widget.DatePicker datePicker}.
             * ```javascript
             * dateField = new DateField({
             *      picker    : {
             *          multiSelect : true
             *      }
             *  });
             * ```
             * @config {DatePickerConfig}
             */
            picker : {
                type         : 'datepicker',
                role         : 'dialog',
                floating     : true,
                scrollAction : 'realign',
                align        : {
                    align    : 't0-b0',
                    axisLock : true
                }
            },

            /**
             * Get/set value, which can be set as a Date or a string but always returns a Date. If a string is
             * specified, it will be converted using the specified {@link #config-format}
             * @member {Date} value
             * @accepts {String|Date}
             */
            /**
             * Value, which can be a Date or a string. If a string is specified, it will be converted using the
             * specified {@link #config-format}
             * @config {String|Date}
             */
            value : null
        };
    }

    //endregion

    //region Init & destroy

    /**
     * Creates default picker widget
     *
     * @internal
     */
    changePicker(picker, oldPicker) {
        const
            me       = this,
            defaults = {
                owner        : me,
                forElement   : me[me.pickerAlignElement],
                minDate      : me.min,
                maxDate      : me.max,
                weekStartDay : me._weekStartDay, // need to pass the raw value to let the component to use its default value

                align : {
                    anchor : me.overlayAnchor,
                    target : me[me.pickerAlignElement]
                },

                onSelectionChange : ({ selection, source : picker }) => {
                    // We only care about what DatePicker does if it has been opened
                    if (picker.isVisible) {
                        me._isUserAction = me._isPickerInput = true;
                        me.value = selection[0];
                        me._isPickerInput = me._isUserAction = false;
                        picker.hide();
                    }
                }
            };

        if (me.calendarContainerCls) {
            defaults.cls = me.calendarContainerCls;
        }

        // If we are in cleared state, the picker will also have no value.
        // But have it focused on today as a default.
        if (me.value) {
            defaults.value = me.value;
        }
        else {
            defaults.activeDate = new Date();
        }

        const result = DatePicker.reconfigure(oldPicker, picker, {
            owner : me,
            defaults
        });

        // Cells must exist early
        result?.refresh.flush();

        return result;
    }

    //endregion

    //region Click listeners

    get backShiftDate() {
        return DH.add(this.value, -1 * this._step.magnitude, this._step.unit);
    }

    onBackClick() {
        const
            me      = this,
            { min } = me;

        if (!me.readOnly && me.value) {
            const newValue = me.backShiftDate;
            if (!min || min.getTime() <= newValue) {
                me._isUserAction = true;
                me.value = newValue;
                me._isUserAction = false;
            }
        }
    }

    get forwardShiftDate() {
        return DH.add(this.value, this._step.magnitude, this._step.unit);
    }

    onForwardClick() {
        const
            me      = this,
            { max } = me;

        if (!me.readOnly && me.value) {
            const newValue = me.forwardShiftDate;
            if (!max || max.getTime() >= newValue) {
                me._isUserAction = true;
                me.value = newValue;
                me._isUserAction = false;
            }
        }
    }

    //endregion

    //region Toggle picker

    showPicker(focusPicker) {
        if (this.readOnly) {
            return;
        }

        const
            me           = this,
            { _picker }  = me;

        // If it's already instanced, navigate it to our date, or default of today.
        // It will be initialized correctly if not.
        if (_picker) {
            // In case a subclass uses a min getter (which does not update our min value) - ensure picker is correctly configured
            const pickerConfig = {
                minDate : me.min,
                maxDate : me.max
            };

            if (me.value) {
                pickerConfig.value = me.value;
            }
            // If the field is cleared, show today's Calendar in the picker
            // unless it's already been navigated elsewhere.
            else if (!_picker.activeDate) {
                pickerConfig.activeDate = new Date();
            }

            // Config dependencies are correctly processed by setConfig
            _picker.setConfig(pickerConfig);
        }

        super.showPicker(focusPicker);
    }

    focusPicker() {
        this.picker.focus();
    }

    //endregion

    // region Validation

    get isValid() {
        const me  = this;

        me.clearError('L{Field.minimumValueViolation}', true);
        me.clearError('L{Field.maximumValueViolation}', true);

        let value = me.value;

        if (value) {
            const { min, max, validateDateOnly } = me;

            // Validation of the date should only care about the date part
            if (validateDateOnly) {
                value = DH.clearTime(value, false);
            }

            if (min && value < min) {
                me.setError('L{Field.minimumValueViolation}', true);
                return false;
            }

            if (max && value > max) {
                me.setError('L{Field.maximumValueViolation}', true);
                return false;
            }
        }

        return super.isValid;
    }

    //endregion

    //region Getters/setters
    transformDateValue(value) {
        const me = this;

        if (value != null) {
            if (!DH.isDate(value)) {
                if (typeof value === 'string') {
                    // If date cannot be parsed with set format, try fallback - the more general one
                    value = DH.parse(value, me.format, me.strictParsing) || DH.parse(value, me.fallbackFormat, me.strictParsing);
                }
                else {
                    value = new Date(value);
                }
            }

            // We insist on a *valid* Date as the value
            if (DH.isValidDate(value)) {
                if ((!me.min || (value - me.min > -DH.MS_PER_DAY)) && (!me.max || value <= me.max)) {
                    return me.transformTimeValue(value);
                }

                return value;
            }
        }
        return null;
    }

    transformTimeValue(value) {
        const
            me = this,
            { keepTime } = me;

        value = DH.clone(value);

        if (!keepTime) {
            DH.clearTime(value, false);
        }
        // change time if keepTime !== 'entered'
        else if (keepTime !== 'entered') {
            const timeValue = DH.parse(keepTime, me.timeFormat);

            // if this.keepTime is a valid date or a string describing valid time copy from it
            if (DH.isValidDate(timeValue)) {
                DH.copyTimeValues(value, timeValue);
            }
            // otherwise try to copy from the current value
            else if (DH.isValidDate(me.value)) {
                DH.copyTimeValues(value, me.value);
            }
        }
        // if keepTime === 'entered' and picker is used apply current value time
        else if (me._isPickerInput && DH.isValidDate(me.value)) {
            DH.copyTimeValues(value, me.value);
        }
        // else don't change time

        return value;
    }

    changeMin(value) {
        return this.transformDateValue(value);
    }

    updateMin(min) {
        const { input, _picker } = this;

        if (input) {
            if (min == null) {
                input.removeAttribute('min');
            }
            else {
                input.min = min;
            }
        }

        // See if our lazy config has been realized...
        if (_picker) {
            _picker.minDate = min;
        }

        this.syncInvalid();
    }

    changeMax(value) {
        return this.transformDateValue(value);
    }

    updateMax(max) {
        const { input, _picker } = this;

        if (input) {
            if (max == null) {
                input.removeAttribute('max');
            }
            else {
                input.max = max;
            }
        }

        if (_picker) {
            _picker.maxDate = max;
        }

        this.syncInvalid();
    }

    get weekStartDay() {
        // This trick allows our weekStartDay to float w/the locale even if the locale changes
        return typeof this._weekStartDay === 'number' ? this._weekStartDay : DH.weekStartDay;
    }

    updateWeekStartDay(weekStartDay) {
        if (this._picker) {
            this._picker.weekStartDay = weekStartDay;
        }
    }

    changeValue(value, oldValue) {
        const
            me = this,
            newValue = me.transformDateValue(value);

        // A value we could not parse
        if (value && !newValue) {
            // setError uses localization
            me.setError('L{invalidDate}');
            return;
        }

        me.clearError('L{invalidDate}');

        // Reject non-change
        if (me.hasChanged(oldValue, newValue)) {
            return super.changeValue(newValue, oldValue);
        }

        // But we must fix up the display in case it was an unparseable string
        // and the value therefore did not change.
        if (!me.inputting) {
            me.syncInputFieldValue();
        }
    }

    updateValue(value, oldValue) {
        const picker = this._picker;

        if (picker && !this.inputting) {
            picker.value = picker.activeDate = value;
        }

        super.updateValue(value, oldValue);
    }

    changeStep(value, was) {
        const type = typeof value;

        if (!value) {
            return null;
        }

        if (type === 'number') {
            value = {
                magnitude : Math.abs(value),
                unit      : was ? was.unit : 'day'
            };
        }
        else if (type === 'string') {
            value = DH.parseDuration(value);
        }

        if (value && value.unit && value.magnitude) {
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

    hasChanged(oldValue, newValue) {
        // if both dates are provided and the field does not has time info in its format
        if (oldValue?.getTime && newValue?.getTime && this.keepTime !== 'entered') {
            // Only compare date part
            return !DH.isEqual(DH.clearTime(oldValue), DH.clearTime(newValue));
        }

        return super.hasChanged(oldValue && oldValue.getTime(), newValue && newValue.getTime());
    }

    get inputValue() {
        // Do not use the _value property. If called during configuration, this
        // will import the configured value from the config object.
        const date = this.value;

        return date ? DH.format(date, this.format) : '';
    }

    updateFormat() {
        if (!this.isConfiguring) {
            this.syncInputFieldValue(true);
        }
    }

    //endregion

    //region Localization

    updateLocalization() {
        super.updateLocalization();
        this.syncInputFieldValue(true);
    }

    //endregion

    //region Other

    internalOnKeyEvent(event) {
        super.internalOnKeyEvent(event);

        if (event.key === 'Enter' && this.isValid) {
            this.picker.hide();
        }
    }

    //endregion
}

// Register this widget type with its Factory
DateField.initClass();
