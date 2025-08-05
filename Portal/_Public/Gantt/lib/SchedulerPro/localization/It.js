import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/It.js';
import '../../Scheduler/localization/It.js';

const locale = {

    localeName : 'It',
    localeDesc : 'Italiano',
    localeCode : 'it',

    ConstraintTypePicker : {
        none                : 'Nessuno',
        assoonaspossible    : 'Il più presto possibile',
        aslateaspossible    : 'Il più tardi possibile',
        muststarton         : 'Deve iniziare il',
        mustfinishon        : 'Deve finire il',
        startnoearlierthan  : 'Inizio non prima di',
        startnolaterthan    : 'Inizio non oltre',
        finishnoearlierthan : 'Fine non prima di',
        finishnolaterthan   : 'Fine non oltre'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Avanti',
        Backward      : 'Indietro',
        inheritedFrom : 'Ereditato da',
        enforcedBy    : 'Impossato da'
    },

    CalendarField : {
        'Default calendar' : 'Calendario predefinito'
    },

    TaskEditorBase : {
        Information   : 'Informazioni',
        Save          : 'Salva',
        Cancel        : 'Annulla',
        Delete        : 'Elimina',
        calculateMask : 'Calcolo...',
        saveError     : 'Impossibile salvare, correggi prima gli errori',
        repeatingInfo : 'Visualizzare un evento ripetitivo',
        editRepeating : 'Modifica'
    },

    TaskEdit : {
        'Edit task'            : 'Modifica attività',
        ConfirmDeletionTitle   : 'Conferma eliminazione',
        ConfirmDeletionMessage : 'Vuoi davvero eliminare l’evento?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Generale',
        Name         : 'Nome',
        Resources    : 'Risorse',
        '% complete' : '% completato',
        Duration     : 'Durata',
        Start        : 'Inizio',
        Finish       : 'Fine',
        Effort       : 'Sforzo',
        Preamble     : 'Preambolo',
        Postamble    : 'Postfazione'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Generale',
        Name         : 'Nome',
        '% complete' : '% completato',
        Duration     : 'Durata',
        Start        : 'Inizio',
        Finish       : 'Fine',
        Effort       : 'Sforzo',
        Dates        : 'Date'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avanzato',
        Calendar                   : 'Calendario',
        'Scheduling mode'          : 'Modalità di programmazione',
        'Effort driven'            : 'Basato sullo sforzo',
        'Manually scheduled'       : 'Programmato manualmente',
        'Constraint type'          : 'Tipo di vincolo',
        'Constraint date'          : 'Data di vincolo',
        Inactive                   : 'Disattivo',
        'Ignore resource calendar' : 'Ignora il calendario delle risorse'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Avanzato',
        Calendar                   : 'Calendario',
        'Scheduling mode'          : 'Modalità di programmazione',
        'Effort driven'            : 'Basato sullo sforzo',
        'Manually scheduled'       : 'Programmato manualmente',
        'Constraint type'          : 'Tipo di vincolo',
        'Constraint date'          : 'Data di vincolo',
        Constraint                 : 'Vincolo',
        Rollup                     : 'Rollup',
        Inactive                   : 'Disattivo',
        'Ignore resource calendar' : 'Ignora il calendario delle risorse',
        'Scheduling direction'     : 'Direzione della pianificazione'
    },

    DependencyTab : {
        Predecessors      : 'Precedenti',
        Successors        : 'Successivi',
        ID                : 'ID',
        Name              : 'Nome',
        Type              : 'Tipo',
        Lag               : 'Ritardo',
        cyclicDependency  : 'Dipendenza ciclica',
        invalidDependency : 'Dipendenza non valida'
    },

    NotesTab : {
        Notes : 'Note'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Risorse',
        Resource  : 'Risorsa',
        Units     : 'Unità'
    },

    RecurrenceTab : {
        title : 'Ripeti'
    },

    SchedulingModePicker : {
        Normal           : 'Normale',
        'Fixed Duration' : 'Durata fissa',
        'Fixed Units'    : 'Unità fisse',
        'Fixed Effort'   : 'Sforzo fisso'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} di {available}</span> assegnato',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} di {available}</span> assegnato',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} di {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} di {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Il {startDate}<br><span class="{cls}">{allocated} di {available}</span> assegnato:<br>{assignments}',
        plusMore              : 'altre +{value}'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> assegnato',
        barTipOnDate          : '<b>{event}</b> il {startDate}<br><span class="{cls}">{allocated}</span> assegnato',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} di {available}</span> assegnato:<br>{assignments}',
        groupBarTipOnDate     : 'Il {startDate}<br><span class="{cls}">{allocated} di {available}</span> assegnato:<br>{assignments}',
        plusMore              : 'altre +{value}',
        nameColumnText        : 'Risorsa / Evento'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Annulla la modifica e non fare nulla',
        schedulingConflict : 'Conflitto di programmazione',
        emptyCalendar      : 'Errore di configurazione calendario',
        cycle              : 'Ciclo di programmazione',
        Apply              : 'Applica'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Seleziona una dipendenza:',
        invalidDependencyLabel : 'Ci sono dipendenze non valide da risolvere:'
    },

    DependencyEdit : {
        Active : 'Attiva'
    },

    SchedulerProBase : {
        propagating     : 'Calcolo progetto',
        storePopulation : 'Caricamento dati',
        finalizing      : 'Finalizzazione risultati'
    },

    EventSegments : {
        splitEvent    : 'Dividi evento',
        renameSegment : 'Rinomina'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Denidificazione non consentita',
        nestingNotAllowed   : 'Nidificazione non consentita'
    },

    VersionGrid : {
        compare       : 'Confronta',
        description   : 'Descrizione',
        occurredAt    : 'Si è verificato il',
        rename        : 'Rinomina',
        restore       : 'Ripristina',
        stopComparing : 'Interrompi confronto'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'compito',
            AssignmentModel : 'assegnazione',
            DependencyModel : 'link',
            ProjectModel    : 'progetto',
            ResourceModel   : 'risorsa',
            other           : 'oggetto'
        },
        entityNamesPlural : {
            TaskModel       : 'compiti',
            AssignmentModel : 'assegnazioni',
            DependencyModel : 'link',
            ProjectModel    : 'progetti',
            ResourceModel   : 'risorse',
            other           : 'oggetti'
        },
        transactionDescriptions : {
            update : 'Modificate {n} {entities}',
            add    : 'Aggiunte {n} {entities}',
            remove : 'Rimosse {n} {entities}',
            move   : 'Spostate {n} {entities}',
            mixed  : 'Modificate {n} {entities}'
        },
        addEntity         : 'Aggiunto {type} **{name}**',
        removeEntity      : 'Rimosso {type} **{name}**',
        updateEntity      : 'Modificato {type} **{name}**',
        moveEntity        : 'Spostato {type} **{name}** da {from} a {to}',
        addDependency     : 'Aggiunto link da **{from}** a **{to}**',
        removeDependency  : 'Rimosso link da **{from}** a **{to}**',
        updateDependency  : 'Modificato link da **{from}** a **{to}**',
        addAssignment     : 'Assegnato **{resource}** a **{event}**',
        removeAssignment  : 'Rimossa assegnazione di **{resource}** da **{event}**',
        updateAssignment  : 'Modificata assegnazione di **{resource}** a **{event}**',
        noChanges         : 'Nessuna modifica',
        nullValue         : 'nessuno',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Modifiche annullate',
        redid             : 'Modifiche ripristinate',
        editedTask        : 'Proprietà compito modificate',
        deletedTask       : 'Compito eliminato',
        movedTask         : 'Compito spostato',
        movedTasks        : 'Compiti spostati'
    }
};

export default LocaleHelper.publishLocale(locale);
