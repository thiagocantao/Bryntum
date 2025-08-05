import Model from "../../../Core/data/Model.js"
import { AnyConstructor, Mixin } from "../../../ChronoGraph/class/Mixin.js"
import { AbstractPartOfProjectStoreMixin } from "./mixin/AbstractPartOfProjectStoreMixin.js"
import { AbstractProjectMixin } from '../model/AbstractProjectMixin.js'
import { AbstractAssignmentStoreMixin } from './AbstractAssignmentStoreMixin.js'

const dataAddRemoveActions = {
    splice : 1,
    clear  : 1
}

// Shared functionality for CoreResourceStore & ChronoResourceStore
export class AbstractResourceStoreMixin extends Mixin(
    [ AbstractPartOfProjectStoreMixin ],
    (base : AnyConstructor<AbstractPartOfProjectStoreMixin, typeof AbstractPartOfProjectStoreMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class AbstractResourceStoreMixin extends base {

        project             : AbstractProjectMixin

        modelClass          : this[ 'project' ][ 'resourceModelClass' ]

        assignmentsForRemoval   : Set<InstanceType<this[ 'project' ][ 'assignmentModelClass' ]>>    = new Set()


        // we need `onDataChange` for `syncDataOnLoad` option to work
        onDataChange (event : { action : string; added : Model[]; removed : Model[] }) {
            // remove from a filter action must be ignored.
            const isAddRemove  = dataAddRemoveActions[event.action];

            super.onDataChange(event)

            if (isAddRemove && event.removed?.length) this.afterResourceRemoval()
        }


        // it seems `onDataChange` is not triggered for `remove` with `silent` flag
        remove (
            records : InstanceType<this[ 'modelClass' ]> | InstanceType<this[ 'modelClass' ]>[] | Set<InstanceType<this[ 'modelClass' ]>>, silent? : boolean
        ) : InstanceType<this[ 'modelClass' ]>[]
        {
            const res   = superProto.remove.call(this, records, silent)

            this.afterResourceRemoval()

            return res
        }


        // it seems `onDataChange` is not triggered for `TreeStore#removeAll()`
        removeAll (silent? : boolean) : boolean {
            const res   = superProto.removeAll.call(this, silent)

            this.afterResourceRemoval()

            return res
        }


        afterResourceRemoval () {

            const assignmentStore   = this.getAssignmentStore() as AbstractAssignmentStoreMixin

            if (assignmentStore && !assignmentStore.allAssignmentsForRemoval) {
                const assignmentsForRemoval = [...this.assignmentsForRemoval].filter(assignment => !assignmentStore.assignmentsForRemoval.has(assignment))

                assignmentsForRemoval.length > 0 && assignmentStore.remove(assignmentsForRemoval)
            }

            this.assignmentsForRemoval.clear()
        }


        processRecord (resourceRecord : InstanceType<this[ 'modelClass' ]>, isDataset : boolean = false) {
            const existingRecord        = this.getById(resourceRecord.id)
            const isReplacing           = existingRecord && existingRecord !== resourceRecord

            if (isReplacing) {

                //@ts-ignore
                for (const assignment of existingRecord.assigned || []) {
                    assignment.resource    = resourceRecord
                }
            }

            return resourceRecord
        }

    }

    return AbstractResourceStoreMixin
}){}
