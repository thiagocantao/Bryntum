import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import Delayable from '../../Core/mixin/Delayable.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import AttachToProjectMixin from '../../Scheduler/data/mixin/AttachToProjectMixin.js';

/**
 * @module SchedulerPro/feature/NestedEvents
 */

const borderWidths     = {
    border : 1,
    hollow : 2
};

// Future improvements might include:
// * Add info to EventTooltip, parent could display number of children, child could display parent name
// * Add parent picker to EventEdit
// * Handle reassigning in editor, what happens if you reassign to a resource that events parent is not assigned to...



/**
 * A feature that renders child events nested inside their parent. Requires Scheduler Pro to use a tree event store
 * (normally handled automatically when events in data has children).
 *
 * {@inlineexample SchedulerPro/feature/NestedEvents.js}
 *
 * The feature has configs for {@link #config-eventLayout}, {@link #config-resourceMargin} and {@link #config-barMargin}
 * that are separate from those on Scheduler Pro and only affect nested events.
 *
 * You can by default drag nested events out of their parents and drop any event onto root level events to nest. The
 * drag and drop behaviour can be customized using the {@link #config-constrainDragToParent},
 * {@link #config-allowNestingOnDrop} and {@link #config-allowDeNestingOnDrop} configs.
 *
 * <div class="note">Note that for a nested event to show up for a resource both the parent and the nested event has to
 * be assigned to that resource.</div>
 *
 * ## Parent / children scheduling
 *
 * Scheduler Pro uses a scheduling engine closely related to the one used by Gantt (a subset of it). It for example
 * schedules based on calendars, dependencies and constraints. Part of its default logic is that parent events start and
 * end dates (and thus duration) is defined by their children. This means that if you remove the latest scheduled child
 * of a parent, the parents end date and duration will be adjusted to match the new latest scheduled child.
 *
 * Depending on what you plan to use nested events for in your application, this might not be the desired behaviour. If
 * you want the parent element to keep its dates regardless of its children, you should flag it as
 * {@link SchedulerPro/model/EventModel#field-manuallyScheduled}.
 *
 * A parent defined like this will shrink / grow with its children:
 *
 * ```json
 * {
 *     "id"        : 1,
 *     "startDate" : "2022-03-24",
 *     "children"  : [
 *         ...
 *     ]
 * }
 * ```
 *
 * Try removing an event here to see what happens:
 *
 * {@inlineexample SchedulerPro/feature/NestedEventsNotManually.js}
 *
 * A parent with `manuallyScheduled : true` will **not** shrink / grow with is children:
 *
 * ```json
 * {
 *     "id"                : 1,
 *     "startDate"         : "2022-03-24",
 *     "duration"          : 10,
 *     "manuallyScheduled" : true
 *     "children"          : [
 *         ...
 *     ]
 * }
 * ```
 *
 * Try the same thing here:
 *
 * {@inlineexample SchedulerPro/feature/NestedEventsManually.js}
 *
 * <div class="note">Note that this also makes resizing a parent event that is not manually scheduled useless, it would
 * only snap back to the dates defined by its children. To avoid confusion, resizing is therefor turned off for parent
 * events unless they have `manuallyScheduled: true`</div>
 *
 * ## Drag and drop for parent events
 *
 * Normally the dates of a parent event is defined by its children (as described above), with exception for when drag
 * dropping a parent event along the time axis. In this case the operation will update the dates of all the children,
 * which will thus also move the parent event in time.
 *
 * If a parent event is dragged to a new resource, all its children will also be assigned to that resource.
 *
 * ## Caveats
 *
 * Usage of the feature comes with some requirements/caveats:
 * * As already mentioned, it requires a tree event store
 * * Requires using an AssignmentStore, the legacy single assignment mode does not handle tree stores
 * * Scheduler must use stack or overlap {@link SchedulerPro/view/SchedulerPro#config-eventLayout}, pack not supported
 * * {@link Scheduler/feature/Dependencies} are not supported for nested events
 * * {@link Scheduler/feature/EventDragSelect} is not supported
 * * Multi event drag is not supported for nested events
 * * Cannot {@link Scheduler/feature/EventDragCreate} within parent events
 * * {@link Scheduler/feature/Labels} are not supported for nested events
 * * {@link SchedulerPro/feature/EventBuffer} won't work with nested events
 * * {@link SchedulerPro/feature/TaskEdit} does not allow assigning resources or dependencies to nested events
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @classtype nestedEvents
 * @feature
 */
