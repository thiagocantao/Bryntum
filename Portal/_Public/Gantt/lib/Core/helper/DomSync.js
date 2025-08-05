import ArrayHelper from './ArrayHelper.js';
import DomHelper from './DomHelper.js';
import DomClassList from './util/DomClassList.js';
import ObjectHelper from './ObjectHelper.js';
import StringHelper from './StringHelper.js';

/**
 * @module Core/helper/DomSync
 */

const
    arraySlice            = Array.prototype.slice,
    emptyArray            = Object.freeze([]),
    emptyObject           = Object.freeze({}),
    htmlRe                = /[&<]/,  // tests if setInnerText is equivalent to innerHTML
    { getPrototypeOf }    = Object,
    { toString }          = Object.prototype,
    { isEqual, isObject } = ObjectHelper,

    // Attributes used during creation that should not be compared
    checkEqualityIgnore = {
        parent      : 1,
        elementData : 1,
        ns          : 1,
        syncOptions : 1
    },

    makeCheckEqualityOptions = () => ({
        ignore    : checkEqualityIgnore,
        refsFound : new Set()
    }),

    isClass = {
        class     : 1,
        className : 1,
        classname : 1
    },

    simpleTypes = {
        bigint   : 1,
        boolean  : 1,
        function : 1,
        number   : 1,
        // object
        string   : 1,
        symbol   : 1
        // undefined
    },

    // Attributes to ignore on sync
    syncIgnoreAttributes = {
        tag           : 1,
        html          : 1,
        text          : 1,
        children      : 1,
        tooltip       : 1,
        parent        : 1,
        nextSibling   : 1,
        ns            : 1,
        reference     : 1,
        elementData   : 1,
        retainElement : 1,
        compareHtml   : 1,
        syncOptions   : 1,
        listeners     : 1, // eslint-disable-line bryntum/no-listeners-in-lib
        isReleased    : 1,
        null          : 1,
        ''            : 1,
        keepChildren  : 1
    };

const addAndCacheCls = (cls, lastDomConfig) => {
    const
        propertyName  = 'className' in lastDomConfig ? 'className' : 'class',
        propertyValue = lastDomConfig[propertyName];

    if (propertyValue) {
        if (typeof propertyValue === 'string') {
            const value = propertyValue.split(' ');

            if (!value.includes(cls)) {
                value.push(cls);

                lastDomConfig[propertyName] = value.join(' ');
            }
        }
        else if (Array.isArray(propertyValue)) {
            if (!propertyValue.includes(cls)) {
                propertyValue.push(cls);
            }
        }
        else if (propertyValue.isDomClassList) {
            propertyValue.add(cls);
        }
        else if (ObjectHelper.isObject(propertyValue)) {
            propertyValue[cls] = 1;
        }
    }
};

const removeAndUncacheCls = (cls, lastDomConfig) => {
    const
        propertyName  = 'className' in lastDomConfig ? 'className' : 'class',
        propertyValue = lastDomConfig[propertyName];

    if (propertyValue) {
        if (typeof propertyValue === 'string') {
            const value = propertyValue.split(' ');

            if (value.includes(cls)) {
                value.splice(value.indexOf(cls), 1);

                lastDomConfig[propertyName] = value.join(' ');
            }
        }
        else if (Array.isArray(propertyValue)) {
            if (propertyValue.includes(cls)) {
                propertyValue.splice(propertyValue.indexOf(cls), 1);
            }
        }
        else if (propertyValue.isDomClassList) {
            propertyValue.remove(cls);
        }
        else if (ObjectHelper.isObject(propertyValue)) {
            delete propertyValue[cls];
        }
    }
};

/**
 * A utility class for syncing DOM config objects to DOM elements. Syncing compares the new config with the previously
 * used for that element, only applying the difference. Very much like a virtual DOM approach on a per element basis
 * (element + its children).
 *
 * Usage example:
 *
 * ```javascript
 * DomSync.sync({
 *     domConfig: {
 *         className : 'b-outer',
 *         children : [
 *             {
 *                 className : 'b-child',
 *                 html      : 'Child 1',
 *                 dataset   : {
 *                     custom : true
 *                 }
 *             },
 *             {
 *                 className : 'b-child',
 *                 html      : 'Child 2',
 *                 style     : {
 *                     fontWeight : 'bold',
 *                     color      : 'blue'
 *                 }
 *             }
 *         ]
 *     },
 *     targetElement : target
 * });
 * ```
 */
