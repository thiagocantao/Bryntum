using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Web.Services;

/// <summary>
/// Summary description for taskboard_service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class taskboard_service : System.Web.Services.WebService
{

    public taskboard_service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true, MessageName = "obter-tarefas")]
    public string ObterTarefas()
    {
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"
BEGIN
      DECLARE @DataInicioParam     DateTime,
       @DataTerminoParam     DateTime,
       @DataTarefasParam     DateTime,
       @DataToDoParam      DateTime,
              @CodigoRecursoParam    Int,
              @CodigoProjeto      int,
              @CodigoEntidade      int,
              @TipoAtualizacao     Char(2)

      SET @DataInicioParam  = null
      SET @DataTerminoParam = null
      SET @DataTarefasParam = null
      SET @DataToDoParam = null
      SET @CodigoRecursoParam = {1}
      SET @CodigoProjeto = -1
      SET @TipoAtualizacao = 'TD'
      SET @CodigoEntidade = {0}

      EXEC dbo.p_TarefasTimeSheetRecursoKanban @DataInicioParam
                                , @DataTerminoParam
                                , @DataTarefasParam
                                , @DataToDoParam
                                , @CodigoRecursoParam
                                , @CodigoProjeto
                                , @TipoAtualizacao
                                , @CodigoEntidade

END", codigoEntidade, idUsuarioLogado);
        var dt = cDados.getDataSet(sql).Tables[0];
        dt.Columns.Add("Status", typeof(string), "IIF(InicioReal Is Null, 'todo', IIF(TerminoReal Is Null,'doing','done'))");
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "salvar-tarefa")]
    public string SalvarTarefa(int codigoAtribuicao, string indicRaiaAtualCard)
    {
        var status = indicRaiaAtualCard.Contains("todo") ? "A" : (indicRaiaAtualCard.Contains("doing") ? "F" : "P");
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"
DECLARE @RC int
DECLARE @in_codigoAtribuicao int
DECLARE @in_indicRaiaAtualCard char(1)
DECLARE @in_codigoEntidadeParam int
DECLARE @idUsuarioLogado int

    SET @in_codigoAtribuicao = {0}
    SET @in_indicRaiaAtualCard = '{1}'
    SET @in_codigoEntidadeParam = {2}
    SET @idUsuarioLogado = {3}

EXECUTE @RC = [dbo].[p_SalvaTarefasTimeSheetRecursoKanban] 
   @in_codigoAtribuicao
  ,@in_indicRaiaAtualCard
  ,@in_codigoEntidadeParam

SELECT * FROM f_GetTarefaTimeSheetRecursoKanban(@in_codigoAtribuicao,@idUsuarioLogado,@in_codigoEntidadeParam)", codigoAtribuicao, status, codigoEntidade, idUsuarioLogado);

        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-detalhes-tarefa")]
    public string ObterDetalhesTarefa(int codigoAtribuicao)
    {
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"
DECLARE @RC int
DECLARE @in_codigoAtribuicao int
DECLARE @in_codigoEntidadeParam int
DECLARE @idUsuarioLogado int

    SET @in_codigoAtribuicao = {0}
    SET @in_codigoEntidadeParam = {1}
    SET @idUsuarioLogado = {2}

SELECT * FROM f_GetTarefaTimeSheetRecursoKanban(@in_codigoAtribuicao,@idUsuarioLogado,@in_codigoEntidadeParam)", codigoAtribuicao, codigoEntidade, idUsuarioLogado);

        var dt = cDados.getDataSet(sql).Tables[0];
        dt.Columns.Add("Status", typeof(string), "IIF(InicioReal Is Null, 'todo', IIF(TerminoReal Is Null,'doing','done'))");
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });

        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-parametro-usa-historico-oc")]
    public string ObterParametroUsaHistoricoOc()
    {
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"SELECT [dbo].[f_art_GetTipoTelaTaskboardTS] ({0},{1})", codigoEntidade, idUsuarioLogado);
        var dt = cDados.getDataSet(sql).Tables[0];
        var output = JsonConvert.SerializeObject(dt);
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "obter-tarefas-pesquisa-ordenacao")]
    public string ObterTarefasPesquisaOrdenacao(string status = "", byte pesquisaExibirCardsAtrasados = 0, string pesquisaDataInicio = "", string pesquisaDataFim = "", string pesquisaTexto = "", string ordenacao = "", int pagina = 0)
    {
        // pesquisaExibirCardsAtrasados
        if ((pesquisaExibirCardsAtrasados != 0) && (pesquisaExibirCardsAtrasados != 1))
        {
            pesquisaExibirCardsAtrasados = 0;
        }

        // pesquisaDataInicio
        DateTime dataInicio;
        if (pesquisaDataInicio != "")
        {
            if (!DateTime.TryParseExact(
                 pesquisaDataInicio,
                 "yyyy/MM/dd",
                 CultureInfo.InvariantCulture,
                 DateTimeStyles.AssumeUniversal,
                 out dataInicio))
            {
                pesquisaDataInicio = "";
            };
        }

        // pesquisaDataFim
        DateTime dataFim;
        if (pesquisaDataFim != "")
        {
            if (!DateTime.TryParseExact(
                 pesquisaDataFim,
                 "yyyy/MM/dd",
                 CultureInfo.InvariantCulture,
                 DateTimeStyles.AssumeUniversal,
                 out dataFim))
            {
                pesquisaDataFim = "";
            };
        }

        // pesquisaTexto
        if (pesquisaTexto != "")
        {
            pesquisaTexto = pesquisaTexto.Replace("'", "''");
        }

        // status e ordenacao
        switch (status)
        {
            case "todo":
                {
                    switch (ordenacao)
                    {
                        case "prioridade":
                        case "titulo":
                        case "dataInicio":
                        case "dataTermino":
                            {
                                break;
                            }
                        default:
                            {
                                ordenacao = "titulo";
                                break;
                            }
                    }
                    break;
                }
            case "doing":
                {
                    switch (ordenacao)
                    {
                        case "prioridade":
                        case "titulo":
                        case "dataInicio":
                        case "dataTermino":
                            {
                                break;
                            }
                        default:
                            {
                                ordenacao = "titulo";
                                break;
                            }
                    }
                    break;
                }
            case "done":
                {
                    switch (ordenacao)
                    {
                        case "prioridade":
                        case "titulo":
                        case "dataInicio":
                        case "dataTermino":
                            {
                                break;
                            }
                        default:
                            {
                                ordenacao = "titulo";
                                break;
                            }
                    }
                    break;
                }
            default:
                {
                    ordenacao = "";
                    break;
                }
        }

        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");

        var sql = string.Format(@"
BEGIN
    DECLARE
        @DataInicioParam                DATETIME,
        @DataTerminoParam               DATETIME,
        @DataTarefasParam               DATETIME,
        @DataToDoParam                  DATETIME,
        @CodigoRecursoParam             INT,
        @CodigoProjeto                  INT,
        @CodigoEntidade                 INT,
        @TipoAtualizacao                CHAR(2),
        @Status                         VARCHAR(20),
        @PesquisaExibirCardsAtrasados   BIT,
        @PesquisaDataInicio             VARCHAR(10),
        @PesquisaDataFim                VARCHAR(10),
        @PesquisaTexto                  VARCHAR(100),
        @Ordenacao                      VARCHAR(20),
        @Pagina                         INT;

    DECLARE
        @TotalTarefasNaoConcluidas INT,
        @TotalTarefasAtrasadas INT,
        @TotalTarefasConcluidas INT;

    SET @DataInicioParam  = null;
    SET @DataTerminoParam = null;
    SET @DataTarefasParam = null;
    SET @DataToDoParam = null;
    SET @CodigoRecursoParam = {1};
    SET @CodigoProjeto = -1;
    SET @TipoAtualizacao = 'TD';
    SET @CodigoEntidade = {0};
    SET @Status = '{2}';
    SET @PesquisaExibirCardsAtrasados = {3};
    SET @PesquisaDataInicio = '{4}';
    SET @PesquisaDataFim = '{5}';
    SET @PesquisaTexto = '{6}';
    SET @Ordenacao = '{7}';
    SET @Pagina = {8};

      EXEC dbo.p_TarefasTimeSheetRecursoKanbanPesquisaOrdenacao @DataInicioParam
                                , @DataTerminoParam
                                , @DataTarefasParam
                                , @DataToDoParam
                                , @CodigoRecursoParam
                                , @CodigoProjeto
                                , @TipoAtualizacao
                                , @CodigoEntidade
                                , @Status
                                , @PesquisaExibirCardsAtrasados
                                , @PesquisaDataInicio
                                , @PesquisaDataFim
                                , @PesquisaTexto
                                , @Ordenacao
                                , @Pagina;
								--, @TotalTarefasNaoConcluidas = @TotalTarefasNaoConcluidas OUTPUT
								--, @TotalTarefasAtrasadas = @TotalTarefasAtrasadas OUTPUT
								--, @TotalTarefasConcluidas = @TotalTarefasConcluidas OUTPUT;

                --SELECT @TotalTarefasNaoConcluidas AS TotalTarefasNaoConcluidas,
                     --@TotalTarefasAtrasadas AS TotalTarefasAtrasadas
                     --@TotalTarefasConcluidas AS TotalTarefasConcluidas;

END", codigoEntidade, idUsuarioLogado, status, pesquisaExibirCardsAtrasados, pesquisaDataInicio, pesquisaDataFim, pesquisaTexto, ordenacao, pagina);
        var dt = cDados.getDataSet(sql).Tables[0];
        dt.Columns.Add("Status", typeof(string), "IIF(InicioReal Is Null, 'todo', IIF(TerminoReal Is Null,'doing','done'))");
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "salvar-tarefa-novo")]
    public string SalvarTarefaNovo(string status, int codigoAtribuicao)
    {
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"
            DECLARE
                @RC INT,
                @in_codigoAtribuicao INT,
                @in_indicRaiaAtualCard CHAR(1),
                @in_codigoEntidadeParam INT,
                @idUsuarioLogado INT;

            SET @in_codigoAtribuicao = {0};
            SET @in_indicRaiaAtualCard = '{1}';
            SET @in_codigoEntidadeParam = {2};
            SET @idUsuarioLogado = {3};

            EXECUTE @RC = [dbo].[p_SalvaTarefasTimeSheetRecursoKanban] 
                 @in_codigoAtribuicao
                ,@in_indicRaiaAtualCard
                ,@in_codigoEntidadeParam;
            SELECT * FROM f_GetTarefaTimeSheetRecursoKanban(@in_codigoAtribuicao,@idUsuarioLogado,@in_codigoEntidadeParam);", codigoAtribuicao, (status.Contains("todo") ? "A" : (status.Contains("doing") ? "F" : "P")), codigoEntidade, idUsuarioLogado);
        var dt = cDados.getDataSet(sql).Tables[0];
        dt.Columns.Add("Status", typeof(string), "IIF(InicioReal Is Null, 'todo', IIF(TerminoReal Is Null,'doing','done'))");
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
        return output;
    }

    [WebMethod(EnableSession = true, MessageName = "arquivar-tarefa")]
    public string ArquivarTarefa(string status, int codigoAtribuicao)
    {
        var cDados = CdadosUtil.GetCdados(null);
        var codigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        var idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado");
        var sql = string.Format(@"
            DECLARE
                @RC INT,
                @in_codigoAtribuicao INT,
                @in_indicRaiaAtualCard CHAR(1),
                @in_codigoEntidadeParam INT,
                @idUsuarioLogado INT;

            SET @in_codigoAtribuicao = {0};
            SET @in_indicRaiaAtualCard = '{1}';
            SET @in_codigoEntidadeParam = {2};
            SET @idUsuarioLogado = {3};

            EXECUTE @RC = [dbo].[p_ArquivaTarefaTimeSheetRecursoKanban] 
                 @in_codigoAtribuicao
                ,@in_indicRaiaAtualCard
                ,@in_codigoEntidadeParam;
            SELECT * FROM f_GetTarefaTimeSheetRecursoKanban(@in_codigoAtribuicao,@idUsuarioLogado,@in_codigoEntidadeParam);", codigoAtribuicao, (status.Contains("todo") ? "A" : (status.Contains("doing") ? "F" : "P")), codigoEntidade, idUsuarioLogado);
        var dt = cDados.getDataSet(sql).Tables[0];
        dt.Columns.Add("Status", typeof(string), "IIF(InicioReal Is Null, 'todo', IIF(TerminoReal Is Null,'doing','done'))");
        var output = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
        return output;
    }
}
