import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import EventMenu from '../../Scheduler/feature/EventMenu.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module Gantt/feature/TaskMenu
 */

/**
 * Displays a context menu for tasks. Items are populated by other features and/or application code.
 * Configure it with `false` to disable it completely. If enabled, {@link Grid.feature.CellMenu} feature
 * is not available. Cell context menu items are handled by this feature.
 *
 * ### Default task menu items
 *
 * Here is the list of menu items provided by the Task menu feature and populated by the other features:
 *
 * | Item reference        | Text                 | Weight | Feature    | Enabled by default | Description                                                                                                                                           |
 * |-----------------------|----------------------|--------|------------|--------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
 * | `editTask`            | Edit task            | 100    | `TaskEdit` | true               | Shows a submenu to control tasks adding                                                                                                               |
 * | `search`*             | Search for value     | 200    | `Search`   | false              | Searches the grid for the selected cell text                                                                                                          |
 * | `filterDateEquals`*   | On                   | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value                                                                         |
 * | `filterDateBefore`*   | Before               | 310    | `Filter`   | false              | Filters records in the store by the column field less than selected cell value                                                                        |
 * | `filterDateAfter`*    | After                | 320    | `Filter`   | false              | Filters records in the store by the column field more than selected cell value                                                                        |
 * | `filterNumberEquals`* | Equals               | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value                                                                         |
 * | `filterNumberLess`*   | Less than            | 310    | `Filter`   | false              | Filters records in the store by the column field less than selected cell value                                                                        |
 * | `filterNumberMore`*   | More than            | 320    | `Filter`   | false              | Filters records in the store by the column field more than selected cell value                                                                        |
 * | `filterStringEquals`* | Equals               | 300    | `Filter`   | false              | Filters records in the store by the column field equal to selected cell value                                                                         |
 * | `filterRemove`*       | Remove filter        | 400    | `Filter`   | false              | Stops filtering by selected column field                                                                                                              |
 * | `add`                 | Add...               | 500    | `TaskMenu` | true               | Shows a submenu to control tasks adding                                                                                                               |
 * | \>`addTaskAbove`      | Task above           | 510    | `TaskMenu` | true               | Adds a new task above the selected task                                                                                                               |
 * | \>`addTaskBelow`      | Task below           | 520    | `TaskMenu` | true               | Adds a new task below the selected task                                                                                                               |
 * | \>`milestone`         | Milestone            | 530    | `TaskMenu` | true               | Adds a new milestone below the selected task                                                                                                          |
 * | \>`subtask`           | Subtask              | 540    | `TaskMenu` | true               | Turns the selected task into a parent task if it is not a parent task yet. Adds a new task as a child of the selected task.                           |
 * | \>`successor`         | Successor            | 550    | `TaskMenu` | true               | Adds a new task below the selected task. Creates an "end-to-start" dependency between the selected task and the new one.                              |
 * | \>`predecessor`       | Predecessor          | 560    | `TaskMenu` | true               | Adds a new task above the selected task. Creates an "end-to-start" dependency between the new task and the selected one.                              |
 * | `convertToMilestone`  | Convert to milestone | 600    | `TaskMenu` | true               | Turns the selected task into a milestone. Shown for leaf tasks only.                                                                                  |
 * | `indent`              | Indent               | 700    | `TaskMenu` | true               | Turns the sibling above of the selected task into a parent task if it is not a parent task yet. The selected task becomes a child of the parent task. |
 * | `outdent`             | Outdent              | 800    | `TaskMenu` | true               | Turns the selected task into a sibling of its parent which goes next to it                                                                            |
 * | `deleteTask`          | Delete task          | 900    | `TaskMenu` | true               | Removes the selected task record from the store                                                                                                       |
 *
 * \* - items that are shown for the locked grid cells only
 *
 * \> - first level of submenu
 *
 * ### Customizing the menu items
 *
 * The menu items in the Task menu can be customized, existing items can be changed or removed,
 * and new items can be added. This is handled using the `items` config of the feature.
 *
 * To add extra items for all events:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         taskMenu : {
 *             // Extra items for all events
 *             items : {
 *                 flagTask : {
 *                     text : 'Extra',
 *                     icon : 'b-fa b-fa-fw b-fa-flag',
 *                     onItem({taskRecord}) {
 *                         taskRecord.flagged = true;
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Manipulate existing items for all tasks or specific tasks:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         taskMenu : {
 *             // We would like to remove some of the provided options in the add menu
 *             items : {
 *                 add : {
 *                     menu : {
 *                         items : {
 *                             addTaskAbove : false,
 *                             addTaskBelow : false,
 *                             milestone    : false
 *                         }
 *                     }
 *                 }
 *             },
 *             // Process items before menu is shown
 *             processItems({taskRecord, items}) {
 *                  // Push an extra item for conferences
 *                  if (taskRecord.type === 'conference') {
 *                      items.showSessions = {
 *                          text : 'Show sessions',
 *                          ontItem({taskRecord}) {
 *                              // ...
 *                          }
 *                      };
 *                  }
 *
 *                  // Do not show menu for secret events
 *                  if (taskRecord.type === 'secret') {
 *                      return false;
 *                  }
 *             }
 *         }
 *     }
 * });
 * ```
 *
 * Full information of the menu customization can be found in the "Customizing the Task menu" guide.
 *
 * This feature is **enabled** by default
 *
 * @extends Scheduler/feature/EventMenu
 * @demo Gantt/taskmenu
 * @classtype taskMenu
 * @externalexample gantt/taskmenu.js
 */
