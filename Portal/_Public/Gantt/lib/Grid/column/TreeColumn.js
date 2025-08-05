import Column from './Column.js';
import ColumnStore from '../data/ColumnStore.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module Grid/column/TreeColumn
 */

let currentParentHasIcon = false;

/**
 * A column that displays a tree structure when using the {@link Grid.feature.Tree tree} feature.
 *
 * Default editor is a {@link Core.widget.TextField TextField}.
 *
 * TreeColumn provides configs to define icons for {@link #config-expandIconCls expanded} /
 * {@link #config-collapseIconCls collapsed} nodes, {@link #config-expandedFolderIconCls expanded folder} /
 * {@link #config-collapsedFolderIconCls collapsed folder} nodes and {@link #config-leafIconCls leaf} nodes.
 *
 * When the TreeColumn renders its cells, it will look for two special fields {@link Grid.data.GridRowModel#field-href}
 * and {@link Grid.data.GridRowModel#field-target}. Specifying `href` will produce a link for the TreeNode,
 * and `target` will have the same meaning as in an A tag:
 *
 * ```javascript
 * {
 *    id        : 1,
 *    name      : 'Some external link'
 *    href      : '//www.website.com",
 *    target    : '_blank"
 * }
 * ```
 *
 * ## Snippet
 * ```javascript
 * new TreeGrid({
 *     appendTo : document.body,
 *
 *     columns : [
 *          { type: 'tree', field: 'name' }
 *     ]
 * });
 * ```
 *
 * {@inlineexample Grid/column/TreeColumn.js}
 *
 * ## Cell renderers
 *
 * You can affect the contents and styling of cells in this column using a
 * {@link Grid.column.TreeColumn#config-renderer} function.
 *
 * ```javascript
 * const grid = new Grid({
 *   columns : [{
 *       type       : 'tree',
 *       field      : 'name',
 *       text       : 'Name',
 *       renderer({ value, record }) {
 *         return `${value} (${record.childLevel})`
 *       }
 *     }]
 * });
 * ```
 *
 * @classType tree
 * @extends Grid/column/Column
 * @column
 */
export default class TreeColumn extends Column {

    static $name = 'TreeColumn';

    static type = 'tree';

    static get defaults() {
        return {
            tree     : true,
            hideable : false,
            minWidth : 150
        };
    }

    static get fields() {
        return [
            /**
             * The icon to use for the collapse icon in collapsed state
             * @config {String|null} expandIconCls
             */
            { name : 'expandIconCls', defaultValue : 'b-icon b-icon-tree-expand' },

            /**
             * The icon to use for the collapse icon in expanded state
             * @config {String|null} collapseIconCls
             */
            { name : 'collapseIconCls', defaultValue : 'b-icon b-icon-tree-collapse' },

            /**
             * The icon to use for the collapse icon in expanded state
             * @config {String|null} collapsedFolderIconCls
             */
            { name : 'collapsedFolderIconCls' },

            /**
             * The icon to use for the collapse icon in expanded state
             * @config {String|null} expandedFolderIconCls
             */
            { name : 'expandedFolderIconCls' },

            /**
             * Size of the child indent in em. Resulting indent is indentSize multiplied by child level.
             * @config {Number} indentSize
             * @default 1.7
             */
            { name : 'indentSize', defaultValue : 1.7 },

            /**
             * The icon to use for the leaf nodes in the tree
             * @config {String|null} leafIconCls
             */
            { name : 'leafIconCls', defaultValue : 'b-icon b-icon-tree-leaf' },

            { name : 'editTargetSelector', defaultValue : '.b-tree-cell-value' },

            /**
             * Renderer function, used to format and style the content displayed in the cell. Return the cell text you
             * want to display. Can also affect other aspects of the cell, such as styling.
             *
             * <div class="note">
             * As the TreeColumn adds its own cell content to the column, there is a limit to what is supported in the
             * renderer function in comparison with an ordinary
             * {@link Grid.column.Column#config-renderer Column renderer}. Most notably is that changing `cellElement`
             * content can yield unexpected results as it will be updated later in the rendering process.
             * </div>
             *
             * You can also return a {@link Core.helper.DomHelper#typedef-DomConfig} object describing the markup
             * ```javascript
             * new Grid({
             *     columns : [
             *         {
             *              type  : 'tree',
             *              field : 'name'
             *              text  : 'Name',
             *              renderer : ({ record }) => {
             *                  return {
             *                      class : 'myClass',
             *                      children : [
             *                          {
             *                              tag : 'i',
             *                              class : 'fa fa-pen'
             *                          },
             *                          {
             *                              tag : 'span',
             *                              html : record.name
             *                          }
             *                      ]
             *                  };
             *              }
             *         }
             *     ]
             * });
             * ```
             *
             * You can modify the row element too from inside a renderer to add custom CSS classes:
             *
             * ```javascript
             * new Grid({
             *     columns : [
             *         {
             *             type     : 'tree',
             *             field    : 'name',
             *             text     : 'Name',
             *             renderer : ({ record, row }) => {
             *                // Add special CSS class to new rows that have not yet been saved
             *               row.cls.newRow = record.isPhantom;
             *
             *               return record.name;
             *         }
             *     ]
             * });
             * ```
             *
             * @param {Object} renderData Object containing renderer parameters
             * @param {HTMLElement} [renderData.cellElement] Cell element, for adding CSS classes, styling etc.
             * Can be `null` in case of export
             * @param {*} renderData.value Value to be displayed in the cell
             * @param {Core.data.Model} renderData.record Record for the row
             * @param {Grid.column.Column} renderData.column This column
             * @param {Grid.view.Grid} renderData.grid This grid
             * @param {Grid.row.Row} [renderData.row] Row object. Can be null in case of export. Use the
             * {@link Grid.row.Row#function-assignCls row's API} to manipulate CSS class names.
             * @param {Object} [renderData.size] Set `size.height` to specify the desired row height for the current
             * row. Largest specified height is used, falling back to configured {@link Grid/view/Grid#config-rowHeight}
             * in case none is specified. Can be null in case of export
             * @param {Number} [renderData.size.height] Set this to request a certain row height
             * @param {Number} [renderData.size.configuredHeight] Row height that will be used if none is requested
             * @param {Boolean} [renderData.isExport] True if record is being exported to allow special handling during
             * export.
             * @param {Boolean} [renderData.isMeasuring] True if the column is being measured for a `resizeToFitContent`
             * call. In which case an advanced renderer might need to take different actions.
             * @config {Function} renderer
             * @category Common
             */
            'renderer'
        ];
    }

