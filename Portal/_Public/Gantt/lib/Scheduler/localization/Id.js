import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Id.js';

const locale = {

    localeName : 'Id',
    localeDesc : 'Bahasa Indonesia',
    localeCode : 'id',

    Object : {
        newEvent : 'Acara baru'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' acara'
    },

    Dependencies : {
        from    : 'Dari',
        to      : 'Hingga',
        valid   : 'Valid',
        invalid : 'Tidak valid'
    },

    DependencyType : {
        SS           : 'MM',
        SF           : 'MS',
        FS           : 'SM',
        FF           : 'SS',
        StartToStart : 'Mulai-hingga-Mulai',
        StartToEnd   : 'Mulai-hingga-Selesai',
        EndToStart   : 'Selesai-hingga-Mulai',
        EndToEnd     : 'Selesai-hingga-Selesai',
        short        : [
            'MM',
            'MS',
            'SM',
            'SS'
        ],
        long : [
            'Mulai-hingga-Mulai',
            'Mulai-hingga-Selesai',
            'Selesai-hingga-Selesai',
            'Selesai-hingga-Selesai'
        ]
    },

    DependencyEdit : {
        From              : 'Dari',
        To                : 'Hingga',
        Type              : 'Jenis',
        Lag               : 'Lag',
        'Edit dependency' : 'Edit dependensi',
        Save              : 'Simpan',
        Delete            : 'Hapus',
        Cancel            : 'Batalkan',
        StartToStart      : 'Mulai hingga Mulai',
        StartToEnd        : 'Mulai hingga Selesai',
        EndToStart        : 'Selesai hingga Mulai',
        EndToEnd          : 'Selesai hingga Selesai'
    },

    EventEdit : {
        Name         : 'Nama',
        Resource     : 'Sumber daya',
        Start        : 'Mulai',
        End          : 'Akhiri',
        Save         : 'Simpan',
        Delete       : 'Hapus',
        Cancel       : 'Batalkan',
        'Edit event' : 'Edit acara',
        Repeat       : 'Ulangi'
    },

    EventDrag : {
        eventOverlapsExisting : 'Acara tumpang tindih dengan acara yang sudah ada untuk sumber daya ini',
        noDropOutsideTimeline : 'Acara tidak dapat dibatalkan sepenuhnya di luar linimasa'
    },

    SchedulerBase : {
        'Add event'      : 'Tambahkan acara',
        'Delete event'   : 'Hapus acara',
        'Unassign event' : 'Batalkan penugasan acara',
        color            : 'Warna'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Perbesar',
        activeDateRange : 'Rentang tanggal',
        startText       : 'Tanggal mulai',
        endText         : 'Tanggal berakhir',
        todayText       : 'Hari ini'
    },

    EventCopyPaste : {
        copyEvent  : 'Salin acara',
        cutEvent   : 'Potong acara',
        pasteEvent : 'Tempel acara'
    },

    EventFilter : {
        filterEvents : 'Filter tugas',
        byName       : 'Berdasarkan nama'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Tampilkan linimasa terkini'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Detik'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Hari'
        },
        day : {
            name : 'Hari/jam'
        },
        week : {
            name : 'Minggu/jam'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Minggu/hari'
        },
        dayAndMonth : {
            name : 'Bulan'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Minggu'
        },
        weekAndMonth : {
            name : 'Minggu-minggu'
        },
        weekAndDayLetter : {
            name : 'Minggu/hari kerja'
        },
        weekDateAndMonth : {
            name : 'Bulan/minggu'
        },
        monthAndYear : {
            name : 'Bulan'
        },
        year : {
            name : 'Tahun'
        },
        manyYears : {
            name : 'Beberapa tahun'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Anda menghapus acara',
        'delete-all-message'        : 'Anda ingin menghapus semua kejadian dari acara ini?',
        'delete-further-message'    : 'Anda ingin menghapus kejadian ini dan semua kejadian di masa mendatang terkait acara ini, atau hanya kejadian tertentu saja?',
        'delete-further-btn-text'   : 'Hapus Semua Acara Di Masa Mendatang',
        'delete-only-this-btn-text' : 'Hapus Hanya Acara Ini',
        'update-title'              : 'Anda mengganti acara rutin',
        'update-all-message'        : 'Anda ingin mengganti semua kejadian dari acara ini?',
        'update-further-message'    : 'Anda ingin hanya mengubah kejadian ini dari acara ini, atau ini dan kejadian di masa mendatang?',
        'update-further-btn-text'   : 'Semua Kejadian di Masa Mendatang',
        'update-only-this-btn-text' : 'Hanya Acara Ini',
        Yes                         : 'Iya',
        Cancel                      : 'Batal',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' dan ',
        Daily                           : 'Harian',
        'Weekly on {1}'                 : ({ days }) => `Mingguan pada ${days}`,
        'Monthly on {1}'                : ({ days }) => `Bulanan pada ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Tahunan pada ${days} dari ${months}`,
        'Every {0} days'                : ({ interval }) => `Setiap ${interval} hari`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Setiap ${interval} minggu pada ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Setiap ${interval} bulan pada ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Setiap ${interval} tahun pada ${days} dari ${months}`,
        position1                       : 'pertama',
        position2                       : 'kedua',
        position3                       : 'ketiga',
        position4                       : 'keempat',
        position5                       : 'kelima',
        'position-1'                    : 'terakhir',
        day                             : 'hari',
        weekday                         : 'hari kerja',
        'weekend day'                   : 'akhir pekan',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Ulangi acara',
        Cancel              : 'Batalkan',
        Save                : 'Simpan',
        Frequency           : 'Frekuensi',
        Every               : 'Setiap',
        DAILYintervalUnit   : 'hari',
        WEEKLYintervalUnit  : 'minggu',
        MONTHLYintervalUnit : 'bulan',
        YEARLYintervalUnit  : 'tahun',
        Each                : 'Setiap',
        'On the'            : 'Pada',
        'End repeat'        : 'Akhiri pengulangan',
        'time(s)'           : 'kali'
    },

    RecurrenceDaysCombo : {
        day           : 'hari',
        weekday       : 'hari kerja',
        'weekend day' : 'akhir pekan'
    },

    RecurrencePositionsCombo : {
        position1    : 'pertama',
        position2    : 'kedua',
        position3    : 'ketiga',
        position4    : 'keempat',
        position5    : 'kelima',
        'position-1' : 'terakhir'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Tidak',
        After     : 'Setelah',
        'On date' : 'Pada tanggal'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Tidak berulang',
        Daily   : 'Harian',
        Weekly  : 'Mingguan',
        Monthly : 'Bulanan',
        Yearly  : 'Tahunan'
    },

    RecurrenceCombo : {
        None   : 'Tidak ada',
        Custom : 'Kustom...'
    },

    Summary : {
        'Summary for' : date => `Ringkasan untuk ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Jadwal lengkap',
        currentview  : 'Jadwal terlihat',
        daterange    : 'Rentang tanggal',
        completedata : 'Jadwal lengkap (untuk semua acara)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Rentang jadwal',
        'Export from'    : 'Dari',
        'Export to'      : 'Hingga'
    },

    ExcelExporter : {
        'No resource assigned' : 'Tidak ada sumber daya yang ditugaskan'
    },

    CrudManagerView : {
        serverResponseLabel : 'Respons server:'
    },

    DurationColumn : {
        Duration : 'Durasi'
    }
};

export default LocaleHelper.publishLocale(locale);
