import DependencyTab from './DependencyTab.js';

/**
 * @module SchedulerPro/widget/taskeditor/SuccessorsTab
 */

/**
 * A tab inside the {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro.widget.GanttTaskEditor gantt task editor} showing the successors of an event or task.
 *
 * | Widget ref | Type    | Weight | Description                                                                                    |
 * |------------|---------|--------|------------------------------------------------------------------------------------------------|
 * | `grid`     | Grid    | 100    | Shows successors task name, dependency type and lag                                            |
 * | `toolbar`  | Toolbar | 200    | Shows control buttons                                                                          |
 * | \>`add`    | Button  | 210    | Adds a new dummy successor. Then need to select a task from the list in the name column editor |
 * | \>`remove` | Button  | 220    | Removes selected outgoing dependency                                                           |
 *
 * @extends SchedulerPro/widget/taskeditor/DependencyTab
 * @classtype successorstab
 */
export default class SuccessorsTab extends DependencyTab {
    static get $name() {
        return 'SuccessorsTab';
    }

    // Factoryable type name
    static get type() {
        return 'successorstab';
    }

    static get defaultConfig() {
        return Object.assign(
            this.makeDefaultConfig('toEvent'),
            {
                cls : 'b-successors-tab'
            }
        );
    }
}

// Register this widget type with its Factory
SuccessorsTab.initClass();
