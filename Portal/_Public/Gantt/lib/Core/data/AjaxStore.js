import Store from './Store.js';
import AjaxHelper from '../helper/AjaxHelper.js';
import ObjectHelper from '../helper/ObjectHelper.js';

/**
 * @module Core/data/AjaxStore
 */

/**
 * Http methods used by the AjaxStore in restful mode.
 * @typedef {Object} HttpMethods
 * @property {'POST'|'PUT'} create
 * @property {'GET'|'POST'} read
 * @property {'PATCH'|'POST'|'PUT'} update
 * @property {'DELETE'|'POST'} delete
 */

const
    processParamEntry = (paramArray, entry) => {
        if (Array.isArray(entry[1])) {
            entry[1].forEach(value => paramArray.push(encodeURIComponent(entry[0]) + '=' + encodeURIComponent(value)));
        }
        else {
            paramArray.push(encodeURIComponent(entry[0]) + '=' + encodeURIComponent(entry[1]));
        }

        return paramArray;
    },
    immediatePromise  = Promise.resolve(),
    prependZeros = num => {
        return String(num).padStart(2, '0');
    },
    dateToString = date => {
        return `${ date.getFullYear() }-${ prependZeros(date.getMonth() + 1) }-${ prependZeros(date.getDate()) }T${ prependZeros(date.getHours()) }:${ prependZeros(date.getMinutes()) }:${ prependZeros(date.getSeconds()) }.${ date.getMilliseconds() }`;
    };

/**
 * Store that uses the [Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch) to read data
 * from a remote server, and optionally sends synchronization requests to the server containing information about
 * locally created, modified and deleted records.
 *
 * ## Create
 * Posts array of JSON data for newly added records to {@link #config-createUrl}, expects response containing an array
 * of JSON objects in same order with id set (uses Model#idField as id).
 *
 * ## Read
 * Reads array of JSON data from the data packet returned from the {@link #config-readUrl}. Unique id for each row is
 * required.
 *
 * By default looks in field 'id' but can be configured by setting {@link Core.data.Model#property-idField-static}.
 *
 * ## Update
 * Posts array of JSON data containing modified records to {@link #config-updateUrl}. By default, only changed fields
 * and any fields configured with {@link Core.data.field.DataField#config-alwaysWrite} are sent.
 * If you want all fields to always be sent, please see {@link #config-writeAllFields}
 *
 * ## Delete
 * Posts to {@link #config-deleteUrl} with removed records ids (for example id=1,4,7).
 *
 * ```javascript
 * new AjaxStore({
 *   createUrl  : 'php/create',
 *   readUrl    : 'php/read',
 *   updateUrl  : 'php/update',
 *   deleteUrl  : 'php/delete',
 *   modelClass : Customer
 * });
 * ```
 *
 * ## Pagination
 * Configuring an `AjaxStore` with {@link #config-pageParamName} or {@link #config-pageStartParamName} means that the
 * store requests **pages** of data from the remote source, sending the configured {@link #config-pageParamName} or
 * {@link #config-pageStartParamName} to request the page along with the {@link #config-pageSizeParamName}.
 *
 * If `pageParamName` is set, that is passed with the requested page number **(one based)**, along with the
 * {@link #config-pageSizeParamName}.
 *
 * If `pageStartParamName` is set, that is passed with the requested page starting record index **(zero based)**, along
 * with the {@link #config-pageSizeParamName}.
 *
 * ## Remote filtering
 * To specify that filtering is the responsibility of the server, configure the store with
 * `{@link #config-filterParamName}: 'nameOfFilterParameter'`
 *
 * When this is set, any {@link Core.data.mixin.StoreFilter#function-filter} operation causes the store to reload
 * itself, encoding the filters as JSON representations in the {@link #config-filterParamName} HTTP parameter.
 *
 * The filters will look like this:
 * ```javascript
 * {
 *     "field": "country",
 *     "operator": "=",
 *     "value": "sweden",
 *     "caseSensitive": false
 * }
 * ```
 *
 * If the value of the filter is a date - it is serialized as a local time, using the format: `YYYY-MM-DDThh:mm:ss.ms`
 *
 * The encoding may be overridden by configuring an implementation of {@link #function-encodeFilterParams}
 * into the store which returns the value for the {@link #config-filterParamName} when passed an _Iterable_ of filters.
 *
 * ## Remote sorting
 * To specify that sorting is the responsibility of the server, configure the store with
 * `{@link #config-sortParamName}: 'nameOfSortParameter'`
 *
 * When this is set, any {@link Core.data.mixin.StoreSort#function-sort} operation causes the store to
 * reload itself, encoding the sorters as JSON representations in the {@link #config-sortParamName} HTTP
 * parameter.
 *
 * The sorters will look like this:
 * ```javascript
 * {
 *     "field": "name",
 *     "ascending": true
 * }
 * ```
 *
 * The encoding may be overridden by configuring an implementation of {@link #function-encodeSorterParams}
 * into the store which returns the value for the {@link #config-sortParamName} when passed an _Iterable_ of sorters.
 *
 * ## Passing HTTP headers
 * As mentioned above `AjaxStore` uses the Fetch API under the hood. Specify {@link #config-fetchOptions} and/or
 * {@link #config-headers} to have control over the options passed with all fetch calls. For example to pass along an
 * authorization header:
 *
 * ```javascript
 * const store = new AjaxStore({
 *    headers : {
 *        Authorization : 'auth-contents-goes-here'
 *    }
 * });
 * ```
 *
 * Learn more about the Fetch API over at [MDN](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch).
 *
 * @extends Core/data/Store
 */
export default class AjaxStore extends Store {

    static $name = 'AjaxStore';

    // region Events

