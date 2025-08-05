import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import AsyncHelper from '../../Core/helper/AsyncHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import Splitter from '../../Core/widget/Splitter.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from './GridFeatureManager.js';

/**
 * @module Grid/feature/Split
 */

const
    startScrollOptions = Object.freeze({
        animate : false,
        block   : 'start'
    }),
    endScrollOptions = Object.freeze({
        animate : false,
        block   : 'end'
    }),
    splitterWidth = 7,
    // Listeners for these events should not be added to splits
    ignoreListeners     = {
        split   : 1,
        unsplit : 1
    };
;

/**
 * This feature allows splitting the Grid into multiple views, either by using the cell context menu, or
 * programmatically by calling {@link #function-split split()}.
 *
 * {@inlineexample Grid/feature/Split.js}
 *
 * It handles splitting in 3 "directions":
 *
 * - `'horizontal'` - Splitting the grid into 2 sub-views, one above the other.
 * - `'vertical'` - Splitting the grid into 2 sub-views, one to the left of the other.
 * - `'both'` - Splitting the grid into 4 sub-views, one in each corner.
 *
 * Or, by supplying a record and/or a column to split by.
 *
 * The first sub-view (top, left or top-left depending on split direction) is the original grid, and the others are
 * clones of the original. The clones share the same store, columns and selection.
 *
 * Sub-views in the same column sync their scrolling horizontally, and sub-views in the same row sync their scrolling
 * vertically.
 *
 * Sub-views are separated by splitters, that can be dragged to resize the views.
 *
 * Splitting a multi-region grid (two regions supported) only includes the region in which the split was performed in
 * the split view.
 *
 * Splitting works best on grids that use fixed column widths, since flexed columns will resize when the grid is split.
 *
 * ## Splitting programmatically
 *
 * The split feature assigns two methods to the owning grid:
 *
 * - {@link #function-split split()} - Splits the grid into sub-views.
 * - {@link #function-unsplit unsplit()} - Re-joins the sub-views into a single grid.
 *
 * Use them to split programmatically in your app.
 *
 * ```javascript
 * // Split horizontally (eg. at the row in the center of the grid)
 * await grid.split({ direction : 'horizontal' });
 *
 * // Split both ways by a specific column and record
 * await grid.split({
 *    atRecord : grid.store.getById(10),
 *    atColumn : grid.columns.get('city')
 * });
 *
 * // Remove splits, returning to a single grid
 * grid.unsplit();
 * ```
 *
 * ## Splitting using the cell context menu
 *
 * The feature also adds a new sub-menu to the cell context menu, allowing the user to split (or un-split) the grid. See
 * the API documentation for the {@link Grid/feature/CellMenu} feature for more information on how to customize the
 * sub-menu.
 *
 * ## Accessing a sub-view
 * The sub-views are accessed by index. The original grid is at index 0, and the others are as shown below.
 * For 'horizontal' splits:
 *
 * <div style="font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-bottom: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-top: none; padding: 1em; width: 13em">1 - Sub-view</div>
 * </div>
 *
 * For 'vertical' splits:
 *
 * <div style="display: flex; flex-direction: row;font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-left: none; padding: 1em; width: 13em">1 - Sub-view</div>
 * </div>
 *
 * For 'both' splits:
 *
 * <div style="display: flex; flex-flow: row wrap; width : 27em;font-size: 0.8em">
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999; border-bottom: 2px solid #999; padding: 1em; width: 13em">0 - Original</div>
 *     <div style="border: 1px solid #ccc; border-left: none; border-bottom: 2px solid #999; padding: 1em; width: 13em">1 - Sub-view</div>
 *     <div style="border: 1px solid #ccc; border-right: 2px solid #999;border-top: none; padding: 1em; width: 13em">2 - Sub-view</div>
 *     <div style="border: 1px solid #ccc; border-top: none; border-left: none; padding: 1em; width: 13em">3 - Sub-view</div>
 * </div>
 *
 * The {@link #property-subViews} property returns an array containing all sub-views, including the original. Note that
 * the property is also exposed on the owning Grid. Access a specific sub-view by index (see illustrations above). For
 * example to access the bottom right sub-view in a 'both' split:
 *
 * ```javascript
 * await grid.split({ direction : 'both' });
 * const bottomRight = grid.subViews[3];
 * await bottomRight.scrollRowIntoView(100);
 * ```
 *
 * ## Troubleshooting
 *
 * The splits are inserted into a container element (which has the `.b-split-container` CSS class), replacing the
 * original grid. If it does not render correctly out of the box, you should make sure that any CSS rules you have that
 * apply size to the grid also applies to the container element.
 *
 * For example if you use a CSS flex rule to size the grid:
 *
 * ```css
 * .b-grid {
 *     // Size grid using flex
 *     flex : 3;
 * }
 * ```
 *
 * Then you should also apply the same rule to the container element:
 *
 * ```css
 * .b-grid,
 * .b-split-container {
 *     flex : 3;
 * }
 * ```
 *
 * {@note}
 * Note that configuration changes at runtime, when already split, are not automatically propagated to the sub-views. If
 * you need to change a config at runtime, either first unsplit the grid, or change it on each sub-view individually. A
 * notable exception from this is that enabling / disabling features at runtime is reflected in the sub-views.
 * {/@note}
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype split
 * @feature
 */
