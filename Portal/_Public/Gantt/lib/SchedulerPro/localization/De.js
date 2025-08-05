import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/De.js';
import '../../Scheduler/localization/De.js';

const locale = {

    localeName : 'De',
    localeDesc : 'Deutsch',
    localeCode : 'de-DE',

    ConstraintTypePicker : {
        none                : 'Keine',
        assoonaspossible    : 'So bald wie möglich',
        aslateaspossible    : 'So spät wie möglich',
        muststarton         : 'Muss starten am',
        mustfinishon        : 'Muss fertig sein am',
        startnoearlierthan  : 'Darf nicht fürher als starten',
        startnolaterthan    : 'Nicht später als starten',
        finishnoearlierthan : 'Nicht früher als enden',
        finishnolaterthan   : 'Nicht später als enden'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Vorwärts',
        Backward      : 'Rückwärts',
        inheritedFrom : 'Geerbt von',
        enforcedBy    : 'Erzwungen von'
    },

    CalendarField : {
        'Default calendar' : 'Hauptkalender'
    },

    TaskEditorBase : {
        Information   : 'Information',
        Save          : 'Speichern',
        Cancel        : 'Abbrechen',
        Delete        : 'Löschen',
        calculateMask : 'Wird berechnet...',
        saveError     : 'Kann nicht gespeichert werden, bitte korrigieren Sie zuerst die Fehler',
        repeatingInfo : 'Ein sich wiederholendes Ereignis anzeigen',
        editRepeating : 'Bearbeiten'
    },

    TaskEdit : {
        'Edit task'            : 'Aufgabe Bearbeiten',
        ConfirmDeletionTitle   : 'Löschung bestätigen',
        ConfirmDeletionMessage : 'Sind Sie sicher, dass Sie das Ereignis löschen möchten?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Generell',
        Name         : 'Name',
        Resources    : 'Ressourcen',
        '% complete' : '% vollständig',
        Duration     : 'Dauer',
        Start        : 'Start',
        Finish       : 'Ende',
        Effort       : 'Anstrengung',
        Preamble     : 'Präambel',
        Postamble    : 'Postambel'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Allgemein',
        Name         : 'Name',
        '% complete' : '% Abgeschlossen',
        Duration     : 'Dauer',
        Start        : 'Anfang',
        Finish       : 'Ende',
        Effort       : 'Aufwand',
        Dates        : 'Termine'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Fortgeschritten',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Planungsmodus',
        'Effort driven'            : 'Durch Aufwand gesteuert',
        'Manually scheduled'       : 'Manuell terminiert',
        'Constraint type'          : 'Art der Einschränkung',
        'Constraint date'          : 'Einschränkungsdatum',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Ressourcenkalender ignorieren'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Erweitert',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Planungsmodus',
        'Effort driven'            : 'Leistungsgesteuert',
        'Manually scheduled'       : 'Manuell geplant',
        'Constraint type'          : 'Einschränkungsart',
        'Constraint date'          : 'Einschränkungsdatum',
        Constraint                 : 'Einschränkung',
        Rollup                     : 'Rollup',
        Inactive                   : 'Inaktiv',
        'Ignore resource calendar' : 'Terminplanung ignoriert Ressourcenkalender',
        'Scheduling direction'     : 'Terminierungsrichtung'
    },

    DependencyTab : {
        Predecessors      : 'Vorgänger',
        Successors        : 'Nachfolger',
        ID                : 'ID',
        Name              : 'Name',
        Type              : 'Typ',
        Lag               : 'Zeitabstand',
        cyclicDependency  : 'Zyklische Abhängigkeit',
        invalidDependency : 'Ungültige Abhängigkeit'
    },

    NotesTab : {
        Notes : 'Notizen'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ressourcen',
        Resource  : 'Ressource',
        Units     : 'Einheiten'
    },

    RecurrenceTab : {
        title : 'Wiederholen'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Fixierte Dauer',
        'Fixed Units'    : 'Fixierte Einheiten',
        'Fixed Effort'   : 'Fixierter Effort'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} von {available}</span> zugewiesen',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} von {available}</span> zugewiesen',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} von {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} von {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Am {startDate}<br><span class="{cls}">{allocated} vom {available}</span> zugewiesen:<br>{assignments}',
        plusMore              : '+{value} mehr'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{zugewiesem}</span> zugewiesen',
        barTipOnDate          : '<b>{event}</b> am {startDate}<br><span class="{cls}">{allocated}</span> zugewiesen',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} von {available}</span> zugewiesen:<br>{assignments}',
        groupBarTipOnDate     : 'Am {startDate}<br><span class="{cls}">{allocated} von {available}</span> zugewiesen:<br>{assignments}',
        plusMore              : '+{value} mehr',
        nameColumnText        : 'Ressource / Ereignis'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Die Änderung rückgängig machen und nichts tun',
        schedulingConflict : 'Terminkonflikt',
        emptyCalendar      : 'Kalenderkonfigurationsfehler',
        cycle              : 'Planungszyklus',
        Apply              : 'Anwenden'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Bitte wählen Sie eine Abhängigkeit:',
        invalidDependencyLabel : 'Es gibt ungültige Abhängigkeiten, die behoben werden müssen:'
    },

    DependencyEdit : {
        Active : 'Aktiv'
    },

    SchedulerProBase : {
        propagating     : 'Projekt wird berechnet',
        storePopulation : 'Daten werden geladen',
        finalizing      : 'Ergebnisse werden finalisiert'
    },

    EventSegments : {
        splitEvent    : 'Ereignis aufteilen',
        renameSegment : 'Umbenennen'
    },

    NestedEvents : {
        deNestingNotAllowed : 'De-nesting nicht erlaubt',
        nestingNotAllowed   : 'Nesting nicht erlaubt'
    },

    VersionGrid : {
        compare       : 'Vergleichen',
        description   : 'Beschreibung',
        occurredAt    : 'Trat auf bei',
        rename        : 'Umbenennen',
        restore       : 'Wiederherstellen',
        stopComparing : 'Vergleichen beenden'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'Aufgabe',
            AssignmentModel : 'Zuweisung',
            DependencyModel : 'Verknüpfung',
            ProjectModel    : 'Projekt',
            ResourceModel   : 'Ressource',
            other           : 'Objekt'
        },
        entityNamesPlural : {
            TaskModel       : 'Aufgaben',
            AssignmentModel : 'Zuweisungen',
            DependencyModel : 'Verknüpfungen',
            ProjectModel    : 'Projekte',
            ResourceModel   : 'Ressourcen',
            other           : 'Objekte'
        },
        transactionDescriptions : {
            update : '{n} geändert(e) {entities}',
            add    : '{n} hinzugefügt(e) {entities}',
            remove : '{n} entfernt(e) {entities}',
            move   : '{n} verschoben(e) {entities}',
            mixed  : '{n} geändert(e) {entities}'
        },
        addEntity         : '{type} **{name}** hinzugefügt',
        removeEntity      : '{type} **{name}** entfernt',
        updateEntity      : '{type} **{name}** geändert',
        moveEntity        : '{type} **{name}** von {from} nach {to} verschoben',
        addDependency     : 'Verknüpfung von **{from}** nach **{to}** hinzugefügt',
        removeDependency  : 'Verknüpfung von **{from}** nach **{to}** entfernt',
        updateDependency  : 'Verknüpfung von **{from}** nach **{to}** bearbeitet',
        addAssignment     : '**{resource}** zugewiesen zu **{event}**',
        removeAssignment  : 'Zuweisung von **{resource}** von **{event}** entfernt',
        updateAssignment  : 'Zuweisung von **{resource}** von **{event}** bearbeitet',
        noChanges         : 'Keine Änderungen',
        nullValue         : 'keine',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Änderungen rückgängig gemacht',
        redid             : 'Änderungen rückgängig gemacht',
        editedTask        : 'Aufgabeneigenschaften bearbeitet',
        deletedTask       : 'Eine Aufgabe gelöscht',
        movedTask         : 'Eine Aufgabe verschoben',
        movedTasks        : 'Aufgaben verschoben'
    }
};

export default LocaleHelper.publishLocale(locale);
