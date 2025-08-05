using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class workflow_Tramitacao : System.Web.UI.Page
{
    dados cDados;
    
    private int codigoWF = -1;
    private int codigoInstancia = -1;
    private int codigoEtapa = -1;
    private int codigoOcorrencia = -1;
    private int codigoUsuarioLogado;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    private string readOnly = "N";

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

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        HeaderOnTela();

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWF = int.Parse(Request.QueryString["CWF"].ToString());

        if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "")
            codigoInstancia = int.Parse(Request.QueryString["CIWF"].ToString());

        if (Request.QueryString["CEWF"] != null && Request.QueryString["CEWF"].ToString() != "")
            codigoEtapa = int.Parse(Request.QueryString["CEWF"].ToString());

        if (Request.QueryString["CSOWF"] != null && Request.QueryString["CSOWF"].ToString() != "")
            codigoOcorrencia = int.Parse(Request.QueryString["CSOWF"].ToString());

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() != "")
            readOnly = Request.QueryString["RO"].ToString();


        if (!IsPostBack)
            hfGeral.Set("CIWF", codigoInstancia);
        else
            codigoInstancia = int.Parse(hfGeral.Get("CIWF").ToString());

        carregaComboForms();
        carregaGrid();
        carregaGvFormulariosBloqueados();

        if (gvHistorico.IsCallback)
            carregaGridHistorico();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        cDados.setaTamanhoMaximoMemo(mmObjeto, 2000, lbl_mmObjeto);

        cDados.aplicaEstiloVisual(this);

        gvFormulariosBloqueados.Settings.ShowFilterRow = false;
        gvFormulariosBloqueados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvHistorico.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        //ddlPrazo.MinDate = DateTime.Now;
        setaTextoPadrao();
    }

    private void setaTextoPadrao()
    {
        string textoCorpo = cDados.getTextoPadraoEntidade(codigoEntidadeUsuarioResponsavel, "CorpoSlcTrmFrmEtpFlx");
        string textoAssunto = cDados.getTextoPadraoEntidade(codigoEntidadeUsuarioResponsavel, "AssSlcTrmFrmEtpFlx");

        string nomeSistema = "", linkSistema = "", nomeUsuarioLogado = "", nomeFluxo = "", nomeEtapa = "";
                
        if (cDados.getInfoSistema("NomeUsuarioLogado") != null)
            nomeUsuarioLogado = cDados.getInfoSistema("NomeUsuarioLogado").ToString();

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloPaginasWEB", "urlAplicacao_AcessoInternet");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            nomeSistema = dsParametros.Tables[0].Rows[0]["tituloPaginasWEB"].ToString();
            linkSistema = dsParametros.Tables[0].Rows[0]["urlAplicacao_AcessoInternet"].ToString();
        }

        getInformacoesEtapa(ref nomeFluxo, ref nomeEtapa);

        textoAssunto = textoAssunto.Replace("[nomeUsuario]", nomeUsuarioLogado);
        textoAssunto = textoAssunto.Replace("[nomeSistema]", nomeSistema);
        textoAssunto = textoAssunto.Replace("[linkSistema]", linkSistema);
        textoAssunto = textoAssunto.Replace("[nomeDoFluxo]", nomeFluxo);
        textoAssunto = textoAssunto.Replace("[nomeDaEtapa]", nomeEtapa);

        textoCorpo = textoCorpo.Replace("[nomeUsuario]", nomeUsuarioLogado);
        textoCorpo = textoCorpo.Replace("[nomeSistema]", nomeSistema);
        textoCorpo = textoCorpo.Replace("[linkSistema]", linkSistema);
        textoCorpo = textoCorpo.Replace("[nomeDoFluxo]", nomeFluxo);
        textoCorpo = textoCorpo.Replace("[nomeDaEtapa]", nomeEtapa);

        mmObjeto.JSProperties["cp_TextoCorpo"] = textoCorpo;
        mmObjeto.JSProperties["cp_TextoAssunto"] = textoAssunto; 
    }

    private void getInformacoesEtapa(ref string nomeFluxo, ref string nomeEtapa)
    {
        string comandoSQL = string.Format(
                    @"SELECT 
	                  i.NomeInstancia
	                , e.[NomeEtapaWf]	
                FROM 
	                [EtapasInstanciasWf]									AS [ei]
            		
		                INNER JOIN [InstanciasWorkflows]		AS [i]	ON 
			                (			i.[CodigoWorkflow]			= ei.[CodigoWorkflow] 
				                AND	i.[CodigoInstanciaWf]		= ei.[CodigoInstanciaWf]	)
            					
			                INNER JOIN [Workflows]						AS [wf] ON 
				                (	wf.[CodigoWorkflow]				= i.[CodigoWorkflow] )
            						
				                INNER JOIN [Fluxos]							AS [f] ON 
					                ( f.[CodigoFluxo]					= wf.[CodigoFluxo] )
            							
		                INNER JOIN [EtapasWf]							AS [e] ON 
			                (			e.[CodigoWorkflow]			= ei.[CodigoWorkflow] 
				                AND e.[CodigoEtapaWf]				= ei.[CodigoEtapaWf] )
                        INNER JOIN Usuario                             AS [u] ON
				            ( u.CodigoUsuario                       = i.IdentificadorUsuarioCriadorInstancia)
				            

					    LEFT JOIN [dbo].[EtapasInstanciasWf]				AS ieAnterior ON 
						(			ieAnterior.[CodigoWorkflow]							= i.[CodigoWorkflow] 
							AND ieAnterior.[CodigoInstanciaWf]						= i.[CodigoInstanciaWf]
							AND ieAnterior.[SequenciaOcorrenciaEtapaWf]	= i.[OcorrenciaAtual]-1 )
							
					    LEFT JOIN [dbo].[EtapasWf]									AS eAnterior ON
						(			eAnterior.[CodigoWorkflow]	= ieAnterior.[CodigoWorkflow] 
							AND eAnterior.[CodigoEtapaWf]		= ieAnterior.[CodigoEtapaWf] 			)				            
            						
             WHERE 
		            ei.[CodigoWorkflow]		        = {0}
                AND	ei.[CodigoInstanciaWf]	        = {1}
                AND	ei.[CodigoEtapaWf]	            = {2}
                AND	ei.[SequenciaOcorrenciaEtapaWf]	= {3} 
", codigoWF, codigoInstancia,  codigoEtapa, codigoOcorrencia);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeFluxo = ds.Tables[0].Rows[0]["NomeInstancia"].ToString();
            nomeEtapa = ds.Tables[0].Rows[0]["NomeEtapaWf"].ToString();
        }
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Tramitacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "Tramitacao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 390);

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
           BEGIN
              DECLARE @in_codigoWorkFlow int
                    , @in_codigoInstanciaWf bigint
                    , @in_SequenciaOcorrenciaEtapaWf int
                    , @in_codigoEtapaWf int

              SET @in_codigoWorkFlow = {0}
              SET @in_codigoInstanciaWf = {1}
              SET @in_SequenciaOcorrenciaEtapaWf = {2}
              SET @in_codigoEtapaWf = {3}
      
              SELECT f.[CodigoModeloFormulario] 
                    ,f.[TituloFormulario]
                    ,CASE WHEN tfef.StatusTramitacao IS NULL THEN 'Sem Tramitação' ELSE tstf.DescricaoStatus END AS DescricaoStatus
                    ,tfef.DataSolicitacaoTramitacao
                    ,tfef.DataPrevistaConclusao
                    ,u.NomeUsuario AS Responsavel
                    ,tfef.CodigoUsuarioResponsavelTramitacao AS CodigoResponsavel
                    ,tfef.AssuntoMensagemNotificacao
                    ,tfef.TextoMensagemNotificacao
                FROM dbo.f_wf_formulariosInstanciaEtapa(@in_codigoWorkflow, @in_codigoInstanciaWf, @in_SequenciaOcorrenciaEtapaWf, @in_codigoEtapaWf) AS [f] INNER JOIN 
			         dbo.ModeloFormulario AS mf ON (mf.[CodigoModeloFormulario] = f.[CodigoModeloFormulario])
                                               AND (mf.[IniciaisFormularioControladoSistema] IS NULL 
                                                OR  mf.[IniciaisFormularioControladoSistema] NOT IN ('TRAMETP', 'TRAMFORMETP')) LEFT JOIN
                     dbo.TramitacaoFormularioEtapaFluxo tfef ON tfef.CodigoModeloFormulario = f.CodigoModeloFormulario
																									          AND tfef.CodigoWorkflow = @in_codigoWorkflow
																									          AND tfef.CodigoInstanciaWf = @in_codigoInstanciaWf
																									          AND tfef.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
																									          AND tfef.CodigoEtapaWf = @in_codigoEtapaWf
																									          AND tfef.DataExclusao IS NULL LEFT JOIN
			         dbo.Usuario u ON u.CodigoUsuario = tfef.CodigoUsuarioResponsavelTramitacao LEFT JOIN
					 dbo.TipoStatusTramitacaoFormulario tstf ON tstf.CodigoStatus = tfef.StatusTramitacao
               WHERE f.[TipoAcessoFormulario] = 'W'
                 AND f.[CodigoWorkflowOrigemFormulario]   IS NULL
                 AND f.[CodigoEtapaWfOrigemFormulario]    IS NULL

        END", codigoWF, codigoInstancia, codigoOcorrencia, codigoEtapa);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string filtro = "";

        //if (!IsPostBack && ddlGerenteProjeto.Value != null)
        //    filtro = ddlGerenteProjeto.Text;
        //else
        filtro = e.Filter.ToString();


        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, filtro, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

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
            return "-1";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoModeloFormulario = int.Parse(getChavePrimaria());

        string codigoResponsavel = ddlResponsavel.Value.ToString();
        string dataPrazo = string.Format("CONVERT(Datetime, '{0:dd/MM/yyyy}', 103)", ddlPrazo.Date);
        string assunto = txtAssunto.Text.Replace("'", "''");
        string mensagem = mmObjeto.Text.Replace("'", "''");

        string comandoSQL = string.Format(@"
            INSERT INTO [dbo].[TramitacaoFormularioEtapaFluxo]
           ([DataSolicitacaoTramitacao]
           ,[CodigoWorkflow]
           ,[CodigoInstanciaWf]
           ,[SequenciaEtapaWf]
           ,[CodigoEtapaWf]
           ,[CodigoModeloFormulario]
           ,[IndicaTipoResponsavelTramitacao]
           ,[CodigoUsuarioResponsavelTramitacao]
           ,[StatusTramitacao]
           ,[DataPrevistaConclusao]
           ,[AssuntoMensagemNotificacao]
           ,[TextoMensagemNotificacao]
           ,[DataInclusao]
           ,[CodigoUsuarioInclusao])
     VALUES
           (GetDate()
           ,{0}
           ,{1}
           ,{2}
           ,{3}
           ,{4}
           ,'U'
           ,{5}
           ,'TP'
           ,{6}
           ,'{7}'
           ,'{8}'
           ,GetDate()
           ,{9})"
            , codigoWF
            , codigoInstancia
            , codigoOcorrencia
            , codigoEtapa
            , codigoModeloFormulario
            , codigoResponsavel
            , dataPrazo
            , assunto
            , mensagem
            , codigoUsuarioLogado);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGrid();
            return "";
        }

    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoModeloFormulario = int.Parse(getChavePrimaria());

        string comandoSQL = string.Format(@"
            UPDATE [dbo].[TramitacaoFormularioEtapaFluxo]
               SET DataExclusao = GetDate()
                  ,CodigoUsuarioExclusao = {5}
                  ,StatusTramitacao = 'TC'
             WHERE [CodigoWorkflow] = {0}
               AND [CodigoInstanciaWf] = {1}
               AND [SequenciaEtapaWf] = {2}
               AND [CodigoEtapaWf] = {3}
               AND [CodigoModeloFormulario] = {4}
               AND DataExclusao IS NULL
           "
            , codigoWF
            , codigoInstancia
            , codigoOcorrencia
            , codigoEtapa
            , codigoModeloFormulario
            , codigoUsuarioLogado);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            carregaGrid();
            return "";
        }
    }

    #endregion

    protected void gvFormulariosBloqueados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Atualizar")
        {
            insereformularioBloqueado();
        }
        else if (e.Parameters == "Excluir")
        {
            excluiformularioBloqueado();
        }
        else
        {
            gvFormulariosBloqueados.Columns[0].Visible = e.Parameters == "Editar";
        }
    }

    private void insereformularioBloqueado()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @in_codigoWorkFlow Int
                  , @in_codigoInstanciaWf Bigint
                  , @in_SequenciaOcorrenciaEtapaWf Int
                  , @in_codigoEtapaWf Int
                  , @CodigoTramitacaoEtapaFluxo Int  
                  , @Bloq_codigoWorkFlow Int
                  , @Bloq_codigoEtapaWf Int              

                  SET @in_codigoWorkFlow = {0};
                  SET @in_codigoInstanciaWf = {1};
                  SET @in_SequenciaOcorrenciaEtapaWf = {2};
                  SET @in_codigoEtapaWf = {3};                 
      
                  SELECT @CodigoTramitacaoEtapaFluxo = CodigoTramitacaoEtapaFluxo 
                    FROM TramitacaoFormularioEtapaFluxo tf 
                   WHERE tf.CodigoModeloFormulario = {4}
                     AND tf.CodigoWorkflow = @in_codigoWorkFlow
                     AND tf.CodigoInstanciaWf = @in_codigoInstanciaWf
                     AND tf.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
                     AND tf.CodigoEtapaWf = @in_codigoEtapaWf
                     AND tf.DataExclusao IS NULL

            SELECT @Bloq_codigoWorkFlow = f.[CodigoWorkflowOrigemFormulario]
                 , @Bloq_codigoEtapaWf = f.[CodigoEtapaWfOrigemFormulario]            
              FROM dbo.f_wf_formulariosInstanciaEtapa(@in_codigoWorkflow, @in_codigoInstanciaWf, @in_SequenciaOcorrenciaEtapaWf, @in_codigoEtapaWf)   AS [f]
             WHERE f.CodigoModeloFormulario = {5}

            IF EXISTS(SELECT 1 FROM [BloqueioTramitacaoFormularioEtapaFluxo] WHERE CodigoTramitacaoEtapaFluxo = @CodigoTramitacaoEtapaFluxo AND CodigoModeloFormulario = {5})
                BEGIN
                    UPDATE BloqueioTramitacaoFormularioEtapaFluxo 
                       SET [StatusRegistro] = 'A'
                          ,[DataAtivacaoRegistro] = GetDate()
                          ,[CodigoUsuarioAtivacao] = {6}
                     WHERE CodigoModeloFormulario = {5}
                       AND CodigoTramitacaoEtapaFluxo = @CodigoTramitacaoEtapaFluxo
                END ELSE
                BEGIN
                    INSERT INTO [dbo].[BloqueioTramitacaoFormularioEtapaFluxo]
                           ([CodigoTramitacaoEtapaFluxo]
                           ,[CodigoModeloFormulario]
                           ,[CodigoWorkflowOrigemFormulario]
                           ,[CodigoEtapaWfOrigemFormulario]
                           ,[StatusRegistro]
                           ,[DataInclusao]
                           ,[CodigoUsuarioInclusao]
                           ,[DataAtivacaoRegistro]
                           ,[CodigoUsuarioAtivacao])
                     VALUES
                           (@CodigoTramitacaoEtapaFluxo
                           ,{5}
                           ,@Bloq_codigoWorkFlow
                           ,@Bloq_codigoEtapaWf
                           ,'A'
                           ,GetDate()
                           ,{6}
                           ,GetDate()
                           ,{6})

                  END
         END"
            , codigoWF
            , codigoInstancia
            , codigoOcorrencia
            , codigoEtapa
            , getChavePrimaria()
            , ddlForms.Value
            , codigoUsuarioLogado);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        carregaGvFormulariosBloqueados();
    }

    private void excluiformularioBloqueado()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @in_codigoWorkFlow Int
                  , @in_codigoInstanciaWf Bigint
                  , @in_SequenciaOcorrenciaEtapaWf Int
                  , @in_codigoEtapaWf Int
                  , @CodigoTramitacaoEtapaFluxo Int                

                  SET @in_codigoWorkFlow = {0};
                  SET @in_codigoInstanciaWf = {1};
                  SET @in_SequenciaOcorrenciaEtapaWf = {2};
                  SET @in_codigoEtapaWf = {3};                 
      
                  SELECT @CodigoTramitacaoEtapaFluxo = CodigoTramitacaoEtapaFluxo 
                    FROM TramitacaoFormularioEtapaFluxo tf 
                   WHERE tf.CodigoModeloFormulario = {4}
                     AND tf.CodigoWorkflow = @in_codigoWorkFlow
                     AND tf.CodigoInstanciaWf = @in_codigoInstanciaWf
                     AND tf.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
                     AND tf.CodigoEtapaWf = @in_codigoEtapaWf
                     AND tf.DataExclusao IS NULL
            
                    UPDATE BloqueioTramitacaoFormularioEtapaFluxo 
                       SET [StatusRegistro] = 'D'
                          ,[DataDesativacaoRegistro] = GetDate()
                          ,[CodigoUsuarioDesativacao] = {6}
                     WHERE CodigoModeloFormulario = {5}
                       AND CodigoTramitacaoEtapaFluxo = @CodigoTramitacaoEtapaFluxo
               
         END"
            , codigoWF
            , codigoInstancia
            , codigoOcorrencia
            , codigoEtapa
            , getChavePrimaria()
            , getChavePrimariaBloqueados()
            , codigoUsuarioLogado);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        carregaGvFormulariosBloqueados();
    }

    // retorna a primary key da tabela.
    private string getChavePrimariaBloqueados()
    {
        if (gvFormulariosBloqueados.FocusedRowIndex >= 0)
            return gvFormulariosBloqueados.GetRowValues(gvFormulariosBloqueados.FocusedRowIndex, gvFormulariosBloqueados.KeyFieldName).ToString();
        else
            return "-1";
    }

    private void carregaComboForms()
    {
        string comandoSQL = string.Format(@"
            BEGIN
                  DECLARE @in_codigoWorkFlow Int
                        , @in_codigoInstanciaWf Bigint
                        , @in_SequenciaOcorrenciaEtapaWf Int
                        , @in_codigoEtapaWf Int
                        , @CodigoTramitacaoEtapaFluxo Int                

                  SET @in_codigoWorkFlow = {0};
                  SET @in_codigoInstanciaWf = {1};
                  SET @in_SequenciaOcorrenciaEtapaWf = {2};
                  SET @in_codigoEtapaWf = {3};                 
      
                  SELECT @CodigoTramitacaoEtapaFluxo = CodigoTramitacaoEtapaFluxo 
                    FROM TramitacaoFormularioEtapaFluxo tf 
                   WHERE tf.CodigoModeloFormulario = {4}
                     AND tf.CodigoWorkflow = @in_codigoWorkFlow
                     AND tf.CodigoInstanciaWf = @in_codigoInstanciaWf
                     AND tf.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
                     AND tf.CodigoEtapaWf = @in_codigoEtapaWf
                     AND tf.DataExclusao IS NULL

                  SELECT f.[CodigoModeloFormulario] 
                        ,f.[TituloFormulario]              
                    FROM dbo.f_wf_formulariosInstanciaEtapa(@in_codigoWorkflow, @in_codigoInstanciaWf, @in_SequenciaOcorrenciaEtapaWf, @in_codigoEtapaWf)   AS [f]
                   WHERE f.CodigoModeloFormulario <> {4}
                     AND NOT EXISTS(SELECT 1 
                                      FROM BloqueioTramitacaoFormularioEtapaFluxo bf 
                                     WHERE bf.CodigoModeloFormulario = f.CodigoModeloFormulario 
                                       AND bf.StatusRegistro = 'A'
                                       AND bf.CodigoTramitacaoEtapaFluxo = @CodigoTramitacaoEtapaFluxo)
            END", codigoWF
                , codigoInstancia
                , codigoOcorrencia
                , codigoEtapa
                , getChavePrimaria());

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlForms.DataSource = ds;
        ddlForms.TextField = "TituloFormulario";
        ddlForms.ValueField = "CodigoModeloFormulario";
        ddlForms.DataBind();        
    }

    private void carregaGvFormulariosBloqueados()
    {
        string comandoSQL = string.Format(@"
            BEGIN
                  DECLARE @in_codigoWorkFlow Int
                        , @in_codigoInstanciaWf Bigint
                        , @in_SequenciaOcorrenciaEtapaWf Int
                        , @in_codigoEtapaWf Int
                        , @CodigoTramitacaoEtapaFluxo Int                

                  SET @in_codigoWorkFlow = {0};
                  SET @in_codigoInstanciaWf = {1};
                  SET @in_SequenciaOcorrenciaEtapaWf = {2};
                  SET @in_codigoEtapaWf = {3};                 
      
                  SELECT @CodigoTramitacaoEtapaFluxo = CodigoTramitacaoEtapaFluxo 
                    FROM TramitacaoFormularioEtapaFluxo tf 
                   WHERE tf.CodigoModeloFormulario = {4}
                     AND tf.CodigoWorkflow = @in_codigoWorkFlow
                     AND tf.CodigoInstanciaWf = @in_codigoInstanciaWf
                     AND tf.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
                     AND tf.CodigoEtapaWf = @in_codigoEtapaWf
                     AND tf.DataExclusao IS NULL

                  SELECT f.[CodigoModeloFormulario] 
                        ,f.[TituloFormulario]              
                    FROM dbo.f_wf_formulariosInstanciaEtapa(@in_codigoWorkflow, @in_codigoInstanciaWf, @in_SequenciaOcorrenciaEtapaWf, @in_codigoEtapaWf)   AS [f]
                   WHERE EXISTS(SELECT 1 
                                  FROM BloqueioTramitacaoFormularioEtapaFluxo bf 
                                 WHERE bf.CodigoModeloFormulario = f.CodigoModeloFormulario 
                                   AND bf.StatusRegistro = 'A'
                                   AND bf.CodigoTramitacaoEtapaFluxo = @CodigoTramitacaoEtapaFluxo)
            END", codigoWF
                , codigoInstancia
                , codigoOcorrencia
                , codigoEtapa
                , getChavePrimaria());

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvFormulariosBloqueados.DataSource = ds;
        gvFormulariosBloqueados.DataBind();
    }
    
    protected void gvHistorico_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        
    }

    private void carregaGridHistorico()
    {
        string comandoSQL = string.Format(@"
           BEGIN
              DECLARE @in_codigoWorkFlow int
                    , @in_codigoInstanciaWf bigint
                    , @in_SequenciaOcorrenciaEtapaWf int
                    , @in_codigoEtapaWf int

              SET @in_codigoWorkFlow = {0}
              SET @in_codigoInstanciaWf = {1}
              SET @in_SequenciaOcorrenciaEtapaWf = {2}
              SET @in_codigoEtapaWf = {3}
      
              SELECT tfef.CodigoTramitacaoEtapaFluxo
                    ,tstf.DescricaoStatus AS DescricaoStatus
                    ,tfef.DataSolicitacaoTramitacao
                    ,tfef.DataPrevistaConclusao
                    ,u.NomeUsuario AS Responsavel
                FROM dbo.TramitacaoFormularioEtapaFluxo tfef INNER JOIN
			         dbo.Usuario u ON u.CodigoUsuario = tfef.CodigoUsuarioResponsavelTramitacao LEFT JOIN
					 dbo.TipoStatusTramitacaoFormulario tstf ON tstf.CodigoStatus = tfef.StatusTramitacao
               WHERE tfef.CodigoModeloFormulario = {4}
				 AND tfef.CodigoWorkflow = @in_codigoWorkflow
				 AND tfef.CodigoInstanciaWf = @in_codigoInstanciaWf
				 AND tfef.SequenciaEtapaWf = @in_SequenciaOcorrenciaEtapaWf
				 AND tfef.CodigoEtapaWf = @in_codigoEtapaWf
               ORDER BY tfef.DataInclusao DESC

        END", codigoWF, codigoInstancia, codigoOcorrencia, codigoEtapa, getChavePrimaria());

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvHistorico.DataSource = ds;
            gvHistorico.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (readOnly == "S")
        {
            if (e.ButtonID == "btnEditarCustom")
            {
                e.Enabled = false;
                e.Image.Url = "../imagens/botoes/editarRegDes.png";
            }
            else if (e.ButtonID == "btnExcluirCustom")
            {
                e.Enabled = false;
                e.Image.Url = "../imagens/botoes/excluirRegDes.png";
            }
        }
        else
        {
            string dataSolicitacaoTramitacao = gvDados.GetRowValues(e.VisibleIndex, "DataSolicitacaoTramitacao") + "";

            if (e.ButtonID == "btnExcluirCustom" && dataSolicitacaoTramitacao == "")
            {
                e.Enabled = false;
                e.Image.Url = "../imagens/botoes/excluirRegDes.png";
            }
        }
    }
}