import InstancePlugin from '../../../Core/mixin/InstancePlugin.js';
import AttachToProjectMixin from '../../data/mixin/AttachToProjectMixin.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import EventHelper from '../../../Core/helper/EventHelper.js';

/**
 * @module Scheduler/feature/base/ResourceTimeRangesBase
 */

/**
 * Abstract base class for ResourceTimeRanges and ResourceNonWorkingTime features.
 * You should not use this class directly.
 *
 * @extends Core/mixin/InstancePlugin
 * @abstract
 */
export default class ResourceTimeRangesBase extends InstancePlugin.mixin(AttachToProjectMixin) {
    //region Config

    static configurable = {
        /**
         * Specify value to use for the tabIndex attribute of range elements
         * @config {Number}
         * @category Misc
         */
        tabIndex : null,

        entityName : 'resourceTimeRange'
    };

    static get pluginConfig()  {
        return {
            chain    : ['getEventsToRender', 'onEventDataGenerated', 'noFeatureElementsInAxis'],
            override : ['matchScheduleCell', 'resolveResourceRecord']
        };
    }

    // Let Scheduler know if we have ResourceTimeRanges in view or not
    noFeatureElementsInAxis() {
        const { timeAxis } = this.client;
        return !this.needsRefresh && this.store && !this.store.storage.values.some(t => timeAxis.isTimeSpanInAxis(t));
    }

    //endregion

    //region Init

    doDisable(disable) {
        if (this.client.isPainted) {
            this.client.refresh();
        }

        super.doDisable(disable);
    }

    updateTabIndex() {
        if (!this.isConfiguring) {
            this.client.refresh();
        }
    }

    //endregion

    getEventsToRender(resource, events) {
        throw new Error('Implement in subclass');
    }

    // Called for each event during render, allows manipulation of render data. Adjust any resource time ranges
    // (chained function from Scheduler)
    onEventDataGenerated(renderData) {
        const
            me                       = this,
            { eventRecord, iconCls } = renderData;

        if (me.shouldInclude(eventRecord)) {
            if (me.client.isVertical) {
                renderData.width = renderData.resourceRecord.columnWidth || me.client.resourceColumnWidth;
            }
            else {
                renderData.top = 0;
            }

            // Flag that we should fill entire row/col
            renderData.fillSize = true;
            // Add our own cls
            renderData.wrapperCls['b-sch-resourcetimerange'] = 1;
            if (me.rangeCls) {
                renderData.wrapperCls[me.rangeCls] = 1;
            }
            renderData.wrapperCls[`b-sch-color-${eventRecord.timeRangeColor}`] = eventRecord.timeRangeColor;
            // Add label
            renderData.eventContent.text = eventRecord.name;
            renderData.children.push(renderData.eventContent);

            // Allow configuring tabIndex
            renderData.tabIndex = me.tabIndex != null ? String(me.tabIndex) : null;

            // Add icon
            if (iconCls?.length > 0) {
                renderData.children.unshift({
                    tag       : 'i',
                    className : iconCls.toString()
                });
            }

            // Event data for DOMSync comparison
            renderData.eventId = me.generateElementId(eventRecord);
        }
    }

    /**
     * Generates ID from the passed time range record
     * @param {Scheduler.model.TimeSpan} record
     * @returns {String} Generated ID for the DOM element
     * @internal
     */
    generateElementId(record) {
        return record.domId;
    }

    resolveResourceTimeRangeRecord(rangeElement) {
        return rangeElement?.closest(`.${this.rangeCls}`)?.elementData.eventRecord;
    }

    getElementFromResourceTimeRangeRecord(record) {
        // return this.client.foregroundCanvas.querySelector(`[data-event-id="${record.domId}"]`);
        return this.client.foregroundCanvas.syncIdMap[record.domId];
    }

    resolveResourceRecord(event) {
        const record = this.overridden.resolveResourceRecord(...arguments);

        return record || this.resolveResourceTimeRangeRecord(event.target || event)?.resource;
    }

    shouldInclude(eventRecord) {
        throw new Error('Implement in subclass');
    }

    // Called when a ResourceTimeRangeModel is manipulated, relays to Scheduler#onInternalEventStoreChange which updates to UI
    onStoreChange(event) {
        // Edge case for scheduler not using any events, it has to refresh anyway to get rid of ResourceTimeRanges
        if (event.action === 'removeall' || event.action === 'dataset') {
            this.needsRefresh = true;
        }

        this.client.onInternalEventStoreChange(event);

        this.needsRefresh = false;
    }

    // Override to let scheduler find the time cell from a resource time range element
    matchScheduleCell(target) {
        let cell = this.overridden.matchScheduleCell(target);

        if (!cell && this.enableMouseEvents) {
            const
                { client }   = this,
                rangeElement = target.closest(`.${this.rangeCls}`);

            cell = rangeElement && client.getCell({
                record : client.isHorizontal ? rangeElement.elementData.resource : client.store.first,
                column : client.timeAxisColumn
            });
        }

        return cell;
    }

    handleRangeMouseEvent(domEvent) {
        const
            me           = this,
            rangeElement = domEvent.target.closest(`.${me.rangeCls}`);

        if (rangeElement) {
            const
                eventName               = EventHelper.eventNameMap[domEvent.type] ?? StringHelper.capitalize(domEvent.type),
                resourceTimeRangeRecord = me.resolveResourceTimeRangeRecord(rangeElement);

            me.client.trigger(me.entityName + eventName, {
                feature                    : me,
                [`${me.entityName}Record`] : resourceTimeRangeRecord,
                resourceRecord             : me.client.resourceStore.getById(resourceTimeRangeRecord.resourceId),
                domEvent
            });
        }
    }

    updateEnableMouseEvents(enable) {
        const
            me         = this,
            { client } = me;

        me.mouseEventsDetacher?.();
        me.mouseEventsDetacher = null;

        if (enable) {
            function attachMouseEvents() {
                me.mouseEventsDetacher = EventHelper.on({
                    element     : client.foregroundCanvas,
                    delegate    : `.${me.rangeCls}`,
                    mousedown   : 'handleRangeMouseEvent',
                    mouseup     : 'handleRangeMouseEvent',
                    click       : 'handleRangeMouseEvent',
                    dblclick    : 'handleRangeMouseEvent',
                    contextmenu : 'handleRangeMouseEvent',
                    mouseover   : 'handleRangeMouseEvent',
                    mouseout    : 'handleRangeMouseEvent',
                    thisObj     : me
                });
            }

            client.whenVisible(attachMouseEvents);
        }

        client.element.classList.toggle('b-interactive-resourcetimeranges', Boolean(enable));
    }
}

// No feature based styling needed, do not add a cls to Scheduler
ResourceTimeRangesBase.featureClass = '';
