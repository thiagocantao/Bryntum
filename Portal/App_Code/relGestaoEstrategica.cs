using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using FatorChaveRow = dsRelGestaoEstrategica.FatorChaveRow;

/// <summary>
/// Summary description for relGestaoEstrategica
/// </summary>
public class relGestaoEstrategica : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dsRelGestaoEstrategica dsRelGestaoEstrategica;
    public DevExpress.XtraReports.Parameters.Parameter CodigoMapa;
    private XRControlStyle estiloTitulo;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRControlStyle estileEstruturaProjetoNivel1;
    private XRControlStyle estileEstruturaProjetoNivel2;
    private XRControlStyle estileEstruturaProjetoNivel3;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel4;
    private XRControlStyle estiloTituloRelatório;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel5;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel19;
    private XRShape xrShape1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;
    private XRChart xrChart1;

    dados cDados;

    int codigoEntidade;
    private GroupFooterBand GroupFooter1;
    private XRLine xrLine2;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel18;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel20;
    private PageHeaderBand PageHeader;
    int codigoUsuario;

    public relGestaoEstrategica()
    {
        InitializeComponent();

        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        //xrPictureBox1.Image = cDados.ObtemLogoEntidade();
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
        string resourceFileName = "relGestaoEstrategica.resx";
        System.Resources.ResourceManager resources = global::Resources.relGestaoEstrategica.ResourceManager;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.PointSeriesView pointSeriesView1 = new DevExpress.XtraCharts.PointSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.PointSeriesView pointSeriesView2 = new DevExpress.XtraCharts.PointSeriesView();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.PointSeriesView pointSeriesView3 = new DevExpress.XtraCharts.PointSeriesView();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel4 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.PointSeriesView pointSeriesView4 = new DevExpress.XtraCharts.PointSeriesView();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.dsRelGestaoEstrategica = new dsRelGestaoEstrategica();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.CodigoMapa = new DevExpress.XtraReports.Parameters.Parameter();
        this.estiloTitulo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.estileEstruturaProjetoNivel1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.estileEstruturaProjetoNivel2 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.estileEstruturaProjetoNivel3 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.estiloTituloRelatório = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategica)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrLabel18,
            this.xrLabel5,
            this.xrShape1,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel19,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrChart1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1350F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel20
        // 
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrLabel20.Multiline = true;
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(650F, 50F);
        this.xrLabel20.StyleName = "estiloTitulo";
        this.xrLabel20.Text = "Fator Chave de Competitividade:";
        this.xrLabel20.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1025F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(325F, 50F);
        this.xrLabel18.StyleName = "estiloTitulo";
        this.xrLabel18.Text = "Status da meta:";
        this.xrLabel18.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.NomeFatorChave"),
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(650.0001F, 50F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1200F, 50F);
        this.xrLabel5.StyleName = "estiloTitulo";
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "xrLabel5";
        this.xrLabel5.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrShape1
        // 
        this.xrShape1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("FillColor", null, "FatorChave.StatusCorFatorChave")});
        this.xrShape1.Dpi = 254F;
        this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(325F, 1025F);
        this.xrShape1.Name = "xrShape1";
        this.xrShape1.Padding = new DevExpress.XtraPrinting.PaddingInfo(34, 34, 8, 9, 254F);
        this.xrShape1.SizeF = new System.Drawing.SizeF(100F, 50F);
        this.xrShape1.StylePriority.UsePadding = false;
        this.xrShape1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.shape_OnEvaluateBinding);
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.UltimaAnaliseAgenda")});
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1275F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(1850F, 50F);
        this.xrLabel17.Text = "xrLabel13";
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1225F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(500F, 50F);
        this.xrLabel16.StyleName = "estiloTitulo";
        this.xrLabel16.Text = "Agenda:";
        this.xrLabel16.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.UltimaAnaliseTendencia")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1150F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(1850F, 50F);
        this.xrLabel15.Text = "xrLabel13";
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1100F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(500F, 50F);
        this.xrLabel14.StyleName = "estiloTitulo";
        this.xrLabel14.Text = "Tendências:";
        this.xrLabel14.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.ComentarioMacroMeta")});
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 925.0001F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(1850F, 50F);
        this.xrLabel13.Text = "xrLabel13";
        // 
        // xrLabel12
        // 
        this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 875F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(500F, 50F);
        this.xrLabel12.StyleName = "estiloTitulo";
        this.xrLabel12.Text = "Comentários:";
        this.xrLabel12.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel19
        // 
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.StatusDescricaoFatorChave")});
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(425.0001F, 1025F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(1425F, 50F);
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "xrLabel19";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(275F, 50F);
        this.xrLabel6.StyleName = "estiloTitulo";
        this.xrLabel6.Text = "Macrometa:";
        this.xrLabel6.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 200F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(275F, 50F);
        this.xrLabel7.StyleName = "estiloTitulo";
        this.xrLabel7.Text = "Indicador:";
        this.xrLabel7.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ForeColor", null, "FatorChave.CorFonteFatorChave")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 275F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(275F, 50F);
        this.xrLabel8.StyleName = "estiloTitulo";
        this.xrLabel8.Text = "Descrição:";
        this.xrLabel8.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.label_OnEvaluateBinding);
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.MacroMeta")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(275F, 125F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(1575F, 49.99998F);
        this.xrLabel9.Text = "xrLabel9";
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.NomeIndicadorMacroMeta")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(275F, 200F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1575F, 50.00002F);
        this.xrLabel10.Text = "xrLabel9";
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.DescricaoMacroMeta")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(275F, 275F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1575F, 50.00009F);
        this.xrLabel11.Text = "xrLabel9";
        // 
        // xrChart1
        // 
        this.xrChart1.AppearanceNameSerializable = "Gray";
        this.xrChart1.BorderColor = System.Drawing.Color.Black;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "FatorChave.FatorChave_Indicador";
        this.xrChart1.DataSource = this.dsRelGestaoEstrategica;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 349.9999F);
        this.xrChart1.Name = "xrChart1";
        this.xrChart1.PaletteName = "Default";
        series1.ArgumentDataMember = "Periodo";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel1.Font = new System.Drawing.Font("Verdana", 8F);
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.Label = pointSeriesLabel1;
        series1.LegendText = "Realizado";
        series1.Name = "SerieRealizado";
        series1.ValueDataMembersSerializable = "ValorRealizado";
        pointSeriesView1.ColorEach = true;
        pointSeriesView1.PointMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        pointSeriesView1.PointMarkerOptions.BorderVisible = false;
        series1.View = pointSeriesView1;
        series2.ArgumentDataMember = "Periodo";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel2.Font = new System.Drawing.Font("Verdana", 8F);
        pointSeriesLabel2.LineLength = 20;
        pointSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        series2.Label = pointSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        series2.LegendText = "Meta";
        series2.Name = "SerieMetaExt";
        series2.ShowInLegend = false;
        series2.ValueDataMembersSerializable = "ValorMeta";
        pointSeriesView2.Color = System.Drawing.Color.White;
        pointSeriesView2.PointMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        pointSeriesView2.PointMarkerOptions.Size = 20;
        series2.View = pointSeriesView2;
        series3.ArgumentDataMember = "Periodo";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Label = pointSeriesLabel3;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.LegendText = "Meta";
        series3.Name = "SerieMeta";
        series3.ValueDataMembersSerializable = "ValorMeta";
        pointSeriesView3.Color = System.Drawing.Color.Red;
        pointSeriesView3.PointMarkerOptions.BorderVisible = false;
        series3.View = pointSeriesView3;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
        pointSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = pointSeriesLabel4;
        this.xrChart1.SeriesTemplate.View = pointSeriesView4;
        this.xrChart1.SizeF = new System.Drawing.SizeF(1850F, 500F);
        this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        // 
        // dsRelGestaoEstrategica
        // 
        this.dsRelGestaoEstrategica.DataSetName = "dsRelGestaoEstrategica";
        this.dsRelGestaoEstrategica.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
        // CodigoMapa
        // 
        this.CodigoMapa.Name = "CodigoMapa";
        this.CodigoMapa.Type = typeof(short);
        this.CodigoMapa.ValueInfo = "0";
        this.CodigoMapa.Visible = false;
        // 
        // estiloTitulo
        // 
        this.estiloTitulo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.estiloTitulo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.estiloTitulo.Name = "estiloTitulo";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.FatorChave_Projeto.NomeProjeto")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1960F, 50F);
        this.xrLabel3.StyleName = "estileEstruturaProjetoNivel3";
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel2
        // 
        this.xrLabel2.BackColor = System.Drawing.Color.Silver;
        this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.FatorChave_Projeto.NomeObjetivoEstrategico")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1960F, 50F);
        this.xrLabel2.StyleName = "estileEstruturaProjetoNivel1";
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseBorderWidth = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // xrLabel1
        // 
        this.xrLabel1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FatorChave.FatorChave_Projeto.AcaoTransformadora")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1960F, 50F);
        this.xrLabel1.StyleName = "estileEstruturaProjetoNivel2";
        this.xrLabel1.StylePriority.UseBackColor = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // estileEstruturaProjetoNivel1
        // 
        this.estileEstruturaProjetoNivel1.Name = "estileEstruturaProjetoNivel1";
        this.estileEstruturaProjetoNivel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 0, 0, 0, 254F);
        // 
        // estileEstruturaProjetoNivel2
        // 
        this.estileEstruturaProjetoNivel2.Name = "estileEstruturaProjetoNivel2";
        this.estileEstruturaProjetoNivel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(75, 0, 0, 0, 254F);
        // 
        // estileEstruturaProjetoNivel3
        // 
        this.estileEstruturaProjetoNivel3.Name = "estileEstruturaProjetoNivel3";
        this.estileEstruturaProjetoNivel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(125, 0, 0, 0, 254F);
        // 
        // xrLabel4
        // 
        this.xrLabel4.CanGrow = false;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(255)))));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(800F, 50F);
        this.xrLabel4.StyleName = "estiloTituloRelatório";
        this.xrLabel4.StylePriority.UseForeColor = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Relatório de Gestão Estratégica";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "Emissão: {0:d}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1350F, 99.99999F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(350F, 50F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(1400F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(450F, 150F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // estiloTituloRelatório
        // 
        this.estiloTituloRelatório.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.estiloTituloRelatório.Name = "estiloTituloRelatório";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader2,
            this.GroupHeader3});
        this.DetailReport1.DataMember = "FatorChave.FatorChave_Projeto";
        this.DetailReport1.DataSource = this.dsRelGestaoEstrategica;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Expanded = false;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.DetailCountOnEmptyDataSource = 0;
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.DetailReport1.Visible = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 50F;
        this.Detail2.Name = "Detail2";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjetivoEstrategico", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 50F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("AcaoTransformadora", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader3.HeightF = 50F;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 100F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(255)))));
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.Padding = new DevExpress.XtraPrinting.PaddingInfo(24, 25, 0, 0, 254F);
        this.xrLine2.SizeF = new System.Drawing.SizeF(1850F, 99.99999F);
        this.xrLine2.StylePriority.UseForeColor = false;
        this.xrLine2.StylePriority.UsePadding = false;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoFatorChave", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 0F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 150F;
        this.PageFooter.Name = "PageFooter";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrPageInfo1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 150F;
        this.PageHeader.Name = "PageHeader";
        // 
        // relGestaoEstrategica
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport1,
            this.GroupFooter1,
            this.GroupHeader1,
            this.PageFooter,
            this.PageHeader});
        this.DataMember = "FatorChave";
        this.DataSource = this.dsRelGestaoEstrategica;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(99, 0, 99, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodigoMapa});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 10F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.estiloTitulo,
            this.estileEstruturaProjetoNivel1,
            this.estileEstruturaProjetoNivel2,
            this.estileEstruturaProjetoNivel3,
            this.estiloTituloRelatório});
        this.Version = "15.1";
        this.Watermark.Image = ((System.Drawing.Image)(resources.GetObject("relGestaoEstrategica.Watermark.Image")));
        this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relGestaoEstrategica_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategica)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void shape_OnEvaluateBinding(object sender, BindingEventArgs e)
    {
        string nomeCor = e.Value as string;
        e.Value = ObtemCor(nomeCor);
    }

    private static Color ObtemCor(string nomeCor)
    {
        nomeCor = (nomeCor ?? string.Empty).Trim().ToLower();
        switch (nomeCor)
        {
            case "": return Color.Black;
            case "branco": return Color.White;
            case "verde": return Color.Green;
            case "vermelho": return Color.Red;
            case "azul": return Color.Blue;
            case "amarelo": return Color.Yellow;
            case "laranja": return Color.Orange;
            default:
                if (nomeCor.Contains("#"))
                {
                    int r = Convert.ToInt32(nomeCor.Substring(1, 2), 16);
                    int g = Convert.ToInt32(nomeCor.Substring(3, 2), 16);
                    int b = Convert.ToInt32(nomeCor.Substring(5, 2), 16);
                    return Color.FromArgb(r, g, b);
                }

                return Color.FromName(nomeCor);
        }
    }

    private void relGestaoEstrategica_DataSourceDemanded(object sender, EventArgs e)
    {
        string connectionString = cDados.classeDados.getStringConexao();
        string comandoSql;

        #region Comando SQL

        codigoEntidade = 1;
        comandoSql = string.Format(@"
DECLARE @CodigoMapa INT,
        @CodigoEntidade INT,
        @CodigoUsuario INT,
        @Ano INT,
        @IniciaisObjetoPespectiva CHAR(2),
        @TipoDetalhe CHAR(1),
        @CodigoFatorChave INT,
        @CodigoIndicador INT
    SET @CodigoMapa = {0}
    SET @CodigoEntidade = {1}
    SET @CodigoUsuario = {2}
    SET @Ano = YEAR(GETDATE())
    SET @IniciaisObjetoPespectiva = 'PP'
    SET @TipoDetalhe = 'A'
    
DECLARE @TblFatorChave TABLE
(
    CodigoFatorChave            INT,
    NomeFatorChave				Varchar(500),
    UltimaAnaliseTendencia		Varchar(1500),
    UltimaAnaliseAgenda			Varchar(1500),
    StatusCorFatorChave			Varchar(50),
    StatusDescricaoFatorChave   Varchar(100),
    CorFonteFatorChave			Varchar(100),
    CodigoIndicadorMacroMeta	Int,
    NomeIndicadorMacroMeta		Varchar(250),
    Fonte						Varchar(250),
    CasasDecimais				Tinyint,
    UnidadeMedida				Varchar(50),
    MacroMeta					Varchar(500),
    ComentarioMacroMeta			Varchar(2000),
    DescricaoMacroMeta			Varchar(2000),
    CodigoUnidadeNegocio		Int
)

DECLARE @TblProjeto TABLE
(
    CodigoFatorChave                    INT,
    CodigoProjeto                       INT,
    NomeProjeto							Varchar(500),
    IndicaProjetoCarteiraPrioritaria	Char(1),
    NomeResponsavel						Varchar(100),
    CorDesempenho						Varchar(30),
    PercentualConcluido					Int,
    PercentualPrevisto					Int,
    PodeAcessarProjeto					Char(1),
    AcaoTransformadora					Varchar(250),
    CodigoObjetivoEstrategico			Int,
    NomeObjetivoEstrategico				Varchar(500),
    ObjetivoProjeto						Varchar(2000)
)

DECLARE @TblIndicadorPeriodicidade TABLE 
(   
    CodigoFatorChave    INT,
    Periodo             Varchar(30), 
    ValorRealizado      Numeric(18,4),
    ValorMeta           Numeric(18,4),         
    CorIndicador        VarChar(8),
	Ano                 Int,
	Mes                 Int
)
        
DECLARE @tblTemp TABLE
(
    Periodo             Varchar(30), 
    ValorRealizado      Numeric(18,4),
    ValorMeta           Numeric(18,4),         
    CorIndicador        VarChar(8),
    Ano                 Int,
    Mes                 Int
)

 INSERT INTO @TblFatorChave
 SELECT f.*
   FROM [dbo].[f_cni_relatorioGestaoEstrategica] (@CodigoMapa) AS f
  ORDER BY
        f.CodigoFatorChave

DECLARE cursor_FatorChave CURSOR FOR 
 SELECT tbl.CodigoFatorChave, 
        tbl.CodigoIndicadorMacroMeta
   FROM @TblFatorChave AS tbl
   
   OPEN cursor_FatorChave
   
FETCH NEXT FROM cursor_FatorChave INTO @CodigoFatorChave, @CodigoIndicador
    
WHILE @@FETCH_STATUS = 0
BEGIN
    INSERT INTO @TblProjeto
    SELECT @CodigoFatorChave, f.* 
      FROM [dbo].[f_cni_getProjetosObjetoPorAno] (
        @CodigoFatorChave,
        @IniciaisObjetoPespectiva,
        @CodigoUsuario,
        @Ano) AS f
      
    DELETE FROM @tblTemp      
    INSERT INTO @tblTemp  
      EXEC [dbo].[p_cni_GetIndicadorPeriodicidade] 
		@CodigoUnidadeParam = @CodigoEntidade,
		@CodigoIndicadorParam = @CodigoIndicador,--187,
		@TipoDetalheParam = @TipoDetalhe
		
    INSERT INTO @TblIndicadorPeriodicidade
    SELECT @CodigoFatorChave, t.* FROM @tblTemp AS t
    
    FETCH NEXT FROM cursor_FatorChave INTO @CodigoFatorChave, @CodigoIndicador
END

CLOSE cursor_FatorChave
DEALLOCATE cursor_FatorChave

 SELECT * 
   FROM @TblFatorChave

 SELECT * 
   FROM @TblProjeto

 SELECT * 
   FROM @TblIndicadorPeriodicidade"
            , CodigoMapa.Value
            , codigoEntidade
            , codigoUsuario);

        #endregion

        SqlDataAdapter da = new SqlDataAdapter(comandoSql, connectionString);
        da.TableMappings.Add("Table", "FatorChave");
        da.TableMappings.Add("Table1", "Projeto");
        da.TableMappings.Add("Table2", "Indicador");
        DataAdapter = da;
    }

    private static bool IndicaMesmaCor(Color cor1, Color cor2)
    {
        return
            cor1.A == cor2.A &&
            cor1.R == cor2.R &&
            cor1.G == cor2.G &&
            cor1.B == cor2.B;
    }

    private void label_OnEvaluateBinding(object sender, BindingEventArgs e)
    {
        if (e.Binding.PropertyName == "ForeColor")
        {
            Color corFonte = ObtemCor(e.Value as string);
            if (IndicaMesmaCor(corFonte, Color.White) ||
                IndicaMesmaCor(corFonte, Color.Empty))
            {
                corFonte = Color.Black;
            }
            e.Value = corFonte;
        }
    }

    private void chart_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
    {
        if (e.Series.Name == "SerieRealizado")
        {
            var drFatorChave = (FatorChaveRow)((DataRowView)GetCurrentRow()).Row;
            string periodo = e.SeriesPoint.Argument;
            string cor = drFatorChave.GetIndicadorRows()
                .Single(row => row.Periodo == periodo).CorIndicador;
            e.SeriesDrawOptions.Color = ObtemCor(cor);
        }
    }
}
