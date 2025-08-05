import CopyPasteBase from './base/CopyPasteBase.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';

/**
 * @module Grid/feature/CellCopyPaste
 */

/**
 * Allows using `[Ctrl/CMD + C]`, `[Ctrl/CMD + X]` and `[Ctrl/CMD + V]` to cut, copy and paste cell or cell ranges. Also
 * makes cut, copy and paste actions available via the cell context menu.
 *
 * <div class="note">
 * Requires {@link Grid/view/Grid#config-selectionMode selectionMode.cell} to be activated. Also, if the
 * {@link Grid/feature/CellEdit} feature is disabled, the {@link #config-copyOnly} config will default to `true` which
 * prevents cut and paste actions completely. Set {@link #config-copyOnly} to `false` to prevent this behaviour.
 * </div>
 *
 * This feature will work alongside with {@link Grid/feature/RowCopyPaste} but there is differences on functionality.
 * * When used together, context menu options will be detailed so the user will know to copy the cell or the row.
 * * They will also detect what type of selection is present at the moment. If there are only rows selected, only row
 *   alternatives are shown in the context menu and the keyboard shortcuts will be processed by RowCopyPaste.
 * * If there are only cells selected, there will be context menu options for both row and cell but keyboard shortcuts
 *   will be handled by CellCopyPaste.
 * * They do share clipboard, even if internal clipboard is used, so it is not possible to have rows and cells copied or
 *   cut at the same time.
 *
 * If the {@link https://developer.mozilla.org/en-US/docs/Web/API/Clipboard_API Clipboard API} is available, that will
 * be used. This enables copying and pasting between different Bryntum products or completely different applications.
 * Please note that only string values are supported.
 *
 * This feature is **disabled** by default
 *
 * ```javascript
 * const grid = new Grid({
 *     features : {
 *         cellCopyPaste : true
 *     }
 * });
 * ```
 *
 * {@inlineexample Grid/feature/CellCopyPaste.js}
 *
 * ## Keyboard shortcuts
 * The feature has the following default keyboard shortcuts:
 *
 * | Keys       | Action  | Action description                                                                      |
 * |------------|---------|-----------------------------------------------------------------------------------------|
 * | `Ctrl`+`C` | *copy*  | Calls {@link #function-copy} which copies selected cell values into the clipboard.      |
 * | `Ctrl`+`X` | *cut*   | Calls {@link #function-cut} which cuts out selected cell values and saves in clipboard. |
 * | `Ctrl`+`V` | *paste* | Calls {@link #function-paste} which inserts string values from the clipboard.           |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [this guide](#Grid/guides/customization/keymap.md).
 *
 * @extends Grid/feature/base/CopyPasteBase
 * @classtype cellCopyPaste
 * @feature
 */
export default class CellCopyPaste extends CopyPasteBase {
    static $name = 'CellCopyPaste';

    static pluginConfig = {
        chain : [
            'populateCellMenu', 'afterSelectionModeChange'
        ]
    };

    static configurable = {

        useNativeClipboard : !VersionHelper.isTestEnv,

        copyText  : 'L{copy}',
        cutText   : 'L{cut}',
        pasteText : 'L{paste}'
    };

    afterConstruct() {
        super.afterConstruct();
        this.afterSelectionModeChange();
    }

    afterSelectionModeChange() {
        const me = this;

        if (!me.client.selectionMode.cell) {
            me.disabled = true;
        }
        else if (me._disabledBySelectionMode) {
            me.disabled = false;
            delete me._disabledBySelectionMode;
        }
    }

    // Used in events to separate events from different features from each other
    entityName = 'cell';

    set copyOnly(value) {
        this._copyOnly = value;
    }

    get copyOnly() {
        // If celledit is disabled, cut and paste actions are disabled by default
        if (this._copyOnly == null) {
            return !this.client.features.cellEdit?.enabled;
        }

        return Boolean(this._copyOnly);
    }

    get canCopy() {
        const { client } = this;

        return Boolean(!this.disabled && client.selectedCells.length &&
            (
                !client._selectedRows.length ||
                client.features.rowCopyPaste?.disabled ||
                client.focusedCell && client.isCellSelected(client.focusedCell)
            ));
    }

    get canCutPaste() {
        return this.canCopy && !this.copyOnly && !this.client.features.cellEdit?.isEditing && !this.client.readOnly;
    }

    // Called from keyMap. Also used internally here
    isActionAvailable({ actionName }) {
        return this.canCopy && (actionName === 'copy' || this.canCutPaste);
    }

    /**
     * Cuts selected cells to clipboard (native if accessible) to paste later
     * @async
     */
    async cut() {
        await this.copy(true);
    }

