/* eslint-disable bryntum/no-listeners-in-lib,bryntum/no-on-in-lib */
import Base from '../Base.js';
import ArrayHelper from '../helper/ArrayHelper.js';
import StringHelper from '../helper/StringHelper.js';
import VersionHelper from '../helper/VersionHelper.js';
import BrowserHelper from '../helper/BrowserHelper.js';
import FunctionHelper from '../helper/FunctionHelper.js';
import Objects from '../helper/util/Objects.js';

/**
 * @module Core/mixin/Events
 */

/**
 * @typedef {Object.<String,Function|Boolean|Object|Object[]|Number|String>} BryntumListenerConfig
 * @property {Object} [thisObj] The `this` reference for all listeners. May be overridden if a handler is specified in object form.
 * @property {Boolean} [once] Specify as `true` to remove the listener as soon as it is invoked.
 * @property {Number|Object} [expires] The listener only waits for a specified time before
 * being removed. The value may be a number or an object containing an expiry handler.
 * @property {Number} [expires.delay] How long to wait for the event for.
 * @property {String|Function} [expires.alt] The function to call when the listener expires **without having been triggered**.
 * @property {Object[]} [args] An array of arguments to be passed to the handler before the event object.
 * @property {Number} [prio] The priority for all listeners; higher priority listeners are called before lower.
 * @property {Number} [buffer] A buffer time in milliseconds to wait after last event trigger to call the handler, to reduce the amount of handler calls for frequent events.
 * @property {Number} [throttle] A millisecond timeout value to throttle event triggering. With it specified a handler
 * will be called once immediately and then all following calls during the timeout period will be grouped together into one call once per throttle period.
 */

const
    // Used by the config system to flatten configs from the class hierarchy.
    // In this case, a pure merge is not wanted. Listener definitions from
    // every class level are collected up into an array.
    // addListener iterates any passed array, adding each element.
    { isArray }        = Array,
    { hasOwnProperty } = Object.prototype,
    // Used to distinguish event names from listener options in addListener object config.
    specialProperties  = {
        thisObj    : 1,
        detachable : 1,
        once       : 1,
        detacher   : 1,
        prio       : 1,
        args       : 1,
        expires    : 1,
        buffer     : 1,
        throttle   : 1,
        name       : 1,
        $internal  : 1
    },
    priorityComparator = (a, b) => b.prio - a.prio;

