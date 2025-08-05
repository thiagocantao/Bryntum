import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Id.js';

const emptyString = new String();

const locale = {

    localeName : 'Id',
    localeDesc : 'Bahasa Indonesia',
    localeCode : 'id',

    ColumnPicker : {
        column          : 'Kolom',
        columnsMenu     : 'Kolom',
        hideColumn      : 'Sembunyikan kolom',
        hideColumnShort : 'Sembunyikan',
        newColumns      : 'Kolom baru'
    },

    Filter : {
        applyFilter   : 'Terapkan filter',
        filter        : 'Filter',
        editFilter    : 'Edit filter',
        on            : 'Dengan',
        before        : 'Sebelum',
        after         : 'Sesudah',
        equals        : 'Sama dengan',
        lessThan      : 'Kurang dari',
        moreThan      : 'Lebih dari',
        removeFilter  : 'Hapus filter',
        disableFilter : 'Nonaktifkan filter'
    },

    FilterBar : {
        enableFilterBar  : 'Tampilkan kolom filter',
        disableFilterBar : 'Sembunyikan kolom filter'
    },

    Group : {
        group                : 'Kelompokkan',
        groupAscending       : 'Kelompokkan naik',
        groupDescending      : 'Kelompokkan turun',
        groupAscendingShort  : 'Naik',
        groupDescendingShort : 'Turun',
        stopGrouping         : 'Hentikan pengelompokan',
        stopGroupingShort    : 'Hentikan'
    },

    HeaderMenu : {
        moveBefore     : text => `Pindahkan sebelum "${text}"`,
        moveAfter      : text => `Pindahkan setelah "${text}"`,
        collapseColumn : 'Ciutkan kolom',
        expandColumn   : 'Perluas kolom'
    },

    ColumnRename : {
        rename : 'Ganti nama'
    },

    MergeCells : {
        mergeCells  : 'Gabungkan sel',
        menuTooltip : 'Gabungkan sel dengan nilai yang sama ketika disortir dengan kolom ini'
    },

    Search : {
        searchForValue : 'Cari nilai'
    },

    Sort : {
        sort                   : 'Sortir',
        sortAscending          : 'Sortir naik',
        sortDescending         : 'Sortir turun',
        multiSort              : 'Multi-sortir',
        removeSorter           : 'Hapus penyortir',
        addSortAscending       : 'Tambahkan penyortir naik',
        addSortDescending      : 'Tambahkan penyortir turun',
        toggleSortAscending    : 'Ubah ke naik',
        toggleSortDescending   : 'Ubah ke turun',
        sortAscendingShort     : 'Naik',
        sortDescendingShort    : 'Turun',
        removeSorterShort      : 'Hapus',
        addSortAscendingShort  : '+ Naik',
        addSortDescendingShort : '+ Turun'
    },

    Split : {
        split        : 'Pisahkan',
        unsplit      : 'Gabungkan',
        horizontally : 'Secara Horizontal',
        vertically   : 'Secara Vertikal',
        both         : 'Kedua-duanya'
    },

    Column : {
        columnLabel : column => `kolom ${column.text ? `${column.text}. ` : ''}SPASI untuk menu konteks${column.sortable ? ', ENTER untuk menyortir' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Aktifkan/nonaktifkan pilihan baris',
        toggleSelection : 'Aktifkan/nonaktifkan pilihan et data keseluruhan'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? ` penilaian : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Pemuatan data gagal!',
        syncFailedMessage  : 'Sinkronisasi data gagal!',
        unspecifiedFailure : 'Kegagalan tidak ditentukan',
        networkFailure     : 'Kesalahan jaringan',
        parseFailure       : 'Gagal mengurai respons server',
        serverResponse     : 'Respons server:',
        noRows             : 'Tidak ada data untuk ditampilkan',
        moveColumnLeft     : 'Pindahkan ke bagian kiri',
        moveColumnRight    : 'Pindahkan ke bagian kanan',
        moveColumnTo       : region => `Pindahkan kolom ke ${region}`
    },

    CellMenu : {
        removeRow : 'Hapus'
    },

    RowCopyPaste : {
        copyRecord  : 'Salin',
        cutRecord   : 'Potong',
        pasteRecord : 'Tempel',
        rows        : 'baris-baris',
        row         : 'baris'
    },

    CellCopyPaste : {
        copy  : 'Salin',
        cut   : 'Potong',
        paste : 'Tempel'
    },

    PdfExport : {
        'Waiting for response from server' : 'Menunggu respons dari server...',
        'Export failed'                    : 'Ekspor gagal',
        'Server error'                     : 'Kesalahan server',
        'Generating pages'                 : 'Membuat halaman...',
        'Click to abort'                   : 'Batalkan'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Pengaturan ekspor',
        export         : 'Ekspor',
        exporterType   : 'Mengontrol pemberian nomor halaman',
        cancel         : 'Batalkan',
        fileFormat     : 'Format file',
        rows           : 'Baris',
        alignRows      : 'Ratakan baris',
        columns        : 'Kolom',
        paperFormat    : 'Format kertas',
        orientation    : 'Orientasi',
        repeatHeader   : 'Ulangi header'
    },

    ExportRowsCombo : {
        all     : 'Semua baris',
        visible : 'Baris yang dapat dilihat'
    },

    ExportOrientationCombo : {
        portrait  : 'Potret',
        landscape : 'Lanskap'
    },

    SinglePageExporter : {
        singlepage : 'Halaman tunggal'
    },

    MultiPageExporter : {
        multipage     : 'Multi-halaman',
        exportingPage : ({ currentPage, totalPages }) => `Mengekspor halaman ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Multi- halaman (vertikal)',
        exportingPage     : ({ currentPage, totalPages }) => `Mengekspor halaman ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Memuat',
        expand   : 'Perluas',
        collapse : 'Ciutkan'
    },

    TreeGroup : {
        group                  : 'Kelompokkan berdasarkan',
        stopGrouping           : 'Berhenti mengelompokkan',
        stopGroupingThisColumn : 'Batal kelompokkan kolom ini'
    }
};

export default LocaleHelper.publishLocale(locale);
