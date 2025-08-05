import Base from '../../../Core/Base.js';
import Delayable from '../../../Core/mixin/Delayable.js';
import DomHelper from '../../../Core/helper/DomHelper.js';
import DomSync from '../../../Core/helper/DomSync.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import VerticalLayout from '../../eventlayout/VerticalLayout.js';
import Rectangle from '../../../Core/helper/util/Rectangle.js';
import DateHelper from '../../../Core/helper/DateHelper.js';
import AttachToProjectMixin from '../../data/mixin/AttachToProjectMixin.js';

/**
 * @module Scheduler/view/orientation/VerticalRendering
 */

const
    releaseEventActions = {
        releaseElement : 1, // Not used at all at the moment
        reuseElement   : 1  // Used by some other element
    },
    renderEventActions  = {
        newElement      : 1,
        reuseOwnElement : 1,
        reuseElement    : 1
    },
    chronoFields        = {
        startDate : 1,
        endDate   : 1,
        duration  : 1
    },
    emptyObject        = Object.freeze({});

/**
 * Handles event rendering in Schedulers vertical mode. Reacts to project/store changes to keep the UI up to date.
 *
 * @internal
 */
export default class VerticalRendering extends Base.mixin(Delayable, AttachToProjectMixin) {

    //region Config & Init

    static get properties() {
        return {
            eventMap               : new Map(),
            resourceMap            : new Map(),
            releasedElements       : {},
            toDrawOnProjectRefresh : new Set(),
            resourceBufferSize     : 1
        };
    }

    construct(scheduler) {
        this.client         = this.scheduler = scheduler;
        this.verticalLayout = new VerticalLayout({ scheduler });

        super.construct({});
    }

    init() {
        const
            me                             = this,
            { scheduler, resourceColumns } = me;

        // Resource header/columns
        resourceColumns.resourceStore = me.resourceStore;

        resourceColumns.ion({
            name              : 'resourceColumns',
            columnWidthChange : 'onResourceColumnWidthChange',
            thisObj           : me
        });

        me.initialized = true;

        if (scheduler.isPainted) {
            me.renderer();
        }

        resourceColumns.availableWidth = scheduler.timeAxisSubGridElement.offsetWidth;
    }

    //endregion

    //region Elements <-> Records

    resolveRowRecord(elementOrEvent, xy) {
        const
            me            = this,
            { scheduler } = me,
            event         = elementOrEvent.nodeType ? null : elementOrEvent,
            element       = event ? event.target : elementOrEvent,
            coords        = event ? [event.borderOffsetX, event.borderOffsetY] : xy,
            // Fix for FF on Linux having text nodes as event.target
            el            = element.nodeType === Element.TEXT_NODE ? element.parentElement : element,
            eventElement  = el.closest(scheduler.eventSelector);

        if (eventElement) {
            return scheduler.resourceStore.getById(eventElement.dataset.resourceId);
        }

        // Need to be inside schedule at least
        if (!element.closest('.b-sch-timeaxis-cell')) {
            return null;
        }

        if (!coords) {
            throw new Error(`Vertical mode needs coordinates to resolve this element. Can also be called with a browser
                event instead of element to extract element and coordinates from`);
        }

        if (scheduler.variableColumnWidths || scheduler.resourceStore.isGrouped) {
            let totalWidth = 0;

            for (const col of me.resourceStore) {
                if (!col.isSpecialRow) {
                    totalWidth += col.columnWidth || me.resourceColumns.columnWidth;
                }
                if (totalWidth >= coords[0]) {
                    return col;
                }
            }

            return null;
        }

        const index = Math.floor(coords[0] / me.resourceColumns.columnWidth);

        return me.allResourceRecords[index];
    }

    toggleCls(assignmentRecord, cls, add = true, useWrapper = false) {
        const eventData = this.eventMap.get(assignmentRecord.eventId)?.[assignmentRecord.resourceId];

        if (eventData) {
            eventData.renderData[useWrapper ? 'wrapperCls' : 'cls'][cls] = add;
            // Element from the map cannot be trusted, might be reused in which case map is not updated to reflect that.
            // To be safe, retrieve using `getElementFromAssignmentRecord`
            const element = this.client.getElementFromAssignmentRecord(assignmentRecord, useWrapper);

            if (element) {
                element.classList[add ? 'add' : 'remove'](cls);
            }
        }
    }

    //endregion

    //region Coordinate <-> Date

    getDateFromXY(xy, roundingMethod, local, allowOutOfRange = false) {
        let coord = xy[1];

        if (!local) {
            coord = this.translateToScheduleCoordinate(coord);
        }

        return this.scheduler.timeAxisViewModel.getDateFromPosition(coord, roundingMethod, allowOutOfRange);
    }

    translateToScheduleCoordinate(y) {
        return y - this.scheduler.timeAxisSubGridElement.getBoundingClientRect().top - globalThis.scrollY;
    }

    translateToPageCoordinate(y) {
        return y + this.scheduler.timeAxisSubGridElement.getBoundingClientRect().top + globalThis.scrollY;
    }

    //endregion

    //region Regions

