import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/En.js';

const locale = {

    localeName : 'En',
    localeDesc : 'English (US)',
    localeCode : 'en-US',

    Object : {
        newEvent : 'New event'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' event' + (data !== 1 ? 's' : '')
    },

    Dependencies : {
        from    : 'From',
        to      : 'To',
        valid   : 'Valid',
        invalid : 'Invalid'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'Start-to-Start',
        StartToEnd   : 'Start-to-Finish',
        EndToStart   : 'Finish-to-Start',
        EndToEnd     : 'Finish-to-Finish',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'Start-to-Start',
            'Start-to-Finish',
            'Finish-to-Start',
            'Finish-to-Finish'
        ]
    },

    DependencyEdit : {
        From              : 'From',
        To                : 'To',
        Type              : 'Type',
        Lag               : 'Lag',
        'Edit dependency' : 'Edit dependency',
        Save              : 'Save',
        Delete            : 'Delete',
        Cancel            : 'Cancel',
        StartToStart      : 'Start to Start',
        StartToEnd        : 'Start to End',
        EndToStart        : 'End to Start',
        EndToEnd          : 'End to End'
    },

    EventEdit : {
        Name         : 'Name',
        Resource     : 'Resource',
        Start        : 'Start',
        End          : 'End',
        Save         : 'Save',
        Delete       : 'Delete',
        Cancel       : 'Cancel',
        'Edit event' : 'Edit event',
        Repeat       : 'Repeat'
    },

    EventDrag : {
        eventOverlapsExisting : 'Event overlaps existing event for this resource',
        noDropOutsideTimeline : 'Event may not be dropped completely outside the timeline'
    },

    SchedulerBase : {
        'Add event'      : 'Add event',
        'Delete event'   : 'Delete event',
        'Unassign event' : 'Unassign event',
        color            : 'Color'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Date range',
        startText       : 'Start date',
        endText         : 'End date',
        todayText       : 'Today'
    },

    EventCopyPaste : {
        copyEvent  : 'Copy event',
        cutEvent   : 'Cut event',
        pasteEvent : 'Paste event'
    },

    EventFilter : {
        filterEvents : 'Filter tasks',
        byName       : 'By name'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Show current timeline'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Seconds'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM/DD, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM/DD',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Day'
        },
        day : {
            name : 'Day/hours'
        },
        week : {
            name : 'Week/hours'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Week/days'
        },
        dayAndMonth : {
            name : 'Month'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Week'
        },
        weekAndMonth : {
            name : 'Weeks'
        },
        weekAndDayLetter : {
            name : 'Weeks/weekdays'
        },
        weekDateAndMonth : {
            name : 'Months/weeks'
        },
        monthAndYear : {
            name : 'Months'
        },
        year : {
            name : 'Years'
        },
        manyYears : {
            name : 'Multiple years'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'You are deleting an event',
        'delete-all-message'        : 'Do you want to delete all occurrences of this event?',
        'delete-further-message'    : 'Do you want to delete this and all future occurrences of this event, or only the selected occurrence?',
        'delete-further-btn-text'   : 'Delete All Future Events',
        'delete-only-this-btn-text' : 'Delete Only This Event',
        'update-title'              : 'You are changing a repeating event',
        'update-all-message'        : 'Do you want to change all occurrences of this event?',
        'update-further-message'    : 'Do you want to change only this occurrence of the event, or this and all future occurrences?',
        'update-further-btn-text'   : 'All Future Events',
        'update-only-this-btn-text' : 'Only This Event',
        Yes                         : 'Yes',
        Cancel                      : 'Cancel',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' and ',
        Daily                           : 'Daily',
        'Weekly on {1}'                 : ({ days }) => `Weekly on ${days}`,
        'Monthly on {1}'                : ({ days }) => `Monthly on ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Yearly on ${days} of ${months}`,
        'Every {0} days'                : ({ interval }) => `Every ${interval} days`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Every ${interval} weeks on ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Every ${interval} months on ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Every ${interval} years on ${days} of ${months}`,
        position1                       : 'the first',
        position2                       : 'the second',
        position3                       : 'the third',
        position4                       : 'the fourth',
        position5                       : 'the fifth',
        'position-1'                    : 'the last',
        day                             : 'day',
        weekday                         : 'weekday',
        'weekend day'                   : 'weekend day',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Repeat event',
        Cancel              : 'Cancel',
        Save                : 'Save',
        Frequency           : 'Frequency',
        Every               : 'Every',
        DAILYintervalUnit   : 'day(s)',
        WEEKLYintervalUnit  : 'week(s)',
        MONTHLYintervalUnit : 'month(s)',
        YEARLYintervalUnit  : 'year(s)',
        Each                : 'Each',
        'On the'            : 'On the',
        'End repeat'        : 'End repeat',
        'time(s)'           : 'time(s)'
    },

    RecurrenceDaysCombo : {
        day           : 'day',
        weekday       : 'weekday',
        'weekend day' : 'weekend day'
    },

    RecurrencePositionsCombo : {
        position1    : 'first',
        position2    : 'second',
        position3    : 'third',
        position4    : 'fourth',
        position5    : 'fifth',
        'position-1' : 'last'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Never',
        After     : 'After',
        'On date' : 'On date'
    },

    RecurrenceFrequencyCombo : {
        None    : 'No repeat',
        Daily   : 'Daily',
        Weekly  : 'Weekly',
        Monthly : 'Monthly',
        Yearly  : 'Yearly'
    },

    RecurrenceCombo : {
        None   : 'None',
        Custom : 'Custom...'
    },

    Summary : {
        'Summary for' : date => `Summary for ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Complete schedule',
        currentview  : 'Visible schedule',
        daterange    : 'Date range',
        completedata : 'Complete schedule (for all events)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Schedule range',
        'Export from'    : 'From',
        'Export to'      : 'To'
    },

    ExcelExporter : {
        'No resource assigned' : 'No resource assigned'
    },

    CrudManagerView : {
        serverResponseLabel : 'Server response:'
    },

    DurationColumn : {
        Duration : 'Duration'
    }
};

export default LocaleHelper.publishLocale(locale);
