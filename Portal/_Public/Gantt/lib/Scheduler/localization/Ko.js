import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Ko.js';

const locale = {

    localeName : 'Ko',
    localeDesc : '한국어',
    localeCode : 'ko',

    Object : {
        newEvent : '새 일정'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + '일정 '
    },

    Dependencies : {
        from    : '부터',
        to      : '까지',
        valid   : '유효함',
        invalid : '유효하지 않음'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : '시작에서 시작까지',
        StartToEnd   : '시작에서 마감까지',
        EndToStart   : '마감에서 시작까지',
        EndToEnd     : '마감에서 마감까지',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            '시작에서 시작까지',
            '시작에서 마감까지',
            '마감에서 시작까지',
            '마감에서 마감까지'
        ]
    },

    DependencyEdit : {
        From              : '부터',
        To                : '까지',
        Type              : '유형',
        Lag               : '지연',
        'Edit dependency' : '의존 관계 편집',
        Save              : '저장',
        Delete            : '삭제',
        Cancel            : '취소',
        StartToStart      : '시작에서 시작까지',
        StartToEnd        : '시작에서 종료까지',
        EndToStart        : '종료에서 시작까지',
        EndToEnd          : '종료에서 종료까지'
    },

    EventEdit : {
        Name         : '이름',
        Resource     : '리소스',
        Start        : '시작',
        End          : '종료',
        Save         : '저장',
        Delete       : '삭제',
        Cancel       : '취소',
        'Edit event' : '일정 편집',
        Repeat       : '반복'
    },

    EventDrag : {
        eventOverlapsExisting : '일정이 이 리소스에 대한 기존 일정과 겹칩니다',
        noDropOutsideTimeline : '일정을 타임라인 외부로 완전히 드롭할 수 없습니다'
    },

    SchedulerBase : {
        'Add event'      : '일정 추가',
        'Delete event'   : '일정 삭제',
        'Unassign event' : '일정 배정 해제',
        color            : '색'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : '확대/축소',
        activeDateRange : '날짜 범위',
        startText       : '시작 날짜',
        endText         : '종료 날짜',
        todayText       : '오늘'
    },

    EventCopyPaste : {
        copyEvent  : '일정 복사하기',
        cutEvent   : '일정 잘라내기',
        pasteEvent : '일정 붙여넣기'
    },

    EventFilter : {
        filterEvents : '작업 필터링',
        byName       : '이름으로'
    },

    TimeRanges : {
        showCurrentTimeLine : '현재 타임라인 표시'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : '초'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM-DD, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM-DD',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : '일'
        },
        day : {
            name : '일/시간'
        },
        week : {
            name : '주/시간'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : '주/일'
        },
        dayAndMonth : {
            name : '달'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : '주'
        },
        weekAndMonth : {
            name : '주'
        },
        weekAndDayLetter : {
            name : '주/주말'
        },
        weekDateAndMonth : {
            name : '달/주'
        },
        monthAndYear : {
            name : '달'
        },
        year : {
            name : '년'
        },
        manyYears : {
            name : '여러 해'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : '일정을 삭제하는 중입니다',
        'delete-all-message'        : '이 일정의 모든 항목을 삭제하시겠습니까?',
        'delete-further-message'    : '이 일정 및 이 일정의 모든 향후 항목 또는, 선택한 항목만 삭제하시겠습니까?',
        'delete-further-btn-text'   : '향후 모든 일정 삭제',
        'delete-only-this-btn-text' : '이 일정만 삭제',
        'update-title'              : '반복되는 일정을 변경하는 중입니다',
        'update-all-message'        : '이 일정의 모든 항목을 변경하시겠습니까?',
        'update-further-message'    : '이 일정 및 이 일정의 모든 향후 항목 또는, 이 항목만 변경하시겠습니까?',
        'update-further-btn-text'   : '향후 모든 일정',
        'update-only-this-btn-text' : '이 일정만',
        Yes                         : '예',
        Cancel                      : '취소',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' 및 ',
        Daily                           : '매일',
        'Weekly on {1}'                 : ({ days }) => `매주 ${days}일마다`,
        'Monthly on {1}'                : ({ days }) => `매달 ${days}일마다`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `매년 ${months}월  ${days}일마다`,
        'Every {0} days'                : ({ interval }) => `매 ${interval} 일마다`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `매 ${interval} 주 간격으로 ${days}일마다`,
        'Every {0} months on {1}'       : ({ interval, days }) => `매 ${interval} 개월 간격으로  ${days}일마다`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `매 ${interval} 년 간격으로 ${months}${days} 월 ${days}일마다`,
        position1                       : '첫 번째',
        position2                       : '두 번째',
        position3                       : '세 번째',
        position4                       : '네 번째',
        position5                       : '다섯 번째',
        'position-1'                    : '마지막',
        day                             : '일',
        weekday                         : '평일',
        'weekend day'                   : '주말',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : '반복 일정',
        Cancel              : '취소',
        Save                : '저장',
        Frequency           : '빈도',
        Every               : '매',
        DAILYintervalUnit   : '일마다',
        WEEKLYintervalUnit  : '주마다',
        MONTHLYintervalUnit : '달마다',
        YEARLYintervalUnit  : '년마다',
        Each                : '각',
        'On the'            : '이하에',
        'End repeat'        : '반복 종료',
        'time(s)'           : '시간'
    },

    RecurrenceDaysCombo : {
        day           : '일',
        weekday       : '평일',
        'weekend day' : '주말'
    },

    RecurrencePositionsCombo : {
        position1    : '첫 번째',
        position2    : '두 번째',
        position3    : '세 번째',
        position4    : '네 번째',
        position5    : '다섯 번째',
        'position-1' : '마지막'
    },

    RecurrenceStopConditionCombo : {
        Never     : '한 번도 해당하지 않음',
        After     : '이후',
        'On date' : '날짜에'
    },

    RecurrenceFrequencyCombo : {
        None    : '반복 없음',
        Daily   : '매일',
        Weekly  : '매주',
        Monthly : '매달',
        Yearly  : '매년'
    },

    RecurrenceCombo : {
        None   : '없음',
        Custom : '사용자 설정...'
    },

    Summary : {
        'Summary for' : date => `${date} :다음의 요약`
    },

    ScheduleRangeCombo : {
        completeview : '스케줄 완료',
        currentview  : '스케줄 표시',
        daterange    : '날짜 범위',
        completedata : '전체 스케줄 (모든 일정)'
    },

    SchedulerExportDialog : {
        'Schedule range' : '스케줄 범위',
        'Export from'    : '부터',
        'Export to'      : '까지'
    },

    ExcelExporter : {
        'No resource assigned' : '할당된 리소스 없음'
    },

    CrudManagerView : {
        serverResponseLabel : '서버 응답:'
    },

    DurationColumn : {
        Duration : '기간'
    }
};

export default LocaleHelper.publishLocale(locale);
