using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;

public partial class _Projetos_Agil_taskboard : System.Web.UI.Page
{
    dados cDados;
    TaskBoard dsTaskBoard;
    int codigoUsuario;
    public int codigoEntidadeUsuarioResponsavel = 0;
    string nomeUsuario;

    public string alturaTabela;
    private string ResolucaoCliente;
    private int idProjeto;
    string fonte = "8pt";
    int largura = 0;
    int altura = 0;
    public static DateTime dtInicioPrevisto;
    public static DateTime dtTerminoPrevisto;

    public static string dataHoraAtual = "";
    private DateTime dataAtual;

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

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

        ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        defineAlturaTela(ResolucaoCliente);

        // PROVISÓRIO, enquanto não recebe pela QueryString
        //idProjeto = 2207;
        if (Request.QueryString["CP"] != null)
            idProjeto = int.Parse(Request.QueryString["CP"].ToString());

        configuraCalendario();

        nomeUsuario = cDados.getInfoSistema("NomeUsuarioLogado").ToString();

        // Pega apenas o primeiro nome do usuário
        if (nomeUsuario.IndexOf(' ') > 0)
            nomeUsuario = nomeUsuario.Substring(0, nomeUsuario.IndexOf(' '));


        imgFullscreen.ClientSideEvents.Click = string.Format(@"
        function(s, e)
        {{
            abreFullscreen('taskboard.aspx?CP={0}&FS=S', {1});
        }}", idProjeto, (altura - 70));
        imgBurndown.ClientSideEvents.Click = "function(s, e){abreGrafico(" + idProjeto + ");}";
        imgReuniaoDiariaSprint.ClientSideEvents.Click = "function(s, e){ pcCalendario.Show();}";
        imgFullscreen.ImageUrl = (Request.QueryString["FS"] + "") == "" ? "~/imagens/fullscreen.png" : "~/imagens/fullscreen_exit.png";
        imgFullscreen.ToolTip = (Request.QueryString["FS"] + "") == "" ? "Modo tela cheia" : "Sair do modo tela cheia";

        if (!IsPostBack && !IsCallback)
        {
            // obtem as informações do projeto
            dsTaskBoard = getDSTaskBoard(null);// null para retornar todos os itens do projeto

            // obtem as fases
            spHeaderBoard.InnerHtml = getStatusItensBacklogHeader();

            // obtem as fases
            spBodyBoard.InnerHtml = getStatusItensBacklogBody();

            dvFooter.InnerHtml = getFooter();
        }

        cDados.aplicaEstiloVisual(Page);

        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/sprite.css"" />"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        if (Request.QueryString["MostraBotoes"] != null && Request.QueryString["MostraBotoes"].ToString() == "N")
            spBodyBoard.Style.Add("height", (altura - 230) + "px");
        else
            spBodyBoard.Style.Add("height", (altura - 300) + "px");
    }

