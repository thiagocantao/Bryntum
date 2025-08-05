/* eslint-disable bryntum/no-on-in-lib */
import DomHelper from './DomHelper.js';
import ObjectHelper from './ObjectHelper.js';
import FunctionHelper from './FunctionHelper.js';
import BrowserHelper from './BrowserHelper.js';
import Rectangle from './util/Rectangle.js';
import './util/Point.js';
import StringHelper from './StringHelper.js';

/**
 * @module Core/helper/EventHelper
 */

/**
 * @typedef {Object.<String,Function|Boolean|Object|Object[]|Number|String>} ElementListenerConfig
 * @property {HTMLElement} options.element The element to add the listener to.
 * @property {Object} options.thisObj The default `this` reference for all handlers added in this call.
 * @property {Boolean} [options.autoDetach=true] The listeners are automatically removed when the `thisObj` is destroyed.
 * @property {String} [options.delegate] A CSS selector string which only fires the handler when the event takes place in a matching element.
 * @property {Boolean} [options.once] Specify as `true` to have the listener(s) removed upon first invocation.
 * @property {Number} [options.delay] The number of milliseconds to delay the handler call after the event fires:
 * @property {Number|Object} [options.expires] The listener only waits for a specified time before
 * being removed. The value may be a number or an object containing an expiry handler.
 * @property {Number} [options.expires.delay] How long to wait for the event for.
 * @property {String|Function} [options.expires.alt] The function to call when the listener expires
 * **without having been triggered**.
 */

const
    touchProperties = [
        'clientX',
        'clientY',
        'pageX',
        'pageY',
        'screenX',
        'screenY'
    ],
    isOption = {
        element    : 1,
        thisObj    : 1,
        once       : 1,
        delegate   : 1,
        delay      : 1,
        capture    : 1,
        passive    : 1,
        throttled  : 1,
        autoDetach : 1,
        expires    : 1,
        block      : 1
    },
    configurable = true,
    returnTrueProp = {
        configurable,
        value : true
    },
    normalizedKeyNames = {
        Spacebar : 'Space',
        Del      : 'Delete',
        Esc      : 'Escape',
        Left     : 'ArrowLeft',
        Up       : 'ArrowUp',
        Right    : 'ArrowRight',
        Down     : 'ArrowDown'
    },
    // Required to identify a keyup event for special key
    specialKeys = {
        Control : 'ctrl',
        Alt     : 'alt',
        Shift   : 'shift'
    },
    specialKeyRe           = /^(ctrl|shift|alt|meta)$/,
    eventProps             = [
        'altKey',
        'bubbles',
        'button',
        'buttons',
        'cancelBubble',
        'cancelable',
        'clientX',
        'clientY',
        'ctrlKey',
        'layerX',
        'layerY',
        'metaKey',
        'pageX',
        'pageY',
        'returnValue',
        'screenX',
        'screenY',
        'shiftKey'
    ];

/**
 * Utility methods for dealing with Events, normalizing Touch/Pointer/Mouse events.
 */
export default class EventHelper {
    /**
     * DOM event to trigger name mapping.
     * @internal
     */
    static eventNameMap = {
        mousedown   : 'MouseDown',
        mouseup     : 'MouseUp',
        click       : 'Click',
        dblclick    : 'DblClick',
        contextmenu : 'ContextMenu',
        mouseenter  : 'MouseEnter',
        mouseleave  : 'MouseLeave',
        mousemove   : 'MouseMove',
        mouseover   : 'MouseOver',
        mouseout    : 'MouseOut',
        keyup       : 'KeyUp',
        keydown     : 'KeyDown',
        keypress    : 'KeyPress'
    };

    static normalizeEvent(event) {
        return ObjectHelper.copyPropertiesIf(event, event.changedTouches[0] || event.touches[0], touchProperties);
    }

    /**
     * For use when synthesizing events from native DOM events. Copies valid properties from the passed
     * event into the destination object;
     * @param {Object} dest Destination object
     * @param {Event} event The event whose properties to copy
     * @returns {Object} An event construction object.
     * @internal
     */
    static copyEvent(dest, event) {
        return ObjectHelper.copyProperties(dest, event, eventProps);
    }

    /**
     * Returns the `[x, y]` coordinates of the event in the viewport coordinate system.
     * @param {Event} event The event
     * @returns {Number[]} The coordinate.
     */
    static getXY(event) {
        if (event.touches) {
            event = event.touches[0];
        }
        return [event.clientX, event.clientY];
    }

