import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/SvSE.js';

const locale = {

    localeName : 'SvSE',
    localeDesc : 'Svenska',
    localeCode : 'sv-SE',

    Object : {
        newEvent : 'Ny händelse'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' händelse' + (data !== 1 ? 'r' : '')
    },

    Dependencies : {
        from    : 'Från',
        to      : 'Till',
        valid   : 'Giltig',
        invalid : 'Ogiltig'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SA',
        FS           : 'AS',
        FF           : 'AA',
        StartToStart : 'Start-Till-Start',
        StartToEnd   : 'Start-Till-Avslut',
        EndToStart   : 'Avslut-Till-Start',
        EndToEnd     : 'Avslut-Till-Avslut',
        short        : [
            'SS',
            'SA',
            'AS',
            'AA'
        ],
        long : [
            'Start-Till-Start',
            'Start-Till-Avslut',
            'Avslut-Till-Start',
            'Avslut-Till-Avslut'
        ]
    },

    DependencyEdit : {
        From              : 'Från',
        To                : 'Till',
        Type              : 'Typ',
        Lag               : 'Fördröjning',
        'Edit dependency' : 'Ändra beroende',
        Save              : 'Spara',
        Delete            : 'Ta bort',
        Cancel            : 'Avbryt',
        StartToStart      : 'Start till Start',
        StartToEnd        : 'Start till Slut',
        EndToStart        : 'Slut till Start',
        EndToEnd          : 'Slut till Slut'
    },

    EventEdit : {
        Name         : 'Namn',
        Resource     : 'Resurs',
        Start        : 'Start',
        End          : 'Slut',
        Save         : 'Spara',
        Delete       : 'Ta bort',
        Cancel       : 'Avbryt',
        'Edit event' : 'Redigera bokning',
        Repeat       : 'Upprepa'
    },

    EventDrag : {
        eventOverlapsExisting : 'Överlappar befintlig händelse för den här resursen',
        noDropOutsideTimeline : 'Händelsen kan inte släppas utanför tidsaxeln'
    },

    SchedulerBase : {
        'Add event'      : 'Lägg till bokning',
        'Delete event'   : 'Ta bort bokning',
        'Unassign event' : 'Ta bort resurskoppling',
        color            : 'Färg'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Välj zoomnivå',
        activeDateRange : 'Aktivt datumintervall',
        startText       : 'Start datum',
        endText         : 'Slut datum',
        todayText       : 'I dag'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiera bokning',
        cutEvent   : 'Klipp ut bokning',
        pasteEvent : 'Klistra in bokning'
    },

    EventFilter : {
        filterEvents : 'Filtrera händelser',
        byName       : 'Med namn'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Visa aktuell tidslinje'
    },

    PresetManager : {
        secondAndMinute : {
            name : 'Sekunder'
        },
        minuteAndHour : {
            topDateFormat : 'ddd, DD/MM, h:mm'
        },
        hourAndDay : {
            topDateFormat : 'ddd DD/MM',
            name          : 'Dag'
        },
        day : {
            name : 'Dag'
        },
        week : {
            name : 'Vecka/timmar'
        },
        dayAndWeek : {
            name : 'Vecka/dagar'
        },
        dayAndMonth : {
            name : 'Månad'
        },
        weekAndDay : {
            displayDateFormat : 'HH:mm',
            name              : 'Vecka'
        },
        weekAndMonth : {
            name : 'Veckor'
        },
        weekAndDayLetter : {
            name : 'Veckor/veckodagar'
        },
        weekDateAndMonth : {
            name : 'Månader/veckor'
        },
        monthAndYear : {
            name : 'Månader'
        },
        year : {
            name : 'År'
        },
        manyYears : {
            name : 'Flera år'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Borttagning av bokning',
        'delete-all-message'        : 'Vill du ta bort alla instanser av denna bokning?',
        'delete-further-message'    : 'Vill du ta bort denna och alla framtida instanser av denna bokning, eller bara denna?',
        'delete-further-btn-text'   : 'Ta bort alla framtida',
        'delete-only-this-btn-text' : 'Ta bort endast denna',
        'update-title'              : 'Redigering av upprepad bokning',
        'update-all-message'        : 'Vill du ändra alla instanser av denna bokning?',
        'update-further-message'    : 'Vill du ändra på endast denna instans, eller denna och alla framtida?',
        'update-further-btn-text'   : 'Alla framtida',
        'update-only-this-btn-text' : 'Endast denna',
        Yes                         : 'Ja',
        Cancel                      : 'Avbryt',
        width                       : 500
    },

    RecurrenceLegend : {
        ' and '                         : ' och ',
        Daily                           : 'Daglig',
        'Weekly on {1}'                 : ({ days }) => `Veckovis på ${days}`,
        'Monthly on {1}'                : ({ days }) => `Måntaligen den ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Årligen ${days} ${months}`,
        'Every {0} days'                : ({ interval }) => `Var ${interval} dag`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Var ${interval} vecka på ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Var ${interval} månad ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Var ${interval} år på ${days} av ${months}`,
        position1                       : 'den första',
        position2                       : 'den andra',
        position3                       : 'den tredje',
        position4                       : 'den fjärde',
        position5                       : 'den femte',
        'position-1'                    : 'den sista',
        day                             : 'dagen',
        weekday                         : 'veckodagen',
        'weekend day'                   : 'dagen i veckoslut',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Upprepa bokning',
        Cancel              : 'Avbryt',
        Save                : 'Spara',
        Frequency           : 'Frekvens',
        Every               : 'Var',
        DAILYintervalUnit   : 'dag',
        WEEKLYintervalUnit  : 'vecka',
        MONTHLYintervalUnit : 'månad',
        YEARLYintervalUnit  : 'år',
        Each                : 'Varje',
        'On the'            : 'På den',
        'End repeat'        : 'Avsluta upprepning',
        'time(s)'           : 'upprepningar'
    },

    RecurrenceDaysCombo : {
        day           : 'dagen',
        weekday       : 'veckodagen',
        'weekend day' : 'dagen i veckoslutet'
    },

    RecurrencePositionsCombo : {
        position1    : 'första',
        position2    : 'andra',
        position3    : 'tredje',
        position4    : 'fjärde',
        position5    : 'femte',
        'position-1' : 'sista'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Aldrig',
        After     : 'Efter',
        'On date' : 'På datum'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Ingen upprepning',
        Daily   : 'Daglig',
        Weekly  : 'Veckovis',
        Monthly : 'Månatlig',
        Yearly  : 'Årlig'
    },

    RecurrenceCombo : {
        None   : 'Ingen',
        Custom : 'Anpassad...'
    },

    Summary : {
        'Summary for' : date => `Sammanfattning för ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Hela schemat',
        currentview  : 'Aktuell vy',
        daterange    : 'Datumintervall',
        completedata : 'Hela schemat (alla aktiviteter)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Tidsintervall',
        'Export from'    : 'Från',
        'Export to'      : 'Till'
    },

    ExcelExporter : {
        'No resource assigned' : 'Ingen resurs tilldelad'
    },

    CrudManagerView : {
        serverResponseLabel : 'Serversvar:'
    },

    DurationColumn : {
        Duration : 'Varaktighet'
    }
};

export default LocaleHelper.publishLocale(locale);
