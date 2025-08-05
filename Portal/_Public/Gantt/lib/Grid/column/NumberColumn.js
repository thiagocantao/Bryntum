import Column from './Column.js';
import ColumnStore from '../data/ColumnStore.js';
import NumberFormat from '../../Core/helper/util/NumberFormat.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Grid/column/NumberColumn
 */

/**
 * A column for showing/editing numbers.
 *
 * Default editor is a {@link Core.widget.NumberField NumberField}.
 *
 * ```javascript
 * new Grid({
 *     appendTo : document.body,
 *     columns : [
 *         { type: 'number', min: 0, max : 100, field: 'score' }
 *     ]
 * });
 * ```
 *
 * Provide a {@link Core/helper/util/NumberFormat} config as {@link #config-format} to be able to show currency. For
 * example:
 * ```javascript
 * new Grid({
 *     appendTo : document.body,
 *     columns : [
 *         {
 *             type   : 'number',
 *             format : {
 *                style    : 'currency',
 *                currency : 'USD'
 *             }
 *         }
 *     ]
 * });
 * ```
 *
 * @extends Grid/column/Column
 * @classType number
 * @inlineexample Grid/column/NumberColumn.js
 * @column
 */
export default class NumberColumn extends Column {
    //region Config

    static type = 'number';

    // Type to use when auto adding field
    static fieldType = 'number';

    static fields = [
        'format',

        /**
         * The minimum value for the field used during editing.
         * @config {Number} min
         * @category Common
         */
        'min',

        /**
         * The maximum value for the field used during editing.
         * @config {Number} max
         * @category Common
         */
        'max',

        /**
         * Step size for the field used during editing.
         * @config {Number} step
         * @category Common
         */
        'step',

        /**
         * Large step size for the field used during editing. In effect for `SHIFT + click/arrows`
         * @config {Number} largeStep
         * @category Common
         */
        'largeStep',

        /**
         * Unit to append to displayed value.
         * @config {String} unit
         * @category Common
         */
        'unit'
    ];

    static get defaults() {
        return {
            filterType : 'number',

            /**
             * The format to use for rendering numbers.
             *
             * By default, the locale's default number formatter is used. For `en-US`, the
             * locale default is a maximum of 3 decimal digits, using thousands-based grouping.
             * This would render the number `1234567.98765` as `'1,234,567.988'`.
             *
             * @config {String|NumberFormatConfig}
             */
            format : ''
        };
    }

    //endregion

    //region Init

    get defaultEditor() {
        const { format, name, max, min, step, largeStep, align } = this;

        // Remove any undefined configs, to allow config system to use default values instead
        return ObjectHelper.cleanupProperties({
            type      : 'numberfield',
            format,
            name,
            max,
            min,
            step,
            largeStep,
            textAlign : align
        });
    }

    get formatter() {
        const
            me         = this,
            { format } = me;

        let formatter = me._formatter;

        if (!formatter || me._lastFormat !== format) {
            me._formatter = formatter = NumberFormat.get(me._lastFormat = format);
        }

        return formatter;
    }

    formatValue(value) {
        if (value != null) {
            value = this.formatter.format(value);

            if (this.unit) {
                value = `${value}${this.unit}`;
            }
        }
        return value ?? '';
    }

    /**
     * Renderer that displays a formatted number in the cell. If you create a custom renderer, and want to include the
     * formatted number you can call `defaultRenderer` from it.
     *
     * ```javascript
     * new Grid({
     *     columns: [
     *         {
     *             type   : 'number',
     *             text   : 'Total cost',
     *             field  : 'totalCost',
     *             format : {
     *                 style    : 'currency',
     *                 currency : 'USD'
     *             },
     *             renderer({ value }) {
     *                  return `Total cost: ${this.defaultRenderer({ value })}`;
     *             }
     *         }
     *     ]
     * }
     * ```
     *
     * @param {Object} rendererData The data object passed to the renderer
     * @param {Number} rendererData.value The value to display
     * @returns {String} Formatted number
     */
    defaultRenderer({ value }) {
        return this.formatValue(value);
    }
}

ColumnStore.registerColumnType(NumberColumn, true);
NumberColumn.exposeProperties();
