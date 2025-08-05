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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;
using System.IO;
using DevExpress.XtraPrinting;

public partial class _Portfolios_frameSelecaoBalanceamento_OlapCriterios : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    private int qtdColunasVisiveis = 0;
    string cenario = "", categoria = "";
    int codigoPortfolio = 0;

    int codigoUsuarioResponsavel = -1;
    
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (cDados.getInfoSistema("Cenario") != null)
            cenario = cDados.getInfoSistema("Cenario").ToString();

        if (cDados.getInfoSistema("Categoria") != null)
            categoria = cDados.getInfoSistema("Categoria").ToString();

 
        cDados.aplicaEstiloVisual(Page);
        

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        //MyEditorsLocalizer.Activate();

        carregaGrid();

        defineLarguraTela();

        grid.OptionsPager.Visible = false;

        string exel = ((Request.QueryString["Excel"] != null) && (Request.QueryString["Excel"].ToString() + "" != "")) ? Request.QueryString["Excel"].ToString() : "";
        
        if (!IsPostBack)
            grid.CollapseAll();

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }
    
    private void carregaGrid()
    {
        string where = " AND IndicaCenario" + cenario + " = 'S'";

        if (categoria != "-1")
            where += " AND CodigoCategoria = " + categoria;

        //DataTable dtOlap = cDados.getOlapRecursos(codigoEntidadeUsuarioResponsavel,where).Tables[0];

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());


        DataTable dtOlap = cDados.getOlapRecursosPorPortifolioESenario(codigoEntidadeUsuarioResponsavel, where, codigoPortfolio, cenario).Tables[0];


        grid.DataSource = dtOlap;

            grid.DataBind();

            atualizaAreaDados();
    }   

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 43).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 340).ToString() + "px";
        
    }

    public class MyEditorsLocalizer : DevExpress.Web.ASPxPivotGrid.ASPxPivotGridResLocalizer
    {
        public static void Activate()
        {
            MyEditorsLocalizer localizer = new MyEditorsLocalizer();
            DefaultActiveLocalizerProvider<PivotGridStringId> provider = new DefaultActiveLocalizerProvider<PivotGridStringId>(localizer);
            MyEditorsLocalizer.SetActiveLocalizerProvider(provider);

        }
        public override string GetLocalizedString(PivotGridStringId id)
        {
            switch (id)
            {
                case PivotGridStringId.GrandTotal: return "Total";
                case PivotGridStringId.FilterShowAll: return "Todos";
                case PivotGridStringId.FilterCancel: return "Cancelar";
                case PivotGridStringId.PopupMenuRemoveAllSortByColumn: return "Remover Ordenação";
                case PivotGridStringId.PopupMenuShowPrefilter: return "Mostrar Pré-filtro";
                case PivotGridStringId.PopupMenuShowFieldList: return "Listar Colunas Disponíveis";
                case PivotGridStringId.PopupMenuRefreshData: return "Atualizar Dados";
                case PivotGridStringId.PrefilterFormCaption: return "Pré-filtro";
                case PivotGridStringId.PopupMenuCollapse: return "Contrair";
                case PivotGridStringId.PopupMenuCollapseAll: return "Contrair Todos";
                case PivotGridStringId.PopupMenuExpand: return "Expandir";
                case PivotGridStringId.PopupMenuExpandAll: return "Expandir Todos";
                case PivotGridStringId.PopupMenuHideField: return "Remover Coluna";
                case PivotGridStringId.PopupMenuHideFieldList: return "Remover Coluna";
                case PivotGridStringId.PopupMenuHidePrefilter: return "Remover Pré-filtro";
                case PivotGridStringId.RowHeadersCustomization: return "Arraste as colunas que irão formar os agrupamentos da consulta";
                case PivotGridStringId.ColumnHeadersCustomization: return "Arraste as colunas que irão formar as colunas da consulta";
                case PivotGridStringId.FilterHeadersCustomization: return "Arraste as colunas que irão formar os filtros da consulta";
                case PivotGridStringId.DataHeadersCustomization: return "Arraste as colunas que irão formar os dados da consulta";
                
                case PivotGridStringId.FilterInvert: return "Inverter Filtro";
                case PivotGridStringId.PopupMenuFieldOrder: return "Ordenar";
                case PivotGridStringId.PopupMenuSortFieldByColumn: return "Ordenar pela coluna {0}";
                case PivotGridStringId.PopupMenuSortFieldByRow: return "Ordenar pela linha {0}";
                case PivotGridStringId.CustomizationFormCaption: return "Colunas Disponíveis";
                default: return base.GetLocalizedString(id);
            }
        }
    }

    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
    {
        grid.CollapseAll();
    }

    private void atualizaAreaDados()
    {
        hfGeral.Clear();
        qtdColunasVisiveis = 0;
        for (int i = 0; i < grid.Fields.Count; i++)
        {
            if (grid.Fields[i].Area == PivotArea.DataArea && grid.Fields[i].Visible == true)
            {
                hfGeral.Set(grid.Fields[i].FieldName, grid.Fields[i].AreaIndex);
                if (qtdColunasVisiveis <= grid.Fields[i].AreaIndex)
                    qtdColunasVisiveis = grid.Fields[i].AreaIndex + 1;
            }
        }
    }

    protected void grid_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        int indexColuna = 0;

        if (hfGeral.Contains("Disponibilidade"))
        {
            indexColuna = int.Parse(hfGeral.Get("Disponibilidade").ToString());
        }

        for (int i = 0; i < grid.ColumnCount; i = i + 1)
        {
            if ((e.ColumnIndex == indexColuna) && Convert.ToInt32(e.Value) < 0)
                e.CellStyle.ForeColor = System.Drawing.Color.Red;

            if (indexColuna != -2)
            {
                indexColuna += qtdColunasVisiveis;
            }
        }
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        grid.CollapseAll();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.exportaOlap(ASPxPivotGridExporter1, parameter, this);
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, false, "OlapRecSelBal", Resources.traducao.frameSelecaoBalanceamento_OlapRecursosTabela_olap_recursos_sel__bal_, this, grid);
    }

    #endregion
}
