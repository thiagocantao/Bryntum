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

public partial class wfRenderizaFormulario_Impressao : System.Web.UI.Page
{
    private int codigoFormulario;
    private int codigoProjeto;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CF"] != null && Request.QueryString["CF"] != "")
            codigoFormulario = int.Parse(Request.QueryString["CF"]);

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"] != "")
            codigoProjeto = int.Parse(Request.QueryString["CP"]);

        if (codigoFormulario > 0)
        {
            rel_ImpressaoFormularios rel = new rel_ImpressaoFormularios(codigoFormulario);
            ReportViewer1.Report = rel;
        }
    }
}
