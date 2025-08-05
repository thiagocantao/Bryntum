import Store from './Store.js';
import AjaxHelper from '../helper/AjaxHelper.js';

/**
 * @module Common/data/AjaxStore
 */

const processParamEntry = (paramArray, entry) => {
        if (Array.isArray(entry[1])) {
            entry[1].forEach(value => paramArray.push(entry[0] + '=' + value));
        }
        else {
            paramArray.push(entry[0] + '=' + entry[1]);
        }
        return paramArray;
    },
    immediatePromise  = new Promise((resolve) => resolve());

/**
 * Store that does CRUD using Ajax.
 *
 * <h3>Create</h3>
 * Posts array of JSON data for newly added records to {@link #config-createUrl}, expects response containing an array of JSON objects
 * in same order with id set (uses Model#idField as id).
 *
 * <h3>Read</h3>
 * Reads array of JSON data from the data packet returned from the {@link #config-readUrl}. Unique id for each row is required.
 * By default looks in field 'id' but can be configured by setting {@link Common.data.Model#property-idField-static}.
 *
 * <h3>Update</h3>
 * Posts array of JSON data for newly modified records to {@link #config-updateUrl}.
 *
 * <h3>Destroy</h3>
 * Posts to {@link #config-deleteUrl} with removed records ids (for example id=1,4,7).
 *
 * @example
 * new AjaxStore({
 *   createUrl  : 'php/create',
 *   readUrl    : 'php/read',
 *   updateUrl  : 'php/update',
 *   deleteUrl  : 'php/delete',
 *   modelClass : Customer
 * });
 *
 * @extends Common/data/Store
 */
export default class AjaxStore extends Store {
    // region Events

    /**
     * Fired when a remote request fails, either at the network level, or the server returns a failure, or an invalid response.
     *
     * Note that when a {@link #function-commit} fails, more than one exception event will be triggered. The individual operation,
     * `create`, `update` or `delete` will trigger their own `exception` event, but the encapsulating commit operation will also
     * trigger an `exception` event when all the operations have finished, so if exceptions are going to be handled gracefully,
     * the event's `action` property must be examined, and the constituent operations of the event must be examined.
     * @event exception
     * @param {Common.data.Store} source This Store
     * @param {Boolean} exception `true`
     * @param {String} action Action that failed, `'create'`, `'read'`, `'update'` or `'delete'`. May also be fired
     * with '`commit'` to indicate the failure of an aggregated `create`, `update` and `delete` operation. In this case,
     * the event will contain a property for each operation of the commit named `'create'`, `'update'` and `'delete'`,
     * each containing the individual `exception` events.
     * @param {String} exceptionType The type of failure, `'network'` or `'server'`
     * @param {Response} response the `Response` object
     * @param {Object} json The decoded response object *if the exceptionType is `'server'`*
     */

    /**
     * Fired after committing added records
     * @event commitAdded
     * @param {Common.data.Store} source This Store
     */

    /**
     * Fired after committing modified records
     * @event commitModified
     * @param {Common.data.Store} source This Store
     */

    /**
     * Fired on successful load
     * @event load
     * @param {Common.data.Store} source This Store
     * @param {Object[]} data Data loaded
     * @param {Response} response the `Response` object
     * @param {Object} json The decoded response object.
     */

    /**
     * Fired on successful load of remote child nodes for a tree node.
     * @event loadChildren
     * @param {Common.data.Store} source This Store
     * @param {Object[]} data Data loaded
     * @param {Object} json The decoded response object.
     */

    /**
     * Fired after committing removed records
     * @event commitRemoved
     * @param {Common.data.Store} source This Store
     */

    /**
     * Fired before loading starts. Allows altering parameters and is cancelable
     * @event beforeLoad
     * @param {Common.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax requet.
     */

    /**
     * Fired before loading of remote child nodes of a tree node starts. Allows altering parameters and is cancelable
     * @event beforeLoadChildren
     * @param {Common.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax requet.
     */

    /**
     * Fired when loading is beginning. This is not cancelable. Parameters in the event may still be
     * mutated at this stage.
     * @event loadStart
     * @param {Common.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax requet.
     */

