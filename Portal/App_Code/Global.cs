using DevExpress.DashboardCommon;
using DevExpress.Web;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Routing;

[assembly: AssemblyVersion("1.0.*")]
/// <summary>
/// Summary description for Global
/// </summary>
public class Global : HttpApplication
{
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        ASPxWebControl.CallbackError += ASPxWebControl_CallbackError;
        if (IndicaAmbienteAzure)
            ConfigureAzureSettings();

        DefineConfiguracoesSegurancaDevExpress();
    }

    private void ASPxWebControl_CallbackError(object sender, EventArgs e)
    {
        var currentContext = CurrentHttpContext;
        if (currentContext != null)
        {
            LogException();
            if (!(CurrentHttpContext.Application["exception"] is DashboardDataLoadingException))
                ASPxWebControl.RedirectOnCallback("~/erros/erro.aspx");
        }
    }

    private void DefineConfiguracoesSegurancaDevExpress()
    {
        DevExpress.XtraReports.Security.ScriptPermissionManager.GlobalInstance = new DevExpress.XtraReports.Security.ScriptPermissionManager(DevExpress.XtraReports.Security.ExecutionMode.Unrestricted);
        DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.EnableCustomSql();
        DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterSqlDataSourceWizardCustomizationService<CustomSqlDataSourceWizardCustomizationService>();
        DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
        ASPxWebControl.BackwardCompatibility.DataControlAllowReadUnlistedFieldsFromClientApiDefaultValue = true;
        ASPxWebControl.BackwardCompatibility.ListEditEnableSynchronizationDefaultValue = true;
    }

    private void ConfigureAzureSettings()
    {
        DevExpress.Utils.AzureCompatibility.Enable = true;
    }

    private bool IndicaAmbienteAzure
    {
        get
        {
            const string key = "indicaAmbienteAzure";
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                bool indicaAmbienteAzure;
                var value = ConfigurationManager.AppSettings[key];
                if (bool.TryParse(value, out indicaAmbienteAzure))
                    return indicaAmbienteAzure;
            }

            return false;
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        LogException();
    }

    private static HttpContext CurrentHttpContext
    {
        get
        {
            return HttpContext.Current;
        }
    }

    private static void LogException()
    {
        var currentContext = CurrentHttpContext;
        if (currentContext != null)
        {
            Exception exception = currentContext.Server.GetLastError();
            if (exception is HttpUnhandledException)
                exception = exception.InnerException;

            CurrentHttpContext.Application["exception"] = exception;//DashboardDataLoadingException

            AddToLog(exception.Message, exception.StackTrace);
        }
    }

    private static void AddToLog(string message, string stackTrace)
    {
        var sb = new StringBuilder();
        sb.AppendLine(DateTime.Now.ToLocalTime().ToString());
        sb.AppendLine(message);
        sb.AppendLine();
        sb.AppendLine("Source File: " + CurrentHttpContext.Request.RawUrl);
        sb.AppendLine();
        sb.AppendLine("Stack Trace: ");
        sb.AppendLine(stackTrace);
        for (int i = 0; i < 150; i++)
            sb.Append("-");
        sb.AppendLine();
        CurrentHttpContext.Application["Log"] = sb.ToString();
        sb.AppendLine();
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        AvisaUsuarioSessaoExpirada();
    }

    void Application_BeginRequest(Object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.Ignore("{resource}.ashx/{*pathInfo}");
    }

    public static Version Version
    {
        get
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }

    private void AvisaUsuarioSessaoExpirada()
    {
        try
        {
            if (Session["urlWSnewBriskBase"] != null)
            {
                const string client_secret = "3MHua!6Xtc4pDE_";
                string urlWsNbr = Session["urlWSnewBriskBase"].ToString();
                string url_origem = Session["baseUrl"].ToString();
                string access_token = Session["TokenAcessoNewBrisk"].ToString();
                var requestUrl = urlWsNbr + "/api/v1/avisar-usuario-sessao-expirada";
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);

                request.Method = "POST";
                request.Accept = "application/json";
                request.Headers.Add("cod-entidade-contexto", "0");
                request.ContentType = "application/json-patch+json";

                var requestData = new
                {
                    client_secret = client_secret,
                    access_token = access_token,
                    url_origem = url_origem
                };

                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonRequest);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
            }
        }
        catch (Exception)
        {
            // ignorando qualquer exceção já que avisar o usuário do fim da sessão é um processo secundário
        }
    }
}