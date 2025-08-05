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

public partial class wfRenderizaFormulario_Edicao : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    string resolucaoCliente;
    private int larguraTabelaWf;


    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados();//listaParametrosDados);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            //Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        

        int codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());

        int? codigoFormulario = null;
        if (Request.QueryString["CF"] != null)
            codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());

        int? codigoProjeto = null;
        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());


        larguraTabelaWf = getLarguraTela() - 30;
        if (larguraTabelaWf > 980)
            larguraTabelaWf = 980;
        string alturaFormulario = "800px";
        if (Request.QueryString["AT"] != null)
            alturaFormulario = Request.QueryString["AT"] + "px";

        if (Request.QueryString["LT"] != null)
            larguraTabelaWf = int.Parse(Request.QueryString["LT"]);

        bool readOnly = false;

        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";

        Hashtable parametros = new Hashtable();
        if (Request.QueryString["ppp"] != null)
        {
            parametros.Add("FormMaster", Request.QueryString["FormMaster"]);
            parametros.Add("ppp", true);
        }

        Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoModeloFormulario, new Unit(larguraTabelaWf + "px"), new Unit(alturaFormulario), readOnly, this, parametros, ref hfSessao,false);
        Control ctrl = myForm.constroiInterfaceFormulario(true, IsPostBack, codigoFormulario, codigoProjeto, "", "");
        form1.Controls.Add(ctrl);

        if (!IsPostBack && !IsCallback)
        {
           // cDados.aplicaEstiloVisual(Page);
            // tradução dos componentes da página
            new traducao("pt-BR", (ASPxWebControl)FindControl("gvGridPrincipal"), gridToDoList());
        }
        popupCampo.Width = new Unit("970px");
        popupCampo.Height = new Unit("550px");
    }

    private int getLarguraTela()
    {
        return int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
    }

    private ASPxGridView gridToDoList()
    {
        if (form1.FindControl("pnExterno") == null)
            return null;

        if (form1.FindControl("pnExterno").FindControl("pnFormulario") == null)
            return null;

        if (form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario") == null)
            return null;

        ASPxPageControl pcFormulario = (ASPxPageControl)form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario");

        TabPage pgToDoList = pcFormulario.TabPages.FindByName("tabPageToDoList");

        if( pgToDoList != null)
            return (ASPxGridView)pgToDoList.FindControl("gvToDoList");
        else
            return null;

    }
}
