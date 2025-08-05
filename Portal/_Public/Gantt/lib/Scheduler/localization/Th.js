import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Grid/localization/Th.js';

const locale = {

    localeName : 'Th',
    localeDesc : 'ไทย',
    localeCode : 'th',

    Object : {
        newEvent : 'เพิ่มกิจกรรมใหม่'
    },

    ResourceInfoColumn : {
        eventCountText : data => data + ' กิจกรรม'
    },

    Dependencies : {
        from    : 'ตั้งแต่',
        to      : 'ถึง',
        valid   : 'ถูกต้อง',
        invalid : 'ไม่ถูกต้อง'
    },

    DependencyType : {
        SS           : 'เอสเอส',
        SF           : 'เอสเอฟ',
        FS           : 'เอฟเอส',
        FF           : 'เอฟเอฟ',
        StartToStart : 'เริ่มต้นถึงเริ่มต้น',
        StartToEnd   : 'เริ่มต้นถึงสิ้นสุด',
        EndToStart   : 'สิ้นสุดถึงเริ่มต้น',
        EndToEnd     : 'สิ้นสุดถึงสิ้นสุด',
        short        : [
            'เอสเอส',
            'เอสเอฟ',
            'เอฟเอส',
            'เอฟเอฟ'
        ],
        long : [
            'เริ่มต้นถึงเริ่มต้น',
            'เริ่มต้นถึงสิ้นสุด',
            'สิ้นสุดถึงเริ่มต้น',
            'สิ้นสุดถึงสิ้นสุด'
        ]
    },

    DependencyEdit : {
        From              : 'ตั้งแต่',
        To                : 'ถึง',
        Type              : 'ประเภท',
        Lag               : 'การล่าช้า',
        'Edit dependency' : 'แก้ไขการอ้างอิง',
        Save              : 'บันทึก',
        Delete            : 'ลบ',
        Cancel            : 'ยกเลิก',
        StartToStart      : 'เริ่มต้นถึงเริ่มต้น',
        StartToEnd        : 'เริ่มต้นถึงสิ้นสุด',
        EndToStart        : 'สิ้นสุดถึงเริ่มต้น',
        EndToEnd          : 'สิ้นสุดถึงสิ้นสุด'
    },

    EventEdit : {
        Name         : 'ชื่อ',
        Resource     : 'ทรัพยากร',
        Start        : 'เริ่มต้น',
        End          : 'สิ้นสุด',
        Save         : 'บันทึก',
        Delete       : 'ลบ',
        Cancel       : 'ยกเลิก',
        'Edit event' : 'แก้ไขกิจกรรม',
        Repeat       : 'ทำซ้ำ'
    },

    EventDrag : {
        eventOverlapsExisting : 'กิจกรรมทับซ้อนกับกิจกรรมที่มีอยู่แล้วสำหรับทรัพยากรนี้',
        noDropOutsideTimeline : 'ไม่สามารถวางกิจกรรมนอกไทม์ไลน์โดยสมบูรณ์ได้'
    },

    SchedulerBase : {
        'Add event'      : 'เพิ่มกิจกรรม',
        'Delete event'   : 'ลบกิจกรรม',
        'Unassign event' : 'ยกเลิกการกำหนดกิจกรรม',
        color            : 'สี'
    },

    TimeAxisHeaderMenu : {
        pickZoomLevel   : 'ขยาย',
        activeDateRange : 'ช่วงวันที่',
        startText       : 'วันที่เริ่มต้น',
        endText         : 'วันที่สิ้นสุด',
        todayText       : 'วันนี้'
    },

    EventCopyPaste : {
        copyEvent  : 'คัดลอกกิจกรรม',
        cutEvent   : 'ตัดกิจกรรม',
        pasteEvent : 'วางกิจกรรม'
    },

    EventFilter : {
        filterEvents : 'กรองกิจกรรม',
        byName       : 'ตามชื่อ'
    },

    TimeRanges : {
        showCurrentTimeLine : 'แสดงไทม์ไลน์ปัจจุบัน'
    },

    PresetManager : {
        secondAndMinute : {
            displayDateFormat : 'll LTS',
            name              : 'วินาที'
        },
        minuteAndHour : {
            topDateFormat     : 'ddd DD/MM, H',
            displayDateFormat : 'll LST'
        },
        hourAndDay : {
            topDateFormat     : 'ddd DD/MM',
            middleDateFormat  : 'LST',
            displayDateFormat : 'll LST',
            name              : 'วัน'
        },
        day : {
            name : 'วัน/ชั่วโมง'
        },
        week : {
            name : 'สัปดาห์/ชั่วโมง'
        },
        dayAndWeek : {
            displayDateFormat : 'll LST',
            name              : 'สัปดาห์/วัน'
        },
        dayAndMonth : {
            name : 'เดือน'
        },
        weekAndDay : {
            displayDateFormat : 'll LST',
            name              : 'สัปดาห์'
        },
        weekAndMonth : {
            name : 'สัปดาห์'
        },
        weekAndDayLetter : {
            name : 'สัปดาห์/วันธรรมดา'
        },
        weekDateAndMonth : {
            name : 'เดือน/สัปดาห์'
        },
        monthAndYear : {
            name : 'เดือน'
        },
        year : {
            name : 'ปี'
        },
        manyYears : {
            name : 'หลายปี'
        }
    },

    RecurrenceConfirmationPopup : {
        'delete-title'              : 'คุณกำลังจะลบกิจกรรม',
        'delete-all-message'        : 'คุณต้องการที่จะลบกิจกรรมนี้ทุกครั้งหรือไม่?',
        'delete-further-message'    : 'คุณต้องการที่จะลบกิจกรรมครั้งนี้รวมถึงกิจกรรมทั้งหมดในอนาคต หรือลบเฉพาะกิจกรรมที่เลือก?',
        'delete-further-btn-text'   : 'ลบกิจกรรมทั้งหมดในอนาคต',
        'delete-only-this-btn-text' : 'ลบเฉพาะกิจกรรมนี้',
        'update-title'              : 'คุณกำลังเปลี่ยนแปลงกิจกรรมที่เกิดซ้ำ',
        'update-all-message'        : 'คุณต้องการที่จะเปลี่ยนแปลงกิจกรรมนี้ทุกครั้งหรือไม่?',
        'update-further-message'    : 'คุณต้องการที่จะเปลี่ยนแปลงเฉพาะกิจกรรมครั้งนี้ หรือเปลี่ยนแปลงกิจกรรมนี้รวมถึงกิจกรรมทั้งหมดในอนาคต?',
        'update-further-btn-text'   : 'กิจกรรมทั้งหมดในอนาคต',
        'update-only-this-btn-text' : 'เฉพาะกิจกรรมครั้งนี้',
        Yes                         : 'ใช่',
        Cancel                      : 'ยกเลิก',
        width                       : 600
    },

    RecurrenceLegend : {
        ' and '                         : ' และ ',
        Daily                           : 'ทุกวัน',
        'Weekly on {1}'                 : ({ days }) => `ทุกสัปดาห์ในวัน ${days}`,
        'Monthly on {1}'                : ({ days }) => `ทุกเดือนในวัน ${days}`,
        'Yearly on {1} of {2}'          : ({ days, months }) => `ทุกปีในวัน ${days} ของ ${months}`,
        'Every {0} days'                : ({ interval }) => `ทุก ${interval} วัน`,
        'Every {0} weeks on {1}'        : ({ interval, days }) => `ทุก ${interval} สัปดาห์ในวัน ${days}`,
        'Every {0} months on {1}'       : ({ interval, days }) => `ทุก ${interval} เดือนในวัน ${days}`,
        'Every {0} years on {1} of {2}' : ({ interval, days, months }) => `ทุก ${interval} ปีในวัน ${days} ของ ${months}`,
        position1                       : 'วันที่หนึ่ง',
        position2                       : 'วันที่สอง',
        position3                       : 'วันที่สาม',
        position4                       : 'วันที่สี่',
        position5                       : 'วันที่ห้า',
        'position-1'                    : 'วันสุดท้าย',
        day                             : 'วัน',
        weekday                         : 'วันธรรมดา',
        'weekend day'                   : 'วันสุดสัปดาห์',
        daysFormat                      : ({ position, days }) => `${position} ${days}`
    },

    RecurrenceEditor : {
        'Repeat event'      : 'ทำกิจกรรมซ้ำ',
        Cancel              : 'ยกเลิก',
        Save                : 'บันทึก',
        Frequency           : 'ความถี่',
        Every               : 'ทุก',
        DAILYintervalUnit   : 'วัน',
        WEEKLYintervalUnit  : 'สัปดาห์',
        MONTHLYintervalUnit : 'เดือน',
        YEARLYintervalUnit  : 'ปี',
        Each                : 'แต่ละ',
        'On the'            : 'ในวัน',
        'End repeat'        : 'สิ้นสุดการทำซ้ำ',
        'time(s)'           : 'ครั้ง'
    },

    RecurrenceDaysCombo : {
        day           : 'วัน',
        weekday       : 'วันธรรมดา',
        'weekend day' : 'วันสุดสัปดาห์'
    },

    RecurrencePositionsCombo : {
        position1    : 'วันที่หนึ่ง',
        position2    : 'วันที่สอง',
        position3    : 'วันที่สาม',
        position4    : 'วันที่สี่',
        position5    : 'วันที่ห้า',
        'position-1' : 'วันสุดท้าย'
    },

    RecurrenceStopConditionCombo : {
        Never     : 'ไม่ต้อง',
        After     : 'หลังจาก',
        'On date' : 'ในวันที่'
    },

    RecurrenceFrequencyCombo : {
        None    : 'ไม่มีการทำซ้ำ',
        Daily   : 'ทุกวัน',
        Weekly  : 'ทุกสัปดาห์',
        Monthly : 'ทุกเดือน',
        Yearly  : 'ทุกปี'
    },

    RecurrenceCombo : {
        None   : 'ไม่มี',
        Custom : 'กำหนดเอง...'
    },

    Summary : {
        'Summary for' : date => `สรุปสำหรับ ${date}`
    },

    ScheduleRangeCombo : {
        completeview : 'กำหนดการทั้งหมด',
        currentview  : 'กำหนดการที่มองเห็น',
        daterange    : 'ช่วงวันที่',
        completedata : 'กำหนดการทั้งหมด (สำหรับทุกกิจกรรม)'
    },

    SchedulerExportDialog : {
        'Schedule range' : 'ช่วงกำหนดการ',
        'Export from'    : 'ตั้งแต่',
        'Export to'      : 'ถึง'
    },

    ExcelExporter : {
        'No resource assigned' : 'ไม่มีการจัดสรรทรัพยากร'
    },

    CrudManagerView : {
        serverResponseLabel : 'การตอบสนองของเซิร์ฟเวอร์:'
    },

    DurationColumn : {
        Duration : 'ระยะเวลา'
    }
};

export default LocaleHelper.publishLocale(locale);
