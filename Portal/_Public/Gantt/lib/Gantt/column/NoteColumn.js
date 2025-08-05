import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import '../../Core/widget/TextAreaPickerField.js';

/**
 * @module Gantt/column/NoteColumn
 */

/**
 * A column which displays a task's {@link Gantt.model.TaskModel#field-note note} field.
 *
 * Default editor is a {@link Core.widget.TextAreaPickerField}.
 *
 * @extends Grid/column/Column
 * @classType note
 * @column
 */
export default class NoteColumn extends Column {

    static get $name() {
        return 'NoteColumn';
    }

    static get type() {
        return 'note';
    }

    static get isGanttColumn() {
        return true;
    }

    static get defaults() {
        return {
            field  : 'note',
            text   : 'L{Note}',
            width  : 150,
            editor : {
                type : 'textareapickerfield'
            }
        };
    }

    renderer({ value }) {
        return (value || '').trim();
    }

}

ColumnStore.registerColumnType(NoteColumn);
