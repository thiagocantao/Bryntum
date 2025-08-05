import StringHelper from '../StringHelper.js';
import DomHelper from '../DomHelper.js';

/**
 * @module Core/helper/util/Fullscreen
 */

/**
 * Encapsulates the functionality related to switching cross-browser to full screen view and back.
 */
export default class Fullscreen {
    static init() {
        const fnNames  = ['fullscreenEnabled', 'requestFullscreen', 'exitFullscreen', 'fullscreenElement'],
            // turns fnNames into function calls to prefixed functions, fullscreenEnabled -> document.mozFullscreenEnabled
            prefixFn = prefix => fnNames.map(fn => {
                let result = prefix + StringHelper.capitalize(fn);

                // fullscreenEnabled in Firefox is called fullScreenEnabled
                if (prefix === 'moz') {
                    result = result.replace('screen', 'Screen');

                    // #6555 - Crash when clicking full screen button twice
                    // firefox doesn't support exitFullScreen method
                    if ('mozCancelFullScreen' in document && fn === 'exitFullscreen') {
                        result = 'mozCancelFullScreen';
                    }
                }

                return result;
            });

        this.functions = (
            ('fullscreenEnabled' in document && fnNames) ||
            ('webkitFullscreenEnabled' in document && prefixFn('webkit')) ||
            ('mozFullScreenEnabled' in document && prefixFn('moz')) ||
            ('msFullscreenEnabled' in document && prefixFn('ms')) ||
            []
        );

        const eventNames   = [
                'fullscreenchange',
                'fullscreenerror'
            ],
            msEventNames = [
                'MSFullscreenChange',
                'MSFullscreenError'
            ],
            prefixEvt    = prefix => eventNames.map(eventName => prefix + StringHelper.capitalize(eventName));

        this.events = (
            ('fullscreenEnabled' in document && eventNames) ||
            ('webkitFullscreenEnabled' in document && prefixEvt('webkit')) ||
            ('mozFullscreenEnabled' in document && prefixEvt('moz')) ||
            ('msFullscreenEnabled' in document && msEventNames) ||
            []
        );

        this.onFullscreenChange(this.onInternalFullscreenChange.bind(this));
    }

    /**
     * True if the fullscreen mode is supported and enabled, false otherwise
     * @property {Boolean}
     */
    static get enabled() {
        return Boolean(this.functions[0] && document[this.functions[0]]);
    }

    /**
     * Request entering the fullscreen mode.
     * @param {HTMLElement} element Element to be displayed fullscreen
     * @returns {Promise} A promise which is resolved with a value of undefined when the transition to full screen is complete.
     */
    static async request(element) {
        return this.functions[1] && element?.[this.functions[1]]();
    }

    /**
     * Exit the previously entered fullscreen mode.
     * @returns {Promise} A promise which is resolved once the user agent has finished exiting full-screen mode
     */
    static async exit() {
        return this.functions[2] && document[this.functions[2]]();
    }

    /**
     * True if fullscreen mode is currently active, false otherwise
     * @property {Boolean}
     */
    static get isFullscreen() {
        return !!this.element;
    }

    static get element() {
        return this.functions[3] && document[this.functions[3]];
    }

    /**
     * Installs the passed listener to fullscreenchange event
     * @param {Function} fn The listener to install
     */
    static onFullscreenChange(fn) {
        if (this.events[0]) {
            document.addEventListener(this.events[0], fn);
        }
    }

    /**
     * Uninstalls the passed listener from fullscreenchange event
     * @param {Function} fn
     */
    static unFullscreenChange(fn) {
        if (this.events[0]) {
            document.removeEventListener(this.events[0], fn);
        }
    }

    // Make sure the floatRoot is added to any element going fullscreen
    static onInternalFullscreenChange() {
        const
            me                              = this,
            { element : fullscreenElement } = me;

        if (fullscreenElement) {
            if (!fullscreenElement.closest('.b-floatroot')) {
                const
                    rootElement   = DomHelper.getRootElement(fullscreenElement),
                    { floatRoot } = rootElement;

                if (floatRoot) {
                    me._floatRoot = floatRoot;
                    me._oldParent = floatRoot.parentElement;
                    fullscreenElement.appendChild(floatRoot);
                }
            }
        }
        else {
            if (me._floatRoot) {
                me._oldParent.appendChild(me._floatRoot);
            }
            me._oldParent = null;
            me._floatRoot = null;
        }
    }
}

Fullscreen.init();
