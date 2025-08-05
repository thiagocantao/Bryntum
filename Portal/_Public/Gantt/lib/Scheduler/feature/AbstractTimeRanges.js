import DragHelper from '../../Core/helper/DragHelper.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import DomSync from '../../Core/helper/DomSync.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';
import ResizeHelper from '../../Core/helper/ResizeHelper.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import Delayable from '../../Core/mixin/Delayable.js';
import InstancePlugin from '../../Core/mixin/InstancePlugin.js';
import Rectangle from '../../Core/helper/util/Rectangle.js';
import Tooltip from '../../Core/widget/Tooltip.js';
import ClockTemplate from '../tooltip/ClockTemplate.js';

/**
 * @module Scheduler/feature/AbstractTimeRanges
 */

/**
 * Abstract base class, you should not use this class directly.
 * @abstract
 * @mixes Core/mixin/Delayable
 * @extends Core/mixin/InstancePlugin
 */
export default class AbstractTimeRanges extends InstancePlugin.mixin(Delayable) {
    //region Config

    /**
     * Fired on the owning Scheduler when a click happens on a time range header element
     * @event timeRangeHeaderClick
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
     * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler when a double click happens on a time range header element
     * @event timeRangeHeaderDblClick
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
     * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
     * @param {MouseEvent} domEvent Browser event
     */

    /**
     * Fired on the owning Scheduler when a right click happens on a time range header element
     * @event timeRangeHeaderContextMenu
     * @on-owner
     * @param {Scheduler.view.Scheduler} source Scheduler instance
     * @param {Scheduler.model.TimeSpan} timeRangeRecord The record
     * @param {MouseEvent} event DEPRECATED 5.3.0 Use `domEvent` instead
     * @param {MouseEvent} domEvent Browser event
     */

    static get defaultConfig() {
        return {
            // CSS class to apply to range elements
            rangeCls : 'b-sch-range',

            // CSS class to apply to line elements (0-duration time range)
            lineCls : 'b-sch-line',

            /**
             * Set to `true` to enable dragging and resizing of range elements in the header. Only relevant when
             * {@link #config-showHeaderElements} is `true`.
             * @config {Boolean}
             * @default
             * @category Common
             */
            enableResizing : false,

            /**
             * A Boolean specifying whether to show tooltip while resizing range elements, or a
             * {@link Core.widget.Tooltip} config object which is applied to the tooltip
             * @config {Boolean|TooltipConfig}
             * @default
             * @category Common
             */
            showTooltip : true,

            /**
             * Template used to generate the tooltip contents when hovering a time range header element.
             * ```
             * const scheduler = new Scheduler({
             *   features : {
             *     timeRanges : {
             *       tooltipTemplate({ timeRange }) {
             *         return `${timeRange.name}`
             *       }
             *     }
             *   }
             * });
             * ```
             * @config {Function} tooltipTemplate
             * @param {Object} data Tooltip data
             * @param {Scheduler.model.TimeSpan} data.timeRange
             * @category Common
             */
            tooltipTemplate : null,

            dragTipTemplate : data => `
                <div class="b-sch-tip-${data.valid ? 'valid' : 'invalid'}">
                    <div class="b-sch-tip-name">${StringHelper.encodeHtml(data.name) || ''}</div>
                    ${data.startClockHtml}
                    ${data.endClockHtml || ''}
                </div>
            `,

            baseCls : 'b-sch-timerange',

            /**
             * Function used to generate the HTML content for a time range header element.
             * ```
             * const scheduler = new Scheduler({
             *   features : {
             *     timeRanges : {
             *       headerRenderer({ timeRange }) {
             *         return `${timeRange.name}`
             *       }
             *     }
             *   }
             * });
             * ```
             * @config {Function} headerRenderer
             * @param {Object} data Render data
             * @param {Scheduler.model.TimeSpan} data.timeRange
             * @category Common
             */
            headerRenderer : null,

            /**
             * Function used to generate the HTML content for a time range body element.
             * ```
             * const scheduler = new Scheduler({
             *   features : {
             *     timeRanges : {
             *       bodyRenderer({ timeRange }) {
             *         return `${timeRange.name}`
             *       }
             *     }
             *   }
             * });
             * ```
             * @config {Function} bodyRenderer
             * @param {Object} data Render data
             * @param {Scheduler.model.TimeSpan} data.timeRange
             * @category Common
             */
            bodyRenderer : null,

            // a unique cls used by subclasses to get custom styling of the elements rendered
            cls : null,

            narrowThreshold : 80
        };
    }

