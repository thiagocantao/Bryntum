import Widget from './Widget.js';
import ArrayHelper from '../helper/ArrayHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import Layout from './layout/Layout.js';
import DomHelper from '../helper/DomHelper.js';
import './Ripple.js';
import Bag from '../util/Bag.js';

/**
 * @module Core/widget/Container
 */

const
    emptyObject  = Object.freeze({}),
    emptyArray   = Object.freeze([]),
    returnWeight = i => i.weight,
    // Allowing string weights (used by subgrids for backwards compatibility)
    sortByWeight = ({ weight : a }, { weight : b }) => {
        if (typeof a === 'string' || typeof b === 'string') {
            return String(a).localeCompare(String(b));
        }

        // Items without weight sort last
        return (a ?? Number.MAX_SAFE_INTEGER) - (b ?? Number.MAX_SAFE_INTEGER);
    },
    isNotHidden = w => !w.hidden;

/**
 * Widget that can contain other widgets. Layout is flexbox by default, see the {@link #config-layout} config.
 *
 * ```javascript
 * // create a container with two widgets
 * let container = new Container({
 *     items : [
 *         { type : 'text', label : 'Name' },
 *         { type : 'number', label : 'Score' }
 *     ]
 * });
 * ```
 *
 * Containers can have child widgets added, or removed during their lifecycle to accommodate business needs.
 *
 * For example:
 *
 *  ```javascript
 *  myTaskPopup.on({
 *      beforeShow() {
 *          if (task.type === task.MASTER) {
 *              // Insert the childTask multiselect before the masterTask field
 *              myPopyup.insert(childTaskMultiselect, masterTaskField)
 *
 *              // We don't need this for master tasks
 *              myPopup.remove(masterTaskField);
 *          }
 *          else {
 *              // Insert the masterTask combo before the childTask multiselect
 *              myPopyup.insert(masterTaskField, childTaskMultiselect)
 *
 *              // We don't need this for child tasks
 *              myPopup.remove(childTaskMultiselect);
 *          }
 *      }
 *  });
 * ```
 *
 * @extends Core/widget/Widget
 * @classType container
 * @externalexample widget/Container.js
 */
export default class Container extends Widget {

    static get $name() {
        return 'Container';
    }

    // Factoryable type name
    static get type() {
        return 'container';
    }

