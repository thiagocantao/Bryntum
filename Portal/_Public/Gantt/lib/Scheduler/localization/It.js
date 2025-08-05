import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/It.js';

const locale = {

    localeName : 'It',
    localeDesc : 'Italiano',
    localeCode : 'it',

    Object : {
        newEvent : 'Nuovo evento'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' event' + (data !== 1 ? 'i' : 'o')
    },

    Dependencies : {
        from    : 'Da',
        to      : 'A',
        valid   : 'Valido',
        invalid : 'Non valido'
    },

    DependencyType : {
        SS           : 'II',
        SF           : 'IF',
        FS           : 'FI',
        FF           : 'FF',
        StartToStart : 'Inizio-Inizio',
        StartToEnd   : 'Inizio-Fine',
        EndToStart   : 'Fine-Inizio',
        EndToEnd     : 'Fine-Fine',
        short        : [
            'II',
            'IF',
            'FI',
            'FF'
        ],
        long : [
            'Inizio-Inizio',
            'Inizio-Fine',
            'Fine-Inizio',
            'Fine-Fine'
        ]
    },

    DependencyEdit : {
        From              : 'Da',
        To                : 'A',
        Type              : 'Tipo',
        Lag               : 'Ritardo',
        'Edit dependency' : 'Modifica dipendenza',
        Save              : 'Salva',
        Delete            : 'Elimina',
        Cancel            : 'Annulla',
        StartToStart      : 'Inizio-Inizio',
        StartToEnd        : 'Inizio-Fine',
        EndToStart        : 'Fine-Inizio',
        EndToEnd          : 'Fine-Fine'
    },

    EventEdit : {
        Name         : 'Nome',
        Resource     : 'Risorsa',
        Start        : 'Inizio',
        End          : 'Fine',
        Save         : 'Salva',
        Delete       : 'Elimina',
        Cancel       : 'Annulla',
        'Edit event' : 'Modifica evento',
        Repeat       : 'Ripeti'
    },

    EventDrag : {
        eventOverlapsExisting : 'L’evento si sovrappone a un evento esistente per questa risorsa',
        noDropOutsideTimeline : 'L’evento non può essere lasciato completamente al di fuori della linea temporale'
    },

    SchedulerBase : {
        'Add event'      : 'Aggiungi evento',
        'Delete event'   : 'Elimina evento',
        'Unassign event' : 'Annulla l’assegnazione dell’evento',
        color            : 'Colore'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Intervallo di date',
        startText       : 'Data d’inizio',
        endText         : 'Data di fine',
        todayText       : 'Oggi'
    },

    EventCopyPaste : {
        copyEvent  : 'Copia evento',
        cutEvent   : 'Taglia evento',
        pasteEvent : 'Incolla evento'
    },

    EventFilter : {
        filterEvents : 'Filtra attività',
        byName       : 'Per nome'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Mostra linea temporale corrente'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Secondi'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Giorno'
        },
        day : {
            name : 'Giorno/ore'
        },
        week : {
            name : 'Settimana/ore'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Settimana/giorni'
        },
        dayAndMonth : {
            name : 'Mese'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Settimana'
        },
        weekAndMonth : {
            name : 'Settimane'
        },
        weekAndDayLetter : {
            name : 'Settimane/giorni lavorativi'
        },
        weekDateAndMonth : {
            name : 'Mesi/settimane'
        },
        monthAndYear : {
            name : 'Mesi'
        },
        year : {
            name : 'Anni'
        },
        manyYears : {
            name : 'Più anni'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Stai per eliminare un evento',
        'delete-all-message'        : 'Vuoi eliminare tutte le occorrenze di questo evento?',
        'delete-further-message'    : 'Vuoi eliminare questa e tutte le future occorrenze di questo evento, o solo l’occorrenza selezionata?',
        'delete-further-btn-text'   : 'Elimina tutti gli eventi futuri',
        'delete-only-this-btn-text' : 'Elimina solo questo evento',
        'update-title'              : 'Stai per modificare un evento ricorrente',
        'update-all-message'        : 'Vuoi modificare tutte le occorrenze di questo evento?',
        'update-further-message'    : 'Vuoi modificare solo questa occorrenza dell’evento o questa e tutte le future occorrenze?',
        'update-further-btn-text'   : 'Tutti gli eventi futuri',
        'update-only-this-btn-text' : 'Solo questo evento',
        Yes                         : 'Sì',
        Cancel                      : 'Annulla',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' e ',
        Daily                           : 'Ogni giorno',
        'Weekly on {1}'                 : ({ days }) => `Ogni settimana il ${days}`,
        'Monthly on {1}'                : ({ days }) => `Ogni mese il ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Ogni anno il ${days} di ${months}`,
        'Every {0} days'                : ({ interval }) => `Ogni ${interval} giorni`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Ogni ${interval} settimane il ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Ogni ${interval} mesi il ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Ogni ${interval} anni il ${days} di ${months}`,
        position1                       : 'il primo',
        position2                       : 'il secondo',
        position3                       : 'il terzo',
        position4                       : 'il quarto',
        position5                       : 'il quinto',
        'position-1'                    : 'l’ultimo',
        day                             : 'giorno',
        weekday                         : 'giorno della settimana',
        'weekend day'                   : 'giorno del fine settimana',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ripeti evento',
        Cancel              : 'Annulla',
        Save                : 'Salva',
        Frequency           : 'Frequenza',
        Every               : 'Ogni',
        DAILYintervalUnit   : 'giorno/i',
        WEEKLYintervalUnit  : 'settimana/e',
        MONTHLYintervalUnit : 'mese/i',
        YEARLYintervalUnit  : 'anno/i',
        Each                : 'Ogni',
        'On the'            : 'Il',
        'End repeat'        : 'Termina ripetizione',
        'time(s)'           : 'volta/e'
    },

    RecurrenceDaysCombo : {
        day           : 'giorno',
        weekday       : 'giorno della settimana',
        'weekend day' : 'giorno del fine settimana'
    },

    RecurrencePositionsCombo : {
        position1    : 'primo',
        position2    : 'secondo',
        position3    : 'terzo',
        position4    : 'quarto',
        position5    : 'quinto',
        'position-1' : 'ultimo'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Mai',
        After     : 'Dopo',
        'On date' : 'Alla data'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Nessuna ripetizione',
        Daily   : 'Ogni giorno',
        Weekly  : 'Ogni settimana',
        Monthly : 'Ogni mese',
        Yearly  : 'Ogni anno'
    },

    RecurrenceCombo : {
        None   : 'Nessuna',
        Custom : 'Personalizza...'
    },

    Summary : {
        'Summary for' : date => `Riepilogo per ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Programma completo',
        currentview  : 'Programma visibile',
        daterange    : 'Intervallo di date',
        completedata : 'Programma completo (per tutti gli eventi)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Intervallo del programma',
        'Export from'    : 'Da',
        'Export to'      : 'A'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nessuna risorsa assegnata'
    },

    CrudManagerView : {
        serverResponseLabel : 'Risposta del server:'
    },

    DurationColumn : {
        Duration : 'Durata'
    }
};

export default LocaleHelper.publishLocale(locale);