    static configurable = {
        /**
         * Set to `false` to not render range elements into the time axis header
         * @prp {Boolean}
         * @default
         * @category Common
         */
        showHeaderElements : true
    };

    // Plugin configuration. This plugin chains some functions in Grid.
    static pluginConfig = {
        chain : [
            'onPaint',
            'populateTimeAxisHeaderMenu',
            'onSchedulerHorizontalScroll',
            'afterScroll',
            'onInternalResize'
        ]
    };

    //endregion

    //region Init & destroy

    construct(client, config) {
        const me = this;

        super.construct(client, config);

        if (client.isVertical) {
            client.ion({
                renderRows : me.onUIReady,
                thisObj    : me,
                once       : true
            });
        }

        // Add a unique cls used by subclasses to get custom styling of the elements rendered
        // This makes sure that each class only removed its own elements from the DOM
        me.cls = me.cls || `b-sch-${me.constructor.$$name.toLowerCase()}`;

        me.baseSelector = `.${me.baseCls}.${me.cls}`;

        // header elements are required for interaction
        if (me.enableResizing) {
            me.showHeaderElements = true;
        }
    }

    doDestroy() {
        const me = this;

        me.detachListeners('timeAxisViewModel');
        me.detachListeners('timeAxis');

        me.clockTemplate?.destroy();
        me.tip?.destroy();

        me.drag?.destroy();
        me.resize?.destroy();

        super.doDestroy();
    }

    doDisable(disable) {
        this.renderRanges();

        super.doDisable(disable);
    }

    setupTimeAxisViewModelListeners() {
        const me = this;

        me.detachListeners('timeAxisViewModel');
        me.detachListeners('timeAxis');

        me.client.timeAxisViewModel.ion({
            name    : 'timeAxisViewModel',
            update  : 'onTimeAxisViewModelUpdate',
            thisObj : me
        });

        me.client.timeAxis.ion({
            name          : 'timeAxis',
            includeChange : 'renderRanges',
            thisObj       : me
        });

        me.updateLineBuffer();
    }

    onUIReady() {
        const
            me         = this,
            { client } = me;

        // If timeAxisViewModel is swapped, re-setup listeners to new instance
        client.ion({
            timeAxisViewModelChange : me.setupTimeAxisViewModelListeners,
            thisObj                 : me
        });

        me.setupTimeAxisViewModelListeners();

        if (!client.hideHeaders) {
            if (me.headerContainerElement) {
                EventHelper.on({
                    click       : me.onTimeRangeClick,
                    dblclick    : me.onTimeRangeClick,
                    contextmenu : me.onTimeRangeClick,
                    delegate    : me.baseSelector,
                    element     : me.headerContainerElement,
                    thisObj     : me
                });
            }

            if (me.enableResizing) {

                me.drag = DragHelper.new({
                    name               : 'rangeDrag',
                    lockX              : client.isVertical,
                    lockY              : client.isHorizontal,
                    constrain          : true,
                    outerElement       : me.headerContainerElement,
                    targetSelector     : `${me.baseSelector}`,
                    isElementDraggable : (el, event) => !client.readOnly && me.isElementDraggable(el, event),
                    rtlSource          : client,

                    internalListeners : {
                        dragstart : 'onDragStart',
                        drag      : 'onDrag',
                        drop      : 'onDrop',
                        reset     : 'onDragReset',
                        abort     : 'onInvalidDrop',
                        thisObj   : me
                    }
                }, me.dragHelperConfig);

                me.resize = ResizeHelper.new({
                    direction          : client.mode,
                    targetSelector     : `${me.baseSelector}.b-sch-range`,
                    outerElement       : me.headerContainerElement,
                    isElementResizable : (el, event) => !el.matches('.b-dragging,.b-readonly') && !event.target.matches('.b-fa'),
                    internalListeners  : {
                        resizestart : 'onResizeStart',
                        resizing    : 'onResizeDrag',
                        resize      : 'onResize',
                        cancel      : 'onInvalidResize',
                        reset       : 'onResizeReset',
                        thisObj     : me
                    }
                }, me.resizeHelperConfig);
            }
        }

        me.renderRanges();

        if (me.tooltipTemplate) {
            me.hoverTooltip = new Tooltip({
                forElement : me.headerContainerElement,
                getHtml({ activeTarget }) {
                    const timeRange = me.resolveTimeRangeRecord(activeTarget);

                    return me.tooltipTemplate({ timeRange });
                },
                forSelector : '.' + me.baseCls + (me.cls ? '.' + me.cls : '')
            });
        }
    }

