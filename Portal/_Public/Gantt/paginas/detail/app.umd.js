var {
    Toolbar,
    Toast,
    DateHelper,
    CSSHelper,
    Column,
    ColumnStore,
    ContextMenu,
    TaskModel,
    Gantt,
    Grid,
    Popup,
    CheckColumn
} = bryntum.gantt;

// ---------------------------------------------
// CONTROLE DE FULLSCREEN (fora do escopo do botão)
let isFullscreen = false;
// ---------------------------------------------

// Sanitize da sua lista de colunas: remove qualquer coluna 'name' existente,
// para evitarmos conflito com a taskNameColumn definida no Gantt.
if (!Array.isArray(columns)) columns = [];
const otherColumns = columns.filter(c => c && c.type !== 'name');

// Garante headers atualizados para o load do projeto
var getHeadersProject = function () {
    var headers = {
        typeResult: "jsonProject",
        idprojeto: idProjeto,
        numLinhaBase: numLinhaBase
    };
    gantt.project.transport.load.requestConfig.headers = headers;
    return headers;
};

const gantt = new Gantt({
    appendTo: 'container',
    selectionMode: {
        cell: true,
        dragSelect: true,
        rowNumber: false
    },
    locale: 'pt-BR',
    loadMask: getTraducao('carregando___'),

    // >>>>>>> AQUI ESTÁ O PULO DO GATO
    // Forçamos a coluna de nome (árvore) pelo config "taskNameColumn".
    // Assim não dependemos do conteúdo de "columns" para a hierarquia aparecer.
    taskNameColumn: {
        type: 'name',
        field: 'name',        // ajuste se o campo do seu dataset tiver outro nome
        text: 'Tarefa',
        width: 300,
        region: 'locked'
    },

    // Suas demais colunas (sem a 'name'), ficam aqui:
    columns: otherColumns,

    // >>>>>>> Mantemos locked/normal para o layout
    subGridConfigs: {
        locked: { flex: 3 },
        normal: { flex: 4 }
    },

    // Projeto: carregamento via transport
    project: {
        transport: {
            load: {
                requestConfig: {
                    url: '../../../../ApiHandler/GanttHandler.ashx',
                    method: 'POST',
                    // get rid of cache-buster parameter
                    disableCaching: false,
                    // custom request headers
                    headers: {
                        typeResult: "jsonProject",
                        idprojeto: idProjeto,
                        numLinhaBase: numLinhaBase
                    }
                }
            }
        },
        autoLoad: true,
        // Reset Undo / Redo after each load
        resetUndoRedoQueuesAfterLoad: true
    },

    tickSize: 50,
    readonly: false,
    contextMenu: false,

    // (Opcional) Garantimos que a feature de árvore está ativa
    features: {
        tree: true,
        progressLine: { disabled: false },
        taskMenu: { disabled: false },
        filter: true,
        nonWorkingTime: { disabled: true },
        percentBar: false,
        taskEdit: {
            editorConfig: {
                items: {
                    advancedTab: {
                        items: {
                            calendarField: false
                        }
                    }
                }
            }
        }
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
                    onClick() { gantt.expandAll(); }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'collapseAllButton',
                    icon: 'b-fa b-fa-angle-double-up',
                    tooltip: getTraducao('RecursosHumanos_contrair_todos'),
                    onClick() { gantt.collapseAll(); }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'zoomInButton',
                    icon: 'b-fa b-fa-search-plus',
                    onClick() { gantt.zoomIn(); }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'zoomOutButton',
                    icon: 'b-fa b-fa-search-minus',
                    onClick() { gantt.zoomOut(); }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'fullScreenButton',
                    icon: 'b-icon b-icon-fullscreen',
                    cls: 'b-blue b-raised',
                    onClick() {
                        if (!isFullscreen) {
                            isFullscreen = true;
                            bryntum.gantt.Fullscreen.request(document.documentElement);
                        } else {
                            isFullscreen = false;
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

                        // Usar API pública: taskStore
                        gantt.taskStore.allRecords.forEach(function (item) {
                            var row = {
                                id: item.id ?? 0,
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

                        // isIE é uma função de ~/script/custom/util/browser.js
                        if (isIE()) {
                            configAjax.headers = { typeResult: "getHtmlGantt", idprojeto: idProjeto };
                            configAjax.success = function (data) {
                                var win = window.open('', '_blank', 'toolbar=yes,scrollbars=yes,resizable=yes');
                                win.document.write(data);
                                win.document.close();
                                win.focus();
                                win.print();
                                win.close();
                            };
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
                    ref: 'GerenciarRecursos',
                    icon: 'b-fa b-fa-briefcase',
                    tooltip: "Gerenciar Recursos",
                    hidden: false,
                    onClick() {
                        if (isFullscreen) {
                            isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        gerenciarRecursos();
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'editarCronograma',
                    icon: 'b-fa b-fa-edit',
                    tooltip: getTraducao('editar_cronograma'),
                    hidden: false,
                    onClick() {
                        if (isFullscreen) {
                            isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        toggleEdit(true);
                    }
                },
                {
                    color: 'b-blue',
                    ref: 'undoredoTool',
                    type: 'undoredo',
                    text: false,
                    items: { transactionsCombo: null }
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
                        const cronogramaJson = JSON.stringify(gantt.project.toJSON());
                        salvaCronograma(cronogramaJson);
                    }
                },
                {
                    type: 'button',
                    color: 'b-blue',
                    ref: 'CancelarAlteracoes',
                    icon: 'b-fa b-fa-close',
                    tooltip: "Cancelar",
                    disabled: true,
                    async onClick() {
                        await stm.undoAll();
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
                        const parent = gantt.selectedRecord || gantt.taskStore.rootNode;
                        const nova = parent.appendChild({ name: 'Nova tarefa', duration: 1 });
                        gantt.editTask(nova);
                    }
                },
                {
                    type: 'button',
                    color: 'b-green',
                    ref: 'EditarTarefa',
                    icon: 'b-fa b-fa-edit',
                    tooltip: "Editar tarefa",
                    disabled: true,
                    onClick() {
                        if (gantt.selectedRecord) {
                            gantt.editTask(gantt.selectedRecord);
                        } else {
                            bryntum.gantt.Toast.show('Primeiro selecione a tarefa que deseja editar');
                        }
                    }
                },
                {
                    type: 'button',
                    color: 'b-green',
                    ref: 'visualizarInfoTarefaButton',
                    icon: 'b-fa b-fa-file-alt',
                    tooltip: "Detalhes da tarefa",
                    onClick() {
                        if (isFullscreen) {
                            isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }

                        if (gantt.selectedRecord) {
                            const editor = gantt.features.taskEdit.editor,
                                saveButton = editor.widgetMap.saveButton,
                                deleteButton = editor.widgetMap.deleteButton;

                            editor.readOnly = true;
                            saveButton.hidden = true;
                            deleteButton.hidden = true;

                            gantt.editTask(gantt.selectedRecord);

                            editor.on('hide', function restoreEditor() {
                                editor.off('hide', restoreEditor);
                                editor.readOnly = false;
                                saveButton.hidden = false;
                                deleteButton.hidden = false;
                            });
                        } else {
                            bryntum.gantt.Toast.show(getTraducao('Primeiro_selecione_a_tarefa_que_deseja_visualizar'));
                        }
                    }
                }
            ]
        },
        {
            type: 'buttonGroup',
            items: [
                {
                    type: 'button',
                    color: 'b-deep-orange',
                    ref: 'editEapButton',
                    icon: 'b-fa b-fa-sitemap',
                    tooltip: getTraducao('editar_eap'),
                    hidden: false,
                    onClick() {
                        if (isFullscreen) {
                            isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        'use strict';
                        var dimensions = getDimension();

                        var myArguments = {};
                        myArguments.param1 = '';
                        myArguments.param2 = '';

                        var retorno = function (codigoItem) {
                            callbackAtualizaTela.PerformCallback(codigoItem);
                        };

                        window.top.showModal("'" + baseUrlEAP + "&AM=RW&Altura='" + (dimensions.height - 40), 'Edição', dimensions.width, dimensions.height, retorno, myArguments);
                    }
                },
                {
                    type: 'button',
                    color: 'b-deep-orange',
                    ref: 'visualizarEapButton',
                    icon: 'b-fa b-fa-file-alt',
                    tooltip: getTraducao('visualizar_eap'),
                    onClick() {
                        if (isFullscreen) {
                            isFullscreen = false;
                            bryntum.gantt.Fullscreen.exit();
                        }
                        'use strict';
                        var dimensions = getDimension();

                        var myArguments = {};
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
            onChange({ value }) {
                numLinhaBase = value;
                getHeadersProject();
                gantt.project.load();
            }
        },
        {
            type: 'button',
            color: 'b-blue',
            ref: 'visualizarLinhaButton',
            icon: 'b-fa b-fa-info-circle',
            tooltip: getTraducao('Visualizar_infor_da_linha_de_base'),
            toggleable: false,
            onClick() { atualizarInfoLb(); }
        }
    ],

    listeners: {
        // Evita menu de contexto do navegador
        beforecontextmenu: event => {
            event.preventDefault();
            const contextMenuElement = document.getElementById('contextMenuElement');
            if (contextMenuElement) {
                contextMenuElement.style.display = 'none';
            }
        }
    }
});

// ---------- State Tracking Manager ----------
const stm = gantt.project.stm;
stm.autoRecord = true;
stm.disable(); // desabilitado até entrar no modo edição


stm.on({
    recordingStop: updateUndoRedoButtons,
    restoringStop: updateUndoRedoButtons,
    queueReset: updateUndoRedoButtons
});

function updateUndoRedoButtons() {
    const { DesfazerAlteracoes, RefazerAlteracoes } = gantt.widgetMap;
    DesfazerAlteracoes.disabled = !stm.canUndo;
    RefazerAlteracoes.disabled = !stm.canRedo;
}

function toggleEdit(enable) {
    const {
        editarCronograma,
        DesfazerAlteracoes,
        RefazerAlteracoes,
        SalvarAlteracoes,
        CancelarAlteracoes,
        AdicionarTarefa,
        EditarTarefa
    } = gantt.widgetMap;

    if (enable) {
        gantt.readOnly = false;

        editarCronograma.disabled = true;
        SalvarAlteracoes.disabled = false;
        CancelarAlteracoes.disabled = false;
        AdicionarTarefa.disabled = false;
        EditarTarefa.disabled = false;

        stm.enable();
        stm.resetQueue();
    } else {
        gantt.readOnly = true;

        editarCronograma.disabled = false;
        SalvarAlteracoes.disabled = true;
        CancelarAlteracoes.disabled = true;
        AdicionarTarefa.disabled = true;
        EditarTarefa.disabled = true;
        DesfazerAlteracoes.disabled = true;
        RefazerAlteracoes.disabled = true;

        stm.disable();
        stm.resetQueue();
    }

    updateUndoRedoButtons();
}

async function gerenciarRecursos() {
    try {
        const recursos = window.recursosCorporativos || [];

        const grid = new Grid({
            columns: [{ text: 'Recurso', field: 'NomeRecursoCorporativo', flex: 1 }],
            data: recursos,
            height: 300,
            width: 400,
            selectionMode: {
                checkbox: true,
                multiSelect: true
            }
        });

        const popup = new Popup({
            title: 'Gerenciar Recursos',
            modal: true,
            width: 420,
            height: 380,
            closable: true,
            autoShow: true,
            items: [grid],
            bbar: [{
                text: 'Adicionar ao projeto',
                onClick() {
                    const selecionados = grid.selectedRecords || [];
                    if (!selecionados.length) {
                        Toast.show('Selecione ao menos um recurso.');
                        return;
                    }
                    const ids = selecionados.map(r => r.CodigoRecursoCorporativo);
                    insereRecursosCorporativos(JSON.stringify(ids));
                    popup.close();
                }
            }]
        });

        popup.show();
    }
    catch (e) {
        Toast.show('Não foi possível carregar os recursos.');
    }
}

gantt.project.on('load', () => {
    const recursos = window.recursosCorporativosAlocados || [];
    gantt.project.resourceStore.data = recursos.map(r => ({
        id: r.CodigoRecursoCorporativo,
        name: r.NomeRecursoCorporativo
    }));

    const atribuicoes = window.atribuicoesRecursos || [];
    gantt.project.assignmentStore.data = atribuicoes.map(a => ({
        id: a.CodigoAtribuicao,
        event: a.CodigoTarefa,
        resource: a.CodigoRecursoProjeto
    }));

    // CORREÇÃO: não sobrescrever .disable
    const _stm = gantt.project.stm;
    _stm.disable();
    _stm.autoRecord = true;

    // Logs de verificação (você pode remover depois):
    try {
        console.log('[DEBUG] Tree feature?', !!gantt.features.tree);
        console.log('[DEBUG] Columns:', gantt.columns.map(c => ({ type: c.type, region: c.region, text: c.text })));
        console.log('[DEBUG] Root children count:', gantt.taskStore?.rootNode?.children?.length);
    } catch (e) { }
});
