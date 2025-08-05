import Tooltip from '../../widget/Tooltip.js';
import Override from '../../mixin/Override.js';
import { getLWCElement } from './Init.js';

/*
 * Fixes tooltips in general, particularly dependency dragcreate tooltip
 */
class TooltipOverrideListenersTarget {
    static get target() {
        return {
            class : Tooltip
        };
    }

    static get listenersTarget() {
        return getLWCElement();
    }
}

Override.apply(TooltipOverrideListenersTarget);
