var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Base, Mixin } from "../../../../ChronoGraph/class/BetterMixin.js";
import { calculate, Entity, field } from "../../../../ChronoGraph/replica/Entity.js";
import { CalendarIntervalMixin } from "../../../calendar/CalendarIntervalMixin.js";
import { CalendarIntervalStore } from "../../../calendar/CalendarIntervalStore.js";
import { model_field } from "../../../chrono/ModelFieldAtom.js";
import { BaseCalendarMixin } from "../scheduler_basic/BaseCalendarMixin.js";
import { BaseResourceMixin } from "../scheduler_basic/BaseResourceMixin.js";
export class ResourceAllocationEventRangeCalendarIntervalMixin extends CalendarIntervalMixin {
    // @model_field({ type : 'boolean', defaultValue : true })
    // isWorking : boolean
    // Calendar classes not entering graph, thus not using @model_field
    static get fields() {
        return [
            { name: 'isWorking', type: 'boolean', defaultValue: true }
        ];
    }
}
export class ResourceAllocationEventRangeCalendarIntervalStore extends CalendarIntervalStore {
    static get defaultConfig() {
        return {
            modelClass: ResourceAllocationEventRangeCalendarIntervalMixin
        };
    }
}
export class ResourceAllocationEventRangeCalendar extends BaseCalendarMixin {
    get intervalStoreClass() {
        return ResourceAllocationEventRangeCalendarIntervalStore;
    }
}
__decorate([
    model_field({ type: 'boolean', defaultValue: false })
], ResourceAllocationEventRangeCalendar.prototype, "unspecifiedTimeIsWorking", void 0);
export class BaseAllocationInterval extends Base {
    constructor() {
        super(...arguments);
        /**
         * Effort in the [[tick|interval]] in milliseconds.
         */
        this.effort = 0;
        /**
         * Utilization level of the resource (or the assignment if the interval represents the one) in percent.
         */
        this.units = 0;
    }
}
export class AssignmentAllocationInterval extends BaseAllocationInterval {
    constructor() {
        super(...arguments);
        /**
         * Indicates if the interval is in the middle of the event timespan.
         */
        this.inEventTimeSpan = false;
    }
}
/**
 * Resource allocation information for a certain tick.
 */
export class ResourceAllocationInterval extends BaseAllocationInterval {
    constructor() {
        super(...arguments);
        /**
         * Maximum possible effort in the [[tick|interval]] in milliseconds.
         */
        this.maxEffort = 0;
        /**
         * Indicates that the resource (or the assignment if the interval represents the one) is over-allocated in the [[tick|interval]].
         * So `true` when [[effort]] is more than [[maxEffort|possible maximum]].
         */
        this.isOverallocated = false;
        /**
         * Indicates that the resource (or assignment if the interval represents the one) is under-allocated in the [[tick|interval]].
         * So `true` when [[effort]] is less than [[maxEffort|possible maximum]].
         */
        this.isUnderallocated = false;
        /**
         * Indicates if the interval is in the middle of the event timespan.
         */
        this.inEventTimeSpan = false;
        /**
         * Resource assignments ingoing in the [[tick|interval]].
         */
        this.assignments = null;
        this.assignmentIntervals = null;
    }
}
export class BaseAllocationInfo extends Entity.mix(Base) {
    getDefaultAllocationIntervalClass() {
        return BaseAllocationInterval;
    }
    initialize(props) {
        props = Object.assign({
            includeInactiveEvents: false,
            allocationIntervalClass: this.getDefaultAllocationIntervalClass()
        }, props);
        super.initialize(props);
    }
}
__decorate([
    field()
], BaseAllocationInfo.prototype, "includeInactiveEvents", void 0);
__decorate([
    field()
], BaseAllocationInfo.prototype, "allocation", void 0);
/**
 * Class implementing _resource allocation report_ - a data representing the provided [[resource]]
 * utilization in the provided period of time.
 * The data is grouped by the provided [[ticks|time intervals]]
 */
