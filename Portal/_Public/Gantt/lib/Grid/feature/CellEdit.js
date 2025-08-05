import DomHelper from '../../Core/helper/DomHelper.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../feature/GridFeatureManager.js';
import Delayable from '../../Core/mixin/Delayable.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Editor from '../../Core/widget/Editor.js';
import GlobalEvents from '../../Core/GlobalEvents.js';
import MessageDialog from '../../Core/widget/MessageDialog.js';
import Location from '../util/Location.js';
import '../../Core/widget/NumberField.js';
import '../../Core/widget/Combo.js';
import '../../Core/widget/DateField.js';
import '../../Core/widget/TimeField.js';



const editingActions = {
    finishAndEditNextRow  : 1,
    finishAndEditPrevRow  : 1,
    finishEditing         : 1,
    cancelEditing         : 1,
    finishAndEditNextCell : 1,
    finishAndEditPrevCell : 1
};

/**
 * @module Grid/feature/CellEdit
 */

/**
 * Adding this feature to the grid and other Bryntum products which are based on the Grid (i.e. Scheduler, SchedulerPro, and Gantt)
 * enables cell editing. Any subclass of {@link Core.widget.Field Field} can be used
 * as editor for the {@link Grid.column.Column Column}. The most popular are:
 *
 * - {@link Core.widget.TextField TextField}
 * - {@link Core.widget.NumberField NumberField}
 * - {@link Core.widget.DateField DateField}
 * - {@link Core.widget.TimeField TimeField}
 * - {@link Core.widget.Combo Combo}
 *
 * Usage instructions:
 * ## Start editing
 * * Double click on a cell
 * * Press [ENTER] or [F2] with a cell selected (see {@link #keyboard-shortcuts Keyboard shortcuts} below)
 * * It is also possible to change double click to single click to start editing, using the {@link #config-triggerEvent} config
 *
 * ```javascript
 * new Grid({
 *    features : {
 *        cellEdit : {
 *            triggerEvent : 'cellclick'
 *        }
 *    }
 * });
 * ```
 *
 * ## Instant update
 * If {@link Grid.column.Column#config-instantUpdate} on the column is set to true, record will be
 * updated instantly as value in the editor is changed. In combination with {@link Core.data.Store#config-autoCommit} it
 * could result in excessive requests to the backend.
 * By default instantUpdate is false, but it is enabled for some special columns, such as Duration column in Scheduler
 * Pro and all date columns in Gantt.
 *
 * ## Keyboard shortcuts
 * ### While not editing
 * | Keys    | Action         | Action description                    |
 * |---------|--------------- |---------------------------------------|
 * | `Enter` | *startEditing* | Starts editing currently focused cell |
 * | `F2`    | *startEditing* | Starts editing currently focused cell |
 *
 * ### While editing
 * | Keys            | Action                  | Weight | Action description                                                                         |
 * |-----------------|-------------------------|:------:|--------------------------------------------------------------------------------------------|
 * | `Enter`         | *finishAndEditNextRow*  |        | Finish editing and start editing the same cell in next row                                 |
 * | `Shift`+`Enter` | *finishAndEditPrevRow*  |        | Finish editing and start editing the same cell in previous row                             |
 * | `F2`            | *finishEditing*         |        | Finish editing                                                                             |
 * | `Ctrl`+`Enter`  | *finishAllSelected*     |        | If {@link #config-multiEdit} is active, this applies new value on all selected rows/cells  |
 * | `Ctrl`+`Enter`  | *finishEditing*         |        | Finish editing                                                                             |
 * | `Escape`        | *cancelEditing*         |        | By default, first reverts the value back to its original value, next press cancels editing |
 * | `Tab`           | *finishAndEditNextCell* | 100    | Finish editing and start editing the next cell                                             |
 * | `Shift`+`Tab`   | *finishAndEditPrevCell* | 100    | Finish editing and start editing the previous cell                                         |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md).
 *
 * ## Editor configuration
 * Columns specify editor in their configuration. Editor can also by set by using a column type. Columns
 * may also contain these three configurations which affect how their cells are edited:
 * * {@link Grid.column.Column#config-invalidAction}
 * * {@link Grid.column.Column#config-revertOnEscape}
 * * {@link Grid.column.Column#config-finalizeCellEdit}
 *
 * ## Preventing editing of certain cells
 * You can prevent editing on a column by setting `editor` to false:
 *
 * ```javascript
 * new Grid({
 *    columns : [
 *       {
 *          type   : 'number',
 *          text   : 'Age',
 *          field  : 'age',
 *          editor : false
 *       }
 *    ]
 * });
 * ```
 *
 * To prevent editing in a specific cell, listen to the {@link #event-beforeCellEditStart} and return false:
 *
 * ```javascript
 * grid.on('beforeCellEditStart', ({ editorContext }) => {
 *     return editorContext.column.field !== 'id';
 * });
 * ```
 *
 * ## Choosing field on the fly
 * To use an alternative input field to edit a cell, listen to the {@link #event-beforeCellEditStart} and
 * set the `editor` property of the context to the input field you want to use:
 *
 * ```javascript
 * grid.on('beforeCellEditStart', ({ editorContext }) => {
 *     return editorContext.editor = myDateField;
 * });
 * ```
 *
 * ## Loading remote data into a combo box cell editor
 * If you need to prepare or modify the data shown by the cell editor, e.g. load remote data into the store used by a combo,
 * listen to the {@link #event-startCellEdit} event:
 * ```javascript
 * const employeeStore = new AjaxStore({ readUrl : '/cities' }); // A server endpoint returning data like:
 *                                                               // [{ id : 123, name : 'Bob Mc Bob' }, { id : 345, name : 'Lind Mc Foo' }]
 *
 * new Grid({
 *     // Example data including a city field which is an id used to look up entries in the cityStore above
 *     data : [
 *         { id : 1, name : 'Task 1', employeeId : 123 },
 *         { id : 2, name : 'Task 2', employeeId : 345 }
 *     ],
 *     columns : [
 *       {
 *          text   : 'Task',
 *          field  : 'name'
 *       },
 *       {
 *          text   : 'Assigned to',
 *          field  : 'employeeId',
 *          editor : {
 *               type : 'combo',
 *               store : employeeStore,
 *               // specify valueField'/'displayField' to match the data format in the employeeStore store
 *               valueField : 'id',
 *               displayField : 'name'
 *           },
 *           renderer : ({ value }) {
 *                // Use a renderer to show the employee name, which we find by querying employeeStore by the id of the grid record
 *                return employeeStore.getById(value)?.name;
 *           }
 *       }
 *    ],
 *    listeners : {
 *        // When editing, you might want to fetch data for the combo store from a remote resource
 *        startCellEdit({ editorContext }) {
 *            const { record, editor, column } = editorContext;
 *            if (column.field === 'employeeId') {
 *                // Load possible employees to assign to this particular task
 *                editor.inputField.store.load({ task : record.id });
 *            }
 *       }
 *    }
 * });
 * ```
 *
 * ## Editing on touch devices
 *
 * On touch devices, a single tap navigates and tapping an already selected cell after a short delay starts the editing.
 *
 * This feature is **enabled** by default.
 *
 * ## Validation
 *
 * To validate the cell editing process you can use the {@link Grid.column.Column#config-finalizeCellEdit} config.
 * Please refer to its documentation for details.
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @demo Grid/celledit
 * @classtype cellEdit
 * @inlineexample Grid/feature/CellEdit.js
 * @feature
 */

