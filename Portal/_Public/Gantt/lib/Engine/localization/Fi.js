import LocaleHelper from '../../Core/localization/LocaleHelper.js';
const locale = {
    localeName: 'Fi',
    localeDesc: 'Suomi',
    localeCode: 'fi',
    RemoveDependencyCycleEffectResolution: {
        descriptionTpl: 'Poista riippuvuus'
    },
    DeactivateDependencyCycleEffectResolution: {
        descriptionTpl: 'Poista riippuvuus käytöstä'
    },
    CycleEffectDescription: {
        descriptionTpl: '{0} muodostama sykli on löytynyt'
    },
    EmptyCalendarEffectDescription: {
        descriptionTpl: '"{0}" kalenterissa ei ole yhtään työaikaintervallia.'
    },
    Use24hrsEmptyCalendarEffectResolution: {
        descriptionTpl: 'Käytä 24 tunnin kalenteria, joissa Lauantait ja Sunnuntait eivät ole työpäiviä.'
    },
    Use8hrsEmptyCalendarEffectResolution: {
        descriptionTpl: 'Käytä 8 tunnin kalenteria (08:00-12:00, 13:00-17:00) jossa Lauantait ja sunnuntait eivät ole työpäiviä.'
    },
    ConflictEffectDescription: {
        descriptionTpl: 'Aikataulukonflikti on löytynyt: {0} on ristiriidassa {1} kanssa'
    },
    ConstraintIntervalDescription: {
        dateFormat: 'LLL'
    },
    ProjectConstraintIntervalDescription: {
        startDateDescriptionTpl: 'Projektin aloituspäivämäärä {0}',
        endDateDescriptionTpl: 'Projektin lopetuspäivämäärä {0}'
    },
    DependencyType: {
        long: [
            'alusta alkuun',
            'alusta loppuun',
            'maalista alkuun',
            'maalista maaliin'
        ]
    },
    ManuallyScheduledParentConstraintIntervalDescription: {
        startDescriptionTpl: 'Aikataulutettu manuaalisesti "{2}" pakottaa lapsensa aloittamaan aikaisintaan {0}',
        endDescriptionTpl: 'Aikataulutettu manuaalisesti "{2}" pakottaa lapsenta lopettamaan aikaisintaan {1}'
    },
    DisableManuallyScheduledConflictResolution: {
        descriptionTpl: 'Poista manuaalinen aikatauluttaminen käytöstä käyttäjälle "{0}"'
    },
    DependencyConstraintIntervalDescription: {
        descriptionTpl: 'Riippuvuus ({2}) välillä "{3}" ja "{4}"'
    },
    RemoveDependencyResolution: {
        descriptionTpl: 'Poista riippuvuus välillä "{1}" ja "{2}"'
    },
    DeactivateDependencyResolution: {
        descriptionTpl: 'Kytke pois päältä riippuvuus välillä "{1}" ja "{2}"'
    },
    DateConstraintIntervalDescription: {
        startDateDescriptionTpl: 'Tehtävä "{2}" {3} {0} rajoitus',
        endDateDescriptionTpl: 'Tehtävä "{2}" {3} {1} rajoitus',
        constraintTypeTpl: {
            startnoearlierthan: 'Aloitus aikaisintaan',
            finishnoearlierthan: 'Lopetus aikaisintaan',
            muststarton: 'On aloitettava',
            mustfinishon: 'On lopetettava',
            startnolaterthan: 'Aloitus viimeistään',
            finishnolaterthan: 'Lopetus viimeistään'
        }
    },
    RemoveDateConstraintConflictResolution: {
        descriptionTpl: 'Poista "{1}" rajoitus tehtävästä "{0}"'
    }
};
export default LocaleHelper.publishLocale(locale);
