import { ProposedOrPrevious, ProposedOrPreviousValueOf } from '../../../../ChronoGraph/chrono/Effect.js'
import { CommitResult } from '../../../../ChronoGraph/chrono/Graph.js'
import { Identifier } from "../../../../ChronoGraph/chrono/Identifier.js"
import { Quark } from "../../../../ChronoGraph/chrono/Quark.js"
import { Transaction } from "../../../../ChronoGraph/chrono/Transaction.js"
import { AnyConstructor, Mixin } from '../../../../ChronoGraph/class/BetterMixin.js'
import { CalculationIterator } from '../../../../ChronoGraph/primitives/Calculation.js'
import { calculate, write } from '../../../../ChronoGraph/replica/Entity.js'
import { prototypeValue } from '../../../../ChronoGraph/util/Helpers.js'
import DateHelper from '../../../../Core/helper/DateHelper.js'
import Localizable from '../../../../Core/localization/Localizable.js'
import { ConflictResolution, ConstraintInterval, ConstraintIntervalDescription } from '../../../chrono/Conflict.js'
import { dateConverter, model_field } from '../../../chrono/ModelFieldAtom.js'
import "../../../localization/En.js"
import { ConstraintIntervalSide, ConstraintType, Direction } from '../../../scheduling/Types.js'
import { format } from '../../../util/Functions.js'
import { HasChildrenMixin } from '../scheduler_basic/HasChildrenMixin.js'
import { ConstrainedEarlyEventMixin } from './ConstrainedEarlyEventMixin.js'

/**
 * This mixin implements a date-based based constraint for the event.
 * It provides the following constraint types:
 *
 * - _Start no earlier than (SNET)_ - restricts the event to start on or after the specified date.
 * - _Finish no earlier than (FNET)_ - restricts the event to finish on or after the specified date.
 * - _Start no later than (SNLT)_ - restricts the event to start before (or on) the specified date.
 * - _Finish no later than (FNLT)_ - restricts the event to finish before (or on) the specified date.
 * - _Must start on (MSO)_ - restricts the event to start on the specified date.
 * - _Must finish on (MFO)_ - restricts the event to finish on the specified date.
 *
 * The type of constraint is defined by the [[constraintType]] property. Types has self-descriptive names.
 * There's also [[constraintDate]] with a constraint date.
 *
 * **Please note** that [[manuallyScheduled|manually scheduled]] events ignore their constraints.
 */
