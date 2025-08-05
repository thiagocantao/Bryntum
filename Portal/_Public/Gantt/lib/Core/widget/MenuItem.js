import DomClassList from '../helper/util/DomClassList.js';
import Menu from './Menu.js';
import Widget from './Widget.js';
import DomHelper from '../helper/DomHelper.js';

/**
 * @module Core/widget/MenuItem
 */

const
    bIcon = /^b-icon-/,
    bFa   = /^b-fa-/;

/**
 * A widget representing a single menu item in a {@link Core.widget.Menu}. May be configured with a
 * {@link #config-checked} state which creates a checkbox which may be toggled. Can also be
 * {@link Core.widget.Widget#config-disabled}, which affects item appearance and blocks interactions.
 *
 * Fires events when activated which bubble up through the parent hierarchy and may be listened for on an ancestor. See
 * {@link Core.widget.Menu Menu} for more details on usage.
 *
 * To add a border above a menu item, you can set {@link #config-separator} to `true`. The separator is automatically
 * hidden if the menu item is the first visible item in the menu.
 *
 * @extends Core/widget/Widget
 * @classType menuitem
 */
export default class MenuItem extends Widget {
    //region Config
    static get $name() {
        return 'MenuItem';
    }

    // Factoryable type name
    static get type() {
        return 'menuitem';
    }

    static get configurable() {
        return {
            /**
             * If configured with a `Boolean` value, a checkbox is displayed
             * as the start icon, and the {@link #event-toggle} event is fired
             * when the checked state changes.
             * @config {Boolean}
             */
            checked : null,

            /**
             * Set to `true` to display a border above this menu item, if there are other visible menu items before it.
             * @config {Boolean}
             * @default false
             */
            separator : null,

            /**
             * Indicates that this menu item is part of a group where only one can be checked. Assigning a value
             * also sets `toggleable` to `true`.
             * ```
             * const yesButton = new Button({
             *    toggleGroup : 'yesno',
             *    text        : 'Yes'
             * });
             *
             * const noButton = new Button({
             *    toggleGroup : 'yesno',
             *    text        : 'No'
             * });
             * ```
             * @config {String}
             */
            toggleGroup : null,

            /**
             * Returns the instantiated menu widget as configured by {@link #config-menu}.
             * @member {Core.widget.Widget} menu
             * @readonly
             */
            /**
             * A submenu configuration object, or an array of MenuItem configuration
             * objects from which to create a submenu.
             *
             * Configuration object example:
             * ```javascript
             * new Menu({
             *     // Menu items
             *     items : {
             *         move : {
             *             text : 'Main item',
             *             menu : {
             *                 // Submenu items
             *                 firstItem : {
             *                     text : 'Sub-item 1',
             *                     onItem({ eventRecord }) {}
             *                 },
             *                 secondItem : {
             *                     text : 'Sub-item 2',
             *                     onItem({ eventRecord }) {}
             *                 }
             *             }
             *         }
             *     }
             * });
             * ```
             *
             * Array of items example:
             * ```javascript
             * new Menu({
             *     // Menu items
             *     items : {
             *         move : {
             *             text : 'Main item',
             *             // Submenu items
             *             menu : [
             *                 {
             *                     text : 'Sub-item 1',
             *                     onItem({ eventRecord }) {}
             *                 },
             *                 {
             *                     text : 'Sub-item 2',
             *                     onItem({ eventRecord }) {}
             *                 }
             *             ]
             *         }
             *     }
             * });
             * ```
             *
             * Note that this does not have to be a Menu. The `type` config can be used to specify any widget as the submenu.
             * ```javascript
             * new Menu({
             *     // Menu items
             *     items : {
             *         move : {
             *             text : 'Main item',
             *             // Submenu items
             *             menu : [
             *                 {
             *                     type  : 'textfield',
             *                     label : 'Type here'
             *                 },
             *                 {
             *                     type : 'button',
             *                     text : 'Confirm'
             *                 }
             *             ]
             *         }
             *     }
             * });
             * ```
             *
             * @config {Object<String,MenuItemConfig|ContainerItemConfig>|Array<MenuItemConfig|ContainerItemConfig>}
             */
            menu : {
                value : null,

                $config : ['lazy', 'nullify']
            },

            /**
             * Item icon class.
             *
             * All [Font Awesome](https://fontawesome.com/cheatsheet) icons may also be specified as `'b-fa-' + iconName`.
             *
             * Otherwise this is a developer-defined CSS class string which results in the desired icon.
             * @config {String}
             */
            icon : null,

            /**
             * The text to be displayed in the item
             * @config {String} text
             */

            /**
             * By default, upon activate, non-checkbox menu items will collapse
             * the owning menu hierarchy.
             *
             * Configure this as `false` to cause the menu to persist after
             * activating an item
             * @config {Boolean}
             */
            closeParent : null,

            /**
             * If provided, turns the menu item into a link
             * @config {String}
             */
            href : null,

            /**
             * The `target` attribute for the {@link #config-href} config
             * @config {'_self'|'_blank'|'_parent'|'_top'|null}
             */
            target : null,

            localizableProperties : ['text'],

            role : 'menuitem',

            closeMenuDelay : 200
        };
    }

