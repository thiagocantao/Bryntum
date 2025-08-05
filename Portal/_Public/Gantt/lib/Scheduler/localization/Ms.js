import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Ms.js';

const locale = {

    localeName : 'Ms',
    localeDesc : 'Melayu',
    localeCode : 'ms',

    Object : {
        newEvent : 'Peristiwa baharu'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' peristiwa'
    },

    Dependencies : {
        from    : 'Daripada',
        to      : 'Kepada',
        valid   : 'Sah',
        invalid : 'Tidak sah'
    },

    DependencyType : {
        SS           : 'MM',
        SF           : 'MS',
        FS           : 'SM',
        FF           : 'SS',
        StartToStart : 'Mula ke Mula',
        StartToEnd   : 'Mula ke Selesai',
        EndToStart   : 'Selesai ke Mula',
        EndToEnd     : 'Selesai ke Selesai',
        short        : [
            'MM',
            'MS',
            'SM',
            'SS'
        ],
        long : [
            'Mula ke Mula',
            'Mula ke Selesai',
            'Selesai ke Mula',
            'Selesai ke Selesai'
        ]
    },

    DependencyEdit : {
        From              : 'Daripada',
        To                : 'Kepada',
        Type              : 'Jenis',
        Lag               : 'Sela',
        'Edit dependency' : 'Edit kebergantungan',
        Save              : 'Simpan',
        Delete            : 'Hapus',
        Cancel            : 'Batal',
        StartToStart      : 'Mula ke Mula',
        StartToEnd        : 'Mula ke Akhir',
        EndToStart        : 'Akhir ke Mula',
        EndToEnd          : 'Akhir ke Akhir'
    },

    EventEdit : {
        Name         : 'Nama',
        Resource     : 'Sumber',
        Start        : 'Mula',
        End          : 'Akhir',
        Save         : 'Simpan',
        Delete       : 'Hapus',
        Cancel       : 'Batal',
        'Edit event' : 'Edit peristiwa',
        Repeat       : 'Ulang'
    },

    EventDrag : {
        eventOverlapsExisting : 'Peristiwa bertindih dengan peristiwa sedia ada untuk sumber ini',
        noDropOutsideTimeline : 'Peristiwa tidak boleh digugurkan sepenuhnya di luar garis masa'
    },

    SchedulerBase : {
        'Add event'      : 'Tambah peristiwa',
        'Delete event'   : 'Hapus peristiwa',
        'Unassign event' : 'Nyahtetap peristiwa',
        color            : 'Warna'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zum',
        activeDateRange : 'Julat tarikh',
        startText       : 'Tarikh mula',
        endText         : 'Tarikh akhir',
        todayText       : 'Hari ini'
    },

    EventCopyPaste : {
        copyEvent  : 'Salin peristiwa',
        cutEvent   : 'Potong peristiwa',
        pasteEvent : 'Tampal peristiwa'
    },

    EventFilter : {
        filterEvents : 'Tapis tugas',
        byName       : 'Ikut nama'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Tunjuk garis masa semasa'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Saat'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD-MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD-MM',
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
            name : 'Minggu'
        },
        weekAndDayLetter : {
            name : 'Minggu/hari biasa'
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
            name : 'Berbilang tahun'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Anda menghapuskan peristiwa',
        'delete-all-message'        : 'Adakah anda mahu menghapuskan semua kejadian peristiwa ini?',
        'delete-further-message'    : 'Adakah anda mahu menghapuskan ini dan semua kejadian masa hadapan peristiwa ini, atau hanya kejadian yang dipilih?',
        'delete-further-btn-text'   : 'Hapus Semua Peristiwa Masa Depan',
        'delete-only-this-btn-text' : 'Padam Hanya Peristiwa Ini',
        'update-title'              : 'Anda mengubah peristiwa berulang',
        'update-all-message'        : 'Adakah anda mahu mengubah semua kejadian peristiwa ini?',
        'update-further-message'    : 'Adakah anda mahu menukar kejadian peristiwa ini sahaja, atau ini dan semua kejadian akan datang?',
        'update-further-btn-text'   : 'Semua Peristiwa Masa Depan',
        'update-only-this-btn-text' : 'Hanya Peristiwa Ini',
        Yes                         : 'Ya',
        Cancel                      : 'Batal',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' dan ',
        Daily                           : 'Harian',
        'Weekly on {1}'                 : ({ days }) => `Mingguan pada ${days}`,
        'Monthly on {1}'                : ({ days }) => `Bulanan pada ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Tahunan pada ${days} daripada ${months}`,
        'Every {0} days'                : ({ interval }) => `Setiap ${interval} hari`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Setiap ${interval} minggu pada ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Setiap ${interval} bulan pada ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Setiap ${interval} tahun pada ${days} daripada ${months}`,
        position1                       : 'pertama',
        position2                       : 'kedua',
        position3                       : 'ketiga',
        position4                       : 'keempat',
        position5                       : 'kelima',
        'position-1'                    : 'terakhir',
        day                             : 'hari',
        weekday                         : 'hari minggu',
        'weekend day'                   : 'hari hujung minggu',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Peristiwa ulang',
        Cancel              : 'Batal',
        Save                : 'Simpan',
        Frequency           : 'Frekuensi',
        Every               : 'Setiap',
        DAILYintervalUnit   : 'hari',
        WEEKLYintervalUnit  : 'minggu',
        MONTHLYintervalUnit : 'bulan',
        YEARLYintervalUnit  : 'tahun',
        Each                : 'Setiap',
        'On the'            : 'Pada',
        'End repeat'        : 'Akhir ulang',
        'time(s)'           : 'masa'
    },

    RecurrenceDaysCombo : {
        day           : 'hari',
        weekday       : 'hari minggu',
        'weekend day' : 'hari hujung minggu'
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
        Never     : 'Jangan',
        After     : 'Selepas',
        'On date' : 'Pada tarikh'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Tiada ulangan',
        Daily   : 'Harian',
        Weekly  : 'Mingguan',
        Monthly : 'Bulanan',
        Yearly  : 'Tahunan'
    },

    RecurrenceCombo : {
        None   : 'Tiada',
        Custom : 'Suaikan...'
    },

    Summary : {
        'Summary for' : date => `Ringkasan untuk ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Jadual lengkap',
        currentview  : 'Jadual boleh lihat',
        daterange    : 'Julat tarikh',
        completedata : 'Jadual lengkap (untuk semua peristiwa)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Julat jadual',
        'Export from'    : 'Daripada',
        'Export to'      : 'Kepada'
    },

    ExcelExporter : {
        'No resource assigned' : 'Tiada sumber diperuntukkan'
    },

    CrudManagerView : {
        serverResponseLabel : 'Respons pelayan:'
    },

    DurationColumn : {
        Duration : 'Tempoh'
    }
};

export default LocaleHelper.publishLocale(locale);
