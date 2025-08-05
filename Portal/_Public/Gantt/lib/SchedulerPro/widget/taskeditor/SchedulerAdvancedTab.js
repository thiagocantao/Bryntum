import FormTab from './FormTab.js';
import '../CalendarField.js';
import '../ConstraintTypePicker.js';
import '../../../Core/widget/DateField.js';
import '../../../Core/widget/Checkbox.js';
import '../SchedulingModePicker.js';
import { SchedulingMode } from '../../../Engine/scheduling/Types.js';

/**
 * @module SchedulerPro/widget/taskeditor/SchedulerAdvancedTab
 */

/**
 * Advanced task options for {@link SchedulerPro/widget/SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro/widget/GanttTaskEditor gantt task editor} tab.
 *
 * Contains the following fields by default (with their default weight):
 *
 * | Field ref                    | Type                                             | Weight | Description                                         |
 * |------------------------------|--------------------------------------------------|--------|-----------------------------------------------------|
 * | `calendarField`              | {@link SchedulerPro/widget/CalendarField}        | 100    | List of available calendars, if calendars are used  |
 * | `constraintTypeField`        | {@link SchedulerPro/widget/ConstraintTypePicker} | 200    | Shows a list of available constraints for this task |
 * | `constraintDateField`        | {@link Core/widget/DateField}                    | 300    | Shows a date for the selected constraint type       |
 * | `manuallyScheduledField`     | {@link Core/widget/Checkbox}                     | 400    | When checked, task is not considered in scheduling  |
 * | `schedulingModeField`        | {@link SchedulerPro/widget/SchedulingModePicker} | 450    | Shows a list of available scheduling modes for this event                     |
 * | `effortDrivenField`          | {@link Core/widget/Checkbox}                     | 460    | If checked, the effort of the event is kept intact when duration is provided. Works when scheduling mode is "Fixed Duration". |
 * | `inactiveField`              | {@link Core/widget/Checkbox}                     | 500    | Allows inactivating the task so it won't take part in the scheduling process. |
 * | `ignoreResourceCalendarField | {@link Core/widget/Checkbox}                     | 600    | If checked the event ignores the assigned resource calendars when scheduling |
 *
 * @extends SchedulerPro/widget/taskeditor/FormTab
 * @classtype scheduleradvancedtab
 */
export default class SchedulerAdvancedTab extends FormTab {

    static get $name() {
        return 'SchedulerAdvancedTab';
    }

    static get type() {
        return 'scheduleradvancedtab';
    }

    static get configurable() {
        return {
            cls : 'b-advanced-tab',

            tab : {
                icon    : 'b-icon-advanced',
                tooltip : 'L{SchedulerAdvancedTab.Advanced}'
            },

            defaults : {
                localeClass : this
            },

            items : {
                calendarField : {
                    type   : 'calendarfield',
                    name   : 'calendar',
                    label  : 'L{Calendar}',
                    weight : 100
                },
                constraintTypeField : {
                    type                         : 'constrainttypepicker',
                    name                         : 'constraintType',
                    label                        : 'L{Constraint type}',
                    clearable                    : true,
                    weight                       : 200,
                    includeAsapAlapAsConstraints : false
                },
                constraintDateField : {
                    type     : 'date',
                    name     : 'constraintDate',
                    label    : 'L{Constraint date}',
                    keepTime : 'entered',
                    weight   : 300
                },
                manuallyScheduledField : {
                    type   : 'checkbox',
                    name   : 'manuallyScheduled',
                    label  : 'L{Manually scheduled}',
                    weight : 400
                },
                schedulingModeField : {
                    type         : 'schedulingmodecombo',
                    name         : 'schedulingMode',
                    label        : 'L{Scheduling mode}',
                    hidden       : true,
                    weight       : 450,
                    allowedModes : `${SchedulingMode.Normal},${SchedulingMode.FixedDuration}`
                },
                effortDrivenField : {
                    type   : 'checkbox',
                    weight : 460,
                    name   : 'effortDriven',
                    label  : 'L{Effort driven}',
                    hidden : true,
                    cls    : 'b-half-width'
                },
                inactiveField : {
                    type   : 'checkbox',
                    weight : 500,
                    name   : 'inactive',
                    label  : 'L{Inactive}'
                },
                ignoreResourceCalendarField : {
                    type   : 'checkbox',
                    weight : 600,
                    name   : 'ignoreResourceCalendar',
                    label  : 'L{Ignore resource calendar}',
                    cls    : 'b-half-width'
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

    get schedulingModeField() {
        return this.widgetMap.schedulingModeField;
    }

    loadEvent(eventRecord) {
        const
            me          = this,
            { project } = me,
            firstLoad   = !me.record,
            {
                calendarField,
                constraintTypeField
            }           = me;



        if (constraintTypeField) {
            constraintTypeField.taskRecord = eventRecord;
        }
        if (calendarField) {
            calendarField.store = project.calendarManagerStore;
            calendarField.hidden = !project.calendarManagerStore.count;
        }

        super.loadEvent(...arguments);
    }
}

SchedulerAdvancedTab.initClass();
