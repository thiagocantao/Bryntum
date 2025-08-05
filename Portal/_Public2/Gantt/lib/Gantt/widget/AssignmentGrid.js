import Grid from '../../Grid/view/Grid.js';
import NumberColumn from '../../Grid/column/NumberColumn.js';
import Objects from '../../Core/helper/util/Objects.js';
import AssignmentManipulationStore from '../data/AssignmentsManipulationStore.js';
import '../../Core/widget/Checkbox.js';
import '../../Grid/feature/FilterBar.js';
import '../../Gantt/localization/En.js';

/**
 * @module Gantt/widget/AssignmentGrid
 */

/**
 * This grid visualizes and lets users edit assignments of an {@link #config-projectEvent event}. Used by the
 * {@link Gantt.widget.AssignmentField}. This grid shows one column showing the resource name, and one showing
 * the units assigned. You can add additional columns by providing a {@link Grid.view.Grid#config-columns} array in your grid config.
 *
 * {@inlineexample gantt/widget/AssignmentGrid.js}
 * @extends Grid/view/Grid
 * @classType assignmentgrid
 */
export default class AssignmentGrid extends Grid {

    static get $name() {
        return 'AssignmentGrid';
    }

    // Factoryable type name
    static get type() {
        return 'assignmentgrid';
    }

    //region Config
    static get configurable() {
        return {
            /**
             * A {@link Grid.column.Column} config object for the resource column. You can pass a ´renderer` which
             * gives you access to the `resource` record.
             *
             * @config {Object}
             */
            resourceColumn : {
                cls        : 'b-assignmentgrid-resource-column',
                field      : 'resourceId',
                flex       : 1,
                editor     : null,
                renderer   : ({ record }) => record.resource?.name,
                filterable : {
                    filterField : {
                        placeholder : 'L{Name}',
                        localeClass : this,
                        triggers    : {
                            filter : {
                                align : 'start',
                                cls   : 'b-icon b-icon-filter'
                            }
                        }
                    },
                    filterFn : ({ value, record }) => {
                        return record.name.toLowerCase().indexOf(value.toLowerCase()) !== -1;
                    }
                },
                sortable : (lhs, rhs) => lhs.name < rhs.name ? -1 : lhs.name > rhs.name ? 1 : 0
            },
            /**
             * A config object for the units colunm
             *
             * @config {Object}
             */
            unitsColumn : {
                field       : 'units',
                type        : NumberColumn.type,
                text        : 'L{Units}',
                localeClass : this,
                width       : 70,
                min         : 0,
                max         : 100,
                step        : 10,
                unit        : '%',
                renderer    : ({ value }) => this.L('L{unitsTpl}', { value }),
                filterable  : false
            }
        };
    }

    static get defaultConfig() {
        return {
            selectionMode : {
                rowCheckboxSelection : true,
                multiSelect          : true,
                showCheckAll         : true
            },

            // If enabled blocks header checkbox click event
            features : {
                group       : false,
                filterBar   : true,
                contextMenu : false
            },

            disableGridRowModelWarning : true,

            /**
             * Event model to manipulate assignments of, the task should be part of a task store.
             * Either task or {@link Grid/view/Grid#config-store store} should be given.
             *
             * @config {Gantt.model.TaskModel}
             */
            projectEvent : null
        };
    }
    //endregion

    construct() {
        super.construct(...arguments);

        this.on('selectionchange', ({ selected, deselected }) => {
            selected.forEach(resourceManipulationRecord => resourceManipulationRecord.assigned = true);
            deselected.forEach(resourceManipulationRecord => resourceManipulationRecord.assigned = false);
        });
    }

    get projectEvent() {
        const me = this,
            store = me.store;

        let projectEvent = me._projectEvent;

        if (store && (projectEvent !== store.projectEvent)) {
            projectEvent = me._projectEvent = store.projectEvent;
        }

        return projectEvent;
    }

    set projectEvent(projectEvent) {
        this._projectEvent = projectEvent;

        this.store.projectEvent = projectEvent;

        this.selectedRecords = this.store.originalAssignmentManipulationRecords;
    }

    get store() {
        return super.store;
    }

    set store(store) {
        const me = this,
            oldStore = me.store;

        if (oldStore !== store) {
            if (!(store instanceof AssignmentManipulationStore)) {
                store = new AssignmentManipulationStore(Object.assign({
                    projectEvent : me._projectEvent
                }, store));
            }

            super.store = store;
        }
    }

    set columns(columns) {
        if (columns) {
            // Clone is needed to flatten the properties from the prototype chain, the Model class wants data
            // in a flat simple object
            columns.unshift(Objects.clone(this.resourceColumn), Objects.clone(this.unitsColumn));
        }

        super.columns = columns;
    }

    get columns() {
        return super.columns;
    }
}

// Register this widget type with its Factory
AssignmentGrid.initClass();