    static get configurable() {
        return {
            /**
             * An object containing typed child widget config objects or Widgets. May also be specified
             * as an array.
             *
             * If configured as an Object, the property names are used as the child component's
             * {@link Core.widget.Widget#config-ref ref} name, and the value is the child component's config object.
             *
             * ```javascript
             *  new Panel({
             *      title    : 'Test Panel',
             *      floating : true,
             *      centered : true,
             *      width    : 600,
             *      height   : 400,
             *      layout   : 'fit',
             *      items    : {
             *          tabs : {
             *              type : 'tabpanel',
             *              items : {
             *                  general : {
             *                      title : 'General',
             *                      html  : 'General content'
             *                  },
             *                  details : {
             *                      title : 'Details',
             *                      html  : 'Details content'
             *                  }
             *              }
             *          }
             *      }
             *  }).show();
             * ```
             *
             * The order of the child widgets is determined by the order they are defined in `items`, but can also be
             * affected by configuring a {@see Core.widget.Widget#config-weight weight} on one or more widgets.
             *
             * @config {Object|Object[]|Core.widget.Widget[]}
             */
            items : null,

            /**
             * An array of {@link #config-items child item} _config objects_ which is to be converted into
             * instances only when this Container is rendered, rather than eagerly at construct time.
             *
             * _This is mutually exclusive with the {@link #config-items} config._
             *
             * @config {Object|Object[]|Core.widget.Widget[]}
             */
            lazyItems : {
                $config : ['lazy'],
                value   : null
            },

            /**
             * A config object containing default settings to apply to all child widgets.
             * @config {Object}
             */
            defaults : null,

            defaultType : 'widget',

            /**
             * The CSS style properties to apply to the {@link Core.widget.Widget#property-contentElement}.
             *
             * By default, a Container's {@link Core.widget.Widget#property-contentElement} uses flexbox layout, so this
             * config may contain the following properties:
             *
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-direction">flexDirection</a> default '`row`'
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-wrap">flexWrap</a>
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-flow">flexFlow</a>
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/justify-content">justifyContent</a>
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-items">alignItems</a>
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-content">alignContent</a>
             * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/place-content">placeContent</a>
             * @config {Object}
             */
            layoutStyle : null,

            /**
             * An optional CSS class to add to child items of this container.
             * @config {String}
             */
            itemCls : null,

            /**
             * The short name of a helper class which manages rendering and styling of child items.
             *
             * By default, the only special processing that is applied is that the Container class's
             * {@link #config-itemCls} is added to child items.
             *
             * Containers use CSS flexbox in its default configuration to arrange child items. You may either use the
             * {@link #config-layoutStyle} configuration to tune how child items are layed out, or use one of the built
             * in helper classes which include:
             *
             *  - `card` Child items are displayed one at a time, size to fit the
             *  {@link Core.widget.Widget#property-contentElement} and are slid in from the side when activated.
             * @config {String}
             */
            layout : 'default',

            // TODO: deprecate namedItems, use items instead
            /**
             * An object containing named config objects which may be referenced by name in any {@link #config-items}
             * object. For example, a specialized {@link Core.widget.Menu Menu} subclass may have a `namedItems` default
             * value defined like this:
             *
             * ```javascript
             *  namedItems : {
             *      removeRow : {
             *          text : 'Remove row',
             *          onItem() {
             *              this.ownerGrid.remove(this.ownerGrid.selectedRecord);
             *          }
             *      }
             *  }
             * ```
             *
             * Then whenever that subclass is instantiated and configured with an {@link #config-items} object, the
             * items may be configured like this:
             *
             * ```javascript
             *  items : {
             *      removeRow : true,   // The referenced namedItem will be applied to this
             *      otherItemRef : {
             *          text : 'Option 2',
             *          onItem() {
             *          }
             *      }
             * }
             * ```
             * @config {Object}
             */
            namedItems : null,

            /**
             * Specify `true` for a container used to show text markup. It will apply the CSS class `b-text-content`
             * which specifies a default max-width that makes long text more readable.
             * 
             * This CSS class is automatically removed if the container adds/defines child Widgets.
             * @config {Boolean}
             * @default
             */
            textContent : true,

            /**
             * Record whose values will be used to populate fields in the container.
             * @config {Object}
             * @internal
             */
            record : null,

            // NOTE: internal since record is not documented
            /**
             * Update assigned {@link #config-record} automatically on field changes
             * @config {Boolean}
             * @internal
             */
            autoUpdateRecord : null,

            /**
             * Specify `true` to make this container hide when it has no visible children (Either empty
             * or all children hidden).
             *
             * Container will show itself when there are visible children, ie: hidden children are
             * shown, or new visible childre are added.
             * @config {Boolean}
             * @default
             */
            hideWhenEmpty : null
        };
    }

    /**
     * An Array of this Container's children. This is read-only and immutable.
     *
     * Use the {@link #function-add} and {@link #function-remove} methods to change content.
     * @member {Core.widget.Widget[]} items
     * @readonly
     */

    /**
     * Sets multiple flexbox settings which affect how child widgets are arranged.
     *
     * By default, Containers use flexbox layout, so this property may contain the following properties:
     *
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-direction">flexDirection</a> default '`row`'
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-wrap">flexWrap</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-flow">flexFlow</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/justify-content">justifyContent</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-items">alignItems</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-content">alignContent</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/place-content">placeContent</a>
     * @member {Object} layoutStyle
     * @category Layout
     */

    /**
     * The {@link Core.data.Model record} to be applied to the fields contained in this Container. Any descendant
     * widgets of this Container with a `name` property will have its value set to the value of that named property of
     * the record. If no record is passed, the widget has its value set to `null`.
     * @member {Core.data.Model} record
     */

    startConfigure(config) {
        // Set a flag so that code can test for presence of items.
        // Widgets which render child widgets outside of the Container scheme
        // can set this flag (eg Panels with tools and tbar).
        const items = config.items || config.lazyItems;
        this.hasItems = Boolean(items && (Array.isArray(items) ? items : Object.keys(items)).length);
        super.startConfigure(config);
    }

