import LocaleManager from '../../Common/localization/LocaleManager.js';

const locale = {

    localeName : 'Ru',
    localeDesc : 'Russian',

    //region Columns

    TemplateColumn : {
        noTemplate : 'TemplateColumn необходим шаблон',
        noFunction : 'TemplateColumn.template должен быть функцией'
    },

    ColumnStore : {
        columnTypeNotFound : data => `Тип колонки '${data.type}' не зарегистрирован`
    },

    //endregion

    //region Mixins

    InstancePlugin : {
        fnMissing         : data => `Пытаемся связать метод ${data.plugIntoName}#${data.fnName}, но в плагине не был найден метод ${data.pluginName}#${data.fnName}`,
        overrideFnMissing : data => `Пытаемся перегрузить метод ${data.plugIntoName}#${data.fnName}, но в плагине не был найден метод ${data.pluginName}#${data.fnName}`
    },

    //endregion

    //region Features
    ColumnPicker : {
        columnsMenu     : 'Колонки',
        hideColumn      : 'Спрятать колонку',
        hideColumnShort : 'Спрятать'
    },

    Filter : {
        applyFilter  : 'Применить фильтр',
        filter       : 'Фильтр',
        editFilter   : 'Изменить фильтр',
        on           : 'В этот день',
        before       : 'До',
        after        : 'После',
        equals       : 'Равно',
        lessThan     : 'Меньше, чем',
        moreThan     : 'Больше, чем',
        removeFilter : 'Убрать фильтр'
    },

    FilterBar : {
        enableFilterBar  : 'Показать панель фильтров',
        disableFilterBar : 'Спрятать панель фильтров'
    },

    Group : {
        groupAscending       : 'Группа по возрастанию',
        groupDescending      : 'Группа по убыванию',
        groupAscendingShort  : 'Возрастание',
        groupDescendingShort : 'Убывание',
        stopGrouping         : 'Убрать группу',
        stopGroupingShort    : 'Убрать'
    },

    Search : {
        searchForValue : 'Найти значение'
    },

    Sort : {
        'sortAscending'          : 'Сортировать по возрастанию',
        'sortDescending'         : 'Сортировать по убыванию',
        'multiSort'              : 'Сложная сортировка',
        'removeSorter'           : 'Убрать сортировку',
        'addSortAscending'       : 'Добавить сортировку по возрастанию',
        'addSortDescending'      : 'Добавить сортировку по убыванию',
        'toggleSortAscending'    : 'Сортировать по возрастанию',
        'toggleSortDescending'   : 'Сортировать по убыванию',
        'sortAscendingShort'     : 'Возрастание',
        'sortDescendingShort'    : 'Убывание',
        'removeSorterShort'      : 'Убрать',
        'addSortAscendingShort'  : '+ Возраст...',
        'addSortDescendingShort' : '+ Убыв...'

    },

    Tree : {
        noTreeColumn : 'Чтобы использовать дерево необходимо чтобы одна колонка имела настройку tree: true'
    },

    //endregion

    //region Grid

    Grid : {
        featureNotFound          : data => `Опция '${data}' недоступна, убедитесь что она импортирована`,
        invalidFeatureNameFormat : data => `Неверное имя функциональности '${data}', так как оно должно начинаться с маленькой буквы`,
        removeRow                : 'Удалить строку',
        removeRows               : 'Удалить строки',
        loadMask                 : 'Загрузка...',
        loadFailedMessage        : 'Не удалось загрузить',
        moveColumnLeft           : 'Передвинуть в левую секцию',
        moveColumnRight          : 'Передвинуть в правую секцию',
        noRows                   : 'Нет строк для отображения'
    },

    //endregion

    //region Widgets

    Field : {
        invalidValue          : 'Недопустимое значение поля',
        minimumValueViolation : 'Нарушение минимального значения',
        maximumValueViolation : 'Нарушение максимального значения',
        fieldRequired         : 'Поле не может быть пустым',
        validateFilter        : 'Выберите значение из списка'
    },

    DateField : {
        invalidDate : 'Невернывй формат даты'
    },

    TimeField : {
        invalidTime : 'Неверный формат времени'
    },

    //endregion

    //region Others

    DateHelper : {
        locale       : 'ru',
        shortWeek    : 'нед',
        shortQuarter : 'квар',
        week         : 'Hеделя',
        weekStartDay : 1,
        unitNames    : [
            { single : 'миллисек', plural : 'миллисек',  abbrev : 'мс' },
            { single : 'секунда',  plural : 'секунд',    abbrev : 'с' },
            { single : 'минута',   plural : 'минут',     abbrev : 'мин' },
            { single : 'час',      plural : 'часов',     abbrev : 'ч' },
            { single : 'день',     plural : 'дней',      abbrev : 'д' },
            { single : 'неделя',   plural : 'недели',    abbrev : 'нед' },
            { single : 'месяц',    plural : 'месяцев',   abbrev : 'мес' },
            { single : 'квартал',  plural : 'кварталов', abbrev : 'квар' },
            { single : 'год',      plural : 'лет',       abbrev : 'г' }
        ],
        // Used to build a RegExp for parsing time units.
        // The full names from above are added into the generated Regexp.
        // So you may type "2 н" or "2 нед" or "2 неделя" or "2 недели" into a DurationField.
        // When generating its display value though, it uses the full localized names above.
        unitAbbreviations : [
            ['мс', 'мил'],
            ['с', 'сек'],
            ['м', 'мин'],
            ['ч'],
            ['д', 'ден', 'дне'],
            ['н', 'нед'],
            ['мес'],
            ['к', 'квар', 'квр'],
            ['г']
        ],
        parsers : {
            'L'  : 'DD.MM.YYYY',
            'LT' : 'HH:mm'
        },
        ordinalSuffix : number => `${number}-й`
    },

    BooleanCombo : {
        'Yes' : 'Да',
        'No'  : 'Нет'
    }

    //endregion
};

export default locale;

LocaleManager.registerLocale('Ru', { desc : 'Русский', path : 'lib/Common/localization/Ru.js', locale : locale });
