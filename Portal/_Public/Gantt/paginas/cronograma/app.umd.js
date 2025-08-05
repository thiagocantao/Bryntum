var {
    Toolbar,
    Toast,
    DateHelper,
    CSSHelper,
    Column,
    ColumnStore,
    TaskModel,
    ContextMenu,
    Gantt
} = bryntum.gantt;

/**
 * @module GanttToolbar
 */

/**
 * @extends Core/widget/Toolbar
 */
class GanttToolbar extends Toolbar {
    // Factoryable type name
    static get type() {
        return 'gantttoolbar';
    }

    static get $name() {
        return 'GanttToolbar';
    }

    static get configurable() {
        return {
            items : [{
                type  : 'buttonGroup',
                items : [{
                    color    : 'b-blue',
                    ref      : 'editButton',
                    icon     : 'b-fa b-fa-edit',
                    text     : 'Editar',
                    tooltip  : 'Habilitar edicao',
                    onAction : 'up.onEnableEditClick'
                }]
            }, {
                type  : 'buttonGroup',
                items : [{
                    color    : 'b-green',
                    ref      : 'addTaskButton',
                    icon     : 'b-fa b-fa-plus',
                    text     : 'Adicionar tarefa',
                    tooltip  : 'Adicionar nova tarefa',
                    onAction : 'up.onAddTaskClick',
                    disabled : true
                }]
            }, {
                color: 'b-blue',
                ref   : 'undoRedo',
                type  : 'undoredo',
                items : {
                    transactionsCombo : null
                }
            }, {
                    type: 'buttonGroup',
                    color: 'b-blue',
                items : [{
                    ref      : 'expandAllButton',
                    icon     : 'b-fa b-fa-angle-double-down',
                    tooltip  : 'Expand all',
                    onAction : 'up.onExpandAllClick'
                }, {
                    ref      : 'collapseAllButton',
                    icon     : 'b-fa b-fa-angle-double-up',
                    tooltip  : 'Collapse all',
                    onAction : 'up.onCollapseAllClick'
                }]
            }, {
                    type: 'buttonGroup',
                    color: 'b-blue',
                items : [{
                    ref      : 'zoomInButton',
                    icon     : 'b-fa b-fa-search-plus',
                    tooltip  : 'Zoom in',
                    onAction : 'up.onZoomInClick'
                }, {
                    ref      : 'zoomOutButton',
                    icon     : 'b-fa b-fa-search-minus',
                    tooltip  : 'Zoom out',
                    onAction : 'up.onZoomOutClick'
                    },
                    //{
                    //ref      : 'zoomToFitButton',
                    //icon     : 'b-fa b-fa-compress-arrows-alt',
                    //tooltip  : 'Zoom to fit',
                    //onAction : 'up.onZoomToFitClick'
                    //},
                    {
                    ref      : 'previousButton',
                    icon     : 'b-fa b-fa-angle-left',
                    tooltip  : 'Previous time span',
                    onAction : 'up.onShiftPreviousClick'
                }, {
                    ref      : 'nextButton',
                    icon     : 'b-fa b-fa-angle-right',
                    tooltip  : 'Next time span',
                    onAction : 'up.onShiftNextClick'
                }]
                },
                {
                type      : 'datefield',
                ref       : 'startDateField',
                    label: 'Project start',
                    hidden: true,
                // required  : true, (done on load)
                flex      : '0 0 18em',
                listeners : {
                    change : 'up.onStartDateChange'
                }
                },
                {
                    color: 'b-blue',
                type         : 'combo',
                ref          : 'projectSelector',
                label        : "Visao:",
                editable     : false,
                width        : '25em',
                displayField : 'name',
                value        : 1,
                store        : {
                    data : [{
                        id   : 1,
                        name : 'Planejamento',
                        url  : '../_datasets/launch-saas.json'
                    }, {
                        id   : 2,
                        name : "Execução",
                        url  : '../_datasets/tasks-workedhours.json'
                        },
                        {
                            id: 3,
                            name: 'Controle',
                            url: '../_datasets/launch-saas.json'
                        }, {
                            id: 4,
                            name: 'Completa',
                            url: '../_datasets/tasks-workedhours.json'
                        }
                    ]
                },
                listeners : {
                    select : 'up.onProjectSelected'
                }
                },

                {
                    type: 'button',
                    ref: 'importMsProjectButton',
                    color: 'b-blue',
                    icon: 'b-fa b-fa-arrow-down',
                    tooltip: 'Importar arquivo MS-Project',
                    text: 'Importar arquivo',
                    triggers: {
                        filter: {
                            align: 'end'
                        }
                    },
                    onAction() {
                        carregarArquivoXML();
                    }
                },

                {
                    type: 'button',
                    ref: 'exportMsProjectButton',
                    color: 'b-blue',
                    icon: 'b-fa b-fa-file-powerpoint',
                    tooltip: 'Exportar para MS-Project',
                    triggers: {
                        filter: {
                            align: 'end'
                        }
                    },
                    onAction() {
                        // give a filename based on task name
                        const filename = 'brisk_msproject_'+gantt.project.taskStore.first && `${gantt.project.taskStore.first.name}.xml`;

                        // call the export to download the XML file
                        gantt.features.mspExport.export({
                            filename
                        });
                    }
                },
                {
                    type: 'button',
                    color: 'b-red',
                    ref: 'exportPdfButton',
                    text: '',
                    icon: 'b-icon b-fa b-fa-file-pdf',
                    tooltip: 'Exportar para PDF',
                    triggers: {
                        filter: {
                            align: 'end'
                        }
                    },
                    onAction() {
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
                            const nomearquivopdf = 'brisk_pdf_' + gantt.project.taskStore.first && `${gantt.project.taskStore.first.name}`;
                            configAjax.headers = { typeResult: "exportToPdf", idprojeto: idProjeto };
                            configAjax.xhrFields = { responseType: 'blob' };
                            configAjax.success = function (data) {
                                var a = document.createElement('a');
                                var url = window.URL.createObjectURL(data);
                                a.href = url;
                                a.download = nomearquivopdf+'.pdf';
                                document.body.appendChild(a);
                                a.click();
                                a.remove();
                                window.URL.revokeObjectURL(url);
                            };
                        }
                        $.ajax(configAjax);
                    }
                },

                '->', {
                hidden: true,
                type                 : 'textfield',
                ref                  : 'filterByName',
                cls                  : 'filter-by-name',
                flex                 : '0 0 18.5em',
                // Label used for material, hidden in other themes
                label                : 'Buscar tarefas pelo nome',
                // Placeholder for others
                placeholder: 'Buscar tarefas pelo nome',
                clearable            : true,
                keyStrokeChangeDelay : 100,
                triggers             : {
                    filter : {
                        align : 'end',
                        cls   : 'b-fa b-fa-filter'
                    }
                },
                onChange : 'up.onFilterChange'
                },
                {
                    type: 'button',
                    color: 'b-green',
                    ref: 'saveButton',
                    text: 'Salvar projeto',
                    icon: 'b-fa b-fa-save',
                    onAction: 'up.onSaveCronograma'
                },

                {
                type    : 'button',
                ref     : 'featuresButton',
                icon    : 'b-fa b-fa-gear',
                text    : '',
                tooltip : 'Toggle features',
                menu    : {
                    onItem       : 'up.onFeaturesClick',
                    onBeforeShow : 'up.onFeaturesShow',
                    // "checked" is set to a boolean value to display a checkbox for menu items. No matter if it is true or false.
                    // The real value is set dynamically depending on the "disabled" config of the feature it is bound to.
                    items        : [{
                        text : 'Layout',
                        icon : 'b-fa-sliders-h',
                        menu : {
                            cls         : 'settings-menu',
                            layoutStyle : {
                                flexDirection : 'column'
                            },
                            onBeforeShow : 'up.onSettingsShow',
                            defaults     : {
                                type      : 'slider',
                                showValue : true
                            },
                            items : [{
                                ref     : 'rowHeight',
                                text    : 'Espessura da linha',
                                min     : 30,
                                max     : 70,
                                onInput : 'up.onRowHeightChange'
                            }
                            //, {
                            //    ref     : 'barMargin',
                            //    text    : 'Bar margin',
                            //    min     : 0,
                            //    max     : 10,
                            //    onInput : 'up.onBarMarginChange'
                            //}, {
                            //    ref     : 'duration',
                            //    text    : 'Animation duration',
                            //    min     : 0,
                            //    max     : 2000,
                            //    step    : 100,
                            //    onInput : 'up.onAnimationDurationChange'
                            //}, {
                            //    ref     : 'radius',
                            //    text    : 'Dependency radius',
                            //    min     : 0,
                            //    max     : 10,
                            //    onInput : 'up.onDependencyRadiusChange'
                            //    }
                            ]
                        }
                    }, {
                        text    : 'Mostrar nomes',
                        feature : 'labels',
                        checked : false
                    }, {
                        text    : 'Mostrar linhas',
                        feature : 'projectLines',
                        checked : false
                        },
                        //{
                        //text    : 'Enable cell editing',
                        //feature : 'cellEdit',
                        //checked : false
                        //},
                        {
                        text    : 'Mostrar linhas das colunas',
                        feature : 'columnLines',
                        checked : true
                        },
                        //{
                        //text    : 'Show baselines',
                        //feature : 'baselines',
                        //checked : false
                        //},
                        //{
                        //text    : 'Show rollups',
                        //feature : 'rollups',
                        //checked : false
                        //},
                    //    {
                    //    text    : 'Show progress line',
                    //    feature : 'progressLine',
                    //    checked : false
                    //}, {
                    //    text    : 'Show parent area',
                    //    feature : 'parentArea',
                    //    checked : false
                    //}, {
                    //    text         : 'Stretch tasks to fill ticks',
                    //    toggleConfig : 'fillTicks',
                    //    checked      : false
                    //    },
                        {
                        text    : 'Esconder gráfico',
                        cls     : 'b-separator',
                        subGrid : 'normal',
                        checked : false
                    }]
                }
            }]
        };
    }

    construct() {
        super.construct(...arguments);
        this.gantt = this.parent;
        this.gantt.project.on({
            load    : 'updateStartDateField',
            refresh : 'updateStartDateField',
            thisObj : this
        });
        this.styleNode = document.createElement('style');
        document.head.appendChild(this.styleNode);
    }

    setAnimationDuration(value) {
        const me = this,
            cssText = `.b-animating .b-gantt-task-wrap { transition-duration: ${value / 1000}s !important; }`;
        me.gantt.transitionDuration = value;
        if (me.transitionRule) {
            me.transitionRule.cssText = cssText;
        }
        else {
            me.transitionRule = CSSHelper.insertRule(cssText);
        }
    }

    updateStartDateField() {
        const {
            startDateField
        } = this.widgetMap;
        startDateField.value = this.gantt.project.startDate;

        // This handler is called on project.load/propagationComplete, so now we have the
        // initial start date. Prior to this time, the empty (default) value would be
        // flagged as invalid.
        startDateField.required = true;
    }

    // region controller methods

    onEnableEditClick() {
        this.gantt.readOnly = false;
        this.widgetMap.addTaskButton.disabled = false;
        this.widgetMap.editButton.disabled = true;
    }

    async onAddTaskClick() {
        const {
                gantt
            } = this,
            added = gantt.taskStore.rootNode.appendChild({
                name     : this.L('Nova tarefa...'),
                duration : 1
            });

        // run propagation to calculate new task fields
        await gantt.project.commitAsync();

        // scroll to the added task
        await gantt.scrollRowIntoView(added);
        gantt.features.cellEdit.startEditing({
            record : added,
            field  : 'name'
        });
    }

    onEditTaskClick() {
        const {
            gantt
        } = this;
        if (gantt.selectedRecord) {
            gantt.editTask(gantt.selectedRecord);
        }
        else {
            Toast.show(this.L('First select the task you want to edit'));
        }
    }

    onExpandAllClick() {
        this.gantt.expandAll();
    }

    onCollapseAllClick() {
        this.gantt.collapseAll();
    }

    onZoomInClick() {
        this.gantt.zoomIn();
    }

    onZoomOutClick() {
        this.gantt.zoomOut();
    }

    //onZoomToFitClick() {
    //    this.gantt.zoomToFit({
    //        leftMargin  : 50,
    //        rightMargin : 50
    //    });
    //}

    onShiftPreviousClick() {
        this.gantt.shiftPrevious();
    }

    onShiftNextClick() {
        this.gantt.shiftNext();
    }

    onStartDateChange({
        value,
        oldValue
    }) {
        if (value) {
            this.gantt.scrollToDate(DateHelper.add(value, -1, 'week'), {
                block : 'start'
            });
            this.gantt.project.setStartDate(value);
        }
    }

    onProjectSelected({
        record
    }) {
        this.gantt.project.load(record.url);
    }

    onFilterChange({
        value
    }) {
        if (value === '') {
            this.gantt.taskStore.clearFilters();
        }
        else {
            value = value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
            this.gantt.taskStore.filter({
                filters : task => task.name && task.name.match(new RegExp(value, 'i')),
                replace : true
            });
        }
    }

    onFeaturesClick({
        source: item
    }) {
        const {
            gantt
        } = this;
        if (item.feature) {
            const feature = gantt.features[item.feature];
            feature.disabled = !feature.disabled;
        }
        else if (item.subGrid) {
            const subGrid = gantt.subGrids[item.subGrid];
            subGrid.collapsed = !subGrid.collapsed;
        }
        else if (item.toggleConfig) {
            gantt[item.toggleConfig] = item.checked;
        }
    }

    onFeaturesShow({
        source: menu
    }) {
        const {
            gantt
        } = this;
        menu.items.map(item => {
            const {
                feature
            } = item;
            if (feature) {
                // a feature might be not presented in the gantt
                // (the code is shared between "advanced" and "php" demos which use a bit different set of features)
                if (gantt.features[feature]) {
                    item.checked = !gantt.features[feature].disabled;
                }
                // hide not existing features
                else {
                    item.hide();
                }
            }
            else if (item.subGrid) {
                item.checked = gantt.subGrids[item.subGrid].collapsed;
            }
        });
    }

    onSettingsShow({
        source: menu
    }) {
        const {
                gantt
            } = this,
            {
                rowHeight,
                barMargin,
                duration,
                radius
            } = menu.widgetMap;
        rowHeight.value = gantt.rowHeight;
        //barMargin.value = gantt.barMargin;
        //barMargin.max = gantt.rowHeight / 2 - 5;
        //duration.value = gantt.transitionDuration;
        //radius.value = gantt.features.dependencies.radius ?? 0;
    }

    onRowHeightChange({
        value,
        source
    }) {
        this.gantt.rowHeight = value;
        source.owner.widgetMap.barMargin.max = value / 2 - 5;
    }

    onSaveCronograma() {
        if (gantt.taskStore.changes != null) {
            console.log('List:', tarefasAlteradas);
            var configAjax = {
                url: '../../../../ApiHandler/GanttSaveHandler.ashx',
                method: 'POST',
                data: JSON.stringify(tarefasAlteradas)
            };

            $.ajax(configAjax);
        }
    }
    //onBarMarginChange({
    //    value
    //}) {
    //    this.gantt.barMargin = value;
    //}

    //onAnimationDurationChange({
    //    value
    //}) {
    //    this.gantt.transitionDuration = value;
    //    this.styleNode.innerHTML = `.b-animating .b-gantt-task-wrap { transition-duration: ${value / 1000}s !important; }`;
    //}

    //onDependencyRadiusChange({
    //    value
    //}) {
    //    this.gantt.features.dependencies.radius = value;
    //}

    //onCriticalPathsClick({
    //    source
    //}) {
    //    this.gantt.features.criticalPaths.disabled = !source.pressed;
    //}

    // endregion
}
;

