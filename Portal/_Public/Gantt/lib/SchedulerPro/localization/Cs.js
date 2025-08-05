import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Cs.js';
import '../../Scheduler/localization/Cs.js';

const locale = {

    localeName : 'Cs',
    localeDesc : 'Česky',
    localeCode : 'cs',

    ConstraintTypePicker : {
        none                : 'Žádný',
        assoonaspossible    : 'Co nejdříve',
        aslateaspossible    : 'Až později',
        muststarton         : 'Musí začít',
        mustfinishon        : 'Musí skončit',
        startnoearlierthan  : 'Nespouštět dříve než',
        startnolaterthan    : 'Nespouštět později než',
        finishnoearlierthan : 'Nedokončovat dříve než',
        finishnolaterthan   : 'Nedokončovat později než'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Vpřed',
        Backward      : 'Zpět',
        inheritedFrom : 'Děděno od',
        enforcedBy    : 'Vynuceno od'
    },

    CalendarField : {
        'Default calendar' : 'Výchozí kalendář'
    },

    TaskEditorBase : {
        Information   : 'Informace',
        Save          : 'Uložit',
        Cancel        : 'Zrušit',
        Delete        : 'Vymazat',
        calculateMask : 'Výpočet...',
        saveError     : 'Nelze uložit. Nejprve prosím opravte chyby',
        repeatingInfo : 'Zobrazení opakující se události',
        editRepeating : 'Upravit'
    },

    TaskEdit : {
        'Edit task'            : 'Upravit úkol',
        ConfirmDeletionTitle   : 'Potvrdit výmaz',
        ConfirmDeletionMessage : 'Opravdu chcete událost vymazat?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Obecné',
        Name         : 'Název',
        Resources    : 'Zdroje',
        '% complete' : '% hotovo',
        Duration     : 'Doba trvání',
        Start        : 'Začátek',
        Finish       : 'Konec',
        Effort       : 'Úsilí',
        Preamble     : 'Preambule',
        Postamble    : 'Postambule'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Obecné',
        Name         : 'Název',
        '% complete' : '% hotovo',
        Duration     : 'Doba trvání',
        Start        : 'Začátek',
        Finish       : 'Konec',
        Effort       : 'Úsilí',
        Dates        : 'Data'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Pokročilý',
        Calendar                   : 'Kalendář',
        'Scheduling mode'          : 'Režim plánování',
        'Effort driven'            : 'Řízený úsilím',
        'Manually scheduled'       : 'Manuálně naplánováno',
        'Constraint type'          : 'Typ překážky',
        'Constraint date'          : 'Datum překážky',
        Inactive                   : 'Neaktivní',
        'Ignore resource calendar' : 'Ignorovat zdrojový kalendář'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Pokročilý',
        Calendar                   : 'Kalendář',
        'Scheduling mode'          : 'Režim plánování',
        'Effort driven'            : 'Řízený úsilím',
        'Manually scheduled'       : 'Manuálně naplánovaný',
        'Constraint type'          : 'Typ překážky',
        'Constraint date'          : 'Datum překážky',
        Constraint                 : 'Překážka',
        Rollup                     : 'Shrnout',
        Inactive                   : 'Neaktivní',
        'Ignore resource calendar' : 'Ignorovat zdrojový kalendář',
        'Scheduling direction'     : 'Směr plánování'
    },

    DependencyTab : {
        Predecessors      : 'Předchůdci',
        Successors        : 'Následovníci',
        ID                : 'ID',
        Name              : 'Název',
        Type              : 'Typ',
        Lag               : 'Prodlení ',
        cyclicDependency  : 'Cyklická závislost',
        invalidDependency : 'Neplatná závislost'
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
        title : 'Opakovat'
    },

    SchedulingModePicker : {
        Normal           : 'Normální',
        'Fixed Duration' : 'Fixní doba trvání',
        'Fixed Units'    : 'Fixní jednotky',
        'Fixed Effort'   : 'Fixní úsilí'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> přiděleno',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} z {available}</span> přiděleno',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} z {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Dne {startDate}<br><span class="{cls}">{allocated} z {available}</span> přiděleno:<br>{assignments}',
        plusMore              : '+{value} více'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> přiděleno',
        barTipOnDate          : '<b>{event}</b> dne {startDate}<br><span class="{cls}">{allocated}</span> přiděleno',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} z {available}</span> přiděleno:<br>{assignments}',
        groupBarTipOnDate     : 'Dne {startDate}<br><span class="{cls}">{allocated} z {available}</span> přiděleno:<br>{assignments}',
        plusMore              : '+{value} další',
        nameColumnText        : 'Zdroj / Událost'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Zrušit změnu a nic nedělat',
        schedulingConflict : 'Konflikt plánování',
        emptyCalendar      : 'Chyba konfigurace kalendáře',
        cycle              : 'Cyklus plánování',
        Apply              : 'Použít'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Vyberte závislost, prosím:',
        invalidDependencyLabel : 'Jsou zde neplatné závislosti, které je třeba vyřešit:'
    },

    DependencyEdit : {
        Active : 'Aktivní'
    },

    SchedulerProBase : {
        propagating     : 'Výpočet projektu',
        storePopulation : 'Načítání dat',
        finalizing      : 'Dokončování výsledků'
    },

    EventSegments : {
        splitEvent    : 'Rozdělit událost',
        renameSegment : 'Přejmenovat'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Odstranění vnoření není povoleno',
        nestingNotAllowed   : 'Vnoření není povoleno'
    },

    VersionGrid : {
        compare       : 'Porovnat',
        description   : 'Popis',
        occurredAt    : 'Stalo se v',
        rename        : 'Přejmenovat',
        restore       : 'Obnovit',
        stopComparing : 'Zastavit porovnávání'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'úkol',
            AssignmentModel : 'pověření',
            DependencyModel : 'odkaz',
            ProjectModel    : 'projekt',
            ResourceModel   : 'zdroj',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'úkoly',
            AssignmentModel : 'přiřazení',
            DependencyModel : 'odkazy',
            ProjectModel    : 'projekty',
            ResourceModel   : 'zdroje',
            other           : 'objekty'
        },
        transactionDescriptions : {
            update : 'Změna {n} {entities}',
            add    : 'Přidáno {n} {entities}',
            remove : 'Odebráno {n} {entities}',
            move   : 'Přesunuto {n} {entities}',
            mixed  : 'Změna {n} {entities}'
        },
        addEntity         : 'Přidán {type} **{name}**',
        removeEntity      : 'Odebrán {type} **{name}**',
        updateEntity      : 'Změněn {type} **{name}**',
        moveEntity        : 'Přesunut {type} **{name}** from {from} to {to}',
        addDependency     : 'Přidán odkaz z **{from}** do **{to}**',
        removeDependency  : 'Odebrán odkaz z **{from}** do **{to}**',
        updateDependency  : 'Upraven odkaz z **{from}** do **{to}**',
        addAssignment     : 'Přiřazen **{resource}** do **{event}**',
        removeAssignment  : 'Odebráno přiřazení **{resource}** z **{event}**',
        updateAssignment  : 'Upraveno přiřazení **{resource}** do **{event}**',
        noChanges         : 'Beze změn',
        nullValue         : 'žádný',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Zrušit změny',
        redid             : 'Opakovat změny',
        editedTask        : 'Upravené vlastnosti úlohy',
        deletedTask       : 'Smazaná úloha',
        movedTask         : 'Přesunutá úloha',
        movedTasks        : 'Přesunuté úlohy'
    }
};

export default LocaleHelper.publishLocale(locale);
