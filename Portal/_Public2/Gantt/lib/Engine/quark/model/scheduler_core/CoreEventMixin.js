import { Mixin } from "../../../../ChronoGraph/class/BetterMixin.js";
import { CorePartOfProjectModelMixin } from "../mixin/CorePartOfProjectModelMixin.js";
import DateHelper from "../../../../Core/helper/DateHelper.js";
export class CoreEventMixin extends Mixin([CorePartOfProjectModelMixin], (base) => {
    const superProto = base.prototype;
    class CoreEventMixin extends base {
        static get fields() {
            return [
                { name: 'startDate', type: 'date' },
                { name: 'endDate', type: 'date' },
                { name: 'duration', type: 'number' },
                { name: 'durationUnit', type: 'string', defaultValue: 'day' }
            ];
        }
        get startDate() { return this.getCurrentOrProposed('startDate'); }
        get endDate() { return this.getCurrentOrProposed('endDate'); }
        get duration() { return this.getCurrentOrProposed('duration'); }
        set startDate(value) { this.proposeStartDate(value); }
        set endDate(value) { this.proposeEndDate(value); }
        set duration(value) { this.proposeDuration(value); }
        getStartDate() {
            return this.startDate;
        }
        proposeStartDate(startDate, keepDuration = true) {
            this.propose({ startDate, keepDuration });
        }
        async setStartDate(startDate, keepDuration = true) {
            var _a;
            this.proposeStartDate(startDate, keepDuration);
            return (_a = this.project) === null || _a === void 0 ? void 0 : _a.commitAsync();
        }
        getEndDate() {
            return this.endDate;
        }
        proposeEndDate(endDate, keepDuration = false) {
            this.propose({ endDate, keepDuration });
        }
        async setEndDate(endDate, keepDuration = false) {
            var _a;
            this.proposeEndDate(endDate, keepDuration);
            return (_a = this.project) === null || _a === void 0 ? void 0 : _a.commitAsync();
        }
        getDuration() {
            return this.duration;
        }
        proposeDuration(duration, unit, keepStart = true) {
            this.propose({ duration, keepStart });
            if (unit)
                this.propose({ durationUnit: unit });
        }
        async setDuration(duration, unit, keepStart = true) {
            var _a;
            this.proposeDuration(duration, unit, keepStart);
            return (_a = this.project) === null || _a === void 0 ? void 0 : _a.commitAsync();
        }
        getDurationUnit() {
            return this.durationUnit;
        }
        joinProject() {
            var _a;
            const changed = this.$changed;
            if (this.hasCurrentOrProposed('startDate'))
                changed.startDate = this.getCurrentOrProposed('startDate');
            if (this.hasCurrentOrProposed('endDate'))
                changed.endDate = this.getCurrentOrProposed('endDate');
            if (this.hasCurrentOrProposed('duration'))
                changed.duration = this.getCurrentOrProposed('duration');
            if (this.eventStore && !this.eventStore.isLoadingData) {
                const unresolved = (_a = this.assignmentStore) === null || _a === void 0 ? void 0 : _a.storage.findItem('event', null);
                if (unresolved) {
                    for (const assignment of unresolved) {
                        if (assignment.getCurrentOrProposed('event') === this.id) {
                            assignment.setChanged('event', this);
                        }
                    }
                }
            }
            superProto.joinProject.call(this);
        }
        applyValue(useProp, key, value, skipAccessors, field) {
            if (key === 'startDate' || key == 'duration' || key === 'endDate') {
                useProp = true;
            }
            if (skipAccessors) {
                useProp = false;
            }
            superProto.applyValue.call(this, useProp, key, value, skipAccessors, field);
        }
        afterChange(toSet, wasSet, silent, fromRelationUpdate, skipAccessors) {
            if (!this.$isCalculating && !skipAccessors) {
                this.setData(this.$changed);
            }
            superProto.afterChange.call(this, toSet, wasSet, silent, fromRelationUpdate, skipAccessors);
        }
        calculateInvalidated() {
            const changed = this.$changed;
            const changedStart = 'startDate' in changed;
            const changedEnd = 'endDate' in changed;
            const changedDuration = 'duration' in changed;
            const { startDate, endDate, duration, keepDuration, keepStart } = changed;
            let calculate = null;
            if (changedStart && !changedEnd && !changedDuration) {
                if (startDate === null) {
                    changed.endDate = null;
                }
                else if (this.hasCurrentOrProposed('endDate') && startDate > this.getCurrentOrProposed('endDate') && !keepDuration) {
                    changed.endDate = startDate;
                    changed.duration = 0;
                }
                else if (this.hasCurrentOrProposed('duration') && (keepDuration || !this.hasCurrentOrProposed('endDate'))) {
                    calculate = 'endDate';
                }
                else if (this.hasCurrentOrProposed('endDate')) {
                    calculate = 'duration';
                }
            }
            else if (!changedStart && changedEnd && !changedDuration) {
                if (endDate === null) {
                    changed.startDate = null;
                }
                else if (this.hasCurrentOrProposed('startDate') && endDate < this.getCurrentOrProposed('startDate') && !keepDuration) {
                    changed.startDate = endDate;
                    changed.duration = 0;
                }
                else if (this.hasCurrentOrProposed('duration') && (keepDuration || !this.hasCurrentOrProposed('startDate'))) {
                    calculate = 'startDate';
                }
                else if (this.hasCurrentOrProposed('startDate')) {
                    calculate = 'duration';
                }
            }
            else if (!changedStart && !changedEnd && changedDuration) {
                if (duration === null) {
                    changed.endDate = null;
                }
                else if (this.hasCurrentOrProposed('startDate') && (keepStart || !this.hasCurrentOrProposed('endDate'))) {
                    if (keepStart && changed.duration < 0) {
                        changed.duration = 0;
                    }
                    calculate = 'endDate';
                }
                else if (this.hasCurrentOrProposed('endDate')) {
                    calculate = 'startDate';
                }
            }
            else if (changedStart && changedEnd && !changedDuration) {
                if (startDate === null && endDate === null) {
                    changed.duration = null;
                }
                else {
                    calculate = 'duration';
                }
            }
            else if (changedStart && !changedEnd && changedDuration) {
                calculate = 'endDate';
            }
            else if (!changedStart && changedEnd && changedDuration) {
                calculate = 'startDate';
            }
            else if (changedStart && changedEnd && changedDuration) {
                if (duration == null) {
                    calculate = 'duration';
                }
                else if (startDate == null) {
                    calculate = 'startDate';
                }
                else {
                    calculate = 'endDate';
                }
            }
            switch (calculate) {
                case 'startDate':
                    changed.startDate = DateHelper.add(this.getCurrentOrProposed('endDate'), -this.getCurrentOrProposed('duration'), this.getCurrentOrProposed('durationUnit'));
                    break;
                case 'endDate':
                    changed.endDate = DateHelper.add(this.getCurrentOrProposed('startDate'), this.getCurrentOrProposed('duration'), this.getCurrentOrProposed('durationUnit'));
                    break;
                case 'duration':
                    changed.duration = DateHelper.diff(this.getCurrentOrProposed('startDate'), this.getCurrentOrProposed('endDate'), this.getCurrentOrProposed('durationUnit'));
                    break;
            }
            delete changed.keepDuration;
            delete changed.keepStart;
        }
    }
    return CoreEventMixin;
}) {
}
