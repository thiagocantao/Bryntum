import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Ko.js';

const emptyString = new String();

const locale = {

    localeName : 'Ko',
    localeDesc : '한국어',
    localeCode : 'ko',

    ColumnPicker : {
        column          : '열',
        columnsMenu     : '열',
        hideColumn      : '열 숨기기',
        hideColumnShort : '숨기기',
        newColumns      : '신규 열'
    },

    Filter : {
        applyFilter   : '필터 적용',
        filter        : '필터',
        editFilter    : '필터 편집',
        on            : '켜기',
        before        : '이전',
        after         : '이후',
        equals        : '같음',
        lessThan      : '이하',
        moreThan      : '이상',
        removeFilter  : '필터 제거',
        disableFilter : '필터 비활성화'
    },

    FilterBar : {
        enableFilterBar  : '필터 막대 표시',
        disableFilterBar : '필터 막대 숨기기'
    },

    Group : {
        group                : '그룹',
        groupAscending       : '그룹 오름차순',
        groupDescending      : '그룹 내림차순',
        groupAscendingShort  : '오름차순',
        groupDescendingShort : '내림차순',
        stopGrouping         : '그룹화 중지',
        stopGroupingShort    : '중지'
    },

    HeaderMenu : {
        moveBefore     : text => ` "${text}" 이전으로 이동`,
        moveAfter      : text => ` "${text} 이후로 이동 "`,
        collapseColumn : '열 접기',
        expandColumn   : '열 펼치기'
    },

    ColumnRename : {
        rename : '이름 바꾸기'
    },

    MergeCells : {
        mergeCells  : '셀 병합',
        menuTooltip : '이 열로 정렬할 때 같은 값으로 셀 병합'
    },

    Search : {
        searchForValue : '값 검색'
    },

    Sort : {
        sort                   : '정렬',
        sortAscending          : '오름차순 정렬',
        sortDescending         : '내림차순 정렬',
        multiSort              : '다중 정렬',
        removeSorter           : '정렬 제거',
        addSortAscending       : '오름차순 정렬 추가',
        addSortDescending      : '내림차순 정렬 추가',
        toggleSortAscending    : '오름차순으로 변경',
        toggleSortDescending   : '내림차순으로 변경',
        sortAscendingShort     : '오름차순',
        sortDescendingShort    : '내림차순',
        removeSorterShort      : '제거',
        addSortAscendingShort  : '+ 오름차순',
        addSortDescendingShort : '+ 내림차순'
    },

    Split : {
        split        : '나누기',
        unsplit      : '병합',
        horizontally : '가로로',
        vertically   : '세로로',
        both         : '둘 다'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} 열. ` : ''}상황에 맞는 메뉴의 경우 SPACE 를 탭합니다${column.sortable ? ', 정렬하려면 ENTER' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : '행 선택 전환',
        toggleSelection : '전체 데이터세트 선택 전환'
    },

    RatingColumn : {
        cellLabel : column => `${column.location?.record ? `${column.location.record.get(column.field) || 0} :평가` : ''} ${column.text ? column.text : ''}`
    },

    GridBase : {
        loadFailedMessage  : '데이터 로드에 실패했습니다!',
        syncFailedMessage  : '데이터 동기화에 실패했습니다!',
        unspecifiedFailure : '알 수 없는 실패',
        networkFailure     : '네트워크 오류',
        parseFailure       : '서버 응답 분석에 실패했습니다',
        serverResponse     : '서버 응답:',
        noRows             : '표시할 기록 없음',
        moveColumnLeft     : '왼쪽 섹션으로 이동',
        moveColumnRight    : '오른쪽 섹션으로 이동',
        moveColumnTo       : region => ` ${region} 열로 이동`
    },

    CellMenu : {
        removeRow : '삭제'
    },

    RowCopyPaste : {
        copyRecord  : '복사하기',
        cutRecord   : '잘라내기',
        pasteRecord : '붙여넣기',
        rows        : '행들',
        row         : '행'
    },

    CellCopyPaste : {
        copy  : '복사하기',
        cut   : '잘라내기',
        paste : '붙여넣기'
    },

    PdfExport : {
        'Waiting for response from server' : '서버 응답 대기 중...',
        'Export failed'                    : '내보내기 실패',
        'Server error'                     : '서버 오류',
        'Generating pages'                 : '페이지 생성 중...',
        'Click to abort'                   : '취소'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : '내보내기 설정',
        export         : '내보내기',
        exporterType   : '페이지 번호 제어',
        cancel         : '취소',
        fileFormat     : '파일 형식',
        rows           : '행',
        alignRows      : '행 정렬',
        columns        : '열',
        paperFormat    : '용지 형식',
        orientation    : '방향',
        repeatHeader   : '헤더 반복'
    },

    ExportRowsCombo : {
        all     : '모든 행',
        visible : '행 표시'
    },

    ExportOrientationCombo : {
        portrait  : '인물',
        landscape : '풍경'
    },

    SinglePageExporter : {
        singlepage : '단일 페이지'
    },

    MultiPageExporter : {
        multipage     : '여러 페이지',
        exportingPage : ({ currentPage, totalPages }) => `${currentPage}/${totalPages} 페이지 내보내기`
    },

    MultiPageVerticalExporter : {
        multipagevertical : '여러 페이지 (세로)',
        exportingPage     : ({ currentPage, totalPages }) => `${currentPage}/${totalPages} 페이지 내보내기`
    },

    RowExpander : {
        loading  : '로딩 중',
        expand   : '펼치기',
        collapse : '접기'
    },

    TreeGroup : {
        group                  : '그룹화',
        stopGrouping           : '그룹화 중지',
        stopGroupingThisColumn : '이 열의 그룹화 해제'
    }
};

export default LocaleHelper.publishLocale(locale);