export default class CellEdit extends Delayable(InstancePlugin) {
    //region Config

    static $name = 'CellEdit';

    // Default configuration
    static get defaultConfig() {
        return {
            /**
             * Set to true to select the field text when editing starts
             * @config {Boolean}
             * @default
             */
            autoSelect : true,

            /**
             * What action should be taken when focus moves leaves the cell editor, for example when clicking outside.
             * May be `'complete'` or `'cancel`'.
             * @config {'complete'|'cancel'}
             * @default
             */
            blurAction : 'complete',

            /**
             * Set to `false` to stop editing when clicking another cell after a cell edit.
             * @config {Boolean}
             * @default
             */
            continueEditingOnCellClick : true,

            /**
             * Set to true to have TAB key on the last cell (and ENTER anywhere in the last row) in the data set create
             * a new record and begin editing it at its first editable cell.
             *
             * If a customized {@link #config-keyMap} is used, this setting will affect the customized keys instead of
             * ENTER and TAB.
             *
             * If this is configured as an object, it is used as the default data value set for each new record.
             * @config {Boolean|Object}
             */
            addNewAtEnd : null,

            /**
             * Set to `true` to add record to the parent of the last record, when configured with {@link #config-addNewAtEnd}.
             * Only applicable when using a tree view and store.
             *
             * By default, it adds records to the root.
             * @config {Boolean}
             * @default false
             */
            addToCurrentParent : false,

            /**
             * Set to `true` to start editing when user starts typing text on a focused cell (as in Excel)
             * @config {Boolean}
             * @default false
             */
            autoEdit : null,

            /**
             * Set to `false` to not start editing next record when user presses enter inside a cell editor (or previous
             * record if SHIFT key is pressed). This is set to `false` when {@link #config-autoEdit} is `true`. Please
             * note that these key combinations could be different if a customized {@link #config-keyMap} is used.
             * @config {Boolean}
             * @default
             */
            editNextOnEnterPress : true,

            /**
             * Class to use as an editor. Default value: {@link Core.widget.Editor}
             * @config {Core.widget.Widget}
             * @typings {typeof Widget}
             * @internal
             */
            editorClass : Editor,

            /**
             * The name of the grid event that will trigger cell editing. Defaults to
             * {@link Grid.view.mixin.GridElementEvents#event-cellDblClick celldblclick} but can be changed to any other event,
             * such as {@link Grid.view.mixin.GridElementEvents#event-cellClick cellclick}.
             *
             * ```javascript
             * features : {
             *     cellEdit : {
             *         triggerEvent : 'cellclick'
             *     }
             * }
             * ```
             *
             * @config {String}
             * @default
             */
            triggerEvent : 'celldblclick',

            // To edit a cell using a touch gesture, at least 300ms should have passed since last cell tap
            touchEditDelay : 300,

            focusCellAnimationDuration : false,

            /**
             * If set to `true` (which is default) this will make it possible to edit current column in multiple rows
             * simultaneously.
             *
             * This is achieved by:
             * 1. Select multiple rows or row's cells
             * 2. Start editing simultaneously as selecting the last row or cell
             * 3. When finished editing, press Ctrl+Enter to apply the new value to all selected rows.
             *
             * If a customized {@link #config-keyMap} is used, the Ctrl+Enter combination could map to something else.
             *
             * @config {Boolean}
             * @default
             */
            multiEdit : true,

            /**
             * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
             * @config {Object<String,String>}
             */
            keyMap : {
                Enter         : ['startEditingFromKeyMap', 'finishAndEditNextRow'],
                'Ctrl+Enter'  : ['finishAllSelected', 'finishEditing'],
                'Shift+Enter' : 'finishAndEditPrevRow',
                'Alt+Enter'   : 'finishEditing',
                F2            : ['startEditingFromKeyMap', 'finishEditing'],
                Escape        : 'cancelEditing',
                Tab           : { handler : 'finishAndEditNextCell', weight : 100 },
                'Shift+Tab'   : { handler : 'finishAndEditPrevCell', weight : 100 }
            },

            /**
             * A CSS selector for elements that when clicked, should not trigger editing. Useful if you render actionable
             * icons or buttons into a grid cell.
             * @config {String}
             * @default
             */
            ignoreCSSSelector : 'button,.b-icon,.b-fa,svg'
        };
    }

