import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/FrFr.js';
import '../../Scheduler/localization/FrFr.js';

const locale = {

    localeName : 'FrFr',
    localeDesc : 'Français (France)',
    localeCode : 'fr-FR',

    ConstraintTypePicker : {
        none                : 'Aucune',
        assoonaspossible    : 'Le plus tôt possible',
        aslateaspossible    : 'Le plus tard possible',
        muststarton         : 'Doit débuter le',
        mustfinishon        : 'Doit finir le',
        startnoearlierthan  : 'Début au plus tôt le',
        startnolaterthan    : 'Début au plus tard le',
        finishnoearlierthan : 'Fin au plus tôt le',
        finishnolaterthan   : 'Fin au plus tard le'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Avant',
        Backward      : 'Arrière',
        inheritedFrom : 'Hérité de',
        enforcedBy    : 'Imposé par'
    },

    CalendarField : {
        'Default calendar' : 'Calendrier par défaut'
    },

    TaskEditorBase : {
        Information   : 'Information',
        Save          : 'Enregistrer',
        Cancel        : 'Annuler',
        Delete        : 'Supprimer',
        calculateMask : 'Calcul en cours...',
        saveError     : "Impossible d'enregistrer, corrigez d'abord les erreurs",
        repeatingInfo : 'Afficher un événement récurrent',
        editRepeating : 'Modifier'
    },

    TaskEdit : {
        'Edit task'            : 'Éditer la tâche',
        ConfirmDeletionTitle   : 'Confirmer la suppression',
        ConfirmDeletionMessage : "Êtes-vous sûr de vouloir supprimer l'événement ?"
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Général',
        Name         : 'Nom',
        Resources    : 'Ressources',
        '% complete' : 'Achevées à %',
        Duration     : 'Durée',
        Start        : 'Début',
        Finish       : 'Fin',
        Effort       : 'Effort',
        Preamble     : 'Préambule',
        Postamble    : 'Postambule'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Général',
        Name         : 'Nom',
        '% complete' : 'Achevées à %',
        Duration     : 'Durée',
        Start        : 'Début',
        Finish       : 'Fin',
        Effort       : 'Effort',
        Dates        : 'Dates'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avancé',
        Calendar                   : 'Calendrier',
        'Scheduling mode'          : 'Mode de planification',
        'Effort driven'            : 'Effort fourni',
        'Manually scheduled'       : 'Planifié manuellement',
        'Constraint type'          : 'Type de contrainte',
        'Constraint date'          : 'Date de contrainte',
        Inactive                   : 'Inactif',
        'Ignore resource calendar' : 'Ignorer le calendrier des ressources'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Advancé',
        Calendar                   : 'Calendrier',
        'Scheduling mode'          : 'Mode de planification',
        'Effort driven'            : 'Effort fourni',
        'Manually scheduled'       : 'Planifié manuellement',
        'Constraint type'          : 'Type de contrainte',
        'Constraint date'          : 'Date de contrainte',
        Constraint                 : 'Contrainte',
        Rollup                     : 'Report au récapitulatif',
        Inactive                   : 'Inactif',
        'Ignore resource calendar' : 'Ignorer le calendrier des ressources',
        'Scheduling direction'     : 'Direction de planification'
    },

    DependencyTab : {
        Predecessors      : 'Prédécesseurs',
        Successors        : 'Successeurs',
        ID                : 'Identifiant',
        Name              : 'Nom',
        Type              : 'Type',
        Lag               : 'Retard',
        cyclicDependency  : 'Dépendance cyclique',
        invalidDependency : 'Dépendance invalide'
    },

    NotesTab : {
        Notes : 'Notes'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Ressources',
        Resource  : 'Ressource',
        Units     : 'Unités'
    },

    RecurrenceTab : {
        title : 'Répéter'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Durée fixe',
        'Fixed Units'    : 'Unités fixes',
        'Fixed Effort'   : 'Effort fixe'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} sur {available}</span> allouées',
        barTipOnDate          : '<b>{resource}</b> le {startDate}<br><span class="{cls}">{allocated} sur {available}</span> allouées',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} sur {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} sur {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Le {startDate}<br><span class="{cls}">{allocated} sur {available}</span> allouées :<br>{assignments}',
        plusMore              : '+{value} de plus'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> allouées',
        barTipOnDate          : '<b>{event}</b> le {startDate}<br><span class="{cls}">{allocated}</span> allouées',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} sur {available}</span> allouées :<br>{assignments}',
        groupBarTipOnDate     : 'Le {startDate}<br><span class="{cls}">{allocated} sur {available}</span> allouées :<br>{assignments}',
        plusMore              : '{value} de plus',
        nameColumnText        : 'Ressource / Événement'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Annuler le changement et ne rien faire',
        schedulingConflict : 'Conflit de planification',
        emptyCalendar      : 'Erreur de configuration du calendrier',
        cycle              : 'Cycle de planification',
        Apply              : 'Appliquer'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Veuillez sélectionner une dépendance :',
        invalidDependencyLabel : 'Des dépendances invalides sont impliquées et doivent être résolues :'
    },

    DependencyEdit : {
        Active : 'Active'
    },

    SchedulerProBase : {
        propagating     : 'Calcul du projet en cours',
        storePopulation : 'Chargement des données en cours',
        finalizing      : 'Résultats en cours de finalisation'
    },

    EventSegments : {
        splitEvent    : "Diviser l'événement",
        renameSegment : 'Renommer'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Désimbrication non autorisée',
        nestingNotAllowed   : 'Imbrication non autorisée'
    },

    VersionGrid : {
        compare       : 'Comparer',
        description   : 'Description',
        occurredAt    : "S'est produit à",
        rename        : 'Renommer',
        restore       : 'Restaurer',
        stopComparing : 'Arrêter de comparer'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tâche',
            AssignmentModel : 'affectation',
            DependencyModel : 'lien',
            ProjectModel    : 'projet',
            ResourceModel   : 'ressource',
            other           : 'objet'
        },
        entityNamesPlural : {
            TaskModel       : 'tâches',
            AssignmentModel : 'affectations',
            DependencyModel : 'liens',
            ProjectModel    : 'projets',
            ResourceModel   : 'ressources',
            other           : 'objets'
        },
        transactionDescriptions : {
            update : 'Modifié {n} {entities}',
            add    : 'Ajouté {n} {entities}',
            remove : 'Supprimé {n} {entities}',
            move   : 'Déplacé {n} {entities}',
            mixed  : 'Modifié {n} {entities}'
        },
        addEntity         : 'Ajouté {type} **{name}**',
        removeEntity      : 'Supprimé {type} **{name}**',
        updateEntity      : 'Modifié {type} **{name}**',
        moveEntity        : 'Déplacé {type} **{name}** de {from} à {to}',
        addDependency     : 'Ajouté le lien de **{from}** à **{to}**',
        removeDependency  : 'Supprimé le lien de **{from}** à **{to}**',
        updateDependency  : 'Modifié le lien de **{from}** à **{to}**',
        addAssignment     : 'Affecté **{resource}** à **{event}**',
        removeAssignment  : 'Supprimé **{resource}** de **{event}**',
        updateAssignment  : "Modifier l'éffectation de **{resource}** à **{event}**",
        noChanges         : 'Aucun changement',
        nullValue         : 'aucun',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Annulé les modifications',
        redid             : 'Rétabli les modifications',
        editedTask        : 'Modifié les propriétés de la tâche',
        deletedTask       : 'Supprimé une tâche',
        movedTask         : 'Déplacé une tâche',
        movedTasks        : 'Déplacé des tâches'
    }
};

export default LocaleHelper.publishLocale(locale);
