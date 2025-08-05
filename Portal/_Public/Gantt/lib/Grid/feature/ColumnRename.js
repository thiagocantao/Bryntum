import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import Editor from '../../Core/widget/Editor.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';

/**
 * @module Grid/feature/ColumnRename
 */

/**
 * Allows user to rename columns by either right-clicking column header or using keyboard shortcuts when column header
 * is focused.
 *
 * To get notified about column renaming listen to `change` event on {@link Grid.data.ColumnStore columns} store.
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * {@inlineexample Grid/feature/ColumnRename.js}
 *
 * ## Keyboard shortcuts
 * This feature has the following default keyboard shortcuts:
 *
 * | Keys          | Action           | Action description                        |
 * |---------------|------------------|-------------------------------------------|
 * | `F2`          | *startEdit*      | Starts editing focused column header text |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md)
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/columns
 * @classtype columnRename
 * @feature
 */
export default class ColumnRename extends InstancePlugin {

    static $name = 'ColumnRename';

    static configurable = {
        /**
         * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
         * @config {Object<String,String>}
         */
        keyMap : {
            F2 : 'startEdit'
        }
    };

    doDestroy() {
        this.editor?.destroy();
        super.doDestroy();
    }

    static get pluginConfig() {
        return {
            after : ['populateHeaderMenu']
        };
    }

    populateHeaderMenu({ items, column }) {
        items.rename = {
            weight   : 215,
            icon     : 'b-fw-icon b-icon-edit',
            text     : this.L('L{rename}'),
            disabled : column.readOnly,
            onItem   : () => this.startEdit(column)
        };
    }

    startEdit(column) {
        if (column instanceof Event) {
            // If started editing by key
            column = this.client.getHeaderDataFromEvent(column)?.column;
        }

        if (column) {
            if (column.readOnly) {
                // return false to let keyMap know that we didn't handle this event
                return false;
            }

            const { textWrapper } = column;
            let { editor } = this;

            if (!editor) {
                this.editor = editor = new Editor({
                    owner : this.client,
                    align : {
                        align : 't0-t0'
                    }
                });
            }

            editor.render(textWrapper);

            editor.startEdit({
                target : textWrapper,
                record : column,
                field  : 'text'
            });
        }
    }
}

GridFeatureManager.registerFeature(ColumnRename, false);
