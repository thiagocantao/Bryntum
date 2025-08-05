import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Hr.js';
import '../../Scheduler/localization/Hr.js';

const locale = {

    localeName : 'Hr',
    localeDesc : 'Hrvatski',
    localeCode : 'hr',

    ConstraintTypePicker : {
        none                : 'Nijedan',
        assoonaspossible    : 'Što je prije moguće',
        aslateaspossible    : 'Što je kasnije moguće',
        muststarton         : 'Treba započeti na',
        mustfinishon        : 'Treba završiti na',
        startnoearlierthan  : 'Započeti ne ranije od',
        startnolaterthan    : 'Započeti ne kasnije od',
        finishnoearlierthan : 'Završiti ne ranije od',
        finishnolaterthan   : 'Završiti ne kasnije od'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Naprijed',
        Backward      : 'Natrag',
        inheritedFrom : 'Naslijeđeno od',
        enforcedBy    : 'Nametnuto od'
    },

    CalendarField : {
        'Default calendar' : 'Zadani kalendar'
    },

    TaskEditorBase : {
        Information   : 'Informacije',
        Save          : 'Spremi',
        Cancel        : 'Otkaži',
        Delete        : 'Obriši',
        calculateMask : 'Izračun u tijeku...',
        saveError     : 'Spremanje nije moguće, najprije ispravite pogreške',
        repeatingInfo : 'Gledanje događaja koji se ponavlja',
        editRepeating : 'Uređivanje'
    },

    TaskEdit : {
        'Edit task'            : 'Uredi zadatak',
        ConfirmDeletionTitle   : 'Potvrdi brisanje',
        ConfirmDeletionMessage : 'Sigurno želite obrisati događaj?'
    },

    GanttTaskEditor : {
        editorWidth : '45em'
    },

    SchedulerTaskEditor : {
        editorWidth : '35em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Općenito',
        Name         : 'Naziv',
        Resources    : 'Resursi',
        '% complete' : '% dovršeno',
        Duration     : 'Trajanje',
        Start        : 'Početak',
        Finish       : 'Završetak',
        Effort       : 'Effort',
        Preamble     : 'Uvod',
        Postamble    : 'Zaključak'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Općenito',
        Name         : 'Naziv',
        '% complete' : '% dovršeno',
        Duration     : 'Trajanje',
        Start        : 'Početak',
        Finish       : 'Završetak',
        Effort       : 'Effort',
        Dates        : 'Datumi'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Način zakazivanja',
        'Effort driven'            : 'Utemeljeno na effortu',
        'Manually scheduled'       : 'Ručno zakazano',
        'Constraint type'          : 'Vrsta ograničenja',
        'Constraint date'          : 'Datum ograničenja',
        Inactive                   : 'Neaktivno',
        'Ignore resource calendar' : 'Zanemari kalendar resursa'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Napredno',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Način zakazivanja',
        'Effort driven'            : 'Utemeljeno na effortu',
        'Manually scheduled'       : 'Ručno zakazano',
        'Constraint type'          : 'Vrsta ograničenja',
        'Constraint date'          : 'Datum ograničenja',
        Constraint                 : 'Ograničenje',
        Rollup                     : 'Kumulirano',
        Inactive                   : 'Neaktivno',
        'Ignore resource calendar' : 'Zanemari kalendar resursa',
        'Scheduling direction'     : 'Smjer planiranja'
    },

    DependencyTab : {
        Predecessors      : 'Prethodnici',
        Successors        : 'Nasljednici',
        ID                : 'ID',
        Name              : 'Naziv',
        Type              : 'Vrsta',
        Lag               : 'Odgoda',
        cyclicDependency  : 'Ciklička ovisnost',
        invalidDependency : 'Nevažeća ovisnost'
    },

    NotesTab : {
        Notes : 'Bilješke'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Resursi',
        Resource  : 'Resurs',
        Units     : 'Jedinice'
    },

    RecurrenceTab : {
        title : 'Ponovi'
    },

    SchedulingModePicker : {
        Normal           : 'Normalno',
        'Fixed Duration' : 'Fiksno trajanje',
        'Fixed Units'    : 'Fiksne jedinice',
        'Fixed Effort'   : 'Fiksni napor'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodijeljeno',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodijeljeno',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} od {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Na {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodijeljeno:<br>{assignments}',
        plusMore              : '+{value} više'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> dodijeljeno',
        barTipOnDate          : '<b>{event}</b> na {startDate}<br><span class="{cls}">{allocated}</span> dodijeljeno',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} od {available}</span> dodijeljeno:<br>{assignments}',
        groupBarTipOnDate     : 'Na {startDate}<br><span class="{cls}">{allocated} od {available}</span> dodijeljeno:<br>{assignments}',
        plusMore              : '+{value} više',
        nameColumnText        : 'Resurs / događaj'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Otkaži promjenu i ne čini ništa',
        schedulingConflict : 'Preklapanje u rasporedu',
        emptyCalendar      : 'Konfiguracijska pogreška kalendara',
        cycle              : 'Ciklus rasporeda',
        Apply              : 'Primijeni'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Odaberite ovisnost:',
        invalidDependencyLabel : 'Uključene su nevažeće ovisnosti koje je potrebno riješiti:'
    },

    DependencyEdit : {
        Active : 'Aktivno'
    },

    SchedulerProBase : {
        propagating     : 'Izračunavanje projekta',
        storePopulation : 'Učitavanje podataka',
        finalizing      : 'Dovršavanje rezultata'
    },

    EventSegments : {
        splitEvent    : 'Odvojeni događaj',
        renameSegment : 'Preimenuj'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Odgniježđivanje nije dopušteno',
        nestingNotAllowed   : 'Gniježđenje nije dopušteno'
    },

    VersionGrid : {
        compare       : 'Usporedi',
        description   : 'Opis',
        occurredAt    : 'Dogodilo se u',
        rename        : 'Preimenuj',
        restore       : 'Vrati',
        stopComparing : 'Prekini usporedbu'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'zadaća',
            AssignmentModel : 'zadatak',
            DependencyModel : 'link',
            ProjectModel    : 'projekt',
            ResourceModel   : 'resurs',
            other           : 'objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'zadaća',
            AssignmentModel : 'zadatak',
            DependencyModel : 'links',
            ProjectModel    : 'projekt',
            ResourceModel   : 'resurs',
            other           : 'objekt'
        },
        transactionDescriptions : {
            update : 'Promijenjeno {n} {entiteti}',
            add    : 'Dodano {n} {entiteti}',
            remove : 'Uklonjeno {n} {entiteti}',
            move   : 'Pomaknuto {n} {entiteti}',
            mixed  : 'Promijenjeno {n} {entiteti}'
        },
        addEntity         : 'Dodano {tip} **{ime}**',
        removeEntity      : 'Uklonjeno {tip} **{ime}**',
        updateEntity      : 'Promijenjeno {tip} **{ime}**',
        moveEntity        : 'Pomaknuto {tip} **{ime}** od {od} do {do}',
        addDependency     : 'Dodano link od **{od}** do **{do}**',
        removeDependency  : 'Uklonjeno link od **{od}** do **{do}**',
        updateDependency  : 'Uređen link od **{od}** do **{do}**',
        addAssignment     : 'Assigned **{resursi}** do **{događaj}**',
        removeAssignment  : 'Uklonjeno zadatak of **{resursi}** od **{događaj}**',
        updateAssignment  : 'Uređen zadatak of **{resursi}** do **{događaj}**',
        noChanges         : 'Nema promjene',
        nullValue         : 'none',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Undid promjene',
        redid             : 'Redid promjene',
        editedTask        : 'Uređen zadatak i svojstva',
        deletedTask       : 'Obrisan zadatak',
        movedTask         : 'Pomaknuti zadatak',
        movedTasks        : 'Pomaknuti zadatak'
    }
};

export default LocaleHelper.publishLocale(locale);
