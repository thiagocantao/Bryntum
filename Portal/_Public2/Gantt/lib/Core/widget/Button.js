import Widget from './Widget.js';
import TemplateHelper from '../helper/TemplateHelper.js';
import Badge from './mixin/Badge.js';
import DomHelper from '../helper/DomHelper.js';
import EventHelper from '../helper/EventHelper.js';
import Objects from '../helper/util/Objects.js';

//TODO: should togglebutton be own class implemented as input type=checkbox?
//TODO: A toggling widget's focusElement should be an opacity:0 input type=checkbox which covers the clickable area.

/**
 * @module Core/widget/Button
 */

const
    bIcon = /^b-icon-/,
    bFa = /^b-fa-/;

/**
 * Button widget, wraps and styles a regular <code>&lt;button&gt;</code> element. Can display text and icon and allows specifying button color.
 *
 * @extends Core/widget/Widget
 * @mixes Core/widget/mixin/Badge
 *
 * @example
 * // button with text and icon
 * let button = new Button({
 *   icon: 'b-fa-plus-circle',
 *   text: 'Add',
 *   color: 'green',
 *   onClick: () => {}
 * });
 *
 * @classType button
 * @externalexample widget/Button.js
 */
export default class Button extends Badge(Widget) {
    //region Config
    static get $name() {
        return 'Button';
    }

    // Factoryable type name
    static get type() {
        return 'button';
    }

    static get configurable() {
        return {
            /**
             * Get/set the Button icon
             * @property {String}
             * @name icon
             */
            /**
             * Button icon class.
             *
             * All [Font Awesome](https://fontawesome.com/cheatsheet) icons may also be specified as `'b-fa-' + iconName`.
             *
             * Otherwise this is a developer-defined CSS class string which results in the desired icon.
             * @config {String}
             */
            icon : null,

            /**
             * Get/set the Button pressed icon
             * @property {String}
             * @name pressedIcon
             */
            /**
             * Icon class for the buttons pressed state. Only applies to toggle buttons
             *
             * All [Font Awesome](https://fontawesome.com/cheatsheet) icons may also be specified as `'b-fa-' + iconName`.
             *
             * Otherwise this is a developer-defined CSS class string which results in the desired icon.
             *
             * ```
             * new Button({
             *    // Icon for unpressed button
             *    icon        : 'b-fa-wine-glass',
             *
             *    // Icon for pressed button
             *    pressedIcon : 'b-fa-wine-glass-alt',
             *
             *    // Only applies to toggle buttons
             *    toggleable  : true
             * });
             * ```
             *
             * @config {String}
             */
            pressedIcon : null,

            /**
             * Get/Set the Button icon alignment.
             * May be `'start'` or `'end'`. Defaults to `'start'`
             * @property {String}
             * @name iconAlign
             */
            /**
             * Button icon alignment. May be `'start'` or `'end'`. Defaults to `'start'`
             * @config {String}
             */
            iconAlign : 'start',

            /**
             * Get/set text displayed on the button.
             * @property {String}
             * @name text
             */
            /**
             * The button's text.
             * @config {String}
             */
            text : '',

            /**
             * Button color (should have match in button.scss or your custom styling). Valid values in Bryntum themes
             * are:
             * * b-amber
             * * b-blue
             * * b-dark-gray
             * * b-deep-orange
             * * b-gray
             * * b-green
             * * b-indigo
             * * b-lime
             * * b-light-gray
             * * b-light-green
             * * b-orange
             * * b-purple
             * * b-red
             * * b-teal
             * * b-white
             * * b-yellow
             * Combine with specifying `b-raised` for raised/filled style (theme dependent).
             *
             * ```
             * new Button({
             *    color : 'b-teal b-raised'
             * });
             * ```
             *
             * @config {String}
             */
            color : null,

            /**
             * Enabled toggling of the button (stays pressed when pressed).
             * @config {Boolean}
             * @default
             */
            toggleable : false,

            /**
             * Get/set button pressed state
             * @property {Boolean}
             * @name pressed
             */
            /**
             * Initially pressed or not. Only applies with `toggleable = true`.
             * ```
             * const toggleButton = new Button({
             *    toggleable : true,
             *    text : 'Enable cool action'
             * });
             * ```
             * @config {Boolean}
             * @default
             */
            pressed : false,

            /**
             * Indicate that this button is part of a group where only one button can be pressed. Assigning a value
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

            ripple : {
                radius : 75
            },

            defaultBindProperty : 'text',

            localizableProperties : ['text'],

            /**
             * Returns the instantiated menu widget as configured by {@link #config-menu}.
             * @property {Core.widget.Widget}
             * @name menu
             */
            /**
             * A submenu configuration object, or an array of MenuItem configuration
             * objects from which to create a submenu which is shown when this button is pressed.
             *
             * Note that this does not have to be a Menu. The `type` config can be used
             * to specify any widget as the submenu.
             *
             * May also be specified as a fully instantiated {@link Core.widget.Widget#config-floating floating Widget}
             * such as a {@link Core.widget.Popup Popup}.
             * @config {Object|Object[]|Core.widget.Widget}
             */
            menu : {
                $config : ['lazy', 'nullify'],
                value   : null
            },

            menuDefaults : {
                type         : 'menu',
                autoShow     : false,
                autoClose    : true,
                floating     : true,
                scrollAction : 'realign',
                align        : 't0-b0'
            },

            /**
             * If provided, turns the button into a link
             * @config {String}
             */
            href : null,

            /**
             * The `target` attribute for the {@link #config-href} config
             * @config {String}
             */
            target : null,

            onMenuHideTimeout : 300,

            testConfig : {
                onMenuHideTimeout : 0
            }
        };
    }

