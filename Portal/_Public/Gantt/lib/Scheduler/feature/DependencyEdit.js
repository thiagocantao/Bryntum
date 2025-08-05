import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import '../../Core/widget/DisplayField.js';
import '../../Core/widget/DurationField.js';
import DependencyEditor from '../view/DependencyEditor.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import DependencyModel from '../model/DependencyModel.js';
import Duration from '../../Core/data/Duration.js';

/**
 * @module Scheduler/feature/DependencyEdit
 */

/**
 * Feature that displays a popup containing fields for editing a dependency. Requires the
 * {@link Scheduler.feature.Dependencies} feature to be enabled. Double click a line in the demo below to show the
 * editor.
 *
 * {@inlineexample Scheduler/feature/Dependencies.js}
 *
 * ## Customizing the built-in widgets
 *
 * ```javascript
 *  const scheduler = new Scheduler({
 *      columns : [
 *          { field : 'name', text : 'Name', width : 100 }
 *      ],
 *      features : {
 *          dependencies   : true,
 *          dependencyEdit : {
 *              editorConfig : {
 *                  items : {
 *                      // Custom label for the type field
 *                      typeField : {
 *                          label : 'Kind'
 *                      }
 *                  },
 *
 *                  bbar : {
 *                      items : {
 *                          // Hiding save button
 *                          saveButton : {
 *                              hidden : true
 *                          }
 *                      }
 *                  }
 *              }
 *          }
 *      }
 *  });
 * ```
 *
 * ## Built in widgets
 *
 * | Widget ref             | Type                              | Weight | Description               |
 * |------------------------|-----------------------------------|--------|---------------------------|
 * | `fromNameField`        | {@link Core.widget.DisplayField}  | 100    | From task name (readonly) |
 * | `toNameField`          | {@link Core.widget.DisplayField}  | 200    | To task name (readonly)   |
 * | `typeField`            | {@link Core.widget.Combo}         | 300    | Edit type                 |
 * | `lagField`             | {@link Core.widget.DurationField} | 400    | Edit lag                  |
 *
 * The built in buttons are:
 *
 * | Widget ref             | Type                       | Weight | Description                       |
 * |------------------------|----------------------------|--------|-----------------------------------|
 * | `saveButton`           | {@link Core.widget.Button} | 100    | Save button on the bbar           |
 * | `deleteButton`         | {@link Core.widget.Button} | 200    | Delete button on the bbar         |
 * | `cancelButton`         | {@link Core.widget.Button} | 300    | Cancel editing button on the bbar |
 *
 * This feature is **off** by default.
 * For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * @extends Core/mixin/InstancePlugin
 * @demo Scheduler/dependencies
 * @classtype dependencyEdit
 * @feature
 */
export default class DependencyEdit extends InstancePlugin {



    //region Config

    static get $name() {
        return 'DependencyEdit';
    }

