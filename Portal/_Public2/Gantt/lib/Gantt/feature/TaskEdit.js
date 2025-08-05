import SchedulerProTaskEdit from '../../SchedulerPro/feature/TaskEdit.js';
import TaskEditor from '../widget/TaskEditor.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';

/**
 * @module Gantt/feature/TaskEdit
 */

/**
 * Feature that displays {@link Gantt.widget.TaskEditor TaskEditor}.
 * The Task editor is a popup containing tabs with fields for editing task data.
 *
 * {@inlineexample gantt/feature/TaskEdit.js}
 *
 * # Customizing tabs and their widgets
 *
 * To customize tabs you can:
 *
 * * Reconfigure built in tabs by providing override configs in the {@link #config-items items} config.
 * * Remove existing tabs or add your own in the {@link #config-items items} config.
 * * Advanced: Reconfigure the whole editor widget using {@link #config-editorConfig} or replace the whole editor using {@link #config-editorClass}.
 *
 * {@inlineexample gantt/feature/TaskEditCustom.js}
 *
 * To add extra items to a tab you need to specify {@link Core.widget.Container#config-items items} for the tab container.
 *
 * {@inlineexample gantt/feature/TaskEditExtraItems.js}
 *
 * {@region Expand to see Default tabs and fields}
 *
 * The {@link Gantt.widget.TaskEditor Task editor} contains tabs by default. Each tab is a container with built in widgets: text fields, grids, etc.
 *
 * | Tab ref           | Text         | Weight | Description                                                                         |
 * |-------------------|--------------|--------|-------------------------------------------------------------------------------------|
 * | `generalTab`      | General      | 100    | Shows basic configuration: name, start/end dates, duration, percent done, effort.   |
 * | `predecessorsTab` | Predecessors | 200    | Shows a grid with incoming dependencies                                             |
 * | `successorsTab`   | Successors   | 300    | Shows a grid with outgoing dependencies                                             |
 * | `resourcesTab`    | Resources    | 400    | Shows a grid with assigned resources to the selected task                           |
 * | `advancedTab`     | Advanced     | 500    | Shows advanced configuration: assigned calendar, scheduling mode, constraints, etc. |
 * | `notesTab`        | Notes        | 600    | Shows a text area to add notes to the selected task                                 |
 *
 * ### General tab
 *
 * General tab contains fields for basic configurations
 *
 * | Field ref     | Type          | Text       | Weight | Description                                                        |
 * |---------------|---------------|------------|--------|--------------------------------------------------------------------|
 * | `name`        | TextField     | Name       | 100    | Task name                                                          |
 * | `percentDone` | NumberField   | % Complete | 200    | Shows what part of task is done already in percentage              |
 * | `effort`      | DurationField | Effort     | 300    | Shows how much working time is required to complete the whole task |
 * | `divider`     | Widget        |            | 400    | Visual splitter between 2 groups of fields                         |
 * | `startDate`   | DateField     | Start      | 500    | Shows when the task begins                                         |
 * | `endDate`     | DateField     | Finish     | 600    | Shows when the task ends                                           |
 * | `duration`    | NumberField   | Duration   | 700    | Shows how long the task is                                         |
 *
 * ### Predecessors tab
 *
 * Predecessors tab contains a grid with incoming dependencies and controls to remove/add dependencies
 *
 * | Widget ref | Type    | Weight | Description                                                                                      |
 * |------------|---------|--------|--------------------------------------------------------------------------------------------------|
 * | `grid`     | Grid    | 100    | Shows predecessors task name, dependency type and lag                                            |
 * | `toolbar`  | Toolbar | 200    | Shows control buttons                                                                            |
 * | \>`add`    | Button  | 210    | Adds a new dummy predecessor. Then need to select a task from the list in the name column editor |
 * | \>`remove` | Button  | 220    | Removes selected incoming dependency                                                             |
 *
 * \> - nested items
 *
 * ### Successors tab
 *
 * Successors tab contains a grid with outgoing dependencies and controls to remove/add dependencies
 *
 * | Widget ref | Type    | Weight | Description                                                                                    |
 * |------------|---------|--------|------------------------------------------------------------------------------------------------|
 * | `grid`     | Grid    | 100    | Shows successors task name, dependency type and lag                                            |
 * | `toolbar`  | Toolbar | 200    | Shows control buttons                                                                          |
 * | \>`add`    | Button  | 210    | Adds a new dummy successor. Then need to select a task from the list in the name column editor |
 * | \>`remove` | Button  | 220    | Removes selected outgoing dependency                                                           |
 *
 * \> - nested items
 *
 * ### Resources tab
 *
 * Resources tab contains a grid with assignments
 *
 * | Widget ref | Type    | Weight | Description                                                                                                                           |
 * |------------|---------|--------|---------------------------------------------------------------------------------------------------------------------------------------|
 * | `grid`     | Grid    | 100    | Shows assignments resource name and assigned units (100 means that the assigned resource spends 100% of its working time to the task) |
 * | `toolbar`  | Toolbar | 200    | Shows control buttons                                                                                                                 |
 * | \>`add`    | Button  | 210    | Adds a new dummy assignment. Then need to select a resource from the list in the name column editor                                   |
 * | \>`remove` | Button  | 220    | Removes selected assignment                                                                                                           |
 *
 * \> - nested items
 *
 * ### Advanced tab
 *
 * Advanced tab contains additional task scheduling options
 *
 * | Field ref                | Type      | Weight | Description                                                                                                                  |
 * |--------------------------|-----------|--------|------------------------------------------------------------------------------------------------------------------------------|
 * | `calendarField`          | Combo     | 100    | Shows a list of available calendars for this task                                                                            |
 * | `manuallyScheduledField` | Checkbox  | 200    | If checked, the task is not considered in scheduling                                                                         |
 * | `schedulingModeField`    | Combo     | 300    | Shows a list of available scheduling modes for this task                                                                     |
 * | `effortDrivenField`      | Checkbox  | 400    | If checked, the effort of the task is kept intact, and the duration is updated. Works when scheduling mode is "Fixed Units". |
 * | `divider`                | Widget    | 500    | Visual splitter between 2 groups of fields                                                                                   |
 * | `constraintTypeField`    | Combo     | 600    | Shows a list of available constraints for this task                                                                          |
 * | `constraintDateField`    | DateField | 700    | Shows a date for the selected constraint type                                                                                |
 * | `rollupField`            | Checkbox  | 800    | If checked, shows a bar below the parent task. Works when the "Rollup" feature is enabled.                                   |
 *
 * ### Notes tab
 *
 * Notes tab contains a text area to show notes
 *
 * | Field ref   | Type          | Weight | Description                                     |
 * |-------------|---------------|--------|-------------------------------------------------|
 * | `noteField` | TextAreaField | 100    | Shows a text area to add text notes to the task |
 *
 * {@endregion}
 *
 * ## Removing a built in item
 *
 * To remove a built in tab or field, specify its `ref` as `false` in the `items` config:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab      : {
 *                     items : {
 *                         // Remove "% Complete","Effort", and the divider in the "General" tab
 *                         percentDone : false,
 *                         effort      : false,
 *                         divider     : false
 *                     }
 *                 },
 *                 // Remove all tabs except the "General" tab
 *                 notesTab        : false,
 *                 predecessorsTab : false,
 *                 successorsTab   : false,
 *                 resourcesTab    : false,
 *                 advancedTab     : false
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
 * const gantt = new Gantt({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab      : {
 *                     // Rename "General" tab
 *                     title : 'Main',
 *                     items : {
 *                         // Rename "% Complete" field
 *                         percentDone : {
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
 * const gantt = new Gantt({
 *     features : {
 *         taskEdit : {
 *             items : {
 *                 generalTab : {
 *                     items : {
 *                         // Add new field to the last position
 *                         newGeneralField : {
 *                             type   : 'textfield',
 *                             weight : 710,
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
 *                             weight : 710,
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
 * const gantt = new Gantt({
 *     features : {
 *         taskEdit : false
 *     }
 * })
 * ```
 *
 * For more info on customizing the Task Editor, please see Guides/Customization/Customize task editor
 *
 * @extends SchedulerPro/feature/TaskEdit
 * @demo Gantt/taskeditor
 * @classtype taskEdit
 *
 * @typings SchedulerPro/feature/TaskEdit -> SchedulerPro/feature/SchedulerProTaskEdit
 */
