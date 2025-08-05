import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import EventHelper from '../../Core/helper/EventHelper.js';

/**
 * @module SchedulerPro/feature/TimeSpanHighlight
 */

const
    timespanDefaults = {
        isHighlightConfig : true,
        clearExisting     : false
    };

/**
 * An object describing the time span region to highlight.
 *
 * @typedef {Object} HighlightTimeSpan
 * @property {Date} startDate A start date constraining the region
 * @property {Date} endDate An end date constraining the region
 * @property {String} name A name to show in the highlight element
 * @property {Scheduler.model.ResourceModel} [resourceRecord] The resource record (applicable for Scheduler only)
 * @property {Core.data.Model} [taskRecord] The task record (applicable for Gantt only)
 * @property {String} [cls] A CSS class to add to the highlight element
 * @property {Boolean} [clearExisting=true] `false` to keep existing highlight elements
 * @property {String} [animationId] An id to enable animation of highlight elements
 * @property {Boolean} [surround=false] True to shade the time axis areas before and after the time span
 * (adds a `b-unavailable` CSS class which you can use for styling)
 * @property {Number} [padding] Inflates the non-timeaxis sides of the region by this many pixels
 */

/**
 * This feature exposes methods on the owning timeline widget which you can use to highlight one or multiple time spans
 * in the schedule. Please see {@link #function-highlightTimeSpan} and {@link #function-highlightTimeSpans} to learn
 * more or try the demo below:
 *
 * {@inlineexample SchedulerPro/feature/TimeSpanHighlight.js}
 *
 * ## Example usage with Scheduler Pro
 *
 * ```javascript
 * const scheduler = new SchedulerPro({
 *     features : {
 *         timeSpanHighlight : true
 *     }
 * })
 *
 * scheduler.highlightTimeSpan({
 *      startDate : new Date(2022, 4, 1),
 *      endDate   : new Date(2022, 4, 5),
 *      name      : 'Time off'
 * });
 * ```
 *
 * ## Example usage with Gantt
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         timeSpanHighlight : true
 *     }
 * })
 *
 * gantt.highlightTimeSpan({
 *      startDate : new Date(2022, 4, 1),
 *      endDate   : new Date(2022, 4, 5),
 *      padding   : 10, // Some "air" around the rectangle
 *      taskRecord, // You can also highlight an area specific to a Gantt task
 *      name      : 'Time off'
 * });
 * ```
 *
 * This feature is **disabled** by default.
 *
 * @extends Core/mixin/InstancePlugin
 * @classtype timeSpanHighlight
 * @feature
 * @demo SchedulerPro/highlight-time-spans
 */
export default class TimeSpanHighlight extends InstancePlugin {

    //region Config
    domConfigs = [];
    configs = [];

    static get $name() {
        return 'TimeSpanHighlight';
    }

    static get configurable() {
        return {
            padding : 0
        };
    }

    static get pluginConfig() {
        return {
            assign : [
                'highlightTimeSpan',
                'highlightTimeSpans',
                'unhighlightTimeSpans'
            ],
            chain : [
                'onTimeAxisViewModelUpdate'
            ]
        };
    }

    //endregion

    construct() {
        super.construct(...arguments);

        this.client.rowManager.ion({
            renderDone : this.onViewChanged,
            thisObj    : this
        });
    }

    /**
     * Highlights the region representing the passed time span and optionally for a single certain resource.
     * @on-owner
     * @param {HighlightTimeSpan} options A single options object describing the time span to highlight.
     */
    highlightTimeSpan(config, draw = true) {
        const
            me         = this,
            {
                startDate,
                endDate,
                name,
                surround,
                padding       = me.padding,
                clearExisting = true
            }          = config,
            { client } = me,
            taskRecord = config.isTimeSpan ? config : config.taskRecord;

        // The resource property allows an actual TaskRecord to be used as a config.
        let resourceRecord  = config.resourceRecord || config.resource;
        const { animationId } = config;

        if (animationId) {
            DomHelper.addTemporaryClass(client.element, 'b-transition-highlight', 500, client);
        }

        if (clearExisting) {
            me.domConfigs.length = me.configs.length = 0;
        }

        if (me.disabled) {
            // nothing to highlight
            return;
        }

        if (surround) {
            me.surroundTimeSpan(config);
            return;
        }

        me.configs.push(config);

        let rect;
        if (client.isGanttBase) {
            rect = client.getScheduleRegion(taskRecord, true, { start : startDate, end : endDate });
        }
        else {
            if (resourceRecord) {
                // Allows resolving link from original in TreeGrouped scheduler
                resourceRecord = client.store.getById(resourceRecord);
            }

            rect = client.getScheduleRegion(resourceRecord, null, true, { start : startDate, end : endDate }, !resourceRecord);
        }

        if (!rect) {
            // nothing to highlight
            return;
        }

        if (padding) {
            if (client.isHorizontal) {
                rect.inflate(padding, 0, padding, 0);
            }
            else {
                rect.inflate(0, padding, 0, padding);
            }
        }

        me.domConfigs.push(
            rect.visualize({
                children : [
                    {
                        class : 'b-sch-highlighted-range-name',
                        html  : name
                    }
                ],
                dataset : {
                    syncId : animationId
                },
                class : {
                    'b-sch-highlighted-range'                           : 1,
                    [config.cls]                                        : config.cls,
                    [config.class || 'b-sch-highlighted-range-default'] : 1
                }
            }, true)
        );

        if (draw) {
            me.draw();
        }

        client.syncSplits?.(split => split.highlightTimeSpan(config, draw));
    }

