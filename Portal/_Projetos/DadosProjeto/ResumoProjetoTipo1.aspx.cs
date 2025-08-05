using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_DadosProjeto_ResumoProjetoTipo1 : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = 0;
    public string alturaFrames = "";
    public string grafico1 = "graficos/grafico_005.aspx", grafico2 = "graficos/grafico_006.aspx", grafico3 = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsStt");
        }

        defineTamanhoObjetos();

        grafico1 = "graficos/grafico_005.aspx?IDProjeto=" + codigoProjeto + "&Altura=" + alturaFrames;
        grafico2 = "graficos/grafico_006.aspx?IDProjeto=" + codigoProjeto + "&Altura=" + alturaFrames;
        grafico3 = "graficos/grafico_023.aspx?IDProjeto=" + codigoProjeto + "&Altura=" + alturaFrames;
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));        

        alturaFrames = ((altura - 230) / 2).ToString();
    }    
}