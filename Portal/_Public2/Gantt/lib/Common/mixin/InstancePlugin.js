import Base from '../Base.js';
import Events from '../mixin/Events.js';
import Localizable from '../localization/Localizable.js';

/**
 * @module Common/mixin/InstancePlugin
 */

/**
 * Base class for plugins. Published functions will be available from the other class. `this` in published functions is
 * referenced to the plugin, access the other class using `this.pluggedInto`.
 *
 * Observe that plugin doesn't apply itself on class level but instead on instance level. Plugin is its own instance
 * that can have own functions and data that is not exposed to target class.
 *
 * Functions can be published in four ways:
 *
 * * `assign` (when function is not already available on target)
 * * `before` (when function is already available on target, will be called before original function)
 * * `after` (when function is already available on target, will be called after original function)
 * * `override` (replaces function on target, but old function can be reached)
 *
 * To configure which functions get published and in what way, specify `pluginConfig` getter on plugin:
 *
 * ```
 * class Sort extends InstancePlugin {
 *   static get pluginConfig {
 *      return {
 *          before   : ['init'],
 *          after    : ['destroy', 'onElementClick'],
 *          override : ['render']
 *      };
 *   }
 * }
 * ```
 *
 * @mixes Common/localization/Localizable
 * @mixes Common/mixin/Events
 */
export default class InstancePlugin extends Localizable(Events(Base)) {

    //region Config

    static get defaultConfig() {
        return {
            /**
             * The plugin disabled state
             * @config {Boolean}
             * @default
             */
            disabled : false
        };
    }

    //endregion

    //region Init

    /**
     * Call from another instance to add plugins to it.
     * @example
     * InstancePlugin.initPlugins(this, Search, Stripe);
     * @param plugInto Instance to mix into (usually this)
     * @param plugins Classes to plug in
     * @internal
     */
    static initPlugins(plugInto, ...plugins) {
        let property = plugInto.plugins || (plugInto.plugins = {});

        for (let PluginClass of plugins) {
            property[PluginClass.$name] = new PluginClass(plugInto);
        }
    }

    /**
     * Initializes the plugin.
     * @internal
     * @param plugInto Target instance to plug into
     * @function constructor
     */
    construct(plugInto, config) {
        /**
         * The Widget which was passed into the constructor,
         * which is the Widget we are providing extra services for.
         * @property {Common.widget.Widget}
         * @readonly
         */
        this.client = plugInto;

        super.construct(config);

        this.applyPluginConfig(plugInto);
    }

    /**
     * Applies config as found in plugInto.pluginConfig, or published all if no config found.
     * @private
     * @param plugInto Target instance to plug into
     */
    applyPluginConfig(plugInto) {
        let me          = this,
            config      = me.pluginConfig || me.constructor.pluginConfig,
            assign      = config && config.assign,
            chain       = config && (config.chain || config.after),
            before      = config && config.before,
            override    = config && config.override;

        me.pluggedInto = plugInto;

        // fill unpublished[] with all fnNames not in chain or override

        // apply chains and overrides.
        if (assign) me.applyAssign(plugInto, assign);
        if (chain) me.applyChain(plugInto, chain);
        if (before) me.applyChain(plugInto, before, false);
        if (override) me.applyOverride(plugInto, override);
    }

    /**
     * Applies assigning for specified functions.
     * @private
     * @param plugInto
     * @param fnNames
     */
    applyAssign(plugInto, fnNames) {
        fnNames.forEach(fnName => this.assign(plugInto, fnName));
    }

    /**
     * Applys chaining for specified functions.
     * @private
     * @param plugInto
     * @param fnNames
     * @param after
     */
    applyChain(plugInto, fnNames, after = true) {
        const me = this;
        fnNames.forEach(fnName => {
            if (plugInto[fnName]) {
                me.chain(plugInto, fnName, after);
            }
            else {
                me.assign(plugInto, fnName);
            }
        });
    }

    /**
     * Applies override for specified functions.
     * @private
     * @param plugInto
     * @param fnNames
     */
    applyOverride(plugInto, fnNames) {
        const me = this;

        if (!me.overridden) me.overridden = {};

        fnNames.forEach(fnName => {
            if (!me[fnName]) {
                throw new Error(this.L('overrideFnMissing', {
                    plugIntoName : plugInto.$name,
                    pluginName   : me.$name,
                    fnName       : fnName
                }));
            }
            // override
            if (typeof plugInto[fnName] === 'function') me.overridden[fnName] = plugInto[fnName].bind(plugInto);

            plugInto[fnName] = me[fnName].bind(me);
        });
    }

    /**
     * Assigns specified functions.
     * @private
     * @param plugInto
     * @param fnName
     */
    assign(plugInto, fnName) {
        let me       = this,
            property = Object.getOwnPropertyDescriptor(Object.getPrototypeOf(me), fnName);

        if (property && (property.get || property.set)) {
            // getter/setter, define corresponding property on target
            Object.defineProperty(plugInto, fnName, {
                configurable : true,
                enumerable   : true,
                get          : property.get && property.get.bind(me),
                set          : property.set && property.set.bind(me)
            });
        }
        else {
            plugInto[fnName] = me[fnName].bind(me);
        }
    }

    //endregion

    //region Chaining

    /**
     * Chains functions. When the function is called on the target class all functions in the chain will be called in
     * the order they where added.
     * @private
     * @param plugInto
     * @param key
     */
    chain(plugInto, key, after = true) {
        let me    = this,
            chain = plugInto.pluginFunctionChain || (plugInto.pluginFunctionChain = {});

        // duplicate function, make chain and use function to run all functions in it upon call...
        if (!chain[key]) {
            chain[key] = [plugInto[key].bind(plugInto)];
        }

        if (!me[key]) {
            throw new Error(
                this.L('fnMissing', {
                    plugIntoName : plugInto.$name,
                    pluginName   : me.$name,
                    fnName       : key
                })
            );
            //throw new Error(`Trying to plug function ${plugInto.$name}#${key}, but InstancePlugin ${me.$name}#${key} does not exist`);
        }

        if (after) {
            chain[key].push(me[key].bind(me));
        }
        else {
            chain[key].unshift(me[key].bind(me));
        }

        // use function to run all functions in chain on call
        plugInto[key] = (...params) => me.functionChainRunner(key, ...params);
    }

    /**
     * Used to run multiple plugged in functions with the same name, see chain above. Returning false from a
     * function will abort chain.
     * @private
     * @param fnName
     * @param params
     * @returns value returned from last function in chain (or false if any returns false)
     */
    functionChainRunner(fnName, ...params) {
        const chain = this.pluggedInto.pluginFunctionChain[fnName];
        let returnValue;

        // changed from for..of to try and fix Edge problems
        for (let i = 0; i < chain.length; i++) {
            returnValue = chain[i](...params);
            if (returnValue === false) return false;
        }

        return returnValue;
    }

    //endregion

    /**
     * Get/set the plugin disabled state
     * @property {Boolean}
     */
    get disabled() {
        return this._disabled;
    }

    set disabled(disabled) {
        this._disabled = disabled;
    }
}
