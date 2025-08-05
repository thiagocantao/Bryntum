import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Es.js';
import '../../Scheduler/localization/Es.js';

const locale = {

    localeName : 'Es',
    localeDesc : 'Español',
    localeCode : 'es',

    ConstraintTypePicker : {
        none                : 'Ninguno',
        assoonaspossible    : 'Lo antes posible',
        aslateaspossible    : 'Lo más tarde posible',
        muststarton         : 'Debe empezar el',
        mustfinishon        : 'Debe terminar el',
        startnoearlierthan  : 'Empezar no antes del',
        startnolaterthan    : 'Empezar no después del',
        finishnoearlierthan : 'Terminar no antes del',
        finishnolaterthan   : 'Terminar no después del'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Adelante',
        Backward      : 'Atrás',
        inheritedFrom : 'Heredado de',
        enforcedBy    : 'Impuesto por'
    },

    CalendarField : {
        'Default calendar' : 'Calendario predeterminado'
    },

    TaskEditorBase : {
        Information   : 'Información',
        Save          : 'Guardar',
        Cancel        : 'Cancelar',
        Delete        : 'Eliminar',
        calculateMask : 'Calculando...',
        saveError     : 'No se puede guardar, corrija antes los errores',
        repeatingInfo : 'Viendo una evento que se repite',
        editRepeating : 'Editar'
    },

    TaskEdit : {
        'Edit task'            : 'Editar tarea',
        ConfirmDeletionTitle   : 'Confirmar eliminación',
        ConfirmDeletionMessage : '¿Seguro que desea eliminar el evento?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'General',
        Name         : 'Nombre',
        Resources    : 'Recursos',
        '% complete' : '% realizado',
        Duration     : 'Duración',
        Start        : 'Inicio',
        Finish       : 'Finalización',
        Effort       : 'Trabajo',
        Preamble     : 'Prólogo',
        Postamble    : 'Epílogo'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'General',
        Name         : 'Nombre',
        '% complete' : '% realizado',
        Duration     : 'Duración',
        Start        : 'Inicio',
        Finish       : 'Finalización',
        Effort       : 'Trabajo',
        Dates        : 'Fechas'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avanzado',
        Calendar                   : 'Calendario',
        'Scheduling mode'          : 'Modo de programación',
        'Effort driven'            : 'Trabajo invertido',
        'Manually scheduled'       : 'Programado manualmente',
        'Constraint type'          : 'Tipo de restricción',
        'Constraint date'          : 'Fecha de la restricción',
        Inactive                   : 'Inactivo',
        'Ignore resource calendar' : 'Ignorar calendario de recursos'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Avanzado',
        Calendar                   : 'Calendario',
        'Scheduling mode'          : 'Modo de programación',
        'Effort driven'            : 'Trabajo invertido',
        'Manually scheduled'       : 'Programado manualmente',
        'Constraint type'          : 'Tipo de restricción',
        'Constraint date'          : 'Fecha de la restricción',
        Constraint                 : 'Restricción',
        Rollup                     : 'Aplazamiento al resumen',
        Inactive                   : 'Inactivo',
        'Ignore resource calendar' : 'Ignorar calendario de recursos',
        'Scheduling direction'     : 'Dirección de programación'
    },

    DependencyTab : {
        Predecessors      : 'Anteriores',
        Successors        : 'Posteriores',
        ID                : 'DNI',
        Name              : 'Nombre',
        Type              : 'Tipo',
        Lag               : 'Retraso',
        cyclicDependency  : 'Dependencia cíclica',
        invalidDependency : 'Dependencia no válida'
    },

    NotesTab : {
        Notes : 'Notas'
    },

    ResourcesTab : {
        unitsTpl  : ({ value }) => `${value}%`,
        Resources : 'Recursos',
        Resource  : 'Recurso',
        Units     : 'Unidades'
    },

    RecurrenceTab : {
        title : 'Repetir'
    },

    SchedulingModePicker : {
        Normal           : 'Normal',
        'Fixed Duration' : 'Duración',
        'Fixed Units'    : 'Unidades fijas',
        'Fixed Effort'   : 'Trabajo fijo'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> asignados',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} de {available}</span> asignados',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} de {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'El {startDate}<br><span class="{cls}">{allocated} de {available}</span> asignado:<br>{assignments}',
        plusMore              : '+{value} más'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> asignado',
        barTipOnDate          : '<b>{event}</b> el {startDate}<br><span class="{cls}">{allocated}</span> asignado',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> asignado:<br>{assignments}',
        groupBarTipOnDate     : 'El {startDate}<br><span class="{cls}">{allocated} de {available}</span> asignado:<br>{assignments}',
        plusMore              : '+{value} más',
        nameColumnText        : 'Recurso/Evento'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Cancelar el cambio y no hacer nada',
        schedulingConflict : 'Conflicto de programación',
        emptyCalendar      : 'Error de configuración del calendario',
        cycle              : 'Programando ciclo',
        Apply              : 'Aplicar'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Seleccione una dependencia:',
        invalidDependencyLabel : 'Hay dependencias no válidas implicadas que deben ser corregidas:'
    },

    DependencyEdit : {
        Active : 'Activa'
    },

    SchedulerProBase : {
        propagating     : 'Calculando proyecto',
        storePopulation : 'Cargando datos',
        finalizing      : 'Finalizando resultados'
    },

    EventSegments : {
        splitEvent    : 'Dividir evento',
        renameSegment : 'Renombrar'
    },

    NestedEvents : {
        deNestingNotAllowed : 'Desagrupado no permitido',
        nestingNotAllowed   : 'Agrupado permitido'
    },

    VersionGrid : {
        compare       : 'Comparar',
        description   : 'Descripción',
        occurredAt    : 'Ocurrió en',
        rename        : 'Renombrar',
        restore       : 'Restaurar',
        stopComparing : 'Detener comparación'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tarea',
            AssignmentModel : 'asignación',
            DependencyModel : 'enlace',
            ProjectModel    : 'proyecto',
            ResourceModel   : 'recurso',
            other           : 'objeto'
        },
        entityNamesPlural : {
            TaskModel       : 'tares',
            AssignmentModel : 'asignaciones',
            DependencyModel : 'enlaces',
            ProjectModel    : 'proyectos',
            ResourceModel   : 'recursos',
            other           : 'objetos'
        },
        transactionDescriptions : {
            update : 'Se han cambiado {n} {entities}',
            add    : 'Se han añadido {n} {entities}',
            remove : 'Se han quitado {n} {entities}',
            move   : 'Se han movido {n} {entities}',
            mixed  : 'Se han cambiado {n} {entities}'
        },
        addEntity         : 'Se ha añadido {type} **{name}**',
        removeEntity      : 'Se ha quitado {type} **{name}**',
        updateEntity      : 'Se ha cambiado {type} **{name}**',
        moveEntity        : 'Se ha movido {type} **{name}** de {from} a {to}',
        addDependency     : 'Se ha añadido el enlace de **{from}** a **{to}**',
        removeDependency  : 'Se ha quitado el enlace de **{from}** a **{to}**',
        updateDependency  : 'Se ha editado el enlace de **{from}** a **{to}**',
        addAssignment     : 'Se ha asignado **{resource}** a **{event}**',
        removeAssignment  : 'Se ha quitado la asignación de **{resource}** a **{event}**',
        updateAssignment  : 'Se ha editado la asignación de **{resource}** a **{event}**',
        noChanges         : 'Sin cambios',
        nullValue         : 'ninguno',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Se han deshecho los cambios',
        redid             : 'Se han rehecho los cambios',
        editedTask        : 'Se han editado las propiedades de tarea',
        deletedTask       : 'Se ha eliminado una tarea',
        movedTask         : 'Se ha movido una tarea',
        movedTasks        : 'Se han movido tareas'
    }
};

export default LocaleHelper.publishLocale(locale);
