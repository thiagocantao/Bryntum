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
    htmlRe                = /[&<]/,  // tests if setInnerText is equivalent to innerHTML
    // Used in sync to give ObjectHelper.isDeeplyEqual() some domain knowledge
    syncEqualityEvaluator = {
        // Attributes used during creation that should not be compared
        ignore : {
            _element    : 1,
            parent      : 1,
            elementData : 1,
            ns          : 1
        },
        // Function to evaluate 'compareHtml' property instead of 'html' for DocumentFragments
        evaluate(property, a, b) {
            if (property === 'html' && typeof a.value !== 'string' && `compareHtml` in a.object) {
                // DocumentFragment, compare separately supplied html
                return (a.object.compareHtml === b.object.compareHtml);
            }
        }
    },
    // Attributes to ignore on sync
    syncIgnoreAttributes  = {
        tag           : 1,
        html          : 1,
        text          : 1,
        children      : 1,
        tooltip       : 1,
        parent        : 1,
        nextSibling   : 1,
        ns            : 1,
        reference     : 1,
        _element      : 1,
        elementData   : 1,
        retainElement : 1,
        compareHtml   : 1,
        syncOptions   : 1,
        listeners     : 1,
        isReleased    : 1
    },
    isClass               = {
        class     : 1,
        className : 1,
        classname : 1
    },
    emptyArray            = Object.freeze([]);

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
     * Sync a DOM config to a target element
     * @param {Object} options Options object
     * @param {Object} options.domConfig A DOM config object
     * @param {HTMLElement} options.targetElement Target element to apply to
     * @param {Boolean} options.strict Specify `true` to limit synchronization to only the values set by previous calls.
     * Styles and classes placed directly on the DOM elements by other means will not be affected.
     * @param {String} [options.syncIdField] Field in dataset to use to match elements for re-usage
     * @param {String|String[]} [options.affected] The references affected by a partial sync.
     * @param {Function} [options.callback] A function that will be called on element re-usage, creation and similar
     * @param {Boolean} [options.configEquality] A function that will be called to compare an incoming config to
     * the last config applied to the `targetElement`. Defaults to {@link Core.helper.ObjectHelper#function-isDeeplyEqual-static isDeeplyEqual}.
     * @returns {HTMLElement} Returns the updated target element (which is also updated in place)
     */
    static sync(options) {
        const
            refOwner = options.refOwner,
            refsWas = refOwner && refOwner.byRef;

        let affected = options.affected,
            i, ref, targetNode, lastDomConfig;

        if (typeof affected === 'string') {
            affected = [affected];
        }

        //<debug>
        if (options.actionLog) {
            const { actionLog, callback } = options;

            options = Object.assign({}, options);

            options.callback = (ev) => {
                actionLog.push(ev);
                callback && callback(ev);
            };
        }
        //</debug>

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

            options = Object.assign({ refsWas }, options);
        }

        // performSync() returns false if nothing was done because the configs were equal...
        if (this.performSync(options, options.targetElement) && refOwner) {
            if (!affected) {
                affected = Object.keys(refsWas);
            }

            for (i = 0; i < affected.length; ++i) {
                ref = affected[i];

                if (!(ref in refOwner.byRef)) {
                    targetNode = refsWas[ref];

                    // If we are doing a partial update, the list of affected refs may contain refs that were not
                    // rendered last time...
                    if (targetNode) {
                        lastDomConfig = targetNode.lastDomConfig;

                        targetNode.remove();

                        refOwner.detachRef(ref, targetNode, lastDomConfig);
                    }
                }
            }
        }

        return options.targetElement;
    }

    static performSync(options, targetElement) {
        const
            { domConfig, callback } = options,
            configIsEqual           = options.configEquality
                ? options.configEquality(domConfig, targetElement.lastDomConfig, syncEqualityEvaluator)
                : ObjectHelper.isDeeplyEqual(domConfig, targetElement.lastDomConfig, syncEqualityEvaluator);

        if (!configIsEqual) {
            if (domConfig) {
                // Sync without affecting the containing element?
                if (!domConfig.onlyChildren) {
                    this.syncAttributes(domConfig, targetElement, options);
                    this.syncContent(domConfig, targetElement);
                }

                this.syncChildren(options, targetElement);
                // Link the element for easy retrieval later
                domConfig._element = targetElement;
            }
            // Allow null to clear html
            else {
                targetElement.innerHTML = null;
                targetElement.syncIdMap = null;
            }

            // Cache the config on the target for future comparison
            targetElement.lastDomConfig = domConfig;

            return true;
        }
        else {
            // Maintain link to element (deep)
            this.relinkElements(domConfig, targetElement);
            // Sync took no action, notify the world
            callback && callback({
                action : 'none',
                domConfig,
                targetElement
            });
        }

        return false;
    }

    // Called from sync when there is no change to elements, to set up link between new config and existing element.
    // Plucks the element from the last applied config, no need to hit DOM so is cheap
    static relinkElements(domConfig, targetElement) {
        // Hands off when retaining element (it is for some reason taken out of the normal rendering flow, for example
        // by dragging it)
        if (!domConfig.retainElement) {
            domConfig._element = targetElement;

            // Since there was no change detected, there is a 1 to 1 ratio between new config and last config, should be
            // safe to do a straight mapping
            domConfig.children && domConfig.children.forEach((childDomConfig, i) => {
                // Skip null entries, allowed for convenience, neat with map.
                // Also skip text nodes
                if (childDomConfig && typeof childDomConfig !== 'string') {
                    this.relinkElements(childDomConfig, targetElement.lastDomConfig.children[i]._element);
                }
            });
        }
    }

    //region Attributes

    static syncDataset(domConfig, targetElement) {
        const
            { lastDomConfig } = targetElement,
            sameConfig        = domConfig === lastDomConfig,
            source            = Object.keys(domConfig.dataset),
            target            = lastDomConfig && lastDomConfig.dataset && Object.keys(lastDomConfig.dataset),
            delta             = ArrayHelper.delta(source, target);

        // New attributes in dataset
        for (let i = 0; i < delta.onlyInA.length; i++) {
            const
                attr = delta.onlyInA[i],
                value = domConfig.dataset[attr];
            // Prevent `data-property="undefined"`
            if (value !== undefined) {
                targetElement.setAttribute(`data-${StringHelper.hyphenate(attr)}`, value);
            }
        }

        // Might have changed
        for (let i = 0; i < delta.inBoth.length; i++) {
            const attr = delta.inBoth[i];
            // Intentional != since dataset is always string but want numbers to match
            // noinspection EqualityComparisonWithCoercionJS
            if (sameConfig || domConfig.dataset[attr] != lastDomConfig.dataset[attr]) {
                targetElement.setAttribute(`data-${StringHelper.hyphenate(attr)}`, domConfig.dataset[attr]);
            }
        }

        // Removed
        for (let i = 0; i < delta.onlyInB.length; i++) {
            targetElement.removeAttribute(`data-${StringHelper.hyphenate(delta.onlyInB[i])}`);
        }
    }

    static syncClassList(domConfig, targetElement, lastDomConfig) {
        let cls = domConfig.className || domConfig.class,
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
            }

            for (c in cls) {
                if (!last[c]) {
                    keep.push(c);
                }
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
            sourceSyncAttrs   = this.getSyncAttributes(domConfig),
            // Extract attributes from elements (sourceElement might be a config)
            {
                attributes : sourceAttributes,
                names      : sourceNames
            }                 = sourceSyncAttrs,
            {
                attributes : targetAttributes,
                names      : targetNames
            }                 = sameConfig ? sourceSyncAttrs : this.getSyncAttributes(lastDomConfig),
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

        // Add new attributes
        for (let i = 0; i < toAdd.length; i++) {
            const
                attr       = toAdd[i],
                sourceAttr = sourceAttributes[attr];

            // Style requires special handling
            if (attr === 'style') {
                // TODO: Do diff style apply also instead of this replace
                DomHelper.applyStyle(targetElement, sourceAttr, true);
            }
            // So does dataset
            else if (attr === 'dataset') {
                this.syncDataset(domConfig, targetElement);
            }
            // And class, which might be an object
            else if (isClass[attr]) {
                this.syncClassList(domConfig, targetElement);
            }
            // Other attributes are set using setAttribute (since it calls toString() DomClassList works fine),
            // unless they are undefined in which case they are ignored to not get `href="undefined"` etc
            else if (sourceAttr !== undefined) {
                targetElement.setAttribute(attr, sourceAttr);
            }
        }

        // Removed no longer used attributes
        for (let i = 0; i < toRemove.length; i++) {
            targetElement.removeAttribute(toRemove[i]);
        }

        // TODO: toAdd and toSync are growing very alike, consider merging
        // Sync values for all other attributes
        for (let i = 0; i < toSync.length; i++) {
            const
                attr       = toSync[i],
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
                        this.syncStyles(targetElement, sourceAttr);
                    }
                    else if (!ObjectHelper.isEqual(sourceAttr, targetAttr, true)) {
                        this.syncStyles(targetElement, sourceAttr, targetAttr);
                    }
                }
                else if (sameConfig || !ObjectHelper.isEqual(sourceAttr, targetAttr, true)) {
                    DomHelper.applyStyle(targetElement, sourceAttr, true);
                }
            }
            // And dataset
            else if (attr === 'dataset') {
                this.syncDataset(domConfig, targetElement);
            }
            // And class, which might be an object
            else if (isClass[attr]) {
                this.syncClassList(domConfig, targetElement, options.strict && targetElement.lastDomConfig);
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

        // elementData holds custom data that we want to attach to the element (not visible in dom)
        if (domConfig.elementData) {
            targetElement.elementData = domConfig.elementData;
        }

        // Apply html from config
        if (content instanceof DocumentFragment) {
            // If given a DocumentFragment, replace content with it
            if (
                targetElement.childNodes.length === 1 &&
                targetElement.childElementCount === 0 &&
                content.childNodes.length === 1 &&
                content.childElementCount === 0
            ) {
                // Syncing a textNode to a textNode? Use shortcut
                DomHelper.setInnerText(targetElement, content.firstChild.data);
            }
            else {
                targetElement.innerHTML = '';
                targetElement.appendChild(content);
            }
        }
        else if (html != null && htmlRe.test(html)) {
            targetElement.innerHTML = String(html);  // convert numbers to strings
        }
        else if (content != null) {
            DomHelper.setInnerText(targetElement, String(content));
        }
    }

    static insertTextNode(text, targetElement, callback, refOwner, beforeElement = null) {
        const newNode = document.createTextNode(text);

        targetElement.insertBefore(newNode, beforeElement);

        if (refOwner) {
            newNode.$refOwnerId = refOwner.id;
        }

        callback && callback({
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
        this.performSync(options, newElement);

        if (syncId != null) {
            syncIdMap[syncId] = newElement;
        }

        if (refOwner) {
            newElement.$refOwnerId = refOwner.id;

            if (syncId) {
                newElement.$reference = syncId;

                refOwner.attachRef(syncId, newElement, domConfig);
            }
        }

        options.callback && options.callback({
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
            domConfig, syncIdField, callback, releaseThreshold, configEquality, ns, refOwner, refsWas, strict
        } = options;

        // Having specified html or text replaces all inner content, no point in syncing
        if (domConfig.html ?? domConfig.text) {
            return;
        }

        const
            me            = this,
            // Always repopulate the map, since elements might get used by other syncId below
            newSyncIdMap  = refOwner ? refOwner.byRef : {},
            sourceConfigs = arraySlice.call(domConfig.children || []),
            targetNodes   = arraySlice.call(targetElement.childNodes),
            syncIdMap     = refsWas || targetElement.syncIdMap || {},
            syncOptions   = domConfig.syncOptions || {},
            releasedIdMap = targetElement.releasedIdMap || {};

        // Each level can optionally specify its own syncIdField, strict and callback, if left out parent levels will be used
        syncIdField = syncOptions.syncIdField || syncIdField;
        strict = syncOptions.strict || strict;
        callback = syncOptions.callback || callback;
        configEquality = syncOptions.configEquality || configEquality;
        // Make sure releaseThreshold 0 is respected...
        releaseThreshold = 'releaseThreshold' in syncOptions ? syncOptions.releaseThreshold : releaseThreshold;

        let cleanupNodes = null, syncId;

        if (syncIdField) {
            targetElement.syncIdMap = newSyncIdMap;
        }

        // Settings to use in all syncs below
        const syncChildOptions = {
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
                        if (
                            // Ignore if flagged with `retainElement` (for example during dragging)
                            !syncTargetElement.retainElement &&
                            // Otherwise sync with the matched element
                            me.performSync(syncChildOptions, syncTargetElement)
                        ) {
                            // Sync took some action, notify the world
                            callback && callback({
                                action        : 'reuseOwnElement',
                                domConfig     : sourceConfig,
                                lastDomConfig,
                                targetElement : syncTargetElement,
                                syncId
                            });
                        }

                        // Since it wont sync above when flagged to be retained, we need to apply the flag here
                        if (sourceConfig.retainElement) {
                            syncTargetElement.retainElement = true;
                            // Normally linked in performSync(), but for retained elements that fn is not called
                            sourceConfig._element = syncTargetElement;
                        }

                        // Cache the element on the syncIdMap for faster retrieval later
                        newSyncIdMap[syncId] = syncTargetElement;

                        // Remove target from targetElements & release tracking, no-one else is allowed to sync with it
                        ArrayHelper.remove(targetNodes, syncTargetElement);
                        delete releasedIdMap[syncId];

                        syncTargetElement.isReleased = false;

                        if (strict || syncTargetElement.parentNode !== targetElement) {
                            targetElement.insertBefore(syncTargetElement, targetNodes[0] || null);
                        }
                    }
                    else if (strict) {
                        this.insertElement(sourceConfig, targetElement, targetNodes[0] || null, refOwner,
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

            while (targetNodes.length && !targetNode) {
                cleanupNode = targetNodes.shift();

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
                    this.insertTextNode(sourceConfig, targetElement, callback, refOwner, beforeNode);
                }
                else {
                    // Will append if beforeNode === null
                    this.insertElement(sourceConfig, targetElement, beforeNode, refOwner,
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
                        result            = me.performSync(syncChildOptions, targetNode);

                    // Remove reused element from release tracking
                    if (syncIdField && lastDomConfig?.dataset?.[syncIdField]) {
                        delete releasedIdMap[lastDomConfig.dataset[syncIdField]];
                    }

                    if (syncId != null) {
                        newSyncIdMap[syncId] = targetNode;
                    }

                    targetNode.isReleased = false;

                    // Only use callback if sync succeeded (anything changed)
                    result && callback && callback({
                        action        : 'reuseElement',
                        domConfig     : sourceConfig,
                        lastDomConfig,
                        targetElement : targetNode,
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
                        this.insertTextNode(sourceConfig, targetElement, callback, refOwner, targetNode);
                    }
                    else {
                        // Will insert
                        this.insertElement(sourceConfig, targetElement, targetNode, refOwner,
                            newSyncIdMap, syncId, syncChildOptions);
                    }

                    targetNode.remove();
                }
            }
        } // while (sourceConfigs.length)

        // Out of source nodes, remove remaining target nodes
        const numTargets = targetNodes.length;

        if (numTargets) {
            if (refOwner) {
                // Any remaining nodes that belong to this refOwner need to be cleaned up. If
                // they have an assigned reference, however, they will be handled at the very
                // end of the sync process since those elements can move in the node hierarchy.
                for (let i = 0; i < numTargets; ++i) {
                    const targetNode = targetNodes[i];

                    if (targetNode.$refOwnerId === refOwner.id && !targetNode.$reference) {
                        (cleanupNodes || (cleanupNodes = [])).push(targetNode);
                    }
                }
            }
            else if (cleanupNodes) {
                cleanupNodes.push(...targetNodes);
            }
            else {
                cleanupNodes = targetNodes;
            }
        }

        if (cleanupNodes) {
            this.syncChildrenCleanup(targetElement, cleanupNodes, newSyncIdMap, callback, refOwner,
                releaseThreshold, syncIdField);
        }
    }

    static syncChildrenCleanup(targetElement, cleanupNodes, newSyncIdMap, callback, refOwner, releaseThreshold, syncIdField) {
        let releaseCount = 0,
            ref;

        for (const targetNode of cleanupNodes) {
            const { lastDomConfig } = targetNode;

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

                        callback && callback({
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

                    callback && callback({
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
     */
    static getChild(element, path) {
        const syncIds = path.split('.');

        for (const id of syncIds) {
            element = element.syncIdMap[id];
        }

        return element;
    }

    //endregion
}
