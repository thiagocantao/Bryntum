import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Bg.js';
import '../../Scheduler/localization/Bg.js';

const locale = {

    localeName : 'Bg',
    localeDesc : 'Български',
    localeCode : 'bg',

    ConstraintTypePicker : {
        none                : 'Няма',
        assoonaspossible    : 'Възможно най-скоро',
        aslateaspossible    : 'Възможно най-късно',
        muststarton         : 'Трябва да започне на',
        mustfinishon        : 'Трябва да свърши на',
        startnoearlierthan  : 'Начало не по-рано от',
        startnolaterthan    : 'Начало не по-късно от',
        finishnoearlierthan : 'Край не по-рано от',
        finishnolaterthan   : 'Край не по-късно от'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Напред',
        Backward      : 'Назад',
        inheritedFrom : 'Наследени от',
        enforcedBy    : 'Наложени от'
    },

    CalendarField : {
        'Default calendar' : 'Календар по подразбиране'
    },

    TaskEditorBase : {
        Information   : 'Информация',
        Save          : 'Запис',
        Cancel        : 'Отказ',
        Delete        : 'Изтриване',
        calculateMask : 'Изчисляване...',
        saveError     : 'Невъзможен запис, моля, първо коригирайте грешките',
        repeatingInfo : 'Преглед на повтарящо се събитие',
        editRepeating : 'Редактиране'
    },

    TaskEdit : {
        'Edit task'            : 'Редактиране на задача',
        ConfirmDeletionTitle   : 'Потвърждаване на изтриване',
        ConfirmDeletionMessage : 'Сигурни ли сте, че желаете да изтриете събитието?'
    },

    GanttTaskEditor : {
        editorWidth : '50em'
    },

    SchedulerTaskEditor : {
        editorWidth : '35em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Обща информация',
        Name         : 'Име',
        Resources    : 'Ресурси',
        '% complete' : '% завършени',
        Duration     : 'Продължителност',
        Start        : 'Старт',
        Finish       : 'Край',
        Effort       : 'Усилие',
        Preamble     : 'Увод',
        Postamble    : 'Послеслов'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Обща информация',
        Name         : 'Име',
        '% complete' : '% завършени',
        Duration     : 'Продължителност',
        Start        : 'Начало',
        Finish       : 'Край',
        Effort       : 'Усилие',
        Dates        : 'Дати'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Разширени',
        Calendar                   : 'Календар',
        'Scheduling mode'          : 'Режим на планиране',
        'Effort driven'            : 'Effort driven',
        'Manually scheduled'       : 'Ръчно планирано',
        'Constraint type'          : 'Тип ограничение',
        'Constraint date'          : 'Дата на ограничението',
        Inactive                   : 'Неактивен',
        'Ignore resource calendar' : 'Игнориране на ресурсния календар'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Разширени',
        Calendar                   : 'Календар',
        'Scheduling mode'          : 'Режим на планиране',
        'Effort driven'            : 'Effort driven',
        'Manually scheduled'       : 'Ръчно планирано',
        'Constraint type'          : 'Тип ограничение',
        'Constraint date'          : 'Дата на ограничението',
        Constraint                 : 'Ограничение',
        Rollup                     : 'Сводка',
        Inactive                   : 'Неактивен',
        'Ignore resource calendar' : 'Игнориране на ресурсния календар',
        'Scheduling direction'     : 'Насочване на графика'
    },

    DependencyTab : {
        Predecessors      : 'Предшественици',
        Successors        : 'Приемници',
        ID                : 'Идентификатор',
        Name              : 'Име',
        Type              : 'Тип',
        Lag               : 'Забавяне',
        cyclicDependency  : 'Циклична зависимост',
        invalidDependency : 'Невалидна зависимост'
    },

    NotesTab : {
        Notes : 'Бележки'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ресурси',
        Resource  : 'Ресурс',
        Units     : 'Единици'
    },

    RecurrenceTab : {
        title : 'Повторение'
    },

    SchedulingModePicker : {
        Normal           : 'Нормално',
        'Fixed Duration' : 'Фиксирана продължителност',
        'Fixed Units'    : 'Фиксирани единици',
        'Fixed Effort'   : 'Фиксирано усилие'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} от {available}</span> са разпределени',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} от {available}</span> са разпределени',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} от {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} от {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'На {startDate}<br><span class="{cls}">{allocated} от {available}</span> са разпределени:<br>{assignments}',
        plusMore              : '+{value} още'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> са разпределени',
        barTipOnDate          : '<b>{event}</b> на {startDate}<br><span class="{cls}">{allocated}</span> са разпределени',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} от {available}</span> са разпределени:<br>{assignments}',
        groupBarTipOnDate     : 'На {startDate}<br><span class="{cls}">{allocated} от {available}</span> са разпределени:<br>{assignments}',
        plusMore              : '+{value} още',
        nameColumnText        : 'Ресурс / събитие'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Отмяна на промяната и не предприемай нищо',
        schedulingConflict : 'Конфликт при планирането',
        emptyCalendar      : 'Грешка в конфигурацията на календара',
        cycle              : 'Цикъл на планиране',
        Apply              : 'Приложи'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Моля, изберете зависимост:',
        invalidDependencyLabel : 'Съществуват невалидни зависимости, които трябва да бъдат разгледани:'
    },

    DependencyEdit : {
        Active : 'Активен'
    },

    SchedulerProBase : {
        propagating     : 'Изчисляване на проект',
        storePopulation : 'Зареждане на данни...',
        finalizing      : 'Финализиране на резултатите'
    },

    EventSegments : {
        splitEvent    : 'Разделяне на събитие',
        renameSegment : 'Преименуване'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Не е разрешено премахването',
        nestingNotAllowed   : 'Вмъкването не е разрешено'
    },

    VersionGrid : {
        compare       : 'Сравни',
        description   : 'Описание',
        occurredAt    : 'Случи се в',
        rename        : 'Преименуване',
        restore       : 'Възстановяване',
        stopComparing : 'Прекрати сравняването'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'задача',
            AssignmentModel : 'възложена задача',
            DependencyModel : 'връзка',
            ProjectModel    : 'проект',
            ResourceModel   : 'ресурс',
            other           : 'обект'
        },
        entityNamesPlural : {
            TaskModel       : 'задачи',
            AssignmentModel : 'възложени задачи',
            DependencyModel : 'връзки',
            ProjectModel    : 'проекти',
            ResourceModel   : 'ресурси',
            other           : 'обекти'
        },
        transactionDescriptions : {
            update : 'Променени {n} {entities}',
            add    : 'Добавени {n} {entities}',
            remove : 'Премахнати {n} {entities}',
            move   : 'Преместени {n} {entities}',
            mixed  : 'Променени {n} {entities}'
        },
        addEntity         : 'Добавено {type} **{name}**',
        removeEntity      : 'Премахнато {type} **{name}**',
        updateEntity      : 'Променено {type} **{name}**',
        moveEntity        : 'Преместено {type} **{name}** от {from} до {to}',
        addDependency     : 'Добавена е връзка от **{from}** до **{to}**',
        removeDependency  : 'Премахната е връзка от **{from}** до **{to}**',
        updateDependency  : 'Редактирана е връзка от **{from}** до **{to}**',
        addAssignment     : 'Възложен **{resource}** на **{event}**',
        removeAssignment  : 'Премахната е възложена задача от **{resource}** от **{event}**',
        updateAssignment  : 'Редактирана е възложена задача от **{resource}** от **{event}**',
        noChanges         : 'Без промяна',
        nullValue         : 'няма',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Неотменени промени',
        redid             : 'Повторни промени',
        editedTask        : 'Редактирани свойства на задачата',
        deletedTask       : 'Изтрита е задача',
        movedTask         : 'Преместена е задача',
        movedTasks        : 'Преместени задачи'
    }
};

export default LocaleHelper.publishLocale(locale);