/**
 * Mix this into another class to enable event handling.
 *
 * ## Basic usage
 * Listeners can be added either through config:
 *
 * ```javascript
 * let button = new Button({
 *   listeners: {
 *     click: () => {},
 *     press: () => {},
 *     ...
 *   }
 * });
 * ```
 *
 * *NOTE*: Do not reuse listeners config object, use new every time:
 * ```javascript
 * // wrong
 * let config = { click : () => {} }
 * new Button({
 *     listeners : config
 * })
 * new Button({
 *     listeners : config
 * })
 * // right
 * new Button({
 *     listeners : { click : () => {} }
 * })
 * new Button({
 *     listeners : { click : () => {} }
 * })
 * ```
 *
 * Or by calling on()/addListener():
 *
 * ```javascript
 * let button = new Button();
 *
 * button.addListener('press', () => {});
 * // on is an alias for addListener
 * button.on('click', () => {});
 * ```
 *
 * This style also accepts multiple listeners in same way as when using config:
 *
 * ```javascript
 * button.on({
 *   click: () => {},
 *   press: () => {},
 *   ...
 * });
 * ```
 *
 * ### Handlers as function name
 *
 * Event handlers may be specified as a function __name__. If a string is specified, it is the name
 * of the function in the `thisObj` object.
 *
 * If the string begins with `up.`, the owning object's ownership hierarchy
 * (if present) is scanned for an object which implements that function name:
 *
 * ```javascript
 * new Popup({
 *     tbar : {
 *         items : {
 *             myCombo : {
 *                 type      : 'combo',
 *                 editable  : false,
 *                 label     : 'Type',
 *                 listeners : {
 *                     // Look in owner chain for this function name
 *                     change : 'up.onFilterChange'
 *                 },
 *                 items     : [
 *                     'Event',
 *                     'Task',
 *                     'Appointment'
 *                 ]
 *             }
 *         }
 *     },
 *     items : {
 *         ...
 *     },
 *     onFilterChange({ value }) {
 *         // Handle event type selection here
 *     }
 * });
 *```
 *
 * ## Listener options
 * ### Once
 * Listeners can be configured to automatically deregister after first trigger by specifying config option `once`:
 *
 * ```javascript
 * button.on({
 *   click: () => {},
 *   once: true
 * });
 * ```
 *
 * ### Priority
 * Specifying priority affects the order in which listeners are called when triggering an event. Higher priorities will be
 * called before lower. Default value is 0.
 *
 * ```javascript
 * button.on({
 *   click: this.onClick,
 *   prio: 1
 * });
 * ```
 *
 * ### This reference
 * If desired, you can specify thisObj when configuring listeners. There is no need if you are using arrow functions as
 * listeners, but might be handy in other cases. Of course, you can also use bind to set `this` reference.
 *
 * ```javascript
 * button.on({
 *   click: this.onClick,
 *   thisObj: this
 * });
 *
 * // or
 *
 * button.on({
 *   click: this.onClick.bind(this)
 * });
 * ```
 *
 * ### Buffering
 * By specifying a `buffer` events that fire frequently can be grouped together and delayed. A handler for the event will be called once only, when no new event has been fired during the specified buffer time:
 *
 * ```javascript
 * button.on({
 *   click  : this.onClick,
 *   buffer : 200 // in milliseconds
 * });
 * ```
 *
 * In this example, if a user clicked the button 6 times very fast (<200ms between each click),
 * the `this.onClick` handler would be called only once 200 milliseconds after the last click.
 *
 * ### Throttling
 * Create a "debounced" function which will call on the "leading edge" of a timer period.
 * When first invoked will call immediately, but invocations after that inside its buffer
 * period will be rejected, and *one* invocation will be made after the buffer period has expired.
 *
 * This is useful for responding immediately to a first mousemove, but from then on, only
 * calling the action function on a regular timer while the mouse continues to move.

 * ```javascript
 * button.on({
 *   click    : this.onClick,
 *   throttle : 200 // in milliseconds
 * });
 * ```
 *
 * In this example, if a user clicked the button 6 times very fast, the `this.onClick` handler would be called once immediately on the first click and a second time 200 milliseconds after the **first** click.
 * So in reality the `click` event handler will be called every 200ms independent of amount of click in a middle, if the event was triggered at least once during the `throttle` timeout.
 *
 * ### Detacher
 * A convenient way of unregistering events is to use a detacher, a function returned when adding listeners that you
 * call later to deregister them. As of version 1.0, detachable defaults to true.
 *
 * ```javascript
 * let detacher = button.on({
 *   click: () => {},
 *   press: () => {},
 *   detachable: true
 * });
 *
 * // when you want to detach, for example in destroy()
 * detacher();
 * ```
 *
 * ### Auto detaching
 * When listeners are bound to a class instance using `thisObj`, the `thisObj`'s `doDestroy` method
 * is overridden to remove the listeners before calling the overridden doDestroy.
 *
 * ```javascript
 * class MyClass extends Base {
 *   construct() {
 *     let button = new Button({
 *       listeners: {
 *         click: () => {},
 *         thisObj: this
 *       }
 *     });
 *   }
 *
 *   doDestroy() {
 *     // clean up stuff
 *   }
 * }
 *
 * let myObj = new MyClass();
 * // clean up, also removes listeners
 * myObj.destroy();
 * ```
 *
 * ### On-functions
 * When mixing Events into another class it can be configured to call on-functions when events are triggered.
 * On-functions are functions named 'onEventName', for example 'onClick', 'onPress' declared on the class triggering
 * the event.
 *
 * ```javascript
 * // mix Events in with on-functions activated
 * let button = new Button({
 *   callOnFunctions: true,
 *
 *   onClick: () => {}
 * });
 *
 * // or add a getter in class declaration
 * ```
 *
 * Returning `false` from an on-function will prevent triggering listeners for the event.
 *
 * ### Catching all events
 * By specifying a listener for {@link #event-catchAll catchAll} a function can be notified when any event is triggered:
 *
 * ```javascript
 * const button = new Button({
 *    listeners : {
 *        catchAll(event) {
 *            // All events on the button will pass through here
 *        }
 *    }
 * });
 * ```
 *
 * ## Preventable events
 *
 * By returning `false` from a listener for an event documented as `preventable` the action that would otherwise be
 * executed after the event is prevented. These events are usually named `beforeXX`, for example `beforeRemove`,
 * `beforeDragStart` etc.
 *
 * <div class="note">Note that Angular does not support return values from listeners. Instead, assign to
 * <code>event.returnValue</code> as shown in the Angular snippet below</div>
 *
 * {@frameworktabs}
 * {@js}
 * ```javascript
 * taskBoard.on({
 *     beforeColumnDrag({ columnRecord }) {
 *         if (columnRecord.locked) {
 *             return false;
 *         }
 *     }
 * });
 * ```
 *
 * {@endjs}
 * {@react}
 *
 * ```jsx
 * const App = props => {
 *     function onBeforeColumnDrag({ columnRecord }) {
 *         if (columnRecord.locked) {
 *             return false;
 *         }
 *     }
 *
 *     return (
 *         <>
 *             <BryntumTaskBoard onBeforeColumnDrag={onBeforeColumnDrag} />
 *         </>
 *     )
 * }
 * ```
 *
 * {@endreact}
 * {@vue}
 *
 * ```html
 * <bryntum-task-board @beforeColumnDrag="onBeforeColumnDrag" />
 * ```
 *
 * ```javascript
 * export default {
 *     methods : {
 *         onBeforeColumnDrag({ columnRecord }) {
 *             if (columnRecord.locked) {
 *                 return false;
 *             }
 *         }
 *    }
 * }
 * ```
 *
 * {@endvue}
 * {@angular}
 *
 * ```html
 * <bryntum-task-board (onBeforeColumnDrag)="onBeforeColumnDrag({event : $event})"></bryntum-task-board>
 * ```
 *
 * ```typescript
 * export class AppComponent {
 *     onBeforeColumnDrag({ event }: { event: any }): void {
 *         event.returnValue = !event.columnRecord.locked;
 *     }
 *  }
 * ```
 *
 * {@endangular}
 * {@endframeworktabs}
 *
 * @mixin
 */
