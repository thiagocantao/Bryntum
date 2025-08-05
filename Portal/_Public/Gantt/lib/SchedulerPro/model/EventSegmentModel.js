import { SchedulerProEventSegment } from '../../Engine/quark/model/scheduler_pro/SchedulerProEventSegment.js';
import TimeSpan from '../../Scheduler/model/TimeSpan.js';
import EventModelMixin from '../../Scheduler/model/mixin/EventModelMixin.js';
import PercentDoneMixin from './mixin/PercentDoneMixin.js';

/**
 * @module SchedulerPro/model/EventSegmentModel
 */

/**
 * This class represents an individual segment of a split event.
 *
 * @extends Scheduler/model/TimeSpan
 * @mixes Scheduler/model/mixin/EventModelMixin
 * @mixes SchedulerPro/model/mixin/PercentDoneMixin
 */
export default class EventSegmentModel extends SchedulerProEventSegment.derive(TimeSpan).mixin(
    EventModelMixin,
    PercentDoneMixin
) {
    static get $name() {
        return 'EventSegmentModel';
    }

    /**
     * Zero-based index of the segment.
     * @property {Number} segmentIndex
     */

    /**
     * The event this segment belongs to.
     * @member {SchedulerPro.model.EventModel} event
     * @readonly
     */

    /**
     * Alias for `event`, to better match naming in Gantt.
     * @member {SchedulerPro.model.EventModel} task
     * @readonly
     */
    get task() {
        return this.event;
    }
}
