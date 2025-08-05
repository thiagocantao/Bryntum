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
using DevExpress.Web;

public partial class formularios_popUpEditaFormulario : System.Web.UI.Page
{
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        int codigoEmpresa = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
        int codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());
        int codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        int codigoUsuarioResponsavel = int.Parse(Request.QueryString["US"].ToString());

        ASPxHiddenField hf = new ASPxHiddenField();

        Hashtable parametros = new Hashtable();
        Formulario xx = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEmpresa, codigoModeloFormulario, new Unit("100%"), new Unit("600px"), false, this.Page, parametros,ref hf,false);
        Control xc = xx.constroiInterfaceFormulario(true, IsPostBack, codigoFormulario, codigoProjeto, "", "");
            
        pnExterno.Controls.Add(xc);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["_ClosePopUp_"] != null)
        {
            this.ClientScript.RegisterClientScriptBlock(GetType(), "close",
                        @"<script type='text/javascript'>
                            window.returnValue = 'OK';
                            window.close()
                        
                         </script>");

            Session.Remove("_CodigoFormularioMaster_");
            Session.Remove("_CodigoToDoList_");
            Session.Remove("_ClosePopUp_");
        } 
    }
}
