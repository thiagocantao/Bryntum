using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CdisReport
/// </summary>
public class CdisReport : XtraReport
{
    public CdisReport()
    {

    }

    protected override void OnDataSourceDemanded(EventArgs e)
    {
        dados cDados = CdadosUtil.GetCdados(null);
        string connectionString = cDados.classeDados.getStringConexao();
        var dataSource = DataSource as DevExpress.DataAccess.Sql.SqlDataSource;
        if (dataSource == null)
            return;

        dataSource.ConnectionParameters = ObtemParametroConfiguracaoFonteDados(connectionString);

        base.OnDataSourceDemanded(e);
    }

    private MsSqlConnectionParameters ObtemParametroConfiguracaoFonteDados(string connectionString)
    {
        var connectionParameter = new MsSqlConnectionParameters();
        var builder = new SqlConnectionStringBuilder(connectionString);
        connectionParameter.AuthorizationType = MsSqlAuthorizationType.SqlServer;
        connectionParameter.DatabaseName = builder.InitialCatalog;
        connectionParameter.ServerName = builder.DataSource;
        connectionParameter.Password = builder.Password;
        connectionParameter.UserName = builder.UserID;
        return connectionParameter;
    }
}