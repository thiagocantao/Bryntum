import Widget from './Widget.js';
import Store from '../data/Store.js';
import TemplateHelper from '../helper/TemplateHelper.js';
import DomHelper from '../helper/DomHelper.js';
import EventHelper from '../helper/EventHelper.js';
import Collection from '../util/Collection.js';
import Navigator from '../helper/util/Navigator.js';
import StringHelper from '../helper/StringHelper.js';
import ArrayHelper from '../helper/ArrayHelper.js';

/**
 * @module Core/widget/List
 */

const itemRange = document.createRange();

/**
 * Displays a list of items which the user can navigate using the keyboard and select using either pointer gestures or the keyboard.
 * @extends Core/widget/Widget
 *
 * @classType list
 * @inlineexample Core/widget/List.js
 * @widget
 */
export default class List extends Widget {


    //region Config
    static get $name() {
        return 'List';
    }

    // Factoryable type name
    static get type() {
        return 'list';
    }

    static get configurable() {
        return {
            itemCls : 'b-list-item',

            selectedCls : 'b-selected',

            selectIconCls : 'b-icon-check',
            tag           : 'ul',

            /**
             * An array of Objects which are converted into records and used to create this
             * List's {@link #config-store}
             * @config {Object[]}
             */
            items : null,

            /**
             * The model field to render into each list item
             * @config {String}
             * @default
             */
            displayField : 'text',

            /**
             * A {@link Core.data.Store} which provides the records which map to List items. Each record is passed through the
             * {@link #config-itemTpl} to produce the DOM structure of the List. May be generated from an array of {@link #config-items}.
             *
             * The store may also be configured as an array of records, or record data objects from which records may be constructed.
             * @config {Object|Object[]|Core.data.Store}
             */
            store : null,

            navigator : true,

            scrollable : {
                overflowX : false,
                overflowY : true
            },

            itemsFocusable : true,

            /**
             * Configure as `true` to allow multi select and add checkboxes to the items
             * @config {Boolean}
             * @default
             */
            multiSelect : false,

            /**
             * Select/deselect all if `CMD`/`CTRL` is pressed when clicking
             * @config {Boolean}
             * @default false
             */
            toggleAllIfCtrlPressed : false,

            /**
             * Set to `true` to add a "Select all" item to the list to select/unselect all items at once.
             * Only applies when {@link #config-multiSelect} mode is enabled.
             * @config {Boolean|String}
             * @default false
             */
            selectAllItem : false,

            /**
             * Template function which, when passed a record, returns the textual HTML for that item. Defaults to a
             * function returning the value of the record´s {@link #config-displayField}
             * @config {Function} itemTpl
             * @param {Core.data.Model} record The record
             */
            itemTpl(record) {
                let text = StringHelper.encodeHtml(record.getValue(this.displayField));

                if (text == null || text === '') {
                    // Make sure we have some content to fill the item element and have equal height for all items
                    text = '&nbsp;';
                }

                return text;
            },

            /**
             * Configure this as a function or the name of a function, which when passed a record in the
             * list, returns a style string to apply to its list item.
             * @config {Function|String} getItemStyle
             * @param {Core.data.Model} record The record
             */
            getItemStyle : null,

            /**
             * Configure this as a function or the name of a function, which when passed a record in the
             * list, returns a CSS class name string to apply to its list item.
             * @config {Function|String} getItemCls
             * @param {Core.data.Model} record The record
             */
            getItemCls : null,

            /**
             * Template function which is passed a group record and the uppercased group field name. The text returned
             * will be rendered as the group header.
             * @config {Function} groupHeaderTpl
             * @param {Core.data.Model} record The record
             * @param {String} groupName The current group name
             */
            groupHeaderTpl(record, groupName) {
                let name = StringHelper.encodeHtml(groupName);

                if (name == null || name === '') {
                    // Make sure we have some content to fill the item element and have equal height for all items
                    name = '&nbsp;';
                }
                return `${this.allowGroupSelect ? this.itemIconTpl(record) : ''}${name}`;
            },

            /**
             * Configure as `true` to allow selecting groups (all the group child records will be toggled). Only
             * applicable when the store is grouped.
             * @config {Boolean}
             * @default
             */
            allowGroupSelect : true,

            /**
             * A {@link Core.util.Collection}, or Collection config object to use to contain this List's selected
             * records.
             *
             * Or, an array encapsulating the initial selection which this List is to have upon Store load. This may be
             * an array of `id`s , or an array of objects with an `id` property:
             *
             * ```javascript
             * new List({
             *     // initially select record IDs 1 and 5 when store loads
             *     selected : [1, 5]
             * });
             * ```
             * @config {Object[]|Number[]|String[]|Core.util.Collection|CollectionConfig}
             */
            selected : {
                $config : 'nullify',
                value   : {}
            },

            /**
             * Configure as `true` to activate items on mouseover. This is used by the Combo
             * field when using a List as its dropdown.
             * @config {Boolean}
             */
            activateOnMouseover : null,

            role : 'listbox',

            keyMap : {
                'Ctrl+a' : 'selectAll'
            }
        };
    }

