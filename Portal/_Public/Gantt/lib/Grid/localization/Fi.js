import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Fi.js';

const emptyString = new String();

const locale = {

    localeName : 'Fi',
    localeDesc : 'Suomi',
    localeCode : 'fi',

    ColumnPicker : {
        column          : 'Sarake',
        columnsMenu     : 'Sarakkeet',
        hideColumn      : 'Piilota sarake',
        hideColumnShort : 'Piilota',
        newColumns      : 'Uudet sarakkeet'
    },

    Filter : {
        applyFilter   : 'Lisää suodatus',
        filter        : 'Suodata',
        editFilter    : 'Muokkaa suodatusta',
        on            : 'On',
        before        : 'Ennen',
        after         : 'Jälkeen',
        equals        : 'Yhtä kuin',
        lessThan      : 'Vähemmän kuin',
        moreThan      : 'Enemmän kuin',
        removeFilter  : 'Poista suodatus',
        disableFilter : 'Poista suodatin'
    },

    FilterBar : {
        enableFilterBar  : 'Näytä suodatinpalkki',
        disableFilterBar : 'Piilota suodatinpalkki'
    },

    Group : {
        group                : 'Ryhmä',
        groupAscending       : 'Ryhmä nouseva',
        groupDescending      : 'Ryhmä laskeva',
        groupAscendingShort  : 'Nouseva',
        groupDescendingShort : 'Laskeva',
        stopGrouping         : 'Lopeta ryhmittely',
        stopGroupingShort    : 'Lopeta'
    },

    HeaderMenu : {
        moveBefore     : text => `Siirry ennen"${text}"`,
        moveAfter      : text => `Siirry jälkeen "${text}"`,
        collapseColumn : 'Tiivistä sarake',
        expandColumn   : 'Laajenna sarake'
    },

    ColumnRename : {
        rename : 'Nimeä uudelleen'
    },

    MergeCells : {
        mergeCells  : 'Yhdistä solut',
        menuTooltip : 'Yhdistä solut, joilla on sama arvo, kun ne lajitellaan tämän sarakkeen mukaan'
    },

    Search : {
        searchForValue : 'Hae arvoa'
    },

    Sort : {
        sort                   : 'Sort',
        sortAscending          : 'Järjestä nouseva',
        sortDescending         : 'Järjestä laskeva',
        multiSort              : 'Monilajittelu',
        removeSorter           : 'Poista lajittelu',
        addSortAscending       : 'Lisää nouseva lajittelu',
        addSortDescending      : 'Lisää laskeva lajittelu',
        toggleSortAscending    : 'Vaihda nousevaan',
        toggleSortDescending   : 'Vaihda laskevaan',
        sortAscendingShort     : 'Nouseva',
        sortDescendingShort    : 'Laskeva',
        removeSorterShort      : 'Poista',
        addSortAscendingShort  : '+ Nouseva',
        addSortDescendingShort : '+ Laskeva'
    },

    Split : {
        split        : 'Jaa',
        unsplit      : 'Ei jaotusta',
        horizontally : 'Vaakasuunnassa',
        vertically   : 'Pystysuunnassa',
        both         : 'Molemmat'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} sarake. ` : ''}VÄLILYÖNTI kontekstivalikkoon${column.sortable ? ',ENTER lajitteluun' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Vaihda rivivalinta',
        toggleSelection : 'Vaihda koko tietokokonaisuuden valintaa'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `luokitus : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Tietojen lataus epäonnistui!',
        syncFailedMessage  : 'Tietojen synkronointi epäonnistui!',
        unspecifiedFailure : 'Määrittelemätön vika',
        networkFailure     : 'Verkkovirhe',
        parseFailure       : 'Palvelimen vastauksen jäsentäminen epäonnistui',
        serverResponse     : 'Vastaus palvelimelta:',
        noRows             : 'Ei näytettäviä tietueita',
        moveColumnLeft     : 'Siirry vasempaan osioon',
        moveColumnRight    : 'Siirry oikeaan osioon',
        moveColumnTo       : region => `Siirry sarakkeeseen ${region}`
    },

    CellMenu : {
        removeRow : 'Poista'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopioi',
        cutRecord   : 'Leikkaa',
        pasteRecord : 'Liitä',
        rows        : 'rivit',
        row         : 'rivi'
    },

    CellCopyPaste : {
        copy  : 'Kopioi',
        cut   : 'Leikkaa',
        paste : 'Liitä'
    },

    PdfExport : {
        'Waiting for response from server' : 'Odotetaan vastausta palvelimelta...',
        'Export failed'                    : 'Vienti epäonnistui',
        'Server error'                     : 'Palvelin virhe',
        'Generating pages'                 : 'Luodaan sivuja...',
        'Click to abort'                   : 'Peruuta'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Vientiasetukset',
        export         : 'Vienti',
        exporterType   : 'Ohjaa sivutusta',
        cancel         : 'Peruuta',
        fileFormat     : 'Tiedostomuoto',
        rows           : 'Rivit',
        alignRows      : 'Kohdista rivit',
        columns        : 'Sarakkeet',
        paperFormat    : 'Paperimuoto',
        orientation    : 'Orientaatio ',
        repeatHeader   : 'Toista otsikko'
    },

    ExportRowsCombo : {
        all     : 'Kaikki rivit',
        visible : 'Näkyvät rivit'
    },

    ExportOrientationCombo : {
        portrait  : 'Pysty',
        landscape : 'Vaaka'
    },

    SinglePageExporter : {
        singlepage : 'Yksittäinen sivu'
    },

    MultiPageExporter : {
        multipage     : 'Useita sivuja',
        exportingPage : ({ currentPage, totalPages }) => `Viedään sivua ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Useita sivuja (pysty)',
        exportingPage     : ({ currentPage, totalPages }) => `Viedään sivua ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Lataa',
        expand   : 'Laajenna',
        collapse : 'Tiivistä'
    },

    TreeGroup : {
        group                  : 'Ryhmittele',
        stopGrouping           : 'Lopeta ryhmittely',
        stopGroupingThisColumn : 'Pura ryhmittely tässä sarakkeessa'
    }
};

export default LocaleHelper.publishLocale(locale);
