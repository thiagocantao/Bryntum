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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPrinting;
using System.Web.Hosting;
using System.Diagnostics;
using DevExpress.Web;
using System.IO;

public partial class _Demandas_ListaDemandas : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    int retornoCodigoWorkflow = 0, retornoCodigoEtapaInicial = 0, retornoFluxo = 0;
    public bool podeIncluir = false;
    string alturaForm, larguraForm;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                      
        verificaPermissoes();

        int codigoFluxoNovaProposta, codigoWFNovaProposta;

        cDados.getCodigoWfAtualPorIniciais(codigoEntidade, out codigoFluxoNovaProposta, out codigoWFNovaProposta, "PLAN_INV_TIC");

        hfGeral.Set("TipoProjeto", "../wfEngine.aspx?NivelNavegacao=3&PlanosInvestimento=S&CF=" + codigoFluxoNovaProposta + "&CW=" + codigoWFNovaProposta + "&IF=PLAN_INV_TIC");
               
        populaGrid();
        defineAlturaTela();
        cDados.aplicaEstiloVisual(this);

        this.Title = cDados.getNomeSistema();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListaPlanosInvestimento.js""></script>"));
        this.TH(this.TS("ListaPlanosInvestimento"));

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void verificaPermissoes()
    {
        bool podeIncluirPlanoInvestimentoForaPeriodo = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade,
            codigoEntidade, "null", "EN", 0, "null", "EN_IncPlnInvPer");              

        string comandoSQL = string.Format(@"
            SELECT 1 
	          FROM {0}.{1}.pbh_PlanoInvestimento pin 
             WHERE CONVERT(DateTime, CONVERT(VarChar(10), GETDATE(), 103), 103) BETWEEN pin.DataInicioInclusaoProjeto AND pin.DataFinalInclusaoProjetos"
            , cDados.getDbName(), cDados.getDbOwner());

        DataSet ds = cDados.getDataSet(comandoSQL);        

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade,
            codigoEntidade, "null", "EN", 0, "null", "EN_IncPrjPlnInv");


        if (podeIncluir)
            podeIncluir = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0])) || podeIncluirPlanoInvestimentoForaPeriodo;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        larguraTabela = gvDados.Width.ToString();
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 340;

        larguraForm = (larguraPrincipal - 10).ToString();
        alturaForm = (alturaPrincipal - 125).ToString();

    }

    private void populaGrid()
    {
        string where = "";

        DataSet ds = cDados.getListaInformacoesPlanosInvestimento(codigoEntidade, codigoUsuarioLogado, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();            
        }

    }
    
    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName != "COLLAPSEROW" && e.CallbackName != "EXPANDROW")
            gvDados.ExpandAll();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {
        gvDados.ExpandAll();
    }

    public string getBotaoGraficoFluxo()
    {        
        string codigoWorkflow = Eval("CodigoWorkflow").ToString();
        string codigoInstanciaWf = Eval("CodigoInstanciaWf").ToString();
        string codigoFluxo = Eval("CodigoFluxo").ToString();
        string codigoProjeto = Eval("CodigoProjeto").ToString();
        string strBotao = "";

        if (codigoFluxo == "" || codigoWorkflow == "" || codigoInstanciaWf == "")
        {
            strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/fluxosDes.PNG' />";
        }
        else
        {
            string urlDirecionamento = "../_Portfolios/GraficoProcesso.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&NivelNavegacao=3";
            strBotao = string.Format(@"<img title='Visualizar Fluxo Graficamente' onclick='window.location.href = ""{0}"";' style='cursor:pointer' src='../imagens/botoes/fluxos.PNG' />"
                , urlDirecionamento);
        }

        return strBotao;
    }

    public string getBotaoInteragirFluxo()
    {
        string etapa = Eval("EtapaAtualProcesso").ToString();
        string codigoWorkflow = Eval("CodigoWorkflow").ToString();
        string codigoInstanciaWf = Eval("CodigoInstanciaWf").ToString();
        string codigoFluxo = Eval("CodigoFluxo").ToString();
        string codigoProjeto = Eval("CodigoProjeto").ToString();
        string codigoEtapaInicial = Eval("CodigoEtapaInicial").ToString();
        string codigoEtapaAtual = Eval("CodigoEtapaAtual").ToString();
        string ocorrenciaAtual = Eval("OcorrenciaAtual").ToString();
        string codigoStatus = Eval("CodigoStatus").ToString();
                
        cDados.getCodigoWfAtualPorIniciaisFluxo(codigoEntidade, out retornoFluxo, out retornoCodigoWorkflow, out retornoCodigoEtapaInicial, "");

        string strBotao = "";

        int acessoFluxo = 0;

        if (codigoInstanciaWf == "")
            codigoInstanciaWf = "-1";

        if (etapa != "" && codigoFluxo != "")
        {
            acessoFluxo = cDados.obtemNivelAcessoEtapaWf(int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWf), int.Parse(ocorrenciaAtual), int.Parse(codigoEtapaAtual), codigoUsuarioLogado.ToString());
        }
        else if (etapa == "" && codigoFluxo == "" && (int.Parse(codigoStatus) == 1 || int.Parse(codigoStatus) == 14))
        {
            codigoFluxo = retornoFluxo.ToString();
            codigoWorkflow = retornoCodigoWorkflow.ToString();
            codigoEtapaInicial = retornoCodigoEtapaInicial.ToString();

            acessoFluxo = cDados.obtemNivelAcessoEtapaWfNaoInstanciada(retornoCodigoWorkflow, codigoProjeto, retornoCodigoEtapaInicial, codigoUsuarioLogado.ToString());
        }
        else
        {
            acessoFluxo = 0;
        }

        if (acessoFluxo == 0 || codigoStatus == "3" || codigoStatus == "16")
        {
            strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/interagirDes.png' />";
        }
        else if (acessoFluxo == 1)
        {
            if (codigoFluxo != "" && codigoProjeto != "" && codigoWorkflow != "" && codigoInstanciaWf != "" && codigoEtapaAtual != "" && ocorrenciaAtual != "")
            {
                string urlDirecionamento = "../wfEngine.aspx?NivelNavegacao=3&RO=S&PlanosInvestimento=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual;
                strBotao = string.Format(@"<img title='Visualizar Etapa Atual' onclick='window.location.href = ""{0}""';' style='cursor:pointer' src='../imagens/botoes/pFormulario.png' />"
                    , urlDirecionamento);
            }
            else
            {
                strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/interagirDes.png' />";
            }
        }
        else
        {
            string parametros = "";

            if (etapa != "" && codigoInstanciaWf != "" && codigoEtapaAtual != "" && ocorrenciaAtual != "")
                parametros = "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual;

            string urlDirecionamento = "../wfEngine.aspx?NivelNavegacao=3&PlanosInvestimento=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + parametros;
            strBotao = string.Format(@"<img title='Atualizar Fluxo' onclick='window.location.href = ""{0}"";' style='cursor:pointer' src='../imagens/botoes/interagir.PNG' />"
                , urlDirecionamento);
        }

        return strBotao;
    }
    
    public string getDescricaoObra()
    {
        string codigoProjeto = Eval("CodigoProjeto").ToString();
        string projeto = Eval("TituloProjeto").ToString();
        
        string descricao = string.Format("target='_top' href = '../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}'", codigoProjeto, projeto);       

        return string.Format(@"<a {0} style='cursor: pointer'>{1}</a>", descricao
                                                                      , projeto);
    }
               
    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {        
        if (e.Parameters != string.Empty)
        {
            (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
            (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
        }
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
        if (e.RowType == GridViewRowType.Footer && e.Text != "")
        {
            e.TextValueFormatString = "{0:c2}";
            try
            {
                e.TextValue = double.Parse(e.Text.Replace(".", ""));
            }
            catch { }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstPlnInvest2");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "insereNovaDemanda('TipoProjeto');", true, true, true, "LstPlnInvest2", lblTituloTela.Text, this);
    }

    #endregion
}
