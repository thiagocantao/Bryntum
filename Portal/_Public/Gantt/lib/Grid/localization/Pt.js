import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Pt.js';

const emptyString = new String();

const locale = {

    localeName : 'Pt',
    localeDesc : 'Português',
    localeCode : 'pt',

    ColumnPicker : {
        column          : 'Coluna',
        columnsMenu     : 'Colunas',
        hideColumn      : 'Ocultar colunas',
        hideColumnShort : 'Ocultar',
        newColumns      : 'Novas colunas'
    },

    Filter : {
        applyFilter   : 'Aplicar filtro',
        filter        : 'Filtro',
        editFilter    : 'Editar filtro',
        on            : 'em',
        before        : 'Antes',
        after         : 'Depois',
        equals        : 'Igual a',
        lessThan      : 'Inferior a',
        moreThan      : 'Superior a',
        removeFilter  : 'Remover filtro',
        disableFilter : 'Desativar filtro'
    },

    FilterBar : {
        enableFilterBar  : 'Mostrar barra de filtro',
        disableFilterBar : 'Ocultar barra de filtro'
    },

    Group : {
        group                : 'Agrupar',
        groupAscending       : 'Agrupar por ordem ascendente',
        groupDescending      : 'Agrupar por ordem descendente',
        groupAscendingShort  : 'Ascendente',
        groupDescendingShort : 'Descendente',
        stopGrouping         : 'Parar agrupamento',
        stopGroupingShort    : 'Parar'
    },

    HeaderMenu : {
        moveBefore     : text => `Mover para antes de "${text}"`,
        moveAfter      : text => `Mover para depois de "${text}"`,
        collapseColumn : 'Minimizar coluna',
        expandColumn   : 'Ampliar coluna'
    },

    ColumnRename : {
        rename : 'Renomear'
    },

    MergeCells : {
        mergeCells  : 'Unir céculas',
        menuTooltip : 'Unir células com o mesmo valor quando ordenadas por esta coluna'
    },

    Search : {
        searchForValue : 'Pesquisar por valor'
    },

    Sort : {
        sort                   : 'Ordenar',
        sortAscending          : 'Ordenar por ordem ascendente',
        sortDescending         : 'Ordenar por ordem descendente',
        multiSort              : 'Ordenação múltipla',
        removeSorter           : 'Remover ordenação',
        addSortAscending       : 'Adicionar ordenação ascendente',
        addSortDescending      : 'Adicionar ordenação descendente',
        toggleSortAscending    : 'Alterar para ascendente',
        toggleSortDescending   : 'Alterar para descendente',
        sortAscendingShort     : 'Ascendente',
        sortDescendingShort    : 'Descendente',
        removeSorterShort      : 'Remover',
        addSortAscendingShort  : '+ Ascendente',
        addSortDescendingShort : '+ Descendente'
    },

    Split : {
        split        : 'Dividido',
        unsplit      : 'Não dividido',
        horizontally : 'Horizontalmente',
        vertically   : 'Verticalmente',
        both         : 'Ambos'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} coluna. ` : ''}ESPAÇO para menu de contexto${column.sortable ? ', ENTER para ordenar' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Ativar/desativar seleção de fila',
        toggleSelection : 'Ativar/desativar seleção de todo o conjunto de dados'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `avaliação ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'O carregamento de dados falhou!',
        syncFailedMessage  : 'A sincronização de dados falhou!',
        unspecifiedFailure : 'Falha não especificada',
        networkFailure     : 'Erro de rede',
        parseFailure       : 'Falha ao analisar a resposta do servidor',
        serverResponse     : 'Resposta do servidor:',
        noRows             : 'Sem registos para exibir',
        moveColumnLeft     : 'Mover para a secção esquerda',
        moveColumnRight    : 'Mover para a secção direita',
        moveColumnTo       : region => `Mover coluna para ${region}`
    },

    CellMenu : {
        removeRow : 'Eliminar'
    },

    RowCopyPaste : {
        copyRecord  : 'Copiar',
        cutRecord   : 'Cortar',
        pasteRecord : 'Colar',
        rows        : 'linhas',
        row         : 'linha'
    },

    CellCopyPaste : {
        copy  : 'Copiar',
        cut   : 'Cortar',
        paste : 'Colar'
    },

    PdfExport : {
        'Waiting for response from server' : 'A aguardar resposta do servidor...',
        'Export failed'                    : 'Falha ao exportar',
        'Server error'                     : 'Erro de servidor',
        'Generating pages'                 : 'A gerar páginas...',
        'Click to abort'                   : 'Cancelar'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Exportar definições',
        export         : 'Exportar',
        exporterType   : 'Controlo de paginação',
        cancel         : 'Cancelar',
        fileFormat     : 'Formato do ficheiro',
        rows           : 'Filas',
        alignRows      : 'Alinhar filas',
        columns        : 'Colunas',
        paperFormat    : 'Formato em papel',
        orientation    : 'Orientação',
        repeatHeader   : 'Repetir cabeçalho'
    },

    ExportRowsCombo : {
        all     : 'Todas as filas',
        visible : 'Filas visíveis'
    },

    ExportOrientationCombo : {
        portrait  : 'Retrato',
        landscape : 'Paisagem'
    },

    SinglePageExporter : {
        singlepage : 'Uma página'
    },

    MultiPageExporter : {
        multipage     : 'Várias páginas',
        exportingPage : ({ currentPage, totalPages }) => `Exportando página ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : ' Várias páginas (vertical)',
        exportingPage     : ({ currentPage, totalPages }) => `Exportando página ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'A carregar',
        expand   : 'Expandir',
        collapse : 'Fechar'
    },

    TreeGroup : {
        group                  : 'Agrupar por',
        stopGrouping           : 'Parar agrupamento',
        stopGroupingThisColumn : 'Remover agrupamento desta coluna'
    }
};

export default LocaleHelper.publishLocale(locale);
