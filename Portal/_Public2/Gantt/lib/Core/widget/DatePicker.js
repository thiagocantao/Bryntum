import CalendarPanel from './CalendarPanel.js';
import DateHelper from '../helper/DateHelper.js';
import EventHelper from '../helper/EventHelper.js';
import DomHelper from '../helper/DomHelper.js';
import Editor from './Editor.js';
import Combo from './Combo.js';

/**
 * @module Core/widget/DatePicker
 */

/**
 * A Panel which can display a month of date cells, which navigates between the cells,
 * fires events upon user selection actions, optionally navigates to other months
 * in response to UI gestures, and optionally displays information about each date cell.
 *
 * This class is used by the {@link Core.widget.DateField} class.
 *
 * @externalexample widget/DatePicker.js
 * @classtype datepicker
 * @extends Core/widget/CalendarPanel
 */
export default class DatePicker extends CalendarPanel {
    static get $name() {
        return 'DatePicker';
    }

    // Factoryable type name
    static get type() {
        return 'datepicker';
    }

    static get delayable() {
        return {
            refresh : 'raf'
        };
    }

    static get configurable() {
        return {
            /**
             * The date that the user has navigated to using the UI *prior* to setting the widget's
             * value by selecting.
             *
             * This may be changed using keyboard navigation. The {@link Core.widget.CalendarPanel#property-date} is set
             * by pressing `ENTER` when the desired date is reached.
             *
             * Programatically setting the {@link Core.widget.CalendarPanel#config-date}, or using the UI to select the date
             * by clicking it also sets the `activeDate`
             * @config {Date}
             */
            activeDate : {
                $config : {
                    equal : 'date'
                },
                value : null
            }
        };
    }

    static get defaultConfig() {
        return {
            focusable : true,

            textContent : false,

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
                title      : '<div class="b-editable b-datepicker-month" data-reference="monthElement"></div> <div class="b-editable b-datepicker-year" data-reference="yearElement"></div>',
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
            multiSelect : false,

            /**
             * By default, the month and year show their editor on hover instead of click.
             * On a touch platform, they both show the editor on tap.
             * @config {Boolean}
             * @default
             */
            editOnHover : true,

            /**
             * By default, the month and year are editable. Configure this as `false` to prevent that.
             * @config {Boolean}
             * @default
             */
            editMonth : true
        };
    }

    /**
     * Fires when a date is selected. If {@link #config-multiSelect} is specified, this
     * will fire upon deselection and selection of dates.
     * @event selectionChange
     * @param {Date[]} selection The selected date. If {@link #config-multiSelect} is specified
     * this may be a two element array specifying start and end dates.
     * @param {Boolean} userAction This will be `true` if the change was caused by user interaction
     * as opposed to programmatic setting.
     */

    construct(config) {
        const me = this;

        me.selection = [];
        super.construct(config);

        me.element.setAttribute('aria-activedescendant', `${me.id}-active-day`);

        EventHelper.on({
            element   : me.element,
            mouseover : 'onPickerMouseover',
            click     : 'onPickerClick',
            thisObj   : me
        });
    }

    doDestroy() {
        if (this._yearEditor) {
            this._yearEditor.destroy();
        }
        if (this._monthEditor) {
            this._monthEditor.destroy();
        }
        super.doDestroy();
    }

    get focusElement() {
        return this.element;
    }

    get childItems() {
        const result = super.childItems;

        if (this._yearEditor) {
            result.unshift(this._yearEditor);
        }
        if (this._monthEditor) {
            result.unshift(this._monthEditor);
        }

        return result;
    }

    refresh() {
        const
            me  = this,
            sbw = DomHelper.scrollBarWidth;

        super.refresh();

        me.monthElement.innerHTML = DateHelper.format(me.month.date, 'MMMM');
        me.yearElement.innerHTML = DateHelper.format(me.month.date, 'YYYY');

        if (me.editMonth) {
            me.monthElement.style.minWidth = `calc(${me.maxMonthLength + 1}ch + ${sbw}px)`;
            me.yearElement.style.minWidth = sbw ? `calc(3ch + ${sbw}px` : '7ch';
        }
    }

    cellRenderer({ cell, date }) {
        const me = this,
            { activeCls, selectedCls } = me,
            cellClassList = cell.classList;

        cell.innerHTML = date.getDate();
        cell.setAttribute('aria-label', DateHelper.format(date, 'MMMM D, YYYY'));

        if (me.isActiveDate(date)) {
            cellClassList.add(activeCls);
            cell.id = `${me.id}-active-day`;
        }
        else {
            cell.removeAttribute('id');
        }

        if (me.isSelectedDate(date)) {
            cellClassList.add(selectedCls);
        }
        if (me.minDate && date < me.minDate) {
            cellClassList.add(me.outOfRangeCls);
        }
        else if (me.maxDate && date > me.maxDate) {
            cellClassList.add(me.outOfRangeCls);
        }
    }

