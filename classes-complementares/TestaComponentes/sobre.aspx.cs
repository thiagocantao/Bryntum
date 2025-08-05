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
using CDIS;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web;

public partial class sobre : System.Web.UI.Page
{
    dados cDados = new dados();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (1==1||Request.QueryString != null && Request.QueryString["CE"] != null)
        {
            int codigoEmpresa = int.Parse(Request.QueryString["CE"].ToString());
            int codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
            int codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            int codigoUsuarioResponsavel = int.Parse(Request.QueryString["US"].ToString());
            int? codigoFormulario = null;
            if (Request.QueryString["CF"] != null && Request.QueryString["CF"].ToString() != "null")
                codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());

            bool readOnly = false;

            if (Request.QueryString["RO"] != null)
                readOnly = Request.QueryString["RO"].ToString() == "S";


            //fx = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEmpresa, codigoModeloFormulario, new Unit("1000px"), new Unit("500px"), readOnly, this, null);
            //Control ctrl = fx.constroiInterfaceFormulario(true, IsPostBack, codigoFormulario, codigoProjeto, "", "");

            //if (ctrl != null)
            //    form1.Controls.Add(ctrl);

            Control pnExterno = form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario"); // pnFormulario -> pcFormulario ->tabPageToDoList
            ASPxPageControl pcFormulario = (ASPxPageControl)form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario");
            Control gv = pcFormulario.TabPages.FindByName("tabPageToDoList").FindControl("gvToDoList"); ;
            //form1.FindControl("pcFormulario");
        }
    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse();
    }
}
