import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Cs.js';

const locale = {

    localeName : 'Cs',
    localeDesc : 'Česky',
    localeCode : 'cs',

    Object : {
        newEvent : 'Nová událost'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' událost' + ({ 1 : '', 2 : 'i', 3 : 'i', 4 : 'i' }[data[data.length - 1]] || 'í')
    },

    Dependencies : {
        from    : 'Od',
        to      : 'Do',
        valid   : 'Platný',
        invalid : 'Neplatný'
    },

    DependencyType : {
        SS           : 'ZZ',
        SF           : 'ZK',
        FS           : 'KZ',
        FF           : 'KK',
        StartToStart : 'Začátek-Začátek',
        StartToEnd   : 'Začátek-Konec',
        EndToStart   : 'Konec-Začátek',
        EndToEnd     : 'Konec-Konec',
        short        : [
            'ZZ',
            'ZK',
            'KZ',
            'KK'
        ],
        long : [
            'Začátek-Začátek',
            'Začátek-Konec',
            'Konec-Začátek',
            'Konec-Konec'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Typ',
        Lag               : 'Prodleva',
        'Edit dependency' : 'Upravit závislost',
        Save              : 'Uložit',
        Delete            : 'Smazat',
        Cancel            : 'Zrušit',
        StartToStart      : 'Začátek-Začátek',
        StartToEnd        : 'Začátek-Konec',
        EndToStart        : 'Konec-Začátek',
        EndToEnd          : 'Konec-Konec'
    },

    EventEdit : {
        Name         : 'Název',
        Resource     : 'Zdroj',
        Start        : 'Začátek',
        End          : 'Konec',
        Save         : 'Uložit',
        Delete       : 'Smazat',
        Cancel       : 'Zrušit',
        'Edit event' : 'Upravit událost',
        Repeat       : 'Opakovat'
    },

    EventDrag : {
        eventOverlapsExisting : 'Překrytí stávajících událostí pro tento zdroj',
        noDropOutsideTimeline : 'Událost nelze přesunout zcela mimo časovou osu'
    },

    SchedulerBase : {
        'Add event'      : 'Přidat událost',
        'Delete event'   : 'Vymazat událost',
        'Unassign event' : 'Zrušit přidělení události',
        color            : 'Barva'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Rozsah dat',
        startText       : 'Datum začátku',
        endText         : 'Datum konce',
        todayText       : 'Dnes'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopírovat událost',
        cutEvent   : 'Vyjmout událost',
        pasteEvent : 'Vložit událost'
    },

    EventFilter : {
        filterEvents : 'Filtrovat úkoly',
        byName       : 'Podle názvu'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Zobrazit aktuální časovou osu'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekundy'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd D. M., H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd D. M.',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Den'
        },
        day : {
            name : 'Den/hodiny'
        },
        week : {
            name : 'Týden/hodiny'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Týden/dny'
        },
        dayAndMonth : {
            name : 'Měsíc'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Týden'
        },
        weekAndMonth : {
            name : 'Týdny'
        },
        weekAndDayLetter : {
            name : 'Týdny/pracovní dny'
        },
        weekDateAndMonth : {
            name : 'Měsíce/týdny'
        },
        monthAndYear : {
            name : 'Měsíce'
        },
        year : {
            name : 'Roky'
        },
        manyYears : {
            name : 'Několik let'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Mažete událost',
        'delete-all-message'        : 'Chcete vymazat všechny výskyty této události?',
        'delete-further-message'    : 'Chcete vymazat tuto a všechny budoucí výskyty této události nebo pouze vybraný výskyt?',
        'delete-further-btn-text'   : 'Vymazat všechny budoucí události',
        'delete-only-this-btn-text' : 'Vymazat jen tuto událost',
        'update-title'              : 'Měníte opakující se událost',
        'update-all-message'        : 'Chcete změnit všechny výskyty této události?',
        'update-further-message'    : 'Chcete změnit pouze tento výskyt události nebo tento a všechny budoucí výskyty?',
        'update-further-btn-text'   : 'Všechny budoucí události',
        'update-only-this-btn-text' : 'Pouze tuto událost',
        Yes                         : 'Ano',
        Cancel                      : 'Zrušit',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' a ',
        Daily                           : 'Denně',
        'Weekly on {1}'                 : ({ days }) => `Týdně v ${days}`,
        'Monthly on {1}'                : ({ days }) => `Měsíčně v ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Ročně dne  ${days}. ${months}.`,
        'Every {0} days'                : ({ interval }) => `Každých ${interval} dnů`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Každých ${interval} týdn v ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Každé ${interval} měsíce v ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Každé ${interval} roky v ${days} ${months}`,
        position1                       : 'první',
        position2                       : 'druhý',
        position3                       : 'třetí',
        position4                       : 'čtvrtý',
        position5                       : 'pátý',
        'position-1'                    : 'poslední',
        day                             : 'den',
        weekday                         : 'pracovní den',
        'weekend day'                   : 'víkendový den',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Opakovat událost',
        Cancel              : 'Zrušit',
        Save                : 'Uložit',
        Frequency           : 'Frekvence',
        Every               : 'Každý',
        DAILYintervalUnit   : 'den (dny) ',
        WEEKLYintervalUnit  : 'týden (týdny)',
        MONTHLYintervalUnit : 'měsíc (měsíce) ',
        YEARLYintervalUnit  : 'rok (roky)',
        Each                : 'Každý',
        'On the'            : 'Dne',
        'End repeat'        : 'Ukončit opakování',
        'time(s)'           : 'krát'
    },

    RecurrenceDaysCombo : {
        day           : 'den',
        weekday       : 'pracovní den',
        'weekend day' : 'víkendový den'
    },

    RecurrencePositionsCombo : {
        position1    : 'první',
        position2    : 'druhý',
        position3    : 'třetí',
        position4    : 'čtvrtý',
        position5    : 'pátý',
        'position-1' : 'poslední'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nikdy',
        After     : 'Po',
        'On date' : 'Dne'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Bez opakování',
        Daily   : 'Denně',
        Weekly  : 'Týdně',
        Monthly : 'Měsíčně',
        Yearly  : 'Ročně'
    },

    RecurrenceCombo : {
        None   : 'Žádný',
        Custom : 'Vlastní...'
    },

    Summary : {
        'Summary for' : date => `Shrnutí pro ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Celý harmonogram',
        currentview  : 'Viditelný harmonogram',
        daterange    : 'Rozsah dat',
        completedata : 'Dokončit harmonogram (pro všechny události)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Rozsah harmonogramu',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Žádný přiřazený zdroj'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odezva serveru:'
    },

    DurationColumn : {
        Duration : 'Doba trvání'
    }
};

export default LocaleHelper.publishLocale(locale);
