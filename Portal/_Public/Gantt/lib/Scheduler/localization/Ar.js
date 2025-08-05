import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Ar.js';

const locale = {

    localeName : 'Ar',
    localeDesc : 'اللغة العربية',
    localeCode : 'ar',

    Object : {
        newEvent : 'حدث جديد'
    },

    ResourceInfoColumn : {
        eventCountText : data => 'حدث' + (data !== 1 ? 'الأ' : '') + ' ' + data
    },

    Dependencies : {
        from    : 'منذ',
        to      : 'إلى',
        valid   : 'صالح',
        invalid : 'غير صالح'
    },

    DependencyType : {
        SS           : 'ب ب',
        SF           : 'ب ن',
        FS           : 'ن ب',
        FF           : 'ن ن',
        StartToStart : 'من البداية إلى البداية',
        StartToEnd   : 'من البداية إلى النهاية',
        EndToStart   : 'من النهاية إلى البداية',
        EndToEnd     : 'من النهاية إلى النهاية',
        short        : [
            'ب ب',
            'ب ن',
            'ن ب',
            'ن ن'
        ],
        long : [
            'من البداية إلى البداية',
            'من البداية إلى النهاية',
            'من النهاية إلى البداية',
            'من النهاية إلى النهاية'
        ]
    },

    DependencyEdit : {
        From              : 'من',
        To                : 'إلى',
        Type              : 'النوع',
        Lag               : 'تأخر',
        'Edit dependency' : 'تعديل التبعية',
        Save              : 'حفظ',
        Delete            : 'حذف',
        Cancel            : 'إلغاء',
        StartToStart      : ' من البداية إلى البداية',
        StartToEnd        : ' من البداية إلى النهاية',
        EndToStart        : 'من النهاية إلى البداية',
        EndToEnd          : ' من النهاية إلى النهاية'
    },

    EventEdit : {
        Name         : 'الاسم',
        Resource     : 'المورد',
        Start        : 'بدء',
        End          : 'إنهاء',
        Save         : 'حفظ',
        Delete       : 'حذف',
        Cancel       : 'إلغاء',
        'Edit event' : 'تعديل حدث',
        Repeat       : 'تجديد'
    },

    EventDrag : {
        eventOverlapsExisting : 'يتداخل الحدث مع حدث موجود لهذا المورد',
        noDropOutsideTimeline : 'قد لا يتم إخراج هذا الحدث بالكامل خارج المخطط الزمني'
    },

    SchedulerBase : {
        'Add event'      : 'إضافة حدث',
        'Delete event'   : 'حذف حدث',
        'Unassign event' : 'حدث غير معين',
        color            : 'لون'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'تكبير',
        activeDateRange : 'النطاق الزمني',
        startText       : 'تاريخ البداية',
        endText         : 'تاريخ النهاية',
        todayText       : 'اليوم'
    },

    EventCopyPaste : {
        copyEvent  : 'نسخ حدث',
        cutEvent   : 'قص حدث',
        pasteEvent : 'لصق حدث'
    },

    EventFilter : {
        filterEvents : 'تصفية المهام',
        byName       : 'بالاسم'
    },

    TimeRanges : {
        showCurrentTimeLine : 'إظهار الخط الزمني الحالي'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'ثواني'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'يوم'
        },
        day : {
            name : 'يوم/ساعات'
        },
        week : {
            name : 'أسبوع/ساعات'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'أسبوع/أيام'
        },
        dayAndMonth : {
            name : 'شهر'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'أسبوع'
        },
        weekAndMonth : {
            name : 'أسابيع'
        },
        weekAndDayLetter : {
            name : 'أسابيع/أيام الأسبوع'
        },
        weekDateAndMonth : {
            name : 'شهور/أسابيع'
        },
        monthAndYear : {
            name : 'شهور'
        },
        year : {
            name : 'سنوات'
        },
        manyYears : {
            name : 'عدة سنوات'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'أنت بصدد حذف حدث',
        'delete-all-message'        : 'هل تريد حذف جميع تكرارات هذا الحدث؟',
        'delete-further-message'    : 'هل تريد حذف هذا الحدث وجميع التكرارات المستقبلية له، أم التكرار المحدد فقط؟',
        'delete-further-btn-text'   : 'حذف جميع الأحداث المستقبلية',
        'delete-only-this-btn-text' : 'حذف هذا الحدث فقط',
        'update-title'              : 'أنت بصدد تغيير حدث متكرر',
        'update-all-message'        : 'هل تريد تغيير جميع تكرارات هذا الحدث؟',
        'update-further-message'    : 'هل تريد تغيير التكرار المحدد للحدث فقط، أم تغيير الحدث وجميع التكرارات المستقبلية له؟',
        'update-further-btn-text'   : 'جميع الأحداث المستقبلية',
        'update-only-this-btn-text' : 'هذا الحدث فقط',
        Yes                         : 'نعم',
        Cancel                      : 'إلغاء',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' و ',
        Daily                           : 'يوميًا',
        'Weekly on {1}'                 : ({ days }) => `${days} أسبوعيًا في`,
        'Monthly on {1}'                : ({ days }) => `${days} شهريًا في`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `${months} من ${days} سنويًا في ${days}`,
        'Every {0} days'                : ({ interval }) => `يومًا ${interval} كل`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `${days} أسبوعًا في ${interval} كل`,
        'Every {0} months on {1}'       : ({ interval, days }) => `${days} شهر في ${interval} كل`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `${months} من ${days}  سنة في ${interval} كل`,
        position1                       : 'الأول',
        position2                       : 'الثاني',
        position3                       : 'الثالث',
        position4                       : 'الرابع',
        position5                       : 'الخامس',
        'position-1'                    : 'الأخير',
        day                             : 'يوم',
        weekday                         : 'يوم من أيام الأسبوع',
        'weekend day'                   : 'يوم عطلة نهاية الأسبوع',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'كرر الحدث',
        Cancel              : 'إلغاء',
        Save                : 'حفظ',
        Frequency           : 'التردد',
        Every               : 'كل',
        DAILYintervalUnit   : 'يوم (أيام)',
        WEEKLYintervalUnit  : 'أسبوع (أسابيع)',
        MONTHLYintervalUnit : 'شهر (شهور)',
        YEARLYintervalUnit  : 'سنة (سنوات)',
        Each                : 'كل',
        'On the'            : 'في ال',
        'End repeat'        : 'نهاية التكرار',
        'time(s)'           : 'وقت (أوقات)'
    },

    RecurrenceDaysCombo : {
        day           : 'يوم',
        weekday       : 'يوم من أيام الأسبوع',
        'weekend day' : 'يوم عطلة نهاية الأسبوع'
    },

    RecurrencePositionsCombo : {
        position1    : 'أول',
        position2    : 'ثانٍ',
        position3    : 'ثالث',
        position4    : 'رابع',
        position5    : 'خامس',
        'position-1' : 'أخير'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'أبدًا',
        After     : 'بعد',
        'On date' : 'بتاريخ'
    },

    RecurrenceFrequencyCombo : {
        None    : 'لا يوجد تكرار',
        Daily   : 'يوميًا',
        Weekly  : 'أسبوعيًا',
        Monthly : 'شهريًا',
        Yearly  : 'سنويًا'
    },

    RecurrenceCombo : {
        None   : 'لا يوجد',
        Custom : 'مخصص...'
    },

    Summary : {
        'Summary for' : date => `${date} موجز ل`
    },

    ScheduleRangeCombo : {
        completeview : 'جدول زمني كامل',
        currentview  : 'جدول زمني مرئي',
        daterange    : 'نطاق التاريخ',
        completedata : 'جدول زمني كامل (لكل الأحداث)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'نطاق الجدول الزمني',
        'Export from'    : 'من',
        'Export to'      : 'إلى'
    },

    ExcelExporter : {
        'No resource assigned' : 'لم يتم تحديد مورد'
    },

    CrudManagerView : {
        serverResponseLabel : 'استجابة الخادم:'
    },

    DurationColumn : {
        Duration : 'المدة الزمنية'
    }
};

export default LocaleHelper.publishLocale(locale);