export default class TaskMenu extends EventMenu {
    //region Config

    static get $name() {
        return 'TaskMenu';
    }

    static get defaultConfig() {
        return {
            type : 'task'

            /**
             * This is a preconfigured set of items used to create the default context menu.
             *
             * The `items` provided by this feature are listed below. These are the property names which you may
             * configure in the feature's {@link Grid.feature.base.ContextMenuBase#config-items} config:
             *
             * - `add` A submenu option containing a `menu` config which contains the following named items:
             *     * `addTaskAbove` Inserts a sibling task above the context task.
             *     * `addTaskBelow` Inserts a sibling task below the context task.
             *     * `milestone` Inserts a sibling milestone below the context task.
             *     * `subtask` Appends a child task to the context task.
             *     * `successor` Adds a sibling task linked by a dependence below the context task.
             *     * `predecessor` Adds a sibling task linked by a dependence above the context task.
             *  - `deleteTask` Deletes the context task.
             *  - `indent` Indents the context task by adding it as a child of its previous sibling.
             *  - `outdent` Outdents the context task by adding it as the final sibling of its parent.
             *  - `convertToMilestone` Converts the context task to a zero duration milestone.
             *
             * See the feature config in the above example for details.
             *
             * @config {Object} items
             */
        };
    }

    static get pluginConfig() {
        const config = super.pluginConfig;

        config.chain.push('populateTaskMenu');

        return config;
    }

    //endregion

    construct(gantt, config) {
        super.construct(...arguments);

        this.gantt = gantt;

        if (gantt.features.cellMenu) {
            console.warn('`CellMenu` feature is ignored, when `TaskMenu` feature is enabled. If you need cell specific menu items, please configure `TaskMenu` feature items instead.');
            gantt.features.cellMenu.disabled = true;
        }
    }

    //region Events

    /**
     * Fired from gantt before the context menu is shown for a task. Allows manipulation of the items
     * to show in the same way as in `processItems`. Returning false from a listener prevents the menu from
     * being shown.
     * @event taskMenuBeforeShow
     * @preventable
     * @param {Gantt.view.Gantt} source
     * @param {Object[]} items Menu item configs
     * @param {Gantt.model.TaskModel} taskRecord Event record for which the menu was triggered
     * @param {HTMLElement} taskElement
     */

    /**
     * Fired from gantt when an item is selected in the context menu.
     * @event taskMenuItem
     * @param {Gantt.view.Gantt} source
     * @param {Core.widget.MenuItem} item
     * @param {Gantt.model.TaskModel} taskRecord
     * @param {HTMLElement} taskElement
     */

    /**
     * Fired from gantt after showing the context menu for an event
     * @event taskMenuShow
     * @param {Gantt.view.Gantt} source
     * @param {Core.widget.Menu} menu The menu
     * @param {Gantt.model.TaskModel} taskRecord Event record for which the menu was triggered
     * @param {HTMLElement} taskElement
     */

    //endregion

    getDataFromEvent(event) {
        const
            { client }    = this,
            targetElement = this.getTargetElementFromEvent(event),
            // to resolve record from a task element or from a grid cell
            taskRecord    = client.resolveTaskRecord(targetElement) || client.getRecordFromElement(targetElement),
            taskElement   = taskRecord && client.getElementFromTaskRecord(taskRecord, false), // get wrapper
            cellData      = client.getCellDataFromEvent(event);

        return ObjectHelper.assign({
            event,
            targetElement,
            taskElement,
            taskRecord
        }, cellData);
    }

