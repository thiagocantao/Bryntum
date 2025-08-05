import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/En.js';
import '../../Scheduler/localization/En.js';

const locale = {

    localeName : 'En',
    localeDesc : 'English (US)',
    localeCode : 'en-US',

    ConstraintTypePicker : {
        none                : 'None',
        assoonaspossible    : 'As soon as possible',
        aslateaspossible    : 'As late as possible',
        muststarton         : 'Must start on',
        mustfinishon        : 'Must finish on',
        startnoearlierthan  : 'Start no earlier than',
        startnolaterthan    : 'Start no later than',
        finishnoearlierthan : 'Finish no earlier than',
        finishnolaterthan   : 'Finish no later than'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Forward',
        Backward      : 'Backward',
        inheritedFrom : 'Inherited from',
        enforcedBy    : 'Enforced by'
    },

    CalendarField : {
        'Default calendar' : 'Default calendar'
    },

    TaskEditorBase : {
        Information   : 'Information',
        Save          : 'Save',
        Cancel        : 'Cancel',
        Delete        : 'Delete',
        calculateMask : 'Calculating...',
        saveError     : "Can't save, please correct errors first",
        repeatingInfo : 'Viewing a repeating event',
        editRepeating : 'Edit'
    },

    TaskEdit : {
        'Edit task'            : 'Edit task',
        ConfirmDeletionTitle   : 'Confirm deletion',
        ConfirmDeletionMessage : 'Are you sure you want to delete the event?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '34em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'General',
        Name         : 'Name',
        Resources    : 'Resources',
        '% complete' : '% complete',
        Duration     : 'Duration',
        Start        : 'Start',
        Finish       : 'Finish',
        Effort       : 'Effort',
        Preamble     : 'Preamble',
        Postamble    : 'Postamble'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'General',
        Name         : 'Name',
        '% complete' : '% complete',
        Duration     : 'Duration',
        Start        : 'Start',
        Finish       : 'Finish',
        Effort       : 'Effort',
        Dates        : 'Dates'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Advanced',
        Calendar                   : 'Calendar',
        'Scheduling mode'          : 'Scheduling mode',
        'Effort driven'            : 'Effort driven',
        'Manually scheduled'       : 'Manually scheduled',
        'Constraint type'          : 'Constraint type',
        'Constraint date'          : 'Constraint date',
        Inactive                   : 'Inactive',
        'Ignore resource calendar' : 'Ignore resource calendar'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Advanced',
        Calendar                   : 'Calendar',
        'Scheduling mode'          : 'Scheduling mode',
        'Effort driven'            : 'Effort driven',
        'Manually scheduled'       : 'Manually scheduled',
        'Constraint type'          : 'Constraint type',
        'Constraint date'          : 'Constraint date',
        Constraint                 : 'Constraint',
        Rollup                     : 'Rollup',
        Inactive                   : 'Inactive',
        'Ignore resource calendar' : 'Ignore resource calendar',
        'Scheduling direction'     : 'Scheduling direction'
    },

    DependencyTab : {
        Predecessors      : 'Predecessors',
        Successors        : 'Successors',
        ID                : 'ID',
        Name              : 'Name',
        Type              : 'Type',
        Lag               : 'Lag',
        cyclicDependency  : 'Cyclic dependency',
        invalidDependency : 'Invalid dependency'
    },

    NotesTab : {
        Notes : 'Notes'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Resources',
        Resource  : 'Resource',
        Units     : 'Units'
    },

    RecurrenceTab : {
        title : 'Repeat'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Fixed Duration',
        'Fixed Units'    : 'Fixed Units',
        'Fixed Effort'   : 'Fixed Effort'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} of {available}</span> allocated',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} of {available}</span> allocated',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} of {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} of {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'On {startDate}<br><span class="{cls}">{allocated} of {available}</span> allocated:<br>{assignments}',
        plusMore              : '+{value} more'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> allocated',
        barTipOnDate          : '<b>{event}</b> on {startDate}<br><span class="{cls}">{allocated}</span> allocated',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} of {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'On {startDate}<br><span class="{cls}">{allocated} of {available}</span> allocated:<br>{assignments}',
        plusMore              : '+{value} more',
        nameColumnText        : 'Resource / Event'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Cancel the change and do nothing',
        schedulingConflict : 'Scheduling conflict',
        emptyCalendar      : 'Calendar configuration error',
        cycle              : 'Scheduling cycle',
        Apply              : 'Apply'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Please select a dependency:',
        invalidDependencyLabel : 'There are invalid dependencies involved that need to be addressed:'
    },

    DependencyEdit : {
        Active : 'Active'
    },

    SchedulerProBase : {
        propagating     : 'Calculating project',
        storePopulation : 'Loading data',
        finalizing      : 'Finalizing results'
    },

    EventSegments : {
        splitEvent    : 'Split event',
        renameSegment : 'Rename'
    },

    NestedEvents : {
        deNestingNotAllowed : 'De-nesting not allowed',
        nestingNotAllowed   : 'Nesting not allowed'
    },

    VersionGrid : {
        compare       : 'Compare',
        description   : 'Description',
        occurredAt    : 'Occurred At',
        rename        : 'Rename',
        restore       : 'Restore',
        stopComparing : 'Stop Comparing'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'task',
            AssignmentModel : 'assignment',
            DependencyModel : 'link',
            ProjectModel    : 'project',
            ResourceModel   : 'resource',
            other           : 'object'
        },
        entityNamesPlural : {
            TaskModel       : 'tasks',
            AssignmentModel : 'assignments',
            DependencyModel : 'links',
            ProjectModel    : 'projects',
            ResourceModel   : 'resources',
            other           : 'objects'
        },
        transactionDescriptions : {
            update : 'Changed {n} {entities}',
            add    : 'Added {n} {entities}',
            remove : 'Removed {n} {entities}',
            move   : 'Moved {n} {entities}',
            mixed  : 'Changed {n} {entities}'
        },
        addEntity         : 'Added {type} **{name}**',
        removeEntity      : 'Removed {type} **{name}**',
        updateEntity      : 'Changed {type} **{name}**',
        moveEntity        : 'Moved {type} **{name}** from {from} to {to}',
        addDependency     : 'Added link from **{from}** to **{to}**',
        removeDependency  : 'Removed link from **{from}** to **{to}**',
        updateDependency  : 'Edited link from **{from}** to **{to}**',
        addAssignment     : 'Assigned **{resource}** to **{event}**',
        removeAssignment  : 'Removed assignment of **{resource}** from **{event}**',
        updateAssignment  : 'Edited assignment of **{resource}** to **{event}**',
        noChanges         : 'No changes',
        nullValue         : 'none',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Undid changes',
        redid             : 'Redid changes',
        editedTask        : 'Edited task properties',
        deletedTask       : 'Deleted a task',
        movedTask         : 'Moved a task',
        movedTasks        : 'Moved tasks'
    }
};

export default LocaleHelper.publishLocale(locale);
