import ColorColumn from '../../Grid/column/ColorColumn.js';
import '../widget/EventColorPicker.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';

/**
 * @module Scheduler/column/EventColorColumn
 */

/**
 * A column that displays Event's `eventColor` values (built-in color classes or CSS colors) as a colored element
 * similar to the {@link Scheduler.widget.EventColorField}. When the user clicks the element, a
 * {@link Scheduler.widget.EventColorPicker} lets the user select from a
 * {@link Scheduler.view.mixin.TimelineEventRendering#config-eventColor range of colors}.
 *
 * ```javascript
 * new Scheduler({
 *    columns : [
 *       {
 *          type : 'eventcolor',
 *          text : 'EventColor'
 *       }
 *    ]
 * });
 * ```
 *
 * {@inlineexample Scheduler/column/EventColorColumn.js}
 *
 * @extends Grid/column/ColorColumn
 * @classType eventcolor
 * @column
 */
export default class EventColorColumn extends ColorColumn {
    static $name = 'EventColorColumn';

    static type = 'eventcolor';

    static defaults = {
        colorEditorType : 'eventcolorpicker',
        field           : 'eventColor'
    };

}

ColumnStore.registerColumnType(EventColorColumn);
