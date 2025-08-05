import Layout from './Layout.js';
import Widget from '../Widget.js';
import EventHelper from '../../helper/EventHelper.js';

/**
 * @module Core/widget/layout/Card
 */

const animationClasses = [
    'b-slide-out-left',
    'b-slide-out-right',
    'b-slide-in-left',
    'b-slide-in-right'
];

/**
 * A helper class for containers which must manage multiple child widgets, of which only one may be visible at once such
 * as a {@link Core.widget.TabPanel}. This class offers an active widget switching API, and optional slide-in,
 * slide-out animations from child to child.
 * @extends Core/widget/layout/Layout
 * @layout
 * @classtype card
 */
export default class Card extends Layout {
    static $name = 'Card';

    static type = 'card';

    static configurable = {
        containerCls : 'b-card-container',

        itemCls : 'b-card-item',

        hideChildHeaderCls : 'b-hide-child-headers',

        /**
         * Specifies whether to slide tabs in and out of visibility.
         * @config {Boolean}
         * @default
         */
        animateCardChange : true,

        /**
         * The active child item.
         * @config {Core.widget.Widget}
         */
        activeItem : null,

        /**
         * The active child index.
         * @config {Number}
         */
        activeIndex : null
    };

    onChildAdd(item) {
        super.onChildAdd(item);

        const
            me = this,
            {
                activeItem,
                owner
            }           = me,
            activeIndex = owner.activeIndex != null ? owner.activeIndex : (me.activeIndex || 0),
            itemIndex   = owner.items.indexOf(item),
            isActive    = activeItem != null ? item === activeItem : itemIndex === activeIndex;

        item.ion({
            beforeHide : 'onBeforeChildHide',
            beforeShow : 'onBeforeChildShow',
            thisObj    : me
        });

        // Ensure inactive child items start hidden, and the active one starts shown.
        // Sync our active indicators with reality ready for render.
        if (isActive) {
            me._activeIndex = itemIndex;
            me._activeItem = item;
            item.show();
        }
        else {
            item.$isDeactivating = true;
            item.hide();
            item.$isDeactivating = false;
        }
    }

    onChildRemove(item) {
        super.onChildRemove(item);

        const me = this;

        // Active child has been removed without setting another child to be active.
        // Choose an immediate sibling to be the new active item
        if (me._activeItem === item) {
            me.activateSiblingOf(item);
        }

        me._activeIndex = me.owner.items.indexOf(me._activeItem);

        item.un({
            beforeHide : 'onBeforeChildHide',
            beforeShow : 'onBeforeChildShow',
            thisObj    : me
        });
    }

    /**
     * Detect external code showing a child. We veto that show and activate it through the API.
     * @internal
     */
    onBeforeChildShow({ source : showingChild }) {
        // Some outside code is showing a child.
        // We must control this, so veto it and activate it in the standard way.
        if (!this.owner.isConfiguring && !showingChild.$isActivating) {
            this.activeItem = showingChild;
            return false;
        }
    }

    /**
     * Detect external code hiding a child. We veto that show and activate an immediate sibling through the API.
     * @internal
     */
    onBeforeChildHide({ source : hidingChild }) {
        // Some outside code is hiding a child.
        // We must control this, so veto it and activate a sibling in the standard way.
        if (!this.owner.isConfiguring && !hidingChild.$isDeactivating) {
            this.activateSiblingOf(hidingChild);
            return false;
        }
    }

    activateSiblingOf(item) {
        const
            { owner } = this,
            items     = owner.items.slice(),
            removeAt  = items.indexOf(item);

        items.splice(removeAt, 1);

        this.activeIndex = Math.min(removeAt, items.length - 1);
    }

