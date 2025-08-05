import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import CopyPasteBase from './base/CopyPasteBase.js';
import Location from '../util/Location.js';

/**
 * @module Grid/feature/RowCopyPaste
 */

/**
 * Allow using [Ctrl/CMD + C/X] and [Ctrl/CMD + V] to copy/cut and paste rows. Also makes cut, copy and paste actions
 * available via the cell context menu.
 *
 * You can configure how a newly pasted record is named using {@link #function-generateNewName}
 *
 * This feature is **enabled** by default
 *
 * ```javascript
 * const grid = new Grid({
 *     features : {
 *         rowCopyPaste : true
 *     }
 * });
 * ```
 *
 * {@inlineexample Grid/feature/RowCopyPaste.js}
 *
 * This feature will work alongside with CellCopyPaste but there is differences on functionality.
 * * When used together, context menu options will be detailed so the user will know to copy the cell or the row.
 * * They will also detect what type of selection is present at the moment. If there is only rows selected, only row
 *   alternatives are shown in the context menu and the keyboard shortcuts will be processed by RowCopyPaste.
 * * If there is only cells selected, there will be context menu options for both row and cell but keyboard shortcuts
 *   will be handled by CellCopyPaste.
 * * They do share clipboard, even if internal clipboard is used, so it is not possible to have rows and cells copied or
 *   cut at the same time.
 *
 * ## Keyboard shortcuts
 * The feature has the following default keyboard shortcuts:
 *
 * | Keys       | Action  | Weight ¹ | Action description                                                                      |
 * |------------|---------|:--------:|-----------------------------------------------------------------------------------------|
 * | `Ctrl`+`C` | *copy*  | 10       | Calls {@link #function-copyRows} which copies selected row(s) into the clipboard.       |
 * | `Ctrl`+`X` | *cut*   | 10       | Calls {@link #function-copyRows} which cuts out selected row(s) and saves in clipboard. |
 * | `Ctrl`+`V` | *paste* | 10       | Calls {@link #function-pasteRows} which inserts copied or cut row(s) from the clipboard.|
 *
 * **¹** Customization of keyboard shortcuts that has a `weight` can affect other features that also uses that
 * particular keyboard shortcut. Read more in [our guide](#Grid/guides/customization/keymap.md).
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md).
 *
 * @extends Grid/feature/base/CopyPasteBase
 * @classtype rowCopyPaste
 * @feature
 */
export default class RowCopyPaste extends CopyPasteBase {

    static $name = 'RowCopyPaste';
    static type  = 'rowCopyPaste';

    static pluginConfig = {
        assign : [
            'copyRows',
            'pasteRows'
        ],
        chain : [
            'populateCellMenu'
        ]
    };

    static configurable = {
        /**
         * The field to use as the name field when updating the name of copied records
         * @config {String}
         * @default
         */
        nameField : 'name',

        keyMap : {
            // Weight to give CellCopyPaste priority
            'Ctrl+C' : { weight : 10, handler : 'copy' },
            'Ctrl+X' : { weight : 10, handler : 'cut' },
            'Ctrl+V' : { weight : 10, handler : 'paste' }
        },

        copyRecordText         : 'L{copyRecord}',
        cutRecordText          : 'L{cutRecord}',
        pasteRecordText        : 'L{pasteRecord}',
        rowSpecifierText       : 'L{row}',
        rowSpecifierTextPlural : 'L{rows}',
        localizableProperties  : [
            'copyRecordText',
            'cutRecordText',
            'pasteRecordText',
            'rowSpecifierText',
            'rowSpecifierTextPlural'
        ],

        /**
         * Adds `Cut (row)`, `Copy (row)` and `Paste (row)` options when opening a context menu on a selected cell when
         * {@link Grid.view.mixin.GridSelection#config-selectionMode cellSelection} and
         * {@link Grid.feature.CellCopyPaste} is active. Default behaviour will only provide row copy/paste actions on a
         * selected row.
         * @config {Boolean}
         * @default
         */
        rowOptionsOnCellContextMenu : false

    };

    construct(grid, config) {
        super.construct(grid, config);

        grid.rowManager.ion({
            beforeRenderRow : 'onBeforeRenderRow',
            thisObj         : this
        });

        this.grid = grid;
    }

    // Used in events to separate events from different features from each other
    entityName = 'row';

    onBeforeRenderRow({ row, record }) {
        row.cls['b-cut-row'] = this.isCut && this.cutData?.includes(record);
    }

