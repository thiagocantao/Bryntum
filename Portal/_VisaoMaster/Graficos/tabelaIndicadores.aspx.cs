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
using System.Drawing;

public partial class _VisaoMaster_Graficos_tabelaIndicadores : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade;
    int numeroUnidadeGeradora = 0;
    string nomeSitio = "";
    string statusTarefas = "";
    string tipoTarefas = "";

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
        numeroUnidadeGeradora = int.Parse(Request.QueryString["NUG"].ToString());
        nomeSitio = Request.QueryString["NS"].ToString();
        statusTarefas = Request.QueryString["ST"].ToString();
        tipoTarefas = Request.QueryString["TT"].ToString();

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
            inicializaFiltros();

        carregaGrid();

        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 120;
        }

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

    }

    private void inicializaFiltros()
    {
        if (tipoTarefas != "")
            gvDados.FilterExpression += "[Concluido] = '" + tipoTarefas + "'";

        if (statusTarefas != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += " AND [StatusCor] = '" + statusTarefas + "'";
            else
                gvDados.FilterExpression = " [StatusCor] = '" + statusTarefas + "'";
        }
    }

    private void carregaGrid()
    {
        DataSet ds;

        if (nomeSitio == "DS")
            ds = cDados.getTarefasStatusPainelPresidenciaDS(codigoEntidade);
        else
            ds = cDados.getTarefasStatusPainelPresidencia(codigoEntidade, nomeSitio, numeroUnidadeGeradora);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            string corLinha = gvDados.GetRowValues(e.VisibleIndex, "StatusCor").ToString();

            if (corLinha == "Vermelho")
            {
                e.Row.Style.Add("Color", "Red");
            }
            else if (corLinha == "Amarelo")
            {
                e.Row.Style.Add("Color", "#ECBD00");
            }
        }
    }

    protected void pJustificativa_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
    {
        if (e.Parameter != "")
        {
            string codigoCronograma = e.Parameter.Split('|')[0];
            string codigoTarefa = e.Parameter.Split('|')[1];

            DataSet ds = cDados.getAnotacoesTarefasCronogramaProjeto(codigoCronograma, codigoTarefa, "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtTarefa.Text = ds.Tables[0].Rows[0]["NomeTarefa"].ToString();
                spanJustificativa.InnerHtml = "<div style='height: 250px; overflow:auto; width:100%; background-color:#EBEBEB; color:Black; padding:3px; border: 1px solid #808080;'>" + ds.Tables[0].Rows[0]["Anotacoes"] + "</div>";
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

        string possuiAnotacoes = "N";

        if (gvDados.GetRowValues(e.VisibleIndex, "IndicaPossuiAnotacao") != null)
        {
            possuiAnotacoes = gvDados.GetRowValues(e.VisibleIndex, "IndicaPossuiAnotacao").ToString();
        }


        if (possuiAnotacoes == "N")
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/comentariosDes.png";
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "TerminoReprogramado")
        {
            string previsaoTermino = gvDados.GetRowValues(e.VisibleIndex, "PrevisaoTermino") + "";

            if (e.CellValue + "" == previsaoTermino)
                e.Cell.Text = "";
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstMarPnlGer");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstMarPnlGer", "Marcos", this);
    }

    #endregion
    
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
        if (e.Column.Name == "StatusCor" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.Text = "l";
            e.TextValue = "l";
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Contains("Vermelho"))
            {
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Contains("Amarelo"))
            {
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Contains("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Contains("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            else if (e.Value.ToString().Contains("Branco"))
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }
}
