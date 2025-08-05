import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Sr.js';

const locale = {

    localeName : 'Sr',
    localeDesc : 'Srpski',
    localeCode : 'sr',

    Object : {
        newEvent : 'Novi dogđaj'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' dogđaj' + (data !== 1 ? 'događaji' : 'i')
    },

    Dependencies : {
        from    : 'Od',
        to      : 'Do',
        valid   : 'Ispravan',
        invalid : 'Neispravan'
    },

    DependencyType : {
        SS           : 'PP',
        SF           : 'PK',
        FS           : 'KP',
        FF           : 'KK',
        StartToStart : 'Od početka do početka',
        StartToEnd   : 'Od početka do kraja',
        EndToStart   : 'Od kraja do početka',
        EndToEnd     : 'Od kraja do kraja',
        short        : [
            'PP',
            'PK',
            'KP',
            'KK'
        ],
        long : [
            'Od početka do početka',
            'Od početka do kraja',
            'Od kraja do početka',
            'Od kraja do kraja'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Tip',
        Lag               : 'Kašnjenje',
        'Edit dependency' : 'Uredi zavisnost',
        Save              : 'Sačuvaj',
        Delete            : 'Obriši',
        Cancel            : 'Otkaži',
        StartToStart      : 'Od početka do početka',
        StartToEnd        : 'Od početka do kraja',
        EndToStart        : 'Od kraja do početka',
        EndToEnd          : 'Od kraja do kraja'
    },

    EventEdit : {
        Name         : 'Ime',
        Resource     : 'Resurs',
        Start        : 'Početak',
        End          : 'Kraj',
        Save         : 'Sačuvaj',
        Delete       : 'Obriši',
        Cancel       : 'Otkaži',
        'Edit event' : 'Uredi događaj',
        Repeat       : 'Ponovi'
    },

    EventDrag : {
        eventOverlapsExisting : 'Događaj preklapa postojeći događaj za ovaj resurs',
        noDropOutsideTimeline : 'Događaj možda nije u potpunosti spušten izvan vremenske linije'
    },

    SchedulerBase : {
        'Add event'      : 'Dodaj događaj',
        'Delete event'   : 'Obriši događaj',
        'Unassign event' : 'Poništi dodelu događaja',
        color            : 'Boja'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zumiranje',
        activeDateRange : 'Opseg datuma',
        startText       : 'Početni datum',
        endText         : 'Krajnji datum',
        todayText       : 'Danas'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiraj događaj',
        cutEvent   : 'Iseci događaj',
        pasteEvent : 'Umetni događaj'
    },

    EventFilter : {
        filterEvents : 'Filtriraj zadatke',
        byName       : 'Po imenu'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Prikaži trenutnu vremensku liniju'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekundi'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd D.M., hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd D.M.',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dan'
        },
        day : {
            name : 'Dan/sati'
        },
        week : {
            name : 'Nedelja/sati'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Nedelja/dana'
        },
        dayAndMonth : {
            name : 'Mesec'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Nedelja'
        },
        weekAndMonth : {
            name : 'Nedelja'
        },
        weekAndDayLetter : {
            name : 'Nedelja/radnih dana'
        },
        weekDateAndMonth : {
            name : 'Meseci/nedelja'
        },
        monthAndYear : {
            name : 'Meseci'
        },
        year : {
            name : 'Godina'
        },
        manyYears : {
            name : 'Više godina'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Brisanje događaja',
        'delete-all-message'        : 'Da li želišda obrišeš sva pojavljivanja ovog događaja?',
        'delete-further-message'    : 'Da li želiš da obrišeš ovaj i sva sledeća pojavljivanja ovog događaja, ili samo odabrano pojavljivanje?',
        'delete-further-btn-text'   : 'Obriši sve buduće događaje',
        'delete-only-this-btn-text' : 'Obriši samo ovaj događaj',
        'update-title'              : 'Izmena događaja koji se ponavlja',
        'update-all-message'        : 'Da li želiš da promeniš sva pojavljivanja ovog događaja?',
        'update-further-message'    : 'Da li želiš da promeniš samo ovo pojavljivanje događaja, ili ovo i svako buduće pojavljivanje? ',
        'update-further-btn-text'   : 'Sve buduće događaje',
        'update-only-this-btn-text' : 'Samo ovaj događaj',
        Yes                         : 'Da',
        Cancel                      : 'Otkaži',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' i ',
        Daily                           : 'Dnevno',
        'Weekly on {1}'                 : ({ days }) => `Svake nedelje u ${days}`,
        'Monthly on {1}'                : ({ days }) => `Svakog meseca u ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Svake godine u ${days} u ${months}`,
        'Every {0} days'                : ({ interval }) => `Svakih ${interval} dana`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Svakih ${interval} nedelja u ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Svakih ${interval} meseci u ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Svakih ${interval} godina na ${days} u ${months}`,
        position1                       : 'prvi',
        position2                       : 'drugi',
        position3                       : 'treći',
        position4                       : 'četvrti',
        position5                       : 'peti',
        'position-1'                    : 'poslednji',
        day                             : 'dan',
        weekday                         : 'radni dan',
        'weekend day'                   : 'dan vikenda',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ponovi događaj',
        Cancel              : 'Otkaži',
        Save                : 'Sačuvaj',
        Frequency           : 'Frekvencija',
        Every               : 'Svaki(h)',
        DAILYintervalUnit   : 'dan(a)',
        WEEKLYintervalUnit  : 'nedelje(e)',
        MONTHLYintervalUnit : 'mesec(a)',
        YEARLYintervalUnit  : 'godina(e)',
        Each                : 'Svake',
        'On the'            : 'Na',
        'End repeat'        : 'Kraj ponavljanja',
        'time(s)'           : 'put(a)'
    },

    RecurrenceDaysCombo : {
        day           : 'dan',
        weekday       : 'radni dan',
        'weekend day' : 'dan vikenda'
    },

    RecurrencePositionsCombo : {
        position1    : 'prvi',
        position2    : 'drugi',
        position3    : 'treći',
        position4    : 'četvrti',
        position5    : 'peti',
        'position-1' : 'poslednji'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nikad',
        After     : 'Nakon',
        'On date' : 'Na dan'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Bez ponavljanja',
        Daily   : 'Dnevno',
        Weekly  : 'Nedeljno',
        Monthly : 'Mesečno',
        Yearly  : 'Godišnje'
    },

    RecurrenceCombo : {
        None   : 'Nema',
        Custom : 'Prilagođeno…'
    },

    Summary : {
        'Summary for' : date => `Pregled na ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Kompletan raspored',
        currentview  : 'Vidljiv raspored',
        daterange    : 'Opseg datuma',
        completedata : 'Kompletan raspored (za sve događaje)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Opseg rasporeda',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nema pridruženih resursa'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odgovor servera:'
    },

    DurationColumn : {
        Duration : 'Trajanje'
    }
};

export default LocaleHelper.publishLocale(locale);