    draw() {
        DomSync.sync({
            targetElement : this.containerEl,
            domConfig     : {
                onlyChildren : true,
                children     : this.domConfigs
            }
        });
    }

    surroundTimeSpan(timeSpan) {
        this.highlightTimeSpans([
            Object.assign({}, timeSpan, {
                animationId : (timeSpan.animationId || '') + 'Before',
                class       : 'b-unavailable',
                surround    : false,
                startDate   : this.client.startDate,
                endDate     : timeSpan.startDate
            }),
            Object.assign({}, timeSpan, {
                animationId : (timeSpan.animationId || '') + 'After',
                class       : 'b-unavailable',
                surround    : false,
                startDate   : timeSpan.endDate,
                endDate     : this.client.endDate
            })
        ], { clearExisting : timeSpan.clearExisting });
    }

    /**
     * Highlights the regions representing the passed time spans.
     * @on-owner
     * @param {HighlightTimeSpan[]} timeSpans An array of objects with start/end dates describing the rectangle to highlight.
     * @param {Object} [options] A single options object
     * @param {Boolean} [options.clearExisting=true] Set to `false` to preserve previously highlighted elements
     */
    highlightTimeSpans(timeSpans, options = {}) {
        const
            me = this,
            {
                clearExisting = true
            }  = options;

        if (clearExisting) {
            timeSpans = timeSpans.slice();
            me.domConfigs.length = me.configs.length = 0;
        }

        if (me.disabled) {
            return;
        }

        timeSpans.forEach(timeSpan => {
            // If we are *re*drawing a set of configs, they will have the isHighlightConfig
            // property, so we can pass them straight in. If its a config from the outside,
            // then apply the defaults and the isHighlightConfig flag.
            me.highlightTimeSpan(timeSpan.isHighlightConfig ? timeSpan : Object.setPrototypeOf(timespanDefaults, timeSpan), false);
        });

        me.draw();
    }

    /**
     * Removes any highlighting elements.
     * @param {Boolean} [fadeOut] `true` to fade out the highlight elements before removing
     * @on-owner
     */
    async unhighlightTimeSpans(fadeOut = false) {
        const
            me         = this,
            { client } = me;

        if (fadeOut) {
            DomHelper.addTemporaryClass(client.element, 'b-transition-highlight', 500, client);
        }

        Array.from(me.containerEl.children).forEach(element => {
            if (fadeOut) {
                element.style.opacity = 0;
                me.fadeOutDetacher    = EventHelper.onTransitionEnd({
                    element,
                    property : 'opacity',
                    thisObj  : client,
                    handler  : () => {
                        me.domConfigs.length = me.configs.length = 0;
                        me.draw();
                    }
                });
            }
            else {
                me.domConfigs.length = me.configs.length = 0;
                me.draw();
            }
        });

        client.syncSplits?.(split => split.unhighlightTimeSpans(fadeOut));
    }

    get containerEl() {
        if (!this._containerEl) {
            this._containerEl = DomHelper.createElement({
                parent        : this.client.foregroundCanvas,
                retainElement : true,
                class         : 'b-sch-highlight-container'
            });
        }

        return this._containerEl;
    }

    onTimeAxisViewModelUpdate() {
        this.onViewChanged();
    }

    onViewChanged() {
        if (this.configs.length > 0) {
            this.highlightTimeSpans(this.configs);
        }
    }

    updateDisabled(disabled, was) {
        if (disabled) {
            this.unhighlightTimeSpans();
        }

        super.updateDisabled(disabled, was);
    }

    // No classname on Scheduler's/Gantt's element
    get featureClass() {}
}

GridFeatureManager.registerFeature(TimeSpanHighlight, false, ['SchedulerPro', 'Gantt']);
