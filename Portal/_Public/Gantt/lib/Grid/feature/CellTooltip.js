import Objects from '../../Core/helper/util/Objects.js';
import Tooltip from '../../Core/widget/Tooltip.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';

/**
 * @module Grid/feature/CellTooltip
 */

/**
 * Displays a tooltip when hovering cells.
 *
 * {@inlineexample Grid/feature/CellTooltip.js}
 *
 * To show contents when hovering a cell, you can specify a global {@link #config-tooltipRenderer} function for the
 * feature, you can also define a {@link Grid.column.Column#config-tooltipRenderer} for individual columns.
 *
 * ```javascript
 * // Column with its own tooltip renderer
 * {
 *   text            : 'Name',
 *   field           : 'name',
 *   tooltipRenderer : ({ record }) => `My name is\xa0<b>${record.name}</b>`
 * }
 * ```
 *
 * Configuration properties passed into this feature are used to configure the {@link Core.widget.Tooltip} instance
 * used.
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * ## Showing async content
 * Showing remotely loaded content is super easy using the {@link #config-tooltipRenderer}:
 *
 * ```javascript
 * // Async tooltip with some custom settings
 * const grid = new Grid({
 *   features: {
 *     cellTooltip: {
 *       // Time that mouse needs to be over cell before tooltip is shown
 *       hoverDelay : 4000,
 *       // Time after mouse out to hide the tooltip, 0 = instantly
 *       hideDelay  : 0,
 *       // Async tooltip renderer, return a Promise which yields the text content
 *       tooltipRenderer({ record, tip }) {
 *         return fetch(`tip.php?id=${record.id}`).then(response => response.text())
 *       }
 *     }
 *   }
 * });
 * ```
 *
 * @extends Core/mixin/InstancePlugin
 * @extendsconfigs Core/widget/Tooltip
 * @demo Grid/celltooltip
 * @classtype cellTooltip
 * @feature
 */
export default class CellTooltip extends InstancePlugin {
    //region Config

    static $name = 'CellTooltip';

    static configurable = {
        /**
         * Function called to generate the HTML content for the cell tooltip.
         * The function should return a string (your HTML), or a Promise yielding a string (for remotely loaded
         * content)
         * @prp {Function}
         * @param {Object} context
         * @param {HTMLElement} context.cellElement The cell element
         * @param {Core.data.Model} context.record The row record
         * @param {Grid.column.Column} context.column The column
         * @param {Core.widget.Tooltip} context.tip The Tooltip instance
         * @param {Grid.feature.CellTooltip} context.cellTooltip The feature
         * @param {Event} context.event The raw DOM event
         * @returns {String|Promise}
         */
        tooltipRenderer : null
    };

    //endregion

    // region Init

    construct(grid, config) {
        super.construct(grid, this.processConfig(config));
    }

    initTip() {
        const me = this;

        me.tip = Tooltip.new({
            forElement        : me.client.element,
            forSelector       : '.b-grid-row:not(.b-group-row) .b-grid-cell, .b-grid-merged-cells',
            hoverDelay        : 1000,
            trackMouse        : false,
            cls               : 'b-celltooltip-tip',
            getHtml           : me.getTooltipContent.bind(me),
            internalListeners : {
                pointerOver : 'onPointerOver',
                thisObj     : me
            },
            // eslint-disable-next-line bryntum/no-listeners-in-lib
            listeners : me.configuredListeners
        }, me.initialConfig);

        me.relayEvents(me.tip, ['beforeShow', 'show']);
    }

    onPointerOver({ target }) {
        const column = this.client.getColumnFromElement(target);

        // Veto onPointerOver if column's tooltipRenderer is false
        return column.tooltipRenderer !== false && Boolean(column.tooltipRenderer || this.tooltipRenderer);
    }

    // CellTooltip feature handles special config cases, where user can supply a function to use as tooltipRenderer
    // instead of a normal config object
    processConfig(config) {
        if (typeof config === 'function') {
            return {
                tooltipRenderer : config
            };
        }

        return config;
    }

    // override setConfig to process config before applying it (used mainly from ReactGrid)
    setConfig(config) {
        super.setConfig(this.processConfig(config));
    }

    doDestroy() {
        this.tip && this.tip.destroy();
        super.doDestroy();
    }

    doDisable(disable) {
        if (!disable) {
            this.initTip();
        }
        else if (this.tip) {
            this.tip.destroy();
            this.tip = null;
        }

        super.doDisable(disable);
    }

    //endregion

    //region Content

    /**
     * Called from Tooltip to populate it with html.
     * @private
     */
    getTooltipContent({ tip, activeTarget : cellElement, event }) {
        const
            me     = this,
            record = me.client.getRecordFromElement(cellElement),
            column = me.client.getColumnFromElement(cellElement),
            arg    = { cellElement, record, column, event, tip, cellTooltip : me };

        let result;

        // If we have not changed context, we should not change content, unless we have a custom target selector (element within the cell)
        if (!me.forSelector && record === me.lastRecord && record.generation === me.lastRecordGeneration && column === me.lastColumn) {
            return me.tip._html;
        }

        me.lastRecord = record;
        me.lastRecordGeneration = record.generation;
        me.lastColumn = column;

        // first, use columns tooltipRenderer if any
        if (column.tooltipRenderer) {
            result = column.tooltipRenderer(arg);
        }
        // secondly, try feature's renderer (specifying column.tooltipRenderer as false prevents tooltip in that column)
        else if (me.tooltipRenderer && column.tooltipRenderer !== false) {
            result = me.tooltipRenderer(arg);
        }

        // No caching of async requests
        if (Objects.isPromise(result)) {
            me.lastRecord = me.lastRecordGeneration = me.lastColumn = null;
        }

        // Tip should hide if no content is available
        if (!result) {
            tip.hide();
        }

        return result;
    }

    //endregion
}

GridFeatureManager.registerFeature(CellTooltip);
