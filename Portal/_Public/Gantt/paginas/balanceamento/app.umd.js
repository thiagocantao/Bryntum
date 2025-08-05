var {
    Toolbar,
    Toast,
    DateHelper,
    CSSHelper,
    Column,
    ColumnStore,
    TaskModel,
    Gantt
} = bryntum.gantt;


var getHeadersProject = function () {

    var headers = {
        typeResult: "jsonProject",
        idEntidade: idEntidade,
        idPortfolio: idPortfolio,
        numCenario: numCenario
    };
    gantt.project.transport.load.requestConfig.headers = headers;
    return headers;
}

this.isFullscreen = false;
const gantt = new Gantt({
    appendTo: 'container',
    locale: 'pt-BR',
    loadMask: getTraducao('carregando___'),
    project: {
        //loadUrl: '../_datasets/calendars.json',
        transport: {
            load: {
                //url: '../../../..'+resultGantt
                requestConfig: {
                    url: '../../../../ApiHandler/GanttBalanceamentoHandler.ashx',
                    method: 'POST',
                    // get rid of cache-buster parameter
                    disableCaching: false,
                    //paramName: 'rq',                           
                    // custom request headers
                    headers: {
                        typeResult: "jsonProject",
                        idEntidade: idEntidade,
                        idPortfolio: idPortfolio,
                        numCenario: numCenario
                    }
                }
            }
        },
        autoLoad: true
    },
    tickSize: 50,
    columns: columns,
    subGridConfigs: {
        locked: {
            flex: 3
        },
        normal: {
            flex: 4
        }
    },
    features: {
        taskNonWorkingTime: {
            tooltipTemplate({
                name,
                startDate,
                endDate,
                iconCls
            }) {
                return `                   
                    <p class="b-nonworkingtime-tip-title">${iconCls ? `<i class="${iconCls}"></i>` : ''}${name || 'Non-working time'}</p>
                    ${DateHelper.format(startDate, 'L')} - ${DateHelper.format(endDate, 'L')}
                `;
            }
        },
        nonWorkingTime: {
            disabled: true
        },
        taskMenu: {
            disabled: true
        },
        percentBar: false,
        progressLine: {
            disabled: true,
            statusDate: new Date(2019, 0, 25)
        }
    },
    tbar: [
        {
            ref: 'custom',
            color: 'b-orange',
            text: getTraducao('mostrar_gr_ficos'),
            icon: 'b-fa-search',
            toggleable: false,
            onClick() {
                window.location.href = '../../../../_Portfolios/frameSelecaoBalanceamento_Simulacao.aspx?NumCenario=' + numCenario;
            }
        },
        {
            type: 'buttonGroup',
            items: [
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'zoomInButton',
                    icon: 'b-fa b-fa-search-plus',
                    onClick() {
                        gantt.zoomIn();
                    }
                }, {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'zoomOutButton',
                    icon: 'b-fa b-fa-search-minus',
                    onClick() {
                        gantt.zoomOut();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'fullScreenButton',
                    icon: 'b-icon b-icon-fullscreen',
                    cls: 'b-blue b-raised',
                    onClick() {
                        if (!this.isFullscreen) {
                            this.isFullscreen = true;
                            bryntum.gantt.Fullscreen.request(document.documentElement);
                        } else {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                    }
                }]
        },
        {
            ref: 'cenario',
            type: 'combo',
            label: getTraducao('cenario') + ':',
            value: numCenario,
            inputWidth: '15.5em',
            editable: false,
            items: [
                { value: 1, text: getTraducao('cenario') + ' 1' },
                { value: 2, text: getTraducao('cenario') + ' 2' },
                { value: 3, text: getTraducao('cenario') + ' 3' },
                { value: 4, text: getTraducao('cenario') + ' 4' },
                { value: 5, text: getTraducao('cenario') + ' 5' },
                { value: 6, text: getTraducao('cenario') + ' 6' },
                { value: 7, text: getTraducao('cenario') + ' 7' },
                { value: 8, text: getTraducao('cenario') + ' 8' },
                { value: 9, text: getTraducao('cenario') + ' 9' }
            ],
            onChange({
                value
            }) {
                numCenario = value;
                getHeadersProject();
                gantt.project.load();
            }
        }, {
            ref: 'custom',
            color: 'b-green',
            text: getTraducao('selecionar'),
            icon: 'b-fa-search',
            toggleable: false,
            onClick() {
                gantt.project.load();
            }
        },
     
    ],
   
});

gantt.project.load().then(function () {
    // Adaptar tamanho
    gantt.zoomToFit({
        leftMargin: 100,
        rightMargin: 100
    });
    var stm = gantt.project.stm;
    stm.disable();
    stm.autoRecord = true;
    stm.disable = true;
    stm.getTransactionTitle = function (transaction) {
        // your custom code to analyze the transaction and return custom transaction title
        var lastAction = transaction.queue[transaction.queue.length - 1];

        if (lastAction instanceof AddAction) {
            var title = 'Add new record';
        }

        return title;
    };

    project.on('schedulingconflict', function (context) {
        // show notification to user
        //bryntum.gantt.Toast.show('Scheduling conflict has happened ..recent changes were reverted'); // as the conflict resolution approach let's simply cancel the changes        
        context.continueWithResolutionResult(bryntum.gantt.EffectResolutionResult.Cancel);
    });
    
});
