import LocaleHelper from '../../../lib/Core/localization/LocaleHelper.js';
import '../../../lib/Gantt/localization/SvSE.js';
import './shared.locale.SvSE.js';

const locale = {

    localeName : 'SvSE',
    localeDesc : 'Svenska',
    localeCode : 'sv-SE',

    Baselines : {
        Start              : 'Börjar',
        End                : 'Slutar',
        Duration           : 'Längd',
        Complete           : 'Färdig',
        baseline           : 'baslinje',
        'Delayed start by' : 'Försenad start med',
        'Overrun by'       : 'Överskridande med'
    },

    Button : {
        Create           : 'Skapa',
        'Critical paths' : 'Kritiska vägar',
        Edit             : 'Redigera',
        'Export to PDF'  : 'Exportera till PDF',
        Features         : 'Funktioner',
        Settings         : 'Inställningar'
    },

    DateColumn : {
        Deadline : 'Deadline'
    },

    Field : {
        'Find tasks by name' : 'Hitta uppgifter efter namn',
        'Project start'      : 'Projektstart'
    },

    GanttToolbar : {
        'First select the task you want to edit' : 'Välj först den uppgift du vill redigera',
        'New task'                               : 'Ny uppgift'
    },

    Indicators : {
        Indicators     : 'Indikatorer',
        constraintDate : 'Villkor'
    },

    MenuItem : {
        'Draw dependencies'          : 'Rita beroenden',
        'Enable cell editing'        : 'Aktivera cellredigering',
        'Hide schedule'              : 'Dölj schema',
        'Highlight non-working time' : 'Markera icke-arbetstid',
        'Project lines'              : 'Projektlinjer',
        'Show baselines'             : 'Visa baslinjer',
        'Show progress line'         : 'Visa framstegslinje',
        'Show rollups'               : 'Visa samlingar',
        'Task labels'                : 'Uppgiftsetiketter'
    },

    Slider : {
        'Animation duration ' : 'Animationens varaktighet',
        'Bar margin'          : 'Bar marginal',
        'Row height'          : 'Radhöjd'
    },

    StartDateColumn : {
        'Start date' : 'Startdatum'
    },

    StatusColumn : {
        Status : 'Status'
    },

    TaskTooltip : {
        'Scheduling Mode' : 'Läge',
        Calendar          : 'Kalender',
        Critical          : 'Kritisk'
    },

    Tooltip : {
        'Adjust settings'          : 'Justera inställningarna',
        'Collapse all'             : 'Kollapsa alla',
        'Create new task'          : 'Skapa ny uppgift',
        'Edit selected task'       : 'Redigera vald uppgift',
        'Expand all'               : 'Expandera alla',
        'Highlight critical paths' : 'Markera kritiska vägar',
        'Next time span'           : 'Nästa tidsperiod',
        'Previous time span'       : 'Föregående tidsperiod',
        'Toggle features'          : 'Växla funktioner',
        'Zoom in'                  : 'Zooma in',
        'Zoom out'                 : 'Zooma ut',
        'Zoom to fit'              : 'Zooma för att passa'
    }
};

export default LocaleHelper.publishLocale(locale);
