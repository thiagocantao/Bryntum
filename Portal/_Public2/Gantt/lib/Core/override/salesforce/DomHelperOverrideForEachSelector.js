import DomHelper from '../../helper/DomHelper.js';
import Override from '../../mixin/Override.js';
import { getLWCElement } from './Init.js';

/*
 * This override changes forEachSelector fallback target from document to LWC container element
 */
class DomHelperOverrideForEachSelector {
    static get target() {
        return {
            class : DomHelper
        };
    }

    static forEachSelector(element, selector, fn) {
        if (typeof element === 'string') {
            fn = selector;
            selector = element;
            element = getLWCElement();
        }
        this.children(element, selector).forEach(fn);
    }
}

Override.apply(DomHelperOverrideForEachSelector);
