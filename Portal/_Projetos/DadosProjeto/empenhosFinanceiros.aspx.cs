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
using DevExpress.Web;
using System.IO;
using DevExpress.XtraPrinting;

public partial class empenhosFinanceiros : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    public string mostraTDProjeto = "";
    private string resolucaoCliente = "";

    int codigoObjeto = -1;
    string iniciaisAssociacao = "";
    string tipoTela = "";
    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podeAprovar = true;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            rbDespesaReceita.SelectedItem = new ListEditItem("Despesa", "D");

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["tp"] != null && Request.QueryString["tp"].ToString() != "")
            tipoTela = Request.QueryString["tp"].ToString();
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoObjeto = int.Parse(Request.QueryString["CP"].ToString());

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoObjeto, "null", "PR", 0, "null", "PR_AltEmpFin");

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoObjeto, "null", "PR", 0, "null", "PR_IncEmpFin");

        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoObjeto, "null", "PR", 0, "null", "PR_ExcEmpFin");

        podeAprovar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoObjeto, "null", "PR", 0, "null", "PR_AprEmpFin");

        if (Request.QueryString["IA"] != null && Request.QueryString["IA"].ToString() != "")
            iniciaisAssociacao = Request.QueryString["IA"].ToString();

        if (codigoObjeto != -1)
        {
            gvDados.Columns["NomeProjeto"].Visible = false;
            mostraTDProjeto = "display:none";
        }

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            podeIncluir = false;
            podeEditar = false;
            podeExcluir = false;
            podeAprovar = false;
            ddlProjeto.JSProperties["cp_RO"] = "S";
        }

        carregaComboProjetos();
        carregaComboTarefasCronogramaProjeto();
        carregaComboRecursosTarefa();
        carregaComboContas();
        carregaComboRazaoSocial();
        carregaComoFontePagadora();

        //if (!IsPostBack)
        //{
        carregaGvDados();//a grid veio com recursos de agrupar, e por isso nao pode ser chamada so no postback
                         //}    


        DataSet dsParametros = cDados.getParametrosSistema("labelDespesa");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            rbDespesaReceita.Items[0].Text = dsParametros.Tables[0].Rows[0]["labelDespesa"].ToString();
        }
        cDados.setaTamanhoMaximoMemo(txtComentariosEmpenho, 4000, lblContadorMemoComentariosEmpenho);
        cDados.setaTamanhoMaximoMemo(mmPagamento, 4000, lblContadorMemoComentariosPagamento);
        cDados.setaTamanhoMaximoMemo(mmAprovacao, 4000, lblContadorMemoComentarios);
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/EmpenhoFinanceiro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "EmpenhoFinanceiro", "empenhosFinanceiros", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 390;
        //gvDados.Width = new Unit((largura - 10) + "px");
    }


    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string where = "";
        string orderby = "";
        if (tipoTela == "Empenhos")
        {
            where = " and  lf.DataPagamentoRecebimento is null";
            orderby = " order by lf.DataEmpenho desc";
        }

        DataSet ds = getEmpenhosFinanceirosProjeto(codigoObjeto, where, orderby);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    public DataSet getEmpenhosFinanceirosProjeto(int codigoProjeto, string where, string orderby)
    {
        string comandoSQL = string.Format(@"            
DECLARE @CodigoEntidade int, 
        @CodigoProjeto int, 
        @CodigoUsuario int, 
        @IndicaEmpenhoPagamento char(1)

    SET @CodigoEntidade = {0}
    SET @CodigoProjeto = {1} 
    SET @CodigoUsuario = {2}
    SET @IndicaEmpenhoPagamento = 'E'

 SELECT * FROM [dbo].[f_gestconv_GetLancamentosFinanceiros] (@CodigoEntidade, @CodigoProjeto, @CodigoUsuario, @IndicaEmpenhoPagamento, 'N')
  ORDER BY 
        DataPagamentoRecebimento DESC
            ", codigoEntidadeUsuarioResponsavel, codigoProjeto, codigoUsuarioResponsavel);

        return cDados.getDataSet(comandoSQL);
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Filter)
            return;

        string valorDataImportacao = gvDados.GetRowValues(e.VisibleIndex, "DataImportacao") != null ? gvDados.GetRowValues(e.VisibleIndex, "DataImportacao").ToString() : "";

        bool ehParcela = (gvDados.GetRowValues(e.VisibleIndex, "CodigoTipoAssociacao") != null &&
                                            gvDados.GetRowValues(e.VisibleIndex, "CodigoTipoAssociacao").ToString() == gvDados.GetRowValues(e.VisibleIndex, "TipoAssociacaoParcela").ToString());
        bool utilizaGestaoConvenio = VerificaUtilizaConvenio();

        if (e.ButtonID == "btnEditar")
        {
            if (!podeEditar || (valorDataImportacao != "") || (ehParcela && !utilizaGestaoConvenio))
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        else if (e.ButtonID == "btnExcluir")
        {
            if (!podeExcluir || (valorDataImportacao != "") || ehParcela)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID == "btnAprovar")
        {
            if (!podeAprovar || (valorDataImportacao != "") || (ehParcela && !utilizaGestaoConvenio))
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/aprovarReprovarDes.png";
            }
        }
    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Aprovar")
        {
            mensagemErro_Persistencia = persisteAprovacaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoLancamento = 0;

        int codigoRecurso = ddlRecurso.SelectedIndex == -1 ? -1 : int.Parse(ddlRecurso.Value.ToString());
        int codigoTarefa = ddlTarefa.SelectedIndex == -1 ? -1 : int.Parse(ddlTarefa.Value.ToString());
        string indicaDespesaReceita = rbDespesaReceita.Value.ToString();
        string dataEmpenho = (ddlEmpenhadoEm.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlEmpenhadoEm.Date);
        string valorEmpenhado = txtValor.Text == "" ? "NULL" : txtValor.Text;
        string codigoPessoaEmitente = ddlRazaoSocial.SelectedIndex == -1 ? "NULL" : ddlRazaoSocial.Value.ToString();
        string numeroDocFiscal = txtNumeroDoc.Text;
        string dataEmissaoDocFiscal = (ddlEmissaoDoc.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlEmissaoDoc.Date);
        string historicoEmpenho = txtComentariosEmpenho.Text;
        string historicoPagamento = mmPagamento.Text;
        string dataPrevistaPagamentoRecebimento = (ddlPrevistoPara.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlPrevistoPara.Date);
        string dataPagamentoRecebimento = (dtPagoEm.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dtPagoEm.Date);
        string codigoConta = ddlConta.SelectedIndex == -1 ? "NULL" : ddlConta.Value.ToString();
        string codigoFontePagadora = ddlFontePagadora.SelectedIndex == -1 ? "NULL" : ddlFontePagadora.Value.ToString();
        string indicaAprovado = podeAprovar ? "A" : "P";

        bool result = cDados.incluiEmpenhoFinanceiroProjeto(codigoObjeto, codigoRecurso, codigoTarefa, indicaDespesaReceita, dataEmpenho, valorEmpenhado, codigoPessoaEmitente
            , numeroDocFiscal, dataEmissaoDocFiscal, codigoUsuarioResponsavel, historicoEmpenho, historicoPagamento, dataPrevistaPagamentoRecebimento, codigoConta, codigoFontePagadora, dataPagamentoRecebimento, "NULL", indicaAprovado, ref codigoLancamento);

        if (result == false)
            return Resources.traducao.empenhosFinanceiros_erro_ao_salvar_o_registro_;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoLancamento = int.Parse(getChavePrimaria());

        int codigoRecurso = ddlRecurso.SelectedIndex == -1 ? -1 : int.Parse(ddlRecurso.Value.ToString());
        int codigoTarefa = ddlTarefa.SelectedIndex == -1 ? -1 : int.Parse(ddlTarefa.Value.ToString());
        string indicaDespesaReceita = rbDespesaReceita.Value.ToString();
        string dataEmpenho = (ddlEmpenhadoEm.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlEmpenhadoEm.Date);
        string valorEmpenhado = txtValor.Text == "" ? "NULL" : txtValor.Text;
        string codigoPessoaEmitente = ddlRazaoSocial.SelectedIndex == -1 ? "NULL" : ddlRazaoSocial.Value.ToString();
        string numeroDocFiscal = txtNumeroDoc.Text;
        string dataEmissaoDocFiscal = (ddlEmissaoDoc.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlEmissaoDoc.Date);
        string historicoEmpenho = txtComentariosEmpenho.Text;
        string historicoPagamento = mmPagamento.Text;
        string dataPrevistaPagamentoRecebimento = (ddlPrevistoPara.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlPrevistoPara.Date);
        string codigoConta = ddlConta.SelectedIndex == -1 ? "NULL" : ddlConta.Value.ToString();
        string codigoFontePagadora = ddlFontePagadora.SelectedIndex == -1 ? "NULL" : ddlFontePagadora.Value.ToString();
        string historicoAprovacao = mmAprovacao.Text;
        string statusEmpenho = rbStatusEmpenho.Value == null ? "P" : rbStatusEmpenho.Value.ToString();
        string dataPagamentoRecebimento = (dtPagoEm.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dtPagoEm.Date);

        bool result = cDados.atualizaEmpenhoFinanceiroProjeto(codigoLancamento, int.Parse(ddlProjeto.Value.ToString()), codigoRecurso, codigoTarefa, indicaDespesaReceita, dataEmpenho, valorEmpenhado, codigoPessoaEmitente
            , numeroDocFiscal, dataEmissaoDocFiscal, codigoUsuarioResponsavel, historicoEmpenho, historicoPagamento, dataPrevistaPagamentoRecebimento, dataPagamentoRecebimento, codigoConta, codigoFontePagadora, "NULL");

        if (result == false)
            return Resources.traducao.empenhosFinanceiros_erro_ao_salvar_o_registro_;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteAprovacaoRegistro() // Método responsável pela Atualização da aprovação do registro
    {
        int codigoLancamento = int.Parse(getChavePrimaria());

        string historicoAprovacao = mmAprovacao.Text;
        string statusEmpenho = rbStatusEmpenho.Value == null ? "P" : rbStatusEmpenho.Value.ToString();

        bool result = cDados.atualizaAprovacaoEmpenhoFinanceiroProjeto(codigoLancamento, statusEmpenho, codigoUsuarioResponsavel, historicoAprovacao);

        if (result == false)
            return Resources.traducao.empenhosFinanceiros_erro_ao_salvar_o_registro_;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoLancamento = int.Parse(getChavePrimaria());

        bool result = cDados.excluiEmpenhoFinanceiroProjeto(codigoLancamento, codigoUsuarioResponsavel);

        if (result == false)
            return Resources.traducao.empenhosFinanceiros_erro_ao_excluir_o_registro_;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    private void carregaComboProjetos()
    {
        DataSet ds = cDados.getProjetosExecucaoEntidade(codigoEntidadeUsuarioResponsavel, "");

        ddlProjeto.DataSource = ds;
        ddlProjeto.TextField = "NomeProjeto";
        ddlProjeto.ValueField = "CodigoProjeto";
        ddlProjeto.DataBind();

        if (codigoObjeto != -1)
        {
            ddlProjeto.JSProperties["cp_RO"] = "S";
            ddlProjeto.Value = codigoObjeto.ToString();
        }
        else
        {
            ddlProjeto.JSProperties["cp_RO"] = "N";
        }
    }

    private void carregaComboTarefasCronogramaProjeto()
    {
        int codigoProjeto = ddlProjeto.Value == null ? 0 : int.Parse(ddlProjeto.Value.ToString());
        DataSet ds = cDados.getTarefasCronogramaProjeto(codigoProjeto, "");

        ddlTarefa.DataSource = ds;
        ddlTarefa.TextField = "NomeTarefa";
        ddlTarefa.ValueField = "CodigoTarefa";
        ddlTarefa.DataBind();
    }

    private void carregaComboRecursosTarefa()
    {
        int codigoProjeto = ddlProjeto.Value == null ? 0 : int.Parse(ddlProjeto.Value.ToString());
        int codigoTarefa = ddlTarefa.Value == null ? 0 : int.Parse(ddlTarefa.Value.ToString());
        DataSet ds = cDados.getRecursosTarefaCronogramaProjeto(codigoProjeto, codigoTarefa, "");

        ddlRecurso.DataSource = ds;
        ddlRecurso.TextField = "NomeRecurso";
        ddlRecurso.ValueField = "CodigoRecursoProjeto";
        ddlRecurso.DataBind();
    }

    private void carregaComboContas()
    {
        string depesaReceita = rbDespesaReceita.Value == null ? "" : rbDespesaReceita.Value.ToString();

        switch (depesaReceita)
        {
            case "D":
                depesaReceita = "S";
                break;
            case "R":
                depesaReceita = "E";
                break;
            default:
                depesaReceita = "";
                break;
        }

        string entradaSaida = rbDespesaReceita.Value == null ? "" : rbDespesaReceita.Value.ToString();
        string where1 = "AND pcfc.EntradaSaida = '" + depesaReceita + "'";

        DataSet ds = getContasAnaliticasEntidadePersonalizado(codigoEntidadeUsuarioResponsavel, depesaReceita, where1);

        ddlConta.DataSource = ds;
        ddlConta.TextField = "DescricaoConta";
        ddlConta.ValueField = "CodigoConta";
        ddlConta.DataBind();
        if (ddlConta.Items.Count > 0 && !Page.IsPostBack)
        {
            ddlConta.SelectedIndex = 0;
        }
    }

    private void carregaComoFontePagadora()
    {
        DataSet ds = cDados.getFontesRecursosFinanceiros(codigoEntidadeUsuarioResponsavel, "");

        ddlFontePagadora.DataSource = ds;
        ddlFontePagadora.TextField = "NomeFonte";
        ddlFontePagadora.ValueField = "CodigoFonteRecursosFinanceiros";
        ddlFontePagadora.DataBind();
    }

    public DataSet getContasAnaliticasEntidadePersonalizado(int codigoEntidade, string tipo, string where)
    {
        string comandoSQL = string.Format(
                  @"SELECT pcfc.CodigoConta,
                           pcfc.CodigoReservadoGrupoConta + ' - ' + pcfc.DescricaoConta as DescricaoConta
                      FROM {0}.{1}.PlanoContasFluxoCaixa AS pcfc
                     WHERE pcfc.CodigoEntidade = {2}
                       AND pcfc.IndicaContaAnalitica = 'S'
                       --AND pcfc.EntradaSaida = '{3}'
                       {4}
                       ORDER BY pcfc.DescricaoConta ASC
               ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, tipo, where);
        return cDados.getDataSet(comandoSQL);
    }

    private void carregaComboRazaoSocial()
    {
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, "");

        ddlRazaoSocial.TextField = "NomePessoa";
        ddlRazaoSocial.ValueField = "CodigoPessoa";
        ddlRazaoSocial.DataSource = ds;
        ddlRazaoSocial.DataBind();
    }

    protected void ddlTarefa_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            ddlTarefa.Value = e.Parameter;
        }
        else
        {
            ddlTarefa.Value = null;
        }
        ddlTarefa.JSProperties["cp_Codigo"] = ddlTarefa.Value;
    }

    protected void ddlRecurso_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            ddlRecurso.Value = e.Parameter;
        }
        else
        {
            ddlRecurso.Value = null;
        }

        ddlRecurso.JSProperties["cp_Codigo"] = ddlRecurso.Value;
    }

    protected void ddlConta_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            ddlConta.Value = e.Parameter;
        }
        else
        {
            ddlConta.Value = null;
        }

        ddlConta.JSProperties["cp_Codigo"] = ddlConta.Value;
    }

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        e.Text = e.Text.Replace("<br>", "");
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
        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
        {
            e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8.0f, System.Drawing.FontStyle.Bold);
        }

        if (e.RowType == DevExpress.Web.GridViewRowType.Header)
        {
            if (e.Column.Name == "colValorEmpenhado")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
            }
            if (e.Column.Name == "colValorPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
            }
            if (e.Column.Name == "colNumeroDocFiscal")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
            if (e.Column.Name == "colEmitente")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
            if (e.Column.Name == "colConta")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
            if (e.Column.Name == "colDataPrevistaPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
        }

        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            if (e.Column.Name == "colDataPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
            if (e.Column.Name == "colTipo")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "D")
                    {
                        e.Text = Resources.traducao.empenhosFinanceiros_despesa;
                        e.TextValue = "Despesa";
                    }
                    else
                    {
                        e.Text = Resources.traducao.empenhosFinanceiros_receita;
                        e.TextValue = "Receita";
                    }
                }
            }

            if (e.Column.Name == "colStatus")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "R")
                    {
                        e.Text = Resources.traducao.empenhosFinanceiros_reprovado;
                        e.TextValue = "Reprovado";
                    }
                    else
                    {
                        e.Text = Resources.traducao.empenhosFinanceiros_aprovado;
                        e.TextValue = "Aprovado";
                    }
                }
            }

            if (e.Column.Name == "colDataEmpenho")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }

            if (e.Column.Name == "colPendente")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }

            if (e.Column.Name == "colDataPrevistaPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
        }

    }
    protected void gvDados_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "IndicaAprovacaoReprovacao")
        {
            string displayText;
            switch (e.Value as string)
            {
                case "A":
                    displayText = Resources.traducao.empenhosFinanceiros_aprovado;
                    break;
                case "R":
                    displayText = Resources.traducao.empenhosFinanceiros_reprovado;
                    break;
                default:
                    displayText = Resources.traducao.empenhosFinanceiros_pendente;
                    break;
            }
            e.DisplayText = displayText;
        }

        if (e.Column.FieldName == "IndicaDespesaReceita")
        {

            string displayText;
            switch (e.Value as string)
            {
                case "R":
                    displayText = Resources.traducao.empenhosFinanceiros_receita;
                    break;
                case "D":
                    displayText = Resources.traducao.empenhosFinanceiros_despesa;
                    break;
                default:
                    displayText = "";
                    break;
            }
            e.DisplayText = displayText;
        }
    }

    protected void ddlFontePagadora_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            ddlFontePagadora.Value = e.Parameter;
        }
        else
        {
            ddlFontePagadora.Value = null;
        }

        ddlFontePagadora.JSProperties["cp_Codigo"] = ddlFontePagadora.Value;

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "EmpFinPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (VerificaUtilizaConvenio())
            cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "var valores = [-1, 'LEN',  "+ codigoObjeto + "];mostraPopupLancamentoFinanceiro(valores);", true, true, false, "PagFinPrj", "Pagamentos do Projeto", this);
        else
            cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "EmpFinPrj", "Empenhos do Projeto", this);
    }

    #endregion

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cp_UtilizaConvenio"] = VerificaUtilizaConvenio();
        e.Properties["cp_CodigoProjeto"] = codigoObjeto;
    }

    private bool VerificaUtilizaConvenio()
    {
        var nomeParametro = "utilizaConvenio";
        var ds = cDados.getParametrosSistema(nomeParametro);
        var dr = ds.Tables[0].Rows[0];
        if (dr.IsNull(nomeParametro))
            return false;

        return dr.Field<string>(nomeParametro).ToUpper() == "S";
    }
}
