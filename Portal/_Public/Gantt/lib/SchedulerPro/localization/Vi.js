import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Vi.js';
import '../../Scheduler/localization/Vi.js';

const locale = {

    localeName : 'Vi',
    localeDesc : 'Tiếng Việt',
    localeCode : 'vi',

    ConstraintTypePicker : {
        none                : 'Không có',
        assoonaspossible    : 'Sớm nhất có thể',
        aslateaspossible    : 'Muộn nhất có thể',
        muststarton         : 'Phải bắt đầu từ',
        mustfinishon        : 'Phải kết thúc từ',
        startnoearlierthan  : 'Bắt đầu không sớm hơn',
        startnolaterthan    : 'Bắt đầu không muộn hơn',
        finishnoearlierthan : 'Hoàn thành không sớm hơn',
        finishnolaterthan   : 'Hoàn thành không muộn hơn'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Tiến',
        Backward      : 'Lùi',
        inheritedFrom : 'Thừa hưởng từ',
        enforcedBy    : 'Bắt buộc bởi'
    },

    CalendarField : {
        'Default calendar' : 'Lịch mặc định'
    },

    TaskEditorBase : {
        Information   : 'Thông tin',
        Save          : 'Lưu',
        Cancel        : 'Hủy bỏ',
        Delete        : 'Xóa bỏ',
        calculateMask : 'Đang tính ...',
        saveError     : 'Không thể lưu, trước tiên hãy sửa lỗi',
        repeatingInfo : 'Xem một sự kiện lặp lại',
        editRepeating : 'Chỉnh sửa'
    },

    TaskEdit : {
        'Edit task'            : 'Chỉnh sửa công việc',
        ConfirmDeletionTitle   : 'Xác nhận xóa',
        ConfirmDeletionMessage : 'Bạn có chắc chắn muốn xóa sự kiện không?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Chung',
        Name         : 'Tên',
        Resources    : 'Tài nguyên',
        '% complete' : '% hoàn thành',
        Duration     : 'Thờ hạn',
        Start        : 'Bắt đầu',
        Finish       : 'Kết thúc',
        Effort       : 'Nỗ lực',
        Preamble     : 'Mở đầu',
        Postamble    : 'Hậu kỳ'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Chung',
        Name         : 'Tên',
        '% complete' : '% hoàn tất',
        Duration     : 'Thời hạn',
        Start        : 'Bắt đầu',
        Finish       : 'Kết thúc',
        Effort       : 'Nỗ lực',
        Dates        : 'Ngày'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Nâng cao',
        Calendar                   : 'Lịch',
        'Scheduling mode'          : 'Chế độ lập lịch',
        'Effort driven'            : 'Nỗ lực thúc đẩy',
        'Manually scheduled'       : 'Lên lịch thủ công',
        'Constraint type'          : 'Loại ràng buộc',
        'Constraint date'          : 'Ngày ràng buộc',
        Inactive                   : 'Không hoạt động',
        'Ignore resource calendar' : 'Bỏ qua lịch tài nguyên'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Nâng cao',
        Calendar                   : 'Lịch',
        'Scheduling mode'          : 'Chế độ lập lịch',
        'Effort driven'            : 'Nỗ lực thúc đẩy',
        'Manually scheduled'       : 'Lên lịch thủ công',
        'Constraint type'          : 'Loại ràng buộc',
        'Constraint date'          : 'Ngày ràng buộc',
        Constraint                 : 'Ràng buộc',
        Rollup                     : 'Báo cáo tóm tắt',
        Inactive                   : 'Không hoạt động',
        'Ignore resource calendar' : 'Bỏ qua lịch tài nguyên',
        'Scheduling direction'     : 'Hướng lập lịch'
    },

    DependencyTab : {
        Predecessors      : 'Sự kiện trước',
        Successors        : 'Sự kiện sau',
        ID                : 'ID',
        Name              : 'Tên',
        Type              : 'Loại',
        Lag               : 'Lag',
        cyclicDependency  : 'Phụ thuộc theo chu kỳ',
        invalidDependency : 'Phụ thuộc không hợp lệ'
    },

    NotesTab : {
        Notes : 'Lưu ý'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Tài nguyên',
        Resource  : 'Tài nguyên',
        Units     : 'Đơn vị'
    },

    RecurrenceTab : {
        title : 'Lặp lai'
    },

    SchedulingModePicker : {
        Normal           : 'Thường',
        'Fixed Duration' : 'Thời hạn cố định',
        'Fixed Units'    : 'Đơn vị cố định',
        'Fixed Effort'   : 'Nỗ lực cố định'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} của {available}</span> được phân bổ',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} của {available}</span> được phân bổ',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} của {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} của {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Vào ngày {startDate}<br><span class="{cls}">{allocated} của {available}</span> được phân bổ:<br>{assignments}',
        plusMore              : 'thêm +{value}'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> được phân bổ',
        barTipOnDate          : '<b>{event}</b> vào ngày {startDate}<br><span class="{cls}">{allocated}</span> được phân bổ',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} của {available}</span> được phân bổ:<br>{assignments}',
        groupBarTipOnDate     : 'Ngày {startDate}<br><span class="{cls}">{allocated} của {available}</span> được phân bổ:<br>{assignments}',
        plusMore              : 'thêm +{value}',
        nameColumnText        : 'Tên cột văn bản'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Hủy bỏ thay đổi ',
        schedulingConflict : 'Xung đột lịch trình',
        emptyCalendar      : 'Lỗi cấu hình lịch',
        cycle              : 'Lập kế hoạch chu kỳ',
        Apply              : 'Áp dụng'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Vui lòng chọn một phụ thuộc:',
        invalidDependencyLabel : 'Có những phụ thuộc không hợp lệ liên quan cần được giải quyết:'
    },

    DependencyEdit : {
        Active : 'Hoạt động'
    },

    SchedulerProBase : {
        propagating     : 'Đang tính toán dự',
        storePopulation : 'Đang tải dữ liệu',
        finalizing      : 'Đang hoàn thiện kết quả'
    },

    EventSegments : {
        splitEvent    : 'Chia tách sự kiện',
        renameSegment : 'Đổi tên'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Không được phép bỏ làm tổ',
        nestingNotAllowed   : 'Không được phép làm tổ'
    },

    VersionGrid : {
        compare       : 'So sánh',
        description   : 'Mô tả',
        occurredAt    : 'Xảy ra lúc',
        rename        : 'Đổi tên',
        restore       : 'Khôi phục',
        stopComparing : 'Dừng so sánh'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'nhiệm vụ',
            AssignmentModel : 'phân nhiệm',
            DependencyModel : 'liên kết',
            ProjectModel    : 'dự án',
            ResourceModel   : 'nguồn',
            other           : 'đối tượng'
        },
        entityNamesPlural : {
            TaskModel       : 'những nhiệm vụ',
            AssignmentModel : 'những phân nhiệm',
            DependencyModel : 'những liên kết',
            ProjectModel    : 'những dự án',
            ResourceModel   : 'những nguồn',
            other           : 'những đối tượng'
        },
        transactionDescriptions : {
            update : 'Đã thay đổi {n} {entities}',
            add    : 'Đã thêm {n} {entities}',
            remove : 'Loại bỏ {n} {entities}',
            move   : 'Đã chuyển {n} {entities}',
            mixed  : 'Đã thay đổi {n} {entities}'
        },
        addEntity         : 'Đã thêm {type} **{name}**',
        removeEntity      : 'Đã loại bỏ {type} **{name}**',
        updateEntity      : 'Đã thay đổi {type} **{name}**',
        moveEntity        : 'Đã chuyển {type} **{name}** từ {from} đến {to}',
        addDependency     : 'Đã thêm liên kết từ **{from}** đến **{to}**',
        removeDependency  : 'Đã bỏ liên kết từ **{from}** đến **{to}**',
        updateDependency  : 'Đã sửa liên kết từ **{from}** đến **{to}**',
        addAssignment     : 'Đã phân công **{resource}** đến **{event}**',
        removeAssignment  : 'Đã hủy phân nhiệm **{resource}** từ **{event}**',
        updateAssignment  : 'Đã chỉnh sửa nhiệm vụ **{resource}** từ **{event}**',
        noChanges         : 'Không thay đổi',
        nullValue         : 'không',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Hoàn tác thay đổi',
        redid             : 'Làm lại các thay đổi',
        editedTask        : 'Thuộc tính tác vụ đã chỉnh sửa',
        deletedTask       : 'Đã xóa một nhiệm vụ',
        movedTask         : 'Đã chuyển một nhiệm vụ',
        movedTasks        : 'Đã chuyển nhiều nhiệm vụ'
    }
};

export default LocaleHelper.publishLocale(locale);
