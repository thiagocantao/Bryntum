import DependencyTab from './DependencyTab.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/taskeditor/SuccessorsTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the successors of a task.
 * @internal
 */
export default class SuccessorsTab extends DependencyTab {

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

BryntumWidgetAdapterRegister.register(SuccessorsTab.type, SuccessorsTab);