    /**
     * Returns the pixel distance between two mouse/touch/pointer events.
     * @param {Event} event1 The first event.
     * @param {Event} event2 The second event.
     * @returns {Number} The distance in pixels between the two events.
     */
    static getDistanceBetween(event1, event2) {
        const
            xy1 = EH.getXY(event1),
            xy2 = EH.getXY(event2);

        // No point in moving this to Point. We are dealing only with number values here.
        return Math.sqrt(Math.pow(xy1[0] - xy2[0], 2) + Math.pow(xy1[1] - xy2[1], 2));
    }

    /**
     * Returns a {@link Core.helper.util.Point} which encapsulates the `pageX/Y` position of the event.
     * May be used in {@link Core.helper.util.Rectangle} events.
     * @param {Event} event A browser mouse/touch/pointer event.
     * @returns {Core.helper.util.Point} The page point.
     */
    static getPagePoint(event) {
        return new Rectangle.Point(event.pageX, event.pageY);
    }

    /**
     * Returns a {@link Core.helper.util.Point} which encapsulates the `clientX/Y` position of the event.
     * May be used in {@link Core.helper.util.Rectangle} events.
     * @param {Event} event A browser mouse/touch/pointer event.
     * @returns {Core.helper.util.Point} The page point.
     */
    static getClientPoint(event) {
        return new Rectangle.Point(event.clientX, event.clientY);
    }

    /**
     * Add a listener or listeners to an element
     * The `options` parameter allows supplying options for the listener(s), for available options see {@link #typedef-ElementListenerConfig}.
     *
     * @param {HTMLElement} element The element to add a listener/listeners to.
     * @param {String|Object} eventName Either a string, being the name of the event to listen for,
     * or an options object containing event names and options as keys. See the options parameter
     * for details, or the {@link #function-on-static} method for details.
     * @param {Function} [handler] If the second parameter is a string event name, this is the handler function.
     * @param {ElementListenerConfig} [options] If the second parameter is a string event name, this is the options.
     * @returns {Function} A detacher function which removes all the listeners when called.
     */
    static addListener(element, eventName, handler, options) {
        if (element.nodeType) {
            // All separate params, element, eventName and handler
            if (typeof eventName === 'string') {
                options = Object.assign({
                    element,
                    [eventName] : handler
                }, options);
            }
            // element, options
            else {
                options = Object.assign({
                    element
                }, eventName);
            }
        }
        // Just an options object passed
        else {
            options = element;
        }
        return EH.on(options);
    }

    /**
     * Adds a listener or listeners to an element.
     * all property names other than the options listed below are taken to be event names,
     * and the values as handler specs.
     *
     * A handler spec is usually a function reference or the name of a function in the `thisObj`
     * option.
     *
     * But a handler spec may also be an options object containing a `handler` property which is
     * the function or function name, and local options, including `element` and `thisObj`
     * which override the top level options.
     *
     * The `options` parameter allows supplying options for the listener(s), for available options see {@link #typedef-ElementListenerConfig}.
     *
     *  Usage example
     *
     * ```javascript
     * construct(config) {
     *     super.construct(config);
     *
     *     // Add auto detaching event handlers to this Widget's reference elements
     *     EventHelper.on({
     *         element : this.iconElement,
     *         click   : '_handleIconClick',
     *         thisObj : this,
     *         contextmenu : {
     *             element : document,
     *             handler : '_handleDocumentContextMenu'
     *         }
     *     });
     * }
     *```
     *
     * The `click` handler on the `iconElement` calls `this._handleIconClick`.
     *
     * The `contextmenu` handler is added to the `document` element, but the `thisObj`
     * is defaulted in from the top `options` and calls `this._handleDocumentContextMenu`.
     *
     * Note that on touch devices, `dblclick` and `contextmenu` events are synthesized.
     * Synthesized events contain a `browserEvent` property containing the final triggering
     * event of the gesture. For example a synthesized `dblclick` event would contain a
     * `browserEvent` property which is the last `touchend` event. A synthetic `contextmenu`
     * event will contain a `browserEvent` property which the longstanding `touchstart` event.
     *
     * @param {ElementListenerConfig} options The full listener specification.
     * @returns {Function} A detacher function which removes all the listeners when called.
     */
    static on(options) {
        const
            element        = options.element,
            thisObj        = options.thisObj,
            handlerDetails = [];

        for (const eventName in options) {
            // Only treat it as an event name if it's not a supported option
            if (!isOption[eventName]) {
                let handlerSpec = options[eventName];
                if (typeof handlerSpec !== 'object') {
                    handlerSpec = {
                        handler : handlerSpec
                    };
                }
                const targetElement = handlerSpec.element || element;

                // Keep track of the real handlers added.
                // addElementLister returns [ element, eventName, addedFunction, capture ]
                handlerDetails.push(EH.addElementListener(targetElement, eventName, handlerSpec, options));
            }
        }

        const detacher = () => {
            for (let handlerSpec, i = 0; i < handlerDetails.length; i++) {
                handlerSpec = handlerDetails[i];
                EH.removeEventListener(handlerSpec[0], handlerSpec[1], handlerSpec[2]);
            }
            handlerDetails.length = 0;
        };

        // { autoDetach : true, thisObj : scheduler } means remove all listeners when the scheduler dies.
        if (thisObj && options.autoDetach !== false) {
            thisObj.doDestroy = FunctionHelper.createInterceptor(thisObj.doDestroy, detacher, thisObj);
        }

        return detacher;
    }

