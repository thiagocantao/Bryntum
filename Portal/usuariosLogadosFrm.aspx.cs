using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class usuariosLogadosFrm : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        callbackSession.JSProperties["cp_Usuario"] = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        callbackSession.JSProperties["cp_IDUsuario"] = cDados.getInfoSistema("IDUsuarioLogado").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}