import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Sk.js';
import '../../Scheduler/localization/Sk.js';

const locale = {

    localeName : 'Sk',
    localeDesc : 'Slovenský',
    localeCode : 'sk',

    ConstraintTypePicker : {
        none                : 'Žiadny',
        assoonaspossible    : 'Čo najskôr',
        aslateaspossible    : 'Čo najneskôr',
        muststarton         : 'Musí začať v',
        mustfinishon        : 'Musí skončiť v',
        startnoearlierthan  : 'Nezačať skôr ako',
        startnolaterthan    : 'Nezačať neskôr ako',
        finishnoearlierthan : 'Neskončiť skôr ako',
        finishnolaterthan   : 'neskončiť neskôr ako'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Vpred',
        Backward      : 'Späť',
        inheritedFrom : 'Zdedené od',
        enforcedBy    : 'Vynútené od'
    },

    CalendarField : {
        'Default calendar' : 'Predvolený kalendár'
    },

    TaskEditorBase : {
        Information   : 'Informácie',
        Save          : 'Uložiť',
        Cancel        : 'Zrušiť',
        Delete        : 'Vymazať',
        calculateMask : 'Vypočítavanie...',
        saveError     : 'Nedá sa uložiť, najprv opravte chyby',
        repeatingInfo : 'Zobrazenie opakujúcej sa udalosti',
        editRepeating : 'Upraviť'
    },

    TaskEdit : {
        'Edit task'            : 'Upraviť úlohu',
        ConfirmDeletionTitle   : 'Potvrdiť vymazanie',
        ConfirmDeletionMessage : 'Naozaj chcete vymazať udalosť?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Všoebecné',
        Name         : 'Názov',
        Resources    : 'Zdroje',
        '% complete' : '% dokončené',
        Duration     : 'Trvanie',
        Start        : 'Start',
        Finish       : 'Finish',
        Effort       : 'Effort',
        Preamble     : 'Preambula',
        Postamble    : 'Poštová zásielka'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Všeobecné',
        Name         : 'Názov',
        '% complete' : '% dokončené',
        Duration     : 'Trvanie',
        Start        : 'Začať',
        Finish       : 'Ukončiť',
        Effort       : 'Effort',
        Dates        : 'Dátumy'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Pokročilé',
        Calendar                   : 'Kalendár',
        'Scheduling mode'          : 'Plánovací režim',
        'Effort driven'            : 'Effort poháňaný',
        'Manually scheduled'       : 'Manuálne naplánované',
        'Constraint type'          : 'Typ obmedzenia',
        'Constraint date'          : 'Dátum obmedzenia',
        Inactive                   : 'Neaktívny',
        'Ignore resource calendar' : 'Ignorovať kalendár zdrojov'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Pokročilý',
        Calendar                   : 'Kalendár',
        'Scheduling mode'          : 'Plánovací režim',
        'Effort driven'            : 'Effort poháňaný',
        'Manually scheduled'       : 'Manuálne naplánovaný',
        'Constraint type'          : 'Typ obmedzenia',
        'Constraint date'          : 'Dátum obmedzenia',
        Constraint                 : 'Obmezenie',
        Rollup                     : 'Akumulácia',
        Inactive                   : 'Neaktívny',
        'Ignore resource calendar' : 'Ignorovať kalendár zdrojov',
        'Scheduling direction'     : 'Smer plánovania'
    },

    DependencyTab : {
        Predecessors      : 'Predchodcovia',
        Successors        : 'Následníci',
        ID                : 'ID',
        Name              : 'Názov',
        Type              : 'Typ',
        Lag               : 'Lag',
        cyclicDependency  : 'Cyklická súvislosť',
        invalidDependency : 'Neplatná súvislosť'
    },

    NotesTab : {
        Notes : 'Poznámky'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Zdroje',
        Resource  : 'Zdroje',
        Units     : 'Jednotky'
    },

    RecurrenceTab : {
        title : 'Zopakovať'
    },

    SchedulingModePicker : {
        Normal           : 'Normálny',
        'Fixed Duration' : 'Pevné trvanie',
        'Fixed Units'    : 'Pevné jednotky',
        'Fixed Effort'   : 'Pevná úsilie'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> pridelených',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} z {available}</span>pridelených',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} z {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'V {startDate}<br><span class="{cls}">{allocated} z {available}</span>pridelených:<br>{assignments}',
        plusMore              : '+{value} viac '
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span>pridelené',
        barTipOnDate          : '<b>{event}</b>V {startDate}<br><span class="{cls}">{allocated}</span>pridelené',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span>pridelených:<br>{assignments}',
        groupBarTipOnDate     : 'V {startDate}<br><span class="{cls}">{allocated} z {available}</span>pridelených:<br>{assignments}',
        plusMore              : '+{value} viac',
        nameColumnText        : 'Zdroj / Udalosť'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Zrušiť zmenu a nerobiť nč',
        schedulingConflict : 'Plánovaný konflikt',
        emptyCalendar      : 'Chyba konfigurácie kalendára',
        cycle              : 'Plánovací cyklus',
        Apply              : 'Použiť'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Zvoľte súvislosť:',
        invalidDependencyLabel : 'Sú zahrnuté neplatné závislosti, které je potrebné osloviť:'
    },

    DependencyEdit : {
        Active : 'Aktívny'
    },

    SchedulerProBase : {
        propagating     : 'Vypočítanie projektu',
        storePopulation : 'Nahrávanie údajov',
        finalizing      : 'Finalizovanie výsledkov'
    },

    EventSegments : {
        splitEvent    : 'Rozdeliť udalosť',
        renameSegment : 'Premenovať'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Vykladanie nepovolené',
        nestingNotAllowed   : 'Nesting nepovolené'
    },

    VersionGrid : {
        compare       : 'Porovnať',
        description   : 'Popis',
        occurredAt    : 'Stalo sa v',
        rename        : 'Premenovať',
        restore       : 'Obnoviť',
        stopComparing : 'Zastaviť porovnávanie'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'úloha',
            AssignmentModel : 'zadanie',
            DependencyModel : 'odkaz',
            ProjectModel    : 'projekt',
            ResourceModel   : 'zdroj',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'úlohy',
            AssignmentModel : 'zadania',
            DependencyModel : 'odkazy',
            ProjectModel    : 'projekty',
            ResourceModel   : 'zdroje',
            other           : 'objekty'
        },
        transactionDescriptions : {
            update : 'Zmenené {n} {entities}',
            add    : 'Pridané {n} {entities}',
            remove : 'Odstránené {n} {entities}',
            move   : 'Presunuté {n} {entities}',
            mixed  : 'Zmenené {n} {entities}'
        },
        addEntity         : 'Pridané {type} **{name}**',
        removeEntity      : 'Odstránené {type} **{name}**',
        updateEntity      : 'Zmenené {type} **{name}**',
        moveEntity        : 'Presunutú {type} **{name}** z {from} do {to}',
        addDependency     : 'Pridaný odkaz z **{from}** do **{to}**',
        removeDependency  : 'Odstránený odkaz z **{from}** do **{to}**',
        updateDependency  : 'Upravený odkaz z **{from}** do **{to}**',
        addAssignment     : 'Pridelený **{resource}** do **{event}**',
        removeAssignment  : 'Odstránená úloha **{resource}** z **{event}**',
        updateAssignment  : 'Upravená úloha z **{resource}** do **{event}**',
        noChanges         : 'Bez zmeny',
        nullValue         : 'žiadne',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Zrušené zmeny',
        redid             : 'Prerobené zmeny',
        editedTask        : 'Upravené vlastnosti úlohy',
        deletedTask       : 'Odstránené úlohy',
        movedTask         : 'Presunutá úloha',
        movedTasks        : 'Presunuté úlohy'
    }
};

export default LocaleHelper.publishLocale(locale);
