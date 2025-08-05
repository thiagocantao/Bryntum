import Container from './Container.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import Button from './Button.js';
import IdHelper from '../helper/IdHelper.js';

/**
 * @module Common/widget/ButtonGroup
 */

/**
 * A specialized container that holds buttons, displaying them in a horizontal group with borders adjusted to make them
 * stick together.
 *
 * Trying to add other widgets than buttons will throw an exception.
 *
 * ```javascript
 * new ButtonGroup({
 *   items : [
 *       { icon : 'b-fa b-fa-kiwi-bird' },
 *       { icon : 'b-fa b-fa-kiwi-otter' },
 *       { icon : 'b-fa b-fa-kiwi-rabbit' },
 *       ...
 *   ]
 * });
 * ```
 *
 * @externalexample widget/ButtonGroup.js
 * @classType buttonGroup
 * @extends Common/widget/Container
 */
export default class ButtonGroup extends Container {
    static get defaultConfig() {
        return {
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
             * @config {Object[]|Common.widget.Button[]}
             */
            items : null,

            /**
             * Default color to apply to all contained buttons, see {@link Common.widget.Button#config-color Button#color}.
             * Individual buttons can override the default.
             * @config {String}
             */
            color : null,

            /**
             * Set to `true` to turn the ButtonGroup into a toggle group, assigning a generated value to each contained
             * buttons {@link Common.widget.Button#config-toggleGroup toggleGroup config}. Individual buttons can
             * override the default.
             */
            toggleGroup : null
        };
    }

    createWidget(widget) {
        const me = this;

        if (me.color && !widget.color) {
            widget.color = me.color;
        }

        if (me.toggleGroup && !widget.toggleGroup) {

            if (typeof me.toggleGroup === 'boolean') {
                me.toggleGroup = IdHelper.generateId('toggleGroup');
            }

            widget.toggleGroup = me.toggleGroup;
        }

        const button = super.createWidget(widget);

        if (!(button instanceof Button)) {
            throw new Error('A ButtonGroup can only contain buttons');
        }

        return button;
    }
}

BryntumWidgetAdapterRegister.register('buttonGroup', ButtonGroup);
