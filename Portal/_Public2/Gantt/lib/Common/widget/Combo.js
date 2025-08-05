import Store from '../data/Store.js';
import Model from '../data/Model.js';
import Filter from '../util/CollectionFilter.js';
import PickerField from './PickerField.js';
import Widget from './Widget.js';
import List from './List.js';
import ChipView from './ChipView.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import Collection from '../util/Collection.js';
import EventHelper from '../helper/EventHelper.js';
import Navigator from '../helper/util/Navigator.js';

const
    comboQueryAll  = Symbol('Combo.queryAll'),
    comboQueryLast = Symbol('Combo.queryLast'),
    fieldRequiredErrorName = 'fieldRequired',
    fieldvalidateFilterErrorName = 'validateFilter';

/**
 * @module Common/widget/Combo
 */

/**
 * Combo (dropdown) widget. Consists of a text field with a trigger icon, which displays a List. Can be
 * populated from a Store.
 *
 * @extends Common/widget/PickerField
 *
 * @example
 * // items as strings
 * let combo = new Combo({
 *   items: ['Small', 'Smaller', 'Really small', 'Tiny', 'Super tiny', '?'],
 *   placeholder: 'Pick size of diamond for ring'
 * });
 *
 * @example
 * // items as configs
 * let combo = new Combo({
 *   items: [{ value: 'a', text: 'First' }, { value: 'z', text: 'Last' }]
 * });
 *
 * @example
 * // items from store
 * let combo = new Combo({
 *   store: memberStore,
 *   valueField: 'id',
 *   displayField: 'name'
 * });
 *
 * @classType combo
 * @externalexample widget/Combo.js
 */
export default class Combo extends PickerField {
    //region Config

