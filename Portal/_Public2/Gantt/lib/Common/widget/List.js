import Widget from './Widget.js';
import Store from '../data/Store.js';
import TemplateHelper from '../helper/TemplateHelper.js';
import DomHelper from '../helper/DomHelper.js';
import EventHelper from '../helper/EventHelper.js';
import Collection from '../util/Collection.js';
import Navigator from '../helper/util/Navigator.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';

/**
 * @module Common/widget/List
 */

const itemRange = document.createRange();

/**
 * Displays a list of items which the user can navigate using the keyboard and select using either pointer gestures or the keyboard.
 * @extends Common/widget/Widget
 *
 * @classType list
 * @externalexample widget/List.js
 */
export default class List extends Widget {
    //region Config

    static get defaultConfig() {
        return {
            itemCls : 'b-list-item',

            selectedCls : 'b-selected',

            /**
             * An array of Objects which are converted into records and used to create this
             * List's {@link #config-store}
             * @config {Object[]}
             */
            items : null,

            /**
             * A {@link Common.data.Store} which provides the records which map to List items. Each record is passed through the
             * {@link #config-itemTpl} to produce the DOM structure of the List. May be generated from an array of {@link #config-items}.
             * @config {Object/Common.data.Store}
             */
            store : null,

            navigator : true,

            scrollable : {
                x : false,
                y : true
            },

            itemsFocusable : true,

            multiSelect : false,

            /**
             * Template function which, when passed a record, returns the textual HTML for that item. Defaults to a
             * function returning the value of records `text` field
             * @config {Function} itemTpl
             */
            itemTpl : record => record.text,

            /**
             * A {@link Common.util.Collection Collection}, or Collection config object
             * to use to contain this List's selected records.
             * @config {Common.util.Collection/Object}
             */
            selected : {},

            /**
             * Configure as `true` to activate items on mouseover. This is used by the Combo
             * field whenm using a List as its dropdown.
             * @config {Boolean}
             */
            activateOnMouseover : null
        };
    }

    //endregion

    //region Events

    /**
     * User activated an item in the list either by pointer or keyboard.
     * The active record, list item index, and the triggering event are passed.
     * @event item
     * @property {Object} record - Activated record
     * @property {Number} index - List item index
     * @property {Event} event - Triggering event
     */

    //endregion

    construct(config, ...args) {
        const me = this;

        // We can be created from a raw array. It becomes our items which we translate to a Store.
        if (Array.isArray(config)) {
            config = {
                items : config
            };
        }

        super.construct(config, ...args);

        const element = me.element,
            classList = element.classList;

        if (me.multiSelect) {
            classList.add('b-multiselect');
        }
        if (me.store.count) {
            me.refresh();
        }
        else {
            classList.add('b-empty');
        }

        EventHelper.on({
            element,
            delegate  : me.itemSelector,
            mouseover : 'onMouseOver',
            click     : 'onClick',
            thisObj   : me
        });

        me.storeDetacher = me.store.on({
            change  : 'onStoreChange',
            refresh : 'onStoreRefresh',
            thisObj : me
        });
    }

    doDestroy() {
        if (this.storeDetacher) {
            this.storeDetacher();
        }
        super.doDestroy();
    }

    contentTpl() {
        return TemplateHelper.tpl`${this.store.records.map((record, i) => this.itemWrapperTpl(record, i))}`;
    }

    itemWrapperTpl(record, i) {
        return TemplateHelper.tpl`<div class="${this.getItemClasses(record, i)}" data-index="${i}" data-id="${record.id}" ${this.itemsFocusable ? 'tabindex="-1"' : ''}>
            ${this.itemContentTpl(record, i)}
            </div>`;
    }

    itemContentTpl(record, i) {
        return TemplateHelper.tpl`${this.multiSelect ? '<div class="b-selected-icon b-icon b-icon-check"></div>' : ''}${this.itemTpl(record, i)}`;
    }

