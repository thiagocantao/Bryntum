import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/FrFr.js';

const emptyString = new String();

const locale = {

    localeName : 'FrFr',
    localeDesc : 'Français (France)',
    localeCode : 'fr-FR',

    ColumnPicker : {
        column          : 'Colonne',
        columnsMenu     : 'Colonnes',
        hideColumn      : 'Masquer la colonne',
        hideColumnShort : 'Masquer',
        newColumns      : 'Nouvelles colonnes'
    },

    Filter : {
        applyFilter   : 'Appliquer le filtre',
        filter        : 'Filtre',
        editFilter    : 'Éditer le filtre',
        on            : 'Le',
        before        : 'Avant',
        after         : 'Après',
        equals        : 'Égal à',
        lessThan      : 'Inférieur à',
        moreThan      : 'Supérieur à',
        removeFilter  : 'Supprimer le filtre',
        disableFilter : 'Désactiver le filtre'
    },

    FilterBar : {
        enableFilterBar  : 'Afficher la barre de filtre',
        disableFilterBar : 'Masquer la barre de filtre'
    },

    Group : {
        group                : 'Regrouper',
        groupAscending       : 'Grouper par ordre croissant',
        groupDescending      : 'Grouper par ordre décroissant',
        groupAscendingShort  : 'Croissant',
        groupDescendingShort : 'Décroissant',
        stopGrouping         : 'Arrêter le regroupement',
        stopGroupingShort    : 'Arrêter'
    },

    HeaderMenu : {
        moveBefore     : text => `Déplacer avant "${text}"`,
        moveAfter      : text => `Déplacer après "${text}"`,
        collapseColumn : 'Réduire la colonne',
        expandColumn   : 'Développer la colonne'
    },

    ColumnRename : {
        rename : 'Renommer'
    },

    MergeCells : {
        mergeCells  : 'Fusionner les cellules',
        menuTooltip : 'Fusionner les cellules de même valeur quand elles sont triées dans cette colonne'
    },

    Search : {
        searchForValue : 'Rechercher la valeur'
    },

    Sort : {
        sort                   : 'Trier',
        sortAscending          : 'Trier par ordre croissant',
        sortDescending         : 'Trier par ordre décroissant',
        multiSort              : 'Tri multiple',
        removeSorter           : 'Supprimer le tri',
        addSortAscending       : 'Ajouter un triage par ordre croissant',
        addSortDescending      : 'Ajouter un triage par ordre décroissant',
        toggleSortAscending    : 'Passer en ordre croissant',
        toggleSortDescending   : 'Passer en ordre décroissant',
        sortAscendingShort     : 'Croissant',
        sortDescendingShort    : 'Décroissant',
        removeSorterShort      : 'Retirer',
        addSortAscendingShort  : '+ Croissant',
        addSortDescendingShort : '+ Décroissant'
    },

    Split : {
        split        : 'Diviser',
        unsplit      : 'Non divisé',
        horizontally : 'Horizontalement',
        vertically   : 'Verticalement',
        both         : 'Les deux'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} colonne. ` : ''}ESPACE pour le menu contextuel${column.sortable ? ', ENTRÉE pour trier' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Basculer la sélection de ligne',
        toggleSelection : 'Basculer la sélection du jeu de données complet'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `évaluation : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Échec de chargement des données !',
        syncFailedMessage  : 'Échec de synchronisation des données !',
        unspecifiedFailure : 'Erreur non spécifiée',
        networkFailure     : 'Erreur réseau',
        parseFailure       : 'Erreur de décryptage de la réponse du serveur',
        serverResponse     : 'Réponse du serveur :',
        noRows             : 'Aucun enregistrement à afficher',
        moveColumnLeft     : 'Déplacer vers la section de gauche',
        moveColumnRight    : 'Déplacer vers la section de droite',
        moveColumnTo       : region => `Déplacer la colonne vers ${region}`
    },

    CellMenu : {
        removeRow : 'Supprimer'
    },

    RowCopyPaste : {
        copyRecord  : 'Copier',
        cutRecord   : 'Couper',
        pasteRecord : 'Coller',
        rows        : 'lignes',
        row         : 'ligne'
    },

    CellCopyPaste : {
        copy  : 'Copier',
        cut   : 'Couper',
        paste : 'Coller'
    },

    PdfExport : {
        'Waiting for response from server' : 'En attente de la réponse du serveur...',
        'Export failed'                    : "Échec de l'export",
        'Server error'                     : 'Erreur serveur',
        'Generating pages'                 : 'Génération de pages en cours...',
        'Click to abort'                   : 'Annuler'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Exporter les paramètres',
        export         : 'Exporter',
        exporterType   : 'Contrôle de la pagination',
        cancel         : 'Annuler',
        fileFormat     : 'Format de fichier',
        rows           : 'Lignes',
        alignRows      : 'Aligner les lignes',
        columns        : 'Colonnes',
        paperFormat    : 'Format du papier',
        orientation    : 'Orientation',
        repeatHeader   : "Répéter l'en-tête"
    },

    ExportRowsCombo : {
        all     : 'Toutes les lignes',
        visible : 'Lignes visibles'
    },

    ExportOrientationCombo : {
        portrait  : 'Portrait',
        landscape : 'Paysage'
    },

    SinglePageExporter : {
        singlepage : 'Une seule page'
    },

    MultiPageExporter : {
        multipage     : 'Plusieurs pages',
        exportingPage : ({ currentPage, totalPages }) => `Export de la page ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Plusieurs pages (en vertical)',
        exportingPage     : ({ currentPage, totalPages }) => `Export de la page ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Chargement en cours',
        expand   : 'Développer',
        collapse : 'Réduire'
    },

    TreeGroup : {
        group                  : 'Regrouper par',
        stopGrouping           : 'Arrêter le regroupement',
        stopGroupingThisColumn : 'Annuler le regroupement de cette colonne'
    }
};

export default LocaleHelper.publishLocale(locale);
