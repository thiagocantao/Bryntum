import Base from '../../../Core/Base.js';
import ArrayHelper from '../../../Core/helper/ArrayHelper.js';

/**
 * @module Scheduler/data/mixin/RecurringTimeSpansMixin
 */

const
    emptyArray = Object.freeze([]);

/**
 * This mixin provides recurrence functionality to a store containing {@link Scheduler.model.TimeSpan TimeSpan} models.
 * Normally you don't need to interact with this mixin directly.
 * @mixin
 */
export default Target => class RecurringTimeSpansMixin extends (Target || Base) {
    static get $name() {
        return 'RecurringTimeSpansMixin';
    }

    construct(...args) {
        const me = this;

        // We store all generated occurrences keyed by `_generated_${recurringTimeSpan.id}:${occurrenceStartDate}`
        // So that when asked to generate an occurrence for a date, an already generated one can be returned.
        me.globalOccurrences = new Map();

        // All recurring events added to the store are accessible through this Set. It's used
        // to generate occurrences.
        me.recurringEvents = new Set();

        super.construct(...args);
    }

    // Override to refreshRecurringEventsCache on initial load
    afterLoadData() {
        // All cached occurrences are now potentially invalid.
        // A store reload might imply any number of changes which invalidate any occurrence.
        this.globalOccurrences.clear();

        // Clear and rebuild the recurring events cache
        this.refreshRecurringEventsCache('clear');
        this.refreshRecurringEventsCache('splice', this.storage.allValues);
        super.afterLoadData && super.afterLoadData();
    }

    /**
     * Responds to mutations of the underlying storage Collection.
     *
     * Maintain indices for fast finding of events by date.
     * @param {Object} event
     * @private
     */
    onDataChange({ action, added, removed, replaced }) {
        // Recurring events cache must be refreshed before responding to change
        this.refreshRecurringEventsCache(action, added, removed, replaced);
        super.onDataChange(...arguments);
    }

    refreshRecurringEventsCache(action, added = emptyArray, removed = emptyArray, replaced) {
        const
            me                  = this,
            { recurringEvents } = me,
            replacedCount       = replaced?.length;

        switch (action) {
            case 'clear':
                recurringEvents.clear();
                break;

            // Add and remove
            case 'splice': {
                // Handle replacement of records by instances with same ID
                if (replacedCount) {
                    added = added.slice();
                    removed = removed.slice();

                    for (let i = 0; i < replacedCount; i++) {
                        removed.push(replaced[i][0]);
                        added.push(replaced[i][1]);
                    }
                }

                const
                    addedCount   = added.length,
                    removedCount = removed.length;

                // Track the recurring events we contain
                if (removedCount && recurringEvents.size) {
                    for (let i = 0; i < removedCount; i++) {
                        // If it's being removed, remove it from the recurring events cache.
                        // If it's not a recurring event, it doesn't matter, it won't be in there.
                        recurringEvents.delete(removed[i]);
                    }
                }
                // Track the recurring events we contain
                if (addedCount) {
                    for (let i = 0; i < addedCount; i++) {
                        const newEvent = added[i];

                        // Allow easy access to recurring events
                        if (newEvent.isRecurring) {
                            recurringEvents.add(newEvent);
                        }
                    }
                }
                break;
            }
        }
    }



    getById(id) {
        let result = super.getById(id);

        // If the id is not found in the Store, then it could be one of our generated occurrences
        if (!result) {
            result = this.globalOccurrences.get(this.modelClass.asId(id));
        }

        return result;
    }

    onModelChange(record, toSet, wasSet, silent, fromRelationUpdate) {
        const isRecurrenceRelatedFieldChange = !silent && this.isRecurrenceRelatedFieldChange(record, wasSet);

        // If this is the base of a recurring sequence, then any reactors to events from
        // the super call must regenerate occurrences, so must be done at top.
        // If silent is true, occurrences won't be recalculated. Do not remove occurrences from cache in such case.
        if (isRecurrenceRelatedFieldChange) {
            record.removeOccurrences();
        }

        super.onModelChange(...arguments);

        // If this is the base of a recurring sequence, then the EventStore must
        // trigger a refresh event so that UIs refresh themselves.
        // This could be at the tail end of the creation of an exception
        // or a new recurring base.
        if (isRecurrenceRelatedFieldChange) {
            const event = { action : 'batch', records : this.storage.values };

            this.trigger('refresh', event);
            this.trigger('change', event);
        }
    }

    /**
     * The method restricts which field modifications should trigger timespan occurrences building.
     * By default, any field change of a recurring timespan causes the rebuilding.
     * @param  {Scheduler.model.TimeSpan} timeSpan The modified timespan.
     * @param  {Object} wasSet Object containing the change set.
     * @returns {Boolean} `True` if the fields modification should trigger the timespan occurrences rebuilding.
     * @internal
     * @category Recurrence
     */
    isRecurrenceRelatedFieldChange(timeSpan, wasSet) {
        return timeSpan.isRecurring || 'recurrenceRule' in wasSet;
    }

    /**
     * Builds occurrences for the provided timespan across the provided date range.
     * @private
     * @category Recurrence
     */
    getOccurrencesForTimeSpan(timeSpan, startDate, endDate) {
        const result = [];

        if (timeSpan.isRecurring) {
            timeSpan.recurrence.forEachOccurrence(startDate, endDate, r => result.push(r));
        }

        return result;
    }

    set data(data) {
        // All cached occurrences are now invalid with a new dataset
        this.globalOccurrences.clear();
        super.data = data;
    }

    /**
     * Returns all the recurring timespans.
     * @returns {Scheduler.model.TimeSpan[]} Array of recurring events.
     * @category Recurrence
     */
    getRecurringTimeSpans() {
        return [...this.recurringEvents];
    }
};
