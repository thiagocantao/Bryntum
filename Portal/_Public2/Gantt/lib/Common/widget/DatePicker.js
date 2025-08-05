import CalendarPanel from './CalendarPanel.js';
import DateHelper from '../helper/DateHelper.js';
import EventHelper from '../helper/EventHelper.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Common/widget/DatePicker
 */

/**
 * A Panel which can display a month of date cells, which navigates between the cells,
 * fires events upon user selection actions, optionally navigates to other months
 * in response to UI gestures, and optionally displays information about each date cell.
 *
 * This class is not intended for use in applications. It is used internally by the
 * {@link Common.widget.DateField} class.
 *
 * @classtype datepicker
 */
export default class DatePicker extends CalendarPanel {

    static get defaultConfig() {
        return {
            focusable : true,

            tools : {
                prevMonth : {
                    align   : 'start',
                    cls     : 'b-icon b-icon-angle-left',
                    handler : 'gotoPrevMonth'
                },
                nextMonth : {
                    align   : 'end',
                    cls     : 'b-icon b-icon-angle-right',
                    handler : 'gotoNextMonth'
                }
            },

            header : {
                title      : '\xa0',
                titleAlign : 'center'
            },

            /**
             * The minimum selectable date. Selection of and navigtion to dates prior
             * to this date will not be possible.
             * @config {Date}
             */
            minDate : null,

            /**
             * The maximum selectable date. Selection of and navigtion to dates after
             * this date will not be possible.
             * @config {Date}
             */
            maxDate : null,

            /**
             * The class name to add to the calendar cell whose date which is outside of the
             * {@link #config-minDate}/{@link #config-maxDate} range.
             * @config {String}
             * @private
             */
            outOfRangeCls : 'b-out-of-range',

            /**
             * The class name to add to the currently focused calendar cell.
             * @config {String}
             * @private
             */
            activeCls : 'b-active-date',

            /**
             * The class name to add to selected calendar cells.
             * @config {String}
             * @private
             */
            selectedCls : 'b-selected-date',

            /**
             * By default, disabled dates cannot be navigated to, and they are skipped over
             * during keyboard navigation. Configure this as `true` to enable navigation to
             * disabled dates.
             * @config {Boolean}
             * @default
             */
            focusDisabledDates : null,

            /**
             * Configure as `true` to enable selecting a single date range by selecting a
             * start and end date.
             * @config {Boolean}
             * @default
             */
            multiSelect : false
        };
    }

    /**
     * Fires when a date is selected. If {@link #config-multiSelect} is specified, this
     * will fire upon deselection and selection of dates.
     * @event selectionChange
     * @param {Date[]} selection The selected date. If {@link #config-multiSelect} is specified
     * this may be a two element array specifying start and end dates.
     */

    construct(config) {
        const me = this;

        me.selection = [];
        me.refresh = me.createOnFrame(me.refresh);
        super.construct(config);
        me.element.setAttribute('aria-activedescendant', `${me.id}-active-day`);

        EventHelper.on({
            element   : me.element,
            mousedown : 'onPickerMousedown',
            click     : {
                delegate : `.${me.cellCls}:not(.${me.disabledCls}):not(.${me.outOfRangeCls})`,
                handler  : 'onCellClick'
            },
            keydown : 'onPickerKeyDown',
            thisObj : me
        });
    }

    get focusElement() {
        return this.element;
    }

    refresh() {
        super.refresh();
        this.title = DateHelper.format(this.month.date, 'MMMM YYYY');
    }

    cellRenderer(cell, cellDate) {
        const me = this,
            { activeCls, selectedCls } = me,
            cellClassList = cell.classList;

        cell.innerHTML = cellDate.getDate();
        cell.setAttribute('aria-label', DateHelper.format(cellDate, 'MMMM D, YYYY'));

        if (me.isActiveDate(cellDate)) {
            cellClassList.add(activeCls);
            cell.id = `${me.id}-active-day`;
        }
        if (me.isSelectedDate(cellDate)) {
            cellClassList.add(selectedCls);
        }
        if (me.minDate && cellDate < me.minDate) {
            cellClassList.add(me.outOfRangeCls);
        }
        else if (me.maxDate && cellDate > me.maxDate) {
            cellClassList.add(me.outOfRangeCls);
        }
    }

    onPickerMousedown(event) {
        event.preventDefault();
    }

    onCellClick(event) {
        this.onDateActivate(DateHelper.parse(event.target.dataset.date, 'YYYY-MM-DD'), event);
    }

