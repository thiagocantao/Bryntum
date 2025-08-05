import '../localization/En.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';
import SchedulingIssueResolution from './mixin/SchedulingIssueResolution.js';
import ProjectProgressMixin from './mixin/ProjectProgressMixin.js';
import SchedulerBase from '../../Scheduler/view/SchedulerBase.js';
import ProjectModel from '../model/ProjectModel.js';
import SchedulerProEventRendering from './mixin/SchedulerProEventRendering.js';
import ProHorizontalRendering from './orientation/ProHorizontalRendering.js';
import ProVerticalRendering from './orientation/ProVerticalRendering.js';

/**
 * @module SchedulerPro/view/SchedulerProBase
 */

/**
 * A thin base class for {@link SchedulerPro/view/SchedulerPro}. Includes fewer features by default, allowing smaller
 * custom-built bundles if used in place of {@link SchedulerPro/view/SchedulerPro}.
 *
 * **NOTE:** In most scenarios you should use SchedulerPro instead of SchedulerProBase.
 *
 * @features SchedulerPro/feature/CalendarHighlight
 * @features SchedulerPro/feature/CellEdit
 * @features SchedulerPro/feature/Dependencies
 * @features SchedulerPro/feature/DependencyEdit
 * @features SchedulerPro/feature/EventBuffer
 * @features SchedulerPro/feature/EventResize
 * @features SchedulerPro/feature/EventSegmentDrag
 * @features SchedulerPro/feature/EventSegmentResize
 * @features SchedulerPro/feature/EventSegments
 * @features SchedulerPro/feature/NestedEvents
 * @features SchedulerPro/feature/PercentBar
 * @features SchedulerPro/feature/ResourceNonWorkingTime
 * @features SchedulerPro/feature/TaskEdit
 * @features SchedulerPro/feature/TimeSpanHighlight
 * @features SchedulerPro/feature/Versions
 *
 * @extends Scheduler/view/SchedulerBase
 * @mixes SchedulerPro/view/mixin/ProjectProgressMixin
 * @mixes SchedulerPro/view/mixin/SchedulerProEventRendering
 * @mixes SchedulerPro/view/mixin/SchedulingIssueResolution
 * @widget
 */
