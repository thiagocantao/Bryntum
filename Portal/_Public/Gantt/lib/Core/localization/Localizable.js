import Base from '../Base.js';
import LocaleManager from './LocaleManager.js';
import LocaleHelper from './LocaleHelper.js';
import Objects from '../helper/util/Objects.js';

/**
 * @module Core/localization/Localizable
 */

const
    ObjectProto     = Object.getPrototypeOf(Object),
    localeRe        = /L{.*?}/g,
    capturelocaleRe = /L{(.*?)}/g,
    classMatchRe    = /((.*?)\.)?(.+)/g,
    escape          = (txt) => txt.replace(/{(\d+)}/gm, '[[$1]]'),
    unescape        = (txt) => txt.replace(/\[\[(\d+)]]/gm, '{$1}'),
    emptyObject     = Object.freeze(Object.create(null));

/**
 * Mixin that provides localization functionality to a class.
 *
 * ```javascript
 * // Get localized string
 * grid.L('foo');
 * grid.L('L{foo}');
 * ```
 *
 * @mixin
 */
export default Target => class Localizable extends (Target || Base) {
    static get $name() {
        return 'Localizable';
    }

    static get configurable() {
        return {
            /**
             * A class translations of which are used for translating this entity.
             * This is often used when translations of an item are defined on its container class.
             * For example:
             *
             * ```js
             * // Toolbar class that has some predefined items
             * class MyToolbar extends Toolbar {
             *
             *     static get $name() {
             *         return 'MyToolbar';
             *     }
             *
             *     static get defaultConfig() {
             *         return {
             *             // this specifies default configs for the items
             *             defaults : {
             *                 // will tell items to use the toolbar locale
             *                 localeClass : this
             *             },
             *
             *             items : [
             *                 // The toolbar has 2 buttons and translation for their texts will be searched in
             *                 // the toolbar locales
             *                 { text : 'Agree' },
             *                 { text : 'Disagree' }
             *             ]
             *         };
             *     }
             *
             *    ...
             * }
             * ```
             * So if one makes a locale for the `MyToolbar` class that will include `Agree` and `Disagree` string translations:
             * ```js
             *     ...
             *     MyToolbar : {
             *         Agree    : 'Yes, I agree',
             *         Disagree : 'No, I do not agree'
             *     }
             * ```
             * They will be used for the toolbar buttons and the button captions will say `Yes, I agree` and `No, I do not agree`.
             *
             * @config {Core.Base}
             * @typings {typeof Base}
             * @category Misc
             * @advanced
             */
            localeClass : null,

            /**
             * Set to `false` to disable localization of this object.
             * @config {Boolean}
             * @default true
             * @category Misc
             * @advanced
             */
            localizable : null,

            /**
             * List of properties which values should be translated automatically upon a locale applying.
             * In case there is a need to localize not typical value (not a String value or a field with re-defined setter/getter),
             * you could use 'localeKey' meta configuration.
             * Example:
             * ```js
             *  static get configurable() {
             *     return {
             *          localizableProperties : ['width'],
             *
             *          width : {
             *              value   : '54em', // default value here
             *              $config : {
             *                  localeKey : 'L{editorWidth}' // name of the property that will be used in localization file
             *              }
             *          }
             *      };
             *  }
             * ```
             * @config {String[]}
             * @category Misc
             * @advanced
             */
            localizableProperties : {
                value : [],

                $config : {
                    merge : 'distinct'
                }
            }
        };
    }

    static clsName(cls) {

        return typeof cls === 'string' ? cls : cls === ObjectProto ? 'Object' : cls.$$name || cls.name || cls.prototype?.$$name || cls.prototype?.name;
    }

    static parseLocaleString(text) {
        const matches = [];
        let m;

        // Parse locale text in case it's wrapped with L{foo}
        if (text?.includes('L{')) {
            // Escape fix for {1}, {2} etc. in locale str
            text = escape(text);
            capturelocaleRe.lastIndex = 0;

            while ((m = capturelocaleRe.exec(text)) != null) {
                classMatchRe.lastIndex = 0;

                // Support for parsing class namespace L{Class.foo}
                const classMatch = classMatchRe.exec(m[1]);
                matches.push({
                    match       : unescape(m[0]),
                    localeKey   : unescape(classMatch[3]),
                    localeClass : classMatch[2]
                });
            }
        }

        return matches.length > 0
            ? matches
            : [{
                match       : text,
                localeKey   : text,
                localeClass : undefined
            }];
    }

    construct(config = {}, ...args) {
        // Base class applies configs.
        super.construct(config, ...args);

        LocaleManager.ion({ locale : 'updateLocalization', thisObj : this });

        this.updateLocalization();
    }

    get localeClass() {
        return this._localeClass || null;
    }

    localizeProperty(property) {
        const
            me           = this,
            currentValue = Objects.getPath(me, property),
            // Grid.column.Column is Localizable too. It uses fields, not configs
            localeKey    = me.$meta.configs[property]?.localeKey || me.fieldMap?.[property]?.defaultValue;

        let localizedValue;

        // check if localeKey is defined and try to translate it
        if (localeKey) {
            localizedValue = Localizable.localize(localeKey, me, me.localeClass || me);
            // if a user set value directly in class definition, his value has a prio
            if (localizedValue && !(property in (me.initialConfig || emptyObject))) {
                Objects.setPath(me.isColumn ? me.data : me, property, localizedValue);
            }
        }
        else if (typeof currentValue === 'string') {

            me.originalLocales = me.originalLocales || {};

            localizedValue = Objects.getPath(me.originalLocales, property);

            // If we haven't saved original values yet let's do that
            if (localizedValue === undefined) {
                Objects.setPath(me.originalLocales, property, currentValue);
                localizedValue = currentValue;
            }

            // Doing localization from the original values
            if (localizedValue) {
                Objects.setPath(me, property, localizedValue = me.optionalL(localizedValue, me));
            }
        }
        return localizedValue || currentValue;
    }

    /**
     * Method that is triggered when applying a locale to the instance
     * (happens on the instance construction steps and when switching to another locale).
     *
     * The method can be overridden to dynamically translate the instance when locale is switched.
     * When overriding the method please make sure you call `super.updateLocalization()`.
     * @category Misc
     * @advanced
     */
    updateLocalization() {
        if (this.localizable !== false) {
            this.localizableProperties?.forEach(this.localizeProperty, this);
            this.trigger?.('localized');
        }
    }

    static getTranslation(text, templateData, localeCls) {
        const locale = LocaleManager.locale;

        let result = null,
            clsName,
            cls;

        if (locale) {

            // Iterate over all found localization entries
            for (const { match, localeKey, localeClass } of this.parseLocaleString(text)) {

                const translate = (clsName) => {
                    const translation = locale[clsName]?.[localeKey];

                    if (translation) {
                        if (typeof translation === 'function') {
                            result = templateData != null ? translation(templateData) : translation;
                        }
                        else if (typeof translation === 'object' || text === match) {
                            result = translation;
                        }
                        // Likely string
                        else {
                            result = (result || text).replace(match, translation);
                        }

                        // Might have nested L{, recurse
                        if (typeof translation === 'string' && translation.includes('L{')) {
                            result = this.getTranslation(translation, templateData, localeCls);
                        }
                    }

                    return translation;
                };

                // Translate order
                // 1. Try to translate for current class
                // 2. Try to translate by Class hierarchy traversing prototypes
                // 3. Try to translate if Class is in {Class.foo} format
                let success = false;
                for (cls = localeCls; cls && (clsName = Localizable.clsName(cls)); cls = Object.getPrototypeOf(cls)) {
                    if ((success = translate(clsName))) {
                        break;
                    }
                    else if (typeof cls === 'string') {
                        break;
                    }
                }
                if (!success && localeClass) {
                    translate(localeClass);
                }
            }
        }

        return result;
    }

    /**
     * Get localized string, returns `null` if no localized string found.
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is a function
     * @returns {String}
     * @internal
     */
    static localize(text, templateData = undefined, ...localeClasses) {
        // In case this static method is called directly third argument is not provided
        // just fallback to searching locales for the class itself
        if (localeClasses?.length === 0) {
            localeClasses = [this];
        }
        let translation = null;
        localeClasses.some(cls => {
            translation = Localizable.getTranslation(text, templateData, cls);
            return translation != null;
        });
        return translation;
    }

    /**
     * Get localized string, returns value of `text` if no localized string found.
     *
     * If {@link Core.localization.LocaleManager#property-throwOnMissingLocale LocaleManager.throwOnMissingLocale}
     * is `true` then calls to `L()` will throw `Localization is not found for 'text' in 'ClassName'` exception when no
     * localization is found.
     *
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is a function
     * @static
     * @returns {String}
     * @advanced
     */
    static L(text, templateData = undefined, ...localeClasses) {
        // In case this static method is called directly third argument is not provided
        // just fallback to searching locales for the class itself
        if (localeClasses?.length === 0) {
            localeClasses = [this];
        }
        const translation = this.localize(text, templateData, ...localeClasses);

        // Throw error if not localized and text matches `L{foo}`
        if (
            translation == null &&
            LocaleManager.throwOnMissingLocale &&
            text.includes('L{')
        ) {
            throw new Error(`Localization is not found for '${text}' in '${localeClasses.map(cls => Localizable.clsName(cls)).join(', ')}'. ${LocaleManager.locale.localeName ? `Locale : ${LocaleManager.locale.localeName}` : ''}`);
        }

        return translation ?? text;
    }

    /**
     * Convenience function that can be called directly on the class that mixes Localizable in
     *
     * ```javascript
     * button.text = grid.L('L{group}');
     * ```
     *
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is a function
     * @returns {String}
     * @category Misc
     * @advanced
     */
    L(text, templateData) {
        const { localeClass, constructor } = this;
        // If we have a different class set as translations provider
        // pass it first and use the class being translated as a fallback provider
        if (localeClass && Localizable.clsName(localeClass) !== Localizable.clsName(constructor)) {
            return Localizable.L(text, templateData, localeClass, constructor);
        }
        else {
            return Localizable.L(text, templateData, constructor);
        }
    }

    /**
     * Convenience function to get an optional translation. The difference compared to `L()` is that it won't throw
     * an error when the translation is missing even if configured with `throwOnMissingLocale`
     *
     * ```javascript
     * button.text = grid.optionalL('L{group}');
     * ```
     *
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is a function
     * @returns {String}
     * @static
     * @category Misc
     * @advanced
     */
    static optionalL(text, templateData = undefined, ...localeClasses) {
        const shouldThrow = LocaleManager.throwOnMissingLocale;

        LocaleManager.throwOnMissingLocale = shouldThrow && localeRe.test(text);

        // In case this static method is called directly third argument is not provided
        // just fallback to searching locales for the class itself
        if (localeClasses?.length === 0) {
            localeClasses = [this];
        }
        const result = Localizable.L(text, templateData, ...localeClasses);

        LocaleManager.throwOnMissingLocale = shouldThrow;

        return result;
    }

    /**
     * Convenience function to get an optional translation. The difference compared to `L()` is that it won't throw
     * an error when the translation is missing even if configured with `throwOnMissingLocale`
     *
     * ```javascript
     * button.text = grid.optionalL('L{group}');
     * ```
     *
     * @param {String} text String key
     * @param {Object} [templateData] Data to supply to template if localized string is a function
     * @param {Boolean} [preventThrow] Prevent throwing error even if localized text matches `L{foo}`
     * @returns {String}
     * @category Misc
     * @internal
     */
    optionalL(text, templateData = this, preventThrow = false) {
        const shouldThrow = LocaleManager.throwOnMissingLocale;

        // Optional locale text should not include L{}
        LocaleManager.throwOnMissingLocale = shouldThrow && localeRe.test(text) && !preventThrow;

        const result = this.L(text, templateData);

        LocaleManager.throwOnMissingLocale = shouldThrow;

        return result;
    }

    /**
     * Get the global LocaleManager
     * @property {Core.localization.LocaleManager}
     * @typings {typeof LocaleManager}
     * @category Misc
     * @readonly
     * @advanced
     */
    get localeManager() {
        return LocaleManager;
    }

    /**
     * Get the global LocaleHelper
     * @property {Core.localization.LocaleHelper}
     * @typings {typeof LocaleHelper}
     * @category Misc
     * @readonly
     * @advanced
     */
    get localeHelper() {
        return LocaleHelper;
    }

};
