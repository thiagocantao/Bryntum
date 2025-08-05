import Base from '../../../Core/Base.js';
import AjaxHelper from '../../../Core/helper/AjaxHelper.js';
import Objects from '../../../Core/helper/util/Objects.js';

/**
 * @module Scheduler/crud/transport/AjaxTransport
 */

/**
 * Implements data transferring functional that can be used for {@link Scheduler.crud.AbstractCrudManager} super classing.
 * Uses the fetch API for transport, https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API
 *
 * @example
 * // create a new CrudManager using AJAX as a transport system and JSON for encoding
 * class MyCrudManager extends AjaxTransport(JsonEncode(AbstractCrudManager)) {}
 *
 * @abstract
 * @mixin
 */
export default Target => class AjaxTransport extends (Target || Base) {
    static get $name() {
        return 'AjaxTransport';
    }

    /**
     * Configuration of the AJAX requests used by _Crud Manager_ to communicate with a server-side.
     *
     * ```javascript
     * transport : {
     *     load : {
     *         url       : 'http://mycool-server.com/load.php',
     *         // HTTP request parameter used to pass serialized "load"-requests
     *         paramName : 'data',
     *         // pass extra HTTP request parameter
     *         params    : {
     *             foo : 'bar'
     *         }
     *     },
     *     sync : {
     *         url     : 'http://mycool-server.com/sync.php',
     *         // specify Content-Type for requests
     *         headers : {
     *             'Content-Type' : 'application/json'
     *         }
     *     }
     * }
     *```
     * Since the class uses Fetch API you can use
     * any its [Request interface](https://developer.mozilla.org/en-US/docs/Web/API/Request) options:
     *
     * ```javascript
     * transport : {
     *     load : {
     *         url         : 'http://mycool-server.com/load.php',
     *         // HTTP request parameter used to pass serialized "load"-requests
     *         paramName   : 'data',
     *         // pass few Fetch API options
     *         method      : 'GET',
     *         credentials : 'include',
     *         cache       : 'no-cache'
     *     },
     *     sync : {
     *         url         : 'http://mycool-server.com/sync.php',
     *         // specify Content-Type for requests
     *         headers     : {
     *             'Content-Type' : 'application/json'
     *         },
     *         credentials : 'include'
     *     }
     * }
     *```
     *
     * An object where you can set the following possible properties:
     * @config {Object} transport
     * @property {Object} [transport.load] Load requests configuration:
     * @property {String} [transport.load.url] URL to request for data loading.
     * @property {String} [transport.load.method='GET'] HTTP method to be used for load requests.
     * @property {String} [transport.load.paramName='data'] Name of the parameter that will contain a serialized `load`
     * request. The value is mandatory for requests using `GET` method (default for `load`) so if the value is not
     * provided `data` string is used as default.
     * This value is optional for HTTP methods like `POST` and `PUT`, the request body will be used for data
     * transferring in these cases.
     * @property {Object} [transport.load.params] An object containing extra HTTP parameters to pass to the server when
     * sending a `load` request.
     *
     * ```javascript
     * transport : {
     *     load : {
     *         url       : 'http://mycool-server.com/load.php',
     *         // HTTP request parameter used to pass serialized "load"-requests
     *         paramName : 'data',
     *         // pass extra HTTP request parameter
     *         // so resulting URL will look like: http://mycool-server.com/load.php?userId=123456&data=...
     *         params    : {
     *             userId : '123456'
     *         }
     *     },
     *     ...
     * }
     * ```
     * @property {Object<String,String>} [transport.load.headers] An object containing headers to pass to each server request.
     *
     * ```javascript
     * transport : {
     *     load : {
     *         url       : 'http://mycool-server.com/load.php',
     *         // HTTP request parameter used to pass serialized "load"-requests
     *         paramName : 'data',
     *         // specify Content-Type for "load" requests
     *         headers   : {
     *             'Content-Type' : 'application/json'
     *         }
     *     },
     *     ...
     * }
     * ```
     * @property {Object} [transport.load.fetchOptions] **DEPRECATED:** Any Fetch API options can be simply defined on
     * the upper configuration level:
     * ```javascript
     * transport : {
     *     load : {
     *         url          : 'http://mycool-server.com/load.php',
     *         // HTTP request parameter used to pass serialized "load"-requests
     *         paramName    : 'data',
     *         // Fetch API options
     *         method       : 'GET',
     *         credentials  : 'include'
     *     },
     *     ...
     * }
     * ```
     * @property {Object} [transport.load.requestConfig] **DEPRECATED:** The config options can be defined on the upper
     * configuration level.
     * @property {Object} [transport.sync] Sync requests (`sync` in further text) configuration:
     * @property {String} [transport.sync.url] URL to request for `sync`.
     * @property {String} [transport.sync.method='POST'] HTTP request method to be used for `sync`.
     * @property {String} [transport.sync.paramName=undefined] Name of the parameter in which `sync` data will be
     * transferred. This value is optional for requests using methods like `POST` and `PUT`, the request body will be
     * used for data transferring in this case (default for `sync`). And the value is mandatory for requests using `GET`
     * method (if the value is not provided `data` string will be used as fallback).
     * @property {Object} [transport.sync.params] HTTP parameters to pass with an HTTP request handling `sync`.
     *
     * ```javascript
     * transport : {
     *     sync : {
     *         url    : 'http://mycool-server.com/sync.php',
     *         // extra HTTP request parameter
     *         params : {
     *             userId : '123456'
     *         }
     *     },
     *     ...
     * }
     * ```
     * @property {Object<String,String>} [transport.sync.headers] HTTP headers to pass with an HTTP request handling `sync`.
     *
     * ```javascript
     * transport : {
     *     sync : {
     *         url     : 'http://mycool-server.com/sync.php',
     *         // specify Content-Type for "sync" requests
     *         headers : {
     *             'Content-Type' : 'application/json'
     *         }
     *     },
     *     ...
     * }
     * ```
     * @property {Object} [transport.sync.fetchOptions] **DEPRECATED:** Any Fetch API options can be simply defined on
     * the upper configuration level:
     * ```javascript
     * transport : {
     *     sync : {
     *         url         : 'http://mycool-server.com/sync.php',
     *         credentials : 'include'
     *     },
     *     ...
     * }
     * ```
     * @property {Object} [transport.sync.requestConfig] **DEPRECATED:** The config options can be defined on the upper
     * configuration level.
     * @category CRUD
     */

    static get defaultMethod() {
        return {
            load : 'GET',
            sync : 'POST'
        };
    }

    /**
     * Cancels a sent request.
     * @param {Promise} requestPromise The Promise object wrapping the Request to be cancelled.
     * The _requestPromise_ is the value returned from the corresponding {@link #function-sendRequest} call.
     * @category CRUD
     */
    cancelRequest(requestPromise, reject) {
        requestPromise.abort?.();

        if (!this.isDestroying) {
            reject({ cancelled : true });
        }
    }

    shouldUseBodyForRequestData(packCfg, method, paramName) {
        return !(method === 'HEAD' || method === 'GET') && !paramName;
    }

    /**
     * Sends a _Crud Manager_ request to the server.
     * @param {Object} request The request configuration object having following properties:
     * @param {'load'|'sync'} request.type The request type. Either `load` or `sync`.
     * @param {String} request.url The URL for the request. Overrides the URL defined in the `transport` object
     * @param {String} request.data The encoded _Crud Manager_ request data.
     * @param {Object} request.params An object specifying extra HTTP params to send with the request.
     * @param {Function} request.success A function to be started on successful request transferring.
     * @param {String} request.success.rawResponse `Response` object returned by the [fetch api](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API).
     * @param {Function} request.failure A function to be started on request transfer failure.
     * @param {String} request.failure.rawResponse `Response` object returned by the [fetch api](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API).
     * @param {Object} request.thisObj `this` reference for the above `success` and `failure` functions.
     * @returns {Promise} The fetch Promise object.
     * @fires beforeSend
     * @async
     * @category CRUD
     */
    sendRequest(request) {
        const
            me              = this,
            { data }        = request,
            transportConfig = me.transport[request.type] || {},
            // clone parameters defined for this type of request
            requestConfig   = Objects.assign({}, transportConfig, transportConfig.requestConfig);

        if (request.url) {
            requestConfig.url = request.url;
        }

        requestConfig.method = requestConfig.method || AjaxTransport.defaultMethod[request.type];
        requestConfig.params = Objects.assign(requestConfig.params || {}, request.params);

        let { paramName } = requestConfig;

        // transfer package in the request body for some types of HTTP requests
        if (me.shouldUseBodyForRequestData(transportConfig, requestConfig.method, paramName)) {
            requestConfig.body = data;

            // for requests having body we set Content-Type to 'application/json' by default
            requestConfig.headers = requestConfig.headers || {};
            requestConfig.headers['Content-Type'] = requestConfig.headers['Content-Type'] || 'application/json';
        }
        else {
            // when we don't use body paramName is mandatory so fallback to 'data' as name
            paramName = paramName || 'data';

            requestConfig.params[paramName] = data;
        }

        if (!requestConfig.url) {
            throw new Error('Trying to request without URL specified');
        }

        // sanitize request config
        delete requestConfig.requestConfig;
        delete requestConfig.paramName;

        let ajaxPromise, resultPromise;

        function performSend() {
            // AjaxHelper.fetch call it "queryParams"
            requestConfig.queryParams = requestConfig.params;

            delete requestConfig.params;

            let cancelled = false;

            const fetchOptions = Objects.assign({}, requestConfig, requestConfig.fetchOptions);

            ajaxPromise  = AjaxHelper.fetch(requestConfig.url, fetchOptions);

            return ajaxPromise.catch(error => {
                ajaxPromise.done = true;

                me.trigger?.('responseReceived', { success : false });

                const signal = fetchOptions.abortController?.signal;

                if (signal) {
                    cancelled = signal.aborted;

                    if (!cancelled) {
                        console.warn(error);
                    }
                }

                return { error, cancelled };
            }).then(response => {
                ajaxPromise.done = true;

                me.trigger?.('responseReceived', { success : Boolean(response?.ok) });

                const callback = response?.ok ? request.success : request.failure;

                return callback?.call(request.thisObj || me, response, fetchOptions, request);
            });
        }

        /**
         * Fires before a request is sent to the server.
         *
         * ```javascript
         * crudManager.on('beforeSend', function ({ params, type }) {
         *     // let's set "sync" request parameters
         *     if (type == 'sync') {
         *         // dynamically depending on "flag" value
         *         if (flag) {
         *             params.foo = 'bar';
         *         }
         *         else {
         *             params.foo = 'smth';
         *         }
         *     }
         * });
         * ```
         * @event beforeSend
         * @param {Scheduler.crud.AbstractCrudManager} crudManager The CRUD manager.
         * @param {Object} params HTTP request params to be passed in the request URL.
         * @param {'sync'|'load'} requestType CrudManager request type (`load`/`sync`)
         * @param {Object} requestConfig Configuration object for Ajax request call
         * @async
         */
        const beforeSendResult = me.trigger('beforeSend', {
            params      : requestConfig.params,
            requestType : request.type,
            requestConfig,
            config      : request
        });

        if (Objects.isPromise(beforeSendResult)) {
            resultPromise = beforeSendResult.then(performSend);
        }
        else {
            resultPromise = performSend();
        }

        resultPromise.abort = () => {
            if (!ajaxPromise.done) {
                ajaxPromise.abort?.();
            }
        };

        return resultPromise;
    }
};
