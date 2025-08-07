import Toolbar from '../../../lib/Core/widget/Toolbar.js';
import Toast from '../../../lib/Core/widget/Toast.js';
import DateHelper from '../../../lib/Core/helper/DateHelper.js';
import CSSHelper from '../../../lib/Core/helper/CSSHelper.js';
import '../../../lib/Scheduler/widget/UndoRedo.js';

/**
 * @module GanttToolbar
 */

/**
 * @extends Core/widget/Toolbar
 */
export default class GanttToolbar extends Toolbar {
    // Factoryable type name
    static get type() {
        return 'gantttoolbar';
    }

    static get $name() {
        return 'GanttToolbar';
    }

    static get configurable() {
        return {
            items : [
                {
                    type  : 'buttonGroup',
                    items : [
                        {
                            color    : 'b-blue',
                            ref      : 'editButton',
                            icon     : 'b-fa b-fa-edit',
                            text     : 'Editar',
                            tooltip  : 'Habilitar edição',
                            onAction : 'up.onEnableEditClick'
                        }
                    ]
                },
                {
                    type  : 'buttonGroup',
                    items : [
                        {
                            color    : 'b-green',
                            ref      : 'addTaskButton',
                            icon     : 'b-fa b-fa-plus',
                            text     : 'Create',
                            tooltip  : 'Create new task',
                            onAction : 'up.onAddTaskClick',
                            disabled : true
                        }
                    ]
                },
                {
                    ref   : 'undoRedo',
                    type  : 'undoredo',
                    items : {
                        transactionsCombo : null
                    }
                },
                {
                    type  : 'buttonGroup',
                    items : [
                        {
                            ref      : 'expandAllButton',
                            icon     : 'b-fa b-fa-angle-double-down',
                            tooltip  : 'Expand all',
                            onAction : 'up.onExpandAllClick'
                        },
                        {
                            ref      : 'collapseAllButton',
                            icon     : 'b-fa b-fa-angle-double-up',
                            tooltip  : 'Collapse all',
                            onAction : 'up.onCollapseAllClick'
                        }
                    ]
                },
                {
                    type  : 'buttonGroup',
                    items : [
                        {
                            ref      : 'zoomInButton',
                            icon     : 'b-fa b-fa-search-plus',
                            tooltip  : 'Zoom in',
                            onAction : 'up.onZoomInClick'
                        },
                        {
                            ref      : 'zoomOutButton',
                            icon     : 'b-fa b-fa-search-minus',
                            tooltip  : 'Zoom out',
                            onAction : 'up.onZoomOutClick'
                        },
                        {
                            ref      : 'zoomToFitButton',
                            icon     : 'b-fa b-fa-compress-arrows-alt',
                            tooltip  : 'Zoom to fit',
                            onAction : 'up.onZoomToFitClick'
                        },
                        {
                            ref      : 'previousButton',
                            icon     : 'b-fa b-fa-angle-left',
                            tooltip  : 'Previous time span',
                            onAction : 'up.onShiftPreviousClick'
                        },
                        {
                            ref      : 'nextButton',
                            icon     : 'b-fa b-fa-angle-right',
                            tooltip  : 'Next time span',
                            onAction : 'up.onShiftNextClick'
                        }
                    ]
                },
                {
                    type      : 'datefield',
                    ref       : 'startDateField',
                    label     : 'Project start',
                    // required  : true, (done on load)
                    flex      : '0 0 18em',
                    listeners : {
                        change : 'up.onStartDateChange'
                    }
                },
                {
                    type         : 'combo',
                    ref          : 'projectSelector',
                    label        : 'Choose project',
                    editable     : false,
                    width        : '25em',
                    displayField : 'name',
                    value        : 1,
                    store        : {
                        data : [
                            {
                                id   : 1,
                                name : 'Launch SaaS',
                                url  : '../_datasets/launch-saas.json'
                            },
                            {
                                id   : 2,
                                name : 'Build web app for customer',
                                url  : '../_datasets/tasks-workedhours.json'
                            }
                        ]
                    },
                    listeners : {
                        select : 'up.onProjectSelected'
                    }
                },
                //'->',
                //{
                //    type                 : 'textfield',
                //    ref                  : 'filterByName',
                //    cls                  : 'filter-by-name',
                //    flex                 : '0 0 13.5em',
                //    // Label used for material, hidden in other themes
                //    label                : 'Find tasks by name',
                //    // Placeholder for others
                //    placeholder          : 'Find tasks by name',
                //    clearable            : true,
                //    keyStrokeChangeDelay : 100,
                //    triggers             : {
                //        filter : {
                //            align : 'end',
                //            cls   : 'b-fa b-fa-filter'
                //        }
                //    },
                //    onChange : 'up.onFilterChange'
                //},
                {
                    type    : 'button',
                    ref     : 'featuresButton',
                    icon    : 'b-fa b-fa-tasks',
                    text    : 'Settings',
                    tooltip : 'Toggle features',
                    menu    : {
                        onItem       : 'up.onFeaturesClick',
                        onBeforeShow : 'up.onFeaturesShow',
                        // "checked" is set to a boolean value to display a checkbox for menu items. No matter if it is true or false.
                        // The real value is set dynamically depending on the "disabled" config of the feature it is bound to.
                        items        : [
                            {
                                text : 'UI settings',
                                icon : 'b-fa-sliders-h',
                                menu : {
                                    cls         : 'settings-menu',
                                    layoutStyle : {
                                        flexDirection : 'column'
                                    },
                                    onBeforeShow : 'up.onSettingsShow',
                                    defaults     : {
                                        type      : 'slider',
                                        showValue : true
                                    },
                                    items : [
                                        {
                                            ref     : 'rowHeight',
                                            text    : 'Row height',
                                            min     : 30,
                                            max     : 70,
                                            onInput : 'up.onRowHeightChange'
                                        },
                                        {
                                            ref     : 'barMargin',
                                            text    : 'Bar margin',
                                            min     : 0,
                                            max     : 10,
                                            onInput : 'up.onBarMarginChange'
                                        },
                                        {
                                            ref     : 'duration',
                                            text    : 'Animation duration',
                                            min     : 0,
                                            max     : 2000,
                                            step    : 100,
                                            onInput : 'up.onAnimationDurationChange'
                                        },
                                        {
                                            ref     : 'radius',
                                            text    : 'Dependency radius',
                                            min     : 0,
                                            max     : 10,
                                            onInput : 'up.onDependencyRadiusChange'
                                        }
                                    ]
                                }
                            },
                            {
                                text    : 'Draw dependencies',
                                feature : 'dependencies',
                                checked : false
                            },
                            {
                                text    : 'Task labels',
                                feature : 'labels',
                                checked : false
                            },
                            {
                                text    : 'Critical paths',
                                feature : 'criticalPaths',
                                tooltip : 'Highlight critical paths',
                                checked : false
                            },
                            {
                                text    : 'Project lines',
                                feature : 'projectLines',
                                checked : false
                            },
                            {
                                text    : 'Highlight non-working time',
                                feature : 'nonWorkingTime',
                                checked : false
                            },
                            {
                                text    : 'Enable cell editing',
                                feature : 'cellEdit',
                                checked : false
                            },
                            {
                                text    : 'Show column lines',
                                feature : 'columnLines',
                                checked : true
                            },
                            {
                                text    : 'Show baselines',
                                feature : 'baselines',
                                checked : false
                            },
                            {
                                text    : 'Show rollups',
                                feature : 'rollups',
                                checked : false
                            },
                            {
                                text    : 'Show progress line',
                                feature : 'progressLine',
                                checked : false
                            },
                            {
                                text    : 'Show parent area',
                                feature : 'parentArea',
                                checked : false
                            },
                            {
                                text         : 'Stretch tasks to fill ticks',
                                toggleConfig : 'fillTicks',
                                checked      : false
                            },
                            {
                                text    : 'Hide schedule',
                                cls     : 'b-separator',
                                subGrid : 'normal',
                                checked : false
                            }
                        ]
                    }
                }
            ]
        };
    }