    updateElement(element, oldElement) {
        const result = super.updateElement(element, oldElement);

        if (typeof this.checked === 'boolean') {
            this.role = 'menuitemcheckbox';
        }
        this.ariaHasPopup = this.hasMenu ? 'menu' : false;
        return result;
    }

    compose() {
        const
            me = this,
            { checked, href, hasMenu, separator, target, text, toggleGroup } = me,
            isCheckItem = typeof checked === 'boolean',
            icon = me.icon || (isCheckItem ? 'b-fw-icon' : ''),
            checkCls = `b-icon-${toggleGroup ? 'radio-' : ''}`,
            hasCustomContent = typeof text === 'object';

        return {
            tag      : href ? 'a' : 'div',
            tabIndex : -1,

            href,
            target,

            class : {
                'b-has-submenu' : hasMenu,
                'b-checked'     : checked,
                // Support both separator config and directly setting separator class
                'b-separator'   : separator || me.cls?.['b-separator']
            },

            dataset : {
                group : me.toggleGroup
            },

            // Only set aria-checked if it's a check item
            [isCheckItem ? 'aria-checked' : ''] : checked,

            // Only set expanded if there's a submenu to expand
            [hasMenu ? 'aria-expanded' : ''] : false,

            children : {
                iconElement : icon && {
                    // This element is a purely visual cue with no meaning to the A11Y tree
                    'aria-hidden' : true,

                    tag   : 'i',
                    class : {
                        'b-fa'            : bFa.test(icon),
                        'b-icon'          : bIcon.test(icon),
                        'b-menuitem-icon' : 1,

                        [`${checkCls}checked`]   : checked === true,
                        [`${checkCls}unchecked`] : checked === false,

                        ...DomClassList.normalize(icon, 'object')
                    }
                },

                textElement : {
                    tag   : 'span',
                    html  : hasCustomContent ? null : text,
                    class : {
                        'b-menu-text'           : 1,
                        'b-menu-custom-content' : hasCustomContent
                    },
                    children : hasCustomContent ? [text] : null
                },

                subMenuIcon : hasMenu && {
                    // This element is a purely visual cue with no meaning to the A11Y tree
                    'aria-hidden' : true,

                    tag   : 'i',
                    class : {
                        'b-fw-icon'       : 1,
                        'b-icon-sub-menu' : 1
                    }
                }
            }
        };
    }

    /**
     * Actions this item. Fires the {@link #event-item} event, and if this is a {@link #config-checked} item, toggles
     * the checked state, firing the {@link #event-toggle} event.
     */
    doAction(event) {
        const
            item      = this,
            menu      = this.parent,
            itemEvent = { menu, item, element : item.element, bubbles : true, domEvent : event };

        if (typeof item.checked === 'boolean') {
            const newCheckedState = !item.checked;

            // Do not allow uncheck in a toggleGroup.
            // A toggleGroup means that one member must always be checked.
            if (!item.toggleGroup || newCheckedState) {
                item.checked = !item.checked;
            }
        }

        // Give internal handlers a chance to inject extra information before
        // user-supplied "item" handlers see the event.
        // Grid's CellMenu feature, HeaderMenu feature and other context menu features do this.
        item.trigger('beforeItem', itemEvent);

        /**
         * This menu item has been activated.
         *
         * Note that this event bubbles up through parents and can be
         * listened for on a top level {@link Core.widget.Menu Menu} for convenience.
         * @event item
         * @param {Core.widget.MenuItem} item - The menu item which is being actioned.
         * @param {Core.widget.Menu} menu - Menu containing the menu item
         * @param {Event} domEvent The user interaction event
         */
        item.trigger('item', itemEvent);

        // Collapse the owning menu hierarchy if configured to do so
        if (item.closeParent && menu) {
            menu.rootMenu.close();

            // Don't prevent links doing their thing
            if (event && !item.href) {
                event.preventDefault();
            }
        }
    }

    get focusElement() {
        return this.element;
    }

    get contentElement() {
        return this.textElement;
    }

    get isFocusable() {
        const { focusElement } = this;

        // We are only focusable if the focusEl is deeply visible, that means
        // it must have layout - an offsetParent. Body does not have offsetParent.
        // Disabled menu items are focusable but cannot be activated.
        // https://www.w3.org/TR/wai-aria-practices/#h-note-17
        return focusElement && this.isVisible && (focusElement === document.body || focusElement.offsetParent);
    }

