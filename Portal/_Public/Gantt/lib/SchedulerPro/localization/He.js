import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/He.js';
import '../../Scheduler/localization/He.js';

const locale = {

    localeName : 'He',
    localeDesc : 'עִברִית',
    localeCode : 'he',

    ConstraintTypePicker : {
        none                : 'אין',
        assoonaspossible    : 'בהקדם האפשרי',
        aslateaspossible    : 'בהקדם האפשרי',
        muststarton         : 'חייב להתחיל ב',
        mustfinishon        : 'חייב להסתיים ב',
        startnoearlierthan  : 'התחל לא לפני',
        startnolaterthan    : 'התחל לא יאוחר מ',
        finishnoearlierthan : 'סיים לא לפני',
        finishnolaterthan   : 'סיים לא יאוחר מ'
    },

    SchedulingDirectionPicker : {
        Forward       : 'קדימה',
        Backward      : 'אחורה',
        inheritedFrom : 'מורש מ',
        enforcedBy    : 'מוטל בכוח על ידי'
    },

    CalendarField : {
        'Default calendar' : 'לוח שנה ברירת מחדל'
    },

    TaskEditorBase : {
        Information   : 'מידע',
        Save          : 'שמור',
        Cancel        : 'בטל',
        Delete        : 'מחוק',
        calculateMask : '...חישוב',
        saveError     : 'לא ניתן לשמור, אנא תקן לפני כן את השגיאות',
        repeatingInfo : 'צופה באירוע חוזר',
        editRepeating : 'עריכה'
    },

    TaskEdit : {
        'Edit task'            : 'ערוך משימה',
        ConfirmDeletionTitle   : 'אשר מחיקה',
        ConfirmDeletionMessage : '?האם הנך בטוח/ה שברצונך למחוק את האירוע'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'כללי',
        Name         : 'שם',
        Resources    : 'משאבים',
        '% complete' : '% הושלם',
        Duration     : 'משך',
        Start        : 'התחלה',
        Finish       : 'סוף',
        Effort       : 'מאמץ',
        Preamble     : 'מבוא',
        Postamble    : 'אחרית'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'כללי',
        Name         : 'שם',
        '% complete' : '% הושלם',
        Duration     : 'משך',
        Start        : 'התחל',
        Finish       : 'סוף',
        Effort       : 'מאמץ',
        Dates        : 'תאריכים'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'מתקדם',
        Calendar                   : 'לוח שנה',
        'Scheduling mode'          : 'מצב תזמון',
        'Effort driven'            : 'מונע מאמץ',
        'Manually scheduled'       : 'מתוזמן ידנית',
        'Constraint type'          : 'סוג אילוץ',
        'Constraint date'          : 'תאריך אילוץ',
        Inactive                   : 'בלתי-פעיל',
        'Ignore resource calendar' : 'התעלם מלוח משאבים'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'מתקדם',
        Calendar                   : 'לוח שנה',
        'Scheduling mode'          : 'מצב תזמון',
        'Effort driven'            : 'מונע מאמץ',
        'Manually scheduled'       : 'מתוזמן ידנית',
        'Constraint type'          : 'סוף אילוץ',
        'Constraint date'          : 'תאריך אילוץ',
        Constraint                 : 'אילוץ',
        Rollup                     : 'גלילה מעלה',
        Inactive                   : 'בלתי-פעיל',
        'Ignore resource calendar' : 'התעלם מלוח משאבים',
        'Scheduling direction'     : 'כיוון תזמון'
    },

    DependencyTab : {
        Predecessors      : 'אירועים קודמים',
        Successors        : 'אירועים יורשים',
        ID                : 'מזהה',
        Name              : 'שם',
        Type              : 'סוג',
        Lag               : 'עיכוב',
        cyclicDependency  : 'תלות ציקלית',
        invalidDependency : 'תלות בלתי-חוקית'
    },

    NotesTab : {
        Notes : 'הערות'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'משאבים',
        Resource  : 'משאב',
        Units     : 'יחידות'
    },

    RecurrenceTab : {
        title : 'חזרה'
    },

    SchedulingModePicker : {
        Normal           : 'נורמלי',
        'Fixed Duration' : 'משך קבוע',
        'Fixed Units'    : 'יחידות קבועות',
        'Fixed Effort'   : 'מאמץ קבוע'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו',
        barTipOnDate          : '<b>{resource}</b> ב {startDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} מתוך {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו:<br>{assignments}',
        groupBarTipOnDate     : 'ב {startDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו:<br>{assignments}',
        plusMore              : '+{value} יותר'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> הוקצו',
        barTipOnDate          : '<b>{event}</b> מתוך {startDate}<br><span class="{cls}">{allocated}</span> הוקצו',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו:<br>{assignments}',
        groupBarTipOnDate     : 'ב {startDate}<br><span class="{cls}">{allocated} מתוך {available}</span> הוקצו:<br>{assignments}',
        plusMore              : '+{value} יותר',
        nameColumnText        : 'משאב / אירוע'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'בטל את השינוי ואל תבצע שום פעולה',
        schedulingConflict : 'קונפליקט תזמון',
        emptyCalendar      : 'שגיאה בקביעת תצורת לוח שנה',
        cycle              : 'מחזור תזמון',
        Apply              : 'החל'
    },

    CycleResolutionPopup : {
        dependencyLabel        : ':אנא בחר תלות',
        invalidDependencyLabel : ':ישנה מעורבות/נוכחות של תלויות בלתי-חוקיות שיש לטפל בהן'
    },

    DependencyEdit : {
        Active : 'פעיל'
    },

    SchedulerProBase : {
        propagating     : 'מחשב פרויקט',
        storePopulation : 'טוען נתונים',
        finalizing      : 'משלים תוצאות'
    },

    EventSegments : {
        splitEvent    : 'פיצול אירוע',
        renameSegment : 'שינוי שם'
    },

    NestedEvents : {
        deNestingNotAllowed : 'ביטול קינון אינו מותר',
        nestingNotAllowed   : 'קינון אינו מותר'
    },

    VersionGrid : {
        compare       : 'השווה',
        description   : 'תיאור',
        occurredAt    : 'קרה בתאריך',
        rename        : 'שינוי שם',
        restore       : 'שחזר',
        stopComparing : 'הפסק השוואה'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'משימה',
            AssignmentModel : 'הקצאה',
            DependencyModel : 'קישור',
            ProjectModel    : 'פרויקט',
            ResourceModel   : 'משאב',
            other           : 'אוביקט'
        },
        entityNamesPlural : {
            TaskModel       : 'משימות',
            AssignmentModel : 'הקצאות',
            DependencyModel : 'קישורים',
            ProjectModel    : 'פרויקטים',
            ResourceModel   : 'משאבים',
            other           : 'אוביקטים'
        },
        transactionDescriptions : {
            update : 'שונו {n} ‏{entities}',
            add    : 'הוספו {n} ‏{entities}',
            remove : 'הוסרו {n} ‏{entities}',
            move   : 'הועברו {n} ‏{entities}',
            mixed  : 'שונו {n} ‏{entities}'
        },
        addEntity         : 'התווסף {type} **{name}**',
        removeEntity      : 'הוסר {type} **{name}**',
        updateEntity      : 'שונה {type} **{name}**',
        moveEntity        : 'הועבר {type} **{name}** מ- {from} ל- {to}',
        addDependency     : 'התווסף קישור מ- **{from}** ל- **{to}**',
        removeDependency  : 'הוסר קישור מ- **{from}** ל- **{to}**',
        updateDependency  : 'נערך קישור מ- **{from}** ל- **{to}**',
        addAssignment     : 'הוקצה **{resource}** ל- **{event}**',
        removeAssignment  : 'הוסרה הקצאת **{resource}** מ- **{event}**',
        updateAssignment  : 'נערכה הקצאת **{resource}**  ל-  **{event}**',
        noChanges         : 'אין שינויים',
        nullValue         : 'אין',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'שינויים בוטלו',
        redid             : 'שינויים בוצעו מחדש',
        editedTask        : 'תכונות משימה נערכו',
        deletedTask       : 'משימה נמחקה',
        movedTask         : 'משימה הוזזה',
        movedTasks        : 'משימות הוזזו'
    }
};

export default LocaleHelper.publishLocale(locale);
