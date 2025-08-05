import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Hi.js';
import '../../Scheduler/localization/Hi.js';

const locale = {

    localeName : 'Hi',
    localeDesc : 'हिन्दी',
    localeCode : 'hi',

    ConstraintTypePicker : {
        none                : 'कोई नहीं',
        assoonaspossible    : 'जल्द से जल्द',
        aslateaspossible    : 'जितनी देर हो सके',
        muststarton         : 'पर शुरू होना चाहिए',
        mustfinishon        : 'पर समाप्त होना चाहिए',
        startnoearlierthan  : 'से पहले शुरू न हो',
        startnolaterthan    : 'के बाद शुरू न हो',
        finishnoearlierthan : 'से पहले समाप्त न हो',
        finishnolaterthan   : 'के बाद समाप्त न हो'
    },

    SchedulingDirectionPicker : {
        Forward       : 'आगे',
        Backward      : 'पीछे',
        inheritedFrom : 'वंशानुक्रम से',
        enforcedBy    : 'ज़बरदस्ती से'
    },

    CalendarField : {
        'Default calendar' : 'डिफॉल्ट कैलेन्डर'
    },

    TaskEditorBase : {
        Information   : 'जानकारी',
        Save          : 'सहेजें',
        Cancel        : 'रद्द करें',
        Delete        : 'हटाएं',
        calculateMask : 'गणना कर रहा है...',
        saveError     : 'नहीं सहेज सकता है, कृपया पहले त्रुटियां सही करें',
        repeatingInfo : 'दोहराव वाले ईवेंट को देखना',
        editRepeating : 'संपादित करें'
    },

    TaskEdit : {
        'Edit task'            : 'टास्क संपादित करें',
        ConfirmDeletionTitle   : 'हटाने की पुष्टि करें',
        ConfirmDeletionMessage : 'क्या आप वाकई इस ईवेंट को हटाना चाहते हैं?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'सामान्य',
        Name         : 'नाम',
        Resources    : 'संसाधन',
        '% complete' : '% पूर्ण',
        Duration     : 'अवधि',
        Start        : 'शुरु करें',
        Finish       : 'समाप्त करें',
        Effort       : 'प्रयास',
        Preamble     : 'प्रस्तावना',
        Postamble    : 'समापन कथन'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'समान्य',
        Name         : 'नाम',
        '% complete' : '% पूर्ण',
        Duration     : 'अवधि',
        Start        : 'शुरु करें',
        Finish       : 'समाप्त करें',
        Effort       : 'प्रयास',
        Dates        : 'तारीखें'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'उन्नत',
        Calendar                   : 'कैलेन्डर',
        'Scheduling mode'          : 'शेड्यूलिंग मोड',
        'Effort driven'            : 'प्रयास चलित',
        'Manually scheduled'       : 'मैनुअल रूप से शेड्यूल',
        'Constraint type'          : 'बाधा प्रकार',
        'Constraint date'          : 'बाधा की तारीख',
        Inactive                   : 'निष्क्रिय',
        'Ignore resource calendar' : 'संसाधन कैलेंडर नजरअंदाज करें'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'उन्नत',
        Calendar                   : 'कैलेन्डर',
        'Scheduling mode'          : 'शेड्यूलिंग मोड',
        'Effort driven'            : 'प्रयास चलित',
        'Manually scheduled'       : 'मैनुअल रूप से शेड्यूल',
        'Constraint type'          : 'बाधा प्रकार',
        'Constraint date'          : 'बाधा तारीख',
        Constraint                 : 'बाधा',
        Rollup                     : 'रोलअप करें',
        Inactive                   : 'निष्क्रिय',
        'Ignore resource calendar' : 'संसाधन कैलेंडर नजरअंदाज करें',
        'Scheduling direction'     : 'अनुसूची दिशा'
    },

    DependencyTab : {
        Predecessors      : 'पूर्ववर्ती',
        Successors        : 'उत्तराधिकारी',
        ID                : 'ID',
        Name              : 'नाम',
        Type              : 'प्रकार',
        Lag               : 'पिछड़ाव',
        cyclicDependency  : 'चक्रीय निर्भरता',
        invalidDependency : 'अमान्य निर्भरता'
    },

    NotesTab : {
        Notes : 'नोट्स'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'संसाधन',
        Resource  : 'संसाधन',
        Units     : 'इकाइयां'
    },

    RecurrenceTab : {
        title : 'दोहराएं'
    },

    SchedulingModePicker : {
        Normal           : 'सामान्य',
        'Fixed Duration' : 'नियत अवधि',
        'Fixed Units'    : 'नियत इकाइयां',
        'Fixed Effort'   : 'नियत प्रयास'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{available} का {allocated}</span> आवंटित',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{available} का {allocated}</span> आवंटित',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{available} का {allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{available} का {allocated}</span> आवंटित:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}"> पर {available} का {allocated}</span> आवंटित:<br>{assignments}',
        plusMore              : '+{value} और'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> आवंटित',
        barTipOnDate          : '<b>{event}</b> {startDate}<br><span class="{cls}"> पर {allocated}</span> आवंटित',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{available} का {allocated}</span> आवंटित:<br>{assignments}',
        groupBarTipOnDate     : '{startDate}<br><span class="{cls}"> पर {available} का {allocated}</span> आवंटित:<br>{assignments}',
        plusMore              : '+{value} और',
        nameColumnText        : 'संसाधन / ईवेंट'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'बदलाव रद्द करें और कुछ न करें',
        schedulingConflict : 'शेड्यूलिंग संघर्ष',
        emptyCalendar      : 'कैलेन्डर कॉन्फिगरेशन त्रुटि',
        cycle              : 'शेड्यूलिंग चक्र',
        Apply              : 'लागू करें'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'कृपया निर्भरता को चुनें:',
        invalidDependencyLabel : 'अमान्य निर्भरताएं शामिल हैं जिन्हें ठीक करने की जरूरत है:'
    },

    DependencyEdit : {
        Active : 'सक्रिय'
    },

    SchedulerProBase : {
        propagating     : 'प्रोजेक्ट की गणना करना',
        storePopulation : 'डेटा लोड करना',
        finalizing      : 'परिणामों को अंतिम रूप देना'
    },

    EventSegments : {
        splitEvent    : 'ईवेंट स्प्लिट करें',
        renameSegment : 'नाम बदलें'
    },

    NestedEvents : {
        deNestingNotAllowed : 'नेस्टिंग से निकलने की अनुमति नहीं है',
        nestingNotAllowed   : 'नेस्टिंग की अनुमति नहीं है'
    },

    VersionGrid : {
        compare       : 'तुलना करें',
        description   : 'विवरण',
        occurredAt    : 'घटित हुआ था',
        rename        : 'नाम बदलें',
        restore       : 'पुनः स्थापित करें',
        stopComparing : 'तुलना बंद करें'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'टास्क',
            AssignmentModel : 'असाइनमेंट',
            DependencyModel : 'लिंक',
            ProjectModel    : 'प्रोजेक्ट',
            ResourceModel   : 'संसाधन',
            other           : 'वस्तु'
        },
        entityNamesPlural : {
            TaskModel       : 'टास्क',
            AssignmentModel : 'असाइनमेंट्स',
            DependencyModel : 'लिंक्स',
            ProjectModel    : 'प्रोजेक्ट्स',
            ResourceModel   : 'संसाधन',
            other           : 'वस्तुएं'
        },
        transactionDescriptions : {
            update : '{n} {entities} बदलीं',
            add    : '{n} {entities} जोड़ी',
            remove : '{n} {entities} हटायी',
            move   : '{n} {entities} मूव की',
            mixed  : '{n} {entities} बदली'
        },
        addEntity         : '{type} **{name} जोड़ा**',
        removeEntity      : '{type} **{name} हटाया**',
        updateEntity      : '{type} **{name} बदला**',
        moveEntity        : '{type} **{name}** को  {from}से {to} पर मूव किया',
        addDependency     : '**{from} से** **{to}पर  लिंक जोड़ा**',
        removeDependency  : '**{from}** से **{to}** तक लिंक हटाया',
        updateDependency  : '**{from}** से **{to}** तक लिंक संपादित किया',
        addAssignment     : '**{resource}** को **{event}** ईवेंट पर असाइन किया',
        removeAssignment  : '**{resource}** से **{event}** पर असाइनमेंट हटाया',
        updateAssignment  : '**{resource}** से **{event}** पर असाइनमेंट संपादित किया',
        noChanges         : 'कोई बदलाव नहीं',
        nullValue         : 'कोई नहीं',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'बदलाव पूर्ववत किए',
        redid             : 'बदलाव बदले',
        editedTask        : 'टास्क गुणों को संपादित किया',
        deletedTask       : 'एक टास्क हटाया',
        movedTask         : 'एक टास्क मूव किया',
        movedTasks        : 'टास्क मूव किए'
    }
};

export default LocaleHelper.publishLocale(locale);