export default class NestedEvents extends InstancePlugin.mixin(AttachToProjectMixin, Delayable) {
    static $name = 'NestedEvents';

    //region Config

    static configurable = {
        /**
         * This config defines how to handle overlapping nested events. Valid values are:
         * - `stack`, events use fixed height and stack on top of each other (not supported in vertical mode)
         * - `pack`, adjusts event height
         * - `none`, allows events to overlap
         *
         * <div class="note">Note that stacking works differently for nested events as compared to normal events (and
         * not at all in vertical mode). The height of the parent event will never change, all nested events use
         * {@link #config-eventHeight fixed height} and will stack until all available space is consumed, after which
         * they will overflow the parent.</div>
         *
         * <div class="note">Also note that stacked nested events are clipped by the parent, making it scrollable on
         * vertical overflow. This cannot be combined with sticky events. If stacking events in your app won't overflow
         * the parent, you can specify `overflow: visible` on `.b-nested-events-container.b-nested-events-layout-stack`
         * to not clip and make sticky events work.</div>
         *
         * @prp {'stack'|'pack'|'none'}
         * @default
         */
        eventLayout : 'pack',

        /**
         * Vertical (horizontal in vertical mode) space between nested event bars, in px
         * @prp {Number}
         * @default
         */
        barMargin : 5,

        /**
         * Margin above first nested event bar and below last (or before / after in vertical mode), in px
         * @prp {Number}
         * @default
         */
        resourceMargin : 0,

        /**
         * Fixed event height (width in vertical mode) to use when configured with `eventLayout : 'stack'`.
         *
         * Also accepts an array, used to control height for each level if nesting deeper than 1 level. Make sure you
         * supply a value for each level, where later values are smaller than earlier ones.
         *
         * ```javascript
         * const scheduler = new SchedulerPro({
         *     features : {
         *         nestedEvents : {
         *         eventHeight : [40, 20]
         *     }
         * });
         * ```
         *
         * @prp {Number|Number[]}
         * @default
         */
        eventHeight : 30,

        /**
         * Space (in px) in a parent element reserved for displaying a title etc. Used to compute available space for
         * the nested events container inside the parent.
         *
         * Setting this config updates the ` --schedulerpro-nested-event-header-height` CSS variable.
         *
         * @prp {Number}
         * @default
         */
        headerHeight : 20,

        /**
         * Constrains dragging of nested events within their parent when configured as `true`, allows them to be
         * dragged out of it when configured as `false` (the default).
         * @prp {Boolean}
         * @default
         */
        constrainDragToParent : false,

        /**
         * Allow an event to be dropped on another to nest it.
         *
         * Dropping an event on another will add the dropped event as a child of the target, turning the target into a
         * parent if it was not already.
         *
         * Parent events dropped on another event are ignored.
         *
         * @prp {Boolean}
         * @default
         */
        allowNestingOnDrop : true,

        /**
         * Allow dropping a nested event directly on a resource to de-nest it, turning it into an ordinary event.
         *
         * Requires {@link #config-constrainDragToParent} to be configured with `false` to be applicable.
         *
         * @prp {Boolean}
         * @default
         */
        allowDeNestingOnDrop : true,

        /**
         * Constrains resizing of nested events to their parents start and end dates when configured as `true` (the
         * default), preventing them from changing their parents dates.
         *
         * Configure as `false` if you want to allow resizing operations to extend the parents dates (only applies for
         * parents not configured with `manuallyScheduled: true`).
         *
         * <div class="note">Note that when using `eventLayout: stack` the nested events are clipped by the parent, the
         * part extending outside if not constrained to parent will not be shown until it re-renders after resize. If
         * stacking events in your app won't overflow the parent, you can specify `overflow: visible` on
         * `.b-nested-events-container.b-nested-events-layout-stack` to not clip.</div>
         *
         * @prp {Boolean}
         * @default
         */
        constrainResizeToParent : true,

        /**
         * Maximum nesting level for events.
         *
         * Larger depths than 2 are not recommended, even if technically possible.
         *
         * @prp {Number}
         * @default
         */
        maxNesting : 1
    };