export class HasDateConstraintMixin extends Mixin(
    [ ConstrainedEarlyEventMixin, HasChildrenMixin ],
    (base : AnyConstructor<ConstrainedEarlyEventMixin & HasChildrenMixin, typeof ConstrainedEarlyEventMixin & typeof HasChildrenMixin>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class HasDateConstraint extends base {

        /**
         * The type of constraint, applied to this event
         */
        @model_field({ type : 'string' }, { sync : true })
        constraintType : ConstraintType

        /**
         * The date of the constraint, applied to this event
         */
        @model_field({ type : 'date' }, { converter : dateConverter, sync : true })
        constraintDate : Date

        // This flag allows to ignore setting pinning constraint when writing start date
        ignorePinningConstraint : boolean = false

        // Flag set in Model.js when reverting changes
        isReverting : boolean

        writeStartDate (me : Identifier, transaction : Transaction, quark : Quark, date : Date, keepDuration : boolean = true) {
            // get constraint type that should be used to enforce start date or
            // null if the change cannot be enforced (happens when the task is manually scheduled so no need for enforcement or
            // some constraint is already set)

            const project = this.getProject()

            // `writeStartDate` will be called for initial write to the `startDate` at the point of adding it to graph
            // at that time there possibly be no `direction` identifier yet
            // it seems this line relies on the fact, that `direction` field is declared after the `startDate`
            if (
                project?.addConstraintOnDateSet
                    && transaction.graph.hasIdentifier(this.$.effectiveDirection)
                    && !project?.eventStore.isSyncingDataOnLoad
                    && !this.isReverting
                    && !project?.getStm().isRestoring
            ) {
                const constrainType = this.getStartDatePinConstraintType()

                if (constrainType) {
                    // the order is important here, because in the absence of proposed value for the `constraintDate`
                    // it is filled with the default value from the project start, when STM transaction is being created
                    // so first set the date
                    this.constraintDate = date
                    // then the constraint type
                    this.constraintType = constrainType
                }
            }

            return superProto.writeStartDate.call(this, me, transaction, quark, date, keepDuration)
        }


        writeEndDate (me : Identifier, transaction : Transaction, quark : Quark, date : Date, keepDuration : boolean = false) {
            // get constraint type that should be used to enforce End date or
            // null if the change cannot be enforced (happens when the task is manually scheduled so no need for enforcement or
            // some constraint is already set)

            const project = this.getProject()

            if (
                project?.addConstraintOnDateSet
                    && transaction.graph.hasIdentifier(this.$.direction)
                    && keepDuration
                    && !project?.eventStore.isSyncingDataOnLoad
                    && !project?.getStm().isRestoring
            ) {
                const constrainType = this.getEndDatePinConstraintType()

                if (constrainType) {
                    // see the comment above, the order is important
                    this.constraintDate = date
                    this.constraintType = constrainType
                }
            }

            return superProto.writeEndDate.call(this, me, transaction, quark, date, keepDuration)
        }


        @write('constraintType')
        writeConstraintType (me : Identifier, transaction : Transaction, quark : Quark, constraintType : ConstraintType, fromWriteDirection : boolean = false) {
            const project   = this.getProject()

            // in case we use MSProject compatibility mode we need to establish the extra mapping
            if (!fromWriteDirection && !me.isWritingUndefined && project.includeAsapAlapAsConstraints) {
                if (
                    constraintType !== ConstraintType.AsSoonAsPossible
                    && constraintType !== ConstraintType.AsLateAsPossible
                ) {
                    // ignore the initial write
                    if (transaction.baseRevision.hasIdentifier(me)) {
                        // this is basically: this.direction = null, however it will correctly work with data branches
                        this.$.direction.write.call(this, this.$.direction, transaction, undefined, null, true)
                    }
                }
                else {
                    // this is basically: this.direction = ..., however it will correctly work with data branches
                    this.$.direction.write.call(
                        this, this.$.direction, transaction, undefined,
                        constraintType === ConstraintType.AsSoonAsPossible ? Direction.Forward : Direction.Backward,
                        true
                    )

                    this.$.constraintDate.write.call(this, this.$.constraintDate, transaction, undefined, null)
                }
            }

            me.constructor.prototype.write.call(this, me, transaction, quark, constraintType)
        }


        @write('direction')
        writeDirection (me : Identifier, transaction : Transaction, quark : Quark, direction : Direction, fromWriteConstraintType : boolean = false) {
            const project   = this.getProject()

            // If we are not in the middle of writing a constraint
            if (!fromWriteConstraintType &&
                // not undefined value is provided
                !me.isWritingUndefined && project.includeAsapAlapAsConstraints &&
                // not null is provided ..or null but it's not the initial write so must be caused by explicit event.direction=null
                (direction || transaction.baseRevision.hasIdentifier(me))
            ) {
                // set constraint type matching the provided direction value
                this.$.constraintType.write.call(
                    this, this.$.constraintType, transaction, undefined,
                    direction === Direction.Forward
                        ? ConstraintType.AsSoonAsPossible
                        : direction === Direction.Backward
                            ? ConstraintType.AsLateAsPossible
                            : null,
                    true
                )
            }

            me.constructor.prototype.write.call(this, me, transaction, quark, direction)
        }


        @calculate('constraintType')
        * calculateConstraintType () : CalculationIterator<ConstraintType> {
            let constraintType : this[ 'constraintType' ]    = yield ProposedOrPrevious

            // use proposed constraint type if provided and is applicable to the event
            if (!(yield* this.isConstraintTypeApplicable(constraintType))) {
                constraintType   = null
            }

            return constraintType
        }


        @calculate('constraintDate')
        * calculateConstraintDate (Y) : CalculationIterator<Date> {

            let constraintDate : Date               = yield ProposedOrPrevious
            const constraintType : ConstraintType   = yield this.$.constraintType

            if (!constraintType) {
                constraintDate      = null
            }
            // use proposed constraint date if provided
            else if (!constraintDate) {
                // fill constraint date based on constraint type provided
                constraintDate      = this.getConstraintTypeDefaultDate(Y, constraintType)
            }

            return constraintDate
        }


        getStartDatePinConstraintType () : ConstraintType {
            const { effectiveDirection : direction } = this.project

            if (!this.isTaskPinnableWithConstraint()) return null

            switch (direction.direction) {
                case Direction.Forward : return ConstraintType.StartNoEarlierThan

                case Direction.Backward : return ConstraintType.StartNoLaterThan
            }
        }


        getEndDatePinConstraintType () : ConstraintType {
            const { effectiveDirection : direction } = this.project

            if (!this.isTaskPinnableWithConstraint()) return null

            switch (direction.direction) {
                case Direction.Forward : return ConstraintType.FinishNoEarlierThan

                case Direction.Backward : return ConstraintType.FinishNoLaterThan
            }
        }


        /**
         * Indicates if the task can be pinned with a constraint
         * to enforce its start/end date changes.
         * @private
         */
        isTaskPinnableWithConstraint () : boolean {
            const { manuallyScheduled, ignorePinningConstraint, constraintType } = this

            let result = false

            // we should not pin manually scheduled tasks
            if (!manuallyScheduled && !ignorePinningConstraint) {

                if (constraintType) {
                    switch (constraintType) {
                        case ConstraintType.AsSoonAsPossible :
                        case ConstraintType.AsLateAsPossible :
                        case ConstraintType.StartNoEarlierThan :
                        case ConstraintType.StartNoLaterThan :
                        case ConstraintType.FinishNoEarlierThan :
                        case ConstraintType.FinishNoLaterThan :
                            result = true
                    }
                }
                // no constraints -> we can pin
                else {
                    result = true
                }
            }

            return result
        }

        applyChangeset (rawChanges : any, phantomIdField : string, remote : boolean) : any {
            // Raise a flag on the record to avoid engine setting pinning constraint when writing start date. We need to
            // avoid pinning constraint in case remote dataset only contains start date for the parent record. If local
            // parent record start date is different, start date will be written to the engine and constraint will be set.
            // We don't need to do that here, dataset is supposed to complete so parent ends up with the same start date
            // without unnecessary constraint
            // https://github.com/bryntum/support/issues/5086
            this.ignorePinningConstraint = remote

            //@ts-ignore
            const result = super.applyChangeset(rawChanges, phantomIdField, remote)

            this.ignorePinningConstraint = false

            return result
        }


        /**
         * Returns default constraint date value for the constraint type provided
         * (either start or end date of the event).
         */
        getConstraintTypeDefaultDate (Y, constraintType : ConstraintType) : Date {
            switch (constraintType) {
                case ConstraintType.StartNoEarlierThan :
                case ConstraintType.StartNoLaterThan :
                case ConstraintType.MustStartOn :
                    return Y(ProposedOrPreviousValueOf(this.$.startDate))

                case ConstraintType.FinishNoEarlierThan :
                case ConstraintType.FinishNoLaterThan :
                case ConstraintType.MustFinishOn :
                    return Y(ProposedOrPreviousValueOf(this.$.endDate))
            }

            return null
        }

        /**
         * Returns true if the provided constraint type is applicable to the event.
         *
         * @param {ConstraintType} constraintType Constraint type.
         * @returns `True` if the provided constraint type is applicable (`false` otherwise).
         */
        * isConstraintTypeApplicable (constraintType : ConstraintType) : CalculationIterator<boolean> {
            // Take into account if the event is leaf
            const hasSubEvents = yield* this.hasSubEvents()

            switch (constraintType) {
                // these constraints are applicable to leaves only
                case ConstraintType.FinishNoEarlierThan :
                case ConstraintType.StartNoLaterThan :
                case ConstraintType.MustFinishOn :
                case ConstraintType.MustStartOn :
                    return !hasSubEvents
            }

            return true
        }

        /**
         * Sets the [[constraintType|constraint type]] of the event.
         * @param {ConstraintType} constraintType Constraint type.
         * @returns Promise<PropagateResult>
         */
        setConstraintType : (constrainType : ConstraintType) => Promise<CommitResult>

        /**
         * Sets the [[constraintDate|constraint date]] of the event.
         * @param {Date}   constraintDate Constraint date.
         * @returns Promise<PropagateResult>
         */
        setConstraintDate : (constrainDate : Date) => Promise<CommitResult>

        /**
         * Sets the constraint type (if applicable) and constraining date to the task.
         * @param {ConstraintType}  constraintType   Constraint type.
         * @param {Date}            [constraintDate] Constraint date.
         * @returns Promise<PropagateResult>
         */
        async setConstraint (constraintType : ConstraintType, constraintDate? : Date) : Promise<CommitResult> {
            this.constraintType = constraintType

            if (constraintDate !== undefined) {
                this.constraintDate = constraintDate
            }

            return this.commitAsync()
        }


        * calculateEndDateConstraintIntervals () : CalculationIterator<this[ 'endDateConstraintIntervals' ]> {
            const intervals : this[ 'endDateConstraintIntervals' ] = yield* superProto.calculateEndDateConstraintIntervals.call(this)

            const manuallyScheduled : boolean       = yield this.$.manuallyScheduled
            const constraintType : ConstraintType   = yield this.$.constraintType
            const constraintDate : Date             = yield this.$.constraintDate

            const dateConstraintIntervalClass : typeof DateConstraintInterval = this.project.dateConstraintIntervalClass

            // manually scheduled task ignores its constraints
            if (!manuallyScheduled && constraintType && constraintDate) {
                // if constraint type is
                switch (constraintType) {
                    case ConstraintType.MustFinishOn :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.End,
                            startDate   : constraintDate,
                            endDate     : constraintDate
                        }))
                        break

                    case ConstraintType.FinishNoEarlierThan :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.End,
                            startDate   : constraintDate
                        }))
                        break

                    case ConstraintType.FinishNoLaterThan :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.End,
                            endDate     : constraintDate
                        }))
                        break
                }
            }

            return intervals
        }


        * calculateStartDateConstraintIntervals () : CalculationIterator<this[ 'startDateConstraintIntervals' ]> {
            const intervals : this[ 'startDateConstraintIntervals' ] = yield* superProto.calculateStartDateConstraintIntervals.call(this)

            const manuallyScheduled : boolean       = yield this.$.manuallyScheduled
            const constraintType : ConstraintType   = yield this.$.constraintType
            const constraintDate : Date             = yield this.$.constraintDate

            const dateConstraintIntervalClass : typeof DateConstraintInterval = this.project.dateConstraintIntervalClass

            // manually scheduled task ignores its constraints
            if (!manuallyScheduled && constraintType && constraintDate) {
                // if constraint type is
                switch (constraintType) {
                    case ConstraintType.MustStartOn :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.Start,
                            startDate   : constraintDate,
                            endDate     : constraintDate
                        }))
                        break

                    case ConstraintType.StartNoEarlierThan :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.Start,
                            startDate   : constraintDate
                        }))
                        break

                    case ConstraintType.StartNoLaterThan :
                        intervals.unshift(dateConstraintIntervalClass.new({
                            owner       : this,
                            side        : ConstraintIntervalSide.Start,
                            endDate     : constraintDate
                        }))
                        break
                }
            }

            return intervals
        }
    }

    return HasDateConstraint
}){}


