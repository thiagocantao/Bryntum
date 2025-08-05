import VerticalRendering from '../../view/orientation/VerticalRendering.js';
import Override from '../../../Core/mixin/Override.js';

/*
 * This override replaces instanceof Event check with specific property lookup
 */
class VerticalRenderingOverrideResolveRowRecord {
    static get target() {
        return {
            class : VerticalRendering
        };
    }

    resolveRowRecord(elementOrEvent, xy) {
        if (elementOrEvent && 'stopImmediatePropagation' in elementOrEvent) {
            elementOrEvent = elementOrEvent.target;
        }

        return this._overridden.resolveRowRecord.call(this, elementOrEvent, xy);
    }
}

Override.apply(VerticalRenderingOverrideResolveRowRecord);
