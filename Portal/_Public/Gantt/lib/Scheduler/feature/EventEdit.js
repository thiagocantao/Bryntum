import EditBase from './base/EditBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import '../view/EventEditor.js';
import Delayable from '../../Core/mixin/Delayable.js';
import RecurringEventEdit from './mixin/RecurringEventEdit.js';
import '../../Core/widget/TextField.js';
import '../../Scheduler/widget/ResourceCombo.js';
import TimeSpan from '../../Scheduler/model/TimeSpan.js';
import '../../Core/widget/DateField.js';
import '../../Core/widget/TimeField.js';
import '../../Core/widget/Button.js';
import '../widget/EventColorField.js';
import Widget from '../../Core/widget/Widget.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import AsyncHelper from '../../Core/helper/AsyncHelper.js';
import TaskEditStm from './mixin/TaskEditStm.js';

/**
 * @module Scheduler/feature/EventEdit
 */

const punctuation = /[^\w\d]/g;

/**
 * Feature that displays a popup containing widgets for editing event data.
 *
 * {@inlineexample Scheduler/feature/EventEdit.js}
 *
 * To customize its contents you can:
 *
 * * Reconfigure built in widgets by providing override configs in the {@link Scheduler.feature.base.EditBase#config-items} config.
 * * Change the date format of the date & time fields: {@link Scheduler.feature.base.EditBase#config-dateFormat} and {@link Scheduler.feature.base.EditBase#config-timeFormat }
 * * Configure provided widgets in the editor and add your own in the {@link Scheduler.feature.base.EditBase#config-items} config.
 * * Remove fields related to recurring events configuration (such as `recurrenceCombo`) by setting {@link Scheduler.feature.mixin.RecurringEventEdit#config-showRecurringUI} config to `false`.
 * * Advanced: Reconfigure the whole editor widget using {@link #config-editorConfig}
 *
 * ## Built in widgets
 *
 * The built in widgets are:
 *
 * | Widget ref             | Type                                                     | Weight | Description                                                    |
 * |------------------------|----------------------------------------------------------|--------|----------------------------------------------------------------|
 * | `nameField`            | {@link Core.widget.TextField}                            | 100    | Edit name                                                      |
 * | `resourceField`        | {@link Scheduler.widget.ResourceCombo}                   | 200    | Pick resource(s)                                               |
 * | `startDateField`       | {@link Core.widget.DateField}                            | 300    | Edit startDate (date part)                                     |
 * | `startTimeField`       | {@link Core.widget.TimeField}                            | 400    | Edit startDate (time part)                                     |
 * | `endDateField`         | {@link Core.widget.DateField}                            | 500    | Edit endDate (date part)                                       |
 * | `endTimeField`         | {@link Core.widget.TimeField}                            | 600    | Edit endDate (time part)                                       |
 * | `recurrenceCombo`      | {@link Scheduler.view.recurrence.field.RecurrenceCombo}  | 700    | Select recurrence rule (only visible if recurrence is used)    |
 * | `editRecurrenceButton` | {@link Scheduler.view.recurrence.RecurrenceLegendButton} | 800    | Edit the recurrence rule  (only visible if recurrence is used) |
 * | `colorField` ¹         | {@link Scheduler.widget.EventColorField}                 | 700    | Choose background color for the event bar                      |
 *
 * **¹** Set the {@link Scheduler.view.SchedulerBase#config-showEventColorPickers} config to `true` to enable this field
 *
 * The built in buttons are:
 *
 * | Widget ref             | Type                                                                     | Weight | Description                                                    |
 * |------------------------|--------------------------------------------------------------------------|--------|----------------------------------------------------------------|
 * | `saveButton`           | {@link Core.widget.Button}                                               | 100    | Save event button on the bbar                                  |
 * | `deleteButton`         | {@link Core.widget.Button}                                               | 200    | Delete event button on the bbar                                |
 * | `cancelButton`         | {@link Core.widget.Button}                                               | 300    | Cancel event editing button on the bbar                        |
 *
 * ## Removing a built in item
 *
 * To remove a built in widget, specify its `ref` as `null` in the `items` config:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventEdit : {
 *             items : {
 *                 // Remove the start time field
 *                 startTimeField : null
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * Bottom buttons may be hidden using `bbar` config passed to `editorConfig`:
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventEdit : {
 *             editorConfig : {
 *                 bbar : {
 *                     items : {
 *                         deleteButton : null
 *                     }
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * To remove fields related to recurring events configuration (such as `recurrenceCombo`), set {@link Scheduler.feature.mixin.RecurringEventEdit#config-showRecurringUI} config to `false`.
 *
 * ## Customizing a built in widget
 *
 * To customize a built in widget, use its `ref` as the key in the `items` config and specify the configs you want
 * to change (they will merge with the widgets default configs):
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventEdit : {
 *             items : {
 *                 // ref for an existing field
 *                 nameField : {
 *                     // Change its label
 *                     label : 'Description'
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * ## Adding custom widgets
 *
 * To add a custom widget, add an entry to the `items` config. The `name` property links the input field to a field in
 * the loaded event record:
 *
 * ```javascript
 * const scheduler = new Scheduler({
 *     features : {
 *         eventEdit : {
 *             items : {
 *                 // Key to use as fields ref (for easier retrieval later)
 *                 color : {
 *                     type  : 'combo',
 *                     label : 'Color',
 *                     items : ['red', 'green', 'blue'],
 *                     // name will be used to link to a field in the event record when loading and saving in the editor
 *                     name  : 'eventColor'
 *                 }
 *             }
 *         }
 *     }
 * })
 * ```
 *
 * For more info on customizing the event editor, please see "Customize event editor" guide.
 *
 * This feature is **enabled** by default
 *
 * @mixes Scheduler/feature/mixin/RecurringEventEdit
 * @extends Scheduler/feature/base/EditBase
 * @demo Scheduler/eventeditor
 * @classtype eventEdit
 * @feature
 */