export default class Split extends InstancePlugin {
    static $name = 'Split';

    static featureClass = '';

    static configurable = {

        /**
         * An array of sub-views. The first sub-view is the original grid, and the others are clones of the original.
         * See the "Accessing a sub-view" section above for more information.
         *
         * ```javascript
         * await grid.split('vertical');
         * const bottom = grid.subViews[1];
         * await bottom.scrollRowIntoView(100);
         * ```
         *
         * Note that this property is accessible directly on the grid instance.
         *
         * @member {Grid.view.Grid[]} subViews
         * @on-owner
         * @readonly
         * @category Common
         */
        subViews : [], // Not a config, but still defined in configurable to allow assigning it in pluginConfig,

        /**
         * Properties whose changes should be relayed to sub-views at runtime.
         *
         * Supply an object with property names as keys, and a truthy value to relay the change, or a falsy value to not
         * relay it. The object will be merged with the default values.
         *
         * By default, these properties are relayed:
         * * {@link Grid/view/Grid#property-readOnly}
         * * {@link Grid/view/Grid#property-rowHeight}
         *
         * Example of supplying a custom set of properties to relay:
         * ```javascript
         * const grid = new Grid({
         *     features : {
         *         split : {
         *             relayProperties : {
         *                 readOnly : false, // Do not relay readOnly changes
         *                 myConfig : true   // Relay changes to the myConfig property
         *             }
         *         }
         *     }
         * }
         * ```
         * @config {Object<String,Boolean>}
         */
        relayProperties : {
            value : {
                readOnly  : 1,
                rowHeight : 1
            },
            $config : {
                merge : 'merge'
            }
        }
    };

    static pluginConfig = {
        chain  : ['populateCellMenu', 'afterConfigChange', 'afterAddListener', 'afterRemoveListener'],
        assign : ['split', 'unsplit', 'subViews', 'syncSplits']
    };

    // Flag used to ignore column changes that arise from syncing columns
    #ignoreColumnChanges = false;

    restorers = [];

    doDestroy() {
        this.unsplit(true);

        super.doDestroy();
    }

    doDisable(disable) {
        const me = this;

        if (!me.isConfiguring) {
            if (disable) {
                me._disabledSplitOptions = me._splitOptions;
                me.unsplit();
            }
            else if (me._disabledSplitOptions) {
                me.split(me._disabledSplitOptions);
                me._disabledSplitOptions = null;
            }
        }
    }

    //region Split / unsplit

    get isSplit() {
        return Boolean(this.widgets?.length);
    }

