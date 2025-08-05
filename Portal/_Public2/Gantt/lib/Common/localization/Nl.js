import LocaleManager from '../../Common/localization/LocaleManager.js';

const locale = {

    localeName : 'Nl',
    localeDesc : 'Nederlands',

    //region Columns

    TemplateColumn : {
        noTemplate : 'TemplateColumn heeft een template nodig',
        noFunction : 'TemplateColumn.template moet een functie zijn'
    },

    ColumnStore : {
        columnTypeNotFound : data => `Kolom type '${data.type}' is niet geregistreerd`
    },

    //endregion

    //region Mixins

    InstancePlugin : {
        fnMissing         : data => `Het lukt niet fn ${data.plugIntoName}#${data.fnName} te schakelen, de plugin fn ${data.pluginName}#${data.fnName} bestaat niet`,
        overrideFnMissing : data => `Het lukt niet fn te overerven ${data.plugIntoName}#${data.fnName}, de plugin fn ${data.pluginName}#${data.fnName} bestaat niet`
    },

    //endregion

    //region Features
    ColumnPicker : {
        columnsMenu     : 'Kolommen',
        hideColumn      : 'Verberg Kolom',
        hideColumnShort : 'Verberg'
    },

    Filter : {
        applyFilter  : 'Pas filter toe',
        filter       : 'Filter',
        editFilter   : 'Wijzig filter',
        on           : 'Aan',
        before       : 'Voor',
        after        : 'Na',
        equals       : 'Is gelijk',
        lessThan     : 'Minder dan',
        moreThan     : 'Meer dan',
        removeFilter : 'Verwijder filter'
    },

    FilterBar : {
        enableFilterBar  : 'Maak filterbalk zichtbaar',
        disableFilterBar : 'Verberg filterbalk'
    },

    Group : {
        groupAscending       : 'Groepeer oplopend',
        groupDescending      : 'Groepeer aflopend',
        groupAscendingShort  : 'Oplopend',
        groupDescendingShort : 'Aflopend',
        stopGrouping         : 'Maak groepering ongedaan',
        stopGroupingShort    : 'Maak ongedaan'
    },

    Search : {
        searchForValue : 'Zoek op term'
    },

    Sort : {
        'sortAscending'          : 'Sorteer oplopend',
        'sortDescending'         : 'Sorteer aflopend',
        'multiSort'              : 'Meerdere sorteringen',
        'removeSorter'           : 'Verwijder sortering',
        'addSortAscending'       : 'Voeg oplopende sortering toe',
        'addSortDescending'      : 'Voeg aflopende sortering toe',
        'toggleSortAscending'    : 'Sorteer oplopend',
        'toggleSortDescending'   : 'Sorteer aflopend',
        'sortAscendingShort'     : 'Oplopend',
        'sortDescendingShort'    : 'Aflopend',
        'removeSorterShort'      : 'Verwijder',
        'addSortAscendingShort'  : '+ Oplopend',
        'addSortDescendingShort' : '+ Aflopend'
    },

    Tree : {
        noTreeColumn : 'Om de boomstructuur (tree) eigenschap te kunnen gebruiken zet, tree: true'
    },

    //endregion

    //region Grid

    Grid : {
        featureNotFound          : data => `Eigenschap '${data}' is niet beschikbaar, controleer of u de optie geimporteerd heeft`,
        invalidFeatureNameFormat : data => `Ongeldige functienaam '${data}', moet beginnen met een kleine letter`,
        removeRow                : 'Verwijder rij',
        removeRows               : 'Verwijder rijen',
        loadMask                 : 'Laden...',
        loadFailedMessage        : 'Laden mislukt.',
        moveColumnLeft           : 'Plaats naar het linker kader',
        moveColumnRight          : 'Plaats naar het rechter kader',
        noRows                   : 'Geen rijen om weer te geven'
    },

    //endregion

    //region Widgets

    Field : {
        invalidValue          : 'Ongeldige veldwaarde',
        minimumValueViolation : 'Minimale waarde schending',
        maximumValueViolation : 'Maximale waarde schending',
        fieldRequired         : 'Dit veld is verplicht',
        validateFilter        : 'Waarde moet worden geselecteerd in de lijst'
    },

    DateField : {
        invalidDate : 'Ongeldige datuminvoer'
    },

    TimeField : {
        invalidTime : 'Ongeldige tijdsinvoer'
    },

    //endregion

    //region Others

    DateHelper : {
        locale       : 'nl',
        shortWeek    : 'w',
        shortQuarter : 'kw',
        week         : 'Week',
        weekStartDay : 1,
        unitNames    : [
            { single : 'ms',       plural : 'ms',        abbrev : 'ms' },
            { single : 'seconde',  plural : 'seconden',  abbrev : 's' },
            { single : 'minuut',   plural : 'minuten',   abbrev : 'm' },
            { single : 'uur',      plural : 'uren',      abbrev : 'u' },
            { single : 'dag',      plural : 'dagen',     abbrev : 'd' },
            { single : 'week',     plural : 'weken',     abbrev : 'w' },
            { single : 'maand',    plural : 'maanden',   abbrev : 'ma' },
            { single : 'kwartaal', plural : 'kwartalen', abbrev : 'kw' },
            { single : 'jaar',     plural : 'jaren',     abbrev : 'j' }
        ],
        // Used to build a RegExp for parsing time units.
        // The full names from above are added into the generated Regexp.
        // So you may type "2 w" or "2 wk" or "2 week" or "2 weken" into a DurationField.
        // When generating its display value though, it uses the full localized names above.
        unitAbbreviations : [
            ['mil'],
            ['s', 'sec'],
            ['m', 'min'],
            ['u'],
            ['d'],
            ['w', 'wk'],
            ['ma', 'mnd', 'm'],
            ['k', 'kwar', 'kwt', 'kw'],
            ['j', 'jr']
        ],
        parsers : {
            'L'  : 'DD-MM-YYYY',
            'LT' : 'HH:mm'
        },
        ordinalSuffix : number => number
    },

    BooleanCombo : {
        'Yes' : 'Ja',
        'No'  : 'Nee'
    }

    //endregion
};

export default locale;

LocaleManager.registerLocale('Nl', { desc : 'Nederlands', locale : locale });
