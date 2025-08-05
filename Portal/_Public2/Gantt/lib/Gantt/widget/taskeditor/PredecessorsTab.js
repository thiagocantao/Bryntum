import DependencyTab from './DependencyTab.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/taskeditor/PredecessorsTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the predecessors of a task.
 * @internal
 */
export default class PredecessorsTab extends DependencyTab {
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

BryntumWidgetAdapterRegister.register(PredecessorsTab.type, PredecessorsTab);