    getClientConfig(appendTo, order, options, config = {}) {
        const
            { client }            = this,
            { subGrids, regions } = client,
            columns                = client.columns.records.slice(),
            subGridConfigs         = ObjectHelper.assign({}, client.subGridConfigs);

        // Match current sub-grid sizes
        client.eachSubGrid(subGrid => {
            const config = subGridConfigs[subGrid.region];
            if (subGrid.flex) {
                config.flex = subGrid.flex;
            }
            else {
                config.width = subGrid.element.style.width;
            }
        });

        if (options.atColumn && regions.length > 1 && order > 0) {
            // Exclude regions to the left of the split
            const subGridIndex = regions.indexOf(options.atColumn.region);

            for (let i = 0; i < subGridIndex; i++) {
                const subGrid = subGrids[regions[i]];

                ArrayHelper.remove(columns, ...subGrid.columns.records);

                delete subGridConfigs[regions[i]];
            }
        }

        const clientConfig = ObjectHelper.assign({}, client.initialConfig, {
            appendTo,
            insertFirst  : null,
            insertBefore : null,
            splitFrom    : client,
            owner        : client.owner,
            // Use no toolbar or fake empty toolbar for things to line up nicely
            tbar         : client.initialConfig.tbar && order === 1 ? {
                height : client.tbar.height,
                items  : [' ']
            } : null,
            // Share store & selection
            store                    : client.store,
            selectedRecordCollection : client.selectedRecordCollection,
            subGridConfigs,
            // Cannot directly share columns, since there is a 1-1 mapping between column and it's header
            columns                  : this.cloneColumns(columns),
            minHeight                : 0,
            minWidth                 : 0
        }, config);

        // Listeners are removed from initialConfig during initialization, use non-internal current listeners
        const appListeners = {};
        for (const name in client.listeners) {
            if (!ignoreListeners[name]) {
                const [listener] = client.listeners[name];
                if (!listener.$internal) {
                    appListeners[name] = listener;
                }
            }
        }

        // Not internalListeners on purpose, these are app listeners
        clientConfig.listeners = appListeners;

        // Hide headers for bottom clone in horizontal split
        if (options.direction === 'horizontal') {
            clientConfig.hideHeaders = true;
        }
        // Hide headers for bottom clones in both split
        else if (options.direction === 'both' && order !== 1) {
            clientConfig.hideHeaders = true;
        }

        delete clientConfig.data;

        return clientConfig;
    }

    cloneColumns(source) {
        return source.flatMap(col => {
            // Do not clone selection column, it will be injected by GridSelection.
            // Ditto for the row expander column
            if (col.meta.isSelectionColumn || col.field === 'expanderActionColumn') {
                return [];
            }

            const data = { ...col.data };

            if (col.children) {
                data.children = col.children.map(child => ({ ...child.data }));
            }

            // RowNumberColumn "pollutes" headerRenderer, will create infinite loop if not cleaned up
            delete data.headerRenderer;
            delete data.parentId;

            return data;
        });
    }

    cloneClient(appendTo, order, options, config) {
        const
            clientConfig = this.getClientConfig(appendTo, order, options, config),
            clone        = new this.client.constructor(clientConfig);

        clone.element.classList.add('b-split-clone');

        return clone;
    }

    // Process options, deducing direction, atRecord, etc.
    processOptions(options) {
        const
            { client }                        = this,
            { atRecord, atColumn, direction } = options;

        if (!direction) {
            // Infer direction from record & column
            if (atRecord && atColumn) {
                options.direction = 'both';
            }
            else if (atColumn) {
                options.direction = 'vertical';
            }
            else {
                options.direction = 'horizontal';
            }
        }
        else {
            // Only given a direction, cut roughly in half
            if (direction !== 'vertical' && !atRecord && client.store.count) {
                const
                    centerY   = client._bodyRectangle.height / 2 + client.scrollable.y,
                    centerRow = client.rowManager.getRowAt(centerY, true) ?? client.rowManager.rows[Math.ceil(client.rowManager.rows.length / 2)];

                options.atRecord = client.store.getById(centerRow.id);
            }

            if (direction !== 'horizontal' && !atColumn) {
                const bounds = Rectangle.from(client.element);

                // Figure out subgrid intersecting center of grid
                let centerX = bounds.center.x - bounds.x,
                    subGrid = client.subGrids[client.regions[0]],
                    i       = 0,
                    column  = null;

                while (centerX > subGrid.width) {
                    centerX -= subGrid.width;
                    subGrid = client.subGrids[client.regions[++i]];
                }

                // We want the center column in view, but iteration below is over all columns
                centerX += subGrid.scrollable.x;

                // Figure out column in the subgrid
                const { visibleColumns } = subGrid.columns;
                let x = 0, j = 0;
                while (x < centerX && j < visibleColumns.length) {
                    column = visibleColumns[j++];
                    x += column.element.offsetWidth;
                }

                options.atColumn = column;
            }
        }

        return options;
    }