    isActionAvailable({ key, action, event }) {
        const
            { grid }     = this,
            { cellEdit } = grid.features,
            { target }   = event;
        // No action if
        // 1. there is selected text on the page
        // 2. cell editing is active
        // 3. cursor is not in the grid (filter bar etc)
        return !this.disabled &&
            globalThis.getSelection().toString().length === 0 &&
            (!cellEdit || !cellEdit.isEditing) &&
            (action === 'copy' || !this.copyOnly) && // Do not allow cut or paste if copyOnly flag is set
            grid.selectedRecords?.length > 0 && // No key action when no selected records
            (!target || Boolean(target.closest('.b-gridbase:not(.b-schedulerbase) .b-grid-subgrid,.b-grid-subgrid:not(.b-timeaxissubgrid)')));
    }

    async copy() {
        await this.copyRows();
    }

    async cut() {
        await this.copyRows(true);
    }

    paste(referenceRecord) {
        return this.pasteRows(referenceRecord?.isModel ? referenceRecord : null);
    }

    /**
     * Copy or cut rows to clipboard to paste later
     *
     * @fires beforeCopy
     * @fires copy
     * @param {Boolean} [isCut] Copies by default, pass `true` to cut
     * @category Common
     * @on-owner
     * @async
     */
    async copyRows(isCut = false) {
        const
            { client, entityName } = this,
            // Don't cut readOnly records
            records                = this.selectedRecords.filter(r => !r.readOnly || !isCut);

        if (!records.length || client.readOnly) {
            return;
        }

        await this.writeToClipboard(records, isCut);

        /**
         * Fires on the owning Grid after a copy action is performed.
         * @event copy
         * @on-owner
         * @param {Grid.view.Grid} source Owner grid
         * @param {Core.data.Model[]} records The records that were copied
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'row' to distinguish this event from other copy events
         */
        client.trigger('copy', { records, isCut, entityName });
    }

    // Called from Clipboardable when cutData changes
    setIsCut(record, isCut) {
        this.grid.rowManager.getRowById(record)?.toggleCls('b-cut-row', isCut);
        record.meta.isCut = isCut;
    }

    // Called from Clipboardable when cutData changes
    handleCutData({ source }) {
        if (source !== this && this.cutData?.length) {
            this.grid.store.remove(this.cutData);
        }
    }

    /**
     * Called from Clipboardable after writing a non-string value to the clipboard
     * @param eventRecords
     * @returns {String}
     * @private
     */
    stringConverter(records) {
        return this.cellsToString(
            records.flatMap(r => this.grid.rowManager.getRowById(r)?.cells.map(c => new Location(c))));
    }

    // Called from Clipboardable before writing to the clipboard
    async beforeCopy({ data, isCut }) {
        /**
         * Fires on the owning Grid before a copy action is performed, return `false` to prevent the action
         * @event beforeCopy
         * @preventable
         * @on-owner
         * @async
         * @param {Grid.view.Grid} source Owner grid
         * @param {Core.data.Model[]} records The records about to be copied
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'row' to distinguish this event from other beforeCopy events
         */
        return await this.client.trigger('beforeCopy', { records : data, isCut, entityName : this.entityName });
    }

    /**
     * Paste rows below selected or passed record
     *
     * @fires beforePaste
     * @param {Core.data.Model} [record] Paste below this record, or currently selected record if left out
     * @category Common
     * @on-owner
     */
    async pasteRows(record) {
        const
            me                            = this,
            { client, isCut, entityName } = me,
            referenceRecord               = record || client.selectedRecord;

        if (client.readOnly || client.isTreeGrouped) {
            return [];
        }

        const
            records = await me.readFromClipboard({ referenceRecord }, true),
            isOwn   = me.clipboardData === records;

        if (!Array.isArray(records) || !records?.length ||
            (client.store.tree && isCut && records.some(rec => rec.contains(referenceRecord, true)))
        ) {
            return [];
        }

        // sort selected to move records to make sure it will be added in correct order independent of how it was selected.
        // Should be done with real records in the clipboard, after records are copied, all indexes will be changed
        me.sortByIndex(records);

        const
            idMap            = {},
            // We need to go over selected records, find all top level nodes and reassemble the tree
            recordsToProcess = me.extractParents(records, idMap, isOwn);

        await me.insertCopiedRecords(recordsToProcess, referenceRecord);

        if (client.isDestroying) {
            return;
        }

        if (isCut) {
            // reset clipboard
            await me.clearClipboard();
        }
        else {
            client.selectedRecords = recordsToProcess;
        }

        /**
         * Fires on the owning Grid after a paste action is performed.
         * @event paste
         * @on-owner
         * @param {Grid.view.Grid} source Owner grid
         * @param {Core.data.Model} referenceRecord The reference record, below which the records were pasted
         * @param {Core.data.Model[]} records Pasted records
         * @param {Core.data.Model[]} originalRecords For a copy action, these are the records that were copied.
         * For cut action, this is same as the `records` param.
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'row' to distinguish this event from other paste events
         */
        client.trigger('paste', {
            records         : recordsToProcess,
            originalRecords : records,
            referenceRecord,
            isCut,
            entityName
        });
        me.clipboard.triggerPaste(me);

        // Focus first cell of last copied or cut row
        client.getRowFor(recordsToProcess[recordsToProcess.length - 1])?.cells?.[0]?.focus();

        return recordsToProcess;
    }

