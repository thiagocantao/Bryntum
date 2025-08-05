import LocaleHelper from '../../../lib/Core/localization/LocaleHelper.js';
import '../../../lib/Gantt/localization/Ru.js';
import './shared.locale.Ru.js';

const locale = {

    localeName : 'Ru',
    localeDesc : 'Русский',
    localeCode : 'ru',

    Baselines : {
        Start              : 'Начало',
        End                : 'Конец',
        Duration           : 'Длительность',
        Complete           : 'Выполнено',
        baseline           : 'исходный план',
        'Delayed start by' : 'Задержка старта на',
        'Overrun by'       : 'Перерасход времени на'
    },

    Button : {
        Create           : 'Создать',
        'Critical paths' : 'Критические пути',
        Edit             : 'Pедактировать',
        'Export to PDF'  : 'Экспорт в PDF',
        Features         : 'Функции',
        Settings         : 'Настройки'
    },

    DateColumn : {
        Deadline : 'Крайний срок'
    },

    Field : {
        'Find tasks by name' : 'Найти задачи по названию',
        'Project start'      : 'Старт проекта'
    },

    GanttToolbar : {
        'First select the task you want to edit' : 'Сначала выберите задачу, которую хотите отредактировать',
        'New task'                               : 'Новое задание'
    },

    Indicators : {
        Indicators     : 'Индикаторы',
        constraintDate : 'Ограничение'
    },

    MenuItem : {
        'Draw dependencies'          : 'Зависимости задач',
        'Enable cell editing'        : 'Редактирование ячеек',
        'Hide schedule'              : 'Скрыть расписание',
        'Highlight non-working time' : 'Выделить нерабочее время',
        'Project lines'              : 'Границы проекта',
        'Show baselines'             : 'Исходные планы',
        'Show progress line'         : 'Линия прогресса',
        'Show rollups'               : 'Сводки задач',
        'Task labels'                : 'Ярлыки задач'
    },

    Slider : {
        'Animation duration ' : 'Время анимации',
        'Bar margin'          : 'Отступ задачи',
        'Row height'          : 'Высота строки'
    },

    StartDateColumn : {
        'Start date' : 'Дата начала'
    },

    StatusColumn : {
        Status : 'Статус'
    },

    TaskTooltip : {
        'Scheduling Mode' : 'Тип планирования',
        Calendar          : 'Календарь',
        Critical          : 'Критично'
    },

    Tooltip : {
        'Adjust settings'          : 'Изменить настройки',
        'Collapse all'             : 'Свернуть все задачи',
        'Create new task'          : 'Создать новую задачу',
        'Edit selected task'       : 'Редактировать выбранную задачу',
        'Expand all'               : 'Раскрыть все задачи',
        'Highlight critical paths' : 'Выделить критические пути',
        'Next time span'           : 'Следующий интервал',
        'Previous time span'       : 'Предыдущий интервал',
        'Toggle features'          : 'Переключить функции',
        'Zoom in'                  : 'Приблизить',
        'Zoom out'                 : 'Уменьшить',
        'Zoom to fit'              : 'Масштаб по содержимому'
    }
};

export default LocaleHelper.publishLocale(locale);
