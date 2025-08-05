import Exporter from './Exporter.js';
import { Orientation, PaperFormat, RowsRange } from '../Utils.js';

/**
 * @module Grid/feature/export/exporter/MultiPageVerticalExporter
 */

/**
 * A vertical multiple page exporter. Used by the {@link Grid.feature.export.PdfExport} feature to export to multiple
 * pages. Content will be scaled in a horizontal direction to fit the page.
 *
 * You do not need to use this class directly.
 *
 * ### Extending exporter
 *
 * ```javascript
 * class MyMultiPageVerticalExporter extends MultiPageVerticalExporter {
 *     // type is required for exporter
 *     static get type() {
 *         return 'mymultipageverticalexporter';
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
 *             exporters : [MyMultiPageVerticalExporter]
 *         }
 *     }
 * });
 *
 * // run export with the new exporter
 * grid.features.pdfExport.export({ exporter : 'mymultipageverticalexporter' });
 * ```
 *
 * @classType multipagevertical
 * @feature
 * @extends Grid/feature/export/exporter/Exporter
 */
export default class MultiPageVerticalExporter extends Exporter {

    static get $name() {
        return 'MultiPageVerticalExporter';
    }

    static get type() {
        return 'multipagevertical';
    }

    static get title() {
        // In case locale is missing exporter is still distinguishable
        return this.L('L{multipagevertical}');
    }

    static get exportingPageText() {
        return 'L{exportingPage}';
    }

    //region State management

    async stateNextPage({ client }) {
        const
            { exportMeta } = this,
            {
                totalRows,
                processedRows,
                totalPages
            } = exportMeta;

        ++exportMeta.currentPage;
        ++exportMeta.verticalPosition;

        // With variable row heights it is possible that initial pages estimation is wrong. If we're out but there are
        // more rows to process - continue exporting
        if (exportMeta.currentPage === totalPages && processedRows.size !== totalRows) {
            ++exportMeta.totalPages;
            ++exportMeta.verticalPages;
        }
    }

    //endregion

    estimateTotalPages(config) {
        const
            me             = this,
            { exportMeta } = me,
            {
                client,
                headerTpl,
                footerTpl,
                alignRows,
                rowsRange,
                repeatHeader,
                enableDirectRendering
            }              = config,
            {
                pageWidth,
                pageHeight,
                totalWidth
            }              = exportMeta,
            scale          = me.getScaleValue(pageWidth, totalWidth);

        // To estimate amount of pages correctly we need to know height of the header/footer on every page
        let
            // bodyHeight does not always report correct value, read it from the DOM element instead, we don't care
            // about forced reflow at this stage
            totalHeight   = 0 - me.getVirtualScrollerHeight(client) + client.height - client.bodyElement.offsetHeight + client.scrollable.scrollHeight,
            // We will be scaling content horizontally, need to adjust content height accordingly
            contentHeight = pageHeight / scale,
            totalRows     = client.store.count,
            initialScroll = 0,
            rowsHeight    = totalHeight,
            verticalPages;

        if (headerTpl) {
            contentHeight -= me.measureElement(headerTpl({
                totalWidth,
                totalPages  : -1,
                currentPage : -1
            }));
        }

        if (footerTpl) {
            contentHeight -= me.measureElement(footerTpl({
                totalWidth,
                totalPages  : -1,
                currentPage : -1
            }));
        }

        // If we are repeating header on every page we have smaller contentHeight
        if (repeatHeader) {
            contentHeight -= client.headerHeight + client.footerHeight;
            totalHeight -= client.headerHeight + client.footerHeight;
        }

        if (rowsRange === RowsRange.visible) {
            const
                rowManager = client.rowManager,
                firstRow = rowManager.firstVisibleRow,
                lastRow  = rowManager.lastVisibleRow;

            // With direct rendering we start rendering from 0, no need to adjust anything
            if (!enableDirectRendering) {
                initialScroll = firstRow.top;
            }

            totalRows = me.getVisibleRowsCount(client);

            if (enableDirectRendering) {
                totalHeight = client.headerHeight + client.footerHeight + lastRow.bottom - firstRow.top;
                rowsHeight = lastRow.bottom - firstRow.top;
            }
            else {
                rowsHeight = totalHeight = totalHeight - client.scrollable.scrollHeight + lastRow.bottom - firstRow.top;
            }

            exportMeta.lastRowIndex = firstRow.dataIndex;
            exportMeta.finishRowIndex = lastRow.dataIndex;
        }
        else {
            exportMeta.finishRowIndex = client.store.count - 1;
        }

        // alignRows config specifies if rows should be always fully visible. E.g. if row doesn't fit on the page, it goes
        // to the top of the next page
        if (alignRows && !repeatHeader && rowsRange !== RowsRange.visible) {
            // we need to estimate amount of vertical pages for case when we only put row on the page if it fits
            // first we need to know how much rows would fit one page, keeping in mind first page also contains header
            // This estimation is loose, because row height might differ much between pages
            const
                rowHeight       = client.rowManager.rowOffsetHeight,
                rowsOnFirstPage = Math.floor((contentHeight - client.headerHeight) / rowHeight),
                rowsPerPage     = Math.floor(contentHeight / rowHeight),
                remainingRows   = totalRows - rowsOnFirstPage;

            verticalPages = 1 + Math.ceil(remainingRows / rowsPerPage);
        }
        else {
            verticalPages = Math.ceil(rowsHeight / contentHeight);
        }

        Object.assign(exportMeta, {
            scale,
            contentHeight,
            totalRows,
            totalHeight,
            verticalPages,
            initialScroll,
            horizontalPages : 1,
            totalPages      : verticalPages
        });
    }

