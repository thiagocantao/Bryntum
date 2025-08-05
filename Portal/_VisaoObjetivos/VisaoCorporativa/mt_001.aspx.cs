using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _VisaoObjetivos_VisaoCorporativa_mt_001 : System.Web.UI.Page
{
    dados cDados;
    int codigoObjetivo = 0;
    int codigoEntidadeLogada = 0;
    int codigoUsuarioLogado = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
        
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (Request.QueryString["CO"] != null && Request.QueryString["CO"].ToString() != "")
            codigoObjetivo = int.Parse(Request.QueryString["CO"].ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        carregaGridProjetos();

        cDados.aplicaEstiloVisual(this);
    }
        
    private void carregaGridProjetos()
    {
        string where = string.Format(@" AND p.CodigoStatusProjeto = 3 AND p.[CodigoProjeto] IN ( 
            SELECT fnc.[CodigoProjeto] FROM {0}.{1}.f_GetProjetosEmParceria(dbo.f_GetUnidadeUsuario({3},{2})) AS [fnc] ) "
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeLogada, codigoUsuarioLogado);

        DataSet dsProjetos = cDados.getProjetosOE(codigoObjetivo, codigoEntidadeLogada, where, "");

        if (cDados.DataSetOk(dsProjetos))
        {
            gridProjetos.DataSource = dsProjetos;

            gridProjetos.DataBind();
        }
    }
}
