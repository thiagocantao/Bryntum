import Base from '../Base.js';
import InstancePlugin from './InstancePlugin.js';

/**
 * @module Core/mixin/Pluggable
 */

/**
 * Enables using plugins for a class by specifying property plugins as an array of plugin classes. If only a single plugin
 * is used, just give the plugin class instead of an array. This class isn't required for using plugins, just makes it
 * easier. Without mixin you can otherwise use `InstancePlugin.initPlugins(this, PluginClass)`.
 *
 * @example
 * new Store({
 *   plugins: [PluginClass, ...]
 * });
 *
 * @mixin
 */
export default Target => class Pluggable extends (Target || Base) {
    static get $name() {
        return 'Pluggable';
    }

    /**
     * Specify plugins (an array of classes) in config
     * @config {Function[]} plugins
     * @category Misc
     * @advanced
     */

    /**
     * Map of applied plugins
     * @property {Object<String,Core.mixin.InstancePlugin>}
     * @readonly
     * @category Misc
     * @advanced
     */
    get plugins() {
        if (!this._plugins) {
            this._plugins = {};
        }
        return this._plugins;
    }

    set plugins(plugins) {
        if (plugins) {
            if (!Array.isArray(plugins)) plugins = [plugins];
            InstancePlugin.initPlugins(this, ...plugins);
        }
        this.initPlugins();
    }

    /**
     * Template method which may be implemented in subclasses to initialize any plugins.
     * This method is empty in the `Pluggable` base class.
     * @internal
     */
    initPlugins() {

    }

    /**
     * Adds plugins to an instance.
     * @param {Function[]} plugins The plugins to add
     * @category Misc
     * @advanced
     */
    addPlugins(...plugins) {
        InstancePlugin.initPlugins(this, ...plugins);
    }

    /**
     * Checks if instance has plugin.
     * @param {String|Function} pluginClassOrName Plugin or name to check for
     * @returns {Boolean}
     * @category Misc
     * @advanced
     */
    hasPlugin(pluginClassOrName) {
        return this.getPlugin(pluginClassOrName) != null;
    }

    /**
     * Get a plugin instance.
     * @param {String|Function} pluginClassOrName
     * @returns {Core.mixin.InstancePlugin}
     * @category Misc
     * @advanced
     */
    getPlugin(pluginClassOrName) {
        if (typeof pluginClassOrName === 'function') {
            pluginClassOrName = pluginClassOrName.$$name;
        }
        return this.plugins?.[pluginClassOrName];
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
