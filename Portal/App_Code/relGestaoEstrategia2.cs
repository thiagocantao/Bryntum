using DevExpress.XtraReports.UI;
using System;
using System.Data;

/// <summary>
/// Summary description for relEstruturacaoFormal
/// </summary>
public class relGestaoEstrategia2 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRLabel xrLabel1;

    private DevExpress.XtraReports.Parameters.Parameter pNomeProjeto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeGerente = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pLogoUnidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoEntidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeMapa = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pdataImpressao = new DevExpress.XtraReports.Parameters.Parameter();

    dados cDados = CdadosUtil.GetCdados(null);
    private PageHeaderBand PageHeader;
    private XRPictureBox logoUnidade;
    private XRLabel lblNomeMapa;
    private XRLabel lblDataImpressao;
    //private string dataImpressao = "";
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel3;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLine xrLine1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private dsRelGestaoEstrategia2 dsRelGestaoEstrategia21;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRLabel xrLabel5;
    private dsRelGestaoEstrategia2TableAdapters.Iniciativas2TableAdapter iniciativasTableAdapter1;
    private dsRelGestaoEstrategia2TableAdapters.AnalisePerformanceTableAdapter analisePerformanceTableAdapter1;
    private XRLabel lblCodigoIndicador;
    private dsRelGestaoEstrategia2TableAdapters.MetaIndicadorUnidadeTableAdapter metaIndicadorUnidadeTableAdapter1;
    private GroupHeaderBand GroupHeader2;
    private XRLabel lblCorObjetivoEstrategico;
    private XRLabel lblMeta;
    private XRPictureBox imgCorObjetivoEstrategico;
    private XRLabel lblObjetivo;
    private XRChart xrChart1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLabel lblDesempenho;
    private XRPictureBox imgDesempenhoProjeto;
    private XRPictureBox imgPolaridade;
    private XRLabel lblPolaridade;
    private XRLabel lblMelhorPior;
    private GroupHeaderBand GroupHeader3;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox imgDesempenhoIndicador;
    private XRLabel lblDesempenhoIndicador;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relGestaoEstrategia2(int CodigoMapa, int CodigoUnidade)
    {
        InitializeComponent();
        pNomeProjeto.Name = "pNomeProjeto";
        pNomeGerente.Name = "pNomeGerente";
        pCodigoProjeto.Name = "pCodigoProjeto";
        pLogoUnidade.Name = "pLogoUnidade";
        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeMapa.Name = "pNomeMapa";
        pdataImpressao.Name = "pdataImpressao";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pNomeProjeto,
            this.pNomeGerente,
            this.pCodigoProjeto,
            this.pLogoUnidade,
            this.pCodigoEntidade,
            this.pNomeMapa,
            this.pdataImpressao});

        cDados = CdadosUtil.GetCdados(null);

        metaIndicadorUnidadeTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();
        iniciativasTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();
        analisePerformanceTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();

        metaIndicadorUnidadeTableAdapter1.Fill(dsRelGestaoEstrategia21.MetaIndicadorUnidade, CodigoMapa, CodigoUnidade);
        iniciativasTableAdapter1.Fill(dsRelGestaoEstrategia21.Iniciativas2);
        analisePerformanceTableAdapter1.Fill(dsRelGestaoEstrategia21.AnalisePerformance, CodigoUnidade);

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
        string resourceFileName = "relGestaoEstrategia2.resx";
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.RectangleGradientFillOptions rectangleGradientFillOptions1 = new DevExpress.XtraCharts.RectangleGradientFillOptions();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.RectangleGradientFillOptions rectangleGradientFillOptions2 = new DevExpress.XtraCharts.RectangleGradientFillOptions();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblCodigoIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.logoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblNomeMapa = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblDataImpressao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.dsRelGestaoEstrategia21 = new dsRelGestaoEstrategia2();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.lblDesempenho = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgDesempenhoProjeto = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.iniciativasTableAdapter1 = new dsRelGestaoEstrategia2TableAdapters.Iniciativas2TableAdapter();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.analisePerformanceTableAdapter1 = new dsRelGestaoEstrategia2TableAdapters.AnalisePerformanceTableAdapter();
        this.metaIndicadorUnidadeTableAdapter1 = new dsRelGestaoEstrategia2TableAdapters.MetaIndicadorUnidadeTableAdapter();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblDesempenhoIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.imgDesempenhoIndicador = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblMelhorPior = new DevExpress.XtraReports.UI.XRLabel();
        this.lblPolaridade = new DevExpress.XtraReports.UI.XRLabel();
        this.imgPolaridade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.lblCorObjetivoEstrategico = new DevExpress.XtraReports.UI.XRLabel();
        this.lblMeta = new DevExpress.XtraReports.UI.XRLabel();
        this.imgCorObjetivoEstrategico = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategia21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.Detail.HeightF = 23F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(384.0625F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(21.87497F, 23F);
        // 
        // lblCodigoIndicador
        // 
        this.lblCodigoIndicador.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodigoIndicador]")});
        this.lblCodigoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(791.2084F, 0F);
        this.lblCodigoIndicador.Name = "lblCodigoIndicador";
        this.lblCodigoIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCodigoIndicador.SizeF = new System.Drawing.SizeF(20.375F, 23.00002F);
        this.lblCodigoIndicador.Text = "lblCodigoIndicador";
        this.lblCodigoIndicador.Visible = false;
        this.lblCodigoIndicador.TextChanged += new System.EventHandler(this.lblCodigoIndicador_TextChanged);
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(118F, 3F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(445F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Relatório de Gestão da Estratégia";
        // 
        // logoUnidade
        // 
        this.logoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.logoUnidade.Name = "logoUnidade";
        this.logoUnidade.SizeF = new System.Drawing.SizeF(114F, 46F);
        this.logoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // lblNomeMapa
        // 
        this.lblNomeMapa.BorderColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblNomeMapa.BorderWidth = 1F;
        this.lblNomeMapa.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.lblNomeMapa.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.LocationFloat = new DevExpress.Utils.PointFloat(117F, 28F);
        this.lblNomeMapa.Name = "lblNomeMapa";
        this.lblNomeMapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeMapa.SizeF = new System.Drawing.SizeF(671F, 16F);
        this.lblNomeMapa.StylePriority.UseBorderColor = false;
        this.lblNomeMapa.StylePriority.UseBorders = false;
        this.lblNomeMapa.StylePriority.UseBorderWidth = false;
        this.lblNomeMapa.StylePriority.UseFont = false;
        this.lblNomeMapa.StylePriority.UseForeColor = false;
        this.lblNomeMapa.Text = "lblNomeMapa";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNomeMapa,
            this.logoUnidade,
            this.xrLabel1,
            this.lblDataImpressao,
            this.xrLine1});
        this.PageHeader.HeightF = 60F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblDataImpressao
        // 
        this.lblDataImpressao.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblDataImpressao.ForeColor = System.Drawing.Color.DarkGray;
        this.lblDataImpressao.LocationFloat = new DevExpress.Utils.PointFloat(593F, 50F);
        this.lblDataImpressao.Name = "lblDataImpressao";
        this.lblDataImpressao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataImpressao.SizeF = new System.Drawing.SizeF(194F, 10F);
        this.lblDataImpressao.StylePriority.UseFont = false;
        this.lblDataImpressao.StylePriority.UseForeColor = false;
        this.lblDataImpressao.StylePriority.UseTextAlignment = false;
        this.lblDataImpressao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrLine1
        // 
        this.xrLine1.BorderColor = System.Drawing.Color.Silver;
        this.xrLine1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(789F, 2F);
        this.xrLine1.StylePriority.UseBorderColor = false;
        this.xrLine1.StylePriority.UseBorders = false;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Perspectiva", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 59F;
        this.GroupHeader1.Level = 1;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BorderColor = System.Drawing.Color.Black;
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel3.BorderWidth = 5F;
        this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Perspectiva]")});
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.ForeColor = System.Drawing.Color.Black;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(10F, 11F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel3.SizeF = new System.Drawing.SizeF(774.9792F, 32.99999F);
        this.xrLabel3.StylePriority.UseBorderColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseBorderWidth = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.HeightF = 23F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(738F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(49F, 23F);
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // dsRelGestaoEstrategia21
        // 
        this.dsRelGestaoEstrategia21.DataSetName = "dsRelGestaoEstrategia2";
        this.dsRelGestaoEstrategia21.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 30F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 30F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader3});
        this.DetailReport1.DataAdapter = this.iniciativasTableAdapter1;
        this.DetailReport1.DataMember = "MetaIndicadorUnidade.MetaIndicadorUnidade_Iniciativas2";
        this.DetailReport1.DataSource = this.dsRelGestaoEstrategia21;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDesempenho,
            this.xrTable2});
        this.Detail2.HeightF = 26.5F;
        this.Detail2.Name = "Detail2";
        // 
        // lblDesempenho
        // 
        this.lblDesempenho.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Desempenho]")});
        this.lblDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(794.2084F, 2.000014F);
        this.lblDesempenho.Name = "lblDesempenho";
        this.lblDesempenho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDesempenho.SizeF = new System.Drawing.SizeF(20.37506F, 22.99998F);
        this.lblDesempenho.Text = "lblDesempenho";
        this.lblDesempenho.Visible = false;
        this.lblDesempenho.TextChanged += new System.EventHandler(this.lblDesempenho_TextChanged);
        // 
        // xrTable2
        // 
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(784.9792F, 26.5F);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1.5D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeProjeto]")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Responsavel]")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 0.747109378497723D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Gestor]")});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "Gestor";
        this.xrTableCell7.Weight = 0.752890621502277D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgDesempenhoProjeto});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 0.5D;
        // 
        // imgDesempenhoProjeto
        // 
        this.imgDesempenhoProjeto.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgDesempenhoProjeto.LocationFloat = new DevExpress.Utils.PointFloat(47.49997F, 2F);
        this.imgDesempenhoProjeto.Name = "imgDesempenhoProjeto";
        this.imgDesempenhoProjeto.SizeF = new System.Drawing.SizeF(22.91669F, 23F);
        this.imgDesempenhoProjeto.StylePriority.UseBorders = false;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader3.HeightF = 35F;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(784.98F, 25F);
        this.xrTable1.StylePriority.UseBackColor = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.Text = "Iniciativa";
        this.xrTableCell1.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.Text = "Responsável";
        this.xrTableCell2.Weight = 0.747109378497723D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.Text = "Gestor";
        this.xrTableCell3.Weight = 0.752890621502277D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Text = "Situação";
        this.xrTableCell4.Weight = 0.5D;
        // 
        // iniciativasTableAdapter1
        // 
        this.iniciativasTableAdapter1.ClearBeforeFill = true;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
        this.DetailReport2.DataAdapter = this.analisePerformanceTableAdapter1;
        this.DetailReport2.DataMember = "MetaIndicadorUnidade.MetaIndicadorUnidade_AnalisePerformance";
        this.DetailReport2.DataSource = this.dsRelGestaoEstrategia21;
        this.DetailReport2.Level = 1;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.Detail3.HeightF = 46F;
        this.Detail3.KeepTogetherWithDetailReports = true;
        this.Detail3.Name = "Detail3";
        // 
        // xrLabel5
        // 
        this.xrLabel5.BorderColor = System.Drawing.Color.RosyBrown;
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel5.BorderWidth = 3F;
        this.xrLabel5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Analise]")});
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(2F, 10.99993F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(784.9792F, 23.00002F);
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseBorderWidth = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // analisePerformanceTableAdapter1
        // 
        this.analisePerformanceTableAdapter1.ClearBeforeFill = true;
        // 
        // metaIndicadorUnidadeTableAdapter1
        // 
        this.metaIndicadorUnidadeTableAdapter1.ClearBeforeFill = true;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDesempenhoIndicador,
            this.imgDesempenhoIndicador,
            this.lblMelhorPior,
            this.lblPolaridade,
            this.imgPolaridade,
            this.xrChart1,
            this.lblCorObjetivoEstrategico,
            this.lblMeta,
            this.imgCorObjetivoEstrategico,
            this.lblObjetivo,
            this.lblCodigoIndicador});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Meta", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader2.HeightF = 355F;
        this.GroupHeader2.KeepTogether = true;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // lblDesempenhoIndicador
        // 
        this.lblDesempenhoIndicador.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DesempenhoIndicador]")});
        this.lblDesempenhoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(794.2085F, 82.50002F);
        this.lblDesempenhoIndicador.Name = "lblDesempenhoIndicador";
        this.lblDesempenhoIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDesempenhoIndicador.SizeF = new System.Drawing.SizeF(17.37488F, 23.00002F);
        this.lblDesempenhoIndicador.Text = "lblDesempenhoIndicador";
        this.lblDesempenhoIndicador.Visible = false;
        this.lblDesempenhoIndicador.TextChanged += new System.EventHandler(this.lblDesempenhoIndicador_TextChanged);
        // 
        // imgDesempenhoIndicador
        // 
        this.imgDesempenhoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(6.999998F, 52.54166F);
        this.imgDesempenhoIndicador.Name = "imgDesempenhoIndicador";
        this.imgDesempenhoIndicador.SizeF = new System.Drawing.SizeF(19.37502F, 20.99998F);
        // 
        // lblMelhorPior
        // 
        this.lblMelhorPior.CanShrink = true;
        this.lblMelhorPior.LocationFloat = new DevExpress.Utils.PointFloat(705.7084F, 78.54166F);
        this.lblMelhorPior.Name = "lblMelhorPior";
        this.lblMelhorPior.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblMelhorPior.SizeF = new System.Drawing.SizeF(80.29163F, 20.95836F);
        this.lblMelhorPior.StylePriority.UseTextAlignment = false;
        this.lblMelhorPior.Text = "lblMelhorPior";
        this.lblMelhorPior.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblPolaridade
        // 
        this.lblPolaridade.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Polaridade]")});
        this.lblPolaridade.LocationFloat = new DevExpress.Utils.PointFloat(794.2085F, 55.54164F);
        this.lblPolaridade.Name = "lblPolaridade";
        this.lblPolaridade.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblPolaridade.SizeF = new System.Drawing.SizeF(20.375F, 23.00002F);
        this.lblPolaridade.Text = "lblCodigoIndicador";
        this.lblPolaridade.Visible = false;
        this.lblPolaridade.TextChanged += new System.EventHandler(this.lblPolaridade_TextChanged);
        // 
        // imgPolaridade
        // 
        this.imgPolaridade.ImageUrl = "~\\imagens\\mapaEstrategico\\polaridadeNegativa.png";
        this.imgPolaridade.LocationFloat = new DevExpress.Utils.PointFloat(758.75F, 105.5F);
        this.imgPolaridade.Name = "imgPolaridade";
        this.imgPolaridade.SizeF = new System.Drawing.SizeF(26.22925F, 22.99998F);
        this.imgPolaridade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrChart1
        // 
        this.xrChart1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top;
        this.xrChart1.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xyDiagram1.AxisX.Label.Angle = 330;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.PaneDistance = 9;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.Name = "Default Legend";
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 102.5F);
        this.xrChart1.Name = "xrChart1";
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.Label = sideBySideBarSeriesLabel1;
        series1.Name = "Resultados";
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Gradient;
        rectangleGradientFillOptions1.Color2 = System.Drawing.Color.LightGray;
        rectangleGradientFillOptions1.GradientMode = DevExpress.XtraCharts.RectangleGradientMode.BottomToTop;
        sideBySideBarSeriesView1.FillStyle.Options = rectangleGradientFillOptions1;
        series1.View = sideBySideBarSeriesView1;
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAllAroundPoint;
        pointSeriesLabel1.TextPattern = "{V:F2}";
        series2.Label = pointSeriesLabel1;
        series2.LegendTextPattern = "{V:G}";
        series2.Name = "Metas";
        series2.View = splineSeriesView1;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel2;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Gradient;
        rectangleGradientFillOptions2.Color2 = System.Drawing.Color.DarkSalmon;
        rectangleGradientFillOptions2.GradientMode = DevExpress.XtraCharts.RectangleGradientMode.BottomToTop;
        sideBySideBarSeriesView2.FillStyle.Options = rectangleGradientFillOptions2;
        this.xrChart1.SeriesTemplate.View = sideBySideBarSeriesView2;
        this.xrChart1.SizeF = new System.Drawing.SizeF(756.75F, 251.4583F);
        this.xrChart1.StylePriority.UseBorders = false;
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart1_BeforePrint);
        // 
        // lblCorObjetivoEstrategico
        // 
        this.lblCorObjetivoEstrategico.CanShrink = true;
        this.lblCorObjetivoEstrategico.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorObjetivo]")});
        this.lblCorObjetivoEstrategico.LocationFloat = new DevExpress.Utils.PointFloat(791.2084F, 23.00002F);
        this.lblCorObjetivoEstrategico.Name = "lblCorObjetivoEstrategico";
        this.lblCorObjetivoEstrategico.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCorObjetivoEstrategico.SizeF = new System.Drawing.SizeF(25.41669F, 23F);
        this.lblCorObjetivoEstrategico.Text = "lblCorObjetivoEstrategico";
        this.lblCorObjetivoEstrategico.Visible = false;
        this.lblCorObjetivoEstrategico.TextChanged += new System.EventHandler(this.lblCorObjetivoEstrategico_TextChanged);
        // 
        // lblMeta
        // 
        this.lblMeta.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblMeta.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Meta]")});
        this.lblMeta.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblMeta.LocationFloat = new DevExpress.Utils.PointFloat(31.87498F, 50.54166F);
        this.lblMeta.Name = "lblMeta";
        this.lblMeta.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblMeta.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblMeta.SizeF = new System.Drawing.SizeF(755.125F, 28F);
        this.lblMeta.StylePriority.UseBorders = false;
        this.lblMeta.StylePriority.UseFont = false;
        this.lblMeta.StylePriority.UseTextAlignment = false;
        this.lblMeta.Text = "lblMeta";
        this.lblMeta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // imgCorObjetivoEstrategico
        // 
        this.imgCorObjetivoEstrategico.LocationFloat = new DevExpress.Utils.PointFloat(5.999998F, 15.00001F);
        this.imgCorObjetivoEstrategico.Name = "imgCorObjetivoEstrategico";
        this.imgCorObjetivoEstrategico.SizeF = new System.Drawing.SizeF(21.87497F, 23F);
        // 
        // lblObjetivo
        // 
        this.lblObjetivo.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblObjetivo.BorderWidth = 5F;
        this.lblObjetivo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivo]")});
        this.lblObjetivo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblObjetivo.ForeColor = System.Drawing.Color.DarkGoldenrod;
        this.lblObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(31.87498F, 9.541669F);
        this.lblObjetivo.Name = "lblObjetivo";
        this.lblObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblObjetivo.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblObjetivo.SizeF = new System.Drawing.SizeF(755.125F, 32.99999F);
        this.lblObjetivo.StylePriority.UseBorderColor = false;
        this.lblObjetivo.StylePriority.UseBorders = false;
        this.lblObjetivo.StylePriority.UseBorderWidth = false;
        this.lblObjetivo.StylePriority.UseFont = false;
        this.lblObjetivo.StylePriority.UseForeColor = false;
        this.lblObjetivo.Text = "lblObjetivo";
        // 
        // relGestaoEstrategia2
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.GroupHeader1,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.DetailReport1,
            this.DetailReport2,
            this.GroupHeader2});
        this.DataAdapter = this.metaIndicadorUnidadeTableAdapter1;
        this.DataMember = "MetaIndicadorUnidade";
        this.DataSource = this.dsRelGestaoEstrategia21;
        this.DrawGrid = false;
        this.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
        this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        this.Version = "19.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategia21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeMapa.Text = pNomeMapa.Value.ToString();
        logoUnidade.ImageUrl = pLogoUnidade.Value.ToString();
        lblDataImpressao.Text = pdataImpressao.Value.ToString();
    }



    private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string codIndicador = lblCodigoIndicador.Text;//(linhaAtual as System.Data.DataRowView).Row.Table.Rows[rowIndex]["CodigoIndicador"].ToString();
        int erroInt = 0;
        if (int.TryParse(codIndicador, out erroInt))
        {

            DataTable dtCampos = cDados.getDadosIndicador(int.Parse(codIndicador), "").Tables[0];

            int casasDecimais = 0;

            string unidadeMedida = "";
            if (cDados.DataTableOk(dtCampos))
            {
                casasDecimais = int.TryParse(dtCampos.Rows[0]["CasasDecimais"].ToString(), out erroInt) ? int.Parse(dtCampos.Rows[0]["CasasDecimais"].ToString()) : 0;
                unidadeMedida = dtCampos.Rows[0]["SiglaUnidadeMedida"].ToString();
            }
            //alterar essa função para que dado um valor de meta, seja funcional
            DataSet ds = cDados.getPeriodicidadeIndicadorPorMeta(int.Parse(pCodigoEntidade.Value.ToString()), lblMeta.Text, "");

            xrChart1.DataSource = ds.Tables[0];

            xrChart1.Series[0].ArgumentDataMember = "Periodo";
            xrChart1.Series[0].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            xrChart1.Series[0].LegendText = "Realizado" + "(" + unidadeMedida + ")";


            xrChart1.Series[1].ArgumentDataMember = "Periodo";
            xrChart1.Series[1].ValueDataMembers.AddRange(new string[] { "ValorPrevisto" });
            xrChart1.Series[1].LegendText = "Meta" + "(" + unidadeMedida + ")";

            xrChart1.Series[0].Label.TextPattern = "{V:F" + casasDecimais + "}";
            xrChart1.Series[1].Label.TextPattern = "{V:F" + casasDecimais + "}";
        }

        /*string cor = lblDesempenhoIndicador.Text;
        if (cor == "Vermelho")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/Azul.gif";
        if (cor == "Branco")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/Branco.gif";*/
    }


    private void imgCorObjetivoEstrategico_AfterPrint(object sender, EventArgs e)
    {
        /* string cor = lblCorObjetivoEstrategico.Text;
         cor = cor.Trim();
         if (cor == "Branco")
             imgCorObjetivoEstrategico.ImageUrl = "~/imagens/Branco.gif";
         if (cor == "Vermelho")
             imgCorObjetivoEstrategico.ImageUrl = "~/imagens/vermelho.gif";
         if (cor == "Verde")
             imgCorObjetivoEstrategico.ImageUrl = "~/imagens/verde.gif";
         if (cor == "Amarelo")
             imgCorObjetivoEstrategico.ImageUrl = "~/imagens/amarelo.gif";
         if (cor == "Azul")
             imgCorObjetivoEstrategico.ImageUrl = "~/imagens/azul.gif";*/
    }

    private void imgCorObjetivoEstrategico_Draw(object sender, DrawEventArgs e)
    {
        /*string cor = lblCorObjetivoEstrategico.Text;
        cor = cor.Trim();
        if (cor == "Branco")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/azul.gif";*/
    }

    private void lblCorObjetivoEstrategico_TextChanged(object sender, EventArgs e)
    {
        string cor = lblCorObjetivoEstrategico.Text;
        cor = cor.Trim();
        if (cor == "Branco")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgCorObjetivoEstrategico.ImageUrl = "~/imagens/azul.gif";
    }

    private void lblDesempenho_TextChanged(object sender, EventArgs e)
    {
        string cor = lblDesempenho.Text.Trim();

        if (cor == "Vermelho")
            imgDesempenhoProjeto.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgDesempenhoProjeto.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgDesempenhoProjeto.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgDesempenhoProjeto.ImageUrl = "~/imagens/Azul.gif";
    }

    private void lblPolaridade_TextChanged(object sender, EventArgs e)
    {
        if (lblPolaridade.Text != "")
        {
            imgPolaridade.Visible = true;
            lblMelhorPior.Visible = true;
            imgPolaridade.ImageUrl = lblPolaridade.Text == "NEG" ? "~/imagens/mapaEstrategico/polaridadeNegativa.png" : "~/imagens/mapaEstrategico/polaridadePositiva.png";
            lblMelhorPior.Text = lblPolaridade.Text == "NEG" ? "Pior" : "Melhor";
        }
        else
        {
            imgPolaridade.Visible = false;
            lblMelhorPior.Visible = false;
        }
    }

    private void lblCodigoIndicador_TextChanged(object sender, EventArgs e)
    {
        if (lblCodigoIndicador.Text == "")
        {
            xrChart1.Visible = false;
        }
    }

    private void lblDesempenhoIndicador_TextChanged(object sender, EventArgs e)
    {
        string cor = lblDesempenhoIndicador.Text.Trim();

        if (cor == "Vermelho")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/Azul.gif";
    }
}