// Register this widget type with its Factory
GanttToolbar.initClass();

/**
 * @module StatusColumn
 */

/**
 * A column showing the status of a task
 *
 * @extends Gantt/column/Column
 * @classType statuscolumn
 */
class StatusColumn extends Column {
    static get $name() {
        return 'StatusColumn';
    }

    static get type() {
        return 'statuscolumn';
    }

    static get isGanttColumn() {
        return true;
    }

    static get defaults() {
        return {
            // Set your default instance config properties here
            field      : 'status',
            text       : 'Status',
            editor     : false,
            cellCls    : 'b-status-column-cell',
            htmlEncode : false,
            filterable : {
                filterField : {
                    type  : 'combo',
                    items : ['Not Started', 'Started', 'Completed', 'Late']
                }
            }
        };
    }

    //endregion

    renderer({
        record
    }) {
        const status = record.status;
        return status ? [{
            tag       : 'i',
            className : `b-fa b-fa-circle ${status}`
        }, status] : '';
    }

    // * reactiveRenderer() {
    //     const
    //         percentDone = yield this.record.$.percentDone;
    //         //endDate     = yield this.record.$.endDate;
    //
    //     let status;
    //
    //     if (percentDone >= 100) {
    //         status = 'Completed';
    //     }
    //     // else if (endDate < Date.now()) {
    //     //     status = 'Late';
    //     // }
    //     else if (percentDone > 0) {
    //         status = 'Started';
    //     }
    //
    //     return status ? {
    //         tag       : 'i',
    //         className : `b-fa b-fa-circle ${status}`,
    //         html      : status
    //     } : '';
    // }
}