    static get defaultConfig() {
        return {
            /**
             * Rows to display in the dropdown (list items).
             *
             * If an object, the property names provide the {@link #config-value} for the Combo, and
             * the property values provide the displayed test in the list and input area eg:
             *
             *     items : {
             *         small  : 'Small',
             *         medium : 'Medium',
             *         large  : 'Large'
             *     }
             *
             * If an array, each entry may be
             *  - an object containing properties which must include
             * the {@link #config-valueField} and {@link #config-displayField} which populates the dropdown
             * with text and provides the corresponding field value.
             *  - An array whos first value provides the {@link #config-value} for the Combo and whos
             * second value provides the displayed test in the list and input area.
             *  - An array of values where the {@link #config-valueField} and {@link #config-displayField} are the same.
             *
             * eg:
             *
             *     items : [
             *         {value : 'small',  text : 'Small'},
             *         {value : 'medium', text : 'Medium'},
             *         {value : 'large',  text : 'Large'},
             *     ]
             *
             * or
             *
             *     items : [
             *         ['small',  'Small'],
             *         ['medium', 'Medium'],
             *         ['large',  'Large'],
             *     ]
             *
             * or
             *
             *     items : [ 'Small', 'Medium', 'Large' ]
             *
             * @config {Object[]|String[]|Object}
             */
            items : null,

            /**
             * A store used to populate items
             * @config {Common.data.Store}
             */
            store : null,

            /**
             * Field used for item value when populating from store
             * @config {String}
             */
            valueField : null,

            /**
             * Field used for item text when populating from store
             * @config {String}
             */
            displayField : 'text',

            /**
             * Width of picker, defaults to this combo's {@link #config-pickerAlignElement} width
             * @config {Number}
             */
            pickerWidth : null,

            /**
             * The minimum string length to trigger the filtering, only relevant when {@link #config-editable} is true
             * @config {Number}
             * @default
             */
            minChars : 1,

            selected : null,

            /**
             * Template for rendering list items contents
             * @config {Function}
             */
            listItemTpl : null,

            /**
             * Template function that can be used to customize the displayed value
             * @config {Function}
             */
            displayValueRenderer : null,

            /**
             * CSS class to add to picker
             * @config {String}
             */
            listCls : null,

            triggers : {
                expand : {
                    cls     : 'b-icon-picker',
                    handler : 'onTriggerClick'
                }
            },

            /**
             * This implies that the picker will display an anchor pointer, but also means that the picker will align closer
             * to the input field so that the pointer pierces the {@link #config-pickerAlignElement}
             * @config {Boolean}
             * @default false
             */
            overlayAnchor : null,

            /**
             * The delay in milliseconds to wait after the last keystroke before filtering the list.
             * @config {Number}
             * @default
             */
            keyStrokeFilterDelay : null,

            defaultAction : 'select',

            /**
             * How to query the store upon click of the expand trigger. There are two constants provided:
             *
             * * `Combo.queryAll` - Clear the filter and display the whole dataset in the dropdown.
             * * `Combo.queryLast` - Filter the dataset using the last filter value.
             * * `null`/any other - Use the value in the input field to filter the dataset.
             *
             * @config {Object}
             * @default Combo.QueryAll
             */
            triggerAction : comboQueryAll,

            /**
             * The name of an operator type as implemented in {@link Common.util.CollectionFilter#config-operator}
             * to use when filtering the dropdown list based upon the typed value.
             *
             * This defaults to `'startsWith'`, but the `'*'` operator may be used to match all
             * values which _contain_ the typed value.
             *
             * @config {String}
             */
            filterOperator : 'startsWith',

            /**
             * Configure as `true` to force case matching when filtering the dropdown list based upon the typed value.
             *
             * @config {Boolean}
             * @default false
             */
            caseSensitive : false,

            /**
             * Configure as `true` to allow selection of multiple values from the dropdown list.
             *
             * Each value is displayed as a "Chip" to the left of the input area. Chips may be
             * selected using the `LEFT` and `RIGHT` arrow keys and deleted using the `DELETE` key
             * to remove values from the field. There is also a clickable close icon in each chip.
             *
             * @config {Boolean}
             * @default false
             */
            multiSelect : null,

            /**
             * By default, the picker is hidden on selection in single select mode, and
             * remains to allow more selections when {@link #config-multiSelect} is `true`.
             * Setting this to a `Boolean` value can override that default.
             */
            hidePickerOnSelect : null,

            /**
             * A config object to configure the {@link Common.widget.ChipView} to display the
             * selected value set when {@link #config-multiSelect} is `true.
             *
             * For example the {@link Common.widget.List#config-itemTpl} or
             * {@link Common.widget.ChipView#config-iconTpl} might be configured to display
             * richer chips for selected items.
             * @config {Boolean}
             */
            chipView : null,

            /**
             * When {@link #config-multiSelect} is `true`, you may configure `filterSelected` as
             * `true` to hide items in the dropdown when they are added to the selection.
             * It will appear as if the requested item has "moved" into the field's
             * {@link #config-chipView ChipView}.
             *
             * @config {Boolean}
             * @default false
             */
            filterSelected : null,

            /**
             * Text to display in the drop down when there are no items in the underlying store
             * @config {String}
             * @default
             */
            emptyText : null,

            /**
             * The initial value of this Combo box. In single select mode (default) it's a simple string value, for {@link #config-multiSelect} mode, it should be an array of record ids.
             * @config {String|Number[]|String[]}
             * @default
             */
            value : null,

            /**
             * `true` to cause the field to be in an invalid state while the typed filter string does not match a record in the store.
             * @config {Boolean}
             * @default
             */
            validateFilter : true,
    
            /**
             * `true` to clear value typed to a multiselect combo when picker is collapsed
             * @config {Boolean}
             * @default
             */
            clearTextOnPickerHide : true
        };
    }

    //endregion

    afterConfigure() {
        super.afterConfigure();
        if (!('_value' in this)) {
            this._value = this.valueField === this.displayField ? '' : null;
        }
    }

    eachWidget(fn, deep = true) {
        for (const widget of [this.chipView, this._picker]) {
            if (widget) {
                if (fn(widget) === false) {
                    return;
                }
                if (deep && widget.eachWidget) {
                    widget.eachWidget(fn, deep);
                }
            }
        }
    }

    set element(element) {
        const me = this;

        super.element = element;

        // If we are multiSelect, create a ChipView who's store is backed
        // by our valueCollection - the collection of selected records.
        if (me.multiSelect) {
            me.element.classList.add('b-multiselect');
            me.chipView = new MultiSelectChipView(ObjectHelper.assign({
                parent       : me,
                insertBefore : me.input,
                store        : me.chipStore = new Store({
                    storage : me.valueCollection
                }),
                navigator : {
                    class          : MultiSelectChipNavigator,
                    keyEventTarget : me.input
                }
            }, me.chipView));

            // Insert the input field
            me.chipView.element.appendChild(me.input);

            // Focus must flow into this field from the ChipView
            EventHelper.on({
                element   : me.chipView.element,
                mousedown : 'onChipViewMousedown',
                thisObj   : me
            });
        }
    }