export default class EventEdit extends EditBase.mixin(TaskEditStm, RecurringEventEdit, Delayable) {
    //region Config

    static get $name() {
        return 'EventEdit';
    }

    static get configurable() {
        return {
            /**
             * The event that shall trigger showing the editor. Defaults to `eventdblclick`, set to `''` or null to
             * disable editing of existing events.
             * @config {String}
             * @default
             * @category Editor
             */
            triggerEvent : 'eventdblclick',

            /**
             * The data field in the model that defines the eventType.
             * Applied as class (b-eventtype-xx) to the editors element, to allow showing/hiding fields depending on
             * eventType. Dynamic toggling of fields in the editor is activated by adding an `eventTypeField` field to
             * your widget:
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *    features : {
             *       eventEdit : {
             *           items : {
             *               eventTypeField : {
             *                  type  : 'combo',
             *                  name  : 'eventType',
             *                  label : 'Type',
             *                  items : ['Appointment', 'Internal', 'Meeting']
             *               }
             *           }
             *        }
             *     }
             * });
             * ```
             * Note, your event model class also must declare this field:
             * ```javascript
             *  class MyEvent extends EventModel {
             *      static get fields() {
             *          return [
             *              { name : 'eventType' }
             *          ];
             *      }
             *  }
             * ```
             * @config {String}
             * @default
             * @category Editor
             */
            typeField : 'eventType',

            /**
             * The current {@link Scheduler.model.EventModel} record, which is being edited by the event editor.
             * @property {Scheduler.model.EventModel}
             * @readonly
             */
            eventRecord : null,

            /**
             * Specify `true` to put the editor in read only mode.
             * @config {Boolean}
             * @default false
             */
            readOnly : null,

            /**
             * The configuration for the internal editor widget. With this config you can control the *type*
             * of editor (defaults to `Popup`) and which widgets to show,
             * change the items in the `bbar`, or change whether the popup should be modal etc.
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         eventEdit  : {
             *             editorConfig : {
             *                 modal  : true,
             *                 cls    : 'my-editor' // A CSS class,
             *                 items  : {
             *                     owner : {
             *                         weight : -100, // Will sort above system-supplied fields which are weight 100 to 800
             *                         type   : 'usercombo',
             *                         name   : 'owner',
             *                         label  : 'Owner'
             *                     },
             *                     agreement : {
             *                         weight : 1000, // Will sort below system-supplied fields which are weight 100 to 800
             *                         type   : 'checkbox',
             *                         name   : 'agreement',
             *                         label  : 'Agree to terms'
             *                     },
             *                     resourceField : {
             *                         // Apply a special filter to limit the Combo's access
             *                         // to resources.
             *                         store  {
             *                             filters : [{
             *                                 filterBy(resource) {
             *                                     return shouldShowResource(record);
             *                                 }
             *                             }]
             *                         }
             *                     }
             *                 },
             *                 bbar : {
             *                     items : {
             *                         deleteButton : {
             *                             hidden : true
             *                         }
             *                     }
             *                 }
             *             }
             *         }
             *     }
             * });
             * ```
             *
             * Or to use your own custom editor:
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *     features : {
             *         eventEdit  : {
             *             editorConfig : {
             *                 type : 'myCustomEditorType'
             *             }
             *         }
             *     }
             * });
             * ```
             * @config {Object}
             * @category Editor
             */
            editorConfig : {
                type        : 'eventeditor',
                title       : 'L{EventEdit.Edit event}',
                closable    : true,
                localeClass : this,

                defaults : {
                    localeClass : this
                },
                items : {
                    /**
                     * Reference to the name field, if used
                     * @member {Core.widget.TextField} nameField
                     * @readonly
                     */
                    nameField : {
                        type      : 'text',
                        label     : 'L{Name}',
                        clearable : true,
                        name      : 'name',
                        weight    : 100,
                        required  : true
                    },
                    /**
                     * Reference to the resource field, if used
                     * @member {Core.widget.Combo} resourceField
                     * @readonly
                     */
                    resourceField : {
                        type                    : 'resourcecombo',
                        label                   : 'L{Resource}',
                        name                    : 'resource',
                        editable                : true,
                        valueField              : 'id',
                        displayField            : 'name',
                        highlightExternalChange : false,
                        destroyStore            : true,
                        weight                  : 200
                    },
                    /**
                     * Reference to the start date field, if used
                     * @member {Core.widget.DateField} startDateField
                     * @readonly
                     */
                    startDateField : {
                        type             : 'date',
                        clearable        : false,
                        required         : true,
                        label            : 'L{Start}',
                        name             : 'startDate',
                        validateDateOnly : true,
                        weight           : 300
                    },
                    /**
                     * Reference to the start time field, if used
                     * @member {Core.widget.TimeField} startTimeField
                     * @readonly
                     */
                    startTimeField : {
                        type      : 'time',
                        clearable : false,
                        required  : true,
                        name      : 'startDate',
                        cls       : 'b-match-label',
                        weight    : 400
                    },
                    /**
                     * Reference to the end date field, if used
                     * @member {Core.widget.DateField} endDateField
                     * @readonly
                     */
                    endDateField : {
                        type             : 'date',
                        clearable        : false,
                        required         : true,
                        label            : 'L{End}',
                        name             : 'endDate',
                        validateDateOnly : true,
                        weight           : 500
                    },
                    /**
                     * Reference to the end time field, if used
                     * @member {Core.widget.TimeField} endTimeField
                     * @readonly
                     */
                    endTimeField : {
                        type      : 'time',
                        clearable : false,
                        required  : true,
                        name      : 'endDate',
                        cls       : 'b-match-label',
                        weight    : 600
                    },

                    colorField : {
                        label  : 'L{SchedulerBase.color}',
                        type   : 'eventColorField',
                        name   : 'eventColor',
                        weight : 700
                    }
                },

                bbar : {
                    // When readOnly, child buttons are hidden
                    hideWhenEmpty : true,

                    defaults : {
                        localeClass : this
                    },
                    items : {
                        /**
                         * Reference to the save button, if used
                         * @member {Core.widget.Button} saveButton
                         * @readonly
                         */
                        saveButton : {
                            color  : 'b-blue',
                            cls    : 'b-raised',
                            text   : 'L{Save}',
                            weight : 100
                        },
                        /**
                         * Reference to the delete button, if used
                         * @member {Core.widget.Button} deleteButton
                         * @readonly
                         */
                        deleteButton : {
                            text   : 'L{Delete}',
                            weight : 200
                        },
                        /**
                         * Reference to the cancel button, if used
                         * @member {Core.widget.Button} cancelButton
                         * @readonly
                         */
                        cancelButton : {
                            text   : 'L{Object.Cancel}',
                            weight : 300
                        }
                    }
                }
            },

            targetEventElement : null
        };
    }

