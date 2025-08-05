var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { calculate, field } from "../../../../ChronoGraph/replica/Entity.js";
import { Conflict, ConflictResolution } from "../../../chrono/Conflict.js";
import { dateConverter, model_field } from "../../../chrono/ModelFieldAtom.js";
import { DateInterval, intersectIntervals } from "../../../scheduling/DateInterval.js";
import { TimeUnit } from "../../../scheduling/Types.js";
import DateHelper from "../../../../Common/helper/DateHelper.js";
export class ConstraintInterval extends DateInterval {
    toString() {
        return `from ${this.startDate} till ${this.endDate}`;
    }
}
//---------------------------------------------------------------------------------------------------------------------
export class RemoveConstrainingInterval extends ConflictResolution {
    get originDescription() {
        return this.interval.originDescription;
    }
    resolve() {
        if (!this.interval)
            throw new Error("Can't use this resolution option - no constraint interval available");
        if (!this.interval.onRemoveAction)
            throw new Error("Can't use this resolution option - no `onRemoveAction` available");
        this.interval.onRemoveAction();
    }
}
export class IntervalConflict extends Conflict {
    get description() {
        return `The change causes scheduling conflict with the constraining interval ${this.conflictingInterval},
which is created by the ${this.conflictingInterval.originDescription}`;
    }
    get resolutions() {
        const resolutions = [];
        if (this.conflictingInterval.onRemoveAction)
            resolutions.push(RemoveConstrainingInterval.new({ interval: this.conflictingInterval }));
        // lazy property pattern
        Object.defineProperty(this, 'resolutions', { value: resolutions });
        return resolutions;
    }
}
//---------------------------------------------------------------------------------------------------------------------
export class ProposedDateOutsideOfConstraint extends IntervalConflict {
    get description() {
        return `The date ${this.proposedDate} is outside of the constraining interval ${this.conflictingInterval},
which is created by the ${this.conflictingInterval.originDescription}`;
    }
}
//---------------------------------------------------------------------------------------------------------------------
export const ConstrainedEvent = (base) => {
    class ConstrainedEvent extends base {
        async setManuallyScheduled(mode) {
            this.$.manuallyScheduled.put(mode);
            return this.propagate();
        }
        *validateProposedStartDate(startDate) {
            // if we have a proposed date let's validate it against registered constraining intervals
            if (startDate) {
                // need to adjust the proposed date according to the calendar, to avoid unnecessary conflicts
                const adjustedProposedDate = yield* this.skipNonWorkingTime(startDate, true);
                const startDateIntervals = yield this.$.combinedStartDateConstraintIntervals;
                let acc = DateInterval.new();
                for (let interval of startDateIntervals) {
                    acc = acc.intersect(interval);
                    if (!acc.containsDate(adjustedProposedDate)) {
                        yield ProposedDateOutsideOfConstraint.new({
                            proposedDate: adjustedProposedDate,
                            conflictingInterval: interval
                        });
                    }
                }
            }
        }
        *validateProposedEndDate(endDate) {
            if (endDate) {
                const adjustedProposedDate = yield* this.skipNonWorkingTime(endDate, false);
                const endDateIntervals = yield this.$.combinedEndDateConstraintIntervals;
                let acc = DateInterval.new();
                for (let interval of endDateIntervals) {
                    acc = acc.intersect(interval);
                    if (!acc.containsDate(adjustedProposedDate)) {
                        yield ProposedDateOutsideOfConstraint.new({
                            proposedDate: adjustedProposedDate,
                            conflictingInterval: interval
                        });
                    }
                }
            }
        }
        *calculateEarlyStartDateRaw() {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateStartDateInitial()
            const project = this.getProject(), projectStartDate = yield project.$.startDate, validInitialIntervals = yield this.$.validInitialIntervals, startDateInterval = validInitialIntervals.startDateInterval;
            return startDateInterval.startDateIsFinite() ? startDateInterval.startDate : projectStartDate;
        }
        *calculateEarlyStartDate() {
            const date = yield this.$.earlyStartDateRaw;
            return yield* this.maybeSkipNonWorkingTime(date, true);
        }
        *calculateEarlyEndDateRaw() {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateEndDateInitial()
            const project = this.getProject(), projectStartDate = yield project.$.startDate, validInitialIntervals = yield this.$.validInitialIntervals, endDateInterval = validInitialIntervals.endDateInterval, canCalculateProjectedXDate = yield* this.canCalculateProjectedXDate();
            let result = null;
            if (endDateInterval.startDateIsFinite()) {
                result = endDateInterval.startDate;
                // if no end date restrictions are found we use formula: task early end == project start - task duration (if duration is available)
            }
            else if (projectStartDate && canCalculateProjectedXDate) {
                result = yield* this.calculateProjectedEndDate(projectStartDate);
            }
            return result;
        }
        *calculateEarlyEndDate() {
            const date = yield this.$.earlyEndDateRaw;
            return yield* this.maybeSkipNonWorkingTime(date, false);
        }
        *calculateLateStartDateRaw() {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateStartDateInitial()
            const project = this.getProject(), projectEndDate = yield project.$.endDate, validInitialIntervals = yield this.$.validInitialIntervals, startDateInterval = validInitialIntervals.startDateInterval, canCalculateProjectedXDate = yield* this.canCalculateProjectedXDate();
            let result = null;
            if (startDateInterval.endDateIsFinite()) {
                result = startDateInterval.endDate;
            }
            else if (projectEndDate && canCalculateProjectedXDate) {
                result = yield* this.calculateProjectedStartDate(projectEndDate);
            }
            return result;
        }
        *calculateLateStartDate() {
            const date = yield this.$.lateStartDateRaw;
            return yield* this.maybeSkipNonWorkingTime(date, true);
        }
        *calculateLateEndDateRaw() {
            // if (yield this.$.manuallyScheduled) return yield* super.calculateEndDateInitial()
            const project = this.getProject(), projectEndDate = yield project.$.endDate, validInitialIntervals = yield this.$.validInitialIntervals, endDateInterval = validInitialIntervals.endDateInterval;
            return endDateInterval.endDateIsFinite() ? endDateInterval.endDate : projectEndDate;
        }
        *calculateLateEndDate() {
            const date = yield this.$.lateEndDateRaw;
            return yield* this.maybeSkipNonWorkingTime(date, false);
        }
        *calculateTotalSlack() {
            const earlyStartDate = yield this.$.earlyStartDateRaw;
            const lateStartDate = yield this.$.lateStartDateRaw;
            const earlyEndDate = yield this.$.earlyEndDateRaw;
            const lateEndDate = yield this.$.lateEndDateRaw;
            const slackUnit = yield this.$.slackUnit;
            let endSlack, result;
            if ((earlyStartDate && lateStartDate) || (earlyEndDate && lateEndDate)) {
                if (earlyStartDate && lateStartDate) {
                    result = yield* this.calculateDurationBetweenDates(earlyStartDate, lateStartDate, slackUnit);
                    if (earlyEndDate && lateEndDate) {
                        endSlack = yield* this.calculateDurationBetweenDates(earlyEndDate, lateEndDate, slackUnit);
                        if (endSlack < result)
                            result = endSlack;
                    }
                }
                else if (earlyEndDate && lateEndDate) {
                    result = yield* this.calculateDurationBetweenDates(earlyEndDate, lateEndDate, slackUnit);
                }
            }
            return result;
        }
        *calculateCritical() {
            const totalSlack = yield this.$.totalSlack;
            return totalSlack <= 0;
        }
        *calculateStartDateInitial() {
            const proposedValue = this.$.startDate.proposedValue;
            // early exit in manually scheduled case
            const manuallyScheduled = yield this.$.manuallyScheduled;
            if (manuallyScheduled)
                return yield* super.calculateStartDateInitial();
            // Trigger effective constraint intervals calculation
            // since when we exit in the following line (happens during data normalization)
            // the graph doesn't build an edge - start date dependency on the intervals
            yield this.$.combinedStartDateConstraintIntervals;
            yield this.$.combinedEndDateConstraintIntervals;
            // If we normalize the task that does not have duration provided
            // and we have start date provided -> return start date as-is to calculate duration based on it
            if (!this.$.duration.hasConsistentValue() && !this.$.duration.hasProposedValue() && proposedValue) {
                return proposedValue;
            }
            // user want to "unschedule the task"
            if (proposedValue === null)
                return null;
            // if we have a proposed date let's validate it against registered constraining intervals
            yield* this.validateProposedStartDate(proposedValue);
            // TODO should also:
            // 1) check if proposed start date is inside the `validInitialIntervals.startDateInterval`
            // (which has an additional constraint interval, calculated by the end date)
            // 2) check if proposed start date is inside `validInitialIntervals.startDateInterval`, but
            // not its start point (which is required by the currently implicit ASAP constraint)
            // then it should yield a conflict like: ProposedDateViolateASAPConstraint
            // the resolutions would be:
            // - remove constraint
            // - add additional "Must start on" constraint
            // - add additional "Must start no sooner than" constraint
            // - switch the task to "manual" scheduling mode (whatever it means)
            return (yield this.$.earlyStartDateRaw) || (yield* super.calculateStartDateInitial());
        }
        *calculateEndDateInitial() {
            const proposedValue = this.$.endDate.proposedValue;
            // early exit in manually scheduled case
            const manuallyScheduled = yield this.$.manuallyScheduled;
            if (manuallyScheduled)
                return yield* super.calculateEndDateInitial();
            // Trigger effective constraint intervals calculation
            // since when we exit in the following line (happens during data normalization)
            // the graph doesn't build an edge - end date dependency on the intervals
            yield this.$.combinedStartDateConstraintIntervals;
            yield this.$.combinedEndDateConstraintIntervals;
            // If we normalize the task that does not have duration provided
            // and we have end date provided -> return end date as-is to calculate duration based on it
            if (!this.$.duration.hasConsistentValue() && !this.$.duration.hasProposedValue() && proposedValue) {
                return proposedValue;
            }
            // user want to "unschedule the task"
            if (proposedValue === null)
                return null;
            yield* this.validateProposedEndDate(proposedValue);
            return (yield this.$.earlyEndDateRaw) || (yield* super.calculateEndDateInitial());
        }
        // TODO make this method smart in regard of providing conflict resolution information
        /**
         * Finds the intersection of provided intervals.
         * If some of the intervals doesn't intersect the methods yields IntervalConflict
         * with the interval reference.
         * @param intervals Intervals to find intersection of
         * @private
         */
        *validateIntervalsIntersection(intervals) {
            let intersection = DateInterval.new();
            for (let interval of intervals) {
                intersection = intersection.intersect(interval);
                if (intersection.isIntervalEmpty()) {
                    // if some interval has no intersection w/ other(s) we throw a conflict
                    yield IntervalConflict.new({ conflictingInterval: interval });
                }
            }
            return intersection;
        }
        *calculateStartDateIntervalsByEndDateIntervals(intervals) {
            let result = [];
            for (let interval of intervals) {
                result.push(ConstraintInterval.new({
                    onRemoveAction: interval.onRemoveAction,
                    originDescription: interval.originDescription,
                    startDate: interval.startDateIsFinite() ? yield* this.calculateProjectedStartDate(interval.startDate) : null,
                    endDate: interval.endDateIsFinite() ? yield* this.calculateProjectedStartDate(interval.endDate) : null,
                }));
            }
            return result;
        }
        *calculateEndDateIntervalsByStartDateIntervals(intervals) {
            let result = [];
            for (let interval of intervals) {
                result.push(ConstraintInterval.new({
                    onRemoveAction: interval.onRemoveAction,
                    originDescription: interval.originDescription,
                    startDate: interval.startDateIsFinite() ? yield* this.calculateProjectedEndDate(interval.startDate) : null,
                    endDate: interval.endDateIsFinite() ? yield* this.calculateProjectedEndDate(interval.endDate) : null,
                }));
            }
            return result;
        }
        // Indicates if calculateProjectedStartDate and calculateProjectedStartDate method can calculate values.
        *canCalculateProjectedXDate() {
            // need to have duration value
            return !(yield* this.shouldRecalculateDuration());
        }
        *calculateValidInitialIntervals() {
            const startDateIntervals = yield this.$.combinedStartDateConstraintIntervals;
            const endDateIntervals = yield this.$.combinedEndDateConstraintIntervals;
            // calculate effective start date constraining interval
            let startDateInterval = intersectIntervals(startDateIntervals);
            // calculate effective end date constraining interval
            let endDateInterval = intersectIntervals(endDateIntervals);
            const canCalculateProjectedXDate = yield* this.canCalculateProjectedXDate();
            // if we can't use calculateProjectedStartDate/calculateProjectedEndDate methods then we cannot do anything else -> return
            if (!canCalculateProjectedXDate && !startDateInterval.isIntervalEmpty() && !endDateInterval.isIntervalEmpty()) {
                return {
                    startDateInterval,
                    endDateInterval
                };
            }
            const additionalConstraintForStartDate = DateInterval.new({
                startDate: endDateInterval.startDateIsFinite() ? yield* this.calculateProjectedStartDate(endDateInterval.startDate) : null,
                endDate: endDateInterval.endDateIsFinite() ? yield* this.calculateProjectedStartDate(endDateInterval.endDate) : null,
            });
            startDateInterval = startDateInterval.intersect(additionalConstraintForStartDate);
            // If no intersection w/ additional interval let's intersects intervals one by one
            // and yield Conflict
            if (startDateInterval.isIntervalEmpty()) {
                const reflectedIntervals = yield* this.calculateStartDateIntervalsByEndDateIntervals(endDateIntervals);
                let combinedIntervals;
                if (startDateIntervals.length > 1) {
                    combinedIntervals = startDateIntervals
                        .slice(0, startDateIntervals.length - 1)
                        .concat(reflectedIntervals)
                        .concat(startDateIntervals[startDateIntervals.length - 1]);
                }
                else {
                    combinedIntervals = startDateIntervals.concat(reflectedIntervals);
                }
                yield* this.validateIntervalsIntersection(combinedIntervals);
            }
            else {
                const additionalConstraintForEndDate = DateInterval.new({
                    startDate: startDateInterval.startDateIsFinite() ? yield* this.calculateProjectedEndDate(startDateInterval.startDate) : null,
                    endDate: startDateInterval.endDateIsFinite() ? yield* this.calculateProjectedEndDate(startDateInterval.endDate) : null,
                });
                endDateInterval = endDateInterval.intersect(additionalConstraintForEndDate);
            }
            return {
                startDateInterval,
                endDateInterval
            };
        }
        /**
         * A template method which should provide an array on intervals, constraining the start date of this event.
         * Supposed to be overridden in other mixins.
         */
        *calculateStartDateConstraintIntervals() {
            return [];
        }
        *calculateCombinedStartDateConstraintIntervals() {
            let result = yield this.$.startDateConstraintIntervals;
            // TODO: add ALAP support
            if (true) {
                result = result.concat(yield this.$.earlyStartDateConstraintIntervals);
            }
            else {
                result = result.concat(yield this.$.lateStartDateConstraintIntervals);
            }
            return result;
        }
        *calculateEarlyStartDateConstraintIntervals() {
            return [];
        }
        *calculateLateStartDateConstraintIntervals() {
            return [];
        }
        /**
         * A template method which should provide an array on intervals, constraining the end date of this event.
         * Supposed to be overridden in other mixins.
         */
        *calculateEndDateConstraintIntervals() {
            return [];
        }
        *calculateCombinedEndDateConstraintIntervals() {
            let result = yield this.$.endDateConstraintIntervals;
            // TODO: add ALAP support
            if (true) {
                result = result.concat(yield this.$.earlyEndDateConstraintIntervals);
            }
            else {
                result = result.concat(yield this.$.lateEndDateConstraintIntervals);
            }
            return result;
        }
        *calculateEarlyEndDateConstraintIntervals() {
            return [];
        }
        *calculateLateEndDateConstraintIntervals() {
            return [];
        }
        putEndDate(date, keepDuration = false) {
            // Force intervals recalculation
            // Covered by 10_basic and 50_milestone
            this.markAsNeedRecalculation(this.$.validInitialIntervals);
            super.putEndDate(date, keepDuration);
        }
    }
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "earlyStartDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', dateFormat: 'YYYY-MM-DDTHH:mm:ssZ', persist: false }, { converter: dateConverter, persistent: false })
    ], ConstrainedEvent.prototype, "earlyStartDate", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "earlyEndDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', dateFormat: 'YYYY-MM-DDTHH:mm:ssZ', persist: false }, { converter: dateConverter, persistent: false })
    ], ConstrainedEvent.prototype, "earlyEndDate", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "lateStartDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', dateFormat: 'YYYY-MM-DDTHH:mm:ssZ', persist: false }, { converter: dateConverter, persistent: false })
    ], ConstrainedEvent.prototype, "lateStartDate", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "lateEndDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', dateFormat: 'YYYY-MM-DDTHH:mm:ssZ', persist: false }, { converter: dateConverter, persistent: false })
    ], ConstrainedEvent.prototype, "lateEndDate", void 0);
    __decorate([
        model_field({ type: 'number', persist: false }, { persistent: false })
    ], ConstrainedEvent.prototype, "totalSlack", void 0);
    __decorate([
        model_field({ type: 'string', defaultValue: TimeUnit.Day, persist: false }, { converter: DateHelper.normalizeUnit, persistent: false })
    ], ConstrainedEvent.prototype, "slackUnit", void 0);
    __decorate([
        model_field({ type: 'boolean', defaultValue: false, persist: false }, { persistent: false })
    ], ConstrainedEvent.prototype, "critical", void 0);
    __decorate([
        model_field({ type: 'boolean', defaultValue: false })
    ], ConstrainedEvent.prototype, "manuallyScheduled", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "startDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "earlyStartDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "lateStartDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "combinedStartDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "endDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "earlyEndDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "lateEndDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "combinedEndDateConstraintIntervals", void 0);
    __decorate([
        field()
    ], ConstrainedEvent.prototype, "validInitialIntervals", void 0);
    __decorate([
        calculate('earlyStartDateRaw')
    ], ConstrainedEvent.prototype, "calculateEarlyStartDateRaw", null);
    __decorate([
        calculate('earlyStartDate')
    ], ConstrainedEvent.prototype, "calculateEarlyStartDate", null);
    __decorate([
        calculate('earlyEndDateRaw')
    ], ConstrainedEvent.prototype, "calculateEarlyEndDateRaw", null);
    __decorate([
        calculate('earlyEndDate')
    ], ConstrainedEvent.prototype, "calculateEarlyEndDate", null);
    __decorate([
        calculate('lateStartDateRaw')
    ], ConstrainedEvent.prototype, "calculateLateStartDateRaw", null);
    __decorate([
        calculate('lateStartDate')
    ], ConstrainedEvent.prototype, "calculateLateStartDate", null);
    __decorate([
        calculate('lateEndDateRaw')
    ], ConstrainedEvent.prototype, "calculateLateEndDateRaw", null);
    __decorate([
        calculate('lateEndDate')
    ], ConstrainedEvent.prototype, "calculateLateEndDate", null);
    __decorate([
        calculate('totalSlack')
    ], ConstrainedEvent.prototype, "calculateTotalSlack", null);
    __decorate([
        calculate('critical')
    ], ConstrainedEvent.prototype, "calculateCritical", null);
    __decorate([
        calculate('validInitialIntervals')
    ], ConstrainedEvent.prototype, "calculateValidInitialIntervals", null);
    __decorate([
        calculate('startDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateStartDateConstraintIntervals", null);
    __decorate([
        calculate('combinedStartDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateCombinedStartDateConstraintIntervals", null);
    __decorate([
        calculate('earlyStartDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateEarlyStartDateConstraintIntervals", null);
    __decorate([
        calculate('lateStartDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateLateStartDateConstraintIntervals", null);
    __decorate([
        calculate('endDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateEndDateConstraintIntervals", null);
    __decorate([
        calculate('combinedEndDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateCombinedEndDateConstraintIntervals", null);
    __decorate([
        calculate('earlyEndDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateEarlyEndDateConstraintIntervals", null);
    __decorate([
        calculate('lateEndDateConstraintIntervals')
    ], ConstrainedEvent.prototype, "calculateLateEndDateConstraintIntervals", null);
    return ConstrainedEvent;
};
