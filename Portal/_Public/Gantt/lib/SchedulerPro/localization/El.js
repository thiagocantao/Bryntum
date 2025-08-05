import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/El.js';
import '../../Scheduler/localization/El.js';

const locale = {

    localeName : 'El',
    localeDesc : 'Ελληνικά',
    localeCode : 'el',

    ConstraintTypePicker : {
        none                : 'Κανένας',
        assoonaspossible    : 'Το συντομότερο δυνατόν',
        aslateaspossible    : 'Το αργότερο δυνατόν',
        muststarton         : 'Πρέπει να ξεκινήσει στις',
        mustfinishon        : 'Πρέπει να τελειώσει στις',
        startnoearlierthan  : 'Δεν μπορεί να ξεκινήσει νωρίτερα από τις',
        startnolaterthan    : 'Δεν μπορεί να ξεκινήσει αργότερα από τις',
        finishnoearlierthan : 'Δεν μπορεί να τελειώσει νωρίτερα από τις',
        finishnolaterthan   : 'Δεν μπορεί να τελειώσει αργότερα από τις'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Μπροστά',
        Backward      : 'Προς τα πίσω',
        inheritedFrom : 'Κληρονομήθηκε από',
        enforcedBy    : 'Επιβλήθηκε από'
    },

    CalendarField : {
        'Default calendar' : 'Προεπιλεγμένο ημερολόγιο'
    },

    TaskEditorBase : {
        Information   : 'Πληροφορίες',
        Save          : 'Αποθήκευση',
        Cancel        : 'Ακύρωση',
        Delete        : 'Διαγραφή',
        calculateMask : 'Υπολογισμός...',
        saveError     : 'Δεν είναι δυνατή η αποθήκευση, διορθώστε τυχόν λάθη πρώτα',
        repeatingInfo : 'Προβολή ενός επαναλαμβανόμενου συμβάντος',
        editRepeating : 'Επεξεργασία'
    },

    TaskEdit : {
        'Edit task'            : 'Επεξεργασία διεργασίας',
        ConfirmDeletionTitle   : 'Επιβεβαίωση διαγραφής',
        ConfirmDeletionMessage : 'Είστε σίγουροι ότι επιθυμείτε τη διαγραφή του συγκεκριμένου συμβάντος;'
    },

    GanttTaskEditor : {
        editorWidth : '50em'
    },

    SchedulerTaskEditor : {
        editorWidth : '35em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Γενικά',
        Name         : 'Όνομα',
        Resources    : 'Πόροι',
        '% complete' : '% ολοκληρωμένο',
        Duration     : 'Διάρκεια',
        Start        : 'Έναρξη',
        Finish       : 'Τερματισμός',
        Effort       : 'Effort',
        Preamble     : 'Προοίμιο',
        Postamble    : 'Σύνοψη'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Γενικά',
        Name         : 'Όνομα',
        '% complete' : '% ολοκληρωμένο',
        Duration     : 'Διάρκεια',
        Start        : 'Έναρξη',
        Finish       : 'Τερματισμός',
        Effort       : 'Effort',
        Dates        : 'Ημερομηνίες'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Για προχωρημένους',
        Calendar                   : 'Ημερολόγιο',
        'Scheduling mode'          : 'Λειτουργία προγραμματισμού',
        'Effort driven'            : 'Προγραμματισμός βάσει Προσπάθειας',
        'Manually scheduled'       : 'Χειροκίνητα προγραμματισμένο',
        'Constraint type'          : 'Τύπος περιορισμού',
        'Constraint date'          : 'Ημερομηνία περιορισμού',
        Inactive                   : 'Ανενεργό',
        'Ignore resource calendar' : 'Παράβλεψη ημερολογίου πόρου'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Για προχωρημένους',
        Calendar                   : 'Ημερολόγιο',
        'Scheduling mode'          : 'Λειτουργία προγραμματισμού',
        'Effort driven'            : 'Προγραμματισμός βάσει Προσπάθειας',
        'Manually scheduled'       : 'Χειροκίνητα προγραμματισμένο',
        'Constraint type'          : 'Τύπος περιορισμού',
        'Constraint date'          : 'Ημερομηνία περιορισμού',
        Constraint                 : 'Περιορισμός',
        Rollup                     : 'Μετάβαση στη Σύνοψη',
        Inactive                   : 'Ανενεργό',
        'Ignore resource calendar' : 'Παράβλεψη ημερολογίου πόρου',
        'Scheduling direction'     : 'Κατεύθυνση προγραμματισμού'
    },

    DependencyTab : {
        Predecessors      : 'Προκάτοχοι',
        Successors        : 'Διάδοχοι',
        ID                : 'Αναγνωριστικό',
        Name              : 'Όνομα',
        Type              : 'Τύπος',
        Lag               : 'Καθυστέρηση',
        cyclicDependency  : 'Κυκλική εξάρτηση',
        invalidDependency : 'Μη έγκυρη εξάρτηση'
    },

    NotesTab : {
        Notes : 'Σημειώσεις'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Πόροι',
        Resource  : 'Πόρος',
        Units     : 'Μονάδες'
    },

    RecurrenceTab : {
        title : 'Επανάληψη'
    },

    SchedulingModePicker : {
        Normal           : 'Κανονικό',
        'Fixed Duration' : 'Σταθερή Διάρκεια',
        'Fixed Units'    : 'Σταθερές Μονάδες',
        'Fixed Effort'   : 'Σταθερή Προσπάθεια'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} των {available}</span> που έχουν εκχωρηθεί',
        barTipOnDate          : '<b>{resource}</b> στις {startDate}<br><span class="{cls}">{allocated} των {available}</span> που έχουν εκχωρηθεί',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} των {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} των {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Την {startDate}<br><span class="{cls}">{allocated} των {available}</span> που έχουν εκχωρηθεί:<br>{assignments}',
        plusMore              : '+{value} παραπάνω'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> που έχουν εκχωρηθεί',
        barTipOnDate          : '<b>{event}</b> των {startDate}<br><span class="{cls}">{allocated}</span> που έχουν εκχωρηθεί',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} των {available}</span> που έχουν εκχωρηθεί:<br>{assignments}',
        groupBarTipOnDate     : 'Την {startDate}<br><span class="{cls}">{allocated} των {available}</span> που έχουν εκχωρηθεί:<br>{assignments}',
        plusMore              : '+{value} παραπάνω',
        nameColumnText        : 'Πόρος / Συμβάν'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Ακύρωση αλλαγής και έπειτα καμία ενέργεια',
        schedulingConflict : 'Σύγκρουση προγραμματισμού',
        emptyCalendar      : 'Σφάλμα ρύθμισης παραμέτρων ημερολογίου',
        cycle              : 'Κύκλος προγραμματισμού',
        Apply              : 'Εφαρμογή'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Επιλέξτε μια εξάρτηση:',
        invalidDependencyLabel : 'Υπάρχουν άκυρες εξαρτήσεις που πρέπει να αντιμετωπιστούν:'
    },

    DependencyEdit : {
        Active : 'Ενεργό'
    },

    SchedulerProBase : {
        propagating     : 'Υπολογισμός έργου',
        storePopulation : 'Φόρτωση δεδομένων',
        finalizing      : 'Οριστικοποίηση αποτελεσμάτων'
    },

    EventSegments : {
        splitEvent    : 'Διαίρεση συμβάντος',
        renameSegment : 'Μετονομασία'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Δεν επιτρέπεται η από-εμφώλευση',
        nestingNotAllowed   : 'Δεν επιτρέπεται η εμφώλευση'
    },

    VersionGrid : {
        compare       : 'Σύγκρινε',
        description   : 'Περιγραφή',
        occurredAt    : 'Συνέβη στις',
        rename        : 'Μετονομασία',
        restore       : 'Επαναφορά',
        stopComparing : 'Σταμάτησε τη σύγκριση'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'εργασία',
            AssignmentModel : 'ανάθεση',
            DependencyModel : 'σύνδεσμος',
            ProjectModel    : 'έργο',
            ResourceModel   : 'πόρος',
            other           : 'αντικείμενο'
        },
        entityNamesPlural : {
            TaskModel       : 'εργασίες',
            AssignmentModel : 'αναθέσεις',
            DependencyModel : 'σύνδεσμοι',
            ProjectModel    : 'έργα',
            ResourceModel   : 'πόροι',
            other           : 'αντικείμενα'
        },
        transactionDescriptions : {
            update : 'Ενημέρωση {n} {entities}',
            add    : 'Προσθήκη {n} {entities}',
            remove : 'Κατάργηση {n} {entities}',
            move   : 'Μετακίνηση {n} {entities}',
            mixed  : 'Συνδυασμός {n} {entities}'
        },
        addEntity         : 'Προστέθηκε {type} **{name}**',
        removeEntity      : 'Καταργήθηκε {type} **{name}**',
        updateEntity      : 'Άλλαξε {type} **{name}**',
        moveEntity        : 'Μετακινήθηκε {type} **{name}** από {from} σε {to}',
        addDependency     : 'Προστέθηκε σύνδεσμος από **{from}** σε **{to}**',
        removeDependency  : 'Καταργήθηκε σύνδεσμος από **{from}** σε **{to}**',
        updateDependency  : 'Επεξεργάστηκε σύνδεσμος από **{from}** σε **{to}**',
        addAssignment     : 'Ανατέθηκε **{resource}** σε **{event}**',
        removeAssignment  : 'Καταργήθηκε ανάθεση **{resource}** από **{event}**',
        updateAssignment  : 'Επεξεργάστηκε ανάθεση από **{resource}** σε **{event}**',
        noChanges         : 'Καμία αλλαγή',
        nullValue         : 'καμία',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Αναίρεση αλλαγών',
        redid             : 'Επαναφορά αλλαγών',
        editedTask        : 'Επεξεργάστηκαν οι ιδιότητες εργασίας',
        deletedTask       : 'Διαγράφηκε μια εργασία',
        movedTask         : 'Μετακινήθηκε μια εργασία',
        movedTasks        : 'Μετακινήθηκαν εργασίες'
    }
};

export default LocaleHelper.publishLocale(locale);
