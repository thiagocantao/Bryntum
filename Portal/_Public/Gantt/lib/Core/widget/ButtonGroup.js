import Container from './Container.js';
import Rotatable from './mixin/Rotatable.js';

import './Button.js';

/**
 * @module Core/widget/ButtonGroup
 */

/**
 * A specialized container that holds buttons, displaying them in a horizontal group with borders adjusted to make them
 * stick together.
 *
 * Trying to add other widgets than buttons will throw an exception.
 *
 * ```javascript
 * new ButtonGroup({
 *     items : [
 *         { icon : 'b-fa b-fa-kiwi-bird' },
 *         { icon : 'b-fa b-fa-kiwi-otter' },
 *         { icon : 'b-fa b-fa-kiwi-rabbit' },
 *         ...
 *     ]
 * });
 * ```
 *
 * @inlineexample Core/widget/ButtonGroup.js
 * @classType buttonGroup
 * @extends Core/widget/Container
 * @widget
 */
export default class ButtonGroup extends Container.mixin(Rotatable) {

    /**
     * Fires when a button in the group is clicked
     * @event click
     * @param {Core.widget.Button} source Clicked button
     * @param {Event} event DOM event
     */

    /**
     * Fires when the default action is performed on a button in the group (the button is clicked)
     * @event action
     * @param {Core.widget.Button} source Clicked button
     * @param {Event} event DOM event
     */

    /**
     * Fires when a button in the group is toggled (the {@link Core.widget.Button#property-pressed} state is changed).
     * If you need to process the pressed button only, consider using {@link #event-click} event or {@link #event-action} event.
     * @event toggle
     * @param {Core.widget.Button} source Toggled button
     * @param {Boolean} pressed New pressed state
     * @param {Event} event DOM event
     */

    static $name = 'ButtonGroup';

    static type = 'buttongroup';

    static configurable = {
        defaultType : 'button',

        /**
         * Custom CSS class to add to element. When using raised buttons (cls 'b-raised' on the buttons), the group
         * will look nicer if you also set that cls on the group.
         *
         * ```
         * new ButtonGroup({
         *   cls : 'b-raised,
         *   items : [
         *       { icon : 'b-fa b-fa-unicorn', cls : 'b-raised' },
         *       ...
         *   ]
         * });
         * ```
         *
         * @config {String}
         * @category CSS
         */
        cls : null,

        /**
         * An array of Buttons or typed Button config objects.
         * @config {ButtonConfig[]|Core.widget.Button[]}
         */
        items : null,

        /**
         * Default color to apply to all contained buttons, see {@link Core.widget.Button#config-color Button#color}.
         * Individual buttons can override the default.
         * @config {String}
         */
        color : null,

        /**
         * Set to `true` to turn the ButtonGroup into a toggle group, assigning a generated value to each contained
         * buttons {@link Core.widget.Button#config-toggleGroup toggleGroup config}. Individual buttons can
         * override the default.
         * @config {Boolean}
         */
        toggleGroup : null,

        valueSeparator : ',',

        columns : null,

        hideWhenEmpty : true,

        defaultBindProperty : 'value'
    };

    onChildAdd(item) {
        super.onChildAdd(item);

        item.ion({
            click   : 'resetValueCache',
            toggle  : 'onItemToggle',
            thisObj : this,
            // This needs to run before the 'click' event is relayed by this button group, in such listener
            // the `value` must already be updated
            prio    : 10000
        });
    }

    onChildRemove(item) {
        item.un({
            toggle  : 'resetValueCache',
            click   : 'resetValueCache',
            thisObj : this
        });
        super.onChildRemove(item);
    }

    onItemToggle(event) {
        const me = this;

        me.resetValueCache();

        if (!me.isSettingValue && (!me.toggleGroup || event.pressed)) {
            me.triggerFieldChange({ value : me.value, userAction : true, event });
        }
    }

    resetValueCache() {
        // reset cached value to revalidate next time it's requested
        this._value = null;
    }

    createWidget(widget) {
        const
            me   = this,
            type = me.constructor.resolveType(widget.type || 'button');

        if (type.isButton) {
            if (me.color && !widget.color) {
                widget.color = me.color;
            }

            if (me.toggleGroup && !widget.toggleGroup) {
                if (typeof me.toggleGroup === 'boolean') {
                    me.toggleGroup = ButtonGroup.generateId('toggleGroup');
                }

                widget.toggleGroup = me.toggleGroup;
            }
        }

        if (me.columns) {
            widget.width = `${100 / me.columns}%`;
        }

        widget = super.createWidget(widget);

        me.relayEvents(widget, ['click', 'action', 'toggle']);

        return widget;
    }

    updateRotate(rotate) {
        this.eachWidget(btn => {
            if (btn.rotate !== false) {
                btn.rotate = rotate;
            }
        });
    }

    get value() {
        // if we don't have cached value
        // let's calculate it based on item values
        if (!this._value) {
            const values = [];

            // collect pressed item values
            this.items.forEach(w => {
                if (w.pressed && w.value !== undefined) {
                    values.push(w.value);
                }
            });

            // build a string
            this._value = values.join(this.valueSeparator);
        }

        return this._value;
    }

    set value(value) {
        const
            me       = this,
            oldValue = me.value;

        if (!Array.isArray(value)) {
            if (value === undefined || value === null) {
                value = [];
            }
            else if (typeof value == 'string') {
                value = value.split(me.valueSeparator);
            }
            else {
                value = [value];
            }
        }

        me._value = value.join(me.valueSeparator);

        me.isSettingValue = true;

        // Reflect value on items
        me.items.forEach(w => {
            if (w.value !== undefined) {
                w.pressed = value.includes(w.value);
            }
        });

        me.isSettingValue = false;

        if (!me.isConfiguring && oldValue !== me.value) {
            me.triggerFieldChange({ value : me.value, userAction : false });
        }
    }

    updateDisabled(disabled) {
        this.items.forEach(button => button.disabled = disabled || (!button.ignoreParentReadOnly && this.readOnly));
    }

    updateReadOnly(readOnly) {
        super.updateReadOnly(readOnly);

        this.updateDisabled(this.disabled);
    }

    get widgetClassList() {
        const classList = super.widgetClassList;
        // if the buttons should be shown in rows
        this.columns && classList.push('b-columned');
        return classList;
    }
}

// Register this widget type with its Factory
ButtonGroup.initClass();
