import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Tr.js';

const locale = {

    localeName : 'Tr',
    localeDesc : 'Türkçe',
    localeCode : 'tr',

    Object : {
        newEvent : 'Yeni etkinlik'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' etkinlik' + (data !== 1 ? 'ler' : '')
    },

    Dependencies : {
        from    : 'Şu tarihten',
        to      : 'Bu tarihe kadar',
        valid   : 'Geçerli',
        invalid : 'Geçersiz'
    },

    DependencyType : {
        SS           : 'BaBa',
        SF           : 'BaBi',
        FS           : 'BiBa',
        FF           : 'BiBi',
        StartToStart : 'Başlangıç-Başlangıç',
        StartToEnd   : 'Başlangıç-Bitiş',
        EndToStart   : 'Bitiş-Başlangıç',
        EndToEnd     : 'Bitiş-Bitiş',
        short        : [
            'BaBa',
            'BaBi',
            'BiBa',
            'BiBi'
        ],
        long : [
            'Başlangıç-Başlangıç',
            'Başlangıç-Bitiş',
            'Bitiş-Başlangıç',
            'Bitiş-Bitiş'
        ]
    },

    DependencyEdit : {
        From              : 'Şu tarihten',
        To                : 'Bu tarihe kadar',
        Type              : 'Tür',
        Lag               : 'Gecikme',
        'Edit dependency' : 'Bağımlılığı düzenle',
        Save              : 'Kaydet',
        Delete            : 'Sil',
        Cancel            : 'İptal et',
        StartToStart      : 'Başlangıç-Başlangıç',
        StartToEnd        : 'Başlangıç-Bitiş',
        EndToStart        : 'Bitiş-Başlangıç',
        EndToEnd          : 'Bitiş-Bitiş'
    },

    EventEdit : {
        Name         : 'Ad',
        Resource     : 'Kaynak',
        Start        : 'Başlangıç',
        End          : 'Bitiş',
        Save         : 'Kaydet',
        Delete       : 'Sil',
        Cancel       : 'İptal et',
        'Edit event' : 'Etkinliği düzenle',
        Repeat       : 'Tekrarla'
    },

    EventDrag : {
        eventOverlapsExisting : 'Etkinlik, bu kaynaktaki mevcut etkinlikle çakışıyor',
        noDropOutsideTimeline : 'Etkinlik, zaman çizelgesinin tamamen dışına bırakılamaz'
    },

    SchedulerBase : {
        'Add event'      : 'Etkinlik ekle',
        'Delete event'   : 'Etkinlik sil',
        'Unassign event' : 'Etkinlik atamasını kaldır',
        color            : 'Renk'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Yakınlaştır',
        activeDateRange : 'Tarih aralığı',
        startText       : 'Başlangıç tarihi',
        endText         : 'Bitiş tarihi',
        todayText       : 'Bugün'
    },

    EventCopyPaste : {
        copyEvent  : 'Etkinliği kopyala',
        cutEvent   : 'Etkinliği kes',
        pasteEvent : 'Etkinliği yapıştır'
    },

    EventFilter : {
        filterEvents : 'Etkinlikleri filtrele',
        byName       : 'Ada göre'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Mevcut zaman çizelgesini göster'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Saniyeler'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Gün'
        },
        day : {
            name : 'Gün/saatler'
        },
        week : {
            name : 'Hafta/saatler'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Hafta/saatler'
        },
        dayAndMonth : {
            name : 'Ay'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Hafta'
        },
        weekAndMonth : {
            name : 'Haftalar'
        },
        weekAndDayLetter : {
            name : 'Haftalar/haftanın günleri'
        },
        weekDateAndMonth : {
            name : 'Aylar/haftalar'
        },
        monthAndYear : {
            name : 'Aylar'
        },
        year : {
            name : 'Yıllar'
        },
        manyYears : {
            name : 'Bir çok yıl'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Bir etkinliği siliyorsunuz',
        'delete-all-message'        : 'Bu etkinliğe ait tüm mesajları silmek istiyor musunuz?',
        'delete-further-message'    : 'Bunu ve bu etkinliğin gelecekteki tüm mesajlarını mı yoksa yalnızca seçilen mesajı mı silmek istiyorsunuz?',
        'delete-further-btn-text'   : 'Gelecekteki Tüm Etkinlikleri Sil',
        'delete-only-this-btn-text' : 'Yalnızca Bu Etkinliği Sil',
        'update-title'              : 'Tekrarlanan bir etkinliği değiştiriyorsunuz',
        'update-all-message'        : 'Bu etkinliğin tüm mesajlarını değiştirmek istiyor musunuz?',
        'update-further-message'    : 'Etkinliğin yalnızca bu mesajını mı yoksa bunu ve gelecekteki tüm mesajlarını mı değiştirmek istiyorsunuz?',
        'update-further-btn-text'   : 'Gelecekteki Tüm Etkinlikler',
        'update-only-this-btn-text' : 'Yalnızca Bu Etkinlik',
        Yes                         : 'Evet',
        Cancel                      : 'İptal Et',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' ve ',
        Daily                           : 'Günlük',
        'Weekly on {1}'                 : ({ days }) => `Her hafta şu gün ${days}`,
        'Monthly on {1}'                : ({ days }) => `Her ay şu gün ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Her yıl şu günü${days} şu ayın ${months}`,
        'Every {0} days'                : ({ interval }) => `Her ${interval} gün`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Her ${interval} haftada şu gün ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Her ${interval} ayda şu gün ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Her ${interval} yıl, şu günü ${days} şu ayın ${months}`,
        position1                       : 'ilk gün',
        position2                       : 'ikinci gün',
        position3                       : 'üçüncü gün',
        position4                       : 'dördüncü gün',
        position5                       : 'beşinci gün',
        'position-1'                    : 'son gün ',
        day                             : 'gün',
        weekday                         : 'hafta içi',
        'weekend day'                   : 'hafta sonu',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Etkinliği tekrarla',
        Cancel              : 'İptal et',
        Save                : 'Kaydet',
        Frequency           : 'Sıklık',
        Every               : 'Her',
        DAILYintervalUnit   : 'gün(ler)de',
        WEEKLYintervalUnit  : 'hafta(lar)da',
        MONTHLYintervalUnit : 'ay(lar)da',
        YEARLYintervalUnit  : 'yıl(lar)da',
        Each                : 'Her',
        'On the'            : 'Şu tarihte',
        'End repeat'        : 'Tekrarlamayı bitir',
        'time(s)'           : 'kere'
    },

    RecurrenceDaysCombo : {
        day           : 'gün',
        weekday       : 'hafta içi',
        'weekend day' : 'hafta sonu'
    },

    RecurrencePositionsCombo : {
        position1    : 'ilk',
        position2    : 'ikinci',
        position3    : 'üçüncü',
        position4    : 'dördüncü',
        position5    : 'beşinci',
        'position-1' : 'son'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Hiçbir zaman',
        After     : 'Sonra',
        'On date' : 'Şu tarihte'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Tekrar yok',
        Daily   : 'Günlük',
        Weekly  : 'Haftalık',
        Monthly : 'Aylık',
        Yearly  : 'Yıllık'
    },

    RecurrenceCombo : {
        None   : 'Hiçbiri',
        Custom : 'Özel...'
    },

    Summary : {
        'Summary for' : date => `Şu tarih için özet ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Tüm planı göster',
        currentview  : 'Görünür plan',
        daterange    : 'Tarih aralığı',
        completedata : 'Tüm planı göster (tüm etkinlikler için)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Plan aralığı',
        'Export from'    : 'Şuradan',
        'Export to'      : 'Buraya'
    },

    ExcelExporter : {
        'No resource assigned' : 'Atanmış kaynak yok'
    },

    CrudManagerView : {
        serverResponseLabel : 'Sunucu yanıtı:'
    },

    DurationColumn : {
        Duration : 'Süre'
    }
};

export default LocaleHelper.publishLocale(locale);
