import AbstractTimeRanges from './AbstractTimeRanges.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import AttachToProjectMixin from '../data/mixin/AttachToProjectMixin.js';
import TimeSpan from '../model/TimeSpan.js';

/**
 * @module Scheduler/feature/TimeRanges
 */

/**
 * Feature that renders global ranges of time in the timeline. Use this feature to visualize a `range` like a 1 hr lunch
 * or some important point in time (a `line`, i.e. a range with 0 duration). This feature can also show a current time
 * indicator if you set {@link #config-showCurrentTimeLine} to true. To style the rendered elements, use the
 * {@link Scheduler.model.TimeSpan#field-cls cls} field of the `TimeSpan` class.
 *
 * {@inlineexample Scheduler/feature/TimeRanges.js}
 *
 * Each time range is represented by an instances of {@link Scheduler.model.TimeSpan}, held in a simple
 * {@link Core.data.Store}. The feature uses {@link Scheduler/model/ProjectModel#property-timeRangeStore} defined on the
 * project by default. The store's persisting/loading is handled by Crud Manager (if it's used by the component).
 *
 * Note that the feature uses virtualized rendering, only the currently visible ranges are available in the DOM.
 *
 * This feature is **off** by default. For info on enabling it, see {@link Grid.view.mixin.GridFeatures}.
 *
 * ## Showing an icon in the time range header
 *
 * You can use Font Awesome icons easily (or set any other icon using CSS) by using the {@link Scheduler.model.TimeSpan#field-iconCls}
 * field. The JSON data below will show a flag icon:
 *
 * ```json
 * {
 *     "id"        : 5,
 *     "iconCls"   : "b-fa b-fa-flag",
 *     "name"      : "v5.0",
 *     "startDate" : "2019-02-07 15:45"
 * },
 * ```
 *
 * ## Recurring time ranges
 *
 * The feature supports recurring ranges in case the provided store and models
 * have {@link Scheduler/data/mixin/RecurringTimeSpansMixin} and {@link Scheduler/model/mixin/RecurringTimeSpan}
 * mixins applied:
 *
 * ```javascript
 * // We want to use recurring time ranges so we make a special model extending standard TimeSpan model with
 * // RecurringTimeSpan which adds recurrence support
 * class MyTimeRange extends RecurringTimeSpan(TimeSpan) {}
 *
 * // Define a new store extending standard Store with RecurringTimeSpansMixin mixin to add recurrence support to the
 * // store. This store will contain time ranges.
 * class MyTimeRangeStore extends RecurringTimeSpansMixin(Store) {
 *     static get defaultConfig() {
 *         return {
 *             // use our new MyResourceTimeRange model
 *             modelClass : MyTimeRange
 *         };
 *     }
 * };
 *
 * // Instantiate store for timeRanges using our new classes
 * const timeRangeStore = new MyTimeRangeStore({
 *     data : [{
 *         id             : 1,
 *         resourceId     : 'r1',
 *         startDate      : '2019-01-01T11:00',
 *         endDate        : '2019-01-01T13:00',
 *         name           : 'Lunch',
 *         // this time range should repeat every day
 *         recurrenceRule : 'FREQ=DAILY'
 *     }]
 * });
 *
 * const scheduler = new Scheduler({
 *     ...
 *     features : {
 *         timeRanges : true
 *     },
 *
 *     crudManager : {
 *         // store for "timeRanges" feature
 *         timeRangeStore
 *     }
 * });
 * ```
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @classtype timeRanges
 * @feature
 * @demo Scheduler/timeranges
 */
export default class TimeRanges extends AbstractTimeRanges.mixin(AttachToProjectMixin) {
    //region Config

    static get $name() {
        return 'TimeRanges';
    }

    static get defaultConfig() {
        return {
            store : true
        };
    }

    static configurable = {
        /**
         * Store that holds the time ranges (using the {@link Scheduler.model.TimeSpan} model or subclass thereof).
         * A store will be automatically created if none is specified.
         * @config {Core.data.Store|StoreConfig}
         * @category Misc
         */
        store : {
            modelClass : TimeSpan
        },

        /**
         * The interval (as amount of ms) defining how frequently the current timeline will be updated
         * @config {Number}
         * @default
         * @category Misc
         */
        currentTimeLineUpdateInterval : 10000,

        /**
         * The date format to show in the header for the current time line (when {@link #config-showCurrentTimeLine} is configured).
         * See {@link Core.helper.DateHelper} for the possible formats to use.
         * @config {String}
         * @default
         * @category Common
         */
        currentDateFormat : 'HH:mm',

        /**
         * Show a line indicating current time. Either `true` or `false` or a {@link Scheduler.model.TimeSpan}
         * configuration object to apply to this special time range (allowing you to provide a custom text):
         *
         * ```javascript
         * showCurrentTimeLine : {
         *     name : 'Now'
         * }
         * ```
         *
         * The line carries the CSS class name `b-sch-current-time`, and this may be used to add custom styling to it.
         *
         * @prp {Boolean|TimeSpanConfig}
         * @default
         * @category Common
         */
        showCurrentTimeLine : false
    };

