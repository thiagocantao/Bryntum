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

public partial class _Portfolios_frameSelecaoBalanceamento_AnaliseGeral : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";

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


        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        //MyEditorsLocalizer.Activate();

        carregaGrid();

        defineLarguraTela();

        pvgDadosProjeto.OptionsPager.Visible = false;

        if (!IsPostBack)
            pvgDadosProjeto.CollapseAll();

        cDados.traduzControles(this);
    }

    private void carregaGrid()
    {
        int codigoPortfolio = 0;

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        DataTable dtOlap = cDados.getOlapAnaliseGeral(codigoEntidadeUsuarioResponsavel, codigoPortfolio, "").Tables[0];

        pvgDadosProjeto.DataSource = dtOlap;

        pvgDadosProjeto.DataBind();
    }   

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 33).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 200).ToString() + "px";
        
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
        pvgDadosProjeto.CollapseAll();
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pvgDadosProjeto.CollapseAll();
    }

    protected void pvgDadosProjeto_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (e.FieldName.Equals("IndiceDesempenhoPrazo"))
        {
            //(%) idp = valor agregado / valor planejado
            double ValorAgregado = 0, ValorPlanejado = 0;
            double dblAux;

            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if (summaryRow["ValorAgregado"] != null && double.TryParse(summaryRow["ValorAgregado"].ToString(), out dblAux))
                    ValorAgregado += dblAux;

                if (summaryRow["ValorPlanejado"] != null && double.TryParse(summaryRow["ValorPlanejado"].ToString(), out dblAux))
                    ValorPlanejado += dblAux;
            }
            if (ValorAgregado == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = ValorAgregado / ValorPlanejado;

        }

        if (e.FieldName.Equals("IndiceDesempenhoCusto"))
        {
            //(%) idp = valor agregado / custo real
            double ValorAgregado = 0, CustoReal = 0;
            double dblAux;

            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if (summaryRow["ValorAgregado"] != null && double.TryParse(summaryRow["ValorAgregado"].ToString(), out dblAux))
                    ValorAgregado += dblAux;

                if (summaryRow["CustoReal"] != null && double.TryParse(summaryRow["CustoReal"].ToString(), out dblAux))
                    CustoReal += dblAux;
            }
            if (ValorAgregado == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = ValorAgregado / CustoReal;
        }
    }

    protected void pvgDadosProjeto_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.Value != null && e.DataField.FieldName == "VariacaoTrabalho")
        {
            if (Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        } 
    }
}
