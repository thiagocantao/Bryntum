import Base from '../../../Core/Base.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import GridFeatureManager from '../../feature/GridFeatureManager.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';

/**
 * @module Grid/view/mixin/GridFeatures
 */

const validConfigTypes = {
    string   : 1,
    object   : 1,
    function : 1 // used by CellTooltip
};

/**
 * Mixin for Grid that handles features. Features are plugins that add functionality to the grid. Feature classes should
 * register with Grid by calling {@link Grid.feature.GridFeatureManager#function-registerFeature-static registerFeature}. This
 * enables features to be specified and configured in grid
 * config.
 *
 * Define which features to use:
 *
 * ```javascript
 * // specify which features to use (note that some features are used by default)
 * const grid = new Grid({
 *   features: {
 *      sort: 'name',
 *      search: true
 *   }
 * });
 * ```
 *
 * Access a feature in use:
 *
 * ```javascript
 * grid.features.search.search('cat');
 * ```
 *
 * Basic example of implementing a feature:
 *
 * ```javascript
 * class MyFeature extends InstancePlugin {
 *
 * }
 *
 * GridFeatures.registerFeature(MyFeature);
 *
 * // using the feature
 * const grid = new Grid({
 *   features: {
 *     myFeature: true
 *   }
 * });
 * ```
 *
 * ## Enable and disable features at runtime
 *
 * Each feature is either "enabled" (included by default), or "off" (excluded completely). You can always check the docs
 * of a specific feature to find out how it is configured by default.
 *
 * Features which are "off" completely are not available and cannot be enabled at runtime.
 *
 * For a feature that is **off** by default that you want to enable later during runtime,
 * configure it with `disabled : true`:
 * ```javascript
 * const grid = new Grid({
 *      featureName : {
 *          disabled : true // on and disabled, can be enabled later
 *      }
 * });
 *
 * // enable the feature
 * grid.featureName.disabled = false;
 * ```
 *
 * If the feature is **off** by default, and you want to include and enable the feature, configure it as `true`:
 * ```javascript
 * const grid = new Grid({
 *      featureName : true // on and enabled, can be disabled later
 * });
 *
 * // disable the feature
 * grid.featureName.disabled = true;
 * ```
 *
 * If the feature is **on** by default, but you want to turn it **off**, configure it as `false`:
 * ```javascript
 * const grid = new Grid({
 *      featureName : false // turned off, not included at all
 * });
 * ```
 *
 * If the feature is **enabled** by default and you have no need of reconfiguring it,
 * you can omit the feature configuration.
 *
 * @mixin
 */
