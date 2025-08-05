import Base from '../../Base.js';
import Events from '../../mixin/Events.js';
import ObjectHelper from '../../helper/ObjectHelper.js';

/**
 * @module Core/widget/layout/Layout
 */

/**
  * A helper class used by {@link Core.widget.Container Container}s which renders child widgets to their
  * {@link Core.widget.Widget#property-contentElement}. It also adds the Container's
  * {@link Core.widget.Container#config-itemCls} class to child items.
  *
  * Subclasses may modify the way child widgets are rendered, or may offer APIs for manipulating the child widgets.
  *
  * The {@link Core.widget.layout.Card Card} layout class offers slide-in, slide-out animation of multiple
  * child widgets. {@link Core.widget.TabPanel} uses Card layout.
  */
export default class Layout extends Events(Base) {

    static get configurable() {
        return {
            /**
             * The CSS class which should be added to the owning {@link Core.widget.Container Container}'s
             * {@link Core.widget.Widget#property-contentElement}.
             */
            containerCls : 'b-auto-container',

            /**
             * The CSS class which should be added to the encapsulating element of child items.
             */
            itemCls : null
        };
    }

    static getLayout(layout, owner) {
        if (layout instanceof Layout) {
            layout.owner = this;
            return layout;
        }

        const config = { owner };

        if (typeof layout === 'string') {
            config.type = layout;
        }
        else {
            ObjectHelper.assign(config, layout);
        }

        return new layoutClasses[config.type || 'default'](config);
    }

    static registerLayout(cls, name = cls.$$name.toLowerCase()) {
        layoutClasses[name] = cls;
    }

    onChildAdd(item) {
    }

    onChildRemove(item) {
    }

    renderChildren() {
        const
            {
                owner,
                containerCls,
                itemCls
            }            = this,
            {
                contentElement,
                items
            }            = owner,
            ownerItemCls = owner.itemCls,
            itemCount    = items && items.length;

        contentElement.classList.add('b-content-element');

        if (containerCls) {
            contentElement.classList.add(containerCls);
        }

        // Need to check that container has widgets, for example TabPanel can have no tabs
        if (itemCount) {
            owner.textContent = false;

            for (let i = 0; i < itemCount; i++) {
                const
                    item = items[i],
                    { element } = item;

                element.dataset.itemIndex = i;
                if (itemCls) {
                    element.classList.add(itemCls);
                }
                if (ownerItemCls) {
                    element.classList.add(ownerItemCls);
                }

                // If instantiated by the app developer, external to Container#createWidget
                // a widget will have the b-outer class. Remove that if it' contained.
                element.classList.remove('b-outer');

                // Only trigger paint if the owner is itself painted, otherwise
                // the outermost Container will cascade the paint signal down.
                item.render(contentElement, Boolean(owner.isPainted));
            }
        }
        this.syncChildCount();
    }

    removeChild(child) {
        const
            { element }        = child,
            { owner, itemCls } = this,
            { contentElement } = owner,
            ownerItemCls       = owner.itemCls;

        contentElement.removeChild(element);
        delete element.dataset.itemIndex;
        if (itemCls) {
            element.classList.remove(itemCls);
        }
        if (ownerItemCls) {
            element.classList.remove(ownerItemCls);
        }
        this.fixChildIndices();
        this.syncChildCount();
    }

    appendChild(child) {
        const
            { element }        = child,
            childIndex         = this.owner.indexOfChild(child),
            { owner, itemCls } = this,
            { contentElement } = owner,
            ownerItemCls       = owner.itemCls;

        element.dataset.itemIndex = childIndex;

        owner.textContent = false;

        if (itemCls) {
            element.classList.add(itemCls);
        }
        if (ownerItemCls) {
            element.classList.add(ownerItemCls);
        }

        child.render(contentElement, Boolean(owner.isPainted));
        this.syncChildCount();
    }

    insertChild(toAdd, childIndex) {
        const
            { element }        = toAdd,
            { owner, itemCls } = this,
            { contentElement } = owner,
            nextSibling        = contentElement.querySelector(`[data-item-index="${childIndex}"]`),
            ownerItemCls       = owner.itemCls;

        owner.textContent = false;

        if (itemCls) {
            element.classList.add(itemCls);
        }
        if (ownerItemCls) {
            element.classList.add(ownerItemCls);
        }

        contentElement.insertBefore(element, nextSibling);
        toAdd.render(null, Boolean(owner.isPainted));
        this.fixChildIndices();
        this.syncChildCount();
    }

    fixChildIndices() {
        this.owner.items.forEach((child, index) => {
            child.element.dataset.itemIndex = index;
        });
    }

    syncChildCount() {
        const
            { owner }  = this,
            { length } = owner.items;

        // Special CSS conditions may apply if there's only a single child.
        owner.contentElement.classList[length === 1 ? 'add' : 'remove']('b-single-child');
    }

    /**
     * The owning Widget
     * @property {String} owner
     * @readonly
     */
}

const layoutClasses = {
    default : Layout
};
