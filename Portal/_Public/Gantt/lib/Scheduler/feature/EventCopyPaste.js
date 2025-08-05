import AttachToProjectMixin from '../../Scheduler/data/mixin/AttachToProjectMixin.js';
import CopyPasteBase from '../../Grid/feature/base/CopyPasteBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import './ScheduleContext.js';

/**
 * @module Scheduler/feature/EventCopyPaste
 */

/**
 * Allow using [Ctrl/CMD + C/X] and [Ctrl/CMD + V] to copy/cut and paste events.
 *
 * This feature also adds entries to the {@link Scheduler/feature/EventMenu} for copying & cutting (see example below
 * for how to configure) and to the {@link Scheduler/feature/ScheduleMenu} for pasting.
 *
 * You can configure how a newly pasted record is named using {@link #function-generateNewName}.
 *
 * {@inlineexample Scheduler/feature/EventCopyPaste.js}
 *
 * If you want to highlight the paste location when clicking in the schedule, consider enabling the
 * {@link Scheduler/feature/ScheduleContext} feature.
 *
 * <div class="note">When used with Scheduler Pro, pasting will bypass any constraint set on the event to allow the
 * copy to be assigned the targeted date.</div>
 *
 * This feature is **enabled** by default.
 *
 * ## Customize menu items
 *
 * See {@link Scheduler/feature/EventMenu} and {@link Scheduler/feature/ScheduleMenu} for more info on customizing the
 * menu items supplied by the feature. This snippet illustrates the concept:
 *
 * ```javascript
 * // Custom copy text + remove cut option from event menu:
 * const scheduler = new Scheduler({
 *     features : {
 *         eventMenu : {
 *             items : {
 *                 copyEvent : {
 *                     text : 'Copy booking'
 *                 },
 *                 cutEvent  : false
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * ## Keyboard shortcuts
 *
 * The feature has the following default keyboard shortcuts:
 *
 * | Keys       | Action   | Action description                                |
 * |------------|----------|---------------------------------------------------|
 * | `Ctrl`+`C` | *copy*   | Copies selected event(s) into the clipboard.      |
 * | `Ctrl`+`X` | *cut*    | Cuts out selected event(s) into the clipboard.    |
 * | `Ctrl`+`V` | *paste*  | Insert copied or cut event(s) from the clipboard. |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Scheduler/guides/customization/keymap.md).
 *
 * ## Multi assigned events
 *
 * In a Scheduler that uses single assignment, copying and then pasting creates a clone of the event and assigns it
 * to the target resource. Cutting and pasting moves the original event to the target resource.
 *
 * In a Scheduler using multi assignment, the behaviour is slightly more complex. Cutting and pasting reassigns the
 * event to the target, keeping other assignments of the same event intact. The behaviour for copying and pasting is
 * configurable using the {@link #config-copyPasteAction} config. It accepts two values:
 *
 * * `'clone'` - The default, the event is cloned and the clone is assigned to the target resource. Very similar to the
 *   behaviour with single assignment (event count goes up by 1).
 * * `'assign'` - The original event is assigned to the target resource (event count is unaffected).
 *
 * This snippet shows how to reconfigure it:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventCopyPaste : {
 *             copyPasteAction : 'assign'
 *         }
 *     }
 * });
 * ```
 *
 * <div class="note">Copying multiple assignments of the same event will always result in all but the first assignment
 * being removed on paste, since paste targets a single resource and an event can only be assigned to a resource once.
 * </div>
 *
 * ## Native/shared clipboard
 *
 * If you have multiple Schedulers (or other Bryntum products) on the same page, they will share clipboard. This makes
 * it possible to copy and paste between different Scheduler instances. It is also possible to use the native Clipboard
 * API if it is available and if you set {@link #config-useNativeClipboard} to `true`.
 *
 * Regardless of native clipboard availability, copy-pasting "outside" of the current Scheduler instance will convert
 * the copied events to a string. When pasting, the string will then be parsed back into events. In case of usage of the
 * native Clipboard API, this means it is possible to copy and paste events between completely different applications.
 *
 * To configure the fields that is converted and parsed from the copied string value, please see the
 * {@link #config-eventToStringFields} config.
 *
 * @extends Grid/feature/base/CopyPasteBase
 * @classtype eventCopyPaste
 * @feature
 */

