using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Web;
using System;
using System.Data.SqlClient;

public partial class _Processos_Visualizacao_QueryBuilder : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var connectionString = cDados.classeDados.getStringConexao();
            var parameter = GetConnectionParameter(connectionString);
            queryBuilder.ValidateQueryByExecution = true;
            queryBuilder.OpenConnection(parameter);
        }
    }

    private static MsSqlConnectionParameters GetConnectionParameter(string connectionString)
    {
        var parameter = new MsSqlConnectionParameters();
        var conexao = new SqlConnectionStringBuilder(connectionString);
        parameter.AuthorizationType = MsSqlAuthorizationType.SqlServer;
        parameter.DatabaseName = conexao.InitialCatalog;
        parameter.ServerName = conexao.DataSource;
        parameter.UserName = "usr_cdis_report";
        parameter.Password = "123456";
        return parameter;
    }

    protected void queryBuilder_SaveQuery(object sender, DevExpress.XtraReports.Web.SaveQueryEventArgs e)
    {
        //Session["ComandoSelect"] = e.SelectStatement;
        //ASPxWebControl.RedirectOnCallback("~/_Processos/Visualizacao/WizardDefinicaoLista.aspx");

        queryBuilder.JSProperties["cpTeste"] = e.SelectStatement;
    }
}