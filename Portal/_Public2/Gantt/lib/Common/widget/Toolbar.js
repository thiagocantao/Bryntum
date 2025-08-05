import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import Container from './Container.js';

/**
 * @module Common/widget/Toolbar
 */

/**
 * Widget that is themed to contain Buttons which is docked to the bottom or top of
 * a {@link Common.widget.Panel Panel}.
 *
 * ```javascript
 * // create a toolbar with two buttons
 * let container = new Toolbar({
 *   items : [
 *     { text : 'Add' },
 *     { text : 'Delete' }
 *   ]
 * });
 * ```
 *
 * @extends Common/widget/Container
 * @classType toolbar
 */
export default class Toolbar extends Container {
    static get defaultConfig() {
        return {
            defaultType : 'button',

            layout : 'default'
        };
    }

    createWidget(widget) {
        if (widget === '->') {
            widget = {
                cls : 'b-toolbar-fill'
            };
        }

        return super.createWidget(widget);
    }
}

BryntumWidgetAdapterRegister.register('toolbar', Toolbar);