    static get pluginConfig() {
        return {
            chain : [
                'populateEventMenu',
                'onEventEnterKey',
                'editEvent'
            ]
        };
    }

    //endregion

    //region Init & destroy

    construct(scheduler, config) {
        // Default to the scheduler's state, but configs may override
        this.readOnly = scheduler.readOnly;

        super.construct(scheduler, config);

        scheduler.ion({
            projectChange : 'onChangeProject',
            readOnly      : 'onClientReadOnlyToggle',
            thisObj       : this
        });
    }

    get scheduler() {
        return this.client;
    }

    get project() {
        return this.client.project;
    }
    //endregion

    //region Editing

    /**
     * Get/set readonly state
     * @property {Boolean}
     */
    get readOnly() {
        return this._editor ? this.editor.readOnly : this._readOnly;
    }

    updateReadOnly(readOnly) {
        super.updateReadOnly(readOnly);

        if (this._editor) {
            this.editor.readOnly = readOnly;
        }
    }

    onClientReadOnlyToggle({ readOnly }) {
        this.readOnly = readOnly;
    }

    /**
     * Returns the editor widget representing this feature
     * @member {Core.widget.Popup}
     */
    get editor() {
        const
            me              = this,
            editorListeners = {
                beforehide : 'resetEditingContext',
                beforeshow : 'onBeforeEditorShow',
                keydown    : 'onPopupKeyDown',
                thisObj    : me
            };

        let { _editor : editor } = me;

        if (editor) {
            return editor;
        }

        editor = me._editor = Widget.create(me.getEditorConfig());

        const {
            startDateField,
            startTimeField,
            endDateField,
            endTimeField
        } = editor.widgetMap;

        // If the date field doesn't exist, the time field must encapsulate the
        // date component of the start/end points and must lay out right.
        if (!startDateField && startTimeField) {
            startTimeField.keepDate = true;
            startTimeField.label = me.L('Start');
            startTimeField.flex = '1 0 100%';
        }
        if (!endDateField && endTimeField) {
            endTimeField.keepDate = true;
            endTimeField.label = me.L('End');
            endTimeField.flex = '1 0 100%';
        }

        // If the default Popup has been reconfigured to be static, add it as a child of our client.
        if (!editor.floating && !editor.positioned) {
            // If not configured with an appendTo, we add it as a child of our client.
            if (!editor.element.parentNode) {
                me.client.add(editor);
            }

            delete editorListeners.beforehide;
            delete editorListeners.beforeshow;

            editorListeners.beforeToggleReveal = 'onBeforeEditorToggleReveal';
        }

        // Must set *after* construction, otherwise it becomes the default state
        // to reset readOnly back to. Must use direct property access because
        // getter consults state of editor.
        editor.readOnly = me._readOnly;

        if (editor.items.length === 0) {
            console.warn('Event Editor configured without any `items`');
        }

        // add listeners programmatically so users cannot override them accidentally
        editor.ion(editorListeners);

        /**
         * Fired before the editor will load the event record data into its input fields. This is useful if you
         * want to modify the fields before data is loaded (e.g. set some input field to be readonly)
         * @on-owner
         * @event eventEditBeforeSetRecord
         * @param {Core.widget.Container} source The editor widget
         * @param {Scheduler.model.EventModel} record The record
         */
        me.scheduler.relayEvents(editor, ['beforeSetRecord'], 'eventEdit');

        // assign widget variables, using widget name: startDate -> me.startDateField
        // widgets with id set use that instead, id -> me.idField
        Object.values(editor.widgetMap).forEach(widget => {
            const ref = widget.ref || widget.id;
            // don't overwrite if already defined
            if (ref && !me[ref]) {
                me[ref] = widget;

                switch (widget.name) {
                    case 'startDate':
                    case 'endDate':
                        widget.ion({ change : 'onDatesChange', thisObj : me });
                        break;
                }
            }
        });

        // launch onEditorConstructed hook if provided
        me.onEditorConstructed?.(editor);

        me.eventTypeField?.ion({ change : 'onEventTypeChange', thisObj : me });

        me.saveButton?.ion({ click : 'onSaveClick', thisObj : me });
        me.deleteButton?.ion({ click : 'onDeleteClick', thisObj : me });
        me.cancelButton?.ion({ click : 'onCancelClick', thisObj : me });

        return editor;
    }

