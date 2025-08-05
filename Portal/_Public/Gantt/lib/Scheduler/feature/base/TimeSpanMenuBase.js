import ContextMenuBase from '../../../Core/feature/base/ContextMenuBase.js';

/**
 * @module Scheduler/feature/base/TimeSpanMenuBase
 */

/**
 * Abstract base class used by other context menu features which show the context menu for TimeAxis.
 * Using this class you can make sure the menu expects the target to disappear,
 * since it can be scroll out of the scheduling zone.
 *
 * Features that extend this class are:
 *  * {@link Scheduler/feature/EventMenu};
 *  * {@link Scheduler/feature/ScheduleMenu};
 *  * {@link Scheduler/feature/TimeAxisHeaderMenu};
 *
 * @extends Core/feature/base/ContextMenuBase
 * @abstract
 */
export default class TimeSpanMenuBase extends ContextMenuBase {
}