    // Create element to contain the splits, it "both" mode it will hold a top container and a bottom container.
    // In single mode, it will hold the splits + splitters directly.
    createSplitContainer({ direction }) {
        const
            { client }  = this,
            { element } = client;

        return this.splitContainer = DomHelper.createElement({
            parent    : element.parentElement,
            className : {
                'b-split-container'      : 1,
                [`b-split-${direction}`] : 1,
                'b-rtl'                  : client.rtl
            },
            style : {
                width  : element.style.width,
                height : element.style.height
            },
            children : [
                // Split in one dir, use original as first child
                direction !== 'both' && element,
                // Split in both directions, make two sub-containers and put original in first
                direction === 'both' && {
                    className : 'b-split-top',
                    children  : [
                        element
                    ]
                },
                direction === 'both' && {
                    className : 'b-split-bottom'
                }
            ]
        });
    }

    // Make the headers of all splits same height. Since headers shrinkwrap, they might differ depending on which
    // subgrids was cloned to each split
    syncHeaderHeights() {
        let maxHeaderHeight = 0;

        // Find tallest header
        for (const split of this.subViews) {
            split.eachSubGrid(subGrid => {
                if (subGrid.header.height > maxHeaderHeight) {
                    maxHeaderHeight = subGrid.header.height;
                }
            });
        }

        // Apply its height to all headers
        for (const split of this.subViews) {
            split.eachSubGrid(subGrid => {
                subGrid.header.height = maxHeaderHeight;
            });
        }
    }

    // Clones can be created with correct subgrids, in the original we might instead need to hide some when splitting
    // in a region that is not the last one (locked for example)
    toggleOriginalSubGrids(options) {
        const
            me          = this,
            { client }  = me,
            { regions } = client;

        // Split at a column with multiple regions
        if (options.atColumn && regions.length > 1) {
            const
                subGridIndex = regions.indexOf(options.atColumn.region),
                // Always process the original
                splits = [client];

            // And the bottom left one in a four way split
            if (options.direction === 'both') {
                splits.push(me.subViews[2]);
            }

            for (const split of splits) {
                // Hide regions to the right of the split in the original
                if (subGridIndex + 1 < regions.length) {
                    const isOriginal = split === client;

                    // Leftmost subgrid to keep visible
                    const subGrid = split.subGrids[regions[subGridIndex]];

                    // It won't need a splitter when succeeding subgrids are hidden
                    subGrid.hideSplitter();
                    isOriginal && me.restorers.push(() => subGrid.showSplitter());

                    // Force flex to fill space left by hiding succeeding subgrids
                    if (!subGrid.flex) {
                        // Don't affect other splits
                        client.inForEachOther = true;
                        subGrid.flex = 1;
                        client.inForEachOther = false;

                        isOriginal && me.restorers.push(() => {
                            subGrid.flex = null;
                            subGrid.width = subGrid._initialWidth;
                        });
                    }

                    // Hide succeeding subgrids
                    for (let i = subGridIndex + 1; i < regions.length; i++) {
                        const subGrid = split.subGrids[regions[i]];
                        subGrid.hide();

                        isOriginal && me.restorers.push(() => {
                            subGrid.show();
                        });
                    }

                    // Only one subgrid remains visible, use its width as splits width
                    if (regions.length === 2) {
                        split._initialWidth = split.element.style.width;
                        split._initialFlex = split.flex;

                        split.width = subGrid._initialWidth;

                        isOriginal && me.restorers.push(() => {
                            if (split._initialFlex !== null) {
                                split.flex = split._initialFlex;
                            }
                            else if (split._initialWidth !== null) {
                                split.width = split._initialWidth;
                            }
                        });
                    }
                }
            }
        }
    }

