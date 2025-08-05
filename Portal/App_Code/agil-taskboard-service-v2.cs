using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services;

/// <summary>
/// Summary description for agil_taskboard_service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class agil_taskboard_service_v2 : System.Web.Services.WebService
{
    dados cDados;
    string codigoEntidade;
    string idUsuarioLogado;
    JsonSerializerSettings settings;

    public agil_taskboard_service_v2()
    {
        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        settings = new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" };

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true, MessageName = "obter-itens-backlog")]
    public string ObterItensBacklog(int codigoProjeto, string pesquisaTexto)
    {
        //fazer inner join para pegar IniciaisUsuarioRecurso
        string where = string.Format("'%{0}%'", pesquisaTexto);
        string sql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoProjeto int
        DECLARE @in_PesquisaTexto varchar(250)

        SET @in_codigoProjeto = {0}
        SET @in_PesquisaTexto = {1}

        EXECUTE @RC = [dbo].[p_Agil_ItensBacklogPlanejamentoSprint]  @in_codigoProjeto, @in_PesquisaTexto
            ", codigoProjeto, where);


        var output = JsonConvert.SerializeObject(cDados.getDataSet(sql).Tables[0], settings);
        return output;

    }
            
    [WebMethod(EnableSession = true, MessageName = "obter-detalhes-item")]
    public string ObterDetalhesItem(int codigoItem)
    {
        //fazer inner join para pegar IniciaisUsuarioRecurso
        string where = string.Format(" WHERE ib.CodigoItem = {0}", codigoItem);
        string sql = string.Format(@"
        BEGIN
            DECLARE @tabelaItens AS TABLE(
            CodigoItem  int,
            textoTag  varchar(130)
            );

              DECLARE @textoTagItem AS VARCHAR(max)

              DECLARE crsWork CURSOR LOCAL FAST_FORWARD FOR   
              SELECT TextoTag, CodigoItem 
                FROM Agil_TagItemBackLog 
               WHERE CodigoItem = {3}

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
              ,ISNULL(ib.[CodigoIteracao], 0) AS CodigoIteracao
              ,ib.[Importancia]
              ,ib.[Complexidade]
              ,(SELECT CASE ib.[Complexidade]
                 WHEN 0 THEN 'Baixa'
                 WHEN 1 THEN 'Média'
                 WHEN 2 THEN 'Alta'
                 WHEN 3 THEN 'Muito Alta'
               END) AS DescricaoComplexidade
              ,ib.[EsforcoPrevisto]
              ,ib.[IndicaItemNaoPlanejado]
              ,ib.[IndicaQuestao]
              ,ib.[IndicaBloqueioItem]
              ,ib.[CodigoWorkflow]
              ,ib.[CodigoInstanciaWF]
              ,ib.[CodigoCronogramaProjetoReferencia]
              ,ib.[CodigoTarefaReferencia]
              ,p.CodigoRecursoCorporativo as CodigoCliente
              ,p.NomeRecursoCorporativo as NomeCliente
              ,ttf.CodigoTipoTarefaTimeSheet
              ,ttf.DescricaoTipoTarefaTimeSheet
              ,ib.ReceitaPrevista
              ,(SELECT p.NomeProjeto  
                  FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                       {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                       {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                 WHERE ai.CodigoItem = ib.[CodigoItem]) as Sprint
             ,ib.DataAlvo
             ,(SELECT TextoTag FROM @tabelaItens WHERE CodigoItem = ib.CodigoItem) as TagItem
             ,(SELECT TOP 1 ibs.CodigoItem 
                                      FROM Agil_ItemBacklog ibs 
                                      WHERE ibs.CodigoItemSuperior = ib.CodigoItem)  as IndicaSeItemBacklogTemTarefaAssociada
             ,isnull(u.IniciaisNomeUsuario, (select SUBSTRING(us1.NomeUsuario,1,1) from Usuario us1 where us1.CodigoUsuario = rc.codigoUsuario )) as IniciaisUsuarioRecurso        
             ,isnull(u.[nomeUsuario],(select us1.NomeUsuario from Usuario us1 where us1.CodigoUsuario = rc.codigoUsuario )) as NomeUsuarioRecurso
             ,( SELECT CASE ( SELECT COUNT(1)   
                     FROM AnexoAssociacao AS aa INNER JOIN  
           Anexo AS ax ON (aa.CodigoAnexo = ax.CodigoAnexo  
                   AND ax.DataExclusao IS NULL)  
        WHERE aa.CodigoObjetoAssociado = ib.CodigoItem   
       AND aa.CodigoTipoAssociacao = dbo.f_GetCodigoTipoAssociacao('IB')  
       ) WHEN 0 THEN 0 ELSE 1 END ) AS PossuiAnexo
       , ( SELECT Count(1)   
    FROM ComentarioObjeto AS co INNER JOIN  
         LinkObjeto AS lo ON (co.CodigoComentario = lo.CodigoObjetoLink  
                    AND lo.CodigoTipoObjetoLink = dbo.f_GetCodigoTipoAssociacao('CN')  
        AND lo.CodigoObjeto = ib.CodigoItem  
        AND lo.CodigoTipoObjeto = dbo.f_GetCodigoTipoAssociacao('IB')) ) AS QuantComentario 
      ,( SELECT Count(1)  
    FROM Agil_TarefaChecklist AS tc  
   WHERE tc.CodigoItem = ib.CodigoItem) AS QuantChecklistTotal
    ,( SELECT Count(1)  
    FROM Agil_TarefaChecklist AS tc  
   WHERE tc.CodigoItem = ib.CodigoItem  
     AND IndicaConcluido = 1) AS QuantChecklistConcluido

    ,(SELECT textotag          
        FROM (SELECT textotag, ROW_NUMBER()          
        OVER (ORDER BY codigoitem ASC) AS ROWNUMBER          
        FROM dbo.agil_tagitembacklog          
        WHERE codigoitem = ib.codigoitem) AS a          
        WHERE rownumber = 1)  AS Tag1
    
    ,(SELECT textotag          
      FROM (SELECT textotag, ROW_NUMBER()          
      OVER (ORDER BY codigoitem ASC) AS ROWNUMBER          
      FROM dbo.agil_tagitembacklog          
      WHERE codigoitem = ib.codigoitem) AS a          
      WHERE rownumber = 2) AS Tag2          
      
    ,(SELECT textotag          
        FROM (SELECT textotag, ROW_NUMBER()          
        OVER (ORDER BY codigoitem ASC) AS ROWNUMBER          
        FROM dbo.agil_tagitembacklog          
       WHERE codigoitem = ib.codigoitem) AS a          
       WHERE rownumber = 3) AS Tag3 

         FROM {0}.{1}.[Agil_ItemBacklog] ib
		 left join {0}.{1}.[Agil_ItemBacklog] ibSUP on (ibSUP.CodigoItem = ib.CodigoItemSuperior)
         LEFT JOIN {0}.{1}.Agil_TipoStatusItemBacklog ts on (ib.CodigoTipoStatusItem = ts.CodigoTipoStatusItem)
         LEFT JOIN {0}.{1}.Agil_TipoClassificacaoItemBacklog tc on (ib.CodigoTipoClassificacaoItem = tc.CodigoTipoClassificacaoItem)
         LEFT JOIN {0}.{1}.RecursoCorporativo p on (p.CodigoRecursoCorporativo = ib.CodigoPessoa)
         LEFT JOIN {0}.{1}.TipoTarefaTimeSheet AS ttf on (ttf.CodigoTipoTarefaTimeSheet = ib.CodigoTipoTarefaTimesheet) 
         LEFT JOIN {0}.{1}.[RecursoCorporativo] AS rc ON (rc.CodigoRecursoCorporativo = ib.CodigoRecursoAlocado)          
         LEFT JOIN {0}.{1}.[usuario] AS u  ON (U.[codigousuario] = rc.[CodigoUsuario])   
{2}
        ORDER BY ib.[Importancia] desc
        END
            ", cDados.getDbName(), cDados.getDbOwner(), where, codigoItem);


        var output = JsonConvert.SerializeObject(cDados.getDataSet(sql).Tables[0], settings);
        return output;

    }

    [WebMethod(EnableSession = true, MessageName = "obter-itens-backlog-por-iteracao")]
    public string ObterItensBacklogPorIteracao(int codigoProjeto, int codigoIteracao)
    {
        string where = string.Format(" and ib.[CodigoIteracao] = '{0}'", codigoIteracao);
        string sql = string.Format(@"
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
		                              WHERE CodigoProjeto = {2})

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
              ,ib.[IndicaItemNaoPlanejado]
              ,ib.[IndicaQuestao]
              ,ib.[IndicaBloqueioItem]
              ,ib.[CodigoWorkflow]
              ,ib.[CodigoInstanciaWF]
              ,ib.[CodigoCronogramaProjetoReferencia]
              ,ib.[CodigoTarefaReferencia]
              ,p.CodigoRecursoCorporativo as CodigoCliente
              ,p.NomeRecursoCorporativo as NomeCliente
              ,ttf.CodigoTipoTarefaTimeSheet
              ,ttf.DescricaoTipoTarefaTimeSheet
              ,ib.ReceitaPrevista
              ,(SELECT p.NomeProjeto  
                  FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                       {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                       {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                 WHERE ai.CodigoItem = ib.[CodigoItem]) as Sprint
             ,ib.DataAlvo
             ,(SELECT TextoTag FROM @tabelaItens WHERE CodigoItem = ib.CodigoItem) as TagItem
             ,(SELECT TOP 1 ibs.CodigoItem 
                                      FROM Agil_ItemBacklog ibs 
                                      WHERE ibs.CodigoItemSuperior = ib.CodigoItem)  as IndicaSeItemBacklogTemTarefaAssociada

         FROM {0}.{1}.[Agil_ItemBacklog] ib
		 left join {0}.{1}.[Agil_ItemBacklog] ibSUP on (ibSUP.CodigoItem = ib.CodigoItemSuperior)
         LEFT JOIN {0}.{1}.Agil_TipoStatusItemBacklog ts on (ib.CodigoTipoStatusItem = ts.CodigoTipoStatusItem)
         LEFT JOIN {0}.{1}.Agil_TipoClassificacaoItemBacklog tc on (ib.CodigoTipoClassificacaoItem = tc.CodigoTipoClassificacaoItem)
         LEFT JOIN {0}.{1}.RecursoCorporativo p on (p.CodigoRecursoCorporativo = ib.CodigoPessoa)
         LEFT JOIN {0}.{1}.TipoTarefaTimeSheet AS ttf on (ttf.CodigoTipoTarefaTimeSheet = ib.CodigoTipoTarefaTimesheet) 

		 WHERE IB.CodigoProjeto IN(SELECT {2} as CodigoProjeto 
                             UNION SELECT CodigoProjetoFilho  as CodigoProjeto
		                             FROM LinkProjeto 
									 WHERE CodigoProjetoPai = {2})
									 AND  ISNULL(ib.IndicaTarefa, 'N') = 'N'
           {3}
        ORDER BY ib.[Importancia] desc
        END
            ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, where);


        var output = JsonConvert.SerializeObject(cDados.getDataSet(sql).Tables[0], settings);
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-sprints")]
    public string ObterSprints(int codigoProjeto, string ordenacao, bool mostrarSprintsEncerradas)
    {
        string whereSprintsEncerradas = " ";
        if(mostrarSprintsEncerradas == false)
        {
            whereSprintsEncerradas = " and st.IniciaisStatus <> 'SPRINTENCERRADA' ";
        }
        string sql;
        string orderBy = "";
        if(ordenacao == "A" || string.IsNullOrEmpty(ordenacao))
        {
            orderBy = "order by ai.[InicioPrevisto] asc";
        }
        else
        {
            orderBy = "order by ai.[InicioPrevisto] desc";
        }

        #region Comando SQL
        sql = string.Format(@"
       SELECT  ROW_NUMBER()over({1}) as seq , 
               ai.[CodigoIteracao]
              ,ai.[CodigoProjetoIteracao]
              ,p.[NomeProjeto] as TituloSprint
              ,ai.[InicioPrevisto] as Inicio
              ,ai.[TerminoPrevisto] as Termino
              ,dbo.f_Agil_GetStatusIteracao(ai.[CodigoIteracao], ai.[DataPublicacaoPlanejamento], ai.[TerminoReal]) as Status 
              ,st.IniciaisStatus
              ,ISNULL((SELECT TOP 1 1 FROM Agil_ItemBacklog WHERE CodigoIteracao = ai.[CodigoIteracao] AND DataExclusao IS NULL),0)  as PossuiItemAssociado
              ,(SELECT CASE WHEN    isnull(capacidade,0) = 0 then 'Nenhuma capacidade alocada'          
			                WHEN     Convert(Decimal(10,2),IsNull(Capacidade,0)) = 0 THEN  'Sprint não Ocupada' 
                            ELSE convert(varchar(20), Convert(Decimal(10,2), ((IsNull(Estimativa,0) / Capacidade) * 100) )) + '% da Sprint Ocupada' END 
                  FROM dbo.f_Agil_GetAlocacaoCapacidadeIteracao(ai.[CodigoProjetoIteracao]) ) as OcupacaoSprint
     

	          ,(SELECT CASE WHEN isnull(count(DISTINCT CodigoItem),0) = 0 THEN 'Nenhum item' 
			                WHEN count(DISTINCT CodigoItem) = 1 then '1 item'   
			                ELSE convert(varchar(15),  count(DISTINCT CodigoItem))   + ' itens' end
                 FROM agil_itembacklog aib    
                WHERE aib.CodigoIteracao =  ai.[CodigoIteracao]   
                 and ISNULL(aib.IndicaTarefa, 'N') = 'N' and aib.dataExclusao  IS NULL) as QuantidadeItens  
              ,dbo.f_VerificaAcessoConcedido({3}, {4}, ai.CodigoProjetoIteracao, null, 'PR', 0, null, 'PR_ExcItSpt') AS PermissaoExcluirItensSprint
              ,dbo.f_VerificaAcessoConcedido({3}, {4}, ai.CodigoProjetoIteracao, null, 'PR', 0, null, 'PR_IncItSpt') AS PermissaoIncluirItensSprint
        FROM [Projeto] p
        INNER JOIN LinkProjeto lp on (lp.CodigoProjetoFilho  = p.CodigoProjeto) 
        INNER JOIN [Agil_Iteracao] ai on (ai.CodigoProjetoIteracao = p.CodigoProjeto) 
         LEFT JOIN [Status] st on (st.CodigoStatus = p.CodigoStatusProjeto)
        where lp.CodigoProjetoPai = {0} and p.DataExclusao is null {2}
        {1} ", codigoProjeto, orderBy, whereSprintsEncerradas, idUsuarioLogado, codigoEntidade, ordenacao);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-percentual-alocacao-sprint")]
    public string ObterPercentualAlocacaoSprint(int codigoIteracao)
    {
        string sql;

        #region Comando SQL
        sql = string.Format(@"
        SELECT CASE WHEN Convert(Decimal(10,2),IsNull(Capacidade,0)) = 0 THEN  'Sprint não Ocupada' 
                     ELSE convert(varchar(20), Convert(Decimal(10,2), ((IsNull(Estimativa,0) / Capacidade) * 100) )) +'% da Sprint Ocupada' END  as alocacao
				    from dbo.f_Agil_GetAlocacaoCapacidadeIteracao((SELECT TOP 1 codigoprojetoiteracao 
				                                                     FROM agil_iteracao  WHERE [CodigoIteracao] = {0})) ", codigoIteracao);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-permissao-manter-sprints")]
    public string ObterPermissaoManterSprints(int codigoProjeto)
    {
        string sql;
        #region Comando SQL
        sql = string.Format(@"


        DECLARE @CodigoUsuario as int
        DECLARE @CodigoEntidade as int
        DECLARE @CodigoObjeto as bigint
        DECLARE @CodigoTipoObjeto as smallint
        DECLARE @IniciaisTipoObjeto as char(2)
        DECLARE @CodigoObjetoPai as bigint
        DECLARE @CodigoPermissao as int
        DECLARE @IniciaisPermissao as varchar(18)

        SET @CodigoUsuario  = {0}
        SET @CodigoEntidade  = {1}
        SET @CodigoObjeto = {2}
        SET @CodigoTipoObjeto = NULL
        SET @IniciaisTipoObjeto = 'PR'
        SET @CodigoObjetoPai = 0
        SET @CodigoPermissao = null
        SET @IniciaisPermissao = 'PR_IteBkl'

        SELECT [dbo].[f_VerificaAcessoConcedido] (
             @CodigoUsuario
             ,@CodigoEntidade
             ,@CodigoObjeto
             ,@CodigoTipoObjeto
             ,@IniciaisTipoObjeto
             ,@CodigoObjetoPai
             ,@CodigoPermissao
             ,@IniciaisPermissao) AS Permissao ", idUsuarioLogado, codigoEntidade, codigoProjeto);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
    }


    [WebMethod(EnableSession = true, MessageName = "excluir-sprint")]
    public string ExcluirSprint(string codigoProjeto, string codigoIteracao, string codigoProjetoPai)
    {
        string mensagemErro = "";
        bool result = false;
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjeto int
        DECLARE @in_CodigoIteracao int
        DECLARE @in_CodigoProjetoPai int
        DECLARE @mensagem as varchar(max)
        SET @in_CodigoProjeto = {0}
        SET @in_CodigoIteracao = {1}
        SET @in_CodigoProjetoPai = {2}
        
        SET @mensagem = ''
        
        IF NOT EXISTS(SELECT  1 FROM agil_itemBacklog WHERE CodigoIteracao = {1})
        BEGIN        
            EXECUTE @RC = dbo.[p_Agil_ExcluiIteracao] 
            @in_CodigoProjeto
            ,@in_CodigoIteracao
            ,@in_CodigoProjetoPai 
        END
        ELSE
        BEGIN
             SET @mensagem = 'Não foi possível excluir pois existem itens associados a esta sprint.'
        END
        select @mensagem
      ", codigoProjeto, codigoIteracao, codigoProjetoPai);
        int regAfetados = 0;
        try
        {
            DataSet ds = cDados.getDataSet(comandoSQL);
            mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        }
        catch(Exception ex)
        {
            mensagemErro = ex.Message;
        }
        
        string output = JsonConvert.SerializeObject(mensagemErro);

        return output;
    }


    [WebMethod(EnableSession = true, MessageName = "excluir-item-backlog-da-iteracao")]
    public string ExcluirItemBacklog(int codigoItem, int codigoIteracao)
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @CodigoEntidadeContexto int
        DECLARE @CodigoUsuarioSistema int
        DECLARE @CodigoItemBacklog int
        DECLARE @SiglaContextoExclusao varchar(9)

        SET @CodigoEntidadeContexto = {0}
        SET @CodigoUsuarioSistema = {1}
        SET @CodigoItemBacklog = {2}
        SET @SiglaContextoExclusao = 'PlnSptPrj'

        EXECUTE @RC = [dbo].[p_Agil_excluiItemBacklog] 
           @CodigoEntidadeContexto
          ,@CodigoUsuarioSistema
          ,@CodigoItemBacklog
          ,@SiglaContextoExclusao", codigoEntidade, idUsuarioLogado, codigoItem);
        int regAfetados = 0;

        var output = JsonConvert.SerializeObject(cDados.execSQL(comandoSQL, ref regAfetados));
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "salvar-item-de-backlog-na-iteracao")]
    public string SalvarItemDeBacklogNaIteracao(int codigoItemMovimentado, int codigoIteracaoOrigem, int codigoIteracaoDestino)
    {
        string comandoSQL = string.Format(@"
        BEGIN TRAN
            DECLARE @in_CodigoItemMovimentado	INT,
                    @in_CodigoIteracaoDestino	INT,
					@in_CodigoIteracaoOrigem	INT,
                    @in_CodigoUsuario           INT
                   
	
           SET @in_CodigoItemMovimentado = {0}
            SET @in_CodigoIteracaoOrigem = {1}
            SET @in_CodigoIteracaoDestino = {2}
            SET @in_CodigoUsuario = {3}
			
            EXEC p_Agil_MovimentaItemEntreSprints @in_CodigoItemMovimentado,
                                                   @in_CodigoIteracaoOrigem,
												   @in_CodigoIteracaoDestino,
                                                   @in_CodigoUsuario
        COMMIT
", codigoItemMovimentado, codigoIteracaoOrigem, codigoIteracaoDestino, idUsuarioLogado);
        int regAfetados = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAfetados);
        string output = JsonConvert.SerializeObject(result);

        return output;
    }

    private void getIniciaisStatus(string codigoStatusOrigem, string codigoStatusDestino, ref string iniciaisStatusOrigem, ref string iniciaisStatusDestino)
    {
        string comandoSQL = string.Format(@"
            SELECT 
                    (SELECT 
                            CASE SequenciaApresentacaoRaia
                              WHEN 0 THEN 'SP_NAOINI'
                              WHEN 255 THEn 'SP_PRONTO'
                              ELSE 'SP_FAZENDO'
                            END AS IniciaisTipoStatusItemControladoSistema
                       FROM Agil_RaiasIteracao WHERE CodigoRaia = {0}) AS Origem,
                    (SELECT 
                            CASE SequenciaApresentacaoRaia
                              WHEN 0 THEN 'SP_NAOINI'
                              WHEN 255 THEn 'SP_PRONTO'
                              ELSE 'SP_FAZENDO'
                            END AS IniciaisTipoStatusItemControladoSistema
                       FROM Agil_RaiasIteracao WHERE CodigoRaia = {1}) AS Destino"
            , codigoStatusOrigem, codigoStatusDestino);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            iniciaisStatusOrigem = ds.Tables[0].Rows[0]["Origem"].ToString();
            iniciaisStatusDestino = ds.Tables[0].Rows[0]["Destino"].ToString();
        }
    }

    [WebMethod(EnableSession = true, MessageName = "verifica-pode-associar-itens")]
    public bool VerificaPodeAssociarItens(int codigoProjetoIteracao)
    {
        string comandoSql = string.Format(@"
declare @CodigoProjetoAgil int, 
        @CodigoIteracao int,
        @CodigoProjetoIteracao int

        SET @CodigoProjetoIteracao = {0}

 SELECT TOP 1 @CodigoIteracao = [CodigoIteracao] FROM [Agil_Iteracao] WHERE [CodigoProjetoIteracao] = @CodigoProjetoIteracao
 SELECT TOP 1 @CodigoProjetoAgil = [CodigoProjetoPai] FROM [dbo].[LinkProjeto] where CodigoProjetoFilho = @CodigoProjetoIteracao

        print @CodigoIteracao
        print @CodigoProjetoAgil

        SELECT COUNT(1) AS QuantidadeRegistros
         FROM    [Agil_ItemBacklog] ib  
     left join [Agil_ItemBacklog] ibSUP on (ibSUP.CodigoItem = ib.CodigoItemSuperior)  
         LEFT JOIN [Agil_TipoStatusItemBacklog] ts on (ib.CodigoTipoStatusItem = ts.CodigoTipoStatusItem)  
         LEFT JOIN [Agil_TipoClassificacaoItemBacklog] tc on (ib.CodigoTipoClassificacaoItem = tc.CodigoTipoClassificacaoItem)  
         LEFT JOIN [RecursoCorporativo] p on (p.CodigoRecursoCorporativo = ib.CodigoPessoa)  
         LEFT JOIN [TipoTarefaTimeSheet] AS ttf on (ttf.CodigoTipoTarefaTimeSheet = ib.CodigoTipoTarefaTimesheet)  
  
         WHERE ib.[CodigoProjeto] = @CodigoProjetoAgil  
     AND ISNULL(ib.[IndicaTarefa],'N') = 'N'  
     AND (ib.[CodigoIteracao] IS NULL OR ib.[CodigoIteracao] = @CodigoIteracao)  
     AND NOT EXISTS (SELECT 1 FROM [dbo].[Agil_ItemBacklog] ib2  
             WHERE ib2.[CodigoItemSuperior] = ib.[CodigoItem]  
             AND ISNULL(ib2.[IndicaTarefa],'N') = 'N'  
             AND ( (ib2.[CodigoIteracao] IS NOT NULL AND ib2.[CodigoIteracao] <> @CodigoIteracao) OR  
                   (ib2.[CodigoIteracao] IS NULL)  
               )  
             )", codigoProjetoIteracao);
        var dataSet = cDados.getDataSet(comandoSql);
        var quantidadeRegistros = dataSet.Tables[0].AsEnumerable().First().Field<int>("QuantidadeRegistros");

        return quantidadeRegistros > 0;
    }


    [WebMethod(EnableSession = true, MessageName = "obter-usuarios-mencao")]
    public string ObterUsuariosMencao(string filtro, int itensRetornados = 5)
    {
        string sql = string.Format(@"
DECLARE @Filtro varchar(100)
    SET @Filtro = '{0}'

 SELECT TOP {1}
        CodigoUsuario, 
        ISNULL(NomeCompletoUsuario, NomeUsuario) AS NomeUsuario, 
        NomeCompletoUsuario, 
        EMail,
        FotoUsuario, 
        IniciaisNomeUsuario, 
        CorPerfilUsuario 
   FROM Usuario AS u 
  WHERE u.DataExclusao IS NULL
    AND ( u.EMail LIKE '%' + @Filtro + '%' OR 
          u.NomeUsuario LIKE '%' + @Filtro + '%' OR 
          u.NomeCompletoUsuario LIKE '%' + @Filtro + '%')"
, filtro, itensRetornados);

        var usuarios = cDados.getDataSet(sql).Tables[0]
            .AsEnumerable()
            .Select(dr => new {
                CodigoUsuario = dr.Field<int>("CodigoUsuario"),
                NomeUsuario = dr.Field<string>("NomeUsuario"),
                NomeCompletoUsuario = dr.Field<string>("NomeCompletoUsuario"),
                EMail = dr.Field<string>("EMail"),
                IniciaisNomeUsuario = dr.Field<string>("IniciaisNomeUsuario"),
                CorPerfilUsuario = dr.Field<string>("CorPerfilUsuario")
            });
        var output = JsonConvert.SerializeObject(usuarios, settings);

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-detalhes-item-backlog")]
    public string ObterDetalhesItemBacklog(int codigoItem)
    {
        string sql = string.Format(@"
        SELECT [CodigoItem]
      ,[CodigoProjeto]
      ,[CodigoItemSuperior]
      ,[TituloItem]
      ,[DetalheItem]
      ,[CodigoTipoStatusItem]
      ,[CodigoTipoClassificacaoItem]
      ,[CodigoUsuarioInclusao]
      ,[DataInclusao]
      ,[CodigoUsuarioUltimaAlteracao]
      ,[DataUltimaAlteracao]
      ,[CodigoUsuarioExclusao]
      ,[PercentualConcluido]
      ,[CodigoIteracao]
      ,[Importancia]
      ,[Complexidade]
      ,[EsforcoPrevisto]
      ,[IndicaItemNaoPlanejado]
      ,[IndicaQuestao]
      ,[IndicaBloqueioItem]
      ,[CodigoWorkflow]
      ,[CodigoInstanciaWF]
      ,[CodigoCronogramaProjetoReferencia]
      ,[CodigoTarefaReferencia]
      ,[CodigoUsuarioResponsavel]
      ,[ValorReceitaItem]
      ,[CodigoPessoa]
      ,[ReceitaPrevista]
      ,[CodigoTipoTarefaTimeSheet]
      ,[dataAlvo]
      ,[CodigoRecursoAlocado]
      ,[IndicaTarefa]
      ,[CodigoRaia]
      ,[EsforcoReal]
      ,[dataExclusao]
      ,[CodigoTipoItemBacklog]
      ,[CodigoCartaPlanningPoker]
      ,[CodigoItemEspelho]
      ,[ValorCustoItem]
      ,[ComentarioCustoItem]
      ,[ComentarioReceitaItem]
  FROM [dbo].[Agil_ItemBacklog]
  where CodigoItem = {0}", codigoItem);

        var item = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(item, settings);
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "incluir-backlog-rapidamente")]
    public string IncluirBacklogRapidamente(int codigoProjeto, string tituloItem)
    {
        string comandoSQL = string.Format(@"
         DECLARE @CodigoProjeto as int
           ,@TituloItem as varchar(150)
           ,@CodigoUsuarioInclusao as int
           ,@DataInclusao as datetime
           ,@IndicaItemNaoPlanejado as char(1)
           ,@CodigoUsuarioResponsavel as int
           ,@IndicaTarefa as char(1)
           ,@EsforcoPrevisto as decimal(10,2)
           ,@CodigoTipoStatusItem as smallint
           ,@PercentualConcluido as decimal(10,2)

		   SET @CodigoProjeto =  {0}
           SET @TituloItem =  '{1}'
           SET @CodigoUsuarioInclusao =  {2}
           SET @DataInclusao =  GETDATE()
           SET @IndicaItemNaoPlanejado =  'S'
           SET @CodigoUsuarioResponsavel =  {2}
           SET @IndicaTarefa =  'N'
           SET @EsforcoPrevisto = 1
           set @PercentualConcluido = 0

	      SELECT @CodigoTipoStatusItem = CodigoTipoStatusItem 
            FROM Agil_TipoStatusItemBacklog 
           WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL'


           INSERT INTO [dbo].[Agil_ItemBacklog]
           ([CodigoProjeto]
           ,[TituloItem]
           ,[CodigoUsuarioInclusao]
           ,[DataInclusao]
           ,[IndicaItemNaoPlanejado]
           ,[CodigoUsuarioResponsavel]
           ,[IndicaTarefa]
           ,[EsforcoPrevisto]
           ,[CodigoTipoStatusItem]
           ,[PercentualConcluido]
           ,[Importancia])
           VALUES
           (@CodigoProjeto
           ,@TituloItem
           ,@CodigoUsuarioInclusao
           ,@DataInclusao
           ,@IndicaItemNaoPlanejado
           ,@CodigoUsuarioResponsavel
           ,@IndicaTarefa
           ,@EsforcoPrevisto
           ,@CodigoTipoStatusItem
           ,@PercentualConcluido
           ,1)

 select SCOPE_IDENTITY()
", codigoProjeto, tituloItem.Replace("'", "'+char(39)+'"), idUsuarioLogado);

        int codigoitem = int.Parse(cDados.getDataSet(comandoSQL).Tables[0].Rows[0][0].ToString());
        var output = ObterDetalhesItem(codigoitem);
        return output;
    }
}
