// IMPORTANT - adding imports here can create problems for Base class
//  That is why this file was split from ObjectHelper

import StringHelper from '../StringHelper.js';

const
    { toString } = Object.prototype,
    { isFrozen } = Object,
    typeNameRe   = /\[object ([^\]]+)]/,
    typeCache    = {};

/**
 * @module Core/helper/util/Objects
 */

// Static methods are not displayed in derived class documentation. Therefore, since this is an internal class, the
// work around is to copy method documentation to ObjectHelper (the public interface). Also tried making ObjectHelper
// a singleton.

/**
 * Helper for low-level Object manipulation.
 *
 * While documented on {@link Core.helper.ObjectHelper}, the following static methods are implemented by this class:
 *
 * - `{@link Core.helper.ObjectHelper#function-assign-static}`
 * - `{@link Core.helper.ObjectHelper#function-assignIf-static}`
 * - `{@link Core.helper.ObjectHelper#function-clone-static}`
 * - `{@link Core.helper.ObjectHelper#function-createTruthyKeys-static}`
 * - `{@link Core.helper.ObjectHelper#function-getPath-static}`
 * - `{@link Core.helper.ObjectHelper#function-getTruthyKeys-static}`
 * - `{@link Core.helper.ObjectHelper#function-getTruthyValues-static}`
 * - `{@link Core.helper.ObjectHelper#function-isEmpty-static}`
 * - `{@link Core.helper.ObjectHelper#function-isObject-static}`
 * - `{@link Core.helper.ObjectHelper#function-merge-static}`
 * - `{@link Core.helper.ObjectHelper#function-setPath-static}`
 * - `{@link Core.helper.ObjectHelper#function-typeOf-static}`
 * @internal
 */
export default class Objects {
    static assign(dest, ...sources) {
        for (let source, key, i = 0; i < sources.length; i++) {
            source = sources[i];

            if (source) {
                for (key in source) {
                    dest[key] = source[key];
                }
            }
        }

        return dest;
    }

    static assignIf(dest, ...sources) {
        for (let source, key, i = 0; i < sources.length; i++) {
            source = sources[i];

            if (source) {
                for (key in source) {
                    if (!(key in dest) || dest[key] === undefined) {
                        dest[key] = source[key];
                    }
                }
            }
        }

        return dest;
    }

    static clone(value, handler) {
        let cloned = value,
            key;

        if (value && typeof value === 'object') {
            if (O.isObject(value)) {
                cloned = {};

                for (key in value) {
                    cloned[key] = O.clone(value[key]);
                }
            }
            else if (Array.isArray(value)) {
                cloned = [];

                // Loop backwards to:
                //  1. read source.length once
                //  2. get result array sized on first pass (avoid growing)
                for (key = value.length; key-- > 0; /* empty */) {
                    cloned[key] = O.clone(value[key]);
                }
            }
            else if (O.isDate(value)) {
                cloned = new Date(value.getTime());
            }
            else if (handler) {
                // Allow other types to be handled (e.g., DOM nodes)
                cloned = handler(value);
            }
        }

        return cloned;
    }

    static createTruthyKeys(source) {
        const
            keys = StringHelper.split(source),
            result = keys && {};

        if (keys) {
            for (const key of keys) {
                // StringHelper.split won't return empty keys if passed a string, but we
                // could have been passed a String[]
                if (key) {
                    result[key] = true;
                }
            }
        }

        return result;
    }

    /**
     * Returns value for a given path in the object
     * @param {Object} object Object to check path on
     * @param {String} path Dot-separated path, e.g. 'object.childObject.someKey'
     * @returns {*} Value associated with passed key
     */
    static getPath(object, path) {
        return path.split('.').reduce((result, key) => {
            return (result || {})[key];
        }, object);
    }

    /**
     * Determines if the specified path exists
     * @param {Object} object Object to check path on
     * @param {String} path Dot-separated path, e.g. 'object.childObject.someKey'
     * @returns {Boolean}
     */
    static hasPath(object, path) {
        return path.split('.').every(key => {
            if (key in object) {
                object = object[key];
                return true;
            }
            return false;
        });
    }

