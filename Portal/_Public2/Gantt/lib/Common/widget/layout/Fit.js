import Layout from './Layout.js';

/**
 * @module Common/widget/layout/Fit
 */

/**
 * A helper class for containers which must manage a single child widget which must fit the container's
 * {@link Common.widget.Widget#property-contentElement contentElement}.
 */
export default class Fit extends Layout {
    static get defaultConfig() {
        return {
            containerCls : 'b-fit-container',

            itemCls : 'b-fit-item'
        };
    }
}

// Layouts must register themselves so that the static layout instantiation
// in Layout knows what to do with layout type names
Layout.registerLayout(Fit);
