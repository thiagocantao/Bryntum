import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ru.js';
import '../../Scheduler/localization/Ru.js';

const locale = {

    localeName : 'Ru',
    localeDesc : 'Русский',
    localeCode : 'ru',

    ConstraintTypePicker : {
        none                : 'Нет',
        assoonaspossible    : 'Как можно скорее',
        aslateaspossible    : 'Как можно позже',
        muststarton         : 'Фиксированное начало',
        mustfinishon        : 'Фиксированное окончание',
        startnoearlierthan  : 'Начало не раньше',
        startnolaterthan    : 'Начало не позднее',
        finishnoearlierthan : 'Окончание не раньше',
        finishnolaterthan   : 'Окончание не позднее'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Вперед',
        Backward      : 'Назад',
        inheritedFrom : 'Унаследовано от',
        enforcedBy    : 'Задано от'
    },

    CalendarField : {
        'Default calendar' : 'Основной календарь'
    },

    TaskEditorBase : {
        Information   : 'Информация',
        Save          : 'Сохранить',
        Cancel        : 'Отменить',
        Delete        : 'Удалить',
        calculateMask : 'Рассчитываю задачи...',
        saveError     : 'Сохранение невозможно, исправьте ошибки',
        repeatingInfo : 'Просмотр повторяющегося события',
        editRepeating : 'Редактировать'
    },

    TaskEdit : {
        'Edit task'            : 'Изменить задачу',
        ConfirmDeletionTitle   : 'Подтвердите удаление',
        ConfirmDeletionMessage : 'Вы уверены, что хотите удалить событие?'
    },

    GanttTaskEditor : {
        editorWidth : '54em'
    },

    SchedulerTaskEditor : {
        editorWidth : '41em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '9em',
        General      : 'Основные',
        Name         : 'Имя',
        Resources    : 'Ресурсы',
        '% complete' : '% выполнено',
        Duration     : 'Длительность',
        Start        : 'Начало',
        Finish       : 'Окончание',
        Effort       : 'Трудозатраты',
        Preamble     : 'Вхождение',
        Postamble    : 'Выход'
    },

    GeneralTab : {
        labelWidth   : '9em',
        General      : 'Основные',
        Name         : 'Имя',
        '% complete' : '% выполнено',
        Duration     : 'Длительность',
        Start        : 'Начало',
        Finish       : 'Окончание',
        Effort       : 'Трудозатраты',
        Dates        : 'Даты'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Дополнительно',
        Calendar                   : 'Календарь',
        'Scheduling mode'          : 'Тип планирования',
        'Effort driven'            : 'Управляемое трудозатратами',
        'Manually scheduled'       : 'Ручное планирование',
        'Constraint type'          : 'Тип ограничения',
        'Constraint date'          : 'Дата ограничения',
        Inactive                   : 'Неактивна',
        'Ignore resource calendar' : 'Не учитывать календарь ресурса'
    },

    AdvancedTab : {
        labelWidth                 : '18em',
        Advanced                   : 'Дополнительные',
        Calendar                   : 'Календарь',
        'Scheduling mode'          : 'Тип планирования',
        'Effort driven'            : 'Управляемое трудозатратами',
        'Manually scheduled'       : 'Ручное планирование',
        'Constraint type'          : 'Тип ограничения',
        'Constraint date'          : 'Дата ограничения',
        Constraint                 : 'Ограничение',
        Rollup                     : 'Сведение',
        Inactive                   : 'Неактивна',
        'Ignore resource calendar' : 'Не учитывать календарь ресурса',
        'Scheduling direction'     : 'Направление планирования'
    },

    DependencyTab : {
        Predecessors      : 'Предшественники',
        Successors        : 'Последователи',
        ID                : 'Идентификатор',
        Name              : 'Имя',
        Type              : 'Тип',
        Lag               : 'Запаздывание',
        cyclicDependency  : 'Обнаружена цикличная зависимость',
        invalidDependency : 'Неверная зависимость'
    },

    NotesTab : {
        Notes : 'Заметки'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ресурсы',
        Resource  : 'Ресурс',
        Units     : '% Занятости'
    },

    RecurrenceTab : {
        title : 'Повтор'
    },

    SchedulingModePicker : {
        Normal           : 'Нормальный',
        'Fixed Duration' : 'Фиксированная длительность',
        'Fixed Units'    : 'Фиксированные единицы',
        'Fixed Effort'   : 'Фиксированные трудозатраты'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} из {available}</span> использовано',
        barTipOnDate          : '<b>{resource}</b> {startDate}<br><span class="{cls}">{allocated} из {available}</span> использовано',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} из {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} из {available}</span> использовано:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}">{allocated} из {available}</span> использовано:<br>{assignments}',
        plusMore              : 'Еще +{value}'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> использовано',
        barTipOnDate          : '<b>{event}</b> {startDate}<br><span class="{cls}">{allocated}</span> использовано',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} из {available}</span> использовано:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}">{allocated} из {available}</span> использовано:<br>{assignments}',
        plusMore              : 'Еще +{value}',
        nameColumnText        : 'Ресурс / Событие'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Отменить изменения',
        schedulingConflict : 'Конфликт планирования',
        emptyCalendar      : 'Ошибка данных календаря',
        cycle              : 'Цикл планирования',
        Apply              : 'Применить'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Пожалуйста выберите зависимость для исправления:',
        invalidDependencyLabel : 'Есть неверные зависимости которые необходимо исправить:'
    },

    DependencyEdit : {
        Active : 'Действующая'
    },

    SchedulerProBase : {
        propagating     : 'Расчет проекта',
        storePopulation : 'Загрузка данных',
        finalizing      : 'Завершение'
    },

    EventSegments : {
        splitEvent    : 'Прервать событие',
        renameSegment : 'Переименовать'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Извлечение запрещено',
        nestingNotAllowed   : 'Вложение запрещено'
    },

    VersionGrid : {
        compare       : 'Сравнивать',
        description   : 'Описание',
        occurredAt    : 'Произошло в',
        rename        : 'Переименовать',
        restore       : 'Восстановить',
        stopComparing : 'Прекратить сравнение'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'задача',
            AssignmentModel : 'назначение',
            DependencyModel : 'связь',
            ProjectModel    : 'проект',
            ResourceModel   : 'ресурс',
            other           : 'объект'
        },
        entityNamesPlural : {
            TaskModel       : 'задач',
            AssignmentModel : 'назначений',
            DependencyModel : 'связей',
            ProjectModel    : 'проектов',
            ResourceModel   : 'русурсов',
            other           : 'объектов'
        },
        transactionDescriptions : {
            update : 'Изменено: {n} {entities}',
            add    : 'Добавлено: {n} {entities}',
            remove : 'Удалено: {n} {entities}',
            move   : 'Перемещено {n} {entities}',
            mixed  : 'Изменено: {n} {entities}'
        },
        addEntity         : 'Добавлено: {type} {name}',
        removeEntity      : 'Удалено: {type} {name}',
        updateEntity      : 'Изменено: {type} {name}',
        moveEntity        : 'Перемещен {type} {name} от {from} к {to}',
        addDependency     : 'Добавлена свзяь {from} с {to}',
        removeDependency  : 'Удалена связь {from} с {to}',
        updateDependency  : 'Изменена свзяь {from} с {to}',
        addAssignment     : 'Назначен {resource} для {event}',
        removeAssignment  : 'Удалено назначение {resource} для {event}',
        updateAssignment  : 'Изменено назначение {resource} для {event}',
        noChanges         : 'Нет изменений',
        nullValue         : 'ничто',
        versionDateFormat : 'D/M/YYYY HH:mm',
        undid             : 'Отмена изменений',
        redid             : 'Возвращение изменений',
        editedTask        : 'Изменены свойства задачи',
        deletedTask       : 'Удалена задача',
        movedTask         : 'Задача перемещена',
        movedTasks        : 'Задачи перемещены'
    }
};

export default LocaleHelper.publishLocale(locale);