    async prepareComponent(config) {
        await super.prepareComponent(config);

        const
            me              = this,
            { exportMeta }  = me,
            { client }      = config,
            paperFormat     = PaperFormat[config.paperFormat],
            isPortrait      = config.orientation === Orientation.portrait,
            paperWidth      = isPortrait ? paperFormat.width : paperFormat.height,
            paperHeight     = isPortrait ? paperFormat.height : paperFormat.width,
            pageWidth       = me.inchToPx(paperWidth),
            pageHeight      = me.inchToPx(paperHeight),
            horizontalPages = 1;

        Object.assign(exportMeta, {
            paperWidth,
            paperHeight,
            pageWidth,
            pageHeight,
            horizontalPages,
            currentPage          : 0,
            verticalPosition     : 0,
            horizontalPosition   : 0,
            currentPageTopMargin : 0,
            lastTop              : 0,
            lastRowIndex         : 0,
            processedRows        : new Set()
        });

        me.estimateTotalPages(config);

        if (!config.enableDirectRendering) {
            me.adjustRowBuffer(client);
        }
    }

    async restoreComponent(config) {
        await super.restoreComponent(config);

        if (!config.enableDirectRendering) {
            this.restoreRowBuffer(config.client);
        }
    }

    async collectRows(config) {
        const
            me                 = this,
            { exportMeta }     = me,
            {
                client,
                alignRows,
                repeatHeader
            }                  = config,
            {
                subGrids,
                currentPageTopMargin,
                verticalPosition,
                totalRows,
                contentHeight
            }                  = exportMeta,
            // If we are repeating header we've already took header height into account when setting content height
            clientHeaderHeight = repeatHeader ? 0 : client.headerHeight,
            { rowManager }     = client,
            { rows }           = rowManager,
            onlyVisibleRows    = config.rowsRange === RowsRange.visible,
            hasMergeCells      = client.hasActiveFeature('mergeCells');

        let index = onlyVisibleRows
                ? rows.findIndex(r => r.bottom > client.scrollable.y)
                : rows.findIndex(r => r.bottom + currentPageTopMargin + clientHeaderHeight > 0),
            remainingHeight;

        const
            firstRowIndex     = index,
            // This is a portion of the row which is not visible, which means it shouldn't affect remaining height
            // Don't calculate for the first page
            overflowingHeight = verticalPosition === 0 ? 0 : rows[index].top + currentPageTopMargin + clientHeaderHeight;

        // Calculate remaining height to fill with rows
        // remainingHeight is height of the page content region to fill. When next row is exported, this heights gets
        // reduced. Since top rows may be partially visible, it would lead to increasing error and eventually to incorrect
        // exported rows for the page
        remainingHeight = contentHeight - overflowingHeight;

        // first exported page container header
        if (verticalPosition === 0) {
            remainingHeight -= clientHeaderHeight;
        }

        // data index of the last collected row
        let lastDataIndex,
            offset = 0;

        while (remainingHeight > 0) {
            const row = rows[index];

            if (alignRows && remainingHeight < row.offsetHeight) {
                offset = -remainingHeight;
                remainingHeight = 0;
            }
            else {
                me.collectRow(row);

                remainingHeight -= row.offsetHeight;

                // only mark row as processed if it fitted without overflow
                if (remainingHeight > 0) {
                    // We cannot use simple counter here because some rows appear on 2 pages. Need to track unique identifier
                    exportMeta.processedRows.add(row.dataIndex);
                }

                lastDataIndex = row.dataIndex;

                // Last row is processed, still need to fill the view
                if (++index === rows.length && remainingHeight > 0) {
                    remainingHeight = 0;
                }
                else if (onlyVisibleRows && (index - firstRowIndex) === totalRows) {
                    remainingHeight = 0;
                }
            }
        }

        // Collect merged cells per subgrid
        if (hasMergeCells) {
            for (const subGridName in subGrids) {
                const
                    subGrid     = subGrids[subGridName],
                    mergedCells = client.subGrids[subGridName].element.querySelectorAll(`.b-grid-merged-cells`);

                subGrid.mergedCellsHtml = [];

                for (const mergedCell of mergedCells) {
                    subGrid.mergedCellsHtml.push(mergedCell.outerHTML);
                }
            }
        }

        const lastRow = rows[index - 1];

        if (lastRow) {
            // Calculate exact grid height according to the last exported row
            exportMeta.exactGridHeight = lastRow.bottom + client.footerContainer.offsetHeight + client.headerContainer.offsetHeight;
        }

        await me.onRowsCollected(rows.slice(firstRowIndex, index), config);

        // No scrolling required if we are only exporting currently visible rows
        if (onlyVisibleRows) {
            exportMeta.scrollableTopMargin = client.scrollable.y;
        }
        else {
            // With variable row height row manager might relayout rows to fix position, moving them up or down.
            const detacher = rowManager.ion({ offsetRows : ({ offset : value }) => offset += value });

            await me.scrollRowIntoView(client, lastDataIndex + 1);

            detacher();
        }

        return offset;
    }

