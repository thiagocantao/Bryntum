var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Entity, calculate, field } from "../../../ChronoGraph/replica/Entity.js";
import { MinimalReplica } from "../../../ChronoGraph/replica/Replica.js";
import { Schema } from "../../../ChronoGraph/schema/Schema.js";
import Model from "../../../Common/data/Model.js";
import StateTrackingManager from "../../../Common/data/stm/StateTrackingManager.js";
import Events from "../../../Common/mixin/Events.js";
import { MinimalCalendarManagerStore } from "../../calendar/CalendarManagerStoreMixin.js";
import { MinimalCalendar } from "../../calendar/CalendarMixin.js";
import { model_field } from "../../chrono/ModelFieldAtom.js";
import { EngineReplica } from "../../chrono/Replica.js";
import { DependenciesCalendar } from "../../scheduling/Types.js";
import { PartOfProjectGenericMixin } from "../PartOfProjectGenericMixin.js";
import { MinimalAssignmentStore } from "../store/AssignmentStoreMixin.js";
import { MinimalDependencyStore } from "../store/DependencyStoreMixin.js";
import { MinimalEventStore } from "../store/EventStoreMixin.js";
import { MinimalResourceStore } from "../store/ResourceStoreMixin.js";
import { MinimalAssignment } from "./AssignmentMixin.js";
import { MinimalDependency } from "./DependencyMixin.js";
import { BryntumEvent } from "./event/BryntumEvent.js";
import { ConstrainedEvent, ConstraintInterval } from "./event/ConstrainedEvent.js";
import { EventMixin } from "./event/EventMixin.js";
import { HasAssignments } from "./event/HasAssignments.js";
import { HasChildren } from "./event/HasChildren.js";
import { HasCalendarMixin } from "./HasCalendarMixin.js";
import { ChronoModelMixin } from "./mixin/ChronoModelMixin.js";
import { PartOfProjectMixin } from "./mixin/PartOfProjectMixin.js";
import { MinimalResource } from "./ResourceMixin.js";
export const ProjectMixin = (base) => {
    class ProjectMixin extends base {
        construct(config = {}) {
            this.replica = EngineReplica(MinimalReplica).new({ project: this, schema: Schema.new() });
            // Expand project by default to make getRange to work
            if (!config.hasOwnProperty('expanded')) {
                config.expanded = true;
            }
            const hasInlineStore = Boolean(config.calendarManagerStore || config.eventStore || config.dependencyStore || config.resourceStore || config.assignmentStore);
            super.construct(config);
            if (!this.eventModelClass)
                this.eventModelClass = BryntumEvent;
            if (!this.eventStoreClass)
                this.eventStoreClass = MinimalEventStore;
            if (!this.dependencyModelClass)
                this.dependencyModelClass = MinimalDependency;
            if (!this.dependencyStoreClass)
                this.dependencyStoreClass = MinimalDependencyStore;
            if (!this.resourceModelClass)
                this.resourceModelClass = MinimalResource;
            if (!this.resourceStoreClass)
                this.resourceStoreClass = MinimalResourceStore;
            if (!this.assignmentModelClass)
                this.assignmentModelClass = MinimalAssignment;
            if (!this.assignmentStoreClass)
                this.assignmentStoreClass = MinimalAssignmentStore;
            if (!this.calendarModelClass)
                this.calendarModelClass = MinimalCalendar;
            if (!this.calendarManagerStoreClass)
                this.calendarManagerStoreClass = MinimalCalendarManagerStore;
            this.replica.addEntity(this);
            this.stm = new StateTrackingManager({ disabled: true });
            if (this.calendarManagerStore) {
                this.setCalendarManagerStore(this.calendarManagerStore);
            }
            else
                this.calendarManagerStore = new this.calendarManagerStoreClass({
                    modelClass: this.calendarModelClass,
                    idField: 'id',
                    project: this,
                    stm: this.stm
                });
            // not part of the CalendarManagerStore intentionally, not persisted
            this.defaultCalendar = new this.calendarManagerStore.modelClass({
                hoursPerDay: 24,
                daysPerWeek: 7,
                daysPerMonth: 30,
                unspecifiedTimeIsWorking: this.unspecifiedTimeIsWorking
            });
            this.defaultCalendar.project = this;
            if (this.eventStore) {
                // a valid use case for accessor?
                this.setEventStore(this.eventStore);
            }
            else
                this.eventStore = new this.eventStoreClass({
                    modelClass: this.eventModelClass,
                    tree: true,
                    idField: 'id',
                    project: this,
                    stm: this.stm
                });
            if (this.dependencyStore) {
                this.setDependencyStore(this.dependencyStore);
            }
            else
                this.dependencyStore = new this.dependencyStoreClass({
                    modelClass: this.dependencyModelClass,
                    idField: 'id',
                    project: this,
                    stm: this.stm
                });
            if (this.resourceStore) {
                this.setResourceStore(this.resourceStore);
            }
            else
                this.resourceStore = new this.resourceStoreClass({
                    modelClass: this.resourceModelClass,
                    idField: 'id',
                    project: this,
                    stm: this.stm
                });
            if (this.assignmentStore) {
                this.setAssignmentStore(this.assignmentStore);
            }
            else
                this.assignmentStore = new this.assignmentStoreClass({
                    modelClass: this.assignmentModelClass,
                    idField: 'id',
                    project: this,
                    stm: this.stm
                });
            const hasInlineData = Boolean(this.calendarsData || this.eventsData || this.dependenciesData || this.resourcesData || this.assignmentsData);
            if (hasInlineData) {
                this.loadInlineData({
                    calendarsData: this.calendarsData,
                    eventsData: this.eventsData,
                    dependenciesData: this.dependenciesData,
                    resourcesData: this.resourcesData,
                    assignmentsData: this.assignmentsData
                });
                delete this.calendarsData;
                delete this.eventsData;
                delete this.dependenciesData;
                delete this.resourcesData;
                delete this.assignmentsData;
            }
            // TODO this should be the same propagate as in "loadInlineData"
            // or at least fire same side effects
            if (hasInlineStore && !hasInlineData)
                this.propagate();
        }
        loadInlineData(data) {
            if (data.calendarsData) {
                this.calendarManagerStore.data = data.calendarsData;
            }
            if (data.eventsData) {
                this.eventStore.data = data.eventsData;
            }
            if (data.dependenciesData) {
                this.dependencyStore.data = data.dependenciesData;
            }
            if (data.resourcesData) {
                this.resourceStore.data = data.resourcesData;
            }
            if (data.assignmentsData) {
                this.assignmentStore.data = data.assignmentsData;
            }
            return this.propagate();
        }
        getGraph() {
            return this.replica;
        }
        getStm() {
            return this.stm;
        }
        calculateProject() {
            return this;
        }
        *calculateCalendar(proposedValue) {
            let result = yield* super.calculateCalendar(proposedValue);
            // fallback to defaultCalendar
            if (!result) {
                result = this.defaultCalendar;
                yield result.$$;
            }
            return result;
        }
        joinStoreRecords(store) {
            const fn = (record) => {
                record.setProject(this);
                record.joinProject();
            };
            if (store.rootNode) {
                store.rootNode.traverse(fn);
            }
            else {
                store.forEach(fn);
            }
        }
        setEventStore(store) {
            //if (this.eventStore !== store) {
            if (this.eventStore && this.stm.hasStore(this.eventStore)) {
                this.stm.removeStore(this.eventStore);
            }
            this.eventStore = store;
            if (store) {
                store.setProject(this);
                this.stm.addStore(store);
                // we've been given an event store from the outside
                // need to change its root node to be the project
                if (store.rootNode !== this) {
                    this.appendChild(store.rootNode.children || []);
                    store.rootNode = this;
                }
                this.joinStoreRecords(store);
            }
            //}
        }
        setDependencyStore(store) {
            //if (this.dependencyStore !== store) {
            if (this.dependencyStore && this.stm.hasStore(this.dependencyStore)) {
                this.stm.removeStore(this.dependencyStore);
            }
            this.dependencyStore = store;
            if (store) {
                store.setProject(this);
                this.stm.addStore(store);
                this.joinStoreRecords(store);
            }
            //}
        }
        setResourceStore(store) {
            //if (this.resourceStore !== store) {
            if (this.resourceStore && this.stm.hasStore(this.resourceStore)) {
                this.stm.removeStore(this.resourceStore);
            }
            this.resourceStore = store;
            if (store) {
                store.setProject(this);
                this.stm.addStore(store);
                this.joinStoreRecords(store);
            }
            //}
        }
        setAssignmentStore(store) {
            //if (this.assignmentStore !== store) {
            if (this.assignmentStore && this.stm.hasStore(this.assignmentStore)) {
                this.stm.removeStore(this.assignmentStore);
            }
            this.assignmentStore = store;
            if (store) {
                store.setProject(this);
                this.stm.addStore(store);
                this.joinStoreRecords(store);
            }
            //}
        }
        setCalendarManagerStore(store) {
            //if (this.calendarManagerStore !== store) {
            if (this.calendarManagerStore && this.stm.hasStore(this.calendarManagerStore)) {
                this.stm.removeStore(this.calendarManagerStore);
            }
            this.calendarManagerStore = store;
            if (store) {
                store.setProject(this);
                this.stm.addStore(store);
                this.joinStoreRecords(store);
            }
            //}
        }
        // the value of this atom is used for the project's constraint interval
        *calculateStartDateInitial() {
            const proposedValue = this.$.startDate.proposedValue;
            if (proposedValue !== undefined)
                return proposedValue;
            return this.$.startDate.getConsistentValue() ? this.startDate : yield* this.calculateInitialMinChildrenStartDateDeep();
        }
        *calculateEndDateInitial() {
            const proposedValue = this.$.endDate.proposedValue;
            if (proposedValue !== undefined)
                return proposedValue;
            return this.$.endDate.getConsistentValue();
        }
        *calculateEarlyStartDateConstraintIntervals() {
            const intervals = [];
            const startDate = yield this.$.startDateInitial;
            // TODO: need to think about resolution options
            startDate && intervals.push(ConstraintInterval.new({
                originDescription: 'the project start date',
                startDate
            }));
            return intervals;
        }
        *calculateEarlyEndDateConstraintIntervals() {
            // TODO for backward scheduling behavior is the opposite - `startDateConstraintIntervals` should
            // be empty and end date constrained by the project end date
            return [];
        }
        *calculateLateStartDateConstraintIntervals() {
            return [];
        }
        *calculateLateEndDateConstraintIntervals() {
            const intervals = [];
            const endDate = yield this.$.endDateInitial;
            // TODO: need to think about resolution options
            endDate && intervals.push(ConstraintInterval.new({
                originDescription: 'the project end date',
                endDate
            }));
            return intervals;
        }
        // Prevents start date calculation
        // TODO: this should be done for forward scheduled projects only
        *calculateStartDate(proposedValue) {
            return proposedValue || this.$.startDate.getConsistentValue() || (yield this.$.startDateInitial);
        }
        *calculateCriticalPaths() {
            const paths = [], pathsToProcess = [], events = yield this.$.childEvents;
            const eventsToProcess = [...events];
            const projectEndDate = yield this.$.endDate;
            // First collect events we'll start collecting paths from.
            // We need to start from critical events w/o incoming dependencies
            let event;
            while ((event = eventsToProcess.shift())) {
                const childEvents = yield event.$.childEvents, eventIsCritical = yield event.$.critical;
                // register a new path finishing at the event
                if (event.endDate - projectEndDate === 0 && eventIsCritical) {
                    pathsToProcess.push([{ event }]);
                }
                eventsToProcess.push(...childEvents);
            }
            let path;
            // fetch paths one by one and process
            while ((path = pathsToProcess.shift())) {
                let taskIndex = path.length - 1, node;
                // get the path last event
                while ((node = path[taskIndex])) {
                    const criticalPredecessorNodes = [];
                    // collect critical successors
                    for (const dependency of (yield node.event.$.incomingDeps)) {
                        const event = yield dependency.$.fromEvent, eventIsCritical = event && (yield event.$.critical);
                        // if we found a critical successor
                        if (eventIsCritical) {
                            criticalPredecessorNodes.push({ event, dependency });
                        }
                    }
                    // if critical successor(s) found
                    if (criticalPredecessorNodes.length) {
                        // make a copy of the path leading part
                        const pathCopy = path.slice();
                        // append the found successor to the path
                        path.push(criticalPredecessorNodes[0]);
                        // if we found more than one successor we start new path as: leading path + successor
                        for (var i = 1; i < criticalPredecessorNodes.length; i++) {
                            pathsToProcess.push(pathCopy.concat(criticalPredecessorNodes[i]));
                        }
                        // increment counter to process the successor we've appended to the path
                        taskIndex++;
                    }
                    else {
                        // no successors -> stop the loop
                        taskIndex = -1;
                    }
                }
                // we collected the path backwards so let's reverse it
                paths.push(path.reverse());
            }
            return paths;
        }
    }
    __decorate([
        model_field({ type: 'boolean', defaultValue: true })
    ], ProjectMixin.prototype, "unspecifiedTimeIsWorking", void 0);
    __decorate([
        model_field({ type: 'string', defaultValue: DependenciesCalendar.ToEvent })
    ], ProjectMixin.prototype, "dependenciesCalendar", void 0);
    __decorate([
        model_field({ type: 'boolean', defaultValue: true })
    ], ProjectMixin.prototype, "autoCalculatePercentDoneForParentTasks", void 0);
    __decorate([
        field()
    ], ProjectMixin.prototype, "criticalPaths", void 0);
    __decorate([
        calculate('criticalPaths')
    ], ProjectMixin.prototype, "calculateCriticalPaths", null);
    return ProjectMixin;
};
/**
 * Function to build a minimal possible [[ProjectMixin]] class
 */
export const BuildMinimalProject = (base = Model) => ProjectMixin(HasAssignments(HasChildren(
// HasDependencies( // strictly speaking this mixin is not listed in the ProjectMixin requirements
ConstrainedEvent(EventMixin(HasCalendarMixin(PartOfProjectMixin(PartOfProjectGenericMixin(ChronoModelMixin(Entity(Events(base)))))))))));
// TODO this should be renamed to BryntumProject
/**
 * Minimal possible [[ProjectMixin]] class.
 */
export class MinimalProject extends BuildMinimalProject() {
}
