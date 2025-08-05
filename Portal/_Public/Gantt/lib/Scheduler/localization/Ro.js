import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Ro.js';

const locale = {

    localeName : 'Ro',
    localeDesc : 'Română',
    localeCode : 'ro',

    Object : {
        newEvent : 'Eveniment nou'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' eveniment' + (data !== 1 ? 'e' : '')
    },

    Dependencies : {
        from    : 'De la',
        to      : 'Până la',
        valid   : 'Valid',
        invalid : 'Nevalid'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'Start-la-start',
        StartToEnd   : 'Start-la-finalizare',
        EndToStart   : 'Finalizare-la-start',
        EndToEnd     : 'Finalizare-la-finalizare',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'Start-la-start',
            'Start-la-finalizare',
            'Finalizare-la-start',
            'Finalizare-la-finalizare'
        ]
    },

    DependencyEdit : {
        From              : 'De la',
        To                : 'Până la',
        Type              : 'Tip',
        Lag               : 'Întârziere',
        'Edit dependency' : 'Editare dependență',
        Save              : 'Salvare',
        Delete            : 'Ștergere',
        Cancel            : 'Anulare',
        StartToStart      : 'Start la start',
        StartToEnd        : 'Start la finalizare',
        EndToStart        : 'Finalizare la start',
        EndToEnd          : 'Finalizare la finalizare'
    },

    EventEdit : {
        Name         : 'Nume',
        Resource     : 'Resursă',
        Start        : 'Start',
        End          : 'Finalizare',
        Save         : 'Salvare',
        Delete       : 'Ștergere',
        Cancel       : 'Anulare',
        'Edit event' : 'Editare eveniment',
        Repeat       : 'Repetare'
    },

    EventDrag : {
        eventOverlapsExisting : 'Evenimentul se suprapune cu cel existent pentru această resursă',
        noDropOutsideTimeline : 'Evenimentul nu poate fi lăsat complet în afara cronologiei'
    },

    SchedulerBase : {
        'Add event'      : 'Adăugare eveniment',
        'Delete event'   : 'Ștergere eveniment',
        'Unassign event' : 'Anulare alocare eveniment',
        color            : 'Culoare'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Interval dată',
        startText       : 'Data începerii',
        endText         : 'Data finalizării',
        todayText       : 'Azi'
    },

    EventCopyPaste : {
        copyEvent  : 'Copiere eveniment',
        cutEvent   : 'Decupare eveniment',
        pasteEvent : 'Lipire eveniment'
    },

    EventFilter : {
        filterEvents : 'Filtrare sarcini',
        byName       : 'În funcție de nume'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Afișare cronologie curentă'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Secunde'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Zi'
        },
        day : {
            name : 'Zi/ore'
        },
        week : {
            name : 'Săptămână/ore'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Săptămână/zile'
        },
        dayAndMonth : {
            name : 'Lună'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Săptămână'
        },
        weekAndMonth : {
            name : 'Săptămâni'
        },
        weekAndDayLetter : {
            name : 'Săptămâni/zile din săptămână'
        },
        weekDateAndMonth : {
            name : 'Luni/săptămâni'
        },
        monthAndYear : {
            name : 'Luni'
        },
        year : {
            name : 'Ani'
        },
        manyYears : {
            name : 'Ani multipli'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Ștergeți un eveniment',
        'delete-all-message'        : 'Doriți să ștergeți toate aparițiile acestui eveniment?',
        'delete-further-message'    : 'Doriți să ștergeți evenimentul împreună cu toate aparițiile sale, sau doar apariția selectată?',
        'delete-further-btn-text'   : 'Ștergeți toate evenimentele viitoare',
        'delete-only-this-btn-text' : 'Ștergeți numai acest eveniment',
        'update-title'              : 'Modificați un eveniment repetitiv',
        'update-all-message'        : 'Doriți să modificați toate aparițiile acestui eveniment?',
        'update-further-message'    : 'Doriți să modificați doar această apariție a evenimentului sau pe aceasta și toate aparițiile viitoare?',
        'update-further-btn-text'   : 'Toate evenimentele viitoare',
        'update-only-this-btn-text' : 'Numai acest eveniment',
        Yes                         : 'Da',
        Cancel                      : 'Anulare',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' și ',
        Daily                           : 'Zilnic',
        'Weekly on {1}'                 : ({ days }) => `Săptămânal, în zilele de ${days}`,
        'Monthly on {1}'                : ({ days }) => `Lunar, pe ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Anual, pe ${days} of ${months}`,
        'Every {0} days'                : ({ interval }) => `La fiecare ${interval} zile`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `La fiecare ${interval} săptămâni, în zilele de ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `La fiecare ${interval} luni pe ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `La fiecare ${interval} ani, pe ${days} din ${months}`,
        position1                       : 'prima',
        position2                       : 'a doua',
        position3                       : 'a treia',
        position4                       : 'a patra',
        position5                       : 'a cincea',
        'position-1'                    : 'ultima',
        day                             : 'zi',
        weekday                         : 'zi lucrătoare',
        'weekend day'                   : 'zi de weekend',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Repetare eveniment',
        Cancel              : 'Anulare',
        Save                : 'Salvare',
        Frequency           : 'Frecvență',
        Every               : 'În fiecare',
        DAILYintervalUnit   : 'zi(le)',
        WEEKLYintervalUnit  : 'săptămână(i)',
        MONTHLYintervalUnit : 'lună(i)',
        YEARLYintervalUnit  : 'an(i)',
        Each                : 'Fiecare',
        'On the'            : 'Pe',
        'End repeat'        : 'Finalizare repetare',
        'time(s)'           : 'ora(e)'
    },

    RecurrenceDaysCombo : {
        day           : 'zi',
        weekday       : 'zi lucrătoare',
        'weekend day' : 'zi de weekend'
    },

    RecurrencePositionsCombo : {
        position1    : 'prima',
        position2    : 'a doua',
        position3    : 'a treia',
        position4    : 'a patra',
        position5    : 'a cincea',
        'position-1' : 'ultima'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Niciodată',
        After     : 'După',
        'On date' : 'La data'
    },

    RecurrenceFrequencyCombo : {
        None    : 'A nu se repeta',
        Daily   : 'Zilnic',
        Weekly  : 'Săptămânal',
        Monthly : 'Lunar',
        Yearly  : 'Anual'
    },

    RecurrenceCombo : {
        None   : 'Niciunul',
        Custom : 'Personalizat..'
    },

    Summary : {
        'Summary for' : date => `Sumar pentru ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Program complet',
        currentview  : 'Program vizibil',
        daterange    : 'Interval de date',
        completedata : 'Program complet (pentru toate evenimentele)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Programare interval',
        'Export from'    : 'De la',
        'Export to'      : 'Până la'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nicio resursă alocată'
    },

    CrudManagerView : {
        serverResponseLabel : 'Răspuns server:'
    },

    DurationColumn : {
        Duration : 'Durată'
    }
};

export default LocaleHelper.publishLocale(locale);
