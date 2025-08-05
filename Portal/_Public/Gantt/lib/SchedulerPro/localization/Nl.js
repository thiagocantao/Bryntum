import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Nl.js';
import '../../Scheduler/localization/Nl.js';

const locale = {

    localeName : 'Nl',
    localeDesc : 'Nederlands',
    localeCode : 'nl',

    ConstraintTypePicker : {
        none                : 'Geen',
        assoonaspossible    : 'Zo snel mogelijk',
        aslateaspossible    : 'Zo laat mogelijk',
        muststarton         : 'Niet eerder eindigen dan',
        mustfinishon        : 'Niet later eindigen dan',
        startnoearlierthan  : 'Moet beginnen op',
        startnolaterthan    : 'Moet eindigen op',
        finishnoearlierthan : 'Niet eerder beginnen dan',
        finishnolaterthan   : 'Niet later beginnen dan'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Vooruit',
        Backward      : 'Achteruit',
        inheritedFrom : 'Geërfd van',
        enforcedBy    : 'Opgelegd door'
    },

    CalendarField : {
        'Default calendar' : 'Standaardkalender'
    },

    TaskEditorBase : {
        Information   : 'Informatie',
        Save          : 'Opslaan',
        Cancel        : 'Annuleer',
        Delete        : 'Verwijder',
        calculateMask : 'Taken berekenen...',
        saveError     : 'Kan niet opslaan. Corrigeer eerst de fouten',
        repeatingInfo : 'Een herhaald item bekijken',
        editRepeating : 'Bewerk'
    },

    TaskEdit : {
        'Edit task'            : 'Bewerk taak',
        ConfirmDeletionTitle   : 'Bevestig verwijderen',
        ConfirmDeletionMessage : 'Weet u zeker dat u dit item wilt verwijderen?'
    },

    GanttTaskEditor : {
        editorWidth : '55em'
    },

    SchedulerTaskEditor : {
        editorWidth : '41em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Algemeen',
        Name         : 'Naam',
        Resources    : 'Resources',
        '% complete' : '% compleet',
        Duration     : 'Duur',
        Start        : 'Begin',
        Finish       : 'Einde',
        Effort       : 'Inspanning',
        Preamble     : 'Preamble',
        Postamble    : 'Postamble'
    },

    GeneralTab : {
        labelWidth   : '6em',
        General      : 'Algemeen',
        Name         : 'Naam',
        '% complete' : '% compleet',
        Duration     : 'Duur',
        Start        : 'Begin',
        Finish       : 'Einde',
        Effort       : 'Inspanning',
        Dates        : 'Datums'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '10em',
        Advanced                   : 'Geavanceerd',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Taaktype',
        'Effort driven'            : 'Op inspanning',
        'Manually scheduled'       : 'Handmatig',
        'Constraint type'          : 'Beperkingstype',
        'Constraint date'          : 'Beperkingsdatum',
        Inactive                   : 'Inactief',
        'Ignore resource calendar' : 'Resourcekalender negeren'
    },

    AdvancedTab : {
        labelWidth                 : '12em',
        Advanced                   : 'Geavanceerd',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Taaktype',
        'Effort driven'            : 'Op inspanning',
        'Manually scheduled'       : 'Handmatig',
        'Constraint type'          : 'Beperkingstype',
        'Constraint date'          : 'Beperkingsdatum',
        Constraint                 : 'Beperking',
        Rollup                     : 'Oprollen',
        Inactive                   : 'Inactief',
        'Ignore resource calendar' : 'Resourcekalender negeren',
        'Scheduling direction'     : 'Planningrichting'
    },

    DependencyTab : {
        Predecessors      : 'Voorafgaande taken',
        Successors        : 'Opvolgende taken',
        ID                : 'ID',
        Name              : 'Naam',
        Type              : 'Type',
        Lag               : 'Vertraging',
        cyclicDependency  : 'Cyclische afhankelijkheid',
        invalidDependency : 'Ongeldige afhankelijkheid'
    },

    NotesTab : {
        Notes : 'Notities'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Middelen',
        Resource  : 'Hulpbron',
        Units     : 'Eenheden'
    },

    RecurrenceTab : {
        title : 'Herhalen'
    },

    SchedulingModePicker : {
        Normal           : 'Normaal',
        'Fixed Duration' : 'Vaste duur',
        'Fixed Units'    : 'Vaste eenheden',
        'Fixed Effort'   : 'Vast werk'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen',
        barTipOnDate          : '<b>{resource}</b> op {startDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} van de {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen:<br>{assignments}',
        groupBarTipOnDate     : 'On {startDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen:<br>{assignments}',
        plusMore              : '+{value} meer'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> toegewezen',
        barTipOnDate          : '<b>{event}</b> op {startDate}<br><span class="{cls}">{allocated}</span> toegewezen',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen:<br>{assignments}',
        groupBarTipOnDate     : 'On {startDate}<br><span class="{cls}">{allocated} van de {available}</span> toegewezen:<br>{assignments}',
        plusMore              : '+{value} meer',
        nameColumnText        : 'Hulpbron / Boeking'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Annuleer wijziging en doe niks',
        schedulingConflict : 'Planningsconflict',
        emptyCalendar      : 'Kalender configuratie fout',
        cycle              : 'Planning lus',
        Apply              : 'Pas toe'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Selecteer een afhankelijkheid om beneden aan te passen:',
        invalidDependencyLabel : 'Er zijn een aantal niet valide afhankelijkheden die moeten worden opgelost:'
    },

    DependencyEdit : {
        Active : 'Actief'
    },

    SchedulerProBase : {
        propagating     : 'Beregning',
        storePopulation : 'Indlæser data',
        finalizing      : 'Finaliseren'
    },

    EventSegments : {
        splitEvent    : 'Item splitsen',
        renameSegment : 'Hernoemen'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Ontnest niet toegestaan',
        nestingNotAllowed   : 'Nesten niet toegestaan'
    },

    VersionGrid : {
        compare       : 'Vergelijken',
        description   : 'Beschrijving',
        occurredAt    : 'Plaatsvond op',
        rename        : 'Hernoemen',
        restore       : 'Herstellen',
        stopComparing : 'Vergelijken stoppen'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'taak',
            AssignmentModel : 'toewijzing',
            DependencyModel : 'afhankelijkheid',
            ProjectModel    : 'project',
            ResourceModel   : 'resource',
            other           : 'object'
        },
        entityNamesPlural : {
            TaskModel       : 'taken',
            AssignmentModel : 'toewijzingen',
            DependencyModel : 'afhankelijkheden',
            ProjectModel    : 'projecten',
            ResourceModel   : 'resources',
            other           : 'objecten'
        },
        transactionDescriptions : {
            update : 'Gewijzigd {n} {entities}',
            add    : 'Toegevoegd {n} {entities}',
            remove : 'Verwijderd {n} {entities}',
            move   : '{n} {entities} verplaatst',
            mixed  : 'Wijzigingen {n} {entities}'
        },
        addEntity         : 'Toegevoegd {type} {name}',
        removeEntity      : 'Verwijderd {type} {name}',
        updateEntity      : 'Gewijzigd {type} {name}',
        moveEntity        : '{type} {name} verplaatst van {from} naar {to}',
        addDependency     : 'Afhankelijkheid van {from} naar {to} toegevoegd',
        removeDependency  : 'Afhankelijkheid {from} met {to} verwijderd',
        updateDependency  : 'Gewijzigde afhankelijkheid van {from} naar {to}',
        addAssignment     : 'Resource {resource} toegewezen aan {event}',
        removeAssignment  : 'Resource {resource} verwijderd van {event}',
        updateAssignment  : 'Gewijzigde toewijzing van {resource} aan {event}',
        noChanges         : 'Geen wijzigingen',
        nullValue         : 'niets',
        versionDateFormat : 'D/M/YYYY HH:mm',
        undid             : 'Ongedaan gemaakt',
        redid             : 'Opnieuw gedaan',
        editedTask        : 'Taak eigenschappen aangepast',
        deletedTask       : 'Taak is verwijderd',
        movedTask         : 'Taak verplaatst',
        movedTasks        : 'Taken verplaatst'
    }
};

export default LocaleHelper.publishLocale(locale);