    static pluginConfig = {
        before : ['onEventStoreBatchedUpdate'],
        chain  : [
            'getEventsToRender', 'processEventDrop', 'processCrossSchedulerEventDrop',
            'beforeEventDragStart', 'afterEventDragStart', 'afterEventDragAbortFinalized',
            'checkEventDragValidity', 'afterEventResizeStart', 'afterRenderEvent'
        ],
        override : [
            'getResourceMargin', 'getBarMargin', 'getAppliedResourceHeight', 'getResourceWidth', 'getEventLayout',
            'getElementFromAssignmentRecord', 'scheduleEvent'
        ]
    };

    static delayable = {
        refreshClient : 'raf'
    };

    //endregion

    construct(client, config) {
        super.construct(client, config);

        // EventStore has to be a tree store for the feature to work.
        // If it starts empty, it might not be flagged as such. Help it out.
        this.client.eventStore.tree = true;
    }

    refreshClient() {
        !this.client.isConfiguring && this.client.refreshWithTransition();
    }

    doDisable() {
        this.refreshClient();
    }

    //region Props

    updateEventLayout(layout) {
        if (layout === 'stack' && this.client.isVertical) {
            console.warn('Stacked nested events are not supported in vertical mode');
        }

        this.refreshClient();
    }

    updateBarMargin() {
        this.refreshClient();
    }

    updateResourceMargin() {
        this.refreshClient();
    }

    changeEventHeight(height) {
        // Always an array for internal use, would be breaking to change it externally
        this._eventHeights = ArrayHelper.asArray(height);

        return height;
    }

    updateEventHeight() {
        this.refreshClient();
    }

    updateHeaderHeight(height) {
        this.client.element.style.setProperty('--schedulerpro-nested-event-header-height', `${height}px`);
        this.refreshClient();
    }

    // Nested events has their own layout setting
    getEventLayout(resourceRecord, parentEventRecord) {
        if (parentEventRecord) {
            return { type : this.eventLayout };
        }

        return this.overridden.getEventLayout(resourceRecord);
    }

    // Specific resource margin for nested events
    getResourceMargin(resourceRecord, parentEventRecord) {
        if (parentEventRecord && !parentEventRecord.isRoot) {
            return this.resourceMargin;
        }

        return this.overridden.getResourceMargin(resourceRecord);
    }

    // Specific bar margin for nested events
    getBarMargin(resourceRecord, parentEventRecord) {
        if (parentEventRecord && !parentEventRecord.isRoot) {
            return this.barMargin;
        }

        return this.overridden.getBarMargin(resourceRecord);
    }

    // Use height available inside the parent event
    getAppliedResourceHeight(resourceRecord, parentEventRecord) {
        const me = this;

        if (parentEventRecord && !parentEventRecord.isRoot) {
            if (me.eventLayout === 'stack') {
                const eventHeight = me._eventHeights[parentEventRecord.childLevel];
                // Layout subtracts resourceMargin * 2, added here to get eventHeight correct after
                return eventHeight + me.resourceMargin * 2;
            }
            else {
                const borderWidth = borderWidths[me.client.getEventStyle(parentEventRecord, resourceRecord)] ?? 0;
                return me.currentParentsHeight - me.headerHeight - borderWidth;
            }
        }

        return me.overridden.getAppliedResourceHeight(resourceRecord);
    }

    getResourceWidth(resourceRecord, parentEventRecord) {
        if (parentEventRecord && !parentEventRecord.isRoot) {
            return this.currentParentsWidth - this.headerHeight;
        }

        return this.overridden.getResourceWidth(resourceRecord);
    }

    //endregion

    //region CRUD listeners

    attachToEventStore(eventStore) {
        eventStore?.ion({
            name    : 'eventStore',
            change  : 'onEventStoreChange',
            thisObj : this
        });
    }

    onEventStoreChange({ records }) {
        // Refresh if a nested event was changed
        if (records?.some(r => r.parent && !r.parent.isRoot)) {
            this.refreshClient();
        }
    }

    onEventStoreBatchedUpdate({ records }) {
        // Refresh if a nested event was changed, and we are listening for batched changes (resizing)
        if (this.client.listenToBatchedUpdates && records?.some(r => r.parent && !r.parent.isRoot)) {
            this.refreshClient();
            // Prevent default handler
            return false;
        }
    }

    //endregion

    //region Drag

