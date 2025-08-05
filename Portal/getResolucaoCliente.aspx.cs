using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class getResolucaoCliente : System.Web.UI.Page
{
    public string baseUrl;
    dados cDados;

    public string urlMobile = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
        Session["baseUrl"] = baseUrl;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        //DataSet ds = cDados.getParametrosSistema("urlMobile");
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString()))
        //    {
        //        if (ds.Tables[0].Rows[0][0].ToString().ToLower().Trim() != "")
        //        {
        //            urlMobile = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
        //        }
        //    }
        //}

        if (ConfigurationManager.AppSettings.AllKeys.Contains("urlMobile"))
        {
            urlMobile = ConfigurationManager.AppSettings["urlMobile"].Trim();
        }
        ASPxHiddenField1.Set("params", Request.QueryString + "");

        if (Request.QueryString["action"] != null)
        {
            cDados.setInfoSistema("ResolucaoCliente", Request.QueryString["res"].ToString());
            //Gambis para contornar problema do IE 8 de PBH
            cDados.setInfoSistema("ResolucaoCliente2", Request.QueryString["res"].ToString());

            Response.Redirect("index.aspx?PassouResolucao=S" + "&" + Request.QueryString);
        }
    }
}