    async renderRows(config) {
        const
            me                    = this,
            { exportMeta }        = me,
            {
                client,
                alignRows,
                repeatHeader
            }                     = config,
            {
                currentPageTopMargin,
                verticalPosition,
                totalRows,
                contentHeight,
                lastRowIndex,
                finishRowIndex,
                fakeRow
            }                     = exportMeta,
            // If we are repeating header we've already took header height into account when setting content height
            clientHeaderHeight    = repeatHeader ? 0 : client.headerHeight,
            { store }             = client,
            hasMergeCells         = client.hasActiveFeature('mergeCells'),
            onlyVisibleRows       = config.rowsRange === RowsRange.visible;

        let index       = lastRowIndex,
            { lastTop } = exportMeta,
            remainingHeight;

        const
            firstRowIndex     = index,
            // This is a portion of the row which is not visible, which means it shouldn't affect remaining height
            // Don't calculate for the first page
            overflowingHeight = verticalPosition === 0 ? 0 : lastTop + currentPageTopMargin + clientHeaderHeight,
            rows              = [];

        // Calculate remaining height to fill with rows
        // remainingHeight is height of the page content region to fill. When next row is exported, this heights gets
        // reduced. Since top rows may be partially visible, it would lead to increasing error and eventually to incorrect
        // exported rows for the page
        remainingHeight = contentHeight - overflowingHeight;

        // first exported page container header
        if (verticalPosition === 0) {
            remainingHeight -= clientHeaderHeight;
        }

        // data index of the last collected row
        let lastDataIndex,
            nextPageTop,
            offset = 0;

        while (remainingHeight > 0) {
            fakeRow.render(index, store.getAt(index), true, false, true);

            if (alignRows && remainingHeight < fakeRow.offsetHeight) {
                offset = -remainingHeight;
                remainingHeight = 0;
            }
            else {
                nextPageTop = lastTop;
                lastDataIndex = index;

                lastTop = fakeRow.translate(lastTop);
                remainingHeight -= fakeRow.offsetHeight;

                me.collectRow(fakeRow);

                // Push an object with data required to build merged cell
                rows.push({
                    top          : fakeRow.top,
                    bottom       : fakeRow.bottom,
                    offsetHeight : fakeRow.offsetHeight,
                    dataIndex    : fakeRow.dataIndex
                });

                // only mark row as processed if it fitted without overflow
                if (remainingHeight > 0) {
                    // We cannot use simple counter here because some rows appear on 2 pages. Need to track unique identifier
                    exportMeta.processedRows.add(index);
                }

                // Last row is processed, still need to fill the view
                if (index === finishRowIndex) {
                    remainingHeight = 0;
                }
                else if ((++index - firstRowIndex) === totalRows && onlyVisibleRows) {
                    remainingHeight = 0;
                }
            }
        }

        if (hasMergeCells) {
            me.renderMergedCells(config, firstRowIndex, index, rows);
        }

        // Store next to last row index and top position so we could proceed on the next page.
        // In fact, when we take full control of row rendering we don't even need to do this. It is only required
        // to be compatible with current exporters. When we get rid of scrolling, we can just start rendering rows
        // on each page from 0 (adjusted by overflow of the previous row)
        exportMeta.lastRowIndex = lastDataIndex;
        exportMeta.lastTop = nextPageTop;

        if (fakeRow) {
            // Calculate exact grid height according to the last exported row to constrain column lines to the last
            // row
            exportMeta.exactGridHeight = fakeRow.bottom + client.footerContainer.offsetHeight + client.headerContainer.offsetHeight;
        }

        await me.onRowsCollected(rows, config);

        return offset;
    }

