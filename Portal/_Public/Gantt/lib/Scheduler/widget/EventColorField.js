import ColorField from '../../Core/widget/ColorField.js';
import './EventColorPicker.js';

/**
 * @module Scheduler/widget/EventColorField
 */

/**
 * Color field widget for editing the EventModel's {@link Scheduler.model.mixin.EventModelMixin#field-eventColor} field.
 * See Schedulers {@link Scheduler.view.mixin.TimelineEventRendering#config-eventColor eventColor config} for default
 * available colors.
 *
 * What differs this widget from {@link Core.widget.ColorField} is that this uses the
 * {@link Scheduler.widget.EventColorPicker} as its picker. And also that the {@link #config-name} config is set to
 * `eventColor` per default.
 *
 * {@inlineexample Scheduler/widget/EventColorField.js}
 *
 * This widget may be operated using the keyboard. `ArrowDown` opens the color picker, which itself is keyboard
 * navigable.
 *
 * ```javascript
 * let eventColorField = new EventColorField();
 * ```
 *
 * @extends Core/widget/ColorField
 * @classType eventcolorfield
 * @inputfield
 */

export default class EventColorField extends ColorField {
    static $name = 'EventColorField';

    static type = 'eventcolorfield';

    static configurable = {
        picker : {
            type : 'eventcolorpicker'
        },
        name : 'eventColor'
    };
}

EventColorField.initClass();
