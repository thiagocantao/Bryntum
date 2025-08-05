import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Da.js';

const locale = {

    localeName : 'Da',
    localeDesc : 'Dansk',
    localeCode : 'da',

    Object : {
        newEvent : 'ny begivenhed'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' begivenhed' + (data !== 1 ? 'er' : '')
    },

    Dependencies : {
        from    : 'Fra',
        to      : 'Til',
        valid   : 'Gyldig',
        invalid : 'Ugyldig'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'Start-til-Start',
        StartToEnd   : 'Start-til-slut',
        EndToStart   : 'Slut-til-Start',
        EndToEnd     : 'slut-til-Slut',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'Start-til-Start',
            'Start-til-slut',
            'slut-til-Start',
            'slut-til-slut'
        ]
    },

    DependencyEdit : {
        From              : 'Fra',
        To                : 'Til',
        Type              : 'Typ',
        Lag               : 'Lag',
        'Edit dependency' : 'Rediger afhængighed',
        Save              : 'Gemme',
        Delete            : 'Slet',
        Cancel            : 'Afbestille',
        StartToStart      : 'Start til Start',
        StartToEnd        : 'Start til Slut',
        EndToStart        : 'Slut til Start',
        EndToEnd          : 'Slut til Slut'
    },

    EventEdit : {
        Name         : 'Navn',
        Resource     : 'Ressource',
        Start        : 'Start',
        End          : 'Slut',
        Save         : 'Gemme',
        Delete       : 'Slet',
        Cancel       : 'Afbestille',
        'Edit event' : 'Rediger begivenhed',
        Repeat       : 'Gentage'
    },

    EventDrag : {
        eventOverlapsExisting : 'Hændelse overlapper eksisterende hændelse for denne ressource',
        noDropOutsideTimeline : 'Begivenheden må ikke droppes helt uden for tidslinjen'
    },

    SchedulerBase : {
        'Add event'      : 'Tilføj begivenhed',
        'Delete event'   : 'Slet begivenhed',
        'Unassign event' : 'Fjern tildeling af begivenhed',
        color            : 'Farve'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Datointerval',
        startText       : 'Start dato',
        endText         : 'Slut dato',
        todayText       : 'I dag'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiér begivenhed',
        cutEvent   : 'Klip begivenhed',
        pasteEvent : 'Indsæt begivenhed'
    },

    EventFilter : {
        filterEvents : 'Filtrer opgaver',
        byName       : 'Ved navn'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Vis den aktuelle tidslinje'
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
            name : 'Uge/timer'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Uge/dage'
        },
        dayAndMonth : {
            name : 'Måned'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Uge'
        },
        weekAndMonth : {
            name : 'Uger'
        },
        weekAndDayLetter : {
            name : 'Uger/hverdage'
        },
        weekDateAndMonth : {
            name : 'Måneder/uger'
        },
        monthAndYear : {
            name : 'Måneder'
        },
        year : {
            name : 'Flere år'
        },
        manyYears : {
            name : 'Flere år'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'du sletter en begivenhed',
        'delete-all-message'        : 'Vil du slette alle forekomster af denne begivenhed?',
        'delete-further-message'    : 'Ønsker du at slette denne og alle fremtidige forekomster af denne begivenhed, eller kun den valgte forekomst?',
        'delete-further-btn-text'   : 'Slet alle fremtidige begivenheder',
        'delete-only-this-btn-text' : 'Slet kun denne begivenhed',
        'update-title'              : 'Du ændrer en gentagende begivenhed',
        'update-all-message'        : 'Vil du ændre alle forekomster af denne begivenhed?',
        'update-further-message'    : 'Vil du kun ændre denne forekomst af begivenheden, eller denne og alle fremtidige begivenheder?',
        'update-further-btn-text'   : 'Alle fremtidige begivenheder',
        'update-only-this-btn-text' : 'Kun denne begivenhed',
        Yes                         : 'ja',
        Cancel                      : 'Afbestille',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' og ',
        Daily                           : 'Daglige',
        'Weekly on {1}'                 : ({ days }) => `Ugentligt på ${days}`,
        'Monthly on {1}'                : ({ days }) => `Månedligt ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Årligt på ${days} af ${months}`,
        'Every {0} days'                : ({ interval }) => `Hver ${interval} dage`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Hver ${interval} uger efter ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Hver${interval} måneder på ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Hver ${interval} år efter ${days} af ${months}`,
        position1                       : 'den første',
        position2                       : 'den anden',
        position3                       : 'den tredje',
        position4                       : 'den fjerde',
        position5                       : 'den femte',
        'position-1'                    : 'den sidste',
        day                             : 'dage',
        weekday                         : 'hverdag',
        'weekend day'                   : 'weekenddag',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Gentag begivenhed',
        Cancel              : 'Afbestille',
        Save                : 'Gemme',
        Frequency           : 'Frekvens',
        Every               : 'Hver',
        DAILYintervalUnit   : 'dage(r)',
        WEEKLYintervalUnit  : 'uge(r)',
        MONTHLYintervalUnit : 'måned(er)',
        YEARLYintervalUnit  : 'år(er)',
        Each                : 'Hver',
        'On the'            : 'På den',
        'End repeat'        : 'Afslut gentagelse',
        'time(s)'           : 'tid(er)'
    },

    RecurrenceDaysCombo : {
        day           : 'dag',
        weekday       : 'hverdag',
        'weekend day' : 'weekenddag'
    },

    RecurrencePositionsCombo : {
        position1    : 'første',
        position2    : 'anden',
        position3    : 'tredje',
        position4    : 'fjerde',
        position5    : 'femte',
        'position-1' : 'sidste'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Aldrig',
        After     : 'Efter',
        'On date' : 'På dato'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Ingen gentagelse',
        Daily   : 'Daglige',
        Weekly  : 'Ugentlig',
        Monthly : 'Månedlige',
        Yearly  : 'Årligt'
    },

    RecurrenceCombo : {
        None   : 'Ingen',
        Custom : 'Brugerdefinerede...'
    },

    Summary : {
        'Summary for' : date => `Resumé for ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Komplet tidsplan',
        currentview  : 'Synlig tidsplan',
        daterange    : 'Datointerval',
        completedata : 'Komplet tidsplan (for alle arrangementer)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Tidsplan rækkevidde',
        'Export from'    : 'Fra',
        'Export to'      : 'Til'
    },

    ExcelExporter : {
        'No resource assigned' : 'Ingen ressource tildelt'
    },

    CrudManagerView : {
        serverResponseLabel : 'Serversvar:'
    },

    DurationColumn : {
        Duration : 'Varighed'
    }
};

export default LocaleHelper.publishLocale(locale);
