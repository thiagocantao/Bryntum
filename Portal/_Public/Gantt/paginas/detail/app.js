import '../_shared/shared.js'; // not required, our example styling etc.
import Gantt from '../../lib/Gantt/view/Gantt.js';
import '../../lib/Gantt/column/AllColumns.js';
import '../../lib/Scheduler/feature/TimeRanges.js';
import '../../lib/SchedulerPro/feature/DependencyEdit.js';
import '../../lib/Gantt/feature/Baselines.js';
import '../../lib/Grid/feature/Filter.js';
import '../../lib/Gantt/feature/Labels.js';
import '../../lib/Gantt/feature/ParentArea.js';
import '../../lib/Gantt/feature/ProjectLines.js';
import '../../lib/Gantt/feature/ProgressLine.js';
import '../../lib/Gantt/feature/Rollups.js';
import '../../lib/Gantt/feature/TaskEdit.js';
import '../../lib/Grid/feature/CellCopyPaste.js';
import '../../lib/Grid/feature/FillHandle.js';

import './lib/GanttToolbar.js';
import Task from './lib/Task.js';
import './lib/StatusColumn.js';

const gantt = new Gantt({
    appendTo : 'container',

    dependencyIdField : 'wbsCode',

    selectionMode : {
        cell       : true,
        dragSelect : true,
        rowNumber  : true
    },

    project : {
        // Let the Project know we want to use our own Task model with custom fields / methods
        taskModelClass : Task,
        transport      : {
            load : {
                url : '../_datasets/launch-saas.json'
            }
        },
        autoLoad  : true,
        taskStore : {
            wbsMode : 'auto'
        },
        // The State TrackingManager which the UndoRedo widget in the toolbar uses
        stm : {
            autoRecord : true
        },
        // Reset Undo / Redo after each load
        resetUndoRedoQueuesAfterLoad : true
    },

    startDate                     : '2019-01-12',
    endDate                       : '2019-03-24',
    resourceImageFolderPath       : '../_shared/images/users/',
    scrollTaskIntoViewOnCellClick : true,

    columns : [
        { type : 'wbs' },
        { type : 'name', width : 250 },
        { type : 'startdate' },
        { type : 'duration' },
        { type : 'resourceassignment', width : 120, showAvatars : true },
        { type : 'percentdone', showCircle : true, width : 70 },
        {
            type  : 'predecessor',
            width : 112
        },
        {
            type  : 'successor',
            width : 112
        },
        { type : 'schedulingmodecolumn' },
        { type : 'calendar' },
        { type : 'constrainttype' },
        { type : 'constraintdate' },
        { type : 'statuscolumn' },
        { type : 'deadlinedate' },
        { type : 'addnew' }
    ],

    subGridConfigs : {
        locked : {
            flex : 3
        },
        normal : {
            flex : 4
        }
    },

    columnLines : false,

    // Shows a color field in the task editor and a color picker in the task menu.
    // Both lets the user select the Task bar's background color
    showTaskColorPickers : true,

    features : {
        baselines : {
            disabled : true
        },
        dependencyEdit : true,
        filter         : true,
        labels         : {
            left : {
                field  : 'name',
                editor : {
                    type : 'textfield'
                }
            }
        },
        parentArea : {
            disabled : true
        },
        progressLine : {
            disabled   : true,
            statusDate : new Date(2019, 0, 25)
        },
        rollups : {
            disabled : true
        },
        rowReorder : {
            showGrip : true
        },
        timeRanges : {
            showCurrentTimeLine : true
        },
        fillHandle    : true,
        cellCopyPaste : true,
        taskCopyPaste : {
            useNativeClipboard : true
        }
    },

    tbar : {
        type : 'gantttoolbar'
    }
});