ColumnStore.registerColumnType(StatusColumn);

// here you can extend our default Task class with your additional fields, methods and logic
class Task extends TaskModel {
    static $name = 'Task';
    static get fields() {
        return ['status' // For status column
        ];
    }

    get isLate() {
        return !this.isCompleted && this.deadlineDate && Date.now() > this.deadlineDate;
    }

    get status() {
        let status = 'Not started';
        if (this.isCompleted) {
            status = 'Completed';
        }
        else if (this.isLate) {
            status = 'Late';
        }
        else if (this.isStarted) {
            status = 'Started';
        }
        return status;
    }
}
const gantt = new Gantt({
    appendTo: 'container',
    locale: 'pt',
    readOnly: true,
    //dependencyIdField : 'wbsCode',
    selectionMode     : {
        cell       : false,
        dragSelect : true,
        rowNumber  : false
    }, 
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
                        numLinhaBase: numLinhaBase,
                        isCarregarHtmlCaminhoCritico: false
                    }
                }
            }
        },
        autoLoad: true,
        //taskStore: {
        //    listeners: {
        //        change: (data) => {
        //            console.log('data1234 Task Store', data, data.changes)
        //        }
        //    }
        //},
        //dependencyStore: {
        //    listeners: {
        //        change: (data) => {
        //            console.log('data1234 Dependency Store', data, data.changes);
        //        }
        //    }
        //}
    },
    startDate                     : '2019-01-12',
    endDate                       : '2019-03-24',
    resourceImageFolderPath       : '../_shared/images/users/',
    scrollTaskIntoViewOnCellClick : true,
    columns: columns,
    
    subGridConfigs : {
        locked : {
            flex : 3
        },
        normal : {
            flex : 4
        }
    },
    columnLines          : false,
    // Shows a color field in the task editor and a color picker in the task menu.
    // Both lets the user select the Task bar's background color
    showTaskColorPickers : true,
    features: {
        
        taskMenu: {
            // Extra items for all events
            items: {
                add: false,
                convertToMilestone: false,
                taskColor: false,
                unlinkTasks: false
                //flagTask: {
                //    text: 'Extra',
                //    icon: 'b-fa b-fa-fw b-fa-flag',
                //    onItem({ taskRecord }) {
                //        taskRecord.flagged = true;
                //    }
                //}
            }
        },
        taskEdit: {
            text: 'Editar Tarefa',
            icon: 'b-fa b-fa-edit',
            type: 'tabpanel',
            defaultType: 'formtab',
            flex: '1 0 100%',
            autoHeight: true,

            layoutConfig: {
                alignItems: 'stretch',
                alignContent: 'stretch'
            },
            tabs: ['generalTab', 'linhaDeBaseTab', 'realizadoTab'],
            items: {
                generalTab: {
                    title: 'Tarefa',
                    index: 0,
                    animateTabChange: true,
                    items: {
                        percentDone: false,
                        effort: false,
                        divider: false,
                        colorField: false,
                        startDate: {
                            type: 'date',
                            label: 'Inicio',
                            name: 'startDate'
                        },
                        finishDate: {
                            type: 'date',
                            label: 'Termino',
                            name: 'endDate'
                        }
                    },
                    //onBeforeShow({ source: tab }) {
                    //    console.log('tab:', tab);
                    //    //tab._align = 'stretch';
                    //    //tab.layout.children.forEach(child => {
                    //    //    child.layout.align = 'stretch';
                    //    //});
                    //}
                },

                //linhaDeBaseTab: {
                //    title: 'Linha de base',
                //    layout: 'vbox',
                //    cls: 'b-general-tab',
                //    items: {
                //        name: {
                //            type: 'text',
                //            weight: 100,
                //            required: true,
                //            label: 'L{Name}',
                //            clearable: true,
                //            name: 'name',
                //            cls: 'b-name'
                //        },
                //        percentDone: {
                //            type: 'number',
                //            weight: 200,
                //            label: 'L{% complete}',
                //            name: 'renderedPercentDone',
                //            cls: 'b-percent-done b-half-width',
                //            min: 0,
                //            max: 100
                //        },
                //        effort: {
                //            type: 'effort',
                //            weight: 300,
                //            label: 'L{Effort}',
                //            name: 'fullEffort',
                //            cls: 'b-half-width'
                //        },
                //        divider: {
                //            html: '',
                //            weight: 400,
                //            dataset: {
                //                text: '--'
                //            },
                //            cls: 'b-divider',
                //            flex: '1 0 100%'
                //        },
                //        startDate: {
                //            type: 'startdate',
                //            weight: 500,
                //            label: 'L{Start}',
                //            name: 'startDate',
                //            cls: 'b-start-date b-half-width'
                //        },
                //        endDate: {
                //            type: 'enddate',
                //            weight: 600,
                //            label: 'L{Finish}',
                //            name: 'endDate',
                //            cls: 'b-end-date b-half-width'
                //        },
                //        duration: {
                //            type: 'durationfield',
                //            weight: 700,
                //            label: 'L{Duration}',
                //            name: 'fullDuration',
                //            cls: 'b-half-width'
                //        },
                //        colorField: {
                //            type: 'eventcolorfield',
                //            weight: 800,
                //            label: 'L{SchedulerBase.color}',
                //            name: 'eventColor',
                //            cls: 'b-half-width'
                //        }
                //    }
                //},

                linhaDeBaseTab: {
                    title: 'Linha de base',
                    index: 1,
                    //layout: 'vbox',
                    //cls: 'b-general-tab',
                    items: {
                        inicioLb: {
                            type: 'date',
                            weight: 500,
                            label: 'INICIO LB',
                            name: 'inicioLb',
                            readOnly: true,
                            cls: 'b-start-date b-half-width'
                        },
                        
                        duracaoLb: {
                            type: 'text',
                            weight: 500,
                            label: 'Duracao',
                            name: 'duracaoLb',
                            readOnly: true,
                            cls: 'b-start-date b-half-width'
                        },
                        terminoLb: {
                            type: 'date',
                            weight: 600,
                            label: 'Termino LB',
                            name: 'terminoLb',
                            readOnly: true,
                            cls: 'b-end-date b-half-width'
                        },
                        trabalhoLb: {
                            type: 'text',
                            weight: 600,
                            label: 'Trabalho',
                            name: 'trabalho',
                            cls: 'b-end-date b-half-width',
                            readOnly: true
                        },
                    }
                },

                realizadoTab: {
                    title: 'Realizado',
                    index: 2,
                    //layout: 'vbox',
                    //cls: 'b-general-tab',
                    items: {
                        startDate: {
                            type: 'startdate',
                            weight: 500,
                            label: 'INICIO',
                            name: 'inicioLb',
                            readOnly: true,
                            cls: 'b-start-date b-half-width'
                        },
                        endDate: {
                            type: 'startdate',
                            weight: 600,
                            label: 'Termino',
                            name: 'terminoLb',
                            cls: 'b-end-date b-half-width',
                            readOnly: true
                        },
                        duracaoRealizado: {
                            type: 'text',
                            weight: 500,
                            label: 'Duracao',
                            name: 'duracao',
                            readOnly: true,
                            cls: 'b-start-date b-half-width'
                        },
                        concluidoRealizado: {
                            type: 'text',
                            weight: 600,
                            label: '% Concluido',
                            name: 'concluido',
                            cls: 'b-end-date b-half-width',
                            readOnly: true
                        },
                    }
                },
                
                resourcesTab: false,
                notesTab: false,
                predecessorsTab:false,
                successorsTab: false,
                advancedTab: false,
                //detalhesTab: {
                //    title: 'Detalhes',
                //    items: [
                //        {
                //            type: 'container',
                //            items: [
                //                {
                //                    type: 'text',
                //                    label: 'Campo 1',
                //                    name: 'campo1',
                //                    flex: 1
                //                },
                //                {
                //                    type: 'date',
                //                    label: 'Campo 2',
                //                    name: 'campo2'
                //                }
                //            ]
                //        },
                //        {
                //            type: 'container',
                //            items: [
                //                {
                //                    type: 'number',
                //                    label: 'Campo 3',
                //                    name: 'campo3',
                //                    flex: 1
                //                },
                //                {
                //                    type: 'combo',
                //                    label: 'Campo 4',
                //                    field: 'campo4',
                //                    items: ['Opção 1', 'Opção 2']
                //                }
                //            ]
                //        }
                //    ],
                //    onBeforeShow({ source: tab }) {
                //        console.log('tab Custom:', tab);
                //        //tab._align = 'stretch';
                //        //tab.layout.children.forEach(child => {
                //        //    child.layout.align = 'stretch';
                //        //});
                //    }
                //},
            }
        },
        baselines : {
            disabled : true
        },
        cellEdit: false,
        mspExport: true,
        //eventContextMenu: true,
        dependencyEdit : true,
        filter         : true,
        labels         : {
            left : {
                field  : 'nomeTarefa',
                editor : {
                    type : 'textfield'
                }
            }
        },
        parentArea : {
            disabled : true
        },
        progressLine : {
            disabled   : true,
            statusDate : new Date(2019, 0, 25)
        },
        rollups : {
            disabled : false
        },
        rowReorder : {
            showGrip : true
        },
        timeRanges : {
            showCurrentTimeLine : true
        },
        fillHandle    : true,
        cellCopyPaste : true,
        taskCopyPaste : {
            useNativeClipboard : true
        }
    },
    listeners: {
        rowDrag: ({ task, index }) => {
            console.log(`Tarefa ${task.name} foi movida para a posição ${index}`);
            // Adicione seu código para lidar com a reordenação aqui
        },
    },
    tbar : {
        type : 'gantttoolbar'
    }
});

