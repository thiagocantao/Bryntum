/*
 * OBSERVAÇÕES
 * 
 * MUDANÇAS:
 * 
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_AnlUnd]
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
using System.Drawing;
using DevExpress.XtraPrinting;
using System.Web.Hosting;
using System.Diagnostics;

public partial class _Estrategias_indicador_OLAP_unidadesIndicador : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoIndicador = 0, codigoUnidadeLogada = 0, codigoUnidadeMapa = 0, codigoObjetivo = 0, codigoMapa = 0, codigoUsuario = 0;

    public int alturaGrafico = 120;

    object metaAtual = new object();
    object resultadoAtual = new object();

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

        codigoObjetivo = cDados.getInfoSistema("COE") != null ? int.Parse(cDados.getInfoSistema("COE").ToString()) : -1;
        codigoMapa = cDados.getInfoSistema("CodigoMapa") != null ? int.Parse(cDados.getInfoSistema("CodigoMapa").ToString()) : 0;
        codigoUnidadeLogada = cDados.getInfoSistema("CodigoEntidade") != null ? int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()) : -1;
        codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado") != null ? int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()) : -1;

        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoUnidadeLogada, codigoIndicador, "null", "IN", codigoMapa*(-1), "null", "IN_AnlUnd");
        }
        //getPermissoesTela();

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
        int? codIndic = null;
        codIndic = codigoIndicador;
        int codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());
        DataSet ds = cDados.getDadosOLAP_AnaliseIndicador(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), codIndic, null, codigoUnidadeLogada);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosIndicador.DataSource = ds.Tables[0];
            pvgDadosIndicador.DataBind();
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int constanteSomatoria = (codigoObjetivo <= 0) ? 60 : 0;

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        alturaGrafico = alturaPrincipal - 170;


        alturaDivGrid = (alturaPrincipal - 295 + constanteSomatoria) + "px";
        larguraDivGrid = (larguraPrincipal - 285) + "px";

    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field.FieldName == "MesPorExtenso")
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        if (("Resultado" == e.DataField.FieldName) || ("ResultadoAcumulado" == e.DataField.FieldName) ||
            ("Meta" == e.DataField.FieldName) || ("MetaAcumulada" == e.DataField.FieldName))
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

    protected void pvgDadosIndicador_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
    {
        string campoAObter;
        bool periodoNaTela = false, anoNaTela = false;
        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);


        for (int i = 0; i < rowFields.Count; i++)
        {
            if (rowFields[i].FieldName == "MesPorExtenso")
                periodoNaTela = true;

            if (rowFields[i].FieldName == "Ano")
                anoNaTela = true;
        }

       
            for (int i = 0; i < colFields.Count; i++)
            {
                if (colFields[i].FieldName == "MesPorExtenso")
                    periodoNaTela = true;

                if (colFields[i].FieldName == "Ano")
                    anoNaTela = true;
            }

        if (periodoNaTela)
            e.CustomValue = e.SummaryValue.Max;
        else
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            DataTable dt = (DataTable)grid.DataSource;
            
            if (anoNaTela)
            {
                if (e.FieldName.Equals("Meta") || e.FieldName.Equals("MetaAcumulada"))
                    campoAObter = "MetaRefAno";
                else if (e.FieldName.Equals("Desempenho") || e.FieldName.Equals("DesempenhoAcumulado"))
                    campoAObter = "DesempenhoRefAno";
                else
                    campoAObter = "ResultadoRefAno";
            } // if (anoNaTela
            else
            {
                campoAObter = e.FieldName;
            }

            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                {
                    DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                    if (campoAObter == "DesempenhoRefAno")
                    {
                        e.CustomValue = dataRow[campoAObter];
                    }
                    else
                    {
                        try
                        {
                            if (dataRow[campoAObter] + "" != "")
                                e.CustomValue = dataRow[campoAObter].ToString().Replace('.', ',');
                        }
                        catch { }
                    }
                    break; // busca uma linha já que será igual para todas as linhas, uma vez que unidade está presente
                } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
            } // foreach summaryRow in ds
        }
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        // não permite esconder o campo unidade
        if (e.Field.FieldName.Equals("Unidade") )
            e.Field.Visible = true;

    }

    #endregion

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            DevExpress.Web.ASPxPivotGrid.PivotGridField dfDecimais = pvgDadosIndicador.Fields["Decimais"];
            DevExpress.Web.ASPxPivotGrid.PivotGridField dfMedida = pvgDadosIndicador.Fields["Medida"];

            DevExpress.XtraPivotGrid.PivotDrillDownDataSource dataSource = pvgDadosIndicador.CreateDrillDownDataSource(e.ColumnIndex, e.RowIndex, e.RowValue.Index);
            string casasDecimais = dataSource.GetValue(0, dfDecimais) + "";
            string unidadeMedida = dataSource.GetValue(0, dfMedida) + "";


            if (e.DataField.FieldName.Contains("Desempenho") && (e.Value != null))
            {
                
                e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Brick.Text = "l";
                e.Brick.TextValue = "l";
                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Appearance.ForeColor = Color.Orange;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.ForeColor = Color.LightGray;
                }
                else
                {
                    e.Brick.Text = " ";
                    e.Brick.TextValue = " ";
                }
            }
            else if (e.DataField.FieldName.Equals("MetaAcumulada"))
            {
                e.Brick.Text = formataValorGrid(double.Parse((e.Value != null ? e.Value.ToString() : "0")), unidadeMedida, int.Parse(casasDecimais));
                e.Brick.TextValue = formataValorGrid(double.Parse((e.Value != null ? e.Value.ToString() : "0")), unidadeMedida, int.Parse(casasDecimais));

            }
            else if (e.DataField.FieldName.Equals("ResultadoAcumulado"))
            {
                e.Brick.Text = formataValorGrid(double.Parse((e.Value != null ? e.Value.ToString() : "0")), unidadeMedida, int.Parse(casasDecimais));
                e.Brick.TextValue = formataValorGrid(double.Parse((e.Value != null ? e.Value.ToString() : "0")), unidadeMedida, int.Parse(casasDecimais));

            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapUnidInd", pvgDadosIndicador);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapUnidInd", "OLAP Unidades Indicador", this, pvgDadosIndicador);
    }

    #endregion

    protected void pvgDadosIndicador_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
    {
        if (e.DataField == null) return;

        var fieldName = e.DataField.FieldName;
        if (fieldName.Equals("Desempenho") || fieldName.Equals("DesempenhoAcumulado"))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
        }
    }

    protected void pvgDadosIndicador_HtmlFieldValuePrepared(object sender, PivotHtmlFieldValuePreparedEventArgs e)
    {
        if (e.Field == null) return;

        var fieldName = e.Field.FieldName;
        if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value &&
            (fieldName.Equals("Desempenho") || fieldName.Equals("DesempenhoAcumulado")))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
        }
    }
}
