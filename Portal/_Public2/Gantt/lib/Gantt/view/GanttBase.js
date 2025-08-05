import TimelineBase from '../../Scheduler/view/TimelineBase.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

import NewTaskRendering from './orientation/NewTaskRendering.js';

import CrudManagerView from '../../Scheduler/crud/mixin/CrudManagerView.js';
import GanttDom from './mixin/GanttDom.js';
import GanttRegions from './mixin/GanttRegions.js';
import GanttScroll from './mixin/GanttScroll.js';
import GanttState from './mixin/GanttState.js';
import GanttStores from './mixin/GanttStores.js';
import GanttTimelineDateMapper from './mixin/GanttTimelineDateMapper.js';
import TaskNavigation from './mixin/TaskNavigation.js';

import EventNavigation from '../../Scheduler/view/mixin/EventNavigation.js';

import '../localization/En.js';
import '../../Gantt/column/TimeAxisColumn.js';

// Always required features
import '../../Grid/feature/Tree.js';
import '../../Grid/feature/RegionResize.js';
import '../feature/Dependencies.js';

import NameColumn from '../column/NameColumn.js';

//import { ReactiveTimeAxis } from '../../Engine/view/ReactiveTimeAxis.js';
import { DependencyType } from '../../Engine/scheduling/Types.js';
import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';

import Toast from '../../Core/widget/Toast.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module Gantt/view/GanttBase
 */

const emptyObject = Object.freeze({});

let newTaskCount = 0;

/**
 * A thin base class for {@link Gantt.view.Gantt}. Does not include any features by default, allowing smaller custom
 * built bundles if used in place of {@link Gantt.view.Gantt}.
 *
 * @mixes Gantt/view/mixin/GanttDom
 * @mixes Gantt/view/mixin/GanttRegions
 * @mixes Gantt/view/mixin/GanttScroll
 * @mixes Gantt/view/mixin/GanttState
 * @mixes Gantt/view/mixin/GanttStores
 * @mixes Scheduler/crud/mixin/CrudManagerView
 * @mixes Scheduler/view/mixin/EventNavigation
 * @mixes Gantt/view/mixin/TaskNavigation
 *
 * @extends Scheduler/view/TimelineBase
 */
