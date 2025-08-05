import DataField from './DataField.js';
import DateHelper from '../../helper/DateHelper.js';

/**
 * @module Core/data/field/DateDataField
 */

/**
 * This field class handles field of type `Date`.
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             'name',
 *             { name : 'birthday', type : 'date', format : 'YYYY-MM-DD' },
 *             { name : 'age', readOnly : true }
 *         ];
 *     }
 * }
 * ```
 *
 * When a field is declared as a `'date'`, non-null values are promoted to `Date` type. This is frequently needed due
 * to how date types are serialized to JSON strings.
 *
 * Date fields can have a special `defaultValue` of `'now'` which will convert to the current date/time.
 *
 * @extends Core/data/field/DataField
 * @classtype date
 * @datafield
 */
export default class DateDataField extends DataField {
    static get $name() {
        return 'DateDataField';
    }

    static get type() {
        return 'date';
    }

    static get prototypeProperties() {
        return {
            /**
             * The format of the date field.
             *
             * See {@link Core.helper.DateHelper DateHelper} for details.
             * @config {String} format
             * @default DateHelper.defaultFormat
             */
            format : null
        };
    }

    convert(value) {
        if (value == null) {
            if (!this.nullable) {
                value = this.nullValue;
            }
        }
        else if (value === 'now') {
            value = new Date();
        }
        else if (!(value instanceof Date)) {
            // Use configured format, if null/undefined use DateHelpers default format
            value = DateHelper.parse(value, this.format || DateHelper.defaultParseFormat);

            // if parsing has failed, we would like to return `undefined` to indicate the "absence" of data
            // instead of `null` (presence of "empty" data)
            if (!value || isNaN(value)) {
                value = undefined;
            }
        }

        return value;
    }

    serialize(value) {
        if (value instanceof Date) {
            // Use configured format or DateHelpers default one
            value = DateHelper.format(value, this.format || DateHelper.defaultFormat);
        }

        return value;
    }

    printValue(value) {
        return DateHelper.format(value, this.format || DateHelper.defaultFormat);
    }
}

DateDataField.initClass();
