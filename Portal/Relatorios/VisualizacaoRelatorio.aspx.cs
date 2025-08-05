using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatorios_VisualizacaoRelatorio : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoCarteira;
    private int codigoEntidade;

    public string IdRelatorio
    {
        get
        {
            return Request.QueryString["id"];
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

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

        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadReport();
    }

    private void LoadReport()
    {
        XtraReport report = GetReport();
        documentViewer.OpenReport(report);
    }

    private XtraReport GetReport()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = @"  
 SELECT ISNULL(ru.ConteudoRelatorio, mr.ConteudoRelatorio) AS ConteudoRelatorio
   FROM ModeloRelatorio AS mr LEFT JOIN
        RelatorioUsuario AS ru ON ru.IDRelatorio = mr.IDRelatorio AND ru.CodigoUsuario = @CodigoUsuario
  WHERE mr.IDRelatorio = @IDRelatorio";

        #endregion

        byte[] reportLayout;
        using (var connection = new SqlConnection(cDados.ConnectionString))
        {
            var command = new SqlCommand(comandoSql, connection);
            command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("IDRelatorio", IdRelatorio),
                new SqlParameter("CodigoUsuario", codigoUsuarioLogado)
            });
            connection.Open();
            reportLayout = command.ExecuteScalar() as byte[];
        }

        if (reportLayout == null)
            return new XtraReport();

        using (var stream = new MemoryStream(reportLayout))
        {
            var report = XtraReport.FromStream(stream, true);
            SetParameters(report, Request.QueryString);
            SetConnectionParameters(report);
            report.Parameters["pUrlLogo"].Value = ObtemUrlLogo();
            return report;
        }
    }

    private static void SetConnectionParameters(XtraReport report)
    {
        var dados = new dados(null);
        var parameters = ((DevExpress.DataAccess.Sql.SqlDataSource)report.DataSource).ConnectionParameters;

        if (parameters is CustomStringConnectionParameters)
        {
            var paramCustomStringConnection = parameters as CustomStringConnectionParameters;
            paramCustomStringConnection.ConnectionString = dados.ConnectionString;
        }
        else if (parameters is MsSqlConnectionParameters)
        {
            var paramMsSql = parameters as MsSqlConnectionParameters;
            var conexao = new SqlConnectionStringBuilder(dados.ConnectionString);
            paramMsSql.AuthorizationType = MsSqlAuthorizationType.SqlServer;
            paramMsSql.DatabaseName = conexao.InitialCatalog;
            paramMsSql.ServerName = conexao.DataSource;
            paramMsSql.Password = conexao.Password;
            paramMsSql.UserName = conexao.UserID;
        }
    }

    private static string ObtemUrlLogo()
    {
        var dados = new dados(null);
        var imgLogo = dados.ObtemLogoEntidade();
        var caminhoFisicoArquivo = Path.Combine(
            HostingEnvironment.ApplicationPhysicalPath, "ArquivosTemporarios",
            Path.ChangeExtension(Path.GetRandomFileName(), "bmp"));

        imgLogo.Save(caminhoFisicoArquivo);

        return caminhoFisicoArquivo;
    }

    private static void SetParameters(XtraReport report, NameValueCollection queryString)
    {
        foreach (var parameter in report.Parameters)
        {
            string strValue = queryString[parameter.Name];
            parameter.Value = GetParameterValue(strValue, parameter.Type);
        }
        //var paramLogo = report.Parameters["pUrlLogo"];
        //if (paramLogo != null)
        //    paramLogo.Value = ObtemUrlLogoEntidade();
    }

    private static object GetParameterValue(string strValue, Type paramType)
    {
        if (string.IsNullOrEmpty(strValue)) return null;

        switch (paramType.Name)
        {
            case "String": return strValue;
            case "DateTime": return DateTime.Parse(strValue);
            case "Int32": return int.Parse(strValue);
            case "Decimal": return decimal.Parse(strValue);
            default: return Convert.ChangeType(strValue, paramType);
        }
    }
}