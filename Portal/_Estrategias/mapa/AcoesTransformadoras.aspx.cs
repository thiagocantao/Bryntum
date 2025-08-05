using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Estrategias_mapa_Macrometa : System.Web.UI.Page
{
    int codigoObjetivo = 0;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";
    private string iniciaisObjeto = "OB";
    
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        if (Request.QueryString["CodigoObjetivo"] != null && Request.QueryString["CodigoObjetivo"].ToString() != "")
            codigoObjetivo = int.Parse(Request.QueryString["CodigoObjetivo"].ToString());

        carregaProjetosFatorChave();

        cDados.aplicaEstiloVisual(this);
    }

    private void carregaProjetosFatorChave()
    {
        DataSet ds = cDados.getProjetosFatorChave(codigoObjetivo, iniciaisObjeto, codigoUsuarioResponsavel, DateTime.Now.Year);

        if (cDados.DataSetOk(ds))
        {
            gvProjetos.DataSource = ds;
            gvProjetos.DataBind();
        }
    }
}