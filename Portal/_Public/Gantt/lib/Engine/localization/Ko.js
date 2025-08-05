import LocaleHelper from '../../Core/localization/LocaleHelper.js';
const locale = {
    localeName: 'Ko',
    localeDesc: '한국어',
    localeCode: 'ko',
    RemoveDependencyCycleEffectResolution: {
        descriptionTpl: '변수 제거'
    },
    DeactivateDependencyCycleEffectResolution: {
        descriptionTpl: '변수 비활성화'
    },
    CycleEffectDescription: {
        descriptionTpl: '다음에 의해 형성된 주기가 발견되었습니다: {0}'
    },
    EmptyCalendarEffectDescription: {
        descriptionTpl: '"{0}" 캘린더는 작업 시간 간격을 제공하지 않습니다.'
    },
    Use24hrsEmptyCalendarEffectResolution: {
        descriptionTpl: '토요일과 일요일이 휴무일 때는 24시간 달력을 사용하십시오.'
    },
    Use8hrsEmptyCalendarEffectResolution: {
        descriptionTpl: '토요일과 일요일이 휴무일 때는 8시간 달력(08:00~12:00, 13:00~17:00)을 사용하십시오.'
    },
    ConflictEffectDescription: {
        descriptionTpl: '스케줄링 충돌이 발견되었습니다: {0}이(가) {1}과(와) 충돌합니다.'
    },
    ConstraintIntervalDescription: {
        dateFormat: 'LLL'
    },
    ProjectConstraintIntervalDescription: {
        startDateDescriptionTpl: '프로젝트 시작 날짜 {0}',
        endDateDescriptionTpl: '프로젝트 종료 날짜 {0}'
    },
    DependencyType: {
        long: [
            '시작에서 시작까지',
            '시작에서 마감까지',
            '마감에서 시작까지',
            '마감에서 마감까지'
        ]
    },
    ManuallyScheduledParentConstraintIntervalDescription: {
        startDescriptionTpl: '수동으로 예약된 "{2}"은(는) 하위 항목이 {0} 이전에 시작되지 않도록 합니다.',
        endDescriptionTpl: '수동으로 예약된 "{2}"은(는) 하위 항목이 {1} 이전에 완료되도록 합니다.'
    },
    DisableManuallyScheduledConflictResolution: {
        descriptionTpl: '"{0}"에 대한 수동 스케줄링 비활성화'
    },
    DependencyConstraintIntervalDescription: {
        descriptionTpl: '"{3}"에서 "{4}"까지의 변수({2})'
    },
    RemoveDependencyResolution: {
        descriptionTpl: '"{1}"에서 "{2}"까지의 변수 제거'
    },
    DeactivateDependencyResolution: {
        descriptionTpl: '"{1}"에서 "{2}"(으)까지의 변수 비활성화'
    },
    DateConstraintIntervalDescription: {
        startDateDescriptionTpl: '작업 "{2}" {3} {0} 제약',
        endDateDescriptionTpl: '작업 "{2}" {3} {1} 제약',
        constraintTypeTpl: {
            startnoearlierthan: '이후에 시작',
            finishnoearlierthan: '이후에 완료',
            muststarton: '에 시작해야 합니다',
            mustfinishon: '에 마감해야 합니다',
            startnolaterthan: '보다 늦지 않게 시작',
            finishnolaterthan: '보다 늦지 않게 완료'
        }
    },
    RemoveDateConstraintConflictResolution: {
        descriptionTpl: '"{0}" 작업의 "{1}" 제약 제거'
    }
};
export default LocaleHelper.publishLocale(locale);
