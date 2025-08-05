import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import Widget from './Widget.js';
import WidgetHelper from '../helper/WidgetHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import Layout from './layout/Layout.js';
import DomHelper from '../helper/DomHelper.js';
import './Ripple.js';

/**
 * @module Common/widget/Container
 */

const emptyObject = Object.freeze({});

/**
 * Widget that can contain other widgets. Layout is flexbox by default, see the {@link #config-layout} config.
 *
 * @extends Common/widget/Widget
 *
 * @example
 * // create a container with two widgets
 * let container = new Container({
 *   widgets : [
 *     { type : 'text', label : 'Name' },
 *     { type : 'number', label : 'Score' }
 *   ]
 * });
 *
 * @classType container
 * @externalexample widget/Container.js
 */
export default class Container extends Widget {
    static get defaultConfig() {
        return {
            /**
             * An array of Widgets or typed Widget config objects.
             *
             * If configured as an Object, the property names are used as the child component's
             * {@link Common.widget.Widget#config-ref ref} name, and the value is the child component's config object.
             *
             * '''javascript
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
             *  }).show();```
             *
             * @config {Object[]|Common.widget.Widget[]|Object}
             */
            items : null,

            /**
             * Synonym for the {@link #config-items} config option.
             * @config {Object[]|Common.widget.Widget[]|Object}
             * @deprecated 2.1
             */
            widgets : null,

            /**
             * A config object containing default settings to apply to all child widgets.
             * @config {Object}
             */
            defaults : null,

            defaultType : 'widget',

            /**
             * The CSS style properties to apply to the {@link Common.widget.Widget#property-contentElement}.
             *
             * By default, a Container's {@link Common.widget.Widget#property-contentElement} uses flexbox layout, so this config
             * may contain the following properties:
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
             * Containers use CSS flexbox in its default configuration to arrange child items. You may either
             * use the {@link #config-layoutStyle} configuration to tune how child items are layed out,
             * or use one of the built in helper classes which include:
             *
             *  - `card` Child items are displayed one at a time, size to fit the {@link Common.widget.Widget#property-contentElement}
             * and are slid in from the side when activated.
             * @config {String}
             */
            layout : 'default',

            /**
             * An object containing named config objects which may be referenced by name in any {@link #config-items}
             * object. For example, a specialized {@link Common.widget.Menu Menu} subclass may have a `namedItems`
             * default value defined like this:
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
             * Then whenever that subclass is instantiated and configured with an {@link #config-items}
             * object, the items may be configured like this:
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
            namedItems : null
        };
    }

    // TODO: Remove when `widgets` is removed.
    setConfig(config, isConstructing) {
        // Assign deprecated widgets to items as early as possible to not have to have special handling in getters/setters
        if (config.widgets) {
            config.items = config.widgets;
        }

        super.setConfig(config, isConstructing);
    }

    startConfigure(config) {
        // Set a flag so that code can test for presence of items without tickling
        // any initial getter.
        const { items } = this;
        this.hasItems = Boolean(items && items.length);
        super.startConfigure(config);
    }

    set widgets(widgets) {
        console.warn('`widgets` was deprecated in 2.1, please change your code to use `items`');
        // Does nothing on purpose
    }

    get widgets() {
        console.warn('`widgets` was deprecated in 2.1, please change your code to use `items`');
        return this.items;
    }

    set items(items) {
        //<debug>
        if (!this.isConfiguring) {
            throw new Error('Child items may not be configured dynamically');
        }
        //</debug>

        this._items = items;
        this._itemsInvalid = this.itemsInitialized = true;
    }

    /**
     * The array of instantiated child Widgets.
     * @property {Common.widget.Widget[]}
     * @readonly
     */
    get items() {
        const
            me = this,
            items = me._items;

        // _widgetMap must exist even if there are no items because of other widget containment
        // situations such as docked toolbars.
        if (!me._widgetMap) {
            me._widgetMap = {};
        }

        // Only convert the widget config objects into widgets
        // when we first access the widgets. This is more efficient
        // if this Container is never rendered.
        if (me._itemsInvalid) {
            const instancedItems = (me._items = []);

            if (Array.isArray(items)) {
                me.processItemsArray(items, instancedItems);
            }
            else if (items) {
                me.processItemsObject(items, me.namedItems, instancedItems);
            }

            // Allow child items to have a weight to establish their order
            if (instancedItems.some(i => i.weight)) {
                instancedItems.sort((a, b) => ((a.weight || 0) - (b.weight || 0)));
            }

            me._itemsInvalid = false;
        }

        return me._items;
    }

    processItemsArray(items, result) {
        const len = items.length;

        let i, item;

        for (i = 0; i < len; i++) {
            item = items[i];

            if (!(item instanceof Widget)) {
                item = this.createWidget(item);
            }
            else {
                item.parent = this;
            }

            // If the widget creation function returns null, nothing to add
            if (item) {
                result.push(item);

                // Add current item to this and every parent widget map
                // cannot use prototype chain here for two reasons:
                // 1. performance
                // 2. prototypes would require new prototype chain for every branch - not optimal
                this.registerReference(item, item.ref || item.id);
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
                    if (!(item instanceof Widget)) {
                        item = this.createWidget(item);
                    }
                    else {
                        item.parent = this;
                    }

                    // If the widget creation function returns null, nothing to add
                    if (item) {
                        result.push(item);
                        //<debug>
                        if (item.ref) {
                            throw new Error('Named child items must not contain ref config. Its property name is its ref');
                        }
                        //</debug>

                        // Add current item to this and every parent widget map
                        // cannot use prototype chain here for two reasons:
                        // 1. performance
                        // 2. prototypes would require new prototype chain for every branch - not optimal
                        this.registerReference(item, ref);
                    }
                }
            }
        }
    }

    registerReference(item, ref) {
        for (let current = this; current; current = current.parent) {
            if (!current.widgetMap[ref]) {
                current.widgetMap[ref] = item;
            }
        }
    }

    /**
     * An object which contains a map of descendant widgets keyed by their {@link Common.widget.Widget#config-ref ref}.
     * All descendant widgets will be available in the `widgetMap`.
     * @property {Object}
     * @readonly
     * @typings any
     */
    get widgetMap() {
        // Force evaluation of the widgets array by the getter
        // so that configs are promoted to widgets and the widgetMap
        // is created, and if there are widgets, populated.
        if (!this._widgetMap) {
            this._thisIsAUsedExpression(this.items);
        }

        return this._widgetMap;
    }

    set record(record) {
        const
            me = this,
            widgets = me.queryAll(w => w.name),
            len = widgets.length,
            cl = me.element.classList;

        // Though we set highlightExternalChange on the widgets we change
        // the value of, onChange listeners may update others, so inhibit
        // field update highlighting at the CSS level.
        cl.add('b-form-updating');

        for (let i = 0; i < len; i++) {
            const
                widget = widgets[i],
                name = widget.name,
                hec = widget.highlightExternalChange;

            // Don't want a field highlight on mass change
            widget.highlightExternalChange = false;

            // Setting record to null clears values
            if (!record && name) {
                widget.value = null;
            }
            else if (record && name in record) {
                widget.value = record[name];
            }

            widget.highlightExternalChange = hec;
        }
        me.setTimeout(() => cl.remove('b-form-updating'), 1500);

        me._record = record;
    }

    /**
     * The {@link Common.data.Model record} to be applied to the fields contained in this Container.
     * Any descendant widgets of this Container with a `name` property will have its value set to the
     * value of that named property of the record. If no record is passed, the widget has its value
     * set to `null`.
     * @property {Common.data.Model}
     */
    get record() {
        return this._record;
    }

    /**
     * Sets multiple flexbox settings which affect how child widgets are arranged.
     *
     * By default, Containers use flexbox layout, so this property
     * may contain the following properties:
     *
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-direction">flexDirection</a> default '`row`'
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-wrap">flexWrap</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/flex-flow">flexFlow</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/justify-content">justifyContent</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-items">alignItems</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/align-content">alignContent</a>
     * - <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/place-content">placeContent</a>
     * @property {Object}
     * @category Layout
     */
    set layoutStyle(layoutStyle) {
        DomHelper.applyStyle(this.contentElement, layoutStyle);
        this._layoutStyle = layoutStyle;
    }

    get layoutStyle() {
        return this._layoutStyle;
    }

    set layout(layout) {
        this._layout = Layout.getLayout(layout, this);
    }

    get layout() {
        return this._layout || (this._layout = new Layout());
    }

    /**
     * Iterate over all widgets in this container and below.
     *
     * *Note*: Due to this method aborting when the function returns
     * `false`, beware of using short form arrow functions. If the expression
     * executed evaluates to `false`, iteration will terminate.
     * @param {Function} fn A function to execute upon all descendant widgets.
     * Iteration terminates if this function returns `false`.
     * @param {Boolean} [deep=true] Pass as `false` to only consider immediate child widgets.
     */
    eachWidget(fn, deep = true) {
        const
            widgets = this.items,
            length = widgets ? widgets.length : 0;

        for (let i = 0; i < length; i++) {
            const widget = widgets[i];

            if (fn(widget) === false) {
                return;
            }
            if (deep && widget.eachWidget) {
                widget.eachWidget(fn, deep);
            }
        }
    }

    /**
     * Returns an array of all descendant widgets which the passed
     * filter function returns `true` for.
     * @param {Function} filter A function which, when passed a widget,
     * returns `true` to include the widget in the results.
     * @returns {Common.widget.Widget[]} All matching descendant widgets.
     */
    queryAll(filter) {
        const result = [];

        this.eachWidget(w => {
            if (filter(w)) {
                result.push(w);
            }
        });

        return result;
    }

    /**
     * Returns the first descendant widgets which the passed
     * filter function returns `true` for.
     * @param {Function} filter A function which, when passed a widget,
     * returns `true` to return the widget as the sole result.
     * @returns {Common.widget.Widget} The first matching descendant widget.
     */
    query(filter) {
        let result = null;

        this.eachWidget(w => {
            if (filter(w)) {
                result = w;
                return false;
            }
        });

        return result;
    }

    /**
     * Returns a directly contained widget by id
     * @param {String} id The widget id
     * @returns {Common.widget.Widget}
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
     * This function converts a Widget config object into a Widget.
     * @param {Object} widget A Widget config object.
     * @internal
     */
    createWidget(widget) {
        const me = this;

        if (!widget.type) {
            widget.type = me.defaultType;
        }

        // A contained Widget must know its parent, and knowing it during construction
        // is important, but me must not mutate incoming config objects.
        widget = Object.setPrototypeOf({
            parent : me
        }, widget);

        let ancestor = widget;
        while ((ancestor = ancestor.parent)) {
            if (ancestor.processWidgetConfig(widget) === false) {
                return null;
            }
        }

        if (me.trigger('beforeWidgetCreate', { widget }) === false) {
            return null;
        }

        return WidgetHelper.createWidget(ObjectHelper.assign({}, me.defaults, widget), me.defaultType || 'widget');
    }

    render(appendToElement) {
        // Outer container has to be in place first.
        // Pass triggerPaint as false, as when contained, the outermost
        // container calls that at the end of its render
        const result = super.render(appendToElement, false);

        this.layout.renderChildren();

        // The outermost container must trigger paint after all children have
        // been rendered, and this cascades down through all descendants.
        if (!this.parent) {
            this.triggerPaint();
        }

        return result;
    }

    get focusElement() {
        const firstFocusable = this.query(this.defaultFocus || (w => w.focusElement));

        if (firstFocusable) {
            return firstFocusable.focusElement;
        }
        return super.focusElement;
    }

    doDestroy() {
        // Only destroy the widgets if they have been instanced.
        if (!this._itemsInvalid && this.items) {
            this.items.forEach(widget => widget.destroy && widget.destroy());
        }

        super.doDestroy();
    }
}

BryntumWidgetAdapterRegister.register('container', Container);
