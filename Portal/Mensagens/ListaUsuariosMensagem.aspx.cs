using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using DevExpress.Web;

public partial class espacoTrabalho_ListaUsuariosMensagem : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (gvUsuarios.IsCallback)
            Thread.Sleep(1000);

        carregaGridUsuarios();

        gvUsuarios.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        if (Request.QueryString["Popup"] != null && Request.QueryString["Popup"].ToString() == "S")
        {
            btnSelecionar.ClientSideEvents.Click = "function(s, e) { window.parent.txtDestinatarios.SetText(txtDestinatarios.GetText()); window.parent.fechaModal(); }";
            btnFechar.ClientSideEvents.Click = "function(s, e) {  window.parent.fechaModal(); }";
        }
    }

    private void carregaGridUsuarios()
    {
        DataSet ds = cDados.getUsuarioDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), "");

        gvUsuarios.DataSource = ds;
        gvUsuarios.DataBind();

        gvUsuarios.JSProperties["cpVisibleRowCount"] = ds.Tables[0].Rows.Count;
    }
}