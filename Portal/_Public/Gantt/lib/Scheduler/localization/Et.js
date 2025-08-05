import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Et.js';

const locale = {

    localeName : 'Et',
    localeDesc : 'Eesti keel',
    localeCode : 'et',

    Object : {
        newEvent : 'Uus sündmus'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' sündmus' + (data !== 1 ? 'ed' : '')
    },

    Dependencies : {
        from    : 'Alates',
        to      : 'Kuni',
        valid   : 'Kehtiv',
        invalid : 'Kehtetu'
    },

    DependencyType : {
        SS           : 'AA',
        SF           : 'AL',
        FS           : 'LA',
        FF           : 'LL',
        StartToStart : 'Algusest alguseni',
        StartToEnd   : 'Algusest lõpuni',
        EndToStart   : 'Lõpust alguseni',
        EndToEnd     : 'Lõpust lõpuni',
        short        : [
            'AA',
            'AL',
            'LA',
            'LL'
        ],
        long : [
            'Algusest alguseni',
            'Algusest lõpuni',
            'Lõpust alguseni',
            'Lõpust lõpuni'
        ]
    },

    DependencyEdit : {
        From              : 'Alates',
        To                : 'Kuni',
        Type              : 'Tüüp',
        Lag               : 'Kiht',
        'Edit dependency' : 'Redigeeri sõltuvust',
        Save              : 'Salvesta',
        Delete            : 'Kustuta',
        Cancel            : 'Tühista',
        StartToStart      : 'Algusest alguseni',
        StartToEnd        : 'Algusest lõpuni',
        EndToStart        : 'Lõpust alguseni',
        EndToEnd          : 'Lõpust lõpuni'
    },

    EventEdit : {
        Name         : 'Nimi',
        Resource     : 'Ressurss',
        Start        : 'Algus',
        End          : 'Lõpp',
        Save         : 'Salvesta',
        Delete       : 'Kustuta',
        Cancel       : 'Tühista',
        'Edit event' : 'Redigeeri sündmust',
        Repeat       : 'Korda'
    },

    EventDrag : {
        eventOverlapsExisting : 'Sündmus kattub selle ressursi puhul olemasoleva sündmusega',
        noDropOutsideTimeline : 'Sündmust ei või jätta täielikult väljapoole ajajoont'
    },

    SchedulerBase : {
        'Add event'      : 'Lisa sündmus',
        'Delete event'   : 'Kustuta sündmus',
        'Unassign event' : 'Eemalda sündmuse määramine',
        color            : 'Värv'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Suumi',
        activeDateRange : 'Kuupäevavahemik',
        startText       : 'Alguskuupäev',
        endText         : 'Lõppkuupäev',
        todayText       : 'Täna'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopeeri sündmus',
        cutEvent   : 'Lõika sündmus',
        pasteEvent : 'Kleebi sündmus'
    },

    EventFilter : {
        filterEvents : 'Filtreeri ülesanded',
        byName       : 'Nime järgi'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Näita praegust ajajoont'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekundit'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Päev'
        },
        day : {
            name : 'Päev/tund'
        },
        week : {
            name : 'Nädal/tund'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Nädal/päev'
        },
        dayAndMonth : {
            name : 'Kuu'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Nädal'
        },
        weekAndMonth : {
            name : 'Nädalat'
        },
        weekAndDayLetter : {
            name : 'Nädal/nädalapäev'
        },
        weekDateAndMonth : {
            name : 'Kuud/nädalad'
        },
        monthAndYear : {
            name : 'Kuud'
        },
        year : {
            name : 'Aastad'
        },
        manyYears : {
            name : 'Mitu aastat'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Oled sündmust kustutamas',
        'delete-all-message'        : 'Kas soovid kustutada kõik selle sündmuse esinemised?',
        'delete-further-message'    : 'Kas soovid kustutada selle ja kõik selle sündmuse tulevased esinemised või ainult valitud esinemised?',
        'delete-further-btn-text'   : 'Kustuta kõik tulevased sündmused',
        'delete-only-this-btn-text' : 'Kustuta ainult see sündmus',
        'update-title'              : 'Muudad korduvat sündmust',
        'update-all-message'        : 'Kas soovid muuta kõiki selle sündmuse esinemisi?',
        'update-further-message'    : 'Kas soovid muuta vaid seda sündmuse esinemist või seda ja kõiki tulevasi esinemisi?',
        'update-further-btn-text'   : 'Kõik tulevased sündmused',
        'update-only-this-btn-text' : 'Ainult see sündmus',
        Yes                         : 'Jah',
        Cancel                      : 'Tühista',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' ja ',
        Daily                           : 'Igapäevane',
        'Weekly on {1}'                 : ({ days }) => `Igapäevane ajal ${days}`,
        'Monthly on {1}'                : ({ days }) => `Igakuine ajal ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Iga-aastane ajal  ${days}  ${months} kuust`,
        'Every {0} days'                : ({ interval }) => `Iga ${interval} päeva tagant`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Iga ${interval} nädala tagant ajal ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Iga ${interval} kuu tagant ajal ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Iga ${interval} aasta tagant ${days}  ${months} kuust`,
        position1                       : 'esimene',
        position2                       : 'teine',
        position3                       : 'kolmas',
        position4                       : 'neljas',
        position5                       : 'viies',
        'position-1'                    : 'viimane',
        day                             : 'päev',
        weekday                         : 'tööpäev',
        'weekend day'                   : 'nädalavahetus',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Korda sündmust',
        Cancel              : 'Tühista',
        Save                : 'Salvesta',
        Frequency           : 'Sagedus',
        Every               : 'Iga',
        DAILYintervalUnit   : 'päev(a) tagant',
        WEEKLYintervalUnit  : 'nädal(a) tagant',
        MONTHLYintervalUnit : 'kuu tagant',
        YEARLYintervalUnit  : 'aasta tagant',
        Each                : 'Iga',
        'On the'            : 'Ajal ',
        'End repeat'        : 'Lõpeta kordumine',
        'time(s)'           : 'aeg (ajad)'
    },

    RecurrenceDaysCombo : {
        day           : 'päev',
        weekday       : 'tööpäev',
        'weekend day' : 'nädalavahetus'
    },

    RecurrencePositionsCombo : {
        position1    : 'esimene',
        position2    : 'teine',
        position3    : 'kolmas',
        position4    : 'neljas',
        position5    : 'viies',
        'position-1' : 'viimane'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Mitte kunagi',
        After     : 'Pärast',
        'On date' : 'Kuupäeval'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Ära korda',
        Daily   : 'Igapäevane',
        Weekly  : 'Iganädalane',
        Monthly : 'Igakuine',
        Yearly  : 'Iga-aastane'
    },

    RecurrenceCombo : {
        None   : 'Mitte ükski',
        Custom : 'Kohandatud...'
    },

    Summary : {
        'Summary for' : date => `Kokkuvõte kuupäeval ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Lõpeta graafik',
        currentview  : 'Nähtav graafik',
        daterange    : 'Kuupäevavahemik',
        completedata : 'Lõpeta graafik (kõigi sündmuste jaoks)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Kuupäevavahemik',
        'Export from'    : 'Alates',
        'Export to'      : 'Kuni'
    },

    ExcelExporter : {
        'No resource assigned' : 'Ressursse pole määratud'
    },

    CrudManagerView : {
        serverResponseLabel : 'Serveri vastus:'
    },

    DurationColumn : {
        Duration : 'Kestus'
    }
};

export default LocaleHelper.publishLocale(locale);
