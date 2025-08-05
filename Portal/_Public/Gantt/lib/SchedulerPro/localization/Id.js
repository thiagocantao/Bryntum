import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Id.js';
import '../../Scheduler/localization/Id.js';

const locale = {

    localeName : 'Id',
    localeDesc : 'Bahasa Indonesia',
    localeCode : 'id',

    ConstraintTypePicker : {
        none                : 'Tidak ada',
        assoonaspossible    : 'Secepat mungkin',
        aslateaspossible    : 'Se-lambat mungkin',
        muststarton         : 'Harus dimulai pada',
        mustfinishon        : 'Harus diselesaikan pada',
        startnoearlierthan  : 'Dimulai secepatnya pada',
        startnolaterthan    : 'Dimulai selambatnya pada',
        finishnoearlierthan : 'Diselesaikan secepatnya pada',
        finishnolaterthan   : 'Diselesaikan selambatnya pada'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Maju',
        Backward      : 'Mundur',
        inheritedFrom : 'Diwarisi dari',
        enforcedBy    : 'Dipaksakan oleh'
    },

    CalendarField : {
        'Default calendar' : 'Kalender default'
    },

    TaskEditorBase : {
        Information   : 'Informasi',
        Save          : 'Simpan',
        Cancel        : 'Batalkan',
        Delete        : 'Hapus',
        calculateMask : 'Menghitung...',
        saveError     : 'Tidak dapat menyimpan, perbaiki kesalahan dulu',
        repeatingInfo : 'Melihat peristiwa yang berulang',
        editRepeating : 'Mengedit'
    },

    TaskEdit : {
        'Edit task'            : 'Edit tugas',
        ConfirmDeletionTitle   : 'Konfirmasi penghapusan',
        ConfirmDeletionMessage : 'Anda ingin menghapus acara?'
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
        Resources    : 'Sumber daya',
        '% complete' : '% selesai',
        Duration     : 'Durasi',
        Start        : 'Mulai',
        Finish       : 'Selesaikan',
        Effort       : 'Upaya',
        Preamble     : 'Pembuka',
        Postamble    : 'Penutup'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Umum',
        Name         : 'Nama',
        '% complete' : '% selesai',
        Duration     : 'Durasi',
        Start        : 'Mulai',
        Finish       : 'Selesaikan',
        Effort       : 'Upaya',
        Dates        : 'Tanggal'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Lanjutan',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Mode penjadwalan',
        'Effort driven'            : 'Upaya yang dilakukan',
        'Manually scheduled'       : 'Dijadwalkan secara manual',
        'Constraint type'          : 'Jenis batasan',
        'Constraint date'          : 'Tanggal batasan',
        Inactive                   : 'Tidak aktif',
        'Ignore resource calendar' : 'Mengabaikan kalender resource'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Lanjutan',
        Calendar                   : 'Kalender',
        'Scheduling mode'          : 'Mode penjadwalan',
        'Effort driven'            : 'Upaya yang dilakukan',
        'Manually scheduled'       : 'Dijadwalkan secara manual',
        'Constraint type'          : 'Jenis batasan',
        'Constraint date'          : 'Tanggal batasan',
        Constraint                 : 'Batasan',
        Rollup                     : 'Penggabungan',
        Inactive                   : 'Tidak aktif',
        'Ignore resource calendar' : 'Mengabaikan kalender resource',
        'Scheduling direction'     : 'Arah penjadwalan'
    },

    DependencyTab : {
        Predecessors      : 'Pendahulu',
        Successors        : 'Penerus',
        ID                : 'ID',
        Name              : 'Nama',
        Type              : 'Jenis',
        Lag               : 'Lag',
        cyclicDependency  : 'Dependensi siklik',
        invalidDependency : 'Dependensi tidak valid'
    },

    NotesTab : {
        Notes : 'Catatan'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Sumber daya',
        Resource  : 'Sumber daya',
        Units     : 'Unit'
    },

    RecurrenceTab : {
        title : 'Mengulangi'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Durasi Tetap',
        'Fixed Units'    : 'Unit Tetap',
        'Fixed Effort'   : 'Upaya Tetap'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} dari {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan:<br>{assignments}',
        groupBarTipOnDate     : 'Pada {startDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan:<br>{assignments}',
        plusMore              : '+{value} lagi'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> yang dialokasikan',
        barTipOnDate          : '<b>{event}</b> pada {startDate}<br><span class="{cls}">{allocated}</span> yang dialokasikan',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan:<br>{assignments}',
        groupBarTipOnDate     : 'Pada {startDate}<br><span class="{cls}">{allocated} dari {available}</span> yang dialokasikan:<br>{assignments}',
        plusMore              : '+{value} lagi',
        nameColumnText        : 'Sumber daya/Acara'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Batalkan perubahan dan jangan terapkan apa pun',
        schedulingConflict : 'Konflik penjadwalan',
        emptyCalendar      : 'Kesalahan konfigurasi kalender',
        cycle              : 'Siklus penjadwalan',
        Apply              : 'Terapkan'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Pilih dependensi:',
        invalidDependencyLabel : 'Ada dependensi tidak valid yang terlibat yang harus ditangani:'
    },

    DependencyEdit : {
        Active : 'Aktif'
    },

    SchedulerProBase : {
        propagating     : 'Menghitung proyek',
        storePopulation : 'Memuat data',
        finalizing      : 'Memfinalisasi hasil'
    },

    EventSegments : {
        splitEvent    : 'Bagikan peristiwa',
        renameSegment : 'Ganti nama'
    },

    NestedEvents : {
        deNestingNotAllowed : 'De-nesting tidak diizinkan',
        nestingNotAllowed   : 'Nesting tidak diizinkan'
    },

    VersionGrid : {
        compare       : 'Bandingkan',
        description   : 'Deskripsi',
        occurredAt    : 'Terjadi pada',
        rename        : 'Ganti nama',
        restore       : 'Pulihkan',
        stopComparing : 'Berhenti membandingkan'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tugas',
            AssignmentModel : 'penugasan',
            DependencyModel : 'tautan',
            ProjectModel    : 'proyek',
            ResourceModel   : 'resource',
            other           : 'objek'
        },
        entityNamesPlural : {
            TaskModel       : 'tugas',
            AssignmentModel : 'penugasan',
            DependencyModel : 'tautan',
            ProjectModel    : 'proyek',
            ResourceModel   : 'resource',
            other           : 'objek'
        },
        transactionDescriptions : {
            update : 'Mengubah {n} {entities}',
            add    : 'Menambahkan {n} {entities}',
            remove : 'Menghapus {n} {entities}',
            move   : 'Memindahkan {n} {entities}',
            mixed  : 'Mengubah {n} {entities}'
        },
        addEntity         : 'Menambahkan {type} **{name}**',
        removeEntity      : 'Menghapus {type} **{name}**',
        updateEntity      : 'Mengubah {type} **{name}**',
        moveEntity        : 'Memindahkan {type} **{name}** dari {from} ke {to}',
        addDependency     : 'Menambahkan tautan dari **{from}** ke **{to}**',
        removeDependency  : 'Menghapus tautan dari **{from}** ke **{to}**',
        updateDependency  : 'Mengedit tautan dari **{from}** ke **{to}**',
        addAssignment     : 'Menugaskan **{resource}** ke **{event}**',
        removeAssignment  : 'Menghapus penugasan **{resource}** dari **{event}**',
        updateAssignment  : 'Mengedit penugasan **{resource}** ke **{event}**',
        noChanges         : 'Tidak ada perubahan',
        nullValue         : 'tidak ada',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Mengurungkan perubahan',
        redid             : 'Mengulangi perubahan',
        editedTask        : 'Mengedit properti tugas',
        deletedTask       : 'Menghapus tugas',
        movedTask         : 'Memindahkan tugas',
        movedTasks        : 'Memindahkan tugas-tugas'
    }
};

export default LocaleHelper.publishLocale(locale);
