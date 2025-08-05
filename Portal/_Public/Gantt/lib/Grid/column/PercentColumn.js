import NumberColumn from './NumberColumn.js';
import ColumnStore from '../data/ColumnStore.js';

/**
 * @module Grid/column/PercentColumn
 */

/**
 * A column that display a basic progress bar.
 *
 * {@inlineexample Grid/column/PercentColumn.js}
 *
 * ```javascript
 * new Grid({
 *     appendTo : document.body,
 *
 *     columns : [
 *         { type: 'percent', text: 'Progress', data: 'progress' }
 *     ]
 * });
 * ```
 *
 * Default editor is a {@link Core.widget.NumberField NumberField}.
 *
 * @extends Grid/column/NumberColumn
 * @classType percent
 * @column
 */
export default class PercentColumn extends NumberColumn {

    static type = 'percent';

    // Type to use when auto adding field
    static fieldType = 'number';

    static fields = ['showValue', 'lowThreshold'];

    static get defaults() {
        return {
            min : 0,
            max : 100,

            /**
             * Set to `true` to render the number value inside the bar, for example `'15%'`.
             * @config {Boolean}
             * @default
             * @category Rendering
             */
            showValue : false,

            /**
             * When below this percentage the bar will have `b-low` CSS class added. By default it turns the bar red.
             * @config {Number}
             * @default
             * @category Rendering
             */
            lowThreshold : 20,

            htmlEncode      : false,
            searchable      : false,
            summaryRenderer : ({ sum }) => `${sum}%`,
            fitMode         : false
        };
    }

    constructor(config, store) {
        super(...arguments);

        this.internalCellCls = 'b-percent-bar-cell';
    }

    /**
     * Renderer that displays a progress bar in the cell. If you create a custom renderer, and want to include the
     * default markup you can call `defaultRenderer` from it.
     *
     * ```javascript
     * new Grid({
     *     columns: [
     *         {
     *             type: 'percent',
     *             text : 'Percent',
     *             field : 'percent',
     *             renderer({ value }) {
     *                 const domConfig = this.defaultRenderer();
     *
     *                 if (value > 100) {
     *                     domConfig.className = b-percent-bar-outer over-allocated';
     *                 }
     *
     *                 return domConfig;
     *             }
     *         }
     *     ]
     * }
     * ```
     *
     * @param {Object} rendererData The data object passed to the renderer
     * @param {Number} rendererData.value The value to display
     * @returns {DomConfig} DomConfig object representing the default markup for the cells content
     */
    defaultRenderer({ value }) {
        value = value || 0;

        return {
            className       : 'b-percent-bar-outer',
            role            : 'progressbar',
            'aria-Valuemin' : 0,
            'aria-Valuemax' : 100,
            'aria-Valuenow' : value,
            tabIndex        : 0,
            children        : [
                {
                    tag       : 'div',
                    className : {
                        'b-percent-bar' : 1,
                        'b-zero'        : value === 0,
                        'b-low'         : value < this.lowThreshold
                    },
                    style : {
                        width : value + '%'
                    },
                    children : [
                        this.showValue ? {
                            tag  : 'span',
                            text : value + '%'
                        } : undefined
                    ]
                }
            ]
        };
    }

    // Null implementation because the column width drives the width of its content.
    // So the concept of sizing to content is invalid here.
    resizeToFitContent() {}
}

PercentColumn.sum = 'average';

ColumnStore.registerColumnType(PercentColumn, true);
