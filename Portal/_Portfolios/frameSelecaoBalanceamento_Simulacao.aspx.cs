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
using System.Web.Hosting;
using DevExpress.Web;
using Microsoft.VisualBasic;
using System.Globalization;

public partial class _Portfolios_frameSelecaoBalanceamento_Simulacao : System.Web.UI.Page
{
    dados cDados;
    public string larguraTela = "", alturaTela = "", metadeAlturaTela = "", larguraGrafico = "";
    public string urlGanttBalanceamento = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();        
    }

    /// <summary>
    /// Page_Load
    /// </summary>    
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


        if (!IsPostBack)
            cDados.aplicaEstiloVisual(this);

        imgVisao.Style.Add("cursor", "pointer");
        urlGanttBalanceamento = Session["baseUrl"].ToString() + "/_Public/Gantt/paginas/balanceamento/Default.aspx";
        imgVisao.ClientSideEvents.Click = "function(s, e) { window.location.href = '" + urlGanttBalanceamento + "' }";
        
        DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            lblDespesaLabel.Text = string.Format("{0}:", dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString());
        }

        if (!IsPostBack)
        {
            DefinirComboCenario();                        
            numerosCenario();
        }

        defineLarguraTela();
    }

    private void DefinirComboCenario()
    {
        string numCenario = Request.QueryString["NumCenario"];
        if (!string.IsNullOrEmpty(numCenario))
        {
            ddlCenario.Value = numCenario;
        }
        else
        {
            if (cDados.getInfoSistema("Cenario") != null)
                ddlCenario.Value = cDados.getInfoSistema("Cenario").ToString();
        }

        cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 450) / 3 - 5).ToString() + "px";
        larguraGrafico = (largura - 255).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 40).ToString() + "px";
        metadeAlturaTela = ((altura -200) / 2).ToString() + "px";

        pNumeros.ContentHeight = altura - 265;        
    }

    private void numerosCenario()
    {
        int codigoPortfolio = -1;

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        DataSet ds = cDados.getNumerosCenario(codigoPortfolio, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), -1, " AND IndicaCenario" + ddlCenario.Value.ToString() + " = 'S'");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            lblProjetosExecucao.Text = string.Format("{0:n0}", float.Parse(dt.Rows[0]["ProjetosExecucao"].ToString()));
            lblNovosProjetos.Text = string.Format("{0:n0}", float.Parse(dt.Rows[0]["ProjetosNovos"].ToString()));
            lblDespesa.Text = string.Format("{0:n2}", float.Parse(dt.Rows[0]["Custo"].ToString()));
            lblReceita.Text = string.Format("{0:n2}", float.Parse(dt.Rows[0]["Receita"].ToString()));
            lblTrabalho.Text = string.Format("{0:n2}", float.Parse(dt.Rows[0]["Trabalho"].ToString()));
            lblRiscos.Text = string.Format("{0:n2}", float.Parse(dt.Rows[0]["Riscos"].ToString()));
            lblCriterios.Text = string.Format("{0:n2}", float.Parse(dt.Rows[0]["Criterios"].ToString()));
        }
        else
        {
            lblProjetosExecucao.Text = string.Format("-");
            lblNovosProjetos.Text = string.Format("-");
            lblDespesa.Text = string.Format("-");
            lblReceita.Text = string.Format("-");
            lblTrabalho.Text = string.Format("-");
            lblRiscos.Text = string.Format("-");
            lblCriterios.Text = string.Format("-");
        }

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet dsFluxoCaixa = cDados.getFluxoCaixaCenario(codigoPortfolio, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("Cenario").ToString().Substring(cDados.getInfoSistema("Cenario").ToString().Length - 1)), "");

        double[] valoresSaldo = new double[dsFluxoCaixa.Tables[0].Rows.Count];
        byte qtdeInversoes = 0;
        for (int i = 0; i < dsFluxoCaixa.Tables[0].Rows.Count; i++)
        {
            valoresSaldo[i] = double.Parse(dsFluxoCaixa.Tables[0].Rows[i]["Saldo"].ToString());
            if (i >= 1 &&
                Math.Sign(valoresSaldo[i]) != Math.Sign(valoresSaldo[i - 1]) &&
                (valoresSaldo[i] * valoresSaldo[i - 1] != 0))
                qtdeInversoes++;
        }

        DataSet dsParametros = cDados.getParametrosSistema("TaxaReferenciaAnual");

        double txRefAnual = 0;

        if (cDados.DataSetOk(dsParametros) && (cDados.DataTableOk(dsParametros.Tables[0])))
        {
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            string separadorDecimal = nfi.CurrencyDecimalSeparator;
            if (separadorDecimal == "")
                separadorDecimal = ".";
            string tempTaxa = dsParametros.Tables[0].Rows[0]["TaxaReferenciaAnual"].ToString().Replace(".", separadorDecimal);

            if (tempTaxa == "")
                tempTaxa = "0";
                        
            txRefAnual = double.Parse(tempTaxa);
        }

        lblTIR.Text = "-";
        if (qtdeInversoes == 1)
        {

            try
            {
                double valorIRR = Financial.IRR(ref valoresSaldo, 0.1);
                lblTIR.Text = string.Format("{0:p2}", valorIRR);

            }
            catch
            {
                lblTIR.Text = Resources.traducao.frameSelecaoBalanceamento_Simulacao_n_o_possui;
            }

        }
        try
        {
            double valorVPL = Financial.NPV(txRefAnual, ref valoresSaldo);
            lblVPL.Text = string.Format("{0:n2}", valorVPL);
        }
        catch
        {
            lblVPL.Text = Resources.traducao.frameSelecaoBalanceamento_Simulacao_n_o_possui;
        }        
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());
        numerosCenario();
    }

}