    /**
     * Used internally to add a single event handler to an element.
     * @param {HTMLElement} element The element to add the handler to.
     * @param {String} eventName The name of the event to add a handler for.
     * @param {Function|String|Object} handlerSpec Either a function to call, or
     * the name of a function to call in the `thisObj`, or an object containing
     * the handler local options.
     * @param {Function|String} [handlerSpec.handler] Either a function to call, or
     * the name of a function to call in the `thisObj`.
     * @param {HTMLElement} [handlerSpec.element] Optionally a local element for the listener.
     * @param {Object} [handlerSpec.thisObj] A local `this` specification for the handler.
     * @param {Object} defaults The `options` parameter from the {@link #function-addListener-static} call.
     * @private
     */
    static addElementListener(element, eventName, handlerSpec, defaults) {
        const
            handler  = EH.createHandler(element, eventName, handlerSpec, defaults),
            { spec } = handler,
            expires  = handlerSpec.expires || defaults.expires,
            options  = spec.capture != null || spec.passive != null ? {
                capture : spec.capture,
                passive : spec.passive
            } : undefined;

        element.addEventListener(eventName, handler, options);

        if (expires) {
            // Extract expires : { delay : 100, alt : 'onExpireFn' }
            const
                thisObj   = handlerSpec.thisObj || defaults.thisObj,
                delayable = thisObj?.isDelayable ? thisObj : globalThis,
                { alt }   = expires,
                delay     = alt ? expires.delay : expires,
                { spec }  = handler;

            // expires is not applied with other options in createHandler(), store it here
            spec.expires = expires;

            spec.timerId = delayable[typeof delay === 'number' ? 'setTimeout' : 'requestAnimationFrame'](() => {
                spec.timerId = null;

                EH.removeEventListener(element, eventName, handler);

                // If we make it here and the handler has not been called, invoke the alt handler
                if (alt && !handler.called) {
                    (typeof alt === 'string' ? thisObj[alt] : alt).call(thisObj);
                }
            }, delay, `listener-timer-${performance.now()}`);
        }

        return [element, eventName, handler, options];
    }

    // composedPath throws in salesforce
    // https://github.com/bryntum/support/issues/4432
    static getComposedPathTarget(event) {
        return event.composedPath()[0] || event.path[0];
    }

