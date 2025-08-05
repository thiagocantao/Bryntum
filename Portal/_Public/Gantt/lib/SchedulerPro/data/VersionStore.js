import PartOfProject from './mixin/PartOfProject.js';
import Store from '../../Core/data/Store.js';
import VersionModel from '../model/VersionModel.js';

/**
 * @module SchedulerPro/data/VersionStore
 */

/**
 * A {@link Core.data.Store} that contains the list of saved versions of the project,
 * managed by the {@link SchedulerPro.feature.Versions} feature.
 * See also {@link SchedulerPro.data.ChangeLogStore}.
 *
 * You can provide a custom subclass of {@link SchedulerPro.model.VersionModel} using the
 * {@link SchedulerPro.feature.Versions#config-versionModelClass} config.
 *
 * @extends Core/data/Store
 */
export default class VersionStore extends Store.mixin(
    PartOfProject
) {

    static $name = 'VersionStore';

    static configurable = {
        modelClass : VersionModel,
        storeId    : 'versions'
    };
}