    /**
     * Removes the passed child/children from this Container.
     * @param  {...Core.widget.Widget} toRemove The child or children to remove.
     * @returns {Core.widget.Widget|Core.widget.Widget[]} All the removed items. An array if multiple items
     * were removed, otherwise, just the item removed.
     */
    remove(...toRemove) {
        let returnArray = true;

        if (toRemove.length === 1) {
            if (Array.isArray(toRemove[0])) {
                toRemove = toRemove[0];
            }
            else {
                returnArray = false;
            }
        }

        const
            me         = this,
            { _items } = me,
            result     = [];

        for (let i = 0; i < toRemove.length; i++) {
            const childToRemove = toRemove[i];

            if (_items.includes(childToRemove)) {
                _items.remove(childToRemove);
                me.layout.removeChild(childToRemove);
                result.push(childToRemove);
                me.onChildRemove(childToRemove);
            }
        }

        return returnArray ? result : result[0];
    }

    /**
     * Removes all children from this Container.
     * @returns {Core.widget.Widget[]} All the removed items.
     */
    removeAll() {
        return this.remove(this.items);
    }

    /**
     * Appends the passed widget/widgets to this Container.
     *
     * If the widgets specify a `weight`, they are inserted at the correct index compared to the existing items weights.
     *
     * @param  {...Core.widget.Widget} toAdd The child or children to add.
     * @returns {Core.widget.Widget|Core.widget.Widget[]} All the added widgets. An array if multiple items
     * were added, otherwise, just the item added.
     */
    add(...toAdd) {
        let returnArray = true;

        if (toAdd.length === 1) {
            if (Array.isArray(toAdd[0])) {
                toAdd = toAdd[0];
            }
            else {
                returnArray = false;
            }
        }

        // Force creation of our items Bag
        if (!this.items) {
            this.items = [];
        }

        const
            me         = this,
            { _items } = me,
            result     = [];

        for (let i = 0; i < toAdd.length; i++) {
            let childToAdd = toAdd[i];

            // Items with weight are inserted at correct index
            if (childToAdd.weight != null) {
                // Cannot use cached _items, weights might be unordered in set being added
                const index = ArrayHelper.findInsertionIndex(childToAdd, me._items.values, sortByWeight);
                result.push(me.insert(childToAdd, index));
            }
            // Those without are appended
            else {
                if (!(childToAdd instanceof Widget)) {
                    childToAdd = me.createWidget(childToAdd);
                }
                else {
                    childToAdd.parent = me;
                }

                if (!_items.includes(childToAdd)) {
                    _items.add(childToAdd);
                    me.onChildAdd(childToAdd);
                    me.layout.appendChild(childToAdd);
                    result.push(childToAdd);
                }
            }
        }

        return returnArray ? result : result[0];
    }

    /**
     * Inserts the passed widget into this Container at the specified position.
     * @param  {Core.widget.Widget} toAdd The child to insert.
     * @param {Number|Core.widget.Widget} The index to insert at or the existing child to insert before.
     * @returns {Core.widget.Widget} The added widget.
     */
    insert(toAdd, index) {
        // Force creation of our items Bag
        if (!this.items) {
            this.items = [];
        }

        const
            me         = this,
            { _items } = me;

        if (toAdd instanceof Widget) {
            toAdd.parent = me;
        }
        else {
            toAdd = me.createWidget(toAdd);
        }

        if (_items.includes(index)) {
            index = me.indexOfChild(index);
        }

        index = Math.min(index, _items.count);

        const newValues = _items.values;
        newValues.splice(index, 0, toAdd);
        _items.values = newValues;

        // Register inserted item
        me.onChildAdd(toAdd);

        me.layout.insertChild(toAdd, index);

        return toAdd;
    }

    indexOfChild(child) {
        return this.items.indexOf(child);
    }

    changeLazyItems(lazyItems) {
        this.items = lazyItems;
        this.layout.renderChildren();
    }

    changeItems(items) {
        const
            me       = this,
            newItems = [],
            result   = new Bag();

        if (Array.isArray(items)) {
            me.processItemsArray(items, newItems);
        }
        else if (items) {
            me.processItemsObject(items, me.namedItems, newItems);
        }

        // Allow child items to have a weight to establish their order
        if (newItems.some(returnWeight)) {
            newItems.sort(sortByWeight);
        }

        result.add(newItems);

        return result;
    }

    updateItems(items) {
        items.forEach(this.onChildAdd, this);
    }

    updateHideWhenEmpty() {
        this.syncChildCount();
    }

    get items() {
        // If we are being asked for items, ingest lazyItems.
        this.getConfig('lazyItems');

        // The documented API for items is an Array.
        // Internal code should access _items
        if (!this._items) {
            if (this.initializingItems) {
                // This is a created array. User may mutate it.
                return [];
            }
            this.items = emptyArray;
        }
        // This is the Collection's array. User may not mutate it.
        return this._items.values;
    }