    static get configurable() {
        return {
            /**
             * True to hide this editor if a click is detected outside it (defaults to true)
             * @config {Boolean}
             * @default
             * @category Editor
             */
            autoClose : true,

            /**
             * True to save and close this panel if ENTER is pressed in one of the input fields inside the panel.
             * @config {Boolean}
             * @default
             * @category Editor
             */
            saveAndCloseOnEnter : true,

            /**
             * True to show a delete button in the form.
             * @config {Boolean}
             * @default
             * @category Editor widgets
             */
            showDeleteButton : true,

            /**
             * The event that shall trigger showing the editor. Defaults to `dependencydblclick`, set to empty string or
             * `null` to disable editing of dependencies.
             * @config {String}
             * @default
             * @category Editor
             */
            triggerEvent : 'dependencydblclick',

            /**
             * True to show the lag field for the dependency
             * @config {Boolean}
             * @default
             * @category Editor widgets
             */
            showLagField : false,

            dependencyRecord : null,

            /**
             * Default editor configuration, used to configure the Popup.
             * @config {PopupConfig}
             * @category Editor
             */
            editorConfig : {
                title       : 'L{Edit dependency}',
                localeClass : this,
                closable    : true,

                defaults : {
                    localeClass : this
                },
                items : {
                    /**
                     * Reference to the from name
                     * @member {Core.widget.DisplayField} fromNameField
                     * @readonly
                     */
                    fromNameField : {
                        type   : 'display',
                        weight : 100,
                        label  : 'L{From}'
                    },
                    /**
                     * Reference to the to name field
                     * @member {Core.widget.DisplayField} toNameField
                     * @readonly
                     */
                    toNameField : {
                        type   : 'display',
                        weight : 200,
                        label  : 'L{To}'
                    },
                    /**
                     * Reference to the type field
                     * @member {Core.widget.Combo} typeField
                     * @readonly
                     */
                    typeField : {
                        type                  : 'combo',
                        weight                : 300,
                        label                 : 'L{Type}',
                        name                  : 'type',
                        editable              : false,
                        valueField            : 'id',
                        displayField          : 'name',
                        localizeDisplayFields : true,
                        buildItems            : function() {
                            const dialog = this.parent;

                            return Object.keys(DependencyModel.Type).map(type => ({
                                id        : DependencyModel.Type[type],
                                name      : dialog.L(type),
                                localeKey : type
                            }));
                        }
                    },

                    /**
                     * Reference to the lag field
                     * @member {Core.widget.DurationField} lagField
                     * @readonly
                     */
                    lagField : {
                        type          : 'duration',
                        weight        : 400,
                        label         : 'L{Lag}',
                        name          : 'lag',
                        allowNegative : true
                    }
                },

                bbar : {
                    defaults : {
                        localeClass : this
                    },
                    items : {
                        foo : {
                            type : 'widget',
                            cls  : 'b-label-filler'
                        },
                        /**
                         * Reference to the save button, if used
                         * @member {Core.widget.Button} saveButton
                         * @readonly
                         */
                        saveButton : {
                            color : 'b-green',
                            text  : 'L{Save}'
                        },
                        /**
                         * Reference to the delete button, if used
                         * @member {Core.widget.Button} deleteButton
                         * @readonly
                         */
                        deleteButton : {
                            color : 'b-gray',
                            text  : 'L{Delete}'
                        },
                        /**
                         * Reference to the cancel button, if used
                         * @member {Core.widget.Button} cancelButton
                         * @readonly
                         */
                        cancelButton : {
                            color : 'b-gray',
                            text  : 'L{Object.Cancel}'
                        }
                    }
                }
            }
        };
    }

    //endregion

    //region Init & destroy

    construct(client, config) {
        const me = this;

        client.dependencyEdit = me;

        super.construct(client, config);

        if (!client.features.dependencies) {
            throw new Error('Dependencies feature required when using DependencyEdit');
        }

        me.clientListenersDetacher = client.ion({
            [me.triggerEvent] : me.onActivateEditor,
            thisObj           : me
        });
    }

    doDestroy() {
        this.clientListenersDetacher();
        this.editor?.destroy();
        super.doDestroy();
    }

    //endregion

    //region Editing

    changeEditorConfig(config) {
        const
            me                         = this,
            { autoClose, cls, client } = me;

        return ObjectHelper.assign({
            owner        : client,
            align        : 'b-t',
            id           : `${client.id}-dependency-editor`, 
            autoShow     : false,
            anchor       : true,
            scrollAction : 'realign',
            clippedBy    : [client.timeAxisSubGridElement, client.bodyContainer],
            constrainTo  : globalThis,
            autoClose,
            cls
        }, config);
    }

    //endregion

    //region Save

    get isValid() {
        return Object.values(this.editor.widgetMap).every(field => {
            if (!field.name || field.hidden) {
                return true;
            }

            return field.isValid !== false;
        });
    }

    get values() {
        const values = {};

        this.editor.eachWidget(widget => {
            if (!widget.name || widget.hidden) return;

            values[widget.name] = widget.value;
        }, true);

        return values;
    }

    /**
     * Template method, intended to be overridden. Called before the dependency record has been updated.
     * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record
     *
     **/
    onBeforeSave(dependencyRecord) {}

    /**
     * Template method, intended to be overridden. Called after the dependency record has been updated.
     * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record
     *
     **/
    onAfterSave(dependencyRecord) {}

