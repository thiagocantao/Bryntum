import Model from "../../../Core/data/Model.js"
import { AnyConstructor, Mixin } from "../../../ChronoGraph/class/Mixin.js"
import { AbstractPartOfProjectStoreMixin } from "./mixin/AbstractPartOfProjectStoreMixin.js"
import { AbstractProjectMixin } from '../model/AbstractProjectMixin.js'
import { AbstractDependencyStoreMixin } from './AbstractDependencyStoreMixin.js'
import { AbstractAssignmentStoreMixin } from './AbstractAssignmentStoreMixin.js'

const dataAddRemoveActions = {
    splice : 1,
    clear  : 1
}

// Shared functionality for CoreEventStore & ChronoEventStore
export class AbstractEventStoreMixin extends Mixin(
    [ AbstractPartOfProjectStoreMixin ],
    (base : AnyConstructor<AbstractPartOfProjectStoreMixin, typeof AbstractPartOfProjectStoreMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class AbstractEventStoreMixin extends base {
        project             : AbstractProjectMixin

        modelClass          : this[ 'project' ][ 'eventModelClass' ]

        assignmentsForRemoval   : Set<InstanceType<this[ 'project' ][ 'assignmentModelClass' ]>>    = new Set()


        dependenciesForRemoval  : Set<InstanceType<this[ 'project' ][ 'dependencyModelClass' ]>>    = new Set()


        // we need `onDataChange` for `syncDataOnLoad` option to work
        onDataChange (event : { action : string; added : Model[]; removed : Model[] }) {
            // remove from a filter action must be ignored.
            const isAddRemove  = dataAddRemoveActions[event.action]

            super.onDataChange(event)

            if (isAddRemove && event.removed?.length) this.afterEventRemoval()
        }


        // it seems `onDataChange` is not triggered for `remove` with `silent` flag
        remove (
            records : InstanceType<this[ 'modelClass' ]> | InstanceType<this[ 'modelClass' ]>[] | Set<InstanceType<this[ 'modelClass' ]>>, silent? : boolean
        ) : InstanceType<this[ 'modelClass' ]>[]
        {
            const res   = superProto.remove.call(this, records, silent)

            this.afterEventRemoval()

            return res
        }


        // it seems `onDataChange` is not triggered for `TreeStore#removeAll()`
        removeAll (silent? : boolean) : boolean {
            const res   = superProto.removeAll.call(this, silent)

            this.afterEventRemoval()

            return res
        }

        onNodeRemoveChild (parent, children, index, flags) {
            // @ts-ignore
            const removed   = superProto.onNodeRemoveChild.call(this, ...arguments)

            this.afterEventRemoval()

            return removed
        }

        afterEventRemoval () {
            const { assignmentsForRemoval, dependenciesForRemoval } = this

            // Can be called from `set data` during construction
            if (!assignmentsForRemoval) return

            // ORDER IS IMPORTANT!
            // First remove assignments
            const assignmentStore   = this.getAssignmentStore() as AbstractAssignmentStoreMixin

            if (assignmentStore && !assignmentStore.allAssignmentsForRemoval && assignmentsForRemoval.size) {
                const toRemove = [...assignmentsForRemoval].filter(assignment => !assignmentStore.assignmentsForRemoval.has(assignment))

                toRemove.length > 0 && assignmentStore.remove(toRemove)
            }

            assignmentsForRemoval.clear()

            // Then remove dependencies
            const dependencyStore   = this.getDependencyStore() as AbstractDependencyStoreMixin

            if (dependencyStore && !dependencyStore.allDependenciesForRemoval && dependenciesForRemoval.size) {
                const toRemove = [...dependenciesForRemoval].filter(dependency => !dependencyStore.dependenciesForRemoval.has(dependency))

                toRemove.length > 0 && dependencyStore.remove(toRemove)
            }

            dependenciesForRemoval.clear()
        }


        processRecord (eventRecord : InstanceType<this[ 'modelClass' ]>, isDataset : boolean = false) {
            if (!this.project?.isRepopulatingStores) {
                const existingRecord = this.getById(eventRecord.id)
                const isReplacing = existingRecord && existingRecord !== eventRecord

                //@ts-ignore
                if (isReplacing && existingRecord.assigned) {

                    //@ts-ignore
                    for (const assignment of existingRecord.assigned) {
                        assignment.event = eventRecord
                    }
                }
            }

            return eventRecord
        }

    }

    return AbstractEventStoreMixin
}){}
