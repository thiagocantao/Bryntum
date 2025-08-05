import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/ZhCn.js';
import '../../Scheduler/localization/ZhCn.js';

const locale = {

    localeName : 'ZhCn',
    localeDesc : '中文（中国）',
    localeCode : 'zh-CN',

    ConstraintTypePicker : {
        none                : '无',
        assoonaspossible    : '尽快',
        aslateaspossible    : '尽晚',
        muststarton         : '必须于（日期）开始',
        mustfinishon        : '必须于（日期）结束',
        startnoearlierthan  : '不早于（日期）开始',
        startnolaterthan    : '不迟于（日期）开始',
        finishnoearlierthan : '不早于（日期）结束',
        finishnolaterthan   : '不迟于（日期）结束'
    },

    SchedulingDirectionPicker : {
        Forward       : '前进',
        Backward      : '后退',
        inheritedFrom : '继承自',
        enforcedBy    : '强制执行者'
    },

    CalendarField : {
        'Default calendar' : '默认日历'
    },

    TaskEditorBase : {
        Information   : '信息',
        Save          : '保存',
        Cancel        : '取消',
        Delete        : '删除',
        calculateMask : '计算……',
        saveError     : '无法保存，请先更正错误',
        repeatingInfo : '查看重复事件',
        editRepeating : '编辑'
    },

    TaskEdit : {
        'Edit task'            : '编辑任务',
        ConfirmDeletionTitle   : '确认删除',
        ConfirmDeletionMessage : '您确定要删除该事件？'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : '通用',
        Name         : '名称',
        Resources    : '资源',
        '% complete' : '完成%',
        Duration     : '持续时间',
        Start        : '开始',
        Finish       : '完成',
        Effort       : 'Effort',
        Preamble     : '前文',
        Postamble    : '后文'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : '通用',
        Name         : '名称',
        '% complete' : '完成%',
        Duration     : '持续时间',
        Start        : '开始',
        Finish       : '完成',
        Effort       : 'Effort',
        Dates        : '日期'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : '高级',
        Calendar                   : '日历',
        'Scheduling mode'          : '日程模式',
        'Effort driven'            : '工作量驱动',
        'Manually scheduled'       : '手动排程',
        'Constraint type'          : '限制类型',
        'Constraint date'          : '限制日期',
        Inactive                   : '无效',
        'Ignore resource calendar' : '忽略资源日历'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : '高级',
        Calendar                   : '日历',
        'Scheduling mode'          : '日程模式',
        'Effort driven'            : '工作量驱动',
        'Manually scheduled'       : '手动排程',
        'Constraint type'          : '限制类型',
        'Constraint date'          : '限制日期',
        Constraint                 : '限制',
        Rollup                     : '打包',
        Inactive                   : '无效',
        'Ignore resource calendar' : '忽略资源日历',
        'Scheduling direction'     : '计划方向'
    },

    DependencyTab : {
        Predecessors      : '前导',
        Successors        : '后继',
        ID                : 'ID',
        Name              : '名称',
        Type              : '类型',
        Lag               : '延迟',
        cyclicDependency  : '循环依附关系',
        invalidDependency : '依附关系无效'
    },

    NotesTab : {
        Notes : '注释'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : '资源',
        Resource  : '资源',
        Units     : '单位'
    },

    RecurrenceTab : {
        title : '重复'
    },

    SchedulingModePicker : {
        Normal           : '正常',
        'Fixed Duration' : '固定时长',
        'Fixed Units'    : '固定单位',
        'Fixed Effort'   : '固定工作量'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} 的 {available}</span> 已分配',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} 的 {available}</span> 已分配',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} 的 {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} 的 {available}</span> 分配的:<br>{assignments}',
        groupBarTipOnDate     : '在 {startDate}<br><span class="{cls}">{allocated} 的 {available}</span> 已分配：<br>{assignments}',
        plusMore              : '+{value} 更多'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> 已分配',
        barTipOnDate          : '<b>{event}</b> 在 {startDate}<br><span class="{cls}">{allocated}</span> 已分配',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} 的 {available}</span> 已分配：<br>{assignments}',
        groupBarTipOnDate     : '在 {startDate}<br><span class="{cls}">{allocated} 的 {available}</span> 已分配：<br>{assignments}',
        plusMore              : '+{value} 更多',
        nameColumnText        : '资源/事件'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : '取消变更，不做任何改变',
        schedulingConflict : '计划冲突',
        emptyCalendar      : '日历配置错误',
        cycle              : '排程周期',
        Apply              : '应用'
    },

    CycleResolutionPopup : {
        dependencyLabel        : '请选择依附关系：',
        invalidDependencyLabel : '存在无效依附关系，需要解决：'
    },

    DependencyEdit : {
        Active : '激活'
    },

    SchedulerProBase : {
        propagating     : '计算项目',
        storePopulation : '加载数据',
        finalizing      : '最终确定结果'
    },

    EventSegments : {
        splitEvent    : '拆分事件',
        renameSegment : '重命名'
    },

    NestedEvents : {
        deNestingNotAllowed : '不允许去嵌套',
        nestingNotAllowed   : '不允许嵌套'
    },

    VersionGrid : {
        compare       : '比较',
        description   : '描述',
        occurredAt    : '发生于',
        rename        : '重命名',
        restore       : '恢复',
        stopComparing : '停止比较'
    },

    Versions : {
        entityNames : {
            TaskModel       : '任务',
            AssignmentModel : '分配',
            DependencyModel : '关联',
            ProjectModel    : '项目',
            ResourceModel   : '资源',
            other           : '对象'
        },
        entityNamesPlural : {
            TaskModel       : '任务',
            AssignmentModel : '分配',
            DependencyModel : '关联',
            ProjectModel    : '项目',
            ResourceModel   : '资源',
            other           : '对象'
        },
        transactionDescriptions : {
            update : '更改{n} {entities}',
            add    : '添加{n} {entities}',
            remove : '删除 {n} {entities}',
            move   : '移动 {n} {entities}',
            mixed  : '更改{n} {entities}'
        },
        addEntity         : '添加{type} **{name}**',
        removeEntity      : '删除{type} **{name}**',
        updateEntity      : '更改{type} **{name}**',
        moveEntity        : '移动 {type} **{name}** 从{from}到{to}',
        addDependency     : '添加从**{from}**到**{to}**的关联',
        removeDependency  : '移除从**{from}**到**{to}**的关联',
        updateDependency  : '编辑从**{from}**到**{to}**的关联',
        addAssignment     : '分配**{resource}**到**{event}**',
        removeAssignment  : '移除**{resource}**到**{event}**的分配',
        updateAssignment  : '编辑**{resource}**到**{event}**的分配',
        noChanges         : '无更改',
        nullValue         : '空',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : '撤销更改',
        redid             : '恢复更改',
        editedTask        : '编辑任务属性',
        deletedTask       : '删除任务',
        movedTask         : '移动一项任务',
        movedTasks        : '移动多项任务'
    }
};

export default LocaleHelper.publishLocale(locale);
