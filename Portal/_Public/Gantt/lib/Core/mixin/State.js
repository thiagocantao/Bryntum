import Base from '../Base.js';
import DateHelper from '../helper/DateHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import StringHelper from '../helper/StringHelper.js';
import StateProvider from '../state/StateProvider.js';

//import Config from '../Config.js';

/**
 * @module Core/mixin/State
 */

const primitiveRe = /boolean|number|string/;

/**
 * A mixin that handles accessing, saving, and restoring an object's persistent state.
 *
 * ## Using Stateful Components
 *
 * Instances of classes that use this mixin (i.e., "stateful components") have a {@link #property-state} property that
 * provides read/write access to their persistable state in the form of a simple object. These state objects can be
 * saved and restored under application control, e.g., using `localStorage`.
 *
 * This approach can be streamlined using a {@link Core.state.StateProvider} either by setting the
 * {@link Core.state.StateProvider#property-instance-static default state provider} or by using an instance-level
 * {@link #config-stateProvider} config.
 *
 * When using a state provider, stateful components with a {@link #config-stateId} or an
 * {@link Core.widget.Widget#config-id} will automatically save (see {@link #function-saveState}) and restore
 * (see {@link #function-loadState}) their `state`. This use of the `id` as a `stateId` can be disabled by assigning
 * the {@link #config-stateful} config to `false`. When using a `stateId` and a state provider, it is not necessary to
 * call the {@link #function-loadState} and {@link #function-saveState} methods directly.
 *
 * ### Simple vs Complex State
 *
 * Some stateful components (e.g., {@link Core.widget.Panel panels}) have state that can be described purely by their
 * config properties. For these components, the {@link #config-stateful} config can be used to control which config
 * properties to include in their persistent state. For example:
 *
 * ```javascript
 *  const mainPanel = new Panel({
 *      collapsible : true,
 *      stateId     : 'mainPanel',
 *      stateful    : ['collapsed']
 *  });
 * ```
 *
 * Other components have a complex state (e.g., `GridState`), and do not use the `stateful` config in this way. In all
 * other ways, however, these components behave the same as their simple state counterparts.
 *
 * ## Implementing Stateful Components
 *
 * Implementors of stateful components have two main design points to consider:
 *
 *  - Getting and setting their persistent {@link #property-state} object.
 *  - Initiating calls to {@link #function-saveState} when the object's persistent state changes.
 *
 * ### Persistent State
 *
 * For simple cases, the {@link #config-stateful} config can be set to the list of config property names that should be
 * saved:
 *
 * ```javascript
 *  class MyStatefulComponent extends Base.mixin(State) {
 *      static get configurable() {
 *          return {
 *              stateful : ['text', 'size']
 *          };
 *      }
 *  }
 * ```
 *
 * While the `stateful` config supports an object form (where keys with truthy values are the config names), this form
 * is typically reserved for configuring instances.
 *
 * Classes can choose to implement the {@link #function-getState} and {@link #function-applyState} methods to enhance
 * the `state` object with data not easily mapped to config properties. These method can call their `super` methods or
 * fully replace them.
 *
 * ```javascript
 *  class MyStatefulComponent extends Base.mixin(State) {
 *      getState() {
 *          return {
 *              text : this.text,
 *              size : this.size
 *          };
 *      }
 *
 *      applyState(state) {
 *          this.text = state.text;
 *          this.size = state.size;
 *      }
 *  }
 * ```
 *
 * ### Saving Dates
 *
 * A stateful property may be a `Date` property if the `changeDate` method of the class accepts an
 * ISO 8601 formatted date. Dates are saved in state using ISO 8601 format: `YYYY-MM-DDTHH:mm:ssZ`
 *
 * ### Saving State
 *
 * When the persistent state of a stateful component changes, it must call {@link #function-saveState}. This method
 * schedules an update of the component's persistence {@link #property-state} with the appropriate
 * {@link #config-stateProvider}. When a config property named in the {@link #config-stateful} config changes, this
 * call will be made automatically. This means that even if a component replaces {@link #function-getState} and
 * {@link #function-applyState}, it can still be helpful to specify a value for the `stateful` config.
 *
 * ```javascript
 *  class MyStatefulComponent extends Base.mixin(State) {
 *      static get configurable() {
 *          return {
 *              stateful : ['text', 'size']
 *          };
 *      }
 *
 *      getState() { ... }
 *      applyState(state) { ... }
 *  }
 * ```
 *
 * Another way to ensure {@link #function-saveState} is called when necessary is to use {@link #config-statefulEvents}.
 *
 * ```javascript
 *  class MyStatefulComponent extends Base.mixin(State) {
 *      static get configurable() {
 *          return {
 *              statefulEvents : ['change', 'resize']
 *          };
 *      }
 *  }
 * ```
 *
 * @mixin
 */
