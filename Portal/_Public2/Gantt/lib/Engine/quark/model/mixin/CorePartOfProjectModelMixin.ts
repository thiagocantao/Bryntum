import { AnyConstructor, Mixin } from "../../../../ChronoGraph/class/BetterMixin.js"
import { CorePartOfProjectGenericMixin } from "../../CorePartOfProjectGenericMixin.js"
import Model from "../../../../Core/data/Model.js"
import { AbstractPartOfProjectModelMixin } from "./AbstractPartOfProjectModelMixin.js"


/**
 * This a mixin for every Model that belongs to a scheduler_core project.
 *
 * It adds functions needed to calculate invalidated fields on project commit.
 */
export class CorePartOfProjectModelMixin extends Mixin(
    [
        AbstractPartOfProjectModelMixin,
        CorePartOfProjectGenericMixin,
        Model
    ],
    (base : AnyConstructor<
        AbstractPartOfProjectModelMixin &
        CorePartOfProjectGenericMixin &
        Model
        ,
        typeof AbstractPartOfProjectModelMixin &
        typeof CorePartOfProjectGenericMixin &
        typeof Model
>) => {

    const superProto : InstanceType<typeof base> = base.prototype


    class CorePartOfProjectModelMixin extends base {

        // Flag set during calculation
        $isCalculating   = false

        // Proposed changes
        $changed         : any = {}

        // Value before proposed change, for buckets that need to update data early
        $beforeChange    : any = {}


        // Invalidate record upon joining project, leads to a buffered commit
        joinProject () {
            this.invalidate()
        }


        // Trigger a buffered commit when leaving the project
        leaveProject (isReplacing : boolean = false) {
            superProto.leaveProject.call(this, isReplacing)
            this.project?.bufferedCommitAsync()
        }


        /**
         * Invalidates this record, queueing it for calculation on project commit.
         */
        invalidate () {
            this.project?.invalidate(this)
        }


        /**
         * Used to retrieve the proposed (before 'dataReady') or current (after 'dataReady') value for a field.
         * If there is no proposed change, it is functionally equal to a normal `record.get()` call.
         */
        getCurrentOrProposed (fieldName : string) : any  {
            if (fieldName in this.$changed) {
                return this.$changed[fieldName]
            }

            return this.get(fieldName) ?? null
        }


        /**
         * Determines if the specified field has a value or not, value can be either current or proposed.
         */
        hasCurrentOrProposed (fieldName : string) : boolean {
            return this.$changed[fieldName] != null || this.get(fieldName) != null
        }


        /**
         * Propose changes, to be considered during calculation. Also invalidates the record.
         */
        propose (changes : any) {
            Object.keys(changes).forEach(field => {
                this.$changed[field] = changes[field]
            })

            this.invalidate()
        }


        /**
         * Similar to propose, but with more options. Mostly used by buckets, since they need data to update early.
         */
        setChanged (field, value, invalidate = true, setData = false) {
            const me                       = this

            me.$changed[field]             = value

            // Buckets need to keep data up to date immediately
            if (setData) {
                if (!(field in me.$beforeChange)) {
                    me.$beforeChange[field] = me.get(field)
                }

                me.setData(field, value)
            }

            invalidate && me.invalidate()
        }


        /**
         * Hook called before project refresh, override and calculate required changes in subclasses
         */
        calculateInvalidated () {}


        /**
         * Called after project refresh, before dataReady. Announce updated data
         */
        finalizeInvalidated (silent = false) {
            const me                  = this

            me.$isCalculating         = true

            // Started appearing in Chrome, no clue what it is, but breaks things.
            // Cannot be removed, but clones away
            if (me.$changed.__REFADR__) {
                me.$changed = {...me.$changed}
            }

            // First silently revert any data change (used by buckets), otherwise it wont be detected by `set()`
            Object.keys(me.$beforeChange).forEach(fieldName => {
                me.setData(fieldName, me.$beforeChange[fieldName])
            })

            // Then do a proper set
            me.set(me.$changed, null, silent)

            me.$changed              = {}
            me.$beforeChange         = {}
            me.$isCalculating        = false
        }

    }

    return CorePartOfProjectModelMixin
}){}