    //endregion

    //region Draw

    refresh() {
        this._timeRanges = null;
        this.renderRanges();
    }

    getDOMConfig(startDate, endDate) {
        const
            me            = this,
            bodyConfigs   = [],
            headerConfigs = [];

        if (!me.disabled) {
            // clear label rotation map cache here, used to prevent height calculations for every timeRange entry to
            // speed up using recurrences
            me._labelRotationMap = {};

            for (const range of me.timeRanges) {
                const result = me.renderRange(range, startDate, endDate);
                if (result) {
                    bodyConfigs.push(result.bodyConfig);
                    headerConfigs.push(result.headerConfig);
                }
            }
        }

        return [bodyConfigs, headerConfigs];
    }

    renderRanges() {
        const
            me                   = this,
            { client }           = me,
            { foregroundCanvas } = client;

        // Scheduler/Gantt might not yet be rendered
        if (foregroundCanvas && client.isPainted && !client.timeAxisSubGrid.collapsed) {
            const
                { headerContainerElement }   = me,
                updatedBodyElements          = [],
                [bodyConfigs, headerConfigs] = me.getDOMConfig();

            if (!me.bodyCanvas) {
                me.bodyCanvas = DomHelper.createElement({
                    className     : `b-timeranges-canvas ${me.cls}-canvas`,
                    parent        : foregroundCanvas,
                    retainElement : true
                });
            }

            DomSync.sync({
                targetElement : me.bodyCanvas,
                childrenOnly  : true,
                domConfig     : {
                    children    : bodyConfigs,
                    syncOptions : {
                        releaseThreshold : 0,
                        syncIdField      : 'id'
                    }
                },
                callback : me.showHeaderElements ? null : ({
                    targetElement,
                    action
                }) => {
                    // Might need to rotate label when not showing header elements
                    if (action === 'reuseElement' || action === 'newElement' || action === 'reuseOwnElement') {
                        // Collect all here, to not force reflows in the middle of syncing
                        updatedBodyElements.push(targetElement);
                    }
                }
            });

            if (me.showHeaderElements && !me.headerCanvas) {
                me.headerCanvas = DomHelper.createElement({
                    className     : `${me.cls}-canvas`,
                    parent        : headerContainerElement,
                    retainElement : true
                });
            }

            if (me.headerCanvas) {
                DomSync.sync({
                    targetElement : me.headerCanvas,
                    childrenOnly  : true,
                    domConfig     : {
                        children    : headerConfigs,
                        syncOptions : {
                            releaseThreshold : 0,
                            syncIdField      : 'id'
                        }
                    }
                });
            }

            // Rotate labels last, to not force reflows. First check if rotation is needed
            for (const bodyElement of updatedBodyElements) {
                me.cacheRotation(bodyElement.elementData.timeRange, bodyElement);
            }

            // Then apply rotation
            for (const bodyElement of updatedBodyElements) {
                me.applyRotation(bodyElement.elementData.timeRange, bodyElement);
            }
        }
    }

