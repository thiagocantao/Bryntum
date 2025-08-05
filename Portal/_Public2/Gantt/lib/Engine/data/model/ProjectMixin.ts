import { ChronoAtom, ChronoIterator } from "../../../ChronoGraph/chrono/Atom.js"
import { EffectResolverFunction } from "../../../ChronoGraph/chrono/Effect.js"
import { ChronoGraph, PropagationResult } from "../../../ChronoGraph/chrono/Graph.js"
import { AnyConstructor, Mixin, MixinConstructor } from "../../../ChronoGraph/class/Mixin.js"
import { FieldAtomI } from "../../../ChronoGraph/replica/Atom.js"
import { Entity, calculate, field } from "../../../ChronoGraph/replica/Entity.js"
import { MinimalReplica } from "../../../ChronoGraph/replica/Replica.js"
import { Schema } from "../../../ChronoGraph/schema/Schema.js"
import Model from "../../../Common/data/Model.js"
import StateTrackingManager from "../../../Common/data/stm/StateTrackingManager.js"
import Store from "../../../Common/data/Store.js"
import FunctionHelper from "../../../Common/helper/FunctionHelper.js"
import Events, { EventsMixin } from "../../../Common/mixin/Events.js"
import { CalendarManagerStoreMixin, MinimalCalendarManagerStore } from "../../calendar/CalendarManagerStoreMixin.js"
import { CalendarMixin, MinimalCalendar } from "../../calendar/CalendarMixin.js"
import { model_field } from "../../chrono/ModelFieldAtom.js"
import { EngineReplica } from "../../chrono/Replica.js"
import { DependenciesCalendar } from "../../scheduling/Types.js"
import { PartOfProjectGenericMixin } from "../PartOfProjectGenericMixin.js"
import { AssignmentStoreMixin, MinimalAssignmentStore } from "../store/AssignmentStoreMixin.js"
import { DependencyStoreMixin, MinimalDependencyStore } from "../store/DependencyStoreMixin.js"
import { EventStoreMixin, MinimalEventStore } from "../store/EventStoreMixin.js"
import { MinimalResourceStore, ResourceStoreMixin } from "../store/ResourceStoreMixin.js"
import { AssignmentMixin, MinimalAssignment } from "./AssignmentMixin.js"
import { DependencyMixin, MinimalDependency } from "./DependencyMixin.js"
import { BryntumEvent } from "./event/BryntumEvent.js"
import { ConstrainedEvent, ConstraintInterval } from "./event/ConstrainedEvent.js"
import { EventMixin } from "./event/EventMixin.js"
import { HasAssignments } from "./event/HasAssignments.js"
import { HasChildren } from "./event/HasChildren.js"
import { HasCalendarMixin } from "./HasCalendarMixin.js"
import { ChronoModelMixin } from "./mixin/ChronoModelMixin.js"
import { PartOfProjectMixin } from "./mixin/PartOfProjectMixin.js"
import { MinimalResource, ResourceMixin } from "./ResourceMixin.js"
import { HasDependencies } from "./event/HasDependencies.js";
import { MAX_DATE } from "../../util/Constants.js";


export interface IProjectMixin {
    eventStore                  : EventStoreMixin
    dependencyStore             : DependencyStoreMixin
    resourceStore               : ResourceStoreMixin
    assignmentStore             : AssignmentStoreMixin
    calendarManagerStore        : CalendarManagerStoreMixin
    defaultCalendar             : CalendarMixin
    getGraph ()                 : ChronoGraph
    calendar                    : CalendarMixin
    startDate                   : Date,
    endDate                     : Date,
    dependenciesCalendar        : DependenciesCalendar

    $                           : { [s in keyof this] : FieldAtomI }

    hasListener (event : string) : boolean
    trigger (event : string, options? : object) : boolean
    on (...args : any[]) : void

    propagate (onEffect? : EffectResolverFunction) : Promise<PropagationResult>
    tryPropagateWithNodes (onEffect? : EffectResolverFunction, nodes? : ChronoAtom[], hatchFn? : Function) : Promise<PropagationResult>
    tryPropagateWithEntities (onEffect? : EffectResolverFunction, entities? : Entity[], hatchFn? : Function) : Promise<PropagationResult>
    waitForPropagateCompleted () : Promise<PropagationResult | null>

    getStm () : StateTrackingManager
}


