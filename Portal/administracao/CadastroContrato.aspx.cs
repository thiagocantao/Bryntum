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
    private bool chamadaPopUp = false;
    private int codigoContrato = -1;
    private int codigoProjeto = -1;
    bool readOnly = false;
    public string mostrarBotaoCancelar = "", tamanhoTable = "100%";

    public string bPodeAlterarNumero = "N";

    string numeracaoAutomatica = "S";
    public string vlabelNumeroInterno2 = "";
    public string vlabelNumeroInterno3 = "";

    public string mostraNumeroInterno2 = " display:block";
    public string mostraNumeroInterno3 = " display:block";
    string fonteContratacao = "";

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

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelNumeroInterno2");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            vlabelNumeroInterno2 = dsParametros.Tables[0].Rows[0]["labelNumeroInterno2"].ToString();
        }
        dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelNumeroInterno3");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            vlabelNumeroInterno3 = dsParametros.Tables[0].Rows[0]["labelNumeroInterno3"].ToString();
        }


        if(vlabelNumeroInterno2 == "")
            mostraNumeroInterno2 = " display:none";

        if (vlabelNumeroInterno3 == "")
            mostraNumeroInterno3 = " display:none";

        ASPxLabelNumeroInterno2.Text = vlabelNumeroInterno2 + ":";
        ASPxLabelNumeroInterno3.Text = vlabelNumeroInterno3 + ":";

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        ddlRazaoSocial.JSProperties["cp_URLRazao"] = cDados.getPathSistema() + "_Projetos/Administracao/frmCadastroPessoa.aspx";

        chamadaPopUp = Request.QueryString["ChP"] != null && Request.QueryString["ChP"].ToString() == "S";

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            desabilitaComponentes();
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            try
            {
                codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

                int codigoTipoProjeto = cDados.getCodigoTipoProjeto(codigoProjeto);

                switch (codigoTipoProjeto)
                {
                    case 1:
                        fonteContratacao = "PBA";
                        break;
                    case 8:
                        fonteContratacao = "Plano de Segurança";
                        break;
                    case 9:
                        fonteContratacao = "PDRS Xingu";
                        break;
                    case 11:
                        fonteContratacao = "PBA - Indígena";
                        break;
                    case 12:
                        fonteContratacao = "Outro";
                        break;
                }

                codigoContrato = cDados.getCodigoContrato(codigoProjeto);
                    
                string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigoProjeto, "./../");
                //link so está VISÍVEL se o projeto foi realmente salvo e se não houver ainda nenhum cronograma associado AO PROJETO                
                linkEditarCronograma.NavigateUrl = linkOpcao;                

            }
            catch { }
        }
        if (Request.QueryString["Modo"] != null && Request.QueryString["Modo"].Equals("Consulta"))
        {
            desabilitaComponentes();
            link.Visible = false;
            readOnly = true;
        }

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "numeracaoAutomaticaContratos");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            numeracaoAutomatica = dsParametros.Tables[0].Rows[0]["numeracaoAutomaticaContratos"].ToString();

        txtNumeroContrato.JSProperties["cp_NumeracaoAutomatica"] = numeracaoAutomatica;

        if (chamadaPopUp)
        {
            btnCancelar.ClientSideEvents.Click = @"function (s, e) {window.top.fechaModal();}";
        }
        else
        {
            mostrarBotaoCancelar = "display: none;";
            tamanhoTable = "100%";
        }

        carregaComboRazaoSocial();
        carregaComboCriterioReajuste();
        carregaComboCriterioMedicao();
        carregaComboGestorContrato();

        if (codigoContrato != -1)
        {
            if (!IsPostBack && !IsCallback)
                carregaDadosContrato();
        }

        cDados.aplicaEstiloVisual(Page);
        linkEditarCronograma.ClientVisible = (codigoProjeto != -1);


        if (codigoContrato != -1)
        {
            pcDados.TabPages[1].ClientVisible = true;
            pcDados.TabPages[2].ClientVisible = true;
            renderizaAnexos();
            renderizaComentarios();
        }
        else
        {
            pcDados.TabPages[1].ClientVisible = false;
            pcDados.TabPages[2].ClientVisible = false;
            ddlTerminoDeVigencia.ClientEnabled = false;
            //ddlAssinatura.ClientEnabled = false;
            
            if (numeracaoAutomatica == "S")
                txtNumeroContrato.ClientEnabled = false;


            callbackSalvar.JSProperties["cp_MsgStatus"] = "";
            callbackSalvar.JSProperties["cp_Status"] = "";
        }        
    }

    private void desabilitaComponentes()
    {

        txtNumeroContrato.ClientEnabled = false;
        ddlRazaoSocial.ClientEnabled = false;
        mmObjeto.ClientEnabled = false;
        ddlInicioDeVigencia.ClientEnabled = false;
        ddlTerminoDeVigencia.ClientEnabled = false;
        ddlAssinatura.ClientEnabled = false;
        txtValorDoContrato.ClientEnabled = false;
        ddlDataBase.ClientEnabled = false;
        ddlCriterioReajuste.ClientEnabled = false;
        ddlCriterioMedicao.ClientEnabled = false;
        ddlGestorContrato.ClientEnabled = false;
        txtNumeroTrabalhadores.ClientEnabled = false;
        mmObservacoes.ClientEnabled = false;
        txtnumeroInterno2.ClientEnabled = false;
        txtnumeroInterno3.ClientEnabled = false;
        txtVigencia.ClientEnabled = false;
        linkEditarCronograma.ClientEnabled = false;
        ddlUnidadeVigencia.ClientEnabled = false;
        btnSalvar.ClientVisible = false;
    }

    #region Combos

    private void carregaComboRazaoSocial()
    {
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, " AND pe.IndicaFornecedor = 'S'");

        ddlRazaoSocial.TextField = "NomePessoa";
        ddlRazaoSocial.ValueField = "CodigoPessoa";
        ddlRazaoSocial.DataSource = ds;
        ddlRazaoSocial.DataBind();
    }

    private void carregaComboCriterioReajuste()
    {
        DataSet ds = cDados.getTipoCriterioReajusteContrato(codigoEntidadeUsuarioResponsavel, "");

        ddlCriterioReajuste.TextField = "DescricaoCriterioReajusteContrato";
        ddlCriterioReajuste.ValueField = "CodigoCriterioReajusteContrato";
        ddlCriterioReajuste.DataSource = ds;
        ddlCriterioReajuste.DataBind();
    }

    private void carregaComboCriterioMedicao()
    {
        DataSet ds = cDados.getTipoCriterioMedicaoContrato(codigoEntidadeUsuarioResponsavel, "");

        ddlCriterioMedicao.TextField = "DescricaoCriterioMedicaoContrato";
        ddlCriterioMedicao.ValueField = "CodigoCriterioMedicaoContrato";
        ddlCriterioMedicao.DataSource = ds;
        ddlCriterioMedicao.DataBind();
    }

    private void carregaComboGestorContrato()
    {
        string nome = "";
        string valor = "";

        string where = string.Format(@" AND CodigoUsuario IN(SELECT us.CodigoUsuario FROM {0}.{1}.Usuario AS us 
											                 INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun 
														             ON (uun.CodigoUsuario = us.CodigoUsuario)
								                                  WHERE Uun.CodigoUnidadeNegocio = {2}  AND us.DataExclusao IS NULL)", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);
        
        ddlGestorContrato.DataSource = cDados.getLovNomeValor2("Usuario", "CodigoUsuario", "NomeUsuario", "EMail", "", true, where, "NomeUsuario", out valor, out nome);
        ddlGestorContrato.Columns[0].FieldName = "ColunaNome";
        ddlGestorContrato.Columns[1].FieldName = "ColunaEmail";
        ddlGestorContrato.TextFormatString = "{0}";
        ddlGestorContrato.DataBind();
    }

    #endregion

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgStatusGravacao = "";
        bool result = false;
        string statusGravacao = "0";
        callbackSalvar.JSProperties["cp_URL"] = "";
        string numeroContrato = txtNumeroContrato.Text;
        int razaoSocial = int.Parse(ddlRazaoSocial.Value.ToString());
        string objeto = mmObjeto.Text;
        string inicio = "";//(ddlInicioDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioDeVigencia.Date);
        string termino = "";// (ddlTerminoDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoDeVigencia.Date);
        string assinatura = (ddlAssinatura.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlAssinatura.Date);
        string valor = txtValorDoContrato.Text;
        string dataBase = (ddlDataBase.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataBase.Date);
        int criterioReajuste = int.Parse(ddlCriterioReajuste.Value.ToString());
        int criterioMedicao = int.Parse(ddlCriterioMedicao.Value.ToString());
        int gestor = int.Parse(ddlGestorContrato.Value.ToString());
        string numeroTrabalhadores = txtNumeroTrabalhadores.Text;
        string observacoes = mmObservacoes.Text;
        string numeroInterno2 = txtnumeroInterno2.Text;
        string numeroInterno3 = txtnumeroInterno3.Text;
        callbackSalvar.JSProperties["cp_URL"] = "";
        string vigencia = txtVigencia.Value + "";
        string unidadeVigenciaContrato = ddlUnidadeVigencia.Value.ToString();

        if (codigoContrato == -1)
        {

            DataSet dsUnidadePadrao = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "CodigoUnidadePadraoObra");

            int codigounidadePadrao = codigoEntidadeUsuarioResponsavel;

            if (cDados.DataSetOk(dsUnidadePadrao) && cDados.DataTableOk(dsUnidadePadrao.Tables[0]) && dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString() != "")

            {
                codigounidadePadrao = int.Parse(dsUnidadePadrao.Tables[0].Rows[0]["CodigoUnidadePadraoObra"].ToString());
            }
            if (numeracaoAutomatica == "N")
            {
                if (!cDados.validaFormatoNumeroContrato(numeroContrato, ""))
                {
                    msgStatusGravacao = "O número do contrato não está no formato válido (XX-X-000/0000).\nAltere o número do contrato e tente incluir novamente!";
                    statusGravacao = "-1";
                }

                if (statusGravacao != "-1" && cDados.verificaExistenciaContrato(numeroContrato, codigoContrato, codigounidadePadrao, ""))
                {
                    msgStatusGravacao = "Já existe um contrato cadastrado com este número. \nAltere o número do contrato e tente incluir novamente!";
                    statusGravacao = "-1";
                }
            }

            if (statusGravacao != "-1")
            {


                result = cDados.incluirContratoObra(numeroContrato, razaoSocial, objeto, inicio, termino, valor, dataBase, criterioReajuste, criterioMedicao
                    , gestor, numeroTrabalhadores, observacoes, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, codigounidadePadrao, assinatura, ref msgStatusGravacao,
                    numeroInterno2, numeroInterno3, vigencia, fonteContratacao, unidadeVigenciaContrato);

                if (result)
                {
                    msgStatusGravacao = "Contrato incluído com sucesso!";
                    statusGravacao = "1";
                    btnSalvar.ClientEnabled = false;

                }

                //Response.RedirectLocation = cDados.getPathSistema() + "administracao/CadastroContrato.aspx?" + Request.QueryString.ToString();
                callbackSalvar.JSProperties["cp_URL"] = cDados.getPathSistema() + "administracao/CadastroContrato.aspx?" + Request.QueryString.ToString();
            }
        }
        else
        {
            if (!cDados.validaFormatoNumeroContrato(numeroContrato, ""))
            {
                msgStatusGravacao = "O número do contrato não está no formato válido (XX-X-000/0000).\nAltere o número do contrato e tente incluir novamente!";
                statusGravacao = "-1";
            }
            if (statusGravacao != "-1" && cDados.validaSequenciaContrato(numeroContrato, "", codigoEntidadeUsuarioResponsavel) && numeracaoAutomatica == "S")
            {
                msgStatusGravacao = "Você não pode informar um sequencial de contrato maior que o controlado pelo sistema. \nAltere o número do contrato e tente incluir novamente!";
                statusGravacao = "-1";
            }

            if (statusGravacao != "-1" && cDados.verificaExistenciaContrato(numeroContrato, codigoContrato, codigoEntidadeUsuarioResponsavel, ""))
            {
                msgStatusGravacao = "Já existe um contrato cadastrado com este número. \nAltere o número do contrato e tente incluir novamente!";
                statusGravacao = "-1";
            }

            if (statusGravacao != "-1")
            {
                result = cDados.atualizaContratoObra(codigoContrato, numeroContrato, razaoSocial, objeto, inicio, termino, valor, dataBase, criterioReajuste, criterioMedicao
                    , gestor, numeroTrabalhadores, observacoes, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, assinatura, ref msgStatusGravacao,
                    numeroInterno2, numeroInterno3, vigencia, unidadeVigenciaContrato);

                if (result)
                {
                    msgStatusGravacao = "Contrato alterado com sucesso!";
                    statusGravacao = "1";
              }
            }
        }

        callbackSalvar.JSProperties["cp_MsgStatus"] = msgStatusGravacao;
        callbackSalvar.JSProperties["cp_Status"] = statusGravacao;
    }

    private void carregaDadosContrato()
    {
        DataSet ds = cDados.getContratosExtendidos(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " AND cont.CodigoContrato = " + codigoContrato);
        bPodeAlterarNumero = "N";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtNumeroContrato.Text = dr["NumeroContrato"].ToString();
            bPodeAlterarNumero = dr["PodeAlterarNumero"].ToString() == "True" || dr["PodeAlterarNumero"].ToString() == "1" ? "S" : "N";

            txtnumeroInterno2.Text = dr["NumeroInterno2"].ToString();
            txtnumeroInterno3.Text = dr["NumeroInterno3"].ToString();
            ddlRazaoSocial.Value = dr["CodigoPessoaContratada"].ToString() != "" ? dr["CodigoPessoaContratada"].ToString() : null;
            mmObjeto.Text = dr["DescricaoObjetoContrato"].ToString();
            if (dr["DataInicio"].ToString() != "")
                ddlInicioDeVigencia.Date = DateTime.Parse(dr["DataInicio"].ToString());
            if (dr["DataTermino"].ToString() != "")
                ddlTerminoDeVigencia.Date = DateTime.Parse(dr["DataTermino"].ToString());
            if (dr["DataAssinatura"].ToString() != "")
                ddlAssinatura.Date = DateTime.Parse(dr["DataAssinatura"].ToString());
            txtValorDoContrato.Text = dr["ValorContrato"].ToString();
            if (dr["DataBaseReajuste"].ToString() != "")
                ddlDataBase.Date = DateTime.Parse(dr["DataBaseReajuste"].ToString());
            ddlCriterioReajuste.Value = dr["CodigoCriterioReajusteContrato"].ToString() != "" ? dr["CodigoCriterioReajusteContrato"].ToString() : null;
            ddlCriterioMedicao.Value = dr["CodigoCriterioMedicaoContrato"].ToString() != "" ? dr["CodigoCriterioMedicaoContrato"].ToString() : null;

            if (dr["CodigoUsuarioResponsavel"].ToString() != "")
            {
                ddlGestorContrato.Text = dr["GestorContrato"].ToString();
                ddlGestorContrato.Value = int.Parse(dr["CodigoUsuarioResponsavel"].ToString());
                ddlGestorContrato.JSProperties["cp_ddlGestorContrato"] = dr["GestorContrato"].ToString();                
            }

            txtGestorContratada.Text = dr["NomeContato"].ToString();
            txtNumeroTrabalhadores.Text = dr["NumeroTrabalhadoresDiretos"].ToString();
            mmObservacoes.Text = dr["Observacao"].ToString();
            
            txtNumeroContrato.ClientEnabled = (bPodeAlterarNumero == "S" && readOnly == false);

            txtVigencia.Value = dr["VigenciaContrato"];

            if (dr["StatusContrato"].ToString() == "A" && readOnly == false)
            {
                ddlTerminoDeVigencia.ClientEnabled = true;                
            }
            else
            {
                ddlTerminoDeVigencia.ClientEnabled = false;
            }
            ddlUnidadeVigencia.Value = dr["UnidadeVigenciaContrato"].ToString();
        }
    }

    protected void callbackFornecedor_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (ddlRazaoSocial.SelectedIndex != -1)
        {
            DataSet ds = cDados.getDadosPessoa(int.Parse(ddlRazaoSocial.Value.ToString()), "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                callbackFornecedor.JSProperties["cp_contatoFornecedor"] = ds.Tables[0].Rows[0]["NomeContato"].ToString();
            }
            else
            {
                callbackFornecedor.JSProperties["cp_contatoFornecedor"] = "";
            }
        }
    }

    protected void ddlRazaoSocial_Callback(object sender, CallbackEventArgsBase e)
    {
        ddlRazaoSocial.JSProperties["cp_NovoValor"] = "-1";

        if (e.Parameter + "" != "")
        {
            ddlRazaoSocial.JSProperties["cp_NovoValor"] = e.Parameter.ToString();
        }
    }

    private void renderizaAnexos()
    {
        string urlAnexo = cDados.getPathSistema() + "espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=CT&ID=" + codigoContrato + "&ALT=" + 265 + (readOnly ? "&RO=S" : "");
        string frmAnexo = string.Format(@"<iframe frameborder=""0"" style=""width:100%; height:{0}px"" id=""frmAnexo"" name=""frmAnexo"" src=""{1}""></iframe>", 300, urlAnexo);

        ASPxPanel pnAnexo = new ASPxPanel();
        pnAnexo.EncodeHtml = false;
        pnAnexo.Width = new Unit("100%");

        Literal myLiteral = new Literal();
        myLiteral.Text = frmAnexo;

        pnAnexo.Controls.Add(myLiteral);

        pcDados.TabPages[1].Controls.Add(pnAnexo);
    }

    private void renderizaComentarios()
    {
        string urlComentarios = cDados.getPathSistema() + "_Projetos/Administracao/frmComentariosContrato.aspx?CC=" + codigoContrato + "&TO=E&ALT=" + 300 + (readOnly ? "&RO=S" : "");
        string frmComentarios = string.Format(@"<iframe frameborder=""0"" style=""width:100%; height:{0}px"" id=""frComentarios"" name=""frComentarios"" src=""{1}""></iframe>", 300, urlComentarios);

        ASPxPanel pnComentarios = new ASPxPanel();
        pnComentarios.EncodeHtml = false;
        pnComentarios.Width = new Unit("100%");

        Literal myLiteral = new Literal();
        myLiteral.Text = frmComentarios;

        pnComentarios.Controls.Add(myLiteral);

        pcDados.TabPages[2].Controls.Add(pnComentarios);
    }
}