    template() {
        const
            me  = this,
            tag = me.href ? 'a' : 'button';

        return TemplateHelper.tpl`
            <${tag} ${tag === 'a' ? `href="${me.href}"` : ''} ${tag === 'a' && me.target ? `target="${me.target}"` : ''} class="${me.color || ''} ${me.toggleable && me.pressed ? 'b-pressed' : ''}" ${me.toggleGroup ? `data-group="${me.toggleGroup}"` : ''}>
                <label data-reference="label">\xa0</label>
            </${tag}>
        `;
    }

    //endregion

    //region Construct/Destroy

    construct(config = {}, ...args) {
        if (config.toggleGroup || config.menu) {
            config.toggleable = true;
        }

        super.construct(config, ...args);

        EventHelper.on({
            element : this.element,
            click   : 'onInternalClick',
            thisObj : this
        });
    }

    //endregion

    /**
     * Iterate over all widgets owned by this widget and any descendants.
     *
     * *Note*: Due to this method aborting when the function returns `false`, beware of using short form arrow
     * functions. If the expression executed evaluates to `false`, iteration will terminate.
     *
     * _Due to the {@link #config-menu} config being a lazy config and only being converted to be a
     * `Menu` instance just before it's shown, the menu will not be part of the iteration before
     * it has been shown once_.
     * @function eachWidget
     * @param {Function} fn A function to execute upon all descendant widgets.
     * Iteration terminates if this function returns `false`.
     * @param {Boolean} [deep=true] Pass as `false` to only consider immediate child widgets.
     * @returns {Boolean} Returns `true` if iteration was not aborted by a step returning `false`
     */

    get childItems() {
        if (this._menu) {
            return [this.menu];
        }
    }

    onFocusOut(e) {
        super.onFocusOut(e);
        if (this.menu) {
            this.menu.hide();
        }
    }

    //region Getters/Setters

    get focusElement() {
        return this.element;
    }

    changeText(text) {
        return (text == null) ? '' : text;
    }

    updateText(text) {
        const { element, label } = this;

        label.innerText = text;
        element.classList[String(text).length ? 'add' : 'remove']('b-text');
    }

    get menuContainerElement() {
        return document.body;
    }

    changeMenu(menu, oldMenu) {
        const me = this;

        if (menu) {
            // In case the reason for the hide is a mousedown
            // on this button, and it's not hiding because we are being destroyed,
            // wait until after any impending click handler to sync our state with the visibility.
            const onHide = () => !me.isDestroying && me.setTimeout(() => me.toggle(false), me.onMenuHideTimeout);

            if (menu.isWidget) {
                menu.forElement = me.element;
                menu.on('hide', onHide);
            }
            else {
                // This covers both Array and Object which are valid items config formats.
                // menu could be { itemRef : { text : 'sub item 1 } }. But if it has
                // child items or html property in it, it's the main config
                if (typeof menu === 'object' && !('type' in menu || 'items' in menu || 'widgets' in menu || 'html' in menu)) {
                    menu = {
                        lazyItems : menu
                    };
                }

                menu = Widget.reconfigure(oldMenu, menu ? Objects.merge({
                    constrainTo : me.menuContainerElement,
                    forElement  : me.element,
                    owner       : me,
                    onHide
                }, me.menuDefaults, menu) : null, me);
            }
        }
        else {
            if (oldMenu) {
                oldMenu.destroy();
            }
        }

        return menu;
    }

