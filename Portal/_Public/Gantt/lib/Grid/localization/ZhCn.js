import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/ZhCn.js';

const emptyString = new String();

const locale = {

    localeName : 'ZhCn',
    localeDesc : '中文（中国）',
    localeCode : 'zh-CN',

    ColumnPicker : {
        column          : '栏',
        columnsMenu     : '栏',
        hideColumn      : '隐藏栏',
        hideColumnShort : '隐藏',
        newColumns      : '新栏'
    },

    Filter : {
        applyFilter   : '应用筛选器',
        filter        : '筛选器',
        editFilter    : '编辑筛选器',
        on            : '打开',
        before        : '之前',
        after         : '之后',
        equals        : '等于',
        lessThan      : '小于',
        moreThan      : '大于',
        removeFilter  : '去除筛选器',
        disableFilter : '禁用过滤器'
    },

    FilterBar : {
        enableFilterBar  : '显示筛选条',
        disableFilterBar : '隐藏筛选条'
    },

    Group : {
        group                : '组',
        groupAscending       : '升序排列组',
        groupDescending      : '降序排列组',
        groupAscendingShort  : '升序',
        groupDescendingShort : '降序',
        stopGrouping         : '停止分组',
        stopGroupingShort    : '停止'
    },

    HeaderMenu : {
        moveBefore     : text => `移到"${text}"之前`,
        moveAfter      : text => `移到"${text}"之后`,
        collapseColumn : '折叠栏',
        expandColumn   : '展开栏'
    },

    ColumnRename : {
        rename : '重命名'
    },

    MergeCells : {
        mergeCells  : '合并单元格',
        menuTooltip : '按这一栏排序时，合并具有相同值的单元格'
    },

    Search : {
        searchForValue : '搜索值'
    },

    Sort : {
        sort                   : '分类',
        sortAscending          : '按升序分类',
        sortDescending         : '按降序分类',
        multiSort              : '多项分类',
        removeSorter           : '去除分类器',
        addSortAscending       : '添加升序分类器',
        addSortDescending      : '添加降序分类器',
        toggleSortAscending    : '改为升序',
        toggleSortDescending   : '改为降序',
        sortAscendingShort     : '升序',
        sortDescendingShort    : '降序',
        removeSorterShort      : '去除',
        addSortAscendingShort  : '+ 升序',
        addSortDescendingShort : '+ 降序'
    },

    Split : {
        split        : '拆分',
        unsplit      : '不拆分',
        horizontally : '水平',
        vertically   : '垂直',
        both         : '两者皆有'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} 栏.` : ''}上下文菜单的空格${column.sortable ? ', 按回车键分类' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : '切换选择行',
        toggleSelection : '切换选择整个数据集'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `评分 : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : '数据加载失败！',
        syncFailedMessage  : '数据同步失败！',
        unspecifiedFailure : '未知错误',
        networkFailure     : '网络错误',
        parseFailure       : '解析服务器响应失败',
        serverResponse     : '服务器响应：',
        noRows             : '无记录显示',
        moveColumnLeft     : '移到左边部分',
        moveColumnRight    : '移到右边部分',
        moveColumnTo       : region => `将栏移到 ${region}`
    },

    CellMenu : {
        removeRow : '删除'
    },

    RowCopyPaste : {
        copyRecord  : '复制',
        cutRecord   : '剪切',
        pasteRecord : '粘贴',
        rows        : '行',
        row         : '行'
    },

    CellCopyPaste : {
        copy  : '复制',
        cut   : '剪切',
        paste : '粘贴'
    },

    PdfExport : {
        'Waiting for response from server' : '等待服务器响应……',
        'Export failed'                    : '输出失败',
        'Server error'                     : '服务器错误',
        'Generating pages'                 : '生成页面……',
        'Click to abort'                   : '取消'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : '输出设置',
        export         : '输出',
        exporterType   : '控制分页',
        cancel         : '取消',
        fileFormat     : '文档格式',
        rows           : '行',
        alignRows      : '对齐行',
        columns        : '栏',
        paperFormat    : '纸张格式',
        orientation    : '方向',
        repeatHeader   : '重复页眉'
    },

    ExportRowsCombo : {
        all     : '所有行',
        visible : '可见行'
    },

    ExportOrientationCombo : {
        portrait  : '竖向',
        landscape : '横向'
    },

    SinglePageExporter : {
        singlepage : '单页'
    },

    MultiPageExporter : {
        multipage     : '多页',
        exportingPage : ({ currentPage, totalPages }) => `导出第 ${currentPage}/${totalPages} 页`
    },

    MultiPageVerticalExporter : {
        multipagevertical : '多页（垂直）',
        exportingPage     : ({ currentPage, totalPages }) => `导出第 ${currentPage}/${totalPages} 页`
    },

    RowExpander : {
        loading  : '正在加载',
        expand   : '展开',
        collapse : '折叠'
    },

    TreeGroup : {
        group                  : '按组',
        stopGrouping           : '停止分组',
        stopGroupingThisColumn : '取消列分组'
    }
};

export default LocaleHelper.publishLocale(locale);
