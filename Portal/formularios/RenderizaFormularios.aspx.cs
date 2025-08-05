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
using DevExpress.Web;
using CDIS;
using System.Collections.Specialized;

public partial class formularios_RenderizaFormularios : System.Web.UI.Page
{
    dados cDados;

    int codigoModeloFormulario ;
    int codigoProjeto;
    int codigoUsuarioResponsavel;
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
        if (Request.QueryString["CMF"] != null)
        {
            codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            codigoUsuarioResponsavel = int.Parse(Request.QueryString["US"].ToString()); //int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

            // se tem filtro por formularios
            string filtroCodigosFormularios = "";
            if (Request.QueryString["FF"] != null)
            {
                filtroCodigosFormularios = Request.QueryString["FF"].ToString().Trim();
            }

            ASPxHiddenField hf = new ASPxHiddenField();
            Hashtable parametros = new Hashtable();
            Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, 1, codigoModeloFormulario, new Unit("100%"), new Unit("600px"), false, this.Page, parametros, ref hf, false);
            Control gridFormulario = myForm.constroiInterfaceFormulario(false, IsPostBack, null, codigoProjeto, "", "");
            form1.Controls.Add(gridFormulario);
        }
    }
}