    updateMenu(menu) {
        // We are toggleable if there's a menu.
        // Pressed means menu visible, not pressed means menu hidden.
        this.toggleable = Boolean(menu);
    }

    updatePressed(pressed) {
        const me = this;

        if (pressed && me.toggleGroup) {
            DomHelper.forEachSelector(`button[data-group=${me.toggleGroup}]`, btnEl => {
                if (btnEl !== me.element) {
                    Widget.getById(btnEl.id).toggle(false);
                }
            });
        }

        if (me.element) {
            me.element.classList[pressed ? 'add' : 'remove']('b-pressed');
        }
    }

    updateIcon() {
        this.syncIconCls();
    }

    updatePressedIcon() {
        this.syncIconCls();
    }

    updateIconAlign(iconAlign, oldAlign) {
        const classList = this.element.classList;

        if (oldAlign) {
            classList.remove(`b-icon-align-${oldAlign}`);
        }

        if (this.icon) {
            classList.add(`b-icon-align-${iconAlign}`);
        }
    }

    //endregion

    //region Events

    /**
     * Triggers events when user clicks button
     * @fires click
     * @fires action
     * @private
     */
    onInternalClick(event) {
        const
            me           = this,
            bryntumEvent = { event };

        if (me.toggleable) {
            // Clicking the pressed button in a toggle group should do nothing
            if (me.toggleGroup && me.pressed) {
                return;
            }

            me.toggle(!me.pressed);

            // Edge case in dragfromgrid demo, where toggling mode destroys the Scheduler and thus destroys the toolbar
            // and the button in it
            if (me.isDestroyed) {
                return;
            }
        }

        /**
         * User clicked button
         * @event click
         * @property {Core.widget.Button} button - Clicked button
         * @property {Event} event - DOM event
         */
        me.trigger('click', bryntumEvent);

        /**
         * User performed the default action (clicked the button)
         * @event action
         * @property {Core.widget.Button} button - Clicked button
         * @property {Event} event - DOM event
         */
        // A handler may have resulted in destruction.
        if (!me.isDestroyed) {
            me.trigger('action', bryntumEvent);
        }

        // since Widget has Events mixed in configured with 'callOnFunctions' this will also call onClick and onAction

        if (!this.href) {
            // stop the event since it has been handled
            event.preventDefault();
            event.stopPropagation();
        }
    }

    //endregion

    //region Toggle

    /**
     * Toggle button state (only use with toggleable = true)
     * @param {Boolean} pressed Specify to force a certain toggle state
     * @fires toggle
     */
    toggle(pressed = null) {
        const
            me       = this,
            { menu } = me;

        if (!me.toggleable) {
            return;
        }

        if (pressed === null) {
            pressed = !me.pressed;
        }

        me.pressed = pressed;

        // For handlers from the code below to detect and avoid recursion
        me.toggling = true;

        if (menu) {
            menu.minWidth = me.width;
            menu[pressed ? 'show' : 'hide']();
        }

        me.syncIconCls();

        /**
         * Button state was toggled
         * @event toggle
         * @property {Core.widget.Button} button - Button
         * @property {Boolean} pressed - New pressed state
         */
        me.trigger('toggle', { pressed });

        me.toggling = false;
    }

    //endregion

    //region Other

    syncIconCls() {
        const me = this;
        let iconEl = me._iconEl;

        if (me.icon) {
            if (!iconEl) {
                iconEl = me._iconEl = DomHelper.createElement({
                    tag         : 'i',
                    nextSibling : me.element.firstChild
                });
            }

            const iconCls = (me.pressed && me.pressedIcon) ? me.pressedIcon : me.icon;
            me._iconEl.className = iconCls;

            // if it's a supplied icon class b-fa-xxx or b-icon-xxx, add extra class
            // so user doesn't have to; they can just specify the icon
            if (bIcon.test(iconCls)) {
                iconEl.classList.add('b-icon');
            }
            else if (bFa.test(iconCls)) {
                iconEl.classList.add('b-fa');
            }
        }
        else {
            iconEl && iconEl.remove();
        }
    }

    //endregion
}

// Register this widget type with its Factory
Button.initClass();
