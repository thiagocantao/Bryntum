import Base from '../../../Core/Base.js';
import WebSocketManager from '../../../Core/util/WebSocketManager.js';

/**
 * @module SchedulerPro/model/mixin/ProjectWebSocketHandlerMixin
 */

/**
 * This mixin allows project to communicate changes over websocket connection to stay in sync with other clients. By
 * default, project will automatically sync changes, to temporary suspend autoSync call {@link #function-suspendAutoSync}
 * and to resume {@link #function-resumeAutoSync}. These methods use counter, meaning for every suspendAutoSync call
 * there should be resumeAutoSync.
 * @mixin
 * @private
 */
export default Target => class ProjectWebSocketHandlerMixin extends (Target || Base) {
    static configurable = {
        /**
         * Address of the websocket server. If configured websocket will be opened during
         * instantiation.
         * @config {String}
         */
        wsAddress : null,

        /**
         * User name config for websocket connection
         * @config {String}
         */
        wsUserName : '',

        /**
         * Set to false to not request dataset automatically
         * @config {Boolean}
         * @category Websocket
         * @default
         */
        wsAutoLoad : true,

        /**
         * Set to false to disable syncing changes automatically
         * @config {Boolean}
         * @category Websocket
         * @default
         */
        wsAutoSync : true,

        /**
         * Websocket connection timeout
         * @config {Number}
         * @category Websocket
         * @default
         */
        wsConnectionTimeout : 60000,

        /**
         * Id of the project to use for load/sync requests. When changed project will load the dataset if
         * {@link #config-wsAutoLoad} is true. Otherwise you need to call {@link #function-wsLoad} manually.
         * @config {String|Number}
         * @category Websocket
         * @default
         */
        wsProjectId : null
    };

    doDestroy() {
        this.websocketManager?.destroy();

        super.doDestroy();
    }

    //#region Config handlers

    updateWsAddress(address) {
        const me = this;

        me.websocketManager?.destroy();

        if (address) {
            me.websocketManager = new WebSocketManager({
                address,
                userName          : me.wsUserName,
                internalListeners : {
                    message : 'handleWebsocketMessage',
                    close   : 'handleWebsocketClose',
                    error   : 'handleWebsocketError',
                    thisObj : me
                }
            });

            if (me.wsAutoLoad) {
                me.wsLoad();
            }
        }

        me.toggleAutoSyncListener();
    }

    toggleAutoSyncListener() {
        const me = this;

        me.detachListeners('websocketlisteners');

        if (me._wsAutoSync && me._wsAddress) {
            me.ion({
                name       : 'websocketlisteners',
                hasChanges : 'scheduleWebsocketMessage'
            });
        }
    }

    updateWsAutoSync() {
        this.toggleAutoSyncListener();
    }

    updateWsProjectId(value) {
        if (value != null && this.wsAutoLoad) {
            this.wsLoad();
        }
    }
    //#endregion

    //#region Auto sync

    scheduleWebsocketMessage() {
        const me = this;

        if (!me.hasTimeout('wsAutoSync') && !me.isAutoSyncSuspended) {
            me.setTimeout({
                name : 'wsAutoSync',
                fn   : () => {
                    me.wsSync();
                },
                delay : me.autoSyncTimeout
            });
        }
    }

    //#endregion

    /**
     * Suspends automatic sync (via CRUD manager or websocket connection) upon store changes. Can be called multiple
     * times (it uses an internal counter).
     * @category CRUD
     * @method suspendAutoSync
     */

    /**
     * Resumes automatic sync upon store changes (via CRUD manager or websocket connection). Will trigger commit if the
     * internal counter is 0.
     * @param {Boolean} [doSync=true] Pass `true` to trigger data syncing after resuming (if there are pending
     * changes) and `false` to not persist the changes.
     * @category CRUD
     */
    resumeAutoSync(doSync = true) {
        super.resumeAutoSync(doSync);

        // If we have an active websocket connection and
        if (this.websocketManager && !this.isAutoSyncSuspended && this.changes) {
            this.wsSync();
        }
    }

    /**
     * Sends message over configured websocket connection. Requires {@link #config-wsAddress} to be configured.
     * @param {String} command
     * @param {Object} [data] Data object to send to the websocket
     * @param {Boolean} silent Pass true to not trigger {@link #event-wsSendMessage} event
     * @returns {Boolean} Returns true if message was sent
     */
    async wsSend(command, data, silent = false) {
        if (await this.wsOpen()) {
            this.websocketManager.send(command, data);

            /**
             * Fires after project has sent a message over websocket connection
             * @event wsSendMessage
             * @param {Object} data Data object with mandatory `command` key and arbitrary data keys.
             * @param {String} data.command Mandatory command to send
             * @category Websocket
             */
            if (!silent) {
                this.trigger('wsSendMessage', { command, data });
            }
            return true;
        }

        return false;
    }

    /**
     * Template function which might be implemented to process messages from the websocket server
     * @param {Object} data Data object with mandatory `command` key and arbitrary data keys.
     * @param {String} data.command Mandatory command to send
     */
    wsReceive(data) { }

    handleWebsocketClose() {
        /**
         * Fires when websocket connection is closed
         * @event wsClose
         * @category Websocket
         */
        this.trigger('wsClose');
    }

    handleWebsocketError({ error }) {
        /**
         * Fires when websocket manager throws error onn connection or trying to process the response
         * @event wsError
         * @param {Error} error Error event
         * @category Websocket
         */
        this.trigger('wsError', { error });
    }

    async handleWebsocketMessage({ data }) {
        const
            me              = this,
            { wsProjectId } = me,
            { project }     = data;

        /**
         * Fires when project receives message from the websocket server
         * @event wsMessage
         * @param {Object} data Data object with mandatory `command` key and arbitrary data keys.
         * @param {String} data.command Mandatory command to send
         * @category Websocket
         */
        me.trigger('wsMessage', { data });

        if (project === wsProjectId && data.command === 'projectChange') {

            /**
             * Fires before project has applied project changes from the websocket server
             * @event wsBeforeReceiveChanges
             * @category Websocket
             */
            me.trigger('wsBeforeReceiveChanges');

            await me.queue(() => me.applyProjectChanges(data.changes));

            /**
             * Fires after project has applied project changes from the websocket server
             * @event wsReceiveChanges
             * @category Websocket
             */
            me.trigger('wsReceiveChanges');
        }
        else if (project === wsProjectId && data.command === 'dataset') {
            await me.loadInlineData(data.dataset);

            /**
             * Fires after project has applied dataset from the websocket server
             * @event wsReceiveDataset
             * @category Websocket
             */
            me.trigger('wsReceiveDataset');
        }
        else if (project === wsProjectId && data.command === 'versionAutoSaveOK') {
            /**
             * Fires when client receives permission to proceed with a version auto-save.
             * @event versionAutoSaveOK
             */
            me.trigger('wsVersionAutoSaveOK');
        }
        else if (project === wsProjectId && data.command === 'loadVersionContent') {
            /**
             * Fires when client receives version content.
             * @event loadVersionContent
             */
            const { versionId, project, content } = data;
            me.trigger('loadVersionContent', { versionId, project, content });
        }
        else {
            me.wsReceive(data);
        }
    }

    /**
     * Open websocket connection
     * @returns {Boolean}
     */
    async wsOpen() {
        const { websocketManager } = this;

        if (websocketManager) {
            const trigger = !websocketManager.isOpened && await websocketManager.open();

            if (trigger) {
                /**
                 * Fires when websocket is opened
                 * @event wsOpen
                 * @category Websocket
                 */
                this.trigger('wsOpen');
            }

            return websocketManager.isOpened;
        }

        return false;
    }

    /**
     * Loads data to the project and calculates it:
     *
     * ```javascript
     * await project.wsLoad();
     * ```
     *
     * @category Websocket
     */
    async wsLoad() {
        const me = this;

        if (me.wsProjectId == null) {
            return;
        }

        // Send request for dataset
        await me.wsSend('dataset', { project : me.wsProjectId });

        // Await for `wsReceiveDataset` event. When such event arrives `inlineData` is set and project committed
        await new Promise(resolve => {
            const detacher = me.ion({
                wsReceiveDataset() {
                    detacher();
                    resolve(true);
                },
                expires : {
                    delay : me.wsConnectionTimeout,
                    alt   : () => {
                        detacher();
                        resolve(false);
                    }
                }
            });
        });

        await me.commitAsync();

        /**
         * Fires when dataset is loaded
         * @event wsLoad
         * @category Websocket
         */
        me.trigger('wsLoad');
    }

    /**
     * Persists changes made on the registered stores to the server and/or receives changes made on the backend.
     * Usage:
     *
     * ```javascript
     * // Send changes to the websocket server
     * await project.wsSync();
     * ```
     *
     * ** Note: ** Please take a look at {@link #config-wsAutoSync} config. This option allows to persist changes
     * automatically after any data modification.
     *
     * @category Websocket
     */
    async wsSync() {
        const
            me              = this,
            { wsProjectId } = me;

        if (wsProjectId == null) {
            return;
        }

        // When running on a timeout, we might get here when engine is not ready, leading to the changes call below
        // getting stuck in an infinite loop.
        await this.commitAsync();

        const { changes } = this;

        if (changes && (me.wsAutoSync && !me.autoSyncSuspendCounter)) {
            me.acceptChanges();

            const trigger = await me.wsSend('projectChange', { project : wsProjectId, changes });

            if (trigger) {
                /**
                 * Fires after project has sent changes over websocket connection
                 * @event wsSendChanges
                 * @category Websocket
                 */
                me.trigger('wsSendChanges');
            }
        }
    }

    /**
     * Closes websocket connection
     */
    wsClose() {
        this.websocketManager?.close();
    }
};
