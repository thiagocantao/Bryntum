import DomHelper from '../../Core/helper/DomHelper.js';
import WidgetHelper from '../../Core/helper/WidgetHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Tooltip from '../../Core/widget/Tooltip.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import CollectionFilter from '../../Core/util/CollectionFilter.js';
import '../../Core/widget/NumberField.js';
import '../../Core/widget/Combo.js';
import '../../Core/widget/DateField.js';
import '../../Core/widget/TimeField.js';
import GridFeatureManager from './GridFeatureManager.js';
import '../widget/GridFieldFilterPickerGroup.js';



/**
 * @module Grid/feature/Filter
 */

export const fieldTypeMap = {
    date     : 'date',
    int      : 'number',
    integer  : 'number',
    number   : 'number',
    string   : 'text',
    duration : 'duration'
};

/**
 * Feature that allows filtering of the grid by settings filters on columns. The actual filtering is done by the store.
 * For info on programmatically handling filters, see {@link Core.data.mixin.StoreFilter}.
 *
 * {@inlineexample Grid/feature/Filter.js}
 *
 * ```javascript
 * // Filtering turned on but no default filter
 * const grid = new Grid({
 *   features : {
 *     filter : true
 *   }
 * });
 *
 * // Using default filter
 * const grid = new Grid({
 *   features : {
 *     filter : { property : 'city', value : 'Gavle' }
 *   }
 * });
 * ```
 *
 * A column can supply a custom filtering function as its {@link Grid.column.Column#config-filterable} config. When
 * filtering by that column using the UI that function will be used to determine which records to include. See
 * {@link Grid.column.Column#config-filterable Column#filterable} for more information.
 *
 * ```javascript
 * // Custom filtering function for a column
 * const grid = new Grid({
 *    features : {
 *        filter : true
 *    },
 *
 *    columns: [
 *        {
 *          field      : 'age',
 *          text       : 'Age',
 *          type       : 'number',
 *          // Custom filtering function that checks "greater than" no matter
 *          // which field user filled in :)
 *          filterable : ({ record, value, operator }) => record.age > value
 *        }
 *    ]
 * });
 * ```
 *
 * If this feature is configured with `prioritizeColumns : true`, those functions will also be used when filtering
 * programmatically:
 *
 * ```javascript
 * const grid = new Grid({
 *    features : {
 *        filter : {
 *            prioritizeColumns : true
 *        }
 *    },
 *
 *    columns: [
 *        {
 *          field      : 'age',
 *          text       : 'Age',
 *          type       : 'number',
 *          filterable : ({ record, value, operator }) => record.age > value
 *        }
 *    ]
 * });
 *
 * // Because of the prioritizeColumns config above, any custom filterable function
 * // on a column will be used when programmatically filtering by that columns field
 * grid.store.filter({
 *     property : 'age',
 *     value    : 41
 * });
 * ```
 *
 * You can supply a field config to use for the filtering field displayed for string type columns:
 *
 * ```javascript
 * // For string-type columns you can also replace the filter UI with a custom field:
 * columns: [
 *     {
 *         field : 'city',
 *         // Filtering for a value out of a list of values
 *         filterable: {
 *             filterField : {
 *                 type  : 'combo',
 *                 items : [
 *                     'Paris',
 *                     'Dubai',
 *                     'Moscow',
 *                     'London',
 *                     'New York'
 *                 ]
 *             }
 *         }
 *     }
 * ]
 * ```
 *
 * You can also change default fields, for example this will use {@link Core.widget.DateTimeField} in filter popup:
 * ```javascript
 * columns : [
 *     {
 *         type       : 'date',
 *         field      : 'start',
 *         filterable : {
 *             filterField : {
 *                 type : 'datetime'
 *             }
 *         }
 *     }
 * ]
 * ```
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * **Note:** This feature cannot be used together with {@link Grid.feature.FilterBar} feature, they are
 * mutually exclusive.
 *
 * ## Keyboard shortcuts
 * This feature has the following default keyboard shortcuts:
 *
 * | Keys   | Action                  | Action description                                                     |
 * |--------|-------------------------|------------------------------------------------------------------------|
 * | `F`    | *showFilterEditorByKey* | When the column header is focused, this shows the filter input field   |
 *
 * <div class="note">Please note that <code>Ctrl</code> is the equivalent to <code>Command</code> and <code>Alt</code>
 * is the equivalent to <code>Option</code> for Mac users</div>
 *
 * For more information on how to customize keyboard shortcuts, please see
 * [our guide](#Grid/guides/customization/keymap.md).
 *
 * To enable an alternative UI that uses {@link Core.widget.FieldFilterPickerGroup} to allow
 * specifying multiple filters on the column at once, set `isMulti` to `true`.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Grid/filtering
 * @classtype filter
 * @feature
 */