    getItemClasses(record) {
        const me = this,
            activeItem = me._navigator && me._navigator.activeItem,
            isActive = activeItem && activeItem.dataset.id == record.id,
            isSelected = me.selected.includes(record);

        return `${me.itemCls} ${record.cls || ''} ${isSelected ? me.selectedCls : ''} ${isActive ? me.navigator.focusCls : ''}`;
    }

    onStoreChange({ source : store, action, records, record }) {
        switch (action) {
            case 'remove':
                this.selected.remove(records);
                break;
            case 'clear':
                this.selected.clear();
                break;
            case 'update':
                this.refreshItem(record);
                return;
        }
        this.refresh();
    }

    onStoreRefresh() {
        this.refresh();
    }

    refresh() {
        const me = this;

        if (me.isVisible) {
            me.clearItems().insertNode(DomHelper.createElementFromTemplate(me.contentTpl(), { fragment : true }));
            me.element.classList[me.store.count > 0 ? 'remove' : 'add']('b-empty');
        }
        else {
            me.on({
                paint : () => me.refresh(),
                once  : true
            });
        }
    }

    clearItems() {
        const me = this,
            firstItem = me.contentElement.querySelector(me.itemSelector),
            lastChild = me.contentElement.lastChild;

        if (firstItem) {
            itemRange.setStartBefore(firstItem);
            itemRange.setEndAfter(me.contentElement.querySelector(`${me.itemSelector}:last-of-type`));
            itemRange.deleteContents();
        }
        else {
            // Allow a static set of elements to be at the top of the list
            if (lastChild) {
                itemRange.setStartAfter(lastChild);
                itemRange.setEndAfter(lastChild);
            }
            else {
                itemRange.setStart(me.contentElement, 0);
                itemRange.setEnd(me.contentElement, 0);
            }
        }
        return itemRange;
    }

    refreshItem(...records) {
        for (const record of records) {
            const item = this.getItem(record);

            // Maybe a record which is filtered out announces a change.
            // There will be no item.
            if (item) {
                const index = this.store.indexOf(record),
                    newItem = DomHelper.createElementFromTemplate(this.itemWrapperTpl(record, index));

                DomHelper.sync(newItem, item);
            }
        }
    }

    getItem(recordOrId) {
        if (typeof recordOrId === 'number') {
            return this.contentElement.querySelector(`[data-index="${recordOrId}"]`);
        }
        if (recordOrId.id != null) {
            recordOrId = recordOrId.id;
        }
        return this.contentElement.querySelector(`[data-id="${recordOrId}"]`);
    }

    getRecord(dom) {
        if (dom.target) {
            dom = dom.target;
        }
        dom = dom.closest(this.itemSelector);

        return this.store.getAt(parseInt(dom.dataset.index));
    }

    //region getters/setters

    /**
     * May be *set* as an array of Objects which are converted into records and used to create this
     * List's {@link #config-store}
     * @property {Object[]}
     */
    set items(items) {
        const me = this;

        if (me.store && me.store.autoCreated) {
            me.store.destroy();
        }

        me.store = Store.getStore(items);
    }

    set selected(selected) {
        if (!(selected && selected instanceof Collection)) {
            selected = new Collection(selected);
        }
        this._selected = selected;
        selected.on({
            change  : 'onSelectionChange',
            thisObj : this
        });
    }

    get itemSelector() {
        return `.${this.itemCls}`;
    }

    get selected() {
        return this._selected;
    }

    /**
     * Get the backing store, a {@link Common.data.Store} holding the records used to generate list items
     * @property {Common.data.Store}
     * @readonly
     */
    get store() {
        // Ensure any configured items is processed into a store before we try to return it.
        this._thisIsAUsedExpression(this.items);

        return this._store;
    }

    set store(store) {
        if (!(store instanceof Store)) {
            store = new Store(store);
        }
        this._store = store;
    }

    get navigator() {
        return this._navigator;
    }

