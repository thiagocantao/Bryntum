import { RowsRange } from '../../../feature/export/Utils.js';
import LocalizableCombo from './LocalizableCombo.js';

export default class ExportRowsCombo extends LocalizableCombo {
    static get $name() {
        return 'ExportRowsCombo';
    }

    // Factoryable type name
    static get type() {
        return 'ExportRowsCombo';
    }

    static get defaultConfig() {
        return {
            editable : false
        };
    }

    buildLocalizedItems() {
        const me = this;

        return [
            { id : RowsRange.all, text : me.L('L{all}') },
            { id : RowsRange.visible, text : me.L('L{visible}') }
        ];
    }
}

// Register this widget type with its Factory
ExportRowsCombo.initClass();
