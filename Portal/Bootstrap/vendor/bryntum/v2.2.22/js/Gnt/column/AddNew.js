/*

Ext Gantt 2.2.22
Copyright(c) 2009-2014 Bryntum AB
http://bryntum.com/contact
http://bryntum.com/license

*/
/**
@class Gnt.column.AddNew
@extends Ext.grid.column.Column

Shows an extra column allowing the user to add a new column for any field in the data model.

*/
Ext.define("Gnt.column.AddNew", {
    extend      : "Ext.grid.column.Column",

    alias       : "widget.addnewcolumn",

    requires    : [
        'Ext.form.field.ComboBox',
        'Ext.Editor'
    ],

    mixins      : ['Gnt.mixin.Localizable'],

    /**
     * @cfg {Object} l10n
     * A object, purposed for the class localization. Contains the following keys/values:

        - text  : 'Add new column...'
     */

    width       : 100,
    resizable   : false,
    sortable    : false,
    draggable   : false,

    colEditor   : null,

    /**
     * @cfg {Array} [columnList] An array of column definition objects. It should be a list containing data as seen below
     *
     *      [
     *          { clsName : 'Gnt.column.StartDate', text : 'Start Date' },
     *          { clsName : 'Gnt.column.Duration', text : 'Duration' },
     *          ...
     *      ]
     *
     * If not provided, a list containing all the columns from the `Gnt.column.*` namespace will be created.
     */
    columnList  : null,

    initComponent : function() {
        if (!this.text) this.text = this.L('text');

        this.addCls('gnt-addnewcolumn');

        this.on({
            headerclick         : this.myOnHeaderClick,
            headertriggerclick  : this.myOnHeaderClick,
            scope               : this
        });

        this.callParent(arguments);
    },

    getGantt : function () {
        if (!this.gantt) {
            this.gantt = this.up('ganttpanel');
        }

        return this.gantt;
    },

    getContainingPanel : function() {
        if (!this.panel) {
            this.panel = this.up('tablepanel');
        }

        return this.panel;
    },

    myOnHeaderClick : function() {
        if (!this.combo) {
            var panel = this.getContainingPanel();
            var list,
                me = this;

            if (this.columnList) {
                list = this.columnList;
            } else {
                list = Ext.Array.map(Ext.ClassManager.getNamesByExpression('Gnt.column.*'), function(name) {
                    var cls = Ext.ClassManager.get(name);

                    if (cls.prototype._isGanttColumn === false || me instanceof cls) return null;

                    return {
                        clsName : name,
                        text    : cls.prototype.localize ? cls.prototype.localize('text') : cls.prototype.text
                    };
                });

                list = Ext.Array.clean(list).sort(function(a, b) { return a.text > b.text ? 1 : -1; });
            }

            var editor = this.colEditor = new Ext.Editor({
                shadow      : false,
                updateEl    : false,
                itemId      : 'addNewEditor',

                // HACK: we need this editor to exist in the column header for scrolling of the grid
                renderTo    : this.el,
                offsets     : [20, 0],
                field       : new Ext.form.field.ComboBox({
                    displayField    : 'text',
                    valueField      : 'clsName',
                    hideTrigger     : true,
                    queryMode       : 'local',
                    listConfig      : {
                        itemId      : 'addNewEditorComboList',
                        minWidth    : 150
                    },
                    store           : new Ext.data.Store({
                        fields  : ['text', 'clsName'],
                        data    : list
                    }),
                    listeners : {
                        render  : function() {
                            this.on('blur', function(){
                                editor.cancelEdit();
                            });
                        },
                        select  : this.onSelect,
                        scope   : this
                    }
                })
            });
        }
        var titleEl = this.el.down('.' + Ext.baseCSSPrefix + 'column-header-text');
        this.colEditor.startEdit(titleEl, '');
        this.colEditor.field.setWidth(this.getWidth() - 20);

        this.colEditor.field.expand();

        return false;
    },

    onSelect : function(combo, records) {
        var rec             = records[0];
        var owner           = this.ownerCt;
        var clsName         = rec.get('clsName');
        var view            = this.getContainingPanel().getView();
        var hasRefreshed,
            checkerFn       = function() { hasRefreshed = true; };

        view.on('refresh', checkerFn);

        this.colEditor.cancelEdit();

        Ext.require(clsName, function() {
            var col = Ext.create(clsName);
            
            col.dataIndex       = this.getGantt().taskStore.model.prototype[ col.fieldProperty ];
            
            owner.insert(owner.items.indexOf(this), col);

            // Ext 4.2.1- doesn't refresh on header insert, 4.2.2+ does
            if (!hasRefreshed) {
                view.refresh();
            }
            view.un('refresh', checkerFn);

        }, this);
    }
});
