import TextField from './TextField.js';
import DateHelper from '../helper/DateHelper.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import ObjectHelper from '../helper/ObjectHelper.js';

/**
 * @module Common/widget/DurationField
 */

/**
 * A specialized field allowing a user to also specify duration unit when editing the duration value.
 *
 * @extends Common/widget/TextField
 *
 * @classType durationfield
 */
export default class DurationField extends TextField {
    static get defaultConfig() {
        return {
            /**
             * The `value` config may be set in Object form specifying two properties,
             * `magnitude`, a Number, and `unit`, a String.
             *
             * If a String is passed, it is parsed in accordance with current locale rules.
             * The string is taken to be the numeric magnitude, followed by whitespace, then an abbreviation, or name of the unit.
             * @config {Object|String}
            */
            value : null,

            step : 1,

            /**
             * The duration unit to use with the current magnitude value.
             * @config {String}
             */
            unit : null,

            /**
             * When set to `true` the field will use short names of unit durations
             * (as returned by {@link Common.helper.DateHelper#function-getShortNameOfUnit-static}) when creating the
             * input field's display value.
             * @config {Boolean}
             */
            useAbbreviation : false,

            /**
             * Set to `true` to allow negative duration
             * @config {Boolean}
             */
            allowNegative : false,

            triggers : {
                spin : {
                    type : 'spintrigger'
                }
            }
        };
    }

    get inputValue() {
        // Do not use the _value property. If called during configuration, this
        // will import the configured value from the config object.
        const value = this.value;

        return value == null ? '' : this.valueToVisible(value.magnitude, value.unit);
    }

    set unit(unit) {
        this.value = {
            magnitude : super.value,
            unit
        };
    }

    get unit() {
        return this._unit;
    }

    valueToVisible(magnitude, unit) {
        if (!isNaN(magnitude)) {
            const valueInt = parseInt(magnitude, 10);

            // could happen if magnitude is null
            if (!isNaN(valueInt)) {
                const valueFixed = ObjectHelper.toFixed(magnitude, this.decimalPrecision);

                return String(valueInt == valueFixed ? valueInt : valueFixed) + ' ' +
                    DateHelper[this.useAbbreviation ? 'getShortNameOfUnit' : 'getLocalizedNameOfUnit'](unit || this.unit, magnitude !== 1);
            }
        }

        return '';
    }

    parseDuration(value) {
        if (value == null) {
            return null;
        }

        const duration = DateHelper.parseDuration(value, this.allowDecimals, this.unit);

        if (!duration) {
            return null;
        }

        duration.unit = duration.unit || this.unit;

        return duration;
    }

    get isValid() {
        const me = this;
        return (me.value == null && me.clearable && !me.required) || (me.value != null) && (me.allowNegative || me.value.magnitude >= 0);
    }

    internalOnChange(event) {
        const me = this,
            value = me.value,
            oldVal = me._lastValue;

        if (me.hasChanged(oldVal, value)) {
            me._lastValue = value;
            me.trigger('change', { value, event, userAction : true, valid : me.isValid });
        }
    }

    onFocusOut(e) {
        this.syncInputFieldValue();

        return super.onFocusOut(e);
    }

    /**
     * The `value` property may be set in Object form specifying two properties,
     * `magnitude`, a Number, and `unit`, a String.
     *
     * If a Number is passed, the field's current unit is used and just the magnitude is changed.
     *
     * If a String is passed, it is parsed in accordance with current locale rules.
     * The string is taken to be the numeric magnitude, followed by whitespace, then an abbreviation, or name of the unit.
     *
     * Upon read, the value is always returned in object form containing `magnitude` and `unit`.
     * @property {String|Number|Object}
    */
    set value(value) {
        const me = this;

        // A number means maintain the unit type
        if (typeof value === 'number') {
            value = {
                magnitude : value,
                unit      : me._unit
            };
        }
        // Not an object, parse as a string
        else if (typeof value !== 'object') {
            value = me.parseDuration(value);
        }

        if (me.value !== value) {
        // Only change the value if the input is valid
            if (value) {
                me._unit = value.unit;
                super.value = value;
            }
            else {
                me._unit = me.clearable ? null : 0;
                super.value = me.clearable ? null : 0;
            }
        }
    }

    get value() {
        return super.value;
    }

    hasChanged(oldValue, newValue) {
        return newValue && !oldValue || !newValue && oldValue || newValue && oldValue &&
            (newValue.magnitude != oldValue.magnitude || newValue.unit != oldValue.unit);
    }

    /**
     * The `milliseconds` property is a read only property which returns the
     * number of milliseconds in this field's value
     * @property {Number}
     */
    get milliseconds() {
        const v = this.value;

        return DateHelper.asMilliseconds(v.magnitude, v.unit);
    }

    onInternalKeyDown(keyEvent) {
        if (keyEvent.key === 'ArrowUp') {
            this.doSpinUp();
        }
        else if (keyEvent.key === 'ArrowDown') {
            this.doSpinDown();
        }
    }

    doSpinUp() {
        const
            me    = this,
            value = me.value;

        me._isUserAction = true;
        me.value = {
            unit      : value.unit,
            // null magnitude will result NaN
            magnitude : (value.magnitude || 0) + me.step
        };
        me._isUserAction = false;
    }

    doSpinDown() {
        const
            me    = this,
            value = me.value;

        if (me.allowNegative || value.magnitude > 0) {
            me._isUserAction = true;
            me.value = {
                unit      : value.unit,
                // null magnitude will result NaN
                magnitude : (value.magnitude || 0) - me.step
            };
            me._isUserAction = false;
        }
    }
}

BryntumWidgetAdapterRegister.register('durationfield', DurationField);
BryntumWidgetAdapterRegister.register('duration', DurationField);