    constructor(config, store) {
        super(...arguments);

        const me = this;

        // We handle htmlEncoding in this class rather than relying on the generic Row DOM manipulation
        // since this class requires quite a lot of DOM infrastructure around the actual rendered content
        me.shouldHtmlEncode = me.htmlEncode;
        me.setData('htmlEncode', false);

        // add tree renderer (which calls original renderer internally)
        if (me.renderer) {
            me.originalRenderer = me.renderer;
        }
        me.renderer = me.treeRenderer.bind(me);
    }

    /**
     * A column renderer that is automatically added to the column with { tree: true }. It adds padding and node icons
     * to the cell to make the grid appear to be a tree. The original renderer is called in the process.
     * @private
     */
    treeRenderer(renderData) {
        const
            me       = this,
            {
                grid,
                cellElement,
                row,
                record,
                isExport
            }           = renderData,
            gridMeta    = record.instanceMeta(grid.store),
            isCollapsed = !record.isLeaf && gridMeta.collapsed,
            innerConfig = {
                className : 'b-tree-cell-value'
            },
            children    = [innerConfig],
            result      = {
                className : {
                    'b-tree-cell-inner' : 1
                },
                tag    : record.href ? 'a' : 'div',
                href   : record.href,
                target : record.target,
                children
            },
            rowClasses  = {
                'b-tree-parent-row'  : 0,
                'b-tree-collapsed'   : 0,
                'b-tree-expanded'    : 0,
                'b-loading-children' : 0
            };

        let outputIsObject, iconCls, { value } = renderData,
            renderingColumn = me;

        const parentRenderer = grid.isTreeGrouped && !record.isLeaf && grid.features.treeGroup.parentRenderer;

        if (me.originalRenderer || parentRenderer) {
            let rendererHtml;

            if (parentRenderer) {
                if (record.field) {
                    renderingColumn = grid.columns.get(record.field);
                    value = renderingColumn.isWidgetColumn ? value
                        : (renderingColumn.renderer || renderingColumn.defaultRenderer)?.call(
                            renderingColumn,
                            {
                                ...renderData,
                                column      : renderingColumn,
                                value       : record.name,
                                isTreeGroup : true
                            }) ?? record.name;
                }
                rendererHtml = grid.features.treeGroup.parentRenderer({
                    field  : record.field,
                    value,
                    column : renderingColumn,
                    record : record.firstGroupChild,
                    grid
                });
            }
            else {
                rendererHtml         = me.originalRenderer(renderData);
            }

            // Check if the cell content is going to be rendered by framework
            const hasFrameworkRenderer = grid.hasFrameworkRenderer?.({
                cellContent : rendererHtml,
                renderingColumn
            });

            outputIsObject = typeof rendererHtml === 'object' && !hasFrameworkRenderer;

            // Reset the value when framework is responsible for the cell content
            value = hasFrameworkRenderer ? '' : (rendererHtml === false ? cellElement.innerHTML : rendererHtml);

            // Save content to the `rendererHtml` to be used in processCellContent implemented by framework
            renderData.rendererHtml = rendererHtml;
        }

        if (!outputIsObject) {
            value = String(value ?? '');
        }

        if (isExport) {
            return value;
        }

        if (!record.isLeaf) {
            const
                isCollapsed     = !record.isExpanded(grid.store),
                expanderIconCls = isCollapsed ? me.expandIconCls : me.collapseIconCls,
                folderIconCls   = isCollapsed ? me.collapsedFolderIconCls : me.expandedFolderIconCls;

            rowClasses['b-tree-parent-row']  = 1;
            rowClasses['b-tree-collapsed']   = isCollapsed;
            rowClasses['b-tree-expanded']    = !isCollapsed;
            rowClasses['b-loading-children'] = gridMeta.isLoadingChildren;

            cellElement.classList.add('b-tree-parent-cell');

            children.unshift({
                tag       : 'i',
                className : {
                    'b-tree-expander' : 1,
                    [expanderIconCls] : 1,
                    'b-empty-parent'  : !gridMeta.isLoadingChildren && (record.children !== true && !record.children?.length)
                }
            });

            // Allow user to customize tree icon or opt out entirely
            currentParentHasIcon = iconCls = renderData.iconCls || record.iconCls || folderIconCls;
        }
        else {

            cellElement.classList.add('b-tree-leaf-cell');

            // Allow user to customize tree icon or opt out entirely
            iconCls = renderData.iconCls || record.iconCls || me.leafIconCls;
        }

        if (iconCls) {
            children.splice(children.length - 1, 0, {
                tag       : 'i',
                className : {
                    'b-tree-icon' : 1,
                    [iconCls]     : 1
                }
            });
        }

        // Row can be just a dummy object for example when the renderer is called from Column#resizeToFitContent.
        // Add/remove the various tree node classes.
        // Keep row's aria state up to date
        if (row.isRow) {
            row.assignCls(rowClasses);

            if (!record.isLeaf) {
                row.setAttribute('aria-expanded', !isCollapsed);

                if (isCollapsed) {
                    row.removeAttribute('aria-owns');
                }
                else {
                    for (const region in grid.subGrids) {
                        const el = row.elements[region];

                        // A branch node may be configured expanded, but yet have no children.
                        // They may be added dynamically.
                        DomHelper.setAttributes(el, {
                            'aria-owns' : record.children?.length ? record.children?.map(r => `${grid.id}-${region}-${r.id}`).join(' ') : null
                        });
                    }
                }
            }
        }

        // Array of DomConfigs
        if (Array.isArray(value)) {
            innerConfig.children = value;
        }
        // Single DomConfig
        else if (outputIsObject) {
            Object.assign(innerConfig, value);
        }
        // If we are encoding HTML, or there's no raw HTML, we can use the text property
        // as the raw value, and DomSync will create a TextNode from that.
        else if (renderingColumn.shouldHtmlEncode || !value.includes('<')) {
            result.className['b-text-value'] = 1;
            innerConfig.text = value;
        }
        // If we are accepting HTML without encoding it, and there is HTML we must use html property
        else {
            innerConfig.html = value;
        }

        const padding = (record.childLevel * me.indentSize + (record.isLeaf ? (currentParentHasIcon ? 2.0 : (iconCls ? 0.5 : 0.4)) : 0));

        result.style = `padding-inline-start:${padding}em`;

        return result;
    }

    // This function is not meant to be called by any code other than Base#getCurrentConfig().
    // It extracts the current configs (fields) for the column, with special handling for the renderer
    getCurrentConfig(options) {
        const result = super.getCurrentConfig(options);

        // Use app renderer
        result.renderer = this.originalRenderer;

        return result;
    }
}

ColumnStore.registerColumnType(TreeColumn, true);
TreeColumn.exposeProperties();
