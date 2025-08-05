import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Fi.js';
import '../../Scheduler/localization/Fi.js';

const locale = {

    localeName : 'Fi',
    localeDesc : 'Suomi',
    localeCode : 'fi',

    ConstraintTypePicker : {
        none                : 'Ei yhtään/mitään',
        assoonaspossible    : 'Mahdollisimman pian',
        aslateaspossible    : 'Mahdollisimman myöhään',
        muststarton         : 'On aloitettava',
        mustfinishon        : 'On lopetettava',
        startnoearlierthan  : 'Aloitus aikaisintaan',
        startnolaterthan    : 'Aloitus viimeistään',
        finishnoearlierthan : 'Lopetus aikaisintaan',
        finishnolaterthan   : 'Lopetus viimeistään'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Eteenpäin',
        Backward      : 'Taaksepäin',
        inheritedFrom : 'Peritty',
        enforcedBy    : 'Pakotettu'
    },

    CalendarField : {
        'Default calendar' : 'Oletuskalenteri'
    },

    TaskEditorBase : {
        Information   : 'Informaatio',
        Save          : 'Tallenna',
        Cancel        : 'Peruuta',
        Delete        : 'Poista',
        calculateMask : 'Laskee...',
        saveError     : 'Ei voida tallentaa, korjaa virheet ensin',
        repeatingInfo : 'Näytetään toistuva tapahtuma',
        editRepeating : 'Muokkaa'
    },

    TaskEdit : {
        'Edit task'            : 'Muokkaa tehtävää',
        ConfirmDeletionTitle   : 'Vahvista poistaminen',
        ConfirmDeletionMessage : 'Haluatko varmasti poistaa tapahtuman?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Yleistä',
        Name         : 'Nimi',
        Resources    : 'Lähteet',
        '% complete' : '% suoritettu',
        Duration     : 'Kesto',
        Start        : 'Alkaa',
        Finish       : 'Loppuun',
        Effort       : 'Effort',
        Preamble     : 'Johdanto',
        Postamble    : 'Jälkisanat'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Yleistä',
        Name         : 'Nimi',
        '% complete' : '% suoritettu',
        Duration     : 'Kesto',
        Start        : 'Aloitus',
        Finish       : 'Lopetus',
        Effort       : 'Effort',
        Dates        : 'Päivämäärät'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Edistynyt',
        Calendar                   : 'Kalenteri',
        'Scheduling mode'          : 'Scheduling mode',
        'Effort driven'            : 'Effort ohjaama',
        'Manually scheduled'       : 'Manuaalisesti aikataulutettu',
        'Constraint type'          : 'Rajoitustyyppi',
        'Constraint date'          : 'Rajoituspäivämäärä',
        Inactive                   : 'Ei-aktiivinen',
        'Ignore resource calendar' : 'Ohita resurssikalenteri'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Edistynyt',
        Calendar                   : 'Kalenteri',
        'Scheduling mode'          : 'Aikataulutustila',
        'Effort driven'            : 'Effort ohjaama',
        'Manually scheduled'       : 'Manuaalisesti aikataulutettu',
        'Constraint type'          : 'Rajoitustyyppi',
        'Constraint date'          : 'Rajoituspäivämäärä',
        Constraint                 : 'Rajoitus',
        Rollup                     : 'Rollup',
        Inactive                   : 'Ei-aktiivinen',
        'Ignore resource calendar' : 'Ohita resurssikalenteri',
        'Scheduling direction'     : 'Aikataulusuunta'
    },

    DependencyTab : {
        Predecessors      : 'Edeltäjät',
        Successors        : 'Seuraajat',
        ID                : 'ID',
        Name              : 'Nimi',
        Type              : 'Tyyppi',
        Lag               : 'Lag',
        cyclicDependency  : 'Syklinen riippuvuus',
        invalidDependency : 'Virheellinen riippuvuus'
    },

    NotesTab : {
        Notes : 'Huomautukset'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Läheteet',
        Resource  : 'Lähde',
        Units     : 'Yksiköt'
    },

    RecurrenceTab : {
        title : 'Toista'
    },

    SchedulingModePicker : {
        Normal           : 'Normaali',
        'Fixed Duration' : 'Kiinteä kesto',
        'Fixed Units'    : 'Kiinteät yksiköt',
        'Fixed Effort'   : 'Kiinteä Vaivaa'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} --sta {available}</span> kohdistettu',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} -sta {available}</span> kohdistettu',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} -sta{available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} -sta{available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : ' {startDate}<br><span class="{cls}">{allocated} -sta {available}</span> kohdistettu:<br>{assignments}',
        plusMore              : '+{value} lisää'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> ',
        barTipOnDate          : '<b>{event}</b>  {startDate}<br><span class="{cls}">{allocated}</span> kohdistettu',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} -sta {available}</span> kohdistettu:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}">{allocated} -sta {available}</span> kohdistettu:<br>{assignments}',
        plusMore              : '+{value} lisää',
        nameColumnText        : 'Lähde/Tapahtuma'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Peruuta muutos, äläkä tee mitään',
        schedulingConflict : 'Aikatauluristiriita',
        emptyCalendar      : 'Kalenterin määritysvirhe',
        cycle              : 'Aikataulusykli',
        Apply              : 'Aseta'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Valitse riippuvuus:',
        invalidDependencyLabel : 'Asiaan liittyy virheellisiä riippuvuuksia, jotka on käsiteltävä:'
    },

    DependencyEdit : {
        Active : 'Aktiivinen'
    },

    SchedulerProBase : {
        propagating     : 'Laskee projektia',
        storePopulation : 'Lataa tietoja',
        finalizing      : 'Viimeistelee tuloksia'
    },

    EventSegments : {
        splitEvent    : 'Jaa tapahtuma osiin',
        renameSegment : 'Nimeä uudelleen'
    },

    NestedEvents : {
        deNestingNotAllowed : 'De-nesting ei allowed',
        nestingNotAllowed   : 'Nesting ei sallita'
    },

    VersionGrid : {
        compare       : 'Vertaa',
        description   : 'Kuvaus',
        occurredAt    : 'Tapahtui',
        rename        : 'Nimeä uudelleen',
        restore       : 'Palauta',
        stopComparing : 'Lopeta vertailu'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tehtävä',
            AssignmentModel : 'toimeksianto',
            DependencyModel : 'linkki',
            ProjectModel    : 'projekti',
            ResourceModel   : 'resurssi',
            other           : 'kohde'
        },
        entityNamesPlural : {
            TaskModel       : 'tehtävät',
            AssignmentModel : 'toimeksiannot',
            DependencyModel : 'linkit',
            ProjectModel    : 'projektit',
            ResourceModel   : 'resurssit',
            other           : 'kohteet'
        },
        transactionDescriptions : {
            update : 'Muutetut {n} {entities}',
            add    : 'Lisätyt {n} {entities}',
            remove : 'Poistetut {n} {entities}',
            move   : 'Siirretyt {n} {entities}',
            mixed  : 'Muutetut {n} {entities}'
        },
        addEntity         : 'Lisätty {type} **{name}**',
        removeEntity      : 'Poistettu {type} **{name}**',
        updateEntity      : 'Muutettu {type} **{name}**',
        moveEntity        : 'Siirretty {type} **{name}** kohdasta {from} kohtaan {to}',
        addDependency     : 'Lisätty linkki kohdasta **{from}** kohtaan **{to}**',
        removeDependency  : 'Poistettu linkki kohdasta **{from}** kohtaan **{to}**',
        updateDependency  : 'Muokattu linkki kohdasta **{from}** kohtaan **{to}**',
        addAssignment     : 'Määritetty **{resource}** tapahtumaan **{event}**',
        removeAssignment  : 'Poistettu toimeksianto **{resource}** tapahtumasta **{event}**',
        updateAssignment  : 'Muokattu toimeksianto **{resource}** tapahtumaan **{event}**',
        noChanges         : 'Ei muutoksia',
        nullValue         : 'ei lainkaan',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Peruutetut muutokset',
        redid             : 'Uudelleen tehdyt muutokset',
        editedTask        : 'Muokatut tehtävän ominaisuudet',
        deletedTask       : 'Tehtävä poistettu',
        movedTask         : 'Tehtävä siirretty',
        movedTasks        : 'Siirretyt tehtävät'
    }
};

export default LocaleHelper.publishLocale(locale);
