using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Bootstrap : System.Web.UI.MasterPage
{
    public string baseUrl;

    public string idioma;

    public bool exibeMenu = true;

    public string logo = @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8/5+hHgAHggJ/PchI7wAAAABJRU5ErkJggg=="; // One pixel transparent PNG

    dados cDados;

    private int codigoUsuario;
    private int codigoEntidade;

    protected void Page_Init(object sender, EventArgs e)
    {




        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        idioma = cultureInfo.ToString();

        try
        {
            cDados = CdadosUtil.GetCdados(null);
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
            {
                Response.Redirect(baseUrl + "/login.aspx");
                Response.End();
            }
        }
        catch
        {
            Response.Redirect(baseUrl + "/login.aspx");
            Response.End();
        }

        this.TH(this.TS("geral", "menu"));

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if ((!IsPostBack) && (!Page.IsCallback))
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        Page.Title = "Brisk PPM";

        formularioMaster.Action = Request.RawUrl;
        bool indicaHttpsViaProxyReverso = false;
        DataSet dsParam1 = cDados.getParametrosSistema("httpsViaProxyReverso");
        if (cDados.DataSetOk(dsParam1) && cDados.DataTableOk(dsParam1.Tables[0]))
        {
            indicaHttpsViaProxyReverso = dsParam1.Tables[0].Rows[0]["httpsViaProxyReverso"].ToString().ToUpper() == "S";
        }

        if (indicaHttpsViaProxyReverso == true)
        {
            baseUrl = "https://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
        }
        else
        {

            bool indicaHttps = false;
            DataSet dsParam = cDados.getParametrosSistema("utilizaProtocoloHttps");
            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                indicaHttps = dsParam.Tables[0].Rows[0]["utilizaProtocoloHttps"].ToString().ToUpper() == "S";
            }

            /* Experimento Bootstrap */
            if (indicaHttps == true)
            {
                baseUrl = "https://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
            else
            {
                baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
        }
        CabecalhoMaster();
    }

    protected string Html(string html)
    {
        return (HttpUtility.HtmlEncode(html));
    }

    private void CabecalhoMaster()
    {

        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/css/bootstrap.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/fontawesome/v5.0.12/css/fontawesome-all.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        if (exibeMenu)
        {
            cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/menu.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        }
        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/style.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));

        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js""></script>", baseUrl)));
        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/js/bootstrap.bundle.min.js""></script>", baseUrl)));
        if (exibeMenu)
        {
            cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/menu.js""></script>", baseUrl)));
        }
        cabecalhoMaster.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/script.js""></script>", baseUrl)));
    }
}
