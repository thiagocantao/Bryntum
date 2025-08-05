import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/He.js';

const emptyString = new String();

const locale = {

    localeName : 'He',
    localeDesc : 'עִברִית',
    localeCode : 'he',

    ColumnPicker : {
        column          : 'עמודה',
        columnsMenu     : 'עמודות',
        hideColumn      : 'הסתר עמודה',
        hideColumnShort : 'הסתר',
        newColumns      : 'עמודות חדשות'
    },

    Filter : {
        applyFilter   : 'השתמש בפילטר',
        filter        : 'פילטר',
        editFilter    : 'ערוך פילטר',
        on            : 'ב-',
        before        : 'לפני',
        after         : 'אחרי',
        equals        : 'שווה',
        lessThan      : 'קטן מ',
        moreThan      : 'גדול מ',
        removeFilter  : 'הסר פילטר',
        disableFilter : 'נטרל פילטר'
    },

    FilterBar : {
        enableFilterBar  : 'הצג סרגל פילטרים',
        disableFilterBar : 'הסתר סרגל פילטרים'
    },

    Group : {
        group                : 'קבוצה',
        groupAscending       : 'קבץ בסדר עולה',
        groupDescending      : 'קבץ בסדר יורד',
        groupAscendingShort  : 'עלייה',
        groupDescendingShort : 'ירידה',
        stopGrouping         : 'עצירת תהליך קיבוץ',
        stopGroupingShort    : 'עצור'
    },

    HeaderMenu : {
        moveBefore     : text => `"${text}" הזז ללפני`,
        moveAfter      : text => `"${text}" הזז לאחרי`,
        collapseColumn : 'כווץ עמודה',
        expandColumn   : 'הרחב עמודה'
    },

    ColumnRename : {
        rename : 'שינוי שם'
    },

    MergeCells : {
        mergeCells  : 'מזג תאים',
        menuTooltip : 'מזג תאים עם אותו ערך כאשר ממיינים באמצעות עמודה זו'
    },

    Search : {
        searchForValue : 'חפש ערך'
    },

    Sort : {
        sort                   : 'מיין',
        sortAscending          : 'מיין בסדר עולה',
        sortDescending         : 'מיין בסדר יורד',
        multiSort              : 'מיון מרובה',
        removeSorter           : 'הסר ממיין',
        addSortAscending       : 'הוסף ממיין בסדר עולה',
        addSortDescending      : 'הוסף ממיין בסדר יורד',
        toggleSortAscending    : 'שנה לסדר עולה',
        toggleSortDescending   : 'שנה לסדר יורד',
        sortAscendingShort     : 'עלייה',
        sortDescendingShort    : 'ירידה',
        removeSorterShort      : 'הסר',
        addSortAscendingShort  : '+ עלייה',
        addSortDescendingShort : '+ ירידה'
    },

    Split : {
        split        : 'חלק',
        unsplit      : 'אל תחלק',
        horizontally : 'באופן אופקי',
        vertically   : 'באופן אנכי',
        both         : 'שניהם'
    },

    Column : {
        columnLabel : column => `${column.text ? `.עמודה ${column.text}` : ''} לתפריט ההקשר SPACE הקש על${column.sortable ? ', על מקש ENTER למיון' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'החלף בין מצבי בחירת שורות',
        toggleSelection : 'החלף בין מצבי בחירת מלוא אוסף הנתונים'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `: ${column.location.record.get(column.field) || 0} : דירוג'` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'טעינת הנתונים נכשלה!',
        syncFailedMessage  : 'סנכרון הנתונים נכשל!',
        unspecifiedFailure : 'תקלה בלתי-מוגדרת',
        networkFailure     : 'שגיאת רשת',
        parseFailure       : 'תקלה בניתוח תגובת השרת',
        serverResponse     : ':תגובת השרת',
        noRows             : 'אין רשומות להצגה',
        moveColumnLeft     : 'הזז למקטע השמאלי',
        moveColumnRight    : 'הזז למקטע הימני',
        moveColumnTo       : region => `${region}-הזז את העמודה ל`
    },

    CellMenu : {
        removeRow : 'מחוק'
    },

    RowCopyPaste : {
        copyRecord  : 'העתק',
        cutRecord   : 'גזור',
        pasteRecord : 'הדבק',
        rows        : 'שורות',
        row         : 'שורה'
    },

    CellCopyPaste : {
        copy  : 'העתק',
        cut   : 'גזור',
        paste : 'הדבק'
    },

    PdfExport : {
        'Waiting for response from server' : '...ממתין לתגובה מהשרת',
        'Export failed'                    : 'הייצוא נכשל',
        'Server error'                     : 'שגיאת שרת',
        'Generating pages'                 : '...יוצר עמודים',
        'Click to abort'                   : 'ביטול'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'ייצוא הגדרות',
        export         : 'ייצוא',
        exporterType   : 'שליטה על דפדוף',
        cancel         : 'ביטול',
        fileFormat     : 'פורמט קובץ',
        rows           : 'שורות',
        alignRows      : 'יישור שורות',
        columns        : 'עמודות',
        paperFormat    : 'פורמט נייר',
        orientation    : 'אוריינטציה',
        repeatHeader   : 'חזרה על כותרת'
    },

    ExportRowsCombo : {
        all     : 'כל השורות',
        visible : 'שורות נראות לעין'
    },

    ExportOrientationCombo : {
        portrait  : 'דיוקן',
        landscape : 'נוף'
    },

    SinglePageExporter : {
        singlepage : 'עמוד יחיד'
    },

    MultiPageExporter : {
        multipage     : 'מספר עמודים',
        exportingPage : ({ currentPage, totalPages }) => `${totalPages}/${currentPage} מייצא עמוד`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'מספר עמודים (אנכי)',
        exportingPage     : ({ currentPage, totalPages }) => `${totalPages}/${currentPage} מייצא עמוד`
    },

    RowExpander : {
        loading  : 'טוען',
        expand   : 'הרחב',
        collapse : 'מזער'
    },

    TreeGroup : {
        group                  : 'קיבוץ לפי',
        stopGrouping           : 'הפסק קיבוץ',
        stopGroupingThisColumn : 'בטל קיבוץ עמודה זו'
    }
};

export default LocaleHelper.publishLocale(locale);