    /**
     * Fired when a remote request fails, either at the network level, or the server returns a failure, or an invalid
     * response.
     *
     * Note that when a {@link #function-commit} fails, more than one exception event will be triggered. The individual
     * operation, `create`, `update` or `delete` will trigger their own `exception` event, but the encapsulating commit
     * operation will also trigger an `exception` event when all the operations have finished, so if exceptions are
     * going to be handled gracefully, the event's `action` property must be examined, and the constituent operations of
     * the event must be examined.
     * @event exception
     * @param {Core.data.Store} source This Store
     * @param {Boolean} exception `true`
     * @param {'create'|'read'|'update'|'delete'|'commit'} action Action that failed, `'create'`, `'read'`,
     * `'update'` or `'delete'`. May also be fired with '`commit'` to indicate the failure of an aggregated `create`,
     * `update` and `delete` operation. In this case, the event will contain a property for each operation of the commit
     * named `'create'`, `'update'` and `'delete'`, each containing the individual `exception` events.
     * @param {'network'|'failure'} exceptionType The type of failure, `'network'` or `'server'`
     * @param {Response} response the `Response` object
     * @param {Object} json The decoded response object *if the exceptionType is `'server'`*
     */

    /**
     * Fired after committing added records
     * @event commitAdded
     * @param {Core.data.Store} source This Store
     */

    /**
     * Fired after committing modified records
     * @event commitModified
     * @param {Core.data.Store} source This Store
     */

    /**
     * Fired on successful load
     * @event load
     * @param {Core.data.Store} source This Store
     * @param {Object[]} data Data loaded
     * @param {Response} response the `Response` object
     * @param {Object} json The decoded response object.
     */

    /**
     * Fired on successful load of remote child nodes for a tree node.
     * @event loadChildren
     * @param {Core.data.Store} source This Store
     * @param {Object[]} data Data loaded
     * @param {Object} json The decoded response object.
     */

    /**
     * Fired after committing removed records
     * @event commitRemoved
     * @param {Core.data.Store} source This Store
     */

    /**
     * Fired before loading starts. Allows altering parameters and is cancelable
     * @event beforeLoad
     * @preventable
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax request.
     */

    /**
     * Fired before loading of remote child nodes of a tree node starts. Allows altering parameters and is cancelable
     * @event beforeLoadChildren
     * @preventable
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax request.
     */

    /**
     * When the store {@link #property-isPaged is paged}, this is fired before loading a page and is cancelable
     * @event beforeLoadPage
     * @preventable
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax request.
     */

    /**
     * Fired when loading is beginning. This is not cancelable. Parameters in the event may still be
     * mutated at this stage.
     * @event loadStart
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax request.
     */

    /**
     * Fired when loading of remote child nodes into a tree node is beginning. This is not cancelable. Parameters in the
     * event may still be mutated at this stage.
     * @event loadChildrenStart
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax request.
     */

    /**
     * Fired before any remote request is initiated.
     * @event beforeRequest
     * @param {Core.data.Store} source This Store
     * @param {Object} params An object containing key/value pairs that are passed on the request query string
     * @param {Object} body The body of the request to be posted to the server.
     * @param {'create'|'read'|'update'|'delete'} action Action that is making the request, `'create'`,
     * `'read'`, `'update'` or `'delete'`
     */

    /**
     * Fired after any remote request has finished whether successfully or unsuccessfully.
     * @event afterRequest
     * @param {Boolean} exception `true`. *Only present if the request triggered an exception.*
     * @param {'create'|'read'|'update'|'delete'} action Action that has finished, `'create'`, `'read'`,
     * `'update'` or `'delete'`
     * @param {'network'|'failure'} exceptionType The type of failure, `'network'` or `'server'`. *Only present
     * if the request triggered an exception.*
     * @param {Response} response The `Response` object
     * @param {Object} json The decoded response object if there was no `'network'` exception.
     */

    // endregion

    //region Config

