import ArrayHelper from '../helper/ArrayHelper.js';
import DomHelper from '../helper/DomHelper.js';
import Promissory from '../helper/util/Promissory.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import VersionHelper from '../helper/VersionHelper.js';
import Renderable from './Renderable.js';


/**
 * @module Core/widget/Mask
 */

/**
 * Masks a target element (document.body if none is specified). Call static methods for ease of use or make instance
 * for reusability.
 *
 * ```javascript
 * Mask.mask('hello');
 * Mask.unmask();
 * ````
 *
 * {@inlineexample Core/widget/Mask.js}
 *
 * Can show progress:
 *
 * ```javascript
 * // Using progress by calling static method
 * const mask = Mask.mask({
 *   text        :'The task is in progress',
 *   progress    : 0,
 *   maxProgress : 100
 * });
 *
 * let timer = setInterval(()=>{
 *   mask.progress += 5;
 *   if(mask.progress >= mask.maxProgress) {
 *     Mask.unmask();
 *     clearInterval(timer)
 *   }
 * }, 100);
 * ```
 *
 * Shortcut to masking Bryntum components:
 *
 * ```javascript
 * // Using progress to mask a Bryntum component
 * scheduler.mask({
 *  text:'Loading in progress',
 *   progress: 0,
 *   maxProgress: 100
 * })
 * let timer = setInterval(()=>{
 *   scheduler.masked.progress += 5;
 *   if(scheduler.masked.progress >= scheduler.masked.maxProgress) {
 *     scheduler.unmask();
 *     clearInterval(timer)
 *   }
 * },100)
 * ```
 */
export default class Mask extends Renderable {
    //region Config
    static $name = 'Mask';

    // Factoryable type name
    static type = 'mask';

    static configurable = {
        /**
         * Set this config to trigger an automatic close after the desired delay:
         * ```javascript
         *  mask.autoClose = 2000;
         * ```
         * If the mask has an `owner`, its `onMaskAutoClosing` method is called when the close starts and its
         * `onMaskAutoClose` method is called when the close finishes.
         * @config {Number}
         * @private
         */
        autoClose : null,

        /**
         * The portion of the {@link #config-target} element to be covered by this mask. By default, the mask fully
         * covers the `target`. In some cases, however, it may be desired to only cover the `'body'` (for example,
         * in a grid).
         *
         * This config is set in conjunction with `owner` which implements the method `syncMaskCover`.
         *
         * @config {String}
         * @private
         */
        cover : null,

        /**
         * The icon to show next to the text. Defaults to showing a spinner
         * @config {String}
         * @default
         */
        icon : 'b-icon b-icon-spinner',

        errorDefaults : {
            icon      : 'b-icon b-icon-warning',
            autoClose : 3000,
            showDelay : 0
        },

        /**
         * The maximum value of the progress indicator
         * @property {Number}
         */
        maxProgress : null,

        /**
         * Mode: bright, bright-blur, dark or dark-blur
         * @config {'bright'|'bright-blur'|'dark'|'dark-blur'}
         * @default
         */
        mode : 'dark',

        /**
         * Number expressing the progress
         * @property {Number}
         */
        progress : null,

        // The owner is involved in the following features:
        //
        // - The `autoClose` timer calls `onMaskAutoClose`.
        // - The `cover` config calls `syncMaskCover`.
        // - If the `target` is a string, that string names the property of the `owner` that holds the
        //   `HTMLElement` reference.
        /**
         * The owning widget of this mask. This is required if `target` is a string.
         *
         * @config {Core.widget.Widget}
         */
        owner : {
            $config : 'nullify',
            value   : null
        },

        /**
         * The element to be masked. If this config is a string, that string is the name of the property of the
         * `owner` that holds the `HTMLElement` that is the actual target of the mask.
         *
         * NOTE: In prior releases, this used to be specified as the `element` config, but that is now, as with
         * `Widget`, the primary element of the mask.
         *
         * @config {String|HTMLElement}
         */
        target : null,

        /**
         * The text (or HTML) to show in mask
         * @config {String}
         */
        text : null,


        type : null,

        /**
         * The number of milliseconds to delay before making the mask visible. If set, the mask will have an
         * initial `opacity` of 0 but will function in all other ways as a normal mask. Setting this delay can
         * reduce flicker in cases where load operations are typically short (for example, a second or less).
         *
         * @config {Number}
         */
        showDelay : null,

        useTransition : false
    };

