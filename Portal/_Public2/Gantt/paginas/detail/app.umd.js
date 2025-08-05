"use strict";

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function asyncGeneratorStep(gen, resolve, reject, _next, _throw, key, arg) { try { var info = gen[key](arg); var value = info.value; } catch (error) { reject(error); return; } if (info.done) { resolve(value); } else { Promise.resolve(value).then(_next, _throw); } }

function _asyncToGenerator(fn) { return function () { var self = this, args = arguments; return new Promise(function (resolve, reject) { var gen = fn.apply(self, args); function _next(value) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "next", value); } function _throw(err) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "throw", err); } _next(undefined); }); }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _get(target, property, receiver) { if (typeof Reflect !== "undefined" && Reflect.get) { _get = Reflect.get; } else { _get = function _get(target, property, receiver) { var base = _superPropBase(target, property); if (!base) return; var desc = Object.getOwnPropertyDescriptor(base, property); if (desc.get) { return desc.get.call(receiver); } return desc.value; }; } return _get(target, property, receiver || target); }

function _superPropBase(object, property) { while (!Object.prototype.hasOwnProperty.call(object, property)) { object = _getPrototypeOf(object); if (object === null) break; } return object; }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

/**
 * @module GanttToolbar
 */

/**
 * @extends Common/widget/bryntum.gantt.Toolbar
 */
