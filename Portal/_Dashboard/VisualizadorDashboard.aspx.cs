using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Xml;
using System.Xml.Linq;

public partial class _Dashboard_VisualizadorDashboard : System.Web.UI.Page
{
    private dados cDados;

    #region Properties

    public Guid DashboardId
    {
        get
        {
            return Guid.Parse(Request.QueryString["id"]);
        }
    }

    public string Origem
    {
        get
        {
            return Request.QueryString["origem"];
        }
    }

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
    }

    private Dictionary<string, object> _DicionarioParametros;
    public Dictionary<string, object> DicionarioParametros
    {
        get
        {
            if (_DicionarioParametros == null)
                _DicionarioParametros = new Dictionary<string, object>();
            return _DicionarioParametros;
        }
    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        CDIS_DashboardLocalizer.Activate();
        cDados = CdadosUtil.GetCdados(null);
        ConfiguraParametros();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dashboardViewer.DashboardId = "Dashboard";
        CDIS_DashboardLocalizer.Activate();
        dashboardViewer.PdfExportOptions.PaperKind = System.Drawing.Printing.PaperKind.A4;
        dashboardViewer.PdfExportOptions.PageLayout = DevExpress.DashboardCommon.DashboardExportPageLayout.Landscape;
        dashboardViewer.PdfExportOptions.DocumentScaleMode = DevExpress.DashboardCommon.DashboardExportDocumentScaleMode.AutoFitToPagesWidth;
        if (!IsCallback)
        {
            ResetCache();
        }
    }

   
    protected void dashboardViewer_CustomParameters(object sender, CustomParametersWebEventArgs e)
    {
        e.Parameters.Add(new DevExpress.DashboardCommon.DashboardParameter("guid", typeof(Guid), UniqueCacheParam));
        foreach (var p in e.Parameters)
        {
            if (DicionarioParametros.ContainsKey(p.Name))
                p.Value = DicionarioParametros[p.Name];
        }
    }

    private void ConfiguraParametros()
    {
        var qs = Request.QueryString;
        DicionarioParametros.Clear();
        DicionarioParametros.Add("pa_CodigoRisco", qs["CodRisco"]);
        DicionarioParametros.Add("pa_CodigoQuestao", qs["CodQuestao"]);
        DicionarioParametros.Add("pa_CodigoUnidade", qs["CodUnidade"]);
        DicionarioParametros.Add("pa_CodigoUsuarioSistema", qs["CodUsuario"]);
        DicionarioParametros.Add("pa_CodigoProjeto", qs["CodProjeto"]);
        DicionarioParametros.Add("pa_CodigoReuniao", qs["CodReuniao"]);
        DicionarioParametros.Add("pa_CodigoCarteira", qs["CodCarteira"]);
        DicionarioParametros.Add("pa_CodigoEntidadeContexto", qs["CodEntidade"]);
        DicionarioParametros.Add("pa_CodigoIndicador", qs["CodIndicador"]);
        DicionarioParametros.Add("pa_CodigoPlanoAcao", qs["CodPlanoAcao"]);
        DicionarioParametros.Add("pa_CodigoMapaEstrategico", qs["CodMapaEstrategico"]);
        DicionarioParametros.Add("pa_CodigoObjetoEstrategia", qs["CodObjetoEstrategia"]);
        DicionarioParametros.Add("pa_CodigoPlano", qs["CodPlano"]);
    }

    protected void dashboardViewer_DashboardLoading1(object sender, DashboardLoadingWebEventArgs e)
    {
        string comandoSql = string.Format(@"
DECLARE @IDDashboard uniqueidentifier
    
    SET @IDDashboard = '{0}'
  
 SELECT XMLDashboard
   FROM Dashboard
  WHERE IDDashboard = @IDDashboard", DashboardId);
        DataSet ds = cDados.getDataSet(comandoSql);
        DataRowCollection rows = ds.Tables[0].Rows;
        string XmlDashboard = rows.Count > 0 ? rows[0]["XMLDashboard"] as string : string.Empty;
        if (string.IsNullOrWhiteSpace(XmlDashboard))
        {
            var title = rows.Count > 0 ? rows[0]["TituloDashboard"] as string : string.Empty;
            XmlDashboard = string.Format(@"
            <Dashboard>
                 <Title Text=""{0}"" />
           </Dashboard> ", title);
        }
        e.DashboardXml = XDocument.Parse(XmlDashboard);
        ConfiguraCulturaMoeda(e.DashboardXml);
    }

    private void ConfiguraCulturaMoeda(XDocument dashboardXml)
    {
        XAttribute atr = dashboardXml.Root.Attribute("CurrencyCulture");
        // se o XML do dashboard tiver um atributo CurrencyCulture, esse será mantido
        if (null != atr)
        {
            return;
        }

        object oCodigoEntidade;
        int codigoEntidade = -1;
        string monetaryLocale = "";

        if (DicionarioParametros.TryGetValue("pa_CodigoEntidadeContexto", out oCodigoEntidade) == true)
        {
            if (!int.TryParse(oCodigoEntidade != null ? oCodigoEntidade.ToString() : "", out codigoEntidade))
            {
                codigoEntidade = -1;
            }
        }

        DataSet dsMonetaryLocale;

        if (codigoEntidade > -1)
        {
            dsMonetaryLocale = cDados.getParametrosSistema(codigoEntidade, "monetaryLocale");
        }
        else
        {
            dsMonetaryLocale = cDados.getParametrosSistema("monetaryLocale");
        }

        if (dsMonetaryLocale.Tables.Count > 0)
        {
            DataTable dt = dsMonetaryLocale.Tables[0];
            monetaryLocale = (dt.Rows[0]["monetaryLocale"] + "" == "") ? "" : dt.Rows[0]["monetaryLocale"].ToString();
        }
        if (!string.IsNullOrEmpty(monetaryLocale))
        {
            dashboardXml.Document.Root.SetAttributeValue("CurrencyCulture", monetaryLocale);
        }
    }
    

    protected void dashboardViewer_ConfigureDataConnection1(object sender, ConfigureDataConnectionWebEventArgs e)
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

    public static void ResetCache()
    {
        if (HttpContext.Current.Session != null)
            HttpContext.Current.Session["UniqueCacheParam"] = Guid.NewGuid();
    }

    public static Guid UniqueCacheParam
    {
        get
        {
            if (HttpContext.Current.Session == null)
                return Guid.Empty;
            else
            {
                if (HttpContext.Current.Session["UniqueCacheParam"] == null)
                    ResetCache();
                return (Guid)HttpContext.Current.Session["UniqueCacheParam"];
            }
        }
    }
}