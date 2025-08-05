import FieldFilterPickerGroup from '../../Core/widget/FieldFilterPickerGroup.js';
import './GridFieldFilterPicker.js';
import '../../Core/widget/Checkbox.js';
import '../../Core/widget/Label.js';

/**
 * @module Grid/widget/GridFieldFilterPickerGroup
 */

/**
 * Extends {@link Core.widget.FieldFilterPickerGroup} to allow providing a {@link Grid.view.Grid} from which
 * available fields will be read. This is useful when a grid is already configured with a set of columns
 * containing display names and type information.
 *
 * The grid should have a {@link Grid.data.ColumnStore} configured (see {@link Grid.view.Grid#config-columns})
 * and a {@link Core.data.Store} whose {@link Core.data.Store#property-modelClass} contains fields with
 * specific data types.
 *
 * Optionally, you can also use {@link #config-allowedFieldNames} to restrict the set of fields shown in the
 * widget.
 *
 * For example:
 *
 * ```javascript
 * new GridFieldFilterPickerGroup({
 *     appendTo : domElement,
 *
 *     grid : myGrid,
 *
 *     filters : [{
 *         property : 'startDate',
 *         operator : '<=',
 *         value    : new Date()
 *     }]
 * });
 * ```
 *
 * @classtype gridfieldfilterpickergroup
 * @extends Core/widget/FieldFilterPickerGroup
 * @demo Grid/fieldfilters
 * @widget
 */
export default class GridFieldFilterPickerGroup extends FieldFilterPickerGroup {
    //region Config
    static get $name() {
        return 'GridFieldFilterPickerGroup';
    }

    // Factoryable type name
    static get type() {
        return 'gridfieldfilterpickergroup';
    }

    /** @hideconfigs fields, store */

    static configurable = {
        /**
         * {@link Grid.view.Grid} from which to read the available field list. In order to
         * appear as a selectable property for a filter, a column must have a `field` property.
         * If the column has a `text` property, that will be shown as the displayed text in the
         * selector; otherwise, the `field` property will be shown as-is.
         *
         * The grid's {@link Core.data.Store}'s {@link Core.data.Store#property-modelClass} will be
         * examined to find field data types.
         *
         * You can limit available fields to a subset of the grid's columns using the
         * {@link #config-allowedFieldNames} configuration property.
         *
         * @config {Grid.view.Grid}
         */
        grid : null,

        /**
         * Optional array of field names that are allowed as selectable properties for filters.
         * This should be a subset of the field names found in the {@link #config-grid}'s store. When supplied,
         * only the named fields will be shown in the property selector combo.
         *
         * @config {String[]}
         */
        allowedFieldNames : null
    };

    //endregion

    static childPickerType = 'gridfieldfilterpicker';

    validateConfig() {
        if (!this.grid) {
            throw new Error(`${this.constructor.$name} requires the 'grid' config property.`);
        }
    }

    getFilterPickerConfig(filter) {
        const { grid, allowedFieldNames } = this;
        return {
            ...super.getFilterPickerConfig(filter),
            grid,
            allowedFieldNames
        };
    }

    updateGrid(newGrid) {
        this.store = this.grid.store;
    }

    /**
     * @private
     */
    canManage(filter) {
        const me = this;
        return super.canManage(filter) && (!me.allowedFieldNames || me.allowedFieldNames.includes(filter.property));
    }

}

GridFieldFilterPickerGroup.initClass();
