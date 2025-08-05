import LocaleHelper from '../../../lib/Core/localization/LocaleHelper.js';
import '../../../lib/Gantt/localization/Nl.js';
import './shared.locale.Nl.js';

const locale = {

    localeName : 'Nl',
    localeDesc : 'Nederlands',
    localeCode : 'nl',

    Baselines : {
        baseline           : 'baslinje',
        Complete           : 'Gedaan',
        'Delayed start by' : 'Uitgestelde start met',
        Duration           : 'Duur',
        End                : 'Einde',
        'Overrun by'       : 'Overschreden met',
        Start              : 'Begin'
    },

    Button : {
        Create           : 'Creëer',
        'Critical paths' : 'Kritieke paden',
        Edit             : 'Bewerk',
        'Export to PDF'  : 'Exporteren naar PDF',
        Features         : 'Kenmerken',
        Settings         : 'Instellingen'
    },

    DateColumn : {
        Deadline : 'Deadline'
    },

    Field : {
        'Find tasks by name' : 'Zoek taken op naam',
        'Project start'      : 'Start van het project'
    },

    GanttToolbar : {
        'First select the task you want to edit' : 'Selecteer eerst de taak die u wilt bewerken',
        'New task'                               : 'Nieuwe taak'
    },

    Indicators : {
        Indicators     : 'Indicatoren',
        constraintDate : 'Beperking'
    },

    MenuItem : {
        'Draw dependencies'          : 'Teken afhankelijkheden',
        'Enable cell editing'        : 'Schakel celbewerking in',
        'Hide schedule'              : 'Verberg schema',
        'Highlight non-working time' : 'Markeer niet-werkende tijd',
        'Project lines'              : 'Projectlijnen',
        'Show baselines'             : 'Toon basislijnen',
        'Show progress line'         : 'Toon voortgangslijn',
        'Show rollups'               : 'Rollups weergeven',
        'Task labels'                : 'Taaklabels'
    },

    Slider : {
        'Animation duration ' : 'Animatieduur',
        'Bar margin'          : 'Staafmarge',
        'Row height'          : 'Rijhoogte'
    },

    StartDateColumn : {
        'Start date' : 'Startdatum'
    },

    StatusColumn : {
        Status : 'Toestand'
    },

    TaskTooltip : {
        'Scheduling Mode' : 'Planningsmodus',
        Calendar          : 'Kalender',
        Critical          : 'Kritisch'
    },

    Tooltip : {
        'Adjust settings'          : 'Pas instellingen aan',
        'Collapse all'             : 'Alles inklappen',
        'Create new task'          : 'Maak een nieuwe taak',
        'Edit selected task'       : 'Bewerk de geselecteerde taak',
        'Expand all'               : 'Alles uitvouwen',
        'Highlight critical paths' : 'Markeer kritieke paden',
        'Next time span'           : 'Volgende tijdspanne',
        'Previous time span'       : 'Vorige tijdspanne',
        'Toggle features'          : 'Schakel tussen functies',
        'Zoom in'                  : 'In zoomen',
        'Zoom out'                 : 'Uitzoomen',
        'Zoom to fit'              : 'Zoom in om te passen'
    }
};

export default LocaleHelper.publishLocale(locale);