    getResourceEventBox(event, resource) {
        const
            eventId    = event.id,
            resourceId = resource.id;

        let { renderData } = this.eventMap.get(eventId)?.[resourceId] || emptyObject;

        if (!renderData) {
            // Never been in view, lay it out
            this.layoutResourceEvents(this.scheduler.resourceStore.getById(resourceId));

            // Have another go at getting the layout data
            renderData = this.eventMap.get(eventId)?.[resourceId]?.renderData;
        }

        return renderData
            ? new Rectangle(renderData.left, renderData.top, renderData.width, renderData.bottom - renderData.top)
            : null;
    }

    getScheduleRegion(resourceRecord, eventRecord, local) {
        const
            me            = this,
            { scheduler } = me,
            // Only interested in width / height (in "local" coordinates)
            region        = Rectangle.from(scheduler.timeAxisSubGridElement, scheduler.timeAxisSubGridElement);

        if (resourceRecord) {

            region.left  = me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth;
            region.right = region.left + scheduler.resourceColumnWidth;
        }

        const
            start           = scheduler.timeAxis.startDate,
            end             = scheduler.timeAxis.endDate,
            dateConstraints = scheduler.getDateConstraints?.(resourceRecord, eventRecord) || {
                start,
                end
            },
            startY          = scheduler.getCoordinateFromDate(DateHelper.max(start, dateConstraints.start)),
            endY            = scheduler.getCoordinateFromDate(DateHelper.min(end, dateConstraints.end));

        if (!local) {
            region.top    = me.translateToPageCoordinate(startY);
            region.bottom = me.translateToPageCoordinate(endY);
        }
        else {
            region.top    = startY;
            region.bottom = endY;
        }

        return region;
    }

    getRowRegion(resourceRecord, startDate, endDate) {
        const
            me            = this,
            { scheduler } = me,
            x             = me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth,
            taStart       = scheduler.timeAxis.startDate,
            taEnd         = scheduler.timeAxis.endDate,
            start         = startDate ? DateHelper.max(taStart, startDate) : taStart,
            end           = endDate ? DateHelper.min(taEnd, endDate) : taEnd,
            startY        = scheduler.getCoordinateFromDate(start),
            endY          = scheduler.getCoordinateFromDate(end, true, true),
            y             = Math.min(startY, endY),
            height        = Math.abs(startY - endY);

        return new Rectangle(x, y, scheduler.resourceColumnWidth, height);
    }

    get visibleDateRange() {
        const
            scheduler = this.scheduler,
            scrollPos = scheduler.scrollable.y,
            height    = scheduler.scrollable.clientHeight,
            startDate = scheduler.getDateFromCoordinate(scrollPos) || scheduler.timeAxis.startDate,
            endDate   = scheduler.getDateFromCoordinate(scrollPos + height) || scheduler.timeAxis.endDate;

        return {
            startDate,
            endDate,
            startMS : startDate.getTime(),
            endMS   : endDate.getTime()
        };
    }

    //endregion

    //region Events

    // Column width changed, rerender fully
    onResourceColumnWidthChange({ width, oldWidth }) {
        const
            me            = this,
            { scheduler } = me;

        // Fix width of column & header
        me.resourceColumns.width = scheduler.timeAxisColumn.width = me.allResourceRecords.length * width;
        me.clearAll();

        // Only transition large changes, otherwise it is janky when dragging slider in demo
        me.refresh(Math.abs(width - oldWidth) > 30);

        // Not detected by resizeobserver? Need to call this for virtual scrolling to react to update
        //        scheduler.callEachSubGrid('refreshFakeScroll');
        //        scheduler.refreshVirtualScrollbars();
    }

    //endregion

    //region Project

    attachToProject(project) {
        super.attachToProject(project);

        if (project) {
            project.ion({
                name    : 'project',
                refresh : 'onProjectRefresh',
                thisObj : this
            });
        }
    }

    onProjectRefresh() {
        const
            me                                    = this,
            { scheduler, toDrawOnProjectRefresh } = me;

        // Only update the UI immediately if we are visible
        if (scheduler.isVisible) {
            if (scheduler.rendered && !scheduler.refreshSuspended) {
                // Either refresh all rows (on for example dataset)
                if (me.refreshAllWhenReady) {
                    me.clearAll();
                    //scheduler.refreshWithTransition();
                    me.refresh();
                    me.refreshAllWhenReady = false;
                }
                // Or only affected rows (if any)
                else if (toDrawOnProjectRefresh.size) {
                    me.refresh();
                }

                toDrawOnProjectRefresh.clear();
            }
        }
        // Otherwise wait till next time we get painted (shown, or a hidden ancestor shown)
        else {
            scheduler.whenVisible('refresh', scheduler, [true]);
        }
    }

    //endregion

    //region EventStore

    attachToEventStore(eventStore) {
        super.attachToEventStore(eventStore);

        this.refreshAllWhenReady = true;

        if (eventStore) {
            eventStore.ion({
                name             : 'eventStore',
                addConfirmed     : 'onEventStoreAddConfirmed',
                refreshPreCommit : 'onEventStoreRefresh',
                thisObj          : this
            });
        }
    }

    onEventStoreAddConfirmed({ record }) {
        for (const element of this.client.getElementsFromEventRecord(record)) {
            element.classList.remove('b-iscreating');
        }
    }

    onEventStoreRefresh({ action }) {
        if (action === 'batch') {
            this.refreshAllWhenReady = true;
        }
    }

