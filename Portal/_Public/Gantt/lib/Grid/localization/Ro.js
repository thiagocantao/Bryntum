import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Ro.js';

const emptyString = new String();

const locale = {

    localeName : 'Ro',
    localeDesc : 'Română',
    localeCode : 'ro',

    ColumnPicker : {
        column          : 'Coloană',
        columnsMenu     : 'Coloane',
        hideColumn      : 'Ascundere coloană',
        hideColumnShort : 'Ascundere',
        newColumns      : 'Coloane noi'
    },

    Filter : {
        applyFilter   : 'Aplicare filtru',
        filter        : 'Filtru',
        editFilter    : 'Editare filtru',
        on            : 'Pornit',
        before        : 'Înainte',
        after         : 'După',
        equals        : 'Egal cu',
        lessThan      : 'Mai puțin decât',
        moreThan      : 'Mai mult decât',
        removeFilter  : 'Eliminare filtru',
        disableFilter : 'Dezactivați filtrul'
    },

    FilterBar : {
        enableFilterBar  : 'Afișare bară filtru',
        disableFilterBar : 'Ascundere bară filtru'
    },

    Group : {
        group                : 'Grupare',
        groupAscending       : 'Grupare ascendentă',
        groupDescending      : 'Grupare descendentă',
        groupAscendingShort  : 'Ascendent',
        groupDescendingShort : 'Descendent',
        stopGrouping         : 'Oprire grupare',
        stopGroupingShort    : 'Oprire'
    },

    HeaderMenu : {
        moveBefore     : text => `Mutare înainte de "${text}"`,
        moveAfter      : text => `Mutare după "${text}"`,
        collapseColumn : 'Restrângeți coloana',
        expandColumn   : 'Extindeți coloana'
    },

    ColumnRename : {
        rename : 'Redenumiți'
    },

    MergeCells : {
        mergeCells  : 'Îmbinare celule',
        menuTooltip : 'Îmbinare celule cu aceeași valoare la sortarea în funcție de această coloană'
    },

    Search : {
        searchForValue : 'Căutare valoare'
    },

    Sort : {
        sort                   : 'Sortare',
        sortAscending          : 'Sortare acendentă',
        sortDescending         : 'Sortare descendentă',
        multiSort              : 'Sortare multiplă',
        removeSorter           : 'Eliminare selector',
        addSortAscending       : 'Adăugare selector ascendent',
        addSortDescending      : 'Adăugare selector descendent',
        toggleSortAscending    : 'Schimbare la ascendent',
        toggleSortDescending   : 'Schimbare la descendent',
        sortAscendingShort     : 'Ascendent',
        sortDescendingShort    : 'Descendent',
        removeSorterShort      : 'Eliminare',
        addSortAscendingShort  : '+ Ascendent',
        addSortDescendingShort : '+ Descendent'
    },

    Split : {
        split        : 'Divizat',
        unsplit      : 'Nedivizat',
        horizontally : 'Orizontal',
        vertically   : 'Vertical',
        both         : 'Ambele'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} coloană. ` : ''}SPAȚIU pentru meniul contextual${column.sortable ? ', ENTER pentru sortare' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Comutare selectare rând',
        toggleSelection : 'Comutare selectare pentru întregul set de date'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `rating : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Încărcarea datelor a eșuat!',
        syncFailedMessage  : 'Sincronizarea datelor a eșuat!',
        unspecifiedFailure : 'Defecțiune nespecificată',
        networkFailure     : 'Eroare de rețea',
        parseFailure       : 'Analiza răspunsului serverului a eșuat',
        serverResponse     : 'Răspuns server:',
        noRows             : 'Nu există nicio înregistrare de afișat',
        moveColumnLeft     : 'Mutare în secțiunea din stânga',
        moveColumnRight    : 'Mutare în secțiunea din dreapta',
        moveColumnTo       : region => `Mutare coloană în ${region}`
    },

    CellMenu : {
        removeRow : 'Ștergere'
    },

    RowCopyPaste : {
        copyRecord  : 'Copiere',
        cutRecord   : 'Tăiere',
        pasteRecord : 'Lipire',
        rows        : 'rânduri',
        row         : 'rând'
    },

    CellCopyPaste : {
        copy  : 'Copiere',
        cut   : 'Tăiere',
        paste : 'Lipire'
    },

    PdfExport : {
        'Waiting for response from server' : 'Se așteaptă răspunsul de la server...',
        'Export failed'                    : 'Exportare eșuată',
        'Server error'                     : 'Eroare server',
        'Generating pages'                 : 'Se generează paginile...',
        'Click to abort'                   : 'Anulare'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Setări exportare',
        export         : 'Exportare',
        exporterType   : 'Control paginație',
        cancel         : 'Anulare',
        fileFormat     : 'Format fișier',
        rows           : 'Rânduri',
        alignRows      : 'Aliniere rânduri',
        columns        : 'Coloane',
        paperFormat    : 'Format hârtie',
        orientation    : 'Orientare',
        repeatHeader   : 'Repetare antet'
    },

    ExportRowsCombo : {
        all     : 'Toate rândurile',
        visible : 'Rânduri vizibile'
    },

    ExportOrientationCombo : {
        portrait  : 'Portret',
        landscape : 'Vedere'
    },

    SinglePageExporter : {
        singlepage : 'Pagină unică'
    },

    MultiPageExporter : {
        multipage     : 'Pagini multiple',
        exportingPage : ({ currentPage, totalPages }) => `Se exportă pagina ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Pagini multiple (vertical)',
        exportingPage     : ({ currentPage, totalPages }) => `Se exportă pagina ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Se încarcă',
        expand   : 'Extindere',
        collapse : 'Restrângere'
    },

    TreeGroup : {
        group                  : 'Grupează după',
        stopGrouping           : 'Oprește gruparea',
        stopGroupingThisColumn : 'Oprește gruparea acestei coloane'
    }
};

export default LocaleHelper.publishLocale(locale);
