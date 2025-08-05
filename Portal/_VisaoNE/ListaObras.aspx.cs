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
using System.Linq;
using System.Text;
using DevExpress.Data;
using DevExpress.Utils;
using System.Data.SqlClient;

public partial class _VisaoNE_ListaObras : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    public dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    private int codigoMunicipio = -1;
    int retornoCodigoWorkflow = 0, retornoCodigoEtapaInicial = 0, retornoFluxo = 0;
    public bool podeIncluir = false, podeIncluirServico = false;
    string alturaForm, larguraForm;
    DataSet dsModelo;

    #region Campos para funcionalidade personalização da lista

    DsListaProcessos dsLP;
    int codigoLista;

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
        set { _ConnectionString = value; }
    }

    #endregion

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

       dsModelo = cDados.getModeloFormulario(" AND IniciaisFormularioControladoSistema = 'OS_INT' AND CodigoEntidade = " + codigoEntidade);
        
        verificaPermissoes();

        #region Instruções para funcionalidade personalização da lista
        codigoLista = getCodigoListaProjetosMapa();
        dsLP = new DsListaProcessos();

        //if (!IsPostBack)
        InitData(); // preenche a dsLP com as listas de campos 
        #endregion

        populaGrid();

        //string comandoSQLColunas = cDados.constroiInsertLayoutColunas(gvDados, "LstObras", "ListaObras");

        defineAlturaTela();
        cDados.aplicaEstiloVisual(this);
        
        if (!IsPostBack)
        {
            filtraCampoGrid();
        }
        this.Title = cDados.getNomeSistema();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListaObras.js""></script>"));
        this.TH(this.TS("ListaObras"));
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void filtraCampoGrid()
    {
        if (Request.QueryString["CodigoMunicipio"] != null && Request.QueryString["CodigoMunicipio"].ToString() != "")
        {
            codigoMunicipio = int.Parse(Request.QueryString["CodigoMunicipio"].ToString());
        }
        
        string statusObra = "";

        if (Request.QueryString["Situacao"] != null && Request.QueryString["Situacao"].ToString() != "")
        {
            statusObra = Request.QueryString["Situacao"].ToString();
        }

        if (codigoMunicipio != -1)
        {
            string nomeMunicipio = "";

            DataSet dsMunicipio = cDados.getMunicipios(" AND CodigoMunicipio = " + codigoMunicipio);

            if (cDados.DataSetOk(dsMunicipio) && cDados.DataTableOk(dsMunicipio.Tables[0]))
            {
                nomeMunicipio = dsMunicipio.Tables[0].Rows[0]["NomeMunicipio"].ToString();

                if (gvDados.FilterExpression != "")
                     gvDados.FilterExpression += " AND [Municipio] = '" + nomeMunicipio + "'";
                else
                     gvDados.FilterExpression = " [Municipio] = '" + nomeMunicipio + "'";
            }
        }

        if (statusObra != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += " AND [Status] = '" + statusObra + "'";   
            else         
                gvDados.FilterExpression = " [Status] = '" + statusObra + "'";   
        }

        if (Request.QueryString["IndicaVigentesAno"] != null && Request.QueryString["IndicaVigentesAno"].ToString() == "S")
        {
            int anoParam = cDados.getInfoSistema("AnoContrato") == null || cDados.getInfoSistema("AnoContrato").ToString() == "-1" ? DateTime.Now.Year : int.Parse(cDados.getInfoSistema("AnoContrato").ToString());

            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [AnoTermino] = {0}", anoParam);
            else
                gvDados.FilterExpression = string.Format("[AnoTermino] = {0}", anoParam);
        }

        if (Request.QueryString["TipoObra"] != null && Request.QueryString["TipoObra"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [TipoObra] = '{0}'", Request.QueryString["TipoObra"].ToString());
            else
                gvDados.FilterExpression = string.Format("[TipoObra] = '{0}'", Request.QueryString["TipoObra"].ToString());
        }

        if (Request.QueryString["ProcessoComigo"] != null && Request.QueryString["ProcessoComigo"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [ProcessoComigo] = '{0}'", Request.QueryString["ProcessoComigo"].ToString() == "S" ? "Sim" : "Não");
            else
                gvDados.FilterExpression = string.Format("[ProcessoComigo] = '{0}'", Request.QueryString["ProcessoComigo"].ToString() == "S" ? "Sim" : "Não");
        }

        if (Request.QueryString["ProcessoMinhaGerencia"] != null && Request.QueryString["ProcessoMinhaGerencia"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [ProcessoMinhaGerencia] = '{0}'", Request.QueryString["ProcessoMinhaGerencia"].ToString() == "S" ? "Sim" : "Não");
            else
                gvDados.FilterExpression = string.Format("[ProcessoMinhaGerencia] = '{0}'", Request.QueryString["ProcessoMinhaGerencia"].ToString() == "S" ? "Sim" : "Não");
        }

        if (Request.QueryString["IndicaObraServico"] != null && Request.QueryString["IndicaObraServico"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [IndicaObraServico] = '{0}'", Request.QueryString["IndicaObraServico"].ToString());
            else
                gvDados.FilterExpression = string.Format("[IndicaObraServico] = '{0}'", Request.QueryString["IndicaObraServico"].ToString());
        }

        if (Request.QueryString["FiltroData"] != null && Request.QueryString["FiltroData"].ToString() != "")
        {
            string campo = "", tipoData = "";
            int diasParam = 0;

            DataSet ds = cDados.getParametrosSistema("diasAtualizacaoCronograma", "diasAtualizacaoFotos", "diasComentariosFiscalizacao");

            tipoData = Request.QueryString["FiltroData"].ToString();

            if (tipoData == "CR")
            {
                diasParam = 10;

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasAtualizacaoCronograma"].ToString().Trim() != "")
                    diasParam = int.Parse(ds.Tables[0].Rows[0]["diasAtualizacaoCronograma"].ToString().Trim());

                campo = "UltimaAtualizacaoCronograma";
            }
            else if (tipoData == "FT")
            {
                diasParam = 30;

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasAtualizacaoFotos"].ToString().Trim() != "")
                    diasParam = int.Parse(ds.Tables[0].Rows[0]["diasAtualizacaoFotos"].ToString().Trim());

                campo = "UltimaAtualizacaoFoto";
            }
            else if (tipoData == "CM")
            {
                diasParam = 30;

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasComentariosFiscalizacao"].ToString().Trim() != "")
                    diasParam = int.Parse(ds.Tables[0].Rows[0]["diasComentariosFiscalizacao"].ToString().Trim());

                campo = "UltimaAnaliseFiscalizacao";
            }

            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [{0}] <= #{1:yyyy-MM-dd}# ", campo, DateTime.Now.AddDays(diasParam * -1));
            else
                gvDados.FilterExpression = string.Format(" [{0}] <= #{1:yyyy-MM-dd}# ", campo, DateTime.Now.AddDays(diasParam * -1));
        }

        if (Request.QueryString["TipoContratacao"] != null && Request.QueryString["TipoContratacao"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [TipoContratacao] = '{0}'", Request.QueryString["TipoContratacao"].ToString());
            else
                gvDados.FilterExpression = string.Format("[TipoContratacao] = '{0}'", Request.QueryString["TipoContratacao"].ToString());
        }

        if (Request.QueryString["DesempenhoFisico"] != null && Request.QueryString["DesempenhoFisico"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [DesempenhoFisico] = '{0}'", Request.QueryString["DesempenhoFisico"].ToString());
            else
                gvDados.FilterExpression = string.Format("[DesempenhoFisico] = '{0}'", Request.QueryString["DesempenhoFisico"].ToString());
        }

        if (Request.QueryString["SituacaoAtualContrato"] != null && Request.QueryString["SituacaoAtualContrato"].ToString() != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [SituacaoAtualContrato] = '{0}'", Request.QueryString["SituacaoAtualContrato"].ToString());
            else
                gvDados.FilterExpression = string.Format("[SituacaoAtualContrato] = '{0}'", Request.QueryString["SituacaoAtualContrato"].ToString());
        }
    }

    private void verificaPermissoes()
    {
        bool podeIncluir_soci = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_IncObr");
        bool podeIncluir_plan = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_IncOPS");
        bool podeIncluir_pdrs = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_IncOPDRS");
        bool podeIncluir_ind = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_IncOIND");

        podeIncluir = podeIncluir_soci || podeIncluir_plan || podeIncluir_pdrs || podeIncluir_ind;
        podeIncluirServico = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_IncOSERV");

        if (!podeIncluir_soci)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #soci
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
        if (!podeIncluir_plan)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #plan
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
        if (!podeIncluir_pdrs)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #pdrs
                                                            {
                                                                display: none;
                                                            }
                                                    </style>"));
        }
        if (!podeIncluir_ind)
        {
            Header.Controls.Add(cDados.getLiteral(@"<style type=""text/css""> 
                                                            #indi
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
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 350;

        larguraForm = (larguraPrincipal - 10).ToString();
        alturaForm = (alturaPrincipal - 125).ToString();

    }

    private void populaGrid()
    {
       
        DataSet ds = cDados.getListaInformacoesObras(codigoEntidade, codigoUsuarioLogado, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            gvDados.TotalSummary.Clear();

            ASPxSummaryItem valorContrato = new ASPxSummaryItem();
            valorContrato.FieldName = "ValorContrato";
            valorContrato.ShowInColumn = "ValorContrato";
            valorContrato.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorContrato.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorContrato);


            ASPxSummaryItem valorPago = new ASPxSummaryItem();
            valorPago.FieldName = "ValorPago";
            valorPago.ShowInColumn = "ValorPago";
            valorPago.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorPago.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorPago);

            ASPxSummaryItem previstoPgto = new ASPxSummaryItem();
            previstoPgto.FieldName = "PrevistoPagamento";
            previstoPgto.ShowInColumn = "PrevistoPagamento";
            previstoPgto.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            previstoPgto.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(previstoPgto);

            ASPxSummaryItem valorSaldo = new ASPxSummaryItem();
            valorSaldo.FieldName = "Saldo";
            valorSaldo.ShowInColumn = "Saldo";
            valorSaldo.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorSaldo.DisplayFormat = "{0:n2}";
            gvDados.TotalSummary.Add(valorSaldo);

            ASPxSummaryItem qtdObras = new ASPxSummaryItem();
            qtdObras.FieldName = "QuantidadeObras";
            qtdObras.ShowInColumn = "QuantidadeObras";
            qtdObras.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            qtdObras.DisplayFormat = "{0:n0}";
            gvDados.TotalSummary.Add(qtdObras);
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

    public string getBotaoGraficoFluxo(string etapa, string codigoWorkflow, string codigoInstanciaWf, string codigoFluxo, string codigoProjeto)
    {
        string strBotao = "";

        if (codigoFluxo == "" || codigoWorkflow == "" || codigoInstanciaWf == "")
        {
            strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/fluxosDes.PNG' />";
        }
        else
        {
            System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

            listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
            listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

            cDados = CdadosUtil.GetCdados(listaParametrosDados);
            int altura = 0;
            int largura = 0;
            cDados.getLarguraAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString(), out largura, out altura);

            string urlDirecionamento = "../_Portfolios/GraficoProcesso.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&NivelNavegacao=2&Largura=" + largura;
            strBotao = string.Format(@"<img alt='Visualizar Fluxo Graficamente' onclick='window.location.href = ""{0}"";' style='cursor:pointer' src='../imagens/botoes/fluxos.PNG' />"
                , urlDirecionamento);
        }

        return strBotao;
    }

    public string getBotaoInteragirFluxo(string etapa, string codigoWorkflow, string codigoInstanciaWf, string codigoFluxo, string codigoProjeto, string codigoEtapaInicial, string codigoEtapaAtual, string ocorrenciaAtual, string codigoStatus, string tipoProjeto)
    {
        string nomeParametroFluso = string.Empty;
        cDados = CdadosUtil.GetCdados(null);
        nomeParametroFluso = "codigoFluxoNovoProjetoTipo" + tipoProjeto;

        cDados.getCodigoWfAtual(codigoEntidade, out retornoFluxo, out retornoCodigoWorkflow, out retornoCodigoEtapaInicial, nomeParametroFluso);
        string strBotao = "";

        int acessoFluxo = 0;

        if (codigoInstanciaWf == "")
            codigoInstanciaWf = "-1";

        if (etapa != "" && codigoFluxo != "")
        {
            acessoFluxo = Eval("AcessoFluxo") + "" == "" ? 0 : int.Parse(Eval("AcessoFluxo").ToString());
        }
        else if (etapa == "" && codigoFluxo == "" && int.Parse(codigoStatus) == 1)
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

        if (acessoFluxo == 0 || codigoStatus == "3")
        {
            strBotao = "<img alt='' style='cursor:default' src='../imagens/botoes/interagirDes.png' />";
        }
        else if (acessoFluxo == 1)
        {
            if (codigoFluxo != "" && codigoProjeto != "" && codigoWorkflow != "" && codigoInstanciaWf != "" && codigoEtapaAtual != "" && ocorrenciaAtual != "")
            {
                string urlDirecionamento = "../wfEngine.aspx?RO=S&Obras=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual + "&NivelNavegacao=2";
                strBotao = string.Format(@"<img alt='Visualizar informações' onclick='window.location.href = ""{0}""';' style='cursor:pointer' src='../imagens/botoes/pFormulario.png' />"
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

            string urlDirecionamento = "../wfEngine.aspx?Obras=S&CF=" + codigoFluxo + "&CP=" + codigoProjeto + "&CW=" + codigoWorkflow + parametros + "&NivelNavegacao=2";
            strBotao = string.Format(@"<img alt='Atualizar informações' onclick='window.location.href = ""{0}"";' style='cursor:pointer' src='../imagens/botoes/interagir.PNG' />"
                , urlDirecionamento);
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
        const string INICIAIS_CONCLUIDO = "CONCLUIDO";
        const string INICIAIS_EM_EXECUCAO = "EM_EXECUCAO";
        const string INICIAIS_EM_CONTRATACAO = "EM_CONTRATACAO";
        const string INICIAS_EMISSAO_OS = "EMISSAO_OS";

        string descricao = string.Empty;
        string status = Eval("IniciaisStatus").ToString();
        string codigoFluxo = Eval("CodigoFluxo").ToString();
        string codigoProjeto = Eval("CodigoProjeto").ToString();
        string projeto = Eval("Projeto").ToString();
        string tipoProjeto = string.Empty;
        switch (Eval("TipoProjeto").ToString())
        {
            case "1":
                tipoProjeto = "soci";
                break;
            case "8":
                tipoProjeto = "plan";
                break;
            case "9":
                tipoProjeto = "pdrs";
                break;
            case "11":
                tipoProjeto = "indi";
                break;
            case "12":
                tipoProjeto = "serv";
                break;
        }

        if (status.Equals(INICIAIS_EM_CONTRATACAO) || status.Equals(INICIAS_EMISSAO_OS))
            descricao = string.Format("target='_self' href = '../Administracao/CadastroContratoExterno.aspx?CP={0}&Modo=Consulta'", codigoProjeto, projeto);
        else if (status.Equals(INICIAIS_EM_EXECUCAO) || status.Equals(INICIAIS_CONCLUIDO))
            descricao = string.Format("target='_top' href = '../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}'", codigoProjeto, projeto);
        else if (string.IsNullOrEmpty(codigoFluxo))
            descricao = string.Format("href='#' onclick='abreEdicaoObras({0}, \"{1}\")'", codigoProjeto, tipoProjeto);
        else
        {            
            if (cDados.DataSetOk(dsModelo) && cDados.DataTableOk(dsModelo.Tables[0]))
                descricao = string.Format(@"target='_top' href = '../wfRenderizaExterno.aspx?AT={0}&WSCR={1}&CMF={2}&CPWF={3}&RO=S'"
                    , alturaForm
                    , larguraForm
                    , dsModelo.Tables[0].Rows[0]["CodigoModeloFormulario"].ToString()
                    , codigoProjeto);
        }

        #region Comentário
        //if (codigoFluxo == "" && status != "3" && status != "6")
        //{
        //    descricao = string.Format("href='#' onclick='abreEdicaoObras({0})'", codigoProjeto);
        //}
        //else
        //{
        //    DataSet ds = cDados.getModeloFormulario(" AND IniciaisFormularioControladoSistema = 'OS_INT' AND CodigoEntidade = " + codigoEntidade);

        //    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && (status != "3" && status != "6"))
        //        descricao = string.Format(@"target='_self' href = '../wfRenderizaFormulario.aspx?AT={0}&WSCR={1}&CMF={2}&CPWF={3}&RO=S'"
        //            , alturaForm
        //            , larguraForm
        //            , ds.Tables[0].Rows[0]["CodigoModeloFormulario"].ToString()
        //            , codigoProjeto);
        //    else
        //    {
        //        descricao = string.Format("target='_top' href = '../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}'", codigoProjeto, projeto);
        //    }
        //} 
        #endregion

        return string.Format(@"<a {0} style='cursor: pointer'>{1}</a>", descricao
                                                                      , projeto);
    }

    public string getBotoesInsercao()
    {
        string resultadoTD = "", botaoObras = "", botaoServicos = "";

        botaoObras = podeIncluir ? @"<img id=""btnIncluir"" src=""../imagens/botoes/incluirReg02.png"" onclick=""pcTipoObras.Show();//abreEdicaoObras(-1)"" title=""Incluir Nova Obra"" style=""cursor: pointer""/>" : @"<img id=""btnIncluir"" src=""../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        botaoServicos = podeIncluirServico ? @"<img id=""btnIncluirServicos"" src=""../imagens/botoes/novoProjeto.png"" onclick=""abreEdicaoServicos(-1)"" title=""Incluir Novo Serviço"" style=""cursor: pointer""/>" : @"<img id=""btnIncluirServico"" src=""../imagens/botoes/novoProjetoDes.png"" title="""" style=""cursor: default;""/>";

        resultadoTD = string.Format(@"<table style=""width:100%"">
                                        <tr>
                                            <td style='padding-right:8px' align=""left"">{0}</td>
                                            <td align=""left"">{1}</td>
                                        </tr>
                                      </table>", botaoObras
                                               , botaoServicos);

        return resultadoTD;

    }

    #region GRAVAÇÃO e RECUPERAÇÃO DE LAYOUT

    private int getCodigoListaProjetosMapa()
    {
        string comandoSQL;
        int codLista = -1;

        comandoSQL = string.Format("SELECT l.[CodigoLista] FROM {0}.{1}.[Lista] AS [l] WHERE l.[CodigoEntidade] = {2} AND l.[IniciaisListaControladaSistema] = 'LstObras' ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            codLista = int.Parse(ds.Tables[0].Rows[0]["CodigoLista"].ToString());

        return codLista;
    }

    private void InitData()
    {
        string comandoPreencheLista;
        string comandoPreencheListaCampo;
        string comandoPreencheListaFluxo;

        #region Comandos SQL

        comandoPreencheLista = string.Format(@"
 SELECT l.CodigoLista, 
		NomeLista, 
		GrupoMenu, 
		ItemMenu, 
		GrupoPermissao, 
		ItemPermissao, 
		IniciaisPermissao, 
		TituloLista, 
		ComandoSelect, 
		IndicaPaginacao, 
		ISNULL(lu.QuantidadeItensPaginacao, l.QuantidadeItensPaginacao) AS QuantidadeItensPaginacao, 
        lu.FiltroAplicado,
		IndicaOpcaoDisponivel, 
		TipoLista, 
		URL, 
		CodigoEntidade,
        CodigoModuloMenu,
        IndicaListaZebrada
   FROM Lista l left join ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0}) 
  WHERE (l.CodigoLista = @CodigoLista)", codigoUsuarioLogado);

        comandoPreencheListaCampo = string.Format(@"
 SELECT lc.CodigoCampo,
        lc.CodigoLista, 
        lc.NomeCampo, 
        lc.TituloCampo, 
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo) AS OrdemCampo, 
        ISNULL(lcu.OrdemAgrupamentoCampo, lc.OrdemAgrupamentoCampo) AS OrdemAgrupamentoCampo,
        lc.TipoCampo, 
        lc.Formato, 
        lc.IndicaAreaFiltro, 
        lc.TipoFiltro, 
        lc.IndicaAgrupamento, 
        lc.TipoTotalizador, 
        lc.IndicaAreaDado, 
        lc.IndicaAreaColuna, 
        lc.IndicaAreaLinha, 
        ISNULL(lcu.AreaDefault, lc.AreaDefault) AS AreaDefault, 
        ISNULL(lcu.IndicaCampoVisivel, lc.IndicaCampoVisivel) AS IndicaCampoVisivel, 
        lc.IndicaCampoControle,
        lc.IniciaisCampoControlado,
        lc.IndicaLink,
        (CASE WHEN lcu.CodigoCampo IS NOT NULL THEN 'S' ELSE 'N' END) AS IndicaCampoCustumizado,
        lc.AlinhamentoCampo,
        lc.IndicaCampoHierarquia,
        ISNULL(lcu.LarguraColuna, lc.LarguraColuna) AS LarguraColuna
   FROM ListaCampo AS lc LEFT JOIN
		ListaUsuario lu ON lu.CodigoLista = lc.CodigoLista AND 
						   lu.CodigoUsuario = {0} LEFT JOIN
        ListaCampoUsuario lcu ON lc.CodigoCampo = lcu.CodigoCampo AND 
                                 lcu.CodigoListaUsuario = lu.CodigoListaUsuario
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)", codigoUsuarioLogado);

        comandoPreencheListaFluxo = @"
 SELECT CodigoLista, 
		CodigoFluxo, 
		TituloMenu
   FROM ListaFluxo
  WHERE (CodigoLista = @CodigoLista)";

        #endregion

        FillData(dsLP.Lista, comandoPreencheLista);
        FillData(dsLP.ListaCampo, comandoPreencheListaCampo);
        FillData(dsLP.ListaFluxo, comandoPreencheListaFluxo);

        DsListaProcessos.ListaRow item = dsLP.Lista.Single();

        SetGridSettings(item);
    }

    private void FillData(DataTable dt, string comandoSql)
    {
        SqlDataAdapter da = new SqlDataAdapter(comandoSql, ConnectionString);
        SqlParameter p = da.SelectCommand.Parameters.Add("@CodigoLista", SqlDbType.Int);
        p.Value = codigoLista;
        da.Fill(dt);
    }

    private void SetGridSettings(DsListaProcessos.ListaRow item)
    {
        foreach (var campo in item.GetListaCampoRows())
            AddGridColumn(campo);

        bool possuiPaginacao = VerificaVerdadeiroOuFalso(item.IndicaPaginacao);
        gvDados.SettingsPager.Visible = possuiPaginacao;

        if (possuiPaginacao)
            gvDados.SettingsPager.PageSize = item.QuantidadeItensPaginacao;

        gvDados.FilterExpression = item.FiltroAplicado;
    }

    private void AddGridColumn(DsListaProcessos.ListaCampoRow campo)
    {
        GridViewEditDataColumn colTxt = (GridViewEditDataColumn)gvDados.Columns[campo.NomeCampo];

        if (VerificaVerdadeiroOuFalso(campo.IndicaCampoControle))
        {
            colTxt.Visible = false;
            colTxt.ShowInCustomizationForm = false;
            string iniciais = campo.IniciaisCampoControlado;
            if (!string.IsNullOrEmpty(iniciais))
                colTxt.Name = string.Format("colCC_{0}", iniciais);
        }
        else
        {
            colTxt.GroupIndex = campo.OrdemAgrupamentoCampo;
            colTxt.Name = string.Format("col#{0}", campo.CodigoCampo);
            colTxt.Caption = campo.TituloCampo;
            colTxt.VisibleIndex = campo.OrdemCampo;
            colTxt.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colTxt.Settings.AllowGroup = campo.IndicaAgrupamento == "S" ? DefaultBoolean.True : DefaultBoolean.False;
            colTxt.PropertiesEdit.DisplayFormatString = campo.Formato;
            /*if (!campo.IsLarguraColunaNull())
                colTxt.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);*/

            #region Tipo campo

            switch (campo.TipoCampo.ToUpper())
            {
                default:
                    break;
            }

            #endregion

            #region Tipo filtro

            colTxt.Settings.ShowFilterRowMenu = DefaultBoolean.True;
            colTxt.Settings.AllowAutoFilter = campo.TipoFiltro.ToUpper() == "E" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colTxt.Settings.AllowHeaderFilter = campo.TipoFiltro.ToUpper() == "C" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colTxt.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

            #endregion

            #region Tipo totalizados

            string nomeCampo = campo.NomeCampo;
            string tipoTotalizador = campo.TipoTotalizador.ToUpper();
            SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
            ASPxSummaryItem summaryItem = new ASPxSummaryItem(nomeCampo, summaryType);
            summaryItem.DisplayFormat = campo.Formato;
            gvDados.GroupSummary.Add(summaryItem);
            gvDados.TotalSummary.Add(summaryItem);

            #endregion

            #region Alinhamento

            HorizontalAlign alinhamento = HorizontalAlign.Center;
            switch (campo.AlinhamentoCampo.ToUpper())
            {
                case "E": alinhamento = HorizontalAlign.Left;
                    break;
                case "D": alinhamento = HorizontalAlign.Right;
                    break;
                case "C": alinhamento = HorizontalAlign.Center;
                    break;
            }
            colTxt.CellStyle.HorizontalAlign = alinhamento;
            colTxt.HeaderStyle.HorizontalAlign = alinhamento;

            #endregion
        }
    }

    private static SummaryItemType GetSummaryType(string tipoTotalizador)
    {
        SummaryItemType summaryType;
        switch (tipoTotalizador)
        {
            case "CONTAR":
                summaryType = SummaryItemType.Count;
                break;
            case "MÉDIA":
                summaryType = SummaryItemType.Average;
                break;
            case "SOMA":
                summaryType = SummaryItemType.Sum;
                break;
            default:
                summaryType = SummaryItemType.None;
                break;
        }
        return summaryType;
    }

    private bool VerificaVerdadeiroOuFalso(string valor)
    {
        if (valor == null)
            return false;
        return valor.ToLower().Equals("s");
    }

    private void SalvarCustomizacaoLayout()
    {
        int qtdeItensPagina = gvDados.SettingsPager.PageSize;
        string filter = gvDados.FilterExpression;
        StringBuilder comandoSql = new StringBuilder(@"
DECLARE @CodigoLista Int,
        @CodigoUsuario INT,
        @CodigoCampo INT,
        @OrdemCampo SMALLINT,
        @OrdemAgrupamentoCampo SMALLINT,
        @AreaDefault CHAR(1),
        @IndicaCampoVisivel CHAR(1),
        @LarguraColuna SMALLINT,
        @QuantidadeItensPaginacao INT,
        @FiltroAplicado VARCHAR(4000),
        @CodigoListaUsuario BIGINT");

        comandoSql.AppendLine();

        comandoSql.AppendFormat(@"
	SET @CodigoLista = {0}
	SET @CodigoUsuario = {1}
	SET @QuantidadeItensPaginacao = {2}
	SET @FiltroAplicado = '{3}'
            
 SELECT TOP 1 @CodigoListaUsuario = CodigoListaUsuario 
   FROM ListaUsuario AS lu 
  WHERE lu.CodigoLista = @CodigoLista 
    AND lu.CodigoUsuario = @CodigoUsuario

IF @CodigoListaUsuario IS NOT NULL
BEGIN
     UPDATE ListaUsuario
        SET QuantidadeItensPaginacao = @QuantidadeItensPaginacao,
			FiltroAplicado = @FiltroAplicado
      WHERE CodigoListaUsuario = @CodigoListaUsuario
END
ELSE
BEGIN
     INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, QuantidadeItensPaginacao, FiltroAplicado, IndicaListaPadrao) VALUES (@CodigoUsuario, @CodigoLista, @QuantidadeItensPaginacao, @FiltroAplicado, 'S')
   
        SET @CodigoListaUsuario = SCOPE_IDENTITY()
END"
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, filter.Replace("'", "''"));
        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(c => !(c is GridViewBandColumn) && c.ShowInCustomizationForm))
        {
            int ordemCampo = col.VisibleIndex;
            int ordemAgrupamentoCampo = col.GroupIndex;
            string areaDefault = "L";
            string indicaCampoVisivel = col.Visible ? "S" : "N";
            int codigoCampo = ObtemCodigoCampo(col);
            double larguraColuna = col.Width.IsEmpty ? 100 : col.Width.Value;

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @CodigoCampo = {1}
    SET @OrdemCampo = {2}
    SET @OrdemAgrupamentoCampo = {3}
    SET @AreaDefault = '{4}'
    SET @IndicaCampoVisivel = '{5}'
    SET @LarguraColuna = {6}

IF EXISTS(SELECT 1 FROM ListaCampoUsuario AS lcu WHERE lcu.CodigoCampo = @CodigoCampo AND lcu.CodigoListaUsuario = @CodigoListaUsuario)
BEGIN
     UPDATE ListaCampoUsuario
        SET OrdemCampo = @OrdemCampo,
            OrdemAgrupamentoCampo = @OrdemAgrupamentoCampo,
            AreaDefault = @AreaDefault,
            IndicaCampoVisivel = @IndicaCampoVisivel,
            LarguraColuna = @LarguraColuna
      WHERE CodigoCampo = @CodigoCampo
        AND CodigoListaUsuario = @CodigoListaUsuario
END
ELSE
BEGIN
     INSERT INTO ListaCampoUsuario(
            CodigoCampo,
            CodigoListaUsuario,
            OrdemCampo,
            OrdemAgrupamentoCampo,
            AreaDefault,
            IndicaCampoVisivel,
            LarguraColuna)
     VALUES(
            @CodigoCampo,
            @CodigoListaUsuario,
            @OrdemCampo,
            @OrdemAgrupamentoCampo,
            @AreaDefault,
            @IndicaCampoVisivel,
            @LarguraColuna)
END",
     codigoUsuarioLogado,
     codigoCampo,
     ordemCampo,
     ordemAgrupamentoCampo,
     areaDefault,
     indicaCampoVisivel,
     larguraColuna);
            comandoSql.AppendLine();

            #endregion
        }
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);
    }

    private int ObtemCodigoCampo(GridViewColumn col)
    {
        int codigoCampo = -1;
        string columnName = col.Name;
        int indiceCodigoCampo = columnName.LastIndexOf('#') + 1;
        if (indiceCodigoCampo > 0)
            codigoCampo = int.Parse(columnName.Substring(indiceCodigoCampo));

        return codigoCampo;
    }

    private void RestaurarLayout()
    {
        StringBuilder comandoSql = new StringBuilder(@"
DECLARE @CodigoUsuario INT,
        @CodigoCampo INT");
        comandoSql.AppendLine();
        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(
            c => c.ShowInCustomizationForm && c is GridViewEditDataColumn))
        {
            int codigoCampo = ObtemCodigoCampo(col);

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @CodigoCampo = {1}

 DELETE 
   FROM ListaCampoUsuario
  WHERE CodigoListaUsuario IN(SELECT lu.CodigoListaUsuario FROM ListaUsuario lu WHERE lu.CodigoUsuario = @CodigoUsuario AND lu.CodigoLista = {2})
    AND CodigoCampo = @CodigoCampo",
                    codigoUsuarioLogado,
                    codigoCampo,
                    codigoLista);
            comandoSql.AppendLine();

            #endregion
        }

        comandoSql.AppendFormat(@"   
     DELETE FROM ListaUsuario
       WHERE CodigoLista = {0}
         AND CodigoUsuario = @CodigoUsuario", codigoLista);
        comandoSql.AppendLine();

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);
    }

    #endregion

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parameter = (e.Parameter ?? string.Empty).ToLower();
        switch (parameter)
        {
            case "save_layout":
                SalvarCustomizacaoLayout();
                break;
            case "restore_layout":
                RestaurarLayout();
                break;
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "R")
        {
            InitData();
        }
    }
}