    onChipViewMousedown(mousedownEvent) {
        mousedownEvent.preventDefault();
        if (!this.containsFocus) {
            this.focus();
        }
    }

    onChipClose(records, options = {}) {
        // Do not clean value collection if input field is not empty - probably text is selected
        // and user just wants to remove it, not the picked values
        if (options.isKeyEvent && this.input.value === '' || !options.isKeyEvent) {
            this.valueCollection.remove(records);
        }
    }

    get element() {
        return super.element;
    }

    //region Getters/setters

    set keyStrokeFilterDelay(delay) {
        const me = this;

        if (delay) {
            me.filterList = me.buffer(me.doFilter, delay);
        }
        me._keyStrokeFilterDelay = delay;
    }

    get keyStrokeFilterDelay() {
        return this._keyStrokeFilterDelay;
    }

    /**
     * Prepares items to work in attached menu (converts strings to items)
     * @private
     */
    set items(items) {
        //<debug>
        if (this.isConfiguring && this.initialConfig.store) {
            throw new Error('A Combo may not be configured with items and store');
        }
        //</debug>

        if (items == null) {
            items = [];
        }

        if (items instanceof Store) {
            return this.store = items;
        }

        let itemModel,
            valueField = this.valueField,
            storeData;

        const me           = this,
            displayField = me.displayField;

        if (Array.isArray(items)) {
            storeData = items.map(item => {
                let result = item;

                if (item instanceof Model) {
                    itemModel = item.constructor;
                    if (!valueField) {
                        me.valueField = valueField = itemModel.idField;
                    }
                }
                else {
                    if (typeof item === 'string') {
                        if (!valueField) {
                            me.valueField = valueField = me.displayField;
                        }
                        result = {
                            [valueField]   : item,
                            [displayField] : item
                        };
                    }
                    else {
                        if (!valueField) {
                            me.valueField = valueField = 'value';
                        }
                        if (Array.isArray(item)) {
                            result = {
                                [valueField]   : item[0],
                                [displayField] : item[1]
                            };
                        }
                    }
                }

                if (result.selected) {
                    me.value = result;
                }
                return result;
            });
        }
        // Must be a value -> text map
        else {
            if (!valueField) {
                me.valueField = valueField = 'value';
            }
            storeData = [];
            Object.entries(items).forEach(([key, value]) => {
                storeData.push({
                    [valueField]   : key,
                    [displayField] : value
                });
            });
        }

        // Allow reconfiguring with a new set of items
        if (me.store) {
            me.store.data = storeData;
        }
        else {
            const valueFieldDefinition = valueField === displayField ? {
                name       : 'value',
                dataSource : displayField
            } : valueField;

            me.store = new Store({
                data       : storeData,
                idField    : valueField,
                modelClass : itemModel || class extends Model {
                    static get idField() {
                        return valueField;
                    }

                    static set idField(idField) {
                        super.idField = idField;
                    }

                    static get fields() {
                        return [valueFieldDefinition, displayField];
                    }
                }
            });
        }
    }

    get items() {
        return this.store.allRecords;
    }

