using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for dadosReunioes
/// </summary>
public class dadosReunioes
{
    private dados cDados;
    #region metodos dados do sistema
    /// <summary>
    /// Armazena ou busca valores usados no sistema gravados em sessão para suportar as constantes atualizações
    /// </summary>
    /// <param name="nome">Nome do valor a ser buscado ou gravado, se caso for informado apenas este parametro será retornado o valor citado</param>
    /// <param name="valor">Ao ser enviado este parametro o evento irá gravar o novo valor para o 'nome' informado</param>
    /// <returns></returns>
    public t valoresReuniao<t>(string nome = "", t valor = default(t), Boolean geral = false, Boolean limpar = false)
    {
        t retorno = default(t);
        string nomeBase = "geralReuniao" + typeof(t).Name;
        if (nome == "codigoItemAtual")
        {

        }
        if (!limpar)
        {
            if (nome.Length > 0)
            {
                Dictionary<string, t> valoresReuniao;
                if (geral)
                    valoresReuniao = (Dictionary<string, t>)HttpContext.Current.Application[nomeBase];
                else
                    valoresReuniao = (Dictionary<string, t>)HttpContext.Current.Session[nomeBase];
                if (valoresReuniao == null)
                    valoresReuniao = new Dictionary<string, t>();
                if (valor == null)
                {
                    if (valoresReuniao.ContainsKey(nome))
                        retorno = (t)valoresReuniao[nome];
                }
                else
                {
                    valoresReuniao[nome] = valor;
                }
                if (geral)
                    HttpContext.Current.Application[nomeBase] = valoresReuniao;
                else
                    HttpContext.Current.Session[nomeBase] = valoresReuniao;
            }
        }
        else
        {
            if (geral)
                HttpContext.Current.Application.Remove(nomeBase);
            else
                HttpContext.Current.Session.Remove(nomeBase);
        }
        return retorno;
    }
    #endregion

