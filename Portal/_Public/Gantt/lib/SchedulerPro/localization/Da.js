import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Da.js';
import '../../Scheduler/localization/Da.js';

const locale = {

    localeName : 'Da',
    localeDesc : 'Dansk',
    localeCode : 'da',

    ConstraintTypePicker : {
        none                : 'Ingen',
        assoonaspossible    : 'Så hurtigt som muligt',
        aslateaspossible    : 'Så sent som muligt',
        muststarton         : 'Skal starte på',
        mustfinishon        : 'Skal slutte på',
        startnoearlierthan  : 'Start tidligst kl',
        startnolaterthan    : 'Start senest kl',
        finishnoearlierthan : 'Afslut tidligst',
        finishnolaterthan   : 'Afslut senest kl'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Fremad',
        Backward      : 'Bagud',
        inheritedFrom : 'Arvet fra',
        enforcedBy    : 'Pålagt af'
    },

    CalendarField : {
        'Default calendar' : 'Standard kalender'
    },

    TaskEditorBase : {
        Information   : 'Information',
        Save          : 'Gemme',
        Cancel        : 'Afbestille',
        Delete        : 'Slet',
        calculateMask : 'Beregner...',
        saveError     : 'Kan ikke gemme. Ret venligst fejl først',
        repeatingInfo : 'Viser en gentagende begivenhed',
        editRepeating : 'Redigere'
    },

    TaskEdit : {
        'Edit task'            : 'Rediger opgave',
        ConfirmDeletionTitle   : 'Bekræft sletning',
        ConfirmDeletionMessage : 'Er du sikker på, at du vil slette begivenheden?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Generel',
        Name         : 'Navn',
        Resources    : 'Ressourcer',
        '% complete' : '% komplet',
        Duration     : 'Varighed',
        Start        : 'Start',
        Finish       : 'slut',
        Effort       : 'Indsats',
        Preamble     : 'Præambel',
        Postamble    : 'Postamble'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Generel',
        Name         : 'Navn',
        '% complete' : '% komplet',
        Duration     : 'Varighed',
        Start        : 'Start',
        Finish       : 'slut',
        Effort       : 'Indsats',
        Dates        : 'Dato'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Fremskreden',
        Calendar                   : 'kalender',
        'Scheduling mode'          : 'Planlægningstilstand',
        'Effort driven'            : 'Indsatsdrevet',
        'Manually scheduled'       : 'Manuelt planlagt',
        'Constraint type'          : 'Begrænsning type',
        'Constraint date'          : 'Begrænsning dato',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Ignorer ressourcekalender'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Fremskreden',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Planlægningstilstand',
        'Effort driven'            : 'Indsatsdrevet',
        'Manually scheduled'       : 'Manuelt planlagt',
        'Constraint type'          : 'Begrænsningstype',
        'Constraint date'          : 'Begrænsningsdato',
        Constraint                 : 'Begrænsning',
        Rollup                     : 'Rul op',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Ignorer ressourcekalender',
        'Scheduling direction'     : 'Planlægningsretning'
    },

    DependencyTab : {
        Predecessors      : 'Forgængere',
        Successors        : 'Efterfølgere',
        ID                : 'identitet',
        Name              : 'Navn',
        Type              : 'Type',
        Lag               : 'Lag',
        cyclicDependency  : 'Cyklisk afhængighed',
        invalidDependency : 'Ugyldig afhængighed'
    },

    NotesTab : {
        Notes : 'Noter'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ressourcer',
        Resource  : 'Ressourcer',
        Units     : 'Enheder'
    },

    RecurrenceTab : {
        title : 'Gentage'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Fast varighed',
        'Fixed Units'    : 'Faste enheder',
        'Fixed Effort'   : 'Fast indsats'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} af {available}</span> tildelt',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} af {available}</span> tildelt',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} af {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated}af {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'på {startDate}<br><span class="{cls}">{allocated} af {available}</span> tildelt:<br>{assignments}',
        plusMore              : '+{value} mere'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> tildelt',
        barTipOnDate          : '<b>{event}</b> på {startDate}<br><span class="{cls}">{allocated}</span> tildelt',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} af {available}</span> tildelt:<br>{assignments}',
        groupBarTipOnDate     : 'på {startDate}<br><span class="{cls}">{allocated} af {available}</span> tildelt:<br>{assignments}',
        plusMore              : '+{value} mere',
        nameColumnText        : 'Ressource / begivenhed'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Annuller ændringen og gør ingenting',
        schedulingConflict : 'Planlægningskonflikt',
        emptyCalendar      : 'Kalender konfigurationsfejl',
        cycle              : 'Planlægningscyklus',
        Apply              : 'ansøge'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Vælg venligst en afhængighed:',
        invalidDependencyLabel : 'Der er ugyldige afhængigheder involveret, som skal løses:'
    },

    DependencyEdit : {
        Active : 'Aktiv'
    },

    SchedulerProBase : {
        propagating     : 'Beregningsprojekt',
        storePopulation : 'Indlæser data',
        finalizing      : 'Afsluttende resultater'
    },

    EventSegments : {
        splitEvent    : 'Opdelt begivenhed',
        renameSegment : 'Omdøb'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Af-nestning ikke tilladt',
        nestingNotAllowed   : 'Nesting ikke tilladt'
    },

    VersionGrid : {
        compare       : 'Sammenlign',
        description   : 'Beskrivelse',
        occurredAt    : 'Skete ved',
        rename        : 'Omdøb',
        restore       : 'Gendan',
        stopComparing : 'Stop sammenligning'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'opgave',
            AssignmentModel : 'opgave',
            DependencyModel : 'link',
            ProjectModel    : 'projekt',
            ResourceModel   : 'ressource',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'opgave',
            AssignmentModel : 'opgave',
            DependencyModel : 'link',
            ProjectModel    : 'projekt',
            ResourceModel   : 'ressource',
            other           : 'objekt'
        },
        transactionDescriptions : {
            update : 'Ændret {n} {entities}',
            add    : 'Tilføjet {n} {entities}',
            remove : 'Fjernede {n} {entities}',
            move   : 'Flyttet {n} {entities}',
            mixed  : 'Ændret {n} {entities}'
        },
        addEntity         : 'Tilføjede {type} **{navn}**',
        removeEntity      : 'Fjernet {type} **{navn}**',
        updateEntity      : 'Ændret {type} **{navn}**',
        moveEntity        : 'Flyttede {type} **{navn}** fra {from} til {to}',
        addDependency     : 'Tilføjet link fra **{from}** til **{to}**',
        removeDependency  : 'Fjernet link fra **{from}** til **{to}**',
        updateDependency  : 'Redigeret link fra **{from}** til **{to}**',
        addAssignment     : 'Tildelt **{ressource}** til **{event}**',
        removeAssignment  : 'Fjernet tildeling af **{ressource}** fra **{event}**',
        updateAssignment  : 'Redigeret tildeling af **{ressource}** til **{event}**',
        noChanges         : 'Ingen ændringer',
        nullValue         : 'ingen',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Undrede ændringer',
        redid             : 'Generede ændringer',
        editedTask        : 'Redigerede opgaveegenskaber',
        deletedTask       : 'Slettede en opgave',
        movedTask         : 'Flyttet en opgave',
        movedTasks        : 'Flyttede opgaver'
    }
};

export default LocaleHelper.publishLocale(locale);