    //endregion

    //region Init & destroy

    doDestroy() {
        this.storeDetacher?.();

        super.doDestroy();
    }

    /**
     * Returns the TimeRanges which occur within the client Scheduler's time axis.
     * @property {Scheduler.model.TimeSpan[]}
     */
    get timeRanges() {
        const me        = this;

        if (!me._timeRanges) {
            const { store } = me;

            let { records } = store;

            if (store.recurringEvents) {
                const {
                    startDate,
                    endDate
                } = me.client.timeAxis;

                records = records.flatMap(timeSpan => {
                    // Collect occurrences for the recurring events in the record set
                    if (timeSpan.isRecurring) {
                        return timeSpan.getOccurrencesForDateRange(startDate, endDate);
                    }

                    return timeSpan;
                });
            }

            if (me.currentTimeLine) {
                // Avoid polluting store records
                if (!store.recurringEvents) {
                    records = records.slice();
                }

                records.push(me.currentTimeLine);
            }

            me._timeRanges = records;
        }

        return me._timeRanges;
    }
    //endregion

    //region Current time line

    attachToProject(project) {
        super.attachToProject(project);
        const me = this;

        me.projectTimeZoneChangeDetacher?.();

        if (me.showCurrentTimeLine) {

            // Update currentTimeLine immediately after a time zone change
            me.projectTimeZoneChangeDetacher = me.client.project?.ion({ timeZoneChange : () => me.updateCurrentTimeLine() });

            // Update currentTimeLine if its already created
            if (me.currentTimeLine) {
                me.updateCurrentTimeLine();
            }
        }
    }

    initCurrentTimeLine() {
        const me = this;

        if (me.currentTimeLine || !me.showCurrentTimeLine) {
            return;
        }

        const data = typeof me.showCurrentTimeLine === 'object' ? me.showCurrentTimeLine : {};

        me.currentTimeLine = me.store.modelClass.new({
            id  : 'currentTime', 
            cls : 'b-sch-current-time'
        }, data);

        me.currentTimeInterval = me.setInterval(() => me.updateCurrentTimeLine(), me.currentTimeLineUpdateInterval);

        me._timeRanges = null;

        me.updateCurrentTimeLine();
    }

    updateCurrentTimeLine() {
        const
            me                  = this,
            { currentTimeLine } = me;

        currentTimeLine.timeZone = me.project?.timeZone;
        currentTimeLine.setLocalDate('startDate', new Date());
        currentTimeLine.endDate = currentTimeLine.startDate;

        if (!currentTimeLine.originalData.name) {
            currentTimeLine.name = DateHelper.format(currentTimeLine.startDate, me.currentDateFormat);
        }

        me.renderRanges();
    }

    hideCurrentTimeLine() {
        const me = this;

        if (!me.currentTimeLine) {
            return;
        }

        me.clearInterval(me.currentTimeInterval);
        me.currentTimeLine = null;

        me.refresh();
    }

    updateShowCurrentTimeLine(show) {
        if (show) {
            this.initCurrentTimeLine();
        }
        else {
            this.hideCurrentTimeLine();
        }
    }

    //endregion

    //region Menu items

    /**
     * Adds a menu item to show/hide current time line.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateTimeAxisHeaderMenu({ items }) {
        items.currentTimeLine = {
            weight   : 400,
            text     : this.L('L{showCurrentTimeLine}'),
            checked  : this.currentTimeLine,
            onToggle : ({ checked }) => this.updateShowCurrentTimeLine(checked && this.showCurrentTimeLine)
        };
    }

    //endregion

    //region Store

    attachToStore(store) {
        const me = this;

        let renderRanges = false;

        // if we had some store assigned before we need to detach it
        if (me.storeDetacher) {
            me.storeDetacher();
            // then we'll need to render ranges provided by the new store
            renderRanges = true;
        }

        me.storeDetacher = store.ion({
            change  : 'onStoreChange',
            refresh : 'onStoreChange',
            thisObj : me
        });

        me._timeRanges = null;

        // render ranges if needed
        renderRanges && me.renderRanges();
    }

    /**
     * Returns the {@link Core.data.Store store} used by this feature
     * @property {Core.data.Store}
     * @category Misc
     */
    get store() {
        return this.client.project.timeRangeStore;
    }

    updateStore(store) {
        const
            me          = this,
            { client }  = me,
            { project } = client;

        store = project.timeRangeStore;

        me.attachToStore(store);

        // timeRanges can be set on scheduler/gantt, for convenience. Should only be processed by the TimeRanges and not
        // any subclasses
        if (client.timeRanges && !client._timeRangesExposed) {
            store.add(client.timeRanges);
            delete client.timeRanges;
        }
    }