    /**
     * Updates record being edited with values from the editor
     * @private
     */
    updateRecord(dependencyRecord) {
        const { values } = this;

        // Engine does not understand { magnitude, unit } syntax
        if (values.lag) {
            values.lagUnit = values.lag.unit;
            values.lag = values.lag.magnitude;
        }

        // Type replaces fromSide/toSide, if they are used
        if ('type' in values) {
            dependencyRecord.fromSide != null && (values.fromSide = null);
            dependencyRecord.toSide != null && (values.toSide = null);
        }

        // Chronograph doesn't filter out undefined fields, it nullifies them instead
        // https://github.com/bryntum/chronograph/issues/11
        ObjectHelper.cleanupProperties(values, true);

        dependencyRecord.set(values);
    }

    //endregion

    //region Events

    onPopupKeyDown({ event }) {
        if (event.key === 'Enter' && this.saveAndCloseOnEnter && event.target.tagName.toLowerCase() === 'input') {
            // Need to prevent this key events from being fired on whatever receives focus after the editor is hidden
            event.preventDefault();

            this.onSaveClick();
        }
    }

    onSaveClick() {
        if (this.save()) {
            this.afterSave();
            this.editor.hide();
        }
    }

    async onDeleteClick() {
        if (await this.deleteDependency()) {
            this.afterDelete();
        }
        this.editor.hide();
    }

    onCancelClick() {
        this.afterCancel();
        this.editor.hide();
    }

    afterSave() {}
    afterDelete() {}
    afterCancel() {}

    //region Editing

    // Called from editDependency() to actually show the editor
    internalShowEditor(dependencyRecord) {
        const
            me         = this,
            { client } = me,
            editor     = me.getEditor(dependencyRecord);

        me.loadRecord(dependencyRecord);

        /**
         * Fires on the owning Scheduler when the editor for a dependency is available but before it is shown. Allows
         * manipulating fields before the widget is shown.
         * @event beforeDependencyEditShow
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler
         * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependencyEdit feature
         * @param {Scheduler.model.DependencyModel} dependencyRecord The record about to be shown in the editor.
         * @param {Core.widget.Popup} editor The editor popup
         */
        client.trigger('beforeDependencyEditShow', {
            dependencyEdit : me,
            dependencyRecord,
            editor
        });

        let showPoint = me.lastPointerDownCoordinate;

        if (!showPoint) {
            const center = Rectangle.from(client.element).center;

            showPoint = [center.x - editor.width / 2, center.y - editor.height / 2];
        }

        return editor.showBy(showPoint);
    }

    /**
     * Opens a popup to edit the passed dependency.
     * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency to edit
     * @return {Promise} A Promise that yields `true` after the editor is shown
     * or `false` if some application logic vetoed the editing (see `beforeDependencyEdit` in the docs).
     */
    async editDependency(dependencyRecord) {
        const
            me         = this,
            { client } = me;

        if (client.readOnly || dependencyRecord.readOnly ||
            /**
             * Fires on the owning Scheduler before an dependency is displayed in the editor.
             * This may be listened for to allow an application to take over dependency editing duties. Return `false` to
             * stop the default editing UI from being shown or a `Promise` yielding `true` or `false` for async vetoing.
             * @event beforeDependencyEdit
             * @on-owner
             * @param {Scheduler.view.Scheduler} source The scheduler
             * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependencyEdit feature
             * @param {Scheduler.model.DependencyModel} dependencyRecord The record about to be shown in the editor.
             * @preventable
             * @async
             */
            await client.trigger('beforeDependencyEdit', { dependencyEdit : me, dependencyRecord }) === false
        ) {
            return false;
        }

        // wait till the editor is shown
        await this.internalShowEditor(dependencyRecord);

        return true;
    }

    //endregion

    //region Save

