import Base from '../../Base.js';
import DomHelper from '../DomHelper.js';
import EventHelper from '../EventHelper.js';
import Events from '../../mixin/Events.js';
import Factoryable from '../../mixin/Factoryable.js';
import StringHelper from '../StringHelper.js';

/**
 * @module Core/helper/util/Navigator
 */

/**
 * A helper class which allows keyboard navigation within the {@link #config-target} element.
 * @private
 */
export default class Navigator extends Base.mixin(Events, Factoryable) {
    static get $name() {
        return 'Navigator';
    }

    // Factoryable type name
    static get type() {
        return 'navigator';
    }

    static get configurable() {
        return {
            /**
             * The owning Widget which is using this Navigator.
             * @config {Core.widget.Widget}
             */
            ownerCmp : null,

            /**
             * If the items in the owning widget are naturally tabbable, then the Navigator does not
             * need to listen for navigation keys and move focus. It just reacts to natural focus
             * movement.
             * @config {Boolean}
             */
            itemsTabbable : null,

            /**
             * The encapsulating element in which navigation takes place.
             * @config {HTMLElement}
             */
            target : null,

            /**
             * The element which provides key events for navigation. Optional. Defaults to the {@link #config-target} element.
             * @config {HTMLElement}
             */
            keyEventTarget : null,

            /**
             * An optional key event processor which may preprocess the key event. Returning `null` prevents processing of the event.
             * @config {Function}
             */
            processEvent : null,

            /**
             * A query selector which identifies descendant elements within the {@link #config-target} which are navigable.
             * @config {String}
             */
            itemSelector : null,

            /**
             * The currently focused element within the {@link #config-target}.
             * @config {HTMLElement}
             */
            activeItem : null,

            /**
             * A CSS class name to add to focused elements.
             * @config {String}
             * @default
             */
            focusCls : 'b-active',

            /**
             * An object containing key definitions keyed by the key name eg:
             *
             * ```javascript
             *  keys : {
             *      "CTRL+Space" : 'onCtrlSpace',
             *      Enter        : 'onEnterKey'
             *  }
             * ```
             *
             * The {@link #config-ownerCmp} is used as the `this` reference and to resolve string method names.
             *
             * Modified key names must be created prepending one or more `'CTRL+'`, `'SHIFT+'`, `'ALT+'`
             * in that order, for example `"CTRL+SHIFT+Enter" : 'showMenu'`
             * @config {Object<String,String>}
             * @default
             */
            keys : null,

            /**
             * Configure as `true` to also navigate when the `CTRL` modifier key is used along with
             * navigation keys.
             * @config {Boolean}
             * @default false
             */
            allowCtrlKey : null,

            /**
             * Configure as `true` to also navigate when the `SHIFT` modifier key is used along with
             * navigation keys.
             * @config {Boolean}
             * @default false
             */
            allowShiftKey : null,

            scrollDuration : 50,

            /**
             * Configure as, or set to `true` to disable the processing of keys.
             * @config {Boolean}
             */
            disabled : null,

            datasetIdProperty : 'id',

            testConfig : {
                scrollDuration : 1
            }
        };
    }

    static get factoryable() {
        return {
            defaultType : 'navigator'
        };
    }

    get activeItem() {
        const { _activeItem } = this;

        if (this.target.contains(_activeItem)) {
            return _activeItem;
        }
        this._activeItem = null;
    }

    updateOwnerCmp(ownerCmp) {
        if (!this.itemSelector) {
            this.itemSelector = `.${this.ownerCmp.itemCls}`;
        }

        DomHelper.setAttributes(this.keyEventTarget, {
            'aria-activedescendant' : `${this.owner.id}-active-descendant`
        });
    }

    set navigationEvent(navigationEvent) {
        const { owner } = this;

        // Both us and our owning component need to know about the navigation event.
        // Used by the owning component's navigation to detect what interaction event if any caused
        // the focus to be moved. If it's a programmatic focus, there won't be one.
        this._navigationEvent = owner.navigationEvent = navigationEvent;

        // But it's transient. As soon as it has been processed, it goes.
        if (navigationEvent) {
            Promise.resolve(1).then(() => this.navigationEvent = null);
        }
    }

    get navigationEvent() {
        return this._navigationEvent;
    }

    static getComposedKeyName(keyEvent) {
        const keyName = (keyEvent.key || '').trim() || keyEvent.code;

        return `${keyEvent.ctrlKey ? 'CTRL+' : ''}${keyEvent.shiftKey ? 'SHIFT+' : ''}${keyEvent.altKey ? 'ALT+' : ''}${keyName}`;
    }

