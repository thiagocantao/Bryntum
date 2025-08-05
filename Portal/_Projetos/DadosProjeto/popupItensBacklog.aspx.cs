using System;
using System.Data;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Globalization;
using System.Text;
using System.Linq;

public partial class _Projetos_DadosProjeto_popupItensBacklog : System.Web.UI.Page
{
    dados cDados;

    private int idProjeto = -1;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoItemBacklog = -1;
    private int codigoProjetoAgil = -1;
    private int codigoProjetoIteracao = -1;
    private int codigoIteracao = -1;

    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    private string acao = "";
    public bool podeIncluir = true;
    public int alturaFrameAnexos = 0;
    public int alturaUrl = 0;
    private bool IndicaImportacaoTarefaCronograma = false;


    private int _codigoRaia;
    /// <summary>
    /// Código da raia atual
    /// </summary>
    public int CodigoRaia
    {
        get { return _codigoRaia; }
        set
        {
            _codigoRaia = value;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
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

        bool retorno = false;
        //if (!Page.IsPostBack && (codigoItemBacklog <= 0))
        //{
        retorno = int.TryParse((Request.QueryString["CI"] + "").Replace(",", ""), out codigoItemBacklog);
        acao = (Request.QueryString["acao"] + "").ToString();
        //}

        retorno = int.TryParse(Request.QueryString["IDProjeto"] + "", out idProjeto);
        retorno = int.TryParse(Request.QueryString["CPA"] + "", out codigoProjetoAgil);
        retorno = int.TryParse(Request.QueryString["ALT"] + "", out alturaUrl);

        btnSalvar.JSProperties["cpAcao"] = acao;

        codigoProjetoIteracao = getCodigoProjetoIteracao();
        codigoIteracao = getCodigoIteracao();


        if (acao != "editar")
        {
            ddlRecurso.DataSourceID = null;
        }
        if (acao == "editarTarefasItemRO")
        {
            txtTituloItem.ClientEnabled = false;
            txtImportancia.ClientEnabled = false;
            txtEsforco.ClientEnabled = false;
            txtDetalheItem.ClientEnabled = false;

            tagBox.ClientEnabled = false;
            txtEsforco.ClientEnabled = false;
            txtPercentualConcluido.ClientEnabled = false;

            ddlRecurso.ClientEnabled = false;
            ddlClassificacao.ClientEnabled = false;
            dtDataAlvo.ClientEnabled = false;
            btnSalvar.ClientVisible = false;
        }
        tagBox.NullText = ((acao == "kanbanAgilIncluiTarefa") || (acao == "kanbanAgilEditaTarefa")) ? "Tags da tarefa" : "Tags do item de backlog";
        retorno_popup.Value = "";
        callbackTela.JSProperties["cpCodigo"] = null;
        if (Session["CodigoProjeto"] == null || Session["CodigoProjeto"].ToString() == "")
            Session["CodigoProjeto"] = idProjeto;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        headerOnTela();
        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(idProjeto, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaFrameAnexos(resolucaoCliente);


        populaClassificacao();

        populaDdlRecursos();

        populaPacoteTrabalho();

        cDados.aplicaEstiloVisual(Page);
        if (!Page.IsPostBack)
        {
            carregaDadosTela();
        }

        DataTable dtMetadados_detalheItem = cDados.getMetadadosTabelaBanco("Agil_ItemBacklog", "DetalheItem").Tables[0];
        DataTable dtMetadados_ComentarioCustoItem = cDados.getMetadadosTabelaBanco("Agil_ItemBacklog", "ComentarioCustoItem").Tables[0];
        DataTable dtMetadados_ComentarioReceitaItem = cDados.getMetadadosTabelaBanco("Agil_ItemBacklog", "ComentarioCustoItem").Tables[0];

        //cDados.setaTamanhoMaximoMemo(txtDetalheItem, int.Parse(dtMetadados_detalheItem.Rows[0]["Tamanho"].ToString()), lblContadorMemoDescricao);
        cDados.setaTamanhoMaximoMemo(memoComentarioCusto, int.Parse(dtMetadados_ComentarioCustoItem.Rows[0]["Tamanho"].ToString()), lblContadorMemoComentarioCusto);
        cDados.setaTamanhoMaximoMemo(memoComentarioReceita, int.Parse(dtMetadados_ComentarioReceitaItem.Rows[0]["Tamanho"].ToString()), lblContadorMemoComentarioReceita);

        TabPage paginaTabFinanceiro = tabControl.TabPages.FindByName("tabFinanceiro");
        paginaTabFinanceiro.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        TabPage paginaTabChecklist = tabControl.TabPages.FindByName("tabChecklist");
        paginaTabChecklist.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        TabPage paginatabComentarios = tabControl.TabPages.FindByName("tabComentarios");
        paginatabComentarios.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        TabPage paginaTabAnexo = tabControl.TabPages.FindByName("tabA");
        paginaTabAnexo.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        TabPage paginaHistorico = tabControl.TabPages.FindByName("tabHistorico");
        paginaHistorico.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        TabPage paginaLinks = tabControl.TabPages.FindByName("tabLinks");
        paginaLinks.ClientVisible = !(acao.ToUpper().Contains("INCLUI") == true);

        tabControl.JSProperties["cpFrameAnexos"] = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=IB&ID=" + codigoItemBacklog + "&ALT=" + (alturaFrameAnexos - 20) + "&Frame=S";
        tabControl.JSProperties["cpFrameChecklist"] = "agil_FrameChecklist.aspx?CI=" + codigoItemBacklog;
        tabControl.JSProperties["cpFrameHistorico"] = "../Agil/agil_HistoricoItem.aspx?CI=" + codigoItemBacklog;
        tabControl.JSProperties["cpFrameLinks"] = "../Agil/agil_Links.aspx?CI=" + codigoItemBacklog;


        spnCustoPrevisto.ReadOnly = IndicaImportacaoTarefaCronograma;
        spnReceitaPrevista.ReadOnly = IndicaImportacaoTarefaCronograma;
        memoComentarioCusto.ReadOnly = IndicaImportacaoTarefaCronograma;
        memoComentarioReceita.ReadOnly = IndicaImportacaoTarefaCronograma;
        ddlPacoteTrabalhoAssociado.ReadOnly = true;
        Session["TituloItem"] = txtTituloItem.Value;
        Session["CodigoItem"] = codigoItemBacklog;

        bool podeManterItemBacklog = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_IteBkl");
        bool podeManterItemSprint = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltItSpt");

        if (acao == "kanbanAgilEditaTarefaBacklog")
        {
            btnSalvar.ClientVisible = podeManterItemBacklog;
            txtTituloItem.ClientEnabled = podeManterItemBacklog;
            txtImportancia.ClientEnabled = podeManterItemBacklog;
            txtEsforco.ClientEnabled = podeManterItemBacklog;
            txtDetalheItem.ClientEnabled = podeManterItemBacklog;

            tagBox.ClientEnabled = podeManterItemBacklog;
            txtEsforco.ClientEnabled = podeManterItemBacklog;
            txtPercentualConcluido.ClientEnabled = podeManterItemBacklog;

            ddlRecurso.ClientEnabled = podeManterItemBacklog;
            ddlClassificacao.ClientEnabled = podeManterItemBacklog;
            dtDataAlvo.ClientEnabled = podeManterItemBacklog;
            btnSalvar.ClientVisible = podeManterItemBacklog;

            memoComentarioCusto.ClientEnabled = podeManterItemBacklog;
            memoComentarioReceita.ClientEnabled = podeManterItemBacklog;

            spnCustoPrevisto.ClientEnabled = podeManterItemBacklog;
            spnReceitaPrevista.ClientEnabled = podeManterItemBacklog;
        }
        if (acao == "kanbanAgilEditaTarefa")
        {
            btnSalvar.ClientVisible = podeManterItemSprint;
            txtTituloItem.ClientEnabled = podeManterItemSprint;
            txtImportancia.ClientEnabled = podeManterItemSprint;
            txtEsforco.ClientEnabled = podeManterItemSprint;
            txtDetalheItem.ClientEnabled = podeManterItemSprint;

            tagBox.ClientEnabled = podeManterItemSprint;
            txtEsforco.ClientEnabled = podeManterItemSprint;
            txtPercentualConcluido.ClientEnabled = podeManterItemSprint;

            ddlRecurso.ClientEnabled = podeManterItemSprint;
            ddlClassificacao.ClientEnabled = podeManterItemSprint;
            dtDataAlvo.ClientEnabled = podeManterItemSprint;
            btnSalvar.ClientVisible = podeManterItemSprint;

            memoComentarioCusto.ClientEnabled = podeManterItemSprint;
            memoComentarioReceita.ClientEnabled = podeManterItemSprint;

            spnCustoPrevisto.ClientEnabled = podeManterItemSprint;
            spnReceitaPrevista.ClientEnabled = podeManterItemSprint;
        }

        DateTime dateOut = DateTime.Now;
        bool bDataAlvoInformada;

        if(string.IsNullOrWhiteSpace(dtDataAlvo.Text))
        {
            bDataAlvoInformada = false;
        }
        else
        {
            bDataAlvoInformada = DateTime.TryParse(dtDataAlvo.Date.ToString(), out dateOut);
        }
        if(bDataAlvoInformada == true)
        {
            /*
            Ajustar a tela de edição popup de item de backlog (em Sprint ou Backlog do projeto) para que, quando a Data-Alvo estiver preenchida, 
            só permitir a sua alteração por alguém que tenha perfil Product Owner ou Scrum Master. Ou seja, enquanto o campo Data-Alvo estiver em branco, 
            fica exatamente como está hoje. Caso este campo esteja com informação, ele deverá ficar bloqueado, podendo ser editado somente pelos perfis 
            mencionados. 
            Quando ele estiver bloqueado, apresentá-lo em fundo cinza indicando que está não editável e, ao posicionar o mouse sobre ele, 
            apresentar o hint "Este campo está bloqueado é só pode ser alterado pelo Product Owner ou Scrum Master".
             */
            DataSet ds1 = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "AGIL_RestricaoEdicaoDataAlvo");
            if(cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                var indicarestricao = ds1.Tables[0].Rows[0]["AGIL_RestricaoEdicaoDataAlvo"].ToString() + "" == "S";
                if(indicarestricao == true)
                {
                    string comandoSQL = string.Format(@"
                    SELECT TOP 1 CAST(1 AS bit) AS [foundRecord]
                        FROM 
						    [dbo].[Agil_ItemBacklog]						    AS [i]

						    INNER JOIN [dbo].[Agil_Iteracao]					AS [itr]	ON 
							    (itr.[CodigoIteracao] = i.[CodigoIteracao])

						    INNER JOIN [dbo].[Projeto]							AS [pItr]	ON 
							    (	pItr.[CodigoProjeto] = itr.[CodigoProjetoIteracao])

						    INNER JOIN [dbo].[Agil_RecursoIteracao]		        AS [ri]		ON
							    (	ri.[CodigoIteracao] = i.[CodigoIteracao] )

						    INNER JOIN [dbo].[RecursoCorporativo]			    AS [rc]		ON 
							    (			rc.[CodigoRecursoCorporativo]   = ri.[CodigoRecursoCorporativo]
								    AND rc.[CodigoUsuario]					= {1}
								    AND rc.[IndicaRecursoAtivo]				= 'S' -- tem que ser um recurso ativo
								    AND rc.[CodigoEntidade]					= pItr.[CodigoEntidade] -- tem que ser um recurso da entidade do projeto
								    AND rc.[CodigoTipoRecurso]				= 1  )-- tem que ser um recurso do tipo pessoa

						    INNER JOIN [dbo].[Agil_TipoPapelRecurso]	        AS [tpr]	ON 
							    (			tpr.[CodigoTipoPapelRecurso] = ri.[CodigoTipoPapelRecursoIteracao]
								    AND tpr.[IniciasTipoPapelRecursoControladoSistema] IN ('SCM', 'PO') )
					    WHERE
						i.[CodigoItem] = {0};", codigoItemBacklog , codigoUsuarioResponsavel);
                    DataSet ds2 = cDados.getDataSet(comandoSQL);
                    if(cDados.DataSetOk(ds2) && cDados.DataTableOk(ds2.Tables[0]))
                    {
                        dtDataAlvo.ReadOnly = false;
                    }
                    else
                    {
                        dtDataAlvo.ReadOnly = true;
                        dtDataAlvo.ToolTip = "Este campo está bloqueado é só pode ser alterado pelo Product Owner ou Scrum Master";
                    }
                }
            }
        }
    }

    private void populaPacoteTrabalho()
    {
        string comandoSQL = string.Format(@"SELECT '-1' as CodigoTarefa, 'Nenhum' as NomeTarefa
                                            UNION
                                                 SELECT 
                                                   CodigoTarefa,
                                                   NomeTarefa 
                                             FROM [dbo].[f_Agil_GetPacotesTrabalho] ({0})
                                           ", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlPacoteTrabalhoAssociado.DataSource = ds.Tables[0];
        ddlPacoteTrabalhoAssociado.TextField = "NomeTarefa";
        ddlPacoteTrabalhoAssociado.ValueField = "CodigoTarefa";
        ddlPacoteTrabalhoAssociado.DataBind();
    }

    private int getCodigoIteracao()
    {
        bool retornoL = false;
        int retornoInt = -1;
        string comandoSQL = string.Format(@" SELECT TOP 1 CodigoIteracao FROM Agil_Iteracao WHERE CodigoProjetoIteracao = {0}", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retornoL = int.TryParse(ds.Tables[0].Rows[0]["CodigoIteracao"].ToString(), out retornoInt);
        }
        return retornoInt;
    }

    private int getCodigoProjetoIteracao()
    {
        bool retornoL = false;
        int retornoInt = -1;
        string comandoSQL = string.Format(@" SELECT [CodigoProjetoPai]
  FROM [dbo].[LinkProjeto]
where CodigoProjetoFilho = {0}", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retornoL = int.TryParse(ds.Tables[0].Rows[0]["CodigoProjetoPai"].ToString(), out retornoInt);
        }
        return retornoInt;
    }
    private bool ehProjetoAgil()
    {
        string comandoSQL = string.Format(@"
        SELECT IndicaProjetoAgil, IndicaTipoProjeto 
          FROM TipoProjeto
         WHERE CodigoTipoProjeto 
            IN (SELECT CodigoTipoProjeto
                  FROM projeto
                 WHERE CodigoProjeto = {0})", idProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            return (ds.Tables[0].Rows[0]["IndicaProjetoAgil"].ToString().Trim() + "") == "S";
        }

        return false;
    }

    private void populaDdlRecursos()
    {
        string comandoSQL_Projeto = string.Format(@"
                            SELECT rp.CodigoRecursoProjeto, 
                                   rp.CodigoRecursoCorporativo, 
                                   rp.CodigoTipoPapelRecursoProjeto, 
                                   rc.NomeRecurso, 
                                   tpr.DescricaoTipoPapelRecurso
                              FROM Agil_RecursoProjeto AS rp 
                              INNER JOIN vi_RecursoCorporativo AS rc ON rc.CodigoRecursoCorporativo = rp.CodigoRecursoCorporativo 
                              INNER JOIN Agil_TipoPapelRecurso AS tpr ON tpr.CodigoTipoPapelRecurso = rp.CodigoTipoPapelRecursoProjeto 
                              WHERE (rp.CodigoProjeto = {0}) ORDER BY rc.NomeRecurso", idProjeto);

        string comandoSQL_Iteracao = string.Format(@"SELECT
                            ri.CodigoRecursoIteracao AS CodigoRecursoProjeto, 
                            ri.CodigoRecursoCorporativo, 
                            ri.CodigoTipoPapelRecursoIteracao as CodigoTipoPapelRecursoProjeto, 
                            rc.NomeRecurso, 
                            tpr.DescricaoTipoPapelRecurso,
                            ri.PercentualAlocacao
						   from Agil_RecursoIteracao ri
						   INNER JOIN vi_RecursoCorporativo AS rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo 
                           INNER JOIN Agil_TipoPapelRecurso AS tpr ON tpr.CodigoTipoPapelRecurso = ri.CodigoTipoPapelRecursoIteracao
                           WHERE (ri.CodigoIteracao = {0}) ORDER BY rc.NomeRecurso", codigoIteracao);

        DataSet ds = cDados.getDataSet(ehProjetoAgil() == true ? comandoSQL_Projeto : comandoSQL_Iteracao);
        if (cDados.DataSetOk(ds))
        {
            ddlRecurso.DataSource = ds.Tables[0];
            ddlRecurso.DataBind();
            ddlRecurso.Items.Add(new ListEditItem("Nenhum", null));
        }
    }

    private void carregaDadosTela()
    {

        bool ret = false;
        string filtraPeloCodigoProjetoIB = string.Format(" ib.CodigoProjeto = {0} AND ", idProjeto);
        if (acao != "editar")
        {
            filtraPeloCodigoProjetoIB = "";
        }
        else if (acao == "editar" && IndicaSeItemDeBacklogEstaEmOutroProjeto())
        {
            filtraPeloCodigoProjetoIB = "";
        }

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @tabelaItens AS TABLE(
            CodigoItem  int,
            textoTag  varchar(130)
            );

              DECLARE @textoTagItem AS VARCHAR(max)

              DECLARE crsWork CURSOR LOCAL FAST_FORWARD FOR   
              SELECT TextoTag, CodigoItem 
                FROM Agil_TagItemBackLog 
               WHERE CodigoItem in (SELECT codigoitem 
	                                   FROM Agil_ItemBacklog 
		                              WHERE CodigoProjeto = {4} OR CodigoItem = {3})

	            DECLARE @l_TextosTAG as varchar(500)
	            DECLARE @l_CodigoItem as int
	
	            OPEN crsWork    
    
	            FETCH NEXT FROM crsWork INTO @l_TextosTAG, @l_CodigoItem
	            WHILE (@@FETCH_STATUS = 0 )
                BEGIN
	               IF(EXISTS(SELECT 1 
	                           FROM @tabelaItens 
				               WHERE CodigoItem = @l_CodigoItem) )
	               BEGIN
		                UPDATE @tabelaItens
			               SET TextoTag = ISNULL(textoTag, '') + '|' + @l_TextosTAG
		                 WHERE CodigoItem = @l_CodigoItem
	               END
	               ELSE
	               BEGIN
	                   INSERT INTO  @tabelaItens(CodigoItem, textoTag) values(@l_CodigoItem, @l_TextosTAG)
	               END
	               FETCH NEXT FROM crsWork INTO @l_TextosTAG, @l_CodigoItem
	            END
    	        --SELECT * FROM @tabelaItens    

        SELECT ib.[CodigoItem]
              ,ib.[CodigoProjeto]
              ,ib.[CodigoItemSuperior]
              ,ib.[TituloItem]
              ,ibSUP.TituloItem as TituloItemSuperior 
              ,ib.[DetalheItem]
              ,ib.[CodigoTipoStatusItem]
              ,ts.DescricaoTipoStatusItem
              ,ib.[CodigoTipoClassificacaoItem]
              ,tc.DescricaoTipoClassificacaoItem
              ,ib.[CodigoUsuarioInclusao]
              ,ib.[DataInclusao]
              ,ib.[CodigoUsuarioUltimaAlteracao]
              ,ib.[DataUltimaAlteracao]
              ,ib.[CodigoUsuarioExclusao]
              ,ib.[PercentualConcluido]
              ,ib.[CodigoIteracao]
              ,ib.[Importancia]
              ,ib.[Complexidade]
              ,(SELECT CASE ib.[Complexidade]
                 WHEN 0 THEN 'Baixa'
                 WHEN 1 THEN 'Média'
                 WHEN 2 THEN 'Alta'
                 WHEN 3 THEN 'Muito Alta'
               END) AS DescricaoComplexidade
              ,ib.[EsforcoPrevisto]
              ,ib.[EsforcoReal]
              ,ib.[IndicaItemNaoPlanejado]
              ,ib.[IndicaQuestao]
              ,ib.[IndicaBloqueioItem]
              ,ib.[CodigoWorkflow]
              ,ib.[CodigoInstanciaWF]
              ,ib.[CodigoCronogramaProjetoReferencia]
              ,ib.[CodigoTarefaReferencia]
              ,rc.CodigoRecursoCorporativo as CodigoRecurso
              ,rc.NomeRecursoCorporativo as NomeRecurso
              ,ttf.CodigoTipoTarefaTimeSheet
              ,ttf.DescricaoTipoTarefaTimeSheet
              ,ib.ReceitaPrevista
              ,ISNULL(ib.[indicatarefa], 'N') AS IndicaTarefa
              ,(SELECT p.NomeProjeto  
                  FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                       {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                       {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                 WHERE ai.CodigoItem = ib.[CodigoItem]) as Sprint
              ,(SELECT i.DataPublicacaoPlanejamento 
                  FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                       {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                       {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                 WHERE ai.CodigoItem = ib.[CodigoItem]) as DataPublicacaoPlanejamento
             ,ib.DataAlvo
             ,(SELECT TextoTag FROM @tabelaItens WHERE CodigoItem = ib.CodigoItem) as TagItem
             ,ib.CodigoRaia
             ,ib.IndicaBloqueioItem
             ,ib.ValorReceitaItem
             ,ib.ComentarioReceitaItem
             ,ib.ValorCustoItem
             ,ib.ComentarioCustoItem 
             ,ib.CodigoTarefaReferencia
             ,ib.IndicaImportacaoTarefaCronograma
         FROM {0}.{1}.[Agil_ItemBacklog] ib
		 left join {0}.{1}.[Agil_ItemBacklog] ibSUP on (ibSUP.CodigoItem = ib.CodigoItemSuperior)
         LEFT JOIN {0}.{1}.Agil_TipoStatusItemBacklog ts on (ib.CodigoTipoStatusItem = ts.CodigoTipoStatusItem)
         LEFT JOIN {0}.{1}.Agil_TipoClassificacaoItemBacklog tc on (ib.CodigoTipoClassificacaoItem = tc.CodigoTipoClassificacaoItem)
         LEFT JOIN {0}.{1}.Agil_RecursoProjeto rp on (rp.CodigoRecursoCorporativo = ib.CodigoRecursoAlocado)
         LEFT JOIN {0}.{1}.RecursoCorporativo rc on (rc.CodigoRecursoCorporativo = rp.CodigoRecursoCorporativo)         
         LEFT JOIN {0}.{1}.TipoTarefaTimeSheet AS ttf on (ttf.CodigoTipoTarefaTimeSheet = ib.CodigoTipoTarefaTimesheet) 
         WHERE {2} ib.CodigoItem = {3} 
        END", cDados.getDbName(), cDados.getDbOwner(), filtraPeloCodigoProjetoIB, codigoItemBacklog, idProjeto);

        DataSet dsDadosTela = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsDadosTela) && cDados.DataTableOk(dsDadosTela.Tables[0]))
        {
            if (acao != "incluirTarefasItem" && acao != "incluir" && acao != "kanbanAgilIncluiTarefa")
            {
                txtCodigoItem.Text = "ID-" + dsDadosTela.Tables[0].Rows[0]["CodigoItem"].ToString();
                tagBox.Text = dsDadosTela.Tables[0].Rows[0]["TagItem"].ToString();
                txtTituloItem.Text = dsDadosTela.Tables[0].Rows[0]["TituloItem"].ToString();
                txtDetalheItem.Html = dsDadosTela.Tables[0].Rows[0]["DetalheItem"].ToString();
                txtEsforco.Text = dsDadosTela.Tables[0].Rows[0]["EsforcoPrevisto"].ToString();

                int import = -1;
                ret = int.TryParse((dsDadosTela.Tables[0].Rows[0]["Importancia"] != null) ? dsDadosTela.Tables[0].Rows[0]["Importancia"].ToString() : "-1", out import);
                txtImportancia.Value = import;
                bool indicaItemBacklog = dsDadosTela.Tables[0].Rows[0]["IndicaTarefa"].ToString() == "N";

                string percentualConcluidoTratado = string.Format("{0:N0}", Math.Truncate(double.Parse(dsDadosTela.Tables[0].Rows[0]["PercentualConcluido"].ToString())) + "%");

                txtPercentualConcluido.Text = percentualConcluidoTratado;

                int recurs = -1;
                ret = int.TryParse((dsDadosTela.Tables[0].Rows[0]["CodigoRecurso"] != null) ? dsDadosTela.Tables[0].Rows[0]["CodigoRecurso"].ToString() : "-1", out recurs);
                ddlRecurso.Value = recurs;
                if (recurs == -1 || recurs == 0)
                {
                    ddlRecurso.Value = null;
                    ddlRecurso.SelectedItem = new ListEditItem("Nenhum", null);
                }

                DateTime dataAlvox = DateTime.MinValue;
                bool retornoXX = DateTime.TryParse(dsDadosTela.Tables[0].Rows[0]["DataAlvo"].ToString(), out dataAlvox);


                dtDataAlvo.Value = dataAlvox;

                ddlClassificacao.Value = dsDadosTela.Tables[0].Rows[0]["CodigoTipoClassificacaoItem"].ToString();

                string codigoTipoStatusItem = dsDadosTela.Tables[0].Rows[0]["CodigoTipoStatusItem"].ToString();

                if (codigoTipoStatusItem != "")
                {
                    string cmdsqlTipoStatus = string.Format(@" SELECT DescricaoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE CodigoTipoStatusItem = {0}", codigoTipoStatusItem);
                    DataSet dsTipoStatusItem = cDados.getDataSet(cmdsqlTipoStatus);
                    if (cDados.DataSetOk(dsTipoStatusItem))
                    {
                        lblDescricaoSprint.Text = dsTipoStatusItem.Tables[0].Rows[0][0].ToString();
                    }
                }
                decimal custoPrevistoItem = 0;
                bool retornoCusto = decimal.TryParse(dsDadosTela.Tables[0].Rows[0]["ValorCustoItem"].ToString(), out custoPrevistoItem);
                spnCustoPrevisto.Value = custoPrevistoItem;

                memoComentarioCusto.Text = dsDadosTela.Tables[0].Rows[0]["ComentarioCustoItem"].ToString();

                decimal receitaPrevistaItem = 0;
                bool retornoReceita = decimal.TryParse(dsDadosTela.Tables[0].Rows[0]["ValorReceitaItem"].ToString(), out receitaPrevistaItem);
                spnReceitaPrevista.Value = receitaPrevistaItem;

                memoComentarioReceita.Text = dsDadosTela.Tables[0].Rows[0]["ComentarioReceitaItem"].ToString();

                ddlPacoteTrabalhoAssociado.Value = dsDadosTela.Tables[0].Rows[0]["CodigoTarefaReferencia"].ToString();
            }
            IndicaImportacaoTarefaCronograma = (dsDadosTela.Tables[0].Rows[0]["IndicaImportacaoTarefaCronograma"].ToString() == "S");

            string comandoSqlPermissao = string.Format(@"SELECT dbo.f_VerificaAcessoEmAlgumObjeto({0}, NULL, 'PR', {1}, 'PR_IteBkl') as Permissao", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
            DataSet dsPermissao = cDados.getDataSet(comandoSqlPermissao);
            if(acao.Contains("Backlog") == true)
            {
                btnSalvar.ClientEnabled = dsPermissao.Tables[0].Rows[0]["Permissao"].ToString() == "True";
                if (btnSalvar.ClientEnabled == false)
                {
                    btnSalvar.ToolTip = "Usuário sem permissão para alterar item de backlog";
                }
            }
            
            //btnSalvar
        }
    }

    private bool IndicaSeItemDeBacklogEstaEmOutroProjeto()
    {
        string comandoSQL = string.Format(@"SELECT CodigoProjeto, CodigoIteracao
				                             FROM {0}.{1}.Agil_ItemBacklog
                                            WHERE CodigoItem = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoItemBacklog);

        DataSet ds = cDados.getDataSet(comandoSQL);
        int CodigoProjetoItem = -1;
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            CodigoProjetoItem = int.Parse(ds.Tables[0].Rows[0]["CodigoProjeto"].ToString());
        }

        return (idProjeto != CodigoProjetoItem);
    }


    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupItensBacklog.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "popupItensBacklog"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }

    private void defineAlturaFrameAnexos(string resolucaoCliente)
    {

        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaFrameAnexos = alturaUrl - 100;
    }

    private void verificaVisibilidadeObjetos()
    {
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "controlaClientesGestaoAgil");

        ddlClassificacao.JSProperties["cp_Visivel"] = "S";

        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["controlaClientesGestaoAgil"] + "" == "N")
        {
            ddlClassificacao.JSProperties["cp_Visivel"] = "N";
            tdInfoSecundarias.Style.Add(HtmlTextWriterStyle.Display, "none");


            string comandoSQL = "SELECT TOP 1 CodigoTipoClassificacaoItem FROM Agil_TipoClassificacaoItemBacklog";

            DataSet dsClassificacao = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsClassificacao) && cDados.DataTableOk(dsClassificacao.Tables[0]))
                ddlClassificacao.Value = dsClassificacao.Tables[0].Rows[0]["CodigoTipoClassificacaoItem"].ToString();
        }
    }

    private void populaClassificacao()
    {
        DataSet ds = new DataSet();

        ds = cDados.getClassificacaoItensBackLog("");

        ddlClassificacao.DataSource = ds.Tables[0];
        ddlClassificacao.TextField = "DescricaoTipoClassificacaoItem";
        ddlClassificacao.ValueField = "CodigoTipoClassificacaoItem";

        ddlClassificacao.DataBind();

        ddlClassificacao.Items.Insert(0, new ListEditItem("", null));
    }
    #endregion

    #region Provavelmente não será preciso alterar nada aqui.


    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";

        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpCodigo"] = "";
        ((ASPxCallback)source).JSProperties["cpAcao"] = "";

        try
        {

            if (e.Parameter == "incluir" || e.Parameter == "kanbanAgilIncluiTarefaBacklogNaoPlanejada")
            {
                ((ASPxCallback)source).JSProperties["cpAcao"] = "incluir";
                mensagemErro_Persistencia = persisteInclusaoRegistro();
                int intAuxiliar = -1;
                if (int.TryParse(mensagemErro_Persistencia, out intAuxiliar) == true)
                {
                    hfGeral.Set("StatusSalvar", "1");
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Item de backlog incluído com sucesso!";
                    ((ASPxCallback)source).JSProperties["cpCodigo"] = mensagemErro_Persistencia;
                    codigoItemBacklog = intAuxiliar;
                    mensagemErro_Persistencia = "";
                    acao = "kanbanAgilEditaTarefaBacklog";
                }
                FilterItens(ddlRecurso.Items);
            }
            else if (e.Parameter == "incluirTarefasItem")
            {
                mensagemErro_Persistencia = persisteInclusaoRegistroTarefaItem();
                ((ASPxCallback)source).JSProperties["cpAcao"] = "incluir";
                int intAuxiliar = -1;
                if (int.TryParse(mensagemErro_Persistencia, out intAuxiliar) == true)
                {
                    hfGeral.Set("StatusSalvar", "1");
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Tarefa de backlog incluída com sucesso!";
                    ((ASPxCallback)source).JSProperties["cpCodigo"] = mensagemErro_Persistencia;
                    codigoItemBacklog = intAuxiliar;
                    mensagemErro_Persistencia = "";
                }
                FilterItens(ddlRecurso.Items);
            }
            else if (e.Parameter == "kanbanAgilIncluiTarefa")
            {
                ((ASPxCallback)source).JSProperties["cpAcao"] = "incluir";
                mensagemErro_Persistencia = persisteInclusaoRegistrokanbanAgilIncluiTarefa();
                int intAuxiliar = -1;
                if (int.TryParse(mensagemErro_Persistencia, out intAuxiliar) == true)
                {
                    hfGeral.Set("StatusSalvar", "1");
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Tarefa incluída com sucesso!";
                    codigoItemBacklog = intAuxiliar;
                    mensagemErro_Persistencia = "";
                    acao = "kanbanAgilEditaTarefa";
                }
                FilterItens(ddlRecurso.Items);
            }
            else if (e.Parameter.ToString().ToLower().Contains("edita"))
            {
                ((ASPxCallback)source).JSProperties["cpAcao"] = "editar";
                mensagemErro_Persistencia = persisteEdicaoRegistro();
                callbackTela.JSProperties["cpCodigo"] = mensagemErro_Persistencia;
                int intAuxiliar = -1;
                if (int.TryParse(mensagemErro_Persistencia, out intAuxiliar) == true)
                {
                    hfGeral.Set("StatusSalvar", "1");
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Item de backlog alterado com sucesso!";
                    codigoItemBacklog = -1/*intAuxiliar*/;
                    mensagemErro_Persistencia = "";
                }
                FilterItens(ddlRecurso.Items);
            }
            else if (e.Parameter == "Fechar")
            {
                codigoItemBacklog = 0;
                acao = "";
                ((ASPxCallback)source).JSProperties["cpAcao"] = e.Parameter;
            }

        ((ASPxCallback)source).JSProperties["cpErro"] = mensagemErro_Persistencia != "OK" ? mensagemErro_Persistencia : "";

        }
        catch (Exception erro) {
            mensagemErro_Persistencia = erro.Message;
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
            ((ASPxCallback)source).JSProperties["cpErro"] = mensagemErro_Persistencia;
        }
    }

    private string persisteInclusaoRegistrokanbanAgilIncluiTarefa()
    {
        string tituloTarefa = txtTituloItem.Text.Replace("'", "'+char(39)+'");

        string detalhe = txtDetalheItem.Html;

        string esforcoPrevisto = txtEsforco.Text == "" ? "0" : txtEsforco.Text.Replace(',', '.');

        decimal esforcoReal = 0;
        //bool retorno1 = decimal.TryParse((txtEsforcoReal.Value == null) ? "0" : txtEsforcoReal.Value.ToString(), out esforcoReal);


        decimal receitaItem = 0;
        bool retorno = decimal.TryParse((spnReceitaPrevista.Value == null) ? "0" : spnReceitaPrevista.Value.ToString(), out receitaItem);
        string ins_valorReceitaItem = (receitaItem == 0) ? "NULL" : receitaItem.ToString().Replace(".", "").Replace(",", ".");

        int complexidade = 0;//int.Parse((ddlComplexidade.Value == null) ? "-1" : ddlComplexidade.Value.ToString());

        int importancia = int.Parse(txtImportancia.Text == "" ? "0" : txtImportancia.Text);

        int codigoRecurso = (ddlRecurso.Value == null) ? -1 : int.Parse(ddlRecurso.Value.ToString());

        string dataAlvo = (dtDataAlvo.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", dtDataAlvo.Date));

        int classificacao = int.Parse((ddlClassificacao.Value != null) ? ddlClassificacao.Value.ToString() : "-1");

        int codigoItem = -1;
        bool retorno2 = int.TryParse((Request.QueryString["CI"] + ""), out codigoItem);
        string codigoRecursoAlocado = (ddlRecurso.Value != null) ? ddlRecurso.Value.ToString() : "NULL";

        int codigoRaia = -1;
        retorno = int.TryParse((Request.QueryString["CR"] + ""), out codigoRaia);

        string comentarioCustoItem = memoComentarioCusto.Text;
        string comentarioReceitaItem = memoComentarioReceita.Text;

        decimal custo = 0;
        decimal.TryParse(spnCustoPrevisto.Value.ToString(), out custo);
        string ins_valorCustoItem = (custo == 0) ? "NULL" : custo.ToString().Replace(".", "").Replace(",", ".");


        string comandoSQL = "declare @codigoItem Int , @l_CodigoStatusItem Int";

        comandoSQL += string.Format(@"            
	            
      INSERT INTO [dbo].[Agil_ItemBacklog]
                   ([CodigoProjeto],[CodigoItemSuperior],[TituloItem], [DetalheItem],[CodigoTipoStatusItem],[CodigoTipoClassificacaoItem]  ,[CodigoUsuarioInclusao],[DataInclusao] ,[CodigoIteracao] ,[Importancia] ,[Complexidade] ,[EsforcoPrevisto] ,[IndicaItemNaoPlanejado] ,[IndicaQuestao],[IndicaBloqueioItem],[IndicaTarefa] ,[CodigoRaia] ,[CodigoRecursoAlocado], [EsforcoReal], [DataAlvo], [ValorReceitaItem],[ComentarioCustoItem],[ComentarioReceitaItem],[ValorCustoItem] )
            SELECT [CodigoProjeto] ,{0}                 ,'{1}'       ,'{3}'         , {7}                  ,{8}                            ,{4}                    ,GetDate()      ,[CodigoIteracao] ,{9}           ,          {10} ,{2}               ,'N'                      ,'N'            ,'N'                 ,'S'            ,{5}          , {6}                  , {11}         , {12}      , {13}             ,'{14}'               ,'{15}'                 ,{16}
              FROM [dbo].[Agil_ItemBacklog]
             WHERE CodigoItem = {0};

            SELECT @codigoItem = SCOPE_IDENTITY()

            SELECT @codigoItem AS id_retorno; 
SELECT @l_CodigoStatusItem = CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI'

            EXEC dbo.p_Agil_ReadequaImplementacaoItem @codigoItem, {4}, '', {7}
",/*{0}*/
            codigoItem
         , /*{1}*/tituloTarefa
         , /*{2}*/esforcoPrevisto
         , /*{3}*/detalhe
         , /*{4}*/codigoUsuarioResponsavel
         ,/*{5}*/ codigoRaia
         ,/*{6}*/ codigoRecursoAlocado
         ,/*{7}*/ " @l_CodigoStatusItem"
         ,/*{8}*/ (classificacao == -1) ? "NULL" : classificacao.ToString()
         ,/*{9}*/ importancia
         ,/*{10}*/ complexidade
         ,/*{11}*/esforcoReal
         ,/*{12}*/dataAlvo
         ,/*{13}*/ ins_valorReceitaItem
         ,/*{14}*/comentarioCustoItem
         ,/*{15}*/comentarioReceitaItem
         ,/*{16}*/ins_valorCustoItem);
        string retornoID = "";
        DataSet result = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(result) && cDados.DataTableOk(result.Tables[0]))
        {
            retornoID = result.Tables[0].Rows[0][0].ToString();

            if (tagBox.Tokens.Count > 0)
            {
                string insereTags = "";
                for (int i = 0; i < tagBox.Tokens.Count; i++)
                {
                    insereTags += string.Format(@" INSERT INTO [dbo].[Agil_TagItemBackLog] ([CodigoItem],[TextoTag]) VALUES({0},'{1}'); ", retornoID, tagBox.Tokens[i].ToString());
                }
                int regAfetados = 0;
                bool retornoInsere = false;
                try
                {
                    retornoInsere = cDados.execSQL(insereTags, ref regAfetados);
                }
                catch (Exception ex)
                {
                }
            }
            callbackTela.JSProperties["cpCodigo"] = retornoID;
            retorno_popup.Value = retornoID;
            return (retornoID);
        }
        else
        {
            return "Erro ao salvar o registro!";
        }

    }

    private string persisteInclusaoRegistro()
    {
        string msgErro = "";

        string tituloItem = txtTituloItem.Text.Replace("'", "'+char(39)+'");
        string detalheItem = txtDetalheItem.Html;

        int complexidade = 0;//int.Parse((ddlComplexidade.Value == null) ? "-1" : ddlComplexidade.Value.ToString());

        int classificacao = int.Parse((ddlClassificacao.Value != null) ? ddlClassificacao.Value.ToString() : "-1");
        int importancia = int.Parse(txtImportancia.Text == "" ? "0" : txtImportancia.Text);

        int codigoRecurso = (ddlRecurso.Value == null) ? -1 : int.Parse(ddlRecurso.Value.ToString());
        string dataAlvo = (dtDataAlvo.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", dtDataAlvo.Date));

        decimal receitaPrevista = 0;
        bool retorno = decimal.TryParse((spnReceitaPrevista.Value == null) ? "0" : spnReceitaPrevista.Value.ToString(), out receitaPrevista);

        decimal custoItem = 0;
        retorno = decimal.TryParse((spnCustoPrevisto.Value == null) ? "0" : spnCustoPrevisto.Value.ToString(), out custoItem);

        decimal esforcoPrevisto = 0;
        bool retorno1 = decimal.TryParse((txtEsforco.Value == null) ? "0" : txtEsforco.Value.ToString(), out esforcoPrevisto);

        decimal esforcoReal = 0;
        retorno1 = true;//decimal.TryParse((txtEsforcoReal.Value == null) ? "0" : txtEsforcoReal.Value.ToString(), out esforcoReal);

        string comentarioCustoItem = memoComentarioCusto.Text.Replace("'", "'+char(39)+'");
        string comentarioReceitaItem = memoComentarioReceita.Text.Replace("'", "'+char(39)+'");

        int? codigoIteracao = null;
        if (acao == "kanbanAgilIncluiTarefaBacklogNaoPlanejada")
        {
            string comandoSELECT = string.Format(@"SELECT CodigoIteracao FROM Agil_Iteracao WHERE CodigoProjetoIteracao = " + Request.QueryString["IDProjeto"] + "");
            DataSet dsCodigoIteracao = cDados.getDataSet(comandoSELECT);
            if (cDados.DataSetOk(dsCodigoIteracao) && cDados.DataTableOk(dsCodigoIteracao.Tables[0]))
            {
                codigoIteracao = int.Parse(dsCodigoIteracao.Tables[0].Rows[0][0].ToString());
            }

        }

        DataSet dsRetorno = incluiItensBackLog(idProjeto, tituloItem, detalheItem, classificacao, codigoUsuarioResponsavel, importancia, complexidade, esforcoPrevisto, esforcoReal, codigoCronogramaProjeto, codigoRecurso, dataAlvo, receitaPrevista, codigoIteracao, custoItem, comentarioCustoItem, comentarioReceitaItem);
        msgErro = dsRetorno.Tables[0].Rows[0][0].ToString();
        return msgErro;
    }

    private string persisteInclusaoRegistroTarefaItem()
    {

        string codigoRaia1 = "NULL";

        string comandoSQLGetCodigoRaia = string.Format(@"
        SELECT TOP 1 CodigoRaia  
        FROM Agil_RaiasIteracao 
        WHERE CodigoIteracao = {0} 
        ORDER BY PercentualConcluido ASC", getCodigoIteracao());
        DataSet dsCodigoRaia = cDados.getDataSet(comandoSQLGetCodigoRaia);
        if (cDados.DataSetOk(dsCodigoRaia) && cDados.DataTableOk(dsCodigoRaia.Tables[0]))
        {
            codigoRaia1 = dsCodigoRaia.Tables[0].Rows[0]["CodigoRaia"].ToString();
        }
        //CI=227&acao=incluirTarefasItem&IDProjeto=1265&CPA=1226
        string tituloTarefa = txtTituloItem.Text.Replace("'", "'+char(39)+'");
        string esforcoPrevisto = txtEsforco.Text == "" ? "0" : txtEsforco.Text.Replace(',', '.');
        string detalhe = txtDetalheItem.Html;
        int codigoItem = -1;
        bool retorno = int.TryParse((Request.QueryString["CI"] + ""), out codigoItem);
        string comandoSQL = string.Format(@"            
	            INSERT INTO [dbo].[Agil_ItemBacklog]
                   ([CodigoProjeto]
                   ,[CodigoItemSuperior]
                   ,[TituloItem]
                   ,[DetalheItem]
                   ,[CodigoTipoStatusItem]
                   ,[CodigoTipoClassificacaoItem]
                   ,[CodigoUsuarioInclusao]
                   ,[DataInclusao]
                   ,[CodigoIteracao]
                   ,[Importancia]
                   ,[Complexidade]
                   ,[EsforcoPrevisto]
                   ,[IndicaItemNaoPlanejado]
                   ,[IndicaQuestao]
                   ,[IndicaBloqueioItem]
                   ,[IndicaTarefa]
                   ,[CodigoRaia])
            SELECT [CodigoProjeto]
                  ,{0}
                  ,'{1}'
                  ,'{3}'
                  ,[CodigoTipoStatusItem]
                  ,[CodigoTipoClassificacaoItem]
                  ,{4}
                  ,GetDate()
                  ,[CodigoIteracao]
                  ,[Importancia]
                  ,[Complexidade]
                  ,{2}
                  ,'N'
                  ,'N'
                  ,'N'
                  ,'S'
                  ,{5}
              FROM [dbo].[Agil_ItemBacklog]
             WHERE CodigoItem = {0}
            
            SELECT SCOPE_IDENTITY() AS id_retorno

            ", codigoItem
             , tituloTarefa
             , esforcoPrevisto
             , detalhe
             , codigoUsuarioResponsavel
             , codigoRaia1
             , Request.QueryString["CPA"]);

        int codigoitem = -1;
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int.TryParse(ds.Tables[0].Rows[0]["id_retorno"] + "", out codigoitem);
        }
        return codigoitem.ToString();
    }


    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria

        string msgErro = "";

        string tituloItem = txtTituloItem.Text.Replace("'", "'+char(39)+'");
        string detalheItem = txtDetalheItem.Html;
        int complexidade = 0;// int.Parse((ddlComplexidade.Value == null) ? "-1" : ddlComplexidade.Value.ToString());
        int classificacao = int.Parse((ddlClassificacao.Value != null) ? ddlClassificacao.Value.ToString() : "-1");
        int importancia = int.Parse(txtImportancia.Text == "" ? "0" : txtImportancia.Text);
        int codigoRecurso = (ddlRecurso.Value == null) ? -1 : int.Parse(ddlRecurso.Value.ToString());
        string dataAlvo = (dtDataAlvo.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", dtDataAlvo.Date));

        // formato para valores em decimais;
        CultureInfo ciEN = new CultureInfo("en-US", false);


        decimal receitaPrevista = 0;
        bool retorno = decimal.TryParse((spnReceitaPrevista.Value == null) ? "0" : spnReceitaPrevista.Value.ToString(), out receitaPrevista);

        decimal esforcoPrevisto = 0;
        retorno = decimal.TryParse((txtEsforco.Value == null) ? "0" : txtEsforco.Value.ToString(), out esforcoPrevisto);

        decimal esforcoReal = 0;
        retorno = true;// decimal.TryParse((txtEsforcoReal.Value == null) ? "0" : txtEsforcoReal.Value.ToString(), out esforcoReal);

        string comentarioReceitaItem = memoComentarioReceita.Text.Replace("'", "'+char(39)+'");
        string comentarioCustoItem = memoComentarioCusto.Text.Replace("'", "'+char(39)+'");

        decimal valorCustoItem = decimal.Parse(spnCustoPrevisto.Value.ToString());

        DataSet dsRetorno = atualizaItensDoBackLog(codigoItemBacklog, idProjeto, tituloItem, detalheItem, classificacao, codigoUsuarioResponsavel, importancia, complexidade, esforcoPrevisto, esforcoReal, codigoCronogramaProjeto, codigoRecurso, dataAlvo, receitaPrevista, comentarioReceitaItem, valorCustoItem, comentarioCustoItem);

        msgErro = dsRetorno.Tables[0].Rows[0][0].ToString();

        return msgErro;
    }



    #endregion


    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "incluiItemBacklog(); TipoOperacao = 'Incluir';", true, true, false, "PR_IteBkl", "Itens do Backlog", this);



        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                this.Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            this.Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            this.Response.End();
        }

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }

        DevExpress.Web.MenuItem btnExportar = ((ASPxMenu)sender).Items.FindByName("btnExportar");

        btnExportar.ClientVisible = true;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = "Exportar para XLS";
        }





        DevExpress.Web.MenuItem btnIncluir = ((ASPxMenu)sender).Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = podeIncluir;

        btnIncluir.ClientVisible = true;

        if (podeIncluir == false)
            btnIncluir.Image.Url = "~/imagens/botoes/incluirRegDes.png";





        string clickBotaoExportar = "";

        if (exportaOLAPTodosFormatos)
            clickBotaoExportar = @"
            else if(e.item.name == 'btnExportar')
	        {
                e.processOnServer = false;		                                        
	        }";

        ((ASPxMenu)sender).ClientSideEvents.ItemClick =
        @"function(s, e){ 
