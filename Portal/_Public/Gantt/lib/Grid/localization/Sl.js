import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Sl.js';

const emptyString = new String();

const locale = {

    localeName : 'Sl',
    localeDesc : 'Slovensko',
    localeCode : 'sl',

    ColumnPicker : {
        column          : 'Stolpec',
        columnsMenu     : 'Stolpci',
        hideColumn      : 'Skrij stolpec',
        hideColumnShort : 'Skrij',
        newColumns      : 'Novi stolpci'
    },

    Filter : {
        applyFilter   : 'Uporabi filter',
        filter        : 'Filter',
        editFilter    : 'Uredi filter',
        on            : 'Vklopljeno',
        before        : 'Prej',
        after         : 'Po',
        equals        : 'Enako',
        lessThan      : 'Manj kot',
        moreThan      : 'Več kot',
        removeFilter  : 'Odstrani filter',
        disableFilter : 'Onemogoči filter'
    },

    FilterBar : {
        enableFilterBar  : 'Pokaži vrstico s filtri',
        disableFilterBar : 'Skrij vrstico s filtri'
    },

    Group : {
        group                : 'Skupina',
        groupAscending       : 'Skupina narašča',
        groupDescending      : 'Skupina pada',
        groupAscendingShort  : 'Naraščajoče',
        groupDescendingShort : 'Padajoče',
        stopGrouping         : 'Ustavi združevanje',
        stopGroupingShort    : 'Ustavi'
    },

    HeaderMenu : {
        moveBefore     : text => ` Premakni pred"${text}"`,
        moveAfter      : text => ` Premakni za "${text}"`,
        collapseColumn : 'Strni stolpec',
        expandColumn   : 'Razširi stolpec'
    },

    ColumnRename : {
        rename : 'Preimenuj'
    },

    MergeCells : {
        mergeCells  : 'Združi celice',
        menuTooltip : 'Združi celice z isto vrednostjo, ko so razvrščene po tem stolpcu'
    },

    Search : {
        searchForValue : 'Išči vrednost'
    },

    Sort : {
        sort                   : 'Razvrsti',
        sortAscending          : 'Razvrsti naraščajoče',
        sortDescending         : 'Razvrsti padajoče',
        multiSort              : 'Več razvrstitev',
        removeSorter           : 'Odstrani razvrščevalnik',
        addSortAscending       : 'Dodaj naraščajoči razvrščevalnik',
        addSortDescending      : 'Dodaj padajoči razvrščevalnik',
        toggleSortAscending    : 'Spremeni v naraščajoče',
        toggleSortDescending   : 'Spremeni v padajoče',
        sortAscendingShort     : 'Naraščajoče',
        sortDescendingShort    : 'Padajoče',
        removeSorterShort      : 'Odstrani',
        addSortAscendingShort  : '+Naraščajoče',
        addSortDescendingShort : '+Padajoče'
    },

    Split : {
        split        : 'Deljenje',
        unsplit      : 'Nedeljenje',
        horizontally : 'Vodoravno',
        vertically   : 'Navpično',
        both         : 'Oboje'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} stolpec. ` : ''}PRESLEDNICA za kontekstni meni${column.sortable ? ', VNESI za razvrstitev' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Preklop izbire vrstice',
        toggleSelection : 'Preklopi izbor celotnega nabora podatkov'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `ocena : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Nalaganje podatkov ni uspelo!',
        syncFailedMessage  : 'Sinhronizacija podatkov ni uspela!',
        unspecifiedFailure : 'Nedoločena napaka',
        networkFailure     : 'Napaka omrežja',
        parseFailure       : 'Razčlenitev odgovora strežnika ni uspela',
        serverResponse     : 'Odziv strežnika:',
        noRows             : 'Ni zapisov za prikaz',
        moveColumnLeft     : 'Premakni se v levi odsek',
        moveColumnRight    : 'Premakni se v desni odsek',
        moveColumnTo       : region => ` Premakni stolpec v ${region}`
    },

    CellMenu : {
        removeRow : 'Izbriši'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopiraj',
        cutRecord   : 'Izreži',
        pasteRecord : 'Prilepi',
        rows        : 'vrstice',
        row         : 'vrstica'
    },

    CellCopyPaste : {
        copy  : 'Kopiraj',
        cut   : 'Izreži',
        paste : 'Prilepi'
    },

    PdfExport : {
        'Waiting for response from server' : 'Čakanje na odgovor strežnika ...',
        'Export failed'                    : 'Izvoz ni uspel',
        'Server error'                     : 'Napaka strežnika',
        'Generating pages'                 : 'Ustvarjanje strani ...',
        'Click to abort'                   : 'Prekliči'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Izvozi nastavitve',
        export         : 'Izvozi',
        exporterType   : 'Nadzor številčenja strani',
        cancel         : 'Prekliči',
        fileFormat     : 'Oblika datoteke',
        rows           : 'Vrstice',
        alignRows      : 'Poravnaj vrstice',
        columns        : 'Stolpci',
        paperFormat    : 'Format papirja',
        orientation    : 'Orientacija',
        repeatHeader   : 'Ponovi glavo'
    },

    ExportRowsCombo : {
        all     : 'Vse vrstice',
        visible : 'Vidne vrstice'
    },

    ExportOrientationCombo : {
        portrait  : 'Portret',
        landscape : 'Pokrajina'
    },

    SinglePageExporter : {
        singlepage : 'Ena stran'
    },

    MultiPageExporter : {
        multipage     : 'Več strani',
        exportingPage : ({ currentPage, totalPages }) => `Izvažanje strani ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Več strani (navpično)',
        exportingPage     : ({ currentPage, totalPages }) => `Izvažanje strani ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Nalaganje',
        expand   : 'Razširi',
        collapse : 'Strni'
    },

    TreeGroup : {
        group                  : 'Združi po',
        stopGrouping           : 'Prenehaj z združevanjem',
        stopGroupingThisColumn : 'Prenehaj z združevanjem te stolpca'
    }
};

export default LocaleHelper.publishLocale(locale);
