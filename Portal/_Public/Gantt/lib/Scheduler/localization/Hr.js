import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Hr.js';

const locale = {

    localeName : 'Hr',
    localeDesc : 'Hrvatski',
    localeCode : 'hr',

    Object : {
        newEvent : 'Novi događaj'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' događa' + (data !== 1 ? 'i' : '')
    },

    Dependencies : {
        from    : 'Od',
        to      : 'Do',
        valid   : 'Važeće',
        invalid : 'Nevažeće'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'Od početka na početak',
        StartToEnd   : 'Od početka na završetak',
        EndToStart   : 'Od završetka na početak',
        EndToEnd     : 'Od završetka na završetak',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'Od početka na početak',
            'Od početka na završetak',
            'Od završetka na početak',
            'Od završetka na završetak'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Vrsta',
        Lag               : 'Odgoda',
        'Edit dependency' : 'Uredi ovisnost',
        Save              : 'Spremi',
        Delete            : 'Obriši',
        Cancel            : 'Otkaži',
        StartToStart      : 'Od početka na početak',
        StartToEnd        : 'Od početka na završetak',
        EndToStart        : 'Od završetka na početak',
        EndToEnd          : 'Od završetka na završetak'
    },

    EventEdit : {
        Name         : 'Naziv',
        Resource     : 'Resurs',
        Start        : 'Početak',
        End          : 'Završetak',
        Save         : 'Spremi',
        Delete       : 'Obriši',
        Cancel       : 'Otkaži',
        'Edit event' : 'Uredi događaj',
        Repeat       : 'Ponovi'
    },

    EventDrag : {
        eventOverlapsExisting : 'Događaj se poklapa s već postojećim događajem za ovaj resurs',
        noDropOutsideTimeline : 'Događaj se ne smije potpuno ispustiti izvan vremenske trake'
    },

    SchedulerBase : {
        'Add event'      : 'Dodaj događaj',
        'Delete event'   : 'Obriši događaj',
        'Unassign event' : 'Poništi dodjelu događaja',
        color            : 'Boja'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Povećaj ili smanji',
        activeDateRange : 'Raspon datuma',
        startText       : 'Datum početka',
        endText         : 'Datum završetka',
        todayText       : 'Danas'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiraj događaj',
        cutEvent   : 'Izreži događaj',
        pasteEvent : 'Zalijepi događaj'
    },

    EventFilter : {
        filterEvents : 'Filtriraj zadatke',
        byName       : 'Po nazivu'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Pokaži trenutačnu vremensku traku'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekunde'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM., H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM.',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dani'
        },
        day : {
            name : 'Dan/sati'
        },
        week : {
            name : 'Tjedan/sati'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Tjedan/dani'
        },
        dayAndMonth : {
            name : 'Mjesec'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Tjedan'
        },
        weekAndMonth : {
            name : 'Tjedni'
        },
        weekAndDayLetter : {
            name : 'Tjedni/Dani u tjednu'
        },
        weekDateAndMonth : {
            name : 'Mjeseci/Tjedni'
        },
        monthAndYear : {
            name : 'Mjeseci'
        },
        year : {
            name : 'Godine'
        },
        manyYears : {
            name : 'Više godina'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Brišete događaj',
        'delete-all-message'        : 'Želite li obrisati sva pojavljivanja ovog događaja?',
        'delete-further-message'    : 'Želite li obrisati ovo i sva buduća pojavljivanja ovog događaja ili samo odabrano pojavljivanje?',
        'delete-further-btn-text'   : 'Obriši sve događaje u budućnosti',
        'delete-only-this-btn-text' : 'Izbriši samo ovaj događaj',
        'update-title'              : 'Mijenjate ponavljajući događaj',
        'update-all-message'        : 'Želite li izmijeniti sva pojavljivanja ovog događaja?',
        'update-further-message'    : 'Želite li izmijeniti samo ovo pojavljivanje događaja ili ovo i sva buduća pojavljivanja?',
        'update-further-btn-text'   : 'Svi budući događaji',
        'update-only-this-btn-text' : 'Samo ovaj događaj',
        Yes                         : 'Da',
        Cancel                      : 'Otkaži',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' i ',
        Daily                           : 'Svakodnevno',
        'Weekly on {1}'                 : ({ days }) => `Tjedno ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mjesečno ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Godišnje ${days} od ${months}`,
        'Every {0} days'                : ({ interval }) => `Svakih ${interval} dana`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Svaka ${interval} tjedna ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Svaka ${interval} mjeseca ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Svake ${interval} godine ${days} od ${months}`,
        position1                       : 'prvi',
        position2                       : 'drugi',
        position3                       : 'treći',
        position4                       : 'četvrti',
        position5                       : 'peti',
        'position-1'                    : 'posljednji',
        day                             : 'dan',
        weekday                         : 'radni dan',
        'weekend day'                   : 'dan vikenda',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ponovi događaj',
        Cancel              : 'Otkaži',
        Save                : 'Spremi',
        Frequency           : 'Učestalost',
        Every               : 'Svaki(h)',
        DAILYintervalUnit   : 'dan(a)',
        WEEKLYintervalUnit  : 'tjedan(a)',
        MONTHLYintervalUnit : 'mjesec(i)',
        YEARLYintervalUnit  : 'godina',
        Each                : 'Svaki',
        'On the'            : 'Na',
        'End repeat'        : 'Završi ponavljanje',
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
        'position-1' : 'posljednji'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nikada',
        After     : 'Nakon',
        'On date' : 'Na datum'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Bez ponavljanja',
        Daily   : 'Svakodnevno',
        Weekly  : 'Tjedno',
        Monthly : 'Mjesečno',
        Yearly  : 'Godišnje'
    },

    RecurrenceCombo : {
        None   : 'Nijedan',
        Custom : 'Prilagođeno...'
    },

    Summary : {
        'Summary for' : date => `Sažetak za ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Cijeli raspored',
        currentview  : 'Vidljivi raspored',
        daterange    : 'Raspon datuma',
        completedata : 'Cijeli raspored (za sve događaje)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Raspon rasporeda',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nema dodijeljenog resursa'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odgovor poslužitelja:'
    },

    DurationColumn : {
        Duration : 'Trajanje'
    }
};

export default LocaleHelper.publishLocale(locale);