export default class Filter extends InstancePlugin {
    //region Init

    static get $name() {
        return 'Filter';
    }

    static get configurable() {
        return {
            /**
             * Use custom filtering functions defined on columns also when programmatically filtering by the columns
             * field.
             *
             * ```javascript
             * const grid = new Grid({
             *     columns : [
             *         {
             *             field : 'age',
             *             text : 'Age',
             *             filterable({ record, value }) {
             *               // Custom filtering, return true/false
             *             }
             *         }
             *     ],
             *
             *     features : {
             *         filter : {
             *             prioritizeColumns : true // <--
             *         }
             *     }
             * });
             *
             * // Because of the prioritizeColumns config above, any custom
             * // filterable function on a column will be used when
             * // programmatically filtering by that columns field
             * grid.store.filter({
             *     property : 'age',
             *     value    : 30
             * });
             * ```
             *
             * @config {Boolean}
             * @default
             * @category Common
             */
            prioritizeColumns : false,

            /**
             * See {@link #keyboard-shortcuts Keyboard shortcuts} for details
             * @config {Object<String,String>}
             */
            keyMap : {
                f : 'showFilterEditorByKey'
            },

            /**
             * Use {@link Grid.widget.GridFieldFilterPickerGroup} instead of the normal UI,
             * enabling multiple filters for the same column. To enable the multi-filter UI,
             * set `isMulti` to either `true` or a {@link Grid.widget.GridFieldFilterPickerGroup}
             * configuration object.
             *
             * @config {Boolean|GridFieldFilterPickerGroupConfig}
             * @default
             * @category Common
             */
            isMulti : false
        };
    }

    construct(grid, config) {
        if (grid.features.filterBar) {
            throw new Error('Grid.feature.Filter feature may not be used together with Grid.feature.FilterBar. These features are mutually exclusive.');
        }

        const me = this;

        me.grid = grid;
        me.closeFilterEditor = me.closeFilterEditor.bind(me);

        super.construct(grid, config);

        me.bindStore(grid.store);

        if (config && typeof config === 'object') {
            const clone = ObjectHelper.clone(config);

            // Feature accepts a filter config object, need to remove this config
            delete clone.prioritizeColumns;
            delete clone.isMulti;
            delete clone.dateFormat;

            if (!ObjectHelper.isEmpty(clone)) {
                grid.store.filter(clone, null, grid.isConfiguring);
            }
        }
    }

    doDestroy() {
        this.filterTip?.destroy();
        this.filterEditorPopup?.destroy();

        super.doDestroy();
    }

    get store() {
        return this.grid.store;
    }

    bindStore(store) {
        this.detachListeners('store');

        store.ion({
            name         : 'store',
            beforeFilter : 'onStoreBeforeFilter',
            filter       : 'onStoreFilter',
            thisObj      : this
        });

        if (this.client.isPainted) {
            this.refreshHeaders(false);
        }
    }

    //endregion

    //region Plugin config

    // Plugin configuration. This plugin chains some of the functions in Grid.
    static get pluginConfig() {
        return {
            chain : ['renderHeader', 'populateCellMenu', 'populateHeaderMenu', 'onElementClick', 'bindStore']
        };
    }

    //endregion

    //region Refresh headers

