import Base from '../../../Core/Base.js';
import DomHelper from '../../../Core/helper/DomHelper.js';

/**
 * @module Scheduler/view/mixin/SchedulerScroll
 */

const
    defaultScrollOptions = {
        block      : 'nearest',
        edgeOffset : 20
    },
    unrenderedScrollOptions = {
        highlight : false,
        focus     : false
    };

/**
 * Functions for scrolling to events, dates etc.
 *
 * @mixin
 */
export default Target => class SchedulerScroll extends (Target || Base) {
    static get $name() {
        return 'SchedulerScroll';
    }

    //region Scroll to event

    /**
     * Scrolls an event record into the viewport.
     * If the resource store is a tree store, this method will also expand all relevant parent nodes to locate the event.
     *
     * This function is not applicable for events with multiple assignments, please use #scrollResourceEventIntoView instead.
     *
     * @param {Scheduler.model.EventModel} eventRecord the event record to scroll into view
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @async
     * @category Scrolling
     */
    async scrollEventIntoView(eventRecord, options = defaultScrollOptions) {
        const
            me        = this,
            resources = eventRecord.resources || [eventRecord];

        if (resources.length > 1) {
            throw new Error('scrollEventIntoView() is not applicable for events with multiple assignments, please use scrollResourceEventIntoView() instead.');
        }

        if (!resources.length) {
            console.warn('You have asked to scroll to an event which is not assigned to a resource');
        }

        await me.scrollResourceEventIntoView(resources[0], eventRecord, options);
    }

    /**
     * Scrolls an assignment record into the viewport.
     *
     * If the resource store is a tree store, this method will also expand all relevant parent nodes
     * to locate the event.
     *
     * @param {Scheduler.model.AssignmentModel} assignmentRecord A resource record an event record is assigned to
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @category Scrolling
     */
    scrollAssignmentIntoView(assignmentRecord, ...args) {
        return this.scrollResourceEventIntoView(assignmentRecord.resource, assignmentRecord.event, ...args);
    }

    /**
     * Scrolls a resource event record into the viewport.
     *
     * If the resource store is a tree store, this method will also expand all relevant parent nodes
     * to locate the event.
     *
     * @param {Scheduler.model.ResourceModel} resourceRecord A resource record an event record is assigned to
     * @param {Scheduler.model.EventModel} eventRecord An event record to scroll into view
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A Promise which resolves when the scrolling is complete.
     * @category Scrolling
     * @async
     */
    async scrollResourceEventIntoView(resourceRecord, eventRecord, options = defaultScrollOptions) {
        const
            me             = this,
            eventStart     = eventRecord.startDate,
            eventEnd       = eventRecord.endDate,
            eventIsOutside = eventRecord.isScheduled && eventStart < me.timeAxis.startDate | ((eventEnd > me.timeAxis.endDate) << 1);


        if (arguments.length > 3) {
            options = arguments[3];
        }

        let el;

        if (options.edgeOffset == null) {
            options.edgeOffset = 20;
        }

        // Make sure event is within TimeAxis time span unless extendTimeAxis passed as false.
        // The EventEdit feature passes false because it must not mutate the TimeAxis.
        // Bitwise flag:
        //  1 === start is before TimeAxis start.
        //  2 === end is after TimeAxis end.
        if (eventIsOutside && options.extendTimeAxis !== false) {
            const currentTimeSpanRange = me.timeAxis.endDate - me.timeAxis.startDate;

            // Event is too wide, expand the range to encompass it.
            if (eventIsOutside === 3) {
                await me.setTimeSpan(
                    new Date(eventStart.valueOf() - currentTimeSpanRange / 2),
                    new Date(eventEnd.valueOf()  + currentTimeSpanRange / 2)
                );
            }
            else if (me.infiniteScroll) {
                const
                    { visibleDateRange } = me,
                    visibleMS = visibleDateRange.endMS - visibleDateRange.startMS,
                    // If event starts before time axis, scroll to a date one full viewport after target date
                    // (reverse for an event starting after time axis), to allow a scroll animation
                    sign = eventIsOutside & 1 ? 1 : -1;

                await me.setTimeSpan(
                    new Date(eventStart.valueOf()  - currentTimeSpanRange / 2),
                    new Date(eventStart.valueOf() + currentTimeSpanRange / 2),
                    {
                        visibleDate : new Date(eventEnd.valueOf() + (sign * visibleMS))
                    }
                );
            }
            // Event is partially or wholly outside but will fit.
            // Move the TimeAxis to include it. That will maintain visual position.
            else {
                // Event starts before
                if (eventIsOutside & 1) {
                    await me.setTimeSpan(
                        new Date(eventStart),
                        new Date(eventStart.valueOf() + currentTimeSpanRange)
                    );
                }
                // Event ends after
                else {
                    await me.setTimeSpan(
                        new Date(eventEnd.valueOf() - currentTimeSpanRange),
                        new Date(eventEnd)
                    );
                }
            }
        }

        if (me.store.tree) {
            // If we're a tree, ensure parents are expanded first
            await me.expandTo?.(resourceRecord);
        }

        // Handle nested events too
        if (eventRecord.parent && !eventRecord.parent.isRoot) {
            await this.scrollEventIntoView(eventRecord.parent);
        }

        // Establishing element to scroll to
        el = me.getElementFromEventRecord(eventRecord, resourceRecord);

        if (el) {
            // It's usually the event wrapper that holds focus
            if (!DomHelper.isFocusable(el)) {
                el = el.parentNode;
            }

            const scroller = me.timeAxisSubGrid.scrollable;

            // Scroll into view with animation and highlighting if needed.
            await scroller.scrollIntoView(el, options);
        }
        // If event is fully outside the range, and we are not allowed to extend
        // the range, then we cannot perform the operation.
        else if (eventIsOutside === 3 && options.extendTimeAxis === false) {
            console.warn('You have asked to scroll to an event which is outside the current view and extending timeaxis is disabled');
        }
        else if (!eventRecord.isOccurrence && !me.eventStore.isAvailable(eventRecord)) {
            console.warn('You have asked to scroll to an event which is not available');
        }
        else if (eventRecord.isScheduled) {
            // Event scheduled but not rendered, scroll to calculated location
            await me.scrollUnrenderedEventIntoView(resourceRecord, eventRecord, options);
        }
        else {
            // Event not scheduled, just scroll resource row into view
            await me.scrollResourceIntoView(resourceRecord, options);
        }
    }

    /**
     * Scrolls an unrendered event into view. Internal function used from #scrollResourceEventIntoView.
     * @private
     * @category Scrolling
     */
    scrollUnrenderedEventIntoView(resourceRec, eventRec, options = defaultScrollOptions) {
        // We must only resolve when the event's element has been painted
        // *and* the scroll has fully completed.
        return new Promise(resolve => {
            const
                me               = this,
                // Knock out highlight and focus options. They must be applied after the scroll
                // has fully completed and we have an element. Use a default edgeOffset of 20.
                modifiedOptions  = Object.assign({ edgeOffset : 20 }, options, unrenderedScrollOptions),
                scroller         = me.timeAxisSubGrid.scrollable,
                box              = me.getResourceEventBox(eventRec, resourceRec),
                scrollerViewport = scroller.viewport;

            // Event may fall on a time not included by workingTime settings
            if (!scrollerViewport || !box) {
                resolve();
                return;
            }

            // In case of subPixel position, scroll the whole pixel into view
            box.x = Math.ceil(box.x);
            box.y = Math.ceil(box.y);

            if (me.rtl) {
                // RTL scrolls in negative direction but coordinates are still LTR
                box.translate(-me.timeAxisViewModel.totalSize + scrollerViewport.width, 0);
            }

            // Note use of scroller.scrollLeft here. We need the natural DOM scrollLeft value
            // not the +ve X position along the scrolling axis.
            box.translate(scrollerViewport.x - scroller.scrollLeft, scrollerViewport.y - scroller.y);

            const
                // delta         = scroller.getDeltaTo(box, modifiedOptions)[me.isHorizontal ? 'xDelta' : 'yDelta'],
                onEventRender = async({ eventRecord, element, targetElement }) => {
                    if (eventRecord === eventRec) {
                        // Vertical's renderEvent is different to horizontal's
                        const el = element || targetElement;

                        detacher();

                        // Don't resolve until the scroll has fully completed.
                        await initialScrollPromise;

                        options.highlight && DomHelper.highlight(el);
                        options.focus && el.focus();

                        resolve();
                    }
                },
                // On either paint or repaint of the event, resolve the scroll promise and detach the listeners.
                detacher = me.ion({
                    renderEvent : onEventRender
                }),
                initialScrollPromise = scroller.scrollIntoView(box, modifiedOptions);

            initialScrollPromise.then(() => {
                if (initialScrollPromise.cancelled) {
                    resolve();
                }
            });
        });
    }

    /**
     * Scrolls the specified resource into view, works for both horizontal and vertical modes.
     * @param {Scheduler.model.ResourceModel} resourceRecord A resource record an event record is assigned to
     * @param {ScrollOptions} [options] How to scroll.
     * @returns {Promise} A promise which is resolved when the scrolling has finished.
     * @category Scrolling
     */
    scrollResourceIntoView(resourceRecord, options = defaultScrollOptions) {
        if (this.isVertical) {
            return this.currentOrientation.scrollResourceIntoView(resourceRecord, options);
        }
        return this.scrollRowIntoView(resourceRecord, options);
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
