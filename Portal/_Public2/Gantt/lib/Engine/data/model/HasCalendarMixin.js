var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { calculate } from "../../../ChronoGraph/replica/Entity.js";
import { isAtomicValue } from '../../../ChronoGraph/util/Helper.js';
import { model_field } from '../../chrono/ModelFieldAtom.js';
export const HasCalendarMixin = (base) => {
    class HasCalendarMixin extends base {
        constructor() {
            super(...arguments);
            this.calendarInherited = true;
        }
        async setCalendar(calendar) {
            this.calendar = calendar;
            return this.propagate();
        }
        getCalendar() {
            return this.calendar;
        }
        *calculateCalendar(proposedValue) {
            const calendarManager = this.getCalendarManagerStore();
            let resolved, calendar;
            // `proposedValue` can be `null`, with semantic - "remove own calendar"
            if (proposedValue != null) {
                if (isAtomicValue(proposedValue)) {
                    resolved = calendarManager.getById(proposedValue) || null;
                    proposedValue = resolved;
                }
                this.calendarInherited = false;
                calendar = proposedValue;
                // keep own, not inherited calendar, in the absence of proposed value
            }
            else if (proposedValue === undefined && this.$.calendar.hasConsistentValue() && !this.calendarInherited) {
                calendar = this.$.calendar.getConsistentValue();
                // let's use the project calendar
            }
            else {
                const project = this.getProject();
                // unless 'this' represents the project itself
                // @ts-ignore
                if (project !== this) {
                    calendar = yield project.$.calendar;
                    this.calendarInherited = true;
                }
            }
            // yield self-atom of the calendar - subscribe on the "version" change of the given calendar instance
            !isAtomicValue(calendar) && (yield calendar.$$);
            return calendar;
        }
    }
    __decorate([
        model_field({ serialize: calendar => calendar.id })
    ], HasCalendarMixin.prototype, "calendar", void 0);
    __decorate([
        calculate('calendar')
    ], HasCalendarMixin.prototype, "calculateCalendar", null);
    return HasCalendarMixin;
};
//export type HasCalendarMixin = Mixin<typeof HasCalendarMixin>
