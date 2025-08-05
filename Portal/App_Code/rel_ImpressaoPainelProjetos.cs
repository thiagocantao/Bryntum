using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for rel_ImpressaoPainelProjetos
/// </summary>
public class rel_ImpressaoPainelProjetos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    int codigoCarteira;
    int idUsuarioLogado;
    int codigoEntidade;
    int anoContabil;
    dados cDados;
    private dsImpressaoPainelProjetos dsImpressaoPainelProjetos1;
    private XRChart chartProjetosDesempenhoUnidade;
    private XRChart chartProjetosDespesaUnidade;
    private XRLabel xrLabel5;
    private XRChart chartProjetosReceitaUnidade;
    private XRLabel xrLabel6;
    private XRChart chartProjetosHistoricoDesempenho;
    private XRChart chartProjetosDesempenho;
    private XRLabel xrLabel2;
    private XRChart chartDesempGeralRealizado;
    private XRChart chartDesempGeralPrevisto;
    private XRLabel xrLabel1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel7;
    private PageHeaderBand PageHeader;
    private XRControlStyle xrControlStyle1;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo2;
    private XRLabel lblCarteiraDeProjetos;
    private XRPageInfo xrPageInfo1;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrLogoEntidade;
    //private XRChart xrChart7;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    /// 

    private System.ComponentModel.IContainer components = null;

    public rel_ImpressaoPainelProjetos(string anoFinanceiro)
    {
        InitializeComponent();
        InitData(anoFinanceiro);
        //
        // TODO: Add constructor logic here
        //
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
        string resourceFileName = "rel_ImpressaoPainelProjetos.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_ImpressaoPainelProjetos.ResourceManager;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView2 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView3 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView4 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView5 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D1 = new DevExpress.XtraCharts.SimpleDiagram3D();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel1 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView1 = new DevExpress.XtraCharts.Pie3DSeriesView();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel2 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView2 = new DevExpress.XtraCharts.Pie3DSeriesView();
        DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView6 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView7 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView8 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.Series series11 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView9 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView10 = new DevExpress.XtraCharts.StackedBarSeriesView();
        DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series12 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series13 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel7 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series14 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel8 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series15 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel9 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView6 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel10 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.chartProjetosHistoricoDesempenho = new DevExpress.XtraReports.UI.XRChart();
        this.dsImpressaoPainelProjetos1 = new dsImpressaoPainelProjetos();
        this.chartProjetosDesempenho = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.chartDesempGeralRealizado = new DevExpress.XtraReports.UI.XRChart();
        this.chartDesempGeralPrevisto = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.chartProjetosDesempenhoUnidade = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.chartProjetosDespesaUnidade = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.chartProjetosReceitaUnidade = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblCarteiraDeProjetos = new DevExpress.XtraReports.UI.XRLabel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosHistoricoDesempenho)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoPainelProjetos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDesempenho)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartDesempGeralRealizado)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartDesempGeralPrevisto)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDesempenhoUnidade)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDespesaUnidade)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosReceitaUnidade)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.chartProjetosHistoricoDesempenho,
            this.chartProjetosDesempenho,
            this.xrLabel2,
            this.chartDesempGeralRealizado,
            this.chartDesempGeralPrevisto,
            this.xrLabel1,
            this.xrLabel4,
            this.chartProjetosDesempenhoUnidade,
            this.xrLabel5,
            this.chartProjetosDespesaUnidade,
            this.xrLabel6,
            this.chartProjetosReceitaUnidade,
            this.xrLabel3});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1709.076F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // chartProjetosHistoricoDesempenho
        // 
        this.chartProjetosHistoricoDesempenho.BackColor = System.Drawing.Color.Black;
        this.chartProjetosHistoricoDesempenho.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartProjetosHistoricoDesempenho.BackImage.Image")));
        this.chartProjetosHistoricoDesempenho.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartProjetosHistoricoDesempenho.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.chartProjetosHistoricoDesempenho.DataMember = "ProjetosHistoricoDesempenho";
        this.chartProjetosHistoricoDesempenho.DataSource = this.dsImpressaoPainelProjetos1;
        xyDiagram1.AxisX.Label.Angle = -60;
        xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Verdana", 6F);
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
        xyDiagram1.AxisX.Tickmarks.Visible = false;
        xyDiagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram1.AxisX.Title.Text = "Fisico (%)";
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.DateTimeScaleOptions.GridSpacing = 10D;
        xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 10D;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosHistoricoDesempenho.Diagram = xyDiagram1;
        this.chartProjetosHistoricoDesempenho.Dpi = 254F;
        this.chartProjetosHistoricoDesempenho.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
        this.chartProjetosHistoricoDesempenho.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartProjetosHistoricoDesempenho.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartProjetosHistoricoDesempenho.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartProjetosHistoricoDesempenho.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartProjetosHistoricoDesempenho.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosHistoricoDesempenho.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartProjetosHistoricoDesempenho.Legend.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        this.chartProjetosHistoricoDesempenho.Legend.Font = new System.Drawing.Font("Verdana", 7F);
        this.chartProjetosHistoricoDesempenho.Legend.HorizontalIndent = 25;
        this.chartProjetosHistoricoDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(0F, 900.6643F);
        this.chartProjetosHistoricoDesempenho.Name = "chartProjetosHistoricoDesempenho";
        series1.ArgumentDataMember = "codigoOrdenacao";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.LegendTextPattern = "{A}";
        series1.Name = "Crítico";
        series1.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series1.ValueDataMembersSerializable = "Vermelhos";
        stackedBarSeriesView1.Color = System.Drawing.Color.Red;
        stackedBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = stackedBarSeriesView1;
        series2.ArgumentDataMember = "codigoOrdenacao";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.LegendTextPattern = "{A}";
        series2.Name = "Atenção";
        series2.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series2.ValueDataMembersSerializable = "Amarelos";
        stackedBarSeriesView2.Color = System.Drawing.Color.Yellow;
        stackedBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series2.View = stackedBarSeriesView2;
        series3.ArgumentDataMember = "codigoOrdenacao";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.LegendTextPattern = "{A}";
        series3.Name = "Satisfatório";
        series3.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
        series3.ValueDataMembersSerializable = "Verdes";
        stackedBarSeriesView3.Color = System.Drawing.Color.Green;
        stackedBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series3.View = stackedBarSeriesView3;
        series4.ArgumentDataMember = "codigoOrdenacao";
        series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.LegendTextPattern = "{A}";
        series4.Name = "Sem Infomações";
        series4.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
        series4.ValueDataMembersSerializable = "Sem_Acompanhamento";
        stackedBarSeriesView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
        stackedBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series4.View = stackedBarSeriesView4;
        this.chartProjetosHistoricoDesempenho.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4};
        this.chartProjetosHistoricoDesempenho.SeriesTemplate.View = stackedBarSeriesView5;
        this.chartProjetosHistoricoDesempenho.SizeF = new System.Drawing.SizeF(940.0801F, 786.34F);
        this.chartProjetosHistoricoDesempenho.StylePriority.UseBackColor = false;
        this.chartProjetosHistoricoDesempenho.StylePriority.UseBorderColor = false;
        this.chartProjetosHistoricoDesempenho.StylePriority.UseBorders = false;
        this.chartProjetosHistoricoDesempenho.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartProjetosHistoricoDesempenho_CustomDrawAxisLabel);
        // 
        // dsImpressaoPainelProjetos1
        // 
        this.dsImpressaoPainelProjetos1.DataSetName = "dsImpressaoPainelProjetos";
        this.dsImpressaoPainelProjetos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // chartProjetosDesempenho
        // 
        this.chartProjetosDesempenho.BackColor = System.Drawing.Color.Black;
        this.chartProjetosDesempenho.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartProjetosDesempenho.BackImage.Image")));
        this.chartProjetosDesempenho.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartProjetosDesempenho.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.chartProjetosDesempenho.BorderWidth = 1F;
        this.chartProjetosDesempenho.DataMember = "ProjetosDesempenho";
        this.chartProjetosDesempenho.DataSource = this.dsImpressaoPainelProjetos1;
        simpleDiagram3D1.RotationMatrixSerializable = "0.996677012339625;-0.00891018582266209;0.0809662995471823;0;0.0126441098312279;0." +
