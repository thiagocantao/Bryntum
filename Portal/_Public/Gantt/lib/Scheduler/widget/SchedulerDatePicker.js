import DatePicker from '../../Core/widget/DatePicker.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Scheduler/widget/SchedulerDatePicker
 */

/**
 * A subclass of {@link Core.widget.DatePicker} which is able to show the presence of
 * events in its cells if configured with an {@link #config-eventStore}, and
 * {@link #config-showEvents} is set to a truthy value.
 *
 * The `datepicker` Widget type is implemented by this class when this class is imported, or built
 * into a bundle, and so any {@link Core.widget.DateField} may have its
 * {@link Core.widget.PickerField#config-picker} configured to use its capabilities of showing
 * the presence of events in its date cells.
 *
 * @classtype datepicker
 * @extends Core/widget/DatePicker
 * @inlineexample Scheduler/widget/SchedulerDatePicker.js
 * @widget
 */
export default class SchedulerDatePicker extends DatePicker {
    static get $name() {
        return 'SchedulerDatePicker';
    }

    static get type() {
        return 'datepicker';
    }

    static get configurable() {
        return {
            /**
             * How to show presence of events in the configured {@link #config-eventStore} in the
             * day cells. Values may be:
             *
             * * `false` - Do not show events in cells.
             * * `true` - Show a themeable bullet to indicate the presence of events for a date.
             * * `'count'` - Show a themeable badge containing the event count for a date.
             * @config {Boolean|'count'}
             * @default false
             */
            showEvents : null,

            /**
             * The {@link Scheduler.data.EventStore event store} from which the in-cell event presence
             * indicators are drawn.
             * @config {Scheduler.data.EventStore}
             */
            eventStore : null,

            /**
             * A function, or the name of a function in the ownership hierarchy to filter which events
             * are collected into the day cell data blocks.
             *
             * Return `true` to include the passed event, or a *falsy* value to exclude the event.
             * @config {Function|String}
             */
            eventFilter : {
                $config : 'lazy',
                value   : null
            }
        };
    }

    construct(config) {
        // Handle deprecated events config. It is now showEvents.
        // events conflicts with the events data which may be passed in
        if ('events' in config) {
            config = {
                ...config,
                showEvents : config.events
            };
            delete config.events;

            VersionHelper.deprecate(VersionHelper['calendar'] ? 'Calendar' : 'Scheduler', '6.0.0', 'DatePicker#events should be configured as showEvents');
        }
        super.construct(config);
    }

    changeEventFilter(eventFilter) {
        if (typeof eventFilter === 'string') {
            const { handler, thisObj } = this.resolveCallback(eventFilter);
            eventFilter = handler.bind(thisObj);
        }
        return eventFilter;
    }

    doRefresh() {
        // Hidden widgets must not query the EventStore for loading on demand to be able to use
        // the EventStore's dateRangeRequested event.
        if (this.isVisible || !this.showEvents) {
            this.refreshEventsMap();
            return super.doRefresh(...arguments);
        }
        else {
            this.whenVisible('doRefresh');
        }
    }

    updateShowEvents(showEvents, oldShowEvents) {
        const
            me            = this,
            { classList } = me.contentElement;

        let { eventStore } = me;

        // Begin any animations in the next AF
        me.requestAnimationFrame(() => {
            me.element.classList.toggle('b-datepicker-with-events', Boolean(showEvents));
            me.owner?.element.classList.toggle('b-datepicker-with-events', Boolean(showEvents));
            showEvents && classList.add(`b-show-events-${showEvents}`);
            classList.remove(`b-show-events-${oldShowEvents}`);
        });

        if (showEvents) {
            if (!eventStore) {
                const eventStoreOwner = me.up(w => w.eventStore);

                if (eventStoreOwner) {
                    eventStore = eventStoreOwner.eventStore;
                }
                else {
                    throw new Error('DatePicker configured with events but no eventStore');
                }
            }
        }
        else {
            me.eventsMap = null;
        }
        if (!me.isConfiguring) {
            me.updateEventStore(eventStore);
            me.doRefresh();
        }
    }

    refreshEventsMap() {
        const me = this;

        if (me.showEvents) {
            me.eventsMap = me.eventStore.getEventCounts({
                startDate : me.startDate,
                endDate   : me.endDate,
                dateMap   : me.eventsMap,
                filter    : me.eventFilter
            });
        }
    }

    updateEventStore(eventStore) {
        // Add a listener to refresh on any event change unless the listener is already added.
        if (eventStore.findListener('change', 'refresh', this) === -1) {
            eventStore?.[this.showEvents ? 'on' : 'un']?.({
                change  : 'refresh',
                thisObj : this
            });
        }
    }

    cellRenderer({ cell, date }) {
        const
            { showEvents } = this,
            count          = this.eventCounts?.get?.(DateHelper.makeKey(date)),
            isCount        = showEvents === 'count';

        delete cell.dataset.btip;
        if (count) {
            if (!isCount && this.eventCountTip) {
                cell.dataset.btip = this.L('L{ResourceInfoColumn.eventCountText}', count);
            }
            DomHelper.createElement({
                dataset : {
                    count
                },
                class : {
                    [isCount ? 'b-cell-events-badge' : 'b-icon b-icon-circle'] : 1,
                    [SchedulerDatePicker.getEventCountClass(count)]            : 1
                },
                parent                  : cell,
                [isCount ? 'text' : ''] : count
            });
        }
    }

    static getEventCountClass(count) {
        if (count) {
            if (count < 4) {
                return 'b-datepicker-1-to-3-events';
            }
            if (count < 7) {
                return 'b-datepicker-4-to-6-events';
            }
            return 'b-calendar-7-or-more-events';
        }
        return '';
    }

    static setupClass(meta) {
        // We take over the type name 'datepicker' when we are in the app
        meta.replaceType = true;

        super.setupClass(meta);
    }
}

// Register this widget type with its Factory
SchedulerDatePicker.initClass();