    static get defaultConfig() {
        return {
            /**
             * A string keyed object containing the HTTP headers to add to each server request issued by this store.
             *
             * `AjaxStore` uses the Fetch API under the hood, read more about headers on
             * [MDN](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#headers)
             *
             * Example usage:
             *
             * ```javascript
             * const store = new AjaxStore({
             *    headers : {
             *        Authorization : 'auth-contents-goes-here'
             *    }
             * });
             * ```
             *
             * @config {Object<String,String>}
             * @category Remote
             */
            headers : null,

            /**
             * An object containing the Fetch options to pass to each server request issued by this store. Use this to
             * control if credentials are sent and other options, read more at
             * [MDN](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#supplying_request_options).
             *
             * Example usage:
             *
             * ```javascript
             * const store = new AjaxStore({
             *    fetchOptions : {
             *        credentials : 'omit',
             *        redirect    : 'error'
             *    }
             * });
             * ```
             *
             * @config {Object}
             * @category Remote
             */
            fetchOptions : null,

            /**
             * Specify `true` to send payloads as form data, `false` to send as regular JSON.
             * @config {Boolean}
             * @default false
             * @category Remote
             */
            sendAsFormData : null,

            /**
             * Specify `true` to send all model fields when committing modified records (as opposed to just the
             * modified fields)
             * @config {Boolean}
             * @default false
             * @category Remote
             */
            writeAllFields : null,

            /**
             * The name of the HTTP parameter passed to this Store's {@link #config-readUrl} to indicate the node `id`
             * to load when loading child nodes on demand if the node being expanded was created with data containing
             * `children: true`.
             * @config {String}
             * @default
             * @category Remote
             */
            parentIdParamName : 'id',

            /**
             * The optional property name in JSON responses from the server that contains a boolean
             * success/fail status.
             * ```json
             * {
             *   "responseMeta" : {
             *   {
             *     "success" : true,
             *     "count" : 100
             *   },
             *   // The property name used here should match that of 'responseDataProperty'
             *   "data" : [
             *     ...
             *   ]
             * }
             * ```
             *
             * The store would be configured with:
             * ```javascript
             *  {
             *      ...
             *      successDataProperty : 'responseMeta.success',
             *      responseTotalProperty : 'responseMeta.count'
             *      ...
             *  }
             *
             * ```
             * @config {String}
             * @default
             * @category Remote
             */
            responseSuccessProperty : 'success',

            /**
             * The property name in JSON responses from the server that contains the data for the records
             * ```json
             * {
             *   "success" : true,
             *   // The property name used here should match that of 'responseDataProperty'
             *   "data" : [
             *     ...
             *   ]
             * }
             * ```
             * @config {String}
             * @default
             * @category Remote
             */
            responseDataProperty : 'data',

            /**
             * The property name in JSON responses from the server that contains the dataset total size
             * **when this store {@link #property-isPaged is paged}**
             * ```json
             * {
             *   "success" : true,
             *   // The property name used here should match that of 'responseDataProperty'
             *   "data" : [
             *     ...
             *   ],
             *   // The property name used here should match that of 'responseTotalProperty'
             *   "total" : 65535
             * }
             * ```
             * @config {String}
             * @default
             * @category Remote
             */
            responseTotalProperty : 'total',

            /**
             * The name of the HTTP parameter to use to pass any encoded filters when loading data from the server and a
             * filtered response is required.
             *
             * **Note:** When this is set, filters must be defined using a field name, an operator and a value
             * to compare, **not** a comparison function.
             * @config {String}
             * @category Remote
             */
            filterParamName : null,

            /**
             * Set this flag to true if you are filtering remote using restful URLs (e.g.
             * https://nominatim.openstreetmap.org/search/paris?format=json)
             *
             * **Note:** When this is set, the filter string is appended to the readUrl.
             * @config {Boolean}
             * @category Remote
             */
            restfulFilter : false,

            /**
             * The name of the HTTP parameter to use to pass any encoded sorters when loading data from the server and a
             * sorted response is required.
             *
             * **Note:** When this is set, sorters must be defined using a field name and an ascending flag,
             * **not** a sort function.
             * @config {String}
             * @category Remote
             */
            sortParamName : null,

            /**
             * The name of the HTTP parameter to use when requesting pages of data using the **one based** page number
             * required.
             * @config {String}
             * @category Paging
             */
            pageParamName : null,

            /**
             * The name of the HTTP parameter to use when requesting pages of data using the **zero based** index of the
             * required page's starting record.
             * @config {String}
             * @category Paging
             */
            pageStartParamName : null,

            /**
             * The name of the HTTP parameter to use when requesting pages of data using the **zero based** index of the
             * required page's starting record.
             * @config {String}
             * @default
             * @category Paging
             */
            pageSizeParamName : 'pageSize',

            /**
             * When paging of data is requested by setting _either_ the {@link #config-pageParamName} _or_ the
             * {@link #config-pageStartParamName}, this is the value to send in the {@link #config-pageSizeParamName}.
             * @config {Number}
             * @default
             * @category Paging
             */
            pageSize : 50,

            /**
             * Set to ´true´ to use restful {@link #config-httpMethods}
             * @config {Boolean}
             * @default false
             * @category Remote
             */
            useRestfulMethods : null,

            /**
             * The HTTP methods to use for CRUD requests when {@link #config-useRestfulMethods} is enabled.
             *
             * ```javascript
             * new AjaxStore({
             *    useRestfulMethods : true,
             *    httpMethods : {
             *        create : 'POST',
             *        read   : 'POST',
             *        update : 'PATCH',
             *        delete : 'DELETE'
             *    }
             * });
             *
             * ```
             * @config {HttpMethods}
             * @default
             * @category Remote
             */
            httpMethods : {
                create : 'POST',
                read   : 'GET',
                update : 'PUT',
                delete : 'DELETE'
            }
        };
    }

    static get configurable() {
        return {
            /**
             * An object containing key/value pairs that are passed on the request query string.
             * @member {Object} params
             * @category Remote
             */
            /**
             * An object containing key/value pairs that are passed on the request query string.
             * @config {Object}
             * @category Remote
             */
            params : null
        };
    }

    /**
     * Url to post newly created records to.
     *
     * The response must be in the form:
     *
     *     {
     *         "success": true,
     *         "data": [{
     *             "id": 0, "name": "General Motors"
     *         }, {
     *             "id": 1, "name": "Apple"
     *         }]
     *     }
     *
     * Just the array of data may be returned, however that precludes the orderly handling of errors encountered at the
     * server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler in the `response` property of the
     * event.
     *
     * The `success` property may be omitted, it defaults to `true`.
     *
     * @prp {String} createUrl
     * @category CRUD
     */

    /**
     * Url to read data from.
     *
     * The response must be in the form:
     *
     *     {
     *         "success": true,
     *         "data": [{
     *             "id": 0, "name": "General Motors"
     *         }, {
     *             "id": 1, "name": "Apple"
     *         }]
     *     }
     *
     * If the store {@link #property-isPaged is paged}, the total dataset size must be returned in the
     * {@link #config-responseTotalProperty} property:
     *
     *     {
     *         "success": true,
     *         "data": [{
     *             "id": 0, "name": "General Motors"
     *         }, {
     *             "id": 1, "name": "Apple"
     *         }],
     *         "total": 65535
     *     }
     *
     * Just the array of data may be returned, however that precludes the orderly handling of errors encountered at the
     * server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler in the `response` property of the
     * event.
     *
     * The `success` property may be omitted, it defaults to `true`.
     *
     * @prp {String} readUrl
     * @category CRUD
     */

    /**
     * Url to post record modifications to.
     *
     * The response must be in the form:
     *
     *     {
     *         "success": true,
     *         "data": [{
     *             "id": 0, "name": "General Motors"
     *         }, {
     *             "id": 1, "name": "Apple"
     *         }]
     *     }
     *
     * Just the array of data may be returned, however that precludes the orderly handling of errors encountered at the
     * server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler in the `response` property of the
     * event.
     *
     * The `success` property may be omitted, it defaults to `true`.
     *
     * @prp {String} updateUrl
     * @category CRUD
     */

    /**
     * Url for deleting records.
     *
     * The response must be in the form:
     *
     *     {
     *         "success": true
     *     }
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler in the `response` property of the
     * event.
     *
     * The `success` property may be omitted, it defaults to `true`.
     *
     * @prp {String} deleteUrl
     * @category CRUD
     */

    /**
     * True to initiate a load when the store is instantiated
     * @config {Boolean} autoLoad
     * @category Common
     */

    //endregion

    afterConstruct(config) {
        super.afterConstruct(config);

        if (this.autoLoad) {
            this.load().catch(() => {});
        }
    }

    /**
     * Returns a truthy value if the Store is currently loading.
     *
     * A load operation is initiated by a load call, but the network request is not sent until
     * after a delay until the next event loop because of allowing all operations which may
     * request a load to coalesce into one call.
     *
     * If the loading request is in this waiting state, the value will be `1`,
     *
     * If the network request is in flight, the value will be `2`
     * @property {Boolean|Number}
     * @readonly
     * @category CRUD
     */
    get isLoading() {
        return this._isLoading ? 2 : this.loadTriggerPromise ? 1 : false;
    }

