using DevExpress.DashboardCommon.Localization;
using DevExpress.Utils.Localization.Internal;

/// <summary>
/// Summary description for CDIS_DashboardViewerLocalizer
/// </summary>
public class CDIS_DashboardLocalizer : DashboardLocalizer
{
    public static void Activate()
    {
        CDIS_DashboardLocalizer localizer = new CDIS_DashboardLocalizer();
        DefaultActiveLocalizerProvider<DashboardStringId> provider =
             new DefaultActiveLocalizerProvider<DashboardStringId>(localizer);
        CDIS_DashboardLocalizer.SetActiveLocalizerProvider(provider);
    }

    public override string GetLocalizedString(DashboardStringId id)
    {
        if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "pt")
        {
            switch (id)
            {
                case DashboardStringId.ActionAllowMultiselection: return "Seleção Múltipla";
                case DashboardStringId.ActionClearMasterFilter: return "Limpar Filtro Mestre";
                case DashboardStringId.ActionClearSelection: return "Limpar Seleção";
                case DashboardStringId.ActionExportTo: return "Exportar para";
                case DashboardStringId.ActionExportToImage: return "Exportar para Imagem";
                case DashboardStringId.ActionExportToPdf: return "Exportar para PDF";
                case DashboardStringId.ActionOtherValues: return "Valores";
                case DashboardStringId.ActualValueCaption: return "Atual";
                case DashboardStringId.AutoArrangeContent: return "Auto Organizar Conteúdo";
                case DashboardStringId.AutoFitPageCount: return "Auto Ajuste de Contagem de Página";
                case DashboardStringId.AxisXNameArguments: return "Argumentos";
                case DashboardStringId.AxisYNameCount: return "Contar";
                case DashboardStringId.AxisYNameValues: return "Valores";
                case DashboardStringId.ButtonCancel: return "Cancelar";
                case DashboardStringId.ButtonExport: return "Exportar";
                case DashboardStringId.ButtonOK: return "OK";
                case DashboardStringId.ButtonReset: return "Redefinir";
                case DashboardStringId.ButtonSubmit: return "Submeter";
                case DashboardStringId.CloseCaption: return "Fechar";
                case DashboardStringId.DataItemsHeader: return "DADOS";
                case DashboardStringId.DateTimeGroupIntervalDateHour: return "Data-Hora";
                case DashboardStringId.FilterElementShowAllItem: return "(Todos)";
                case DashboardStringId.DashboardStatePosition: return "Estado do Filtro";
                case DashboardStringId.FilterStatePresentationAfterAndSplitPage: return "Página separadora";
                case DashboardStringId.FilterStatePresentationNone: return "Nenhum";
                case DashboardStringId.FitToPageWidth: return "Ajustar à largura da página";
                case DashboardStringId.GridClearSorting: return "Limpar classificação";
                case DashboardStringId.GridResetColumnWidths: return "Redefinir a largura das colunas";
                case DashboardStringId.GridSortAscending: return "Classificação crescente";
                case DashboardStringId.GridSortDescending: return "Classificação decrescente";
                case DashboardStringId.GroupIntervalNone: return "Sem Agrupamento";
                case DashboardStringId.HighCaption: return "Alto";
                case DashboardStringId.ImageFormat: return "Formato da Imagem";
                case DashboardStringId.LowCaption: return "Baixo";
                case DashboardStringId.MapAttributeDimension: return "Atributo";
                case DashboardStringId.MessageGridHasNoData: return "Não há dados a serem exibidos";
                case DashboardStringId.OpenCaption: return "Abrir";
                case DashboardStringId.PageLayout: return "Layout da Página";
                case DashboardStringId.PageLayoutAuto: return "Auto";
                case DashboardStringId.PageLayoutLandscape: return "Paisagem";
                case DashboardStringId.PageLayoutPortrait: return "Retrato";
                case DashboardStringId.PaperKind: return "Tamanho";
                case DashboardStringId.PaperKindExecutive: return "Executivo";
                case DashboardStringId.PaperKindLetter: return "Carta";
                case DashboardStringId.ParametersFormCaption: return "Parâmetros";
                case DashboardStringId.ParametersSelectorText: return "Selecione...";
                case DashboardStringId.PivotGridGrandTotal: return "Total geral";
                case DashboardStringId.PivotGridTotal: return "Total de {0}";
                case DashboardStringId.PrintHeadersOnEveryPage: return "Imprimir cabeçalhos em todas as páginas";
                case DashboardStringId.Resolution: return "Resolução (dpi)";
                case DashboardStringId.ScaleFactor: return "Fator de Escala";
                case DashboardStringId.ScaleMode: return "Moddo de Escala";
                case DashboardStringId.ScaleModeAutoFitToPagesWidth: return "Auto ajustar à largura da página";
                case DashboardStringId.ScaleModeNone: return "Nenhum";
                case DashboardStringId.ScaleModeUseScaleFactor: return "Fator de escala de utilização";
                case DashboardStringId.TopNOthersValue: return "Outros";
                default:
                    return base.GetLocalizedString(id);
            }
        }
        else
        {
            switch (id)
            {
                case DashboardStringId.ActionAllowMultiselection: return "Multiple Selection";
                case DashboardStringId.ActionClearMasterFilter: return "Clear Master Filter";
                case DashboardStringId.ActionClearSelection: return "Clear Selection";
                case DashboardStringId.ActionExportTo: return "Export to";
                case DashboardStringId.ActionExportToImage: return "Export to Image";
                case DashboardStringId.ActionExportToPdf: return "Export to PDF";
                case DashboardStringId.ActionOtherValues: return "Values";
                case DashboardStringId.ActualValueCaption: return "Current";
                case DashboardStringId.AutoArrangeContent: return "Auto Organizing Content";
                case DashboardStringId.AutoFitPageCount: return "Auto Page Adjustment Adjustment";
                case DashboardStringId.AxisXNameArguments: return "Arguments";
                case DashboardStringId.AxisYNameCount: return "Count";
                case DashboardStringId.AxisYNameValues: return "Values";
                case DashboardStringId.ButtonCancel: return "Cancel";
                case DashboardStringId.ButtonExport: return "Export";
                case DashboardStringId.ButtonOK: return "OK";
                case DashboardStringId.ButtonReset: return "Reset";
                case DashboardStringId.ButtonSubmit: return "Submit";
                case DashboardStringId.CloseCaption: return "Close";
                case DashboardStringId.DataItemsHeader: return "DATA";
                case DashboardStringId.DateTimeGroupIntervalDateHour: return "Date-Time";
                case DashboardStringId.FilterElementShowAllItem: return "(All)";
                case DashboardStringId.DashboardStatePosition: return "Filter State";
                case DashboardStringId.FilterStatePresentationAfterAndSplitPage: return "Separator Page";
                case DashboardStringId.FilterStatePresentationNone: return "None";
                case DashboardStringId.FitToPageWidth: return "Fit to page width";
                case DashboardStringId.GridClearSorting: return "Clear sort";
                case DashboardStringId.GridResetColumnWidths: return "Reset the width of the columns";
                case DashboardStringId.GridSortAscending: return "Growing sort";
                case DashboardStringId.GridSortDescending: return "Decreasing sorting";
                case DashboardStringId.GroupIntervalNone: return "No Grouping";
                case DashboardStringId.HighCaption: return "High";
                case DashboardStringId.ImageFormat: return "Image Format";
                case DashboardStringId.LowCaption: return "Low";
                case DashboardStringId.MapAttributeDimension: return "Attribute";
                case DashboardStringId.MessageGridHasNoData: return "There is no data for the same";
                case DashboardStringId.OpenCaption: return "Open";
                case DashboardStringId.PageLayout: return "Page Layout";
                case DashboardStringId.PageLayoutAuto: return "Auto";
                case DashboardStringId.PageLayoutLandscape: return "Landscape";
                case DashboardStringId.PageLayoutPortrait: return "Portrait";
                case DashboardStringId.PaperKind: return "Size";
                case DashboardStringId.PaperKindExecutive: return "Execute";
                case DashboardStringId.PaperKindLetter: return "Letter";
                case DashboardStringId.ParametersFormCaption: return "Parameters";
                case DashboardStringId.ParametersSelectorText: return "Select ...";
                case DashboardStringId.PivotGridGrandTotal: return "General Total";
                case DashboardStringId.PivotGridTotal: return "Total {0}";
                case DashboardStringId.PrintHeadersOnEveryPage: return "Print Valves on All Pages";
                case DashboardStringId.Resolution: return "Resolution (dpi)";
                case DashboardStringId.ScaleFactor: return "Scale Factor";
                case DashboardStringId.ScaleMode: return "Scale Modes";
                case DashboardStringId.ScaleModeAutoFitToPagesWidth: return "Auto call to page width";
                case DashboardStringId.ScaleModeNone: return "Nenhum";
                case DashboardStringId.ScaleModeUseScaleFactor: return "Scale Factor Usage";
                case DashboardStringId.TopNOthersValue: return "Other";
                default:
                    return base.GetLocalizedString(id);


            }
        }
    }
}