    getEditorConfig() {
        const
            me                 = this,
            { cls, scheduler } = me,
            result             = ObjectHelper.assign({
                owner            : scheduler,
                eventEditFeature : me,
                weekStartDay     : me.weekStartDay,
                align            : 'b-t',
                id               : `${scheduler.id}-event-editor`,
                autoShow         : false,
                anchor           : true,
                scrollAction     : 'realign',
                constrainTo      : globalThis,
                cls
            }, me.editorConfig);

        // User configuration may have included a render target which means the editor
        // will not be floating.
        if (Widget.prototype.getRenderContext(result)[0]) {
            result.floating = false;
        }

        // If the default Popup has been reconfigured to be static, ensure it starts
        // life as a visible but collapsed panel.
        if (result.floating === false && !result.positioned) {
            result.collapsible = {
                type           : 'overlay',
                direction      : 'right',
                autoClose      : false,
                tool           : null,
                recollapseTool : null
            };
            result.collapsed = true;
            result.hidden = result.anchor = false;
            result.hide = function() {
                this.collapsible.toggleReveal(false);
            };
        }

        if (!scheduler.showEventColorPickers && result.items.colorField) {
            result.items.colorField.hidden = true;
        }

        // Layout-affecting props must be available early so that appendTo ends up with
        // correct layout.
        result.onElementCreated = me.updateCSSVars.bind(this);

        return result;
    }

    updateCSSVars({ element }) {
        // must result in longest format, ie 2 digits for date and all time parts.
        const
            time               = new Date(2000, 12, 31, 23, 55, 55),
            dateLength         = DateHelper.format(time, this.dateFormat).replace(punctuation, '').length,
            timeLength         = DateHelper.format(time, this.timeFormat).replace(punctuation, '').length,
            dateTimeLength     = dateLength + timeLength;

        element.style.setProperty('--date-time-length', `${dateTimeLength}em`);
        element.style.setProperty('--date-width-difference', `${(dateLength - timeLength) / 2}em`);
    }

