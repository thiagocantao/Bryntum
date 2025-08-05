import Base from '../../../Core/Base.js';
import BrowserHelper from '../../../Core/helper/BrowserHelper.js';
import DomHelper from '../../../Core/helper/DomHelper.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import EventHelper from '../../../Core/helper/EventHelper.js';
import DomDataStore from '../../../Core/data/DomDataStore.js';
import GlobalEvents from '../../../Core/GlobalEvents.js';

/**
 * @module Scheduler/view/mixin/TimelineDomEvents
 */

const { eventNameMap } = EventHelper;

/**
 * An object which encapsulates a schedule timeline tick context based on a DOM event. This will include
 * the row and resource information and the tick and time information for a DOM pointer event detected
 * in the timeline.
 * @typedef {Object} TimelineContext
 * @property {Event} domEvent The DOM event which triggered the context change.
 * @property {HTMLElement} eventElement If the `domEvent` was on an event bar, this will be the event bar element.
 * @property {HTMLElement} cellElement The cell element under the `domEvent`
 * @property {Date} date The date corresponding to the `domEvent` position in the timeline
 * @property {Scheduler.model.TimeSpan} tick A {@link Scheduler.model.TimeSpan} record which encapsulates the contextual tick
 * @property {Number} tickIndex The contextual tick index. This may be fractional.
 * @property {Number} tickParentIndex The integer contextual tick index.
 * @property {Date} tickStartDate The start date of the contextual tick.
 * @property {Date} tickEndDate The end date of the contextual tick.
 * @property {Grid.row.Row} row The contextual {@link Grid.row.Row}
 * @property {Number} index The contextual row index
 * @property {Scheduler.model.EventModel} [eventRecord] The contextual event record (if any) if the event source is a `Scheduler`
 * @property {Scheduler.model.AssignmentModel} [assignmentRecord] The contextual assignment record (if any) if the event source is a `Scheduler`
 * @property {Scheduler.model.ResourceModel} [resourceRecord] The contextual resource record(if any)  if the event source is a `Scheduler`
 */

/**
 * Mixin that handles dom events (click etc) for scheduler and rendered events.
 *
 * @mixin
 */
