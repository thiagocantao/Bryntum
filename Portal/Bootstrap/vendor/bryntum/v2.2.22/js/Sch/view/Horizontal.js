﻿/**
 * @class Sch.view.Horizontal
 * @private
 *
 * An internal view mixin, purposed to be consumed along with {@link Sch.mixin.AbstractTimelineView}.
 * This class is consumed by the scheduling view and provides the horizontal implementation of certain methods.
 */
Ext.define("Sch.view.Horizontal", {
    requires : [
        'Ext.util.Region',
        'Ext.Element',
        'Sch.util.Date'
    ],
    // Provided by creator, in the config object
    view: null,

    constructor: function (config) {
        Ext.apply(this, config);
    },

    translateToScheduleCoordinate: function (x) {
        var view = this.view;

        if (view.rtl) {
            return view.getTimeAxisColumn().getEl().getRight() - x;
        }
        return x - view.getEl().getX() + view.getScroll().left;
    },

    translateToPageCoordinate: function (x) {
        var view = this.view;
        return x + view.getEl().getX() - view.getScroll().left;
    },

    getEventRenderData: function (event, start, end) {
        var eventStart  = start || event.getStartDate(),
            eventEnd    = end || event.getEndDate() || eventStart, // Allow events to be rendered even they are missing an end date
            view        = this.view,
            viewStart   = view.timeAxis.getStart(),
            viewEnd     = view.timeAxis.getEnd(),
            M           = Math,
            startX      = view.getXFromDate(Sch.util.Date.max(eventStart, viewStart)),
            endX        = view.getXFromDate(Sch.util.Date.min(eventEnd, viewEnd)),
            data        = {};

        if (this.view.rtl) {
            data.right = M.min(startX, endX);
        } else {
            data.left = M.min(startX, endX);
        }

        data.width = M.max(1, M.abs(endX - startX)) - view.eventBorderWidth;

        if (view.managedEventSizing) {
            data.top = M.max(0, (view.barMargin - ((Ext.isIE && !Ext.isStrict) ? 0 : view.eventBorderWidth - view.cellTopBorderWidth)));
            data.height = view.timeAxisViewModel.rowHeightHorizontal - (2 * view.barMargin) - view.eventBorderWidth;
        }

        data.start              = eventStart;
        data.end                = eventEnd;
        data.startsOutsideView  = eventStart < viewStart;
        data.endsOutsideView    = eventEnd > viewEnd;
        return data;
    },

    /**
    * Gets the Ext.util.Region, relative to the page, represented by the schedule and optionally only for a single resource. This method will call getDateConstraints to 
    * allow for additional resource/event based constraints. By overriding that method you can constrain events differently for
    * different resources.
    * @param {Sch.model.Resource} resourceRecord (optional) The resource record
    * @param {Sch.model.Event} eventRecord (optional) The event record
    * @return {Ext.util.Region} The region of the schedule
    */
    getScheduleRegion: function (resourceRecord, eventRecord) {
        var getRegionFn     = Ext.Element.prototype.getRegion ? 'getRegion' : 'getPageBox',
            view            = this.view,
            region          = resourceRecord ? Ext.fly(view.getRowNode(resourceRecord))[getRegionFn]() : view.getTableRegion(),
            taStart         = view.timeAxis.getStart(),
            taEnd           = view.timeAxis.getEnd(),
            dateConstraints = view.getDateConstraints(resourceRecord, eventRecord) || { start: taStart, end: taEnd },
            startX          = this.translateToPageCoordinate(view.getXFromDate(Sch.util.Date.max(taStart, dateConstraints.start))),
            endX            = this.translateToPageCoordinate(view.getXFromDate(Sch.util.Date.min(taEnd, dateConstraints.end))),
            top             = region.top + view.barMargin,
            bottom          = region.bottom - view.barMargin - view.eventBorderWidth;

        return new Ext.util.Region(top, Math.max(startX, endX), bottom, Math.min(startX, endX));
    },


    /**
    * Gets the Ext.util.Region, relative to the scheduling view element, representing the passed resource and optionally just for a certain date interval.
    * @param {Sch.model.Resource} resourceRecord The resource record
    * @param {Date} startDate A start date constraining the region
    * @param {Date} endDate An end date constraining the region
    * @return {Ext.util.Region} The region of the resource
    */
    getResourceRegion: function (resourceRecord, startDate, endDate) {
        var view        = this.view,
            rowNode     = view.getRowNode(resourceRecord),
            offsets     = Ext.fly(rowNode).getOffsetsTo(view.getEl()),
            taStart     = view.timeAxis.getStart(),
            taEnd       = view.timeAxis.getEnd(),
            start       = startDate ? Sch.util.Date.max(taStart, startDate) : taStart,
            end         = endDate ? Sch.util.Date.min(taEnd, endDate) : taEnd,
            startX      = view.getXFromDate(start),
            endX        = view.getXFromDate(end),
            top         = offsets[1] + view.cellTopBorderWidth,
            bottom      = offsets[1] + Ext.fly(rowNode).getHeight() - view.cellBottomBorderWidth;

        if (!Ext.versions.touch) {
            var ctElScroll  = view.getScroll();
            top += ctElScroll.top;
            bottom += ctElScroll.top;
        }
        return new Ext.util.Region(top, Math.max(startX, endX), bottom, Math.min(startX, endX));
    },


    columnRenderer: function (val, meta, resourceRecord, rowIndex, colIndex) {
        var view            = this.view;
        var resourceEvents  = view.eventStore.getEventsForResource(resourceRecord);

        if (resourceEvents.length === 0) {
            return;
        }

        var ta              = view.timeAxis,
            eventsTplData   = [],
            i, l;

        // Iterate events belonging to current row
        for (i = 0, l = resourceEvents.length; i < l; i++) {
            var event       = resourceEvents[i],
                start       = event.getStartDate(),
                end         = event.getEndDate();

            // Determine if the event should be rendered or not
            if (start && end && ta.timeSpanInAxis(start, end)) {
                eventsTplData[eventsTplData.length] = view.generateTplData(event, resourceRecord, rowIndex);
            }
        }

        // Event data is now gathered, calculate layout properties for each event (if dynamicRowHeight is used)
        if (view.dynamicRowHeight) {
            var layout              = view.eventLayout.horizontal;
            
            layout.applyLayout(eventsTplData, resourceRecord);
            
            meta.rowHeight          = layout.getRowHeight(resourceRecord, resourceEvents);
        }

        return view.eventTpl.apply(eventsTplData);
    },
    
    
    // private
    resolveResource: function (t) {
        var view = this.view;
        var node = view.findRowByChild(t);

        if (node) {
            return view.getRecordForRowNode(node);
        }

        return null;
    },

    /**
    *  Returns the region for a "global" time span in the view. Coordinates are relative to element containing the time columns
    *  @param {Date} startDate The start date of the span
    *  @param {Date} endDate The end date of the span
    *  @return {Ext.util.Region} The region for the time span
    */
    getTimeSpanRegion: function (startDate, endDate, useViewSize) {
        var view    = this.view,
            startX  = view.getXFromDate(startDate),
            endX    = endDate ? view.getXFromDate(endDate) : startX,
            height, region;

        region = view.getTableRegion();
        
        if (useViewSize) {
            height = Math.max(region ? region.bottom - region.top: 0, view.getEl().dom.clientHeight); // fallback in case grid is not rendered (no rows/table)
        } else {
            height = region ? region.bottom - region.top: 0;
        }
        return new Ext.util.Region(0, Math.max(startX, endX), height, Math.min(startX, endX));
    },

    /**
    * Gets the start and end dates for an element Region
    * @param {Ext.util.Region} region The region to map to start and end dates 
    * @param {String} roundingMethod The rounding method to use
    * @returns {Object} an object containing start/end properties
    */
    getStartEndDatesFromRegion: function (region, roundingMethod, allowPartial) {
        var view        = this.view;
        var rtl         = view.rtl;
        
        var startDate   = view.getDateFromCoordinate(rtl ? region.right : region.left, roundingMethod),
            endDate     = view.getDateFromCoordinate(rtl ? region.left : region.right, roundingMethod);
            
        if (startDate && endDate || allowPartial && (startDate || endDate)) {
            return {
                start   : startDate,
                end     : endDate
            };
        }
        
        return null;
    },

    // private
    onEventAdd: function (s, events) {
        var view = this.view;
        var affectedResources = {};

        for (var i = 0, l = events.length; i < l; i++) {
            var resources = events[i].getResources(view.eventStore);

            for (var j = 0, k = resources.length; j < k; j++) {
                var resource = resources[j];

                affectedResources[resource.getId()] = resource;
            }
        }

        Ext.Object.each(affectedResources, function (id, resource) {
            view.repaintEventsForResource(resource);
        });
    },

    // private
    onEventRemove: function (s, eventRecords) {
        var view = this.view;
        var resourceStore   = this.resourceStore;
        var isTree          = Ext.tree && Ext.tree.View && view instanceof Ext.tree.View;

        if (!Ext.isArray(eventRecords)) {
            eventRecords = [eventRecords];
        }

        var updateResource  = function(resource) {
            if (view.store.indexOf(resource) >= 0) {
                view.repaintEventsForResource(resource);
            }
        };

        for (var i = 0; i < eventRecords.length; i++) {
            var resources = eventRecords[i].getResources(view.eventStore);

            if (resources.length > 1) {
                Ext.each(resources, updateResource, this);
            } else {
                var node = view.getEventNodeByRecord(eventRecords[i]);

                if (node) {
                    var resource = view.resolveResource(node);

                    // Note, the methods below should not rely on Ext.get but since Ext.anim.run
                    // doesn't support HTMLElements, and due to this bug:
                    // http://www.sencha.com/forum/showthread.php?248981-4.1.x-fadeOut-fadeIn-etc-not-safe-to-use-with-flyweights&p=911314#post911314
                    // currently we have to live with this

                    if (Ext.Element.prototype.fadeOut) {
                        Ext.get(node).fadeOut({
                            callback: function() { updateResource(resource); }
                        });
                    } else {
                        Ext.Anim.run(Ext.get(node), 'fade', {
                            out         : true,
                            duration    : 500,
                            after       : function() { updateResource(resource); },
                            autoClear   : false
                        });
                    }
                }
            }
        }
    },

    // private
    onEventUpdate: function (store, model, operation) {
        var previous = model.previous;
        var view = this.view;

        if (previous && previous[model.resourceIdField]) {
            // If an event has been moved to a new row, refresh old row first
            var resource = model.getResource(previous[model.resourceIdField], view.eventStore);
            if (resource) {
                view.repaintEventsForResource(resource, true);
            }
        }

        var resources = model.getResources(view.eventStore);

        Ext.each(resources, function(resource) {
            view.repaintEventsForResource(resource, true);
        });
    },

    setColumnWidth: function (width, preventRefresh) {
        var view = this.view;

        view.getTimeAxisViewModel().setViewColumnWidth(width, preventRefresh);
    },

    /**
    * Method to get the currently visible date range in a scheduling view. Please note that it only works when the schedule is rendered.
    * @return {Object} object with `startDate` and `endDate` properties.
    */
    getVisibleDateRange: function () {
        var view = this.view;

        if (!view.getEl()) {
            return null;
        }

        var tableRegion = view.getTableRegion(),
            startDate   = view.timeAxis.getStart(),
            endDate     = view.timeAxis.getEnd(),
            width       = view.getWidth();

        if ((tableRegion.right - tableRegion.left) < width) {
            return { startDate: startDate, endDate: endDate };
        }

        var scroll      = view.getScroll();

        return {
            startDate   : view.getDateFromCoordinate(scroll.left, null, true),
            endDate     : view.getDateFromCoordinate(scroll.left + width, null, true)
        };
    }
}); 