    construct() {
        super.construct(...arguments);

        this.gantt = this.parent;
        this.gantt.project.on({
            load    : 'updateStartDateField',
            refresh : 'updateStartDateField',
            thisObj : this
        });

        this.styleNode = document.createElement('style');
        document.head.appendChild(this.styleNode);
    }

    setAnimationDuration(value) {
        const
            me      = this,
            cssText = `.b-animating .b-gantt-task-wrap { transition-duration: ${value / 1000}s !important; }`;

        me.gantt.transitionDuration = value;

        if (me.transitionRule) {
            me.transitionRule.cssText = cssText;
        }
        else {
            me.transitionRule = CSSHelper.insertRule(cssText);
        }
    }

    updateStartDateField() {
        const { startDateField } = this.widgetMap;

        startDateField.value = this.gantt.project.startDate;

        // This handler is called on project.load/propagationComplete, so now we have the
        // initial start date. Prior to this time, the empty (default) value would be
        // flagged as invalid.
        startDateField.required = true;
    }

    // region controller methods

    onEnableEditClick() {
        this.gantt.readOnly = false;
        this.widgetMap.addTaskButton.disabled = false;
        this.widgetMap.editButton.disabled = true;
    }

    async onAddTaskClick() {
        const { gantt } = this,
            parent = gantt.selectedRecord || gantt.taskStore.rootNode,
            added = parent.appendChild({ name : this.L('New task'), duration : 1 });

        await gantt.project.commitAsync();
        gantt.editTask(added);
    }