    // Called from editEvent() to actually show the editor
    async internalShowEditor(eventRecord, resourceRecord, align = null) {
        const
            me            = this,
            { scheduler } = me,
            // Align to the element (b-sch-event) and not the wrapper
            eventElement = align?.target?.nodeType === Element.ELEMENT_NODE
                ? align.target
                : scheduler.getElementFromEventRecord(eventRecord, resourceRecord),
            isPartOfStore = eventRecord.isPartOfStore(scheduler.eventStore);

        align = align ?? {
            // Align to the element (b-sch-event) and not the wrapper
            target : eventElement,
            anchor : true
        };

        // Event not in current TimeAxis - cannot be edited without extending the TimeAxis.
        // If there's no event element and the eventRecord is not in the store, we still
        // edit centered on the Scheduler - we're adding a new event
        if (align.target || (!isPartOfStore || eventRecord.resources.length === 0) || eventRecord.isCreating) {
            // need to add this css class as early as possible to prevent
            // the event tooltip from appearing
            scheduler.element.classList.add('b-eventeditor-editing');

            me.resourceRecord = resourceRecord;

            const { editor } = me;

            me.editingContext = {
                eventRecord,
                resourceRecord,
                eventElement,
                editor,
                isPartOfStore
            };

            super.internalShowEditor?.(eventRecord, resourceRecord, align);

            if (me.typeField) {
                me.toggleEventType(eventRecord.getValue(me.typeField));
            }

            me.loadRecord(eventRecord, resourceRecord);

            // If it's a static child of the client which is collapsed, expand it.
            // Floating components focusOnShow by default, this will need to be focused.
            if (editor.collapsed) {
                // The *initial* reveal does not animate unless the toggleReveal call is delayed.
                await AsyncHelper.sleep(100);
                await editor.collapsible.toggleReveal(true);
                editor.focus();
            }
            // Honour alignment settings "anchor" and "centered" which may be injected from editorConfig.
            else if (editor.centered || !editor.anchor || !editor.floating) {
                editor.show();
            }
            else if (eventElement) {
                me.targetEventElement = eventElement;
                editor.showBy(align);
            }
            // We are adding an unrendered event. Display the editor centered
            else {
                editor.show();

                // Must be done after show because show always reverts to its configured centered setting.
                editor.updateCentered(true);
            }

            // Adjust time field step increment based on timeAxis resolution
            const timeResolution = scheduler.timeAxisViewModel.timeResolution;

            if (timeResolution.unit === 'hour' || timeResolution.unit === 'minute') {
                const step = `${timeResolution.increment}${timeResolution.unit}`;
                if (me.startTimeField) {
                    me.startTimeField.step = step;
                }
                if (me.endTimeField) {
                    me.endTimeField.step = step;
                }
            }

            // Might end up here with the old listener still around in monkey test for stress demo in turbo mode.
            // Some action happening during edit, but cannot track down what is going on
            me.detachListeners('changesWhileEditing');

            scheduler.eventStore.ion({
                change  : me.onChangeWhileEditing,
                refresh : me.onChangeWhileEditing,
                thisObj : me,
                name    : 'changesWhileEditing'
            });
        }
    }

    onChangeWhileEditing() {
        const me = this;
        // If event was removed, cancel editing
        // however, there's one valid case when even can be removed during save finalization - that is when
        // all its assignments has been removed - in such case ignore the removal and do not call the `onCancelClick`
        // because that will reject the STM transaction and revert all changes
        if (!me.isFinalizingEventSave && me.isEditing && me.editingContext.isPartOfStore && !me.eventRecord.isPartOfStore(me.scheduler.eventStore)) {
            me.onCancelClick();
        }
    }

    // Fired in a listener so that it's after the auto-called onBeforeShow listeners so that
    // subscribers to the beforeEventEditShow are called at exactly the correct lifecycle point.
    onBeforeEditorShow() {
        super.onBeforeEditorShow(...arguments);

        /**
         * Fires on the owning Scheduler when the editor for an event is available but before it is populated with
         * data and shown. Allows manipulating fields etc.
         * @event beforeEventEditShow
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler
         * @param {Scheduler.feature.EventEdit} eventEdit The eventEdit feature
         * @param {Scheduler.model.EventModel} eventRecord The record about to be shown in the event editor.
         * @param {Scheduler.model.ResourceModel} resourceRecord The Resource record for the event. If the event
         * is being created, it will not contain a resource, so this parameter specifies the resource the
         * event is being created for.
         * @param {HTMLElement} eventElement The element which represents the event in the scheduler display.
         * @param {Core.widget.Popup} editor The editor
         */
        this.scheduler.trigger('beforeEventEditShow', {
            eventEdit : this,
            ...this.editingContext
        });
    }

    updateTargetEventElement(targetEventElement, oldTargetEventElement) {
        targetEventElement?.classList.add('b-editing');
        oldTargetEventElement?.classList.remove('b-editing');
    }

    /**
     * Opens an editor for the passed event. This function is exposed on Scheduler and can be called as
     * `scheduler.editEvent()`.
     * @param {Scheduler.model.EventModel} eventRecord Event to edit
     * @param {Scheduler.model.ResourceModel} [resourceRecord] The Resource record for the event.
     * This parameter is needed if the event is newly created for a resource and has not been assigned, or when using
     * multi assignment.
     * @param {HTMLElement} [element] Element to anchor editor to (defaults to events element)
     * @on-owner
     */
    editEvent(eventRecord, resourceRecord, element = null, stmCapture = null) {
        const
            me = this,
            { client } = me,
            { simpleEventEdit } = client.features;

        if (me.isEditing) {
            // old editing flow already running, clean it up
            me.resetEditingContext();
        }

        // If simple edit feature is active, use it when a new event is created
        if (me.disabled || eventRecord.readOnly || (eventRecord.isCreating && simpleEventEdit?.enabled)) {
            return;
        }

        /**
         * Fires on the owning Scheduler before an event is displayed in an editor.
         * This may be listened for to allow an application to take over event editing duties. Returning `false`
         * stops the default editing UI from being shown.
         * @event beforeEventEdit
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler
         * @param {Scheduler.feature.EventEdit} eventEdit The eventEdit feature
         * @param {Scheduler.model.EventModel} eventRecord The record about to be shown in the event editor.
         * @param {Scheduler.model.ResourceModel} resourceRecord The Resource record for the event. If the event
         * is being created, it will not contain a resource, so this parameter specifies the resource the
         * event is being created for.
         * @param {HTMLElement} eventElement The element which represents the event in the scheduler display.
         * @preventable
         */
        if (client.trigger('beforeEventEdit', {
            eventEdit    : me,
            eventRecord,
            resourceRecord,
            eventElement : client.getElementFromEventRecord?.(eventRecord, resourceRecord) || element
        }) === false) {
            client.element.classList.remove('b-eventeditor-editing');
            return false;
        }

        if (stmCapture) {
            me.applyStmCapture(stmCapture);
            me.hasStmCapture = true;

            // indicate that editor has been opened, and is now managing the "stm capture"
            stmCapture.transferred = true;
        }
        // it is set to `false` by calendar, to ignore the STM mechanism
        else if (stmCapture !== false && !client.isCalendar && !me.hasStmCapture) {
            me.captureStm(true);
        }

        return me.doEditEvent(...arguments).then(result => {
            if (!me.isDestroying) {
                // The Promise being async allows a mouseover to trigger the event tip
                // unless we add the editing class immediately (But only if we actually began editing).
                if (!me.isEditing && !client.isCalendar && !me.rejectingStmTransaction) {
                    // probably a custom event editor was used or editing was vetoed for some other reason
                    if (result !== false && me.hasStmCapture) {
                        // Skip stm rejection if built-in editor is disabled in beforeEventEdit (using of custom event editor)
                        return me.freeStm(false);
                    }
                    else {
                        return me.freeStm();
                    }
                }
            }
        });
    }

