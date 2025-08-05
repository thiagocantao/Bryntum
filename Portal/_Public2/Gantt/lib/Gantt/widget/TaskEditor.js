import GanttTaskEditor from '../../SchedulerPro/widget/GanttTaskEditor.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Gantt/widget/TaskEditor
 */

/**
 * Provides a UI to edit tasks in a popup dialog. It is implemented as a Tab Panel with
 * several preconfigured built-in tabs. Although the default configuration may be adequate
 * in many cases, the Task Editor is easily configurable.
 *
 * To hide built-in tabs or to add custom tabs, or to append widgets to any of the built-in tabs
 * use the {@link Gantt.feature.TaskEdit#config-items items} config.
 *
 * The Task editor contains tabs by default. Each tab is a container with built in widgets: text fields, grids, etc.
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
 *
 * ## Task editor customization example
 *
 * {@inlineexample gantt/feature/TaskEditCustom.js}
 *
 * @externalexample gantt/widget/TaskEditor.js
 * @extends SchedulerPro/widget/GanttTaskEditor
 */
export default class TaskEditor extends GanttTaskEditor {
    // Factoryable type name
    static get type() {
        return 'taskeditor';
    }

    static get $name() {
        return 'TaskEditor';
    }

    static get defaultConfig() {
        return {
            cls : 'b-gantt-taskeditor b-schedulerpro-taskeditor'
        };
    }

    afterConfigure() {
        super.afterConfigure();

        if ('durationDecimalPrecision' in this) {
            VersionHelper.deprecate('gantt', '5.0.0', '`durationDecimalPrecision` is deprecated, in favor of `durationDisplayPrecision`');
        }
    }
}

// Register this widget type with its Factory
TaskEditor.initClass();