    //endregion

    //region Events

    /**
     * User activated an item in the list either by pointer or keyboard.
     * The active record, list item index, and the triggering event are passed.
     * @event item
     * @param {Core.data.Model} record Activated record
     * @param {Number} index List item index
     * @param {Event} event Triggering event
     */

    /**
     * User going to activate an item in the list either by pointer or keyboard.
     * The active record, list item index, and the triggering event are passed.
     * It is preventable by returning `false`
     * @event beforeItem
     * @preventable
     * @param {Core.data.Model} record Activated record
     * @param {Number} args List item index
     * @param {Event} event Triggering event
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

        const
            { element }   = me,
            { classList } = element;

        if (me.multiSelect) {
            classList.add('b-multiselect');
        }
        if (me.store?.count) {
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
    }

    doDestroy() {
        this.detachListeners('store');
        this.navigator?.destroy();

        super.doDestroy();
    }

    onFocusIn(e) {
        this.restoreActiveItem(e._target?.closest(this.itemSelector));
        super.onFocusIn(e);
    }

    contentTpl() {
        const
            me = this,
            allRecordsTpl = me.store.records.map((record, i) => me.itemWrapperTpl(record, i));

        if (me.multiSelect && me.selectAllItem !== false) {
            allRecordsTpl.unshift(me.selectAllItemTpl());
        }
        return TemplateHelper.tpl`${allRecordsTpl}`;
    }

    itemWrapperTpl(record, i) {
        const
            {
                selected,
                getItemStyle
            }     = this,
            style = getItemStyle ? `style="${this.callback(getItemStyle, this, [record])}"` : '';

        return TemplateHelper.tpl`<li ${style} class="${this.getItemClasses(record, i)}" role="option" aria-selected="${selected.includes(record)}" data-index="${i}" data-id="${StringHelper.encodeHtml(record.id)}" ${this.itemsFocusable ? 'tabindex="-1"' : ''}>${this.itemContentTpl(record, i)}</li>`;
    }

    itemContentTpl(record, i) {
        if ('groupRowFor' in record.meta) {
            return this.groupHeaderTpl(record, StringHelper.capitalize(record.groupChildren[0][record.meta.groupField]));
        }
        return `${this.itemIconTpl(record, i)}${this.itemTpl(record, i)}`;
    }

    itemIconTpl(record, i) {
        return this.multiSelect ? `<i class="b-selected-icon b-icon ${this.selectIconCls}"></i>` : '';
    }

    selectAllItemTpl() {
        const selectedCls = this.allSelected ? 'b-selected' : '';

        return `<li class="${this.itemCls} b-select-all-item ${selectedCls}" data-noselect data-select-all data-id="select-all">${this.itemIconTpl({})}<div>${this.L('selectAll')}</div></li>`;
    }

    getItemClasses(record) {
        const
            me             = this,
            { getItemCls } = me,
            activeItem     = me._navigator?.activeItem,
            isActive       = activeItem?.dataset.id == record.id,
            groupHeaderCls = 'groupRowFor' in record.meta ? 'b-list-item-group-header' : '',
            isSelected     = me.selected.includes(record) || (record.isSpecialRow && record.groupChildren.every(r => me.selected.includes(r))),
            itemCls        = getItemCls ? this.callback(getItemCls, this, [record]) : ''; ;

        return `${me.itemCls} ${record.cls || ''} ${itemCls} ${isSelected ? me.selectedCls : ''} ${isActive ? me.navigator.focusCls : ''} ${groupHeaderCls}`;
    }

    onBeforeStoreLoad() {
        this.mask(this.L('L{loading}'));
    }

    onAfterStoreRequest() {
        this.unmask();
    }

    onStoreChange({ source : store, action, records, record, changes }) {
        const
            me           = this,
            { selected } = me;

        switch (action) {
            // We must keep the selected Collection in sync with the new dataset.
            // Some selected items may not be part of the new dataset.
            // If some items with the same id *are* in the new dataset, the
            // selected Collection must hold the new instance.
            case 'dataset':
            {
                selected.match(store.storage, !me._externallyOwnedSelection);
                break;
            }
            case 'remove':
                selected.remove(records);
                break;
            case 'clear':
                me.deselectAll();
                break;
            case 'update': {
                const { id } = changes;

                // update elements data-id when changing id, to allow refreshItem to find correct element
                if (id) {
                    DomHelper.setAttributes(
                        me.contentElement.querySelector(StringHelper.encodeAttributeSelector(`data-id`, id.oldValue)),
                        {
                            'data-id' : id.value
                        });
                }

                me.refreshItem(record);
                return;
            }
        }

        me.refresh();
    }

    onStoreRefresh() {
        this.refresh();
    }

    refresh() {
        const me = this;

        if (me.isVisible) {
            const activeItem = me.navigator?.activeItem;

            // Ensure that the next call to refresh when not visible queues a refresh
            me.paintListener = null;

            // IMPORTANT! DO NOT INLINE!
            // This is a fix for issue: https://github.com/bryntum/support/issues/2171
            // Long story short: DomHelper returns document-fragment and first time we insert node, nothing is actually
            // inserted if (and only) fragment is not stored as a variable.
            const node = DomHelper.createElementFromTemplate(me.contentTpl(), { fragment : true });
            me.clearItems().insertNode(node);

            // The item elements will be all new, so restore the active item.
            // Navigator will reacquire it by its id.
            if (activeItem) {
                me.restoreActiveItem(activeItem);
            }
            me.refreshCount = (me.refreshCount || 0) + 1;
            me.element.classList[me.store.count > 0 ? 'remove' : 'add']('b-empty');
            me.updateSelectAllState();
        }
        // Only queue a refresh if not already queued
        else if (!me.paintListener) {
            me.paintListener = me.ion({
                paint   : 'refresh',
                thisObj : me,
                once    : true
            });
        }
    }

    clearItems() {
        const me = this,
            firstItem = me.contentElement.querySelector(me.itemSelector),
            lastChild = me.contentElement.lastChild;

        if (firstItem) {
            // Can't use :last-of-type when other elements of same type may be present.
            // Have to pluck the last element of the NodeList.
            const items = me.contentElement.querySelectorAll(`${me.itemSelector}:not(.b-mask)`);

            itemRange.setStartBefore(firstItem);
            itemRange.setEndAfter(items[items.length - 1]);
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
        // Cannot use truthiness test because index zero may be passed.
        if (recordOrId != null) {
            if (typeof recordOrId === 'number') {
                // -ve index means indexing from beyond the end.
                // So -1 gets the last item, -2 the penultimate etc
                return this.contentElement.querySelector(`[data-index="${recordOrId < 0 ? this.store.count + recordOrId : recordOrId}"]`);
            }
            else if (recordOrId.nodeType === Element.ELEMENT_NODE) {
                recordOrId = recordOrId.dataset.id;
            }

            if (recordOrId?.id != null) {
                recordOrId = recordOrId.id;
            }
            return this.contentElement.querySelector(StringHelper.encodeAttributeSelector(`data-id`, recordOrId));
        }
    }

    /**
     * Searches up from the specified element for a list item and returns the associated record.
     * @param {HTMLElement} element Element somewhere within a list item element
     * @returns {Core.data.Model} Record for the item
     */
    getRecordFromElement(element) {
        if (element.target) {
            element = element.target;
        }
        element = element.closest(this.itemSelector);

        return this.store.getAt(parseInt(element.dataset.index));
    }

