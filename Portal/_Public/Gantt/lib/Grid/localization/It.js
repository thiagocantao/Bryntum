import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/It.js';

const emptyString = new String();

const locale = {

    localeName : 'It',
    localeDesc : 'Italiano',
    localeCode : 'it',

    ColumnPicker : {
        column          : 'Colonna',
        columnsMenu     : 'Colonne',
        hideColumn      : 'Nascondi colonna',
        hideColumnShort : 'Nascondi',
        newColumns      : 'Nuove colonne'
    },

    Filter : {
        applyFilter   : 'Applica filtro',
        filter        : 'Filtra',
        editFilter    : 'Modifica filtro',
        on            : 'Il',
        before        : 'Prima',
        after         : 'Dopo',
        equals        : 'Uguale',
        lessThan      : 'Meno di',
        moreThan      : 'Più di',
        removeFilter  : 'Rimuovi filtro',
        disableFilter : 'Disabilita filtro'
    },

    FilterBar : {
        enableFilterBar  : 'Mostra barra del filtro',
        disableFilterBar : 'Nascondi barra del filtro'
    },

    Group : {
        group                : 'Raggruppa',
        groupAscending       : 'Gruppo ascendente',
        groupDescending      : 'Gruppo discendente',
        groupAscendingShort  : 'Ascendente',
        groupDescendingShort : 'Discendente',
        stopGrouping         : 'Interrompi raggruppamento',
        stopGroupingShort    : 'Stop'
    },

    HeaderMenu : {
        moveBefore     : text => `Sposta prima "${text}"`,
        moveAfter      : text => `Sposta dopo "${text}"`,
        collapseColumn : 'Riduci colonna',
        expandColumn   : 'Espandi colonna'
    },

    ColumnRename : {
        rename : 'Rinomina'
    },

    MergeCells : {
        mergeCells  : 'Unisci celle',
        menuTooltip : 'Unisci celle con lo stesso valore se ordinate da questa colonna'
    },

    Search : {
        searchForValue : 'Cerca valore'
    },

    Sort : {
        sort                   : 'Ordina',
        sortAscending          : 'Ordine ascendente',
        sortDescending         : 'Ordine discendente',
        multiSort              : 'Ordinamento multiplo',
        removeSorter           : 'Rimuovi ordinatore',
        addSortAscending       : 'Aggiungi ordinatore ascendente',
        addSortDescending      : 'Aggiungi ordinatore discendente',
        toggleSortAscending    : 'Passa ad ascendente',
        toggleSortDescending   : 'Passa a discendente',
        sortAscendingShort     : 'Ascendente',
        sortDescendingShort    : 'Discendente',
        removeSorterShort      : 'Rimuovi',
        addSortAscendingShort  : '+ Ascendente',
        addSortDescendingShort : '+ Discendente'
    },

    Split : {
        split        : 'Dividi',
        unsplit      : 'Unisci',
        horizontally : 'Orizzontalmente',
        vertically   : 'Verticalmente',
        both         : 'Entrambi'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} colonna. ` : ''}SPAZIO per il menu contestuale${column.sortable ? ', INVIO per ordinare' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Passa a selezione righe',
        toggleSelection : 'Passa a selezione di un intero set di dati'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `valutazione : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Caricamento dati non riuscito!',
        syncFailedMessage  : 'Sincronizzazione dati non riuscita!',
        unspecifiedFailure : 'Errore non specificato',
        networkFailure     : 'Errore di rete',
        parseFailure       : 'Impossibile interpretare la risposta del server',
        serverResponse     : 'Risposta del server:',
        noRows             : 'Nessun record da visualizzare',
        moveColumnLeft     : 'Sposta alla sezione sinistra',
        moveColumnRight    : 'Sposta alla sezione destra',
        moveColumnTo       : region => `Sposta colonna a ${region}`
    },

    CellMenu : {
        removeRow : 'Elimina'
    },

    RowCopyPaste : {
        copyRecord  : 'Copia',
        cutRecord   : 'Taglia',
        pasteRecord : 'Incolla',
        rows        : 'righe',
        row         : 'riga'
    },

    CellCopyPaste : {
        copy  : 'Copia',
        cut   : 'Taglia',
        paste : 'Incolla'
    },

    PdfExport : {
        'Waiting for response from server' : 'In attesa della risposta dal server...',
        'Export failed'                    : 'Esportazione non riuscita',
        'Server error'                     : 'Errore del server',
        'Generating pages'                 : 'Generazione pagine...',
        'Click to abort'                   : 'Annulla'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Esporta impostazioni',
        export         : 'Esporta',
        exporterType   : 'Controllo dell’impaginazione',
        cancel         : 'Annulla',
        fileFormat     : 'Formato del file',
        rows           : 'Righe',
        alignRows      : 'Allinea righe',
        columns        : 'Colonne',
        paperFormat    : 'Formato carta',
        orientation    : 'Orientamento',
        repeatHeader   : 'Ripeti intestazione'
    },

    ExportRowsCombo : {
        all     : 'Tutte le righe',
        visible : 'Righe visibili'
    },

    ExportOrientationCombo : {
        portrait  : 'Ritratto',
        landscape : 'Paesaggio'
    },

    SinglePageExporter : {
        singlepage : 'Singola pagina'
    },

    MultiPageExporter : {
        multipage     : 'Più pagine',
        exportingPage : ({ currentPage, totalPages }) => `Esportazione pagina ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Più pagine (verticale)',
        exportingPage     : ({ currentPage, totalPages }) => `Esportazione pagina ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Caricamento',
        expand   : 'Espandi',
        collapse : 'Comprimi'
    },

    TreeGroup : {
        group                  : 'Raggruppa per',
        stopGrouping           : 'Interrompi raggruppamento',
        stopGroupingThisColumn : 'Elimina raggruppamento di questa colonna'
    }
};

export default LocaleHelper.publishLocale(locale);
