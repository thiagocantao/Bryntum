import FormTab from './FormTab.js';
import '../../../Core/widget/TextAreaField.js';

/**
 * @module SchedulerPro/widget/taskeditor/NotesTab
 */

/**
 * A tab inside the {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro.widget.GanttTaskEditor gantt task editor} showing the notes for an event or task.
 *
 * | Field ref   | Type          | Weight | Description                                     |
 * |-------------|---------------|--------|-------------------------------------------------|
 * | `noteField` | TextAreaField | 100    | Shows a text area to add text notes to the task |
 *
 * @extends SchedulerPro/widget/taskeditor/FormTab
 * @classtype notestab
 */
export default class NotesTab extends FormTab {

    static get $name() {
        return 'NotesTab';
    }

    // Factoryable type name
    static get type() {
        return 'notestab';
    }

    static get defaultConfig() {
        return {
            title : '<i class="b-icon b-icon-note" data-btip="L{Notes}"></i>',
            cls   : 'b-notes-tab',

            layoutConfig : {
                alignItems   : 'flex-start',
                alignContent : 'stretch'
            },

            items : {
                noteField : {
                    weight : 100,
                    type   : 'textareafield',
                    cls    : 'b-taskeditor-notes-field',
                    name   : 'note'
                }
            }
        };
    }
}

// Register this widget type with its Factory
NotesTab.initClass();
