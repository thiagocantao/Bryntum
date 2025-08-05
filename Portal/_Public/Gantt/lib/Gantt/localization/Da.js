import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../SchedulerPro/localization/Da.js';

const locale = {

    localeName : 'Da',
    localeDesc : 'Dansk',
    localeCode : 'da',

    Object : {
        Save : 'Gemme'
    },

    IgnoreResourceCalendarColumn : {
        'Ignore resource calendar' : 'Ignorer ressourcekalender'
    },

    InactiveColumn : {
        Inactive : 'Inaktiv'
    },

    AddNewColumn : {
        'New Column' : 'Ny kolonne'
    },

    BaselineStartDateColumn : {
        baselineStart : 'Oprindelig startdato'
    },

    BaselineEndDateColumn : {
        baselineEnd : 'Oprindelig slutdato'
    },

    BaselineDurationColumn : {
        baselineDuration : 'Oprindelig varighed'
    },

    BaselineStartVarianceColumn : {
        startVariance : 'Startdatoafvigelse'
    },

    BaselineEndVarianceColumn : {
        endVariance : 'Slutdatoafvigelse'
    },

    BaselineDurationVarianceColumn : {
        durationVariance : 'Varighedsafvigelse'
    },

    CalendarColumn : {
        Calendar : 'Kalender'
    },

    EarlyStartDateColumn : {
        'Early Start' : 'Tidlig start'
    },

    EarlyEndDateColumn : {
        'Early End' : 'Tidlig afslutning'
    },

    LateStartDateColumn : {
        'Late Start' : 'Sen start'
    },

    LateEndDateColumn : {
        'Late End' : 'Sen afslutning'
    },

    TotalSlackColumn : {
        'Total Slack' : 'Total tidsglidning'
    },

    ConstraintDateColumn : {
        'Constraint Date' : 'Begrænsningsdato'
    },

    ConstraintTypeColumn : {
        'Constraint Type' : 'Begrænsningstype'
    },

    DeadlineDateColumn : {
        Deadline : 'fristen'
    },

    DependencyColumn : {
        'Invalid dependency' : 'Ugyldig afhængighed'
    },

    DurationColumn : {
        Duration : 'Varighed'
    },

    EffortColumn : {
        Effort : ' Indsats'
    },

    EndDateColumn : {
        Finish : 'Afslut'
    },

    EventModeColumn : {
        'Event mode' : 'Hændelsestilstand',
        Manual       : 'Brugervejledning',
        Auto         : 'Automatisk'
    },

    ManuallyScheduledColumn : {
        'Manually scheduled' : 'Manuelt planlagt'
    },

    MilestoneColumn : {
        Milestone : 'Milepæl'
    },

    NameColumn : {
        Name : 'Navn'
    },

    NoteColumn : {
        Note : 'Bemærk'
    },

    PercentDoneColumn : {
        '% Done' : '% Færdig'
    },

    PredecessorColumn : {
        Predecessors : 'Forgængere'
    },

    ResourceAssignmentColumn : {
        'Assigned Resources' : 'Tildelte ressourcer',
        'more resources'     : 'flere ressourcer'
    },

    RollupColumn : {
        Rollup : ' Rul op'
    },

    SchedulingModeColumn : {
        'Scheduling Mode' : 'Planlægningstilstand'
    },

    SchedulingDirectionColumn : {
        schedulingDirection : 'Planlægningsretning',
        inheritedFrom       : 'Arvet fra',
        enforcedBy          : 'Pålagt af'
    },

    SequenceColumn : {
        Sequence : 'Sekvens'
    },

    ShowInTimelineColumn : {
        'Show in timeline' : 'Vis i tidslinjen'
    },

    StartDateColumn : {
        Start : 'Start'
    },

    SuccessorColumn : {
        Successors : 'Efterfølgere'
    },

    TaskCopyPaste : {
        copyTask  : 'Kopi',
        cutTask   : 'klip',
        pasteTask : 'sæt ind'
    },

    WBSColumn : {
        WBS      : ' WBS',
        renumber : 'Omnummerere'
    },

    DependencyField : {
        invalidDependencyFormat : 'Ugyldigt afhængighedsformat'
    },

    ProjectLines : {
        'Project Start' : 'Projektstart',
        'Project End'   : 'Projektets afslutning'
    },

    TaskTooltip : {
        Start    : 'Start',
        End      : 'Slut',
        Duration : 'Varighed',
        Complete : 'Komplet'
    },

    AssignmentGrid : {
        Name     : 'Ressourcenavn',
        Units    : 'Enheder',
        unitsTpl : ({ value }) => value ? value + '%' : ''
    },

    Gantt : {
        Edit                   : 'Redigere',
        Indent                 : 'Indryk',
        Outdent                : 'Udryk',
        'Convert to milestone' : 'Konverter til milepæl',
        Add                    : 'Tilføje...',
        'New task'             : 'Ny opgave',
        'New milestone'        : 'Ny milepæl',
        'Task above'           : 'Opgave ovenfor',
        'Task below'           : 'Opgave nedenfor',
        'Delete task'          : 'Slet',
        Milestone              : 'Milepæl',
        'Sub-task'             : 'Underopgave',
        Successor              : 'Efterfølger',
        Predecessor            : 'Forgænger',
        changeRejected         : 'Ændring afvist',
        linkTasks              : 'Tilføj afhængigheder',
        unlinkTasks            : 'Fjern afhængigheder',
        color                  : 'Farve'
    },

    EventSegments : {
        splitTask : 'Opdelt opgave'
    },

    Indicators : {
        earlyDates   : 'Tidlig start/afslutning',
        lateDates    : 'Sen start/afslutning',
        Start        : 'Start',
        End          : 'Slut',
        deadlineDate : 'fristen'
    },

    Versions : {
        indented     : 'indrykket',
        outdented    : 'Overdenen',
        cut          : 'Skære',
        pasted       : 'Indsat',
        deletedTasks : 'Slettede opgaver'
    }
};

export default LocaleHelper.publishLocale(locale);