    // Called from Clipboardable before finishing the internal clipboard read
    async beforePaste({ referenceRecord, data, text, isCut }) {
        const records = data !== text ? data : [];

        /**
         * Fires on the owning Grid before a paste action is performed, return `false` to prevent the action
         * @event beforePaste
         * @preventable
         * @on-owner
         * @async
         * @param {Grid.view.Grid} source Owner grid
         * @param {Core.data.Model} referenceRecord The reference record, the clipboard event records will
         * be pasted above this record
         * @param {Core.data.Model[]} records The records about to be pasted
         * @param {Boolean} isCut `true` if this is a cut action
         * @param {String} entityName 'row' to distinguish this event from other beforePaste events
         */
        return await this.client.trigger('beforePaste', {
            records, referenceRecord, isCut, entityName : this.entityName, data
        });
    }

    /**
     * Called from Clipboardable after reading from clipboard, and it is determined that the clipboard data is
     * "external"
     * @param json
     * @private
     */
    stringParser(clipboardData) {
        return this.setFromStringData(clipboardData, true).modifiedRecords;
    }

    /**
     * A method used to generate the name for a copy-pasted record. By defaults appends "- 2", "- 3" as a suffix. Override
     * it to provide your own naming of pasted records.
     *
     * @param {Core.data.Model} record The new record being pasted
     * @returns {String}
     */
    generateNewName(record) {
        const originalName = record.getValue(this.nameField);
        let counter = 2;

        while (this.client.store.findRecord(this.nameField, `${originalName} - ${counter}`)) {
            counter++;
        }

        return `${originalName} - ${counter}`;
    }

    insertCopiedRecords(toInsert, recordReference) {
        const
            { store } = this.client,
            insertAt  = store.indexOf(recordReference) + 1;

        if (store.tree) {
            return recordReference.parent.insertChild(toInsert, recordReference.nextSibling, false, {
                // Specify node to insert before in the ordered tree. It allows to paste to a
                // correct place both ordered and visual.
                // Covered by TaskOrderedWbs.t.js
                orderedBeforeNode : recordReference.nextOrderedSibling
            });
        }
        else {
            return store.insert(insertAt, toInsert);
        }
    }

    get selectedRecords() {
        const records = [...this.client.selectedRecords];

        // Add eventual selected cells records
        this.client.selectedCells.forEach(cell => {
            if (!records.includes(cell.record)) {
                records.push(cell.record);
            }
        });

        return records;
    }

    getMenuItemText(action, addRowSpecifier = false) {
        const me = this;
        let text = me[action + 'RecordText'];

        // If cellCopyPaste is enabled and there is selected cells, add a row specifier text to menu options
        if (addRowSpecifier) {
            text += ` (${me.selectedRecords.length > 1 ? me.rowSpecifierTextPlural : me.rowSpecifierText})`;
        }

        return text;
    }

