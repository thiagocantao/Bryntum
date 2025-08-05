import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/FrFr.js';

const locale = {

    localeName : 'FrFr',
    localeDesc : 'Français (France)',
    localeCode : 'fr-FR',

    Object : {
        newEvent : 'Nouvel événement'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' événement' + (data !== 1 ? 's' : '')
    },

    Dependencies : {
        from    : 'De',
        to      : 'à',
        valid   : 'Valide',
        invalid : 'Invalide'
    },

    DependencyType : {
        SS           : 'DD',
        SF           : 'DF',
        FS           : 'FD',
        FF           : 'FF',
        StartToStart : 'Du début au début',
        StartToEnd   : 'Du début à la fin',
        EndToStart   : 'De la fin au début',
        EndToEnd     : 'De la fin à la fin',
        short        : [
            'DD',
            'DF',
            'FD',
            'FF'
        ],
        long : [
            'Du début au début',
            'Du début à la fin',
            'De la fin au début',
            'De la fin à la fin'
        ]
    },

    DependencyEdit : {
        From              : 'Du',
        To                : 'Au',
        Type              : 'Type',
        Lag               : 'Retard',
        'Edit dependency' : 'Éditer la dépendance',
        Save              : 'Enregistrer',
        Delete            : 'Supprimer',
        Cancel            : 'Annuler',
        StartToStart      : 'Du début au début',
        StartToEnd        : 'Du début à la fin',
        EndToStart        : 'De la fin au début',
        EndToEnd          : 'De la fin à la fin'
    },

    EventEdit : {
        Name         : 'Nom',
        Resource     : 'Ressource',
        Start        : 'Début',
        End          : 'Fin',
        Save         : 'Enregistrer',
        Delete       : 'Supprimer',
        Cancel       : 'Annuler',
        'Edit event' : "Éditer l'événement",
        Repeat       : 'Répéter'
    },

    EventDrag : {
        eventOverlapsExisting : "Chevauchement d'événements, événement déjà existant pour cette ressource",
        noDropOutsideTimeline : "L'événement n'a peut-être pas été complètement déposé hors de la chronologie"
    },

    SchedulerBase : {
        'Add event'      : 'Ajouter un événement',
        'Delete event'   : 'Supprimer un événement',
        'Unassign event' : 'Désattribuer un événement',
        color            : 'Couleur'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Plage de dates',
        startText       : 'Date de début',
        endText         : 'Date de fin',
        todayText       : "Aujourd'hui"
    },

    EventCopyPaste : {
        copyEvent  : "Copier l'événement",
        cutEvent   : "Couper l'événement",
        pasteEvent : "Coller l'événement"
    },

    EventFilter : {
        filterEvents : 'Filtrer les tâches',
        byName       : 'Par nom'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Afficher la chronologie actuelle'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Secondes'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Jour'
        },
        day : {
            name : 'Jour/heures'
        },
        week : {
            name : 'Semaine/heures'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Semaine/jours'
        },
        dayAndMonth : {
            name : 'Mois'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Semaine'
        },
        weekAndMonth : {
            name : 'Semaines'
        },
        weekAndDayLetter : {
            name : 'Semaines/jours de la semaine'
        },
        weekDateAndMonth : {
            name : 'Mois/semaines'
        },
        monthAndYear : {
            name : 'Mois'
        },
        year : {
            name : 'Années'
        },
        manyYears : {
            name : 'Plusieurs années'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Vous êtes en train de supprimer un événement',
        'delete-all-message'        : 'Voulez-vous supprimer toutes les occurrences de cet événement ?',
        'delete-further-message'    : "Voulez-vous supprimer cet événement et toutes les occurrences futures de cet événement, ou uniquement l'occurrence sélectionnée ?",
        'delete-further-btn-text'   : 'Effacer tous les événements futurs',
        'delete-only-this-btn-text' : 'Effacer uniquement cet événement',
        'update-title'              : 'Vous modifiez un événement récurrent',
        'update-all-message'        : 'Voulez-vous modifier toutes les occurrences de cet événement ?',
        'update-further-message'    : "Voulez-vous changer seulement cette occurrence de l'événement, ou cette occurrence et toutes les occurrences futures ?",
        'update-further-btn-text'   : 'Tous les événements futurs',
        'update-only-this-btn-text' : 'Seulement cet événement',
        Yes                         : 'Oui',
        Cancel                      : 'Annuler',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' et ',
        Daily                           : 'Quotidien',
        'Weekly on {1}'                 : ({ days }) => `Hebdomadaire, le ${days}`,
        'Monthly on {1}'                : ({ days }) => `Mensuel, le ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Annuel, le ${days} de ${months}`,
        'Every {0} days'                : ({ interval }) => `Tous les ${interval} jours`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Toutes les ${interval} semaines, le ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Tous les ${interval} mois, le ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Tous les ${interval} ans, le ${days} ${months}`,
        position1                       : 'le premier',
        position2                       : 'le second',
        position3                       : 'le troisième',
        position4                       : 'le quatrième',
        position5                       : 'le cinquième',
        'position-1'                    : 'le dernier',
        day                             : 'jour',
        weekday                         : 'jour (semaine)',
        'weekend day'                   : 'jour (week-end)',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : "Répéter l'événement",
        Cancel              : 'Annuler',
        Save                : 'Sauvegarder',
        Frequency           : 'Fréquence',
        Every               : 'Tous les',
        DAILYintervalUnit   : 'jour(s)',
        WEEKLYintervalUnit  : 'semaine(s)',
        MONTHLYintervalUnit : 'mois(s)',
        YEARLYintervalUnit  : 'an(s)',
        Each                : 'Chaque',
        'On the'            : 'Le',
        'End repeat'        : 'Mettre fin à la répétition',
        'time(s)'           : 'fois(s)'
    },

    RecurrenceDaysCombo : {
        day           : 'jour',
        weekday       : 'jour (semaine)',
        'weekend day' : 'jour (week-end)'
    },

    RecurrencePositionsCombo : {
        position1    : 'premier',
        position2    : 'second',
        position3    : 'troisième',
        position4    : 'quatrième',
        position5    : 'cinquième',
        'position-1' : 'dernier'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Jamais',
        After     : 'Après',
        'On date' : 'Le'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Pas de répétition',
        Daily   : 'Quotidien',
        Weekly  : 'Hebdomadaire',
        Monthly : 'Mensuel',
        Yearly  : 'Annuel'
    },

    RecurrenceCombo : {
        None   : 'Aucun(e)',
        Custom : 'Personnalisé...'
    },

    Summary : {
        'Summary for' : date => `Résumé pour le ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Planning complet',
        currentview  : 'Planning visible',
        daterange    : 'Plage de dates',
        completedata : 'Planning complet (pour tous les événements)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Plage de planification',
        'Export from'    : 'Du',
        'Export to'      : 'Au'
    },

    ExcelExporter : {
        'No resource assigned' : 'Aucune ressource assignée'
    },

    CrudManagerView : {
        serverResponseLabel : 'Réponse du serveur :'
    },

    DurationColumn : {
        Duration : 'Durée'
    }
};

export default LocaleHelper.publishLocale(locale);
