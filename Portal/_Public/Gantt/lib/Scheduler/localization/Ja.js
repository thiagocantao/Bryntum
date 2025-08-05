import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Ja.js';

const locale = {

    localeName : 'Ja',
    localeDesc : '日本語',
    localeCode : 'ja',

    Object : {
        newEvent : '新しいイベント'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' イベント'
    },

    Dependencies : {
        from    : 'から',
        to      : 'まで',
        valid   : '有効',
        invalid : '無効'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : '開始 – 開始',
        StartToEnd   : '開始 – 終了',
        EndToStart   : '終了 – 開始',
        EndToEnd     : '終了 – 終了',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            '開始 – 開始',
            '開始 – 終了',
            '終了 – 開始',
            '終了 – 終了'
        ]
    },

    DependencyEdit : {
        From              : 'から',
        To                : 'まで',
        Type              : '種類',
        Lag               : 'ラグ',
        'Edit dependency' : '依存関係を編集する',
        Save              : '保存する',
        Delete            : '削除する',
        Cancel            : '取り消す',
        StartToStart      : '開始 – 開始',
        StartToEnd        : '開始 – 終了',
        EndToStart        : '終了 – 開始',
        EndToEnd          : '終了 – 終了'
    },

    EventEdit : {
        Name         : '名前',
        Resource     : 'リソース',
        Start        : '開始',
        End          : '終了',
        Save         : '保存する',
        Delete       : '削除する',
        Cancel       : '取り消す',
        'Edit event' : 'イベントを編集する',
        Repeat       : '繰り返す'
    },

    EventDrag : {
        eventOverlapsExisting : 'イベントはこのリソースの既存イベントと重複しています',
        noDropOutsideTimeline : 'イベントはタイムラインの外に完全にドロップすることはできません'
    },

    SchedulerBase : {
        'Add event'      : 'イベントを追加する',
        'Delete event'   : 'イベントを削除する',
        'Unassign event' : 'イベントを割り当て解除する',
        color            : '色'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'ズーム',
        activeDateRange : '日付範囲',
        startText       : '開始日',
        endText         : '終了日',
        todayText       : '今日'
    },

    EventCopyPaste : {
        copyEvent  : 'イベントをコピーする',
        cutEvent   : 'イベントを切り取る',
        pasteEvent : 'イベントを貼り付ける'
    },

    EventFilter : {
        filterEvents : 'タスクをフィルターする',
        byName       : '名前'
    },

    TimeRanges : {
        showCurrentTimeLine : '現在のタイムラインを表示する'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : '秒'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd MM月DD日, hA',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd MM月DD日',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : '日'
        },
        day : {
            name : '日/時間'
        },
        week : {
            name : '週/時間'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : '週/日'
        },
        dayAndMonth : {
            name : '月'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : '週'
        },
        weekAndMonth : {
            name : '週'
        },
        weekAndDayLetter : {
            name : '週/平日'
        },
        weekDateAndMonth : {
            name : '月/週'
        },
        monthAndYear : {
            name : '月'
        },
        year : {
            name : '年'
        },
        manyYears : {
            name : '複数の年'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'イベントを削除しようとしています',
        'delete-all-message'        : 'このイベントのすべての出来事を削除しますか？',
        'delete-further-message'    : 'イベントのこの出来事と将来のすべての出来事を削除しますか、それとも選択された出来事のみ削除しますか？',
        'delete-further-btn-text'   : '将来のすべてのイベントを削除する',
        'delete-only-this-btn-text' : 'このイベントのみ削除する',
        'update-title'              : '繰り返しイベントを変更しようとしています',
        'update-all-message'        : 'このイベントのすべての出来事を変更しますか？',
        'update-further-message'    : 'イベントのこの出来事のみ変更しますか、それともこの出来事と将来のすべての出来事を変更しますか？',
        'update-further-btn-text'   : '将来のすべてのイベント',
        'update-only-this-btn-text' : 'このイベントのみ',
        Yes                         : 'はい',
        Cancel                      : '取り消す',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' と ',
        Daily                           : '毎日',
        'Weekly on {1}'                 : ({ days }) => `毎週 ${days}`,
        'Monthly on {1}'                : ({ days }) => `毎月 ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => ` 毎年${months}${days}`,
        'Every {0} days'                : ({ interval }) => `各 ${interval} 日ごと`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `各 ${interval} 週ごとの ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `各 ${interval} カ月ごとの ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `各 ${interval} 年ごとの ${months}${days}`,
        position1                       : '第1',
        position2                       : '第２',
        position3                       : '第３',
        position4                       : '第4',
        position5                       : '第5',
        'position-1'                    : '最後の',
        day                             : '日',
        weekday                         : 'ウィークデイ',
        'weekend day'                   : '週末',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'イベントを繰り返す',
        Cancel              : '取り消す',
        Save                : '保存する',
        Frequency           : '頻度',
        Every               : '各',
        DAILYintervalUnit   : '日',
        WEEKLYintervalUnit  : '週',
        MONTHLYintervalUnit : '月',
        YEARLYintervalUnit  : '年',
        Each                : '各',
        'On the'            : 'の',
        'End repeat'        : '繰り返しを終了する',
        'time(s)'           : '期間'
    },

    RecurrenceDaysCombo : {
        day           : '日',
        weekday       : '平日',
        'weekend day' : '週末'
    },

    RecurrencePositionsCombo : {
        position1    : '第1',
        position2    : '第2',
        position3    : '第3',
        position4    : '第4',
        position5    : '第5',
        'position-1' : '最後の'
    },

    RecurrenceStopConditionCombo : {
        Never     : '決して',
        After     : '後で',
        'On date' : '指定日'
    },

    RecurrenceFrequencyCombo : {
        None    : '繰り返しなし',
        Daily   : '毎日',
        Weekly  : '毎週',
        Monthly : '毎月',
        Yearly  : '毎年'
    },

    RecurrenceCombo : {
        None   : 'なし',
        Custom : 'ユーザー…'
    },

    Summary : {
        'Summary for' : date => ` ${date}の概要`
    },

    ScheduleRangeCombo : {
        completeview : 'スケジュール全体',
        currentview  : '表示中のスケジュール',
        daterange    : '日付範囲',
        completedata : 'スケジュール全体（すべてのイベント用）'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'スケジュール範囲',
        'Export from'    : 'から',
        'Export to'      : 'まで'
    },

    ExcelExporter : {
        'No resource assigned' : 'リソースが割り当てられていません'
    },

    CrudManagerView : {
        serverResponseLabel : 'サーバーの応答:'
    },

    DurationColumn : {
        Duration : '期間'
    }
};

export default LocaleHelper.publishLocale(locale);