    /**
     * Get/sets combo value, selects corresponding item in the list
     * Setting null clears the field.
     *
     * If {@link #config-multiSelect} is `true`, then multiple values may be passed as an array.
     * If the values are records, these become the selected record set held by {@link #property-valueCollection},
     * and the `value` yielded by this field is an array of all the {@link #config-valueField}s from the records.
     * @fires select
     * @fires action
     * @property {Object}
     */
    set value(value) {
        const me = this;

        if (value === me.value) {
            // prevent highlighting animation on clear
            const old = me.highlightExternalChange;
            me.highlightExternalChange = false;

            me.syncInputFieldValue();

            me.highlightExternalChange = old;
            return;
        }

        if (!me.multiSelect && Array.isArray(value) && value.length > 1) {
            throw new Error('Multiple values cannot be set to a non-multiselect Combo');
        }

        // This forces promotion of the items config into a Store if it has not already been injected
        me._thisIsAUsedExpression(me.items);

        const
            {
                valueField,
                displayField,
                store,
                valueCollection
            } = me;

        // Unfilter the store so we can do the value lookup.
        if (store.filtered) {
            me.primaryFilter.disabled = true;
            store.filter();
        }

        let record;

        if (value != null) {
            const values = Array.isArray(value) ? value : [value];

            for (let i = 0, len = values.length; i < len; i++) {
                let value = values[i];

                if (value instanceof Model) {
                    // The required record value may not yet be in the store. Add it if not.
                    if (!store.storage.includes(value)) {
                        store.add(value);
                    }
                }
                else {
                    const isObject = value instanceof Object;

                    // If they passed a data object, match the valueField
                    if (isObject) {
                        value = value[valueField];
                    }

                    // Use the Store Collection's extra indices to quickly find a match
                    record = store.storage.getBy(displayField, value) ||
                        store.storage.getBy(valueField, value);

                    if (record) {
                        // If the incoming value was a matched object, use it to update the record
                        if (isObject) {
                            record.set(values[i]);
                        }
                        values[i] = record;
                    }
                    else {
                        values.splice(i, 1);
                        len--;
                        i--;
                    }
                }
            }

            // Remove all old values, add new values in one shot.
            const vcGen = valueCollection.generation;
            valueCollection.splice(0, valueCollection.count, values);

            // If no change has fed through to onValueCollectionChange, just ensure the input matches
            if (valueCollection.generation === vcGen) {
                me.syncInputFieldValue();
            }

            // If we got no matches, onValueCollectionChange will set the _value to null.
            // Tests specify that the _value should be set to the incoming unmatched value
            if (!values.length) {
                me._value = value;
            }
        }
        else {
            if (valueCollection.count) {
                valueCollection.clear();
            }
            else {
                const oldValue = me._value;

                // Cache the value for use by our change handler next time, and also so that
                // when get value yields null, the fallback to ._value will be correct
                me._value = null;

                me.syncInputFieldValue();
                me.updateEmpty();
                if (!me.isConfiguring) {
                    me.trigger('change', {
                        value,
                        oldValue,
                        userAction : me._isUserAction,
                        valid      : me.isValid
                    });
                }
            }
        }
    }

    get value() {
        const me = this;

        return me.multiSelect ? me.valueCollection.map(r => r[me.valueField]) : me.valueCollection.count ? me.valueCollection.first[me.valueField] : me._value;
    }

    syncInputFieldValue() {
        // We only sync the input's value if we are not multiselecting.
        // If we are multiselecting, our value is represented by a ChipView.
        // The ChipView automatically syncs itself with our valueCollection.
        if (!this.multiSelect) {
            super.syncInputFieldValue();
        }
    }

    get isEmpty() {
        return this.valueCollection.count === 0;
    }

    get isValid() {
        if (this.required && !this.valueCollection.count) {
            this.setError(fieldRequiredErrorName, true);
        }
        return super.isValid;
    }

    get inputValue() {
        // This must be evaluated first, and NOT moved to be directly used as the
        // second expression in the ternary. If called during configuration, this
        // will import the configured value from the config object and ensure the
        // value is matched against the store, and that the "selected" property is set.
        let me = this,
            result = me.value;

        result = me.selected ? me.selected[me.displayField] : result;

        if (me.displayValueRenderer) {
            result = this.displayValueRenderer(me.selected);
        }

        return result == null ? '' : result;
    }

    set displayValueRenderer(value) {
        this._displayValueRenderer = value;
    }

    get displayValueRenderer() {
        return this._displayValueRenderer;
    }

    /**
     * A {@link Common.util.Collection Collection} which holds the currently seleted records
     * from the store which dictates this field's value.
     *
     * Usually, this will contain one record, the record selected.
     *
     * When {@link #config-multiSelect} is `true`, there may be several records selected.
     */
    get valueCollection() {
        if (!this._valueCollection) {
            this._valueCollection = new Collection({
                listeners : {
                    noChange : 'onValueCollectionNoChange',
                    change   : 'onValueCollectionChange',
                    prio     : -1000, // The ChipView must react to changes first.
                    thisObj  : this
                }
            });
        }

        return this._valueCollection;
    }

