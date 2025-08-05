import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/El.js';

const emptyString = new String();

const locale = {

    localeName : 'El',
    localeDesc : 'Ελληνικά',
    localeCode : 'el',

    ColumnPicker : {
        column          : 'Στήλη',
        columnsMenu     : 'Στήλες',
        hideColumn      : 'Απόκρυψη στήλης',
        hideColumnShort : 'Απόκρυψη',
        newColumns      : 'Νέες στήλες'
    },

    Filter : {
        applyFilter   : 'Εφαρμογή φίλτρου',
        filter        : 'Φίλτρο',
        editFilter    : 'Επεξεργασία φίλτρου',
        on            : 'Ενεργό',
        before        : 'Πριν',
        after         : 'Μετά',
        equals        : 'Ίσο με',
        lessThan      : 'Μικρότερο από',
        moreThan      : 'Μεγαλύτερο από',
        removeFilter  : 'Κατάργηση φίλτρου',
        disableFilter : 'Απενεργοποίηση φίλτρου'
    },

    FilterBar : {
        enableFilterBar  : 'Εμφάνιση μπάρας φίλτρου',
        disableFilterBar : 'Απόκρυψη μπάρας φίλτρου'
    },

    Group : {
        group                : 'Ομάδα',
        groupAscending       : 'Αύξουσα σειρά ως προς την ομάδα',
        groupDescending      : 'Φθίνουσα σειρά ως προς την ομάδα',
        groupAscendingShort  : 'Αύξουσα σειρά',
        groupDescendingShort : 'Φθίνουσα σειρά',
        stopGrouping         : 'Διακοπή ομαδοποίησης',
        stopGroupingShort    : 'Διακοπή'
    },

    HeaderMenu : {
        moveBefore     : text => `Μετακίνηση πριν από "${text}"`,
        moveAfter      : text => `Μετακίνηση μετά από "${text}"`,
        collapseColumn : 'Σύμπτυξη στήλης',
        expandColumn   : 'Ανάπτυξη στήλης'
    },

    ColumnRename : {
        rename : 'Μετονομασία'
    },

    MergeCells : {
        mergeCells  : 'Συγχώνευση κελιών',
        menuTooltip : 'Συγχώνευση κελιών με την ίδια τιμή κατά την ταξινόμηση με βάση αυτή τη στήλη'
    },

    Search : {
        searchForValue : 'Αναζήτηση τιμής'
    },

    Sort : {
        sort                   : 'Ταξινόμηση',
        sortAscending          : 'Αύξουσα ταξινόμηση',
        sortDescending         : 'Φθίνουσα ταξινόμηση',
        multiSort              : 'Πολλαπλή ταξινόμηση',
        removeSorter           : 'Αφαίρεση ταξινομητή',
        addSortAscending       : 'Προσθήκη αύξοντα ταξινομητή',
        addSortDescending      : 'Προσθήκη φθίνοντα ταξινομητή',
        toggleSortAscending    : 'Αλλαγή σε αύξουσα',
        toggleSortDescending   : 'Αλλαγή σε φθίνουσα',
        sortAscendingShort     : 'Αύξουσα',
        sortDescendingShort    : 'Φθίνουσα',
        removeSorterShort      : 'Κατάργηση',
        addSortAscendingShort  : '+ Αύξουσα',
        addSortDescendingShort : '+ Φθίνουσα'
    },

    Split : {
        split        : 'Διαίρεση',
        unsplit      : 'Μη διαίρεση',
        horizontally : 'Οριζόντια',
        vertically   : 'Κατακόρυφα',
        both         : 'Και τα δύο'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} στήλη. ` : ''}SPACE για το μενού περιβάλλοντος${column.sortable ? ', πατήστε ENTER για ταξινόμηση' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Εναλλαγή επιλογής γραμμής',
        toggleSelection : 'Εναλλαγή επιλογής ολόκληρου του συνόλου δεδομένων'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `εκτίμηση : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Η φόρτωση δεδομένων απέτυχε!',
        syncFailedMessage  : 'Ο συγχρονισμός δεδομένων απέτυχε!',
        unspecifiedFailure : 'Απροσδιόριστο σφάλμα',
        networkFailure     : 'Σφάλμα δικτύου',
        parseFailure       : 'Αποτυχία ανάλυσης απόκρισης διακομιστή',
        serverResponse     : 'Απόκριση διακομιστή:',
        noRows             : 'Δεν υπάρχουν εγγραφές προς προβολή',
        moveColumnLeft     : 'Μετακίνηση στο αριστερό τμήμα',
        moveColumnRight    : 'Μετακίνηση στο δεξιό τμήμα',
        moveColumnTo       : region => `Μετακίνηση στήλης σε ${region}`
    },

    CellMenu : {
        removeRow : 'Διαγραφή'
    },

    RowCopyPaste : {
        copyRecord  : 'Αντιγραφή',
        cutRecord   : 'Αποκοπή',
        pasteRecord : 'Επικόλληση',
        rows        : 'σειρές',
        row         : 'σειρά'
    },

    CellCopyPaste : {
        copy  : 'Αντιγραφή',
        cut   : 'Αποκοπή',
        paste : 'Επικόλληση'
    },

    PdfExport : {
        'Waiting for response from server' : 'Αναμονή απόκρισης από τον διακομιστή...',
        'Export failed'                    : 'Η εξαγωγή απέτυχε',
        'Server error'                     : 'Σφάλμα διακομιστή',
        'Generating pages'                 : 'Δημιουργία σελίδων...',
        'Click to abort'                   : 'Ακύρωση'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Εξαγωγή ρυθμίσεων',
        export         : 'Εξαγωγή',
        exporterType   : 'Έλεγχος σελιδοποίησης',
        cancel         : 'Ακύρωση',
        fileFormat     : 'Μορφοποίηση αρχείου',
        rows           : 'Γραμμές',
        alignRows      : 'Ευθυγράμμιση γραμμών',
        columns        : 'Στήλες',
        paperFormat    : 'Διαμόρφωση χαρτιού',
        orientation    : 'Προσανατολισμός',
        repeatHeader   : 'Επανάληψη κεφαλίδας'
    },

    ExportRowsCombo : {
        all     : 'Όλες οι γραμμές',
        visible : 'Ορατές γραμμές'
    },

    ExportOrientationCombo : {
        portrait  : 'Πορτρέτο',
        landscape : 'Τοπίο'
    },

    SinglePageExporter : {
        singlepage : 'Μονή σελίδα'
    },

    MultiPageExporter : {
        multipage     : 'Πολλαπλές σελίδες',
        exportingPage : ({ currentPage, totalPages }) => `Εξαγωγή της σελίδας ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Πολλαπλές σελίδες (κάθετο)',
        exportingPage     : ({ currentPage, totalPages }) => `Εξαγωγή της σελίδας ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Φόρτωση',
        expand   : 'Ανάπτυξη',
        collapse : 'Σύμπτυξη'
    },

    TreeGroup : {
        group                  : 'Ομαδοποίηση κατά',
        stopGrouping           : 'Διακοπή ομαδοποίησης',
        stopGroupingThisColumn : 'Απο-ομαδοποίηση στήλης'
    }
};

export default LocaleHelper.publishLocale(locale);
