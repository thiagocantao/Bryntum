import Combo from '../../Core/widget/Combo.js';
import PickerField from '../../Core/widget/PickerField.js';
import AssignmentPicker from './AssignmentPicker.js';
import AssignmentsManipulationStore from '../data/AssignmentsManipulationStore.js';

/**
 * @module Gantt/widget/AssignmentField
 */

/**
 * Special field class to edit single event assignments.
 *
 * This field is used as the default editor for the {@link Gantt.column.ResourceAssignmentColumn ResourceAssignmentColumn}
 *
 * {@inlineexample gantt/widget/AssignmentField.js}
 * @extends Core/widget/Combo
 * @classType 'assignmentfield'
 */
export default class AssignmentField extends Combo {

    static get $name() {
        return 'AssignmentField';
    }

    // Factoryable type name
    static get type() {
        return 'assignmentfield';
    }

    //region Config
    static get configurable() {
        return {
            chipView : {
                cls : 'b-assignment-chipview',
                itemTpl(assignment, i) {
                    return `${assignment.name} ${assignment.units}%`;
                },
                scrollable : {
                    overflowX : 'hidden-scroll'
                }
            },

            triggers : {
                expand : {
                    cls     : 'b-icon-down',
                    handler : 'onTriggerClick'
                }
            },

            multiSelect : true,
            clearable   : false, // TODO: change when it's back to editable
            editable    : false,
            value       : null,

            /**
             * A config object used to configure the {@link Gantt.widget.AssignmentGrid assignment grid}
             * used to select resources to assign.
             *
             * Any `columns` provided are concatenated onto the default column set.
             * @config {Object|Gantt.widget.AssignmentGrid} picker
             */
            picker : {
                type         : AssignmentPicker.type,
                floating     : true,
                scrollAction : 'realign'
            },

            /**
             * Width of picker, defaults to this field's {@link Core/widget/PickerField#config-pickerAlignElement} width
             *
             * @config {Number}
             */
            pickerWidth : null,

            /**
             * Event to load resource assignments for.
             * Either event or {@link #config-store store} should be given.
             *
             * @config {Gantt.model.TaskModel}
             */
            projectEvent : null,

            /**
             * Assignment manipulation store to use or it's configuration object.
             * Either store or {@link #config-projectEvent projectEvent} should be given
             *
             * @config {Core.data.Store|Object}
             */
            store : {}
        };
    }
    //endregion

    // Any change must offer the save/cancel UI since THAT is what actually makes the edit
    onChipClose(records) {
        this.showPicker();

        // Showing the picker recreates the AssignmentsManipulationModels, so we
        // must find the corresponding new version to deassign.
        records.forEach(record => this.picker.deselectRow(record));
    }

    syncInputFieldValue() {
        super.syncInputFieldValue();

        if (this.store) {
            this.tooltip = this.store.toValueString();
        }
    }

    //region Picker

    // Override. This field does not have a primary filter, so
    // down arrow/trigger click should just show the picker.
    onTriggerClick(event) {
        if (this.pickerVisible) {
            this.hidePicker();
        }
        else {
            PickerField.prototype.showPicker.call(this, event && ('key' in event));
        }
    }

    focusPicker() {
        this.picker.focus();
    }

    changePicker(picker, oldPicker) {
        const me = this;

        return AssignmentPicker.reconfigure(oldPicker, picker, {
            owner    : me,
            defaults : {
                projectEvent : me.projectEvent,
                store        : me.store,
                readOnly     : me.readOnly,
                owner        : me,
                forElement   : me[me.pickerAlignElement],
                assignments  : me.valueCollection,

                align : {
                    anchor : me.overlayAnchor,
                    target : me[me.pickerAlignElement]
                }
            }
        });
    }

    //endregion

    //region Value

    changeProjectEvent(projectEvent) {
        // NOTE: This kind of thing would normally be handled in updateProjectEvent, however, the setter of the
        //  AssignmentManipulationStore pulls double duty and resyncs some fields, even if presented with the same
        //  projectEvent.
        const { picker, store } = this;
        this._projectEvent = projectEvent;
        this._projectEventGeneration = projectEvent.generation;

        if (store) {
            store.projectEvent = projectEvent;
        }

        if (picker) {
            picker.projectEvent = projectEvent;
        }

        return projectEvent;
    }

    changeStore(store) {
        if (store && !(store instanceof AssignmentsManipulationStore)) {
            store = new AssignmentsManipulationStore(store);
        }

        return store;
    }

    updateStore(store) {
        const me = this;

        me.detachListeners('storeMutation');

        if (store instanceof AssignmentsManipulationStore) {
            const { projectEvent } = store;

            if (projectEvent) {
                me.projectEvent = projectEvent;
            }
            else {
                // This is to not do the store::fillFromMaster() call, otherwise editor will be unhappy
                store.projectEvent = me.projectEvent;
            }
        }

        store.on({
            name    : 'storeMutation',
            change  : 'syncInputFieldValue',
            thisObj : me
        });
    }

    // The AssignmentPicker's "Save" button applies the change to the task
    // being edited immediately. The field's value has no bearing and
    // the Editor's completeEdit will find no change upon complete.
    get value() {}

    set value(v) {}
    //endregion

    // Override. Picker is completely self-contained. Prevent any
    // field action on its key events.
    onPickerKeyDown(event) {
        const grid = this.picker;

        // Move "down" into the grid body
        if (event.key === 'ArrowDown' && event.target.compareDocumentPosition(grid.bodyContainer) === document.DOCUMENT_POSITION_FOLLOWING) {
            grid.element.focus();
        }
    }
}

// Register this widget type with its Factory
AssignmentField.initClass();
