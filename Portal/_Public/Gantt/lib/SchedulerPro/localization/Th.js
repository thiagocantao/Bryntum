import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Th.js';
import '../../Scheduler/localization/Th.js';

const locale = {

    localeName : 'Th',
    localeDesc : 'ไทย',
    localeCode : 'th',

    ConstraintTypePicker : {
        none                : 'ไม่มี',
        assoonaspossible    : 'โดยเร็วที่สุด',
        aslateaspossible    : 'โดยช้าที่สุด',
        muststarton         : 'ต้องเริ่มตั้งแต่',
        mustfinishon        : 'ต้องสิ้นสุดเมื่อ',
        startnoearlierthan  : 'ต้องไม่เริ่มต้นก่อน',
        startnolaterthan    : 'ต้องไม่เริ่มต้นหลังจาก',
        finishnoearlierthan : 'ต้องไม่สิ้นสุดก่อน',
        finishnolaterthan   : 'ต้องไม่สิ้นสุดหลังจาก'
    },

    SchedulingDirectionPicker : {
        Forward       : 'ไปข้างหน้า',
        Backward      : 'ถอยหลัง',
        inheritedFrom : 'สืบทอดมาจาก',
        enforcedBy    : 'บังคับใช้โดย'
    },

    CalendarField : {
        'Default calendar' : 'ปฏิทินเริ่มต้น'
    },

    TaskEditorBase : {
        Information   : 'ข้อมูล',
        Save          : 'บันทึก',
        Cancel        : 'ยกเลิก',
        Delete        : 'ลบ',
        calculateMask : 'กำลังคำนวณ...',
        saveError     : 'ไม่สามารถบันทึก กรุณาแก้ไขข้อผิดพลาดก่อน',
        repeatingInfo : 'กำลังเรียกดูกิจกรรมที่เกิดซ้ำ',
        editRepeating : 'แก้ไข'
    },

    TaskEdit : {
        'Edit task'            : 'แก้ไขงาน',
        ConfirmDeletionTitle   : 'ยืนยันการลบ',
        ConfirmDeletionMessage : 'คุณแน่ใจหรือไม่ว่าคุณต้องการลบกิจกรรมนี้?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '33em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'ทั่วไป',
        Name         : 'ชื่อ',
        Resources    : 'ทรัพยากร',
        '% complete' : '% ที่เสร็จสิ้น',
        Duration     : 'ระยะเวลา',
        Start        : 'เริ่มต้น',
        Finish       : 'สิ้นสุด',
        Effort       : 'การลงแรง',
        Preamble     : 'ส่วนต้น',
        Postamble    : 'ส่วนท้าย'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'ทั่วไป',
        Name         : 'ชื่อ',
        '% complete' : '% ที่เสร็จสิ้น',
        Duration     : 'ระยะเวลา',
        Start        : 'เริ่มต้น',
        Finish       : 'สิ้นสุด',
        Effort       : 'การลงแรง',
        Dates        : 'จำนวนวัน'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'ขั้นสูง',
        Calendar                   : 'ปฏิทิน',
        'Scheduling mode'          : 'โหมดจัดกำหนดการ',
        'Effort driven'            : 'ขับเคลื่อนด้วยการลงแรง',
        'Manually scheduled'       : 'จัดกำหนดการด้วยตนเอง',
        'Constraint type'          : 'ประเภทของข้อจำกัด',
        'Constraint date'          : 'วันที่ของข้อจำกัด',
        Inactive                   : 'ไม่ทำงาน',
        'Ignore resource calendar' : 'เพิกเฉยต่อปฏิทินทรัพยากร'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'ขั้นสูง',
        Calendar                   : 'ปฏิทิน',
        'Scheduling mode'          : 'โหมดจัดกำหนดการ',
        'Effort driven'            : 'ขับเคลื่อนด้วยการลงแรง',
        'Manually scheduled'       : 'จัดกำหนดการด้วยตนเอง',
        'Constraint type'          : 'ประเภทของข้อจำกัด',
        'Constraint date'          : 'วันที่ของข้อจำกัด',
        Constraint                 : 'ข้อจำกัด',
        Rollup                     : 'การแสดงข้อมูลที่หน้าสรุป',
        Inactive                   : 'ไม่ทำงาน',
        'Ignore resource calendar' : 'เพิกเฉยต่อปฏิทินทรัพยากร',
        'Scheduling direction'     : 'ทิศทางการตั้งตารางเวลา'
    },

    DependencyTab : {
        Predecessors      : 'งานลำดับก่อนหน้า',
        Successors        : 'งานที่ตามมา',
        ID                : 'รหัส',
        Name              : 'ชื่อ',
        Type              : 'ประเภท',
        Lag               : 'การล่าช้า',
        cyclicDependency  : 'การอ้างอิงเป็นวงรอบ',
        invalidDependency : 'การอ้างอิงที่ไม่ถูกต้อง'
    },

    NotesTab : {
        Notes : 'หมายเหตุ'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'ทรัพยากร',
        Resource  : 'ทรัพยากร',
        Units     : 'หน่วย'
    },

    RecurrenceTab : {
        title : 'ทำซ้ำ'
    },

    SchedulingModePicker : {
        Normal           : 'ปกติ',
        'Fixed Duration' : 'ระยะเวลาคงที่',
        'Fixed Units'    : 'หน่วยคงที่',
        'Fixed Effort'   : 'การลงแรงคงที่'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} จาก {available}</span>ที่ได้รับการจัดสรร',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} จาก {available}</span> ที่ได้รับการจัดสรร',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} จาก {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} จาก {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'ใน {startDate}<br><span class="{cls}">{allocated} จาก {available}</span> ที่ได้รับการจัดสรร:<br>{assignments}',
        plusMore              : '+{value} เพิ่มเติม'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> ที่ได้รับการจัดสรร',
        barTipOnDate          : '<b>{event}</b> ใน {startDate}<br><span class="{cls}">{allocated}</span> ที่ได้รับการจัดสรร',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} จาก {available}</span> ที่ได้รับการจัดสรร:<br>{assignments}',
        groupBarTipOnDate     : 'ใน {startDate}<br><span class="{cls}">{allocated} จาก {available}</span> ที่ได้รับการจัดสรร:<br>{assignments}',
        plusMore              : '+{value} เพิ่มเติม',
        nameColumnText        : 'ทรัพยากร / กิจกรรม'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'ยกเลิกการเปลี่ยนแปลงและไม่ดำเนินการใด ๆ',
        schedulingConflict : 'ความขัดแย้งของการจัดกำหนดการ',
        emptyCalendar      : 'ข้อผิดพลาดของการกำหนดค่าปฏิทิน',
        cycle              : 'วงรอบการจัดกำหนดการ',
        Apply              : 'นำไปใช้'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'กรุณาเลือกการอ้างอิง:',
        invalidDependencyLabel : 'พบการอ้างอิงไม่ถูกต้องที่จำเป็นต้องได้รับการแก้ไข:'
    },

    DependencyEdit : {
        Active : 'ทำงาน'
    },

    SchedulerProBase : {
        propagating     : 'กำลังคำนวณโครงการ',
        storePopulation : 'กำลังโหลดข้อมูล',
        finalizing      : 'กำลังสรุปผลลัพธ์'
    },

    EventSegments : {
        splitEvent    : 'แยกกิจกรรม',
        renameSegment : 'เปลี่ยนชื่อ'
    },

    NestedEvents : {
        deNestingNotAllowed : 'ไม่อนุญาตให้ยกเลิกการรวมกลุ่ม',
        nestingNotAllowed   : 'ไม่อนุญาตให้รวมกลุ่ม'
    },

    VersionGrid : {
        compare       : 'เปรียบเทียบ',
        description   : 'คำอธิบาย',
        occurredAt    : 'เกิดขึ้นที่',
        rename        : 'เปลี่ยนชื่อ',
        restore       : 'กู้คืน',
        stopComparing : 'หยุดเปรียบเทียบ'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'งาน',
            AssignmentModel : 'งานที่กำหนด',
            DependencyModel : 'ลิงก์',
            ProjectModel    : 'โครงการ',
            ResourceModel   : 'ทรัพยากร',
            other           : 'ออบเจ็กต์'
        },
        entityNamesPlural : {
            TaskModel       : 'งาน',
            AssignmentModel : 'งานที่กำหนด',
            DependencyModel : 'ลิงก์',
            ProjectModel    : 'โครงการ',
            ResourceModel   : 'ทรัพยากร',
            other           : 'ออบเจ็กต์'
        },
        transactionDescriptions : {
            update : 'เปลี่ยน {n} {entities} แล้ว',
            add    : 'เพิ่ม {n} {entities} แล้ว',
            remove : 'นำ {n} {entities} ออกแล้ว',
            move   : 'ย้าย {n} {entities} แล้ว',
            mixed  : 'เปลี่ยนแปลง {n} {entities} แล้ว'
        },
        addEntity         : 'เพิ่ม {type} **{name}** แล้ว',
        removeEntity      : 'นำ {type} **{name}** ออกแล้ว',
        updateEntity      : 'เปลี่ยน {type} **{name}** แล้ว',
        moveEntity        : 'ย้าย {type} **{name}** จาก {from} ไปยัง {to}',
        addDependency     : 'เพิ่มลิงก์จาก **{from}** ไปยัง **{to}** แล้ว',
        removeDependency  : 'นำลิงก์ออกจาก **{from}** ไปยัง **{to}** แล้ว',
        updateDependency  : 'แก้ไขลิงก์จาก **{from}** ไปยัง **{to}**',
        addAssignment     : 'กำหนด **{resource}** ไปยัง **{event}** แล้ว',
        removeAssignment  : 'นำงานที่กำหนดของ **{resource}** ออกจาก **{event}** แล้ว',
        updateAssignment  : 'แก้ไขงานที่กำหนดของ **{resource}** ไปยัง **{event}** แล้ว',
        noChanges         : 'ไม่มีการเปลี่ยนแปลง',
        nullValue         : 'ไม่มี',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'ยกเลิกการเปลี่ยนแปลง',
        redid             : 'ทำการเปลี่ยนแปลงซ้ำ',
        editedTask        : 'แก้ไขคุณสมบัติงานแล้ว',
        deletedTask       : 'ลบงานแล้ว',
        movedTask         : 'ย้ายงานหนึ่งแล้ว',
        movedTasks        : 'ย้ายหลายงานแล้ว'
    }
};

export default LocaleHelper.publishLocale(locale);
