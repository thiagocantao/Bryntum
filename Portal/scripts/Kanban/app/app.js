var taskBoard;
var movimentoValido = false;

Ext.Loader.setConfig({
    enabled        : true,
    disableCaching : true,
    paths          : {
        'Sch'    : '../../lib/Sch',
        'Kanban' : '../../lib/Kanban'
    }
});

Ext.require([
    'Kanban.view.TaskBoard',
    'Kanban.field.TaskFilter',
    'Kanban.field.TaskHighlight',
    'Kanban.field.ColumnFilter',
    'Kanban.menu.UserPictureMenu'
]);



Ext.onReady(function () {

    var resourceStore = new Kanban.data.ResourceStore({
        sorters: 'Name',
        autoLoad: true,
        proxy: {
            type: 'ajax',

            api: {
                read: 'users.js',
                update: undefined,
                destroy: undefined,
                create: undefined
            }
        }
    });

    Ext.define('MyTask', {
        extend: 'Kanban.model.Task',

        fields: [
            { name: 'IndicaCorDiferente', type: 'int' },
            { name: 'IndicaCorBorda', type: 'int' },
            { name: 'StatusAux', type: 'string' },
            { name: 'ColunaValorAux', type: 'string' },
            { name: 'ColunaOrder', type: 'int' }
        ],

        isValidTransition: function (toState) {
            estadoAnterior = this.getState();

            movimentoValido = validaMovimento(this, toState);

            return movimentoValido;
        }
    })

    var taskStore = new Kanban.data.TaskStore({
        model: MyTask,
        sorters: colunaOrdenacao == '' ? 'Name' : colunaOrdenacao,
        autoLoad: true,
        proxy: {
            type: 'ajax',

            api: {
                read: arquivoTarefas,
                update: undefined,
                destroy: undefined,
                create: undefined
            }
        }
    });

    Ext.define('MyKanbanPanel', {
        extend: 'Kanban.view.TaskBoard',
        region: 'center',
        padding: '0 10 10 10',
        initComponent: function () {

            Ext.apply(this, {
                viewConfig: {
                    multiSelect: multiplaSelecao,
                    plugins: {
                        xclass: 'Ext.ux.DataView.DragSelector'
                    },

                    taskToolTpl: '<div class="sch-tool-ct">' +
                        (mostrarConsulta == "S" ? '<div title="Visualizar Detalhes" class="sch-tool sch-tool-view"></div>' : "") +
                        (mostrarEdicao == "S" ? '<div title="Editar" class="sch-tool sch-tool-edit"></div>' : "") +
                        (mostrarComentarios == "S" ? '<div title="Editar Comentários" class="sch-tool sch-tool-comment"></div>' : "") +
                        (mostrarAnexos == "S" ? '<div title="Visualizar Anexos" class="sch-tool sch-tool-attachment"></div>' : "") +
                        '</div>',

                    taskRenderer: function (task, renderData) {
                        constroiTarefa(task, renderData);
                    }
                },

                editor: new Kanban.editor.SimpleEditor({
                    dataIndex: 'Name',
                    triggerEvent: null // only invoked via the "edit" tool
                })
            })

            this.callParent(arguments);
        },

        afterRender: function () {
            this.callParent(arguments);

            this.el.on('click', this.onViewClick, this, { delegate: '.sch-tool-view' });
            this.el.on('click', this.onEditClick, this, { delegate: '.sch-tool-edit' });
            this.el.on('click', this.onCommentClick, this, { delegate: '.sch-tool-comment' });
            this.el.on('click', this.onAttachmentClick, this, { delegate: '.sch-tool-attachment' });
        },

        onViewClick: function (e, t) {
            var task = this.resolveRecordByNode(t).data;
            executaAcaoConsulta(task);
        },

        onEditClick: function (e, t) {
            var task = this.resolveRecordByNode(t).data;
            executaAcaoEdicao(task);
        },

        onCommentClick: function (e, t) {
            var task = this.resolveRecordByNode(t).data;
            abreComentarios(task);
        },

        onAttachmentClick: function (e, t) {
            var task = this.resolveRecordByNode(t).data;
            abreAnexos(task);
        }
    });



    taskBoard = new MyKanbanPanel({
        resourceStore: resourceStore,
        taskStore: taskStore,
        columns: arrayColumns
    });

    taskBoard.on('taskdrop', function (drop, task, eOpts) { executaAcaoMovimento(drop, task, eOpts); })

    taskBoard.on('select', function (task, record, eOpts) { if (window.eventoSelect) window.eventoSelect(task); })

    var vp = new Ext.Viewport({
        items: [
            {
                xtype: 'toolbar',
                cls: 'the-toolbar',
                height: 40,
                region: 'north',
                border: 0,
                padding: '5 21',               

                items: [
                    {
                        xtype: 'label',
                        text: 'Filtro',
                        style: 'color:#fff;margin:0 10px'
                    },
                    {
                        xtype: 'filterfield',
                        width: 350,
                        store: taskStore
                    },
                    '->',
                    {
                        xtype: 'slider',
                        width: 100,
                        increment: 0,
                        minValue: 0,
                        maxValue: 3,
                        value: 4,
                        style: 'background-color:transparent;margin-right:10px;' + callbackStatus.cp_MostraBotoes,
                        listeners: {
                            change: function (slider, val) {
                                taskBoard.setZoomLevel(val === 0 ? 'mini' : (val === 1 ? 'small' : (val === 2 ? 'medium' : 'large')));
                            }
                        }
                    },
                    {
                        xtype: 'button',
                        width: 34,
                        height: 30,
                        style: 'background-color:#FFFFB9;background-image: url(../../imagens/graficoKanban.png);' + callbackStatus.cp_MostraBotoes,
                        listeners: {
                            click: function (button, val) {
                                
                            }
                        }
                    },
                    {
                        xtype: 'button',
                        itemId: 'btnTelaCheia',
                        id: 'btnTelaCheia',
                        width: 34,
                        height: 30,
                        style: 'background-color:#FFFFB9;background-image: url(../../imagens/telaCheia.png);' + callbackStatus.cp_MostraBotoes,
                        listeners: {
                            click: function (button, val) {
                                window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/' + callbackStatus.cp_Url, '', screen.width - 30, screen.height - 190, '', null);
                            }
                        }
                    }
                ]
            },
            taskBoard
        ],
        layout: 'border'
    });
});