    onEventStoreChange({ action, records : eventRecords = [], record, replaced, changes, isAssign }) {
        const
            me          = this,
            resourceIds = new Set();

        eventRecords.forEach(eventRecord => {
            // Update all resource rows to which this event is assigned *if* the resourceStore
            // contains that resource (We could have filtered the resourceStore)
            const renderedEventResources = eventRecord.$linkedResources?.filter(r => me.resourceStore.includes(r));

            renderedEventResources?.forEach(resourceRecord => resourceIds.add(resourceRecord.id));
        });

        switch (action) {
            // No-ops
            case 'sort':  // Order in EventStore does not matter, so these actions are no-ops
            case 'group':
            case 'move':
            case 'remove': // Remove is a no-op since assignment will also be removed
                return;

            case 'dataset':
                me.refreshAllResourcesWhenReady();
                return;

            case 'add':
            case 'updateMultiple':
                // Just refresh below
                break;

            case 'replace':
                // Gather resources from both the old record and the new one
                replaced.forEach(([, newEvent]) => {
                    // Old cleared by changed assignment
                    newEvent.resources.map(resourceRecord => resourceIds.add(resourceRecord.id));
                });
                // And clear them
                me.clearResources(resourceIds);
                break;

            case 'removeall':
            case 'filter':
                // Clear all when filtering for simplicity. If that turns out to give bad performance, one would need to
                // figure out which events was filtered out and only clear their resources.
                me.clearAll();
                me.refresh();
                return;

            case 'update': {
                // Check if changes are graph related or not
                const allChrono = record.$entity
                    ? !Object.keys(changes).some(name => !record.$entity.getField(name))
                    : !Object.keys(changes).some(name => !chronoFields[name]);

                // If any one of these in changes, it will affect visuals
                let changeCount = 0;
                if ('startDate' in changes) changeCount++;
                if ('endDate' in changes) changeCount++;
                if ('duration' in changes) changeCount++;

                // Always redraw non chrono changes (name etc)
                if (!allChrono || changeCount || 'percentDone' in changes || 'inactive' in changes || 'segments' in changes) {
                    if (me.shouldWaitForInitializeAndEngineReady) {
                        me.refreshResourcesWhenReady(resourceIds);
                    }
                    else {
                        me.clearResources(resourceIds);
                        me.refresh();
                    }
                }
                return;
            }
        }

        me.refreshResourcesWhenReady(resourceIds);
    }

    //endregion

    //region ResourceStore

    attachToResourceStore(resourceStore) {
        const me = this;

        super.attachToResourceStore(resourceStore);

        me.refreshAllWhenReady = true;

        if (me.resourceColumns) {
            me.resourceColumns.resourceStore = resourceStore;
        }

        resourceStore.ion({
            name             : 'resourceStore',
            changePreCommit  : 'onResourceStoreChange',
            refreshPreCommit : 'onResourceStoreRefresh',
            // In vertical, resource store is not the row store but should toggle the load mask
            load             : () => me.scheduler.unmaskBody(),
            thisObj          : me,
            prio             : 1 // Call before others to clear cache before redraw
        });

        if (me.initialized && me.scheduler.isPainted) {
            // Invalidate resource range and events
            me.firstResource = me.lastResource = null;
            me.clearAll();

            me.renderer();
        }
    }

    onResourceStoreChange({ source : resourceStore, action, records = [], record, replaced, changes }) {
        const
            me              = this,
            // records for add, record for update, replaced [[old, new]] for replace
            resourceRecords = replaced ? replaced.map(r => r[1]) : records,
            resourceIds     = new Set(resourceRecords.map(resourceRecord => resourceRecord.id));

        // Invalidate resource range
        me.firstResource                  = me.lastResource = null;
        resourceStore._allResourceRecords = null;

        const { allResourceRecords } = resourceStore;

        // Operation that did not invalidate engine, refresh directly
        if (me.scheduler.isEngineReady) {
            switch (action) {
                case 'update':
                    if (changes?.id) {
                        me.clearResources([changes.id.oldValue, changes.id.value]);
                    }
                    else {
                        me.clearResources([record.id]);
                    }
                    // Only the invalidation above needed
                    break;

                case 'filter':
                    // All filtered out resources needs clearing and so does those not filtered out since they might have
                    // moved horizontally when others hide
                    me.clearAll();
                    break;
            }

            // Changing a column width means columns after that will have to be recalculated
            // so clear all cached layouts.
            if (changes && ('columnWidth' in changes)) {
                me.clearAll();
            }
            me.refresh(true);
        }
        // Operation that did invalidate project, update on project refresh
        else {
            switch (action) {
                case 'dataset':
                case 'remove': // Cannot tell from which index it was removed
                case 'removeall':
                    me.refreshAllResourcesWhenReady();
                    return;

                case 'replace':
                case 'add': {
                    if (!resourceStore.isGrouped) {
                        // Make sure all existing events following added resources are offset correctly
                        const
                            firstIndex = resourceRecords.reduce(
                                (index, record) => Math.min(index, allResourceRecords.indexOf(record)),
                                allResourceRecords.length
                            );

                        for (let i = firstIndex; i < allResourceRecords.length; i++) {
                            resourceIds.add(allResourceRecords[i].id);
                        }
                    }
                }
            }

            me.refreshResourcesWhenReady(resourceIds);
        }
    }

