using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;


/// <summary>
/// Classe responsável por traduzir os textos que serão apresentados ao usuário
/// </summary>
public class traducao : Page
{
    public traducao(string idioma, params ASPxWebControl[] controles)
    {
        // Traduzir os objetos da página
        UICulture = idioma;
        Culture = idioma;

        foreach (object controle in controles)
        {
            if (controle is ASPxCallbackPanel)
                ASPxCallbackPanel(controle as ASPxCallbackPanel);
            else if (controle is ASPxGridView)
                ASPxGridView(controle as ASPxGridView);
        }
    }

    public void ASPxGridView(ASPxGridView grid)
    {
        grid.SettingsText.EmptyDataRow = Resources.traducao.ctrl_EmptyDataRow;
    }

    public void ASPxCallbackPanel(ASPxCallbackPanel pnCallback)
    {
        //pnCallback.LoadingPanelText = Resources.traducao.ctrl_Loading + "&hellip;";
        pnCallback.SettingsLoadingPanel.Text = Resources.traducao.ctrl_Loading + "&hellip;";
    }
}
