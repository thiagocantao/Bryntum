import DateHelper from '../../../../Core/helper/DateHelper.js';
import Rectangle from '../../../../Core/helper/util/Rectangle.js';
import DomHelper from '../../../../Core/helper/DomHelper.js';
import DomSync from '../../../../Core/helper/DomSync.js';
import { ScheduleRange } from '../Utils.js';

const immediatePromise = Promise.resolve();

export default base => class SchedulerExporterMixin extends base {
    async scrollRowIntoView(client, index) {
        const
            {
                rowManager,
                scrollable
            }    = client,
            oldY = scrollable.y;

        // If it's a valid index to scroll to, then try it.
        if (index < client.store.count) {
            // Scroll the requested row to the viewport top
            scrollable.scrollTo(null, rowManager.calculateTop(index));

            // If that initiated a scroll, we need to wait for the row to be rendered, so return
            // a Promise which resolves when that happens.
            if (scrollable.y !== oldY) {
                // GridBase adds listener to vertical scroll to update rows. Rows might be or might not be updated,
                // but at the end of each scroll grid will trigger `scroll` event. So far this is the only scroll event
                // triggered by the grid itself and it is different from `scroll` event on scrollable.
                return new Promise(resolve => {
                    const detacher = client.ion({
                        scroll({ scrollTop }) {
                            // future-proof: only react to scroll event with certain argument
                            if (scrollTop != null && rowManager.getRow(index)) {
                                detacher();
                                resolve();
                            }
                        }
                    });
                });
            }
        }

        // No scroll occurred. Promise must be resolved immediately
        return immediatePromise;
    }

    async scrollToDate(client, date) {
        let scrollFired = false;

        const promises    = [];

        // Time axis is updated on element scroll, which is async event. We need to synchronize this logic.
        // If element horizontal scroll is changed then sync event is fired. We add listener to that one specific event
        // and remove it right after scrollToDate sync code, keeping listeners clean. If scrolling occurred, we need
        // to wait until time header is updated.
        const detacher = client.timeAxisSubGrid.scrollable.ion({
            scrollStart({ x }) {
                if (x != null) {
                    scrollFired = true;
                }
            }
        });

        // added `block: start` to do scrolling faster
        // it moves data to begin of visible area that is longer section for re-render
        promises.push(client.scrollToDate(date, { block : 'start' }));

        detacher();

        if (scrollFired) {
            // We have to wait for scrollEnd event before moving forward. When exporting large view we might have to scroll
            // extensively and it might occur that requested scroll position would not be reached because concurrent
            // scrollEnd events would move scroll back.
            // scrollEnd is on a 100ms timer *after* the last scroll event fired, so all necessary
            // updated will have occurred.
            // Covered by Gantt/tests/feature/export/MultiPageVertical.t.js
            promises.push(client.timeAxisSubGrid.header.scrollable.await('scrollEnd', { checkLog : false }));
        }

        await Promise.all(promises);
    }

    cloneElement(element, target, clear) {
        super.cloneElement(element, target, clear);

        const clonedEl = this.element.querySelector('.b-schedulerbase');

        // Remove default animation classes
        clonedEl?.classList.remove(...['fade-in', 'slide-from-left', 'slide-from-top', 'zoom-in'].map(name => `b-initial-${name}`));
    }

    async prepareComponent(config) {
        const
            me                     = this,
            { client }             = config,
            { currentOrientation } = client,
            includeTimeline        = client.timeAxisSubGrid.width > 0;

        switch (config.scheduleRange) {
            case ScheduleRange.completeview:
                config.rangeStart = client.startDate;
                config.rangeEnd   = client.endDate;
                break;
            case ScheduleRange.currentview: {
                const { startDate, endDate } = client.visibleDateRange;
                config.rangeStart = startDate;
                config.rangeEnd = endDate;
                break;
            }
        }

        await client.waitForAnimations();

        // Disable infinite scroll before export, so it doesn't change time span
        config.infiniteScroll = client.infiniteScroll;
        client.infiniteScroll = false;

        // Don't change timespan if time axis subgrid is not visible
        if (includeTimeline) {
            // set new timespan before calling parent to get proper scheduler header/content size
            client.setTimeSpan(config.rangeStart, config.rangeEnd);

            // Access svgCanvas el to create dependency canvas early
            client.svgCanvas;
        }

        // Disable event animations during export
        me._oldEnableEventAnimations = client.enableEventAnimations;
        client.enableEventAnimations = false;

        // Add scroll buffer for the horizontal rendering
        if (currentOrientation.isHorizontalRendering) {
            me._oldScrollBuffer = currentOrientation.scrollBuffer;
            me._oldVerticalBuffer = currentOrientation.verticalBufferSize;
            currentOrientation.scrollBuffer = 100;
            currentOrientation.verticalBufferSize = -1;
        }

        // Raise flag on the client to render all suggested dependencies
        client.ignoreViewBox = true;

        await super.prepareComponent(config);

        const
            { exportMeta, element } = me,
            fgCanvasEl              = element.querySelector('.b-sch-foreground-canvas'),
            timeAxisEl              = element.querySelector('.b-horizontaltimeaxis');

        exportMeta.includeTimeline = includeTimeline;

        if (includeTimeline && config.scheduleRange !== ScheduleRange.completeview) {
            // If we are exporting subrange of dates we need to change subgrid size accordingly
            exportMeta.totalWidth -= exportMeta.subGrids.normal.width;
            exportMeta.totalWidth += exportMeta.subGrids.normal.width = client.timeAxisViewModel.getDistanceBetweenDates(config.rangeStart, config.rangeEnd);

            const
                horizontalPages = Math.ceil(exportMeta.totalWidth / exportMeta.pageWidth),
                totalPages      = horizontalPages * exportMeta.verticalPages;

            exportMeta.horizontalPages = horizontalPages;
            exportMeta.totalPages = totalPages;

            // store left scroll to imitate normal grid/header scroll using margin
            exportMeta.subGrids.normal.scrollLeft = client.getCoordinateFromDate(config.rangeStart);
        }

        exportMeta.timeAxisHeaders = [];
        exportMeta.timeAxisPlaceholders = [];
        exportMeta.headersColleted = false;

        DomHelper.forEachSelector(timeAxisEl, '.b-sch-header-row', headerRow => {
            exportMeta.timeAxisPlaceholders.push(me.createPlaceholder(headerRow));
            exportMeta.timeAxisHeaders.push(new Map());
        });

        // Add placeholder for events, clear all event elements, but not the entire elements as it contains svg canvas
        exportMeta.subGrids.normal.eventsPlaceholder = me.createPlaceholder(fgCanvasEl, false);
        DomHelper.removeEachSelector(fgCanvasEl, '.b-sch-event-wrap,.b-sch-resourcetimerange');

        DomHelper.removeEachSelector(me.element, '.b-released');

        exportMeta.eventsBoxes = new Map();
        exportMeta.client = client;

        if (client.hasActiveFeature('columnLines')) {
            const columnLinesCanvas = element.querySelector('.b-column-lines-canvas');

            exportMeta.columnLinesPlaceholder = me.createPlaceholder(columnLinesCanvas);

            exportMeta.columnLines = { lines : new Map(), majorLines : new Map() };
        }

        if (client.hasActiveFeature('timeRanges')) {
            const
                timeRangesHeaderCanvas = element.querySelector('.b-sch-timeaxiscolumn .b-sch-timeranges-canvas'),
                timeRangesBodyCanvas = element.querySelector('.b-sch-foreground-canvas .b-sch-timeranges-canvas');

            exportMeta.timeRanges = {};

            // header is optional
            if (timeRangesHeaderCanvas) {
                exportMeta.timeRanges.header = config.enableDirectRendering ? '' : {};
                exportMeta.timeRangesHeaderPlaceholder = me.createPlaceholder(timeRangesHeaderCanvas);
            }

            exportMeta.timeRanges.body = config.enableDirectRendering ? '' : {};
            exportMeta.timeRangesBodyPlaceholder = me.createPlaceholder(timeRangesBodyCanvas);
        }

        if (client.hasActiveFeature('dependencies')) {
            client.features.dependencies.fillDrawingCache();

            const svgCanvasEl = element.querySelector(`[id="${client.svgCanvas.getAttribute('id')}"]`);

            // Same as above, clear only dependency lines, because there might be markers added by user
            if (svgCanvasEl) {
                exportMeta.dependencyCanvasEl = svgCanvasEl;

                exportMeta.dependenciesPlaceholder = me.createPlaceholder(svgCanvasEl, false, {
                    ns  : 'http://www.w3.org/2000/svg',
                    tag : 'path'
                });
                DomHelper.removeEachSelector(svgCanvasEl, '.b-sch-dependency');
            }
        }

        // We need to scroll component to date to calculate correct start margin
        if (includeTimeline && !DateHelper.betweenLesser(config.rangeStart, client.startDate, client.endDate)) {
            await me.scrollToDate(client, config.rangeStart);
        }
    }

    async restoreState(config) {
        let waitForHorizontalScroll = false;

        const
            { client } = config,
            promises = [];

        // If scroll will be changed during restoring state (and it will likely be), raise a flag that exporter should
        // wait for scrollEnd event before releasing control
        const detacher = client.timeAxisSubGrid.scrollable.ion({
            scrollStart({ x }) {
                // HACK: scrollStart might actually fire when scroll is set to existing value
                if (this.element.scrollLeft !== x) {
                    waitForHorizontalScroll = true;
                }
            }
        });

        promises.push(super.restoreState(config));

        // Scroll start will be fired synchronously
        detacher();

        if (waitForHorizontalScroll) {
            promises.push(client.timeAxisSubGrid.header.scrollable.await('scrollEnd', { checkLog : false }));
        }

        await Promise.all(promises);
    }

    async restoreComponent(config) {
        const
            { client }             = config,
            { currentOrientation } = client;

        client.ignoreViewBox = false;

        client.infiniteScroll = config.infiniteScroll;

        client.enableEventAnimations = this._oldEnableEventAnimations;

        if (currentOrientation.isHorizontalRendering) {
            currentOrientation.scrollBuffer = this._oldScrollBuffer;
            currentOrientation.verticalBufferSize = this._oldVerticalBuffer;
        }

        await super.restoreComponent(config);
    }

    async onRowsCollected(rows, config) {
        const me = this;

        await super.onRowsCollected(rows, config);

        // Only collect this data if timeline is visible
        if (me.exportMeta.includeTimeline) {
            const
                { client, enableDirectRendering } = config,
                { timeView }                      = client,
                { pageRangeStart, pageRangeEnd }  = me.getCurrentPageDateRange(config);

            if (enableDirectRendering) {
                // If first page does not include timeline we don't need to render anything for it
                if (pageRangeStart && pageRangeEnd) {
                    me.renderHeaders(config, pageRangeStart, pageRangeEnd);

                    me.renderLines(config, pageRangeStart, pageRangeEnd);

                    me.renderRanges(config, pageRangeStart, pageRangeEnd);

                    me.renderEvents(config, rows, pageRangeStart, pageRangeEnd);
                }
            }
            else {
                // Exported page may not contain timeline view, in which case we need to fall through
                if (pageRangeStart) {
                    let rangeProcessed = false;

                    await me.scrollToDate(client, pageRangeStart);

                    // Time axis and events are only rendered for the visible time span
                    // we need to scroll the view and gather events/timeline elements
                    // while (timeView.endDate <= config.rangeEnd) {
                    while (!rangeProcessed) {
                        me.collectLines(config);

                        me.collectHeaders(config);

                        me.collectRanges(config);

                        me.collectEvents(rows, config);

                        if (DateHelper.timeSpanContains(timeView.startDate, timeView.endDate, pageRangeStart, pageRangeEnd)) {
                            rangeProcessed = true;
                        }
                        else if (timeView.endDate.getTime() >= pageRangeEnd.getTime()) {
                            rangeProcessed = true;
                        }
                        else {
                            const endDate = timeView.endDate;

                            await me.scrollToDate(client, timeView.endDate);

                            // If timeview end date is same as before scroll it means client is not able to scroll to date
                            // and will go into infinite loop unless we stop it
                            if (endDate.getTime() === timeView.endDate.getTime()) {
                                throw new Error('Could not scroll to date');
                            }
                        }
                    }
                }

                await me.scrollToDate(client, config.rangeStart);
            }
        }
    }

    getCurrentPageDateRange({ rangeStart, rangeEnd, enableDirectRendering, client }) {
        const
            me = this,
            { exportMeta } = me,
            { horizontalPages, horizontalPosition, pageWidth, subGrids } = exportMeta;

        let pageRangeStart, pageRangeEnd;

        // when exporting to multiple pages we only need to scroll sub-range within visible time span
        if (horizontalPages > 1) {
            const
                pageStartX = horizontalPosition * pageWidth,
                pageEndX   = (horizontalPosition + 1) * pageWidth,
                // Assuming normal grid is right next to right side of the locked grid
                // There is also a default splitter
                normalGridX = subGrids.locked.width + subGrids.locked.splitterWidth;

            if (pageEndX <= normalGridX) {
                pageRangeEnd = pageRangeStart = null;
            }
            else {
                const { scrollLeft = 0 } = subGrids.normal;

                pageRangeStart = client.getDateFromCoordinate(Math.max(pageStartX - normalGridX + scrollLeft, 0));

                // Extend visible schedule by 20% to cover up possible splitter
                const multiplier = enableDirectRendering ? 1 : 1.2;

                pageRangeEnd = client.getDateFromCoordinate((pageEndX - normalGridX + scrollLeft) * multiplier) || rangeEnd;
            }
        }
        else {
            pageRangeStart = rangeStart;
            pageRangeEnd   = rangeEnd;
        }

        return {
            pageRangeStart,
            pageRangeEnd
        };
    }

    prepareExportElement() {
        const
            { element, exportMeta }                = this,
            { id, headerId, footerId, scrollLeft } = exportMeta.subGrids.normal,
            el                                     = element.querySelector(`[id="${id}"]`);

        ['.b-sch-background-canvas', '.b-sch-foreground-canvas'].forEach(selector => {
            const canvasEl = el.querySelector(selector);

            if (canvasEl) {
                // Align canvases to last exported row bottom. If no such property exists - remove inline height
                if (exportMeta.lastExportedRowBottom) {
                    canvasEl.style.height = `${exportMeta.lastExportedRowBottom}px`;
                }
                else {
                    canvasEl.style.height = '';
                }

                // Simulate horizontal scroll
                if (scrollLeft) {
                    canvasEl.style.marginLeft = `-${scrollLeft}px`;
                }
            }
        });

        if (scrollLeft) {
            [headerId, footerId].forEach(id => {
                const el = element.querySelector(`[id="${id}"] .b-widget-scroller`);
                if (el) {
                    el.style.marginLeft = `-${scrollLeft}px`;
                }
            });
        }

        return super.prepareExportElement();
    }

    collectHeaders(config) {
        const
            me             = this,
            { client }     = config,
            { exportMeta } = me;

        // We only need to collect headers once, this flag is raised once they are collected along all exported range
        if (!exportMeta.headersCollected) {
            const
                timeAxisEl = client.timeView.element,
                timeAxisHeaders = exportMeta.timeAxisHeaders;

            DomHelper.forEachSelector(timeAxisEl, '.b-sch-header-row', (headerRow, index, headerRows) => {
                const headersMap = timeAxisHeaders[index];

                DomHelper.forEachSelector(headerRow, '.b-sch-header-timeaxis-cell', el => {
                    if (!headersMap.has(el.dataset.tickIndex)) {
                        headersMap.set(el.dataset.tickIndex, el.outerHTML);
                    }
                });

                if (index === headerRows.length - 1 && headersMap.has(String(client.timeAxis.count - 1))) {
                    exportMeta.headersCollected = true;
                }
            });
        }
    }

    collectRanges(config) {
        const
            me             = this,
            { client }     = config,
            { exportMeta } = me,
            { timeRanges } = exportMeta;

        if (!exportMeta.headersCollected && timeRanges) {
            const
                { headerCanvas, bodyCanvas } = client.features.timeRanges;

            if (headerCanvas) {
                DomHelper.forEachSelector(headerCanvas, '.b-sch-timerange', el => {
                    timeRanges.header[el.dataset.id] = el.outerHTML;
                });
            }

            DomHelper.forEachSelector(bodyCanvas, '.b-sch-timerange', el => {
                timeRanges.body[el.dataset.id] = el.outerHTML;
            });
        }
    }

    collectLines(config) {
        const
            me              = this,
            { client }      = config,
            { exportMeta }  = me,
            { columnLines } = exportMeta;

        if (!exportMeta.headersCollected && columnLines) {
            const bgCanvas = client.backgroundCanvas;

            DomHelper.forEachSelector(bgCanvas, '.b-column-line, .b-column-line-major', (lineEl) => {
                if (lineEl.classList.contains('b-column-line')) {
                    const lineIndex = Number(lineEl.dataset.line.replace(/line-/, ''));

                    columnLines.lines.set(lineIndex, lineEl.outerHTML);
                }
                else {
                    const lineIndex = Number(lineEl.dataset.line.replace(/major-/, ''));

                    columnLines.majorLines.set(lineIndex, lineEl.outerHTML);
                }
            });
        }
    }

    collectEvents(rows, config) {
        const
            me         = this,
            addedRows  = rows.length,
            { client } = config,
            normalRows = me.exportMeta.subGrids.normal.rows;

        rows.forEach((row, index) => {
            const
                rowConfig = normalRows[normalRows.length - addedRows + index],
                resource  = client.store.getAt(row.dataIndex),
                eventsMap = rowConfig[3];

            resource.events?.forEach(event => {
                if (event.isScheduled) {
                    let el = client.getElementFromEventRecord(event, resource);

                    if (el && (el = el.parentElement) && !eventsMap.has(event.id)) {
                        eventsMap.set(event.id, [el.outerHTML, Rectangle.from(el, el.offsetParent)]);
                    }
                }
            });

            resource.timeRanges?.forEach(timeRange => {
                const
                    elId = client.features.resourceTimeRanges?.generateElementId(timeRange) || '',
                    el   = client.foregroundCanvas.syncIdMap[elId];

                if (el && !eventsMap.has(elId)) {
                    eventsMap.set(elId, [el.outerHTML, Rectangle.from(el, el.offsetParent)]);
                }
            });
        });
    }

    //#region Direct rendering

    renderHeaders(config, start, end) {
        const
            me               = this,
            { exportMeta }   = me,
            { client }       = config,
            timeAxisHeaders  = exportMeta.timeAxisHeaders,
            // Get the time axis view reference that we will use to build cells for specific time ranges
            { timeAxisView } = client.timeAxisColumn,
            domConfig        = timeAxisView.buildCells(start, end),
            targetElement    = document.createElement('div');

        DomSync.sync({
            targetElement,
            domConfig
        });

        DomHelper.forEachSelector(targetElement, '.b-sch-header-row', (headerRow, index) => {
            const headersMap = timeAxisHeaders[index];

            DomHelper.forEachSelector(headerRow, '.b-sch-header-timeaxis-cell', el => {
                if (!headersMap.has(el.dataset.tickIndex)) {
                    headersMap.set(el.dataset.tickIndex, el.outerHTML);
                }
            });
        });
    }

    renderEvents(config, rows, start, end) {
        const
            me         = this,
            { client } = config,
            normalRows = me.exportMeta.subGrids.normal.rows;

        rows.forEach((row, index) => {
            const
                rowConfig      = normalRows[index],
                eventsMap      = rowConfig[3],
                resource       = client.store.getAt(row.dataIndex),
                resourceLayout = client.currentOrientation.getResourceLayout(resource),
                left           = client.getCoordinateFromDate(start),
                right          = client.getCoordinateFromDate(end),
                eventDOMConfigs = client.currentOrientation.getEventDOMConfigForCurrentView(resourceLayout, row, left, right),
                targetElement   = document.createElement('div');

            eventDOMConfigs.forEach(domConfig => {
                const
                    { eventId }                  = domConfig.dataset,
                    { left, top, width, height } = domConfig.style;

                DomSync.sync({
                    targetElement,
                    domConfig
                });

                eventsMap.set(eventId, [targetElement.outerHTML, new Rectangle(left, top, width, height)]);
            });
        });
    }

    renderLines(config, start, end) {
        const
            me              = this,
            { client }      = config,
            { exportMeta }  = me,
            { columnLines } = exportMeta;

        if (columnLines) {
            const
                domConfigs    = client.features.columnLines.getColumnLinesDOMConfig(start, end),
                targetElement = document.createElement('div');

            DomSync.sync({
                targetElement,
                domConfig : {
                    children : domConfigs
                },
                onlyChildren : true
            });

            // Put all lines HTML to a single key in the set. That allows us to share code path with legacy export mode
            columnLines.lines.set(0, targetElement.innerHTML);
        }
    }

    renderRanges(config, start, end) {
        const
            me             = this,
            { client }     = config,
            { exportMeta } = me,
            { timeRanges } = exportMeta;

        if (timeRanges) {
            const
                domConfigs    = client.features.timeRanges.getDOMConfig(start, end),
                targetElement = document.createElement('div');

            // domConfigs is an array of two elements - first includes time range configs for body, second - for head
            domConfigs.forEach((children, i) => {
                DomSync.sync({
                    targetElement,
                    domConfig : {
                        children,
                        onlyChildren : true
                    }
                });

                // body configs
                if (i === 0) {
                    timeRanges.body = targetElement.innerHTML;
                }
                // header configs
                else {
                    timeRanges.header = targetElement.innerHTML;
                }
            });
        }
    }

    //#endregion

    buildPageHtml(config) {
        const
            me = this,
            {
                subGrids,
                timeAxisHeaders,
                timeAxisPlaceholders,
                columnLines,
                columnLinesPlaceholder,
                timeRanges,
                timeRangesHeaderPlaceholder,
                timeRangesBodyPlaceholder
            }  = me.exportMeta,
            { enableDirectRendering } = config;

        // Now when rows are collected, we need to add them to exported grid
        let html = me.prepareExportElement();

        Object.values(subGrids).forEach(({ placeHolder, eventsPlaceholder, rows, mergedCellsHtml }) => {
            const
                placeHolderText       = placeHolder.outerHTML,
                // Rows can be repositioned, in which case event related to that row should also be translated
                { resources, events } = me.positionRows(rows, config);

            let contentHtml =  resources.join('');

            if (mergedCellsHtml?.length) {
                contentHtml += `<div class="b-grid-merged-cells-container">${mergedCellsHtml.join('')}</div>`;
            }

            html = html.replace(placeHolderText, contentHtml);

            if (eventsPlaceholder) {
                html = html.replace(eventsPlaceholder.outerHTML, events.join(''));
            }
        });

        timeAxisHeaders.forEach((headers, index) => {
            html = html.replace(timeAxisPlaceholders[index].outerHTML, Array.from(headers.values()).join(''));
        });

        if (columnLines) {
            const lineElements = Array.from(columnLines.lines.values()).concat(Array.from(columnLines.majorLines.values()));

            html = html.replace(columnLinesPlaceholder.outerHTML, lineElements.join(''));

            // Lines are collected once for old mode, don't clear them
            if (enableDirectRendering) {
                me.exportMeta.columnLines.lines.clear();
                me.exportMeta.columnLines.majorLines.clear();
            }
        }

        if (timeRanges) {
            if (enableDirectRendering) {
                html = html.replace(timeRangesBodyPlaceholder.outerHTML, timeRanges.body);

                // time ranges header element is optional
                if (timeRangesHeaderPlaceholder) {
                    html = html.replace(timeRangesHeaderPlaceholder.outerHTML, timeRanges.header);
                }

                me.exportMeta.timeRanges = {};
            }
            else {
                html = html.replace(timeRangesBodyPlaceholder.outerHTML, Object.values(timeRanges.body).join(''));

                // time ranges header element is optional
                if (timeRangesHeaderPlaceholder) {
                    html = html.replace(timeRangesHeaderPlaceholder.outerHTML, Object.values(timeRanges.body).join(''));
                }
            }
        }

        html = me.buildDependenciesHtml(html);

        return html;
    }

    getEventBox(event) {
        const
            me = this,
            {
                eventsBoxes,
                enableDirectRendering
            } = me.exportMeta;

        const box = event && eventsBoxes.get(String(event.id));

        // In scheduler milestone box left edge is aligned with milestone start date. Later element is rotated and
        // shifted by CSS by 50% of its width. Dependency feature relies on actual element sizes, but pdf export
        // does not render actual elements. Therefore, we need to adjust the box.

        if (enableDirectRendering && box && event.isMilestone) {
            box.translate(-box.width / 2, 0);
        }

        return box;
    }

    renderDependencies() {
        const
            me                = this,
            {
                client,
                eventsBoxes
            }                 = me.exportMeta,
            { dependencies }  = client,
            dependencyFeature = client.features.dependencies,
            targetElement     = DomHelper.createElement();

        let draw = false;

        dependencies.forEach(dependency => {
            if ((!eventsBoxes.has(String(dependency.from)) &&
                !eventsBoxes.has(String(dependency.to))) ||
                !dependencyFeature.isDependencyVisible(dependency)) {
                return;
            }

            const
                fromBox = me.getEventBox(dependency.fromEvent),
                toBox   = me.getEventBox(dependency.toEvent);

            dependencyFeature.drawDependency(dependency, true, { from : fromBox?.clone(), to : toBox?.clone() });
            draw = true;
        });

        // Force dom sync
        if (draw) {
            dependencyFeature.domSync(targetElement);
        }

        return targetElement.innerHTML;
    }

    buildDependenciesHtml(html) {
        const { dependenciesPlaceholder, includeTimeline } = this.exportMeta;

        if (dependenciesPlaceholder && includeTimeline) {
            const placeholder = dependenciesPlaceholder.outerHTML;
            html = html.replace(placeholder, this.renderDependencies());
        }

        return html;
    }
};
