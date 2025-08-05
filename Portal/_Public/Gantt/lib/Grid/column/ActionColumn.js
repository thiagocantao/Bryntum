import Column from './Column.js';
import ColumnStore from '../data/ColumnStore.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import Tooltip from '../../Core/widget/Tooltip.js';

/**
 * @module Grid/column/ActionColumn
 */

/**
 * Config object for an action in an ActionColumn.
 * @typedef {Object} ActionConfig
 * @property {String} cls CSS Class for action icon
 * @property {Function|String|TooltipConfig} tooltip Tooltip text, or a config object which can reconfigure the shared
 * tooltip by setting boolean, numeric and string config values, or a function to return the tooltip text, passed the
 * row's `record`
 * @property {Function|Boolean} visible Boolean to define the action icon visibility or a callback function, passed the
 * row's `record`, to change it dynamically
 * @property {Function} onClick Callback to handle click action item event, passed the row's `record`
 * @property {Boolean} showForGroup Set to true to have action icon visible in group headers only when using the `group`
 * feature
 * @property {Function|String} renderer A render function, or the name of a function in the Grid's ownership tree used
 * to define the action element. Passed the row's `record`, expected to return an HTML string or a DOM config object.
 * **Note**: when specified, the `cls` action config is ignored. Make sure you add an action icon manually, for example:
 * ```javascript
 * {
 *      type    : 'action',
 *      text    : 'Increase amount',
 *      actions : [{
 *          cls      : 'b-fa b-fa-plus', // this line will be ignored
 *          renderer : ({ record }) => '<i class="b-action-item b-fa b-fa-plus"></i> ' + record.name,
 *          onClick  : ({ record }) => {}
 *      }]
 * }
 * ```
 *
 * or
 *
 * ```javascript
 * {
 *      type    : 'action',
 *      text    : 'Increase amount',
 *      actions : [{
 *          cls      : 'b-fa b-fa-plus', // this line will be ignored
 *          renderer : 'up.renderAction' // Defined on the Grid
 *          onClick  : ({ record }) => {}
 *      }]
 * }
 * ```
 */

/**
 * A column that displays actions as clickable icons in the cell.
 *
 * {@inlineexample Grid/column/ActionColumn.js}
 *
 * ```javascript
 * new TreeGrid({
 *     appendTo : document.body,
 *     columns  : [{
 *         type    : 'action',
 *         text    : 'Increase amount',
 *         actions : [{
 *             cls      : 'b-fa b-fa-plus',
 *             renderer : ({ action, record }) => `<i class="b-action-item ${action.cls} b-${record.enabled ? "green" : "red"}-class"></i>`,
 *             visible  : ({ record }) => record.canAdd,
 *             tooltip  : ({ record }) => `<p class="b-nicer-than-default">Add to ${record.name}</p>`,
 *             onClick  : ({ record }) => console.log(`Adding ${record.name}`)
 *         }, {
 *             cls     : 'b-fa b-fa-pencil',
 *             tooltip : 'Edit note',
 *             onClick : ({ record }) => console.log(`Editing ${record.name}`)
 *         }]
 *     }]
 * });
 * ```
 *
 * Actions may be placed in {@link Grid/feature/Group} headers, by setting `action.showForGroup` to `true`. Those
 * actions will not be shown on normal rows.
 *
 * @extends Grid/column/Column
 * @classType action
 * @column
 */
export default class ActionColumn extends Column {

    static type = 'action';

    static fields = [
        /**
         * An array of action config objects, see {@link #typedef-ActionConfig} for details.
         *
         * ```javascript
         * new Grid({
         *     columns  : [{
         *         type    : 'action',
         *         text    : 'Actions',
         *         actions : [{
         *             cls      : 'b-fa b-fa-plus',
         *             visible  : ({ record }) => record.canAdd,
         *             onClick  : ({ record }) => console.log(`Adding ${record.name}`)
         *         }, {
         *             cls     : 'b-fa b-fa-pencil',
         *             tooltip : 'Edit note',
         *             onClick : ({ record }) => console.log(`Editing ${record.name}`)
         *         }]
         *     }]
         * });
         * ```
         *
         * @config {ActionConfig[]} actions List of action configs
         * @category Common
         */
        { name : 'actions', type : 'array' },

        /**
         * Set true to hide disable actions in this column if the grid is {@link Core.widget.Widget#config-readOnly}
         * @config {Boolean} disableIfGridReadOnly
         * @default
         * @category Common
         */
        { name : 'disableIfGridReadOnly', defaultValue : false }
    ];

    static defaults = {
        /**
         * Filtering by action column is not supported by default, because it has a custom renderer and uses HTML with icons as content.
         * @config {Boolean} filterable
         * @default false
         * @category Interaction
         * @hide
         */
        filterable : false,

        /**
         * Grouping by action column is not supported by default, because it has a custom renderer and uses HTML with icons as content.
         * @config {Boolean} groupable
         * @default false
         * @category Interaction
         * @hide
         */
        groupable : false,

        /**
         * Sorting by action column is not supported by default, because it has a custom renderer and uses HTML with icons as content.
         * @config {Boolean} sortable
         * @default false
         * @category Interaction
         * @hide
         */
        sortable : false,

        /**
         * Editor for action column is not supported by default, because it has a custom renderer and uses HTML with icons as content.
         * @config {Boolean} editor
         * @default false
         * @category Interaction
         * @hide
         */
        editor : false,

        /**
         * Searching by action column is not supported by default, because it has a custom renderer and uses HTML with icons as content.
         * @config {Boolean} searchable
         * @default false
         * @category Interaction
         * @hide
         */
        searchable : false,

        /**
         * By default, for action column this flag is switched to `true`, because the content of this column is always HTML.
         * @config {Boolean} htmlEncode
         * @default false
         * @category Misc
         * @hide
         */
        htmlEncode : false,

        /**
         * Set to `true` to allow the column to being drag-resized when the ColumnResize plugin is enabled.
         * @config {Boolean} resizable
         * @default false
         * @category Interaction
         */
        resizable : false,

        /**
         * Column minimal width. If value is Number then minimal width is in pixels.
         * @config {Number|String} minWidth
         * @default 30
         * @category Layout
         */
        minWidth : 30
    };

