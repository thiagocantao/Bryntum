import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import GridSplit from '../../Grid/feature/Split.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import DomHelper from '../../Core/helper/DomHelper.js';

/**
 * @module Scheduler/feature/Split
 */

const
    startScrollOptions = Object.freeze({
        animate : false,
        block   : 'start'
    }),
    endScrollOptions = Object.freeze({
        animate : false,
        block   : 'end'
    });

/**
 * This feature allows splitting the Scheduler into multiple views, either by using the cell context menu, or
 * programmatically by calling {@link #function-split split()}.
 *
 * {@inlineexample Scheduler/feature/Split.js}
 *
 * See {@link Grid/feature/Split} for more details.
 *
 * ## Scheduler specifics
 *
 * - Scheduler allows splitting by dates, either programmatically or by using the context menu.
 * - Scheduler prevents splitting in the grid part using the context menu.
 * - The `eventDrag` feature will automatically be configured to allow dragging between the clones (by setting
 *   `constrainDragToTimeline` to `false`).
 * - Splitting is not supported in vertical mode.
 *
 * {@note}
 * Note that although Gantt is related to Scheduler, splitting is not supported in Gantt.
 * {/@note}
 *
 * @classtype split
 * @feature
 * @extends Grid/feature/Split
 *
 * @typings Grid.feature.Split -> Grid.feature.GridSplit
 */
export default class Split extends GridSplit {
    static $name = 'Split';

    static configurable = {
        /**
         * Properties whose changes should be relayed to sub-views at runtime.
         *
         * Supply an object with property names as keys, and a truthy value to relay the change, or a falsy value to not
         * relay it. The object will be merged with the default values.
         *
         * In addition to the properties relayed by Grid, Scheduler also relays these:
         * * {@link Scheduler/view/Scheduler#property-barMargin}
         * * {@link Scheduler/view/Scheduler#property-eventColor}
         * * {@link Scheduler/view/Scheduler#property-eventStyle}
         * * {@link Scheduler/view/Scheduler#property-eventLayout}
         * * {@link Scheduler/view/Scheduler#property-fillTicks}
         * * {@link Scheduler/view/Scheduler#property-resourceMargin}
         * * {@link Scheduler/view/Scheduler#property-snap}
         * * {@link Scheduler/view/Scheduler#property-tickSize}
         *
         * Example of supplying a custom set of properties to relay:
         * ```javascript
         * const scheduler = new Scheduler({
         *     features : {
         *         split : {
         *             relayProperties : {
         *                 barMargin : false, // Do not relay barMargin changes
         *                 myConfig : true   // Relay changes to the myConfig property
         *             }
         *         }
         *     }
         * }
         * ```
         * @config {Object<String,Boolean>}
         */
        relayProperties : {
            value : {
                barMargin      : 1,
                eventColor     : 1,
                eventStyle     : 1,
                eventLayout    : 1,
                fillTicks      : 1,
                resourceMargin : 1,
                snap           : 1,
                tickSize       : 1
            },
            $config : {
                merge : 'merge'
            }
        }
    };

    static pluginConfig = {
        chain  : ['populateCellMenu', 'populateScheduleMenu', 'afterConfigChange', 'afterAddListener', 'afterRemoveListener'],
        assign : ['split', 'unsplit', 'subViews', 'syncSplits']
    };

    getClientConfig(appendTo, order, options, config) {
        const
            { client }   = this,
            clientConfig = super.getClientConfig(appendTo, order, options, config);

        // Grid demo in Schedulers docs
        if (!client.isSchedulerBase && !client.isSchedulerProBase && !client.isGanttBase) {
            return clientConfig;
        }

        // Use project instead of store / inline data
        delete clientConfig.store;
        delete clientConfig.events;
        delete clientConfig.resources;
        delete clientConfig.assignments;
        delete clientConfig.dependencies;
        delete clientConfig.resourceTimeRanges;
        delete clientConfig.timeRanges;
        clientConfig.project = client.project;
        clientConfig.assignmentStore = clientConfig.project.assignmentStore;
        clientConfig.crudManager = client.crudManager;
        clientConfig.viewPreset = client.viewPreset;
        clientConfig.startDate = client.startDate;
        clientConfig.endDate = client.endDate;
        clientConfig.selectedCollection = client.selectedCollection;

        clientConfig.useInitialAnimation = false;

        // Allow dragging between the clones, unless drag is explicitly turned off
        if (clientConfig.features?.eventDrag !== false) {
            ObjectHelper.setPath(clientConfig, 'features.eventDrag.constrainDragToTimeline', false);
            client.features.eventDrag.constrainDragToTimeline = false;
        }

        return clientConfig;
    }

    unsplitCleanup(options) {
        // Force time axis to get correct width
        this.client.timeAxisSubGrid.onElementResize();
    }

    /**
     * Fired when splitting the Scheduler.
     * @event split
     * @param {Scheduler.view.SchedulerBase[]} subViews The sub views created by the split
     * @param {Object} options The options passed to the split call
     * @param {'horizontal'|'vertical'|'both'} options.direction The direction of the split
     * @param {Grid.column.Column} options.atColumn The column to split at
     * @param {Core.data.Model} options.atRecord The record to split at
     * @param {Date} options.atDate The date to split at
     * @on-owner
     */