    #region Eventos Banco de Dados
    /// <summary>
    /// Grava o novo comentário no banco de dados
    /// </summary>
    /// <param name="comentario">comentário a ser gravado no item atual</param>
    /// <returns></returns>
    public Boolean enviarComentario(string comentario)
    {
        DataTable dtComentarios = valoresReuniao<DataTable>("dtComentarios");
        DataRow row = dtComentarios.NewRow();
        DateTime agora = DateTime.Now;
        Boolean retorno = false;
        if (cDados == null)
            cDados = funcoes();
        int referencia = 0;
        int quantidade = dtComentarios.Rows.Count;
        string comando = string.Format(@"INSERT INTO {0}.{1}.[ComentarioParticipanteEvento] (
        CodigoEvento
        ,CodigoParticipante
        ,CodigoObjetoItemPauta
        ,CodigoTipoObjetoItemPauta
        ,SequenciaComentario
        ,IndicaParticipanteConvidado
        ,DataComentario
        ,Comentario) VALUES (
        {2}, {3}, {4}, {5}, {6}, '{7}', GetDate(), '{9}')
             "
        , cDados.getDbName()
        , cDados.getDbOwner()
        , valoresReuniao<string>("codigoEvento")
        , valoresReuniao<string>("codigoUsuario")
        , valoresReuniao<string>("codigoItemAtual")
        , valoresReuniao<string>("tipoItemAtual")
        , quantidade
        , valoresReuniao<string>("tipoParticipante")
        , agora
        , comentario
        );
        if (cDados.execSQL(comando, ref referencia))
        {
            carregarComentarios();
            atualizarAtualizacaoStatus("dtComentarios", true);
            retorno = true;
        }
        return retorno;
    }
    /// <summary>
    /// Realiza o carregamento inicial de todos os valores necessários para o sistema inicialmente
    /// </summary>
    public Boolean carregarTudo(ref string mensagem)
    {
        Boolean retorno = false;
        try
        {
            valoresReuniao<DataTable>(nome: "", limpar: true);

            carregarItens();
            carregarDadosReuniao();
            carregarParticipantes();
            carregarComentarios();
            carregarAnexos();
            carregarDeliberacoes();
            carregarPlanos();
            carregarDadosItens();
            carregarCombos();
            carregarParametros();
            carregarStatusReuniao();
            retorno = true;
        }
        catch (Exception ext)
        {
            mensagem = "Ocorreu um erro ao carregar todos os dados do banco de dados. Motivo: " + ext.Message;
            retorno = false;
        }
        return retorno;
    }
    /// <summary>
    /// Carrega os dados do evento atual
    /// </summary>
    /// <returns></returns>
    public t carregarDadosEvento<t>(Boolean somenteEvento = false) where t : class, new()
    {
        DataTable dtEvento = new DataTable();
        DataTable dtObjetoAsociado = new DataTable();
        DataSet dsDadosAssociado = new DataSet();
        t retorno = new t();
        if (cDados == null)
            cDados = funcoes();
        string comandoEvento = string.Format("SELECT * FROM {0}.{1}.[Evento]   where CodigoEvento = {2}", cDados.getDbName(), cDados.getDbOwner(), valoresReuniao<string>("codigoEvento"));
        DataSet dadosEvento = cDados.getDataSet(comandoEvento);
        if (dadosEvento.Tables.Count > 0)
        {
            dtEvento = dadosEvento.Tables[0];
            valoresReuniao<DataTable>("dtEvento", dtEvento);
            if (!somenteEvento)
            {
                DataRow rowReuniao = dtEvento.Rows[0];
                valoresReuniao<string>("codigoEventoAssociacao", rowReuniao["CodigoObjetoAssociado"].ToString());
                string comandoTipo = string.Format("SELECT * FROM {0}.{1}.[TipoAssociacao] where CodigoTipoAssociacao = {2}", cDados.getDbName(), cDados.getDbOwner(), rowReuniao["CodigoTipoAssociacao"]);
                DataRow rowTipo = cDados.getDataSet(comandoTipo).Tables[0].Rows[0];
                string tabelaTipo = "";
                string colunaTipo = "";
                string comandoDadosTipo = "";
                string colunaTipoTitulo = "";
                switch (rowTipo["DescricaoTipoAssociacao"].ToString())
                {
                    case "Projeto":
                        tabelaTipo = "[Projeto]";
                        colunaTipo = "CodigoProjeto";
                        colunaTipoTitulo = "NomeProjeto";
                        comandoDadosTipo = string.Format("EXEC [p_getDadosProjetoReuniao] {0}", rowReuniao["CodigoObjetoAssociado"].ToString());
                        dsDadosAssociado = cDados.getDataSet(comandoDadosTipo);
                        break;
                    case "Unidade de Negócio":
                        tabelaTipo = "[UnidadeNegocio]";
                        colunaTipo = "CodigoUnidadeNegocio";
                        colunaTipoTitulo = "NomeUnidadeNegocio";
                        comandoDadosTipo = string.Format("EXEC [p_getDadosProjetoReuniao] {0}", rowReuniao["CodigoObjetoAssociado"].ToString());
                        dsDadosAssociado = cDados.getDataSet(comandoDadosTipo);
                        break;
                }
                string comandoAssociacao = string.Format("SELECT * FROM {0}.{1}.{2} where {3} = {4}", cDados.getDbName(), cDados.getDbOwner(), tabelaTipo, colunaTipo, rowReuniao["CodigoObjetoAssociado"].ToString());
                dtObjetoAsociado = cDados.getDataSet(comandoAssociacao).Tables[0];
                DataRow rowAssociacao = dtObjetoAsociado.Rows[0];
                valoresReuniao<string>("tipoObjetoEvento", rowTipo["DescricaoTipoAssociacao"].ToString());
                valoresReuniao<string>("nomeObjetoEvento", rowAssociacao[colunaTipoTitulo].ToString());
                valoresReuniao<DataTable>("dtObjetoAsociado", dtObjetoAsociado);
            }
        }
        if (dsDadosAssociado.Tables.Count > 0)
            valoresReuniao<DataSet>("dsDadosAssociado", dsDadosAssociado);
        if (typeof(t) == typeof(DataTable))
        {
            retorno = (t)Convert.ChangeType(dtEvento, typeof(t));
        }
        else
        {
            retorno = (t)Convert.ChangeType(dsDadosAssociado, typeof(t));
        }
        return retorno;
    }
    /// <summary>
    /// Carrega os dados do evento e o título da reunião atual de acordo com o registro já lido e guardado em sessão
    /// </summary>
    /// <returns></returns>
    public string carregarDadosReuniao()
    {
        string retorno = "";
        retorno = valoresReuniao<string>("tituloReuniao");
        if (retorno == null)
        {
            if (cDados == null)
                cDados = funcoes();
            carregarDadosEvento<DataSet>();

            retorno = valoresReuniao<string>("tipoObjetoEvento") + " - §";
            retorno += valoresReuniao<string>("nomeObjetoEvento");
        }
        valoresReuniao<string>("tituloReuniao", retorno);
        return retorno;
    }
    /// <summary>
    /// Carrega do banco de dados todos os itens da reunião/evento atual e grava em session
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarItens()
    {
        DataTable dtItens = valoresReuniao<DataTable>("dtItens");
        string comando = string.Format("select * from dbo.f_GetItensPautaReuniao({0})", valoresReuniao<string>("codigoEvento"));
        if (cDados == null)
            cDados = funcoes();
        dtItens = cDados.getDataSet(comando).Tables[0];
        if (dtItens.Rows.Count > 0)
        {
            dtItens.DefaultView.Sort = "SequenciaApresentacao";
            if (valoresReuniao<string>("codigoItemAtual") == null)
            {
                valoresReuniao<string>("codigoItemAtual", dtItens.Rows[0]["CodigoObjeto"].ToString());
                valoresReuniao<string>("tipoItemAtual", dtItens.Rows[0]["CodigoTipoObjeto"].ToString());
            }
            valoresReuniao<DataTable>("dtItens", dtItens);
        }
    }
    /// <summary>
    /// Carrega todos os participantes no banco para session, no qual têm permissão para participar da reunião/evento atual, também confere se o usuário atual está credenciado para continuar na pagina
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarParticipantes()
    {
        if (cDados == null)
            cDados = funcoes();
        string comando = String.Format(@"SELECT P.* FROM dbo.f_GetParticipantesReuniao({0}) AS p", valoresReuniao<string>("codigoEvento"));
        DataTable dtParticipantes = cDados.getDataSet(comando).Tables[0];
        if (dtParticipantes.Rows.Count > 0)
        {
            dtParticipantes.DefaultView.Sort = "NomeParticipante ASC";
            valoresReuniao<DataTable>("dtParticipantes", dtParticipantes);
            DataRow[] rows = dtParticipantes.Select(string.Format("CodigoParticipante = {0}", valoresReuniao<string>("codigoUsuario")));
            if (rows.Count() > 0)
            {
                DataRow row = rows[0];
                valoresReuniao<string>("tipoUsuario", row["TipoParticipante"].ToString());

            }
            else
            {

            }
        }
    }
    /// <summary>
    /// Carrega todos os comentários da banco de dados e os armazena em session para utilização do sistema
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarComentarios()
    {
        DataTable dtComentarios = valoresReuniao<DataTable>("dtComentarios");
        if (cDados == null)
            cDados = funcoes();
        string comando = string.Format(@"SELECT * FROM {0}.{1}.ComentarioParticipanteEvento 
            WHERE CodigoEvento = {2} 
            ORDER BY SequenciaComentario DESC"
            , cDados.getDbName(), cDados.getDbOwner(), valoresReuniao<string>("codigoEvento"));
        dtComentarios = cDados.getDataSet(comando).Tables[0];
        dtComentarios.DefaultView.Sort = "DataComentario DESC";
        valoresReuniao<DataTable>("dtComentarios", dtComentarios);
    }
    /// <summary>
    /// Carrega todas as deliberações do banco e armazena em session para uso do sistema
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarDeliberacoes()
    {
        DataTable dtDeliberacoes = valoresReuniao<DataTable>("dtDeliberacoes");
        if (cDados == null)
            cDados = funcoes();
        string comando = string.Format("SELECT * FROM {0}.{1}.ObjetoAssociadoEvento WHERE CodigoEvento = {2}", cDados.getDbName(), cDados.getDbOwner(), valoresReuniao<string>("codigoEvento"));
        dtDeliberacoes = cDados.getDataSet(comando).Tables[0];
        valoresReuniao<DataTable>("dtDeliberacoes", dtDeliberacoes);
    }
    /// <summary>
    /// Carrega todos os anexos vinculados a reunião/evento atual para session para ser usado pelo o sistema
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarAnexos()
    {
        DataTable dtAnexos = valoresReuniao<DataTable>("dtAnexos");
        if (cDados == null)
            cDados = funcoes();
        string comando = string.Format(@"SELECT u.NomeUsuario, i.NomeObjeto, p.CodigoAnexo, v.codigoSequencialAnexo, c.Anexo, a.CodigoUsuarioInclusao, a.DescricaoAnexo, a.Nome, a.DataInclusao, p.CodigoEvento, p.CodigoObjetoItemPauta 
            FROM {0}.{1}.AnexoItemPautaReuniao AS p 
            INNER JOIN {0}.{1}.AnexoVersao AS v ON p.CodigoAnexo = v.codigoAnexo
            INNER JOIN {0}.{1}.ConteudoAnexo AS c ON v.codigoSequencialAnexo = c.codigoSequencialAnexo
            INNER JOIN {0}.{1}.Anexo AS a ON p.CodigoAnexo = a.CodigoAnexo
            INNER JOIN {0}.{1}.Usuario AS u ON a.CodigoUsuarioInclusao = u.CodigoUsuario
            INNER JOIN dbo.f_GetItensPautaReuniao({2}) AS i ON p.CodigoObjetoItemPauta = i.CodigoObjeto
            WHERE p.CodigoEvento = {2} AND a.CodigoEntidade = {3} ", cDados.getDbName(), cDados.getDbOwner(), valoresReuniao<string>("codigoEvento"), valoresReuniao<string>("codigoEntidade"));
        dtAnexos = cDados.getDataSet(comando).Tables[0];
        if (dtAnexos != null && dtAnexos.Rows.Count > 0)
            dtAnexos.DefaultView.Sort = "DataInclusao DESC";
        valoresReuniao<DataTable>("dtAnexos", dtAnexos);
    }
    /// <summary>
    /// Carrega todos os planos do banco para a session para ficar disponível para o sistema
    /// </summary>
    /// <param name="carregarTabela">Informa se necessário atualizar as tabelas e listas</param>
    public void carregarPlanos()
    {
        DataTable dtPlanos = valoresReuniao<DataTable>("dtPlanos");
        DataTable dtPlanosItem = new DataTable();
        DataTable dtDadosRisco = valoresReuniao<DataTable>("dtDadosRisco");
        DataTable dtPlanosPendentes = new DataTable();
        if (cDados == null)
            cDados = funcoes();
        DataTable dtItens = valoresReuniao<DataTable>("dtItens");
        if (dtItens == null)
        {
            carregarItens();
            dtItens = valoresReuniao<DataTable>("dtItens");
        }
        if (dtItens.Rows.Count > 0)
        {
            string comandoI = String.Empty;
            DataSet dadosItem = new DataSet();
            foreach (DataRow linha in dtItens.Rows)
            {
                switch (linha["CodigoTipoObjeto"].ToString())
                {
                    case "11":
                        comandoI = string.Format("EXEC dbo.p_getDadosRiscoQuestao {0}", linha["CodigoObjeto"]);
                        if (comandoI != String.Empty)
                        {
                            dadosItem = cDados.getDataSet(comandoI);
                            if (dtPlanos == null || dtPlanos.Rows.Count <= 0)
                                dtPlanos = colunaCodigoItemPlanos(dadosItem.Tables[1], linha["CodigoObjeto"].ToString());
                            else
                            {
                                dtPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao <> {0}", linha["CodigoObjeto"].ToString())).CopyToDataTable();
                                dtPlanos.Merge(colunaCodigoItemPlanos(dadosItem.Tables[1], linha["CodigoObjeto"].ToString()));
                            }
                        }
                        break;
                    case "0":
                        comandoI = string.Format("exec [dbo].[p_getPendenciasReuniaoAnterior] {0}", valoresReuniao<string>("codigoEvento"));
                        dtPlanosPendentes = cDados.getDataSet(comandoI).Tables[0];
                        comandoI = String.Empty;
                        break;
                }
            }
        }

        valoresReuniao<DataTable>("dtPlanos", dtPlanos);
        valoresReuniao<DataTable>("dtPlanosPendentes", dtPlanosPendentes);
    }
    /// <summary>
    /// Carrega todos os combos que serão alimentados pelo o banco de dados
    /// </summary>
    public void carregarCombos()
    {
        if (cDados == null)
            cDados = funcoes();
        string comandoU = string.Format("SELECT u.CodigoUsuario, u.NomeUsuario FROM Usuario AS u WHERE u.DataExclusao IS NULL AND u.NomeUsuario != '' ORDER BY u.NomeUsuario ASC", cDados.getDbName(), cDados.getDbOwner());
        string comandoS = string.Format("SELECT s.CodigoStatusTarefa, s.DescricaoStatusTarefa FROM {0}.{1}.StatusTarefa AS s WHERE DescricaoStatusTarefa != '' ORDER BY S.DescricaoStatusTarefa ASC", cDados.getDbName(), cDados.getDbOwner());
        DataTable dtUsuarios = cDados.getDataSet(comandoU).Tables[0];
        DataTable dtStatusTarefa = cDados.getDataSet(comandoS).Tables[0];
        valoresReuniao<DataTable>("dtUsuarios", dtUsuarios);
        valoresReuniao<DataTable>("dtStatusTarefa", dtStatusTarefa);
    }
    /// <summary>
    /// Carrega todos o parametros necessários para reunião
    /// </summary>
    public string[] carregarParametros()
    {
        string[] retorno = new String[1];
        if (cDados == null)
            cDados = funcoes();
        string comando = string.Format(@"SELECT p.Valor 
            FROM {0}.{1}.ParametroConfiguracaoSistema AS p
            WHERE CodigoEntidade = {2} AND Parametro = 'tamanhoMaximoArquivoAnexoEmMegaBytes'"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , valoresReuniao<string>("codigoEntidade"));
        DataRow row = cDados.getDataSet(comando).Tables[0].Rows[0];
        int tamanho = int.Parse(row["Valor"].ToString()) * (1024 * 1024);
        retorno[0] = tamanho.ToString();
        return retorno;
    }
    /// <summary>
    /// Verifica a situação da reunião atualmente e o responsável(moderador) da reunião
    /// </summary>
    public void carregarStatusReuniao()
    {
        if (cDados == null)
            cDados = funcoes();
        string comando = string.Format(@"SELECT TOP 1 e.InicioReal, e.TerminoReal, e.InicioPrevisto, e.TerminoPrevisto, u.CodigoUsuario, u.NomeUsuario 
            FROM {0}.{1}.Evento AS e 
            INNER JOIN {0}.{1}.Usuario AS u ON e.CodigoResponsavelEvento = u.CodigoUsuario 
            WHERE CodigoEvento = {2} AND CodigoEntidade = {3}"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , valoresReuniao<string>("codigoEvento")
            , valoresReuniao<string>("codigoEntidade"));
        DataRow row = cDados.getDataSet(comando).Tables[0].Rows[0];
        if (row["InicioReal"].ToString() == "")
        {
            valoresReuniao<string>("reuniaoIniciada", "");
        }
        else if (row["InicioReal"].ToString() != "" && row["TerminoReal"].ToString() == "")
        {
            valoresReuniao<string>("reuniaoIniciada", "true");
            valoresReuniao<string>("usuarioAvisado", "false");
            valoresReuniao<string>("horarioInicio", row["InicioReal"].ToString().Substring(11, 5));
        }
        else if (row["InicioReal"].ToString() != "" && row["TerminoReal"].ToString() != "")
        {
            valoresReuniao<string>("reuniaoIniciada", "false");
            valoresReuniao<string>("usuarioAvisado", "false");
            valoresReuniao<string>("horarioFim", row["TerminoReal"].ToString().Substring(11, 5));
        }
        valoresReuniao<string>("codigoModerador", row["CodigoUsuario"].ToString());
        valoresReuniao<string>("nomeModerador", row["NomeUsuario"].ToString());

    }
    /// <summary>
    /// Carrega todos o dados dos itens do evento atual
    /// </summary>
    public void carregarDadosItens()
    {
        DataTable dtItens = valoresReuniao<DataTable>("dtItens");
        DataTable dtDadosTarefaRecursos = new DataTable();
        DataTable dtDadosTarefa = new DataTable();
        DataTable dtDadosIndicador = new DataTable();
        DataTable dtDadosRisco = new DataTable();
        DataSet dsResultado = new DataSet();
        DataTable dtPlanos = valoresReuniao<DataTable>("dtPlanos");
        string anoAtual = DateTime.Now.ToString("yyyy");
        string comando = "";
        if (cDados == null)
            cDados = funcoes();
        if (dtItens == null)
        {
            carregarItens();
            dtItens = valoresReuniao<DataTable>("dtItens");
        }
        if (dtItens.Rows.Count > 0)
        {
            foreach (DataRow row in dtItens.Rows)
            {
                switch (row["CodigoTipoObjeto"].ToString())
                {
                    case "11":
                        comando = string.Format("EXEC dbo.p_getDadosRiscoQuestao {0}", row["CodigoObjeto"]);
                        if (comando != String.Empty)
                        {
                            dsResultado = cDados.getDataSet(comando);
                            if (dtDadosRisco != null && dtDadosRisco.Rows.Count > 0)
                                dtDadosRisco.Merge(dsResultado.Tables[0]);
                            else
                                dtDadosRisco = dsResultado.Tables[0];
                            comando = String.Empty;
                            row["DescricaoTipoObjeto"] = dsResultado.Tables[0].Rows[0]["Indica_Risco_Questao"];
                        }
                        break;
                    case "5":
                        comando = string.Format("EXEC dbo.p_getDadosTarefaCronograma @CodigoReunião = {0},  @CodigoTarefa = {1}", valoresReuniao<string>("codigoEvento"), row["CodigoObjeto"].ToString());
                        dsResultado = cDados.getDataSet(comando);
                        if (dtDadosTarefa == null)
                            dtDadosTarefa = dsResultado.Tables[0];
                        else
                            dtDadosTarefa.Merge(dsResultado.Tables[0]);
                        if (dtDadosTarefaRecursos == null)
                            dtDadosTarefaRecursos = dsResultado.Tables[1];
                        else
                            dtDadosTarefaRecursos.Merge(dsResultado.Tables[1]);
                        break;
                    case "8":
                        comando = string.Format("EXEC dbo.p_getDadosMetaIndicadorOperacional @codigoIndicador = {0}, @codigoProjeto = {1}, @Ano = {2}", row["CodigoObjeto"].ToString(), valoresReuniao<string>("codigoEventoAssociacao"), anoAtual);
                        dsResultado = cDados.getDataSet(comando);
                        if (dtDadosIndicador == null)
                            dtDadosIndicador = dsResultado.Tables[0];
                        else
                            dtDadosIndicador.Merge(dsResultado.Tables[0]);
                        if (dtPlanos == null)
                            dtPlanos = colunaCodigoItemPlanos(dsResultado.Tables[1], row["CodigoObjeto"].ToString());
                        else
                        {
                            if (dtPlanos.Rows.Count > 0)
                            {
                                DataRow[] rowsPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao <> {0}", row["CodigoObjeto"].ToString()));
                                if (rowsPlanos.Count() > 0)
                                {
                                    dtPlanos = rowsPlanos.CopyToDataTable();
                                    dtPlanos.Merge(colunaCodigoItemPlanos(dsResultado.Tables[1], row["CodigoObjeto"].ToString()));
                                }
                                else
                                {
                                    dtPlanos = colunaCodigoItemPlanos(dsResultado.Tables[1], row["CodigoObjeto"].ToString());
                                }
                            }
                            else
                            {
                                dtPlanos = colunaCodigoItemPlanos(dsResultado.Tables[1], row["CodigoObjeto"].ToString());
                            }
                        }
                        break;
                }
            }
            if (dtDadosRisco != null)
            {
                valoresReuniao<DataTable>("dtItens", dtItens);
                valoresReuniao<DataTable>("dtDadosRisco", dtDadosRisco);
            }
            if (dtDadosTarefa != null)
                valoresReuniao<DataTable>("dtDadosTarefa", dtDadosTarefa);
            if (dtDadosTarefaRecursos != null)
                valoresReuniao<DataTable>("dtDadosTarefaRecursos", dtDadosTarefaRecursos);
            if (dtDadosIndicador != null)
                valoresReuniao<DataTable>("dtDadosIndicador", dtDadosIndicador);
            if (dtPlanos != null)
                valoresReuniao<DataTable>("dtPlanos", dtPlanos);
        }
    }
    /// <summary>
    /// Grava um novo plano de ação ou ao informar o "numero" grava as alteraçõs do plano de ação
    /// </summary>
    /// <param name="numero"></param>
    public void gravarAlterarPlanos(List<string> valoresPlanos, int numero = -1, int codigoTarefa = -1)
    {
        dados cDados = funcoes();
        int referencia = 0;
        List<string> valores = valoresPlanos;
        if (numero < 0 && codigoTarefa < 0)
        {
            string comandoTI = string.Format(@"BEGIN DECLARE @CodigoToDoList int
                    IF NOT EXISTS(SELECT 1 FROM {0}.{1}.ToDoList AS tdl WHERE tdl.CodigoObjetoAssociado = {4} AND CodigoTipoAssociacao = {3})
                    BEGIN
                        INSERT INTO {0}.{1}.ToDoList(DataInclusao, CodigoUsuarioInclusao, CodigoUsuarioResponsavelToDoList, CodigoEntidade, CodigoObjetoAssociado, CodigoTipoAssociacao)
                                VALUES(GETDATE(), {2}, {2}, {5}, {4}, {3})
                        SELECT @CodigoToDoList = SCOPE_IDENTITY()
                    END
                        ELSE
                            SELECT TOP 1 @CodigoToDoList = CodigoToDoList 
                              FROM {0}.{1}.ToDoList AS tdl 
                             WHERE tdl.CodigoObjetoAssociado = {4} 
                               AND CodigoTipoAssociacao = {3}
                                AND CodigoEntidade = {5}       
                    
                    
                    INSERT INTO {0}.{1}.TarefaToDoList(CodigoToDoList, DescricaoTarefa, InicioPrevisto, TerminoPrevisto, InicioReal, TerminoReal
	                                                  ,CodigoUsuarioResponsavelTarefa, PercentualConcluido, Anotacoes, Prioridade, CodigoStatusTarefa
                                                      ,EsforcoPrevisto, EsforcoReal, CustoPrevisto, CustoReal, CodigoEvento)
                                        VALUES(@CodigoToDoList, '{7}', '{8}', '{9}', '{10}', '{11}', {12}, 0, '{13}', '{14}', {15}, {16}, {17}, {18}, {19}, {6})
                END"
                    , cDados.getDbName()
                    , cDados.getDbOwner()
                    , valoresReuniao<string>("codigoUsuario")
                    , valoresReuniao<string>("tipoItemAtual") //3
                    , valoresReuniao<string>("codigoItemAtual")
                    , valoresReuniao<string>("codigoEntidade")
                    , valoresReuniao<string>("codigoEvento") //6
                    , valores[0]
                    , valores[4]
                    , valores[5]
                    , valores[8]
                    , valores[9]
                    , valores[2]
                    , valores[12]
                    , valores[1]
                    , valores[3]
                    , valores[6]
                    , valores[10]
                    , valores[7]
                    , valores[11]
                    );
            if (cDados.execSQL(comandoTI, ref referencia))
            {
                carregarPlanos();
                carregarDadosItens();
            }
        }
        else if (numero > 0 || codigoTarefa > 0)
        {
            DataRow row;
            if (numero > 0 && codigoTarefa < 0)
            {
                DataTable dtPlanos = valoresReuniao<DataTable>("dtPlanos").Copy();
                if (valoresReuniao<string>("filtroConcluidoPlanos") != "false")
                    dtPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao = {0} and CodigoStatusTarefa <> 2 and CodigoEvento is not null", valoresReuniao<string>("codigoItemAtual"))).CopyToDataTable();
                else
                    dtPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao = {0}", valoresReuniao<string>("codigoItemAtual"))).CopyToDataTable();
                row = dtPlanos.Rows[numero];
                codigoTarefa = int.Parse(row["CodigoTarefa"].ToString());
            }
            string comando = string.Format(@"
                BEGIN UPDATE {0}.{1}.TarefaToDoList SET 
                DescricaoTarefa = '{3}', 
                InicioPrevisto = '{4}', 
                TerminoPrevisto = '{5}', 
                InicioReal = '{6}', 
                TerminoReal = '{7}', 
                CodigoUsuarioResponsavelTarefa = {8}, 
                Anotacoes = '{9}', 
                Prioridade = '{10}', 
                CodigoStatusTarefa = {11}, 
                EsforcoPrevisto = {12}, 
                EsforcoReal = {13}, 
                CustoPrevisto = {14}, 
                CustoReal = {15}
                WHERE CodigoTarefa = {2}
                END
                "
                , cDados.getDbName()
                , cDados.getDbOwner()
                , codigoTarefa
                , valores[0]
                , valores[4]
                , valores[5]
                , valores[8]
                , valores[9]
                , valores[2]
                , valores[12]
                , valores[1]
                , valores[3]
                , valores[6].Replace(".", "").Replace(',', '.')
                , valores[10].Replace(".", "").Replace(',', '.')
                , valores[7].Replace(".", "").Replace(',', '.')
                , valores[11].Replace(".", "").Replace(',', '.')
                );
            if (cDados.execSQL(comando, ref referencia))
            {
                valoresReuniao<string>("editarPlanos", "false");
                carregarPlanos();
                carregarDadosItens();
            }
        }
        atualizarAtualizacaoStatus("dtPlanos", true);
    }
    /// <summary>
    /// Grava as alterações realizadas ao se digitar na deliberação atual.
    /// </summary>
    /// <param name="valor"></param>
    /// <returns></returns>
    public string gravarDeliberacao(string valor)
    {
        string retorno = "true";
        try
        {
            dados cDados = funcoes();
            int referencia = 0;
            DataTable dtDeliberacoes = valoresReuniao<DataTable>("dtDeliberacoes");
            DataRow row = dtDeliberacoes.Select(string.Format("CodigoObjetoAssociado = {0}", valoresReuniao<string>("codigoItemAtual")))[0];
            row["Deliberacao"] = valor;
            valoresReuniao<DataTable>("dtDeliberacoes", dtDeliberacoes);
            string comando = string.Format(@"UPDATE {0}.{1}.ObjetoAssociadoEvento 
                SET DataDeliberacao = GetDate(), Deliberacao = '{2}' 
                WHERE CodigoEvento = {3} AND CodigoObjetoAssociado = {4} "
                , cDados.getDbName(), cDados.getDbOwner(), valor, valoresReuniao<string>("codigoEvento"), valoresReuniao<string>("codigoItemAtual"));
            if (!cDados.execSQL(comando, ref referencia))
            {
                retorno = "Não foi possível salvar as alterações no banco de dados. Informe o administrador do sistema.";
            }
            atualizarAtualizacaoStatus("dtDeliberacoes", true);
        }
        catch (Exception erro)
        {
            retorno = "Não foi possível enviar alteração '" + erro.Message + "'. Informe o administrador do sistema.";
        }
        return retorno;
    }
    /// <summary>
    /// Realiza a marcação do participante se prente ou não no banco de dados
    /// </summary>
    /// <param name="todos">especificar se deverá ser marcados/desmarcados todos ou apenas um</param>
    /// <param name="linha">caso seja para marcar/desmarcar apenas um, informar a linha a ser alterada</param>
    public DataTable marcarParticipanteEvento(Boolean todos, int linha = -1)
    {
        string valor = String.Empty;
        string comando = String.Empty;
        string nomeTabela = "";
        int quantidade = 0;
        if (cDados == null)
            cDados = funcoes();
        int referencia = 0;
        string complemento = String.Empty;
        DataTable dtParticipantes = valoresReuniao<DataTable>("dtParticipantes");
        DataView dv = dtParticipantes.DefaultView;
        DataRow row = null;
        dv.Sort = "NomeParticipante Asc";
        dtParticipantes = dv.ToTable();
        if (!todos)
        {
            row = dtParticipantes.Rows[linha];
            valor = row["IndicaParticipantePresente"].ToString() == "S" ? "N" : "S";
            row["IndicaParticipantePresente"] = valor;
            if (row["TipoParticipante"].ToString() == "C")
                nomeTabela = "CodigoConvidadoEvento";
            else
                nomeTabela = "CodigoParticipante";
            complemento = string.Format("AND {1} = {0}", row["CodigoParticipante"].ToString(), nomeTabela);
        }
        else
        {
            quantidade = dtParticipantes.Select("IndicaParticipantePresente = 'S'").Length;
            valor = quantidade > 0 ? "N" : "S";
            foreach (DataRow rowP in dtParticipantes.Rows)
            {
                rowP["IndicaParticipantePresente"] = valor;
            }
        }
        if (row != null && row["TipoParticipante"].ToString() == "C")
        {
            comando = string.Format(@"UPDATE {0}.{1}.ConvidadoExternoEvento 
                SET IndicaConvidadoPresente = '{2}'
                WHERE CodigoEvento = {3} {4} "
                , cDados.getDbName()
                , cDados.getDbOwner()
                , valor
                , valoresReuniao<string>("codigoEvento")
                , complemento
                );
        }
        else
        {
            comando = string.Format(@"UPDATE {0}.{1}.ParticipanteEvento 
        SET IndicaParticipantePresente = '{2}'
        WHERE CodigoEvento = {3} {4}"
                , cDados.getDbName()
                , cDados.getDbOwner()
                , valor
                , valoresReuniao<string>("codigoEvento")
                , complemento
                );
        }

        if (cDados.execSQL(comando, ref referencia))
        {
            valoresReuniao<DataTable>("dtParticipantes", dtParticipantes);
            carregarParticipantes();
            atualizarAtualizacaoStatus("dtParticipantes", true);
        }
        return dtParticipantes;
    }
    /// <summary>
    /// Excluir o plano de ação do banco
    /// </summary>
    /// <param name="linha">número da linha  ser excluida</param>
    public void excluirPlanos(int linha, int codigoTarefa = -1)
    {
        DataTable dtPlanos = new DataTable();
        DataTable dtPendentes = new DataTable();
        DataRow row;
        if (codigoTarefa < 0)
        {
            dtPlanos = valoresReuniao<DataTable>("dtPlanos").Copy();
            if (valoresReuniao<string>("filtroConcluidoPlanos") != "false")
                dtPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao = {0} and CodigoStatusTarefa <> 2 and CodigoEvento is not null", valoresReuniao<string>("codigoItemAtual"))).CopyToDataTable();
            else
                dtPlanos = dtPlanos.Select(string.Format("CodigoItemReuniao = {0}", valoresReuniao<string>("codigoItemAtual"))).CopyToDataTable();
            row = dtPlanos.Rows[linha];
            codigoTarefa = int.Parse(row["CodigoTarefa"].ToString());
        }
        else
        {
            dtPendentes = valoresReuniao<DataTable>("dtPlanosPendentes").Copy();
            row = dtPendentes.Select(string.Format("codigoTarefa = {0}", codigoTarefa))[0];
        }
        string nome = row["DescricaoTarefa"].ToString();
        dados cDados = funcoes();
        string comando = string.Format("UPDATE {0}.{1}.TarefaToDoList SET DataExclusao = getdate(), CodigoUsuarioExclusao = {2} WHERE CodigoTarefa = {3}", cDados.getDbName(), cDados.getDbOwner(), valoresReuniao<string>("codigoUsuario"), codigoTarefa);
        int referencia = 0;
        cDados.execSQL(comando, ref referencia);
        atualizarAtualizacaoStatus("dtPlanos", true);

    }
    /// <summary>
    /// Cria a tabela geral que será usada pelo o sistema
    /// </summary>
    /// <returns>Retorna a tabela criada</returns>
    public DataTable criarTabelaStatus()
    {
        DataTable retorno = null;
        DataTable data = valoresReuniao<DataTable>("dtAtualizacoes", geral: true);
        string[] colunas = { "CodigoEvento", "NomeTabela", "UltimaAtualizacao", "CodigoUsuario", "NomeUsuario" };
        string[] tabelas = { "dtItens", "dtParticipantes", "dtAnexos", "dtComentarios", "dtDeliberacoes", "dtPlanos", "dtStatus" };
        Boolean criarColunas = false;
        Boolean criarValores = false;
        if (data != null)
        {
            DataRow[] rows = data.Select(string.Format("CodigoEvento = '{0}'", valoresReuniao<string>("codigoEvento")));
            if (rows.Length == tabelas.Length)
                return data;
            else
            {
                retorno = data;
                criarValores = true;
            }
        }
        else
        {
            retorno = new DataTable();
            criarColunas = true;
        }
        if (criarColunas || criarValores)
        {
            if (criarColunas)
            {
                foreach (string nome in colunas)
                {
                    retorno.Columns.Add(nome, typeof(string));
                }
                retorno.DefaultView.Sort = "UltimaAtualizacao DESC";
            }
            if (criarValores)
            {
                foreach (string valor in tabelas)
                {
                    DataRow row = retorno.NewRow();
                    row[0] = valoresReuniao<string>("codigoEvento");
                    row[1] = valor;
                    row[2] = DateTime.Now.ToString();
                    row[3] = valoresReuniao<string>("codigoUsuario");
                    row[4] = valoresReuniao<string>("nomeUsuario");
                    retorno.Rows.Add(row);
                }
            }
        }
        return retorno;
    }
    /// <summary>
    /// Marca no banco de dados os que foram habilitados como item concluídos
    /// </summary>
    /// <param name="indice">Número de indice da tabela a ser marcada</param>
    /// <param name="marca">Valor a ser marcado no banco</param>
    /// <returns></returns>
    public Boolean marcarDeliberacao(int indice, string marca)
    {
        Boolean retorno = true;
        if (valoresReuniao<string>("reuniaoIniciada") != "false")
        {

            dados cDados = funcoes();
            int referencia = 0;
            string valor = marca.ToUpper();
            DataRow row = valoresReuniao<DataTable>("dtItens").Rows[indice];
            string comando = string.Format(@"UPDATE {0}.{1}.ObjetoAssociadoEvento 
            SET IndicaItemTratado = '{2}' 
            WHERE CodigoEvento = {3} AND CodigoObjetoAssociado = {4}"
                , cDados.getDbName(), cDados.getDbOwner(), valor, valoresReuniao<string>("codigoEvento"), row["CodigoObjeto"]);
            if (!cDados.execSQL(comando, ref referencia))
            {
                retorno = false;
            }
            else
                carregarItens();
        }
        return retorno;
    }
    /// <summary>
    /// Realiza o inicio e o fim da reunião
    /// </summary>
    /// <param name="iniciar">especifica se será controle para iniciar ou para finalizar</param>
    /// <param name="data">data que será utilizada caso null será a atual</param>
    /// <param name="hora">horário que será utilizado caso null será a atual</param>
    public Boolean iniciarFinalizarReuniao(Boolean iniciar, String data = null, string hora = null)
    {
        dados cDados = funcoes();
        Boolean retorno = false;
        int referencia = 0;
        string comando = "";
        string dataCompleta = "";

        if (iniciar)
        {
            if (data == null || hora == null)
            {
                dataCompleta = "getdate()";

            }
            else
            {
                string[] datas = data.Split('/');
                data = datas[2] + "-" + datas[1] + "-" + datas[0] + " " + hora + ":00.000";
                dataCompleta = "convert(Datetime, '" + data + "', 120)";

            }
            comando = string.Format(@"UPDATE {0}.{1}.Evento 
            SET InicioReal = {2}, DataUltimaAlteracao = getdate(), CodigoUsuarioUltimaAlteracao = {3} 
            WHERE CodigoEvento = {4} "
                , cDados.getDbName(), cDados.getDbOwner(), dataCompleta, valoresReuniao<string>("codigoUsuario"), valoresReuniao<string>("codigoEvento"));

            if (valoresReuniao<string>("codigoUsuario") == valoresReuniao<string>("codigoModerador"))
            {
                valoresReuniao<DataTable>("dtAtualizacoes", criarTabelaStatus(), geral: true);
            }
        }
        else
        {
            DataTable dtParticipantes = valoresReuniao<DataTable>("dtParticipantes");
            DataTable dtItens = valoresReuniao<DataTable>("dtItens");
            Boolean ItensOk = dtItens.Select("IndicaItemTratado = 'S'").Length > 0;
            Boolean ParticipantesOk = dtParticipantes.Select("IndicaParticipantePresente = 'S'").Length > 0;
            if (ItensOk && ParticipantesOk)
            {
                if (data == null || hora == null || data == "" || hora == "")
                {
                    dataCompleta = "getdate()";
                }
                else
                {
                    string[] datas = data.Split('/');
                    data = datas[2] + "-" + datas[1] + "-" + datas[0] + " " + hora + ":00.000";
                    dataCompleta = "convert(Datetime, '" + data + "', 120)";
                }
                comando = string.Format(@"UPDATE {0}.{1}.Evento 
                SET TerminoReal = {2}, DataUltimaAlteracao = getdate(), CodigoUsuarioUltimaAlteracao = {3}
                WHERE CodigoEvento = {4} "
                        , cDados.getDbName(), cDados.getDbOwner(), dataCompleta, valoresReuniao<string>("codigoUsuario"), valoresReuniao<string>("codigoEvento"));

                if (valoresReuniao<string>("codigoUsuario") == valoresReuniao<string>("codigoModerador"))
                {
                    excluirTabelaStatus();
                }
            }
        }
        if (comando != "" && cDados.execSQL(comando, ref referencia))
        {

            if (iniciar)
            {
                // MessageBox.Show("Reunião iniciada com sucesso! Para finalizar é necessário informar os participantes presentes na reunião no menu \"Participantes\" .", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                valoresReuniao<string>("reuniaoIniciada", "true");
                if (data == null || hora == null || data == "" || hora == "")
                    valoresReuniao<string>("horarioInicio", DateTime.Now.ToString().Substring(11, 5));
                else
                    valoresReuniao<string>("horarioInicio", dataCompleta.Substring(11, 5));
            }
            else
            {
                //MessageBox.Show("Reunião finalizada com sucesso! Após a finalização não será mais possível realizar alterações nesta reunião apenas consultas.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                valoresReuniao<string>("reuniaoIniciada", "false");
                if (data == null || hora == null || data == "" || hora == "")
                    valoresReuniao<string>("horarioFim", DateTime.Now.ToString().Substring(11, 5));
                else
                    valoresReuniao<string>("horarioFim", dataCompleta.Substring(11, 5));
            }
            retorno = true;
            atualizarAtualizacaoStatus("dtStatus", true);
        }
        if (retorno)
            carregarStatusReuniao();
        return retorno;
    }
    #endregion

    #region metodos diversos
    public Dictionary<string, string> criarGraficos(string tipo, string codigo)
    {
        Dictionary<string, string> retorno = new Dictionary<string, string>();
        StringBuilder xml = new StringBuilder();
        string codigoItem = valoresReuniao<string>("codigoItemAtual");
        DataTable dtItens;
        string nomeGrafico = "";
        if (cDados == null)
            cDados = funcoes();
        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";
        string nomeBase = @"/ArquivosTemporarios/metaItemObjetoAtualReuniao_" + valoresReuniao<string>("codigoUsuario") + "_" + valoresReuniao<string>("codigoEvento");


        switch (tipo)
        {
            case "metasItem":
                nomeGrafico = nomeBase + "_" + tipo + "_" + codigo + "_" + codigoItem + "_" + dataHora;
                ///Valores Básicos
                retorno["grafico_titulo"] = "Desempenho";
                retorno["grafico_swf"] = "../Flashs/MSCombi2D.swf";
                retorno["grafico_xml"] = "";
                retorno["grafico_xmlzoom"] = "";
                retorno["alturaGrafico"] = "400";
                retorno["larguraGrafico"] = "1000";
                retorno["permissaoLink"] = false.ToString();
                retorno["divGrafico"] = "divDadosIndicadorGrafico";
                ///

                string categorias = string.Empty;
                string datasets = string.Empty;
                string linhas = string.Empty;
                Dictionary<string, string> cor = coresMestasIndicadores();
                dtItens = valoresReuniao<DataTable>("dtItens");
                DataTable dtDadosIndicador = valoresReuniao<DataTable>("dtDadosIndicador");
                if (dtItens != null && dtDadosIndicador != null)
                {
                    string codigoItemAtual = codigo.Split('_')[0];
                    string codigoMetaAtual = codigo.Split('_')[1];
                    DataRow rowItem = dtItens.Select(string.Format("CodigoObjeto = {0}", codigoItemAtual))[0];
                    DataTable metas = dtDadosIndicador.Select(string.Format("CodigoIndicador = {0} and CodigoMetaOperacional = {1}", codigoItemAtual, codigoMetaAtual)).CopyToDataTable();
                    xml.Append(string.Format(@"<chart labelDisplay='Rotate' slantLabels='1' caption='Ano {0}' showValues='0' palette='2' divLineDecimalPrecision='1' limitsDecimalPrecision='1' PYAxisName='Valores' SYAxisName='Metas' numberPrefix='R$' formatNumberScale='0' plotGradientColor='' >", metas.Rows[0]["Ano"].ToString()));
                    foreach (DataRow row in metas.Rows)
                    {
                        categorias += string.Format("<category label='{0}' baseFontSize='2' />", row["DescricaoPeriodo"].ToString());
                        datasets += string.Format("<set value='{0}' color='{1}' />", row["ValorResultadoPeriodo"].ToString().Replace(",", "."), cor[row["CorDesempenhoPeriodo"].ToString()]);
                        //datasets += string.Format("{2}<set value='{0}' color='{1}' />{3}", row["ValorResultadoPeriodo"].ToString().Replace(",", "."), cor[row["CorDesempenhoPeriodo"].ToString()], string.Format("<dataset seriesName='{0}' >", metas.Rows[0]["NomeIndicador"]), "</dataset>");
                        linhas += string.Format("<set value='{0}' />", row["ValorMetaPeriodo"].ToString().Replace(",", "."));
                    }
                    xml.Append("<categories >" + categorias + "</categories>");
                    xml.Append(string.Format("<dataset seriesName='{0}' >", metas.Rows[0]["NomeIndicador"]) + datasets + "</dataset>");
                    //xml.Append(datasets);
                    xml.Append("<dataset seriesName='Metas' renderAs='Line' color='BBDA00' anchorSides='4' anchorRadius='5' anchorBorderColor='000000' anchorBorderThickness='1'>" + linhas + "</dataset>");
                    xml.Append("</chart>");
                }
                break;
            case "projetoItem":
                string[] codigos = codigo.Split('§');
                nomeGrafico = nomeBase + "_" + tipo + "_" + codigos[1] + "_" + dataHora;
                ///Valores Básicos
                retorno["grafico_titulo"] = codigos[0];
                retorno["grafico_swf"] = "../Flashs/Bar2D.swf";
                retorno["grafico_xml"] = "";
                retorno["grafico_xmlzoom"] = "";
                retorno["alturaGrafico"] = "150";
                retorno["larguraGrafico"] = "100%";
                retorno["permissaoLink"] = false.ToString();
                retorno["divGrafico"] = codigos[1];
                retorno["naoMostrar"] = "false";
                ///
                DataSet dsDadosAssociado = carregarDadosEvento<DataSet>();
                if (dsDadosAssociado.Tables.Count > 0 && dsDadosAssociado.Tables[0].Rows.Count > 0 && dsDadosAssociado.Tables[1].Rows.Count > 0)
                {
                    DataRow rowObjeto = dsDadosAssociado.Tables[0].Rows[0];
                    string perRO = "", perPO = "", corPO = "BEBEBE", corRO = "", valorPO = "0", valorRO = "0";
                    switch (codigos[0])
                    {
                        case "Físico":
                            if (rowObjeto["percentualPrevistoRealizacao"].ToString().Length > 0)
                                perPO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["percentualPrevistoRealizacao"].ToString()));
                            if (rowObjeto["percentualRealizacao"].ToString().Length > 0)
                                perRO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["percentualRealizacao"].ToString()));
                            if (rowObjeto["corDesempenhoFisico"].ToString().Length > 0)
                                corRO = coresMestasIndicadores()[rowObjeto["corDesempenhoFisico"].ToString()];
                            if ((perPO == ""
                                && perRO == "") ||
                                (decimal.Parse(rowObjeto["percentualPrevistoRealizacao"].ToString()) <= 0
                                && decimal.Parse(rowObjeto["percentualRealizacao"].ToString()) <= 0)
                                )
                            {
                                retorno["naoMostrar"] = "true";
                            }
                            break;
                        case "Despesa":
                            if (rowObjeto["PercentualFinanceiroPrevisto"].ToString().Length > 0)
                                perPO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["PercentualFinanceiroPrevisto"].ToString()));
                            if (rowObjeto["PercentualFinanceiroRealizado"].ToString().Length > 0)
                                perRO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["PercentualFinanceiroRealizado"].ToString()));
                            if (rowObjeto["corDesempenhoCusto"].ToString().Length > 0)
                                corRO = coresMestasIndicadores()[rowObjeto["corDesempenhoCusto"].ToString()];
                            if (rowObjeto["valorCustoPrevisto"].ToString().Length > 0)
                                valorPO = string.Format("R$ {0:n2}", decimal.Parse(rowObjeto["valorCustoPrevisto"].ToString()));
                            if (rowObjeto["valorCustoRealizado"].ToString().Length > 0)
                                valorRO = string.Format("R$ {0:n2}", decimal.Parse(rowObjeto["valorCustoRealizado"].ToString()));
                            if ((perPO == ""
                                && perRO == "") ||
                                (decimal.Parse(rowObjeto["PercentualFinanceiroPrevisto"].ToString()) <= 0
                                && decimal.Parse(rowObjeto["PercentualFinanceiroRealizado"].ToString()) <= 0)
                                )
                            {
                                retorno["naoMostrar"] = "true";
                            }
                            break;
                        case "Receita":
                            if (rowObjeto["PercentualReceitaPrevisto"].ToString().Length > 0)
                                perPO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["PercentualReceitaPrevisto"].ToString()));
                            if (rowObjeto["PercentualReceitaRealizado"].ToString().Length > 0)
                                perRO = string.Format("{0:#,##}", decimal.Parse(rowObjeto["PercentualReceitaRealizado"].ToString()));
                            if (rowObjeto["corDesempenhoReceita"].ToString().Length > 0)
                                corRO = coresMestasIndicadores()[rowObjeto["corDesempenhoReceita"].ToString()];
                            if (rowObjeto["valorReceitaPrevisto"].ToString().Length > 0)
                                valorPO = string.Format("R$ {0:n2}", decimal.Parse(rowObjeto["valorReceitaPrevisto"].ToString()));
                            if (rowObjeto["valorReceitaRealizado"].ToString().Length > 0)
                                valorRO = string.Format("R$ {0:n2}", decimal.Parse(rowObjeto["valorReceitaRealizado"].ToString()));
                            if ((perPO == ""
                                && perRO == "") ||
                                (decimal.Parse(rowObjeto["PercentualReceitaPrevisto"].ToString()) <= 0
                                && decimal.Parse(rowObjeto["PercentualReceitaRealizado"].ToString()) <= 0)
                                )
                            {
                                retorno["naoMostrar"] = "true";
                            }
                            break;
                    }
                    xml.Append(string.Format("<chart bgColor='F1F1F1' showValues='1' canvasBorderThickness='1' canvasBorderColor='999999' plotFillAngle='330' plotBorderColor='999999' showAlternateVGridColor='1' divLineAlpha='0' numberSuffix='%' plotGradientColor='' formatNumber='0'>"
                    , codigos[0]
                     ));
                    xml.Append(string.Format("<set label='{4}' value='{0}' color='{2}' toolText='' />"
                    , perPO
                    , perRO
                    , corPO
                    , corRO
                    , valorPO
                    , valorRO
                    ));
                    xml.Append(string.Format("<set label='{5}' value='{1}' color='{3}' toolText='' />"
                    , perPO
                    , perRO
                    , corPO
                    , corRO
                    , valorPO
                    , valorRO
                    ));
                    xml.Append("</chart>");
                }

                break;
        }

        cDados.escreveXML(xml.ToString(), nomeGrafico);
        retorno["grafico_xml"] = ".." + nomeGrafico;
        return retorno;
    }
    /// <summary>
    /// Adiciona aos planos atuais os planos de cada item
    /// </summary>
    /// <param name="planos">tabela com plano do item</param>
    /// <param name="codigoItem">codigo do item atual</param>
    /// <returns></returns>
    protected DataTable colunaCodigoItemPlanos(DataTable planos, string codigoItem = "")
    {
        DataTable retorno = planos.Copy();
        if (!retorno.Columns.Contains("CodigoItemReuniao"))
        {
            retorno.Columns.Add("CodigoItemReuniao", typeof(string));
        }
        if (retorno.Rows.Count > 0 && (retorno.Rows[0]["CodigoItemReuniao"].ToString() == "" || retorno.Rows[0]["CodigoItemReuniao"].ToString() == "NULL"))
        {
            for (int i = 0; i < retorno.Rows.Count; i++)
            {
                retorno.Rows[i]["CodigoItemReuniao"] = codigoItem;
            }
        }
        return retorno;
    }
    /// <summary>
    /// Evita acumulo limpando os valores da tabela permanente ou se for a última reunião exclui a tabela inteira
    /// </summary>
    protected void excluirTabelaStatus()
    {
        DataTable data = valoresReuniao<DataTable>("dtAtualizacoes", geral: true);
        Boolean excluir = false;
        if (data != null && data.Rows.Count > 0)
        {
            DataRow[] rows = data.Select(string.Format("CodigoEvento = '{0}'", valoresReuniao<string>("codigoEvento")));
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    data.Rows.Remove(row);
                }
            }
            valoresReuniao<DataTable>("dtAtualizacoes", data, geral: true);
        }
        else
            excluir = true;
        if (excluir)
        {
            valoresReuniao<DataTable>("dtAtualizacoes", geral: true, limpar: true);
        }
    }
    /// <summary>
    /// Retorna a classe cDados para uso nos UploadsControls
    /// </summary>
    /// <returns></returns>
    public dados funcoes()
    {
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = HttpContext.Current.Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = HttpContext.Current.Session["NomeUsuario"] + "";
        valoresReuniao<string>("nomeUsuario", HttpContext.Current.Session["NomeUsuario"] + "");

        dados banco = new dados(listaParametrosDados);

        return banco;
    }
    /// <summary>
    /// Atualiza a tabela geral informando o que deve ser atualizado ou busca na tabela geral o que deve ser atualizado
    /// </summary>
    /// <param name="tabela">Nome da tabela que está sendo atualizada</param>
    /// <param name="atualizar">Como true verifica o que deverá ser atualizado</param>
    public string atualizarAtualizacaoStatus(string tabela = "", Boolean atualizar = false)
    {
        DataTable data = valoresReuniao<DataTable>("dtAtualizacoes", geral: true);
        string retorno = "";
        if (data == null)
        {
            data = criarTabelaStatus();
        }
        if (data != null && data.Rows.Count > 0)
        {
            if (atualizar)
            {
                DataRow row = data.Select(string.Format("CodigoEvento = '{0}' AND NomeTabela = '{1}'", valoresReuniao<string>("codigoEvento"), tabela))[0];
                if (row != null)
                {
                    row["UltimaAtualizacao"] = DateTime.Now.ToString();
                    row["CodigoUsuario"] = valoresReuniao<string>("codigoUsuario");
                    row["NomeUsuario"] = valoresReuniao<string>("nomeUsuairo");
                }
            }
            else
            {
                DataRow[] rowsD = data.Select(string.Format("CodigoEvento = {0}", valoresReuniao<string>("codigoEvento")));
                DataTable dtAtualizacoes = valoresReuniao<DataTable>("dtAtualizacoes");
                if (dtAtualizacoes != null)
                {
                    DataRow[] rowsL = dtAtualizacoes.Select(string.Format("CodigoEvento = {0}", valoresReuniao<string>("codigoEvento")));
                    for (int i = 0; i < rowsD.Length; i++)
                    {
                        DateTime atual = DateTime.Parse(rowsD[i]["UltimaAtualizacao"].ToString());
                        DateTime anterior = DateTime.Parse(rowsL[i]["UltimaAtualizacao"].ToString());
                        int status = DateTime.Compare(anterior, atual);
                        if (status != 0)
                        {
                            switch (rowsD[i]["NomeTabela"].ToString())
                            {
                                case "dtItens":
                                    carregarItens();
                                    break;
                                case "dtParticipantes":
                                    carregarParticipantes();
                                    break;
                                case "dtAnexos":
                                    carregarAnexos();
                                    break;
                                case "dtComentarios":
                                    carregarComentarios();
                                    break;
                                case "dtDeliberacoes":
                                    carregarDeliberacoes();
                                    break;
                                case "dtPlanos":
                                    carregarPlanos();
                                    break;
                                case "dtStatus":
                                    carregarStatusReuniao();
                                    break;
                            }
                            retorno += "§" + rowsD[i]["NomeTabela"].ToString();
                        }
                    }
                }
            }
            valoresReuniao<DataTable>("dtAtualizacoes", data, geral: true);
            valoresReuniao<DataTable>("dtAtualizacoes");
        }
        return retorno;
    }
    /// <summary>
    /// Cores utilizadas na metas do item do tipo indicador
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> coresMestasIndicadores()
    {
        Dictionary<string, string> retorno = new Dictionary<string, string>();
        retorno["Azul"] = "0000FF";
        retorno["Vermelho"] = "FF0000";
        retorno["Branco"] = "FFFFFF";
        retorno["Verde"] = "00FF00";
        retorno["Amarelo"] = "FFFF00";
        retorno["Laranja"] = "FFA500";
        return retorno;
    }
    public string participanteReuniaoAtual()
    {
        string mensagem = "";
        Boolean moderador = false;
        Boolean botaoFechar = true;
        if (valoresReuniao<DataTable>("dtParticipantes") != null)
        {
            Boolean participantes = valoresReuniao<DataTable>("dtParticipantes").Select(string.Format("CodigoParticipante = {0}", valoresReuniao<string>("codigoUsuario"))).Count() > 0;
            if (!participantes)
                moderador = participantes = valoresReuniao<string>("codigoUsuario") == valoresReuniao<string>("codigoModerador");
            else
                moderador = valoresReuniao<string>("codigoUsuario") == valoresReuniao<string>("codigoModerador");
            if (participantes)
            {
                if (valoresReuniao<string>("usuarioAvisado") != "true" && !moderador)
                {
                    switch (valoresReuniao<string>("reuniaoIniciada"))
                    {
                        case "":
                            mensagem = string.Format("A reunião não foi iniciada ainda pelo o moderador {0}, com isso não será possível interagir com a reunião. Você será informado quando for inciada.", valoresReuniao<string>("nomeModerador"));
                            botaoFechar = true;
                            valoresReuniao<string>("usuarioAvisado", "true");
                            break;
                        case "true":
                            mensagem = string.Format("A reunião foi iniciada pelo o moderador {0}, ás {1}.", valoresReuniao<string>("nomeModerador"), valoresReuniao<string>("horarioInicio"));
                            botaoFechar = true;
                            valoresReuniao<string>("usuarioAvisado", "true");
                            break;
                        case "false":
                            mensagem = string.Format("A reunião foi finalizada pelo o moderador {0}, ás {1}. Não será possível interagir com a reunião.", valoresReuniao<string>("nomeModerador"), valoresReuniao<string>("horarioFim"));
                            botaoFechar = true;
                            valoresReuniao<string>("usuarioAvisado", "true");
                            break;
                    }
                }
            }
            else
            {
                mensagem = string.Format("Caro usuário você não foi adicionado pelo o moderador {0} na lista de participantes desta reunião.", valoresReuniao<string>("nomeModerador"));
                botaoFechar = false;
                carregarParticipantes();
                carregarStatusReuniao();
            }
        }
        return mensagem + "§" + botaoFechar.ToString();
    }
    #endregion
}