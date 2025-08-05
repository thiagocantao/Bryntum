import ContextMenuBase from '../../../Grid/feature/base/ContextMenuBase.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module Scheduler/feature/base/TimeSpanMenuBase
 */

/**
 * Abstract base class used by other context menu features which show the context menu for TimeAxis.
 * Using this class you can make sure the menu expects the target to disappear,
 * since it can be scroll out of the scheduling zone.
 *
 * Features that extend this class are:
 *  * {@link Scheduler/feature/EventMenu EventMenu};
 *  * {@link Scheduler/feature/ScheduleMenu ScheduleMenu};
 *  * {@link Scheduler/feature/TimeAxisHeaderMenu TimeAxisHeaderMenu};
 *
 * @extends Grid/feature/base/ContextMenuBase
 * @abstract
 */
export default class TimeSpanMenuBase extends ContextMenuBase {
    get menuConfig() {
        const me = this;

        return ObjectHelper.assign({}, super.menuConfig, {
            // It's a rectangle outside which the target of alignment may disappear.
            // It has to be like that because when scrolling in a scheduler, there are two scrolling elements.
            // The main Scheduler body for up/down and the subgrid for left and right
            clippedBy : [me.client.timeAxisSubGridElement, me.client.bodyContainer]
        });
    }
}
