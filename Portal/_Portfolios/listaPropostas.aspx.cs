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
using System.Text;

public partial class _Portfolios_listaPropostas : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;

    public int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // se a chamada veio do click no link projetos... 
        if (Request.QueryString["P"] != null && Request.QueryString["P"] == "S")
        {
            Response.Redirect("~/_Portfolios/framePropostas.aspx");
        }

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        defineAlturaTela();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        populaGrid();
    }

    private void populaGrid()
    {
        DataSet ds = cDados.getPropostas(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btnIncluir")
        {
            Response.Redirect("wf_novaProposta.aspx");  
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        gvDados.Settings.VerticalScrollableHeight = (alturaPrincipal - 165);

    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string parametro = e.Parameters;
        if (parametro.Substring(0, 2) == "CP")
        {
            string codigoProjeto = parametro.Substring(2);
            cDados.setInfoSistema("CodigoProjeto", codigoProjeto);
        }
    }
}