import '../../column/ScaleColumn.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/view/mixin/TimelineHistogramScaleColumn
 */

/**
 * Mixin of {@link Scheduler/view/TimelineHistogram} class that implements
 * {@link Scheduler/column/ScaleColumn} automatic injection and functioning.
 *
 * @mixin
 */
export default Target => class TimelineHistogramScaleColumn extends (Target) {

    static $name = 'TimelineHistogramScaleColumn';

    //region Config

    static configurable = {

        /**
         * The locked grid scale column reference.
         * @member {Scheduler.column.ScaleColumn} scaleColumn
         * @readonly
         * @category Scale column
         */

        /**
         * An object with configuration for the {@link Scheduler/column/ScaleColumn}.
         *
         * Example:
         *
         * ```javascript
         * new TimelineHistogram({
         *     scaleColumn : {
         *         width : 50
         *     },
         *     ...
         * });
         * ```
         *
         * Provide `null` to the config to get rid of the column completely:
         *
         * ```javascript
         * new TimelineHistogram({
         *     // do not add scale column
         *     scaleColumn : null,
         *     ...
         * });
         * ```
         *
         * @config {Object} scaleColumn
         * @category Scale column
         */
        scaleColumn : {},

        scalePoints : null,

        scalePointsModelField : 'scalePoints',

        calculateTopValueByScalePoints : true
    };

    updateScalePoints(scalePoints) {
        const
            me            = this,
            topScalePoint = scalePoints[scalePoints.length - 1];

        if (topScalePoint) {
            me.scaleUnit = topScalePoint.unit;

            // Applying new maximum value to the histogram.
            me.histogramWidget.topValue = me.getTopValueByScalePoints(scalePoints);
        }

        // Applying new points to the scale column
        if (me.scaleColumn) {
            me.scaleColumn.scalePoints = scalePoints;
        }
    }

    //endregion

    //region Columns

    changeColumns(columns, currentStore) {
        const
            me = this,
            scaleColumn = me.getConfig('scaleColumn');

        // No columns means destroy
        if (columns && scaleColumn) {
            const isArray = Array.isArray(columns);

            let cols = columns;

            if (!isArray) {
                cols = columns.data;
            }

            let
                scaleColumnIndex = cols?.length,
                scaleColumnConfig = scaleColumn;

            cols.some((col, index) => {
                if (col.type === 'scale') {
                    scaleColumnIndex  = index;
                    scaleColumnConfig = ObjectHelper.assign(col, scaleColumnConfig);
                    return true;
                }
            });

            // We're going to mutate this array which we do not own, so copy it first.
            cols = cols.slice();

            // Fix up the scaleColumn config in place
            cols[scaleColumnIndex] = {
                type : 'scale',
                ...scaleColumnConfig
            };

            if (isArray) {
                columns = cols;
            }
            else {
                columns.data = cols;
            }
        }

        return super.changeColumns(columns, currentStore);
    }

    updateColumns(columns, was) {
        super.updateColumns(columns, was);

        // Extract the known columns by type. Sorting will have placed them into visual order.
        if (columns) {
            this._scaleColumn = this.columns.find(c => c.isScaleColumn);
        }
    }

    onColumnsChanged({ action, changes, record : column, records }) {
        const { scaleColumn, columns } = this;
        // If someone replaces the column set (syncing leads to batch), ensure scale is always added
        if (scaleColumn && (action === 'dataset' || action === 'batch') && !columns.includes(scaleColumn)) {
            columns.add(scaleColumn, true);
        }

        super.onColumnsChanged(...arguments);
    }

    //endregion

    //region Data processing

    /**
     * A hook to convert scale point values to histogram ones.
     * In case they use different units.
     *
     * Override this method in a sub-class to implement your custom
     * application specific conversion.
     * @param {Number} value Scale point value
     * @param {String} unit Scale point unit
     * @internal
     */
    convertUnitsToHistogramValue(value, unit) {
        return value;
    }

    /**
     * A hook to convert histogram values to scale point ones.
     * In case they use different units.
     *
     * Override this method in a sub-class to implement your custom
     * application specific conversion.
     * @param {Number} value Scale point value
     * @param {String} unit Scale point unit
     * @internal
     */
    convertHistogramValueToUnits(value, unit) {
        return value;
    }

    extractHistogramDataArray(histogramData, record) {
        return histogramData;
    }

    getTopValueByScalePoints(scalePoints) {
        const
            me              = this,
            { scaleColumn } = me,
            lastPoint       = scalePoints[scalePoints.length - 1],
            { value, unit } = lastPoint;

        let rawValue = value;

        if (scaleColumn) {
            // add padding to top value
            rawValue *= 1 + (scaleColumn.scaleWidget.scaleMaxPadding || 0);
        }

        return me.convertUnitsToHistogramValue(rawValue, unit || me.scaleUnit);
    }

    processRecordRenderData(renderData) {
        renderData = super.processRecordRenderData(...arguments);

        if (this.scaleColumn) {
            const
                me = this,
                { record, histogramData, histogramConfig = {} } = renderData;

            let
                topValue = me.initialConfig.histogramWidget?.topValue,
                scalePoints = me.scalePoints || record.get(me.scalePointsModelField);

            if (!topValue) {
                // if no topValue provided but we have scalePoints
                if (scalePoints && me.calculateTopValueByScalePoints) {
                    // calculate topValue based on the max scale point
                    topValue = me.getTopValueByScalePoints(scalePoints);
                }

                // if still no topValue
                if (!topValue && histogramData) {
                    const histogramWidget = renderData.histogramWidget || me.histogramWidget;

                    ObjectHelper.assign(histogramWidget, histogramConfig);

                    // get top value based on histogramData
                    topValue = histogramWidget.getDataTopValue(histogramData);

                    scalePoints = [{

                        value : me.convertHistogramValueToUnits(topValue, me.scaleUnit),
                        text  : me.convertHistogramValueToUnits(topValue, me.scaleUnit)
                    }];

                    topValue += me.scaleColumn.scaleWidget.scaleMaxPadding * topValue;
                }

                renderData.scaleWidgetConfig = { scalePoints };
                renderData.histogramConfig = { ...histogramConfig, topValue };
            }
        }

        return renderData;
    }

    //endregion

    //region Render

    /**
     * Group feature hook triggered by the feature to render group headers
     * @param {Object} renderData
     * @internal
     */
    buildGroupHeader(renderData) {
        if (renderData.column === this.scaleColumn) {
            return this.scaleColumn.renderer(renderData);
        }

        return super.buildGroupHeader(...arguments);
    }

    beforeRenderCell(renderData) {
        if (this.scaleColumn && renderData.column === this.scaleColumn) {
            renderData.histogramData = this.getRecordHistogramData(renderData.record);

            // If data is read apply prepared render data
            if (!ObjectHelper.isPromise(renderData.histogramData)) {
                Object.assign(renderData, this._recordRenderData);
            }
        }

        return super.beforeRenderCell(...arguments);
    }

    /**
     * Renders record scale column content.
     * @param {Core.data.Model} record Record to render scale for
     * @param {Object} [renderData]
     * @category Scale column
     */
    renderRecordScale(record, renderData) {
        if (this.scaleColumn) {
            const
                row         = this.getRowFor(record),
                cellElement = row?.getCell(this.scaleColumn.id);

            if (cellElement) {
                row.renderCell(cellElement);
            }
        }
    }

    get widgetClass() {}

    //endregion
};
