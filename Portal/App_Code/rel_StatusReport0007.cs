using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

/// <summary>
/// Summary description for rel_StatusReport0007
/// </summary>
public class rel_StatusReport0007 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private ReportHeaderBand ReportHeader;
    private XRLabel lblTituloRelatorioStatus;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private dados cDados;
    private dsRelStatusReport0007 dsRelStatusReport;
    private XRLabel lblTituloPaginasWeb;
    private XRPageInfo xrPageInfo2;
    private XRLine xrLine3;
    private XRLabel lblEmitidoEm;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell17;
    private XRPictureBox xrImgVerde;
    private XRLabel xrIDPValue;
    private XRLabel xrLblQtdeEntregasRealizadas;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLblQtdeEntregasAtrasadas;
    private XRLabel xrLblQtdeEntregasPrazo;
    private XRChart xrChartCurvaSFisica;
    private XRPictureBox pbSpeedometro;
    private XRLine xrLine6;
    private XRLine xrLine5;
    private XRLine xrLine4;
    private XRLine xrLine2;
    private XRLine xrLine1;
    private XRLabel xrLabel2;
    private XRLine xrLine7;
    private XRLine xrLine8;
    private XRCrossBandBox xrCrossBandBox1;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;
    private XRPictureBox pbCorDesempFisico;
    private XRPictureBox picLogoEntidade;
    private XRControlStyle xrControlStyle1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel1;
    private XRLabel lblFooterTopLeft;
    private XRLabel lblFooterTopRight;


    private const int tamanhoMargem = 35;
    private decimal? inicioFaixaAmarelo;
    private XRRichText xrRichText1;
    private decimal? inicioFaixaVerde;

    public rel_StatusReport0007()
    {
        cDados = CdadosUtil.GetCdados(null);

        InitializeComponent();
        DefineLogoRelatorio();
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
        string resourceFileName = "rel_StatusReport0007.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_StatusReport0007.ResourceManager;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.pbCorDesempFisico = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine8 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.pbSpeedometro = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrChartCurvaSFisica = new DevExpress.XtraReports.UI.XRChart();
        this.dsRelStatusReport = new dsRelStatusReport0007();
        this.xrIDPValue = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLblQtdeEntregasRealizadas = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrImgVerde = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLblQtdeEntregasPrazo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLblQtdeEntregasAtrasadas = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.lblEmitidoEm = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloRelatorioStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.lblFooterTopLeft = new DevExpress.XtraReports.UI.XRLabel();
        this.lblFooterTopRight = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.lblTituloPaginasWeb = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChartCurvaSFisica)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelStatusReport)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.xrLabel4,
            this.pbCorDesempFisico,
            this.xrLine8,
            this.xrLine7,
            this.xrLine6,
            this.xrLine5,
            this.xrLine4,
            this.xrLine2,
            this.xrLine1,
            this.xrLabel2,
            this.pbSpeedometro,
            this.xrChartCurvaSFisica,
            this.xrIDPValue,
            this.xrLblQtdeEntregasRealizadas,
            this.xrPictureBox1,
            this.xrPictureBox2,
            this.xrImgVerde,
            this.xrLblQtdeEntregasPrazo,
            this.xrLblQtdeEntregasAtrasadas});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 386.4792F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.StylePriority.UseBorders = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "StatusReport.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(213F, 264.48F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1652F, 94.46F);
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.NomeObjeto")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(21.99999F, 5.000018F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(719.9999F, 254.3F);
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "xrLabel4";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // pbCorDesempFisico
        // 
        this.pbCorDesempFisico.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "StatusReport.CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.pbCorDesempFisico.Dpi = 254F;
        this.pbCorDesempFisico.LocationFloat = new DevExpress.Utils.PointFloat(810.8125F, 139F);
        this.pbCorDesempFisico.Name = "pbCorDesempFisico";
        this.pbCorDesempFisico.SizeF = new System.Drawing.SizeF(63.5F, 63.5F);
        // 
        // xrLine8
        // 
        this.xrLine8.Dpi = 254F;
        this.xrLine8.LineWidth = 3;
        this.xrLine8.LocationFloat = new DevExpress.Utils.PointFloat(17F, 0F);
        this.xrLine8.Name = "xrLine8";
        this.xrLine8.SizeF = new System.Drawing.SizeF(2748F, 5F);
        // 
        // xrLine7
        // 
        this.xrLine7.Dpi = 254F;
        this.xrLine7.LineWidth = 3;
        this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(17F, 259.4F);
        this.xrLine7.Name = "xrLine7";
        this.xrLine7.SizeF = new System.Drawing.SizeF(1845F, 5F);
        // 
        // xrLine6
        // 
        this.xrLine6.Dpi = 254F;
        this.xrLine6.LineWidth = 3;
        this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(17F, 359F);
        this.xrLine6.Name = "xrLine6";
        this.xrLine6.SizeF = new System.Drawing.SizeF(2748F, 5F);
        // 
        // xrLine5
        // 
        this.xrLine5.Dpi = 254F;
        this.xrLine5.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine5.LineWidth = 3;
        this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(1860F, 5F);
        this.xrLine5.Name = "xrLine5";
        this.xrLine5.SizeF = new System.Drawing.SizeF(5F, 254.3F);
        // 
        // xrLine4
        // 
        this.xrLine4.Dpi = 254F;
        this.xrLine4.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine4.LineWidth = 3;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(1286.5F, 5F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(5F, 254.3F);
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(930.5F, 5F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(5F, 254.3F);
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(748.5F, 5F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(5F, 254.3F);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(17F, 264.4792F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(195F, 89.16829F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Resumo Executivo:";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // pbSpeedometro
        // 
        this.pbSpeedometro.Dpi = 254F;
        this.pbSpeedometro.LocationFloat = new DevExpress.Utils.PointFloat(1340.792F, 5.000018F);
        this.pbSpeedometro.Name = "pbSpeedometro";
        this.pbSpeedometro.SizeF = new System.Drawing.SizeF(474.6251F, 254.3F);
        this.pbSpeedometro.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.pbSpeedometro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.pbSpeedometro_BeforePrint);
        // 
        // xrChartCurvaSFisica
        // 
        this.xrChartCurvaSFisica.BorderColor = System.Drawing.Color.Black;
        this.xrChartCurvaSFisica.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChartCurvaSFisica.DataMember = "StatusReport.StatusReport_Valores";
        this.xrChartCurvaSFisica.DataSource = this.dsRelStatusReport;
        xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.DateTimeScaleOptions.GridSpacing = 50D;
        xyDiagram1.AxisY.GridLines.MinorVisible = true;
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 50D;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisualRange.Auto = false;
        xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram1.AxisY.WholeRange.Auto = false;
        xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.Margins.Bottom = 0;
        xyDiagram1.Margins.Left = 0;
        xyDiagram1.Margins.Right = 0;
        xyDiagram1.Margins.Top = 0;
        this.xrChartCurvaSFisica.Diagram = xyDiagram1;
        this.xrChartCurvaSFisica.Dpi = 254F;
        this.xrChartCurvaSFisica.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChartCurvaSFisica.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChartCurvaSFisica.LocationFloat = new DevExpress.Utils.PointFloat(1876.521F, 5.000018F);
        this.xrChartCurvaSFisica.Name = "xrChartCurvaSFisica";
        this.xrChartCurvaSFisica.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
        this.xrChartCurvaSFisica.PivotGridDataSourceOptions.AutoBindingSettingsEnabled = false;
        this.xrChartCurvaSFisica.PivotGridDataSourceOptions.AutoLayoutSettingsEnabled = false;
        series1.ArgumentDataMember = "Periodo";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series1.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
        series1.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.Label = pointSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Name = "Previsto";
        series1.ValueDataMembersSerializable = "ValorPrevisto";
        lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.View = lineSeriesView1;
        series2.ArgumentDataMember = "Periodo";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series2.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
        series2.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
        pointSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series2.Label = pointSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.Name = "Realizado";
        series2.ValueDataMembersSerializable = "ValorRealizado";
        lineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.View = lineSeriesView2;
        this.xrChartCurvaSFisica.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        pointSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChartCurvaSFisica.SeriesTemplate.Label = pointSeriesLabel3;
        this.xrChartCurvaSFisica.SeriesTemplate.View = lineSeriesView3;
        this.xrChartCurvaSFisica.SizeF = new System.Drawing.SizeF(870.4795F, 348.6474F);
        this.xrChartCurvaSFisica.StylePriority.UsePadding = false;
        this.xrChartCurvaSFisica.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChartCurvaSFisica_BeforePrint);
        // 
        // dsRelStatusReport
        // 
        this.dsRelStatusReport.DataSetName = "dsRelStatusReport";
        this.dsRelStatusReport.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrIDPValue
        // 
        this.xrIDPValue.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.SPI", "{0:0.00}")});
        this.xrIDPValue.Dpi = 254F;
        this.xrIDPValue.LocationFloat = new DevExpress.Utils.PointFloat(762.5001F, 75.23F);
        this.xrIDPValue.Name = "xrIDPValue";
        this.xrIDPValue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrIDPValue.SizeF = new System.Drawing.SizeF(160F, 63.5F);
        this.xrIDPValue.StylePriority.UseTextAlignment = false;
        this.xrIDPValue.Text = "xrIDPValue";
        this.xrIDPValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLblQtdeEntregasRealizadas
        // 
        this.xrLblQtdeEntregasRealizadas.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLblQtdeEntregasRealizadas.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.QuantidadeEntregasRealizadas", "{0:0 (concluídas)}")});
        this.xrLblQtdeEntregasRealizadas.Dpi = 254F;
        this.xrLblQtdeEntregasRealizadas.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLblQtdeEntregasRealizadas.LocationFloat = new DevExpress.Utils.PointFloat(1009.854F, 183.3333F);
        this.xrLblQtdeEntregasRealizadas.Name = "xrLblQtdeEntregasRealizadas";
        this.xrLblQtdeEntregasRealizadas.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLblQtdeEntregasRealizadas.SizeF = new System.Drawing.SizeF(255.5208F, 63.5F);
        this.xrLblQtdeEntregasRealizadas.StylePriority.UseBorders = false;
        this.xrLblQtdeEntregasRealizadas.StylePriority.UseFont = false;
        this.xrLblQtdeEntregasRealizadas.StylePriority.UseTextAlignment = false;
        this.xrLblQtdeEntregasRealizadas.Text = "xrLblQtdeEntregasRealizadas";
        this.xrLblQtdeEntregasRealizadas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.ImageUrl = "~/imagens/Verde.gif";
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(937.3537F, 23.37503F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(63.5F, 63.5F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~/imagens/Vermelho.gif";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(937.3536F, 103.3542F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(63.5F, 63.5F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrImgVerde
        // 
        this.xrImgVerde.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrImgVerde.Dpi = 254F;
        this.xrImgVerde.ImageUrl = "~/imagens/VerdeOk.gif";
        this.xrImgVerde.LocationFloat = new DevExpress.Utils.PointFloat(937.3537F, 183.3333F);
        this.xrImgVerde.Name = "xrImgVerde";
        this.xrImgVerde.SizeF = new System.Drawing.SizeF(63.5F, 63.5F);
        this.xrImgVerde.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        this.xrImgVerde.StylePriority.UseBorders = false;
        // 
        // xrLblQtdeEntregasPrazo
        // 
        this.xrLblQtdeEntregasPrazo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLblQtdeEntregasPrazo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.QuantidadeEntregasPrazo", "{0:0 (no prazo)}")});
        this.xrLblQtdeEntregasPrazo.Dpi = 254F;
        this.xrLblQtdeEntregasPrazo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLblQtdeEntregasPrazo.LocationFloat = new DevExpress.Utils.PointFloat(1009.854F, 23.37503F);
        this.xrLblQtdeEntregasPrazo.Name = "xrLblQtdeEntregasPrazo";
        this.xrLblQtdeEntregasPrazo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLblQtdeEntregasPrazo.SizeF = new System.Drawing.SizeF(255.5208F, 63.5F);
        this.xrLblQtdeEntregasPrazo.StylePriority.UseBorders = false;
        this.xrLblQtdeEntregasPrazo.StylePriority.UseFont = false;
        this.xrLblQtdeEntregasPrazo.StylePriority.UseTextAlignment = false;
        this.xrLblQtdeEntregasPrazo.Text = "xrLblQtdeEntregasPrazo";
        this.xrLblQtdeEntregasPrazo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLblQtdeEntregasAtrasadas
        // 
        this.xrLblQtdeEntregasAtrasadas.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLblQtdeEntregasAtrasadas.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.QuantidadeEntregasAtrasadas", "{0:0 (atraso)}")});
        this.xrLblQtdeEntregasAtrasadas.Dpi = 254F;
        this.xrLblQtdeEntregasAtrasadas.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLblQtdeEntregasAtrasadas.LocationFloat = new DevExpress.Utils.PointFloat(1009.854F, 103.3542F);
        this.xrLblQtdeEntregasAtrasadas.Name = "xrLblQtdeEntregasAtrasadas";
        this.xrLblQtdeEntregasAtrasadas.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLblQtdeEntregasAtrasadas.SizeF = new System.Drawing.SizeF(255.5208F, 63.5F);
        this.xrLblQtdeEntregasAtrasadas.StylePriority.UseBorders = false;
        this.xrLblQtdeEntregasAtrasadas.StylePriority.UseFont = false;
        this.xrLblQtdeEntregasAtrasadas.StylePriority.UseTextAlignment = false;
        this.xrLblQtdeEntregasAtrasadas.Text = "xrLblQtdeEntregasAtrasadas";
        this.xrLblQtdeEntregasAtrasadas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 61.95835F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 62.35391F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.StylePriority.UseBorders = false;
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picLogoEntidade,
            this.xrTable5,
            this.xrLine3,
            this.lblEmitidoEm,
            this.lblTituloRelatorioStatus});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 228.5F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(2507.6F, 11.00002F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(260.4F, 126F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(17F, 165F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(2748F, 63.49998F);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell16,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell17});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.Text = "Iniciativa";
        this.xrTableCell13.Weight = 0.802401764118926D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "SPI";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell16.Weight = 0.197598258694487D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "Entregas";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell14.Weight = 0.389737977455426D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "Desempenho Físico";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell15.Weight = 0.625545853282745D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Curva S Física";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell17.Weight = 0.984716146448416D;
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.LineWidth = 3;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(14F, 140F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(2754F, 5F);
        // 
        // lblEmitidoEm
        // 
        this.lblEmitidoEm.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.DataEmissaoRelatorio", "{0:\"Emitido em: \"dd/MM/yyyy HH:mm:ss}")});
        this.lblEmitidoEm.Dpi = 254F;
        this.lblEmitidoEm.LocationFloat = new DevExpress.Utils.PointFloat(13.99959F, 85F);
        this.lblEmitidoEm.Name = "lblEmitidoEm";
        this.lblEmitidoEm.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblEmitidoEm.SizeF = new System.Drawing.SizeF(920.0004F, 46F);
        this.lblEmitidoEm.Text = "lblEmitidoEm";
        // 
        // lblTituloRelatorioStatus
        // 
        this.lblTituloRelatorioStatus.Dpi = 254F;
        this.lblTituloRelatorioStatus.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold);
        this.lblTituloRelatorioStatus.LocationFloat = new DevExpress.Utils.PointFloat(13.99999F, 1.00002F);
        this.lblTituloRelatorioStatus.Name = "lblTituloRelatorioStatus";
        this.lblTituloRelatorioStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloRelatorioStatus.SizeF = new System.Drawing.SizeF(920F, 75F);
        this.lblTituloRelatorioStatus.StylePriority.UseFont = false;
        this.lblTituloRelatorioStatus.StylePriority.UseTextAlignment = false;
        this.lblTituloRelatorioStatus.Text = "Relatório de Status";
        this.lblTituloRelatorioStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblFooterTopLeft,
            this.lblFooterTopRight,
            this.xrLabel1,
            this.xrPageInfo2,
            this.lblTituloPaginasWeb});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 150.3125F;
        this.PageFooter.Name = "PageFooter";
        // 
        // lblFooterTopLeft
        // 
        this.lblFooterTopLeft.Dpi = 254F;
        this.lblFooterTopLeft.LocationFloat = new DevExpress.Utils.PointFloat(13.99959F, 73.97755F);
        this.lblFooterTopLeft.Name = "lblFooterTopLeft";
        this.lblFooterTopLeft.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFooterTopLeft.SizeF = new System.Drawing.SizeF(1359.958F, 58.42F);
        this.lblFooterTopLeft.StylePriority.UseTextAlignment = false;
        this.lblFooterTopLeft.Text = "lblFooterTopLeft";
        this.lblFooterTopLeft.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblFooterTopRight
        // 
        this.lblFooterTopRight.Dpi = 254F;
        this.lblFooterTopRight.LocationFloat = new DevExpress.Utils.PointFloat(1405.042F, 73.97755F);
        this.lblFooterTopRight.Name = "lblFooterTopRight";
        this.lblFooterTopRight.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFooterTopRight.SizeF = new System.Drawing.SizeF(1359.958F, 58.42F);
        this.lblFooterTopRight.StylePriority.UseTextAlignment = false;
        this.lblFooterTopRight.Text = "lblFooterTopRight";
        this.lblFooterTopRight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(2526F, 1.479238F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(130F, 50F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "- página:";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(2656F, 1.479238F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(107.396F, 50F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblTituloPaginasWeb
        // 
        this.lblTituloPaginasWeb.Dpi = 254F;
        this.lblTituloPaginasWeb.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblTituloPaginasWeb.LocationFloat = new DevExpress.Utils.PointFloat(1373F, 1.479238F);
        this.lblTituloPaginasWeb.Name = "lblTituloPaginasWeb";
        this.lblTituloPaginasWeb.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloPaginasWeb.SizeF = new System.Drawing.SizeF(1152.979F, 50F);
        this.lblTituloPaginasWeb.StylePriority.UseFont = false;
        this.lblTituloPaginasWeb.StylePriority.UseTextAlignment = false;
        this.lblTituloPaginasWeb.Text = "lblTituloPaginasWeb";
        this.lblTituloPaginasWeb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.Expanded = false;
        this.ReportHeader.HeightF = 0F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // pCodigoStatusReport
        // 
        this.pCodigoStatusReport.Name = "pCodigoStatusReport";
        this.pCodigoStatusReport.Type = typeof(short);
        this.pCodigoStatusReport.ValueInfo = "0";
        this.pCodigoStatusReport.Visible = false;
        // 
        // xrCrossBandBox1
        // 
        this.xrCrossBandBox1.BorderWidth = 1F;
        this.xrCrossBandBox1.Dpi = 254F;
        this.xrCrossBandBox1.EndBand = this.PageFooter;
        this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(13.99999F, 60F);
        this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(13.99999F, 162F);
        this.xrCrossBandBox1.Name = "xrCrossBandBox1";
        this.xrCrossBandBox1.StartBand = this.PageHeader;
        this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(13.99999F, 162F);
        this.xrCrossBandBox1.WidthF = 2754F;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrControlStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // rel_StatusReport0007
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.ReportHeader});
        this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox1});
        this.DataMember = "StatusReport";
        this.DataSource = this.dsRelStatusReport;
        this.DisplayName = "Relatório de Acompanhamento de Iniciativas";
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(99, 99, 62, 62);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoStatusReport});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "15.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_StatusReport0007_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChartCurvaSFisica)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelStatusReport)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Instruções complementares
    private void rel_StatusReport0007_DataSourceDemanded(object sender, System.EventArgs e)
    {
        string connectionString = cDados.classeDados.getStringConexao();
        string strCommand = string.Format("exec  [dbo].[p_rel_StatusReport0007] {0}", pCodigoStatusReport.Value);
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(strCommand, connection);
                command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
                dsRelStatusReport.Load(
                    command.ExecuteReader(), LoadOption.OverwriteChanges,
                    "StatusReport", "Valores");

                DataSet ds = cDados.getParametrosSistema("tituloPaginasWeb");
                if (ds.Tables[0].Rows.Count > 0)
                    lblTituloPaginasWeb.Text = ds.Tables[0].Rows[0]["tituloPaginasWeb"] as string;

                int nCodigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));

                lblTituloRelatorioStatus.Text = cDados.getTextoPadraoEntidade(nCodigoEntidade, "TtlSttRpt0007");
                lblFooterTopLeft.Text = cDados.getTextoPadraoEntidade(nCodigoEntidade, "RdpSttRpt_TopL");
                lblFooterTopRight.Text = cDados.getTextoPadraoEntidade(nCodigoEntidade, "RdpSttRpt_TopR");

                inicioFaixaAmarelo = cDados.getValorInicioFaixaCorFisico("Amarelo");
                inicioFaixaVerde = cDados.getValorInicioFaixaCorFisico("Verde");
            }
            finally
            {
                connection.Close();
            }
        }
    }

    private void DefineLogoRelatorio()
    {
        DataSet dsTemp = cDados.getParametrosSistema("UrlLogo01BoletimStatus");
        if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 &&
            dsTemp.Tables[0].Columns.IndexOf("UrlLogo01BoletimStatus") > -1)
        {
            string urlLogo01BoletimStatus =
                dsTemp.Tables[0].Rows[0]["UrlLogo01BoletimStatus"] as string;
            picLogoEntidade.ImageUrl = urlLogo01BoletimStatus;
        }

        if (string.IsNullOrWhiteSpace(picLogoEntidade.ImageUrl))
        {
            System.Drawing.Image logo = cDados.ObtemLogoEntidade();
            picLogoEntidade.Image = logo;
        }
    }

    private void DefineCorSeriesGrafico(XRChart chart)
    {
        //int argbPrevisto = Int32.Parse(cDados.corPrevisto, NumberStyles.HexNumber);
        //int argbReal = Int32.Parse(cDados.corReal, NumberStyles.HexNumber);
        //Palette novaPaleta = new Palette();
        //novaPaleta.Add(Color.Green);
        //novaPaleta.Add(Color.Blue);
        ////novaPaleta.Add(Color.FromArgb(argbPrevisto));
        ////novaPaleta.Add(Color.FromArgb(argbReal));

        //chart.PaletteRepository.Add("RealizadoxPrevisto", novaPaleta);
        //chart.PaletteName = "RealizadoxPrevisto";
        ////chart.Series["Previsto"].View.Color = Color.FromArgb(argbPrevisto);
        ////chart.Series["Realizado"].View.Color = Color.FromArgb(argbReal);
    }

    private void xrChartCurvaSFisica_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //XRChart chart = (XRChart)sender;
        //DefineCorSeriesGrafico(chart);
    }
    #endregion

    #region speedômetro
    private void pbSpeedometro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int limiteFaixaVermelho, limiteFaixaAmarelo;
        XRPictureBox grafico = (XRPictureBox)sender;

        int percPrevisto = Convert.ToInt32(grafico.Report.GetCurrentColumnValue("PercentualFisicoPrevisto"));
        int percReal = Convert.ToInt32(grafico.Report.GetCurrentColumnValue("PercentualFisicoRealizado"));

        if (percPrevisto == 100)
        {
            limiteFaixaVermelho = 99;
            limiteFaixaAmarelo = 99;
        }
        else
        {
            int limiteDesempVermelho, limiteDesempAmarelo;
            limiteFaixaVermelho = inicioFaixaAmarelo == null ? 0 : (int)inicioFaixaAmarelo;
            limiteFaixaAmarelo = inicioFaixaVerde == null ? 0 : (int)inicioFaixaVerde;
            limiteDesempVermelho = (int)(percPrevisto * (limiteFaixaVermelho / 100.0));
            limiteDesempAmarelo = (int)(percPrevisto * (limiteFaixaAmarelo / 100.0));

            if (limiteDesempVermelho == limiteDesempAmarelo)
                limiteFaixaAmarelo = limiteFaixaVermelho;
        }
        grafico.Image = CriaImagemSpeedometro(limiteFaixaVermelho, limiteFaixaAmarelo, percReal, percPrevisto, 250);
    }

    private static void DesenhaLinhaValorAtual(Graphics g, Point pontoCentral, int tamanho, float angulo)
    {
        Pen pen = new Pen(Color.Black, 6);
        pen.StartCap = LineCap.Round;
        pen.EndCap = LineCap.Triangle;
        Point pontoFinal = ObtemPontoPeloAngulo(pontoCentral, (int)(tamanho * 0.8), angulo);
        g.DrawLine(pen, pontoCentral, pontoFinal);

        pen = new Pen(Color.White, 2);
        g.DrawLine(pen, pontoCentral, pontoFinal);
    }

    private static void DesenhaLinhaMeta(Graphics g, Point pontoCentral, int tamanho, float angulo)
    {
        Pen pen = new Pen(Color.Black, 2);
        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        pen.EndCap = LineCap.DiamondAnchor;
        Point pontoInicial = ObtemPontoPeloAngulo(pontoCentral, (int)(tamanho * 0.6), angulo);
        Point pontoFinal = ObtemPontoPeloAngulo(pontoCentral, tamanho, angulo);
        g.DrawLine(pen, pontoInicial, pontoFinal);

        float percentualMeta = angulo * 100 / 180;
        if (angulo > 90)
            pontoInicial.X -= 20;
        DesenhaString(g, percentualMeta + "%", pontoInicial);
    }

    private static Point ObtemPontoPeloAngulo(Point pontoCentral, int tamanho, float angulo)
    {
        double grausRad = (1 - (angulo / 180)) * Math.PI;
        int x = pontoCentral.X + (int)(Math.Cos(grausRad) * tamanho);
        int y = pontoCentral.Y - (int)(Math.Sin(grausRad) * tamanho);
        Point pontoFim = new Point(x, y);
        return pontoFim;
    }

    private static Rectangle ObtemRetanguloAreaInterna(int largura, int altura)
    {
        float valorEscala = 0.66f;
        int novaLargura = (int)(largura * valorEscala);
        int novaAltura = (int)((altura * valorEscala));
        int x = (int)((largura - novaLargura) / 2) + tamanhoMargem;
        int y = (int)((altura - novaAltura) / 2) + tamanhoMargem;

        return new Rectangle(x, y, novaLargura, novaAltura);
    }

    private Image CriaImagemSpeedometro(float limiteFaixaVermelha, float limiteFaixaAmarela, float valorAtual, float valorMeta, int tamanho)
    {
        int largura = tamanho;
        int altura = tamanho / 2 + tamanhoMargem;
        Bitmap bitmap = new Bitmap(largura, altura);
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;

        Rectangle ret = new Rectangle(tamanhoMargem, tamanhoMargem,
            tamanho - 2 * tamanhoMargem, tamanho - 2 * tamanhoMargem);
        SolidBrush brushFaixaVermelha = new SolidBrush(Color.Red);
        SolidBrush brushFaixaAmarela = new SolidBrush(Color.Yellow);
        SolidBrush brushFaixaVerde = new SolidBrush(Color.Green);

        float grausArco = 180;
        float grausMeta = valorMeta / 100 * grausArco;
        float grausValorAtual = valorAtual / 100 * grausArco;
        float grausFaixaVermelha = limiteFaixaVermelha / 100 * grausMeta;
        float grausFaixaAmarela = limiteFaixaAmarela / 100 * grausMeta;
        //Desenha faixas
        g.FillPie(brushFaixaVermelha, ret, -180, grausFaixaVermelha);
        g.FillPie(brushFaixaAmarela, ret, grausFaixaVermelha - 180, grausFaixaAmarela - grausFaixaVermelha);
        g.FillPie(brushFaixaVerde, ret, grausFaixaAmarela - 180, 180 - grausFaixaAmarela);
        g.DrawPie(new Pen(Color.White), ret, -180, 180);
        //Desenha área interna
        Rectangle retAreaInterna = ObtemRetanguloAreaInterna(ret.Width, ret.Height);
        SolidBrush brushAreaInterna = new SolidBrush(Color.White);
        g.FillPie(brushAreaInterna, retAreaInterna, -180, 180);
        //Desenha linhas
        Point pontoCentral = new Point(ret.X + ret.Width / 2, ret.Y + ret.Height / 2);
        int tamanhoLinhas = (tamanho / 2) - tamanhoMargem;
        DesenhaLinhaMeta(g, pontoCentral, tamanhoLinhas, grausMeta);
        DesenhaLinhaValorAtual(g, pontoCentral, tamanhoLinhas, grausValorAtual);

        DesenhaString(g, "0%", new PointF(5, ret.Height / 2 + 15));
        DesenhaString(g, "25%", ObtemPontoPeloAngulo(pontoCentral, tamanhoLinhas, 45) + new Size(-20, -20));
        DesenhaString(g, "50%", new PointF(ret.X + ret.Width / 2, ret.Y - 15));
        DesenhaString(g, "75%", ObtemPontoPeloAngulo(pontoCentral, tamanhoLinhas, 135) + new Size(-5, -15));
        DesenhaString(g, "100%", new PointF(ret.X + ret.Width, (ret.Y + ret.Height / 2) - 10));
        DesenhaString(g, valorAtual + "%", new PointF((ret.X + ret.Width / 2) - 5, (ret.Y + ret.Height / 2) + 5));

        g.Flush();
        g.Dispose();

        return bitmap;
    }

    private static void DesenhaString(Graphics g, string texto, PointF ponto)
    {
        Font font = new Font("Verdana", 8, FontStyle.Regular);
        SolidBrush brushTexto = new SolidBrush(Color.Black);
        g.DrawString(texto, font, brushTexto, ponto);
    }

    #endregion

}