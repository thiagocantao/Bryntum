import { Mixin } from "../../../../ChronoGraph/class/BetterMixin.js";
import { CorePartOfProjectGenericMixin } from "../../CorePartOfProjectGenericMixin.js";
import Model from "../../../../Core/data/Model.js";
import { AbstractPartOfProjectModelMixin } from "./AbstractPartOfProjectModelMixin.js";
/**
 * This a mixin for every Model that belongs to a scheduler_core project.
 *
 * It adds functions needed to calculate invalidated fields on project commit.
 */
export class CorePartOfProjectModelMixin extends Mixin([
    AbstractPartOfProjectModelMixin,
    CorePartOfProjectGenericMixin,
    Model
], (base) => {
    const superProto = base.prototype;
    class CorePartOfProjectModelMixin extends base {
        constructor() {
            super(...arguments);
            // Flag set during calculation
            this.$isCalculating = false;
            // Proposed changes
            this.$changed = {};
            // Value before proposed change, for buckets that need to update data early
            this.$beforeChange = {};
        }
        get isInActiveTransaction() {
            return true;
        }
        // Invalidate record upon joining project, leads to a buffered commit
        joinProject() {
            this.invalidate();
        }
        // Trigger a buffered commit when leaving the project
        leaveProject(isReplacing = false) {
            superProto.leaveProject.call(this, isReplacing);
            this.project?.bufferedCommitAsync();
        }
        /**
         * Invalidates this record, queueing it for calculation on project commit.
         */
        invalidate() {
            this.project?.invalidate(this);
        }
        /**
         * Used to retrieve the proposed (before 'dataReady') or current (after 'dataReady') value for a field.
         * If there is no proposed change, it is functionally equal to a normal `record.get()` call.
         */
        getCurrentOrProposed(fieldName) {
            if (fieldName in this.$changed && this.$changed[fieldName] !== true) {
                return this.$changed[fieldName];
            }
            return this.get(fieldName) ?? null;
        }
        /**
         * Determines if the specified field has a value or not, value can be either current or proposed.
         */
        hasCurrentOrProposed(fieldName) {
            return ((fieldName in this.$changed) && this.$changed[fieldName] != true) || this.get(fieldName) != null;
        }
        /**
         * Propose changes, to be considered during calculation. Also invalidates the record.
         */
        propose(changes) {
            // @ts-ignore
            if (this.project || this.recurringTimeSpan?.project) {
                const keys = Object.keys(changes);
                for (let i = 0; i < keys.length; i++) {
                    const key = keys[i];
                    this.$changed[key] = changes[key];
                }
                this.invalidate();
            }
            else {
                // If no project, behave as a normal model would
                this.set(changes);
            }
        }
        /**
         * Similar to propose, but with more options. Mostly used by buckets, since they need data to update early.
         */
        setChanged(field, value, invalidate = true, setData = false) {
            const me = this;
            me.$changed[field] = value;
            // Buckets need to keep data up to date immediately
            if (setData) {
                if (!(field in me.$beforeChange)) {
                    me.$beforeChange[field] = me.get(field);
                }
                me.setData(field, value);
            }
            invalidate && me.invalidate();
        }
        /**
         * Hook called before project refresh, override and calculate required changes in subclasses
         */
        calculateInvalidated() { }
        /**
         * Called after project refresh, before dataReady. Announce updated data
         */
        finalizeInvalidated(silent = false) {
            const me = this;
            me.$isCalculating = true;
            if (!silent) {
                // First silently revert any data change (used by buckets), otherwise it won't be detected by `set()`
                me.setData(me.$beforeChange);
                // Then do a proper set
                me.set(me.$changed);
            }
            else {
                me.setData(me.$changed);
            }
            me.$changed = {};
            me.$beforeChange = {};
            me.$isCalculating = false;
        }
    }
    return CorePartOfProjectModelMixin;
}) {
}
