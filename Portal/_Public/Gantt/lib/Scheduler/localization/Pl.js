import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Pl.js';

const locale = {

    localeName : 'Pl',
    localeDesc : 'Polski',
    localeCode : 'pl',

    Object : {
        newEvent : ' Nowe wydarzenie'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' wydarzeni' + (data !== 1 ? 'a' : 'e')
    },

    Dependencies : {
        from    : 'From',
        to      : 'To',
        valid   : 'Ważny',
        invalid : 'Nieważny'
    },

    DependencyType : {
        SS           : 'PP',
        SF           : 'PK',
        FS           : 'KP',
        FF           : 'KK',
        StartToStart : 'Od początku do początku',
        StartToEnd   : 'Od początku do końca',
        EndToStart   : 'Od końca do początku',
        EndToEnd     : 'Od końca do końca',
        short        : [
            'PP',
            'PK',
            'KP',
            'KK'
        ],
        long : [
            'Od początku do początku',
            'Od początku do końca',
            'Od końca do początku',
            'Od końca do końca'
        ]
    },

    DependencyEdit : {
        From              : 'Od',
        To                : 'Do',
        Type              : 'Rodzaj',
        Lag               : 'Lag',
        'Edit dependency' : 'Edytuj zależność',
        Save              : 'Zapisz',
        Delete            : 'Usuń',
        Cancel            : 'Anuluj',
        StartToStart      : 'Od początku do początku',
        StartToEnd        : 'Od początku do końca',
        EndToStart        : 'Od końca do początku',
        EndToEnd          : 'Od końca do końca'
    },

    EventEdit : {
        Name         : 'Nazwa',
        Resource     : 'Zasób',
        Start        : 'Początek',
        End          : 'Koniec',
        Save         : 'Zapisz',
        Delete       : 'Usuń',
        Cancel       : 'Anuluj',
        'Edit event' : 'Edytuj wydarzenie',
        Repeat       : 'Powtórz'
    },

    EventDrag : {
        eventOverlapsExisting : 'Wydarzenie nakłada się na istniejące wydarzenie dla tego zasobu',
        noDropOutsideTimeline : 'Wydarzenie nie może zostać całkowicie usunięte poza osią czasu'
    },

    SchedulerBase : {
        'Add event'      : 'Dodaj wydarzenie',
        'Delete event'   : 'Usuń wydarzenie',
        'Unassign event' : 'Cofnij przypisanie wydarzenia',
        color            : 'Kolor'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Powiększenie',
        activeDateRange : 'Zakres dat',
        startText       : 'Data rozpoczęcia',
        endText         : 'Data zakończenia',
        todayText       : 'Dzisiaj'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopiuj wydarzenie',
        cutEvent   : 'Wytnij wydarzenie',
        pasteEvent : 'Wklej wydarzenie'
    },

    EventFilter : {
        filterEvents : 'Filtruj zadania',
        byName       : 'Według nazwy'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Pokaż aktualną oś czasu'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekundy'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Dzień'
        },
        day : {
            name : 'Dzień/godzina'
        },
        week : {
            name : 'Tydzień/godzina'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Tydzień/dni'
        },
        dayAndMonth : {
            name : 'Miesiąc'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Tydzień'
        },
        weekAndMonth : {
            name : 'Tygodnie'
        },
        weekAndDayLetter : {
            name : 'Tygodnie/dni tygodnia'
        },
        weekDateAndMonth : {
            name : 'Miesiące/tygodnie'
        },
        monthAndYear : {
            name : 'Miesiące'
        },
        year : {
            name : 'Lata'
        },
        manyYears : {
            name : 'Kilka lat'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Usuwasz wydarzenie',
        'delete-all-message'        : 'Czy chcesz usunąć wszystkie przypadki wystąpienia tego wydarzenia?',
        'delete-further-message'    : 'Czy chcesz usunąć ten oraz przyszłe przypadki wystąpienia tego wydarzenia lub tylko wybrany przypadek?',
        'delete-further-btn-text'   : 'Usuń wszystkie przyszłe wydarzenia',
        'delete-only-this-btn-text' : 'Usuń tylko to wydarzenie',
        'update-title'              : 'Zmieniasz powtarzające się wydarzenie',
        'update-all-message'        : 'Czy chcesz zmienić wszystkie przypadki wystąpienia tego wydarzenia?',
        'update-further-message'    : 'Czy chcesz zmienić tylko ten przypadek wystąpienia wydarzenia lub ten oraz przyszłe przypadki?',
        'update-further-btn-text'   : 'Wszystkie przyszłe wydarzenia',
        'update-only-this-btn-text' : 'Tylko to wydarzenie',
        Yes                         : 'Tak',
        Cancel                      : 'Anuluj',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' oraz ',
        Daily                           : 'Codziennie',
        'Weekly on {1}'                 : ({ days }) => `Cotygodniowo w ${days}`,
        'Monthly on {1}'                : ({ days }) => `Miesięcznie w ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Rocznie w ${days} of ${months}`,
        'Every {0} days'                : ({ interval }) => `Co${interval} dni`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Co ${interval} tygodni w ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Co ${interval} miesięcy w ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Co${interval} lat w ${days} of ${months}`,
        position1                       : 'pierwszy',
        position2                       : 'drugi',
        position3                       : 'trzeci',
        position4                       : 'czwarty',
        position5                       : 'piąty',
        'position-1'                    : 'ostatni',
        day                             : 'dzień',
        weekday                         : 'dzień tygodnia',
        'weekend day'                   : 'dzień weekendu',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Powtórz wydarzenie',
        Cancel              : 'Anuluj',
        Save                : 'Zapisz',
        Frequency           : 'Częstotliwość',
        Every               : 'Co',
        DAILYintervalUnit   : 'dni',
        WEEKLYintervalUnit  : 'tygodni',
        MONTHLYintervalUnit : 'miesięcy',
        YEARLYintervalUnit  : 'lat',
        Each                : 'Każdy',
        'On the'            : 'W',
        'End repeat'        : 'Zakończ powtórzenie',
        'time(s)'           : 'godzina(-y)'
    },

    RecurrenceDaysCombo : {
        day           : 'dzień',
        weekday       : 'dzień tygodnia',
        'weekend day' : 'dzień weekendu'
    },

    RecurrencePositionsCombo : {
        position1    : 'pierwszy',
        position2    : 'drugi',
        position3    : 'trzeci',
        position4    : 'czwarty',
        position5    : 'piąty',
        'position-1' : 'ostatni'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Nigdy',
        After     : 'Po',
        'On date' : 'Dnia'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Bez powtórzeń',
        Daily   : 'Codziennie',
        Weekly  : 'Co tydzień',
        Monthly : 'Co miesiąc',
        Yearly  : 'Co rok'
    },

    RecurrenceCombo : {
        None   : 'Żaden',
        Custom : 'Standard...'
    },

    Summary : {
        'Summary for' : date => `Podsumowanie dla ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Complete harmonogram',
        currentview  : 'Widoczny harmonogram',
        daterange    : 'Zakres dat',
        completedata : 'Pełen harmonogram (wszystkich wydarzeń)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Zakres harmonogramu',
        'Export from'    : 'Od',
        'Export to'      : 'Do'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nie przydzielono żadnego zasobu'
    },

    CrudManagerView : {
        serverResponseLabel : 'Odpowiedź serwera:'
    },

    DurationColumn : {
        Duration : 'Czas trwania'
    }
};

export default LocaleHelper.publishLocale(locale);