    /**
     * Returns true if the editor is currently active
     * @readonly
     * @property {Boolean}
     */
    get isEditing() {
        const { _editor } = this;

        return Boolean(
            // Editor is not visible if it is collapsed and not expanded
            _editor?.isVisible && !(_editor.collapsed && !_editor.revealed)
        );
    }

    // editEvent is the single entry point in the base class.
    // Subclass implementations of the action may differ, so are implemented in doEditEvent
    async doEditEvent(eventRecord, resourceRecord, element = null) {
        const
            me            = this,
            { scheduler } = me,
            isNewRecord   = eventRecord.isCreating;

        if (!resourceRecord) {
            // Need to handle resourceId for edge case when creating an event with resourceId and editing it before
            // adding it to the EventStore
            resourceRecord = eventRecord.resource || me.resourceStore.getById(eventRecord.resourceId);
        }

        if (isNewRecord) {
            // Ensure temporal data fields are ready when the editor is shown
            TimeSpan.prototype.normalize.call(eventRecord);
        }

        // If element is specified (call triggered by EventDragCreate)
        // Then we can align to that, and no scrolling is necessary.
        // If we are simply being asked to edit a new event which is not
        // yet added, the editor is centered, and no scroll is necessary
        if (element || isNewRecord || eventRecord.resources.length === 0) {
            return me.internalShowEditor(eventRecord, resourceRecord, element ? {
                target : element
            } : null);
        }
        else {
            // Ensure event is in view before showing the editor.
            // Note that we first need to extend the time axis to include
            // currently out of range events.
            return scheduler.scrollResourceEventIntoView(resourceRecord, eventRecord, {
                animate        : true,
                edgeOffset     : 0,
                extendTimeAxis : false
            }).then(() => me.internalShowEditor(eventRecord, resourceRecord), () => scheduler.element.classList.remove('b-eventeditor-editing'));
        }
    }

    /**
     * Sets fields values from record being edited
     * @private
     */
    loadRecord(eventRecord, resourceRecord) {
        this.loadingRecord = true;

        this.internalLoadRecord(eventRecord, resourceRecord);

        this.loadingRecord = false;
    }

    get eventRecord() {
        return this._editor?.record;
    }

    internalLoadRecord(eventRecord, resourceRecord) {
        const
            me                        = this,
            { eventStore }            = me.client,
            { editor, resourceField } = me;

        me.resourceRecord = resourceRecord;

        // Update chained store early, to have records in place when setting value below (avoids adding the resource to
        // empty combo store, https://github.com/bryntum/support/issues/5378). It is not done automatically for
        // grouping/trees or when project is replaced
        if (resourceField && resourceField.store?.masterStore !== me.resourceStore) {
            resourceField.store = editor.chainResourceStore();
        }

        editor.record = eventRecord;

        if (resourceField) {
            const resources = eventStore.assignmentStore.getResourcesForEvent(eventRecord);

            // Flag on parent Container to indicate that initially blank fields are valid
            editor.assigningValues = true;

            // If this is an unassigned event, select the resource we've been provided
            if (!eventRecord.isOccurrence && !eventStore.storage.includes(eventRecord, true) && resourceRecord) {
                me.resourceField.value = resourceRecord.getValue(me.resourceField.valueField);
            }
            else if (me.assignmentStore) {
                me.resourceField.value = resources.map((resource) => resource.getValue(me.resourceField.valueField));
            }
            editor.assigningValues = false;
        }

        super.internalLoadRecord(eventRecord, resourceRecord);
    }