    populateCellMenu({ record, items, cellSelector }) {
        const
            me           = this,
            {
                client,
                rowOptionsOnCellContextMenu
            }            = me,
            cellCopyPaste = client.features.cellCopyPaste?.enabled === true,
            // If cellCopyPaste is active and contextmenu originates from a selected cell
            targetIsCell = cellCopyPaste && client.isCellSelected(cellSelector);

        if (!client.readOnly &&
            !client.isTreeGrouped &&
            record?.isSpecialRow === false &&
            (cellCopyPaste ? client.selectedRows.length : client.selectedRecords.length) &&
            (!targetIsCell || me.rowOptionsOnCellContextMenu)
        ) {
            if (!me.copyOnly) {
                items.cut = {
                    text        : me.getMenuItemText('cut', targetIsCell && rowOptionsOnCellContextMenu),
                    localeClass : me,
                    icon        : 'b-icon b-icon-cut',
                    weight      : 135,
                    disabled    : record.readOnly,
                    onItem      : () => me.cut()
                };

                items.paste = {
                    text        : me.getMenuItemText('paste', targetIsCell && rowOptionsOnCellContextMenu),
                    localeClass : me,
                    icon        : 'b-icon b-icon-paste',
                    weight      : 140,
                    onItem      : () => me.paste(record),
                    disabled    : me.hasClipboardData() === false
                };
            }

            items.copy = {
                text        : me.getMenuItemText('copy', targetIsCell && rowOptionsOnCellContextMenu),
                localeClass : me,
                cls         : 'b-separator',
                icon        : 'b-icon b-icon-copy',
                weight      : 120,
                onItem      : () => me.copy()
            };

        }
    }

    /**
     * Sort array of records ASC by its indexes stored in indexPath
     * @param {Core.data.Model[]} array array to sort
     * @private
     */
    sortByIndex(array) {
        const { store } = this.client;

        return array.sort((rec1, rec2) => {
            const
                idx1 = rec1.indexPath,
                idx2 = rec2.indexPath;

            // When a record is copied without its parent, its index in the visible tree should be used
            if (!array.includes(rec1.parent) && !array.includes(rec2.parent)) {
                // For row copy-paste feature both records are normally in store. Unless someone wants
                // to include invisible records. Which does not happen yet.
                return store.indexOf(rec1) - store.indexOf(rec2);
            }

            if (idx1.length === idx2.length) {
                for (let i = 0; i < idx1.length; i++) {
                    if (idx1[i] < idx2[i]) {
                        return -1;
                    }
                    if (idx1[i] > idx2[i]) {
                        return 1;
                    }
                }
                return 0;
            }
            else {
                return idx1.length - idx2.length;
            }
        });
    }

    /**
     * Iterates over passed pre-sorted list of records and reassembles hierarchy of records.
     * @param {Core.data.Model[]} taskRecords array of records to extract parents from
     * @param {Object} idMap Empty object which will contain map linking original id with copied record
     * @returns {Core.data.Model[]} Returns array of new top-level nodes with children filled
     * @private
     */
    extractParents(taskRecords, idMap, generateNames = true) {
        const
            me        = this,
            { store } = me.client;

        // Unwrap children to pass them all through `generateNewName` function
        if (store.tree) {
            taskRecords.forEach(node => {
                node.traverse(n => {
                    const parents = n.getTopParent(true);
                    if (!taskRecords.includes(n) && (!me.isCut || !taskRecords.some(rec => parents.includes(rec)))) {
                        taskRecords.push(n);
                    }
                });
            });
        }

        const result = taskRecords.reduce((parents, node) => {
            let copy;
            // Fallback is for when the node was removed from the tree
            const parentId = node.parentId || node.meta.modified;

            if (me.isCut) {
                copy = node;

                // reset record cut state
                copy.meta.isCut = false;
            }
            else {
                copy               = node.copy();
                if (generateNames) {
                    copy[me.nameField] = me.generateNewName(copy);
                }

                // Ensure initial expanded state in new node matches state that the client's
                // store has for source node.
                copy.data.expanded = node.isExpanded(me.client.store);
            }

            idMap[node.id] = copy;

            // If we're copying top level node, add it directly
            if (node.parent === store.rootNode) {
                parents.push(copy);
            }
            // If node parent is also copied, append copy to the copied parent. Parents
            // are always at the beginning of the array, so we know if there is a parent
            // it was already copied
            else if (parentId in idMap) {
                idMap[parentId].appendChild(copy, true); // Silent to not cause redraws
            }
            // If parent is not copied and record is not top level, then append it as a
            // sibling.
            else {
                parents.push(copy);
            }
            return parents;
        }, []);

        // Now when tree is assembled we want to restore ordered tree. Traverse the tree, sort children
        // by previous value of `orderedParentIndex`
        result.forEach(parent => {
            parent.sortOrderedChildren(true, true);
        });

        return result;
    }

};

RowCopyPaste.featureClass = 'b-row-copypaste';

GridFeatureManager.registerFeature(RowCopyPaste, true, 'Grid');
GridFeatureManager.registerFeature(RowCopyPaste, false, 'Gantt');
GridFeatureManager.registerFeature(RowCopyPaste, false, 'SchedulerPro');
GridFeatureManager.registerFeature(RowCopyPaste, false, 'ResourceHistogram');