    private TaskBoard getDSTaskBoard(int? CodigoItem)
    {
        // obtem a string de conexão. É necessário para o uso do SqlDataAdapter
        string strCon = cDados.classeDados.getStringConexao(); //"Data Source=cdisdb;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678";

        // comando SQL para obter os registros das tabelas "Agil_TipoClassificacaoItemBacklog", "Agil_TipoStatusItemBacklog" e "Usuario"
        // ATENÇÃO: A sequencia dos comandos não pode ser alterada
        string comandoSQL =
            string.Format(@"-- Table
              SELECT CodigoTipoClassificacaoItem, DescricaoTipoClassificacaoItem
                FROM Agil_TipoClassificacaoItemBacklog

               -- Table1
               SELECT CodigoTipoStatusItem, TituloStatusItem, 
                      case when IndicaPossuiRecursoAssociado = 'S' then 'true' else 'false' end IndicaPossuiRecursoAssociado, 
	                  SequenciaApresentacaoRaia
                 FROM Agil_TipoStatusItemBacklog
                WHERE SequenciaApresentacaoRaia > 0
                ORDER BY SequenciaApresentacaoRaia

               -- Table2
                SELECT CodigoUsuario, NomeUsuario
                  FROM Usuario u
                 WHERE EXISTS (SELECT 1 FROM dbo.f_Agil_GetItensKanban({0}, {1}) WHERE CodigoUsuarioResponsavel = u.CodigoUsuario)", idProjeto, codigoUsuario);

        // O comando para buscar os itens será retornado por um método, pois também será utilizado em outra situação
        comandoSQL += getComandoRecuperaItemBacklog(CodigoItem);

        // obtem os dados a partir do banco e faz o De-Para para o dataset tipado.
        // É importante que as tabelas estejam na ordem abaixo
        SqlDataAdapter da = new SqlDataAdapter(comandoSQL, strCon);
        da.TableMappings.Add("Table", "ClassificacaoItemBackLog");
        da.TableMappings.Add("Table1", "TipoStatusItemBacklog");
        da.TableMappings.Add("Table2", "Usuario");
        da.TableMappings.Add("Table3", "ItemBacklog");

        // carrega os dados do banco no dataset tipado
        TaskBoard dsTipado = new TaskBoard();
        da.Fill(dsTipado);

        return dsTipado;
        //return getDataSetPortal(strCon);
    }

    private string getComandoRecuperaItemBacklog(int? CodigoItem)
    {
        string filtraItem = "";

        if (txtPesquisa.Text.Trim() != "")
            filtraItem += string.Format(@" AND (ibl.TituloItem LIKE '%{0}%' OR NomeUsuario LIKE '%{0}%' OR ibPai.TituloItem LIKE '%{0}%')", txtPesquisa.Text);

        if (CodigoItem.HasValue)
            filtraItem = " and ibl.CodigoItem = " + CodigoItem.Value;

        string comandoSQL = string.Format(
            @"
               -- Table3
               SELECT ibl.CodigoItem, ibl.CodigoTipoStatusItem, ibl.CodigoTipoClassificacaoItem, 
                      ISNULL(ibPai.TituloItem + ' - ', '') + ibl.TituloItem AS TituloItem
                    , ibl.DetalheItem, ibl.Importancia, ibl.EsforcoPrevisto, ibl.CodigoUsuarioResponsavel, u.NomeUsuario, ibl.IndicaItemNaoPlanejado
                 FROM Agil_ItemBacklog ibl LEFT JOIN
		              Agil_ItemBacklog AS ibPai ON (ibl.CodigoItemSuperior = ibPai.CodigoItem) LEFT JOIN 
                      Usuario u ON u.CodigoUsuario = ibl.CodigoUsuarioResponsavel
                 WHERE ibl.CodigoItem in (SELECT f.CodigoItem FROM dbo.f_Agil_GetItensKanban({0}, {1}) f)
                    {2} 
                 ORDER BY ibl.Importancia DESC", idProjeto, codigoUsuario, filtraItem);

        return comandoSQL;
    }

    /// <summary>
    /// Constrói os títulos das Raias do taskBoard. Cada raia será um "TD" da table
    /// </summary>
    /// <returns></returns>
    private string getStatusItensBacklogHeader()
    {
        string RowHeader = "<table class='board'><thead><tr>";

        int SequenciaStatus = 0;
        // monta o HTML correpondente ao cabeçalho de todas os status ativos
        foreach (TaskBoard.TipoStatusItemBacklogRow TipoStatusItemBacklog in dsTaskBoard.TipoStatusItemBacklog.Rows)
        {
            // obtem a quantidade de ItensBacklog no Status
            int Qtde = dsTaskBoard.ItemBacklog.Select("CodigoTipoStatusItem = " + TipoStatusItemBacklog.CodigoTipoStatusItem).Count();

            RowHeader += string.Format("<td  id='headerStatus{0}'>{1} ({2})</td>", TipoStatusItemBacklog.CodigoTipoStatusItem, TipoStatusItemBacklog.TituloStatusItem, Qtde);

            // guarda a quantidade de ItensBacklog do status atual em um HiddenField para ser utilizado pelo javascript
            hf.Set("hfStatus" + TipoStatusItemBacklog.CodigoTipoStatusItem, Qtde);

            // garda se o status atual possui a informação do recurso associado
            hf.Set("hfRecStatus" + TipoStatusItemBacklog.CodigoTipoStatusItem, TipoStatusItemBacklog.IndicaPossuiRecursoAssociado ? "S" : "N");

            // garda a sequencia do status. Será utilizado para controlar os avanços para a direita, que deverão ser feitos uma raia por vêz
            hf.Set("hfSeqStatus" + TipoStatusItemBacklog.CodigoTipoStatusItem, ++SequenciaStatus);
        }
        // inclui uma coluna extra correpondente a largura da barra de rolagem
        RowHeader += "<td width='10px'> </td>";
        RowHeader += "</tr></thead></table>";

        return RowHeader;
    }

    /// <summary>
    /// Constrói as Raias do taskBoard. Cada raia será um "TD" da table 
    /// </summary>
    /// <returns></returns>
    private string getStatusItensBacklogBody()
    {
        // Monta o HTML de todas os itensBacklog em todos os StatusItemBacklog
        string RowBody = "<table class='board'><tr>";
        foreach (TaskBoard.TipoStatusItemBacklogRow StatusItemBacklog in dsTaskBoard.TipoStatusItemBacklog.Rows)
        {
            // monta o HTML correspondente a todos os ItensBacklog do StatusItemBacklog (Raia) atual 
            // Aos "TDs", está sendo associado os eventos "ondrop" e "ondragover"
            // O "ondragover" é disparado no momento que o objeto arrastado "entra" na área do "TD". 
            // Se for necessário, o processo poderá ser cancelado, colocando um "return false" ou "ev.stopPropagation()"
            // O "ondrop", será disparado no momento que o objeto arrastado for solto na área do "TD"
            RowBody += string.Format(@"<td id='Status{0}' style='background-color:white;' align='center' ondragover='allowDrop(event)' ondrop='drop(event, {2})' height='100%'>{1}</td>"
                , StatusItemBacklog.CodigoTipoStatusItem, getTarefasStatusItensBacklog(StatusItemBacklog), idProjeto);
        }
        RowBody += "</tr></table>";

        return RowBody;
    }

    private string getFooter()
    {
        string comandoSQL = string.Format(@"
        SELECT ISNULL(dbo.f_Agil_GetPercentualPrevistoIteracao(null, {0}), 0) AS PercentualPrevisto
             , ISNULL(dbo.f_Agil_GetPercentualRealizadoIteracao(null, {0}), 0) AS PercentualReal", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);



        string footer = string.Format(@"
        <table cellpadding=""0"" cellspacing=""0"" class=""style1"" style=""height: 100%"">
                <tr>
                    <td align=""left"" width=""20"" bgcolor=""#DDDDDD"" style=""border: 1px solid #808080"">
                       
                    </td>
                    <td align=""left""  style=""padding-left: 3px;"">{2}</td>
                    <td align=""right"" style=""font-size:8pt"">{3}{0:p0}&nbsp;&nbsp;{4}{1:p0}</td>
                </tr>
            </table>", ds.Tables[0].Rows[0]["PercentualPrevisto"]
               , ds.Tables[0].Rows[0]["PercentualReal"]
               , Resources.traducao.itens_n_o_planejados
               , Resources.traducao.previsto_
               , Resources.traducao.realizado_
               );

        return footer;
    }

    /// <summary>
    /// Constrói a raia correspondente ao StatusItemBacklog informado no parâmetro
    /// </summary>
    /// <param name="StatusItemBacklog"></param>
    /// <returns></returns>
    private string getTarefasStatusItensBacklog(TaskBoard.TipoStatusItemBacklogRow StatusItemBacklog)
    {
        // seleciona os ItensBacklogs do StatusItemBacklog informado no parâmetro
        TaskBoard.ItemBacklogRow[] ItensBacklog = (TaskBoard.ItemBacklogRow[])dsTaskBoard.ItemBacklog.Select("CodigoTipoStatusItem = " + StatusItemBacklog.CodigoTipoStatusItem);

        // Cada ItemBacklog será uma DIV
        string dvItensBacklog = "";
        foreach (TaskBoard.ItemBacklogRow ItemBacklog in ItensBacklog)
        {
            // busca o html correspondente ao ItemBacklog atual
            dvItensBacklog += getDivItemBacklog(ItemBacklog);
        }

        return dvItensBacklog;
    }

    /// <summary>
    /// Constrói a DIV correspondente ao ItemBacklog 
    /// </summary>
    /// <param name="ItemBacklog"></param>
    /// <returns></returns>
    private string getDivItemBacklog(TaskBoard.ItemBacklogRow ItemBacklog)
    {
        // Anota se na Raia atual, o itembacklog terá recurso associado 
        bool RaiaPossuiRecursoAssociado = ItemBacklog.TipoStatusItemBacklogRow.IndicaPossuiRecursoAssociado;

        // A DIV terá três linhas representadas por tables
        // Linha 1, terá o ID do ItemBacklog, esforço e importância
        // ----------------------------------------------------------------------------------------------
        string NomeUsuario = "";
        if (RaiaPossuiRecursoAssociado)
            NomeUsuario = ItemBacklog.UsuarioRow + "" == "" ? "???" : ItemBacklog.UsuarioRow.NomeUsuario + "";

        string tabelaLinha1 = string.Format(
            @"<table class='TableItemBacklog SeparadorTipoClassificacaoItem{0}'>
                <tr>
                    <td class='TdImagemDetalhe' onclick='detalheItemBacklog({1}, {4}, {5})'><img alt='Detalhes' src='../../imagens/searchBox.png' /></td>
                    <td class='TdIdItemBacklog' >Id: {1}</td>
                    <td class='TdEstimativaItemBacklog'>" + Resources.traducao.estimativa_ + @" {2:N0}h</td>
                    <td class='TdImportanciaItemBacklog'>" + Resources.traducao.import_ncia_ + @" {3:N0}</td>
                </tr>
              </table>", ItemBacklog.CodigoTipoClassificacaoItem, ItemBacklog.CodigoItem, ItemBacklog.EsforcoPrevisto, ItemBacklog.Importancia, ItemBacklog.CodigoTipoStatusItem, idProjeto);

        // Linha 2, terá o título do ItemBacklog 
        // ----------------------------------------------------------------------------------------------
        string tabelaLinha2 = string.Format(
            @"<table class='TableTituloItem SeparadorTipoClassificacaoItem{0}'>
                <tr>
                    <td class='TdTituloItem'>{1}</td>
                </tr>
              </table>", RaiaPossuiRecursoAssociado ? ItemBacklog.CodigoTipoClassificacaoItem : 99, ItemBacklog.TituloItem);

        // Linha 3, terá o nome do Responsável pela tarefa
        // ----------------------------------------------------------------------------------------------
        string tabelaLinha3 = "";
        if (RaiaPossuiRecursoAssociado && nomeUsuario != "")
        {
            tabelaLinha3 = string.Format(
                @"<table class='TableResponsavelItem'>
                <tr>
                    <td id='Recurso{0}' class='TdRecurso'>" + Resources.traducao.respons_vel_ + @" {1}</td>
                </tr>
              </table>", ItemBacklog.CodigoItem, NomeUsuario);
        }

        bool podeArrastar;

        try
        {
            podeArrastar = (!RaiaPossuiRecursoAssociado || (ItemBacklog.CodigoUsuarioResponsavel == codigoUsuario));
        }
        catch (Exception)
        {
            ItemBacklog.CodigoUsuarioResponsavel = 1;
            podeArrastar = (!RaiaPossuiRecursoAssociado || (ItemBacklog.CodigoUsuarioResponsavel == codigoUsuario));
        }

        // Se o itembacklog puder ser arrastado, a DIV terá a propriedade "draggable='true'" e o evento "ondragstart()"
        // Ao iniciar o "arrasto", será disparado o evento "ondragstart"
        string draggable = "";
        if (podeArrastar)
            draggable = "draggable='true' ondragstart='drag(event)'";

        // constroi a div
        string div = string.Format(
            @"<div id='ItemBacklog_{1}' style='font-size:{6}' class='ItemBacklog TipoClassificacaoItem{2}' {0} >
                <table style='border-collapse: collapse;'>
                    <tr>
                        <td class='situacaoItem{2}'></td>
                        <td>{3}{5}{4}</td>
                    </tr>
                </table>
              </div>", draggable, ItemBacklog.CodigoItem, ItemBacklog.IndicaItemNaoPlanejado == "S" ? 9999 : ItemBacklog.CodigoTipoClassificacaoItem, tabelaLinha1, tabelaLinha2, tabelaLinha3, fonte);

        return div;
    }

    private bool usuarioEquipe()
    {
        bool usuarioDaEquipe = false;

        string comandoSQL = string.Format(@"
        BEGIN
	            DECLARE @CodigoIteracao Int       
	
	            SELECT @CodigoIteracao = CodigoIteracao
		          FROM Agil_Iteracao
	             WHERE CodigoProjetoIteracao = {0}

                SELECT 1
                  FROM Agil_RecursoIteracao ri INNER JOIN
			           vi_RecursoCorporativo rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo 
								               AND rc.CodigoRecurso = {1}
                 WHERE ri.CodigoIteracao = @CodigoIteracao
        END", idProjeto, codigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);

        usuarioDaEquipe = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]));

        return usuarioDaEquipe;
    }

    protected void cb_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.ToLower().IndexOf("ajusta") == 0)
        {
            string[] parametros = e.Parameter.Split(';');
            string tarefa = parametros[1];
            string origem = parametros[2];
            string destino = parametros[3];
            string status = "";
            string statusAnterior = "";

            getIniciaisStatus(origem, destino, ref statusAnterior, ref status);

            // verifica se a ação pode ser executada
            int sequenciaOrigem = int.Parse(hf.Get("hfSeqStatus" + origem).ToString());
            int sequenciaDestino = int.Parse(hf.Get("hfSeqStatus" + destino).ToString());

            // se está movendo para a direita, só pode avançar uma raia por vez
            if (sequenciaDestino > sequenciaOrigem + 1)
            {
                cb.JSProperties["cp_retorno"] = "ERRO";
                cb.JSProperties["cp_param"] = "O item do backlog deve avançar uma raia por vez.";
                return;
            }

            // escreva aqui o comando para executar a ação no servidor de dados
            string comandoSQL = "";

            if (statusAnterior == "SP_NAOINI" && status == "SP_FAZENDO")
            {
                comandoSQL += string.Format(@"
                            DECLARE @CodigoNovoStatus INT

                            SELECT @CodigoNovoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoNovoStatus
                      ", cDados.getDbName(), cDados.getDbOwner(), tarefa, codigoUsuario, status);
            }
            else if ((statusAnterior == "SP_PRONTO" && status == "SP_FAZENDO") || (statusAnterior == "SP_FAZENDO" && status == "SP_NAOINI") || (statusAnterior == "SP_PRONTO" && status == "SP_NAOINI"))
            {
                comandoSQL += string.Format(@"
                        BEGIN
                            DECLARE @CodigoStatus INT

                            SELECT @CodigoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                             EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoStatus
                        END                       
                      ", cDados.getDbName(), cDados.getDbOwner(), tarefa, codigoUsuario, status);
            }
            else if (status == "SP_PRONTO")
            {
                comandoSQL += string.Format(@"
                            DECLARE @CodigoStatus INT

                            SELECT @CodigoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoStatus
                      ", cDados.getDbName(), cDados.getDbOwner(), tarefa, codigoUsuario, status);
            }
            else
            {
                comandoSQL = string.Format(
                @"UPDATE Agil_ItemBacklog
                     SET CodigoTipoStatusItem = {1}
                       , CodigoUsuarioResponsavel = {2} 
                   WHERE CodigoItem = {0}", tarefa, destino, codigoUsuario);
            }

            int regAf = 0;
            bool result = cDados.execSQL(comandoSQL, ref regAf);

            // se deu algum erro, trate-o aqui
            if (!result)
            {
                cb.JSProperties["cp_retorno"] = "ERRO";
                cb.JSProperties["cp_param"] = "Erro ao mover tarefa!";
                return;
            }

            // atualiza as informações do taskBoar. Será feito apenas para o itenBacklog que foi movimentado
            dsTaskBoard = getDSTaskBoard(int.Parse(tarefa));

            // Este comando devera retornar se o status possui recurso associado ou não.
            bool possuiRecursoAssociado = dsTaskBoard.ItemBacklog[0].TipoStatusItemBacklogRow.IndicaPossuiRecursoAssociado;
            string nomeUsuarioCliente = possuiRecursoAssociado ? nomeUsuario : "";

            // Este é o retorno que será enviado ao cliente informando que a açao no servidor foi concluída
            string retorno = string.Format("{0};{1};{2};{3}", tarefa, origem, destino, nomeUsuarioCliente);
            cb.JSProperties["cp_retorno"] = "OK";
            cb.JSProperties["cp_param"] = retorno;
            cb.JSProperties["cp_Div"] = getDivItemBacklog(dsTaskBoard.ItemBacklog[0]);
            cb.JSProperties["cp_Footer"] = getFooter();
        }
    }

    private void getIniciaisStatus(string codigoStatusOrigem, string codigoStatusDestino, ref string iniciaisStatusOrigem, ref string iniciaisStatusDestino)
    {
        string comandoSQL = string.Format(@"
            SELECT 
                (SELECT IniciaisTipoStatusItemControladoSistema FROM Agil_TipoStatusItemBacklog WHERE CodigoTipoStatusItem = {0}) AS Origem,
                (SELECT IniciaisTipoStatusItemControladoSistema FROM Agil_TipoStatusItemBacklog WHERE CodigoTipoStatusItem = {1}) AS Destino"
            , codigoStatusOrigem, codigoStatusDestino);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            iniciaisStatusOrigem = ds.Tables[0].Rows[0]["Origem"].ToString();
            iniciaisStatusDestino = ds.Tables[0].Rows[0]["Destino"].ToString();
        }
    }

    protected void callbackFiltro_Callback1(object source, DevExpress.Web.CallbackEventArgs e)
    {
        // obtem as informações do projeto
        dsTaskBoard = getDSTaskBoard(null);// null para retornar todos os itens do projeto

        // obtem as fases
        callbackFiltro.JSProperties["cp_spHeaderBoard"] = getStatusItensBacklogHeader();

        // obtem as fases
        callbackFiltro.JSProperties["cp_spBodyBoard"] = getStatusItensBacklogBody();
    }
    private void configuraCalendario()
    {
        string comandosql = string.Format(@"SELECT [CodigoIteracao]
                                                  ,[InicioPrevisto]
                                                  ,[TerminoPrevisto]
                                              FROM {0}.{1}.[Agil_Iteracao] where [CodigoProjetoIteracao] = {2}", cDados.getDbName(), cDados.getDbOwner(), idProjeto);

        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dtInicioPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["InicioPrevisto"].ToString());
            dtTerminoPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoPrevisto"].ToString());
        }

        if (!Page.IsPostBack)
        {
            dataHoraAtual = cDados.classeDados.getDateDB();
            calendario.SelectedDate = (DateTime.Compare(dtInicioPrevisto, DateTime.MinValue) == 0) ? DateTime.Now : dtInicioPrevisto;
            System.Globalization.CultureInfo cultureInfoTmp = System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR");
            dataAtual = DateTime.Parse(dataHoraAtual, cultureInfoTmp);
        }
    }

    protected void ASPxCalendar1_DayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
    {

        string funcao_javascript = "abreReuniaoDiaria(" + idProjeto + "," + e.Date.Day + "," + e.Date.Month + "," + e.Date.Year + "); pcCalendario.Hide();";

        if (dtInicioPrevisto <= e.Date && e.Date <= dtTerminoPrevisto)
        {
            e.Cell.Font.Bold = true;
            if (e.Date <= dataAtual && e.Date.DayOfWeek != DayOfWeek.Sunday)
            {
                e.Cell.Text = @"<a onclick= " + funcao_javascript + " href='javascript:void(0);'>" + e.Date.Day.ToString() + "</a>";

                string whereAux = string.Format(@" AND ev.CodigoTipoAssociacao = (SELECT {0}.{1}.[f_GetCodigoTipoAssociacao]('PR'))   
                                               AND ev.CodigoTipoEvento = (SELECT top 1 CodigoTipoEvento 
                                                                            FROM TipoEvento 
                                                                           WHERE DescricaoTipoEvento like '%Sprint%')
                                               AND  (YEAR(ev.TerminoReal) = {2} 
                                               AND MONTH(ev.TerminoReal) = {3}
                                               AND   DAY(ev.TerminoReal) = {4})
                                               AND ev.CodigoObjetoAssociado = {5}", cDados.getDbName(), cDados.getDbOwner(), e.Date.Year, e.Date.Month, e.Date.Day, idProjeto);
                DataSet dsReuniao = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), whereAux, codigoUsuario.ToString(), "PR_AdmReu");

                if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
                {
                    e.Cell.BackColor = Color.LightGreen;
                    e.Cell.Font.Bold = true;
                }
            }
        }



    }
}
