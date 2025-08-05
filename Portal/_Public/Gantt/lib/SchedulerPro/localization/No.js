import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/No.js';
import '../../Scheduler/localization/No.js';

const locale = {

    localeName : 'No',
    localeDesc : 'Norsk',
    localeCode : 'no',

    ConstraintTypePicker : {
        none                : 'Ingen',
        assoonaspossible    : 'Så snart som mulig',
        aslateaspossible    : 'Så sent som mulig',
        muststarton         : 'Må starte på',
        mustfinishon        : 'Må slutte på',
        startnoearlierthan  : 'Start ikke tidligere enn',
        startnolaterthan    : 'Start ikke senere enn',
        finishnoearlierthan : 'Slutt ikke tidligere enn',
        finishnolaterthan   : 'Slutt ikke senere enn'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Fremover',
        Backward      : 'Bakover',
        inheritedFrom : 'Arvet fra',
        enforcedBy    : 'Påtvunget av'
    },

    CalendarField : {
        'Default calendar' : 'Standard kalender'
    },

    TaskEditorBase : {
        Information   : 'Informasjon',
        Save          : 'Lagre',
        Cancel        : 'Avbryt',
        Delete        : 'Slett',
        calculateMask : 'Kalkulerer...',
        saveError     : 'Kan ikke lagre, vennligst korriger feil først',
        repeatingInfo : 'Viser en gjentatt hendelse',
        editRepeating : 'Redigere'
    },

    TaskEdit : {
        'Edit task'            : 'Rediger oppgave',
        ConfirmDeletionTitle   : 'Bekrefte sletting',
        ConfirmDeletionMessage : 'Er du sikker på at du vil slette denne hendelsen?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Generelt',
        Name         : 'Navn',
        Resources    : 'Ressurser',
        '% complete' : '% ferdig',
        Duration     : 'Varighet',
        Start        : 'Start',
        Finish       : 'Slutt',
        Effort       : 'Innsats',
        Preamble     : 'Innledning',
        Postamble    : 'Konklusjon'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Generelt',
        Name         : 'Navn',
        '% complete' : '% ferdig',
        Duration     : 'Varighet',
        Start        : 'Start',
        Finish       : 'Slutt',
        Effort       : 'Innsats',
        Dates        : 'Datoer'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avansert',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Planleggingsmodus',
        'Effort driven'            : 'Innsatsdrevet',
        'Manually scheduled'       : 'Planlagt manuelt',
        'Constraint type'          : 'Begrensningstype',
        'Constraint date'          : 'Begrensningsdato',
        Inactive                   : 'Ikke aktive',
        'Ignore resource calendar' : 'Ignorer ressurskalender'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Avansert',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Planleggingsmodus',
        'Effort driven'            : 'Innsatsdrevet',
        'Manually scheduled'       : 'Planlagt manuelt',
        'Constraint type'          : 'Begrensningstype',
        'Constraint date'          : 'Begrensningsdato',
        Constraint                 : 'Begrensning',
        Rollup                     : 'Rapporter til oppsummering',
        Inactive                   : 'Ikke aktiv',
        'Ignore resource calendar' : 'Ignorer ressurskalender',
        'Scheduling direction'     : 'Planleggingsretning'
    },

    DependencyTab : {
        Predecessors      : 'Tidligere',
        Successors        : 'Senere',
        ID                : 'ID',
        Name              : 'Navn',
        Type              : 'Type',
        Lag               : 'Forsinkelse',
        cyclicDependency  : 'Syklisk avhengighet',
        invalidDependency : 'Ugyldig avhengighet'
    },

    NotesTab : {
        Notes : 'Merkander'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ressurser',
        Resource  : 'Ressurs',
        Units     : 'Enheter'
    },

    RecurrenceTab : {
        title : 'Gjenta'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Fast varighet',
        'Fixed Units'    : 'Faste enheter',
        'Fixed Effort'   : 'Fast innsats'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> tildelt',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} av {available}</span> tildelt',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} av {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'På {startDate}<br><span class="{cls}">{allocated} av {available}</span> tildelt:<br>{assignments}',
        plusMore              : '+{value} mer'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> tildelt',
        barTipOnDate          : '<b>{event}</b> av {startDate}<br><span class="{cls}">{allocated}</span> tildelt',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} av {available}</span> tildelt:<br>{assignments}',
        groupBarTipOnDate     : 'På {startDate}<br><span class="{cls}">{allocated} av {available}</span> tildelt:<br>{assignments}',
        plusMore              : '+{value} mer',
        nameColumnText        : 'Ressurs / Hendelse'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Avbryt endringen og gjør ingenting',
        schedulingConflict : 'Planleggingskonflikt',
        emptyCalendar      : 'Kalenderkonfigurasjonsfeil',
        cycle              : 'Planleggingssyklus',
        Apply              : 'Bruk'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Vennligst velg avhengighet:',
        invalidDependencyLabel : 'Det er ugyldige avhengigheter involvert som må adresseres:'
    },

    DependencyEdit : {
        Active : 'Aktiv'
    },

    SchedulerProBase : {
        propagating     : 'Kalkulerer prosjekt',
        storePopulation : 'Laster data',
        finalizing      : 'Fullfører resultater'
    },

    EventSegments : {
        splitEvent    : 'Delt arrangement',
        renameSegment : 'Gi nytt navn'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Gjennesting ikke tillatt',
        nestingNotAllowed   : 'Nesting ikke tillatt'
    },

    VersionGrid : {
        compare       : 'Sammenlign',
        description   : 'Beskrivelse',
        occurredAt    : 'Skjedde på',
        rename        : 'Gi nytt navn',
        restore       : 'Gjenopprette',
        stopComparing : 'Stopp sammenligning'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'oppgave',
            AssignmentModel : 'oppdrag',
            DependencyModel : 'lenke',
            ProjectModel    : 'prosjekt',
            ResourceModel   : 'ressurss',
            other           : 'gjenstand'
        },
        entityNamesPlural : {
            TaskModel       : 'oppgaver',
            AssignmentModel : 'oppdrag',
            DependencyModel : 'lenker',
            ProjectModel    : 'prosjekter',
            ResourceModel   : 'ressursser',
            other           : 'gjenstander'
        },
        transactionDescriptions : {
            update : 'Endret {n} {entities}',
            add    : 'Lagt til {n} {entities}',
            remove : 'Fjernet {n} {entities}',
            move   : 'Flyttet {n} {entities}',
            mixed  : 'Endret {n} {entities}'
        },
        addEntity         : 'Lagt til {type} **{name}**',
        removeEntity      : 'Fjernet {type} **{name}**',
        updateEntity      : 'Endret {type} **{name}**',
        moveEntity        : 'Flyttet {type} **{name}** from {from} to {to}',
        addDependency     : 'Lagt til lenke fra **{from}** to **{to}**',
        removeDependency  : 'Fjernet lenke fra **{from}** to **{to}**',
        updateDependency  : 'Redigert lenke fra **{from}** to **{to}**',
        addAssignment     : 'Tildelt **{resource}** to **{event}**',
        removeAssignment  : 'Fjernet oppdrag av **{resource}** from **{event}**',
        updateAssignment  : 'Redigert oppgave av **{resource}** to **{event}**',
        noChanges         : 'Ingen endringer',
        nullValue         : 'ingen',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Angret endringer',
        redid             : 'Gjentok endringer',
        editedTask        : 'Redigerte oppgaveegenskaper',
        deletedTask       : 'Slettet en oppgave',
        movedTask         : 'Flyttet en oppgave',
        movedTasks        : 'Flyttet oppgaver'
    }
};

export default LocaleHelper.publishLocale(locale);
