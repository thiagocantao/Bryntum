using DevExpress.Utils.Localization.Internal;
using DevExpress.Web.Localization;

/// <summary>
/// Summary description for CDIS_GridLocalizer
/// </summary>
public class CDIS_GridLocalizer : DevExpress.Web.Localization.ASPxGridViewResLocalizer
{
    public static void Activate()
    {
        CDIS_GridLocalizer localizer = new CDIS_GridLocalizer();
        DefaultActiveLocalizerProvider<ASPxGridViewStringId> provider = new DefaultActiveLocalizerProvider<ASPxGridViewStringId>(localizer);
        CDIS_GridLocalizer.SetActiveLocalizerProvider(provider);

    }

    public override string GetLocalizedString(ASPxGridViewStringId id)
    {
        if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "pt")
        {
            switch (id)
            {
                case ASPxGridViewStringId.AutoFilterBeginsWith: return "Começa com";
                case ASPxGridViewStringId.AutoFilterContains: return "Contém";
                case ASPxGridViewStringId.AutoFilterDoesNotContain: return "Não contém";
                case ASPxGridViewStringId.AutoFilterEndsWith: return "Termina com";
                case ASPxGridViewStringId.AutoFilterEquals: return "Equivale a";
                case ASPxGridViewStringId.AutoFilterGreater: return "É maior";
                case ASPxGridViewStringId.AutoFilterGreaterOrEqual: return "É maior ou igual";
                case ASPxGridViewStringId.AutoFilterLess: return "É menor";
                case ASPxGridViewStringId.AutoFilterLessOrEqual: return "É menor ou igual";
                case ASPxGridViewStringId.AutoFilterNotEqual: return "Não é igual";

                case ASPxGridViewStringId.HeaderFilterCancelButton: return "Cancelar";
                case ASPxGridViewStringId.HeaderFilterOkButton: return "Confirmar";
                case ASPxGridViewStringId.HeaderFilterFrom: return "De";
                case ASPxGridViewStringId.HeaderFilterLastMonth: return "Mês Anterior";
                case ASPxGridViewStringId.HeaderFilterLastWeek: return "Semana Anterior";
                case ASPxGridViewStringId.HeaderFilterLastYear: return "Ano Anterior";
                case ASPxGridViewStringId.HeaderFilterNextMonth: return "Próximo Mês";
                case ASPxGridViewStringId.HeaderFilterNextWeek: return "Próxima Semana";
                case ASPxGridViewStringId.HeaderFilterNextYear: return "Próximo Ano";
                case ASPxGridViewStringId.HeaderFilterThisMonth: return "Este Mês";
                case ASPxGridViewStringId.HeaderFilterThisWeek: return "Esta Semana";
                case ASPxGridViewStringId.HeaderFilterThisYear: return "Este Ano";
                case ASPxGridViewStringId.HeaderFilterTo: return "Até";
                case ASPxGridViewStringId.HeaderFilterToday: return "Hoje";
                case ASPxGridViewStringId.HeaderFilterTomorrow: return "Amanhã";
                case ASPxGridViewStringId.HeaderFilterYesterday: return "Ontem";
                default: return base.GetLocalizedString(id);
            }
        }

        return base.GetLocalizedString(id);
    }
}