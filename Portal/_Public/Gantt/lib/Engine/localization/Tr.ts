import LocaleHelper from '../../Core/localization/LocaleHelper.js'

const locale = {

    localeName : 'Tr',
    localeDesc : 'Türkçe',
    localeCode : 'tr',

    RemoveDependencyCycleEffectResolution : {
        descriptionTpl : 'Bağımlılığı kaldırın'
    },

    DeactivateDependencyCycleEffectResolution : {
        descriptionTpl : 'Bağımlılığı durdurun'
    },

    CycleEffectDescription : {
        descriptionTpl : 'Bir döngü bulundu, {0} tarafından oluşturuldu'
    },

    EmptyCalendarEffectDescription : {
        descriptionTpl : '"{0}" takvim herhangi bir çalışma zamanı aralığı sağlamıyor.'
    },

    Use24hrsEmptyCalendarEffectResolution : {
        descriptionTpl : 'Cumartesi ve Pazar günleri çalışılmayan, 24 saatlik takvim kullanın.'
    },

    Use8hrsEmptyCalendarEffectResolution : {
        descriptionTpl : 'Cumartesi ve Pazar günleri çalışılmayan, 8 saatlik takvim kullanın (08:00-12:00, 13:00-17:00).'
    },

    ConflictEffectDescription : {
        descriptionTpl : 'Bir programlama çakışması bulundu: {0} {1} ile çakışıyor'
    },

    ConstraintIntervalDescription : {
        dateFormat : 'LLL'
    },

    ProjectConstraintIntervalDescription : {
        startDateDescriptionTpl : 'Proje başlama tarihi {0}',
        endDateDescriptionTpl   : 'Proje bitiş tarihi {0}'
    },

    DependencyType : {
        long : [
            'Başlangıç-Başlangıç',
            'Başlangıç-Bitiş',
            'Bitiş-Başlangıç',
            'Bitiş-Bitiş'
        ]
    },

    ManuallyScheduledParentConstraintIntervalDescription : {
        startDescriptionTpl : 'Manuel planlanmış "{2}" çocuklarının {0}dan daha erken başlamasına zorluyor',
        endDescriptionTpl   : 'Manuel planlanmış "{2}" çocuklarının {1}den daha geç bitirmesine zorluyor'
    },

    DisableManuallyScheduledConflictResolution : {
        descriptionTpl : '"{0}" için manuel planlamayı durdurun'
    },

    DependencyConstraintIntervalDescription : {
        descriptionTpl : 'Bağımlılık ({2}) "{3}"ten "{4}"e'
    },

    RemoveDependencyResolution : {
        descriptionTpl : 'Bağımlılığı "{1}"den "{2}"ye kaldırın'
    },

    DeactivateDependencyResolution : {
        descriptionTpl : 'Bağımlılığı "{1}"den "{2}"ye durdurun'
    },

    DateConstraintIntervalDescription : {
        startDateDescriptionTpl : 'Görev "{2}" {3} {0} kısıt',
        endDateDescriptionTpl   : 'Görev "{2}" {3} {1} kısıt',
        constraintTypeTpl       : {
            startnoearlierthan  : 'En erken şu tarihte başla',
            finishnoearlierthan : 'En erken şu tarihte bitir',
            muststarton         : 'Şu tarihte başlamalı',
            mustfinishon        : 'Şu tarihte bitmeli',
            startnolaterthan    : 'En geç şu tarihte başla',
            finishnolaterthan   : 'En geç şu tarihte bitir'
        }
    },

    RemoveDateConstraintConflictResolution : {
        descriptionTpl : 'Görev "{0}"ın "{1}" kısıtını kaldırın'
    }
}

export default LocaleHelper.publishLocale(locale)