    // Plugin configuration. This plugin chains some functions in Grid.
    static get pluginConfig() {
        return {
            assign : ['startEditing', 'finishEditing', 'cancelEditing'],
            before : ['onElementKeyDown', 'onElementPointerUp'],
            chain  : ['onElementClick', 'bindStore']
        };
    }

    //endregion

    //region Init

    construct(grid, config) {
        super.construct(grid, config);

        const
            me            = this,
            gridListeners = {
                renderRows : 'onGridRefreshed',
                cellClick  : 'onCellClick',
                thisObj    : me
            };

        me.grid = grid;

        if (me.triggerEvent !== 'cellclick') {
            gridListeners[me.triggerEvent] = 'onTriggerEditEvent';
        }

        if (me.autoEdit && !('editNextOnEnterPress' in config)) {
            me.editNextOnEnterPress = false;
        }

        grid.ion(gridListeners);

        grid.rowManager.ion({
            changeTotalHeight : 'onGridRefreshed',
            thisObj           : me
        });
        me.bindStore(grid.store);
    }

    bindStore(store) {
        this.detachListeners('store');

        store.ion({
            name       : 'store',
            update     : 'onStoreUpdate',
            beforeSort : 'onStoreBeforeSort',
            thisObj    : this
        });
    }

    /**
     * Displays an OK / Cancel confirmation dialog box owned by the current Editor. This is intended to be
     * used by {@link Grid.column.Column#config-finalizeCellEdit} implementations. The returned promise resolves passing
     * `true` if the "OK" button is pressed, and `false` if the "Cancel" button is pressed. Typing `ESC` rejects.
     * @param {Object} options An options object for what to show.
     * @param {String} [options.title] The title to show in the dialog header.
     * @param {String} [options.message] The message to show in the dialog body.
     * @param {String|Object} [options.cancelButton] A text or a config object to apply to the Cancel button.
     * @param {String|Object} [options.okButton] A text or config object to apply to the OK button.
     */
    async confirm(options) {
        let result = true;

        if (this.editorContext) {
            // The input field must not lose containment of focus during this confirmation
            // so temporarily make the MessageDialog a descendant widget.
            MessageDialog.owner = this.editorContext.editor.inputField;
            options.rootElement = this.grid.rootElement;
            result = await MessageDialog.confirm(options);
            MessageDialog.owner = null;
        }

        return result === MessageDialog.yesButton;
    }

    doDestroy() {
        // To kill timeouts
        this.grid.columns.allRecords.forEach(column => {
            column._cellEditor?.destroy();
        });

        super.doDestroy();
    }

    doDisable(disable) {
        if (disable && !this.isConfiguring) {
            this.cancelEditing(true);
        }

        super.doDisable(disable);
    }

    set disabled(disabled) {
        super.disabled = disabled;
    }

    get disabled() {
        const { grid } = this;

        return Boolean(super.disabled || grid.disabled || grid.readOnly);
    }

    //endregion

    //region Editing

    /**
     * Is any cell currently being edited?
     * @readonly
     * @property {Boolean}
     */
    get isEditing() {
        return Boolean(this.editorContext);
    }

    /**
     * Returns the record currently being edited, or `null`
     * @readonly
     * @property {Core.data.Model}
     */
    get activeRecord() {
        return this.editorContext?.record || null;
    }

    /**
     * Internal function to create or get existing editor for specified cell.
     * @private
     * @param cellContext Cell to get or create editor for
     * @returns {Core.widget.Editor} An Editor container which displays the input field.
     * @category Internal
     */
    getEditorForCell({ id, cell, column, columnId, editor }) {
        const
            me = this,
            {
                grid,
                editorClass
            }  = me;

        // Reuse the Editor by caching it on the column.
        let cellEditor = column.cellEditor,
            leftOffset = 0; // Only applicable for tree cells to show editor right of the icons etc

        // Help Editor match size and position
        if (column.editTargetSelector) {
            const editorTarget = cell.querySelector(column.editTargetSelector);

            leftOffset = editorTarget.offsetLeft;
        }

        editor.autoSelect = me.autoSelect;

        // Still a config
        if (!cellEditor?.isEditor) {
            cellEditor = column.data.cellEditor = editorClass.create(editorClass.mergeConfigs({
                type          : editorClass.type,
                constrainTo   : null,
                cls           : 'b-cell-editor',
                inputField    : editor,
                blurAction    : 'none',
                invalidAction : column.invalidAction,
                completeKey   : false,
                cancelKey     : false,
                owner         : grid,
                align         : {
                    align  : 't0-t0',
                    offset : [leftOffset, 0]
                },
                internalListeners : me.getEditorListeners(),

                // Listen for cell edit control keys from the Editor
                onInternalKeyDown : me.onEditorKeydown.bind(me),

                // React editor wrapper code uses this flag to enable mouse events pass through to editor
                allowMouseEvents : editor.allowMouseEvents
            }, cellEditor));
        }

        // If matchSize auto heights it, ensure it at least covers the cell.
        cellEditor.minHeight = grid.rowHeight;

        // If the input field needs changing, change it.
        if (cellEditor.inputField !== editor) {
            cellEditor.remove(cellEditor.items[0]);
            cellEditor.add(editor);
        }

        // Ensure the X offset is set to clear TreeCell furniture
        cellEditor.align.offset[0] = leftOffset;

        // Keep the record synced with the value
        if (column.instantUpdate && !editor.cellEditValueSetter) {
            ObjectHelper.wrapProperty(editor, 'value', null, value => {
                const
                    { editorContext } = me,
                    inputField = editorContext?.editor.inputField;
                // Only tickle the record if the value has changed.
                if (
                    editorContext?.editor.isValid &&
                    !ObjectHelper.isEqual(editorContext.record.getValue(editorContext.column.field), value) &&
                    // If editor is a dateField, only allow picker input as not to trigger change on each keystroke.
                    (!inputField?.isDateField || inputField._isPickerInput)
                ) {
                    editorContext.record.setValue(editorContext.column.field, value);
                }
            });
            editor.cellEditValueSetter = true;
        }

        Object.assign(cellEditor.element.dataset, {
            rowId : id,
            columnId,
            field : column.field
        });

        // First ESC press reverts
        cellEditor.inputField.revertOnEscape = column.revertOnEscape;

        return me.editor = cellEditor;
    }

