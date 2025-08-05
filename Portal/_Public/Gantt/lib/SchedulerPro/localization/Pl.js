import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Pl.js';
import '../../Scheduler/localization/Pl.js';

const locale = {

    localeName : 'Pl',
    localeDesc : 'Polski',
    localeCode : 'pl',

    ConstraintTypePicker : {
        none                : 'Żaden',
        assoonaspossible    : 'Najwcześniej jak to możliwe',
        aslateaspossible    : 'Najpóźniej jak to możliwe',
        muststarton         : 'Musi rozpocząć się dnia',
        mustfinishon        : 'Musi zakończyć się dnia',
        startnoearlierthan  : 'Rozpocznie się nie wcześniej niż',
        startnolaterthan    : 'Rozpocznie się nie później niż',
        finishnoearlierthan : 'Zakończy się nie wcześniej niż',
        finishnolaterthan   : 'Zakończy się nie później niż'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Do przodu',
        Backward      : 'Do tyłu',
        inheritedFrom : 'Odziedziczone z',
        enforcedBy    : 'Narzucone przez'
    },

    CalendarField : {
        'Default calendar' : 'Kalendarz domyślny'
    },

    TaskEditorBase : {
        Information   : 'Informacje',
        Save          : 'Zapisz',
        Cancel        : 'Anuluj',
        Delete        : 'Usuń',
        calculateMask : 'Liczenie…',
        saveError     : 'Nie można zapisać, proszę najpierw poprawić błędy',
        repeatingInfo : 'Wyświetlanie powtarzającego się zdarzenia',
        editRepeating : 'Edytuj'
    },

    TaskEdit : {
        'Edit task'            : 'Edytuj zadanie',
        ConfirmDeletionTitle   : 'Potwierdź usunięcie',
        ConfirmDeletionMessage : 'Czy na pewno chcesz usunąć to wydarzenie?'
    },

    GanttTaskEditor : {
        editorWidth : 'Czterdziesty czwarty'
    },

    SchedulerTaskEditor : {
        editorWidth : 'trzydziesty drugi'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Ogólne',
        Name         : 'Nazwa',
        Resources    : 'Zasoby',
        '% complete' : 'Kompletne w %',
        Duration     : 'Czas trwania',
        Start        : 'Początek',
        Finish       : 'Koniec',
        Effort       : 'Wysiłek',
        Preamble     : 'Preambuła',
        Postamble    : 'Postambuła'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Ogólne',
        Name         : 'Nazwa',
        '% complete' : 'Kompletny w %',
        Duration     : 'Czas trwania',
        Start        : 'Rozpocznij',
        Finish       : 'Zakończ',
        Effort       : 'Wysiłek',
        Dates        : 'Daty'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13',
        Advanced                   : 'Zaawansowany',
        Calendar                   : 'Kalendarz',
        'Scheduling mode'          : 'Tryb planowania',
        'Effort driven'            : 'Napędzany wysiłek',
        'Manually scheduled'       : 'Zaplanowany ręcznie',
        'Constraint type'          : 'Rodzaj ograniczenia',
        'Constraint date'          : 'Data ograniczenia',
        Inactive                   : 'Nieaktywny',
        'Ignore resource calendar' : 'Ignoruj kalendarz zasobów'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Zaawansowany',
        Calendar                   : 'Kalendarz',
        'Scheduling mode'          : 'Tryb planowania',
        'Effort driven'            : 'Napędzany wysiłek',
        'Manually scheduled'       : 'Zaplanowany ręcznie',
        'Constraint type'          : 'Rodzaj ograniczenia',
        'Constraint date'          : 'Data ograniczenia',
        Constraint                 : 'Ograniczenie',
        Rollup                     : 'Zestawienie',
        Inactive                   : 'Nieaktywny',
        'Ignore resource calendar' : 'Ignoruj kalendarz zasobów',
        'Scheduling direction'     : 'Kierunek harmonogramu'
    },

    DependencyTab : {
        Predecessors      : 'Poprzednie wydarzenia',
        Successors        : 'Następne wydarzenia',
        ID                : 'Identyfikator',
        Name              : 'Nazwa',
        Type              : 'Rodzaj',
        Lag               : 'Lag',
        cyclicDependency  : 'Cykliczna zależność',
        invalidDependency : 'Nieważna zależność'
    },

    NotesTab : {
        Notes : 'Uwagi'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Zasoby',
        Resource  : 'Zasób',
        Units     : 'Jednostki'
    },

    RecurrenceTab : {
        title : 'Powtórz'
    },

    SchedulingModePicker : {
        Normal           : 'Zwykły',
        'Fixed Duration' : 'Określony czas trwania',
        'Fixed Units'    : 'Określone jednostki',
        'Fixed Effort'   : 'Określony wysiłek'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> przydzielonych',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} z{available}</span> przydzielonych',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} z {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Dnia{startDate}<br><span class="{cls}">{allocated} z{available}</span> przydzielonych:<br>{assignments}',
        plusMore              : '+{value} więcej'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> przydzielony',
        barTipOnDate          : '<b>{event}</b> Dnia {startDate}<br><span class="{cls}">{allocated}</span> przydzielonych',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> przydzielonych:<br>{assignments}',
        groupBarTipOnDate     : 'Dnia {startDate}<br><span class="{cls}">{allocated} z{available}</span> przydzielonych<br>{assignments}',
        plusMore              : '+{value} więcej',
        nameColumnText        : 'Zasób/Zdarzenie'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Anuluj zmianę i nic więcej nie rób',
        schedulingConflict : 'Konflikt harmonogramów',
        emptyCalendar      : 'Błąd konfiguracji kalendarza',
        cycle              : 'Cykl harmonogramu',
        Apply              : 'Zastosuj'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Proszę wybrać zależność',
        invalidDependencyLabel : 'Istnieją nieprawidłowe zależności, które należy rozwiązać:'
    },

    DependencyEdit : {
        Active : 'Aktywny'
    },

    SchedulerProBase : {
        propagating     : 'Obliczanie projektu',
        storePopulation : 'Ładowanie danych',
        finalizing      : 'Finalizowanie wyników'
    },

    EventSegments : {
        splitEvent    : 'Podziel wydarzenie',
        renameSegment : 'Zmień nazwę'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Nie można cofnąć osadzenia',
        nestingNotAllowed   : 'Osadzenie nie jest możliwe'
    },

    VersionGrid : {
        compare       : 'Porównaj',
        description   : 'Opis',
        occurredAt    : 'Zdarzyło się',
        rename        : 'Zmień nazwę',
        restore       : 'Przywróć',
        stopComparing : 'Przestań porównywać'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'zadanie',
            AssignmentModel : 'przydział',
            DependencyModel : 'łącze',
            ProjectModel    : 'projekt',
            ResourceModel   : 'zasób',
            other           : 'obiekt'
        },
        entityNamesPlural : {
            TaskModel       : 'zadania',
            AssignmentModel : 'przydziały',
            DependencyModel : 'łącza',
            ProjectModel    : 'projekty',
            ResourceModel   : 'zasoby',
            other           : 'obiekty'
        },
        transactionDescriptions : {
            update : 'Zmieniono {n} {entities}',
            add    : 'Dodano {n} {entities}',
            remove : 'Usunięto {n} {entities}',
            move   : 'Przeniesiono {n} {entities}',
            mixed  : 'Zmieniono {n} {entities}'
        },
        addEntity         : 'Dodano {type} **{name}**',
        removeEntity      : 'Usunięto {type} **{name}**',
        updateEntity      : 'Zmieniono {type} **{name}**',
        moveEntity        : 'Przeniesiono {type} **{name}** z {from} do {to}',
        addDependency     : 'Dodano łącze z **{from}** do **{to}**',
        removeDependency  : 'Usunięto łącze z **{from}** do **{to}**',
        updateDependency  : 'Edytowano łącze z **{from}** do **{to}**',
        addAssignment     : 'Przydzielono **{resource}** do **{event}**',
        removeAssignment  : 'Usunięto przydziału **{resource}** od **{event}**',
        updateAssignment  : 'Edycja przydziału **{resource}** do **{event}**',
        noChanges         : 'Brak zmian',
        nullValue         : 'brak',
        versionDateFormat : 'D/M/YYYY h:mm a',
        undid             : 'Cofnięto zmiany',
        redid             : 'Ponownie edytuj zmiany',
        editedTask        : 'Edytowano właściwości zadania',
        deletedTask       : 'Usunięto zadanie',
        movedTask         : 'Przesunięto zadanie',
        movedTasks        : 'Przesunięto zadania'
    }
};

export default LocaleHelper.publishLocale(locale);
