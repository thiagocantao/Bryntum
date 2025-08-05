using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class espacoTrabalho_DownloadArquivo : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        dados cDados = CdadosUtil.GetCdados(null);
        cDados.download(int.Parse(Request.QueryString["CA"].ToString()), null, Page, Response, Request, true);
    }
}