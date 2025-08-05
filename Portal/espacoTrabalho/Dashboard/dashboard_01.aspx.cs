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

public partial class espacoTrabalho_VisaoCorporativa_dashboard_01 : System.Web.UI.Page
{
    dados cDados;
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "";

    public string telaEstrategia = "", telaPortfolio = "", telaOrcamento01 = "", telaOrcamento02 = "", telaProjetos01 = "", telaProjetos02 = "";

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

        criaFrames();

        DataSet dsParametro = cDados.getParametrosSistema("CodigoPortfolioDefault", "CodigoMapaDefault");

        cDados.setInfoSistema("CodigoMapa", "-1");
        cDados.setInfoSistema("CodigoPortfolio", "-1");
        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            if (dsParametro.Tables[0].Rows[0]["CodigoPortfolioDefault"] + "" != "")
                cDados.setInfoSistema("CodigoPortfolio", dsParametro.Tables[0].Rows[0]["CodigoPortfolioDefault"].ToString());

            if (dsParametro.Tables[0].Rows[0]["CodigoMapaDefault"] + "" != "")
                cDados.setInfoSistema("CodigoMapa", dsParametro.Tables[0].Rows[0]["CodigoMapaDefault"].ToString());
        }

        //if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        //{
        //    grafico1 = dsParametro.Tables[0].Rows[0]["vc_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["vc_grafico001"] + ".aspx";
        //    grafico2 = dsParametro.Tables[0].Rows[0]["vc_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["vc_grafico002"] + ".aspx";
        //    grafico3 = dsParametro.Tables[0].Rows[0]["vc_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["vc_grafico003"] + ".aspx";
        //    grafico4 = dsParametro.Tables[0].Rows[0]["vc_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["vc_grafico004"] + ".aspx";
        //    grafico5 = dsParametro.Tables[0].Rows[0]["vc_grafico005"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["vc_grafico005"] + ".aspx";
        //    grafico6 = dsParametro.Tables[0].Rows[0]["vc_grafico006"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["vc_grafico006"] + ".aspx";
        //}        
    }

    private void criaFrames()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 10) / 4).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 154).ToString() + "px";
        metadeAlturaTela = ((altura - 154) / 2).ToString() + "px";

        string larguraGraficoProjetos = ((largura - 10) / 4 - 15).ToString();
        string alturaGraficoProjetos = ((altura - 154) / 2 - 45).ToString();

        telaPortfolio = "../../_Portfolios/VisaoCorporativa/vc_003.aspx?Largura=" + ((largura - 10) / 4) + "&Altura=" + (altura - 154);
        telaProjetos01 = "../../_Projetos/VisaoCorporativa/vc_001.aspx?Largura=" + ((largura - 10) / 4) + "&Altura=" + ((altura - 154) / 2);
        telaProjetos02 = "../../_Projetos/VisaoCorporativa/vc_002.aspx?Largura=" + larguraGraficoProjetos + "&Altura=" + alturaGraficoProjetos;
        telaOrcamento01 = "../../_Orcamentos/graficos/vc_002.aspx?Largura=" + ((largura - 10) / 4) + "&Altura=" + ((altura - 154) / 2);
        telaOrcamento02 = "../../_Orcamentos/graficos/vc_001.aspx?Largura=" + ((largura - 10) / 4) + "&Altura=" + ((altura - 154) / 2);
        telaEstrategia = "../../_Estrategias/telaDashboard.aspx?Largura=" + ((largura - 10) / 4) + "&Altura=" + (altura - 154);

    }
}