debugger
            e.processOnServer = false;

            if(e.item.name == 'btnIncluir')
            {
                incluiItemBacklog(); TipoOperacao = 'Incluir';
            }" + clickBotaoExportar + @"	
           else if(e.item.name == 'btnSeletorColunas')
           {
               e.processOnServer = false; 
               tlDados.ShowCustomizationWindow();
            }

	        else if(e.item.name != 'btnLayout')
	        {
                e.processOnServer = true;		                                        
	        }	

        }";





        DevExpress.Web.MenuItem btnLayout = ((ASPxMenu)sender).Items.FindByName("btnLayout");

        btnLayout.ClientVisible = false;

        if (false && !this.IsPostBack)
        {
            DataSet ds = cDados.getDataSet("SELECT 1 FROM Lista WHERE CodigoEntidade = " + cDados.getInfoSistema("CodigoEntidade") + " AND IniciaisListaControladaSistema = 'PR_IteBkl'");

            if (ds.Tables[0].Rows.Count == 0)
            {
                int regAf = 0;

                cDados.execSQL(cDados.constroiInsertLayoutColunas((((ASPxMenu)sender).Parent as GridViewHeaderTemplateContainer).Grid, "PR_IteBkl", "Itens do Backlog"), ref regAf);
            }

            cDados.InitData((((ASPxMenu)sender).Parent as GridViewHeaderTemplateContainer).Grid, "PR_IteBkl");
        }
    }

    protected void menu_ItemClick1(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.exportaTreeList(ASPxTreeListExporter1, "XLS");
    }

    public DataSet incluiItensBackLog(int CodigoProjeto, string TituloItem, string DetalheItem,
        int CodigoTipoClassificacaoItem, int CodigoUsuarioInclusao, int Importancia, int Complexidade,
        decimal EsforcoPrevisto, decimal EsforcoReal, string CodigoCronogramaProjetoReferencia, int codigoRecurso, string dataAlvo, decimal receitaPrevista, int? codigoIteracao, decimal valorCustoItem, string comentarioCustoItem, string comentarioReceitaItem)
    {
        string ins_codigoRecurso = (codigoRecurso == -1) ? "NULL" : codigoRecurso.ToString();
        string ins_DataAlvo = dataAlvo;
        string ins_receitaPrevista = (receitaPrevista == 0) ? "NULL" : receitaPrevista.ToString().Replace(".", "").Replace(",", ".");
        string ins_EsforcoPrevisto = EsforcoPrevisto.ToString().Replace(".", "").Replace(",", ".");
        string ins_EsforcoReal = (EsforcoReal == 0) ? "NULL" : EsforcoReal.ToString().Replace(".", "").Replace(",", ".");
        string ins_valorCustoItem = (valorCustoItem == 0) ? "NULL" : valorCustoItem.ToString().Replace(".", "").Replace(",", ".");

        string codigoItemSuperior = "NULL";

        string incluiTAGS = "";

        if (tagBox.Tokens.Count > 0)
        {
            for (int i = 0; i < tagBox.Tokens.Count; i++)
            {
                incluiTAGS += string.Format(@" INSERT INTO [dbo].[Agil_TagItemBackLog] ([CodigoItem],[TextoTag]) VALUES(@codigoItem,'{0}') ", tagBox.Tokens[i].ToString());
            }
        }

        string strSelectTipoStatusItem = "";
        if (acao == "incluir")
        {
            strSelectTipoStatusItem = @"(SELECT CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL')";
        }
        else if (acao == "kanbanAgilIncluiTarefaBacklogNaoPlanejada")
        {
            strSelectTipoStatusItem = @"(SELECT CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL')";
        }



        string comandoSQL = string.Format(@"
	 DECLARE @codigoItem INT            
       INSERT INTO {0}.{1}.[Agil_ItemBacklog]
                     ([CodigoProjeto]        ,[TituloItem]                       ,[DetalheItem]        ,[CodigoTipoStatusItem]     ,[CodigoTipoClassificacaoItem],
                      [CodigoUsuarioInclusao],[DataInclusao]                     ,[PercentualConcluido],[Importancia]              ,[Complexidade]               ,
                      [EsforcoPrevisto]      ,[CodigoCronogramaProjetoReferencia],[CodigoRecursoAlocado]       ,[DataAlvo]         ,[ValorReceitaItem]           , [CodigoItemSuperior], [IndicaTarefa], [EsforcoReal], [CodigoIteracao], [IndicaBloqueioItem],
                      [ValorCustoItem]       ,[ComentarioCustoItem]              ,[ComentarioReceitaItem] )
                
               VALUES({2}                    ,'{3}'                              ,'{4}'                ,{5}                        ,{6},                    
                      {7}                    ,GETDATE()                          ,0.0                  ,{8}                        ,{9},
                      {10}                   ,'{11}'                             ,{12}                 ,{13}                       ,{14}                         ,{15}                ,  'N'            ,{17}        ,{18}             , '{19}',
                      {20}                   ,'{21}'                             ,'{22}')

        Select @codigoItem = SCOPE_IDENTITY()



 {16}


         SELECT @codigoItem AS codigoItem
"



            , /*{0}*/cDados.getDbName()
            , /*{1}*/cDados.getDbOwner()
            , /*{2}*/CodigoProjeto
            , /*{3}*/TituloItem
            , /*{4}*/DetalheItem
            , /*{5}*/strSelectTipoStatusItem
            , /*{6}*/(CodigoTipoClassificacaoItem == 0 || CodigoTipoClassificacaoItem == -1) ? "NULL" : CodigoTipoClassificacaoItem.ToString()
            , /*{7}*/CodigoUsuarioInclusao
            , /*{8}*/Importancia
            , /*{9}*/Complexidade
            , /*{10}*/ins_EsforcoPrevisto
            , /*{11}*/CodigoCronogramaProjetoReferencia
            , /*{12}*/ins_codigoRecurso
            , /*{13}*/ins_DataAlvo
            , /*{14}*/ins_receitaPrevista
            , /*{15}*/codigoItemSuperior
            , /*{16}*/incluiTAGS
            , /*{17}*/ins_EsforcoReal
            , /*{18}*/codigoIteracao.HasValue ? codigoIteracao.ToString() : "NULL"
            , /*{19}*/"N"
            ,/*{20}*/ins_valorCustoItem
            ,/*{21}*/comentarioCustoItem
            ,/*{22}*/comentarioReceitaItem);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    public DataSet atualizaItensDoBackLog(int CodigoItem, int codigoProjeto, string tituloItem, string detalheItem, int CodigoTipoClassificacaoItem
           , int CodigoUsuarioUltimaAlteracao, int Importancia, int Complexidade, decimal EsforcoPrevisto, decimal EsforcoReal, string CodigoCronogramaProjetoReferencia, int codigoRecurso, string dataAlvo, decimal valorReceitaItem, string comentarioReceitaItem, decimal valorCustoItem, string comentarioCustoItem)
    {

        string atualizaTAGS = "";

        string ins_valorReceitaItem = (valorReceitaItem == 0) ? "NULL" : valorReceitaItem.ToString().Replace(".", "").Replace(",", ".");
        string ins_EsforcoPrevisto = (txtEsforco.Text == "") ? "0" : EsforcoPrevisto.ToString().Replace(".", "").Replace(",", ".");
        string ins_EsforcoReal = (EsforcoReal == 0) ? "NULL" : EsforcoReal.ToString().Replace(".", "").Replace(",", ".");
        string ins_valorCustoItem = (valorCustoItem == 0) ? "NULL" : valorCustoItem.ToString().Replace(".", "").Replace(",", ".");
        string comandoFinal = string.Format(@" select {0} ", CodigoItem);


        string inicioComandoUpdate = string.Format(@"

BEGIN 
                        DECLARE @Erro INT
                        DECLARE @MensagemErro nvarchar(2048)
                        DECLARE @CodigoRetorno INT
                        SET @Erro = 0
                        BEGIN TRAN
                            BEGIN TRY

UPDATE {0}.{1}.[Agil_ItemBacklog]", cDados.getDbName(), cDados.getDbOwner());
        string sets = " SET ";
        //sets += string.Format(" [CodigoProjeto] = {0} ", codigoProjeto);
        sets += string.Format(" [TituloItem] = '{0}' ", tituloItem);
        sets += string.Format(" ,[DetalheItem] = '{0}' ", detalheItem);
        //sets += string.Format(" ,[CodigoTipoStatusItem] = (SELECT CodigoTipoStatusItem from Agil_TipoStatusItemBacklog where IniciaisTipoStatusItemControladoSistema =  'AG_IMPL') ");
        sets += string.Format(" ,[CodigoTipoClassificacaoItem] = {0} ", (CodigoTipoClassificacaoItem == -1) ? "NULL" : CodigoTipoClassificacaoItem.ToString());
        sets += string.Format(" ,[CodigoUsuarioUltimaAlteracao] = {0} ", CodigoUsuarioUltimaAlteracao);
        sets += " ,[DataUltimaAlteracao] = getdate() ";
        sets += string.Format(" ,[Importancia] = {0} ", Importancia);
        sets += string.Format(" ,[Complexidade] = {0} ", Complexidade);
        sets += string.Format(" ,[EsforcoPrevisto] = {0} ", ins_EsforcoPrevisto);
        sets += string.Format(" ,[EsforcoReal] = {0} ", ins_EsforcoReal);
        sets += string.Format(" ,[CodigoCronogramaProjetoReferencia] = '{0}' ", CodigoCronogramaProjetoReferencia);
        sets += (codigoRecurso == -1) ? " ,[CodigoRecursoAlocado] = NULL " : string.Format(" ,[CodigoRecursoAlocado] = {0} ", codigoRecurso);
        sets += string.Format(" ,[DataAlvo] = {0} ", dataAlvo);

        sets += string.Format(" ,[ValorReceitaItem] = {0} ", ins_valorReceitaItem);
        sets += string.Format(@", [ComentarioReceitaItem] = '{0}'", comentarioReceitaItem);
        sets += string.Format(@", [valorCustoItem] = {0}", ins_valorCustoItem);
        sets += string.Format(@", [ComentarioCustoItem] = '{0}'", comentarioCustoItem);
        sets += string.Format(@", [CodigoTarefaReferencia] = {0}", (ddlPacoteTrabalhoAssociado.Value != null && !string.IsNullOrEmpty(ddlPacoteTrabalhoAssociado.Value.ToString())) ? ddlPacoteTrabalhoAssociado.Value.ToString() : "NULL");

        string wheres = "";

        wheres += string.Format(" WHERE CodigoItem = {0} ", CodigoItem);

        atualizaTAGS = string.Format("DELETE Agil_TagItemBackLog WHERE codigoitem = {0}", CodigoItem);

        if (tagBox.Tokens.Count > 0)
        {
            for (int i = 0; i < tagBox.Tokens.Count; i++)
                atualizaTAGS += string.Format(@" INSERT INTO [dbo].[Agil_TagItemBackLog] ([CodigoItem],[TextoTag]) VALUES({0},'{1}') ", CodigoItem, tagBox.Tokens[i]);
        }

        atualizaTAGS += string.Format(@"
         END TRY
	                    BEGIN CATCH
		                    SET @Erro = ERROR_NUMBER()
		                    SET @MensagemErro = ERROR_MESSAGE()
	                    END CATCH

	                    IF @Erro = 0
	                    BEGIN
		                    SELECT {0} AS ErrorMessage;
		                    COMMIT
	                    END
	                    ELSE
	                    BEGIN
		                    SELECT @MensagemErro AS ErrorMessage;
		                    ROLLBACK
	                    END
                    END 
", CodigoItem);

        string sincronizaItensClonados = string.Format(@"

DECLARE @RC int
DECLARE @in_CodigoItem int
    SET @in_CodigoItem = {0}

EXECUTE @RC = [dbo].[p_Agil_SincronizaItensClonados] 
   @in_CodigoItem
", CodigoItem);


        DataSet ds = cDados.getDataSet(inicioComandoUpdate + sets + wheres + atualizaTAGS + sincronizaItensClonados);
        return ds;

    }

    protected void tlDados_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if (e.Column.FieldName == "TagItem")
        {
            if (e.CellValue != null && e.CellValue.ToString() != "")
            {
                e.Cell.Text = e.CellValue.ToString().Replace("|", ", ");
            }
        }
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "TagItem")
        {
            if (e.TextValue != null && e.TextValue.ToString() != "")
            {
                e.Text = e.TextValue.ToString().Replace("|", ", ");
                e.TextValue = e.Text;
            }
        }
    }

    protected void tlDados_FocusedNodeChanged(object sender, EventArgs e)
    {
        if (((ASPxTreeList)sender).FocusedNode != null)
        {
            string tituloItem = ((ASPxTreeList)sender).FocusedNode.GetValue("TituloItem").ToString();
            ((ASPxTreeList)sender).JSProperties["cpNoSelecionado"] = tituloItem;
        }
    }

    protected void callbackBotaoFechar_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpCodigo"] = codigoItemBacklog;
        codigoItemBacklog = -1;
        acao = "";
    }
    
    protected void hfRecursoSelecionadoInit_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        Session["CodigoRecursoCorporativo"] = null;
        foreach (ListEditItem item in ddlRecurso.Items)
        {
            if (item.Selected)
                Session["CodigoRecursoCorporativo"] = item.Value;
        }
    }

    protected void FilterItens(ListEditItemCollection items)
    {
        try
        {
            foreach (ListEditItem item in items)
            {
                if (item.Selected)
                {
                    if (item.Value != null)
                        if (Session["CodigoRecursoCorporativo"] != null && Session["CodigoRecursoCorporativo"].ToString() != "")
                        {
                            if (Session["CodigoRecursoCorporativo"].ToString() != item.Value.ToString())
                                cDados.envioEmailRecursoCorporativo(Session["CodigoItem"].ToString(), Session["TituloItem"].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), Session["NomeUsuario"].ToString(), item.Value.ToString());
                        }
                        else
                            cDados.envioEmailRecursoCorporativo(Session["CodigoItem"].ToString(), Session["TituloItem"].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), Session["NomeUsuario"].ToString(), item.Value.ToString());
                }
            }
        }
        catch
        {
            throw new Exception(
                                "Itens:" + items.Count() +
                                " Codigo Recurso: " + Session["CodigoRecursoCorporativo"].ToString() +
                                " Código Item: " + Session["CodigoItem"].ToString() +
                                " Título item: " + Session["TituloItem"].ToString() +
                                " Nome Usuário: " + Session["NomeUsuario"].ToString()
                                );
        }
    }

    protected void tabControl_Load(object sender, EventArgs e)
    {
        var parametros = new[] { "URLBRISKIntegracao", "TokenAutenticacaoIntegracao", "TFS_URLClienteIntegracao" };
        var ds = cDados.getParametrosSistema(parametros);
        var dr = ds.Tables[0].AsEnumerable().SingleOrDefault();
        var parametrosValidos = parametros.All(p => !(dr.IsNull(p) || string.IsNullOrWhiteSpace(dr.Field<string>(p))));
        tabControl.TabPages.FindByName("tabLinks").ClientVisible = parametrosValidos;
    }
}

