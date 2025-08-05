import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Engine/localization/Pt.js';
import '../../Scheduler/localization/Pt.js';

const locale = {

    localeName : 'Pt',
    localeDesc : 'Português',
    localeCode : 'pt',

    ConstraintTypePicker : {
        none                : 'Nenhum',
        assoonaspossible    : 'O mais cedo possível',
        aslateaspossible    : 'O mais tarde possível',
        muststarton         : 'Tem de começar em',
        mustfinishon        : 'Tem de terminar em',
        startnoearlierthan  : 'Iniciar não antes de',
        startnolaterthan    : 'Iniciar não depois de',
        finishnoearlierthan : 'Terminar não antes de',
        finishnolaterthan   : 'Terminar não depois de'
    },

    SchedulingDirectionPicker : {
        Forward       : 'Para a frente',
        Backward      : 'Para trás',
        inheritedFrom : 'Herdado de',
        enforcedBy    : 'Imposto por'
    },

    CalendarField : {
        'Default calendar' : 'Calendário predefinido'
    },

    TaskEditorBase : {
        Information   : 'Informação',
        Save          : 'Guardar',
        Cancel        : 'Cancelar',
        Delete        : 'Eliminar',
        calculateMask : 'A calcular...',
        saveError     : 'Não é possível guardar, corrija os erros primeiro',
        repeatingInfo : 'Visualizar um evento repetido',
        editRepeating : 'Editar'
    },

    TaskEdit : {
        'Edit task'            : 'Editar tarefa',
        ConfirmDeletionTitle   : 'Confirmar eliminação',
        ConfirmDeletionMessage : 'Tem a certeza de que quer eliminar o evento?'
    },

    GanttTaskEditor : {
        editorWidth : '44em'
    },

    SchedulerTaskEditor : {
        editorWidth : '32em'
    },

    SchedulerGeneralTab : {
        labelWidth   : '6em',
        General      : 'Geral',
        Name         : 'Nome',
        Resources    : 'Recursos',
        '% complete' : '% concluído',
        Duration     : 'Duração',
        Start        : 'Início',
        Finish       : 'Fim',
        Effort       : 'Esforço',
        Preamble     : 'Preâmbulo',
        Postamble    : 'Conclusão'
    },

    GeneralTab : {
        labelWidth   : '6.5em',
        General      : 'Geral',
        Name         : 'Nome',
        '% complete' : '% concluído',
        Duration     : 'Duração',
        Start        : 'Início',
        Finish       : 'Fim',
        Effort       : 'Esforço',
        Dates        : 'Datas'
    },

    SchedulerAdvancedTab : {
        labelWidth                 : '13em',
        Advanced                   : 'Avançado',
        Calendar                   : 'Calendário',
        'Scheduling mode'          : 'Modo de agendamento',
        'Effort driven'            : 'Esforço impulsionado',
        'Manually scheduled'       : 'Agendado manualmente',
        'Constraint type'          : 'Tipo de restrição',
        'Constraint date'          : 'Data de restrição',
        Inactive                   : 'Inativo',
        'Ignore resource calendar' : 'Ignorar calendário de recursos'
    },

    AdvancedTab : {
        labelWidth                 : '11.5em',
        Advanced                   : 'Avançado',
        Calendar                   : 'Calendário',
        'Scheduling mode'          : 'Modo de agendamento',
        'Effort driven'            : 'Esforço impulsionado',
        'Manually scheduled'       : 'Agendado manualmente',
        'Constraint type'          : 'Tipo de restrição',
        'Constraint date'          : 'Data de restrição',
        Constraint                 : 'Restrição',
        Rollup                     : 'Sumarização',
        Inactive                   : 'Inativo',
        'Ignore resource calendar' : 'Ignorar calendário de recursos',
        'Scheduling direction'     : 'Direção de agendamento'
    },

    DependencyTab : {
        Predecessors      : 'Anteriores',
        Successors        : 'Sucessores',
        ID                : 'ID',
        Name              : 'Nome',
        Type              : 'Tipo',
        Lag               : 'Atraso',
        cyclicDependency  : 'Dependência cíclica',
        invalidDependency : 'Dependência inválida'
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
        'Fixed Duration' : 'Duração fixa',
        'Fixed Units'    : 'Unidades fixas',
        'Fixed Effort'   : 'Esforço fixo'
    },

    ResourceHistogram : {
        barTipInRange         : '<b>{resource}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> atribuídos',
        barTipOnDate          : '<b>{resource}</b> on {startDate}<br><span class="{cls}">{allocated} de {available}</span> atribuídos',
        groupBarTipAssignment : '<b>{resource}</b> - <span class="{cls}">{allocated} de {available}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> allocated:<br>{assignments}',
        groupBarTipOnDate     : 'Em {startDate}<br><span class="{cls}">{allocated} de {available}</span> atribuídos:<br>{assignments}',
        plusMore              : '+{value} mais'
    },

    ResourceUtilization : {
        barTipInRange         : '<b>{event}</b> {startDate} - {endDate}<br><span class="{cls}">{allocated}</span> atribuídos',
        barTipOnDate          : '<b>{event}</b> em {startDate}<br><span class="{cls}">{allocated}</span> atribuídos',
        groupBarTipAssignment : '<b>{event}</b> - <span class="{cls}">{allocated}</span>',
        groupBarTipInRange    : '{startDate} - {endDate}<br><span class="{cls}">{allocated} de {available}</span> atribuídos:<br>{assignments}',
        groupBarTipOnDate     : 'Em {startDate}<br><span class="{cls}">{allocated} de {available}</span> atribuídos:<br>{assignments}',
        plusMore              : '+{value} mais',
        nameColumnText        : 'Recurso/Evento'
    },

    SchedulingIssueResolutionPopup : {
        'Cancel changes'   : 'Cancelar a alteração e não fazer nada',
        schedulingConflict : 'Conflito de agendamento',
        emptyCalendar      : 'Erro de configuração de calendário',
        cycle              : 'Ciclo de agendamento',
        Apply              : 'Aplicar'
    },

    CycleResolutionPopup : {
        dependencyLabel        : 'Selecione uma dependência:',
        invalidDependencyLabel : 'Existem dependências inválidas envolvidas que têm de ser resolvidas:'
    },

    DependencyEdit : {
        Active : 'Ativo'
    },

    SchedulerProBase : {
        propagating     : 'A calcular projeto',
        storePopulation : 'A carregar dados',
        finalizing      : 'A finalizar resultados'
    },

    EventSegments : {
        splitEvent    : 'Dividir evento',
        renameSegment : 'Renomear'
    },

    NestedEvents : {
        deNestingNotAllowed : 'A remoção de aninhamento não é permitida',
        nestingNotAllowed   : 'O aninhamento não é permitido'
    },

    VersionGrid : {
        compare       : 'Comparar',
        description   : 'Descrição',
        occurredAt    : 'Ocorreu em',
        rename        : 'Renomear',
        restore       : 'Restaurar',
        stopComparing : 'Parar comparação'
    },

    Versions : {
        entityNames : {
            TaskModel       : 'tarefa',
            AssignmentModel : 'atribuição',
            DependencyModel : 'hiperligação',
            ProjectModel    : 'projeto',
            ResourceModel   : 'recurso',
            other           : 'objeto'
        },
        entityNamesPlural : {
            TaskModel       : 'tarefas',
            AssignmentModel : 'atribuições',
            DependencyModel : 'hiperligações',
            ProjectModel    : 'projetos',
            ResourceModel   : 'recursos',
            other           : 'objetos'
        },
        transactionDescriptions : {
            update : 'Alterada(s) {n} {entities}',
            add    : 'Adicionada(s) {n} {entities}',
            remove : 'Removida(s) {n} {entities}',
            move   : 'Movida(s) {n} {entities}',
            mixed  : 'Alterada(s) {n} {entities}'
        },
        addEntity         : 'Adicionado {type} **{name}**',
        removeEntity      : 'Removido {type} **{name}**',
        updateEntity      : 'Alterado {type} **{name}**',
        moveEntity        : 'Movido {type} **{name}** de {from} para {to}',
        addDependency     : 'Adicionada hiperligação de **{from}** para **{to}**',
        removeDependency  : 'Removida hiperligação de **{from}** para **{to}**',
        updateDependency  : 'Editada hiperligação de **{from}** para **{to}**',
        addAssignment     : 'Atribuído **{resource}** para **{event}**',
        removeAssignment  : 'Removida atribuição de **{resource}** de **{event}**',
        updateAssignment  : 'Editada atribuição de **{resource}** para **{event}**',
        noChanges         : 'Sem alterações',
        nullValue         : 'nenhum',
        versionDateFormat : 'M/D/YYYY h:mm a',
        undid             : 'Alterações anuladas',
        redid             : 'Alterações refeitas',
        editedTask        : 'Propriedades da tarefa editadas',
        deletedTask       : 'Uma tarefa eliminada',
        movedTask         : 'Uma tarefa movida',
        movedTasks        : 'Tarefas movidas'
    }
};

export default LocaleHelper.publishLocale(locale);