    /**
     * Copies selected cells to clipboard (native if accessible) to paste later
     * @async
     */
    async copy(isCut = false) {
        if (typeof isCut != 'boolean') {
            isCut = false; // If called by keymap, arguments[0] will be an event
        }

        const
            me                = this,
            { selectedCells } = me.client,
            cells             = isCut ? selectedCells.filter(r => !r.record?.readOnly) : selectedCells;

        if (cells) {
            if ((isCut ? !me.canCutPaste : !me.canCopy)) {
                return;
            }

            const copiedDataString = me.cellsToString(cells);
            await me.writeToClipboard(copiedDataString, isCut, { cells });

            if (isCut === true) {
                for (const cell of cells) {
                    if (!cell.column.readOnly) {
                        cell.record.set(cell.column.field, null);
                    }
                }
            }

            /**
             * Fires on the owning Grid after a copy action is performed.
             * @event copy
             * @on-owner
             * @param {Grid.view.Grid} source Owner grid
             * @param {Grid.util.Location[]} cells The cells about to be copied or cut
             * @param {String} copiedDataString The concatenated data string that was copied or cut
             * @param {Boolean} isCut `true` if this was a cut action
             * @param {String} entityName 'cell' to distinguish this event from other copy events
             */
            me.client.trigger('copy', { cells, copiedDataString, isCut, entityName : me.entityName });

        }
    }

    // Called from Clipboardable before writing to the clipboard
    async beforeCopy({ data, isCut, cells }) {
        /**
         * Fires on the owning Grid before a copy action is performed, return `false` to prevent the action
         * @event beforeCopy
         * @preventable
         * @on-owner
         * @async
         * @param {Grid.view.Grid} source Owner grid
         * @param {Grid.util.Location[]} cells The cells about to be copied or cut
         * @param {String} data The string data about to be copied or cut
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'cell' to distinguish this event from other beforeCopy events
         */
        return await this.client.trigger('beforeCopy', { cells, data, isCut, entityName : this.entityName });
    }

    /**
     * Pastes string data into a cell or a range of cells. Either from native clipboard if that is accessible or from a
     * fallback clipboard that is only available to the owner Grid.
     *
     * The string data will be split on `\n` and `\t` and put in different rows and columns accordingly.
     *
     * Note that there must be a selected cell to paste the data into.
     * @async
     */
    async paste() {
        const
            me                     = this,
            { client, entityName } = me,
            targetCell             = client.selectedCells[0];

        if (!me.canCutPaste || !targetCell) {
            return;
        }

        const clipboardData = await me.readFromClipboard({}, true);

        if (!clipboardData) {
            return;
        }

        const { modifiedRecords, targetCells } = me.setFromStringData(clipboardData);

        if (client.selectedCells.length === 1 && targetCells.length > 1) {
            client.selectCellRange(targetCells[0], targetCells[targetCells.length - 1]);
        }

        /**
         * Fires on the owning Grid after a paste action is performed.
         * @event paste
         * @on-owner
         * @param {Grid.view.Grid} source Owner grid
         * @param {String} clipboardData The clipboardData that was pasted
         * @param {Core.data.Model[]} modifiedRecords The records which have been modified due to the paste action
         * @param {Grid.util.Location} targetCell The cell from which the paste will be started
         * @param {String} entityName 'cell' to distinguish this event from other paste events
         */
        client.trigger('paste', { clipboardData, targetCell, modifiedRecords : [...modifiedRecords], entityName });
    }

    // Called from Clipboardable before finishing the clipboard read
    async beforePaste({ data }) {
        /**
         * Fires on the owning Grid before a paste action is performed, return `false` to prevent the action
         * @event beforePaste
         * @preventable
         * @on-owner
         * @async
         * @param {Grid.view.Grid} source Owner grid
         * @param {String} clipboardData The clipboardData about to be pasted
         * @param {Grid.util.Location} targetCell The cell from which the paste will be started
         * @param {String} entityName 'cell' to distinguish this event from other beforePaste events
         */
        return await this.client.trigger('beforePaste', {
            clipboardData : data, targetCell : this.client.selectedCell, entityName : this.entityName
        });
    }

    populateCellMenu({ record, items }) {
        const me = this;

        if (me.canCopy) {
            items.cutCell = {
                text        : me.cutText,
                localeClass : me,
                icon        : 'b-icon b-icon-cut',
                weight      : 115,
                disabled    : record.readOnly || !me.canCutPaste,
                onItem      : () => me.cut()
            };

            items.pasteCell = {
                text        : me.pasteText,
                localeClass : me,
                icon        : 'b-icon b-icon-paste',
                weight      : 120,
                disabled    : record.readOnly || !me.canCutPaste || me.hasClipboardData() === false,
                onItem      : () => me.paste()
            };

            items.copyCell = {
                text        : me.copyText,
                localeClass : me,
                cls         : 'b-separator',
                icon        : 'b-icon b-icon-copy',
                weight      : 110,
                onItem      : () => me.copy()
            };
        }
    }
}

GridFeatureManager.registerFeature(CellCopyPaste);
