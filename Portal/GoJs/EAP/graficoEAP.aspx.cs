
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections;

public partial class GoJs_EAP_Default : System.Web.UI.Page
{
    #region --- Variáveis usadas na montagem do HTML
    public string webServicePath = "";  // caminho do web service
    public string doubleClipTarea = ""; // link del popup.
    public string codigoEAP = "";       // código do controle de edição da EAP
    public string largoObject = "";
    public string baseUrl;
    #endregion

    dados cDados;
    private string bancodb;
    private string ownerdb;
    private string codigoProjeto = "-1", codigoCronograma = "-1";
    private string IdUsuario = "";
    private string IdEntidade = "";
    private string nomeProjeto = "";
    protected string modoAcessoDesejado = "";
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    string modoAcessoFinal;
    object CheckoutBy;
    string idEdicaoEap;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        cDados = CdadosUtil.GetCdados(null);

        ownerdb = cDados.getDbOwner();
        bancodb = cDados.getDbName();

        cDados.aplicaEstiloVisual(this);
    }

    /* Experimento Bootstrap */
    private void Head()
    {
        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
        
        //Carregamento Depois para chamada Jason
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/css/bootstrap.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/fontawesome/v5.0.12/css/fontawesome-all.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/material-icons/v3.0.1/material-icons.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/Linearicons/v1.0.0/Linearicons.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        ////HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/custom.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/menu.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js""></script>", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/js/bootstrap.bundle.min.js""></script>", baseUrl)));
        ////HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/custom.js""></script>", baseUrl)));
        //Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/menu.js""></script>", baseUrl)));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("barraNavegacao", "graficoEAP"));
        if (Request.QueryString["CCR"] != null && Request.QueryString["CCR"].ToString() != "")
        {
            codigoCronograma = Request.QueryString["CCR"].ToString();
        }
        else
        {

        }

        Head();

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = Request.QueryString["IDProjeto"].ToString();
        }
        if (Request.QueryString["CE"] != null)
            IdEntidade = Request.QueryString["CE"].ToString();
        if (Request.QueryString["CU"] != null)
            IdUsuario = Request.QueryString["CU"].ToString();
        if (Request.QueryString["NP"] != null)
            nomeProjeto = Request.QueryString["NP"].ToString();
        if (Request.QueryString["AM"] != null)
            modoAcessoDesejado = Request.QueryString["AM"].ToString();

        if (modoAcessoDesejado == "RW")
        {
            modoAcessoDesejado = "G";
            //toolBar.Style.Add("display", "");
        }
        else
        {
            modoAcessoDesejado = "L";
            //toolBar.Style.Add("display", "none");

        }

        defineAlturaTela();



        hfGeral.Set("Acesso", modoAcessoDesejado == "G");

        if (!hfGeral.Contains("IDEdicaoEAP"))
        {
            hfGeral.Set("IDEdicaoEAP", getIDEdicaoEAP(modoAcessoDesejado, out modoAcessoFinal, out CheckoutBy));
            retorno_popup.Value = hfGeral.Get("IDEdicaoEAP").ToString();
        }
            

        idEdicaoEap = hfGeral.Get("IDEdicaoEAP").ToString();
        if(!string.IsNullOrEmpty(idEdicaoEap))
        {
            getEapProjeto();

            getEstiloEAP();

            ExibirAlertaLinhasDeBaseExistentes();
        }
        



        //imgGlossario.JSProperties["cp_CodigoGlossario"] = cDados.getCodigoGlossarioTela(this.Page);
        pcApresentacaoAcao.JSProperties["cp_Path"] = cDados.getPathSistema();

    }

    private void ExibirAlertaLinhasDeBaseExistentes()
    {
        var possuiLinhaDeBaseAprovada = cDados.getVersoesLinhaBase(int.Parse(codigoProjeto), "").Tables[0].Rows.Count > 0;
        lblAviso.ClientVisible = possuiLinhaDeBaseAprovada;
    }

    private void defineAlturaTela()
    {
        //Calcula a altura da tela
        string strAltura = Request.QueryString["Altura"];
        if (string.IsNullOrWhiteSpace(strAltura) || !int.TryParse(strAltura, out alturaPrincipal))
            alturaPrincipal = 670;
        alturaPrincipal += 80;

        int outint = -1;
        bool botaoFecharVisivel = !int.TryParse(Request.QueryString["CIWF"] + "", out outint);
        btnCancelar.ClientVisible = botaoFecharVisivel;

    }

    private void getEstiloEAP()
    {
        string estiloEAP = "";
        string corDefaultCaixa = "#7BA4C6";
        string corDefaultFonte = "#FFFFFF";
        string tamanhoDefaultFonte = "9";
        string escalaZoom = "1";
        string visao = "S";
        string layout = "0";

        string comandoSQL = string.Format(@"
            SELECT EstiloVisualEAP 
	          FROM {0}.{1}.CronogramaProjeto 
             WHERE CodigoProjeto = {2}
            ", bancodb, ownerdb, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            estiloEAP = ds.Tables[0].Rows[0]["EstiloVisualEAP"].ToString();

            if (estiloEAP != "")
            {
                string[] valoresEstilos = estiloEAP.Split(',');

                if (valoresEstilos.Length > 0)
                    corDefaultCaixa = valoresEstilos[0].Split(':')[1];
                if (valoresEstilos.Length > 1)
                    corDefaultFonte = valoresEstilos[1].Split(':')[1];
                if (valoresEstilos.Length > 2)
                    tamanhoDefaultFonte = valoresEstilos[2].Split(':')[1];
                if (valoresEstilos.Length > 3)
                    escalaZoom = valoresEstilos[3].Split(':')[1];
                if (valoresEstilos.Length > 4)
                    visao = valoresEstilos[4].Split(':')[1];
                if (valoresEstilos.Length > 5)
                    layout = valoresEstilos[5].Split(':')[1];
            }
        }

        if (!IsPostBack)
        {
            colorEdit.Text = corDefaultCaixa;
            colorEditFonte.Text = corDefaultFonte;
            colorEditFonte.JSProperties["cp_Fonte"] = tamanhoDefaultFonte;
            colorEditFonte.JSProperties["cp_EscalaZoom"] = escalaZoom;
            ddlVisao.Value = visao;
            ddlLayout.Value = layout;
        }
    }


    //Bloquea EAP para Edição no Primeiro Acesso
    private string getIDEdicaoEAP(string modoAcessoDesejado, out string modoAcessoFinal, out object CheckoutBy)
    {
        int codigoInstanciaWorkflow = -1;
        if (Request.QueryString["CIWF"] != null)
        {
            int.TryParse(Request.QueryString["CIWF"] + "", out codigoInstanciaWorkflow);
        }
        CheckoutBy = System.DBNull.Value;
        modoAcessoFinal = "L";
        string idEdicao = "";
        string comandoSQL = string.Format(@"
            DECLARE
		              @IdEdicaoEAP          Varchar(64)
	                , @CheckoutBy           Varchar(64)
                    , @modoAcessoFinal      char(1)
                    
            EXEC {0}.{1}.[p_crono_GeraIDEdicaoEAP]
		                  @in_codigoProjeto         = {2}
	                    , @in_codigoUsuarioEdicao   = {3}
	                    , @in_modoAcessoDesejado    = {4}
	                    , @ou_IdEdicaoEAP           = @IdEdicaoEAP          OUTPUT
	                    , @ou_checkoutBy            = @CheckoutBy           OUTPUT
                        , @ou_modoAcessoFinal       = @modoAcessoFinal      OUTPUT
                        , @in_BloquearEAP = '{5}'

            SELECT @IdEdicaoEAP AS CodigoEAP, @CheckoutBy AS CheckoutBy, @modoAcessoFinal AS ModoAcessoFinal
            ", bancodb, ownerdb, codigoProjeto, IdUsuario, modoAcessoDesejado, (codigoInstanciaWorkflow == -1) ? 'S' : 'N');

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            idEdicao = ds.Tables[0].Rows[0]["codigoEAP"].ToString();
            CheckoutBy = ds.Tables[0].Rows[0]["CheckoutBy"];
            modoAcessoFinal = ds.Tables[0].Rows[0]["ModoAcessoFinal"].ToString();
        }
        
        //if(codigoInstanciaWorkflow == -1)
        //{
        //    int regAf = 0;
        //    string msgErro = "";
        //    bool retorno = cDados.BloqueiaCronogramaAcessoUsuario(int.Parse(IdUsuario), codigoProjeto, ref regAf, ref msgErro);
        //}
        

        return idEdicao;
    }

    bool VerificaPodeExcluir(int codigoTarefa, IEnumerable<DataRow> dataRows)
    {
        var row = dataRows.SingleOrDefault(dr => dr.Field<int>("CodigoTarefa") == codigoTarefa);
        var childrenRows = dataRows.Where(dr => !dr.IsNull("CodigoTarefaSuperior") && dr.Field<int>("CodigoTarefaSuperior") == codigoTarefa);

        foreach (var childRow in childrenRows)
        {
            var codigoTarefaFilha = childRow.Field<int>("CodigoTarefa");
            if (!VerificaPodeExcluir(codigoTarefaFilha, dataRows))
                return false;
        }

        return (
            (row.Field<string>("EDT") != "0") &&
            (row.Field<short>("PossuiTarefas") == 0) &&
            ((row["PodeExcluir"] as string ?? string.Empty).ToUpper().Equals("S")));
    }

    private void getEapProjeto()
    {
        //Pegando as tarefas do cronograma.
        string comandoSQL = string.Format(@"
        SELECT f.*, {0}.{1}.f_VerificaExisteTarefaPacoteTrabalho('{2}', f.CodigoTarefa) AS PossuiTarefas FROM {0}.{1}.f_GetEAPPlanejamento('{2}', 1) AS f
", cDados.getDbName(), cDados.getDbOwner(), codigoCronograma, IdUsuario);
        DataSet ds = cDados.getDataSet(comandoSQL);

        var rows = ds.Tables.OfType<DataTable>().First().AsEnumerable();

        var tableRowCount = ds.Tables[0].Rows.Count;
        var nodeDataArray = new List<object>();

        if (ds != null && ds.Tables.Count > 0)
        {
            //recorriendo las tareas del cronograma.
            foreach (var row in rows)
            {
                string indicaFechado = "0";

                if (row["XmlEstiloEap"].ToString().Contains("IndicaEAPFechada"))
                {
                    string[] valoresEstilos = row["XmlEstiloEap"].ToString().Split(',');

                    if (valoresEstilos.Length > 0)
                        indicaFechado = valoresEstilos[0].Split(':')[1];
                }
                var obj = new
                {
                    key = row.Field<int>("CodigoTarefa"),
                    name = row["NomeTarefa"] as string,
                    parent = row.GetNullableValue<int>("CodigoTarefaSuperior"),
                    idTarefa = row["IDTarefa"] as string,
                    nivel = row.GetNullableValue<short>("Nivel"),
                    indicaFechado = indicaFechado,
                    Inicio = row.GetNullableValue<DateTime>("Inicio"),
                    Termino = row.GetNullableValue<DateTime>("Termino"),
                    ValorPeso = string.Format("{0:n2}", row["ValorPeso"]),
                    PercentualPesoTarefa = string.Format("{0:n1}", row["PercentualPesoTarefa"]),
                    Custo = string.Format("{0:n2}", row["Custo"]),
                    Receita = string.Format("{0:n2}", row["Receita"]),
                    Trabalho = string.Format("{0:n2}", row["Trabalho"]),
                    PodeEditar = row["PodeEditar"] as string,
                    PodeExcluir = VerificaPodeExcluir(row.Field<int>("CodigoTarefa"), rows) ? "S" : "N",//Convert.ToBoolean(row["PossuiTarefas"]) ? "N" : row["PodeExcluir"] as string,
                    PodeAdicionarFilho = row["PodeAdicionarFilho"] as string,
                    CodigoUsuarioResponsavel = row.GetNullableValue<int>("CodigoUsuarioResponsavel"),
                    Anotacoes = row["Anotacoes"] as string,
                    Duracao = string.Format("{0:d0}", Convert.ToInt32(row["Duracao"])),
                    IndicaAtribuicaoManualPesoTarefa = row["IndicaPesoEditadoManualmente"] as string,
                    PodeEditarPeso = row["PodeEditarPeso"] as string,
                    CodigoInternoTarefa = (row["IDTarefa"] as string) ?? "-1",
                    CriterioAceitacao = row["CriterioAceitacao"] as string,
                    IndicaResumo = row.Field<short>("Nivel") == 0 ? "S" : "N",
                    SequenciaTarefaCronograma = string.Empty,
                    EDT = row["EDT"] as string
                };
                nodeDataArray.Add(obj);
            }
        }

        if (tableRowCount > 0)
        {
            var result = new
            {
                @class = "go.TreeModel",
                nodeDataArray = nodeDataArray.ToArray()
            };

            mySavedModel.InnerHtml = JsonConvert.SerializeObject(
                result, new JsonSerializerSettings()
                {
                    DateFormatString = CultureInfo.CurrentCulture.Name.StartsWith("en") ? "MM/dd/yyyy" : "dd/MM/yyyy",
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });
        }
        else
        {
            mySavedModel.InnerHtml = @"{ ""class"": ""go.TreeModel"",
  ""nodeDataArray"": [
{ ""key"":""0"", ""name"":""" + cDados.getNomeProjeto(codigoProjeto, "") + @""", ""idTarefa"":"""", ""nivel"":""0"", ""indicaFechado"":""0"",
                                                   ""Inicio"":"""",""Termino"":"""",""ValorPeso"":""0"",""PercentualPesoTarefa"":""0"",
                                                   ""Custo"":""0"",""Receita"":"""",""Trabalho"":""0"",
                                                   ""PodeEditar"":""S"",""PodeExcluir"":""S"",""PodeAdicionarFilho"":""S"",""CodigoUsuarioResponsavel"":"""",
                                                   ""Anotacoes"":"""", ""Duracao"":"""", ""IndicaAtribuicaoManualPesoTarefa"":""N"", ""PodeEditarPeso"":""S"", ""CodigoInternoTarefa"": ""-1"",""CriterioAceitacao"":"""", ""IndicaResumo"":""S"", ""SequenciaTarefaCronograma"":""""}
 ]
}
        ";

        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (PodeAlterarEap())
        {
            SalvarEap(e.Parameter);
        }
        else
        {
            callbackSalvar.JSProperties["cp_status"] = "erro";
            callbackSalvar.JSProperties["cp_msg"] = "EAP bloqueada para gravação!";
        }
        callbackSalvar.JSProperties["cpCodigo"] = idEdicaoEap;
    }

    void SalvarEap(string jsonString)
    {
        using (var connection = new SqlConnection(cDados.ConnectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction("transacao-eap"))
            {
                try
                {
                    DateTime? dataInicioProjeto = ObtemDataInicioProjeto();
                    string codigoCronogramaReplanejamento = ObterCodigoCronogramaReplanejamento();
                    AtualizaCronogramaProjeto(transaction, codigoCronogramaReplanejamento);
                    ItemEap[] itensEap = DesserializaItensEap(jsonString);
                    LimpaCronogramaProjeto(transaction, itensEap, codigoCronogramaReplanejamento);
                    foreach (var item in itensEap)
                    {
                        SalvaItemEap(transaction, item, codigoCronogramaReplanejamento, dataInicioProjeto);
                    }
                    AtualizarEstruturaHierarquica(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    //Could not convert string to integer: 2,486. Path '[5].Duracao', line 8, position 400.
                    transaction.Rollback("transacao-eap");
                    throw;
                }
            }
        }
    }

    private static ItemEap[] DesserializaItensEap(string jsonString)
    {
        var jsonSerializer = new JsonSerializer()
        {
            Culture = CultureInfo.CurrentCulture,
            DateFormatString = CultureInfo.CurrentCulture.Name.StartsWith("en") ? "MM/dd/yyyy" : "dd/MM/yyyy"
        };
        var itensEap = ((JArray)JObject.Parse(jsonString)["nodeDataArray"]).ToObject<ItemEap[]>(jsonSerializer);
        return itensEap;
    }

    private DateTime? ObtemDataInicioProjeto()
    {
        DateTime? dataInicioProjeto = null;
        var ds = GetCronogramaProjetoByIdEdicaoEap(idEdicaoEap);
        var row = ds.Tables.OfType<DataTable>().SelectMany(dt => dt.AsEnumerable()).FirstOrDefault();
        if (row != null && !row.IsNull("InicioProjeto"))
        {
            dataInicioProjeto = row.Field<DateTime>("InicioProjeto");
        }

        return dataInicioProjeto;
    }

    private void AtualizarEstruturaHierarquica(SqlTransaction transaction)
    {
        var comandoSQL = string.Format(@"
                    UPDATE {0}.{1}.[TarefaCronogramaProjeto] 
                       SET [EstruturaHierarquica] = {0}.{1}.f_GetEstruturaHierarquicaTarefa(cp.[CodigoProjeto], [CodigoTarefa])

                    FROM {0}.{1}.[ControleEdicaoEap]                    AS [ceap]
                        INNER JOIN {0}.{1}.[CronogramaProjeto]          AS [cp] 
                            ON (cp.[CodigoProjeto] = ceap.[CodigoProjeto])   
                        INNER JOIN {0}.{1}.[TarefaCronogramaProjeto]    AS [tc] 
                            ON (tc.[CodigoCronogramaProjeto] = cp.[CodigoCronogramaProjeto])
                    WHERE ceap.[IDEdicaoEap]  =  '{2}'
                    ", bancodb, ownerdb, idEdicaoEap);

        ExecuteCommandInTransaction(transaction, comandoSQL);
    }

    private void LimpaCronogramaProjeto(SqlTransaction transaction, ItemEap[] itensEap, string codigoCronogramaReplanejamento)
    {
        var ids = string.Join(",", itensEap.Select(i => string.Format("'{0}'", i.CodigoInternoTarefa)));
        if (string.IsNullOrEmpty(ids))
            ids = "-1";

        var comandoSQL = string.Format(@"     

                DELETE {0}.{1}.[AtribuicaoRecursoTarefa] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto   
                   AND CodigoTarefa IN(SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto AND IDTarefa NOT IN({2}))

                DELETE {0}.{1}.[TarefaCronogramaProjetoPredecessoras] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto   
                   AND (
                    CodigoTarefa IN ( SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto AND IDTarefa NOT IN ({2}) )
                    OR
                    CodigoTarefaPredecessora IN ( SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto AND IDTarefa NOT IN ({2}) )
                   )
                
            DELETE FROM [dbo].[TarefaCronogramaProjetoTipoTarefa]
                  WHERE [CodigoCronogramaProjeto] = @CodigoCronogramaProjeto
                    AND [CodigoTarefa] IN(SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto AND IDTarefa NOT IN({2}))

                DELETE {0}.{1}.[TarefaCronogramaProjeto] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                   AND IDTarefa NOT IN({2})
                ", bancodb, ownerdb, ids);

        var dicionarioParametros = new Dictionary<string, object>();
        dicionarioParametros.Add("@CodigoCronogramaProjeto", codigoCronogramaReplanejamento);

        ExecuteCommandInTransaction(transaction, comandoSQL, dicionarioParametros);
    }

    private void SalvaItemEap(SqlTransaction transaction, ItemEap item, string codigoCronogramaReplanejamento, DateTime? dataInicioProjeto = null)
    {
        #region Valores

        if (string.IsNullOrWhiteSpace(item.idTarefa))
            item.idTarefa = Guid.NewGuid().ToString();
        if (!item.Inicio.HasValue)
        {
            item.Inicio = dataInicioProjeto;
        }
        if (!item.Duracao.HasValue || item.Duracao.Value == 0)
        {
            item.Duracao = 1;
        }
        if (!item.Termino.HasValue)
        {
            item.Termino = item.Inicio.Value.AddDays((double)item.Duracao.Value);
        }
        if (!item.Trabalho.HasValue)
        {
            item.Trabalho = 0;
        }
        if (!item.Custo.HasValue)
        {
            item.Custo = 0;
        }        
        var tarefaCronogramaProjeto = new
        {
            CodigoCronogramaProjeto = codigoCronogramaReplanejamento,
            CodigoTarefa = item.key,
            NomeTarefa = item.name,
            SequenciaTarefaCronograma = item.SequenciaTarefaCronograma,
            CodigoTarefaSuperior = item.parent,
            Nivel = item.nivel,
            Duracao = item.Duracao,
            Inicio = item.Inicio,//(inicioTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)"),
            Termino = item.Termino,//(terminoTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + terminoTarefa + "', 103)"),
            Trabalho = item.Trabalho,
            Custo = item.Custo,
            PercentualFisicoPrevisto = 0,
            PercentualFisicoConcluido = 0,
            DataInclusao = DateTime.Now,
            FormatoDuracao = "d",
            FormatoTrabalho = "H",
            IndicaMarco = "N",
            IndicaTarefaCritica = "N",
            IndicaTarefaResumo = item.nivel == 0 ? "S" : item.IndicaResumo,  // a tarefa nivel 0 tem que gravar sempre como resumo para não dar erro no Tasques.
            IndicaEap = "S",
            IndicaTarefaResumoCronograma = item.nivel == 0 ? "S" : "N",
            IndicaInicioFixado = item.Inicio.HasValue ? "S" : "N",
            IndicaTerminoFixado = item.Termino.HasValue ? "S" : "N",
            TipoCalculoTarefa = "DF",
            Anotacoes = item.Anotacoes,
            IndicaLinhaBasePendente = "N",
            IDTarefa = item.idTarefa,
            CodigoUsuarioResponsavel = item.CodigoUsuarioResponsavel,
            XmlEstiloEap = "IndicaEAPFechada:" + item.indicaFechado,
            ValorPesoTarefa = item.ValorPeso,
            PercentualPesoTarefa = item.PercentualPesoTarefa ?? 0,
            Receita = item.Receita,
            CriterioAceitacao = item.CriterioAceitacao,
            //DuracaoEmMinutos = "",//(SELECT {0}.{1}.f_crono_GetValorDuracaoConvertido({8},'d', 'n', @CodigoCronogramaProjeto)),
            IndicaAtribuicaoManualPesoTarefa = item.IndicaAtribuicaoManualPesoTarefa,
            DataRestricao = item.Inicio,
            tipoRestricao = 4,
            edt = item.EDT
        };

        #endregion

        #region Parametros

        var dicionarioParametros = new Dictionary<string, object>();
        dicionarioParametros.Add("@CodigoCronogramaProjeto", tarefaCronogramaProjeto.CodigoCronogramaProjeto);
        dicionarioParametros.Add("@CodigoTarefa", tarefaCronogramaProjeto.CodigoTarefa);
        dicionarioParametros.Add("@NomeTarefa", tarefaCronogramaProjeto.NomeTarefa);
        dicionarioParametros.Add("@SequenciaTarefaCronograma", tarefaCronogramaProjeto.SequenciaTarefaCronograma);
        dicionarioParametros.Add("@CodigoTarefaSuperior", tarefaCronogramaProjeto.CodigoTarefaSuperior);
        dicionarioParametros.Add("@Nivel", tarefaCronogramaProjeto.Nivel);
        dicionarioParametros.Add("@Duracao", tarefaCronogramaProjeto.Duracao);
        dicionarioParametros.Add("@Inicio", tarefaCronogramaProjeto.Inicio);
        dicionarioParametros.Add("@Termino", tarefaCronogramaProjeto.Termino);
        dicionarioParametros.Add("@Trabalho", tarefaCronogramaProjeto.Trabalho);
        dicionarioParametros.Add("@Custo", tarefaCronogramaProjeto.Custo);
        dicionarioParametros.Add("@PercentualFisicoPrevisto", tarefaCronogramaProjeto.PercentualFisicoPrevisto);
        dicionarioParametros.Add("@PercentualFisicoConcluido", tarefaCronogramaProjeto.PercentualFisicoConcluido);
        dicionarioParametros.Add("@DataInclusao", tarefaCronogramaProjeto.DataInclusao);
        dicionarioParametros.Add("@FormatoDuracao", tarefaCronogramaProjeto.FormatoDuracao);
        dicionarioParametros.Add("@FormatoTrabalho", tarefaCronogramaProjeto.FormatoTrabalho);
        dicionarioParametros.Add("@IndicaMarco", tarefaCronogramaProjeto.IndicaMarco);
        dicionarioParametros.Add("@IndicaTarefaCritica", tarefaCronogramaProjeto.IndicaTarefaCritica);
        dicionarioParametros.Add("@IndicaTarefaResumo", tarefaCronogramaProjeto.IndicaTarefaResumo);
        dicionarioParametros.Add("@IndicaEap", tarefaCronogramaProjeto.IndicaEap);
        dicionarioParametros.Add("@IndicaTarefaResumoCronograma", tarefaCronogramaProjeto.IndicaTarefaResumoCronograma);
        dicionarioParametros.Add("@IndicaInicioFixado", tarefaCronogramaProjeto.IndicaInicioFixado);
        dicionarioParametros.Add("@IndicaTerminoFixado", tarefaCronogramaProjeto.IndicaTerminoFixado);
        dicionarioParametros.Add("@TipoCalculoTarefa", tarefaCronogramaProjeto.TipoCalculoTarefa);
        dicionarioParametros.Add("@Anotacoes", tarefaCronogramaProjeto.Anotacoes);
        dicionarioParametros.Add("@IndicaLinhaBasePendente", tarefaCronogramaProjeto.IndicaLinhaBasePendente);
        dicionarioParametros.Add("@IDTarefa", tarefaCronogramaProjeto.IDTarefa);
        dicionarioParametros.Add("@CodigoUsuarioResponsavel", tarefaCronogramaProjeto.CodigoUsuarioResponsavel);
        dicionarioParametros.Add("@XmlEstiloEap", tarefaCronogramaProjeto.XmlEstiloEap);
        dicionarioParametros.Add("@ValorPesoTarefa", tarefaCronogramaProjeto.ValorPesoTarefa);
        dicionarioParametros.Add("@PercentualPesoTarefa", tarefaCronogramaProjeto.PercentualPesoTarefa);
        dicionarioParametros.Add("@Receita", tarefaCronogramaProjeto.Receita);
        dicionarioParametros.Add("@CriterioAceitacao", tarefaCronogramaProjeto.CriterioAceitacao);
        //dicionarioParametros.Add("@DuracaoEmMinutos", tarefaCronogramaProjeto.DuracaoEmMinutos);
        dicionarioParametros.Add("@IndicaAtribuicaoManualPesoTarefa", tarefaCronogramaProjeto.IndicaAtribuicaoManualPesoTarefa);
        dicionarioParametros.Add("@DataRestricao", tarefaCronogramaProjeto.DataRestricao);
        dicionarioParametros.Add("@tipoRestricao", tarefaCronogramaProjeto.tipoRestricao);
        dicionarioParametros.Add("@edt", tarefaCronogramaProjeto.edt);

        #endregion

        #region Comando SQL

        string comandoSql = string.Empty;

        if (item.CodigoInternoTarefa == "-1")
        {
            comandoSql = @"
INSERT INTO TarefaCronogramaProjeto
(
  [CodigoCronogramaProjeto],
  [CodigoTarefa],
  [NomeTarefa],
  [SequenciaTarefaCronograma],
  [CodigoTarefaSuperior],
  [Nivel],
  [Duracao],
  [Inicio],
  [Termino],
  [Trabalho],
  [Custo],
  [PercentualFisicoPrevisto],
  [PercentualFisicoConcluido],
  [DataInclusao],
  [FormatoDuracao],
  [FormatoTrabalho],
  [IndicaMarco],
  [IndicaTarefaCritica],
  [IndicaTarefaResumo],
  [IndicaEap],
  [IndicaTarefaResumoCronograma],
  [IndicaInicioFixado],
  [IndicaTerminoFixado],
  [TipoCalculoTarefa],
  [Anotacoes],
  [IndicaLinhaBasePendente],
  [IDTarefa],
  [CodigoUsuarioResponsavel],
  [XmlEstiloEap],
  [ValorPesoTarefa],
  [PercentualPesoTarefa],
  [Receita],
  [CriterioAceitacao],
  [DuracaoEmMinutos],
  [IndicaAtribuicaoManualPesoTarefa],
  [DataRestricao],
  [tipoRestricao],
  [edt]
)
VALUES
(
  @CodigoCronogramaProjeto,
  @CodigoTarefa,
  @NomeTarefa,
  @SequenciaTarefaCronograma,
  @CodigoTarefaSuperior,
  @Nivel,
  @Duracao,
  @Inicio,
  @Termino,
  @Trabalho,
  @Custo,
  @PercentualFisicoPrevisto,
  @PercentualFisicoConcluido,
  @DataInclusao,
  @FormatoDuracao,
  @FormatoTrabalho,
  @IndicaMarco,
  @IndicaTarefaCritica,
  @IndicaTarefaResumo,
  @IndicaEap,
  @IndicaTarefaResumoCronograma,
  @IndicaInicioFixado,
  @IndicaTerminoFixado,
  @TipoCalculoTarefa,
  @Anotacoes,
  @IndicaLinhaBasePendente,
  @IDTarefa,
  @CodigoUsuarioResponsavel,
  @XmlEstiloEap,
  @ValorPesoTarefa,
  @PercentualPesoTarefa,
  @Receita,
  @CriterioAceitacao,
  (SELECT dbo.f_crono_GetValorDuracaoConvertido(@Duracao,'d', 'n', @CodigoCronogramaProjeto)),
  @IndicaAtribuicaoManualPesoTarefa,
  @DataRestricao,
  @tipoRestricao,
  @edt
)";
        }
        else
        {
            comandoSql = @"
UPDATE TarefaCronogramaProjeto SET
      [CodigoTarefa] = @CodigoTarefa
    , [NomeTarefa] = @NomeTarefa
    , [SequenciaTarefaCronograma] = @SequenciaTarefaCronograma
    , [CodigoTarefaSuperior] = @CodigoTarefaSuperior
    , [Nivel] = @Nivel
    , [Duracao] = @Duracao
    , [Inicio] = @Inicio
    , [Termino] = @Termino
    , [Trabalho] = @Trabalho
    , [Custo] = @Custo
    , [DataUltimaAlteracao] = GETDATE()
    , [IndicaTarefaResumo] = @IndicaTarefaResumo
    , [IndicaTarefaResumoCronograma] = @IndicaTarefaResumoCronograma
    , [IndicaInicioFixado] = @IndicaInicioFixado
    , [IndicaTerminoFixado] = @IndicaTerminoFixado
    , [Anotacoes] = @Anotacoes
    , [CodigoUsuarioResponsavel] = @CodigoUsuarioResponsavel
    , [XmlEstiloEap] = @XmlEstiloEap
    , [ValorPesoTarefa] = @ValorPesoTarefa
    , [PercentualPesoTarefa] = @PercentualPesoTarefa
    , [Receita] = @Receita
    , [CriterioAceitacao] = @CriterioAceitacao
    , [DuracaoEmMinutos] = (SELECT dbo.f_crono_GetValorDuracaoConvertido(@Duracao,'d', 'n', @CodigoCronogramaProjeto))
    , [IndicaAtribuicaoManualPesoTarefa] = @IndicaAtribuicaoManualPesoTarefa
    , [DataRestricao] = @DataRestricao
    , [tipoRestricao] = @tipoRestricao
    , [edt] = @edt
WHERE [IDTarefa] = @IDTarefa
AND [CodigoCronogramaProjeto] = @CodigoCronogramaProjeto";
        }

        if (!tarefaCronogramaProjeto.IndicaTarefaResumo.Equals("S", StringComparison.CurrentCultureIgnoreCase))
        {
            comandoSql += string.Format("{5}EXEC {0}.{1}.p_crono_registraAtribuicaoRecursoTarefa {2}, '{3}', {4}{5}"
                , bancodb
                , ownerdb
                , codigoProjeto
                , codigoCronograma
                , tarefaCronogramaProjeto.CodigoTarefa
                , Environment.NewLine);
        }

        #endregion

        ExecuteCommandInTransaction(transaction, comandoSql, dicionarioParametros);
    }

    private void AtualizaCronogramaProjeto(SqlTransaction transaction, string codigoCronogramaReplanejamento)
    {
        var estiloEAP = ObtemEstiloEap();
        var comandoSql = @"UPDATE CronogramaProjeto SET EstiloVisualEAP = @EstiloVisualEAP WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto";
        var dicionarioParametros = new Dictionary<string, object>();
        dicionarioParametros.Add("@CodigoCronogramaProjeto", codigoCronogramaReplanejamento);
        dicionarioParametros.Add("@EstiloVisualEAP", estiloEAP);
        ExecuteCommandInTransaction(transaction, comandoSql, dicionarioParametros);
    }

    private static void ExecuteCommandInTransaction(SqlTransaction transaction, string sql, Dictionary<string, object> parametersDictionary = null)
    {
        var command = transaction.Connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = sql;
        if (parametersDictionary != null)
        {
            foreach (var item in parametersDictionary)
            {
                var parameter = new SqlParameter(item.Key, item.Value ?? DBNull.Value);
                command.Parameters.Add(parameter);
            }
        }
        command.ExecuteNonQuery();
    }

    private string ObtemEstiloEap()
    {
        string corCaixas = colorEdit.Text;
        string corFonte = colorEditFonte.Text;
        string tamanhoFonte = hfGeral.Get("Fonte").ToString();
        string escala = hfGeral.Get("Escala").ToString().Replace(",", ".");
        string visao = ddlVisao.Value.ToString();
        string layout = ddlLayout.Value.ToString();
        string estiloEAP = string.Format("CorCaixa:{0},CorFonte:{1},TamanhoFonte:{2},Escala:{3},Visao:{4},Layout:{5}", corCaixas, corFonte, tamanhoFonte, escala, visao, layout);
        return estiloEAP;
    }

    private string ObterCodigoCronogramaReplanejamento()
    {
        var codigoCronogramaReplanejamento = string.Empty;
        var comandoSQL = string.Format(@"EXEC {0}.{1}.p_eap_BuscaCodigoEAP {2}",
            cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        var ds = cDados.getDataSet(comandoSQL);
        var dr = ds.Tables.OfType<DataTable>().SelectMany(dt => dt.AsEnumerable()).FirstOrDefault();

        if ((dr != null) && (!dr.IsNull("CronogramaReplanejamento")))
            codigoCronogramaReplanejamento = dr.Field<string>("CronogramaReplanejamento");

        return string.IsNullOrEmpty(codigoCronogramaReplanejamento) ? "-1" : codigoCronogramaReplanejamento;
    }

    private void SalvaEapOld(DevExpress.Web.CallbackEventArgs e)
    {
        #region codigo coberto
        string json = e.Parameter;
        var objects = JObject.Parse(json);
        string corCaixas = colorEdit.Text;
        string corFonte = colorEditFonte.Text;
        string tamanhoFonte = hfGeral.Get("Fonte").ToString();
        string escala = hfGeral.Get("Escala").ToString().Replace(",", ".");
        string visao = ddlVisao.Value.ToString();
        string layout = ddlLayout.Value.ToString();
        string comandoSQLUpdate = "", comandoSQLInsert = "";
        string codigoCronogramaReplanejamento = "";
        string estiloEAP = string.Format("CorCaixa:{0},CorFonte:{1},TamanhoFonte:{2},Escala:{3},Visao:{4},Layout:{5}", corCaixas, corFonte, tamanhoFonte, escala, visao, layout);

        string comandoSQL = string.Format(@"
               EXEC {0}.{1}.p_eap_BuscaCodigoEAP {2}
                ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            codigoCronogramaReplanejamento = dr["CronogramaReplanejamento"].ToString() == "" ? "-1" : dr["CronogramaReplanejamento"].ToString();
        }
        else
        {
            codigoCronogramaReplanejamento = "-1";
        }

        comandoSQL = string.Format(@"
                --1ro) Apago o registros do conograma do projeto.
                
                DECLARE @CodigoCronogramaProjeto VARCHAR(64)
                
                SET @CodigoCronogramaProjeto = '{4}'

                UPDATE {0}.{1}.CronogramaProjeto SET EstiloVisualEAP = '{3}' WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto

                

                ", bancodb, ownerdb, idEdicaoEap, estiloEAP, codigoCronogramaReplanejamento);
        #endregion


        int index = 0;
        string whereDelete = "AND IDTarefa NOT IN('-1'";

        foreach (KeyValuePair<String, JToken> app in objects)
        {
            if (app.Key == "nodeDataArray")
            {
                string array = app.Value.ToString();
                var valores = JArray.Parse(array);

                foreach (JObject root in valores)
                {
                    string codigoTarefa = (string)root["key"];
                    string nome = (string)root["name"];
                    string parent = (string)root["parent"];
                    string guid = (string)root["idTarefa"];
                    string nivel = (string)root["nivel"];
                    string indicaFechado = (string)root["indicaFechado"];
                    string ValorPeso = (string)root["ValorPeso"];
                    string PercentualPesoTarefa = (string)root["PercentualPesoTarefa"];

                    string Inicio = (string)root["Inicio"];
                    string Termino = (string)root["Termino"];
                    string Custo = (string)root["Custo"];
                    string Trabalho = (string)root["Trabalho"];
                    string Receita = (string)root["Receita"];
                    string CodigoUsuarioResponsavel = ((string)root["CodigoUsuarioResponsavel"]) ?? "null";
                    string Anotacoes = (string)root["Anotacoes"];
                    string codigoInternoTarefa = (string)root["CodigoInternoTarefa"];
                    string Duracao = (string)root["Duracao"];
                    string CriterioAceitacao = (string)root["CriterioAceitacao"];
                    string IndicaResumo = (string)root["IndicaResumo"];
                    string SequenciaTarefaCronograma = (string)root["SequenciaTarefaCronograma"];
                    string IndicaAtribuicaoManualPesoTarefa = (string)root["IndicaAtribuicaoManualPesoTarefa"];
                    string EDT = (string)root["EDT"];

                    string estilo = "IndicaEAPFechada:" + indicaFechado;

                    whereDelete += ",'" + codigoInternoTarefa + "'";

                    gravaEapNaBaseDeDados(codigoTarefa, nome, parent, guid, IndicaResumo, SequenciaTarefaCronograma, nivel
                        , ValorPeso, PercentualPesoTarefa, Inicio, Termino, Custo, Trabalho, Receita, CodigoUsuarioResponsavel, Anotacoes, codigoInternoTarefa
                        , Duracao, CriterioAceitacao, estilo, IndicaAtribuicaoManualPesoTarefa, EDT, ref comandoSQLInsert, ref comandoSQLUpdate);
                }
            }
        }

        comandoSQL += string.Format(@"     

                DELETE {0}.{1}.[AtribuicaoRecursoTarefa] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto   
                   AND CodigoTarefa IN(SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto {2})

                DELETE {0}.{1}.[TarefaCronogramaProjetoPredecessoras] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto   
                   AND (
                    CodigoTarefa IN ( SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto {2} )
                    OR
                    CodigoTarefaPredecessora IN ( SELECT CodigoTarefa FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto {2} )
                   )

                DELETE {0}.{1}.[TarefaCronogramaProjeto] 
                 WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                   {2}
                ", bancodb, ownerdb, whereDelete + ")");

        comandoSQL += comandoSQLUpdate;
        comandoSQL += comandoSQLInsert;

        comandoSQL += string.Format(@"
                    --3ro) atualizando a estructura gerarquita.

                    UPDATE {0}.{1}.[TarefaCronogramaProjeto] 
                       SET [EstruturaHierarquica] = {0}.{1}.f_GetEstruturaHierarquicaTarefa(cp.[CodigoProjeto], [CodigoTarefa])

                    FROM {0}.{1}.[ControleEdicaoEap]                    AS [ceap]
                        INNER JOIN {0}.{1}.[CronogramaProjeto]          AS [cp] 
                            ON (cp.[CodigoProjeto] = ceap.[CodigoProjeto])   
                        INNER JOIN {0}.{1}.[TarefaCronogramaProjeto]    AS [tc] 
                            ON (tc.[CodigoCronogramaProjeto] = cp.[CodigoCronogramaProjeto])
                    WHERE ceap.[IDEdicaoEap]  =  '{2}'
                    ", bancodb, ownerdb, idEdicaoEap);

        //System.Diagnostics.Debug.WriteLine(comandoSQL);
        int regAfetados = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAfetados);

        if (result)
        {
            callbackSalvar.JSProperties["cp_status"] = "ok";
            callbackSalvar.JSProperties["cp_msg"] = "EAP salva com sucesso!";
        }
        else
        {
            callbackSalvar.JSProperties["cp_status"] = "erro";
            callbackSalvar.JSProperties["cp_msg"] = "Erro ao salvar EAP!";
        }
    }

    private bool PodeAlterarEap()
    {
        bool podeSalbar = true;
        //object CodigoCronogramaProjeto;
        //object DataUltimaGravacaoDesktop;
        //object CodigoUsuarioCheckoutCronograma;
        //object DataCheckoutCronograma;
        //object DataInicioEdicao;
        //object CodigoUsuarioEdicao;
        //string ModoAcesso;

        //string commandoSQL = string.Format(@"
        //    DECLARE @idEdicaoEap      VARCHAR(64)
        //    DECLARE @CodigoProjeto  INT

        //    SET @idEdicaoEap = '{2}'

        //    SET @CodigoProjeto = (SELECT CodigoProjeto FROM {0}.{1}.ControleEdicaoEap WHERE IDEdicaoEap = @idEdicaoEap)

        //    SELECT CP.CodigoCronogramaProjeto
        //        ,CP.DataUltimaGravacaoDesktop
        //        ,CP.CodigoUsuarioCheckoutCronograma
        //        ,CP.DataCheckoutCronograma
        //        ,CEAP.DataInicioEdicao
        //        ,CEAP.CodigoUsuarioEdicao
        //        ,CEAP.ModoAcesso
        //    FROM 
        //        {0}.{1}.ControleEdicaoEap CEAP 
        //            INNER JOIN {0}.{1}.CronogramaProjeto CP  ON 
        //                    (CP.CodigoProjeto = CEAP.CodigoProjeto)   

        //    WHERE
        //            CEAP.[IDEdicaoEap]  =  @idEdicaoEap
        //    ", cDados.getDbName(), cDados.getDbOwner(), idEdicaoEap);

        //DataSet ds = cDados.getDataSet(commandoSQL);
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    System.Data.DataRow row = ds.Tables[0].Rows[0];
        //    CodigoCronogramaProjeto = row["CodigoCronogramaProjeto"];
        //    DataUltimaGravacaoDesktop = row["DataUltimaGravacaoDesktop"];
        //    CodigoUsuarioCheckoutCronograma = row["CodigoUsuarioCheckoutCronograma"];
        //    DataCheckoutCronograma = row["DataCheckoutCronograma"];
        //    DataInicioEdicao = row["DataInicioEdicao"];
        //    CodigoUsuarioEdicao = row["CodigoUsuarioEdicao"];
        //    ModoAcesso = row["ModoAcesso"].ToString();

        //    if ((DataUltimaGravacaoDesktop == System.DBNull.Value) && ModoAcesso.Equals("G")
        //         && (CodigoUsuarioEdicao.Equals(CodigoUsuarioCheckoutCronograma))
        //         && (DataInicioEdicao.Equals(DataCheckoutCronograma)))
        //        podeSalbar = true;
        //}
        return podeSalbar;
    }

    private void gravaEapNaBaseDeDados(string idTarefa, string nomeTarefa, string parent, string guidTarefa, string indicaResumo, string sequencia, string nivel
        , string ValorPeso, string PercentualPesoTarefa, string Inicio, string Termino, string Custo, string Trabalho, string Receita, string CodigoUsuarioResponsavel, string Anotacoes, string codigoInternoTarefa
        , string duracao, string criterioAceitacao, string estilo, string indicaAtribuicaoManualPesoTarefa, string edt, ref string comandoSQLInsert, ref string comandoSQLUpdate)
    {
        DataRow enCronogramaProjeto;// = new DataRow();

        string varNomeTarefa = "";
        string varGuidTarefa = "";
        string varCodigoTarefa = "";
        string varCodigoSupTarefa = "";
        string varSecuenciaTarefa = sequencia;
        string varNivelTarefa = nivel;
        string varTarefaResumo = indicaResumo;

        string inicioTarefa = (Inicio ?? string.Empty);
        string terminoTarefa = (Termino ?? string.Empty);
        //string terminoTarefaPadrao = "";
        string duracaoTarefa = (duracao ?? string.Empty);
        string trabalhoTarefa = (Trabalho ?? string.Empty);
        string custoTarefa = (Custo ?? string.Empty);
        string receitaTarefa = (Receita ?? string.Empty);
        string IndicaTarefaResumoCronograma = nivel == "0" ? "S" : "N";
        string IndicaInicioFixado = Inicio != "" ? "S" : "N";
        string IndicaTerminoFixado = Termino != "" ? "S" : "N";
        string anotacoes = (Anotacoes ?? string.Empty).Replace("'", "''");
        string usuarioResponsavel = CodigoUsuarioResponsavel;

        varNomeTarefa = (nomeTarefa ?? string.Empty).Replace("'", "''");
        varGuidTarefa = guidTarefa;
        varCodigoTarefa = idTarefa;
        varCodigoSupTarefa = parent;

        //
        //Verificar os valores caso qeu nao tenha valor, seran setados com valores padrões...
        //
        if (inicioTarefa == null || inicioTarefa == "")
        {
            DataSet ds = GetCronogramaProjetoByIdEdicaoEap(idEdicaoEap);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                System.Data.DataRow row = ds.Tables[0].Rows[0];
                inicioTarefa = ((DateTime)row["InicioProjeto"]).ToString("dd/MM/yyyy HH:mm:ss");
            }

        }
        if ("" == duracaoTarefa || null == duracaoTarefa)
            duracaoTarefa = "1";
        if (terminoTarefa == null || terminoTarefa == "")
        {
            IFormatProvider ifpBR = CultureInfo.CurrentCulture;//new CultureInfo("pt-BR", false);

            DateTime d = DateTime.Parse(inicioTarefa, ifpBR);
            terminoTarefa = d.AddDays(double.Parse(duracaoTarefa)).ToString("dd/MM/yyyy HH:mm:ss");
        }

        if ("" == trabalhoTarefa || null == trabalhoTarefa)
            trabalhoTarefa = "0";
        if ("" == custoTarefa || null == custoTarefa)
            custoTarefa = "0";
        if (IndicaInicioFixado == "")
            IndicaInicioFixado = "N";
        if (IndicaTerminoFixado == "")
            IndicaTerminoFixado = "N";


        if (varGuidTarefa == "")
            varGuidTarefa = Guid.NewGuid().ToString();

        if (codigoInternoTarefa == "-1")
        {
            comandoSQLInsert += string.Format(@"
                 INSERT INTO {0}.{1}.TarefaCronogramaProjeto(
                             [CodigoCronogramaProjeto], [CodigoTarefa], [NomeTarefa], [SequenciaTarefaCronograma]
                            , [CodigoTarefaSuperior], [Nivel], [Duracao], [Inicio], [Termino], [Trabalho], [Custo], [PercentualFisicoPrevisto]
                            , [PercentualFisicoConcluido], [DataInclusao], [FormatoDuracao], [FormatoTrabalho], [IndicaMarco], [IndicaTarefaCritica]
                            , [IndicaTarefaResumo], [IndicaEap], [IndicaTarefaResumoCronograma], [IndicaInicioFixado]
                            , [IndicaTerminoFixado], [TipoCalculoTarefa], [Anotacoes], [IndicaLinhaBasePendente], [IDTarefa]
                            , [CodigoUsuarioResponsavel], [XmlEstiloEap], [ValorPesoTarefa], [PercentualPesoTarefa], [Receita], [CriterioAceitacao]
                            , [DuracaoEmMinutos], [IndicaAtribuicaoManualPesoTarefa], [DataRestricao], [tipoRestricao], [edt]
                            )
                    VALUES(@CodigoCronogramaProjeto
                        , {3}
                        , '{4}'
                        , {5}
                        , {6}
                        , {7}
                        ,{8}
                        ,{9}
                        ,{10}
                        ,{11}
                        ,{12}
                        ,0
                        ,0
                        ,GETDATE()
                        ,'d'
                        ,'H'
                        ,'N'
                        ,'N'
                        ,'{13}'
                        ,'S'
                        ,'{14}'
                        ,'{15}'
                        ,'{16}'
                        ,'DF'
                        ,'{17}'
                        ,'N'
                        ,'{18}'
                        , {19}
                        ,'{20}'
                        , {21}
                        , {22}
                        , {24}
                        ,'{25}'
                        ,(SELECT {0}.{1}.f_crono_GetValorDuracaoConvertido({8},'d', 'n', @CodigoCronogramaProjeto))
                        ,'{26}'
                        , {27}
                        , {28}
                        ,'{29}') 
                "
             , bancodb
             , ownerdb
             , idEdicaoEap
             , varCodigoTarefa
             , varNomeTarefa
             , varSecuenciaTarefa
             , (varCodigoSupTarefa == null ? "NULL" : varCodigoSupTarefa)
             , varNivelTarefa
             , duracaoTarefa.Replace(".", "").Replace(",", ".")
             , (inicioTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)")
             , (terminoTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + terminoTarefa + "', 103)")
             , trabalhoTarefa.Replace(".", "").Replace(",", ".")
             , custoTarefa.Replace(".", "").Replace(",", ".")
             , varTarefaResumo
             , IndicaTarefaResumoCronograma
             , IndicaInicioFixado
             , IndicaTerminoFixado
             , anotacoes
             , varGuidTarefa
             , (usuarioResponsavel != "" ? usuarioResponsavel : "NULL")
             , estilo
             , ValorPeso.Replace(".", "").Replace(",", ".")
             , PercentualPesoTarefa == "" ? "0" : PercentualPesoTarefa.Replace(".", "").Replace(",", ".")
             , codigoInternoTarefa
             , receitaTarefa == "" ? "NULL" : receitaTarefa.Replace(".", "").Replace(",", ".")
             , (criterioAceitacao ?? string.Empty).Replace("'", "''")
             , indicaAtribuicaoManualPesoTarefa
             , (string.IsNullOrWhiteSpace(terminoTarefa) || terminoTarefa == "NULL" ? "[DataRestricao]" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)")
             , (string.IsNullOrWhiteSpace(terminoTarefa) || terminoTarefa == "NULL" ? "[tipoRestricao]" : "4")
             , edt);
            if (!"S".Equals(varTarefaResumo, StringComparison.CurrentCultureIgnoreCase))
            {
                comandoSQLInsert += string.Format("{5}EXEC {0}.{1}.p_crono_registraAtribuicaoRecursoTarefa {2}, '{3}', {4}{5}"
                    , bancodb
                    , ownerdb
                    , codigoProjeto
                    , codigoCronograma
                    , varCodigoTarefa
                    , Environment.NewLine);
            }
        }
        else
        {
            comandoSQLUpdate += string.Format(@"               
                    UPDATE {0}.{1}.TarefaCronogramaProjeto SET
                         [CodigoTarefa] = {3}
                       , [NomeTarefa] = '{4}'
                       , [SequenciaTarefaCronograma] = {5}
                       , [CodigoTarefaSuperior] = {6}
                       , [Nivel] = {7}
                       , [Duracao] = {8}
                       , [Inicio] = {9}
                       , [Termino] = {10}
                       , [Trabalho] = {11}
                       , [Custo] = {12}
                       , [DataUltimaAlteracao] = GETDATE()
                       , [IndicaTarefaResumo] = '{13}'
                       , [IndicaTarefaResumoCronograma] = '{14}'
                       , [IndicaInicioFixado] = '{15}'
                       , [IndicaTerminoFixado] = '{16}'
                       , [Anotacoes] = '{17}'
                       , [CodigoUsuarioResponsavel] = {19}
                       , [XmlEstiloEap] = '{20}'
                       , [ValorPesoTarefa] = {21}
                       , [PercentualPesoTarefa] = {22}
                       , [Receita] = {24}
                       , [CriterioAceitacao] = '{25}'
                       , [DuracaoEmMinutos] = (SELECT {0}.{1}.f_crono_GetValorDuracaoConvertido({8},'d', 'n', @CodigoCronogramaProjeto))
                       , [IndicaAtribuicaoManualPesoTarefa] = '{26}'
                       , [DataRestricao] = {27}
                       , [tipoRestricao] = {28}
                       , [edt] = '{29}'
                 WHERE [IDTarefa] = '{23}'
                   AND [CodigoCronogramaProjeto] = @CodigoCronogramaProjeto
                "
         , bancodb
         , ownerdb
         , idEdicaoEap
         , varCodigoTarefa
         , varNomeTarefa
         , varSecuenciaTarefa
         , (varCodigoSupTarefa == null ? "NULL" : varCodigoSupTarefa)
         , varNivelTarefa
         , duracaoTarefa.Replace(".", "").Replace(",", ".")
         , (inicioTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)")
         , (terminoTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + terminoTarefa + "', 103)")
         , trabalhoTarefa.Replace(".", "").Replace(",", ".")
         , custoTarefa.Replace(".", "").Replace(",", ".")
         , varTarefaResumo
         , IndicaTarefaResumoCronograma
         , IndicaInicioFixado
         , IndicaTerminoFixado
         , anotacoes
         , varGuidTarefa
         , (usuarioResponsavel != "" ? usuarioResponsavel : "NULL")
         , estilo
         , ValorPeso.Replace(".", "").Replace(",", ".")
         , PercentualPesoTarefa == "" ? "0" : PercentualPesoTarefa.Replace(".", "").Replace(",", ".")
         , codigoInternoTarefa
         , receitaTarefa == "" ? "NULL" : receitaTarefa.Replace(".", "").Replace(",", ".")
         , (criterioAceitacao ?? string.Empty).Replace("'", "''")
         , indicaAtribuicaoManualPesoTarefa
         , (string.IsNullOrWhiteSpace(terminoTarefa) || terminoTarefa == "NULL" ? "[DataRestricao]" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)")
         , (string.IsNullOrWhiteSpace(terminoTarefa) || terminoTarefa == "NULL" ? "[tipoRestricao]" : "4")
         , edt);
            if (!"S".Equals(varTarefaResumo, StringComparison.CurrentCultureIgnoreCase))
            {
                comandoSQLUpdate += string.Format("{5}EXEC {0}.{1}.p_crono_registraAtribuicaoRecursoTarefa {2}, '{3}', {4}{5}"
                    , bancodb
                    , ownerdb
                    , codigoProjeto
                    , codigoCronograma
                    , varCodigoTarefa
                    , Environment.NewLine);
            }
        }
    }

    private string GetCodigoCronogramaFromProjectId(string idEdicaoEap)
    {
        string codigoCronogramaProjeto = "";

        string comandoSQL = string.Format(@"
                SELECT t.CodigoCronogramaProjeto
	                FROM
		                {0}.{1}.ControleEdicaoEap ceap
			                INNER JOIN CronogramaProjeto cp on (cp.CodigoProjeto = ceap.CodigoProjeto)
				                INNER JOIN TarefaCronogramaProjeto t on (t.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto)
	                WHERE
		                ceap.IDEdicaoEap = '{2}'
                ", bancodb, ownerdb, idEdicaoEap);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        return codigoCronogramaProjeto;
    }

    private DataSet GetCronogramaProjetoByIdEdicaoEap(string idEdicaoEap)
    {
        string codigoCronogramaProjeto = GetCodigoCronogramaFromProjectId(idEdicaoEap);
        string comandoSQL = string.Format(@"
                    SELECT * from {0}.{1}.CronogramaProjeto 
                    WHERE CodigoCronogramaProjeto = '{2}'
                ", bancodb, ownerdb, codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private bool ExisteGuidNaTarefaCronogramaProjeto(string idEdicaoEap, string varGuidTarefa, out DataRow enCronogramaProjeto)
    {
        bool encontrado = false;

        if (varGuidTarefa == null || varGuidTarefa == "")
        {
            enCronogramaProjeto = null;
            return false;
        }

        string comandoSqlTarefa = string.Format(@"
                SELECT  *
                FROM    {0}.{1}.TarefaCronogramaProjeto
                WHERE   IDTarefa = '{2}'
                ", bancodb, ownerdb, varGuidTarefa);
        DataSet ds = cDados.getDataSet(comandoSqlTarefa);
        enCronogramaProjeto = null;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            enCronogramaProjeto = ds.Tables[0].Rows[0];
            encontrado = true;
        }
        return encontrado;
    }

    //Retorna o estado da página alterado para salvamento 
    protected void ddlLayout_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SaveButton.Src = ResolveUrl(String.Format(@"~/imagens/WBSTools/{0}", "salvar.png"));
    }

    protected void ddlVisao_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SaveButton.Src = ResolveUrl(String.Format(@"~/imagens/WBSTools/{0}", "salvar.png"));
    }
}

internal static class Extensao
{
    public static T? GetNullableValue<T>(this DataRow row, string fieldName) where T : struct
    {
        if (row.IsNull(fieldName))
            return null;

        return row.Field<T>(fieldName);
    }
}

internal class ItemEap
{
    public int key { get; set; }
    public string name { get; set; }
    public int? parent { get; set; }
    public string idTarefa { get; set; }
    public short nivel { get; set; }
    public string indicaFechado { get; set; }
    public decimal? ValorPeso { get; set; }
    public decimal? PercentualPesoTarefa { get; set; }
    public DateTime? Inicio { get; set; }
    public DateTime? Termino { get; set; }
    public decimal? Custo { get; set; }
    public decimal? Trabalho { get; set; }
    public decimal? Receita { get; set; }
    public int? CodigoUsuarioResponsavel { get; set; }
    public string Anotacoes { get; set; }
    public string CodigoInternoTarefa { get; set; }
    public decimal? Duracao { get; set; }
    public string CriterioAceitacao { get; set; }
    public string IndicaResumo { get; set; }
    public int SequenciaTarefaCronograma { get; set; }
    public string IndicaAtribuicaoManualPesoTarefa { get; set; }
    public string EDT { get; set; }
}