/**
 @class Sch.widget.ExportDialogForm
 @private
 @extends Ext.form.Panel

 Form for {@link Sch.widget.ExportDialog}. This is a private class and can be overriden by providing `formPanel` option to
 {@link Sch.plugin.Export#cfg-exportDialogConfig exportDialogConfig}.
 */
Ext.define('Sch.widget.ExportDialogForm', {
    extend      : 'Ext.form.Panel',
    requires    : [
        'Ext.data.Store',
        'Ext.ProgressBar',
        'Ext.form.field.ComboBox',
        'Ext.form.field.Date',
        'Ext.form.FieldContainer',
        'Ext.form.field.Checkbox',
        'Sch.widget.ResizePicker'
    ],

    border      : false,
    bodyPadding : '10 10 0 10',
    autoHeight  : true,

    initComponent : function () {
        var me = this;

        // HACK
        // fix for tooltip width
        // http://www.sencha.com/forum/showthread.php?260106-Tooltips-on-forms-and-grid-are-not-resizing-to-the-size-of-the-text
        if (Ext.getVersion('extjs').isLessThan('4.2.1')) {
            if (typeof Ext.tip !== 'undefined' && Ext.tip.Tip && Ext.tip.Tip.prototype.minWidth != 'auto') {
                Ext.tip.Tip.prototype.minWidth      = 'auto';
            }
        }

        me.createFields();

        Ext.apply(this, {
            fieldDefaults   : {
                labelAlign  : 'left',
                labelWidth  : 120,
                anchor      : '99%'
            },
            items           : [
                me.rangeField,
                me.resizerHolder,
                me.datesHolder,
                me.showHeaderField,
                me.exportToSingleField,
                me.formatField,
                me.orientationField,

                me.progressBar || me.createProgressBar()
            ]
        });

        me.callParent(arguments);

        me.onRangeChange(null, me.dialogConfig.defaultConfig.range);

        me.on({
            hideprogressbar     : me.hideProgressBar,
            showprogressbar     : me.showProgressBar,
            updateprogressbar   : me.updateProgressBar,
            scope               : me
        });
    },

    isValid : function () {
        var me  = this;
        if (me.rangeField.getValue() === 'date') return me.dateFromField.isValid() && me.dateToField.isValid();

        return true;
    },

    getValues : function (asString, dirtyOnly, includeEmptyText, useDataValues) {
        var result      = this.callParent(arguments);

        var cellSize    = this.resizePicker.getValues();
        if (!asString) {
            result.cellSize = cellSize;
        } else {
            result += '&cellSize[0]='+cellSize[0]+'&cellSize[1]='+cellSize[1];
        }

        return result;
    },

    createFields : function () {
        var me                  = this,
            cfg                 = me.dialogConfig,
            beforeLabelTextTpl  = '<table class="sch-fieldcontainer-label-wrap"><td width="1" class="sch-fieldcontainer-label">',
            afterLabelTextTpl   = '<td><div class="sch-fieldcontainer-separator"></div></table>';

        me.rangeField = new Ext.form.field.ComboBox({
            value           : cfg.defaultConfig.range,
            triggerAction   : 'all',
            cls             : 'sch-export-dialog-range',
            forceSelection  : true,
            editable        : false,
            fieldLabel      : cfg.rangeFieldLabel,
            name            : 'range',
            queryMode       : 'local',
            displayField    : 'name',
            valueField      : 'value',
            store           : new Ext.data.Store({
                fields  : ['name', 'value'],
                data    : [
                    { name : cfg.completeViewText,  value : 'complete' },
                    { name : cfg.dateRangeText,     value : 'date' },
                    { name : cfg.currentViewText,   value : 'current' }
                ]
            }),
            listeners      : {
                change  : me.onRangeChange,
                scope   : me
            }
        });

        // col/row resizer
        me.resizePicker = new Sch.widget.ResizePicker({
            dialogConfig    : cfg,
            margin          : '10 20'
        });

        me.resizerHolder    = new Ext.form.FieldContainer({
            fieldLabel          : cfg.scrollerDisabled ? cfg.adjustCols : cfg.adjustColsAndRows,
            labelAlign          : 'top',
            hidden              : true,
            labelSeparator      : '',
            beforeLabelTextTpl  : beforeLabelTextTpl,
            afterLabelTextTpl   : afterLabelTextTpl,
            layout              : 'vbox',
            defaults            : {
                flex        : 1,
                allowBlank  : false
            },
            items               : [me.resizePicker]
        });

        // from date
        me.dateFromField = new Ext.form.field.Date({
            fieldLabel  : cfg.dateRangeFromText,
            baseBodyCls : 'sch-exportdialogform-date',
            name        : 'dateFrom',
            format      : cfg.dateRangeFormat || Ext.Date.defaultFormat,
            allowBlank  : false,
            maxValue    : cfg.endDate,
            minValue    : cfg.startDate,
            value       : cfg.startDate
        });

        // till date
        me.dateToField = new Ext.form.field.Date({
            fieldLabel  : cfg.dateRangeToText,
            name        : 'dateTo',
            format      : cfg.dateRangeFormat || Ext.Date.defaultFormat,
            baseBodyCls : 'sch-exportdialogform-date',
            allowBlank  : false,
            maxValue    : cfg.endDate,
            minValue    : cfg.startDate,
            value       : cfg.endDate
        });

        // date fields holder
        me.datesHolder  = new Ext.form.FieldContainer({
            fieldLabel          : cfg.specifyDateRange,
            labelAlign          : 'top',
            hidden              : true,
            labelSeparator      : '',
            beforeLabelTextTpl  : beforeLabelTextTpl,
            afterLabelTextTpl   : afterLabelTextTpl,
            layout              : 'vbox',
            defaults            : {
                flex        : 1,
                allowBlank  : false
            },
            items               : [me.dateFromField, me.dateToField]
        });

        me.showHeaderField = new Ext.form.field.Checkbox({
            xtype       : 'checkboxfield',
            boxLabel    : me.dialogConfig.showHeaderLabel,
            name        : 'showHeader',
            checked     : !!cfg.defaultConfig.showHeaderLabel
        });

        me.exportToSingleField = new Ext.form.field.Checkbox({
            xtype       : 'checkboxfield',
            boxLabel    : me.dialogConfig.exportToSingleLabel,
            name        : 'singlePageExport',
            checked     : !!cfg.defaultConfig.singlePageExport
        });

        me.formatField = new Ext.form.field.ComboBox({
            value          : cfg.defaultConfig.format,
            triggerAction  : 'all',
            forceSelection : true,
            editable       : false,
            fieldLabel     : cfg.formatFieldLabel,
            name           : 'format',
            queryMode      : 'local',
            store          : ["A5", "A4", "A3", "Letter", "Legal"]
        });

        var orientationLandscapeCls = cfg.defaultConfig.orientation === "portrait" ? 'class="sch-none"' : '',
            orientationPortraitCls = cfg.defaultConfig.orientation === "landscape" ? 'class="sch-none"' : '';

        me.orientationField = new Ext.form.field.ComboBox({
            value          : cfg.defaultConfig.orientation,
            triggerAction  : 'all',
            baseBodyCls    : 'sch-exportdialogform-orientation',
            forceSelection : true,
            editable       : false,
            fieldLabel     : me.dialogConfig.orientationFieldLabel,
            afterSubTpl    : new Ext.XTemplate('<span id="sch-exportdialog-imagePortrait" ' + orientationPortraitCls +
                '></span><span id="sch-exportdialog-imageLandscape" ' + orientationLandscapeCls + '></span>'),
            name           : 'orientation',
            displayField   : 'name',
            valueField     : 'value',
            queryMode      : 'local',
            store          : new Ext.data.Store({
                fields : ['name', 'value'],
                data   : [
                    { name : cfg.orientationPortraitText, value : 'portrait' },
                    { name : cfg.orientationLandscapeText, value : 'landscape' }
                ]
            }),
            listeners      : {
                change : function (field, newValue) {
                    switch (newValue) {
                        case 'landscape':
                            Ext.fly('sch-exportdialog-imagePortrait').toggleCls('sch-none');
                            Ext.fly('sch-exportdialog-imageLandscape').toggleCls('sch-none');
                            break;
                        case 'portrait':
                            Ext.fly('sch-exportdialog-imagePortrait').toggleCls('sch-none');
                            Ext.fly('sch-exportdialog-imageLandscape').toggleCls('sch-none');
                            break;
                    }
                }
            }
        });
    },

    createProgressBar : function () {
        return this.progressBar = new Ext.ProgressBar({
            text    : this.config.progressBarText,
            animate : true,
            hidden  : true,
            margin  : '4px 0 10px 0'
        });
    },

    onRangeChange : function (field, newValue) {
        switch (newValue) {
            case 'complete':
                this.datesHolder.hide();
                this.resizerHolder.hide();
                break;
            case 'date':
                this.datesHolder.show();
                this.resizerHolder.hide();
                break;
            case 'current':
                this.datesHolder.hide();
                this.resizerHolder.show();
                this.resizePicker.expand(true);
                break;
        }
    },

    showProgressBar : function () {
        if (this.progressBar) this.progressBar.show();
    },

    hideProgressBar : function () {
        if (this.progressBar) this.progressBar.hide();
    },

    updateProgressBar : function (value) {
        if (this.progressBar) this.progressBar.updateProgress(value);
    }
});