    static fixEvent(event) {
        if (event.fixed) {
            return event;
        }

        const { type, target } = event;

        // When we listen to event on document and get event which bubbled from shadow dom, reading its target would
        // return shadow root element, or null if accessed in an async timeframe.
        // We need actual element which started the event
        if ((target?.shadowRoot || target?.getRootNode?.()?.host) && event.composedPath) {
            const
                targetElement  = this.getComposedPathTarget(event),
                originalTarget = target;

            // Can there be an event which actually originated from custom element, not its shadow dom?
            Object.defineProperty(event, 'target', {
                value : targetElement,
                configurable
            });

            // Save original target just in case
            Object.defineProperty(event, 'originalTarget', {
                value : originalTarget,
                configurable
            });
        }

        // Flag that we have fixed this event
        Object.defineProperty(event, 'fixed', returnTrueProp);

        // Normalize key names
        if (type.startsWith('key')) {
            const normalizedKeyName = normalizedKeyNames[event.key];
            if (normalizedKeyName) {
                Object.defineProperty(event, 'key', {
                    value : normalizedKeyName,
                    configurable
                });
            }

            // Polyfill the code property for SPACE because it is not set for synthetic events.
            if (event.key === ' ' && !event.code) {
                Object.defineProperty(event, 'code', {
                    value : 'Space',
                    configurable
                });
            }
        }

        // Sync OSX's meta key with the ctrl key. This will only happen on Mac platform.
        // It's read-only, so define a local property to return true for ctrlKey.
        if (event.metaKey && !event.ctrlKey) {
            Object.defineProperty(event, 'ctrlKey', returnTrueProp);
        }

        // if (isRTL && (type.startsWith('mouse') || type.startsWith('pointer') || type === 'click')) {
        //     event.nativePageX = event.pageX;
        //
        //     if (!Object.getOwnPropertyDescriptor(event, 'pageX')) {
        //         Object.defineProperties(event, {
        //             pageX : {
        //                 get : () => {
        //                     return document.body.offsetWidth - event.nativePageX;
        //                 }
        //             }
        //         });
        //     }
        // }

        // offsetX/Y are within padding box. Border is outside padding box, so -ve values are possible
        // which are not useful for calculating intra-element positions.
        // We add borderOffsetX and borderOffsetY properties which are offsets within the border box.
        // Tested in EventHelper.js
        if (target && 'offsetX' in event) {
            // Wrap calculating `borderOffsetX/Y` until this property is actually accessed in the code to avoid forced reflow.
            if (!Object.getOwnPropertyDescriptor(event, 'borderOffsetX')) {
                Object.defineProperty(event, 'borderOffsetX', {
                    get : () => {
                        return event.offsetX + (BrowserHelper.isSafari ? 0 : parseInt(target.ownerDocument.defaultView.getComputedStyle(target).getPropertyValue('border-left-width')));
                    }
                });
            }
            if (!Object.getOwnPropertyDescriptor(event, 'borderOffsetY')) {
                Object.defineProperty(event, 'borderOffsetY', {
                    get : () => {
                        return event.offsetY + (BrowserHelper.isSafari ? 0 : parseInt(target.ownerDocument.defaultView.getComputedStyle(target).getPropertyValue('border-top-width')));
                    }
                });
            }
        }

        // Firefox has a bug where it can report that the target is the #document when mouse is over a pseudo element
        if (target?.nodeType === Element.DOCUMENT_NODE && 'clientX' in event) {
            const targetElement = DomHelper.elementFromPoint(event.clientX, event.clientY);
            Object.defineProperty(event, 'target', {
                value : targetElement,
                configurable
            });
        }

        // Firefox has a bug where it can report a textNode as an event target/relatedTarget.
        // We standardize this to report the parentElement.
        if (target?.nodeType === Element.TEXT_NODE) {
            const targetElement = event.target.parentElement;
            Object.defineProperty(event, 'target', {
                value : targetElement,
                configurable
            });
        }
        if (event.relatedTarget?.nodeType === Element.TEXT_NODE) {
            const relatedTargetElement = event.target.parentElement;
            Object.defineProperty(event, 'relatedTarget', {
                value : relatedTargetElement,
                configurable
            });
        }

        // If it's a touch event, move the positional details
        // of touches[0] up to the event.
        if (type.startsWith('touch') && event.touches.length) {
            this.normalizeEvent(event);
        }

        return event;
    }

