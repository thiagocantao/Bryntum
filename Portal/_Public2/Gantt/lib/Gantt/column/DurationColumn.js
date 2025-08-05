import ColumnStore from '../../Grid/data/ColumnStore.js';
import NumberColumn from '../../Grid/column/NumberColumn.js';
import DateHelper from '../../Common/helper/DateHelper.js';
import '../../Common/widget/DurationField.js';

/**
 * @module Gantt/column/DurationColumn
 */

/**
 * A column showing the task {@link Scheduler/model/TimeSpan#property-fullDuration duration}. The editor of this column understands the time units,
 * so user can enter "4d" indicating 4 days duration, or "4h" indicating 4 hours, etc.
 *
 * @extends Grid/column/NumberColumn
 * @classType duration
 */
export default class DurationColumn extends NumberColumn {
    static get type() {
        return 'duration';
    }

    static get isGanttColumn() {
        return true;
    }

    get durationUnitField() {
        return `${this.field}Unit`;
    }

    //region Config

    static get fields() {
        return [
            /**
             * Precision of displayed duration, defaults to use {@link Gantt.view.Gantt#config-durationDisplayPrecision}.
             * Specify a integer value to override that setting, or `false` to use raw value
             * @config {Number|Boolean} decimalPrecision
             */
            { name : 'decimalPrecision', defaultValue : null }
        ];
    }

    static get defaults() {
        return {
            field         : 'fullDuration',
            text          : 'Duration',
            min           : 0,
            step          : 1,
            instantUpdate : true
        };
    }

    //endregion

    defaultRenderer({ record : task }) {
        const value       = task[this.field],
            type          = typeof value,
            durationValue = type === 'number' ? value : value && value.magnitude;

        // in case of bad input (for instance NaN, undefined or NULL value)
        if (typeof durationValue !== 'number') return;

        switch (type) {
            // We're using eg 'lag' or 'duration' which is the magnitude part
            case 'number':
                return this.round(durationValue) + ' ' + DateHelper.getLocalizedNameOfUnit(task[this.durationUnitField], value !== 1);
            // We're using eg 'fullLag' or 'fullDuration' which is the two part encapsulated value
            case 'object':
                return this.round(durationValue) + ' ' + DateHelper.getLocalizedNameOfUnit(value.unit, value.magnitude !== 1);
        }
    }

    round(value) {
        let { decimalPrecision } = this;

        if (decimalPrecision === false) {
            return value;
        }

        if (decimalPrecision == null) {
            decimalPrecision = this.grid.durationDisplayPrecision || 1;
        }

        // Prefer this way over toFixed() to not display "unused" decimal places
        const multiplier = Math.pow(10, decimalPrecision);

        return Math.round(value * multiplier) / multiplier;
    }

    get defaultEditor() {
        return {
            type : 'duration',
            name : this.field
        };
    }

    // Can only edit leafs
    canEdit(record) {
        return record.isLeaf;
    }
}

ColumnStore.registerColumnType(DurationColumn);
