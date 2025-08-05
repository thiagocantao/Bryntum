var tarefaAlteracao;
var tarefaSelecionada = -1;
var gantt;
var ExampleDefaults = {
    width: 'auto',
    height: 500
};

Ext.ns('App');

Ext.define('CDISTask', {
    extend: 'Gnt.model.Task',
    clsField: 'TaskStatusClass',
    fields: [
        { name: 'PercentualPrevisto', type: 'string' },
        //{ name: 'PercentDone', type: 'string' },
        { name: 'ValorPesoTarefaLB', type: 'string' },
        { name: 'PercentualPesoTarefa', type: 'string' },
        { name: 'Trabalho', type: 'string' },
        { name: 'TaskStatusClass', type: 'string' },
        { name: 'TerminoReal', type: 'string' },
        { name: 'PDone', type: 'string' },
        { name: 'Duracao', type: 'string' },
        { name: 'CodigoRealTarefa', type: 'string' }
    ]
});

Ext.onReady(function () {

    if (Ext.data && Ext.data.Types) {
        Ext.data.Types.stripRe = /[\$,%]/g;
    }

    if (Ext.Date) {
        Ext.Date.monthNames = JSON.parse(traducao.geral_meses);

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

        Ext.Date.dayNames = JSON.parse(traducao.geral_dias_semana);

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

    //App.Gantt.init();
});

/*
Ext.Loader.setConfig({
    paths: {
        Sch: '../../scripts/basicBryNTum',
        Gnt: '../../scripts/basicBryNTum'
    }
});

Ext.Loader.loadScript({
    url: '../../scripts/basicBryNTum/locale/' + 'It' + '.js'
});

if (idioma.substr(0, 2).toLowerCase() == "en") {
    var idiomaBryntum = "Gnt.locale.En";
} else {
    var idiomaBryntum = "Gnt.locale.PtBR";
}
var idiomaBryntum = "Sch.locale.It";
var idiomaBryntum2 = "Gnt.locale.It";

Ext.require([
  'Gnt.panel.Gantt',
  'Gnt.column.PercentDone',
  'Gnt.column.StartDate',
  'Gnt.column.BaselineStartDate',
  'Gnt.column.BaselineEndDate',
  'Gnt.column.EndDate',
  idiomaBryntum,
  idiomaBryntum2,
  "locale.It"
]);

Ext.define('Gnt.locale.It', {
    extend: 'Gnt.locale.Locale',
    singleton: true,
    requires: 'Gnt.locale.It',
    apply: function () {
        Sch.locale.It.apply(Sch.locale.It, arguments);
        this.callParent(arguments);
    }
});

Ext.application({
    requires: [
        'Sch.locale.It',
        'Gnt.locale.It'
    ],
    launch: function () {
        Sch.locale['It'].apply();
        Gnt.locale['It'].apply();
    }
});
*/

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
        try {
            var preset = Sch.preset.Manager.get('monthAndYear');

            preset.timeColumnWidth = 150;
            preset.displayDateFormat = 'd/m/Y';
            delete preset.headerConfig.bottom;

            var taskStore = Ext.create("Gnt.data.TaskStore", {
                model: 'CDISTask',
                autoLoad: true,
                proxy: {
                    type: 'ajax',
                    url: urlJSON
                }
            });

            var dependencyStore = Ext.create("Gnt.data.DependencyStore", {
                autoLoad: true,
                proxy: {
                    type: 'ajax',
                    url: urlJSONDep
                }
            });

            var g = Ext.create('Gnt.panel.Gantt', {
                height: 200,
                width: 'auto',
                monitorResize: true,
                autoWidth: true,
                renderTo: 'basicGantt',
                highlightWeekends: true,
                loadMask: true,
                rowHeight: 30,
                resizeConfig: {
                    showDuration: false
                },
                lockedGridConfig: { width: 401 },
                viewConfig: {
                    focusedItemCls: 'row-focused',
                    selectedItemCls: 'row-selected',
                    trackOver: true
                },
                enableBaseline: true,
                // show baselines from start
                baselineVisible: true,
                enableProgressBarResize: false,
                enableDependencyDragDrop: false,
                snapToIncrement: false,
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
                    '<li><strong>' + traducao.basic_tarefa + ':</strong>{Name}</li>',
                    '<li><strong>' + traducao.basic_in_cio + ':</strong>{[values._record.getDisplayStartDate("d/m/y")]}</li>',
                    '<li><strong>' + traducao.basic_dura__o + ':</strong> {Duration}d</li>',
                    '<li><strong>% ' + traducao.basic_conclu_do + ':</strong>{PercentDone}%</li>',
                    '</ul>'
                ).compile(),

                // Setup your static columns
                columns: [
                    {
                        dataIndex: 'Id',
                        text: '#',
                        width: 45,
                        align: 'center',
                        renderer: onRenderEnumerate
                    },
                    {
                        xtype: 'namecolumn',
                        width: 450,
                        text: traducao.basic_tarefa,
                        renderer: onRender
                    },
                    {
                        dataIndex: 'BaselineStartDate',
                        width: 120,
                        text: traducao.basic_in_cio_lb,
                        align: 'center',
                        renderer: onRenderDate
                    },
                    {
                        // column with baseline end date of a task
                        dataIndex: 'BaselineEndDate',
                        width: 120,
                        text: traducao.basic_t_rmino_lb,
                        align: 'center',
                        renderer: onRenderDate
                    },
                    {
                        dataIndex: 'PercentualPrevisto',
                        text: traducao.basic_previsto,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'PDone',
                        width: 120,
                        text: traducao.basic_realizado,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'ValorPesoTarefaLB',
                        width: 120,
                        text: traducao.basic_peso_lb,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'PercentualPesoTarefa',
                        text: traducao.basic___peso,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'Duracao',
                        text: traducao.basic_duracao__d_,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'Trabalho',
                        text: traducao.basic_trabalho__h_,
                        align: 'center',
                        renderer: onRender
                    },
                    {
                        dataIndex: 'StartDate',
                        width: 90,
                        text: traducao.basic_in_cio,
                        align: 'center',
                        renderer: onRenderDate
                    },
                    {
                        dataIndex: 'EndDate',
                        width: 90,
                        text: traducao.basic_t_rmino,
                        align: 'center',
                        renderer: onRenderDate
                    },
                    {
                        dataIndex: 'TerminoReal',
                        width: 90,
                        text: traducao.basic_t_rmino_real,
                        align: 'center',
                        renderer: onRenderDate
                    }
                ],
                tbar: [
                    {
                        tooltip: "- Zoom",
                        cls: 'iconzoom-out',
                        height: 22,
                        handler: function () {
                            g.zoomOut();
                        }
                    },
                    {
                        tooltip: "+ Zoom",
                        cls: 'iconzoom-in',
                        height: 22,
                        handler: function () {
                            g.zoomIn();
                        }
                    },
                    {
                        tooltip: "Zoom to Fit",
                        cls: 'iconzoom-to-fit',
                        height: 22,
                        handler: function () {
                            g.zoomToFit();
                        }

                    }
                ],

                taskStore: taskStore,
                dependencyStore: dependencyStore,

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


                    },

                    cellclick: function (table, td, cellIndex, record, tr, rowIndex, e, eOpts) {
                        tarefaSelecionada = record.data.CodigoRealTarefa;
                    }
                }
            });

            gantt = g;

            g.setReadOnly(true);
            g.setHeight(window.innerHeight - 55);
            Ext.QuickTips.init();
        }
        catch(e) { }
        


    },

    refresh: function () {
        gantt.getView().refresh();
    }
};

