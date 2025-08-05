import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Ar.js';

const emptyString = new String();

const locale = {

    localeName : 'Ar',
    localeDesc : 'اللغة العربية',
    localeCode : 'ar',

    ColumnPicker : {
        column          : 'عمود',
        columnsMenu     : 'أعمدة',
        hideColumn      : 'إخفاء عمود',
        hideColumnShort : 'إخفاء',
        newColumns      : 'أعمدة جديدة'
    },

    Filter : {
        applyFilter   : 'تطبيق التصفية',
        filter        : 'التصفية',
        editFilter    : 'تعديل التصفية',
        on            : 'في',
        before        : 'قبل',
        after         : 'بعد',
        equals        : 'يساوي',
        lessThan      : 'أقل من',
        moreThan      : 'أكثر من',
        removeFilter  : 'إزالة التصفية',
        disableFilter : 'تعطيل عامل التصفية'
    },

    FilterBar : {
        enableFilterBar  : 'عرض شريط التصفية',
        disableFilterBar : 'إخفاء شريط التصفية'
    },

    Group : {
        group                : 'المجموعة',
        groupAscending       : 'المجموعة تصاعديًا',
        groupDescending      : 'المجموعة تنازليًا',
        groupAscendingShort  : 'تصاعديًا',
        groupDescendingShort : 'تنازليًا',
        stopGrouping         : 'إيقاف تكوين مجموعات',
        stopGroupingShort    : 'إإيقاف'
    },

    HeaderMenu : {
        moveBefore     : text => `${text}التحرك قبل `,
        moveAfter      : text => `${text}التحرك بعد `,
        collapseColumn : 'طي العمود',
        expandColumn   : 'تمديد العمود'
    },

    ColumnRename : {
        rename : 'إعادة التسمية'
    },

    MergeCells : {
        mergeCells  : 'دمج  الخانات',
        menuTooltip : 'دمج الخانات بنفس القيمة عند فرزها حسب هذا العمود'
    },

    Search : {
        searchForValue : 'ابحث عن القيمة'
    },

    Sort : {
        sort                   : 'الفرز',
        sortAscending          : 'الفرز تصاعديًا',
        sortDescending         : 'الفرز تنازليًا',
        multiSort              : 'فرز متعدد',
        removeSorter           : 'إزالة الفارز',
        addSortAscending       : 'إضافة فارز تصاعدي',
        addSortDescending      : 'إضافة فارز تنازلي',
        toggleSortAscending    : 'التغيير ليصبح تصاعديًا',
        toggleSortDescending   : 'التغيير ليصبح تنازليًا',
        sortAscendingShort     : 'تصاعديًا',
        sortDescendingShort    : 'تنازليًا',
        removeSorterShort      : 'إزالة',
        addSortAscendingShort  : '+ تصاعديًا',
        addSortDescendingShort : '+ تنازليًا'
    },

    Split : {
        split        : 'تقسيم',
        unsplit      : 'عدم التقسيم',
        horizontally : 'أفقيًا',
        vertically   : 'عموديًا',
        both         : 'كلاهما'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} .عمود ` : ''}لقائمة للسياق SPACE الضغط على${column.sortable ? ', للفرز ENTER' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'تبديل اختيار الصف',
        toggleSelection : 'تبديل اختيار مجموعة البيانات بأكملها'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `${column.location.record.get(column.field) || 0} : تقييم ` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'فشل تحميل البيانا',
        syncFailedMessage  : 'فشل مزامنة البيانات',
        unspecifiedFailure : 'فشل غير محدد',
        networkFailure     : 'خطأ في الشبكة',
        parseFailure       : 'فشل في تحليل استجابة الخادم',
        serverResponse     : 'استجابة الخادم:',
        noRows             : 'لا توجد سجلات لعرضها',
        moveColumnLeft     : 'نقل إلى القسم الأيسر',
        moveColumnRight    : 'نقل إلى القسم الأيمن',
        moveColumnTo       : region => `${region} نقل العمود إلى`
    },

    CellMenu : {
        removeRow : 'حذف'
    },

    RowCopyPaste : {
        copyRecord  : 'نسخ',
        cutRecord   : 'قص',
        pasteRecord : 'لصق',
        rows        : 'صفوف',
        row         : 'صف'
    },

    CellCopyPaste : {
        copy  : 'نسخ',
        cut   : 'قص',
        paste : 'لصق'
    },

    PdfExport : {
        'Waiting for response from server' : '...في انتظار استجابة الخادم',
        'Export failed'                    : 'فشل التصدير',
        'Server error'                     : 'خطأ في الخادم',
        'Generating pages'                 : '...جارٍ توليد صفحات',
        'Click to abort'                   : 'إلغاء'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'تصدير الإعدادات',
        export         : 'تصدير',
        exporterType   : 'التحكم في ترقيم الصفحات',
        cancel         : 'إلغاء',
        fileFormat     : 'تنسيق الملف',
        rows           : 'الصفوف',
        alignRows      : 'محاذاة الصفوف',
        columns        : 'الأعمدة',
        paperFormat    : 'تنسيق الصفحة',
        orientation    : 'الاتجاه',
        repeatHeader   : 'تكرار رأس الصفحة'
    },

    ExportRowsCombo : {
        all     : 'كل الصفوف',
        visible : 'الصفوف المرئية'
    },

    ExportOrientationCombo : {
        portrait  : 'عمودي',
        landscape : 'أفقي'
    },

    SinglePageExporter : {
        singlepage : 'صفحة واحدة'
    },

    MultiPageExporter : {
        multipage     : 'صفحات متعددة',
        exportingPage : ({ currentPage, totalPages }) => `${currentPage}/${totalPages} تصدير الصفحة`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'صفحات متعددة ((عموديًا))',
        exportingPage     : ({ currentPage, totalPages }) => `${currentPage}/${totalPages} تصدير الصفحة`
    },

    RowExpander : {
        loading  : 'تحميل',
        expand   : 'توسيع',
        collapse : 'تصغير'
    },

    TreeGroup : {
        group                  : 'تجميع حسب',
        stopGrouping           : 'إيقاف التجميع',
        stopGroupingThisColumn : 'فك تجميع العمود'
    }
};

export default LocaleHelper.publishLocale(locale);
