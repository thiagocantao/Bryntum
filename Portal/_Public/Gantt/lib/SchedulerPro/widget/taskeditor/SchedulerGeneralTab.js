import FormTab from './FormTab.js';
import DateHelper from '../../../Core/helper/DateHelper.js';
import '../CalendarField.js';
import '../../../Core/widget/Checkbox.js';
import '../../../Core/widget/DateTimeField.js';
import '../StartDateField.js';
import '../EndDateField.js';
import '../../../Scheduler/widget/EventColorField.js';

/**
 * @module SchedulerPro/widget/taskeditor/SchedulerGeneralTab
 */

/**
 * A tab inside the {@link SchedulerPro.widget.SchedulerTaskEditor scheduler task editor} showing the general
 * information for an event from a simplified scheduler project.
 *
 * Contains the following fields by default:
 *
 * | Field ref          | Type                                     | Text       | Weight | Description                                                         |
 * |--------------------|------------------------------------------|------------|--------|---------------------------------------------------------------------|
 * | `nameField`        | {@link Core.widget.TextField}            | Name       | 100    | Task name                                                           |
 * | `resourcesField`   | {@link Core.widget.Combo}                | Resources  | 200    | Shows a list of available resources for this task                   |
 * | `startDateField`   | {@link Core.widget.DateTimeField}        | Start      | 300    | Shows when the task begins                                          |
 * | `endDateField`     | {@link Core.widget.DateTimeField}        | Finish     | 400    | Shows when the task ends                                            |
 * | `durationField`    | {@link Core.widget.DurationField}        | Duration   | 500    | Shows how long the task is                                          |
 * | `percentDoneField` | {@link Core.widget.NumberField}          | % Complete | 600    | Shows what part of task is done already in percentage               |
 * | `effort`           | {@link SchedulerPro.widget.EffortField}  | Effort     | 620    | Shows how much working time is required to complete the whole event |
 * | `preambleField`    | {@link Core.widget.DurationField}        | Preamble   | 650    | Shows preamble time (task preparation time)                         |
 * | `postambleField`   | {@link Core.widget.DurationField}        | Postamble  | 660    | Shows postamble time (clean up after the task)                      |
 * | `colorField` ¹     | {@link Scheduler.widget.EventColorField} | Color ¹    | 700    | Choose background color for the task bar                            |
 *
 * **¹** Set the {@link SchedulerPro.view.SchedulerProBase#config-showTaskColorPickers} config to `true` to enable this field
 *
 * To customize the tab or its fields:
 *
 * ```javascript
 * features : {
 *     taskEdit : {
 *         items : {
 *             generalTab : {
 *                 // Custom title
 *                 title: 'Common',
 *                 // Customized items
 *                 items : {
 *                     // Hide the duration field
 *                     durationField : null,
 *                     // Customize the name field
 *                     nameField : {
 *                         label : 'Title'
 *                     },
 *                     // Add a custom field
 *                     colorField : {
 *                         type   : 'text',
 *                         label  : 'Color',
 *                         // name maps to a field on the event record
 *                         name   : 'eventColor',
 *                         // place at top
 *                         weight : 0
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * }
 * ```
 *
 * @extends SchedulerPro/widget/taskeditor/FormTab
 * @classtype schedulergeneraltab
 */
export default class SchedulerGeneralTab extends FormTab {
    static get $name() {
        return 'SchedulerGeneralTab';
    }

    // Factoryable type name
    static get type() {
        return 'schedulergeneraltab';
    }

