import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Sk.js';

const emptyString = new String();

const locale = {

    localeName : 'Sk',
    localeDesc : 'Slovenský',
    localeCode : 'sk',

    ColumnPicker : {
        column          : 'Stĺpec',
        columnsMenu     : 'Stĺpce',
        hideColumn      : 'Skryť stĺpec',
        hideColumnShort : 'Skryť',
        newColumns      : 'Nové stĺpce'
    },

    Filter : {
        applyFilter   : 'Použiť filter',
        filter        : 'Filter',
        editFilter    : 'Upraviť filter',
        on            : 'On',
        before        : 'Pred',
        after         : 'Po',
        equals        : 'Rovná sa',
        lessThan      : 'Menej ako',
        moreThan      : 'Viac ako',
        removeFilter  : 'Odstrániť filter',
        disableFilter : 'Deaktivovať filter'
    },

    FilterBar : {
        enableFilterBar  : 'Ukázať lištu filtra',
        disableFilterBar : 'Skryť lištu filtra'
    },

    Group : {
        group                : 'Group',
        groupAscending       : 'Vzostup skupiny',
        groupDescending      : 'Pokles skupiny',
        groupAscendingShort  : 'Vzostu',
        groupDescendingShort : 'Pokles',
        stopGrouping         : 'Zastaviť zoskupovanie',
        stopGroupingShort    : 'Zastaviť'
    },

    HeaderMenu : {
        moveBefore     : text => `Pohyb pred "${text}"`,
        moveAfter      : text => `Pohyb po "${text}"`,
        collapseColumn : 'Zbaliť stĺpec',
        expandColumn   : 'Rozbaliť stĺpec'
    },

    ColumnRename : {
        rename : 'Premenovať'
    },

    MergeCells : {
        mergeCells  : 'Zlúčiť bunky',
        menuTooltip : 'Zlúčiť bunky s rovnakou hodnotou, keď sú triedené podľa tohto stĺpca'
    },

    Search : {
        searchForValue : 'Hľadať hodnotu'
    },

    Sort : {
        sort                   : 'Sort',
        sortAscending          : 'Triediť vzrastajúce',
        sortDescending         : 'Triediť klesajúce',
        multiSort              : 'Viacnásobné triedenie',
        removeSorter           : 'Odstrániť filter',
        addSortAscending       : 'Pridať stúpajúci filetr',
        addSortDescending      : 'Pridať klesajúci filter',
        toggleSortAscending    : 'Zmeniť na stúpajúci',
        toggleSortDescending   : 'Zmeniť na klesajúci',
        sortAscendingShort     : 'Stúpajúci',
        sortDescendingShort    : 'Klesajúci',
        removeSorterShort      : 'Odstrániť',
        addSortAscendingShort  : '+ Stúpajúci',
        addSortDescendingShort : '+ Klesajúci'
    },

    Split : {
        split        : 'Rozdelené',
        unsplit      : 'Nerozdelené',
        horizontally : 'Horizontálne',
        vertically   : 'Vertikálne',
        both         : 'Obe'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} stĺpec. ` : ''}MEDZERNÍK pre kontextové menu${column.sortable ? ', ZADAŤ na triedenie' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Prepnúť výber riadka',
        toggleSelection : 'Prepnúť výber celej sady údajov'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `hodnotenie : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Načítavanie údajov zlyhalo!',
        syncFailedMessage  : 'Synchronizácia údajov zlyhala!',
        unspecifiedFailure : 'Nešpecifikované zlyhanie',
        networkFailure     : 'Chyba siete',
        parseFailure       : 'Analýza odpovede server zlyhala',
        serverResponse     : 'Odpoveď servera:',
        noRows             : 'Žiadne záznamy na zobrazenie',
        moveColumnLeft     : 'Presunúť do ľavej časti',
        moveColumnRight    : 'Presunúť do pravej časti',
        moveColumnTo       : region => `Presunúť stĺpec do ${region}`
    },

    CellMenu : {
        removeRow : 'Vymazať'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopírovať',
        cutRecord   : 'Orezať',
        pasteRecord : 'Vložiť',
        rows        : 'riadky',
        row         : 'radok'
    },

    CellCopyPaste : {
        copy  : 'Kopírovať',
        cut   : 'Vystrihnúť',
        paste : 'Vložiť'
    },

    PdfExport : {
        'Waiting for response from server' : 'Čakanie na odpoveď servera...',
        'Export failed'                    : 'Export zlyhal',
        'Server error'                     : 'Chyba servera',
        'Generating pages'                 : 'Generujú sa stránky...',
        'Click to abort'                   : 'Zrušiť'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Exportovať nastavenia',
        export         : 'Exportovať',
        exporterType   : 'Kontrolovať stránkovanie',
        cancel         : 'Zrušiť',
        fileFormat     : 'Formát súboru',
        rows           : 'Riadky',
        alignRows      : 'Zarovnať riaky',
        columns        : 'Stĺpce',
        paperFormat    : 'Formát papiera',
        orientation    : 'Orientácia',
        repeatHeader   : 'Zopakovať hlavičku'
    },

    ExportRowsCombo : {
        all     : 'Všetky riadky',
        visible : 'Viditeľné riadky'
    },

    ExportOrientationCombo : {
        portrait  : 'Portrét',
        landscape : 'Krajina'
    },

    SinglePageExporter : {
        singlepage : 'Jedna strana'
    },

    MultiPageExporter : {
        multipage     : 'Viaceré strany',
        exportingPage : ({ currentPage, totalPages }) => `Exportuje sa strana ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Viaceré strany (vertikálne)',
        exportingPage     : ({ currentPage, totalPages }) => `Exportuje sa strana ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Načítavanie',
        expand   : 'Rozbaliť',
        collapse : 'Zbaliť'
    },

    TreeGroup : {
        group                  : 'Zoskupiť podľa',
        stopGrouping           : 'Zastaviť zoskupovanie',
        stopGroupingThisColumn : 'Zrušiť zoskupovanie tejto stĺpca'
    }
};

export default LocaleHelper.publishLocale(locale);
