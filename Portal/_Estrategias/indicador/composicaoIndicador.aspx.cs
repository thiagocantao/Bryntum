/*
 * OBSERVAÇÕES
 * 
 * MUDANÇAS:
 * 
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_AnlDad]
 */
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
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraPrinting;
using System.Web.Hosting;
using System.Diagnostics;
using System.Drawing;

public partial class _Estrategias_indicador_composicaoIndicador : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoIndicador = 0, codigoUnidadeLogada = 0, codigoUnidadeMapa = 0, codigoObjetivo = 0, codigoMapa = 0, codigoUsuario = 0;

    public int alturaGrafico = 120;
    public string alturaDivGrid = "";
    public string larguraDivGrid = "";

    System.Globalization.CultureInfo wCultureInfo;
    private List<string> Meses = new List<string>();

    #endregion

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


        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());
        codigoObjetivo = cDados.getInfoSistema("COE") != null ? int.Parse(cDados.getInfoSistema("COE").ToString()) : -1;
        codigoMapa = cDados.getInfoSistema("CodigoMapa") != null ? int.Parse(cDados.getInfoSistema("CodigoMapa").ToString()) : 0;
        codigoUnidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString() != null ? int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()) : -1;
        codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString() != null ? int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()) : -1;

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoUnidadeLogada, codigoIndicador, "null", "IN", codigoMapa * (-1), "null", "IN_AnlDad");
        }

        // variável usada para formatar dinamicamente os valores a serem mostrados na grid.
        wCultureInfo = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        Meses.Clear();
        Meses.Add("Jan");
        Meses.Add("Fev");
        Meses.Add("Mar");
        Meses.Add("Abr");
        Meses.Add("Mai");
        Meses.Add("Jun");
        Meses.Add("Jul");
        Meses.Add("Ago");
        Meses.Add("Set");
        Meses.Add("Out");
        Meses.Add("Nov");
        Meses.Add("Dez");

        cDados.aplicaEstiloVisual(this);

        montaCampos();
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        populaGrid();

        defineAlturaTela();

        cDados.configuraPainelBotoesOLAP(tbBotoes);

        if (codigoObjetivo <= 0)
            tdObjetivoMapa.Style.Add("display", "none");
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int constanteSomatoria = (codigoObjetivo <= 0) ? 90 : 0;

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        alturaGrafico = alturaPrincipal - 170;

        alturaDivGrid = (alturaPrincipal - 290 + constanteSomatoria) + "px";
        larguraDivGrid = (larguraPrincipal - 285) + "px";

    }


    private void montaCampos()
    {
        DataTable dtCampos = cDados.getInformacoesIndicador(codigoIndicador, codigoObjetivo).Tables[0];

        if (cDados.DataTableOk(dtCampos))
        {
            //txtMapa.Text = dtCampos.Rows[0]["TituloMapaEstrategico"].ToString();
            //txtObjetivoEstrategico.Text = dtCampos.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtIndicador.Text = dtCampos.Rows[0]["NomeIndicador"].ToString();
        }
    }

    private void populaGrid()
    {
        int codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());
        
        DataSet ds = cDados.getRelAnaliseDadosIndicador(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), codigoUnidadeLogada, codigoIndicador, codigoMapa);

        //int decimais = Convert.ToInt32(row["Decimais"]);
        //pvgDadosIndicador.Fields["Valor"];

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosIndicador.DataSource = ds.Tables[0];
            pvgDadosIndicador.DataBind();

            AjustaVisibilidadeTotais(pvgDadosIndicador);  // ajusta visibilidade de totais
        }
    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field.FieldName == "Mes")
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }

    protected void pvgDadosIndicador_FieldAreaIndexChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldFilterChanged(object sender, PivotFieldEventArgs e)
    {
        // se estiver mudando o filtro do campo 'Dado'
        if ("Dado" == e.Field.FieldName)
            AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        // se estiver mudando a visibilidade do campo 'Dado'
        if ("Dado" == e.Field.FieldName)
            e.Field.Visible = true;

        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldAreaChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    private void AjustaVisibilidadeTotais(ASPxPivotGrid grid)
    {
        DevExpress.XtraPivotGrid.PivotTotalsVisibility visibilidadeLinha, visibilidadeColuna, visibilidade;
        //bool grandTotalLinha, grandTotalColuna;

        // se o campo dado não estiver visível, nenhum total será permitido
        if (false == fldDado.Visible)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            //grandTotalLinha = false;
            //grandTotalColuna = false;
        }
        else if (1 == fldDado.FilterValues.Count)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
        }
        else
            switch (fldDado.Area)
            {
                case DevExpress.XtraPivotGrid.PivotArea.FilterArea:
                    // se estiver ná área de filtro, desativa os totais por que certamente estará 
                    // filtro mais de um valor, caso contrário teria entrado no 'else if' acima
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.ColumnArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.RowArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    break;
                default:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;

            } // switch (fldDado.Area)

        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        foreach (PivotGridField campo in rowFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeLinha)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeLinha;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        foreach (PivotGridField campo in colFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeColuna)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeColuna;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeLinha)
            grid.OptionsView.ShowRowGrandTotals = true;
        else
            grid.OptionsView.ShowRowGrandTotals = false;

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeColuna)
            grid.OptionsView.ShowColumnGrandTotals = true;
        else
            grid.OptionsView.ShowColumnGrandTotals = false;
    }

    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        DataTable dt = (DataTable)grid.DataSource;
        string dbFuncao = string.Empty;

        if ((dt != null) && (ds != null))
        {
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                {
                    DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];

                    dbFuncao = dataRow["Funcao"].ToString().ToUpper();

                    if (dbFuncao.Equals("AVG"))
                        e.CustomValue = e.SummaryValue.Average;
                    else if (dbFuncao.Equals("COUNT"))
                        e.CustomValue = e.SummaryValue.Count;
                    else if (dbFuncao.Equals("MAX"))
                        e.CustomValue = e.SummaryValue.Max;
                    else if (dbFuncao.Equals("MIN"))
                        e.CustomValue = e.SummaryValue.Min;
                    else if (dbFuncao.Equals("SUM"))
                        e.CustomValue = e.SummaryValue.Summary;
                    else if (dbFuncao.Equals("STDDEV"))
                        e.CustomValue = e.SummaryValue.StdDev;
                    else if (dbFuncao.Equals("STDDEVP"))
                        e.CustomValue = e.SummaryValue.StdDevp;
                    else if (dbFuncao.Equals("VAR"))
                        e.CustomValue = e.SummaryValue.Var;
                    else if (dbFuncao.Equals("VARP"))
                        e.CustomValue = e.SummaryValue.Varp;
                    else
                        e.CustomValue = e.SummaryValue.Summary;
                }

                break;  // faz apenas para a primeira linha por que obviamente todos as linhas 
                // irão se referir à mesma função já que todas tratam do mesmo 'Dado'
            }
        }
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        if ("Valor" == e.DataField.FieldName)
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            DataTable dt = (DataTable)grid.DataSource;
            int Decimais = 0;
            double valor = 0;

            if ((dt != null) && (ds != null))
            {
                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];

                        int.TryParse(dataRow["Decimais"].ToString(), out Decimais);

                        if (null != e.Value)
                            double.TryParse(e.Value.ToString(), out valor);

                        e.DisplayText = formataValorGrid(valor, dataRow["Medida"].ToString(), Decimais);
                    }
                    break;
                }
            }
        }
    }

    private string formataValorGrid(double valor, string medida, int casasDecimais)
    {
        string numeroFormatado = "";
        wCultureInfo.NumberFormat.CurrencyDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.NumberDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.PercentDecimalDigits = casasDecimais;

        if (medida.Equals("%"))
        {
            valor /= 100;
            numeroFormatado = valor.ToString("P", wCultureInfo);
        }
        else if (medida.Equals("Nº"))
            numeroFormatado = valor.ToString("N", wCultureInfo);
        else if (medida.Equals("R$"))
            numeroFormatado = valor.ToString("C", wCultureInfo);
        else
        {
            numeroFormatado = valor.ToString("N", wCultureInfo);
            numeroFormatado = medida + " " + numeroFormatado;
        }

        return numeroFormatado;
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapComposInd", pvgDadosIndicador);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapComposInd", "OLAP Dados Indicador", this, pvgDadosIndicador);
    }

    #endregion

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.ToLower().Contains("valor") && (e.Value != null))
            {
                ((TextBrick)e.Brick).XlsExportNativeFormat = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }
}
