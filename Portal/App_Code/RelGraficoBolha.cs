using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// Summary description for RelGraficoBolha
/// </summary>
public class RelGraficoBolha : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DsGraficoBolha dsGraficoBolha1;
    public DevExpress.XtraReports.Parameters.Parameter ParamCodigoStatusReport;
    private ReportHeaderBand ReportHeader;
    private XRChart xrChart1;
    private GroupHeaderBand GroupHeader1;
    private GroupFooterBand GroupFooter1;
    private DevExpress.XtraReports.Parameters.Parameter ParamNomeObjeto;
    private XRPanel xrPanel1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell16;
    private XRCrossBandBox xrCrossBandBox1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRLine xrLine2;
    private XRLine xrLine1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel5;
    private DevExpress.XtraReports.Parameters.Parameter ParamPeriodo;
    private DevExpress.XtraReports.Parameters.Parameter ParamDataEmissao;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRPanel xrPanel2;
    private XRLabel xrLabel11;
    private DevExpress.XtraReports.Parameters.Parameter ParamAnaliseCriticaEscritorioProjetos;
    private XRLabel xrLabel10;
    private CalculatedField cfTotalDesvioCusto;
    private CalculatedField cfPercentualFisicoPrevisto;
    private CalculatedField cfPercentualFisicoRealizado;
    private CalculatedField cfDesvioFisico;
    private CalculatedField cfDesvioCusto;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox3;
    private CalculatedField cfPertualRelativoAoTodo;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public RelGraficoBolha()
    {
        InitializeComponent();
        DefineLogoEntitade();
    }

    private void DefineLogoEntitade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        xrPictureBox1.Image = cDados.ObtemLogoEntidade();
    }

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

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "RelGraficoBolha.resx";
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
        DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.BubbleSeriesLabel bubbleSeriesLabel1 = new DevExpress.XtraCharts.BubbleSeriesLabel();
        DevExpress.XtraCharts.BubbleSeriesView bubbleSeriesView1 = new DevExpress.XtraCharts.BubbleSeriesView();
        DevExpress.XtraCharts.BubbleSeriesLabel bubbleSeriesLabel2 = new DevExpress.XtraCharts.BubbleSeriesLabel();
        DevExpress.XtraCharts.BubbleSeriesView bubbleSeriesView2 = new DevExpress.XtraCharts.BubbleSeriesView();
        DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsGraficoBolha1 = new DsGraficoBolha();
        this.ParamCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.ParamNomeObjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.ParamDataEmissao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.ParamPeriodo = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.ParamAnaliseCriticaEscritorioProjetos = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
        this.cfTotalDesvioCusto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfPercentualFisicoPrevisto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfPercentualFisicoRealizado = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDesvioFisico = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDesvioCusto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfPertualRelativoAoTodo = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsGraficoBolha1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 50F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(3F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(2760F, 50F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.Legenda")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "Legenda";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell15.Weight = 0.151515151515152D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.NomeObjeto")});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Projeto";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell17.Weight = 0.346320346320346D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Weight = 0.124999997558824D;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "GraficoBolha.CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(105F, 45F);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox3});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Weight = 0.126082253523428D;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "GraficoBolha.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(105F, 45F);
        this.xrPictureBox3.StylePriority.UseBorders = false;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.cfPercentualFisicoPrevisto", "{0:p2}")});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "Percentual Previsto para o período";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell20.Weight = 0.201086954439559D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.cfPercentualFisicoRealizado", "{0:p2}")});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBackColor = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "Percentual Realizado* para o período";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell21.Weight = 0.201086954439559D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.cfDesvioFisico", "{0:p2}")});
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBackColor = false;
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.Text = "Desvio no perido";
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell22.Weight = 0.1660079093027D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorOrcamento", "{0:c2}")});
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBackColor = false;
        this.xrTableCell23.Text = "Orçamento Total do Projeto";
        this.xrTableCell23.Weight = 0.324675324675325D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.PercentualRelativoAoTodo", "{0:p0}")});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBackColor = false;
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.Text = "% do Projeto na Carteira da DIRET";
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell24.Weight = 0.217391297598691D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorCustoPrevisto", "{0:c2}")});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBackColor = false;
        this.xrTableCell25.Text = "Previsto para o período";
        this.xrTableCell25.Weight = 0.288043471511735D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorCustoRealizado", "{0:c2}")});
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBackColor = false;
        this.xrTableCell26.Text = "Realizado* acumulado até o período";
        this.xrTableCell26.Weight = 0.288043471511735D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.cfDesvioCusto", "{0:p0}")});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBackColor = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "Desvio no período";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell27.Weight = 0.153491456347537D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ComentarioGeral")});
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Text = "PONTOS DE ATENÇÂO";
        this.xrTableCell28.Weight = 0.411255411255411D;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 99F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 99F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dsGraficoBolha1
        // 
        this.dsGraficoBolha1.DataSetName = "DsGraficoBolha";
        this.dsGraficoBolha1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // ParamCodigoStatusReport
        // 
        this.ParamCodigoStatusReport.Name = "ParamCodigoStatusReport";
        this.ParamCodigoStatusReport.Type = typeof(short);
        this.ParamCodigoStatusReport.ValueInfo = "0";
        this.ParamCodigoStatusReport.Visible = false;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrPanel1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 1900F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ParamNomeObjeto, "Text", "")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(2770F, 75F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // ParamNomeObjeto
        // 
        this.ParamNomeObjeto.Name = "ParamNomeObjeto";
        this.ParamNomeObjeto.Visible = false;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel2,
            this.xrLabel1,
            this.xrChart1});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(2770F, 1650F);
        this.xrPanel1.StylePriority.UseBackColor = false;
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1385F, 1565F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 10, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1375F, 80F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Fonte: Sistema Integrado de Gestão de Projetos";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 1565F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 10, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1375F, 80F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UsePadding = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "As bolhas demonstram os desvios físicos e financeiros em relação ao alvo (0%). A " +
"dimensão da bolha refere-se ao orçamento da iniciativa.";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 1525F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(250F, 40F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Sobre o gráfico:";
        // 
        // xrChart1
        // 
        this.xrChart1.BorderColor = System.Drawing.Color.Black;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "GraficoBolha";
        this.xrChart1.DataSource = this.dsGraficoBolha1;
        xyDiagram1.AxisX.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        constantLine1.AxisValueSerializable = "0";
        constantLine1.Color = System.Drawing.Color.Black;
        constantLine1.Name = "Constant Line 1";
        constantLine1.ShowBehind = true;
        constantLine1.Title.Visible = false;
        xyDiagram1.AxisX.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
        xyDiagram1.AxisX.Interlaced = true;
        xyDiagram1.AxisX.Label.TextPattern = "{A:G}%";
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        xyDiagram1.AxisX.Title.Text = "Desempenho Físico";
        xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisX.VisualRange.Auto = false;
        xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "25";
        xyDiagram1.AxisX.VisualRange.MinValueSerializable = "-25";
        xyDiagram1.AxisX.WholeRange.Auto = false;
        xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "25";
        xyDiagram1.AxisX.WholeRange.MinValueSerializable = "-25";
        xyDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
        xyDiagram1.AxisY.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        constantLine2.AxisValueSerializable = "0";
        constantLine2.Color = System.Drawing.Color.Black;
        constantLine2.Name = "Constant Line 1";
        constantLine2.ShowBehind = true;
        constantLine2.ShowInLegend = false;
        constantLine2.Title.Visible = false;
        xyDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine2});
        xyDiagram1.AxisY.Label.TextPattern = "{V:G}%";
        xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        xyDiagram1.AxisY.Title.Text = "Desempenho Financeiro";
        xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisualRange.Auto = false;
        xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "25";
        xyDiagram1.AxisY.VisualRange.MinValueSerializable = "-25";
        xyDiagram1.AxisY.WholeRange.Auto = false;
        xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "25";
        xyDiagram1.AxisY.WholeRange.MinValueSerializable = "-25";
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
        this.xrChart1.Name = "xrChart1";
        series1.ArgumentDataMember = "DesvioFisico";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        bubbleSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Label = bubbleSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.LegendTextPattern = "{V:G}";
        series1.Name = "Series 4";
        series1.ShowInLegend = false;
        series1.ValueDataMembersSerializable = "DesvioCusto;cfPertualRelativoAoTodo";
        bubbleSeriesView1.ColorEach = true;
        bubbleSeriesView1.MaxSize = 11.5D;
        bubbleSeriesView1.MinSize = 3.5D;
        bubbleSeriesView1.Transparency = ((byte)(25));
        series1.View = bubbleSeriesView1;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
        this.xrChart1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        bubbleSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        bubbleSeriesLabel2.TextPattern = "{S}";
        this.xrChart1.SeriesTemplate.Label = bubbleSeriesLabel2;
        this.xrChart1.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.LegendTextPattern = "{S}";
        this.xrChart1.SeriesTemplate.ShowInLegend = false;
        bubbleSeriesView2.MaxSize = 10D;
        bubbleSeriesView2.MinSize = 0.1D;
        this.xrChart1.SeriesTemplate.View = bubbleSeriesView2;
        this.xrChart1.SizeF = new System.Drawing.SizeF(2750F, 1500F);
        chartTitle1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        chartTitle1.Text = "GRÁFICO DE DESEMPENHO FÍSICO-FINANCEIRO";
        this.xrChart1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
        this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart1_CustomDrawSeriesPoint);
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLine2,
            this.xrLine1,
            this.xrPictureBox1,
            this.xrLabel5,
            this.xrTable2,
            this.xrTable1,
            this.xrLabel8});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold);
        this.GroupHeader1.HeightF = 460F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        this.GroupHeader1.StylePriority.UseFont = false;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ParamDataEmissao, "Text", "Emissão: {0:dd/MM/yyyy HH:mm:ss}")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(2014.907F, 170F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(750F, 40F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "xrLabel6";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // ParamDataEmissao
        // 
        this.ParamDataEmissao.Name = "ParamDataEmissao";
        this.ParamDataEmissao.Type = typeof(System.DateTime);
        this.ParamDataEmissao.ValueInfo = "01/21/2014 17:20:26";
        this.ParamDataEmissao.Visible = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ParamPeriodo, "Text", "")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(10F, 170F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(500F, 40F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // ParamPeriodo
        // 
        this.ParamPeriodo.Name = "ParamPeriodo";
        this.ParamPeriodo.Visible = false;
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.Silver;
        this.xrLine2.LineWidth = 5;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 260F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(2700F, 5F);
        this.xrLine2.StylePriority.UseBackColor = false;
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.Silver;
        this.xrLine1.LineWidth = 5;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 210F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(2700F, 5F);
        this.xrLine1.StylePriority.UseBackColor = false;
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(2368F, 10F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(380F, 160F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(142.6465F, 10.00004F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(2225.35F, 140F);
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Boletim de Ações Estratégicas";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(692.0045F, 270F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1692.642F, 40F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell16});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.CanGrow = false;
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "Físico";
        this.xrTableCell14.Weight = 0.923160187454578D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.CanGrow = false;
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "Financeiro";
        this.xrTableCell16.Weight = 2.06612184127539D;
        // 
        // xrTable1
        // 
        this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3F, 310F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2760F, 150F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell7,
            this.xrTableCell4,
            this.xrTableCell8,
            this.xrTableCell2,
            this.xrTableCell13,
            this.xrTableCell9,
            this.xrTableCell5,
            this.xrTableCell10,
            this.xrTableCell3,
            this.xrTableCell11,
            this.xrTableCell6,
            this.xrTableCell12});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.CanGrow = false;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Legenda";
        this.xrTableCell1.Weight = 0.151515151515152D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.CanGrow = false;
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Projeto";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell7.Weight = 0.346320346320346D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.CanGrow = false;
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Físico";
        this.xrTableCell4.Weight = 0.124999997558824D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.CanGrow = false;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "Financ.";
        this.xrTableCell8.Weight = 0.126082253523428D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.CanGrow = false;
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Percentual Previsto para o período";
        this.xrTableCell2.Weight = 0.201086954439559D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.CanGrow = false;
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "Percentual Realizado* para o período";
        this.xrTableCell13.Weight = 0.201086954439559D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.CanGrow = false;
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Desvio no perido";
        this.xrTableCell9.Weight = 0.1660079093027D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.CanGrow = false;
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Orçamento Total do Projeto";
        this.xrTableCell5.Weight = 0.324675324675325D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.CanGrow = false;
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "% do Projeto na Carteira da DIRET";
        this.xrTableCell10.Weight = 0.217391297598691D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.CanGrow = false;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Previsto para o período";
        this.xrTableCell3.Weight = 0.288043471511735D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.CanGrow = false;
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "Realizado* acumulado até o período";
        this.xrTableCell11.Weight = 0.288043471511735D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.CanGrow = false;
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Desvio no período";
        this.xrTableCell6.Weight = 0.153491456347537D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.CanGrow = false;
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "PONTOS DE ATENÇÂO";
        this.xrTableCell12.Weight = 0.411255411255411D;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 220F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(700F, 40F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "Análise Crítica do Estritório de Projetos";
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.xrLabel9,
            this.xrTable4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 250.0001F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrPanel2
        // 
        this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel10});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125.0001F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(2770F, 125F);
        this.xrPanel2.StylePriority.UseBorders = false;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ParamAnaliseCriticaEscritorioProjetos, "Text", "")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(10F, 65F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(2755F, 40F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "Análise Crítica do Escritório de Projetos:";
        // 
        // ParamAnaliseCriticaEscritorioProjetos
        // 
        this.ParamAnaliseCriticaEscritorioProjetos.Name = "ParamAnaliseCriticaEscritorioProjetos";
        this.ParamAnaliseCriticaEscritorioProjetos.Visible = false;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 25.00009F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(2755F, 40F);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "Análise Crítica do Escritório de Projetos:";
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(2.999978F, 50.00018F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 10, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(2381.645F, 40F);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UsePadding = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "*As realizações referem-se às previsões física e financeira do período";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(3F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(2381.645F, 50F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.Text = "TOTAL (Financeiro)";
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell35.Weight = 1.31471780533137D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorOrcamento")});
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBackColor = false;
        this.xrTableCell36.StylePriority.UseBorderColor = false;
        xrSummary1.FormatString = "{0:c2}";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell36.Summary = xrSummary1;
        this.xrTableCell36.Text = "xrTableCell36";
        this.xrTableCell36.Weight = 0.324088327613842D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.PercentualRelativoAoTodo")});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseBackColor = false;
        this.xrTableCell37.StylePriority.UseBorderColor = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:p0}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell37.Summary = xrSummary2;
        this.xrTableCell37.Text = "xrTableCell37";
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell37.Weight = 0.216998207144626D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorCustoPrevisto")});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBackColor = false;
        this.xrTableCell38.StylePriority.UseBorderColor = false;
        xrSummary3.FormatString = "{0:c2}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell38.Summary = xrSummary3;
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.Weight = 0.287522626670595D;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.ValorCustoRealizado")});
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseBackColor = false;
        this.xrTableCell39.StylePriority.UseBorderColor = false;
        xrSummary4.FormatString = "{0:c2}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell39.Summary = xrSummary4;
        this.xrTableCell39.Text = "xrTableCell39";
        this.xrTableCell39.Weight = 0.287522643226231D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GraficoBolha.cfTotalDesvioCusto", "{0:p2}")});
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseBackColor = false;
        this.xrTableCell40.StylePriority.UseBorderColor = false;
        this.xrTableCell40.StylePriority.UseTextAlignment = false;
        this.xrTableCell40.Text = "Desvio no período";
        this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell40.Weight = 0.153213967234177D;
        // 
        // xrCrossBandBox1
        // 
        this.xrCrossBandBox1.BorderWidth = 1F;
        this.xrCrossBandBox1.Dpi = 254F;
        this.xrCrossBandBox1.EndBand = this.GroupFooter1;
        this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(0F, 125.0001F);
        this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox1.Name = "xrCrossBandBox1";
        this.xrCrossBandBox1.StartBand = this.GroupHeader1;
        this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox1.WidthF = 2770F;
        // 
        // cfTotalDesvioCusto
        // 
        this.cfTotalDesvioCusto.DataMember = "GraficoBolha";
        this.cfTotalDesvioCusto.Expression = "(Sum([ValorCustoRealizado])-Sum([ValorCustoPrevisto]))/Sum([ValorCustoPrevisto])";
        this.cfTotalDesvioCusto.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfTotalDesvioCusto.Name = "cfTotalDesvioCusto";
        // 
        // cfPercentualFisicoPrevisto
        // 
        this.cfPercentualFisicoPrevisto.DataMember = "GraficoBolha";
        this.cfPercentualFisicoPrevisto.Expression = "[PercentualFisicoPrevisto]/100";
        this.cfPercentualFisicoPrevisto.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfPercentualFisicoPrevisto.Name = "cfPercentualFisicoPrevisto";
        // 
        // cfPercentualFisicoRealizado
        // 
        this.cfPercentualFisicoRealizado.DataMember = "GraficoBolha";
        this.cfPercentualFisicoRealizado.Expression = "[PercentualFisicoRealizado]/100";
        this.cfPercentualFisicoRealizado.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfPercentualFisicoRealizado.Name = "cfPercentualFisicoRealizado";
        // 
        // cfDesvioFisico
        // 
        this.cfDesvioFisico.DataMember = "GraficoBolha";
        this.cfDesvioFisico.Expression = "[DesvioFisico]/100";
        this.cfDesvioFisico.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfDesvioFisico.Name = "cfDesvioFisico";
        // 
        // cfDesvioCusto
        // 
        this.cfDesvioCusto.DataMember = "GraficoBolha";
        this.cfDesvioCusto.Expression = "[DesvioCusto]/100";
        this.cfDesvioCusto.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfDesvioCusto.Name = "cfDesvioCusto";
        // 
        // cfPertualRelativoAoTodo
        // 
        this.cfPertualRelativoAoTodo.DataMember = "GraficoBolha";
        this.cfPertualRelativoAoTodo.Expression = "[PercentualRelativoAoTodo] * 10";
        this.cfPertualRelativoAoTodo.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfPertualRelativoAoTodo.Name = "cfPertualRelativoAoTodo";
        // 
        // RelGraficoBolha
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfTotalDesvioCusto,
            this.cfPercentualFisicoPrevisto,
            this.cfPercentualFisicoRealizado,
            this.cfDesvioFisico,
            this.cfDesvioCusto,
            this.cfPertualRelativoAoTodo});
        this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox1});
        this.DataMember = "GraficoBolha";
        this.DataSource = this.dsGraficoBolha1;
        this.Dpi = 254F;
        this.FilterString = "[IndicaStatusReportSuperior] = \'N\'";
        this.Font = new System.Drawing.Font("Verdana", 10F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(99, 99, 99, 99);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ParamCodigoStatusReport,
            this.ParamNomeObjeto,
            this.ParamPeriodo,
            this.ParamDataEmissao,
            this.ParamAnaliseCriticaEscritorioProjetos});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "15.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.RelGraficoBolha_DataSourceDemanded);
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.RelGraficoBolha_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsGraficoBolha1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void RelGraficoBolha_DataSourceDemanded(object sender, EventArgs e)
    {
        var cDados = CdadosUtil.GetCdados(null);
        var comandoSql = "p_rel_GraficoBolha";
        var connectionString = cDados.classeDados.getStringConexao();
        var param = new SqlParameter(
            "@in_CodigoStatusReport", ParamCodigoStatusReport.Value);
        var adapter = new SqlDataAdapter(comandoSql, connectionString);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        adapter.SelectCommand.Parameters.Add(param);
        DataAdapter = adapter;
    }

    private void RelGraficoBolha_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DefineParametrosRelatorio();
        DefineIntervaloValoresGraficos();
    }

    private void DefineParametrosRelatorio()
    {
        var row = dsGraficoBolha1.GraficoBolha.Single(
            r => r.IndicaStatusReportSuperior.ToUpper() == "S");
        string inicioPeriodo = string.Format(
            "{0:MMMM} de {0:yyyy}", row.DataInicioPeriodoRelatorio);
        ParamAnaliseCriticaEscritorioProjetos.Value = row.ComentarioGeral;
        ParamDataEmissao.Value = row.DataEmissaoRelatorio;
        ParamNomeObjeto.Value = row.NomeObjeto;
        ParamPeriodo.Value = inicioPeriodo;
    }

    private void DefineIntervaloValoresGraficos()
    {
        const decimal margemDesvioCusto = 15M;
        const decimal margemDesvioFisico = 5M;
        decimal maiorDesvioCustoAbsoluto = dsGraficoBolha1.GraficoBolha.Max(
            r => Math.Abs(r.IsDesvioCustoNull() ? 0 : r.DesvioCusto));
        decimal maiorDesvioFisicoAbsoluto = dsGraficoBolha1.GraficoBolha.Max(
            r => Math.Abs(r.IsDesvioFisicoNull() ? 0 : r.DesvioFisico));
        maiorDesvioCustoAbsoluto += margemDesvioCusto;
        maiorDesvioFisicoAbsoluto += margemDesvioFisico;
        XYDiagram diagrama = (XYDiagram)xrChart1.Diagram;
        diagrama.AxisX.VisualRange.Auto = false;
        diagrama.AxisY.VisualRange.Auto = false;
        diagrama.AxisX.VisualRange.MaxValueSerializable =
            maiorDesvioFisicoAbsoluto.ToString();
        diagrama.AxisX.VisualRange.MinValueSerializable =
            (-maiorDesvioFisicoAbsoluto).ToString();
        diagrama.AxisY.VisualRange.MaxValueSerializable =
            maiorDesvioCustoAbsoluto.ToString();
        diagrama.AxisY.VisualRange.MinValueSerializable =
            (-maiorDesvioCustoAbsoluto).ToString();
    }
    private void xrChart1_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
    {
        var drv = e.SeriesPoint.Tag as DataRowView;
        if (drv == null)
            return;
        var dr = drv.Row as DsGraficoBolha.GraficoBolhaRow;
        e.LabelText = dr.Legenda;
    }
}
