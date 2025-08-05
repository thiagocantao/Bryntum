import Base from '../../Base.js';
import Events from '../../mixin/Events.js';

/**
 * @module Common/widget/layout/Layout
 */

/**
  * A helper class used by {@link Common.widget.Container Container}s which renders child widgets to their
  * {@link Common.widget.Widget#property-contentElement}. It also adds the Container's
  * {@link Common.widget.Container#config-itemCls} class to child items.
  *
  * Subclasses may modify the way child widgets are rendered, or may offer APIs for manipulating the child widgets.
  *
  * The {@link Common.widget.layout.Card Card} layout class offers slide-in, slide-out animation of multiple
  * child widgets. {@link Common.widget.TabPanel} uses Card layout.
  */
export default class Layout extends Events(Base) {

    static get defaultConfig() {
        return {
            /**
             * The CSS class which should be added to the owning {@link Common.widget.Container Container}'s
             * {@link Common.widget.Widget#property-contentElement}.
             */
            containerCls : null,

            /**
             * The CSS class which should be added to the encapsulating element of child items.
             */
            itemCls : null
        };
    }

    static getLayout(layout, owner) {
        if (layout instanceof Layout) {
            return layout;
        }

        const
            isString = typeof layout === 'string',
            config   = {
                owner
            };

        return new (isString ? layoutClasses[layout] : layout)(isString ? config : Object.assign(config, layout));
    }

    static registerLayout(cls, name = cls.$name.toLowerCase()) {
        layoutClasses[name] = cls;
    }

    renderChildren() {
        const
            { owner, containerCls, itemCls } = this,
            { contentElement, items }        = owner,
            ownerItemCls                     = owner.itemCls,
            itemCount                        = items && items.length;

        contentElement.classList.add('b-content-element');
        if (containerCls) {
            contentElement.classList.add(containerCls);
        }

        // Need to check that container has widgets, for example TabPanel can have no tabs
        if (itemCount) {
            for (let i = 0; i < itemCount; i++) {
                const
                    item = items[i],
                    { element } = item;

                element.dataset.index = i;
                if (itemCls) {
                    element.classList.add(itemCls);
                }
                if (ownerItemCls) {
                    element.classList.add(ownerItemCls);
                }

                // If instantiated by the app developer, external to Container#createWidget
                // a widget will have the b-outer class. Remove that if it' contained.
                element.classList.remove('b-outer');
                item.render(contentElement);
            }
        }
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
