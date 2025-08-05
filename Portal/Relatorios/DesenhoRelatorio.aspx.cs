using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using DevExpress.DataAccess.ConnectionParameters;
using System.Collections.Specialized;
using DevExpress.XtraReports.Web;
using System.Drawing.Printing;

public partial class Relatorios_DesenhoRelatorio : Page
{
    private string IdRelatorio
    {
        get
        {
            return Request.QueryString["id"];
        }
    }

    private bool IndicaCostumizacaoUsuario
    {
        get
        {
            var qsCustom = Request.QueryString["custom"];
            if (string.IsNullOrEmpty(qsCustom))
                return false;

            return qsCustom.ToLower().Equals("s");
        }
    }

    int codigoUsuarioLogado;

    private dados cDados;

    private string ConnectionString
    {
        get
        {
            return cDados.classeDados.getStringConexao();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
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
        //cDados.aplicaEstiloVisual(this);
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XtraReport report = ObtemRelatorio();
            var sqlDataSource = report.DataSource as DevExpress.DataAccess.Sql.SqlDataSource;
            if (sqlDataSource != null)
                sqlDataSource.ConnectionParameters = ObtemParametroConfiguracaoFonteDados();
            reportDesigner.OpenReport(report);
        }
    }

    private XtraReport ObtemRelatorio()
    {
        return ObtemRelatorio<XtraReport>();
    }

    private T ObtemRelatorio<T>() where T : XtraReport
    {
        var reportLayout = IndicaCostumizacaoUsuario ?
            ObtemLayoutRelatorioCustomizadoUsuario() :
            ObtemLayoutRelatorio();

        if (reportLayout == null)
        {
            var reportType = typeof(T);
            var reportAssembly = reportType.Assembly;
            var report = reportAssembly.CreateInstance(reportType.FullName) as T;
            ConfiguraFonteDadosRelatorio(report);
            ConfiguraRelatorio(report);
            return report;
        }

        using (var stream = new MemoryStream(reportLayout))
            return (T)XtraReport.FromStream(stream, true);
    }

    private void ConfiguraRelatorio(XtraReport report)
    {
        report.PaperKind = PaperKind.A4;
    }

    private void ConfiguraFonteDadosRelatorio(XtraReport report)
    {
        var dataSource = new DevExpress.DataAccess.Sql.SqlDataSource();
        dataSource.ConnectionParameters = ObtemParametroConfiguracaoFonteDados();
        dataSource.ConnectionName = "DefaultConnection";
        dataSource.Name = "Fonte de Dados";

        report.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { dataSource });
        report.DataSource = dataSource;
    }

    private MsSqlConnectionParameters ObtemParametroConfiguracaoFonteDados()
    {
        var connectionParameter = new MsSqlConnectionParameters();
        var builder = new SqlConnectionStringBuilder(ConnectionString);
        connectionParameter.AuthorizationType = MsSqlAuthorizationType.SqlServer;
        connectionParameter.DatabaseName = builder.InitialCatalog;
        connectionParameter.ServerName = builder.DataSource;
        connectionParameter.Password = builder.Password;
        connectionParameter.UserName = builder.UserID;
        return connectionParameter;
    }

    private byte[] ObtemLayoutRelatorioCustomizadoUsuario()
    {
        byte[] conteudoLayoutRelatorio;

        string sqlCommand = @" 
 SELECT ISNULL(ru.ConteudoRelatorio, mr.ConteudoRelatorio) AS ConteudoRelatorio
   FROM ModeloRelatorio AS mr LEFT JOIN
        RelatorioUsuario AS ru ON ru.IDRelatorio = mr.IDRelatorio AND ru.CodigoUsuario = @CodigoUsuario
  WHERE mr.IDRelatorio = @IDRelatorio";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            cmd.Parameters.Add(new SqlParameter("@IDRelatorio", IdRelatorio));
            cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", codigoUsuarioLogado));
            conteudoLayoutRelatorio = cmd.ExecuteScalar() as byte[];
        }

        return conteudoLayoutRelatorio;
    }

    private byte[] ObtemLayoutRelatorio()
    {
        byte[] conteudoLayoutRelatorio;

        string sqlCommand = "SELECT ConteudoRelatorio FROM ModeloRelatorio WHERE IDRelatorio = @IDRelatorio";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.Parameters.Add(new SqlParameter("@IDRelatorio", IdRelatorio));
            conteudoLayoutRelatorio = cmd.ExecuteScalar() as byte[];
        }

        return conteudoLayoutRelatorio;
    }

    protected void reportDesigner_SaveReportLayout(object sender, SaveReportLayoutEventArgs e)
    {
        var reportLayout = e.ReportLayout;
        if (IndicaCostumizacaoUsuario)
            SalvaLayoutRelatorioCustomizadoUsuario(reportLayout);
        else
            SalvaLayoutRelatorio(reportLayout);
    }

    private int SalvaLayoutRelatorio(byte[] reportLayout)
    {
        int result;
        string sqlCommand = "UPDATE ModeloRelatorio SET ConteudoRelatorio = @ConteudoRelatorio WHERE IDRelatorio = @IDRelatorio";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            var pId = new SqlParameter("@IDRelatorio", IdRelatorio);
            var pConteudoRelatorio = new SqlParameter("@ConteudoRelatorio", reportLayout);
            cmd.Parameters.AddRange(new SqlParameter[] { pId, pConteudoRelatorio });
            cmd.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            result = cmd.ExecuteNonQuery();
        }
        return result;
    }

    private int SalvaLayoutRelatorioCustomizadoUsuario(byte[] reportLayout)
    {
        int result;
        string sqlCommand = @"
IF EXISTS (SELECT 1 FROM RelatorioUsuario WHERE IDRelatorio = @IDRelatorio AND CodigoUsuario = @CodigoUsuario)
BEGIN
UPDATE RelatorioUsuario
   SET ConteudoRelatorio = @ConteudoRelatorio
 WHERE IDRelatorio = @IDRelatorio
   AND CodigoUsuario = @CodigoUsuario
END
ELSE
BEGIN
INSERT INTO RelatorioUsuario
           (IDRelatorio
           ,CodigoUsuario
           ,ConteudoRelatorio)
     VALUES
           (@IDRelatorio
           ,@CodigoUsuario
           ,@ConteudoRelatorio)
END";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            var pId = new SqlParameter("@IDRelatorio", IdRelatorio);
            var pCodigoUsuario = new SqlParameter("@CodigoUsuario", codigoUsuarioLogado);
            var pConteudoRelatorio = new SqlParameter("@ConteudoRelatorio", reportLayout);
            cmd.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            cmd.Parameters.AddRange(new SqlParameter[] { pId, pConteudoRelatorio, pCodigoUsuario });
            result = cmd.ExecuteNonQuery();
        }
        return result;
    }
}