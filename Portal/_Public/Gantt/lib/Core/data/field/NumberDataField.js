import DataField from './DataField.js';

/**
 * @module Core/data/field/NumberDataField
 */

/**
 * This field class handles field of type `Number`.
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             'name',
 *             { name : 'age', type : 'number' }
 *         ];
 *     }
 * }
 * ```
 *
 * When a field is declared as a `'number'`, non-null values are promoted to `Number` type. This is seldom required, but
 * can be useful if a field value is received as a string but should be treated as a number.
 *
 * @extends Core/data/field/DataField
 * @classtype number
 * @datafield
 */
export default class NumberDataField extends DataField {
    static get $name() {
        return 'NumberDataField';
    }

    static get type() {
        return 'number';
    }

    static get alias() {
        return 'float';
    }

    static get prototypeProperties() {
        return {
            /**
             * The value to replace `null` when the field is not `nullable`.
             * @config {Number}
             * @default
             */
            nullValue : 0,

            /**
             * The numeric precision of this field. Values are rounded to the specified number of digits. If `null`,
             * the default, no rounding is performed.
             * @config {Number}
             * @default
             */
            precision : null
        };
    }

    isEqual(first, second) {
        // NaN !== NaN in JS which results having a number field w/ such value always dirty
        // Not sure having two NaN-s not equal each other makes any sense here to us ..so handle it
        return (isNaN(Number(first)) && isNaN(Number(second))) || super.isEqual(first, second);
    }

    convert(value) {
        if (value == null) {
            return this.nullable ? value : this.nullValue;
        }

        value = Number(value);

        // Returning undefined to let set know that this is a invalid value
        if (isNaN(value)) {
            return;
        }

        let scale = this.precision;

        if (scale) {
            scale = 10 ** scale;
            value = Math.round(value * scale) / scale;
        }
        else if (scale === 0) {
            value = Math.round(value);
        }

        return value;
    }
}

NumberDataField.initClass();