    /**
     * Split the grid into two or four parts.
     *
     * - Splits into two when passed `direction : 'vertical'`, `direction : 'horizontal'` or `atColumn` or `atRecord`.
     * - Splits into four when passed `direction : 'both'` or `atColumn` and `atRecord`.
     *
     * ```javascript
     * // Split horizontally (at the row in the center of the grid)
     * await grid.split({ direction : 'horizontal' });
     *
     * // Split both ways by a specific column and record
     * await grid.split({
     *    atRecord : grid.store.getById(10),
     *    atColumn : grid.columns.get('city')
     * });
     * ```
     *
     * To return to a single grid, call {@link #function-unsplit}.
     *
     * Note that this function is callable directly on the grid instance.
     *
     * @param {Object} [options] Split options
     * @param {'vertical'|'horizontal'|'both'} [options.direction] Split direction, 'vertical', 'horizontal' or 'both'.
     * Not needed when passing `atColumn` or `atRecord`.
     * @param {Grid.column.Column} [options.atColumn] Column to split at
     * @param {Core.data.Model} [options.atRecord] Record to split at
     * @returns {Promise} Resolves when split is complete, and subviews are scrolled to the correct position.
     * @async
     * @on-owner
     * @category Common
     */
    async split(options = {}) {
        const
            me         = this,
            { client } = me;

        // Can't split a split
        if (client.splitFrom) {
            return;
        }

        if (me.isSplit) {
            await me.unsplit(true);
        }

        const
            { rtl }                           = client,
            { atRecord, atColumn, direction } = me.processOptions(options);

        let { splitX, remainingWidth } = options,
            splitY                     = null,
            remainingHeight            = null;

        if (atRecord) {
            await client.scrollRowIntoView(atRecord);

            const row = client.getRowFor(atRecord);
            if (!row) {
                throw new Error(`Could not find row for record ${atRecord.id}`);
            }

            splitY = Rectangle.from(row.cells[0], client.element).bottom;
            remainingHeight = Rectangle.from(client.element).height - splitY;
        }

        if (atColumn && !splitX) {
            splitX = Rectangle.from(atColumn.element, client.element).getEnd(rtl);
            remainingWidth = Rectangle.from(client.element).width - splitX - DomHelper.scrollBarWidth;

            if (rtl) {
                const x = splitX;
                splitX = remainingWidth + DomHelper.scrollBarWidth;
                remainingWidth = x - DomHelper.scrollBarWidth;
            }
        }

        const
            scrollPromises        = [],
            splitContainer        = me.createSplitContainer(options),
            { visibleColumns }    = client.columns,
            nextColumn            = atColumn ? visibleColumns[visibleColumns.indexOf(atColumn) + 1] : null,
            nextRecord            = atRecord ? client.store.getNext(atRecord) : null;

        client.eachSubGrid(subGrid => subGrid._initialWidth = subGrid.width);

        if (direction !== 'both') {
            const cloneConfig = {
                flex   : `0 0 ${(splitY != null ? remainingHeight : remainingWidth) - splitterWidth}px`,
                height : null
            };

            // Horizontal or vertical, only needs one splitter and one clone
            const [, clone] = me.widgets = [
                new Splitter({ appendTo : splitContainer }),
                me.cloneClient(splitContainer, direction === 'vertical' ? 1 : 0, options, cloneConfig)
            ];

            if (splitX != null) {
                // It does not like being thrown around in DOM and resized when scrolled, fix up
                client.renderRows();

                // Don't bother scrolling here if given a date, Schedulers feature handles that
                if (!options.atDate) {
                    scrollPromises.push(client.scrollColumnIntoView(atColumn, endScrollOptions));
                    nextColumn && scrollPromises.push(clone.scrollColumnIntoView(nextColumn, startScrollOptions));
                }
            }

            if (splitY != null) {
                // Always have an atRecord to split at
                scrollPromises.push(clone.scrollRowIntoView(nextRecord, startScrollOptions));
            }

            client.element.classList.add('b-split-start');
            clone.element.classList.add('b-split-end');

            // Sync scrolling
            client.scrollable.addPartner(clone.scrollable, {
                x : direction === 'horizontal',
                y : direction !== 'horizontal'
            });
        }
        else {
            const rightConfig = {
                flex : `0 0 ${remainingWidth - splitterWidth}px`
            };

            splitContainer.lastElementChild.style.flex = `0 0 ${remainingHeight - splitterWidth}px`;

            // Both directions, 3 splitters (one horizontal with full span, two vertical halves) and 3 clones
            me.widgets = [
                new Splitter({ insertBefore : splitContainer.lastElementChild }), // Full horizontal
                me.topSplitter = new Splitter({ appendTo : splitContainer.firstElementChild }), // Top vertical
                me.cloneClient(splitContainer.firstElementChild, 1, options, rightConfig), // Top right
                me.cloneClient(splitContainer.lastElementChild, 0, options), // Bottom left
                me.bottomSplitter = new Splitter({ appendTo : splitContainer.lastElementChild }), // Bottom vertical
                me.cloneClient(splitContainer.lastElementChild, 2, options, rightConfig) // Bottom right
            ];

            const
                topLeft     = client,
                topRight    = me.widgets[2],
                bottomLeft  = me.widgets[3],
                bottomRight = me.widgets[5];

            topLeft.element.classList.add('b-split-top-start');
            topRight.element.classList.add('b-split-top-end');
            bottomLeft.element.classList.add('b-split-bottom-start');
            bottomRight.element.classList.add('b-split-bottom-end');

            if (splitX != null) {
                // It does not like being thrown around in DOM and resized when scrolled, fix up
                topLeft.renderRows();
                bottomLeft.renderRows();

                // Don't bother scrolling here if given a date, Schedulers feature handles that
                if (atColumn && !options.atDate) {
                    scrollPromises.push(client.scrollColumnIntoView(atColumn, endScrollOptions));
                    nextColumn && scrollPromises.push(topRight.scrollColumnIntoView(nextColumn, startScrollOptions));
                }
            }
            if (splitY != null) {
                scrollPromises.push(
                    bottomLeft.scrollRowIntoView(nextRecord, startScrollOptions),
                    bottomRight.scrollRowIntoView(nextRecord, startScrollOptions)
                );
            }

            // Sync scrolling
            topLeft.scrollable.addPartner(topRight.scrollable, 'y');
            topLeft.scrollable.addPartner(bottomLeft.scrollable, 'x');
            topRight.scrollable.addPartner(bottomRight.scrollable, 'x');
            bottomLeft.scrollable.addPartner(bottomRight.scrollable, 'y');

            // Set up vertical splitter sync
            me.topSplitter.ion({
                splitterMouseDown : 'onSplitterMouseDown',
                drag              : 'onSplitterDrag',
                drop              : 'onSplitterDrop',
                thisObj           : me
            });

            me.bottomSplitter.ion({
                splitterMouseDown : 'onSplitterMouseDown',
                drag              : 'onSplitterDrag',
                drop              : 'onSplitterDrop',
                thisObj           : me
            });
        }

        me.subViews = [client, ...me.widgets.filter(w => w.isGridBase)];

        me.toggleOriginalSubGrids(options);

        me.syncHeaderHeights();

        me._splitOptions = options;

        await Promise.all(scrollPromises);

        // Moving in DOM does not seem to trigger resize, do it manually
        const bounds = Rectangle.from(client.element);
        client.onInternalResize(client.element, bounds.width, bounds.height);

        client.eachSubGrid(subGrid => {
            const subGridBounds = Rectangle.from(subGrid.element);
            subGrid.onInternalResize(subGrid.element, subGridBounds.width, subGridBounds.height);
        });

        // If scrolled, the original element gets out of sync when moved around in DOM
        client.scrollable.x += 0.5;
        client.scrollable.y += 0.5;

        me.startSyncingColumns();

        /**
         * Fires when splitting the Grid.
         * @event split
         * @param {Grid.view.GridBase[]} subViews The sub views created by the split
         * @param {Object} options The options passed to the split call
         * @param {'horizontal'|'vertical'|'both'} options.direction The direction of the split
         * @param {Grid.column.Column} options.atColumn The column to split at
         * @param {Core.data.Model} options.atRecord The record to split at
         * @on-owner
         */
        client.trigger('split', { subViews : me.subViews, options });

        return me.subViews;
    }