    static createHandler(element, eventName, handlerSpec, defaults) {
        const
            delay                   = handlerSpec.delay || defaults.delay,
            throttled               = handlerSpec.throttled || defaults.throttled,
            block                   = handlerSpec.block || defaults.block,
            once                    = ('once'     in handlerSpec) ? handlerSpec.once     : defaults.once,
            capture                 = ('capture'  in handlerSpec) ? handlerSpec.capture  : defaults.capture,
            passive                 = ('passive'  in handlerSpec) ? handlerSpec.passive  : defaults.passive,
            delegate                = ('delegate' in handlerSpec) ? handlerSpec.delegate : defaults.delegate,
            wrappedFn               = handlerSpec.handler,
            expires                 = handlerSpec.expires,
            thisObj                 = handlerSpec.thisObj || defaults.thisObj,
            { rtlSource = thisObj } = thisObj || {};

        //Capture initial conditions in case of destruction of thisObj.
        // Destruction completely wipes the object.


        // Innermost level of wrapping which calls the user's handler.
        // Normalize the event cross-browser, and attempt to normalize touch events.
        let handler = (event, ...args) => {
            // When playing a demo using DemoBot, only handle synthetic events
            if (EH.playingDemo && event.isTrusted) {
                return;
            }

            // If the thisObj is already destroyed, we cannot call the function.
            // If in dev mode, warn the developer with a JS error.



            if (thisObj?.isDestroyed) {
                return;
            }

            // Fix up events to handle various browser inconsistencies
            event = EH.fixEvent(event, rtlSource?.rtl);

            // Flag for the expiry timer
            handler.called = true;

            (typeof wrappedFn === 'string' ? thisObj[wrappedFn] : wrappedFn).call(thisObj, event, ...args);

            // Remove properties that our fixEvent method added.
            // Other applications to which this may bubble need the pure browser event.
            delete event.target;
            delete event.relatedTarget;
            delete event.originalarget;
            delete event.key;
            delete event.code;
            delete event.ctrlKey;
            delete event.fixed;
        };

        // Allow events to be blocked for a certain time
        if (block) {
            const wrappedFn = handler;
            let lastCallTime, lastTarget;

            handler = (e, ...args) => {
                const now = performance.now();
                if (!lastCallTime || e.target !== lastTarget || now - lastCallTime > block) {
                    lastTarget = e.target;
                    lastCallTime = now;
                    wrappedFn(e, ...args);
                }
            };
        }

        // Go through options, each creates a new handler by wrapping the previous handler to implement the options.
        // Right now, we have delay. Note that it may be zero, so test != null
        if (delay != null) {
            const
                wrappedFn = handler,
                delayable = thisObj?.setTimeout ? thisObj : globalThis;

            handler = (...args) => {
                delayable.setTimeout(() => {
                    wrappedFn(...args);
                }, delay);
            };
        }

        // If they specified the throttled option, wrap the handler in a createdThrottled
        // version. Allow the called to specify an alt function to call when the event
        // fires before the buffer time has expired.
        if (throttled != null) {
            let alt, buffer = throttled;

            if (throttled.buffer) {
                alt = e => {
                    return throttled.alt.call(EH, EH.fixEvent(e, rtlSource?.rtl));
                };
                buffer = throttled.buffer;
            }

            if (thisObj?.isDelayable) {
                handler = thisObj.throttle(handler, {
                    delay     : buffer,
                    throttled : alt
                });
            }
            else {
                handler = FunctionHelper.createThrottled(handler, buffer, thisObj, null, alt);
            }
        }

        // This must always be added late to be processed before delay so that the handler is removed immediately.
        // Note that we cant use native once because of our support for `delegate`, it would remove the listener even
        // when delegate does not match
        if (once) {
            const wrappedFn = handler;
            handler = (...args) => {
                EH.removeEventListener(element, eventName, handler);
                wrappedFn(...args);
            };
        }

        // This must be added last to be called first, once and delay should not act on wrong targets when configured
        // with a delegate
        if (delegate) {
            const wrappedFn = handler;
            handler = (event, ...args) => {
                event = EH.fixEvent(event, rtlSource?.rtl);

                // delegate: '.b-field-trigger' only fires when click is in a matching el.
                // currentTarget becomes the delegate.

                // Maintainer: In Edge event.target can be an empty object for transitionend events
                const delegatedTarget = event.target.closest?.call && event.target.closest(delegate);
                if (!delegatedTarget) {
                    return;
                }

                // Allow this to be redefined as it bubbles through listeners up the parentNode axis
                // which might have their own delegate settings.
                Object.defineProperty(event, 'currentTarget', {
                    get          : () => delegatedTarget,
                    configurable : true
                });

                wrappedFn(event, ...args);
            };
        }

        // Only autoDetach here if there's a local thisObj is in the handlerSpec for this one listener.
        // If it's in the defaults, then the "on" method will handle it.
        if (handlerSpec.thisObj && handlerSpec.autoDetach !== false) {
            thisObj.doDestroy = FunctionHelper.createInterceptor(thisObj.doDestroy, () => EH.removeEventListener(element, eventName, handler), thisObj);
        }

        handler.spec = {
            delay,
            throttled,
            block,
            once,
            thisObj,
            capture,
            expires,
            passive,
            delegate
        };

        return handler;
    }

