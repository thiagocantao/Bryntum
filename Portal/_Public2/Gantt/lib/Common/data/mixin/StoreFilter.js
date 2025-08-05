import Base from '../../Base.js';
import Filter from '../../util/CollectionFilter.js';
import FunctionHelper from '../../helper/FunctionHelper.js';
import Collection from '../../util/Collection.js';
import ObjectHelper from '../../helper/ObjectHelper.js';

/**
 * @module Common/data/mixin/StoreFilter
 */

/**
 * Mixin for Store that handles filtering.
 * Filters are instances of {@link Common.util.CollectionFilter CollectionFilter} class.
 *
 * - Adding a filter for the same property will replace the current one (unless a unique {@link Common.util.CollectionFilter#config-id Id} is specified),
 * but will not clear any other filters.
 * - Adding a filter through the {@link #function-filterBy} function is ultimate.
 * It will clear all the property based filters and replace the current filterBy function if present.
 * - Removing records from the store does not remove filters!
 * The filters will be reapplied if {@link #config-reapplyFilterOnAdd}/{@link #config-reapplyFilterOnUpdate} are true and you add new records or update current.
 *
 * ```
 * // Add a filter
 * store.filter({
 *   property : 'score',
 *   value    : 10,
 *   operator : '>'
 * });
 *
 * // Reapply filters
 * store.filter();
 * ```
 *
 * @mixin
 */
