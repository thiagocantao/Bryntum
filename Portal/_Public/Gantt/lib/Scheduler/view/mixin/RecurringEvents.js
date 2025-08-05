import Base from '../../../Core/Base.js';
import '../recurrence/RecurrenceConfirmationPopup.js';

/**
 * @module Scheduler/view/mixin/RecurringEvents
 */

/**
 * A mixin that adds recurring events functionality to the Scheduler.
 *
 * The main purpose of the code in here is displaying a {@link Scheduler.view.recurrence.RecurrenceConfirmationPopup special confirmation}
 * on user mouse dragging/resizing/deleting recurring events and their occurrences.
 *
 * @mixin
 */
export default Target => class RecurringEvents extends (Target || Base) {
    static $name = 'RecurringEvents';

    static configurable = {
        /**
         * Enables showing occurrences of recurring events across the scheduler's time axis.
         *
         * Enables extra recurrence UI fields in the system-provided event editor (not in Scheduler Pro's task editor).
         * @config {Boolean}
         * @default
         * @category Scheduled events
         */
        enableRecurringEvents : false,

        recurrenceConfirmationPopup : {
            $config : ['lazy'],
            value   : {
                type : 'recurrenceconfirmation'
            }
        }
    };

    construct(config) {
        super.construct(config);

        this.ion({
            beforeEventDropFinalize   : 'onRecurrableBeforeEventDropFinalize',
            beforeEventResizeFinalize : 'onRecurrableBeforeEventResizeFinalize',
            beforeAssignmentDelete    : 'onRecurrableAssignmentBeforeDelete'
        });
    }

    changeRecurrenceConfirmationPopup(recurrenceConfirmationPopup, oldRecurrenceConfirmationPopup) {
        // Widget.reconfigure reither reconfigures an existing instance, or creates a new one, or,
        // if the configuration is null, destroys the existing instance.
        const result = this.constructor.reconfigure(oldRecurrenceConfirmationPopup, recurrenceConfirmationPopup, 'recurrenceconfirmation');
        result.owner = this;
        return result;
    }

    findRecurringEventToConfirmDelete(eventRecords) {
        // show confirmation if we deal with at least one recurring event (or its occurrence)
        // and if the record is not being edited by event editor (since event editor has its own confirmation)
        return eventRecords.find(eventRecord => eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence));
    }

    onRecurrableAssignmentBeforeDelete({ assignmentRecords, context }) {
        const
            eventRecords = assignmentRecords.map(as => as.event),
            eventRecord  = this.findRecurringEventToConfirmDelete(eventRecords);

        if (this.enableRecurringEvents && eventRecord) {
            this.recurrenceConfirmationPopup.confirm({
                actionType : 'delete',
                eventRecord,
                changerFn() {
                    context.finalize(true);
                },
                cancelFn() {
                    context.finalize(false);
                }
            });

            return false;
        }
    }

    onRecurrableBeforeEventDropFinalize({ context }) {
        if (this.enableRecurringEvents) {
            const
                { eventRecords } = context,
                recurringEvents = eventRecords.filter(eventRecord => eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence));

            if (recurringEvents.length) {
                context.async = true;

                this.recurrenceConfirmationPopup.confirm({
                    actionType  : 'update',
                    eventRecord : recurringEvents[0],
                    changerFn() {
                        context.finalize(true);
                    },
                    cancelFn() {
                        context.finalize(false);
                    }
                });
            }
        }
    }

    onRecurrableBeforeEventResizeFinalize({ context }) {
        if (this.enableRecurringEvents) {
            const
                { eventRecord } = context,
                isRecurring     = eventRecord.supportsRecurring && (eventRecord.isRecurring || eventRecord.isOccurrence);

            if (isRecurring) {
                context.async = true;

                this.recurrenceConfirmationPopup.confirm({
                    actionType : 'update',
                    eventRecord,
                    changerFn() {
                        context.finalize(true);
                    },
                    cancelFn() {
                        context.finalize(false);
                    }
                });
            }
        }
    }

    // Make sure occurrence cache is up-to-date when reassigning events
    onAssignmentChange({ action, records : assignments }) {
        if (action !== 'dataset' && Array.isArray(assignments)) {
            for (const assignment of assignments) {
                if (assignment.event?.isRecurring && !assignment.event.isBatchUpdating) {
                    assignment.event.removeOccurrences();
                }
            }
        }
    }

    /**
     * Returns occurrences of the provided recurring event across the date range of this Scheduler.
     * @param  {Scheduler.model.TimeSpan} recurringEvent Recurring event for which occurrences should be retrieved.
     * @returns {Scheduler.model.TimeSpan[]} Array of the provided timespans occurrences.
     *
     * __Empty if the passed event is not recurring, or has no occurrences in the date range.__
     *
     * __If the date range encompasses the start point, the recurring event itself will be the first entry.__
     * @category Data
     */
    getOccurrencesFor(recurringEvent) {
        return this.eventStore.getOccurrencesForTimeSpan(recurringEvent, this.timeAxis.startDate, this.timeAxis.endDate);
    }

    /**
     * Internal utility function to remove events. Used when pressing [DELETE] or [BACKSPACE] or when clicking the
     * delete button in the event editor. Triggers a preventable `beforeEventDelete` or `beforeAssignmentDelete` event.
     * @param {Scheduler.model.EventModel[]|Scheduler.model.AssignmentModel[]} eventRecords Records to remove
     * @param {Function} [callback] Optional callback executed after triggering the event but before deletion
     * @returns {Boolean} Returns `false` if the operation was prevented, otherwise `true`
     * @internal
     * @fires beforeEventDelete
     * @fires beforeAssignmentDelete
     */
    async removeEvents(eventRecords, callback = null, popupOwner = this) {
        const me = this;

        if (!me.readOnly && eventRecords.length) {
            const context = {
                finalize(removeRecord = true) {
                    if (callback) {
                        callback(removeRecord);
                    }

                    if (removeRecord !== false) {
                        if (eventRecords.some(record => record.isOccurrence || record.event?.isOccurrence)) {
                            eventRecords.forEach(record => record.isOccurrenceAssignment ? record.event.remove() : record.remove());
                        }
                        else {
                            const store = eventRecords[0].isAssignment ? me.assignmentStore : me.eventStore;

                            store.remove(eventRecords);
                        }
                    }
                }
            };

            let shouldFinalize;

            if (eventRecords[0].isAssignment) {
                /**
                 * Fires before an assignment is removed. Can be triggered by user pressing [DELETE] or [BACKSPACE] or
                 * by the event editor. Can for example be used to display a custom dialog to confirm deletion, in which
                 * case records should be "manually" removed after confirmation:
                 *
                 * ```javascript
                 * scheduler.on({
                 *    beforeAssignmentDelete({ assignmentRecords, context }) {
                 *        // Show custom confirmation dialog (pseudo code)
                 *        confirm.show({
                 *            listeners : {
                 *                onOk() {
                 *                    // Remove the assignments on confirmation
                 *                    context.finalize(true);
                 *                },
                 *                onCancel() {
                 *                    // do not remove the assignments if "Cancel" clicked
                 *                    context.finalize(false);
                 *                }
                 *            }
                 *        });
                 *
                 *        // Prevent default behaviour
                 *        return false;
                 *    }
                 * });
                 * ```
                 *
                 * @event beforeAssignmentDelete
                 * @param {Scheduler.view.Scheduler} source  The Scheduler instance
                 * @param {Scheduler.model.EventModel[]} eventRecords  The records about to be deleted
                 * @param {Object} context  Additional removal context:
                 * @param {Function} context.finalize  Function to call to finalize the removal.
                 *      Used to asynchronously decide to remove the records or not. Provide `false` to the function to
                 *      prevent the removal.
                 * @param {Boolean} [context.finalize.removeRecords = true]   Provide `false` to the function to prevent
                 *      the removal.
                 * @preventable
                 */
                shouldFinalize = me.trigger('beforeAssignmentDelete', { assignmentRecords : eventRecords, context });
            }
            else {
                /**
                 * Fires before an event is removed. Can be triggered by user pressing [DELETE] or [BACKSPACE] or by the
                 * event editor. Return `false` to immediately veto the removal (or a `Promise` yielding `true` or `false`
                 * for async vetoing).
                 *
                 * Can for example be used to display a custom dialog to confirm deletion, in which case
                 * records should be "manually" removed after confirmation:
                 *
                 * ```javascript
                 * scheduler.on({
                 *    beforeEventDelete({ eventRecords, context }) {
                 *        // Show custom confirmation dialog (pseudo code)
                 *        confirm.show({
                 *            listeners : {
                 *                onOk() {
                 *                    // Remove the events on confirmation
                 *                    context.finalize(true);
                 *                },
                 *                onCancel() {
                 *                    // do not remove the events if "Cancel" clicked
                 *                    context.finalize(false);
                 *                }
                 *            }
                 *        });
                 *
                 *        // Prevent default behaviour
                 *        return false;
                 *    }
                 * });
                 * ```
                 *
                 * @event beforeEventDelete
                 * @param {Scheduler.view.Scheduler} source  The Scheduler instance
                 * @param {Scheduler.model.EventModel[]} eventRecords  The records about to be deleted
                 * @param {Object} context  Additional removal context:
                 * @param {Function} context.finalize  Function to call to finalize the removal.
                 *      Used to asynchronously decide to remove the records or not. Provide `false` to the function to
                 *      prevent the removal.
                 * @param {Boolean} [context.finalize.removeRecords = true]  Provide `false` to the function to prevent
                 *      the removal.
                 * @preventable
                 * @async
                 */
                shouldFinalize = await me.trigger('beforeEventDelete', { eventRecords, context });
            }

            if (shouldFinalize !== false) {
                const recurringEventRecord = eventRecords.find(eventRecord => eventRecord.isRecurring || eventRecord.isOccurrence);

                if (recurringEventRecord) {
                    me.recurrenceConfirmationPopup.owner = popupOwner;
                    me.recurrenceConfirmationPopup.confirm({
                        actionType  : 'delete',
                        eventRecord : recurringEventRecord,
                        changerFn() {
                            context.finalize(true);
                        },
                        cancelFn() {
                            context.finalize(false);
                        }
                    });
                }
                else {
                    context.finalize(true);
                }
                return true;
            }
        }

        return false;
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