    onResourceStoreRefresh({ action }) {
        const me = this;

        if (action === 'sort' || action === 'group') {
            // Invalidate resource range & cache
            me.firstResource = me.lastResource = me.resourceStore._allResourceRecords = null;
            me.clearAll();
            me.refresh();
        }
    }

    //endregion

    //region AssignmentStore

    attachToAssignmentStore(assignmentStore) {
        super.attachToAssignmentStore(assignmentStore);

        this.refreshAllWhenReady = true;

        if (assignmentStore) {
            assignmentStore.ion({
                name             : 'assignmentStore',
                changePreCommit  : 'onAssignmentStoreChange',
                refreshPreCommit : 'onAssignmentStoreRefresh',
                thisObj          : this
            });
        }
    }

    onAssignmentStoreChange({ action, records : assignmentRecords = [], replaced, changes }) {
        const
            me          = this,
            resourceIds = new Set(assignmentRecords.map(assignmentRecord => assignmentRecord.resourceId));

        // Operation that did not invalidate engine, refresh directly
        if (me.scheduler.isEngineReady) {
            switch (action) {
                case 'remove':
                    me.clearResources(resourceIds);
                    break;

                case 'filter':
                    me.clearAll();
                    break;

                case 'update': {
                    // When reassigning, clear old resource also
                    if ('resourceId' in changes) {
                        resourceIds.add(changes.resourceId.oldValue);
                    }

                    // Ignore engine resolving resourceId -> resource, eventId -> event
                    if (!Object.keys(changes).filter(field => field !== 'resource' && field !== 'event').length) {
                        return;
                    }

                    me.clearResources(resourceIds);
                }
            }

            me.refresh(true);
        }
        // Operation that did invalidate project, update on project refresh
        else {
            if (changes && 'resourceId' in changes) {
                resourceIds.add(changes.resourceId.oldValue);
            }

            switch (action) {
                case 'removeall':
                    me.refreshAllResourcesWhenReady();
                    return;

                case 'replace':
                    // Gather resources from both the old record and the new one
                    replaced.forEach(([oldAssignment, newAssignment]) => {
                        resourceIds.add(oldAssignment.resourceId);
                        resourceIds.add(newAssignment.resourceId);
                    });
            }

            me.refreshResourcesWhenReady(resourceIds);
        }
    }

    onAssignmentStoreRefresh({ action, records }) {
        if (action === 'batch') {
            this.clearAll();
            this.refreshAllResourcesWhenReady();
        }
    }

    //endregion

    //region View hooks

    refreshRows(reLayoutEvents) {
        if (reLayoutEvents) {
            this.clearAll();
            this.scheduler.refreshFromRerender = false;
        }
    }

    // Called from SchedulerEventRendering
    repaintEventsForResource(resourceRecord) {
        this.renderResource(resourceRecord);
    }

    updateFromHorizontalScroll(scrollX) {
        if (scrollX !== this.prevScrollX) {
            this.renderer();
            this.prevScrollX = scrollX;
        }
    }

    updateFromVerticalScroll() {
        this.renderer();
    }

    scrollResourceIntoView(resourceRecord, options) {
        const
            { scheduler } = this,
            x             = this.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth;

        return scheduler.scrollHorizontallyTo(x, options);
    }

    get allResourceRecords() {
        return this.scheduler.resourceStore.allResourceRecords;
    }

    // Called when viewport size changes
    onViewportResize(width) {
        this.resourceColumns.availableWidth = width;
        this.renderer();
    }

    get resourceColumns() {
        return this.scheduler.timeAxisColumn?.resourceColumns;
    }

    // Clear events in case they use date as part of displayed info
    onLocaleChange() {
        this.clearAll();
    }

    // No need to do anything special
    onDragAbort() {}

    onBeforeRowHeightChange() {}

    onTimeAxisViewModelUpdate() {}

    updateElementId() {}

    releaseTimeSpanDiv() {}

    //endregion

    //region Dependency connectors

    /**
     * Gets displaying item start side
     *
     * @param {Scheduler.model.EventModel} eventRecord
     * @returns {'top'|'left'|'bottom'|'right'} 'left' / 'right' / 'top' / 'bottom'
     */
    getConnectorStartSide(eventRecord) {
        return 'top';
    }

    /**
     * Gets displaying item end side
     *
     * @param {Scheduler.model.EventModel} eventRecord
     * @returns {'top'|'left'|'bottom'|'right'} 'left' / 'right' / 'top' / 'bottom'
     */
    getConnectorEndSide(eventRecord) {
        return 'bottom';
    }

    //endregion

    //region Refresh resources

    /**
     * Clears resources directly and redraws them on next project refresh
     * @param {Number[]|String[]} resourceIds
     * @private
     */
    refreshResourcesWhenReady(resourceIds) {
        this.clearResources(resourceIds);
        resourceIds.forEach(id => this.toDrawOnProjectRefresh.add(id));
    }

    /**
     * Clears all resources directly and redraws them on next project refresh
     * @private
     */
    refreshAllResourcesWhenReady() {
        this.clearAll();
        this.refreshAllWhenReady = true;
    }

    //region Rendering

    // Resources in view + buffer
    get resourceRange() {
        return this.getResourceRange(true);
    }

    // Resources strictly in view
    get visibleResources() {
        const { first, last } = this.getResourceRange();

        return {
            first : this.allResourceRecords[first],
            last  : this.allResourceRecords[last]
        };
    }

