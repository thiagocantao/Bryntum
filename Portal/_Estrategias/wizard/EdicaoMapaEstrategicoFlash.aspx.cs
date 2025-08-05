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
using System.Collections.Specialized;

public partial class _Estrategias_wizard_EdicaoMapaEstrategicoFlash : System.Web.UI.Page
{
    public int alturaSwf;
    dados cDados;
    private int alturaPrincipal = 0;

    private string bancodb;
    private string Ownerdb;
    private string resolucaoCliente = "";
    public string webServicePath = "";  // caminho do web service
    public string doubleClipTarea = ""; // link del popup.
    public string codigoMapa = "";
    public string nomeMapa = "";
    public string codigoUsuario = "";
    public string codigoEntidade = "";
    public string alturaObject = "";
    public string alturaFlasEdicao = "";
    public string largoFlasEdicao = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Ownerdb = cDados.getDbOwner();
        bancodb = cDados.getDbName();

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }


        if (Request.QueryString["IDMapa"] != null && Request.QueryString["IDMapa"].ToString() != "")
        {
            codigoMapa = Request.QueryString["IDMapa"].ToString();
        }
        if (Request.QueryString["CU"] != null && Request.QueryString["CU"].ToString() != "")
        {
            codigoUsuario = Request.QueryString["CU"].ToString();
        }
        if (Request.QueryString["CE"] != null && Request.QueryString["CE"].ToString() != "")
        {
            codigoEntidade = Request.QueryString["CE"].ToString();
        }
        if (Request.QueryString["NMP"] != null && Request.QueryString["NMP"].ToString() != "")
        {
            nomeMapa = Request.QueryString["NMP"].ToString();
            
        }

        webServicePath = getWebServicePath();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        //Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 100);
        if (altura > 0)
        {
            //alturaObject = altura + "px";

            //alturaObject = "600px";
            //alturaFlasEdicao = altura + 200 + "";
            alturaFlasEdicao = "920px";
            //largoFlasEdicao = altura + 385 + "px";
            largoFlasEdicao = "1024px";
        }

        //alturaObject = Request.QueryString["Altura"] + "" == "" ? alturaPrincipal + "px" : int.Parse(Request.QueryString["Altura"].ToString()) - 30 + "px";


        alturaObject = altura + "px";

        alturaSwf = altura - 80;
    }

    private string getWebServicePath()
    {
        return cDados.getPathSistema() + "wsPortal.asmx";
    }
}
