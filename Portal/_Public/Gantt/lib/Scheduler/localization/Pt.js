import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Pt.js';

const locale = {

    localeName : 'Pt',
    localeDesc : 'Português',
    localeCode : 'pt',

    Object : {
        newEvent : 'Novo evento'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' evento' + (data !== 1 ? 's' : '')
    },

    Dependencies : {
        from    : 'De',
        to      : 'Até',
        valid   : 'Válido',
        invalid : 'Inváldio'
    },

    DependencyType : {
        SS           : 'II',
        SF           : 'IF',
        FS           : 'FI',
        FF           : 'FF',
        StartToStart : 'Início a Início',
        StartToEnd   : 'Início a Fim',
        EndToStart   : 'Fim a Início',
        EndToEnd     : 'Fim a Fim',
        short        : [
            'II',
            'IF',
            'FI',
            'FF'
        ],
        long : [
            'Início a Início',
            'Início a Fim',
            'Fim a Início',
            'Fim a Fim'
        ]
    },

    DependencyEdit : {
        From              : 'De',
        To                : 'A',
        Type              : 'Tipo',
        Lag               : 'Atraso',
        'Edit dependency' : 'Editar dependência',
        Save              : 'Guardar',
        Delete            : 'Eliminar',
        Cancel            : 'Cancelar',
        StartToStart      : 'Início a Início',
        StartToEnd        : 'Início a Fim',
        EndToStart        : 'Fim a Início',
        EndToEnd          : 'Fim a Fim'
    },

    EventEdit : {
        Name         : 'Nome',
        Resource     : 'Recurso',
        Start        : 'Início',
        End          : 'Fim',
        Save         : 'Guardar',
        Delete       : 'Eliminar',
        Cancel       : 'Cancelar',
        'Edit event' : 'Editar evento',
        Repeat       : 'Repetir'
    },

    EventDrag : {
        eventOverlapsExisting : 'O evento sobrepõe-se ao evento existente para este recurso',
        noDropOutsideTimeline : 'O evento não pode ser deixado completamente fora da cronologia'
    },

    SchedulerBase : {
        'Add event'      : 'Adicionar evento',
        'Delete event'   : 'Eliminar evento',
        'Unassign event' : 'Remover atribuição de evento',
        color            : 'Cor'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Ampliação',
        activeDateRange : 'Intervalo de datas',
        startText       : 'Data de início',
        endText         : 'Data de fim',
        todayText       : 'Hoje'
    },

    EventCopyPaste : {
        copyEvent  : 'Copiar evento',
        cutEvent   : 'Cortar evento',
        pasteEvent : 'Colar evento'
    },

    EventFilter : {
        filterEvents : 'Filtrar tarefas',
        byName       : 'Por nome'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Mostrar cronologia atual'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Segundos'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dia'
        },
        day : {
            name : 'Dia/horas'
        },
        week : {
            name : 'Semana/horas'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Semana/dias'
        },
        dayAndMonth : {
            name : 'Mês'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Semana'
        },
        weekAndMonth : {
            name : 'Semanas'
        },
        weekAndDayLetter : {
            name : 'Semanas/dias da semana'
        },
        weekDateAndMonth : {
            name : 'Meses/semanas'
        },
        monthAndYear : {
            name : 'Meses'
        },
        year : {
            name : 'Anos'
        },
        manyYears : {
            name : 'Múltiplos anos'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Está a eliminar um evento',
        'delete-all-message'        : 'Quer eliminar todas as ocorrências deste evento?',
        'delete-further-message'    : 'Quer eliminar esta e todas as ocorrências futuras deste evento ou apenas a ocorrência selecionada?',
        'delete-further-btn-text'   : 'Eliminar todos os eventos futuros',
        'delete-only-this-btn-text' : 'Eliminar apenas este evento',
        'update-title'              : 'Está a alterar um evento repetido',
        'update-all-message'        : 'Quer alterar todas as ocorrências deste evento?',
        'update-further-message'    : 'Quer alterar apenas esta ocorrência do evento ou esta e todas as ocorrências futuras?',
        'update-further-btn-text'   : 'Todos os eventos futuros',
        'update-only-this-btn-text' : 'Apenas este evento',
        Yes                         : 'Sim',
        Cancel                      : 'Cancelar',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' e ',
        Daily                           : 'Diariamente',
        'Weekly on {1}'                 : ({ days }) => `Semanalmente a ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mensalmente a ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Anualmente a ${days} de ${months}`,
        'Every {0} days'                : ({ interval }) => `A cada ${interval} dias`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `A cada ${interval} semanas em ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `A cada ${interval} meses em ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `A cada ${interval} anos em ${days} de ${months}`,
        position1                       : 'no primeiro',
        position2                       : 'no segundo',
        position3                       : 'no terceiro',
        position4                       : 'no quarto',
        position5                       : 'no quinto',
        'position-1'                    : 'no último',
        day                             : 'dia',
        weekday                         : 'dia da semana',
        'weekend day'                   : 'dia de fim de semana',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Repetir evento',
        Cancel              : 'Cancelar',
        Save                : 'Guardar',
        Frequency           : 'Frequência',
        Every               : 'A cada',
        DAILYintervalUnit   : 'dia(s)',
        WEEKLYintervalUnit  : 'semana(s)',
        MONTHLYintervalUnit : 'mês(es)',
        YEARLYintervalUnit  : 'ano(s)',
        Each                : 'Cada',
        'On the'            : 'Em',
        'End repeat'        : 'Repetir no fim',
        'time(s)'           : 'vez(es)'
    },

    RecurrenceDaysCombo : {
        day           : 'dia',
        weekday       : 'dia da semana',
        'weekend day' : 'dia do fim de semana'
    },

    RecurrencePositionsCombo : {
        position1    : 'primeiro',
        position2    : 'segundo',
        position3    : 'terceiro',
        position4    : 'quarto',
        position5    : 'quinto',
        'position-1' : 'último'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nunca',
        After     : 'Depois',
        'On date' : 'Na data'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Sem repetição',
        Daily   : 'Diariamente',
        Weekly  : 'Semanalmente',
        Monthly : 'Mensalmente',
        Yearly  : 'Anualmente'
    },

    RecurrenceCombo : {
        None   : 'Nenhum',
        Custom : 'Personalizar'
    },

    Summary : {
        'Summary for' : date => `Resumo de ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Agendamento completo',
        currentview  : 'Agendamento visível',
        daterange    : 'Intervalo de datas',
        completedata : 'Agendamento completo (para todos os eventos)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Intervalo de agendamento',
        'Export from'    : 'De',
        'Export to'      : 'Até'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nenhum recurso atribuído'
    },

    CrudManagerView : {
        serverResponseLabel : 'Resposta do servidor:'
    },

    DurationColumn : {
        Duration : 'Duração'
    }
};

export default LocaleHelper.publishLocale(locale);