/**
 * Class implements resolving a scheduling conflict happened due to a task constraint.
 * It resolves the conflict by removing the constraint.
 */
export class RemoveDateConstraintConflictResolution extends Localizable(ConflictResolution) {

    static get $name () {
        return 'RemoveDateConstraintConflictResolution'
    }

    event           : HasDateConstraintMixin

    interval        : DateConstraintInterval

    construct () {
        super.construct(...arguments)
        this.event = this.interval.owner
    }

    getDescription () : string {
        const { event } = this

        return format(this.L('L{descriptionTpl}'),
            event.name || event.id,
            this.interval.getConstraintName(event.constraintType)
        )
    }

    /**
     * Resolves the conflict by removing the event constraint.
     */
    resolve () {
        this.event.constraintType = null
    }
}

/**
 * Description builder for an [[DateConstraintInterval|event constraint interval]].
 */
export class DateConstraintIntervalDescription extends ConstraintIntervalDescription {

    static get $name () {
        return 'DateConstraintIntervalDescription'
    }

    /**
     * Returns description for the provided event constraint interval.
     * @param interval Constraint interval
     */
    static getDescription (interval : DateConstraintInterval) : string {
        let tpl : string

        switch (interval.owner.constraintType) {
            case ConstraintType.StartNoEarlierThan :
            case ConstraintType.FinishNoEarlierThan :
            case ConstraintType.MustStartOn :
            case ConstraintType.MustFinishOn :
                tpl = this.L('L{startDateDescriptionTpl}')
                break

            case ConstraintType.StartNoLaterThan :
            case ConstraintType.FinishNoLaterThan :
                tpl = this.L('L{endDateDescriptionTpl}')
                break
        }

        return format(tpl, ...this.getDescriptionParameters(interval))
    }

