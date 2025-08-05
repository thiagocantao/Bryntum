import DateHelper from '../../Core/helper/DateHelper.js';
import Model from '../../Core/data/Model.js';
import Localizable from '../../Core/localization/Localizable.js';

/**
 * @module SchedulerPro/model/VersionModel
 */

/**
 * Represents a snapshot of a {@link SchedulerPro.model.ProjectModel} at a point in time.
 * Each VersionModel has an associated set of {@link SchedulerPro.model.changelog.ChangeLogTransactionModel changes} that describe the
 * user-initiated modifications to the project that happened since the previous version was captured.
 *
 * @extends Core/data/Model
 */
export default class VersionModel extends Localizable(Model) {
    static get $name() {
        return 'VersionModel';
    }

    /**
     * @hidefields id, readOnly, children, parentId, parentIndex
     */

    static fields = [
        /**
         * The name of the version. When an auto-saved version's `name` is `null`, the version description
         * will return a default text description instead.
         *
         * @field {String} name
         * @category Common
         */
        {
            name : 'name',
            type : 'string'
        },

        /**
         * Whether this version was auto-saved.
         *
         * @field {Boolean} isAutosave
         * @category Common
         */
        {
            name : 'isAutosave',
            type : 'boolean'
        },

        /**
         * A serializable object snapshot of the {@link SchedulerPro.model.ProjectModel} at the point in time when the
         * version was created.
         *
         * Note that this field is not loaded from the backend by default, due to its size. The
         * {@link SchedulerPro.feature.Versions} feature manages loading the contents of this field on demand.
         *
         * @field {Object} content
         * @category Common
         */
        {
            name : 'content',
            type : 'object'
        },

        /**
         * The timestamp when the version was created.
         *
         * @field {Date} savedAt
         * @category Common
         */
        {
            name : 'savedAt',
            type : 'date'
        }
    ];

    onBeforeSave() { }

    get description() {
        return this.name ?? this.defaultDescription;
    }

    get defaultDescription() {
        return `${this.isAutosave ? 'Auto-saved' : 'Saved'} at ${DateHelper.format(this.savedAt,
            this.L(`L{Versions.versionDateFormat}`))}`;
    }

}

VersionModel.exposeProperties();
