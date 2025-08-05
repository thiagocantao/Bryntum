using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class workflow_workflow : System.Web.UI.Page
{
    dados cDados;
    private int alturaPrincipal = 0;
    private int larguraPrincipal = 0;
    private string bancodb;
    private string Ownerdb;
    private string resolucaoCliente = "";
    public string webServicePath = "";  // caminho do web service
    public string doubleClipTarea = ""; // link del popup.
    public string codigoFluxo = "";
    public string codigoWorkflow = "";
    public string nomeMapa = "";
    public string codigoUsuario = "";
    public string codigoEntidade = "";
    public string alturaObject = "";
    public string larguraObject = "";
    public int alturaFlashEdicao = 0;
    public int larguraFlashEdicao = 0;
    public string tipoFluxo = "";
    public string tipoSalvo = "";
    public string xmlVersao = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
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
        codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        webServicePath = getWebServicePath();
        try
        {
            codigoWorkflow = Request.QueryString["cW"].ToString();
            codigoFluxo = Request.QueryString["cF"].ToString();
            xmlVersao = Request.QueryString["vF"].ToString();
            if (Request.QueryString["tW"].ToString() == "0" && codigoWorkflow == "0")
                tipoFluxo = "novo";
            else if (codigoWorkflow != "0")
                tipoFluxo = "continuar"; 
            else
                tipoFluxo = "novoModelo";
        }
        catch(Exception)
        {
            Response.Redirect("~/administracao/adm_CadastroWorkflows.aspx");
        }
        
    }

    protected internal void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Split('x')[1]) - 120;
        larguraPrincipal = int.Parse(resolucaoCliente.Split('x')[0]) - 10;
        alturaFlashEdicao = alturaPrincipal - 5;
        larguraFlashEdicao = larguraPrincipal - 5;

        larguraObject = larguraFlashEdicao + "px";
        alturaObject = alturaFlashEdicao + "px";
    }

    private string getWebServicePath()
    {
        return cDados.getPathSistema() + "workflow/wsFluxo.asmx";
    }


    
}