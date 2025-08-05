import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Ms.js';

const emptyString = new String();

const locale = {

    localeName : 'Ms',
    localeDesc : 'Melayu',
    localeCode : 'ms',

    ColumnPicker : {
        column          : 'Kolum',
        columnsMenu     : 'Kolum',
        hideColumn      : 'Sembunyi kolum',
        hideColumnShort : 'Sembunyi',
        newColumns      : 'Kolum baharu'
    },

    Filter : {
        applyFilter   : 'Guna penapis',
        filter        : 'Penapis',
        editFilter    : 'Edit penapis',
        on            : 'Hidup',
        before        : 'Sebelum',
        after         : 'Selepas',
        equals        : 'Sama dengan',
        lessThan      : 'Kurang daripada',
        moreThan      : 'Lebih daripada',
        removeFilter  : 'Buang penapis',
        disableFilter : 'Nyahdaya penapis'
    },

    FilterBar : {
        enableFilterBar  : 'Tunjuk bar penapis',
        disableFilterBar : 'Sembunyi bar penapis'
    },

    Group : {
        group                : 'Kumpulan',
        groupAscending       : 'Kumpulan menaik',
        groupDescending      : 'Kumpulan menurun',
        groupAscendingShort  : 'Menaik',
        groupDescendingShort : 'Menurun',
        stopGrouping         : 'Henti mengumpulkan',
        stopGroupingShort    : 'Henti'
    },

    HeaderMenu : {
        moveBefore     : text => `Gerak sebelum "${text}"`,
        moveAfter      : text => `Gerak selepas "${text}"`,
        collapseColumn : 'Runtuh lajur',
        expandColumn   : 'Kembang lajur'
    },

    ColumnRename : {
        rename : 'Nama semula'
    },

    MergeCells : {
        mergeCells  : 'Gabung sel',
        menuTooltip : 'Gabungkan sel dengan nilai yang sama apabila diisih mengikut kolum ini'
    },

    Search : {
        searchForValue : 'Cari nilai'
    },

    Sort : {
        sort                   : 'Isih',
        sortAscending          : 'Isih menaik',
        sortDescending         : 'Isih menurun',
        multiSort              : 'Multi isih',
        removeSorter           : 'Buang pengisih',
        addSortAscending       : 'Tambah pengisih menaik',
        addSortDescending      : 'Tambah pengisih menurun',
        toggleSortAscending    : 'Tukar kepada menaik',
        toggleSortDescending   : 'Tukar kepada menurun',
        sortAscendingShort     : 'Menaik',
        sortDescendingShort    : 'Menurun',
        removeSorterShort      : 'Buang',
        addSortAscendingShort  : '+ Menaik',
        addSortDescendingShort : '+ Menurun'
    },

    Split : {
        split        : 'Bahagi',
        unsplit      : 'Tidak Dipisah',
        horizontally : 'Secara Menegak',
        vertically   : 'Secara Mendatar',
        both         : 'Kedua-duanya'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} kolum. ` : ''}SPACE untuk menu konteks${column.sortable ? ', ENTER untuk isih' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Togel pemilihan baris',
        toggleSelection : 'Togel pemilihan set data keseluruhan'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? ` penilaian : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Pemuatan data gagal!',
        syncFailedMessage  : 'Sinkronisasi data gagal!',
        unspecifiedFailure : 'Kegagalan tak dinyata',
        networkFailure     : 'Ralat rangkaian',
        parseFailure       : 'Gagal menghuraikan respons pelayan',
        serverResponse     : 'Respons pelayan:',
        noRows             : 'Tiada rekod untuk dipaparkan',
        moveColumnLeft     : 'Gerak ke bahagian kiri',
        moveColumnRight    : 'Gerak ke bahagian kanan',
        moveColumnTo       : region => `Gerak kolum ke ${region}`
    },

    CellMenu : {
        removeRow : 'Hapus'
    },

    RowCopyPaste : {
        copyRecord  : 'Salin',
        cutRecord   : 'Potong',
        pasteRecord : 'Tampal',
        rows        : 'baris-baris',
        row         : 'baris'
    },

    CellCopyPaste : {
        copy  : 'Salin',
        cut   : 'Potong',
        paste : 'Tampal'
    },

    PdfExport : {
        'Waiting for response from server' : 'Menunggu respons daripada pelayan...',
        'Export failed'                    : 'Eksport gagal',
        'Server error'                     : 'Ralat pelayan',
        'Generating pages'                 : 'Menjana halaman...',
        'Click to abort'                   : 'Batal'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Tetapan eksport',
        export         : 'Eksport',
        exporterType   : 'Kawal penomboran',
        cancel         : 'Batal',
        fileFormat     : 'Format fail',
        rows           : 'Baris',
        alignRows      : 'Jajarkan baris',
        columns        : 'Kolum',
        paperFormat    : 'Format kertas',
        orientation    : 'Orientasi',
        repeatHeader   : 'Pengepala ulang'
    },

    ExportRowsCombo : {
        all     : 'Semua baris',
        visible : 'Baris boleh lihat'
    },

    ExportOrientationCombo : {
        portrait  : 'Portret',
        landscape : 'Landskap'
    },

    SinglePageExporter : {
        singlepage : 'Halaman tunggal'
    },

    MultiPageExporter : {
        multipage     : 'Halaman pelbagai',
        exportingPage : ({ currentPage, totalPages }) => `Mengeksport halaman ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Halaman pelbagai (menegak)',
        exportingPage     : ({ currentPage, totalPages }) => `Mengeksport halaman ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Memuat',
        expand   : 'Kembang',
        collapse : 'Kecil'
    },

    TreeGroup : {
        group                  : 'Kumpulkan mengikut',
        stopGrouping           : 'Berhenti mengumpulkan',
        stopGroupingThisColumn : 'Batalkan pengumpulan baris ini'
    }
};

export default LocaleHelper.publishLocale(locale);
