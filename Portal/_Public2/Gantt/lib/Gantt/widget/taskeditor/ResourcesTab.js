import Container from '../../../Common/widget/Container.js';
import EventLoader from './mixin/EventLoader.js';
import EventChangePropagator from './mixin/EventChangePropagator.js';
import BryntumWidgetAdapterRegister from '../../../Common/adapter/widget/util/BryntumWidgetAdapterRegister.js';
import TaskEditorTab from './mixin/TaskEditorTab.js';

/**
 * @module Gantt/widget/taskeditor/ResourcesTab
 */

/**
 * A tab inside the {@link Gantt/widget/TaskEditor task editor} showing the assigned resources for a task.
 * @internal
 */
export default class ResourcesTab extends EventChangePropagator(TaskEditorTab(EventLoader(Container))) {

    static get type() {
        return 'resourcestab';
    }

    static get defaultConfig() {
        return {
            localeClass : this,

            title : 'L{Resources}',

            layoutStyle : {
                flexFlow : 'column nowrap'
            },

            items : [{
                type  : 'container',
                flex  : '0 0 auto',
                items : [{
                    type : 'button',
                    cls  : 'b-add-button b-green',
                    icon : 'b-icon b-icon-add',
                    ref  : 'resourcestab-add'
                }, {
                    type     : 'button',
                    cls      : 'b-remove-button b-red',
                    icon     : 'b-icon b-icon-trash',
                    disabled : true,
                    ref      : 'resourcestab-remove'
                }]
            }, {
                type    : 'grid',
                flex    : '1 1 auto',
                ref     : 'resourcestab-grid',
                columns : [{
                    localeClass : this,
                    text        : 'L{Resource}',
                    field       : 'resource',
                    flex        : 7,
                    renderer    : ({ value }) => {
                        return value && value.name || '';
                    },
                    editor : {
                        type         : 'modelcombo',
                        displayField : 'name',
                        valueField   : 'id',
                        editable     : false
                    }
                }, {
                    localeClass : this,
                    text        : 'L{Units}',
                    field       : 'units',
                    flex        : 3,
                    renderer    : (data) => {
                        return this.L('unitsTpl', data);
                    },
                    editor : {
                        type : 'numberfield',
                        min  : 0,
                        max  : 100,
                        step : 10
                    }
                }],

                disableGridRowModelWarning : true
            }]
        };
    }

    afterConstruct() {
        super.afterConstruct();

        const me = this,
            addButton = me.addButton = me.widgetMap['resourcestab-add'],
            removeButton = me.removeButton = me.widgetMap['resourcestab-remove'],
            grid = me.grid = me.widgetMap['resourcestab-grid'];

        addButton && addButton.on('click', me.onAddClick, me);
        removeButton && removeButton.on('click', me.onRemoveClick, me);

        grid.on({
            selectionChange : me.onGridSelectionChange,
            startCellEdit   : me.onGridStartCellEdit,
            finishCellEdit  : me.onGridFinishCellEdit,
            cancelCellEdit  : me.onGridCancelCellEdit,
            thisObj         : me
        });
    }

    get assignmentGrid() {
        return this.widgetMap && this.widgetMap['resourcestab-grid'];
    }

    get resourceCombo() {
        const grid = this.assignmentGrid,
            from = grid && grid.columns.get('resource');

        return from && from.editor;
    }

    loadEvent(eventRecord) {
        const me = this,
            grid = me.assignmentGrid,
            firstLoad = !grid.store.chained,
            recordChange = !firstLoad && (eventRecord !== me.record);

        //<debug>
        console.assert(
            firstLoad || grid.store.masterStore.getProject() === eventRecord.getProject(),
            'Loading of a record from another project is not currently supported!'
        );
        //</debug>

        super.loadEvent(eventRecord);

        const resourceCombo = me.resourceCombo,
            assignmentStore = me.getProject().getAssignmentStore(),
            resourceStore = me.getProject().getResourceStore();

        if (firstLoad) {
            // Cache the mutation generation of the underlying data collection
            // so that we know when we need to refill the chained stores.
            me.assignmentStoreGeneration = assignmentStore.storage.generation;
            me.resourceStoreGeneration = resourceStore.storage.generation;

            grid.store = assignmentStore.makeChained(a => a.event === me.record, ['resource']);
            resourceCombo.store = resourceStore.makeChained(r => me.record && r !== me.record.resource);
        }
        else {
            // Only repopulate the chained stores if the master stores have changed
            // or if this is being loaded with a different record.
            if (recordChange || assignmentStore.storage.generation !== me.assignmentStoreGeneration) {
                grid.store.fillFromMaster();
            }
            if (recordChange || resourceStore.storage.generation !== me.resourceStoreGeneration) {
                resourceCombo.store.fillFromMaster();
            }
        }
    }

    async insertNewAssignment() {
        const me = this,
            project = me.getProject(),
            assignmentStore = project.getAssignmentStore(),
            grid = me.assignmentGrid;

        const [newAssignment] = assignmentStore.insert(0, {
            event    : me.record,
            resource : null,
            units    : 100
        });

        // Reset the assignment store mutation monitor when we add a assignment
        me.assignmentStoreGeneration = assignmentStore.storage.generation;
        grid.features.cellEdit.startEditing({ field : 'resource', id : newAssignment.id });

        return newAssignment;
    }

    onAddClick() {
        this.insertNewAssignment();
    }

    onRemoveClick() {
        this.assignmentGrid.store.remove(this.assignmentGrid.selectedRecords);
        this.assignmentGrid.selectedRecords = null;
        this.removeButton.disable();
        this.requestPropagation();
    }

    onGridSelectionChange({ selection }) {
        if (selection && selection.length) {
            this.removeButton.enable();
        }
        else {
            this.removeButton.disable();
        }
    }

    onGridStartCellEdit({ editorContext }) {
        if (editorContext.column.field === 'resource') {
            this._editingAssignment = editorContext.record;
        }
    }

    onGridFinishCellEdit() {
        if (this._editingAssignment) {

            if (this._editingAssignment['resource']) {
                this.requestPropagation();
            }
            else {
                this._editingAssignment.remove();
            }

            this._editingAssignment = null;
        }
        else {
            this.requestPropagation();
        }
    }

    onGridCancelCellEdit() {
        if (this._editingAssignment) {
            if (!this._editingAssignment['resource']) {
                this._editingAssignment.remove();
            }

            this._editingAssignment = null;
        }
    }
}

BryntumWidgetAdapterRegister.register(ResourcesTab.type, ResourcesTab);
