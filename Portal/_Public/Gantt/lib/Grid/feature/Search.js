import DomHelper from '../../Core/helper/DomHelper.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import DomDataStore from '../../Core/data/DomDataStore.js';
import GridFeatureManager from './GridFeatureManager.js';
import Delayable from '../../Core/mixin/Delayable.js';
import StringHelper from '../../Core/helper/StringHelper.js';



/**
 * @module Grid/feature/Search
 */

/**
 * {@inlineexample Grid/feature/Search.js}
 *
 * Feature that allows the user to search the entire grid. Navigate between hits using the
 * keyboard, [F3] or [Ctrl/CMD + G] moves to next, also pressing [Shift] moves to previous.
 *
 * Note that this feature does not include a UI, please build your own and call appropriate methods in the feature.
 * For a demo implementation, see <a href="../examples/search/" target="_blank">Search example</a>.
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * ## Keyboard shortcuts
 *
 * This feature has the following default keyboard shortcuts:
 *
 * | Keys               | Action        | Action description                |
 * |--------------------|---------------|-----------------------------------|
 * | `F3`               | *goToNextHit* | Move focus to next search hit     |
 * | `Shift`+`F3`       | *goToPrevHit* | Move focus to previous search hit |
 * | `Ctrl`+`G`         | *goToNextHit* | Move focus to next search hit     |
 * | `Ctrl`+`Shift`+`G` | *goToPrevHit* | Move focus to previous search hit |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md)
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @example
 * // enable Search
 * let grid = new Grid({
 *   features: {
 *     search: true
 *   }
 * });
 *
 * // perform search
 * grid.features.search.search('steve');
 *
 * @demo Grid/search
 * @classtype search
 * @feature
 */
export default class Search extends Delayable(InstancePlugin) {
    //region Init

    static $name = 'Search';

    static configurable = {
        /**
         * The maximum amount of search hits
         * @config {Number}
         * @default
         */
        limit : 1000,

        /**
         * Set to false to not show the search hit index numbers
         * @config {Boolean}
         * @default
         */
        showHitIndex : true,

        /**
         * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
         * @config {Object<String,String>}
         */
        keyMap : {
            F3             : 'gotoNextHit',
            'Ctrl+g'       : 'gotoNextHit',
            'Shift+F3'     : 'gotoPrevHit',
            'Ctrl+Shift+g' : 'gotoPrevHit'
        }
    };

    static get properties() {
        return {
            hitCls          : 'b-search-hit',
            hitCellCls      : 'b-search-hit-cell',
            hitCellBadgeCls : 'b-search-hit-cell-badge',
            hitTextCls      : 'b-search-hit-text'
        };
    }

    construct(grid, config) {
        super.construct(grid, config);

        Object.assign(this, {
            grid,
            text       : '',
            hitEls     : [],
            treeWalker : grid.setupTreeWalker(grid.element, DomHelper.NodeFilter.SHOW_TEXT, () => DomHelper.NodeFilter.FILTER_ACCEPT)
        });

        // When new nodes appear due to node expand, include them in the search
        grid.ion({
            expandNode : 'onTreeNodeExpand',
            thisObj    : this
        });
    }

    isActionAvailable() {
        return Boolean(this.text);
    }

    onTreeNodeExpand() {
        if (this.text) {
            this.requestAnimationFrame(this.search, [this.text, false, true]);
        }
    }

    doDestroy() {
        this.clear(true);
        super.doDestroy();
    }

    doDisable(disable) {
        if (disable) {
            this.clear();
        }

        super.doDisable(disable);
    }

    get store() {
        return this.grid.store;
    }

    //endregion

    //region Plugin config

    // Plugin configuration. This plugin chains some of the functions in Grid.
    static get pluginConfig() {
        return {
            chain : ['populateCellMenu']
        };
    }

    //endregion

    //region Search