    processItemsArray(items, result) {
        const len = items.length;

        let i, item;

        for (i = 0; i < len; i++) {
            item = items[i];

            if (item instanceof Widget) {
                item.parent = this;
                DomHelper.removeClasses(item.element, Widget.outerCls);
            }
            else {
                item = this.createWidget(item);
            }

            // If the widget creation function returns null, nothing to add
            if (item) {
                result.push(item);
            }
        }
    }

    processItemsObject(items, namedItems = emptyObject, result) {
        let item, ref;

        for (ref in items) {
            item = items[ref];

            // It might come in as itemRef : false
            if (item) {
                // If this class or instance has a "namedItems" object
                // named by this ref, then use it as the basis for the item
                if (ref in namedItems) {
                    item = typeof item === 'object' ? ObjectHelper.merge(ObjectHelper.clone(namedItems[ref]), item) : namedItems[ref];
                }

                // Allow namedItems to be overridden with itemKey : false to indicate unavailability of an item
                if (item) {
                    if (item instanceof Widget) {
                        item.parent = this;
                    }
                    else {
                        //<debug>
                        if (item.ref && item.ref !== ref) {
                            throw new Error(`Named child item ref property ${item.ref} doesn't match the property name it was from ${ref}`);
                        }
                        //</debug>
                        if (item instanceof Object) {
                            item.ref = ref;
                        }
                        item = this.createWidget(item);
                    }

                    // If the widget creation function returns null, nothing to add
                    if (item) {
                        item.ref = ref;
                        result.push(item);
                    }
                }
            }
        }
    }

    onChildAdd(item) {
        const ref = item.ref || item.id;

        // Don't just assign the property across since the default value is undefined
        // which means false. Only set to true if we are readOnly
        if (this.readOnly) {
            item.readOnly = true;
        }

        // Keep layout informed of child item state
        this.layout.onChildAdd(item);

        if (ref) {
            for (let current = this; current; current = current.parent) {
                // Silently add the descendant to the ancestor's widgetMap without kicking off
                // the ancestor's items processing by directly accessing the widgetMap property.
                current.addDescendant(item, ref);
            }
        }
        this.syncChildCount();
    }

    onChildHide() {
        this.syncChildCount();
    }

    onChildShow() {
        this.syncChildCount();
    }

    syncChildCount() {
        // If called during configuration, the element may not be available because
        // some non-item widgets (eg, Tools) instantiate and declare themselves through
        // onChildAdd before the element is set.
        // But also there may be no items because of     lazyItems.
        // So do the initial sync at render time.
        if (!this.isConfiguring) {
            const
                me                         = this,
                { childItems }             = me,
                { length }                 = childItems,
                { length : visibleLength } = childItems.filter(isNotHidden);

            if (me.hideWhenEmpty) {
                const shouldHide = Boolean(!visibleLength);

                if (Boolean(me._hidden) !== shouldHide) {
                    me.hidden = shouldHide;
                }
            }

            // Keep hasItems property up to date. It's used by the isFocusable getter.
            me.hasItems = Boolean(length);

            me.contentElement.classList[visibleLength ? 'remove' : 'add']('b-no-visible-children');

            // Reevaluate whether we should have the b-text-content class
            me.updateTextContent(me._textContent);
        }
    }

    addDescendant(item, ref) {
        const widgetMap = this._widgetMap || (this._widgetMap = {});

        if (!widgetMap[ref]) {
            widgetMap[ref] = item;
        }
    }

    onChildRemove(item) {
        const ref = item.ref || item.id;

        if (ref) {
            for (let current = this; current; current = current.parent) {
                if (current.widgetMap[ref] === item) {
                    delete current.widgetMap[ref];
                }
            }
        }

        // Keep layout informed of child item state
        this.layout.onChildRemove(item);

        this.syncChildCount();
    }

    /**
     * An object which contains a map of descendant widgets keyed by their {@link Core.widget.Widget#config-ref ref}.
     * All descendant widgets will be available in the `widgetMap`.
     * @property {Object}
     * @readonly
     * @typings any
     */
    get widgetMap() {
        if (!this._widgetMap) {
            this._widgetMap = {};
        }

        // Force evaluation of the configured items by the getter
        // so that configs are promoted to widgets and the widgetMap
        // is created, and if there are widgets, populated.
        if (!this.initializingItems) {
            this.getConfig('items');
        }

        return this._widgetMap;
    }