export default Target => class State extends (Target || Base) {
    static $name = 'State';

    static configurable = {
        /**
         * This value can be one of the following:
         *
         * - `false` to not use an explicitly assigned {@link Core.widget.Widget#config-id} as the component's
         * {@link #config-stateId} (this is only necessary when there is a {@link #config-stateProvider}).
         * - An array of strings naming the config properties to save in the component's {@link #property-state}
         * object.
         * - An object whose truthy keys are the config properties to save in the component's {@link #property-state}
         * object.
         *
         * These last two uses of the `stateful` config property do not apply to components that have a complex
         * state, as described in the {@link Core.mixin.State State mixin documentation}.
         *
         * This config property is typically set by derived classes to a value including any config property that
         * the user can affect via the user interface. For example, the {@link Core.widget.Panel#config-collapsed}
         * config property is listed for a {@link Core.widget.Panel} since the user can toggle this config property
         * using the {@link Core.widget.panel.PanelCollapser#config-tool collapse tool}.
         *
         * @config {Boolean|Object|String[]}
         * @category State
         */
        stateful : {
            value   : null,
            $config : {
                merge : 'classList'
            }
        },

        /**
         * The events that, when fired by this component, should trigger it to save its state by calling
         * {@link #function-saveState}.
         *
         * ```javascript
         *  class MyStatefulComponent extends Base.mixin(State) {
         *      static get configurable() {
         *          return {
         *              statefulEvents : [ 'change', 'resize' ]
         *          };
         *      }
         *  }
         * ```
         * In the above example, {@link #function-saveState} will be called any time an instance of this class
         * fires the `change` or `resize` event.
         *
         * This config is typically set by derived classes as a way to ensure {@link #function-saveState} is called
         * whenever their persistent state changes.
         *
         * @config {Object|String[]}
         * @category State
         * @default
         */
        statefulEvents : {
            $config : {
                merge : 'classList'
            },

            value : ['stateChange']
        },

        /**
         * The key to use when saving this object's state in the {@link #config-stateProvider}. If this config is
         * not assigned, and {@link #config-stateful} is not set to `false`, the {@link Core.widget.Widget#config-id}
         * (if explicitly specified) will be used as the `stateId`.
         *
         * If neither of these is given, the {@link #function-loadState} and {@link #function-saveState} methods
         * will need to be called directly to make use of the `stateProvider`.
         *
         * For single page applications (SPA's), or multi-page applications (MPA's) that have common, stateful
         * components on multiple pages, the `stateId` should be unique across all stateful components (similar to DOM
         * element id's). MPA's that want each page to be isolated can more easily achieve that isolation using the
         * {@link Core.state.StateProvider#config-prefix}.
         *
         * @config {String}
         * @category State
         */
        stateId : null,

        /**
         * The `StateProvider` to use to save and restore this object's {@link #property-state}. By default, `state`
         * will be saved using the {@link Core.state.StateProvider#property-instance-static default state provider}.
         *
         * This config is useful for multi-page applications that have a set of common components that want to share
         * state across pages, as well as other components that want their state to be isolated. One of these groups
         * of stateful components could be assigned an explicit `stateProvider` while the other group could use the
         * default state provider.
         *
         * @config {Core.state.StateProvider}
         * @category State
         */
        stateProvider : null
    };

    static prototypeProperties = {
        statefulLoaded : false,

        statefulSuspended : 0
    };

    afterConstruct() {
        super.afterConstruct();

        this.loadState();
    }

    finalizeInit() {
        // For widgets, this should happen before rendering which happens in Widget.finalizeInit():
        this.loadState();

        super.finalizeInit();
    }

    /**
     * Returns `true` if this instance implements the {@link Core.mixin.State} interface.
     * @property {Boolean}
     * @readonly
     * @advanced
     */
    get isStateful() {

        return true;
    }

    /**
     * Returns `true` if this instance is ready to participate in state activities.
     * @property {Boolean}
     * @readonly
     * @internal
     */
    get isStatefulActive() {
        // If a widget is rendered via appendTo (for example), this happens inside construct(), before we are called
        // in afterConstruct(). When the Widget uses Responsive mixin, that will trigger its initial responsive update.
        // In short, when isResponsivePending, the Widget is Responsive _and_ has not yet determined its responsiveState.
        // In this case we do NOT want to activate statefulness.
        // Further, if we are updating configs from a responsiveUpdate, we do not want to save to state.
        return !this.statefulSuspended && !this.isResponsivePending && !this.isResponsiveUpdating;
    }

    // state

    /**
     * Gets or sets a component's state
     * @property {Object}
     * @category State
     */
    get state() {
        return this._state = this.getState();
    }

    set state(state) {
        this._state = state;

        if (state) {
            this.applyState(state);
        }
    }

    // statefulEvents

    updateStatefulEvents(events) {
        const
            me        = this,
            listeners = {
                name    : 'statefulEvents',
                thisObj : me
            };

        me.detachListeners(listeners.name);

        if (events) {
            if (typeof events === 'string') {
                events = StringHelper.split(events);
            }
            else if (!Array.isArray(events)) {
                events = ObjectHelper.getTruthyKeys(events);
            }

            if (events.length) {
                for (const event of events) {
                    listeners[event] = 'onStatefulEvent';
                }

                me.ion?.(listeners);
            }
        }
    }

    // statefulId

    /**
     * Returns the state key to use for this instance. This will be either the {@link #config-stateId} or the
     * {@link Core.widget.Widget#config-id} (if explicitly specified and {@link #config-stateful} is not `false`).
     * @property {String}
     * @category State
     * @internal
     */
    get statefulId() {
        const
            me = this,
            { responsiveState } = me;

        let statefulId = me.stateId;

        if (statefulId == null && me.hasGeneratedId === false && me.stateful !== false) {
            statefulId = me.id;
        }

        if (statefulId && responsiveState) {
            statefulId = `${statefulId}[${responsiveState}]`;  // ex = 'foo[small]'
        }

        return statefulId;
    }

    // statefulness

    /**
     * Returns an object whose truthy keys are the config properties to include in this object's {@link #property-state}.
     * @property {Object}
     * @category State
     * @readonly
     * @private
     */
    get statefulness() {
        const { stateful } = this;

        return Array.isArray(stateful) ? ObjectHelper.createTruthyKeys(stateful) : stateful;
    }

    // stateProvider

    get stateProvider() {
        return this._stateProvider || StateProvider.instance;
    }

    //---------------------------------------------------------------------------------------------------------------
    // Methods

    /**
     * Applies the given `state` to this instance.
     *
     * This method is not called directly, but is called when the {@link #property-state} property is assigned a value.
     *
     * This method is implemented by derived classes that have complex state which exceeds the simple list of config
     * properties provided by {@link #config-stateful}. In these cases, the `super` method can be called to handle any
     * config properties that are part of the complex state. The default implementation of this method will only assign
     * those config properties listed in {@link #config-stateful} from the provided `state` object.
     *
     * @param {Object} state The state object to apply to this instance.
     * @category State
     * @advanced
     */
    applyState(state) {
        state = this.pruneState(state);

        if (state) {
            this.setConfig(state);
        }
    }

    /**
     * Returns this object's state information.
     *
     * This method is not called directly, but is called to return the value of the {@link #property-state} property.
     *
     * This method is implemented by derived classes that have complex state which exceeds the simple list of config
     * properties provided by {@link #config-stateful}. In these cases, the `super` method can be called to gather the
     * config properties that are part of the complex state. The default implementation of this method will only copy
     * those config properties listed in {@link #config-stateful} to the returned `state` object.
     *
     * @returns {Object}
     * @category State
     * @advanced
     */
    getState() {
        const
            me          = this,
            {
                initialConfig,
                statefulness,
                isConstructing : defaultState
            }           = me,
            { configs } = me.$meta,
            // If we are reading state at construction time, we are collecting the defaultState, so
            // we should read from the initial config and the defaults.
            source      = defaultState ? Object.setPrototypeOf(initialConfig, me.$meta.config) : me;

        let state = null,
            key, value;

        if (statefulness) {
            state = {};

            for (key in statefulness) {
                if (statefulness[key]) {
                    value = source[key];

                    if (value?.isStateful) {
                        value = value.state;  // e.g.: stateful : { store : true }
                    }
                    else if (!defaultState) {
                        // Dates can be saved in state as ISO 8601 Date and time.
                        // This class's changer must be able to ingest this format.
                        if (ObjectHelper.isDate(value)) {
                            value = DateHelper.format(value, 'YYYY-MM-DDTHH:mm:ssZ');
                        }
                        // If we are reading state to save, ignore configs that have their initial value or aren't primitives
                        if (configs[key].equal(value, initialConfig?.[key]) || !primitiveRe.test(typeof value)) {
                            continue;
                        }
                    }

                    state[key] = value;
                }
            }
        }

        return state;
    }

    /**
     * Loads this object's state from its {@link #config-stateProvider} and applies it to its {@link #property-state}.
     *
     * This method only acts upon its first invocation for a given instance (unless `true` is passed for the `reload`
     * parameter). This allows for flexibility in the timing of that call during the early stages of the instances'
     * lifecycle. To reload the state after this time, manually assign the desired value to the {@link #property-state}
     * property or call this method and pass `reload` as `true`.
     *
     * This method is called automatically during construction when a {@link #config-stateId} or (in some cases) an
     * explicit {@link Core.widget.Widget#config-id} is provided.
     *
     * @param {String} [stateId] An overriding key to use instead of this object's {@link #config-stateId}.
     * @param {Boolean} [reload=false] Pass `true` to load the state even if previously loaded.
     * @category State
     */
    loadState(stateId, reload) {
        if (typeof stateId === 'boolean') {
            reload = stateId;
            stateId = null;
        }

        const
            me = this,
            { statefulLoaded } = me;

        if (me.isStatefulActive && (reload || !statefulLoaded)) {
            const state = me.loadStatefulData(stateId || (stateId = me.statefulId));

            if (!statefulLoaded && stateId) {
                // Whether we have state data or not, we attempted to load it, so track the defaults and load attempt.
                // The state as gathered when statefulLoaded not set is gathered from the configuration, *not*
                // the running state.
                me.defaultState = me.state;
                me.statefulLoaded = true;
            }

            if (state) {
                me.state = state;
            }
        }
    }

    loadStatefulData(stateId) {
        stateId = this.isStatefulActive ? stateId || this.statefulId : null;

        return stateId && this.stateProvider?.getValue(stateId);
    }

    resetDefaultState() {
        if (this.defaultState) {
            this.state = this.defaultState;
        }
    }

    resumeStateful(full = false) {
        this.statefulSuspended = full ? 0 : Math.max(this.statefulSuspended - 1, 0);
    }

    /**
     * Saves this object's state to its {@link #config-stateProvider}.
     *
     * When a {@link #config-stateId} or (in some cases) an explicit {@link Core.widget.Widget#config-id} is provided,
     * this method will be called automatically any time a config property listed in {@link #config-stateful} changes or
     * when a {@link #config-statefulEvents stateful event} is fired.
     *
     * Derived classes are responsible for calling this method whenever the persistent {@link #property-state} of the
     * object changes.
     *
     * @param {Object|String} [options] Options that affect the state saving process or, if a string, the state `id`.
     * @param {String} [options.id] The state id for the saved state (overrides {@link #config-stateId}).
     * @param {Boolean} [options.immediate] Pass `true` to save the data synchronously instead of on a delay.
     * @category State
     */
    saveState(options) {
        if (typeof options === 'string') {
            options = {
                id : options
            };
        }
        else {
            options = options || {};
        }

        const
            me                = this,
            { stateProvider } = me,
            statefulId        = options.id || (me.isStatefulActive && me.statefulId);

        if (statefulId && stateProvider) {
            if (options.immediate) {
                me.isSaveStatePending = false;

                stateProvider.setValue(statefulId, me.state);
            }
            else if (!me.isSaveStatePending) {
                me.isSaveStatePending = true;

                stateProvider.saveStateful(me, options);
            }

            return statefulId;
        }
    }

    suspendStateful() {
        ++this.statefulSuspended;
    }

    //---------------------------------------------------------------------------------------------------------------
    // Private / Internal

    onConfigChange({ name, value, was, config }) {
        super.onConfigChange({ name, value, was, config });

        if (!this.isConstructing && this.isStatefulActive && this.statefulId) {
            const { stateful } = this;

            if (Array.isArray(stateful) ? stateful.includes(name) : stateful?.[name]) {
                this.saveState();
            }
        }
    }

    onStatefulEvent() {
        if (!this.isConstructing) {
            this.saveState();
        }
    }

    /**
     * Returns an object that copies the {@link #config-stateful} config properties from the provided `state` object.
     *
     * @param {Object} state A state object from which to copy stateful configs.
     * @returns {Object}
     * @category State
     * @private
     */
    pruneState(state) {
        const { statefulness } = this;

        if (statefulness) {
            const pruned = {};

            for (const key in state) {
                if (statefulness[key]) {
                    pruned[key] = state[key];
                }
            }

            state = pruned;
        }

        return state;
    }

    //---------------------------------------------------------------------------------------------------------------
    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
