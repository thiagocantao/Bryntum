import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import Editor from '../../Core/widget/Editor.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import TaskEditStm from './mixin/TaskEditStm.js';

/**
 * @module Scheduler/feature/SimpleEventEdit
 */

/**
 * Feature that displays a text field to edit the event name. You can control the flow of this by listening to the events relayed by this class from the underlying {@link Core.widget.Editor}.
 * To use this feature, you also need to disable the built-in default editing feature:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventEdit       : false,
 *         simpleEventEdit : true
 *     }
 * });
 * ```
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/simpleeditor
 * @inlineexample Scheduler/feature/SimpleEventEdit.js
 * @classtype simpleEventEdit
 * @feature
 */
export default class SimpleEventEdit extends InstancePlugin.mixin(TaskEditStm) {

    // region Events
    /**
     * Fired before the editor is shown to start an edit operation. Returning `false` from a handler vetoes the edit operation.
     * @event beforeStart
     * @param {Object} value The value to be edited.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     * @preventable
     */
    /**
     * Fired when an edit operation has begun.
     * @event start
     * @param {Object} value The starting value of the field.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     */
    /**
     * Fired when an edit completion has been requested, either by `ENTER`, or focus loss (if configured to complete on blur).
     * The completion may be vetoed, in which case, focus is moved back into the editor.
     * @event beforeComplete
     * @param {Object} oldValue The original value.
     * @param {Object} value The new value.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     * @preventable
     */
    /**
     * Edit has been completed, and any associated record or element has been updated.
     * @event complete
     * @param {Object} oldValue The original value.
     * @param {Object} value The new value.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     */

    /**
     * Fired when cancellation has been requested, either by `ESC`, or focus loss (if configured to cancel on blur).
     * The cancellation may be vetoed, in which case, focus is moved back into the editor.
     * @event beforeCancel
     * @param {Object} oldValue The original value.
     * @param {Object} value The new value.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     * @preventable
     */
    /**
     * Edit has been canceled without updating the associated record or element.
     * @event cancel
     * @param {Object} oldValue The original value.
     * @param {Object} value The value of the field.
     * @param {Core.widget.Editor} source The Editor that triggered the event.
     */
    // endregion

    //region Config

    static get $name() {
        return 'SimpleEventEdit';
    }

    static get defaultConfig() {
        return {
            /**
             * The event that shall trigger showing the editor. Defaults to `eventdblclick`, set to `''` or null to
             * disable editing of existing events.
             * @config {String}
             * @default
             * @category Editor
             */
            triggerEvent : 'eventdblclick',

            /**
             * The current {@link Scheduler.model.EventModel} record, which is being edited by the event editor.
             * @property {Scheduler.model.EventModel}
             * @readonly
             */
            eventRecord : null,

            /**
             * The {@link Scheduler.model.EventModel} field to edit
             * @config {String}
             * @category Editor
             */
            field : 'name',

            /**
             * The editor configuration, where you can control which widget to show
             * @config {EditorConfig}
             * @category Editor
             */
            editorConfig : null
        };
    }

    static get pluginConfig() {
        return {
            chain : ['onEventEnterKey', 'editEvent']
        };
    }

    //endregion

    //region Editing

    construct(scheduler, config) {
        const me = this;

        me.scheduler = scheduler;

        scheduler.eventEdit = me;

        super.construct(scheduler, config);

        me.clientListenersDetacher = scheduler.ion({
            [me.triggerEvent] : ({ eventRecord, eventElement }) => me.editEvent(eventRecord, eventRecord.resource, eventElement),
            dragcreateend     : me.onDragCreateEnd,
            thisObj           : me
        });
    }

    doDestroy() {
        this.clientListenersDetacher();

        this.editor?.destroy();

        super.doDestroy();
    }

    get eventStore() {
        return this.scheduler.eventStore;
    }

    get project() {
        return this.client.project;
    }