    /**
     * Returns true if the Store is currently committing
     * @property {Boolean}
     * @readonly
     * @category CRUD
     */
    get isCommitting() {
        return Boolean(this.commitPromise);
    }

    set pageParamName(pageParamName) {
        if (this.tree) {
            throw new Error('Paging cannot be supported for tree stores');
        }
        if (this.pageStartParamName) {
            throw new Error('Configs pageStartParamName and pageParamName are mutually exclusive');
        }
        this._pageParamName = pageParamName;
    }

    get pageParamName() {
        return this._pageParamName;
    }

    set pageStartParamName(pageStartParamName) {
        if (this.tree) {
            throw new Error('Paging cannot be supported for tree stores');
        }
        if (this.pageParamName) {
            throw new Error('Configs pageParamName and pageStartParamName are mutually exclusive');
        }
        this._pageStartParamName = pageStartParamName;
    }

    get pageStartParamName() {
        return this._pageStartParamName;
    }

    /**
     * Yields true if this Store is loaded page by page. This yields `true` if either of the
     * {@link #config-pageParamName} of {@link #config-pageStartParamName} configs are set.
     * @property {Boolean}
     * @readonly
     * @category Paging
     */
    get isPaged() {
        return this.pageParamName || this.pageStartParamName;
    }

    /**
     * Yields the complete dataset size. If the store is {@link #property-isPaged is paged} this is the value
     * returned in the last loaded data block in the {@link #config-responseTotalProperty} property. Otherwise it is
     * the number of records in the store's underlying storage collection.
     * @property {Number}
     * @readonly
     * @category Paging
     */
    get allCount() {
        return ('remoteTotal' in this) ? this.remoteTotal : super.allCount;
    }

    /**
     * **If the store {@link #property-isPaged is paged}**, yields the highest page number in the dataset as calculated
     * from the {@link #config-responseTotalProperty}
     * returned in the last page data block loaded.
     * @property {Number}
     * @readonly
     * @category Paging
     */
    get lastPage() {
        if (this.isPaged) {
            return Math.floor((this.allCount + this.pageSize - 1) / this.pageSize);
        }
    }

    buildQueryString(url, ...paramObjects) {
        const
            hasParamsInUrl = url.includes('?'),
            queryString    = Object.entries(Object.assign({}, ...paramObjects)).reduce(processParamEntry, []).join('&');

        return queryString ? (hasParamsInUrl ? '&' : '?') + queryString : '';
    }

    /**
     * Internal sort method.
     * Should not be used in application code directly.
     * @param silent
     * @returns {Promise|null} If {@link Core/data/AjaxStore#config-sortParamName} is set on store, this method returns `Promise`
     * which is resolved after data is loaded from remote server, otherwise it returns `null`
     * @async
     * @internal
     */
    async performSort(silent) {
        const me = this;
        if (me.remoteSort && !me.isRemoteDataLoading) {
            me.isRemoteDataLoading = true;
            const result           = await me.internalLoad({}, '', event => {
                me.data = event.data;
                me.afterPerformSort(silent);
            });
            me.isRemoteDataLoading = false;
            return result;
        }
        else {
            super.performSort(silent);
        }
    }

    /**
     * Internal filter method.
     * Should not be used in application code directly.
     * @param silent
     * @returns {Promise|null} If {@link Core/data/AjaxStore#config-filterParamName} is set on store, this method returns `Promise`
     * which is resolved after data is loaded from remote server, otherwise it returns `null`
     * @async
     * @internal
     */
    async performFilter(silent) {
        const
            me = this;

        // For remote filtering, the dataset cannot be preserved. The size may be completely different.
        // This is a reload operation.
        if (me.remoteFilter) {
            me.loadingPromise?.abort();
            // Flag store data loading state to not get into loop when data is sorted internally after request
            me.isRemoteDataLoading = true;

            const
                oldCount    = me.count,
                { filters } = me;

            // load should default to page 1
            me.currentPage         = 1;
            const result           = await me.internalLoad({}, '', event => {
                me.data = event.data;
                event   = silent
                    ? null
                    : {
                        action  : 'filter',
                        filters,
                        oldCount,
                        records : me.storage.values
                    };

                me.afterPerformFilter(event);
                me.trigger('refresh', event);
            });
            me.isRemoteDataLoading = false;
            return result;
        }
        else {
            super.performFilter(silent);
        }
    }

    /**
     * A provided function which creates an array of values for the {@link #config-filterParamName} to pass
     * any filters to the server upon load.
     *
     * By default, this creates a JSON string containing the following properties:
     *
     * ```javascript
     *    [{
     *        field         : <theFieldName>
     *        operator      : May be: `'='`, `'!='`, `'>'`, `'>='`, `'<'`, `'<='`, `'*'`, `'startsWith'`, `'endsWith'`
     *        value         : The value to compare
     *        caseSensitive : true for case sensitive comparisons
     *    }]
     * ```
     * @param {Core.util.CollectionFilter[]} filters The filters to encode.
     */
    encodeFilterParams(filters) {
        const
            result = [];

        for (const { property, operator, value, caseSensitive } of filters) {
            result.push({
                field : property,
                operator,
                value,
                caseSensitive
            });
        }

        return JSON.stringify(result, function(key, value) {
            return key === ''
                ? value
                : this[key] instanceof Date ? dateToString(this[key]) : value;
        });
    }

    /**
     * A provided function which creates an array of values for the {#config-sortParamName} to pass
     * any sorters to the server upon load.
     *
     * By default, this creates a JSON string containing the following properties:
     *
     * ```javascript
     *    [{
     *        field     : <theFieldName>
     *        ascending : true/false
     *    }]
     * ```
     *
     * @param {Sorter[]} sorters The sorters to encode.
     */
    encodeSorterParams(sorters) {
        return JSON.stringify(sorters.filter(sorter => !sorter.sortFn).map(sorter => sorter));
    }

    buildReadUrl() {
        const { readUrl } = this;
        if (this.restfulFilter && this.filters.count) {
            const url = readUrl.endsWith('/') ? readUrl : (readUrl + '/');
            return url + this.filters.first.value;
        }
        return readUrl;
    }

