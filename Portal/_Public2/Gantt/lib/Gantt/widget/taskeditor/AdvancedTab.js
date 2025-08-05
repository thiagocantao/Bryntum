import FormTab from './FormTab.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';
import '../CalendarField.js';
import '../ConstraintTypePicker.js';
import '../../../Common/widget/DateField.js';
import '../../../Common/widget/FlagField.js';
import '../SchedulingModePicker.js';

/**
 * @module Gantt/widget/taskeditor/AdvancedTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the advanced fields of task, such as calendar and scheduling mode.
 * @internal
 */
export default class AdvancedTab extends FormTab {

    static get type() {
        return 'advancedtab';
    }

    static get defaultConfig() {
        return {
            localeClass : this,
            title       : 'L{Advanced}',
            ref         : 'advancedtab',

            defaults : {
                localeClass : this,
                labelWidth  : this.L('labelWidth')
            },

            items : [
                {
                    type  : 'calendarfield',
                    ref   : 'calendarField',
                    name  : 'calendar',
                    label : 'L{Calendar}',
                    flex  : '1 0 50%',
                    cls   : 'b-inline'
                }, {
                    type  : 'flagfield',
                    ref   : 'manuallyScheduledField',
                    name  : 'manuallyScheduled',
                    label : 'L{Manually scheduled}',
                    flex  : '1 0 50%'
                },
                {
                    type  : 'schedulingmodecombo',
                    ref   : 'schedulingModeField',
                    name  : 'schedulingMode',
                    label : 'L{Scheduling mode}',
                    flex  : '1 0 50%',
                    cls   : 'b-inline'
                },
                {
                    type  : 'flagfield',
                    ref   : 'effortDrivenField',
                    name  : 'effortDriven',
                    label : 'L{Effort driven}',
                    flex  : '1 0 50%'
                },
                {
                    html    : '',
                    dataset : {
                        text : this.L('Constraint')
                    },
                    cls  : 'b-divider',
                    flex : '1 0 100%'
                },
                {
                    type      : 'constrainttypepicker',
                    ref       : 'constraintTypeField',
                    name      : 'constraintType',
                    label     : 'L{Constraint type}',
                    clearable : true,
                    flex      : '1 0 50%',
                    cls       : 'b-inline'
                },
                {
                    type  : 'date',
                    ref   : 'constraintDateField',
                    name  : 'constraintDate',
                    label : 'L{Constraint date}',
                    flex  : '1 0 50%',
                    cls   : 'b-inline'
                }
            ]
        };
    }

    get calendarField() {
        return this.widgetMap.calendarField;
    }

    get constraintTypeField() {
        return this.widgetMap.constraintTypeField;
    }

    get constraintDateField() {
        return this.widgetMap.constraintDateField;
    }

    loadEvent(eventRecord) {
        const me = this,
            firstLoad = !me.record;

        //<debug>
        console.assert(
            firstLoad || me.getProject() === eventRecord.getProject(),
            'Loading of a record from another project is not currently supported!'
        );
        //</debug>

        const calendarField = this.calendarField;

        if (firstLoad) {
            calendarField.store = eventRecord.getProject().getCalendarManagerStore();
        }

        super.loadEvent(eventRecord);
    }
}

BryntumWidgetAdapterRegister.register(AdvancedTab.type, AdvancedTab);