export default class EventCopyPaste extends CopyPasteBase.mixin(AttachToProjectMixin) {
    static $name = 'EventCopyPaste';

    static pluginConfig = {
        assign : [
            'copyEvents',
            'pasteEvents'
        ],
        chain : [
            'populateEventMenu',
            'populateScheduleMenu',
            'onEventDataGenerated'
        ]
    };

    static configurable = {
        /**
         * The field to use as the name field when updating the name of copied records
         * @config {String}
         * @default
         */
        nameField : 'name',

        /**
         * How to handle a copy paste operation when the host uses multi assignment. Either:
         *
         * - `'clone'`  - The default, clone the copied event, assigning the clone to the target resource.
         * - `'assign'` - Add an assignment for the existing event to the target resource.
         *
         * For single assignment mode, it always uses the `'clone'` behaviour.
         *
         * @config {'clone'|'assign'}
         * @default
         */
        copyPasteAction : 'clone',

        /**
         * When copying events (or assignments), data will be sent to the clipboard as a tab (`\t`) and new-line (`\n`)
         * separated string with field values for fields present in this config (in specified order). The default
         * included fields are (in this order):
         * * name
         * * startDate
         * * endDate
         * * duration
         * * durationUnit
         * * allDay
         * To override, provide your own array of fields:
         * ```javascript
         * new Scheduler({
         *     features : {
         *         eventCopyPaste : {
         *             eventToStringFields : [
         *                'name',
         *                'startDate',
         *                'endDate',
         *                'percentDone'
         *             ]
         *         }
         *     }
         * });
         * ```
         * <div class="note">Please note that this config is both used for **converting** events to a string value and
         * is also used to **parse** a string value to events.</div>
         * @config {Array<String>}
         */
        eventToStringFields : ['name', 'startDate', 'endDate', 'duration', 'durationUnit', 'allDay']
    };

    construct(scheduler, config) {
        super.construct(scheduler, config);

        scheduler.ion({
            eventClick    : 'onEventClick',
            scheduleClick : 'onScheduleClick',
            projectChange : () => {
                this.clearClipboard();
                this._cellClickedContext = null;
            },
            thisObj : this
        });
    }

    // Used in events to separate events from different features from each other
    entityName = 'event';

    get scheduler() {
        return this.client;
    }

    attachToEventStore(eventStore) {
        super.attachToEventStore(eventStore);
        delete this._eventClickedContext;
    }

    onEventDataGenerated(eventData) {
        const { assignmentRecord } = eventData;

        // No assignmentRecord for resource time ranges, which we want to ignore anyway
        if (assignmentRecord) {
            eventData.cls['b-cut-item'] = assignmentRecord.meta.isCut;
        }
    }

    onEventClick(context) {
        this._cellClickedContext = null;
        this._eventClickedContext = context;
    }

    onScheduleClick(context) {
        this._cellClickedContext = context;
        this._eventClickedContext = null;
    }

    isActionAvailable({ event }) {
        // No action if
        // 1. there is selected text on the page
        // 2. cell editing is active
        // 3. cursor is not in the grid (filter bar etc)
        // 4. focus is on specialrow
        return !this.disabled &&
            globalThis.getSelection().toString().length === 0 &&
            !this.client.features.cellEdit?.isEditing &&
            Boolean(event.target.closest('.b-timeaxissubgrid')) &&
            !this.client.focusedCell?.isSpecialRow;
    }

    async copy() {
        await this.copyEvents();
    }

    async cut() {
        await this.copyEvents(undefined, true);
    }

    async paste() {
        await this.pasteEvents();
    }

