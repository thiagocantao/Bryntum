import ArrayHelper from '../../../Core/helper/ArrayHelper.js';
import Localizable from '../../../Core/localization/Localizable.js';
import Model from '../../../Core/data/Model.js';

/**
 * @module SchedulerPro/model/changelog/ChangeLogTransactionModel
 */

/**
 * Represents a set of changes made as a result of a single user action. Changelog transactions may
 * optionally be associated with a single VersionModel.
 *
 * In normal usage, the Versions feature will capture one ChangeLogTransactionModel as a result of
 * a single user action, for example, dragging a task on the timeline. This transaction will contain
 * multiple {@link SchedulerPro.model.changelog.ChangeLogAction}s representing the various effects that
 * dragging a task can have - changes to start and end dates, updates to related dependent tasks, and
 * so on.
 *
 * Changelog transactions can be customized by extending a new model from `ChangeLogTransactionModel`.
 * For an example, see the Gantt versions demo.
 *
 * Individual changes making up a transaction are stored in the {@link #field-actions} field.
 *
 * Refer to {@link SchedulerPro.feature.Versions} for more information about versioning.
 *
 * @extends Core/data/Model
 * @demo Gantt/versions
 */
export default class ChangeLogTransactionModel extends Localizable(Model) {
    static $name = 'ChangeLogTransactionModel';

    /**
     * @hidefields id, readOnly, children, parentId, parentIndex
     */

    static fields = [
        /**
         * The ID of the version to which this transaction belongs, or null if the transaction
         * is not yet associated with any version.
         * @field {String} versionId
         * @category Common
         */
        {
            name : 'versionId'
        },

        /**
         * The {@link SchedulerPro.model.changelog.ChangeLogAction}s that this transaction comprises.
         * @field {SchedulerPro.model.changelog.ChangeLogAction[]} actions
         * @category Common
         */
        {
            name : 'actions',
            type : 'array'
        },

        /**
         * The date and time when the transaction started.
         * @field {Date} occurredAt
         * @category Common
         */
        {
            name : 'occurredAt',
            type : 'date'
        },

        /**
         * An optional, custom text description of the transaction.
         * @field {String} description
         * @category Common
         */
        {
            name : 'description',
            type : 'string'
        }
    ];

    get description() {
        return this.get('description') ?? this.defaultDescription;
    }

    get actions() {
        return this.get('actions') ?? [];
    }

    get defaultDescription() {
        if (this.actions.length === 0) {
            return this.L(`L{Versions.noChanges}`); // Normally shouldn't happen
        }
        const
            me = this,
            actionTypes = ArrayHelper.unique(me.actions.map(({ actionType }) => {
                if (actionType.startsWith('add')) {
                    return 'add';
                }
                if (actionType.startsWith('remove')) {
                    return 'remove';
                }
                if (actionType.startsWith('update')) {
                    return 'update';
                }
                return actionType;
            })),
            entityTypes = ArrayHelper.unique(me.actions.map(({ entity }) => entity.type)),
            entityCount = ArrayHelper.unique(me.actions.map(({ entity }) => `${entity.type}.${entity.id}`)).length,
            transactionDescriptions = me.L(`L{Versions.transactionDescriptions}`),
            entityNames = me.L(`L{Versions.entityNames}`),
            entityNamesPlural = me.L(`L{Versions.entityNamesPlural}`),
            firstEntityType = entityTypes[0],
            assortedEntityTypes = entityTypes.length > 1;

        return transactionDescriptions[actionTypes.length > 1 ? 'mixed' : actionTypes[0]]
            .replace('{n}', entityCount)
            .replace('{entities}', entityCount > 1
                ? (entityNamesPlural[assortedEntityTypes ? 'other' : firstEntityType] ?? entityNamesPlural.other)
                : (entityNames[firstEntityType] ?? entityNames.other));
    }

}

ChangeLogTransactionModel.exposeProperties();
