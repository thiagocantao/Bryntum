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

public partial class _Portfolios_listaProjetos : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    
    public int codigoEntidadeUsuarioResponsavel;

    public string alturaGrid;
    public string estiloFooter = "dxtlFooter";

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        
        defineAlturaTela();

        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;
        
        populaGrid();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        tvDados.Settings.ScrollableHeight = (altura - 240);        
    }

    private void populaGrid()
    {
        DataSet ds = cDados.getListaProjetosPortfolios(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), "");

        if (cDados.DataSetOk(ds))
        {
            tvDados.DataSource = ds;
            tvDados.DataBind();
        }
    }
    
    protected void tvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
    {
    }
}
