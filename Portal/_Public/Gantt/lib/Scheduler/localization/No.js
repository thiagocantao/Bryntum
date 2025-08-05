import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/No.js';

const locale = {

    localeName : 'No',
    localeDesc : 'Norsk',
    localeCode : 'no',

    Object : {
        newEvent : 'Ny hendelse'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' hendelse' + (data !== 1 ? 'r' : '')
    },

    Dependencies : {
        from    : 'Fra',
        to      : 'Til',
        valid   : 'Gyldig',
        invalid : 'Ugyldig'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SSl',
        FS           : 'SlS',
        FF           : 'SlSl',
        StartToStart : 'Start-til-start',
        StartToEnd   : 'Start-til-slutt',
        EndToStart   : 'Slutt-til-start',
        EndToEnd     : 'Slutt-til-slutt',
        short        : [
            'SS',
            'SSl',
            'SlS',
            'SlSl'
        ],
        long : [
            'Start-til-start',
            'Start-til-slutt',
            'Slutt-til-start',
            'Slutt-til-slutt'
        ]
    },

    DependencyEdit : {
        From              : 'Fra',
        To                : 'Til',
        Type              : 'Type',
        Lag               : 'Forsinkelse',
        'Edit dependency' : 'Rediger avhengighet',
        Save              : 'Lagre',
        Delete            : 'Slett',
        Cancel            : 'Avbryt',
        StartToStart      : 'Start til start',
        StartToEnd        : 'Start til slutt',
        EndToStart        : 'Slutt til start',
        EndToEnd          : 'Slutt til slutt'
    },

    EventEdit : {
        Name         : 'Navn',
        Resource     : 'Ressurs',
        Start        : 'Start',
        End          : 'Slutt',
        Save         : 'Lagre',
        Delete       : 'Slett',
        Cancel       : 'Avbryt',
        'Edit event' : 'Redigere hendelse',
        Repeat       : 'Gjenta'
    },

    EventDrag : {
        eventOverlapsExisting : 'Hendelse overlapper eksisterende hendelse for denne ressursen',
        noDropOutsideTimeline : 'Hendelsen kan ikke droppes helt utenfor tidslinjen'
    },

    SchedulerBase : {
        'Add event'      : 'Legg til hendelse',
        'Delete event'   : 'Slett hendelse',
        'Unassign event' : 'Opphev tilordningen av hendelsen',
        color            : 'Farge'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Datointervall',
        startText       : 'Startdato',
        endText         : 'Sluttdato',
        todayText       : 'I dag'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopier hendelse',
        cutEvent   : 'Klipp ut hendelse',
        pasteEvent : 'Lim inn hendelse'
    },

    EventFilter : {
        filterEvents : 'Filtrer oppgaver',
        byName       : 'Etter navn'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Vi gjeldende tidslinje'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekunder'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dag'
        },
        day : {
            name : 'Dag/timer'
        },
        week : {
            name : 'Uke/timer'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Uke/dager'
        },
        dayAndMonth : {
            name : 'Måned'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Uke'
        },
        weekAndMonth : {
            name : 'Uker'
        },
        weekAndDayLetter : {
            name : 'Uker/ukedager'
        },
        weekDateAndMonth : {
            name : 'Måneder/uker'
        },
        monthAndYear : {
            name : 'Måneder'
        },
        year : {
            name : 'År'
        },
        manyYears : {
            name : 'Flere år'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Du sletter en hendelse',
        'delete-all-message'        : 'Vil du slette alle forekomster av denne hendelsen?',
        'delete-further-message'    : 'Vil du slette denne og alle fremtidige forekomster av denne hendelsen, eller bare den valgte forekomsten?',
        'delete-further-btn-text'   : 'Slett alle fremtidige hendelser',
        'delete-only-this-btn-text' : 'Slett bare denne hendelsen',
        'update-title'              : 'Du endrer en gjentakende hendelse',
        'update-all-message'        : 'Vil du endre alle forekomster av denne hendelsen?',
        'update-further-message'    : 'Vil du endre bare denne forekomsten av hendelsen, eller denne og alle fremtidige forekomster?',
        'update-further-btn-text'   : 'Alle fremtidige hendelser',
        'update-only-this-btn-text' : 'Bare denne hendelsen',
        Yes                         : 'Ja',
        Cancel                      : 'Avbryt',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' og ',
        Daily                           : 'Daglig',
        'Weekly on {1}'                 : ({ days }) => `Ukentlig i ${days}`,
        'Monthly on {1}'                : ({ days }) => `Månedlig i ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Årlig i ${days} av ${months}`,
        'Every {0} days'                : ({ interval }) => `Årlig ${interval} dager`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Hver ${interval} uke(r) i ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Hver ${interval} måned(er) i on ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Hvert ${interval} år i ${days} of ${months}`,
        position1                       : 'den første',
        position2                       : 'den andre',
        position3                       : 'den tredje',
        position4                       : 'den fjerde',
        position5                       : 'den femte',
        'position-1'                    : 'den siste',
        day                             : 'dad',
        weekday                         : 'ukedag',
        'weekend day'                   : 'helgedag',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Gjenta hendelse',
        Cancel              : 'Avbryt',
        Save                : 'Lagre',
        Frequency           : 'Hyppighet',
        Every               : 'Hver',
        DAILYintervalUnit   : 'dag(er)',
        WEEKLYintervalUnit  : 'uke(r)',
        MONTHLYintervalUnit : 'måned (er)',
        YEARLYintervalUnit  : 'år',
        Each                : 'Hver',
        'On the'            : 'Den',
        'End repeat'        : 'Slutt gjentakelse',
        'time(s)'           : 'gang(er)'
    },

    RecurrenceDaysCombo : {
        day           : 'dag',
        weekday       : 'ukedag',
        'weekend day' : 'helgedag'
    },

    RecurrencePositionsCombo : {
        position1    : 'første',
        position2    : 'andre',
        position3    : 'tredje',
        position4    : 'fjerde',
        position5    : 'femte',
        'position-1' : 'siste'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Aldri',
        After     : 'Etter',
        'On date' : 'På dato'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Ingen gjentakelse',
        Daily   : 'Daglig',
        Weekly  : 'Ukentlig',
        Monthly : 'Måndelig',
        Yearly  : 'Årlig'
    },

    RecurrenceCombo : {
        None   : 'Ingen',
        Custom : 'Tilpasset...'
    },

    Summary : {
        'Summary for' : date => `Oppsummering for ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Fullstendig tidsplan',
        currentview  : 'Synlig tidsplan',
        daterange    : 'Datointervall',
        completedata : 'Fullstendig tidsplan (for alle hendelser)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Tidsplanintervall',
        'Export from'    : 'Fra',
        'Export to'      : 'Til'
    },

    ExcelExporter : {
        'No resource assigned' : 'Ingen ressurs tildelt'
    },

    CrudManagerView : {
        serverResponseLabel : 'Serversvar:'
    },

    DurationColumn : {
        Duration : 'Varighet'
    }
};

export default LocaleHelper.publishLocale(locale);
