import Container from './Container.js';
import EventHelper from '../helper/EventHelper.js';
import DomHelper from '../helper/DomHelper.js';
import './layout/Card.js';

/**
 * @module Core/widget/TabPanel
 */

/**
 * Tab panel widget, displays a collection of tabs which each can contain other widgets. Layout is handled using css
 *
 * @extends Core/widget/Container
 * @example
 * let tabPanel = new TabPanel({
 *  items: [
 *      {
 *          title: 'First',
 *          items: [
 *              { type: 'textfield', label: 'Name' },
 *              ...
 *          ]
 *      }, {
 *          title: 'Last',
 *          items: [
 *              ...
 *          ]
 *      }
 *  ]
 * });
 *
 * @classType tabpanel
 * @externalexample widget/TabPanel.js
 */
export default class TabPanel extends Container {
    //region Config
    static get $name() {
        return 'TabPanel';
    }

    // Factoryable type name
    static get type() {
        return 'tabpanel';
    }

    // Factoryable type alias
    static get alias() {
        return 'tabs';
    }

    static get configurable() {
        return {
            // Prevent child panels from displaying a header unless explicitly configured with one
            suppressChildHeaders : true,

            /**
             * The index of the initially active tab.
             * @config {Number}
             * @default
             */
            activeTab : 0,

            /**
             * Min width of a tab title. 0 means no minimum width. This is default.
             * @config {Number}
             * @default
             */
            tabMinWidth : null,

            /**
             * Max width of a tab title. 0 means no maximum width. This is default.
             * @config {Number}
             * @default
             */
            tabMaxWidth : null,

            /**
             * Specifies whether to slide tabs in and out of visibility.
             * @config {Boolean}
             * @default
             */
            animateTabChange : true
        };
    }

    /**
     * The index of the initially active tab.
     * @property {Number}
     * @name activeTab
     * @member
     */

    static get defaultConfig() {
        return {
            itemCls : 'b-tabpanel-item',

            defaultType : 'container',

            layout : 'card'
        };
    }

    //endregion

    //region Init

    construct(config) {
        const me = this;

        super.construct(config);

        // assign elements to titleElement, so they can get updated automatically when title is changed
        me.items.forEach((tab, i) => {
            tab.titleElement = me.element.querySelector(`div.b-tabpanel-tab[data-index="${i}"] span`);
        });

        EventHelper.on({
            element  : me.tabStrip,
            delegate : '.b-tabpanel-tab',
            click    : 'onTabElementClick',
            thisObj  : this
        });
    }

    changeElement() {
        const me = this;

        return super.changeElement({
            children : [{
                className : 'b-tabpanel-tabs',
                reference : 'tabStrip',
                children  : me.items.map((childItem, i) => {
                    return {
                        className : {
                            'b-tabpanel-tab' : 1,
                            'b-active'       : i === me.activeTab,
                            [childItem.cls]  : childItem.cls,
                            'b-hidden'       : childItem.$hideTab
                        },
                        style : {
                            minWidth : DomHelper.setLength(me.tabMinWidth),
                            maxWidth : DomHelper.setLength(me.tabMaxWidth)
                        },
                        tabindex : -1,
                        dataset  : {
                            index : i
                        },
                        children : [{
                            tag       : 'span',
                            className : 'b-tabpanel-tab-title',
                            html      : childItem.title
                        }]
                    };
                })
            }, {
                className : {
                    'b-tabpanel-body' : 1
                },
                reference : 'tabPanelBody'
            }]
        });
    }

    onChildAdd(child) {
        // If the items come in as hidden, assume they are to be not available.
        // This flag causes the tab selectors to be rendered hidden.
        if (child.hidden) {
            child.$hideTab = true;
        }

        // The layout will hide inactive new items.
        // The $hideTab flag is for items which were preconfigured as hidden.
        // And we must add our beforeHide listener *after* call super.
        super.onChildAdd(child);

        child.on({
            beforeHide : 'onBeforeChildHide',
            thisObj    : this,
            prio       : 1000 // We must know before the layout intercepts and activates a sibling
        });
    }

