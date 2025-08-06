var {
    Toolbar,
    Toast,
    DateHelper,
    CSSHelper,
    Column,
    ColumnStore,
    ContextMenu,
    TaskModel,
    Gantt
} = bryntum.gantt;



var getHeadersProject = function () {

    var headers = {
        typeResult: "jsonProject",
        idprojeto: idProjeto,
        numLinhaBase: numLinhaBase
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
                    url: '../../../../ApiHandler/GanttHandler.ashx',
                    method: 'POST',
                    // get rid of cache-buster parameter
                    disableCaching: false,
                    //paramName: 'rq',                           
                    // custom request headers
                    headers: {
                        typeResult: "jsonProject",
                        idprojeto: idProjeto,
                        numLinhaBase: numLinhaBase
                    }
                }
            }
        },
        autoLoad: true
    },
    tickSize: 50,
    readonly: true,
    columns: columns,
    subGridConfigs: {
        locked: {
            flex: 3
        },
        normal: {
            flex: 4
        }
    },
    contextMenu: false,
    features: {
        progressLine: {
            disabled: true
        },
        taskMenu: {
            disabled: true
        },
        //contextMenu: {
        //    // Configuração do menu de contexto
        //    items: {
        //        customItem: {
        //            text: 'Item Personalizado',
        //            icon: 'b-icon b-icon-add' // Ícone opcional
        //            // ... outras configurações do item ...
        //        },
        //        // Adicione outros itens conforme necessário
        //    },
        //},
        //taskNonWorkingTime: {
        //    tooltipTemplate({
        //        nomeTarefa,
        //        startDate,
        //        endDate,
        //        iconCls
        //    }) {
        //        return `                   
        //            <p class="b-nonworkingtime-tip-title">${iconCls ? `<i class="${iconCls}"></i>` : ''}${nomeTarefa || 'Non-working time'}</p>
        //            ${DateHelper.format(startDate, 'L')} - ${DateHelper.format(endDate, 'L')}
        //        `;
        //    }
        //},
        filter: true,
        nonWorkingTime: {
            disabled: true
        },
        percentBar: false
    },
    tbar: [

        {
            type: 'buttonGroup',
            items: [
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'expandAllButton',
                    icon: 'b-fa b-fa-angle-double-down',
                    tooltip: getTraducao('RecursosHumanos_expandir_todos'),
                    onClick() {
                        gantt.expandAll();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'collapseAllButton',
                    icon: 'b-fa b-fa-angle-double-up',
                    tooltip: getTraducao('RecursosHumanos_contrair_todos'),
                    onClick() {
                        gantt.collapseAll();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'zoomInButton',
                    icon: 'b-fa b-fa-search-plus',
                    onClick() {
                        gantt.zoomIn();
                    }
                },
                {
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
                }
            ]
        },
        {
            type: 'buttonGroup',
            items: [
                {
                    ref: 'exportPdfButton',
                    color: 'b-red',
                    tooltip: getTraducao('mapaEstrategico_exportar_para_pdf'),
                    icon: 'b-icon b-fa b-fa-file-pdf',
                    toggleable: false,
                    onClick() {
                        var arrayTask = [];
                        gantt._store.allRecords.map(function (item) {
                            var row = {
                                id: 0,
                                edtcode: item.edtcode,
                                isCaminhoCriticoStr: item.isCaminhoCriticoStr,
                                name: item.name,
                                inicioLb: item.inicioLb,
                                terminoLb: item.terminoLb,
                                previsto: item.previsto,
                                realizado: item.realizado,
                                pesoLb: item.pesoLb,
                                peso: item.peso,
                                duracao: item.duracao,
                                trabalho: item.trabalho,
                                inicio: item.inicio,
                                isMarcoStr: item.isMarcoStr,
                                isAtrasoStr: item.isAtrasoStr,
                                termino: item.termino,
                                terminoReal: item.terminoReal
                            };
                            arrayTask.push(row);
                        });

                        var configAjax = {
                            url: '../../../../ApiHandler/GanttHandler.ashx',
                            method: 'POST',
                            data: JSON.stringify(arrayTask)
                        };

                        //isIE é uma função de ~/script/custom/util/browser.js
                        if (isIE()) {
                            configAjax.headers = { typeResult: "getHtmlGantt", idprojeto: idProjeto };
                            configAjax.success = function (data) {
                                var win = window.open('', '_blank', 'toolbar=yes,scrollbars=yes,resizable=yes');
                                win.document.write(data);
                                win.document.close();
                                win.focus();
                                win.print();
                                win.close();
                            }
                        } else {
                            configAjax.headers = { typeResult: "exportToPdf", idprojeto: idProjeto };
                            configAjax.xhrFields = { responseType: 'blob' };
                            configAjax.success = function (data) {
                                var a = document.createElement('a');
                                var url = window.URL.createObjectURL(data);
                                a.href = url;
                                a.download = 'ganttBrisk.pdf';
                                document.body.appendChild(a);
                                a.click();
                                a.remove();
                                window.URL.revokeObjectURL(url);
                            };
                        }
                        $.ajax(configAjax);
                    }
                },
                {
                    type: 'button',
                    color: 'b-red',
                    ref: 'criticalPathsButton',
                    icon: 'b-fa b-fa-fire',
                    tooltip: getTraducao('caminho_critico'),
                    toggleable: true,
                    onClick() {
                        incluirClassCaminhoCritico();
                    }
                }
            ]
        },
        {
            type: 'buttonGroup',
            items: [
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'editarCronograma',
                    icon: 'b-fa b-fa-edit',
                    tooltip: getTraducao('editar_cronograma'),
                    hidden: false,
                    onClick() {
                        if (this.isFullscreen) {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }

                        toggleEdit(true);
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'DesfazerAlteracoes',
                    icon: 'b-fa b-fa-rotate-left',
                    tooltip: "Desfazer",
                    disabled: true,
                    onClick() {
                        const stm = gantt.project.stm;
                        if (stm.canUndo) {
                            stm.undo();
                        }
                        updateUndoRedoButtons();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'RefazerAlteracoes',
                    icon: 'b-fa b-fa-rotate-right',
                    tooltip: "Refazer",
                    disabled: true,
                    onClick() {
                        const stm = gantt.project.stm;
                        if (stm.canRedo) {
                            stm.redo();
                        }
                        updateUndoRedoButtons();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'SalvarAlteracoes',
                    icon: 'b-fa b-fa-save',
                    tooltip: "Salvar",
                    disabled: true,
                    onClick() {
                        toggleEdit(false);
                    }
                }
            ]
        },
        {
            type: 'buttonGroup',
            items: [
                {
                    type: 'button',
                    color: 'b-green',
                    ref: 'AdicionarTarefa',
                    icon: 'b-fa b-fa-plus',
                    tooltip: "Nova tarefa",
                    disabled: true,
                    onClick() {

                    }
                }, {
                    type: 'button',
                    color: 'b-green',
                    ref: 'EditarTarefa',
                    icon: 'b-fa b-fa-edit',
                    tooltip: "Editar tarefa",
                    disabled: true,
                    onClick() {
                        if (this.isFullscreen) {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }

                        alert('AAA');
                    }
                },
                {
                    type: 'button',
                    color: 'b-green',
                    ref: 'visualizarInfoTarefaButton',
                    icon: 'b-fa b-fa-file-alt',
                    tooltip: "Detalhes da tarefa",
                    onClick() {

                        if (this.isFullscreen) {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }

                        if (gantt.selectedRecord) {
                            var dimensions = getDimension();

                            var codTarefa = gantt.selectedRecord.originalData.codTarefa;

                            var tarefaParam = 'CT=' + codTarefa;
                            var dataParam = '&Data=';
                            var idProjetoParam = '&IDProjeto=' + idProjeto;

                            window.top.showModal("PopUpCronograma.aspx?" + tarefaParam + idProjetoParam + dataParam, getTraducao('Cronograma_gantt_detalhes_da_tarefa'), 820, 550, "", null);
                        } else {
                            bryntum.gantt.Toast.show(getTraducao('Primeiro_selecione_a_tarefa_que_deseja_visualizar'));
                        }
                    }
                }]
        },
        {       type: 'buttonGroup',
                items: [                 
                {
                    type: 'button',
                    color: 'b-deep-orange',
                    ref: 'editEapButton',
                    icon: 'b-fa b-fa-sitemap',
                    tooltip: getTraducao('editar_eap'),
                    hidden: false,
                    onClick() {
                        if (this.isFullscreen) {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        'use strict';
                        var dimensions = getDimension();

                        var myArguments = new Object();
                        myArguments.param1 = '';
                        myArguments.param2 = '';

                        var retorno = function (codigoItem) {
                            callbackAtualizaTela.PerformCallback(codigoItem);
                        };

                        window.top.showModal("'" + baseUrlEAP + "&AM=RW&Altura='" + (dimensions.height - 40), 'Edição', dimensions.width, dimensions.height, retorno, myArguments);
                    }
                }, {
                    type: 'button',
                    color: 'b-deep-orange',
                    ref: 'visualizarEapButton',
                    icon: 'b-fa b-fa-file-alt',
                    tooltip: getTraducao('visualizar_eap'),
                    onClick() {
                        if (this.isFullscreen) {
                            this.isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        'use strict';
                        var dimensions = getDimension();

                        var myArguments = new Object();
                        myArguments.param1 = '';
                        myArguments.param2 = ' (VISUALIZAÇÃO) ';
                        window.top.showModal("'" + baseUrlEAP + "&AM=RO&Altura='" + (dimensions.height - 40), 'Visualização', dimensions.width, dimensions.height, recarregar, myArguments);
                    }
                }
            ]
        },
        {
            ref: 'cenario',
            type: 'combo',
            label: '',
            index: 1,
            value: numLinhaBase,
            inputWidth: '23em',
            editable: false,
            items: jsonComboLinhaBase,
            onChange({
                value
            }) {
                numLinhaBase = value;
                getHeadersProject();
                gantt.project.load();  
            }
        }, {
            type: 'button',
            color: 'b-blue',
            ref: 'visualizarLinhaButton',
            icon: 'b-fa b-fa-info-circle',
            tooltip: getTraducao('Visualizar_infor_da_linha_de_base'),
            toggleable: false,
            onClick() {
                    atualizarInfoLb();  
                }
            },
    ],
    listeners: {
        // Adiciona um ouvinte para o evento 'beforecontextmenu'
        beforecontextmenu: event => {
            // Impede a ação padrão do menu de contexto
            event.preventDefault();

            // Oculta o menu de contexto (substitua 'contextMenuElement' pelo elemento real do menu)
            const contextMenuElement = document.getElementById('contextMenuElement');
            if (contextMenuElement) {
                contextMenuElement.style.display = 'none';
            }
        },
    },
});

const stm = gantt.project.stm;
stm.autoRecord = true;
stm.disable();
stm.on({
    recordingStop : updateUndoRedoButtons,
    restoringStop : updateUndoRedoButtons,
    queueReset    : updateUndoRedoButtons
});

function updateUndoRedoButtons() {
    const { DesfazerAlteracoes, RefazerAlteracoes } = gantt.widgetMap;
    DesfazerAlteracoes.disabled = !stm.canUndo;
    RefazerAlteracoes.disabled = !stm.canRedo;
}

function toggleEdit(enable) {
    const { editarCronograma, DesfazerAlteracoes, RefazerAlteracoes, SalvarAlteracoes, AdicionarTarefa, EditarTarefa } = gantt.widgetMap;

    if (enable) {
        gantt.readOnly = false;

        editarCronograma.disabled = true;
        SalvarAlteracoes.disabled = false;
        AdicionarTarefa.disabled = false;
        EditarTarefa.disabled = false;

        stm.enable();
        stm.resetQueue();
    } else {
        gantt.readOnly = true;

        editarCronograma.disabled = false;
        SalvarAlteracoes.disabled = true;
        AdicionarTarefa.disabled = true;
        EditarTarefa.disabled = true;
        DesfazerAlteracoes.disabled = true;
        RefazerAlteracoes.disabled = true;

        stm.disable();
        stm.resetQueue();
    }

    updateUndoRedoButtons();
}

//gantt.project.load().then(function () {
//    // Adaptar tamanho
//    gantt.zoomToFit({
//        leftMargin: 100,
//        rightMargin: 100
//    });
//    var stm = gantt.project.stm;
//    stm.disable();
//    stm.autoRecord = true;
//    stm.disable = true;

//});
