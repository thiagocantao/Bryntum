import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Fi.js';

const locale = {

    localeName : 'Fi',
    localeDesc : 'Suomi',
    localeCode : 'fi',

    Object : {
        newEvent : 'Uusi tapahtuma'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' tapahtuma' + (data !== 1 ? 't' : '')
    },

    Dependencies : {
        from    : 'From',
        to      : 'To',
        valid   : 'Voimassa',
        invalid : 'Virheellinen'
    },

    DependencyType : {
        SS           : 'SS',
        SF           : 'SF',
        FS           : 'FS',
        FF           : 'FF',
        StartToStart : 'Start-to-Start',
        StartToEnd   : 'Start-to-Finish',
        EndToStart   : 'Finish-to-Start',
        EndToEnd     : 'Finish-to-Finish',
        short        : [
            'SS',
            'SF',
            'FS',
            'FF'
        ],
        long : [
            'Start-to-Start',
            'Start-to-Finish',
            'Finish-to-Start',
            'Finish-to-Finish'
        ]
    },

    DependencyEdit : {
        From              : 'Lähettäjä',
        To                : 'Vastaanottaja',
        Type              : 'Tyyppi',
        Lag               : 'Lag',
        'Edit dependency' : 'Muokkaa riippuvuutta',
        Save              : 'Tallenna',
        Delete            : 'Poista',
        Cancel            : 'Peruuta',
        StartToStart      : 'alusta alkuun',
        StartToEnd        : 'alusta loppuun',
        EndToStart        : 'maalista alkuun',
        EndToEnd          : 'maalista maaliin'
    },

    EventEdit : {
        Name         : 'Nimi',
        Resource     : 'Lähde',
        Start        : 'Start',
        End          : 'Loppuun',
        Save         : 'Tallenna',
        Delete       : 'Poista',
        Cancel       : 'Peruuta',
        'Edit event' : 'Muokkaa tapahtumaa',
        Repeat       : 'Toista'
    },

    EventDrag : {
        eventOverlapsExisting : 'Tapahtuma on päällekkäinen tämän resurssin olemassa olevan tapahtuman kanssa',
        noDropOutsideTimeline : 'Tapahtumaa ei saa pudottaa kokonaan aikajanan ulkopuolelle'
    },

    SchedulerBase : {
        'Add event'      : 'Lisää tapahtuma',
        'Delete event'   : 'Poista tapahtuma',
        'Unassign event' : 'Poista tapahtuma',
        color            : 'Väri'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'Zoom',
        activeDateRange : 'Päiväysalue',
        startText       : 'Aloituspäivä',
        endText         : 'Lopetuspäivä',
        todayText       : 'Tänään'
    },

    EventCopyPaste : {
        copyEvent  : 'Kopioi tapahtuma',
        cutEvent   : 'Leikkaa tapahtuma',
        pasteEvent : 'Liitä tapahtuma'
    },

    EventFilter : {
        filterEvents : 'Suodata tehtävät',
        byName       : 'nimien mukaan'
    },

    TimeRanges : {
        showCurrentTimeLine : 'Näytä nykyinen aikajana'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'Sekuntia'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD.MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD.MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'Päivä'
        },
        day : {
            name : 'Päivä/tunnit'
        },
        week : {
            name : 'Viikko/tunnit'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'Viikko/päivät'
        },
        dayAndMonth : {
            name : 'Kuukausi'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'Viikko'
        },
        weekAndMonth : {
            name : 'Viikkoa'
        },
        weekAndDayLetter : {
            name : 'Viikot/viikonpäivät'
        },
        weekDateAndMonth : {
            name : 'Kuukaudet/viikot'
        },
        monthAndYear : {
            name : 'Kuukaudet'
        },
        year : {
            name : 'Vuodet'
        },
        manyYears : {
            name : 'Useita vuosia'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'Olet poistamassa tapahtumaa',
        'delete-all-message'        : 'Haluatko poistaa tämän tapahtuman kaikki merkinnät?',
        'delete-further-message'    : 'Haluatko poistaa tämän tapahtuman ja kaikki tulevat tapahtumat vai vain valitun tapahtuman?',
        'delete-further-btn-text'   : 'Poista kaikki tulevaisuuden tapahtumat',
        'delete-only-this-btn-text' : 'Poista vain tämä tapahtuma',
        'update-title'              : 'Olet muuttamassa toistuvaa tapahtumaa',
        'update-all-message'        : 'Haluatko muuttaa tämän tapahtuman kaikki merkinnät?',
        'update-further-message'    : 'Haluatko muuttaa vain tämän tapahtuman vai tämän ja kaikki tulevat tapahtumat?',
        'update-further-btn-text'   : 'Kaikki tulevaisuuden tapahtumat',
        'update-only-this-btn-text' : 'Vain tämä tapahtuma',
        Yes                         : 'Kyllä',
        Cancel                      : 'Peruuta',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' ja ',
        Daily                           : 'Päivittäin',
        'Weekly on {1}'                 : ({ days }) => `Viikoittain ${days}`,
        'Monthly on {1}'                : ({ days }) => `Kuukausittain ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `Vuosittain ${days} of ${months}`,
        'Every {0} days'                : ({ interval }) => `Joka ${interval} days`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `Joka ${interval} viikko ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `Joka ${interval} kuukausi on ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `Joka ${interval} vuosi ${days} of ${months}`,
        position1                       : 'the ensimmäinen',
        position2                       : 'toinen',
        position3                       : 'kolmas',
        position4                       : 'neljäs',
        position5                       : 'viides',
        'position-1'                    : 'the last',
        day                             : 'päivä',
        weekday                         : 'viikonpäivä',
        'weekend day'                   : 'viikonlopun päivä',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'Toista tapahtuma',
        Cancel              : 'Peruuta',
        Save                : 'Tallenna',
        Frequency           : 'Taajuus',
        Every               : 'Joka',
        DAILYintervalUnit   : 'day(s)',
        WEEKLYintervalUnit  : 'viikkoa',
        MONTHLYintervalUnit : 'kuukautta',
        YEARLYintervalUnit  : 'vuotta',
        Each                : 'kukin',
        'On the'            : 'Päivänä',
        'End repeat'        : 'Loppu toisto',
        'time(s)'           : 'ajastin(s)'
    },

    RecurrenceDaysCombo : {
        day           : 'päivä',
        weekday       : 'viikonpäivä',
        'weekend day' : 'viikonlopun päivä'
    },

    RecurrencePositionsCombo : {
        position1    : 'ensimmäinen',
        position2    : 'toinen',
        position3    : 'kolmas',
        position4    : 'neljäs',
        position5    : 'viides',
        'position-1' : 'viimeinen'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'Ei koskaan',
        After     : 'Jälkeen',
        'On date' : 'Päivänä'
    },

    RecurrenceFrequencyCombo : {
        None    : 'Ei toistoa',
        Daily   : 'Päivittäin',
        Weekly  : 'Viikoittain',
        Monthly : 'Kuukausittain',
        Yearly  : 'Vuosittain'
    },

    RecurrenceCombo : {
        None   : 'Ei ole',
        Custom : 'Kustomoitu...'
    },

    Summary : {
        'Summary for' : date => `Yhteenve ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'Täydellinen aikataulu',
        currentview  : 'Näkyvä aikataulu',
        daterange    : 'Päivämääräalue',
        completedata : 'Täydellinen aikataulu (kaikkien tapahtumien osalta)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'Aikataulualue',
        'Export from'    : 'Lähettäjä',
        'Export to'      : 'Vastaanottaja'
    },

    ExcelExporter : {
        'No resource assigned' : 'Resurssia ei määritetty'
    },

    CrudManagerView : {
        serverResponseLabel : 'Vastaus palvelimelta:'
    },

    DurationColumn : {
        Duration : 'Kesto'
    }
};

export default LocaleHelper.publishLocale(locale);
