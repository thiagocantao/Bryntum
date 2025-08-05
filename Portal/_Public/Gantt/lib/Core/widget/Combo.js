import Store from '../data/Store.js';
import AjaxStore from '../data/AjaxStore.js';
import Model from '../data/Model.js';
import Filter from '../util/CollectionFilter.js';
import PickerField from './PickerField.js';
import ChipView from './ChipView.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import Collection from '../util/Collection.js';
import DomHelper from '../helper/DomHelper.js';
import DomSync from '../helper/DomSync.js';
import EventHelper from '../helper/EventHelper.js';
import Navigator from '../helper/util/Navigator.js';
import List from './List.js';
import StringHelper from '../helper/StringHelper.js';
import ArrayHelper from '../helper/ArrayHelper.js';

const
    errorFieldRequired  = 'L{Field.fieldRequired}',
    errorValidateFilter = 'L{Field.validateFilter}',
    errorRecordNotCommitted = 'L{Combo.recordNotCommitted}';

/**
 * @module Core/widget/Combo
 */

/**
 * Combo (dropdown) widget. Consists of a text field with a trigger icon, which displays a List. Can be
 * populated from a Store.
 *
 * This field can be used as an {@link Grid/column/Column#config-editor} for the {@link Grid/column/Column}.
 *
 * Please be aware that when populating the Combo with objects or records you have to configure {@link #config-valueField} and {@link #config-displayField}
 * to point to the correct field names in your data.
 *
 * ### Basic scenarios
 * {@inlineexample Core/widget/Combo.js vertical}
 *
 * ### Multiselect + grouped list
 *
 * {@inlineexample Core/widget/ComboMultiselect.js}
 *
 * ## Snippet: Loading data from simple string array
 * ```javascript
 * const combo = new Combo({
 *     items       : ['Small', 'Smaller', 'Really small', 'Tiny'],
 *     placeholder : 'Pick size of diamond for ring'
 * });
 *```
 *
 * ## Snippet: Loading data from array with item configs
 * ```javascript
 * const combo = new Combo({
 *     items : [{ value: 'a', text: 'First' }, { value: 'z', text: 'Last' }]
 * });
 *```
 *
 * ## Snippet: Loading data from store
 * ```javascript
 * const combo = new Combo({
 *     store        : memberStore,
 *     valueField   : 'id',
 *     displayField : 'name'
 * });
 *```
 *
 * ## Snippet: Grouped list
 * To group the list contents, simply group your store using {@link Core.data.mixin.StoreGroup#config-groupers}. You
 * can decide if clicking a header should toggle all group children (or if it should do nothing) with the
 * {@link Core.widget.List#config-allowGroupSelect} flag.
 * ```javascript
 * const combo = new Combo({
 *     width            : 400,
 *     displayField     : 'name',
 *     valueField       : 'id',
 *     multiSelect      : true,
 *     picker : {
 *         allowGroupSelect : false,
 *         // Show icon based on group name
 *         groupHeaderTpl   : (record, groupName) => `
 *             <i class="icon-${groupName}"></i>${groupName}
 *         `
 *     },
 *     value : [1, 4],
 *     store : new Store({
 *         fields : [
 *             'type'
 *         ],
 *         groupers : [
 *             { field : 'type', ascending : true }
 *         ],
 *         data : [
 *             { id : 1, name : 'pizza', type : 'food' },
 *             { id : 2, name : 'bacon', type : 'food' },
 *             { id : 3, name : 'egg', type : 'food' },
 *             { id : 4, name : 'Gin tonic', type : 'drinks' },
 *             { id : 5, name : 'Wine', type : 'drinks' },
 *             { id : 6, name : 'Beer', type : 'drinks' }
 *         ]
 *     })
 * });
 *```
 *
 * ## Shared Stores
 * More than one Combo may share a Store if they are required to draw their values from a shared
 * data set.
 *
 * The only limitation here is that the characteristics of the filter that is applied to the store
 * by typing are inherited from the __first__ combo. So for example, all would be
 * {@link #config-caseSensitive} or all case-insensitve, and all would use the same
 * {@link #config-filterOperator}.
 *
 * In the example below, all three email address inputs use the same store of recipients.
 *
 * {@inlineexample Core/widget/EmailMultiselect.js}
 *
 * This may be operated using the keyboard. `ArrowDown` opens the picker, ann then `ArrowDown` and
 * `ArrowUp` navigate the picker's options. `Enter` selects an active option in the picker. `Escape`
 * closes the picker.
 *
 * @extends Core/widget/PickerField
 * @classType combo
 * @inputfield
 */
export default class Combo extends PickerField {



    //region Config
    static get $name() {
        return 'Combo';
    }

    // Factoryable type name
    static get type() {
        return 'combo';
    }

    // Factoryable type aliases
    static get alias() {
        return 'combobox,dropdown';
    }

    static delayable = {
        filterOnInput : 0
    };

