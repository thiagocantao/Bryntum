import Base from '../../Base.js';
import Factoryable from '../../mixin/Factoryable.js';
import ObjectHelper from '../../helper/ObjectHelper.js';

/**
 * @module Core/data/field/DataField
 */

/**
 * This is the base class for Model field classes. A field class defines how to handle the data for a particular type
 * of field. Many of these behaviors can be configured on individual field instances.
 *
 * @extends Core/Base
 */
export default class DataField extends Base.mixin(Factoryable) {
    static get type() {
        return 'auto';
    }

    static get factoryable() {
        return {
            defaultType : 'auto'
        };
    }

    static get prototypeProperties() {
        return {
            /**
             * The name of the field.
             * @config {String} name
             */

            /**
             * A function that compares two values and returns a value < 0 if the first is less than the second, or 0
             * if the values are equal, or a value > 0 if the first is greater than the second.
             * @config {Function}
             * @default
             */
            compare : null,

            /**
             * A function that compares two objects or records using the `compare` function on the properties of each
             * objects based on the `name` of this field.
             * @config {Function}
             * @default
             * @internal
             */
            compareItems : null,

            /**
             * The property in a record's data object that contains the field's value.
             * Defaults to the field's `name`.
             * @config {String}
             * @default
             */
            dataSource : null,

            /**
             * The default value to assign to this field in a record if no value is provided.
             * @config {*} defaultValue
             */

            /**
             * Setting to `false` indicates that `null` is not a valid value.
             * @config {Boolean}
             * @default
             */
            nullable : true,

            /**
             * The value to return from {@link #function-print} for a `null` or `undefined` value.
             * @config {String}
             * @default
             */
            nullText : null,

            /**
             * The value to replace `null` when the field is not `nullable`.
             * @config {*}
             * @default
             */
            nullValue : undefined,

            /**
             * Set to `false` to exclude this field when saving records to a server.
             * @config {Boolean}
             * @default
             */
            persist : true,

            /**
             * Set to `true` for the field's set accessor to ignore attempts to set this field.
             * @config {Boolean}
             * @default
             */
            readOnly : false
        };
    }

    /**
     * The class that first defined this field. Derived classes that override a field do not change this property.
     * @member {Core.data.Model} definedBy
     * @private
     * @readonly
     */

    /**
     * The class that most specifically defined this field. Derived classes that override a field set this property to
     * themselves.
     * @member {Core.data.Model} owner
     * @private
     * @readonly
     */

    // NOTE: Since we create lots of instances, they have no life cycle (they are not destroyed) and are readonly after
    // creation, this class does not use configurable.
    construct(config) {
        const me = this;

        if (config) {
            me.name = config.name;  // assign name first for diagnostic reasons

            Object.assign(me, config);
        }

        if (me.compare) {
            // We wrap in this way to allow compareItems() to be used as an array sorter fn (which gets no "this"):
            me.compareItems = (itemA, itemB) => me.compare(itemA?.[me.name], itemB?.[me.name]);
        }
    }

    /**
     * This method transforms a data value into the desired form for storage in the record's data object.
     * @method convert
     * @param {*} value The value to convert for storage in a record.
     * @returns {*} The converted value.
     */

    /**
     * This method transforms a data value into the desired form for transmitting to a server.
     * @method serialize
     * @param {*} value The value to serialize
     * @param {Core.data.Model} record The record that contains the value being serialized.
     * @returns {*} The serialized value.
     */

    /**
     * Create getter and setter functions for the specified field name under the specified key.
     * @internal
     */
    defineAccessor(target, force) {
        const { name } = this;

        if (force || !(name in target)) {
            Reflect.defineProperty(target, name, {
                configurable : true, // To allow removing it later
                enumerable   : true,

                // no arrow functions here, need `this` to change to instance
                get() {
                    return this.get(name);
                },

                // no arrow functions here, need `this` to change to instance
                set(value) {
                    // Since the accessor is defined on a base class, we dip into the fields map for the actual
                    // calling class to get the correct field definition
                    const field = this.$meta.fields.map[name];

                    // Only set if field is read/write. Privately, we use setData to set its value
                    if (!(field && field.readOnly)) {
                        this.set(name, value);
                    }
                }
            });
        }
    }

    /**
     * Compares two values for this field and returns `true` if they are equal, and `false` if not.
     * @param {*} first The first value to compare for equality.
     * @param {*} second The second value to compare for equality.
     * @returns {Boolean} `true` if `first` and `second` are equal.
     */
    isEqual(first, second) {
        return ObjectHelper.isEqual(first, second);
    }

    /**
     * Returns the given field value as a `String`. If `value` is `null` or `undefined`, the value specified by
     * {@link #config-nullText} is returned.
     * @param {*} value The value to convert to a string.
     * @returns {String}
     */
    print(value) {
        return (value == null) ? this.nullText : this.printValue(value);
    }

    /**
     * Returns the given, non-null field value as a `String`.
     * @param {*} value The value to convert to a string (will not be `null` or `undefined`).
     * @returns {String}
     * @protected
     */
    printValue(value) {
        return String(value);
    }
}
