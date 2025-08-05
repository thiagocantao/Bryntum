import LocaleManager from '../../Common/localization/LocaleManager.js';

const locale = {

    localeName: 'Pt_BR',
    localeDesc : 'Português brasileiro',

    //region Columns

    TemplateColumn : {
        noTemplate: 'TemplateColumn precisa de um modelo',
        noFunction: 'TemplateColumn.template deve ser uma função'
    },

    ColumnStore : {
        columnTypeNotFound : data => `Tipo de coluna '${data.type}' não registrado`
    },

    //endregion

    //region Mixins

    InstancePlugin : {
        fnMissing: data => `Tentando encadear fn ${data.plugIntoName}#${data.fnName}, mas plugin fn ${data.pluginName}#${data.fnName} não existe`,
        overrideFnMissing: data => `Tentando substituir fn ${data.plugIntoName}#${data.fnName}, mas plugin fn ${data.pluginName}#${data.fnName} não existe`
    },

    //endregion

    //region Features

    ColumnPicker : {
        columnsMenu     : 'Colunas',
        hideColumn      : 'Ocultar coluna',
        hideColumnShort : 'Ocultar'
    },

    Filter : {
        applyFilter: 'Aplicar filtro',
        filter       : 'Filtro',
        editFilter   : 'Editar filtro',
        on           : 'Em',
        before       : 'Antes',
        after        : 'Depois',
        equals       : 'Igual a',
        lessThan     : 'Menos que',
        moreThan     : 'Mais que',
        removeFilter : 'Remover filtro'
    },

    FilterBar : {
        enableFilterBar: 'Mostrar barra de filtro',
        disableFilterBar: 'Ocultar barra de filtro'
    },

    Group : {
        groupAscending: 'Group ascending',
        groupDescending: 'Grupo descendente',
        groupAscendingShort: 'Ascending',
        groupDescendingShort: 'Descending',
        stopGrouping: 'Pare de agrupar',
        stopGroupingShort: 'Pare'
    },

    Search : {        
        searchForValue: 'Pesquisar valor'
    },
   
    Sort : {
        'sortAscending'          : 'Ordernar ascendente',
        'sortDescending'         : 'Classificar em ordem decrescente',
        'multiSort'              : 'Multi-ordenação',
        'removeSorter'           : 'Remover classificador',
        'addSortAscending'       : 'Adicionar classificador ascendente',
        'addSortDescending'      : 'Adicionar classificador descendente',
        'toggleSortAscending'    : 'Alterar para ascendente',
        'toggleSortDescending'   : 'Alterar para descendente',
        'sortAscendingShort'     : 'Ascendente',
        'sortDescendingShort'    : 'Descendente',
        'removeSorterShort'      : 'Remover',
        'addSortAscendingShort'  : '+ Crescente',
        'addSortDescendingShort': '+ Decrescente'
    },

    Tree : {
        noTreeColumn: 'Para usar o recurso de árvore, uma coluna deve ser configurada com a tree: true'
    },

    //endregion

    //region Grid

     Grid : {
        featureNotFound: data => `O recurso '${data}' não está disponível, verifique se você o importou`,
        invalidFeatureNameFormat: data => `O nome do recurso inválido '${data}' deve começar com uma letra minúscula`,
        removeRow                : 'Excluir linha',
        removeRows               : 'Excluir linhas',
        loadMask                 : 'Carregando...',
        loadFailedMessage        : 'Falha no carregamento dos dados.',
        moveColumnLeft           : 'Mover para a seção esquerda',
        moveColumnRight          : 'Mover para a seção direita',
        noRows                   : 'Nenhuma linha para exibir'
    },

    //endregion


    //region Widgets

    Field : {
        invalidValue          : 'Valor de campo inválido',
        minimumValueViolation : 'Violação de valor mínimo',
        maximumValueViolation : 'Violação de valor máximo',
        fieldRequired         : 'Este campo é obrigatório',
        validateFilter: 'O valor deve ser selecionado da lista'
    },

    DateField : {
        invalidDate: 'Entrada de data inválida'
    },

    TimeField : {
        invalidTime: 'Entrada de hora inválida'
    },

    //endregion

    //region Others

    DateHelper : {
        locale       : 'pt-BR',
        shortWeek    : 'S',
        shortQuarter : 'T',
        week         : 'Semana',
        weekStartDay : 0,
        unitNames    : [
            { single : 'millisegundo', plural : 'ms',       abbrev : 'ms' },
            { single : 'segundo',      plural : 'segundos',  abbrev : 's' },
            { single : 'minuto',      plural : 'minutos',  abbrev : 'min' },
            { single : 'hora',        plural : 'horas',    abbrev : 'h' },
            { single : 'dia',         plural : 'dias',     abbrev : 'd' },
            { single : 'semana',        plural : 'semanas',    abbrev : 's' },
            { single : 'mês',       plural : 'meses',   abbrev : 'mon' },
            { single : 'trimestre',     plural : 'trimestres', abbrev : 't' },
            { single : 'ano',        plural : 'anos',    abbrev : 'ano' }
        ],
        // Used to build a RegExp for parsing time units.
        // The full names from above are added into the generated Regexp.
        // So you may type "2 w" or "2 wk" or "2 week" or "2 weeks" into a DurationField.
        // When generating its display value though, it uses the full localized names above.
        unitAbbreviations : [
            ['mil'],
            ['s', 'seg'],
            ['m', 'min'],
            ['h', 'hr'],
            ['d'],
            ['s', 'sm'],
            ['m', 'mes'],
            ['t', 'tri'],
            ['a', 'ano']
        ],
        parsers : {
            'L'  : 'MM/DD/YYYY',
            'LT' : 'HH:mm A'
        },
        ordinalSuffix : number => number + ({ '1' : 'st', '2' : 'nd', '3' : 'rd' }[number[number.length - 1]] || 'th')
    },

    BooleanCombo : {
        'Yes' : 'Sim',
        'No'  : 'Não'
    }

    //endregion
};

export default locale;

LocaleManager.registerLocale('Pt_BR', { desc : 'Português brasileiro', locale : locale });
