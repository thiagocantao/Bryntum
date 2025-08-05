! function (e, t) {
    "object" == typeof exports && "object" == typeof module ? module.exports = t() : "function" == typeof define && define.amd ? define("Pt_BR", [], t) : "object" == typeof exports ? exports.Pt_BR = t() : (e.bryntum = e.bryntum || {}, e.bryntum.locales = e.bryntum.locales || {}, e.bryntum.locales.Pt_BR = t())
}(window, function () {
    return function (e) {
        var t = {};

        function n(o) {
            if (t[o]) return t[o].exports;
            var r = t[o] = {
                i: o,
                l: !1,
                exports: {}
            };
            return e[o].call(r.exports, r, r.exports, n), r.l = !0, r.exports
        }
        return n.m = e, n.c = t, n.d = function (e, t, o) {
            n.o(e, t) || Object.defineProperty(e, t, {
                enumerable: !0,
                get: o
            })
        }, n.r = function (e) {
            "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
                value: "Module"
            }), Object.defineProperty(e, "__esModule", {
                value: !0
            })
        }, n.t = function (e, t) {
            if (1 & t && (e = n(e)), 8 & t) return e;
            if (4 & t && "object" == typeof e && e && e.__esModule) return e;
            var o = Object.create(null);
            if (n.r(o), Object.defineProperty(o, "default", {
                enumerable: !0,
                value: e
            }), 2 & t && "string" != typeof e)
                for (var r in e) n.d(o, r, function (t) {
                    return e[t]
                }.bind(null, r));
            return o
        }, n.n = function (e) {
            var t = e && e.__esModule ? function () {
                return e.default
            } : function () {
                return e
            };
            return n.d(t, "a", t), t
        }, n.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t)
        }, n.p = "", n(n.s = 6)
    }({
        6: function (e, t, n) {
            "use strict";
            n.r(t);
            var o = {
                localeName: "Pt_BR",
                localeDesc: "Português - Brasil",
                TemplateColumn: {
                    noTemplate: "TemplateColumn precisa de um modelo",
                    noFunction: "TemplateColumn.template deve ser uma função"
                },
                ColumnStore: {
                    columnTypeNotFound: function (e) {
                        return "Tipo de coluna '".concat(e.type, "' não registrado")
                    }
                },
                InstancePlugin: {
                    fnMissing: function (e) {
                        return "Tentando encadear fn".concat(e.plugIntoName, "#").concat(e.fnName, ", mas plugin fn ").concat(e.pluginName, "#").concat(e.fnName, " não existe")
                    },
                    overrideFnMissing: function (e) {
                        return "Tentando substituir fn ".concat(e.plugIntoName, "#").concat(e.fnName, ", mas plugin fn ").concat(e.pluginName, "#").concat(e.fnName, " não existe")
                    }
                },
                ColumnPicker: {
                    columnsMenu: "Colunas",
                    hideColumn: "Ocultar coluna",
                    hideColumnShort: "Ocultar"
                },
                Filter: {
                    applyFilter: "Aplicar filtro",
                    filter: "Filtro",
                    editFilter: "Edita filtro",
                    on: "Em",
                    before: "Antes",
                    after: "Depois",
                    equals: "Igual",
                    lessThan: "Menor que",
                    moreThan: "Maior que",
                    removeFilter: "Remover filtro"
                },
                FilterBar: {
                    enableFilterBar: "Mostrar barra de filtro",
                    disableFilterBar: "Ocultar barra de filtro"
                },
                Group: {
                    groupAscending: "Grupo ascendente",
                    groupDescending: "Grupo descendente",
                    groupAscendingShort: "Ascendente",
                    groupDescendingShort: "Descendente",
                    stopGrouping: "Parar de agrupar",
                    stopGroupingShort: "Parar"
                },
                Search: {
                    searchForValue: "Pesquisar por valor"
                },
                Sort: {
                    sortAscending: "Ordernar ascendente",
                    sortDescending: "Ordenar descendente",
                    multiSort: "Ordenação múltipla",
                    removeSorter: "Remover ordenação",
                    addSortAscending: "Adicionar ordenação ascendente",
                    addSortDescending: "Adicionar ordenação descendente",
                    toggleSortAscending: "Mudar para ascendente",
                    toggleSortDescending: "Mudar para descendente",
                    sortAscendingShort: "Ascendente",
                    sortDescendingShort: "Descendente",
                    removeSorterShort: "Remover",
                    addSortAscendingShort: "+ Ascendente",
                    addSortDescendingShort: "+ Descendente"
                },
                Tree: {
                    noTreeColumn: "Para usar o recurso de árvore, uma coluna deve ser configurada com tree: true"
                },
                Grid: {
                    featureNotFound: function (e) {
                        return "Característica '".concat(e, "' não disponível, verifique se você importou")
                    },
                    invalidFeatureNameFormat: function (e) {
                        return "Nome do recurso inválido '".concat(e, "', deve começar com uma letra minúscula")
                    },
                    removeRow: "Excluir linha",
                    removeRows: "Excluir linhas",
                    loadMask: "Carregando...",
                    loadFailedMessage: "O carregamento de dados falhou.",
                    moveColumnLeft: "Mover para a seção esquerda",
                    moveColumnRight: "Mover para a seção direita"
                },
                Field: {
                    invalidValue: "Valor do campo inválido",
                    minimumValueViolation: "Violação do valor mínimo",
                    maximumValueViolation: "Violação do valor máximo",
                    fieldRequired: "Este campo é obrigatório",
                    validateFilter: "O valor deve ser selecionado na lista"
                },
                DateField: {
                    invalidDate: "Entrada de data inválida"
                },
                TimeField: {
                    invalidTime: "Entrada de tempo inválida"
                },
                DateHelper: {
                    locale: "pt-BR",
                    shortWeek: "S",
                    shortQuarter: "t",
                    week: "Semana",
                    weekStartDay: 0,
                    unitNames: [{
                        single: "milissegundo",
                        plural: "milissegundos",
                        abbrev: "ms"
                    }, {
                        single: "segundo",
                        plural: "segundos",
                        abbrev: "s"
                    }, {
                        single: "minuto",
                        plural: "minutos",
                        abbrev: "min"
                    }, {
                        single: "hora",
                        plural: "horas",
                        abbrev: "h"
                    }, {
                        single: "dia",
                        plural: "dias",
                        abbrev: "dia"
                    }, {
                        single: "semana",
                        plural: "semanas",
                        abbrev: "s"
                    }, {
                        single: "mês",
                        plural: "meses",
                        abbrev: "mes"
                    }, {
                        single: "trimestre",
                        plural: "trimestres",
                        abbrev: "tri"
                    }, {
                        single: "ano",
                        plural: "anos",
                        abbrev: "ano"
                    }],
                    unitAbbreviations: [
                        ["mil"],
                        ["s", "seg"],
                        ["m", "min"],
                        ["h", "h"],
                        ["d"],
                        ["s", "sem"],
                        ["me", "mes"],
                        ["t", "tri"],
                        ["a", "ano"]
                    ],
                    parsers: {
                        L: "DD/MM/YYYY",
                        LT: "HH:mm A"
                    }
                },
                BooleanCombo: {
                    Yes: "Sim",
                    No: "Não"
                }
            },
                r = {
                    ExcelExporter: {
                        "No resource assigned": "Nenhum recurso atribuído"
                    },
                    ResourceInfoColumn: {
                        eventCountText: function (e) {
                            return e + " evento" + (1 !== e ? "s" : "")
                        }
                    },
                    Dependencies: {
                        from: "De",
                        to: "Para",
                        valid: "Válido",
                        invalid: "Inválido",
                        Checking: "Verificação…"
                    },
                    DependencyEdit: {
                        From: "De",
                        To: "Para",
                        Type: "Tipo",
                        Lag: "Atraso",
                        "Edit dependency": "Editar dependência",
                        Save: "Salvar",
                        Delete: "Excluir",
                        Cancel: "Cancelar",
                        StartToStart: "Começar pelo início",
                        StartToEnd: "Começar pelo fim",
                        EndToStart: "Terminar pelo início",
                        EndToEnd: "Terminar pelo final"
                    },
                    EventEdit: {
                        Name: "Nome",
                        Resource: "Recurso",
                        Start: "Começar",
                        End: "Fim",
                        Save: "Salvar",
                        Delete: "Excluir",
                        Cancel: "Cancelar",
                        "Edit Event": "Editar evento"
                    },
                    Scheduler: {
                        "Add event": "Adicionar Evento",
                        "Delete event": "Excluir evento",
                        "Unassign event": "Desatribuir evento"
                    },
                    HeaderContextMenu: {
                        pickZoomLevel: "Zoom",
                        activeDateRange: "Intervalo de datas",
                        startText: "Data de início",
                        endText: "Data final",
                        todayText: "Hoje"
                    },
                    EventFilter: {
                        filterEvents: "Filtrar tarefas",
                        byName: "Por nome"
                    },
                    TimeRanges: {
                        showCurrentTimeLine: "Mostrar a linha do tempo atual"
                    },
                    PresetManager: {
                        minuteAndHour: {
                            topDateFormat: "ddd MM/DD, hA"
                        },
                        hourAndDay: {
                            topDateFormat: "ddd MM/DD"
                        },
                        weekAndDay: {
                            displayDateFormat: "hh:mm A"
                        }
                    }
                };
            for (var a in o) r[a] = o[a];
            var i = r,
                l = {
                    AddNewColumn: {
                        "New Column": "Nova coluna"
                    },
                    CalendarColumn: {
                        Calendar: "Calendário"
                    },
                    EarlyStartDateColumn: {
                        "Early Start": "Início antecipado"
                    },
                    EarlyEndDateColumn: {
                        "Early End": "Fim adiantado"
                    },
                    LateStartDateColumn: {
                        "Late Start": "Começo tardio"
                    },
                    LateEndDateColumn: {
                        "Late End": "Fim atrasado"
                    },
                    TotalSlackColumn: {
                        "Total Slack": "Folga total"
                    },
                    ConstraintDateColumn: {
                        "Constraint Date": "Data de restrição"
                    },
                    ConstraintTypeColumn: {
                        "Constraint Type": "Tipo de restrição"
                    },
                    DependencyColumn: {
                        "Invalid dependency found, change is reverted": "Dependência inválida encontrada, alteração é revertida"
                    },
                    DurationColumn: {
                        Duration: "Duração"
                    },
                    EffortColumn: {
                        Effort: "Esforço"
                    },
                    EndDateColumn: {
                        Finish: "Terminar"
                    },
                    EventModeColumn: {
                        "Event mode": "Modo de evento",
                        Manual: "Manual",
                        Auto: "Auto"
                    },
                    ManuallyScheduledColumn: {
                        "Manually scheduled": "Agendado manualmente"
                    },
                    MilestoneColumn: {
                        Milestone: "Marco histórico"
                    },
                    NameColumn: {
                        Name: "Nome"
                    },
                    NoteColumn: {
                        Note: "Nota"
                    },
                    PercentDoneColumn: {
                        "% Done": "% Pronto"
                    },
                    PredecessorColumn: {
                        Predecessors: "Antecessores"
                    },
                    ResourceAssignmentColumn: {
                        "Assigned Resources": "Recursos atribuídos"
                    },
                    SchedulingModeColumn: {
                        "Scheduling Mode": "Modo de agendamento"
                    },
                    SequenceColumn: {
                        Sequence: "Sequência"
                    },
                    ShowInTimelineColumn: {
                        "Show in timeline": "Mostrar na linha do tempo"
                    },
                    StartDateColumn: {
                        Start: "Começar"
                    },
                    SuccessorColumn: {
                        Successors: "Sucessores"
                    },
                    WBSColumn: {
                        WBS: "WBS"
                    },
                    ProjectLines: {
                        "Project Start": "Início do projeto",
                        "Project End": "Final do projeto"
                    },
                    TaskTooltip: {
                        Start: "Início",
                        End: "Final",
                        Duration: "Duração",
                        Complete: "Completo"
                    },
                    AssignmentGrid: {
                        Name: "Nome do recurso",
                        Units: "Unidades",
                        "%": "%",
                        unitsTpl: function (e) {
                            var t = e.value;
                            return t ? t + "%" : ""
                        }
                    },
                    AssignmentPicker: {
                        Save: "Salvar",
                        Cancel: "Cancelar"
                    },
                    AssignmentEditGrid: {
                        Name: "Nome do recurso",
                        Units: "Unidades"
                    },
                    ConstraintTypePicker: {
                        "Must start on": "Deve começar",
                        "Must finish on": "Deve terminar em",
                        "Start no earlier than": "Começar não mais cedo do que",
                        "Start no later than": "Começar não mais tarde do que",
                        "Finish no earlier than": "Terminar não mais cedo do que",
                        "Finish no later than": "Termine o mais tardar"
                    },
                    Gantt: {
                        Add: "Adicionar...",
                        "New Task": "Nova tarefa",
                        "Task above": "Tarefa acima",
                        "Task below": "Tarefa abaixo",
                        "Delete task": "Excluir tarefa",
                        Milestone: "Marco histórico",
                        "Sub-task": "Subtarefa",
                        Successor: "Sucessor",
                        Predecessor: "Antecessor"
                    },
                    GanttCommon: {
                        SS: "SS",
                        SF: "SF",
                        FS: "FS",
                        FF: "FF",
                        StartToStart: "Start-to-Start",
                        StartToEnd: "Start-to-End",
                        EndToStart: "End-to-Start",
                        EndToEnd: "End-to-End",
                        dependencyTypes: ["SS", "SF", "FS", "FF"],
                        dependencyTypesLong: ["Start-to-Start", "Start-to-End", "End-to-Start", "End-to-End"]
                    },
                    TaskEdit: {
                        Edit: "Editar"
                    },
                    TaskEditor: {
                        editorWidth: "45em",
                        Information: "Informação",
                        Save: "Salvar",
                        Cancel: "Cancelar",
                        Delete: "Exluir"
                    },
                    GeneralTab: {
                        labelWidth: "6.5em",
                        General: "Geral",
                        Name: "Nome",
                        "% complete": "% completo",
                        Duration: "Duração",
                        Start: "Iniciar",
                        Finish: "Terminar",
                        Effort: "Esforço",
                        Dates: "Datas"
                    },
                    AdvancedTab: {
                        labelWidth: "11.5em",
                        Advanced: "Avançado",
                        Calendar: "Calendário",
                        "Scheduling mode": "Modo de agendamento",
                        "Effort driven": "Esforço dirigido",
                        "Manually scheduled": "Agendado manualmente",
                        "Constraint type": "Tipo de restrição",
                        "Constraint date": "Data de restrição",
                        Constraint: "Restrição"
                    },
                    DependencyTab: {
                        Predecessors: "Antecessores",
                        Successors: "Sucessores",
                        ID: "ID",
                        Name: "Nome",
                        Type: "Tipo",
                        Lag: "Atraso",
                        "Cyclic dependency has been detected": "Dependência cíclica foi detectada"
                    },
                    ResourcesTab: {
                        unitsTpl: function (e) {
                            var t = e.value;
                            return "".concat(t, "%")
                        },
                        Resources: "Recursos",
                        Resource: "Recurso",
                        Units: "Unidades"
                    },
                    NotesTab: {
                        Notes: "Notas"
                    },
                    SchedulingModePicker: {
                        Normal: "Normal",
                        "Fixed Duration": "Duração fixa",
                        "Fixed Units": "Unidades fixas",
                        "Fixed Effort": "Esforço fixo"
                    }
                };
            for (var s in i) l[s] = i[s];
            t.default = l
        }
    }).default
});