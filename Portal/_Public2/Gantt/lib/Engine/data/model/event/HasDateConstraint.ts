import { ChronoIterator } from "../../../../ChronoGraph/chrono/Atom.js"
import { PropagationResult } from "../../../../ChronoGraph/chrono/Graph.js"
import { AnyConstructor, Mixin } from "../../../../ChronoGraph/class/Mixin.js"
import { calculate } from "../../../../ChronoGraph/replica/Entity.js"
import { dateConverter, model_field } from "../../../chrono/ModelFieldAtom.js"
import { ConstraintType } from "../../../scheduling/Types.js"
import { ConstrainedEvent, ConstraintInterval } from "./ConstrainedEvent.js"
import { HasChildren } from "./HasChildren.js"


export const HasDateConstraint = <T extends AnyConstructor<ConstrainedEvent & HasChildren>>(base : T) => {

    class HasDateConstraint extends base {

        /**
         * The type of constraint, applied to this task
         */
        @model_field({ type : 'string' })
        constraintType : ConstraintType

        /**
         * The date of the constraint, applied to this task
         */
        @model_field({ type : 'date', dateFormat : 'YYYY-MM-DDTHH:mm:ssZ' }, { converter : dateConverter })
        constraintDate : Date


        @calculate('constraintType')
        * calculateConstraintType (proposedValue? : ConstraintType) : ChronoIterator<this[ 'constraintType' ] | boolean> {
            let result : this[ 'constraintType' ] = null

            // use proposed constraint type if provided and is applicable to the event
            if (proposedValue !== undefined && (yield* this.isConstraintTypeApplicable(proposedValue))) {
                result = proposedValue
            // use consistent value otherwise (if it's applicable)
            } else {
                const consistentValue = this.$.constraintType.getConsistentValue()

                // this check is probably no longer needed since all data now goes through the "proposed" stage
                if (yield* this.isConstraintTypeApplicable(consistentValue)) {
                    result = consistentValue
                }
            }

            return result
        }

        getStartDatePinConstraintType () : ConstraintType {
            // TODO: for BW projects this should return ConstraintType.StartNoLaterThan
            return this.isTaskPinnableWithConstraint() && ConstraintType.StartNoEarlierThan || null
        }

        getEndDatePinConstraintType () : ConstraintType {
            // TODO: for BW projects this should return ConstraintType.FinishNoLaterThan
            return this.isTaskPinnableWithConstraint() && ConstraintType.FinishNoEarlierThan || null
        }

        /**
         * Indicates if the task can be pinned with a constraint
         * to enforce its start/end date changes.
         * @private
         */
        isTaskPinnableWithConstraint () : boolean {
            let result = false

            // we should not pin manually scheduled tasks
            if (!this.manuallyScheduled) {
                const constraintType = this.constraintType

                if (constraintType) {
                    switch (constraintType) {
                        case ConstraintType.StartNoEarlierThan :
                        case ConstraintType.FinishNoEarlierThan :
                            result = true
                    }

                // no constraints -> we can pin
                } else {
                    result = true
                }
            }

            return result
        }

        async setStartDate (date : Date, keepDuration : boolean = true) : Promise<PropagationResult> {
            // get constraint type that should be used to enforce start date or
            // null if the change cannot be enforced (happens when the task is manually scheduled so no need for enforcement or
            // some constraint is already set)
            const constrainType = this.getStartDatePinConstraintType()

            if (constrainType) {
                this.$.constraintType.put(constrainType)
                this.$.constraintDate.put(date)
            }

            return super.setStartDate(date, keepDuration)
        }

        async setEndDate (date : Date, keepDuration : boolean = false) : Promise<PropagationResult> {
            // if we move the event
            if (keepDuration) {
                // get constraint type that should be used to enforce end date or
                // null if the change cannot be enforced (happens when the task is manually scheduled so no need for enforcement or
                // some constraint is already set)
                const constrainType = this.getEndDatePinConstraintType()

                if (constrainType) {
                    this.$.constraintType.put(constrainType)
                    this.$.constraintDate.put(date)
                }
            }

            return super.setEndDate(date, keepDuration)
        }

        @calculate('constraintDate')
        * calculateConstraintDate (proposedValue? : Date) : ChronoIterator<this[ 'constraintDate' ]> {

            let result : Date
            const constraintType = yield this.$.constraintType

            // use proposed constraint date if provided
            if (proposedValue !== undefined) {
                result = proposedValue
            // if no constraint type -> reset constraint date
            } else if (!constraintType) {
                result = null
            // fill constraint date based on constraint type provided
            } else {
                result = this.getConstraintTypeDefaultDate(constraintType) || this.$.constraintDate.getConsistentValue()
            }

            return result
        }

        /**
         * Returns default constraint date value for the constraint type provided
         * (either start or end date of the event).
         */
        getConstraintTypeDefaultDate (constraintType : ConstraintType) : Date {
            switch (constraintType) {
                case ConstraintType.StartNoEarlierThan :
                case ConstraintType.StartNoLaterThan :
                case ConstraintType.MustStartOn :
                    return this.startDate

                case ConstraintType.FinishNoEarlierThan :
                case ConstraintType.FinishNoLaterThan :
                case ConstraintType.MustFinishOn :
                    return this.endDate
            }

            return null
        }

        /**
         * Returns true if the provided constraint type is applicable to the event.
         *
         * @param {ConstraintType} constraintType Constraint type.
         * @returns `True` if the provided constraint type is applicable (`false` otherwise).
         */
        * isConstraintTypeApplicable (constraintType : ConstraintType) : ChronoIterator<boolean> {
            const childEvents = yield this.$.childEvents

            // Take into account if the event is leaf
            const isSummary : boolean = childEvents.size > 0

            switch (constraintType) {
                // these constraints are applicable to leaves only
                case ConstraintType.FinishNoEarlierThan :
                case ConstraintType.StartNoLaterThan :
                case ConstraintType.MustFinishOn :
                case ConstraintType.MustStartOn :
                    return !isSummary
            }

            return true
        }

        /**
         * Sets the constraint type to the event.
         * @param {ConstraintType} constraintType Constraint type.
         * @returns Promise<PropagationResult>
         */
        setConstraintType : (constrainType : ConstraintType) => Promise<PropagationResult>

        /**
         * Sets the constraint type to the event.
         * @param {Date}   constraintDate Constraint date.
         * @returns Promise<PropagationResult>
         */
        setConstraintDate : (constrainDate : Date) => Promise<PropagationResult>

        /**
         * Sets the constraint type (if applicable) and constraining date to the task.
         * @param {ConstraintType} constraintType Constraint type.
         * @param {Date}   constraintDate Constraint date.
         * @returns Promise<PropagationResult>
         */
        async setConstraint (constraintType : ConstraintType, constraintDate? : Date) : Promise<PropagationResult> {
            this.$.constraintType.put(constraintType)

            if (constraintDate !== undefined) {
                this.$.constraintDate.put(constraintDate)
            }

            return this.propagate()
        }


        protected * calculateEndDateConstraintIntervals () : ChronoIterator<this[ 'endDateConstraintIntervals' ]> {

            const intervals : this[ 'endDateConstraintIntervals' ] = yield* super.calculateEndDateConstraintIntervals()

            const constraintType = yield this.$.constraintType

            if (constraintType) {

                const constraintDate = yield this.$.constraintDate

                if (constraintDate) {
                    // if constraint type is
                    switch (constraintType) {

                        case ConstraintType.MustFinishOn :
                            intervals.unshift(ConstraintInterval.new({
                                startDate           : constraintDate,
                                endDate             : constraintDate,
                                originDescription   : '"Must Finish On" constraint',
                                // TODO:
                                // onRemoveAction      : this.getOnRemoveAction(dependency)
                            }))
                            break

                        case ConstraintType.FinishNoEarlierThan :
                            intervals.unshift(ConstraintInterval.new({
                                startDate         : constraintDate,
                                originDescription : '"Finish No Ealier Than" constraint'
                            }))
                            break

                        case ConstraintType.FinishNoLaterThan :
                            intervals.unshift(ConstraintInterval.new({
                                endDate           : constraintDate,
                                originDescription : '"Finish No Ealier Than" constraint'
                            }))
                            break
                    }
                }
            }

            return intervals
        }


        protected * calculateStartDateConstraintIntervals () : ChronoIterator<this[ 'startDateConstraintIntervals' ]> {

            const intervals : this[ 'startDateConstraintIntervals' ] = yield* super.calculateStartDateConstraintIntervals()

            const constraintType : ConstraintType = yield this.$.constraintType

            if (constraintType) {

                const constraintDate = yield this.$.constraintDate

                if (constraintDate) {
                    // if constraint type is
                    switch (constraintType) {

                        case ConstraintType.MustStartOn :
                            intervals.unshift(ConstraintInterval.new({
                                startDate         : constraintDate,
                                endDate           : constraintDate,
                                originDescription : '"Must Start On" constraint',
                                // TODO:
                                // onRemoveAction    : this.getOnRemoveAction(dependency)
                            }))
                            break

                        case ConstraintType.StartNoEarlierThan :
                            intervals.unshift(ConstraintInterval.new({
                                startDate         : constraintDate,
                                originDescription : '"Start No Ealier Than" constraint'
                            }))
                            break

                        case ConstraintType.StartNoLaterThan :
                            intervals.unshift(ConstraintInterval.new({
                                endDate           : constraintDate,
                                originDescription : '"Start No Later Than" constraint'
                            }))
                            break
                    }
                }
            }

            return intervals
        }

    }

    return HasDateConstraint
}

/**
 * This mixin adds support for various constraints for the task. The type of constraint is defined by the
 * [[constraintType]] property.
 */
export interface HasDateConstraint extends Mixin<typeof HasDateConstraint> {}
