var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Reject } from "../../../../ChronoGraph/chrono/Effect.js";
import { Mixin } from '../../../../ChronoGraph/class/BetterMixin.js';
import { calculate, field } from '../../../../ChronoGraph/replica/Entity.js';
import DateHelper from '../../../../Core/helper/DateHelper.js';
import { dateConverter, model_field } from '../../../chrono/ModelFieldAtom.js';
import { DateInterval } from '../../../scheduling/DateInterval.js';
import { Direction, TimeUnit } from '../../../scheduling/Types.js';
import { MAX_DATE, MIN_DATE, isDateFinite } from "../../../util/Constants.js";
import { HasChildrenMixin } from '../scheduler_basic/HasChildrenMixin.js';
import { ConstrainedEarlyEventMixin, EarlyLateLazyness } from "../scheduler_pro/ConstrainedEarlyEventMixin.js";
import { ConflictEffect, ConflictResolutionResult } from '../../../chrono/Conflict.js';
export class ConstrainedLateEventMixin extends Mixin([ConstrainedEarlyEventMixin, HasChildrenMixin], (base) => {
    const superProto = base.prototype;
    class ConstrainedLateEventMixin extends base {
        *calculateLateStartDateConstraintIntervals() {
            const intervals = [];
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                const parentIntervals = yield parentEvent.$.lateStartDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
            }
            return intervals;
        }
        *calculateLateEndDateConstraintIntervals() {
            const intervals = [];
            const parentEvent = yield this.$.parentEvent;
            if (parentEvent) {
                const parentIntervals = yield parentEvent.$.lateEndDateConstraintIntervals;
                intervals.push.apply(intervals, parentIntervals);
                if ((yield parentEvent.$.manuallyScheduled) && (yield this.$.direction) === Direction.Backward) {
                    intervals.push(DateInterval.new({
                        endDate: yield parentEvent.$.endDate
                    }));
                }
            }
            return intervals;
        }
        *calculateLateStartDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MAX_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.lateStartDate;
                    if (childDate && childDate < result)
                        result = childDate;
                }
                result = result.getTime() - MAX_DATE.getTime() ? result : null;
            }
            else {
                if (yield this.$.manuallyScheduled) {
                    return yield this.$.startDate;
                }
                if (!(yield* this.isConstrainedLate())) {
                    return yield this.$.startDate;
                }
                const startDateConstraintIntervals = yield this.$.lateStartDateConstraintIntervals;
                const endDateConstraintIntervals = yield this.$.lateEndDateConstraintIntervals;
                const effectiveInterval = (yield* this.calculateEffectiveConstraintInterval(true, startDateConstraintIntervals.concat(yield this.$.startDateConstraintIntervals), endDateConstraintIntervals.concat(yield this.$.endDateConstraintIntervals)));
                if (effectiveInterval === null) {
                    return null;
                }
                else if (effectiveInterval.isIntervalEmpty()) {
                    const conflict = ConflictEffect.new();
                    if ((yield conflict) === ConflictResolutionResult.Cancel) {
                        yield Reject(conflict);
                    }
                    else {
                        return null;
                    }
                }
                return isDateFinite(effectiveInterval.endDate) ? effectiveInterval.endDate : null;
            }
            return result;
        }
        *calculateLateStartDate() {
            const date = yield this.$.lateStartDateRaw;
            return yield* this.maybeSkipNonWorkingTime(date, true);
        }
        *calculateLateEndDateRaw() {
            const childEvents = yield this.$.childEvents;
            let result;
            if (childEvents.size) {
                result = MIN_DATE;
                for (let childEvent of childEvents) {
                    const childDate = yield childEvent.$.lateEndDate;
                    if (childDate && childDate > result)
                        result = childDate;
                }
                result = result.getTime() - MIN_DATE.getTime() ? result : null;
            }
            else {
                if (yield this.$.manuallyScheduled) {
                    return yield this.$.endDate;
                }
                if (!(yield* this.isConstrainedLate())) {
                    return yield this.$.endDate;
                }
                const startDateConstraintIntervals = yield this.$.lateStartDateConstraintIntervals;
                const endDateConstraintIntervals = yield this.$.lateEndDateConstraintIntervals;
                const effectiveInterval = (yield* this.calculateEffectiveConstraintInterval(false, startDateConstraintIntervals.concat(yield this.$.startDateConstraintIntervals), endDateConstraintIntervals.concat(yield this.$.endDateConstraintIntervals)));
                if (effectiveInterval === null) {
                    return null;
                }
                else if (effectiveInterval.isIntervalEmpty()) {
                    const conflict = ConflictEffect.new();
                    if ((yield conflict) === ConflictResolutionResult.Cancel) {
                        yield Reject(conflict);
                    }
                    else {
                        return null;
                    }
                }
                return isDateFinite(effectiveInterval.endDate) ? effectiveInterval.endDate : null;
            }
            return result;
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
                    result = yield* this.calculateProjectedDuration(earlyStartDate, lateStartDate, slackUnit);
                    if (earlyEndDate && lateEndDate) {
                        endSlack = yield* this.calculateProjectedDuration(earlyEndDate, lateEndDate, slackUnit);
                        if (endSlack < result)
                            result = endSlack;
                    }
                }
                else if (earlyEndDate && lateEndDate) {
                    result = yield* this.calculateProjectedDuration(earlyEndDate, lateEndDate, slackUnit);
                }
            }
            return result;
        }
        *calculateCritical() {
            const totalSlack = yield this.$.totalSlack;
            return totalSlack <= 0;
        }
        *isConstrainedLate() {
            const startDateIntervals = yield this.$.startDateConstraintIntervals;
            const endDateIntervals = yield this.$.endDateConstraintIntervals;
            const lateStartDateConstraintIntervals = yield this.$.lateStartDateConstraintIntervals;
            const lateEndDateConstraintIntervals = yield this.$.lateEndDateConstraintIntervals;
            return Boolean((startDateIntervals === null || startDateIntervals === void 0 ? void 0 : startDateIntervals.length) || (endDateIntervals === null || endDateIntervals === void 0 ? void 0 : endDateIntervals.length) || (lateStartDateConstraintIntervals === null || lateStartDateConstraintIntervals === void 0 ? void 0 : lateStartDateConstraintIntervals.length) || (lateEndDateConstraintIntervals === null || lateEndDateConstraintIntervals === void 0 ? void 0 : lateEndDateConstraintIntervals.length));
        }
        *calculateStartDatePure() {
            const direction = yield this.$.direction;
            if (direction === Direction.Backward) {
                if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                    return yield* superProto.calculateStartDatePure.call(this);
                }
                return yield this.$.lateStartDate;
            }
            else {
                return yield* superProto.calculateStartDatePure.call(this);
            }
        }
        *calculateStartDateProposed() {
            const direction = yield this.$.direction;
            switch (direction) {
                case Direction.Backward:
                    if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                        return yield* superProto.calculateStartDateProposed.call(this);
                    }
                    return (yield this.$.lateStartDate) || (yield* superProto.calculateStartDateProposed.call(this));
                default:
                    return yield* superProto.calculateStartDateProposed.call(this);
            }
        }
        *calculateEndDatePure() {
            const direction = yield this.$.direction;
            if (direction === Direction.Backward) {
                if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                    return yield* superProto.calculateEndDatePure.call(this);
                }
                return yield this.$.lateEndDate;
            }
            else {
                return yield* superProto.calculateEndDatePure.call(this);
            }
        }
        *calculateEndDateProposed() {
            const direction = yield this.$.direction;
            switch (direction) {
                case Direction.Backward:
                    if (!(yield* this.isConstrainedLate()) || (yield this.$.manuallyScheduled) || (yield* this.hasSubEvents())) {
                        return yield* superProto.calculateEndDateProposed.call(this);
                    }
                    return (yield this.$.lateEndDate) || (yield* superProto.calculateEndDateProposed.call(this));
                default:
                    return yield* superProto.calculateEndDateProposed.call(this);
            }
        }
    }
    __decorate([
        field({ lazy: EarlyLateLazyness })
    ], ConstrainedLateEventMixin.prototype, "lateStartDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', persist: false }, { lazy: EarlyLateLazyness, converter: dateConverter, persistent: false })
    ], ConstrainedLateEventMixin.prototype, "lateStartDate", void 0);
    __decorate([
        field({ lazy: EarlyLateLazyness })
    ], ConstrainedLateEventMixin.prototype, "lateEndDateRaw", void 0);
    __decorate([
        model_field({ type: 'date', persist: false }, { lazy: EarlyLateLazyness, converter: dateConverter, persistent: false })
    ], ConstrainedLateEventMixin.prototype, "lateEndDate", void 0);
    __decorate([
        field({ lazy: EarlyLateLazyness })
    ], ConstrainedLateEventMixin.prototype, "lateStartDateConstraintIntervals", void 0);
    __decorate([
        field({ lazy: EarlyLateLazyness })
    ], ConstrainedLateEventMixin.prototype, "lateEndDateConstraintIntervals", void 0);
    __decorate([
        model_field({ type: 'number', persist: false }, { lazy: EarlyLateLazyness, persistent: false })
    ], ConstrainedLateEventMixin.prototype, "totalSlack", void 0);
    __decorate([
        model_field({ type: 'string', defaultValue: TimeUnit.Day, persist: false }, { lazy: EarlyLateLazyness, converter: DateHelper.normalizeUnit, persistent: false })
    ], ConstrainedLateEventMixin.prototype, "slackUnit", void 0);
    __decorate([
        model_field({ type: 'boolean', defaultValue: false, persist: false }, { persistent: false, lazy: EarlyLateLazyness })
    ], ConstrainedLateEventMixin.prototype, "critical", void 0);
    __decorate([
        calculate('lateStartDateConstraintIntervals')
    ], ConstrainedLateEventMixin.prototype, "calculateLateStartDateConstraintIntervals", null);
    __decorate([
        calculate('lateEndDateConstraintIntervals')
    ], ConstrainedLateEventMixin.prototype, "calculateLateEndDateConstraintIntervals", null);
    __decorate([
        calculate('lateStartDateRaw')
    ], ConstrainedLateEventMixin.prototype, "calculateLateStartDateRaw", null);
    __decorate([
        calculate('lateStartDate')
    ], ConstrainedLateEventMixin.prototype, "calculateLateStartDate", null);
    __decorate([
        calculate('lateEndDateRaw')
    ], ConstrainedLateEventMixin.prototype, "calculateLateEndDateRaw", null);
    __decorate([
        calculate('lateEndDate')
    ], ConstrainedLateEventMixin.prototype, "calculateLateEndDate", null);
    __decorate([
        calculate('totalSlack')
    ], ConstrainedLateEventMixin.prototype, "calculateTotalSlack", null);
    __decorate([
        calculate('critical')
    ], ConstrainedLateEventMixin.prototype, "calculateCritical", null);
    return ConstrainedLateEventMixin;
}) {
}
