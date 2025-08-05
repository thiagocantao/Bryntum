import BrowserHelper from '../../../../Core/helper/BrowserHelper.js';
import Exporter from './Exporter.js';
import { Orientation, PaperFormat, RowsRange } from '../Utils.js';

/**
 * @module Grid/feature/export/exporter/SinglePageExporter
 */

/**
 * A single page exporter. Used by the {@link Grid.feature.export.PdfExport} feature to export to single page. Content
 * will be scaled in both directions to fit the page.
 *
 * You do not need to use this class directly.
 *
 * ### Extending exporter
 *
 * ```javascript
 * class MySinglePageExporter extends SinglePageExporter {
 *     // type is required for exporter
 *     static get type() {
 *         return 'mysinglepageexporter';
 *     }
 *
 *     get stylesheets() {
 *         const stylesheets = super.stylesheets;
 *
 *         stylesheets.forEach(styleNodeOrLinkTag => doSmth(styleNodeOrLinkTag))
 *
 *         return stylesheets;
 *     }
 * }
 *
 * const grid = new Grid({
 *     features : {
 *         pdfExport : {
 *             // this export feature is configured with only one exporter
 *             exporters : [MySinglePageExporter]
 *         }
 *     }
 * });
 *
 * // run export with the new exporter
 * grid.features.pdfExport.export({ exporter : 'mysinglepageexporter' });
 * ```
 *
 * @classType singlepage
 * @feature
 * @extends Grid/feature/export/exporter/Exporter
 */
export default class SinglePageExporter extends Exporter {

    static get $name() {
        return 'SinglePageExporter';
    }

    static get type() {
        return 'singlepage';
    }

    static get title() {
        // In case locale is missing exporter is still distinguishable
        return this.localize('L{singlepage}');
    }

    static get defaultConfig() {
        return {
            /**
             * Set to true to center content horizontally on the page
             * @config {Boolean}
             */
            centerContentHorizontally : false
        };
    }

    async prepareComponent(config) {
        await super.prepareComponent(config);

        Object.assign(this.exportMeta, {
            verticalPages      : 1,
            horizontalPages    : 1,
            totalPages         : 1,
            currentPage        : 0,
            verticalPosition   : 0,
            horizontalPosition : 0
        });
    }

    async onRowsCollected() {}

    positionRows(rows, config) {
        if (config.enableDirectRendering) {
            return rows.map(r => r[0]);
        }
        else {
            let currentTop = 0;

            // In case of variable row height row vertical position is not guaranteed to increase
            // monotonously. Position row manually instead
            return rows.map(([html, , height]) => {
                const result = html.replace(/translate\(\d+px, \d+px\)/, `translate(0px, ${currentTop}px)`);

                currentTop += height;

                return result;
            });
        }
    }

    async collectRows(config) {
        const
            me                    = this,
            { client }            = config,
            { rowManager, store } = client,
            hasMergeCells         = client.hasActiveFeature('mergeCells'),
            { subGrids }          = me.exportMeta,
            totalRows             = config.rowsRange === RowsRange.visible && store.count
                // visibleRowCount is a projection of how much rows will fit the view, which should be
                // maximum amount of exported rows. and there can be less
                ? me.getVisibleRowsCount(client)
                : store.count;

        let { totalHeight } = me.exportMeta,
            processedRows   = 0,
            lastDataIndex   = -1;

        if (rowManager.rows.length > 0) {
            if (config.rowsRange === RowsRange.visible) {
                lastDataIndex = rowManager.firstVisibleRow.dataIndex - 1;
            }

            if (hasMergeCells) {
                for (const subGrid of Object.values(subGrids)) {
                    subGrid.mergedCellsHtml = [];
                }
            }

            // Collecting rows
            while (processedRows < totalRows) {
                const
                    rows    = rowManager.rows,
                    lastRow = rows[rows.length - 1],
                    lastProcessedRowIndex = processedRows;

                rows.forEach(row => {
                    // When we are scrolling rows will be duplicated even with disabled buffers (e.g. when we are trying to
                    // scroll last record into view). So we store last processed row dataIndex (which is always growing
                    // sequence) and filter all rows with lower/same dataIndex
                    if (row.dataIndex > lastDataIndex && processedRows < totalRows) {
                        ++processedRows;
                        totalHeight += row.offsetHeight;
                        me.collectRow(row);
                    }
                });

                // Collect merged cells per subgrid
                if (hasMergeCells) {
                    for (const subGridName in subGrids) {
                        const
                            subGrid     = subGrids[subGridName],
                            mergedCells = client.subGrids[subGridName].element.querySelectorAll(`.b-grid-merged-cells`);

                        for (const mergedCell of mergedCells) {
                            subGrid.mergedCellsHtml.push(mergedCell.outerHTML);
                        }
                    }
                }

                // Calculate new rows processed in this iteration e.g. to collect events
                const
                    firstNewRowIndex = rows.findIndex(r => r.dataIndex === lastDataIndex + 1),
                    lastNewRowIndex  = firstNewRowIndex + (processedRows - lastProcessedRowIndex);

                await me.onRowsCollected(rows.slice(firstNewRowIndex, lastNewRowIndex), config);

                if (processedRows < totalRows) {
                    lastDataIndex = lastRow.dataIndex;
                    await me.scrollRowIntoView(client, lastDataIndex + 1);
                }
            }
        }

        return totalHeight;
    }

