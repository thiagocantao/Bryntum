/*
 * DomHelper.activeElement fix
 * https://github.com/salesforce/lwc/issues/1792
 *
 * Core uses shadowRoot to dive into webcomponents looking for active element. That doesn't work with LockerService.
 * This override relies on a bundle local (closure) variable `lwcElement`
 * @ignore
 */
import DomHelper from '../../helper/DomHelper.js';
import Override from '../../mixin/Override.js';
import { getLWCElement } from './Init.js';

class DomHelperOverrideActiveElement {
    static get target() {
        return {
            class : DomHelper
        };
    }

    static get activeElement() {
        const componentElement = getLWCElement();

        return componentElement?.activeElement;
    }
}

Override.apply(DomHelperOverrideActiveElement);
