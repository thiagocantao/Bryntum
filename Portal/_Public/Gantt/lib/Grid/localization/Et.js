import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Et.js';

const emptyString = new String();

const locale = {

    localeName : 'Et',
    localeDesc : 'Eesti keel',
    localeCode : 'et',

    ColumnPicker : {
        column          : 'Veerg',
        columnsMenu     : 'Veerud',
        hideColumn      : 'Peida veerg',
        hideColumnShort : 'Peida',
        newColumns      : 'Uued veerud'
    },

    Filter : {
        applyFilter   : 'Kohalda filtrit',
        filter        : 'Filter',
        editFilter    : 'Redigeeri filtrit',
        on            : 'Kuupäeval',
        before        : 'Enne',
        after         : 'Pärast',
        equals        : 'Võrdub',
        lessThan      : 'Vähem kui',
        moreThan      : 'Rohkem kui',
        removeFilter  : 'Eemalda filter',
        disableFilter : 'Keela filter'
    },

    FilterBar : {
        enableFilterBar  : 'Näita filtritulpa',
        disableFilterBar : 'Peida filtritulp'
    },

    Group : {
        group                : 'Grupeeri',
        groupAscending       : 'Grupeeri kasvavalt',
        groupDescending      : 'Grupeeri kahanevalt',
        groupAscendingShort  : 'Kasvav',
        groupDescendingShort : 'Kahanev',
        stopGrouping         : 'Peata grupeerimine',
        stopGroupingShort    : 'Peata'
    },

    HeaderMenu : {
        moveBefore     : text => `Liiguta enne "${text}"`,
        moveAfter      : text => `Liiguta pärast "${text}"`,
        collapseColumn : 'Koonda veerg',
        expandColumn   : 'Laienda veergu'
    },

    ColumnRename : {
        rename : 'Nimeta ümber'
    },

    MergeCells : {
        mergeCells  : 'Ühenda lahtrid',
        menuTooltip : 'Ühenda sama väärtusega lahtrid, kui sorteeritakse selle veeru järgi'
    },

    Search : {
        searchForValue : 'Otsi väärtust'
    },

    Sort : {
        sort                   : 'Sordi',
        sortAscending          : 'Sordi kasvavalt',
        sortDescending         : 'Sordi kahanevalt',
        multiSort              : 'Multisortimine',
        removeSorter           : 'Eemalda sortija',
        addSortAscending       : 'Lisa kasvav sortija',
        addSortDescending      : 'Lisa kahanev sortija',
        toggleSortAscending    : 'Muuda kasvavaks',
        toggleSortDescending   : 'Muuda kahanevaks',
        sortAscendingShort     : 'Kasvav',
        sortDescendingShort    : 'Kahanev',
        removeSorterShort      : 'Eemalda',
        addSortAscendingShort  : '+ Kasvav',
        addSortDescendingShort : '+ Kahanev'
    },

    Split : {
        split        : 'Jaga',
        unsplit      : 'Lõpeta jagamine',
        horizontally : 'Horisontaalselt',
        vertically   : 'Vertikaalselt',
        both         : 'Mõlemad'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} veerg. ` : ''}TÜHIKULAADI - kontekstimenüü jaoks${column.sortable ? ', ENTER - et sortida' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Lülita sisse reavalik',
        toggleSelection : 'Lülita sisse kogu andmekogumi valik'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `hinnang : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Andmete laadimine nurjus!',
        syncFailedMessage  : 'Andmete sünrkoonimine nurjus!',
        unspecifiedFailure : 'Täpsustamata tõrge',
        networkFailure     : 'Võrgu viga',
        parseFailure       : 'Serveri vastuse parsimine nurjus',
        serverResponse     : 'Serveri vastus:',
        noRows             : 'Kuvatavad kirjed puuduvad',
        moveColumnLeft     : 'Liiguta vasakpoolsesse jaotisesse',
        moveColumnRight    : 'Liiguta parempoolsesse jaotisesse',
        moveColumnTo       : region => `Liiguta veerg kohta ${region}`
    },

    CellMenu : {
        removeRow : 'Kustuta'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopeeri',
        cutRecord   : 'Lõika',
        pasteRecord : 'Kleebi',
        rows        : 'rida',
        row         : 'rida'
    },

    CellCopyPaste : {
        copy  : 'Kopeeri',
        cut   : 'Lõika',
        paste : 'Kleebi'
    },

    PdfExport : {
        'Waiting for response from server' : 'Serveri vastuse ootamine...',
        'Export failed'                    : 'Eksport nurjus',
        'Server error'                     : 'Serveri viga',
        'Generating pages'                 : 'Lehekülgede loomine...',
        'Click to abort'                   : 'Tühista'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Ekspordi seaded',
        export         : 'Eksport',
        exporterType   : 'Kontrolli leheküljestamist',
        cancel         : 'Tühista',
        fileFormat     : 'Failivorming',
        rows           : 'Read',
        alignRows      : 'Joonda read',
        columns        : 'Veerud',
        paperFormat    : 'Paberi vorming',
        orientation    : 'Paigutus',
        repeatHeader   : 'Korda päist'
    },

    ExportRowsCombo : {
        all     : 'Kõik read',
        visible : 'Nähtavad read'
    },

    ExportOrientationCombo : {
        portrait  : 'Püstpaigutus',
        landscape : 'Rõhtpaigutus'
    },

    SinglePageExporter : {
        singlepage : 'Üks lehekülg'
    },

    MultiPageExporter : {
        multipage     : 'Mitu lehekülge',
        exportingPage : ({ currentPage, totalPages }) => `Ekspordi lehekülg ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Mitu lehekülge (vertikaalne)',
        exportingPage     : ({ currentPage, totalPages }) => `Ekspordi lehekülg ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Laadimine',
        expand   : 'Laienda',
        collapse : 'Koonda'
    },

    TreeGroup : {
        group                  : 'Rühmita',
        stopGrouping           : 'Lõpeta rühmitamine',
        stopGroupingThisColumn : 'Eemalda rühmitamine veerult'
    }
};

export default LocaleHelper.publishLocale(locale);
