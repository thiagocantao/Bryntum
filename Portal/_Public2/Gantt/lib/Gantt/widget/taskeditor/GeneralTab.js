import FormTab from './FormTab.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';
import '../DependencyTypePicker.js';
import '../../column/DurationColumn.js';
import '../../widget/EffortField.js';

/**
 * @module Gantt/widget/taskeditor/GeneralTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the general information for a task.
 * @internal
 */
export default class GeneralTab extends FormTab {

    static get type() {
        return 'generaltab';
    }

    static get defaultConfig() {
        return {
            localeClass : this,
            title       : 'L{General}',
            ref         : 'generaltab',

            defaults : {
                localeClass : this,
                labelWidth  : this.L('labelWidth')
            },

            items : [
                {
                    type      : 'text',
                    required  : true,
                    label     : 'L{Name}',
                    clearable : true,
                    name      : 'name',
                    ref       : 'nameField',
                    cls       : 'b-name'
                },
                {
                    type  : 'number',
                    label : 'L{% complete}',
                    name  : 'percentDone',
                    ref   : 'percentDoneField',
                    cls   : 'b-percent-done b-inline',
                    flex  : '1 0 50%',
                    min   : 0,
                    max   : 100
                },
                {
                    type  : 'effort',
                    label : 'L{Effort}',
                    name  : 'fullEffort',
                    ref   : 'effortField',
                    flex  : '1 0 50%'
                },
                {
                    html    : '',
                    dataset : {
                        text : this.L('Dates')
                    },
                    cls  : 'b-divider',
                    flex : '1 0 100%'
                },
                {
                    type  : 'date',
                    label : 'L{Start}',
                    name  : 'startDate',
                    ref   : 'startDateField',
                    cls   : 'b-start-date b-inline',
                    flex  : '1 0 50%'
                },
                {
                    type  : 'date',
                    label : 'L{Finish}',
                    name  : 'endDate',
                    ref   : 'endDateField',
                    cls   : 'b-end-date',
                    flex  : '1 0 50%'
                },
                {
                    type  : 'durationfield',
                    label : 'L{Duration}',
                    name  : 'fullDuration',
                    ref   : 'fullDurationField',
                    flex  : '.5 0',
                    cls   : 'b-inline'
                }
            ]
        };
    }

    loadEvent(record) {
        const
            step = {
                unit      : record.durationUnit,
                magnitude : 1
            },
            { isParent } = record,
            {
                fullDurationField,
                percentDoneField,
                startDateField,
                endDateField,
                effortField
            } = this.widgetMap;

        // Editing duration, percentDone & endDate disallowed for parent tasks
        if (fullDurationField) {
            fullDurationField.disabled = isParent;
        }

        if (percentDoneField) {
            percentDoneField.disabled = isParent;
        }

        if (startDateField) {
            startDateField.step = step;
        }

        if (endDateField) {
            endDateField.step = step;
            endDateField.disabled = isParent;
        }

        if (effortField) {
            effortField.unit = record.effortUnit;
            effortField.disabled = isParent;
        }

        super.loadEvent(record);
    }
}

BryntumWidgetAdapterRegister.register(GeneralTab.type, GeneralTab);
