import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Nl.js';

const emptyString = new String();

const locale = {

    localeName : 'Nl',
    localeDesc : 'Nederlands',
    localeCode : 'nl',

    ColumnPicker : {
        column          : 'Kolom',
        columnsMenu     : 'Kolommen',
        hideColumn      : 'Verberg Kolom',
        hideColumnShort : 'Verberg',
        newColumns      : 'Nieuwe kolommen'
    },

    Filter : {
        applyFilter   : 'Pas filter toe',
        filter        : 'Filter',
        editFilter    : 'Wijzig filter',
        on            : 'Aan',
        before        : 'Voor',
        after         : 'Na',
        equals        : 'Is gelijk',
        lessThan      : 'Minder dan',
        moreThan      : 'Meer dan',
        removeFilter  : 'Verwijder filter',
        disableFilter : 'Uitschakelen filter'
    },

    FilterBar : {
        enableFilterBar  : 'Maak filterbalk zichtbaar',
        disableFilterBar : 'Verberg filterbalk'
    },

    Group : {
        group                : 'Groepeer',
        groupAscending       : 'Groepeer oplopend',
        groupDescending      : 'Groepeer aflopend',
        groupAscendingShort  : 'Oplopend',
        groupDescendingShort : 'Aflopend',
        stopGrouping         : 'Maak groepering ongedaan',
        stopGroupingShort    : 'Maak ongedaan'
    },

    HeaderMenu : {
        moveBefore     : text => `Verplaatsen naar voor "${text}"`,
        moveAfter      : text => `Verplaatsen naar na "${text}"`,
        collapseColumn : 'Kolom inklappen',
        expandColumn   : 'Kolom uitklappen'
    },

    ColumnRename : {
        rename : 'Hernoemen'
    },

    MergeCells : {
        mergeCells  : 'Cellen samenvoegen',
        menuTooltip : 'Met deze kolom sortering cellen met dezelfde waarde samenvoegen'
    },

    Search : {
        searchForValue : 'Zoek op term'
    },

    Sort : {
        sort                   : 'Sorteer',
        sortAscending          : 'Sorteer oplopend',
        sortDescending         : 'Sorteer aflopend',
        multiSort              : 'Meerdere sorteringen',
        removeSorter           : 'Verwijder sortering',
        addSortAscending       : 'Voeg oplopende sortering toe',
        addSortDescending      : 'Voeg aflopende sortering toe',
        toggleSortAscending    : 'Sorteer oplopend',
        toggleSortDescending   : 'Sorteer aflopend',
        sortAscendingShort     : 'Oplopend',
        sortDescendingShort    : 'Aflopend',
        removeSorterShort      : 'Verwijder',
        addSortAscendingShort  : '+ Oplopend',
        addSortDescendingShort : '+ Aflopend'
    },

    Split : {
        split        : 'Gesplitst',
        unsplit      : 'Ongeplitst',
        horizontally : 'Horizontaal',
        vertically   : 'Verticaal',
        both         : 'Beide'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} kolom. ` : ''}SPATIEBALK voor contextmenu${column.sortable ? ', ENTER om te sorteren' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Rijselectie wisselen',
        toggleSelection : 'Toggle selectie van volledige dataset'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `rating : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Laden mislukt!',
        syncFailedMessage  : 'Gegevenssynchronisatie is mislukt!',
        unspecifiedFailure : 'Niet-gespecificeerde fout',
        networkFailure     : 'Netwerk fout',
        parseFailure       : 'Kan server response niet parsen',
        serverResponse     : 'Server reactie:',
        noRows             : 'Geen rijen om weer te geven',
        moveColumnLeft     : 'Plaats naar het linker kader',
        moveColumnRight    : 'Plaats naar het rechter kader',
        moveColumnTo       : region => `Kolom verplaatsen naar ${region}`
    },

    CellMenu : {
        removeRow : 'Verwijder'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopieer',
        cutRecord   : 'Knip',
        pasteRecord : 'Plak',
        rows        : 'rijen',
        row         : 'row'
    },

    CellCopyPaste : {
        copy  : 'Kopieer',
        cut   : 'Knip',
        paste : 'Plak'
    },

    PdfExport : {
        'Waiting for response from server' : 'Wachten op antwoord van server...',
        'Export failed'                    : 'Export mislukt',
        'Server error'                     : 'Serverfout',
        'Generating pages'                 : "Pagina's genereren...",
        'Click to abort'                   : 'Annuleren'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Instellingen exporteren',
        export         : 'Exporteren',
        exporterType   : 'Paginering beheren',
        cancel         : 'Annuleren',
        fileFormat     : 'Bestandsformaat',
        rows           : 'Rijen',
        alignRows      : 'Rijen uitlijnen',
        columns        : 'Kolommen',
        paperFormat    : 'Papier formaat',
        orientation    : 'Oriëntatatie',
        repeatHeader   : 'Herhaal koptekst'
    },

    ExportRowsCombo : {
        all     : 'Alle rijen',
        visible : 'Zichtbare rijen'
    },

    ExportOrientationCombo : {
        portrait  : 'Staand',
        landscape : 'Liggend'
    },

    SinglePageExporter : {
        singlepage : 'Enkele pagina'
    },

    MultiPageExporter : {
        multipage     : "Meerdere pagina's",
        exportingPage : ({ currentPage, totalPages }) => `Exporteren van de pagina ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : "Meerdere pagina's (verticaal)",
        exportingPage     : ({ currentPage, totalPages }) => `Exporteren van de pagina ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Bezig met laden',
        expand   : 'Klap uit',
        collapse : 'Klap in'
    },

    TreeGroup : {
        group                  : 'Groeperen op',
        stopGrouping           : 'Stop groeperen',
        stopGroupingThisColumn : 'Groeperen opheffen voor deze kolom'
    }
};

export default LocaleHelper.publishLocale(locale);
