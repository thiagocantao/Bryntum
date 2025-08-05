import DomHelper from '../../helper/DomHelper.js';
import Override from '../../mixin/Override.js';
import BrowserHelper from '../../helper/BrowserHelper.js';
import EventHelper from '../../helper/EventHelper.js';
import { getLWCElement } from './Init.js';
import Widget from '../../widget/Widget.js';

/*
 * This override moves floatRoot to each individual LWC, no other way to fix key navigation in menus/combos/popups in
 * general. Downside is that at the moment it is not always possible to make components to share the bundle.
 */
class WidgetOverrideFloatRoot {
    static get target() {
        return {
            class : Widget
        };
    }

    static get floatRoot() {
        const
            me         = this,
            lwcElement = getLWCElement();

        // In case of expired trial this could be referred to early
        if (!lwcElement) {
            return null;
        }

        const parent = lwcElement.firstChild;

        if (!me._floatRoot) {
            // Reuse any existing floatRoot. There might be one if using multiple product bundles
            me._floatRoot = lwcElement.querySelector('.b-float-root');
        }

        if (!me._floatRoot) {
            me._floatRoot = DomHelper.createElement({
                className : 'b-float-root',
                parent
            });

            // Make float root immune to keyboard-caused size changes
            if (BrowserHelper.isAndroid) {
                me._floatRoot.style.height = `${screen.height}px`;
                EventHelper.on({
                    element           : window,
                    orientationchange : () => me._floatRoot.style.height = `${screen.height}px`
                });
            }
        }
        else if (!parent.contains(me._floatRoot)) {
            // Reattach floatRoot if it was detached
            parent.appendChild(me._floatRoot);
        }

        return me._floatRoot;
    }
}

Override.apply(WidgetOverrideFloatRoot);
