import AssignmentGrid from './AssignmentGrid.js';
import '../../Core/widget/Container.js';
import '../../Core/widget/Button.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Gantt/widget/AssignmentPicker
 */

/**
 * Class for assignment field dropdown, wraps {@link Gantt/widget/AssignmentGrid} within a frame and adds two buttons: Save and Cancel
 * @private
 */
export default class AssignmentPicker extends AssignmentGrid {

    static get $name() {
        return 'AssignmentPicker';
    }

    // Factoryable type name
    static get type() {
        return 'assignmentpicker';
    }

    static get defaultConfig() {
        return {
            /**
             * A config object used to modify the {@link Gantt.widget.AssignmentGrid assignment grid}
             * used to select resources to assign.
             *
             * This config is merged with the configuration of the picker's assignment grid, so features
             * can be added (or default features removed by using `featureName : false`).
             *
             * Any `columns` provided are concatenated onto the default column set.
             * @deprecated 5.0 AssignmentPicker *is* a grid so configure this class directly
             * @config {Object} [grid]
             */
            focusable : true,
            trapFocus : true,
            height    : '20em',
            minWidth  : '25em',
            bbar      : [
                {
                    type        : 'button',
                    text        : this.L('L{Object.Save}'),
                    localeClass : this,
                    ref         : 'saveBtn',
                    color       : 'b-green'
                },
                {
                    type        : 'button',
                    text        : this.L('L{Object.Cancel}'),
                    localeClass : this,
                    ref         : 'cancelBtn',
                    color       : 'b-gray'
                }
            ],
            /**
             * The Event to load resource assignments for.
             * Either an Event or {@link #config-store store} should be given.
             *
             * @config {Gantt.model.TaskModel}
             */
            projectEvent : null,

            /**
             * Store for the picker.
             * Either store or {@link #config-projectEvent projectEvent} should be given
             *
             * @config {Gantt.data.AssignmentsManipulationStore}
             */
            store : null
        };
    }

    configure(config) {
        let gridExtraConfig = config.grid;

        if (gridExtraConfig) {
            VersionHelper.deprecate('Gantt', '5.0.0', 'The `grid` config is deprecated, you can now set the config properties directly on this AssignmentPicker since it *is* the Grid');
            // Columns is an array, and won't merge, so concat it before.
            if (gridExtraConfig.columns) {
                config.columns = gridExtraConfig.columns || [];

                // We must not mutate config objects owned by outside classes.
                gridExtraConfig = Object.assign({}, gridExtraConfig);
                delete gridExtraConfig.columns;
            }

            // Merge the AssignmentField's grid config into the default.
            ObjectHelper.merge(config, gridExtraConfig);
        }

        config.selectedRecordCollection = config.assignments;

        super.configure(config);
    }

    afterConfigure() {
        const me = this;

        super.afterConfigure();

        me.bbar.widgetMap.saveBtn?.on('click', me.onSaveClick, me);
        me.bbar.widgetMap.cancelBtn?.on('click', me.onCancelClick, me);
    }

    // Override, default focus to the filter field
    get focusElement() {
        return this.element.querySelector('.b-filter-bar-field-input');
    }

    //region Event handlers
    onSaveClick() {
        this.store.applyChanges();
        this.hide();
    }

    onCancelClick() {
        this.hide();
    }
    //endregion
}

// Register this widget type with its Factory
AssignmentPicker.initClass();
