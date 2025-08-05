import Widget from './Widget.js';
import EventHelper from '../helper/EventHelper.js';

/**
 * @module Core/widget/Tool
 */

/**
 * Base class for tools.
 *
 * May be configured with a `cls` and a `handler` which is a function (or name of a function)
 * in the owning Panel.
 * @extends Core/widget/Widget
 *
 * @classType tool
 */
export default class Tool extends Widget {

    static get $name() {
        return 'Tool';
    }

    // Factoryable type name
    static get type() {
        return 'tool';
    }

    // Align is a simple string at this level
    static get configurable() {
        return {
            align : {
                value   : null,
                $config : {
                    merge : 'replace'
                }
            }
        };
    }

    changeAlign(align) {
        return align;
    }

    template() {
        return `<button class="b-icon b-align-${this.align || 'end'}"></button>`;
    }

    construct(config) {
        super.construct(config);

        EventHelper.on({
            element : this.element,
            click   : 'onClick',
            thisObj : this
        });
    }

    get focusElement() {
        return this.element;
    }

    onClick(e) {
        const { handler, panel } = this;

        if (panel.trigger('toolclick', {
            domEvent : e,
            tool     : this
        }) !== false) {
            handler && this.callback(handler, panel, [e]);
        }
    }

    onInternalKeyDown(keyEvent) {
        const keyName = keyEvent.key.trim() || keyEvent.code;

        // Don't allow key invocations to bubble and trigger owning
        // widget's key handlers.
        if (keyName === 'Enter') {
            keyEvent.cancelBubble = true;
            keyEvent.stopPropagation();
        }
    }

    get panel() {
        return this.parent;
    }
}

// Register this widget type with its Factory
Tool.initClass();
