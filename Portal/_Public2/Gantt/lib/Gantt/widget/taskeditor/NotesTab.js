import FormTab from './FormTab.js';
import '../../../Common/widget/TextAreaField.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Gantt/widget/taskeditor/NotesTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the notes for a task.
 * @internal
 */
export default class NotesTab extends FormTab {

    static get type() {
        return 'notestab';
    }

    static get defaultConfig() {
        return {
            localeClass : this,
            title       : 'L{Notes}',
            ref         : 'notestab',

            layoutConfig : {
                alignItems   : 'flex-start',
                alignContent : 'stretch'
            },

            items : [
                {
                    type : 'textareafield',
                    cls  : 'b-taskeditor-notes-field',
                    name : 'note'
                }
            ]
        };
    }
}

BryntumWidgetAdapterRegister.register(NotesTab.type, NotesTab);