    // Implement in subclasses
    get timeRanges() {
        return [];
    }

    /**
     * Based on this method result the feature decides whether the provided range should
     * be rendered or not.
     * The method checks that the range intersects the current viewport.
     *
     * Override the method to implement your custom range rendering vetoing logic.
     * @param {Scheduler.model.TimeSpan} range Range to render.
     * @param {Date} [startDate] Specifies view start date. Defaults to view visible range start
     * @param {Date} [endDate] Specifies view end date. Defaults to view visible range end
     * @returns {Boolean} `true` if the range should be rendered and `false` otherwise.
     */
    shouldRenderRange(
        range,
        startDate = this.client.visibleDateRange.startDate,
        endDate   = this.client.visibleDateRange.endDate
    ) {
        const
            { timeAxis }                                             = this.client,
            { startDate : rangeStart, endDate : rangeEnd, duration } = range;

        return Boolean(rangeStart && (timeAxis.isContinuous || timeAxis.isTimeSpanInAxis(range)) && DateHelper.intersectSpans(
            startDate,
            endDate,
            rangeStart,
            // Lines are included longer, to make sure label does not disappear
            duration ? rangeEnd : DateHelper.add(rangeStart, this._lineBufferDurationMS)
        ));
    }

    getRangeDomConfig(timeRange, minDate, maxDate, relativeTo = 0) {
        const
            me         = this,
            { client } = me,
            { rtl }    = client,
            startPos   = client.getCoordinateFromDate(DateHelper.max(timeRange.startDate, minDate), {
                respectExclusion : true
            }) - relativeTo,
            endPos     = timeRange.endDate ? client.getCoordinateFromDate(DateHelper.min(timeRange.endDate, maxDate), {
                respectExclusion : true,
                isEnd            : true
            }) - relativeTo : startPos,
            size       = Math.abs(endPos - startPos),
            isRange    = size > 0,
            translateX = rtl ? `calc(${startPos}px - 100%)` : `${startPos}px`;

        return {
            className : {
                [me.baseCls]     : 1,
                [me.cls]         : me.cls,
                [me.rangeCls]    : isRange,
                [me.lineCls]     : !isRange,
                [timeRange.cls]  : timeRange.cls,
                'b-narrow-range' : isRange && size < me.narrowThreshold,
                'b-readonly'     : timeRange.readOnly,
                'b-rtl'          : rtl
            },
            dataset : {
                id : timeRange.id
            },
            elementData : {
                timeRange
            },
            style : client.isVertical
                ? `transform: translateY(${translateX}); ${isRange ? `height:${size}px` : ''};`
                : `transform: translateX(${translateX}); ${isRange ? `width:${size}px` : ''};`
        };
    }

    renderRange(timeRange, startDate, endDate) {
        const
            me           = this,
            { client }   = me,
            { timeAxis } = client;

        if (me.shouldRenderRange(timeRange, startDate, endDate) && timeAxis.startDate) {
            const
                config     = me.getRangeDomConfig(timeRange, timeAxis.startDate, timeAxis.endDate),
                icon       = timeRange.iconCls && StringHelper.xss`<i class="${timeRange.iconCls}"></i>`,
                name       = timeRange.name && StringHelper.encodeHtml(timeRange.name),
                labelTpl   = (name || icon) ? `<label>${icon || ''}${name || '&nbsp;'}</label>` : '',
                bodyConfig = {
                    ...config,
                    style : config.style + (timeRange.style || ''),
                    html  : me.bodyRenderer ? me.bodyRenderer({ timeRange }) : (me.showHeaderElements && !me.showLabelInBody ? '' : labelTpl)
                };

            let headerConfig;

            if (me.showHeaderElements) {
                headerConfig = {
                    ...config,
                    html : (me.headerRenderer ? me.headerRenderer({ timeRange }) : (me.showLabelInBody ? '' : labelTpl))
                };
            }

            return { bodyConfig, headerConfig };
        }
    }