    /**
     * Fired when loading of remote child nodes into a tree node is beginning. This is not cancelable. Parameters in the event may still be
     * mutated at this stage.
     * @event loadChildrenStart
     * @param {Common.data.Store} source This Store
     * @param {Object} params An object containing property/name pairs which are the parameters.
     * This may be mutated to affect the parameters used in the Ajax requet.
     */

    /**
     * Fired before any remote request is initiated.
     * @event beforeRequest
     * @param {Common.data.Store} source This Store
     * @param {String} action Action that is making the request, `'create'`, `'read'`, `'update'` or `'delete'`
     */

    /**
     * Fired after any remote request has finished whether successfully or unsuccessfully.
     * @event afterRequest
     * @param {Boolean} exception `true`. *Only present if the request triggered an exception.*
     * @param {String} action Action that has finished, `'create'`, `'read'`, `'update'` or `'delete'`
     * @param {String} exceptionType The type of failure, `'network'` or `'server'`. *Only present if the request triggered an exception.*
     * @param {Response} response The `Response` object
     * @param {Object} json The decoded response object if there was no `'network'` exception.
     */

    // endregion

    //region Config

    static get defaultConfig() {
        return {
            /**
             * An object containing the HTTP headers to add to each server request issued by this Store.
             * @config {Object}
             * @default
             */
            headers : null,

            /**
             * An object containing the Fetch options to pass to each server request issued by this Store. Use this to control if credentials are sent
             * and other options, read more at [MDN](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch).
             * @config {Object}
             * @default
             */
            fetchOptions : null,

            /**
             * Specify `true` to send payloads as form data, `false` to send as regular JSON.
             * @config {Boolean}
             * @default
             */
            sendAsFormData : false,

            /**
             * Specify `true` to send all model fields when committing modified records (as opposed to just the modified fields)
             * @config {Boolean}
             * @default
             */
            writeAllFields : false,

            /**
             * The name of the HTTP parameter passed to this Store's {@link #config-readUrl} to indicate the node `id` to
             * load when loading child nodes on demand if the node being expanded was created with data containing `children: true`.
             * @config {String}
             * @default
             */
            parentIdParamName : 'id',

            /**
             * The property name in JSON responses from the server that contains the data for the records
             * ```
             * {
             *   "success" : true,
             *   // The property name used here should match that of 'reponseDataProperty'
             *   "data" : [
             *     ...
             *   ]
             * }
             * ```
             * @config {String}
             * @default
             */
            responseDataProperty : 'data'
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
     * Just the array of data may be returned, however that precludes the
     * orderly handling of errors encountered at the server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler
     * in the `response` property of the event.
     *
     * The `success` property may be ommitted, it defaults to `true`.
     *
     * @config {String} createUrl
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
     * Just the array of data may be returned, however that precludes the
     * orderly handling of errors encountered at the server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler
     * in the `response` property of the event.
     *
     * The `success` property may be omitted, it defaults to `true`.
     *
     * @config {String} readUrl
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
     * Just the array of data may be returned, however that precludes the
     * orderly handling of errors encountered at the server.
     *
     * If the server encountered an error, the packet would look like this:
     *
     *     {
     *         "success": false,
     *         "message": "Some kind of database error"
     *     }
     *
     * And that packet would be available in the {@link #event-exception} handler
     * in the `response` property of the event.
     *
     * The `success` property may be ommitted, it defaults to `true`.
     *
     * @config {String} updateUrl
     * @category CRUD
     */

    /**
     * Url for destroying records.
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
     * And that packet would be available in the {@link #event-exception} handler
     * in the `response` property of the event.
     *
     * The `success` property may be ommitted, it defaults to `true`.
     *
     * @config {String} deleteUrl
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
            this.load().catch((e) => {});
        }
    }

    /**
     * Returns true if the Store is currently loading
     * @property {Boolean}
     * @readonly
     * @category CRUD
     */
    get isLoading() {
        return this._isLoading;
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

    buildQueryString(...paramObjects) {
        const queryString = Object.entries(Object.assign({}, ...paramObjects)).reduce(processParamEntry, []).join('&');

        return queryString ? '?' + queryString : '';
    }

    internalLoad(params, eventName, successFn) {
        const me        = this,
            allParams = Object.assign({}, me.params, params),
            event     = { action : 'read' + eventName, params : allParams },
            result    = me.readUrl ? new Promise((resolve, reject) => {
                if (me.trigger('beforeLoad' + eventName, event) === false) {
                      return reject(false); // eslint-disable-line
                }

                me._isLoading = true;

                // This may look redundant, but it allows for two levels of event listening.
                // Granular, where the observer observes only the events of interest, and
                // catch-all, where the observer is interested in all requests.
                me.trigger(`load${eventName}Start`, event);
                me.trigger('beforeRequest', event);

                AjaxHelper.get(me.readUrl + me.buildQueryString(allParams), Object.assign({ headers : me.headers, parseJson : true }, me.fetchOptions))
                    .then((response) => {
                        const
                            data = response.parsedJson,
                            isArray = Array.isArray(data),
                            success = isArray || (data && (data.success !== false));

                        me._isLoading = false;
                        event.response = response;
                        event.json    = data;

                        if (success) {
                            event.data = isArray ? data : data[me.responseDataProperty];
                            successFn(event);
                            me.trigger('load' + eventName, event);
                            resolve(event);
                        }
                        else {
                            event.exception     = true;

                            event.exceptionType = 'server';
                            me.trigger('exception', event);
                            reject(event);
                        }

                        // finally
                        me.trigger('afterRequest', event);
                    }).catch(responseOrError => {
                        me._isLoading = false;

                        event.exception     = true;

                        if (responseOrError instanceof Response) {
                            event.exceptionType = responseOrError.ok ? 'server' : 'network';
                            event.response = responseOrError;
                            event.error = responseOrError.error;
                        }
                        else {
                            event.exceptionType = 'server';
                            event.error = responseOrError;
                        }

                        me.trigger('exception', event);
                        reject(event);

                        // finally
                        me.trigger('afterRequest', event);
                    });
            }) : null;

        return result;
    }

    /**
     * Load data from the {@link #config-readUrl}.
     * @param {Object} params A hash of parameters to append to querystring (will also append Store#params)
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
    load(params) {
        const me = this;

        return me.internalLoad(params, '', (event) => {
            // The set Data setter will trigger the refresh event with { action: 'dataset' }
            me.data = event.data;
        });
    }

    /**
     * Loads children into specified parent record. Parent records id is sent as a param (param name configured with
     * {@link #config-parentIdParamName}.
     * @param {Common.data.Model} parentRecord Parent record
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
        const me = this;

        return me.internalLoad({ [me.parentIdParamName] : parentRecord.id }, 'Children', (event) => {
            event.parentRecord = parentRecord;
            // Append received children
            parentRecord.data[parentRecord.constructor.childrenField] = event.data;
            parentRecord.processChildren(parentRecord.stores);
        });
    }

    /**
     * Commits all changes (added, modified and removed) using corresponding urls ({@link #config-createUrl},
     * {@link #config-updateUrl} and {@link #config-deleteUrl})
     * @fires beforeCommit
     * @returns {Promise} A Promise which is resolved only if all pending changes (Create, Update and Delete) successfully resolve.
     * Both the resolve and reject functions are passed a `commitState` object which is stored the {@link #event-afterRequest}
     * event for each request. Each event contains the `exception`, `request` and `response` properties eg:
     *
     *     {
     *          success: true,                  // If *all* commits succeeded
     *          changes: {
     *              added: [records...],
     *              modified: [records...],
     *              removed: [records...],
     *          },
     *          added: {
     *              source: theStore,
     *              exception: true,            // Only if the add request triggered an exception
     *              exceptionType: 'server'/'network', // Only if the add request triggered an exception
     *              response: Response,
     *              json: parsedResponseObject
     *          },
     *          modified: {},                   // Same format as added
     *          removed: {}                     // Same format as added
     *     }
     *
     * If there were no pending changes, the resolve and reject functions are passed no parameters.
     *
     * Returns `false` if a commit operation is already in progress.
     * The resolved function is passed the event object passed to any event handlers.
     * The rejected function is passed the {@link #event-exception} event if an exception occurred,
     * @category CRUD
     */
    commit() {
        const me          = this,
            changes     = me.changes,
            allPromises = [];

        // not allowing additional commits while in progress
        // TODO: should queue
        if (me.commitPromise) return false;

        // No outstanding changes, return a Promise that resolves immediately.
        if (!changes) {
            return immediatePromise;
        }

        // Flag all affected records as being committed
        [...changes.added, ...changes.modified, ...changes.removed].forEach(record => record.meta.committing = true);

        // TODO: do we need a general way of disabling plugins?
        if (!me.disabled && me.trigger('beforeCommit', { changes }) !== false) {
            let commitState = {
                    action    : 'commit',
                    exception : false,
                    changes
                },
                p           = me.commitRemoved(commitState);

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
                me.modified.forEach(r => r.clearChanges(false));
                me.modified.clear();

                me.added.forEach(r => r.clearChanges(false));
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

    // TODO: need a way to abort commits

    /**
     * Commits added records by posting to {@link #config-createUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was succesful.
     * @param {Object} commitState An object into which is added a `delete` property being the {@link #event-afterRequest} event.
     * @returns {Promise} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `add` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @private
     * @fires beforeRequest
     * @fires commitAdded
     * @fires refresh
     * @fires exception
     * @fires afterRequest
     */
    commitAdded(commitState) {
        const me     = this,
            added  = me.added,
            event  = { action : 'create', params : me.params },
            result = added.count && me.createUrl ? new Promise((resolve) => {
                const toAdd = added.values.map(r => r.persistableData);
                commitState.create = event;
                me.trigger('beforeRequest', event);

                let dataToSend = { data : toAdd };

                if (me.sendAsFormData) {
                    const formData = new FormData();

                    formData.append('data', JSON.stringify(toAdd));
                    dataToSend = formData;
                }

                AjaxHelper.post(me.createUrl + me.buildQueryString(me.params), dataToSend, Object.assign({ headers : me.headers, parseJson : true }, me.fetchOptions)).then(response => {
                    const
                        data = response.parsedJson,
                        isArray = Array.isArray(data),
                        success = isArray || (data && (data.success !== false));

                    event.json = data;
                    event.response = response;

                    if (success) {
                        // Copy updated fields and updated ID back into records
                        me.processReturnedData(added.values, isArray ? data : data[me.responseDataProperty]);

                        me.added.forEach(r => r.clearChanges(false));
                        added.clear();

                        me.trigger('commitAdded');

                        // We must signal a full refresh because any number of records could have recieved any number of field updates
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
                        event.exceptionType = responseOrError.ok ? 'server' : 'network';
                        event.response = responseOrError;
                        event.error = responseOrError.error;
                    }
                    else {
                        event.exceptionType = 'server';
                        event.error = responseOrError;
                    }

                    me.trigger('exception', event);
                    resolve(commitState);

                    // finally
                    me.trigger('afterRequest', event);
                });
            }) : null;

        return result;
    }

    /**
     * Commits modified records by posting to {@link #config-updateUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was succesful.
     * @param {Object} commitState An object into which is added a `delete` property being the {@link #event-afterRequest} event.
     * @returns {Promise} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `update` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @private
     * @fires beforeRequest
     * @fires commitModified
     * @fires refresh
     * @fires exception
     * @fires afterRequest
     */
    commitModified(commitState) {
        let me       = this,
            modified = me.modified,
            event    = { action : 'update', params : me.params },
            result   = modified.count && me.updateUrl ? new Promise((resolve) => {
                const modifications = modified.map(r => me.writeAllFields ? r.persistableData : r.modifications);

                commitState.update = event;
                me.trigger('beforeRequest', event);

                let dataToSend = { data : modifications };

                if (me.sendAsFormData) {
                    const formData = new FormData();

                    formData.append('data', JSON.stringify(modifications));
                    dataToSend = formData;
                }

                AjaxHelper.post(
                    me.updateUrl + me.buildQueryString(me.params),
                    dataToSend,
                    Object.assign({ headers : me.headers, parseJson : true }, me.fetchOptions)
                ).then(response => {
                    const
                        data = response.parsedJson,
                        isArray = Array.isArray(data),
                        success = isArray || (data && (data.success !== false));

                    event.json = data;
                    event.response = response;

                    if (success) {
                        // Copy updated fields and updated ID back into records
                        me.processReturnedData(me.modified.values, isArray ? data : data[me.responseDataProperty], true);

                        // Clear down modified fields cache
                        modified.forEach(r => r.clearChanges(false));
                        modified.clear();

                        me.trigger('commitModified');

                        // We must signal a full refresh because any number of records could have recieved any number of field updates
                        // back from the server, so a refresh is more efficient than picking through the received updates.
                        me.trigger('refresh', event);

                        resolve(commitState);
                    }
                    else {
                        // Clear committing flag
                        modified.forEach(r => r.meta.committing = false);

                        commitState.exception = event.exception = true;
                        event.exceptionType = 'server';
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
                        event.exceptionType = responseOrError.ok ? 'server' : 'network';
                        event.response = responseOrError;
                        event.error = responseOrError.error;
                    }
                    else {
                        event.exceptionType = 'server';
                        event.error = responseOrError;
                    }

                    me.trigger('exception', event);
                    resolve(commitState);

                    // finally
                    me.trigger('afterRequest', event);
                });
            }) : null;

        return result;
    }

    processReturnedData(localRecords, returnedData, isUpdating = false) {
        const me = this,
            Model = me.modelClass,
            idDataSource = Model.fieldMap.id.dataSource;

        returnedData.forEach((recData, i) => {
            // Using syncId to update record's id with no flagging the property as modified.
            localRecords[i].syncId(recData[idDataSource]);

            // When updating, only want to apply the actual changes and not reapply defaults. When adding, also
            // apply the defaults
            Object.assign(localRecords[i].data, Model.processData(recData, isUpdating));
        });
    }

    /**
     * Commits removed records by posting to {@link #config-deleteUrl}.
     * Server should return a JSON object with a 'success' property indicating whether the operation was succesful.
     * @param {Object} commitState An object into which is added a `delete` property being the {@link #event-afterRequest} event.
     * @returns {Promise} If there are added records, a Promise which will be resolved whether the commit
     * succeeds or fails. The resulting event is placed into the `delete` property of the passed `commitState`
     * parameter. If there are no added records, `null` is returned.
     * The resolved function is passed the event object passed to any event handlers.
     * @private
     * @fires beforerequest
     * @fires commitremoved
     * @fires refresh
     * @fires exception
     * @fires afterrequest
     */
    commitRemoved(commitState) {
        const me      = this,
            removed = me.removed,
            event   = { action : 'delete', params : me.params },
            result  = removed.count && me.deleteUrl ? new Promise((resolve) => {
                commitState.delete = event;
                me.trigger('beforeRequest', event);

                let dataToSend = { ids : removed.map(r => r.id) };

                if (me.sendAsFormData) {
                    const formData = new FormData();

                    formData.append('id', JSON.stringify(dataToSend.ids));
                    dataToSend = formData;
                }

                AjaxHelper.post(
                    me.deleteUrl + me.buildQueryString(me.params),
                    dataToSend,
                    Object.assign({ headers : me.headers, parseJson : true }, me.fetchOptions)
                ).then(response => {
                    const
                        data = response.parsedJson,
                        isArray = Array.isArray(data),
                        success = isArray || (data && (data.success !== false));

                    event.json = data;
                    event.response = response;

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
                        event.exceptionType = responseOrError.ok ? 'server' : 'network';
                        event.response = responseOrError;
                        event.error = responseOrError.error;
                    }
                    else {
                        event.exceptionType = 'server';
                        event.error = responseOrError;
                    }

                    me.trigger('exception', event);
                    resolve(commitState);

                    // finally
                    me.trigger('afterRequest', event);
                });
            }) : null;

        return result;
    }
}
