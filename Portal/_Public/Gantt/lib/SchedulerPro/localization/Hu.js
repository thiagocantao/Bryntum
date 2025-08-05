import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Hu.js';
import '../../Scheduler/localization/Hu.js';

const locale = {

    localeName : 'Hu',
    localeDesc : 'Magyar',
    localeCode : 'hu',

    ConstraintTypePicker : {
        none                : 'Nincs',
        assoonaspossible    : 'Amint lehetséges',
        aslateaspossible    : 'Amennyire csak lehetséges',
        muststarton         : 'Muszáj elkezdődnie ekkor:',
        mustfinishon        : 'Muszáj befejeződnie ekkor:',
        startnoearlierthan  : 'Nem kezdődhet előbb mint',
        startnolaterthan    : 'Nem kezdődhet később mint',
        finishnoearlierthan : 'Nem érhet véget előbb mint',
        finishnolaterthan   : 'Nem érhet véget később mint'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Előre',
        Backward      : 'Hátra',
        inheritedFrom : 'Örökölt',
        enforcedBy    : 'Kényszerítve'
    },

    CalendarField : {
        'Default calendar' : 'Alapértelmezett naptár'
    },

    TaskEditorBase : {
        Information   : 'Információ',
        Save          : 'Mentés',
        Cancel        : 'Mégse',
        Delete        : 'Törlés',
        calculateMask : 'Számítás...',
        saveError     : 'Nem menthető, előbb javítsa a hibákat',
        repeatingInfo : 'Ismétlődő esemény megjelenítése',
        editRepeating : 'Szerkesztés'
    },

    TaskEdit : {
        'Edit task'            : 'Feladat szerkesztése',
        ConfirmDeletionTitle   : 'Törlés jóváhagyása',
        ConfirmDeletionMessage : 'Biztosan törli az eseményt?'
    },

    GanttTaskEditor : {
        editorWidth : '45em'
    },

    SchedulerTaskEditor : {
        editorWidth : '35em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Általános',
        Name         : 'Név',
        Resources    : 'Erőforrások',
        '% complete' : '% kész',
        Duration     : 'Időtartam',
        Start        : 'Kezdés',
        Finish       : 'Vége',
        Effort       : 'Effort',
        Preamble     : 'Bevezető',
        Postamble    : 'Lezárás'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Általános',
        Name         : 'Név',
        '% complete' : '% kész',
        Duration     : 'Időtartam',
        Start        : 'Kezdés',
        Finish       : 'Vége',
        Effort       : 'Effort',
        Dates        : 'Dátumok'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Speciális',
        Calendar                   : 'Naptár',
        'Scheduling mode'          : 'Ütemezési mód',
        'Effort driven'            : 'Munkaalapú',
        'Manually scheduled'       : 'Manuálisan ütemezve',
        'Constraint type'          : 'Korlátozás típusa',
        'Constraint date'          : 'Korlátozás dátuma',
        Inactive                   : 'Inaktív',
        'Ignore resource calendar' : 'Erőforrásnaptár figyelmen kívül hagyása'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Speciális',
        Calendar                   : 'Naptár',
        'Scheduling mode'          : 'Ütemezési mód',
        'Effort driven'            : 'Munkaalapú',
        'Manually scheduled'       : 'Manuálisan ütemezve',
        'Constraint type'          : 'Korlátozás típusa',
        'Constraint date'          : 'Korlátozás dátuma',
        Constraint                 : 'Korlátozás',
        Rollup                     : 'Összesítés',
        Inactive                   : 'Inaktív',
        'Ignore resource calendar' : 'Erőforrásnaptár figyelmen kívül hagyása',
        'Scheduling direction'     : 'Menetrend iránya'
    },

    DependencyTab : {
        Predecessors      : 'Elődök',
        Successors        : 'Utódok',
        ID                : 'Azonosító',
        Name              : 'Név',
        Type              : 'Típus',
        Lag               : 'Késés',
        cyclicDependency  : 'Ciklikus függőség',
        invalidDependency : 'Érvénytelen függőség'
    },

    NotesTab : {
        Notes : 'Jegyzetek'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Erőforrások',
        Resource  : 'Erőforrás',
        Units     : 'Egységek'
    },

    RecurrenceTab : {
        title : 'Ismétlés'
    },

    SchedulingModePicker : {
        Normal           : 'Normál',
        'Fixed Duration' : 'Rögzített időtartam',
        'Fixed Units'    : 'Rögzített egységek',
        'Fixed Effort'   : 'Rögzített munka'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> hozzárendelve',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} / {available}</span> > hozzárendelve',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} / {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : '{startDate} napon<br><span class="{cls}">{allocated} / {available}</span>hozzárendelve:<br>{assignments}',
        plusMore              : '+{value} további'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> hozzárendelve',
        barTipOnDate          : '<b>{event}</b> / {startDate}<br><span class="{cls}">{allocated}</span> hozzárendelve',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} / {available}</span> hozzárendelve:<br>{assignments}',
        groupBarTipOnDate     : '{startDate} napon<br><span class="{cls}">{allocated} / {available}</span> allocated:<br>{assignments}',
        plusMore              : '+{value} további',
        nameColumnText        : 'Erőforrás / esemény'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Törli a módosítást és nem tesz semmit',
        schedulingConflict : 'Ütemezési ütközés',
        emptyCalendar      : 'Naptárbeállítási hiba',
        cycle              : 'Ütemezési ciklus',
        Apply              : 'Alkalmazás'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Válasszon egy függőséget:',
        invalidDependencyLabel : 'Érvénytelen függőségek állnak fenn, amelyeket kezelni kell:'
    },

    DependencyEdit : {
        Active : 'Aktív'
    },

    SchedulerProBase : {
        propagating     : 'Projekt kiszámítása',
        storePopulation : 'Adatok betöltése',
        finalizing      : 'Eredmények véglegesítése'
    },

    EventSegments : {
        splitEvent    : 'Esemény megszakítása',
        renameSegment : 'Átnevezés'
    },

    NestedEvents : {
        deNestingNotAllowed : 'A beágyazás bontása tilos',
        nestingNotAllowed   : 'A beágyazás tilos'
    },

    VersionGrid : {
        compare       : 'Összehasonlítás',
        description   : 'Leírás',
        occurredAt    : 'Bekövetkezett',
        rename        : 'Átnevezés',
        restore       : 'Visszaállítás',
        stopComparing : 'Összehasonlítás befejezése'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'feladat',
            AssignmentModel : 'hozzárendelés',
            DependencyModel : 'csatolás',
            ProjectModel    : 'projekt',
            ResourceModel   : 'erőforrás',
            other           : 'objektum'
        },
        entityNamesPlural : {
            TaskModel       : 'feladatok',
            AssignmentModel : 'hozzárendelések',
            DependencyModel : 'csatolások',
            ProjectModel    : 'projektek',
            ResourceModel   : 'erőforrások',
            other           : 'objektumok'
        },
        transactionDescriptions : {
            update : 'Lecserélt {n} {entities}',
            add    : 'Hozzáadott {n} {entities}',
            remove : 'Eltávolított {n} {entities}',
            move   : 'Áthelyezett {n} {entities}',
            mixed  : 'Lecserélt {n} {entities}'
        },
        addEntity         : 'Hozzáadott {type} **{name}**',
        removeEntity      : 'Eltávolított {type} **{name}**',
        updateEntity      : 'Lecserélt {type} **{name}**',
        moveEntity        : 'Áthelyezett {type} **{name}** ettől {from} eddig {to}',
        addDependency     : 'Hozzáadott csatolás ettől **{from}** eddig **{to}**',
        removeDependency  : 'Eltávolított csatolás ettől **{from}** eddig **{to}**',
        updateDependency  : 'Szerkesztett csatolás ettől **{from}** eddig **{to}**',
        addAssignment     : 'Hozzárendelt **{resource}** a következőhöz **{event}**',
        removeAssignment  : 'Eltávolított hozzárendelés **{resource}** a következőtől **{event}**',
        updateAssignment  : 'Szerkesztett hozzárendelés **{resource}** a következőhöz **{event}**',
        noChanges         : 'Nincs módosítás',
        nullValue         : 'nincs',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Módosítások törlése',
        redid             : 'Módosítások visszaállítása',
        editedTask        : 'Szerkesztett feladatok tulajdonságai',
        deletedTask       : 'Törölt feladat',
        movedTask         : 'Áthelyezett feladat',
        movedTasks        : 'Áthelyezett feladatok'
    }
};

export default LocaleHelper.publishLocale(locale);
