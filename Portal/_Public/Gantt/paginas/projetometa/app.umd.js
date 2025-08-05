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

var listStatus = [];
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
            items: [{
                type  : 'buttonGroup',
                    items: [
                        {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'expandAllButton',
                            icon: 'b-fa b-fa-angle-double-down',
                            tipText: getTraducao('RecursosHumanos_expandir_todos'),
                            onAction: 'up.onExpandAllClick'
                        }, {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'collapseAllButton',
                            icon: 'b-fa b-fa-angle-double-up',
                            tipText: getTraducao('RecursosHumanos_contrair_todos'),
                            onAction: 'up.onCollapseAllClick'
                        },
                        {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'zoomInButton',
                            icon: 'b-fa b-fa-search-plus',
                            tipText: getTraducao('aumentar_zoom'),
                            onAction: 'up.onZoomInClick'
                        }, {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'zoomOutButton',
                            icon: 'b-fa b-fa-search-minus',
                            tipText: getTraducao('diminuir_zoom'),
                            onAction: 'up.onZoomOutClick'
                        },
                        {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'fullScreenButton',
                            icon: 'b-icon b-icon-fullscreen',
                            tipText: getTraducao('tela_cheia'),
                            cls: 'b-blue b-raised',
                            onAction: 'up.onFullScreenClick'
                        }]
                },  
            ]
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
        
        this.gantt.zoomToFit({
            leftMargin: 100,
            rightMargin: 100
        });
        this.gantt.expandAll();

        listStatus = ['Em Execução'];
            //this.gantt.data.Select(s => s._data.situacao).Distinct(d => d);
       
    }

    // region controller methods

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
        barMargin.value = gantt.barMargin;
        barMargin.max = gantt.rowHeight / 2 - 5;
        duration.value = gantt.transitionDuration;
        radius.value = gantt.features.dependencies.radius ?? 0;
    }

    onRowHeightChange({
        value,
        source
    }) {
        this.gantt.rowHeight = value;
        source.owner.widgetMap.barMargin.max = value / 2 - 5;
    }

    onBarMarginChange({
        value
    }) {
        this.gantt.barMargin = value;
    }

    onAnimationDurationChange({
        value
    }) {
        this.gantt.transitionDuration = value;
        this.styleNode.innerHTML = `.b-animating .b-gantt-task-wrap { transition-duration: ${value / 1000}s !important; }`;
    }

    onDependencyRadiusChange({
        value
    }) {
        this.gantt.features.dependencies.radius = value;
    }

    onCriticalPathsClick({
        source
    }) {
        this.gantt.features.criticalPaths.disabled = !source.pressed;
    }

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
        var listCombo = ['Em Execução',
            'Cancelado',
            'Suspenso',
            'Encerrado',
            'Aguardando Termo de Abertura',
            'Aguardando Suspensão/ Encerramento',
            'Em Planejamento'];

        if (langCode != 'Pt_BR') {
            listCombo = ['Running',
                        'Canceled',
                        'Suspended',
                        'Closed',
                        'Awaiting Opening Term',
                        'Waiting for Suspension/Termination',
                        'In planning'];
        }
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
                    items: listCombo
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

  
}

ColumnStore.registerColumnType(StatusColumn);

//// here you can extend our default Task class with your additional fields, methods and logic
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
        let status = 'Nao iniciado';
        if (this.isCompleted) {
            status = 'Concluido';
        }
        else if (this.isLate) {
            status = 'Atrasado';
        }
        else if (this.isStarted) {
            status = 'Em Execucao';
        }
        console.log('ST:', this);
        return status;
    }
}

const gantt = new Gantt({
    appendTo: 'container',
    locale: 'pt-BR',
    loadMask: getTraducao('carregando___'),
    dependencyIdField : 'wbsCode',
    selectionMode     : {
        cell       : false,
        dragSelect : false,
        rowNumber  : false
    },
    project : {
    // Let the Project know we want to use our own Task model with custom fields / methods
        taskModelClass : Task,
        transport      : {
            load : {
                //url : '../_datasets/launch-saas.json'
                requestConfig: {
                    url: '../../../../ApiHandler/GanttProjetoMetaHandler.ashx',
                    method: 'POST',
                    // get rid of cache-buster parameter
                    disableCaching: false,
                    //paramName: 'rq',                           
                    // custom request headers
                    headers: {
                        typeResult: "jsonProject",
                        idEntidade: idEntidade,
                        idUsuario: idUsuario,
                        idCarteira: idCarteira
                    }
                }
            }
        },
        autoLoad: true,
        startGroupsCollapsed: false,
        taskStore : {
            wbsMode : 'auto'
        },
        // The State TrackingManager which the UndoRedo widget in the toolbar uses
        stm : {
            autoRecord : true
        },
        // Reset Undo / Redo after each load
        resetUndoRedoQueuesAfterLoad : true
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
    features             : {
        baselines : {
            disabled : true
        },
        dependencyEdit : true,
        filter         : true,
        labels         : {
            left : {
                field  : 'name',
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
            disabled : true
        },
        rowReorder : {
            showGrip : false
        },
        timeRanges : {
            showCurrentTimeLine : true
        },
        fillHandle    : true,
        cellCopyPaste : true,
        taskCopyPaste : {
            useNativeClipboard : true
        },
        taskMenu: {
            disabled: true
        }
    },
    tbar : {
        type : 'gantttoolbar'
    }
});