    /**
     * Update headers to match stores filters. Called on store load and grid header render.
     * @param reRenderRows Also refresh rows?
     * @private
     */
    refreshHeaders(reRenderRows) {
        const
            me      = this,
            grid    = me.grid,
            element = grid.headerContainer;

        if (element) {
            // remove .latest from all filters, will be applied to actual latest
            DomHelper.children(element, '.b-filter-icon.b-latest').forEach(iconElement => iconElement.classList.remove('b-latest'));

            if (!me.filterTip) {
                me.filterTip = new Tooltip({
                    forElement  : element,
                    forSelector : '.b-filter-icon',
                    getHtml({ activeTarget }) {
                        return activeTarget.dataset.filterText;
                    }
                });
            }

            if (!grid.store.isFiltered) {
                me.filterTip.hide();
            }

            grid.columns.visibleColumns.forEach(column => {
                if (column.filterable !== false) {
                    const
                        columnFilters    = me.store.filters.allValues.filter(({ property, disabled, internal }) =>
                            property === column.field && !disabled && !internal),
                        isColumnFiltered = columnFilters.length > 0,
                        headerEl = column.element;

                    if (headerEl) {
                        const textEl = column.textWrapper;

                        let filterIconEl = textEl?.querySelector('.b-filter-icon'),
                            filterText;

                        if (isColumnFiltered) {
                            const bullet = '&#x2022 ';
                            filterText = `${me.L('L{filter}')}: ` +
                                (columnFilters.length > 1 ? '<br/><br/>' : '') +
                                columnFilters.map(columnFilter => {
                                    let value = columnFilter.value ?? '';
                                    const
                                        isArray = Array.isArray(value),
                                        relation = me.store?.modelRelations?.find(
                                            ({ foreignKey }) => foreignKey === columnFilter.property);

                                    if (columnFilter.displayValue) {
                                        value = columnFilter.displayValue;
                                    }
                                    else {
                                        if (me.isMulti && relation) {
                                            // Look up remote display value per filterable-field config (FieldFilterPicker.js#FieldOption)
                                            const { relatedDisplayField } = me.isMulti.fields?.[columnFilter.property];
                                            if (relatedDisplayField) {
                                                const getDisplayValue = foreignId => relation.foreignStore.getById(foreignId)?.[relatedDisplayField];
                                                if (isArray) {
                                                    value = value.map(getDisplayValue)
                                                        .sort((a, b) => (a ?? '').localeCompare(b ?? ''));
                                                }
                                                else {
                                                    value = getDisplayValue(value);
                                                }
                                            }
                                        }
                                        else if (column.formatValue && value) {
                                            value = isArray
                                                ? value.map(val => column.formatValue(val))
                                                : column.formatValue(value);
                                        }

                                        if (isArray) {
                                            value = `[ ${value.join(', ')} ]`;
                                        }
                                    }

                                    return (columnFilters.length > 1 ? bullet : '') +
                                        (typeof columnFilter === 'string'
                                            ? columnFilter
                                            : `${columnFilter.operator} ${value}`);
                                }).join('<br/><br/>');
                        }
                        else {
                            filterText = me.L('L{applyFilter}');
                        }

                        if (!filterIconEl) {
                            // putting icon in header text to have more options for positioning it
                            filterIconEl = DomHelper.createElement({
                                parent    : textEl,
                                tag       : 'div',
                                className : 'b-filter-icon',
                                dataset   : {
                                    filterText
                                }
                            });
                        }
                        else {
                            filterIconEl.dataset.filterText = filterText;
                        }

                        // latest applied filter distinguished with class to enable highlighting etc.
                        if (column.field === me.store.latestFilterField) filterIconEl.classList.add('b-latest');

                        headerEl.classList.add('b-filterable');
                        headerEl.classList.toggle('b-filter', isColumnFiltered);
                    }

                    column.meta.isFiltered = isColumnFiltered;
                }
            });

            if (reRenderRows) {
                grid.refreshRows();
            }
        }
    }

    //endregion

    //region Filter

    applyFilter(column, config) {
        const
            { store }    = this,
            { filterFn } = column.filterable;

        // Must add the filter silently, so that the column gets a reference to its $filter
        // before the filter happens and events are broadcast.
        column.$filter = store.addFilter({
            ...column.filterable,
            ...config,
            property : column.field,

            // Only inject a filterBy configuration if the column has a custom filterBy
            [filterFn ? 'filterBy' : '_'] : function(record) {
                return filterFn({ value : this.value, record, operator : this.operator, property : this.property, column });
            }
        }, true);

        // Apply the new set of store filters.
        store.filter();
    }

    removeFilter(column) {
        if (this.isMulti) {
            for (const filter of this.getCurrentMultiFilters(column)) {
                this.store.removeFilter(filter);
            }
        }
        else {
            this.store.removeFilter(column.field);
        }
    }

