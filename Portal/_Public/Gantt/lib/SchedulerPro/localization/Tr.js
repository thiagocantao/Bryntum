import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Tr.js';
import '../../Scheduler/localization/Tr.js';

const locale = {

    localeName : 'Tr',
    localeDesc : 'Türkçe',
    localeCode : 'tr',

    ConstraintTypePicker : {
        none                : 'Hiçbiri',
        assoonaspossible    : 'Mümkün olan en kısa sürede',
        aslateaspossible    : 'Mümkün olan en geç',
        muststarton         : 'Şu tarihte başlamalı',
        mustfinishon        : 'Şu tarihte bitmeli',
        startnoearlierthan  : 'En erken şu tarihte başla',
        startnolaterthan    : 'En geç şu tarihte başla',
        finishnoearlierthan : 'En erken şu tarihte bitir',
        finishnolaterthan   : 'En geç şu tarihte bitir'
    },

    SchedulingDirectionPicker : {
        Forward       : 'İleri',
        Backward      : 'Geri',
        inheritedFrom : 'Miras alınan',
        enforcedBy    : 'Zorunlu olarak'
    },

    CalendarField : {
        'Default calendar' : 'Varsayılan takvim'
    },

    TaskEditorBase : {
        Information   : 'Bilgi',
        Save          : 'Kaydet',
        Cancel        : 'İptal et',
        Delete        : 'Sil',
        calculateMask : 'Hesaplanıyor...',
        saveError     : 'Kaydedilemez, önce lütfen hataları düzeltin',
        repeatingInfo : 'Tekrarlanan bir olayı görüntüleme',
        editRepeating : 'Düzenle'
    },

    TaskEdit : {
        'Edit task'            : 'Görevi düzenle',
        ConfirmDeletionTitle   : 'Silmeyi onayla',
        ConfirmDeletionMessage : 'Etkinliği silmek istediğinizden emin misiniz?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Genel',
        Name         : 'Ad',
        Resources    : 'Kaynaklar',
        '% complete' : '% tamamlandı',
        Duration     : 'Süre',
        Start        : 'Başlangıç',
        Finish       : 'Bitiş',
        Effort       : 'Harcanacak çaba',
        Preamble     : 'Giriş',
        Postamble    : 'Çıkış'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Genel',
        Name         : 'Ad',
        '% complete' : '% tamamlandı',
        Duration     : 'Süre',
        Start        : 'Başlangıç',
        Finish       : 'Bitiş',
        Effort       : 'Harcanacak çaba',
        Dates        : 'Tarihler'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Gelişmiş',
        Calendar                   : 'Takvim',
        'Scheduling mode'          : 'Planlama modu',
        'Effort driven'            : 'Sarfedilen efor temelli',
        'Manually scheduled'       : 'Manuel planlanmış',
        'Constraint type'          : 'Kısıtlama türü',
        'Constraint date'          : 'Kısıtlama tarihi',
        Inactive                   : 'Etkin olmayan',
        'Ignore resource calendar' : 'Kaynak takvimi yoksay'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Gelişmiş',
        Calendar                   : 'Takvim',
        'Scheduling mode'          : 'Planlama modu',
        'Effort driven'            : 'Sarfedilen efor temelli',
        'Manually scheduled'       : 'Manuel planlanmış',
        'Constraint type'          : 'Kısıtlama türü',
        'Constraint date'          : 'Kısıtlama tarihi',
        Constraint                 : 'Kısıtlama',
        Rollup                     : 'Toplama',
        Inactive                   : 'Etkin olmayan',
        'Ignore resource calendar' : 'Kaynak takvimi yoksay',
        'Scheduling direction'     : 'Planlama yönü'
    },

    DependencyTab : {
        Predecessors      : 'Öncüller',
        Successors        : 'Ardıllar',
        ID                : 'Kimlik',
        Name              : 'Ad',
        Type              : 'Tür',
        Lag               : 'Gecikme',
        cyclicDependency  : 'Döngüsel bağımlılık',
        invalidDependency : 'Geçersiz bağımlılık'
    },

    NotesTab : {
        Notes : 'Notlar'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Kaynaklar',
        Resource  : 'Kaynak',
        Units     : 'Birimler'
    },

    RecurrenceTab : {
        title : 'Yenile'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Sabit Süre',
        'Fixed Units'    : 'Sabit Birimler',
        'Fixed Effort'   : 'Sarfedilen Sabit Efor'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} -den {available}</span> ayrıldı',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} -den {available}</span> ayrıldı',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} -den {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} -den {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Şu gün {startDate}<br><span class="{cls}">{allocated} -den {available}</span> ayırldı:<br>{assignments}',
        plusMore              : '+{value} daha fazla'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> ayrıldı',
        barTipOnDate          : '<b>{event}</b> şu gün {startDate}<br><span class="{cls}">{allocated}</span> ayrıldı',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} -den {available}</span> ayrıldı:<br>{assignments}',
        groupBarTipOnDate     : 'Şu gün {startDate}<br><span class="{cls}">{allocated} -den {available}</span> ayrıldı:<br>{assignments}',
        plusMore              : '+{value} daha fazla',
        nameColumnText        : 'Kaynak / Etkinlik'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Değişikliği iptal et ve hiçbir şey yapma',
        schedulingConflict : 'Zamanlama çakışması',
        emptyCalendar      : 'Takvim yapılandırma hatası',
        cycle              : 'Planlama döngüsü',
        Apply              : 'Uygula'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Lütfen bir bağımlılık seçin:',
        invalidDependencyLabel : 'İlgilenilmesi gereken geçersiz bağımlılıklar mevcut:'
    },

    DependencyEdit : {
        Active : 'Etkin'
    },

    SchedulerProBase : {
        propagating     : 'Proje hesaplanıyor',
        storePopulation : 'Veri yükleniyor',
        finalizing      : 'Sonuçlar tamamlanıyor'
    },

    EventSegments : {
        splitEvent    : 'Olayı böl',
        renameSegment : 'Yeniden adlandır'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Ayırmaya izin verilmiyor',
        nestingNotAllowed   : 'İç içe yerleştirmeye izin verilmiyor'
    },

    VersionGrid : {
        compare       : 'Karşılaştır',
        description   : 'Açıklama',
        occurredAt    : 'Meydana geldiği tarih',
        rename        : 'Yeniden adlandır',
        restore       : 'Geri yükle',
        stopComparing : 'Karşılaştırmayı durdur'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'görev',
            AssignmentModel : 'görevlendirme',
            DependencyModel : 'bağlantı',
            ProjectModel    : 'proje',
            ResourceModel   : 'kaynak',
            other           : 'nesne'
        },
        entityNamesPlural : {
            TaskModel       : 'görevler',
            AssignmentModel : 'görevlendirmeler',
            DependencyModel : 'bağlantılar',
            ProjectModel    : 'projeler',
            ResourceModel   : 'kaynaklar',
            other           : 'nesneler'
        },
        transactionDescriptions : {
            update : 'Değiştirildi {n} {entities}',
            add    : 'Eklendi {n} {entities}',
            remove : 'Kaldırıldı {n} {entities}',
            move   : 'Taşındı {n} {entities}',
            mixed  : 'Değiştirildi {n} {entities}'
        },
        addEntity         : 'Eklendi {type} **{name}**',
        removeEntity      : 'Kaldırıldı {type} **{name}**',
        updateEntity      : 'Değiştirildi {type} **{name}**',
        moveEntity        : 'Taşındı {type} **{name}** from {from} buraya {to}',
        addDependency     : 'Bağlantı buradan eklendi **{from}** buraya **{to}**',
        removeDependency  : 'Bağlantı buradan kaldırıldı **{from}** buraya **{to}**',
        updateDependency  : 'Bağlantı buradan düzenlendi **{from}** buraya **{to}**',
        addAssignment     : 'Atandı **{resource}** buraya **{event}**',
        removeAssignment  : 'Ataması kaldırıldı **{resource}** buraya **{event}**',
        updateAssignment  : 'Ataması düzenlendi **{resource}** buraya **{event}**',
        noChanges         : 'Değişiklik yok',
        nullValue         : 'yok',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Değişiklikleri geri aldı',
        redid             : 'Değişiklikleri yendien yaptı',
        editedTask        : 'Görev özelliklerini düzenledi',
        deletedTask       : 'Bir görevi sildi',
        movedTask         : 'Bir görevi taşıdı',
        movedTasks        : 'Görevler taşındı'
    }
};

export default LocaleHelper.publishLocale(locale);