"998874180049239;-0.0457219741211854;0;-0.0804677547862139;0.0465937873494753;0.9" +
"95667594842827;0;0;0;0;1";
        this.chartProjetosDesempenho.Diagram = simpleDiagram3D1;
        this.chartProjetosDesempenho.Dpi = 254F;
        this.chartProjetosDesempenho.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
        this.chartProjetosDesempenho.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartProjetosDesempenho.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartProjetosDesempenho.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartProjetosDesempenho.Legend.EnableAntialiasing = DefaultBoolean.True;
        this.chartProjetosDesempenho.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosDesempenho.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.chartProjetosDesempenho.Legend.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        this.chartProjetosDesempenho.Legend.Font = new System.Drawing.Font("Verdana", 7F);
        this.chartProjetosDesempenho.Legend.MarkerSize = new System.Drawing.Size(23, 16);
        this.chartProjetosDesempenho.Legend.TextOffset = 10;
        this.chartProjetosDesempenho.LocationFloat = new DevExpress.Utils.PointFloat(954.9501F, 58.42004F);
        this.chartProjetosDesempenho.Name = "chartProjetosDesempenho";
        series5.ArgumentDataMember = "Situacao";
        series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pie3DSeriesLabel1.BackColor = System.Drawing.Color.Transparent;
        pie3DSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        pie3DSeriesLabel1.Font = new System.Drawing.Font("Verdana", 7F);
        pie3DSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pie3DSeriesLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        pie3DSeriesLabel1.TextPattern = "{V:N0}";
        series5.Label = pie3DSeriesLabel1;
        series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        series5.LegendText = "legenda";
        series5.LegendTextPattern = "{A}";
        series5.Name = "serieUnica";
        series5.ValueDataMembersSerializable = "Quantidade";
        pie3DSeriesView1.Depth = 1;
        pie3DSeriesView1.ExplodeMode = DevExpress.XtraCharts.PieExplodeMode.UsePoints;
        pie3DSeriesView1.PieFillStyle.FillMode = DevExpress.XtraCharts.FillMode3D.Solid;
        series5.View = pie3DSeriesView1;
        this.chartProjetosDesempenho.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
        pie3DSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.chartProjetosDesempenho.SeriesTemplate.Label = pie3DSeriesLabel2;
        pie3DSeriesView2.PieFillStyle.FillMode = DevExpress.XtraCharts.FillMode3D.Solid;
        this.chartProjetosDesempenho.SeriesTemplate.View = pie3DSeriesView2;
        this.chartProjetosDesempenho.SizeF = new System.Drawing.SizeF(934.9996F, 757.1921F);
        this.chartProjetosDesempenho.StylePriority.UseBackColor = false;
        this.chartProjetosDesempenho.StylePriority.UseBorderColor = false;
        this.chartProjetosDesempenho.StylePriority.UseBorders = false;
        this.chartProjetosDesempenho.StylePriority.UseBorderWidth = false;
        this.chartProjetosDesempenho.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartProjetosDesempenho_CustomDrawSeriesPoint);
        this.chartProjetosDesempenho.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chartProjetosDesempenho_BeforePrint);
        // 
        // xrLabel2
        // 
        this.xrLabel2.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel2.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(954.9501F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(935F, 58.42F);
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseBorderColor = false;
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Projetos - Desempenho";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chartDesempGeralRealizado
        // 
        this.chartDesempGeralRealizado.BackColor = System.Drawing.Color.Black;
        this.chartDesempGeralRealizado.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartDesempGeralRealizado.BackImage.Image")));
        this.chartDesempGeralRealizado.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartDesempGeralRealizado.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xyDiagram2.AxisX.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram2.AxisX.MinorCount = 1;
        xyDiagram2.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 6F);
        xyDiagram2.AxisX.Title.Text = "Receita (R$)";
        xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram2.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram2.AxisY.MinorCount = 4;
        xyDiagram2.AxisY.Thickness = 2;
        xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram2.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram2.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.PaneDistance = 0;
        xyDiagram2.Rotated = true;
        this.chartDesempGeralRealizado.Diagram = xyDiagram2;
        this.chartDesempGeralRealizado.Dpi = 254F;
        this.chartDesempGeralRealizado.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartDesempGeralRealizado.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartDesempGeralRealizado.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartDesempGeralRealizado.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartDesempGeralRealizado.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartDesempGeralRealizado.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartDesempGeralRealizado.Legend.HorizontalIndent = 25;
        this.chartDesempGeralRealizado.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartDesempGeralRealizado.LocationFloat = new DevExpress.Utils.PointFloat(0F, 419.4709F);
        this.chartDesempGeralRealizado.Name = "chartDesempGeralRealizado";
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series6.Label = sideBySideBarSeriesLabel1;
        series6.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series6.Name = "Series 1";
        sideBySideBarSeriesView1.Color = System.Drawing.Color.Green;
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series6.View = sideBySideBarSeriesView1;
        this.chartDesempGeralRealizado.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series6};
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.chartDesempGeralRealizado.SeriesTemplate.Label = sideBySideBarSeriesLabel2;
        this.chartDesempGeralRealizado.SizeF = new System.Drawing.SizeF(940.0801F, 398.1413F);
        this.chartDesempGeralRealizado.StylePriority.UseBackColor = false;
        this.chartDesempGeralRealizado.StylePriority.UseBorderColor = false;
        this.chartDesempGeralRealizado.StylePriority.UseBorders = false;
        this.chartDesempGeralRealizado.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartDesempGeralPrevisto_CustomDrawAxisLabel);
        this.chartDesempGeralRealizado.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chartDesempGeralRealizado_BeforePrint);
        // 
        // chartDesempGeralPrevisto
        // 
        this.chartDesempGeralPrevisto.BackColor = System.Drawing.Color.Black;
        this.chartDesempGeralPrevisto.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartDesempGeralPrevisto.BackImage.Image")));
        this.chartDesempGeralPrevisto.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartDesempGeralPrevisto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xyDiagram3.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram3.AxisX.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram3.AxisX.MinorCount = 1;
        xyDiagram3.AxisX.Tickmarks.Visible = false;
        xyDiagram3.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram3.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 6F);
        xyDiagram3.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram3.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram3.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram3.AxisY.MinorCount = 4;
        xyDiagram3.AxisY.Thickness = 2;
        xyDiagram3.AxisY.Title.Text = "Despesa (R$)";
        xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram3.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram3.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.PaneDistance = 0;
        xyDiagram3.Rotated = true;
        this.chartDesempGeralPrevisto.Diagram = xyDiagram3;
        this.chartDesempGeralPrevisto.Dpi = 254F;
        this.chartDesempGeralPrevisto.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartDesempGeralPrevisto.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartDesempGeralPrevisto.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartDesempGeralPrevisto.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartDesempGeralPrevisto.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartDesempGeralPrevisto.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartDesempGeralPrevisto.Legend.HorizontalIndent = 25;
        this.chartDesempGeralPrevisto.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartDesempGeralPrevisto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.41996F);
        this.chartDesempGeralPrevisto.Name = "chartDesempGeralPrevisto";
        series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series7.Label = sideBySideBarSeriesLabel3;
        series7.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series7.LegendTextPattern = "{V:G}";
        series7.Name = "SerieDespesa";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Green;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series7.View = sideBySideBarSeriesView2;
        this.chartDesempGeralPrevisto.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7};
        this.chartDesempGeralPrevisto.SeriesTemplate.ArgumentDataMember = "TotalCustoOrcado";
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartDesempGeralPrevisto.SeriesTemplate.Label = sideBySideBarSeriesLabel4;
        this.chartDesempGeralPrevisto.SeriesTemplate.ValueDataMembersSerializable = "TotalCustoOrcado";
        this.chartDesempGeralPrevisto.SizeF = new System.Drawing.SizeF(940.0801F, 346.1682F);
        this.chartDesempGeralPrevisto.StylePriority.UseBackColor = false;
        this.chartDesempGeralPrevisto.StylePriority.UseBorderColor = false;
        this.chartDesempGeralPrevisto.StylePriority.UseBorders = false;
        this.chartDesempGeralPrevisto.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartDesempGeralPrevisto_CustomDrawAxisLabel);
        this.chartDesempGeralPrevisto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chartDesempGeralPrevisto_BeforePrint);
        // 
        // xrLabel1
        // 
        this.xrLabel1.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel1.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(940.08F, 58.41998F);
        this.xrLabel1.StylePriority.UseBackColor = false;
        this.xrLabel1.StylePriority.UseBorderColor = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Projetos - Desempenho Geral";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel4.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel4.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 842.2443F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(940.0801F, 58.41998F);
        this.xrLabel4.StylePriority.UseBackColor = false;
        this.xrLabel4.StylePriority.UseBorderColor = false;
        this.xrLabel4.StylePriority.UseBorderDashStyle = false;
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Projetos - Histórico/Desempenho";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chartProjetosDesempenhoUnidade
        // 
        this.chartProjetosDesempenhoUnidade.BackColor = System.Drawing.Color.Black;
        this.chartProjetosDesempenhoUnidade.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartProjetosDesempenhoUnidade.BackImage.Image")));
        this.chartProjetosDesempenhoUnidade.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartProjetosDesempenhoUnidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.chartProjetosDesempenhoUnidade.DataMember = "ProjetosDesempenhoUnidade";
        this.chartProjetosDesempenhoUnidade.DataSource = this.dsImpressaoPainelProjetos1;
        xyDiagram4.AxisX.Label.Angle = -60;
        xyDiagram4.AxisX.MinorCount = 1;
        xyDiagram4.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram4.AxisX.Title.Font = new System.Drawing.Font("Verdana", 6F);
        xyDiagram4.AxisX.Title.Text = "Fisico (%)";
        xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram4.AxisY.DateTimeScaleOptions.GridSpacing = 2D;
        xyDiagram4.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram4.AxisY.MinorCount = 1;
        xyDiagram4.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram4.AxisY.NumericScaleOptions.GridSpacing = 2D;
        xyDiagram4.AxisY.Tickmarks.MinorVisible = false;
        xyDiagram4.AxisY.Tickmarks.Visible = false;
        xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.WholeRange.AutoSideMargins = false;
        xyDiagram4.AxisY.WholeRange.SideMarginsValue = 0D;
        xyDiagram4.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram4.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        this.chartProjetosDesempenhoUnidade.Diagram = xyDiagram4;
        this.chartProjetosDesempenhoUnidade.Dpi = 254F;
        this.chartProjetosDesempenhoUnidade.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartProjetosDesempenhoUnidade.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartProjetosDesempenhoUnidade.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartProjetosDesempenhoUnidade.Legend.EnableAntialiasing = DefaultBoolean.True;
        this.chartProjetosDesempenhoUnidade.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartProjetosDesempenhoUnidade.Legend.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
        this.chartProjetosDesempenhoUnidade.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosDesempenhoUnidade.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartProjetosDesempenhoUnidade.Legend.HorizontalIndent = 25;
        this.chartProjetosDesempenhoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(1904.82F, 58.42004F);
        this.chartProjetosDesempenhoUnidade.Name = "chartProjetosDesempenhoUnidade";
        series8.ArgumentDataMember = "Codigo";
        series8.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series8.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series8.LegendTextPattern = "{A}";
        series8.Name = "Crítico";
        series8.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series8.ValueDataMembersSerializable = "Vermelhos";
        stackedBarSeriesView6.Color = System.Drawing.Color.Red;
        stackedBarSeriesView6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series8.View = stackedBarSeriesView6;
        series9.ArgumentDataMember = "Codigo";
        series9.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series9.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series9.LegendTextPattern = "{A}";
        series9.Name = "Atenção";
        series9.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series9.ValueDataMembersSerializable = "Amarelos";
        stackedBarSeriesView7.Color = System.Drawing.Color.Yellow;
        stackedBarSeriesView7.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series9.View = stackedBarSeriesView7;
        series10.ArgumentDataMember = "Codigo";
        series10.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series10.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series10.LegendTextPattern = "{A}";
        series10.Name = "Satisfatório";
        series10.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series10.ValueDataMembersSerializable = "Verdes";
        stackedBarSeriesView8.Color = System.Drawing.Color.Green;
        stackedBarSeriesView8.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series10.View = stackedBarSeriesView8;
        series11.ArgumentDataMember = "Codigo";
        series11.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series11.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series11.LegendTextPattern = "{A}";
        series11.Name = "Sem Informações";
        series11.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
        series11.ValueDataMembersSerializable = "Sem_Acompanhamento";
        stackedBarSeriesView9.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
        stackedBarSeriesView9.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series11.View = stackedBarSeriesView9;
        this.chartProjetosDesempenhoUnidade.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series8,
        series9,
        series10,
        series11};
        this.chartProjetosDesempenhoUnidade.SeriesTemplate.View = stackedBarSeriesView10;
        this.chartProjetosDesempenhoUnidade.SizeF = new System.Drawing.SizeF(937.1798F, 757.1921F);
        this.chartProjetosDesempenhoUnidade.StylePriority.UseBackColor = false;
        this.chartProjetosDesempenhoUnidade.StylePriority.UseBorderColor = false;
        this.chartProjetosDesempenhoUnidade.StylePriority.UseBorders = false;
        this.chartProjetosDesempenhoUnidade.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartProjetosDesempenhoUnidade_CustomDrawAxisLabel);
        // 
        // xrLabel5
        // 
        this.xrLabel5.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel5.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(954.9503F, 842.2443F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(936.18F, 58.41992F);
        this.xrLabel5.StylePriority.UseBackColor = false;
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Projetos - Despesa/Unidade (R$)";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chartProjetosDespesaUnidade
        // 
        this.chartProjetosDespesaUnidade.BackColor = System.Drawing.Color.Black;
        this.chartProjetosDespesaUnidade.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartProjetosDespesaUnidade.BackImage.Image")));
        this.chartProjetosDespesaUnidade.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartProjetosDespesaUnidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.chartProjetosDespesaUnidade.DataMember = "ProjetosDespesaUnidade";
        this.chartProjetosDespesaUnidade.DataSource = this.dsImpressaoPainelProjetos1;
        xyDiagram5.AxisX.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram5.AxisX.MinorCount = 1;
        xyDiagram5.AxisX.Reverse = true;
        xyDiagram5.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram5.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram5.AxisX.Title.Text = "Fisico (%)";
        xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram5.AxisY.InterlacedFillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        xyDiagram5.AxisY.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram5.AxisY.Label.TextPattern = "{V:G}";
        xyDiagram5.AxisY.MinorCount = 1;
        xyDiagram5.AxisY.Tickmarks.MinorVisible = false;
        xyDiagram5.AxisY.Tickmarks.Visible = false;
        xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram5.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram5.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.Rotated = true;
        this.chartProjetosDespesaUnidade.Diagram = xyDiagram5;
        this.chartProjetosDespesaUnidade.Dpi = 254F;
        this.chartProjetosDespesaUnidade.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
        this.chartProjetosDespesaUnidade.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartProjetosDespesaUnidade.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartProjetosDespesaUnidade.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartProjetosDespesaUnidade.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartProjetosDespesaUnidade.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosDespesaUnidade.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartProjetosDespesaUnidade.Legend.Font = new System.Drawing.Font("Verdana", 7F);
        this.chartProjetosDespesaUnidade.Legend.HorizontalIndent = 25;
        this.chartProjetosDespesaUnidade.LocationFloat = new DevExpress.Utils.PointFloat(954.9501F, 900.6643F);
        this.chartProjetosDespesaUnidade.Name = "chartProjetosDespesaUnidade";
        series12.ArgumentDataMember = "Codigo";
        series12.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series12.Label = sideBySideBarSeriesLabel5;
        series12.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series12.Name = "Realizado";
        series12.ValueDataMembersSerializable = "CustoReal";
        sideBySideBarSeriesView3.Border.Color = System.Drawing.Color.Black;
        sideBySideBarSeriesView3.Color = System.Drawing.Color.Blue;
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series12.View = sideBySideBarSeriesView3;
        series13.ArgumentDataMember = "Codigo";
        series13.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series13.Label = sideBySideBarSeriesLabel6;
        series13.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series13.Name = "Previsto";
        series13.ValueDataMembersSerializable = "CustoPrevisto";
        sideBySideBarSeriesView4.Border.Color = System.Drawing.Color.Black;
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Gray;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series13.View = sideBySideBarSeriesView4;
        this.chartProjetosDespesaUnidade.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series12,
        series13};
        sideBySideBarSeriesLabel7.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.chartProjetosDespesaUnidade.SeriesTemplate.Label = sideBySideBarSeriesLabel7;
        this.chartProjetosDespesaUnidade.SizeF = new System.Drawing.SizeF(936.1801F, 786.3401F);
        this.chartProjetosDespesaUnidade.StylePriority.UseBackColor = false;
        this.chartProjetosDespesaUnidade.StylePriority.UseBorderColor = false;
        this.chartProjetosDespesaUnidade.StylePriority.UseBorders = false;
        this.chartProjetosDespesaUnidade.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartProjetosDespesaUnidade_CustomDrawAxisLabel);
        // 
        // xrLabel6
        // 
        this.xrLabel6.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel6.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1903.64F, 842.2443F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(938.36F, 58.41992F);
        this.xrLabel6.StylePriority.UseBackColor = false;
        this.xrLabel6.StylePriority.UseBorderColor = false;
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Projetos - Receita/Unidade (R$)";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chartProjetosReceitaUnidade
        // 
        this.chartProjetosReceitaUnidade.BackColor = System.Drawing.Color.Black;
        this.chartProjetosReceitaUnidade.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("chartProjetosReceitaUnidade.BackImage.Image")));
        this.chartProjetosReceitaUnidade.BorderColor = System.Drawing.Color.Gainsboro;
        this.chartProjetosReceitaUnidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.chartProjetosReceitaUnidade.DataMember = "ProjetosReceitaUnidade";
        this.chartProjetosReceitaUnidade.DataSource = this.dsImpressaoPainelProjetos1;
        xyDiagram6.AxisX.AutoScaleBreaks.Enabled = true;
        xyDiagram6.AxisX.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram6.AxisX.MinorCount = 1;
        xyDiagram6.AxisX.Reverse = true;
        xyDiagram6.AxisX.Tickmarks.MinorVisible = false;
        xyDiagram6.AxisX.Tickmarks.Visible = false;
        xyDiagram6.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram6.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram6.AxisX.Title.Text = "Fisico (%)";
        xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram6.AxisY.Label.Font = new System.Drawing.Font("Verdana", 7F);
        xyDiagram6.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram6.AxisY.MinorCount = 1;
        xyDiagram6.AxisY.Tickmarks.MinorVisible = false;
        xyDiagram6.AxisY.Tickmarks.Visible = false;
        xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram6.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram6.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.Rotated = true;
        this.chartProjetosReceitaUnidade.Diagram = xyDiagram6;
        this.chartProjetosReceitaUnidade.Dpi = 254F;
        this.chartProjetosReceitaUnidade.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
        this.chartProjetosReceitaUnidade.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.chartProjetosReceitaUnidade.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.chartProjetosReceitaUnidade.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.chartProjetosReceitaUnidade.Legend.BackColor = System.Drawing.Color.Transparent;
        this.chartProjetosReceitaUnidade.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.chartProjetosReceitaUnidade.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.chartProjetosReceitaUnidade.Legend.Font = new System.Drawing.Font("Verdana", 7F);
        this.chartProjetosReceitaUnidade.Legend.HorizontalIndent = 25;
        this.chartProjetosReceitaUnidade.LocationFloat = new DevExpress.Utils.PointFloat(1903.64F, 900.6643F);
        this.chartProjetosReceitaUnidade.Name = "chartProjetosReceitaUnidade";
        series14.ArgumentDataMember = "Codigo";
        series14.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel8.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series14.Label = sideBySideBarSeriesLabel8;
        series14.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series14.LegendTextPattern = "{V:N0}";
        series14.Name = "Realizado";
        series14.ValueDataMembersSerializable = "ReceitaReal";
        sideBySideBarSeriesView5.Color = System.Drawing.Color.Blue;
        sideBySideBarSeriesView5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series14.View = sideBySideBarSeriesView5;
        series15.ArgumentDataMember = "Codigo";
        series15.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel9.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series15.Label = sideBySideBarSeriesLabel9;
        series15.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series15.LegendTextPattern = "{V:N0}";
        series15.Name = "Previsto";
        series15.ValueDataMembersSerializable = "ReceitaPrevista";
        sideBySideBarSeriesView6.Border.Color = System.Drawing.Color.Black;
        sideBySideBarSeriesView6.Color = System.Drawing.Color.Gray;
        sideBySideBarSeriesView6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series15.View = sideBySideBarSeriesView6;
        this.chartProjetosReceitaUnidade.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series14,
        series15};
        sideBySideBarSeriesLabel10.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.chartProjetosReceitaUnidade.SeriesTemplate.Label = sideBySideBarSeriesLabel10;
        this.chartProjetosReceitaUnidade.SizeF = new System.Drawing.SizeF(938.3601F, 786.3398F);
        this.chartProjetosReceitaUnidade.StylePriority.UseBackColor = false;
        this.chartProjetosReceitaUnidade.StylePriority.UseBorderColor = false;
        this.chartProjetosReceitaUnidade.StylePriority.UseBorders = false;
        this.chartProjetosReceitaUnidade.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartProjetosReceitaUnidade_CustomDrawAxisLabel);
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel3.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1904.82F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(937.1796F, 58.42F);
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseBorderColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Projetos - Desempenho/Unidade";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 64F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 64F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(2518.368F, 58.42F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Relatório de Status de Projetos";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.lblCarteiraDeProjetos,
            this.xrLabel7});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 177F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(2518.368F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(323.6323F, 152F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // lblCarteiraDeProjetos
        // 
        this.lblCarteiraDeProjetos.Dpi = 254F;
        this.lblCarteiraDeProjetos.Font = new System.Drawing.Font("Verdana", 15F);
        this.lblCarteiraDeProjetos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96.51999F);
        this.lblCarteiraDeProjetos.Name = "lblCarteiraDeProjetos";
        this.lblCarteiraDeProjetos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCarteiraDeProjetos.SizeF = new System.Drawing.SizeF(2518.368F, 79.58667F);
        this.lblCarteiraDeProjetos.StylePriority.UseFont = false;
        this.lblCarteiraDeProjetos.StylePriority.UseTextAlignment = false;
        this.lblCarteiraDeProjetos.Text = "Carteira de projetos: Projetos que acesso";
        this.lblCarteiraDeProjetos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.BorderColor = System.Drawing.Color.Blue;
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrPageInfo2,
            this.xrLogoEntidade});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 69.8783F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageFooter_BeforePrint);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.Format = "Data de Impressão: {0:dd/MM/yyyy}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(730.25F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 9F);
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1458.695F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(304.271F, 58.42F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLogoEntidade
        // 
        this.xrLogoEntidade.Dpi = 254F;
        this.xrLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(2606.52F, 0F);
        this.xrLogoEntidade.Name = "xrLogoEntidade";
        this.xrLogoEntidade.SizeF = new System.Drawing.SizeF(235.479F, 69.8783F);
        this.xrLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        this.xrLogoEntidade.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLogoEntidade_BeforePrint);
        // 
        // rel_ImpressaoPainelProjetos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(64, 64, 64, 64);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 5F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "15.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_ImpressaoPainelProjetos_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosHistoricoDesempenho)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoPainelProjetos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDesempenho)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartDesempGeralRealizado)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartDesempGeralPrevisto)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDesempenhoUnidade)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosDespesaUnidade)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.chartProjetosReceitaUnidade)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void InitData(string anoFinanceiro)
    {
        if (anoFinanceiro == "T")
        {
            anoContabil = -1;
        }
        else
        {
            anoContabil = DateTime.Now.Year;
        }


        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        string where1 = "";
        string where2 = "";
        string where3 = "";
        string where4 = "";
        string where5 = "";
        string where6 = "";

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());
        dsImpressaoPainelProjetos1.Tables.Clear();

        string[] tableNames = new string[] { "ProjetosDesempenhoGeral", "ProjetosDesempenho", "ProjetosDesempenhoUnidade", "ProjetosHistoricoDesempenho", "ProjetosDespesaUnidade", "ProjetosReceitaUnidade" };



        #region Comando SQL

        #region grafico 001 - vc001.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where1 += " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where1 += " AND rp.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where1 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);

        //DataSet ds_vc_001 = cDados.getOrcamentoTrabalhoProjetos(idUsuarioLogado, codigoEntidade, anoContabil, where1);
        DataSet ds_vc_001 = cDados.getResumoDesempenhoGeralEntidade(idUsuarioLogado, codigoEntidade, anoContabil, where1);

        dsImpressaoPainelProjetos1.Tables.Add(ds_vc_001.Tables[0].Copy());
        dsImpressaoPainelProjetos1.Tables[0].TableName = tableNames[0];
        //Data Set contendo a tabela com os dados a serem carregados no gráfico 

        #endregion

        #region grafico 002 - vc002.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where2 = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where2 += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where2 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        //DataSet ds_vc_002 = cDados.getDesempenhoProjetosVC(idUsuarioLogado, codigoEntidade, where2);
        DataSet ds_vc_002 = cDados.getDesempenhoProjetosVCComCor(idUsuarioLogado, codigoEntidade, where2);
        DataView dv = ds_vc_002.Tables[0].AsDataView();
        dv.RowFilter = "Quantidade > 0";
        dsImpressaoPainelProjetos1.Tables.Add(dv.ToTable());
        dsImpressaoPainelProjetos1.Tables[1].TableName = tableNames[1];
        #endregion

        #region grafico 003 - vc003.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where3 = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where3 += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where3 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);

        DataSet ds_vc_003 = cDados.getDesempenhoProjetosUnidadeVC(idUsuarioLogado, codigoEntidade, where3);
        dsImpressaoPainelProjetos1.Tables.Add(ds_vc_003.Tables[0].Copy());
        dsImpressaoPainelProjetos1.Tables[2].TableName = tableNames[2];

        #endregion

        #region grafico 004 - vc007.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where4 = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where4 += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where4 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);

        where4 += string.Format(" AND p.UltimaAtualizacao > CONVERT(DateTime, '{0}', 103) ", "01/" + DateTime.Now.AddMonths(-5).Month + "/" + DateTime.Now.AddMonths(-6).Year);

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        //DataSet ds_vc_004 = cDados.getHistoricoDesempenhoProjetosVC(idUsuarioLogado, codigoEntidade, where4);
        DataSet ds_vc_004 = cDados.getHistoricoDesempenhoProjetosVC_ordena(idUsuarioLogado, codigoEntidade, where4);

        dsImpressaoPainelProjetos1.Tables.Add(ds_vc_004.Tables[0].Copy());
        dsImpressaoPainelProjetos1.Tables[3].TableName = tableNames[3];
        #endregion

        #region grafico 005 - vc005.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where5 = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where5 += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where5 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);


        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds_vc_005 = cDados.getOrcamentoTrabalhoPorUnidade(idUsuarioLogado, codigoEntidade, anoContabil, where5);
        dsImpressaoPainelProjetos1.Tables.Add(ds_vc_005.Tables[0].Copy());
        dsImpressaoPainelProjetos1.Tables[4].TableName = tableNames[4];
        #endregion

        #region grafico 006 - vc006.aspx

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where6 = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where6 += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where6 += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoCarteira);


        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds_vc_006 = cDados.getOrcamentoTrabalhoPorUnidade(idUsuarioLogado, codigoEntidade, anoContabil, where6);
        dsImpressaoPainelProjetos1.Tables.Add(ds_vc_005.Tables[0].Copy());
        dsImpressaoPainelProjetos1.Tables[5].TableName = tableNames[5];

        #endregion

        #endregion


    }


    #region Event Handlers
    private void chart_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //XRChart chart = (XRChart)sender;
        //DefineCoresSeriesGraficosStatus(chart);
        //int codigoObjeto = Convert.ToInt32(chart.Report.GetCurrentColumnValue("CodigoObjeto"));
        //foreach (Series serie in chart.Series)
        //{
        //    serie.DataFilters.ClearAndAddRange(new DataFilter[] {
        //        new DataFilter("CodigoObjeto", "System.Int32", DataFilterCondition.Equal, codigoObjeto)});
        //}
    }

    private void rel_ImpressaoPainelProjetos_DataSourceDemanded(object sender, EventArgs e)
    {
        //string connectionString = "";
    }

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string where = string.Format(" AND cp.CodigoCarteira = {0}", codigoCarteira);
        DataSet ds = cDados.getCarteirasDeProjetos(where);

        string nomeEntidade = "";
        DataSet dsNomeEntidade = cDados.getUnidade(string.Format(@" AND CodigoUnidadeNegocio = {0}", cDados.getInfoSistema("CodigoEntidade").ToString()));
        if (cDados.DataSetOk(dsNomeEntidade) && cDados.DataTableOk(dsNomeEntidade.Tables[0]))
        {
            nomeEntidade = dsNomeEntidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        string nomeCarteira = "";

        string labelCarteira = "";

        DataSet dsLabelCarteira = cDados.getParametrosSistema(codigoEntidade, "labelCarteiras");
        if (cDados.DataSetOk(dsLabelCarteira))
        {
            labelCarteira = dsLabelCarteira.Tables[0].Rows[0]["labelCarteiras"].ToString();
        }

        if (cDados.DataSetOk(ds))
        {
            nomeCarteira = ds.Tables[0].Rows[0]["NomeCarteira"].ToString();
        }
        lblCarteiraDeProjetos.Text = nomeEntidade;
    }

    private void chartProjetosDesempenho_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        Pie3DDrawOptions Ldraw = e.LegendDrawOptions as Pie3DDrawOptions;
        //PieDrawOptions Ldraw = e.LegendDrawOptions as PieDrawOptions;

        string x = e.LabelText;
        string argumento = e.SeriesPoint.Argument;
        string valor = (e.SeriesPoint.Values.Length > 0) ? e.SeriesPoint.Values[0].ToString() : "";

        //Ldraw.FillStyle.FillMode = FillMode3D.Solid;

        if (argumento == "Satisfatório")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml(cDados.corSatisfatorio.IndexOf('#') == -1 ? "#" + cDados.corSatisfatorio : cDados.corSatisfatorio);
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Atenção")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml(cDados.corAtencao.IndexOf('#') == -1 ? "#" + cDados.corAtencao : cDados.corAtencao);
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Finalizando")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml("#ff6600");
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Excelente")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml(cDados.corExcelente.IndexOf('#') == -1 ? "#" + cDados.corExcelente : cDados.corExcelente);
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Não Iniciado")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml("#F3F3F3");
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Crítico")
        {
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml(cDados.corCritico.IndexOf('#') == -1 ? "#" + cDados.corCritico : cDados.corCritico);
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        else if (argumento == "Sem Informação")
        {
            //243; 243; 243
            Ldraw.Color = System.Drawing.ColorTranslator.FromHtml("#F3F3F3");
            e.SeriesDrawOptions.Color = Ldraw.Color;
        }
        e.LegendTextColor = Color.Black;

    }

    private void chartProjetosReceitaUnidade_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisY)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string valorX = value.ToString();
                valorX = string.Format("{0:0.}", decimal.Parse(valorX));
                string concatena = "";
                if (decimal.Parse(valorX) > 999 && decimal.Parse(valorX) < 1000000)
                {
                    concatena = "K";
                    valorX = (decimal.Parse(valorX) / 1000).ToString();
                }
                else if (decimal.Parse(valorX) > 999999)
                {
                    concatena = "M";
                    valorX = (decimal.Parse(valorX) / 1000000).ToString();
                }
                valorX = string.Format("{0:0.}{1}", decimal.Parse(valorX), concatena);
                e.Item.Text = valorX;
            }

        }
        if (e.Item.Axis is AxisX)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string codigo = value.ToString();
                if (codigo != "" && codigo != "-1")
                {
                    string select1 = string.Format(
                @" Codigo = {0} ", codigo);
                    DataRow[] rowsPeriodo = dsImpressaoPainelProjetos1.Tables["ProjetosReceitaUnidade"].Select(select1);
                    if (rowsPeriodo.Length > 0)
                    {
                        e.Item.Text = rowsPeriodo[0]["Descricao"].ToString();
                    }
                }
            }
        }

    }

    private void chartProjetosHistoricoDesempenho_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisX)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string strData = value.ToString();

                string select1 = string.Format(
                @" codigoOrdenacao = '{0}' ", strData);
                DataRow[] rowsPeriodo = dsImpressaoPainelProjetos1.Tables[3].Select(select1);
                if (rowsPeriodo.Length > 0)
                {
                    e.Item.Text = rowsPeriodo[0]["Periodo"].ToString();
                }
            }
        }
    }

    private void chartProjetosDesempenhoUnidade_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisX)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string codigo = value.ToString();
                if (codigo != "")
                {
                    string select1 = string.Format(
                @" Codigo = {0} ", codigo);
                    DataRow[] rowsPeriodo = dsImpressaoPainelProjetos1.Tables["ProjetosDesempenhoUnidade"].Select(select1);
                    if (rowsPeriodo.Length > 0)
                    {
                        e.Item.Text = rowsPeriodo[0]["Descricao"].ToString();
                    }
                }
            }
        }
    }

    private void chartProjetosDespesaUnidade_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisY)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string valorX = value.ToString();
                valorX = string.Format("{0:0.}", decimal.Parse(valorX));
                string concatena = "";
                if (decimal.Parse(valorX) > 999 && decimal.Parse(valorX) < 1000000)
                {
                    concatena = "K";
                    valorX = (decimal.Parse(valorX) / 1000).ToString();
                }
                else if (decimal.Parse(valorX) > 999999)
                {
                    concatena = "M";
                    valorX = (decimal.Parse(valorX) / 1000000).ToString();
                }
                valorX = string.Format("{0:0.}{1}", decimal.Parse(valorX), concatena);
                e.Item.Text = valorX;
            }
        }
        if (e.Item.Axis is AxisX)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string codigo = value.ToString();
                if (codigo != "")
                {
                    string select1 = string.Format(
                @" Codigo = {0} ", codigo);
                    DataRow[] rowsPeriodo = ((XRChart)(sender)).Name == "chartProjetosDespesaUnidade" ? dsImpressaoPainelProjetos1.Tables["ProjetosDespesaUnidade"].Select(select1) : dsImpressaoPainelProjetos1.Tables["ProjetosReceitaUnidade"].Select(select1);
                    if (rowsPeriodo.Length > 0)
                    {
                        e.Item.Text = rowsPeriodo[0]["Descricao"].ToString();
                    }
                }
            }
        }
    }

    private void chartDesempGeralPrevisto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        XRChart chart = (XRChart)sender;
        //despesa

        DataRow dr = dsImpressaoPainelProjetos1.Tables["ProjetosDesempenhoGeral"].Rows[0];

        ConstantLine ctl = new ConstantLine("constantLine1");
        ctl.AxisValue = Convert.ToDecimal(dr["TotalCustoOrcado"]);
        ctl.Color = Color.Black;
        ctl.LineStyle.Thickness = 5;
        ctl.Title.Visible = false;
        ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.ConstantLines.Add(ctl);


        chart.Series[0].Points.Add(new SeriesPoint(" ", decimal.Parse(dr["TotalCustoReal"].ToString())));


        decimal rangeMaximoReceitaOrcada = Convert.ToDecimal(dr["TotalReceitaOrcadaGeral"]);
        decimal rangeMaximoDespesaOrcada = Convert.ToDecimal(dr["TotalCustoOrcadoGeral"]);


        ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;

        if (rangeMaximoReceitaOrcada != 0)
        {
            ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.VisualRange.MaxValue = (rangeMaximoReceitaOrcada > rangeMaximoDespesaOrcada) ? rangeMaximoReceitaOrcada : rangeMaximoDespesaOrcada;
        }


        string cor = dr["CorFinanceiro"].ToString();
        if (cor == "Verde")
        {
            chart.Series[0].View.Color = Color.Green;
        }
        else if (cor == "Amarelo")
        {
            chart.Series[0].View.Color = Color.Yellow;
        }
        else if (cor == "Vermelho")
        {
            chart.Series[0].View.Color = Color.Red;
        }
    }

    private void chartDesempGeralPrevisto_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        if (e.Item.Axis is AxisY)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string valorX = value.ToString();
                valorX = string.Format("{0:0.0}", decimal.Parse(valorX));
                string concatena = "";
                if (decimal.Parse(valorX) > 999 && decimal.Parse(valorX) < 1000000)
                {
                    concatena = "K";
                    valorX = (decimal.Parse(valorX) / 1000).ToString();
                }
                else if (decimal.Parse(valorX) > 999999)
                {
                    concatena = "M";
                    valorX = (decimal.Parse(valorX) / 1000000).ToString();
                }
                valorX = string.Format("{0:0.}{1}", decimal.Parse(valorX), concatena);
                e.Item.Text = valorX;
            }
        }

    }

    private void chartDesempGeralRealizado_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {


        XRChart chart = (XRChart)sender;

        DataRow dr = dsImpressaoPainelProjetos1.Tables["ProjetosDesempenhoGeral"].Rows[0];

        ConstantLine ctl = new ConstantLine("constantLine1");
        ctl.AxisValue = Convert.ToDecimal(dr["TotalReceitaOrcada"]);
        ctl.Color = Color.Black;
        ctl.LineStyle.Thickness = 5;
        ctl.Title.Visible = false;
        ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.ConstantLines.Add(ctl);


        chart.Series[0].Points.Add(new SeriesPoint(" ", decimal.Parse(dr["TotalReceitaReal"].ToString())));


        decimal rangeMaximoReceitaOrcada = Convert.ToDecimal(dr["TotalReceitaOrcadaGeral"]);
        decimal rangeMaximoDespesaOrcada = Convert.ToDecimal(dr["TotalCustoOrcadoGeral"]);


        ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;
        if (rangeMaximoReceitaOrcada != 0)
        {
            ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisY.VisualRange.MaxValue = (rangeMaximoReceitaOrcada > rangeMaximoDespesaOrcada) ? rangeMaximoReceitaOrcada : rangeMaximoDespesaOrcada;
        }
        string cor = dr["CorReceita"].ToString();
        if (cor == "Verde")
        {
            chart.Series[0].View.Color = Color.Green;
        }
        else if (cor == "Amarelo")
        {
            chart.Series[0].View.Color = Color.Yellow;
        }
        else if (cor == "Vermelho")
        {
            chart.Series[0].View.Color = Color.Red;
        }
    }

    private void chartProjetosDesempenho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {


    }


    #endregion

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //xrPictureBox1.Image
    }

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        xrLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private void xrLogoEntidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DefineLogoEntidade();
    }





}