    disableFilter(column) {
        for (const filter of this.getCurrentMultiFilters(column)) {
            filter.disabled = true;
            this.store.filter(filter);
        }
        this.store.filter();
    }

    getCurrentMultiFilters(column) {
        return this.store.filters.values.filter(filter => filter.property === column.field);
    }



    getPopupDateItems(column, fieldType, filter, initialValue, store, changeCallback, closeCallback, filterField) {
        const
            me      = this,
            onClose = changeCallback;

        function onClear() {
            me.removeFilter(column);
        }

        function onKeydown({ event }) {
            if (event.key === 'Enter') {
                changeCallback();
            }
        }

        function onChange({ source, value }) {
            if (value == null) {
                onClear();
            }
            else {
                me.clearSiblingsFields(source);
                me.applyFilter(column, { operator : source.operator, value, displayValue : source._value, type : 'date' });
            }
        }

        return [
            ObjectHelper.assign({
                type        : 'date',
                ref         : 'on',
                placeholder : 'L{on}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
                value       : filter?.operator === 'sameDay' ? filter.value : initialValue,
                operator    : 'sameDay',
                onKeydown,
                onChange,
                onClose,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'date',
                ref         : 'before',
                placeholder : 'L{before}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-before"></i>',
                value       : filter?.operator === '<' ? filter.value : null,
                operator    : '<',
                onKeydown,
                onChange,
                onClose,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'date',
                ref         : 'after',
                cls         : 'b-last-row',
                placeholder : 'L{after}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-after"></i>',
                value       : filter?.operator === '>' ? filter.value : null,
                operator    : '>',
                onKeydown,
                onChange,
                onClose,
                onClear
            }, filterField)
        ];
    }

    getPopupNumberItems(column, fieldType, filter, initialValue, store, changeCallback, closeCallback, filterField) {
        const
            me    = this,
            onEsc = changeCallback;

        function onClear() {
            me.removeFilter(column);
        }

        function onKeydown({ event }) {
            if (event.key === 'Enter') {
                changeCallback();
            }
        }

        function onChange({ source, value }) {
            if (value == null) {
                onClear();
            }
            else {
                me.clearSiblingsFields(source);
                me.applyFilter(column, { operator : source.operator, value });
            }
        }

        return [
            ObjectHelper.assign({
                type        : 'number',
                placeholder : 'L{Filter.equals}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
                value       : filter?.operator === '=' ? filter.value : initialValue,
                operator    : '=',
                onKeydown,
                onChange,
                onEsc,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'number',
                placeholder : 'L{lessThan}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-less"></i>',
                value       : filter?.operator === '<' ? filter.value : null,
                operator    : '<',
                onKeydown,
                onChange,
                onEsc,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'number',
                cls         : 'b-last-row',
                placeholder : 'L{moreThan}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-more"></i>',
                value       : filter?.operator === '>' ? filter.value : null,
                operator    : '>',
                onKeydown,
                onChange,
                onEsc,
                onClear
            }, filterField)
        ];
    }

    clearSiblingsFields(sourceField) {

        this.filterEditorPopup?.items.forEach(field => {
            field !== sourceField && field?.clear();
        });
    }

    getPopupDurationItems(column, fieldType, filter, initialValue, store, changeCallback, closeCallback, filterField) {
        const
            me      = this,
            onEsc   = changeCallback,
            onClear = () => me.removeFilter(column);

        function onChange({ source, value }) {
            if (value == null) {
                onClear();
            }
            else {
                me.clearSiblingsFields(source);
                me.applyFilter(column, { operator : source.operator, value });
            }
        }

        return [
            ObjectHelper.assign({
                type        : 'duration',
                placeholder : 'L{Filter.equals}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
                value       : filter?.operator === '=' ? filter.value : initialValue,
                operator    : '=',
                onChange,
                onEsc,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'duration',
                placeholder : 'L{lessThan}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-less"></i>',
                value       : filter?.operator === '<' ? filter.value : null,
                operator    : '<',
                onChange,
                onEsc,
                onClear
            }, filterField),
            ObjectHelper.assign({
                type        : 'duration',
                cls         : 'b-last-row',
                placeholder : 'L{moreThan}',
                localeClass : me,
                clearable   : true,
                label       : '<i class="b-fw-icon b-icon-filter-more"></i>',
                value       : filter?.operator === '>' ? filter.value : null,
                operator    : '>',
                onChange,
                onEsc,
                onClear
            }, filterField)
        ];
    }