    //region getters/setters

    /**
     * May be *set* as an array of Objects which are converted into records and used to create this
     * List's {@link #config-store}
     * @member {Object[]} items
     */
    updateItems(items) {
        const me = this;

        if (me.store && me.store.autoCreated) {
            me.store.destroy();
        }

        me.store = Store.getStore(items);
    }

    updateDisabled(disabled) {
        super.updateDisabled(...arguments);

        this.navigator && (this.navigator.disabled = disabled);
    }

    changeSelected(selected, oldSelected) {
        if (selected) {
            let initialSelection;

            // We convert incoming selected block to a Collection.
            // We may be configured with an initial selection array
            if (selected.isCollection) {
                this._externallyOwnedSelection = true;
            }
            else {
                if (Array.isArray(selected)) {
                    initialSelection = selected;
                    selected = {};
                }
                else if (selected.values) {
                    initialSelection = selected.values;
                    selected = {
                        ...selected,
                        values : []
                    };
                }
                selected = new Collection(selected);
            }
            selected.ion({
                change  : 'onSelectionChange',
                thisObj : this
            });

            // We will inject any configured initial selection array only after
            // the store has been loaded. Set the property even if it's zero length
            // because it acts as a flag that an initial selection was requested.
            this.initialSelection = initialSelection;
        }
        else {
            oldSelected?.destroy();
        }
        return selected;
    }

