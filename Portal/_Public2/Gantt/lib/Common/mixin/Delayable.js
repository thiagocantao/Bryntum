import Base from '../Base.js';

/**
 * @module Common/mixin/Delayable
 */

/**
 * Tracks setTimeout, setInterval and requestAnimationFrame calls and clears them on destroy.
 *
 * @example
 * someClass.setTimeout(() => console.log('hi'), 200);
 * someClass.setInterval(() => console.log('annoy'), 100);
 * // can also use named timeouts for easier tracking
 * someClass.setTimeout(() => console.log('named'), 300, 'named');
 * someClass.clearTimeout('named');
 *
 * @private
 * @mixin
 */
export default Target => class Delayable extends (Target || Base) {
    doDestroy() {
        const me = this;

        if (me.timeoutIds) {
            Object.keys(me.timeoutIds).forEach(id => {
                if (typeof me.timeoutIds[id] === 'function') {
                    me.timeoutIds[id]();
                }
                clearTimeout(id);
            });
            me.timeoutIds = null;
        }

        if (me.timeoutMap) {
            Object.values(me.timeoutMap).forEach(id => clearTimeout(id));
            me.timeoutMap = null;
        }

        if (me.intervalIds) {
            Object.keys(me.intervalIds).forEach(id => clearInterval(id));
            me.intervalIds = null;
        }

        if (me.animationFrameIds) {
            Object.keys(me.animationFrameIds).forEach(id => cancelAnimationFrame(id));
            me.animationFrameIds = null;
        }

        super.doDestroy();
    }

    /**
     * Check if a named timeout is active
     * @param name
     */
    hasTimeout(name) {
        return !!(this.timeoutMap && this.timeoutMap[name]);
    }

    /**
     * Same as native setTimeout, but will be cleared automatically on destroy. If a propertyName is supplied it will
     * be used to store the timeout id.
     * @param {Object} timeoutSpec An object containing the details about that function, and the time delay.
     * @param {Function|String} timeoutSpec.fn The function to call, or name of function in this object to call. Used as the `name` parameter if a string.
     * @param {Number} timeoutSpec.delay The milliseconds to delay the call by.
     * @param {String} [timeoutSpec.name] The name under which to register the timer. Defaults to `fn.name`.
     * @param {Boolean} [timeoutSpec.runOnDestroy] Pass `true` if this function should be executed if the Delayable instance is destroyed while function is scheduled.
     * @param {Boolean} [timeoutSpec.cancelOutstanding] Pass `true` to cancel any outstanding invocation of the passed function.
     * @returns {Number}
     */
    setTimeout({ fn, delay, name, runOnDestroy, cancelOutstanding }) {
        if (arguments.length > 1 || typeof arguments[0] === 'function') {
            [fn, delay, name, runOnDestroy] = arguments;
        }
        if (typeof fn === 'string') {
            name = fn;
        }
        else if (!name) {
            name = fn.name;
        }

        if (cancelOutstanding) {
            this.clearTimeout(name);
        }
        const me = this,
            timeoutIds = me.timeoutIds || (me.timeoutIds = {}),
            timeoutMap = me.timeoutMap || (me.timeoutMap = {}),
            timeoutId = setTimeout(() => {
                if (typeof fn === 'string') {
                    fn = me[name];
                }
                fn.call(me);
                if (timeoutIds && timeoutId in timeoutIds) delete timeoutIds[timeoutId];
                if (timeoutMap && name in timeoutMap) delete timeoutMap[name];
            }, delay);

        timeoutIds[timeoutId] = runOnDestroy ? fn : true;

        if (name) {
            timeoutMap[name] = timeoutId;
        }

        return timeoutId;
    }

    /**
     * clearTimeout wrapper, either call with timeout id as normal clearTimeout or with timeout name (if you specified
     * a name to setTimeout())
     * property to null.
     * @param {Number|String} idOrName timeout id or name
     */
    clearTimeout(idOrName) {
        let id = idOrName;

        if (typeof id === 'string') {
            if (this.timeoutMap) {
                id = this.timeoutMap[idOrName];
                delete this.timeoutMap[idOrName];
            }
            else {
                return;
            }
        }

        clearTimeout(id);

        this.timeoutIds && delete this.timeoutIds[id];
    }

    /**
     * clearInterval wrapper
     * @param {Number} id
     */
    clearInterval(id) {
        clearInterval(id);

        this.intervalIds && delete this.intervalIds[id];
    }

    /**
     * Same as native setInterval, but will be cleared automatically on destroy
     * @param fn
     * @param delay
     * @returns {Number}
     */
    setInterval(fn, delay) {
        const me = this,
            intervalId = setInterval(fn, delay);

        if (!me.intervalIds) me.intervalIds = {};
        me.intervalIds[intervalId] = true;

        return intervalId;
    }

    /**
     * Relays to native requestAnimationFrame and adds to tracking to have call automatically canceled on destroy.
     * @param {Function} fn
     * @param {Object[]} [args] The argument list to append to those passed to the function.
     * @param {Object} [thisObj] `this` reference for the function.
     * @returns {Number}
     */
    requestAnimationFrame(fn, extraArgs, thisObj) {
        const handler = extraArgs || thisObj ? () => fn.apply(thisObj, extraArgs) : fn,
            frameId = requestAnimationFrame(handler);

        (this.animationFrameIds || (this.animationFrameIds = {}))[frameId] = true;

        return frameId;
    }

    /**
     * Creates a function which will execute once, on the next animation frame. However many time it is
     * called in one event run, it will only be scheduled to run once.
     * @param {Function|String} fn The function to call, or name of function in this object to call.
     * @param {Object[]} [args] The argument list to append to those passed to the function.
     * @param {Object} [thisObj] `this` reference for the function.
     * @param {Boolean} [cancelOutstanding] Cancel any outstanding queued invocation upon call.
     */
    createOnFrame(fn, extraArgs = [], thisObj = this, cancelOutstanding) {
        let me = this,
            rafId,
            result = (...args) => {
                // Cancel if outstanding if requested
                if (rafId && cancelOutstanding) {
                    me.cancelAnimationFrame(rafId);
                    rafId = null;
                }
                if (!rafId) {
                    rafId = this.requestAnimationFrame(() => {
                        if (typeof fn === 'string') {
                            fn = thisObj[fn];
                        }
                        rafId = null;
                        args.push(...extraArgs);
                        fn.apply(thisObj, args);
                    });
                }
            };

        result.cancel = () => me.cancelAnimationFrame(rafId);

        return result;
    }

    /**
     * Relays to native cancelAnimationFrame and removes from tracking.
     * @param {Number} handle
     */
    cancelAnimationFrame(handle) {
        cancelAnimationFrame(handle);

        this.animationFrameIds && delete this.animationFrameIds[handle];
    }

    /**
     * Wraps a function with another function that delays it specified amount of time, repeated calls to the wrapper
     * resets delay.
     * @param {Function|String} fn Function to buffer, or name of function in this object to call.
     * @param {Number} delay Delay in ms
     * @param {Object} [thisObj] `this` reference for the function.
     * @returns {Function} Wrapped function, call this
     */
    buffer(fn, delay, thisObj = this) {
        let timeoutId = null;

        if (typeof fn === 'string') {
            fn = thisObj[fn];
        }

        const func = (...params) => {
            func.called = false;

            if (timeoutId !== null)  {
                this.clearTimeout(timeoutId);
            }

            timeoutId = this.setTimeout(() => {
                fn.call(thisObj, ...params); // this will be instance of class that we are mixed into.
                func.called = true;
            }, delay);
        };

        return func;
    }

    /**
     * Create a "debounced" function which will call on the "leading edge" of a timer period.
     * When first invoked will call immediately, but invocations after that inside its buffer
     * period will be rejected, and *one* invocation will be made after the buffer period has expired.
     *
     * This is useful for responding immediately to a first mousemove, but from then on, only
     * calling the action function on a regular timer while the mouse continues to move.
     *
     * @param {Function} fn The function to call.
     * @param {Number} buffer The milliseconds to wait after each execution before another execution takes place.
     */
    throttle(fn, buffer) {
        let me = this,
            lastCallTime = 0,
            callArgs,
            timerId,
            result;

        const invoke = () => {
            timerId = 0;
            lastCallTime = performance.now();
            fn.apply(me, callArgs);
            result.called = true;
        };

        result = (...args) => {
            let elapsed = performance.now() - lastCallTime;

            callArgs = args;

            // If it's been more then the buffer period since we invoked, we can call it now
            if (elapsed >= buffer) {
                me.clearTimeout(timerId);
                invoke();
            }
            // Otherwise, kick off a timer for the requested period.
            else if (!timerId) {
                timerId = me.setTimeout(invoke, buffer - elapsed);
                result.called = false;
            }
        };

        result.cancel = () => me.clearTimeout(timerId);

        return result;
    }

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
