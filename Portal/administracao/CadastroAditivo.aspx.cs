using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;


public partial class Administracao_CadastroContrato : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoAditivo = -1;
    private int codigoWf = -1;
    private int codigoInstanciaWf = -1;
    bool readOnly = false;

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

        if (Request.QueryString["CA"] != null)
            codigoAditivo = int.Parse(Request.QueryString["CA"].ToString());
        else
        {
            if (Request.QueryString["CWF"] != null)
                codigoWf = int.Parse(Request.QueryString["CWF"].ToString());

            if (Request.QueryString["CIWF"] != null)
                codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

            codigoAditivo = cDados.getCodigoAditivoFluxo(codigoWf, codigoInstanciaWf);
        }

        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";

        ddlDataPrazo.JSProperties["cp_RO"] = readOnly ? "S" : "N";

        carregaComboTipoInstrumento();
        if (!IsPostBack)
        {
            montaCampos();
        }

        if (readOnly)
            desabilitaComponentes();
    }

    private void desabilitaComponentes()
    {
        ddlTipoInstrumento.ClientEnabled = false;
        txtNumeroInstrumento.ClientEnabled = false;
        ddlAditivar.ClientEnabled = false;
        txtNovoValor.ClientEnabled = false;
        txtValorAditivo.ClientEnabled = false;
        ddlDataPrazo.ClientEnabled = false;
        mmMotivo.ClientEnabled = false;
        ddlDataPrazo.JSProperties["cp_RO"] = "S";
        btnSalvar.ClientVisible = false;
    }
    
    private void montaCampos()
    {
        if (codigoAditivo != -1)
        {
            DataSet dsAditivo = cDados.getAditivo(codigoAditivo, "");

            DataRow dr = dsAditivo.Tables[0].Rows[0];

            int codigoContrato = int.Parse(dr["CodigoContrato"].ToString());

            ddlTipoInstrumento.Value = dr["CodigoTipoContratoAditivo"].ToString();
            txtNumeroInstrumento.Text = dr["NumeroContratoAditivo"].ToString();
            ddlAditivar.Value = dr["TipoAditivo"].ToString();
            txtValorAditivo.Text = dr["ValorAditivo"].ToString();           
            txtNovoValor.Text = dr["NovoValorContrato"].ToString();
            ddlDataPrazo.Value = dr["NovaDataVigencia"];
            mmMotivo.Text = dr["DescricaoMotivoAditivo"].ToString();
            txtDataInclusao.Text = string.Format("{0:dd/MM/yyyy}", dr["DataInclusao"]);
            txtUsuarioInclusao.Text = dr["UsuarioInclusao"].ToString();
            txtDataAprovacao.Text = string.Format("{0:dd/MM/yyyy}", dr["DataAprovacaoAditivo"]);
            txtUsuarioAprovacao.Text = dr["UsuarioAprovacao"].ToString();

            DataSet ds = cDados.getInformacoesContrato(codigoContrato);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtValorDoContrato.Text = ds.Tables[0].Rows[0]["ValorContrato"].ToString();
                pnCallback.JSProperties["cp_ValorContrato"] = ds.Tables[0].Rows[0]["ValorContrato"].ToString();
                pnCallback.JSProperties["cp_TerminoContrato"] = ds.Tables[0].Rows[0]["DataTermino"].ToString();
                pnCallback.JSProperties["cp_StatusContrato"] = ds.Tables[0].Rows[0]["StatusContrato"].ToString();
            }
        }
    }

    private void carregaComboTipoInstrumento()
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, " AND IndicaTipoAditivoContrato = 'S'");

        ddlTipoInstrumento.TextField = "DescricaoTipoContrato";
        ddlTipoInstrumento.ValueField = "CodigoTipoContrato";
        ddlTipoInstrumento.DataSource = ds;
        ddlTipoInstrumento.DataBind();
    }

    protected void pnCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgStatusGravacao = "";

        int codigoTipoInstrumento = int.Parse(ddlTipoInstrumento.Value.ToString());
        string numeroContrato = txtNumeroInstrumento.Text;
        string aditivar = "";

        aditivar = ddlAditivar.Value.ToString();

        string novoValor = aditivar == "VL" || aditivar == "PV" ? txtNovoValor.Text : "NULL";
        string valorContrato = txtValorDoContrato.Text;
        string valorAditivo = aditivar == "VL" || aditivar == "PV" ? txtValorAditivo.Text : "NULL";
        string dataPrazo = aditivar == "PR" || aditivar == "PV" ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrazo.Date) : "NULL";
        string observacoes = mmMotivo.Text;

        bool result = cDados.atualizaDadosAditivo(codigoAditivo, codigoTipoInstrumento, numeroContrato, aditivar, novoValor, valorContrato, valorAditivo, dataPrazo, observacoes, ref msgStatusGravacao);

        if (result == false)
            pnCallback.JSProperties["cp_Msg"] = msgStatusGravacao;
        else
        {
            pnCallback.JSProperties["cp_Msg"] = "Aditivo alterado com sucesso!";
        }    
    }
}