    // Move event element to foreground canvas during drag. Has to happen before drag starts for the feature to pick up
    // correct coordinates to resolve resource by, transition back to on abort etc.
    beforeEventDragStart(context, dragData) {
        const
            me                                 = this,
            { client }                         = me,
            { eventRecord, assignmentRecords } = dragData,
            { parentElement }                  = context.element;

        // Dragging nested events?
        if (eventRecord.parent && parentElement !== client.foregroundCanvas) {
            me.isDraggingNestedEvent = true;

            // Remember origin to be able to restore on abort (success redraws so that will be covered anyway)
            context.originalParentElement = parentElement;
            context.originalBounds = [];

            for (const assignment of assignmentRecords) {
                const { event } = assignment;

                // UI should not allow selecting nested events from different parents, but it is programmatically
                // possible. We only include from the dragged events parent here, behaviour for mixed parents are for
                // now undefined
                if (event.parent === eventRecord.parent) {
                    const eventElement = client.getElementFromAssignmentRecord(assignment, true);

                    context.originalBounds.push({
                        element : eventElement,
                        bounds  : Rectangle.from(eventElement, parentElement)
                    });

                    if (!me.constrainDragToParent && client.features.eventDrag.constrainDragToTimeline) {
                        // Pull nested events out
                        const relativeBounds = Rectangle.from(eventElement, client.timeAxisSubGridElement);
                        eventElement.style.top = `${relativeBounds.top}px`;
                        eventElement.style.left = `${relativeBounds.left}px`;

                        DomSync.addChild(client.foregroundCanvas, eventElement, assignment.id);
                    }
                }
            }
        }
        else {
            me.isDraggingNestedEvent = false;
        }
    }

    // Setup constraints when drag starts if needed
    afterEventDragStart(context, dragData) {
        // Constrain to current parent?
        if (this.isDraggingNestedEvent && this.constrainDragToParent) {
            const
                { eventDrag } = this.client.features,
                { parent }    = dragData.eventRecord,
                parentBounds  = context.originalParentElement.getBoundingClientRect();

            // Constrain top / bottom
            eventDrag.setYConstraint(0, parentBounds.height - context.originalBounds[0].bounds.height);

            // For left / right we also have to constrain the dates, otherwise only the element will be constrained
            eventDrag.setXConstraint(0, parentBounds.width - context.originalBounds[0].bounds.width);
            dragData.dateConstraints = { start : parent.startDate, end : parent.endDate };
        }
    }

    checkEventDragValidity({ targetEventRecord, eventRecord, timeDiff, newResource, resourceRecord }) {
        const me = this;

        // Disallow dropping on a blank space in a resource if configured to not allow de-nesting
        // (ignore first round, targetEventRecord cannot be resolved until on next, which we determine here by checking
        // timeDiff or resource change)
        if (me.isDraggingNestedEvent && !me.allowDeNestingOnDrop && !targetEventRecord && (timeDiff || newResource !== resourceRecord)) {
            return {
                valid   : false,
                message : me.L('L{deNestingNotAllowed}')
            };
        }

        // Disallow dropping on a new parent if configured to not allow nesting
        if (!me.allowNestingOnDrop && targetEventRecord && targetEventRecord !== eventRecord.parent) {
            return {
                valid   : false,
                message : me.L('L{nestingNotAllowed}')
            };
        }

        if (targetEventRecord && targetEventRecord !== eventRecord.parent) {
            const
                maxLevel           = me.maxNesting,
                targetLevel        = targetEventRecord.isParent ? targetEventRecord.childLevel : targetEventRecord.parent.childLevel,
                maxChildLevel      = Math.max(...eventRecord.allChildren.map(child => child.childLevel)),
                relativeChildLevel = maxChildLevel - eventRecord.childLevel;

            if (targetLevel + relativeChildLevel >= maxLevel) {
                return {
                    valid   : false,
                    message : me.L('L{nestingNotAllowed}')
                };
            }
        }
    }