    onKeyDown(keyEvent) {
        const
            me              = this,
            {
                ownerCmp,
                itemSelector,
                activeItem,
                itemsTabbable
            } = me,
            { target }      = keyEvent,
            firstItem       = me.target.querySelector(itemSelector),
            // Not all key events have 'key'
            keyName         = (keyEvent.key || '').trim() || keyEvent.code,
            composedKeyName = me.constructor.getComposedKeyName(keyEvent),
            validTarget     = target.matches(itemSelector) || target === me.keyEventTarget;

        // Feed the key event through our configured processor, process the event that returns if any.
        // We need to do this even if no items because there may be other widgets inside the owner.
        if (!me.disabled && me.processEvent) {
            keyEvent = me.processEvent.call(ownerCmp, keyEvent);
        }

        // Process the key gesture if there are items and we are visible.
        // Also, if key emanated from a valid target element (Not an owned positioned Widget).
        if (keyEvent && !me.disabled && firstItem && me.target.offsetParent && validTarget) {

            // Only set navigation key names if we are *not* using tabbing.
            if (!itemsTabbable) {
                // Detect whether the navigable items flow inline or downwards.
                if (me.inlineFlow == null) {
                    const
                        itemContainer     = firstItem.parentNode,
                        itemPositionStyle = DomHelper.getStyleValue(firstItem, 'position'),
                        itemDisplayStyle  = DomHelper.getStyleValue(firstItem, 'display'),
                        itemFloatStyle    = DomHelper.getStyleValue(firstItem, 'float');

                    // This is how we know that RIGHT and LEFT arrow should be used for next and previous.
                    // If inlineFlow is false, we use UP and DOWN. Consider tabs in a tab bar.
                    me.inlineFlow = (
                        itemPositionStyle === 'absolute' ||
                        itemDisplayStyle === 'inline' || itemDisplayStyle === 'inline-block' ||
                        itemFloatStyle === 'left' || itemFloatStyle === 'right' ||
                        (DomHelper.getStyleValue(itemContainer, 'display') === 'flex' && DomHelper.getStyleValue(itemContainer, 'flex-direction') === 'row')
                    );
                }

                if (!me.prevKey) {
                    if (me.inlineFlow) {
                        me.prevKey = 'ArrowLeft';
                        me.nextKey = 'ArrowRight';
                    }
                    else {
                        me.prevKey = 'ArrowUp';
                        me.nextKey = 'ArrowDown';
                    }
                }
            }

            // So that we and our owning component know how we are being told to navigate
            me.navigationEvent = keyEvent;

            if (activeItem) {
                switch (keyName) {
                    case me.prevKey:
                        if (me.disabled || keyEvent.ctrlKey && !me.allowCtrlKey) {
                            return;
                        }
                        if (keyEvent.shiftKey && !me.allowShiftKey) {
                            return;
                        }
                        keyEvent.preventDefault();
                        /* Flagging the event as handled to let KeyMap know that it should ignore it. Need to do it here
                         * because navigatePrevious is throttled in Scheduler.
                         */
                        keyEvent.handled = true;
                        me.navigatePrevious(keyEvent);
                        break;
                    case me.nextKey:
                        if (me.disabled || keyEvent.ctrlKey && !me.allowCtrlKey) {
                            return;
                        }
                        if (keyEvent.shiftKey && !me.allowShiftKey) {
                            return;
                        }
                        keyEvent.preventDefault();
                        /* Flagging the event as handled to let KeyMap know that it should ignore it. Need to do it here
                         * because navigatePrevious is throttled in Scheduler.
                         */
                        keyEvent.handled = true;
                        me.navigateNext(keyEvent);
                        break;
                    default: {
                        const keyHandler = me.keys?.[composedKeyName];

                        if (keyHandler && !me.disabled) {
                            if (me.callback(keyHandler, me.thisObj || ownerCmp, [keyEvent]) === false) {
                                return;
                            }
                        }

                        // Note that even if this.disabled, the ownerCmp will expect
                        // to have its key down handler invoked.
                        else if (ownerCmp.onInternalKeyDown) {
                            ownerCmp.onInternalKeyDown(keyEvent);
                        }

                        // One of the handlers moved focus.
                        // This KeyDown should not act upon the new target.
                        // For example popped up a Popup and focused its "close" Tool.
                        if (DomHelper.getActiveElement(target) !== target) {
                            keyEvent.preventDefault();
                        }

                        // No navigation must take place when items are tabbable.
                        if (itemsTabbable) {
                            return;
                        }
                    }
                }
            }
            else {
                // We don't need to handle navigation into the list if the items are naturally tabbable
                if (itemsTabbable) {
                    return;
                }
                if (keyName === me.nextKey || keyName === me.prevKey) {
                    me.activeItem = me.getDefaultNavigationItem(keyEvent);
                }
            }

            if (me.activeItem !== activeItem) {
                /**
                 * Fired when a user gesture causes the active item to change _or become `null`_.
                 * @event navigate
                 * @param {Event} event The browser event which instigated navigation. May be a click or key or focus move event.
                 * @param {HTMLElement} item The newly active item, or `null` if focus moved out.
                 * @param {HTMLElement} oldItem The previously active item, or `null` if focus is moving in.
                 */
                me.triggerNavigate(keyEvent);
            }
        }
    }

