import FieldFilterPicker, { SUPPORTED_FIELD_DATA_TYPES, isSupportedDurationField } from '../../Core/widget/FieldFilterPicker.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Model from '../../Core/data/Model.js';
import '../../Core/widget/Combo.js';
import '../../Core/widget/Checkbox.js';
import '../../Core/widget/NumberField.js';
import '../../Core/widget/TextField.js';
import '../../Core/widget/DateField.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Grid/widget/GridFieldFilterPicker
 */

/**
 * Subclass of {@link Core.widget.FieldFilterPicker} allowing configuration using an
 * existing {@link Grid.view.Grid}.
 *
 * See also {@link Grid.widget.GridFieldFilterPickerGroup}.
 *
 * @extends Core/widget/FieldFilterPicker
 * @classtype gridfieldfilterpicker
 * @demo Grid/fieldfilters
 * @widget
 */
export default class GridFieldFilterPicker extends FieldFilterPicker {

    //region Config
    static get $name() {
        return 'GridFieldFilterPicker';
    }

    // Factoryable type name
    static get type() {
        return 'gridfieldfilterpicker';
    }

    /** @hideconfigs store */

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
         * This is a subset of the field names found in the {@link #config-grid}'s columns. When supplied, only
         * the named fields will be shown in the property selector combo.
         *
         * Note that field names are case-sensitive and should match the data field name in the store
         * model.
         *
         * @config {String[]}
         */
        allowedFieldNames : null
    };

    //endregion

    afterConstruct() {
        const
            me = this;
        if (!me.grid) {
            throw new Error(`${me.constructor.$name} requires 'grid' to be configured.`);
        }
        me.fields = me.fields ?? {};  // Force `fields` changer if fields is left null, to merge w/ grid fields
        super.afterConstruct();
    }

    updateGrid(newGrid) {
        if (!newGrid.store?.modelClass) {
            throw new Error(`Grid does not have a store with a modelClass defined.`);
        }
        if (!newGrid.columns) {
            throw new Error(`Grid does not have a column store.`);
        }
    }

    /**
     * Returns a subset of the fields defined on the model class, excluding those considered internal or otherwise not
     * suitable for user-facing filtering.
     * @param {Core.data.Model} modelClass The Model subclass whose fields will be read
     * @returns {Core.data.field.DataField[]}
     * @private
     */
    static getModelClassFields(modelClass) {
        const ownFieldNames = new Set(modelClass.fields.map(({ name }) => name));
        return modelClass?.allFields
            .filter(field =>
                !field.internal &&
                (
                    SUPPORTED_FIELD_DATA_TYPES.includes(field.type) ||
                    isSupportedDurationField(field)
                ) &&
                (field.definedBy !== Model || ownFieldNames.has(field.name))
            ) || [];
    }

    /**
     * Gets the filterable fields backing any of the configured `grid`'s columns, for those columns for which
     * it is possible to do so.
     * @private
     * @returns {Object} Filterable fields dictionary of the form { [fieldName]: { title, type } }
     */
    static getColumnFields(columnStore, modelClass, allowedFieldNames) {
        const
            modelFields = ArrayHelper.keyBy(GridFieldFilterPicker.getModelClassFields(modelClass), 'name'),
            allowedNameSet = allowedFieldNames && new Set(allowedFieldNames);
        return Object.fromEntries(
            columnStore?.records
                .filter(({ field }) => field &&
                    modelFields[field] &&
                    (!allowedNameSet || allowedNameSet.has(field)))
                .map(({ field, text }) => [
                    field,
                    {
                        title : text || field,
                        type  : isSupportedDurationField(modelFields[field]) ? 'duration' : modelFields[field].type
                    }
                ]) ??
            []);
    }

    changeFields(newFields) {
        let localFields = newFields;
        if (Array.isArray(newFields)) {
            VersionHelper.deprecate('Core', '6.0.0', 'FieldOption[] deprecated, use Object<String, FieldOption[]> keyed by field name instead');
            // Support old array syntax for `fields` during deprecation
            localFields = ArrayHelper.keyBy(localFields, 'name');
        }
        return ObjectHelper.merge(
            {},
            GridFieldFilterPicker.getColumnFields(this.grid.columns,
                this.grid.store?.modelClass, this.allowedFieldNames),
            localFields
        );
    }
}

GridFieldFilterPicker.initClass();
