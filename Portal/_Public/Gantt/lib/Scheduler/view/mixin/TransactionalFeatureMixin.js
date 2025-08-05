/**
 * @module Scheduler/view/mixin/TransactionalFeatureMixin
 */

/**
 * This mixin declares a common config to disable feature transactions in components which support scheduling engine:
 * SchedulerPro and Gantt.
 * @mixin
 */
export default Target => class TransactionalFeatureMixin extends Target {
    static get $name() {
        return 'TransactionalFeatureMixin';
    }

    static configurable = {
        /**
         * When true, some features will start a project transaction, blocking the project queue, suspending
         * store events and preventing UI from updates. It behaves similar to
         * {@link Grid.column.Column#config-instantUpdate} set to `false`.
         * Set `false` to not use project queue.
         * @config {Boolean}
         * @internal
         * @default
         */
        enableTransactionalFeatures : false,

        testConfig : {
            enableTransactionalFeatures : false
        }
    };

    get widgetClass() {}

    /**
     * Returns `true` if queue is supported and enabled
     * @member {Boolean}
     * @internal
     * @readonly
     */
    get transactionalFeaturesEnabled() {
        return this.enableTransactionalFeatures && this.project?.queue;
    }
};
