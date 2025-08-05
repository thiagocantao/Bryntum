import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Bg.js';

const locale = {

    localeName : 'Bg',
    localeDesc : 'Български',
    localeCode : 'bg',

    Object : {
        newEvent : 'Ново събитие'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' събити' + (data !== 1 ? 'я' : 'е')
    },

    Dependencies : {
        from    : 'От',
        to      : 'До',
        valid   : 'Валидно',
        invalid : 'Невалидно'
    },

    DependencyType : {
        SS           : 'НН',
        SF           : 'НК',
        FS           : 'КН',
        FF           : 'КК',
        StartToStart : 'От начало до начало',
        StartToEnd   : 'От начало до край',
        EndToStart   : 'От край до начало',
        EndToEnd     : 'От край до край',
        short        : [
            'НН',
            'НК',
            'КН',
            'КК'
        ],
        long : [
            'От начало до начало',
            'От начало до край',
            'От край до начало',
            'От край до край'
        ]
    },

    DependencyEdit : {
        From              : 'От',
        To                : 'До',
        Type              : 'Тип',
        Lag               : 'Забавяне',
        'Edit dependency' : 'Редактиране на зависимостта',
        Save              : 'Запис',
        Delete            : 'Изтриване',
        Cancel            : 'Отказ',
        StartToStart      : 'От начало до начало',
        StartToEnd        : 'От начало до край',
        EndToStart        : 'От край до начало',
        EndToEnd          : 'От край до край'
    },

    EventEdit : {
        Name         : 'Име',
        Resource     : 'Ресурс',
        Start        : 'Начало',
        End          : 'Край',
        Save         : 'Запис',
        Delete       : 'Изтриване',
        Cancel       : 'Отказ',
        'Edit event' : 'Редактиране на събитие',
        Repeat       : 'Повторение'
    },

    EventDrag : {
        eventOverlapsExisting : 'Събитието се припокрива със съществуващо събитие за този ресурс',
        noDropOutsideTimeline : 'Събитието не може да бъде изхвърлено изцяло извън времевата линия'
    },

    SchedulerBase : {
        'Add event'      : 'Добавяне на събитие',
        'Delete event'   : 'Изтриване на събитие',
        'Unassign event' : 'Отмяна на събитието',
        color            : 'цвят'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Мащабиране',
        activeDateRange : 'Диапазон на датите',
        startText       : 'Начална дата',
        endText         : 'Крайна дата',
        todayText       : 'Днес'
    },

    EventCopyPaste : {
        copyEvent  : 'Събитие за копиране',
        cutEvent   : 'Прекъсване на събитието',
        pasteEvent : 'Събитие за вмъкване'
    },

    EventFilter : {
        filterEvents : 'Задачи за филтриране',
        byName       : 'По име'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Показване на текуша хронология'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Секунди'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Ден'
        },
        day : {
            name : 'Ден/часа'
        },
        week : {
            name : 'Седмица/часа'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Седмица/дни'
        },
        dayAndMonth : {
            name : 'Месец'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Седмица'
        },
        weekAndMonth : {
            name : 'Седмици'
        },
        weekAndDayLetter : {
            name : 'Седмици/делници'
        },
        weekDateAndMonth : {
            name : 'Месеци/седмици'
        },
        monthAndYear : {
            name : 'Месеци'
        },
        year : {
            name : 'Години'
        },
        manyYears : {
            name : 'Много години'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Вие изтривате събитие',
        'delete-all-message'        : 'Искате ли да изтриете всички повторения на това събитие?',
        'delete-further-message'    : 'Искате ли да изтриете това и всички бъдещи повторения на това събитие, или само избраното повторение?',
        'delete-further-btn-text'   : 'Изтриване на всички бъдещи събития',
        'delete-only-this-btn-text' : 'Изтриване само на това събитие',
        'update-title'              : 'Променяте повтарящо се събитие',
        'update-all-message'        : 'Искате ли промяна на всички повторения на това събитие?',
        'update-further-message'    : 'Искате ли да промените само това повторение на събитието или тази и всички бъдещи повторения?',
        'update-further-btn-text'   : 'Всички бъдещи събития',
        'update-only-this-btn-text' : 'Само това събитие',
        Yes                         : 'Да',
        Cancel                      : 'Отказ',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' и ',
        Daily                           : 'Ежедневно',
        'Weekly on {1}'                 : ({ days }) => `Ежеседмично на ${days}`,
        'Monthly on {1}'                : ({ days }) => `Ежемесечно на ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) =>  `Ежегодно на ${days} на ${months}`,
        'Every {0} days'                : ({ interval }) => `На всеки ${interval} дни`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `На всеки ${interval} седмици на ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `На всеки ${interval} месеца на ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `На всеки ${interval} години на ${days} от ${months}`,
        position1                       : 'първия',
        position2                       : 'втория',
        position3                       : 'третия',
        position4                       : 'четвъртия',
        position5                       : 'петия',
        'position-1'                    : 'последния',
        day                             : 'дни',
        weekday                         : 'ден от седмицата',
        'weekend day'                   : 'ден от уикенда',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Повтаряне на събития',
        Cancel              : 'Отказ',
        Save                : 'Запис',
        Frequency           : 'Честота',
        Every               : 'Всеки',
        DAILYintervalUnit   : 'ден(дни)',
        WEEKLYintervalUnit  : 'седмица(и)',
        MONTHLYintervalUnit : 'месец(и)',
        YEARLYintervalUnit  : 'година(и)',
        Each                : 'Всеки',
        'On the'            : 'На',
        'End repeat'        : 'Край на повторението',
        'time(s)'           : 'път(и)'
    },

    RecurrenceDaysCombo : {
        day           : 'ден',
        weekday       : 'ден от седмицата',
        'weekend day' : 'ден от уикенда'
    },

    RecurrencePositionsCombo : {
        position1    : 'първи',
        position2    : 'втори',
        position3    : 'трети',
        position4    : 'четвърти',
        position5    : 'пети',
        'position-1' : 'последния'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Никога',
        After     : 'След',
        'On date' : 'На дата'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Без повторение',
        Daily   : 'Ежедневно',
        Weekly  : 'Ежеседмично',
        Monthly : 'Ежемесечно',
        Yearly  : 'Ежегодно'
    },

    RecurrenceCombo : {
        None   : 'Няма',
        Custom : 'Потребителски...'
    },

    Summary : {
        'Summary for' : date => `Резюме за ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Пълен график',
        currentview  : 'Видим график',
        daterange    : 'Диапазон на датите',
        completedata : 'Пълен график (за всички събития)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Диапазон на графика',
        'Export from'    : 'От',
        'Export to'      : 'До'
    },

    ExcelExporter : {
        'No resource assigned' : 'Няма назначен ресурс'
    },

    CrudManagerView : {
        serverResponseLabel : 'Отговор на сървъра:'
    },

    DurationColumn : {
        Duration : 'Продължителност'
    }
};

export default LocaleHelper.publishLocale(locale);