    onDateActivate(date, event) {
        const me = this,
            { lastClickedDate, selection } = me;

        me.activeDate = date;
        me.lastClickedDate = date;

        // Handle multi selecting.
        // * single contiguous date range, eg: an event start and end
        // * multiple discontiguous ranges
        if (me.multiSelect) {
            if (me.multiRange) {
                // TODO: multiple date ranges
            }
            else if (!lastClickedDate || date.getTime() !== lastClickedDate.getTime()) {
                if (lastClickedDate && event.shiftKey) {
                    selection[1] = date;
                    selection.sort();
                }
                else {
                    selection.length = 0;
                    selection[0] = date;
                }

                me.trigger('selectionChange', {
                    selection
                });
            }
        }
        else {
            if (!me.value || me.value.getTime() !== date.getTime()) {
                me.value = date;
            }
            else {
                me.hide();
            }
        }
    }

    onPickerKeyDown(keyEvent) {
        const me = this,
            keyName = keyEvent.key.trim() || keyEvent.code,
            activeDate = me.activeDate;

        let newDate = new Date(activeDate);

        if (activeDate) {
            do {
                switch (keyName) {
                    case 'Escape':
                        me.hide();
                        break;
                    case 'ArrowLeft':
                        if (keyEvent.ctrlKey) {
                            // Disable browser use of this key
                            keyEvent.preventDefault();
                            newDate.setMonth(newDate.getMonth() - 1);
                        }
                        else {
                            newDate.setDate(newDate.getDate() - 1);
                        }
                        break;
                    case 'ArrowUp':
                        newDate.setDate(newDate.getDate() - 7);
                        break;
                    case 'ArrowRight':
                        if (keyEvent.ctrlKey) {
                            // Disable browser use of this key
                            keyEvent.preventDefault();
                            newDate.setMonth(newDate.getMonth() + 1);
                        }
                        else {
                            newDate.setDate(newDate.getDate() + 1);
                        }
                        break;
                    case 'ArrowDown':
                        newDate.setDate(newDate.getDate() + 7);
                        break;
                    case 'Enter':
                        me.onDateActivate(activeDate, keyEvent);
                        break;
                }
            } while (me.isDisabledDate(newDate) && !me.focusDisabledDates);

            // Don't allow navigation to outside of date bounds.
            if (me.minDate && newDate < me.minDate) {
                return;
            }
            if (me.maxDate && newDate > me.maxDate) {
                return;
            }
            me.activeDate = newDate;
        }
    }

    set minDate(minDate) {
        this._minDate = minDate ? this.ingestDate(minDate) : null;
        this.refresh();
    }

    get minDate() {
        return this._minDate;
    }

    set maxDate(maxDate) {
        this._maxDate = maxDate ? this.ingestDate(maxDate) : null;
        this.refresh();
    }

    get maxDate() {
        return this._maxDate;
    }

    set activeDate(activeDate) {
        const me = this;

        if (activeDate) {
            me._activeDate = me.ingestDate(activeDate);
        }
        else {
            me._activeDate = DateHelper.clearTime(new Date());
        }

        // New active date is in another month
        if (me.month.month !== me._activeDate.getMonth()) {
            me.month.date = me._activeDate;
        }
        me.refresh();
    }

    get activeDate() {
        return this._activeDate;
    }

    set value(date) {
        const me = this,
            { selection } = me;

        let changed;

        if (date) {
            date = me.ingestDate(date);
            if (!me.value || date.getTime() !== me.value.getTime()) {
                selection.length = 0;
                selection[0] = date;
                me.date = date;
                changed = true;
            }
        }
        else {
            changed = selection.length;
            selection.length = 0;

            // Clearing the value - go to today's calendar
            me.date = new Date();
        }

        if (changed) {
            me.trigger('selectionChange', {
                selection
            });
        }
    }

    get value() {
        return this.selection[this.selection.length - 1];
    }

    gotoPrevMonth() {
        const date = this.date;

        date.setMonth(date.getMonth() - 1);
        this.date = date;
    }

    gotoNextMonth() {
        const date = this.date;

        date.setMonth(date.getMonth() + 1);
        this.date = date;
    }

    isActiveDate(date) {
        return this.activeDate && this.ingestDate(date).getTime() === this.activeDate.getTime();
    }

    isSelectedDate(date) {
        return this.selection.some(d => d.getTime() === date.getTime());
    }
}

BryntumWidgetAdapterRegister.register('datepicker', DatePicker);
