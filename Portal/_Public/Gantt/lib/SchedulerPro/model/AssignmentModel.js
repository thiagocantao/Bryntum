import Model from '../../Core/data/Model.js';
import AssignmentModelMixin from '../../Scheduler/model/mixin/AssignmentModelMixin.js';
import { SchedulerProAssignmentMixin } from '../../Engine/quark/model/scheduler_pro/SchedulerProAssignmentMixin.js';
import PartOfProject from '../data/mixin/PartOfProject.js';

/**
 * @module SchedulerPro/model/AssignmentModel
 */

/**
 * This class represent a single assignment of a resource to an event in Scheduler Pro. It has a lot in common with
 * Schedulers AssignmentModel, they are separate models but they share much functionality using the
 * {@link Scheduler.model.mixin.AssignmentModelMixin AssignmentModelMixin} mixin.
 *
 * It is a subclass of {@link Core.data.Model} class. Please refer to the documentation for that class to become
 * familiar with the base interface of this class.
 *
 * ## Fields and references
 *
 * An Assignment has the following fields:
 * - `id` - The id of the assignment
 * - `resourceId` - The id of the resource assigned (optionally replaced with `resource` for load)
 * - `eventId` - The id of the event to which the resource is assigned (optionally replaced with `event` for load)
 *
 * The data source for these fields can be customized by subclassing this class:
 *
 * ```javascript
 * class MyAssignment extends AssignmentModel {
 *   static get fields() {
 *       return [
 *          { name : 'resourceId', dataSource : 'linkedResource' }
 *       ];
 *   }
 * }
 * ```
 *
 * After load and project normalization, these references are accessible (assuming their respective stores are loaded):
 * - `event` - The linked event record
 * - `resource` - The linked resource record
 *
 * ## Async resolving of references
 *
 * As described above, an assignment links an event to a resource. It holds references to an event record and a resource
 * record. These references are populated async, using the calculation engine of the project that the assignment via
 * its store is a part of. Because of this asyncness, references cannot be used immediately after modifications:
 *
 * ```javascript
 * assignment.resourceId = 2;
 * // assignment.resource is not yet available
 * ```
 *
 * To make sure references are updated, wait for calculations to finish:
 *
 * ```javascript
 * assignment.resourceId = 2;
 * await assignment.project.commitAsync();
 * // assignment.resource is available
 * ```
 *
 * As an alternative, you can also use `setAsync()` to trigger calculations directly after the change:
 *
 * ```javascript
 * await assignment.setAsync({ resourceId : 2});
 * // assignment.resource is available
 * ```
 *
 * @extends Core/data/Model
 * @mixes Scheduler/model/mixin/AssignmentModelMixin
 * @uninherit Core/data/mixin/TreeNode
 *
 * @typings Scheduler.model.AssignmentModel -> Scheduler.model.SchedulerAssignmentModel
 */
export default class AssignmentModel extends PartOfProject(AssignmentModelMixin(SchedulerProAssignmentMixin.derive(Model))) {

    // NOTE: Leave field defs at top to be picked up by jsdoc

    /**
     * Id for event to assign. Can be used as an alternative to `eventId`, but please note that after
     * load it will be populated with the actual event and not its id. This field is not persistable.
     * @field {SchedulerPro.model.EventModel} event
     * @accepts {String|Number|SchedulerPro.model.EventModel}
     * @typings {String||Number||SchedulerPro.model.EventModel||Core.model.Model}
     * @category Common
     */

    /**
     * Id for resource to assign to. Can be used as an alternative to `resourceId`, but please note that after
     * load it will be populated with the actual resource and not its id. This field is not persistable.
     * @field {SchedulerPro.model.ResourceModel} resource
     * @accepts {String|Number|SchedulerPro.model.ResourceModel}
     * @category Common
     */

    /**
     * A numeric, percent-like value, indicating the "contribution level"
     * of the resource availability to the {@link #field-event}.
     * Number 100 means that the assigned {@link #field-resource} spends all its working time
     * on the {@link #field-event}.
     * And number 50 means that the resource spends only half of its available time
     * on the {@link #field-event}.
     * Setting the value to 0 will unassign the resource (and remove the assignment)
     * @field {Number} units
     * @category Common
     */

    //region Config

    static $name = 'AssignmentModel';

    static isProAssignmentModel = true;

    //endregion

    //region Early render

    get event() {
        const
            { project } = this,
            event       = super.event;

        // Figure reference out before buckets are created (if part of project)
        if (project?.isDelayingCalculation) {
            return project.eventStore.getById(event);
        }

        return event;
    }

    set event(event) {
        super.event = event;
    }

    get resource() {
        const
            { project }  = this;

        let resource     = super.resource;

        // Figure reference out before buckets are created (if part of project)
        if (project?.isDelayingCalculation) {
            resource = project.resourceStore.getById(resource);
        }

        return resource?.$original;
    }

    set resource(resource) {
        super.resource = resource;
    }

    //endregion

    get eventResourceKey() {
        return this.isInActiveTransaction
            ? this.buildEventResourceKey(this.event, this.resource)
            : this.buildEventResourceKey(this.$.event.DATA, this.$.resource.DATA);
    }
}
