var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { calculate, field, generic_field } from "../../../../ChronoGraph/replica/Entity.js";
import DateHelper from "../../../../Common/helper/DateHelper.js";
import { combineCalendars } from "../../../calendar/CalendarCacheMultiple.js";
import { model_field, ModelBucketField } from "../../../chrono/ModelFieldAtom.js";
import { SchedulingMode, TimeUnit } from "../../../scheduling/Types.js";
const hasMixin = Symbol('HasAssignments');
export const HasAssignments = (base) => {
    class HasAssignments extends base {
        constructor() {
            super(...arguments);
            this.assignmentsByCalendar = new Map();
        }
        [hasMixin]() { }
        get assignments() {
            return [...this.assigned];
        }
        *hasFirstAssignment() {
            const assignments = yield this.$.assigned;
            const hasAssignments = assignments.size > 0;
            const hadAssignments = this.$.assigned.value.size > 0;
            // console.log("hasFirstAssignment, ", hasAssignments && !hadAssignments)
            return hasAssignments && !hadAssignments;
        }
        *calculateEffortDriven(proposedValue) {
            const schedulingMode = yield this.$.schedulingMode;
            // Normal scheduling mode doesn't support effort driven flag
            if (schedulingMode == SchedulingMode.Normal)
                return false;
            return proposedValue !== undefined ? proposedValue : this.$.effortDriven.getConsistentValue();
        }
        // pure calculation methods
        *calculateTotalUnits() {
            let result = 0;
            const assignments = yield this.$.assigned;
            for (let assignment of assignments) {
                result += yield assignment.$.units;
            }
            return result;
        }
        *calculateEffortByAssignments(startDate, endDate) {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.Normal || assignmentsByCalendar.size === 0) {
                return yield* this.calculateProjectedDuration(startDate, endDate);
            }
            let resultN = 0;
            const totalUnitsByCalendar = new Map();
            for (const [calendar, assignments] of assignmentsByCalendar) {
                let intervalUnits = 0;
                for (const assignment of assignments) {
                    intervalUnits += (yield assignment.$.units);
                }
                totalUnitsByCalendar.set(calendar, intervalUnits);
            }
            const options = Object.assign(yield* this.getBaseOptionsForEffortCalculations(), { startDate, endDate });
            yield* this.forEachAvailabilityInterval(options, (intervalStart, intervalEnd, calendarCacheIntervalMultiple) => {
                const workCalendars = calendarCacheIntervalMultiple.getCalendarsWorking();
                const intervalStartN = intervalStart.getTime(), intervalEndN = intervalEnd.getTime(), intervalDuration = intervalEndN - intervalStartN;
                let intervalUnits = 0;
                for (const workingCalendar of workCalendars) {
                    // the calendar of the event itself will be in the `workCalendars`, but it
                    // will be missing in the `totalUnitsByCalendar` map, which is fine
                    intervalUnits += totalUnitsByCalendar.get(workingCalendar) || 0;
                }
                // Effort = Units * Duration
                resultN += intervalUnits * intervalDuration * 0.01;
            });
            return yield* this.$convertDuration(resultN, TimeUnit.Millisecond, yield this.$.effortUnit);
        }
        *calculateUnitsByDurationAndEffort(_assignment, proposedValue) {
            const effort = yield this.$.effort, effortUnit = yield this.$.effortUnit, effortMS = yield* this.$convertDuration(effort, effortUnit, TimeUnit.Millisecond);
            let collectedEffort = 0;
            const options = Object.assign(yield* this.getBaseOptionsForEffortCalculations(), { startDate: yield this.$.startDate, endDate: yield this.$.endDate });
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            yield* this.forEachAvailabilityInterval(options, (intervalStart, intervalEnd, calendarCacheIntervalMultiple) => {
                const workCalendars = calendarCacheIntervalMultiple.getCalendarsWorking();
                const intervalStartN = intervalStart.getTime(), intervalEndN = intervalEnd.getTime(), intervalDuration = intervalEndN - intervalStartN;
                for (const workingCalendar of workCalendars) {
                    collectedEffort +=
                        (assignmentsByCalendar.has(workingCalendar) ? assignmentsByCalendar.get(workingCalendar).length : 0) * intervalDuration;
                }
            });
            return collectedEffort ? 100 * effortMS / collectedEffort : 100;
        }
        // EOF pure calculation methods
        getEffort(unit) {
            const effort = this.effort;
            return unit !== undefined ? this.calendar.convertDuration(effort, this.effortUnit, unit) : effort;
        }
        async setEffort(effort, unit) {
            if (unit != null && unit !== this.effortUnit) {
                this.$.effortUnit.put(unit);
            }
            this.$.effort.put(effort);
            return this.propagate();
        }
        getEffortUnit() {
            return this.effortUnit;
        }
        setEffortUnit(_value) {
            throw new Error("Use `setEffort` instead");
        }
        async setAssignmentUnits(assignment, units) {
            assignment.$.units.put(units);
            return assignment.propagate();
        }
        *calculateAssignmentUnits(assignment, proposedValue) {
            if (proposedValue !== undefined)
                return proposedValue;
            const shouldRecalculateAssignmentUnits = yield* this.shouldRecalculateAssignmentUnits(assignment);
            if (shouldRecalculateAssignmentUnits) {
                return yield* this.calculateUnitsByDurationAndEffort(assignment, proposedValue);
            }
            else {
                // otherwise keep stable value
                return assignment.$.units.value;
            }
        }
        *calculateEffort(proposedValue) {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size) {
                return yield* this.calculateTotalEffort();
            }
            else {
                if (proposedValue !== undefined)
                    return proposedValue;
                const shouldRecalculateEffort = yield* this.shouldRecalculateEffort();
                if (shouldRecalculateEffort) {
                    // TODO can be smarter here, at least should handle the normalization of effort to duration case
                    const startDate = yield this.$.startDate;
                    if (startDate) {
                        const endDate = yield* this.calculateProjectedEndDate(startDate);
                        return yield* this.calculateEffortByAssignments(startDate, endDate);
                    }
                    else {
                        return null;
                    }
                }
                else {
                    // otherwise keep stable value
                    return this.$.effort.value;
                }
            }
        }
        *canRecalculateDuration() {
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.Normal) {
                return yield* super.canRecalculateDuration();
            }
            else {
                return this.$.effort.hasProposedValue() || this.$.effort.hasConsistentValue();
            }
        }
        *shouldRecalculateEffort() {
            const childEvents = yield this.$.childEvents;
            // Parent case
            if (childEvents.size > 0) {
                return true;
            }
            // Child case
            else {
                return !this.$.effort.hasConsistentValue() && (yield* this.canRecalculateEffort());
            }
        }
        *canRecalculateEffort() {
            const childEvents = yield this.$.childEvents;
            let result = true;
            if (childEvents.size) {
                result = true;
                // each child should be able to recalculate its effort
                // for (const child of childEvents) {
                //     result = result && (yield* child.canRecalculateEffort())
                // }
            }
            else {
                // even if event has no assignments (and thus no assignment units are available)
                // we still normalize effort to duration, so thats the only data required for effort calculation
                result = this.$.duration.hasValue();
            }
            return result;
        }
        *shouldRecalculateAssignmentUnits(_assignment) {
            return false;
        }
        *calculateTotalEffort() {
            const childEvents = yield this.$.childEvents;
            let result = 0;
            for (const child of childEvents) {
                result += yield child.$.effort;
            }
            return result;
        }
        *forEachAvailabilityInterval(options, func) {
            const calendar = yield this.$.calendar;
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const effectiveCalendarsCombination = yield this.$.effectiveCalendarsCombination;
            effectiveCalendarsCombination.forEachAvailabilityInterval(options, (startDate, endDate, calendarCacheIntervalMultiple) => {
                const calendarsStatus = calendarCacheIntervalMultiple.getCalendarsWorkStatus();
                const workCalendars = calendarCacheIntervalMultiple.getCalendarsWorking();
                if (calendarsStatus.get(calendar)
                    &&
                        (options.ignoreResourceCalendars || workCalendars.some((calendar) => assignmentsByCalendar.has(calendar)))) {
                    return func(startDate, endDate, calendarCacheIntervalMultiple);
                }
            });
        }
        *getBaseOptionsForEffortCalculations() {
            return { ignoreResourceCalendars: false };
        }
        *getBaseOptionsForDurationCalculations() {
            return { ignoreResourceCalendars: false };
        }
        *skipNonWorkingTime(date, isForward = true) {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            if (assignmentsByCalendar.size > 0) {
                const options = Object.assign(yield* this.getBaseOptionsForDurationCalculations(), isForward ? { startDate: date, isForward } : { endDate: date, isForward });
                let workingDate;
                yield* this.forEachAvailabilityInterval(options, (startDate, endDate, calendarCacheIntervalMultiple) => {
                    workingDate = isForward ? startDate : endDate;
                    return false;
                });
                return new Date(workingDate);
            }
            else {
                return yield* super.skipNonWorkingTime(date, isForward);
            }
        }
        *calculateDurationBetweenDates(startDate, endDate, unit) {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            if (assignmentsByCalendar.size > 0) {
                const options = Object.assign(yield* this.getBaseOptionsForDurationCalculations(), { startDate, endDate, isForward: true });
                let result = 0;
                yield* this.forEachAvailabilityInterval(options, (startDate, endDate) => {
                    result += endDate.getTime() - startDate.getTime();
                });
                return yield* this.$convertDuration(result, TimeUnit.Millisecond, unit);
            }
            else {
                return yield* super.calculateDurationBetweenDates(startDate, endDate, unit);
            }
        }
        *calculateEffectiveCalendarsCombination() {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const calendars = [...assignmentsByCalendar.keys(), yield this.$.calendar];
            return combineCalendars(calendars);
        }
        *calculateAssignmentsByCalendar() {
            const assignments = yield this.$.assigned;
            const result = new Map();
            for (const assignment of assignments) {
                const resource = yield assignment.$.resource;
                if (resource) {
                    const resourceCalendar = yield resource.$.calendar;
                    let assignments = result.get(resourceCalendar);
                    if (!assignments) {
                        assignments = [];
                        result.set(resourceCalendar, assignments);
                    }
                    assignments.push(assignment);
                }
            }
            return result;
        }
        // helper method for `Normal` scheduling mode
        *calculateProjectedXDateByDuration(baseDate, isForward = true) {
            const duration = yield this.$.duration;
            // temp fix, we might default to 1d duration
            if (duration == null || isNaN(duration))
                return null;
            if (duration == 0)
                return baseDate;
            const durationUnit = yield this.$.durationUnit;
            const durationMS = yield* this.$convertDuration(duration, durationUnit, TimeUnit.Millisecond);
            let resultN = baseDate.getTime();
            let leftDuration = durationMS;
            const calendar = yield this.$.calendar;
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            if (assignmentsByCalendar.size > 0) {
                const options = Object.assign(yield* this.getBaseOptionsForDurationCalculations(), isForward ? { startDate: baseDate, isForward } : { endDate: baseDate, isForward });
                yield* this.forEachAvailabilityInterval(options, (intervalStart, intervalEnd, calendarCacheIntervalMultiple) => {
                    const intervalStartN = intervalStart.getTime(), intervalEndN = intervalEnd.getTime(), intervalDuration = intervalEndN - intervalStartN;
                    if (intervalDuration >= leftDuration) {
                        resultN = isForward ? intervalStartN + leftDuration : intervalEndN - leftDuration;
                        return false;
                    }
                    else {
                        const dstDiff = intervalStart.getTimezoneOffset() - intervalEnd.getTimezoneOffset();
                        leftDuration -= intervalDuration + dstDiff * 60 * 1000;
                    }
                });
                return new Date(resultN);
            }
            else {
                return calendar.accumulateWorkingTime(baseDate, durationMS, TimeUnit.Millisecond, isForward).finalDate;
            }
        }
        *calculateProjectedXDateByEffort(baseDate, isForward = true) {
            const effort = yield this.$.effort, effortUnit = yield this.$.effortUnit, effortMS = yield* this.$convertDuration(effort, effortUnit, TimeUnit.Millisecond);
            let resultN = baseDate.getTime();
            let leftEffort = effortMS;
            const calendar = yield this.$.calendar;
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const totalUnitsByCalendar = new Map();
            for (const [calendar, assignments] of assignmentsByCalendar) {
                let intervalUnits = 0;
                for (const assignment of assignments) {
                    intervalUnits += (yield assignment.$.units);
                }
                totalUnitsByCalendar.set(calendar, intervalUnits);
            }
            if (assignmentsByCalendar.size > 0) {
                const options = Object.assign(yield* this.getBaseOptionsForDurationCalculations(), isForward ? { startDate: baseDate, isForward } : { endDate: baseDate, isForward });
                yield* this.forEachAvailabilityInterval(options, (intervalStart, intervalEnd, calendarCacheIntervalMultiple) => {
                    const workCalendars = calendarCacheIntervalMultiple.getCalendarsWorking();
                    const intervalStartN = intervalStart.getTime(), intervalEndN = intervalEnd.getTime(), intervalDuration = intervalEndN - intervalStartN;
                    let intervalUnits = 0;
                    for (const workingCalendar of workCalendars) {
                        // the calendar of the event itself will be in the `workCalendars`, but it
                        // will be missing in the `totalUnitsByCalendar` map, which is fine
                        intervalUnits += totalUnitsByCalendar.get(workingCalendar) || 0;
                    }
                    // Effort = Units * Duration
                    const intervalEffort = intervalUnits * intervalDuration * 0.01;
                    if (intervalEffort >= leftEffort) {
                        // resulting date is interval start plus left duration (Duration = Effort / Units)
                        resultN =
                            isForward
                                ?
                                    intervalStartN + leftEffort / (0.01 * intervalUnits)
                                :
                                    intervalEndN - leftEffort / (0.01 * intervalUnits);
                        return false;
                    }
                    else {
                        leftEffort -= intervalEffort;
                    }
                });
                return new Date(resultN);
            }
            else {
                return calendar.accumulateWorkingTime(baseDate, effortMS, TimeUnit.Millisecond, isForward).finalDate;
            }
        }
        *doCalculateDuration() {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const schedulingMode = yield this.$.schedulingMode;
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0
                && (schedulingMode === SchedulingMode.Normal || (assignmentsByCalendar.size === 0))
                && !this.$.duration.hasValue() && this.$.effort.hasValue()
            // possibly also: && !this.$.startDate.hasValue() || !this.$.endDate.hasValue()
            // meaning - we only normalize duration to effort if there's no start/end date
            // otherwise duration should be normalized by start/end date
            ) {
                return yield* this.$convertDuration(yield this.$.effort, yield this.$.effortUnit, yield this.$.durationUnit);
            }
            else {
                return yield* super.doCalculateDuration();
            }
        }
        *calculateProjectedDuration(startDate, endDate) {
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            const schedulingMode = yield this.$.schedulingMode;
            if (schedulingMode === SchedulingMode.Normal || assignmentsByCalendar.size === 0) {
                return yield* super.calculateProjectedDuration(startDate, endDate);
            }
            const effort = yield this.$.effort, effortUnit = yield this.$.effortUnit, effortMS = yield* this.$convertDuration(effort, effortUnit, TimeUnit.Millisecond);
            let resultN = 0;
            let leftEffort = effortMS;
            const totalUnitsByCalendar = new Map();
            for (const [calendar, assignments] of assignmentsByCalendar) {
                let intervalUnits = 0;
                for (const assignment of assignments) {
                    intervalUnits += (yield assignment.$.units);
                }
                totalUnitsByCalendar.set(calendar, intervalUnits);
            }
            const options = Object.assign(yield* this.getBaseOptionsForDurationCalculations(), { startDate, endDate });
            yield* this.forEachAvailabilityInterval({ startDate, endDate }, (intervalStart, intervalEnd, calendarCacheIntervalMultiple) => {
                const workCalendars = calendarCacheIntervalMultiple.getCalendarsWorking();
                const intervalStartN = intervalStart.getTime(), intervalEndN = intervalEnd.getTime(), intervalDuration = intervalEndN - intervalStartN;
                let intervalUnits = 0;
                for (const workingCalendar of workCalendars) {
                    // the calendar of the event itself will be in the `workCalendars`, but it
                    // will be missing in the `totalUnitsByCalendar` map, which is fine
                    intervalUnits += totalUnitsByCalendar.get(workingCalendar) || 0;
                }
                // Effort = Units * Duration
                const intervalEffort = intervalUnits * intervalDuration * 0.01;
                if (intervalEffort >= leftEffort) {
                    // increment result by left duration (Duration = Effort / Units)
                    resultN += leftEffort / (0.01 * intervalUnits);
                    return false;
                }
                else {
                    leftEffort -= intervalEffort;
                    resultN += intervalDuration;
                }
            });
            return yield* this.$convertDuration(resultN, TimeUnit.Millisecond, yield this.$.durationUnit);
        }
        *canCalculateProjectedXDate() {
            const useDuration = yield* this.useDurationForProjectedXDateCalculation(), durationIsMutating = yield* this.shouldRecalculateDuration();
            // TODO: need to add support of cases when we can calculate projected dates using Effort
            return useDuration && !durationIsMutating && (this.$.duration.hasProposedValue() || this.$.duration.hasConsistentValue());
        }
        *useDurationForProjectedXDateCalculation() {
            const schedulingMode = yield this.$.schedulingMode;
            const assignmentsByCalendar = yield this.$.assignmentsByCalendar;
            return (schedulingMode === SchedulingMode.Normal || assignmentsByCalendar.size === 0)
                // this means:
                // 1) this is not a initial data calculation (hasConsistentValue)
                // 2) there's a user input for duration (hasProposedValue)
                || (this.$.duration.hasProposedValue() && this.$.duration.hasConsistentValue());
        }
        *useDurationForProjectedStartDateCalculation() {
            return (yield* this.useDurationForProjectedXDateCalculation());
        }
        *useDurationForProjectedEndDateCalculation() {
            return (yield* this.useDurationForProjectedXDateCalculation());
        }
        *calculateProjectedStartDate(endDate) {
            if (yield* this.useDurationForProjectedStartDateCalculation()) {
                return yield* this.calculateProjectedXDateByDuration(endDate, false);
            }
            else {
                return yield* this.calculateProjectedXDateByEffort(endDate, false);
            }
        }
        *calculateProjectedEndDate(startDate) {
            if (yield* this.useDurationForProjectedEndDateCalculation()) {
                return yield* this.calculateProjectedXDateByDuration(startDate, true);
            }
            else {
                return yield* this.calculateProjectedXDateByEffort(startDate, true);
            }
        }
        /**
         * If a given resource is assigned to this task, returns a [[AssignmentMixin]] instance for it.
         * Otherwise returns `null`
         */
        getAssignmentFor(resource) {
            let result;
            this.assigned.forEach(assignment => {
                if (assignment.resource === resource)
                    result = assignment;
            });
            return result;
        }
        isAssignedTo(resource) {
            return Boolean(this.getAssignmentFor(resource));
        }
        reassign(oldResource, newResource) {
            const assignment = this.getAssignmentFor(oldResource);
            //<debug>
            if (!assignment)
                throw new Error(`Can't unassign resource \`${oldResource}\` from task \`${this}\` - resource is not assigned to the task!`);
            //</debug>
            this.removeAssignment(assignment);
            //<debug>
            // Preconditions:
            if (this.getAssignmentFor(newResource))
                throw new Error('Resource can\'t be assigned twice to the same task');
            //</debug>
            const assignmentCls = this.getProject().assignmentStore.modelClass;
            this.addAssignment(new assignmentCls({
                event: this,
                resource: newResource
            }));
            return this.propagate();
        }
        /**
         * A method which assigns a resource to the current event
         */
        async assign(resource, units = 100) {
            //<debug>
            // Preconditions:
            if (this.getAssignmentFor(resource))
                throw new Error('Resource can\'t be assigned twice to the same task');
            //</debug>
            const assignmentCls = this.getProject().assignmentStore.modelClass;
            this.addAssignment(new assignmentCls({
                event: this,
                resource: resource,
                units: units
            }));
            return this.propagate();
        }
        /**
         * A method which unassigns a resource from the current event
         */
        async unassign(resource) {
            const assignment = this.getAssignmentFor(resource);
            //<debug>
            if (!assignment)
                throw new Error(`Can't unassign resource \`${resource}\` from task \`${this}\` - resource is not assigned to the task!`);
            //</debug>
            this.removeAssignment(assignment);
            return this.propagate();
        }
        // template methods, overridden in scheduling modes mixins
        // should probably be named something like "onEventAssignmentAdded"
        // should be a listener for the `add` event of the assignment store instead
        addAssignment(assignment) {
            this.getProject().assignmentStore.add(assignment);
            return assignment;
        }
        // should be a listener for the `remove` event of the assignment store instead
        removeAssignment(assignment) {
            this.getProject().assignmentStore.remove(assignment);
            return assignment;
        }
        leaveProject() {
            const assignmentStore = this.getAssignmentStore();
            this.assigned.forEach(assignment => assignmentStore.remove(assignment));
            super.leaveProject();
        }
    }
    __decorate([
        model_field({ 'type': 'number', allowNull: true })
    ], HasAssignments.prototype, "effort", void 0);
    __decorate([
        model_field({ 'type': 'string', defaultValue: TimeUnit.Hour }, { converter: DateHelper.normalizeUnit })
    ], HasAssignments.prototype, "effortUnit", void 0);
    __decorate([
        model_field({ 'type': 'boolean', defaultValue: false })
    ], HasAssignments.prototype, "effortDriven", void 0);
    __decorate([
        generic_field({}, ModelBucketField)
    ], HasAssignments.prototype, "assigned", void 0);
    __decorate([
        field()
    ], HasAssignments.prototype, "effectiveCalendarsCombination", void 0);
    __decorate([
        field()
    ], HasAssignments.prototype, "assignmentsByCalendar", void 0);
    __decorate([
        calculate('effortDriven')
    ], HasAssignments.prototype, "calculateEffortDriven", null);
    __decorate([
        calculate('effort')
    ], HasAssignments.prototype, "calculateEffort", null);
    __decorate([
        calculate('effectiveCalendarsCombination')
    ], HasAssignments.prototype, "calculateEffectiveCalendarsCombination", null);
    __decorate([
        calculate('assignmentsByCalendar')
    ], HasAssignments.prototype, "calculateAssignmentsByCalendar", null);
    return HasAssignments;
};
/**
 * Event mixin type guard
 */
export const hasHasAssignments = (model) => Boolean(model && model[hasMixin]);