    /**
     * Remove splits, returning to a single grid.
     *
     * Note that this function is callable directly on the grid instance.
     *
     * @on-owner
     * @async
     * @category Common
     */
    async unsplit(silent = false) {
        const
            me          = this,
            { client }  = me,
            { element } = client;

        if (me.isSplit) {
            me.stopSyncingColumns();

            me.widgets?.forEach(split => split.destroy());
            me.widgets = null;

            // Safari & FF looses scroll position when moving elements around in DOM,
            // but reading it here fixes it
            client.eachSubGrid(subGrid => subGrid.scrollable.x);
            client.scrollable.y;

            me.splitContainer.parentElement.appendChild(element);
            me.splitContainer.remove();
            me.splitContainer = null;

            // Reset any size applied by splitter
            element.style.flexBasis = element.style.flexGrow = '';
            element.classList.remove('b-split-top-start', 'b-split-start');

            me.subViews.length = 0;

            if (!me.isDestroying) {
                // We have been pretty violent with the DOM, so force a repaint of rows
                client.renderRows();

                me.unsplitCleanup();

                for (const restorer of me.restorers) {
                    restorer();
                }

                me.restorers.length = 0;

                // Ugly, but FF needs a couple of frames to not lose scroll position if we are splitting again
                await AsyncHelper.animationFrame();
                await AsyncHelper.animationFrame();

                if (me.isDestroyed) {
                    return;
                }

                /**
                 * Fires when un-splitting the Grid.
                 * @event unsplit
                 * @on-owner
                 */
                !silent && client.trigger('unsplit');

                me._splitOptions = null;
            }
        }
    }

