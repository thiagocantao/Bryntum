import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Vi.js';

const emptyString = new String();

const locale = {

    localeName : 'Vi',
    localeDesc : 'Tiếng Việt',
    localeCode : 'vi',

    ColumnPicker : {
        column          : 'Cột',
        columnsMenu     : 'Cột',
        hideColumn      : 'Ẩn Cột',
        hideColumnShort : 'Ẩn',
        newColumns      : 'Cột mới'
    },

    Filter : {
        applyFilter   : 'Áp dụng bộ lọc',
        filter        : 'Bộ lọc',
        editFilter    : 'Chỉnh sửa bộ lọc',
        on            : 'Bật',
        before        : 'Trước',
        after         : 'Sau',
        equals        : 'Bằng',
        lessThan      : 'Ít hơn',
        moreThan      : 'Nhiều hơn',
        removeFilter  : 'Xóa bỏ bộ lọc',
        disableFilter : 'Bỏ bộ lọc'
    },

    FilterBar : {
        enableFilterBar  : 'Hiển thị thanh bộ lọc',
        disableFilterBar : 'Ẩn thanh bộ lọc'
    },

    Group : {
        group                : 'Nhóm',
        groupAscending       : 'Nhóm tăng dần',
        groupDescending      : 'Nhóm giảm dần',
        groupAscendingShort  : 'Tăng dần',
        groupDescendingShort : 'Giảm dần',
        stopGrouping         : 'Dừng nhóm',
        stopGroupingShort    : 'Dừng'
    },

    HeaderMenu : {
        moveBefore     : text => `Di chuyển trước "${text}"`,
        moveAfter      : text => `Di chuyển sau "${text}"`,
        collapseColumn : 'Thu gọn cột',
        expandColumn   : 'Mở rộng cột'
    },

    ColumnRename : {
        rename : 'Đổi tên'
    },

    MergeCells : {
        mergeCells  : 'Hợp nhất các ô',
        menuTooltip : 'Hợp nhất các ô có cùng giá trị khi được sắp xếp theo Column này'
    },

    Search : {
        searchForValue : 'Tìm kiếm giá trị'
    },

    Sort : {
        sort                   : 'Sắp xếp',
        sortAscending          : 'Sắp xếp tăng dần',
        sortDescending         : 'Sắp xếp giảm dần',
        multiSort              : 'Sắp xếp nhiều kiểu',
        removeSorter           : 'Xóa bộ sắp xếp',
        addSortAscending       : 'Thêm bộ sắp xếp tăng dần',
        addSortDescending      : 'Thêm bộ sắp xếp giảm dần',
        toggleSortAscending    : 'Thay đổi thành tăng dần',
        toggleSortDescending   : 'Thay đổi thành giảm dần',
        sortAscendingShort     : 'Tăng dần',
        sortDescendingShort    : 'Giảm dần',
        removeSorterShort      : 'Xóa',
        addSortAscendingShort  : '+ Tăng dần',
        addSortDescendingShort : '+ Giảm dần'
    },

    Split : {
        split        : 'Chia',
        unsplit      : 'Gộp',
        horizontally : 'Ngang',
        vertically   : 'Dọc',
        both         : 'Cả hai'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} column. ` : ''}nhấn phím SPACE cho menu ngữ cảnh menu${column.sortable ? ', nhấn phím ENTER để sắp xếp' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Chuyển đổi lựa chọn hàng',
        toggleSelection : 'Chuyển đổi lựa chọn của toàn bộ tập dữ liệu'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `xếp hạng : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Tải dữ liệu không thành công!',
        syncFailedMessage  : 'Đồng bộ hóa dữ liệu không thành công!',
        unspecifiedFailure : 'Lỗi không xác định',
        networkFailure     : 'Lỗi mạng',
        parseFailure       : 'Không thể phân tích cú pháp phản hồi của máy chủ',
        serverResponse     : 'Phản hồi của máy chủ:',
        noRows             : 'Không có hồ sơ nào để hiển thị',
        moveColumnLeft     : 'Di chuyển sang phần bên trái',
        moveColumnRight    : 'Di chuyển sang phần bên phải',
        moveColumnTo       : region => `Di chuyển cột đến ${region}`
    },

    CellMenu : {
        removeRow : 'Xóa hàng'
    },

    RowCopyPaste : {
        copyRecord  : 'Sao chép',
        cutRecord   : 'Cắt',
        pasteRecord : 'Dán',
        rows        : 'hàng',
        row         : 'hàng'
    },

    CellCopyPaste : {
        copy  : 'Sao chép',
        cut   : 'Cắt',
        paste : 'Dán'
    },

    PdfExport : {
        'Waiting for response from server' : 'Đang chờ phản hồi từ máy chủ ...',
        'Export failed'                    : 'Xuất không thành công',
        'Server error'                     : 'Lỗi máy chủ',
        'Generating pages'                 : 'Đang tạo trang ...',
        'Click to abort'                   : 'Hủy bỏ'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Cài đặt xuất',
        export         : 'Xuất',
        exporterType   : 'Kiểm soát phân trang',
        cancel         : 'Hủy bỏ',
        fileFormat     : 'Định dạng tệp',
        rows           : 'Dòng',
        alignRows      : 'Căn chỉnh các dòng',
        columns        : 'Các cột',
        paperFormat    : 'Định dạng trang',
        orientation    : 'Hướng',
        repeatHeader   : 'Lặp lại tiêu đề'
    },

    ExportRowsCombo : {
        all     : 'Tất cả các hàng',
        visible : 'Dòng có thể nhìn thấy'
    },

    ExportOrientationCombo : {
        portrait  : 'Dọc',
        landscape : 'Ngang'
    },

    SinglePageExporter : {
        singlepage : 'Trang đơn'
    },

    MultiPageExporter : {
        multipage     : 'Nhiều trang',
        exportingPage : ({ currentPage, totalPages }) => `Xuất trang ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Nhiều trang (theo chiều dọc)',
        exportingPage     : ({ currentPage, totalPages }) => `Xuất trang ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Đang tải',
        expand   : 'Mở rộng',
        collapse : 'Thu gọn'
    },

    TreeGroup : {
        group                  : 'Nhóm theo',
        stopGrouping           : 'Dừng nhóm',
        stopGroupingThisColumn : 'Hủy nhóm cột này'
    }
};

export default LocaleHelper.publishLocale(locale);
