import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ar.js';
import '../../Scheduler/localization/Ar.js';

const locale = {

    localeName : 'Ar',
    localeDesc : 'اللغة العربية',
    localeCode : 'ar',

    ConstraintTypePicker : {
        none                : 'لا يوجد',
        assoonaspossible    : 'في أقرب وقت ممكن',
        aslateaspossible    : 'في أحرى وقت ممكن',
        muststarton         : 'يجب أن يبدأ في',
        mustfinishon        : 'يجب أن ينتهي في',
        startnoearlierthan  : 'لا تبدأ قبل',
        startnolaterthan    : 'ابدأ في قبل',
        finishnoearlierthan : 'لا تنهِ قبل',
        finishnolaterthan   : 'أنهِ قبل'
    },

    SchedulingDirectionPicker : {
        Forward       : 'إلى الأمام',
        Backward      : 'إلى الخلف',
        inheritedFrom : 'موروث من',
        enforcedBy    : 'فُرض بواسطة'
    },

    CalendarField : {
        'Default calendar' : 'التقويم الافتراضي'
    },

    TaskEditorBase : {
        Information   : 'معلومات',
        Save          : 'حفظ',
        Cancel        : 'إلغاء',
        Delete        : 'حذف',
        calculateMask : '...جارٍ الحساب',
        saveError     : 'حفظ، المرجو تصحيح الأخطاء أولا',
        repeatingInfo : 'عرض حدث متكرر',
        editRepeating : 'تعديل'
    },

    TaskEdit : {
        'Edit task'            : 'تعديل المهمة',
        ConfirmDeletionTitle   : 'تأكيد الحذف',
        ConfirmDeletionMessage : 'هل أنت متأكد من أنك تريد حذف الحدث؟'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'عام',
        Name         : 'اسم',
        Resources    : 'الموارد',
        '% complete' : '% تم إكماله',
        Duration     : 'المدة الزمنية',
        Start        : 'ابدأ',
        Finish       : 'إنهاء',
        Effort       : 'الجهد',
        Preamble     : 'الديباجة',
        Postamble    : 'الخاتمة'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'عام',
        Name         : 'اسم',
        '% complete' : '% تم إكماله',
        Duration     : 'المدة الزمنية',
        Start        : 'بدء',
        Finish       : 'إنهاء',
        Effort       : 'الجهد',
        Dates        : 'المواعيد'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'متقدم',
        Calendar                   : 'التقويم',
        'Scheduling mode'          : 'وضع الجدولة',
        'Effort driven'            : 'حسب الجهد',
        'Manually scheduled'       : 'تمت جدولته يدويًا',
        'Constraint type'          : 'نوع التقييد',
        'Constraint date'          : 'تاريخ التقييد',
        Inactive                   : 'غير نشط',
        'Ignore resource calendar' : 'تجاهل تقويم المورد'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'متقدم',
        Calendar                   : 'التقويم',
        'Scheduling mode'          : 'وضع الجدولة',
        'Effort driven'            : 'حسب الجهد',
        'Manually scheduled'       : 'تمت جدولته يدويًا',
        'Constraint type'          : 'نوع التقييد',
        'Constraint date'          : 'تاريخ التقييد',
        Constraint                 : 'التقييد',
        Rollup                     : 'التراكمي',
        Inactive                   : 'غير نشط',
        'Ignore resource calendar' : 'تجاهل تقويم المورد',
        'Scheduling direction'     : 'اتجاه الجدولة'
    },

    DependencyTab : {
        Predecessors      : 'السالفة',
        Successors        : 'اللاحقة',
        ID                : 'المعرف',
        Name              : 'اسم',
        Type              : 'النوع',
        Lag               : 'تأخير',
        cyclicDependency  : 'التبعية الدورية',
        invalidDependency : 'تبعية غير صالحة'
    },

    NotesTab : {
        Notes : 'ملاحظات'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'الموارد',
        Resource  : 'المورد',
        Units     : 'الوحدات'
    },

    RecurrenceTab : {
        title : 'العنوان'
    },

    SchedulingModePicker : {
        Normal           : 'طبيعي',
        'Fixed Duration' : 'المدة الزمنية الثابتة',
        'Fixed Units'    : 'الوحدات الثابتة',
        'Fixed Effort'   : 'الجهد الثابت'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} من {available}</span> المخصص',
        barTipOnDate          : '<b>{resource}</b> بتاريخ {startDate}<br><span class="{cls}">{allocated} من {available}</span> المخصص',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} من {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} من {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'في {startDate}<br><span class="{cls}">{allocated} من {available}</span> المخصص:<br>{assignments}',
        plusMore              : '+{value} أكثر'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> المخصص',
        barTipOnDate          : '<b>{event}</b> بتاريخ {startDate}<br><span class="{cls}">{allocated}</span> المخصص',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} من {available}</span> المخصص:<br>{assignments}',
        groupBarTipOnDate     : 'في {startDate}<br><span class="{cls}">{allocated} من {available}</span> المخصص:<br>{assignments}',
        plusMore              : '+{value} أكثر',
        nameColumnText        : 'مورد/حدث'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'إلغاء التغييرات وعدم فعل شيء',
        schedulingConflict : 'تعارض في الجدولة',
        emptyCalendar      : 'خطأ في تكوين التقويم',
        cycle              : 'دورة الجدولة',
        Apply              : 'تطبيق'
    },

    CycleResolutionPopup : {
        dependencyLabel        : ':يُرجى اختيار تبعية',
        invalidDependencyLabel : ':هناك تبعيات غير صالحة متضمنة تحتاج لأن تعالج'
    },

    DependencyEdit : {
        Active : 'نشط'
    },

    SchedulerProBase : {
        propagating     : 'جارٍ حساب المشروع',
        storePopulation : 'جارٍ تحميل البيانات',
        finalizing      : 'جارٍ إنهاء النتائج'
    },

    EventSegments : {
        splitEvent    : 'تقسيم الحدث',
        renameSegment : 'إعادة التسمية'
    },

    NestedEvents : {
        deNestingNotAllowed : 'غير مسموح بإزالة التداخل',
        nestingNotAllowed   : 'غير مسموح بالتداخل'
    },

    VersionGrid : {
        compare       : 'قارن',
        description   : 'الوصف',
        occurredAt    : 'حدث في',
        rename        : 'إعادة التسمية',
        restore       : 'استعادة',
        stopComparing : 'توقف عن المقارنة'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'المهمة',
            AssignmentModel : 'التعيين',
            DependencyModel : 'الرابط',
            ProjectModel    : 'المشروع',
            ResourceModel   : 'المصدر',
            other           : 'الموضوع'
        },
        entityNamesPlural : {
            TaskModel       : 'المهام',
            AssignmentModel : 'التعيينات',
            DependencyModel : 'الروابط',
            ProjectModel    : 'المشاريع',
            ResourceModel   : 'المصادر',
            other           : 'المواضيع'
        },
        transactionDescriptions : {
            update : 'تم تغيير {n} {entities}',
            add    : 'تمت إضافة {n} {entities}',
            remove : 'تمت إزالة {n} {entities}',
            move   : 'تم نقل {n} {entities}',
            mixed  : 'تم تغيير {n} {entities}'
        },
        addEntity         : 'تمت الإضافة {type} ** {name} **',
        removeEntity      : 'تمت إزالة {type} ** {name} **',
        updateEntity      : 'تم تغيير {type} ** {name} **',
        moveEntity        : 'تم النقل {type} ** {name} ** من {from} إلى {to}',
        addDependency     : 'تمت إضافة رابط من ** {from} ** إلى ** {to} **',
        removeDependency  : 'تمت إزالة رابط من ** {from} ** إلى ** {to} **',
        updateDependency  : 'تم تعديل رابط من ** {from} ** إلى ** {to} **',
        addAssignment     : 'تم تعيين **{resource}** to **{event}**',
        removeAssignment  : 'تمت إزالة تعيين ** {resource} ** من ** {event} **',
        updateAssignment  : 'تم تعديل تعيين ** {resource} ** من ** {event} **',
        noChanges         : 'لا توجد تغييرات',
        nullValue         : 'لا يوجد',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'تم إلغاء التغييرات',
        redid             : 'تمت إعادة التغييرات',
        editedTask        : 'خصائص المهمة المعدلة',
        deletedTask       : 'تم حذف المهمة',
        movedTask         : 'تم نقل المهمة',
        movedTasks        : 'تم نقل المهام'
    }
};

export default LocaleHelper.publishLocale(locale);
