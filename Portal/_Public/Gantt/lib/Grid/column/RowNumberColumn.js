import Column from './Column.js';
import ColumnStore from '../data/ColumnStore.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module Grid/column/RowNumberColumn
 */

/**
 * A column that displays the row number in each cell.
 *
 * There is no `editor`, since value is read-only.
 *
 * ```javascript
 * const grid = new Grid({
 *   appendTo : targetElement,
 *   width    : 300,
 *   columns  : [
 *     { type : 'rownumber' }
 *   ]
 * });
 *
 * @extends Grid/column/Column
 *
 * @classType rownumber
 * @inlineexample Grid/column/RowNumberColumn.js
 * @column
 */
export default class RowNumberColumn extends Column {

    static $name = 'RowNumberColumn';

    static type = 'rownumber';

    static get defaults() {
        return {
            /**
             * @config {Boolean} groupable
             * @hide
             */
            groupable : false,

            /**
             * @config {Boolean} sortable
             * @hide
             */
            sortable : false,

            /**
             * @config {Boolean} filterable
             * @hide
             */
            filterable : false,

            /**
             * @config {Boolean} searchable
             * @hide
             */
            searchable : false,

            /**
             * @config {Boolean} resizable
             * @hide
             */
            resizable : false,

            /**
             * @config {Boolean} draggable
             * @hide
             */
            draggable : false,

            minWidth : 50,
            width    : 50,
            align    : 'center',
            text     : '#',
            editor   : false,
            readOnly : true
        };
    }

    construct(config) {
        super.construct(...arguments);

        const
            me       = this,
            { grid } = me;

        me.internalCellCls        = 'b-row-number-cell';
        me.externalHeaderRenderer = me.headerRenderer;
        me.headerRenderer         = me.internalHeaderRenderer;

        if (grid) {
            // Update our width when the store mutates (tests test Columns in isolation with no grid, so we must handle that!)
            grid.ion({
                bindStore : 'bindStore',
                thisObj   : me
            });

            me.bindStore({ store : grid.store, initial : true });

            if (grid.store.count && !grid.rendered) {
                grid.ion({
                    paint   : 'resizeToFitContent',
                    thisObj : me,
                    once    : true
                });
            }
        }
    }

    get groupHeaderReserved() {
        return true;
    }

    bindStore({ store, initial }) {
        const me = this;

        me.detachListeners('grid');

        store.ion({
            name                                  : 'grid',
            [`change${me.grid.asyncEventSuffix}`] : 'onStoreChange',
            thisObj                               : me
        });

        if (!initial && !me.resizeToFitContent()) {
            me.measureOnRender();
        }
    }

    onStoreChange({ action, isMove }) {
        if (action === 'dataset' || action === 'add' || action === 'remove' || action === 'removeall') {

            // Ignore remove phase of move operation, resize on add phase only
            if (action === 'remove' && isMove) {
                return;
            }

            const result = this.resizeToFitContent();

            // Gantt/Scheduler draws later when loading using CrudManager (refresh is suspended), catch first draw
            if (action === 'dataset' && !result && this.grid.store.count) {
                this.measureOnRender();
            }
        }
    }

    measureOnRender() {
        this.grid.rowManager.ion({
            renderDone() {
                this.resizeToFitContent();
            },
            once    : true,
            thisObj : this
        });
    }

    /**
     * Renderer that displays the row number in the cell.
     * @private
     */
    renderer({ record, grid }) {
        return record.isSpecialRow ? '' : grid.store.indexOf(record, true) + 1;
    }

    /**
     * Resizes the column to match the widest string in it. Called when you double click the edge between column
     * headers
     */
    resizeToFitContent() {
        const
            { grid }  = this,
            { store } = grid,
            { count } = store;

        if (count && !this.hidden) {
            const cellElement = grid.element.querySelector(`.b-grid-cell[data-column-id="${this.id}"]`);


            if (cellElement) {
                const
                    cellPadding = parseInt(DomHelper.getStyleValue(cellElement, 'padding-left')),
                    maxWidth    = DomHelper.measureText(count, cellElement);

                this.width = Math.max(this.minWidth, maxWidth + 2 * cellPadding);

                return true;
            }
        }

        return false;
    }

    set flex(f) {

    }

    internalHeaderRenderer({ headerElement, column }) {
        headerElement.classList.add('b-rownumber-header');
        return column.externalHeaderRenderer?.call(this, ...arguments) || column.headerText;
    }
}

ColumnStore.registerColumnType(RowNumberColumn, true);
