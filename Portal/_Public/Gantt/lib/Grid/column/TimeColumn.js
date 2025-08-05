import Column from './Column.js';
import ColumnStore from '../data/ColumnStore.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module Grid/column/TimeColumn
 */

/**
 * A column that displays a time in the specified format (see {@link Core.helper.DateHelper#function-format-static} for formatting options).
 *
 * Default editor is a {@link Core.widget.TimeField TimeField}.
 *
 * @extends Grid/column/Column
 *
 * @example
 * new Grid({
 *     appendTo : document.body,
 *
 *     columns : [
 *          { type: 'time', text: 'Start time', format: 'HH:mm:ss', data: 'start' }
 *     ]
 * });
 *
 * @classType time
 * @inlineexample Grid/column/TimeColumn.js
 * @column
 */
export default class TimeColumn extends Column {

    //region Config

    static type = 'time';

    // Type to use when auto adding field
    static fieldType = 'date';

    static fields = ['format'];

    static get defaults() {
        return {
            /**
             * Time format
             * @config {String}
             * @category Common
             */
            format : 'LT',

            minWidth : 140,

            filterType : 'time'
        };
    }

    //endregion

    //region Display

    /**
     * Renderer that displays the time with the specified format. Also adds cls 'b-time-cell' to the cell.
     * @private
     */
    defaultRenderer({ value }) {
        return value ? this.formatValue(value) : '';
    }

    /**
     * Group renderer that displays the time with the specified format.
     * @private
     */
    groupRenderer({ cellElement, groupRowFor }) {
        cellElement.innerHTML = this.formatValue(groupRowFor);
    }

    //endregion

    //region Formatter

    /**
     * Used by both renderer and groupRenderer to do the actual formatting of the time
     * @private
     * @param value
     * @returns {String}
     */
    formatValue(value) {
        // Ideally we should be served a time, but if not make it easier for the user by parsing
        if (typeof value === 'string') {
            value = DateHelper.parse(value, this.format);
        }
        return DateHelper.format(value, this.format);
    }

    //endregion

    //region Getters/Setters
    /**
     * Get/Set format for time displayed in cell and editor (see {@link Core.helper.DateHelper#function-format-static} for formatting options)
     * @property {String}
     */
    set format(value) {
        const { editor } = this;

        this.set('format', value);

        if (editor) {
            editor.format = value;
        }
    }

    get format() {
        return  this.get('format');
    }

    get defaultEditor() {
        return {
            name   : this.field,
            type   : 'time',
            format : this.format
        };
    }

    //endregion
}

ColumnStore.registerColumnType(TimeColumn, true);
TimeColumn.exposeProperties();