    // region Editor
    /**
     * Opens an Editor for the passed event. This function is exposed on Scheduler and can be called as
     * `scheduler.editEvent()`.
     * @param {Scheduler.model.EventModel} eventRecord The Event to edit
     * @param {Scheduler.model.ResourceModel} [resourceRecord] The Resource record for the event.
     * @on-owner
     * @async
     */
    async editEvent(eventRecord, resourceRecord, element, stmCapture) {
        const
            me            = this,
            { scheduler } = me,
            { eventEdit } = me.client.features;

        // If event edit feature also exists, we use simple edit for new events and eventEdit it for already created events
        if (scheduler.readOnly || me.disabled || eventRecord.readOnly || (eventEdit && !eventEdit.disabled && !eventRecord.isCreating)) {
            return;
        }

        let { editor } = me;

        // Want to put editor in inner element (b-sch-event) to get correct font size, but when drag creating the proxy
        // has no inner element
        element = DomHelper.down(element, scheduler.eventInnerSelector) || element;

        eventRecord = eventRecord.isAssignment ? eventRecord.event : eventRecord;

        me.resource = resourceRecord;
        me.event    = eventRecord;
        me.element  = element;

        scheduler.element.classList.add('b-eventeditor-editing');

        if (editor) {
            // Positioned editors remove themselves so that their appendTo element
            // may have its content updated unobstructed.
            editor.render(scheduler.timeAxisSubGridElement);
        }
        else {
            // Editor is contained in, and owned by the TimeAxisSubGrid to avoid focus flipping out and in.
            // The editor is an owned descendant of the SubGrid.
            me.editor = editor = Editor.new({
                owner        : scheduler.timeAxisSubGrid,
                appendTo     : scheduler.timeAxisSubGridElement,
                scrollAction : 'realign',
                maxHeight    : 40,
                align        : {
                    align : scheduler.isHorizontal ? 'c-c' : 't-t'
                },
                cls               : 'b-simpleeventeditor',
                internalListeners : {
                    complete : 'onEditorComplete',
                    cancel   : 'onEditorCancel',
                    thisObj  : me
                },

                // Keys must not propagate into the scheduler
                onInternalKeyDown : keyEvent => keyEvent.stopPropagation()
            }, me.editorConfig);

            me.relayEvents(me.editor, ['beforestart', 'start', 'beforecomplete', 'complete', 'beforecancel', 'cancel']);
        }

        if (stmCapture) {
            me.stmInitiallyAutoRecord = stmCapture.stmInitiallyAutoRecord;
            me.stmInitiallyDisabled = stmCapture.stmInitiallyDisabled;
            me.hasStmCapture = true;

            // indicate that editor has been opened, and is now managing the "stm capture"
            stmCapture.transferred = true;
        }
        // it is set to `false` by calendar, to ignore the STM mechanism
        else if (stmCapture !== false && !me.hasStmCapture) {
            me.captureStm(true);
        }

        // Drag-created records get a "New event" name for cosmetic purposes.
        // Remove just before editing.
        if (eventRecord.isCreating) {
            eventRecord.name = '';
        }

        await editor.startEdit({
            target : element,
            record : eventRecord,
            field  : me.field
        });

        // If text label is not visible, scroll it into view
        if (scheduler.isVertical && eventRecord.startDate < scheduler.visibleDateRange.startDate) {
            editor.element.scrollIntoView();
        }

        // No key navigation during editing
        scheduler.navigator.disabled = true;
    }

    onEditorComplete() {
        // Promote event to being permanent so that it is syncable to the server as a new event
        this.event.isCreating = false;

        this.reset();

        this.freeStm(true);
    }

    onEditorCancel() {
        // Remove the transient event
        if (this.event.isCreating) {
            this.event.remove();
        }

        this.reset();

        this.freeStm(false);
    }

    reset() {
        this.scheduler.element.classList.remove('b-eventeditor-editing');

        // Restore key navigation after editing
        this.scheduler.navigator.disabled = false;
        this.event                        = null;
        this.resource                     = null;
    }

    //endregion

    // chained from EventNavigation
    onEventEnterKey({ assignmentRecord, eventRecord }) {
        const
            element        = assignmentRecord ? this.scheduler.getElementFromAssignmentRecord(assignmentRecord) : this.scheduler.getElementFromEventRecord(eventRecord),
            resourceRecord = (assignmentRecord || eventRecord).resource;

        this.editEvent(eventRecord, resourceRecord, element);
    }

    //endregion

    onDragCreateEnd({ eventRecord, resourceRecord, eventElement, stmCapture }) {
        this.element = eventElement;

        this.editEvent(eventRecord, resourceRecord, eventElement, stmCapture);
    }
}

GridFeatureManager.registerFeature(SimpleEventEdit, false, 'Scheduler');
