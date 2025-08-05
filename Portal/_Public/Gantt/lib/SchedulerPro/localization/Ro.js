import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ro.js';
import '../../Scheduler/localization/Ro.js';

const locale = {

    localeName : 'Ro',
    localeDesc : 'Română',
    localeCode : 'ro',

    ConstraintTypePicker : {
        none                : 'Niciunul',
        assoonaspossible    : 'Cât mai curând posibil',
        aslateaspossible    : 'Cât mai târziu posibil',
        muststarton         : 'Trebuie să înceapă la',
        mustfinishon        : 'Trebuie finalizat la',
        startnoearlierthan  : 'Începutul nu mai devreme de',
        startnolaterthan    : 'Începutul nu mai târziu de',
        finishnoearlierthan : 'Finalizare nu mai devreme de',
        finishnolaterthan   : 'Finalizare nu mai târziu de'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Înainte',
        Backward      : 'Înapoi',
        inheritedFrom : 'Moștenit de la',
        enforcedBy    : 'Impus de'
    },

    CalendarField : {
        'Default calendar' : 'Calendar implicit'
    },

    TaskEditorBase : {
        Information   : 'Informații',
        Save          : 'Salvare',
        Cancel        : 'Anulare',
        Delete        : 'Ștergere',
        calculateMask : 'Se calculează...',
        saveError     : 'Salvare imposibilă, vă rugăm să corectați mai întâi eroarea',
        repeatingInfo : 'Vizualizare eveniment recurent',
        editRepeating : 'Editare'
    },

    TaskEdit : {
        'Edit task'            : 'Editare sarcină',
        ConfirmDeletionTitle   : 'Confirmare ștergere',
        ConfirmDeletionMessage : 'Sigur doriți să ștergeți evenimentul?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Generalități',
        Name         : 'Nume',
        Resources    : 'Resurse',
        '% complete' : '% finalizat',
        Duration     : 'Durată',
        Start        : 'Start',
        Finish       : 'Finalizare',
        Effort       : 'Efort',
        Preamble     : 'Preambul',
        Postamble    : 'Epilog'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Generalități',
        Name         : 'Nume',
        '% complete' : '% finalizat',
        Duration     : 'Durată',
        Start        : 'Start',
        Finish       : 'Finalizare',
        Effort       : 'Efort',
        Dates        : 'Date'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avansat',
        Calendar                   : 'Calendar',
        'Scheduling mode'          : 'Mod programare',
        'Effort driven'            : 'Motivat de efort ',
        'Manually scheduled'       : 'Programat manual',
        'Constraint type'          : 'Tip de restricționare',
        'Constraint date'          : 'Data restricționării',
        Inactive                   : 'Inactiv',
        'Ignore resource calendar' : 'Ignorați calendarul de resurse'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Avansat',
        Calendar                   : 'Calendar',
        'Scheduling mode'          : 'Mod programare',
        'Effort driven'            : 'Motivat de efort ',
        'Manually scheduled'       : 'Programat manual',
        'Constraint type'          : 'Tip de restricționare',
        'Constraint date'          : 'Data restricționării',
        Constraint                 : 'Restricționare',
        Rollup                     : 'Cumul',
        Inactive                   : 'Inactiv',
        'Ignore resource calendar' : 'Ignorați calendarul de resurse',
        'Scheduling direction'     : 'Direcția programării'
    },

    DependencyTab : {
        Predecessors      : 'Predecesori',
        Successors        : 'Succesori',
        ID                : 'ID',
        Name              : 'Nume',
        Type              : 'Tip',
        Lag               : 'Întârziere',
        cyclicDependency  : 'Dependență ciclică',
        invalidDependency : 'Dependență nevalidă'
    },

    NotesTab : {
        Notes : 'Note'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Resurse',
        Resource  : 'Resursă',
        Units     : 'Unități'
    },

    RecurrenceTab : {
        title : 'Repetare'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Durată fixă',
        'Fixed Units'    : 'Unități fixe',
        'Fixed Effort'   : 'Efort fix'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} din {available}</span> alocat',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} din {available}</span> alocat',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} din {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} din {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'La {startDate}<br><span class="{cls}">{allocated} din {available}</span> alocat:<br>{assignments}',
        plusMore              : '+{value} mai mult'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> alocat',
        barTipOnDate          : '<b>{event}</b> la {startDate}<br><span class="{cls}">{allocated}</span> alocat',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} din {available}</span> alocat:<br>{assignments}',
        groupBarTipOnDate     : 'La {startDate}<br><span class="{cls}">{allocated} din {available}</span> alocat:<br>{assignments}',
        plusMore              : '+{value} mai mult',
        nameColumnText        : 'Resursă / eveniment'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Anulați modificarea și nu luați nicio acțiune',
        schedulingConflict : 'Conflict de programare',
        emptyCalendar      : 'Eroare de configurare calendar',
        cycle              : 'Ciclu de programare',
        Apply              : 'Aplicare'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Vă rugăm să selectați o dependență:',
        invalidDependencyLabel : 'Există dependențe nevalide care trebuie adresate:'
    },

    DependencyEdit : {
        Active : 'Activ'
    },

    SchedulerProBase : {
        propagating     : 'Se calculează proiectul',
        storePopulation : 'Se încarcă datele',
        finalizing      : 'Se finalizează rezultatele'
    },

    EventSegments : {
        splitEvent    : 'Împărțiți eveniment',
        renameSegment : 'Redenumire'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Anularea imbricării nu este permisă',
        nestingNotAllowed   : 'Imbricarea nu este permisă'
    },

    VersionGrid : {
        compare       : 'Compară',
        description   : 'Descriere',
        occurredAt    : 'S-a întâmplat la',
        rename        : 'Redenumește',
        restore       : 'Restaurează',
        stopComparing : 'Oprește compararea'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'sarcină',
            AssignmentModel : 'atribuire',
            DependencyModel : 'link',
            ProjectModel    : 'proiect',
            ResourceModel   : 'resursă',
            other           : 'obiect'
        },
        entityNamesPlural : {
            TaskModel       : 'sarcini',
            AssignmentModel : 'atribuiri',
            DependencyModel : 'link-uri',
            ProjectModel    : 'proiecte',
            ResourceModel   : 'resurse',
            other           : 'obiecte'
        },
        transactionDescriptions : {
            update : 'Schimbat {n} {entities}',
            add    : 'Adăugat {n} {entities}',
            remove : 'Înlăturat {n} {entities}',
            move   : 'Mutat {n} {entities}',
            mixed  : 'Schimbat {n} {entities}'
        },
        addEntity         : 'Adăugat {type} **{name}**',
        removeEntity      : 'Înlăturat {type} **{name}**',
        updateEntity      : 'Schimbat {type} **{name}**',
        moveEntity        : 'Mutat {type} **{name}** from {from} în {to}',
        addDependency     : 'Adăugat link from **{from}** în **{to}**',
        removeDependency  : 'Înlăturat link din **{from}** în **{to}**',
        updateDependency  : 'Editat link din **{from}** în **{to}**',
        addAssignment     : 'Atribuit **{resource}** în **{event}**',
        removeAssignment  : 'Înlăturat atribuire din **{resource}** din **{event}**',
        updateAssignment  : 'Editare atribuire din **{resource}** în **{event}**',
        noChanges         : 'Nicio schimbare',
        nullValue         : 'niciuna',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Anulat schimbări',
        redid             : 'Refăcut schimbări',
        editedTask        : 'Proprietățile sarcinii editate',
        deletedTask       : 'Sarcină ștearsă',
        movedTask         : 'Sarcină mutată',
        movedTasks        : 'Sarcini mutate'
    }
};

export default LocaleHelper.publishLocale(locale);