    // Move event to new parent if dropped on a parent or moved out of one
    processEventDrop({ context, toScheduler, eventRecord, resourceRecord, reassignedFrom, element, eventsToAdd, addedEvents, draggedAssignment }) {
        const
            { parent }            = eventRecord,
            { targetEventRecord } = context;

        let newParent = parent;

        // targetEventRecord is resolved using mouse coords, it might be outside of parent when constrained thus
        // we have to check if constrained here to not move it out by mistake
        if (parent !== targetEventRecord && !this.constrainDragToParent) {
            // Dropped on a new parent and allowed to nest
            if (targetEventRecord && this.allowNestingOnDrop) {
                // Allow creating a new parent if dropped on a child of root, otherwise add to the parent
                newParent = targetEventRecord.isParent ? targetEventRecord : targetEventRecord.parent.isRoot ? targetEventRecord : targetEventRecord.parent;
                // We resolve resource and targetEventRecord differently (mouse vs element), might get next resource so
                // we re-resolve here to be sure it is correct
                const targetResource = this.client.resolveResourceRecord(context.browserEvent);
                if (targetResource !== resourceRecord) {
                    resourceRecord = draggedAssignment.resource = targetResource;
                }
            }
            // Dropped directly on resource and allowed to de-nest (cant get here if not allowed, blocked in validation)
            else {
                newParent = toScheduler.eventStore.rootNode;
            }

            if (newParent && newParent !== parent) {
                addedEvents.push(newParent.appendChild(eventRecord));
                // Don't want to add it to root when dragging to another scheduler
                ArrayHelper.remove(eventsToAdd, eventRecord);
            }
        }

        // Moved parent to new resource, reassign all children assigned to its previous resource
        if (eventRecord.isParent && reassignedFrom && reassignedFrom !== resourceRecord) {
            for (const child of eventRecord.allChildren) {
                const existingAssignment = child.assignments.find(a => a.resource === reassignedFrom);
                if (existingAssignment) {
                    existingAssignment.resource = resourceRecord;
                }
            }
        }

        // Add to new parent (or put back in old) matching outer position. If we don't do this element might get released
        // on DomSync of foregroundCanvas (also this lets it transition within the parent)
        if (newParent && !newParent.isRoot) {
            const
                newParentElement = this.client.getElementFromEventRecord(newParent, resourceRecord).syncIdMap.nestedEventsContainer,
                intersection     = newParentElement && Rectangle.from(element, newParentElement);

            // If dropped on a root level leaf it has no nested events container yet
            if (newParentElement) {
                element.style.top = `${intersection.top}px`;
                element.style.left = `${intersection.left}px`;

                // If dropped at the same position in a new parent it won't transition into place if it thinks nothing
                // changed
                element.lastDomConfig = null;

                DomSync.addChild(newParentElement, element, element.dataset.syncId);
            }
        }
    }

    // Assign all children to same resource when dropping on another scheduler
    processCrossSchedulerEventDrop({ eventRecord }) {
        if (eventRecord.isParent) {
            for (const child of eventRecord.allChildren) {
                child.resource = eventRecord.resource;
            }
        }
    }

    // Restore element after abort (back to original parent and position)
    async afterEventDragAbortFinalized({ originalParentElement, originalBounds }) {
        if (this.isDraggingNestedEvent) {
            // Wait for any position transition
            for (const animation of originalBounds[0].element.getAnimations()) {
                if (animation.transitionProperty === 'top' || animation.transitionProperty === 'left') {
                    await animation.finished;
                }
            }

            for (const { element, bounds } of originalBounds) {
                // Move it back
                element.style.top = `${bounds.top}px`;
                element.style.left = `${bounds.left}px`;
                originalParentElement.appendChild(element);
            }
        }
    }

    // Limit resizing to parent bounds if configured to do so (it is the default)
    afterEventResizeStart(context) {
        if (this.constrainResizeToParent) {
            const { parent } = context.timespanRecord;
            if (parent && !parent.isRoot) {
                let { startDate, endDate } = parent;

                if (context.dateConstraints) {
                    startDate = DateHelper.max(startDate, context.dateConstraints.start);
                    endDate = DateHelper.min(endDate, context.dateConstraints.end);
                }

                context.dateConstraints = {
                    start : startDate,
                    end   : endDate
                };
            }
        }
    }

    //endregion

    //region Overrides to make scheduler work with nested events

    // Let Scheduler resolve nested events too
    getElementFromAssignmentRecord(assignmentRecord, returnWrapper) {
        if (assignmentRecord?.event?.parent && !assignmentRecord.event.parent.isRoot) {
            const parentElement = this.client.getElementFromEventRecord(assignmentRecord.event.parent, assignmentRecord.resource);
            return DomSync.getChild(parentElement, `nestedEventsContainer.${assignmentRecord.id}${returnWrapper ? '' : '.event'}`);
        }

        return this.overridden.getElementFromAssignmentRecord(assignmentRecord, returnWrapper);
    }

