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
using System.IO;
using System.Drawing;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using System.Collections.Generic;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Projetos_DadosProjeto_Financeiro : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int idProjeto;
    public string alturaTabela;
    public string larguraTabela = "";
    private int qtdColunasVisiveis = 0;
    public bool mostraCusto = true;

    List<string> valores = new List<string>(12);



    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        valores.Add(Resources.traducao.Financeiro_janeiro);//indice 1
        valores.Add(Resources.traducao.Financeiro_fevereiro);//indice 2
        valores.Add(Resources.traducao.Financeiro_mar_o);
        valores.Add(Resources.traducao.Financeiro_abril);
        valores.Add(Resources.traducao.Financeiro_maio);
        valores.Add(Resources.traducao.Financeiro_junho);
        valores.Add(Resources.traducao.Financeiro_julho);
        valores.Add(Resources.traducao.Financeiro_agosto);
        valores.Add(Resources.traducao.Financeiro_setembro);
        valores.Add(Resources.traducao.Financeiro_outubro);
        valores.Add(Resources.traducao.Financeiro_novembro);
        valores.Add(Resources.traducao.Financeiro_dezembro);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (!IsPostBack)
        {
            if (Request.QueryString["MostraGrupo"] != null && Request.QueryString["MostraGrupo"].ToString() == "N")
                campoGrupo.Visible = false;

            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsFin");
        }
        
        cDados.aplicaEstiloVisual(Page);        

        if (!IsPostBack)
        {
            if (cDados.getInfoSistema("Tipo") != null)
                ddlTipo.Value = cDados.getInfoSistema("Tipo").ToString();

            cDados.setInfoSistema("Tipo", ddlTipo.Items.Count > 0 ? ddlTipo.Value.ToString() : "-1");

            DataSet dsTemp = cDados.getParametrosSistema("layoutAnaliseFinanceira", "TASQUES_OcultarColunaCusto", "labelDespesa");
            
            

            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["layoutAnaliseFinanceira"].ToString() + "" != "")
            {
                campoDespesaReceita.Caption = dsTemp.Tables[0].Rows[0]["labelDespesa"].ToString() + "/Receita";
                mostraCusto = dsTemp.Tables[0].Rows[0]["TASQUES_OcultarColunaCusto"].ToString() == "S";

                if (dsTemp.Tables[0].Rows[0]["layoutAnaliseFinanceira"].ToString() == "1")
                {
                    campoValor.Visible = true;
                    campoValorPrevistoData.Visible = true;
                    campoMes.Visible = false;
                    campoAno.Visible = false;

                    campoValor.Options.ShowInCustomizationForm = true;
                    campoValorPrevistoData.Options.ShowInCustomizationForm = true;
                    campoValorRestante.Options.ShowInCustomizationForm = true;
                    campoValorHE.Options.ShowInCustomizationForm = true;
                }
                else if (dsTemp.Tables[0].Rows[0]["layoutAnaliseFinanceira"].ToString() == "2")
                {
                    //SESCOOP
                    campoValor.Visible = false;
                    campoValorPrevistoData.Visible = false;
                    fieldNomeCR.Visible = true;
                    fieldNomeCR.Caption = "Ação";
                    campoValorReal.Caption = "Valor Realizado";

                    campoValor.Options.ShowInCustomizationForm = false;
                    campoValorPrevistoData.Options.ShowInCustomizationForm = false;
                    campoValorRestante.Options.ShowInCustomizationForm = false;
                    campoValorHE.Options.ShowInCustomizationForm = false;

                }
                else if (dsTemp.Tables[0].Rows[0]["layoutAnaliseFinanceira"].ToString() == "3")
                {
                    //TELES PIRES
                    campoValor.Visible = false;
                    campoValorPrevistoData.Visible = false;
                    fieldNomeCR.Visible = false;
                    campoConta.Visible = false;
                    campoValorHE.Visible = false;
                    campoValorRestante.Visible = false;
                    campoVariacaoValor.Visible = false;

                    campoValor.Options.ShowInCustomizationForm = false;
                    campoValorPrevistoData.Options.ShowInCustomizationForm = false;
                    fieldNomeCR.Options.ShowInCustomizationForm = false;
                    campoConta.Options.ShowInCustomizationForm = false;
                    campoValorHE.Options.ShowInCustomizationForm = false;
                    campoValorRestante.Options.ShowInCustomizationForm = false;
                    campoVariacaoValor.Options.ShowInCustomizationForm = false;

                }


                campoValor.Visible = mostraCusto;
                campoVariacaoValor.Visible = mostraCusto;
                campoVariacaoValor.Options.ShowInCustomizationForm = mostraCusto;
                campoValor.Options.ShowInCustomizationForm = mostraCusto;
            }

            DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            {
                if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N")
                {
                    ddlTipo.Items.Remove(ddlTipo.Items.FindByValue("R"));
                }

                if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N")
                {
                    ddlTipo.Items.Remove(ddlTipo.Items.FindByValue("D"));
                }

                if (ddlTipo.Items.Count > 0)
                    ddlTipo.SelectedIndex = 0;
            }
        }

        if (ddlTipo.Items.Count == 1)
        {
            tbBotoes0.Style.Add("display", "none");
            campoDespesaReceita.Visible = false;
        }

        //MyEditorsLocalizer.Activate();
        DataSet ds = cDados.getDadosFinanceiro(idProjeto, ddlTipo.Value.ToString(), "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            pvgFinanceiro.DataSource = ds.Tables[0];
            pvgFinanceiro.DataBind();
        }
        atualizaAreaDados();
        defineAlturaTela();

        string urlGrafico = "FinanceiroCurvaS.aspx";

        //if (Request.QueryString["VersaoGrafico"] != null && Request.QueryString["VersaoGrafico"].ToString() != "")
        //    urlGrafico = "FinanceiroGraficos" + Request.QueryString["VersaoGrafico"].ToString() + ".aspx";

        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = '" + urlGrafico + "?idProjeto=" + idProjeto + "';}";
        imgGraficos.Style.Add("cursor", "pointer");

        if (ddlTipo.Items.FindByValue("D") != null)
        {
            DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "labelDespesa");

            string labelDespesa = "Despesa";

            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["labelDespesa"].ToString() != "")
                labelDespesa = dsParam.Tables[0].Rows[0]["labelDespesa"].ToString();

            ddlTipo.Items.FindByValue("D").Text = labelDespesa;
        }

        cDados.configuraPainelBotoesOLAP(tbBotoes);

        //dxpgFilterArea
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        pnDiv.Height = (alturaPrincipal - 245);//a div vai ficar com essa altura
        pnDiv.Width = (larguraPrincipal - 200);

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
    
    protected void pvgFinanceiro_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
    {
        int incrementaVariacao = (hfGeral.Contains("VariacaoValor")) ? int.Parse(hfGeral.Get("VariacaoValor").ToString()) : -1;

        if (incrementaVariacao != -1)
        {
            for (int i = 0; i < pvgFinanceiro.ColumnCount; i = i + 1)
            {

                if ((e.ColumnIndex == incrementaVariacao) && Convert.ToDouble(e.Value) > 0)
                    e.CellStyle.ForeColor = System.Drawing.Color.Red;
                incrementaVariacao = incrementaVariacao + qtdColunasVisiveis;
            }
        }
    }

    private void atualizaAreaDados()
    {
        hfGeral.Clear();
        qtdColunasVisiveis = 0;
        for (int i = 0; i < pvgFinanceiro.Fields.Count; i++)
        {
            if (pvgFinanceiro.Fields[i].Area == PivotArea.DataArea && pvgFinanceiro.Fields[i].Visible ==true)
            {
                hfGeral.Set(pvgFinanceiro.Fields[i].FieldName, pvgFinanceiro.Fields[i].AreaIndex);
                if (qtdColunasVisiveis <= pvgFinanceiro.Fields[i].AreaIndex)
                qtdColunasVisiveis = pvgFinanceiro.Fields[i].AreaIndex + 1;
            }
        }
    }

    protected void pvgFinanceiro_FieldAreaIndexChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }
    protected void pvgFinanceiro_FieldAreaChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }
    protected void pvgFinanceiro_FieldVisibleChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }
    protected void pvgFinanceiro_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        cDados.setInfoSistema("Tipo", ddlTipo.Items.Count > 0 ? ddlTipo.Value.ToString() : "-1");
    }
    protected void pvgFinanceiro_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field.FieldName == "Mes")
        {
            object valor1 = valores.IndexOf(e.Value1.ToString());
            object valor2 = valores.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "AnlFinPrj", pvgFinanceiro);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "AnlFinPrj", Resources.traducao.Financeiro_an_lise_financeira, this, pvgFinanceiro);
    }

    #endregion
}