    /**
     * Get/set store to display items from. Also accepts a Store config object
     * @property {Common.data.Store|Object}
     */
    set store(store) {
        const
            me = this,
            storeFilters = [];

        if (Array.isArray(store)) {
            me.initialConfig.store = null;
            return me.items = store;
        }

        // Config object supplied, create a store
        if (store && !(store instanceof Store)) {
            store = new Store(store);
        }

        if (!me.valueField) {
            me.valueField = store.modelClass.idField;
        }

        // This is the filter that performs filtering on typing.
        if (!me.primaryFilter) {
            // Need an id to replace any existing combo filter on the store. Precommit hook wont allow it to be set
            // directly...
            const id = 'primary';
            me.primaryFilter = new Filter({
                id,
                property      : me.displayField,
                operator      : me.filterOperator,
                disabled      : true,
                caseSensitive : me.caseSensitive
            });
        }
        storeFilters.push(me.primaryFilter);
        if (me.filterSelected) {
            me.selectedItemsFilter = r => !me.valueCollection.includes(r);
            storeFilters.push(me.selectedItemsFilter);
        }
        store.filter(storeFilters);

        me._store = store;

        // Allow fast lookup by value or displayed value
        store.storage.addIndex(me.displayField);
        store.storage.addIndex(me.valueField);

        if (me.displayValueRenderer) {
            store.on({
                change : () => me.syncInputFieldValue()
            });

            me.syncInputFieldValue();
        }
    }

    get store() {
        return this._store;
    }

    /**
     * Get selected record.
     * @property {Common.data.Model[]}
     * @readonly
     */
    get record() {
        return this.selected;
    }

    /**
     * Get the selected record(s).
     * @property {Common.data.Model[]}
     * @readonly
     */
    get records() {
        return this.valueCollection.values.slice();
    }

    get selected() {
        return this.valueCollection.first;
    }

    //endregion

    //region Value handling

    /**
     * Check if field value is valid
     * @internal
     */
    onEditComplete() {
        const me = this,
            selectionCount = me.valueCollection.count;

        super.onEditComplete();

        // Ensure the input area matches the selected value
        if (selectionCount) {
            me.clearError(fieldvalidateFilterErrorName);
            me.syncInputFieldValue();
        }
        if (me.required && !selectionCount) {
            me.setError(fieldRequiredErrorName);
        }
    }

    //endregion

    //region Events

    /**
     * User clicked trigger icon, toggle list.
     * @private
     */
    onTriggerClick() {
        const me = this;

        if (me.pickerVisible) {
            me.hidePicker();
        }
        else {
            if (!me.readOnly && !me.disabled) {
                switch (me.triggerAction) {
                    case comboQueryAll:
                        me.doFilter(null);
                        break;
                    case comboQueryLast:
                        me.doFilter(me.lastQuery);
                        break;
                    default:
                        me.doFilter(me.input.value);
                }
            }
        }
    }

    /**
     * User types into input field in editable combo, show list and filter it.
     * @private
     */
    internalOnInput(event) {
        const me = this,
            inputLen = me.input.value.length;

        // IE11 triggers input event on focus for some reason, ignoring it if not editable
        if (!me.editable) return;

        me.updateEmpty();

        if (inputLen >= me.minChars) {
            me.filterList(me.input.value);
        }
        else {
            if (me.validateFilter) {
                me[inputLen ? 'setError' : 'clearError'](fieldvalidateFilterErrorName);
            }
            me.hidePicker();
        }

        /**
         * User typed into the field. Please note that the value attached to this event is the raw input field value and
         * not the combos value
         * @event input
         * @param {Common.widget.Combo} source - The combo
         * @param {String} value - Raw input value
         */
        me.trigger('input', { value : me.input.value, event });
    }

    // This is potentially a buffered function to respond to keystrokes in a buffered manner.
    // This only becomes useful as a saving when using remote querying where each filter is an Ajax request.
    filterList(queryString) {
        this.doFilter(queryString);
    }

    doFilter(queryString) {
        const me            = this,
            store         = me.store,
            disableFilter = queryString == null || queryString == '';

        me.lastQuery = queryString;

        me.primaryFilter.setConfig({
            value    : queryString,
            disabled : disableFilter
        });
        store.filter();

        me.showPicker();

        if (store.count) {
            // If we are filtering, activate the first match
            if (!disableFilter) {
                me.picker.navigator.activeItem = 0;
            }
        }
        // If we were actively filtering on a string but there were no matches
        // and we are validateFilter: true, then mark as invalid even though we
        // may have an underlying valid selected value.
        else if (!disableFilter && me.validateFilter) {
            me.setError(fieldvalidateFilterErrorName);
        }
    }

