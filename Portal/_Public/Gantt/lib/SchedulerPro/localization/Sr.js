import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Sr.js';
import '../../Scheduler/localization/Sr.js';

const locale = {

    localeName : 'Sr',
    localeDesc : 'Srpski',
    localeCode : 'sr',

    ConstraintTypePicker : {
        none                : 'Nema',
        assoonaspossible    : 'Što je pre moguće',
        aslateaspossible    : 'Što je kasnije moguće',
        muststarton         : 'Mora da počne',
        mustfinishon        : 'Mora da se završi',
        startnoearlierthan  : 'Počni ne ranije od',
        startnolaterthan    : 'Počni ne kasnije od',
        finishnoearlierthan : 'Završi ne ranije od',
        finishnolaterthan   : 'Završi ne kasnije od'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Napred',
        Backward      : 'Nazad',
        inheritedFrom : 'Наслеђено од',
        enforcedBy    : 'Намеће'
    },

    CalendarField : {
        'Default calendar' : 'Podrazumevani kalendar'
    },

    TaskEditorBase : {
        Information   : 'Informacija',
        Save          : 'Sačuvaj',
        Cancel        : 'Otkaži',
        Delete        : 'Obriši',
        calculateMask : 'Računam...',
        saveError     : 'Nije moguće sačuvati, najpre ispravi greške',
        repeatingInfo : 'Gledate događaj koji se ponavlja',
        editRepeating : 'Uredi'
    },

    TaskEdit : {
        'Edit task'            : 'Uredi zadatak',
        ConfirmDeletionTitle   : 'Potvrda brisanja',
        ConfirmDeletionMessage : 'Sigurno želiš da obrišeš ovaj događaj?'
    },

    GanttTaskEditor : {
        editorWidth : '45em'
    },

    SchedulerTaskEditor : {
        editorWidth : '35em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Opšte',
        Name         : 'Ime',
        Resources    : 'Resursi',
        '% complete' : '% dovršeno',
        Duration     : 'Trajanje',
        Start        : 'Početak',
        Finish       : 'Kraj',
        Effort       : 'Trud',
        Preamble     : 'Uvod',
        Postamble    : 'Posle uvoda'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Opšte',
        Name         : 'Ime',
        '% complete' : '% dovršeno',
        Duration     : 'Trajanje',
        Start        : 'Početak',
        Finish       : 'Kraj',
        Effort       : 'Trud',
        Dates        : 'Datumi'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Režim planiranja',
        'Effort driven'            : 'Gonjeno trudom',
        'Manually scheduled'       : 'Ručno planirano',
        'Constraint type'          : 'Tip ograničenja',
        'Constraint date'          : 'Datum ograničenja',
        Inactive                   : 'Neaktivan',
        'Ignore resource calendar' : 'Ignoriši kalendar resursa'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Režim planiranja',
        'Effort driven'            : 'Gonjeno trudom',
        'Manually scheduled'       : 'Ručno planirano',
        'Constraint type'          : 'Tip ograničenja',
        'Constraint date'          : 'Datum ograničenja',
        Constraint                 : 'Ograničenje',
        Rollup                     : 'Postignuće',
        Inactive                   : 'Neaktivan',
        'Ignore resource calendar' : 'Ignoriši kalendar resursa',
        'Scheduling direction'     : 'Smer planiranja'
    },

    DependencyTab : {
        Predecessors      : 'Prethodnici',
        Successors        : 'Naslednici',
        ID                : 'ID',
        Name              : 'Ime',
        Type              : 'Tipe',
        Lag               : 'Kašnjenje',
        cyclicDependency  : 'Ciklična zavisnost',
        invalidDependency : 'Neispravna zavisnost'
    },

    NotesTab : {
        Notes : 'Beleške'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Resursi',
        Resource  : 'Resurs',
        Units     : 'Jedinice'
    },

    RecurrenceTab : {
        title : 'Sa ponavljanjem'
    },

    SchedulingModePicker : {
        Normal           : 'Normalni',
        'Fixed Duration' : 'Fiksno trajanje',
        'Fixed Units'    : 'Fiksne jedinice',
        'Fixed Effort'   : 'Fiksni trud'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljeno',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljeno',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} od {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Dana {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljeno:<br>{assignments}',
        plusMore              : '+{value} više'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> dodeljeno',
        barTipOnDate          : '<b>{event}</b> on {startDate}<br><span class="{cls}">{allocated}</span> dodeljeno',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljeno:<br>{assignments}',
        groupBarTipOnDate     : 'Dana {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodeljeno:<br>{assignments}',
        plusMore              : '+{value} više',
        nameColumnText        : 'Resurs / Događaj'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Otkaži promenu i ne radi ništa',
        schedulingConflict : 'Konflikt pri planiranju',
        emptyCalendar      : 'Greška u konfiguraciji kalendara',
        cycle              : 'Ciklus planiranja',
        Apply              : 'Primeni'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Iaberitzavisnost:',
        invalidDependencyLabel : 'Postoje neispravne zavisnosti koje moraš da popraviš:'
    },

    DependencyEdit : {
        Active : 'Aktivna'
    },

    SchedulerProBase : {
        propagating     : 'Proračunavam projekat',
        storePopulation : 'Učitavam podatke',
        finalizing      : 'Završavam rezultate'
    },

    EventSegments : {
        splitEvent    : 'Podeli događaj',
        renameSegment : 'Preimenuj'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Uklanjanje iz ugnežđivanja nije dozvoljeno',
        nestingNotAllowed   : 'Ugnežđivanje nije dozvoljeno'
    },

    VersionGrid : {
        compare       : 'Uporedi',
        description   : 'Opis',
        occurredAt    : 'Dogodilo se u',
        rename        : 'Preimenuj',
        restore       : 'Vrati',
        stopComparing : 'Zaustavi upoređivanje'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'zadatak',
            AssignmentModel : 'zaduženje',
            DependencyModel : 'veza',
            ProjectModel    : 'projekat',
            ResourceModel   : 'resurs',
            other           : 'objekat'
        },
        entityNamesPlural : {
            TaskModel       : 'zadaci',
            AssignmentModel : 'zaduženja',
            DependencyModel : 'veze',
            ProjectModel    : 'projekti',
            ResourceModel   : 'resursi',
            other           : 'objekti'
        },
        transactionDescriptions : {
            update : 'Promenjeno {n} {entities}',
            add    : 'Dodato {n} {entities}',
            remove : 'Uklonjeno {n} {entities}',
            move   : 'Pomereno {n} {entities}',
            mixed  : 'Promenjeno {n} {entities}'
        },
        addEntity         : 'Dodato {type} **{name}**',
        removeEntity      : 'Uklonjeno {type} **{name}**',
        updateEntity      : 'Promenjeno {type} **{name}**',
        moveEntity        : 'Pomereno {type} **{name}** od {from} do {to}',
        addDependency     : 'Dodata veza od **{from}** do **{to}**',
        removeDependency  : 'Uklonjena veza od **{from}** do **{to}**',
        updateDependency  : 'Izmenjena veza od **{from}** do **{to}**',
        addAssignment     : 'Dodeljen **{resource}** na **{event}**',
        removeAssignment  : 'Uklonjen zadatak sa **{resource}** od **{event}**',
        updateAssignment  : 'Izmenjen zadatak sa **{resource}** na **{event}**',
        noChanges         : 'Nema promena',
        nullValue         : 'nema',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Vraćene promene',
        redid             : 'Ponovo primenjene promene',
        editedTask        : 'Izmeni svojstva zadatka',
        deletedTask       : 'Obrisan zadatak',
        movedTask         : 'Pomeren zadatak',
        movedTasks        : 'Pomereni zadaci'
    }
};

export default LocaleHelper.publishLocale(locale);