    async renderRows(config) {
        const
            me                    = this,
            { client, rowsRange } = config,
            { rowManager, store } = client,
            hasMergeCells         = client.hasActiveFeature('mergeCells'),
            onlyVisibleRows       = rowsRange === RowsRange.visible;

        let { totalHeight } = me.exportMeta;

        if (store.count) {
            const
                { fakeRow }         = me.exportMeta,
                { firstVisibleRow } = rowManager,
                fromIndex           = onlyVisibleRows ? firstVisibleRow.dataIndex : 0,
                toIndex             = onlyVisibleRows ? rowManager.lastVisibleRow.dataIndex : store.count - 1,
                rows                = [];

            let top = 0;

            // Fake row might not have cells if there are no columns
            if (fakeRow.cells.length) {
                for (let i = fromIndex; i <= toIndex; i++) {
                    fakeRow.render(i, store.getAt(i), true, false, true);

                    top = fakeRow.translate(top);

                    me.collectRow(fakeRow);

                    // Push an object with data required to build merged cell
                    rows.push({
                        top          : fakeRow.top,
                        bottom       : fakeRow.bottom,
                        offsetHeight : fakeRow.offsetHeight,
                        dataIndex    : fakeRow.dataIndex
                    });
                }

                await me.onRowsCollected(rows, config);
            }

            totalHeight += top;

            if (hasMergeCells) {
                me.renderMergedCells(config, fromIndex, toIndex, rows);
            }
        }

        return totalHeight;
    }

    buildPageHtml(config) {
        const
            me           = this,
            { subGrids } = me.exportMeta;

        // Now when rows are collected, we need to add them to exported grid
        let html = me.prepareExportElement();

        Object.values(subGrids).forEach(({ placeHolder, rows, mergedCellsHtml }) => {
            const placeHolderText = placeHolder.outerHTML;
            let contentHtml =  me.positionRows(rows, config).join('');

            if (mergedCellsHtml?.length) {
                contentHtml += `<div class="b-grid-merged-cells-container">${mergedCellsHtml.join('')}</div>`;
            }

            html = html.replace(placeHolderText, contentHtml);
        });

        return html;
    }
}

// HACK: terser/obfuscator doesn't yet support async generators, when processing code it converts async generator to regular async
// function.
SinglePageExporter.prototype.pagesExtractor = async function * pagesExtractor(config) {
    // When we prepared grid we stretched it horizontally, now we need to gather all rows
    // There are two ways:
    // 1. set component height to scrollable.scrollHeight value to render all rows at once (maybe a bit more complex
    // if rows have variable height)
    // 2. iterate over rows, scrolling new portion into view once in a while
    // #1 sounds simpler, but that might require too much rendering, let's scroll rows instead

    const
        me             = this,
        { client }     = config,
        { totalWidth } = me.exportMeta,
        styles         = me.stylesheets,
        portrait       = config.orientation === Orientation.portrait,
        paperFormat    = PaperFormat[config.paperFormat],
        paperWidth     = portrait ? paperFormat.width : paperFormat.height,
        paperHeight    = portrait ? paperFormat.height : paperFormat.width;

    let totalHeight, header, footer;

    if (config.enableDirectRendering) {
        totalHeight = await me.renderRows(config);

        totalHeight += client.headerHeight + client.footerHeight;
    }
    else {
        totalHeight = await me.collectRows(config);

        totalHeight += client.height - client.bodyHeight;
    }

    const html = me.buildPageHtml(config);

    const totalClientHeight = totalHeight;

    // Measure header and footer height
    if (config.headerTpl) {
        header = me.prepareHTML(config.headerTpl({ totalWidth }));
        const height = me.measureElement(header);
        totalHeight += height;
    }

    if (config.footerTpl) {
        footer = me.prepareHTML(config.footerTpl({ totalWidth }));
        const height = me.measureElement(footer);
        totalHeight += height;
    }

    const
        widthScale  = Math.min(1, me.getScaleValue(me.inchToPx(paperWidth), totalWidth)),
        heightScale = Math.min(1, me.getScaleValue(me.inchToPx(paperHeight), totalHeight)),
        scale       = Math.min(widthScale, heightScale);

    // Now add style to stretch grid vertically
    styles.push(
        `<style>
                #${client.id} {
                    height: ${totalClientHeight}px !important;
                    width: ${totalWidth}px !important;
                }
                
                .b-export-content {
                    ${me.centerContentHorizontally ? 'left: 50%;' : ''}
                    transform: scale(${scale}) ${me.centerContentHorizontally ? 'translateX(-50%)' : ''};
                    transform-origin: top left;
                    height: ${scale === 1 ? 'inherit' : 'auto !important'};
                }
            </style>`
    );

    if (BrowserHelper.isIE11) {
        styles.push(
        `<style>
                .b-export-body {
                   min-height: ${totalClientHeight}px !important;
                }
         </style>`
        );
    }

    // This is a single page exporter so we only yield one page
    yield {
        html : me.pageTpl({
            html,
            header,
            footer,
            styles,
            paperWidth,
            paperHeight
        })
    };
};