    get groupHeaderReserved() {
        return true;
    }

    construct(config, store) {
        const me = this;

        super.construct(...arguments);

        // use auto-size only as default behaviour
        if (!config.width && !config.flex) {
            me.grid.ion({ paint : 'updateAutoWidth', thisObj : me });
        }

        if (me.disableIfGridReadOnly) {
            me.grid.element.classList.add('b-actioncolumn-readonly');
        }

        // If column is cloned, renderer is already set up
        if (me.renderer !== me.internalRenderer) {
            me.externalRenderer = me.renderer;
            me.renderer = me.internalRenderer;
        }
    }

    /**
     * Renderer that displays action icon(s) in the cell.
     * @private
     */
    internalRenderer({ grid, column, record, callExternalRenderer = true }) {
        const
            inGroupTitle = record && ('groupRowFor' in record.meta),
            { subGrid }  = column;

        if (callExternalRenderer) {
            this.externalRenderer?.(...arguments);
        }

        return {
            className : { 'b-action-ct' : 1 },
            children  : column.actions?.map((actionConfig, index) => {
                if ('visible' in actionConfig) {
                    if ((typeof actionConfig.visible === 'function') && actionConfig.visible({ record }) === false) {
                        return '';
                    }
                    if (actionConfig.visible === false) {
                        return '';
                    }
                }

                // check if an action allowed to be shown in case of using grouping
                if ((inGroupTitle && !actionConfig.showForGroup) || (!inGroupTitle && actionConfig.showForGroup)) {
                    return '';
                }

                const
                    {
                        tooltip,
                        renderer
                    }    = actionConfig,
                    btip = (typeof tooltip === 'function' || tooltip?.startsWith?.('up.')) ? subGrid.callback(tooltip, subGrid, [{ record }]) : tooltip || '';

                // handle custom renderer if it is specified
                if (renderer) {
                    const customRendererData = subGrid.callback(renderer, subGrid, [{
                        index,
                        record,
                        column,
                        tooltip : btip,
                        action  : actionConfig
                    }]);

                    // take of set data-index to make onClick handler work stable
                    if (typeof customRendererData === 'string') {
                        return {
                            tag     : 'span',
                            dataset : {
                                ...Tooltip.encodeConfig(btip),
                                index
                            },
                            html : customRendererData
                        };
                    }
                    else {
                        customRendererData.dataset = customRendererData.dataset || {};
                        customRendererData.dataset.index = index;
                        return customRendererData;
                    }
                }
                else {
                    return {
                        tag     : 'button',
                        dataset : {
                            ...Tooltip.encodeConfig(btip),
                            index
                        },
                        'aria-label' : btip,
                        className    : {
                            'b-tool'           : 1,
                            'b-action-item'    : 1,
                            [actionConfig.cls] : actionConfig.cls
                        }
                    };
                }
            })
        };
    }

    /**
     * Handle icon click and call action handler.
     * @private
     */
    onCellClick({ column, record, target }) {
        if (column !== this || !target.classList.contains('b-action-item')) {
            return;
        }

        let actionIndex = target.dataset.index;
        // index may be set in a parent node if user used an html string in his custom renderer
        // and we take care to set this property to support onClick handler
        if (!actionIndex) {
            actionIndex = target.parentElement.dataset && target.parentElement.dataset.index;
        }

        const
            action        = column.actions?.[actionIndex],
            actionHandler = action?.onClick;

        if (actionHandler) {
            this.callback(actionHandler, column, [{ record, action, target }]);
        }
    }

    /**
     * Update width for actions column to fit content.
     * @private
     */
    updateAutoWidth() {
        const
            me           = this,
            groupActions = [],
            {
                actions : oldActions
            }            = me;

        // header may be disabled, in that case we won't be able to calculate the width properly
        if (!me.element) {
            return;
        }

        const actions = me.actions = [];

        // collect group and non group actions to check length later
        oldActions?.forEach(actionOriginal => {
            const action = { ...actionOriginal };

            // remove possible visibility condition to make sure an action will exists in test HTML
            delete action.visible;
            // group actions shows in different row and never together with non group
            if (action.showForGroup) {
                delete action.showForGroup;
                groupActions.push(action);
            }
            else {
                actions.push(action);
            }
        });

        // use longest actions length to calculate column width
        if (groupActions.length > actions.length) {
            me._actions = groupActions;
        }

        const actionsHtml = DomHelper.createElement(me.internalRenderer({ column : me, callExternalRenderer : false })).outerHTML;

        me.width = DomHelper.measureText(actionsHtml, me.element, true, me.element.parentElement);
        me.actions = oldActions;
    }
}

ColumnStore.registerColumnType(ActionColumn);
ActionColumn.exposeProperties();
