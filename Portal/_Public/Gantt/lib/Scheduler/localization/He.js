import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/He.js';

const locale = {

    localeName : 'He',
    localeDesc : 'עִברִית',
    localeCode : 'he',

    Object : {
        newEvent : 'אירוע חדש'
    },

    ResourceInfoColumn : {
        eventCountText : data => (data !== 1 ? 'ים' : '') + 'אירוע ' + data
    },

    Dependencies : {
        from    : 'מ',
        to      : 'אל',
        valid   : 'חוקי',
        invalid : 'בלתי-חוקי'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'מההתחלה להתחלה',
        StartToEnd   : 'מההתחלה לסוף',
        EndToStart   : 'מהסוף להתחלה',
        EndToEnd     : 'מהסוף לסוף',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'מההתחלה להתחלה',
            'מהסוף להתחלה',
            'מהסוף להתחלה',
            'מהסוף לסוף'
        ]
    },

    DependencyEdit : {
        From              : 'מ',
        To                : 'אל',
        Type              : 'סוג',
        Lag               : 'עיכוב',
        'Edit dependency' : 'ערוך תלות',
        Save              : 'שמור',
        Delete            : 'מחוק',
        Cancel            : 'בטל',
        StartToStart      : 'מההתחלה להתחלה',
        StartToEnd        : 'מההתחלה לסוף',
        EndToStart        : 'מהסוף להתחלה',
        EndToEnd          : 'מהסוף לסוף'
    },

    EventEdit : {
        Name         : 'שם',
        Resource     : 'משאב',
        Start        : 'התחלה',
        End          : 'סוף',
        Save         : 'שמור',
        Delete       : 'מחוק',
        Cancel       : 'בטלl',
        'Edit event' : 'ערוך אירוע',
        Repeat       : 'חזור'
    },

    EventDrag : {
        eventOverlapsExisting : 'האירוע חופף לאירוע קיים במשאב זה',
        noDropOutsideTimeline : 'לא ניתן לסלק את האירוע לחלוטין אל מחוץ לקו הזמן'
    },

    SchedulerBase : {
        'Add event'      : 'הוספת אירוע',
        'Delete event'   : 'מחיקת אירוע',
        'Unassign event' : 'בטל הקצאת אירוע',
        color            : 'צבע'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'זום',
        activeDateRange : 'טווח תאריכים',
        startText       : 'תאריך התחלה',
        endText         : 'תאריך סיום',
        todayText       : 'היום'
    },

    EventCopyPaste : {
        copyEvent  : 'העתק אירוע',
        cutEvent   : 'גזור אירוע',
        pasteEvent : 'הדבק אירוע'
    },

    EventFilter : {
        filterEvents : 'סינון משימות',
        byName       : 'עפ”י שם'
    },

    TimeRanges : {
        showCurrentTimeLine : 'הצג את קו הזמן הנוכחי'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'שניות'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'יום'
        },
        day : {
            name : 'יום/שעות'
        },
        week : {
            name : 'שבוע/שעות'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'שבוע/ימים'
        },
        dayAndMonth : {
            name : 'חודש'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'שבוע'
        },
        weekAndMonth : {
            name : 'שבועות'
        },
        weekAndDayLetter : {
            name : 'שבועות/ימי השבוע'
        },
        weekDateAndMonth : {
            name : 'חודשים/שבועות'
        },
        monthAndYear : {
            name : 'חודשים'
        },
        year : {
            name : 'שנים'
        },
        manyYears : {
            name : 'מספר שנים'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'הנך מוחק/ת כעת אירוע',
        'delete-all-message'        : '?האם ברצונך למחוק את כל המופעים של אירוע זה',
        'delete-further-message'    : '?האם ברצונך למחוק מופע זה וכל המופעים העתידיים של אירוע זה, או רק את המופע הנבחר',
        'delete-further-btn-text'   : 'מחוק את כל האירועים העתידיים',
        'delete-only-this-btn-text' : 'מחק רק את האירוע הזה',
        'update-title'              : 'הנך משנה כעת אירוע חוזר',
        'update-all-message'        : '?האם ברצונך לשנות את כל המופעים של אירוע זה',
        'update-further-message'    : '?האם ברצונך לשנות רק את המופע הזה של האירוע, או גם את המופע הזה וגם מופעים עתידיים',
        'update-further-btn-text'   : 'כל האירועים העתידיים',
        'update-only-this-btn-text' : 'רק את האירוע הזה',
        Yes                         : 'כן',
        Cancel                      : 'בטל',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' וגם ',
        Daily                           : 'יומי',
        'Weekly on {1}'                 : ({ days }) => `${days}-מדי שבוע ב`,
        'Monthly on {1}'                : ({ days }) => `${days}-מדי חודש ב`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `${months} מתוך ${days}-מדי שנה ב`,
        'Every {0} days'                : ({ interval }) => `ימים ${interval} מדי`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `${days} שבועות מדי ${interval}-ב`,
        'Every {0} months on {1}'       : ({ interval, days }) => `${days} חודשים מדי ${interval}-ב`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `מתוך ${months} מדי ${days}-שנים ב ${interval}-ב`,
        position1                       : 'הראשון',
        position2                       : 'השני',
        position3                       : 'השלישי',
        position4                       : 'הרביעי',
        position5                       : 'החמישי',
        'position-1'                    : 'האחרון',
        day                             : 'יום',
        weekday                         : 'היום בשבוע',
        'weekend day'                   : 'היום בסופ”ש',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'אירוע חוזר',
        Cancel              : 'בטל',
        Save                : 'שמור',
        Frequency           : 'תדירות',
        Every               : 'מדי',
        DAILYintervalUnit   : 'יום/ימים',
        WEEKLYintervalUnit  : 'שבוע/שבועות',
        MONTHLYintervalUnit : 'חודש/ים',
        YEARLYintervalUnit  : 'שנה/שנים',
        Each                : 'כל',
        'On the'            : '-ב',
        'End repeat'        : 'סוף החזרה',
        'time(s)'           : 'פעם/פעמים'
    },

    RecurrenceDaysCombo : {
        day           : 'יום',
        weekday       : 'היום בשבוע',
        'weekend day' : 'היום בסופ”ש'
    },

    RecurrencePositionsCombo : {
        position1    : 'ראשון',
        position2    : 'שני',
        position3    : 'שלישי',
        position4    : 'רביעי',
        position5    : 'חמישי',
        'position-1' : 'אחרון'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'לעולם לא',
        After     : 'לאחר',
        'On date' : 'בתאריך'
    },

    RecurrenceFrequencyCombo : {
        None    : 'ללא חזרה',
        Daily   : 'מדי יום',
        Weekly  : 'מדי שבוע',
        Monthly : 'מדי חודש',
        Yearly  : 'מדי שנה'
    },

    RecurrenceCombo : {
        None   : 'אין',
        Custom : '...מותאם אישית'
    },

    Summary : {
        'Summary for' : date => `${date} סיכום עבור`
    },

    ScheduleRangeCombo : {
        completeview : 'לו”ז מלא',
        currentview  : 'לו”ז נראה לעין',
        daterange    : 'טווח תאריכים',
        completedata : 'הלו”ז המלא (לכל האירועים)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'טווח לו”ז',
        'Export from'    : 'מ',
        'Export to'      : 'אל'
    },

    ExcelExporter : {
        'No resource assigned' : 'לא הוקצה שום משאב'
    },

    CrudManagerView : {
        serverResponseLabel : ':תגובת השרת'
    },

    DurationColumn : {
        Duration : 'משך'
    }
};

export default LocaleHelper.publishLocale(locale);
