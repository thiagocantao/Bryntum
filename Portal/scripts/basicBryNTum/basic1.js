var tarefaAlteracao;

var ExampleDefaults = {
    width: '100%',
    height: 500
};
Ext.ns('App');


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
            model: 'Gnt.model.Task',
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
        var dependencyStore = Ext.create("Gnt.data.DependencyStore", {
            allowedDependencyTypes: ['EndToStart'],
            autoLoad: true,
            proxy: {
                type: 'ajax',
                url: urlXMLDep,
                method: 'GET',
                reader: {
                    type: 'xml',
                    root: 'Links',
                    record: 'Link' // records will have a 'Link' tag
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
                    '<li><strong>Tarefa:</strong>{Name}</li>',
                    '<li><strong>Início:</strong>{[values._record.getDisplayStartDate("d/m/y")]}</li>',
                    '<li><strong>Duração:</strong> {Duration}d</li>',
                    '<li><strong>% Concluído:</strong>{PercentDone}%</li>',
                '</ul>'
            ).compile(),
/*
            tbar: [
                {
                    tooltip: 'Anterior',
                    iconCls: 'iconAnterior',
                    scope: this,
                    handler: function () {
                        g.shiftPrevious();
                    }
                },
                {
                    tooltip: 'Próximo',
                    iconCls: 'iconProximo',
                    scope: this,
                    handler: function () {
                        g.shiftNext();
                    }
                },
                {
                    tooltip: 'Fechar Todos',
                    iconCls: 'iconFechar',
                    scope: this,
                    handler: function () {
                        g.collapseAll();
                    }
                },
                {
                    tooltip: 'Expandir Todos',
                    iconCls: 'iconExpandir',
                    scope: this,
                    handler: function () {
                        g.expandAll();
                    }
                },
                {
                    iconCls: 'iconIncluir',
                    tooltip: 'Incluir',
                    enableToggle: true,
                    menu : {
                        plain : true,
                        items : [
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                            newTask = copyTask(task, g);

                                        task.addTaskAbove(newTask);

                                        tarefaAlteracao = newTask;

                                        txtTarefa.SetText(newTask.get('Name'));
                                        txtPercentual.SetValue(newTask.get('PercentDone'));
                                        ddlInicio.SetValue(newTask.get('StartDate'));
                                        ddlTermino.SetValue(newTask.get('EndDate'));
                                        pcEdicao.Show();
                                    })
                                },
                                scope : this,
                                text : 'Tarefa Acima'
                            },
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                        newTask = copyTask(task, g),
                                        insertIndex;

                                        if (task.data.leaf == true) {
                                            insertIndex = s.indexOf(task) + 1;
                                        } else {
                                            //var sibling = s.getNodeNextSibling(task);

                                            //insertIndex = sibling ? s.indexOf(sibling) : s.getCount();
                                            insertIndex = s.getCount();
                                        }
                                        task.addTaskBelow(newTask);

                                        tarefaAlteracao = newTask;

                                        txtTarefa.SetText(newTask.get('Name'));
                                        txtPercentual.SetValue(newTask.get('PercentDone'));
                                        ddlInicio.SetValue(newTask.get('StartDate'));
                                        ddlTermino.SetValue(newTask.get('EndDate'));
                                        pcEdicao.Show();
                                    })
                                    
                                },
                                scope : this,
                                text : 'Tarefa Abaixo'
                            },
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                           newMilestone = copyTask(task, g);
                                        newMilestone.set('StartDate', newMilestone.get('EndDate'));
                                       //task.addMilestone(newMilestone);

                                        tarefaAlteracao = newMilestone;

                                        txtTarefa.SetText(newMilestone.get('Name'));
                                        txtPercentual.SetValue(newMilestone.get('PercentDone'));
                                        ddlInicio.SetValue(newMilestone.get('StartDate'));
                                        ddlTermino.SetValue(newMilestone.get('EndDate'));
                                        pcEdicao.Show();
                                    })
                                },
                                scope : this,
                                text : 'Marco'
                            },
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                        newTask = copyTask(task, g);

                                        task.set('leaf', false);
                                        task.addSubtask(newTask);

                                        tarefaAlteracao = newTask;

                                        txtTarefa.SetText(newTask.get('Name'));
                                        txtPercentual.SetValue(newTask.get('PercentDone'));
                                        ddlInicio.SetValue(newTask.get('StartDate'));
                                        ddlTermino.SetValue(newTask.get('EndDate'));
                                        pcEdicao.Show();
                                    })
                                },
                                scope : this,
                                text : 'Sub Tarefa'
                            },
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                        depStore = g.dependencyStore,
                                        newTask = copyTask(task, g);

                                        newTask.set('StartDate', task.get('EndDate'));
                                        newTask.set('EndDate', Sch.util.Date.add(task.get('EndDate'), Sch.util.Date.DAY, 1));

                                        task.addTaskBelow(newTask);
                                        depStore.add(new depStore.model({
                                            From: task,
                                            To: newTask,
                                            Type: 2
                                        })
                                        );

                                        tarefaAlteracao = newTask;

                                        txtTarefa.SetText(newTask.get('Name'));
                                        txtPercentual.SetValue(newTask.get('PercentDone'));
                                        ddlInicio.SetValue(newTask.get('StartDate'));
                                        ddlTermino.SetValue(newTask.get('EndDate'));
                                        pcEdicao.Show();
                                    })
                                },
                                scope : this,
                                text : 'Sucessora'
                            },
                            {
                                handler: function (btn) {
                                    g.getSelectionModel().selected.each(function (task) {
                                        var s = task.store,
                                    depStore = g.dependencyStore,
                                    newTask = copyTask(task, g),
                                    newEnd = task.get('StartDate');

                                        newTask.set('EndDate', newEnd);
                                        newTask.set('StartDate', Sch.util.Date.add(newEnd, Sch.util.Date.DAY, -1));
                                        this.addTaskAbove(newTask);

                                        var a = new depStore.model({

                                            From: newTask,

                                            To: this,
                                            type: depStore.model.Type.EndToStart

                                        });

                                        depStore.add(a);

                                        tarefaAlteracao = newTask;

                                        txtTarefa.SetText(newTask.get('Name'));
                                        txtPercentual.SetValue(newTask.get('PercentDone'));
                                        ddlInicio.SetValue(newTask.get('StartDate'));
                                        ddlTermino.SetValue(newTask.get('EndDate'));
                                        pcEdicao.Show();

                                    })
                                },
                                scope : this,
                                text : 'Predecessora'
                            }
                        ]
                    } 
                },
                {
                    iconCls: 'iconEditar',
                    tooltip: 'Editar',
                    enableToggle: true,
                    handler: function (btn) {
                        g.getSelectionModel().selected.each(function (task) {
                            tarefaAlteracao = task;

                            txtTarefa.SetText(task.get('Name'));
                            txtPercentual.SetValue(task.get('PercentDone'));
                            ddlInicio.SetValue(task.get('StartDate'));
                            ddlTermino.SetValue(task.get('EndDate'));
                            pcEdicao.Show();
                        })
                    }
                },
                {
                    iconCls: 'iconExcluir',
                    tooltip: 'Excluir',
                    enableToggle: true,
                    handler: function (btn) {
                        g.getSelectionModel().selected.each(function (task) {
                            task.remove();
                        })
                    }
                },
                 {
                     tooltip: 'Salvar',
                     iconCls: 'iconSalvar',
                     handler: function () {
                         var insertedTasks = taskStore.getNewRecords()
                         var updatedTasks = taskStore.getModifiedRecords();
                         var removedTasks = taskStore.getRemovedRecords();

                         var data = {
                             tasks: {
                                 inserted: insertedTasks,
                                 updated: updatedTasks,
                                 removed: removedTasks
                             }
                         }
                         
                         salvaDados(insertedTasks, updatedTasks, removedTasks);

                     }

                 },
            {
                tooltip: 'Aumentar Recuo',
                iconCls: 'iconIdentar',
                enableToggle: true,
                handler: function (btn) {
                    g.taskStore.indent(g.getSelectionModel().getSelection());
                }
            },
                {
                    tooltip: 'Diminuir Recuo',
                    iconCls: 'iconDesidentar',
                    enableToggle: true,
                    handler: function (btn) {
                        g.taskStore.outdent(g.getSelectionModel().getSelection());
                    }
                }

            ],*/


            // Setup your static columns
            columns: [
                {
                    xtype: 'namecolumn',
                    width: 450,
                    text: 'Tarefa'
                },
                {
                    xtype: 'startdatecolumn',
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    width: 90,
                    text: 'Início',
                    align : 'center'
                },
                {
                    xtype: 'enddatecolumn',
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    width: 90,
                    text: 'Término',
                    align: 'center'
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
        g.setHeight(window.innerHeight - 30);
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