    // Turned into function to allow overriding in Gantt, and make more configurable in general
    getEditorListeners() {
        return {
            focusOut       : 'onEditorFocusOut',
            focusIn        : 'onEditorFocusIn',
            start          : 'onEditorStart',
            beforeComplete : 'onEditorBeforeComplete',
            complete       : 'onEditorComplete',
            beforeCancel   : 'onEditorBeforeCancel',
            cancel         : 'onEditorCancel',
            beforeHide     : 'onBeforeEditorHide',
            finishEdit     : 'onEditorFinishEdit',
            thisObj        : this
        };
    }

    onEditorStart({ source : editor }) {
        const
            me            = this,
            editorContext = me.editorContext = editor.cellEditorContext;

        if (editorContext) {
            const { grid } = me;

            // Should move editing to new cell on click, unless click is configured to start editing - in which case it
            // will move anyway
            if (me.triggerEvent !== 'cellclick') {
                me.detachListeners('cellClickWhileEditing');
                grid.ion({
                    name      : 'cellClickWhileEditing',
                    cellclick : 'onCellClickWhileEditing',
                    thisObj   : me
                });
            }

            me.removeEditingListeners?.();

            // Handle tapping outside the grid element. Use GlobalEvents
            // because it uses a capture:true listener before any other handlers
            // might stop propagation.
            // Cannot use delegate here. A tapped cell will match :not(#body-container)
            me.removeEditingListeners = GlobalEvents.addListener({
                globaltap : 'onTapOut',
                thisObj   : me
            });

            /**
             * Fires on the owning Grid when editing starts
             * @event startCellEdit
             * @on-owner
             * @param {Grid.view.Grid} source Owner grid
             * @param {Grid.util.Location} editorContext Editing context
             * @param {Core.widget.Editor} editorContext.editor The Editor being used.
             * Will contain an `inputField` property which is the field being used to perform the editing.
             * @param {Grid.column.Column} editorContext.column Target column
             * @param {Core.data.Model} editorContext.record Target record
             * @param {HTMLElement} editorContext.cell Target cell
             * @param {*} editorContext.value Cell value
             */
            grid.trigger('startCellEdit', { grid, editorContext });
        }
    }

    onEditorBeforeComplete(context) {
        const
            { grid }      = this,
            editor        = context.source,
            editorContext = editor.cellEditorContext;

        context.grid = grid;
        context.editorContext = editorContext;

        /**
         * Fires on the owning Grid before the cell editing is finished, return false to signal that the value is invalid and editing should not be finalized.
         * @on-owner
         * @event beforeFinishCellEdit
         * @param {Grid.view.Grid} grid Target grid
         * @param {Grid.util.Location} editorContext Editing context
         * @param {Core.widget.Editor} editorContext.editor The Editor being used.
         * Will contain an `inputField` property which is the field being used to perform the editing.
         * @param {Grid.column.Column} editorContext.column Target column
         * @param {Core.data.Model} editorContext.record Target record
         * @param {HTMLElement} editorContext.cell Target cell
         * @param {*} editorContext.value Cell value
         */
        return grid.trigger('beforeFinishCellEdit', context);
    }

    onEditorComplete({ source : editor }) {
        const
            { grid }      = this,
            editorContext = editor.cellEditorContext;

        // Ensure the docs below are accurate!
        editorContext.value = editor.inputField.value;

        /**
         * Fires on the owning Grid when cell editing is finished
         * @event finishCellEdit
         * @on-owner
         * @param {Grid.view.Grid} grid Target grid
         * @param {Grid.util.Location} editorContext Editing context
         * @param {Core.widget.Editor} editorContext.editor The Editor being used.
         * Will contain an `inputField` property which is the field being used to perform the editing.
         * @param {Grid.column.Column} editorContext.column Target column
         * @param {Core.data.Model} editorContext.record Target record
         * @param {HTMLElement} editorContext.cell Target cell
         * @param {*} editorContext.value Cell value
         */
        grid.trigger('finishCellEdit', { grid, editorContext });
    }

    onEditorBeforeCancel() {
        const { editorContext } = this;

        /**
         * Fires on the owning Grid before the cell editing is canceled, return `false` to prevent cancellation.
         * @event beforeCancelCellEdit
         * @preventable
         * @on-owner
         * @param {Grid.view.Grid} source Owner grid
         * @param {Grid.util.Location} editorContext Editing context
         */
        return this.grid.trigger('beforeCancelCellEdit', { editorContext });
    }

    onEditorCancel({ event }) {
        const { editorContext, muteEvents, grid } = this;

        if (!muteEvents) {
            /**
             * Fires on the owning Grid when editing is cancelled
             * @event cancelCellEdit
             * @on-owner
             * @param {Grid.view.Grid} source Owner grid
             * @param {Grid.util.Location} editorContext Editing context
             * @param {Event} event Included if the cancellation was triggered by a DOM event
             */
            grid.trigger('cancelCellEdit', { grid, editorContext, event });
        }
    }

    onBeforeEditorHide({ source }) {
        const
            me = this,
            {
                row,
                cell
            }  = source.cellEditorContext;

        // Clean up and restore cell to full visibility
        // before we hide and attempt to revert focus to the cell.
        cell?.classList.remove('b-editing');
        row?.removeCls('b-editing');
        me.detachListeners('cellClickWhileEditing');
        me.removeEditingListeners();
    }

    onEditorFinishEdit({ source }) {
        // Clean up context objects so we know we are not editing
        source.cellEditorContext = this.editorContext = null;
    }