export default class DomSync {
    /**
     * Compares two DOM configs or properties of such objects for equality.
     * @param {Object} is The new value.
     * @param {Object} was The old value.
     * @param {Object} options An object with various options to control the comparison.
     * @param {Object} options.ignore An object containing names of attributes to ignore having `true` value.
     * @param {Map} options.equalityCache A map that can be used to record equality results for objects to avoid
     * recomputing the result for the same objects.
     * @param {Set} options.refsFound A Set that must be populated with the values of any `reference` properties found.
     * @param {Boolean|String} [ignoreRefs] Pass `true` to ignore `reference` properties on domConfigs. Pass `'children'`
     * to ignore `reference` properties only on child element configs.
     * @returns {Boolean}
     * @private
     */
    static checkEquality(is, was, options, ignoreRefs) {
        if (is === was) {
            return true;
        }

        // For purposes of DomSync, null and undefined are equivalent
        if (is == null) {
            return was == null;
        }

        if (!is || !was) {
            return false;  // false since is !== was and is != null (we get here if was == null)
        }

        const
            typeA = typeof is,
            typeB = typeof was;

        if (typeA !== typeB || simpleTypes[typeA]) {  // only test simpleTypes[typeA] since typeA === typeB
            return false;
        }

        // a and b are distinct objects or maybe arrays
        let cache = options.equalityCache || (options.equalityCache = new Map()),
            ignoreChildRefs = Boolean(ignoreRefs),
            equal, i, ignore, ignoreRefOpt, key, syncOptions, val;

        // We must cache results based on both sides of the comparison. If we only cache the result of "is" vs
        // any other value, we get failures in SchedulerWithAutoCommitStore.t.js
        cache = cache.get(is) || cache.set(is, new Map()).get(is);
        equal = cache.get(was);

        if (equal === undefined) {
            equal = true;

            if (getPrototypeOf(is) !== getPrototypeOf(was) || is instanceof Node) {
                // Two Nodes are not equal since they are !==
                equal = false;
            }
            else if (Array.isArray(is)) {
                // Since we have === prototypes, we know that "was" is also an array
                i = is.length;

                if (i !== was.length) {
                    equal = false;
                }
                else {
                    while (i-- > 0) {
                        if (!DomSync.checkEquality(is[i], was[i], options, ignoreChildRefs)) {
                            equal = false;
                            break;
                        }
                    }
                }
            }
            else {
                syncOptions = is.syncOptions;
                ignoreRefOpt = syncOptions?.ignoreRefs;

                if (ignoreRefOpt) {
                    ignoreChildRefs = true;
                    ignoreRefs = ignoreRefOpt !== 'children';
                }

                ignore = options.ignore || emptyObject;

                // We have 2 objects w/same prototype and that are not HTML nodes
                // https://jsbench.me/n2kgfre1r5/1 - profiles for-in-object loops vs for loop over keys array
                // fwiw, the smaller the object, the greater the benefit of for-in loop
                for (key in was) {
                    if (!ignore[key] && !(key in is) && !(ignoreRefs && key === 'reference')) {
                        equal = false;
                        break;
                    }
                }

                if (equal) {
                    if (toString.call(was) === '[object Date]') {
                        // Since we have === prototypes, we know that "was" is also a Date
                        equal = is.getTime() === was.getTime();
                    }
                    else {
                        for (key in is) {
                            if (!ignore[key] && !(ignoreRefs && key === 'reference')) {
                                if (!(key in was)) {
                                    equal = false;
                                    break;
                                }

                                val = is[key];

                                // Per Johan:
                                //  Not sure we still use DocumentFragment. Used to be part of event rendering earlier,
                                //  but I think I have refactored it away. Worth checking
                                //  ...
                                //  Not finding any usages
                                //
                                // DocumentFragment, compare separately supplied html
                                // if (key === 'html' && typeof val !== 'string' && `compareHtml` in is) {
                                //     if (is.compareHtml === was.compareHtml) {
                                //         continue;
                                //     }
                                // }

                                if (!DomSync.checkEquality(val, was[key], options, ignoreChildRefs)) {
                                    equal = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (!ignoreRefs && isObject(is) && is.reference) {
                // We need to track object w/reference properties to know what reference elements may have
                // been removed since we may skip synchronizing them.
                options.refsFound?.add(is.reference);
            }

            cache.set(was, equal);
        }

        return equal;
    }

    /**
     * Sync a DOM config to a target element
     * @param {Object} options Options object
     * @param {DomConfig} options.domConfig A DOM config object
     * @param {HTMLElement} options.targetElement Target element to apply to
     * @param {Boolean} [options.strict=false] Specify `true` to limit synchronization to only the values set by
     * previous calls. Styles and classes placed directly on the DOM elements by other means will not be affected.
     * @param {String} [options.syncIdField] Field in dataset to use to match elements for re-usage
     * @param {String|String[]} [options.affected] The references affected by a partial sync.
     * @param {Function} [options.callback] A function that will be called on element re-usage, creation and similar
     * @param {Boolean} [options.configEquality] A function that will be called to compare an incoming config to
     * the last config applied to the `targetElement`. This function returns `true` if the passed values are equal and
     * `false` otherwise.
     * @returns {HTMLElement} Returns the updated target element (which is also updated in place)
     */
    static sync(options) {
        const
            optionsIn            = options,
            { refOwner }         = options,
            refsWas              = refOwner?.byRef,
            checkEqualityOptions = makeCheckEqualityOptions();

        let affected = options.affected,
            i, ref, targetNode, lastDomConfig;

        if (typeof affected === 'string') {
            affected = [affected];
        }

        // NOTE: it is possible to reenter this method in at least the following way:
        //   - sync() causes a focus change by manipulating the activeElement.
        //   - a focus/blur/focusin/out event causes a widget config to initiate a recompose.
        //   - the event also triggers code that forces the recompose to flush (e.g., by using a reference el).
        options = {
            ...options,
            checkEqualityOptions
        };



        if (refOwner) {
            // We always rebuild the byRef map on each call
            refOwner.byRef = {};

            if (affected) {
                // We need to preserve all previously rendered refs that are not going to be affected by this partial
                // update...
                for (ref in refsWas) {
                    if (!affected.includes(ref)) {
                        refOwner.byRef[ref] = refsWas[ref];
                    }
                }
            }

            options.refsWas = refsWas;
        }

        // performSync() returns false if nothing was done because the configs were equal... we bend the rules on
        // modifying input objects so we can return this potentially important detail to our caller:
        optionsIn.changed = DomSync.performSync(options, options.targetElement);

        if (refOwner) {
            if (!affected) {
                affected = Object.keys(refsWas);
            }

            for (i = 0; i < affected.length; ++i) {
                ref = affected[i];
                targetNode = refsWas[ref];

                if (checkEqualityOptions.refsFound.has(ref) || targetNode.retainElement) {
                    refOwner.byRef[ref] = targetNode;
                }
                else {
                    lastDomConfig = targetNode.lastDomConfig;

                    targetNode.remove();

                    refOwner.detachRef(ref, targetNode, lastDomConfig);
                }
            }
        }

        return options.targetElement;
    }

    static performSync(options, targetElement) {
        const
            { domConfig, callback } = options,
            { lastDomConfig }       = targetElement,
            configIsEqual           = options.configEquality || DomSync.checkEquality;

        if (!configIsEqual(domConfig, lastDomConfig, options.checkEqualityOptions, options.ignoreRefs)) {
            if (domConfig) {
                // Sync without affecting the containing element?
                if (!domConfig.onlyChildren) {
                    DomSync.syncAttributes(domConfig, targetElement, options);
                    DomSync.syncContent(domConfig, targetElement);
                }

                if (!domConfig.keepChildren) {
                    DomSync.syncChildren(options, targetElement);
                }
            }
            // Allow null to clear html
            else {
                targetElement.innerHTML = null;
                targetElement.syncIdMap = null;
            }

            // Cache the config on the target for future comparison
            targetElement.lastDomConfig = !(domConfig?.onlyChildren && lastDomConfig) ? domConfig : {
                ...lastDomConfig,
                children : domConfig.children
            };

            return true;
        }
        else {
            // Sync took no action, notify the world
            callback?.({
                action : 'none',
                domConfig,
                targetElement
            });
        }

        return false;
    }

    //region Attributes

    static syncDataset(domConfig, targetElement) {
        const
            { lastDomConfig } = targetElement,
            sameConfig        = domConfig === lastDomConfig,
            source            = Object.keys(domConfig.dataset),
            target            = lastDomConfig && lastDomConfig.dataset && Object.keys(lastDomConfig.dataset),
            delta             = ArrayHelper.delta(source, target);

        let attr, i, name, value;

        // New attributes in dataset
        for (i = 0; i < delta.onlyInA.length; i++) {
            attr = delta.onlyInA[i];
            value = domConfig.dataset[attr];

            // Prevent data-property="null" or data-property="undefined"
            if (value != null) {
                targetElement.setAttribute(`data-${StringHelper.hyphenate(attr)}`, value);
            }
        }

        // Might have changed
        for (i = 0; i < delta.inBoth.length; i++) {
            attr = delta.inBoth[i];
            value = domConfig.dataset[attr];

            // Intentional != since dataset is always string but want numbers to match
            // noinspection EqualityComparisonWithCoercionJS
            if (sameConfig || value != lastDomConfig.dataset[attr]) {
                name = `data-${StringHelper.hyphenate(attr)}`;

                if (value == null) {
                    targetElement.removeAttribute(name);
                }
                else {
                    targetElement.setAttribute(name, value);
                }
            }
        }

        // Removed
        for (i = 0; i < delta.onlyInB.length; i++) {
            targetElement.removeAttribute(`data-${StringHelper.hyphenate(delta.onlyInB[i])}`);
        }
    }

    /**
     * Adds CSS classes to the element and to the cache.
     * @param {Core.helper.util.DomClassList|String|String[]|Object} cls
     * @param {HTMLElement} targetElement A previously DomSynced element
     * @internal
     */
    static addCls(cls, targetElement) {
        const { lastDomConfig } = targetElement;

        cls = DomClassList.normalize(cls, 'array');

        cls.forEach(cls => {
            targetElement.classList.add(cls);

            addAndCacheCls(cls, lastDomConfig);
        });
    }

    /**
     * Adds CSS classes from the element and from the cache.
     * @param {Core.helper.util.DomClassList|String|String[]|Object} cls
     * @param {HTMLElement} targetElement A previously DomSynced element
     * @internal
     */
    static removeCls(cls, targetElement) {
        const { lastDomConfig } = targetElement;

        cls = DomClassList.normalize(cls, 'array');

        cls.forEach(cls => {
            targetElement.classList.remove(cls);

            removeAndUncacheCls(cls, lastDomConfig);
        });
    }

    static syncClassList(domConfig, targetElement, lastDomConfig) {
        let cls     = domConfig.className || domConfig.class,
            changed = false,
            c, currentClasses, i, k, keep, last;

        if (lastDomConfig) {
            // NOTE: The following reads the DOM to determine classes that may have been added by other means. This
            //  diff is only enabled when "strict" is used (see our callers)
            currentClasses = DomClassList.normalize(targetElement, 'array');
            cls = DomClassList.normalize(cls, 'object');
            last = DomClassList.normalize(lastDomConfig.className || lastDomConfig.class, 'object');
            keep = [];

            for (i = 0, k = currentClasses.length; i < k; ++i) {
                c = currentClasses[i];

                // We want to keep classes not in cls if we didn't add them last time
                if (cls[c] || !(c in last)) {
                    last[c] = 1;
                    keep.push(c);
                }
                else {
                    changed = true;
                }
            }

            for (c in cls) {
                if (!last[c]) {
                    keep.push(c);
                    changed = true;
                }
            }

            if (!changed) {
                return;
            }

            cls = keep.join(' ');
        }
        else {
            cls = DomClassList.normalize(cls);  // to string
        }

        targetElement.setAttribute('class', cls);
    }

    // Attributes as map { attr : value, ... }
    static getSyncAttributes(domConfig) {
        const
            attributes = {},
            // Attribute names, simplifies comparisons and calls to set/removeAttribute
            names      = [];

        // On a first sync, there are no domConfig on the target element yet
        if (domConfig) {
            Object.keys(domConfig).forEach(attr => {
                if (!syncIgnoreAttributes[attr]) {
                    const name = attr.toLowerCase();
                    attributes[name] = domConfig[attr];
                    names.push(name);
                }
            });
        }

        return { attributes, names };
    }

    static syncAttributes(domConfig, targetElement, options) {
        const
            { lastDomConfig } = targetElement,
            // If the same config has come through, due to configEquality, we must update all attrs.
            sameConfig        = domConfig === lastDomConfig,
            sourceSyncAttrs   = DomSync.getSyncAttributes(domConfig),
            // Extract attributes from elements (sourceElement might be a config)
            {
                attributes : sourceAttributes,
                names      : sourceNames
            }                 = sourceSyncAttrs,
            {
                attributes : targetAttributes,
                names      : targetNames
            }                 = sameConfig ? sourceSyncAttrs : DomSync.getSyncAttributes(lastDomConfig),
            // Intersect arrays to determine what needs adding, removing and syncing
            {
                onlyInA : toAdd,
                onlyInB : toRemove,
                inBoth  : toSync
            }                 = sameConfig ? {
                onlyInA : emptyArray,
                onlyInB : emptyArray,
                inBoth  : sourceNames
            } : ArrayHelper.delta(sourceNames, targetNames);

        let attr, i;

        // Add new attributes
        for (i = 0; i < toAdd.length; i++) {
            attr = toAdd[i];

            const sourceAttr = sourceAttributes[attr];

            // Style requires special handling
            if (attr === 'style' && sourceAttr != null) {

                DomHelper.applyStyle(targetElement, sourceAttr, true);
            }
            // So does dataset
            else if (attr === 'dataset') {
                DomSync.syncDataset(domConfig, targetElement);
            }
            // And class, which might be an object
            else if (isClass[attr]) {
                DomSync.syncClassList(domConfig, targetElement);
            }
            // Other attributes are set using setAttribute (since it calls toString() DomClassList works fine),
            // unless they are undefined in which case they are ignored to not get `href="undefined"` etc
            else if (sourceAttr != null) {
                targetElement.setAttribute(attr, sourceAttr);
            }
        }

        // Removed no longer used attributes
        for (i = 0; i < toRemove.length; i++) {
            targetElement.removeAttribute(toRemove[i]);
        }


        for (i = 0; i < toSync.length; i++) {
            attr = toSync[i];

            const
                sourceAttr = sourceAttributes[attr],
                targetAttr = targetAttributes[attr];

            // Attribute value null means remove attribute
            if (sourceAttr == null) {
                targetElement.removeAttribute(attr);
            }
            // Set all attributes that has changed, with special handling for style.
            else if (attr === 'style') {
                if (options.strict) {
                    if (sameConfig) {
                        DomSync.syncStyles(targetElement, sourceAttr);
                    }
                    else if (!isEqual(sourceAttr, targetAttr, true)) {
                        DomSync.syncStyles(targetElement, sourceAttr, targetAttr);
                    }
                }
                else if (sameConfig || !isEqual(sourceAttr, targetAttr, true)) {
                    DomHelper.applyStyle(targetElement, sourceAttr, true);
                }
            }
            // And dataset
            else if (attr === 'dataset') {
                DomSync.syncDataset(domConfig, targetElement);
            }
            // And class, which might be an object
            else if (isClass[attr]) {
                DomSync.syncClassList(domConfig, targetElement, options.strict && targetElement.lastDomConfig);
            }
            else if (sameConfig || sourceAttr !== targetAttr) {
                targetElement.setAttribute(attr, sourceAttr);
            }
        }
    }

    static syncStyles(targetElement, sourceAttr, targetAttr) {
        let styles, key, value;

        if (!targetAttr) {
            styles = sourceAttr;
        }
        else {
            styles = {};
            // Style could be a string so we parse it to object to iterate over it's properties correctly
            sourceAttr = DomHelper.parseStyle(sourceAttr);
            targetAttr = DomHelper.parseStyle(targetAttr);

            if (sourceAttr) {
                for (key in sourceAttr) {
                    value = sourceAttr[key];

                    if (targetAttr[key] !== value) {
                        styles[key] = value;
                    }
                }
            }

            for (key in targetAttr) {
                if (!(key in sourceAttr)) {
                    styles[key] = '';
                }
            }
        }

        DomHelper.applyStyle(targetElement, styles);
    }

    //endregion

    //region Content

    static syncContent(domConfig, targetElement) {
        const
            { html, text } = domConfig,
            content = text ?? html;

        targetElement.didSetTextContent = false;

        // elementData holds custom data that we want to attach to the element (not visible in dom)
        if (domConfig.elementData) {
            targetElement.elementData = domConfig.elementData;
        }

        // Apply html from config
        if (content instanceof DocumentFragment) {
            // If given a DocumentFragment, replace content with it
            if (
                targetElement.childNodes.length === 1 &&
                DomHelper.getChildElementCount(targetElement) === 0 &&
                content.childNodes.length === 1 &&
                DomHelper.getChildElementCount(content) === 0
            ) {
                // Syncing a textNode to a textNode? Use shortcut
                DomHelper.setInnerText(targetElement, content.firstChild.data);
            }
            else {
                targetElement.innerHTML = '';
                targetElement.appendChild(content);
            }
        }
        // If content is likely html we set innerHTML
        else if (html != null && htmlRe.test(html)) {
            targetElement.innerHTML = String(html);  // convert numbers to strings
        }
        else if (content != null) {
            const { lastDomConfig } = targetElement;
            // If element had children or actual html content in last sync we force innerHTML to get rid of them
            if (lastDomConfig?.children?.length || (lastDomConfig?.html && htmlRe.test(targetElement.lastDomConfig.html))) {
                targetElement.innerHTML = content;
            }
            // Otherwise we try to update the text as cheaply as possible (node data if possible)
            else {
                DomHelper.setInnerText(targetElement, String(content));
                // syncChildren won't sync the text node, since we set it here. Instead, it will attempt to release it,
                // which we prevent using this flag
                targetElement.didSetTextContent = true;
            }
        }
    }

    static insertTextNode(text, targetElement, callback, refOwner, beforeElement = null) {
        const newNode = document.createTextNode(text);

        targetElement.insertBefore(newNode, beforeElement);

        if (refOwner) {
            newNode.$refOwnerId = refOwner.id;
        }

        callback?.({
            action        : 'newNode',
            domConfig     : text,
            targetElement : newNode
        });
    }

    static insertElement(domConfig, targetElement, targetNode, refOwner, syncIdMap, syncId, options) {
        // Create a new element
        const newElement = options.ns
            ? document.createElementNS(options.ns, domConfig.tag || 'svg')
            : document.createElement(domConfig.tag || 'div');

        // Insert (or append if no targetNode)
        targetElement.insertBefore(newElement, targetNode);

        // Sync to it
        DomSync.performSync(options, newElement);

        if (syncId != null) {
            syncIdMap[syncId] = newElement;
        }

        // ARIA. In the absence of a defined role or the element being hidden from ARIA,
        // omit unfocusable elements from the accessibility tree.
        if (!domConfig.role && !domConfig.tabIndex && !domConfig['aria-hidden'] && !DomHelper.isFocusable(newElement, true) && !newElement.htmlFor) {
            newElement.setAttribute('role', 'presentation');
        }
        if (refOwner) {
            newElement.$refOwnerId = refOwner.id;

            if (syncId) {
                newElement.$reference = syncId;

                refOwner.attachRef(syncId, newElement, domConfig);
            }
        }

        options.callback?.({
            action        : 'newElement',
            domConfig,
            targetElement : newElement,
            syncId
        });
    }

    //endregion

    //region Children

    static syncChildren(options, targetElement) {
        let {
                domConfig,
                syncIdField,
                callback,
                releaseThreshold,
                configEquality,
                ns,
                refOwner,
                refsWas,
                strict,
                checkEqualityOptions,
                ignoreRefs
            }            = options,
            cleanupNodes = null,
            index, nextNode, syncId;
        const syncOptions = domConfig.syncOptions || {};

        // Having specified html or text replaces all inner content, no point in syncing
        if (domConfig.html ?? domConfig.text) {
            return;
        }

        // This will affect the whole subtree since this goes into syncChildOptions (we convert 'children' to true
        // here because we are only processing the children):
        ignoreRefs = Boolean(syncOptions.ignoreRefs ?? ignoreRefs);

        if (ignoreRefs) {
            refOwner = refsWas = null;
        }

        if ('strict' in syncOptions) {
            strict = syncOptions.strict;
        }

        const
            // Always repopulate the map, since elements might get used by other syncId below
            newSyncIdMap  = refOwner ? refOwner.byRef : {},
            sourceConfigs = arraySlice.call(domConfig.children || []),
            targetNodes   = arraySlice.call(targetElement.childNodes),
            syncIdMap     = refsWas || targetElement.syncIdMap || {},
            releasedIdMap = targetElement.releasedIdMap || {},
            nextTarget    = remove => {
                // Recursive calls to performSync can teleport elements around the DOM tree (when we are given
                // the DOM nodes in the domConfig), so be sure to skip over any elements that are no longer children
                // of our targetElement
                while (targetNodes.length && targetNodes[0].parentNode !== targetElement) {
                    targetNodes.shift();
                }

                return (remove ? targetNodes.shift() : targetNodes[0]) || null;
            };

        // Each level can optionally specify its own syncIdField, strict and callback, if left out parent levels will be used
        syncIdField = syncOptions.syncIdField || syncIdField;
        strict = syncOptions.strict || strict;
        callback = syncOptions.callback || callback;
        configEquality = syncOptions.configEquality || configEquality;
        // Make sure releaseThreshold 0 is respected...
        releaseThreshold = 'releaseThreshold' in syncOptions ? syncOptions.releaseThreshold : releaseThreshold;

        if (syncIdField) {
            targetElement.syncIdMap = newSyncIdMap;
        }

        // Settings to use in all syncs below
        const syncChildOptions = {
            checkEqualityOptions : checkEqualityOptions || makeCheckEqualityOptions(),
            ignoreRefs,
            refOwner,
            refsWas,
            strict,
            syncIdField,
            releaseThreshold,
            callback,
            configEquality
        };

        while (sourceConfigs.length) {
            const sourceConfig = sourceConfigs.shift();

            syncId = null;

            // Allowing null, convenient when using Array.map() to generate children
            if (!sourceConfig) {
                continue;
            }

            // React JSX, pass directly to callback
            if (DomHelper.isReactElement(sourceConfig)) {
                callback({ jsx : sourceConfig, targetElement });
                continue;
            }

            // If a child item is an array, unroll it
            if (Array.isArray(sourceConfig)) {
                this.syncChildren({
                    ...options,
                    domConfig : {
                        ...options.domConfig,
                        children : sourceConfig
                    }
                }, targetElement);
                continue;
            }

            if (sourceConfig instanceof Node) {
                nextNode = nextTarget();

                // Widgets may supply the element of another widget in their rendering... just insert it and move on
                if (sourceConfig !== nextNode) {
                    targetElement.insertBefore(sourceConfig, nextNode);
                }

                index = targetNodes.indexOf(sourceConfig);

                if (index > -1) {
                    targetNodes.splice(index, 1);
                }

                continue;
            }

            const isTextNode = typeof sourceConfig === 'string';

            // Used in all syncs
            syncChildOptions.domConfig = sourceConfig;
            syncChildOptions.ns = sourceConfig.ns || ns;

            if (!isTextNode) {
                // If syncIdField was supplied, we should first try to reuse element with
                // matching "id"
                if (refOwner) {
                    syncId = sourceConfig.reference;
                }
                else if (syncIdField && sourceConfig.dataset) {
                    syncId = sourceConfig.dataset[syncIdField];
                }

                // We have an id to look for
                if (syncId != null && !sourceConfig.unmatched) {
                    // Find any matching element, either in use or previously released
                    const syncTargetElement = syncIdMap[syncId] || releasedIdMap[syncId];

                    if (syncTargetElement) {
                        const { lastDomConfig } = syncTargetElement;

                        // Just relink if flagged with `retainElement` (for example during dragging)
                        if (syncTargetElement.retainElement) {
                            // DomSync.relinkElements(syncChildOptions.domConfig, syncTargetElement);
                        }
                        // Otherwise sync with the matched element
                        else if (
                            DomSync.performSync(syncChildOptions, syncTargetElement)
                        ) {
                            // Sync took some action, notify the world
                            callback?.({
                                action        : 'reuseOwnElement',
                                domConfig     : sourceConfig,
                                targetElement : syncTargetElement,
                                lastDomConfig,
                                syncId
                            });
                        }

                        // Since it won't sync above when flagged to be retained, we need to apply the flag here
                        if (sourceConfig.retainElement) {
                            syncTargetElement.retainElement = true;
                        }

                        // Cache the element on the syncIdMap for faster retrieval later
                        newSyncIdMap[syncId] = syncTargetElement;

                        // Remove target from targetElements & release tracking, no-one else is allowed to sync with it
                        ArrayHelper.remove(targetNodes, syncTargetElement);
                        delete releasedIdMap[syncId];

                        syncTargetElement.isReleased = false;
                        nextNode = nextTarget();

                        if (syncTargetElement.parentNode !== targetElement ||
                            (strict && syncTargetElement.nextSibling !== nextNode)) {
                            targetElement.insertBefore(syncTargetElement, nextNode);
                        }
                    }
                    else if (strict) {
                        DomSync.insertElement(sourceConfig, targetElement, nextTarget(), refOwner,
                            newSyncIdMap, syncId, syncChildOptions);
                    }
                    else {
                        // No match, move to end of queue to not steal some one else's element
                        sourceConfigs.push(sourceConfig);
                        // Also flag as unmatched to know that when we reach this element again
                        sourceConfig.unmatched = true;
                    }

                    // Node handled, carry on with next one
                    continue;
                }

                // Avoid polluting the config object when done
                if (sourceConfig.unmatched) {
                    delete sourceConfig.unmatched;
                }
            }

            // Skip over any retained elements
            let beforeNode = null,
                targetNode = null,
                cleanupNode;

            while (!targetNode && (cleanupNode = nextTarget(true))) {
                if (refOwner) {
                    // When syncing for a refOwner, foreign elements are skipped.
                    if (cleanupNode.$refOwnerId !== refOwner.id) {
                        continue;
                    }

                    if (cleanupNode.$reference) {
                        // In refOwner mode we always pass strict:true, so this won't happen... but if it did, the
                        // idea is that ref els do not get cleaned up until the end of the sync process.
                        if (!strict) {
                            continue;
                        }

                        // Since we want to maintain DOM order, this ref el marks the spot where to insert. We also
                        // don't want to put it into cleanupNodes (see above). We cannot reuse ref els.
                        beforeNode = cleanupNode;
                        break;
                    }

                    // The element is owned by this refOwner and not assigned a reference...
                    // We can reuse it
                    targetNode = cleanupNode;
                }
                else if (!cleanupNode.retainElement) {
                    targetNode = cleanupNode;
                }

                if (!targetNode) {
                    (cleanupNodes || (cleanupNodes = [])).push(cleanupNode);
                }
            }

            if (beforeNode || !targetNode) {
                if (isTextNode) {
                    DomSync.insertTextNode(sourceConfig, targetElement, callback, refOwner, beforeNode);
                }
                else {
                    // Will append if beforeNode === null
                    DomSync.insertElement(sourceConfig, targetElement, beforeNode, refOwner,
                        newSyncIdMap, syncId, syncChildOptions);
                }
            }
            // We have targets left
            else {
                // Matching element tag, sync it
                if (
                    !isTextNode &&
                    targetNode.nodeType === Node.ELEMENT_NODE &&
                    (sourceConfig.tag || 'div').toLowerCase() === targetNode.tagName.toLowerCase()
                ) {
                    const
                        { lastDomConfig } = targetNode,
                        result            = DomSync.performSync(syncChildOptions, targetNode);

                    // Remove reused element from release tracking
                    if (syncIdField && lastDomConfig?.dataset?.[syncIdField] != null) {
                        const lastSyncId = lastDomConfig.dataset[syncIdField];
                        delete releasedIdMap[lastSyncId];
                        // Also remove from syncIdMap to not reuse again in the same sync
                        delete syncIdMap[lastSyncId];
                    }

                    if (syncId != null) {
                        newSyncIdMap[syncId] = targetNode;
                    }

                    targetNode.isReleased = false;

                    // Only use callback if sync succeeded (anything changed)
                    result && callback?.({
                        action        : 'reuseElement',
                        domConfig     : sourceConfig,
                        targetElement : targetNode,
                        lastDomConfig,
                        syncId
                    });
                }
                // Text node to text node, change text :)
                else if (isTextNode && targetNode.nodeType === Node.TEXT_NODE) {
                    targetNode.data = sourceConfig;
                    // Not using callback for updating text of node, have no usecase for it currently
                }
                // Not matching, replace it
                else {
                    if (isTextNode) {
                        DomSync.insertTextNode(sourceConfig, targetElement, callback, refOwner, targetNode);
                    }
                    else {
                        // Will insert
                        DomSync.insertElement(sourceConfig, targetElement, targetNode, refOwner,
                            newSyncIdMap, syncId, syncChildOptions);
                    }

                    targetNode.remove();
                }
            }
        } // while (sourceConfigs.length)

        // If we have a single text node remaining, and we did set node data or textContent, we should not clean it up
        if (targetElement.didSetTextContent && targetNodes.length === 1 && targetNodes[0].nodeType === Node.TEXT_NODE) {
            return;
        }

        // Out of source nodes, remove remaining target nodes
        while ((nextNode = nextTarget(true))) {
            // Any remaining nodes that belong to this refOwner need to be cleaned up. If
            // they have an assigned reference, however, they will be handled at the very
            // end of the sync process since those elements can move in the node hierarchy.
            if (!refOwner || (nextNode.$refOwnerId === refOwner.id && !nextNode.$reference)) {
                (cleanupNodes || (cleanupNodes = [])).push(nextNode);
            }
        }

        if (cleanupNodes) {
            DomSync.syncChildrenCleanup(targetElement, cleanupNodes, newSyncIdMap, callback, refOwner,
                releaseThreshold, syncIdField);
        }
    }

    static syncChildrenCleanup(targetElement, cleanupNodes, newSyncIdMap, callback, refOwner, releaseThreshold, syncIdField) {
        let releaseCount = 0,
            ref;

        for (const targetNode of cleanupNodes) {
            const { lastDomConfig } = targetNode;

            // Text nodes are always just wiped out as they are not recycled
            if (targetNode.nodeType === Element.TEXT_NODE) {
                targetNode.remove();
                continue;
            }

            // Element might be retained, hands off (for example while dragging)
            if (!targetNode.retainElement) {
                // When using syncId to reuse elements, "release" left over elements instead of removing them, up to a
                // limit specified as releaseThreshold, above which elements are removed instead
                if (!refOwner && syncIdField && (releaseThreshold == null || releaseCount < releaseThreshold)) {
                    // Prevent releasing already released element
                    if (!targetNode.isReleased) {
                        targetNode.className = 'b-released';
                        targetNode.isReleased = true;

                        // Store released element in syncIdMap, to facilitate reusing it for self later
                        if (lastDomConfig?.dataset) {
                            if (!targetElement.releasedIdMap) {
                                targetElement.releasedIdMap = {};
                            }
                            targetElement.releasedIdMap[lastDomConfig.dataset[syncIdField]] = targetNode;
                        }

                        callback?.({
                            action        : 'releaseElement',
                            domConfig     : lastDomConfig,
                            lastDomConfig,
                            targetElement : targetNode
                        });

                        // Done after callback on purpose, to allow checking old className
                        if (lastDomConfig) {
                            // Make sure lastDomConfig differs even from the same domConfig applied again
                            // Do not want to discard it completely since it is needed for diff when reused later
                            lastDomConfig.isReleased = true;

                            // To force reapply of classes on reuse
                            if (lastDomConfig.className) {
                                lastDomConfig.className = 'b-released';
                            }
                            if (lastDomConfig.class) {
                                lastDomConfig.class = 'b-released';
                            }

                            // Same for style
                            // (for elements positioned using style, when moved in a non DomSync way, aka EventDrag)
                            if (lastDomConfig.style) {
                                lastDomConfig.style = null;
                            }
                        }
                    }

                    releaseCount++;
                }
                // In normal sync mode, remove left overs
                else {
                    targetNode.remove();

                    if (refOwner) {
                        ref = targetNode.$reference;

                        if (ref) {
                            refOwner.detachRef(ref, targetNode, lastDomConfig);
                        }
                    }

                    // Remove from "release tracking"
                    if (targetElement.releasedIdMap && syncIdField && lastDomConfig?.dataset) {
                        delete targetElement.releasedIdMap[lastDomConfig.dataset[syncIdField]];
                    }

                    callback?.({
                        action        : 'removeElement',
                        domConfig     : targetNode.lastDomConfig,
                        lastDomConfig : targetNode.lastDomConfig,
                        targetElement : targetNode
                    });
                }
            }
            else if (syncIdField) {
                // Keep retained element in map
                if (lastDomConfig) {
                    newSyncIdMap[targetNode.dataset[syncIdField]] = targetNode;
                }
            }
        }
    }

    /**
     * Remove a child element without syncing, for example when dragging an element to some other parent.
     * Removes it both from DOM and the parent elements syncMap
     * @param {HTMLElement} parentElement
     * @param {HTMLElement} childElement
     */
    static removeChild(parentElement, childElement) {
        if (parentElement.contains(childElement)) {
            const syncIdMap = parentElement.syncIdMap;
            if (syncIdMap) {
                const index = Object.values(syncIdMap).indexOf(childElement);
                if (index > -1) {
                    delete syncIdMap[Object.keys(syncIdMap)[index]];
                }
            }
            parentElement.removeChild(childElement);
        }
    }

    /**
     * Adds a child element without syncing, making it properly available for later syncs. Useful for example
     * when dragging and dropping an element from some other parent.
     * @param {HTMLElement} parentElement
     * @param {HTMLElement} childElement
     * @param {String|Number} syncId
     */
    static addChild(parentElement, childElement, syncId) {
        parentElement.appendChild(childElement);

        if (!parentElement.syncIdMap) {
            parentElement.syncIdMap = {};
        }

        parentElement.syncIdMap[syncId] = childElement;
    }

    /**
     * Get a child element using a dot separated syncIdMap path.
     *
     * ```javascript
     * DomSync.getChild(eventWrap, 'event.percentBar');
     * ```
     *
     * @param {HTMLElement} element "root" element, under which the path starts
     * @param {String} path Dot '.' separated path of syncIdMap entries
     * @returns {HTMLElement} Child element or `null` if path did not match any element
     */
    static getChild(element, path) {
        const syncIds = String(path).split('.');

        for (const id of syncIds) {
            element = element?.syncIdMap?.[id];

            if (!element) {
                return null;
            }
        }

        return element;
    }

    //endregion
}