    getPopupStringItems(column, fieldType, filter, initialValue, store, changeCallback, closeCallback, filterField) {
        const me = this;

        return [ObjectHelper.assign({
            type        : fieldType,
            cls         : 'b-last-row',
            placeholder : 'L{filter}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
            value       : filter ? filter.value || filter : initialValue,
            operator    : '*',
            onChange({ source, value }) {
                if (value === '') {
                    closeCallback();
                }
                else {
                    me.applyFilter(column, { operator : source.operator, value, displayValue : source.displayField && source.records ? source.records.map(rec => rec[source.displayField]).join(', ') : undefined });
                    // Leave multiselect filter combo visible to be able to select many items at once
                    if (!source.multiSelect) {
                        changeCallback();
                    }
                }
            },
            onClose : changeCallback,
            onClear : closeCallback
        }, filterField)];
    }

    /**
     * Get fields to display in filter popup.
     * @param {Grid.column.Column} column Column
     * @param fieldType Type of field, number, date etc.
     * @param filter Current filter filter
     * @param initialValue
     * @param store Grid store
     * @param changeCallback Callback for when filter has changed
     * @param closeCallback Callback for when editor should be closed
     * @param filterField filter field
     * @returns {*}
     * @private
     */
    getPopupItems(column, fieldType, filter, initialValue, store, changeCallback, closeCallback, filterField) {
        const me = this;
        if (me.isMulti) {
            return me.getMultiFilterPopupItems(...arguments);
        }
        switch (fieldType) {
            case 'date':
                return me.getPopupDateItems(...arguments);
            case 'number':
                return me.getPopupNumberItems(...arguments);
            case 'duration':
                return me.getPopupDurationItems(...arguments);
            default:
                return me.getPopupStringItems(...arguments);
        }
    }

    getMultiFilterPopupItems(column) {
        const
            { grid, isMulti } = this,
            existingFilter = grid.store?.filters.find(filter => filter.property === column.field);
        return [{
            ...(typeof isMulti === 'object' ? isMulti : undefined),
            type            : 'gridfieldfilterpickergroup',
            ref             : 'pickerGroup',
            limitToProperty : column.field,
            grid,
            filters         : existingFilter ? [] : [{
                property : column.field
            }],
            propertyFieldCls : 'b-transparent property-field',
            operatorFieldCls : 'b-transparent operator-field',
            valueFieldCls    : 'b-transparent value-field',
            width            : 360
        }];
    }

    /**
     * Shows a popup where a filter can be edited.
     * @param {Grid.column.Column|String} column Column to show filter editor for
     * @param {*} [value] The initial value of the filter field
     */
    showFilterEditor(column, value) {
        column = this.grid.columns.getById(column);

        const
            me        = this,
            { store, isMulti } = me,
            headerEl  = column.element,
            filter    = store.filters.getBy('property', column.field),
            fieldType = me.getFilterType(column);

        if (column.filterable === false) {
            return;
        }

        // Destroy previous filter popup
        me.closeFilterEditor();

        const items = me.getPopupItems(
            column,
            fieldType,

            // Only pass filter if it's not an internal filter
            filter?.internal ? null : filter,

            value,
            store,
            me.closeFilterEditor,
            () => {
                me.removeFilter(column);
                me.closeFilterEditor();
            },
            column.filterable.filterField,
            isMulti
        );

        // Localize placeholders
        items.forEach(item => item.placeholder = item.placeholder ? this.L(item.placeholder) : item.placeholder);

        me.filterEditorPopup = WidgetHelper.openPopup(headerEl, {
            owner        : me.grid,
            cls          : 'b-filter-popup',
            scrollAction : 'realign',
            layout       : {
                type  : 'vbox',
                align : 'stretch'
            },
            items
        });
    }

    /**
     * Close the filter editor.
     */
    closeFilterEditor() {
        // Must defer the destroy because it may be closed by an event like a "change" event where
        // there may be plenty of code left to execute which must not execute on destroyed objects.
        this.filterEditorPopup?.setTimeout(this.filterEditorPopup.destroy);
        this.filterEditorPopup = null;
    }

