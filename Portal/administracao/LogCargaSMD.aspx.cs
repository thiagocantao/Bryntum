using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class administracao_DesfazerCargaSMD : System.Web.UI.Page
{
    dados cDados;
    int codigoCarga = -1;

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

        if (Request.QueryString["CC"] != null && Request.QueryString["CC"].ToString() != "")
            codigoCarga = int.Parse(Request.QueryString["CC"].ToString());

        carregaDados();
        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void carregaDados()
    {
        string comandoSQL = string.Format(@"
                      SELECT UnidadeNegocio, Indicador, QuantidadeOcorrencia
                        FROM {0}.{1}.SMD_Log_RegistrosRejeitados
                       WHERE CodigoCarga = {2}
                       ORDER BY UnidadeNegocio", cDados.getDbName(), cDados.getDbOwner(), codigoCarga);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

   
}