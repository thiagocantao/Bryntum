import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Hi.js';

const locale = {

    localeName : 'Hi',
    localeDesc : 'हिन्दी',
    localeCode : 'hi',

    Object : {
        newEvent : 'नया ईवेंट'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' ईवेंट' + (data !== 1 ? '' : '')
    },

    Dependencies : {
        from    : 'से',
        to      : 'तक',
        valid   : 'मान्य',
        invalid : 'अमान्य'
    },

    DependencyType : {
        SS           : 'भ-भ',
        SF           : 'त-भ',
        FS           : 'भ-त',
        FF           : 'त-त',
        StartToStart : 'आरंभ से आरंभ तक',
        StartToEnd   : 'आरंभ से अंत तक',
        EndToStart   : 'अंत से आरंभ तक',
        EndToEnd     : 'अंत से अंत तक',
        short        : [
            'भ-भ',
            'त-भ',
            'भ-त',
            'त-त'
        ],
        long : [
            'आरंभ से आरंभ तक',
            'आरंभ से अंत तक',
            'अंत से आरंभ तक',
            'अंत से अंत तक'
        ]
    },

    DependencyEdit : {
        From              : 'से',
        To                : 'तक',
        Type              : 'प्रकार',
        Lag               : 'पिछड़ाव',
        'Edit dependency' : 'निर्भरता संपादित करें',
        Save              : 'सहेजें',
        Delete            : 'हटाएं',
        Cancel            : 'रद्द करें',
        StartToStart      : 'आरंभ से आरंभ तक',
        StartToEnd        : 'आरंभ से अंत तक',
        EndToStart        : 'अंत से आरंभ तक',
        EndToEnd          : 'अंत से अंत तक'
    },

    EventEdit : {
        Name         : 'नाम',
        Resource     : 'संसाधन',
        Start        : 'आरंभ',
        End          : 'अंत',
        Save         : 'सहेजें',
        Delete       : 'हटाएं',
        Cancel       : 'रद्द करें',
        'Edit event' : 'ईवेंट संपादित करें',
        Repeat       : 'दोहराएं'
    },

    EventDrag : {
        eventOverlapsExisting : 'इवेंट इस संसाधन के लिए मौजूदा ईवेंट को ओवरलैप करता है',
        noDropOutsideTimeline : 'इवेंट को पूरी तरह से टाइमलाइन के बाहर नहीं छोड़ा जा सकता'
    },

    SchedulerBase : {
        'Add event'      : 'ईवेंट जोड़ें',
        'Delete event'   : 'ईवेंट हटाएं',
        'Unassign event' : 'ईवेंट असाइनमेंट निकालें',
        color            : 'रंग'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'ज़ूम करें',
        activeDateRange : 'तारीख की रेंज',
        startText       : 'आरंभ तारीख',
        endText         : 'समापन तारीख',
        todayText       : 'आज'
    },

    EventCopyPaste : {
        copyEvent  : 'ईवेंट कॉपी करें',
        cutEvent   : 'ईवेंट कट करें',
        pasteEvent : 'ईवेंट पेस्ट करें'
    },

    EventFilter : {
        filterEvents : 'टास्क फिल्टर करें',
        byName       : 'नाम के आधार पर'
    },

    TimeRanges : {
        showCurrentTimeLine : 'वर्तमान टाइमलाइन दिखाएं'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'सेकंड'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM/DD, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM/DD',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'दिन'
        },
        day : {
            name : 'दिन/घंटे'
        },
        week : {
            name : 'सप्ताह/घंटे'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'सप्ताह/दिन'
        },
        dayAndMonth : {
            name : 'माह'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'सप्ताह'
        },
        weekAndMonth : {
            name : 'सप्ताह'
        },
        weekAndDayLetter : {
            name : 'सप्ताह/सप्ताहांत'
        },
        weekDateAndMonth : {
            name : 'माह/सप्ताह'
        },
        monthAndYear : {
            name : 'माह'
        },
        year : {
            name : 'साल'
        },
        manyYears : {
            name : 'एकाधिक साल'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'आप एक ईवेंट हटा रहे हैं',
        'delete-all-message'        : 'क्या आप इस ईवेंट की सभी घटनाएं हटाना चाहते हैं?',
        'delete-further-message'    : 'क्या आप इसे और इस ईवेंट की भविष्य की सभी घटनाओं को हटाना चाहते हैं, या केवल चयनित ईवेंट को हटाना चाहते हैं?',
        'delete-further-btn-text'   : 'भविष्य के सभी ईवेंट हटाएं',
        'delete-only-this-btn-text' : 'केवल यह ईवेंट हटाएं',
        'update-title'              : 'आप एक दोहराव वाले ईवेंट को बदल रहे हैं',
        'update-all-message'        : 'क्या आप इस ईवेंट की सभी घटनाएं हटाना चाहते हैं?',
        'update-further-message'    : 'क्या आप केवल इस ईवेंट की घटना को बदलना चाहते हैं, या यह और भविष्य के सभी ईवेंट बदलना चाहते हैं?',
        'update-further-btn-text'   : 'भविष्य के सभी ईवेंट',
        'update-only-this-btn-text' : 'केवल यह ईवेंट',
        Yes                         : 'हाँ',
        Cancel                      : 'रद्द करें',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' और ',
        Daily                           : 'दैनिक',
        'Weekly on {1}'                 : ({ days }) => `${days} पर साप्ताहिक`,
        'Monthly on {1}'                : ({ days }) => `${days} पर मासिक`,
        'Yearly on {1} of {2}'          : ({ days, months }) => ` ${months} के ${days} सलाना`,
        'Every {0} days'                : ({ interval }) => `प्रत्येक ${interval} दिनों के अंतराल पर`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `प्रत्येक ${interval} सप्ताह के अंतराल पर ${days} को`,
        'Every {0} months on {1}'       : ({ interval, days }) => `प्रत्येक ${interval} महीनो के अंतराल पर  ${days} को`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `प्रत्येक ${interval} सालों के अंतराल पर ${months} के ${days} को`,
        position1                       : 'पहला',
        position2                       : 'दूसरा',
        position3                       : 'तीसरा',
        position4                       : 'चौथा',
        position5                       : 'पांचवां',
        'position-1'                    : 'अंतिम',
        day                             : 'दिन',
        weekday                         : 'सप्ताह का दिन',
        'weekend day'                   : 'सप्ताहांत का दिन',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'ईवेंट दोहराएं',
        Cancel              : 'रद्द करें',
        Save                : 'सहेजें',
        Frequency           : 'आवृत्ति',
        Every               : 'हर',
        DAILYintervalUnit   : 'दिन',
        WEEKLYintervalUnit  : 'हफ्ता(ते)',
        MONTHLYintervalUnit : 'महीना(ने)',
        YEARLYintervalUnit  : 'साल',
        Each                : 'प्रत्येक',
        'On the'            : 'पर',
        'End repeat'        : 'अंत दोहराव',
        'time(s)'           : 'बार'
    },

    RecurrenceDaysCombo : {
        day           : 'दिन',
        weekday       : 'सप्ताह का दिन',
        'weekend day' : 'सप्ताहांत का दिन'
    },

    RecurrencePositionsCombo : {
        position1    : 'पहला',
        position2    : 'दूसरा',
        position3    : 'तीसरा',
        position4    : 'चौथा',
        position5    : 'पांचवां',
        'position-1' : 'अंतिम'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'कभी नहीं',
        After     : 'के बाद',
        'On date' : 'इस तारीख को'
    },

    RecurrenceFrequencyCombo : {
        None    : 'कोई दोहराव नहीं',
        Daily   : 'दैनिक',
        Weekly  : 'साप्ताहिक',
        Monthly : 'मासिक',
        Yearly  : 'वार्षिक'
    },

    RecurrenceCombo : {
        None   : 'कोई नहीं',
        Custom : 'कस्टम...'
    },

    Summary : {
        'Summary for' : date => ` ${date} का सारांश`
    },

    ScheduleRangeCombo : {
        completeview : 'पूरा शेड्यूल',
        currentview  : 'दिखने वाला शेड्यूल',
        daterange    : 'तारीख की रेंज',
        completedata : 'पूरा शेड्यूल(सभी ईवेंट के लिए)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'शेड्यूल रेंज',
        'Export from'    : 'से',
        'Export to'      : 'तक'
    },

    ExcelExporter : {
        'No resource assigned' : 'कोई संसाधन असाइन नहीं'
    },

    CrudManagerView : {
        serverResponseLabel : 'सर्वर प्रतिक्रिया:'
    },

    DurationColumn : {
        Duration : 'अवधि'
    }
};

export default LocaleHelper.publishLocale(locale);
