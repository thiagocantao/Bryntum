import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Sl.js';

const locale = {

    localeName : 'Sl',
    localeDesc : 'Slovensko',
    localeCode : 'sl',

    Object : {
        newEvent : 'Nov dogodek'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' dogod' + (data !== 1 ? 'ke' : 'ek')
    },

    Dependencies : {
        from    : 'Od',
        to      : 'Do',
        valid   : 'Veljavno',
        invalid : 'Neveljavno'
    },

    DependencyType : {
        SS           : 'ZZ',
        SF           : 'ZK',
        FS           : 'KZ',
        FF           : 'KK',
        StartToStart : 'Od začetka do začetka',
        StartToEnd   : 'Od začetka do konca',
        EndToStart   : 'Od konca do začetka',
        EndToEnd     : 'Od konca do konca',
        short        : [
            'ZZ',
            'ZK',
            'KZ',
            'KK'
        ],
        long : [
            'Od začetka do začetka',
            'Od začetka do konca',
            'Od konca do začetka',
            'Od konca do konca'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Vrsta',
        Lag               : 'Zaostajanje',
        'Edit dependency' : 'Uredi odvisnost',
        Save              : 'Shrani',
        Delete            : 'Izbriši',
        Cancel            : 'Prekliči',
        StartToStart      : 'Od začetka do začetka',
        StartToEnd        : 'Od začetka do konca',
        EndToStart        : 'Od konca do začetka',
        EndToEnd          : 'Od konca do konca'
    },

    EventEdit : {
        Name         : 'Ime',
        Resource     : 'Vir',
        Start        : 'Začetek',
        End          : 'Konec',
        Save         : 'Shrani',
        Delete       : 'Izbriši',
        Cancel       : 'Prekliči',
        'Edit event' : 'Uredi dogodek',
        Repeat       : 'Ponovi'
    },

    EventDrag : {
        eventOverlapsExisting : 'Dogodek prekriva obstoječi dogodek za ta vir',
        noDropOutsideTimeline : 'Dogodek ne sme biti popolnoma izpuščen izven časovnice'
    },

    SchedulerBase : {
        'Add event'      : 'Dodaj dogodek',
        'Delete event'   : 'Izbriši dogodek',
        'Unassign event' : 'Prekliči dodelitev dogodka',
        color            : 'Barva'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Povečaj',
        activeDateRange : 'Datumski obseg',
        startText       : 'Začetni datum',
        endText         : 'Končni datum',
        todayText       : 'Danes'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiraj dogodek',
        cutEvent   : 'Izreži dogodek',
        pasteEvent : 'Prilepi dogodek'
    },

    EventFilter : {
        filterEvents : 'Filtriraj opravila',
        byName       : 'Po imenu'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Pokaži trenutno časovnico'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekunde'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM/DD, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM/DD',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dan'
        },
        day : {
            name : 'Dan/ure'
        },
        week : {
            name : 'Teden/ure'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Teden/dnevi'
        },
        dayAndMonth : {
            name : 'Mesec'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Teden'
        },
        weekAndMonth : {
            name : 'Tedni'
        },
        weekAndDayLetter : {
            name : 'Tedni/delavniki'
        },
        weekDateAndMonth : {
            name : 'Meseci/tedni'
        },
        monthAndYear : {
            name : 'Meseci'
        },
        year : {
            name : 'Leta'
        },
        manyYears : {
            name : 'Več let'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Brišete dogodek',
        'delete-all-message'        : 'Želite izbrisati vse pojavitve tega dogodka?',
        'delete-further-message'    : 'Želite izbrisati to in vse prihodnje pojavitve tega dogodka ali samo trenutno pojavitev?',
        'delete-further-btn-text'   : 'Izbriši vse prihodnje dogodke',
        'delete-only-this-btn-text' : 'Izbriši samo ta dogodek',
        'update-title'              : 'Spreminjate ponavljajoči se dogodek',
        'update-all-message'        : 'Želite spremeniti vse pojavitve tega dogodka?',
        'update-further-message'    : 'Želite spremeniti samo to pojavitev dogodka ali to in vse prihodnje pojavitve?',
        'update-further-btn-text'   : 'Vsi prihodnji dogodki',
        'update-only-this-btn-text' : 'Samo ta dogodek',
        Yes                         : 'Da',
        Cancel                      : 'Prekliči',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' in ',
        Daily                           : 'Dnevno',
        'Weekly on {1}'                 : ({ days }) => ` Tedensko ob ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mesečno ob ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Letno ob ${days} v mesecu  ${months}`,
        'Every {0} days'                : ({ interval }) => `Vsakih ${interval} dni`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => ` Vsakih ${interval} tednov ob ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => ` Vsakih ${interval} mesecev ob ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => ` Vsakih ${interval} let ob ${days} v mesecu ${months}`,
        position1                       : 'prvi',
        position2                       : 'drugi',
        position3                       : 'tretji',
        position4                       : 'četrti',
        position5                       : 'peti',
        'position-1'                    : 'zadnji',
        day                             : 'dan',
        weekday                         : 'delovni dan',
        'weekend day'                   : 'dan za vikend',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ponovi dogodek',
        Cancel              : 'Prekliči',
        Save                : 'Shrani',
        Frequency           : 'Pogostost',
        Every               : 'Vsak',
        DAILYintervalUnit   : 'dan',
        WEEKLYintervalUnit  : 'teden',
        MONTHLYintervalUnit : 'mesec',
        YEARLYintervalUnit  : 'leto',
        Each                : 'Vsak',
        'On the'            : 'Na',
        'End repeat'        : 'Končaj ponavljanje',
        'time(s)'           : 'krat'
    },

    RecurrenceDaysCombo : {
        day           : 'dan',
        weekday       : 'delovni dan',
        'weekend day' : 'dan za vikend'
    },

    RecurrencePositionsCombo : {
        position1    : 'prvi',
        position2    : 'drugi',
        position3    : 'tretji',
        position4    : 'četrti',
        position5    : 'peti',
        'position-1' : 'zadnji'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nikoli',
        After     : 'Po',
        'On date' : 'Na datum'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Brez ponavljanja',
        Daily   : 'Dnevno',
        Weekly  : 'Tedensko',
        Monthly : 'Mesečno',
        Yearly  : 'Letno'
    },

    RecurrenceCombo : {
        None   : 'Brez',
        Custom : 'Po meri...'
    },

    Summary : {
        'Summary for' : date => ` Povzetek za ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Celoten urnik',
        currentview  : 'Viden urnik',
        daterange    : 'Datumski obseg',
        completedata : 'Celoten urnik (za vse dogodke)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Obseg urnika',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Ni dodeljenega vira'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odziv strežnika:'
    },

    DurationColumn : {
        Duration : 'Trajanje'
    }
};

export default LocaleHelper.publishLocale(locale);
