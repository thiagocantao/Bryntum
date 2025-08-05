import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ko.js';
import '../../Scheduler/localization/Ko.js';

const locale = {

    localeName : 'Ko',
    localeDesc : '한국어',
    localeCode : 'ko',

    ConstraintTypePicker : {
        none                : '없음',
        assoonaspossible    : '가능한 한 빨리',
        aslateaspossible    : '가능한 한 늦게',
        muststarton         : '에 시작해야 합니다',
        mustfinishon        : '에 마감해야 합니다',
        startnoearlierthan  : '이후에 시작',
        startnolaterthan    : '보다 늦지 않게 시작',
        finishnoearlierthan : '이후에 완료',
        finishnolaterthan   : '보다 늦지 않게 완료'
    },

    SchedulingDirectionPicker : {
        Forward       : '앞으로',
        Backward      : '뒤로',
        inheritedFrom : '계승됨',
        enforcedBy    : '강제로 시행됨'
    },

    CalendarField : {
        'Default calendar' : '기본 캘린더'
    },

    TaskEditorBase : {
        Information   : '정보',
        Save          : '저장',
        Cancel        : '취소',
        Delete        : '삭제',
        calculateMask : '계산 중...',
        saveError     : '저장할 수 없습니다. 먼저 오류를 수정하십시오',
        repeatingInfo : '반복 이벤트 보기',
        editRepeating : '편집'
    },

    TaskEdit : {
        'Edit task'            : '작업 편집',
        ConfirmDeletionTitle   : '삭제 확인',
        ConfirmDeletionMessage : '일정을 삭제하시겠습니까?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '38em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : '일반',
        Name         : '이름',
        Resources    : '리소스',
        '% complete' : '% 완료',
        Duration     : '기간',
        Start        : '시작',
        Finish       : '마침',
        Effort       : '업무',
        Preamble     : '프리앰블',
        Postamble    : '포스트앰블'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : '일반',
        Name         : '이름',
        '% complete' : '% 완료',
        Duration     : '기간',
        Start        : '시작',
        Finish       : '마감',
        Effort       : '업무',
        Dates        : '날짜'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : '고급',
        Calendar                   : '캘린더',
        'Scheduling mode'          : '스케줄 관리 모드',
        'Effort driven'            : '업무량 고정',
        'Manually scheduled'       : '수동으로 일정 예약',
        'Constraint type'          : '제한 유형',
        'Constraint date'          : '날짜 제한',
        Inactive                   : '비활성화',
        'Ignore resource calendar' : '리소스 일정 무시'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : '고급',
        Calendar                   : '캘린더',
        'Scheduling mode'          : '스케줄 관리 모드',
        'Effort driven'            : '업무량 고정',
        'Manually scheduled'       : '수동으로 일정 예약',
        'Constraint type'          : '제한 유형',
        'Constraint date'          : '날짜 제한',
        Constraint                 : '제약 조건',
        Rollup                     : '롤업',
        Inactive                   : '비활성화',
        'Ignore resource calendar' : '리소스 일정 무시',
        'Scheduling direction'     : '일정 방향'
    },

    DependencyTab : {
        Predecessors      : '선행 활동 ',
        Successors        : '후행 활동',
        ID                : 'ID',
        Name              : '이름',
        Type              : '유형',
        Lag               : '지연',
        cyclicDependency  : '순환 의존 관계',
        invalidDependency : '의존 관계 유효하지 않음'
    },

    NotesTab : {
        Notes : '참고 '
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : '리소스',
        Resource  : '리소스',
        Units     : '단위'
    },

    RecurrenceTab : {
        title : '반복'
    },

    SchedulingModePicker : {
        Normal           : '정상',
        'Fixed Duration' : '고정 기간',
        'Fixed Units'    : '고정 단위',
        'Fixed Effort'   : '고정 업무'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}  {available}</span> 할당',
        barTipOnDate          : '<b>{resource}</b> {startDate}<br><span class="{cls}">{allocated}  {available}</span> 할당',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated}  {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated}  {available}</span> 할당:<br>{assignments}',
        groupBarTipOnDate     : ' {startDate}<br><span class="{cls}">{allocated}  {available}</span> 할당:<br>{assignments}',
        plusMore              : '+{value} 추가'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> 할당',
        barTipOnDate          : '<b>{event}</b>  {startDate}<br><span class="{cls}">{allocated}</span> 할당',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated}  {available}</span> 할당:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}">{allocated}  {available}</span> 할당:<br>{assignments}',
        plusMore              : '+{value} 추가',
        nameColumnText        : '리소스 / 일정'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : '변경 사항을 취소하고 아무 것도 하지 않음',
        schedulingConflict : '스케줄링 충돌',
        emptyCalendar      : '캘린더 구성 오류',
        cycle              : '스케줄링 주기',
        Apply              : '적용'
    },

    CycleResolutionPopup : {
        dependencyLabel        : '의존 관계를 선택하십시오:',
        invalidDependencyLabel : '해결해야 하는 유효하지 않은 관련된 의존 관계가 있습니다:'
    },

    DependencyEdit : {
        Active : '활성화'
    },

    SchedulerProBase : {
        propagating     : '프로젝트 계산 중',
        storePopulation : '데이터 로드 중',
        finalizing      : '결과 마무리 중'
    },

    EventSegments : {
        splitEvent    : '일정 분할',
        renameSegment : '이름 바꾸기'
    },

    NestedEvents : {
        deNestingNotAllowed : '네스팅 해제가 허용되지 않음',
        nestingNotAllowed   : '네스팅이 허용되지 않음'
    },

    VersionGrid : {
        compare       : '비교',
        description   : '설명',
        occurredAt    : '발생일',
        rename        : '이름 바꾸기',
        restore       : '복원',
        stopComparing : '비교 중지'
    },

    Versions : {
        entityNames : {
            TaskModel       : '할 일',
            AssignmentModel : '할당',
            DependencyModel : '링크',
            ProjectModel    : '프로젝트',
            ResourceModel   : '리소스',
            other           : '대상'
        },
        entityNamesPlural : {
            TaskModel       : '할 일',
            AssignmentModel : '할당',
            DependencyModel : '링크',
            ProjectModel    : '프로젝트',
            ResourceModel   : '리소스',
            other           : '대상'
        },
        transactionDescriptions : {
            update : '{n} {entities} 변경됨',
            add    : '{n} {entities} 추가됨',
            remove : '{n} {entities} 삭제됨',
            move   : '{n} {entities} 이동함',
            mixed  : '{n} {entities} 변경됨'
        },
        addEntity         : '추가된 {유형} **{이름}**',
        removeEntity      : '삭제된 {유형} **{이름}**',
        updateEntity      : '변경된 {유형} **{이름}**',
        moveEntity        : '이동한 {유형} **{이름}** {에서}에서 {으로}으로',
        addDependency     : '**{에서}**에서 **{으로}**으로 추가된 링크',
        removeDependency  : '**{에서}**에서 **{으로}**으로 삭제된 링크',
        updateDependency  : '**{에서}**에서 **{으로}**으로 편집된 링크',
        addAssignment     : '**{리소스}**를 **{일정}**에 할당함',
        removeAssignment  : '**{일정}**에서 **{리소스}**로 삭제된 할당',
        updateAssignment  : '**{리소스}**에서 **{일정}**로 편집된 할당',
        noChanges         : '변경 사항 없음',
        nullValue         : '없음',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : '변경 취소',
        redid             : '다시한 변경 사항',
        editedTask        : '편집된 작업 속성',
        deletedTask       : '삭제된 작업',
        movedTask         : '이동된 할 일',
        movedTasks        : '이동된 할 일'
    }
};

export default LocaleHelper.publishLocale(locale);
