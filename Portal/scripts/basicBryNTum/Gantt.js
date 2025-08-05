var tarefaAlteracao;

var ExampleDefaults = {
  width: '100%',
  height: 500
};

var gantt = null;

Ext.ns('App');

Ext.define('CDISTask', {
  extend: 'Gnt.model.Task',
  clsField: 'TaskStatusClass',
  fields: [
    { name: 'TaskStatusClass', type: 'string' },
    { name: 'PDone', type: 'string' }
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
      autoLoad: true,
      proxy: {
        type: 'ajax',
        url: urlJSON
      }
    });

    var dependencyStore = Ext.create("Gnt.data.DependencyStore", {
      allowedDependencyTypes: ['EndToStart'],
      autoLoad: true,
      proxy: {
        type: 'ajax',
        url: urlJSONDep
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
      //viewPreset: {
      //  timeColumnWidth: 100,
      //  name: 'weekAndDayLetter',
      //  headerConfig: {
      //    middle: {
      //      unit: 'w',
      //      dateFormat: 'D d M Y'
      //    }
      //  }
      //},
      viewPreset: 'monthAndYear',

      eventRenderer: function (taskRecord) {
        return {
          ctcls: "Id-" + taskRecord.get('Id') // Add a CSS class to the task container element
        };
      },

      tooltipTpl: new Ext.XTemplate(
        '<ul class="taskTip">',
        '<li>{Name}</li>',
        '<li><strong>' + traducao.Gantt_in_cio + ':</strong>{[values._record.getDisplayStartDate("d/m/y")]}</li>',
        '<li><strong>% ' + traducao.Gantt_conclu_do + ':</strong>{PercentDone}%</li>',
        '</ul>'
      ).compile(),

      // Setup your static columns
      columns: [
        {
          dataIndex: 'Id',
          text: ' ',
          width: 45,
          align: 'center',
          renderer: onRenderEnumerate
        },
        {
          xtype: 'namecolumn',
          width: 450,
          text: traducao.Gantt_descri__o,
          renderer: onRender
        },
        {
          dataIndex: 'StartDate',
          width: 90,
          text: traducao.Gantt_in_cio,
          align: 'center',
          renderer: onRenderDate
        },
        {
          dataIndex: 'EndDate',
          width: 90,
          text: traducao.Gantt_t_rmino,
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
          //,
          //{       
          //    itemId: 'viewFullScreen',
          //    reference : 'viewFullScreen',
          //    tooltip: "Full Screen",
          //    reference: 'viewFullScreen',
          //    cls: 'icon-fullscreen',
          //    height: 22,
          //    handler: function () {
          //        //showFullScreen(g)
          //    }               
          //}
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


        }
      }
    });



    g.setReadOnly(true);
    g.setHeight(window.innerHeight - 130);
    Ext.QuickTips.init();
  }
};

function onRender(value, meta, record, index) {
  //meta.tdCls = record.data.TaskStatusClass;
  return value;
}

function onRenderDate(value, meta, record, index){
 // meta.tdCls = record.data.TaskStatusClass;
  return value === '-' ? value : Ext.util.Format.date(value, 'd/m/Y');// + '1'//Ext.util.Format.dateRenderer('d/m/Y'),
}

function onRenderEnumerate(value, meta, record, index) {
   // meta.tdCls = record.data.TaskStatusClass;

    var corStatus = record.data.TaskStatusClass.toLowerCase();

    if (corStatus != "branco" && corStatus != "verde" && corStatus != "amarelo" && corStatus != "vermelho" && corStatus != "azul")
        corStatus = "";

    return corStatus == "" ? "" : "<img src='" + window.top.getPathUrl() + "imagens/" + corStatus + ".gif' />";
}

function onRenderDuracao(value, meta, record, index) {
  //meta.tdCls = record.data.TaskStatusClass;
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

  return a

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