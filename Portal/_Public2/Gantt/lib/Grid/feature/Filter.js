//TODO: Format value in header filter tooltip (see date)

import DomHelper from '../../Core/helper/DomHelper.js';
import WidgetHelper from '../../Core/helper/WidgetHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Tooltip from '../../Core/widget/Tooltip.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import '../../Core/widget/NumberField.js';
import '../../Core/widget/Combo.js';
import '../../Core/widget/DateField.js';
import '../../Core/widget/TimeField.js';
import GridFeatureManager from './GridFeatureManager.js';

/**
 * @module Grid/feature/Filter
 */

const fieldTypeMap = {
    date     : 'date',
    integer  : 'number',
    number   : 'number',
    string   : 'text',
    duration : 'duration'
};

/**
 * Feature that allows filtering of the grid by settings filters on columns. The actual filtering is done by the store.
 * For info on programmatically handling filters, see {@link Core.data.mixin.StoreFilter StoreFilter}.
 *
 * This feature is <strong>disabled</strong> by default.
 *
 * **Note:** This feature cannot be used together with {@link Grid.feature.FilterBar filterBar} feature, they are mutually exclusive.
 *
 * @extends Core/mixin/InstancePlugin
 *
 * @example
 * // Filtering turned on but no default filter
 * let grid = new Grid({
 *   features : {
 *     filter : true
 *   }
 * });
 *
 * // Using default filter
 * let grid = new Grid({
 *   features : {
 *     filter : { property : 'city', value : 'Gavle' }
 *   }
 * });
 *
 * // Custom filtering function for a column
 * let grid = new Grid({
 *    features : {
 *        filter : true
 *    },
 *
 *    columns: [
 *        {
 *          field      : 'age',
 *          text       : 'Age',
 *          type       : 'number',
 *          // Custom filtering function that checks "greater than" no matter which field user filled in :)
 *          filterable : ({ record, value, operator }) => record.age > value
 *        }
 *    ]
 * });
 *
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
 * @demo Grid/filtering
 * @classtype filter
 * @externalexample feature/Filter.js
 */
export default class Filter extends InstancePlugin {
    //region Init

    static get $name() {
        return 'Filter';
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
            grid.store.filter(config, null, grid.isConfiguring);
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
        this.detachListeners('name');

        store.on({
            name    : 'store',
            filter  : 'onStoreFilter',
            thisObj : this
        });
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

            for (const column of grid.columns) {
                if (column.filterable !== false) {
                    const
                        filter   = me.store.filters.getBy('property', column.field),
                        headerEl = column.element;

                    if (headerEl) {
                        const textEl = column.textWrapper;

                        let filterIconEl = textEl?.querySelector('.b-filter-icon'),
                            filterText;

                        if (filter) {
                            filterText = me.L('L{filter}') + ': ' + (typeof filter === 'string'
                                ? filter
                                : `${filter.operator} ${filter.displayValue || filter.value || ''}`);
                            //TODO: filter.value needs to be formatted using column format or something
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
                                    filterText : filterText
                                }
                            });
                            headerEl.classList.add('b-filterable');
                        }
                        else {
                            filterIconEl.dataset.filterText = filterText;
                        }

                        // latest applied filter distinguished with class to enable highlighting etc.
                        if (column.field === me.store.latestFilterField) filterIconEl.classList.add('b-latest');

                        headerEl.classList[filter ? 'add' : 'remove']('b-filter');
                        // When IE11 support is dropped
                        // headerEl.classList.toggle('b-filter', !!filter);
                    }