    static get defaultConfig() {
        return {
            title : 'L{General}',
            cls   : 'b-general-tab',

            defaults : {
                localeClass : this,
                // New fields at the end
                weight      : 10
            },

            items : {
                nameField : {
                    type      : 'text',
                    required  : true,
                    label     : 'L{Name}',
                    clearable : true,
                    name      : 'name',
                    cls       : 'b-name',
                    weight    : 100
                },

                resourcesField : {
                    type                    : 'combo',
                    label                   : 'L{Resources}',
                    name                    : 'resources',
                    editable                : true,
                    valueField              : 'id',
                    displayField            : 'name',
                    highlightExternalChange : false,
                    cls                     : 'b-resources',
                    weight                  : 200
                },
                startDateField : {
                    type      : 'datetime',
                    label     : 'L{Start}',
                    name      : 'startDate',
                    cls       : 'b-start-date',
                    flex      : '1 0 100%',
                    weight    : 300,
                    dateField : {
                        type : 'startdatefield'
                    }
                },
                endDateField : {
                    type      : 'datetime',
                    label     : 'L{Finish}',
                    name      : 'endDate',
                    cls       : 'b-end-date',
                    flex      : '1 0 100%',
                    weight    : 400,
                    dateField : {
                        type : 'enddatefield'
                    }
                },
                durationField : {
                    type   : 'durationfield',
                    label  : 'L{Duration}',
                    name   : 'fullDuration',
                    cls    : 'b-half-width',
                    min    : 0,
                    weight : 500
                },
                percentDoneField : {
                    type   : 'number',
                    label  : 'L{% complete}',
                    name   : 'renderedPercentDone',
                    cls    : 'b-half-width',
                    min    : 0,
                    max    : 100,
                    weight : 600
                },
                effortField : {
                    type   : 'durationfield',
                    label  : 'L{Effort}',
                    name   : 'fullEffort',
                    weight : 620
                },
                preambleField : {
                    type            : 'durationfield',
                    hidden          : true,
                    useAbbreviation : true,
                    weight          : 650,
                    label           : 'L{SchedulerGeneralTab.Preamble}',
                    name            : 'preamble',
                    cls             : 'b-half-width'
                },
                postambleField : {
                    type            : 'durationfield',
                    hidden          : true,
                    useAbbreviation : true,
                    weight          : 660,
                    label           : 'L{SchedulerGeneralTab.Postamble}',
                    name            : 'postamble',
                    cls             : 'b-half-width'
                },
                colorField : {
                    type   : 'eventcolorfield',
                    weight : 700,
                    label  : 'L{SchedulerBase.color}',
                    name   : 'eventColor'
                }
            }
        };
    }

    loadEvent(record) {
        const
            me                    = this,
            { project }           = me,
            step                  = {
                unit      : record.durationUnit,
                magnitude : 1
            },
            { isParent } = record,
            {
                durationField,
                percentDoneField,
                startDateField,
                endDateField,
                resourcesField
            }                     = me.widgetMap,
            storeChange           = resourcesField?.store.masterStore !== project.resourceStore;



        // Editing duration, percentDone & endDate disallowed for parent tasks
        if (durationField) {
            durationField.disabled = isParent;
        }

        if (percentDoneField) {
            percentDoneField.disabled = isParent;
        }

        if (startDateField) {
            startDateField.dateField.eventRecord = record;
            if (DateHelper.compareUnits(step.unit, 'hour') > 0) {
                startDateField.dateField.step = step;
            }
            else {
                startDateField.timeField.step = step;
            }
        }

        if (endDateField) {
            endDateField.dateField.eventRecord = record;
            if (DateHelper.compareUnits(step.unit, 'hour') > 0) {
                endDateField.dateField.step = step;
            }
            else {
                endDateField.timeField.step = step;
            }
            endDateField.disabled = isParent;
        }

        if (resourcesField) {
            resourcesField.multiSelect = resourcesField.config.multiSelect ?? !project.eventStore.usesSingleAssignment;



            if (storeChange) {
                // Can't use store directly since it may be grouped and then contains irrelevant group records
                resourcesField.store = project.resourceStore.chain(record => !record.isSpecialRow);
            }
            else {
                resourcesField.store.resumeChain();
            }
        }

        super.loadEvent(...arguments);
    }

    onFieldChange({ source, valid, userAction, value }) {
        if (userAction && valid) {
            const
                { eventStore }     = this.record,
                resourceUnassigned = source.name === 'resources' && value.length === 0 && this.autoUpdateRecord && eventStore.removeUnassignedEvent;

            if (resourceUnassigned) {
                // Do not remove unassigned event if all resources are removed, we will do it after
                eventStore.removeUnassignedEvent = false;
            }
            super.onFieldChange(...arguments);

            if (resourceUnassigned) {
                eventStore.removeUnassignedEvent = true;
            }
        }
    }

    beforeSave() {
        // We skipped removing event on field change, if resource is still empty before save - remove record
        if (this.record.resources.length === 0 && this.record.eventStore.removeUnassignedEvent) {
            this.record.remove();
        }

        this.widgetMap.resourcesField?.store.suspendChain();

        super.beforeSave();
    }
}

// Register this widget type with its Factory
SchedulerGeneralTab.initClass();