    /**
     * Internal data loading method.
     * @returns {Promise}
     * @internal
     */
    internalLoad(params, eventName, successFn, delay = 0) {
        // Accumulate all configured parameters
        params = ObjectHelper.assign({}, this.params, params);

        const
            me    = this,
            url   = me.buildReadUrl(),
            event = { action : 'read' + eventName, params, url };

        if (!url) {
            throw new Error('No load url specified');
        }

        if (me.trigger('beforeLoad' + eventName, event) === false) {
            throw false;  // eslint-disable-line no-throw-literal
        }

        me.loadArgs = [url, event, params, eventName, successFn];

        if (delay === false) {
            return new Promise((resolve, reject) => me.sendLoadRequest(resolve, reject));
        }

        return me.loadTriggerPromise || (me.loadTriggerPromise = new Promise((resolve, reject) => {
            me.setTimeout({
                delay,
                fn                : 'sendLoadRequest',
                args              : [resolve, reject],
                cancelOutstanding : true
            });
        }));
    }

    // Send the request for the internalLoad.
    // This is called on a timeout 1ms after the internalLoad call.
    async sendLoadRequest(resolve, reject) {
        const
            me = this,
            [
                url,
                event,
                params,
                eventName,
                successFn
            ]  = me.loadArgs;

        // As soon as it kicks off, new load requests can be made which will result in another load
        me.loadTriggerPromise = null;
        if (url) {
            me._isLoading = true;

            // This may look redundant, but it allows for two levels of event listening.
            // Granular, where the observer observes only the events of interest, and
            // catch-all, where the observer is interested in all requests.
            me.trigger(`load${eventName}Start`, event);
            me.trigger('beforeRequest', event);

            // Add filter information to the request parameters
            if (me.filterParamName && me.isFiltered) {
                params[me.filterParamName] = me.encodeFilterParams(me.filters.values);
            }

            // Add sorter information to the request parameters.
            // isSorted includes grouping in its evaluation.
            if (me.remoteSort && me.isSorted) {
                params[me.sortParamName] = me.encodeSorterParams(me.groupers ? me.groupers.concat(me.sorters) : me.sorters);
            }

            // Ensure our next page is passed to the server in the params if not already set.
            // Ensure our page size is always passed.
            if (me.isPaged) {
                if (!((me.pageParamName in params) || (me.pageStartParamName in params))) {
                    const
                        page = Math.min(me.currentPage || 1, me.allCount ? me.lastPage : Infinity);

                    if (me.pageParamName) {
                        params[me.pageParamName] = page;
                    }
                    else {
                        params[me.pageStartParamName] = (page - 1) * me.pageSize;
                    }
                }
                params[me.pageSizeParamName] = me.pageSize;
            }

            const options = { headers : me.headers, parseJson : true };

            if (me.useRestfulMethods) {
                options.method = me.httpMethods.read;
                // user might define body in case of using custom restful method
                if (event.body) {
                    options.body = JSON.stringify(event.body);
                }
            }

            try {
                const
                    promise     = me.loadingPromise = AjaxHelper.get(event.url + me.buildQueryString(event.url, params), ObjectHelper.assign(options, me.fetchOptions)),
                    response    = await promise,
                    data        = response.parsedJson,
                    isArray     = Array.isArray(data),
                    success     = isArray || (data && (ObjectHelper.getPath(data, me.responseSuccessProperty) !== false)),
                    remoteTotal = isArray ? null : ObjectHelper.getPath(data, me.responseTotalProperty);

                if (me.isDestroyed) {
                    return;
                }
                me.loadingPromise = null;
                me._isLoading  = false;
                event.response = response;
                event.json     = data;

                if (success) {
                    if (remoteTotal != null) {
                        me.remoteTotal = parseInt(remoteTotal, 10);
                    }

                    // If we are issuing paged requests, work out what page we are on based
                    // on the requested page and the size of the dataset declared.
                    if (me.isPaged) {
                        if (me.remoteTotal >= 0) {
                            const requestedPage = me.pageParamName ? params[me.pageParamName] : params[me.pageStartParamName] / me.pageSize + 1;
                            me.currentPage      = Math.min(requestedPage, me.lastPage);
                        }
                        else {
                            throw new Error('A paged store must receive its responseTotalProperty in each data packet');
                        }
                    }
                    event.data = isArray ? data : ObjectHelper.getPath(data, me.responseDataProperty);
                    await successFn(event);
                    !me.isDestroyed && me.trigger('load' + eventName, event);
                    resolve(event);
                }
                else {
                    Object.assign(event, {
                        exception     : true,
                        exceptionType : 'server',
                        error         : data?.error
                    });
                    !me.isDestroyed && me.trigger('exception', event);
                    reject(event);
                }

                // finally
                !me.isDestroyed && me.trigger('afterRequest', event);
            }
            catch (responseOrError) {
                me._isLoading = false;

                event.exception = true;

                if (responseOrError instanceof Response) {
                    event.exceptionType = responseOrError.ok ? 'server' : 'network';
                    event.response      = responseOrError;
                    event.error         = responseOrError.error;
                }
                else {
                    event.exceptionType = 'server';
                    event.error         = responseOrError;
                }

                !me.isDestroyed && me.trigger('exception', event);
                reject(event);

                // finally
                !me.isDestroyed && me.trigger('afterRequest', event);
            }
        }
    }

    /**
     * Load data from the {@link #config-readUrl}.
     * @param {Object} [params] A hash of parameters to append to querystring (will also append Store#params)
     * @returns {Promise} A Promise which will be resolved if the load succeeds, and rejected if the load is
     * vetoed by a {@link #event-beforeLoad} handler, or if an {@link #event-exception} is detected.
     * The resolved function is passed the event object passed to any event handlers.
     * The rejected function is passed the {@link #event-exception} event if an exception occurred,
     * or `false` if the load was vetoed by a {@link #event-beforeLoad} handler.
     * @fires beforeLoad
     * @fires loadStart
     * @fires beforeRequest
     * @fires load
     * @fires exception
     * @fires afterRequest
     * @category CRUD
     */
    async load(params) {
        const
            me = this;

        if (me.isPaged) {
            return me.loadPage(me.currentPage || 1, params);
        }
        else {
            return me.internalLoad(params, '', (event) => {
                // The set Data setter will trigger the refresh event with { action: 'dataset' }
                me.data = event.data;
            });
        }
    }

