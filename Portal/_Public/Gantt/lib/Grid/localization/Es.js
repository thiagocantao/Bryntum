import LocaleHelper from '../../Core/localization/LocaleHelper.js';
import '../../Core/localization/Es.js';

const emptyString = new String();

const locale = {

    localeName : 'Es',
    localeDesc : 'Español',
    localeCode : 'es',

    ColumnPicker : {
        column          : 'Columna',
        columnsMenu     : 'Columnas',
        hideColumn      : 'Ocultar columna',
        hideColumnShort : 'Ocultar',
        newColumns      : 'Nuevas columnas'
    },

    Filter : {
        applyFilter   : 'Aplicar filtro',
        filter        : 'Filtro',
        editFilter    : 'Editar filtro',
        on            : 'Activo',
        before        : 'Antes',
        after         : 'Después',
        equals        : 'Equivale a',
        lessThan      : 'Inferior a',
        moreThan      : 'Superior a',
        removeFilter  : 'Quitar filtro',
        disableFilter : 'Deshabilitar filtro'
    },

    FilterBar : {
        enableFilterBar  : 'Mostrar barra de filtro',
        disableFilterBar : 'Ocultar barra de filtro'
    },

    Group : {
        group                : 'Agrupar',
        groupAscending       : 'Agrupar ascendentes',
        groupDescending      : 'Agrupar descendentes',
        groupAscendingShort  : 'Ascendentes',
        groupDescendingShort : 'Descendentes',
        stopGrouping         : 'Dejar de agrupar',
        stopGroupingShort    : 'Dejar'
    },

    HeaderMenu : {
        moveBefore     : text => `Mover delante de "${text}"`,
        moveAfter      : text => `Mover detrás de "${text}"`,
        collapseColumn : 'Comprimir columna',
        expandColumn   : 'Expandir columna'
    },

    ColumnRename : {
        rename : 'Renombrar'
    },

    MergeCells : {
        mergeCells  : 'Combinar celdas',
        menuTooltip : 'Combinar celdas con el mismo valor al ordenarlas por esta columna'
    },

    Search : {
        searchForValue : 'Buscar valor'
    },

    Sort : {
        sort                   : 'Ordenar',
        sortAscending          : 'Orden ascendente',
        sortDescending         : 'Orden descendente',
        multiSort              : 'Orden múltiple',
        removeSorter           : 'Eliminar criterio de orden',
        addSortAscending       : 'Añadir criterio ascendente',
        addSortDescending      : 'Añadir criterio ascendente',
        toggleSortAscending    : 'Cambiar a ascendente',
        toggleSortDescending   : 'Cambiar a descendente',
        sortAscendingShort     : 'Ascendente',
        sortDescendingShort    : 'Descendente',
        removeSorterShort      : 'Eliminar',
        addSortAscendingShort  : '+ Ascendente',
        addSortDescendingShort : '+ Descendente'
    },

    Split : {
        split        : 'Dividir',
        unsplit      : 'No dividir',
        horizontally : 'Horizontalmente',
        vertically   : 'Verticalmente',
        both         : 'Ambos'
    },

    Column : {
        columnLabel : column => `${column.text ? `${column.text} columna. ` : ''}ESPACIO para el menú contextual${column.sortable ? ', INTRO para ordenar' : ''}`,
        cellLabel   : emptyString
    },

    Checkbox : {
        toggleRowSelect : 'Alternar selección de filas',
        toggleSelection : 'Alternar selección de todo el conjunto de datos'
    },

    RatingColumn : {
        cellLabel : column => `${column.text ? column.text : ''} ${column.location?.record ? `clasificación : ${column.location.record.get(column.field) || 0}` : ''}`
    },

    GridBase : {
        loadFailedMessage  : 'Fallo al cargar los datos',
        syncFailedMessage  : 'Fallo al sincronizar los datos',
        unspecifiedFailure : 'Fallo no especificado',
        networkFailure     : 'Error de red',
        parseFailure       : 'Fallo al analizar la respuesta del servidor',
        serverResponse     : 'Respuesta del servidor:',
        noRows             : 'Sin registros que mostrar',
        moveColumnLeft     : 'Mover a la sección izquierda',
        moveColumnRight    : 'Mover a la sección derecha',
        moveColumnTo       : region => `Mover columna a ${region}`
    },

    CellMenu : {
        removeRow : 'Eliminar'
    },

    RowCopyPaste : {
        copyRecord  : 'Copiar',
        cutRecord   : 'Cortar',
        pasteRecord : 'Pegar',
        rows        : 'filas',
        row         : 'fila'
    },

    CellCopyPaste : {
        copy  : 'Copiar',
        cut   : 'Cortar',
        paste : 'Pegar'
    },

    PdfExport : {
        'Waiting for response from server' : 'Esperando respuesta del servidor...',
        'Export failed'                    : 'Fallo al exportar',
        'Server error'                     : 'Error del servidor',
        'Generating pages'                 : 'Generando páginas...',
        'Click to abort'                   : 'Cancelar'
    },

    ExportDialog : {
        width          : '40em',
        labelWidth     : '12em',
        exportSettings : 'Exportar ajustes',
        export         : 'Exportar',
        exporterType   : 'Controlar la paginación',
        cancel         : 'Cancelar',
        fileFormat     : 'Formato de archivo',
        rows           : 'Filas',
        alignRows      : 'Alinear filas',
        columns        : 'Columnas',
        paperFormat    : 'Formato de papel',
        orientation    : 'Orientación',
        repeatHeader   : 'Repetir encabezado'
    },

    ExportRowsCombo : {
        all     : 'Todas las filas',
        visible : 'Filas visibles'
    },

    ExportOrientationCombo : {
        portrait  : 'Retrato',
        landscape : 'Panorámica'
    },

    SinglePageExporter : {
        singlepage : 'Una sola página'
    },

    MultiPageExporter : {
        multipage     : 'Páginas múltiples',
        exportingPage : ({ currentPage, totalPages }) => `Exportando página ${currentPage}/${totalPages}`
    },

    MultiPageVerticalExporter : {
        multipagevertical : 'Páginas múltiples (vertical)',
        exportingPage     : ({ currentPage, totalPages }) => `Exportando página  ${currentPage}/${totalPages}`
    },

    RowExpander : {
        loading  : 'Cargando',
        expand   : 'Expandir',
        collapse : 'Contrar'
    },

    TreeGroup : {
        group                  : 'Agrupar por',
        stopGrouping           : 'Detener agrupación',
        stopGroupingThisColumn : 'Desagrupar esta columna'
    }
};

export default LocaleHelper.publishLocale(locale);
