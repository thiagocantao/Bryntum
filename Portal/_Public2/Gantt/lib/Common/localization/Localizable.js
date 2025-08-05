import Base from '../Base.js';
import LocaleManager from './LocaleManager.js';

/**
 * @module Common/localization/Localizable
 */

/**
 * Mixin that simplifies localization of strings in a class.
 *
 * ```
 * // Get localized string
 * grid.L('sort')
 * ```
 *
 * @mixin
 */
export default Target => class Localizable extends (Target || Base) {
    static get defaultConfig() {
        return {
            localeClass           : null,
            localizableProperties : []
        };
    }

    static get inTextLocaleRegExp() {
        return /L\{([^}]+)\}/g;
    }

    // In case it's wrapped in 'L{text}'
    static parseText(text) {
        const match = this.inTextLocaleRegExp.exec(text);
        return match ? match[1] : text;
    }

    construct(config = {}, ...args) {
        const me = this;

        // Base class applies configs.
        super.construct(config, ...args);

        LocaleManager.on('locale', me.updateLocalization, me);

        me.updateLocalization();
    }

    get localeClass() {
        // Trying to extract localeClass from a parent widget. null by default
        return this._localeClass || (this.parent && this.parent.localeClass) || null;
    }

    set localeClass(key) {
        this._localeClass = key;
    }

    updateLocalization() {
        const me = this;

        me.localizableProperties && me.localizableProperties.forEach(name => {
            // No need to translate properties which are not defined
            if (me[name] === undefined) return;

            me.originalLocales = me.originalLocales || {};

            // Need to save original values since they will be overridden by localizable equivalents
            me.originalLocales[name] = me.originalLocales[name] || me[name];

            // Doing localization from the original values
            if (me.originalLocales[name]) {
                me[name] = Localizable.L.call(me, me.originalLocales[name]);
            }
        });
    }

    /**
     * Get localized string, returns value of `text` if no localized string found
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is one
     * @returns {String}
     */
    static L(text, templateData, current = this.localeClass || this) {
        if (LocaleManager.locale) {
            // traverse prototypes to find localization

            while (current) {
                const name      = current.$name || current.name,
                    localeClass = LocaleManager.locale[name],
                    localeText  = localeClass && localeClass[Localizable.parseText(text)];

                if (localeText) {
                    return typeof localeText === 'function' && templateData != null ? localeText(templateData) : localeText;
                }

                current = Object.getPrototypeOf(current);
            }
        }
        return text;
    }

    /**
     * Convenience function that can be called directly on the class that mixes Localizable in
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is one
     * @returns {String}
     * @category Misc
     * @example
     * button.text = grid.L('group');
     */
    L(text, templateData) {
        return Localizable.L(text, templateData, this.constructor);
    }

    /**
     * Get the global LocaleManager
     * @returns {Common.localization.LocaleManager}
     * @category Misc
     */
    get localeManager() {
        return LocaleManager;
    }
};
