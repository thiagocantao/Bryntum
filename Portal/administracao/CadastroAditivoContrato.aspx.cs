using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;

public partial class Administracao_CadastroAditivoContrato : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private bool chamadaPopUp = false;
    private int codigoProjeto = -1;
    public string mostrarBotaoCancelar = "", tamanhoTable = "100%";


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
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack && !IsCallback)
        {
            hfWF.Set("CodigoWF", "-1");
            hfWF.Set("CodigoCI", "-1");
            hfWF.Set("CodigoAditivo", "-1");
        }

        chamadaPopUp = Request.QueryString["ChP"] != null && Request.QueryString["ChP"].ToString() == "S";

        ddlDataPrazo.JSProperties["cp_RO"] = "N";

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            desabilitaComponentes();
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            try
            {
                codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            }
            catch { }
        }       

        if (chamadaPopUp)
        {
            btnCancelar.ClientSideEvents.Click = @"function (s, e) {window.top.fechaModal();}";
        }
        else
        {
            mostrarBotaoCancelar = "display: none;";
            tamanhoTable = "100%";
        }

        carregaComboContrato();
        carregaComboTipoInstrumento();

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "")
        {
            hfWF.Set("CodigoAditivo", Request.QueryString["CA"].ToString());

            if (!IsPostBack && !IsCallback)
            {
                DataSet dsAditivo = cDados.getAditivosContratos(" AND ac.CodigoAditivoContrato = " + hfWF.Get("CodigoAditivo"));

                montaCampos(dsAditivo);
            }
        }
        else
        {
            getValorAtualContrato();
        }
        cDados.aplicaEstiloVisual(Page);

        
    }

    private void getValorAtualContrato()
    {
        txtValorDoContrato.Text = "";

        if (ddlContrato.SelectedIndex != -1)
        {
            DataSet ds = cDados.getInformacoesContrato(int.Parse(ddlContrato.Value.ToString()));

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtValorDoContrato.Text = ds.Tables[0].Rows[0]["ValorContrato"].ToString();
                txtNumeroInstrumento.Text = ds.Tables[0].Rows[0]["NumeroContrato"].ToString() + "-" + 0;
            }
        }
    }

    private void desabilitaComponentes()
    {
        ddlContrato.ClientEnabled = false;
        ddlTipoInstrumento.ClientEnabled = false;
        txtNumeroInstrumento.ClientEnabled = false;
        ddlAditivar.ClientEnabled = false;
        txtNovoValor.ClientEnabled = false;
        ddlDataPrazo.ClientEnabled = false;
        mmMotivo.ClientEnabled = false;
        ddlDataPrazo.JSProperties["cp_RO"] = "S";
        btnSalvar.ClientVisible = false;
    }

    #region Combos

    private void carregaComboContrato()
    {
        DataSet ds = cDados.getContratosAtivosProjeto(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigoProjeto, "");

        ddlContrato.TextField = "NumeroContrato";
        ddlContrato.ValueField = "CodigoContrato";
        ddlContrato.DataSource = ds;
        ddlContrato.DataBind();

        if (!IsPostBack && !IsCallback && ddlContrato.Items.Count > 0)
            ddlContrato.SelectedIndex = 0;
    }

    private void carregaComboTipoInstrumento()
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, " AND IndicaTipoAditivoContrato = 'S'");

        ddlTipoInstrumento.TextField = "DescricaoTipoContrato";
        ddlTipoInstrumento.ValueField = "CodigoTipoContrato";
        ddlTipoInstrumento.DataSource = ds;
        ddlTipoInstrumento.DataBind();
    }

    #endregion

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackSalvar.JSProperties["cp_MsgStatus"] = "";
        callbackSalvar.JSProperties["cp_Status"] = "";

        int codigoWF, codigoInstancia;

        codigoWF = int.Parse(hfWF.Get("CodigoWF").ToString());
        codigoInstancia = int.Parse(hfWF.Get("CodigoCI").ToString());

        if (e.Parameter.ToString() == "S")
        {
            string msgStatusGravacao = "";
            bool result = false;
            string statusGravacao = "0";

            int codigoContrato = int.Parse(ddlContrato.Value.ToString());
            int codigoTipoInstrumento = int.Parse(ddlTipoInstrumento.Value.ToString());
            string numeroContrato = txtNumeroInstrumento.Text;
            string aditivar = ddlAditivar.Value.ToString();
            string novoValor = aditivar == "VL" || aditivar == "PV" || aditivar == "TM" ? txtNovoValor.Text : "NULL";
            string valorContrato = txtValorDoContrato.Text;
            string valorAditivo = aditivar == "VL" || aditivar == "PV" ? txtValorAditivo.Text : "NULL";
            string dataPrazo = aditivar == "PR" || aditivar == "PV" || aditivar == "TM" ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrazo.Date) : "NULL";
            string observacoes = mmMotivo.Text;

            if (hfWF.Get("CodigoAditivo") == null || hfWF.Get("CodigoAditivo").ToString() == "-1" || hfWF.Get("CodigoAditivo").ToString() == "")
            {
                int novoCodigoAditivo = 0;

                result = cDados.incluirAditivoContratoObra(codigoContrato, codigoTipoInstrumento, numeroContrato, aditivar, novoValor, valorContrato, valorAditivo, dataPrazo, observacoes, codigoUsuarioResponsavel, codigoWF, codigoInstancia, ref novoCodigoAditivo, ref msgStatusGravacao);

                if (result)
                {
                    msgStatusGravacao = "Contrato aditivado com sucesso!";
                    statusGravacao = "2";
                    btnSalvar.ClientEnabled = false;

                    DataSet dsAditivo = cDados.getAditivosContratos(" AND ac.CodigoAditivoContrato = " + novoCodigoAditivo);
                    montaCampos(dsAditivo);
                    callbackSalvar.JSProperties["cp_CodigoAditivoContrato"] = novoCodigoAditivo;
                }
            }
            else
            {
                result = cDados.atualizaAditivoContratoObra(int.Parse(hfWF.Get("CodigoAditivo").ToString()), codigoContrato, codigoTipoInstrumento, numeroContrato, aditivar, novoValor, valorContrato, valorAditivo, dataPrazo, observacoes, ref msgStatusGravacao);

                if (result)
                {
                    msgStatusGravacao = "Aditivo alterado com sucesso!";
                    statusGravacao = "1";
                }

                DataSet dsAditivo = cDados.getAditivosContratos(" AND ac.CodigoAditivoContrato = " + hfWF.Get("CodigoAditivo").ToString());
                montaCampos(dsAditivo);
            }

            callbackSalvar.JSProperties["cp_MsgStatus"] = msgStatusGravacao;
            callbackSalvar.JSProperties["cp_Status"] = statusGravacao;
        }
        else
        {
            DataSet dsAditivo = cDados.getAditivosContratos(" AND ac.CodigoWorkflow = " + codigoWF + " AND ac.CodigoInstanciaWf = " + codigoInstancia);
            montaCampos(dsAditivo);
        }
        
    }

    private void montaCampos(DataSet dsAditivo)
    {
        if (cDados.DataSetOk(dsAditivo) && cDados.DataTableOk(dsAditivo.Tables[0]))
        {
            DataRow dr = dsAditivo.Tables[0].Rows[0];
            callbackSalvar.JSProperties["cp_CodigoAditivoContrato"] = dr["CodigoAditivoContrato"].ToString();
            callbackSalvar.JSProperties["cp_CodigoContrato"] = dr["CodigoContrato"].ToString();
            callbackSalvar.JSProperties["cp_CodigoTipoContratoAditivo"] = dr["CodigoTipoContratoAditivo"].ToString();
            callbackSalvar.JSProperties["cp_DataAprovacaoAditivo"] = dr["DataAprovacaoAditivo"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["DataAprovacaoAditivo"].ToString())) : "";
            callbackSalvar.JSProperties["cp_DataInclusao"] = dr["DataInclusao"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["DataInclusao"].ToString())) : "";
            callbackSalvar.JSProperties["cp_DescricaoMotivoAditivo"] = dr["DescricaoMotivoAditivo"].ToString();
            callbackSalvar.JSProperties["cp_NovaDataVigencia"] = dr["NovaDataVigencia"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["NovaDataVigencia"].ToString())).Replace("/", "") : "";
            callbackSalvar.JSProperties["cp_NovoValorContrato"] = dr["NovoValorContrato"].ToString();
            callbackSalvar.JSProperties["cp_NumeroContratoAditivo"] = dr["NumeroContratoAditivo"].ToString();
            callbackSalvar.JSProperties["cp_TipoAditivo"] = dr["TipoAditivo"].ToString();
            callbackSalvar.JSProperties["cp_UsuarioAprovacao"] = dr["UsuarioAprovacao"].ToString();
            callbackSalvar.JSProperties["cp_UsuarioInclusao"] = dr["UsuarioInclusao"].ToString();
            callbackSalvar.JSProperties["cp_ValorAditivo"] = dr["ValorAditivo"].ToString();
            callbackSalvar.JSProperties["cp_ValorContrato"] = dr["ValorContrato"].ToString();            
        }
    }

    protected void callbackInstancia_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackInstancia.JSProperties["cp_URL"] = "";

        int codigoIsntanciaWf = int.Parse(e.Parameter.ToString());
        string msgStatusGravacao = "";        
        cDados.atualizaInstanciaAditivoContratoObra(int.Parse(hfWF.Get("CodigoAditivo").ToString()), codigoIsntanciaWf, ref msgStatusGravacao);

        DataSet ds = cDados.getWorkFlows("AND wf.CodigoWorkflow = " + hfWF.Get("CodigoWF").ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string urlRedirect = string.Format(@"../wfEngineInterno.aspx?CF={0}&CP={1}&CW={2}&CI={3}&CE=1&CS=1"
                , ds.Tables[0].Rows[0]["CodigoFluxo"].ToString()
                , codigoProjeto
                , hfWF.Get("CodigoWF").ToString()
                , codigoIsntanciaWf);

            callbackInstancia.JSProperties["cp_URL"] = urlRedirect;
        }
    }
}