    toggleEventType(eventType) {
        // expose eventType in dataset, for querying and styling
        this.editor.element.dataset.eventType = eventType || '';

        this.editor.eachWidget(widget => { // need {}'s here so we don't return false and end iteration
            widget.dataset?.eventType && (widget.hidden = widget.dataset.eventType !== eventType);
        });
    }

    //endregion

    //region Save

    async finalizeEventSave(eventRecord, resourceRecords, resolve, reject) {
        const
            me = this,
            {
                scheduler,
                assignmentStore
            }  = me;

        const aborted = false;

        // Prevent multiple commits from this flow
        assignmentStore.suspendAutoCommit();

        // Avoid multiple redraws, from event changes + assignment changes
        scheduler.suspendRefresh();

        me.onBeforeSave(eventRecord);

        eventRecord.beginBatch();
        me.updateRecord(eventRecord);
        eventRecord.endBatch();

        if (!eventRecord.isOccurrence) {
            if (me.resourceField) {
                assignmentStore.assignEventToResource(eventRecord, resourceRecords, null, true);
            }
        }
        // An occurrence event record may have changed only resources value. In that case we'll never get into afterChange() method that
        // apply changed data and make an event "real", because resources is not a field and a record won't be marked as dirty.
        // We used temporary field to save updated resources list and get into afterChange() method.
        else if (resourceRecords) {
            eventRecord.set('resourceRecords', resourceRecords);
        }

        // If it was a provisional event, passed in here from drag-create or dblclick or contextmenu,
        // it's now it's no longer a provisional event and will not be removed in resetEditingContext
        // Also, when promoted to be permanent, auto syncing will kick in if configured.
        eventRecord.isCreating = false;

        if (!aborted) {
            await scheduler.project.commitAsync();
        }

        assignmentStore.resumeAutoCommit();

        // Redraw once
        scheduler.resumeRefresh(true);

        if (!aborted) {
            /**
             * Fires on the owning Scheduler after an event is successfully saved
             * @event afterEventSave
             * @on-owner
             * @param {Scheduler.view.Scheduler} source The scheduler instance
             * @param {Scheduler.model.EventModel} eventRecord The record about to be saved
             */
            scheduler.trigger('afterEventSave', { eventRecord });
            me.onAfterSave(eventRecord);
        }
        resolve(aborted ? false : eventRecord);
    }

    /**
     * Saves the changes (applies them to record if valid, if invalid editor stays open)
     * @private
     * @fires beforeEventSave
     * @fires beforeEventAdd
     * @fires afterEventSave
     * @async
     */
    save() {
        return new Promise((resolve, reject) => {
            const
                me                         = this,
                { scheduler, eventRecord } = me;

            if (!eventRecord || !me.editor.isValid) {
                resolve(false);
                return;
            }

            const
                { eventStore, values } = me,
                resourceRecords        = me.resourceField?.records || (me.resourceRecord ? [me.resourceRecord] : []);

            // Check for potential overlap scenarios before saving

            if (!me.scheduler.allowOverlap && eventStore) {
                let { startDate, endDate } = values;

                // Should support using a duration field instead of the end date field
                if (!endDate) {
                    if ('duration' in values) {
                        endDate = DateHelper.add(startDate, values.duration, values.durationUnit || eventRecord.durationUnit);
                    }
                    else if ('fullDuration' in values) {
                        endDate = DateHelper.add(startDate, values.fullDuration);
                    }
                    else {
                        endDate = eventRecord.endDate;
                    }
                }

                const abort = resourceRecords.some(resource => {
                    return !eventStore.isDateRangeAvailable(startDate, endDate, eventRecord, resource);
                });

                if (abort) {
                    resolve(false);
                    return;
                }
            }

            const context = {
                finalize(saveEvent) {
                    try {
                        if (saveEvent !== false) {
                            me.finalizeEventSave(eventRecord, resourceRecords, resolve, reject);
                        }
                        else {
                            resolve(false);
                        }
                    }
                    catch (e) {
                        reject(e);
                    }
                }
            };

            /**
             * Fires on the owning Scheduler before an event is saved.
             * Return `false` to immediately prevent saving
             *
             * ```javascript
             *  scheduler.on({
             *      beforeEventSave() {
             *          // prevent saving if some custom variable hasn't 123 value
             *          return myCustomValue === 123;
             *      }
             *  });
             * ```
             * or a `Promise` yielding `true` or `false` for async vetoing.
             *
             * ```javascript
             *  scheduler.on({
             *      beforeEventSave() {
             *          const
             *              // send ajax request
             *              response = await fetch('http://my-server/check-parameters.php'),
             *              data     = await response.json();
             *
             *          // decide whether it's ok to save based on response "okToSave" property
             *          return data.okToSave;
             *      }
             *  });
             * ```
             *
             * @event beforeEventSave
             * @on-owner
             * @param {Scheduler.view.Scheduler} source The scheduler instance
             * @param {Scheduler.model.EventModel} eventRecord The record about to be saved
             * @param {Scheduler.model.ResourceModel[]} resourceRecords The resources to which the event is assigned
             * @param {Object} values The new values
             * @param {Object} context Extended save context:
             * @param {Boolean} [context.async] Set this to `true` in a listener to indicate that the listener will asynchronously decide to prevent or not the event save.
             * @param {Function} context.finalize Function to call to finalize the save. Used when `async` is `true`. Provide `false` to the function to prevent the save.
             * @preventable
             * @async
             */
            const triggerResult = scheduler.trigger('beforeEventSave', { eventRecord, resourceRecords, values, context });

            // Helper function to handle beforeEventSave listeners result
            function handleEventResult(result, eventRecord, context) {
                // save prevented by a listener
                if (result === false) {
                    resolve(false);
                }
                else {

                    me.onRecurrableEventBeforeSave({ eventRecord, context });
                    // truthy context.async means than a listener will decide to approve saving asynchronously
                    if (!context.async) {
                        context.finalize();
                    }
                }
            }

            if (ObjectHelper.isPromise(triggerResult)) {
                triggerResult.then(result => handleEventResult(result, eventRecord, context));
            }
            else {
                handleEventResult(triggerResult, eventRecord, context);
            }
        });
    }