    /**
     * Get/set active item, using index or the Widget to activate
     * @param {Core.widget.Widget|Number} activeIndex
     * @param {Number} [prevActiveIndex]
     * @param {Object} [options]
     * @param {Boolean} [options.animation] Pass `false` to disable animation
     * @param {Boolean} [options.silent] Pass `true` to not fire transition events
     * @returns {Object} An object describing the card change containing the following properties:
     *  - `prevActiveIndex` The previously active index.
     *  - `prevActiveItem ` The previously active child item.
     *  - `activeIndex    ` The newly active index.
     *  - `activeItem     ` The newly active child item.
     *  - `promise        ` A promise which completes when the slide-in animation finishes and the child item contains
     * focus if it is focusable.
     * @internal
     */
    setActiveItem(activeIndex, prevActiveIndex = this.activeIndex, options) {
        const
            me             = this,
            { owner }      = me,
            { items }      = owner,
            widgetPassed   = activeIndex instanceof Widget,
            prevActiveItem = items[prevActiveIndex],
            newActiveItem  = owner.items[activeIndex = widgetPassed ? items.indexOf(activeIndex) : parseInt(activeIndex, 10)],
            animation      = options?.animation !== false,
            chatty         = !options?.silent,
            event = {
                prevActiveIndex,
                prevActiveItem
            };

        // There's a child widget at that index to activate and we're not already activating it.
        if (newActiveItem && !newActiveItem.$isActivating && newActiveItem !== prevActiveItem) {
            const
                prevItemElement = prevActiveItem && prevActiveItem.element,
                newActiveElement = newActiveItem && newActiveItem.element;

            // A previous card change is in progress, abort it and clean the items it was operating upon
            if (me.animateDetacher) {
                const activeCardChange = me.animateDetacher.event;

                // The animation that is in flight is already doing what we are being asked for.
                // Allow it to complete.
                if (activeCardChange.activeItem === newActiveItem) {
                    return activeCardChange.promise;
                }
                me.animateDetacher();
                activeCardChange.prevActiveItem.element.classList.remove(...animationClasses);
                activeCardChange.activeItem.element.classList.remove(...animationClasses);
                me.animateDetacher = null;
            }

            event.activeIndex = activeIndex;
            event.activeItem = newActiveItem;

            /**
             * The active item is about to be changed. Return `false` to prevent this.
             * @event beforeActiveItemChange
             * @preventable
             * @on-owner
             * @param {Number} activeIndex - The new active index.
             * @param {Core.widget.Widget} activeItem - The new active child widget.
             * @param {Number} prevActiveIndex - The previous active index.
             * @param {Core.widget.Widget} prevActiveItem - The previous active child widget.
             */
            if (chatty && owner.trigger('beforeActiveItemChange', event) === false) {
                return null;
            }

            // Since onBeforeActiveItemChange happens before event handlers run, the activation could be cancelled by
            // a listener, so we do a special hook once we are sure things are going down.
            // We pretend that we have already switched active index so that the owner
            // does not attempt to initiate the change.
            const reset = me._activeIndex !== event.activeIndex;

            if (reset) {
                me._activeIndex = event.activeIndex;
            }

            chatty && owner.onBeginActiveItemChange?.(event);

            if (reset) {
                me._activeIndex = event.prevActiveIndex;
            }

            // If we're animating and there's something to slide out
            // then slide it out, and slide the new item in
            if (animation && prevItemElement && owner.isVisible && me.animateCardChange) {
                event.promise = me.cardChangeAnimation = new Promise((resolve, reject) => {                   // During the card sliding trick, we don't want resize notifications.
                    // The outgoing card should be as inert as if it were hidden.
                    const wasMonitoringSize = prevActiveItem.monitorResize;
                    prevActiveItem.monitorResize = false;

                    me.contentElement.style.overflowX = 'hidden';

                    // The outgoing card must report its isVisible property as false from now on
                    // even before we officially hide it.
                    prevActiveItem._hidden = true;

                    // Show the item so that it can be slid in.
                    // Events will ensue, UIs can react to the show event.
                    // The flag is so that our onBeforeChildShow listener can
                    // tell if it's part of our orderly activate operation.
                    newActiveItem.$isActivating = true;
                    newActiveItem.show();
                    newActiveItem.$isActivating = false;

                    prevItemElement.classList.add(activeIndex > prevActiveIndex ? 'b-slide-out-left' : 'b-slide-out-right');
                    newActiveElement.classList.add(activeIndex < prevActiveIndex ? 'b-slide-in-left' : 'b-slide-in-right');
                    owner.isAnimating = true;

                    // When the new widget is in place, clean up
                    me.animateDetacher = EventHelper.onTransitionEnd({
                        mode    : 'animation',
                        element : newActiveElement,

                        // onTransitionEnd protects us from being called
                        // after the thisObj is destroyed.
                        thisObj : prevActiveItem,

                        handler()  {
                            // Calendar got stuck with `b-animating` in some monkey scenarios, hoisted this to make
                            // sure it was not left behind
                            owner.isAnimating = me.cardChangeAnimation = false;

                            // if animateDetacher variable has been cleared before this callback,
                            // this means race-condition call happened. active item should be called again to
                            // prevent unexpected layout behaviour
                            if (!me.animateDetacher) {
                                me.setActiveItem(activeIndex, prevActiveIndex, options);
                                return;
                            }

                            me.animateDetacher = null;

                            // Clean incoming widget's animation classes
                            newActiveElement.classList.remove(...animationClasses);

                            // If there's an outgoing item, clean its animation classes and hide it
                            if (prevItemElement) {
                                prevItemElement.classList.remove(...animationClasses);

                                // The flag is so that our onBeforeChildHide listener can
                                // tell if it's part of our orderly activate operation.
                                prevActiveItem.$isDeactivating = true;
                                prevActiveItem._hidden = false;
                                prevActiveItem.hide();
                                prevActiveItem.monitorResize = wasMonitoringSize;
                                prevActiveItem.$isDeactivating = false;
                            }

                            me.contentElement.style.overflowX = '';
                            me.onActiveItemChange(event, resolve, !chatty);
                        }
                    });

                    me.animateDetacher.reject = reject;
                    me.animateDetacher.event = event;
                });
            }
            // Nothing to slide out or we are not animating.
            else {
                // Show the new active items first, so that the hide listener doesn't
                // automatically set a new active item based on active item being hidden.
                // The flag is so that our onBeforeChildShow listener can
                // tell if it's part of our orderly activate operation.
                newActiveItem.$isActivating = true;
                newActiveItem.show();

                // focus the new item before lost the component focus when hide the old one
                // (because losing focus closes owner if it is floatable)
                newActiveItem.focus();

                newActiveItem.$isActivating = false;

                if (prevActiveItem) {
                    // The flag is so that our onBeforeChildHide listener can
                    // tell if it's part of our orderly activate operation.
                    prevActiveItem.$isDeactivating = true;
                    prevActiveItem.hide();
                    prevActiveItem.$isDeactivating = false;
                }

                me.onActiveItemChange(event, null, !chatty);
            }
        }

        return event;
    }