    /**
     * Loads children into specified parent record. Parent records id is sent as a param (param name configured with
     * {@link #config-parentIdParamName}.
     * @param {Core.data.Model} parentRecord Parent record
     * @returns {Promise} A Promise which will be resolved if the load succeeds, and rejected if the load is
     * vetoed by a {@link #event-beforeLoadChildren} handler, or if an {@link #event-exception} is detected.
     * The resolved function is passed the event object passed to any event handlers.
     * The rejected function is passed the {@link #event-exception} event if an exception occurred,
     * or `false` if the load was vetoed by a {@link #event-beforeLoadChildren} handler.
     * @fires beforeLoadChildren
     * @fires loadChildrenStart
     * @fires beforeRequest
     * @fires loadChildren
     * @fires exception
     * @fires afterRequest
     * @category CRUD
     */
    async loadChildren(parentRecord) {
        // Immediate call to sendLoadRequest because we can make multiple, concurrent requests
        // to load many tree nodes at once, so pass delay parameter as false.
        return this.readUrl ? this.internalLoad({ [this.parentIdParamName] : parentRecord.id }, 'Children', event => {
            event.parentRecord = parentRecord;

            if (parentRecord.children.length) {
                parentRecord.clearChildren(true);
            }
            // Append received children
            parentRecord.data[parentRecord.constructor.childrenField] = event.data;
            parentRecord.processChildren(parentRecord.stores);
        }, false) : this.immediatePromise;
    }

    /**
     * Loads a page of data from the {@link #config-readUrl}.
     * @param {Number} page The *one based* page number to load.
     * @param {Object} params A hash of parameters to append to querystring (will also append Store#params)
     * @returns {Promise} A Promise which will be resolved if the load succeeds, and rejected if the load is
     * vetoed by a {@link #event-beforeLoadPage} handler, or if an {@link #event-exception} is detected.
     * The resolved function is passed the event object passed to any event handlers.
     * The rejected function is passed the {@link #event-exception} event if an exception occurred,
     * or `false` if the load was vetoed by a {@link #event-beforeLoadPage} handler.
     * @fires beforeLoadPage
     * @fires loadPageStart
     * @fires beforeRequest
     * @fires loadPage
     * @fires exception
     * @fires afterRequest
     * @category CRUD
     */
    async loadPage(page, params) {
        if (this.allCount) {
            page = Math.min(page, this.lastPage);
        }
        const
            me        = this,
            pageParam = me.pageParamName
                ? {
                    [me.pageParamName] : page
                }
                : {
                    [me.pageStartParamName] : (page - 1) * me.pageSize
                };

        pageParam[me.pageSizeParamName] = me.pageSize;
        return me.internalLoad(ObjectHelper.assign(pageParam, params), 'Page', (event) => {
            // We go directly to loadPage because paging a tree store is unsupportable.
            // loadPage will trigger the refresh event with { action: 'pageLoad' }
            me.loadData(event.data, 'pageLoad');
        });
    }

    /**
     * If this store {@link #property-isPaged is paged}, and is not already at the {@link #property-lastPage}
     * then this will load the next page of data.
     * @fires beforeLoadPage
     * @fires loadPageStart
     * @fires beforeRequest
     * @fires loadPage
     * @fires exception
     * @fires afterRequest
     * @category CRUD
     * @returns {Promise} A promise which is resolved when the Ajax request completes and has been processed.
     */
    async nextPage(params) {
        const me = this;
        return me.isPaged && me.currentPage !== me.lastPage ? me.loadPage(me.currentPage + 1, params) : immediatePromise;
    }

    /**
     * If this store {@link #property-isPaged is paged}, and is not already at the first page
     * then this will load the previous page of data.
     * @fires beforeLoadPage
     * @fires loadPageStart
     * @fires beforeRequest
     * @fires loadPage
     * @fires exception
     * @fires afterRequest
     * @category CRUD
     * @returns {Promise} A promise which is resolved when the Ajax request completes and has been processed.
     */
    async previousPage(params) {
        return this.isPaged && this.currentPage !== 1 ? this.loadPage(this.currentPage - 1, params) : immediatePromise;
    }

