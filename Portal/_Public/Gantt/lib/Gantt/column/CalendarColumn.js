import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import CalendarPicker from '../widget/CalendarPicker.js';

/**
 * @module Gantt/column/CalendarColumn
 */

/**
 * A column that displays (and allows user to update) the current {@link Gantt.model.CalendarModel calendar} of the task.
 *
 * Default editor is a {@link Gantt.widget.CalendarPicker CalendarPicker}.
 *
 * @extends Grid/column/Column
 * @classType calendar
 * @column
 */
export default class CalendarColumn extends Column {

    static get $name() {
        return 'CalendarColumn';
    }

    static get type() {
        return 'calendar';
    }

    static get isGanttColumn() {
        return true;
    }

    static get defaults() {
        return {
            field  : 'calendar',
            text   : 'L{Calendar}',
            editor : {
                type         : CalendarPicker.type,
                clearable    : true,
                allowInvalid : false
            }
        };
    }

    afterConstruct() {
        super.afterConstruct();

        const
            me      = this,
            project = me.grid.project;

        // Store default calendar to filter out this value
        me.defaultCalendar = project.defaultCalendar;

        me.refreshCalendars();

        project.calendarManagerStore.ion({
            changePreCommit : me.refreshCalendars,
            refresh         : me.refreshCalendars,
            thisObj         : me
        });
    }

    // region Events

    refreshCalendars() {
        if (this.editor) {
            const project = this.grid.project;

            this.editor.refreshCalendars(project.calendarManagerStore.allRecords);
        }
    }

    // endregion

    renderer({ value }) {
        if (value !== this.defaultCalendar && value?.id != null) {
            const model = this.grid.project.calendarManagerStore.getById(value.id);
            return model?.name ?? '';
        }
        return '';
    }
}

ColumnStore.registerColumnType(CalendarColumn);