    /**
     * Performs a search and highlights hits.
     * @param {String} text Text to search for
     * @param {Boolean} [gotoHit] Go to first hit after search
     * @param {Boolean} [reapply] Pass true to force search
     * @param {String[]} [fields] An array of the fields to search for the value in
     */
    async search(text, gotoHit = true, reapply = false, fields, fromSplit = false) {
        const me = this;

        // empty search considered a clear
        if (!text) {
            return me.clear();
        }

        // searching for same thing again, do nothing
        if (!reapply && text === me.text || me.disabled) {
            return;
        }

        const
            { grid, store } = me,
            // Only search columns in use
            columns         = grid.columns.visibleColumns.filter(col => col.searchable !== false),
            formatters      = [];

        fields      = fields || columns.map(col => {
            // For date / number columns
            formatters.push(col.formatValue?.bind(col));
            return col.field;
        });
        const found = store.search(text, fields, formatters);

        if (store.isTree && found.length) {
            await grid.expandTo(found.map(hit => hit.id));
        }

        // Only include first result for merged cells in the count
        for (const column of columns) {
            if (column.mergeCells && column.isSorted) {
                let prevValue = null,
                    belongsTo = null;

                for (const hit of found) {
                    if (hit.field === column.field) {
                        const value = hit.data[hit.field];
                        if (value === prevValue) {
                            hit.belongsTo = belongsTo;
                        }
                        prevValue = value;
                        belongsTo = `${hit.field}-${hit.id}`;
                    }
                }
            }
        }

        let i = 1;

        Object.assign(me, {
            foundMap  : {},
            prevFound : me.found,
            found,
            text,
            findRe    : new RegExp(`(\\s+)?(${StringHelper.escapeRegExp(String(text))})(\\s+)?`, 'ig')
        });

        me.clearHits();

        if (!found) {
            return;
        }

        // highlight hits for visible cells
        for (const hit of found) {
            // merged cells reuse the index of the first hit in the range
            me.foundMap[`${hit.field}-${hit.id}`] = hit.belongsTo ? me.foundMap[hit.belongsTo] : i++;
            // limit hits
            if (i > me.limit) {
                break;
            }
        }

        if (!me.listenersInitialized) {
            grid.rowManager.ion({
                name       : 'renderCell',
                renderCell : 'renderCell',
                thisObj    : me
            });
            store.ion({
                name                                : 'storeRefresh',
                [`refresh${grid.asyncEventSuffix}`] : 'onStoreRefresh',
                thisObj                             : me
            });
            me.listenersInitialized = true;
        }

        grid.refreshRows();

        grid.trigger('search', { grid, find : text, found });

        if (gotoHit && !me.isHitFocused && !fromSplit) {
            me.gotoNextHit(true);
        }

        grid.syncSplits?.(other => other.features.search.search(text, gotoHit, reapply, fields, true));

        return found;
    }

    clearHits() {
        // Clear old hits
        for (const cellElement of DomHelper.children(this.grid.element, '.' + this.hitCls)) {
            cellElement.classList.remove(this.hitCls, this.hitCellCls);

            // Rerender cell to remove search-hit-text
            const row = DomDataStore.get(cellElement).row;

            // Merged cells have no row, but they will be cleared anyway by their own DomSync call
            if (row) {
                // Need to force replace the markup
                row.forceInnerHTML = true;
                row.renderCell(cellElement);
                row.forceInnerHTML = false;
            }
        }
    }

    /**
     * Clears search results.
     */
    clear(silent = false) {
        const
            me       = this,
            { grid } = me;

        if (me.foundMap) {
            delete me.foundMap;
        }

        me.text = null;

        me.clearHits();

        if (me.listenersInitialized) {
            me.detachListeners('renderCell');
            me.detachListeners('storeRefresh');
            me.listenersInitialized = false;
        }

        if (!silent) {
            grid.trigger('clearSearch', { grid });

            // Propagate to other grids when splitting
            grid.syncSplits?.(other => other.features.search.clear());
        }
    }

    /**
     * Number of results found
     * @readonly
     * @property {Number}
     */
    get foundCount() {
        return this.found?.length ?? 0;
    }

    //endregion

    //region Navigation

    /**
     * Returns true if focused row is a hit
     * @property {Boolean}
     * @readonly
     */
    get isHitFocused() {
        const
            me              = this,
            { grid }        = me,
            { focusedCell } = grid;

        if (focusedCell?.cell?.contains(DomHelper.getActiveElement(grid.element))) {
            const { rowIndex, column } = focusedCell;

            return rowIndex !== -1 && me.found.some(hit =>
                hit.index === rowIndex && column && hit.field === column.field
            );
        }
    }

    /**
     * Select the next hit, scrolling it into view. Triggered with [f3] or [ctrl]/[cmd] + [g].
     */
    gotoNextHit(fromStart = false) {
        const
            me = this;

        if (!me.found?.length) return;

        const
            { grid, store } = me,
            fromCell        = grid.focusedCell || grid.lastFocusedCell,
            currentIndex    = fromCell && fromStart !== true ? store.indexOf(fromCell.record, undefined, true) : -1,
            nextHit         = me.found.findIndex(hit => hit.index > currentIndex);

        if (nextHit !== -1) {
            me.gotoHit(nextHit);
        }
    }

