import Delayable from '../../Core/mixin/Delayable.js';
import TaskEditStm from '../../Scheduler/feature/mixin/TaskEditStm.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import MessageDialog from '../../Core/widget/MessageDialog.js';
import Widget from '../../Core/widget/Widget.js';
import '../widget/GanttTaskEditor.js';
import '../widget/SchedulerTaskEditor.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import { ProjectType } from '../../Engine/scheduling/Types.js';
import TaskEditTransactional from '../../Scheduler/feature/mixin/TaskEditTransactional.js';
import TransactionalFeature from '../../Scheduler/feature/mixin/TransactionalFeature.js';

/**
 * @module SchedulerPro/feature/TaskEdit
 */

/**
 * Feature that displays a {@link SchedulerPro.widget.SchedulerTaskEditor Task editor}, allowing users to edit task data.
 * The Task editor is a popup containing tabs with fields for editing task data.
 *
 * This demo shows the task edit feature, double-click child task bar to start editing:
 *
 * {@inlineexample SchedulerPro/feature/TaskEdit.js}
 *
 * ## Customizing tabs and their widgets
 *
 * To customize tabs you can:
 *
 * * Reconfigure built in tabs by providing override configs in the {@link #config-items} config.
 * * Remove existing tabs or add your own in the {@link #config-items} config.
 * * Advanced: Reconfigure the whole editor widget using {@link #config-editorConfig} or replace the whole editor using
 *   {@link #config-editorClass}.
 *
 * To add extra items to a tab you need to specify {@link Core.widget.Container#config-items} for the tab container.
 *
 * This demo shows adding of custom widgets to the task editor, double-click child task bar to start editing:
 *
 * {@inlineexample SchedulerPro/feature/TaskEditExtraItems.js}
 *
 * {@region Expand to see Default tabs and fields}
 *
 * The {@link SchedulerPro.widget.SchedulerTaskEditor Task editor} contains tabs by default. Each tab contains built in
 * widgets: text fields, grids, etc.
 *
 * | Tab ref           | Type                                                        | Text         | Weight | Description                                                            |
 * |-------------------|-------------------------------------------------------------|--------------|--------|------------------------------------------------------------------------|
 * | `generalTab`      | {@link SchedulerPro.widget.taskeditor.SchedulerGeneralTab}  | General      | 100    | Basic fields: name, resources, start/end dates, duration, percent done |
 * | `predecessorsTab` | {@link SchedulerPro.widget.taskeditor.PredecessorsTab}      | Predecessors | 200    | Shows a grid with incoming dependencies                                |
 * | `successorsTab`   | {@link SchedulerPro.widget.taskeditor.SuccessorsTab}        | Successors   | 300    | Shows a grid with outgoing dependencies                                |
 * | `advancedTab`     | {@link SchedulerPro.widget.taskeditor.SchedulerAdvancedTab} | Advanced     | 500    | Shows advanced configuration: constraints and manual scheduling mode   |
 * | `notesTab`        | {@link SchedulerPro.widget.taskeditor.NotesTab}             | Notes        | 600    | Shows a text area to add notes to the selected task                    |
 *
 * ### General tab
 *
 * General tab contains fields for basic configurations
 *
 * | Field ref          | Type                                     | Text       | Weight | Description                                           |
 * |--------------------|------------------------------------------|------------|--------|-------------------------------------------------------|
 * | `nameField`        | {@link Core.widget.TextField}            | Name       | 100    | Task name                                             |
 * | `resourcesField`   | {@link Core.widget.Combo}                | Resources  | 200    | Shows a list of available resources for this task     |
 * | `startDateField`   | {@link Core.widget.DateTimeField}        | Start      | 300    | Shows when the task begins                            |
 * | `endDateField`     | {@link Core.widget.DateTimeField}        | Finish     | 400    | Shows when the task ends                              |
 * | `durationField`    | {@link Core.widget.DurationField}        | Duration   | 500    | Shows how long the task is                            |
 * | `percentDoneField` | {@link Core.widget.NumberField}          | % Complete | 600    | Shows what part of task is done already in percentage |
 * | `colorField` ¹     | {@link Scheduler.widget.EventColorField} | Color ¹    | 700    | Choose background color for the task bar              |
 *
 * **¹** Set the {@link SchedulerPro.view.SchedulerProBase#config-showTaskColorPickers} config to `true` to enable this field
 *
 * ### Predecessors tab
 *
 * Predecessors tab contains a grid with incoming dependencies and controls to remove/add dependencies
 *
 * | Widget ref | Type                        | Weight | Description                                                                        |
 * |------------|-----------------------------|--------|------------------------------------------------------------------------------------|
 * | `grid`     | {@link Grid.view.Grid}      | 100    | Shows predecessors task name, dependency type and lag                              |
 * | `toolbar`  | {@link Core.widget.Toolbar} | 200    | Shows control buttons                                                              |
 * | \>`add`    | {@link Core.widget.Button}  | 210    | Adds a new predecessor. Then select a task from the list in the name column editor |
 * | \>`remove` | {@link Core.widget.Button}  | 220    | Removes selected incoming dependency                                               |
 *
 * \> - nested items
 *
 * ### Successors tab
 *
 * Successors tab contains a grid with outgoing dependencies and controls to remove/add dependencies
 *
 * | Widget ref | Type                        | Weight | Description                                                                      |
 * |------------|-----------------------------|--------|----------------------------------------------------------------------------------|
 * | `grid`     | {@link Grid.view.Grid}      | 100    | Shows successors task name, dependency type and lag                              |
 * | `toolbar`  | {@link Core.widget.Toolbar} | 200    | Shows control buttons                                                            |
 * | \>`add`    | {@link Core.widget.Button}  | 210    | Adds a new successor. Then select a task from the list in the name column editor |
 * | \>`remove` | {@link Core.widget.Button}  | 220    | Removes selected outgoing dependency                                             |
 *
 * \> - nested items
 *
 * ### Advanced tab
 *
 * Advanced tab contains additional task scheduling options
 *
 * | Field ref                     | Type                                             | Weight | Description                                                                    |
 * |-------------------------------|--------------------------------------------------|--------|--------------------------------------------------------------------------------|
 * | `calendarField`               | {@link SchedulerPro.widget.CalendarField}        | 100    | List of available calendars for this task. Shown when calendars are downloaded |
 * | `constraintTypeField`         | {@link SchedulerPro.widget.ConstraintTypePicker} | 200    | Shows a list of available constraints for this task                            |
 * | `constraintDateField`         | {@link Core.widget.DateField}                    | 300    | Shows a date for the selected constraint type                                  |
 * | `manuallyScheduledField`      | {@link Core.widget.Checkbox}                     | 400    | If checked, the task is not considered in scheduling                           |
 * | `inactiveField`               | {@link Core.widget.Checkbox}                     | 500    | Allows inactivating the task so it won't take part in the scheduling process.  |
 * | `ignoreResourceCalendarField` | {@link Core.widget.Checkbox}                     | 600    | If checked the event ignores the assigned resource calendars when scheduling   |
 *
 * ### Notes tab
 *
 * Notes tab contains a text area to show notes
 *
 * | Field ref   | Type                              | Weight | Description                                     |
 * |-------------|-----------------------------------|--------|-------------------------------------------------|
 * | `noteField` | {@link Core.widget.TextAreaField} | 100    | Shows a text area to add text notes to the task |
 *
 * The built in buttons are:
 *
 * | Widget ref             | Type                        | Weight | Description                                                    |
 * |------------------------|-----------------------------|--------|----------------------------------------------------------------|
 * | `saveButton`           | {@link Core.widget.Button}  | 100    | Save event button on the bbar                                  |
 * | `deleteButton`         | {@link Core.widget.Button}  | 200    | Delete event button on the bbar                                |
 * | `cancelButton`         | {@link Core.widget.Button}  | 300    | Cancel event editing button on the bbar                        |
 *
 * {@endregion}
 *
 * ## Removing a built in item or toolbar button
 *
 * To remove a built in toolbar button, tab or field, specify its `ref` as `false` in the `items` config:
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab      : {
 *                     items : {
 *                         // Remove "Duration" and "% Complete" fields in the "General" tab
 *                         durationField    : false,
 *                         percentDoneField : false
 *                     }
 *                 },
 *                 // Remove all tabs except the "General" tab
 *                 notesTab        : false,
 *                 predecessorsTab : false,
 *                 successorsTab   : false,
 *                 advancedTab     : false
 *             },
 *             editorConfig : {
 *                 bbar : {
 *                     // Remove delete button
 *                     items : {
 *                         deleteButton : false
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * ## Customizing a built in item
 *
 * To customize a built in tab or field, use its `ref` as the key in the `items` config and specify the configs you want
 * to change (they will be merged with the tabs or fields default configs correspondingly):
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab      : {
 *                     // Rename "General" tab
 *                     title : 'Main',
 *                     items : {
 *                         // Rename "% Complete" field
 *                         percentDoneField : {
 *                             label : 'Status'
 *                         }
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * ## Adding a custom item
 *
 * To add a custom tab or field, add an entry to the `items` config. When you add a field,
 * the `name` property links the input field to a field in the loaded task record:
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab : {
 *                     items : {
 *                         // Add new field to the last position
 *                         newGeneralField : {
 *                             type   : 'textfield',
 *                             weight : 610,
 *                             label  : 'New field in General Tab',
 *                             // Name of the field matches data field name, so value is loaded/saved automatically
 *                             name   : 'custom'
 *                         }
 *                     }
 *                 },
 *                 // Add a custom tab to the first position
 *                 newTab     : {
 *                     // Tab is a FormTab by default
 *                     title  : 'New tab',
 *                     weight : 90,
 *                     items  : {
 *                         newTabField : {
 *                             type   : 'textfield',
 *                             weight : 10,
 *                             label  : 'New field in New Tab',
 *                             // Name of the field matches data field name, so value is loaded/saved automatically.
 *                             // In this case it is equal to the Task "name" field.
 *                             name   : 'name'
 *                         }
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * To turn off the Task Editor just simple disable the feature.
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         taskEdit : false
 *     }
 * })
 * ```
 *
 * By default predecessors and successors in successorsTab and predecessorsTab are displayed using task id and a name.
 *  The id part is configurable, any task field may be used instead (for example wbsCode or sequence number)
 * by Gantt `dependencyIdField` property, to set it globally, or using
 *  taskEdit config {@link SchedulerPro/widget/TaskEditorBase#config-dependencyIdField} to set format only for taskEditor.
 * ```javascript
 * const gantt = new Gantt({
 *    dependencyIdField: 'wbsCode', // for global format
 *
 *    project,
 *    columns : [
 *        { type : 'name', width : 250 }
 *    ],
 *    features : {
 *         taskEdit : {
 *             editorConfig : {
 *                 dependencyIdField : 'wbsCode' // set only for taskEditor
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * For more info on customizing the Task Editor, please see Guides/Customization/Customize task editor
 *
 * ## Editing nested events
 *
 * Note that when editing nested events the resource field, the successors tab and the predecessors tab are hidden
 * automatically.
 *
 * @extends Core/mixin/InstancePlugin
 * @mixes Scheduler/feature/mixin/TaskEditStm
 * @mixes Core/mixin/Delayable
 * @demo SchedulerPro/taskeditor
 * @classtype taskEdit
 * @feature
 */
