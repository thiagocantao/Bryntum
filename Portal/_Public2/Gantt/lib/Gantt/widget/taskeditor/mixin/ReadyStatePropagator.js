import Base from '../../../../Common/Base.js';
import Events from '../../../../Common/mixin/Events.js';

export default Target => class extends (Target || Events(Base)) {

    get isReadyStatePropagator() {
        return true;
    }

    get canSave() {
        return true;
    }

    requestReadyStateChange() {
        this.trigger('readystatechange', { canSave : this.canSave });
    }
};
