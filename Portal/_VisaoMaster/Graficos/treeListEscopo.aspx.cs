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
using DevExpress.Web.ASPxTreeList;

public partial class _VisaoMaster_Graficos_treeListEscopo : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade;
    string status = "T";
    public string alturaTree = "";

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

        //cDados.aplicaEstiloVisual(this);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["ST"] != null && Request.QueryString["ST"].ToString() != "")
        {
            status = Request.QueryString["ST"].ToString();
        }

        carregaGrid();
        
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        
        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            gvDados.Settings.ScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 90;
        }

        if (!IsPostBack)
            gvDados.ExpandToLevel(2);

        cDados.aplicaEstiloVisual(this);
    }

    private void carregaGrid()
    {
        string codigoArea = Request.QueryString["CA"] == null ? "0" : Request.QueryString["CA"].ToString();
        int ano = DateTime.Now.Year;
        int trimestre = Convert.ToInt32(Math.Round(Convert.ToDouble(DateTime.Now.Month) / 3 + 0.25));

        DataSet ds = cDados.getArvoreAcompanhamentoEscopo(codigoEntidade, int.Parse(codigoArea), status);

        //gvDados.SortBy((TreeListDataColumn)gvDados.Columns["CodigoReservado"], DevExpress.Data.ColumnSortOrder.Ascending);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void ASPxGridViewExporter1_RenderBrick1(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        int level = gvDados.FindNodeByKeyValue(e.NodeKey).Level;

        e.BrickStyle.ResetBackColor();

        if (e.RowKind == TreeListRowKind.Header)
        {
            if (e.Column.FieldName == "Item")
            {
                Font fonteBold = new Font("Verdana", 12, FontStyle.Bold);
                e.BrickStyle.Font = fonteBold;
                e.BrickStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(10);
            }
            else
            {
                Font fonteBold = new Font("Verdana", 8, FontStyle.Bold);
                e.BrickStyle.Font = fonteBold;
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
            }
        }
        else
        {

            if (level == 1)
            {
                Font fonteBold = new Font("Verdana", 8, FontStyle.Bold);
                e.BrickStyle.Font = fonteBold;
                e.BrickStyle.BackColor = Color.MidnightBlue;
                e.BrickStyle.ForeColor = Color.White;
            }
            else if (level == 2)
            {
                Font fonteBold = new Font("Verdana", 8, FontStyle.Bold);
                e.BrickStyle.Font = fonteBold;
                e.BrickStyle.BackColor = Color.Gainsboro;
                e.BrickStyle.ForeColor = Color.Black;
            }
            else if (level > 3)
            {
                Font fonteItalic = new Font("Verdana", 8, FontStyle.Italic);
                e.BrickStyle.Font = fonteItalic;
            }
            else
            {
                Font fonte = new Font("Verdana", 8);
                e.BrickStyle.Font = fonte;
            }

            if (e.Column.FieldName == "DataPrevista" || e.Column.FieldName == "Status")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
            }
        }
        //e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center);
        //e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        //e.BrickStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3);
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
    {
        if (e.Column.FieldName == "PercRelacaoTotalPrevisto" || e.Column.FieldName == "PercRelacaoTotalContratado" || e.Column.FieldName == "PercContratado")
        {
            if (e.Level <= 3)
            {
                e.Cell.Text = string.Format("{0:p2}", e.CellValue);
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
    {
       // e.Row.Font.Size = new FontUnit("10pt");
       //e.Row.Font.Name = "Arial";     
        if (e.Level == 1)
        {
            e.Row.BackColor = Color.FromName("#16365C");
            e.Row.ForeColor = Color.White;
            e.Row.Font.Bold = true;
        }
        else if (e.Level == 2)
        {
            e.Row.BackColor = Color.FromName("#F2F2F2");
            e.Row.ForeColor = Color.Black;
            e.Row.Font.Bold = true;
        }
        else if (e.Level > 3)
        {
            e.Row.Font.Italic = true;
        }
    }
}
