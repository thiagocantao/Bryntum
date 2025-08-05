import AbstractTimeRanges from './AbstractTimeRanges.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import DateHelper from '../../Core/helper/DateHelper.js';

/**
 * @module Scheduler/feature/TimeSelection
 */

/**
 * Feature that allows selection of a time span in the time axis header. When a time span is selected in the header,
 * a {@link #event-timeSelectionChange} event is fired.
 *
 * {@inlineexample Scheduler/feature/TimeSelection.js}
 *
 * ## Configuration
 *
 * You can configure the content of the header element using the {@link #config-headerRenderer} function.
 *
 * <div class="note">Not compatible with the {@link Scheduler/feature/HeaderZoom} feature.</div>
 *
 * @extends Scheduler/feature/AbstractTimeRanges
 * @demo Scheduler/time-selection
 * @classtype timeSelection
 * @feature
 */
export default class TimeSelection extends AbstractTimeRanges {
    //region Default config

    static $name = 'TimeSelection';

    /** @hideconfigs enableResizing, showTooltip, dragTipTemplate, cls, showHeaderElements, tooltipTemplate */
    /** @hideproperties showHeaderElements, timeRanges */
    /** @hidefunctions getTipHtml, shouldRenderRange */

    static get configurable() {
        return {
            enableResizing   : true,
            showTooltip      : false,
            dragTipTemplate  : null,
            cls              : 'b-selected-time-span',
            dragHelperConfig : {
                // Data will be updated on drag, no need for DragHelper to touch the element
                skipUpdatingElement : true
            },
            resizeHelperConfig : {
                // Data will be updated on resize, no need for ResizeHelper to touch the element
                skipUpdatingElement : true
            },

            /**
             * Function used to generate the HTML content for the selected time span's header element.
             *
             * If you want to include an icon or similar to clear the selection on click, make sure to set
             * `date-ref="closeButton"` on it.
             *
             * ```javascript
             * const scheduler = new Scheduler({
             *   features : {
             *     timeSelection : {
             *       headerRenderer({ timeRange }) {
             *         return `
             *           ${DateHelper.format(timeRange.startDate, 'LL')}
             *           <div class="close" data-ref="closeButton></div>
             *         `;
             *       }
             *     }
             *   }
             * });
             * ```
             * @config {Function} headerRenderer
             * @param {Object} data Render data
             * @param {Object} data.timeRange
             * @param {Date} data.timeRange.startDate
             * @param {Date} data.timeRange.endDate
             * @returns {String}
             * @category Common
             */
            headerRenderer({ timeRange }) {
                const { dateFormat } = this.client.timeAxisViewModel.bottomHeader;

                return `<span class="b-selection-start">${DateHelper.format(timeRange.startDate, dateFormat)}</span>
                        <span class="b-selection-end">${DateHelper.format(timeRange.endDate, dateFormat)}</span>
                        <i class='b-icon b-icon-close' data-ref="closeButton" data-btip="L{Popup.close}"></i>`;
            },

            /**
             * The selected time span, which can be set using simple `startDate` and `endDate` properties
             * @prp {Object} selectedTimeSpan
             * @param {Date} selectedTimeSpan.startDate The start date of the selected time span
             * @param {Date} selectedTimeSpan.endDate The end date of the selected time span
             */
            selectedTimeSpan : null
        };
    }

    //endregion

    //region Init & destroy

    onUIReady() {
        super.onUIReady();

        this.client.ion({
            timeAxisHeaderMouseDown : 'onTimeAxisHeaderMouseDown',
            thisObj                 : this
        });
    }

    onTimeAxisHeaderMouseDown({ startDate, endDate }) {
        if (!this.disabled) {
            this.selectedTimeSpan = {
                startDate,
                endDate
            };
        }
    }

    onTimeRangeClick(event) {
        super.onTimeRangeClick(event);

        const me = this;

        if (!me.disabled && event.target.closest('.b-selected-time-span')) {
            if (event.target.matches('[data-ref="closeButton"]')) {
                me.selectedTimeSpan = null;
            }
            else {
                /**
                 * Triggered when clicking the time selection header element
                 * @event timeSelectionElementClick
                 * @on-owner
                 * @param {Scheduler.view.Scheduler} source The scheduler
                 * @param {Date} startDate The selected range start date
                 * @param {Date} endDate The selected range end date
                 * @param {Event} domEvent The raw DOM event
                 */
                me.client.trigger('timeSelectionElementClick', {
                    ...me.selectedTimeSpan,
                    domEvent : event
                });
            }
        }
    }

    updateFromCoords(x, y, width, height) {
        const
            { client } = this,
            size       = client.isHorizontal ? width : height;

        let start = client.isHorizontal ? x : y;

        if (client.rtl && client.isHorizontal) {
            start = Math.max(client.timeAxisViewModel.totalSize - start - size, 0);
        }

        const
            startDate = client.getDateFromCoord({ coord : start, roundingMethod : 'round', ignoreRTL : true }),
            endDate   = client.getDateFromCoord({ coord : start + size, roundingMethod : 'round', ignoreRTL : true });

        this.selectedTimeSpan = {
            startDate : DateHelper.max(startDate, client.startDate),
            endDate   : DateHelper.min(endDate, client.endDate)
        };
    }

    onResizeStart({ source }) {
        const
            {
                timeAxisViewModel,
                timeResolution
            }                    = this.client,
            resolutionDurationMS = DateHelper.asMilliseconds(timeResolution.increment, timeResolution.unit);

        // Prevent resizing smaller than one resolution increment
        this.resize.minWidth = timeAxisViewModel.getDistanceForDuration(resolutionDurationMS);
    }

    onResizeDrag({ context }) {
        this.updateFromCoords(
            context.newX,
            context.newY,
            context.newWidth ?? context.elementWidth,
            context.newHeight ?? context.elementHeight
        );
    }

    onDragStart(event) {
        super.onDragStart(event);

        const { context } = event;

        context.elementWidth = context.element.offsetWidth;
        context.elementHeight = context.element.offsetHeight;
    }

    onDrag({ context }) {
        this.updateFromCoords(
            context.newX,
            context.newY,
            context.elementWidth,
            context.elementHeight
        );
    }

    onDrop() {
        this.client.element.classList.remove('b-dragging-timerange');
    }

    onDragReset() {
        this.refresh();
    }

    get timeRanges() {
        return this.selectedTimeSpan ? [this.selectedTimeSpan] : [];
    }

    // Always render the selection
    shouldRenderRange() {
        return true;
    }

    changeSelectedTimeSpan(timeSpan) {
        if (timeSpan) {
            timeSpan.id = 'time-selection';
        }

        return timeSpan;
    }

    updateSelectedTimeSpan(timeSpan) {
        if (!this.isConfiguring) {
            this.refresh();
        }

        /**
         * Triggered when time selection changes
         * @event timeSelectionChange
         * @on-owner
         * @param {Scheduler.view.Scheduler} source The scheduler
         * @param {Date} startDate The selected range start date, or `undefined`
         * @param {Date} endDate The selected range end date, or `undefined`
         */
        this.client.trigger('timeSelectionChange', {
            ...timeSpan ?? {}
        });
    }
}

GridFeatureManager.registerFeature(TimeSelection, false, 'Scheduler');