    get hasMenu() {
        return this.hasConfig('menu');
    }

    get childItems() {
        // Do not call Menu into existence
        const { _menu } = this;

        return _menu ? [_menu] : [];
    }

    get text() {
        return this.html;
    }

    set text(text) {
        this.html = text;
    }

    onFocusIn(e) {
        super.onFocusIn(e);

        if (!this.disabled && this.menu) {
            // Small delay so that when mousing down a Menu, every item moved over
            // doesn't instantiate its lazy-create menu and show it.
            this.delay('openMenu', 200);
        }
    }

    onFocusOut(e) {
        this.clearTimeout('openMenu');
        super.onFocusOut(e);

        // If this item has as menu, wait a bit before hiding it to allow cursor to move over it
        // https://github.com/bryntum/support/issues/4080
        if (this._menu) {
            this.menu.closeTimer = this.menu.setTimeout(() => this.closeMenu(), this.closeMenuDelay);
        }
    }

    openMenu(andFocus) {
        const { menu } = this;

        if (!this.disabled && menu) {
            menu.focusOnToFront = andFocus;
            menu.show();
        }
    }

    onChildShow(shown) {
        super.onChildShow(shown);
        this.ariaElement.setAttribute('aria-expanded', true);
    }

    closeMenu() {
        if (this._menu instanceof Widget) {
            this.menu.close();
        }
    }

    onChildHide(hidden) {
        super.onChildHide(hidden);
        this.ariaElement.setAttribute('aria-expanded', false);
    }

    changeToggleGroup(toggleGroup) {
        if (toggleGroup && typeof this.checked !== 'boolean') {
            this.checked = false;
        }
        return toggleGroup;
    }

    /**
     * Get/sets the checked state of this `MenuItem` and fires the {@link #event-toggle}
     * event upon change.
     *
     * Note that this must be configured as a `Boolean` to enable the checkbox UI.
     * @member {Boolean} checked
     */

    changeChecked(checked, old) {
        if (this.isConfiguring || typeof old === 'boolean') {
            return Boolean(checked);
        }
    }

    updateChecked(checked) {
        const me = this;

        if (!me.isConfiguring) {
            if (me.toggleGroup) {
                me.uncheckToggleGroupMembers();
            }

            me.element.setAttribute('aria-checked', checked);

            /**
             * The checked state of this menu item has changed.
             *
             * Note that this event bubbles up through parents and can be listened for on a top level
             * {@link Core.widget.Menu Menu} for convenience.
             * @event toggle
             * @param {Core.widget.MenuItem} item - The menu item whose checked state changed.
             * @param {Core.widget.Menu} menu - Menu containing the menu item
             * @param {Boolean} checked - The _new_ checked state.
             */
            me.trigger('toggle', {
                menu    : me.owner,
                item    : me,
                element : me.element,
                bubbles : true,
                checked
            });
        }
    }

    getToggleGroupMembers() {
        const
            me = this,
            { checked, toggleGroup, element } = me,
            result = [];

        if (checked && toggleGroup) {
            DomHelper.forEachSelector(me.rootElement, `[data-group=${toggleGroup}]`, otherElement => {
                if (otherElement !== element) {
                    const partnerCheckItem = Widget.fromElement(otherElement);
                    partnerCheckItem && result.push(partnerCheckItem);
                }
            });
        }

        return result;
    }

    uncheckToggleGroupMembers() {
        if (this.checked && this.toggleGroup) {
            this.getToggleGroupMembers().forEach(widget => widget.checked = false);
        }
    }

    get closeParent() {
        const result = (typeof this.checked === 'boolean') ? this._closeParent : (this._closeParent !== false);

        return result && !this.hasMenu;
    }

    changeMenu(config, existingMenu) {
        const
            me = this,
            { constrainTo, scrollAction } = me.owner;

        // This covers both Array and Object which are valid items config formats.
        // menu could be { itemRef : { text : 'sub item 1 } }. But if it has
        // child items or html property in it, it's the main config
        if (config && typeof config === 'object' && !('items' in config) && !('widgets' in config) && !('html' in config)) {
            config = {
                lazyItems : config
            };
        }

        return Menu.reconfigure(existingMenu, config, {
            owner    : me,
            defaults : {
                type       : 'menu',
                align      : 's0-e0',
                anchor     : true,
                autoClose  : true,
                autoShow   : false,
                cls        : 'b-sub-menu', // Makes the anchor hoverable to avoid mouseleave
                forElement : me.element,
                owner      : me,
                ariaLabel  : me.text,

                constrainTo,
                scrollAction
            }
        });
    }
}

// Register this widget type with its Factory
MenuItem.initClass();