    getResourceRange(withBuffer) {
        const
            {
                scheduler,
                resourceStore
            }                  = this,
            {
                resourceColumnWidth,
                scrollX
            }                  = scheduler,
            {
                scrollWidth
            }                  = scheduler.timeAxisSubGrid.scrollable,
            resourceBufferSize = withBuffer ? this.resourceBufferSize : 0,
            viewportStart      = scrollX - resourceBufferSize,
            viewportEnd        = scrollX + scrollWidth + resourceBufferSize;
        if (!resourceStore?.count) {
            return { first : -1, last : -1 };
        }

        // Some resources define their own width
        if (scheduler.variableColumnWidths) {
            let first, last = 0, start, end = 0;
            this.allResourceRecords.forEach((resource, i) => {
                resource.instanceMeta(scheduler).insetStart = start = end;
                end                                         = start + resource.columnWidth;

                if (start > viewportEnd) {
                    return false;
                }
                if (end > viewportStart && first == null) {
                    first = i;
                }
                else if (start < viewportEnd) {
                    last = i;
                }
            });
            return { first, last };
        }
        // We are using fixed column widths
        else {
            return {
                first : Math.max(Math.floor(scrollX / resourceColumnWidth) - resourceBufferSize, 0),
                last  : Math.min(
                    Math.floor((scrollX + scheduler.timeAxisSubGrid.width) / resourceColumnWidth) + resourceBufferSize,
                    this.allResourceRecords.length - 1
                )
            };
        }
    }

    // Dates in view + buffer
    get dateRange() {
        const
            { scheduler } = this;

        let bottomDate = scheduler.getDateFromCoordinate(Math.min(
            scheduler.scrollTop + scheduler.bodyHeight + scheduler.tickSize - 1,
            (scheduler.virtualScrollHeight || scheduler.scrollable.scrollHeight) - 1)
        );

        // Might end up below time axis (out of ticks)

        if (!bottomDate) {
            bottomDate = scheduler.timeAxis.last.endDate;
        }

        let topDate = scheduler.getDateFromCoordinate(Math.max(scheduler.scrollTop - scheduler.tickSize, 0));

        // Might end up above time axis when reconfiguring (since this happens as part of rendering)
        if (!topDate) {
            topDate    = scheduler.timeAxis.first.startDate;
            bottomDate = scheduler.getDateFromCoordinate(scheduler.bodyHeight + scheduler.tickSize - 1);
        }

        return {
            topDate,
            bottomDate
        };
    }

    getTimeSpanRenderData(eventRecord, resourceRecord, includeOutside = false) {
        const
            me             = this,
            {
                scheduler
            }              = me,
            {
                preamble,
                postamble
            }              = eventRecord,
            {
                variableColumnWidths
            }              = scheduler,
            useEventBuffer = scheduler.features.eventBuffer?.enabled && me.isProVerticalRendering &&
                (preamble || postamble) && !eventRecord.isMilestone,
            startDateField = useEventBuffer ? 'wrapStartDate' : 'startDate',
            endDateField   = useEventBuffer ? 'wrapEndDate' : 'endDate',
            // Must use Model.get in order to get latest values in case we are inside a batch.
            // EventResize changes the endDate using batching to enable a tentative change
            // via the batchedUpdate event which is triggered when changing a field in a batch.
            // Fall back to accessor if propagation has not populated date fields.
            startDate      = eventRecord.isBatchUpdating && eventRecord.hasBatchedChange(startDateField) && !useEventBuffer
                ? eventRecord.get(startDateField) : eventRecord[startDateField],
            endDate        = eventRecord.isBatchUpdating && eventRecord.hasBatchedChange(endDateField) && !useEventBuffer
                ? eventRecord.get(endDateField) : eventRecord[endDateField],
            resourceMargin = scheduler.getResourceMargin(resourceRecord),
            top            = scheduler.getCoordinateFromDate(startDate),
            instanceMeta   = resourceRecord.instanceMeta(scheduler),
            // Preliminary values for left & width, used for proxy. Will be changed on layout.
            // The property "left" is utilized based on Scheduler's rtl setting.
            // If RTL, then it's used as the "right" style position.
            left           = variableColumnWidths ? instanceMeta.insetStart : me.allResourceRecords.indexOf(resourceRecord) * scheduler.resourceColumnWidth,
            resourceWidth  = scheduler.getResourceWidth(resourceRecord),
            width          = resourceWidth - resourceMargin * 2,
            startDateMS    = startDate.getTime(),
            endDateMS      = endDate.getTime();

        let bottom = scheduler.getCoordinateFromDate(endDate),
            height = bottom - top;

        // Below, estimate height
        if (bottom === -1) {
            height = Math.round((endDateMS - startDateMS) * scheduler.timeAxisViewModel.getSingleUnitInPixels('millisecond'));
            bottom = top + height;
        }

        return {
            eventRecord,
            resourceRecord,
            left,
            top,
            bottom,
            resourceWidth,
            width,
            height,
            startDate,
            endDate,
            startDateMS,
            endDateMS,
            useEventBuffer,

            children : [],


            start   : startDate,
            end     : endDate,
            startMS : startDateMS,
            endMS   : endDateMS
        };
    }

