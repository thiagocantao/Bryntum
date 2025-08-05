import DependencyTab from './DependencyTab.js';

/**
 * @module SchedulerPro/widget/taskeditor/PredecessorsTab
 */

/**
 * A tab inside the {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro.widget.GanttTaskEditor gantt task editor} showing the predecessors of an event or task.
 *
 * | Widget ref | Type    | Weight | Description                                                                                      |
 * |------------|---------|--------|--------------------------------------------------------------------------------------------------|
 * | `grid`     | Grid    | 100    | Shows predecessors task name, dependency type and lag                                            |
 * | `toolbar`  | Toolbar | 200    | Shows control buttons                                                                            |
 * | \>`add`    | Button  | 210    | Adds a new dummy predecessor. Then need to select a task from the list in the name column editor |
 * | \>`remove` | Button  | 220    | Removes selected incoming dependency                                                             |
 *
 * @extends SchedulerPro/widget/taskeditor/DependencyTab
 * @classtype predecessorstab
 */
export default class PredecessorsTab extends DependencyTab {
    static get $name() {
        return 'PredecessorsTab';
    }

    // Factoryable type name
    static get type() {
        return 'predecessorstab';
    }

    static get defaultConfig() {
        return Object.assign(
            this.makeDefaultConfig('fromEvent'),
            {
                cls : 'b-predecessors-tab'
            }
        );
    }
}

// Register this widget type with its Factory
PredecessorsTab.initClass();
