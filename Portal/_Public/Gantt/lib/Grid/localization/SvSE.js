import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/SvSE.js';

const emptyString = new String();

const locale = {

    localeName : 'SvSE',
    localeDesc : 'Svenska',
    localeCode : 'sv-SE',

    ColumnPicker : {
        column          : 'Kolumn',
        columnsMenu     : 'Kolumner',
        hideColumn      : 'Dölj kolumn',
        hideColumnShort : 'Dölj',
        newColumns      : 'Nya kolumner'
    },

    Filter : {
        applyFilter   : 'Använd filter',
        filter        : 'Filter',
        editFilter    : 'Redigera filter',
        on            : 'På',
        before        : 'Före',
        after         : 'Efter',
        equals        : 'Lika med',
        lessThan      : 'Mindre än',
        moreThan      : 'Större än',
        removeFilter  : 'Ta bort filter',
        disableFilter : 'Inaktivera filter'
    },

    FilterBar : {
        enableFilterBar  : 'Visa filterrad',
        disableFilterBar : 'Dölj filterrad'
    },

    Group : {
        group                : 'Gruppera',
        groupAscending       : 'Gruppera stigande',
        groupDescending      : 'Gruppera fallande',
        groupAscendingShort  : 'Stigande',
        groupDescendingShort : 'Fallande',
        stopGrouping         : 'Sluta gruppera',
        stopGroupingShort    : 'Sluta'
    },

    HeaderMenu : {
        moveBefore     : text => `Flytta före "${text}"`,
        moveAfter      : text => `Flytta efter "${text}"`,
        collapseColumn : 'Expandera kolumn',
        expandColumn   : 'Fäll ihop kolumn'
    },

    ColumnRename : {
        rename : 'Byt namn'
    },

    MergeCells : {
        mergeCells  : 'Slå ihop celler',
        menuTooltip : 'Slå ihop celler när kolumnen är sorterad'
    },

    Search : {
        searchForValue : 'Sök efter värde'
    },

    Sort : {
        sort                   : 'Sortera',
        sortAscending          : 'Sortera stigande',
        sortDescending         : 'Sortera fallande',
        multiSort              : 'Multisortering',
        removeSorter           : 'Ta bort sorterare',
        addSortAscending       : 'Lägg till stigande',
        addSortDescending      : 'Lägg till fallande',
        toggleSortAscending    : 'Ändra till stigande',
        toggleSortDescending   : 'Ändra till fallande',
        sortAscendingShort     : 'Stigande',
        sortDescendingShort    : 'Fallande',
        removeSorterShort      : 'Ta bort',
        addSortAscendingShort  : '+ Stigande',
        addSortDescendingShort : '+ Fallande'
    },

    Split : {
        split        : 'Dela',
        unsplit      : 'Odelat',
        horizontally : 'Horisontellt',
        vertically   : 'Vertikalt',
        both         : 'Båda'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} kolumn. ` : ''}SPACE för snabbmenyn${column.sortable ? ', ENTER för att sortera' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Växla radval',
        toggleSelection : 'Växla val av hela datamängden'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `rating : ${column.location.record.get(column.field) || 0} || 0` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Ett fel har uppstått, vänligen försök igen!',
        syncFailedMessage  : 'Datasynkronisering misslyckades!',
        unspecifiedFailure : 'Ospecificerat fel',
        networkFailure     : 'Nätverksfel',
        parseFailure       : 'Det gick inte att bearbeta serversvaret',
        serverResponse     : 'Serversvar:',
        noRows             : 'Inga rader att visa',
        moveColumnLeft     : 'Flytta till vänstra sektionen',
        moveColumnRight    : 'Flytta till högra sektionen',
        moveColumnTo       : region => `Flytta kolumn till ${region}`
    },

    CellMenu : {
        removeRow : 'Ta bort'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopiera',
        cutRecord   : 'Klipp ut',
        pasteRecord : 'Klistra in',
        rows        : 'rader',
        row         : 'rad'
    },

    CellCopyPaste : {
        copy  : 'Kopiera',
        cut   : 'Klipp',
        paste : 'Klistra'
    },

    PdfExport : {
        'Waiting for response from server' : 'Väntar på svar från servern...',
        'Export failed'                    : 'Export misslyckades',
        'Server error'                     : 'Serverfel',
        'Generating pages'                 : 'Genererar sidor...',
        'Click to abort'                   : 'Avbryt'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '13em',
        exportSettings : 'Exportera inställningar',
        export         : 'Exportera',
        exporterType   : 'Styra sidbrytningarna',
        cancel         : 'Avbryt',
        fileFormat     : 'Filformat',
        rows           : 'Кader',
        alignRows      : 'Anpassa raderna',
        columns        : 'Kolumner',
        paperFormat    : 'Pappersformat',
        orientation    : 'Orientering',
        repeatHeader   : 'Upprepa rubriken'
    },

    ExportRowsCombo : {
        all     : 'Alla rader',
        visible : 'Synliga rader'
    },

    ExportOrientationCombo : {
        portrait  : 'Stående',
        landscape : 'Liggande'
    },

    SinglePageExporter : {
        singlepage : 'En sida'
    },

    MultiPageExporter : {
        multipage     : 'Flera sidor',
        exportingPage : ({ currentPage, totalPages }) => `Exporterar sidan ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Flera sidor (lodrätt)',
        exportingPage     : ({ currentPage, totalPages }) => `Exporterar sidan ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Laddar',
        expand   : 'Expandera',
        collapse : 'Fäll ihop'
    },

    TreeGroup : {
        group                  : 'Gruppera efter',
        stopGrouping           : 'Avsluta gruppering',
        stopGroupingThisColumn : 'Avgruppera kolumn'
    }
};

export default LocaleHelper.publishLocale(locale);