    static removeEventListener(element, eventName, handler) {
        const { expires, timerId, thisObj, capture } = handler.spec;

        // Cancel outstanding expires.alt() call when removing the listener
        if (expires?.alt && timerId) {
            const delayable = thisObj?.isDelayable ? thisObj : globalThis;
            delayable[typeof expires.delay === 'number' ? 'clearTimeout' : 'cancelAnimationFrame'](timerId);
        }

        element.removeEventListener(eventName, handler, capture);
    }

    /**
     * Calls a callback when the described animation completes.
     *
     * @param {Object} detail
     * @param {HTMLElement} detail.element The element which is being animated.
     * @param {String|RegExp} [detail.animationName] The name of the animation to wait for.
     * @param {String} [detail.property] If no `animationName` specified, the CSS property
     * which is being animated.
     * @param {Function} detail.handler The function to call on animation end.
     * @param {Number} [detail.duration] Optional fallback time to wait until calling the callback.
     * @param {Object} [detail.thisObj] The `this` reference to call the callback with.
     * @param {Array} [detail.args] Optional arguments to call the callback with.
     * @param {Core.mixin.Delayable} [detail.timerSource] A Delayable to provide the fallback timeout.
     * @param {Boolean} [detail.runOnDestroy] If `timerSource` is a {@link Core.mixin.Delayable},
     * `true` to invoke the callback if it is destroyed during the animation.
     * @returns {Function} a function which detaches the animation end listener.
     */
    static onTransitionEnd({
        element,
        animationName,
        property,
        handler,
        mode     = animationName ? 'animation' : 'transition',
        duration = DomHelper[`get${mode === 'transition' ? 'Property' : ''}${StringHelper.capitalize(mode)}Duration`](element, property),
        thisObj  = globalThis,
        args     = [],
        timerSource,
        runOnDestroy
    }) {
        let timerId;

        timerSource = timerSource || (thisObj.isDelayable ? thisObj : globalThis);

        const
            callbackArgs = [element, property, ...args],
            doCallback  = () => {
                detacher();
                if (!thisObj.isDestroyed) {
                    if (thisObj.callback) {
                        thisObj.callback(handler, thisObj, callbackArgs);
                    }
                    else {
                        handler.apply(thisObj, callbackArgs);
                    }
                }
            },
            detacher    = EH.on({
                element,
                [`${mode}end`]({ animationName : endedAnimation,  propertyName, target }) {
                    if (target === element) {
                        if (propertyName === property || endedAnimation?.match(animationName)) {
                            if (timerId) {
                                timerSource.clearTimeout?.(timerId);
                                timerId = null;
                            }

                            doCallback();
                        }
                    }
                }
            });

        // If the transition has not signalled its end within duration + 50 milliseconds
        // then give up on it. Remove the listener and call the handler.
        if (duration != null) {
            timerId = timerSource.setTimeout(doCallback, duration + 50, 'onTransitionEnd', runOnDestroy);
        }

        return detacher;
    }

    /**
     * Waits for the described animation completes.
     *
     * @param {Object} config
     * @param {HTMLElement} config.element The element which is being animated.
     * @param {String|RegExp} [config.animationName] The name of the animation to wait for.
     * @param {String} [config.property] If no `animationName` specified, the CSS property
     * which is being animated.
     * @param {Number} [config.duration] Optional fallback time to wait until calling the callback.
     * @param {Core.mixin.Delayable} [config.timerSource] A Delayable to provide the fallback timeout.
     * @param {Boolean} [config.runOnDestroy] If `timerSource` is a {@link Core.mixin.Delayable},
     * `true` to invoke the callback if it is destroyed during the animation.
     * @async
     */
    static async waitForTransitionEnd(config) {
        return new Promise(resolve => {
            config.handler = resolve;
            EventHelper.onTransitionEnd(config);
        });
    }