    onPickerMouseover(event) {
        if (this.editOnHover) {
            const editable = this.editMonth && DomHelper.up(event.target, '.b-editable');

            if (editable) {
                return this.onEditGesture(event);
            }
        }
    }

    onPickerClick(event) {
        const
            me         = this,
            { target } = event,
            cell       = DomHelper.up(target, `.${me.dayCellCls}:not(.${me.disabledCls}):not(.${me.outOfRangeCls})`),
            editable   = me.editMonth && DomHelper.up(target, '.b-editable');

        if (cell) {
            return me.onCellClick(event);
        }
        if ((!me.editOnHover || DomHelper.isTouchEvent) && editable) {
            return me.onEditGesture(event);
        }
        if (me._monthEditor && !me._monthEditor.owns(event)) {
            me._monthEditor.cancelEdit();
        }
        if (me._yearEditor && !me._yearEditor.owns(event)) {
            me._yearEditor.cancelEdit();
        }
    }

    onCellClick(event) {
        this.onUIDateSelect(DateHelper.parseKey(event.target.dataset.date), event);
    }

    onEditGesture(event) {
        const
            me         = this,
            { month }  = me,
            { target } = event;

        if (target === me.monthElement) {
            me.monthEditor.startEdit({
                target,
                value            : month.month,
                fitTargetContent : false,
                hideTarget       : true
            });
        }
        else if (target === me.yearElement) {
            me.yearEditor.minWidth = `${DomHelper.scrollBarWidth + 50}px)`;
            me.yearEditor.startEdit({
                target,
                value            : month.year,
                fitTargetContent : true,
                hideTarget       : true
            });
        }
    }

