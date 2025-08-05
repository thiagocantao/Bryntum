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
using System.Drawing;

public partial class _VisaoMaster_Graficos_painelGerencial : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", alturaGrafico = "", larguraGrafico = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", grafico5 = "", grafico6 = "";

    int codigoEntidade;

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

        defineLarguraTela();

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaInformacoesPainel();
    }

    private void carregaInformacoesPainel()
    {
        string codigoArea = "-1", nomeArea = "";

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" && Request.QueryString["CA"].ToString().ToUpper().Trim() != "NULL")
            codigoArea = Request.QueryString["CA"].ToString();

        if (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "" && Request.QueryString["NA"].ToString().ToUpper().Trim() != "NULL")
            nomeArea = Request.QueryString["NA"].ToString();

        DataSet dsParametro = cDados.getInformacoesPainelGerenciamento(codigoEntidade, int.Parse(codigoArea), "");

        if (cDados.DataSetOk(dsParametro))
        {
            if (cDados.DataTableOk(dsParametro.Tables[0]))
            {
                DataRow dr = dsParametro.Tables[0].Rows[0];

                imgFisicoUHE.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoUHE"]);
                imgFinanceiroUHE.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroUHE"]);
                lblDesempenhoUHE.Text = string.Format("{0:n2}%", dr["DesempenhoUHE"]);
            }

            if (cDados.DataTableOk(dsParametro.Tables[1]))
            {
                DataRow dr = dsParametro.Tables[1].Rows[0];

                imgFisicoPimental.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoPimental"]);
                imgFinanceiroPimental.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroPimental"]);
                lblDesempenhoPimental.Text = string.Format("{0:n2}%", dr["DesempenhoPimental"]);
            }

            if (cDados.DataTableOk(dsParametro.Tables[2]))
            {
                DataRow dr = dsParametro.Tables[2].Rows[0];

                imgFisicoBeloMonte.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoBeloMonte"]);
                imgFinanceiroBeloMonte.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroBeloMonte"]);
                lblDesempenhoBeloMonte.Text = string.Format("{0:n2}%", dr["DesempenhoBeloMonte"]);
            }

            if (cDados.DataTableOk(dsParametro.Tables[3]))
            {
                DataRow dr = dsParametro.Tables[3].Rows[0];

                imgFisicoInfra.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoInfra"]);
                imgFinanceiroInfra.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroInfra"]);
                lblDesempenhoInfra.Text = string.Format("{0:n2}%", dr["DesempenhoInfra"]);
            }

            if (cDados.DataTableOk(dsParametro.Tables[4]))
            {
                DataRow dr = dsParametro.Tables[4].Rows[0];

                imgFisicoDerivacao.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoDerivacao"]);
                imgFinanceiroDerivacao.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroDerivacao"]);
                lblDesempenhoDerivacao.Text = string.Format("{0:n2}%", dr["DesempenhoDerivacao"]);
            }

            if (cDados.DataTableOk(dsParametro.Tables[5]))
            {
                DataRow dr = dsParametro.Tables[5].Rows[0];

                imgFisicoDiques.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoDiques"]);
                imgFinanceiroDiques.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroDiques"]);
                lblDesempenhoDiques.Text = string.Format("{0:n2}%", dr["DesempenhoDiques"]);
            }

            DateTime dataParam = DateTime.Now.Day <= 10 ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);

            lblInformacoes.Text = string.Format(@"Informações referentes a {0:MMMM/yyyy}", dataParam);

        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 205).ToString() + "px";
        metadeAlturaTela = ((altura - 205) / 2).ToString() + "px";

        //imgBeloMonte.Height = altura - 550;        
        imgBeloMonte.Width = largura / 6 + 10;

        //imgPimental.Height = altura - 550;
        imgPimental.Width = largura / 6 + 10;

        //imgDiques.Height = altura - 550;
        imgDiques.Width = largura / 6  + 10;

        //imgInfra.Height = altura - 550;
        imgInfra.Width = largura / 6 + 10;

        //imgTransposicao.Height = altura - 550;
        //imgTransposicao.Width = largura / 6 - 15;

        //imgDerivacao.Height = altura - 550;
        imgDerivacao.Width = largura / 6 + 10;

        
    }
}
