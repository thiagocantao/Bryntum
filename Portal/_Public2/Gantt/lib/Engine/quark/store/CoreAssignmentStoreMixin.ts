import { CoreAssignmentMixin } from "../model/scheduler_core/CoreAssignmentMixin.js"
import { SchedulerCoreProjectMixin } from "../model/scheduler_core/SchedulerCoreProjectMixin.js"
import { Mixin, AnyConstructor } from "../../../ChronoGraph/class/BetterMixin.js"
import { CorePartOfProjectStoreMixin } from "./mixin/CorePartOfProjectStoreMixin.js"
import { CoreEventMixin } from "../model/scheduler_core/CoreEventMixin.js"
import { CoreResourceMixin } from "../model/scheduler_core/CoreResourceMixin.js"
import { AbstractAssignmentStoreMixin } from "./AbstractAssignmentStoreMixin.js"

const emptySet = new Set<CoreAssignmentMixin>()


/**
 * A store mixin class, that represent collection of all assignments in the [[SchedulerCoreProjectMixin|project]].
 */
export class CoreAssignmentStoreMixin extends Mixin(
    [ AbstractAssignmentStoreMixin, CorePartOfProjectStoreMixin ],
    (base : AnyConstructor<AbstractAssignmentStoreMixin & CorePartOfProjectStoreMixin, typeof AbstractAssignmentStoreMixin & typeof CorePartOfProjectStoreMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class CoreAssignmentStoreMixin extends base {

        project                     : SchedulerCoreProjectMixin

        modelClass                  : this[ 'project' ][ 'assignmentModelClass' ]

        isBatchAssigning            : boolean


        static get defaultConfig () : object {
            return {
                modelClass  : CoreAssignmentMixin,

                storage : {
                    extraKeys : [
                        { property : 'event', unique : false },
                        { property : 'resource', unique : false }
                    ]
                }
            }
        }


        set data (value) {
            this.allAssignmentsForRemoval   = true

            super.data = value

            this.allAssignmentsForRemoval   = false
        }


        getEventsAssignments (event : CoreEventMixin) : Set<CoreAssignmentMixin> {
            return this.storage.findItem('event', event) as Set<CoreAssignmentMixin> || emptySet
        }


        getResourcesAssignments (resource : CoreResourceMixin) : Set<CoreAssignmentMixin> {
            return this.storage.findItem('resource', resource) as Set<CoreAssignmentMixin> || emptySet
        }


        updateIndices () {
            // @ts-ignore
            this.storage.rebuildIndices()
        }


        // Link events/resources to assignments, called when those stores are populated or joined to project
        linkAssignments (store : CorePartOfProjectStoreMixin, modelName : string) {
            const unresolved = this.storage.findItem(modelName, null) as Set<CoreAssignmentMixin>
            if (unresolved) {
                this.isBatchAssigning = true

                for (const assignment of unresolved) {
                    const record = store.getById(assignment.getCurrentOrProposed(modelName))
                    if (record) assignment.setChanged(modelName, record)
                }

                this.isBatchAssigning = false

                this.updateIndices()
            }
        }


        onCommitAsync () {
            this.updateIndices()
        }

    }

    return CoreAssignmentStoreMixin
}){}

