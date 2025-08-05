import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Es.js';

const locale = {

    localeName : 'Es',
    localeDesc : 'Español',
    localeCode : 'es',

    Object : {
        newEvent : 'Nuevo evento'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' evento' + (data !== 1 ? 's' : '')
    },

    Dependencies : {
        from    : 'Desde',
        to      : 'a',
        valid   : 'Válido',
        invalid : 'No válido'
    },

    DependencyType : {
        SS           : 'II',
        SF           : 'IF',
        FS           : 'FI',
        FF           : 'FF',
        StartToStart : 'De inicio a inicio',
        StartToEnd   : 'De inicio a finalización',
        EndToStart   : 'De finalización a inicio',
        EndToEnd     : 'De finalización a finalización',
        short        : [
            'II',
            'IF',
            'FI',
            'FF'
        ],
        long : [
            'De inicio a inicio',
            'De inicio a finalización',
            'De finalización a inicio',
            'De finalización a finalización'
        ]
    },

    DependencyEdit : {
        From              : 'Desde',
        To                : 'Hasta',
        Type              : 'Tipo',
        Lag               : 'Retraso',
        'Edit dependency' : 'Editar dependencia',
        Save              : 'Guardar',
        Delete            : 'Eliminar',
        Cancel            : 'Cancelar',
        StartToStart      : 'De inicio a inicio',
        StartToEnd        : 'De inicio a finalización',
        EndToStart        : 'De finalización a inicio',
        EndToEnd          : 'De finalización a finalización'
    },

    EventEdit : {
        Name         : 'Nombre',
        Resource     : 'Recurso',
        Start        : 'Inicio',
        End          : 'Finalización',
        Save         : 'Guardar',
        Delete       : 'Eliminar',
        Cancel       : 'Cancelar',
        'Edit event' : 'Editar evento',
        Repeat       : 'Repetir'
    },

    EventDrag : {
        eventOverlapsExisting : 'El evento se superpone con un evento existente para este recurso',
        noDropOutsideTimeline : 'No se puede soltar el evento completamente fuera de la línea temporal'
    },

    SchedulerBase : {
        'Add event'      : 'Añadir evento',
        'Delete event'   : 'Eliminar evento',
        'Unassign event' : 'Desasignar evento',
        color            : 'Color'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Rango de fechas',
        startText       : 'Fecha de inicio',
        endText         : 'Fecha de finalización',
        todayText       : 'Hoy'
    },

    EventCopyPaste : {
        copyEvent  : 'Copiar evento',
        cutEvent   : 'Cortar evento',
        pasteEvent : 'Pegar evento'
    },

    EventFilter : {
        filterEvents : 'Filtrar tareas',
        byName       : 'Por nombre'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Mostrar línea temporal actual'
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
            name              : 'Día'
        },
        day : {
            name : 'Día/horas'
        },
        week : {
            name : 'Semana/horas'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Semana/días'
        },
        dayAndMonth : {
            name : 'Mes'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Semana'
        },
        weekAndMonth : {
            name : 'Semanas'
        },
        weekAndDayLetter : {
            name : 'Semanas/días laborables'
        },
        weekDateAndMonth : {
            name : 'Meses/semanas'
        },
        monthAndYear : {
            name : 'Mes'
        },
        year : {
            name : 'Años'
        },
        manyYears : {
            name : 'Múltiples años'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Está eliminando un evento',
        'delete-all-message'        : '¿Desea eliminar todas las instancias de este evento?',
        'delete-further-message'    : 'Desea eliminar esta y toda otra futura instancia de este evento o solo la instancia selecccionada?',
        'delete-further-btn-text'   : 'Eliminar todos los eventos futuros',
        'delete-only-this-btn-text' : 'Eliminar solo este evento',
        'update-title'              : 'Está cambiando un evento recurrente',
        'update-all-message'        : '¿Desea cambiar todas las instancias de este evento?',
        'update-further-message'    : '¿Desea cambiar solo esta instancia del evento o esta instancia y toda otra futura instancia?',
        'update-further-btn-text'   : 'Todos los futuros eventos',
        'update-only-this-btn-text' : 'Solo este evento',
        Yes                         : 'Sí',
        Cancel                      : 'Cancelar',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : 'y',
        Daily                           : 'Diariamente',
        'Weekly on {1}'                 : ({ days }) => `Semanalmente los ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mensualmente el ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Anualmente el ${days} de ${months}`,
        'Every {0} days'                : ({ interval }) => `Cada ${interval} días`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Cada ${interval} semanas el ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Cada ${interval} meses el ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Cada ${interval} años el ${days} de ${months}`,
        position1                       : 'el primero',
        position2                       : 'el segundo',
        position3                       : 'el tercero',
        position4                       : 'el cuarto',
        position5                       : 'el quinto',
        'position-1'                    : 'el último',
        day                             : 'día',
        weekday                         : 'día laborable',
        'weekend day'                   : 'día del fin de semana',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Repetir evento',
        Cancel              : 'Cancelar',
        Save                : 'Guardar',
        Frequency           : 'Frecuencia',
        Every               : 'Cada',
        DAILYintervalUnit   : 'día(s)',
        WEEKLYintervalUnit  : 'semana(s)',
        MONTHLYintervalUnit : 'mes(es)',
        YEARLYintervalUnit  : 'año(s)',
        Each                : 'Cada',
        'On the'            : 'El',
        'End repeat'        : 'Repetir finalización',
        'time(s)'           : 'vez(es)'
    },

    RecurrenceDaysCombo : {
        day           : 'día',
        weekday       : 'día de la semana',
        'weekend day' : 'día del fin de semana'
    },

    RecurrencePositionsCombo : {
        position1    : 'primero',
        position2    : 'segundo',
        position3    : 'tercero',
        position4    : 'cuarto',
        position5    : 'quinto',
        'position-1' : 'último'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nunca',
        After     : 'Después',
        'On date' : 'En fecha'
    },

    RecurrenceFrequencyCombo : {
        None    : 'No repetir',
        Daily   : 'Diariamente',
        Weekly  : 'Semanalmente',
        Monthly : 'Mensualmente',
        Yearly  : 'Anualmente'
    },

    RecurrenceCombo : {
        None   : 'Ninguno',
        Custom : 'Personalizar...'
    },

    Summary : {
        'Summary for' : date => `Resumen para ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Programa completo',
        currentview  : 'Programa visible',
        daterange    : 'Rango de fechas',
        completedata : 'Programa completo (para todos los eventos)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Rango del programa',
        'Export from'    : 'Desde',
        'Export to'      : 'a'
    },

    ExcelExporter : {
        'No resource assigned' : 'Sin recursos asignados'
    },

    CrudManagerView : {
        serverResponseLabel : 'Respuesta del servidor:'
    },

    DurationColumn : {
        Duration : 'Duración'
    }
};

export default LocaleHelper.publishLocale(locale);