    /**
     * Find the next succeeding or preceding cell which is editable (column.editor != false)
     * @param {Object} cellInfo
     * @param {Boolean} isForward
     * @returns {Object}
     * @private
     * @category Internal
     */
    getAdjacentEditableCell(cellInfo, isForward) {
        const
            { grid }           = this,
            { store, columns } = grid,
            { visibleColumns } = columns;

        let
            rowId    = cellInfo.id,
            column   = columns.getAdjacentVisibleLeafColumn(cellInfo.columnId, isForward);

        while (rowId) {
            if (column) {
                if (column.editor && column.canEdit(store.getById(rowId))) {
                    return { id : rowId, columnId : column.id };
                }

                column = columns.getAdjacentVisibleLeafColumn(column, isForward);
            }
            else {
                const record = store.getAdjacent(cellInfo.id, isForward, false, true);

                rowId = record?.id;

                if (record) {
                    column = isForward ? visibleColumns[0] : visibleColumns[visibleColumns.length - 1];
                }
            }
        }

        return null;
    }

    /**
     * Adds a new, empty record at the end of the TaskStore with the initial
     * data specified by the {@link Grid.feature.CellEdit#config-addNewAtEnd} setting.
     *
     * @private
     * @returns {Core.data.Model} Newly added record
     */
    doAddNewAtEnd() {
        const
            newRecordConfig              = typeof this.addNewAtEnd === 'object' ? ObjectHelper.clone(this.addNewAtEnd) : {},
            { grid : { store, rowManager }, addToCurrentParent } = this;

        let record;
        if (store.tree && addToCurrentParent) {
            record = store.last.parent.appendChild(newRecordConfig);
        }
        else {
            record = store.add(newRecordConfig)[0];
        }

        // If the new record was not added due to it being off the end of the rendered block
        // ensure we force it to be there before we attempt to edit it.
        if (!rowManager.getRowFor(record)) {
            rowManager.displayRecordAtBottom();
        }

        return record;
    }

    /**
     * Creates an editing context object for the passed cell context (target cell must be in the DOM).
     *
     * If the referenced cell is editable, a {@link Grid.util.Location} will
     * be returned containing the following extra properties:
     *
     *     - editor
     *     - value
     *
     * If the referenced cell is _not_ editable, `false` will be returned.
     * @param {Object} cellContext an object which encapsulates a cell.
     * @param {String} cellContext.id The record id of the row to edit
     * @param {String} cellContext.columnId The column id of the column to edit
     * @returns {Grid.util.Location}
     * @private
     */
    getEditingContext(cellContext) {
        cellContext = this.grid.normalizeCellContext(cellContext);

        const { column, record } = cellContext;

        // Cell must be in the DOM to edit.
        // Cannot edit hidden columns and columns without an editor.
        // Cannot edit special rows (groups etc).
        if (column?.isVisible && column.editor && !column.readOnly && record && !record.isSpecialRow && !record.readOnly && column.canEdit(record)) {
            // If the field name is a complex mapping (instead of using a field name with a dataSource)
            // set it correctly. Row#renderCell gets its contentValue in this way.
            const value = record ? column.getRawValue(record) : record;

            Object.assign(cellContext, {
                value  : value === undefined ? null : value,
                editor : column.editor
            });
            return cellContext;
        }
        else {
            return false;
        }
    }

    startEditingFromKeyMap() {
        return this.startEditing(this.grid.focusedCell);
    }

    /**
     * Start editing specified cell. If no cellContext is given it starts with the first cell in the first row.
     * This function is exposed on Grid and can thus be called as `grid.startEditing(...)`
     * @param {Object} cellContext Cell specified in format { id: 'x', columnId/column/field: 'xxx' }. See
     * {@link Grid.view.Grid#function-getCell} for details.
     * @fires startCellEdit
     * @returns {Promise} Resolved promise returns`true` if editing has been started, `false` if an {@link Core.widget.Editor#event-beforeStart} listener
     * has vetoed the edit.
     * @category Editing
     * @on-owner
     */
    async startEditing(cellContext = {}) {
        const me = this;

        // If disabled no can do.
        if (!me.disabled) {
            const { grid } = me;

            // If we got here from keyMap, start editing currently focused cell instead
            if (cellContext?.fromKeyMap) {
                cellContext = me.grid.focusedCell;
            }

            // When cell context is not available add the first cell context
            if (ObjectHelper.isEmpty(cellContext)) {
                cellContext.id = grid.firstVisibleRow.id;
            }

            // Has to expand before normalizing to a Location, since Location only maps to visible rows
            if (grid.store.isTree && grid.features.tree) {
                const record = cellContext.id ? grid.store.getById(cellContext.id) : cellContext.record ?? grid.store.getAt(cellContext.row);

                if (record) {
                    await grid.expandTo(record);
                }
                else {
                    return false;
                }
            }

            const editorContext = me.getEditingContext(cellContext);

            // Cannot edit hidden columns and columns without an editor
            // Cannot edit special rows (groups etc).
            if (!editorContext) {
                return false;
            }

            if (me.editorContext) {
                me.cancelEditing();
            }

            // Now that we know we can edit this cell, scroll the record into view and register it as last focusedCell
            // While any potential scroll may be async, the desired cell will be rendered immediately.
            if (!grid.focusedCell?.equals(editorContext)) {
                grid.focusCell(editorContext);
            }

            /**
             * Fires on the owning Grid before editing starts, return `false` to prevent editing
             * @event beforeCellEditStart
             * @on-owner
             * @preventable
             * @param {Grid.view.Grid} source Owner grid
             * @param {Grid.util.Location} editorContext Editing context
             * @param {Grid.column.Column} editorContext.column Target column
             * @param {Core.data.Model} editorContext.record Target record
             * @param {HTMLElement} editorContext.cell Target cell
             * @param {Core.widget.Field} editorContext.editor The input field that the column is configured
             * with (see {@link Grid.column.Column#config-field}). This property mey be replaced
             * to be a different {@link Core.widget.Field field} in the handler, to take effect
             * just for the impending edit.
             * @param {Function} [editorContext.finalize] An async function may be injected into this property
             * which performs asynchronous finalization tasks such as complex validation of confirmation. The
             * value `true` or `false` must be returned.
             * @param {Object} [editorContext.finalize.context] An object describing the editing context upon requested
             * completion of the edit.
             * @param {*} editorContext.value Cell value
             */
            if (grid.trigger('beforeCellEditStart', { grid, editorContext }) === false) {
                return false;
            }

            const
                editor = editorContext.editor = me.getEditorForCell(editorContext),
                {
                    row,
                    cell,
                    record
                }      = editorContext;

            // Prevent highlight when setting the value in the editor
            editor.inputField.highlightExternalChange = false;

            editor.cellEditorContext = editorContext;
            editor.render(cell);

            // CSS state must be set before the startEdit causes the Editor to align itself
            // because if its target is overflow:hidden, it automatically constrains its size.
            cell.classList.add('b-editing');
            row.addCls('b-editing');

            // Attempt to start edit.
            // We will set up our context in onEditorStart *if* the start was successful.
            if (!(await editor.startEdit({
                target : cell,
                field  : editor.inputField.name || editorContext.column.field,
                value  : editorContext.value,
                record
            }))) {
                // If the editor was vetoed, undo the CSS state.
                cell.classList.remove('b-editing');
                row.removeCls('b-editing');
            }

            me.onCellEditStart?.();

            return true;
        }

        return false;
    }

