import ColorPicker from '../../Core/widget/ColorPicker.js';
import SchedulerBase from '../view/SchedulerBase.js';
/**
 * @module Scheduler/widget/EventColorPicker
 */

/**
 * A color picker that displays a list of available event colors which the user can select by using mouse or keyboard.
 * See Schedulers {@link Scheduler.view.mixin.TimelineEventRendering#config-eventColor eventColor config} for default
 * available colors.
 *
 * {@inlineexample Scheduler/widget/EventColorPicker.js}
 *
 * ```javascript
 * new EventColorPicker({
 *    appendTo : 'container',
 *    width    : '10em',
 *    onColorSelected() {
 *        console.log(...arguments);
 *    }
 * });
 * ```
 *
 * @classType colorpicker
 * @extends Core/widget/ColorPicker
 */
export default class EventColorPicker extends ColorPicker {
    static $name = 'EventColorPicker';

    static type = 'eventcolorpicker';

    static configurable = {

        colorClasses : SchedulerBase.eventColors,

        colorClassPrefix : 'b-sch-',

        /**
         * @hideconfigs colors
         */

        colors : null,

        /**
         * Provide a {@link Scheduler.model.EventModel} instance to update it's
         * {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} field
         * @config {Scheduler.model.EventModel}
         */
        record : null
    };

    colorSelected({ color }) {
        if (this.record) {
            this.record.eventColor = color;
        }
    }
}

EventColorPicker.initClass();