    /**
     * Called when the user uses the UI to select the current activeDate. So ENTER when focused
     * or clicking a date cell.
     * @param {Date} date The active date to select
     * @param {Event} event the intigating event, either a `click` event or a `keydown` event.
     * @internal
     */
    onUIDateSelect(date, event) {
        const me = this,
            { lastClickedDate, selection } = me;

        me.activeDate = date;
        me.lastClickedDate = date;

        if (!me.isDisabledDate(date)) {
            me.activatingEvent = event;

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
                        selection,
                        userAction : Boolean(event)
                    });
                }
            }
            else {
                if (!me.value || me.value.getTime() !== date.getTime()) {
                    me.value = date;
                }
                else if (me.floating) {
                    me.hide();
                }
            }

            me.activatingEvent = null;
        }

        // Ensure the b-selected-date class is on the correct cell(s).
        me.refresh();
    }

    onInternalKeyDown(keyEvent) {
        const
            me         = this,
            keyName    = keyEvent.key.trim() || keyEvent.code,
            activeDate = me.activeDate,
            newDate    = new Date(activeDate);

        if (keyName === 'Escape' && me.floating) {
            return me.hide();
        }

        // Only navigate if not focused on one of our child widgets.
        // We have a prevMonth and nextMonth tool and possibly month and year pickers.
        if (activeDate && keyEvent.target === me.focusElement) {
            do {
                switch (keyName) {
                    case 'ArrowLeft':
                        // Disable browser use of this key.
                        // Ctrl+ArrowLeft navigates back.
                        // ArrowLeft scrolls if there is horizontal scroll.
                        keyEvent.preventDefault();

                        if (keyEvent.ctrlKey) {
                            newDate.setMonth(newDate.getMonth() - 1);
                        }
                        else {
                            newDate.setDate(newDate.getDate() - 1);
                        }
                        break;
                    case 'ArrowUp':
                        // Disable browser use of this key.
                        // ArrowUp scrolls if there is vertical scroll.
                        keyEvent.preventDefault();

                        newDate.setDate(newDate.getDate() - 7);
                        break;
                    case 'ArrowRight':
                        // Disable browser use of this key.
                        // Ctrl+ArrowRight navigates forwards.
                        // ArrowRight scrolls if there is horizontal scroll.
                        keyEvent.preventDefault();

                        if (keyEvent.ctrlKey) {
                            newDate.setMonth(newDate.getMonth() + 1);
                        }
                        else {
                            newDate.setDate(newDate.getDate() + 1);
                        }
                        break;
                    case 'ArrowDown':
                        // Disable browser use of this key.
                        // ArrowDown scrolls if there is vertical scroll.
                        keyEvent.preventDefault();

                        newDate.setDate(newDate.getDate() + 7);
                        break;
                    case 'Enter':
                        return me.onUIDateSelect(activeDate, keyEvent);
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
        this._minDate = minDate ? this.changeDate(minDate) : null;
        this.refresh();
    }

    get minDate() {
        return this._minDate;
    }

    set maxDate(maxDate) {
        this._maxDate = maxDate ? this.changeDate(maxDate) : null;
        this.refresh();
    }

    get maxDate() {
        return this._maxDate;
    }

    updateDate(date) {
        super.updateDate(date);
        this.activeDate = date;
    }

    changeActiveDate(activeDate) {
        activeDate =  activeDate ? this.changeDate(activeDate) : this.date || (this.date = DateHelper.clearTime(new Date()));

        if (isNaN(activeDate)) {
            throw new Error('DatePicker date ingestion must be passed a Date, or a YYYY-MM-DD date string');
        }

        return activeDate;
    }

    updateActiveDate(activeDate) {
        // Month's date setter protects it from non-changes.
        this.month.date = activeDate;

        // Must refresh so that the active cell gets a refresh.
        this.refresh();
    }

    set value(value) {
        const me = this,
            { selection } = me;

        let changed;

        if (value) {
            value = me.changeDate(value, me.value);

            // Undefined return value means no change
            if (value === undefined) {
                return;
            }

            if (!me.value || value.getTime() !== me.value.getTime()) {
                selection.length = 0;
                selection[0] = value;
                changed = true;
            }
            me.date = value;
        }
        else {
            changed = selection.length;
            selection.length = 0;

            // Clearing the value - go to today's calendar
            me.date = new Date();
        }

        if (changed) {
            me.trigger('selectionChange', {
                selection,
                userAction : Boolean(me.activatingEvent)
            });
        }
    }

    get value() {
        return this.selection[this.selection.length - 1];
    }

    gotoPrevMonth() {
        const
            { activeDate } = this,
            // Navigate from the activeDate if the activeDate is in the UI.
            baseDate = activeDate && this.getCell(activeDate) ? activeDate : this.date,
            newDate = new Date(baseDate);

        newDate.setMonth(newDate.getMonth() - 1);

        this.date = newDate;
    }

    gotoNextMonth() {
        const
            { activeDate } = this,
            // Navigate from the activeDate if the activeDate is in the UI.
            baseDate = activeDate && this.getCell(activeDate) ? activeDate : this.date,
            newDate = new Date(baseDate);

        newDate.setMonth(newDate.getMonth() + 1);

        this.date = newDate;
    }

    isActiveDate(date) {
        return this.activeDate && this.changeDate(date).getTime() === this.activeDate.getTime();
    }

    isSelectedDate(date) {
        return this.selection.some(d => d.getTime() === date.getTime());
    }

    get monthEditor() {
        const me = this;

        if (!me._monthEditor) {
            me._monthEditor = new Editor({
                owner      : me,
                appendTo   : me.element,
                inputField : me.monthInput = new Combo({
                    editable                : false,
                    autoExpand              : !me.editOnHover,
                    items                   : me.monthItems,
                    highlightExternalChange : false,
                    picker                  : {
                        align : {
                            align : 't0-b0'
                        },
                        cls        : 'b-month-picker-list',
                        scrollable : {
                            overflowX : false
                        }
                    }
                }),
                completeOnChange : true,
                listeners        : {
                    complete     : 'onMonthPicked',
                    beforeCancel : 'onBeforeEditorCancel',
                    thisObj      : me
                }
            });
        }

        return me._monthEditor;
    }

    onMonthPicked({ value, oldValue }) {
        this.activeDate = DateHelper.add(this.activeDate, value - oldValue, 'month');
    }

    get yearEditor() {
        const me = this;

        if (!me._yearEditor) {
            me._yearEditor = new Editor({
                owner      : me,
                appendTo   : me.element,
                inputField : me.yearInput = new Combo({
                    editable                : false,
                    autoExpand              : !me.editOnHover,
                    items                   : me.yearItems,
                    highlightExternalChange : false,
                    picker                  : {
                        cls        : 'b-year-picker-list',
                        scrollable : {
                            overflowX : false
                        }
                    }
                }),
                completeOnChange : true,
                listeners        : {
                    complete     : 'onYearPicked',
                    beforeCancel : 'onBeforeEditorCancel',
                    thisObj      : me
                }
            });
        }

        return me._yearEditor;
    }

    onYearPicked({ value }) {
        const newDate = new Date(this.activeDate);

        newDate.setFullYear(value);

        this.activeDate = newDate;
    }

    onBeforeEditorCancel({ source }) {
        // Always move focus to this to cancel the edit so that it doesn't try to find a revert target.
        if (source.containsFocus) {
            this.element.focus();
            return false;
        }
    }

    get monthItems() {
        return DateHelper.getMonthNames().map((m, i) => [i, m]);
    }

    get yearItems() {
        const
            result = [],
            middle = new Date().getFullYear();

        for (let y = middle - 20; y < middle + 21; y++) {
            result.push(y);
        }

        return result;
    }

    get maxMonthLength() {
        if (!this._maxMonthLength) {
            this._maxMonthLength = 0;

            for (let i = 0, months = this.monthItems; i < 12; i++) {
                this._maxMonthLength = Math.max(this._maxMonthLength, months[i][1].length);
            }
        }

        return this._maxMonthLength;
    }

    updateLocalization() {
        if (this._monthEditor) {
            this._monthEditor.doDestroy();
            this._monthEditor = null;
        }
        if (this._yearEditor) {
            this._yearEditor.doDestroy();
            this._yearEditor = null;
        }
        this._maxMonthLength = 0;
        super.updateLocalization();
    }
}

// Register this widget type with its Factory
DatePicker.initClass();