export class ResourceAllocationInfo extends BaseAllocationInfo {
    enterGraph(graph) {
        super.enterGraph(graph);
    }
    leaveGraph(graph) {
        if (this.eventRangesCalendar) {
            this.resource?.getProject().clearCombinationsWith(this.eventRangesCalendar);
        }
        super.leaveGraph(graph);
        this.resource?.entities.delete(this);
    }
    getDefaultAllocationIntervalClass() {
        return ResourceAllocationInterval;
    }
    *shouldIncludeAssignmentInAllocation(assignment) {
        const event = yield assignment.$.event, includeInactiveEvents = yield this.$.includeInactiveEvents, inactive = event && (yield event.$.inactive), // includeInactiveEvents
        startDate = event && (yield event.$.startDate), endDate = event && (yield event.$.endDate);
        return Boolean(event && startDate && endDate && (includeInactiveEvents || !inactive));
    }
    *calculateAllocation() {
        const total = [], ticksCalendar = yield this.ticks, resource = yield this.$.resource, includeInactiveEvents = yield this.$.includeInactiveEvents, assignments = yield resource.$.assigned, calendar = yield resource.$.effectiveCalendar, assignmentsByCalendar = new Map(), eventRanges = [], assignmentTicksData = new Map(), byAssignments = new Map();
        let hasIgnoreResourceCalendarEvent = false, weightedUnitsSum, weightsSum, lastTick;
        // collect the resource assignments into assignmentsByCalendar map
        for (const assignment of assignments) {
            // skip missing or unscheduled event assignments
            if (!(yield* this.shouldIncludeAssignmentInAllocation(assignment)))
                continue;
            // we're going to need up-to-date assignment "units" below in this method ..so we yield it here
            yield assignment.$.units;
            const event = yield assignment.$.event;
            const ignoreResourceCalendar = yield event.$.ignoreResourceCalendar;
            const startDate = yield event.$.startDate;
            const endDate = yield event.$.endDate;
            const segments = yield event.$.segments;
            const eventCalendar = yield event.$.effectiveCalendar;
            hasIgnoreResourceCalendarEvent = hasIgnoreResourceCalendarEvent || ignoreResourceCalendar;
            // if the event is segmented collect segment ranges
            if (segments) {
                for (const segment of segments) {
                    const startDate = yield segment.$.startDate;
                    const endDate = yield segment.$.endDate;
                    eventRanges.push({ startDate, endDate, assignment });
                }
            }
            else {
                eventRanges.push({ startDate, endDate, assignment });
            }
            let assignments = assignmentsByCalendar.get(eventCalendar);
            if (!assignments) {
                assignments = [];
                assignmentsByCalendar.set(eventCalendar, assignments);
            }
            assignmentTicksData.set(assignment, new Map());
            byAssignments.set(assignment, []);
            assignments.push(assignment);
        }
        if (this.eventRangesCalendar) {
            this.resource?.getProject().clearCombinationsWith(this.eventRangesCalendar);
        }
        const eventRangesCalendar = this.eventRangesCalendar = new ResourceAllocationEventRangeCalendar({ intervals: eventRanges });
        // Provide extra calendars:
        // 1) a calendar containing list of ticks to group the resource allocation by
        // 2) a calendar containing list of assigned event start/end ranges
        // 3) assigned task calendars
        const calendars = [ticksCalendar, eventRangesCalendar, ...assignmentsByCalendar.keys()];
        const ticksData = new Map();
        // Initialize the resulting array with empty items
        ticksCalendar.intervalStore.forEach(tick => {
            const tickData = ResourceAllocationInterval.new({ tick, resource });
            ticksData.set(tick, tickData);
            total.push(tickData);
            assignmentTicksData.forEach((ticksData, assignment) => {
                const assignmentTickData = AssignmentAllocationInterval.new({ tick, assignment });
                ticksData.set(tick, assignmentTickData);
                byAssignments.get(assignment).push(assignmentTickData);
            });
        });
        const startDate = total[0].tick.startDate, endDate = total[total.length - 1].tick.endDate, iterationOptions = {
            startDate,
            endDate,
            calendars,
            includeNonWorkingIntervals: hasIgnoreResourceCalendarEvent,
        }, ticksTotalDuration = endDate.getTime() - startDate.getTime();
        // provide extended maxRange if total ticks duration is greater than it
        if (ticksTotalDuration > resource.getProject().maxCalendarRange) {
            iterationOptions.maxRange = ticksTotalDuration;
        }
        yield* resource.forEachAvailabilityInterval(iterationOptions, (intervalStartDate, intervalEndDate, intervalData) => {
            const isWorkingCalendar = intervalData.getCalendarsWorkStatus();
            // We are inside a tick interval and it's a working time according
            // to a resource calendar
            if (isWorkingCalendar.get(ticksCalendar)) {
                const tick = intervalData.intervalsByCalendar.get(ticksCalendar)[0], intervalDuration = intervalEndDate.getTime() - intervalStartDate.getTime(), tickData = ticksData.get(tick), tickAssignments = tickData.assignments || new Set(), tickAssignmentIntervals = tickData.assignmentIntervals || new Map();
                // Check previous tick
                if (lastTick && lastTick !== tick) {
                    const lastTicksData = ticksData.get(lastTick);
                    // last attempt to detect underallocation in the tick - check if its collected effort is less than its maxEffort
                    lastTicksData.isUnderallocated = lastTicksData.isUnderallocated || (lastTicksData.effort && lastTicksData.effort < lastTicksData.maxEffort);
                }
                // remember last tick processed
                lastTick = tick;
                if (!tickData.assignments) {
                    weightedUnitsSum = 0;
                    weightsSum = 0;
                }
                let units = 0, intervalHasAssignments = false, duration, intervalEffort = 0;
                // for each event intersecting the interval
                intervalData.intervalsByCalendar.get(eventRangesCalendar).forEach((interval) => {
                    const assignment = interval.assignment;

                    const event = assignment?.event;
                    // if event is performing in the interval
                    if (event &&
                        isWorkingCalendar.get(event.effectiveCalendar) &&
                        ( /* !hasIgnoreResourceCalendarEvent || */event.ignoreResourceCalendar || isWorkingCalendar.get(calendar))) {
                        // constrain the event start/end with the tick borders
                        const workingStartDate = Math.max(intervalStartDate.getTime(), assignment.event.startDate.getTime());
                        const workingEndDate = Math.min(intervalEndDate.getTime(), assignment.event.endDate.getTime());
                        intervalHasAssignments = true;
                        duration = workingEndDate - workingStartDate;
                        const assignmentInterval = assignmentTicksData.get(assignment).get(tick);
                        const assignmentEffort = duration * assignment.units / 100;
                        assignmentInterval.effort += assignmentEffort;
                        assignmentInterval.units = assignment.units;
                        assignmentInterval.inEventTimeSpan = true;
                        intervalEffort += assignmentEffort;
                        // collect total resource usage percent in the current interval
                        units += assignment.units;
                        tickAssignments.add(assignment);
                        tickAssignmentIntervals.set(assignment, assignmentInterval);
                    }
                });
                tickData.inEventTimeSpan = tickData.inEventTimeSpan || intervalHasAssignments;
                // maxEffort represents the resource calendar intervals
                if (isWorkingCalendar.get(calendar)) {
                    tickData.maxEffort += intervalDuration;
                }
                // if we have assignments running in the interval - calculate average allocation %
                if (units) {
                    if (duration) {
                        // keep weightedUnitsSum & weightsSum since there might be another intervals in the tick
                        weightedUnitsSum += duration * units;
                        weightsSum += duration;
                        // "units" weighted arithmetic mean w/ duration values as weights
                        tickData.units = weightedUnitsSum / weightsSum;
                    }
                    else if (!weightedUnitsSum) {
                        tickData.units = units;
                    }
                }
                if (intervalHasAssignments) {
                    tickData.effort += intervalEffort;
                    tickData.assignments = tickAssignments;
                    tickData.assignmentIntervals = tickAssignmentIntervals;
                    tickData.isOverallocated = tickData.isOverallocated || tickData.effort > tickData.maxEffort || tickData.units > 100;
                    tickData.isUnderallocated = tickData.isUnderallocated || intervalEffort < intervalDuration || tickData.units < 100;
                }
            }
        });
        if (lastTick) {
            const lastTicksData = ticksData.get(lastTick);
            // last attempt to detect underallocation in the tick - check if its collected effort is less than its maxEffort
            lastTicksData.isUnderallocated = lastTicksData.isUnderallocated || (lastTicksData.effort && lastTicksData.effort < lastTicksData.maxEffort);
        }
        return {
            owner: this,
            resource,
            total,
            byAssignments
        };
    }
}
__decorate([
    field()
], ResourceAllocationInfo.prototype, "resource", void 0);
__decorate([
    calculate('allocation')
], ResourceAllocationInfo.prototype, "calculateAllocation", null);
/**
 * A mixin for the resource entity at the Scheduler Pro level.
 */
