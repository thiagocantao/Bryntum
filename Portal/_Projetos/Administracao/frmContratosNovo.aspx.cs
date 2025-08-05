/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 
 * 01/03/2011 :: Alejandro : -Alteração no método 'carregaComboFontePagadora()', aonde agora indicara o código da
 *                          entidade logada para filtrar as fontes pagadoras correspondientes.
 *                          -Alteração do desenho da grid para obter o padron.
 * 
 * 17/03/2011 :: Alejandro : adiciono el botão de Permissãos para os contratos.
 * 21/03/2011 :: Alejandro : adiciono el control de acesso para o botão de permissões [CT_AdmPrs].
 */
using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_frmContratos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

    private int idProjeto = -1;

    private string captioCIA = "Número do CIA:";

    public string temAditivo = "N";

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

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        if (!IsPostBack && !IsCallback)
        {
            DataSet dsAux = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelNumeroInternoContrato");

            if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]))
                captioCIA = dsAux.Tables[0].Rows[0]["labelNumeroInternoContrato"].ToString() + ":";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        verificaSeUtilizaIntegracaoSENARDocs();

        if (Request.QueryString["ID"] != null)
            idProjeto = int.Parse(Request.QueryString["ID"].ToString());

        bool bPodeAcessarTela;
        /// se houver algum contrato que o usuário pode consultar
        bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "CT", "CT_Cns");

        /// se ainda não foi determinado que pode acessar, 
        /// verifica se há alguma unidade em que o usuário possa incluir contratos
        if (bPodeAcessarTela == false)
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "UN_IncCtt");

        /// se ainda não foi determinado que pode acessar, 
        /// verifica se há alguma unidade em que o usuário possa consultar contratos, mesmo que não exista nenhum
        if (bPodeAcessarTela == false)
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "CT_Cns");

        // se não puder, redireciona para a página sem acesso
        if (bPodeAcessarTela == false)
            RedirecionaParaTelaSemAcesso(this);

        hfGeral.Set("CodigoResponsavel", "");

        string where = string.Format(@" DataExclusao IS NULL AND CodigoUsuario IN(SELECT uun.CodigoUsuario FROM {0}.{1}.UsuarioUnidadeNegocio AS uun 
								                                  WHERE Uun.CodigoUnidadeNegocio = {2} AND uun.[IndicaUsuarioAtivoUnidadeNegocio] = 'S')", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel.ToString());
        // conteúdo usado na tela para listar os usuários
        hfGeral.Set("ComandoWhere", where);

        carregaGvDados();

        if (!IsPostBack)
        {
            if (Request.QueryString["DiasVencimento"] != null && Request.QueryString["DiasVencimento"].ToString() != "")
            {
                int diasVencimento = int.Parse(Request.QueryString["DiasVencimento"].ToString());
                DateTime dataVencimento = DateTime.Now.AddDays(diasVencimento);

                gvDados.FilterExpression = string.Format("[SituacaoContrato] = 'Ativo' AND [DataTermino] <= #{0:yyyy-MM-dd}# AND [DataTermino] >= #{1:yyyy-MM-dd}# AND [DataTermino] <> ''", dataVencimento, DateTime.Now);
                //gvDados.Columns[""].
            }

            if (Request.QueryString["Vencidos"] != null && Request.QueryString["Vencidos"].ToString() == "S")
            {
                DateTime dataVencimento = DateTime.Now;

                gvDados.FilterExpression = string.Format("[SituacaoContrato] = 'Ativo' AND [DataTermino] < #{0:yyyy-MM-dd}# AND [DataTermino] <> ''", dataVencimento);
                //gvDados.Columns[""].
            }

            if (Request.QueryString["ApenasMeusContratos"] != null && Request.QueryString["ApenasMeusContratos"].ToString() != "")
            {
                DataSet dsFiltro = cDados.getUsuarios(" AND u.CodigoUsuario = " + codigoUsuarioResponsavel);

                if (cDados.DataSetOk(dsFiltro) && cDados.DataTableOk(dsFiltro.Tables[0]))
                {
                    string nomeUsuario = dsFiltro.Tables[0].Rows[0]["NomeUsuario"].ToString();

                    if (gvDados.FilterExpression != "")
                        gvDados.FilterExpression += " AND [SituacaoContrato] = 'Ativo' AND  [NomeUsuario] = '" + nomeUsuario + "'";
                    else
                        gvDados.FilterExpression = " [SituacaoContrato] = 'Ativo' AND  [NomeUsuario] = '" + nomeUsuario + "'";
                }
            }

            tabControl.ActiveTabIndex = 0;

            DataSet ds1 = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloAbaLinksContratos");
            tabControl.TabPages[4].Text = (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0])) ? ds1.Tables[0].Rows[0][0].ToString() : "LINKS";
            lblNomeCIA.Text = captioCIA;

        }

        if (!IsPostBack)
        {
            carregaComboAquisicao(codigoEntidadeUsuarioResponsavel);
            carregaComboFontePagadora();
            carregaComboProjetos();
            carregaComboTipoContrato("");
            carregaComboStatus();
            //populaUsuarios();

        }

        carregaComboRazaoSocial();
        podeIncluir = carregaComboUnidadeGestora(string.Format(@" AND CodigoEntidade = {0} AND IndicaUnidadeNegocioAtiva = 'S' AND DataExclusao IS NULL", codigoEntidadeUsuarioResponsavel)) > 0;

        cDados.setaTamanhoMaximoMemo(mmObjeto, 500, lbl_mmObjeto);
        cDados.setaTamanhoMaximoMemo(mmObservacoes, 500, lbl_mmObservacoes);


        cDados.aplicaEstiloVisual(Page);
        lblAvisos.Font.Size = new FontUnit("11pt");
    }

    private void verificaSeUtilizaIntegracaoSENARDocs()
    {
        DataSet dsutilizaIntegracaoSenarDocs = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "utilizaIntegracaoSenarDocs");
        if (cDados.DataSetOk(dsutilizaIntegracaoSenarDocs) && cDados.DataTableOk(dsutilizaIntegracaoSenarDocs.Tables[0]))
        {
            TabPage paginaLinks = tabControl.TabPages.FindByName("tabLinks");
            paginaLinks.ClientVisible = dsutilizaIntegracaoSenarDocs.Tables[0].Rows[0][0].ToString().Trim().ToUpper() == "S";
        }
    }

    private void carregaComboStatus()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoStatusComplementarContrato]
              ,[DescricaoStatusComplementarContrato]
              ,[IndicaStatusContrato]
              ,[IniciaisStatusControladoSistema]
          FROM [StatusComplementarContrato]");

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlStatusComplementarContrato.DataSource = ds;
        ddlStatusComplementarContrato.TextField = "DescricaoStatusComplementarContrato";
        ddlStatusComplementarContrato.ValueField = "CodigoStatusComplementarContrato";
        ddlStatusComplementarContrato.DataBind();

    }

    public void RedirecionaParaTelaSemAcesso(System.Web.UI.Page page)
    {
        try
        {

            page.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
        }
        catch
        {
            page.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
            page.Response.End();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ContratosNovo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ContratosNovo", "frmContratosNovo"));

    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string strWhere = "";

        if (idProjeto != -1)
            strWhere = " AND proj.CodigoProjeto = " + idProjeto;
        DataSet ds = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, strWhere);

        configuraVisibilidadeColunasCamposGrid();

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void configuraVisibilidadeColunasCamposGrid()
    {
        bool mostraColunaRevisaoPrevia = false;
        DataSet dsRevisaoPrevia = cDados.getParametrosSistema("mostraRevisaoPreviaContratos");
        if (cDados.DataSetOk(dsRevisaoPrevia) && cDados.DataTableOk(dsRevisaoPrevia.Tables[0]))
        {
            mostraColunaRevisaoPrevia = dsRevisaoPrevia.Tables[0].Rows[0]["mostraRevisaoPreviaContratos"].ToString().Trim().ToUpper() == "S";
        }
        ((GridViewDataComboBoxColumn)gvDados.Columns["IndicaRevisaoPrevia"]).Visible = mostraColunaRevisaoPrevia;

        gvDados.JSProperties["cp_MostraColunaIndicaRevisaoPrevia"] = (mostraColunaRevisaoPrevia == true) ? "S" : "N";
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {

    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "DescricaoObjetoContrato")
        {
            if (e.CellValue.ToString().Length > 60)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 60) + "...";
            }
        }
    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_tipoOperacao"] = e.Parameter;


        if (e.Parameter == "Incluir")
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.frmContratosNovo_contrato_inclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteInclusaoRegistro();

        }
        if (e.Parameter == "Editar")
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.frmContratosNovo_contrato_atualizado_com_sucesso_;
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.frmContratosNovo_contrato_exclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }
        else // alguma coisa deu errado...
            ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
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
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";
        int novoCodigoContrato = -1;

        try
        {
            //recoleção de dados.
            string numeroContrato = txtNumeroContrato.Text;
            //string codigoProjeto = idProjeto.ToString();
            string codigoTipoContrato = ddlTipoContrato.Value != null ? ddlTipoContrato.Value.ToString() : "-1";
            string codigoTipoAquisicao = ddlModalidadAquisicao.Value != null ? ddlModalidadAquisicao.Value.ToString() : "NULL";


            string razaoSocial = (ddlRazaoSocial.Value == null) ? "NULL" : ddlRazaoSocial.Value.ToString();

            string descricaoObjetoContrato = mmObjeto.Text;
            string codigoFonteRecursosFinancieros = ddlFontePagadora.Value != null ? ddlFontePagadora.Value.ToString() : "NULL";
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "";
            DateTime dtInicio = ddlInicioDeVigencia.Date;
            DateTime dtTermino = ddlTerminoDeVigencia.Date;

            string statusContrato = ddlSituacao.Value != null ? ddlSituacao.Value.ToString() : "";
            string observacoes = mmObservacoes.Text;
            string numeroContratoSAP = txtSAP.Text;
            string codigoProjeto = ddlProjetos.Value != null ? (int.Parse(ddlProjetos.Value.ToString()) != 0 ? ddlProjetos.Value.ToString() : "NULL") : "NULL";
            string valorContrato = txtValorDoContrato.Text.Replace(".", "").Replace(",", ".");
            string tipoPessoa = (rbClienteFor.Value == null) ? "F" : rbClienteFor.Value.ToString();
            string indicaRevisaoPrevia = cbIndicaRevisaoPrevia.Value == null ? "N" : cbIndicaRevisaoPrevia.Value.ToString();
            string codigoStatusComplementarContrato = ddlStatusComplementarContrato.Value == null ? "NULL" : ddlStatusComplementarContrato.Value.ToString();

            //Insere a Unidade Negocio com NivelHierarquico como 0 e EstruturaHierarquica com 0 pois será feito um "scope_identity()" para poder montar o Nivel e a Estrutura
            cDados.incluirContratoAquisicoes(codigoEntidadeUsuarioResponsavel, numeroContrato, codigoProjeto, codigoTipoAquisicao
                                               , "NULL", descricaoObjetoContrato, codigoFonteRecursosFinancieros
                                               , codigoUnidadeNegocio, dtInicio, dtTermino
                                               , ddlResponsavel.Value.ToString(), codigoUsuarioResponsavel.ToString(), statusContrato
                                               , observacoes, numeroContratoSAP, codigoTipoContrato
                                               , valorContrato, tipoPessoa, razaoSocial, indicaRevisaoPrevia, codigoStatusComplementarContrato, ref novoCodigoContrato);

            hfGeral.Set("CodigoContrato", novoCodigoContrato.ToString());
            pnCallback.JSProperties["cp_NovoCodigoContrato"] = novoCodigoContrato;
            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoContrato);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = Resources.traducao.frmContratosNovo_houve_um_erro_ao_salvar_o_registro__entre_em_contato_com_o_administrador_do_sistema__nmensagem_servidor__n + ex.Message;
        }
        return msg;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";

        try
        {
            //recoleção de dados.
            string codigoContratoAquisicao = getChavePrimaria();
            string numeroContrato = txtNumeroContrato.Text;
            //string codigoProjeto = idProjeto.ToString();
            string codigoTipoContrato = ddlTipoContrato.Value != null ? ddlTipoContrato.Value.ToString() : "-1";
            string codigoTipoAquisicao = ddlModalidadAquisicao.Value != null ? ddlModalidadAquisicao.Value.ToString() : "NULL";

            string razaoSocial = (ddlRazaoSocial.Value == null) ? "NULL" : ddlRazaoSocial.Value.ToString();

            string tipoPessoa = (rbClienteFor.Value == null) ? "F" : rbClienteFor.Value.ToString();

            string descricaoObjetoContrato = mmObjeto.Text;
            string codigoFonteRecursosFinancieros = ddlFontePagadora.Value != null ? ddlFontePagadora.Value.ToString() : "NULL";
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "";
            DateTime dtInicio = ddlInicioDeVigencia.Date;
            DateTime dtTermino = ddlTerminoDeVigencia.Date;
            string codigoUsuarioResponsavel = ddlResponsavel.Value.ToString();
            string codigoUsuarioAlteracao = codigoUsuarioResponsavel.ToString();
            string statusContrato = ddlSituacao.Value != null ? ddlSituacao.Value.ToString() : "";
            string observacoes = mmObservacoes.Text;
            string numeroContratoSAP = txtSAP.Text;
            string sqlParcelas = "";
            string codigoProjeto = ddlProjetos.Value != null ? (int.Parse(ddlProjetos.Value.ToString()) != 0 ? ddlProjetos.Value.ToString() : "NULL") : "NULL";
            string valorContrato = txtValorDoContrato.Text.Replace(".", "").Replace(",", ".");
            string indicaRevisaoPrevia = cbIndicaRevisaoPrevia.Value == null ? "N" : cbIndicaRevisaoPrevia.Value.ToString();
            string codigoStatusComplementarContrato = ddlStatusComplementarContrato.Value == null ? "NULL" : ddlStatusComplementarContrato.Value.ToString();








            cDados.atualizaContratoAquisicoes(numeroContrato, codigoTipoAquisicao, "NULL", descricaoObjetoContrato
                                                , codigoFonteRecursosFinancieros, codigoUnidadeNegocio, dtInicio, dtTermino
                                                , codigoUsuarioResponsavel, codigoUsuarioAlteracao, statusContrato
                                                , codigoContratoAquisicao, observacoes, numeroContratoSAP, codigoProjeto
                                                , codigoTipoContrato, valorContrato, sqlParcelas, tipoPessoa, razaoSocial, indicaRevisaoPrevia, codigoStatusComplementarContrato);

            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoContratoAquisicao);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = Resources.traducao.frmContratosNovo_houve_um_erro_ao_salvar_o_registro__entre_em_contato_com_o_administrador_do_sistema__nmensagem_servidor__n + ex.Message;
        }
        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        string msg = "";
        string chave = getChavePrimaria();
        try
        {
            cDados.excluiContratoAquisicoes(chave, cDados.getCodigoTipoAssociacao("CT"), ref msg);
            carregaGvDados();
        }
        catch
        {
            gvDados.ClientVisible = false;
            msg = Resources.traducao.frmContratosNovo_houve_um_erro_ao_excluir_o_registro__entre_em_contato_com_o_administrador_do_sistema_;
        }

        return msg;
    }

    #endregion

    #region COMBOBOX'S

    private void carregaComboAquisicao(int idEntidadLogada)
    {
        DataSet ds = cDados.getTiposAquisicao(codigoEntidadeUsuarioResponsavel.ToString());

        if (cDados.DataSetOk(ds))
        {
            ddlModalidadAquisicao.DataSource = ds.Tables[0];
            ddlModalidadAquisicao.DataBind();

            if (!IsPostBack && ddlModalidadAquisicao.Items.Count > 0)
                ddlModalidadAquisicao.SelectedIndex = 0;
        }

        ListEditItem itemModalidadeAquisicao = new ListEditItem("", "NULL");
        ddlModalidadAquisicao.Items.Insert(0, itemModalidadeAquisicao);
    }

    private int carregaComboUnidadeGestora(string where)
    {
        int qtdUnidades = 0;
        where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({3}, {2}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncCtt')=1
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel);
        DataSet ds = cDados.getUnidade(where);

        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeGestora.DataSource = ds.Tables[0];
            ddlUnidadeGestora.DataBind();
        }

        qtdUnidades = ddlUnidadeGestora.Items.Count;

        //saver quantas unidades tem. So para saver si tem 1, colocar como opção default.
        hfGeral.Set("CantUnidadesGestora", qtdUnidades);

        if (!IsPostBack && ddlUnidadeGestora.Items.Count > 0)
        {
            if (qtdUnidades == 1)
                ddlUnidadeGestora.SelectedIndex = 1;
            else
                ddlUnidadeGestora.SelectedIndex = 0;
        }

        return qtdUnidades;
    }

    /// <summary>
    /// filtro where, saver o alias das tabelas:: FROM {0}.{1}.FonteRecursosFinanceiros AS frf
    /// </summary>
    private void carregaComboFontePagadora()
    {
        string where = "where frf.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;
        DataSet ds = cDados.getFontePagadora(where);

        if (cDados.DataSetOk(ds))
        {
            ddlFontePagadora.DataSource = ds.Tables[0];
            ddlFontePagadora.DataBind();

            if (!IsPostBack && ddlFontePagadora.Items.Count > 0)
                ddlFontePagadora.SelectedIndex = 0;
        }
        ListEditItem itemFontePagadora = new ListEditItem("", "NULL");
        ddlFontePagadora.Items.Insert(0, itemFontePagadora);

    }

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND Projeto.CodigoEntidade = {0} ORDER BY Projeto.NomeProjeto", codigoEntidadeUsuarioResponsavel);

        if (idProjeto != -1)
            where = " AND Projeto.CodigoProjeto = " + idProjeto + where;

        DataSet ds = cDados.getProjetosContratos(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

        }

        if (idProjeto == -1)
        {
            ListEditItem sinProjeto = new ListEditItem(Resources.traducao.nenhum, "0");
            ddlProjetos.Items.Insert(0, sinProjeto);
        }

        if (!IsPostBack && ddlProjetos.Items.Count > 0)
            ddlProjetos.SelectedIndex = 0;
    }

    //todo: Pendiente la consulta do combo TipoContrato.
    private void carregaComboTipoContrato(string where)
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds))
        {
            ddlTipoContrato.DataSource = ds.Tables[0];
            ddlTipoContrato.DataBind();
        }

        ListEditItem sinTipoContrato = new ListEditItem(Resources.traducao.frmContratosNovo_selecionar___, "0");
        ddlTipoContrato.Items.Insert(0, sinTipoContrato);

        if (!IsPostBack && ddlTipoContrato.Items.Count > 0)
            ddlTipoContrato.SelectedIndex = 0;
    }

    protected void populaUsuarios()
    {

        //cDados = new dados();
        string where1 = "";

        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, where1);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlResponsavel.TextField = "NomeUsuario";
            ddlResponsavel.ValueField = "CodigoUsuario";
            ddlResponsavel.DataSource = ds.Tables[0];
            ddlResponsavel.DataBind();
        }
    }

    private void carregaComboRazaoSocial()
    {

        string sWhere = " AND pe.IndicaFornecedor = 'X'  AND pe.IndicaCliente = 'X' ";

        if (rbClienteFor.Value != null)
        {
            if (rbClienteFor.Value.ToString() == "F")
            {
                sWhere = " AND pe.IndicaFornecedor = 'S' ";
            }
            else if (rbClienteFor.Value.ToString() == "C")
            {
                sWhere = " AND pe.IndicaCliente = 'S' ";
            }
            else if (rbClienteFor.Value.ToString() == "D")
            {
                sWhere = " AND pe.IndicaParticipe = 'S' ";
            }
        }

        sWhere = (rbClienteFor.Value != null) ? rbClienteFor.Value.ToString() == "F" ? " AND pe.IndicaFornecedor = 'S' " : " AND pe.IndicaCliente = 'S' " : " AND pe.IndicaFornecedor = 'S' ";
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, sWhere);

        ddlRazaoSocial.TextField = "NomePessoa";
        ddlRazaoSocial.ValueField = "CodigoPessoa";
        ddlRazaoSocial.DataSource = ds;
        ddlRazaoSocial.DataBind();
    }

    #endregion

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_AfterPerformCallback1(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "CtrPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClick_NovoContrato();", true, true, false, "CtrPrj", "Contratos do Projeto", this);
    }

    #endregion

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }
    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }

    protected void pnPagoAcumulado_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoContrato = e.Parameter;

        string comandoSQL = string.Format(@" SELECT ISNULL(SUM(isnull(ValorPago,0)),0) as valorPagoAcumulado
                                               FROM ParcelaContrato 
                                              WHERE CodigoContrato = {0} AND [DataExclusao] IS NULL
                                                AND DataPagamento IS NOT NULL ", codigoContrato);
        DataSet ds = cDados.getDataSet(comandoSQL);
        spnPagoAcumulado.Value = 0;
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            spnPagoAcumulado.Value = (decimal)ds.Tables[0].Rows[0]["valorPagoAcumulado"];
        }
    }

    protected void pnPrevistoAcumulado_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoContrato = e.Parameter;

        string comandoSQL = string.Format(@" SELECT isnull(SUM(isnull(ValorPrevisto,0)),0) as ValorPrevistoAcumulado
                                               FROM ParcelaContrato 
                                              WHERE CodigoContrato = {0} AND [DataExclusao] IS NULL
                                                AND DataPagamento is NULL ", codigoContrato);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            spnPrevistoAcumulado.Value = (decimal)ds.Tables[0].Rows[0]["ValorPrevistoAcumulado"];
        }

    }

    protected void pnSaldo_Callback(object sender, CallbackEventArgsBase e)
    {
        /*
         Saldo: Valor total - valores pagos - valores previstos sem pagamento associado 
         */
        string codigoContrato = e.Parameter;
        int codigoContratoInt = -1;

        bool r = int.TryParse(codigoContrato, out codigoContratoInt);


        ((ASPxCallbackPanel)sender).JSProperties["cpAviso"] = "";
        string comandoSQL = string.Format(@" SELECT CodigoContrato, SaldoContrato, IndicaDadoIntegracaoAtualizado, DataUltimaIntegracao,
                                                    ValorPagoAcumulado, ValorPrevistoAcumulado, ValorComAditivo
                                            FROM f_GetSaldoContrato({0}) ", codigoContratoInt);

        DataSet ds = cDados.getDataSet(comandoSQL);
        spnSaldo.Value = 0;
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string indicaDadoIntegracaoAtualizado = (string)ds.Tables[0].Rows[0]["IndicaDadoIntegracaoAtualizado"].ToString().ToUpper().Trim();
            string dataUltimaIntegracao = (string)ds.Tables[0].Rows[0]["DataUltimaIntegracao"].ToString().Trim();

            spnSaldo.Value = (decimal)ds.Tables[0].Rows[0]["SaldoContrato"];
            spnPagoAcumulado.Value = (decimal)ds.Tables[0].Rows[0]["ValorPagoAcumulado"];
            spnPrevistoAcumulado.Value = (decimal)ds.Tables[0].Rows[0]["ValorPrevistoAcumulado"];
            spnValorComAditivo.Value = ds.Tables[0].Rows[0]["ValorComAditivo"] == DBNull.Value ? spnValorComAditivo.Value = "" : spnValorComAditivo.Value = (decimal)ds.Tables[0].Rows[0]["ValorComAditivo"];
            txtValorDoContrato.Enabled = false;

            if ((decimal)ds.Tables[0].Rows[0]["SaldoContrato"] < 0)
            {
                ((ASPxCallbackPanel)sender).JSProperties["cpAviso"] = Resources.traducao.frmContratosNovo____os_valores_envolvidos_ir_o_extrapolar_o_valor_do_contrato_;
            }
            imgStatusSincronizacao.ClientVisible = (indicaDadoIntegracaoAtualizado == "N");
            if (imgStatusSincronizacao.ClientVisible)
            {
                imgStatusSincronizacao.ImageUrl = "~/imagens/botoes/caution.png";
                if (!string.IsNullOrEmpty(dataUltimaIntegracao))
                {
                    DateTime dtAtualizacao = DateTime.Parse(dataUltimaIntegracao);
                    string dataformatada = dtAtualizacao.ToString("dd/MM/yyyy HH:mm");
                    imgStatusSincronizacao.ToolTip = string.Format(@"A última sincronização dos valores Pago, Previsto e de Saldo foi em {0}.", dataformatada);
                }
                else
                {
                    imgStatusSincronizacao.ToolTip = string.Format(@"Atenção! Não foi possível determinar quando que os valores Pago, Previsto e do Saldo foram sincronizados.");
                }
            }
        }
    }

    protected void cbAvisos_Callback(object sender, CallbackEventArgsBase e)
    {
        lblAvisos.Text = e.Parameter;
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }


    protected void pnValorComAditivo_Callback(object sender, CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"SELECT ValorContrato FROM contrato 
				                             WHERE codigocontrato = {0}", e.Parameter);

        DataSet ds1 = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            spnValorComAditivo.Value = ds1.Tables[0].Rows[0]["ValorContrato"];
        }
    }
}
