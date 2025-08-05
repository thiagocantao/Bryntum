using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDIS;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using System.Collections.Specialized;

public class DataBaseEditaleDashboardStorage : IDashboardStorage
{
    private readonly string _dashboardId;
    dados cDados;

    public DataBaseEditaleDashboardStorage(string dashboardId)
    {
        cDados = new dados(null);
        _dashboardId = dashboardId;
    }

    public IEnumerable<DashboardInfo> GetAvailableDashboardsInfo()
    {
        List<DashboardInfo> infos = new List<DashboardInfo>();
        var sql = string.Format(@"SELECT [IDDashboard], [TituloDashboard] FROM [dbo].[Dashboard] where IDDashboard = '{0}'", _dashboardId);
        DataSet ds = cDados.getDataSet(sql);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            DashboardInfo dsi = new DashboardInfo();
            dsi.ID = row.Field<Guid>("IDDashboard").ToString();
            dsi.Name = row.Field<string>("TituloDashboard");
            infos.Add(dsi);
        }
        return infos;
    }

    public XDocument LoadDashboard(string dashboardID)
    {
        string dashboard;
        var sql = string.Format(@"SELECT [XMLDashboard], [TituloDashboard] FROM [dbo].[Dashboard] WHERE [IDDashboard] = '{0}'", dashboardID);
        DataSet ds = cDados.getDataSet(sql);
        var row = ds.Tables[0].AsEnumerable().FirstOrDefault();
        if (row == null || row.IsNull("XMLDashboard"))
            dashboard = string.Format(@"<Dashboard><Title Text=""{0}"" /></Dashboard> ", row["TituloDashboard"]);
        else
            dashboard = row.Field<string>("XMLDashboard");

        return XDocument.Parse(dashboard);
    }

    public void SaveDashboard(string dashboardID, XDocument document)
    {
        var dashboard = document.ToString();
        var sql = string.Format(@"
UPDATE [dbo].[Dashboard]
   SET [XMLDashboard] = '{0}'
 WHERE [IDDashboard] = '{1}'", dashboard.Replace("'", "''"), dashboardID);
        int qtd = 0;
        cDados.execSQL(sql, ref qtd);
    }
}

public partial class Relatorios_GeradorDashboard_frame_EditorDashboard : BasePageBrisk
{
    private string _ConnectionString;
    public Guid guidDashboard;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = CDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool retorno = Guid.TryParse(Request.QueryString["id"] + "", out guidDashboard);

        dashboardDesigner.SetConnectionStringsProvider(new ConfigFileConnectionStringsProvider());
        DashboardConfigurator.PassCredentials = true;
        DataBaseEditaleDashboardStorage dashboardStorage = new DataBaseEditaleDashboardStorage(guidDashboard.ToString());
        dashboardDesigner.SetDashboardStorage(dashboardStorage);
        dashboardDesigner.EnableCustomSql = true;

        dashboardDesigner.Height = new Unit((TelaAltura - 125) + "px");
        CDados.aplicaEstiloVisual(btnAlternar);

    }


    //    protected void ASPxDashboard1_DashboardLoading(object sender, DashboardLoadingWebEventArgs e)
    //    {
    //        string comandoSql = string.Format(@"
    //DECLARE @IDDashboard uniqueidentifier

    //    SET @IDDashboard = '{0}'

    // SELECT XMLDashboard, TituloDashboard
    //   FROM Dashboard
    //  WHERE IDDashboard = @IDDashboard", guidDashboard);
    //        DataSet ds = CDados.getDataSet(comandoSql);
    //        string XmlDashboard = (ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0]["XMLDashboard"].ToString() : string.Empty;
    //        if (ds.Tables[0].Rows[0]["XMLDashboard"].ToString() == "")
    //        {
    //            XmlDashboard = string.Format(@"
    //            <Dashboard>
    //                 <Title Text=""{0}"" />
    //           </Dashboard> ", ds.Tables[0].Rows[0]["TituloDashboard"]);
    //        }


    //        e.DashboardXml = XDocument.Parse(XmlDashboard);
    //    }

    protected void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e)
    {
        if (e.ConnectionParameters is CustomStringConnectionParameters)
        {
            var paramCustomStringConnection = e.ConnectionParameters as CustomStringConnectionParameters;
            paramCustomStringConnection.ConnectionString = ConnectionString;
        }
        else if (e.ConnectionParameters is MsSqlConnectionParameters)
        {
            var paramMsSql = e.ConnectionParameters as MsSqlConnectionParameters;
            var conexao = new SqlConnectionStringBuilder(ConnectionString);
            paramMsSql.AuthorizationType = MsSqlAuthorizationType.SqlServer;
            paramMsSql.DatabaseName = conexao.InitialCatalog;
            paramMsSql.ServerName = conexao.DataSource;
            paramMsSql.Password = conexao.Password;
            paramMsSql.UserName = conexao.UserID;
        }
    }

    //protected void dashboardDesigner_DashboardSaving(object sender, DashboardSavingWebEventArgs e)
    //{
    //    string comandoSQL = string.Format(" UPDATE Dashboard SET XMldashboard = '{0}' WHERE IDDashboard = '{1}' ", e.DashboardXml.ToString().Replace("'", "''"), guidDashboard);

    //    int regAfetados = 0;
    //    CDados.execSQL(comandoSQL, ref regAfetados);

    //}
}
