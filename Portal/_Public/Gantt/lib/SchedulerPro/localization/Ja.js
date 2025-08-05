import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ja.js';
import '../../Scheduler/localization/Ja.js';

const locale = {

    localeName : 'Ja',
    localeDesc : '日本語',
    localeCode : 'ja',

    ConstraintTypePicker : {
        none                : 'なし',
        assoonaspossible    : 'できるだけ早く',
        aslateaspossible    : 'できるだけ遅く',
        muststarton         : '指定日に開始',
        mustfinishon        : '指定日に終了',
        startnoearlierthan  : '指定日以降に開始',
        startnolaterthan    : '指定日までに開始',
        finishnoearlierthan : '指定日以降に終了',
        finishnolaterthan   : '指定日までに終了'
    },

    SchedulingDirectionPicker : {
        Forward       : '前進',
        Backward      : '後退',
        inheritedFrom : '継承された',
        enforcedBy    : '強制された'
    },

    CalendarField : {
        'Default calendar' : '既定の予定表'
    },

    TaskEditorBase : {
        Information   : '情報',
        Save          : '保存する',
        Cancel        : '取り消す',
        Delete        : '削除する',
        calculateMask : '計算中',
        saveError     : '保存できません。まずエラーを修正してください',
        repeatingInfo : '繰り返しイベントを表示',
        editRepeating : '編集する'
    },

    TaskEdit : {
        'Edit task'            : 'タスクを編集する',
        ConfirmDeletionTitle   : '削除を確認する',
        ConfirmDeletionMessage : 'イベントを本当に削除しますか？'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : '全般',
        Name         : '名前',
        Resources    : 'リソース',
        '% complete' : '％完了',
        Duration     : '期間',
        Start        : '開始',
        Finish       : '終了',
        Effort       : '工数',
        Preamble     : 'プリアンブル',
        Postamble    : 'ポストアンブル'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : '全般',
        Name         : '名前',
        '% complete' : '％完了',
        Duration     : '期間',
        Start        : '開始',
        Finish       : '終了',
        Effort       : '工数',
        Dates        : '日付'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'アドバンスト',
        Calendar                   : '予定表',
        'Scheduling mode'          : 'スケジュールモード',
        'Effort driven'            : '存作業の優先',
        'Manually scheduled'       : 'スケジュール',
        'Constraint type'          : '制約タイプ',
        'Constraint date'          : '制約日',
        Inactive                   : '非アクティブ',
        'Ignore resource calendar' : 'リソースカレンダーを無視する'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'アドバンスト',
        Calendar                   : '予定表',
        'Scheduling mode'          : 'スケジュールモード',
        'Effort driven'            : '存作業の優先',
        'Manually scheduled'       : '手動スケジュール',
        'Constraint type'          : '制約タイプ',
        'Constraint date'          : '制約日',
        Constraint                 : '制約',
        Rollup                     : 'ロールアップ',
        Inactive                   : '非アクティブ',
        'Ignore resource calendar' : 'リソースカレンダーを無視する',
        'Scheduling direction'     : 'スケジュールの方向'
    },

    DependencyTab : {
        Predecessors      : '先行タスク',
        Successors        : '後続タスク',
        ID                : 'ID',
        Name              : '名前',
        Type              : '種類',
        Lag               : 'ラグ',
        cyclicDependency  : '循環依存関係',
        invalidDependency : '無効な依存関係'
    },

    NotesTab : {
        Notes : '備考'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'リソース',
        Resource  : 'リソース',
        Units     : '単位'
    },

    RecurrenceTab : {
        title : '繰り返す'
    },

    SchedulingModePicker : {
        Normal           : '普通',
        'Fixed Duration' : '期間固定',
        'Fixed Units'    : '単位数固定',
        'Fixed Effort'   : '工数固定'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} の {available}</span> 割り当て済',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} の {available}</span> 割り当て済',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} の {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} の {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'に {startDate}<br><span class="{cls}">{allocated} の {available}</span> 割り当て済:<br>{assignments}',
        plusMore              : '+{value} 増'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> 割り当て済',
        barTipOnDate          : '<b>{event}</b> に {startDate}<br><span class="{cls}">{allocated}</span> 割り当て済',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} の {available}</span> 割り当て済:<br>{assignments}',
        groupBarTipOnDate     : 'に {startDate}<br><span class="{cls}">{allocated} の {available}</span> 割り当て済:<br>{assignments}',
        plusMore              : '+{value} 増',
        nameColumnText        : 'リソース／イベント'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : '変更を取り消し、何もしない',
        schedulingConflict : 'スケジュール上の矛盾',
        emptyCalendar      : '予定表設定のエラー',
        cycle              : 'スケジュールサイクル',
        Apply              : '適用する'
    },

    CycleResolutionPopup : {
        dependencyLabel        : '依存関係を選択してください:',
        invalidDependencyLabel : '対処すべき無効な依存関係があります:'
    },

    DependencyEdit : {
        Active : 'アクティブ'
    },

    SchedulerProBase : {
        propagating     : 'プロジェクト計算中',
        storePopulation : 'データ読み込み中',
        finalizing      : '確定中'
    },

    EventSegments : {
        splitEvent    : 'イベントを分割する',
        renameSegment : '名前を変更する'
    },

    NestedEvents : {
        deNestingNotAllowed : '入れ子の解除はできません',
        nestingNotAllowed   : '入れ子はできません'
    },

    VersionGrid : {
        compare       : '比較',
        description   : '説明',
        occurredAt    : '発生日時',
        rename        : '名前を変更',
        restore       : '復元する',
        stopComparing : '比較を停止する'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'タスク',
            AssignmentModel : '割り当て',
            DependencyModel : 'リンク',
            ProjectModel    : 'プロジェクト',
            ResourceModel   : 'リソース',
            other           : '対象'
        },
        entityNamesPlural : {
            TaskModel       : '複数のタスク',
            AssignmentModel : '複数の割り当て',
            DependencyModel : '複数のリンク',
            ProjectModel    : '複数のプロジェクト',
            ResourceModel   : '複数のリソース',
            other           : '複数の対象'
        },
        transactionDescriptions : {
            update : ' {n} {entities}を変更した',
            add    : ' {n} {entities}を追加した',
            remove : ' {n} {entities}を削除した',
            move   : ' {n} {entities}を移動した',
            mixed  : ' {n} {entities}を変更した'
        },
        addEntity         : '{type} **{name}**を追加した',
        removeEntity      : '{type} **{name}**を削除した',
        updateEntity      : '{type} **{name}**を変更した',
        moveEntity        : '{from} から {to}　へ{type} **{name}**を移動した',
        addDependency     : '**{from}** から **{to}**　へのリンクを追加した',
        removeDependency  : '**{from}** から **{to}**　へのリンクを削除した',
        updateDependency  : '**{from}** から **{to}**　へのリンクを編集した',
        addAssignment     : '**{resource}** を **{event}**　に割り当てた',
        removeAssignment  : '**{event}**　から　**{resource}**　の割り当てを削除した',
        updateAssignment  : '**{event}**　へ　**{resource}**　の割り当てを編集した',
        noChanges         : '変更なし',
        nullValue         : 'なし',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : '変更を取り消す',
        redid             : '変更をやり直す',
        editedTask        : 'タスクプロパティを編集した',
        deletedTask       : 'ひとつのタスクを削除した',
        movedTask         : 'ひとつのタスクを移動した',
        movedTasks        : '複数のタスクを移動した'
    }
};

export default LocaleHelper.publishLocale(locale);
