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

public partial class _VisaoMaster_Graficos_tabelaOrcamento : System.Web.UI.Page
{
    dados cDados;
    
    int codigoEntidade;

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
        ajustaCabecalhoGrid();
        carregaGrid();

        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 70;
        }
    }

    private void ajustaCabecalhoGrid()
    {
        //Se o mes atual for janeiro fevereiro e março
        gvDados.Columns["Jan"].Caption = "Janeiro/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Fev"].Caption = "Fevereiro/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Mar"].Caption = "Março/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Abr"].Caption = "Abril/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Mai"].Caption = "Maio/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Jun"].Caption = "Junho/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Jul"].Caption = "Julho/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Ago"].Caption = "Agosto/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Set"].Caption = "Setembro/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Out"].Caption = "Outubro/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Nov"].Caption = "Novembro/" + DateTime.Now.Year + " (R$ Mil)";
        gvDados.Columns["Dez"].Caption = "Dezembro/" + DateTime.Now.Year + " (R$ Mil)";
    }

    private void carregaGrid()
    {
        gvDados.DataSource = getDataTableGrid();
        gvDados.DataBind();
    }

    private DataTable getDataTableGrid()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Grupo");
        dt.Columns.Add("Conta");

        for (int i = 1; i <= 12; i++)
        {
            dt.Columns.Add("Prev" + i, System.Type.GetType("System.Double"));
            dt.Columns.Add("Real" + i, System.Type.GetType("System.Double"));
        }

        string codigoArea = Request.QueryString["CA"] == null ? "0" : Request.QueryString["CA"].ToString();
        int ano = DateTime.Now.Year;
        int trimestre = Convert.ToInt32(Math.Round(Convert.ToDouble(DateTime.Now.Month) / 3 + 0.25));

        DataSet ds = cDados.getListaAcompanhamentoCusto(codigoEntidade, int.Parse(codigoArea), DateTime.Now.Year);

        string descricaoGrupoConta = "";

        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["Grupo"] + "#" + dr["Conta"] != descricaoGrupoConta)
            {
                string grupo = dr["Grupo"].ToString();
                string conta = dr["Conta"].ToString();
                descricaoGrupoConta = grupo + "#" + conta;
                DataRow novaLinha = dt.NewRow();
                novaLinha["Grupo"] = grupo;
                novaLinha["Conta"] = conta;

                string clausula = "Grupo = '" + grupo + "' AND Conta = '" + conta + "'";

                if (grupo == "")
                {
                    clausula = "(Grupo IS NULL OR Grupo = '') AND Conta = '" + conta + "'";
                }

                DataRow[] drs = ds.Tables[0].Select(clausula);

                foreach(DataRow drLinha in drs)
                {
                    novaLinha["Prev" + drLinha["Mes"]] = drLinha["ValorPrevisto"];
                    novaLinha["Real" + drLinha["Mes"]] = drLinha["ValorReal"];
                }

                dt.Rows.Add(novaLinha);
            }
        }

        return dt;
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
    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
            e.Row.BackColor = Color.FromName("#EBEBEB");
    }
}
