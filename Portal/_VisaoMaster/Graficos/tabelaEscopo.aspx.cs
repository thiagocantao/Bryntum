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
using System.Drawing;
using DevExpress.Web;

public partial class _VisaoMaster_Graficos_tabelaEscopo: System.Web.UI.Page
{
		#region Fields (3) 

    dados cDados;
    int codigoEntidade;
    string status = "";

		#endregion Fields 

		#region Methods (4) 

		// Protected Methods (3)     

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

        cDados.aplicaEstiloVisual(this);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["ST"] != null && Request.QueryString["ST"].ToString() != "")
        {
            status = Request.QueryString["ST"].ToString();
        }

        carregaGrid();

        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 110;
        }
    }
		// Private Methods (1) 

    private void carregaGrid()
    {
        string codigoArea = Request.QueryString["CA"] == null ? "0" : Request.QueryString["CA"].ToString();
        int ano = DateTime.Now.Year;
        int trimestre = Convert.ToInt32(Math.Round(Convert.ToDouble(DateTime.Now.Month) / 3 + 0.25));

        DataSet ds = cDados.getListaAcompanhamentoEscopo(codigoEntidade, int.Parse(codigoArea), status);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
        Font fonte = new Font("Verdana", 9);
        e.BrickStyle.Font = fonte;
    }

		#endregion Methods 
}
