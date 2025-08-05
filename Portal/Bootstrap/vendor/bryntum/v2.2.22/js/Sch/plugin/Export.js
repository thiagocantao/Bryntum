/**
@class Sch.plugin.Export
@extends Ext.util.Observable

A plugin (ptype = 'scheduler_export') for generating PDF/PNG out of a Scheduler panel. NOTE: This plugin will make an AJAX request to the server, POSTing
 the HTML to be exported. The {@link #printServer} URL must therefore be on the same domain as your application.

#Configuring/usage

To use this plugin, add it to your scheduler as any other plugin. It is also required to have [PhantomJS][1] and [Imagemagick][2]
installed on the server. The complete process of setting up a backend for this plugin can be found in the readme file inside export examples
as well as on our [blog][3]. Note that export is currently not supported if your view (or store) is buffered.

        var scheduler = Ext.create('Sch.panel.SchedulerGrid', {
            ...

            resourceStore   : resourceStore,
            eventStore      : eventStore,

            plugins         : [
                Ext.create('Sch.plugin.Export', {
                    // default values
                    printServer: 'server.php'
                })
            ]
        });

Scheduler will be extended with three new methods:

* {@link #setFileFormat}, which allows setting the format to which panel should be exported. Default format is `pdf`.

* {@link #showExportDialog}, which shows export settings dialog

        scheduler.showExportDialog();

* {@link #doExport} which actually performs the export operation using {@link #defaultConfig} or provided config object :

        scheduler.doExport(
            {
                format: "A5",
                orientation: "landscape",
                range: "complete",
                showHeader: true,
                singlePageExport: false
            }
        );

#Export options

In the current state, plugin gives few options to modify the look and feel of the generated PDF document throught a dialog window :

{@img scheduler/images/export_dialog.png}

If no changes are made to the form, the {@link #defaultConfig} will be used.

##Export Range

This setting controls the timespan visible on the exported document. Three options are available here :

{@img scheduler/images/export_dialog_ranges.png}

###Complete schedule

Whole current timespan will be visible on the exported document.

###Date range

User can select the start and end dates (from the total timespan of the panel) visible on the exported document.

{@img scheduler/images/export_dialog_ranges_date.png}

###Current view

Timespan of the exported document/image will be set to the currently visible part of the time axis. User can control
the width of the time column and height of row.

{@img scheduler/images/export_dialog_ranges_current.png}

##Paper Format

This combo gives control of the size of the generated document/image by choosing one from a list of supported ISO paper sizes : (`A5`, `A4`, `A3`, `Letter`).
Generated PDF has a fixed DPI value of 72. Dafault format is `A4`.

{@img scheduler/images/export_dialog_format.png}

##Orientation

This setting defines the orientation of the generated document/image.

{@img scheduler/images/export_dialog_orientation.png}

Default option is the `portrait` (horizontal) orientation :

{@img scheduler/images/export_dialog_portrait.png}

Second option is the `landscape` (vertical) orientation :

{@img scheduler/images/export_dialog_landscape.png}

[1]: http://www.phantomjs.org
[2]: http://www.imagemagick.org
[3]: http://bryntum.com/blog

*/
Ext.define('Sch.plugin.Export', {
    extend                  : 'Ext.util.Observable',

    alternateClassName      : 'Sch.plugin.PdfExport',
    alias                   : 'plugin.scheduler_export',

    mixins                  : ['Ext.AbstractPlugin'],

    requires        : [
        'Ext.XTemplate'
    ],

    lockableScope           : 'top',

    /**
    * @cfg {String}
    * URL of the server responsible for running the export steps.
    */
    printServer             : undefined,

    //private template for the temporary export html page
    tpl                     : null,

    /**
    * @cfg {String}
    * Class name of the dialog used to change export settings.
    */
    exportDialogClassName   : 'Sch.widget.ExportDialog',

    /**
    * @cfg {Object}
    * Config object for the {@link #exportDialogClassName}. Use this to override default values for the export dialog.
    */
    exportDialogConfig      : {},

    /**
    * @cfg {Object}
    * Default export configuration.
    */
    defaultConfig           : {
        format              : "A4",
        orientation         : "portrait",
        range               : "complete",
        showHeader          : true,
        singlePageExport    : false
    },

    /**
     * @cfg {Boolean} expandAllBeforeExport Only applicable for tree views, set to true to do a full expand prior to the export. Defaults to false.
     */
    expandAllBeforeExport   : false,

    /**
    * @private
    * @cfg {Object}
    * Predefined paper sizes in inches for different formats, as defined by ISO standards.
    */
    pageSizes               : {
        A5      : {
            width   : 5.8,
            height  : 8.3
        },
        A4      : {
            width   : 8.3,
            height  : 11.7
        },
        A3      : {
            width   : 11.7,
            height  : 16.5
        },
        Letter  : {
            width   : 8.5,
            height  : 11
        },
        Legal   : {
            width   : 8.5,
            height  : 14
        }
    },

    /**
    * @cfg {Boolean}
    * If set to true, open new window with the generated document after the operation has finished.
    */
    openAfterExport         : true,

    /**
     * An empty function by default, but provided so that you can perform a custom action
     * before the export plugin extracts data from the scheduler.
     * @param {Sch.panel.SchedulerGrid/Sch.panel.SchedulerTree} scheduler The scheduler instance
     * @param {Object[]} ticks The ticks gathered by plugin to export.
     * @method beforeExport
     */
    beforeExport            : Ext.emptyFn,

    /**
     * An empty function by default, but provided so that you can perform a custom action
     * after the export plugin has extracted the data from the scheduler.
     * @param {Sch.panel.SchedulerGrid/Sch.panel.SchedulerTree} scheduler The scheduler instance
     * @method afterExport
     */
    afterExport             : Ext.emptyFn,

    /**
    * @cfg {String}
    * Format of the exported file, selectable from `pdf` or `png`. By default plugin exports panel contents to PDF
    * but PNG file format is also available.
    */
    fileFormat              : 'pdf',

    //private Constant DPI value for generated PDF
    DPI                     : 72,

    /**
     * @event hidedialogwindow
     * Fires to hide the dialog window.
     * @param {Object} response Full server response.
     */

    /**
     * @event showdialogerror
     * Fires to show error in the dialog window.
     * @param {Ext.window.Window} dialog The dialog used to change export settings.
     * @param {String} message Error message to show in the dialog window.
     * @param {Object} response Full server response.
     */

    /**
     * @event updateprogressbar
     * Fires when a progressbar of the {@link #exportDialogClassName dialog} should update it's value.
     * @param {Number} value Value (between 0 and 1) to set on the progressbar.
     * @param {Object} [response] Full server response. This argument is specified only when `value` equals to `1`.
     */

    constructor : function (config) {
        config = config || {};

        if (config.exportDialogConfig) {
            Ext.Object.each(this.defaultConfig, function(k, v, o){
                var configK = config.exportDialogConfig[k];
                if (configK) {
                    o[k] = configK;
                }
            });
        }

        this.callParent([ config ]);

        if (!this.tpl) {
            this.tpl = new Ext.XTemplate('<!DOCTYPE html>' +
                '<html class="' + Ext.baseCSSPrefix + 'border-box {htmlClasses}">' +
                    '<head>' +
                        '<meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />' +
                        '<title>{column}/{row}</title>' +
                        '{styles}' +
                    '</head>' +
                    '<body class="' + Ext.baseCSSPrefix + 'webkit sch-export {bodyClasses}">' +
                        '<tpl if="showHeader">' +
                            '<div class="sch-export-header" style="width:{totalWidth}px"><h2>{column}/{row}</h2></div>' +
                        '</tpl>' +
                        '<div class="{componentClasses}" style="height:{bodyHeight}px; width:{totalWidth}px; position: relative !important">' +
                            '{HTML}' +
                        '</div>' +
                    '</body>' +
                '</html>',
                {
                    disableFormats: true
                }
            );
        }

        this.setFileFormat(this.fileFormat);
    },

    init : function (scheduler) {
        this.scheduler = scheduler;

        scheduler.showExportDialog = Ext.Function.bind(this.showExportDialog, this);
        scheduler.doExport         = Ext.Function.bind(this.doExport, this);
    },

    /**
    * Function for setting the {@link #fileFormat} of exporting panel. Can be either `pdf` or `png`.
    *
    * @param {String} format format of the file to set. Can take either `pdf` or `png`.
    */
    setFileFormat : function (format) {
        if (typeof format !== 'string') {
            this.fileFormat = 'pdf';
        } else {
            format = format.toLowerCase();

            if (format === 'png') {
                this.fileFormat = format;
            } else {
                this.fileFormat = 'pdf';
            }
        }
    },

    /**
    * Instantiates and shows a new {@link #exportDialogClassName} class using {@link #exportDialogConfig} config.
    * This popup should give user possibility to change export settings.
    */
    showExportDialog : function() {
        var me   = this,
            view = me.scheduler.getSchedulingView();

        //dialog window is always removed to avoid resetting its layout after hiding
        if (me.win) {
            me.win.destroy();
            me.win = null;
        }

        me.win  = Ext.create(me.exportDialogClassName, {
            plugin                  : me,
            exportDialogConfig      : Ext.apply({
                startDate       : me.scheduler.getStart(),
                endDate         : me.scheduler.getEnd(),
                rowHeight       : view.timeAxisViewModel.getViewRowHeight(),
                columnWidth     : view.timeAxisViewModel.getTickWidth(),
                defaultConfig   : me.defaultConfig
            }, me.exportDialogConfig)
        });

        me.saveRestoreData();

        me.win.show();
    },

    /*
    * @private
    * Save values to restore panel after exporting
    */
    saveRestoreData : function() {
        var component  = this.scheduler,
            view       = component.getSchedulingView(),
            normalGrid = component.normalGrid,
            lockedGrid = component.lockedGrid;

        //values needed to restore original size/dates of panel
        this.restoreSettings = {
            width           : component.getWidth(),
            height          : component.getHeight(),
            rowHeight       : view.timeAxisViewModel.getViewRowHeight(),
            columnWidth     : view.timeAxisViewModel.getTickWidth(),
            startDate       : component.getStart(),
            endDate         : component.getEnd(),
            normalWidth     : normalGrid.getWidth(),
            normalLeft      : normalGrid.getEl().getStyle('left'),
            lockedWidth     : lockedGrid.getWidth(),
            lockedCollapse  : lockedGrid.collapsed,
            normalCollapse  : normalGrid.collapsed
        };
    },

    /*
    * @private
    * Get links to the stylesheets of current page.
    */
    getStylesheets : function() {
        var styleSheets = Ext.getDoc().select('link[rel="stylesheet"]'),
            ctTmp = Ext.get(Ext.core.DomHelper.createDom({
                tag : 'div'
            })),
            stylesString;

        styleSheets.each(function(s) {
            ctTmp.appendChild(s.dom.cloneNode(true));
        });

        stylesString = ctTmp.dom.innerHTML + '';

        return stylesString;
    },

    /**
    * Function performing the export operation using config from arguments or default {@link #defaultConfig config}. After getting data
    * from the scheduler an XHR request to {@link #printServer} will be made with the following JSON encoded data :
    *
    * * `html` {Array}         - array of html strings containing data of each page
    * * `format` {String}      - paper size of the exported file
    * * `orientation` {String} - orientation of the exported file
    * * `range`       {String} - range of the exported file
    * * `fileFormat`  {String} - file format of the exported file
    *
    * @param {Object} [conf] Config options for exporting. If not provided, {@link #defaultConfig} is used.
    * Possible parameters are :
    *
    * * `format` {String}            - format of the exported document/image, selectable from the {@link #pageSizes} list.
    * * `orientation` {String}       - orientation of the exported document/image. Either `portrait` or `landscape`.
    * * `range` {String}             - range of the panel to be exported. Selectable from `complete`, `current`, `date`.
    * * `showHeader` {Boolean}       - boolean value defining if exported pages should have row/column numbers added in the headers.
    * * `singlePageExport` {Boolean} - boolean value defining if exported file should be divided into separate pages or not
    *
    * @param {Function} [callback] Optional function that will be called after successful response from export backend script.
    * @param {Function} [errback] Optional function that will be called if export backend script returns error.
    */
    doExport : function (conf, callback, errback) {
        // put mask on the panel
        this.mask();

        var me           = this,
            component    = me.scheduler,
            view         = component.getSchedulingView(),
            styles       = me.getStylesheets(),
            config       = conf || me.defaultConfig,
            normalGrid   = component.normalGrid,
            lockedGrid   = component.lockedGrid,
            headerHeight = normalGrid.headerCt.getHeight();

        // keep scheduler state to restore after export
        me.saveRestoreData();

        //expand grids in case they're collapsed
        normalGrid.expand();
        lockedGrid.expand();

        me.fireEvent('updateprogressbar', 0.1);

        // For Tree grid, optionally expand all nodes
        if (this.expandAllBeforeExport && component.expandAll) {
            component.expandAll();
        }

        var ticks           = component.timeAxis.getTicks(),
            timeColumnWidth = view.timeAxisViewModel.getTickWidth(),
            paperWidth,
            printHeight,
            paperHeight;

        //check if we're not exporting to single image as those calculations are not needed in this case
        if (!config.singlePageExport) {
            //size of paper we will be printing on. 72 DPI used by phantomJs generator
            //take orientation into account
            if (config.orientation === 'landscape') {
                paperWidth     = me.pageSizes[config.format].height*me.DPI;
                paperHeight    = me.pageSizes[config.format].width*me.DPI;
            } else {
                paperWidth     = me.pageSizes[config.format].width*me.DPI;
                paperHeight    = me.pageSizes[config.format].height*me.DPI;
            }

            var pageHeaderHeight = 41;

            printHeight = Math.floor(paperHeight) - headerHeight - (config.showHeader ? pageHeaderHeight : 0);
        }

        view.timeAxisViewModel.suppressFit = true;

        var skippedColsBefore   = 0;
        var skippedColsAfter    = 0;

        // if we export a part of scheduler
        if (config.range !== 'complete') {
            var newStart, newEnd;

            switch (config.range) {
                case 'date' :
                    newStart    = new Date(config.dateFrom);
                    newEnd      = new Date(config.dateTo);

                    // ensure that specified period has at least a day
                    if (Sch.util.Date.getDurationInDays(newStart, newEnd) < 1) {
                        newEnd  = Sch.util.Date.add(newEnd, Sch.util.Date.DAY, 1);
                    }

                    newStart    = Sch.util.Date.constrain(newStart, component.getStart(), component.getEnd());
                    newEnd      = Sch.util.Date.constrain(newEnd, component.getStart(), component.getEnd());
                    break;

                case 'current' :
                    var visibleSpan = view.getVisibleDateRange();
                    newStart        = visibleSpan.startDate;
                    newEnd          = visibleSpan.endDate || view.timeAxis.getEnd();

                    if (config.cellSize) {
                        // will change columns wiidth to provided value
                        timeColumnWidth = config.cellSize[0];

                        // change the row height only if value is provided
                        if (config.cellSize.length > 1) {
                            view.setRowHeight(config.cellSize[1]);
                        }
                    }
                    break;
            }

            // set specified time frame
            component.setTimeSpan(newStart, newEnd);

            var startTick   = Math.floor(view.timeAxis.getTickFromDate(newStart));
            var endTick     = Math.floor(view.timeAxis.getTickFromDate(newEnd));

            ticks       = component.timeAxis.getTicks();
            // filter only needed ticks
            ticks       = Ext.Array.filter(ticks, function (tick, index) {
                if (index < startTick) {
                    skippedColsBefore++;
                    return false;
                } else if (index > endTick) {
                    skippedColsAfter++;
                    return false;
                }
                return true;
            });
        }

        // run template method
        this.beforeExport(component, ticks);

        var format, htmlArray, calculatedPages;

        // multiple pages mode
        if (!config.singlePageExport) {

            component.setWidth(paperWidth);
            component.setTimeColumnWidth(timeColumnWidth);
            view.timeAxisViewModel.setTickWidth(timeColumnWidth);

            //calculate amount of pages in the document
            calculatedPages = me.calculatePages(config, ticks, timeColumnWidth, paperWidth, printHeight);

            htmlArray       = me.getExportJsonHtml(calculatedPages, {
                styles              : styles,
                config              : config,
                ticks               : ticks,
                skippedColsBefore   : skippedColsBefore,
                skippedColsAfter    : skippedColsAfter,
                printHeight         : printHeight,
                paperWidth          : paperWidth,
                headerHeight        : headerHeight
            });

            format          = config.format;

        // single page mode
        } else {

            htmlArray           = me.getExportJsonHtml(null, {
                styles              : styles,
                config              : config,
                ticks               : ticks,
                skippedColsBefore   : skippedColsBefore,
                skippedColsAfter    : skippedColsAfter,
                timeColumnWidth     : timeColumnWidth
            });

            var sizeInInches    = me.getRealSize(),
                width           = Ext.Number.toFixed(sizeInInches.width / me.DPI, 1),
                height          = Ext.Number.toFixed(sizeInInches.height / me.DPI, 1);

            format = width+'in*'+height+'in';
        }

        //further update progress bar
        me.fireEvent('updateprogressbar', 0.4);

        if (me.printServer) {

            // if it's not debugging or test environment
            if (!me.debug && !me.test) {
                Ext.Ajax.request({
                    type    : 'POST',
                    url     : me.printServer,
                    timeout : 60000,
                    params  : Ext.apply({
                        html        : {
                            array   : htmlArray
                        },
                        startDate   : component.getStartDate(),
                        endDate     : component.getEndDate(),
                        format      : format,
                        orientation : config.orientation,
                        range       : config.range,
                        fileFormat  : me.fileFormat
                    }, this.getParameters()),
                    success : function(response) {
                        me.onSuccess(response, callback, errback);
                    },
                    failure : function(response) {
                        me.onFailure(response, errback);
                    },
                    scope   : me
                });

            // for debugging mode we just show output instead of sending it to server
            } else if (me.debug) {

                var w, a = Ext.JSON.decode(htmlArray);
                for (var i = 0, l = a.length; i < l; i++) {
                    w = window.open();
                    w.document.write(a[i].html);
                    w.document.close();
                }

            }

        } else {
            throw 'Print server URL is not defined, please specify printServer config';
        }

        view.timeAxisViewModel.suppressFit = false;

        // restore scheduler state
        me.restorePanel();


        // run template method
        this.afterExport(component);

        // for test environment we return export results
        if (me.test) {
            return {
                htmlArray       : Ext.JSON.decode(htmlArray),
                calculatedPages : calculatedPages
            };
        }
    },

    /**
     * This method should return an object, that will be applied to the JSON data passed to the export XHR request.
     * By default this method returns empty object, it is supposed to be overriden in the subclass to provide some extra custom data.
     *
     * @return {Object}
     */
    getParameters : function () {
        return {};
    },

    /*
    * @private
    * Function returning full width and height of both grids.
    *
    * @return {Object} values Object containing width and height properties.
    */
    getRealSize : function() {
        var component     = this.scheduler,
            headerHeight  = component.normalGrid.headerCt.getHeight(),
            tableSelector = '.' + Ext.baseCSSPrefix + (Ext.versions.extjs.isLessThan('5.0') ? 'grid-table' : 'grid-item-container'),
            height        = (headerHeight + component.lockedGrid.getView().getEl().down(tableSelector).getHeight()),
            width         = (component.lockedGrid.headerCt.getEl().first().getWidth() +
                             component.normalGrid.body.down(tableSelector).getWidth());

        return {
            width   : width,
            height  : height
        };
    },

    /*
    * @private
    * Function calculating amount of pages in vertical/horizontal direction in the exported document/image.
    *
    * @param {Array} ticks Ticks from the TickStore.
    * @param {Number} timeColumnWidth Width of a single time column.
    * @return {Object} valuesObject Object containing calculated amount of pages, rows and columns.
    */
    calculatePages : function (config, ticks, timeColumnWidth, paperWidth, printHeight) {
        var me                  = this,
            component           = me.scheduler,
            lockedGrid          = component.lockedGrid,
            rowHeight           = component.getSchedulingView().timeAxisViewModel.getViewRowHeight(),
            lockedHeader        = lockedGrid.headerCt,
            lockedGridWidth     = lockedHeader.getEl().first().getWidth(),
            lockedColumnPages   = null,
            //amount of columns with locked grid visible
            columnsAmountLocked = 0;

        if (lockedGridWidth > lockedGrid.getWidth()) {
            var startCursor   = 0,
                endCursor     = 0,
                width         = 0,
                addItem       = false,
                columnWidth;

            lockedColumnPages = [];

            lockedGrid.headerCt.items.each(function(column, idx, len) {
                columnWidth = column.width;

                if (!width || width + columnWidth < paperWidth) {
                    width += columnWidth;
                    if (idx === len -1) {
                        addItem = true;

                        //we still need to check if any time columns fit
                        var widthLeft = paperWidth - width;
                        columnsAmountLocked = Math.floor(widthLeft / timeColumnWidth);
                    }
                } else {
                    addItem = true;
                }

                if (addItem) {
                    endCursor = idx;

                    lockedColumnPages.push({
                        firstColumnIdx    : startCursor,
                        lastColumnIdx     : endCursor,
                        totalColumnsWidth : width || columnWidth
                    });

                    startCursor = endCursor + 1;
                    width       = 0;
                }
            });
        } else {
            columnsAmountLocked = Math.floor((paperWidth - lockedGridWidth) / timeColumnWidth);
        }

        //amount of columns without locked grid visible
        var columnsAmountNormal = Math.floor(paperWidth / timeColumnWidth),
            //amount of pages horizontally
            columnPages         = Math.ceil((ticks.length - columnsAmountLocked) / columnsAmountNormal),
            rowsAmount          = Math.floor(printHeight/rowHeight);

        if (!lockedColumnPages || columnPages === 0) {
            columnPages += 1;
        }

        return {
            columnsAmountLocked : columnsAmountLocked,
            columnsAmountNormal : columnsAmountNormal,
            lockedColumnPages   : lockedColumnPages,
            rowsAmount          : rowsAmount,
            //amount of pages vertically
            rowPages            : Math.ceil(component.getSchedulingView().store.getCount()/rowsAmount),
            columnPages         : columnPages,
            timeColumnWidth     : timeColumnWidth,
            lockedGridWidth     : lockedGridWidth,
            rowHeight           : rowHeight,
            panelHTML           : {}
        };
    },

    /*
    * @private
    * Method exporting panel's HTML to JSON structure. This function is taking snapshots of the visible panel (by changing timespan
    * and hiding rows) and pushing their html to an array, which is then encoded to JSON.
    *
    * @param {Object} calculatedPages Object with values returned from {@link #calculatePages}.
    * @param {Object} params Object with additional properties needed for calculations.
    *
    * @return {Array} htmlArray JSON string created from an array of objects with stringified html.
    */
    getExportJsonHtml : function (calculatedPages, params) {
        var me                  = this,
            component           = me.scheduler,
            htmlArray           = [],

            //Remove any non-webkit browser-specific css classes
            re                  = new RegExp(Ext.baseCSSPrefix + 'ie\\d?|' + Ext.baseCSSPrefix + 'gecko', 'g'),
            bodyClasses         = Ext.getBody().dom.className.replace(re, ''),
            componentClasses    = component.el.dom.className,
            styles              = params.styles,
            config              = params.config,
            ticks               = params.ticks,
            panelHTML, readyHTML, htmlObject, html,
            timeColumnWidth;

        //Hack for IE
        if (Ext.isIE) {
            bodyClasses += ' sch-ie-export';
        }

        //we need to prevent Scheduler from auto adjusting the timespan
        component.timeAxis.autoAdjust = false;

        if (!config.singlePageExport) {
            var columnsAmountLocked = calculatedPages.columnsAmountLocked,
                columnsAmountNormal = calculatedPages.columnsAmountNormal,
                lockedColumnPages   = calculatedPages.lockedColumnPages,
                rowsAmount          = calculatedPages.rowsAmount,
                rowPages            = calculatedPages.rowPages,
                columnPages         = calculatedPages.columnPages,
                paperWidth          = params.paperWidth,
                printHeight         = params.printHeight,
                headerHeight        = params.headerHeight,
                lastColumn          = null,
                columns, lockedColumnPagesLen;

            timeColumnWidth = calculatedPages.timeColumnWidth;
            panelHTML       = calculatedPages.panelHTML;

            panelHTML.skippedColsBefore = params.skippedColsBefore;
            panelHTML.skippedColsAfter  = params.skippedColsAfter;

            if (lockedColumnPages) {
                lockedColumnPagesLen = lockedColumnPages.length;
                columnPages         += lockedColumnPagesLen;
            }

            //horizontal pages
            for (var i = 0; i < columnPages; i++) {

                //set visible time range to corresponding ticks
                if (lockedColumnPages && i < lockedColumnPagesLen) {
                    if (i === lockedColumnPagesLen - 1 && columnsAmountLocked !== 0) {
                        component.normalGrid.show();
                        lastColumn = Ext.Number.constrain((columnsAmountLocked-1), 0, (ticks.length - 1));
                        component.setTimeSpan(ticks[0].start, ticks[lastColumn].end);
                    } else {
                        component.normalGrid.hide();
                    }
                    var visibleColumns = lockedColumnPages[i];

                    this.showLockedColumns();
                    this.hideLockedColumns(visibleColumns.firstColumnIdx, visibleColumns.lastColumnIdx);

                    //resize lockedGrid to width of visible columns + 1px of border
                    component.lockedGrid.setWidth(visibleColumns.totalColumnsWidth+1);
                } else {

                    if (i === 0) {
                        this.showLockedColumns();

                        if (columnsAmountLocked !== 0) {
                            component.normalGrid.show();
                        }

                        lastColumn = Ext.Number.constrain(columnsAmountLocked - 1, 0, ticks.length - 1);
                        component.setTimeSpan(ticks[0].start, ticks[lastColumn].end);
                    } else {
                        //hide locked grid
                        component.lockedGrid.hide();

                        component.normalGrid.show();

                        if (lastColumn === null) {
                            //set lastColumn to -1 as it'll be incremented by 1, and in this case
                            //we want to start from 0
                            lastColumn = -1;
                        }

                        if (ticks[lastColumn+columnsAmountNormal]){
                            component.setTimeSpan(ticks[lastColumn+1].start, ticks[lastColumn+columnsAmountNormal].end);
                            lastColumn = lastColumn+columnsAmountNormal;
                        } else {
                            component.setTimeSpan(ticks[lastColumn+1].start, ticks[ticks.length-1].end);
                        }
                    }
                }

                //changing timespan resets column width
                component.setTimeColumnWidth(timeColumnWidth, true);
                component.getSchedulingView().timeAxisViewModel.setTickWidth(timeColumnWidth);

                //vertical pages
                for (var k = 0; k < rowPages; k+=1) {

                    //hide rows that are not supposed to be visible on the current page
                    me.hideRows(rowsAmount, k);

                    panelHTML.dom   = component.body.dom.innerHTML;
                    panelHTML.k     = k;
                    panelHTML.i     = i;

                    readyHTML       = me.resizePanelHTML(panelHTML);

                    html            = me.tpl.apply(Ext.apply({
                        bodyClasses      : bodyClasses,
                        bodyHeight       : printHeight + headerHeight,
                        componentClasses : componentClasses,
                        styles           : styles,
                        showHeader       : config.showHeader,
                        HTML             : readyHTML.dom.innerHTML,
                        totalWidth       : paperWidth,
                        headerHeight     : headerHeight,
                        column           : i+1,
                        row              : k+1
                    }));

                    htmlObject = {'html': html};
                    htmlArray.push(htmlObject);

                    //unhide all rows
                    me.showRows();
                }
            }

        } else {
            timeColumnWidth = params.timeColumnWidth;
            panelHTML = calculatedPages ? calculatedPages.panelHTML : {};

            component.setTimeSpan(ticks[0].start, ticks[ticks.length-1].end);
            component.lockedGrid.setWidth(component.lockedGrid.headerCt.getEl().first().getWidth());
            component.setTimeColumnWidth(timeColumnWidth);
            component.getSchedulingView().timeAxisViewModel.setTickWidth(timeColumnWidth);

            var realSize  = me.getRealSize();

            Ext.apply(panelHTML, {
                dom                 : component.body.dom.innerHTML,
                column              : 1,
                row                 : 1,
                timeColumnWidth     : params.timeColumnWidth,
                skippedColsBefore   : params.skippedColsBefore,
                skippedColsAfter    : params.skippedColsAfter
            });

            readyHTML   = me.resizePanelHTML(panelHTML);

            html        = me.tpl.apply(Ext.apply({
                bodyClasses      : bodyClasses,
                bodyHeight       : realSize.height,
                componentClasses : componentClasses,
                styles           : styles,
                showHeader       : false,
                HTML             : readyHTML.dom.innerHTML,
                totalWidth       : realSize.width
            }));

            htmlObject = {'html': html};
            htmlArray.push(htmlObject);
        }

        component.timeAxis.autoAdjust = true;

        return Ext.JSON.encode(htmlArray);
    },

    /*
    * @private
    * Resizes panel elements to fit on the print page. This has to be done manually in case of wrapping Scheduler
    * inside another, smaller component.
    *
    * @param {Object} HTML Object with html of panel, and row & column number.
    *
    * @return {Object} frag Ext.dom.Element with resized html.
    */
    resizePanelHTML: function (HTML) {
        //create empty div that will temporarily hold our panel current HTML
        var frag       = Ext.get(Ext.core.DomHelper.createDom({
                tag: 'div',
                html: HTML.dom
            })),
            component  = this.scheduler,
            lockedGrid = component.lockedGrid,
            normalGrid = component.normalGrid,
            lockedEl,
            lockedElements,
            normalElements;

        //HACK for resizing in IE6/7 and Quirks mode. Requires operating on a document fragment with DOM methods
        //instead of using unattached div and Ext methods.
        if (Ext.isIE6 || Ext.isIE7 || Ext.isIEQuirks){
            var dFrag = document.createDocumentFragment(),
                method, selector;

            //IE removed getElementById from documentFragment in later browsers
            if (dFrag.getElementById){
                method   = 'getElementById';
                selector = '';
            } else {
                method = 'querySelector';
                selector = '#';
            }

            dFrag.appendChild(frag.dom);

            lockedEl = lockedGrid.view.el;

            lockedElements = [
                dFrag[method](selector+component.id+'-targetEl'),
                dFrag[method](selector+component.id+'-innerCt'),
                dFrag[method](selector+lockedGrid.id),
                dFrag[method](selector+lockedGrid.body.id),
                dFrag[method](selector+lockedEl.id)
            ];
            normalElements = [
                dFrag[method](selector+normalGrid.id),
                dFrag[method](selector+normalGrid.headerCt.id),
                dFrag[method](selector+normalGrid.body.id),
                dFrag[method](selector+normalGrid.getView().id)
            ];

            Ext.Array.each(lockedElements, function(el){
                if(el !== null){
                    el.style.height = '100%';
                    el.style.width  = '100%';
                }
            });

            Ext.Array.each(normalElements, function(el, idx){
                if (el !== null){
                    if (idx === 1){
                        el.style.width = '100%';
                    } else {
                        el.style.height = '100%';
                        el.style.width  = '100%';
                    }
                }
            });

            frag.dom.innerHTML = dFrag.firstChild.innerHTML;
        } else {
            //this wasn't needed in real life, only for tests under 4.2 to pass
            lockedEl = lockedGrid.view.el;

            lockedElements = [
                frag.select('#'+component.id+'-targetEl').first(),
                frag.select('#'+component.id+'-innerCt').first(),
                frag.select('#'+lockedGrid.id).first(),
                frag.select('#'+lockedGrid.body.id).first(),
                frag.select('#'+lockedEl.id)
            ];
            normalElements = [
                frag.select('#'+normalGrid.id).first(),
                frag.select('#'+normalGrid.headerCt.id).first(),
                frag.select('#'+normalGrid.body.id).first(),
                frag.select('#'+normalGrid.getView().id).first()
            ];

            Ext.Array.each(lockedElements, function(el, idx){
                if(el){
                    el.setHeight('100%');
                    if (idx !== 3 && idx !== 2 ) {
                        el.setWidth('100%');
                    }
                }
            });

            Ext.Array.each(normalElements, function(el, idx){
                //don't change height of the header, just width
                if (idx === 1){
                    el.setWidth('100%');
                } else {
                    el.applyStyles({
                        height: '100%',
                        width: '100%'
                    });
                }
            });
        }

        return frag;
    },

    //Private used to prevent using old reference in the response callbacks
    getWin : function () {
        return this.win || null;
    },


    hideDialogWindow : function(response) {
        var me  = this;
        //fire event for hiding window
        me.fireEvent('hidedialogwindow', response);
        me.unmask();

        if (me.openAfterExport) {
            window.open(response.url, 'ExportedPanel');
        }
    },


    //Private.
    onSuccess : function (response, callback, errback) {
        var me  = this,
            win = me.getWin(),
            result;

        try {
            result = Ext.JSON.decode(response.responseText);
        } catch (e) {
            this.onFailure(response, errback);
            return;
        }

        //set progress to 100%
        me.fireEvent('updateprogressbar', 1, result);

        if (result.success) {
            //close print widget
            setTimeout(function() { me.hideDialogWindow(result); }, win ? win.hideTime : 3000);
        } else {
            //show error message in print widget window
            me.fireEvent('showdialogerror', win, result.msg, result);
        }

        me.unmask();

        if (callback) {
            callback.call(this, response);
        }
    },

    //Private.
    onFailure : function (response, errback) {
        var win = this.getWin(),                     // Not JSON           // Decoded JSON ok
            msg = response.status === 200 ? response.responseText : response.statusText;

        this.fireEvent('showdialogerror', win, msg);
        this.unmask();

        if (errback) {
            errback.call(this, response);
        }
    },

    /*
    * @private
    * Hide rows from the panel that are not needed on current export page by adding css class to them.
    *
    * @param {Number} rowsAmount Amount of rows to be hidden.
    * @param {Number} page Current page number.
    */
    hideRows : function (rowsAmount, page) {
        var lockedRows = this.scheduler.lockedGrid.view.getNodes(),
            normalRows = this.scheduler.normalGrid.view.getNodes(),
            start      = rowsAmount * page,
            end        = start + rowsAmount;

        for (var i = 0, l = normalRows.length; i < l; i++) {
            if (i < start || i >= end) {
                lockedRows[i].className += ' sch-none';
                normalRows[i].className += ' sch-none';
            }
        }
    },

    /*
    * @private
    * Unhide all rows of the panel by removing the previously added css class from them.
    */
    showRows : function () {
        this.scheduler.getEl().select(this.scheduler.getSchedulingView().getItemSelector()).each(function(el){
            el.removeCls('sch-none');
        });
    },

    hideLockedColumns : function (startColumn, endColumn) {
        var lockedColumns = this.scheduler.lockedGrid.headerCt.items.items;

        for (var i = 0, l = lockedColumns.length; i < l; i++) {
            if (i < startColumn || i > endColumn) {
                lockedColumns[i].hide();
            }
        }
    },

    showLockedColumns : function () {
        this.scheduler.lockedGrid.headerCt.items.each(function(column){
            column.show();
        });
    },

    /*
    * @private
    * Mask the body, hiding panel to allow changing it's parameters in the background.
    */
    mask : function () {
        var mask = Ext.getBody().mask();
        mask.addCls('sch-export-mask');
    },

    //Private.
    unmask : function () {
        Ext.getBody().unmask();
    },

    /*
    * @private
    * Restore panel to pre-export state.
    */
    restorePanel : function () {
        var s      = this.scheduler,
            config = this.restoreSettings;

        s.setWidth(config.width);
        s.setHeight(config.height);
        s.setTimeSpan(config.startDate, config.endDate);
        s.setTimeColumnWidth(config.columnWidth, true);
        s.getSchedulingView().setRowHeight(config.rowHeight);
        s.lockedGrid.show();
        s.normalGrid.setWidth(config.normalWidth);
        s.normalGrid.getEl().setStyle('left', config.normalLeft);
        s.lockedGrid.setWidth(config.lockedWidth);

        if (config.lockedCollapse) {
            s.lockedGrid.collapse();
        }
        if (config.normalCollapse) {
            s.normalGrid.collapse();
        }
        //We need to update TimeAxisModel for layout fix #1334
        s.getSchedulingView().timeAxisViewModel.update();
    },

    destroy : function () {
        if (this.win) {
            this.win.destroy();
        }
    }
});
