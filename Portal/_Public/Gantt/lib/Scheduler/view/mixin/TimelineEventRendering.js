import Base from '../../../Core/Base.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/view/mixin/TimelineEventRendering
 */

/**
 * Functions to handle event rendering (EventModel -> dom elements).
 *
 * @mixin
 */
export default Target => class TimelineEventRendering extends (Target || Base) {
    static get $name() {
        return 'TimelineEventRendering';
    }

    //region Default config

    static get defaultConfig() {
        return {
            /**
             * When `true`, events are sized and positioned based on rowHeight, resourceMargin and barMargin settings.
             * Set this to `false` if you want to control height and vertical position using CSS instead.
             *
             * Note that events always get an absolute top position, but when this setting is enabled that position
             * will match row's top. To offset within the row using CSS, use `transform : translateY(y)`.
             *
             * @config {Boolean}
             * @default
             * @category Scheduled events
             */
            managedEventSizing : true,

            /**
             * The CSS class added to an event/assignment when it is newly created
             * in the UI and unsynced with the server.
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            generatedIdCls : 'b-sch-dirty-new',

            /**
             * The CSS class added to an event when it has unsaved modifications
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            dirtyCls : 'b-sch-dirty',

            /**
             * The CSS class added to an event when it is currently committing changes
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            committingCls : 'b-sch-committing',

            /**
             * The CSS class added to an event/assignment when it ends outside of the visible time range.
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            endsOutsideViewCls : 'b-sch-event-endsoutside',

            /**
             * The CSS class added to an event/assignment when it starts outside of the visible time range.
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            startsOutsideViewCls : 'b-sch-event-startsoutside',

            /**
             * The CSS class added to an event/assignment when it is not draggable.
             * @config {String}
             * @default
             * @private
             * @category CSS
             */
            fixedEventCls : 'b-sch-event-fixed'
        };
    }

    static configurable = {
        /**
         * Controls how much space to leave between stacked event bars in px.
         *
         * Value will be constrained by half the row height in horizontal mode.
         *
         * @prp {Number}
         * @default
         * @category Scheduled events
         */
        barMargin : 10,

        /**
         * Specify `true` to force rendered events/tasks to fill entire ticks. This only affects rendering, start
         * and end dates retain their value on the data level.
         *
         * When enabling `fillTicks` you should consider either disabling EventDrag/TaskDrag and EventResize/TaskResize,
         * or enabling {@link Scheduler/view/mixin/TimelineDateMapper#config-snap}. Otherwise their behaviour might not
         * be what a user expects.
         *
         * @prp {Boolean}
         * @default
         * @category Scheduled events
         */
        fillTicks : false,

        resourceMargin : null,

        /**
         * Event color used by default. Events and resources can specify their own color, with priority order being:
         * Event -> Resource -> Scheduler default.
         *
         * Specify `null` to not apply a default color and take control using custom CSS (an easily overridden color
         * will be used to make sure events are still visible).
         *
         * For available standard colors, see {@link Scheduler.model.mixin.EventModelMixin#typedef-EventColor}.
         *
         * @prp {EventColor} eventColor
         * @category Scheduled events
         */
        eventColor : 'green',

        /**
         * Event style used by default. Events and resources can specify their own style, with priority order being:
         * Event -> Resource -> Scheduler default. Determines the appearance of the event by assigning a CSS class
         * to it. Available styles are:
         *
         * * `'plain'` (default) - flat look
         * * `'border'` - has border in darker shade of events color
         * * `'colored'` - has colored text and wide left border in same color
         * * `'hollow'` - only border + text until hovered
         * * `'line'` - as a line with the text below it
         * * `'dashed'` - as a dashed line with the text below it
         * * `'minimal'` - as a thin line with small text above it
         * * `'rounded'` - minimalistic style with rounded corners
         * * `null` - do not apply a default style and take control using custom CSS (easily overridden basic styling will be used).
         *
         * In addition, there are two styles intended to be used when integrating with Bryntum Calendar. To match
         * the look of Calendar events, you can use:
         *
         * * `'calendar'` - a variation of the "colored" style matching the default style used by Calendar
         * * `'interday'` - a variation of the "plain" style, for interday events
         *
         * @prp {'plain'|'border'|'colored'|'hollow'|'line'|'dashed'|'minimal'|'rounded'|'calendar'|'interday'|null}
         * @default
         * @category Scheduled events
         */
        eventStyle : 'plain',

        /**
         * The width/height (depending on vertical / horizontal mode) of all the time columns.
         *
         * There is a limit for the tick size value. Its minimal allowed value is calculated so ticks would fit the
         * available space. Only applicable when {@link Scheduler.view.TimelineBase#config-forceFit} is set to
         * `false`. To set `tickSize` freely skipping that limitation please set
         * {@link Scheduler.view.TimelineBase#config-suppressFit} to `true`.
         *
         * @prp {Number}
         * @category Scheduled events
         */
        tickSize : null
    };

    //endregion

    //region Settings

    updateFillTicks(fillTicks) {
        if (!this.isConfiguring) {
            this.timeAxis.forceFullTicks = fillTicks && this.snap;

            this.refreshWithTransition();

            this.trigger('stateChange');
        }
    }

    changeBarMargin(margin) {
        ObjectHelper.assertNumber(margin, 'barMargin');

        // bar margin should not exceed half of the row height
        if (this.isHorizontal && this.rowHeight) {
            return Math.min(Math.ceil(this.rowHeight / 2), margin);
        }

        return margin;
    }

    updateBarMargin() {
        if (this.rendered) {
            this.currentOrientation.onBeforeRowHeightChange();
            this.refreshWithTransition();
            this.trigger('stateChange');
        }
    }

    // Documented in SchedulerEventRendering to not show up in Gantt docs
    get resourceMargin() {
        return this._resourceMargin == null ? this.barMargin : this._resourceMargin;
    }

    changeResourceMargin(margin) {
        ObjectHelper.assertNumber(margin, 'resourceMargin');

        // resource margin should not exceed half of the row height
        if (this.isHorizontal && this.rowHeight) {
            return Math.min(Math.ceil(this.rowHeight / 2), margin);
        }

        return margin;
    }

    updateResourceMargin() {
        if (this.rendered) {
            this.currentOrientation.onBeforeRowHeightChange();
            this.refreshWithTransition();
        }
    }

    changeTickSize(width) {
        ObjectHelper.assertNumber(width, 'tickSize');

        return width;
    }

    updateTickSize(width) {
        this.timeAxisViewModel.tickSize = width;
    }

    get tickSize() {
        return this.timeAxisViewModel.tickSize;
    }

    /**
     * Predefined event colors, useful in combos etc.
     * @type {String[]}
     * @category Scheduled events
     */
    static get eventColors() {
        // These are the colors available by default for Scheduler and Gantt
        // They classes are located in eventstyles.scss
        return ['red', 'pink', 'purple', 'magenta', 'violet', 'indigo', 'blue', 'cyan', 'teal', 'green', 'gantt-green', 'lime', 'yellow', 'orange', 'deep-orange', 'gray', 'light-gray'];
    }

    /**
     * Predefined event styles , useful in combos etc.
     * @type {String[]}
     * @category Scheduled events
     */
    static get eventStyles() {
        return ['plain', 'border', 'hollow', 'colored', 'line', 'dashed', 'minimal', 'rounded'];
    }

    updateEventStyle(style) {
        if (!this.isConfiguring) {
            this.refreshWithTransition();
            this.trigger('stateChange');
        }
    }

    updateEventColor(color) {
        if (!this.isConfiguring) {
            this.refreshWithTransition();
            this.trigger('stateChange');
        }
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
