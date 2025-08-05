import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/De.js';

const locale = {

    localeName : 'De',
    localeDesc : 'Deutsch',
    localeCode : 'de-DE',

    Object : {
        newEvent : 'Neues Ereignis'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' Ereignis' + (data !== 1 ? 'se' : '')
    },

    Dependencies : {
        from    : 'Von',
        to      : 'Bis',
        valid   : 'Gültig',
        invalid : 'Ungültig'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SE',
        FS           : 'ES',
        FF           : 'EE',
        StartToStart : 'Start-zu-Start',
        StartToEnd   : 'Start-zu-Ende',
        EndToStart   : 'Ende-zu-Start',
        EndToEnd     : 'Ende-zu-Ende',
        short        : [
            'SS',
            'SE',
            'ES',
            'EE'
        ],
        long : [
            'Start-zu-Start',
            'Start-zu-Ende',
            'Ende-zu-Start',
            'Ende-zu-Ende'
        ]
    },

    DependencyEdit : {
        From              : 'Von',
        To                : 'Bis',
        Type              : 'Typ',
        Lag               : 'Lag',
        'Edit dependency' : 'Abhängigkeit bearbeiten',
        Save              : 'Speichern',
        Delete            : 'Löschen',
        Cancel            : 'Abbrechen',
        StartToStart      : 'Start-zu-Start',
        StartToEnd        : 'Start-zu-Ende',
        EndToStart        : 'Ende-zu-Start',
        EndToEnd          : 'Ende-zu-Ende'
    },

    EventEdit : {
        Name         : 'Name',
        Resource     : 'Ressource',
        Start        : 'Start',
        End          : 'Ende',
        Save         : 'Speichern',
        Delete       : 'Löschen',
        Cancel       : 'Abbrechen',
        'Edit event' : 'Ereignis bearbeiten',
        Repeat       : 'Wiederholen'
    },

    EventDrag : {
        eventOverlapsExisting : 'Ereignis überschneidet sich mit bestehendem Ereignis für diese Ressource',
        noDropOutsideTimeline : 'Das Ereignis darf nicht vollständig außerhalb der Zeitspanne liegen'
    },

    SchedulerBase : {
        'Add event'      : 'Ereignis hinzufügen',
        'Delete event'   : 'Ereignis löschen',
        'Unassign event' : 'Zuordnung des Ereignisses aufheben',
        color            : 'Farbe'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Datumspanne',
        startText       : 'Startdatum',
        endText         : 'Enddatum',
        todayText       : 'Heute'
    },

    EventCopyPaste : {
        copyEvent  : 'Ereignis kopieren',
        cutEvent   : 'Ereignis ausschneiden',
        pasteEvent : 'Ereignis einfügen'
    },

    EventFilter : {
        filterEvents : 'Aufgaben filtern',
        byName       : 'Nach Namen'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Aktuelle Zeitleiste anzeigen'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekunden'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Tag'
        },
        day : {
            name : 'Tag/Stunden'
        },
        week : {
            name : 'Woche/Stunden'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Woche/Tage'
        },
        dayAndMonth : {
            name : 'Monat'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Woche'
        },
        weekAndMonth : {
            name : 'Wochen'
        },
        weekAndDayLetter : {
            name : 'Woche/Wochentage'
        },
        weekDateAndMonth : {
            name : 'Monate/Wochen'
        },
        monthAndYear : {
            name : 'Monate'
        },
        year : {
            name : 'Jahre'
        },
        manyYears : {
            name : 'Mehrere Jahre'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Sie löschen ein Ereignis',
        'delete-all-message'        : 'Möchten Sie alle Vorkommnisse dieses Ereignisses löschen?',
        'delete-further-message'    : 'Möchten Sie dieses und alle zukünftigen Ereignisse dieses Ereignisses löschen, oder nur das ausgewählte Vorkommnis?',
        'delete-further-btn-text'   : 'Alle zukünftigen Ereignisse löschen',
        'delete-only-this-btn-text' : 'Nur dieses Ereignis löschen',
        'update-title'              : 'Sie ändern ein sich wiederholendes Ereignis',
        'update-all-message'        : 'Möchten Sie alle Vorkommnisse dieses Ereignisses ändern?',
        'update-further-message'    : 'Möchten Sie nur dieses Ereignis oder dieses und alle zukünftigen Vorkommnisse ändern?',
        'update-further-btn-text'   : 'Alle zukünftigen Ereignisse',
        'update-only-this-btn-text' : 'Nur dieses Ereignis',
        Yes                         : 'Ja',
        Cancel                      : 'Abbrechen',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' und ',
        Daily                           : 'Täglich',
        'Weekly on {1}'                 : ({ days }) => `Wöchentlich an ${days}`,
        'Monthly on {1}'                : ({ days }) => `Monatlich an ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Jährlich an ${days} von ${months}`,
        'Every {0} days'                : ({ interval }) => `Alle ${interval} Tage`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Alle ${interval} Wochen an ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Alle ${interval} Monate an ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Alle ${interval} Jahre an ${days} von ${months}`,
        position1                       : 'der erste',
        position2                       : 'der zweite',
        position3                       : 'der dritte',
        position4                       : 'der vierte',
        position5                       : 'der fünfte',
        'position-1'                    : 'der letzte',
        day                             : 'tag',
        weekday                         : 'wochentag',
        'weekend day'                   : 'wochenend tag',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ereignis wiederholen',
        Cancel              : 'Abbrechen',
        Save                : 'Speichern',
        Frequency           : 'Frequenz',
        Every               : 'Alle',
        DAILYintervalUnit   : 'tag(e)',
        WEEKLYintervalUnit  : 'woche(n)',
        MONTHLYintervalUnit : 'monat(e)',
        YEARLYintervalUnit  : 'jahr(e)',
        Each                : 'jede',
        'On the'            : 'am',
        'End repeat'        : 'Wiederholung beenden',
        'time(s)'           : 'mal(e)'
    },

    RecurrenceDaysCombo : {
        day           : 'tag',
        weekday       : 'wochentag',
        'weekend day' : 'wochend tag'
    },

    RecurrencePositionsCombo : {
        position1    : 'erste',
        position2    : 'zweite',
        position3    : 'dritte',
        position4    : 'vierte',
        position5    : 'fünfte',
        'position-1' : 'letzte'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nie',
        After     : 'Nach',
        'On date' : 'Am Datum'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Keine Wiederholung',
        Daily   : 'Täglich',
        Weekly  : 'Wöchentlich',
        Monthly : 'Monatlich',
        Yearly  : 'Jährlich'
    },

    RecurrenceCombo : {
        None   : 'Kein',
        Custom : 'Benutzerdefiniert...'
    },

    Summary : {
        'Summary for' : date => `Zusammenfassung für ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Vollständiger Zeitplan',
        currentview  : 'Sichtbarer Zeitplan',
        daterange    : 'Datumsspanne',
        completedata : 'Vollständiger Zeitplan (für alle Ereignisse)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Zeitplanbereich',
        'Export from'    : 'Von',
        'Export to'      : 'Bis'
    },

    ExcelExporter : {
        'No resource assigned' : 'Keine Ressource zugewiesen'
    },

    CrudManagerView : {
        serverResponseLabel : 'Server Antwort:'
    },

    DurationColumn : {
        Duration : 'Dauer'
    }
};

export default LocaleHelper.publishLocale(locale);