    getDefaultNavigationItem(keyEvent) {
        const { target, itemSelector, prevKey,  nextKey } = this;

        // Navigating backwards from after the component, we default to last item
        if (target.compareDocumentPosition(keyEvent.target) & 4 && keyEvent.key === prevKey) {
            return target.querySelector(`${itemSelector}:last-of-type`);
        }
        // Navigating forwards from before the component we default to the first item
        if (target.compareDocumentPosition(keyEvent.target) & 2 && keyEvent.key === nextKey) {
            return target.querySelector(`${itemSelector}`);
        }
    }

    navigatePrevious(keyEvent) {
        const me = this,
            previous = me.previous;

        keyEvent.preventDefault();
        if (previous) {
            me.ownerCmp.scrollable.scrollIntoView(previous, { animate : me.scrollDuration }).then(() => {
                me.activeItem = previous;
                me.triggerNavigate(keyEvent);
            });
        }
    }

    navigateNext(keyEvent) {
        const me = this,
            next = me.next;

        keyEvent.preventDefault();
        if (next) {
            me.ownerCmp.scrollable.scrollIntoView(next, { animate : me.scrollDuration }).then(() => {
                me.activeItem = next;
                me.triggerNavigate(keyEvent);
            });
        }
    }

    get owner() {
        return this.ownerCmp;
    }

    get previous() {
        return this.getAdjacent(-1);
    }

    get next() {
        return this.getAdjacent(1);
    }

    /**
     * Returns the next or previous navigable element starting from the passed `from` element,
     * navigating in the passed direction.
     * @param {HTMLElement} [from] The start point. Defaults to the current {@link #config-activeItem}
     * @param {Number} [direction=1] The direction. -1 for backwards, else forwards.
     */
    getAdjacent(direction = 1, from = this.activeItem) {
        const treeWalker = this.treeWalker;

        treeWalker.currentNode = from;
        treeWalker[direction < 0 ? 'previousNode' : 'nextNode']();
        if (treeWalker.currentNode !== this.activeItem) {
            return treeWalker.currentNode;
        }
    }

    onTargetFocusIn(focusInEvent) {
        const
            me         = this,
            {
                target,
                relatedTarget
            } = focusInEvent,
            {
                owner,
                itemsTabbable,
                skipScrollIntoView,
                previousActiveItem
            }          = me;

        // Ignore navigating to a focus trap. It will bounce back in
        if (!target.matches('.b-focus-trap')) {
            if (target.matches(me.itemSelector)) {
                // We may need to know this in downstream code.
                // for example set activeItem must not scrollIntoView on click
                me.skipScrollIntoView = true;

                me.activeItem = target;

                me.skipScrollIntoView = skipScrollIntoView;

                // If we are using natural, TAB based navigation, trigger the navigate event after it really happens.
                if (me.activeItem && itemsTabbable) {
                    me.triggerNavigate(focusInEvent);
                }
            }
            // Focus onto target
            else if (target === me.target) {
                const fromWhere = relatedTarget ? target.compareDocumentPosition(relatedTarget) : 0;

                // Upward focusing from within means a SHIFT+TAB, so go to previous sibling
                if (fromWhere & Node.DOCUMENT_POSITION_CONTAINED_BY) {
                    owner.previousSibling?.focus();
                }
                // From outside means go to last active item or first item
                else {
                    me.activeItem = previousActiveItem && me.target.contains(previousActiveItem) ? previousActiveItem : 0;
                }
            }
        }
    }