export default Target => class GridFeatures extends (Target || Base) {
    static get $name() {
        return 'GridFeatures';
    }

    //region Init

    /**
     * Specify which features to use on the grid. Most features accepts a boolean, some also accepts a config object.
     * Please note that if you are not using the bundles you might need to import the features you want to use.
     *
     * ```javascript
     * const grid = new Grid({
     *     features : {
     *         stripe : true,   // Enable stripe feature
     *         sort   : 'name', // Configure sort feature
     *         group  : false   // Disable group feature
     *     }
     * }
     * ```
     *
     * @config {Object} features
     * @category Common
     */

    /**
     * Map of the features available on the grid. Use it to access them on your grid object
     *
     * ```javascript
     * grid.features.group.expandAll();
     * ```
     *
     * @readonly
     * @member {Object} features
     * @category Common
     */

    set features(features) {
        const
            me              = this,
            defaultFeatures = GridFeatureManager.getInstanceDefaultFeatures(this);

        features = me._features = ObjectHelper.assign({}, features);

        // default features, enabled unless otherwise specified
        if (defaultFeatures) {
            Object.keys(defaultFeatures).forEach(feature => {
                if (!(feature in features)) {
                    features[feature] = true;
                }
            });
        }

        // We *prime* the features so that if any configuration code accesses a feature, it
        // will self initialize, but if not, they will remain in a primed state until afterConfigure.
        const registeredInstanceFeatures = GridFeatureManager.getInstanceFeatures(this);

        for (const featureName of Object.keys(features)) {
            const config = features[featureName];

            // Create feature initialization property if config is truthy.
            // Config must be a valid configuration value for the feature class.
            if (config) {
                const throwIfError = !globalThis.__bryntum_code_editor_changed;

                // Feature configs name must start with lowercase letter to be valid
                if (StringHelper.uncapitalize(featureName) !== featureName) {
                    const errorMessage = `Invalid feature name '${featureName}', must start with a lowercase letter`;

                    if (throwIfError) {
                        throw new Error(errorMessage);
                    }
                    console.error(errorMessage);
                    me._errorDuringConfiguration = errorMessage;
                }

                const featureClass = registeredInstanceFeatures[featureName];

                if (!featureClass) {
                    const errorMessage = `Feature '${featureName}' not available, make sure you have imported it`;
                    if (throwIfError) {
                        throw new Error(errorMessage);
                    }
                    console.error(errorMessage);
                    me._errorDuringConfiguration = errorMessage;
                    return;
                }

                // Create a self initializing property on the features object named by the feature name.
                // when accessed, it will create and return the real feature.
                // Now, if some Feature initialization code attempt to access a feature which has not yet been initialized
                // it will be initialized just in time.
                Reflect.defineProperty(features, featureName, me.createFeatureInitializer(features, featureName,
                    featureClass, config));
            }
        }
    }

    get features() {
        return this._features;
    }

    createFeatureInitializer(features, featureName, featureClass, config) {
        const
            constructorArgs = [this],
            construct       = featureClass.prototype.construct;

        // Config arg must be processed if feature is just requested with true
        // so that default configurable values are processed.
        if (config === true) {
            config = {};
        }

        // Only pass config if there is one.
        // The constructor(config = {}) only works for undefined config
        if (validConfigTypes[typeof config]) {
            constructorArgs[1] = config;
        }

        return {
            configurable : true,
            get() {
                // Delete this defined property and replace it with the Feature instance.
                delete features[featureName];

                // Ensure the feature is injected into the features object before initialization
                // so that it is available from call chains from its initialization.
                featureClass.prototype.construct = function(...args) {
                    features[featureName] = this;
                    construct.apply(this, args);
                    featureClass.prototype.construct = construct;
                };

                // Return the Feature instance
                return new featureClass(...constructorArgs);
            }
        };
    }

    //endregion

    //region Other stuff

    /**
     * Check if a feature is included
     * @param {String} name Feature name, as registered with `GridFeatureManager.registerFeature()`
     * @returns {Boolean}
     * @category Misc
     */
    hasFeature(name) {
        const { features } = this;

        if (features) {
            const featureProp = Object.getOwnPropertyDescriptor(this.features, name);

            if (featureProp) {
                // Do not actually force creation of the feature
                return Boolean(featureProp.value || featureProp.get);
            }
        }
        return false;
    }

    hasActiveFeature(name) {
        return Boolean(this.features?.[name] && !this.features?.[name].disabled);
    }

    //endregion

    //region Extract config

    // This function is not meant to be called by any code other than Base#getCurrentConfig().
    // It extracts the current configs for the features
    getConfigValue(name, options) {
        if (name === 'features') {
            const result = {};

            for (const feature in this.features) {
                // Feature might be configured as `false`
                const featureConfig = this.features[feature]?.getCurrentConfig?.(options);
                if (featureConfig) {
                    // Use `true` for empty feature configs `{ stripe : true }`
                    if (ObjectHelper.isEmpty(featureConfig)) {
                        // Exclude default features to not spam the config
                        if (!GridFeatureManager.isDefaultFeatureForInstance(this.features[feature].constructor, this)) {
                            result[feature] = true;
                        }
                    }
                    else {
                        result[feature] = featureConfig;
                    }
                }
                else {
                    result[feature] = false;
                }
            }

            return result;
        }

        return super.getConfigValue(name, options);
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
