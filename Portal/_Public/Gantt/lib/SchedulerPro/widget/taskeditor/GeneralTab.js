import FormTab from './FormTab.js';
import '../../widget/StartDateField.js';
import '../../widget/EndDateField.js';
import '../../widget/EffortField.js';
import '../../../Core/widget/NumberField.js';
import '../../../Scheduler/widget/EventColorField.js';

/**
 * @module SchedulerPro/widget/taskeditor/GeneralTab
 */

/**
 * A tab inside the {@link SchedulerPro/widget/SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro/widget/GanttTaskEditor gantt task editor} showing the general information for a task.
 *
 * | Field ref      | Type                                       | Text       | Weight | Description                                                        |
 * |----------------|--------------------------------------------|------------|--------|--------------------------------------------------------------------|
 * | `name`         | {@link Core/widget/TextField}              | Name       | 100    | Task name                                                          |
 * | `percentDone`  | {@link Core/widget/NumberField}            | % Complete | 200    | Shows what part of task is done already in percentage              |
 * | `effort`       | {@link SchedulerPro/widget/EffortField}    | Effort     | 300    | Shows how much working time is required to complete the whole task |
 * | `divider`      | {@link Core/widget/Widget}                 |            | 400    | Visual splitter between 2 groups of fields                         |
 * | `startDate`    | {@link SchedulerPro/widget/StartDateField} | Start      | 500    | Shows when the task begins                                         |
 * | `endDate`      | {@link SchedulerPro/widget/EndDateField}   | Finish     | 600    | Shows when the task ends                                           |
 * | `duration`     | {@link Core/widget/DurationField}          | Duration   | 700    | Shows how long the task is                                         |
 * | `colorField` ¹ | {@link Scheduler.widget.EventColorField}   | Color ¹    | 800    | Choose background color for the task bar                           |
 *
 * **¹** Set the {@link SchedulerPro.view.SchedulerProBase#config-showTaskColorPickers} config to `true` to enable this field
 *
 * @extends SchedulerPro/widget/taskeditor/FormTab
 * @classtype generaltab
 */
export default class GeneralTab extends FormTab {
    static get $name() {
        return 'GeneralTab';
    }

    // Factoryable type name
    static get type() {
        return 'generaltab';
    }

    static get defaultConfig() {
        return {
            title : 'L{General}',
            cls   : 'b-general-tab',

            defaults : {
                localeClass : this
            },

            items : {
                name : {
                    type      : 'text',
                    weight    : 100,
                    required  : true,
                    label     : 'L{Name}',
                    clearable : true,
                    name      : 'name',
                    cls       : 'b-name'
                },
                percentDone : {
                    type   : 'number',
                    weight : 200,
                    label  : 'L{% complete}',
                    name   : 'renderedPercentDone',
                    cls    : 'b-percent-done b-half-width',
                    min    : 0,
                    max    : 100
                },
                effort : {
                    type   : 'effort',
                    weight : 300,
                    label  : 'L{Effort}',
                    name   : 'fullEffort',
                    cls    : 'b-half-width'
                },
                divider : {
                    html    : '',
                    weight  : 400,
                    dataset : {
                        text : this.L('L{Dates}')
                    },
                    cls  : 'b-divider',
                    flex : '1 0 100%'
                },
                startDate : {
                    type   : 'startdate',
                    weight : 500,
                    label  : 'L{Start}',
                    name   : 'startDate',
                    cls    : 'b-start-date b-half-width'
                },
                endDate : {
                    type   : 'enddate',
                    weight : 600,
                    label  : 'L{Finish}',
                    name   : 'endDate',
                    cls    : 'b-end-date b-half-width'
                },
                duration : {
                    type   : 'durationfield',
                    weight : 700,
                    label  : 'L{Duration}',
                    name   : 'fullDuration',
                    cls    : 'b-half-width'
                },
                colorField : {
                    type   : 'eventcolorfield',
                    weight : 800,
                    label  : 'L{SchedulerBase.color}',
                    name   : 'eventColor',
                    cls    : 'b-half-width'
                }
            }
        };
    }

    loadEvent(record) {
        const
            step         = {
                unit      : record.durationUnit,
                magnitude : 1
            },
            {
                startDate,
                endDate,
                effort
            }            = this.widgetMap;

        if (startDate) {
            startDate.step = step;
            startDate.eventRecord = record;
        }

        if (endDate) {
            endDate.step = step;
            endDate.eventRecord = record;
        }

        if (effort) {
            effort.unit = record.effortUnit;
        }

        super.loadEvent(record);
    }
}

// Register this widget type with its Factory
GeneralTab.initClass();
