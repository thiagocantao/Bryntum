/**
 @class Sch.widget.ExportDialog
 @private
 @extends Ext.window.Window

 Widget for export options.

 */
Ext.define('Sch.widget.ExportDialog', {
    alternateClassName  : 'Sch.widget.PdfExportDialog',
    extend              : 'Ext.window.Window',
    requires            : ['Sch.widget.ExportDialogForm'],
    mixins              : ['Sch.mixin.Localizable'],
    alias               : "widget.exportdialog",

    //Panel settings. Overridable with {@link Sch.plugin.Export#cfg-exportDialogConfig}
    modal               : false,
    width               : 350,
    cls                 : 'sch-exportdialog',
    frame               : false,
    layout              : 'fit',
    draggable           : true,
    padding             : 0,

    //Private
    plugin              : null,

    /**
     * @cfg {Ext.Component} buttonsPanel Component with buttons controlling export.
     */
    buttonsPanel        : null,

    /**
     * @cfg {Object} buttonsPanelScope
     * The scope for the {@link #buttonsPanel}
     */
    buttonsPanelScope   : null,

    /**
     * @cfg {Ext.Component} progressBar Progress bar component.
     */
    progressBar         : null,

    /**
     * @cfg {String} generalError Text used for displaying errors, when no error message was returned from the server.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} formatFieldLabel Text used as a label for the paper format setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} orientationFieldLabel Text used as a label for the orientation setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} rangeFieldLabel Text used as a label for the export range setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} showHeaderLabel Text used as a label for the showing/hiding of page numbers checkbox.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} exportToSingleLabel Text used as a label for the checkbox defining if export should create single page.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} orientationPortraitText Text used for the portrait orientation setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} orientationLandscapeText Text used for the landscape orientation setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} completeViewText Text used for the complete view export range setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} currentViewText Text used for the current view export range setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} dateRangeText Text used for the date range export range setting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} dateRangeFromText Text indicating the start of timespan when exporting a date range.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} pickerText Text used as a legend for the row/column picker.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} dateRangeToText Text indicating the end of timespan when exporting a date range.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} exportButtonText Text displayed on the button starting the export operation.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} cancelButtonText Text displayed on the button cancelling the export and hiding the dialog.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {String} progressBarText Text displayed on the progress bar while exporting.
     * @deprecated Please use {@link #l10n l10n} instead.
     */
    /**
     * @cfg {Object} l10n
     * A object, purposed for the class localization. Contains the following keys/values:

            - generalError                : 'An error occured, try again.',
            - title                       : 'Export Settings',
            - formatFieldLabel            : 'Paper format',
            - orientationFieldLabel       : 'Orientation',
            - rangeFieldLabel             : 'Export range',
            - showHeaderLabel             : 'Add page number',
            - orientationPortraitText     : 'Portrait',
            - orientationLandscapeText    : 'Landscape',
            - completeViewText            : 'Complete schedule',
            - currentViewText             : 'Current view',
            - dateRangeText               : 'Date range',
            - dateRangeFromText           : 'Export from',
            - pickerText                  : 'Resize column/rows to desired value',
            - dateRangeToText             : 'Export to',
            - exportButtonText            : 'Export',
            - cancelButtonText            : 'Cancel',
            - progressBarText             : 'Exporting...',
            - exportToSingleLabel         : 'Export as single page'
     */

    /**
     * @cfg {String} dateRangeFormat Valid date format to be used by the date ranges fields.
     */
    dateRangeFormat : '',

    constructor : function (config) {
        Ext.apply(this, config.exportDialogConfig);

        Ext.Array.forEach(
            [
                'generalError',
                'title',
                'formatFieldLabel',
                'orientationFieldLabel',
                'rangeFieldLabel',
                'showHeaderLabel',
                'orientationPortraitText',
                'orientationLandscapeText',
                'completeViewText',
                'currentViewText',
                'dateRangeText',
                'dateRangeFromText',
                'pickerText',
                'dateRangeToText',
                'exportButtonText',
                'cancelButtonText',
                'progressBarText',
                'exportToSingleLabel'
            ],
            function (prop) {
                if (prop in config) this[prop] = config[prop];
            },
            this
        );

        this.title = this.L('title');

        //store fields texts in the config object for further use by form
        this.config = Ext.apply({
            progressBarText          : this.L('progressBarText'),
            dateRangeToText          : this.L('dateRangeToText'),
            pickerText               : this.L('pickerText'),
            dateRangeFromText        : this.L('dateRangeFromText'),
            dateRangeText            : this.L('dateRangeText'),
            currentViewText          : this.L('currentViewText'),
            formatFieldLabel         : this.L('formatFieldLabel'),
            orientationFieldLabel    : this.L('orientationFieldLabel'),
            rangeFieldLabel          : this.L('rangeFieldLabel'),
            showHeaderLabel          : this.L('showHeaderLabel'),
            exportToSingleLabel      : this.L('exportToSingleLabel'),
            orientationPortraitText  : this.L('orientationPortraitText'),
            orientationLandscapeText : this.L('orientationLandscapeText'),
            completeViewText         : this.L('completeViewText'),
            adjustCols               : this.L('adjustCols'),
            adjustColsAndRows        : this.L('adjustColsAndRows'),
            specifyDateRange         : this.L('specifyDateRange'),
            dateRangeFormat          : this.dateRangeFormat,
            defaultConfig            : this.defaultConfig
        }, config.exportDialogConfig);

        this.callParent(arguments);
    },

    initComponent : function () {
        var me          = this,
            listeners   = {
                hidedialogwindow    : me.destroy,
                showdialogerror     : me.showError,
                updateprogressbar   : function (value) {
                    me.fireEvent('updateprogressbar', value);
                },
                scope               : this
            };

        me.form         = me.buildForm(me.config);

        Ext.apply(this, {
            items : me.form,
            fbar  : me.buildButtons(me.buttonsPanelScope || me)
        });

        me.callParent(arguments);

        me.plugin.on(listeners);
    },

    afterRender : function () {
        var me = this;

        me.relayEvents(me.form.resizePicker, ['change', 'changecomplete', 'select']);

        me.form.relayEvents(me, ['updateprogressbar', 'hideprogressbar', 'showprogressbar']);

        me.callParent(arguments);
    },

    /**
     * Create Dialog's buttons.
     *
     * @param {Object} buttonsScope Scope for the buttons.
     * @return {Object} buttons Object containing buttons for Exporting/Cancelling export.
     */
    buildButtons : function (buttonsScope) {
        return [
            {
                xtype   : 'button',
                scale   : 'medium',
                text    : this.L('exportButtonText'),
                handler : function () {
                    if (this.form.isValid()) {
                        this.fireEvent('showprogressbar');
                        this.plugin.doExport(this.form.getValues());
                    }
                },
                scope   : buttonsScope
            },
            {
                xtype   : 'button',
                scale   : 'medium',
                text    : this.L('cancelButtonText'),
                handler : function () {
                    this.destroy();
                },
                scope   : buttonsScope
            }
        ];
    },

    /**
     * Build the {@link Sch.widget.ExportDialogForm} for the dialog window.
     *
     * @param {Object} config Config object for the form, containing field names and values.
     * @return {Sch.widget.ExportDialogForm} form
     */
    buildForm : function (config) {
        return new Sch.widget.ExportDialogForm({
            progressBar  : this.progressBar,
            dialogConfig : config
        });
    },
    
    /**
     * @private
     * Displays error message in the dialog. When it's called, both form and buttons are hidden.
     * @param {Sch.widget.ExportDialog} dialog Dialog window or null
     * @param {String} error (optional) Text of the message that will be displayed in the dialog. If not provided, {@link #generalError}
     * will be used.
     */
    showError : function (dialog, error) {
        var me = dialog,
            text = error || me.L('generalError');

        me.fireEvent('hideprogressbar');
        Ext.Msg.alert('', text);
    }
});
