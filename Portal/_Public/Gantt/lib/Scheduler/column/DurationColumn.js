import ColumnStore from '../../Grid/data/ColumnStore.js';
import NumberColumn from '../../Grid/column/NumberColumn.js';
import Duration from '../../Core/data/Duration.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import '../../Core/widget/DurationField.js';

/**
 * @module Scheduler/column/DurationColumn
 */

/**
 * A column showing the task {@link Scheduler/model/TimeSpan#field-fullDuration duration}. Please note, this column
 * is preconfigured and expects its field to be of the {@link Core.data.Duration} type.
 *
 * The default editor is a {@link Core.widget.DurationField}. It parses time units, so you can enter "4d"
 * indicating 4 days duration, or "4h" indicating 4 hours, etc. The numeric magnitude can be either an integer or a
 * float value. Both "," and "." are valid decimal separators. For example, you can enter "4.5d" indicating 4.5 days
 * duration, or "4,5h" indicating 4.5 hours.
 *
 * {@inlineexample Scheduler/column/DurationColumn.js}
 * @extends Grid/column/NumberColumn
 * @classType duration
 * @column
 */
export default class DurationColumn extends NumberColumn {
    compositeField = true;

    //region Config

    static get $name() {
        return 'DurationColumn';
    }

    static get type() {
        return 'duration';
    }

    static get isGanttColumn() {
        return true;
    }

    static get fields() {
        return [
            /**
             * Precision of displayed duration, defaults to use {@link Scheduler.view.Scheduler#config-durationDisplayPrecision}.
             * Specify an integer value to override that setting, or `false` to use raw value
             * @config {Number|Boolean} decimalPrecision
             */
            { name : 'decimalPrecision', defaultValue : 1 }
        ];
    }

    static get defaults() {
        return {
            /**
             * Min value
             * @config {Number}
             */
            min : null,

            /**
             * Max value
             * @config {Number}
             */
            max : null,

            /**
             * Step size for spin button clicks.
             * @member {Number} step
             */
            /**
             * Step size for spin button clicks. Also used when pressing up/down keys in the field.
             * @config {Number}
             * @default
             */
            step : 1,

            /**
             * Large step size, defaults to 10 * `step`. Applied when pressing SHIFT and stepping either by click or
             * using keyboard.
             * @config {Number}
             * @default 10
             */
            largeStep : 0,

            field         : 'fullDuration',
            text          : 'L{Duration}',
            instantUpdate : true,
            // Undocumented, used by Filter feature to get type of the filter field
            filterType    : 'duration',

            sortable(durationEntity1, durationEntity2) {
                const
                    ms1 = durationEntity1.getValue(this.field),
                    ms2 = durationEntity2.getValue(this.field);

                return ms1 - ms2;
            }
        };
    }

    construct() {
        super.construct(...arguments);

        const sortFn = this.sortable;

        this.sortable = (...args) => sortFn.call(this, ...args);
    }

    get defaultEditor() {
        const { max, min, step, largeStep } = this;

        // Remove any undefined configs, to allow config system to use default values instead
        return ObjectHelper.cleanupProperties({
            type : 'duration',
            name : this.field,
            max,
            min,
            step,
            largeStep
        });
    }

    //endregion

    //region Internal

    get durationUnitField() {
        return `${this.field}Unit`;
    }

    roundValue(duration) {
        const
            nbrDecimals = typeof this.grid.durationDisplayPrecision === 'number' ? this.grid.durationDisplayPrecision : this.decimalPrecision,
            multiplier  = Math.pow(10, nbrDecimals),
            rounded     = Math.round(duration * multiplier) / multiplier;

        return rounded;
    }

    formatValue(duration, durationUnit) {
        if (duration instanceof Duration) {
            durationUnit = duration.unit;
            duration     = duration.magnitude;
        }

        duration = this.roundValue(duration);

        return duration + ' ' + DateHelper.getLocalizedNameOfUnit(durationUnit, duration !== 1);
    }

    //endregion

    //region Render

    defaultRenderer({ value, record, isExport }) {
        const
            type          = typeof value,
            durationValue = type === 'number' ? value : value?.magnitude,
            durationUnit  = type === 'number' ? record.getValue(this.durationUnitField) : value?.unit;

        // in case of bad input (for instance NaN, undefined or NULL value)
        if (typeof durationValue !== 'number') {
            return isExport ? '' : null;
        }

        return this.formatValue(durationValue, durationUnit);
    }

    //endregion

    // Used with CellCopyPaste as fullDuration doesn't work via record.get
    toClipboardString({ record }) {
        return record.getValue(this.field).toString();
    }

    fromClipboardString({ string, record }) {
        const duration = DateHelper.parseDuration(string, true, this.durationUnit);

        if (duration && 'magnitude' in duration) {
            return duration;
        }

        return record.fullDuration;
    }

    calculateFillValue({ value, record }) {
        return this.fromClipboardString({ string : value, record });
    }

}

ColumnStore.registerColumnType(DurationColumn);
