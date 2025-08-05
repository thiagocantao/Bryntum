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

public partial class administracao_frmAditivosContratoNovo : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string oldAditivar = "";

    int codigoContrato = -1;
    public string somenteLeitura = "";

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
        //DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        string IniciaisTipoAssociacao = "CT";

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        }
        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CC"] != null)
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 120;
        }

        ddlDataPrazo.JSProperties["cp_RO"] = "N";
        //somenteLeitura = "S";
        if (somenteLeitura == "S" || verificaAditivoPendenteFluxo())
        {
            ddlDataPrazo.JSProperties["cp_RO"] = "S";
        }
        else
        {

            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncAdt");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcAdt");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltAdt");

            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }
        carregaComboTipoInstrumento();
        //if (!IsPostBack)
        //{
        carregaGvDados();
        //}

        getValorAtualContrato();
        oldAditivar = ddlAditivar.Value != null ? ddlAditivar.Value.ToString() : "NULL";

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        DataSet ds = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtNumeroContrato.Text = ds.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = (ds.Tables[0].Rows[0]["StatusContrato"].ToString().ToUpper().Trim() == "A") ? "Ativo" : "Inativo";
            txtInicioVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataInicio"].ToString()).ToString("dd/MM/yyyy");
            txtTerminoVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataTermino"].ToString()).ToString("dd/MM/yyyy");

        }
    }

    private bool verificaAditivoPendenteFluxo()
    {
        bool possuiPendenciaFluxo = cDados.verificaPendenciaAditivoFluxo(codigoContrato);

        if (possuiPendenciaFluxo)
            gvDados.Settings.VerticalScrollableHeight = gvDados.Settings.VerticalScrollableHeight - 20;

        gvDados.Settings.ShowStatusBar = possuiPendenciaFluxo ? DevExpress.Web.GridViewStatusBarMode.Visible : DevExpress.Web.GridViewStatusBarMode.Hidden;

        return possuiPendenciaFluxo;
    }

    private void getValorAtualContrato()
    {
        DataSet ds = cDados.getInformacoesContrato(codigoContrato);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pnCallback.JSProperties["cp_ValorContrato"] = ds.Tables[0].Rows[0]["ValorContrato"].ToString();
            pnCallback.JSProperties["cp_TerminoContrato"] = ds.Tables[0].Rows[0]["DataTermino"].ToString();
            pnCallback.JSProperties["cp_StatusContrato"] = ds.Tables[0].Rows[0]["StatusContrato"].ToString();

            int numeroProximoAditivo = 1;

            if (gvDados.VisibleRowCount > 0)
            {
                try
                {
                    string numeroUltimoAditivo = gvDados.GetRowValues(gvDados.VisibleRowCount - 1, "NumeroContratoAditivo") + "";
                    int index = numeroUltimoAditivo.LastIndexOf('-');
                    if (index >= 0)
                        numeroProximoAditivo = int.Parse(numeroUltimoAditivo.Substring(index + 1)) + 1;
                }
                catch { }
            }

            pnCallback.JSProperties["cp_NumeroNovoInstrumento"] = ds.Tables[0].Rows[0]["NumeroContrato"].ToString() + "-" + numeroProximoAditivo;
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

    }

    #endregion

    #region Combos

    private void carregaComboTipoInstrumento()
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, " AND IndicaTipoAditivoContrato = 'S'");

        //ddlTipoInstrumento.TextField = "DescricaoTipoContrato";
        //ddlTipoInstrumento.ValueField = "CodigoTipoContrato";
        //ddlTipoInstrumento.DataSource = ds;
        //ddlTipoInstrumento.DataBind();
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getInformacoesContrato(codigoContrato);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["StatusContrato"].ToString() == "I")
            {
                podeIncluir = false;
            }
        }

        DataSet dsAditivo = cDados.getAditivosContratos(" AND CodigoContrato = " + codigoContrato);

        if ((cDados.DataSetOk(dsAditivo)))
        {
            gvDados.DataSource = dsAditivo;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string editavel = "N";
        string editavelTC = "N";
        string codigoWF = "";

        editavel = gvDados.GetRowValues(e.VisibleIndex, "Editavel") + "";
        editavelTC = gvDados.GetRowValues(e.VisibleIndex, "TipoAditivo") + "";
        codigoWF = gvDados.GetRowValues(e.VisibleIndex, "CodigoWorkflow") + "";

        var tipoOperacao = (Request.QueryString["TO"] + "");
        var indicaConsultar = (tipoOperacao == "Consultar");

        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir && editavelTC == "TC" && codigoWF == "" && !indicaConsultar)
            {
                e.Enabled = true;
            }
            else if (podeExcluir && editavel == "S" && codigoWF == "" && !indicaConsultar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
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

        getValorAtualContrato();
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
        int novoCodigoAditivo = 0;
        string msgStatusGravacao = "";

        int codigoTipoInstrumento = 2;//int.Parse(ddlTipoInstrumento.Value.ToString());
        string numeroContrato = txtNumeroInstrumento.Text;
        string aditivar = "";

        //if ((ddlTipoInstrumento.Text == "Termo de Encerramento de Contrato" || ddlTipoInstrumento.Text == "TEC"))
        //    aditivar = "TC";
        //else
        aditivar = ddlAditivar.Value.ToString();

        string novoValor = aditivar == "VL" || aditivar == "PV" || aditivar == "TC" ? txtNovoValor.Text : "NULL";
        string valorContrato = txtValorDoContrato.Text;
        string valorAditivo = aditivar == "VL" || aditivar == "PV" ? txtValorAditivo.Text : "NULL";
        string dataPrazo = aditivar == "PR" || aditivar == "PV" || aditivar == "TC" ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrazo.Date) : "NULL";
        string observacoes = mmMotivo.Text;

        bool result = cDados.incluirAditivoContratoObra(codigoContrato, codigoTipoInstrumento, numeroContrato, aditivar, novoValor, valorContrato, valorAditivo, dataPrazo, observacoes, codigoUsuarioResponsavel, -1, -1, ref novoCodigoAditivo, ref msgStatusGravacao);

        if (result == false)
            return msgStatusGravacao;
        else
        {
            if (aditivar == "TC") // se foi incluído um TEC, não pode mais alterar nada em aditivos.
            {
                podeIncluir = false;
                podeEditar = false;
                podeExcluir = false;
            }
            carregaGvDados();
            return "";
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoAditivo = int.Parse(getChavePrimaria());

        string msgStatusGravacao = "";

        int codigoTipoInstrumento = 2;//int.Parse(ddlTipoInstrumento.Value.ToString());
        string numeroContrato = txtNumeroInstrumento.Text;
        string aditivar = "";

        //if ((ddlTipoInstrumento.Text == "Termo de Encerramento de Contrato" || ddlTipoInstrumento.Text == "TEC"))
        //    aditivar = "TC";
        //else
        aditivar = ddlAditivar.Value.ToString();

        string novoValor = aditivar == "VL" || aditivar == "PV" || aditivar == "TC" ? txtNovoValor.Text : "NULL";
        string valorContrato = txtValorDoContrato.Text;
        string valorAditivo = aditivar == "VL" || aditivar == "PV" ? txtValorAditivo.Text : "NULL";
        string dataPrazo = aditivar == "PR" || aditivar == "PV" || aditivar == "TC" ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrazo.Date) : "NULL";
        string observacoes = mmMotivo.Text;

        bool result = cDados.atualizaAditivoContratoObra(codigoAditivo, codigoContrato, codigoTipoInstrumento, numeroContrato, aditivar, novoValor, valorContrato, valorAditivo, dataPrazo, observacoes, ref msgStatusGravacao);

        if (result == false)
            return msgStatusGravacao;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoAditivo = int.Parse(getChavePrimaria());
        string msgStatusGravacao = "";

        bool result = cDados.excluiAditivoContratoObra(codigoAditivo, codigoUsuarioResponsavel, ref msgStatusGravacao); ;

        if (result == false)
            return msgStatusGravacao;
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

    protected string botaoNovo()
    {
        var tipoOperacao = (Request.QueryString["TO"] + "");
        var indicaConsultar = (tipoOperacao == "Consultar");
        return string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", (podeIncluir && !indicaConsultar) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""debugger;onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>");
    }
}