    onTargetFocusOut(focusOutEvent) {
        const
            me                = this,
            { relatedTarget } = focusOutEvent;

        // Ignore navigating to a focus trap. It will bounce back in
        if (!relatedTarget?.matches('.b-focus-trap')) {
            if (!relatedTarget || !me.target.contains(relatedTarget) || !relatedTarget.matches(me.itemSelector)) {
                if (me.activeItem) {
                    me.activeItem = null;

                    // If we are using natural, TAB based navigation, trigger the navigate event after it really happens.
                    if (me.itemsTabbable) {
                        me.triggerNavigate(focusOutEvent);
                    }
                }
            }
        }
    }

    onTargetMouseDown(mouseDownEvent) {
        const
            me     = this,
            target = mouseDownEvent.target.closest(me.itemSelector);

        me.navigationEvent = mouseDownEvent;

        if (me.itemsTabbable) {
            // We will already be focused, but selection is driven off navigation
            // so announce that we have "renavigated" to the curremt active item.
            if (target === me.activeItem) {
                me.triggerNavigate(mouseDownEvent);
            }
        }
        // Mousedown is the focus gesture.
        // This holds true even on touch platforms
        // where the mousedown event is synthesized -
        // preventing default prevents focus on the upcoming touchend.
        else if (me.ownerCmp.itemsFocusable === false) {
            me.onFocusGesture(mouseDownEvent);
        }
        // Preempt browser's focusing behaviour which focuses the closest focusable
        // element, and scrolls it into view.
        else if (target) {
            mouseDownEvent.preventDefault();
            // We attempt to focus the target without scrolling.
            DomHelper.focusWithoutScrolling(target);
        }
    }

    onTargetClick(clickEvent) {
        const
            me                     = this,
            { skipScrollIntoView } = me;

        if (me.skipNextClick || me.navigationEvent?.ignoreNavigation) {
            me.skipNextClick = false;
            return;
        }

        if (me.disabled) {
            return;
        }

        // We may need to know this in downstream code.
        // for example set activeItem must not scrollIntoView on click
        me.skipScrollIntoView = true;

        // ownerCmp's preprocessing of any navigate event.
        if (me.processEvent) {
            clickEvent = me.processEvent.call(me.ownerCmp, clickEvent);
        }

        if (clickEvent) {
            me.activeItem = clickEvent.target.closest(me.itemSelector);
            me.triggerNavigate(clickEvent);
        }
        me.skipScrollIntoView = skipScrollIntoView;
    }

    // We have to prevent focus moving from eg, an input field when we mousedown
    // or touchtap a non focusable item when ownerCmp has itemsFocusable: false.
    // If the event was not on an item, we must allow it through to allow Lists
    // to contain other widgets.
    onFocusGesture(event) {
        if (event.target === this.ownerCmp.contentElement || (event.target.closest(this.itemSelector) && this.ownerCmp.itemsFocusable === false)) {
            event.preventDefault();
        }
    }

    acceptNode(node) {
        return node.offsetParent && node.matches && node.matches(this.itemSelector) ? DomHelper.NodeFilter.FILTER_ACCEPT : DomHelper.NodeFilter.FILTER_SKIP;
    }

    changeActiveItem(activeItem) {
        if (activeItem != null) {
            // List and Menu's getItem API allows number, or node or record or record id to be passed
            if (this.ownerCmp.getItem) {
                activeItem = this.ownerCmp.getItem(activeItem);
            }
        }
        return activeItem;
    }

    updateActiveItem(activeItem, oldActiveItem) {
        const
            me           = this,
            { ownerCmp } = me,
            isActive     = oldActiveItem && me.target.contains(oldActiveItem),
            // If we are being called in response to focus movement, it will already be the document.activeElement
            // so in that case, behave is if we were in non-focusing mode, and just add the focused class.
            needsFocus   = activeItem && DomHelper.isFocusable(activeItem) && activeItem !== DomHelper.getActiveElement(activeItem);

        if (isActive) {
            me.previousActiveItem = oldActiveItem;
        }

        if (oldActiveItem) {
            oldActiveItem.classList.remove(me.focusCls);
            oldActiveItem.removeAttribute('id');
        }

        // This may be set to null on focusout of the target element.
        // Cannot use truthiness test because index zero may be passed.
        if (activeItem != null) {
            // If the user was able to click the event, they will not expect it to attempt to scroll.
            if (me.skipScrollIntoView) {
                if (needsFocus) {
                    DomHelper.focusWithoutScrolling(activeItem);
                }
            }
            else {
                ownerCmp.scrollable?.scrollIntoView(activeItem, {
                    block  : 'nearest',
                    focus  : needsFocus,
                    silent : me.scrollSilently
                });
            }

            // No change in active item, do nothing after we've ensured it's fully in view.
            if (activeItem === oldActiveItem && isActive) {
                return;
            }

            activeItem.classList.add(me.focusCls);
            activeItem.id = `${me.owner.id}-active-descendant`;
            me._activeItem = activeItem;
        }
        else {
            me._activeItem = null;

            // We are clearing the activeItem.
            // If it's focused, keep focus close by actively reverting it.
            if (oldActiveItem === DomHelper.getActiveElement(oldActiveItem)) {
                ownerCmp.revertFocus();
            }
        }
    }

