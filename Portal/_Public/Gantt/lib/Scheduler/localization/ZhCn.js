import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/ZhCn.js';

const locale = {

    localeName : 'ZhCn',
    localeDesc : '中文（中国）',
    localeCode : 'zh-CN',

    Object : {
        newEvent : '新事件'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' 事件'
    },

    Dependencies : {
        from    : '从',
        to      : '至',
        valid   : '有效',
        invalid : '无效'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : '开始到开始',
        StartToEnd   : '开始到结束',
        EndToStart   : '结束到开始',
        EndToEnd     : '结束到结束',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            '开始到开始',
            '开始到结束',
            '结束到开始',
            '结束到结束'
        ]
    },

    DependencyEdit : {
        From              : '从',
        To                : '至',
        Type              : '类型',
        Lag               : '延迟',
        'Edit dependency' : '编辑依附关系',
        Save              : '保存',
        Delete            : '删除',
        Cancel            : '取消',
        StartToStart      : '开始到开始',
        StartToEnd        : '开始到结束',
        EndToStart        : '结束到开始',
        EndToEnd          : '结束到结束'
    },

    EventEdit : {
        Name         : '名称',
        Resource     : '资源',
        Start        : '开始',
        End          : '结束',
        Save         : '保存',
        Delete       : '删除',
        Cancel       : '取消',
        'Edit event' : '编辑事件',
        Repeat       : '重复'
    },

    EventDrag : {
        eventOverlapsExisting : '事件与该资源的现有事件重叠',
        noDropOutsideTimeline : '事件不得完全在时间线之外'
    },

    SchedulerBase : {
        'Add event'      : '增添事件',
        'Delete event'   : '删除事件',
        'Unassign event' : '未安排事件',
        color            : '颜色'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : '放大/缩小',
        activeDateRange : '日期范围',
        startText       : '起始日期',
        endText         : '结束日期',
        todayText       : '今天'
    },

    EventCopyPaste : {
        copyEvent  : '复制事件',
        cutEvent   : '剪切事件',
        pasteEvent : '粘贴事件'
    },

    EventFilter : {
        filterEvents : '筛选器任务',
        byName       : '根据名称'
    },

    TimeRanges : {
        showCurrentTimeLine : '显示当前时间线'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : '秒'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM-DD, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM-DD',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : '日'
        },
        day : {
            name : '日/小时'
        },
        week : {
            name : '周/小时'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : '周/日'
        },
        dayAndMonth : {
            name : '月'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : '周'
        },
        weekAndMonth : {
            name : '周'
        },
        weekAndDayLetter : {
            name : '周/工作日'
        },
        weekDateAndMonth : {
            name : '月/周'
        },
        monthAndYear : {
            name : '月'
        },
        year : {
            name : '年'
        },
        manyYears : {
            name : '多年'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : '您在删除事件',
        'delete-all-message'        : '您是否想彻底删除此事件？',
        'delete-further-message'    : '您是想删除本次和未来的所有事件，和还是仅删除选定的一次事件?',
        'delete-further-btn-text'   : '删除所有的未来事件',
        'delete-only-this-btn-text' : '仅删除此事件',
        'update-title'              : '您在变更一个重复性事件',
        'update-all-message'        : '您是否想变更所有的事件？',
        'update-further-message'    : '您是想只本更这一事件，还是本次和未来的所有事件？',
        'update-further-btn-text'   : '所有未来的事件',
        'update-only-this-btn-text' : '仅事件',
        Yes                         : '是',
        Cancel                      : '取消',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' 且',
        Daily                           : '每天',
        'Weekly on {1}'                 : ({ days }) => `每周 ${days}`,
        'Monthly on {1}'                : ({ days }) => `每月 ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `每年 ${days}的 ${months}`,
        'Every {0} days'                : ({ interval }) => `每 ${interval} 天`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `每 ${interval} 周 ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `每 ${interval} 月 ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `每 ${interval} 年 ${days} 的 ${months}`,
        position1                       : '第 1',
        position2                       : '第2',
        position3                       : '第3',
        position4                       : '第4',
        position5                       : '第5',
        'position-1'                    : '最后',
        day                             : '天',
        weekday                         : '工作日',
        'weekend day'                   : '周末',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : '重复性事件',
        Cancel              : '取消',
        Save                : '保存',
        Frequency           : '频次',
        Every               : '每',
        DAILYintervalUnit   : '天',
        WEEKLYintervalUnit  : '周',
        MONTHLYintervalUnit : '月',
        YEARLYintervalUnit  : '年',
        Each                : '每',
        'On the'            : '在',
        'End repeat'        : '最后重复一遍',
        'time(s)'           : '次'
    },

    RecurrenceDaysCombo : {
        day           : '天',
        weekday       : '工作日',
        'weekend day' : '周末'
    },

    RecurrencePositionsCombo : {
        position1    : '第1',
        position2    : '第2',
        position3    : '第3',
        position4    : '第4',
        position5    : '第5',
        'position-1' : '最后'
    },

    RecurrenceStopConditionCombo : {
        Never     : '从不',
        After     : '之后',
        'On date' : '日期'
    },

    RecurrenceFrequencyCombo : {
        None    : '不重复',
        Daily   : '每天',
        Weekly  : '每周',
        Monthly : '每月',
        Yearly  : '每年'
    },

    RecurrenceCombo : {
        None   : '无',
        Custom : '自定义……'
    },

    Summary : {
        'Summary for' : date => `汇总 ${date}`
    },

    ScheduleRangeCombo : {
        completeview : '完成计划',
        currentview  : '可见计划',
        daterange    : '日期范围',
        completedata : '完成计划（所有事件）'
    },

    SchedulerExportDialog : {
        'Schedule range' : '计划范围',
        'Export from'    : '从',
        'Export to'      : '至'
    },

    ExcelExporter : {
        'No resource assigned' : '未分配资源'
    },

    CrudManagerView : {
        serverResponseLabel : '服务器响应：'
    },

    DurationColumn : {
        Duration : '持续时间'
    }
};

export default LocaleHelper.publishLocale(locale);
