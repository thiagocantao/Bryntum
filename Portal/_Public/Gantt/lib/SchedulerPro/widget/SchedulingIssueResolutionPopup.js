import StringHelper from '../../Core/helper/StringHelper.js';
import Popup from '../../Core/widget/Popup.js';
import { EffectResolutionResult } from '../../Engine/chrono/SchedulingIssueEffect.js';
import Promissory from '../../Core/helper/util/Promissory.js';
import RadioGroup from '../../Core/widget/RadioGroup.js';

import '../localization/En.js';

/**
 * @module SchedulerPro/widget/SchedulingIssueResolutionPopup
 */

/**
 * A Popup informing user of a scheduling issue that needs manual resolution.
 * Examples of such cases could be an infinite cycle, a scheduling conflict or a calendar misconfiguration.
 * The dialog displays the case description and lets the user pick one of the possible resolutions.
 *
 * {@inlineexample SchedulerPro/widget/SchedulingIssueResolutionPopup.js}
 *
 * @demo SchedulerPro/conflicts
 * @extends Core/widget/Popup
 * @classType schedulingissueresolutionpopup
 */
export default class SchedulingIssueResolutionPopup extends Popup {



    static get $name() {
        return 'SchedulingIssueResolutionPopup';
    }

    // Factoryable type name
    static get type() {
        return 'schedulingissueresolutionpopup';
    }

    static get configurable() {
        return {
            localizableProperties : [],
            schedulingIssue       : null,
            align                 : 'b-t',
            autoShow              : false,
            autoClose             : false,
            closeAction           : 'onCloseButtonClick',
            modal                 : true,
            centered              : true,
            scrollAction          : 'realign',
            constrainTo           : globalThis,
            draggable             : false,
            closable              : true,
            floating              : true,
            cls                   : 'b-schedulerpro-issueresolutionpopup',
            layout                : 'vbox',

            resolutionsGroup : {
                type : 'radiogroup',
                name : 'resolutions'
            },

            items : {
                description : {
                    type   : 'widget',
                    cls    : 'b-error-description',
                    weight : -100
                }
            },
            bbar : {
                defaults : {
                    localeClass : this
                },
                items : {
                    applyButton : {
                        weight   : 100,
                        color    : 'b-raised b-blue',
                        text     : 'L{Apply}',
                        onClick  : 'up.onApplyButtonClick',
                        disabled : true
                    },
                    cancelButton : {
                        weight  : 200,
                        color   : 'b-gray',
                        text    : 'L{Object.Cancel}',
                        onClick : 'up.onCancelButtonClick'
                    }
                }
            }
        };
    }

    get selectedResolutions() {
        const
            selectedResolutions = new Set(),
            selectedResolution = this.resolutionsGroup?.selected?.resolution;

        if (selectedResolution) {
            selectedResolutions.add(selectedResolution);
        }

        return selectedResolutions;
    }

    changeResolutionsGroup(config, existing) {
        return RadioGroup.reconfigure(existing, config, /* owner = */this);
    }

    updateResolutionsGroup(instance) {
        instance && this.add(instance);
    }

    /**
     * Returns parameters for the provided resolution that should be
     * passed to its `resolve` method.
     * @param {Object} resolution Scheduling exception resolution
     * @returns {Array} The resolution arguments
     */
    getResolutionParameters(resolution) {
        return [];
    }

    onApplyButtonClick() {
        const
            me                      = this,
            { selectedResolutions } = me;

        if (selectedResolutions.size) {
            // apply selected resolutions
            selectedResolutions.forEach(resolution => resolution.resolve(...me.getResolutionParameters(resolution)));

            me.continueWithResolutionResult(EffectResolutionResult.Resume);

            me.doResolve(selectedResolutions);
        }
        else {
            me.onCancelButtonClick();
        }
    }

    onCancelButtonClick() {
        this.continueWithResolutionResult(EffectResolutionResult.Cancel);

        this.doResolve();
    }

