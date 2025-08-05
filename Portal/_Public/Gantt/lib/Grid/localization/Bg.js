import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Bg.js';

const emptyString = new String();

const locale = {

    localeName : 'Bg',
    localeDesc : 'Български',
    localeCode : 'bg',

    ColumnPicker : {
        column          : 'Колона',
        columnsMenu     : 'Колони',
        hideColumn      : 'Скриване на колона',
        hideColumnShort : 'Скриване',
        newColumns      : 'Нова колона'
    },

    Filter : {
        applyFilter   : 'Прилагане на филтър',
        filter        : 'Филтри',
        editFilter    : 'Редактиране на филтър',
        on            : 'Вкл.',
        before        : 'Преди',
        after         : 'След',
        equals        : 'Равно',
        lessThan      : 'По-малко от',
        moreThan      : 'Повече от',
        removeFilter  : 'Премахване на филтър',
        disableFilter : 'Деактивиране на филтъра'
    },

    FilterBar : {
        enableFilterBar  : 'Показване на лентата с филтри',
        disableFilterBar : 'Скриване на лентата с филтри'
    },

    Group : {
        group                : 'Група',
        groupAscending       : 'Възходяща група',
        groupDescending      : 'Низходящ група',
        groupAscendingShort  : 'Възходящ',
        groupDescendingShort : 'Низходящ',
        stopGrouping         : 'Стоп на групиране',
        stopGroupingShort    : 'Стоп'
    },

    HeaderMenu : {
        moveBefore     : text => `Преместване преди "${text}"`,
        moveAfter      : text => `Преместване след "${text}"`,
        collapseColumn : 'Свиване на колона',
        expandColumn   : 'Разширяване на колона'
    },

    ColumnRename : {
        rename : 'Преименуване'
    },

    MergeCells : {
        mergeCells  : 'Сливане на клетки',
        menuTooltip : 'Сливане на клетки с една и съща стойност при сортиране по тази колона'
    },

    Search : {
        searchForValue : 'Търсене на стойност'
    },

    Sort : {
        sort                   : 'Сортиране',
        sortAscending          : 'Сортиране във възходящ ред',
        sortDescending         : 'Сортиране в низходящ ред',
        multiSort              : 'Мулти сортиране',
        removeSorter           : 'Премахване на сортировач',
        addSortAscending       : 'Добавяне на възходящ сортировач',
        addSortDescending      : 'Добавяне на низходящ сортировач',
        toggleSortAscending    : 'Промяна към възходящ',
        toggleSortDescending   : 'Промяна към низходящ',
        sortAscendingShort     : 'Възходящ',
        sortDescendingShort    : 'Низходящ',
        removeSorterShort      : 'Отстраняване',
        addSortAscendingShort  : '+ Възходящ',
        addSortDescendingShort : '+ Низходящ'
    },

    Split : {
        split        : 'Разделено',
        unsplit      : 'Неразделено',
        horizontally : 'Хоризонтално',
        vertically   : 'Вертикално',
        both         : 'И двете'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} колона. ` : ''}SPACE за контекстно меню${column.sortable ? ', ENTER за сортиране' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Превключване на избора на ред',
        toggleSelection : 'Превключване на избора на целия набор от данни'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `рейтинг : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Неуспешно зареждане на данни!',
        syncFailedMessage  : 'Неуспешна синхронизация за данни!',
        unspecifiedFailure : 'Неуточнена неизправност',
        networkFailure     : 'Грешка в мрежата',
        parseFailure       : 'Неуспешен анализ на отговора на сървъра',
        serverResponse     : 'Отговор на сървъра:',
        noRows             : 'Няма записи за показване',
        moveColumnLeft     : 'Преместване в лявата част',
        moveColumnRight    : 'Преместване в дясната част',
        moveColumnTo       : region => `Преместване на колоната в ${region}`
    },

    CellMenu : {
        removeRow : 'Изтриване'
    },

    RowCopyPaste : {
        copyRecord  : 'Копирай',
        cutRecord   : 'Изрежи',
        pasteRecord : 'Постави',
        rows        : 'редове',
        row         : 'ред'
    },

    CellCopyPaste : {
        copy  : 'Копиране',
        cut   : 'Изрязване',
        paste : 'Вмъкване'
    },

    PdfExport : {
        'Waiting for response from server' : 'В очакване на отговор от сървъра...',
        'Export failed'                    : 'Неуспешен експорт',
        'Server error'                     : 'Грешка в сървъра',
        'Generating pages'                 : 'Генериране на страници...',
        'Click to abort'                   : 'Отказ'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Настройки на експорта',
        export         : 'Експорт',
        exporterType   : 'Контрол на странирането',
        cancel         : 'Отказ',
        fileFormat     : 'Файлов формат',
        rows           : 'Редове',
        alignRows      : 'Подравняване на редовете',
        columns        : 'Колони',
        paperFormat    : 'Формат на документа',
        orientation    : 'Ориентация',
        repeatHeader   : 'Повтаряне на заглавката'
    },

    ExportRowsCombo : {
        all     : 'Всички редове',
        visible : 'Видими редове'
    },

    ExportOrientationCombo : {
        portrait  : 'Портрет',
        landscape : 'Пейзаж'
    },

    SinglePageExporter : {
        singlepage : 'Единична страница'
    },

    MultiPageExporter : {
        multipage     : 'Няколко страници',
        exportingPage : ({ currentPage, totalPages }) => `Експортиране на страница ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Няколко страници (вертикално)',
        exportingPage     : ({ currentPage, totalPages }) => `Експортиране на страница ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Зареждане',
        expand   : 'Разгръщане',
        collapse : 'Свиване'
    },

    TreeGroup : {
        group                  : 'Групиране по',
        stopGrouping           : 'Спиране на групирането',
        stopGroupingThisColumn : 'Разгрупиране на колона'
    }
};

export default LocaleHelper.publishLocale(locale);
