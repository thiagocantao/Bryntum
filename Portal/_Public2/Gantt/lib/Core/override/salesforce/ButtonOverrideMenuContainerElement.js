import Button from '../../widget/Button.js';
import Override from '../../mixin/Override.js';
import { getLWCElement } from './Init.js';

/*
 * Menu by default is attached to document.body. We need to replace that with first real HTMLElement inside the LWC
 */
class ButtonOverrideMenuContainerElement {
    static get target() {
        return {
            class : Button
        };
    }

    get menuContainerElement() {
        return getLWCElement().firstChild;
    }
}

Override.apply(ButtonOverrideMenuContainerElement);