    unsplitCleanup() {}

    //endregion

    //region Context menu

    populateCellMenu({ record, column, items }) {
        const
            me            = this,
            { isSplit }   = me,
            { splitFrom } = me.client;

        if (!me.disabled) {
            items.splitGrid = {
                text        : 'L{split}',
                localeClass : me,
                icon        : 'b-icon b-icon-split-vertical',
                weight      : 500,
                separator   : true,
                hidden      : isSplit || splitFrom,
                menu        : {
                    splitHorizontally : {
                        text        : 'L{horizontally}',
                        icon        : 'b-icon b-icon-split-horizontal',
                        localeClass : me,
                        weight      : 100,
                        onItem() {
                            me.split({ atRecord : record });
                        }
                    },
                    splitVertically : {
                        text        : 'L{vertically}',
                        icon        : 'b-icon b-icon-split-vertical',
                        localeClass : me,
                        weight      : 200,
                        onItem() {
                            me.split({ atColumn : column });
                        }
                    },
                    splitBoth : {
                        text        : 'L{both}',
                        icon        : 'b-icon b-icon-split-both',
                        localeClass : me,
                        weight      : 300,
                        onItem() {
                            me.split({ atColumn : column, atRecord : record });
                        }
                    }
                }
            };

            items.unsplitGrid = {
                text        : 'L{unsplit}',
                localeClass : me,
                icon        : 'b-icon b-icon-clear',
                hidden      : !(isSplit || splitFrom),
                weight      : 400,
                separator   : true,
                onItem() {
                    (splitFrom || me).unsplit();
                }
            };
        }
    }

    //endregion

    //region Syncing columns

    startSyncingColumns() {
        for (const subView of this.subViews) {
            subView.columns.ion({
                name    : 'columns',
                change  : 'onColumnsChange',
                thisObj : this
            });
        }
    }

    stopSyncingColumns() {
        this.detachListeners('columns');
    }

