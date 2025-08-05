import PercentDoneColumn from './PercentDoneColumn.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';

/**
 * @module Gantt/column/PercentDoneCircleColumn
 */

/**
 * A column drawing a circular progress bar based on the `percentDone` value.
 *
 * @extends Gantt/column/PercentDoneColumn
 * @classType percentdonecircle
 */
export default class PercentDoneCircleColumn extends PercentDoneColumn {
    static get type() {
        return 'percentdonecircle';
    }

    static get isGanttColumn() {
        return true;
    }

    static get defaults() {
        return {
            htmlEncode   : false,
            autoSyncHtml : true,
            align        : 'center'
        };
    }

    //endregion

    renderer({ value }) {
        const
            roundedValue = Math.round(value),
            size = this.grid.rowHeight * 0.8;

        return `<div class="b-percentdonecircle" style="animation-delay: -${roundedValue - 0.1}s;width: ${size}px;height: ${size}px" data-value="${roundedValue}"></div>`;
    }
}

ColumnStore.registerColumnType(PercentDoneCircleColumn);