export class SchedulerProResourceMixin extends Mixin([BaseResourceMixin], (base) => {
    const superProto = base.prototype;
    class SchedulerProResourceMixin extends base {
        constructor() {
            super(...arguments);
            this.observers = new Set();
            this.entities = new Set();
        }
        addObserver(observer) {
            this.graph.addIdentifier(observer);
            this.observers.add(observer);
        }
        removeObserver(observer) {
            if (this.graph) {
                this.graph.removeIdentifier(observer);
            }
            this.observers.delete(observer);
        }
        addEntity(entity) {
            this.graph.addEntity(entity);
            this.entities.add(entity);
        }
        removeEntity(entity) {
            if (this.graph) {
                this.graph.removeEntity(entity);
            }
            this.entities.delete(entity);
        }
        leaveGraph(replica) {
            const { graph } = this;
            for (const observer of this.observers) {
                this.removeObserver(observer);
            }
            for (const entity of this.entities) {
                this.removeEntity(entity);
            }
            superProto.leaveGraph.call(this, replica);
        }
        *forEachAvailabilityInterval(options, func) {
            const project = this.getProject();
            const calendar = yield this.$.effectiveCalendar;
            const effectiveCalendarsCombination = project.combineCalendars([calendar].concat(options.calendars || []));
            const maxRange = project.maxCalendarRange;
            const includeNonWorkingIntervals = options.includeNonWorkingIntervals;
            if (maxRange) {
                options = Object.assign({ maxRange }, options);
            }
            return effectiveCalendarsCombination.forEachAvailabilityInterval(options, (startDate, endDate, calendarCacheIntervalMultiple) => {
                const calendarsStatus = calendarCacheIntervalMultiple.getCalendarsWorkStatus();
                if (includeNonWorkingIntervals || calendarsStatus.get(calendar)) {
                    return func(startDate, endDate, calendarCacheIntervalMultiple);
                }
            });
        }
    }
    return SchedulerProResourceMixin;
}) {
}