    /**
     * Copy events (when using single assignment mode) or assignments (when using multi assignment mode) to clipboard to
     * paste later
     * @fires beforeCopy
     * @fires copy
     * @param {Scheduler.model.EventModel[]|Scheduler.model.AssignmentModel[]} [records] Pass records to copy them,
     * leave out to copying current selection
     * @param {Boolean} [isCut] Copies by default, pass `true` to cut instead
     * @category Edit
     * @on-owner
     */
    async copyEvents(records = this.scheduler.selectedAssignments, isCut = false) {
        const
            me            = this,
            { scheduler } = me;

        // Relay to original if split
        if (scheduler.splitFrom) {
            return scheduler.splitFrom.features.eventCopyPaste.copyEvents(records, isCut);
        }

        if (!records?.length) {
            return;
        }

        let assignmentRecords = records.slice(); // Slice to not lose records if selection changes

        if (records[0].isEventModel) {
            assignmentRecords = records.map(r => r.assignments).flat();
        }

        // Prevent cutting readOnly events
        if (isCut) {
            assignmentRecords = assignmentRecords.filter(a => !a.event.readOnly);
        }

        const eventRecords = assignmentRecords.map(a => a.event);

        if (!assignmentRecords.length || scheduler.readOnly) {
            return;
        }

        await me.writeToClipboard({ assignmentRecords, eventRecords }, isCut);

        /**
         * Fires on the owning Scheduler after a copy action is performed.
         * @event copy
         * @on-owner
         * @param {Scheduler.view.Scheduler} source Owner scheduler
         * @param {Scheduler.model.EventModel[]} eventRecords The event records that were copied
         * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignment records that were copied
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'event' to distinguish this event from other copy events
         */
        scheduler.trigger('copy', { assignmentRecords, eventRecords, isCut, entityName : me.entityName });

        // refresh to call onEventDataGenerated and reapply the cls for records where the cut was canceled
        scheduler.refreshWithTransition();

        me._focusedEventOnCopy = me._eventClickedContext;
    }

    async beforeCopy({ data : { assignmentRecords, eventRecords }, isCut }) {
        /**
         * Fires on the owning Scheduler before a copy action is performed, return `false` to prevent the action
         * @event beforeCopy
         * @preventable
         * @on-owner
         * @async
         * @param {Scheduler.view.Scheduler} source Owner scheduler
         * @param {Scheduler.model.EventModel[]} eventRecords The event records about to be copied
         * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignment records about to be copied
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'event' to distinguish this event from other beforeCopy events
         */
        return await this.scheduler.trigger('beforeCopy',
            { assignmentRecords,  eventRecords, isCut, entityName : this.entityName });
    }

    // Called from Clipboardable when cutData changes
    handleCutData({ source }) {
        const me = this;

        if (source !== me && me.cutData?.length) {
            const { assignmentRecords, eventRecords } = me.cutData[0];

            if (assignmentRecords?.length) {
                me.scheduler.assignmentStore.remove(assignmentRecords);
            }
            if (eventRecords?.length) {
                me.scheduler.eventStore.remove(eventRecords);
            }
        }
    }

    /**
     * Called from Clipboardable after writing a non-string value to the clipboard
     * @param eventRecords
     * @returns {string}
     * @private
     */
    stringConverter({ eventRecords }) {
        const rows = [];

        for (const event of eventRecords) {
            rows.push(this.eventToStringFields.map(field => {
                const value = event[field];

                if (value instanceof Date) {
                    return DateHelper.format(value, this.dateFormat);
                }

                return value;
            }).join('\t'));
        }

        return rows.join('\n');
    }

    // Called from Clipboardable for each cut out record
    setIsCut({ assignmentRecords }, isCut) {
        assignmentRecords.forEach(assignment => {
            assignment.meta.isCut = isCut;
        });
        // refresh to call onEventDataGenerated and reapply the cls for records where the cut was canceled
        this.scheduler.refreshWithTransition();
    }

