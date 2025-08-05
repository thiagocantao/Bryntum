import LocaleHelper from '../../../lib/Core/localization/LocaleHelper.js';
import '../../../lib/Gantt/localization/En.js';
import './shared.locale.En.js';

const locale = {

    localeName : 'En',
    localeDesc : 'English (US)',
    localeCode : 'en-US',

    Baselines : {
        baseline           : 'baseline',
        Complete           : 'Complete',
        'Delayed start by' : 'Delayed start by',
        Duration           : 'Duration',
        End                : 'End',
        'Overrun by'       : 'Overrun by',
        Start              : 'Start'
    },

    Button : {
        Create           : 'Create',
        'Critical paths' : 'Critical paths',
        Edit             : 'Edit',
        'Export to PDF'  : 'Export to PDF',
        Features         : 'Features',
        Settings         : 'Settings'
    },

    DateColumn : {
        Deadline : 'Deadline'
    },

    Field : {
        'Find tasks by name' : 'Find tasks by name',
        'Project start'      : 'Project start'
    },

    GanttToolbar : {
        'First select the task you want to edit' : 'First select the task you want to edit',
        'New task'                               : 'New task'
    },

    Indicators : {
        Indicators     : 'Indicators',
        lateDates      : 'Late start/end',
        constraintDate : 'Constraint'
    },

    MenuItem : {
        'Draw dependencies'          : 'Draw dependencies',
        'Enable cell editing'        : 'Enable cell editing',
        'Hide schedule'              : 'Hide schedule',
        'Highlight non-working time' : 'Highlight non-working time',
        'Project lines'              : 'Project lines',
        'Show baselines'             : 'Show baselines',
        'Show progress line'         : 'Show progress line',
        'Show rollups'               : 'Show rollups',
        'Task labels'                : 'Task labels'
    },

    Slider : {
        'Animation duration ' : 'Animation duration',
        'Bar margin'          : 'Bar margin',
        'Row height'          : 'Row height'
    },

    StartDateColumn : {
        'Start date' : 'Start date'
    },

    StatusColumn : {
        Status : 'Status'
    },

    TaskTooltip : {
        'Scheduling Mode' : 'Scheduling Mode',
        Calendar          : 'Calendar',
        Critical          : 'Critical'
    },

    Tooltip : {
        'Adjust settings'          : 'Adjust settings',
        'Collapse all'             : 'Collapse all',
        'Create new task'          : 'Create new task',
        'Edit selected task'       : 'Edit selected task',
        'Expand all'               : 'Expand all',
        'Highlight critical paths' : 'Highlight critical paths',
        'Next time span'           : 'Next time span',
        'Previous time span'       : 'Previous time span',
        'Toggle features'          : 'Toggle features',
        'Zoom in'                  : 'Zoom in',
        'Zoom out'                 : 'Zoom out',
        'Zoom to fit'              : 'Zoom to fit'
    }
};

export default LocaleHelper.publishLocale(locale);
