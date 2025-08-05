import DurationField from '../../Core/widget/DurationField.js';

/**
 * @module SchedulerPro/widget/EffortField
 */

// NOTE: class is created mostly for localization reasons
//       effort field invalidText might differ from duration field one.

/**
 * A specialized field allowing a user to also specify duration unit when editing the effort value.
 *
 * {@inlineexample SchedulerPro/widget/EffortField.js}
 * @extends Core/widget/DurationField
 * @classType effort
 * @inputfield
 */
export default class EffortField extends DurationField {

    static $name = 'EffortField';

    static type = 'effort';

    static alias = 'effortfield';
}

// Register this widget type with its Factory
EffortField.initClass();
