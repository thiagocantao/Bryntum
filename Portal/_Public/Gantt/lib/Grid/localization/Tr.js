import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Tr.js';

const emptyString = new String();

const locale = {

    localeName : 'Tr',
    localeDesc : 'Türkçe',
    localeCode : 'tr',

    ColumnPicker : {
        column          : 'Sütun',
        columnsMenu     : 'Sütun menüsü',
        hideColumn      : 'Sütunu gizle',
        hideColumnShort : 'Gizle',
        newColumns      : 'Yeni sütunlar'
    },

    Filter : {
        applyFilter   : 'Filtre uygula',
        filter        : 'Filtre',
        editFilter    : 'Filtre düzenle',
        on            : 'Açık',
        before        : 'Önce',
        after         : 'Sonra',
        equals        : 'Eşittir',
        lessThan      : 'den az',
        moreThan      : 'den çok',
        removeFilter  : 'Filtreyi kaldır',
        disableFilter : 'Filtreyi devre dışı bırak'
    },

    FilterBar : {
        enableFilterBar  : 'Filtre çubuğunu göster',
        disableFilterBar : 'Filtre çubuğunu gizle'
    },

    Group : {
        group                : 'Gruplandır',
        groupAscending       : 'Küçükten büyüğe doğru gruplandır',
        groupDescending      : 'Büyükten küçüğe doğru gruplandır',
        groupAscendingShort  : 'Küçükten büyüğe',
        groupDescendingShort : 'Büyükten küçüğe ',
        stopGrouping         : 'Gruplandırmayı durdur',
        stopGroupingShort    : 'Durdur'
    },

    HeaderMenu : {
        moveBefore     : text => `Önce taşı "${text}"`,
        moveAfter      : text => `Sonra taşı "${text}"`,
        collapseColumn : 'Sütunu daralt',
        expandColumn   : 'Sütunu genişlet'
    },

    ColumnRename : {
        rename : 'Yeniden adlandır'
    },

    MergeCells : {
        mergeCells  : 'Hücreleri birleştir',
        menuTooltip : 'Bu sütuna göre sıralandığında aynı değere sahip hücreleri birleştir'
    },

    Search : {
        searchForValue : 'Değer ara'
    },

    Sort : {
        sort                   : 'Sırala',
        sortAscending          : 'Küçükten büyüğe doğru sırala',
        sortDescending         : 'Büyükten küçüğe doğru sırala',
        multiSort              : 'Çoklu sırala',
        removeSorter           : 'Sıralayıcıyı kaldır',
        addSortAscending       : 'Küçükten büyüğe doğru sıralayıcı ekle',
        addSortDescending      : 'Büyükten küçüğe doğru sıralayıcı ekle',
        toggleSortAscending    : 'Küçükten büyüğe doğru değiştir',
        toggleSortDescending   : 'Büyükten küçüğe doğru değiştir',
        sortAscendingShort     : 'Küçükten büyüğe',
        sortDescendingShort    : 'Büyükten küçüğe',
        removeSorterShort      : 'Kaldır',
        addSortAscendingShort  : '+ Küçükten büyüğe',
        addSortDescendingShort : '+ Büyükten küçüğe'
    },

    Split : {
        split        : 'Bölünmüş',
        unsplit      : 'Bölünmemiş',
        horizontally : 'Yatay',
        vertically   : 'Dikey',
        both         : 'Her İkisi'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} sütun. ` : ''}bağlam menüsü için SPACE’a dokunun${column.sortable ? `, sıralamak için ENTER’a bas` : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Satır seçimini değiştir',
        toggleSelection : 'Tüm veri kümesinin seçimini değiştir'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `değerlendirme : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Veri yükleme başarısız!',
        syncFailedMessage  : 'Veri senkronizasyonu başarısız!',
        unspecifiedFailure : 'Belirtilmemiş hata',
        networkFailure     : 'Ağ hatası',
        parseFailure       : 'Sunucu yanıtı ayrıştırma başarısız',
        serverResponse     : 'Sunucu yanıtı:',
        noRows             : 'Gösterilecek kayıt bulunamadı',
        moveColumnLeft     : 'Sol sütuna taşı',
        moveColumnRight    : 'Sağ sütuna taşı',
        moveColumnTo       : region => `Sütunu şuraya taşı ${region}`
    },

    CellMenu : {
        removeRow : 'Satır sil'
    },

    RowCopyPaste : {
        copyRecord  : 'Kopyala',
        cutRecord   : 'Kes',
        pasteRecord : 'Yapıştır',
        rows        : 'sıralar',
        row         : 'sıra'
    },

    CellCopyPaste : {
        copy  : 'Kopyala',
        cut   : 'Kes',
        paste : 'Yapıştır'
    },

    PdfExport : {
        'Waiting for response from server' : 'Sunucudan yanıt bekleniyor...',
        'Export failed'                    : 'Dışarı aktarma başarısız',
        'Server error'                     : 'Sunucu hatası',
        'Generating pages'                 : 'Sayfalar oluşturuluyor...',
        'Click to abort'                   : 'İptal et'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Dışarı aktarma ayarları',
        export         : 'Dışarı aktar',
        exporterType   : 'Dışarı aktarma aracı türü',
        cancel         : 'İptal et',
        fileFormat     : 'Dosya biçimi',
        rows           : 'Satır',
        alignRows      : 'Satırları hizala',
        columns        : 'Sütun',
        paperFormat    : 'Sayfa formatı',
        orientation    : 'Yön',
        repeatHeader   : 'Başlığı tekrarla'
    },

    ExportRowsCombo : {
        all     : 'Tüm satırlar',
        visible : 'Görünür satırlar'
    },

    ExportOrientationCombo : {
        portrait  : 'Dikey',
        landscape : 'Yatay'
    },

    SinglePageExporter : {
        singlepage : 'Tek sayfa'
    },

    MultiPageExporter : {
        multipage     : 'Birden fazla sayfa',
        exportingPage : ({ currentPage, totalPages }) => `Sayfa ${currentPage}/${totalPages} dışa aktarılıyor`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Birden fazla sayfa (dikey)',
        exportingPage     : ({ currentPage, totalPages }) => `Sayfa ${currentPage}/${totalPages} dışa aktarılıyor`
    },

    RowExpander : {
        loading  : 'Yükleniyor',
        expand   : 'Genişlet',
        collapse : 'Daralt'
    },

    TreeGroup : {
        group                  : 'Grupla',
        stopGrouping           : 'Gruplamayı Durdur',
        stopGroupingThisColumn : 'Bu Sütunun Gruplamasını Kaldır'
    }
};

export default LocaleHelper.publishLocale(locale);