    /**
     * Paste events or assignments to specified date and resource
     * @fires beforePaste
     * @fires paste
     * @param {Date} [date] Date where the events or assignments will be pasted
     * @param {Scheduler.model.ResourceModel} [resourceRecord] Resource to assign the pasted events or assignments to
     * @category Edit
     * @on-owner
     */
    async pasteEvents(date, resourceRecord) {
        const
            me            = this,
            { scheduler } = me;

        // Relay to original if split
        if (scheduler.splitFrom) {
            return scheduler.splitFrom.features.eventCopyPaste.pasteEvents(date, resourceRecord);
        }

        const
            {
                entityName,
                isCut,
                _cellClickedContext,
                _eventClickedContext
            }  = me,
            {
                eventStore,
                assignmentStore
            }  = scheduler;

        if (arguments.length === 0) {
            if (_cellClickedContext) {
                date           = _cellClickedContext.date;
                resourceRecord = _cellClickedContext.resourceRecord;
            }
            else if (me._focusedEventOnCopy !== _eventClickedContext) {
                date           = _eventClickedContext.eventRecord.startDate;
                resourceRecord = _eventClickedContext.resourceRecord;
            }

        }

        if (resourceRecord) {
            resourceRecord = resourceRecord.$original;
        }

        const clipboardData = await me.readFromClipboard({ resourceRecord, date });

        if (!clipboardData?.assignmentRecords?.length) {
            return;
        }

        const
            {
                assignmentRecords,
                eventRecords
            }            = clipboardData;
        let toFocus      = null;

        const
            pastedEvents = new Set(),
            pastedEventRecords = [];

        for (const assignmentRecord of assignmentRecords) {
            let { event }            = assignmentRecord;
            const
                targetResourceRecord = resourceRecord || assignmentRecord.resource,
                targetDate           = date || assignmentRecord.event.startDate;

            // Pasting targets a specific resource, we cannot have multiple assignments to the same so remove all but
            // the first (happens when pasting multiple assignments of the same event)
            if (pastedEvents.has(event)) {
                if (isCut) {
                    assignmentRecord.remove();
                }
                continue;
            }

            pastedEvents.add(event);

            // Cut always means reassign
            if (isCut) {
                assignmentRecord.meta.isCut = false;
                assignmentRecord.resource   = targetResourceRecord;
                toFocus                     = assignmentRecord;
            }
            // Copy creates a new event in single assignment, or when configured to copy
            else if (eventStore.usesSingleAssignment || me.copyPasteAction === 'clone') {
                event      = event.copy();
                event.name = me.generateNewName(event);
                eventStore.add(event);
                event.assign(targetResourceRecord);
                toFocus = assignmentStore.last;
            }
            // Safeguard against pasting on a resource where the event is already assigned,
            // a new assignment in multiassign mode will only change the date in such case
            else if (!event.resources.includes(targetResourceRecord)) {
                const newAssignmentRecord    = assignmentRecord.copy();
                newAssignmentRecord.resource = targetResourceRecord;
                [toFocus]                    = assignmentStore.add(newAssignmentRecord);
            }

            event.startDate = targetDate;

            // Pro specific, to allow event to appear where pasted
            if (event.constraintDate) {
                event.constraintDate = null;
            }

            pastedEventRecords.push(event);
        }

        /**
         * Fires on the owning Scheduler after a paste action is performed.
         * @event paste
         * @on-owner
         * @param {Scheduler.view.Scheduler} source Owner scheduler
         * @param {Scheduler.model.EventModel[]} eventRecords Original events
         * @param {Scheduler.model.EventModel[]} pastedEventRecords Pasted events
         * @param {Scheduler.model.AssignmentModel[]} assignmentRecords Pasted assignments
         * @param {Date} date date Pasted to this date
         * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'event' to distinguish this event from other paste events
         */
        scheduler.trigger('paste', { assignmentRecords, pastedEventRecords, eventRecords, resourceRecord, date, isCut, entityName });

        // Focus the last pasted assignment
        const detacher = scheduler.ion({
            renderEvent({ assignmentRecord }) {
                if (assignmentRecord === toFocus) {
                    scheduler.navigateTo(assignmentRecord, { scrollIntoView : false });
                    detacher();
                }
            }
        });

        if (isCut) {
            await me.clearClipboard();
        }
    }

