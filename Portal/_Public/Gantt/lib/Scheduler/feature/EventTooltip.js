import TooltipBase from './base/TooltipBase.js';
import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import { parseAlign } from '../../Core/helper/util/Rectangle.js';

/**
 * @module Scheduler/feature/EventTooltip
 */

// Alignment offsets to clear any dependency terminals depending on whether
// the tooltip is aligned top/bottom (1) or left/right (2) as parsed from the
// align string by Rectangle's parseAlign
const
    zeroOffset = [0, 0],
    depOffset  = [
        null, [0, 10], [10, 0]
    ];

/**
 * Displays a tooltip when hovering events. The template used to render the tooltip can be customized, see {@link #config-template}.
 * Config options are also applied to the tooltip shown, see {@link Core.widget.Tooltip} for available options.
 *
 * ## Showing local data
 * To show a basic "local" tooltip (with data available in the Event record) upon hover:
 * ```javascript
 * new Scheduler({
 *   features : {
 *     eventTooltip : {
 *         // Tooltip configs can be used here
 *         align : 'l-r' // Align left to right,
 *         // A custom HTML template
 *         template : data => `<dl>
 *           <dt>Assigned to:</dt>
 *              <dt>Time:</dt>
 *              <dd>
 *                  ${DateHelper.format(data.eventRecord.startDate, 'LT')} - ${DateHelper.format(data.eventRecord.endDate, 'LT')}
 *              </dd>
 *              ${data.eventRecord.get('note') ? `<dt>Note:</dt><dd>${data.eventRecord.note}</dd>` : ''}
 *
 *              ${data.eventRecord.get('image') ? `<dt>Image:</dt><dd><img class="image" src="${data.eventRecord.get('image')}"/></dd>` : ''}
 *          </dl>`
 *     }
 *   }
 * });
 * ```
 *
 * ## Showing remotely loaded data
 * Loading remote data into the event tooltip is easy. Simply use the {@link #config-template} and return a Promise which yields the content to show.
 * ```javascript
 * new Scheduler({
 *   features : {
 *     eventTooltip : {
 *        template : ({ eventRecord }) => AjaxHelper.get(`./fakeServer?name=${eventRecord.name}`).then(response => response.text())
 *     }
 *   }
 * });
 * ```
 *
 * This feature is **enabled** by default
 *
 * By default, the tooltip {@link Core.widget.Widget#config-scrollAction realigns on scroll}
 * meaning that it will stay aligned with its target should a scroll interaction make the target move.
 *
 * If this is causing performance issues in a Scheduler, such as if there are many dozens of events
 * visible, you can configure this feature with `scrollAction: 'hide'`. This feature's configuration is
 * applied to the tooltip, so that will mean that the tooltip will hide if its target is moved by a
 * scroll interaction.
 *
 * @extends Scheduler/feature/base/TooltipBase
 * @demo Scheduler/basic
 * @inlineexample Scheduler/feature/EventTooltip.js
 * @classtype eventTooltip
 * @feature
 */
export default class EventTooltip extends TooltipBase {
    //region Config

    static get $name() {
        return 'EventTooltip';
    }

    static get defaultConfig() {
        return {
            /**
             * A function which receives data about the event and returns a string,
             * or a Promise yielding a string (for async tooltips), to be displayed in the tooltip.
             * This method will be called with an object containing the fields below
             * @param {Object} data
             * @param {Scheduler.model.EventModel} data.eventRecord
             * @param {Date} data.startDate
             * @param {Date} data.endDate
             * @param {String} data.startText
             * @param {String} data.endText
             * @config {Function} template
             */
            template : data => `
                ${data.eventRecord.name ? StringHelper.xss`<div class="b-sch-event-title">${data.eventRecord.name}</div>` : ''}
                ${data.startClockHtml}
                ${data.endClockHtml}`,

            cls : 'b-sch-event-tooltip',

            monitorRecordUpdate : true,

            /**
             * Defines what to do if document is scrolled while the tooltip is visible.
             *
             * Valid values: ´null´: do nothing, ´hide´: hide the tooltip or ´realign´: realign to the target if possible.
             *
             * @config {'hide'|'realign'|null}
             * @default
             */
            scrollAction : 'hide'
        };
    }

    /**
     * The event which the tooltip feature has been activated for.
     * @member {Scheduler.model.EventModel} eventRecord
     * @readonly
     */

    //endregion

    construct(client, config) {
        super.construct(client, config);

        if (typeof this.align === 'string') {
            this.align = { align : this.align };
        }
    }

    onPaint({ firstPaint }) {
        super.onPaint(...arguments);

        if (firstPaint) {
            const
                { dependencies } = this.client.features;

            if (dependencies) {
                this.tooltip.ion({
                    beforeAlign({ source : tooltip, offset = zeroOffset }) {
                        const
                            { edgeAligned }   = parseAlign(tooltip.align.align),
                            depTerminalOffset = dependencies.disabled ? zeroOffset : depOffset[edgeAligned];

                        // Add the spec's offset to the offset necessitated by dependency terminals
                        arguments[0].offset = [
                            offset[0] + depTerminalOffset[0],
                            offset[1] + depTerminalOffset[1]
                        ];
                    }
                });
            }
        }
    }
}

GridFeatureManager.registerFeature(EventTooltip, true, 'Scheduler');
GridFeatureManager.registerFeature(EventTooltip, false, 'ResourceHistogram');
