import HorizontalRendering from '../../view/orientation/HorizontalRendering.js';
import Override from '../../../Core/mixin/Override.js';

/*
 * This override replaces instanceof Event check with specific property lookup
 */
class HorizontalRenderingOverrideResolveRowRecord {
    static get target() {
        return {
            class : HorizontalRendering
        };
    }

    resolveRowRecord(elementOrEvent) {
        if (elementOrEvent && 'stopImmediatePropagation' in elementOrEvent) {
            elementOrEvent = elementOrEvent.target;
        }

        return this._overridden.resolveRowRecord.call(this, elementOrEvent);
    }
}

Override.apply(HorizontalRenderingOverrideResolveRowRecord);