export default class TaskEdit extends InstancePlugin.mixin(
    Delayable,
    TaskEditStm,
    TransactionalFeature,
    TaskEditTransactional
) {

    //region Events
    /**
     * Fires on the owning Scheduler instance before a task is displayed in the editor.
     * This may be listened to in order to take over the task editing flow. Returning `false`
     * stops the default editing UI from being shown.
     *
     * Allows async flows by awaiting async listeners. For example:
     *
     * ```javascript
     * new SchedulerPro({
     *     listeners : {
     *         async beforeTaskEdit() {
     *            await asyncCheckOfRightsOnBackend();
     *         }
     *     }
     * });
     * ```
     * @event beforeTaskEdit
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.feature.TaskEdit} taskEdit The taskEdit feature
     * @param {SchedulerPro.model.EventModel} taskRecord The task about to be shown in the editor.
     * @param {HTMLElement} taskElement The element which represents the task
     * @preventable
     * @async
     */

    /**
     * Fires on the owning Scheduler when the editor for an event is canceled.
     * @event taskEditCanceled
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} taskRecord the task about the shown in the editor
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor
     */

    /**
     * Fires on the owning Scheduler when the editor for an event is available but before it is shown. Allows
     * manipulating fields etc.
     * @event beforeTaskEditShow
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The SchedulerPro instance
     * @param {SchedulerPro.feature.TaskEdit} taskEdit The taskEdit feature
     * @param {SchedulerPro.model.EventModel} taskRecord The task about to be shown in the editor.
     * @param {HTMLElement} eventElement The element which represents the task
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor
     */

    /**
     * Fires on the owning Scheduler Pro instance before a task is saved, return `false` to prevent it.
     *
     * Allows async flows by awaiting async listeners. For example:
     *
     * ```javascript
     * new SchedulerPro({
     *     listeners : {
     *         async beforeTaskSave() {
     *            await someAsyncConditionLikeAskingForConfirmation();
     *         }
     *     }
     * });
     * ```
     *
     * @event beforeTaskSave
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} taskRecord The task about to be saved
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     * @preventable
     * @async
     */

    /**
     * Fires on the owning Scheduler Pro instance after a task is saved
     * @event afterTaskSave
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} taskRecord The task about to be saved
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     */

    /**
     * Fires on the owning Scheduler Pro instance after task editing is finished by applying changes, cancelling them
     * or deleting the task record.
     * @event afterTaskEdit
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} taskRecord Task record used in the task editor
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     */

    /**
     * Fires on the owning Scheduler Pro before a task is deleted, return `false` to prevent it.
     *
     * Allows async flows by awaiting async listeners. For example:
     *
     * ```javascript
     * new SchedulerPro({
     *     listeners : {
     *         async beforeTaskDelete() {
     *            await someAsyncConditionLikeAskingForConfirmation();
     *         }
     *     }
     * });
     * ```
     *
     * @event beforeTaskDelete
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance.
     * @param {SchedulerPro.model.EventModel} taskRecord The record about to be deleted
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     * @preventable
     * @async
     */

    /**
     * Fires on the owning Scheduler Pro instance before an event record is saved, return `false` to prevent it.
     *
     * Allows async flows by awaiting async listeners. For example:
     *
     * ```javascript
     * new SchedulerPro({
     *     listeners : {
     *         async beforeEventSave() {
     *            await someAsyncConditionLikeAskingForConfirmation();
     *         }
     *     }
     * });
     * ```
     *
     * @event beforeEventSave
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} eventRecord The event record about to be saved
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     * @preventable
     * @async
     */

    /**
     * Fires on the owning Scheduler Pro instance after an event record is saved
     * @event afterEventSave
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance
     * @param {SchedulerPro.model.EventModel} eventRecord The event record about to be saved
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     */

    /**
     * Fires on the owning Scheduler Pro before an event record is deleted, return `false` to prevent it.
     *
     * Allows async flows by awaiting async listeners. For example:
     *
     * ```javascript
     * new SchedulerPro({
     *     listeners : {
     *         async beforeEventDelete() {
     *            await someAsyncConditionLikeAskingForConfirmation();
     *         }
     *     }
     * });
     * ```
     * @event beforeEventDelete
     * @on-owner
     * @param {SchedulerPro.view.SchedulerPro} source The Scheduler Pro instance.
     * @param {SchedulerPro.model.EventModel} eventRecord The event record about to be deleted
     * @param {SchedulerPro.widget.TaskEditorBase} editor The editor widget
     * @preventable
     * @async
     */
    //endregion

    //region Config

    static get $name() {
        return 'TaskEdit';
    }

    static get pluginConfig() {
        return {
            chain  : ['populateEventMenu', 'onEventEnterKey'],
            assign : ['editEvent']
        };
    }

    static get defaultConfig() {
        return {
            /**
             * The event that shall trigger showing the editor. Defaults to `eventdblclick`, set to `` or null to
             * disable editing of existing events.
             * @config {String}
             * @default
             * @category Editor
             */
            triggerEvent : 'eventdblclick',

            /**
             * Project type to editor class map. Editor will be used depending on project, not on product.
             *
             * @config {Object}
             * @internal
             * @category Editor
             */
            editorClassMap : {
                [ProjectType.SchedulerBasic] : 'schedulertaskeditor',
                [ProjectType.SchedulerPro]   : 'schedulertaskeditor',
                [ProjectType.Gantt]          : 'gantttaskeditor'
            },


            /**
             * Class to use as the editor. By default it picks editor class depending on the project type.
             * It can be either {@link SchedulerPro.widget.SchedulerTaskEditor} or
             * {@link SchedulerPro.widget.GanttTaskEditor}.
             * By specifying your own `editorClass` you override this.
             * @config {Core.widget.Widget}
             * @typings {typeof Widget}
             * @category Editor
             */
            editorClass : null,

            /**
             * A configuration object applied to the internal {@link SchedulerPro.widget.TaskEditorBase TaskEditor}.
             * Useful to for example change the title of the editor or to set its dimensions in code:
             *
             * ```javascript
             * features : {
             *     taskEdit : {
             *         editorConfig : {
             *             title : 'My title',
             *             height : 300
             *         }
             *     }
             * }
             * ```
             *
             * NOTE: The easiest approach to affect editor contents is to use the {@link #config-items items config}.
             *
             *  @config {TaskEditorBaseConfig}
             */
            editorConfig : null,

            /**
             * True to show a confirmation dialog before deleting the event
             * @config {Boolean}
             * @default
             * @category Editor widgets
             */
            confirmDelete : true,

            /**
             * True to save and close this panel if ENTER is pressed in one of the input fields inside the panel.
             * @config {Boolean}
             * @default
             * @category Editor
             */
            saveAndCloseOnEnter : true,

            /**
             * What action should be taken when you click outside the editor, `cancel` or `save`
             * @config {'cancel'|'save'}
             * @default
             */
            blurAction : 'cancel',

            /**
             * The week start day used in all date fields of the feature editor form by default.
             * 0 means Sunday, 6 means Saturday.
             * Defaults to the locale's week start day.
             * @config {Number}
             */
            weekStartDay : null,

            /**
             * Set to false to not scroll event into view when invoking edit action (e.g. if event is only partially visible)
             * @config {Boolean}
             * @default
             */
            scrollIntoView : true
        };
    }

    static get configurable() {
        return {
            /**
             * A configuration object used to customize the contents of the task editor. Supply a config object or
             * boolean per tab (listed below) to either affects its contents or toggle it on/off.
             *
             * Supplied config objects will be merged with the tabs predefined configs.
             *
             * To remove existing items, set corresponding keys to `null`.
             *
             * Built-in tab names are:
             *  * `generalTab`
             *  * `predecessorsTab`
             *  * `successorsTab`
             *  * `advancedTab`
             *  * `notesTab`
             *
             *  ```
             *  features : {
             *      taskEdit : {
             *          items : {
             *              // Custom settings and additional items for the general tab
             *              generalTab : {
             *                  title : 'Common',
             *                  items : {
             *                      durationField : false,
             *                      myCustomField : {
             *                          type : 'text',
             *                          name : 'color'
             *                      }
             *                  }
             *              },
             *              // Hide the advanced tab
             *              advancedTab : null
             *          }
             *      }
             *  }
             *  ```
             *
             *  Please see the `taskeditor` demo for a customized editor in action.
             *  @config {Object}
             */
            items : null,

            /**
             * When field in task editor is changed, project model normally will trigger `hasChanges` event. If you use
             * this event to handle project changes excessive events might be a problem. Set this flag to true to only
             * trigger single `hasChanges` event after task changes are applied.
             * @config {Boolean} suspendHasChangesEvent
             * @default
             */
            suspendHasChangesEvent : false
        };
    }

    //endregion

    //region Constructor/Destructor

    construct(scheduler, config) {
        scheduler.taskEdit = this;

        super.construct(scheduler, ObjectHelper.assign({
            weekStartDay          : scheduler.weekStartDay,
            enableEventSpanBuffer : scheduler.features.eventBuffer?.enabled
        }, config));

        scheduler.ion({
            [this.triggerEvent] : 'onActivateEditor',
            readOnly            : 'onClientReadOnlyToggle',
            dragCreateEnd       : 'onDragCreateEnd',
            afterEventDrop      : 'onEventDropped',
            eventResizeEnd      : 'onEventResized',
            thisObj             : this
        });
    }

    doDestroy() {
        this.cleanupProjectListener();

        this.editor?.destroy();

        if (this.deleteConfirmationPromise) {
            MessageDialog.hide();
        }

        super.doDestroy();
    }

    //endregion

    //region Internal

    onClientReadOnlyToggle({ readOnly }) {
        if (this.editor) {
            this.editor.readOnly = readOnly;
        }
    }

    get scheduler() {
        return this.client;
    }

    getElementFromTaskRecord(taskRecord, resourceRecord) {
        return this.client.getElementFromEventRecord(taskRecord, resourceRecord);
    }

    scrollEventIntoView(eventRecord, resourceRecord) {
        this.client.scrollResourceEventIntoView(resourceRecord, eventRecord);
    }

    get isValid() {
        return this.editor.eachWidget(widget => {
            if (widget.isValid === true || widget.hidden || widget.disabled || (widget.isField && !widget.name)) {
                return true;
            }

            return widget.isValid !== false;
        }, true);
    }

    //endregion

    //region Project

    get project() {
        return this.scheduler.project;
    }

    setupProjectListener() {
        this.cleanupProjectListener();

        this.project.ion({
            name      : 'project',
            loadstart : () => this.save(),
            dataReady : 'onDataReady',
            thisObj   : this
        });
    }

    cleanupProjectListener() {
        this.detachListeners('project');
    }

    //endregion

    onDataReady() {
        const { record } = this;

        // Record could've been removed from project
        if (record?.project && this.scheduler.taskStore.includes(record)) {
            this.load(record, true);
        }
        else {
            this.editor.close();
        }
    }

    //region Editor

    /**
     * Returns true if the editor is currently active
     * @readonly
     * @property {Boolean}
     */
    get isEditing() {
        return !!this._editing;
    }

    onActivateEditor({ eventRecord, resourceRecord, eventElement }) {
        // attempt to re-open already opened editor for the same record - do nothing
        if (eventRecord === this.record && this.isEditing) {
            return;
        }

        this.editEvent(eventRecord, resourceRecord, eventElement);
    }

    /**
     * Shows a {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or {@link SchedulerPro.widget.GanttTaskEditor gantt task editor}
     * to edit the passed task. This function is exposed on the Scheduler Pro instance and can be called as `scheduler.editTask()`.
     * @param {SchedulerPro.model.EventModel|Function} taskRecord Task to edit or a function returning a task to edit,
     * the function will be executed within an STM transaction which will be canceled in case user presses Cancel button
     * or closes editor w/o hitting Save.
     * @param {SchedulerPro.model.ResourceModel} [resourceRecord] The Resource record for the event. This parameter is
     * required if the event is newly created for a resource and has not been assigned, or when using multi assignment.
     * @param {HTMLElement} [element] Element to anchor editor to (defaults to events element)
     * @returns {Promise} Promise which resolves after the editor is shown
     */
    async editEvent(taskRecord, resourceRecord = null, element = null, stmCapture = null) {
        const
            me          = this,
            {
                scheduler,
                suspendHasChangesEvent
            }           = me,
            { project } = scheduler,
            cleanup     = async() => {
                project.resumeAutoSync();
                await me.freeStm(true);

                if (me.isDestroyed) {
                    return;
                }

                if (suspendHasChangesEvent && !project.isDestroying) {
                    project.resumeChangesTracking();
                }

                me._editing = false;
            };

        taskRecord = taskRecord.isEventSegment ? taskRecord.event : taskRecord;

        const doEdit = !me.disabled && !taskRecord.readOnly && !project.isDelayingCalculation && (taskRecord.project || !scheduler.usesDisplayStore);

        if (doEdit && stmCapture) {
            // need to set this flag synchronously, to indicate to the outer world,
            // that we are going to take the ownership of the "stm capture"
            // only setting the flag, actual taking ownership happens later
            stmCapture.transferred = true;
        }

        // If we are editing, cancel the edit.
        if (me.isEditing) {
            await me.cancel();
        }

        if (scheduler.features.cellEdit?.isEditing) {
            scheduler.cancelEditing();
        }

        // We may have just canceled the edit above, or some other user gesture may
        // have begun cancelling prior to that.
        // Either way, we must wait for it to cancel before the new edit can begin.
        await Promise.all([me._cancelling, me._hiding]).catch(() => { /* Do nothing on rejected Promise */ });

        if (!scheduler.isGanttBase && !resourceRecord) {
            // In case of assignments, take the first resource
            resourceRecord = taskRecord.resource || taskRecord.resources?.[0];
        }

        // Record without project might be a transient record in a display store, not meant to be manipulated
        if (doEdit) {
            const { taskStore } = scheduler;

            me._editing = true;

            if (stmCapture) {
                me.applyStmCapture(stmCapture);
                me.hasStmCapture = true;
                me.stmCapture = stmCapture;
            }
            else if (!me.hasStmCapture) {
                await me.captureStm(true);
            }

            project.suspendAutoSync();

            if (typeof taskRecord === 'function') {
                taskRecord = taskRecord();
            }

            // Open original record when editing a linked record
            taskRecord = taskRecord.$original;

            // If this is a new record, add it to the store and assign to a resource only after we have started a transaction
            // which can be rolled back in case of Cancel button press
            if (!taskRecord.isOccurrence && !taskStore.includes(taskRecord) && !taskRecord.isCreating) {
                taskRecord.isCreating = true;

                taskStore.add(taskRecord);

                if (resourceRecord) {
                    scheduler.assignmentStore.assignEventToResource(taskRecord, resourceRecord);
                }

                await project.commitAsync();

                if (me.isDestroyed) {
                    return;
                }
            }

            // For programmatic edit calls for an event not currently in view, scroll it into view first
            if (me.scrollIntoView && !scheduler.timeAxisSubGrid.collapsed && !element && taskStore.includes(taskRecord) && (resourceRecord || scheduler.isGantt)) {
                await me.scrollEventIntoView(taskRecord, resourceRecord);

                if (me.isDestroyed) {
                    return;
                }
            }

            const
                taskElement = element || DomHelper.down(
                    me.getElementFromTaskRecord(taskRecord, resourceRecord),
                    scheduler.eventInnerSelector
                ),
                editor      = me.getEditor(taskRecord);

            if (await me.triggerOnClient('beforeTaskEdit', { taskEdit : me, taskRecord, taskElement }) !== false) {
                if (me.suspendHasChangesEvent) {
                    scheduler.project.suspendChangesTracking();
                }

                // The Promise being async allows a mouseover to trigger the event tip
                // unless we add the editing class immediately.
                scheduler.element.classList.add('b-taskeditor-editing');

                // Wait for any pending commit
                await project.commitAsync();

                if (!taskRecord.isOccurrence) {
                    if (!scheduler.eventStore.includes(taskRecord)) {
                        // Edge case - ensure the record was not removed from task store in the async flows above
                        await cleanup();
                        return;
                    }
                    else {
                        // In case dataset was replaced in listeners above
                        taskRecord = scheduler.eventStore.getById(taskRecord.id);
                    }
                }

                if (me.isDestroyed) {
                    return;
                }

                me.load(taskRecord);

                me.triggerOnClient('beforeTaskEditShow', {
                    taskEdit : me,
                    taskRecord,
                    taskElement,
                    editor
                });

                const
                    { widgetMap } = editor,
                    isNestedEvent = taskRecord.parent && !taskRecord.parent.isRoot;

                if (widgetMap.deleteButton) {
                    widgetMap.deleteButton.hidden = scheduler.readOnly || taskRecord.isCreating || editor.readOnly;
                }

                if (scheduler.features.nestedEvents) {
                    // Editing a nested event, disable parts of the editor
                    if (widgetMap.predecessorsTab) {
                        widgetMap.predecessorsTab.disabled = isNestedEvent;
                    }

                    if (widgetMap.successorsTab) {
                        widgetMap.successorsTab.disabled = isNestedEvent;
                    }

                    if (widgetMap.resourcesField) {
                        widgetMap.resourcesField.disabled = isNestedEvent;
                    }
                }

                me.setupProjectListener();

                if (editor.centered) {
                    await editor.show();
                }
                else {
                    if (
                        !scheduler.timeAxisSubGrid.collapsed &&
                        taskElement &&
                        // In Gantt, ensure task bar is not out of view (if triggering edit from a grid cell)
                        (!scheduler.isGanttBase || me.scrollIntoView || Rectangle.from(taskElement).intersect(Rectangle.from(scheduler.timeAxisSubGridElement)))
                    ) {
                        await editor.showBy({
                            target : taskElement,
                            anchor : true,
                            offset : -5
                        });
                    }
                    else {
                        // Display the editor centered in the Scheduler
                        await editor.showBy({
                            target    : scheduler.element,
                            anchor    : false,
                            // For records not part of the store (new ones, or filtered out ones) - center the editor
                            align     : 'c-c',
                            clippedBy : null
                        });
                    }
                }
            }
            else {
                await cleanup();
            }
        }
    }

    prepareEditorConfig(config) {
        return config;
    }

    getEditor(taskRecord = this.record) {
        const
            me         = this,
            { client } = me;

        let { editor } = me;

        if (!editor) {
            const config = me.prepareEditorConfig(ObjectHelper.merge({
                clippedBy                : [client.timeAxisSubGridElement, client.bodyContainer],
                eventEditFeature         : me,
                weekStartDay             : me.weekStartDay,
                enableEventSpanBuffer    : me.enableEventSpanBuffer,
                saveAndCloseOnEnter      : me.saveAndCloseOnEnter,
                blurAction               : me.blurAction,
                owner                    : client,
                dependencyIdField        : me.editorConfig?.dependencyIdField || client.dependencyIdField,
                project                  : me.project,
                durationDisplayPrecision : client.durationDisplayPrecision,
                tabPanelItems            : me.items,
                internalListeners        : {
                    beforeHide : {
                        fn   : 'onBeforeHide',
                        // Unreasonable to prevent hide in non-prioritized listener
                        prio : -1000
                    },
                    cancel  : 'onCancel',
                    delete  : 'onDelete',
                    save    : 'onSave',
                    thisObj : me
                },
                // For backward compatibility
                tabsConfig : me.tabsConfig
            }, me.editorConfig));

            if (!client.showTaskColorPickers && config.tabPanelItems?.generalTab !== false) {
                config.tabPanelItems = ObjectHelper.merge(config.tabPanelItems || {}, {
                    generalTab : { items : { colorField : false } }
                });
            }

            // Configured type should always win
            if (me.editorClass && !config.type) {
                editor = me.editor = me.editorClass.new(config);
            }
            else {
                const
                    // Editor will be used depending on project, not on product
                    project     = taskRecord?.project || me.project,
                    projectType = project.getType();

                editor = me.editor = Widget.create(Object.assign({
                    type : me.editorClassMap[projectType] || 'schedulertaskeditor'
                }, config));
            }
        }

        // Must set *after* construction, otherwise it becomes the default state to reset readOnly back to.
        // Recurrent events are opened read-only, unless this is a reload after choosing to edit it
        editor.readOnly = (client.readOnly || taskRecord?.isOccurrence || taskRecord?.isRecurring) && !editor.editingRecurring;

        editor.project = me.project;

        return editor;
    }

    //endregion

    //region Actions

    load(taskRecord, highlightChanges) {
        const
            me     = this,
            editor = me.getEditor(taskRecord);

        me._loading = true;

        // task editor is not meant to edit a segment model so load its main task instead
        if (taskRecord.isEventSegment) {
            taskRecord = taskRecord.event;
        }

        me.record = taskRecord;
        editor.loadEvent(taskRecord, highlightChanges);

        me._loading = false;
    }

    /**
     * Call this method to close task editor saving changes.
     * @returns {Promise} A promise which is resolved when the task editor is closed and changes are saved.
     */
    async save() {
        const
            me                                 = this,
            { scheduler, record : taskRecord } = me;

        if (me.isEditing) {
            const editor = me.getEditor();

            if (!me.isValid || await me.triggerOnClient('beforeTaskSave', {
                taskRecord,
                editor
            }) === false) {
                return;
            }

            me.cleanupProjectListener();

            editor.beforeSave();

            // Turn a newly created record into a permanent one (no-op for others)
            taskRecord.isCreating = false;

            // Reset exceptionDate tracking, to not react to it on the following cancel
            editor.resetRecurrenceData = editor.editingRecurring = null;

            // Editor close promise have a chance to get resolved after the transaction promise (e.g. if there's a
            // beforeClose listener on the widget). Therefore, we use a special flag to tell if promise is resolved to
            // make sure we don't skip `hasChanges` event.
            let skipChangeCheck = true;
            // const commitPromise = me.commitStmTransaction();
            const commitPromise = me.freeStm(true);
            if (ObjectHelper.isPromise(commitPromise)) {
                commitPromise.then(() => skipChangeCheck = false);
            }

            me._editing = false;

            // afterSave to happen only after the editor is fully invisible.
            await editor.close();

            if (ObjectHelper.isPromise(commitPromise)) {
                await commitPromise;

                if (me.isDestroying) {
                    return;
                }
            }

            scheduler.project.resumeAutoSync(true);

            scheduler.element.classList.remove('b-taskeditor-editing');

            me.triggerOnClient('afterTaskSave', { taskRecord, editor });

            editor.afterSave();

            if (me.suspendHasChangesEvent) {
                // Suspend event if transactional features are enabled. There would be another commitAsync on the project
                // which will trigger `hasChanges` event
                scheduler.project.resumeChangesTracking(skipChangeCheck);
            }

            me.triggerOnClient('afterTaskEdit', { taskRecord, editor });
        }
    }

    // This is called by the TaskEditor's hide method prior to the super call,
    // so however it gets hidden, it will signal a cancel.
    async doCancel() {
        const
            me = this,
            { scheduler, record : taskRecord } = me;

        if (me.isEditing) {
            // prevent the re-entrance to this method
            me._editing = false;

            me.cleanupProjectListener();

            const
                { project } = me,
                editor      = me.getEditor();

            editor.editingRecurring = null;

            editor.beforeCancel();

            // the feature could get destroyed asynchronously
            if (me.isDestroyed) {
                // project could get destroyed at the same time, if configured with
                project.resumeAutoSync?.(false);
                return;
            }

            // We always need to reject transaction, to make sure we unblock the queue
            await me.rejectStmTransaction();

            if (taskRecord?.isCreating) {
                taskRecord.remove();
            }

            // the feature could get destroyed asynchronously
            if (me.isDestroyed) {
                // project could get destroyed at the same time, if configured with
                project.resumeAutoSync?.(false);
                return;
            }

            // me.disableStm();

            await project.commitAsync();

            // the feature could get destroyed asynchronously
            if (me.isDestroyed) {
                // project could get destroyed at the same time, if configured with
                project.resumeAutoSync?.(false);
                return;
            }

            // the feature could get destroyed asynchronously
            if (me.isDestroyed) {
                // project could get destroyed at the same time, if configured with
                project.resumeAutoSync?.(false);
                return;
            }

            me.freeStm();

            editor.afterCancel();

            project.resumeAutoSync(false);

            scheduler.element.classList.remove('b-taskeditor-editing');

            me.trigger('afterTaskEdit');

            me.triggerOnClient('taskEditCanceled', { taskRecord, editor });

            if (me.suspendHasChangesEvent) {
                project.resumeChangesTracking();
            }

            me.triggerOnClient('afterTaskEdit', { taskRecord, editor });
        }
    }

    /**
     * Call this method to close task editor reverting changes.
     * @returns {Promise} A promise which is resolved when the task editor is closed and changes are reverted.
     */
    async cancel() {
        const
            me                      = this,
            { editor }              = me,
            { resetRecurrenceData } = editor;

        // Reset exceptionDate on cancel, not handled by STM
        if (resetRecurrenceData) {
            resetRecurrenceData.recurringTimeSpan.exceptionDates = resetRecurrenceData.originalExceptionDates;
            editor.resetRecurrenceData = null;
        }

        return me._cancelling || (me._cancelling = me.doCancel().finally(() => me._cancelling = undefined));
    }

    async delete() {
        const
            me                                         = this,
            { editor, scheduler, record : taskRecord, project } = me;

        if (!taskRecord.isCreating && await me.triggerOnClient('beforeTaskDelete', { taskRecord, editor }) === false) {
            return;
        }

        me.cleanupProjectListener();

        editor.beforeDelete();

        taskRecord.remove();

        await me.commitStmTransaction();

        // the feature could get destroyed asynchronously
        if (me.isDestroyed) {
            // project could get destroyed at the same time, if configured with
            project.resumeAutoSync?.(false);
            return;
        }

        me.freeStm();

        await project.commitAsync();

        // the feature could get destroyed asynchronously
        if (me.isDestroyed) {
            return;
        }

        // Resume and sync, unless we are removing a newly created record (via cancel)
        project.resumeAutoSync(!taskRecord.isCreating);

        me._editing = false;

        editor.close();

        editor.afterDelete();

        scheduler.element.classList.remove('b-taskeditor-editing');

        if (me.suspendHasChangesEvent) {
            scheduler.project.resumeChangesTracking();
        }

        me.triggerOnClient('afterTaskEdit', { editor, taskRecord });
    }

    //endregion

    //region Events

    onSave() {
        // There's might be propagation requested, so we giving the chance to start propagating
        // before we're doing save commit procedure.
        this.requestAnimationFrame(() => this.save());
    }

    onCancel() {
        this.cancel().then();
    }

    async onDelete() {
        const me = this;

        if (me.confirmDelete) {

            const
                { editor } = me,
                autoClose  = editor.autoClose;

            editor.autoClose = false;

            me.deleteConfirmationPromise = MessageDialog.confirm({
                title       : 'L{TaskEdit.ConfirmDeletionTitle}',
                message     : 'L{TaskEdit.ConfirmDeletionMessage}',
                okButton    : 'L{TaskEditorBase.Delete}',
                rootElement : me.rootElement
            });

            const result     = await me.deleteConfirmationPromise;

            editor.autoClose = autoClose;

            me.deleteConfirmationPromise = null;

            if (result === MessageDialog.yesButton) {
                me.delete();
            }
        }
        else {
            // There's might be propagation requested, so we giving the chance to start propagating
            // before we're doing cancel rejection procedure.
            me.requestAnimationFrame(() => me.delete());
        }
    }

    onBeforeHide({ source, animate }) {
        // It should not matter that we might override previous promise. Any chained promises still are going to be
        // executed. We only need to make sure that current promise and doCancel promises end at the same time.
        if (source.hideAnimation && animate !== false) {
            this._hiding = new Promise(resolve => {
                source.ion({ hideAnimationEnd : resolve });
            }).finally(() => this._hiding = undefined);
        }
    }

    onDragCreateEnd({ eventRecord, resourceRecord, proxyElement, stmCapture }) {
        // Only edit if it a real create. If it is a drag to schedule an already existing
        // event in gantt, then we do not offer the edit UI.
        if (!this.isDestroyed && !this.disabled && eventRecord.isCreating) {
            this.editEvent(eventRecord, resourceRecord, null, stmCapture);
        }
    }

    //endregion

    //region Context menu

    populateEventMenu({ eventRecord, resourceRecord, items }) {
        if (!this.scheduler.readOnly) {
            items.editEvent = {
                text        : 'L{Edit task}',
                localeClass : this,
                icon        : 'b-icon b-icon-edit',
                weight      : -200,
                disabled    : this.disabled || eventRecord.readOnly,
                onItem      : () => this.editEvent(eventRecord, resourceRecord)
            };
        }
    }

    // chained from EventNavigation
    onEventEnterKey({ assignmentRecord, eventRecord }) {
        if (assignmentRecord) {
            this.editEvent(eventRecord, assignmentRecord.resource);
        }
        else if (eventRecord) {
            this.editEvent(eventRecord, eventRecord.resource);
        }
    }

    //endregion

    // Fire 2 events with param / event name using 'task' + 'event'
    async triggerOnClient(name, params) {
        const
            eventEvent          = name.replace(/task/, 'event').replace(/Task/, 'Event'),
            returnValTaskRecord = await this.scheduler.trigger(...arguments);

        params.eventRecord = params.taskRecord;
        // RecurringEvents mixin expects there to be 'eventRecords' in the beforeEventDelete event
        params.eventRecords = [params.taskRecord];

        const returnValEventRecord = await this.scheduler.trigger(eventEvent, params);

        return returnValTaskRecord && returnValEventRecord;
    }

    onEventDropped({ eventRecords }) {
        const eventRecord = this.editor?.loadedRecord && eventRecords.find(rec => rec === this.editor.loadedRecord);

        eventRecord && this.onEventResized({ eventRecord });
    }

    onEventResized({ eventRecord }) {
        const { editor } = this;
        if (editor?.isVisible && eventRecord === editor.loadedRecord) {
            editor.realign();
        }
    }
}

GridFeatureManager.registerFeature(TaskEdit, true, 'SchedulerPro');//, 'EventEdit');
GridFeatureManager.registerFeature(TaskEdit, false, 'ResourceHistogram');
