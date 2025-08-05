import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Hu.js';

const locale = {

    localeName : 'Hu',
    localeDesc : 'Magyar',
    localeCode : 'hu',

    Object : {
        newEvent : 'Új esemény'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' esemény'
    },

    Dependencies : {
        from    : 'Kezdete',
        to      : 'Vége',
        valid   : 'Érvényes',
        invalid : 'Érvénytelen'
    },

    DependencyType : {
        SS           : 'KK',
        SF           : 'KV',
        FS           : 'VK',
        FF           : 'VV',
        StartToStart : 'Kezdéstől a kezdésig',
        StartToEnd   : 'Kezdéstől a végéig',
        EndToStart   : 'Végétől a kezdésig',
        EndToEnd     : 'Végétől a végéig',
        short        : [
            'KK',
            'KV',
            'VK',
            'VV'
        ],
        long : [
            'Kezdéstől kezdésig',
            'Kezdéstől a végéig',
            'Végétől a kezdésig',
            'Végétől a végéig'
        ]
    },

    DependencyEdit : {
        From              : 'Kezdete',
        To                : 'Vége',
        Type              : 'Típus',
        Lag               : 'Késés',
        'Edit dependency' : 'Függőség szerkesztése',
        Save              : 'Mentés',
        Delete            : 'Törlés',
        Cancel            : 'Mégse',
        StartToStart      : 'Kezdéstől a kezdésig',
        StartToEnd        : 'Kezdéstől a végéig',
        EndToStart        : 'Végétől a kezdésig',
        EndToEnd          : 'Végétől a végéig'
    },

    EventEdit : {
        Name         : 'Név',
        Resource     : 'Erőforrás',
        Start        : 'Kezdés',
        End          : 'Vége',
        Save         : 'Mentés',
        Delete       : 'Törlés',
        Cancel       : 'Mégse',
        'Edit event' : 'Esemény szerkesztése',
        Repeat       : 'Ismét'
    },

    EventDrag : {
        eventOverlapsExisting : 'Az esemény átfedésben van az erőforrás eseményével',
        noDropOutsideTimeline : 'Az esemény nem ejthető teljesen az idővonalon kívülre'
    },

    SchedulerBase : {
        'Add event'      : 'Esemény hozzáadása',
        'Delete event'   : 'Esemény törlése',
        'Unassign event' : 'Esemény hozzárendelésének törlése',
        color            : 'Szín'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Nagyítás',
        activeDateRange : 'Dátumtartomány',
        startText       : 'Kezdő dátum',
        endText         : 'Befejező dátum',
        todayText       : 'Ma'
    },

    EventCopyPaste : {
        copyEvent  : 'Esemény másolása',
        cutEvent   : 'Esemény kivágása',
        pasteEvent : 'Esemény beillesztése'
    },

    EventFilter : {
        filterEvents : 'Események szűrése',
        byName       : 'Név szerint'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Jelenlegi idővonal megjelenítése'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Másodperc'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM. DD., H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM. DD.',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Nap'
        },
        day : {
            name : 'Nap/óra'
        },
        week : {
            name : 'Hét/óra'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Hét/nap'
        },
        dayAndMonth : {
            name : 'Hónap'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Hét'
        },
        weekAndMonth : {
            name : 'Hét'
        },
        weekAndDayLetter : {
            name : 'Hét/hétköznap'
        },
        weekDateAndMonth : {
            name : 'Hónap/hét'
        },
        monthAndYear : {
            name : 'Hónap'
        },
        year : {
            name : 'Év'
        },
        manyYears : {
            name : 'Több év'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Töröl egy eseményt',
        'delete-all-message'        : 'Törli az esemény minden előfordulását?',
        'delete-further-message'    : 'Szeretné törölni ezt és az esemény összes jövőbeli előfordulását, vagy csak a kiválasztott előfordulást?',
        'delete-further-btn-text'   : 'Minden jövőbeli esemény törlése',
        'delete-only-this-btn-text' : 'Csak ezt az eseményt törölje',
        'update-title'              : 'Egy ismétlődő eseményt módosít',
        'update-all-message'        : 'Módosítja az esemény minden előfordulását?',
        'update-further-message'    : 'Csak ezt az eseményt módosítja, vagy annak összes többi előfordulását is?',
        'update-further-btn-text'   : 'Minden jövőbeli esemény',
        'update-only-this-btn-text' : 'Csak ez az esemény',
        Yes                         : 'Igen',
        Cancel                      : 'Mégse',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' és ',
        Daily                           : 'Naponta',
        'Weekly on {1}'                 : ({ days }) => `Hetente ${days}`,
        'Monthly on {1}'                : ({ days }) => `Havonta ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Évente, minden ${months}, ${days}`,
        'Every {0} days'                : ({ interval }) => `Minden ${interval}. napon`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Minden ${interval} . héten, ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Minden ${interval} . hónapban, ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Minden ${interval} . évben,  ${days} , ${months}`,
        position1                       : 'első',
        position2                       : 'második',
        position3                       : 'harmadik',
        position4                       : 'negyedik',
        position5                       : 'ötödik',
        'position-1'                    : 'utolsó',
        day                             : 'nap',
        weekday                         : 'hétköznap',
        'weekend day'                   : 'hétvége',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Esemény ismétlése',
        Cancel              : 'Mégse',
        Save                : 'Mentés',
        Frequency           : 'Gyakoriság',
        Every               : 'Minden',
        DAILYintervalUnit   : 'nap',
        WEEKLYintervalUnit  : 'hét',
        MONTHLYintervalUnit : 'hónap',
        YEARLYintervalUnit  : 'év',
        Each                : 'Mindegyik',
        'On the'            : 'Ekkor:',
        'End repeat'        : 'Ismétlés vége',
        'time(s)'           : 'alkalom'
    },

    RecurrenceDaysCombo : {
        day           : 'nap',
        weekday       : 'hétköznap',
        'weekend day' : 'hétvége'
    },

    RecurrencePositionsCombo : {
        position1    : 'első',
        position2    : 'második',
        position3    : 'harmadik',
        position4    : 'negyedik',
        position5    : 'ötödik',
        'position-1' : 'utolsó'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Soha',
        After     : 'Utána',
        'On date' : 'Aznap'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Nincs ismétlés',
        Daily   : 'Naponta',
        Weekly  : 'Hetente',
        Monthly : 'Havonta',
        Yearly  : 'Évente'
    },

    RecurrenceCombo : {
        None   : 'Nincs',
        Custom : 'Egyedi...'
    },

    Summary : {
        'Summary for' : date => `Összefoglaló, ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Teljes ütemterv',
        currentview  : 'Látható ütemterv',
        daterange    : 'Dátumtartomány',
        completedata : 'Teljes ütemterv (minden eseményhez)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Ütemezési tartomány',
        'Export from'    : 'Kezdete',
        'Export to'      : 'Vége'
    },

    ExcelExporter : {
        'No resource assigned' : 'Nincs hozzárendelt erőforrás'
    },

    CrudManagerView : {
        serverResponseLabel : 'Szerverválasz:'
    },

    DurationColumn : {
        Duration : 'Időtartam'
    }
};

export default LocaleHelper.publishLocale(locale);