    updateTarget(target, oldTarget) {
        const
            me        = this,
            listeners = {
                element  : target,
                thisObj  : me,
                focusin  : 'onTargetFocusIn',
                focusout : 'onTargetFocusOut',

                // If items are tabbable the mousedown handler will not force the issue by focusing
                // the closest item. We just need to trigger the method so that it is hookable
                mousedown : {
                    handler  : 'onTargetMouseDown',
                    delegate : me.itemSelector
                }
            };

        if (!me.itemsTabbable && !me.itemsFocusable) {
            // We only need to listen for clicks if the items cannot receive focus.
            // If focusable in any way, selection is triggered by navigation.
            listeners.click = 'onTargetClick';
        }

        if (!Object.prototype.hasOwnProperty.call(me, 'acceptNode')) {
            me.acceptNode = me.acceptNode.bind(me);
            // https://github.com/webcomponents/webcomponentsjs/issues/556
            // Work around Internet Explorer wanting a function instead of an object.
            // IE also *requires* this argument where other browsers don't.
            me.acceptNode.acceptNode = me.acceptNode;
        }

        EventHelper.on(listeners);

        // This Navigator object acts as the filter for the TreeWalker. We must implement acceptNode(node)
        me.treeWalker = me.setupTreeWalker(target, DomHelper.NodeFilter.SHOW_ELEMENT, me.acceptNode);

        // If we were not configured with an outside key event provider (think the input field providing UP/DOWN keys for the dropdown)
        // then use the target element as the source.
        if (!me.keyEventTarget) {
            me.keyEventTarget = target;
        }



        // If the activeItem gets removed, we must know, and deactivate.
        (me.targetMutationMonitor = new MutationObserver(me.onTargetChildListChange.bind(me))).observe(target, {
            childList : true,
            subtree   : true
        });
    }

    setupTreeWalker(root, whatToShow, filter) {
        // This Navigator object acts as the filter for the TreeWalker. We must implement acceptNode(node)
        return document.createTreeWalker(root, whatToShow, filter);
    }

    onTargetChildListChange() {
        const
            me  = this,
            {
                activeItem,
                datasetIdProperty
            } = me;

        // On DOM mutation, if the activeItem got changed, pull the one with the same ID out again.
        if (activeItem) {
            if (me.target.contains(activeItem)) {
                // This seeminly redundant hack is important.
                // Adding an already present class causes DOM mutation and style recalc.
                if (!activeItem.classList.contains(me.focusCls)) {
                    activeItem.classList.add(me.focusCls);
                }
            }
            else {
                // Try to focus the same record id, or if not present, the same item index.
                // Passing undefined results in the config setter assuming no-change
                // So we must use null to clear.
                me.activeItem =
                    me.target.querySelector(`${me.itemSelector}.${me.focusCls}[data-${StringHelper.hyphenate(datasetIdProperty)}="${activeItem.dataset[datasetIdProperty]}"]`) ||
                    me.target.querySelector(`${me.itemSelector}.${me.focusCls}`)?.[activeItem.dataset.index] || null;
            }
        }
    }

    changeKeyEventTarget(keyEventTarget) {
        this._keyEventTarget = keyEventTarget;

        EventHelper.on({
            element : keyEventTarget,
            keydown : 'onKeyDown',
            thisObj : this
        });
    }

    triggerNavigate(event, item = this.activeItem) {
        const navEvent = {
            event,
            item,
            oldItem : this.previousActiveItem
        };

        this.trigger('navigate', navEvent);
        this.ownerCmp?.trigger('navigate', navEvent);
    }
}
