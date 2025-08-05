using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Linq;

/// <summary>
/// Summary description for rel_BoletimStatusBahia
/// </summary>
public class rel_BoletimStatusBahia : XtraReport
{
    #region fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private CalculatedField calculatedField1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel11;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox3;
    private XRPictureBox xrPictureBox4;
    private XRPictureBox xrPictureBox5;
    private PageFooterBand PageFooter;
    private XRSubreport xrSubreport1;
    private CalculatedField ValorCustoPrevistoMultiplicadoPorMil;
    private CalculatedField ValorCustoRealizadoMultiplicadoPorMil;
    private DataTable dataTable3;
    private DataColumn dataColumn20;
    private DataColumn dataColumn21;
    private DataColumn dataColumn22;
    private DataColumn dataColumn23;
    private DataColumn dataColumn24;
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private System.Data.DataSet ds;
    private XRPageInfo xrPageInfo1;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel5;
    private XRLabel xrlStatus;
    private XRChart xrChart3;
    private XRLabel xrLabel3;
    private XRChart xrChart2;
    private FormattingRule formattingRuleNomePrograma;
    private FormattingRule formattingRuleSubtitulosPrograma;
    private FormattingRule formattingRuleObjetivoProjeto;
    private XRLine xrLine1;
    private XRLabel xrLabel2;
    private XRLine xrLine2;
    private XRPanel xrpObjetivo;
    private XRPanel xrpDesempenho;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private DataColumn dataColumn28;
    private DataColumn dataColumn29;
    #endregion
    private DataTable dataTable1;
    private DataColumn dataColumn1;
    private DataColumn dataColumn3;
    private DataColumn dataColumn4;
    private DataColumn dataColumn5;
    private DataColumn dataColumn6;
    private DataColumn dataColumn7;
    private DataColumn dataColumn8;
    private DataColumn dataColumn9;
    private DataColumn dataColumn10;
    private DataColumn dataColumn13;
    private DataColumn dataColumn14;
    private DataColumn dataColumn15;
    private DataColumn dataColumn16;
    private DataColumn dataColumn17;
    private DataColumn dataColumn18;
    private DataColumn dataColumn19;
    private DataColumn dataColumn25;
    private DataColumn dataColumn26;
    private DataColumn dataColumn27;
    private DataTable dataTable2;
    private DataColumn dataColumn2;
    private DataColumn dataColumn11;
    private DataColumn dataColumn12;
    private DetailReportBand DetailReportEntregas;
    private DetailBand Detail1;
    private XRPanel xrpEntregas;
    private XRLabel xrlEntrega;
    private XRChart xrChart1;
    private DetailReportBand DetailReportAnaliseCritica;
    private DetailBand Detail2;
    private XRPanel xrpAnaliseCritica;
    private XRLabel lblDataAnalise;
    private XRLabel xrlAnaliseCritica;
    private XRRichText xrRichText1;
    private DetailReportBand DetailReportGraficoPizza;
    private DetailBand Detail3;
    private DetailReportBand DetailReportListagemProjetos;
    private DetailBand Detail4;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel20;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel19;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell12;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell11;
    private DataColumn dataColumn30;
    private DataColumn dataColumn31;
    private DataColumn dataColumn32;
    private DataColumn dataColumn33;
    private XRPictureBox xrPictureBox6;
    private XRPictureBox xrPictureBox7;
    private XRPictureBox xrPictureBox8;
    private DataTable dataTable4;
    private DataColumn dataColumn34;
    private DataColumn dataColumn35;
    private DataColumn dataColumn36;
    private XRChart xrChart4;


    private dados cDados = CdadosUtil.GetCdados(null);

    #region Constructors

    public rel_BoletimStatusBahia(int codigoStatusReport)
    {
        InitializeComponent();
        InitData(codigoStatusReport);
    }

    #endregion

    #region Methods
    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void DefineCoresSeriesGraficosStatus(XRChart chart)
    {
        string columnName = chart == xrChart2 ?
            "CorDesempenhoFisico" : "CorDesempenhoFinanceiro";
        string nomeCor = (string)GetCurrentColumnValue(columnName);
        Color cor = Color.Black;
        switch (nomeCor.ToLower())
        {
            case "verde":
                cor = Color.Green;
                break;
            case "vermelho":
                cor = Color.Red;
                break;
            case "amarelo":
                cor = Color.Yellow;
                break;
            case "azul":
                cor = Color.Blue;
                break;
        }
        chart.Series["SerieRealizado"].View.Color = cor;
    }

    private void InitData(int codigoStatusReport)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        xrPictureBox1.Image = logo;

        #region Comando SQL

        comandoSql = string.Format(@"exec p_rel_BoletimQuinzenal02 {0}", codigoStatusReport);

        #endregion

        string[] tableNames = new string[] { "Projeto", "DadosEntrega", "LegendaDesempenho", "DistribuicaoProjetosPorStatus" };