    /**
     * This reacts to our {@link #property-valueCollection} being mutated in any way.
     * The `change`, `select` and `action` events are fired here.
     *
     * This could happen in four ways:
     *
     *  - User selected or deselected an item in the dropdown list.
     *  - `set value` changes the content.
     *  - The {@link #config-multiSelect} Chip view (which uses this in its store) deletes a record.
     *  - The application programmatically mutates the {@link #property-valueCollection}.
     *
     * @private
     */
    onValueCollectionChange({ source : valueCollection }) {
        const me = this,
            { multiSelect } = me,
            hidePicker = ('hidePickerOnSelect' in me) ? me.hidePickerOnSelect : !multiSelect,
            record = multiSelect ? valueCollection.values.slice() : valueCollection.first,
            records = valueCollection.values.slice(),
            isUserAction = me._isUserAction ||  hidePicker && me.pickerVisible || false,
            oldValue = me._value;

        if (hidePicker) {
            me.hidePicker();
        }

        if (!valueCollection.count && me.required) {
            me.setError(fieldRequiredErrorName);
        }
        else {
            me.clearError(fieldRequiredErrorName);
            me.clearError(fieldvalidateFilterErrorName);
        }

        if (me.validateFilter && record) {
            me.clearError(fieldvalidateFilterErrorName);
        }

        // Re-evaluate the filtering so that selected items are filtered out of the dropdown
        if (me.filterSelected) {
            me.store.filter();
        }

        // Clear the cached value so that there's no fallback when we read back the value below
        me._value = null;

        // Cache the value for use by our change handler next time, and also so that
        // if we just cleared the valueCollection, the fallback to ._value will be correct
        const value = me._value = me.value;

        me.syncInputFieldValue();
        me.updateEmpty();

        if (!me.isConfiguring) {
            me.trigger('change', {
                value,
                oldValue,
                userAction : isUserAction,
                valid      : me.isValid
            });

            /**
             * User selected an item in the list
             * @event select
             * @property {Combo} combo - Combo
             * @property {Common.data.Model} record - Selected record
             * @property {Common.data.Model} records - Selected records as an array if {@link #config-multiSelect} is `true`
             * @property {Boolean} userAction - `true` if the value change is due to user interaction.
             */
            me.trigger('select', { record, records, userAction : me.containsFocus });

            /**
             * User performed the default action in the list (selected an item)
             * @event action
             * @property {Combo} combo - Combo
             * @property {Mixed} value - The {@link #valueField} of the selected record
             * @property {Common.data.Model} record - Selected record
             * @property {Common.data.Model} records - Selected records as an array if {@link #config-multiSelect} is `true`
             */
            if (me.defaultAction === 'select') {
                me.trigger('action', { value, record, records });
            }
        }
    }

    /**
     * This listens for when a record from the list is selected, but is already part of
     * the selection and so the {@link #property-valueCollection} rejects that as a no-op.
     * At this point, the user will still expect the picker to hide.
     * @param {Object} event The noChange event containing the splice parameters
     * @private
     */
    onValueCollectionNoChange({ toAdd }) {
        if (!this.multiSelect && toAdd.length && this.pickerVisible) {
            this.picker.hide();
        }
    }

    //endregion

    //region Picker

    get picker() {
        if (!this._picker) {
            this.picker = true;
        }
        return this._picker;
    }

    set picker(picker) {
        const me = this;

        if (me._picker) {
            me._picker.destroy();
        }

        if (picker instanceof Widget) {
            me._picker = picker;
        }
        else {
            me._picker = me.createPicker(picker);
        }
    }

    showPicker() {
        const me = this,
            { picker } = me;

        super.showPicker();

        // Once we have access to the anchor size, overlay the anchor pointer over the target if configured to do so.
        if (me.overlayAnchor && !picker.align.offset) {
            picker.align.offset = -picker.anchorSize[1];
            picker.realign();
        }

        if (me.selected) {
            picker.navigator.activeItem = me.store.indexOf(me.selected);
        }

        me.input.focus();
    }