    // Earlier start dates are above later tasks
    // If same start date, longer tasks float to top
    // If same start + duration, sort by name
    eventSorter(a, b) {
        const
            startA = a.dataStartMs || a.startDateMS, // dataXX are used if configured with fillTicks
            endA   = a.dataEndMs || a.endDateMS,
            startB = b.dataStartMs || b.startDateMS,
            endB   = b.dataEndMs || b.endDateMS,
            nameA  = a.isModel ? a.name : a.eventRecord.name,
            nameB  = b.isModel ? b.name : b.eventRecord.name;

        return startA - startB || endB - endA || (nameA < nameB ? -1 : nameA == nameB ? 0 : 1);
    }

    layoutEvents(resourceRecord, allEvents, includeOutside = false, parentEventRecord, eventSorter) {
        const
            me                  = this,
            { scheduler }       = me,
            {
                variableColumnWidths
            }                   = scheduler,
            { id : resourceId } = resourceRecord,
            instanceMeta        = resourceRecord.instanceMeta(scheduler),
            cacheKey            = parentEventRecord ? `${resourceId}-${parentEventRecord.id}` : resourceId,
            // Cache per resource
            cache               = me.resourceMap.set(cacheKey, {}).get(cacheKey),
            // Resource "column"
            resourceIndex       = me.allResourceRecords.indexOf(resourceRecord),
            {
                barMargin,
                resourceMargin
            }                   = scheduler.getResourceLayoutSettings(resourceRecord, parentEventRecord);

        const layoutData = allEvents.reduce((toLayout, eventRecord) => {
            if (eventRecord.isScheduled) {
                const
                    renderData     = scheduler.generateRenderData(eventRecord, resourceRecord, false),
                    // Elements will be appended to eventData during syncing
                    eventData      = { renderData },
                    eventResources = ObjectHelper.getMapPath(me.eventMap, renderData.eventId, {});

                // Cache per event, { e1 : { r1 : { xxx }, r2 : ... }, e2 : ... }
                // Uses renderData.eventId in favor of eventRecord.id to work with ResourceTimeRanges
                eventResources[resourceId] = eventData;

                // Cache per resource
                cache[renderData.eventId] = eventData;

                // Position ResourceTimeRanges directly, they do not affect the layout of others
                if (renderData.fillSize) {
                    // The property "left" is utilized based on Scheduler's rtl setting.
                    // If RTL, then it's used as the "right" style position.
                    renderData.left = variableColumnWidths ? instanceMeta.insetStart : resourceIndex * scheduler.resourceColumnWidth;

                    renderData.width = scheduler.getResourceWidth(resourceRecord);
                }
                // Anything not flagged with `fillSize` should take part in layout
                else {
                    toLayout.push(renderData);
                }
            }

            return toLayout;
        }, []);

        // Ensure the events are rendered in natural order so that navigation works.
        layoutData.sort(eventSorter ?? me.eventSorter);

        // Apply per resource event layout (pack, overlap or mixed)
        me.verticalLayout.applyLayout(
            layoutData,
            scheduler.getResourceWidth(resourceRecord, parentEventRecord),
            resourceMargin,
            barMargin,
            resourceIndex,
            scheduler.getEventLayout(resourceRecord, parentEventRecord)
        );

        return cache;
    }

    // Calculate the layout for all events assigned to a resource. Since we are never stacking, the layout of one
    // resource will never affect the others
    layoutResourceEvents(resourceRecord) {
        const
            me                  = this,
            { scheduler }       = me,
            // Used in loop, reduce access time a wee bit
            {
                assignmentStore,
                eventStore,
                timeAxis
            }                   = scheduler;

        // Events for the resource, minus those that are filtered out by filtering assignments and events
        let events = eventStore.getEvents({
            includeOccurrences : scheduler.enableRecurringEvents,
            resourceRecord,
            startDate          : timeAxis.startDate,
            endDate            : timeAxis.endDate,
            filter             : (assignmentStore.isFiltered || eventStore.isFiltered) && (eventRecord =>
                eventRecord.assignments.some(a => a.resource === resourceRecord && assignmentStore.includes(a)))
        });

        // Hook for features to inject additional timespans to render
        events = scheduler.getEventsToRender(resourceRecord, events);

        return me.layoutEvents(resourceRecord, events);
    }

    /**
     * Used by event drag features to bring into existence event elements that are outside of the rendered block.
     * @param {Scheduler.model.TimeSpan} eventRecord The event to render
     * @private
     */
    addTemporaryDragElement(eventRecord) {
        const
            { scheduler } = this,
            renderData    = scheduler.generateRenderData(
                eventRecord,
                eventRecord.resource,
                { timeAxis : true, viewport : true }
            );

        renderData.top = renderData.row
            ? (renderData.top + renderData.row.top)
            : scheduler.getResourceEventBox(eventRecord, eventRecord.resource, true).top;

        const
            domConfig   = this.renderEvent({ renderData }),
            { dataset } = domConfig;

        delete domConfig.tabIndex;
        delete dataset.eventId;
        delete dataset.resourceId;
        delete dataset.assignmentId;
        delete dataset.syncId;
        dataset.transient = true;
        domConfig.parent  = this.scheduler.foregroundCanvas;

        // So that the regular DomSyncing which may happen during scroll does not
        // sweep up and reuse the temporary element.
        domConfig.retainElement = true;

        const result = DomHelper.createElement(domConfig);

        result.innerElement = result.firstChild;

        eventRecord.instanceMeta(scheduler).hasTemporaryDragElement = true;

        return result;
    }