    async buildPage(config) {
        const
            me             = this,
            { exportMeta } = me,
            {
                client,
                headerTpl,
                footerTpl,
                enableDirectRendering
            }              = config,
            {
                totalWidth,
                totalPages,
                currentPage,
                subGrids
            }              = exportMeta;

        // Rows are stored in shared state object, need to clean it before exporting next page
        Object.values(subGrids).forEach(subGrid => subGrid.rows = []);

        // With variable row height total height might change after scroll, update it
        // to show content completely on the last page
        if (config.rowsRange === RowsRange.all) {
            exportMeta.totalHeight = client.headerHeight + client.footerHeight + client.scrollable.scrollHeight;

            if (!enableDirectRendering) {
                exportMeta.totalHeight -= me.getVirtualScrollerHeight(client);
            }
        }

        let header, footer, offset;

        // Measure header and footer height
        if (headerTpl) {
            header = me.prepareHTML(headerTpl({
                totalWidth,
                totalPages,
                currentPage
            }));
        }

        if (footerTpl) {
            footer = me.prepareHTML(footerTpl({
                totalWidth,
                totalPages,
                currentPage
            }));
        }

        if (enableDirectRendering) {
            offset = await me.renderRows(config);
        }
        else {
            offset = await me.collectRows(config);
        }

        const html = me.buildPageHtml(config);

        return { html, header, footer, offset };
    }

    async onRowsCollected() {}

    buildPageHtml() {
        const
            me           = this,
            { subGrids } = me.exportMeta;

        // Now when rows are collected, we need to add them to exported grid
        let html = me.prepareExportElement();

        Object.values(subGrids).forEach(({ placeHolder, rows, mergedCellsHtml }) => {
            const placeHolderText = placeHolder.outerHTML;

            let contentHtml = rows.reduce((result, row) => {
                result += row[0];

                return result;
            }, '');

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
MultiPageVerticalExporter.prototype.pagesExtractor = async function * pagesExtractor(config) {
    const
        me = this,
        {
            exportMeta,
            stylesheets
        }  = me,
        {
            totalWidth,
            paperWidth,
            paperHeight,
            contentHeight,
            scale,
            initialScroll
        }  = exportMeta;

    let
        { totalPages } = exportMeta,
        currentPage;

    while ((currentPage = exportMeta.currentPage) < totalPages) {
        me.trigger('exportStep', {
            text     : me.L(MultiPageVerticalExporter.exportingPageText, { currentPage, totalPages }),
            progress : Math.round(((currentPage + 1) / totalPages) * 90)
        });

        const { html, header, footer, offset } = await me.buildPage(config);

        // TotalHeight might change in case of variable row heights
        // Move exported content in the visible frame
        const styles = [
            ...stylesheets,
            `
                <style>
                    #${config.client.id} {
                        width: ${totalWidth}px !important;
                    }
                    
                    .b-export .b-export-content {
                        transform: scale(${scale});
                        transform-origin: top left;
                        height: auto;
                    }
                </style>
            `
        ];

        if (config.repeatHeader) {
            const gridHeight = exportMeta.exactGridHeight ? `${exportMeta.exactGridHeight + exportMeta.currentPageTopMargin}px` : '100%';

            styles.push(
                `
                <style>
                    #${config.client.id} {
                        height: ${gridHeight} !important;
                    }
                    
                    .b-export .b-export-content {
                        height: ${100 / scale}%;
                    }
                    
                    .b-export-body {
                        height: 100%;
                        display: flex;
                    }
                
                    .b-export-viewport {
                        height: 100%;
                    }
                    
                    .b-grid-vertical-scroller {
                        margin-top: ${exportMeta.currentPageTopMargin - initialScroll}px;
                    }
                </style>
                `
            );
        }
        else {
            const gridHeight = exportMeta.exactGridHeight || (contentHeight - exportMeta.currentPageTopMargin);

            styles.push(
                `
                <style>
                    #${config.client.id} {
                        height: ${gridHeight}px !important;
                    }
                    
                    .b-export-body {
                        overflow: hidden;
                    }
                    
                    .b-export .b-export-content {
                        height: ${100 / scale}%;
                    }
                    
                    .b-export-body .b-export-viewport {
                        margin-top: ${exportMeta.currentPageTopMargin}px;
                    }
                    
                    .b-grid-vertical-scroller {
                        margin-top: -${initialScroll}px;
                    }
                </style>
                `
            );
        }

        // when aligning rows, offset gets accumulated, so we need to take it into account
        exportMeta.currentPageTopMargin -= contentHeight + offset;

        await me.stateNextPage(config);

        ({ totalPages } = exportMeta);

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
    }
};
