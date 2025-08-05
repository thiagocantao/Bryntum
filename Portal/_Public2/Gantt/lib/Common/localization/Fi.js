import LocaleManager from '../../Common/localization/LocaleManager.js';

const locale = {

    localeName : 'Fi',
    localeDesc : 'Suomi',

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

    CellEdit : {
        editCell : 'Redigera cell'
    },

    ColumnPicker : {
        columnsMenu     : 'Kolumner',
        hideColumn      : 'Dölj kolumn',
        hideColumnShort : 'Dölj',
        Column          : 'Kolumn'
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
        stopGroupingShort    : 'Sluta',
        Group                : 'Gruppera'
    },

    Search : {
        searchForValue : 'Sök efter värde'
    },

    Sort : {
        sortAscending          : 'Lajittele nouseva',
        sortDescending         : 'Lajittele laskevasti',
        multiSort              : 'Multisortering',
        addSortAscending       : 'Lägg till stigande',
        addSortDescending      : 'Lägg till fallande',
        removeSorter           : 'Ta bort sorterare',
        sortAscendingShort     : 'Stigande',
        sortDescendingShort    : 'Fallande',
        removeSorterShort      : 'Ta bort',
        addSortAscendingShort  : '+ Stigande',
        addSortDescendingShort : '+ Fallande',
        Sort                   : 'Sortera',
        Multisort              : 'Multisortera'
    },

    Tree : {
        noTreeColumn : 'För att använda featuren tree måste en kolumn vara konfigurerad med tree: true'
    },

    //endregion

    //region Grid

    Grid : {
        featureNotFound   : data => `Featuren '${data}' är inte tillgänglig, kontrollera att den är importerad`,
        removeRow         : 'Ta bort rad',
        removeRows        : 'Ta bort rader',
        loadMask          : 'Laddar...',
        loadFailedMessage : 'Ett fel har uppstått.',
        moveColumnLeft    : 'Siirry vasemmalle osalle',
        moveColumnRight   : 'Siirry oikeaan osaan',
        noRows            : 'Ei näytettäviä rivejä'
    },

    //endregion

    //region Widgets

    Field : {
        invalidValue          : 'Invalid field value',
        minimumValueViolation : 'Minimum value violation',
        maximumValueViolation : 'Maximum value violation',
        fieldRequired         : 'This field is required',
        validateFilter        : 'Value must be selected from the list'
    },

    DateField : {
        invalidDate : 'Invalid date input'
    },

    TimeField : {
        invalidTime : 'Invalid time input'
    },

    Tooltip : {
        'Loading...' : 'Laddar...'
    },

    //endregion

    //region Others

    // TODO: Correct this locale, it's copied from SvSE
    DateHelper : {
        locale       : 'fi',
        shortWeek    : 'V',
        shortQuarter : 'q',
        unitNames    : [
            { single : 'ms',      plural : 'ms',       abbrev : 'ms' },
            { single : 'sekund',  plural : 'sekunder', abbrev : 's' },
            { single : 'minut',   plural : 'minuter',  abbrev : 'min' },
            { single : 'timme',   plural : 'timmar',   abbrev : 'tim' },
            { single : 'dag',     plural : 'dagar',    abbrev : 'd' },
            { single : 'vecka',   plural : 'veckor',   abbrev : 'v' },
            { single : 'månad',   plural : 'månader',  abbrev : 'mån' },
            { single : 'kvartal', plural : 'kvartal',  abbrev : 'kv' },
            { single : 'år',      plural : 'år',       abbrev : 'år' }
        ],
        // Used to build a RegExp for parsing time units.
        // The full names from above are added into the generated Regexp.
        // So you may type "2 v" or "2 ve" or "2 vecka" or "2 veckor" into a DurationField.
        // When generating its display value though, it uses the full localized names above.
        unitAbbreviations : [
            ['mil'],
            ['s', 'sek'],
            ['m', 'min'],
            ['t', 'tim'],
            ['d'],
            ['v', 've'],
            ['må', 'mån'],
            ['kv', 'kva'],
            []
        ]
    }

    //endregion
};

export default locale;

LocaleManager.registerLocale('Fi', { desc : 'Suomi',  path : 'lib/Common/localization/Fi.js', locale : locale });