    onColumnsChange({ source, isMove, action, /*index, */parent, records, changes }) {
        const me = this;

        if (!me.#ignoreColumnChanges) {
            me.#ignoreColumnChanges = true;

            for (const clone of me.subViews) {
                const { columns } = clone;

                if (source !== columns) {
                    // Special handling for column moved from subgrid not in split to subgrid in split
                    if (action === 'update' && changes.region && Object.keys(changes).length === 1) {
                        // Move from non-existing to existing, add
                        if (!columns.getById(records[0].id)) {
                            const
                                [column]     = records,
                                targetParent = columns.getById(me.$before.parent.id) ?? columns.rootNode,
                                targetBefore = me.$before.id !== null && columns.getById(me.$before.id);

                            targetParent.insertChild(column.data, targetBefore);
                        }
                        // Vice versa, remove
                        else {
                            columns.remove(records[0].id);
                        }

                        me.$before = null;
                    }
                    else if (!isMove?.[records[0].id]) {
                        if (action === 'add') {
                            // Only add columns that are in a subgrid that is visible in the clone
                            const relevantColumns = records.filter(column => clone.getSubGridFromColumn(column));
                            columns.add(me.cloneColumns(relevantColumns));
                        }
                        else {
                            columns.applyChangesFromStore(source);
                        }
                    }
                    // We have to handle move separately, since it does not leave the column store modified (in any
                    // meaningful way)
                    else if (action === 'add') {
                        const
                            sourceColumn = records[0],
                            sourceBefore = sourceColumn.nextSibling,
                            targetColumn = columns.getById(sourceColumn.id); //columns.allRecords.find(r => r.id === sourceColumn.id);

                        // When splitting a multi-region grid, not all columns are present in all splits. But, it might
                        // be moved from locked to normal (etc.) in original, but split is not showing source region. In
                        // that case, we handle it on the region update - and must store details here
                        if (!targetColumn) {
                            me.$before = {
                                id : sourceBefore?.id,
                                parent
                            };
                            me.#ignoreColumnChanges = false;
                            return;
                        }

                        if (sourceColumn.meta.isSelectionColumn) {
                            me.#ignoreColumnChanges = false;
                            return;
                        }
                        const
                            targetParent = columns.getById(parent.id) ?? columns.rootNode,
                            targetBefore = sourceBefore && columns.getById(sourceBefore.id);

                        targetParent.insertChild(targetColumn, targetBefore);
                    }

                    columns.commit();
                }
            }

            source.commit();

            me.#ignoreColumnChanges = false;
        }
    }

    //endregion

    //region Syncing splitters

    getOtherSplitter(splitter) {
        return splitter === this.topSplitter ? this.bottomSplitter : this.topSplitter;
    }

    onSplitterMouseDown({ source, event }) {
        if (!event.handled) {
            event.handled = true;
            this.getOtherSplitter(source).onMouseDown(event);
        }
    }

    onSplitterDrag({ source, event }) {
        if (!event.handled) {
            event.handled = true;
            this.getOtherSplitter(source).onMouseMove(event);
        }
    }

    onSplitterDrop({ source, event }) {
        if (!event.handled) {
            event.handled = true;
            this.getOtherSplitter(source).onMouseUp(event);
        }
    }

    //endregion

    //region Relaying property changes & events

    // Relay relevant config changes to other splits
    afterConfigChange({ name, value }) {
        if (this.isSplit && this.relayProperties[name]) {
            this.syncSplits(split => {
                split[name] = value;
            });
        }
    }

    // Sync listeners added at runtime to other splits
    afterAddListener(eventName, listener) {
        if (this.isSplit && !listener.$internal && !ignoreListeners[eventName]) {
            // Not using `ion()` on purpose, these are app listeners
            // eslint-disable-next-line bryntum/no-on-in-lib
            this.syncSplits(split => split.on(eventName, listener));
        }
    }

    afterRemoveListener(eventName, listener) {
        if (!listener.$internal) {
            this.syncSplits(split => split.un(eventName, listener));
        }
    }

    //endregion

    //region Util

    // Call a fn for all splits except the on this fn is called on
    forEachOther(fn) {
        const original = this.client.splitFrom || this.client;

        if (original.features.split.enabled && !original.inForEachOther) {
            // Protect against infinite recursion by being called from the fn
            original.inForEachOther = true;

            for (const view of original.subViews) {
                if (view !== this.client) {
                    fn(view);
                }
            }

            original.inForEachOther = false;
        }
    }

    syncSplits(fn) {
        this.forEachOther(fn);
    }

    //endregion
}

GridFeatureManager.registerFeature(Split, false);