    /**
     * Commits all changes (added, modified and removed) using corresponding urls ({@link #config-createUrl},
     * {@link #config-updateUrl} and {@link #config-deleteUrl})
     * @fires beforeCommit
     * @returns {Promise} A Promise which is resolved only if all pending changes (Create, Update and Delete)
     * successfully resolve. Both the resolve and reject functions are passed a `commitState` object which is stored the
     * {@link #event-afterRequest} event for each request. Each event contains the `exception`, `request` and `response`
     * properties eg:
     *
     * ```javascript
     * {
     *      // If *all* commits succeeded
     *      success: true,
     *      changes: {
     *          added: [records...],
     *          modified: [records...],
     *          removed: [records...],
     *      },
     *      added: {
     *          source: theStore,
     *
     *          // Only if the add request triggered an exception
     *          exception: true,
     *
     *          // Only if the add request triggered an exception
     *          exceptionType: 'server', // Or 'network'
     *
     *          response: Response,
     *          json: parsedResponseObject
     *      },
     *      // Same format as added
     *      modified: {},
     *      removed: {}
     * }
     * ```
     *
     * If there were no pending changes, the resolve and reject functions are passed no parameters.
     *
     * Returns `false` if a commit operation is already in progress.
     * The resolved function is passed the event object passed to any event handlers.
     * The rejected function is passed the {@link #event-exception} event if an exception occurred,
     * @category CRUD
     */
    async commit() {
        const
            me          = this,
            { changes } = me,
            allPromises = [];


        // not allowing additional commits while in progress
        if (me.commitPromise) {
            return false;
        }

        // No outstanding changes, return a Promise that resolves immediately.
        if (!changes) {
            // Special handling of modified. If only non-persistable fields have been modified, it won't count among
            // `changes`, but still needs to be cleared
            if (me.modified.count) {
                me.modified.forEach(record => record.clearChanges(true, false));
                me.modified.clear();
            }

            return immediatePromise;
        }

        if (me.trigger('beforeCommit', { changes }) !== false) {
            // Flag all affected records as being committed
            [...changes.added, ...changes.modified, ...changes.removed].forEach(record => record.meta.committing = true);

            // Commit was not prevented in beforeCommit listener, so we begin the commit
            me.trigger('commitStart', { changes });

            const
                commitState = {
                    action    : 'commit',
                    exception : false,
                    changes
                };

            let p = me.commitRemoved(commitState);

            if (p) {
                allPromises.push(p);
            }
            p = me.commitAdded(commitState);
            if (p) {
                allPromises.push(p);
            }
            p = me.commitModified(commitState);
            if (p) {
                allPromises.push(p);
            }

            // If there were no urls configured, behave as a local store
            if (!allPromises.length) {
                me.modified.forEach(r => r.clearChanges(true, false));
                me.modified.clear();

                me.added.forEach(r => r.clearChanges(true, false));
                me.added.clear();

                me.removed.clear();
                me.trigger('commit', { changes });
                return immediatePromise;
            }

            // The Promises from the commit methods all resolve whether the request
            // succeeded or not. They each contribute their afterrequest event to the
            // commitState which can be used to detect overall success or failure
            // and granular inspection of which operations succeeded or failed.
            // If there's only one operation, wait for it.
            // If there's more than one operation, we have to wait for allPromises to resolve.
            p = allPromises.length === 1 ? allPromises[0] : Promise.all(allPromises);

            return me.commitPromise = new Promise((resolve, reject) => {
                p.then(() => {

                    me.commitPromise = null;
                    if (commitState.exception) {
                        me.trigger('exception', commitState);
                        reject(commitState);
                    }
                    else {
                        me.trigger('commit', { changes });
                        resolve(commitState);
                    }
                }).catch(() => {
                    me.commitPromise = null;
                    reject(commitState);
                });
            });
        }
    }

    // Performs background autocommit with reject checking
    doAutoCommit() {
        if (this.suspendCount <= 0) {
            this.commit().catch(commitState => {
                const { response } = commitState;
                // Skip throw if response is `ok` or `status` is 500 or 404 and request is successfully parsed
                // These errors are notified by API events
                if (!(response && (response.ok && response.parsedJson || [500, 404].includes(response.status)))) {
                    throw commitState;
                }
            });
        }
    }



    /**
     * Commits added records by posting to {@link #config-createUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was successful.
     * @param {Object} commitState An object into which is added a `delete` property being the
     * {@link #event-afterRequest} event.
     * @returns {Promise|null} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `add` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @async
     * @private
     * @fires beforeRequest
     * @fires commitAdded
     * @fires refresh
     * @fires exception
     * @fires afterRequest
     */
    commitAdded(commitState) {
        const
            me    = this,
            added = me.added,
            event = { action : 'create', params : me.params };

        return added.count && me.createUrl ? new Promise((resolve) => {
            const toAdd        = added.values.map(r => r.persistableData);
            commitState.create = event;

            event.body = { data : toAdd };

            me.trigger('beforeRequest', event);

            let dataToSend = event.body;

            if (me.sendAsFormData) {
                const
                    formData = new FormData();

                formData.append('data', JSON.stringify(toAdd));
                dataToSend = formData;
            }

            const
                options = { headers : me.headers, parseJson : true },
                url     = me.createUrl + me.buildQueryString(me.createUrl, me.params);

            if (me.useRestfulMethods) {
                options.method = me.httpMethods.create;
            }

            AjaxHelper.post(url, dataToSend, ObjectHelper.assign(options, me.fetchOptions)).then(response => {
                const
                    data    = response.parsedJson,
                    isArray = Array.isArray(data),
                    success = isArray || (data && (data.success !== false));

                commitState.response = response;
                event.json           = data;
                event.response       = response;

                if (success) {
                    // Copy updated fields and updated ID back into records.
                    // This also calls clearChanges on each record.
                    me.processReturnedData(added.values, isArray ? data : ObjectHelper.getPath(data, me.responseDataProperty));

                    // Clear down added records cache
                    added.clear();

                    me.trigger('commitAdded');

                    // We must signal a full refresh because any number of records could have received any number of field updates
                    // back from the server, so a refresh is more efficient than picking through the received updates.
                    me.trigger('refresh', event);

                    resolve(commitState);
                }
                else {
                    // Clear committing flag
                    added.forEach(r => r.meta.committing = false);

                    commitState.exception = event.exception = true;

                    commitState.exceptionType = event.exceptionType = 'server';

                    me.trigger('exception', event);
                    resolve(commitState);
                }

                // finally
                me.trigger('afterRequest', event);
            }).catch(responseOrError => {
                // Clear committing flag
                added.forEach(r => r.meta.committing = false);

                commitState.exception = event.exception = true;

                if (responseOrError instanceof Response) {
                    commitState.response = responseOrError;
                    event.exceptionType  = responseOrError.ok ? 'server' : 'network';
                    event.response       = responseOrError;
                    event.error          = responseOrError.error;
                }
                else {
                    event.exceptionType = 'server';
                    event.error         = responseOrError;
                }

                me.trigger('exception', event);
                resolve(commitState);

                // finally
                me.trigger('afterRequest', event);
            });
        }) : null;
    }

