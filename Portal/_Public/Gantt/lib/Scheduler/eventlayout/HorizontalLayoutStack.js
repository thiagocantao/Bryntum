import HorizontalLayout from './HorizontalLayout.js';

/**
 * @module Scheduler/eventlayout/HorizontalLayoutStack
 */

/**
 * Handles layout of events within a row (resource) in horizontal mode. Stacks events, increasing row height when to fit
 * all overlapping events.
 *
 * This layout is used by default in horizontal mode.
 *
 * @extends Scheduler/eventlayout/HorizontalLayout
 * @private
 */
export default class HorizontalLayoutStack extends HorizontalLayout {
    static get $name() {
        return 'HorizontalLayoutStack';
    }

    static get configurable() {
        return {
            type : 'stack'
        };
    }

    // Input: Array of event layout data
    // heightRun is used when pre-calculating row heights, taking a cheaper path
    layoutEventsInBands(events, resource, heightRun = false) {
        let verticalPosition = 0;

        do {
            let eventIndex = 0,
                event      = events[0];

            while (event) {
                if (!heightRun) {
                    // Apply band height to the event cfg
                    event.top = this.bandIndexToPxConvertFn.call(
                        this.bandIndexToPxConvertThisObj || this,
                        verticalPosition,
                        event.eventRecord,
                        event.resourceRecord
                    );
                }

                // Remove it from the array and continue searching
                events.splice(eventIndex, 1);

                eventIndex = this.findClosestSuccessor(event, events);
                event = events[eventIndex];
            }

            verticalPosition++;
        } while (events.length > 0);

        // Done!
        return verticalPosition;
    }


    findClosestSuccessor(eventRenderData, events) {
        const
            { endMS, group } = eventRenderData,
            isMilestone      = eventRenderData.eventRecord && eventRenderData.eventRecord.duration === 0;

        let minGap      = Infinity,
            closest,
            gap,
            event;

        for (let i = 0, l = events.length; i < l; i++) {
            event = events[i];
            gap = event.startMS - endMS;

            if (
                gap >= 0 && gap < minGap &&
                // Two milestones should not overlap
                (gap > 0 || event.endMS - event.startMS > 0 || !isMilestone)
            ) {
                // Events are sorted by group, so when we find first event with a different group, we can stop iteration
                if (this.grouped && group !== event.group) {
                    break;
                }
                closest = i;
                minGap  = gap;
            }
        }

        return closest;
    }
}
