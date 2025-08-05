import DragHelper from '../../helper/DragHelper.js';
import Override from '../../mixin/Override.js';
import { getLWCElement } from './Init.js';
import EventHelper from '../../helper/EventHelper.js';
import FunctionHelper from '../../helper/FunctionHelper.js';

/*
 * This override replaces default target (which is document) with LWC container element.
 */
class DragHelperOverrideListenersTarget {
    static get target() {
        return {
            class : DragHelper
        };
    }

    get listenersTarget() {
        return getLWCElement();
    }

    onPointerDown(event) {
        this._overridden.onPointerDown.call(this, event);

        // If drag started, add one more listener to document body to handle mouseups outside of the current element
        if (this.context) {
            const
                isTouch       = 'touches' in event,
                dragListeners = {
                    element : document.body,
                    thisObj : this
                };

            if (isTouch) {
                // Touch desktops don't fire touchend event when touch has ended, instead pointerup is fired
                // iOS do fire touchend
                dragListeners.touchend = dragListeners.pointerup = 'onTouchEnd';
            }
            else {
                dragListeners.mouseup = 'onMouseUp';
            }

            this.removeListeners = FunctionHelper.createSequence(this.removeListeners, EventHelper.on(dragListeners));
        }
    }
}

Override.apply(DragHelperOverrideListenersTarget);
