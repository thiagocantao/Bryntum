import Combo from '../../Core/widget/Combo.js';
import { SchedulingMode } from '../../Engine/scheduling/Types.js';

/**
 * @module SchedulerPro/widget/SchedulingModePicker
 */

/**
 * Combo box preconfigured with possible scheduling mode values.
 *
 * This field can be used as an editor for the {@link Grid.column.Column Column}.
 * It is used as the default editor for the `SchedulingModeColumn`.
 *
 * {@inlineexample SchedulerPro/widget/SchedulingModePicker.js}
 * @extends Core/widget/Combo
 * @classType schedulingmodecombo
 * @inputfield
 */
export default class SchedulingModePicker extends Combo {

    static get $name() {
        return 'SchedulingModePicker';
    }

    // Factoryable type name
    static get type() {
        return 'schedulingmodecombo';
    }

    //region Config

    static get configurable() {
        return {
            /**
             * Specifies a list of allowed scheduling modes to be shown in the picker.
             * Supports either a string of comma separated values:
             *
             * ```javascript
             * new SchedulingModePicker ({
             *     allowedModes : 'FixedDuration,Normal'
             *     ...
             * })
             *
             * or an array of values:
             *
             * ```javascript
             * new SchedulingModePicker ({
             *     allowedModes : ['FixedDuration', 'Normal']
             *     ...
             * })
             * @config {String|Array}
             */
            allowedModes : null,
            store        : {
                data : this.defaultStoreData
            }
        };
    }

    static get defaultStoreData() {
        return [
            {
                id   : SchedulingMode.Normal,
                text : this.L('L{Normal}')
            },
            {
                id   : SchedulingMode.FixedDuration,
                text : this.L('L{Fixed Duration}')
            },
            {
                id   : SchedulingMode.FixedUnits,
                text : this.L('L{Fixed Units}')
            },
            {
                id   : SchedulingMode.FixedEffort,
                text : this.L('L{Fixed Effort}')
            }
        ];
    }

    //endregion

    //region Internal

    changeAllowedModes(value) {
        if (typeof value === 'string') {
            return value.split(',');
        }

        return value;
    }

    updateAllowedModes(value) {
        this._allowedModes = value;

        if (value) {
            this.store.addFilter({
                id       : 'allowed-mode-filter', 
                filterBy : (record) => this._allowedModes.includes(record.id)
            });
        }
        else {
            this.store.removeFilter('allowed-mode-filter');
        }
    }

    updateLocalization() {
        super.updateLocalization();
        // rebuild newly translated options
        this.store.data = this.constructor.defaultStoreData;
    }

    //endregion

}

// Register this widget type with its Factory
SchedulingModePicker.initClass();
