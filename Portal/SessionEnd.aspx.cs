using System;
using System.Collections.Specialized;

public partial class SessionEnd : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(sessaoAtiva());
    }

    private bool sessaoAtiva()
    {
        bool bSessaoAtiva;

        if (Session["infoSistema"] == null)
        {
            bSessaoAtiva = false;
        }
        else
        {
            OrderedDictionary infoSistema = (OrderedDictionary)Session["infoSistema"];
            bSessaoAtiva = infoSistema.Contains("IDUsuarioLogado");
        }
        return bSessaoAtiva;
    }
}