    // Called from Clipboardable before finishing the internal clipboard read
    async beforePaste({ data : { assignmentRecords, eventRecords }, resourceRecord, isCut, date }) {
        const
            { scheduler } = this,
            eventData     = {
                assignmentRecords,
                eventRecords,
                resourceRecord : resourceRecord || assignmentRecords[0].resource,
                date,
                isCut,
                entityName     : this.entityName
            };
        let reason;

        // No pasting to readOnly resources
        if (resourceRecord?.readOnly) {
            reason = 'resourceReadOnly';
        }

        if (!scheduler.allowOverlap) {
            const pasteWouldResultInOverlap = assignmentRecords.some(assignmentRecord => !scheduler.isDateRangeAvailable(
                assignmentRecord.event.startDate,
                assignmentRecord.event.endDate,
                isCut ? assignmentRecord.event : null,
                assignmentRecord.resource)
            );

            if (pasteWouldResultInOverlap) {
                reason = 'overlappingEvents';
            }
        }

        /**
         * Fires on the owning Scheduler if a paste action is not allowed
         * @event pasteNotAllowed
         * @on-owner
         * @param {Scheduler.view.Scheduler} source Owner scheduler
         * @param {Scheduler.model.EventModel[]} eventRecords
         * @param {Scheduler.model.AssignmentModel[]} assignmentRecords
         * @param {Date} date The paste date
         * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'event' to distinguish this event from other `pasteNotAllowed` events
         * @param {'overlappingEvents'|'resourceReadOnly'} reason A string id to use for displaying an error message to the user.
         */
        if (reason) {
            scheduler.trigger('pasteNotAllowed', {
                ...eventData,
                reason
            });
            return false;
        }

        /**
         * Fires on the owning Scheduler before a paste action is performed, return `false` to prevent the action
         * @event beforePaste
         * @preventable
         * @on-owner
         * @async
         * @param {Scheduler.view.Scheduler} source Owner scheduler
         * @param {Scheduler.model.EventModel[]} eventRecords The events about to be pasted
         * @param {Scheduler.model.AssignmentModel[]} assignmentRecords The assignments about to be pasted
         * @param {Date} date The date when the pasted events will be scheduled
         * @param {Scheduler.model.ResourceModel} resourceRecord The target resource record, the clipboard
         * event records will be assigned to this resource.
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'event' to distinguish this event from other beforePaste events
         */
        return await this.scheduler.trigger('beforePaste', eventData);
    }

    /**
     * Called from Clipboardable after reading from clipboard, and it is determined that the clipboard data is
     * "external"
     * @param json
     * @returns {Object}
     * @private
     */
    stringParser(clipboardData) {
        const
            { eventStore, assignmentStore }    = this.scheduler,
            { modifiedRecords : eventRecords } = this.setFromStringData(clipboardData, true, eventStore, this.eventToStringFields),
            assignmentRecords                  = [];

        for (const event of eventRecords) {
            const assignment = new assignmentStore.modelClass({ eventId : event.id });
            assignment.event = event;
            assignmentRecords.push(assignment);
        }
        return { eventRecords, assignmentRecords };
    }

    populateEventMenu({ assignmentRecord, items }) {
        const
            me            = this,
            { scheduler } = me;

        if (!scheduler.readOnly) {
            items.copyEvent = {
                text        : 'L{copyEvent}',
                localeClass : me,
                icon        : 'b-icon b-icon-copy',
                weight      : 110,
                onItem      : () => {
                    const assignments = scheduler.isAssignmentSelected(assignmentRecord) ? scheduler.selectedAssignments : [assignmentRecord];

                    me.copyEvents(assignments);
                }
            };

            items.cutEvent = {
                text        : 'L{cutEvent}',
                localeClass : me,
                icon        : 'b-icon b-icon-cut',
                weight      : 120,
                disabled    : assignmentRecord.event.readOnly,
                onItem      : () => {
                    const assignments = scheduler.isAssignmentSelected(assignmentRecord) ? scheduler.selectedAssignments : [assignmentRecord];
                    me.copyEvents(assignments, true);
                }
            };
        }
    }

    populateScheduleMenu({ items, resourceRecord }) {
        const
            me            = this,
            { scheduler } = me;

        if (!scheduler.readOnly && me.hasClipboardData() !== false) {
            items.pasteEvent = {
                text        : 'L{pasteEvent}',
                localeClass : me,
                icon        : 'b-icon b-icon-paste',
                disabled    : scheduler.resourceStore.count === 0 || resourceRecord.readOnly,
                weight      : 110,
                onItem      : ({
                    date, resourceRecord
                }) => me.pasteEvents(date, resourceRecord, scheduler.getRowFor(resourceRecord))
            };
        }
    }

    /**
     * A method used to generate the name for a copy pasted record. By defaults appends "- 2", "- 3" as a suffix.
     *
     * @param {Scheduler.model.EventModel} eventRecord The new eventRecord being pasted
     * @returns {String}
     */
    generateNewName(eventRecord) {
        const originalName = eventRecord.getValue(this.nameField);
        let counter = 2;

        while (this.client.eventStore.findRecord(this.nameField, `${originalName} - ${counter}`)) {
            counter++;
        }

        return `${originalName} - ${counter}`;
    }
}

EventCopyPaste.featureClass = 'b-event-copypaste';

GridFeatureManager.registerFeature(EventCopyPaste, true, 'Scheduler');
