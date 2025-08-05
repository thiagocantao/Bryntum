import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import ConstraintTypePicker from '../../SchedulerPro/widget/ConstraintTypePicker.js';

/**
 * @module Gantt/column/ConstraintTypeColumn
 */
const directionMap = { Forward : 'assoonaspossible', Backward : 'aslateaspossible' };

/**
 * {@link Gantt/model/TaskModel#field-constraintType Constraint type} column.
 *
 * Default editor is a {@link SchedulerPro/widget/ConstraintTypePicker}.
 *
 * The constraint can be one of:
 *
 * - Must start on [date]
 * - Must finish on [date]
 * - Start no earlier than [date]
 * - Start no later than [date]
 * - Finish no earlier than [date]
 * - Finish no later than [date]
 *
 * The date of the constraint can be specified with the {@link Gantt/column/ConstraintDateColumn}
 *
 * @extends Grid/column/Column
 * @classType constrainttype
 * @column
 */
export default class ConstraintTypeColumn extends Column {
    static get $name() {
        return 'ConstraintTypeColumn';
    }

    static get type() {
        return 'constrainttype';
    }

    static get isGanttColumn() {
        return true;
    }

    static get defaults() {
        return {
            field  : 'constraintType',
            text   : 'L{Constraint Type}',
            width  : 146,
            editor : {
                type         : 'constrainttypepicker',
                clearable    : true,
                allowInvalid : false
            },
            filterable : {
                filterField : {
                    type : 'constrainttypepicker'
                }
            }
        };
    }

    get editor() {
        const editor = super.editor;

        editor.includeAsapAlapAsConstraints = this.grid.project.includeAsapAlapAsConstraints;

        return editor;
    }

    renderer({ record, value }) {
        return ConstraintTypePicker.localize((this.grid.project.includeAsapAlapAsConstraints && directionMap[record.direction]) || value) || '';
    }
}

ColumnStore.registerColumnType(ConstraintTypeColumn);