    onEditTaskClick() {
        const { gantt } = this;

        if (gantt.selectedRecord) {
            gantt.editTask(gantt.selectedRecord);
        }
        else {
            Toast.show(this.L('First select the task you want to edit'));
        }
    }

    onExpandAllClick() {
        this.gantt.expandAll();
    }

    onCollapseAllClick() {
        this.gantt.collapseAll();
    }

    onZoomInClick() {
        this.gantt.zoomIn();
    }

    onZoomOutClick() {
        this.gantt.zoomOut();
    }

    onZoomToFitClick() {
        this.gantt.zoomToFit({
            leftMargin  : 50,
            rightMargin : 50
        });
    }

    onShiftPreviousClick() {
        this.gantt.shiftPrevious();
    }

    onShiftNextClick() {
        this.gantt.shiftNext();
    }

    onStartDateChange({ value, oldValue }) {
        if (value) {
            this.gantt.scrollToDate(DateHelper.add(value, -1, 'week'), {
                block : 'start'
            });

            this.gantt.project.setStartDate(value);
        }
    }

    onProjectSelected({ record }) {
        this.gantt.project.load(record.url);
    }

    onFilterChange({ value }) {
        if (value === '') {
            this.gantt.taskStore.clearFilters();
        }
        else {
            value = value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

            this.gantt.taskStore.filter({
                filters : task => task.name && task.name.match(new RegExp(value, 'i')),
                replace : true
            });
        }
    }

    onFeaturesClick({ source : item }) {
        const { gantt } = this;

        if (item.feature) {
            const feature = gantt.features[item.feature];
            feature.disabled = !feature.disabled;
        }
        else if (item.subGrid) {
            const subGrid = gantt.subGrids[item.subGrid];
            subGrid.collapsed = !subGrid.collapsed;
        }
        else if (item.toggleConfig) {
            gantt[item.toggleConfig] = item.checked;
        }
    }

    onFeaturesShow({ source : menu }) {
        const { gantt } = this;

        menu.items.map(item => {
            const { feature } = item;

            if (feature) {
                // a feature might be not presented in the gantt
                // (the code is shared between "advanced" and "php" demos which use a bit different set of features)
                if (gantt.features[feature]) {
                    item.checked = !gantt.features[feature].disabled;
                }
                // hide not existing features
                else {
                    item.hide();
                }
            }
            else if (item.subGrid) {
                item.checked = gantt.subGrids[item.subGrid].collapsed;
            }
        });
    }

    onSettingsShow({ source : menu }) {
        const
            { gantt }                                  = this,
            { rowHeight, barMargin, duration, radius } = menu.widgetMap;

        rowHeight.value = gantt.rowHeight;
        barMargin.value = gantt.barMargin;
        barMargin.max = (gantt.rowHeight / 2) - 5;
        duration.value = gantt.transitionDuration;
        radius.value = gantt.features.dependencies.radius ?? 0;
    }

    onRowHeightChange({ value, source }) {
        this.gantt.rowHeight = value;
        source.owner.widgetMap.barMargin.max = (value / 2) - 5;
    }

    onBarMarginChange({ value }) {
        this.gantt.barMargin = value;
    }

    onAnimationDurationChange({ value }) {
        this.gantt.transitionDuration = value;
        this.styleNode.innerHTML = `.b-animating .b-gantt-task-wrap { transition-duration: ${value / 1000}s !important; }`;
    }

    onDependencyRadiusChange({ value }) {
        this.gantt.features.dependencies.radius = value;
    }

    onCriticalPathsClick({ source }) {
        this.gantt.features.criticalPaths.disabled = !source.pressed;
    }

    // endregion
};

// Register this widget type with its Factory
GanttToolbar.initClass();
