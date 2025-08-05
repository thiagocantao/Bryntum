import { AnyConstructor } from "../../../../ChronoGraph/class/Mixin.js"
import { Entity } from "../../../../ChronoGraph/replica/Entity.js"
import Model from "../../../../Common/data/Model.js"
import { PartOfProjectGenericMixin } from "../../PartOfProjectGenericMixin.js"
import { HasCalendarMixin } from "../HasCalendarMixin.js"
import { ChronoModelMixin } from "../mixin/ChronoModelMixin.js"
import { PartOfProjectMixin } from "../mixin/PartOfProjectMixin.js"
import { ConstrainedEvent } from "./ConstrainedEvent.js"
import { EventMixin } from "./EventMixin.js"
import { FixedDuration } from "./FixedDuration.js"
import { FixedEffort } from "./FixedEffort.js"
import { FixedUnits } from "./FixedUnits.js"
import { HasAssignments } from "./HasAssignments.js"
import { HasChildren } from "./HasChildren.js"
import { HasDateConstraint } from "./HasDateConstraint.js"
import { HasDependencies } from "./HasDependencies.js"
import { HasPercentDone } from "./HasPercentDone.js"

/**
 * Function to build an event class for the Bryntum Gantt
 */
export const BuildBryntumEvent = (base : typeof Model = Model)
    : AnyConstructor<HasPercentDone & HasDateConstraint & FixedUnits & FixedDuration & FixedEffort & HasDependencies> =>
        (HasDateConstraint as any)(
        FixedUnits(
        FixedEffort(
        FixedDuration(
        HasAssignments(
        HasPercentDone(
        HasChildren(
        HasDependencies(
        ConstrainedEvent(
        EventMixin(
        HasCalendarMixin(
        PartOfProjectMixin(
        PartOfProjectGenericMixin(
        ChronoModelMixin(
        Entity(
            base
        )))))))))))))))

/**
 * The default class for event, used in Bryntum Gantt.
 *
 * It is configured from the following mixins, providing the maximum scheduling functionality:
 *
 * * [[HasDateConstraint]]
 * * [[FixedUnits]]
 * * [[FixedEffort]]
 * * [[FixedDuration]]
 * * [[HasAssignments]]
 * * [[HasChildren]]
 * * [[ConstrainedEvent]]
 * * [[EventMixin]]
 * * [[HasCalendarMixin]]
 */
export class BryntumEvent extends BuildBryntumEvent(Model) {}