export default class GanttBase extends TimelineBase.mixin(
    CrudManagerView,
    GanttDom,
    GanttRegions,
    GanttScroll,
    GanttState,
    GanttStores,
    GanttTimelineDateMapper,
    EventNavigation,
    TaskNavigation
) {

    //region Task interaction events

    /**
     * Triggered after a mousedown on a task bar.
     * @event taskMouseDown
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered after a mouseup on a task bar.
     * @event taskMouseUp
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered after a click on a task bar.
     * @event taskClick
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered after a doubleclick on a task.
     * @event taskDblClick
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered after a rightclick (or long press on a touch device) on a task.
     * @event taskContextMenu
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered after a mouseover on a task.
     * @event taskMouseOver
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    /**
     * Triggered for mouseout from a task.
     * @event taskMouseOut
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Gantt.model.TaskModel} taskRecord The Task record
     * @param {MouseEvent} event The native browser event
     */

    //endregion

    //region Other events

    /**
     * Task is rendered, its element is available in DOM.
     * @event renderTask
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Object} renderData Task render data
     * @param {Gantt.model.TaskModel} taskRecord Rendered task
     * @param {HTMLElement} element Task element
     */

    /**
     * Task is released, no longer in view/removed. A good spot for cleaning custom things added in a `renderTask`
     * listener up, if needed.
     * @event releaseTask
     * @param {Gantt.view.Gantt} source The Gantt instance
     * @param {Object} renderData Task render data
     * @param {Gantt.model.TaskModel} taskRecord Rendered task
     * @param {HTMLElement} element Task element
     */

    //endregion

    // For documentation & typings purposes

    /**
     * Returns the dependency record for a DOM element
     *
     * *NOTE: Only available when the {@link Gantt/feature/Dependencies Dependencies} feature is enabled.*
     *
     * @function resolveDependencyRecord
     * @param {HTMLElement} element The dependency line element
     * @return {Gantt.model.DependencyModel} The dependency record
     * @category Feature shortcuts
     */

    //endregion

    //region Config

    static get $name() {
        return 'GanttBase';
    }

    // Factoryable type name
    static get type() {
        return 'ganttbase';
    }

    static get defaultConfig() {
        return {

            /**
             * A Project instance or a config object. The project holds all Gantt data.
             *
             * @config {Gantt.model.ProjectModel|Object} project
             * @category Common
             */

            /**
             * The project instance, containing the data visualized by this Gantt chart.
             *
             * @property {Gantt.model.ProjectModel}
             */
            project : null,

            /**
             * The path for resource images, used by various widgets such as the resource assignment column.
             * @config {String}
             * @category Common
             */
            resourceImageFolderPath : null,

            /**
             * The file name of an image file to use when a resource has no image, or its image cannot be loaded.
             * @config {String}
             * @category Common
             */
            defaultResourceImageName : null,

            /**
             * True to toggle the collapsed/expanded state when clicking a parent task bar.
             * @config {Boolean}
             * @default true
             * @category Common
             */
            toggleParentTasksOnClick : true,

            // data for the stores, in the topological order
            calendars    : null,
            resources    : null,
            tasks        : null,
            dependencies : null,
            assignments  : null,

            eventCls                : 'b-gantt-task',
            eventBarTextField       : null,
            eventLayout             : 'none',
            eventSelectionDisable   : true,
            eventColor              : null,
            eventStyle              : null,
            rowHeight               : 45,
            scheduledEventName      : 'task',
            eventScrollMode         : 'move',
            overScheduledEventClass : 'b-gantt-task-hover',
            mode                    : 'horizontal',
            //fixedRowHeight          : true, // Not working with exporter, no time to investigate why currently

            timeCellCls : 'b-sch-timeaxis-cell',

            timeCellSelector : '.b-sch-timeaxis-cell',

            // TODO: This will be brought in by the TaskNavigation mixin when it is implemented
            focusCls : 'b-active',

            /**
             * An empty function by default, but provided so that you can override it. This function is called each time
             * a task is rendered into the gantt to render the contents of the task.
             *
             * Returning a string will display it in the task bar, it accepts both plain text or HTML. It is also
             * possible to return a DOM config object which will be synced to the task bars content.
             *
             * ```javascript
             * // using plain string
             * new Gantt({
             *    taskRenderer : ({ taskRecord }) => taskRecord.name
             * });
             *
             * // using html string
             * new Gantt({
             *    taskRenderer : ({ taskRecord }) => `${taskRecord.id} <b>${taskRecord.name}</b>`
             * });
             *
             * // using DOM config
             * new Gantt({
             *    taskRenderer({ taskRecord }) {
             *       return {
             *           tag  : 'b',
             *           html : taskRecord.name
             *       }
             *    }
             * });
             * ```
             *
             * @param {Object} detail An object containing the information needed to render a Task.
             * @param {Gantt.model.TaskModel} detail.taskRecord The task record.
             * @param {Object} detail.tplData DEPRECATED 4.0: Use renderData instead.
             * @param {Object} detail.renderData An object containing details about the task rendering.
             * @param {Core.helper.util.DomClassList|String} detail.renderData.cls An object whose property names represent the CSS class names
             * to be added to the tasks's element. Set a property's value to truthy or falsy to add or remove the class
             * name based on the property name. Using this technique, you do not have to know whether the class is already
             * there, or deal with concatenation.
             * @param {Core.helper.util.DomClassList|String} detail.renderData.wrapperCls An object whose property names represent the CSS class names
             * to be added to the event wrapper element. Set a property's value to truthy or falsy to add or remove the class
             * name based on the property name. Using this technique, you do not have to know whether the class is already
             * there, or deal with concatenation.
             * @param {Core.helper.util.DomClassList|String} detail.renderData.iconCls An object whose property names represent the CSS class
             * names to be added to an task icon element.
             * @param {Scheduler.model.TimeSpan[]|Object[]} indicators An array that can be populated with TimeSpan
             * records or their config objects to have them rendered in the task row
             * @returns {String} A simple string creating the actual HTML
             * @config {Function}
             * @category Scheduled events
             */
            taskRenderer : null
        };
    }

    static get deprecatedEvents() {
        return {
            taskContextMenuBeforeShow : {
                product            : 'Gantt',
                invalidAsOfVersion : '5.0.0',
                message            : '`taskContextMenuBeforeShow` event is deprecated, in favor of `taskMenuBeforeShow` event. Please see https://bryntum.com/docs/gantt/#guides/upgrades/4.0.0.md for more information.'
            },
            taskContextMenuShow : {
                product            : 'Gantt',
                invalidAsOfVersion : '5.0.0',
                message            : '`taskContextMenuShow` event is deprecated, in favor of `taskMenuShow` event. Please see https://bryntum.com/docs/gantt/#guides/upgrades/4.0.0.md for more information.'
            },
            taskContextMenuItem : {
                product            : 'Gantt',
                invalidAsOfVersion : '5.0.0',
                message            : '`taskContextMenuItem` event is deprecated, in favor of `taskMenuItem` event. Please see https://bryntum.com/docs/gantt/#guides/upgrades/4.0.0.md for more information.'
            }
        };
    }

    get isGantt() {
        return true;
    }

    //endregion

    //region Init

    construct(config) {
        const
            me = this,
            hasInlineStores = Boolean(config.calendars || config.taskStore || config.dependencyStore || config.resourceStore || config.assignmentStore),
            hasInlineData = Boolean(config.calendars || config.tasks || config.dependencies || config.resources || config.assignments);

        // TODO: Config system to be enhanced to merge object properties from the hierarchy's
        // defaultConfig set so that each class level could just declare the features it requires.
        if (!config.features) {
            config.features = {};
        }

        // gantt is always a tree
        if (!('tree' in config.features)) {
            config.features.tree = true;
        }

        // disable group feature by default
        if (!('group' in config.features)) {
            config.features.group = false;
        }

        const { project } = config;

        if (project && (hasInlineStores || hasInlineData)) {
            throw new Error('Providing both project and inline data is not supported');
        }

        // gather all data in the ProjectModel instance
        if (!project?.isModel) {
            config.project = ObjectHelper.assign({
                calendarsData    : config.calendars,
                eventsData       : config.tasks,
                dependenciesData : config.dependencies,
                resourcesData    : config.resources,
                assignmentsData  : config.assignments,

                resourceStore   : config.resourceStore,
                eventStore      : config.taskStore,
                assignmentStore : config.assignmentStore,
                dependencyStore : config.dependencyStore,
                timeRangeStore  : config.timeRangeStore
            }, project);

            delete config.resourceStore;
            delete config.taskStore;
            delete config.assignmentStore;
            delete config.dependencyStore;
            delete config.timeRangeStore;

            delete config.calendars;
            delete config.resources;
            delete config.tasks;
            delete config.assignments;
            delete config.dependencies;
        }
        // EOF data gathering

        // Add listeners with negative priority to call propagation after rows are rendered
        // project.getEventStore().on({
        //     add     : me.onTaskStoreAddOrRemove,
        //     remove  : me.onTaskStoreAddOrRemove,
        //     prio    : -1,
        //     thisObj : me
        // });

        // If project is already loaded, possibly need to adjust timeline
        // if (project.loaded) {
        //     this.onProjectLoad();
        // }

        super.construct(config);

        // const { timeAxis } = me;
        //
        // me.reactiveTimeAxis = ReactiveTimeAxis.new({
        //     project,
        //     defaultSpan : timeAxis.defaultSpan,
        //     mainUnit    : timeAxis.mainUnit,
        //     dateRange   : 'startDate' in me.initialConfig ? {
        //         startDate : me.startDate,
        //         endDate   : me.endDate
        //     } : null
        // });
        // project.replica.addEntity(me.reactiveTimeAxis);
        // project.replica.addListener(me.reactiveTimeAxis.$.dateRange, dateRange => {
        //     timeAxis.setTimeSpan(dateRange.startDate, dateRange.endDate);
        // });

        if (me.features.cellEdit) {
            me.features.cellEdit.addNewAtEnd = {
                name         : me.L('L{Gantt.New task}'),
                startDate    : me.project.startDate,
                duration     : 1,
                durationUnit : me.timeAxis.unit
            };
        }

        if (me.toggleParentTasksOnClick) {
            me.on('taskclick', me.onTaskBarClick);
        }
    }

    get columns() {
        return super.columns;
    }

    set columns(columns) {
        if (columns) {
            let cols = columns;

            if (!Array.isArray(columns)) {
                cols = columns.data;

                // Need to pull the taskstore in, to make sure any fields added by columns are added to it
                this._thisIsAUsedExpression(this.taskStore);
            }

            // Always include the name column
            if (!cols.some(column => {
                const constructor = column instanceof Column ? column.constructor : ColumnStore.getColumnClass(column.type) || Column;

                return constructor === NameColumn || constructor.prototype instanceof NameColumn;
            })) {
                cols.unshift({
                    type : 'name'
                });
            }
        }

        super.columns = columns;

        // this.timeAxisColumn.reactiveRenderer = this.taskRendering.reactiveRenderer;
    }

    // Overrides TimelineBase to supply taskStore as its store (which is only used in passed events)
    set timeAxisViewModel(timeAxisViewModel) {
        super.timeAxisViewModel = timeAxisViewModel;

        if (this.taskStore) {
            this.timeAxisViewModel.store = this.taskStore;
        }
    }

    get timeAxisViewModel() {
        return super.timeAxisViewModel;
    }

    //endregion

    //region Events

    // onTaskStoreAddOrRemove({ isExpand, isCollapse }) {
    //     // Do not recalculate project if we expand or collapse
    //     // Also if we undoing/redoing, because project will be in already calculated state
    //     if (!isExpand && !isCollapse && !this.project.stm.isRestoring) {
    //         // This code basically means "if no one started propagation on previous animation frame after record
    //         // was added, start one to schedule new records". Required by refactored async addTask method, which forces
    //         // propagation
    //         if (!this.project.isPropagating()) {
    //             this.project.propagate();
    //         }
    //     }
    // }

    resumeRefresh(trigger) {

        super.resumeRefresh(false);

        if (!this.refreshSuspended && trigger) {
            if (!this.rowManager.topRow) {
                // TODO: investigate why we need this
                this.rowManager.reinitialize();
            }
            else {
                this.refreshWithTransition();
            }
        }
    }

    // Overriding grids behaviour to ignore individual updates caused by propagation
    // TODO: scheduler doesn't need that code and it shouldn't be needed in gantt too
    onStoreUpdateRecord(params) {
        if (!this.project.isBatchingChanges) {
            let result;

            this.runWithTransition(() => {
                result = super.onStoreUpdateRecord(params);
            });

            return result;
        }
    }

    // Transition batch changes
    onStoreDataChange(params) {
        this.runWithTransition(() => {
            super.onStoreDataChange(params);
        }, params.action === 'batch');
    }

    // Do not remove. Assertion strings for Localization sanity check.
    // 'L{storePopulation}'
    // 'L{propagating}'

    onProjectProgress({ total, remaining, phase = 'propagating' }) {
        const me = this;

        if (me.project.enableProgressNotifications) {
            const
                str  = me.L(phase),
                text = total ? `${str} ${total - remaining} / ${total}` : str;

            if (!me.masked) {
                me.mask({
                    maxProgress   : total,
                    useTransition : false
                });
            }

            me.masked.text = text;

            if (total) {
                // In case total changes...
                me.masked.maxProgress = total;
                me.masked.progress = total - remaining;
            }
        }
    }

    // Features can hook into this to add to generated task data
    onTaskDataGenerated() {}

    onTaskBarClick({ taskRecord }) {
        if (!taskRecord.isLeaf) {
            this.toggleCollapse(taskRecord);
        }
    }

    // Grid row selection change
    // TODO #8301 - EventSelection based on Collection may break it
    triggerChangeEvent(selectionChangeEvent, silent) {
        super.triggerChangeEvent(selectionChangeEvent, silent);

        const me = this;

        function setTaskSelection(record, selected) {
            const taskElement = me.getElementFromTaskRecord(record);
            if (taskElement) {
                taskElement.classList[selected ? 'add' : 'remove']('b-task-selected');
            }
        }

        if (selectionChangeEvent.mode === 'row') {
            selectionChangeEvent.selected.map(record => setTaskSelection(record, true));
            selectionChangeEvent.deselected.map(record => setTaskSelection(record, false));
        }
    }

    //endregion

    //region TimelineBase implementations

    // setStartDate(date, keepDuration = true) {
    //     const
    //         me = this,
    //         ta = me._timeAxis || {},
    //         {
    //             startDate,
    //             endDate,
    //             mainUnit
    //         } = ta;
    //
    //     if (me._timeAxis && endDate) {
    //         if (date) {
    //             let calcEndDate = endDate;
    //
    //             if (keepDuration && startDate) {
    //                 const diff = DateHelper.diff(startDate, endDate, mainUnit, true);
    //                 calcEndDate = DateHelper.add(date, diff, mainUnit);
    //             }
    //
    //             me.reactiveTimeAxis.dateRange = { startDate : date, endDate : calcEndDate };
    //             me.reactiveTimeAxis.propagateAsync();
    //         }
    //     }
    //     else {
    //         me._tempStartDate = date;
    //     }
    // }
    //
    // setEndDate(date, keepDuration = false) {
    //     const
    //         me = this,
    //         ta = me._timeAxis || {},
    //         {
    //             startDate,
    //             endDate,
    //             mainUnit
    //         } = ta;
    //
    //     if (me._timeAxis && startDate) {
    //         if (date) {
    //             let calcStartDate = startDate;
    //
    //             if (keepDuration && endDate) {
    //                 const diff = DateHelper.diff(startDate, endDate, mainUnit, true);
    //                 calcStartDate = DateHelper.add(date, -diff, mainUnit);
    //             }
    //
    //             me.reactiveTimeAxis.dateRange = { startDate : calcStartDate, endDate : date };
    //             me.reactiveTimeAxis.propagateAsync();
    //         }
    //     }
    //     else {
    //         me._tempEndDate = date;
    //     }
    // }
    //
    // set startDate(date) {
    //     this.setStartDate(date);
    // }
    //
    // get startDate() {
    //     const me = this;
    //
    //     if (me._timeAxis) {
    //         return me._timeAxis.startDate;
    //     }
    //
    //     return me._tempStartDate;
    // }
    //
    // set endDate(date) {
    //     this.setEndDate(date);
    // }
    //
    // get endDate() {
    //     const me = this;
    //
    //     if (me._timeAxis) {
    //         return me._timeAxis.endDate;
    //     }
    //
    //     return me._tempEndDate;
    // }

    // Overrides grid to take project loading into account
    toggleEmptyText() {
        const
            me = this;
        if (me.bodyContainer) {
            DomHelper.toggleClasses(me.bodyContainer, 'b-grid-empty', !(me.rowManager.rowCount || me.project.isLoadingOrSyncing));
        }
    }

    // Gantt only has one orientation, but TimelineBase expects this to work to call correct rendering code
    get currentOrientation() {
        const me = this;

        if (!me._currentOrientation) {
            //me.taskRendering = me._currentOrientation = new TaskRendering(me);
            me.taskRendering = me._currentOrientation = new NewTaskRendering(me);
        }

        return me._currentOrientation;
    }

    getTimeSpanMouseEventParams(taskElement, event) {
        const taskRecord = this.resolveTaskRecord(taskElement);

        return !taskRecord ? null : {
            taskRecord,
            taskElement,
            event
        };
    }

    getScheduleMouseEventParams(cellData) {
        return {
            taskRecord : this.store.getById(cellData.id)
        };
    }

    // Used by shared features to resolve an event or task
    resolveTimeSpanRecord(element) {
        return this.resolveTaskRecord(element);
    }

    repaintEventsForResource(taskRecord) {
        this.taskRendering.redraw(taskRecord);
    }

    //endregion

    //region Feature hooks

    /**
     * Populates the task context menu. Chained in features to add menu items.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown.
     * @param {Scheduler.model.EventModel} options.taskRecord The context event.
     * @param {Scheduler.model.ResourceModel} options.resourceRecord The context resource.
     * @param {Scheduler.model.AssignmentModel} options.assignmentRecord The context assignment if any.
     * @param {Object} options.items A named object to describe menu items.
     * @internal
     */
    populateTaskMenu() {}

    //endregion

    // region ContextMenu

    async addTask(referenceTask, options = emptyObject) {
        const
            me        = this,
            { milestone, above, asChild, asPredecessor, asSuccessor } = options,
            project   = me.project,
            parent    = referenceTask.parent,
            newRecord = referenceTask.copy();

        if (milestone) {
            newRecord.name = `${me.L('L{Gantt.New milestone}')} ${++newTaskCount}`;
        }
        else {
            newRecord.name = `${me.L('L{Gantt.New task}')} ${++newTaskCount}`;
        }

        if (asChild) {
            referenceTask.insertChild(newRecord, referenceTask.firstChild);
        }
        else if (above) {
            referenceTask.parent.insertChild(newRecord, referenceTask);
        }
        else {
            parent.insertChild(newRecord, referenceTask.nextSibling);
        }

        if (milestone) {
            await project.commitAsync();
            await newRecord.convertToMilestone();
        }
        else {
            await project.commitAsync();
        }

        // run propagation to handle the new task record
        // and then add a dependency if needed
        if (asSuccessor) {
            me.dependencyStore.add({
                fromEvent : referenceTask,
                toEvent   : newRecord,
                type      : DependencyType.EndToStart,
                fromSide  : 'right',
                toSide    : 'left'
            });
        }
        else if (asPredecessor) {
            me.dependencyStore.add({
                fromEvent : newRecord,
                toEvent   : referenceTask,
                type      : DependencyType.EndToStart,
                fromSide  : 'right',
                toSide    : 'left'
            });
        }

        if (asSuccessor || asPredecessor) {
            // run propagation to handle the new dependency
            await project.propagateAsync();
        }

        return newRecord;
    }

    addTaskAbove(taskRecord) {
        return this.addTask(taskRecord, { above : true });
    }

    addTaskBelow(taskRecord) {
        return this.addTask(taskRecord);
    }

    addMilestonBelow(taskRecord) {
        return this.addTask(taskRecord, { milestone : true });
    }

    addSubtask(taskRecord) {
        const result = this.addTask(taskRecord, { asChild : true });

        this.toggleCollapse(taskRecord, false);
        return result;
    }

    addSuccessor(taskRecord) {
        return this.addTask(taskRecord, { asSuccessor : true });
    }

    addPredecessor(taskRecord) {
        return this.addTask(taskRecord, { above : true, asPredecessor : true });
    }

    /**
     * Increase the indentation level of one or more tasks in the tree
     *
     * @param {Gantt.model.TaskModel[]|Gantt.model.TaskModel} tasks The task(s) to indent.
     * @return {Promise} A promise which resolves if operation is successful
     */
    async indent(nodes) {
        const
            me     = this,
            result = await me.taskStore.indent(nodes);

        // If `false`, the scheduling engine has found a reason that the operation could not happen.
        if (!result) {
            Toast.show(me.L('L{Gantt.changeRejected}'));
        }

        return result;
    }

    /**
     * Decrease the indentation level of one or more tasks in the tree
     *
     * @param {Gantt.model.TaskModel[]|Gantt.model.TaskModel} tasks The task(s) to outdent.
     * @return {Promise} A promise which resolves if operation is successful
     */
    async outdent(nodes) {
        const
            me     = this,
            result = await me.taskStore.outdent(nodes);

        // If `false`, the scheduling engine has found a reason that the operation could not happen.
        if (!result) {
            Toast.show(me.L('L{Gantt.changeRejected}'));
        }

        return result;
    }

    // endregion
}

// Register this widget type with its Factory
GanttBase.initClass();

VersionHelper.setVersion('gantt', '4.0.3');