    //endregion

    //region Context menu



    getFilterType(column) {
        const
            fieldName = column.field,
            field     = this.client.store.modelClass.getFieldDefinition(fieldName),
            type      = column.filterType;

        return type ? fieldTypeMap[type] : (fieldTypeMap[column.type] || field && fieldTypeMap[field.type]) || 'text';
    }

    populateCellMenuWithDateItems({ column, record, items }) {
        const
            property = column.field,
            type     = this.getFilterType(column);

        if (type === 'date') {
            const
                me       = this,
                value    = record.getValue(property),
                filter   = operator => {
                    me.applyFilter(column, {
                        operator,
                        value,
                        displayValue : column.formatValue ? column.formatValue(value) : value,
                        type         : 'date'
                    });
                };

            items.filterDateEquals = {
                text        : 'L{on}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterDateBefore = {
                text        : 'L{before}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-before',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterDateAfter = {
                text        : 'L{after}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-after',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithNumberItems({ column, record, items }) {
        const
            property = column.field,
            type     = this.getFilterType(column);

        if (type === 'number') {
            const
                me       = this,
                value    = record.getValue(property),
                filter   = operator => {
                    me.applyFilter(column, { operator, value });
                };

            items.filterNumberEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterNumberLess = {
                text        : 'L{lessThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-less',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterNumberMore = {
                text        : 'L{moreThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-more',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithDurationItems({ column, record, items }) {
        const
            type = this.getFilterType(column);

        if (type === 'duration') {
            const
                me       = this,
                value    = column.getFilterableValue(record),
                filter   = operator => {
                    me.applyFilter(column, { operator, value });
                };

            items.filterDurationEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterDurationLess = {
                text        : 'L{lessThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-less',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterDurationMore = {
                text        : 'L{moreThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-more',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithStringItems({ column, record, items }) {
        const type = this.getFilterType(column);

        if (!/(date|number|duration)/.test(type)) {
            const
                me       = this,
                value    = column.getFilterableValue(record),
                operator = column.filterable.filterField?.operator ?? '*';

            items.filterStringEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => me.applyFilter(column, { value, operator })
            };
        }
    }

    /**
     * Add menu items for filtering.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Core.data.Model} options.record Record for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateCellMenu({ column, record, items }) {
        const me = this;

        if (column.filterable !== false && !record.isSpecialRow) {
            me.populateCellMenuWithDateItems(...arguments);
            me.populateCellMenuWithNumberItems(...arguments);
            me.populateCellMenuWithDurationItems(...arguments);
            me.populateCellMenuWithStringItems(...arguments);

            if (column.meta.isFiltered) {
                items.filterRemove = {
                    text        : 'L{removeFilter}',
                    localeClass : me,
                    icon        : 'b-fw-icon b-icon-remove',
                    cls         : 'b-separator',
                    weight      : 400,
                    disabled    : me.disabled || (me.isMulti && !me.columnHasRemovableFilters(column)),
                    onItem      : () => me.removeFilter(column)
                };
            }

            if (me.isMulti) {
                items.filterDisable = {
                    text        : 'L{disableFilter}',
                    localeClass : me,
                    icon        : 'b-fw-icon b-icon-filter-disable',
                    cls         : 'b-separator',
                    weight      : 400,
                    disabled    : me.disabled || !me.columnHasEnabledFilters(column),
                    onItem      : () => me.disableFilter(column)
                };
            }
        }
    }

    /**
     * Used by isMulti mode to determine whether the 'remove filters' menu item should be enabled.
     * @internal
     */
    columnHasRemovableFilters(column) {
        const me = this;
        return Boolean(me.getCurrentMultiFilters(column).find(filter =>
            !me.canDeleteFilter || (me.callback(me.canDeleteFilter, me, [filter]) !== false)));
    }

    /**
     * Used by isMulti mode to determine whether the 'disable filters' menu item should be enabled.
     * @internal
     */
    columnHasEnabledFilters(column) {
        return Boolean(this.getCurrentMultiFilters(column).find(filter => !filter.disabled));
    }

    /**
     * Add menu item for removing filter if column is filtered.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateHeaderMenu({ column, items }) {
        const me = this;

        if (column.meta.isFiltered) {
            items.editFilter = {
                text        : 'L{editFilter}',
                localeClass : me,
                weight      : 100,
                icon        : 'b-fw-icon b-icon-filter',
                cls         : 'b-separator',
                disabled    : me.disabled,
                onItem      : () => me.showFilterEditor(column)
            };

            items.removeFilter = {
                text        : 'L{removeFilter}',
                localeClass : me,
                weight      : 110,
                icon        : 'b-fw-icon b-icon-remove',
                disabled    : me.disabled || (me.isMulti && !me.columnHasRemovableFilters(column)),
                onItem      : () => me.removeFilter(column)
            };

            if (me.isMulti) {
                items.disableFilter = {
                    text        : 'L{disableFilter}',
                    localeClass : me,
                    icon        : 'b-fw-icon b-icon-filter-disable',
                    weight      : 115,
                    disabled    : me.disabled || !me.columnHasEnabledFilters(column),
                    onItem      : () => me.disableFilter(column)
                };
            }
        }
        else if (column.filterable !== false) {
            items.filter = {
                text        : 'L{filter}',
                localeClass : me,
                weight      : 100,
                icon        : 'b-fw-icon b-icon-filter',
                cls         : 'b-separator',
                disabled    : me.disabled,
                onItem      : () => me.showFilterEditor(column)
            };
        }
    }

    //endregion

    //region Events

    // Intercept filtering by a column that has a custom filtering fn, and inject that fn
    onStoreBeforeFilter({ filters }) {
        const { columns } = this.client;

        for (let i = 0; i < filters.count; i++) {
            const filter = filters.getAt(i);

            // Only take ownership of filters which are not internal
            if (!filter.internal) {
                const column = (filter.columnOwned || this.prioritizeColumns) && columns.find(col => col.filterable !== false && col.field === filter.property);

                if (column?.filterable?.filterFn) {
                    // If the filter was sourced from the store, replace it with a filter which
                    // uses the column's filterFn
                    if (!column.$filter) {
                        column.$filter = new CollectionFilter({
                            columnOwned : true,
                            property    : filter.property,
                            operator    : filter.operator,
                            value       : filter.value,
                            filterBy(record) {
                                return column.filterable.filterFn({ value : this.value, record, operator : this.operator, property : this.property, column });
                            }
                        });
                    }

                    // Update value and operator used by filters filtering fn
                    column.$filter.value = filter.value;
                    column.$filter.displayValue = filter.displayValue;
                    column.$filter.operator = filter.operator;

                    filters.splice(i, 1, column.$filter);
                }
            }
        }
    }

    /**
     * Store filtered; refresh headers.
     * @private
     */
    onStoreFilter() {
        // Pass false to not refresh rows.
        // Store's refresh event will refresh the rows.
        this.refreshHeaders(false);
    }

    /**
     * Called after headers are rendered, make headers match stores initial sorters
     * @private
     */
    renderHeader() {
        this.refreshHeaders(false);
    }

    /**
     * Called when user clicks on the grid. Only care about clicks on the filter icon.
     * @param {MouseEvent} event
     * @private
     */
    onElementClick({ target }) {
        if (this.filterEditorPopup) {
            this.closeFilterEditor();
        }

        if (target.classList.contains('b-filter-icon')) {
            const headerEl = target.closest('.b-grid-header');

            this.showFilterEditor(headerEl.dataset.columnId);

            return false;
        }
    }

    /**
     * Called when user presses F-key grid.
     * @param {MouseEvent} event
     * @private
     */
    showFilterEditorByKey({ target }) {
        const headerEl = target.matches('.b-grid-header') && target;
        // Header must be focused
        if (headerEl) {
            this.showFilterEditor(headerEl.dataset.columnId);
        }
        return Boolean(headerEl);
    }

    // Only care about F key when a filterable header is focused
    isActionAvailable({ event }) {
        const
            headerElement = event.target.closest('.b-grid-header'),
            column        = headerElement && this.client.columns.find(col => col.id === headerElement.dataset.columnId);

        return Boolean(column?.filterable);
    }

    //endregion
}

GridFeatureManager.registerFeature(Filter);
