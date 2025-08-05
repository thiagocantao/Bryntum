import FormTab from './FormTab.js';
import '../CalendarField.js';
import '../ConstraintTypePicker.js';
import '../../../Core/widget/DateField.js';
import '../../../Core/widget/Checkbox.js';
import '../SchedulingModePicker.js';

/**
 * @module SchedulerPro/widget/taskeditor/SchedulerAdvancedTab
 */

/**
 * Advanced task options for {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} or
 * {@link SchedulerPro.widget.GanttTaskEditor gantt task editor} tab.
 *
 * Contains the following fields by default (with their default weight):
 *
 * | Field ref                | Type      | Weight | Description                                                                             |
 * |--------------------------|-----------|--------|-----------------------------------------------------------------------------------------|
 * | `calendarField`          | Combo     | 100    | Shows a list of available calendars for this task. Shown when calendars are downloaded. |
 * | `constraintTypeField`    | Combo     | 200    | Shows a list of available constraints for this task                                     |
 * | `constraintDateField`    | DateField | 300    | Shows a date for the selected constraint type                                           |
 * | `manuallyScheduledField` | Checkbox  | 400    | If checked, the task is not considered in scheduling                                    |
 *
 * @extends  SchedulerPro/widget/taskeditor/FormTab
 * @classtype scheduleradvancedtab
 */
export default class SchedulerAdvancedTab extends FormTab {

    static get $name() {
        return 'SchedulerAdvancedTab';
    }

    static get type() {
        return 'scheduleradvancedtab';
    }

    static get defaultConfig() {
        return {
            title : '<i class="b-icon b-icon-advanced" data-btip="L{Advanced}"></i>',
            cls   : 'b-advanced-tab',

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
                    type      : 'constrainttypepicker',
                    name      : 'constraintType',
                    label     : 'L{Constraint type}',
                    clearable : true,
                    weight    : 200
                },
                constraintDateField : {
                    type   : 'date',
                    name   : 'constraintDate',
                    label  : 'L{Constraint date}',
                    weight : 300
                },
                manuallyScheduledField : {
                    type   : 'checkbox',
                    name   : 'manuallyScheduled',
                    label  : 'L{Manually scheduled}',
                    weight : 400
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

    get manuallyScheduledField() {
        return this.widgetMap.manuallyScheduledField;
    }

    loadEvent(eventRecord) {
        const
            me                = this,
            firstLoad         = !me.record,
            { calendarField } = me;

        //<debug>
        console.assert(
            firstLoad || me.project === eventRecord.project,
            'Loading of a record from another project is not currently supported!'
        );
        //</debug>

        if (calendarField) {
            calendarField.store = eventRecord.project.calendarManagerStore;
            calendarField.hidden = !eventRecord.project.calendarManagerStore.count;
        }

        super.loadEvent(...arguments);
    }
}

SchedulerAdvancedTab.initClass();
