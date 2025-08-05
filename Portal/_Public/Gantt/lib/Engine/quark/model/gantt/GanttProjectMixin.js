var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { ProposedOrPrevious, UnsafeProposedOrPreviousValueOf } from "../../../../ChronoGraph/chrono/Effect.js";
import { Mixin } from '../../../../ChronoGraph/class/BetterMixin.js';
import { field, calculate } from "../../../../ChronoGraph/replica/Entity.js";
import { Direction, ProjectType, ConstraintIntervalSide } from '../../../scheduling/Types.js';
import { MAX_DATE, MIN_DATE } from '../../../util/Constants.js';
import { ChronoEventTreeStoreMixin } from "../../store/ChronoEventStoreMixin.js";
import { SchedulerProProjectMixin } from "../scheduler_pro/SchedulerProProjectMixin.js";
import { SchedulerProResourceMixin } from "../scheduler_pro/SchedulerProResourceMixin.js";
import { ConstrainedLateEventMixin } from "./ConstrainedLateEventMixin.js";
import { SchedulerProAssignmentMixin } from "../scheduler_pro/SchedulerProAssignmentMixin.js";
import { GanttEvent } from './GanttEvent.js';
import { HasCriticalPathsMixin } from "./HasCriticalPathsMixin.js";
import { HasEffortMixin } from "../scheduler_pro/HasEffortMixin.js";
import { ConstraintInterval, ConstraintIntervalDescription } from "../../../chrono/Conflict.js";
import { format } from "../../../util/Functions.js";
import "../../../localization/En.js";
import { prototypeValue } from "../../../../ChronoGraph/util/Helpers.js";
//---------------------------------------------------------------------------------------------------------------------
/**
 * Gantt project mixin type. At this level, events are called "tasks". All scheduling features from the [[SchedulerProProjectMixin]]
 * are preserved. Additionally, tasks inherit constraints from parent tasks. Tasks also receives the [[HasEffortMixin.effort|effort]] field
 * and [[HasSchedulingModeMixin.schedulingMode|schedulingMode]] field.
 *
 * The base event class for this level is [[GanttEvent]]. The base assignment class is [[SchedulerProAssignmentMixin]].
 *
 * At this level, project can be scheduled in backward direction. This is controlled with the [[direction]] field.
 *
 * * Forward ASAP scheduling
 *
 * This is a default, most-used mode. In this mode, the "base" date is project start date. If it is not provided,
 * it is calculated as the earliest date of all project tasks. Events are scheduled ASAP, based on the "early" constraints
 * (plus "generic" constraints).
 *
 * * Forward ALAP scheduling
 *
 * In this mode, the "base" date is still project start date. If it is not provided,
 * it is calculated as the earliest date of all project tasks.
 *
 * Events are first scheduled ASAP, based on the "early" constraints. This gives the project end date.
 * Now events are scheduled ALAP, using the project end date as the base.
 *
 * The difference between the task position in Forward ASAP and Forward ALAP scheduling is called [[ConstrainedLateEventMixin.totalSlack|"slack"]]
 *
 * * Backward ALAP scheduling
 *
 * This is a "default" backward scheduling. In this mode, the "base" date is project end date. If it is not provided,
 * it is calculated as the latest date of all project tasks. Events are scheduled ALAP, based on the "late" constraints
 * (plus "generic" constraints).
 *
 * * Backward ASAP scheduling
 *
 * In this mode, the "base" date is still project end date. If it is not provided,
 * it is calculated as the latest date of all project tasks.
 *
 * Events are first scheduled ALAP, based on the "late" constraints. This gives the project start date.
 * Now events are scheduled ASAP, using the project start date as the base.
 *
 */
