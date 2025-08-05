/*

Ext Gantt 2.2.22
Copyright(c) 2009-2014 Bryntum AB
http://bryntum.com/contact
http://bryntum.com/license

*/
/**
 * Dutch translations for the Scheduler component
 *
 * NOTE: To change locale for month/day names you have to use the corresponding Ext JS language file.
 */
Ext.define('Sch.locale.Nl', {
    extend      : 'Sch.locale.Locale',
    singleton   : true,

    l10n        : {
        'Sch.util.Date' : {
            unitNames : {
                YEAR        : { single : 'jaar',    plural : 'jaren',   abbrev : 'j' },
                QUARTER     : { single : 'kwartaal', plural : 'kwartalen',abbrev : 'kw' },
                MONTH       : { single : 'maand',   plural : 'maanden',  abbrev : 'm' },
                WEEK        : { single : 'week',    plural : 'weken',   abbrev : 'w' },
                DAY         : { single : 'dag',     plural : 'dagen',    abbrev : 'd' },
                HOUR        : { single : 'uur',    plural : 'uren',   abbrev : 'u' },
                MINUTE      : { single : 'minuut',  plural : 'minuten', abbrev : 'm' },
                SECOND      : { single : 'seconde',  plural : 'seconden', abbrev : 's' },
                MILLI       : { single : 'ms',      plural : 'ms',      abbrev : 'ms' }
            }
        },

        'Sch.view.SchedulerGridView' : {
            loadingText : 'Events laden...'
        },

        'Sch.plugin.CurrentTimeLine' : {
            tooltipText : 'Huidige tijd'
        },

        'Sch.plugin.EventEditor' : {
            saveText    : 'Opslaan',
            deleteText  : 'Verwijderen',
            cancelText  : 'Annuleer'
        },

        'Sch.plugin.SimpleEditor' : {
            newEventText    : 'Nieuwe boeking...'
        },

        'Sch.widget.ExportDialog' : {
            generalError                : 'Er is een fout opgetreden, probeer nogmaals.',
            title                       : 'Export instellingen',
            formatFieldLabel            : 'Papier formaat',
            orientationFieldLabel       : 'OriÃ«ntatatie',
            rangeFieldLabel             : 'Export bereik',
            showHeaderLabel             : 'Toevoegen paginanummer',
            orientationPortraitText     : 'Portret',
            orientationLandscapeText    : 'Landschap',
            completeViewText            : 'Compleet schema',
            currentViewText             : 'Huidige weergave',
            dateRangeText               : 'Periode',
            dateRangeFromText           : 'Exporteer vanaf',
            pickerText                  : 'Wijzig formaat van kolommen en/of rijen',
            dateRangeToText             : 'Exporteer naar',
            exportButtonText            : 'Exporteer',
            cancelButtonText            : 'Annuleer',
            progressBarText             : 'Bezig met exporteren...',
            exportToSingleLabel         : 'Exporteer als enkele pagina',
            adjustCols                  : 'Wijzig kolom breedte',
            adjustColsAndRows           : 'Wijzig kolom breedte en rij hoogte',
            specifyDateRange            : 'Specificeer periode'
        },

        // -------------- View preset date formats/strings -------------------------------------
        'Sch.preset.Manager' : function () {
            var M = Sch.preset.Manager,
                vp = M.getPreset("hourAndDay");

            if (vp) {
                vp.displayDateFormat = 'G:i';
                vp.headerConfig.middle.dateFormat = 'G:i';
                vp.headerConfig.top.dateFormat = 'd-m-Y';
            }

            vp = M.getPreset("secondAndMinute");
            if (vp) {
                vp.displayDateFormat = 'G:i:s';
                vp.headerConfig.top.dateFormat = 'D, d G:i';
            }


            vp = M.getPreset("dayAndWeek");
            if (vp) {
                vp.displayDateFormat = 'd-m H:i';
                vp.headerConfig.middle.dateFormat = 'D d M';
            }

            vp = M.getPreset("weekAndDay");
            if (vp) {
                vp.displayDateFormat = 'd-m';
                vp.headerConfig.bottom.dateFormat = 'd M';
                vp.headerConfig.middle.dateFormat = 'j-F Y';
            }

            vp = M.getPreset("weekAndMonth");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'd-m';
                vp.headerConfig.top.dateFormat = 'd-m-Y';
            }

            vp = M.getPreset("weekAndDayLetter");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'D j M Y';
            }

            vp = M.getPreset("weekDateAndMonth");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'd';
                vp.headerConfig.top.dateFormat = 'F Y';
            }

            vp = M.getPreset("monthAndYear");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'M Y';
                vp.headerConfig.top.dateFormat = 'Y';
            }

            vp = M.getPreset("year");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'Y';
            }

            vp = M.getPreset("manyYears");
            if (vp) {
                vp.displayDateFormat = 'd-m-Y';
                vp.headerConfig.middle.dateFormat = 'Y';
            }
        }
    }
});
