import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import Scale from '../../Core/widget/graph/Scale.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/column/ScaleColumn
 */

/**
 * An object representing a point on the scale displayed by {@link Scheduler.column.ScaleColumn}.
 *
 * @typedef {Object} ScalePoint
 * @property {Number} value Point value
 * @property {String} unit Point value unit
 * @property {String} text Point text label
 */

/**
 * A specialized column showing a graduated scale from a defined array of values
 * and labels. This column is used in the {@link Scheduler.view.TimelineHistogram} and is not editable. Normally
 * you should not need to interact with this class directly.
 *
 * @extends Grid/column/Column
 * @classType scale
 * @column
 */
export default class ScaleColumn extends Column {

    //region Config

    static $name = 'ScaleColumn';

    static type = 'scale';

    static isScaleColumn = true;

    static get fields() {
        return [
            'scalePoints'
        ];
    }

    static get defaults() {
        return {
            text            : '\xa0',
            width           : 40,
            minWidth        : 40,
            field           : 'scalePoints',
            cellCls         : 'b-scale-cell',
            editor          : false,
            sortable        : false,
            groupable       : false,
            filterable      : false,
            alwaysClearCell : false,
            scalePoints     : null
        };
    }

    //endregion

    //region Constructor/Destructor

    onDestroy() {
        this.scaleWidget.destroy();
    }

    //endregion

    //region Internal

    set width(width) {
        super.width = width;
        this.scaleWidget.width = width;
    }

    get width() {
        return super.width;
    }

    applyValue(useProp, key, value) {
        // pass value to scaleWidget
        if (key === 'scalePoints') {
            this.scaleWidget[key] = value;
        }

        return super.applyValue(...arguments);
    }

    buildScaleWidget() {
        const me = this;

        const scaleWidget = new Scale({
            owner         : me.grid,
            appendTo      : me.grid.floatRoot,
            cls           : 'b-hide-offscreen',
            align         : 'right',
            scalePoints   : me.scalePoints,
            monitorResize : false
        });

        Object.defineProperties(scaleWidget, {
            width : {
                get() {
                    return me.width;
                },
                set(width) {
                    this.element.style.width = `${width}px`;
                    this._width = me.width;
                }
            },
            height : {
                get() {
                    return this._height;
                },
                set(height) {
                    this.element.style.height = `${height}px`;
                    this._height = height;
                }
            }
        });

        scaleWidget.width = me.width;

        return scaleWidget;
    }

    get scaleWidget() {
        const me = this;

        if (!me._scaleWidget) {
            me._scaleWidget = me.buildScaleWidget();
        }

        return me._scaleWidget;
    }

    //endregion

    //region Render

    renderer({ cellElement, value, scaleWidgetConfig, scaleWidget = this.scaleWidget }) {
        ObjectHelper.assign(scaleWidget, {
            scalePoints : value || this.scalePoints,
            height      : this.grid.rowHeight
        }, scaleWidgetConfig);

        scaleWidget.refresh();

        // Clone the scale widget element since every row is supposed to have
        // the same scale settings
        const scaleCloneElement = scaleWidget.element.cloneNode(true);
        scaleCloneElement.removeAttribute('id');
        scaleCloneElement.classList.remove('b-hide-offscreen');

        cellElement.innerHTML = '';
        cellElement.appendChild(scaleCloneElement);
    }

    //endregion

}

ColumnStore.registerColumnType(ScaleColumn);
