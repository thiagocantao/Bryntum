import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Ja.js';

const emptyString = new String();

const locale = {

    localeName : 'Ja',
    localeDesc : '日本語',
    localeCode : 'ja',

    ColumnPicker : {
        column          : '列',
        columnsMenu     : '列',
        hideColumn      : '列を非表示にする',
        hideColumnShort : '非表示',
        newColumns      : '新しい列'
    },

    Filter : {
        applyFilter   : 'フィルターをかける',
        filter        : 'フィルター',
        editFilter    : 'フィルターを編集する',
        on            : '条件',
        before        : '前',
        after         : '後',
        equals        : '同じ',
        lessThan      : 'より少ない',
        moreThan      : 'より多い',
        removeFilter  : 'フィルターを解除する',
        disableFilter : 'フィルタを無効にする'
    },

    FilterBar : {
        enableFilterBar  : 'フィルターバーを表示する',
        disableFilterBar : 'フィルターバーを非表示にする'
    },

    Group : {
        group                : 'グループ',
        groupAscending       : 'グループ昇順',
        groupDescending      : 'グループ降順',
        groupAscendingShort  : '昇順',
        groupDescendingShort : '降順',
        stopGrouping         : 'グループ化を解除する',
        stopGroupingShort    : '解除する'
    },

    HeaderMenu : {
        moveBefore     : text => `前に移動する "${text}"`,
        moveAfter      : text => `後に移動する "${text}"`,
        collapseColumn : '列を折りたたむ',
        expandColumn   : '列を展開する'
    },

    ColumnRename : {
        rename : '名前を変更する'
    },

    MergeCells : {
        mergeCells  : 'セルを結合する',
        menuTooltip : 'この列で並び替えたとき同じ値のセルを結合する'
    },

    Search : {
        searchForValue : '値を探す'
    },

    Sort : {
        sort                   : '並び替える',
        sortAscending          : '昇順に並び替える',
        sortDescending         : '降順に並び替える',
        multiSort              : '複数並び替え',
        removeSorter           : '並び替えを解除する',
        addSortAscending       : '昇順並び替えを追加する',
        addSortDescending      : '降順並び替えを追加する',
        toggleSortAscending    : '昇順に変更する',
        toggleSortDescending   : '降順に変更する',
        sortAscendingShort     : '昇順',
        sortDescendingShort    : '降順',
        removeSorterShort      : '解除する',
        addSortAscendingShort  : '＋昇順',
        addSortDescendingShort : '＋降順'
    },

    Split : {
        split        : '分割する',
        unsplit      : '結合する',
        horizontally : '横方向に',
        vertically   : '縦方向に',
        both         : '両方'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} 列. ` : ''}コンテキストメニューの「SPACE」キーを押します${column.sortable ? ', ENTERキーを押して並び替える' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : '行選択を切り替える',
        toggleSelection : 'データセット全体の選択を切り替える'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `評価 : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'データの読み込みに失敗しました',
        syncFailedMessage  : 'データの同期に失敗しました',
        unspecifiedFailure : 'エラーを特定できません',
        networkFailure     : 'ネットワークエラー',
        parseFailure       : 'サーバーの応答の解析に失敗しました',
        serverResponse     : 'サーバーの応答:',
        noRows             : '表示するレコードがありません',
        moveColumnLeft     : '左セクションに移動する',
        moveColumnRight    : '右セクションに移動する',
        moveColumnTo       : region => `列を ${region}に移動する`
    },

    CellMenu : {
        removeRow : '削除する'
    },

    RowCopyPaste : {
        copyRecord  : 'コピーする',
        cutRecord   : '切り取る',
        pasteRecord : '貼り付ける',
        rows        : '複数の行',
        row         : '行'
    },

    CellCopyPaste : {
        copy  : 'コピーする',
        cut   : '切り取る',
        paste : '貼り付ける'
    },

    PdfExport : {
        'Waiting for response from server' : 'サーバーの応答を待っています。',
        'Export failed'                    : 'エクスポートに失敗しました',
        'Server error'                     : 'サーバーエラー',
        'Generating pages'                 : 'ページを生成しています。',
        'Click to abort'                   : '取り消す'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : '設定をエクスポートする',
        export         : 'エクスポート',
        exporterType   : 'ページ設定を管理する',
        cancel         : '取り消す',
        fileFormat     : 'ファイル形式',
        rows           : '行',
        alignRows      : '行を揃える',
        columns        : '列',
        paperFormat    : '用紙形式',
        orientation    : '向き',
        repeatHeader   : 'ヘッダーを繰り返す'
    },

    ExportRowsCombo : {
        all     : 'すべての行',
        visible : '表示可能な行'
    },

    ExportOrientationCombo : {
        portrait  : '縦長',
        landscape : '横長'
    },

    SinglePageExporter : {
        singlepage : '単一ページ'
    },

    MultiPageExporter : {
        multipage     : '複数ページ',
        exportingPage : ({ currentPage, totalPages }) => `ページ ${currentPage}/${totalPages} をエクスポートしています`
    },

    MultiPageVerticalExporter : {
        multipagevertical : '複数ページ（縦）',
        exportingPage     : ({ currentPage, totalPages }) => `ページ ${currentPage}/${totalPages} をエクスポートしています`
    },

    RowExpander : {
        loading  : '読み込み中',
        expand   : '拡張する',
        collapse : '縮小する'
    },

    TreeGroup : {
        group                  : 'グループ化',
        stopGrouping           : 'グループ化の解除',
        stopGroupingThisColumn : 'この列のグループ化を解除'
    }
};

export default LocaleHelper.publishLocale(locale);
