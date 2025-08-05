using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;

public partial class _VisaoNE_Administracao_CadastroServicos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private bool chamadaPopUp = false;
    private int codigoObra = -1;
    private int codigoProjeto = -1;
    private int codigoTipoProjeto;
    bool readOnly = false;
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

        chamadaPopUp = Request.QueryString["ChP"] != null && Request.QueryString["ChP"].ToString() == "S";

        bool podeAlterar = false;
        bool podeExcluir = false;

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            try
            {
                codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

                codigoObra = cDados.getCodigoObra(codigoProjeto);

            }
            catch { }
        }

        codigoTipoProjeto = 12;

        podeAlterar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltOSERV");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ExcOSERV");



        if (!chamadaPopUp)
        {
            btnExcluir.ClientVisible = false;
            mostrarBotaoCancelar = "display: none;";
            tamanhoTable = "98%";
        }

        carregaComboMunicipio();
        carregaComboProjetos();

        if (codigoObra != -1)
        {
            callbackSalvar.JSProperties["cp_TipoOperacao"] = readOnly ? "C" : "E";
            if (!IsPostBack && !IsCallback)
                carregaDadosObra();
        }
        else
        {
            callbackSalvar.JSProperties["cp_TipoOperacao"] = "I";
            btnExcluir.ClientVisible = false;
        }

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            desabilitaComponentes();
        }
        else
        {
            if (podeAlterar == false)
                desabilitaComponentes();

            if (podeExcluir == false)
                btnExcluir.ClientVisible = false;
        }

        cDados.setaTamanhoMaximoMemo(txtEndereco, 500, lblContadorEnderecoCompleto);
        cDados.setaTamanhoMaximoMemo(txtDetalhamento, 4000, lblContadorDetalhamento);
        cDados.setaTamanhoMaximoMemo(txtObservacoes, 2000, lblContadorObservacoes);
    }
    
    private void desabilitaComponentes()
    {
        txtNomeObra.ClientEnabled = false;
        txtObservacoes.ClientEnabled = false;
        ddlInicio.ClientEnabled = false;
        ddlMunicipio.ClientEnabled = false;
        ddlTermino.ClientEnabled = false;

        btnSalvar.ClientVisible = false;
    }

    #region Combos
        
    private void carregaComboMunicipio()
    {
        DataSet ds = cDados.getMunicipiosObra("");

        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataSource = ds;
        ddlMunicipio.DataBind();
    }

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND Projeto.CodigoProjeto <> {0}", codigoProjeto);

        DataSet ds = cDados.getProjetosServico(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

        }


        ListEditItem sinProjeto = new ListEditItem(Resources.traducao.nenhum, "0");
        ddlProjetos.Items.Insert(0, sinProjeto);


        if (!IsPostBack && ddlProjetos.Items.Count > 0)
            ddlProjetos.SelectedIndex = 0;
    }

    #endregion

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgStatusGravacao = "";
        bool result = false;
        string statusGravacao = "0";

        if (e.Parameter.ToString() == "X")
        {
            result = cDados.excluiObra(codigoObra, codigoUsuarioResponsavel, ref msgStatusGravacao);

            if (result)
            {
                msgStatusGravacao = "Serviço excluído com sucesso!";
                statusGravacao = "1";
            }
        }
        else
        {
            string nomeObra, numeroOS, dataEmissaoOS, enderecoCompleto, coordenadasGPS, dataInicio, dataTermino, detalhamento, licencas, observacoes;
            int codigoMunicipio, codigoProjetoVinculado;

            nomeObra = txtNomeObra.Text;
            numeroOS = txtNumeroOS.Text;
            dataEmissaoOS = ddlEmissao.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlEmissao.Date) : "NULL";
            codigoMunicipio = int.Parse(ddlMunicipio.Value.ToString());
            codigoProjetoVinculado = int.Parse(ddlProjetos.Value.ToString());
            enderecoCompleto = txtEndereco.Text;
            coordenadasGPS = txtCoordenadas.Text;
            dataInicio = ddlInicio.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date) : "NULL";
            dataTermino = ddlTermino.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date) : "NULL";
            detalhamento = txtDetalhamento.Text;
            licencas = txtLicencas.Text;
            observacoes = txtObservacoes.Text;

            if (codigoObra == -1)
            {
                if (cDados.verificaExistenciaNomeTabelaProjeto(codigoEntidadeUsuarioResponsavel, nomeObra, ""))
                {
                    msgStatusGravacao = "Já existe um serviço ou obra cadastrado com este nome. <br>Altere o nome do serviço e tente incluir novamente!";
                }
                else
                {
                    DataSet dsUnidadePadrao = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "CodigoUnidadePadraoObra");

                    int codigounidadePadrao = codigoEntidadeUsuarioResponsavel;

                    if (cDados.DataSetOk(dsUnidadePadrao) && cDados.DataTableOk(dsUnidadePadrao.Tables[0]) && dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString() != "")
                    {
                        codigounidadePadrao = int.Parse(dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString());
                    }

                    result = cDados.incluirServico(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigounidadePadrao, nomeObra
                        , numeroOS, dataEmissaoOS, codigoMunicipio, codigoProjetoVinculado, enderecoCompleto, coordenadasGPS, dataInicio, dataTermino
                        , detalhamento, licencas, observacoes, codigoTipoProjeto, ref codigoObra, ref msgStatusGravacao);

                    if (result)
                    {
                        msgStatusGravacao = "Serviço incluído com sucesso!";
                        statusGravacao = "1";
                        btnSalvar.ClientEnabled = false;
                    }
                }
            }
            else
            {
                string whereNomeObra = string.Format(@"AND p.CodigoProjeto NOT IN(SELECT o.CodigoProjeto FROM {0}.{1}.Obra o WHERE o.CodigoObra = {2})", cDados.getDbName()
                                                                                                                                                , cDados.getDbOwner()
                                                                                                                                                , codigoObra);
                if (cDados.verificaExistenciaNomeTabelaProjeto(codigoEntidadeUsuarioResponsavel, nomeObra, whereNomeObra))
                {
                    msgStatusGravacao = "Já existe um serviço ou obra cadastrado com este nome. <br>Altere o nome do serviço e tente salvar novamente!";
                }
                else
                {
                    result = cDados.atualizaServico(codigoObra, codigoUsuarioResponsavel, nomeObra
                        , numeroOS, dataEmissaoOS, codigoMunicipio, codigoProjetoVinculado, enderecoCompleto, coordenadasGPS, dataInicio, dataTermino
                        , detalhamento, licencas, observacoes, ref msgStatusGravacao);

                    if (result)
                    {
                        msgStatusGravacao = "Serviço alterado com sucesso!";
                        statusGravacao = "1";
                    }
                }
            }
        }

        callbackSalvar.JSProperties["cp_MsgStatus"] = msgStatusGravacao;
        callbackSalvar.JSProperties["cp_Status"] = statusGravacao;

    }

    private void carregaDadosObra()
    {
        DataSet ds = cDados.getInformacoesServico(codigoObra, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtNomeObra.Text = dr["NomeProjeto"].ToString();
            txtNumeroOS.Text = dr["NumeroOS"].ToString();

            ddlMunicipio.Value = (dr["CodigoMunicipioObra"] != null) ? dr["CodigoMunicipioObra"].ToString() : "";
            ddlProjetos.Value = dr["CodigoProjetoAssociado"].ToString();
            txtEndereco.Text = dr["LocalObra"].ToString();
            txtCoordenadas.Text = dr["CoordenadaGPS"].ToString();
                                  
            if (dr["InicioPrevistoObraPBA"].ToString() != "")
                ddlInicio.Date = DateTime.Parse(dr["InicioPrevistoObraPBA"].ToString());
            if (dr["TerminoPrevistoObraPBA"].ToString() != "")
                ddlTermino.Date = DateTime.Parse(dr["TerminoPrevistoObraPBA"].ToString());
            if (dr["DataEmissaoOS"].ToString() != "")
                ddlEmissao.Date = DateTime.Parse(dr["DataEmissaoOS"].ToString());

            txtDetalhamento.Text = dr["DetalhamentoObra"].ToString();
            txtLicencas.Text = dr["LicencaExigida"].ToString();

            txtObservacoes.Text = dr["ObservacaoObra"].ToString();
        }
    }
}