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
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPivotGrid.Localization;
using System.IO;
using System.Web.Hosting;
using DevExpress.Web;

public partial class _Portfolios_frameSelecaoBalanceamento_Relatorio : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    rel_SelecaoBalanceamentoPortfolio relatorio;
    int codigoPortfolio = 0;
    string montaNomeArquivo = "";

    int idUsuarioLogado = 0;
    string nomeUsuarioLogado = "";

    public string alturaTela = "";
    public string larguraTela = "";

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
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        DataSet dsUsuario = cDados.getUsuarios(" and u.[CodigoUsuario] = " + idUsuarioLogado.ToString());
        if (cDados.DataSetOk(dsUsuario) && cDados.DataTableOk(dsUsuario.Tables[0]))
        {
            nomeUsuarioLogado = dsUsuario.Tables[0].Rows[0]["NomeUsuario"].ToString();
        }

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        defineAlturaTela();

        byte[] vetorBytes = null;
        DataSet ds = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            }
        }
        
        ASPxBinaryImage image1 = new ASPxBinaryImage();
        try
        {
            image1.ContentBytes = vetorBytes;

            if (image1.ContentBytes != null)
            {
                montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        catch
        {

        }
       
        relatorio = new rel_SelecaoBalanceamentoPortfolio(codigoEntidadeUsuarioResponsavel);
        relatorio.Parameters["pcodigoPortfolio"].Value = codigoPortfolio;
        relatorio.Parameters["pAno"].Value = "-1";
        relatorio.Parameters["pCaminhoArquivo"].Value = montaNomeArquivo;
        relatorio.Parameters["pCodigoEntidade"].Value = codigoEntidadeUsuarioResponsavel;
        relatorio.Parameters["pNomeUsuarioLogado"].Value = nomeUsuarioLogado;
        relatorio.Parameters["pNomePortfolio"].Value = getNomePortfolioAtual(codigoPortfolio);

        ReportViewer1.Report = relatorio;
        ReportViewer1.WritePdfTo(Response);
    }

    private string getNomePortfolioAtual(int codPortfolio)
    {
        string nomePortfolioAtual = "";
        DataSet ds1 = cDados.getPortfolios(codigoEntidadeUsuarioResponsavel, " AND p.CodigoPortfolio = " + codPortfolio.ToString());

        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            nomePortfolioAtual = ds1.Tables[0].Rows[0]["DescricaoPortfolio"].ToString();
        }
        return nomePortfolioAtual;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        // Calcula a largura da tela
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0,resolucaoCliente.IndexOf('x')));
        
        alturaTela = (alturaPrincipal - 240).ToString() + "px";
        larguraTela = (larguraPrincipal - 90).ToString() + "px";
    }
}