    get itemSelector() {
        return `.${this.itemCls}`;
    }

    /**
     * Get the collection of selected records
     * @property {Core.util.Collection}
     * @readonly
     */
    get selected() {
        return this._selected;
    }

    /**
     * Get the backing store, a {@link Core.data.Store} holding the records used to generate list items
     * @property {Core.data.Store}
     * @readonly
     */
    get store() {
        // Ensure any configured items is processed into a store before we try to return it.
        this.getConfig('items');

        return this._store;
    }

    changeStore(store) {
        const me = this;

        if (!(store instanceof Store)) {
            if (Array.isArray(store)) {
                store = {
                    data : store
                };
            }
            store = new Store(store);
        }

        me.detachListeners('store');

        if (store) {
            const storeListeners = {
                name    : 'store',
                change  : 'onStoreChange',
                refresh : 'onStoreRefresh',
                thisObj : me
            };

            if (store.readUrl) {
                storeListeners.beforeLoad   = 'onBeforeStoreLoad';
                storeListeners.afterRequest = 'onAfterStoreRequest';
            }
            if (store.isGrouped) {
                me.element.classList.add('b-grouped');
                storeListeners.groupchange = 'onStoreRefresh';
            }

            store.ion(storeListeners);
        }

        return store;
    }

    updateStore(store) {
        const { initialSelection } = this;

        // If our selected config contained an initial selection array,
        // apply it when the store is loaded.
        if (Array.isArray(initialSelection)) {
            if (store.count) {
                this.select(initialSelection);

                // We must process the fact that the selection is empty.
                // There will have been no event from the Collection if nothing was selected.
                // Note: The initialSelection array may have invalid ids in which could
                // not be selected, so a check of the actual Collection count is necessary.
                if (!this.selected.count) {
                    this.onSelectionChange({ action : 'clear', removed : [] });
                }
            }
            else {
                store.ion({
                    refresh : () => this.select(initialSelection),
                    once    : true
                });
            }
        }

        this.refresh();
    }

    changeNavigator(navigator, oldNavigator) {
        const me = this,
            { element } = me;

        if (element) {
            navigator = Navigator.reconfigure(oldNavigator, navigator ? Navigator.mergeConfigs({
                ownerCmp       : me,
                disabled       : me.disabled,
                target         : element,
                keyEventTarget : element
            }, navigator) : null, me);

            // This widget is responsible for relaying keyboard events into the navigator
            // So it must be able to receive focus if its items do not receive focus.
            if (navigator?.keyEventTarget === element && !me.itemsTabbable) {
                element.tabIndex = 0;
            }
        }

        return navigator;
    }

