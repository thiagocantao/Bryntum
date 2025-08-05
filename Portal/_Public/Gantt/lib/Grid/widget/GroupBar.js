import ChipView from '../../Core/widget/ChipView.js';
import Store from '../../Core/data/Store.js';
import StringHelper from '../../Core/helper/StringHelper.js';

/**
 * @module Grid/widget/GroupBar
 */

/**
 * A widget used to manage grouping of a tree with the {@link Grid.feature.TreeGroup} feature. Column headers can be
 * drag-dropped on this widget to regroup the data in the tree store. This widget only handles column-based grouping,
 * and doesn't handle custom group functions.
 *
 * ```javascript
 * const tree = new TreeGrid({
 *     appendTo : 'container',
 *     features : {
 *         treeGroup : {
 *             hideGroupedColumns : true,
 *             levels             : [
 *                 'manager',
 *                 'airline'
 *             ],
 *             parentRenderer : (field, data) => `${StringHelper.capitalize(field)}: ${data.name}`
 *         }
 *     },
 *
 *     columns : [
 *         {
 *             text  : 'Name',
 *             field : 'name',
 *             flex  : 3,
 *             type  : 'tree'
 *         },
 *         {
 *             text   : 'Airline',
 *             field  : 'airline',
 *             align  : 'center',
 *             flex   : 2,
 *         },
 *         {
 *             type  : 'check',
 *             text  : 'Domestic',
 *             field : 'domestic',
 *             align : 'left',
 *             flex  : 1
 *         },
 *         {
 *             type  : 'number',
 *             text  : 'Capacity',
 *             field : 'capacity',
 *             flex  : 1
 *         },
 *         {
 *             type  : 'number',
 *             text  : 'Crew',
 *             field : 'crew',
 *             flex  : 1
 *         }
 *     ],
 *
 *     tbar : [
 *         'Group by',
 *         {
 *             type : 'groupbar'
 *         }
 *     ]
 * ```
 * @classtype groupbar
 * @extends Core/widget/ChipView
 * @demo Grid/tree-grouping
 * @widget
 */
export default class GroupBar extends ChipView {
    static type  = 'groupbar';
    static $name = 'GroupBar';

    static configurable = {
        selectedCls : 'not-used',
        itemTpl(record) {
            return StringHelper.encodeHtml(StringHelper.capitalize(record.getValue(this.displayField)));
        }
    };

    construct() {
        super.construct(...arguments);

        const treeGrid = this.treeGrid = this.up('gridbase', true);

        if (!treeGrid) {
            throw new Error('GroupBar must be used inside a Grid component');
        }
        treeGrid.ion({
            paint   : this.onTreePaint,
            once    : true,
            thisObj : this
        });
    }

    onTreePaint() {
        const
            me           = this,
            { treeGrid } = me,
            { treeGroup, columnReorder } = treeGrid.features;

        if (!treeGroup) {
            throw new Error('GroupBar widget requires the TreeGroup feature to be present');
        }

        columnReorder.usingGroupBarWidget = true;

        me.store = new Store({
            fields            : ['cls', 'ascending'],
            internalListeners : {
                add     : me.onStoreChanged,
                remove  : me.onStoreChanged,
                thisObj : me
            }
        });

        treeGrid.ion({
            treeGroupChange          : me.onTreeGroupChanged,
            beforeColumnDropFinalize : me.onBeforeColumnDropFinalize,
            columnDrag               : me.onColumnDrag,
            columnDragStart          : me.onColumnDragStart,
            columnDrop               : me.onColumnDrop,
            thisObj                  : me
        });

        me.onTreeGroupChanged({ levels : treeGroup.levels });
    }

    onStoreChanged({ records }) {
        const
            me           = this,
            { treeGrid } = me;

        if (!me.treeGrid.isConstructing && records?.[0]?.cls !== 'b-drop-target') {
            me.ignoreGroupChange               = true;
            treeGrid.features.treeGroup.levels = me.store.map(({ field }) => field);
            me.ignoreGroupChange               = false;
        }
    }

    onTreeGroupChanged({ levels }) {
        if (!this.ignoreGroupChange) {
            if (levels.some(level => level instanceof Function && !level.fieldName)) {
                throw new Error('GroupBar only supports column grouping');
            }
            this.store.data = (levels || []).map(level => {
                level = level.fieldName || level;
                return this.treeGrid.columns.get(level);
            });
        }
    }

    onColumnDragStart() {
        this.store.add({ id : 'placeholder', cls : 'b-drop-target' }); 
    }

    onColumnDrag({ context, column, event }) {
        const overGroupBar = event.target.closest('.b-groupbar');

        if (overGroupBar) {
            context.valid = true;
        }
    }

    onBeforeColumnDropFinalize({ column, event }) {
        const droppedOnGroupBar = event.target.closest('.b-groupbar');

        if (droppedOnGroupBar) {
            if (!column.isTreeColumn) {
                this.store.getById('placeholder').remove();
                this.store.add(column);
            }
        }
    }

    onColumnDrop() {
        this.store.getById('placeholder')?.remove();
    }
}

GroupBar.initClass();