var listParent = [];

var SetOrderList = function (parent) {
    //console.log('Children:', parent.children);
    return new Promise((resolve) => {
        for (let i = 0; i < parent.children.length; i++) {
            let rowTask = parent.children[i];
            SetOrdem(rowTask, i, parent);
        }
        resolve(); 
    });
    
}

var GetTaskById = function (id) {
    return new Promise((resolve) => {

        let isAddParent = listParent.Any(parent => parent.data.id == id);
        if (!isAddParent) {
            listParent.push(gantt.taskStore.getById(id));
            return listParent.Where(parent => parent.data.id == id)[0];
        } else {
            return listParent.Where(parent => parent.data.id == id)[0];
        }
        resolve();
    });

}

var SetOrdem = function (item, numberOrder, parent) {
    if (item != undefined) {
        let orderReal = ++numberOrder;

        if (parent == undefined || parent == null) {
            GetTaskById(item.parentId).then((parentItem) => {
                parent = parentItem;
            });
        }

        if (parent != undefined) {
            item.data.edtcode = parent.data.edtcode == '0' ? orderReal : parent.data.edtcode + '.' + orderReal;
            //console.log('item:', item.data);
            //console.log('Parent:', parent);
            //console.log('OrderReal:', orderReal);
            if (item.children != undefined) {
                for (let i = 0; i <= item.children.length; i++) {
                    SetOrdem(item.children[i], i, item);
                }
            }
        } 
        
    }
}