    changeRecord(record) {
        // The config system's non-change vetoing must be bypassed.
        // The record might have changed, or the destination fields may be out of sync.
        this._record = record == null ? emptyObject : null;

        return record;
    }

    updateRecord(record) {
        /**
         * Fired before this container will load record values into its child fields. This is useful if you
         * want to modify the UI before data is loaded (e.g. set some input field to be readonly)
         * @event beforeSetRecord
         * @param {Core.widget.Container} source The container
         * @param {Core.data.Model} record The record
         */
        this.trigger('beforeSetRecord', { record });
        this.setValues(record, true, !this.$highlight);
    }

    setRecord(record, highlightChanges) {
        this.$highlight = highlightChanges;
        this.record = record;
        this.$highlight = false;
    }

    /**
     * A function called by descendant widgets after they trigger their 'change' event, in reaction to field changes.
     * By default implements the functionality for the `autoUpdateRecord` config.
     *
     * @param {Object} params Normally the event params used when triggering the 'change' event
     * @internal
     */
    onFieldChange({ source, userAction }) {
        // When configured with `autoUpdateRecord`, changes from descendant fields/widgets are applied to the loaded
        // record using the fields `name`. Only changes from valid fields will be applied
        if (this.autoUpdateRecord) {
            const
                { record }               = this,
                { name, isValid, value } = source;

            if (record && userAction && name && isValid) {
                record[name] = value;
            }
        }
    }

    getValues(filterFn) {
        const
            me      = this,
            widgets = me.queryAll(w => w.name),
            len     = widgets.length,
            result = {};

        for (let i = 0; i < len; i++) {
            const
                widget = widgets[i],
                name   = widget.name;

            if (!filterFn || filterFn(widget)) {
                result[name] = widget.value;
            }
        }

        return result;
    }

    get hasNoChildren() {
        // If we have *uningested* lazyItems, use them to find our items length.
        // In that case we must not cause ingestion of the lazyItems by referencing this.items.
        // If we have items, then only those that are visible have any bearing.
        // Popup also has to consult this property to decide on its CSS classes.
        const
            { lazyItems }  = this.initialConfig,
            lazyItemsArray = lazyItems && (Array.isArray(lazyItems) ? lazyItems : Object.values(lazyItems)),
            { length }     = (lazyItemsArray && !this._items ? lazyItemsArray : this.items).filter(isNotHidden);

        return !length;
    }

    updateTextContent(textContent) {
        // Add the text content class if we have no visible immediate item children.
        this.contentElement.classList[textContent && this.hasNoChildren ? 'add' : 'remove']('b-text-content');

        // Depending on CSS settings around how this widget handles text content and widthing
        // we *may* have to realign. Realign only goes ahead if we're visible and floating.
        if (this.rendered) {
            this.realign();
        }
    }

    updateLayoutStyle(layoutStyle) {
        DomHelper.applyStyle(this.contentElement, layoutStyle);
    }

    changeLayout(layout) {
        // TODO: Layouts to be Factoryable
        return Layout.getLayout(layout, this);
    }

    // Items to iterate over
    get childItems() {
        return this.items;
    }

    /**
     * Returns a directly contained widget by id
     * @param {String} id The widget id
     * @returns {Core.widget.Widget}
     */
    getWidgetById(id) {
        return this.widgetMap[id];
    }

    /**
     * This function is called prior to creating widgets, override it in subclasses to allow containers to modify the
     * configuration of each widget. When adding a widget to a container hierarchy each parent containers
     * `processWidgetConfig` will be called. Returning false from the function prevents the widget from being added at
     * all.
     */
    processWidgetConfig(widget) {

    }

    /**
     * This method combines container {@link #config-defaults}
     * @param {Object} widgetConfig
     * @param {String|Function} [type] The type of widget described by `widgetConfig`.
     * @returns {Object}
     * @internal
     */
    setupWidgetConfig(widgetConfig, type) {
        const me = this;

        // A string becomes the defaultType (see below) with the html set to the string.
        if (typeof widgetConfig === 'string') {
            widgetConfig = {
                html : widgetConfig
            };
        }
        // An element is encapsulated by a Widget
        else if (widgetConfig.nodeType === 1) {
            widgetConfig = {
                element : widgetConfig,
                id      : widgetConfig.id
            };
        }

        if (typeof type === 'string' || !type && (type /* assignment */= widgetConfig.type)) {
            type = Widget.resolveType(type, true);
        }

        // widgetConfig = ObjectHelper.assign({}, me.defaults, widgetConfig, { parent : me });
        widgetConfig = (type || Widget).mergeConfigs(me.defaults, widgetConfig, { parent : me });

        for (let ancestor = widgetConfig.parent; ancestor; ancestor = ancestor.parent) {
            if (ancestor.processWidgetConfig(widgetConfig) === false) {
                return null;
            }
        }

        if (me.trigger('beforeWidgetCreate', { widgetConfig }) === false) {
            return null;
        }

        return widgetConfig;
    }