function onRender(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass;
    return value;
}

function onRenderDate(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass;
    return value === '-' ? value : Ext.util.Format.date(value, 'd/m/Y');// + '1'//Ext.util.Format.dateRenderer('d/m/Y'),
}

function onRenderEnumerate(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass;
    return index;
}

function onRenderDuracao(value, meta, record, index) {
    meta.tdCls = record.data.TaskStatusClass;
    return record.data.Duration;
}

function copyTask(c, g) {
    var b = g.getTaskStore().model;
    var a = new b({
        leaf: true
    });

    a.setPercentDone(0);
    a.setName('');
    a.set(a.startDateField, (c && c.getStartDate()) || null);
    a.set(a.endDateField, (c && c.getEndDate()) || null);

    return a;

}

function salvaDados(tarefasInseridas, tarefasEditadas, tarefasExcluidas) {
    var inseridas = '';
    var editadas = '';
    var excluidas = '';

    for (i = 0; i < tarefasInseridas.length; i++) {
        if (i == tarefasInseridas.length - 1)
            inseridas += JSON.stringify(tarefasInseridas[0].data);
        else
            inseridas += JSON.stringify(tarefasInseridas[0].data) + ',';
    }

    for (i = 0; i < tarefasEditadas.length; i++) {
        if (i == tarefasEditadas.length - 1)
            editadas += JSON.stringify(tarefasEditadas[0].data);
        else
            editadas += JSON.stringify(tarefasEditadas[0].data) + ',';
    }

    for (i = 0; i < tarefasExcluidas.length; i++) {
        if (i == tarefasExcluidas.length - 1)
            excluidas += JSON.stringify(tarefasExcluidas[0].data);
        else
            excluidas += JSON.stringify(tarefasExcluidas[0].data) + ',';
    }

    var param = inseridas + '¥' + editadas + '¥' + excluidas;

    callbackSalvar.PerformCallback(param);
}

function salvaTarefa() {
    tarefaAlteracao.set('Name', txtTarefa.GetText());
    tarefaAlteracao.set('PercentDone', txtPercentual.GetValue());
    tarefaAlteracao.set('StartDate', ddlInicio.GetValue());
    tarefaAlteracao.set('EndDate', ddlTermino.GetValue());
    pcEdicao.Hide();
}