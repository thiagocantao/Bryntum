import Store from '../../Core/data/Store.js';
import Popup from '../../Core/widget/Popup.js';
import '../../Core/widget/SlideToggle.js';

/**
 * @module Scheduler/view/EventEditor
 */

/**
 * Provided event editor dialog.
 *
 * @extends Core/widget/Popup
 * @private
 */
export default class EventEditor extends Popup {
    // Factoryable type name
    static get type() {
        return 'eventeditor';
    }

    static get $name() {
        return 'EventEditor';
    }

    static get configurable() {
        return {
            items     : [],
            draggable : {
                handleSelector : ':not(button,.b-field-inner)' // Ignore buttons and field inners
            },
            axisLock : 'flexible',

            scrollable : {
                // In case editor is very tall or window is small, make it scrollable
                overflowY : true
            },
            readOnly : null,

            /**
             * A Function (or *name* of a function) which produces a customized Panel header based upon the event being edited.
             * @config {Function|String}
             * @param {Scheduler.model.EventModel} eventRecord The record being edited
             * @returns {String} The Panel title.
             */
            titleRenderer : null,

            // We want to maximize on phones and tablets
            maximizeOnMobile : true
        };
    }

    updateLocalization() {
        super.updateLocalization(...arguments);

        // Use this if there's no titleRenderer
        this.initialTitle = this.title || '';
    }

    chainResourceStore() {
        return this.eventEditFeature.resourceStore.chain(
            record => !record.isSpecialRow,
            null,
            {
                // It doesn't need to be a Project-based Store
                storeClass              : Store,
                // Need to show all records in the combo. Required in case resource store is a tree.
                excludeCollapsedRecords : false
            }
        );
    }

    processWidgetConfig(widget) {
        if (widget.type?.includes('date') && widget.weekStartDay == null) {
            widget.weekStartDay = this.weekStartDay;
        }

        if (widget.type === 'extraItems') {
            return false;
        }

        const
            { eventEditFeature } = this,
            fieldConfig          = {};

        if (widget.ref === 'resourceField') {
            const { store } = widget;

            // Can't use store directly since it may be grouped and then contains irrelevant group records
            widget.store = this.chainResourceStore();

            // Allow the incoming widget's config to augment its store
            if (store) {
                widget.store.setConfig(store);
            }

            // When events are loaded with resourceId, we should only support single select.
            // Only override this if the widget has not been explicitly configured
            // with multiSelect.
            if (!('multiSelect' in widget)) {
                widget.multiSelect = !eventEditFeature.eventStore.usesSingleAssignment;
            }
        }

        if ((widget.name === 'startDate' || widget.name === 'endDate') && widget.type === 'date') {
            fieldConfig.format = eventEditFeature.dateFormat;
        }

        if ((widget.name === 'startDate' || widget.name === 'endDate') && widget.type === 'time') {
            fieldConfig.format = eventEditFeature.timeFormat;
        }

        Object.assign(widget, fieldConfig);

        return super.processWidgetConfig(widget);
    }

    setupEditorButtons() {
        const
            { record }       = this,
            { deleteButton } = this.widgetMap;

        // Hide delete button if we are readOnly or the event is in a create phase
        // which means we are editing a dblclick-created or drag-created event.
        if (deleteButton) {
            deleteButton.hidden = this.readOnly || record.isCreating;
        }
    }

    onBeforeShow(...args) {
        const
            me               = this,
            {
                record,
                titleRenderer
            }                = me;

        me.setupEditorButtons();

        if (titleRenderer) {
            me.title = me.callback(titleRenderer, me, [record]);
        }
        else {
            me.title = me.initialTitle;
        }

        super.onBeforeShow?.(...args);
    }

    onInternalKeyDown(event) {
        this.trigger('keyDown', { event });
        super.onInternalKeyDown(event);
    }

    updateReadOnly(readOnly) {
        const
            {
                deleteButton,
                saveButton,
                cancelButton
            } = this.widgetMap;

        super.updateReadOnly(readOnly);

        if (deleteButton) {
            deleteButton.hidden = readOnly;
        }

        if (saveButton) {
            saveButton.hidden = readOnly;
        }

        if (cancelButton) {
            cancelButton.hidden = readOnly;
        }
    }
}

// Register this widget type with its Factory
EventEditor.initClass();
