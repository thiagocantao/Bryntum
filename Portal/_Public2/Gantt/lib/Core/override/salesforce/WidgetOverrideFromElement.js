import Widget from '../../widget/Widget.js';
import Override from '../../mixin/Override.js';

/*
 * This override replaces instanceof Event check with a specific property lookup
 */
class WidgetOverrideFromElement {
    static get target() {
        return {
            class : Widget
        };
    }

    // Cannot use `instanceof Event` assertions here
    static fromElement(element, type, limit) {
        if (element && 'stopImmediatePropagation' in element) {
            element = element.target;
        }

        return this._overridden.fromElement.call(this, element, type, limit);
    }
}

Override.apply(WidgetOverrideFromElement);
