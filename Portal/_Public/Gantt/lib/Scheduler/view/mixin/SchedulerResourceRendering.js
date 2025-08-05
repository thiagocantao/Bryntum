import Base from '../../../Core/Base.js';

/**
 * @module Scheduler/view/mixin/SchedulerResourceRendering
 */

/**
 * Configs and functions used for resource rendering
 * and by the {@link Scheduler/column/ResourceInfoColumn} class.
 *
 * @mixin
 */
export default Target => class SchedulerResourceRendering extends (Target || Base) {

    static $name = 'SchedulerResourceRendering';

    //region Default config

    static configurable = {

        /**
         * Control how much space to leave between the first event/last event and the resources edge (top/bottom
         * margin within the resource row in horizontal mode, left/right margin within the resource column in
         * vertical mode), in px. Defaults to the value of {@link Scheduler.view.Scheduler#config-barMargin}.
         *
         * Can be configured per resource by setting {@link Scheduler.model.ResourceModel#field-resourceMargin
         * resource.resourceMargin}.
         *
         * @prp {Number}
         * @category Scheduled events
         */
        resourceMargin : null,

        /**
         * A config object used to configure the resource columns in vertical mode.
         * See {@link Scheduler.view.ResourceHeader} for more details on available properties.
         *
         * ```javascript
         * new Scheduler({
         *     resourceColumns : {
         *         columnWidth    : 100,
         *         headerRenderer : ({ resourceRecord }) => `${resourceRecord.id} - ${resourceRecord.name}`
         *     }
         * })
         * ```
         * @config {ResourceHeaderConfig}
         * @category Resources
         */
        resourceColumns : null,

        /**
         * Path to load resource images from. Used by the resource header in vertical mode and the
         * {@link Scheduler.column.ResourceInfoColumn} in horizontal mode. Set this to display miniature
         * images for each resource using their `image` or `imageUrl` fields.
         *
         * * `image` represents image name inside the specified `resourceImagePath`,
         * * `imageUrl` represents fully qualified image URL.
         *
         *  If set and a resource has no `imageUrl` or `image` specified it will try show miniature using
         *  the resource's name with {@link #config-resourceImageExtension} appended.
         *
         * **NOTE**: The path should end with a `/`:
         *
         * ```
         * new Scheduler({
         *   resourceImagePath : 'images/resources/'
         * });
         * ```
         * @config {String}
         * @category Resources
         */
        resourceImagePath : null,

        /**
         * Generic resource image, used when provided `imageUrl` or `image` fields or path calculated from resource
         * name are all invalid. If left blank, resource name initials will be shown when no image can be loaded.
         * @default
         * @config {String}
         * @category Resources
         */
        defaultResourceImageName : null,

        /**
         * Resource image extension, used when creating image path from resource name.
         * @default
         * @config {String}
         * @category Resources
         */
        resourceImageExtension : '.jpg'
    };

    //endregion

    //region Resource header/columns

    // NOTE: The configs below are initially applied to the resource header in `TimeAxisColumn#set mode`

    /**
     * Use it to manipulate resource column properties at runtime.
     * @property {Scheduler.view.ResourceHeader}
     * @readonly
     */
    get resourceColumns() {
        return this.timeAxisColumn?.resourceColumns || this._resourceColumns;
    }

    /**
     * Get resource column width. Only applies to vertical mode. To set it, assign to
     * `scheduler.resourceColumns.columnWidth`.
     * @property {Number}
     * @readonly
     */
    get resourceColumnWidth() {
        return this.resourceColumns?.columnWidth || null;
    }

    //endregion

    //region Event rendering

    // Returns a resource specific resourceMargin, falling back to Schedulers setting
    // This fn could be made public to allow hooking it as an alternative to only setting this in data
    getResourceMargin(resourceRecord) {
        return resourceRecord?.resourceMargin ?? this.resourceMargin;
    }

    // Returns a resource specific barMargin, falling back to Schedulers setting
    // This fn could be made public to allow hooking it as an alternative to only setting this in data
    getBarMargin(resourceRecord) {
        return resourceRecord?.barMargin ?? this.barMargin;
    }

    // Returns a resource specific rowHeight, falling back to Schedulers setting
    // Prio order: Height from record, configured height
    // This fn could be made public to allow hooking it as an alternative to only setting this in data
    getResourceHeight(resourceRecord) {
        return resourceRecord.rowHeight ?? (this.isHorizontal ? this.rowHeight : this.getResourceWidth(resourceRecord));
    }

    getResourceWidth(resourceRecord) {
        return resourceRecord.columnWidth ?? this.resourceColumnWidth;
    }

    // Similar to getResourceHeight(), but for usage later in the process to take height set by renderers into account.
    // Cant be used earlier in the process because then the row will grow
    // Prio order: Height requested by renderer, height from record, configured height
    getAppliedResourceHeight(resourceRecord) {
        const row = this.getRowById(resourceRecord);

        return row?.maxRequestedHeight ?? this.getResourceHeight(resourceRecord);
    }

    // Combined convenience getter for destructuring on calling side
    // Second arg only passed for nested events, handled by NestedEvent feature
    getResourceLayoutSettings(resourceRecord, parentEventRecord = null) {
        const
            resourceMargin = this.getResourceMargin(resourceRecord, parentEventRecord),
            rowHeight      = this.getAppliedResourceHeight(resourceRecord, parentEventRecord);

        return {
            barMargin     : this.getBarMargin(resourceRecord, parentEventRecord),
            contentHeight : Math.max(rowHeight - resourceMargin * 2, 1),
            rowHeight,
            resourceMargin
        };
    }

    //endregion

    // This does not need a className on Widgets.
    // Each *Class* which doesn't need 'b-' + constructor.name.toLowerCase() automatically adding
    // to the Widget it's mixed in to should implement thus.
    get widgetClass() {}
};