    /**
     * Select the previous hit, scrolling it into view. Triggered with [shift] + [f3] or [shift] + [ctrl]/[cmd] + [g].
     */
    gotoPrevHit() {
        const
            me        = this,
            { store } = me;

        if (!me.found?.length) return;

        const
            { grid, found } = me,
            fromCell        = grid.focusedCell || grid.lastFocusedCell,
            currentIndex    = fromCell ? store.indexOf(fromCell.record, undefined, true) : 0;

        for (let i = found.length - 1; i--; i >= 0) {
            const hit = found[i];
            if (hit.index < currentIndex) {
                me.gotoHit(i);
                break;
            }
        }
    }

    /**
     * Go to specified hit.
     * @param {Number} index
     */
    gotoHit(index) {
        const
            { grid } = this,
            nextHit  = this.found[index];

        if (nextHit) {
            grid.focusCell({
                field : nextHit.field,
                id    : nextHit.id
            });
        }

        return Boolean(nextHit);
    }

    /**
     * Go to the first hit.
     */
    gotoFirstHit() {
        this.gotoHit(0);
    }

    /**
     * Go to the last hit.
     */
    gotoLastHit() {
        this.gotoHit(this.found.length - 1);
    }

    //endregion

    //region Render

    /**
     * Called from SubGrid when a cell is rendered. Highlights search hits.
     * @private
     */
    renderCell({ cellElement, column, record, value }) {
        const
            me       = this,
            {
                treeWalker,
                findRe,
                hitTextCls,
                showHitIndex
            }        = me,
            hitIndex = me.foundMap?.[column.field + '-' + record.id];

        if (hitIndex) {
            // highlight cell
            cellElement.classList.add(me.hitCls);

            // Remove any previous hit badge
            showHitIndex && cellElement.querySelector(`.${me.hitCellBadgeCls}`)?.remove();

            // highlight in cell if found in innerHTML
            const inner = DomHelper.down(cellElement, '.b-grid-cell-value,.b-tree-cell-value') || cellElement;

            if (String(value).toLowerCase() === String(me.text).toLowerCase()) {
                inner.innerHTML = `<span class="${me.hitTextCls}">${inner.innerHTML}</span>${showHitIndex ? `<div class="${me.hitCellBadgeCls}">${hitIndex}</div>` : ''}`;
            }
            // Replace every occurrence of the text in every descendant text node with a span
            // encapsulating the matched string.
            else {
                treeWalker.currentNode = inner;
                for (let textNode = treeWalker.nextNode(); textNode && inner.contains(textNode);) {
                    const
                        nodeToReplace = textNode,
                        textContent   = textNode.nodeValue,
                        newText       = ['<span>'];

                    // Move onto next text node before we replace the node with a highlihght HTML sequence
                    textNode = treeWalker.nextNode();

                    let offset = findRe.lastIndex;

                    // Convert textContent into an innerHTML string which htmlEncodes the text and embeds
                    // a highlighting span which contains the target text.
                    for (let match = findRe.exec(textContent); match; match = findRe.exec(textContent)) {
                        const
                            preamble    = textContent.substring(offset, match.index),
                            spaceBefore = match[1] ? '\xa0' : '',
                            v           = match[2],
                            spaceAfter  = match[3] ? '\xa0' : '';

                        newText.push(`${StringHelper.encodeHtml(preamble)}${spaceBefore}<span class="${hitTextCls}">${StringHelper.encodeHtml(v)}</span>${spaceAfter}`);
                        offset = findRe.lastIndex;
                    }

                    newText.push(StringHelper.encodeHtml(textContent.substring(offset)), '<span>');

                    // Insert a fragment with each match wrapped with a span.
                    nodeToReplace.parentNode.insertBefore(DomHelper.createElementFromTemplate(newText.join(''), {
                        fragment : true
                    }), nodeToReplace);
                    nodeToReplace.remove();
                }

                if (showHitIndex) {
                    DomHelper.createElement({
                        parent    : cellElement,
                        className : me.hitCellBadgeCls,
                        text      : hitIndex
                    });
                }
            }

            me.hitEls.push(cellElement);
        }
    }

    //endregion

    //region Context menu

    /**
     * Add search menu item to cell context menu.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Core.data.Model} options.record Record for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateCellMenu({ column, record, items, cellElement }) {
        const me = this;

        if (column.searchable) {
            items.search = {
                text        : 'L{searchForValue}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-search',
                cls         : 'b-separator',
                weight      : 200,
                disabled    : me.disabled,
                onItem      : () => {

                    let sel = globalThis.getSelection().toString();

                    if (!sel) {
                        sel = cellElement.innerText;
                    }

                    me.search(sel);
                }
            };
        }
    }

    //endregion

    //region Events

    onStoreRefresh() {
        this.search(this.text, false, true);
    }

    //endregion
}

Search.featureClass = 'b-search';

GridFeatureManager.registerFeature(Search);