    set navigator(navigator) {
        const me = this,
            { element } = me;

        if (element) {
            me._navigator = new (navigator.class || Navigator)(Object.assign({
                ownerCmp       : me,
                target         : element,
                keyEventTarget : element
            }, navigator));
        }
        else {
            me._navigator = navigator;
        }
    }

    get minHeight() {
        return super.minHeight;
    }

    set minHeight(minHeight) {
        super.minHeight = this._minHeight = minHeight;
    }

    get minAlignHeight() {
        const lastItem = this.element.lastElementChild,
            minHeight = this.minHeight;

        // No minHeight specified, always defer to the items height
        if (minHeight != null) {
            return this.store.count ? Math.min(lastItem.offsetTop + lastItem.offsetHeight, minHeight) : 0;
        }
    }

    //endregion

    //region Hide/Show

    alignTo(...args) {
        // When aligning, if the items total height is less than minHeight, use that.
        super.minHeight = this.minAlignHeight;
        super.alignTo(...args);
    }

    hide(...args) {
        this.navigator.activeItem = null;
        super.hide(...args);
    }

    show() {
        // Restore the configured minHeight
        super.minHeight = this._minHeight;
        let activeItem = this.navigator.previousActiveItem;

        super.show();

        if (activeItem) {
            if (!this.element.contains(activeItem)) {
                activeItem = this.element.querySelector(`[data-id="${activeItem.dataset.id}"]`);
            }
            this.navigator.activeItem = activeItem;
        }
    }

    //endregion

    //region Events

    /**
     * Focuses list items on hover.
     * @private
     */
    onMouseOver(event) {
        const me        = this,
            itemElement = event.target.closest(me.itemSelector);

        if (itemElement && me.navigator && me.activateOnMouseover) {
            me.navigator.activeItem = itemElement;
        }
    }

    /**
     * Selects list items on click.
     * @private
     */
    onClick(event) {
        const itemElement = event.target.closest(this.itemSelector);

        if (itemElement) {
            this.onItemClick(itemElement, event);
        }
    }

    /**
     * Key events which are not navigation are delegated up to here by the Navigator
     * @private
     */
    onInternalKeyDown(event) {
        const me     = this,
            active   = me.navigator.activeItem;

        switch (event.key) {
            case ' ':
                if (!event.target.readOnly) {
                    break; // eslint-disable-line
                }
            case 'Enter': // eslint-disable-line
                if (active) {
                    this.onItemClick(active, event);

                    // Stop the keydown from bubbling.
                    // And stop it from creating a keypress event.
                    // No further action should be taken after item selection.
                    event.stopImmediatePropagation();
                    event.preventDefault();
                }
        }
    }

    //endregion

    onItemClick(item, event) {
        const me = this,
            index = parseInt(item.dataset.index),
            record = me.store.getAt(index),
            selected = me.selected,
            isSelected = selected.includes(record);

        me.trigger('item', {
            item,
            record,
            index,
            event
        });

        // Clicking on any element with the data-noselect attribute means no selection
        if (!item.contains(event.target.closest('[data-noselect]'))) {
            if (me.multiSelect) {
                selected[isSelected ? 'remove' : 'add'](record);
            }
            else {
                selected.splice(0, selected.count, record);
            }
        }

        me.lastClicked = record;
    }

    /**
     * Handles items being added or removed from the selected Collection
     * @param {Object} changeEvent
     * @private
     */
    onSelectionChange({ action, removed, added, replaced }) {
        const me = this,
            { selectedCls } = me;

        let record, item;

        if (action === 'clear') {
            for (item of me.element.querySelectorAll(`.${selectedCls}`)) {
                item.classList.remove(selectedCls);
            }
        }
        else {
            for (record of removed) {
                item = me.getItem(record);
                item && item.classList.remove(selectedCls);
            }
            for (record of added) {
                item = me.getItem(record);
                item && item.classList.add(selectedCls);
            }
        }
    }
}

List.prototype.navigatorClass = Navigator;

BryntumWidgetAdapterRegister.register('list', List);
