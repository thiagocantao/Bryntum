import Container from '../../../../Core/widget/Container.js';

/**
 * @module SchedulerPro/widget/taskeditor/mixin/EventLoader
 */

/**
 * Mixin class for task editor widgets which require record loading functionality
 *
 * @mixin
 * @mixinbase Container
 */
export default Target => class EventLoader extends (Target || Container) {

    get project() {
        // Use project set on editor by default, since ocurrences are not part of project.
        // Fall back to records project for tests that test editor in isolation.
        return this.up(w => w.isTaskEditorBase)?.project ?? this.record?.project;
    }

    loadEvent(record, highlightChanges) {
        this.setRecord(record, highlightChanges);
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

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