    // Called by ProjectConsumer after a new store is assigned at runtime
    attachToTimeRangeStore(store) {
        this.store = store;
    }

    resolveTimeRangeRecord(el) {
        return this.store.getById(el.closest(this.baseSelector).dataset.id);
    }

    onStoreChange({ type, action }) {
        const me = this;

        // Force re-evaluating of which ranges to consider for render
        me._timeRanges = null;

        // https://github.com/bryntum/support/issues/1398 - checking also if scheduler is visible to change elements
        if (me.disabled || !me.client.isVisible || me.isConfiguring || (type === 'refresh' && action !== 'batch')) {
            return;
        }

        me.client.runWithTransition(() => me.renderRanges(), !me.client.refreshSuspended);
    }

    //endregion

    //region Drag

    onDragStart(event) {
        const
            me                = this,
            { context }       = event,
            record            = me.resolveTimeRangeRecord(context.element.closest(me.baseSelector)),
            rangeBodyEl       = me.getBodyElementByRecord(record);

        context.relatedElements = [rangeBodyEl];

        Object.assign(context, {
            record,
            rangeBodyEl,
            originRangeX : DomHelper.getTranslateX(rangeBodyEl),
            originRangeY : DomHelper.getTranslateY(rangeBodyEl)
        });

        super.onDragStart(event);

        me.showTip(context);
    }

    onDrop(event) {
        const { context } = event;

        if (!context.valid) {
            return this.onInvalidDrop({ context });
        }

        const
            me          = this,
            { client }  = me,
            { record }  = context,
            box         = Rectangle.from(context.rangeBodyEl),
            newStart    = client.getDateFromCoordinate(box.getStart(client.rtl, client.isHorizontal), 'round', false),
            wasModified = (record.startDate - newStart !== 0);

        if (wasModified) {
            record.setStartDate(newStart);
        }
        else {
            me.onInvalidDrop();
        }

        me.destroyTip();

        super.onDrop(event);
    }

    //endregion

    //region Resize

    onResizeStart({ context }) {
        const
            me          = this,
            record      = me.resolveTimeRangeRecord(context.element.closest(me.baseSelector)),
            rangeBodyEl = me.getBodyElementByRecord(record);

        Object.assign(context, {
            record,
            rangeBodyEl
        });

        me.showTip(context);
    }

    onResizeDrag({ context }) {
        const
            me              = this,
            { rangeBodyEl } = context,
            { client }      = me,
            box             = Rectangle.from(context.element),
            startPos        = box.getStart(client.rtl, client.isHorizontal),
            endPos          = box.getEnd(client.rtl, client.isHorizontal),
            startDate       = client.getDateFromCoordinate(startPos, 'round', false),
            endDate         = client.getDateFromCoordinate(endPos, 'round', false);

        if (me.client.isVertical) {
            if (context.edge === 'top') {
                DomHelper.setTranslateY(rangeBodyEl, context.newY);
            }

            rangeBodyEl.style.height = context.newHeight + 'px';
        }
        else {
            if (context.edge === 'left') {
                DomHelper.setTranslateX(rangeBodyEl, context.newX);
            }

            rangeBodyEl.style.width = context.newWidth + 'px';
        }

        me.updateDateIndicator({ startDate, endDate });
    }

    onResize({ context }) {
        if (!context.valid) {
            return this.onInvalidDrop({ context });
        }

        const
            me          = this,
            { client }  = me,
            { rtl }     = client,
            record      = context.record,
            box         = Rectangle.from(context.element),
            startPos    = box.getStart(rtl, client.isHorizontal),
            endPos      = box.getEnd(rtl, client.isHorizontal),
            newStart    = client.getDateFromCoordinate(startPos, 'round', false),
            isStart     = (rtl && context.edge === 'right') || (!rtl && context.edge === 'left') || context.edge === 'top',
            newEnd      = client.getDateFromCoordinate(endPos, 'round', false),
            wasModified = (isStart && record.startDate - newStart !== 0) ||
                (newEnd && record.endDate - newEnd !== 0);

        if (wasModified && newEnd > newStart) {
            if (isStart) {
                // could be that the drag operation placed the range with start/end outside the axis
                record.setStartDate(newStart, false);
            }
            else {
                record.setEndDate(newEnd, false);
            }
        }
        else {
            me.onInvalidResize({ context });
        }

        me.destroyTip();
    }

    onInvalidResize({ context }) {
        const me = this;

        me.resize.reset();
        // Allow DomSync to reapply original state
        context.rangeBodyEl.parentElement.lastDomConfig = context.rangeBodyEl.lastDomConfig = null;
        me.renderRanges();

        me.destroyTip();
    }

    //endregion
}

GridFeatureManager.registerFeature(TimeRanges, false, ['Scheduler', 'Gantt']);