export default Target => class TimelineDomEvents extends (Target || Base) {
    /**
     * Fires after a click on a time axis cell
     * @event timeAxisHeaderClick
     * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
     * @param {Date} startDate The start date of the header cell
     * @param {Date} endDate The end date of the header cell
     * @param {Event} event The event object
     */

    /**
     * Fires after a double click on a time axis cell
     * @event timeAxisHeaderDblClick
     * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
     * @param {Date} startDate The start date of the header cell
     * @param {Date} endDate The end date of the header cell
     * @param {Event} event The event object
     */

    /**
     * Fires after a right click on a time axis cell
     * @event timeAxisHeaderContextMenu
     * @param {Scheduler.column.TimeAxisColumn|Scheduler.column.VerticalTimeAxisColumn} source The column object
     * @param {Date} startDate The start date of the header cell
     * @param {Date} endDate The end date of the header cell
     * @param {Event} event The event object
     */

    static $name = 'TimelineDomEvents';

    //region Default config

    static configurable = {
        /**
         * The currently hovered timeline context. This is updated as the mouse or pointer moves over the timeline.
         * @member {TimelineContext} timelineContext
         * @readonly
         * @category Dates
         */
        timelineContext : {
            $config : {
                // Reject non-changes so that when set from scheduleMouseMove and EventMouseMove,
                // we only update the context and fire events when it changes.
                equal(c1, c2) {
                    // index is the resource index, tickParentIndex is the
                    // tick's index in the TimeAxis.
                    return c1?.index === c2?.index &&
                        c1?.tickParentIndex === c2?.tickParentIndex &&
                        !((c1?.tickStartDate || 0) - (c2?.tickStartDate || 0));
                }
            }
        },

        /**
         * By default, scrolling the schedule will update the {@link #property-timelineContext} to reflect the new
         * currently hovered context. When displaying a large number of events on screen at the same time, this will
         * have a slight impact on scrolling performance. In such scenarios, opt out of this behavior by setting
         * this config to `false`.
         * @default
         * @prp {Boolean}
         * @category Misc
         */
        updateTimelineContextOnScroll : true,

        /**
         * Set to `true` to ignore reacting to DOM events (mouseover/mouseout etc) while scrolling. Useful if you
         * want to maximize scroll performance.
         * @config {Boolean}
         * @default false
         */
        ignoreDomEventsWhileScrolling : null
    };

    static properties = {
        schedulerEvents : {
            pointermove : 'handleScheduleEvent',
            mouseover   : 'handleScheduleEvent',
            mousedown   : 'handleScheduleEvent',
            mouseup     : 'handleScheduleEvent',
            click       : 'handleScheduleEvent',
            dblclick    : 'handleScheduleEvent',
            contextmenu : 'handleScheduleEvent',
            mousemove   : 'handleScheduleEvent',
            mouseout    : 'handleScheduleEvent'
        }
    };

    static delayable = {
        // Allow the scroll event to complete in its thread, and dispatch the mousemove event next AF
        onScheduleScroll : 'raf'
    };

    // Currently hovered events (can be parent + child)
    hoveredEvents = new Set();

    //endregion

    //region Init

    /**
     * Adds listeners for DOM events for the scheduler and its events.
     * Which events is specified in Scheduler#schedulerEvents.
     * @private
     */
    initDomEvents() {
        const
            me = this,
            { schedulerEvents }  = me;

        // Set thisObj and element of the configured listener specs.
        schedulerEvents.element = me.timeAxisSubGridElement;
        schedulerEvents.thisObj = me;
        EventHelper.on(schedulerEvents);
        EventHelper.on({
            element    : me.timeAxisSubGridElement,
            mouseleave : 'handleScheduleLeaveEvent',
            capture    : true,
            thisObj    : me
        });

        // This is to handle scroll events while the mouse is over the schedule.
        // For example magic mouse or touchpad scrolls, or scrolls caused by keyboard
        // navigation while the mouse happens to be over the schedule.
        // The context must update. We must consider any scroll because the document
        // or some other wrapping element could be scrolling the Scheduler under the mouse.
        if (me.updateTimelineContextOnScroll && BrowserHelper.supportsPointerEventConstructor) {
            EventHelper.on({
                element : document,
                scroll  : 'onScheduleScroll',
                capture : true,
                thisObj : me
            });
        }
    };

    //endregion

    //region Event handling
    getTimeSpanMouseEventParams(eventElement, event) {
        throw new Error('Implement in subclass');
    }

    getScheduleMouseEventParams(cellData, event) {
        throw new Error('Implement in subclass');
    }

    /**
     * Wraps dom Events for the scheduler and event bars and fires as our events.
     * For example click -> scheduleClick or eventClick
     * @private
     * @param event
     */
    handleScheduleEvent(event) {
        const me = this;

        if (me.ignoreDomEventsWhileScrolling && (me.scrolling || me.timeAxisSubGrid.scrolling)) {
            return;
        }

        const timelineContext = me.getTimelineEventContext(event);

        // Cache the last pointer event so that  when scrolling below the mouse
        // we can inject mousemove events at that point.
        me.lastPointerEvent = event;

        // We are over the schedule region
        if (timelineContext) {
            // Only fire a scheduleXXXX event if we are *not* over an event.
            // If over an event fire (event|task)XXXX.
            me.trigger(`${timelineContext.eventElement ? me.scheduledEventName : 'schedule'}${eventNameMap[event.type] || StringHelper.capitalize(event.type)}`, timelineContext);
        }

        // If the context has changed, updateTimelineContext will fire events
        me.timelineContext = timelineContext;
    }

    handleScheduleLeaveEvent(event) {
        if (event.target === this.timeAxisSubGridElement) {
            this.handleScheduleEvent(event);
        }
    }

    /**
     * This handles the scheduler being scrolled below the mouse by trackpad or keyboard events.
     * The context, if present needs to be recalculated.
     * @private
     */
    onScheduleScroll({ target }) {
        const me = this;
        // If the latest mouse event resulted in setting a context, we need to reproduce that event at the same clientX,
        // clientY in order to keep the context up to date while scrolling.
        // If the scroll is because of a pan feature drag (on us or a partner), we must not do this.
        // Target might be removed in salesforce by Locker Service if scroll event occurs on body
        if (
            target && me.updateTimelineContextOnScroll && !me.features.pan?.isActive &&
            !me.partners.some(p => p.features.pan?.isActive) &&
            (target.contains(me.element) || me.bodyElement.contains(target))
        ) {
            const { timelineContext, lastPointerEvent } = me;

            if (timelineContext) {
                const
                    targetElement = DomHelper.elementFromPoint(timelineContext.domEvent.clientX, timelineContext.domEvent.clientY),
                    pointerEvent  = new BrowserHelper.PointerEventConstructor('pointermove', lastPointerEvent),
                    mouseEvent    = new MouseEvent('mousemove', lastPointerEvent);

                // See https://github.com/bryntum/support/issues/6274
                // The pointerId does not propagate correctly on the synthetic PointerEvent, but also is readonly, so
                // redefine the property. This is required by Ext JS gesture publisher which tracks pointer movements
                // while a pointer is down. Without the correct pointerId, Ext JS would see this move as a "missed"
                // pointerdown and forever await its pointerup (i.e., it would get stuck in the activeTouches). This
                // would cause all future events to be perceived as part of or the end of a drag and would never again
                // dispatch pointer events correctly. Finally, lastPointerEvent.pointerId is often incorrect (undefined
                // in fact), so check the most recent pointerdown/touchstart event and default to 1
                Object.defineProperty(pointerEvent, 'pointerId', {
                    value : GlobalEvents.currentPointerDown?.pointerId ?? GlobalEvents.currentTouch?.identifier ?? 1
                });

                // Drag code should ignore these synthetic events
                pointerEvent.scrollInitiated = mouseEvent.scrollInitiated = true;

                // Emulate the correct browser sequence for mouse move events
                targetElement?.dispatchEvent(pointerEvent);
                targetElement?.dispatchEvent(mouseEvent);
            }
        }
    }

    updateTimelineContext(context, oldContext) {
        /**
         * Fired when the pointer-activated {@link #property-timelineContext} has changed.
         * @event timelineContextChange
         * @param {TimelineContext} oldContext The tick/resource context being deactivated.
         * @param {TimelineContext} context The tick/resource context being activated.
         */
        this.trigger('timelineContextChange', { oldContext, context });

        if (context && !oldContext) {
            this.trigger('scheduleMouseEnter', context);
        }
        else if (!context) {
            this.trigger('scheduleMouseLeave', { event : oldContext.event });
        }
    }

    /**
     * Gathers contextual information about the schedule contextual position of the passed event.
     *
     * Used by schedule mouse event handlers, but also by the scheduleContext feature.
     * @param {Event} domEvent The DOM event to gather context for.
     * @returns {TimelineContext} the schedule DOM event context
     * @internal
     */
    getTimelineEventContext(domEvent) {
        const
            me           = this,
            eventElement = domEvent.target.closest(me.eventInnerSelector),
            cellElement  = me.getCellElementFromDomEvent(domEvent);

        if (cellElement) {
            const date = me.getDateFromDomEvent(domEvent, 'floor');

            if (!date) {
                return;
            }

            const
                cellData    = DomDataStore.get(cellElement),
                mouseParams = eventElement ? me.getTimeSpanMouseEventParams(eventElement, domEvent) : me.getScheduleMouseEventParams(cellData, domEvent);

            if (!mouseParams) {
                return;
            }

            const
                index     = me.isVertical ? me.resourceStore.indexOf(mouseParams.resourceRecord) : cellData.row.dataIndex,
                tickIndex = me.timeAxis.getTickFromDate(date),
                tick      = me.timeAxis.getAt(Math.floor(tickIndex));

            if (tick) {
                return {
                    isTimelineContext : true,
                    domEvent,
                    eventElement,
                    cellElement,
                    index,
                    tick,
                    tickIndex,
                    date,
                    tickStartDate     : tick.startDate,
                    tickEndDate       : tick.endDate,
                    tickParentIndex   : tick.parentIndex,
                    row               : cellData.row,
                    event             : domEvent,
                    ...mouseParams
                };
            }
        }
    }

    getCellElementFromDomEvent({ target, clientY, type }) {
        const
            me           = this,
            {
                isVertical,
                foregroundCanvas
            }            = me,
            eventElement = target.closest(me.eventSelector);

        // If event was on an event bar, calculate the cell.
        if (eventElement) {
            return me.getCell({
                [isVertical ? 'row' : 'record'] : isVertical ? 0 : me.resolveRowRecord(eventElement),
                column                          : me.timeAxisColumn
            });
        }
        // If event was triggered by an element in the foreground canvas, but not an event element
        // we need to ascertain the cell "behind" that element to be able to create the context.
        else if (foregroundCanvas.contains(target)) {
            // Only trigger a Scheduler event if the event was on the background itself.
            // Otherwise, we will trigger unexpected events on things like dependency lines which historically
            // have never triggered scheduleXXXX events. The exception to this is the mousemove event which
            // needs to always fire so that timelineContext and scheduleTooltip correctly track the mouse
            if (target === foregroundCanvas || type === 'mousemove') {
                return me.rowManager.getRowAt(clientY, false)?.getCell(me.timeAxisColumn.id);
            }
        }
        else {
            // Event was inside a row, or on a row border.
            return target.matches('.b-grid-row') ? target.firstElementChild : target.closest(me.timeCellSelector);
        }
    }

    // Overridden by ResourceTimeRanges to "pass events through" to the schedule
    matchScheduleCell(element) {
        return element.closest(this.timeCellSelector);
    }

    onElementMouseButtonEvent(event) {
        const targetCell = event.target.closest('.b-sch-header-timeaxis-cell');
        if (targetCell) {
            const
                me           = this,
                position     = targetCell.parentElement.dataset.headerPosition,
                headerCells  = me.timeAxisViewModel.columnConfig[position],
                index        = me.timeAxis.isFiltered ? headerCells.findIndex(cell => cell.index == targetCell.dataset.tickIndex) : targetCell.dataset.tickIndex,
                cellConfig   = headerCells[index],
                contextMenu  = me.features.contextMenu;

            // Skip same events with Grid context menu triggerEvent
            if (!contextMenu || event.type !== contextMenu.triggerEvent) {
                this.trigger(`timeAxisHeader${StringHelper.capitalize(event.type)}`, {
                    startDate : cellConfig.start,
                    endDate   : cellConfig.end,
                    event
                });
            }
        }
    }

    onElementMouseDown(event) {
        this.onElementMouseButtonEvent(event);
        super.onElementMouseDown(event);
    }

    onElementClick(event) {
        this.onElementMouseButtonEvent(event);
        super.onElementClick(event);
    }

    onElementDblClick(event) {
        this.onElementMouseButtonEvent(event);
        super.onElementDblClick(event);
    }

    onElementContextMenu(event) {
        this.onElementMouseButtonEvent(event);
        super.onElementContextMenu(event);
    }

    /**
     * Relays mouseover events as eventmouseenter if over rendered event.
     * Also adds Scheduler#overScheduledEventClass to the hovered element.
     * @private
     */
    onElementMouseOver(event) {
        const
            me                = this;

        if (me.ignoreDomEventsWhileScrolling && (me.scrolling || me.timeAxisSubGrid.scrolling)) {
            return;
        }

        super.onElementMouseOver(event);

        const
            { target }        = event,
            { hoveredEvents } = me;

        // We must be over the event bar
        if (target.closest(me.eventInnerSelector) && !me.features.eventDrag?.isDragging) {
            const eventElement = target.closest(me.eventSelector);

            if (!hoveredEvents.has(eventElement) && !me.preventOverCls) {
                hoveredEvents.add(eventElement);
                eventElement.classList.add(me.overScheduledEventClass);

                const params = me.getTimeSpanMouseEventParams(eventElement, event);
                if (params) {
                    // do not fire this event if model cannot be found
                    // this can be the case for "b-sch-dragcreator-proxy" elements for example
                    me.trigger(`${me.scheduledEventName}MouseEnter`, params);
                }
            }
        }
        else if (hoveredEvents.size) {
            me.unhoverAll(event);
        }
    }

    /**
     * Relays mouseout events as eventmouseleave if out from rendered event.
     * Also removes Scheduler#overScheduledEventClass from the hovered element.
     * @private
     */
    onElementMouseOut(event) {
        super.onElementMouseOut(event);

        const
            me                        = this,
            { target, relatedTarget } = event,
            eventInner                = target.closest(me.eventInnerSelector),
            eventWrap                 = target.closest(me.eventSelector),
            timeSpanRecord            = me.resolveTimeSpanRecord(target);

        // We must be over the event bar

        if (eventInner && timeSpanRecord && me.hoveredEvents.has(eventWrap) && !me.features.eventDrag?.isDragging) {
            // out to child shouldn't count...
            if (relatedTarget && DomHelper.isDescendant(eventInner, relatedTarget)) {
                return;
            }

            me.unhover(eventWrap, event);
        }
    }

    unhover(element, event) {
        const me = this;

        element.classList.remove(me.overScheduledEventClass);
        me.trigger(`${me.scheduledEventName}MouseLeave`, me.getTimeSpanMouseEventParams(element, event));
        me.hoveredEvents.delete(element);
    }

    unhoverAll(event) {
        for (const element of this.hoveredEvents) {
            !element.isReleased && !element.classList.contains('b-released') && this.unhover(element, event);
        }

        // Might not be empty because of conditional unhover above
        this.hoveredEvents.clear();
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