    onCloseButtonClick() {
        if (this.canCancel) {
            this.onCancelButtonClick();
        }
    }

    get isResolving() {
        return Boolean(this.resolving);
    }

    /**
     * Resolves a scheduling conflict happened on the project (a scheduling conflict or a calendar misconfiguration).
     * @param {Object} event The scheduling exception event data:
     * @param {SchedulerPro.model.ProjectModel} event.source The project
     * @param {*} event.schedulingIssue The scheduling exception
     * @param {Function} event.continueWithResolutionResult The function to be called once the resolution is chosen and
     * applied (or it was decided to cancel the changes).
     * @returns {Promise} Promise that gets resolved when user picks a resolution and clicks "Apply" (or "Cancel") button.
     */
    async resolve({ source, schedulingIssue, continueWithResolutionResult }) {
        const me = this;

        me.project = source;
        me.schedulingIssue = schedulingIssue;
        me.continueWithResolutionResult = continueWithResolutionResult;

        me.updatePopupContent(schedulingIssue, continueWithResolutionResult);

        me.onResolutionChange({});

        me.show();

        me.resolving = new Promissory();

        return me.resolving.promise;
    }

    doResolve(resolutions) {
        const
            me            = this,
            { resolving } = me;

        if (resolving) {
            me.resolving.resolve(resolutions);
            me.resolving = null;
            me.schedulingIssue = null;
            me.hide();
        }
    }

    getResolutionWidgetConfig(resolution) {
        return {
            text              : StringHelper.encodeHtml(resolution.getDescription()),
            cls               : 'b-resolution',
            checkedValue      : resolution.$$name,
            localeClass       : this,
            name              : 'resolutions',
            internalListeners : {
                change : 'up.onResolutionChange'
            },
            resolution
        };
    }

    getResolutions() {
        return this.schedulingIssue?.getResolutions();
    }

    updatePopupContent(schedulingIssue, continueWithResolutionResult) {
        const
            me = this,
            { resolutionsGroup } = me;

        if (continueWithResolutionResult) {
            me.continueWithResolutionResult = continueWithResolutionResult;
        }

        if (schedulingIssue) {
            me.schedulingIssue = schedulingIssue;
        }
        else {
            schedulingIssue = me.schedulingIssue;
        }

        me.title = schedulingIssue?.type ? me.optionalL(schedulingIssue.type) : 'Unknown error';

        me.widgetMap.description.content = schedulingIssue && StringHelper.encodeHtml(schedulingIssue.getDescription());

        const
            resolutions     = me.getResolutions(),
            resolutionItems = resolutions?.map(resolution => me.getResolutionWidgetConfig(resolution)) || [],
            name            = resolutionItems[0]?.name || 'resolutions';

        resolutionsGroup.removeAll();
        resolutionsGroup.name = name;
        resolutionsGroup.add(...resolutionItems, {
            ref               : 'cancelResolution',
            text              : 'L{Cancel changes}',
            name,
            checkedValue      : 'cancel',
            localeClass       : this,
            cls               : 'b-resolution',
            internalListeners : {
                change : 'up.onResolutionChange'
            }
        });

        // toggle ok/cancel controls state
        me.toggleControlsState();
    }

    get canApply() {
        return this.resolutionsGroup.value != null;
    }

    get canCancel() {
        // cancel makes no sense for initial transaction
        return !this.project?.isInitialCommit;
    }

    get cancelResolution() {
        return this.resolutionsGroup.widgetMap.cancelResolution;
    }

    onResolutionChange() {
        this.toggleControlsState();
    }

    toggleControlsState() {
        const
            me = this,
            { applyButton, cancelButton } = me.widgetMap;

        applyButton.disabled = !me.canApply;
        me.cancelResolution.hidden = cancelButton.hidden = !me.canCancel;
    }

    updateLocalization() {
        this.updatePopupContent();
        super.updateLocalization();
    }
};

// Register this widget type with its Factory
SchedulingIssueResolutionPopup.initClass();