    onActiveItemChange(event, resolve, silent) {
        const me = this;

        me._activeItem = event.activeItem;
        me._activeIndex = event.activeIndex;

        /**
         * The active item has changed.
         * @event activeItemChange
         * @on-owner
         * @param {Number} activeIndex - The new active index.
         * @param {Core.widget.Widget} activeItem - The new active child widget.
         * @param {Number} prevActiveIndex - The previous active index.
         * @param {Core.widget.Widget} prevActiveItem - The previous active child widget.
         */
        !silent && me.owner.trigger('activeItemChange', event);

        // Note that we have to call focus *after* the element is in its new position
        // because focus({preventScroll:true}) is not supported everywhere
        // and crazy browser scrolling behaviour on focus breaks the animation.
        me.owner.containsFocus && event.activeItem.focus();

        resolve?.(event);
    }

    renderChildren() {
        const { owner } = this;

        owner.contentElement.classList.toggle(this.hideChildHeaderCls, owner.suppressChildHeaders);

        super.renderChildren();
    }

    changeActiveIndex(activeIndex) {
        const { owner } = this;

        // Sanitize it if possible
        return owner.isConfiguring && !owner._items ? activeIndex : Math.min(activeIndex, owner.items.length - 1);
    }

    updateActiveIndex(activeIndex, oldActiveIndex) {
        if (!this.owner.isConfiguring) {
            this.setActiveItem(activeIndex, oldActiveIndex);
        }
    }

    updateActiveItem(activeItem) {
        if (!this.owner.isConfiguring) {
            this.setActiveItem(activeItem, this.activeIndex);
        }
    }

    /**
     * If the layout is set to {@link #config-animateCardChange}, then this property
     * will be `true` during the animated card change.
     * @property {Boolean}
     * @readonly
     */
    get isChangingCard() {
        return Boolean(this.animateDetacher);
    }
}

// Layouts must register themselves so that the static layout instantiation
// in Layout knows what to do with layout type names
Card.initClass();