export class GanttProjectMixin extends Mixin([
    SchedulerProProjectMixin,
    HasEffortMixin,
    ConstrainedLateEventMixin,
    HasCriticalPathsMixin
], (base) => {
    const superProto = base.prototype;
    class GanttProjectMixin extends base {
        constructor() {
            super(...arguments);
            // this atom is recalculated in every transaction (it is "self dependent")
            // this happens because it is always calculated to the value which is different from
            // proposed value
            // this is a "source of changes" which we use for `project.startDate/endDate` in case
            // scanning the children returned `null`
            // so it will cause the `project.startDate` to always recalculate until it obtain some value
            this.nonEqual = false;
        }
        *calculateNonEqual() {
            return !(yield ProposedOrPrevious);
        }
        get isGanttProjectMixin() {
            return true;
        }
        afterConfigure() {
            superProto.afterConfigure.apply(this, arguments);
            this.projectConstraintIntervalClass = this.projectConstraintIntervalClass || ProjectConstraintInterval;
        }

        *hasSubEvents() {
            const childEvents = yield this.$.childEvents;
            return childEvents.size > 0;
        }
        *subEventsIterable() {
            return yield this.$.childEvents;
        }
        *calculateStartDate() {
            const direction = yield this.$.effectiveDirection;
            if (direction.direction === Direction.Forward) {
                let result = yield ProposedOrPrevious;
                if (!result) {
                    result = yield* this.unsafeCalculateInitialMinChildrenStartDateDeep();
                    if (!result)
                        yield this.$.nonEqual;
                }
                return result;
            }
            else if (direction.direction === Direction.Backward) {
                const startDate = yield* this.calculateMinChildrenStartDate();
                const endDate = yield this.$.endDate;
                // Calculated startDate can get after the project endDate
                // (in case its built based on a manually scheduled task that start after the project finishes).
                // We set the project startDate to its endDate value then.
                return startDate && endDate > startDate ? startDate : endDate;
            }
        }
        *calculateEndDate() {
            const direction = yield this.$.effectiveDirection;
            if (direction.direction === Direction.Forward) {
                const startDate = yield this.$.startDate;
                const endDate = yield* this.calculateMaxChildrenEndDate();
                // Calculated endDate can be before the project startDate
                // (in case its built based on a manually scheduled task that finishes before the project starts).
                // Then it leads to an infinite cycle because of these BaseEventMixin.calculateDurationPure lines:
                //      if (startDate > endDate) {
                //          yield Write(this.$.duration, 0, null)
                //      }
                // So we simply check if the project endDate got earlier than its start date
                // and set endDate to startDate value then.
                // This case is reported in: https://github.com/bryntum/support/issues/3127
                // and asserted by: 031_manually_scheduled.t.ts
                return endDate && endDate > startDate ? endDate : startDate;
            }
            else if (direction.direction === Direction.Backward) {
                let result = yield ProposedOrPrevious;
                if (!result) {
                    result = yield* this.unsafeCalculateInitialMaxChildrenEndDateDeep();
                    if (!result)
                        yield this.$.nonEqual;
                }
                return result;
            }
        }
        *shouldRollupChildStartDate(child) {
            // Do not take into account inactive children dates when calculating start date
            return !(yield child.$.inactive);
        }
        *shouldRollupChildEndDate(child) {
            // Do not take into account inactive children dates when calculating start date
            return !(yield child.$.inactive);
        }
        *calculateEarlyStartDateConstraintIntervals() {
            const intervals = yield* superProto.calculateEarlyStartDateConstraintIntervals.call(this);
            const direction = yield this.$.effectiveDirection;
            if (direction.direction === Direction.Forward) {
                const startDate = yield this.$.startDate;
                startDate && intervals.push(this.projectConstraintIntervalClass.new({
                    owner: this,
                    side: ConstraintIntervalSide.Start,
                    startDate
                }));
            }
            else if (direction.direction === Direction.Backward) {
                const startDate = yield this.$.lateStartDate;
                startDate && intervals.push(this.projectConstraintIntervalClass.new({
                    owner: this,
                    side: ConstraintIntervalSide.Start,
                    startDate
                }));
            }
            return intervals;
        }
        *calculateLateEndDateConstraintIntervals() {
            const intervals = yield* superProto.calculateLateEndDateConstraintIntervals.call(this);
            const direction = yield this.$.effectiveDirection;
            if (direction.direction === Direction.Forward) {
                const endDate = yield this.$.earlyEndDate;
                endDate && intervals.push(this.projectConstraintIntervalClass.new({
                    owner: this,
                    side: ConstraintIntervalSide.End,
                    endDate
                }));
            }
            else if (direction.direction === Direction.Backward) {
                const endDate = yield this.$.endDate;
                endDate && intervals.push(this.projectConstraintIntervalClass.new({
                    owner: this,
                    side: ConstraintIntervalSide.End,
                    endDate
                }));
            }
            return intervals;
        }
        getDefaultEventModelClass() {
            return GanttEvent;
        }
        getDefaultAssignmentModelClass() {
            return SchedulerProAssignmentMixin;
        }
        getDefaultResourceModelClass() {
            return SchedulerProResourceMixin;
        }
        getDefaultEventStoreClass() {
            return ChronoEventTreeStoreMixin;
        }
        getType() {
            return ProjectType.Gantt;
        }
        // this method is only used to calculated "initial" project start date only
        *unsafeCalculateInitialMinChildrenStartDateDeep() {
            const childEvents = yield this.$.childEvents;
            // note, that we does not yield here, as we want to calculate "initial" project start date
            // which will be used only if there's no user input or explicit setting for it
            // such project date should be calculated as earliest date of all tasks, based on the
            // "initial" data (which includes proposed)
            if (!childEvents.size)
                return yield UnsafeProposedOrPreviousValueOf(this.$.startDate);
            let result = MAX_DATE, child;
            const toProcess = [...childEvents];
            while ((child = toProcess.shift())) {
                let childDate = yield UnsafeProposedOrPreviousValueOf(child.$.startDate);
                // in case a task has no start date but has end date provided - use that value
                if (!childDate) {
                    childDate = yield UnsafeProposedOrPreviousValueOf(child.$.endDate);
                }
                if (childDate && childDate < result)
                    result = childDate;
                toProcess.push(...yield child.$.childEvents);
            }
            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null;
        }
        *unsafeCalculateInitialMaxChildrenEndDateDeep() {
            const childEvents = yield this.$.childEvents;
            // note, that we use "unsafe" ProposedOrPrevious effect here, because we only get into this method
            // if there's no user input for the project end date
            if (!childEvents.size)
                return yield UnsafeProposedOrPreviousValueOf(this.$.endDate);
            let result = MIN_DATE, child;
            const toProcess = [...childEvents];
            while ((child = toProcess.shift())) {
                let childDate = yield UnsafeProposedOrPreviousValueOf(child.$.endDate);
                // in case a task has no end date but has start date provided - use that value
                if (!childDate) {
                    childDate = yield UnsafeProposedOrPreviousValueOf(child.$.startDate);
                }
                if (childDate && childDate > result)
                    result = childDate;
                toProcess.push(...yield child.$.childEvents);
            }
            return (result.getTime() !== MIN_DATE.getTime() && result.getTime() !== MAX_DATE.getTime()) ? result : null;
        }
        getDependencyCycleDetectionIdentifiers(fromEvent, toEvent) {
            return [
                // @ts-ignore
                toEvent.$.earlyStartDateConstraintIntervals,
                // @ts-ignore
                toEvent.$.earlyEndDateConstraintIntervals,
                // @ts-ignore
                toEvent.$.lateEndDateConstraintIntervals,
                // @ts-ignore
                toEvent.$.lateStartDateConstraintIntervals
            ];
        }
    }
    __decorate([
        field({ equality: () => false })
    ], GanttProjectMixin.prototype, "nonEqual", void 0);
    __decorate([
        calculate('nonEqual')
    ], GanttProjectMixin.prototype, "calculateNonEqual", null);
    return GanttProjectMixin;
}) {
}
/**
 * Class providing a [[ProjectConstraintInterval]] instance description.
 */
export class ProjectConstraintIntervalDescription extends ConstraintIntervalDescription {
    static get $name() {
        return 'ProjectConstraintIntervalDescription';
    }
    static getDescription(interval) {
        return format(interval.startDate ? this.L('L{startDateDescriptionTpl}') : this.L('L{endDateDescriptionTpl}'), ...this.getDescriptionParameters(interval));
    }
}
/**
 * Class implementing constraining interval applied by a project.
 * A forward scheduled project implicitly restricts tasks to start not early than the project start date
 * and a backward scheduled project restricts tasks to finish not later than the project end date.
 */
export class ProjectConstraintInterval extends ConstraintInterval {
    isAffectedByTransaction(transaction) {
        const project = this.owner;
        transaction = transaction || project.graph.activeTransaction;
        const dateQuark = transaction.entries.get(this.startDate ? project.$.startDate : project.$.endDate);
        // modified project start (end for BW projects) date
        return dateQuark && !dateQuark.isShadow();
    }
}
__decorate([
    prototypeValue(ProjectConstraintIntervalDescription)
], ProjectConstraintInterval.prototype, "descriptionBuilderClass", void 0);
