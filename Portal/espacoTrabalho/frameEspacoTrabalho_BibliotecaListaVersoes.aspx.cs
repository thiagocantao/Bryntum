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

public partial class espacoTrabalho_frameEspacoTrabalho_BibliotecaListaVersoes : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    int codigoAnexo;
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
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        codigoAnexo = int.Parse(Request.QueryString["CA"].ToString());

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        carregaGrid();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        cDados.aplicaEstiloVisual(Page);

        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 320);

        //gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 45;
        gvDados.Width = new Unit("100%");
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getVersoesAnexo(codigoAnexo);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string codigoAnexo = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAnexo").ToString();
        string codigoSequenciaAnexo = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();

        cDados.download(int.Parse(codigoAnexo), int.Parse(codigoSequenciaAnexo), Page, Response, Request, true);
    }
}