    // To update an event, first release its element and then render it again.
    // The element will be reused and updated. Keeps code simpler
    renderEvent(eventData) {
        // No point in rendering event that already has an element
        const
            { scheduler } = this,
            data          = eventData.renderData,
            {
                resourceRecord,
                assignmentRecord,
                eventRecord
            }             = data,
            // Event element config, applied to existing element or used to create a new one below
            elementConfig = {
                className : data.wrapperCls,
                tabIndex  : -1,
                children  : [
                    {
                        role      : 'presentation',
                        className : data.cls,
                        style     : (data.internalStyle || '') + (data.style || ''),
                        children  : data.children,
                        dataset   : {
                            // Each feature putting contents in the event wrap should have this to simplify syncing and
                            // element retrieval after sync
                            taskFeature : 'event'
                        },
                        syncOptions : {
                            syncIdField : 'taskBarFeature'
                        }
                    },
                    ...data.wrapperChildren
                ],
                style : {
                    top                                : data.top,
                    [scheduler.rtl ? 'right' : 'left'] : data.left,
                    // DomHelper appends px to dimensions when using numbers
                    height                             : eventRecord.isMilestone ? '1em' : data.height,
                    width                              : data.width,
                    style                              : data.wrapperStyle || '',
                    fontSize                           : eventRecord.isMilestone ? Math.min(data.width, 40) : null
                },
                dataset : {
                    // assignmentId is set in this function conditionally
                    resourceId : resourceRecord.id,
                    eventId    : data.eventId, // Not using eventRecord.id to distinguish between Event and ResourceTimeRange
                    // Sync using assignment id for events and event id for ResourceTimeRanges
                    syncId     : assignmentRecord ? this.assignmentStore.getOccurrence(assignmentRecord, eventRecord).id : data.eventId

                },
                // Will not be part of DOM, but attached to the element
                elementData   : eventData,
                // Dragging etc. flags element as retained, to not reuse/release it during that operation. Events
                // always use assignments, but ResourceTimeRanges does not
                retainElement : (assignmentRecord || eventRecord).instanceMeta(this.scheduler).retainElement,
                // Options for this level of sync, lower levels can have their own
                syncOptions   : {
                    syncIdField      : 'taskFeature',
                    // Remove instead of release when a feature is disabled
                    releaseThreshold : 0
                }
            };

        elementConfig.className['b-sch-vertical'] = 1;

        // Some browsers throw warnings on zIndex = ''
        if (data.zIndex) {
            elementConfig.zIndex = data.zIndex;
        }

        // Do not want to spam dataset with empty prop when not using assignments (ResourceTimeRanges)
        if (assignmentRecord) {
            elementConfig.dataset.assignmentId = assignmentRecord.id;
        }

        // Allows access to the used config later, for example to retrieve element
        data.elementConfig = eventData.elementConfig = elementConfig;

        scheduler.afterRenderEvent({ renderData : data, domConfig : elementConfig });

        return elementConfig;
    }

    renderResource(resourceRecord) {
        const
            me                          = this,
            // Date at top and bottom for determining which events to include
            { topDateMS, bottomDateMS } = me,
            // Will hold element configs
            eventDOMConfigs             = [];

        let resourceEntry = me.resourceMap.get(resourceRecord.id);

        // Layout all events for the resource unless already done
        if (!resourceEntry) {
            resourceEntry = me.layoutResourceEvents(resourceRecord);
        }

        // Iterate over all events for the resource
        for (const eventId in resourceEntry) {
            const
                eventData                               = resourceEntry[eventId],
                { endDateMS, startDateMS, eventRecord } = eventData.renderData;

            if (
                // Only collect configs for those actually in view
                endDateMS >= topDateMS && startDateMS <= bottomDateMS &&
                // And not being dragged, those have a temporary element already
                !eventRecord.instanceMeta(me.scheduler).hasTemporaryDragElement
            ) {
                // Reuse DomConfig if available, otherwise render event to create one
                const domConfig = eventData.elementConfig?.className !== 'b-released' && eventData.elementConfig || me.renderEvent(eventData);
                eventDOMConfigs.push(domConfig);
            }
        }

        return eventDOMConfigs;
    }

    isEventElement(domConfig) {
        const className = domConfig && domConfig.className;

        return className && className[this.scheduler.eventCls + '-wrap'];
    }

    get shouldWaitForInitializeAndEngineReady() {
        return !this.initialized || (!this.scheduler.isEngineReady && !this.scheduler.isCreating);
    }

