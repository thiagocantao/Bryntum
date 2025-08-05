import { Orientation } from '../../../feature/export/Utils.js';
import Combo from '../../../../Core/widget/Combo.js';

export default class ExportOrientationCombo extends Combo {

    //region Config

    static get $name() {
        return 'ExportOrientationCombo';
    }

    // Factoryable type name
    static get type() {
        return 'exportorientationcombo';
    }

    static get defaultConfig() {
        return {
            editable : false
        };
    }

    //endregion

    buildItems() {
        const me = this;

        return [
            { id : Orientation.portrait, text : me.L('L{portrait}') },
            { id : Orientation.landscape, text : me.L('L{landscape}') }
        ];
    }
}

// Register this widget type with its Factory
ExportOrientationCombo.initClass();
