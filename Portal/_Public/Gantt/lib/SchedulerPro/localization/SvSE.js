import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/SvSE.js';
import '../../Scheduler/localization/SvSE.js';

const locale = {

    localeName : 'SvSE',
    localeDesc : 'Svenska',
    localeCode : 'sv-SE',

    ConstraintTypePicker : {
        none                : 'Ingen',
        assoonaspossible    : 'Så snart som möjligt',
        aslateaspossible    : 'Så sent som möjligt',
        muststarton         : 'Måste starta',
        mustfinishon        : 'Måste avslutas',
        startnoearlierthan  : 'Starta tidigast',
        startnolaterthan    : 'Starta senast',
        finishnoearlierthan : 'Avsluta tidigast',
        finishnolaterthan   : 'Avsluta senast'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Framåt',
        Backward      : 'Bakåt',
        inheritedFrom : 'Ärvd från',
        enforcedBy    : 'Tvingad av'
    },

    CalendarField : {
        'Default calendar' : 'Standardkalender'
    },

    TaskEditorBase : {
        Information   : 'Information',
        Save          : 'Spara',
        Cancel        : 'Avbryt',
        Delete        : 'Ta bort',
        calculateMask : 'Beräknar...',
        saveError     : 'Kan inte spara, vänligen korrigera fel först',
        repeatingInfo : 'Visar upprepad bokning',
        editRepeating : 'Redigera'
    },

    TaskEdit : {
        'Edit task'            : 'Redigera uppgift',
        ConfirmDeletionTitle   : 'Bekräfta borttagning',
        ConfirmDeletionMessage : 'Är du säker på att du vill ta bort händelsen?'
    },

    GanttTaskEditor : {
        editorWidth : '46em'
    },

    SchedulerTaskEditor : {
        editorWidth : '36em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '8em',
        General      : 'Allmänt',
        Name         : 'Namn',
        Resources    : 'Resurser',
        '% complete' : '% Färdig',
        Duration     : 'Varaktighet',
        Start        : 'Start',
        Finish       : 'Slut',
        Effort       : 'Arbetsinsats',
        Preamble     : 'Inledning',
        Postamble    : 'Avslutning'
    },

    GeneralTab : {
        labelWidth   : '8em',
        General      : 'Allmänt',
        Name         : 'Namn',
        '% complete' : '% Färdig',
        Duration     : 'Varaktighet',
        Start        : 'Start',
        Finish       : 'Slut',
        Effort       : 'Arbetsinsats',
        Dates        : 'Datum'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '11em',
        Advanced                   : 'Avancerat',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Aktivitetstyp',
        'Effort driven'            : 'Insatsdriven',
        'Manually scheduled'       : 'Manuellt planerad',
        'Constraint type'          : 'Villkorstyp',
        'Constraint date'          : 'Måldatum',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Ignorera resurskalender'
    },

    AdvancedTab : {
        labelWidth                 : '11em',
        Advanced                   : 'Avancerat',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Aktivitetstyp',
        'Effort driven'            : 'Insatsdriven',
        'Manually scheduled'       : 'Manuellt planerad',
        'Constraint type'          : 'Villkorstyp',
        'Constraint date'          : 'Måldatum',
        Constraint                 : 'Villkor',
        Rollup                     : 'Upplyft',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Ignorera resurskalender',
        'Scheduling direction'     : 'Planeringsriktning'
    },

    DependencyTab : {
        Predecessors      : 'Föregångare',
        Successors        : 'Efterföljare',
        ID                : 'ID',
        Name              : 'Namn',
        Type              : 'Typ',
        Lag               : 'Fördröjning',
        cyclicDependency  : 'Cykliskt beroende',
        invalidDependency : 'Ogiltigt beroende'
    },

    NotesTab : {
        Notes : 'Anteckning'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Resurser',
        Resource  : 'Resurs',
        Units     : 'Enheter'
    },

    RecurrenceTab : {
        title : 'Upprepa'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Fast varaktighet',
        'Fixed Units'    : 'Fasta enheter',
        'Fixed Effort'   : 'Fast arbete'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade',
        barTipOnDate          : '<b>{resource}</b> på {startDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} av {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade:<br>{assignments}',
        groupBarTipOnDate     : 'On {startDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade:<br>{assignments}',
        plusMore              : '+{value} more'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> allokerade',
        barTipOnDate          : '<b>{event}</b> på {startDate}<br><span class="{cls}">{allocated}</span> allokerade',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade:<br>{assignments}',
        groupBarTipOnDate     : 'På {startDate}<br><span class="{cls}">{allocated} av {available}</span> allokerade:<br>{assignments}',
        plusMore              : '+{value} more',
        nameColumnText        : 'Resurs / Bokning'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Avbryt ändringen',
        schedulingConflict : 'Schemaläggningskonflikt',
        emptyCalendar      : 'Felaktig kalendarkonfiguration',
        cycle              : 'Cyklisk sekvens',
        Apply              : 'Utför'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Välj ett beroende att ändra enligt nedanstående:',
        invalidDependencyLabel : 'Det finns ogiltiga beroenden som måste korrigeras:'
    },

    DependencyEdit : {
        Active : 'Aktiv'
    },

    SchedulerProBase : {
        propagating     : 'Beräknar',
        storePopulation : 'Laddar data',
        finalizing      : 'Slutför'
    },

    EventSegments : {
        splitEvent    : 'Dela bokning',
        renameSegment : 'Byt namn'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Av-nästling ej tillåten',
        nestingNotAllowed   : 'Nästling ej tillåten'
    },

    VersionGrid : {
        compare       : 'Jämför',
        description   : 'Beskrivning',
        occurredAt    : 'Inträffade vid',
        rename        : 'Döp om',
        restore       : 'Återställa',
        stopComparing : 'Sluta jämföra'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'aktivitet',
            AssignmentModel : 'tilldelning',
            DependencyModel : 'beroende',
            ProjectModel    : 'projekt',
            ResourceModel   : 'resurs',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'aktiviteter',
            AssignmentModel : 'tilldelningar',
            DependencyModel : 'beroenden',
            ProjectModel    : 'projekt',
            ResourceModel   : 'resurser',
            other           : 'objekt'
        },
        transactionDescriptions : {
            update : 'Ändrade {n} {entities}',
            add    : 'La till {n} {entities}',
            remove : 'Tog bort {n} {entities}',
            move   : 'Flyttade {n} {entities}',
            mixed  : 'Uppdaterade {n} {entities}'
        },
        addEntity         : 'La till {type} {name}',
        removeEntity      : 'Tog bort {type} {name}',
        updateEntity      : 'Ändrade {type} {name}',
        moveEntity        : 'Flyttade {type} {name} från {from} till {to}',
        addDependency     : 'La till beroende från {from} till {to}',
        removeDependency  : 'Tog bort bereonde från {from} till {to}',
        updateDependency  : 'Ändrade beroende från {from} till {to}',
        addAssignment     : 'Tilldelade {resource} till {event}',
        removeAssignment  : 'Tog bort tilldelning av {resource} från {event}',
        updateAssignment  : 'Ändrade tilldelning av {resource} till {event}',
        noChanges         : 'Inga ändringar',
        nullValue         : 'ingen',
        versionDateFormat : 'YYYY-MM-DD HH:mm',
        undid             : 'Ångrade ändringar',
        redid             : 'Gjorde om ändringar',
        editedTask        : 'Redigerade en aktivitet',
        deletedTask       : 'Tog bort en aktivitet',
        movedTask         : 'Flyttade en aktivitet',
        movedTasks        : 'Flyttade aktiviteter'
    }
};

export default LocaleHelper.publishLocale(locale);