    // Single cell so only one call to this renderer, determine which events are in view and draw them.
    // Drawing on scroll is triggered by `updateFromVerticalScroll()` and `updateFromHorizontalScroll()`
    renderer() {
        const
            me                                           = this,
            { scheduler }                                = me,
            // Determine resource range to draw events for
            { first : firstResource, last : lastResource } = me.resourceRange,
            // Date at top and bottom for determining which events to include
            { topDate, bottomDate }                        = me.dateRange,
            syncConfigs                                    = [],
            featureDomConfigs                              = [];

        // If scheduler is creating a new event, the render needs to be synchronous, so
        // we cannot wait for the engine to normalize - the new event will have correct data set.
        if (me.shouldWaitForInitializeAndEngineReady) {
            return;
        }



        // Update current time range, reflecting the change on the vertical time axis header
        if (!DateHelper.isEqual(topDate, me.topDate) || !DateHelper.isEqual(bottomDate, me.bottomDate)) {
            // Calculated values used by `renderResource()`
            me.topDate      = topDate;
            me.bottomDate   = bottomDate;
            me.topDateMS    = topDate.getTime();
            me.bottomDateMS = bottomDate.getTime();

            const range = me.timeView.range = { startDate : topDate, endDate : bottomDate };

            scheduler.onVisibleDateRangeChange(range);
        }

        if (firstResource !== -1 && lastResource !== -1) {
            // Collect all events for resources in view
            for (let i = firstResource; i <= lastResource; i++) {
                syncConfigs.push.apply(syncConfigs, me.renderResource(me.allResourceRecords[i]));
            }
        }

        scheduler.getForegroundDomConfigs(featureDomConfigs);

        syncConfigs.push.apply(syncConfigs, featureDomConfigs);

        DomSync.sync({
            domConfig : {
                onlyChildren : true,
                children     : syncConfigs
            },
            targetElement : scheduler.foregroundCanvas,
            syncIdField   : 'syncId',

            // Called by DomHelper when it creates, releases or reuses elements
            callback({ action, domConfig, lastDomConfig, targetElement, jsx }) {
                const { reactComponent } = scheduler;
                // If element is an event wrap, trigger appropriate events
                if (me.isEventElement(domConfig) || jsx || domConfig?.elementData?.jsx) {
                    const
                        // Some actions are considered first a release and then a render (reusing another element).
                        // This gives clients code a chance to clean up before reusing an element
                        isRelease = releaseEventActions[action],
                        isRender  = renderEventActions[action];

                    if (scheduler.processEventContent?.({
                        action,
                        domConfig,
                        isRelease : false,
                        targetElement,
                        reactComponent,
                        jsx

                    })) return;

                    // If we are reusing an element that was previously released we should not trigger again
                    if (isRelease && me.isEventElement(lastDomConfig) && !lastDomConfig.isReleased) {
                        const
                            data  = lastDomConfig.elementData.renderData,
                            event = {
                                renderData       : data,
                                assignmentRecord : data.assignmentRecord,
                                eventRecord      : data.eventRecord,
                                resourceRecord   : data.resourceRecord,
                                element          : targetElement
                            };

                        // Release any portal in React event content
                        scheduler.processEventContent?.({
                            isRelease,
                            targetElement,
                            reactComponent,
                            assignmentRecord : data.assignmentRecord
                        });

                        // Some browsers do not blur on set to display:none, so releasing the active element
                        // must *explicitly* move focus outwards to the view.
                        if (targetElement === DomHelper.getActiveElement(targetElement)) {
                            scheduler.focusElement.focus();
                        }

                        // This event is documented on Scheduler
                        scheduler.trigger('releaseEvent', event);
                    }

                    if (isRender) {
                        const
                            data  = domConfig.elementData.renderData,
                            event = {
                                renderData       : data,
                                assignmentRecord : data.assignmentRecord,
                                eventRecord      : data.eventRecord,
                                resourceRecord   : data.resourceRecord,
                                element          : targetElement,
                                isReusingElement : action === 'reuseElement',
                                isRepaint        : action === 'reuseOwnElement'
                            };

                        event.reusingElement = action === 'reuseElement';

                        // This event is documented on Scheduler
                        scheduler.trigger('renderEvent', event);
                    }
                }
            }
        });

        // Change in displayed resources?
        if (me.firstResource !== firstResource || me.lastResource !== lastResource) {
            // Update header to match
            const range = me.resourceColumns.visibleResources = { firstResource, lastResource };

            // Store which resources are currently in view
            me.firstResource = firstResource;
            me.lastResource  = lastResource;

            scheduler.onVisibleResourceRangeChange(range);
            scheduler.trigger('resourceRangeChange', range);
        }
    }

    refresh(transition) {
        this.scheduler.runWithTransition(() => this.renderer(), transition);
    }

    // To match horizontals API, used from EventDrag
    refreshResources(resourceIds) {
        this.clearResources(resourceIds);
        this.refresh();
    }

    // To match horizontals API, used from EventDrag
    refreshEventsForResource(recordOrRow, force = true, draw = true) {
        this.refreshResources([recordOrRow.id]);
    }

    onRenderDone() {

    }

    //endregion

    //region Other

    get timeView() {
        return this.scheduler.timeView;
    }

    //endregion

    //region Cache

    // Clears cached resource layout
    clearResources(resourceIds) {
        const { resourceMap, eventMap } = this;



        resourceIds.forEach(resourceId => {
            if (resourceMap.has(resourceId)) {
                // The *keys* of an Object are strings, so we must iterate the values
                // and use the original eventId to look up in the Map which preserves key type.
                Object.values(resourceMap.get(resourceId)).forEach(({ renderData : { eventId } }) => {
                    delete eventMap.get(eventId)[resourceId];
                });

                resourceMap.delete(resourceId);
            }
        });
    }

    clearAll() {


        this.resourceMap.clear();
        this.eventMap.clear();
    }

    //endregion
}