    static delayable = {
        deferredClose : 0,
        delayedShow   : 0,
        syncCover     : {
            type  : 'throttle',
            delay : 100
        }
    };

    //endregion

    //region Init

    // Used to give masks unique names
    static counter = 0;
    // Tracks open masks
    static masks = [];

    construct(config) {
        if (config) {
            let el = config.element,
                cfg;

            // Upgrade config -> cfg
            // Treat config as readonly, cfg is lazily copied and writable
            if (el) {
                VersionHelper.deprecate('Core', '4.0.0', 'Mask "element" config has been renamed to "target"');

                config = cfg = Object.assign({}, config);

                delete cfg.element;
                cfg.target = el;
            }

            el = config.target;

            if (typeof el === 'string') {
                config = cfg = cfg || Object.assign({}, config);

                cfg.target = config.owner[el];  // must supply "owner" in this case
            }
        }

        super.construct(config);

        const
            me       = this,
            { type } = me;

        if (!me.target) {
            me.target = document.body;
        }

        me.maskName = `mask${typeof type === 'string' ? type.trim() : ''}-${Mask.counter++}`;

        me.show();
    }

    doDestroy() {
        const
            me          = this,
            { element } = me;


        if (me.type === 'trial') {
            return false;
        }

        if (element) {
            me.element = null;

            if (me.mode.endsWith('blur')) {
                DomHelper.forEachChild(element, child => {
                    child.classList.remove(`b-masked-${me.mode}`);
                });
            }

            me.target.classList.remove('b-masked');
            me.target[me.maskName] = null;
            ArrayHelper.remove(Mask.masks, me);
        }

        super.doDestroy();
    }

    get maskElement() {

        return this.element;
    }

    set error(value) {
        this.setConfig(this.errorDefaults);
        this.text = value;
    }

    renderDom() {
        const
            me              = this,
            { maxProgress } = me;

        return {
            class : {
                'b-mask'                : 1,
                'b-delayed-show'        : me.showDelay,
                'b-widget'              : 1,
                [`b-mask-${me.mode}`]   : 1,
                'b-progress'            : maxProgress,
                'b-prevent-transitions' : !me.useTransition
            },
            children : [{
                reference : 'maskContent',
                class     : 'b-mask-content b-drawable',
                children  : [
                    maxProgress ? {
                        reference : 'progressElement',
                        class     : 'b-mask-progress-bar',
                        style     : {
                            width : `${Math.max(0, Math.min(100, Math.round(me.progress / maxProgress * 100)))}%`
                        }
                    } : null,
                    {
                        reference : 'maskText',
                        class     : 'b-mask-text',
                        html      : (me.icon ? `<i class="b-mask-icon ${me.icon}"></i>` : '') + me.text
                    }
                ]
            }]
        };
    }

    //endregion

    //region Static

    static mergeConfigs(...sources) {
        const ret = {};

        for (const src of sources) {
            if (typeof src === 'string') {
                ret.text = src;
            }
            else {
                ObjectHelper.assign(ret, src); // not Object.assign!
            }
        }

        return ret;
    }

    /**
     * Shows a mask with the specified message.
     *
     * Masks stack, call {@link #function-unmask-static} to remove the topmost mask. Or call {@link #function-close}
     * on the returned mask to close it specifically.
     *
     * @param {String|MaskConfig} text Message
     * @param {HTMLElement} target The element to mask
     * @returns {Core.widget.Mask}
     */
    static mask(text, target = document.body) {
        return Mask.new({ target }, typeof text !== 'string' ? { ...text } : { text });
    }