        DataSet dsTemp = cDados.getDataSet(comandoSql);
        ds.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);

        DefineCodigoObjetoSuperior();

        xrSubreport1.ReportSource = new rel_SubreportLegendasFisicoFinanceiro(ds.Tables["LegendaDesempenho"]);
    }

    private void DefineCodigoObjetoSuperior()
    {
        DataTable dtProjeto = ds.Tables["Projeto"];
        if (dtProjeto.Rows.Count == 1)
        {
            DataRow row = dtProjeto.AsEnumerable().Single();
            row["IndicaPrograma"] = "N";
            dtProjeto.AcceptChanges();
            DetailReportGraficoPizza.Visible = false;
            DetailReportListagemProjetos.Visible = false;
        }
    }

    //private Image ObtemLogoEntidade()
    //{
    //    int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
    //    DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");
    //    if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
    //    {
    //        byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
    //        System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
    //        Image logo = Image.FromStream(stream);
    //        return logo;
    //    }
    //    else
    //        return null;
    //}
    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_BoletimStatusBahia.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_BoletimStatusBahia.ResourceManager;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel7 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel8 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D1 = new DevExpress.XtraCharts.SimpleDiagram3D();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel1 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView1 = new DevExpress.XtraCharts.Pie3DSeriesView();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel2 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView2 = new DevExpress.XtraCharts.Pie3DSeriesView();
        this.dataTable1 = new System.Data.DataTable();
        this.dataColumn1 = new System.Data.DataColumn();
        this.dataColumn3 = new System.Data.DataColumn();
        this.dataColumn4 = new System.Data.DataColumn();
        this.dataColumn5 = new System.Data.DataColumn();
        this.dataColumn6 = new System.Data.DataColumn();
        this.dataColumn7 = new System.Data.DataColumn();
        this.dataColumn8 = new System.Data.DataColumn();
        this.dataColumn9 = new System.Data.DataColumn();
        this.dataColumn10 = new System.Data.DataColumn();
        this.dataColumn13 = new System.Data.DataColumn();
        this.dataColumn14 = new System.Data.DataColumn();
        this.dataColumn15 = new System.Data.DataColumn();
        this.dataColumn16 = new System.Data.DataColumn();
        this.dataColumn17 = new System.Data.DataColumn();
        this.dataColumn18 = new System.Data.DataColumn();
        this.dataColumn19 = new System.Data.DataColumn();
        this.dataColumn25 = new System.Data.DataColumn();
        this.dataColumn26 = new System.Data.DataColumn();
        this.dataColumn27 = new System.Data.DataColumn();
        this.dataColumn29 = new System.Data.DataColumn();
        this.dataColumn30 = new System.Data.DataColumn();
        this.dataColumn31 = new System.Data.DataColumn();
        this.dataColumn32 = new System.Data.DataColumn();
        this.dataColumn33 = new System.Data.DataColumn();
        this.dataTable2 = new System.Data.DataTable();
        this.dataColumn2 = new System.Data.DataColumn();
        this.dataColumn11 = new System.Data.DataColumn();
        this.dataColumn12 = new System.Data.DataColumn();
        this.dataColumn28 = new System.Data.DataColumn();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrpDesempenho = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrlStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.formattingRuleSubtitulosPrograma = new DevExpress.XtraReports.UI.FormattingRule();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox5 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.ds = new System.Data.DataSet();
        this.dataTable3 = new System.Data.DataTable();
        this.dataColumn20 = new System.Data.DataColumn();
        this.dataColumn21 = new System.Data.DataColumn();
        this.dataColumn22 = new System.Data.DataColumn();
        this.dataColumn23 = new System.Data.DataColumn();
        this.dataColumn24 = new System.Data.DataColumn();
        this.dataTable4 = new System.Data.DataTable();
        this.dataColumn34 = new System.Data.DataColumn();
        this.dataColumn35 = new System.Data.DataColumn();
        this.dataColumn36 = new System.Data.DataColumn();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.formattingRuleNomePrograma = new DevExpress.XtraReports.UI.FormattingRule();
        this.xrpObjetivo = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.formattingRuleObjetivoProjeto = new DevExpress.XtraReports.UI.FormattingRule();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
        this.ValorCustoPrevistoMultiplicadoPorMil = new DevExpress.XtraReports.UI.CalculatedField();
        this.ValorCustoRealizadoMultiplicadoPorMil = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReportEntregas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrpEntregas = new DevExpress.XtraReports.UI.XRPanel();
        this.xrlEntrega = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.DetailReportAnaliseCritica = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrpAnaliseCritica = new DevExpress.XtraReports.UI.XRPanel();
        this.lblDataAnalise = new DevExpress.XtraReports.UI.XRLabel();
        this.xrlAnaliseCritica = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.DetailReportGraficoPizza = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportListagemProjetos = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox6 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox8 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // dataTable1
        // 
        this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33});
        this.dataTable1.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "CodigoObjeto"}, false),
            new System.Data.UniqueConstraint("Constraint2", new string[] {
                        "CodigoObjeto",
                        "CodigoTipoAssociacaoObjeto"}, false)});
        this.dataTable1.TableName = "Projeto";
        // 
        // dataColumn1
        // 
        this.dataColumn1.ColumnName = "CodigoObjeto";
        this.dataColumn1.DataType = typeof(int);
        // 
        // dataColumn3
        // 
        this.dataColumn3.ColumnName = "NomeObjeto";
        // 
        // dataColumn4
        // 
        this.dataColumn4.ColumnName = "IndicaPrograma";
        // 
        // dataColumn5
        // 
        this.dataColumn5.ColumnName = "PercentualFisicoPrevisto";
        this.dataColumn5.DataType = typeof(decimal);
        // 
        // dataColumn6
        // 
        this.dataColumn6.ColumnName = "PercentualFisicoRealizado";
        this.dataColumn6.DataType = typeof(decimal);
        // 
        // dataColumn7
        // 
        this.dataColumn7.ColumnName = "ValorCustoPrevisto";
        this.dataColumn7.DataType = typeof(decimal);
        // 
        // dataColumn8
        // 
        this.dataColumn8.ColumnName = "ValorCustoRealizado";
        this.dataColumn8.DataType = typeof(decimal);
        // 
        // dataColumn9
        // 
        this.dataColumn9.ColumnName = "DescricaoObjetivoPrincipal";
        // 
        // dataColumn10
        // 
        this.dataColumn10.ColumnName = "AnaliseCritica";
        // 
        // dataColumn13
        // 
        this.dataColumn13.ColumnName = "CorDesempenhoFisico";
        // 
        // dataColumn14
        // 
        this.dataColumn14.ColumnName = "CorDesempenhoFinanceiro";
        // 
        // dataColumn15
        // 
        this.dataColumn15.ColumnName = "DataInicioPeriodoRelatorio";
        this.dataColumn15.DataType = typeof(System.DateTime);
        // 
        // dataColumn16
        // 
        this.dataColumn16.ColumnName = "DataTerminoPeriodoRelatorio";
        this.dataColumn16.DataType = typeof(System.DateTime);
        // 
        // dataColumn17
        // 
        this.dataColumn17.ColumnName = "ResultadoFisico";
        // 
        // dataColumn18
        // 
        this.dataColumn18.ColumnName = "ResultadoFinanceiro";
        // 
        // dataColumn19
        // 
        this.dataColumn19.ColumnName = "DataEmissaoRelatorio";
        this.dataColumn19.DataType = typeof(System.DateTime);
        // 
        // dataColumn25
        // 
        this.dataColumn25.ColumnName = "DesvioFisico";
        this.dataColumn25.DataType = typeof(decimal);
        // 
        // dataColumn26
        // 
        this.dataColumn26.ColumnName = "DesvioCusto";
        this.dataColumn26.DataType = typeof(decimal);
        // 
        // dataColumn27
        // 
        this.dataColumn27.ColumnName = "CodigoTipoAssociacaoObjeto";
        this.dataColumn27.DataType = typeof(int);
        // 
        // dataColumn29
        // 
        this.dataColumn29.ColumnName = "DataAnalisePerformance";
        this.dataColumn29.DataType = typeof(System.DateTime);
        // 
        // dataColumn30
        // 
        this.dataColumn30.ColumnName = "NomeGerenteProjeto";
        // 
        // dataColumn31
        // 
        this.dataColumn31.ColumnName = "CodigoStatus";
        this.dataColumn31.DataType = typeof(int);
        // 
        // dataColumn32
        // 
        this.dataColumn32.ColumnName = "DescricaoStatus";
        // 
        // dataColumn33
        // 
        this.dataColumn33.ColumnName = "CorDesempenhoGeral";
        // 
        // dataTable2
        // 
        this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn28});
        this.dataTable2.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Relation_Projeto_DadosEntrega", "Projeto", new string[] {
                        "CodigoObjeto"}, new string[] {
                        "CodigoObjeto"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade),
            new System.Data.ForeignKeyConstraint("Constraint1", "Projeto", new string[] {
                        "CodigoObjeto",
                        "CodigoTipoAssociacaoObjeto"}, new string[] {
                        "CodigoObjeto",
                        "CodigoTipoAssociacaoObjeto"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dataTable2.TableName = "DadosEntrega";
        // 
        // dataColumn2
        // 
        this.dataColumn2.ColumnName = "CodigoObjeto";
        this.dataColumn2.DataType = typeof(int);
        // 
        // dataColumn11
        // 
        this.dataColumn11.ColumnName = "DescricaoSituacaoEntregas";
        // 
        // dataColumn12
        // 
        this.dataColumn12.ColumnName = "QuantidadeEntregas";
        this.dataColumn12.DataType = typeof(int);
        // 
        // dataColumn28
        // 
        this.dataColumn28.ColumnName = "CodigoTipoAssociacaoObjeto";
        this.dataColumn28.DataType = typeof(int);
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrpDesempenho,
            this.xrLabel3,
            this.xrpObjetivo});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 750F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("IndicaPrograma", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending),
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrpDesempenho
        // 
        this.xrpDesempenho.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrLabel17,
            this.xrlStatus,
            this.xrPictureBox4,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel15,
            this.xrLabel16,
            this.xrPictureBox2,
            this.xrPictureBox3,
            this.xrLabel7,
            this.xrPictureBox5,
            this.xrChart2,
            this.xrChart3});
        this.xrpDesempenho.Dpi = 254F;
        this.xrpDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(0F, 230F);
        this.xrpDesempenho.Name = "xrpDesempenho";
        this.xrpDesempenho.SizeF = new System.Drawing.SizeF(1800F, 500F);
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 445F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "Desvio:";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(950F, 445.0001F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(214.3959F, 50.00006F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Desvio:";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrlStatus
        // 
        this.xrlStatus.Dpi = 254F;
        this.xrlStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlStatus.FormattingRules.Add(this.formattingRuleSubtitulosPrograma);
        this.xrlStatus.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrlStatus.Name = "xrlStatus";
        this.xrlStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlStatus.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrlStatus.StylePriority.UseFont = false;
        this.xrlStatus.StylePriority.UseForeColor = false;
        this.xrlStatus.Text = "Desempenho:";
        // 
        // formattingRuleSubtitulosPrograma
        // 
        this.formattingRuleSubtitulosPrograma.Condition = "[IndicaPrograma] == \'S\'";
        this.formattingRuleSubtitulosPrograma.DataMember = "Projeto";
        // 
        // 
        // 
        this.formattingRuleSubtitulosPrograma.Formatting.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.formattingRuleSubtitulosPrograma.Formatting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
        this.formattingRuleSubtitulosPrograma.Name = "formattingRuleSubtitulosPrograma";
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 345F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 394.9999F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Realizado:";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.PercentualFisicoPrevisto", "{0:n0} %")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(274.9792F, 345.0001F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(625.0209F, 50F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "xrLabel9";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.PercentualFisicoRealizado", "{0:n0} %")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(274.9792F, 394.9999F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(625.0209F, 50F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "xrLabel9";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DesvioFisico", "{0:n0} %")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(274.9792F, 445.0001F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(625.0209F, 50.00006F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "xrLabel9";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel11_BeforePrint);
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(950.0001F, 345.0001F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(214.3959F, 50F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Previsto:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(950.0001F, 394.9999F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(214.3959F, 50F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Realizado:";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.ValorCustoPrevistoMultiplicadoPorMil", "{0:n0}")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1164.396F, 345.0001F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(635.604F, 50F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "xrLabel14";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.ValorCustoRealizadoMultiplicadoPorMil", "{0:n0}")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(1164.396F, 394.9999F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(635.604F, 49.99994F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "xrLabel15";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DesvioCusto", "{0:n0} %")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(1164.396F, 445.0001F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(635.604F, 50.00006F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "xrLabel9";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel16.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel11_BeforePrint);
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(900.0001F, 345.0001F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", this.dataTable1, "CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(900.0001F, 394.9999F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 344.9999F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Previsto:";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox5
        // 
        this.xrPictureBox5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", this.dataTable1, "CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.xrPictureBox5.Dpi = 254F;
        this.xrPictureBox5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 395F);
        this.xrPictureBox5.Name = "xrPictureBox5";
        this.xrPictureBox5.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrChart2
        // 
        this.xrChart2.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "Projeto";
        this.xrChart2.DataSource = this.ds;
        xyDiagram1.AxisX.Label.Visible = false;
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram1.AxisX.Title.Text = "Físico (%)";
        xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisualRange.Auto = false;
        xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram1.AxisY.WholeRange.Auto = false;
        xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram1.Rotated = true;
        this.xrChart2.Diagram = xyDiagram1;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
        this.xrChart2.Name = "xrChart2";
        series1.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.Label = sideBySideBarSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Name = "SerieRealizado";
        series1.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series2.Label = sideBySideBarSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.Name = "SeriePrevisto";
        series2.ValueDataMembersSerializable = "PercentualFisicoPrevisto";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series2.View = sideBySideBarSeriesView2;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart2.SizeF = new System.Drawing.SizeF(900F, 250F);
        this.xrChart2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // ds
        // 
        this.ds.DataSetName = "NewDataSet";
        this.ds.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("Relation_Projeto_DadosEntrega", "Projeto", "DadosEntrega", new string[] {
                        "CodigoObjeto",
                        "CodigoTipoAssociacaoObjeto"}, new string[] {
                        "CodigoObjeto",
                        "CodigoTipoAssociacaoObjeto"}, false)});
        this.ds.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable4});
        // 
        // dataTable3
        // 
        this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24});
        this.dataTable3.TableName = "LegendaDesempenho";
        // 
        // dataColumn20
        // 
        this.dataColumn20.ColumnName = "IDRegistro";
        this.dataColumn20.DataType = typeof(int);
        // 
        // dataColumn21
        // 
        this.dataColumn21.ColumnName = "CorFaixaFisico";
        // 
        // dataColumn22
        // 
        this.dataColumn22.ColumnName = "DescricaoFaixaFisico";
        // 
        // dataColumn23
        // 
        this.dataColumn23.ColumnName = "CorFaixaFinanceiro";
        // 
        // dataColumn24
        // 
        this.dataColumn24.ColumnName = "DescricaoFaixaFinanceiro";
        // 
        // dataTable4
        // 
        this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36});
        this.dataTable4.TableName = "DistribuicaoProjetosPorStatus";
        // 
        // dataColumn34
        // 
        this.dataColumn34.ColumnName = "CodigoStatus";
        this.dataColumn34.DataType = typeof(int);
        // 
        // dataColumn35
        // 
        this.dataColumn35.ColumnName = "DescricaoStatus";
        // 
        // dataColumn36
        // 
        this.dataColumn36.ColumnName = "QuantidadeProjetos";
        this.dataColumn36.DataType = typeof(int);
        // 
        // xrChart3
        // 
        this.xrChart3.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart3.DataMember = "Projeto";
        this.xrChart3.DataSource = this.ds;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram2.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.Label.Angle = 15;
        xyDiagram2.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisY.Label.TextPattern = "{V:N0} K";
        xyDiagram2.AxisY.MinorCount = 1;
        xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram2.Rotated = true;
        this.xrChart3.Diagram = xyDiagram2;
        this.xrChart3.Dpi = 254F;
        this.xrChart3.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(900F, 60F);
        this.xrChart3.Name = "xrChart3";
        series3.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series3.Label = sideBySideBarSeriesLabel4;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Name = "SerieRealizado";
        series3.ValueDataMembersSerializable = "ValorCustoRealizado";
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series3.View = sideBySideBarSeriesView3;
        series4.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series4.Label = sideBySideBarSeriesLabel5;
        series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.Name = "SeriePrevisto";
        series4.ValueDataMembersSerializable = "ValorCustoPrevisto";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series4.View = sideBySideBarSeriesView4;
        this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart3.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart3.SizeF = new System.Drawing.SizeF(900F, 250F);
        this.xrChart3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.NomeObjeto")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.FormattingRules.Add(this.formattingRuleNomePrograma);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // formattingRuleNomePrograma
        // 
        this.formattingRuleNomePrograma.Condition = "[IndicaPrograma] == \'S\'";
        this.formattingRuleNomePrograma.DataMember = "Projeto";
        // 
        // 
        // 
        this.formattingRuleNomePrograma.Formatting.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.formattingRuleNomePrograma.Formatting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
        this.formattingRuleNomePrograma.Name = "formattingRuleNomePrograma";
        // 
        // xrpObjetivo
        // 
        this.xrpObjetivo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel2});
        this.xrpObjetivo.Dpi = 254F;
        this.xrpObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 90F);
        this.xrpObjetivo.Name = "xrpObjetivo";
        this.xrpObjetivo.SizeF = new System.Drawing.SizeF(1800F, 120F);
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.Text = "Objetivo:";
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DescricaoObjetivoPrincipal")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.99997F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // formattingRuleObjetivoProjeto
        // 
        this.formattingRuleObjetivoProjeto.Condition = "[IndicaPrograma] == \'S\'";
        this.formattingRuleObjetivoProjeto.DataMember = "Projeto";
        // 
        // 
        // 
        this.formattingRuleObjetivoProjeto.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
        this.formattingRuleObjetivoProjeto.Name = "formattingRuleObjetivoProjeto";
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 100F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "{0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1546F, 41.58F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 99F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLine1,
            this.xrLabel1,
            this.xrPictureBox1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 300F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DataEmissaoRelatorio", "Emissão: {0:dd/MM/yyyy HH:mm:ss}")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1120F, 225F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(671.0002F, 50F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 275F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 10F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 225F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(830F, 50F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "BOLETIM DE STATUS";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(400F, 200F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // calculatedField1
        // 
        this.calculatedField1.DisplayName = "Quinzena";
        this.calculatedField1.Expression = "Iif(GetDay([DataEmissaoRelatorio])<=15, 1 , 2)";
        this.calculatedField1.FieldType = DevExpress.XtraReports.UI.FieldType.Int32;
        this.calculatedField1.Name = "calculatedField1";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrSubreport1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 60F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1800F, 10F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrSubreport1
        // 
        this.xrSubreport1.Dpi = 254F;
        this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrSubreport1.Name = "xrSubreport1";
        this.xrSubreport1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        // 
        // ValorCustoPrevistoMultiplicadoPorMil
        // 
        this.ValorCustoPrevistoMultiplicadoPorMil.Expression = "Iif(IsNull([ValorCustoPrevisto]),  0,[ValorCustoPrevisto]*1000)";
        this.ValorCustoPrevistoMultiplicadoPorMil.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.ValorCustoPrevistoMultiplicadoPorMil.Name = "ValorCustoPrevistoMultiplicadoPorMil";
        // 
        // ValorCustoRealizadoMultiplicadoPorMil
        // 
        this.ValorCustoRealizadoMultiplicadoPorMil.Expression = "Iif(IsNull([ValorCustoRealizado]),  0, [ValorCustoRealizado]*1000)";
        this.ValorCustoRealizadoMultiplicadoPorMil.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.ValorCustoRealizadoMultiplicadoPorMil.Name = "ValorCustoRealizadoMultiplicadoPorMil";
        // 
        // DetailReportEntregas
        // 
        this.DetailReportEntregas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
        this.DetailReportEntregas.Dpi = 254F;
        this.DetailReportEntregas.Level = 2;
        this.DetailReportEntregas.Name = "DetailReportEntregas";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrpEntregas});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 570F;
        this.Detail1.Name = "Detail1";
        // 
        // xrpEntregas
        // 
        this.xrpEntregas.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlEntrega,
            this.xrChart1});
        this.xrpEntregas.Dpi = 254F;
        this.xrpEntregas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrpEntregas.Name = "xrpEntregas";
        this.xrpEntregas.Scripts.OnAfterPrint = "xrpEntregas_AfterPrint";
        this.xrpEntregas.SizeF = new System.Drawing.SizeF(1800F, 570F);
        // 
        // xrlEntrega
        // 
        this.xrlEntrega.Dpi = 254F;
        this.xrlEntrega.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlEntrega.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlEntrega.FormattingRules.Add(this.formattingRuleSubtitulosPrograma);
        this.xrlEntrega.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrlEntrega.Name = "xrlEntrega";
        this.xrlEntrega.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlEntrega.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrlEntrega.StylePriority.UseFont = false;
        this.xrlEntrega.StylePriority.UseForeColor = false;
        this.xrlEntrega.Text = "Entregas:";
        // 
        // xrChart1
        // 
        this.xrChart1.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "Projeto.Relation_Projeto_DadosEntrega";
        this.xrChart1.DataSource = this.ds;
        xyDiagram3.AxisX.Label.Angle = 15;
        xyDiagram3.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram3.AxisX.MinorCount = 1;
        xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
        this.xrChart1.Diagram = xyDiagram3;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
        this.xrChart1.Name = "xrChart1";
        this.xrChart1.PaletteName = "Palette 1";
        this.xrChart1.PaletteRepository.Add("Palette 1", new DevExpress.XtraCharts.Palette("Palette 1", DevExpress.XtraCharts.PaletteScaleMode.Repeat, new DevExpress.XtraCharts.PaletteEntry[] {
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(157)))), ((int)(((byte)(49))))), System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(157)))), ((int)(((byte)(49)))))),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(106)))), ((int)(((byte)(66))))), System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(106)))), ((int)(((byte)(66)))))),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(196)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(196)))), ((int)(((byte)(0)))))),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Yellow, System.Drawing.Color.Yellow),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Red, System.Drawing.Color.Red)}));
        series5.ArgumentDataMember = "DescricaoSituacaoEntregas";
        sideBySideBarSeriesLabel7.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel7.ShowForZeroValues = true;
        series5.Label = sideBySideBarSeriesLabel7;
        series5.Name = "Series 1";
        series5.ValueDataMembersSerializable = "QuantidadeEntregas";
        sideBySideBarSeriesView5.ColorEach = true;
        series5.View = sideBySideBarSeriesView5;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
        sideBySideBarSeriesLabel8.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel8;
        this.xrChart1.SizeF = new System.Drawing.SizeF(1800F, 500F);
        this.xrChart1.StylePriority.UsePadding = false;
        // 
        // DetailReportAnaliseCritica
        // 
        this.DetailReportAnaliseCritica.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.DetailReportAnaliseCritica.Dpi = 254F;
        this.DetailReportAnaliseCritica.Level = 3;
        this.DetailReportAnaliseCritica.Name = "DetailReportAnaliseCritica";
        this.DetailReportAnaliseCritica.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrpAnaliseCritica});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 200F;
        this.Detail2.Name = "Detail2";
        // 
        // xrpAnaliseCritica
        // 
        this.xrpAnaliseCritica.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDataAnalise,
            this.xrlAnaliseCritica,
            this.xrRichText1});
        this.xrpAnaliseCritica.Dpi = 254F;
        this.xrpAnaliseCritica.KeepTogether = false;
        this.xrpAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrpAnaliseCritica.Name = "xrpAnaliseCritica";
        this.xrpAnaliseCritica.SizeF = new System.Drawing.SizeF(1800F, 180F);
        // 
        // lblDataAnalise
        // 
        this.lblDataAnalise.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DataAnalisePerformance", "Data desta Análise Crítica: {0:dd/MM/yyyy}")});
        this.lblDataAnalise.Dpi = 254F;
        this.lblDataAnalise.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataAnalise.LocationFloat = new DevExpress.Utils.PointFloat(0F, 141.9F);
        this.lblDataAnalise.Name = "lblDataAnalise";
        this.lblDataAnalise.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataAnalise.SizeF = new System.Drawing.SizeF(762F, 38.1F);
        this.lblDataAnalise.StylePriority.UseFont = false;
        this.lblDataAnalise.Text = "lblDataAnalise";
        // 
        // xrlAnaliseCritica
        // 
        this.xrlAnaliseCritica.Dpi = 254F;
        this.xrlAnaliseCritica.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlAnaliseCritica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlAnaliseCritica.FormattingRules.Add(this.formattingRuleSubtitulosPrograma);
        this.xrlAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrlAnaliseCritica.Name = "xrlAnaliseCritica";
        this.xrlAnaliseCritica.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlAnaliseCritica.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrlAnaliseCritica.StylePriority.UseFont = false;
        this.xrlAnaliseCritica.StylePriority.UseForeColor = false;
        this.xrlAnaliseCritica.Text = "Análise Crítica:";
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projeto.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrRichText1.StylePriority.UseFont = false;
        this.xrRichText1.StylePriority.UseTextAlignment = false;
        // 
        // DetailReportGraficoPizza
        // 
        this.DetailReportGraficoPizza.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader1});
        this.DetailReportGraficoPizza.Dpi = 254F;
        this.DetailReportGraficoPizza.Level = 0;
        this.DetailReportGraficoPizza.Name = "DetailReportGraficoPizza";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart4});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 508F;
        this.Detail3.Name = "Detail3";
        // 
        // xrChart4
        // 
        this.xrChart4.BackColor = System.Drawing.Color.White;
        this.xrChart4.BorderColor = System.Drawing.Color.Black;
        this.xrChart4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart4.DataMember = "DistribuicaoProjetosPorStatus";
        this.xrChart4.DataSource = this.ds;
        simpleDiagram3D1.HorizontalScrollPercent = 3.05958132045088D;
        simpleDiagram3D1.RotationMatrixSerializable = "0.881262909994873;-0.293841398668131;-0.37017957249979;0;0.221823031025316;0.9487" +