    callChainablePopulateMenuMethod(eventParams) {
        // When context menu is called for a task cell, need to collect items from features
        // which usually add items to CellMenu in Grid and Scheduler,
        // since CellMenu feature is disabled when TaskMenu feature is enabled.
        if (eventParams.cellData && this.client.populateCellMenu) {
            this.client.populateCellMenu(eventParams);
        }

        super.callChainablePopulateMenuMethod(...arguments);
    }

    shouldShowMenu(eventParams) {
        const { column } = eventParams;

        return eventParams.taskRecord && (!column || column.enableCellContextMenu !== false);
    }

    getElementFromRecord(record) {
        return this.client.getElementFromTaskRecord(record);
    }

    populateTaskMenu({ items, column, selection, taskRecord }) {
        const
            { client }    = this,
            // Context menu on the selection offers multi actions on the selection.
            // Context menu on a non-selected record offers single actions on the context record.
            multiSelected = selection.includes(taskRecord) && selection.length > 1;

        Object.assign(items,  {
            add : {
                text   : 'L{Gantt.Add}',
                cls    : 'b-separator',
                icon   : 'b-icon-add',
                hidden : multiSelected,
                weight : 500,
                menu   : {
                    addTaskAbove : {
                        text : 'L{Gantt.Task above}',
                        weight : 510,
                        icon : 'b-icon-up',
                        onItem({ taskRecord }) {
                            client.addTaskAbove(taskRecord);
                        }
                    },
                    addTaskBelow : {
                        text : 'L{Gantt.Task below}',
                        weight : 520,
                        icon : 'b-icon-down',
                        onItem({ taskRecord }) {
                            client.addTaskBelow(taskRecord);
                        }
                    },
                    milestone : {
                        text : 'L{Gantt.Milestone}',
                        weight : 530,
                        icon : 'b-fa-flag',
                        name : 'milestone',
                        onItem({ taskRecord }) {
                            client.addMilestonBelow(taskRecord);
                        }
                    },
                    subtask : {
                        text : 'L{Gantt.Sub-task}',
                        weight : 540,
                        name : 'subtask',
                        onItem({ taskRecord }) {
                            client.addSubtask(taskRecord);
                        }
                    },
                    successor : {
                        text : 'L{Gantt.Successor}',
                        weight : 550,
                        onItem({ taskRecord }) {
                            client.addSuccessor(taskRecord);
                        }
                    },
                    predecessor : {
                        text : 'L{Gantt.Predecessor}',
                        weight : 560,
                        name : 'predecessor',
                        onItem({ taskRecord }) {
                            client.addPredecessor(taskRecord);
                        }
                    }
                }
            },
            convertToMilestone : {
                icon   : 'b-icon-milestone',
                text   : 'L{Gantt.Convert to milestone}',
                weight : 600,
                hidden : taskRecord.isParent || taskRecord.milestone,
                onItem({ taskRecord }) {
                    taskRecord.convertToMilestone();
                }
            },
            indent : {
                text     : 'L{Gantt.Indent}',
                icon     : 'b-fa-indent',
                disabled : !taskRecord.previousSibling,
                weight   : 700,
                onItem({ selection, taskRecord }) {
                    // Context menu on the selection offers multi actions on the selection.
                    // Context menu on a non-selected record offers single actions on the context record.
                    client.indent(selection.includes(taskRecord) ? selection : taskRecord);
                }
            },
            outdent : {
                text     : 'L{Gantt.Outdent}',
                icon     : 'b-fa-outdent',
                disabled : taskRecord.parent === client.taskStore.rootNode,
                weight   : 800,
                onItem({ selection, taskRecord }) {
                    // Context menu on the selection offers multi actions on the selection.
                    client.outdent(selection.includes(taskRecord) ? selection : taskRecord);
                }
            },
            deleteTask : {
                text   : 'L{Gantt.Delete task}',
                icon   : 'b-icon-trash',
                name   : 'deleteTask',
                cls    : 'b-separator',
                weight : 900,
                onItem({ selection, taskRecord }) {
                    // Context menu on the selection offers multi actions on the selection.
                    // Context menu on a non-selected record offers single actions on the context record.
                    client.taskStore.remove(selection.includes(taskRecord) ? selection : taskRecord);
                }
            }
        });

        // TaskMenu feature is responsible for cell items
        if (column?.cellMenuItems) {
            ObjectHelper.merge(items, column.cellMenuItems);
        }
    }

}

TaskMenu.featureClass = '';

GridFeatureManager.registerFeature(TaskMenu, true, 'Gantt');
