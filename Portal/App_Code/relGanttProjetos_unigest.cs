using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;

/// <summary>
/// Summary description for relGanttProjetos
/// </summary>
public class relGanttProjetos_unigest : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRChart chart;
    private DsGanttProjetos dsGanttProjetos;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private int codigoEntidade;
    private int codigoCarteira;
    public DevExpress.XtraReports.Parameters.Parameter pTitulo;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo3;
    private XRLabel lblCarteiraDeProjetos;
    private XRLabel xrLabel7;
    private XRPictureBox xrLogoEntidade;
    private XRPictureBox xrPictureBox1;
    dados cDados;

    public relGanttProjetos_unigest()
    {
        cDados = CdadosUtil.GetCdados(null);
        InitializeComponent();
    }

    private void InitData()
    {
        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        string command = string.Format(
            "SELECT * FROM [dbo].[f_GetGanttProjetosImpressaoDIRET]({0},{1}) ORDER BY StringOrdenacao"
            , codigoUsuarioLogado, codigoCarteira);
        string connectionString = cDados.classeDados.getStringConexao();
        SqlDataAdapter adapter = new SqlDataAdapter(command, connectionString);
        adapter.Fill(dsGanttProjetos.GanttProjetos);
        var query = dsGanttProjetos.GanttProjetos
            .OrderBy(gp => gp.NumeroPagina)
            .GroupBy(gp => gp.NumeroPagina);
        foreach (var pagina in query)
            dsGanttProjetos.Pagina.AddPaginaRow(pagina.Key);
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
        string resourceFileName = "relGanttProjetos_unigest.resx";
        System.Resources.ResourceManager resources = global::Resources.relGanttProjetos_unigest.ResourceManager;
        DevExpress.XtraCharts.GanttDiagram ganttDiagram1 = new DevExpress.XtraCharts.GanttDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.RangeBarSeriesLabel rangeBarSeriesLabel1 = new DevExpress.XtraCharts.RangeBarSeriesLabel();
        DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView1 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.RangeBarSeriesLabel rangeBarSeriesLabel2 = new DevExpress.XtraCharts.RangeBarSeriesLabel();
        DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView2 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.RangeBarSeriesLabel rangeBarSeriesLabel3 = new DevExpress.XtraCharts.RangeBarSeriesLabel();
        DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView3 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
        DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView4 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
        DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.chart = new DevExpress.XtraReports.UI.XRChart();
        this.dsGanttProjetos = new DsGanttProjetos();
        this.pTitulo = new DevExpress.XtraReports.Parameters.Parameter();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCarteiraDeProjetos = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsGanttProjetos)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.chart});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1899.188F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // chart
        // 
        this.chart.BorderColor = System.Drawing.Color.Black;
        this.chart.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.chart.DataMember = "Pagina.Pagina_GanttProjetos";
        this.chart.DataSource = this.dsGanttProjetos;
        ganttDiagram1.AxisX.Label.Font = new System.Drawing.Font("Verdana", 8F);
        ganttDiagram1.AxisX.Label.MaxLineCount = 1;
        ganttDiagram1.AxisX.Label.MaxWidth = 400;
        ganttDiagram1.AxisX.Label.ResolveOverlappingOptions.MinIndent = 0;
        ganttDiagram1.AxisX.Label.TextAlignment = System.Drawing.StringAlignment.Near;
        ganttDiagram1.AxisX.MinorCount = 1;
        ganttDiagram1.AxisX.Tickmarks.MinorVisible = false;
        ganttDiagram1.AxisX.Tickmarks.Visible = false;
        ganttDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        ganttDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
        ganttDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
        ganttDiagram1.AxisY.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Quarter;
        ganttDiagram1.AxisY.Label.Font = new System.Drawing.Font("Verdana", 8F);
        ganttDiagram1.AxisY.Label.TextPattern = "{V:MM/yyyy}";
        ganttDiagram1.AxisY.Tickmarks.MinorVisible = false;
        ganttDiagram1.AxisY.Tickmarks.Visible = false;
        ganttDiagram1.AxisY.Title.Font = new System.Drawing.Font("Verdana", 12F);
        ganttDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        ganttDiagram1.DefaultPane.BorderColor = System.Drawing.Color.Black;
        ganttDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        ganttDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        ganttDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        ganttDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.chart.Diagram = ganttDiagram1;
        this.chart.Dpi = 254F;
        this.chart.EmptyChartText.Text = "Não existem informações a serem exibidas";
        this.chart.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chart.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chart.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.chart.Legend.Font = new System.Drawing.Font("Verdana", 8F);
        this.chart.Legend.Name = "Default Legend";
        this.chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chart.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.chart.Name = "chart";
        series1.ArgumentDataMember = "Codigo";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        rangeBarSeriesLabel1.Font = new System.Drawing.Font("Verdana", 8F);
        rangeBarSeriesLabel1.Kind = DevExpress.XtraCharts.RangeBarLabelKind.OneLabel;
        series1.Label = rangeBarSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.LegendText = "Previsto";
        series1.Name = "Previsto";
        series1.ValueDataMembersSerializable = "InicioPrevisto;TerminoPrevisto";
        series1.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
        overlappedGanttSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        overlappedGanttSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = overlappedGanttSeriesView1;
        series2.ArgumentDataMember = "Codigo";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        rangeBarSeriesLabel2.Font = new System.Drawing.Font("Verdana", 7F);
        rangeBarSeriesLabel2.Kind = DevExpress.XtraCharts.RangeBarLabelKind.OneLabel;
        rangeBarSeriesLabel2.TextColor = System.Drawing.Color.Black;
        series2.Label = rangeBarSeriesLabel2;
        series2.LegendText = "Concluído";
        series2.Name = "Concluido";
        series2.ValueDataMembersSerializable = "InicioConcluido;TerminoConcluido";
        series2.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
        overlappedGanttSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        overlappedGanttSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series2.View = overlappedGanttSeriesView2;
        series3.ArgumentDataMember = "Codigo";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        rangeBarSeriesLabel3.Font = new System.Drawing.Font("Verdana", 8F);
        rangeBarSeriesLabel3.Kind = DevExpress.XtraCharts.RangeBarLabelKind.OneLabel;
        series3.Label = rangeBarSeriesLabel3;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.LegendText = "Previsto";
        series3.Name = "PrevistoUnidade";
        series3.ValueDataMembersSerializable = "InicioUnidade;TerminoUnidade";
        series3.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
        overlappedGanttSeriesView3.BarWidth = 0.1D;
        overlappedGanttSeriesView3.Color = System.Drawing.Color.Blue;
        overlappedGanttSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        overlappedGanttSeriesView3.MaxValueMarker.Color = System.Drawing.Color.Blue;
        overlappedGanttSeriesView3.MaxValueMarker.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        overlappedGanttSeriesView3.MaxValueMarker.Kind = DevExpress.XtraCharts.MarkerKind.InvertedTriangle;
        overlappedGanttSeriesView3.MaxValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
        overlappedGanttSeriesView3.MinValueMarker.Color = System.Drawing.Color.Blue;
        overlappedGanttSeriesView3.MinValueMarker.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        overlappedGanttSeriesView3.MinValueMarker.Kind = DevExpress.XtraCharts.MarkerKind.InvertedTriangle;
        overlappedGanttSeriesView3.MinValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
        series3.View = overlappedGanttSeriesView3;
        this.chart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
        this.chart.SeriesTemplate.View = overlappedGanttSeriesView4;
        this.chart.SizeF = new System.Drawing.SizeF(2850F, 1749.188F);
        this.chart.StylePriority.UseBorderColor = false;
        this.chart.StylePriority.UseBorders = false;
        chartTitle1.Font = new System.Drawing.Font("Verdana", 18F);
        chartTitle1.Text = "Gantt de Projetos";
        chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
        this.chart.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        this.chart.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chart_CustomDrawAxisLabel);
        // 
        // dsGanttProjetos
        // 
        this.dsGanttProjetos.DataSetName = "DsGanttProjetos";
        this.dsGanttProjetos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // pTitulo
        // 
        this.pTitulo.Description = "Título do Gráfico de Gantt";
        this.pTitulo.Name = "pTitulo";
        this.pTitulo.ValueInfo = "Gantt de Projetos";
        this.pTitulo.Visible = false;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 25F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 25F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel7,
            this.lblCarteiraDeProjetos});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 205.6342F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(2497.667F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(352.333F, 180.6342F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(6.260257F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(2469.615F, 58.42F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Relatório de Status";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblCarteiraDeProjetos
        // 
        this.lblCarteiraDeProjetos.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTitulo, "Text", "")});
        this.lblCarteiraDeProjetos.Dpi = 254F;
        this.lblCarteiraDeProjetos.Font = new System.Drawing.Font("Verdana", 15F);
        this.lblCarteiraDeProjetos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 101.0475F);
        this.lblCarteiraDeProjetos.Name = "lblCarteiraDeProjetos";
        this.lblCarteiraDeProjetos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCarteiraDeProjetos.SizeF = new System.Drawing.SizeF(2475.875F, 79.58666F);
        this.lblCarteiraDeProjetos.StylePriority.UseFont = false;
        this.lblCarteiraDeProjetos.StylePriority.UseTextAlignment = false;
        this.lblCarteiraDeProjetos.Text = "lblCarteiraDeProjetos";
        this.lblCarteiraDeProjetos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogoEntidade,
            this.xrPageInfo3,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 79.58655F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLogoEntidade
        // 
        this.xrLogoEntidade.Dpi = 254F;
        this.xrLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(2512.917F, 0F);
        this.xrLogoEntidade.Name = "xrLogoEntidade";
        this.xrLogoEntidade.SizeF = new System.Drawing.SizeF(298.979F, 79.58655F);
        this.xrLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        this.xrLogoEntidade.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLogoEntidade_BeforePrint);
        // 
        // xrPageInfo3
        // 
        this.xrPageInfo3.Dpi = 254F;
        this.xrPageInfo3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo3.Format = "Data de Impressão: {0:dd/MM/yyyy}";
        this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo3.Name = "xrPageInfo3";
        this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo3.SizeF = new System.Drawing.SizeF(730.25F, 58.42F);
        this.xrPageInfo3.StylePriority.UseFont = false;
        this.xrPageInfo3.StylePriority.UseTextAlignment = false;
        this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 9F);
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1532.843F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // relGanttProjetos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.DataMember = "Pagina";
        this.DataSource = this.dsGanttProjetos;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(25, 25, 25, 25);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pTitulo});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 50F;
        this.Version = "16.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relGanttProjetos_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsGanttProjetos)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        xrLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private void chart_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisY)
        {
            DateTime value = (DateTime)e.Item.AxisValue;
            int mes = value.Month;
            int semestre = (mes <= 6) ? 1 : 2;
            if ((mes < 10 && semestre == 2) || (mes < 4))
                e.Item.Text = string.Empty;
            else
                e.Item.Text = string.Format("S{0}/{1:yy}", semestre, value);
        }
        else
        {

            int saida = -1;
            int codigo = -1;

            if (int.TryParse(e.Item.AxisValue.ToString(), out saida))
            {
                codigo = int.Parse(e.Item.AxisValue.ToString());
            }

            if (codigo != -1)
            {
                DsGanttProjetos.GanttProjetosRow row =
                    dsGanttProjetos.GanttProjetos.Single(gp => gp.Codigo == codigo);
                e.Item.Text = row.Descricao;
                string indica = getIndicaProgramaUnidadeProjeto(row, codigo);

                switch (indica)
                {
                    case "PRG":
                        e.Item.Font = new Font("Verdana", 9, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                        e.Item.TextColor = Color.Blue;
                        if (row.Descricao != "")
                        {
                            e.Item.Text = "Programa: " + row.Descricao;
                        }
                        break;
                    case "UND":
                        e.Item.Font = new Font("Verdana", 8, FontStyle.Bold | FontStyle.Underline);
                        break;
                    case "PRJ":
                        e.Item.Font = new Font("Verdana", 8, FontStyle.Bold);
                        e.Item.TextColor = Color.Blue;
                        break;
                }
            }
        }
    }

    private string getIndicaProgramaUnidadeProjeto(DsGanttProjetos.GanttProjetosRow linha, int codigo)
    {

        bool indicaPrograma = false;
        indicaPrograma = (linha.IsCodigoPaiNull() == true && codigo > 0);
        if (indicaPrograma == true)
        {
            return "PRG";
        }
        bool indicaUnidade = codigo < 0;
        if (indicaUnidade == true)
        {
            return "UND";
        }

        return "PRJ";

    }

    private void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        int codigo = Convert.ToInt32(e.SeriesPoint.Argument);
        if (e.Series.Name == "Concluido")
        {
            DsGanttProjetos.GanttProjetosRow row =
                dsGanttProjetos.GanttProjetos.Single(gp => gp.Codigo == codigo);
            e.SeriesDrawOptions.Color = ObtemCor(row.Cor);
            if (row.IsConcluidoNull())
                e.LabelText = string.Empty;
            else
                e.LabelText = string.Format("{0:p0}", row.Concluido / 100);
        }
    }

    private Color ObtemCor(string nomeCor)
    {
        switch (nomeCor.ToLower())
        {
            case "branco":
                return Color.White;
            case "verde":
                return Color.Green;
            case "vermelho":
                return Color.Red;
            case "amarelo":
                return Color.Yellow;
            case "azul":
                return Color.Blue;
            default:
                return Color.Black;
        }
    }

    private void relGanttProjetos_DataSourceDemanded(object sender, EventArgs e)
    {
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        InitData();
    }

    private void xrLogoEntidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DefineLogoEntidade();
    }
}