    /**
     * Gets an editor instance. Creates on first call, reuses on consecutive
     * @internal
     * @returns {Scheduler.view.DependencyEditor} Editor popup
     */
    getEditor() {
        const me = this;

        let { editor } = me;

        if (editor) {
            return editor;
        }

        editor = me.editor = DependencyEditor.new({
            dependencyEditFeature : me,
            autoShow              : false,
            anchor                : true,
            scrollAction          : 'realign',
            constrainTo           : globalThis,
            autoClose             : me.autoClose,
            cls                   : me.cls,
            rootElement           : me.client.rootElement,
            internalListeners     : {
                keydown : me.onPopupKeyDown,
                thisObj : me
            }
        }, me.editorConfig);

        if (editor.items.length === 0) {
            console.warn('Editor configured without any `items`');
        }

        // assign widget refs
        editor.eachWidget(widget => {
            const ref = widget.ref || widget.id;
            // don't overwrite if already defined
            if (ref && !me[ref]) {
                me[ref] = widget;
            }
        });

        me.saveButton?.ion({ click : 'onSaveClick', thisObj : me });
        me.deleteButton?.ion({ click : 'onDeleteClick', thisObj : me });
        me.cancelButton?.ion({ click : 'onCancelClick', thisObj : me });

        return me.editor;
    }

    //endregion

    //region Delete

    /**
     * Sets fields values from record being edited
     * @private
     */
    loadRecord(dependency) {
        const me = this;

        me.fromNameField.value = dependency.fromEvent.name;
        me.toNameField.value = dependency.toEvent.name;

        if (me.lagField) {
            me.lagField.value = new Duration(dependency.lag, dependency.lagUnit);
        }

        me.editor.record = me.dependencyRecord = dependency;
    }

    //endregion

    //region Stores

    /**
     * Saves the changes (applies them to record if valid, if invalid editor stays open)
     * @private
     * @fires beforeDependencySave
     * @fires beforeDependencyAdd
     * @fires afterDependencySave
     * @returns {*}
     */
    async save() {
        const
            me                           = this,
            { client, dependencyRecord } = me;

        if (!dependencyRecord || !me.isValid) {
            return;
        }

        const { dependencyStore, values } = me;

        /**
         * Fires on the owning Scheduler before a dependency is saved
         * @event beforeDependencySave
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler instance
         * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be saved
         * @param {Object} values The new values
         * @preventable
         */
        if (client.trigger('beforeDependencySave', {
            dependencyRecord,
            values
        }) !== false) {
            me.onBeforeSave(dependencyRecord);

            me.updateRecord(dependencyRecord);

            // Check if this is a new record
            if (dependencyStore && !dependencyRecord.stores.length) {
                /**
                 * Fires on the owning Scheduler before a dependency is added
                 * @event beforeDependencyAdd
                 * @on-owner
                 * @param {Scheduler.view.Scheduler} source The scheduler
                 * @param {Scheduler.feature.DependencyEdit} dependencyEdit The dependency edit feature
                 * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be added
                 * @preventable
                 */
                if (client.trigger('beforeDependencyAdd', { dependencyRecord, dependencyEdit : me }) === false) {
                    return;
                }

                dependencyStore.add(dependencyRecord);
            }

            await client.project?.commitAsync();

            /**
             * Fires on the owning Scheduler after a dependency is successfully saved
             * @event afterDependencySave
             * @on-owner
             * @param {Scheduler.view.Scheduler} source The scheduler instance
             * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency about to be saved
             */
            client.trigger('afterDependencySave', { dependencyRecord });

            me.onAfterSave(dependencyRecord);
        }

        return dependencyRecord;
    }

    /**
     * Delete dependency being edited
     * @private
     * @fires beforeDependencyDelete
     */
    async deleteDependency() {
        const { client, editor, dependencyRecord } = this;

        /**
         * Fires on the owning Scheduler before a dependency is deleted
         * @event beforeDependencyDelete
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler instance
         * @param {Scheduler.model.DependencyModel} dependencyRecord The dependency record about to be deleted
         * @preventable
         */
        if (client.trigger('beforeDependencyDelete', { dependencyRecord }) !== false) {
            if (editor.containsFocus) {
                editor.revertFocus();
            }

            client.dependencyStore.remove(dependencyRecord);
            await client.project?.commitAsync();

            return true;
        }

        return false;
    }

    get dependencyStore() {
        return this.client.dependencyStore;
    }
    //endregion

    //region Events

    onActivateEditor({ dependency, event }) {
        if (!this.disabled) {
            this.lastPointerDownCoordinate = [event.clientX, event.clientY];
            this.editDependency(dependency);
        }
    }

    //endregion

}

GridFeatureManager.registerFeature(DependencyEdit, false);
