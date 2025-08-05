using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services;

/// <summary>
/// Summary description for agil_taskboard_service
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class agil_taskboard_service : System.Web.Services.WebService
{
    dados cDados;
    string codigoEntidade;
    string idUsuarioLogado;
    JsonSerializerSettings settings;

    public agil_taskboard_service()
    {
        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        settings = new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" };

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true, MessageName = "encerrar-sprint")]
    public string EncerrarSprint(int codigoProjetoIteracao)
    {
        string comandoSQL = string.Format(@"
        UPDATE Agil_ItemBacklog
		   SET CodigoIteracao = NULL,
			     CodigoRaia = NULL,
                 CodigoTipoStatusItem = (SELECT TOP 1 CodigoTipoStatusItem 
                                           FROM Agil_TipoStatusItemBacklog  
                                          WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL')

		   FROM Agil_Iteracao AS i
		  WHERE Agil_ItemBacklog.CodigoIteracao = i.CodigoIteracao
		    AND i.CodigoProjetoIteracao = {0}
		    AND IndicaTarefa = 'S'
			AND EXISTS(SELECT 1
			              FROM Agil_ItemBacklog AS pai
						 WHERE pai.CodigoItem = Agil_ItemBacklog.CodigoItemSuperior
						   AND pai.PercentualConcluido < 100
						   AND pai.CodigoIteracao = i.CodigoIteracao)
		  
         UPDATE Agil_ItemBacklog
		    SET CodigoIteracao = NULL,
			     CodigoRaia = NULL,
                 CodigoTipoStatusItem = (SELECT TOP 1 CodigoTipoStatusItem 
                                           FROM Agil_TipoStatusItemBacklog  
                                          WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL')
		   FROM Agil_Iteracao AS i
		   WHERE Agil_ItemBacklog.CodigoIteracao = i.CodigoIteracao
		     AND i.CodigoProjetoIteracao = {0}
		     AND PercentualConcluido < 100
			 AND IndicaTarefa = 'N'

          UPDATE Agil_Iteracao
             SET TerminoReal = getdate()
           WHERE CodigoProjetoIteracao = {0} 

         --passar o status do projeto para 'SPRINTENCERRADA'
		  DECLARE @l_CodigoStatus SmallInt
		  SELECT @l_CodigoStatus = Min(codigostatus )
			FROM Status
		   WHERE IniciaisStatus = 'SPRINTENCERRADA'
			 AND TipoStatus = 'SPT'
          IF @l_CodigoStatus IS NOT NULL
		    UPDATE Projeto
		       SET CodigoStatusProjeto = @l_CodigoStatus
		     WHERE CodigoProjeto = {0}",
        codigoProjetoIteracao);
        int regAfetados = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAfetados);
        string output = JsonConvert.SerializeObject(result);

        return output;
    }
    [WebMethod(EnableSession = true, MessageName = "obter-tarefas")]
    public string ObterTarefas(int codigoProjetoIteracao, int codigoItem, string pesquisaTexto, string indicaProblema, string indicaBloqueioItem, int codigoStatusItem, string indicaFiltroPorUsuarioLogado)
    {
        string sql;

        #region Comando SQL

        sql = string.Format(@"

        DECLARE @CodigoProjetoIteracao int

        DECLARE @RC int
        DECLARE @in_CodigoIteracao int
        DECLARE @in_CodigoItem int
        DECLARE @in_PesquisaTexto varchar(100)
        DECLARE @in_IndicaProblema char(1)
        DECLARE @in_IndicaBloqueioItem char(1)
        DECLARE @in_CodigoStatusItem int
        DECLARE @in_CodigoUsuarioLogado int

        SET @CodigoProjetoIteracao = {0};
        SELECT @in_CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao;
        SET @in_CodigoItem  = {1}
        SET @in_PesquisaTexto = '{2}'
        SET @in_IndicaProblema  = '{3}'
        SET @in_IndicaBloqueioItem = '{4}'
        SET @in_CodigoStatusItem = '{5}'
        SET @in_CodigoUsuarioLogado = {6}

        EXECUTE @RC = [dbo].[p_Agil_ItensBacklogTarefasSprint] 
           @in_CodigoIteracao
          ,@in_CodigoItem
          ,@in_PesquisaTexto
          ,@in_IndicaProblema
          ,@in_IndicaBloqueioItem
          ,@in_CodigoStatusItem
          ,@in_CodigoUsuarioLogado
        ", codigoProjetoIteracao,
        ((codigoItem == 0) ? "NULL" : codigoItem.ToString()),
        pesquisaTexto.Substring(0, Math.Min(pesquisaTexto.Length, 100)).Replace("'", "''"),
        indicaProblema,
        indicaBloqueioItem,
        codigoStatusItem,
        (indicaFiltroPorUsuarioLogado == "S") ? idUsuarioLogado : "NULL");

        #endregion

        if (codigoItem == 0)
        {

            var dataSet = cDados.getDataSet(sql);
            var table = dataSet.Tables[0];
            var relation = new DataRelation("rel_itemsuperior_itemfilho",
                table.Columns["CodigoItem"], table.Columns["CodigoItemSuperior"], false);
            relation.Nested = true;
            dataSet.Relations.Add(relation);
            table.Columns.Add("QuantidadeItensFilhos", typeof(int), "Count(Child.CodigoItem)");
            var result = table.AsEnumerable()
                .Select(r => new
                {
                    CodigoItem = r.Field<int>("CodigoItem"),
                    CodigoProjeto = r.Field<int>("CodigoProjeto"),
                    CodigoItemSuperior = r.Field<int?>("CodigoItemSuperior"),
                    TituloItem = r.Field<string>("TituloItem"),
                    TituloItemSuperior = r.Field<string>("TituloItemSuperior"),
                    DetalheItem = r.Field<string>("DetalheItem"),
                    CodigoTipoStatusItem = r.Field<short?>("CodigoTipoStatusItem"),
                    CodigoTipoClassificacaoItem = r.Field<short?>("CodigoTipoClassificacaoItem"),
                    DescricaoTipoClassificacaoItem = r.Field<string>("DescricaoTipoClassificacaoItem"),
                    IniciaisTipoClassificacaoItemControladoSistema = r.Field<string>("IniciaisTipoClassificacaoItemControladoSistema"),
                    CodigoUsuarioInclusao = r.Field<int?>("CodigoUsuarioInclusao"),
                    DataInclusao = r.Field<DateTime?>("DataInclusao"),
                    CodigoUsuarioUltimaAlteracao = r.Field<int?>("CodigoUsuarioUltimaAlteracao"),
                    DataUltimaAlteracao = r.Field<DateTime?>("DataUltimaAlteracao"),
                    CodigoUsuarioExclusao = r.Field<int?>("CodigoUsuarioExclusao"),
                    PercentualConcluido = r.Field<decimal?>("PercentualConcluido"),
                    CodigoIteracao = r.Field<int?>("CodigoIteracao"),
                    Importancia = r.Field<short?>("Importancia"),
                    Complexidade = r.Field<byte?>("Complexidade"),
                    DescricaoComplexidade = r.Field<string>("DescricaoComplexidade"),
                    EsforcoPrevisto = r.Field<decimal?>("EsforcoPrevisto"),
                    IndicaItemNaoPlanejado = r.Field<string>("IndicaItemNaoPlanejado"),
                    IndicaQuestao = r.Field<string>("IndicaQuestao"),
                    IndicaBloqueioItem = r.Field<string>("IndicaBloqueioItem"),
                    CodigoWorkflow = r.Field<int?>("CodigoWorkflow"),
                    CodigoInstanciaWF = r.Field<long?>("CodigoInstanciaWF"),
                    CodigoCronogramaProjetoReferencia = r.Field<string>("CodigoCronogramaProjetoReferencia"),
                    CodigoTarefaReferencia = r.Field<int?>("CodigoTarefaReferencia"),
                    CodigoTipoTarefaTimeSheet = r.Field<short?>("CodigoTipoTarefaTimeSheet"),
                    DescricaoTipoTarefaTimeSheet = r.Field<string>("DescricaoTipoTarefaTimeSheet"),
                    DataAlvo = r.Field<DateTime?>("DataAlvo"),
                    DataAlvoExcedida = r.Field<DateTime?>("DataAlvo").HasValue && r.Field<DateTime?>("DataAlvo").Value < DateTime.Today,
                    IndicaTarefa = r.Field<string>("IndicaTarefa"),
                    CodigoRaia = r.Field<int?>("CodigoRaia"),
                    ValorReceitaItem = r.Field<decimal?>("ValorReceitaItem"),
                    CodigoUsuarioRecurso = r.Field<int?>("CodigoUsuarioRecurso"),
                    NomeUsuarioRecurso = r.Field<string>("NomeUsuarioRecurso"),
                    EmailUsuarioRecurso = r.Field<string>("EmailUsuarioRecurso"),
                    IniciaisUsuarioRecurso = r.Field<string>("IniciaisUsuarioRecurso"),
                    CorPerfilUsuarioRecurso = r.Field<string>("CorPerfilUsuarioRecurso"),
                    CodigoItemEspelho = r.Field<int?>("CodigoItemEspelho"),
                    QuantidadeItensFilhos = r.Field<int>("QuantidadeItensFilhos"),
                    QuantChecklistTotal = r.Field<int>("QuantChecklistTotal"),
                    QuantChecklistConcluido = r.Field<int>("QuantChecklistConcluido"),
                    QuantComentario = r.Field<int>("QuantComentario"),
                    Tag1 = r.Field<string>("Tag1"),
                    Tag2 = r.Field<string>("Tag2"),
                    Tag3 = r.Field<string>("Tag3"),
                    PossuiAnexo = r.Field<int>("PossuiAnexo")

                })
                .ToList();
            var structuredResult = result
                .Where(r => r.IndicaTarefa == "N")
                .Select(sr => new { Dados = sr, Tarefas = result.Where(r => r.CodigoItemSuperior == sr.CodigoItem) });

            var output = JsonConvert.SerializeObject(structuredResult, settings);
            return output;
        }
        else
        {
            var output = JsonConvert.SerializeObject(cDados.getDataSet(sql).Tables[0], settings);
            return output;
        }
    }

    //[WebMethod(EnableSession = true, MessageName = "obter-permissao-de-encerrar-sprint")]
    //public string ObterPermissaoDeEncerrarSprint()
    //{
    //     string comandoSQL = string.Format(@"SELECT dbo.f_VerificaAcessoEmAlgumObjeto({0}, NULL, 'PR', {1}, 'PR_EncerrarSprint') AS Permissao"
    //       , idUsuarioLogado
    //       , codigoEntidade);

    //    var dt = cDados.getDataSet(comandoSQL).Tables[0];
    //    var output = JsonConvert.SerializeObject(dt, settings);
    //    return output;
    //}




    [WebMethod(EnableSession = true, MessageName = "obter-raias")]
    public string ObterRaias(int codigoProjetoIteracao)
    {
        string sql;

        #region Comando SQL

        sql = string.Format(@"
DECLARE @CodigoProjetoIteracao INT,
        @CodigoIteracao INT

    SET @CodigoProjetoIteracao = {0}--1205--1207

 SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao

SELECT * FROM [dbo].[f_Agil_GetRaiasIteracao] (@CodigoIteracao)", codigoProjetoIteracao);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-informacoes-gerais")]
    public string ObterInformacoesGerais(int codigoProjetoIteracao)
    {
        string sql;

        #region Comando SQL
        sql = string.Format(@"
DECLARE @CodigoProjeto INT = {0},
        @CodigoUsuario INT = {1}

 SELECT 
        ISNULL(i.InicioReal, i.InicioPrevisto) AS Inicio, 
        ISNULL(i.TerminoReal, i.TerminoPrevisto) AS Termino, 
        i.InicioPrevisto, 
        i.TerminoPrevisto, 
        i.InicioReal, 
        i.TerminoReal, 
        i.DataPublicacaoPlanejamento ,
        (CASE WHEN EXISTS (
                           SELECT 1
                             FROM [Agil_RecursoIteracao] AS rp
                             JOIN [RecursoCorporativo] AS rc ON rp.[CodigoRecursoCorporativo] = rc.[CodigoRecursoCorporativo]
                            WHERE rc.[CodigoUsuario] = @CodigoUsuario
                              AND rp.[CodigoIteracao] = (SELECT TOP 1 [CodigoIteracao] from Agil_Iteracao WHERE [CodigoProjetoIteracao] = @CodigoProjeto)) THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END
        ) AS IndicaUsuarioLogadoMembroEquipe
     ,
     (CASE WHEN EXISTS (
      (SELECT 1 FROM Agil_ItemBacklog WHERE IndicaBloqueioItem = 'S' AND CodigoIteracao = i.CodigoIteracao)) THEN CAST(1 AS bit) ELSE CAST(0 as bit) END ) AS IndicaQueTemItemBloqueado
     ,(CASE WHEN(LEN(p.NomeProjeto) > 40 ) THEN  SUBSTRING(p.NomeProjeto, 0, 40) + '...' + ' - '  ELSE ISNULL(p.NomeProjeto,'') + ' - ' END) as NomeSprint
     ,st.IniciaisStatus  
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_EncerrarSprint') AS Permissao
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_CadRaias') AS PermissaoManterRaias
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_EqAgil') AS PermissaoManterEquipeAgil
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExcItSpt') AS PermissaoExcluirItensSprint
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncItSpt') AS PermissaoIncluirItensSprint
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltItSpt') AS PermissaoAlterarItensSprint
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_VisPnlSpt') AS PermissaoVisualizarPainelSprint
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncTarItem') AS PermissaoIncluirTarefasItensBacklog
     ,dbo.f_VerificaAcessoConcedido(@CodigoUsuario, {2}, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExcTarItem') AS PermissaoExcluirTarefasItensBacklog
  FROM Agil_Iteracao AS i
   INNER JOIN Projeto as p on (p.CodigoProjeto = i.CodigoProjetoIteracao)
 LEFT JOIN [Status] st on (st.CodigoStatus = p.CodigoStatusProjeto)
  WHERE CodigoProjetoIteracao = @CodigoProjeto", codigoProjetoIteracao, idUsuarioLogado, codigoEntidade);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "salvar-tarefa")]
    public string SalvarTarefa(string codigoItem, string origem, string destino)
    {
        var status = string.Empty;
        var statusAnterior = string.Empty;
        getIniciaisStatus(origem, destino, ref statusAnterior, ref status);

        // escreva aqui o comando para executar a ação no servidor de dados
        var dbName = cDados.getDbName();
        var dbOwner = cDados.getDbOwner();
        var comandoSQL = new StringBuilder();
        comandoSQL.AppendFormat(@"
        BEGIN
                  DECLARE @CodigoRecursoCorporativo INT

                  SELECT TOP 1
	                  @CodigoRecursoCorporativo = rc.[CodigoRecursoCorporativo]
                  FROM [RecursoCorporativo] AS rc
                  WHERE rc.CodigoUsuario = {2}
                  

                  UPDATE Agil_ItemBacklog
                     SET CodigoRaia = {1}
                        ,CodigoTipoStatusItem = (SELECT TOP 1 CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE IniciaisTipoStatusItemControladoSistema = '{3}')
                        ,PercentualConcluido = (SELECT TOP 1 PercentualConcluido FROM Agil_RaiasIteracao WHERE CodigoRaia = {1})
                        ,CodigoRecursoAlocado = @CodigoRecursoCorporativo 
                   WHERE CodigoItem = {0}", codigoItem, destino, idUsuarioLogado, status);

        comandoSQL.AppendFormat(@"

DECLARE
        @CodigoProjeto INT,
        @CodigoItem INT,
        @CodigoIteracao INT,
        @StatusIteracao VARCHAR(20)

    SET @CodigoItem = {0}

    SELECT @CodigoIteracao = [CodigoIteracao], @CodigoProjeto = [CodigoProjeto] FROM [Agil_ItemBacklog] WHERE [CodigoItem] = @CodigoItem

    SELECT @StatusIteracao = [dbo].[f_Agil_VerificaIteracaoEncerrada](@CodigoProjeto)


    IF ISNULL(@StatusIteracao, '') <> 'SPRINTENCERRADA'
    BEGIN", codigoItem);
        if (statusAnterior == "SP_NAOINI" && status == "SP_FAZENDO")
        {
            comandoSQL.AppendFormat(@"
                       DECLARE @CodigoNovoStatus INT

                            SELECT @CodigoNovoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoNovoStatus
                      ", dbName, dbOwner, codigoItem, idUsuarioLogado, status);
        }

        else if (status == "SP_PRONTO")
        {
            comandoSQL.AppendFormat(@"
                       DECLARE @CodigoNovoStatus INT

                            SELECT @CodigoNovoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'                        

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoNovoStatus
                      ", dbName, dbOwner, codigoItem, idUsuarioLogado, status);
        }
        else
        {

            comandoSQL.AppendFormat(@"
                        BEGIN
                            DECLARE @CodigoStatus INT

                            SELECT @CodigoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                             EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoStatus
                        END                       
                      ", dbName, dbOwner, codigoItem, idUsuarioLogado, status);

        }


        comandoSQL.AppendFormat(@"
    END
END
                  SELECT TOP 1 [CodigoProjetoIteracao] FROM [Agil_Iteracao] WHERE [CodigoIteracao] = @CodigoIteracao");

        var result = cDados.getDataSet(comandoSQL.ToString());
        /*
        var output = JsonConvert.SerializeObject(result.Tables[0], new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
        return output;
        */
        string codigoProjetoIteracao;
        codigoProjetoIteracao = result.Tables[0].Rows[0]["CodigoProjetoIteracao"].ToString();
        return this.ObterTarefas(Convert.ToInt32(codigoProjetoIteracao), Convert.ToInt32(codigoItem), "", "N", "N", -1, "N");
    }

    [WebMethod(EnableSession = true, MessageName = "obter-detalhes-tarefa")]
    public string ObterDetalhesTarefa(int codigoProjetoIteracao, int codigoItem)
    {
        string pesquisaTexto = "";
        var output = ObterTarefas(codigoProjetoIteracao, codigoItem, pesquisaTexto, "N", "N", -1, "N");
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "excluir-item-backlog")]
    public string ExcluirItemBacklog(int codigoProjetoIteracao, int codigoItem)
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
        SET @SiglaContextoExclusao = 'KnbSpt'

        EXECUTE @RC = [dbo].[p_Agil_excluiItemBacklog] 
           @CodigoEntidadeContexto
          ,@CodigoUsuarioSistema
          ,@CodigoItemBacklog
          ,@SiglaContextoExclusao", codigoEntidade, idUsuarioLogado, codigoItem);
        int regAfetados = 0;

        var output = JsonConvert.SerializeObject(cDados.execSQL(comandoSQL, ref regAfetados));
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

    [WebMethod(EnableSession = true, MessageName = "obter-permissao-manter-item-backlog")]
    public string ObterPermissaoManterItemBacklog(int codigoProjeto)
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

        SET @CodigoUsuario = {0}
        SET @CodigoEntidade = {1}
        SET @CodigoObjeto = {2}
        SET @CodigoTipoObjeto = null
        SET @IniciaisTipoObjeto = 'PR'
        SET @CodigoObjetoPai = 0
        SET @CodigoPermissao = null
        SET @IniciaisPermissao = 'PR_IncItSpt'

        SELECT [dbo].[f_VerificaAcessoConcedido] (
           @CodigoUsuario
          ,@CodigoEntidade
          ,@CodigoObjeto
          ,@CodigoTipoObjeto
          ,@IniciaisTipoObjeto
          ,@CodigoObjetoPai
          ,@CodigoPermissao
          ,@IniciaisPermissao) as Permissao
 ", idUsuarioLogado, codigoEntidade, codigoProjeto);

        #endregion

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, settings);

        return output;
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

    [WebMethod(EnableSession = true, MessageName = "verifica-pode-associar-itens2")]
    public bool VerificaPodeAssociarItens2(int codigoProjetoIteracao)
    {
        string comandoSql = string.Format(@"
        SELECT dbo.f_VerificaAcessoConcedido({0}, {1}, {2}, null, 'PR', 0, null, 'PR_AssItSpt') AS PermissaoAssociarItensSprint
        ", idUsuarioLogado, codigoEntidade, codigoProjetoIteracao);
        var dataSet = cDados.getDataSet(comandoSql);
        var retorno = dataSet.Tables[0].AsEnumerable().First().Field<bool>("PermissaoAssociarItensSprint");

        return retorno;
    }

    [WebMethod(EnableSession = true, MessageName = "criar-tarefa-espelho")]
    public string CriarTarefaEspelho(int codigoProjetoIteracao, int codigoItem, int codigoRaia)
    {
        string sql = string.Format(@"
DECLARE @CodigoItem Int, 
        @CodigoRaia Int,
        @CodigoUsuario Int,
        @CodigoStatusItem Int, 
        @CodigoTarefaEspelho Int


  SET @CodigoItem = {0}
  SET @CodigoRaia = {2}
  SET @CodigoUsuario = {1}

/* Verifica se N�O existe uma tarefa espelho associada ao item em quest�o. Se n�o existir, cria a tarefa
     e a coloca como 'espelho' do item em quest�o */
  IF NOT EXISTS(SELECT 1
                   FROM[Agil_ItemBacklog]
                  WHERE[CodigoItem] = @CodigoItem

                    AND[CodigoItemEspelho] IS NOT NULL)
     
      BEGIN
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
                   ,[CodigoRaia] 
                   ,[CodigoRecursoAlocado]
                   ,[EsforcoReal]
                   ,[DataAlvo]
                   ,[ReceitaPrevista]
                   ,[CodigoTarefaReferencia]
                   ,[ValorCustoItem]
				   ,[ComentarioCustoItem]
                   ,[ValorReceitaItem]
				   ,[ComentarioReceitaItem])
             SELECT TOP 1
                     [CodigoProjeto] 
                    ,@CodigoItem                 
                    ,ib.[TituloItem]
                    ,ib.[DetalheItem]   
                    ,ib.[CodigoTipoStatusItem]       
                    ,ib.[CodigoTipoClassificacaoItem]                            
                    ,@CodigoUsuario                
                    ,GetDate()      
                    ,ib.[CodigoIteracao] 
                    ,ib.[Importancia]        
                    ,ib.[Complexidade] 
                    ,ib.[EsforcoPrevisto]            
                    ,ib.[IndicaItemNaoPlanejado]                    
                    ,ib.[IndicaQuestao]          
                    ,ib.[IndicaBloqueioItem]               
                    ,'S'           
                    ,@CodigoRaia    
                    ,ib.[CodigoRecursoAlocado]                  
                    ,ib.[EsforcoReal]      
                    ,ib.[DataAlvo]      
                    ,ib.[ReceitaPrevista]
                    ,ib.[CodigoTarefaReferencia]
                    ,ib.[ValorCustoItem]
					,ib.[ComentarioCustoItem]
                    ,ib.[ValorReceitaItem]
					,ib.[ComentarioReceitaItem]
               FROM [Agil_ItemBacklog] AS ib
              WHERE CodigoItem = @CodigoItem;

            SELECT @CodigoTarefaEspelho = SCOPE_IDENTITY()

            SELECT @CodigoStatusItem = CodigoTipoStatusItem 
              FROM Agil_TipoStatusItemBacklog 
             WHERE IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI'

            UPDATE [Agil_ItemBackLog] 
               SET [CodigoItemEspelho] = @CodigoTarefaEspelho 
                  ,[CodigoTipoStatusItem] = @CodigoStatusItem
              WHERE [CodigoItem] = @CodigoItem
            


            EXEC [dbo].[p_Agil_SincronizaItensClonados] @CodigoItem 

           --EXEC dbo.p_Agil_ReadequaImplementacaoItem @CodigoTarefaEspelho, @CodigoUsuario, '',  @CodigoStatusItem
        SELECT @CodigoTarefaEspelho AS id_retorno;
     END"
, codigoItem, idUsuarioLogado, codigoRaia, codigoEntidade);

        var dataSet = cDados.getDataSet(sql);
        if (dataSet.Tables.Count > 0)
        {
            var dataRow = dataSet.Tables[0].AsEnumerable().SingleOrDefault();
            int codigoTarefaEspelho = dataRow.Field<int>("id_retorno");
            return ObterDetalhesTarefa(codigoProjetoIteracao, codigoTarefaEspelho);
        }
        else
        {
            return ObterDetalhesTarefa(codigoProjetoIteracao, codigoItem);
        }

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
            .Select(dr => new
            {
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

    [WebMethod(EnableSession = true, MessageName = "obter-historico-item")]
    public string ObterHistoricoItem(int codigoItem, string iniciaisTipoObjeto)
    {
        string sql = string.Format(@"
        DECLARE @in_CodigoObjeto AS bigint
        DECLARE @in_IniciaisTipoObjeto AS char(2)

        SET @in_CodigoObjeto = {0}
        SET @in_IniciaisTipoObjeto = '{1}'

        SELECT 
           TipoRegistro,
           DataRegistro,
           CodigoUsuarioRegistro,
           NomeUsuarioRegistro,
           DetalhesRegistro 
        FROM [dbo].[f_GetTimeLineObjeto] (@in_CodigoObjeto, @in_IniciaisTipoObjeto)
        ORDER BY 
        DataRegistro DESC", codigoItem, iniciaisTipoObjeto);

        var itensHistorico = cDados.getDataSet(sql).Tables[0];

        var output = JsonConvert.SerializeObject(itensHistorico, settings);
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-status-itens")]
    public string ObterStatusItens()
    {
        string sql = string.Format(@"
        SELECT CodigoTipoStatusItem,
               TituloStatusItem
          FROM Agil_TipoStatusItemBacklog
         WHERE IndicaAtribuicaoManualItem = 'N'");

        var statusItens = cDados.getDataSet(sql).Tables[0];

        var output = JsonConvert.SerializeObject(statusItens, settings);
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
           ,@CodigoIteracao as int

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

          SELECT @CodigoIteracao = CodigoIteracao 
           FROM Agil_Iteracao WHERE CodigoProjetoIteracao = @CodigoProjeto

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
           ,[CodigoIteracao]
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
           ,@CodigoIteracao
           ,1)
 select SCOPE_IDENTITY()
", codigoProjeto, tituloItem.Replace("'", "'+char(39)+'"), idUsuarioLogado);

        int codigoitem = int.Parse(cDados.getDataSet(comandoSQL).Tables[0].Rows[0][0].ToString());
        var output = ObterDetalhesTarefa(codigoProjeto, codigoitem);
        return output;
    }

}
