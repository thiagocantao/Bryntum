import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import TaskMenu from '../feature/TaskMenu.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Gantt/feature/TaskContextMenu
 */

/**
 * **DEPRECATED: This class is deprecated since v4.0.0,
 * use {@link Gantt.feature.TaskMenu TaskMenu} feature instead.**
 *
 * Displays a context menu for tasks. Items are populated by other features and/or application code.
 * If enabled, {@link Grid.feature.CellMenu} feature is not available. Cell context menu items are handled by this feature.
 *
 * This feature is **disabled** by default in favor of `TaskMenu`.
 *
 * @deprecated 4.0.0
 * @extends Gantt/feature/TaskMenu
 * @classtype taskContextMenu
 */
export default class TaskContextMenu extends TaskMenu {
    //region Config

    static get $name() {
        return 'TaskContextMenu';
    }

    construct(gantt, config) {
        super.construct(...arguments);

        VersionHelper.deprecate(
            'Gantt',
            '5.0.0',
            '`TaskContextMenu` feature is deprecated, in favor of `TaskMenu` feature. Please see https://bryntum.com/docs/gantt/#guides/upgrades/4.0.0.md#context-menu for more information.'
        );

        // If this feature is enabled, need to disable TaskMenu. They are mutually exclusive.
        if (gantt.features.taskMenu) {
            gantt.features.taskMenu.disabled = true;
        }

        // If deprecated ContextMenu is used
        if (gantt.features.contextMenu) {
            gantt.showRemoveRowInContextMenu = false;
            gantt.features.contextMenu.disableCellContextMenu = true;
        }
    }

    //endregion

    //region Events

    /**
     * Fired from gantt before the context menu is shown for a task. Allows manipulation of the items
     * to show in the same way as in `processItems`. Returning false from a listener prevents the menu from
     * being shown.
     * @event taskContextMenuBeforeShow
     * @deprecated 4.0.0 Renamed to taskMenuBeforeShow
     * @preventable
     * @param {Gantt.view.Gantt} source
     * @param {Object[]} items Menu item configs
     * @param {Gantt.model.TaskModel} taskRecord Event record for which the menu was triggered
     * @param {HTMLElement} taskElement
     */

    /**
     * Fired from gantt when an item is selected in the context menu.
     * @event taskContextMenuItem
     * @deprecated 4.0.0 Renamed to taskMenuItem
     * @param {Gantt.view.Gantt} source
     * @param {Core.widget.MenuItem} item
     * @param {Gantt.model.TaskModel} taskRecord
     * @param {HTMLElement} taskElement
     */

    /**
     * Fired from gantt after showing the context menu for an event
     * @event taskContextMenuShow
     * @deprecated 4.0.0 Renamed to taskMenuShow
     * @param {Gantt.view.Gantt} source
     * @param {Core.widget.Menu} menu The menu
     * @param {Gantt.model.TaskModel} taskRecord Event record for which the menu was triggered
     * @param {HTMLElement} taskElement
     */

    //endregion

    /**
     * Returns Task record associated with current element (row/cell/task element)
     * @param {HTMLElement} element
     * @returns {Gantt.model.TaskModel}
     * @private
     */
    resolveRecord(element) {
        // We may be asked to resolve from a task bar element
        // or a regular grid inner element. Both must lead
        // to the Task.
        return this.client.resolveTaskRecord(element) || this.client.getRecordFromElement(element);
    }

    /**
     * Shows context menu for the provided task. If task is not rendered (outside of time span, or collapsed)
     * menu won't appear.
     * @param {Gantt.model.TaskModel} taskRecord
     * @param {Object} [options]
     * @param {HTMLElement} options.targetElement Element to align context menu to
     * @param {HTMLElement} options.eventElement Event element if target is an event, otherwise `null`
     * @param {Event} options.event Browser event. If provided menu will be aligned according to clientX/clientY coordinates.
     * If omitted, context menu will be centered to taskElement
     * @typings Scheduler/feature/EventMenu:showContextMenuFor:eventRecord -> {Scheduler.model.EventModel|Gantt.model.TaskModel}
     */
    showContextMenuFor(taskRecord, { targetElement, eventElement, event } = {}) {
        const { client } = this;

        if (!taskRecord) {
            return;
        }

        if (!targetElement) {
            targetElement = client.getElementFromTaskRecord(taskRecord);

            // If task record is not rendered, do nothing
            if (!targetElement) {
                return;
            }
        }

        // If it was a click
        if (event) {
            const cellData = client.getEventData(event);

            // If click occurs on row border there won't be cell data
            if (!cellData) {
                return;
            }

            const column = client.columns.getById(cellData.columnId);

            // If target is not a task and column has a flag to disable context menu
            if (!eventElement && column && !column.enableCellContextMenu) {
                return;
            }
        }

        this.showContextMenu({
            menuType    : 'task',
            taskElement : targetElement,
            targetElement,
            taskRecord,
            event
        });
    }
}

TaskContextMenu.featureClass = '';

GridFeatureManager.registerFeature(TaskContextMenu);
