import Combo from '../../Core/widget/Combo.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import '../../Grid/view/TreeGrid.js';

/**
 * @module Grid/widget/TreeCombo
 */

/**
 * A powerful {@link Core/widget/Combo} box using a {@link Grid/view/TreeGrid} as its drop down widget. You can define
 * your own set of columns to display and use all the regular features of the Grid.
 *
 * {@inlineexample Grid/widget/TreeCombo.js}
 *
 * ```javascript
 * new TreeCombo({
 *     label    : 'Pick task(s)',
 *     width    : '30em',
 *     appendTo : document.body,
 *     picker   : {
 *         // Define the columns to show in the grid
 *         columns : [
 *             { type : 'tree', text : 'Tasks', field : 'name', flex : 1 },
 *             { text : 'Priority', field : 'prio' }
 *         ]
 *     },
 *     chipView : {
 *         // Render the chips in the combo field
 *         itemTpl(record) {
 *             return StringHelper.xss`${record.name}`;
 *         }
 *     },
 *     store : {
 *         fields     : [
 *             'prio'
 *         ],
 *         data : [
 *             {
 *                 name     : 'Development Tasks',
 *                 expanded : true,
 *                 children : [
 *                     { id : 1, name : 'Improve React docs', prio : 'High' },
 *                     { id : 2, name : 'Build Angular module', prio : 'Low' },
 *                     { id : 3, name : 'Creat Vue project', prio : 'Low' }
 *                 ]
 *             },
 *             { name : 'Customer meeting', prio : 'Normal' },
 *             {
 *                 name     : 'Customer Tasks',
 *                 expanded : true,
 *                 children : [
 *                     { id : 4, name : 'Intro meeting', prio : 'Normal' },
 *                     { id : 5, name : 'Build POC', prio : 'High' },
 *                     { id : 6, name : 'Documentation', prio : 'Low' }
 *                 ]
 *             }
 *         ]
 *     }
 * });
 * ```
 *
 * @extends Core/widget/Combo
 * @classtype treecombo
 * @inputfield
 */
export default class TreeCombo extends Combo {
    static $name = 'TreeCombo';

    static type = 'treecombo';

    static configurable = {
        multiSelect : true,
        picker      : {
            type                       : 'treegrid',
            minWidth                   : '35em',
            disableGridRowModelWarning : true,
            selectionMode              : {
                row                  : true,
                rowCheckboxSelection : true
            }
        },
        chipView : {
            itemTpl(record) {
                return StringHelper.xss`${record.name}`;
            },
            scrollable : {
                overflowX : 'hidden-scroll'
            }
        }
    };

    changePicker(picker, oldPicker) {
        picker = super.changePicker(picker, oldPicker);

        picker?.ion({
            selectionChange : 'onPickerSelectionChange',
            thisObj         : this
        });

        return picker;
    }

    updateMultiSelect(multiSelect) {
        super.updateMultiSelect(...arguments);

        this.picker.selectionMode.multiSelect = multiSelect;
    }

    updateReadOnly(readOnly) {
        super.updateReadOnly(...arguments);

        this.picker.readOnly = readOnly;
    }

    get value() {
        return super.value;
    }

    set value(value) {
        // indicate we are setting the field value
        this._settingValue = true;

        super.value = value;

        // select provided value enitres in the picker
        this.picker.selectedRecords = value.map?.(val => this.store.getById(val)) || [];

        this._settingValue = false;
    }

    onPickerSelectionChange({ selection }) {
        // apply selection to value (if we aren't in the middle of value setting)
        if (!this._settingValue) {
            this.value = selection;
        }
    }
}

TreeCombo.initClass();