    /**
     * This function converts a Widget config object into a Widget.
     * @param {Object} widget A Widget config object.
     * @internal
     */
    createWidget(widget) {
        return Widget.create(this.setupWidgetConfig(widget), this.defaultType);
    }

    // Reapply defaults, not used during config
    updateDefaults(defaults, oldDefaults) {
        if (!this.isConfiguring) {
            const entries = Object.entries(defaults);

            this.eachWidget(widget => {
                entries.forEach(([prop, value]) => {
                    // Apply defaults only if current value matches the old default
                    if (widget[prop] === oldDefaults[prop]) {
                        widget[prop] = value;
                    }
                });
            }, false);
        }
    }

    render() {
        // Pull in lazyItems at last second
        this.getConfig('lazyItems');

        this.layout.renderChildren();

        // If called during configuration, the element may not have been available because
        // some non-item widgets (eg, Tools) instantiate and declare themselves through
        // onChildAdd before the element is set.
        // But also there may have been no items because of lazyItems.
        // So do the initial sync at render time.
        this.syncChildCount();

        super.render(...arguments);
    }

    get focusElement() {
        // Find first focusable descendant widget that is not our close tool.
        const firstFocusable = this.query(this.defaultFocus || (w => w.isFocusable && w.ref !== 'close'));

        if (firstFocusable) {
            return firstFocusable.focusElement;
        }
        return super.focusElement;
    }

    doDestroy() {
        const { _items } = this;

        _items && _items.forEach(widget => widget.destroy && widget.destroy());

        super.doDestroy();
    }

    /**
     * Checks that all descendant fields are valid.
     * @returns {Boolean} Returns `true` if all contained fields are valid, otherwise `false`
     */
    get isValid() {
        let valid = true;

        this.eachWidget(widget => {
            // Touch each widget so that they all update their invalid state. This is important for required fields
            // since they don't initially complain if they start as empty.
            if ('isValid' in widget && !widget.isValid) {
                valid = false;
            }
        }, true);

        return valid;
    }

    /**
     * Retrieves or sets all values from/to contained fields.
     * Accepts and returns a map, using {@link Core.widget.Field#config-name name},
     * {@link Core.widget.Widget#config-ref ref} or {@link Core.widget.Widget#config-id id} (in that order) as keys.
     *
     * ```javascript
     * container.values = {
     *     firstName : 'Clark',
     *     surname : 'Kent'
     * };
     * ```
     *
     * @property {Object}
     */
    get values() {
        const values = {};

        this.eachWidget(widget => {
            if ('value' in widget) {
                values[widget.name || widget.ref || widget.id] = widget.value;
            }
        }, true);

        return values;
    }

    set values(values) {
        this.setValues(values);
    }

    /**
     * Returns `true` if currently setting values. Allows fields change highlighting to distinguishing between initially
     * setting values and later on changing values.
     * @property {Boolean}
     */
    get isSettingValues() {
        // Fields query their parent, pass the question up in case containers are nested
        return Boolean(this._isSettingValues || this.parent?.isSettingValues);
    }

    setValues(values, onlyName = false, preventHighlight = true) {
        // Flag checked by Field to determine if it should highlight change or not
        this._isSettingValues = { preventHighlight };

        this.eachWidget(widget => {
            const
                hec = widget.highlightExternalChange,
                key = onlyName ? widget.name : (widget.name || widget.ref || widget.id);

            if ('value' in widget && key) {
                if (preventHighlight) {
                    // Don't want a field highlight on mass change
                    widget.highlightExternalChange = false;
                }

                // Setting to null when value not matched clears field
                widget.value = (values && (key in values)) ? values[key] : null;

                widget.highlightExternalChange = hec;
            }

        }, true);

        this._isSettingValues = false;
    }
}

// Register this widget type with its Factory
Container.initClass();