var atualizarGantt = async function () {
    const result = await gantt.project.commitAsync();
    gantt.project.commitAsync().then(() => {
        gantt.project.propagate();
    });
}

gantt.taskStore.on({
    change({ record, changes }) {
        
        //console.log("committing...:", changes);

        if (changes != undefined && record != undefined && record.parent != undefined && changes != undefined && changes.parentIndex != undefined) {
            //console.log("CG - record:", record);
            //console.log("CG - Parent:", record.parent);
            //console.log("CG - changes:", changes);
            //console.log("Task - changes:", gantt.taskStore.changes);
            listParent.push(record.parent);
            SetOrderList(record.parent)
                .then(atualizarGantt);
            //obterTarefasAlteradas();
        }
        
    },
    commit({ record, task, changes, action }) {
        //console.log("commit:", changes);
        //adicionarTarefaAlterada(record);
        //console.log("CT - record:", record);
        //console.log("CT - task:", task);
        //console.log("CT - changes:", changes);
        //console.log("CT - action:", action);
    },
    update({ record, task, changes, action }) {
        //console.log("UP - record:", record);
        obterTarefasAlteradas();
        //console.log("UP - task:", task);
        //console.log("UP - changes:", changes);
        //console.log("UP - action:", action);
        //gantt.taskStore.commitAsync();
    }

});

