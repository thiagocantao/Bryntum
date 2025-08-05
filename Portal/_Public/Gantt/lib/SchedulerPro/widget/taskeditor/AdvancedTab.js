import FormTab from './FormTab.js';
import '../CalendarField.js';
import '../ConstraintTypePicker.js';
import '../SchedulingDirectionPicker.js';
import '../../../Core/widget/DateField.js';
import '../../../Core/widget/Checkbox.js';
import '../SchedulingModePicker.js';

/**
 * @module SchedulerPro/widget/taskeditor/AdvancedTab
 */

/**
 * Advanced task options {@link SchedulerPro/widget/SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro/widget/GanttTaskEditor gantt task editor} tab.
 *
 * | Field ref                     | Type                                             | Weight | Description                                                                                                                  |
 * |-------------------------------|--------------------------------------------------|--------|------------------------------------------------------------------------------------------------------------------------------|
 * | `calendarField`               | {@link Core/widget/Combo}                        | 100    | Shows a list of available calendars for this task                                                                            |
 * | `manuallyScheduledField`      | {@link Core/widget/Checkbox}                     | 200    | If checked, the task is not considered in scheduling                                                                         |
 * | `schedulingModeField`         | {@link SchedulerPro/widget/SchedulingModePicker} | 300    | Shows a list of available scheduling modes for this task                                                                     |
 * | `effortDrivenField`           | {@link Core/widget/Checkbox}                     | 400    | If checked, the effort of the task is kept intact, and the duration is updated. Works when scheduling mode is "Fixed Units". |
 * | `divider`                     | {@link Core/widget/Widget}                       | 500    | Visual splitter between 2 groups of fields                                                                                   |
 * | `constraintTypeField`         | {@link SchedulerPro/widget/ConstraintTypePicker} | 600    | Shows a list of available constraints for this task                                                                          |
 * | `constraintDateField`         | {@link Core/widget/DateField}                    | 700    | Shows a date for the selected constraint type                                                                                |
 * | `rollupField`                 | {@link Core/widget/Checkbox}                     | 800    | If checked, shows a bar below the parent task. Works when the "Rollup" feature is enabled.                                   |
 * | `inactiveField`               | {@link Core/widget/Checkbox}                     | 900    | Allows to inactivate the task so it won't take part in the scheduling process.                                               |
 * | `ignoreResourceCalendarField` | {@link Core/widget/Checkbox}                     | 1000   | If checked the task ignores the assigned resource calendars when scheduling                                                  |
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
        const
            col1 = {
                flex       : '0 0 calc(64% - var(--autocontainer-gap) / 2)',
                labelWidth : '30%'
            },
            col2 = {
                flex       : '0 0 calc(34% - var(--autocontainer-gap) / 2)',
                labelWidth : '80%'
            };

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
                    ...col1
                },
                ignoreResourceCalendarField : {
                    type   : 'checkbox',
                    weight : 100,
                    name   : 'ignoreResourceCalendar',
                    label  : 'L{Ignore resource calendar}',
                    cls    : 'b-ignore-resource-calendar',
                    ...col2
                },
                schedulingModeField : {
                    type   : 'schedulingmodecombo',
                    weight : 300,
                    name   : 'schedulingMode',
                    label  : 'L{Scheduling mode}',
                    ...col1
                },
                effortDrivenField : {
                    type   : 'checkbox',
                    weight : 400,
                    name   : 'effortDriven',
                    label  : 'L{Effort driven}',
                    ...col2
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
                    type      : 'constrainttypepicker',
                    weight    : 600,
                    name      : 'constraintType',
                    label     : 'L{Constraint type}',
                    clearable : true,
                    ...col1
                },
                rollupField : {
                    type   : 'checkbox',
                    weight : 700,
                    name   : 'rollup',
                    label  : 'L{Rollup}',
                    ...col2
                },
                constraintDateField : {
                    type     : 'date',
                    weight   : 800,
                    name     : 'constraintDate',
                    label    : 'L{Constraint date}',
                    keepTime : 'entered',
                    ...col1
                },
                schedulingDirectionField : {
                    type   : 'schedulingdirectionpicker',
                    weight : 850,
                    name   : 'direction',
                    label  : 'L{Scheduling direction}',
                    ...col1
                },
                inactiveField : {
                    type   : 'checkbox',
                    weight : 900,
                    name   : 'inactive',
                    label  : 'L{Inactive}',
                    ...col2
                },
                manuallyScheduledField : {
                    type   : 'checkbox',
                    weight : 1000,
                    name   : 'manuallyScheduled',
                    label  : 'L{Manually scheduled}',
                    cls    : 'b-last-row',
                    style  : 'margin-left:auto',
                    ...col2
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

    get schedulingDirectionField() {
        return this.widgetMap.schedulingDirectionField;
    }

    loadEvent(eventRecord) {
        const
            me                       = this,
            {
                calendarField,
                constraintTypeField,
                includeAsapAlapAsConstraints,
                schedulingDirectionField
            }                        = me,
            { calendarManagerStore } = eventRecord.project,
            storeChange              = calendarField?.store !== calendarManagerStore;

        if (calendarField) {
            if (storeChange) {
                // Ensure also child calendars of collapsed parents are visible in the Combo list
                calendarField.store = calendarManagerStore.chain(undefined, undefined, { excludeCollapsedRecords : false });
            }
            else {
                calendarField.store.resumeChain();
            }
        }

        if (constraintTypeField) {
            constraintTypeField.taskRecord = eventRecord;
            constraintTypeField.includeAsapAlapAsConstraints = includeAsapAlapAsConstraints;
        }
        if (schedulingDirectionField) {
            schedulingDirectionField.hidden = includeAsapAlapAsConstraints;
        }



        super.loadEvent(eventRecord);
    }

    beforeSave() {
        const { calendarField } = this.widgetMap;

        if (calendarField) {
            calendarField.store.suspendChain();
        }
    }
}

// Register this widget type with its Factory
AdvancedTab.initClass();
