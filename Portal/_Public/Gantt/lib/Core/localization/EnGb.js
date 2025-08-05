import LocaleHelper from '../../Core/localization/LocaleHelper.js';

const locale = {

    localeName : 'EnGb',
    localeDesc : 'English (GB)',
    localeCode : 'en-GB',

    Object : {
        Yes    : 'Yes',
        No     : 'No',
        Cancel : 'Cancel',
        Ok     : 'OK',
        Week   : 'Week'
    },

    ColorPicker : {
        noColor : 'No colour'
    },

    Combo : {
        noResults          : 'No results',
        recordNotCommitted : 'Record could not be added',
        addNewValue        : value => `Add ${value}`
    },

    FilePicker : {
        file : 'File'
    },

    Field : {
        badInput              : 'Invalid field value',
        patternMismatch       : 'Value should match a specific pattern',
        rangeOverflow         : value => `Value must be less than or equal to ${value.max}`,
        rangeUnderflow        : value => `Value must be greater than or equal to ${value.min}`,
        stepMismatch          : 'Value should fit the step',
        tooLong               : 'Value should be shorter',
        tooShort              : 'Value should be longer',
        typeMismatch          : 'Value is required to be in a special format',
        valueMissing          : 'This field is required',
        invalidValue          : 'Invalid field value',
        minimumValueViolation : 'Minimum value violation',
        maximumValueViolation : 'Maximum value violation',
        fieldRequired         : 'This field is required',
        validateFilter        : 'Value must be selected from the list'
    },

    DateField : {
        invalidDate : 'Invalid date input'
    },

    DatePicker : {
        gotoPrevYear  : 'Go to previous year',
        gotoPrevMonth : 'Go to previous month',
        gotoNextMonth : 'Go to next month',
        gotoNextYear  : 'Go to next year'
    },

    NumberFormat : {
        locale   : 'en-GB',
        currency : 'GBP'
    },

    DurationField : {
        invalidUnit : 'Invalid unit'
    },

    TimeField : {
        invalidTime : 'Invalid time input'
    },

    TimePicker : {
        hour   : 'Hour',
        minute : 'Minute',
        second : 'Second'
    },

    List : {
        loading   : 'Loading...',
        selectAll : 'Select All'
    },

    GridBase : {
        loadMask : 'Loading...',
        syncMask : 'Saving changes, please wait...'
    },

    PagingToolbar : {
        firstPage         : 'Go to first page',
        prevPage          : 'Go to previous page',
        page              : 'Page',
        nextPage          : 'Go to next page',
        lastPage          : 'Go to last page',
        reload            : 'Reload current page',
        noRecords         : 'No records to display',
        pageCountTemplate : data => `of ${data.lastPage}`,
        summaryTemplate   : data => `Displaying records ${data.start} - ${data.end} of ${data.allCount}`
    },

    PanelCollapser : {
        Collapse : 'Collapse',
        Expand   : 'Expand'
    },

    Popup : {
        close : 'Close Popup'
    },

    UndoRedo : {
        Undo           : 'Undo',
        Redo           : 'Redo',
        UndoLastAction : 'Undo last action',
        RedoLastAction : 'Redo last undone action',
        NoActions      : 'No items in the undo queue'
    },

    FieldFilterPicker : {
        equals                 : 'equals',
        doesNotEqual           : 'does not equal',
        isEmpty                : 'is empty',
        isNotEmpty             : 'is not empty',
        contains               : 'contains',
        doesNotContain         : 'does not contain',
        startsWith             : 'starts with',
        endsWith               : 'ends with',
        isOneOf                : 'is one of',
        isNotOneOf             : 'is not one of',
        isGreaterThan          : 'is greater than',
        isLessThan             : 'is less than',
        isGreaterThanOrEqualTo : 'is greater than or equal to',
        isLessThanOrEqualTo    : 'is less than or equal to',
        isBetween              : 'is between',
        isNotBetween           : 'is not between',
        isBefore               : 'is before',
        isAfter                : 'is after',
        isToday                : 'is today',
        isTomorrow             : 'is tomorrow',
        isYesterday            : 'is yesterday',
        isThisWeek             : 'is this week',
        isNextWeek             : 'is next week',
        isLastWeek             : 'is last week',
        isThisMonth            : 'is this month',
        isNextMonth            : 'is next month',
        isLastMonth            : 'is last month',
        isThisYear             : 'is this year',
        isNextYear             : 'is next year',
        isLastYear             : 'is last year',
        isYearToDate           : 'is year to date',
        isTrue                 : 'is true',
        isFalse                : 'is false',
        selectAProperty        : 'Select a property',
        selectAnOperator       : 'Select an operator',
        caseSensitive          : 'Case-sensitive',
        and                    : 'and',
        dateFormat             : 'D/M/YY',
        selectOneOrMoreValues  : 'Select one or more values',
        enterAValue            : 'Enter a value',
        enterANumber           : 'Enter a number',
        selectADate            : 'Select a date'
    },

    FieldFilterPickerGroup : {
        addFilter : 'Add filter'
    },

    DateHelper : {
        locale         : 'en-GB',
        weekStartDay   : 1,
        nonWorkingDays : {
            0 : true,
            6 : true
        },
        weekends : {
            0 : true,
            6 : true
        },
        unitNames : [
            { single : 'millisecond', plural : 'ms', abbrev : 'ms' },
            { single : 'second', plural : 'seconds', abbrev : 's' },
            { single : 'minute', plural : 'minutes', abbrev : 'min' },
            { single : 'hour', plural : 'hours', abbrev : 'h' },
            { single : 'day', plural : 'days', abbrev : 'd' },
            { single : 'week', plural : 'weeks', abbrev : 'w' },
            { single : 'month', plural : 'months', abbrev : 'mon' },
            { single : 'quarter', plural : 'quarters', abbrev : 'q' },
            { single : 'year', plural : 'years', abbrev : 'yr' },
            { single : 'decade', plural : 'decades', abbrev : 'dec' }
        ],
        unitAbbreviations : [
            ['mil'],
            ['s', 'sec'],
            ['m', 'min'],
            ['h', 'hr'],
            ['d'],
            ['w', 'wk'],
            ['mo', 'mon', 'mnt'],
            ['q', 'quar', 'qrt'],
            ['y', 'yr'],
            ['dec']
        ],
        parsers : {
            L   : 'DD/MM/YYYY',
            LT  : 'HH:mm A',
            LTS : 'HH:mm:ss A'
        },
        ordinalSuffix : number => {
            const hasSpecialCase = ['11', '12', '13'].find((n) => number.endsWith(n));

            let suffix = 'th';

            if (!hasSpecialCase) {
                const lastDigit = number[number.length - 1];
                suffix = { 1 : 'st', 2 : 'nd', 3 : 'rd' }[lastDigit] || 'th';
            }

            return number + suffix;
        }
    }
};

export default LocaleHelper.publishLocale(locale);
