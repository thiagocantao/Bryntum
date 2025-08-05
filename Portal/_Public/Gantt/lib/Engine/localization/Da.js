import LocaleHelper from '../../Core/localization/LocaleHelper.js';
const locale = {
    localeName: 'Da',
    localeDesc: 'Dansk',
    localeCode: 'da',
    RemoveDependencyCycleEffectResolution: {
        descriptionTpl: 'Fjern afhængighed'
    },
    DeactivateDependencyCycleEffectResolution: {
        descriptionTpl: 'Deaktiver afhængighed'
    },
    CycleEffectDescription: {
        descriptionTpl: 'En cyklus er blevet fundet, dannet af: {0}'
    },
    EmptyCalendarEffectDescription: {
        descriptionTpl: '"{0}" Kalender giver ikke nogen tidstidsintervaller.'
    },
    Use24hrsEmptyCalendarEffectResolution: {
        descriptionTpl: 'Brug 24 timers kalender med ikke-arbejdende lørdage og søndage.'
    },
    Use8hrsEmptyCalendarEffectResolution: {
        descriptionTpl: 'Brug 8 timers kalender (kl. 08.00 til 22.00, 13: 00-17: 00) med ikke-arbejdende lørdage og søndage.'
    },
    ConflictEffectDescription: {
        descriptionTpl: 'Der er fundet en planlægningskonflikt: {0} er modstrid med {1}'
    },
    ConstraintIntervalDescription: {
        dateFormat: 'LLL'
    },
    ProjectConstraintIntervalDescription: {
        startDateDescriptionTpl: 'Projektstartdato {0}',
        endDateDescriptionTpl: 'Projektledning {0}'
    },
    DependencyType: {
        long: [
            'Start-til-Start',
            'Start-til-Slut',
            'Slut-til-Start',
            'Slut-til-Slut'
        ]
    },
    ManuallyScheduledParentConstraintIntervalDescription: {
        startDescriptionTpl: 'Manuelt planlagt "{2}" tvinger sine børn til ikke at starte nr. Tidligere end {0}',
        endDescriptionTpl: 'Manuelt planlagt "{2}" tvinger sine børn til at afslutte senest {1}'
    },
    DisableManuallyScheduledConflictResolution: {
        descriptionTpl: 'Deaktiver manuel planlægning for "{0}"'
    },
    DependencyConstraintIntervalDescription: {
        descriptionTpl: 'Afhængighed ({2}) fra "{3}" til "{4}"'
    },
    RemoveDependencyResolution: {
        descriptionTpl: 'Fjern afhængighed fra "{1}" til "{2}"'
    },
    DeactivateDependencyResolution: {
        descriptionTpl: 'Deaktiver afhængighed fra "{1}" til "{2}"'
    },
    DateConstraintIntervalDescription: {
        startDateDescriptionTpl: 'Opgave "{2}" {3} {0} begrænsning',
        endDateDescriptionTpl: 'Opgave "{2}" {3} {1} begrænsning',
        constraintTypeTpl: {
            startnoearlierthan: 'Start-no-tidligere-end',
            finishnoearlierthan: 'Er færdig-No-tidligere end',
            muststarton: 'Skal begynde',
            mustfinishon: 'Skal man have pålæg på',
            startnolaterthan: 'Start-ikke-sidst-nu',
            finishnolaterthan: 'Færdig-ikke-sidst-nu'
        }
    },
    RemoveDateConstraintConflictResolution: {
        descriptionTpl: 'Fjern begrænsningen "{1}" for opgaven "{0}"'
    }
};
export default LocaleHelper.publishLocale(locale);
