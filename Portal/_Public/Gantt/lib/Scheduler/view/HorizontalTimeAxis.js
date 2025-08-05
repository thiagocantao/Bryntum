import TimeAxisBase from './TimeAxisBase.js';

/**
 * @module Scheduler/view/HorizontalTimeAxis
 */

/**
 * A visual horizontal representation of the time axis described in the
 * {@link Scheduler.preset.ViewPreset#field-headers} field.
 * Normally you should not interact with this class directly.
 *
 * @extends Scheduler/view/TimeAxisBase
 * @private
 */
export default class HorizontalTimeAxis extends TimeAxisBase {

    //region Config

    static $name = 'HorizontalTimeAxis';

    static type = 'horizontaltimeaxis';

    static configurable = {
        model        : null,
        sizeProperty : 'width'
    };

    //endregion

    get positionProperty() {
        return this.owner?.rtl ? 'right' : 'left';
    }

    get width() {
        return this.size;
    }

    onModelUpdate() {
        // Force rebuild when availableSpace has changed, to recalculate width and maybe apply compact styling


        if (!this.owner?.hideHeaders && this.model.availableSpace > 0 && this.model.availableSpace !== this.width) {
            this.refresh(true);
        }
    }

    updateModel(timeAxisViewModel) {
        this.detachListeners('tavm');

        timeAxisViewModel?.ion({
            name    : 'tavm',
            update  : 'onModelUpdate',
            thisObj : this
        });
    }
}