export type ProjectDataPackage = {
    eventsData?             : any[]
    dependenciesData?       : any[]
    resourcesData?          : any[]
    assignmentsData?        : any[]
    calendarsData?          : any[]
}

/**
 * Critical path entry.
 */
export type CriticalPathNode = {
    /**
     * Critical event.
     */
    event       : HasDependencies,
    /**
     * Dependency leading to the next path entry.
     * Omitted for the last entry.
     */
    dependency? : DependencyMixin
}

/**
 * Project _critical path_.
 */
export type CriticalPath = CriticalPathNode[]


export const ProjectMixin = <T extends AnyConstructor<HasCalendarMixin & HasAssignments & EventsMixin & Entity>>(base : T) => {

    class ProjectMixin extends base implements IProjectMixin {

        replica                   : EngineReplica

        // Note, that we specify the `EventMixin` as the type for events, assuming the minimal possible functionality
        // (like in scheduler). To extend the functionality need to override this property
        /**
         * The constructor for the "Event" entity of the project
         */
        eventModelClass           : AnyConstructor<EventMixin>

        /**
         * The constructor for the "Dependency" entity of the project
         */
        dependencyModelClass      : AnyConstructor<DependencyMixin>

        /**
         * The constructor for the "Resource" entity of the project
         */
        resourceModelClass        : AnyConstructor<ResourceMixin>

        /**
         * The constructor for the "Assignment" entity of the project
         */
        assignmentModelClass      : AnyConstructor<AssignmentMixin>

        /**
         * The constructor for the "Calendar" entity of the project
         */
        calendarModelClass        : AnyConstructor<CalendarMixin>

        /**
         * The constructor for the "Events" collection of the project
         */
        eventStoreClass           : AnyConstructor<EventStoreMixin>

        /**
         * The constructor for the "Dependencies" collection of the project
         */
        dependencyStoreClass      : AnyConstructor<DependencyStoreMixin>

        /**
         * The constructor for the "Resources" collection of the project
         */
        resourceStoreClass        : AnyConstructor<ResourceStoreMixin>

        /**
         * The constructor for the "Assignments" collection of the project
         */
        assignmentStoreClass      : AnyConstructor<AssignmentStoreMixin>

        /**
         * The constructor for the "Calendars" collection of the project
         */
        calendarManagerStoreClass : AnyConstructor<CalendarManagerStoreMixin>

        /**
         * State tracking manager instance the project relies on
         */
        stm                       : StateTrackingManager

        /**
         * The instance of the "Events" collection of the project
         */
        eventStore                : EventStoreMixin

        /**
         * The instance of the "Dependencies" collection of the project
         */
        dependencyStore           : DependencyStoreMixin

        /**
         * The instance of the "Resources" collection of the project
         */
        resourceStore             : ResourceStoreMixin

        /**
         * The instance of the "Assignments" collection of the project
         */
        assignmentStore           : AssignmentStoreMixin

        /**
         * The instance of the "Calendars" collection of the project
         */
        calendarManagerStore      : CalendarManagerStoreMixin

        eventsData                : any[]
        dependenciesData          : any[]
        resourcesData             : any[]
        assignmentsData           : any[]
        calendarsData             : any[]

        defaultCalendar           : MinimalCalendar

        @model_field({ type : 'boolean', defaultValue : true })
        unspecifiedTimeIsWorking  : boolean

        @model_field({ type : 'string', defaultValue : DependenciesCalendar.ToEvent })
        dependenciesCalendar      : DependenciesCalendar

        @model_field({ type : 'boolean', defaultValue : true })
        autoCalculatePercentDoneForParentTasks      : boolean

        /**
         * The array of the project _critical paths_.
         * Each critical path in turn is represented as an array of [[CriticalPathNode]] entries.
         */
        @field()
        criticalPaths             : CriticalPath[]

        construct (config : any = {}) {
            this.replica        = EngineReplica(MinimalReplica).new({ project : this, schema : Schema.new() })

            // Expand project by default to make getRange to work
            if (!config.hasOwnProperty('expanded')) {
                config.expanded = true
            }

            const hasInlineStore = Boolean(
                config.calendarManagerStore || config.eventStore || config.dependencyStore || config.resourceStore || config.assignmentStore
            )

            super.construct(config)

            if (!this.eventModelClass) this.eventModelClass = BryntumEvent
            if (!this.eventStoreClass) this.eventStoreClass = MinimalEventStore

            if (!this.dependencyModelClass) this.dependencyModelClass = MinimalDependency
            if (!this.dependencyStoreClass) this.dependencyStoreClass = MinimalDependencyStore

            if (!this.resourceModelClass) this.resourceModelClass = MinimalResource
            if (!this.resourceStoreClass) this.resourceStoreClass = MinimalResourceStore

            if (!this.assignmentModelClass) this.assignmentModelClass = MinimalAssignment
            if (!this.assignmentStoreClass) this.assignmentStoreClass = MinimalAssignmentStore

            if (!this.calendarModelClass) this.calendarModelClass = MinimalCalendar
            if (!this.calendarManagerStoreClass) this.calendarManagerStoreClass = MinimalCalendarManagerStore


            this.replica.addEntity(this)

            this.stm = new StateTrackingManager({ disabled : true })

            if (this.calendarManagerStore) {
                this.setCalendarManagerStore(this.calendarManagerStore)
            } else
                this.calendarManagerStore = new this.calendarManagerStoreClass({

                    modelClass  : this.calendarModelClass,

                    idField     : 'id',

                    project     : this,

                    stm         : this.stm
                })

            // not part of the CalendarManagerStore intentionally, not persisted
            this.defaultCalendar    = new this.calendarManagerStore.modelClass({
                hoursPerDay                 : 24,
                daysPerWeek                 : 7,
                daysPerMonth                : 30,

                unspecifiedTimeIsWorking    : this.unspecifiedTimeIsWorking
            })

            this.defaultCalendar.project = this

            if (this.eventStore) {
                // a valid use case for accessor?
                this.setEventStore(this.eventStore)
            } else
                this.eventStore      = new this.eventStoreClass({

                    modelClass  : this.eventModelClass,

                    tree        : true,

                    idField     : 'id',

                    project     : this,

                    stm         : this.stm
                })

            if (this.dependencyStore) {
                this.setDependencyStore(this.dependencyStore)
            } else
                this.dependencyStore    = new this.dependencyStoreClass({

                    modelClass  : this.dependencyModelClass,

                    idField     : 'id',

                    project     : this,

                    stm         : this.stm
                })

            if (this.resourceStore) {
                this.setResourceStore(this.resourceStore)
            } else
                this.resourceStore    = new this.resourceStoreClass({

                    modelClass  : this.resourceModelClass,

                    idField     : 'id',

                    project     : this,

                    stm         : this.stm
                })

            if (this.assignmentStore) {
                this.setAssignmentStore(this.assignmentStore)
            } else
                this.assignmentStore    = new this.assignmentStoreClass({

                    modelClass  : this.assignmentModelClass,

                    idField     : 'id',

                    project     : this,

                    stm         : this.stm
                })

            const hasInlineData = Boolean(this.calendarsData || this.eventsData || this.dependenciesData || this.resourcesData || this.assignmentsData)

            if (hasInlineData) {
                this.loadInlineData({
                    calendarsData       : this.calendarsData,
                    eventsData          : this.eventsData,
                    dependenciesData    : this.dependenciesData,
                    resourcesData       : this.resourcesData,
                    assignmentsData     : this.assignmentsData
                })

                delete this.calendarsData
                delete this.eventsData
                delete this.dependenciesData
                delete this.resourcesData
                delete this.assignmentsData
            }

            // TODO this should be the same propagate as in "loadInlineData"
            // or at least fire same side effects
            if (hasInlineStore && !hasInlineData) this.propagate()
        }


        loadInlineData (data : ProjectDataPackage) : Promise<PropagationResult> {
            if (data.calendarsData) {
                this.calendarManagerStore.data = data.calendarsData
            }
            if (data.eventsData) {
                this.eventStore.data   = data.eventsData
            }
            if (data.dependenciesData) {
                this.dependencyStore.data   = data.dependenciesData
            }
            if (data.resourcesData) {
                this.resourceStore.data     = data.resourcesData
            }
            if (data.assignmentsData) {
                this.assignmentStore.data   = data.assignmentsData
            }

            return this.propagate()
        }


        getGraph () : EngineReplica {
            return this.replica
        }


        getStm () : StateTrackingManager {
            return this.stm
        }


        calculateProject () : IProjectMixin {
            return this
        }


        * calculateCalendar (proposedValue? : CalendarMixin) : ChronoIterator<this[ 'calendar' ]> {
            let result = yield *super.calculateCalendar(proposedValue)

            // fallback to defaultCalendar
            if (!result) {
                result = this.defaultCalendar
                yield result.$$
            }

            return result
        }


        joinStoreRecords (store : Store) {
            const fn = (record : PartOfProjectMixin) => {
                record.setProject(this)
                record.joinProject()
            }

            if (store.rootNode) {
                store.rootNode.traverse(fn)
            } else {
                store.forEach(fn)
            }
        }


        setEventStore (store : EventStoreMixin) {
            //if (this.eventStore !== store) {

                if (this.eventStore && this.stm.hasStore(this.eventStore)) {
                    this.stm.removeStore(this.eventStore)
                }

                this.eventStore         = store

                if (store) {

                    store.setProject(this)
                    this.stm.addStore(store)

                    // we've been given an event store from the outside
                    // need to change its root node to be the project
                    if (store.rootNode !== this) {
                        this.appendChild(store.rootNode.children || [])

                        store.rootNode      = this
                    }

                    this.joinStoreRecords(store)
                }
            //}
        }


        setDependencyStore (store : DependencyStoreMixin) {
            //if (this.dependencyStore !== store) {

                if (this.dependencyStore && this.stm.hasStore(this.dependencyStore)) {
                    this.stm.removeStore(this.dependencyStore)
                }

                this.dependencyStore    = store

                if (store) {
                    store.setProject(this)
                    this.stm.addStore(store)
                    this.joinStoreRecords(store)
                }
            //}
        }


        setResourceStore (store : ResourceStoreMixin) {
            //if (this.resourceStore !== store) {

                if (this.resourceStore && this.stm.hasStore(this.resourceStore)) {
                    this.stm.removeStore(this.resourceStore)
                }

                this.resourceStore      = store

                if (store) {
                    store.setProject(this)
                    this.stm.addStore(store)
                    this.joinStoreRecords(store)
                }
            //}
        }


        setAssignmentStore (store : AssignmentStoreMixin) {
            //if (this.assignmentStore !== store) {

                if (this.assignmentStore && this.stm.hasStore(this.assignmentStore)) {
                    this.stm.removeStore(this.assignmentStore)
                }

                this.assignmentStore    = store

                if (store) {
                    store.setProject(this)
                    this.stm.addStore(store)
                    this.joinStoreRecords(store)
                }
            //}
        }


        setCalendarManagerStore (store : CalendarManagerStoreMixin) {
            //if (this.calendarManagerStore !== store) {

                if (this.calendarManagerStore && this.stm.hasStore(this.calendarManagerStore)) {
                    this.stm.removeStore(this.calendarManagerStore)
                }

                this.calendarManagerStore       = store

                if (store) {
                    store.setProject(this)
                    this.stm.addStore(store)
                    this.joinStoreRecords(store)
                }
            //}
        }


        // the value of this atom is used for the project's constraint interval
        * calculateStartDateInitial () : ChronoIterator<Date> {
            const proposedValue = this.$.startDate.proposedValue

            if (proposedValue !== undefined) return proposedValue

            return this.$.startDate.getConsistentValue() ? this.startDate : yield* this.calculateInitialMinChildrenStartDateDeep()
        }


        * calculateEndDateInitial () : ChronoIterator<Date> {
            const proposedValue = this.$.endDate.proposedValue

            if (proposedValue !== undefined) return proposedValue

            return this.$.endDate.getConsistentValue()
        }


        protected * calculateEarlyStartDateConstraintIntervals () : ChronoIterator<this[ 'startDateConstraintIntervals' ]> {
            const intervals         = []

            const startDate : Date = yield this.$.startDateInitial

            // TODO: need to think about resolution options
            startDate && intervals.push(ConstraintInterval.new({
                originDescription : 'the project start date',
                startDate
            }))

            return intervals
        }


        protected * calculateEarlyEndDateConstraintIntervals () : ChronoIterator<this[ 'endDateConstraintIntervals' ]> {
            // TODO for backward scheduling behavior is the opposite - `startDateConstraintIntervals` should
            // be empty and end date constrained by the project end date
            return []
        }


        protected * calculateLateStartDateConstraintIntervals () : ChronoIterator<this[ 'endDateConstraintIntervals' ]> {
            return []
        }


        protected * calculateLateEndDateConstraintIntervals () : ChronoIterator<this[ 'endDateConstraintIntervals' ]> {
            const intervals = []

            const endDate : Date = yield this.$.endDateInitial

            // TODO: need to think about resolution options
            endDate && intervals.push(ConstraintInterval.new({
                originDescription : 'the project end date',
                endDate
            }))

            return intervals
        }

        // Prevents start date calculation
        // TODO: this should be done for forward scheduled projects only
        * calculateStartDate (proposedValue : Date) : ChronoIterator<Date | boolean> {
            return proposedValue || this.$.startDate.getConsistentValue() || (yield this.$.startDateInitial)
        }


        @calculate('criticalPaths')
        * calculateCriticalPaths () : ChronoIterator<CriticalPath[]> {
            const paths : CriticalPath[]                      = [],
                pathsToProcess : CriticalPath[]               = [],
                events : Set<HasChildren & HasDependencies>   = yield this.$.childEvents

            const eventsToProcess = [...events]

            const projectEndDate = yield this.$.endDate

            // First collect events we'll start collecting paths from.
            // We need to start from critical events w/o incoming dependencies
            let event

            while ((event = eventsToProcess.shift())) {
                const childEvents : Set<HasChildren & HasDependencies> = yield event.$.childEvents,
                    eventIsCritical : boolean                          = yield event.$.critical

                // register a new path finishing at the event
                if (event.endDate - projectEndDate === 0 && eventIsCritical) {
                    pathsToProcess.push([{ event }])
                }

                eventsToProcess.push(...childEvents)
            }

            let path : CriticalPath

            // fetch paths one by one and process
            while ((path = pathsToProcess.shift())) {
                let taskIndex : number = path.length - 1,
                    node : CriticalPathNode

                // get the path last event
                while ((node = path[taskIndex])) {

                    const criticalPredecessorNodes : CriticalPathNode[] = []

                    // collect critical successors
                    for (const dependency of (yield node.event.$.incomingDeps)) {
                        const event : HasDependencies = yield dependency.$.fromEvent,
                            eventIsCritical : boolean = event && (yield event.$.critical)

                        // if we found a critical successor
                        if (eventIsCritical) {
                            criticalPredecessorNodes.push({ event, dependency })
                        }
                    }

                    // if critical successor(s) found
                    if (criticalPredecessorNodes.length) {
                        // make a copy of the path leading part
                        const pathCopy = path.slice()

                        // append the found successor to the path
                        path.push(criticalPredecessorNodes[0])

                        // if we found more than one successor we start new path as: leading path + successor
                        for (var i = 1; i < criticalPredecessorNodes.length; i++) {
                            pathsToProcess.push(pathCopy.concat(criticalPredecessorNodes[i]))
                        }

                        // increment counter to process the successor we've appended to the path
                        taskIndex++
                    }
                    else {
                        // no successors -> stop the loop
                        taskIndex = -1
                    }
                }

                // we collected the path backwards so let's reverse it
                paths.push(path.reverse())
            }

            return paths
        }

    }

    return ProjectMixin
}


/**
 * Project mixin type.
 *
 * Project serves as a central place for all data in the gantt chart, like events and dependencies collections.
 * It is a `Model` instance itself, so it can also contain any other project-wide configuration options.
 */
export interface ProjectMixin extends Mixin<typeof ProjectMixin> {}


/**
 * Function to build a minimal possible [[ProjectMixin]] class
 */
export const BuildMinimalProject = (base : typeof Model = Model) : MixinConstructor<typeof ProjectMixin> =>
    (ProjectMixin as any)(
    HasAssignments(
    HasChildren(
    // HasDependencies( // strictly speaking this mixin is not listed in the ProjectMixin requirements
    ConstrainedEvent(
    EventMixin(
    HasCalendarMixin(
    PartOfProjectMixin(
    PartOfProjectGenericMixin(
    ChronoModelMixin(
    Entity(
    Events(
        base
    )))))))))))


// TODO this should be renamed to BryntumProject
/**
 * Minimal possible [[ProjectMixin]] class.
 */
export class MinimalProject extends BuildMinimalProject() {
    eventModelClass     : AnyConstructor<BryntumEvent>
}
