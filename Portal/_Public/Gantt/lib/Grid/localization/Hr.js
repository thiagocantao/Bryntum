import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Hr.js';

const emptyString = new String();

const locale = {

    localeName : 'Hr',
    localeDesc : 'Hrvatski',
    localeCode : 'hr',

    ColumnPicker : {
        column          : 'Stupac',
        columnsMenu     : 'Stupci',
        hideColumn      : 'Sakrij stupac',
        hideColumnShort : 'Sakrij',
        newColumns      : 'Novi stupci'
    },

    Filter : {
        applyFilter   : 'Primijeni filtar',
        filter        : 'Filtar',
        editFilter    : 'Uredi filtar',
        on            : 'Na',
        before        : 'Prije',
        after         : 'Poslije',
        equals        : 'Jednako',
        lessThan      : 'Manje od',
        moreThan      : 'Više od',
        removeFilter  : 'Ukloni filtar',
        disableFilter : 'Onemogući filtar'
    },

    FilterBar : {
        enableFilterBar  : 'Pokaži traku s filtrima',
        disableFilterBar : 'Sakrij traku s filtrima'
    },

    Group : {
        group                : 'Grupiraj',
        groupAscending       : 'Grupiraj uzlazno',
        groupDescending      : 'Grupiraj silazno',
        groupAscendingShort  : 'Uzlazno',
        groupDescendingShort : 'Silazno',
        stopGrouping         : 'Zaustavi grupiranje',
        stopGroupingShort    : 'Zaustavi'
    },

    HeaderMenu : {
        moveBefore     : text => `Pomakni prije "${text}"`,
        moveAfter      : text => `Pomakni nakon "${text}"`,
        collapseColumn : 'Sažmi stupac',
        expandColumn   : 'Proširi stupac'
    },

    ColumnRename : {
        rename : 'Preimenuj'
    },

    MergeCells : {
        mergeCells  : 'Spoji ćelije',
        menuTooltip : 'Spoji ćelije s istom vrijednosti kad su razvrstane po ovom stupcu'
    },

    Search : {
        searchForValue : 'Pretraži vrijednost'
    },

    Sort : {
        sort                   : 'Razvrstaj',
        sortAscending          : 'Razvrstaj uzlazno',
        sortDescending         : 'Razvrstaj silazno',
        multiSort              : 'Višestruko razvrstavanje',
        removeSorter           : 'Ukloni razvrstavač',
        addSortAscending       : 'Dodaj uzlazni razvrstavač',
        addSortDescending      : 'Dodaj silazni razvrstavač',
        toggleSortAscending    : 'Promijeni na uzlazno',
        toggleSortDescending   : 'Promijeni na silazno',
        sortAscendingShort     : 'Uzlazno',
        sortDescendingShort    : 'Silazno',
        removeSorterShort      : 'Ukloni',
        addSortAscendingShort  : '+ Uzlazno',
        addSortDescendingShort : '+ Silazno'
    },

    Split : {
        split        : 'Podijeljeno',
        unsplit      : 'Nepodijeljeno',
        horizontally : 'Horizontalno',
        vertically   : 'Vertikalno',
        both         : 'Oboje'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} stupac. ` : ''}RAZMAKNICA za kontekstni izbornik${column.sortable ? ', ENTER za razvrstavanje' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Prebaci odabir retka',
        toggleSelection : 'Prebaci odabir čitavog skupa podataka'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `ocjena : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Učitavanje podataka nije uspjelo!',
        syncFailedMessage  : 'Sinkronizacija podataka nije uspjela!',
        unspecifiedFailure : 'Neočekivana pogreška',
        networkFailure     : 'Pogreška mreže',
        parseFailure       : 'Analiziranje odgovora poslužitelja nije uspjelo',
        serverResponse     : 'Odgovor poslužitelja:',
        noRows             : 'Nema zapisa za prikazivanje',
        moveColumnLeft     : 'Pomakni se na lijevi odjeljak',
        moveColumnRight    : 'Pomakni se na desni odjeljak',
        moveColumnTo       : region => `Pomakni stupac na ${region}`
    },

    CellMenu : {
        removeRow : 'Obriši'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopiraj',
        cutRecord   : 'Izreži',
        pasteRecord : 'Zalijepi',
        rows        : 'redci',
        row         : 'redak'
    },

    CellCopyPaste : {
        copy  : 'Kopiraj',
        cut   : 'Izreži',
        paste : 'Zalijepi'
    },

    PdfExport : {
        'Waiting for response from server' : 'Čeka se odgovor poslužitelja...',
        'Export failed'                    : 'Izvoz nije uspio',
        'Server error'                     : 'Pogreška poslužitelja',
        'Generating pages'                 : 'Izrada stranica...',
        'Click to abort'                   : 'Otkaži'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Postavke izvoza',
        export         : 'Izvoz',
        exporterType   : 'Kontrola numeriranja stranica',
        cancel         : 'Otkaži',
        fileFormat     : 'Oblik datoteke',
        rows           : 'Retci',
        alignRows      : 'Poravnaj retke',
        columns        : 'Stupci',
        paperFormat    : 'Oblik papira',
        orientation    : 'Usmjerenje',
        repeatHeader   : 'Ponovi zaglavlje'
    },

    ExportRowsCombo : {
        all     : 'Svi retci',
        visible : 'Vidljivi retci'
    },

    ExportOrientationCombo : {
        portrait  : 'Okomito',
        landscape : 'Vodoravno'
    },

    SinglePageExporter : {
        singlepage : 'Jedna stranica'
    },

    MultiPageExporter : {
        multipage     : 'Višestruke stranice',
        exportingPage : ({ currentPage, totalPages }) => `Izvoz stranice ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Višestruke stranice (okomito)',
        exportingPage     : ({ currentPage, totalPages }) => `Izvoz stranice ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Učitavanje',
        expand   : 'Proširi',
        collapse : 'Sažmi'
    },

    TreeGroup : {
        group                  : 'Grupiraj po',
        stopGrouping           : 'Prekini grupiranje',
        stopGroupingThisColumn : 'Poništi grupiranje stupca'
    }
};

export default LocaleHelper.publishLocale(locale);