var tarefasAlteradas = {
    ListAdd: [],
    ListMod: [],
    ListRem: [],
    ListPredecessor: []
};

async function limparTarefasAlteradas() {

    tarefasAlteradas = {
        ListAdd: [],
        ListMod: [],
        ListRem: [],
        ListPredecessor: []
    };
}

async function obterTarefasAlteradas() {
    limparTarefasAlteradas();

    // Obtém as alterações na taskStore
    const changes = gantt.taskStore.changes;

    if (!changes) {
        return;
    }

    // Lida com tarefas adicionadas
    for (const task of changes.added) {

        const tarefaData = mountTarefa(task.data);

        if (tarefaData.edtcode != undefined && tarefaData.edtcode != '0' && !tarefasAlteradas.ListAdd.Any(item => item.id == tarefaData.id)) {
            await obterPredecessorasRecursivas(tarefaData);
            tarefasAlteradas.ListAdd.push(tarefaData);
        }
    }
    
    // Lida com tarefas modificadas
    for (const task of changes.modified) {
        const tarefaData = mountTarefa(task.data);
       
        if (tarefaData.edtcode != undefined && tarefaData.edtcode != '0' && !tarefasAlteradas.ListMod.Any(item => item.id == tarefaData.id)) {
            await obterPredecessorasRecursivas(tarefaData);
            tarefasAlteradas.ListMod.push(tarefaData);
        }
    }
    
    // Lida com tarefas removidas
    for (const task of changes.removed) {
        const tarefaData = mountTarefa(task.data);
        if (!tarefasAlteradas.ListRem.Any(item => item.id == tarefaData.id)) {
            await obterPredecessorasRecursivas(tarefaData);
            tarefasAlteradas.ListRem.push(tarefaData);
        }
    }
}