    // Cache label rotation to not have to calculate for each occurrence when using recurring timeranges
    cacheRotation(range, bodyElement) {
        // Lines have no label. Do not check label content to do not force DOM layout!
        if ((!range.iconCls && !range.name) || !range.duration) {
            return;
        }

        const label = bodyElement.firstElementChild;

        if (label && !range.recurringTimeSpan) {
            this._labelRotationMap[range.id] = this.client.isVertical
                ? label.offsetHeight < bodyElement.offsetHeight
                : label.offsetWidth > bodyElement.offsetWidth;
        }
    }

    applyRotation(range, bodyElement) {
        const rotate = this._labelRotationMap[range.recurringTimeSpan?.id ?? range.id];

        bodyElement.firstElementChild?.classList.toggle('b-vertical', Boolean(rotate));
    }

    getBodyElementByRecord(idOrRecord) {
        const id = typeof idOrRecord === 'string' ? idOrRecord : idOrRecord?.id;

        return id != null && DomSync.getChild(this.bodyCanvas, id);
    }

    // Implement in subclasses
    resolveTimeRangeRecord(el) {}

    get headerContainerElement() {
        const
            me                                       = this,
            { isVertical, timeView, timeAxisColumn } = me.client;

        if (!me._headerContainerElement) {
            // Render into the subGrid´s header element or the vertical timeaxis depending on mode
            if (isVertical && timeView.element) {
                me._headerContainerElement = timeView.element.parentElement;
            }
            else if (!isVertical) {
                me._headerContainerElement = timeAxisColumn.element;
            }
        }

        return me._headerContainerElement;
    }

    //endregion

    //region Settings

    get showHeaderElements() {
        return !this.client.hideHeaders && this._showHeaderElements;
    }

    updateShowHeaderElements(show) {
        const { client } = this;

        if (!this.isConfiguring) {
            client.element.classList.toggle('b-sch-timeranges-with-headerelements', Boolean(show));

            this.renderRanges();
        }
    }

    //endregion

    //region Menu items

    /**
     * Adds menu items for the context menu, and may mutate the menu configuration.
     * @param {Object} options Contains menu items and extra data retrieved from the menu target.
     * @param {Grid.column.Column} options.column Column for which the menu will be shown
     * @param {Object<String,MenuItemConfig|Boolean|null>} options.items A named object to describe menu items
     * @internal
     */
    populateTimeAxisHeaderMenu({ column, items }) {}

    //endregion

    //region Events & hooks

    onPaint({ firstPaint }) {
        if (firstPaint && this.client.isHorizontal) {
            this.onUIReady();
        }
    }

    onSchedulerHorizontalScroll() {
        // Don't need a refresh, ranges are already available. Just need to draw those now in view
        this.client.isHorizontal && this.renderRanges();
    }

    afterScroll() {
        this.client.isVertical && this.renderRanges();
    }

    updateLineBuffer() {
        const { timeAxisViewModel } = this.client;
        // Lines have no duration, but we want them to be visible longer for the label to not suddenly disappear.
        // We use a 300px buffer for that, recalculated as an amount of ms
        this._lineBufferDurationMS = timeAxisViewModel.getDateFromPosition(300) - timeAxisViewModel.getDateFromPosition(0);
    }

    onInternalResize(element, newWidth, newHeight, oldWidth, oldHeight) {
        if (this.client.isVertical && oldHeight !== newHeight) {
            this.renderRanges();
        }
    }

    onTimeAxisViewModelUpdate() {
        this.updateLineBuffer();

        this.refresh();
    }

    onTimeRangeClick(event) {
        const timeRangeRecord = this.resolveTimeRangeRecord(event.target);

        this.client.trigger(`timeRangeHeader${StringHelper.capitalize(event.type)}`, { event, domEvent : event, timeRangeRecord });
    }

    //endregion

    //region Drag drop

