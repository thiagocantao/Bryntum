import EventResize from '../EventResize.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import EventHelper from '../../../Core/helper/EventHelper.js';
import Draggable from '../../../Core/mixin/Draggable.js';
import TaskEditStm from '../mixin/TaskEditStm.js';
import TaskEditTransactional from '../mixin/TaskEditTransactional.js';
import TransactionalFeature from '../mixin/TransactionalFeature.js';

/**
 * @module Scheduler/feature/base/DragCreateBase
 */
const getDragCreateDragDistance = function(event) {
    // Do not allow the drag to begin if the taskEdit feature (if present) is in the process
    // of canceling. We must wait for it to have cleaned up its data manipulations before
    // we can add the new, drag-created record
    if (this.source?.client.features.taskEdit?._canceling) {
        return false;
    }
    return EventHelper.getDistanceBetween(this.startEvent, event);
};

/**
 * Base class for EventDragCreate (Scheduler) and TaskDragCreate (Gantt) features. Contains shared code. Not to be used directly.
 *
 * @extends Scheduler/feature/EventResize
 */
export default class DragCreateBase extends EventResize.mixin(
    TaskEditStm,
    TransactionalFeature,
    TaskEditTransactional
) {
    //region Config

    static configurable = {
        /**
         * true to show a time tooltip when dragging to create a new event
         * @config {Boolean}
         * @default
         */
        showTooltip : true,

        /**
         * Number of pixels the drag target must be moved before dragging is considered to have started. Defaults to 2.
         * @config {Number}
         * @default
         */
        dragTolerance : 2,

        // used by gantt to only allow one task per row
        preventMultiple : false,

        dragTouchStartDelay : 300,

        /**
         * `this` reference for the validatorFn
         * @config {Object}
         */
        validatorFnThisObj : null,

        tipTemplate : data => `
            <div class="b-sch-tip-${data.valid ? 'valid' : 'invalid'}">
                ${data.startClockHtml}
                ${data.endClockHtml}
                <div class="b-sch-tip-message">${data.message}</div>
            </div>
        `,

        dragActiveCls : 'b-dragcreating'
    };

    static pluginConfig = {
        chain  : ['render', 'onEventDataGenerated'],
        before : ['onElementContextMenu']
    };

    construct(scheduler, config) {
        if (config?.showTooltip === false) {
            config.tip = null;
        }
        super.construct(...arguments);
    }

    //endregion

    changeValidatorFn(validatorFn) {
        // validatorFn property is used by the EventResize base to validate each mousemove
        // We change the property name to createValidatorFn
        this.createValidatorFn = validatorFn;
    }

    render() {
        const
            me         = this,
            { client } = me;

        // Set up elements and listeners
        me.dragRootElement = me.dropRootElement = client.timeAxisSubGridElement;

        // Drag only in time dimension
        me.dragLock = client.isVertical ? 'y' : 'x';
    }

    onDragEndSwitch(context) {
        const
            { client }                = this,
            { enableEventAnimations } = client,
            {
                eventRecord,
                draggingEnd
            }                         = context,
            horizontal                = this.dragLock === 'x',
            { initialDate }           = this.dragging;

        // Setting the new opposite end should not animate
        client.enableEventAnimations = false;

        // Zero duration at the moment of the flip
        eventRecord.set({
            startDate : initialDate,
            endDate   : initialDate
        });

        // We're switching to dragging the start
        if (draggingEnd) {
            Object.assign(context, {
                endDate        : initialDate,
                toSet          : 'startDate',
                otherEnd       : 'endDate',
                setMethod      : 'setStartDate',
                setOtherMethod : 'setEndDate',
                edge           : horizontal ? 'left' : 'top'
            });
        }
        else {
            Object.assign(context, {
                startDate      : initialDate,
                toSet          : 'endDate',
                otherEnd       : 'startDate',
                setMethod      : 'setEndDate',
                setOtherMethod : 'setStartDate',
                edge           : horizontal ? 'right' : 'bottom'
            });
        }

        context.draggingEnd = this.draggingEnd = !draggingEnd;
        client.enableEventAnimations = enableEventAnimations;
    }

    beforeDrag(drag) {
        const
            me                       = this,
            result                   = super.beforeDrag(drag),
            { pan, eventDragSelect } = me.client.features;

        // Superclass's handler may also veto
        if (result !== false && (
            // used by gantt to only allow one task per row
            (me.preventMultiple && !me.isRowEmpty(drag.rowRecord)) ||
            me.disabled ||
            // If Pan is enabled, it has right of way
            (pan && !pan.disabled) ||
            // If EventDragSelect is enabled, it has right of way
            (eventDragSelect && !eventDragSelect.disabled)
        )) {
            return false;
        }

        // Prevent drag select if drag-creating, could collide otherwise
        // (reset by GridSelection)
        me.client.preventDragSelect = true;

        return result;
    }

    startDrag(drag) {
        const result = super.startDrag(drag);

        // Returning false means operation is aborted.
        if (result !== false) {
            const { context } = drag;

            // Date to flip around when changing direction
            drag.initialDate = context.eventRecord.get(this.draggingEnd ? 'startDate' : 'endDate');

            this.client.trigger('dragCreateStart', {
                proxyElement   : drag.element,
                eventElement   : drag.element,
                eventRecord    : context.eventRecord,
                resourceRecord : context.resourceRecord
            });

            // We are always dragging the exact edge of the event element.
            drag.context.offset   = 0;
            drag.context.oldValue = drag.mousedownDate;
        }
        return result;
    }

    // Used by our EventResize superclass to know whether the drag point is the end or the beginning.
    isOverEndHandle() {
        return this.draggingEnd;
    }

    setupDragContext(event) {
        const { client } = this;

        // Only mousedown on an empty cell can initiate drag-create
        if (client.matchScheduleCell(event.target)) {
            const resourceRecord = client.resolveResourceRecord(event)?.$original;

            // And there must be a resource backing the cell.
            if (resourceRecord && !resourceRecord.isSpecialRow) {
                // Skip the EventResize's setupDragContext. We want the base one.
                const
                    result      = Draggable().prototype.setupDragContext.call(this, event),
                    scrollables = [];

                if (client.isVertical) {
                    scrollables.push({
                        element   : client.scrollable.element,
                        direction : 'vertical'
                    });
                }
                else {
                    scrollables.push({
                        element   : client.timeAxisSubGrid.scrollable.element,
                        direction : 'horizontal'
                    });
                }

                result.scrollManager = client.scrollManager;
                result.monitoringConfig = { scrollables };
                result.resourceRecord = result.rowRecord = resourceRecord;

                // We use a special method to get the distance moved.
                // If the TaskEdit feature is still in its canceling phase, then
                // it returns false which inhibits the start of the drag-create
                // until the cancelation is complete.
                result.getDistance = getDragCreateDragDistance;
                return result;
            }
        }
    }

    async dragDrop({ context, event }) {
        // Set the start/end date, whichever we were dragging
        // to the correctly rounded value before updating.
        context[context.toSet] = context.snappedDate;

        const
            {
                client
            } = this,
            {
                startDate,
                endDate,
                eventRecord
            } = context,
            { generation } = eventRecord;

        let modified;

        this.tip?.hide();

        // Handle https://github.com/bryntum/support/issues/3210.
        // The issue arises when the mouseup arrives very quickly and the commit kicked off
        // at event add has not yet completed. If it now completes *after* we finalize
        // the drag, it will reset the event to its initial state.
        // If that commit has in fact finished, this will be a no-op
        await client.project.commitAsync();

        // If the above commit in fact reset the event back to the initial state, we have to
        // force the event rendering to bring it back to the currently known context state.
        if (eventRecord.generation !== generation) {
            context.eventRecord[context.toSet] = context.oldValue;
            context.eventRecord[context.toSet] = context[context.toSet];
        }

        context.valid = startDate && endDate && (endDate - startDate > 0) && // Input sanity check
            (context[context.toSet] - context.oldValue) && // Make sure dragged end changed
            context.valid !== false;

        if (context.valid) {
            // Seems to be a valid drag-create operation, ask outside world if anyone wants to take control over the finalizing,
            // to show a confirm dialog prior to finalizing the create.
            client.trigger('beforeDragCreateFinalize', {
                context,
                event,
                proxyElement   : context.element,
                eventElement   : context.element,
                eventRecord    : context.eventRecord,
                resourceRecord : context.resourceRecord
            });
            modified = true;
        }

        // If a handler has set the async flag, it means that they are going to finalize
        // the operation at some time in the future, so we should not call it.
        if (!context.async) {
            await context.finalize(modified);
        }
    }

    updateDragTolerance(dragTolerance) {
        this.dragThreshold = dragTolerance;
    }

    //region Tooltip

    changeTip(tip, oldTip) {
        return super.changeTip(!tip || tip.isTooltip ? tip : ObjectHelper.assign({
            id : `${this.client.id}-drag-create-tip`
        }, tip), oldTip);
    }

    //endregion

    //region Finalize (create EventModel)

    // this method is actually called on the `context` object,
    // so `this` object inside might not be what you think (see `me = this.owner` below)
    // not clear what was the motivation for such design
    async finalize(doCreate) {
        // only call this method once, do not re-enter
        if (this.finalized) {
            return;
        }

        this.finalized = true;

        const
            me                = this.owner,
            context           = this,
            completeFinalization = () => {
                if (!me.isDestroyed) {
                    me.client.trigger('afterDragCreate', {
                        proxyElement   : context.element,
                        eventElement   : context.element,
                        eventRecord    : context.eventRecord,
                        resourceRecord : context.resourceRecord
                    });
                    me.cleanup(context);
                }
            };

        if (doCreate) {
            // Call product specific implementation
            await me.finalizeDragCreate(context);

            completeFinalization();
        }
        // Aborting without going ahead with create - we must deassign and remove the event
        else {
            await me.cancelDragCreate(context);

            me.onAborted?.(context);
            completeFinalization();
        }
    }

    async cancelDragCreate(context) {
    }

    async finalizeDragCreate(context) {
        // EventResize base class applies final changes to the event record
        await this.internalUpdateRecord(context, context.eventRecord);

        const stmCapture = this.getStmCapture();

        this.client?.trigger('dragCreateEnd', {
            eventRecord    : context.eventRecord,
            resourceRecord : context.resourceRecord,
            event          : context.event,
            eventElement   : context.element,
            stmCapture
        });

        // Part of the Scheduler API. Triggered by its createEvent method.
        // Auto-editing features can use this to edit new events.
        // Note that this may be destroyed by a listener of the previous event.
        this.client?.trigger('eventAutoCreated', {
            eventRecord    : context.eventRecord,
            resourceRecord : context.resourceRecord
        });

        return stmCapture.transferred;
    }

    cleanup(context) {
        const
            { client }      = this,
            { eventRecord } = context;

        // Base class's cleanup is not called, we have to clear this flag.
        // The isCreating flag is only set if the event is to be handed off to the
        // eventEdit feature and that feature then has responsibility for clearing it.
        eventRecord.meta.isResizing = false;

        client.endListeningForBatchedUpdates();
        this.tip?.hide();
        client.element.classList.remove(...this.dragActiveCls.split(' '));

        context.element.parentElement.classList.remove('b-sch-dragcreating');
    }

    //endregion

    //region Events

    /**
     * Prevent right click when drag creating
     * @returns {Boolean}
     * @private
     */
    onElementContextMenu() {
        if (this.proxy) {
            return false;
        }
    }

    prepareCreateContextForFinalization(createContext, event, finalize, async = false) {
        return {
            ...createContext,
            async,
            event,
            finalize
        };
    }

    // Apply drag create "proxy" styling
    onEventDataGenerated(renderData) {
        if (this.dragging?.context?.eventRecord === renderData.eventRecord) {
            // Allow custom styling for drag creation element
            renderData.wrapperCls['b-sch-dragcreating'] = true;
            // Styling when drag create will be aborted on drop (because it would yield zero duration)
            renderData.wrapperCls['b-too-narrow'] = this.dragging.context.tooNarrow;
        }
    }

    //endregion

    //region Product specific, implemented in subclasses

    // Empty implementation here. Only base EventResize class triggers this
    triggerBeforeResize() {}

    // Empty implementation here. Only base EventResize class triggers this
    triggerEventResizeStart() {}

    checkValidity(context, event) {
        throw new Error('Implement in subclass');
    }

    handleBeforeDragCreate(dateTime, event) {
        throw new Error('Implement in subclass');
    }

    isRowEmpty(rowRecord) {
        throw new Error('Implement in subclass');
    }

    //endregion
}
