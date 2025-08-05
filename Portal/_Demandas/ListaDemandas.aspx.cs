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
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.Utils;
using DevExpress.Data;

public partial class _Demandas_ListaDemandas : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    int retornoCodigoWorkflow = 0, retornoCodigoEtapaInicial = 0, retornoFluxo = 0;
    public bool podeIncluir = false, podeIncluirServico = false, podeEditarVinculos = false;
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
                       
        callbackEVT.JSProperties["cp_Url"] = "";

        verificaPermissoes();

        int codigoFluxoNovaProposta, codigoWFNovaProposta;

        cDados.getCodigoWfAtualPorIniciais(codigoEntidade, out codigoFluxoNovaProposta, out codigoWFNovaProposta, "DEM_TIC");

        hfGeral.Set("TipoProjetoA", "../wfEngine.aspx?NivelNavegacao=3&Demandas=S&CF=" + codigoFluxoNovaProposta + "&CW=" + codigoWFNovaProposta + "&IF=DEM_TIC");

        cDados.getCodigoWfAtualPorIniciais(codigoEntidade, out codigoFluxoNovaProposta, out codigoWFNovaProposta, "PLAN_INV_TIC");

        hfGeral.Set("TipoProjetoB", "../wfEngine.aspx?NivelNavegacao=3&Demandas=S&CF=" + codigoFluxoNovaProposta + "&CW=" + codigoWFNovaProposta + "&IF=PLAN_INV_TIC");

        cDados.getCodigoWfAtualPorIniciais(codigoEntidade, out codigoFluxoNovaProposta, out codigoWFNovaProposta, "PRJ_TIC_EX");

        hfGeral.Set("TipoProjetoC", "../wfEngine.aspx?NivelNavegacao=3&Demandas=S&CF=" + codigoFluxoNovaProposta + "&CW=" + codigoWFNovaProposta + "&IF=PRJ_TIC_EX");

        populaGrid();
        defineAlturaTela();
        cDados.aplicaEstiloVisual(this);
        
        this.Title = cDados.getNomeSistema();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListaDemandas.js""></script>"));
        this.TH(this.TS("login", "ListaDemandas"));
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

        podeEditarVinculos = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade,
            codigoEntidade, "null", "EN", 0, "null", "EN_EdtVincDem");


        string comandoSQL = string.Format(@"
            SELECT 1 
	          FROM {0}.{1}.pbh_PlanoInvestimento pin 
             WHERE CONVERT(DateTime, CONVERT(VarChar(10), GETDATE(), 103), 103) BETWEEN pin.DataInicioInclusaoProjeto AND pin.DataFinalInclusaoProjetos"
            , cDados.getDbName(), cDados.getDbOwner());

        DataSet ds = cDados.getDataSet(comandoSQL);

        bool podeIncluir_ds = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade,
            codigoEntidade, "null", "EN", 0, "null", "EN_IncDemTic");

        bool podeIncluir_pps = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade,
            codigoEntidade, "null", "EN", 0, "null", "EN_IncPrjPlnInv");     

        bool podeIncluir_pnps = true;     

        //if(podeIncluir_pps)
        //    podeIncluir_pps = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0])) || podeIncluirPlanoInvestimentoForaPeriodo;

        podeIncluir_pps = false;

        podeIncluir = podeIncluir_ds || podeIncluir_pps || podeIncluir_pnps;

        if (!podeIncluir_ds)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #ds
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
        if (!podeIncluir_pps)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #pps
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
        if (!podeIncluir_pnps)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #pnps
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        larguraTabela = gvDados.Width.ToString();
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 380;

        larguraForm = (larguraPrincipal - 10).ToString();
        alturaForm = (alturaPrincipal - 125).ToString();

    }

    private void populaGrid()
    {
        string where = "";

        DataSet ds = cDados.getListaInformacoesDemandas(codigoEntidade, codigoUsuarioLogado, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            gvDados.TotalSummary.Clear();

            ASPxSummaryItem valorSolicitado = new ASPxSummaryItem();
            valorSolicitado.FieldName = "ValorSolicitado";
            valorSolicitado.ShowInColumn = "ValorSolicitado";
            valorSolicitado.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorSolicitado.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorSolicitado);


            ASPxSummaryItem valorAprovado = new ASPxSummaryItem();
            valorAprovado.FieldName = "ValorAprovado";
            valorAprovado.ShowInColumn = "ValorAprovado";
            valorAprovado.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorAprovado.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorAprovado);

            ASPxSummaryItem valorExecutado = new ASPxSummaryItem();
            valorExecutado.FieldName = "ValorExecutado";
            valorExecutado.ShowInColumn = "ValorExecutado";
            valorExecutado.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorExecutado.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorExecutado);

            ASPxSummaryItem valorSolicitadoAno = new ASPxSummaryItem();
            valorSolicitadoAno.FieldName = "ValorSolicitadoAno";
            valorSolicitadoAno.ShowInColumn = "ValorSolicitadoAno";
            valorSolicitadoAno.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorSolicitadoAno.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorSolicitadoAno);

            ASPxSummaryItem valorAprovadoAno = new ASPxSummaryItem();
            valorAprovadoAno.FieldName = "ValorAprovadoAno";
            valorAprovadoAno.ShowInColumn = "ValorAprovadoAno";
            valorAprovadoAno.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorAprovadoAno.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorAprovadoAno);

            ASPxSummaryItem valorExecutadoAno = new ASPxSummaryItem();
            valorExecutadoAno.FieldName = "ValorExecutadoAno";
            valorExecutadoAno.ShowInColumn = "ValorExecutadoAno";
            valorExecutadoAno.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorExecutadoAno.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorExecutadoAno);

            string comandoSQL = string.Format(@"Select CodigoProjeto, NomeProjeto FROM {0}.{1}.f_pbh_GetProjetosOriginamDemanda({2}, {3})"
                , cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, codigoUsuarioLogado);

            DataSet dsCombo = cDados.getDataSet(comandoSQL);

            ddlProjetoAbertura.DataSource = dsCombo;
            ddlProjetoAbertura.TextField = "NomeProjeto";
            ddlProjetoAbertura.ValueField = "CodigoProjeto";
            ddlProjetoAbertura.DataBind();
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
        string codigoProjeto = Eval("CodigoDemanda").ToString();
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
        string codigoProjeto = Eval("CodigoDemanda").ToString();
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
                string urlDirecionamento = "../wfEngine.aspx?NivelNavegacao=3&RO=S&Demandas=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual;
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

            string urlDirecionamento = "../wfEngine.aspx?NivelNavegacao=3&Demandas=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + parametros;
            strBotao = string.Format(@"<img title='Atualizar Fluxo' onclick='window.location.href = ""{0}"";' style='cursor:pointer' src='../imagens/botoes/interagir.PNG' />"
                , urlDirecionamento);
        }

        return strBotao;
    }

    public string getBotaoEVTFluxo()
    {
        string codigoProjeto = Eval("CodigoDemanda").ToString();
        string podeIniciarFluxo = Eval("PodeIniciarFluxoAbrirProjeto").ToString();        

        string strBotao = "";

        //if (podeIniciarFluxo == "S")
        //    strBotao = string.Format(@"<img title='Fazer abertura do projeto' onclick='abreNovoFluxoEVT({0}, ""PRJ_TIC_PS"");' style='cursor:pointer' src='../imagens/botoes/btnEngrenagem.png' />"
        //            , codigoProjeto);
        //else
        //    strBotao = string.Format(@"<img alt='' src='../imagens/botoes/btnEngrenagemDes.png' />"); 

        return strBotao;
    }

    public string getBotaoVinculo()
    {
        string editarVinculo = Eval("PodeEditarVinculos").ToString();
        string codigoProjeto = Eval("CodigoDemanda").ToString();
        string nomeDemanda = Eval("TituloDemanda").ToString();
        string strBotao = "";

        if (editarVinculo == "N" || podeEditarVinculos == false)
        {
            strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/btnVinculoDes.PNG' />";
        }
        else
        {

            strBotao = string.Format(@"<img title='Vínculos da Demanda' onclick='window.top.showModal(""./VinculosDemanda.aspx?CodigoDemanda={0}&NomeDemanda={1}"", ""Vínculos da Demanda"", screen.width - 60, 250, """", null);' style='cursor:pointer' src='../imagens/botoes/btnVinculo.PNG' />"
                , codigoProjeto
                , Server.UrlEncode(nomeDemanda));
        }

        return strBotao;
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

    public string getDescricaoObra()
    {
        string codigoProjeto = Eval("CodigoDemanda").ToString();
        string projeto = Eval("TituloDemanda").ToString();
        
        string descricao = string.Format("target='_top' href = '../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}'", codigoProjeto, projeto);       

        return string.Format(@"<a {0} style='cursor: pointer'>{1}</a>", descricao
                                                                      , projeto);
    }

    public string getBotoesInsercao()
    {
        string resultadoTD = "", botao = "";

        botao = podeIncluir ? @"<img id=""btnIncluir"" src=""../imagens/botoes/incluirReg02.png"" onclick=""pcTipoObras.Show();//abreEdicaoObras(-1)"" title=""Novo Registro"" style=""cursor: pointer""/>" : @"<img id=""btnIncluir"" src=""../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        
        resultadoTD = string.Format(@"<table style=""width:100%"">
                                        <tr>
                                            <td style='padding-right:8px' align=""left"">{0}</td>
                                        </tr>
                                      </table>", botao);

        return resultadoTD;

    }

    protected void callbackEVT_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string[] parametros = e.Parameter.Split(';');
            int codigoProjeto = int.Parse(parametros[0]);
            string iniciais = parametros[1];

            int codigoFluxo, codigoWF, codigoInstancia, codigoEtapa, codigoSequencia;

            bool retorno = cDados.incluiFluxoAberturaProjetoPorIniciais(codigoEntidade, codigoProjeto, codigoUsuarioLogado, out codigoFluxo, out codigoWF, out codigoInstancia, out codigoEtapa, out codigoSequencia, "PRJ_TIC_PS");

            string urlDirecionamento = "../wfEngine.aspx?NivelNavegacao=3&Demandas=S&CF=" + codigoFluxo + "&CW=" + codigoWF + "&CP=" + codigoProjeto + "&CI=" + codigoInstancia + "&CE=" + codigoEtapa + "&CS=" + codigoSequencia;

            callbackEVT.JSProperties["cp_Url"] = urlDirecionamento;
        }
    }
    
    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != string.Empty)
        {
            (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
            (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstDemTic");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "LstDemTic", lblTituloTela.Text, this);
    }

    #endregion
}
