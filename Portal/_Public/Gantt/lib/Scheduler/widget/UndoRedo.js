import UndoRedoBase from '../../Core/widget/base/UndoRedoBase.js';
import '../../Core/widget/Combo.js';

/**
 * @module Scheduler/widget/UndoRedo
 */

const isProjectConsumer = w => w.isProjectConsumer;

/**
 * A widget which encapsulates undo/redo functionality for the {@link Scheduler.model.ProjectModel project}
 * of a scheduling widget (`Scheduler`, `Gantt` or `Calendar`).
 *
 * To make use of this, the project must be configured with a
 * {@link Scheduler.model.mixin.ProjectModelMixin#config-stm State Tracking Manager}.
 *
 * If inserted into a scheduling widget (such as into a `tbar`, or `bbar`, or as an item in a context menu),
 * the project of the encapsulating scheduling widget will be used.
 *
 * If this widget is to be used "standalone" (rendered into the DOM outside of a scheduling widget),
 * this must be configured with a reference to the project, or the scheduling widget which is
 * using the project.
 *
 * There are three child widgets encapsulated which may be referenced through the {@link Core.widget.Container#property-widgetMap}:
 *
 * - `undoBtn` - The button which operates the undo operation (CTRL+Z, or CMD+Z in Mac OS)
 * - `transactionsCombo` - A combobox into which is pushed the list of transactions,
 * - `redoBtn` - The button which operates the redo operation (CTRL+SHIFT+Z, + CMD+SHIFT+Z in Mac OS)
 *
 * To disable keyboard shortcuts for undo/redo, set {@link Scheduler.view.Scheduler#config-enableUndoRedoKeys} to false.
 *
 * The transactionsCombo may be configured away if only the buttons are required:
 *
 * ```javascript
 * {
 *     type      : 'undoredo',
 *     items     : {
 *         transactionsCombo : null
 *     }
 * }
 * ```
 *
 * The example below illustrated how to embed an `undoredo` widget in the top toolbar of a Scheduler.
 * @extends Core/widget/base/UndoRedoBase
 * @classType undoredo
 * @demo Scheduler/undoredo
 * @inlineexample Scheduler/widget/UndoRedo.js
 * @widget
 */
export default class UndoRedo extends UndoRedoBase {
    static get $name() {
        return 'UndoRedo';
    }

    static get type() {
        return 'undoredo';
    }

    static get configurable() {
        return {
            /**
             * The Scheduling Widget (or its `id`) whose transaction to track.
             *
             * This may be a `Scheduler`, a `Gantt` or a `Calendar`.
             *
             * ```javascript
             *     {
             *         type      : 'undoredo',
             *         scheduler : myCalendar
             *     }
             * ```
             * @config {Core.widget.Widget|String}
             */
            scheduler : null,

            /**
             * Get/set ProjectModel instance, containing the data visualized by the SchedulerPro.
             * @member {Scheduler.model.ProjectModel} project
             * @typings {ProjectModel}
             * @category Data
             */

            /**
             * The Scheduling {@link Scheduler.model.ProjectModel project}'s whose
             * transaction to track.
             *
             * ```javascript
             *     {
             *         type    : 'undoredo',
             *         project : scheduler.project
             *     }
             * ```
             * @config {Scheduler.model.ProjectModel|ProjectModelConfig}
             * @category Data
             */
            project : null
        };
    }

    construct() {
        super.construct(...arguments);

        // Look up a Project owner in our ancestors.
        if (!this.stm) {
            this.scheduler = this.up(isProjectConsumer);
        }

    }

    changeScheduler(scheduler) {
        return scheduler.isProjectConsumer ? scheduler : UndoRedo.getById(scheduler);
    }

    updateScheduler(scheduler) {
        const { crudManager } = scheduler;

        scheduler.ion({
            projectChange : 'onProjectChanged',
            thisObj       : this
        });

        if (crudManager) {
            this.setupLoadListener(crudManager);
        }
        // No CrudManager, so it must be inline data, so we can start immediately
        else {
            this.onLoad();
        }

        this.stm = scheduler.project.stm;
    }

    setupLoadListener(source) {
        source.detachListeners('load');
        source.ion({
            name    : 'load',
            load    : 'onLoad',
            thisObj : this
        });
    }

    async onLoad() {
        // Do not want normalization changes to be tracked by STM, wait until they are finished before enabling
        await this.scheduler.project.commitAsync();

        // Widget could be destroyed during async project commit
        if (!this.isDestroyed) {
            this.stm.enable();

            this.fillUndoRedoCombo();
        }
    }

    onProjectChanged({ project }) {
        this.project = project;
    }

    updateProject(project) {
        super.updateProject(...arguments);

        // The original condition `(project && !this.scheduler?.isConfiguring)` is not
        // correctly transpiled by angular webpack leading to https://github.com/bryntum/support/issues/3789
        // The following syntax fixes the above bug
        if (project && this.scheduler && !this.scheduler.isConfiguring) {
            this.setupLoadListener(project);
        }
    }
}

UndoRedo.initClass();