    /**
     * Close the topmost mask for the specified element
     * @param {HTMLElement} element Element to unmask
     * @returns {Promise|null} A promise which is resolved when the mask is gone, or null if element is not masked
     */
    static unmask(element = document.body) {
        const masks = this.getElementMasks(element);
        if (masks.length > 0) {
            return masks[masks.length - 1].close();
        }
        return null;
    }

    /**
     * Close all masks for the specified element
     * @internal
     */
    static unmaskAll(element = document.body) {
        return this.getElementMasks(element).forEach(mask => mask.close());
    }

    static getElementMasks(element) {
        return this.masks.filter(mask => mask.target === element);
    }

    //endregion

    //region Config

    updateAutoClose(delay) {
        this.deferredClose.cancel();

        if (delay) {
            this.deferredClose.delay = delay;
            this.deferredClose();
        }
    }

    updateCover() {
        this.syncCover();
    }

    syncCover() {
        this.owner?.syncMaskCover?.(this);  // pass "this" since owner may not yet have assigned us to "masked"
    }

    onOwnerResize() {
        this.syncCover();
    }

    updateOwner(owner) {
        this.detachListeners('cover');

        owner?.ion({
            name      : 'cover',
            recompose : 'onOwnerResize',
            resize    : 'onOwnerResize',
            thisObj   : this
        });
    }

    updateShowDelay(delay) {
        const { delayedShow } = this;

        delayedShow.delay = delay;

        if (!delay) {
            delayedShow.flush();
        }
    }

    //endregion

    //region Show & hide

    deferredClose() {
        const { owner } = this;

        this.close().then(() => {
            owner?.onMaskAutoClose?.(this);
        });

        owner?.onMaskAutoClosing?.(this);
    }

    delayedShow() {
        this.classes.remove('b-delayed-show');
    }

    /**
     * Show mask
     */
    show() {
        const
            me = this,
            { element, target, hiding, maskName } = me;

        // We don't do this because we may want to show but automatically close after a
        // brief delay. The order of applying those configs should not be an issue. In
        // other words, to stop the deferredClose, you must set autoClose to falsy.
        // me.deferredClose.cancel();

        if (hiding) {
            // Resolving seems much better than the only other options:
            //  1. Never settling
            //  2. Rejecting
            hiding.resolve();

            // This will be nulled out as the promise resolves but that is a race condition
            // compared to the next hide() call.
            me.hiding = null;

            me.clearTimeout('hide');
        }

        if (me.showDelay) {
            element.classList.add('b-delayed-show');

            me.delayedShow();
        }

        element.classList.add('b-visible');
        element.classList.remove('b-hidden');
        target.classList.add('b-masked');

        if (!target[maskName]) {
            target[maskName] = me;
            target.appendChild(element);
        }

        ArrayHelper.include(Mask.masks, me);

        me.shown = true;
        me.trigger('show');

        // blur has to blur child elements
        if (me.mode.endsWith('blur')) {
            DomHelper.forEachChild(target, child => {
                if (child !== element) {
                    child.classList.add(`b-masked-${me.mode}`);
                }
            });
        }
    }

    /**
     * Hide mask
     * @returns {Promise} A promise which is resolved when the mask is hidden, or immediately if already hidden
     */
    hide() {
        const
            me = this,
            { target, element } = me;

        let { hiding } = me;

        if (!hiding) {
            if (!me.shown) {
                return Promise.resolve();
            }

            me.hiding = hiding = new Promissory();
            me.shown = false;

            element.classList.remove('b-visible');
            element.classList.add('b-hidden');
            target.classList.remove('b-masked');

            if (me.mode.endsWith('blur')) {
                DomHelper.forEachChild(target, child => {
                    if (child !== element) {
                        child.classList.remove(`b-masked-${me.mode}`);
                    }
                });
            }

            hiding.promise = hiding.promise.then(() => {
                if (me.hiding === hiding) {
                    me.hiding = null;
                }
            });


            me.setTimeout(() => hiding.resolve(), 500, 'hide');
        }

        return hiding.promise;
    }

    /**
     * Close mask (removes it)
     * @returns {Promise} A promise which is resolved when the mask is closed
     */
    async close() {
        await this.hide();
        this.destroy();
    }

    //endregion
}
