var tarefaAlteracao;

var ExampleDefaults = {
    width: '100%',
    height: 500
};
Ext.ns('App');

Ext.define('CDISTask', {
    extend: 'Gnt.model.Task',
    clsField: 'TaskStatusClass',
    fields: [
        { name: 'TaskStatusClass', type: 'string' }
    ]
});

Ext.onReady(function () {
    
    if (Ext.data && Ext.data.Types) {
        Ext.data.Types.stripRe = /[\$,%]/g;
    }

    if (Ext.Date) {
        Ext.Date.monthNames = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

        Ext.Date.getShortMonthName = function (month) {
            return Ext.Date.monthNames[month].substring(0, 3);
        };

        Ext.Date.monthNumbers = {
            Jan: 0,
            Feb: 1,
            Mar: 2,
            Apr: 3,
            May: 4,
            Jun: 5,
            Jul: 6,
            Aug: 7,
            Sep: 8,
            Oct: 9,
            Nov: 10,
            Dec: 11
        };

        Ext.Date.getMonthNumber = function (name) {
            return Ext.Date.monthNumbers[name.substring(0, 1).toUpperCase() + name.substring(1, 3).toLowerCase()];
        };

        Ext.Date.dayNames = ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"];

        Ext.Date.getShortDayName = function (day) {
            return Ext.Date.dayNames[day].substring(0, 3);
        };

        Ext.Date.parseCodes.S.s = "(?:st|nd|rd|th)";
    }

    if (Ext.util && Ext.util.Format) {
        Ext.apply(Ext.util.Format, {
            thousandSeparator: ',',
            decimalSeparator: '.',
            currencySign: '$',
            dateFormat: 'd/m/Y'
        });
    }

    App.Gantt.init();
});

Ext.require([
    'Gnt.panel.Gantt',
    'Gnt.column.PercentDone',
    'Gnt.column.StartDate',
    'Gnt.column.EndDate'
]);

Ext.require('Gnt.locale.EN');

Ext.Loader.loadScript({
    url: '../../scripts/basicBryNTum/global.js',
    onLoad: function () {
        // after ExtJs locale is applied we invoke rendering
        // (when Ext will load all the required classes)
        Ext.onReady(App.Gantt.init, App.Gantt);
    }
});

App.Gantt = {

    // Initialize application
    init: function (serverCfg) {

        var preset = Sch.preset.Manager.get('monthAndYear');

        preset.timeColumnWidth = 150;
        preset.displayDateFormat = 'd/m/Y';
        delete preset.headerConfig.bottom;

        var taskStore = Ext.create("Gnt.data.TaskStore", {
            model: 'CDISTask',
            plugins: [
                Ext.create("Gnt.plugin.Printable")
            ],
            proxy: {
                type: 'ajax',
                method: 'GET',
                url: urlXML,
                reader: {
                    type: 'xml',
                    // records will have a 'Task' tag
                    record: ">Task",
                    root: "Tasks"
                }
            }
        });
        

        var g = Ext.create('Gnt.panel.Gantt', {
            height: 200,
            width: ExampleDefaults.width,
            renderTo: 'basicGantt',
            highlightWeekends: true,
            loadMask: true,
            rowHeight: 30,
            resizeConfig: {
                showDuration: false
            },

            viewConfig: {
                focusedItemCls: 'row-focused',
                selectedItemCls: 'row-selected',
                trackOver: true
            },
            
            enableProgressBarResize: false,
            enableDependencyDragDrop: false,
            snapToIncrement : false,
            startDate: dataInicio,
            endDate: dataTermino,
            viewPreset: 'monthAndYear',

            eventRenderer: function (taskRecord) {
                
                return {
                    ctcls: "Id-" + taskRecord.get('Id') // Add a CSS class to the task container element

                };
            },

            tooltipTpl: new Ext.XTemplate(
                '<ul class="taskTip">',
                    '<li><strong>Projeto:</strong>{Name}</li>',
                    '<li><strong>Início:</strong>{[values._record.getDisplayStartDate("d/m/y")]}</li>',
                    '<li><strong>% Concluído:</strong>{PercentDone}%</li>',
                '</ul>'
            ).compile(),



            // Setup your static columns
            columns: [
                {
                    xtype: 'namecolumn',
                    width: 450,
                    text: 'Projeto',
                    renderer: onRender
                },
                {
                    xtype: 'startdatecolumn',
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    width: 90,
                    text: 'Início',
                    align: 'center',
                    renderer: onRenderDate
                },
                {
                    xtype: 'enddatecolumn',
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    width: 90,
                    text: 'Término',
                    align: 'center',
                    renderer: onRenderDate
                }
            ],

            taskStore: taskStore,

            listeners: {

                // Setup a time header tooltip after rendering
                render: function (view) {
                    var header = view.getSchedulingView().headerCt;

                    view.tip = Ext.create('Ext.tip.ToolTip', {
                        // The overall target element.
                        target: header.id,
                        // Each grid row causes its own separate show and hide.
                        delegate: '.sch-simple-timeheader',
                        showDelay: 0,
                        trackMouse: true,
                        anchor: 'bottom',

                        //to see different date formats, see http://docs.sencha.com/ext-js/4-1/#!/api/Ext.Date
                        //dateFormat: 'Y-m-d',
                        dateFormat: 'd/m/y,',
                        renderTo: 'basicGantt',
                        listeners: {
                            // Change content dynamically depending on which element triggered the show.
                            beforeshow: function (tip) {
                                var el = Ext.get(tip.triggerElement),
                                    position = el.getXY(),
                                    date = view.getSchedulingView().getDateFromXY(position);

                                //update the tip with date
                                tip.update(Ext.Date.format(date, tip.dateFormat));
                            }
                        }
                    });


                }
            }
        });

        

        g.setReadOnly(true);
        g.setHeight(window.innerHeight - 130);
        Ext.QuickTips.init();
    }
    
};

function copyTask(c, g) {    
 var b = g.getTaskStore().model;
 var a = new b({
     leaf: true
 });
    
 a.setPercentDone(0);
 a.setName('');
 a.set(a.startDateField, (c && c.getStartDate()) || null);
 a.set(a.endDateField, (c && c.getEndDate()) || null);

return a

}

function onRender(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass.trim();
    return value;
}

function onRenderDate(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass.trim();
    return value === '-' ? value : Ext.util.Format.date(value, 'd/m/Y');// + '1'//Ext.util.Format.dateRenderer('d/m/Y'),
}

function onRenderEnumerate(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass.trim();
    return index;
}

