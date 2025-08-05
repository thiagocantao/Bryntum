import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/El.js';

const locale = {

    localeName : 'El',
    localeDesc : 'Ελληνικά',
    localeCode : 'el',

    Object : {
        newEvent : 'Νέο συμβάν'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' συμβάν' + (data !== 1 ? 'εις' : '')
    },

    Dependencies : {
        from    : 'Από',
        to      : 'Έως',
        valid   : 'Έγκυρο',
        invalid : 'Άκυρο'
    },

    DependencyType : {
        SS           : 'ΑΑ',
        SF           : 'ΑΤ',
        FS           : 'ΤΑ',
        FF           : 'ΤΤ',
        StartToStart : 'Αρχή-με-Αρχή',
        StartToEnd   : 'Αρχή-με-Τέλος',
        EndToStart   : 'Τέλος-με-Αρχή',
        EndToEnd     : 'Τέλος-με-Τέλος',
        short        : [
            'ΑΑ',
            'ΑΤ',
            'ΤΑ',
            'ΤΤ'
        ],
        long : [
            'Αρχή-με-Αρχή',
            'Αρχή-με-Τέλος',
            'Τέλος-με-Αρχή',
            'Τέλος-με-Τέλος'
        ]
    },

    DependencyEdit : {
        From              : 'Από',
        To                : 'Έως',
        Type              : 'Τύπος',
        Lag               : 'Καθυστέρηση',
        'Edit dependency' : 'Επεξεργασία εξάρτησης',
        Save              : 'Αποθήκευση',
        Delete            : 'Διαγραφή',
        Cancel            : 'Ακύρωση',
        StartToStart      : 'Αρχή με Αρχή',
        StartToEnd        : 'Αρχή με Τέλος',
        EndToStart        : 'Τέλος με Αρχή',
        EndToEnd          : 'Τέλος με Τέλος'
    },

    EventEdit : {
        Name         : 'Όνομα',
        Resource     : 'Πόρος',
        Start        : 'Αρχή',
        End          : 'Τέλος',
        Save         : 'Αποθήκευση',
        Delete       : 'Διαγραφή',
        Cancel       : 'Ακύρωση',
        'Edit event' : 'Επεξεργασία συμβάντος',
        Repeat       : 'Επανάληψη'
    },

    EventDrag : {
        eventOverlapsExisting : 'Το συγκεκριμένο συμβάν επικαλύπτει τρέχον συμβάν για αυτόν τον πόρο',
        noDropOutsideTimeline : 'Το συμβάν δεν μπορεί να οριστεί εντελώς εκτός του χρονοδιαγράμματος'
    },

    SchedulerBase : {
        'Add event'      : 'Προσθήκη συμβάντος',
        'Delete event'   : 'Διαγραφή συμβάντος',
        'Unassign event' : 'Ακύρωση ανάθεσης συμβάντος',
        color            : 'Χρώμα '
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Εστίαση',
        activeDateRange : 'Εύρος ημερομηνιών',
        startText       : 'Ημερομηνία έναρξης',
        endText         : 'Ημερομηνία λήξης',
        todayText       : 'Σήμερα'
    },

    EventCopyPaste : {
        copyEvent  : 'Αντιγραφή συμβάντος',
        cutEvent   : 'Αποκοπή συμβάντος',
        pasteEvent : 'Επικόλληση συμβάντος'
    },

    EventFilter : {
        filterEvents : 'Φιλτράρισμα διεργασιών',
        byName       : 'Κατά όνομα'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Εμφάνιση τρέχοντος χρονοδιαγράμματος'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Δευτερόλεπτα'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Ημέρα'
        },
        day : {
            name : 'Ημέρα/Ώρες'
        },
        week : {
            name : 'Εβδομάδα/Ώρες'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Εβδομάδα/Ημέρες'
        },
        dayAndMonth : {
            name : 'Μήνας'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Εβδομάδα'
        },
        weekAndMonth : {
            name : 'Εβδομάδες'
        },
        weekAndDayLetter : {
            name : 'Εβδομάδες/εργάσιμες ημέρες'
        },
        weekDateAndMonth : {
            name : 'Μήνες/Εβδομάδες'
        },
        monthAndYear : {
            name : 'Μήνες'
        },
        year : {
            name : 'Έτη'
        },
        manyYears : {
            name : 'Πολλά χρόνια'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Πρόκειται να διαγράψετε κάποιο συμβάν',
        'delete-all-message'        : 'Επιθυμείτε τη διαγραφή όλων των επαναλήψεων του συγκεκριμένου συμβάντος;',
        'delete-further-message'    : 'Θέλετε να διαγράψετε αυτήν και όλες τις μελλοντικές επαναλήψεις αυτού του συμβάντος ή μόνο την επιλεγμένη επανάληψη;',
        'delete-further-btn-text'   : 'Διαγραφή όλων των μελλοντικών συμβάντων',
        'delete-only-this-btn-text' : 'Διαγραφή μόνο αυτού του συμβάντος',
        'update-title'              : 'Πρόκειται να τροποποιήσετε κάποιο επαναλαμβανόμενο συμβάν',
        'update-all-message'        : 'Επιθυμείτε την τροποποίηση όλων των επαναλήψεων του συγκεκριμένου συμβάντος;',
        'update-further-message'    : 'Επιθυμείτε την αλλαγή μόνο της συγκεκριμένης επανάληψης του συμβάντος, ή αυτής και όλων των επόμενων επαναλήψεων;',
        'update-further-btn-text'   : 'Όλα τα Μελλοντικά Συμβάντα',
        'update-only-this-btn-text' : 'Μόνο το συγκεκριμένο Συμβάν',
        Yes                         : 'Ναι',
        Cancel                      : 'Ακύρωση',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' και ',
        Daily                           : 'Καθημερινά',
        'Weekly on {1}'                 : ({ days }) => `Εβδομαδιαίως κάθε ${days}`,
        'Monthly on {1}'                : ({ days }) => `Μηνιαίως κάθε ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Ετησίως κάθε ${days} του ${months}`,
        'Every {0} days'                : ({ interval }) => `Κάθε ${interval} ημέρες`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Κάθε ${interval} εβδομάδες τις ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Κάθε ${interval} μήνες τις {days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Κάθε ${interval} έτη την ${days} του ${months}`,
        position1                       : 'πρώτη',
        position2                       : 'δεύτερη',
        position3                       : 'τρίτη',
        position4                       : 'τέταρτη',
        position5                       : 'πέμπτη',
        'position-1'                    : 'τελευταία',
        day                             : 'ημέρα',
        weekday                         : 'ημέρα της εβδομάδας',
        'weekend day'                   : 'ημέρα από το Σαββατοκύριακο',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Επανάληψη Συμβάντος',
        Cancel              : 'Ακύρωση',
        Save                : 'Αποθήκευση',
        Frequency           : 'Συχνότητα',
        Every               : 'Κάθε',
        DAILYintervalUnit   : 'ημέρα(-ες)',
        WEEKLYintervalUnit  : 'εβδομάδα(-ες)',
        MONTHLYintervalUnit : 'μήνα(-ες)',
        YEARLYintervalUnit  : 'έτος(-η)',
        Each                : 'Κάθε',
        'On the'            : 'Την',
        'End repeat'        : 'Τέλος επανάληψης',
        'time(s)'           : 'φορά(-ές)'
    },

    RecurrenceDaysCombo : {
        day           : 'ημέρα',
        weekday       : 'ημέρα της εβδομάδας',
        'weekend day' : 'ημέρα από το Σαββατοκύριακο'
    },

    RecurrencePositionsCombo : {
        position1    : 'πρώτη',
        position2    : 'δεύτερη',
        position3    : 'τρίτη',
        position4    : 'τέταρτη',
        position5    : 'πέμπτη',
        'position-1' : 'τελευταία'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Ποτέ',
        After     : 'Μετά από',
        'On date' : 'Κατά την ημερομηνία'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Χωρίς επανάληψη',
        Daily   : 'Καθημερινά',
        Weekly  : 'Εβδομαδιαίως',
        Monthly : 'Μηνιαίως',
        Yearly  : 'Ετησίως'
    },

    RecurrenceCombo : {
        None   : 'Καμία',
        Custom : 'Προσαρμοσμένη...'
    },

    Summary : {
        'Summary for' : date => `Σύνοψη για ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Πλήρες πρόγραμμα',
        currentview  : 'Ορατό πρόγραμμα',
        daterange    : 'Εύρος ημερομηνιών',
        completedata : 'Ολοκλήρωση προγραμματισμού (για όλα τα συμβάντα)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Εύρος προγραμματισμού',
        'Export from'    : 'Από',
        'Export to'      : 'Έως'
    },

    ExcelExporter : {
        'No resource assigned' : 'Δεν υπάρχουν εκχωρημένοι πόροι'
    },

    CrudManagerView : {
        serverResponseLabel : 'Απόκριση διακομιστή:'
    },

    DurationColumn : {
        Duration : 'Διάρκεια'
    }
};

export default LocaleHelper.publishLocale(locale);