    static get configurable() {
        return {
            /**
             * Optionally a {@link Core.util.CollectionFilter Filter} config object which the combo should use for
             * filtering using the typed value.
             * This may use a `filterBy` property to test its `value` against any field in the passed record.
             * ```javascript
             * {
             *     type          : 'combo',
             *     store         : myStore,
             *     primaryFilter : {
             *         filterBy(record) {
             *             if (this.value == null) {
             *                 return true;
             *             }
             *             const value = this.value.toLowerCase();
             *
             *             // Match typed value with forename or surname
             *             return record.forename.toLowerCase().startsWith(value)
             *                 || record.surname.toLowerCase().startsWith(value);
             *         }
             *     }
             * }
             * ```
             * @config {CollectionFilterConfig}
             */
            primaryFilter : {},

            picker : {
                type                : 'list',
                floating            : true,
                scrollAction        : 'realign',
                itemsFocusable      : false,
                activateOnMouseover : true,
                align               : {
                    align    : 't0-b0',
                    axisLock : true
                },
                maxHeight  : 324,
                scrollable : {
                    overflowY : true
                },
                autoShow     : false,
                focusOnHover : false
            },

            /**
             * Configure as `true` to allow selection of multiple values from the dropdown list.
             *
             * Each value is displayed as a "Chip" to the left of the input area. Chips may be
             * selected using the `LEFT` and `RIGHT` arrow keys and deleted using the `DELETE` key
             * to remove values from the field. There is also a clickable close icon in each chip.
             *
             * Use {@link Core.widget.List#config-toggleAllIfCtrlPressed} to implement "select all" behaviour.
             *
             * ```javascript
             * {
             *     type   : 'combo',
             *     store  : myStore,
             *     picker : {
             *         toggleAllIfCtrlPressed : true
             *     }
             * }
             * ```
             *
             * @config {Boolean}
             * @default false
             */
            multiSelect : null,

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
             *  - An array whose first value provides the {@link #config-value} for the Combo and whose
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
             * Store used to populate items. Also accepts a Store config object
             * @prp {Core.data.Store|StoreConfig}
             */
            store : null,

            /**
             * Field used for item value when populating from store. Setting this to `null` will
             * yield the selected record as the Combo's {@link #property-value}.
             * @config {String|null}
             */
            valueField : undefined,

            /**
             * Field used for item text when populating from store
             * @config {String}
             * @default
             */
            displayField : 'text',

            /**
             * Width of picker, defaults to this combo's {@link #config-pickerAlignElement} width
             * @config {Number}
             */
            pickerWidth : null,

            /**
             * The minimum string length to trigger the filtering, only relevant when {@link #config-editable} is `true`.
             *
             * This defaults to `1` in the case of local filtering, but `4` if the
             * {@link #config-filterParamName} is set to cause remote dropdown loading.
             *
             * @config {Number}
             * @default
             */
            minChars : null,

            selected : null,

            /**
             * Template string used to render the list items in the dropdown list
             *
             * ```javascript
             * new Combo({
             *     listItemTpl : ({ text }) => `<div class="combo-color-box ${text}"></div>${text}`,
             *     editable    : false,
             *     items       : [
             *         'Black',
             *         'Green',
             *         'Orange',
             *         'Pink',
             *         'Purple',
             *         'Red',
             *         'Teal'
             *     ]
             * });
             * ```
             *
             * @config {Function}
             * @param {Core.data.Model} record The record representing the item being rendered
             * @returns {String}
             */
            listItemTpl : null,

            /**
             * Template function that can be used to customize the displayed value
             * @param {Core.data.Model} record The record to provide a textual value for.
             * @param {Core.widget.Combo} combo A reference to this Combo.
             * @config {Function}
             * @returns {String}
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
             * If the dropdown is to be populated with a filtered query to a remote server, specify the
             * name of the parameter to pass the typed string here. By default, the string is simply sent
             * as the value of the parameter. For special encoding, configure the combo with {@link #config-encodeFilterParams}
             * @config {String}
             */
            filterParamName : null,

            /**
             * A function which creates an array of values for the {#config-filterParamName} to pass
             * any filters to the server upon load.
             *
             * The default behaviour is just to set the parameter value to the filter's `value`,
             * but the filter can be fully encoded for example:
             *
             * ```javascript
             *    {
             *        encodeFilterParams(filters) {
             *            const result = [];
             *
             *            for (const { property, operator, value, caseSensitive } of filters) {
             *                result.push(JSON.stringify({
             *                    field : property,
             *                    operator,
             *                    value,
             *                    caseSensitive
             *                }));
             *           }
             *        return result;
             *    }
             * ```
             * @config {Function}
             */
            encodeFilterParams : filters => filters.map(f => f.value),

            /**
             * If `false`, filtering will be triggered once you exceed {@link #config-minChars}. To filter only when
             * hitting Enter key, set this to `true`;
             * @config {Boolean}
             */
            filterOnEnter : false,

            /**
             * Configure as `true` to hide the expand trigger. This is automatically set to `true` if
             * remote filtering is enabled by setting the {@link #config-filterParamName} config.
             * @config {Boolean}
             * @default false
             */
            hideTrigger : null,

            /**
             * This implies that the picker will display an anchor pointer, but also means that the picker will align closer
             * to the input field so that the pointer pierces the {@link #config-pickerAlignElement}
             * @config {Boolean}
             * @default false
             */
            overlayAnchor : null,

            /**
             * The delay in milliseconds to wait after the last keystroke before filtering the list.
             *
             * This is a minimum of 300ms for remote filtering to keep network requests manageable, and
             * defaults to 10ms for locally filtered stores.
             * @config {Number}
             * @default
             */
            keyStrokeFilterDelay : null,

            defaultAction : 'select',

            /**
             * How to query the store upon click of the expand trigger. Specify one of these values:
             *
             *  - `'all'` - Clear the filter and display the whole dataset in the dropdown.
             *  - `'last'` - Filter the dataset using the last filter value.
             *  - `null`/any other - Use the value in the input field to filter the dataset.
             *
             * @config {'all'|'last'|null}
             * @default
             */
            triggerAction : 'all',

            /**
             * The name of an operator type as implemented in {@link Core.util.CollectionFilter#config-operator}
             * to use when filtering the dropdown list based upon the typed value.
             *
             * This defaults to `'startsWith'`, but the `'*'` operator may be used to match all
             * values which _contain_ the typed value.
             *
             * Not used when {@link #config-filterParamName} is set to cause remote dropdown loading.
             * The exact filtering operation is up to the server.
             *
             * @default
             * @prp {'='|'!='|'>'|'>='|'<'|'<='|'*'|'startsWith'|'endsWith'|'isIncludedIn'}
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
             * By default, the picker is hidden on selection in single select mode, and
             * remains to allow more selections when {@link #config-multiSelect} is `true`.
             * Setting this to a `Boolean` value can override that default.
             * @config {Boolean}
             */
            hidePickerOnSelect : null,

            /**
             * A config object to configure the {@link Core.widget.ChipView} to display the
             * selected value set when {@link #config-multiSelect} is `true`.
             *
             * For example the {@link Core.widget.List#config-itemTpl} or
             * {@link Core.widget.ChipView#config-iconTpl} might be configured to display
             * richer chips for selected items.
             * @config {ChipViewConfig}
             */
            chipView : {
                $config : ['lazy', 'nullify'],

                value : {
                    type : 'combochipview'
                }
            },

            chipStore : {
                $config : ['lazy', 'nullify'],
                value   : {}
            },

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
             * Get/sets combo value, selects corresponding item in the list
             * Setting null clears the field.
             *
             * If {@link #config-multiSelect} is `true`, then multiple values may be passed as an array.
             * If the values are records, these become the selected record set held by {@link #property-valueCollection},
             * and the `value` yielded by this field is an array of all the {@link #config-valueField}s from the records.
             * @fires select
             * @fires action
             * @member {Object|Number|String} value
             */
            /**
             * The initial value of this Combo box. In single select mode (default) it's a simple string value, for
             * {@link #config-multiSelect} mode, it should be an array of record ids.
             * @config {String|Number|String[]|Number[]}
             * @default
             */
            value : null,

            valueCollection : {
                $config : ['nullify', 'lazy'],
                value   : {}
            },

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
            clearTextOnPickerHide : true,

            // set to true to destroy the old combo store when it is replaced with a new store
            destroyStore : false,

            /**
             * A key value which, when typed in a {@link #config-multiSelect} Combo, selects the
             * currently active item in the picker, and clears the input field ready for another
             * match to be typed.
             * @config {String}
             * @default
             */
            multiValueSeparator : ',',

            /**
             * If configured as `true`, this means that when an unmatched string is typed into the
             * combo's input field, and `ENTER`, or the {@link #config-multiValueSeparator} is typed,
             * a new record will be created using the typed string as the {@link #config-displayField}.
             *
             * If configured as a function, or the name of a function in the owning component hierarchy, the function
             * will be called passing the string and combo field instance and should return the record to add (if any).
             *
             * The new record will be appended to the store, and the value selected.
             *
             * If the Store is an {@link Core.data.AjaxStore}, the new record will be eiligible for
             * syncing to the database through its {@link Core.data.AjaxStore#config-createUrl createUrl}.
             *
             * If the `AjaxStore` is configured to {@link Core.data.AjaxStore#config-autoCommit autoCommit},
             * the record will be synced immediately. If the server does not accept the new addition,
             * the field is placed temporarily into an invalid state with a message that explains this.
             *
             * For example:
             *
             * ```javascript
             *     new Combo({
             *         label : 'Employee name',
             *         store : employees,
             *         createOnUnmatched(name, combo) {
             *             name = validateEmployeeName(name);
             *
             *             if (name) {
             *                 return new Employee({
             *                     name,
             *                     email : generateEmployeeEmail(name)
             *                 });
             *             }
             *             else {
             *                 combo.setError('Invalid new employee name');
             *             }
             *         }
             *     });
             * ```
             * @config {Function|String|Boolean}
             */
            createOnUnmatched : null,

            role : 'combobox',

            /**
             * Configure this as `true` to render the dropdown list as a permanently visible list
             * in the document flow immediately below the input area instead of as a popup.
             *
             * This also hides the expand trigger since it is not needed.
             * @config {Boolean}
             * @default false
             */
            inlinePicker : null,

            testConfig : {
                // So that locally filtered tests do not have to wait after type gestures.
                // Note that for remote filtering, we set a min of 300ms
                keyStrokeFilterDelay : 0
            },

            /**
             * Configure this as `true` and the items display field values will be localized. The display field values
             * need to be a locale string.
             * @config {Boolean}
             * @private
             * @default
             */
            localizeDisplayFields : false,

            /**
             * Provide a function that returns items to be shown in the combo's selector.
             * @config {Function}
             * @private
             */
            buildItems : null
        };
    }

    /**
     * A constant value for the {@link #config-triggerAction} config to indicate that clicking the trigger should
     * filter the dataset using the last filter query string, *not* the input field value.
     * @member {String} queryLast
     * @readonly
     * @static
     */
    static get queryLast() {
        return 'last';
    }

    //endregion

    construct(config) {
        super.construct(...arguments);
        // Ensure there will always be a store created if Combo is created without items/store
        if (!this.store) {
            this.items = [];
        }

        if (this.filterOnEnter) {
            this.hideTrigger = true;
        }
    }

    startConfigure(config) {
        this.usingRecordAsValue = config.valueField === null;

        super.startConfigure(...arguments);
    }

    afterConfigure() {
        super.afterConfigure();

        const me = this;

        if (!ObjectHelper.hasOwn(me, '_value')) {
            me._value = me.valueField === me.displayField ? '' : null;
            // _lastValue should stay keeping an initial value if any
            me._lastValue = me._lastValue || me._value;
        }
    }

    get childItems() {
        const
            { _chipView, _picker } = this,
            result = super.childItems;

        if (_chipView) {
            result.push(_chipView);
        }

        if (_picker) {
            result.push(_picker);
        }

        return result;
    }

    get innerElements() {
        const
            chipViewElement = this._chipView?.element,  // don't trigger chipView create on first compose()
            { input, inputElement } = this;

        if (chipViewElement) {
            // Once the input el is transplanted inside the chipView, normal compose() calls won't sync it, so we
            // have to do so now:
            DomSync.sync({
                targetElement : input,
                domConfig     : inputElement
            });
        }

        return [chipViewElement || inputElement];
    }

    updateInlinePicker(inlinePicker) {
        if (inlinePicker) {
            this.element.classList.add('b-inline-picker');

            // Force eager ingestion of picker
            this.getConfig('picker');

            // No expand trigger
            this.triggers.expand = null;

            this.pickerVisible = true;
        }
    }

    hidePicker() {
        if (!this.inlinePicker) {
            return super.hidePicker(...arguments);
        }
    }

    changeChipStore(chipStore) {
        if (chipStore && !(chipStore instanceof Store)) {
            chipStore = new Store(Store.mergeConfigs({
                storage : this.valueCollection
            }, chipStore));
        }

        return chipStore;
    }

    updateChipStore(store, was) {
        was?.destroy();
    }

    changeChipView(chipView, oldChipView) {
        const
            me = this;

        me.element.classList[chipView ? 'add' : 'remove']('b-uses-chipview');

        if (chipView) {
            const { input } = me;

            if (!me.chipStore) {
                me.chipStore = {};
            }

            return ComboChipView.reconfigure(oldChipView, chipView, {
                defaults : {
                    parent       : me,
                    insertBefore : input,
                    store        : me.chipStore,
                    closable     : !me.readOnly,

                    navigator : {
                        type           : 'combochipnavigator',
                        keyEventTarget : input
                    }
                }
            });
        }

        oldChipView?.destroy();
    }

    updateChipView(chipView) {
        const me = this;

        me._chipViewEventDetacher  = me._chipViewEventDetacher?.();

        me.chipStore = chipView?.store;

        if (chipView) {
            // Insert the input field
            chipView.element.appendChild(me.input);

            // Focus must flow into our field from the ChipView
            me._chipViewEventDetacher  = EventHelper.on({
                element   : chipView.element,
                mousedown : 'onChipViewMousedown',
                thisObj   : me
            });
        }
    }

    updateMultiSelect(multiSelect, oldMultiSelect) {
        const
            me             = this,
            {
                input,
                element
            }              = me,
            fixValue       = !me.isConfiguring;

        let { value } = me;

        element.classList[multiSelect ? 'add' : 'remove']('b-multiselect');

        if (multiSelect) {
            const
                { chipView }   = me,
                { parentNode } = input,
                chipViewEl  = chipView?.element;

            // If the input's parentNode is not the chipView's element, we need to restore that DOM arrangement
            if (chipViewEl && chipViewEl !== parentNode) {
                // This is where the chipView is created in the DOM but it may have been removed.
                parentNode.insertBefore(chipViewEl, input);
                chipViewEl.appendChild(input);
                me.chipView.refresh();
            }

            input.value = '';

            if (fixValue) {
                value = ArrayHelper.asArray(value);
            }
        }
        // else when !multiSelect, if the input's parentNode is the chipView's element, we need to put things back the
        // other way
        else {
            // avoid triggering lazy config if !multiSelect
            const
                chipView   = me._chipView,
                { parentNode } = input,
                chipViewEl = chipView?.element;

            if (chipViewEl === parentNode) {
                // Put the input back in its proper place
                chipViewEl.parentNode.insertBefore(input, chipViewEl);

                // When no longer multiSelect, remove the chipView from the DOM. We do not destroy it because derived
                // classes or the instance config may have specified chipView config options. If we destroyed it, we could
                // not then properly recreate it.
                chipViewEl.remove();
                element.classList.remove('b-uses-chipview');
            }

            if (fixValue && typeof value !== 'string') {
                value = value?.length ? value[0] : null;
            }
        }

        if (fixValue) {
            me.value = value;
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
            this._isUserAction = true;
            this.valueCollection.remove(records);
            this._isUserAction = false;
        }
    }

    updateFilterParamName(filterParamName) {
        if (this.hideTrigger !== false) {
            this.hideTrigger = Boolean(filterParamName);
        }
    }

    updateHideTrigger(hideTrigger) {
        this.element.classList[hideTrigger ? 'add' : 'remove']('b-hide-trigger');
    }

    //region Getters/setters

    updateKeyStrokeFilterDelay(delay) {
        this.filterOnInput.delay = delay;
    }

    updateReadOnly(readOnly) {
        super.updateReadOnly(...arguments);

        // Disable closing (removing) chips when we are read-only.
        this._chipView && (this._chipView.closable = !readOnly);
    }

    updateDisabled(disabled) {
        super.updateDisabled(...arguments);

        // Disable closing (removing) chips when we are disabled
        this._chipView && (this._chipView.closable = !disabled);
    }

    updateFilterOperator(filterOperator) {
        if (this.primaryFilter) {
            this.primaryFilter.operator = filterOperator;
        }
    }

    get minChars() {
        const minChars = this._minChars;

        if (minChars != null) {
            return minChars;
        }

        // If it's not actually set, default differently for remote filtering.
        return this.remoteFilter ? 4 : 1;
    }

    get validateFilter() {
        // Do not show the error if the user has the opportunity to add the typed filter string
        return this._validateFilter && !this.createOnUnmatched;
    }

    get items() {
        return this.store.allRecords;
    }

    updateBuildItems(fn) {
        if (fn) {
            this.items = fn.call(this);
        }
    }

    /**
     * Prepares items to work in attached menu (converts strings to items)
     * @private
     */
    changeItems(items) {
        const me = this;

        if (items == null) {
            if (me.store && !me.store.isItemStore) {
                return;
            }
            items = [];
        }

        if (me.buildItems && !items?.length) {
            items = me.buildItems();
        }

        if (items instanceof Store) {
            me.store = items;
            return;
        }

        const displayField = me.displayField;

        let itemModel,
            valueField = me.valueField,
            storeData;

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
                    if (typeof item === 'string' || typeof item === 'number') {
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

                if (item.selected) {
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

            if (!itemModel) {
                // angular prod build messes up "Foo = class extends Base" (https://github.com/bryntum/support/issues/6395)
                class ModelClass extends Model {
                    static get idField() {
                        // Need to use instance var and not rely on closure for cases where valueField changes
                        // (like first assigning [] and then ['a'] without configured valueField
                        return me.valueField;
                    }

                    static set idField(idField) {
                        super.idField = idField;
                    }

                    static get fields() {
                        return [valueFieldDefinition, displayField];
                    }
                }
                itemModel = ModelClass;
            }

            me.store = new Store({
                isItemStore          : true,
                data                 : storeData,
                idField              : valueField,
                // We frequently populate combos with data from other stores, don't want warnings for consuming local
                // records from those stores with generated ids
                verifyNoGeneratedIds : false,
                modelClass           : itemModel
            });
        }
    }

    get value() {
        const
            me                              = this,
            { valueCollection, valueField } = me;

        if (valueField == null) {
            return me.multiSelect ? valueCollection.values.slice() : valueCollection.first;
        }

        let value;

        if (me.multiSelect) {
            value = valueCollection.count ? valueCollection.map(r => r[valueField]) : (me._lastValue || []);
        }
        else {
            value = valueCollection.count ? valueCollection.first[valueField] : me._lastValue;
        }

        return value;
    }

    set value(value) {
        super.value = value;
    }

    // Documented in superclass.
    get needsInputSync() {
        // Syncing the input field to the internal value is only needed in a Combo
        // if there's no ChipView which reflects the value, and the input field
        // is *not* being used as a type-to-filter input.
        return this.usesChipView ? false : !this.editable;
    }

    changeValue(value, oldValue) {
        const me = this;

        if (value === oldValue) {
            // Sync without highlight
            me.syncInputFieldValue(true);
            return;
        }

        if (!me.multiSelect && Array.isArray(value) && value.length > 1) {
            throw new Error('Multiple values cannot be set to a non-multiSelect Combo');
        }

        // This forces promotion of the items config into a Store if it has not already been injected
        me.triggerConfig('items');

        // On programmatic value change, we need to clear the chip view selection.
        // Use the _property name in order not to call it into existence
        if (!me.inputting) {
            me._chipView?.selected.clear();
        }

        // Set an empty items array if no data or store was given
        if (!me.store) {
            me.items = [];
        }

        const
            {
                valueField,
                displayField,
                store,
                valueCollection,
                _picker
            }            = me,
            { storage }  = store,
            hidePicker   = me.hidePickerOnSelect ?? !me.multiSelect,
            isUserAction = me._isUserAction || _picker?._isUserAction || hidePicker && me.pickerVisible || false;

        // if not remoteFilter mode and AjaxStore has been used, try again to set a value after first store loading
        if (!me.remoteFilter && store.isAjaxStore && !store.count) {
            store.ion({ load : () => me.value = value, once : true, thisObj : me });
            // save value to make sure getter returns the correct data fot multiSelect while data loading
            me._lastValue = value;
            return;
        }

        let record;

        if (value != null) {
            // If value is set as an array, make sure to slice it to not mutate original array below
            const
                arrayPassed = Array.isArray(value),
                values      = arrayPassed ? value.slice() : [value];

            // It's a remote filter store, so we have to do a filter down to just match(es)
            // and add the result to the valueCollection
            if (me.remoteFilter) {
                // The null case will drop through and be processed as for local just by clearing the valueCollection
                if (value != null) {

                    if (ObjectHelper.isObject(value) || value.isModel) {
                        me.store.data = [value];
                        me.valueCollection.splice(0, me.valueCollection.count, me.store.first);
                    }
                    else {
                        const wasConfiguring = me.isConfiguring;

                        me.primaryFilter.setConfig({
                            value,
                            disabled : false
                        });

                        store.performFilter(true).then(() => {
                            if (me.isDestroyed) {
                                return;
                            }
                            const { isConfiguring } = me;

                            // Carry the wasConfiguring flag from the set value call frame so that
                            // if it's the configuring set value, we do not now fire the change event.
                            me.isConfiguring = wasConfiguring;
                            valueCollection.splice(0, valueCollection.count, store.allRecords);
                            me.isConfiguring = isConfiguring;
                        });
                    }
                    return;
                }
            }
            // Else, if it's a *locally* filtered store, then unfilter it, so we can do the value lookup.
            else if (store.isFiltered) {
                me.primaryFilter.disabled = true;
                store.filter();
            }

            for (let i = 0, len = values.length; i < len; i++) {
                let currentValue = values[i];

                if (currentValue instanceof Model) {
                    // The required record value may not yet be in the store. Add it if not.
                    // Be sure to look past filters when checking if value is already present.
                    if (!storage.includes(currentValue, true)) {
                        store.add(currentValue);
                    }
                }
                else {
                    const isObject = ObjectHelper.isObject(currentValue);

                    // If they passed a data object, match the valueField
                    if (isObject) {
                        currentValue = currentValue[store.modelClass.fieldMap[valueField].dataSource];
                    }

                    // Use the Store Collection's extra indices to quickly find a match.
                    // They may not be found if the current valueCollection state and
                    // filterSelected mean that some are filtered out, so check in
                    // valueCollection if not found in the Store.
                    record =
                        (storage.getBy(displayField, currentValue) ||
                        storage.getBy(valueField, currentValue)) ||
                        (valueCollection.getBy(displayField, currentValue) ||
                        valueCollection.getBy(valueField, currentValue));

                    // If it's a potentially non-unique index (such as the by-displayField index)
                    // then key lookups yield a Set. Use the first entry as the match.
                    if (record instanceof Set) {
                        record = [...record][0];
                    }

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

            const noMatches = !values.length;

            // Remove all old values, add new values in one shot.
            const vcGen = valueCollection.generation;
            valueCollection.splice(0, valueCollection.count, values);

            // If we got no matches, onValueCollectionChange will set the _value to null.
            // Tests specify that the _value should be set to the incoming unmatched value.
            // Handle the case that an array was passed.
            if (noMatches) {
                me._value = arrayPassed && value.length === 0 ? null : value;
                // _lastValue has to be updated here to have an actual value for syncInputFieldValue() and syncEmpty() below
                me._lastValue = me._value;
            }

            // If no change has fed through to onValueCollectionChange, just ensure the input matches.
            // Must be done last so that the fallback of using this._value if the passed value did
            // not match a record can be used.
            if (noMatches || valueCollection.generation === vcGen) {
                me.syncInputFieldValue();
            }

            me.syncEmpty();

            // If there were matches, onValueCollectionChange would have triggered the change event.
            // If not, we trigger it here.
            if (noMatches && !me.isConfiguring) {
                me.triggerFieldChange({
                    value,
                    oldValue,
                    userAction : isUserAction,
                    valid      : me.isValid
                });
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
                me.syncEmpty();
                if (!me.isConfiguring) {
                    me.triggerFieldChange({
                        value,
                        oldValue,
                        userAction : isUserAction,
                        valid      : me.isValid
                    });
                }
            }
        }

        me._lastValue = me._value;
    }

    hasChanged(oldValue, newValue) {
        if (this.multiSelect) {
            return !ObjectHelper.isEqual(oldValue, newValue);
        }

        return super.hasChanged(...arguments);
    }

    onComboStoreChange({ action }) {
        // Local filter-on-type is happening, do not sync the input value.
        if (action !== 'filter') {
            this.syncInputFieldValue(true);
        }
    }

    syncInputFieldValue(skipHighlight) {
        // We only sync the input's value if we are not using the chip view (DependencyField).
        // If we are multiselecting, our value is represented by a ChipView.
        // The ChipView automatically syncs itself with our valueCollection.
        // If the valueCollection gets updated silently, we may still need this function.
        if (this.usesChipView) {
            this.chipView?.refresh();
        }
        else {
            super.syncInputFieldValue(skipHighlight);
        }
    }

    get usesChipView() {
        return Boolean(this.multiSelect && this._chipView);
    }

    /**
     * Returns `true` if this field has no selected records.
     * @property {Boolean}
     * @readonly
     */
    get isEmpty() {
        return this.valueCollection.count === 0;
    }

    get inputValue() {
        // This must be evaluated first, and NOT moved to be directly used as the
        // second expression in the ternary. If called during configuration, this
        // will import the configured value from the config object and ensure the
        // value is matched against the store, and that the "selected" property is set.
        const me = this;

        let result = me.selected ? me.selected[me.displayField] : me.value;

        if (me.displayValueRenderer) {
            result = me.callback(me.displayValueRenderer, me, [me.selected, me]);
        }

        return result == null ? '' : result;
    }

    get nonEditableClickTarget() {
        return this.multiSelect && this.chipView?.element || super.nonEditableClickTarget;
    }

    /**
     * A {@link Core/util/Collection} which holds the currently selected records
     * from the store which dictates this field's value.
     *
     * Usually, this will contain one record, the record selected.
     *
     * When {@link #config-multiSelect} is `true`, there may be several records selected.
     * @member {Core.util.Collection} valueCollection
     * @readonly
     */
    changeValueCollection(valueCollection, oldValueCollection) {
        oldValueCollection?.destroy();

        if (valueCollection) {
            if (!valueCollection.isCollection) {
                valueCollection = new Collection({
                    internalListeners : {
                        noChange : 'onValueCollectionNoChange',
                        change   : 'onValueCollectionChange',
                        prio     : -1000, // The ChipView must react to changes first.
                        thisObj  : this
                    }
                });
            }
            return valueCollection;
        }
    }

    changePrimaryFilter(primaryFilter) {
        if (primaryFilter.isCollectionFilter) {
            primaryFilter.setConfig({
                disabled      : true,
                property      : this.displayField,
                operator      : this.filterOperator,
                caseSensitive : this.caseSensitive
            });
        }
        else {
            if (typeof primaryFilter === 'function') {
                primaryFilter = {
                    filterBy : primaryFilter
                };
            }

            // This is the filter that performs filtering on typing.
            primaryFilter = new Filter({
                // Need an id to replace any existing combo filter on the store.
                id            : 'primary', 
                disabled      : true,
                property      : this.displayField,
                operator      : this.filterOperator,
                caseSensitive : this.caseSensitive,

                ...primaryFilter
            });
        }

        return primaryFilter;
    }

    changeStore(store) {
        const
            me           = this,
            storeFilters = [],
            {
                valueCollection,
                keyStrokeFilterDelay,
                filterParamName
            }            = me;

        if (Array.isArray(store)) {
            me.items = store;
            return;
        }

        let remoteFilter;

        if (store) {
            // We are using an Store which we do not own.
            if (store.isStore) {
                const sharedFilter = store.filters.get('primary');

                if (me.remoteFilter) {
                    store.filterParamName = filterParamName;
                }
                remoteFilter = store.remoteFilter || store.restfulFilter;

                // If the Store is from another combo, we also share the incoming filter
                if (sharedFilter) {
                    me.primaryFilter = sharedFilter;
                }
                else {
                    storeFilters.push(me.primaryFilter);
                }
            }
            else {
                if (typeof store === 'string') {
                    store = Store.getStore(store);
                }
                else {
                    store = new (store.readUrl ? AjaxStore : Store)(store);
                    me.destroyStore = true;
                }

                remoteFilter = me.remoteFilter || store.restfulFilter;

                if (remoteFilter && filterParamName) {
                    store.filterParamName = filterParamName;

                    if (me.encodeFilterParams) {
                        store.encodeFilterParams = me.encodeFilterParams;
                    }
                }

                // We add our primary filter to stores we own
                storeFilters.push(me.primaryFilter);
            }

            // If no value field provided, read it off of the Store's modelClass
            // Unless we want to use full record as the value
            if (!me.valueField && !me.usingRecordAsValue) {
                me.valueField = store.modelClass.idField;
            }

            // Filtering of already-selected values is always done locally.
            // So if we are filtering remotely, add filter directly to Store's Collection
            if (me.filterSelected) {
                const selectedItemsFilter = r => !me.containsFocus || !valueCollection.includes(r);

                if (remoteFilter) {
                    store.storage.autoFilter = true;
                    store.storage.addFilter({
                        id       : `${me.id}-selected-filter`,
                        filterBy : selectedItemsFilter
                    });
                }
                else {
                    storeFilters.push(selectedItemsFilter);
                    store.reapplyFilterOnAdd = true;
                }
            }

            // Allow fast lookup in the valueCollection by value or displayed value.
            // We add these now because we are now guaranteed to have inferred
            // displayField and startField from any passed items array items is
            // promoted to a Store.
            valueCollection.addIndex({
                property : me.displayField,
                unique   : false
            });
            valueCollection.addIndex({
                property : me.valueField,
                unique   : true
            });

            // *Add* our filters in case the store already has its own filters
            storeFilters.forEach(f => store.addFilter(f, true));

            // If there's no configured delay, use sensible defaults.
            // AjaxStore, we don't want a network request fired off on every single keystroke.
            // Local store, delay it just a little to save data and DOM churn.
            if (remoteFilter) {
                // We enforce at least a 300ms delay when firing off network requests
                me.keyStrokeFilterDelay = Math.max(300, keyStrokeFilterDelay || 0);
            }
            else {
                me.keyStrokeFilterDelay = keyStrokeFilterDelay ?? 10;
            }
        }

        return store;
    }

    updateStore(store, oldStore) {
        const
            me = this,
            { _picker } = me;

        let storeListeners;

        if (me.destroyStore && oldStore) {
            oldStore.destroy();
        }

        if (_picker) {
            _picker.store = store;
        }

        // Allow fast lookup by value or displayed value
        store.storage.addIndex({
            property : me.displayField,
            unique   : false
        });
        store.storage.addIndex({
            property : me.valueField,
            unique   : true
        });

        storeListeners = {
            filter : 'onStoreFilter'
        };

        if (me.displayValueRenderer) {
            (storeListeners || (storeListeners = {})).change = 'onComboStoreChange';
        }

        me.detachListeners('store');

        // Update selected records collection to match what is in Store.
        // Incoming records of the same id will be replaced in the valueCollection.
        // Records in the valueCollection which are no longer in the Store will
        // be removed.
        store?.storage && me.valueCollection.match(store.storage);

        // Ensure UI matches potential new record values.
        me.syncInputFieldValue();

        if (storeListeners) {
            storeListeners.name = 'store';
            storeListeners.thisObj = me;

            store.ion(storeListeners);
        }
    }

    get remoteFilter() {
        // Don't use "this.store" since we need to run during changeStore()
        return Boolean(this.filterParamName || this._store?.restfulFilter);
    }

    /**
     * Get selected record.
     * @property {Core.data.Model[]}
     * @readonly
     */
    get record() {
        return this.selected;
    }

    /**
     * Get the selected record(s).
     * @property {Core.data.Model[]}
     * @readonly
     */
    get records() {
        return this.valueCollection.values.slice();
    }

    get selected() {
        const { store } = this;

        // If multiSelect, choose as a single selected value the item which comes first
        // in the dropdown.
        return this.multiSelect ? this.valueCollection.values.slice().sort((l, r) => store.indexOf(l) - store.indexOf(r))[0] : this.valueCollection.first;
    }

    //endregion

    //region Value handling

    /**
     * Check if field value is valid
     * @internal
     */
    onEditComplete() {
        const me             = this,
            selectionCount = me.valueCollection.count;

        super.onEditComplete();

        // Ensure the input area matches the selected value
        if (selectionCount) {
            me.clearError(errorValidateFilter);
            me.syncInputFieldValue();
        }
        if (me.required && !selectionCount) {
            me.setError(errorFieldRequired);
        }
    }

    //endregion

    //region Events

    /**
     * User clicked trigger icon, toggle list.
     * @private
     */
    onTriggerClick(event) {
        const
            me             = this,
            activatePicker = 'key' in event;

        // Bail out if enter key is required to trigger a filter
        if (me.ignoreTriggerClick || (me.remoteFilter && me.filterOnEnter)) {
            return;
        }

        if (me.pickerVisible) {
            me.hidePicker();
        }
        else if (!me.readOnly && !me.disabled) {
            switch (me.triggerAction?.toLowerCase()) {
                case 'all':
                    me.doFilter(null, activatePicker);
                    break;
                case 'last':
                    me.doFilter(me.lastQuery, activatePicker);
                    break;
                default:
                    me.doFilter(me.input.value, activatePicker);
            }
        }
    }

    /**
     * User types into input field in editable combo, show list and filter it.
     * @private
     */
    internalOnInput(event) {
        const me = this;

        me.syncEmpty();
        me.syncInputWidth();

        // This method may be buffered by keyStrokeFilterDelay milliseconds
        me.filterOnInput(event);

        /**
         * User typed into the field. Please note that the value attached to this event is the raw input field value and
         * not the combos value
         * @event input
         * @param {Core.widget.Combo} source The combo
         * @param {String} value Raw input value
         */
        me.trigger('input', { value : me.input.value, event });
    }

    filterOnInput(event) {
        const
            me        = this,
            { value } = event.type === 'input' ? event.target : me.input,
            inputLen  = value.length;

        me.inputting = true;

        // If the picker is inline, as opposed to a floating popup,
        // or the minChars limit is met (or its a filter on enter gesture)
        // then perform the filtering.
        if (me.inlinePicker || (inputLen >= me.minChars && (!me.filterOnEnter || event.key === 'Enter'))) {
            me.doFilter(value);
        }
        else {
            // During typing, the field is invalid
            if (me.validateFilter && !me.remoteFilter) {
                me[inputLen ? 'setError' : 'clearError'](errorValidateFilter);
            }
            me.hidePicker();
        }
        me.inputting = false;

    }

    syncInputWidth() {
        const me = this;

        if (me.usesChipView) {
            const
                input        = me.input,
                // padding on the input el won't change, so cache the measurement:
                inputPadding = me._inputPadding ||
                    (me._inputPadding = DomHelper.getEdgeSize(input, 'padding', 'lr')),
                value        = input.value || '',
                width        =
                    // +'W' to avoid text getting clipped or horizontal scrolling
                    DomHelper.measureText(value + 'W', input, false, me.element) +
                    inputPadding.width;

            // Normally the input is given "flex: 1 1 0px" so it will fill the space
            // so we just need to adjust the flex-basis to ensure the input is at least
            // as long as the text. Since it is also flex-shrink, it will not become
            // any larger than one "row".
            input.style.flex = `1 1 ${Math.ceil(width)}px`;
        }
    }

    doFilter(queryString, activatePicker) {
        const
            me = this,
            {
                store,
                // Force the lazy config to create picker since the List needs to add its beforeLoad listener
                picker
            } = me,
            disableFilter = queryString == null || queryString === '';

        me.lastQuery = queryString;

        me.primaryFilter.setConfig({
            value    : queryString,
            disabled : disableFilter
        });

        if (me.remoteFilter) {
            store.clear(true);
        }

        const onAfterFilter = () => {
            const { navigator, isVisible } = picker;

            if (store.count) {
                // If we are filtering, activate the first match
                if (!disableFilter && navigator) {
                    navigator.activeItem = 0;
                }
            }
            // If we were actively *locally* filtering on a string but there were no matches...
            //  Ensure there's no orphaned active item in the picker.
            //  If we are validateFilter: true, then mark as invalid even though we
            //  may have an underlying valid selected value.
            else if (!me.remoteFilter && !disableFilter) {
                if (navigator) {
                    navigator.activeItem = null;
                }
                if (me.validateFilter) {
                    me.setError(errorValidateFilter);
                }
            }

            // filtering will have changed the store count.
            // If the height was set due to constraining, this may need to be released.
            isVisible && picker.realign();
        };

        // We have the property 'filterPromise' while a filter operation is in flight.
        (me.filterPromise = store.filter())?.then(() => {
            me.filterPromise = null;
            onAfterFilter();
        });

        if (!me.inlinePicker) {
            if (picker?.isVisible) {
                // If aligned above, filtering will change its height so will need realigning
                if (picker.lastAlignSpec.zone === 0) {
                    picker.realign();
                }
            }
            else {
                me.showPicker(activatePicker);
            }
        }

        if (!me.filterPromise) {
            onAfterFilter();
        }
    }

    onStoreFilter({ source : store }) {
        const
            me        = this,
            picker    = me._picker,
            dataset   = picker?.element.dataset,
            { count } = store;

        if (me.remoteFilter) {
            // If we are filtering, activate the first match
            if (count) {
                if (picker) {
                    picker.navigator.activeItem = 0;
                }
            }
            // Invalid if no matches after filtering
            else {
                if (me.validateFilter) {
                    me.setError(errorValidateFilter);
                }
            }

            // If we have selection, evict selected items from the newly remote-filtered list
            if (me.filterSelected && me.valueCollection.count) {
                store.storage.onFiltersChanged({ action : 'splice', oldCount : 1 });

                // Store does not react to Collection filtering yet because it does its own filtering and
                // then fires its own event. So we have to refresh the picker to hide the selected items.
                if (picker) {
                    picker.refresh();
                }
            }
        }

        // If createOnUnmatched, we add a hint to add the value
        if (dataset) {
            if (me.createOnUnmatched && !count) {
                dataset.addNewValue = me.L('L{addNewValue}')(me.primaryFilter.value);
            }
            else {
                delete dataset?.addNewValue;
            }
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
        const me              = this,
            { multiSelect, _picker } = me,
            hidePicker      = me.hidePickerOnSelect ?? !multiSelect,
            record          = multiSelect ? valueCollection.values.slice() : valueCollection.first,
            records         = valueCollection.values.slice(),
            isUserAction    = me._isUserAction || _picker?._isUserAction || hidePicker && me.pickerVisible || false,
            oldValue        = me._value;

        if (hidePicker) {
            me.hidePicker();
        }

        if (!valueCollection.count && me.required) {
            me.setError(errorFieldRequired);
        }
        else {
            me.clearError(errorFieldRequired);
            me.clearError(errorValidateFilter);
        }

        if (me.validateFilter && record) {
            me.clearError(errorValidateFilter);
        }

        // Re-evaluate *local* filtering so that selected items are filtered out of the dropdown.
        // For remote filtering, we programmatically add a filter to the store's storage
        if (me.filterSelected) {
            if (me.remoteFilter) {
                me.store.storage.onFiltersChanged({ action : 'splice', oldCount : 1 });

                // Store does not react to Collection filtering yet because it does its own filtering and
                // then fires its own event. So we have to refresh the picker to hide the selected items.
                if (me._picker) {
                    me._picker.refresh();
                }
            }
            else {
                me.store.filter();
            }
        }

        // Clear the cached value so that there's no fallback when we read back the value below
        me._value = null;
        me._lastValue = null;

        // Cache the value for use by our change handler next time, and also so that
        // if we just cleared the valueCollection, the fallback to ._value will be correct
        const value = me.cacheCurrentValue(me.value);

        me.syncInputFieldValue();
        me.syncEmpty();

        if (!me.isConfiguring) {
            me.triggerFieldChange({
                value,
                oldValue,
                userAction : isUserAction,
                valid      : me.isValid
            });

            /**
             * An item in the list was selected
             * @event select
             * @param {Core.widget.Combo} source The combo
             * @param {Core.data.Model} record Selected record
             * @param {Core.data.Model[]} records Selected records as an array if {@link #config-multiSelect} is `true`
             * @param {Boolean} userAction `true` if the value change is due to user interaction.
             */
            me.trigger?.('select', { record, records, userAction : isUserAction });

            /**
             * The default action was performed (an item in the list was selected)
             * @event action
             * @param {Core.widget.Combo} source The combo
             * @param {*} value The {@link #config-valueField value} of the selected record
             * @param {Core.data.Model} record Selected record
             * @param {Core.data.Model[]} records Selected records as an array if {@link #config-multiSelect} is `true`
             * @param {Boolean} userAction `true` if the value change is due to user interaction.
             */
            if (me.defaultAction === 'select') {
                me.trigger('action', { value, record, records, userAction : isUserAction });
            }
        }
    }

    // Caching a copy of current value, which can be changed by subclasses (see AssignmentField for reference)
    cacheCurrentValue(v) {
        return this._value = v;
    }

    /**
     * This listens for when a record from the list is selected, but is already part of
     * the selection and so the {@link #property-valueCollection} rejects that as a no-op.
     * At this point, the user will still expect the picker to hide.
     * @param {Object} event The noChange event containing the splice parameters
     * @private
     */
    onValueCollectionNoChange({ toAdd }) {
        if (!this.inlinePicker && !this.multiSelect && toAdd.length && this.pickerVisible) {
            this.picker.hide();
        }
    }

    //endregion

    //region Picker

    showPicker() {
        const
            me         = this,
            { picker } = me;

        if (me.readOnly || me.inlinePicker) {
            return;
        }

        picker.multiSelect = me.multiSelect;

        super.showPicker(...arguments);

        // Once we have access to the anchor size, overlay the anchor pointer over the target if configured to do so.
        if (me.overlayAnchor && !picker.align.offset) {
            picker.align.offset = -picker.anchorSize[1];
            picker.realign();
        }

        // Picker type might have been reconfigured from being a List
        if (picker.restoreActiveItem) {
            // Activate and make visible an active item.
            // If we are multiSelect, only pass the selected value if the user has not
            // previously navigated to her item of interest.
            // If single select, it's always value to navigate to the value item.
            // In either case, if there is no target, navigate to item 0.
            if (me.multiSelect) {
                picker.restoreActiveItem(picker.navigator?.previousActiveItem || me.selected || 0, true);
            }
            else {
                picker.restoreActiveItem(me.selected || 0, true);
            }
        }

        me.input.focus();
    }

    /**
     * Creates default picker widget
     *
     * @internal
     */
    changePicker(picker, oldPicker) {
        const
            me          = this,
            pickerWidth = me.pickerWidth || picker?.width,
            config      = List.mergeConfigs({
                owner        : me,
                store        : me.store,
                selected     : me.valueCollection,
                multiSelect  : me.multiSelect,
                cls          : me.listCls,
                displayField : me.displayField,
                forElement   : me[me.pickerAlignElement],
                align        : {
                    matchSize : pickerWidth == null,
                    anchor    : me.overlayAnchor,
                    target    : me[me.pickerAlignElement],
                    // Reasonable minimal height to fit few combo items below the combo.
                    // When height is not enough, list will appear on top. That works for windows higher than 280px,
                    // worrying about shorter windows sounds overkill.
                    // We cannot use relative measures here, each combo list item is ~40px high
                    minHeight : me.inlinePicker ? null : Math.min(3, me.store.count) * 40
                },
                [me.listItemTpl ? 'itemTpl' : undefined] : me.listItemTpl,
                width                                    : pickerWidth,
                navigator                                : {
                    keyEventTarget : me.input
                }
            }, picker);

        if (me.inlinePicker) {
            Object.assign(config, {
                floating            : false,
                align               : null,
                activateOnMouseover : false,
                maxHeight           : null,
                appendTo            : me.element
            });
        }

        picker = List.reconfigure(oldPicker, picker ? config : null, me);

        if (picker) {
            picker.element.classList.add('b-combo-picker');
            picker.element.dataset.emptyText = me.emptyText ? me.L(me.emptyText) : me.L('L{noResults}');

            // We have to handle the click on "Add new value" when createOnUnmatched is set
            // because it's not a real list item, it's a repurposing of the .b-empty:after pseudo el.
            picker.ion({
                navigate : 'onPickerNavigate',
                thisObj  : me
            });

            // "Add new *" option is rendered as a ::before element, therefore there would be no navigation event.
            // We need separate mousedown listener on the list element
            EventHelper.on({
                element     : picker.element,
                pointerdown : event => me.onPickerNavigate({ event }),
                thisObj     : me
            });
        }

        return picker;
    }

    onPickerNavigate({ event }) {
        // It's a click on the "Add new value" prompt
        if (event.target.matches('[data-add-new-value]')) {
            this.addNewRecord(this.primaryFilter.value);
        }
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

    async internalOnKeyEvent(keyEvent) {
        const
            me       = this,
            value    = me.input[me.inputValueAttr],
            inputLen = value.length,
            { key }  = keyEvent,
            {
                store,
                _picker : picker,
                multiSelect
            }        = me;

        // Typing the multiValueSeparator character selects the active list item, or
        // if there is no active item, and we are creating new records, creates the selected item.
        // The picker may not be a List with a Navigator.
        // We can only honour this functionality for normal, unoverridden List Pickers.
        // Some subclasses use Grids as pickers.
        if (keyEvent.type === 'keydown' && picker?.isVisible && picker.navigator) {
            const { activeItem } = picker.navigator;

            // If they type `,` in a multiSelect, it means add the active matched item if any
            if (activeItem && multiSelect && key === me.multiValueSeparator) {
                me.input.value = '';
                me.primaryFilter.setConfig({
                    value    : '',
                    disabled : true
                });
                store.filter();
                picker.onItemClick(activeItem, keyEvent);
                picker.hide();
                keyEvent.preventDefault();
                return;
            }

            // Else, if there's no matched item and we are creating new records for unmatched keys
            // then `,` and Enter add a new record with the string as the displayField
            if (!activeItem && me.createOnUnmatched && (multiSelect && key === me.multiValueSeparator || key === 'Enter')) {
                keyEvent.preventDefault();
                await me.addNewRecord(value);
                return;
            }
        }

        super.internalOnKeyEvent(...arguments);

        if (keyEvent.type === 'keydown' && key === 'Enter' && me.filterOnEnter && inputLen >= me.minChars) {
            keyEvent.stopPropagation();
            me.filterOnInput.now(keyEvent);
        }
    }

    async addNewRecord(value) {
        const
            me = this,
            {
                store,
                _picker : picker,
                valueCollection,
                multiSelect,
                primaryFilter
            }  = me,
            remoteAutoCommit = store.remoteFilter && store.autoCommit;

        // We have to wait for remote filtering to finish before we add a new record to the added Bag
        // because store load (filter is a load) clears added records, and if the load returned during
        // the create commit that would invalidate the upcoming create return values by leaving no added
        // records to update with correct new IDs, and so the new record would appear to still
        // be a phantom, and therefore we would throw an error that the server has not accepted the addition..
        if (me.filterPromise) {
            await me.filterPromise;
        }

        // We can do this early if it's *NOT* a remote filtered, auto-committing AjaxStore.
        // If remote filtered and autoCommit, this will instigate a load which cannot be
        // concurrent with the upcoming auto commit.
        if (!remoteAutoCommit) {
            primaryFilter.setConfig({
                value    : '',
                disabled : true
            });
            store.filter();
        }
        const [newRecord] = store.add(me.callback(me.createOnUnmatched, me, [value, me]));

        // It's an AjaxStore which is autoCommitting the new record to its createUrl
        // We wait to see if it's successful.
        if (store.isCommitting) {
            let error;

            try {
                await store.commitPromise;
            }
            catch (exception) {
                error = exception.response?.parsedJson?.error;
            }

            // If the sync from the server did not return a concrete id, the record gets
            // left as phantom, so we have to remove it.
            if (newRecord.isPhantom) {
                me.clearError();
                me.setError(error || errorRecordNotCommitted, false, true);
                store.remove(newRecord);

                // AjaxStore adds to its removed Bag here because it clears down the addded Bag even
                // if not all added records were successfully synced, and remove adds to removed Bag
                // if the  record is *not* in the added Bag.
                store.removed.remove(newRecord);
            }
        }

        // Have to wait until now if its a remote filtered, auto-committing AjaxStore.
        if (remoteAutoCommit) {
            primaryFilter.setConfig({
                value    : '',
                disabled : true
            });
            store.filter();
        }

        me.input.value = '';

        // If an AjaxStore's sync of the add failed, do not select the new record
        if (store.includes(newRecord)) {
            // Append for multiSelect, or replace for non-multiSelect
            valueCollection.splice(multiSelect ? valueCollection.count : 0, multiSelect ? 0 : valueCollection.count, newRecord);
        }
        picker?.hide();
    }

    changeCreateOnUnmatched(createOnUnmatched) {
        if (createOnUnmatched === true) {
            createOnUnmatched = this.defaultRecordCreater;
        }
        return createOnUnmatched;
    }

    defaultRecordCreater(value) {
        return this.store.createRecord({
            [this.displayField] : value
        });
    }

    updateLocalization() {
        super.updateLocalization();
        const
            me                            = this,
            { displayField }              = me;
        let { localizedDisplayFieldsMap } = me;

        if (me.localizeDisplayFields === true) {

            // Create a map in which to save the original locale strings.
            if (!localizedDisplayFieldsMap) {
                me.localizedDisplayFieldsMap = localizedDisplayFieldsMap = new Map();
            }

            if (!me.store && me.buildItems) {
                me.items = me.buildItems();
            }

            for (const item of me.items) {
                // Uses .id an unique identifier
                if (item.id) {
                    let localeString = localizedDisplayFieldsMap.get(item.id);

                    // If not already saved, save the locale string in the map
                    if (!localeString && item[displayField]?.startsWith('L{')) {
                        localeString = item[displayField];
                        localizedDisplayFieldsMap.set(item.id, localeString);
                    }

                    // If a locale string is provided, localize it and set it to items display field
                    if (localeString) {
                        item[displayField] = me.L(localeString);
                    }
                }
            }
            // Update the current value of the combo
            me.syncInputFieldValue();
        }
    }
}

class ComboChipView extends ChipView {
    static get $name() {
        return 'ComboChipView';
    }

    static get type() {
        return 'combochipview';
    }

    static get defaultConfig() {
        return {
            closeHandler : 'up.onChipClose',

            itemsFocusable : false,

            multiSelect : true,

            itemTpl(record) {
                return StringHelper.encodeHtml(record.getValue(this.owner.displayField));
            },

            scrollable : {
                overflowY : 'auto'
            }
        };
    }
}

class ComboChipNavigator extends Navigator {
    static get $name() {
        return 'ComboChipNavigator';
    }

    // Factoryable type name
    static get type() {
        return 'combochipnavigator';
    }

    static get configurable() {
        return {
            allowShiftKey : true
        };
    }

    onTargetClick(clickEvent) {
        const
            me   = this,
            item = clickEvent.target.closest(me.itemSelector);

        // Only activate the item if the click was not on the close icon
        if (item && !clickEvent.target.classList.contains('b-close-icon')) {
            if (!clickEvent.shiftKey && !item.contains(clickEvent.target.closest('[data-noselect]'))) {
                me.ownerCmp.selected.clear();
            }
            // Our own updateActiveItem also selects because on superclass *key* navigation
            // (which is async on scroll end), it sets activeItem, and we select at that time.
            // So set a flag which disables this.
            me.inClickHandler = true;
            me.activeItem = item;
            me.inClickHandler = false;
        }
    }

    onKeyDown(keyEvent) {
        // ENTER does not toggle selectedness in a ChipView.
        // ChipView's selection is bound to navigation.
        // Ignore key presses not at the edge of the input field, those are handled by the input field (to avoid
        // left/right from both moving the cursor and navigating the chips)
        if (keyEvent.key !== 'Enter' && !keyEvent.target.selectionStart && !keyEvent.target.selectionEnd) {
            super.onKeyDown(keyEvent);
        }
    }

    updateActiveItem(activeItem, oldActiveItem) {
        const chipView = this.ownerCmp;

        super.updateActiveItem(activeItem, oldActiveItem);

        // Selection simply follows navigation in a ChipView
        if (activeItem && !this.inClickHandler) {
            chipView.selected.add(chipView.getRecordFromElement(activeItem));
        }
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

// Register this widget type and associated classes with their Factories
Combo.initClass();
ComboChipView.initClass();
ComboChipNavigator.initClass();