function mountTarefa(ganttTask) {
    return {
        id: ganttTask.id,
        edtcode: ganttTask.edtcode,
        isCaminhoCriticoStr: ganttTask.isCaminhoCriticoStr,
        name: ganttTask.name,
        nomeTarefa: ganttTask.nomeTarefa,
        previsto: ganttTask.previsto,
        custo: ganttTask.custo,
        previstoStr: ganttTask.previstoStr,
        percentDone: ganttTask.percentDone,
        realizado: ganttTask.realizado,
        percentDoneStr: ganttTask.percentDoneStr,
        pesoLb: ganttTask.pesoLb,
        peso: ganttTask.peso,
        duracao: ganttTask.duracao,
        duracaoStr: ganttTask.duracaoStr,
        duracaoLb: ganttTask.duracaoLb,
        trabalho: ganttTask.trabalho,
        startDate: ganttTask.startDate,
        endDate: ganttTask.endDate,
        inicio: ganttTask.inicio,
        inicioLb: ganttTask.inicioLb,
        termino: ganttTask.termino,
        terminoReal: ganttTask.terminoReal,
        terminoLb: ganttTask.terminoLb,
        isMarcoStr: ganttTask.isMarcoStr,
        isAtrasoStr: ganttTask.isAtrasoStr,
        recurso: ganttTask.recurso,
        codTarefa: ganttTask.codTarefa,
        parentId : ganttTask.parentId
    };
}

