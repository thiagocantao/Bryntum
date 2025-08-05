import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Sl.js';
import '../../Scheduler/localization/Sl.js';

const locale = {

    localeName : 'Sl',
    localeDesc : 'Slovensko',
    localeCode : 'sl',

    ConstraintTypePicker : {
        none                : 'Brez',
        assoonaspossible    : 'Čim prej',
        aslateaspossible    : 'Čim kasneje',
        muststarton         : 'Začeti se mora na',
        mustfinishon        : 'Končati se mora na',
        startnoearlierthan  : 'Začetek ne prej kot',
        startnolaterthan    : 'Začetek najkasneje',
        finishnoearlierthan : 'Končati ne prej kot',
        finishnolaterthan   : 'Končati najkasneje'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Naprej',
        Backward      : 'Nazaj',
        inheritedFrom : 'Podedovani od',
        enforcedBy    : 'Prisiljeni od'
    },

    CalendarField : {
        'Default calendar' : 'Privzeti koledar'
    },

    TaskEditorBase : {
        Information   : 'Informacija',
        Save          : 'Shrani',
        Cancel        : 'Prekliči',
        Delete        : 'Izbriši',
        calculateMask : 'Računanje...',
        saveError     : 'Ni mogoče shraniti, najprej popravite napake',
        repeatingInfo : 'Ogled ponavljajočega se dogodka',
        editRepeating : 'Uredi'
    },

    TaskEdit : {
        'Edit task'            : 'Uredi opravilo',
        ConfirmDeletionTitle   : 'Potrdi brisanje',
        ConfirmDeletionMessage : 'Ste prepričani, da želite izbrisati dogodek?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Splošno',
        Name         : 'Ime',
        Resources    : 'Viri',
        '% complete' : '% dokončano',
        Duration     : 'Trajanje',
        Start        : 'Začetek',
        Finish       : 'Konec',
        Effort       : 'Trud',
        Preamble     : 'Preambula',
        Postamble    : 'Poambula'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Splošno',
        Name         : 'Ime',
        '% complete' : '% dokončano',
        Duration     : 'Trajanje',
        Start        : 'Začetek',
        Finish       : 'Konec',
        Effort       : 'Trud',
        Dates        : 'Datumi'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Koledar',
        'Scheduling mode'          : 'Način razporejanja',
        'Effort driven'            : 'Gnano po naporu',
        'Manually scheduled'       : 'Ročno razporejeno',
        'Constraint type'          : 'Vrsta omejitve',
        'Constraint date'          : 'Datum omejitve',
        Inactive                   : 'Neaktivno',
        'Ignore resource calendar' : 'Prezri koledar virov'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Koledar',
        'Scheduling mode'          : 'Način razporejanja',
        'Effort driven'            : 'Gnano po naporu',
        'Manually scheduled'       : 'Ročno razporejeno',
        'Constraint type'          : 'Vrsta omejitve',
        'Constraint date'          : 'Datum omejitve',
        Constraint                 : 'Omejitev',
        Rollup                     : 'Poročilo v povzetek',
        Inactive                   : 'Neaktivno',
        'Ignore resource calendar' : 'Prezri koledar virov',
        'Scheduling direction'     : 'Smer načrtovanja'
    },

    DependencyTab : {
        Predecessors      : 'Predhodniki',
        Successors        : 'Nasledniki',
        ID                : 'ID',
        Name              : 'Ime',
        Type              : 'Vrsta',
        Lag               : 'Zakasnitev',
        cyclicDependency  : 'Ciklična odvisnost',
        invalidDependency : 'Neveljavna odvisnost'
    },

    NotesTab : {
        Notes : 'Opombe'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Viri',
        Resource  : 'Vir',
        Units     : 'Enote'
    },

    RecurrenceTab : {
        title : 'Ponovi'
    },

    SchedulingModePicker : {
        Normal           : 'Normalno',
        'Fixed Duration' : 'Stalno trajanje',
        'Fixed Units'    : 'Stalne enote',
        'Fixed Effort'   : 'Stalni napor'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljenih',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljenih',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} od {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Na {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljenih:<br>{assignments}',
        plusMore              : '+{value} več'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> dodeljen',
        barTipOnDate          : '<b>{event}</b> na {startDate}<br><span class="{cls}">{allocated}</span> dodeljen',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljenih:<br>{assignments}',
        groupBarTipOnDate     : 'Na {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljenih:<br>{assignments}',
        plusMore              : '+{value} več',
        nameColumnText        : 'Vir / Dogodek'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Prekliči spremembo in ne stori ničesar',
        schedulingConflict : 'Spor pri razporedu',
        emptyCalendar      : 'Napaka konfiguracije koledarja',
        cycle              : 'Cikel razporejanja',
        Apply              : 'Uporabi'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Prosimo, izberite odvisnost:',
        invalidDependencyLabel : 'Vključene so neveljavne odvisnosti, ki jih je treba obravnavati:'
    },

    DependencyEdit : {
        Active : 'Aktivno'
    },

    SchedulerProBase : {
        propagating     : 'Preračunavanje projekta',
        storePopulation : 'Nalaganje podatkov',
        finalizing      : 'Dokončevanje rezultatov'
    },

    EventSegments : {
        splitEvent    : 'Razdeli dogodek',
        renameSegment : 'Preimenuj'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Odstranjevanje gnezdenja ni dovoljeno',
        nestingNotAllowed   : 'Gnezdenje ni dovoljeno'
    },

    VersionGrid : {
        compare       : 'Primerjaj',
        description   : 'Opis',
        occurredAt    : 'Zgodilo se ob',
        rename        : 'Preimenuj',
        restore       : 'Obnovi',
        stopComparing : 'Prenehaj primerjati'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'naloga',
            AssignmentModel : 'dodelitev',
            DependencyModel : 'povezava',
            ProjectModel    : 'projekt',
            ResourceModel   : 'vir',
            other           : 'predmet'
        },
        entityNamesPlural : {
            TaskModel       : 'naloge',
            AssignmentModel : 'dodelitve',
            DependencyModel : 'povezave',
            ProjectModel    : 'projekti',
            ResourceModel   : 'viri',
            other           : 'predmeti'
        },
        transactionDescriptions : {
            update : 'Spremenjenih {n} {entities}',
            add    : 'Dodanih {n} {entities}',
            remove : 'Odstranjenih {n} {entities}',
            move   : 'Premakjenih {n} {entities}',
            mixed  : 'Spremenjenih {n} {entities}'
        },
        addEntity         : 'Dodano {type} **{name}**',
        removeEntity      : 'Odstranjeno {type} **{name}**',
        updateEntity      : 'Spremenjeno {type} **{name}**',
        moveEntity        : 'Premaknjeno {type} **{name}** od {from} do {to}',
        addDependency     : 'Dodana povezava od **{from}** do **{to}**',
        removeDependency  : 'Odstranjena povezava od **{from}** do **{to}**',
        updateDependency  : 'Urejena povezava od **{from}** do **{to}**',
        addAssignment     : 'Dodeljen **{resource}** do **{event}**',
        removeAssignment  : 'Odstranjena dodelitev **{resource}** od **{event}**',
        updateAssignment  : 'Urejena dodelitev **{resource}** do **{event}**',
        noChanges         : 'Brez sprememb',
        nullValue         : 'nič',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Razveljavljene spremembe',
        redid             : 'Ponovno uvedene spremembe',
        editedTask        : 'Urejene lastnosti naloge',
        deletedTask       : 'Izbrisal nalogo',
        movedTask         : 'Premaknil nalogo',
        movedTasks        : 'Premaknjene naloge'
    }
};

export default LocaleHelper.publishLocale(locale);
