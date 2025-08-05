import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import SchedulerEventResize from '../../Scheduler/feature/EventResize.js';

/**
 * @module SchedulerPro/feature/EventResize
 */

/**
 * Feature that allows resizing an event by dragging its end.
 *
 * By default it displays a tooltip with the new start and end dates, formatted using
 * {@link Scheduler.view.mixin.TimelineViewPresets#config-displayDateFormat}.
 *
 * This feature is **enabled** by default
 *
 * @inlineexample SchedulerPro/feature/EventResize.js
 * @extends Scheduler/feature/EventResize
 * @classtype eventResize
 * @feature
 *
 * @typings Scheduler.feature.EventResize -> Scheduler.feature.SchedulerEventResize
 */
export default class EventResize extends SchedulerEventResize {
    static get $name() {
        return 'EventResize';
    }

    render() {
        const
            me         = this,
            { client } = me;

        super.render(...arguments);

        // Only active when in these items (ignore segments)
        me.dragSelector = me.dragItemSelector = client.eventSelector + ':not(.b-sch-event-segment)';
    }
}

GridFeatureManager.registerFeature(EventResize, true, 'SchedulerPro');