async function obterPredecessorasRecursivas(tarefa) {
    async function buscarPredecessoras(id) {
        if (!tarefasAlteradas.ListPredecessor.Any(item => item.id == id)) {
            const predecessor = gantt.taskStore.getById(id);
            if (predecessor) {
                tarefasAlteradas.ListPredecessor.push(mountTarefa(predecessor.data));
                if (predecessor.data.parentId) {
                    await buscarPredecessoras(predecessor.data.parentId);
                }
            }
        }
    }
    await buscarPredecessoras(tarefa.parentId);
}

//**region import ms project file

function convertXMLtoJSON(xmlString) {
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlString, 'text/xml');

    const tasks = xmlDoc.querySelectorAll('Task');
    const jsonData = [];

    tasks.forEach(task => {
        const taskData = {
            uid: task.querySelector('UID').textContent,
            name: task.querySelector('Name').textContent,
            active: task.querySelector('Active').textContent,
            manual: task.querySelector('Manual').textContent,
            type: task.querySelector('Type').textContent,
            isNull: task.querySelector('IsNull').textContent,
            wbs: task.querySelector('WBS').textContent,
            outlineNumber: task.querySelector('OutlineNumber').textContent,
            outlineLevel: task.querySelector('OutlineLevel').textContent,
            start: task.querySelector('Start').textContent,
            finish: task.querySelector('Finish').textContent,
            duration: task.querySelector('Duration').textContent,
            manualStart: task.querySelector('ManualStart').textContent,
            manualFinish: task.querySelector('ManualFinish').textContent,
            manualDuration: task.querySelector('ManualDuration').textContent,
            durationFormat: task.querySelector('DurationFormat').textContent,
            work: task.querySelector('Work').textContent,
            effortDriven: task.querySelector('EffortDriven').textContent,
            estimated: task.querySelector('Estimated').textContent,
            milestone: task.querySelector('Milestone').textContent,
            summary: task.querySelector('Summary').textContent,
            percentComplete: task.querySelector('PercentComplete').textContent,
            actualStart: task.querySelector('ActualStart').textContent,
            actualDuration: task.querySelector('ActualDuration').textContent,
            remainingDuration: task.querySelector('RemainingDuration').textContent,
            ignoreResourceCalendar: task.querySelector('IgnoreResourceCalendar').textContent,
            rollup: task.querySelector('Rollup').textContent,
            constraintType: task.querySelector('ConstraintType').textContent,
            calendarUid: task.querySelector('CalendarUID').textContent,
        };

        jsonData.push(taskData);
    });

    return jsonData;
}

function carregarArquivoXML() {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.xml';

    input.addEventListener('change', handleFile);

    input.click();

    function handleFile() {
        const file = input.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = function (e) {
                const xmlString = e.target.result;
                const jsonData = convertXMLtoJSON(xmlString);

                // Faça o que quiser com o JSON, como carregá-lo na sua Gantt
                console.log(jsonData);

                gantt.project.stores.forEach(store => store.removeAll());

                // Carregue o novo conjunto de dados JSON na Gantt
                gantt.project.load(jsonData);
            };

            reader.readAsText(file);
        }
    }
}