    processOptions(options) {
        const { client } = this;

        let { atDate, atRecord } = options;

        // Check if center is a date if not given a specific split point
        if (options.direction !== 'horizontal' && !options.atColumn && !options.atRecord && !atDate) {
            const
                bounds = Rectangle.from(client.element),
                date   = client.getDateFromCoordinate(bounds.center.x, 'round', false);

            // If it is a date, split at it
            if (date) {
                options.atDate = atDate = date;
                options.atColumn = client.timeAxisColumn;
            }
        }

        // Asked to split at a date, determine split sizes
        if (atDate) {
            if (!options.direction) {
                options.direction = atRecord ? 'both' : 'vertical';
            }

            options.splitX = client.getCoordinateFromDate(atDate, { ignoreRTL : true }) - client.timeAxisSubGrid.scrollable.x;

            if (client.rtl) {
                options.splitX += client._bodyRectangle.width - Rectangle.from(client.timeAxisSubGridElement).right + DomHelper.scrollBarWidth;
            }
            else {
                options.splitX += Rectangle.from(client.timeAxisSubGridElement, client.bodyContainer).left;
            }

            options.remainingWidth = Rectangle.from(client.element).width - options.splitX;
        }

        return super.processOptions(options);
    }

    /**
     * Split the scheduler into two or four parts.
     *
     * - Splits into two when passed `direction : 'vertical'`, `direction : 'horizontal'` or `atColumn`, `atRecord` or
     *   `atDate`.
     * - Splits into four when passed `direction : 'both'` or `atColumn`/`atDate` and `atRecord`.
     *
     * ```javascript
     * // Split horizontally (at the row in the center of the scheduler)
     * await scheduler.split({ direction : 'horizontal' });
     *
     * // Split both ways by a specific date and resource
     * await schedule.split({
     *    atRecord : scheduler.resourceStore.getById(10),
     *    atDate   : new Date(2023, 5, 8)
     * });
     * ```
     *
     * To return to a single scheduler, call {@link #function-unsplit}.
     *
     * Note that this function is callable directly on the scheduler instance.
     *
     * @param {Object} [options] Split options
     * @param {'vertical'|'horizontal'|'both'} [options.direction] Split direction, 'vertical', 'horizontal' or 'both'.
     * Not needed when passing `atColumn` or `atRecord`.
     * @param {Date} [options.atDate] Date to split at. Has to be within the time axis
     * @param {Grid.column.Column} [options.atColumn] Column to split at
     * @param {Core.data.Model} [options.atRecord] Record to split at
     * @returns {Promise} Resolves when split is complete, and subviews are scrolled to the correct position.
     * @async
     * @on-owner
     */
    async split(options = {}) {
        const { client } = this;

        if (client.isVertical) {
            throw new Error('Splitting is not supported in vertical mode');
        }

        const result = await super.split(options);

        if (result && options.atDate) {
            await Promise.all([
                // Scroll prev tick into view on left side
                result[0].scrollToDate(DateHelper.add(options.atDate, -1, client.timeAxis.unit), endScrollOptions),
                // And current tick into view on right side
                result[1].scrollToDate(options.atDate, startScrollOptions)
            ]);
        }

        // For whatever reason, the above scroll does not always refresh the timeaxis correctly, have not been able
        // to track it down so triggering a refresh manually
        client.timeAxisSubGrid.scrollable.x += 0.5;

        return result;
    }

    //region Context menu

    // Overwritten to prevent splitting in locked region
    populateCellMenu() {}

    populateScheduleMenu({ items, date, record }) {
        const
            me            = this,
            { isSplit }   = me,
            { splitFrom } = me.client;

        // Splitting not supported in vertical mode
        if (!me.disabled && me.client.isHorizontal) {
            items.splitSchedule = {
                text        : 'L{split}',
                localeClass : me,
                icon        : 'b-icon b-icon-split-vertical',
                weight      : 500,
                separator   : true,
                hidden      : isSplit || splitFrom,

                menu : {
                    splitHorizontally : {
                        text        : 'L{horizontally}',
                        icon        : 'b-icon b-icon-split-horizontal',
                        localeClass : me,
                        weight      : 100,
                        onItem() {
                            me.split({ atRecord : record, direction : 'horizontal' }).then();
                        }
                    },
                    splitVertically : {
                        text        : 'L{vertically}',
                        icon        : 'b-icon b-icon-split-vertical',
                        localeClass : me,
                        weight      : 200,
                        onItem() {
                            me.split({ atDate : date }).then();
                        }
                    },
                    splitBoth : {
                        text        : 'L{both}',
                        icon        : 'b-icon b-icon-split-both',
                        localeClass : me,
                        weight      : 300,
                        onItem() {
                            me.split({ atDate : date, atRecord : record, direction : 'both' }).then();
                        }
                    }
                }
            };

            items.unsplitSchedule = {
                text        : 'L{unsplit}',
                localeClass : me,
                icon        : 'b-icon b-icon-clear',
                hidden      : !(isSplit || splitFrom),
                weight      : 550,
                onItem() {
                    (splitFrom || me).unsplit();
                }
            };
        }
    }

    //endregion
}

GridFeatureManager.registerFeature(Split, false, 'Scheduler');
