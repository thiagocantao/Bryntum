import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Vi.js';

const locale = {

    localeName : 'Vi',
    localeDesc : 'Tiếng Việt',
    localeCode : 'vi',

    Object : {
        newEvent : 'Sự kiện mới'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' kiện'
    },

    Dependencies : {
        from    : 'Từ',
        to      : 'Đến',
        valid   : 'Hợp lệ',
        invalid : 'Không hợp lệ'
    },

    DependencyType : {
        SS           : 'BB',
        SF           : 'BK',
        FS           : 'KB',
        FF           : 'KK',
        StartToStart : 'Bắt đầu đến Bắt đầu',
        StartToEnd   : 'Bắt đầu đến Kết thúc',
        EndToStart   : 'Kết thúc đến Bắt đầu',
        EndToEnd     : 'Kết thúc đến Kết thúc',
        short        : [
            'BB',
            'BK',
            'KB',
            'KK'
        ],
        long : [
            'Bắt đầu đến Bắt đầu',
            'Bắt đầu đến Kết thúc',
            'Kết thúc đến Bắt đầu',
            'Kết thúc đến Kết thúc'
        ]
    },

    DependencyEdit : {
        From              : 'Từ',
        To                : 'Đến',
        Type              : 'Loại',
        Lag               : 'Lag',
        'Edit dependency' : 'Chỉnh sửa Phụ thuộc',
        Save              : 'Lưu',
        Delete            : 'Xóa',
        Cancel            : 'Hủy bỏ',
        StartToStart      : 'Bắt đầu đến Bắt đầu',
        StartToEnd        : 'Bắt đầu đến Kết thúc',
        EndToStart        : 'Kết thúc đến Bắt đầu',
        EndToEnd          : 'Kết thúc đến Kết thúc'
    },

    EventEdit : {
        Name         : 'Tên',
        Resource     : 'Tài nguyên',
        Start        : 'Bắt đầu',
        End          : 'Kết thúc',
        Save         : 'Lưu',
        Delete       : 'Xóa',
        Cancel       : 'Hủy bỏ',
        'Edit event' : 'Chỉnh sửa sự kiện',
        Repeat       : 'Lặp lại'
    },

    EventDrag : {
        eventOverlapsExisting : 'Sự kiện chồng chéo sự kiện hiện có cho tài nguyên này',
        noDropOutsideTimeline : 'Sự kiện không thể bị bỏ hoàn toàn ngoài dòng thời gian'
    },

    SchedulerBase : {
        'Add event'      : 'Thêm sự kiện',
        'Delete event'   : 'Xóa sự kiện',
        'Unassign event' : 'Bỏ chỉ định sự kiện',
        color            : 'Màu sắc'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Thu phóng',
        activeDateRange : 'Phạm vi ngày',
        startText       : 'Ngày bắt đầu',
        endText         : 'Ngày kết thúc',
        todayText       : 'Hôm nay'
    },

    EventCopyPaste : {
        copyEvent  : 'Sao chép sự kiện',
        cutEvent   : 'Cắt sự kiện',
        pasteEvent : 'Dán sự kiện'
    },

    EventFilter : {
        filterEvents : 'Lọc nhiệm vụ',
        byName       : 'Theo tên'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Hiển thị dòng thời gian hiện tại'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'giây'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Ngày'
        },
        day : {
            name : 'Ngày/giờ'
        },
        week : {
            name : 'Tuần/giờ'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Tuần/ngày'
        },
        dayAndMonth : {
            name : 'Tháng'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Tuần'
        },
        weekAndMonth : {
            name : 'Tuần'
        },
        weekAndDayLetter : {
            name : 'Tuần/ngày trong tuần'
        },
        weekDateAndMonth : {
            name : 'Tháng/tuần'
        },
        monthAndYear : {
            name : 'Tháng'
        },
        year : {
            name : 'Năm'
        },
        manyYears : {
            name : 'Năm'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Bạn đang xóa một sự kiện',
        'delete-all-message'        : 'Bạn có muốn xóa tất cả các lần xuất hiện của sự kiện này không?',
        'delete-further-message'    : 'Bạn có muốn xóa sự kiện này và tất cả các lần xuất hiện trong tương lai của sự kiện này hay chỉ sự kiện đã chọn?',
        'delete-further-btn-text'   : 'Xóa tất cả các sự kiện trong tương la',
        'delete-only-this-btn-text' : 'Chỉ xóa sự kiện này',
        'update-title'              : 'Bạn đang thay đổi một sự kiện lặp lại',
        'update-all-message'        : 'Bạn có muốn thay đổi tất cả các lần xuất hiện của sự kiện này không?',
        'update-further-message'    : 'Bạn chỉ muốn thay đổi lần xuất hiện này của sự kiện hay lần này và tất cả các lần xuất hiện trong tương lai?',
        'update-further-btn-text'   : 'Tất cả các sự kiện trong tương lai',
        'update-only-this-btn-text' : 'Chỉ Sự kiện này',
        Yes                         : 'Có',
        Cancel                      : 'Hủy bỏ',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' và ',
        Daily                           : 'Hằng ngày',
        'Weekly on {1}'                 : ({ days }) => `Hằng tuần vào ngày ${days}`,
        'Monthly on {1}'                : ({ days }) => `Hằng tháng vào tháng ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Hằng nằm vào năm ${days} của ${months}`,
        'Every {0} days'                : ({ interval }) => `Mỗi ${interval} ngày`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Mỗi ${interval} tuần vào ngày ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Mỗi ${interval} tháng vào ngày ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Mỗi ${interval} năm vào ngày ${days} của tháng ${months}`,
        position1                       : 'thứ nhất',
        position2                       : 'thứ hai',
        position3                       : 'thứ ba',
        position4                       : 'thứ tư',
        position5                       : 'thứ năm',
        'position-1'                    : 'cuối cùng',
        day                             : 'ngày',
        weekday                         : 'ngày trong tuần',
        'weekend day'                   : 'ngày cuối tuần',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Lặp lại sự kiện',
        Cancel              : 'Hủy bỏ',
        Save                : 'Lưu',
        Frequency           : 'Tần suất',
        Every               : 'Mỗi',
        DAILYintervalUnit   : 'ngày',
        WEEKLYintervalUnit  : 'tuần',
        MONTHLYintervalUnit : 'tháng',
        YEARLYintervalUnit  : 'năm',
        Each                : 'Mỗi',
        'On the'            : 'Vào',
        'End repeat'        : 'Kết thúc lặp lại',
        'time(s)'           : 'lần'
    },

    RecurrenceDaysCombo : {
        day           : 'ngày',
        weekday       : 'ngày trong tuần',
        'weekend day' : 'ngày cuối tuần'
    },

    RecurrencePositionsCombo : {
        position1    : 'thứ nhất',
        position2    : 'thứ hai',
        position3    : 'thứ ba',
        position4    : 'thứ tư',
        position5    : 'thứ năm',
        'position-1' : 'cuối cùng'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Không bao giờ',
        After     : 'Sau',
        'On date' : 'Vào ngày'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Không lặp lại',
        Daily   : 'Hằng ngày',
        Weekly  : 'Hằng tuần',
        Monthly : 'Hằng tháng',
        Yearly  : 'Hằng năm'
    },

    RecurrenceCombo : {
        None   : 'Không',
        Custom : 'Tùy chỉnh...'
    },

    Summary : {
        'Summary for' : date => `Tóm tắt cho ngày ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Hoàn thành lịch trình',
        currentview  : 'Lịch trình hiển thị',
        daterange    : 'Phạm vi ngày',
        completedata : 'Toàn bộ lịch trình (cho tất cả các sự kiện)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Phạm vi lịch trình',
        'Export from'    : 'Xuất từ',
        'Export to'      : 'Xuất đến'
    },

    ExcelExporter : {
        'No resource assigned' : 'Không có tài nguyên nào được chỉ định'
    },

    CrudManagerView : {
        serverResponseLabel : 'Phản hồi của máy chủ:'
    },

    DurationColumn : {
        Duration : 'Thời hạn'
    }
};

export default LocaleHelper.publishLocale(locale);
