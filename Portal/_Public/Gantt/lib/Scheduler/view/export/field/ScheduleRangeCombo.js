import { ScheduleRange } from '../../../feature/export/Utils.js';
import Combo from '../../../../Core/widget/Combo.js';

export default class ScheduleRangeCombo extends Combo {



    static get $name() {
        return 'ScheduleRangeCombo';
    }

    // Factoryable type name
    static get type() {
        return 'schedulerangecombo';
    }

    static get defaultConfig() {
        return {
            editable              : false,
            localizeDisplayFields : true,
            displayField          : 'text',
            buildItems() {
                return Object.entries(ScheduleRange).map(([id, text]) => ({ value : id, text : 'L{' + text + '}' }));
            }
        };
    }
}

// Register this widget type with its Factory
ScheduleRangeCombo.initClass();
