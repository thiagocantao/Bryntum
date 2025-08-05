import Menu from '../../Core/widget/Menu.js';
import Column from './Column.js';
import  '../../Core/widget/ColorPicker.js';
import ColumnStore from '../data/ColumnStore.js';

/**
 * @module Grid/column/ColorColumn
 */

/**
 * A column that displays color values (built-in color classes or CSS colors) as a colored element similar to
 * the {@link Core.widget.ColorField}. When the user clicks the element, a {@link Core.widget.ColorPicker} lets the user
 * select from a range of colors.
 *
 * {@inlineexample Grid/column/ColorColumn.js}
 *
 * ```javascript
 * new Grid({
 *    columns : [
 *       {
 *          type   : 'color',
 *          field  : 'color',
 *          text   : 'Color'
 *       }
 *    ]
 * });
 * ```
 *
 * @extends Grid/column/Column
 * @classType color
 */
export default class ColorColumn extends Column {
    static $name = 'ColorColumn';

    static type = 'color';

    static fields = [
        { name : 'colorEditorType', defaultValue : 'colorpicker' },

        /**
         * Array of CSS color strings to be able to chose from. This will override the
         * {@link Core.widget.ColorPicker#config-colors pickers default colors}.
         *
         * Provide an array of string CSS colors:
         * ```javascript
         * new Grid({
         *    columns : [
         *       {
         *          type   : 'color',
         *          field  : 'color',
         *          text   : 'Color',
         *          colors : ['#00FFFF', '#F0FFFF', '#89CFF0', '#0000FF', '#7393B3']
         *       }
         *    ]
         * });
         * ```
         * @prp {String[]}
         */
        'colors',

        /**
         * Adds an option in the picker to set no background color
         * @prp {Boolean}
         * @default true
         */
        { name : 'addNoColorItem', defaultValue : true }
    ];

    static defaults = {
        align  : 'center',
        editor : null
    };

    construct() {
        super.construct(...arguments);

        const
            me       = this,
            { grid } = me;

        me.menu = new Menu({
            owner             : grid,
            rootElement       : grid.rootElement,
            autoShow          : false,
            align             : 't50-b50',
            anchor            : true,
            internalListeners : {
                hide() {
                    me.picker.navigator.activeItem = null;
                    delete me._editingRecord;
                }
            },
            items : [
                Object.assign({
                    type           : me.colorEditorType,
                    ref            : 'list',
                    addNoColorItem : me.addNoColorItem,
                    colorSelected({ color }) {
                        me._editingRecord?.set(me.field, color);
                        me.menu.hide();
                    }
                }, me.colors?.length ? { colors : me.colors } : {})
            ]
        });
    }

    applyValue(useProp, field, value) {
        if (!this.isConstructing) {
            const { picker } = this;

            if (field === 'colors') {
                picker.colors = value;
            }
            else if (field === 'addNoColorItem') {
                picker.addNoColorItem = value;
            }
        }

        super.applyValue(...arguments);
    }

    get picker() {
        return this.menu.widgetMap.list;
    }

    renderer({ value }) {
        let colorClass      = 'b-empty',
            backgroundColor = value;

        if (value) {
            const colorClassName = this.picker.getColorClassName(value);

            if (colorClassName) {
                colorClass      = colorClassName;
                backgroundColor = null;
            }
            else {
                colorClass = '';
            }
        }

        return {
            className : 'b-color-cell-inner ' + colorClass,
            style     : {
                backgroundColor
            },
            'data-btip' : value
        };
    }

    onCellClick({ grid, record, target }) {
        if (target.classList.contains('b-color-cell-inner') && !this.readOnly &&
            !grid.readOnly && !record.isSpecialRow && !record.readOnly
        ) {
            const
                { picker, menu } = this,
                value            = record.get(this.field);

            this._editingRecord = record;

            picker.deselectAll();
            picker.select(value);
            picker.refresh();
            menu.showBy(target);
        }
    }
}

ColumnStore.registerColumnType(ColorColumn);