var isFullScreenControl = false;
var GanttToolbar =
    /*#__PURE__*/
    function (_bryntum$gantt$Toolba) {
        _inherits(GanttToolbar, _bryntum$gantt$Toolba);

        function GanttToolbar() {
            _classCallCheck(this, GanttToolbar);

            return _possibleConstructorReturn(this, _getPrototypeOf(GanttToolbar).apply(this, arguments));
        }

        _createClass(GanttToolbar, [{
            key: "construct",
            value: function construct(config) {
                var me = this,
                    gantt = me.gantt = config.gantt,
                    project = gantt.project;
                var stm = project.stm;
                stm.on({
                    recordingstop: me.updateUndoRedoButtons,
                    restoringstop: me.updateUndoRedoButtons,
                    queueReset: me.updateUndoRedoButtons,
                    thisObj: me
                });

                _get(_getPrototypeOf(GanttToolbar.prototype), "construct", this).call(this, config); // Since the code is shared between "advanced" and "php" demo
                // in "php" demo we make "Save" button visible
                // and track project changes to disable/enable the button


                if (project.transport.sync) {
                    // track project changes to disable/enable "Save" button
                    gantt.project.on({
                        haschanges: me.onProjectChanges,
                        nochanges: me.onProjectChanges,
                        thisObj: me
                    }); // make button visible

                    me.widgetMap.saveButton.show();
                }
            }
        }, {
            key: "updateUndoRedoButtons",
            value: function updateUndoRedoButtons() {
                var gantt = this.gantt,
                    project = gantt.project,
                    stm = project.stm,
                    _this$widgetMap = this.widgetMap,
                    undoBtn = _this$widgetMap.undoBtn,
                    redoBtn = _this$widgetMap.redoBtn,
                    redoCount = stm.length - stm.position;
                undoBtn.badge = stm.position || '';
                redoBtn.badge = redoCount || '';
                undoBtn.disabled = !stm.canUndo;
                redoBtn.disabled = !stm.canRedo;
            }
        }, {
            key: "setAnimationDuration",
            value: function setAnimationDuration(value) {
                var me = this,
                    cssText = ".b-animating .b-gantt-task-wrap { transition-duration: ".concat(value / 1000, "s !important; }");
                me.gantt.transitionDuration = value;

                if (me.transitionRule) {
                    me.transitionRule.cssText = cssText;
                } else {
                    me.transitionRule = bryntum.gantt.CSSHelper.insertRule(cssText);
                }
            }
            },             
        {
            key: "onFullScreenClick",
            value: function onFullScreenClick() {                
                if (!this.isFullscreen) {
                    this.isFullscreen = true;
                    bryntum.gantt.Fullscreen.request(document.documentElement);                 
                } else {
                    this.isFullscreen = false;
                    bryntum.gantt.Fullscreen.exit();                 
                }
                
            } // region controller methods

        },
        {
            key: "onExportPdf",
            value: function onExportPdf() {                

                var arrayTask = [];                                
                this.gantt._store.allRecords.map(function (item) {                    
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
            }   //region controller methods

            },
        {
            key: "onChangeComboLinhaBase",
            value: function onChangeComboLinhaBase(_ref) {
                numLinhaBase = _ref.value;
                getHeadersProject();
            }
            },
            {
                key: "onBuscarLinhaBase",
                value: function onBuscarLinhaBase(_ref) {
                    project.load();
                }
            },
            {
                key: "onVisualizarDetalheLinhaBase",
                value: function onVisualizarDetalheLinhaBase(_ref) {
                    atualizarInfoLb();                    
                }
            },
            
        {
            key: "onExpandAllClick",
            value: function onExpandAllClick() {
                this.gantt.expandAll();
            }
        }, {
            key: "onCollapseAllClick",
            value: function onCollapseAllClick() {
                this.gantt.collapseAll();
            }
        }, {
            key: "onZoomInClick",
            value: function onZoomInClick() {
                this.gantt.zoomIn();
            }
        }, {
            key: "onZoomOutClick",
            value: function onZoomOutClick() {
                this.gantt.zoomOut();
            }
        }, {
            key: "onStartDateChange",
            value: function onStartDateChange(_ref) {
                var value = _ref.value,
                    oldValue = _ref.oldValue;

                if (!oldValue) {
                    // ignore initial set
                    return;
                }

                this.gantt.startDate = bryntum.gantt.DateHelper.add(value, -1, 'week');
                this.gantt.project.setStartDate(value);
            }
        },
            {
            key: "onFeaturesClick",
            value: function onFeaturesClick(_ref3) {
                var item = _ref3.source;
                var gantt = this.gantt;

                if (item.feature) {
                    var feature = gantt.features[item.feature];
                    feature.disabled = !feature.disabled;
                } else if (item.subGrid) {
                    var subGrid = gantt.subGrids[item.subGrid];
                    subGrid.collapsed = !subGrid.collapsed;
                }
            }
        }, {
            key: "onUndoClick",
            value: function onUndoClick() {
                this.gantt.project.stm.canUndo && this.gantt.project.stm.undo();
            }
        }, {
            key: "onRedoClick",
            value: function onRedoClick() {
                this.gantt.project.stm.canRedo && this.gantt.project.stm.redo();
            }
        }, {
            key: "onProjectChanges",
            value: function onProjectChanges(_ref10) {
                var type = _ref10.type;
                var saveButton = this.widgetMap.saveButton; // disable "Save" button if there is no changes in the project data

                saveButton.disabled = type === 'nochanges';
            }
        }, {
                key: "onDesbloquearCronogramaClick",
                value: function onDesbloquearCronogramaClick()
                {          
                    pcInformacao.Show();                                   
                } 

            },
            {
                key: "onEditarCronogramaClick",
                value: function onEditarCronogramaClick() {
                    if (this.isFullscreen) {
                        this.isFullscreen = false;
                        bryntum.gantt.Fullscreen.exit();
                    }
                    
                    AbrirCronograma();                                     
                }
            },
            {
                key: "onEditarEAPClick",
                value: function onEditarEAPClick()
                {
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
                key: "onVisualizarEAPClick",
                value: function onVisualizarEAPClick()
                {
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
            }, {
                key: "onClickOnceClick",
                value: function onClickOnceClick() {
                    if (this.isFullscreen) {
                        this.isFullscreen = false;
                        bryntum.gantt.Fullscreen.exit();
                        window.open(infoCronograma.LinkDownloadClickOnce);
                    } else {
                        window.open(infoCronograma.LinkDownloadClickOnce);
                    }
                    
                }
            }, {
                key: "onVisualizarInfoTarefa",
                value: function onVisualizarInfoTarefa() {
                    var gantt = this.gantt;                    

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
            },
            {
                key: "onCriticalPathsClick",
                value: function onCriticalPathsClick(_ref9) {
                    incluirClassCaminhoCritico();                                        
                }
            }

            

        ], [{
            key: "defaultConfig",
            get: function get() {
                return {
                    // Only one tooltip instance using forSelector to show for every button
                    // which has a tipText property
                    tooltip: {
                        forSelector: '.b-button',
                        onBeforeShow: function onBeforeShow() {
                            var activeButton = this.activeTarget && bryntum.gantt.IdHelper.fromElement(this.activeTarget, 'button');

                            if (activeButton && activeButton.tipText) {
                                this.html = activeButton.tipText;
                            } else {
                                return false;
                            }
                        }
                    },
                    items: [                        
                        {
                            type: 'buttonGroup',
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
                                }
                            ]
                        },
                        {
                            type: 'buttonGroup',
                            items: [
                                {
                                    type: 'button',
                                    color: 'b-red',
                                    ref: 'exportPdfButton',
                                    icon: 'b-icon b-fa b-fa-file-pdf',
                                    //text: 'Export to pdf',
                                    tipText: getTraducao('mapaEstrategico_exportar_para_pdf'),
                                    toggleable: true,
                                    onAction: 'up.onExportPdf'
                                }]
                        },

                        {
                            type: 'buttonGroup',
                            items: [
                                {
                                    type: 'button',
                                    color: 'b-green',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-calendar-alt',                                    
                                    tipText: getTraducao('desbloquear_cronograma'),
                                    onAction: 'up.onDesbloquearCronogramaClick',
                                    hidden: true
                                },
                                {
                                    type: 'button',
                                    color: 'b-green',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-calendar-alt',                                    
                                    tipText: getTraducao('editar_cronograma'),
                                    onAction: 'up.onEditarCronogramaClick',
                                    hidden: false
                                },
                                {
                                    type: 'button',
                                    color: 'b-blue',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-info-circle',
                                    tipText: infoCronograma.DescDownloadClickOnce,
                                    onAction: 'up.onClickOnceClick',
                                    hidden: !infoCronograma.HasLinkDownloadClickOnce
                                },
                                {
                                    type: 'button',
                                    color: 'b-deep-orange',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-edit',                                    
                                    tipText: getTraducao('editar_eap'),
                                    onAction: 'up.onEditarEAPClick',
                                    hidden: false
                                }, {
                                    type: 'button',
                                    color: 'b-deep-orange',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-file-alt',                                    
                                    tipText: getTraducao('visualizar_eap'),
                                    onAction: 'up.onVisualizarEAPClick',
                                    hidden: false
                                },
                                {
                                    type: 'button',
                                    color: 'b-blue',
                                    ref: 'saveButton',
                                    icon: 'b-fa b-fa-file-alt',
                                    tipText: 'Visualizar',
                                    onAction: 'up.onVisualizarInfoTarefa'                                    
                                },
                                {
                                    type: 'button',
                                    color: 'b-red',
                                    ref: 'criticalPathsButton',
                                    icon: 'b-fa b-fa-fire',
                                    //text: getTraducao('caminho_critico'),
                                    tipText: getTraducao('caminho_critico'),
                                    toggleable: true,
                                    onAction: 'up.onCriticalPathsClick'
                                } 
                            ]
                        },
                        {
                            type: 'combo',
                            name: 'eventType',
                            label: '',//getTraducao('linha_de_base') + ':',
                            width: '23em',
                            index: 1,
                            
                            value: numLinhaBase,
                            items: jsonComboLinhaBase,
                            onChange: 'up.onChangeComboLinhaBase'
                        },
                        {
                            type: 'button',
                            color: 'b-blue',
                            ref: 'saveButton',
                            icon: 'b-fa b-fa-info-circle',
                            tipText: 'Visualizar informações da linha de base',//getTipLinhaBase(),
                            onAction: 'up.onVisualizarDetalheLinhaBase'
                        } 
                        ,
                        {
                            type: 'button',
                            color: 'b-green',
                            icon: 'b-fa b-fa-search',
                            tipText: getTraducao('selecionar'),
                            text: getTraducao('selecionar'),
                            onAction: 'up.onBuscarLinhaBase'
                        }
                        
                        
                    ]
                };
            }
        }]);

        return GanttToolbar;
    }(bryntum.gantt.Toolbar);

;
/**
 * @module PercentDonePieColumn
 */

/**
 * A column drawing a pie chart of the `percentDone` value
 *
 * @extends bryntum.gantt.Gantt/column/bryntum.gantt.PercentDoneColumn
 * @classType percentdonepie
 */

var PercentDonePieColumn =
    /*#__PURE__*/
    function (_bryntum$gantt$Percen) {
        _inherits(PercentDonePieColumn, _bryntum$gantt$Percen);

        function PercentDonePieColumn() {
            _classCallCheck(this, PercentDonePieColumn);

            return _possibleConstructorReturn(this, _getPrototypeOf(PercentDonePieColumn).apply(this, arguments));
        }

        _createClass(PercentDonePieColumn, [{
            key: "renderer",
            //endregion
            value: function renderer(_ref11) {
                var value = _ref11.value;

                if (value) {
                    return "<div class=\"b-pie\" style=\"animation-delay: -".concat(value, "s;\"></div>");
                }
            }
        }], [{
            key: "type",
            get: function get() {
                return 'percentdonepie';
            }
        }, {
            key: "isGanttColumn",
            get: function get() {
                return true;
            }
        }, {
            key: "defaults",
            get: function get() {
                return {
                    // Set your default instance config propertiess here
                    htmlEncode: false,
                    align: 'center'
                };
            }
        }]);

        return PercentDonePieColumn;
    }(bryntum.gantt.PercentDoneColumn);

bryntum.gantt.ColumnStore.registerColumnType(PercentDonePieColumn);
/**
 * @module ResourceAvatarColumn
 */

var imgFolderPath = '../_shared/images/users/';
/**
 * bryntum.gantt.Column showing avatars of the assigned resource
 *
 * @extends Grid/column/bryntum.gantt.Column
 * @classType resourceavatar
 */

var ResourceAvatarColumn =
    /*#__PURE__*/
    function (_bryntum$gantt$Resour) {
        _inherits(ResourceAvatarColumn, _bryntum$gantt$Resour);

        function ResourceAvatarColumn() {
            _classCallCheck(this, ResourceAvatarColumn);

            return _possibleConstructorReturn(this, _getPrototypeOf(ResourceAvatarColumn).apply(this, arguments));
        }

        _createClass(ResourceAvatarColumn, [{
            key: "renderer",
            value: function renderer(_ref12) {
                var value = _ref12.value;
                var imgSize = 30,
                    nbrVisible = Math.floor((this.width - 20) / (imgSize + 2));
                return Array.from(value).map(function (assignment, i) {
                    var resource = assignment.resource;
                    var markup = '';

                    if (resource) {
                        var imgMarkup = "<img title=\"".concat(resource.name, " ").concat(assignment.units, "%\" class=\"b-resource-avatar\" src=\"").concat(imgFolderPath).concat(resource.name.toLowerCase() || 'none', ".jpg\">"),
                            lastIndex = nbrVisible - 1,
                            overflowCount = value.length - nbrVisible;

                        if (overflowCount === 0 || i < lastIndex) {
                            markup = imgMarkup;
                        } else if (i === lastIndex && overflowCount > 0) {
                            markup = "<div class=\"b-overflow-img\">\n                                  ".concat(imgMarkup, "\n                                  <span class=\"b-overflow-count\" title=\"").concat(resource.name, " ").concat(assignment.units, "% (+").concat(overflowCount, " more resources)\">+").concat(overflowCount, "</span>\n                              </div>");
                        }
                    }

                    return markup;
                }).join('');
            }
        }], [{
            key: "type",
            get: function get() {
                return 'resourceavatar';
            }
        }, {
            key: "isGanttColumn",
            get: function get() {
                return true;
            }
        }, {
            key: "defaults",
            get: function get() {
                return {
                    repaintOnResize: true,
                    htmlEncode: false,
                    cellCls: 'b-resource-avatar-cell'
                };
            }
        }]);

        return ResourceAvatarColumn;
    }(bryntum.gantt.ResourceAssignmentColumn);

bryntum.gantt.ColumnStore.registerColumnType(ResourceAvatarColumn);
/**
 * @module StatusColumn
 */

/**
 * A column showing the status of a task
 *
 * @extends bryntum.gantt.Gantt/column/bryntum.gantt.Column
 * @classType percentdonepie
 */

var StatusColumn =
    /*#__PURE__*/
    function (_bryntum$gantt$Column) {
        _inherits(StatusColumn, _bryntum$gantt$Column);

        function StatusColumn() {
            _classCallCheck(this, StatusColumn);

            return _possibleConstructorReturn(this, _getPrototypeOf(StatusColumn).apply(this, arguments));
        }

        _createClass(StatusColumn, [{
            key: "renderer",
            //endregion
            value: function renderer(_ref13) {
                var record = _ref13.record;
                var icon = '',
                    status = '',
                    cls = '';

                if (record.isCompleted) {
                    status = 'Completed';
                    cls = 'b-completed';
                } else if (record.endDate > Date.now()) {
                    status = 'Late';
                    cls = 'b-late';
                } else if (record.isStarted) {
                    status = 'Started';
                    cls = 'b-started';
                }

                return status ? "<i class=\"b-fa b-fa-circle ".concat(status, "\"></i>").concat(status) : '';
            }
        }], [{
            key: "type",
            get: function get() {
                return 'statuscolumn';
            }
        }, {
            key: "isGanttColumn",
            get: function get() {
                return true;
            }
        }, {
            key: "defaults",
            get: function get() {
                return {
                    // Set your default instance config propertiess here
                    text: 'Status',
                    htmlEncode: false,
                    editor: false
                };
            }
        }]);

        return StatusColumn;
    }(bryntum.gantt.Column);

bryntum.gantt.ColumnStore.registerColumnType(StatusColumn); // here you can extend our default Task class with your additional fields, methods and logic

var Task =
    /*#__PURE__*/
    function (_bryntum$gantt$TaskMo) {
        _inherits(Task, _bryntum$gantt$TaskMo);

        function Task() {
            _classCallCheck(this, Task);

            return _possibleConstructorReturn(this, _getPrototypeOf(Task).apply(this, arguments));
        }

        _createClass(Task, [{
            key: "isLate",
            get: function get() {
                return this.deadline && Date.now() > this.deadline;
            }
        }], [{
            key: "fields",
            get: function get() {
                return [{
                    name: 'deadline',
                    type: 'date'
                }];
            }
        }]);

        return Task;
    }(bryntum.gantt.TaskModel);
/* eslint-disable no-unused-vars */


var getHeadersProject = function () {

    var headers = {
        typeResult: "jsonProject",
        idprojeto: idProjeto,
        numLinhaBase: numLinhaBase
    };
    //console.log("headers:", headers);
    //console.log("numCenario:", numCenario);
    //project.transport.load.headers = headers;
    project.transport.load.requestConfig.headers = headers;
    return headers;
}

var project = window.project = new bryntum.gantt.ProjectModel({
    // Let the Project know we want to use our own Task model with custom fields / methods
    taskModelClass: Task,
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
    }
});
var configDone = {
    allowCreate: false,
    baseCls: false,
    bufferSize: false,
    cacheGridSize: false,
    creationTooltip: false,
    dependencies: false,
    dependenciesToRefresh: false,
    disabled: false,
    drawnDependencies: false,
    drawnLines: false,
    highlightDependenciesOnEventHover: false,
    isDraw: false,
    isDrawn: false,
    localeClass: false,
    localizableProperties: false,
    overCls: false,
    pathFinderConfig: false,
    showCreationTooltip: false,
    showTooltip: false,
    store: false,
    storeClass: false,
    terminalCls: false,
    terminalSides: false,
    tooltip: false
};
var gantt = new bryntum.gantt.Gantt({
    project: project,
    //startDate: '2016-01-13',
    //endDate: '2016-04-01',
    //disabled: true,
    draggable: false,
    columns: columns,
    subGridConfigs: {
        locked: {
            flex: 1
        },
        normal: {
            flex: 2
        }
    },
    columnLines: false,
    features: {
        taskResize: false,
        rowReorder: false,
        columnReorder: false,
        taskDrag: false,
        contextMenu: false,
        taskEdit: false,
        cellEdit: false,        
        taskDragCreate: false,
        columnDragToolbar: false,
        baselines: false,
        progressLine: false,
        eventFilter: true,
        percentBar : true,
        taskContextMenu: false,
        //tree: true,
        //timeRanges: false,
        sort: false,
        //filter: true,
        filterBar: true,//{ filter: { property: 'food', value: 'Pancake' } },
        nonWorkingTime: false,
        dependencyEdit: false,
        //criticalPaths: false,      
        timeRanges: {
            showCurrentTimeLine: false
        },
        labels: false,
        projectLines: false        
    }
});

//this.gantt.features.criticalPaths.disabled = false;
//#custom start
this.gantt._emptyText = "Sem registros";
//console.log("gantt.features.getRecordCoords():", gantt.getRecordCoords());
//console.log("gantt.features.dependencies:", gantt.features.dependencies.doScheduleDraw());

//#custom end

var panel = new bryntum.gantt.Panel({
    appendTo: 'container',
    layout: 'fit',
    items: [gantt],
    tbar: new GanttToolbar({
        gantt: gantt
    })
});

this.gantt.rowHeight = 31;
//this.widgetMap.settingsButton.menu.widgetMap.barMargin.max = (31 / 2) - 5;

project.load().then(function () {
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