    //endregion

    //region Delete

    /**
     * Delete event being edited
     * @fires beforeEventDelete
     * @private
     * @async
     */
    deleteEvent() {
        this.detachListeners('changesWhileEditing');

        return new Promise((resolve, reject) => {
            const
                me                      = this,
                { eventRecord, editor } = me;

            me.scheduler.removeEvents([eventRecord], removeRecord => {
                // The reason it does it here is to move focus *before* it gets deleted,
                // and then there's code in the delete to see that it's deleting the focused one,
                // and jump forwards or backwards to move to the next or previous event
                // See 'Should allow key activation' test in tests/view/mixins/EventNavigation.t.js
                if (removeRecord && editor.containsFocus) {
                    editor.revertFocus();
                }

                resolve(removeRecord);
            }, editor);
        });
    }

    //endregion

    //region Stores

    onChangeProject() {
        // Release resource store on project change, it will be re-chained on next show
        if (this.resourceField) {
            this.resourceField.store = {}; // Cannot use null
        }
    }

    get eventStore() {
        return this.scheduler.project.eventStore;
    }

    get resourceStore() {
        return this.scheduler.project.resourceStore;
    }

    get assignmentStore() {
        return this.scheduler.project.assignmentStore;
    }

    //endregion

    //endregion

    //region Events

    onActivateEditor({ eventRecord, resourceRecord, eventElement }) {
        this.editEvent(eventRecord, resourceRecord, eventElement);
    }

    onDragCreateEnd({ eventRecord, resourceRecord, proxyElement, stmCapture }) {
        this.editEvent(eventRecord, resourceRecord, proxyElement, stmCapture);
    }

    // chained from EventNavigation
    onEventEnterKey({ assignmentRecord, eventRecord, target }) {
        const
            { client }  = this,
            // Event can arrive from the wrap element in some products (such as Calendar)
            // so in these cases, we must use querySelector to look *inside* the element.
            element     = target[target.matches(client.eventSelector) ? 'querySelector' : 'closest'](client.eventInnerSelector);

        if (assignmentRecord) {
            this.editEvent(eventRecord, assignmentRecord.resource, element);
        }
        else if (eventRecord) {
            this.editEvent(eventRecord, eventRecord.resource, element);
        }
    }

    // Toggle fields visibility when changing eventType
    onEventTypeChange({ value }) {
        this.toggleEventType(value);
    }

    //endregion

    //region Context menu

    populateEventMenu({ eventRecord, resourceRecord, items }) {
        if (!this.scheduler.readOnly && !this.disabled) {
            items.editEvent = {
                text        : 'L{EventEdit.Edit event}',
                localeClass : this,
                icon        : 'b-icon b-icon-edit',
                weight      : 100,
                disabled    : eventRecord.readOnly,
                onItem      : () => {
                    this.editEvent(eventRecord, resourceRecord);
                }
            };
        }
    }

    //endregion

    onBeforeEditorToggleReveal({ reveal }) {

        if (reveal) {
            this.editor.setupEditorButtons();
        }

        // reveal true/false is analogous to show/hide
        this[reveal ? 'onBeforeEditorShow' : 'resetEditingContext']();
    }

    async resetEditingContext() {
        const me = this;

        me.detachListeners('changesWhileEditing');

        // super call has to go before the `me.rejectStmTransaction();` below
        // because it can be removing an event manually, bypassing the stm
        super.resetEditingContext();

        // client does not use STM for task editing (at least yet)
        if (me.hasStmCapture && !me.isDeletingEvent && !me.isCancelingEdit) {
            await me.freeStm(false);
        }

        // Clear to prevent retaining project
        me.resourceRecord = null;
    }

    finalizeStmCapture(shouldReject) {
        return this.freeStm(!shouldReject);
    }

    updateLocalization() {
        if (this._editor) {
            this.updateCSSVars({ element : this._editor.element });
        }

        super.updateLocalization(...arguments);
    }
}

GridFeatureManager.registerFeature(EventEdit, true, 'Scheduler');
GridFeatureManager.registerFeature(EventEdit, false, ['SchedulerPro', 'ResourceHistogram']);

EventEdit.initClass();