    /**
     * Cancel editing, destroys the editor
     * This function is exposed on Grid and can thus be called as `grid.cancelEditing(...)`
     * @param {Boolean} silent Pass true to prevent method from firing event
     * @fires cancelCellEdit
     * @category Editing
     * @on-owner
     */
    cancelEditing(silent = false, triggeredByEvent) {
        const
            me         = this,
            { editor } = me;

        if (!me.isEditing) {
            return;
        }

        // If called from keyMap, first argument is an event, ignore that
        if (silent.fromKeyMap) {
            triggeredByEvent = silent;
            silent = false;
        }

        me.muteEvents = silent;
        editor.cancelEdit(triggeredByEvent);
        me.muteEvents = false;

        // In case editing is canceled while waiting for finishing promise
        me.finishEditingPromise = false;

        me.afterCellEdit?.();
    }

    /**
     * Finish editing, update the underlying record and destroy the editor
     * This function is exposed on Grid and can thus be called as `grid.finishEditing(...)`
     * @fires finishCellEdit
     * @category Editing
     * @returns {Promise} Resolved promise returns `false` if the edit could not be finished due to the value being invalid or the
     * Editor's `complete` event was vetoed.
     * @on-owner
     */
    async finishEditing() {
        const
            me                      = this,
            { editorContext, grid } = me;

        let result = false;

        // If already waiting for finishing promise, return that
        if (me.finishEditingPromise) {
            return me.finishEditingPromise;
        }

        if (editorContext) {
            const { column } = editorContext;

            // If completeEdit finds that the editor context has a finalize method in it,
            // it will *await* the completion of that method before completing the edit
            // so we must await completeEdit.
            // We can override that finalize method by passing the column's own finalizeCellEdit.
            // Set a flag (promise) indicating that we are in the middle of editing finalization
            me.finishEditingPromise = editorContext.editor.completeEdit(column.bindCallback(column.finalizeCellEdit));
            result = await me.finishEditingPromise;

            // If grid is animating, wait for it to finish to not start a follow-up edit when things are moving
            // (only applies to Scheduler for now, tested in Schedulers CellEdit.t.js)
            await grid.waitForAnimations();

            // reset the flag
            me.finishEditingPromise = null;

            if (result) {
                me.afterCellEdit?.();
            }
        }

        return result;
    }

    //endregion

    //region Events

    /**
     * Event handler added when editing is active called when user clicks a cell in the grid during editing.
     * It finishes editing and moves editor to the selected cell instead.
     * @private
     * @category Internal event handling
     */
    async onCellClickWhileEditing({ event, cellSelector }) {
        const me = this;

        // React cell editor is configured with `allowMouseEvents=true` to prevent editor from swallowing mouse events
        // We ignore these events from editor here to not prevent editing
        if (event.target.closest('.b-editor')) {
            return;
        }

        if (DomHelper.isTouchEvent || event.target.matches(me.ignoreCSSSelector)) {
            await me.finishEditing();
            return;
        }

        // Ignore clicks if async finalization is running
        if (me.finishEditingPromise) {
            return;
        }

        // Ignore clicks in the editor.
        if (me.editorContext && !me.editorContext.editor.owns(event.target)) {
            if (me.getEditingContext(cellSelector)) {
                // Attempt to finish the current edit.
                // Will return false if the field is invalid.
                if (await me.finishEditing()) {
                    if (me.continueEditingOnCellClick) {
                        await me.startEditing(cellSelector);
                    }
                }
                // Previous edit was invalid, return to it.
                else {
                    me.grid.focusCell(me.editorContext);
                    me.editor.inputField.focus();
                }
            }
            else {
                await me.finishEditing();
            }
        }
    }

    /**
     * Starts editing if user taps selected cell again on touch device. Chained function called when user clicks a cell.
     * @private
     * @category Internal event handling
     */
    async onCellClick({ cellSelector, target, event, column }) {
        if (column.onCellClick) {
            // Columns may provide their own handling of cell editing
            return;
        }

        const
            me              = this,
            { focusedCell } = me.client;

        if (target.closest('.b-tree-expander')) {
            return false;
        }
        else if (DomHelper.isTouchEvent &&
            me._lastCellClicked === focusedCell?.cell &&
            event.timeStamp - me.touchEditDelay > me._lastCellClickedTime
        ) {
            await me.startEditing(cellSelector);
        }
        else if (this.triggerEvent === 'cellclick') {
            await me.onTriggerEditEvent({ cellSelector, target });
        }

        me._lastCellClicked     = focusedCell?.cell;
        me._lastCellClickedTime = event.timeStamp;
    }

