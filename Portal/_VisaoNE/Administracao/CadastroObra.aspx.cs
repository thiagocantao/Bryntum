using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class _VisaoNE_Administracao_CadastroObra : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private bool chamadaPopUp = false;
    private int codigoObra = -1;
    private int codigoProjeto = -1;
    private int codigoTipoProjeto;
    private string tipoObra;
    bool readOnly = false;
    public string mostrarBotaoCancelar = "", tamanhoTable = "100%";
    private int CodTipoProjetoIndigena = -99;


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

        string comandoSql1 = string.Format("SELECT {0}.{1}.f_GetCodigoTipoProjeto('Indígena') as CodTipoProjetoIndigena", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds1 = cDados.getDataSet(comandoSql1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            CodTipoProjetoIndigena = Convert.ToInt32(ds1.Tables[0].Rows[0]["CodTipoProjetoIndigena"]);
        }



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
        tipoObra = Request.QueryString["TpO"];
        DefineCamposVisiveis();

        podeAlterar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltObr");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ExcObr");



        if (!chamadaPopUp)
        {
            btnExcluir.ClientVisible = false;
            mostrarBotaoCancelar = "display: none;";
            tamanhoTable = "98%";
        }

        carregaComboMunicipio();
        carregaComboSegmento();
        carregaComboTipoServico();

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
        cDados.setaTamanhoMaximoMemo(txtPrevistoPBA, 2000, lblContadorPrevistoNoPBA);
        cDados.setaTamanhoMaximoMemo(txtReferenciaPBA, 2000, lblContadorReferenciaNoPBA);
        cDados.setaTamanhoMaximoMemo(txtObservacoes, 2000, lblContadorObservacoes);
    }

    private void DefineCamposVisiveis()
    {
        if (string.IsNullOrEmpty(tipoObra))
        {
            string comandoSql = string.Format("SELECT P.CodigoTipoProjeto FROM {0}.{1}.Projeto P WHERE P.CodigoProjeto = {2} ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
            DataSet ds = cDados.getDataSet(comandoSql);
            if (ds.Tables[0].Rows.Count > 0)
                codigoTipoProjeto = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoTipoProjeto"]);
        }
        else
        {
            switch (tipoObra)
            {
                case "soci":
                    codigoTipoProjeto = 1;
                    break;
                case "plan":
                    codigoTipoProjeto = 8;
                    break;
                case "pdrs":
                    codigoTipoProjeto = 9;
                    break;
                case "indi":
                    codigoTipoProjeto = CodTipoProjetoIndigena;
                    break;
            }
        }
        if (codigoTipoProjeto != 1)
        {
            string strOcultar = "<style type=\"text/css\"> {0} {{ display: none; }} </style>";
            if (codigoTipoProjeto == 8)
            {
                lblReferencia.Text = "Referência no Plano de Segurança Pública: ";
                Header.Controls.Add(cDados.getLiteral(String.Format(strOcultar, ".PrevistoPBA")));
                Header.Controls.Add(cDados.getLiteral(String.Format(strOcultar, "#TextAnuencia")));
                Header.Controls.Add(cDados.getLiteral(String.Format(strOcultar, "#LabelAnuencia")));
            }
            else
            {
                Header.Controls.Add(cDados.getLiteral(String.Format(strOcultar, ".PrevistoPBA")));
                Header.Controls.Add(cDados.getLiteral(String.Format(strOcultar, ".ReferenciaPBA")));
            }
        }
    }

    private void desabilitaComponentes()
    {

        txtNomeObra.ClientEnabled = false;
        txtObservacoes.ClientEnabled = false;
        txtOficio.ClientEnabled = false;
        txtPrevistoPBA.ClientEnabled = false;
        txtQuantidadeObras.ClientEnabled = false;
        txtReferenciaPBA.ClientEnabled = false;
        txtTermoAnuencia.ClientEnabled = false;
        txtTermoCooperacao.ClientEnabled = false;
        ddlInicio.ClientEnabled = false;
        ddlMunicipio.ClientEnabled = false;
        ddlSegmento.ClientEnabled = false;
        ddlTermino.ClientEnabled = false;
        ddlTipoContratacao.ClientEnabled = false;
        ckConstrucao.ClientEnabled = false;
        ckReforma.ClientEnabled = false;
        ckAmpliacao.ClientEnabled = false;
        ddlInicioRepactuado.ClientEnabled = false;
        ddlTerminoRepactuado.ClientEnabled = false;

        btnSalvar.ClientVisible = false;
    }

    #region Combos

    private void carregaComboTipoServico()
    {
        DataSet ds = cDados.getTipoServico(codigoEntidadeUsuarioResponsavel, " AND ts.IndicaObra = 'S'");

        ddlTipoContratacao.TextField = "DescricaoTipoServico";
        ddlTipoContratacao.ValueField = "CodigoTipoServico";
        ddlTipoContratacao.DataSource = ds;
        ddlTipoContratacao.DataBind();
    }

    private void carregaComboSegmento()
    {
        DataSet ds = cDados.getTipoSegmentoObra(codigoEntidadeUsuarioResponsavel, "");

        ddlSegmento.TextField = "DescricaoSegmentoObra";
        ddlSegmento.ValueField = "CodigoSegmentoObra";
        ddlSegmento.DataSource = ds;
        ddlSegmento.DataBind();
    }

    private void carregaComboMunicipio()
    {
        DataSet ds = cDados.getMunicipiosObra("");

        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataSource = ds;
        ddlMunicipio.DataBind();
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
                msgStatusGravacao = "Obra excluída com sucesso!";
                statusGravacao = "1";
            }
        }
        else
        {
            string nomeObra, previsaoPBA, referenciaPBA, numeroTermoCooperacao, numeroTermoAnuencia, numeroOficio, indicaConstrucao, indicaReforma, indicaAmpliacao, dataInicio, dataTermino, observacoes, InicioRepactuado, TerminoRepactuado;
            int codigoMunicipio, codigoSegmento, quantidadeObras, codigoTipoObra;

            nomeObra = txtNomeObra.Text.Replace("'", "");
            previsaoPBA = txtPrevistoPBA.Text.Replace("'", "'+char(39)+'");
            referenciaPBA = txtReferenciaPBA.Text.Replace("'", "'+char(39)+'");
            codigoMunicipio = int.Parse(ddlMunicipio.Value.ToString());
            codigoSegmento = int.Parse(ddlSegmento.Value.ToString());
            numeroTermoCooperacao = txtTermoCooperacao.Text.Replace("'", "'+char(39)+'");
            numeroTermoAnuencia = txtTermoAnuencia.Text.Replace("'", "'+char(39)+'");
            numeroOficio = txtOficio.Text.Replace("'", "'+char(39)+'");
            quantidadeObras = int.Parse(txtQuantidadeObras.Text);
            codigoTipoObra = int.Parse(ddlTipoContratacao.Value.ToString());
            indicaConstrucao = ckConstrucao.Checked ? "S" : "N";
            indicaReforma = ckReforma.Checked ? "S" : "N";
            indicaAmpliacao = ckAmpliacao.Checked ? "S" : "N";
            dataInicio = ddlInicio.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date) : "NULL";
            dataTermino = ddlTermino.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date) : "NULL";
            InicioRepactuado = ddlInicioRepactuado.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioRepactuado.Date) : "NULL";
            TerminoRepactuado = ddlTerminoRepactuado.Text != "" ? string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoRepactuado.Date) : "NULL";

            observacoes = txtObservacoes.Text;

            if (codigoObra == -1)
            {
                if (cDados.verificaExistenciaNomeTabelaProjeto(codigoEntidadeUsuarioResponsavel, nomeObra, ""))
                {
                    msgStatusGravacao = "Já existe uma obra cadastrada com este nome. <br>Altere o nome da obra e tente incluir novamente!";
                }
                else
                {
                    DataSet dsUnidadePadrao = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "CodigoUnidadePadraoObra");

                    int codigounidadePadrao = codigoEntidadeUsuarioResponsavel;

                    if (cDados.DataSetOk(dsUnidadePadrao) && cDados.DataTableOk(dsUnidadePadrao.Tables[0]) && dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString() != "")
                    {
                        codigounidadePadrao = int.Parse(dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString());
                    }

                    result = cDados.incluirObra(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigounidadePadrao, nomeObra, previsaoPBA, referenciaPBA, codigoMunicipio, codigoSegmento
                        , numeroTermoCooperacao, numeroTermoAnuencia, numeroOficio, quantidadeObras, codigoTipoObra, indicaConstrucao, indicaReforma, indicaAmpliacao, dataInicio, dataTermino, observacoes, codigoTipoProjeto,
                        InicioRepactuado, TerminoRepactuado, ref codigoObra,  ref msgStatusGravacao);

                    if (result)
                    {
                        msgStatusGravacao = "Obra incluída com sucesso!";
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
                    msgStatusGravacao = "Já existe uma obra cadastrada com este nome. <br>Altere o nome da obra e tente salvar novamente!";
                }
                else
                {
                    result = cDados.atualizaObra(codigoObra, codigoUsuarioResponsavel, nomeObra, previsaoPBA, referenciaPBA, codigoMunicipio, codigoSegmento
                    , numeroTermoCooperacao, numeroTermoAnuencia, numeroOficio, quantidadeObras, codigoTipoObra, indicaConstrucao, indicaReforma, indicaAmpliacao, dataInicio, dataTermino, observacoes, InicioRepactuado, TerminoRepactuado, ref msgStatusGravacao);

                    if (result)
                    {
                        msgStatusGravacao = "Obra alterada com sucesso!";
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
        DataSet ds = cDados.getInformacoesObra(codigoObra, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtNomeObra.Text = dr["NomeProjeto"].ToString();
            txtPrevistoPBA.Text = dr["PrevistoPBA"].ToString();
            txtReferenciaPBA.Text = dr["ItemAtendidoPBA"].ToString();
            ddlMunicipio.Value = dr["CodigoMunicipioObra"].ToString();
            ddlSegmento.Value = dr["CodigoSegmentoObra"].ToString();
            txtTermoCooperacao.Text = dr["NumeroTermoCooperacao"].ToString();
            txtTermoAnuencia.Text = dr["NumeroTermoAnuencia"].ToString();
            txtOficio.Text = dr["NumeroOficio"].ToString();
            txtQuantidadeObras.Text = dr["QuantidadeEntregaPrevistaPBA"].ToString();
            ddlTipoContratacao.Value = dr["CodigoTipoServico"].ToString();
            ckConstrucao.Checked = dr["IndicaConstrucao"].ToString() == "S";
            ckReforma.Checked = dr["IndicaReforma"].ToString() == "S";
            ckAmpliacao.Checked = dr["IndicaAmpliacao"].ToString() == "S";
            if (dr["InicioPrevistoObraPBA"].ToString() != "")
                ddlInicio.Date = DateTime.Parse(dr["InicioPrevistoObraPBA"].ToString());
            if (dr["TerminoPrevistoObraPBA"].ToString() != "")
                ddlTermino.Date = DateTime.Parse(dr["TerminoPrevistoObraPBA"].ToString());
            if (dr["InicioRepactuado"].ToString() != "")
                ddlInicioRepactuado.Date = DateTime.Parse(dr["InicioRepactuado"].ToString());
            if (dr["TerminoRepactuado"].ToString() != "")
                ddlTerminoRepactuado.Date = DateTime.Parse(dr["TerminoRepactuado"].ToString());
            txtObservacoes.Text = dr["ObservacaoObra"].ToString();
        }
    }
}