    /**
     * Private function to wrap the passed function. The returned wrapper function to be used as
     * a `touchend` handler which will call the passed function passing a fabricated `dblclick`
     * event if there is a `click` within 300ms.
     * @param {Element} element element
     * @param {String|Function} handler The handler to call.
     * @param {Object} thisObj The owner of the function.
     * @private
     */
    static createDblClickWrapper(element, handler, thisObj) {
        let startId, secondListenerDetacher, tapholdTimer;

        return () => {
            if (!secondListenerDetacher) {
                secondListenerDetacher = EH.on({
                    element,

                    // We only get here if a touchstart arrives within 300ms of a click
                    touchstart : secondStart => {
                        startId = secondStart.changedTouches[0].identifier;
                        // Prevent zoom
                        secondStart.preventDefault();
                    },
                    touchend : secondClick => {
                        if (secondClick.changedTouches[0].identifier === startId) {
                            secondClick.preventDefault();

                            clearTimeout(tapholdTimer);
                            startId = secondListenerDetacher = null;

                            const
                                targetRect          = Rectangle.from(secondClick.changedTouches[0].target, null, true),
                                offsetX             = secondClick.changedTouches[0].pageX - targetRect.x,
                                offsetY             = secondClick.changedTouches[0].pageY - targetRect.y,
                                dblclickEventConfig = Object.assign({
                                    browserEvent : secondClick
                                }, secondClick),
                                dblclickEvent       = new MouseEvent('dblclick', dblclickEventConfig);

                            Object.defineProperties(dblclickEvent, {
                                target  : { value : secondClick.target },
                                offsetX : { value : offsetX },
                                offsetY : { value : offsetY }
                            });

                            if (typeof handler === 'string') {
                                handler = thisObj[handler];
                            }

                            // Call the wrapped handler passing the fabricated dblclick event
                            handler.call(thisObj, dblclickEvent);
                        }
                    },
                    once : true
                });

                // Cancel the second listener is there's no second click within <dblClickTime> milliseconds.
                tapholdTimer = setTimeout(() => {
                    secondListenerDetacher();
                    startId = secondListenerDetacher = null;
                }, EH.dblClickTime);
            }
        };
    }

    /**
     * Handles various inputs to figure out the name of the special key of the event.
     *
     * ```javascript
     * EventHelper.toSpecialKey('ctrl') // 'ctrlKey'
     * EventHelper.toSpecialKey(true)   // 'ctrlKey', default for PC (Cmd for Mac)
     * EventHelper.toSpecialKey(false)  // false
     * EventHelper.toSpecialKey('foo')  // false
     * ```
     *
     * @param {*} value User input value to process.
     * @param {String} defaultValue Default value to fall back to if `true` value is passed.
     * @returns {Boolean|String} Returns `false` if provided value cannot be converted to special key and special key
     * name otherwise.
     * @internal
     */
    static toSpecialKey(value, defaultValue = BrowserHelper.isMac ? 'metaKey' : 'ctrlKey') {
        let result = false;

        if (value === true) {
            result = defaultValue;
        }
        else if (typeof value === 'string') {
            value = value.toLowerCase();

            if (value.match(specialKeyRe)) {
                result = `${value}Key`;
            }
        }

        return result;
    }

    /**
     * If keyup event is triggered when special key is pressed, we don't get special key value from properties like
     * `ctrlKey`. Instead we need to read `event.key`. That property uses full name and we use abbreviations, so we
     * need to convert the key.
     * @param {String} code
     * @returns {String}
     * @internal
     */
    static specialKeyFromEventKey(code) {
        return specialKeys[code] || 'no-special-key';
    }
}

const EH = EventHelper;

/**
 * The time in milliseconds for a `taphold` gesture to trigger a `contextmenu` event.
 * @member {Number} [longPressTime=700]
 * @readonly
 * @static
 */
EH.longPressTime = 700;

/**
 * The time in milliseconds within which a second touch tap event triggers a `dblclick` event.
 * @member {Number} [dblClickTime=300]
 * @readonly
 * @static
 */
EH.dblClickTime = 300;

// When dragging on a touch device, we need to prevent scrolling from happening.
// Dragging only starts on a touchmove event, by which time it's too late to preventDefault
// on the touchstart event which started it.
// To do this we need a capturing, non-passive touchmove listener at the document level so we can preventDefault.
// This is in lieu of a functioning touch-action style on iOS Safari. When that's fixed, this will not be needed.
if (BrowserHelper.isTouchDevice) {
    EH.on({
        element   : document,
        touchmove : event => {
            // If we're touching a b-dragging event, then stop any panning by preventing default.
            if (event.target.closest('.b-dragging')) {
                event.preventDefault();
            }
        },
        passive : false,
        capture : true
    });
}
