import FormTab from './FormTab.js';
import '../CalendarField.js';
import '../ConstraintTypePicker.js';
import '../../../Core/widget/DateField.js';
import '../../../Core/widget/Checkbox.js';
import '../SchedulingModePicker.js';

/**
 * @module SchedulerPro/widget/taskeditor/AdvancedTab
 */

/**
 * Advanced task options {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro.widget.GanttTaskEditor gantt task editor} tab.
 *
 * | Field ref                | Type      | Weight | Description                                                                                                                  |
 * |--------------------------|-----------|--------|------------------------------------------------------------------------------------------------------------------------------|
 * | `calendarField`          | Combo     | 100    | Shows a list of available calendars for this task                                                                            |
 * | `manuallyScheduledField` | Checkbox  | 200    | If checked, the task is not considered in scheduling                                                                         |
 * | `schedulingModeField`    | Combo     | 300    | Shows a list of available scheduling modes for this task                                                                     |
 * | `effortDrivenField`      | Checkbox  | 400    | If checked, the effort of the task is kept intact, and the duration is updated. Works when scheduling mode is "Fixed Units". |
 * | `divider`                | Widget    | 500    | Visual splitter between 2 groups of fields                                                                                   |
 * | `constraintTypeField`    | Combo     | 600    | Shows a list of available constraints for this task                                                                          |
 * | `constraintDateField`    | DateField | 700    | Shows a date for the selected constraint type                                                                                |
 * | `rollupField`            | Checkbox  | 800    | If checked, shows a bar below the parent task. Works when the "Rollup" feature is enabled.                                   |
 *
 * @extends SchedulerPro/widget/taskeditor/FormTab
 * @classtype advancedtab
 */
export default class AdvancedTab extends FormTab {

    static get $name() {
        return 'AdvancedTab';
    }

    // Factoryable type name
    static get type() {
        return 'advancedtab';
    }

    static get defaultConfig() {
        return {
            localeClass : this,
            title       : 'L{Advanced}',
            cls         : 'b-advanced-tab',

            defaults : {
                localeClass : this
            },

            items : {
                calendarField : {
                    type   : 'calendarfield',
                    weight : 100,
                    ref    : '',
                    name   : 'calendar',
                    label  : 'L{Calendar}',
                    flex   : '1 0 50%',
                    cls    : 'b-inline'
                },
                manuallyScheduledField : {
                    type   : 'checkbox',
                    weight : 200,
                    name   : 'manuallyScheduled',
                    label  : 'L{Manually scheduled}',
                    flex   : '1 0 50%'
                },
                schedulingModeField : {
                    type   : 'schedulingmodecombo',
                    weight : 300,
                    name   : 'schedulingMode',
                    label  : 'L{Scheduling mode}',
                    flex   : '1 0 50%',
                    cls    : 'b-inline'
                },
                effortDrivenField : {
                    type   : 'checkbox',
                    weight : 400,
                    name   : 'effortDriven',
                    label  : 'L{Effort driven}',
                    flex   : '1 0 50%'
                },
                divider : {
                    weight  : 500,
                    html    : '',
                    dataset : {
                        text : this.L('L{Constraint}')
                    },
                    cls  : 'b-divider',
                    flex : '1 0 100%'
                },
                constraintTypeField : {
                    type        : 'constrainttypepicker',
                    weight      : 600,
                    name        : 'constraintType',
                    label       : 'L{Constraint type}',
                    pickerWidth : '14em',
                    clearable   : true,
                    flex        : '1 0 50%',
                    cls         : 'b-inline'
                },
                constraintDateField : {
                    type   : 'date',
                    weight : 700,
                    name   : 'constraintDate',
                    label  : 'L{Constraint date}',
                    flex   : '1 0 50%',
                    cls    : 'b-inline'
                },
                rollupField : {
                    type   : 'checkbox',
                    weight : 800,
                    name   : 'rollup',
                    label  : 'L{Rollup}',
                    flex   : '1 0 50%',
                    cls    : 'b-inline'
                }
            }
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

    get effortDrivenField() {
        return this.widgetMap.effortDrivenField;
    }

    get manuallyScheduledField() {
        return this.widgetMap.manuallyScheduledField;
    }

    get rollupField() {
        return this.widgetMap.rollupField;
    }

    get schedulingModeField() {
        return this.widgetMap.schedulingModeField;
    }

    loadEvent(eventRecord) {
        const
            me        = this,
            firstLoad = !me.record;

        //<debug>
        console.assert(
            firstLoad || me.project === eventRecord.project,
            'Loading of a record from another project is not currently supported!'
        );
        //</debug>

        const { calendarField } = me;

        if (calendarField && firstLoad) {
            calendarField.store = eventRecord.project.calendarManagerStore;
        }

        super.loadEvent(eventRecord);
    }
}

// Register this widget type with its Factory
AdvancedTab.initClass();
