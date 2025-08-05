import Base from '../../Base.js';

/**
 * @module Core/widget/mixin/Badge
 */

/**
 * Mixin that allows a widget to display a badge (mostly done as css)
 *
 * @example
 * // show badge
 * button.badge = 5;
 *
 * // hide badge
 * button.badge = null;
 *
 * @externalexample widget/Badge.js
 *
 * @mixin
 */
export default Target => class Badge extends (Target || Base) {
    static get $name() {
        return 'Badge';
    }

    static get configurable() {
        return {
            /**
             * Get/sets and display badge, set to null or empty string to hide.
             * @property {String}
             * @name badge
             */
            /**
             * Initial text to show in badge.
             * @config {String} badge
             */
            badge : null
        };
    }

    updateBadge(badge) {
        const { element } = this;

        if (element) {
            if (badge != null && badge !== '') {
                element.dataset.badge = badge;
                element.classList.add('b-badge');
            }
            else {
                if (element.dataset.badge) {
                    delete element.dataset.badge;
                }

                element.classList.remove('b-badge');
            }
        }
    }

    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {
        // If we don't have a badge, our classList doesn't include "b-badge"
        if (this.badge) {
            return 'b-badge';
        }
    }
};