export default Target => class StoreFilter extends (Target || Base) {
    //region Config

    static get defaultConfig() {
        return {
            /**
             * Specify a filter config to use initial filtering
             * @config {Object}
             * @category Filtering
             */
            filters : null,

            /**
             * Specify true to reapply filters when a record is added to the store.
             * @config {Boolean}
             * @default
             * @category Filtering
             */
            reapplyFilterOnAdd : false,

            /**
             * Specify true to reapply filters when a record is updated in the store.
             * @config {Boolean}
             * @default
             * @category Filtering
             */
            reapplyFilterOnUpdate : false

        };
    }

    //endregion

    //region Events

    /**
     * Fired after applying filters to the store
     * @event filter
     * @param {Common.data.Store} source This Store
     * @param {Common.util.Collection} filters Filters used by this Store
     * @param {Common.data.Model[]} records Filtered records
     */

    //endregion

    //region Properties

    /**
     * Currently applied filters. A collection of {@link Common.util.CollectionFilter} instances.
     * @type {Common.util.Collection}
     * @readonly
     * @category Sort, group & filter
     */
    set filters(filters) {
        const me         = this,
            collection = me.filters;

        collection.clear();

        // Invalidate the filtersFunction so that it has to be recalculated upon next access
        me._filtersFunction = null;

        // If we are being asked to filter, parse the filters.
        if (filters) {
            if (filters.constructor.name === 'Object') {
                for (const f of Object.entries(filters)) {
                    // Entry keys are either a field name with its value being the filter value
                    // or, there may be one filterBy property which specifies a filering function.
                    if (f[0] === 'filterBy' && typeof f[1] === 'function') {
                        collection.add(new Filter({
                            filterBy : f[1]
                        }));
                    }
                    else {
                        collection.add(new Filter(f[1].constructor.name === 'Object' ? Object.assign({
                            property : f[0]
                        }, f[1]) : {
                            property : f[0],
                            value    : f[1]
                        }));
                    }
                }
            }
            else if (Array.isArray(filters)) {
                // Make sure we are adding CollectionFilters
                collection.add(...filters.map(filterConfig => {
                    if (filterConfig instanceof Filter) {
                        return filterConfig;
                    }
                    return new Filter(filterConfig);
                }));
            }
            else if (filters.isCollection) {
                // Use supplied collection? Opting to use items from it currently
                collection.add(...filters.values);
            }
            else {
                //<debug>
                if (typeof filters !== 'function') {
                    throw new Error('Store filters must be an object whose properties are Filter configs keyed by field name, or an array of Filter configs, or a filtering function');
                }
                //</debug>
                collection.add(new Filter({
                    filterBy : filters
                }));
            }

            collection.forEach(item => item.owner = me);
        }
    }

    get filters() {
        return this._filters || (this._filters = new Collection({ extraKeys : ['property'] }));
    }

    set filtersFunction(filtersFunction) {
        this._filtersFunction = filtersFunction;
    }

    get filtersFunction() {
        const me                     = this,
            { filters, isGrouped } = me;

        if (!me._filtersFunction) {
            if (filters.count) {
                const generatedFilterFunction = Filter.generateFiltersFunction(filters);

                me._filtersFunction = candidate => {
                    // A group record is filtered in if it has passing groupChildren.
                    if (isGrouped && candidate.meta.specialRow) {
                        return candidate.groupChildren.some(generatedFilterFunction);
                    }
                    else {
                        return generatedFilterFunction(candidate);
                    }
                };
            }
            else {
                me._filtersFunction = FunctionHelper.returnTrue;
            }
        }

        return me._filtersFunction;
    }

    /**
     * Check if store is filtered
     * @returns {Boolean}
     * @readonly
     * @category Sort, group & filter
     */
    get isFiltered() {
        return this.filters.values.some(filter => !filter.disabled);
    }

    //endregion

    traverseFilter(record) {
        let me          = this,
            hitsCurrent = !record.isRoot && me.filtersFunction(record),
            hitsChild   = false,
            children    = record.unfilteredChildren || record.children;

        // leaf, bail out
        if (!children || !children.length) {
            return hitsCurrent;
        }

        if (!record.unfilteredChildren) {
            record.unfilteredChildren = record.children.slice();
        }

        record.children = record.unfilteredChildren.filter(r => {
            return me.traverseFilter(r);
        });

        if (record.children.length) hitsChild = true;

        return hitsCurrent || hitsChild;
    }

    traverseClearFilter(record) {
        const me = this;

        if (record.children) {
            record.children = record.unfilteredChildren || record.children;
            record.children.forEach(r => me.traverseClearFilter(r));
        }
    }

    // TODO: Get rid of this.
    // The Filter feature of a Grid pokes around in the Store to ask this question.
    get latestFilterField() {
        return this.filters.last ? this.filters.last.property : null;
    }

    processFieldFilter(filter, value) {
        if (typeof filter === 'string') {
            filter = {
                property : filter,
                value    : value
            };
        }

        filter = filter instanceof Filter ? filter : new Filter(filter);

        // We want notification upon change of field, value or operator
        filter.owner = this;

        // Collection will replace any already existing filter on the field, unless it has id specified
        this.filters.add(filter);
    }

    /**
     * Filters the store by *adding* the specified filter or filters to the existing filters applied to this Store. Call without arguments to reapply filters.
     * ```
     * // Add a filter
     * store.filter({
     *   property : 'age',
     *   operator : '>',
     *   value    : 90
     * });
     *
     * // Reapply filters
     * store.filter();
     * ```
     * @param {String|Object|Object[]|function} field Field name or a filter config or a function to use for filtering
     * @param value Value, used if field is a field name and not a config
     * @fires filter
     * @fires change
     * @category Sort, group & filter
     */
    filter(field, value, silent = false) {
        const me          = this,
            { storage, filters, rootNode } = me,
            oldCount    = me.count;

        if (field) {
            const fieldType = typeof field;

            // We will not be informed about Filter mutations while configuring.
            me.isConfiguring = true;

            // If we provide array of objects looking like :
            //  {
            //      field : 'fieldName',
            //      value : 'someValue',
            //      [operator : '>']
            //  }
            //  or ...
            //  {
            //      field : 'fieldName',
            //      filterBy : function (value, record) {
            //          return value > 50;
            //      }
            //  }
            if (Array.isArray(field)) {
                // we omit "value" argument in this case
                silent = value;
                field.forEach(me.processFieldFilter, me);
            }
            else if (fieldType === 'function') {
                me.filters = field;
            }
            else {
                me.processFieldFilter(field, value);
            }

            // Open up to recieving Filter mutation notifications again
            me.isConfiguring = false;

            // We added a disabled filter to either no filters, or all disabled filters, so no change.
            if (!me.isFiltered) {
                return;
            }
        }

        // Invalidate the filtersFunction so that it has to be recalculated upon next access
        me.filtersFunction = null;

        if (me.tree) {
            if (me.isFiltered) {
                me.traverseFilter(rootNode);
            }
            else {
                me.traverseClearFilter(rootNode);
            }
            storage.replaceValues(me.collectDescendants(rootNode).visible, true);
        }
        else {
            if (me.isFiltered) {
                storage.addFilter({
                    id       : 'primary-filter',
                    filterBy : me.filtersFunction
                });
            }
            else {
                storage.filters.clear();
            }
        }

        me.resetRelationCache();

        if (!silent) {
            me.triggerFilterEvent({ action : 'filter', filters, oldCount, records : me.storage.values });
        }
    }

    get filtered() {
        return this.storage.isFiltered;
    }

    // Used from filter() and StoreCRUD when reapplying filters
    triggerFilterEvent(event) {
        this.trigger('filter', event);
        this.trigger('refresh', event);
        this.trigger('change', event);
    }

    /**
     * Filter store using a function to test each record. Return true from the function to include record in filtered set
     * @param {Function} fn Function used to test records
     * @example
     * store.filterBy(record => record.age > 25 && record.name.startsWith('A'));
     * @category Sort, group & filter
     */
    filterBy(fn) {
        this.filter(fn);
    }

    /**
     * Removes filtering from the specified field.
     * @param {String} field Field to not filter the store on any longer
     * @private
     * @deprecated
     * Only used by the Grid Filtering plugin which assumes one Filter per field.
     * @category Sort, group & filter
     */
    removeFieldFilter(field, silent) {
        const me     = this,
            filter = me.filters.getBy('property', field);

        // If we have such a filter, remove it.
        if (filter) {

            me.filters.remove(filter);

            // Invalidate the filtersFunction so that it has to be recalculated upon next access
            me._filtersFunction = null;

            if (!silent) {
                me.filter();
            }
        }
    }

    /**
     * Removes all filters from the store.
     * @category Sort, group & filter
     */
    clearFilters(silent) {
        this.filters.clear();
        this.filter(undefined, undefined, silent);
    }

    convertFilterToString(field) {
        let filter = this.filters.getBy('property', field),
            result = '';

        if (filter && !filter.filterBy) {
            result = String(filter);
        }

        return result;
    }

    get filterState() {
        return this.filters.values.map(filter => ObjectHelper.cleanupProperties(filter.config));
    }
};
