import List from '../../Core/widget/List.js';
import DomHelper from '../../Core/helper/DomHelper.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';

/**
 * @module Scheduler/widget/ResourceFilter
 */

/**
 * A List which allows selection of resources to filter a specified eventStore to only show
 * events for the selected resources.
 *
 * Because this widget maintains a state that can be changed through the UI, it offers some of the
 * API of an input field. It has a read only {@link #property-value} property, and it fires a
 * {@link #event-change} event.
 *
 * @extends Core/widget/List
 * @classType resourceFilter
 * @widget
 */
export default class ResourceFilter extends List {
    static get $name() {
        return 'ResourceFilter';
    }

    // Factoryable type name
    static get type() {
        return 'resourcefilter';
    }

    static get delayable() {
        return {
            applyFilters : 'raf'
        };
    }

    static get configurable() {
        return {
            /**
             * The {@link Scheduler.data.EventStore EventStore} to filter.
             * Events for resources which are deselected in this List will be filtered out.
             * @config {Scheduler.data.EventStore}
             */
            eventStore : null,

            multiSelect            : true,
            toggleAllIfCtrlPressed : true,
            itemTpl                : record => StringHelper.encodeHtml(record.name || ''),

            /**
             * An optional filter function to apply when loading resources from the project's
             * resource store. Defaults to loading all resources.
             *
             * **This is called using this `ResourceFilter` as the `this` object.**
             * @config {Function|String}
             * @default
             */
            masterFilter : () => true,

            /**
             * By default, deselecting list items filters only the {@link #config-eventStore} so that
             * events for the deselected resources are hidden from view. The `resourceStore` is __not__
             * filtered.
             *
             * Configure this as `true` to also filter the `resourceStore` so that deselected resources
             * are also hidden from view (They will remain in this `List`)
             * @config {Boolean}
             * @default false
             */
            filterResources : null
        };
    }

    itemIconTpl(record, i) {
        const
            { eventColor } = record,
            // Named colors are applied using CSS
            cls            = DomHelper.isNamedColor(eventColor) ? ` b-sch-foreground-${eventColor}` : '',
            // CSS style color is used as is
            style          = !cls && eventColor ? ` style="color:${eventColor}"` : '';

        return this.multiSelect ? `<div class="b-selected-icon b-icon${cls}"${style}></div>` : '';
    }

    updateEventStore(eventStore) {
        const
            me                 = this,
            // HACK: Temp workaround until List's store is dynamically updatable.
            chainedStoreConfig = me.initialConfig.store?.isStore ? me.initialConfig.store.initialConfig : me.store?.config,
            // Allow configuration of the filter for loading records from the master store.
            { resourceStore }  = eventStore,
            store              = me.store = resourceStore.chain(me.masterFilter, null, {
                ...chainedStoreConfig,
                syncOrder : true
            }),
            changeListeners    = {
                change  : 'onStoreChange',
                thisObj : me
            };

        // We need to sync selection and rendering on changes fired from master store
        store.un(changeListeners);
        resourceStore.ion(changeListeners);

        if (!resourceStore.count) {
            resourceStore.project.ion({
                name    : 'project',
                refresh : 'initFilter',
                thisObj : me
            });
        }
        else {
            me.initFilter();
        }
    }

    changeMasterFilter(masterFilter) {
        // Cannot use bind, otherwise fillFromMaster's check for whether it's a filter function fails.
        const me = this;

        // If we are filtering the resource store, we cannot now fill ourselves from the filtered
        // view of the resource store. Otherwise, the list would hide the list items as they are deselected.
        if (!me.filterResources) {
            return function(r) {
                return me.callback(masterFilter, me, [r]);
            };
        }
    }

    initFilter() {
        const { eventStore, selected } = this;

        if (eventStore.resourceStore.count) {
            // We default to all resources selected unless this was configured with
            // an initialSelection. See List#changeSelection
            if (!this.initialSelection) {
                selected.add(this.store.getRange());
            }
            this.detachListeners('project');
        }
    }

    onStoreRefresh({ source : store, action }) {
        // We need to re-enable the filter if the store becomes filtered.
        // We only disable the filter if we know that we have selected all available
        // resources.
        if (action === 'filter' && this.eventStoreFilter) {
            const
                { eventStoreFilter } = this,
                { disabled }       = eventStoreFilter,
                newDisabled        = !store.isFiltered && this.allSelected;

            if (newDisabled !== disabled) {
                eventStoreFilter.disabled = newDisabled;
                this.applyFilters();
            }
        }
        super.onStoreRefresh(...arguments);
    }

    onSelectionChange({ source : selected, added, removed }) {
        // Filter disabled if all resources selected
        const
            me       = this,
            // Only disable the filter if the allSelected method is seeing *all* of the
            // records from its masterStore with no filtering.
            disabled = !me.store.isFiltered && me.allSelected;

        super.onSelectionChange(...arguments);

        let filtersAdded = false;

        // If this is the first selection change triggered from the first project refresh
        // in which all the resources are selected, then we ony need to apply the filters.
        // if *not* all resources are selected, ie if added.length !== entire store length.
        if (!me.eventStoreFilter) {
            // Our client EventStore is filtered to only show events for our selected resources.
            // Events without an associated resource are filtered into visibility.
            // The addFilter function with silent param adds the filter but don't reevaluate filtering.
            me.eventStoreFilter = me.eventStore.addFilter({
                id       : `${me.id}-filter-instance`,
                filterBy : e => !e.resource || me.selected.includes(e.resources),
                disabled
            }, added?.length === me.store.count);

            filtersAdded = true;
        }

        if (me.filterResources && !me.resourceStoreFilter) {
            // Our client EventStore is filtered to only show events for our selected resources.
            // Events without an associated resource are filtered into visibility.
            // The addFilter function with silent param adds the filter but don't reevaluate filtering.
            me.resourceStoreFilter = me.eventStore.resourceStore.addFilter({
                id       : `${me.id}-filter-instance`,
                filterBy : r => me.selected.includes(r),
                disabled
            }, added?.length === me.store.count);

            filtersAdded = true;
        }

        // The filters have been just added and so will take effect. No need to call applyFilter.
        if (filtersAdded) {
            return;
        }

        // Filter disabled if all resources selected
        me.eventStoreFilter.disabled = disabled;
        me.resourceStoreFilter && (me.resourceStoreFilter.disabled = disabled);

        // Have the client EventStore refresh its filtering but after a small delay so the List UI updates immediately.
        me.applyFilters();

        if (me.eventListeners.change) {
            const
                value    = selected.values,
                oldValue = value.concat(removed);

            ArrayHelper.remove(oldValue, ...added);

            /**
             * Fired when this widget's selection changes
             * @event change
             * @param {String} value - This field's value
             * @param {String} oldValue - This field's previous value
             * @param {Core.widget.Field} source - This ResourceFilter
             */
            me.triggerFieldChange({
                value,
                oldValue
            });
        }
    }

    /**
     * An array encapsulating the currently selected resources.
     * @member {Scheduler.model.ResourceModel[]}
     * @readonly
     */
    get value() {
        return this.selected.values;
    }

    applyFilters() {
        this.eventStore.filter();
        this.filterResources && this.eventStore.resourceStore.filter();
    }

    doDestroy() {
        this.store?.destroy();
        super.doDestroy();
    }
}

// Register this widget type with its Factory
ResourceFilter.initClass();