    // Allow scheduling nested events by overriding Schedulers implementation
    async scheduleEvent({ eventRecord, parentEventRecord, startDate, element }) {
        // When passed a parent, append to it and assign to its resource
        if (parentEventRecord) {
            eventRecord.startDate = startDate;
            eventRecord = parentEventRecord.appendChild(eventRecord);
            eventRecord.assign(parentEventRecord.resource);

            // When given an element, it is positioned inside the parent and adopted by DomSync, letting it transition
            if (element) {
                const
                    parentElement = this.client.getElementFromEventRecord(parentEventRecord).syncIdMap.nestedEventsContainer,
                    eventRect     = Rectangle.from(element, parentElement);

                // Clear translate styles used by DragHelper
                DomHelper.setTranslateXY(element, 0, 0);
                DomHelper.setTopLeft(element, eventRect.y, eventRect.x);

                DomSync.addChild(parentElement, element, eventRecord.assignments[0].id);
            }

            await this.client.project.commitAsync();
        }
        else {
            return this.overridden.scheduleEvent(...arguments);
        }
    }

    //endregion

    //region Rendering

    // Hook into event collection to filter out children, since they will be rendered inside their parents
    getEventsToRender(resourceRecord, eventRecords) {
        if (!this.disabled) {
            // Only keep direct children of the root (?. in case someone tries to use a flat store)
            ArrayHelper.remove(eventRecords, ...eventRecords.filter(eventRecord => eventRecord.isEventModel && !eventRecord.parent.isRoot));
        }

        return eventRecords;
    }

    afterRenderEvent({ renderData }) {
        const
            { eventRecord } = renderData,
            { childLevel }  = eventRecord;

        if (eventRecord.isParent) {
            const
                me = this,
                { resourceRecord, width, height, left, top, wrapperCls } = renderData;

            wrapperCls.add('b-nested-events-parent');

            me.currentParentsHeight = height;
            me.currentParentsWidth = width;

            const
                {
                    currentOrientation,
                    isVertical
                }                      = me.client,
                assignedChildren       = eventRecord.children.filter(e => e.$linkedResources?.includes(resourceRecord)),
                // This call uses the same render path as normal events, applying event layout etc. The layout is then
                // as needed patched up below (to be relative to parent etc)
                layouts                = currentOrientation.layoutEvents(resourceRecord, assignedChildren, false, eventRecord, me.overlappingEventSorter),
                nestedEvents           = [];

            let eventsData;

            if (isVertical) {
                eventsData = [];
                for (const layout of Object.values(layouts)) {
                    eventsData.push(layout.renderData);
                }
            }
            else {
                eventsData = layouts?.eventsData;
            }

            if (eventsData) {
                for (const layout of eventsData) {
                    // Positioned inside parent
                    if (isVertical) {
                        layout.left -= left;
                        layout.top -= top;
                        layout.absoluteTop = layout.top;
                    }
                    else {
                        // Special handling for overlap, it does not use the same render path as other layouts
                        if (me.eventLayout === 'none') {
                            layout.top = 0;
                            layout.height = me.getAppliedResourceHeight(resourceRecord, eventRecord);
                        }
                        // Stack also needs some special handling of height, since it uses fixed event height
                        else if (me.eventLayout === 'stack') {
                            layout.height = me._eventHeights[childLevel];
                        }

                        layout.absoluteLeft = layout.left;
                        layout.left -= (renderData.absoluteLeft ?? left);
                        layout.absoluteTop = layout.top;
                    }

                    const domConfig = currentOrientation.renderEvent(isVertical ? { renderData : layout } : layout, height);
                    domConfig.className['b-nested-event'] = 1;
                    nestedEvents.push(domConfig);
                }
            }

            const containerDomConfig = {
                className : {
                    'b-nested-events-container'                  : 1,
                    [`b-nested-events-layout-${me.eventLayout}`] : 1
                },
                dataset : {
                    taskBarFeature : 'nestedEventsContainer'
                },
                children    : nestedEvents,
                syncOptions : {
                    syncIdField      : 'syncId',
                    releaseThreshold : 0
                }
            };

            // renderData is reused, children are cached. We want ours to be up to date
            const
                { children } = renderData,
                index        = children.findIndex(child => child.dataset.taskBarFeature === 'nestedEventsContainer');
            if (index === -1) {
                children.push(containerDomConfig);
            }
            else {
                children.splice(index, 1, containerDomConfig);
            }
        }

        renderData.elementConfig.dataset.level = childLevel;
    }

    //endregion
}

GridFeatureManager.registerFeature(NestedEvents, false, 'SchedulerPro');
