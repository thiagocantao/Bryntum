import PartOfProject from './mixin/PartOfProject.js';
import Store from '../../Core/data/Store.js';
import ChangeLogTransactionModel from '../model/changelog/ChangeLogTransactionModel.js';

/**
 * @module SchedulerPro/data/ChangeLogStore
 */

/**
 * A {@link Core.data.Store} that contains the changelog, an append-only record of changes to the project,
 * managed by the {@link SchedulerPro.feature.Versions} feature. See also {@link SchedulerPro.data.VersionStore}.
 *
 * You can provide a custom subclass of {@link SchedulerPro.model.changelog.ChangeLogTransactionModel} using the
 * {@link SchedulerPro.feature.Versions#config-transactionModelClass} configuration.
 *
 * @extends Core/data/Store
 */
export default class ChangeLogStore extends Store.mixin(
    PartOfProject
) {

    static $name = 'ChangeLogStore';

    static configurable = {
        modelClass : ChangeLogTransactionModel,
        storeId    : 'changelogs'
    };
}