    /**
     * Creates default picker widget
     *
     * @internal
     */
    createPicker(pickerConfig) {
        const me = this,
            { multiSelect, pickerWidth } = me,
            picker = new List(ObjectHelper.merge({
                owner               : me,
                floating            : true,
                scrollAction        : 'realign',
                itemsFocusable      : false,
                activateOnMouseover : true,
                store               : me.store,
                selected            : me.valueCollection,
                multiSelect,
                cls                 : me.listCls,
                itemTpl             : me.listItemTpl || (item => item[me.displayField]),
                forElement          : me[me.pickerAlignElement],
                align               : {
                    align     : 't-b',
                    axisLock  : true,
                    matchSize : pickerWidth == null,
                    anchor    : me.overlayAnchor,
                    target    : me[me.pickerAlignElement]
                },
                width     : pickerWidth,
                navigator : {
                    keyEventTarget : me.input
                },
                maxHeight  : 324,
                scrollable : {
                    overflowY : true
                },
                autoShow     : false,
                focusOnHover : false
            }, pickerConfig));

        picker.element.dataset.emptyText = me.emptyText || me.L('No results');

        return picker;
    }
    
    onPickerHide() {
        const me = this;
        
        super.onPickerHide();
        
        // https://app.assembla.com/spaces/bryntum/tickets/7736
        if (me.multiSelect && me.clearTextOnPickerHide) {
            me.input.value = '';
        }
    }
    
    //endregion
}

// Constants for how to query on clicking the trigger.
// queryAll means disable the primaryFilter
// queryLast means query using the last query string
// Any other value means use the input field's content

/**
 * A constant value for the {@link #config-triggerAction} config to indicate that clicking the trigger should
 * clear the filter and display the whole dataset in the dropdown.
 * @member {Symbol} queryAll
 * @readonly
 * @static
 */
Combo.queryAll = comboQueryAll;

/**
 * A constant value for the {@link #config-triggerAction} config to indicate that clicking the trigger should
 * filter the dataset usiong the last filter query string, *not* the input field value.
 * @member {Symbol} queryLast
 * @readonly
 * @static
 */
Combo.queryLast = comboQueryLast;

class MultiSelectChipView extends ChipView {
    static get defaultConfig() {
        return {
            cls : 'b-combo-chip-list',

            itemsFocusable : false,

            multiSelect : true,

            closeHandler : 'up.onChipClose',

            itemTpl : function(record) {
                return record[this.owner.displayField];
            }
        };
    }
}

class MultiSelectChipNavigator extends Navigator {
    static get defaultConfig() {
        return {
            allowShiftKey : true
        };
    }

    onTargetClick(clickEvent) {
        const item = clickEvent.target.closest(this.itemSelector);

        if (item && !clickEvent.shiftKey && !item.contains(clickEvent.target.closest('[data-noselect]'))) {
            this.ownerCmp.selected.clear();
        }
        // Our own set activeItem also selects because on superclass *key* navigation
        // (which is async on scroll end), it sets activeItem, and we select at that time.
        // So on click we skip this class and go straight to the superclass because the
        // List's onItemClick must run, and that does selection.
        super.activeItem = item;
    }

    onKeyDown(keyEvent) {
        // ENTER does not toggle selectedness in a ChipView.
        // ChipView's selection is bound to navigation.
        if (keyEvent.key !== 'Enter') {
            super.onKeyDown(keyEvent);
        }
    }

    set activeItem(activeItem) {
        const chipView = this.ownerCmp;

        super.activeItem = activeItem;

        // Selection simply follows navigation in a ChipView
        if (activeItem) {
            chipView.selected.add(chipView.getRecord(activeItem));
        }
    }

    get activeItem() {
        return super.activeItem;
    }

    navigatePrevious(keyEvent) {
        const chipView = this.ownerCmp;
    
        if (chipView.navigator.activeItem && !keyEvent.shiftKey) {
            chipView.selected.clear();
        }
        if (this.previous) {
            super.navigatePrevious(keyEvent);
        }
        else {
            this.activeItem = null;
        }
    }

    navigateNext(keyEvent) {
        const chipView = this.ownerCmp;

        // SHIFT+navigate preserves selection
        if (chipView.navigator.activeItem && !keyEvent.shiftKey) {
            chipView.selected.clear();
        }
        if (this.next) {
            super.navigateNext(keyEvent);
        }
        else {
            this.activeItem = null;
        }
    }
}

BryntumWidgetAdapterRegister.register('combo', Combo);
BryntumWidgetAdapterRegister.register('combobox', Combo);
BryntumWidgetAdapterRegister.register('dropdown', Combo);