export default class TaskEdit extends SchedulerProTaskEdit {

    static get $name() {
        return 'TaskEdit';
    }

    static get defaultConfig() {
        return {
            /**
             * The event that shall trigger showing the editor. Defaults to `eventdblclick`, set to `` or null to disable editing of existing events.
             * @config {String}
             * @default
             * @category Editor
             */
            triggerEvent : 'taskdblclick',

            saveAndCloseOnEnter : true,

            /**
             * A configuration object used to configure tabs of the task editor which can be used to
             * customize the built-in tabs without changing the whole {@link #config-editorConfig editorConfig}.
             *
             * @config {Object}
             * @deprecated 5.0.0 Use `items` instead
             */
            tabsConfig : null,

            /**
             * Class to use as the editor. By default it uses {@link Gantt.widget.TaskEditor TaskEditor}
             * @config {Core.widget.Widget}
             * @category Editor
             */
            editorClass : TaskEditor
        };
    }

    static get pluginConfig() {
        return {
            chain  : ['populateTaskMenu', 'onTaskEnterKey'],
            assign : ['editTask']
        };
    }

    /**
     * Shows a {@link Gantt.widget.TaskEditor TaskEditor} to edit the passed task. This function is exposed on
     * the Gantt instance and can be called as `gantt.editTask()`.
     * @param {Gantt.model.TaskModel} taskRecord Task to edit
     * @param {HTMLElement} [element] The task element
     * @return {Promise} Promise which resolves after the editor is shown
     * @async
     */
    editTask(taskRecord, element) {
        return this.editEvent(taskRecord, null, element);
    }

    onActivateEditor({ taskRecord, taskElement }) {
        this.editTask(taskRecord, taskElement);
    }

    getElementFromTaskRecord(taskRecord) {
        return this.client.getElementFromTaskRecord(taskRecord);
    }

    onTaskEnterKey({ taskRecord }) {
        this.editTask(taskRecord);
    }

    //region Context menu

    populateTaskMenu({ taskRecord, selection, items }) {
        if (!this.client.readOnly && selection.length <= 1) {
            items.editTask = {
                text        : 'L{Gantt.Edit}',
                localeClass : this.client,
                cls         : 'b-separator',
                icon        : 'b-icon b-icon-edit',
                weight      : 100,
                disabled    : this.disabled,
                onItem      : () => this.editTask(taskRecord)
            };
        }
    }

    //endregion

    onEventEnterKey({ taskRecord, target }) {
        this.editTask(taskRecord);
    }

    scrollTaskIntoView(taskRecord) {
        this.scrollEventIntoView(taskRecord);
    }

    scrollEventIntoView(eventRecord) {
        this.client.scrollTaskIntoView(eventRecord);
    }
}

GridFeatureManager.registerFeature(TaskEdit, true, 'Gantt');
