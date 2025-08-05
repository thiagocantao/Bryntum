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
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.Web;
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Collections.Generic;
using System.Drawing;

public partial class administracao_Relatorios_logAcessoAoSistema : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    private List<string> Meses = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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
        Meses.Clear();
        Meses.Add("Janeiro");
        Meses.Add("Fevereiro");
        Meses.Add("Março");
        Meses.Add("Abril");
        Meses.Add("Maio");
        Meses.Add("Junho");
        Meses.Add("Julho");
        Meses.Add("Agosto");
        Meses.Add("Setembro");
        Meses.Add("Outubro");
        Meses.Add("Novembro");
        Meses.Add("Dezembro");
        Meses.Add("Nenhum");
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioLogado, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_CnsLogAcs");
        }

        if (!Page.IsCallback && !Page.IsPostBack)
        {
            txtInicio.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            txtTermino.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        if (!Page.IsCallback)
        {
            //MyEditorsLocalizer.Activate();
        }

        cDados.aplicaEstiloVisual(this);

        populaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        //pvgLogAcessoSistema.Height = alturaPrincipal - 270;

        alturaTabela = (alturaPrincipal - 185) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";//(larguraPrincipal - 10) + "px";

        divGrid.Style.Add("Overflow", "auto");
        divGrid.Style.Add("height", alturaTabela);
        divGrid.Style.Add("width", larguraTabela);
    }
    
    private void populaGrid()
    {
        DataSet ds = cDados.getRelLogAcessoSistema(codigoUsuarioLogado, codigoEntidade, txtInicio.Text, txtTermino.Text);
        if (cDados.DataSetOk(ds))
        {
            pvgLogAcessoSistema.DataSource = ds.Tables[0];
            pvgLogAcessoSistema.DataBind();
        }
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
    protected void pvgLogAcessoSistema_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field.FieldName == "Data")
        {
            if (e.Value1 != null && e.Value2 != null)
            {
                DateTime data1 = DateTime.Parse(e.Value1.ToString());
                DateTime data2 = DateTime.Parse(e.Value2.ToString());
                // e.Result = (data1 < data2) ? 1 : (data1 == data2) ? 0 : -1;
                e.Result = System.Collections.Comparer.Default.Compare(data1, data2);
                e.Handled = true;
            } 
        }
        if (e.Field.FieldName == "Horario")
        {
            if (e.Value1 != null && e.Value2 != null)
            {
                DateTime data1 = DateTime.Parse(e.Value1.ToString());
                DateTime data2 = DateTime.Parse(e.Value2.ToString());
                //e.Result = (data1 < data2) ? 1 : (data1 == data2) ? 0 : -1;
                e.Result = System.Collections.Comparer.Default.Compare(data1, data2);
                e.Handled = true;
            } 
        }
        if (e.Field == fldMes)
        {
            if (e.Value1 != null && e.Value2 != null)
            {
                object valor1 = Meses.IndexOf(e.Value1.ToString());
                object valor2 = Meses.IndexOf(e.Value2.ToString());
                e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
                e.Handled = true;
            }
        }
    }

    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        populaGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.exportaOlap(exporter, parameter, this);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, false, "LogAcsSis", lblTituloTela.Text, this, pvgLogAcessoSistema);
    }

    #endregion

    protected void exporter_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName == "QuantidadeAcesso" && (e.Value != null))
            {
                e.Brick.TextValueFormatString = "{0:n0}";
            }
        }
    }
}
