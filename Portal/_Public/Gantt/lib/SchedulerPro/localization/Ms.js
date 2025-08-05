import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Ms.js';
import '../../Scheduler/localization/Ms.js';

const locale = {

    localeName : 'Ms',
    localeDesc : 'Melayu',
    localeCode : 'ms',

    ConstraintTypePicker : {
        none                : 'Tiada',
        assoonaspossible    : 'Secepat mungkin',
        aslateaspossible    : 'Selewat-lewatnya',
        muststarton         : 'Mesti mula pada',
        mustfinishon        : 'Mesti selesai ada',
        startnoearlierthan  : 'Mula tidak awal daripada',
        startnolaterthan    : 'Mula tidak lewat daripada',
        finishnoearlierthan : 'Selesai tidak awal daripada',
        finishnolaterthan   : 'Selesai tidak lewat daripada'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Ke hadapan',
        Backward      : 'Ke belakang',
        inheritedFrom : 'Diwarisi dari',
        enforcedBy    : 'Dikuatkuasakan oleh'
    },

    CalendarField : {
        'Default calendar' : 'Kalendar lalai'
    },

    TaskEditorBase : {
        Information   : 'Maklumat',
        Save          : 'Simpan',
        Cancel        : 'Batal',
        Delete        : 'Hapus',
        calculateMask : 'Mengira...',
        saveError     : 'Tak boleh simpan, sila betulkan ralat dahulu',
        repeatingInfo : 'Melihat acara berulang',
        editRepeating : 'Edit'
    },

    TaskEdit : {
        'Edit task'            : 'Edit tugas',
        ConfirmDeletionTitle   : 'Sahkan penghapusan',
        ConfirmDeletionMessage : 'Adakah anda pasti mahu menghapuskan peristiwa?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Umum',
        Name         : 'Nama',
        Resources    : 'Sumber',
        '% complete' : '% selesai',
        Duration     : 'Tempoh',
        Start        : 'Mula',
        Finish       : 'Selesai',
        Effort       : 'Usaha',
        Preamble     : 'Mukadimah',
        Postamble    : 'Pasca Mukadimah'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Umum',
        Name         : 'Nama',
        '% complete' : '% selesai',
        Duration     : 'Tempoh',
        Start        : 'Mula',
        Finish       : 'Selesai',
        Effort       : 'Usaha',
        Dates        : 'Tarikh'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Lanjutan',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Mod Penjadualan',
        'Effort driven'            : 'Didorong usaha',
        'Manually scheduled'       : 'Dijadualkan secara manual',
        'Constraint type'          : 'Jenis kekang',
        'Constraint date'          : 'Tarikh kekang',
        Inactive                   : 'Tidak aktif',
        'Ignore resource calendar' : 'Abai kalendar sumber'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Lanjutan',
        Calendar                   : 'Kalendar',
        'Scheduling mode'          : 'Mod Penjadualan',
        'Effort driven'            : 'Didorong usaha',
        'Manually scheduled'       : 'Dijadualkan secara manual',
        'Constraint type'          : 'Jenis kekang',
        'Constraint date'          : 'Tarikh kekang',
        Constraint                 : 'Kekang',
        Rollup                     : 'Menggulung',
        Inactive                   : 'Tidak aktif',
        'Ignore resource calendar' : 'Abai kalendar sumber',
        'Scheduling direction'     : 'Arah jadual'
    },

    DependencyTab : {
        Predecessors      : 'Pendahulu',
        Successors        : 'Pengganti',
        ID                : 'ID',
        Name              : 'Nama',
        Type              : 'Jenis',
        Lag               : 'Sela',
        cyclicDependency  : 'Kebergantungan kitaran',
        invalidDependency : 'Kebergantungan tak sah'
    },

    NotesTab : {
        Notes : 'Nota'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Sumber',
        Resource  : 'Sumber',
        Units     : 'Unit'
    },

    RecurrenceTab : {
        title : 'Ulang'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Tempoh Tetap',
        'Fixed Units'    : 'Unit Tetap',
        'Fixed Effort'   : 'Usaha Tetap'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} daripada {available}</span> diperuntukkan',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} daripada {available}</span> diperuntukkan',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} daripada {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} daripada {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Pada {startDate}<br><span class="{cls}">{allocated} daripada {available}</span> diperuntukkan:<br>{assignments}',
        plusMore              : '+{value} lagi'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> diperuntukkan',
        barTipOnDate          : '<b>{event}</b> pada {startDate}<br><span class="{cls}">{allocated}</span> diperuntukkan',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} daripada {available}</span> diperuntukkan:<br>{assignments}',
        groupBarTipOnDate     : 'Pada {startDate}<br><span class="{cls}">{allocated} daripada {available}</span> diperuntukkan:<br>{assignments}',
        plusMore              : '+{value} lagi',
        nameColumnText        : 'Sumber / Peristiwa'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Batalkan perubahan dan jangan lakukan apa-apa',
        schedulingConflict : 'Konflik penjadualan',
        emptyCalendar      : 'Ralat konfigurasi kalendar',
        cycle              : 'Kitaran penjadualan',
        Apply              : 'Guna'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Sila pilih kebergantungan:',
        invalidDependencyLabel : 'Terdapat kebergantungan tak sah yang terlibat yang perlu ditangani:'
    },

    DependencyEdit : {
        Active : 'Aktif'
    },

    SchedulerProBase : {
        propagating     : 'Mengira projek',
        storePopulation : 'Memuat data',
        finalizing      : 'Memuktamad keputusan'
    },

    EventSegments : {
        splitEvent    : 'Pecah acara',
        renameSegment : 'Nama semula'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Menyahsarangan tak dibenarkan',
        nestingNotAllowed   : 'Sarangan tak dibenarkan'
    },

    VersionGrid : {
        compare       : 'Bandingkan',
        description   : 'Penerangan',
        occurredAt    : 'Berlaku pada',
        rename        : 'Tukar nama',
        restore       : 'Kembalikan',
        stopComparing : 'Berhenti bandingkan'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tugas',
            AssignmentModel : 'tugasan',
            DependencyModel : 'pautan',
            ProjectModel    : 'projek',
            ResourceModel   : 'sumber',
            other           : 'objek'
        },
        entityNamesPlural : {
            TaskModel       : 'tugas',
            AssignmentModel : 'tugasan',
            DependencyModel : 'pautan',
            ProjectModel    : 'projek',
            ResourceModel   : 'sumber',
            other           : 'objek'
        },
        transactionDescriptions : {
            update : 'Tukar {n} {entities}',
            add    : 'Tambah {n} {entities}',
            remove : 'Buang {n} {entities}',
            move   : 'Alih {n} {entities}',
            mixed  : 'Tukar {n} {entities}'
        },
        addEntity         : 'Tambah {type} **{name}**',
        removeEntity      : 'Buang {type} **{name}**',
        updateEntity      : 'Tukar {type} **{name}**',
        moveEntity        : 'Alih {type} **{name}** from {from} to {to}',
        addDependency     : 'Tambah pautan daripada **{from}** sehingga **{to}**',
        removeDependency  : 'Buang pautan daripada **{from}** sehingga **{to}**',
        updateDependency  : 'Edit pautan daripada **{from}** sehingga **{to}**',
        addAssignment     : 'Tugaskan **{resource}** sehingga **{event}**',
        removeAssignment  : 'Buang tugasan **{resource}** daripada **{event}**',
        updateAssignment  : 'Edit tugasan **{resource}** sehingga **{event}**',
        noChanges         : 'Tiada perubahan',
        nullValue         : 'tiada',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Nyahbuat perubahan',
        redid             : 'Buat semula perubahan',
        editedTask        : 'Edit properti tugas',
        deletedTask       : 'Padam tugas',
        movedTask         : 'Alih satu tugas',
        movedTasks        : 'Alih tugas'
    }
};

export default LocaleHelper.publishLocale(locale);