    /**
     * Returns localized constraint name.
     * @param constraintType Type of constraint
     */
    static getConstraintName (constraintType : ConstraintType) : string {
        return this.L('L{constraintTypeTpl}')[constraintType]
    }


    static getDescriptionParameters (interval : DateConstraintInterval) : any[] {
        const event         = interval.owner

        return [
            DateHelper.format(interval.startDate, this.L('L{dateFormat}')),
            DateHelper.format(interval.endDate, this.L('L{dateFormat}')),
            event.name || event.id,
            this.getConstraintName(event.constraintType)
        ]
    }

}

/**
 * Class implements an interval applied by an event [[constraintType|constraint]].
 * The interval suggests the only resolution option - removing the constraint.
 */
export class DateConstraintInterval extends ConstraintInterval {

    /**
     * The event applying the interval.
     */
    owner                                       : HasDateConstraintMixin //event

    /**
     * Class implementing "removing the constraint" resolution.
     */
    @prototypeValue(RemoveDateConstraintConflictResolution)
    removeDateConstraintConflictResolutionClass : typeof RemoveDateConstraintConflictResolution

    /**
     * Class implementing the interval description builder.
     */
    @prototypeValue(DateConstraintIntervalDescription)
    descriptionBuilderClass                     : typeof DateConstraintIntervalDescription

    getConstraintName (constraintType : ConstraintType) : string {
        return this.descriptionBuilderClass.getConstraintName(constraintType || this.owner.constraintType)
    }

    getDescription () : string {
        return this.descriptionBuilderClass.getDescription(this)
    }

    isAffectedByTransaction (transaction? : Transaction) : boolean {
        const event = this.owner

        transaction = transaction || event.graph.activeTransaction

        const
            constraintDateQuark = transaction.entries.get(event.$.constraintDate),
            constraintTypeQuark = transaction.entries.get(event.$.constraintType)

        // new constrained event or modified constraint
        return !transaction.baseRevision.hasIdentifier(event.$$) ||
            constraintDateQuark && !constraintDateQuark.isShadow() ||
            constraintTypeQuark && !constraintTypeQuark.isShadow()
    }

    /**
     * Returns possible resolution options for cases when
     * the interval takes part in a conflict.
     *
     * The interval suggests the only resolution option - removing the constraint.
     */
    getResolutions () : ConflictResolution[] {
        return this.resolutions || (this.resolutions = [
            this.removeDateConstraintConflictResolutionClass.new({ interval : this })
        ])
    }
}