export default class SchedulerProBase extends SchedulerBase.mixin(
    ProjectProgressMixin,
    SchedulerProEventRendering,
    SchedulingIssueResolution
) {

    //region Config

    static $name = 'SchedulerProBase';

    static type = 'schedulerprobase';

    static configurable =  {
        projectModelClass : ProjectModel,

        /**
         * A task field (id, wbsCode, sequenceNumber etc) that will be used when displaying and editing linked tasks.
         * @config {String} dependencyIdField
         * @default 'id'
         */
        dependencyIdField : 'id',

        /**
         * If set to `true` this will show a color field in the {@link SchedulerPro.feature.TaskEdit} editor and also a
         * picker in the {@link Scheduler.feature.EventMenu}. Both enables the user to choose a color which will be
         * applied to the event bar's background. See EventModel's
         * {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} config.
         * config.
         * @config {Boolean}
         * @category Misc
         */
        showTaskColorPickers : false
    };

    static isSchedulerPro = true;

    //endregion

    //region Store & model docs

    // Configs

    /**
     * A {@link SchedulerPro.model.ProjectModel} instance or a config object. The project holds all SchedulerPro data.
     * @config {SchedulerPro.model.ProjectModel|ProjectModelConfig} project
     * @category Data
     */

    /**
     * Inline events, will be loaded into the backing project's EventStore.
     * @config {SchedulerPro.model.EventModel[]|Object[]} events
     * @category Data
     */

    /**
     * The {@link SchedulerPro.data.EventStore} holding the events to be rendered into the scheduler.
     * @config {SchedulerPro.data.EventStore|EventStoreConfig} eventStore
     * @category Data
     */

    /**
     * Inline resources, will be loaded into the backing project's ResourceStore.
     * @config {SchedulerPro.model.ResourceModel[]|ResourceModelConfig[]} resources
     * @category Data
     */

    /**
     * The {@link SchedulerPro.data.ResourceStore} holding the resources to be rendered into the scheduler.
     * @config {SchedulerPro.data.ResourceStore|ResourceStoreConfig} resourceStore
     * @category Data
     */

    // For some reason Typings won't accept AssignmentModelConfig here. Object will be turned into it though
    /**
     * Inline assignments, will be loaded into the backing project's AssignmentStore.
     * @config {SchedulerPro.model.AssignmentModel[]|Object[]} assignments
     * @category Data
     */

    /**
     * The optional {@link SchedulerPro.data.AssignmentStore}, holding assignments between resources and events.
     * Required for multi assignments.
     * @config {SchedulerPro.data.AssignmentStore|AssignmentStoreConfig} assignmentStore
     * @category Data
     */

    /**
     * Inline dependencies, will be loaded into the backing project's DependencyStore.
     * @config {SchedulerPro.model.DependencyModel[]|DependencyModelConfig[]} dependencies
     * @category Data
     */

    /**
     * The optional {@link SchedulerPro.data.DependencyStore}.
     * @config {SchedulerPro.data.DependencyStore|DependencyStoreConfig} dependencyStore
     * @category Data
     */

    /**
     * Inline calendars, will be loaded into the backing project's CalendarManagerStore.
     * @config {SchedulerPro.model.CalendarModel[]|CalendarModelConfig[]} calendars
     * @category Data
     */

    // Properties

    /**
     * Get/set ProjectModel instance, containing the data visualized by the SchedulerPro.
     * @member {SchedulerPro.model.ProjectModel} project
     * @typings {ProjectModel}
     * @category Data
     */

    /**
     * Get/set events, applies to the backing project's EventStore.
     * @member {SchedulerPro.model.EventModel[]} events
     * @accepts {SchedulerPro.model.EventModel[]|EventModelConfig[]}
     * @category Data
     */

    /**
     * Get/set the event store instance of the backing project.
     * @member {SchedulerPro.data.EventStore} eventStore
     * @typings Scheduler.view.SchedulerBase:eventStore -> {Scheduler.data.EventStore||SchedulerPro.data.EventStore}
     * @category Data
     */

    /**
     * Get/set resources, applies to the backing project's ResourceStore.
     * @member {SchedulerPro.model.ResourceModel[]} resources
     * @accepts {SchedulerPro.model.ResourceModel[]|ResourceModelConfig[]}
     * @category Data
     */

    /**
     * Get/set the resource store instance of the backing project
     * @member {SchedulerPro.data.ResourceStore} resourceStore
     * @typings Scheduler.view.SchedulerBase:resourceStore -> {Scheduler.data.ResourceStore||SchedulerPro.data.ResourceStore}
     * @category Data
     */

    // For some reason Typings won't accept AssignmentModelConfig here. Object will be turned into it though
    /**
     * Get/set assignments, applies to the backing project's AssignmentStore.
     * @member {SchedulerPro.model.AssignmentModel[]} assignments
     * @accepts {SchedulerPro.model.AssignmentModel[]|Object[]}
     * @category Data
     */

    /**
     * Get/set the event store instance of the backing project.
     * @member {SchedulerPro.data.AssignmentStore} assignmentStore
     * @typings Scheduler.view.SchedulerBase:assignmentStore -> {Scheduler.data.AssignmentStore||SchedulerPro.data.AssignmentStore}
     * @category Data
     */

    /**
     * Get/set dependencies, applies to the backing projects DependencyStore.
     * @member {SchedulerPro.model.DependencyModel[]} dependencies
     * @accepts {SchedulerPro.model.DependencyModel[]|DependencyModelConfig[]}
     * @category Data
     */

    /**
     * Get/set the dependencies store instance of the backing project.
     * @member {SchedulerPro.data.DependencyStore} dependencyStore
     * @typings Scheduler.view.SchedulerBase:dependencyStore -> {Scheduler.data.DependencyStore||SchedulerPro.data.DependencyStore}
     * @category Data
     */

    /**
     * Get/set calendars, applies to the backing projects CalendarManagerStore.
     * @member {SchedulerPro.model.CalendarModel[]} calendars
     * @accepts {SchedulerPro.model.CalendarModel[]|CalendarModelConfig[]}
     * @category Data
     */

    //endregion

    //region Overrides

    onPaintOverride() {
        // Internal procedure used for paint method overrides
        // Not used in onPaint() because it may be chained on instance and Override won't be applied
    }

    //endregion

    //region Inline data

    // Pro specific extension of SchedulerStores

    set calendars(calendars) {
        this.project.calendars = calendars;
    }

    get calendars() {
        return this.project.calendars;
    }

    //endregion

    //region Mode

    /**
     * Get mode (horizontal/vertical)
     * @property {'horizontal'|'vertical'}
     * @readonly
     * @category Common
     */
    get mode() {
        return this._mode;
    }

    set mode(mode) {
        const me = this;

        me._mode = mode;

        if (!me[mode]) {
            me.element.classList.add(`b-sch-${mode}`);

            if (mode === 'horizontal') {
                me.horizontal = new ProHorizontalRendering(me);

                if (me.isPainted) {
                    me.horizontal.init();
                }

            }
            else if (mode === 'vertical') {
                me.vertical = new ProVerticalRendering(me);

                if (me.rendered) {
                    me.vertical.init();
                }
            }
        }
    }

    //endregion

    //region Internal

    // Overrides grid to take project loading into account
    toggleEmptyText() {
        const
            me = this;

        if (me.bodyContainer && me.rowManager) {
            DomHelper.toggleClasses(me.bodyContainer, 'b-grid-empty', !(me.rowManager.rowCount || me.project.isLoadingOrSyncing));
        }
    }

    // Needed to work with Gantt features
    get taskStore() {
        return this.project.eventStore;
    }

    //endregion

    createEvent(startDate, resourceRecord, row) {
        // For resources with a calendar, ensure the date is inside a working time range
        if (!resourceRecord.isWorkingTime(startDate)) {
            return;
        }

        // If task editor is active dblclick will trigger number of async actions:
        // store add which would schedule project commit
        // editor cancel on next animation frame
        // editor hide
        // rejecting previous transaction
        // and there is also dependency feature listening to transitionend on scheduler to draw lines after
        // It can happen that user dblclicks too fast, then event will be added, then dependency will schedule itself
        // to render, and then event will be removed as part of transaction rejection from editor. So we cannot add
        // event before active transaction is done.
        if (this.taskEdit && this.taskEdit.isEditing) {
            this.ion({
                aftertaskedit : () => super.createEvent(startDate, resourceRecord, row),
                once          : true
            });
        }
        else {
            return super.createEvent(startDate, resourceRecord, row);
        }
    }

}

SchedulerProBase.initClass();
VersionHelper.setVersion('schedulerpro', '5.5.0');
