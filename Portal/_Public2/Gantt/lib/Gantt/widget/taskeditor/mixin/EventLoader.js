import Container from '../../../../Common/widget/Container.js';

/**
 * @module Gantt/widget/taskeditor/mixin/EventLoader
 */

/**
 * Mixin class for task editor widgtes which require record loading functionality
 *
 * @mixin
 */
export default Target => class extends (Target || Container) {

    getProject() {
        return this.record && this.record.getProject();
    }

    loadEvent(record) {
        this.record = record;
    }

    resetData() {
        this.record = null;
    }

    beforeSave() {}

    afterSave() {
        this.resetData();
    }

    beforeCancel() {}

    afterCancel() {
        this.resetData();
    }

    beforeDelete() {}

    afterDelete() {
        this.resetData();
    }
};