    /**
     * Commits modified records by posting to {@link #config-updateUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was successful.
     * @param {Object} commitState An object into which is added a `delete` property being the
     * {@link #event-afterRequest} event.
     * @returns {Promise|null} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `update` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @async
     * @private
     * @fires beforeRequest
     * @fires commitModified
     * @fires refresh
     * @fires exception
     * @fires afterRequest
     */
    commitModified(commitState) {
        const
            me           = this,
            // Only include persistable changes
            { modified } = me.changes,
            event        = { action : 'update', params : me.params },
            result       = modified.length && me.updateUrl ? new Promise(resolve => {
                // Use the record's modificationData, not modifications.
                // modifications returns a map using *field names*
                // The server will expect a map using the original dataSource properties.
                const
                    modifications = modified.map(record => {
                        if (me.writeAllFields) {
                            return record.persistableData;
                        }
                        else {
                            return record.modificationDataToWrite;
                        }
                    }).filter(el => !ObjectHelper.isEmpty(el));

                // Check if modifications are empty and don't make request
                if (modifications.length === 0) {
                    me.modified.clear();
                    modified.forEach(r => r.meta.committing = false);
                    resolve();
                    return;
                }

                commitState.update = event;

                event.body = { data : modifications };

                me.trigger('beforeRequest', event);

                let dataToSend = event.body;

                if (me.sendAsFormData) {
                    const
                        formData = new FormData();

                    formData.append('data', JSON.stringify(modifications));
                    dataToSend = formData;
                }

                const
                    options = { headers : me.headers, parseJson : true };

                if (me.useRestfulMethods) {
                    options.method = me.httpMethods.update;
                }

                AjaxHelper.post(
                    me.updateUrl + me.buildQueryString(me.updateUrl, me.params),
                    dataToSend,
                    ObjectHelper.assign(options, me.fetchOptions)
                ).then(response => {
                    const
                        data    = response.parsedJson,
                        isArray = Array.isArray(data),
                        success = isArray || (data && (data.success !== false));

                    commitState.response = response;
                    event.json           = data;
                    event.response       = response;

                    if (success) {
                        // Copy updated fields and updated ID back into records.
                        // This also calls clearChanges on each record.
                        me.processReturnedData(modified, isArray ? data : ObjectHelper.getPath(data, me.responseDataProperty), true);

                        // Clear down modified records cache
                        me.modified.clear();

                        me.trigger('commitModified');

                        // We must signal a full refresh because any number of records could have received any number of
                        // field updates back from the server, so a refresh is more efficient than picking through the
                        // received updates.
                        me.trigger('refresh', event);

                        resolve(commitState);
                    }
                    else {
                        // Clear committing flag
                        modified.forEach(r => r.meta.committing = false);

                        commitState.exception = event.exception = true;
                        event.exceptionType   = 'server';
                        me.trigger('exception', event);
                        resolve(commitState);
                    }

                    // finally
                    me.trigger('afterRequest', event);
                }).catch(responseOrError => {
                    // Clear committing flag
                    modified.forEach(r => r.meta.committing = false);

                    commitState.exception = event.exception = true;

                    if (responseOrError instanceof Response) {
                        commitState.response = responseOrError;
                        event.exceptionType  = responseOrError.ok ? 'server' : 'network';
                        event.response       = responseOrError;
                        event.error          = responseOrError.error;
                    }
                    else {
                        event.exceptionType = 'server';
                        event.error         = responseOrError;
                    }

                    me.trigger('exception', event);
                    resolve(commitState);

                    // finally
                    me.trigger('afterRequest', event);
                });
            }) : null;

        // Also clear non-persistable changes
        if (!modified.length && me.modified.count) {
            me.modified.clear();
        }

        return result;
    }

    processReturnedData(localRecords, returnedData, isUpdating = false) {
        const
            me           = this,
            Model        = me.modelClass,
            idDataSource = Model.fieldMap.id.dataSource;

        returnedData.forEach((recData, i) => {
            const
                record = localRecords[i];

            // Must clear changed state before syncId goes through store.onModelChange
            record.clearChanges(true, false);

            // Using syncId to update record's id with no flagging the property as modified.
            record.syncId(recData[idDataSource]);

            // When updating, only want to apply the actual changes and not reapply defaults. When adding, also
            // apply the defaults
            Object.assign(localRecords[i].data, Model.processData(recData, isUpdating, me, record));
        });
    }

    /**
     * Commits removed records by posting to {@link #config-deleteUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was successful.
     * @param {Object} commitState An object into which is added a `delete` property being the
     * {@link #event-afterRequest} event.
     * @returns {Promise|null} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `delete` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @async
     * @private
     * @fires beforerequest
     * @fires commitremoved
     * @fires refresh
     * @fires exception
     * @fires afterrequest
     */
    commitRemoved(commitState) {
        const
            me      = this,
            removed = me.removed,
            event   = { action : 'delete', params : me.params };

        return removed.count && me.deleteUrl ? new Promise((resolve) => {
            commitState.delete = event;

            event.body = { ids : removed.map(r => r.id) };

            me.trigger('beforeRequest', event);

            let dataToSend = event.body;

            if (me.sendAsFormData) {
                const
                    formData = new FormData();

                formData.append('id', JSON.stringify(dataToSend.ids));
                dataToSend = formData;
            }

            const
                options = { headers : me.headers, parseJson : true };

            if (me.useRestfulMethods) {
                options.method = me.httpMethods.delete;
            }

            AjaxHelper.post(
                me.deleteUrl + me.buildQueryString(me.deleteUrl, me.params),
                dataToSend,
                ObjectHelper.assign(options, me.fetchOptions)
            ).then(response => {
                const
                    data    = response.parsedJson,
                    isArray = Array.isArray(data),
                    success = isArray || (data && (data.success !== false));

                commitState.response = response;
                event.json           = data;
                event.response       = response;

                if (success) {
                    removed.forEach(record => record.meta.committing = false); // In case used by other store etc.
                    removed.clear();

                    me.trigger('commitRemoved');
                    me.trigger('refresh', event);

                    resolve(commitState);
                }
                else {
                    // Clear committing flag
                    removed.forEach(r => r.meta.committing = false);

                    commitState.exception = event.exception = true;

                    event.exceptionType = 'server';
                    me.trigger('exception', event);
                    resolve(commitState);
                }

                // finally
                me.trigger('afterRequest', event);
            }).catch(responseOrError => {
                // Clear committing flag
                removed.forEach(r => r.meta.committing = false);

                commitState.exception = event.exception = true;

                if (responseOrError instanceof Response) {
                    commitState.response = responseOrError;
                    event.exceptionType  = responseOrError.ok ? 'server' : 'network';
                    event.response       = responseOrError;
                    event.error          = responseOrError.error;
                }
                else {
                    event.exceptionType = 'server';
                    event.error         = responseOrError;
                }

                me.trigger('exception', event);
                resolve(commitState);

                // finally
                me.trigger('afterRequest', event);
            });
        }) : null;
    }

    get remoteFilter() {
        return Boolean(this.filterParamName || this.restfulFilter);
    }

    get remoteSort() {
        return Boolean(this.sortParamName);
    }

}