    onChildRemove(child) {
        super.onChildRemove(child);
        child.un({
            beforeHide : 'onBeforeChildHide',
            thisObj    : this
        });
    }

    //endregion

    updateTabMinWidth(tabMinWidth) {
        this.element && DomHelper.forEachSelector(this.element, '.b-tabpanel-tab', tab => {
            DomHelper.setLength(tab, 'minWidth', tabMinWidth || null);
        });
    }

    updateTabMaxWidth(tabMaxWidth) {
        this.element && DomHelper.forEachSelector(this.element, '.b-tabpanel-tab', tab => {
            DomHelper.setLength(tab, 'maxWidth', tabMaxWidth || null);
        });
    }

    changeLayout(layout) {
        layout = super.changeLayout(layout);
        layout.activeIndex = this.activeTab;
        layout.animateCardChange = this.animateTabChange;
        return layout;
    }

    //region Tabs

    get contentElement() {
        return this.tabPanelBody;
    }

    get focusElement() {
        const
            me              = this,
            activeTab       = me.items[me.activeTab || 0],
            tabFocusElement = activeTab && activeTab.focusElement;

        return tabFocusElement || me.tabStrip.children[me.activeTab];
    }

    changeActiveTab(activeTab) {
        return parseInt(activeTab, 10);
    }

    updateActiveTab(activeTab) {
        const me = this;

        if (typeof activeTab === 'number') {
            if (activeTab < 0 || activeTab >= me.items.length) {
                throw new Error('Invalid tab index: ' + activeTab);
            }
        }
        // Must be a child widget, so add if it's not already in our items.
        else if (this.items.indexOf(activeTab) === -1) {
            activeTab = me.add(activeTab);
        }

        if (!me.isConfiguring) {
            me.layout.activeItem = activeTab;
        }
    }

    // Auto called because Card layout triggers the activeItemChange on its owner
    onActiveItemChange(activeItemChangeEvent) {
        const
            me             = this,
            { tabStrip }   = me,
            {
                prevActiveIndex,
                activeIndex
            }              = activeItemChangeEvent,
            prevTabElement = tabStrip.children[prevActiveIndex];

        // Our UI changes immediately, our state must be accurate
        me.activeTab = activeIndex;

        // Deactivate previous active tab
        if (prevTabElement) {
            prevTabElement.classList.remove('b-active');
        }

        // Activate the new tab
        tabStrip.children[activeIndex].classList.add('b-active');
        tabStrip.children[activeIndex].classList.remove('b-hidden');

        /**
         * The active tab has changed.
         * @event tabChange
         * @param {Core.widget.Widget} prevActiveItem - The previous active child widget.
         * @param {Number} prevActiveIndex - The previous active index.
         * @param {Core.widget.Widget} activeItem - The new active child widget.
         * @param {Number} activeIndex - The new active index.
         */
        me.trigger('tabChange', activeItemChangeEvent);
    }

    /**
     * The active tab index. Setting must be done through {@link #property-activeTab}
     * @property {Number}
     * @readonly
     */
    get activeIndex() {
        return this.layout.activeIndex;
    }

    /**
     * The active child widget. Setting must be done through {@link #property-activeTab}
     * @property {Core.widget.Widget}
     * @readonly
     */
    get activeItem() {
        return this.layout.activeItem;
    }

    //endregion

    //region Events

    onTabElementClick(event) {
        this.activeTab = event.currentTarget.dataset.index;
    }

    onBeforeChildHide({ source : hidingChild }) {
        // If it's a hide that is not part of the layout's deactivating, we must hide the tab selector :(
        if (!hidingChild.$isDeactivating) {
            const tabBarEl = this.element.querySelector(`div.b-tabpanel-tab[data-index="${this.items.indexOf(hidingChild)}"]`);

            tabBarEl.classList.add('b-hidden');
        }
    }

    //endregion
}

// Register this widget type with its Factory
TabPanel.initClass();
