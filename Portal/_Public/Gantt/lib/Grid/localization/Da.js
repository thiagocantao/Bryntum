import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Da.js';

const emptyString = new String();

const locale = {

    localeName : 'Da',
    localeDesc : 'Dansk',
    localeCode : 'da',

    ColumnPicker : {
        column          : 'Kolonne',
        columnsMenu     : 'Kolonner',
        hideColumn      : 'Skjul kolonne',
        hideColumnShort : 'Skjul',
        newColumns      : 'Nye kolonner'
    },

    Filter : {
        applyFilter   : 'Anvend filter',
        filter        : 'Filter',
        editFilter    : 'Rediger filter',
        on            : 'On',
        before        : 'Før',
        after         : 'Efter',
        equals        : 'Lige med',
        lessThan      : 'Mindre end',
        moreThan      : 'Mere end',
        removeFilter  : 'Fjern filteret',
        disableFilter : 'Deaktiver filter'
    },

    FilterBar : {
        enableFilterBar  : 'Vis filterbjælke',
        disableFilterBar : 'skjul filterbjælken'
    },

    Group : {
        group                : 'Gruppér',
        groupAscending       : 'Gruppér stigende',
        groupDescending      : 'Gruppér faldende',
        groupAscendingShort  : 'stigende',
        groupDescendingShort : 'faldende',
        stopGrouping         : 'Stop gruppering',
        stopGroupingShort    : 'Stop'
    },

    HeaderMenu : {
        moveBefore     : text => `Flyt før "${text}"`,
        moveAfter      : text => `Flyt efter "${text}"`,
        collapseColumn : 'Skjul kolonne',
        expandColumn   : 'Skjul kolonne'
    },

    ColumnRename : {
        rename : 'Omdøb'
    },

    MergeCells : {
        mergeCells  : 'Flet celler',
        menuTooltip : 'Flet celler med samme værdi, når de sorteres efter denne kolonne'
    },

    Search : {
        searchForValue : 'Søg efter værdi'
    },

    Sort : {
        sort                   : 'Sortér',
        sortAscending          : 'Sorter stigende',
        sortDescending         : 'Sorter faldende',
        multiSort              : 'Multi sortering',
        removeSorter           : 'Fjern sorteringsenheden',
        addSortAscending       : 'Tilføj stigende sortering',
        addSortDescending      : 'Tilføj faldende sortering',
        toggleSortAscending    : 'Skift til stigende',
        toggleSortDescending   : 'Skift til faldende',
        sortAscendingShort     : 'Stigende',
        sortDescendingShort    : 'Faldende',
        removeSorterShort      : 'Fjerne',
        addSortAscendingShort  : '+ Stigende',
        addSortDescendingShort : '+ Faldende'
    },

    Split : {
        split        : 'Opdel',
        unsplit      : 'Ikke opdelt',
        horizontally : 'Vandret',
        vertically   : 'Lodret',
        both         : 'Begge'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} kolonne. ` : ''}MELLEMRUM for kontekstmenu${column.sortable ? ',ENTER for at sortere' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Skift rækkevalg',
        toggleSelection : 'Skift valg af hele datasættet'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? ` bedømmelse : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Dataindlæsning mislykkedes!',
        syncFailedMessage  : 'Datasynkronisering mislykkedes!',
        unspecifiedFailure : 'Uspecificeret fejl',
        networkFailure     : 'Netværksfejl',
        parseFailure       : 'Kunne ikke parse serversvar',
        serverResponse     : 'Serversvar:',
        noRows             : 'Ingen poster at vise',
        moveColumnLeft     : 'Flyt til venstre sektion',
        moveColumnRight    : 'Flyt til højre sektion',
        moveColumnTo       : region => `Flyt kolonne til ${region}`
    },

    CellMenu : {
        removeRow : 'Slet'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopi',
        cutRecord   : 'klip',
        pasteRecord : 'sæt ind',
        rows        : 'rækker',
        row         : 'række'
    },

    CellCopyPaste : {
        copy  : 'Kopi',
        cut   : 'Skære',
        paste : 'Sæt ind'
    },

    PdfExport : {
        'Waiting for response from server' : 'Venter på svar fra serveren...',
        'Export failed'                    : 'Eksporten mislykkedes',
        'Server error'                     : 'Serverfejl',
        'Generating pages'                 : 'Generering af sider...',
        'Click to abort'                   : 'Afbestille'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Eksporter indstillinger',
        export         : 'Eksport',
        exporterType   : 'Styr paginering',
        cancel         : 'Afbestille',
        fileFormat     : 'Filformat',
        rows           : 'Rækker',
        alignRows      : 'Juster rækker',
        columns        : 'Kolonner',
        paperFormat    : 'Papirformat',
        orientation    : 'Orientering',
        repeatHeader   : 'Gentag overskriften'
    },

    ExportRowsCombo : {
        all     : 'Alle rækker',
        visible : 'Synlige rækker'
    },

    ExportOrientationCombo : {
        portrait  : 'Portræt',
        landscape : 'Landskab'
    },

    SinglePageExporter : {
        singlepage : 'Enkelt side'
    },

    MultiPageExporter : {
        multipage     : 'Flere sider',
        exportingPage : ({ currentPage, totalPages }) => `Eksporterer side ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Flere sider (lodret)',
        exportingPage     : ({ currentPage, totalPages }) => `Eksporterer side ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Indlæser',
        expand   : 'Udvide',
        collapse : 'Samle'
    },

    TreeGroup : {
        group                  : 'Gruppér efter',
        stopGrouping           : 'Stop gruppering',
        stopGroupingThisColumn : 'Fjern gruppe på kolonne'
    }
};

export default LocaleHelper.publishLocale(locale);
