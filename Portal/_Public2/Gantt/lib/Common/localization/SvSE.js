import LocaleManager from '../../Common/localization/LocaleManager.js';

const locale = {

    localeName : 'SvSE',
    localeDesc : 'Svenska',

    //region Columns

    TemplateColumn : {
        noTemplate : 'TemplateColumn kräver en template',
        noFunction : 'TemplateColumn.template måste vara en funktion'
    },

    ColumnStore : {
        columnTypeNotFound : data => `Kolumntypen '${data.type}' är inte registrerad`
    },

    //endregion

    //region Mixins

    InstancePlugin : {
        fnMissing         : data => `Försöker att länka fn ${data.plugIntoName}#${data.fnName}, men plugin fn ${data.pluginName}#${data.fnName} finns inte`,
        overrideFnMissing : data => `Försöker att skriva över fn ${data.plugIntoName}#${data.fnName}, men plugin fn ${data.pluginName}#${data.fnName} finns inte`
    },

    //endregion

    //region Features
    ColumnPicker : {
        columnsMenu     : 'Kolumner',
        hideColumn      : 'Dölj kolumn',
        hideColumnShort : 'Dölj'
    },

    Filter : {
        applyFilter  : 'Använd filter',
        editFilter   : 'Redigera filter',
        filter       : 'Filter',
        on           : 'På',
        before       : 'Före',
        after        : 'Efter',
        equals       : 'Lika med',
        lessThan     : 'Mindre än',
        moreThan     : 'Större än',
        removeFilter : 'Ta bort filter'
    },

    FilterBar : {
        enableFilterBar  : 'Visa filterrad',
        disableFilterBar : 'Dölj filterrad'
    },

    Group : {
        groupAscending       : 'Gruppera stigande',
        groupDescending      : 'Gruppera fallande',
        groupAscendingShort  : 'Stigande',
        groupDescendingShort : 'Fallande',
        stopGrouping         : 'Sluta gruppera',
        stopGroupingShort    : 'Sluta'
    },

    Search : {
        searchForValue : 'Sök efter värde'
    },

    Sort : {
        sortAscending          : 'Sortera stigande',
        sortDescending         : 'Sortera fallande',
        multiSort              : 'Multisortering',
        addSortAscending       : 'Lägg till stigande',
        addSortDescending      : 'Lägg till fallande',
        toggleSortAscending    : 'Ändra till stigande',
        toggleSortDescending   : 'Ändra till fallande',
        removeSorter           : 'Ta bort sorterare',
        sortAscendingShort     : 'Stigande',
        sortDescendingShort    : 'Fallande',
        removeSorterShort      : 'Ta bort',
        addSortAscendingShort  : '+ Stigande',
        addSortDescendingShort : '+ Fallande'
    },

    Tree : {
        noTreeColumn : 'För att använda featuren tree måste en kolumn vara konfigurerad med tree: true'
    },

    //endregion

    //region Grid

    Grid : {
        featureNotFound          : data => `Featuren '${data}' är inte tillgänglig, kontrollera att den är importerad`,
        invalidFeatureNameFormat : data => `Ogiltigt funktionsnamn '${data}' måste börja med en liten bokstav`,
        removeRow                : 'Ta bort rad',
        removeRows               : 'Ta bort rader',
        loadMask                 : 'Laddar...',
        loadFailedMessage        : 'Ett fel har uppstått, vänligen försök igen.',
        moveColumnLeft           : 'Flytta till vänstra sektionen',
        moveColumnRight          : 'Flytta till högra sektionen',
        noRows                   : 'Inga rader att visa'
    },

    //endregion

    //region Widgets

    Field : {
        invalidValue          : 'Ogiltigt värde',
        minimumValueViolation : 'För lågt värde',
        maximumValueViolation : 'För högt värde',
        fieldRequired         : 'Detta fält är obligatoriskt',
        validateFilter        : 'Värdet måste väljas från listan'
    },

    DateField : {
        invalidDate : 'Ogiltigt datum'
    },

    TimeField : {
        invalidTime : 'Ogiltig tid'
    },

    //endregion

    //region Others

    DateHelper : {
        locale       : 'sv-SE',
        shortWeek    : 'V',
        shortQuarter : 'q',
        week         : 'Vecka',
        weekStartDay : 1,
        unitNames    : [
            { single : 'millisekund', plural : 'millisekunder', abbrev : 'ms' },
            { single : 'sekund', plural : 'sekunder', abbrev : 's' },
            { single : 'minut', plural : 'minuter', abbrev : 'min' },
            { single : 'timme', plural : 'timmar', abbrev : 'tim' },
            { single : 'dag', plural : 'dagar', abbrev : 'd' },
            { single : 'vecka', plural : 'vecka', abbrev : 'v' },
            { single : 'månad', plural : 'månader', abbrev : 'mån' },
            { single : 'kvartal', plural : 'kvartal', abbrev : 'kv' },
            { single : 'år', plural : 'år', abbrev : 'år' }
        ],
        // Used to build a RegExp for parsing time units.
        // The full names from above are added into the generated Regexp.
        // So you may type "2 v" or "2 ve" or "2 vecka" or "2 vecka" into a DurationField.
        // When generating its display value though, it uses the full localized names above.
        unitAbbreviations : [
            ['ms', 'mil'],
            ['s', 'sek'],
            ['m', 'min'],
            ['t', 'tim', 'h'],
            ['d'],
            ['v', 've'],
            ['må', 'mån'],
            ['kv', 'kva'],
            []
        ],
        ordinalSuffix : number => {
            const lastDigit = number[number.length - 1];
            return number + (number !== '11' && number !== '12' && (lastDigit === '1' || lastDigit === '2') ? 'a' : 'e');
        },
        parsers : {
            'L'  : 'YYYY-MM-DD',
            'LT' : 'HH:mm'
        }
    },

    BooleanCombo : {
        'Yes' : 'Ja',
        'No'  : 'Nej'
    }

    //endregion
};

export default locale;

LocaleManager.registerLocale('SvSE', { desc : 'Svenska', path : 'lib/Common/localization/SvSE.js', locale : locale });