    static getTruthyKeys(source) {
        const keys = Object.keys(source);

        for (let i = keys.length; i-- > 0; /* empty */) {
            if (!source[keys[i]]) {
                keys.splice(i, 1);
            }
        }

        return keys;
    }

    static getTruthyValues(source) {
        const keys = Object.keys(source);

        for (let i = keys.length; i-- > 0; /* empty */) {
            if (source[keys[i]]) {
                keys[i] = source[keys[i]];
            }
            else {
                keys.splice(i, 1);
            }
        }

        return keys;
    }

    static isClass(object) {
        if (typeof object === 'function' && object.prototype?.constructor === object) {
            // TODO find a way to differentiate function(){} from class{}
            return true;
        }

        return false;
    }

    static isDate(object) {
        // A couple quick rejections but only sure way is typeOf:
        return Boolean(object && object.getUTCDate) && O.typeOf(object) === 'date';
    }

    static isPromise(object) {
        if (Promise && Promise.resolve) {
            return Promise.resolve(object) === object;
        } else {
            throw new Error('Promise not supported in your environment');
        }
    }

    static isEmpty(object) {
        if (object && typeof object === 'object') {
            // noinspection LoopStatementThatDoesntLoopJS
            for (const p in object) { // eslint-disable-line no-unused-vars
                return false;
            }
        }

        return true;
    }

    static isObject(value) {
        const C = value && value.constructor;

        return Boolean(C
            // Most things have a .constructor property
            ? (
                // An in-frame instance of Object
                C === Object ||
                // Detect cross-frame objects, but exclude instance of custom classes named Object. typeOf(value) is
                // "object" even for instances of a class and typeOf(C) is "function" for all constructors. We'll have
                // to settle for relying on the fact that getPrototypeOf(Object.prototype) === null.
                // NOTE: this issue does come up in Scheduler unit tests at least.
                // NOTE: IE11 does not have C.name === 'Object' for cross-frame objects! So instead of that check we
                //  do a presence check for a method unlikely to be found elsewhere.
                (C.getPrototypeOf && C.prototype && !Object.getPrototypeOf(C.prototype))
            )
            // Since all classes have a constructor property, an object w/o one is likely from Object.create(null). Of
            // course, primitive types do not have ".constructor"
            : (value && typeof value === 'object')
        );
    }

    static merge(dest, ...sources) {
        for (let destValue, source, key, value, i = 0; i < sources.length; i++) {
            source = sources[i];

            if (source) {
                for (key in source) {
                    value = source[key];

                    if (value && O.isObject(value)) {
                        destValue = dest[key];

                        if (destValue && O.isObject(destValue)) {
                            if (isFrozen(destValue)) {
                                dest[key] = destValue = O.clone(destValue);
                            }

                            O.merge(destValue, value);
                        }
                        else {
                            dest[key] = isFrozen(value) ? value : O.clone(value);
                        }
                    }
                    else {
                        dest[key] = value;
                    }
                }
            }
        }

        return dest;
    }

    /**
     * Sets value for a given path in the object
     * @param {Object} object Target object
     * @param {String} path Dot-separated path, e.g. 'object.childObject.someKey'
     * @param {*} value Value for a given path
     * @returns {Object} Returns passed object
     */
    static setPath(object, path, value) {
        path.split('.').reduce((result, key, index, array) => {
            const isLast = index === array.length - 1;

            if (isLast) {
                return result[key] = value;
            }
            else if (!(result[key] instanceof Object)) {
                result[key] = {};
            }

            return result[key];
        }, object);

        return object;
    }

    static typeOf(value) {
        let type = typeof value,
            match, trueType;

        // If not atomic type, we handle date or null
        if (type === 'object') {
            if (value === null) {
                type = 'null';
            }
            else {
                trueType = toString.call(value);

                if (!(type = typeCache[trueType])) {
                    match = typeNameRe.exec(trueType);
                    typeCache[trueType] = type = match ? match[1].toLowerCase() : trueType;
                }
            }
        }
        // NaN is the only value that is !== to itself
        else if (value !== value) { // eslint-disable-line no-self-compare
            type = 'nan';
        }

        return type;
    }
}

const O = Objects;
