import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Sk.js';

const locale = {

    localeName : 'Sk',
    localeDesc : 'Slovenský',
    localeCode : 'sk',

    Object : {
        newEvent : 'Nová udalosť'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' udalos' + (data !== 1 ? 'ti' : 'ť')
    },

    Dependencies : {
        from    : 'From',
        to      : 'To',
        valid   : 'Platný',
        invalid : 'Neplatný'
    },

    DependencyType : {
        SS           : 'ZZ',
        SF           : 'ZK',
        FS           : 'KZ',
        FF           : 'KK',
        StartToStart : 'Od začiatku po začiatok',
        StartToEnd   : 'Od začiatku po koniec',
        EndToStart   : 'Od konca po začiatok',
        EndToEnd     : 'Od konca po koniec',
        short        : [
            'ZZ',
            'ZK',
            'KZ',
            'KK'
        ],
        long : [
            'Od začiatku po začiatok',
            'Od začiatku po koniec',
            'Od konca po začiatok',
            'Od konca po koniec'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Typ',
        Lag               : 'Lag',
        'Edit dependency' : 'Upraviť súvislosť',
        Save              : 'Uložiť',
        Delete            : 'Vymazať',
        Cancel            : 'Zrušiť',
        StartToStart      : 'Od začiatku po začiatok',
        StartToEnd        : 'Od začiatku po koniec',
        EndToStart        : 'Od konca po začiatok',
        EndToEnd          : 'Od konca po koniec'
    },

    EventEdit : {
        Name         : 'Názov',
        Resource     : 'Zdroj',
        Start        : 'Start',
        End          : 'End',
        Save         : 'Uložiť',
        Delete       : 'Vymazať',
        Cancel       : 'Zrušiť',
        'Edit event' : 'Upraviť udalosť',
        Repeat       : 'Opakovať'
    },

    EventDrag : {
        eventOverlapsExisting : 'Udalosť presahuje existujúcu udalosť pre tento zdroj',
        noDropOutsideTimeline : 'Udalosť nemusí byť úplne klesnutá mimo časového rámca'
    },

    SchedulerBase : {
        'Add event'      : 'Pridať udalosť',
        'Delete event'   : 'Vymazať udalosť',
        'Unassign event' : 'Zrušiť pridelenie udalosti',
        color            : 'Farba'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Priblížiť',
        activeDateRange : 'Rozsah dátumu',
        startText       : 'Dátum začiatku',
        endText         : 'Dátum ukončenia',
        todayText       : 'Dnes'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopírovať udalosť',
        cutEvent   : 'Odstrániť udalosť',
        pasteEvent : 'Vložiť udalosť'
    },

    EventFilter : {
        filterEvents : 'Filtrovať úlohy',
        byName       : 'Podľa názvu'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Ukázať súčasnú časovú líniu'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekundy'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD. MM., H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD. MM.',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Deň'
        },
        day : {
            name : 'Deň/hodiny'
        },
        week : {
            name : 'Týždeň/hodiny'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Týždeň/dni'
        },
        dayAndMonth : {
            name : 'Month'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Týždeň'
        },
        weekAndMonth : {
            name : 'Týždne'
        },
        weekAndDayLetter : {
            name : 'Týždne/dni v týždni'
        },
        weekDateAndMonth : {
            name : 'Mesiace/týždne'
        },
        monthAndYear : {
            name : 'Mesiace'
        },
        year : {
            name : 'Roky'
        },
        manyYears : {
            name : 'Viac rokov'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Vymazávate udalosť',
        'delete-all-message'        : 'Chcete vymazať všetky výskyty tejto udalosti?',
        'delete-further-message'    : 'Chcete vymazať tento a všetky budúce výskyty tejto udalosti alebo len zvolený výskyt?',
        'delete-further-btn-text'   : 'Vymazať všetky budúce udalosti',
        'delete-only-this-btn-text' : 'Odstrániť iba túto udalosť',
        'update-title'              : 'Meníte opakujúcu sa udalosť',
        'update-all-message'        : 'Chcete zmeniť všetky výskyty tejto udalosti?',
        'update-further-message'    : 'Chcete zmeniť len tento výskyt udalosti alebo tento a všetky budúce výskyty?',
        'update-further-btn-text'   : 'Všetky budúce udalosti',
        'update-only-this-btn-text' : 'Len túto udalosť',
        Yes                         : 'Áno',
        Cancel                      : 'Zrušiť',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' a ',
        Daily                           : 'Denne',
        'Weekly on {1}'                 : ({ days }) => `Týždenne v ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mesačne v ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Ročne v${days} of ${months}`,
        'Every {0} days'                : ({ interval }) => `Každých ${interval} days`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Každých ${interval} týždňov v ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Každých ${interval} mesiacov v ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Každých ${interval} rokov v ${days} z ${months}`,
        position1                       : 'the prvý',
        position2                       : 'druhý',
        position3                       : 'tretí',
        position4                       : 'štvrtý',
        position5                       : 'piaty',
        'position-1'                    : 'ten last',
        day                             : 'deň',
        weekday                         : 'pracovný deň',
        'weekend day'                   : 'deň víkendu',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Opakovať udalosť',
        Cancel              : 'Zrušiť',
        Save                : 'Uložiť',
        Frequency           : 'Frekvencia',
        Every               : 'Každý',
        DAILYintervalUnit   : 'day(ni)',
        WEEKLYintervalUnit  : 'týždeň(ne)',
        MONTHLYintervalUnit : 'mesiac(e)',
        YEARLYintervalUnit  : 'rok(y)',
        Each                : 'Každý',
        'On the'            : 'V the',
        'End repeat'        : 'Ukončiť opakovanie',
        'time(s)'           : 'čas(s)'
    },

    RecurrenceDaysCombo : {
        day           : 'deň',
        weekday       : 'pracovný deň',
        'weekend day' : 'deň víkendu'
    },

    RecurrencePositionsCombo : {
        position1    : 'prvý',
        position2    : 'druhý',
        position3    : 'tretí',
        position4    : 'štvrtý',
        position5    : 'piaty',
        'position-1' : 'posledný'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nikdy',
        After     : 'Po',
        'On date' : 'V dátum'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Bez opakovania',
        Daily   : 'Denne',
        Weekly  : 'Týždenne',
        Monthly : 'Mesačne',
        Yearly  : 'Ročne'
    },

    RecurrenceCombo : {
        None   : 'Žiadny',
        Custom : 'Vlastný...'
    },

    Summary : {
        'Summary for' : date => `Súhrn pre ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Complete rozvrh',
        currentview  : 'Viditeľný rozvrh',
        daterange    : 'Rozsah dátumov',
        completedata : 'Dokončiť rozvrh (pre všetky udalosti)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Rozsah rozvrhu',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nepridelený žiadny zdroj'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odpoveď servera:'
    },

    DurationColumn : {
        Duration : 'Trvanie'
    }
};

export default LocaleHelper.publishLocale(locale);
