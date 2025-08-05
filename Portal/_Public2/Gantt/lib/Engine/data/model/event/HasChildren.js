var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { bucket, reference } from "../../../../ChronoGraph/replica/Reference.js";
import { MAX_DATE, MIN_DATE } from "../../../util/Constants.js";
export const HasChildren = (base) => {
    class HasChildren extends base {
        *shouldRecalculateStartDate() {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0) {
                return yield* super.shouldRecalculateStartDate();
            }
            else {
                return false;
            }
        }
        *shouldRecalculateEndDate() {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0) {
                return yield* super.shouldRecalculateEndDate();
            }
            else {
                return false;
            }
        }
        *shouldRecalculateDuration() {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0) {
                return yield* super.shouldRecalculateDuration();
            }
            else {
                return true;
            }
        }
        *calculateEarlyStartDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MAX_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.earlyStartDateRaw;
                    if (childDate && childDate < result)
                        result = childDate;
                }
                result = result.getTime() - MAX_DATE.getTime() ? result : null;
            }
            else {
                result = yield* super.calculateEarlyStartDateRaw();
            }
            return result;
        }
        *calculateEarlyEndDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MIN_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.earlyEndDateRaw;
                    if (childDate && childDate > result)
                        result = childDate;
                }
                result = result.getTime() - MIN_DATE.getTime() ? result : null;
            }
            else {
                result = yield* super.calculateEarlyEndDateRaw();
            }
            return result;
        }
        *calculateLateStartDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MAX_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.lateStartDateRaw;
                    if (childDate && childDate < result)
                        result = childDate;
                }
                result = result.getTime() - MAX_DATE.getTime() ? result : null;
            }
            else {
                result = yield* super.calculateLateStartDateRaw();
            }
            return result;
        }
        *calculateLateEndDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MIN_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.lateEndDateRaw;
                    if (childDate && childDate > result)
                        result = childDate;
                }
                result = result.getTime() - MIN_DATE.getTime() ? result : null;
            }
            else {
                result = yield* super.calculateLateEndDateRaw();
            }
            return result;
        }
        *calculateStartDateConstraintIntervals() {
            // if (window.DEBUG) debugger
            const intervals = yield* super.calculateStartDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.startDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateEndDateConstraintIntervals() {
            // if (window.DEBUG) debugger
            const intervals = yield* super.calculateEndDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.endDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateEarlyStartDateConstraintIntervals() {
            // if (window.DEBUG) debugger
            const intervals = yield* super.calculateEarlyStartDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.earlyStartDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateEarlyEndDateConstraintIntervals() {
            // if (window.DEBUG) debugger
            const intervals = yield* super.calculateEarlyEndDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.earlyEndDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateLateStartDateConstraintIntervals() {
            // if (window.DEBUG) debugger
            const intervals = yield* super.calculateLateStartDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.lateStartDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateLateEndDateConstraintIntervals() {
            const intervals = yield* super.calculateLateEndDateConstraintIntervals();
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                // Child inherits its parent task constraints
                const parentIntervals = yield parentEvent.$.lateEndDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateStartDate(proposedValue) {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0) {
                return yield* super.calculateStartDate(proposedValue);
            }
            else {
                return yield* this.calculateMinChildrenStartDate();
            }
        }
        *calculateEndDate(proposedValue) {
            const childEvents = yield this.$.childEvents;
            if (childEvents.size === 0) {
                return yield* super.calculateEndDate(proposedValue);
            }
            else {
                return yield* this.calculateMaxChildrenEndDate();
            }
        }
        *calculateDuration(proposedValue) {
            const childEvents = yield this.$.childEvents;
            // for the "parent" case, ignore the proposed value
            return yield* super.calculateDuration(childEvents.size === 0 ? proposedValue : undefined);
        }
        *calculateMinChildrenStartDate() {
            const childEvents = yield this.$.childEvents;
            const childStartDates = [];
            for (let childEvent of childEvents) {
                childStartDates.push(yield childEvent.$.startDate);
            }
            let timestamp = childStartDates.reduce((acc, childStartDate) => childStartDate ? Math.min(acc, childStartDate.getTime()) : acc, MAX_DATE.getTime());
            if (timestamp === MIN_DATE.getTime() || timestamp === MAX_DATE.getTime())
                return null;
            return new Date(timestamp);
        }
        *calculateMaxChildrenEndDate() {
            const childEvents = yield this.$.childEvents;
            const childEndDates = [];
            for (let childEvent of childEvents) {
                childEndDates.push(yield childEvent.$.endDate);
            }
            let timestamp = childEndDates.reduce((acc, childEndDate) => childEndDate ? Math.max(acc, childEndDate.getTime()) : acc, MIN_DATE.getTime());
            if (timestamp === MIN_DATE.getTime() || timestamp === MAX_DATE.getTime())
                return null;
            return new Date(timestamp);
        }
        // this method is only used to calculated "initial" project start date only
        *calculateInitialMinChildrenStartDateDeep() {
            const childEvents = yield this.$.childEvents;
            // note, that we does not yield here, as we want to calculate "initial" project start date
            // which will be used only if there's no user input or explicit setting for it
            // such project date should be calculated as earliest date of all tasks, based on the
            // "initial" data (which includes proposed)
            if (!childEvents.size)
                return this.startDate;
            const childStartDates = [];
            for (let childEvent of childEvents) {
                const childInitialDate = yield* childEvent.calculateInitialMinChildrenStartDateDeep();
                childInitialDate && childStartDates.push(childInitialDate);
            }
            let timestamp = childStartDates.reduce((acc, childStartDate) => Math.min(acc, childStartDate.getTime()), MAX_DATE.getTime());
            if (timestamp === MIN_DATE.getTime() || timestamp === MAX_DATE.getTime())
                return null;
            return new Date(timestamp);
        }
        get parent() {
            return this._parent;
        }
        set parent(value) {
            this._parent = value;
            this.$.parentEvent.put(value);
        }
    }
    __decorate([
        reference({ bucket: 'childEvents' })
    ], HasChildren.prototype, "parentEvent", void 0);
    __decorate([
        bucket()
    ], HasChildren.prototype, "childEvents", void 0);
    return HasChildren;
};