    get minAlignHeight() {
        const lastItem = this.element.lastElementChild,
            minHeight = this.minHeight;

        // No minHeight specified, always defer to the items height
        if (minHeight != null) {
            return lastItem ? Math.min(lastItem.offsetTop + lastItem.offsetHeight, minHeight) : 0;
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
        this.navigator && (this.navigator.activeItem = null);
        return super.hide(...args);
    }

    show() {
        const
            { refreshCount } = this,
            { previousActiveItem } = this.navigator;

        // Restore the configured minHeight
        super.minHeight = this._minHeight;

        const retVal = super.show(...arguments);

        // If no refresh on paint took place, we need to restore the activeItem.
        // Refresh does that itself.
        if (this.refreshCount === refreshCount) {
            this.restoreActiveItem(previousActiveItem, true);
        }

        return retVal;
    }

    /**
     * Sets the passed record as the current focused record for keyboard navigation and selection purposes.
     * @param {Core.data.Model} activeItem The item to highlight as the active ("focused") item.
     * @param {String|Boolean} [block='nearest'] The block scroll specification to scroll
     * the item into view. Defaults to `'nearest'` which means a minimal scroll, only if
     * the item is actually out of view. Passing `true` is for when the List is aligned
     * to a picker and the selected item should be scrolled to be close to the picker,
     * however that is aligned. See `Combo.showPicker` for usage.
     * @internal
     */
    restoreActiveItem(activeItem = this.navigator.previousActiveItem, block = 'nearest') {
        const
            me = this,
            { navigator, lastAlignSpec } = me;

        // Zero may be passed to select first item, so cannot use truthiness.
        // This won't mean *clear* the active item if any. Falsy means
        // nothing to restore so leave it as is.
        if (activeItem != null) {
            if (typeof block !== 'string') {
                const alignZone = lastAlignSpec ? lastAlignSpec.zone : 1;

                // Scroll item into view appropriately, meaning closest to any align target.
                // Align zones are T,R,B,L => 0,1,2,3
                // If 1 or 3 (right or left), align nearest.
                // if 0 (above) align end.
                // else, we're below so align start.
                block = alignZone & 1 ? 'nearest' : alignZone ? 'start' : 'end';
            }

            activeItem = me.getItem(activeItem);

            if (activeItem && !me.disabled) {
                // If we are aligned to a target, first, scroll the required item to the optimal
                // position to avoid Navigator's default minimal scroll to block: 'nearest'.
                // If we are aligned below a combo, move the point of interest to the top.
                // If we are aligned above a combo, move the point of interest to the bottom.
                me.scrollable.scrollIntoView(activeItem, {
                    block
                });

                navigator.activeItem = activeItem;
            }
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
            itemElement = event.currentTarget;

        // Activate soon in case they're moving fast over items.
        if (!DomHelper.isTouchEvent && itemElement && me.navigator && me.activateOnMouseover) {
            me.setTimeout({
                fn                : 'handleMouseOver',
                delay             : 30,
                args              : [itemElement],
                cancelOutstanding : true
            });
        }
    }

    handleMouseOver(itemElement) {
        this.navigator.activeItem = itemElement;
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

        if (me.disabled) {
            return;
        }

        switch (event.key) {
            case ' ':
                // If it's an input field that's providing events, allow it to process space.
                // Otherwise, it falls through and is processed the same as ENTER to make a click.
                if (event.target.nodeName.toUpperCase() === 'INPUT' && !event.target.readOnly) {
                    break;
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
        const
            me                   = this,
            { allowGroupSelect } = me,
            index                = parseInt(item.dataset.index),
            record               = me.store.getAt(index),
            isSpecialRow         = record?.isSpecialRow;

        if (me.disabled || me.trigger('beforeItem', { item, record, index, event, userAction : true }) === false) {
            return;
        }

        me._isUserAction = true;

        // It's the selectAll item
        if (item.contains(event.target.closest('[data-select-all]'))) {
            me.onSelectAllClick(item, event);
        }
        // Clicking on any element with the data-noselect attribute means no selection
        else if (record && !item.contains(event.target.closest('[data-noselect]')) && (!isSpecialRow || allowGroupSelect)) {
            const
                { selected }    = me,
                { groupParent } = record.instanceMeta(this.store);

            if (me.multiSelect) {
                let isSelected  = selected.includes(record),
                    toAddRemove = record;

                if (me.toggleAllIfCtrlPressed && event.ctrlKey) {
                    toAddRemove = me.store.records;
                }
                else if (allowGroupSelect && isSpecialRow) {
                    isSelected = !record.groupChildren.some(rec => !selected.includes(rec));
                    item.classList.toggle('b-selected', !isSelected);
                    toAddRemove = record.groupChildren;
                }
                selected[isSelected ? 'remove' : 'add'](toAddRemove);

                // Update the selected status of the group parent
                if (!isSpecialRow && groupParent && allowGroupSelect) {
                    const groupItem = me.getItem(groupParent);

                    groupItem.classList.toggle('b-selected', groupParent.groupChildren.every(r => selected.includes(r)));
                }
            }
            else {
                selected.splice(0, selected.count, record);
            }
        }

        if (me.isDestroyed) {
            return;
        }

        me.lastClicked = record;

        me.trigger('item', {
            item,
            record,
            index,
            event,
            userAction : true
        });

        me._isUserAction = false;
    }

    /**
     * Handles items being added or removed from the selected Collection
     * @param {Object} changeEvent
     * @private
     */
    onSelectionChange({ action, removed, added, replaced }) {
        const
            me = this,
            {
                selectedCls,
                unselectedCls
            } = me;

        let record, item;

        if (action === 'clear') {
            for (item of me.element.querySelectorAll(`.${selectedCls}`)) {
                item.classList.remove(selectedCls);
                item.setAttribute('aria-selected', false);
                unselectedCls && item.classList.remove(unselectedCls);
            }
        }
        else if (action !== 'replaceValues') {
            for (record of removed) {
                item = me.getItem(record);
                if (item) {
                    item.classList.remove(selectedCls);
                    item.setAttribute('aria-selected', false);
                }
            }
            for (record of added) {
                item = me.getItem(record);
                if (item) {
                    item.classList.add(selectedCls);
                    item.setAttribute('aria-selected', true);
                }
            }
        }

        me.selectAllItem && me.updateSelectAllState();
    }

    onSelectAllClick(item) {
        const checked = item.classList.contains(this.selectedCls);

        if (checked) {
            this.deselectAll();
        }
        else {
            this.selectAll();
        }
    }

    updateSelectAllState() {
        this.element.querySelector('.b-select-all-item')?.classList.toggle(this.selectedCls, this.allSelected);
    }

    /**
     * Yields `true` if all the available items are selected.
     * @property {Boolean}
     * @readonly
     */
    get allSelected() {
        const { selected, store } = this;

        return selected.count && (store.isFiltered ? store.records.every(r => selected.includes(r)) : store.count === selected.count);
    }

    /**
     * Selects all items in this list.
     * @category Selection
     */
    selectAll(keyEvent) {
        const me = this;

        // A programmatic selectAll must work. A UI-invoked call must honour multiSelect:false
        if (me.multiSelect || !keyEvent) {
            me.selected.add(me.store.records);
            if (keyEvent && !me.navigator.activeItem) {
                me.navigator.activeItem = me.getItem(-1);
            }
        }
    }

    /**
     * Deselects all selected items
     * @category Selection
     */
    deselectAll() {
        this.selected.clear();
    }

    /**
     * Selects the passed item(s).
     *
     * An item to select may be the `id` of a record in this List's {@link #config-store}, or
     * it may be an object with an `id` __property__ which is the `id` of a record in this List's
     * {@link #config-store} (For example one of the records).
     * @param {String|String[]|Number|Number[]|Object|Object[]} toSelect
     * @category Selection
     */
    select(toSelect) {
        const
            { store }      = this,
            { modelClass } = store;

        toSelect = ArrayHelper.asArray(toSelect).reduce((v, c) => {
            c = store.getById(modelClass.asId(c));
            if (c) {
                v.push(c);
            }
            return v;
        }, []);

        this.selected.add(toSelect);
    }

    /**
     * Deselects the passed item(s).
     *
     * An item to deselect may be the `id` of a record in this List's {@link #config-store}, or
     * it may be an object with an `id` __property__ which is the `id` of a record in this List's
     * {@link #config-store} (For example one of the records).
     * @param {String|String[]|Number|Number[]|Object|Object[]} toSelect
     * @category Selection
     */
    deselect(toDeselect) {
        const { modelClass } = this.store;

        toDeselect = ArrayHelper.asArray(toDeselect).reduce((v, c) => {
            c = this.selected.get(modelClass.asId(c));
            if (c) {
                v.push(c);
            }
            return v;
        }, []);

        this.selected.remove(toDeselect);
    }
}

// Register this widget type with its Factory
List.initClass();

List.prototype.navigatorClass = Navigator;