export default Target => class Events extends (Target || Base) {
    eventsSuspended = null;

    static get $name() {
        return 'Events';
    }

    //region Events

    /**
     * Fires before an object is destroyed.
     * @event beforeDestroy
     * @param {Core.Base} source The Object that is being destroyed.
     */

    /**
     * Fires when an object is destroyed.
     * @event destroy
     * @param {Core.Base} source The Object that is being destroyed.
     */

    /**
     * Fires when any other event is fired from the object.
     *
     * **Note**: `catchAll` is fired for both public and private events. Please rely on the public events only.
     * @event catchAll
     * @param {Object} event The Object that contains event details
     * @param {String} event.type The type of the event which is caught by the listener
     */

    //endregion

    static get declarable() {
        return [
            /**
             * The list of deprecated events as an object, where `key` is an event name which is deprecated and
             * `value` is an object which contains values for
             * {@link Core.helper.VersionHelper#function-deprecate-static VersionHelper}:
             * - product {String} The name of the product;
             * - invalidAsOfVersion {String} The version where the offending code is invalid (when any compatibility
             *   layer is actually removed);
             * - message {String} Warning message to show to the developer using a deprecated API;
             *
             * For example:
             *
             * ```javascript
             * return {
             *     click : {
             *         product            : 'Grid',
             *         invalidAsOfVersion : '1.0.0',
             *         message            : 'click is deprecated!'
             *     }
             * }
             * ```
             *
             * @name deprecatedEvents
             * @returns {Object}
             * @static
             * @internal
             */
            'deprecatedEvents'
        ];
    }

    static setupDeprecatedEvents(cls, meta) {
        const
            all = meta.getInherited('deprecatedEvents'),
            add = cls.deprecatedEvents;

        for (const eventName in add) {
            // Event names are case-insensitive so build our map using toLowerCased names (but keep true case too):
            all[eventName.toLowerCase()] = all[eventName] = add[eventName];
        }
    }

    //region Config

    static get configurable() {
        return {

            /**
             * Set to true to call onXXX method names (e.g. `onShow`, `onClick`), as an easy way to listen for events.
             *
             * ```javascript
             * const container = new Container({
             *     callOnFunctions : true
             *
             *     onHide() {
             *          // Do something when the 'hide' event is fired
             *     }
             * });
             * ```
             *
             * @config {Boolean} callOnFunctions
             * @category Misc
             * @default false
             */

            /**
             * The listener set for this object.
             *
             * An object whose property names are the names of events to handle, or options which modifiy
             * __how__ the handlers are called.
             *
             * See {@link #function-addListener} for details about the options.
             *
             * Listeners can be specified in target class config and they will be merged with any listeners specified in
             * the instantiation config. Class listeners will be fired first:
             *
             * ```javascript
             * class MyStore extends Store({
             *     static get configurable() {
             *         return {
             *             listeners : {
             *                 myCustomEvent() {
             *                 },
             *                 load : {
             *                     prio : 10000,
             *                     fn() { // this load listener handles things first }
             *                 }
             *             }
             *         }
             *     }
             * });
             *
             * let store = new MyStore({
             *   listeners: {
             *     load: () => { // This load listener runs after the class's },
             *     ...
             *   }
             * });
             * ```
             *
             * ### Handlers as function name
             *
             * Object event handlers may be specified as a function __name__. If a string is specified, it is the name
             * of the function in the `thisObj` object.
             *
             * If the string begins with `up.`, this object's ownership hierarchy
             * (if present) is scanned for an object which implements that function name:
             *
             * ```javascript
             * new Popup({
             *     tbar : {
             *         items : {
             *             myCombo : {
             *                 type      : 'combo',
             *                 editable  : false,
             *                 label     : 'Type',
             *                 listeners : {
             *                     // Look in owner chain for this function name
             *                     change : 'up.onFilterChange'
             *                 },
             *                 items     : [
             *                     'Event',
             *                     'Task',
             *                     'Appointment'
             *                 ]
             *             }
             *         }
             *     },
             *     items : {
             *         ...
             *     },
             *     onFilterChange({ value }) {
             *         // Handle event type selection here
             *     }
             * });
             *```
             *
             * @config {Object}
             * @category Common
             */
            listeners : {
                value : null,

                $config : {
                    merge(newValue, currentValue) {
                        if (newValue !== null) {
                            if (!newValue) {
                                return currentValue;
                            }
                            if (currentValue) {
                                newValue = newValue ? [newValue] : [];
                                newValue.push[isArray(currentValue) ? 'apply' : 'call'](newValue, currentValue);
                            }
                        }
                        return newValue;
                    }
                }
            },

            /**
             * Internal listeners, that cannot be removed by the user.
             * @config {Object}
             * @internal
             */
            internalListeners : null,

            /**
             * An object where property names with a truthy value indicate which events should bubble up the ownership
             * hierarchy when triggered.
             *
             * ```javascript
             * const container = new Container({
             *     items : [
             *        { type : 'text', bubbleEvents : { change : true }}
             *     ],
             *
             *     listeners : {
             *         change() {
             *             // Will catch change event from the text field
             *         }
             *     }
             * });
             * ```
             *
             * @config {Object}
             * @category Misc
             */
            bubbleEvents : null

        };
    }

    destroy() {
        this.trigger('beforeDestroy');
        super.destroy();
    }

    //endregion

    //region Init

    construct(config, ...args) {
        // Configured listeners use this as the thisObj
        if ((this.configuredListeners /* assignment */ = config?.listeners)) {
            // We have to copy in case listeners have been forked
            config = Objects.assign({}, config);
            delete config.listeners;
        }

        super.construct(config, ...args);
        // Apply configured listeners after construction.
        // Note that some classes invoke this during parts of their construction.
        // Store invokes this prior to setting data so that observers are notified of data load.
        this.processConfiguredListeners();
    }

    processConfiguredListeners() {
        // This can only happen once
        if (this.configuredListeners) {
            const
                me                = this,
                { isConfiguring } = me;

            // If called from config ingestion during configuration, listeners must be added
            // so temporarily clear the isConfiguring flag.
            me.isConfiguring = false;
            me.listeners = me.configuredListeners;
            me.configuredListeners = null;
            me.isConfiguring = isConfiguring;
        }
    }

    /**
     * Auto detaches listeners registered from start, if set as detachable
     * @internal
     */
    doDestroy() {
        this.trigger('destroy');
        this.removeAllListeners(false);
        super.doDestroy();
    }

    static setupClass(meta) {
        super.setupClass(meta);

        Events.prototype.onListen.$nullFn = true;
        Events.prototype.onUnlisten.$nullFn = true;
    }

    //endregion

    //region Listeners

    /**
     * Adds an event listener. This method accepts parameters in the following format:
     *
     * ```javascript
     *  myObject.addListener({
     *      thisObj    : this,          // The this reference for the handlers
     *      eventname2 : 'functionName' // Resolved at invocation time using the thisObj,
     *      otherevent : {
     *          fn      : 'handlerFnName',
     *          once    : true          // Just this handler is auto-removed on fire
     *      },
     *      yetanother  : {
     *          fn      : 'yetAnotherHandler',
     *          args    : [ currentState1, currentState2 ] // Capture info to be passed to handler
     *      },
     *      prio        : 100           // Higher prio listeners are called before lower
     *  });
     * ```
     *
     * When listeners have a `thisObj` option, they are linked to the lifecycle of that object.
     * When it is destroyed, those listeners are removed.
     *
     * The `config` parameter allows supplying options for the listener(s), for available options see {@link #typedef-BryntumListenerConfig}.
     *
     * A simpler signature may be used when only adding a listener for one event and no extra options
     * (such as `once` or `delay`) are required:
     *
     * ```javascript
     * myObject.addListener('click', myController.handleClicks, myController);
     * ```
     *
     * The args in this simple case are `eventName`, `handler` and `thisObj`
     *
     * @param {BryntumListenerConfig|String} config An object containing listener definitions, or the event name to listen for
     * @param {Object|Function} [thisObj] Default `this` reference for all listeners in the config object, or the handler
     * function to call if providing a string as the first arg.
     * @param {Object} [oldThisObj] The `this` reference if the old signature starting with a string event name is used..
     * @returns {Function} Returns a detacher function unless configured with `detachable: false`. Call detacher to remove listeners
     */
    addListener(config, thisObj, oldThisObj) {
        if (isArray(config)) {
            for (let i = 0, { length } = config; i < length; i++) {
                this.addListener(config[i], thisObj);
            }
            return;
        }

        const
            me               = this,
            deprecatedEvents = me.$meta.getInherited('deprecatedEvents');

        if (typeof config === 'string') {
            // arguments[2] is thisObj if (eventname, handler, thisObj) form called.
            // Note that the other side of the if compares to undefined, so this will work.
            return me.addListener({
                [config]   : thisObj,
                detachable : thisObj.detachable !== false,
                thisObj    : oldThisObj
            });
        }
        else {
            // Capture the default thisObj.
            thisObj = config.thisObj = config.thisObj !== undefined ? config.thisObj : thisObj;

            for (const key in config) {
                // Skip special properties or events without handlers (convenient syntax with optional handlers)
                if (!specialProperties[key] && config[key] != null) {
                    // comparing should be case insensitive
                    const
                        // comparing should be case insensitive
                        eventName       = key.toLowerCase(),
                        deprecatedEvent = deprecatedEvents?.[eventName],
                        events          = me.eventListeners || (me.eventListeners = {}),
                        listenerSpec    = config[key],
                        expires         = listenerSpec.expires || config.expires,
                        listener        = {
                            fn        : typeof listenerSpec === 'object' ? listenerSpec.fn : listenerSpec,
                            thisObj   : listenerSpec.thisObj !== undefined ? listenerSpec.thisObj : thisObj,
                            args      : listenerSpec.args || config.args,
                            prio      : listenerSpec.prio !== undefined ? listenerSpec.prio : config.prio !== undefined ? config.prio : 0,
                            once      : listenerSpec.once !== undefined ? listenerSpec.once : config.once !== undefined ? config.once : false,
                            buffer    : listenerSpec.buffer || config.buffer,
                            throttle  : listenerSpec.throttle || config.throttle,
                            $internal : config.$internal,
                            catchAll  : key === 'catchAll'
                        };

                    if (deprecatedEvent) {
                        const { product, invalidAsOfVersion, message } = deprecatedEvent;
                        VersionHelper.deprecate(product, invalidAsOfVersion, message);
                    }


                    if (expires) {
                        // Extract expires : { delay : 100, alt : 'onExpireFn' }
                        const
                            { alt } = expires,
                            delay   = alt ? expires.delay : expires,
                            name    = config.name || key,
                            fn      = () => {
                                me.un(eventName, listener);

                                // If we make it here and the handler has not been called, invoke the alt handler
                                if (alt && !listener.called) {
                                    me.callback(alt, thisObj);
                                }
                            };
                        if (me.isDelayable) {
                            me.setTimeout({ fn, name, cancelOutstanding : true, delay });
                        }
                        else {
                            globalThis.setTimeout(fn, delay);
                        }
                    }

                    let listeners = events[eventName] || (events[eventName] = []);

                    if (listeners.$firing) {
                        events[eventName] = listeners = listeners.slice();
                    }

                    // Insert listener directly in prio order
                    listeners.splice(
                        ArrayHelper.findInsertionIndex(listener, listeners, priorityComparator, listeners.length),
                        0, listener);

                    if (!me.onListen.$nullFn && listeners.length < 2) {
                        me.onListen(eventName);
                    }

                    // Hook to call when a listener is added
                    me.afterAddListener?.(eventName, listener);
                }
            }

            if (config.relayAll) {
                me.relayAll(config.relayAll);
            }

            // Hook into the thisObj's destruction sequence to remove these listeners.
            // Pass the default thisObj in for use when it comes to destruction time.
            if (thisObj && thisObj !== me) {
                me.attachAutoDetacher(config, thisObj);
            }

            const
                detachable = config.detachable !== false,
                name       = config.name,
                destroy    = (config.expires || detachable || name) ? () => {
                    // drop listeners if not destroyed yet
                    if (!me.isDestroyed) {
                        me.removeListener(config, thisObj);
                    }
                } : null;

            if (destroy) {
                destroy.eventer = me;
                destroy.listenerName = name;

                if (name && thisObj?.trackDetacher) {
                    thisObj.trackDetacher(name, destroy);
                }

                if (config.expires) {
                    // handle expires : { alt : timeoutHandler, delay : 2000 }
                    me.delay(destroy, isNaN(config.expires) ? config.expires.delay : config.expires, name);
                }

                if (detachable) {
                    return destroy;
                }
            }
        }
    }

    /**
     * Alias for {@link #function-addListener}. Adds an event listener. This method accepts parameters in the following format:
     *
     * ```javascript
     *  myObject.on({
     *      thisObj    : this,          // The this reference for the handlers
     *      eventname2 : 'functionName' // Resolved at invocation time using the thisObj,
     *      otherevent : {
     *          fn      : 'handlerFnName',
     *          once    : true          // Just this handler is auto-removed on fire
     *      },
     *      yetanother  : {
     *          fn      : 'yetAnotherHandler',
     *          args    : [ currentState1, currentState2 ] // Capture info to be passed to handler
     *      },
     *      prio        : 100           // Higher prio listeners are called before lower
     *  });
     * ```
     *
     * When listeners have a `thisObj` option, they are linked to the lifecycle of that object.
     * When it is destroyed, those listeners are removed.
     *
     * The `config` parameter allows supplying options for the listener(s), for available options see {@link #typedef-BryntumListenerConfig}.
     *
     * A simpler signature may be used when only adding a listener for one event and no extra options
     * (such as `once` or `delay`) are required:
     *
     * ```javascript
     * myObject.on('click', myController.handleClicks, myController);
     * ```
     *
     * The args in this simple case are `eventName`, `handler` and `thisObj`
     *
     * @param {BryntumListenerConfig|String} config An object containing listener definitions, or the event name to listen for
     * @param {Object|Function} [thisObj] Default `this` reference for all listeners in the config object, or the handler
     * function to call if providing a string as the first arg.
     * @param {Object} [oldThisObj] The `this` reference if the old signature starting with a string event name is used..
     * @returns {Function} Returns a detacher function unless configured with `detachable: false`. Call detacher to remove listeners
     */
    on(config, thisObj, oldThisObj) {
        return this.addListener(config, thisObj, oldThisObj);
    }

    /**
     * Internal convenience method for adding an internal listener, that cannot be removed by the user.
     *
     * Alias for `on({ $internal : true, ... })`. Only supports single argument form.
     *
     * @internal
     */
    ion(config) {
        config.$internal = true;
        return this.on(config);
    }

    /**
     * Shorthand for {@link #function-removeListener}
     * @param {Object|String} config A config object or the event name
     * @param {Object|Function} [thisObj] `this` reference for all listeners, or the listener function
     * @param {Object} [oldThisObj] `this` The `this` object for the legacy way of adding listeners
     */
    un(...args) {
        this.removeListener(...args);
    }

    updateInternalListeners(internalListeners, oldInternalListeners) {
        oldInternalListeners?.detach();

        if (internalListeners) {
            internalListeners.detach = this.ion(internalListeners);
        }
    }

    get listeners() {
        return this.eventListeners;
    }

    changeListeners(listeners) {
        // If we are receiving class listeners, add them early, and they do not become
        // the configured listeners, and are not removed by setting listeners during the lifecycle.
        if (this.isConfiguring) {
            // Pull in internal listeners first
            this.getConfig('internalListeners');

            if (listeners) {
                this.on(listeners, this);
            }
        }
        // Setting listeners after config time clears the old set and adds the new.
        // This will initially happen at the tail end of the constructor when config
        // listeners are set.
        else {
            // Configured listeners use this as the thisObj by default.
            // Flatten using Objects.assign because it may have been part of
            // a prototype chained default configuration of another object.
            // eg: the tooltip config of a Widget.
            // listener object blocks from multiple configuration levels are pushed
            // onto an array (see listeners merge function in configurable block above).
            // If this has happened, each entry must be processed like this.
            if (Array.isArray(listeners)) {
                for (let i = 0, l = listeners[0], { length } = listeners; i < length; l = listeners[++i]) {
                    if (!('thisObj' in l)) {
                        listeners[i] = Objects.assign({ thisObj : this }, l);
                    }
                }
            }
            else if (listeners && !('thisObj' in listeners)) {
                listeners = Objects.assign({ thisObj : this }, listeners);
            }

            return listeners;
        }
    }

    updateListeners(listeners, oldListeners) {
        // Only configured listeners get here. Class listeners are added by changeListeners.
        oldListeners && this.un(oldListeners);
        listeners && this.on(listeners);
    }

    /**
     * Removes an event listener. Same API signature as {@link #function-addListener}
     * @param {Object|String} config A config object or the event name
     * @param {Object|Function} thisObj `this` reference for all listeners, or the listener function
     * @param {Object} oldThisObj `this` The `this` object for the legacy way of adding listeners
     */
    removeListener(config, thisObj = config.thisObj, oldThisObj) {
        const me = this;

        if (typeof config === 'string') {
            return me.removeListener({ [config] : thisObj }, oldThisObj);
        }

        Object.entries(config).forEach(([eventName, listenerToRemove]) => {
            if (!specialProperties[eventName] && listenerToRemove != null) {
                eventName = eventName.toLowerCase();

                const
                    { eventListeners } = me,
                    index = me.findListener(eventName, listenerToRemove, thisObj);

                if (index >= 0) {
                    let listeners = eventListeners[eventName];

                    // Hook to call when a listener is removed (slightly before removing for now)
                    me.afterRemoveListener?.(eventName, listeners[index]);

                    if (listeners.length > 1) {
                        if (listeners.$firing) {
                            eventListeners[eventName] = listeners = listeners.slice();
                        }

                        // NOTE: we cannot untrack any detachers here because we may only be
                        // removing some of its listeners
                        listeners.splice(index, 1);
                    }
                    else {
                        delete eventListeners[eventName];

                        if (!me.onUnlisten.$nullFn) {
                            me.onUnlisten(eventName);
                        }
                    }
                }
            }
        });

        if (config.thisObj && !config.thisObj.isDestroyed) {
            me.detachAutoDetacher(config);
        }
    }

    /**
     * Finds the index of a particular listener to the named event. Returns `-1` if the passed
     * function/thisObj listener is not present.
     * @param {String} eventName The name of an event to find a listener for.
     * @param {String|Function} listenerToFind The handler function to find.
     * @param {Object} defaultThisObj The `thisObj` for the required listener.
     * @internal
     */
    findListener(eventName, listenerToFind, defaultThisObj) {
        const
            eventListeners = this.eventListeners?.[eventName],
            fn             = listenerToFind.fn || listenerToFind,
            thisObj        = listenerToFind.thisObj || defaultThisObj;

        if (eventListeners) {
            for (let listenerEntry, i = 0, { length } = eventListeners; i < length; i++) {
                listenerEntry = eventListeners[i];

                if (listenerEntry.fn === fn && listenerEntry.thisObj === thisObj) {
                    return i;
                }
            }
        }

        return -1;
    }

    /**
     * Check if any listener is registered for the specified eventName
     * @param {String} eventName
     * @returns {Boolean} `true` if listener is registered, otherwise `false`
     * @advanced
     */
    hasListener(eventName) {
        return Boolean(this.eventListeners?.[eventName?.toLowerCase()]);
    }

    /**
     * Relays all events through another object that also implements Events mixin. Adds a prefix to the event name
     * before relaying, for example add -> storeAdd
     * ```
     * // Relay all events from store through grid, will make it possible to listen for store events prefixed on grid:
     * 'storeLoad', 'storeChange', 'storeRemoveAll' etc.
     * store.relayAll(grid, 'store');
     *
     * grid.on('storeLoad', () => console.log('Store loaded');
     * ```
     * @param {Core.mixin.Events} through Object to relay the events through, needs to mix Events mixin in
     * @param {String} prefix Prefix to add to event name
     * @param {Boolean} [transformCase] Specify false to prevent making first letter of event name uppercase
     * @advanced
     */
    relayAll(through, prefix, transformCase = true) {
        if (!this.relayAllTargets) {
            this.relayAllTargets = [];
        }

        const { relayAllTargets } = this;

        through.ion({
            beforeDestroy : ({ source }) => {
                if (source === through) {
                    const configs = relayAllTargets.filter(r => r.through === through);

                    configs.forEach(config => ArrayHelper.remove(relayAllTargets, config));
                }
            }
        });

        relayAllTargets.push({ through, prefix, transformCase });
    }

    /**
     * Removes all listeners registered to this object by the application.
     */
    removeAllListeners(preserveInternal = true) {
        const listeners = this.eventListeners;
        let i, thisObj;

        for (const event in listeners) {
            const bucket = listeners[event];

            // We iterate backwards since we call removeListener which will splice out of
            // this array as we go...
            for (i = bucket.length; i-- > 0; /* empty */) {
                const cfg = bucket[i];

                if (!cfg.$internal || !preserveInternal) {
                    this.removeListener(event, cfg);

                    thisObj = cfg.thisObj;

                    thisObj?.untrackDetachers?.(this);
                }
            }
        }
    }

    relayEvents(source, eventNames, prefix = '') {
        const listenerConfig = { detachable : true, thisObj : this };

        eventNames.forEach(eventName => {
            listenerConfig[eventName] = (event, ...params) => {
                return this.trigger(prefix + eventName, event, ...params);
            };
        });

        return source.on(listenerConfig);
    }

    /**
     * This method is called when the first listener for an event is added.
     * @param {String} eventName
     * @internal
     */
    onListen() {}

    /**
     * This method is called when the last listener for an event is removed.
     * @param {String} eventName
     * @internal
     */
    onUnlisten() {}

    destructorInterceptor() {
        const { autoDetachers, target, oldDestructor } = this;
        // Remove listeners first, so that they do not fire during destruction.
        // The observable being listened to by the thisObj may already have
        // been destroyed in a clean up sequence
        for (let i = 0; i < autoDetachers.length; i++) {
            const { dispatcher, config } = autoDetachers[i];
            if (!dispatcher.isDestroyed) {
                dispatcher.removeListener(config, target);
            }
        }

        oldDestructor.call(target);
    }

    /**
     * Internal function used to hook destroy() calls when using thisObj
     * @private
     */
    attachAutoDetacher(config, thisObj) {
        const
            target         = config.thisObj || thisObj,
            // If it's a Bryntum Base subclass, hook doDestroy, otherwise, destroy
            destructorName = ('doDestroy' in target) ? 'doDestroy' : 'destroy';

        if (destructorName in target) {
            let { $autoDetachers } = target;

            if (!$autoDetachers) {
                target.$autoDetachers = $autoDetachers = [];
            }

            if (!target.$oldDestructor) {
                target.$oldDestructor = target[destructorName];

                // Binding instead of using closure (used to use FunctionHelper.createInterceptor) to not retain target
                // when detaching manually
                target[destructorName] = this.destructorInterceptor.bind({
                    autoDetachers : $autoDetachers,
                    oldDestructor : target.$oldDestructor,
                    target
                });
            }

            $autoDetachers.push({ config, dispatcher : this });
        }
        else {
            target[destructorName] = () => {
                this.removeListener(config);
            };
        }
    }

    /**
     * Internal function used restore hooked destroy() calls when using thisObj
     * @private
     */
    detachAutoDetacher(config) {
        const target = config.thisObj;

        // Restore old destructor and remove from auto detachers only if we are not called as part of destruction.
        // (Altering $autoDetachers affects destruction iterating over them, breaking it. It is pointless to clean up
        // during destruction anyway, since everything gets removed)
        if (target.$oldDestructor && !target.isDestroying) {
            ArrayHelper.remove(
                target.$autoDetachers,
                target.$autoDetachers.find(detacher => detacher.config === config && detacher.dispatcher === this)
            );

            if (!target.$autoDetachers.length) {
                target['doDestroy' in target ? 'doDestroy' : 'destroy'] = target.$oldDestructor;
                target.$oldDestructor = null;
            }
        }
    }

    //endregion

    //region Promise based workflow

    // experimental, used in tests to support async/await workflow
    await(eventName, options = { checkLog : true, resetLog : true, args : null }) {
        const me = this;

        if (options === false) {
            options = { checkLog : false };
        }

        const { args } = options;

        return new Promise(resolve => {
            // check if previously triggered?
            if (options.checkLog && me._triggered?.[eventName]) {
                // resolve immediately, no params though...
                resolve();
                // reset log to be able to await again
                if (options.resetLog) {
                    me.clearLog(eventName);
                }
            }

            // This branch will listen for events until catches one with specific arguments
            if (args) {
                const detacher = me.on({
                    [eventName] : (...params) => {
                        // if args is a function use it to match arguments
                        const argsOk = typeof args === 'function' ? args(...params) : Object.keys(args).every(key => {
                            return key in params[0] && params[0][key] === args[key];
                        });

                        if (argsOk) {
                            // resolve when event is fired with required arguments
                            resolve(...params);

                            // reset log to be able to await again
                            if (options.resetLog) {
                                me.clearLog(eventName);
                            }

                            detacher();
                        }
                    },
                    prio : -10000 // Let others do their stuff first
                });
            }
            else {
                me.on({
                    [eventName] : (...params) => {
                        // resolve when event is caught
                        resolve(...params);
                        // reset log to be able to await again
                        if (options.resetLog) {
                            me.clearLog(eventName);
                        }
                    },
                    prio : -10000, // Let others do their stuff first
                    once : true // promises can only be resolved once anyway
                });
            }
        });
    }

    clearLog(eventName) {
        if (this._triggered) {
            if (eventName) {
                delete this._triggered[eventName];
            }
            else {
                this._triggered = {};

            }
        }
    }

    //endregion

    //region Trigger

    /**
     * Triggers an event, calling all registered listeners with the supplied arguments. Returning false from any listener
     * makes function return false.
     * @param {String} eventName Event name for which to trigger listeners
     * @param {Object} [param] Single parameter passed on to listeners, source property will be added to it (this)
     * @param {Boolean} [param.bubbles] Pass as `true` to indicate that the event will bubble up the widget
     * ownership hierarchy. For example up a `Menu`->`parent` Menu tree, or a `Field`->`Container` tree.
     * @typings param -> {{bubbles?: boolean, [key: string]: any}}
     * @returns {Boolean|Promise} Returns false if any listener returned `false`, or a `Promise` yielding
     * `true` / `false` based on what is returned from the async listener functions, otherwise `true`
     * @async
     * @advanced
     */
    trigger(eventName, param) {


        const
            me   = this,
            name = eventName.toLowerCase(),
            {
                eventsSuspended,
                relayAllTargets,
                callOnFunctions
            }   = me;

        let listeners = me.eventListeners?.[name],
            handlerPromises;

        // log trigger, used by experimental promise support to resolve immediately when needed
        if (!me._triggered) {
            me._triggered = {};
        }
        me._triggered[eventName] = true;

        if (eventsSuspended) {
            if (eventsSuspended.shouldQueue) {
                eventsSuspended.queue.push(arguments);
            }
            return true;
        }

        // Include catchall listener for all events.
        // Do not push the catchAll listeners onto the events own listener array.
        if (me.eventListeners?.catchall) {
            (listeners = (listeners ? listeners.slice() : [])).push(...me.eventListeners.catchall);

            // The catchAll listeners must honour their prio settings.
            listeners.sort(priorityComparator);
        }

        if (!listeners && !relayAllTargets && !callOnFunctions) {
            return true;
        }

        // default to include source : this in param
        if (param) {
            if (!('source' in param)) {
                if (Object.isExtensible(param)) {
                    param.source = me;
                }
                else {
                    param = Object.setPrototypeOf({
                        source : me
                    }, param);
                }
            }
        }
        else {
            param = {
                source : me
            };
        }

        // Lowercased event name should be the "type" property in keeping with DOM events.
        if (param.type !== name) {
            // Create instance property because "type" is read only
            if (param.constructor !== Object) {
                Reflect.defineProperty(param, 'type', { get : () => name });
            }
            else {
                param.type = name;
            }
        }

        param.eventName = eventName;

        // Bubble according to `bubbleEvents` config if `bubbles` is not explicitly set
        if (!('bubbles' in param) && me.bubbleEvents?.[eventName]) {
            param.bubbles = me.bubbleEvents[eventName];
        }

        if (callOnFunctions) {
            const fnName = 'on' + StringHelper.capitalize(eventName);

            if (fnName in me) {
                // Return true if the on[fnName] is not set to keep default behavior
                const result = me[fnName] ? me.callback(me[fnName], me, [param]) : true;

                let inhibit;

                if (Objects.isPromise(result)) {
                    (handlerPromises || (handlerPromises = [])).push(result);
                }
                else {
                    inhibit = result === false || inhibit;
                }

                // See if the called function was injected into the instance
                // masking an implementation in the prototype.
                // we must call the class's implementation after the injected one.
                // unless it's an injected chained function, in which case it will have been called above.
                // Note: The handler may have resulted in destruction.
                if (!me.isDestroyed && hasOwnProperty.call(me, fnName) && !me.pluginFunctionChain?.[fnName]) {
                    const myProto = Object.getPrototypeOf(me);
                    if (fnName in myProto) {
                        const result = myProto[fnName].call(me, param);

                        if (Objects.isPromise(result)) {
                            (handlerPromises || (handlerPromises = [])).push(result);
                        }
                        else {
                            inhibit = result === false || inhibit;
                        }

                        // A handler may have resulted in destruction.
                        if (me.isDestroyed) {
                            return;
                        }
                    }
                }

                // Returning false from an on-function prevents further triggering
                if (inhibit) {
                    return false;
                }
            }
        }

        let ret;

        if (listeners) {
            let i = 0, internalAbort = false;

            // Let add/removeListener know that we're using the array to protect against a situation where an event
            // listener changes the listeners when triggering the event.
            listeners.$firing = true;

            // If any listener resulted in our destruction, abort.
            for (i; i < listeners.length && !me.isDestroyed && !internalAbort; i++) {
                const listener = listeners[i];

                // Previously, returning false would abort all further listeners. But now internal listeners
                // are allowed to run anyway
                if (ret === false && !listener.$internal) {
                    continue;
                }

                let handler,
                    thisObj = listener.thisObj;

                // Listeners that have thisObj are auto removed when thisObj is destroyed. If thisObj is destroyed from
                // a listener we might still end up here, since listeners are sliced and not affected by the removal
                if (!thisObj || !thisObj.isDestroyed) {
                    // Flag for the expiry timer
                    listener.called = true;

                    if (listener.once) {
                        me.removeListener(name, listener);
                    }

                    // prepare handler function
                    if (typeof listener.fn === 'string') {
                        if (thisObj) {
                            handler = thisObj[listener.fn];
                        }

                        // keep looking for the callback in the hierarchy
                        if (!handler) {
                            const result = me.resolveCallback(listener.fn);

                            handler = result.handler;
                            thisObj = result.thisObj;
                        }
                    }
                    else {
                        handler = listener.fn;
                    }

                    // if `buffer` option is provided, the handler will be wrapped into buffer function,
                    // but only once on the first call
                    if (listener.buffer) {

                        if (!listener.bufferFn) {
                            const buffer = Number(listener.buffer);

                            if (typeof buffer !== 'number' || isNaN(buffer)) {
                                throw new Error(`Incorrect type for buffer, got "${buffer}" (expected a Number)`);
                            }
                            listener.bufferFn = FunctionHelper.createBuffered(handler, buffer, thisObj, listener.args);
                        }

                        handler = listener.bufferFn;
                    }

                    // if `throttle` option is provided, the handler will be called immediately, but all the rest calls
                    // that happened during time specified in `throttle`, will be delayed and glued into 1 call
                    if (listener.throttle) {
                        const throttle = Number(listener.throttle);

                        if (typeof throttle !== 'number' || isNaN(throttle)) {
                            throw new Error(`Incorrect type for throttle, got "${throttle}" (expected a Number)`);
                        }

                        if (!listener.throttledFn) {
                            listener.throttledFn = FunctionHelper.createThrottled(handler, throttle, thisObj, listener.args);
                        }

                        handler = listener.throttledFn;
                    }

                    const result = handler.call(thisObj || me, ...(listener.args || []), param);

                    // Store result until we get a false return value, to mimic the old behavior from before we carried
                    // on with calling internal listeners
                    if (ret !== false) {
                        ret = result;
                    }

                    if (listener.$internal && result === false) {
                        internalAbort = true;
                    }

                    if (Objects.isPromise(result)) {
                        result.$internal = listener.$internal;
                        // If a handler is async (returns a Promise), then collect all Promises.
                        // At the end we return a Promise which encapsulates all returned Promises
                        // or, if only one handler was async, *the* Promise.
                        // Don't allocate an Array until we have to.
                        (handlerPromises || (handlerPromises = [])).push(result);
                    }
                }
            }

            listeners.$firing = false;

            // An internal listener returned `false`, abort before relaying events etc.
            if (internalAbort) {
                return false;
            }
        }



        // relay all?
        relayAllTargets?.forEach(config => {
            let name = eventName;
            if (config.transformCase) {
                name = StringHelper.capitalize(name);
            }
            if (config.prefix) {
                name = config.prefix + name;
            }
            if (config.through.trigger(name, param) === false) {
                return false;
            }
        });

        // Use DOM standard event property name to indicate that the event
        // bubbles up the owner axis.
        // False from any handler cancels the bubble.
        // Must also avoid owner if any handlers destroyed the owner.
        if (param.bubbles && me.owner && !me.owner.isDestroyed) {
            return me.owner.trigger(eventName, param);
        }

        // Run internal promises even if external listener returned false
        handlerPromises = handlerPromises?.filter(p => ret !== false || p.$internal);

        // If any handlers were async functions (returned a Promise), then return a Promise
        // which resolves when they all resolve.
        if (handlerPromises?.length) {
            return new Promise(resolve => {
                Promise.all(handlerPromises).then(promiseResults => {
                    const finalResult = !promiseResults.some(result => result === false);

                    resolve(finalResult);
                });
            });
        }

        return ret !== false;
    }

    /**
     * Prevents events from being triggered until {@link #function-resumeEvents()} is called. Optionally queues events that are triggered while
     * suspended. Multiple calls stack to require matching calls to `resumeEvents()` before actually resuming.
     * @param {Boolean} queue Specify true to queue events triggered while suspended
     * @advanced
     */
    suspendEvents(queue = false) {
        const eventsSuspended = this.eventsSuspended || (this.eventsSuspended = { shouldQueue : queue, queue : [], count : 0 });
        eventsSuspended.count++;
    }

    /**
     * Resume event triggering after a call to {@link #function-suspendEvents()}. If any triggered events were queued they will be triggered.
     * @returns {Boolean} `true` if events have been resumed (multiple calls to suspend require an equal number of resume calls to resume).
     * @advanced
     */
    resumeEvents() {
        const suspended = this.eventsSuspended;
        if (suspended) {
            if (--suspended.count === 0) {
                this.eventsSuspended = null;
                if (suspended.shouldQueue) {
                    for (const queued of suspended.queue) {
                        this.trigger(...queued);
                    }
                }
            }
        }

        return !Boolean(this.eventsSuspended);
    }

    //endregion
};