"65172939442;-0.225031530066211;0;0.417337065660497;0.116197586231188;0.901292346" +
"899685;0;0;0;0;1";
        simpleDiagram3D1.VerticalScrollPercent = 6.5934065934066D;
        this.xrChart4.Diagram = simpleDiagram3D1;
        this.xrChart4.Dpi = 254F;
        this.xrChart4.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrChart4.Name = "xrChart4";
        series6.ArgumentDataMember = "DescricaoStatus";
        pie3DSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pie3DSeriesLabel1.TextPattern = "{V:N0}";
        series6.Label = pie3DSeriesLabel1;
        series6.LegendTextPattern = "{A}";
        series6.Name = "Series 1";
        series6.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
        series6.ValueDataMembersSerializable = "QuantidadeProjetos";
        series6.View = pie3DSeriesView1;
        this.xrChart4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series6};
        pie3DSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart4.SeriesTemplate.Label = pie3DSeriesLabel2;
        this.xrChart4.SeriesTemplate.View = pie3DSeriesView2;
        this.xrChart4.SizeF = new System.Drawing.SizeF(1800F, 508F);
        this.xrChart4.StylePriority.UseBackColor = false;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 50F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel20
        // 
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel20.FormattingRules.Add(this.formattingRuleSubtitulosPrograma);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseForeColor = false;
        this.xrLabel20.Text = "Distribuição dos Projetos por Status:";
        // 
        // DetailReportListagemProjetos
        // 
        this.DetailReportListagemProjetos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader2});
        this.DetailReportListagemProjetos.DataMember = "Projeto";
        this.DetailReportListagemProjetos.DataSource = this.ds;
        this.DetailReportListagemProjetos.Dpi = 254F;
        this.DetailReportListagemProjetos.Expanded = false;
        this.DetailReportListagemProjetos.FilterString = "[IndicaPrograma] <> \'S\'";
        this.DetailReportListagemProjetos.Level = 1;
        this.DetailReportListagemProjetos.Name = "DetailReportListagemProjetos";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 50F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell8,
            this.xrTableCell5,
            this.xrTableCell10,
            this.xrTableCell6,
            this.xrTableCell12});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.NomeObjeto")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 1.33333333333333D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.NomeGerenteProjeto")});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Weight = 0.708333333333333D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox6});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell5.Weight = 0.175D;
        // 
        // xrPictureBox6
        // 
        this.xrPictureBox6.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", this.dataTable1, "CorDesempenhoGeral", "~/imagens/{0}.gif")});
        this.xrPictureBox6.Dpi = 254F;
        this.xrPictureBox6.LocationFloat = new DevExpress.Utils.PointFloat(30F, 2.5F);
        this.xrPictureBox6.Name = "xrPictureBox6";
        this.xrPictureBox6.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox6.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox6.StylePriority.UseBorders = false;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox7});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.175D;
        // 
        // xrPictureBox7
        // 
        this.xrPictureBox7.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", this.dataTable1, "CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.xrPictureBox7.Dpi = 254F;
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(30F, 2.5F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox7.StylePriority.UseBorders = false;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox8});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.175D;
        // 
        // xrPictureBox8
        // 
        this.xrPictureBox8.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", this.dataTable1, "CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox8.Dpi = 254F;
        this.xrPictureBox8.LocationFloat = new DevExpress.Utils.PointFloat(30F, 2.5F);
        this.xrPictureBox8.Name = "xrPictureBox8";
        this.xrPictureBox8.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox8.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox8.StylePriority.UseBorders = false;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projeto.DescricaoStatus")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Weight = 0.433333333333333D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLabel19});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.HeightF = 100F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell7,
            this.xrTableCell2,
            this.xrTableCell9,
            this.xrTableCell3,
            this.xrTableCell11});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Descrição";
        this.xrTableCell1.Weight = 1.33333333333333D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Gestor do Projeto";
        this.xrTableCell7.Weight = 0.708333333333333D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Geral";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell2.Weight = 0.175D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Físico";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.175D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Custo";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell3.Weight = 0.175D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "Status";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell11.Weight = 0.433333333333333D;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
        this.xrLabel19.FormattingRules.Add(this.formattingRuleSubtitulosPrograma);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseForeColor = false;
        this.xrLabel19.Text = "Listagem de Projetos:";
        // 
        // rel_BoletimStatusBahia
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReportEntregas,
            this.DetailReportAnaliseCritica,
            this.DetailReportGraficoPizza,
            this.DetailReportListagemProjetos});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.ValorCustoPrevistoMultiplicadoPorMil,
            this.ValorCustoRealizadoMultiplicadoPorMil});
        this.DataMember = "Projeto";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRuleNomePrograma,
            this.formattingRuleSubtitulosPrograma,
            this.formattingRuleObjetivoProjeto});
        this.Margins = new System.Drawing.Printing.Margins(150, 150, 100, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        bool indicaPrograma = "S" == GetCurrentColumnValue("IndicaPrograma").ToString();

        if (indicaPrograma)
        {
            if (Detail.Controls.Contains(xrpObjetivo))
                Detail.Controls.Remove(xrpObjetivo);

            xrpDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(0, 90);
            //xrpEntregas.LocationFloat = new DevExpress.Utils.PointFloat(0, 610);
            //xrpAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0, 1200);

            Detail.HeightF = 610;// 1360;
        }
        else
        {
            if (!Detail.Controls.Contains(xrpObjetivo))
                Detail.Controls.Add(xrpObjetivo);

            xrpDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(0, 230);
            //xrpEntregas.LocationFloat = new DevExpress.Utils.PointFloat(0, 750);
            //xrpAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0, 1340);

            Detail.HeightF = 750;// 1500;
        }
        DetailReportGraficoPizza.Visible = indicaPrograma;
        DetailReportListagemProjetos.Visible = indicaPrograma;

        DefineCoresSeriesGraficosStatus(xrChart2);
        DefineCoresSeriesGraficosStatus(xrChart3);
    }

    private void chart_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        int codigoObjeto = Convert.ToInt32(GetCurrentColumnValue("CodigoObjeto").ToString());
        foreach (DevExpress.XtraCharts.Series serie in chart.Series)
        {
            serie.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
                new DevExpress.XtraCharts.DataFilter("CodigoObjeto", "System.Int32", DevExpress.XtraCharts.DataFilterCondition.Equal, codigoObjeto)});
        }
    }

    private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        if (label.Text.Equals(" %"))
            label.Text = "- %";
    }

    private void xrLabel4_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRRichText richText = (XRRichText)sender;
        String value = (e.Value as string) ?? string.Empty;
        if (String.IsNullOrEmpty(value.Trim()))
        {
            richText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            e.Value = "[Sem análise crítica para o período]";
            lblDataAnalise.Visible = false;
        }
        else
        {
            lblDataAnalise.Visible = true;
        }
    }

    #endregion

}
