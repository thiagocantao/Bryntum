import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Et.js';
import '../../Scheduler/localization/Et.js';

const locale = {

    localeName : 'Et',
    localeDesc : 'Eesti keel',
    localeCode : 'et',

    ConstraintTypePicker : {
        none                : 'Puudub',
        assoonaspossible    : 'Nii kiiresti kui võimalik',
        aslateaspossible    : 'Nii hilja kui võimalik',
        muststarton         : 'Peab algama',
        mustfinishon        : 'Peab lõppema',
        startnoearlierthan  : 'Ei tohi alata varem kui',
        startnolaterthan    : 'Ei tohi alata hiljem kui',
        finishnoearlierthan : 'Ei tohi lõppeda varem kui',
        finishnolaterthan   : 'Ei tohi lõppeda hiljem kui'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Edasi',
        Backward      : 'Tagasi',
        inheritedFrom : 'Pärineb',
        enforcedBy    : 'Jõustatud'
    },

    CalendarField : {
        'Default calendar' : 'Vaikimisi kalender'
    },

    TaskEditorBase : {
        Information   : 'Informatsioon',
        Save          : 'Salvesta',
        Cancel        : 'Tühista',
        Delete        : 'Kustuta',
        calculateMask : 'Arvutamine...',
        saveError     : 'Ei saa salvestada, palun parandage kõigepealt vead',
        repeatingInfo : 'Korduva sündmuse vaatamine',
        editRepeating : 'Redigeeri'
    },

    TaskEdit : {
        'Edit task'            : 'Redigeeri ülesannet',
        ConfirmDeletionTitle   : 'Kinnita kustutamine',
        ConfirmDeletionMessage : 'Kas olete kindel, et soovite sündmuse kustutada?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Üldine',
        Name         : 'Nimi',
        Resources    : 'Ressursid',
        '% complete' : '% tehtud',
        Duration     : 'Kestus',
        Start        : 'Algus',
        Finish       : 'Lõpp',
        Effort       : 'Panus',
        Preamble     : 'Preambula',
        Postamble    : 'Postambula'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Üldine',
        Name         : 'Nimi',
        '% complete' : '% tehtud',
        Duration     : 'Kestus',
        Start        : 'Algus',
        Finish       : 'Lõpp',
        Effort       : 'Panus',
        Dates        : 'Kuupäevad'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Täiustatud',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Kavandamise režiim',
        'Effort driven'            : 'Tehtud pingutus',
        'Manually scheduled'       : 'Manuaalselt kavandatud',
        'Constraint type'          : 'Piirangu tüüp',
        'Constraint date'          : 'Piirangu kuupäev',
        Inactive                   : 'Mitteaktiivne',
        'Ignore resource calendar' : 'Ignoreeri ressursikalendrit'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Täiustatud',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Kavandamise režiim',
        'Effort driven'            : 'Tehtud pingutus',
        'Manually scheduled'       : 'Manuaalselt kavandatud',
        'Constraint type'          : 'Piirangu tüüp',
        'Constraint date'          : 'Piirangu kuupäev',
        Constraint                 : 'Piirang',
        Rollup                     : 'Ümberarvestus',
        Inactive                   : 'Mitteaktiivne',
        'Ignore resource calendar' : 'Ignoreeri ressursikalendrit',
        'Scheduling direction'     : 'Ajakava suund'
    },

    DependencyTab : {
        Predecessors      : 'Eelkäijad',
        Successors        : 'Järglased',
        ID                : 'ID',
        Name              : 'Nimi',
        Type              : 'Tüüp',
        Lag               : 'Kiht',
        cyclicDependency  : 'Tsükliline sõltuvus',
        invalidDependency : 'Kehtetu sõltuvus'
    },

    NotesTab : {
        Notes : 'Märkused'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ressursid',
        Resource  : 'Ressurss',
        Units     : 'Üksused'
    },

    RecurrenceTab : {
        title : 'Repeat'
    },

    SchedulingModePicker : {
        Normal           : 'Normaalne',
        'Fixed Duration' : 'Määratud kestus',
        'Fixed Units'    : 'Määratud üksused',
        'Fixed Effort'   : 'Määratud panus'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> määratust',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} / {available}</span> määratust',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} / {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Kuupäeval {startDate}<br><span class="{cls}">{allocated} / {available}</span> määratust:<br>{assignments}',
        plusMore              : '+{value} veel'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> määratust',
        barTipOnDate          : '<b>{event}</b> kuupäeval {startDate}<br><span class="{cls}">{allocated}</span> määratust',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> määratust:<br>{assignments}',
        groupBarTipOnDate     : 'Kuupäeval {startDate}<br><span class="{cls}">{allocated} / {available}</span> määratust:<br>{assignments}',
        plusMore              : '+{value} veel',
        nameColumnText        : 'Ressurss / sündmus'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Tühista muudatus ja ära tee midagi',
        schedulingConflict : 'Kavandamise konflikt',
        emptyCalendar      : 'Kalendri konfigureerimise viga',
        cycle              : 'Kavandamistsükkel',
        Apply              : 'Rakenda'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Palun vali sõltuvus:',
        invalidDependencyLabel : 'Kaasatud on kehtetuid sõltuvusi, millega tuleb tegeleda:'
    },

    DependencyEdit : {
        Active : 'Aktiivne'
    },

    SchedulerProBase : {
        propagating     : 'Projekti arvutamine',
        storePopulation : 'Andmete laadimine',
        finalizing      : 'Tulemuste lõpetamine'
    },

    EventSegments : {
        splitEvent    : 'Jaga sündmus',
        renameSegment : 'Nimeta ümber'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Pesastamise eemaldamine ei ole lubatud',
        nestingNotAllowed   : 'Pesastamine ei ole lubatud'
    },

    VersionGrid : {
        compare       : 'Võrdle',
        description   : 'Kirjeldus',
        occurredAt    : 'Toimus',
        rename        : 'Nimeta ümber',
        restore       : 'Taasta',
        stopComparing : 'Lõpeta võrdlemine'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'ülesanne',
            AssignmentModel : 'määramine',
            DependencyModel : 'link',
            ProjectModel    : 'projekt',
            ResourceModel   : 'ressurss',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'ülesanded',
            AssignmentModel : 'määramised',
            DependencyModel : 'lingid',
            ProjectModel    : 'projektid',
            ResourceModel   : 'ressursid',
            other           : 'objektid'
        },
        transactionDescriptions : {
            update : 'Muudetud {n} {entities}',
            add    : 'Lisatud {n} {entities}',
            remove : 'Eemaldatud {n} {entities}',
            move   : 'Teisaldatud {n} {entities}',
            mixed  : 'Muudetud {n} {entities}'
        },
        addEntity         : 'Lisatud {type} **{name}**',
        removeEntity      : 'Eemaldatud {type} **{name}**',
        updateEntity      : 'Muudetud {type} **{name}**',
        moveEntity        : 'Teisaldatud {type} **{name}** kohast {from} kohta {to}',
        addDependency     : 'Lisatud link kohast **{from}** kohta **{to}**',
        removeDependency  : 'Eemaldatud link kohast **{from}** kohta **{to}**',
        updateDependency  : 'Muudetud linki kohast **{from}** kohta **{to}**',
        addAssignment     : 'Määratud **{resource}** sündmusele **{event}**',
        removeAssignment  : 'Eemaldatud määramine: **{resource}** sündmusest **{event}**',
        updateAssignment  : 'Muudetud määramist: **{resource}** sündmusele **{event}**',
        noChanges         : 'Muudatusi pole',
        nullValue         : 'puudub',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Ennistas muudatused',
        redid             : 'Tegi uuesti muudatused',
        editedTask        : 'Muutis ülesande omadusi',
        deletedTask       : 'Kustutas ülesande',
        movedTask         : 'Teisaldas ülesande',
        movedTasks        : 'Teisaldas ülesanded'
    }
};

export default LocaleHelper.publishLocale(locale);