                    column.meta.isFiltered = !!filter;
                }
            }

            if (reRenderRows) {
                grid.refreshRows();
            }
        }
    }

    //endregion

    //region Filter

    applyFilter(config) {
        const
            { store } = this,
            column    = this.grid.columns.get(config.property);

        if (typeof column.filterable === 'function') {
            store.filter({
                filterBy     : record => column.filterable(Object.assign({}, config, { record })),
                // To be able to retrieve the value next time filtering popup is shown, not actually used by the filter
                value        : config.value,
                property     : config.property,
                operator     : config.operator,
                displayValue : config.displayValue
            });
        }
        else {
            store.filter(config);
        }
    }

    // TODO: break out as own views, registering with Filter the same way columns register with ColumnManager

    getPopupDateItems(fieldType, filter, initialValue, field, store, changeCallback, closeCallback) {
        const
            me      = this,
            onClose = changeCallback;

        function onClear() {
            me.store.removeFilter(field);
        }

        function onKeyDown({ event }) {
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
                me.applyFilter({ property : field, operator : source.operator, value, displayValue : source._value });
            }
        }

        return [{
            type        : 'date',
            ref         : 'on',
            placeholder : 'L{on}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
            value       : filter?.operator === '=' ? filter.value : initialValue,
            operator    : '=',
            onKeydown   : onKeyDown,
            onChange,
            onClose,
            onClear
        }, {
            type        : 'date',
            ref         : 'before',
            placeholder : 'L{before}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-before"></i>',
            value       : filter?.operator === '<' ? filter.value : null,
            operator    : '<',
            onKeydown   : onKeyDown,
            onChange,
            onClose,
            onClear
        }, {
            type        : 'date',
            ref         : 'after',
            cls         : 'b-last-row',
            placeholder : 'L{after}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-after"></i>',
            value       : filter?.operator === '>' ? filter.value : null,
            operator    : '>',
            onKeydown   : onKeyDown,
            onChange,
            onClose,
            onClear
        }];
    }

    getPopupNumberItems(fieldType, filter, initialValue, field, store, changeCallback, closeCallback) {
        const
            me    = this,
            onEsc = changeCallback;

        function onClear() {
            me.store.removeFilter(field);
        }

        function onKeyDown({ event }) {
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
                me.applyFilter({ property : field, operator : source.operator, value });
            }
        }

        return [{
            type        : 'number',
            placeholder : 'L{Filter.equals}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
            value       : filter?.operator === '=' ? filter.value : initialValue,
            onKeydown   : onKeyDown,
            operator    : '=',
            onChange,
            onEsc,
            onClear
        }, {
            type        : 'number',
            placeholder : 'L{lessThan}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-less"></i>',
            value       : filter?.operator === '<' ? filter.value : null,
            onKeydown   : onKeyDown,
            operator    : '<',
            onChange,
            onEsc,
            onClear
        }, {
            type        : 'number',
            cls         : 'b-last-row',
            placeholder : 'L{moreThan}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-more"></i>',
            value       : filter?.operator === '>' ? filter.value : null,
            onKeydown   : onKeyDown,
            operator    : '>',
            onChange,
            onEsc,
            onClear
        }];
    }

    clearSiblingsFields(sourceField) {
        // TODO: Store filtering allows multiple filters per field (for example age > 50 and age < 80),
        // but the Filter feature only handles a single filter per field.
        // For now, trying to add filter by age > and then for age <,
        // it should clear the previous field since that filter is replaced
        this.filterEditorPopup?.items.forEach(field => {
            field !== sourceField && field?.clear();
        });
    }

    getPopupDurationItems(fieldType, filter, initialValue, field, store, changeCallback, closeCallback) {
        const
            me      = this,
            onEsc   = changeCallback,
            onClear = closeCallback;

        function onChange({ source, value }) {
            if (value == null) {
                closeCallback();
            }
            else {
                me.applyFilter({ property : field, operator : source.operator, value : value });
                changeCallback();
            }
        }

        return [{
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
        }, {
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
        }, {
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
        }];
    }

    getPopupStringItems(fieldType, filter, initialValue, field, store, changeCallback, closeCallback, filterField) {
        const me = this;

        return [ObjectHelper.assign({
            type        : fieldType,
            cls         : 'b-last-row',
            placeholder : 'L{filter}',
            localeClass : me,
            clearable   : true,
            label       : '<i class="b-fw-icon b-icon-filter-equal"></i>',
            value       : filter ? filter.value || filter : initialValue,
            onChange    : ({ value }) => {
                if (value === '') {
                    closeCallback();
                }
                else {
                    me.applyFilter({ property : field, value });
                    changeCallback();
                }
            },
            onClose : changeCallback,
            onClear : closeCallback
        }, filterField)];
    }

    /**
     * Get fields to display in filter popup.
     * @param fieldType Type of field, number, date etc.
     * @param filter Current filter filter
     * @param initialValue
     * @param field Column
     * @param store Grid store
     * @param changeCallback Callback for when filter has changed
     * @param closeCallback Callback for when editor should be closed
     * @param filterField filter field
     * @returns {*}
     * @private
     */
    getPopupItems(fieldType, filter, initialValue, field, store, changeCallback, closeCallback, filterField) {
        switch (fieldType) {
            case 'date':
                return this.getPopupDateItems(...arguments);
            case 'number':
                return this.getPopupNumberItems(...arguments);
            case 'duration':
                return this.getPopupDurationItems(...arguments);
            default:
                return this.getPopupStringItems(...arguments);
        }
    }

    /**
     * Shows a popup where a filter can be edited.
     * @param {Grid.column.Column|String} column Column to show filter editor for
     * @param {*} [value] The initial value of the filter field
     */
    showFilterEditor(column, value) {
        const
            me        = this,
            { store } = me,
            col       = typeof column === 'string' ? me.grid.columns.getById(column) : column,
            headerEl  = col.element,
            field     = store.modelClass.fieldMap[col.field],
            filter    = store.filters.getBy('property', col.field),
            type      = col.filterType,
            fieldType = type ? fieldTypeMap[type] : (fieldTypeMap[col.type] || field && fieldTypeMap[field.type]) || 'text';

        if (col.filterable === false) {
            return;
        }

        // Destroy previous filter popup
        me.closeFilterEditor();

        const items = me.getPopupItems(
            fieldType,
            filter,
            value,
            col.field,
            me.store,
            me.closeFilterEditor,
            () => {
                me.store.removeFilter(col.field);
                me.closeFilterEditor();
            },
            col.filterable.filterField
        );

        // Localize placeholders
        items.forEach(item => item.placeholder = this.L(item.placeholder));

        me.filterEditorPopup = WidgetHelper.openPopup(headerEl, {
            owner        : this.grid,
            width        : '16em',
            cls          : 'b-filter-popup',
            scrollAction : 'realign',
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

    //TODO: break out together with getPopupXXItems() (see comment above)

    populateCellMenuWithDateItems({ column, record, items }) {
        const
            property   = column.field,
            field      = record.getFieldDefinition(property),
            type       = column.filterType || field?.type || column.type;

        if (type === 'date') {
            const
                me       = this,
                value    = record[property],
                filter   = operator => {
                    me.applyFilter({
                        property,
                        operator,
                        value,
                        displayValue : column.formatValue ? column.formatValue(value) : value
                    });
                };

            items.filterDateEquals = {
                text        : 'L{on}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                name        : 'filterDateEquals',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterDateBefore = {
                text        : 'L{before}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-before',
                name        : 'filterDateBefore',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterDateAfter = {
                text        : 'L{after}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-after',
                name        : 'filterDateAfter',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithNumberItems({ column, record, items }) {
        const
            property   = column.field,
            field      = record.getFieldDefinition(property),
            type       = column.filterType || column.type || field?.type;

        if (type === 'number') {
            const
                me       = this,
                value    = record[property],
                filter   = operator => {
                    me.applyFilter({ property, operator, value });
                };

            items.filterNumberEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                name        : 'filterNumberEquals',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterNumberLess = {
                text        : 'L{lessThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-less',
                name        : 'filterNumberLess',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterNumberMore = {
                text        : 'L{moreThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-more',
                name        : 'filterNumberMore',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithDurationItems({ column, record, items }) {
        const
            property   = column.field,
            field      = record.getFieldDefinition(property),
            type       = column.filterType || field?.type || column.type;

        if (type === 'duration') {
            const
                me       = this,
                value    = record[property],
                filter   = operator => {
                    me.applyFilter({ property, operator, value });
                };

            items.filterDurationEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                name        : 'filterDurationEquals',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => filter('=')
            };

            items.filterDurationLess = {
                text        : 'L{lessThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-less',
                name        : 'filterDurationLess',
                weight      : 310,
                disabled    : me.disabled,
                onItem      : () => filter('<')
            };

            items.filterDurationMore = {
                text        : 'L{moreThan}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-more',
                name        : 'filterDurationMore',
                weight      : 320,
                disabled    : me.disabled,
                onItem      : () => filter('>')
            };
        }
    }

    populateCellMenuWithStringItems({ column, record, items }) {
        const
            property   = column.field,
            field      = record.getFieldDefinition(property),
            type       = column.filterType || field?.type || column.type;

        if (!/(date|number|duration)/.test(type)) {
            const
                me    = this,
                value = record[property];

            items.filterStringEquals = {
                text        : 'L{equals}',
                localeClass : me,
                icon        : 'b-fw-icon b-icon-filter-equal',
                cls         : 'b-separator',
                name        : 'filterStringEquals',
                weight      : 300,
                disabled    : me.disabled,
                onItem      : () => me.applyFilter({ property, value })
            };
        }
    }

    /**
     * Add menu items for filtering.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Core.data.Model} options.record Record for which the menu will be shown
     * @param {Object} options.items A named object to describe menu items
     * @internal
     */
    populateCellMenu({ column, record, items }) {
        const me = this;

        if (column.filterable !== false) {
            me.populateCellMenuWithDateItems(...arguments);
            me.populateCellMenuWithNumberItems(...arguments);
            me.populateCellMenuWithDurationItems(...arguments);
            me.populateCellMenuWithStringItems(...arguments);

            if (column.meta.isFiltered) {
                items.filterRemove = {
                    text        : 'L{removeFilter}',
                    localeClass : me,
                    icon        : 'b-fw-icon b-icon-clear',
                    cls         : 'b-separator',
                    weight      : 400,
                    name        : 'filterRemove',
                    disabled    : me.disabled,
                    onItem      : () => me.store.removeFilter(column.field)
                };
            }
        }
    }

    /**
     * Add menu item for removing filter if column is filtered.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object} options.items A named object to describe menu items
     * @returns {Object} items Menu items config
     * @internal
     */
    populateHeaderMenu({ column, items }) {
        const me = this;

        if (column.meta.isFiltered) {
            items.editFilter = {
                text        : 'L{editFilter}',
                localeClass : me,
                name        : 'editFilter',
                weight      : 100,
                icon        : 'b-fw-icon b-icon-filter',
                cls         : 'b-separator',
                disabled    : me.disabled,
                onItem      : () => me.showFilterEditor(column)
            };

            items.removeFilter = {
                text        : 'L{removeFilter}',
                localeClass : me,
                name        : 'removeFilter',
                weight      : 110,
                icon        : 'b-fw-icon b-icon-remove',
                disabled    : me.disabled,
                onItem      : () => me.store.removeFilter(column.field)
            };
        }
        else if (column.filterable !== false) {
            items.filter = {
                text        : 'L{filter}',
                localeClass : me,
                name        : 'filter',
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
    onElementClick(event) {
        const target = event.target;

        if (this.filterEditorPopup) this.closeFilterEditor();

        // Checks if click is on node expander icon, then toggles expand/collapse
        if (target.classList.contains('b-filter-icon')) {
            const headerEl = DomHelper.up(target, '.b-grid-header');

            this.showFilterEditor(headerEl.dataset.columnId);
            return false;
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(Filter);
