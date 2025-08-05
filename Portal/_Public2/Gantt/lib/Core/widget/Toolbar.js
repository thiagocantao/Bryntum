import Container from './Container.js';
import './Button.js';

/**
 * @module Core/widget/Toolbar
 */

/**
 * A container widget that can contain Buttons or other widgets, and is docked to the bottom or top of
 * a {@link Core.widget.Panel Panel}.
 *
 * @extends Core/widget/Container
 * @classType toolbar
 * @externalexample widget/Toolbar.js
 */
export default class Toolbar extends Container {
    static get $name() {
        return 'Toolbar';
    }

    // Factoryable type name
    static get type() {
        return 'toolbar';
    }

    static get configurable() {
        return {
            defaultType : 'button',

            /**
             * Custom CSS class to add to toolbar widgets
             * @config {String}
             * @category CSS
             */
            widgetCls : null,

            layout : 'default'
        };
    }

    createWidget(widget) {
        if (widget === '->') {
            widget = {
                type : 'widget',
                cls  : 'b-toolbar-fill'
            };
        }
        else if (widget === '|') {
            widget = {
                type : 'widget',
                cls  : 'b-toolbar-separator'
            };
        }
        else if (typeof widget === 'string') {
            widget = {
                type : 'widget',
                cls  : 'b-toolbar-text',
                html : widget
            };
        }

        const result = super.createWidget(widget);

        if (this.widgetCls) {
            result.element.classList.add(this.widgetCls);
        }

        return result;
    }
}

// Register this widget type with its Factory
Toolbar.initClass();