    showTip(context) {
        const me = this;

        if (me.showTooltip) {
            me.clockTemplate = new ClockTemplate({
                scheduler : me.client
            });

            me.tip = new Tooltip(ObjectHelper.assign({
                id                       : `${me.client.id}-time-range-tip`,
                cls                      : 'b-interaction-tooltip',
                align                    : 'b-t',
                autoShow                 : true,
                updateContentOnMouseMove : true,
                forElement               : context.element,
                getHtml                  : () => me.getTipHtml(context.record, context.element)
            }, me.showTooltip));
        }
    }

    destroyTip() {
        if (this.tip) {
            this.tip.destroy();
            this.tip = null;
        }
    }

    isElementDraggable(el) {
        el = el.closest(this.baseSelector + ':not(.b-resizing):not(.b-readonly)');

        return el && !el.classList.contains('b-over-resize-handle');
    }

    onDragStart({ context }) {
        const { client, drag } = this;

        if (client.isVertical) {
            drag.minY = 0;
            // Moving the range, you can drag the start marker down until the end of the range hits the time axis end
            drag.maxY = client.timeAxisViewModel.totalSize - context.element.offsetHeight;
            // Setting min/max for X makes drag right of the header valid, but visually still constrained vertically
            drag.minX = 0;
            drag.maxX = Number.MAX_SAFE_INTEGER;
        }
        else {
            drag.minX = 0;
            // Moving the range, you can drag the start marker right until the end of the range hits the time axis end
            drag.maxX = client.timeAxisViewModel.totalSize - context.element.offsetWidth;
            // Setting min/max for Y makes drag below header valid, but visually still constrained horizontally
            drag.minY = 0;
            drag.maxY = Number.MAX_SAFE_INTEGER;
        }

        client.element.classList.add('b-dragging-timerange');
    }

    onDrop({ context }) {
        this.client.element.classList.remove('b-dragging-timerange');
    }

    onInvalidDrop() {
        this.drag.reset();
        this.client.element.classList.remove('b-dragging-timerange');

        this.destroyTip();
    }

    updateDateIndicator({ startDate, endDate }) {
        const
            me             = this,
            { tip }        = me,
            endDateElement = tip.element.querySelector('.b-sch-tooltip-enddate');

        me.clockTemplate.updateDateIndicator(tip.element, startDate);
        endDateElement && me.clockTemplate.updateDateIndicator(endDateElement, endDate);
    }

    onDrag({ context }) {
        const
            me         = this,
            { client } = me,
            box        = Rectangle.from(context.element),
            startPos   = box.getStart(client.rtl, client.isHorizontal),
            endPos     = box.getEnd(client.rtl, client.isHorizontal),
            startDate  = client.getDateFromCoordinate(startPos, 'round', false),
            endDate    = client.getDateFromCoordinate(endPos, 'round', false);

        me.updateDateIndicator({ startDate, endDate });
    }

    onDragReset() {}

    // endregion

    // region Resize

    onResizeStart() {}

    onResizeDrag() {}

    onResize() {}

    onInvalidResize() {}

    onResizeReset() {}

    //endregion

    //region Tooltip

    /**
     * Generates the html to display in the tooltip during drag drop.
     *
     */
    getTipHtml(record, element) {
        const
            me         = this,
            { client } = me,
            box        = Rectangle.from(element),
            startPos   = box.getStart(client.rtl, client.isHorizontal),
            endPos     = box.getEnd(client.rtl, client.isHorizontal),
            startDate  = client.getDateFromCoordinate(startPos, 'round', false),
            endDate    = record.endDate && client.getDateFromCoordinate(endPos, 'round', false),
            startText  = client.getFormattedDate(startDate),
            endText    = endDate && client.getFormattedEndDate(endDate, startDate);

        return me.dragTipTemplate({
            name           : record.name || '',
            startDate,
            endDate,
            startText,
            endText,
            startClockHtml : me.clockTemplate.template({
                date : startDate,
                text : startText,
                cls  : 'b-sch-tooltip-startdate'
            }),
            endClockHtml : endText && me.clockTemplate.template({
                date : endDate,
                text : endText,
                cls  : 'b-sch-tooltip-enddate'
            })
        });
    }

    //endregion
}
