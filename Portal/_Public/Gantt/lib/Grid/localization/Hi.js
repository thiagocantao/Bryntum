import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Hi.js';

const emptyString = new String();

const locale = {

    localeName : 'Hi',
    localeDesc : 'हिन्दी',
    localeCode : 'hi',

    ColumnPicker : {
        column          : 'कॉलम',
        columnsMenu     : 'कॉलम',
        hideColumn      : 'कॉलम छिपाएं',
        hideColumnShort : 'छिपाएं',
        newColumns      : 'नए कॉलम'
    },

    Filter : {
        applyFilter   : 'फिल्टर लागू करें',
        filter        : 'फिल्टर',
        editFilter    : 'फिल्टर संपादित करें',
        on            : 'पर',
        before        : 'पहले',
        after         : 'बाद में',
        equals        : 'के बराबर',
        lessThan      : 'से कम',
        moreThan      : 'से अधिक',
        removeFilter  : 'फिल्टर हटाएं',
        disableFilter : 'फिल्टर अक्षम करें'
    },

    FilterBar : {
        enableFilterBar  : 'फिल्टर बार दिखाएं',
        disableFilterBar : 'फिल्टर बार छिपाएं'
    },

    Group : {
        group                : 'समूहित करें',
        groupAscending       : 'आरोही समूहित करें',
        groupDescending      : 'अवरोही समूहित करें',
        groupAscendingShort  : 'आरोही',
        groupDescendingShort : 'अवरोही',
        stopGrouping         : 'समूहित करना बंद करें',
        stopGroupingShort    : 'बंद करें'
    },

    HeaderMenu : {
        moveBefore     : text => `"${text}" से पहले ले जाएं`,
        moveAfter      : text => `"${text}" के बाद ले जाएं`,
        collapseColumn : 'कॉलम कोलैप्स करें',
        expandColumn   : 'कॉलम फैलाएं'
    },

    ColumnRename : {
        rename : 'नाम बदलें'
    },

    MergeCells : {
        mergeCells  : 'सेल मर्ज करें',
        menuTooltip : 'इस कॉलम द्वारा छांटते समय समान मान वाले सेल मर्ज करें'
    },

    Search : {
        searchForValue : 'मान की खोज करें'
    },

    Sort : {
        sort                   : 'छांटें',
        sortAscending          : 'आरोही छांटें',
        sortDescending         : 'अवरोही छांटें',
        multiSort              : 'एकाधिक छांटें',
        removeSorter           : 'छांटने वाला निकालें',
        addSortAscending       : 'आरोही छांटने वाला जोड़ें',
        addSortDescending      : 'अवरोही छांटने वाला जोड़ें',
        toggleSortAscending    : 'आरोही में बदलें',
        toggleSortDescending   : 'अवरोही में बदलें',
        sortAscendingShort     : 'आरोही',
        sortDescendingShort    : 'अवरोही',
        removeSorterShort      : 'हटाएं',
        addSortAscendingShort  : '+ आरोही',
        addSortDescendingShort : '+ अवरोही'
    },

    Split : {
        split        : 'विभाजित',
        unsplit      : 'अविभाजित',
        horizontally : 'क्षैतिजता से',
        vertically   : 'लंबवत',
        both         : 'दोनों'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} कॉलम. ` : ''}प्रसंग मेनू के लिए SPACE पर टैप करें${column.sortable ? ', सॉर्ट करने के लिए ENTER' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'पंक्ति चयन टॉगल करें',
        toggleSelection : 'पूरे डेटासेट का चयन टॉगल करें'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? ` रेटिंग : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'डेटा लोडिंग विफल रहा!',
        syncFailedMessage  : 'डेटा सिक्रोनाइजेशन विफल रहा!',
        unspecifiedFailure : 'अनिर्दिष्ट विफलता',
        networkFailure     : 'नेटवर्क त्रुटि',
        parseFailure       : 'सर्वर प्रतिक्रिया पार्स करना विफल रहा',
        serverResponse     : 'सर्वर प्रतिक्रिया:',
        noRows             : 'दर्शाने के लिए कोई रिकॉर्ड नहीं है',
        moveColumnLeft     : 'बाएं सेक्शन पर जाएं',
        moveColumnRight    : 'दाएं सेक्शन पर जाएं',
        moveColumnTo       : region => `कॉलम को ${region} पर ले जाएं`
    },

    CellMenu : {
        removeRow : 'हटाएं'
    },

    RowCopyPaste : {
        copyRecord  : 'कॉपी करें',
        cutRecord   : 'कट करें',
        pasteRecord : 'पेस्ट करें',
        rows        : 'पंक्तियां',
        row         : 'पंक्ति'
    },

    CellCopyPaste : {
        copy  : 'कॉपी करें',
        cut   : 'कट करें',
        paste : 'पेस्ट करें'
    },

    PdfExport : {
        'Waiting for response from server' : 'सर्वर प्रतिक्रिया की प्रतीक्षा कर रहा है...',
        'Export failed'                    : 'एक्सपोर्ट विफल रहा',
        'Server error'                     : 'सर्वर त्रुटि',
        'Generating pages'                 : 'पेजों को जनरेट कर रहा है...',
        'Click to abort'                   : 'रद्द करें'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'सेटिंग्स निर्यात करें',
        export         : 'एक्सपोर्ट करें',
        exporterType   : 'पेजिनेशन नियंत्रित करें',
        cancel         : 'रद्द करें',
        fileFormat     : 'फाइल फार्मेट',
        rows           : 'पंक्तियां',
        alignRows      : 'पंक्तियां संरेखित करें',
        columns        : 'कॉलम',
        paperFormat    : 'पेपर फार्मेट',
        orientation    : 'अभिविन्यास',
        repeatHeader   : 'हेडर दोहराएं'
    },

    ExportRowsCombo : {
        all     : 'सभी पंक्तियां',
        visible : 'दिखने वाली पंक्तियां'
    },

    ExportOrientationCombo : {
        portrait  : 'पोर्ट्रेट',
        landscape : 'लैंडस्केप'
    },

    SinglePageExporter : {
        singlepage : 'एकल पेज'
    },

    MultiPageExporter : {
        multipage     : 'एकाधिक पेज',
        exportingPage : ({ currentPage, totalPages }) => `निर्यात पृष्ठ ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'एकाधिक पेज (लंबवत)',
        exportingPage     : ({ currentPage, totalPages }) => `निर्यात पृष्ठ ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'लोडिंग',
        expand   : 'फैलाएं',
        collapse : 'समेटें'
    },

    TreeGroup : {
        group                  : 'द्वारा समूह बनाएं',
        stopGrouping           : 'समूह बनाना बंद करें',
        stopGroupingThisColumn : 'इस स्तंभ का समूह विस्थापित न करें'
    }
};

export default LocaleHelper.publishLocale(locale);