    // onElementPointerUp should be used to cancel editing before toggleCollapse handled
    // otherwise data collisions may be happened
    onElementPointerUp(event) {
        if (event.target.closest('.b-tree-expander')) {
            this.cancelEditing(undefined, event);
        }
    }

    /**
     * Called when the user triggers the edit action in {@link #config-triggerEvent} config. Starts editing.
     * @private
     * @category Internal event handling
     */
    async onTriggerEditEvent({ cellSelector, target, event }) {
        const { editorContext, client } = this;

        if (target.closest('.b-tree-expander') || (DomHelper.isTouchEvent && event.type === 'dblclick')) {
            return;
        }

        // Should not start editing if cellMenu configured to be shown on event
        if (event && client.features.cellMenu?.triggerEvent === event.type) {
            return;
        }

        if (editorContext) {
            // If we are already editing the cellSelector cell, or the editor cannot finish editing
            // then we must not attempt to start an edit.
            if (editorContext.equals(this.grid.normalizeCellContext(cellSelector)) || !(await this.finishEditing())) {
                return;
            }
        }

        await this.startEditing(cellSelector);
    }

    /**
     * Update the input field if underlying data changes during edit.
     * @private
     * @category Internal event handling
     */
    onStoreUpdate({ changes, record }) {
        const { editorContext } = this;

        if (editorContext?.editor.isVisible) {
            if (record === editorContext.record && editorContext.editor.dataField in changes) {
                editorContext.editor.refreshEdit();
            }
        }
    }

    onStoreBeforeSort() {
        const editor = this.editorContext?.editor;

        if (this.isEditing && !editor?.isFinishing && !editor.isValid) {
            this.cancelEditing();
        }
    }

    /**
     * Realign editor if grid renders rows while editing is ongoing (as a result to autoCommit or WebSocket data received).
     * @private
     * @category Internal event handling
     */
    onGridRefreshed() {
        const
            me = this,
            {
                grid,
                editorContext
            }  = me;

        if (editorContext && grid.isVisible && grid.focusedCell) {
            const
                cell       = grid.getCell(grid.focusedCell),
                { editor } = editorContext;

            // If refresh was triggered by the data change in onEditComplete
            // do not re-show the editor.
            if (cell && DomHelper.isInView(cell) && !editor.isFinishing) {
                editorContext._cell = cell;

                // Editor is inside the cell for A11Y reasons.
                // So any refresh will remove its DOM.
                // We need to silently restore and refocus it.
                GlobalEvents.suspendFocusEvents();
                editor.render(cell);
                editor.showBy(cell);
                editor.focus();
                GlobalEvents.resumeFocusEvents();
            }
            else {
                me.cancelEditing();
            }
        }
    }

    // Gets selected records or selected cells records
    get gridSelection() {
        return [...this.grid.selectedRows, ...this.grid.selectedCells];
    }

    // Tells keyMap what actions are available in certain conditions
    isActionAvailable({ actionName, event }) {
        const me = this;

        if (!me.disabled && !event.target.closest('.b-grid-header')) {
            if (me.isEditing) {
                if (actionName === 'finishAllSelected') {
                    return me.multiEdit && me.gridSelection.length > 1;
                }
                else if (editingActions[actionName]) {
                    return true;
                }
            }
            else if (actionName === 'startEditingFromKeyMap') {
                return me.grid.focusedCell.cell === event.target;
            }
        }
        return false;
    }

    // Will copy edited field value to all selected records
    async finishAllSelected() {
        const
            me                    = this,
            { dataField, record } = me.editor;

        if (await me.finishEditing() && !me.isDestroyed) {
            // Micro-optimization here - accessors could execute additional logic, so we only read it once
            const value = record.getValue(dataField);

            for (const selected of me.gridSelection) {
                if (selected.isModel) {
                    if (selected !== record) {
                        selected.setValue(dataField, value);
                    }
                }
                else {
                    selected.record.set(selected.column.field, value);
                }
            }
        }
    }

    // Will finish editing and start editing next row (unless it's a touch device)
    // If addNewAtEnd, it will create a new row and edit that one if currently editing last row
    async finishAndEditNextRow(event, previous = false) {
        const
            me         = this,
            { grid }   = me,
            { record } = me.editorContext;

        let nextCell;

        if (await me.finishEditing()) {
            // Might be destroyed during the async operation
            if (me.isDestroyed) {
                return;
            }

            // Finalizing might have been blocked by an invalid value
            if (!me.isEditing) {
                // Move to previous
                if (previous) {
                    nextCell = grid.internalNextPrevRow(false, true, false);
                }
                // Move to next
                else {
                    // If we are at the last editable cell, optionally add a new row
                    if (me.addNewAtEnd && record === grid.store.last) {
                        await me.doAddNewAtEnd();
                    }

                    if (!me.isDestroyed) {
                        nextCell = grid.internalNextPrevRow(true, true);
                    }
                }

                // If we have moved, and we are configure to edit the next cell on Enter key...
                if (nextCell && me.editNextOnEnterPress && !grid.touch) {
                    await me.startEditing(nextCell);
                }
            }
        }
    }

    // Will finish editing and start editing previous row
    finishAndEditPrevRow(event) {
        return this.finishAndEditNextRow(event, true);
    }

