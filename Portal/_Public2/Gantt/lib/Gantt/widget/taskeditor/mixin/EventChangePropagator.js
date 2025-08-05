import Base from '../../../../Common/Base.js';
import Events from '../../../../Common/mixin/Events.js';

/**
 * Mixin for task editor widgets to properly propagate event changes.
 *
 * Works in tandem with {@link Common/mixin/Events events} mixin.
 *
 * @mixin
 */
export default Target => class extends (Target || Events(Base)) {

    get isEventChangePropagator() {
        return true;
    }

    requestPropagation() {
        this.trigger('requestPropagation');
    }
};
