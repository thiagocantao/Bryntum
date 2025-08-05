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
                key: "onExpandAllClick",
                value: function onExpandAllClick() {
                    this.gantt.expandAll();
                }
            }, {
                key: "onCollapseAllClick",
                value: function onCollapseAllClick() {
                    this.gantt.collapseAll();
                }
            },

            {
            key: "onZoomInClick",
            value: function onZoomInClick() {
                this.gantt.zoomIn();
            }
        }, {
            key: "onZoomOutClick",
            value: function onZoomOutClick() {
                this.gantt.zoomOut();
            }
            }        
            , {
                key: "onCloseClick",
                value: function onZoomOutClick() {
                    if (this.isFullscreen) {
                        this.isFullscreen = false;
                        bryntum.gantt.Fullscreen.exit();
                    } 
                    window.top.fechaModal();
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
                                }]
                        },
                        {
                            type: 'button',
                            color: 'b-red',
                            ref: 'closeButton',
                            icon: 'b-fa b-fa-window-close',
                            text: getTraducao('fechar'),
                            tipText: getTraducao('fechar'),
                            onAction: 'up.onCloseClick'
                        }
                    ]
                };
            }
            }]
        );

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


var project = window.project = new bryntum.gantt.ProjectModel({
    // Let the Project know we want to use our own Task model with custom fields / methods
    taskModelClass: Task,
    transport: {
        load: {            
            requestConfig: {
                url: '../../../../ApiHandler/GanttPlanoAcaoHandler.ashx',
                method: 'POST',
                // get rid of cache-buster parameter
                disableCaching: false,
                //paramName: 'rq',                           
                // custom request headers
                headers: {
                    typeResult: "jsonProject",
                    iniciaisObjeto: iniciaisObjeto,
                    idObjeto: idObjeto,                    
                    isIniciativas: isIniciativas,
                    idEntidade: idEntidade,
                    idPlanoAcao: idPlanoAcao
                }
            }
        }
    }
});

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
        percentBar: true,
        taskContextMenu: false,
        //tree: true,
        //timeRanges: false,
        sort: false,
        //filter: true,
        filterBar: true,
        nonWorkingTime: false,
        dependencyEdit: false,
        criticalPaths: false,
        timeRanges: {
            showCurrentTimeLine: false
        },        
        labels: false,
        projectLines: false
    }
});

var panel = new bryntum.gantt.Panel({
    title: strTituloLabel + ' : ' + strDescricaoTitulo,
    appendTo: 'container',
    layout: 'fit',
    items: [gantt],
    tbar: new GanttToolbar({
        gantt: gantt
    })
});

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

    gantt.expandAll();
});