    // Will finish editing and start editing next cell
    // If addNewAtEnd, it will create a new row and edit that one if currently editing last row
    async finishAndEditNextCell(event, previous = false) {
        const
            me              = this,
            { grid }        = me,
            { focusedCell } = grid;

        if (focusedCell) {
            let cellInfo = me.getAdjacentEditableCell(focusedCell, !previous);

            // If we are at the last editable cell, optionally add a new row
            if (!cellInfo && !previous && me.addNewAtEnd) {
                const currentEditableFinalizationResult = await me.finishEditing();

                if (currentEditableFinalizationResult === true) {
                    await this.doAddNewAtEnd();

                    // Re-grab the next editable cell
                    cellInfo = !me.isDestroyed && me.getAdjacentEditableCell(focusedCell, !previous);
                }
            }

            let finalizationResult = true;

            if (me.isEditing) {
                finalizationResult = await me.finishEditing();
            }

            if (cellInfo) {
                if (!me.isDestroyed && finalizationResult) {
                    grid.focusCell(cellInfo, {
                        animate : me.focusCellAnimationDuration
                    });

                    if (!(await me.startEditing(cellInfo))) {
                        // if editing a cell was vetoed, move on and try again
                        await me.finishAndEditNextCell(event, previous);
                    }
                }
                else {
                    // finishing cell editing was not allowed, current editor value is invalid
                }
            }
            // Finished editing last cell of last row, let grid handle
            else if (grid.isNested && grid.owner &&
                !grid.owner.catchFocus?.({ source : grid, navigationDirection : previous ? 'up' : 'down', editing : true })
            ) {
                grid.onTab(event);
            }
        }
    }

    // Will finish editing and start editing next cell
    finishAndEditPrevCell(event) {
        return this.finishAndEditNextCell(event, true);
    }

    // Handles autoedit
    async onElementKeyDown(event) {
        const
            me              = this,
            { grid }        = me,
            { focusedCell } = grid;

        // flagging event with handled = true used to signal that other features should probably not care about it
        if (event.handled || !me.autoEdit || me.isEditing || !focusedCell || focusedCell.isActionable || event.ctrlKey) {
            return;
        }

        const
            { key }           = event,
            isDelete          = event.key === 'Delete' || event.key === 'Backspace',
            { gridSelection } = isDelete ? me : {},
            isMultiDelete     = me.multiEdit && gridSelection?.length > 1;

        // Any character or space starts editing while autoedit is true
        if ((key.length <= 1 || (isDelete && !isMultiDelete)) && await me.startEditing(focusedCell)) {
            const
                { inputField } = me.editor,
                { input }      = inputField;

            // if editing started with a keypress and the editor has an input field, set its value
            if (input) {
                // Simulate a keydown in an input field by setting input value
                // plus running our internal processing of that event
                inputField.internalOnKeyEvent(event);

                if (!event.defaultPrevented) {
                    input.value = isDelete ? '' : key;
                    inputField.internalOnInput(event);
                }
            }
            event.preventDefault();
        }
        // If deleting while selected multiple rows or cells, the records are changed directly
        else if (isMultiDelete) {
            /**
             * Fires on the owning Grid before deleting a range of selected cell values by pressing `Backspace` or `Del`
             * buttons while {@link #config-autoEdit} is set to `true`. Return `false` to prevent editing.
             * @event beforeCellDelete
             * @on-owner
             * @preventable
             * @param {Grid.view.Grid} source Owner grid
             * @param {Array<Grid.util.Location|Core.data.Model>} gridSelection An array of cell selectors or records
             * that will have their values deleted (the records themself will not get deleted, only visible column
             * values).
             */
            if (grid.trigger('beforeCellRangeDelete', { grid, gridSelection }) !== false) {
                for (const selected of gridSelection) {
                    if (selected.isModel) {
                        grid.columns.visibleColumns.forEach(col => {
                            !col.readOnly && selected.set(col.field, null);
                        });
                    }
                    else if (!selected.column.readOnly) {
                        selected.record.set(selected.column.field, null);
                    }
                }
            }
        }
    }

    // Prevents keys which the Grid handles from bubbling to the grid while editing
    onEditorKeydown(event) {
        if (event.key.length !== 1 && this.grid.matchKeyMapEntry(event) && !this.grid.matchKeyMapEntry(event, this.keyMap)) {
            // Don't allow browser key commands such as PAGE-UP, PAGE-DOWN to continue.
            if (!event.key.startsWith('Arrow') && !event.key === 'Backspace') {
                event.preventDefault();
            }
            event.handled = true;
            event.stopPropagation();
            return false;
        }
    }

    /**
     * Cancel editing on widget focusout
     * @private
     */
    async onEditorFocusOut(event) {
        const
            me              = this,
            {
                grid,
                editor,
                editorContext
            }                   = me,
            toCell              = new Location(event.relatedTarget),
            isEditableCellClick = (toCell.grid === grid) && me.getEditingContext(toCell);

        // If the editor is not losing focus as a result of its tidying up process
        // And focus is moving to outside of the editor, then explicitly terminate.
        if (editorContext && !editor.isFinishing && editor.owns(event._target)) {
            if (me.blurAction === 'cancel') {
                me.cancelEditing(undefined, event);
            }
            // If not already in the middle of editing finalization (that could be async)
            // and it's not a onCellClickWhileEditing situation, finish the edit.
            else if (!me.finishEditingPromise && (me.triggerEvent === 'cellclick' || (me.triggerEvent !== 'cellclick' && !isEditableCellClick))) {
                await me.finishEditing();
            }
        }
    }

    onEditorFocusIn(event) {
        const widget = event.toWidget;

        if (widget === this.editor.inputField) {
            if (this.autoSelect && widget.selectAll && !widget.readOnly && !widget.disabled) {
                widget.selectAll();
            }
        }
    }

    /**
     * Cancel edit on touch outside of grid for mobile Safari (focusout not triggering unless you touch something focusable)
     * @private
     */
    async onTapOut({ event }) {
        const
            me         = this,
            { target } = event;

        // Only "tap out" if were not clicking an element with a _shadowRoot
        if (!target._shadowRoot && !me.editor.owns(target) &&
            (!me.grid.bodyContainer.contains(target) || event.button)) {
            me.editingStoppedByTapOutside = true;
            if (me.blurAction === 'cancel') {
                me.cancelEditing(undefined, event);
            }
            else {
                await me.finishEditing();
            }
            delete me.editingStoppedByTapOutside;
        }
    }

    /**
     * Finish editing if clicking below rows (only applies when grid is higher than rows).
     * @private
     * @category Internal event handling
     */
    async onElementClick(event) {
        if (event.target.classList.contains('b-grid-body-container') && this.editorContext) {
            await this.finishEditing();
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(CellEdit, true);
