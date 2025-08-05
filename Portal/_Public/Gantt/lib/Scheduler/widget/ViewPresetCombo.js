import Combo from '../../Core/widget/Combo.js';
import Widget from '../../Core/widget/Widget.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module Scheduler/widget/ViewPresetCombo
 */

/**
 * A combo for selecting {@link Scheduler.preset.ViewPreset} for Scheduler and Gantt. Lets the user select
 * between specified {@link #config-presets} available.
 *
 * {@inlineexample Scheduler/widget/ViewPresetCombo.js}
 *
 * Add it to the component's toolbar to connect it automatically:
 * ```javascript
 * new Scheduler({
 *    tbar : {
 *        viewPresetCombo: {
 *            type: 'viewpresetcombo',
 *            width: '7em'
 *        }
 *    }
 * });
 * ```
 *
 * Or specify which Scheduler, SchedulerPro or Gantt component instance it should connect to the {@link #config-client}
 * config:
 * ```javascript
 * const scheduler = new Scheduler({ ... });
 * const viewPresetCombo = new ViewPresetCombo({
 *     appendTo : 'someElementClassName',
 *     client   : scheduler
 * });
 * ```
 *
 * By default, the following presets are shown in the combo:
 * * {@link Scheduler.preset.PresetManager hourAndDay}
 * * {@link Scheduler.preset.PresetManager weekAndDay}
 * * {@link Scheduler.preset.PresetManager dayAndMonth}
 *
 * ## Changing selectable presets
 * To change the default selectable presets specify an array of preset ids. The presets specified must be available to
 * the client.
 *
 * ```javascript
 * viewPresetCombo: {
 *    presets: ['weekAndDay', 'dayAndMonth', 'myCustomPreset']
 * }
 * ```
 *
 * NOTE: The selectable presets will be arranged in the order provided in the {@link #config-presets} config.
 * @extends Core/widget/Combo
 * @classType viewpresetcombo
 * @widget
 */
export default class ViewPresetCombo extends Combo {
    static $name = 'ViewPresetCombo';

    static type = 'viewpresetcombo';

    static configurable = {

        /**
         * An array containing string {@link Scheduler.preset.ViewPreset} ids available for selection. The specified
         * presets must be {@link Scheduler.view.mixin.TimelineViewPresets#config-presets available} for the
         * {@link #config-client} (Scheduler, SchedulerPro or Gantt) for it to work properly. The selectable presets
         * will be arranged in the order provided here.
         * @config {Array}
         */
        presets : ['hourAndDay', 'weekAndDay', 'dayAndMonth'],

        /**
         * If not added to a toolbar, provide a Scheduler, SchedulerPro or Gantt component instance to which the
         * ViewPresetCombo should be connected.
         * @config {Scheduler.view.TimelineBase}
         * @default
         */
        client : null,

        /**
         * @hideconfigs caseSensitive,chipView,clearTextOnPickerHide,createOnUnmatched,displayField,displayValueRenderer,emptyText,encodeFilterParams,filterOnEnter,filterOperator,filterParamName,filterSelected,hideTrigger,inlinePicker,items,keyStrokeFilterDelay,minChars,multiSelect,multiValueSeparator,primaryFilter,store,triggerAction,validateFilter,value,valueField,containValues,container,inline,adopt,dataset,title,autoSelect,clearable,highlightExternalChange,keyStrokeChangeDelay,maxLength,minLength,placeholder,required,revertOnEscape,triggers,autoComplete,inputType,spellCheck,validateOnInput,tooltip,autoClose,autoExpand,picker,pickerAlignElement
         */
        /**
         * @hideproperties filterOperator,isEmpty,queryLast,records,store,valueCollection,content,contentElement,dataset,html,overflowElement,errorTip,isEmptyInput,isValid,triggers,scrollable,cellInfo,tab
         */
        /**
         * @hidefunctions clear,clearError,getErrors,select,setError,contains,exitFullscreen,requestFullscreen,owns,query,queryAll
         */
        /**
         * @hideevents input
         */

        editable     : false,
        valueField   : 'id',
        displayField : 'name',
        placeholder  : 'Select view'
    };

    construct() {
        super.construct(...arguments);
        this.scheduler.ion({
            presetchange : this.onClientPresetChange,
            thisObj      : this
        });
    }

    // Returns current client (Scheduler, SchedulerPro or Gantt)
    get scheduler() {
        return this.client || this.up(widget => widget.isTimelineBase) || Widget.query(widget => widget.isTimelineBase);
    }

    changeValue(value, oldValue) {
        // Set up items before applying value, to prevent an empty store from being created and then replaced,
        // which in turn leads to preset changing (caused by https://github.com/bryntum/support/issues/5732)
        this.getConfig('presets');

        return super.changeValue(value, oldValue);
    }

    // Creates a chained store of the clients presets store filtered and sorted by the presets config
    updatePresets(presets) {
        this.store = this.scheduler.presets.chain(r => presets.includes(r.id));
        this.store.sort((a, b) => presets.indexOf(a.id) - presets.indexOf(b.id));
    }

    // When client preset is changed from somewhere else, zooming for example.
    onClientPresetChange({ preset }) {
        const me = this;
        if (!me._isSelecting) {
            me.isSettingValue = true;
            // Select preset in combo if it exists
            if (me.store.includes(preset.id)) {
                me.value = preset;
            }
            // Clear combo otherwise
            else {
                me.clear();
            }
            me.isSettingValue = false;
        }
    }

    onSelect({ record }) {
        if (!this.isSettingValue) {
            const
                { scheduler }                    = this,
                { mainUnit, start, defaultSpan } = record;

            scheduler.suspendRefresh();
            this._isSelecting = true;
            scheduler.viewPreset = record;
            this._isSelecting = false;

            if (mainUnit && defaultSpan) {
                let beginningOf = mainUnit;
                if (start && typeof start === 'string') {
                    beginningOf = DateHelper.parseTimeUnit(start) ?? start;
                }
                let startDate = DateHelper.startOf(scheduler.startDate, beginningOf);

                if (record.start && typeof start === 'number') {
                    startDate = DateHelper.add(startDate, start, mainUnit);
                }

                const endDate = DateHelper.add(startDate, defaultSpan, mainUnit);
                scheduler.setTimeSpan(startDate, endDate);
            }
            if (scheduler.isVertical) {
                scheduler.scrollTop = 0;
            }
            else {
                scheduler.scrollLeft = 0;
            }
            scheduler.resumeRefresh(true);
        }
    }

    // If underlying store localizes, we need to sync the input field value
    updateLocalization() {
        super.updateLocalization();
        this.syncInputFieldValue();
    }
}

// Register this widget type with its Factory
ViewPresetCombo.initClass();
