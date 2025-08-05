using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_WizardDefinicaoLista : System.Web.UI.Page
{
    #region Fields

    int codigoUsuarioLogado;
    int codigoEntidade;
    dados cDados;

    #endregion

    #region Properties

    private string _connectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_connectionString))
                _connectionString = cDados.classeDados.getStringConexao();
            return _connectionString;
        }
    }

    #endregion

    #region Event Handlers

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
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
       SetConnectionStrings();
        ConfiguraQueryBuilder();

        this.TH(this.TS("WizardDefinicaoLista"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        (formLayout.Items[0] as TabbedLayoutGroup).ShowGroupDecoration = false;
        if (!IsPostBack)
            PermiteUsuarioEditarComando();
    }

    private void ConfiguraQueryBuilder()
    {
        var appName = string.Format("app=portal,ce={0},cul={1}", codigoEntidade, codigoUsuarioLogado);
        var parameter = GetConnectionParameter(ConnectionString, appName);
        queryBuilder.ValidateQueryByExecution = true;
        queryBuilder.OpenConnection(parameter);
    }

    protected void dataSourceLista_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        string tipoLista;
        var parametros = e.Command.Parameters;
        List<string> colunas = new List<string>(new string[]
        {
            "NomeLista",
            "TituloLista",
            "CodigoModuloMenu",
            "IndicaOpcaoDisponivel",
            "GrupoMenu",
            "ItemMenu",
            "GrupoPermissao",
            "ItemPermissao",
            "OrdemGrupoMenu",
            "OrdemItemGrupoMenu"
        });
        if (rblUrlOuTipo.Value.ToString().Equals("URL"))
        {
            colunas.Add("URL");
            tipoLista = "RELATORIO";
            parametros["@IndicaListaZebrada"].Value = "N";
        }
        else
        {
            tipoLista = cmbTipoLista.Value.ToString();
            switch (tipoLista)
            {
                case "RELATORIO":
                case "PROCESSO":
                case "OLAP":
                case "ARVORE":
                    colunas.Add("IndicaPaginacao");
                    colunas.Add("QuantidadeItensPaginacao");
                    colunas.Add("ComandoSelect");
                    if (tipoLista.Equals("RELATORIO") || tipoLista.Equals("PROCESSO"))
                    {
                        colunas.Add("IndicaListaZebrada");
                        colunas.Add("IndicaBuscaPalavraChave");
                        colunas.Add("CodigoSubLista");
                    }
                    break;
                case "DASHBOARD":
                    colunas.Add("IDDashboard");
                    break;
                case "REPORT":
                    colunas.Add("IDRelatorio");
                    break;
                default:
                    break;
            }
        }
        parametros["@CodigoUsuario"].Value = codigoUsuarioLogado;
        parametros["@CodigoEntidade"].Value = codigoEntidade;
        parametros["@TipoLista"].Value = tipoLista;

        parametros["@OrdemGrupoMenu"].Value = 1;
        parametros["@OrdemItemGrupoMenu"].Value = 1;


        foreach (var nomeColuna in colunas)
        {
            var nomeParametro = string.Format("@{0}", nomeColuna);
            parametros[nomeParametro].Value = GetValue(nomeColuna);
        }
    }

    protected void dataSourceLista_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        var gerarColunas = GetValue<bool>("unbound_GerarColunas");
        if (gerarColunas)
        {
            string comandoSelect = e.Command.Parameters["@ComandoSelect"].Value.ToString();
            int codigoLista = (int)e.Command.Parameters["@CodigoLista"].Value;
            Session["cl"] = codigoLista;
            CriaCamposLista(codigoLista, comandoSelect);
        }
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        switch (e.Parameter)
        {
            case "finalizarWizard":
                try { dataSourceLista.Insert(); } catch (Exception ex) { ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message; }
                break;
            default:
                break;
        }
    }

    protected void queryBuilder_SaveQuery(object sender, DevExpress.XtraReports.Web.SaveQueryEventArgs e)
    {
        queryBuilder.JSProperties["cpComandoSelect"] = e.SelectStatement;
    }

    #endregion

    #region Methods

    private static DataConnectionParametersBase GetConnectionParameter(string connectionString, string applicationName)
    {
        var conexao = new SqlConnectionStringBuilder(connectionString);
        conexao.ApplicationName = applicationName;
        conexao.UserID = "usr_cdis_report";
        conexao.Password = "123456";

        return new CustomStringConnectionParameters(conexao.ConnectionString);

        /*var parameter = new MsSqlConnectionParameters();
        var conexao = new SqlConnectionStringBuilder(connectionString);
        parameter.AuthorizationType = MsSqlAuthorizationType.SqlServer;
        parameter.DatabaseName = conexao.InitialCatalog;
        parameter.ServerName = conexao.DataSource;
        parameter.UserName = "usr_cdis_report";
        parameter.Password = "123456";
        return parameter;*/
    }

    private void SetConnectionStrings()
    {
        dataSourceLista.ConnectionString = ConnectionString;
        dataSourceSubLista.ConnectionString = ConnectionString;
        dataSourceDashboard.ConnectionString = ConnectionString;
        dataSourceRelatorio.ConnectionString = ConnectionString;
    }

    object GetValue(string fieldName)
    {
        return formLayout.GetNestedControlValueByFieldName(fieldName);
    }

    T GetValue<T>(string fieldName)
    {
        return (T)formLayout.GetNestedControlValueByFieldName(fieldName);
    }

    private void CriaCamposLista(int codigoLista, string comandoSelect)
    {
        StringBuilder comandoSql = new StringBuilder("DECLARE @OrdemCampo INT");
        var camposDisponiveis = ObtemCamposDisponiveisPorLista(comandoSelect);
        foreach (string campo in camposDisponiveis.Keys)
        {
            string tipo = camposDisponiveis[campo];
            string formato;
            if (tipo.Equals("DAT"))
                formato = "{0:d}";
            else if (tipo.Equals("MON"))
                formato = "{0:c2}";
            else
                formato = string.Empty;

            if (campo.Equals("CodigoProjeto", StringComparison.InvariantCultureIgnoreCase))
            {
                #region Comando SQL

                comandoSql.AppendFormat(@"

    SET @OrdemCampo = ISNULL((SELECT MAX(lc.OrdemCampo) FROM ListaCampo AS lc WHERE CodigoLista = {0}), 0) + 1

INSERT INTO [ListaCampo]
           ([CodigoLista]
           ,[NomeCampo]
           ,[TituloCampo]
           ,[OrdemCampo]
           ,[OrdemAgrupamentoCampo]
           ,[TipoCampo]
           ,[IndicaAreaFiltro]
           ,[TipoFiltro]
           ,[IndicaAgrupamento]
           ,[TipoTotalizador]
           ,[IndicaAreaDado]
           ,[IndicaAreaColuna]
           ,[IndicaAreaLinha]
           ,[AreaDefault]
           ,[IndicaCampoVisivel]
           ,[AlinhamentoCampo]
           ,[IndicaCampoHierarquia]
		   ,[IndicaColunaFixa]
           ,[Formato]
           ,[IndicaLink]
           ,[IndicaCampoControle]
           ,[IniciaisCampoControlado])
     VALUES
           ({0}
           ,'{1}'
           ,'{1}'
           , @OrdemCampo
           ,-1
           ,'{2}'
           ,'S'
           ,'E'
           ,'N'
           ,'NENHUM'
           ,'N'
           ,'N'
           ,'N'
           ,'L'
           ,'S'
           ,'E'
           ,'N'
		   ,'N'
           ,'{3}'
		   ,'N'
           ,'S'
           ,'CP')"
                    , codigoLista
                    , campo
                    , tipo
                    , formato);

                #endregion
            }
            else
            {
                #region Comando SQL

                comandoSql.AppendFormat(@"

    SET @OrdemCampo = ISNULL((SELECT MAX(lc.OrdemCampo) FROM ListaCampo AS lc WHERE CodigoLista = {0}), 0) + 1

INSERT INTO [ListaCampo]
           ([CodigoLista]
           ,[NomeCampo]
           ,[TituloCampo]
           ,[OrdemCampo]
           ,[OrdemAgrupamentoCampo]
           ,[TipoCampo]
           ,[IndicaAreaFiltro]
           ,[TipoFiltro]
           ,[IndicaAgrupamento]
           ,[TipoTotalizador]
           ,[IndicaAreaDado]
           ,[IndicaAreaColuna]
           ,[IndicaAreaLinha]
           ,[AreaDefault]
           ,[IndicaCampoVisivel]
           ,[AlinhamentoCampo]
           ,[IndicaCampoHierarquia]
		   ,[IndicaColunaFixa]
           ,[Formato]
           ,[IndicaLink]
           ,[IndicaCampoControle])
     VALUES
           ({0}
           ,'{1}'
           ,'{1}'
           , @OrdemCampo
           ,-1
           ,'{2}'
           ,'S'
           ,'E'
           ,'N'
           ,'NENHUM'
           ,'N'
           ,'N'
           ,'N'
           ,'L'
           ,'S'
           ,'E'
           ,'N'
		   ,'N'
           ,'{3}'
		   ,'N'
		   ,'N')"
                    , codigoLista
                    , campo
                    , tipo
                    , formato);

                #endregion
            }
        }
        string strComandoSql = comandoSql.ToString();
        if (!string.IsNullOrWhiteSpace(strComandoSql))
        {
            int registrosAfetados = 0;
            cDados.execSQL(strComandoSql, ref registrosAfetados);
        }
    }

    private Dictionary<string, string> ObtemCamposDisponiveisPorLista(string comandoOriginal)
    {
        var dicionarioColunaTipo = new Dictionary<string, string>();
        var connectionString = cDados.classeDados.getStringConexao();
        //Remove 'dbname' e 'dbowner'
        comandoOriginal = Regex.Replace(comandoOriginal, @"\{[0-1]\}\.", string.Empty);
        //substitui os demais parâmetros por '1'
        comandoOriginal = Regex.Replace(comandoOriginal, @"\{\d+\}", "1");
        var dataAdapter = new SqlDataAdapter(comandoOriginal, connectionString);

        var selectCmdParameters = dataAdapter.SelectCommand.Parameters;
        var parameterDictionary = DataExtensions.GetSqlCommandParameters(comandoOriginal);
        foreach (var paramKeyValuePair in parameterDictionary)
            selectCmdParameters.AddWithValue(paramKeyValuePair.Key, paramKeyValuePair.Value);

        var dataTable = new DataTable();
        dataAdapter.FillSchema(dataTable, SchemaType.Source);

        foreach (DataColumn col in dataTable.Columns)
        {
            string tipo;
            switch (col.DataType.Name)
            {
                case "String":
                    tipo = "VAR";
                    break;
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                    tipo = "NUM";
                    break;
                case "Decimal":
                case "Single":
                    tipo = "MON";
                    break;
                case "DateTime":
                    tipo = "DAT";
                    break;
                default:
                    tipo = string.Empty;
                    break;
            }
            dicionarioColunaTipo.Add(col.ColumnName, tipo);
        }

        return dicionarioColunaTipo;
    }

    private void PermiteUsuarioEditarComando()
    {
        bool acessoConcedido = false;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoUsuario Int, 
        @CodigoEntidade Int

    SET @CodigoUsuario = {2}
    SET @CodigoEntidade = {0}.{1}.f_GetCodigoEntidadePrimariaSistema();

 SELECT {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, 1, NULL, 'SI', 0, NULL, 'SI_incRel') AS AcessoConcedido;",
cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioLogado);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (ds.Tables[0].Rows.Count > 0)
            acessoConcedido = (bool)ds.Tables[0].Rows[0]["AcessoConcedido"];
        memoComandoSelect.ReadOnly = !acessoConcedido;
        ////Bug 5322: [Configuração de Relatórios Dinâmicos] - Deficiências a corrigir
        //(formLayout.Items[0] as TabbedLayoutGroup).Items[5].ClientVisible = acessoConcedido;
    }

    #endregion

    protected void callbackCompletaAcao_Callback(object source, CallbackEventArgs e)
    {

    }
}